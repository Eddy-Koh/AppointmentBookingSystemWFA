using System;
using System.Windows.Forms;

namespace AppointmentBookingSystemWFA.Forms.Account
{
    public partial class LogoutForm : Form
    {
        private string _username;
        private string _role;

        public LogoutForm(string username, string role)
        {
            InitializeComponent();
            _username = username;
            _role = role;
        }

        private void LogoutForm_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "Are you sure you want to log out?";
        }

        private void btnConfirmLogout_Click(object sender, EventArgs e)
        {
            // Clear login state (simulate Session.Clear)
            _username = null;
            _role = null;

            MessageBox.Show("You have been logged out.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Redirect to HomeForm without user info
            this.Hide();
            var homeForm = new Home.HomeForm(null, null);
            homeForm.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Go back to HomeForm with current user info
            this.Hide();
            var homeForm = new Home.HomeForm(_username, _role);
            homeForm.Show();
        }
    }
}
