using System;
using System.Windows.Forms;

namespace AppointmentBookingSystemWFA.Forms.Home
{
    public partial class HomeForm : Form
    {
        private string _username;
        private string _role;

        public HomeForm(string username, string role)
        {
            InitializeComponent();
            _username = username;
            _role = role;
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_username))
            {
                lblMessage.Text = "You need to login to access the system.";
                btnLogin.Visible = true;
                btnRegister.Visible = true;
                btnLogout.Visible = false;

                btnAppointments.Visible = false;
                btnManageAppointments.Visible = false;
                btnManageUsers.Visible = false;
            }
            else
            {
                lblMessage.Text = $"Welcome, {_username} ({_role}) !";

                btnLogin.Visible = false;
                btnRegister.Visible = false;

                if (_role == "Approver")
                {
                    btnManageAppointments.Visible = true;
                    btnManageUsers.Visible = true;
                    btnAppointments.Visible = false;
                    btnLogout.Visible = true;
                }
                else
                {
                    btnAppointments.Visible = true;
                    btnManageAppointments.Visible = false;
                    btnManageUsers.Visible = false;
                    btnLogout.Visible = true;
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var loginForm = new Account.LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new Account.RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            var appointmentForm = new Appointment.AppointmentForm(_username, _role);
            appointmentForm.Show();
            this.Hide();
        }

        private void btnManageAppointments_Click(object sender, EventArgs e)
        {
            var appointmentForm = new Appointment.AppointmentForm(_username, _role);
            appointmentForm.Show();
            this.Hide();
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            var userForm = new User.UserManagementForm(_username, _role);
            userForm.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var logoutForm = new Account.LogoutForm(_username, _role);
            logoutForm.Show();
            this.Hide();
        }
    }
}
