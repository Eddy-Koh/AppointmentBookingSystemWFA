using System;
using System.Windows.Forms;
using AppointmentBookingSystemWFA.Database;
using MySql.Data.MySqlClient;

namespace AppointmentBookingSystemWFA.Forms.Appointment
{
    public partial class EditAppointmentForm : Form
    {
        private int _appointmentId;
        private string _username;

        public EditAppointmentForm(int appointmentId, string username)
        {
            InitializeComponent();
            _appointmentId = appointmentId;
            _username = username;
        }

        private void EditAppointmentForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            LoadAppointment();
        }

        private void LoadAppointment()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand("SELECT * FROM appointments WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", _appointmentId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtRequesterName.Text = reader["RequesterName"].ToString();
                        dtpAppointmentDate.Value = Convert.ToDateTime(reader["AppointmentDate"]);
                        dtpStartTime.Value = DateTime.Today.Add(TimeSpan.Parse(reader["StartTime"].ToString()));
                        dtpEndTime.Value = DateTime.Today.Add(TimeSpan.Parse(reader["EndTime"].ToString()));
                        txtReason.Text = reader["Reason"].ToString();
                        txtStatus.Text = reader["Status"].ToString();
                    }
                }
            }

            dtpAppointmentDate.MinDate = DateTime.Today.AddDays(1);
            dtpStartTime.Format = DateTimePickerFormat.Time;
            dtpStartTime.ShowUpDown = true;
            dtpEndTime.Format = DateTimePickerFormat.Time;
            dtpEndTime.ShowUpDown = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DateTime appointmentDate = dtpAppointmentDate.Value.Date;
            TimeSpan startTime = dtpStartTime.Value.TimeOfDay;
            TimeSpan endTime = dtpEndTime.Value.TimeOfDay;
            string reason = txtReason.Text.Trim();

            bool error = ValidateAppointment(appointmentDate, startTime, endTime);
            if (error == true) return; // back to appointment form if error found

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
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(@"UPDATE appointments SET 
                    AppointmentDate=@date,
                    StartTime=@start,
                    EndTime=@end,
                    Reason=@reason
                    WHERE Id=@id", conn);

                cmd.Parameters.AddWithValue("@date", appointmentDate);
                cmd.Parameters.AddWithValue("@start", startTime);
                cmd.Parameters.AddWithValue("@end", endTime);
                cmd.Parameters.AddWithValue("@reason", reason);
                cmd.Parameters.AddWithValue("@id", _appointmentId);

                cmd.ExecuteNonQuery();
            }

            lblError.Visible = false;
            MessageBox.Show("Appointment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
            var appointmentForm = new AppointmentForm(_username, "Requester");
            appointmentForm.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
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
    }
}
