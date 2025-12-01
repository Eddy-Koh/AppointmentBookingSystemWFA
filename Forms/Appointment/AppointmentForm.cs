using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AppointmentBookingSystemWFA.Database;
using MySql.Data.MySqlClient;
using System.Drawing;

using AppointmentModel = AppointmentBookingSystemWFA.Models.Appointment;

namespace AppointmentBookingSystemWFA.Forms.Appointment
{
    public partial class AppointmentForm : Form
    { 
        private string _username;
        private string _role;

        public AppointmentForm(string username, string role)
        {
            InitializeComponent();
            _username = username;
            _role = role;
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (_role == "Requester")
            {
                btnCreate.Visible = true;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }

            if (_role == "Approver")
            {
                btnCreate.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                btnApprove.Visible = true;
                btnReject.Visible = true;
            }

            LoadAppointments();          
        }

        private void LoadAppointments()
        {
            var appointments = new List<AppointmentModel>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand("SELECT * FROM appointments", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(new AppointmentModel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            RequesterName = reader["RequesterName"].ToString(),
                            AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                            StartTime = TimeSpan.Parse(reader["StartTime"].ToString()),
                            EndTime = TimeSpan.Parse(reader["EndTime"].ToString()),
                            Reason = reader["Reason"].ToString(),
                            Status = reader["Status"].ToString()
                        });
                    }
                }
            }

            // Filter for Requester: only their own appointments
            if (_role == "Requester")
            {
                appointments = appointments
                    .Where(a => a.RequesterName.Equals(_username, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Sort by date/time
            appointments = appointments
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.StartTime)
                .ThenBy(a => a.EndTime)
                .ToList();

            // Bind to grid
            dgvAppointments.DataSource = appointments.Select(a => new
            {
                a.Id,
                a.RequesterName,
                Date = a.AppointmentDate.ToString("yyyy-MM-dd"),
                Time = $"{a.StartTime:hh\\:mm} - {a.EndTime:hh\\:mm}",
                a.Reason,
                a.Status
            }).ToList();

            dgvAppointments.ReadOnly = true;
            dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Hide();
            var createForm = new CreateAppointmentForm(_username);
            createForm.Show();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvAppointments.CurrentRow.Cells["Id"].Value);

            this.Hide();
            var viewForm = new ViewAppointmentForm(id, _username, _role);
            viewForm.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null) return;
            string status = dgvAppointments.CurrentRow.Cells["Status"].Value.ToString();
            int id = Convert.ToInt32(dgvAppointments.CurrentRow.Cells["Id"].Value);

            if (status == "Pending")
            {
                this.Hide();
                var editForm = new EditAppointmentForm(id, _username);
                editForm.Show();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Only pending appointments can be edited.";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null) return;
            string status = dgvAppointments.CurrentRow.Cells["Status"].Value.ToString();
            int id = Convert.ToInt32(dgvAppointments.CurrentRow.Cells["Id"].Value);

            if (status == "Pending" || status == "Rejected")
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    var cmd = new MySqlCommand("DELETE FROM appointments WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                LoadAppointments();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Approved appointments cannot be deleted.";
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null) return;
            string status = dgvAppointments.CurrentRow.Cells["Status"].Value.ToString();
            int id = Convert.ToInt32(dgvAppointments.CurrentRow.Cells["Id"].Value);

            if (status != "Pending")
            {
                lblError.Visible = true;
                lblError.Text = "Only pending appointment can be approved.";
                return;
            }

            // Get the appointment details
            DateTime apptDate;
            TimeSpan startTime;
            TimeSpan endTime;

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand(
                    "SELECT AppointmentDate, StartTime, EndTime FROM appointments WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        apptDate = Convert.ToDateTime(reader["AppointmentDate"]);
                        startTime = (TimeSpan)reader["StartTime"];
                        endTime = (TimeSpan)reader["EndTime"];
                    }
                    else return;
                }
            }

            // Approve selected appointment
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand("UPDATE appointments SET Status='Approved' WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            // Reject all overlapping pending appointments
            using (var conn = DatabaseHelper.GetConnection())
            {
                var rejectCmd = new MySqlCommand(
                    @"UPDATE appointments 
                      SET Status='Rejected' 
                      WHERE AppointmentDate = @date
                        AND Status='Pending'
                        AND Id <> @id
                        AND (StartTime < @endTime AND EndTime > @startTime)", conn);

                rejectCmd.Parameters.AddWithValue("@date", apptDate.Date);
                rejectCmd.Parameters.AddWithValue("@startTime", startTime);
                rejectCmd.Parameters.AddWithValue("@endTime", endTime);
                rejectCmd.Parameters.AddWithValue("@id", id);

                rejectCmd.ExecuteNonQuery();
            }

            lblError.Visible = false;
            MessageBox.Show("Appointment approved successfully. Other appointments that crash with this appointment will be rejected.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAppointments(); 
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.CurrentRow == null) return;
            string status = dgvAppointments.CurrentRow.Cells["Status"].Value.ToString();
            int id = Convert.ToInt32(dgvAppointments.CurrentRow.Cells["Id"].Value);

            if (status == "Pending")
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    var cmd = new MySqlCommand("UPDATE appointments SET Status='Rejected' WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                lblError.Visible = false;
                MessageBox.Show("Appointment rejected successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAppointments();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Only pending appointment can be rejected.";
            }
        }

        private void backToHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            var homeForm = new Home.HomeForm(_username, _role);
            homeForm.Show();
        }

        private void backToHome_MouseEnter(object sender, EventArgs e)
        {
            backToHome.ForeColor = Color.Blue;
            backToHome.Cursor = Cursors.Hand;
        }

        private void backToHome_MouseLeave(object sender, EventArgs e)
        {
            backToHome.ForeColor = Color.Black;
        }
    }
}
