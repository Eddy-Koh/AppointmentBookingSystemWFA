using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using AppointmentBookingSystemWFA.Database;

namespace AppointmentBookingSystemWFA.Forms.User
{
    public partial class ViewUserForm : Form
    {
        private string _username;
        private string _requester;

        public ViewUserForm(string username, string requester)
        {
            InitializeComponent();
            _username = username;
            _requester = requester;
        }

        private void ViewUserForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            LoadUser();
        }

        private void LoadUser()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand("SELECT * FROM users WHERE Username=@username", conn);
                cmd.Parameters.AddWithValue("@username", _requester);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtUsername.Text = reader["Username"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtMobilePhone.Text = reader["MobilePhone"].ToString();
                        txtRole.Text = reader["Role"].ToString();
                        txtStatus.Text = reader["ApprovalStatus"].ToString();
                    }
                }
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if(txtStatus.Text != "Pending")
            {
                lblError.Visible = true;
                lblError.Text = "Only pending user can be accepted.";
                return;
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand("UPDATE users SET ApprovalStatus='Accepted' WHERE Username=@username", conn);
                cmd.Parameters.AddWithValue("@username", _requester);
                cmd.ExecuteNonQuery();
            }

            lblError.Visible = false;
            // Update success
            MessageBox.Show("Update successful !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
            var userManagementForm = new UserManagementForm(_username, "Approver");
            userManagementForm.Show();
            return;
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (txtStatus.Text != "Pending")
            {
                lblError.Visible = true;
                lblError.Text = "Only pending user can be rejected.";
                return;
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand("UPDATE users SET ApprovalStatus='Rejected' WHERE Username=@username", conn);
                cmd.Parameters.AddWithValue("@username", _requester);
                cmd.ExecuteNonQuery();
            }

            lblError.Visible = false;
            // Update success
            MessageBox.Show("Update successful !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
            var userManagementForm = new UserManagementForm(_username, "Approver");
            userManagementForm.Show();
            return;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var userManagementForm = new UserManagementForm(_username, "Approver");
            userManagementForm.Show();
            return;
        }
    }
}
