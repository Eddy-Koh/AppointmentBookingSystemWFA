using System;
using System.Windows.Forms;
using AppointmentBookingSystemWFA.Database;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace AppointmentBookingSystemWFA.Forms.Account
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;   // hide error label initially 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Visible = true;
                lblError.Text = "Username and password are required.";
                return;
            }

            string hashedInput = HashPassword(password);

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmdUser = new MySqlCommand(
                    "SELECT * FROM users WHERE BINARY Username=@user", conn);
                cmdUser.Parameters.AddWithValue("@user", username);

                using (var reader = cmdUser.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        lblError.Visible = true;
                        lblError.Text = "User not found.";
                        return;
                    }

                    string storedPassword = reader["Password"].ToString();
                    string role = reader["Role"].ToString();
                    string status = reader["ApprovalStatus"].ToString();

                    if (storedPassword != hashedInput)
                    {
                        lblError.Visible = true;
                        lblError.Text = "Wrong password.";
                        return;
                    }

                    if (role == "Approver" && status == "Pending")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Your approver registration is still pending approval.";
                        return;
                    }

                    if (role == "Approver" && status == "Rejected")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Your approver registration was rejected.";
                        return;
                    }

                    lblError.Visible = false;
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    var homeForm = new Home.HomeForm(username, role);
                    homeForm.Show();
                }
            }
        }

        private void lnkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            var resetForm = new ResetPasswordForm();
            resetForm.Show();
        }

        private void backToHome_Click(object sender, EventArgs e)
        {
            this.Hide();
            var homeForm = new Home.HomeForm(null, null);
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

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
