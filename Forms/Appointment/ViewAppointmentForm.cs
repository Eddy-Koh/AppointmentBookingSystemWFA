using System;
using System.Windows.Forms;
using AppointmentBookingSystemWFA.Database;
using MySql.Data.MySqlClient;

namespace AppointmentBookingSystemWFA.Forms.Appointment
{
    public partial class ViewAppointmentForm : Form
    {
        private int _id;
        private string _username;
        private string _role;
        public ViewAppointmentForm(int id, string username, string role)
        {
            InitializeComponent();
            _id = id;
            _username = username;
            _role = role;
        }

        private void ViewAppointmentForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            LoadAppointment();
        }

        private void LoadAppointment()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand("SELECT * FROM appointments WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", _id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtRequesterName.Text = reader["RequesterName"].ToString();
                        txtAppointmentDate.Text = Convert.ToDateTime(reader["AppointmentDate"])
                           .ToString("yyyy-MM-dd");
                        txtStartTime.Text = reader["StartTime"].ToString();
                        txtEndTime.Text = reader["EndTime"].ToString();
                        txtReason.Text = reader["Reason"].ToString();
                        txtStatus.Text = reader["Status"].ToString();
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(_role == "Approver")
            {
                lblError.Visible = true;
                lblError.Text = "Only requester can edit the appointment details";
                return;
            }

            if(txtStatus.Text == "Pending")
            {
                this.Hide();
                var editForm = new EditAppointmentForm(_id, _username);
                editForm.Show();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Only pending appointment can be edited";
                return;
            } 
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var appointmentForm = new AppointmentForm(_username, _role);
            appointmentForm.Show();
        }
    }
}
