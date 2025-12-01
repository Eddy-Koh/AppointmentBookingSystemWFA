using System;
using System.Linq;
using System.Windows.Forms;
using AppointmentBookingSystemWFA.Database;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace AppointmentBookingSystemWFA.Forms.Account
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;   // hide error label initially
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string mobile = txtMobilePhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString();

            // Validation
            if (string.IsNullOrEmpty(username))
            {
                lblError.Visible = true;
                lblError.Text = "Username is required.";
                return;
            }
            if (string.IsNullOrEmpty(email))
            {
                lblError.Visible = true;
                lblError.Text = "Email is required.";
                return;
            }
            if (string.IsNullOrEmpty(mobile) || !(mobile.Length == 10 || mobile.Length == 11) || !mobile.All(char.IsDigit))
            {
                lblError.Visible = true;
                lblError.Text = "Mobile number must be 10 or 11 digits.";
                return;
            }
            if (string.IsNullOrEmpty(password) || password.Length < 3)
            {
                lblError.Visible = true;
                lblError.Text = "Password must be at least 3 characters.";
                return;
            }
            if (string.IsNullOrEmpty(role))
            {
                lblError.Visible = true;
                lblError.Text = "Role is required.";
                return;
            }

            // Check if username/email already exists
            using (var conn = DatabaseHelper.GetConnection())
            {
                var checkUsernameCmd = new MySqlCommand("SELECT COUNT(*) FROM users WHERE Username=@u", conn);
                checkUsernameCmd.Parameters.AddWithValue("@u", username);
                int usernameCount = Convert.ToInt32(checkUsernameCmd.ExecuteScalar());
                if (usernameCount > 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Username already exists.";
                    return;
                }

                var checkEmailCmd = new MySqlCommand("SELECT COUNT(*) FROM users WHERE Email=@e", conn);
                checkEmailCmd.Parameters.AddWithValue("@e", email);
                int emailCount = Convert.ToInt32(checkEmailCmd.ExecuteScalar());
                if (emailCount > 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Email already registered.";
                    return;
                }
            }

            // Approval status logic, first approver will direct accept, others pending
            string approvalStatus = "Accepted";
            if (role == "Approver")
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    var checkApproverCmd = new MySqlCommand("SELECT COUNT(*) FROM users WHERE Role='Approver'", conn);
                    int approverCount = Convert.ToInt32(checkApproverCmd.ExecuteScalar());
                    approvalStatus = approverCount == 0 ? "Accepted" : "Pending";
                }
            }

            string hashedPassword = HashPassword(password);

            // Save new user
            using (var conn = DatabaseHelper.GetConnection())
            {
                var insertCmd = new MySqlCommand(@"INSERT INTO users 
                    (Username, MobilePhone, Email, Password, Role, ApprovalStatus) 
                    VALUES (@Username, @MobilePhone, @Email, @Password, @Role, @ApprovalStatus)", conn);

                insertCmd.Parameters.AddWithValue("@Username", username);
                insertCmd.Parameters.AddWithValue("@MobilePhone", mobile);
                insertCmd.Parameters.AddWithValue("@Email", email);
                insertCmd.Parameters.AddWithValue("@Password", hashedPassword);
                insertCmd.Parameters.AddWithValue("@Role", role);
                insertCmd.Parameters.AddWithValue("@ApprovalStatus", approvalStatus);

                insertCmd.ExecuteNonQuery();
            }

            lblError.Visible = false;
            MessageBox.Show("Registration successful !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            var loginForm = new LoginForm();
            loginForm.Show();
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
