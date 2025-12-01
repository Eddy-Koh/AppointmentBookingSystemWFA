using System;
using System.Windows.Forms;
using AppointmentBookingSystemWFA.Database;
using MySql.Data.MySqlClient;

namespace AppointmentBookingSystemWFA.Forms.Appointment
{
    public partial class CreateAppointmentForm : Form
    {
        private string _username;

        public CreateAppointmentForm(string username)
        {
            InitializeComponent();
            _username = username;
        }

        private void CreateAppointmentForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;

            txtRequesterName.Text = _username;
            dtpAppointmentDate.MinDate = DateTime.Today.AddDays(1);

            dtpStartTime.Format = DateTimePickerFormat.Time;
            dtpStartTime.ShowUpDown = true;

            dtpEndTime.Format = DateTimePickerFormat.Time;
            dtpEndTime.ShowUpDown = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime appointmentDate = dtpAppointmentDate.Value.Date;
            TimeSpan startTime = dtpStartTime.Value.TimeOfDay;
            TimeSpan endTime = dtpEndTime.Value.TimeOfDay;
            string reason = txtReason.Text.Trim();

            bool error = ValidateAppointment(appointmentDate, startTime, endTime);
            if (error == true) return; // back to create appointment form if error found

            // Check for conflict with approved appointments
            using (var conn = DatabaseHelper.GetConnection())
            {
                var checkCmd = new MySqlCommand(@"
                    SELECT COUNT(*) 
                    FROM appointments
                    WHERE AppointmentDate = @date
                      AND Status = 'Approved'
                      AND (StartTime < @endTime AND EndTime > @startTime)", conn);

                checkCmd.Parameters.AddWithValue("@date", appointmentDate);
                checkCmd.Parameters.AddWithValue("@startTime", startTime);
                checkCmd.Parameters.AddWithValue("@endTime", endTime);

                int conflictCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (conflictCount > 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "The selected time conflicts with an approved appointment. Please choose another time.";
                    return;
                }
            }

            // No conflict then proceed to insert
            int id = Math.Abs(Guid.NewGuid().GetHashCode());

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(@"INSERT INTO appointments 
                    (Id, RequesterName, AppointmentDate, StartTime, EndTime, Reason, Status) 
                    VALUES (@Id, @RequesterName, @AppointmentDate, @StartTime, @EndTime, @Reason, @Status)", conn);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@RequesterName", _username);
                cmd.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                cmd.Parameters.AddWithValue("@StartTime", startTime);
                cmd.Parameters.AddWithValue("@EndTime", endTime);
                cmd.Parameters.AddWithValue("@Reason", reason);
                cmd.Parameters.AddWithValue("@Status", "Pending");

                cmd.ExecuteNonQuery();
            }

            lblError.Visible = false;
            MessageBox.Show("Appointment created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Redirect back to AppointmentForm
            this.Hide();
            var appointmentForm = new AppointmentForm(_username, "Requester");
            appointmentForm.Show();
        }

        private bool ValidateAppointment(DateTime date, TimeSpan start, TimeSpan end)
        {
            if (date == DateTime.MinValue)
            {
                lblError.Visible = true;
                lblError.Text = "Please select an appointment date.";
                return true;
            }

            if (date <= DateTime.Today)
            {
                lblError.Visible = true;
                lblError.Text = "Appointment date must be in the future.";
                return true;
            }

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                lblError.Visible = true;
                lblError.Text = "Appointments cannot be scheduled on weekends.";
                return true;
            }

            if (start == TimeSpan.Zero)
            {
                lblError.Visible = true;
                lblError.Text = "Please select a valid start time.";
                return true;
            }

            if (start < TimeSpan.FromHours(8))
            {
                lblError.Visible = true;
                lblError.Text = "Start time must be at or after 8:00 am.";
                return true;
            }

            if (end == TimeSpan.Zero)
            {
                lblError.Visible = true;
                lblError.Text = "Please select a valid end time.";
                return true;
            }

            if (end > TimeSpan.FromHours(17))
            {
                lblError.Visible = true;
                lblError.Text = "End time must be at or before 5:00 pm.";
                return true;
            }

            if (end <= start)
            {
                lblError.Visible = true;
                lblError.Text = "End time must be after start time.";
                return true;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                lblError.Visible = true;
                lblError.Text = "Reason cannot be empty.";
                return true;
            }

            return false; // no error found
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var appointmentForm = new AppointmentForm(_username, "Requester");
            appointmentForm.Show();
        }
    }
}
