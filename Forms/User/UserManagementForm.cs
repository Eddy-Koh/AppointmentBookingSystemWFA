using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AppointmentBookingSystemWFA.Database;
using MySql.Data.MySqlClient;
using System.Drawing;

using UserModel = AppointmentBookingSystemWFA.Models.User;

namespace AppointmentBookingSystemWFA.Forms.User
{
    public partial class UserManagementForm : Form
    {
        private string _username;
        private string _role;

        public UserManagementForm(string username, string role)
        {
            InitializeComponent();
            _username = username;
            _role = role;
            LoadUsers();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        // Load all users from database and bind to DataGridView
        private void LoadUsers()
        {
            var users = new List<UserModel>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand("SELECT * FROM users", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserModel
                        {
                            Username = reader["Username"].ToString(),
                            MobilePhone = reader["MobilePhone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(), // not displayed
                            Role = reader["Role"].ToString(),
                            ApprovalStatus = reader["ApprovalStatus"].ToString()
                        });
                    }
                }
            }

            // Bind only the columns that want to show
            dgvUsers.DataSource = users.Select(u => new
            {
                u.Username,
                u.MobilePhone,
                u.Email,
                u.Role,
                Status = u.Role == "Approver" ? u.ApprovalStatus : "N/A"
            }).ToList();
        }

        // View selected user
        private void btnView_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                lblError.Visible = true;
                lblError.Text = "Please select a user.";
                return;
            }

            string requester = dgvUsers.CurrentRow.Cells["Username"].Value.ToString();

            lblError.Visible = false;
            this.Hide();
            var viewUserForm = new ViewUserForm(_username, requester);
            viewUserForm.Show();
        }

        // Approve selected user
        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                lblError.Visible = true;
                lblError.Text = "Please select a user.";
                return;
            }

            string status = dgvUsers.CurrentRow.Cells["Status"].Value.ToString();
            if (status != "Pending")
            {
                lblError.Visible = true;
                lblError.Text = "Only pending user can be accepted.";
                return;
            }

            string username = dgvUsers.CurrentRow.Cells["Username"].Value.ToString();

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand("UPDATE users SET ApprovalStatus='Accepted' WHERE Username=@username", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }

            lblError.Visible = false;
            MessageBox.Show("User accepted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadUsers();
        }

        // Reject selected user
        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                lblError.Visible = true;
                lblError.Text = "Please select a user.";
                return;
            }

            string status = dgvUsers.CurrentRow.Cells["Status"].Value.ToString();
            if (status != "Pending")
            {
                lblError.Visible = true;
                lblError.Text = "Only pending user can be rejected.";
                return;
            }

            string username = dgvUsers.CurrentRow.Cells["Username"].Value.ToString();

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand("UPDATE users SET ApprovalStatus='Rejected' WHERE Username=@username", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }

            lblError.Visible = false;
            MessageBox.Show("User rejected successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadUsers();
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
