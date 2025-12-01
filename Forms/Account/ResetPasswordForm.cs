using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using AppointmentBookingSystemWFA.Database;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace AppointmentBookingSystemWFA.Forms.Account
{
    public partial class ResetPasswordForm : Form
    {
        private string resetUser;
        private string resetOtp;

        public ResetPasswordForm()
        {
            InitializeComponent();
            ShowStep(1);
        }

        private void ResetPasswordForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;   // hide error label initially
        }

        private void ShowStep(int step)
        {
            panelStep1.Visible = (step == 1);
            panelStep2.Visible = (step == 2);
            panelStep3.Visible = (step == 3);
            panelStep4.Visible = (step == 4);
        }

        private void btnSendOtp_Click(object sender, EventArgs e)
        {
            string identifier = txtIdentifier.Text.Trim();
            string username = null;
            string email = null;

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmdUser = new MySqlCommand("SELECT Email FROM users WHERE Username=@identifier", conn);
                cmdUser.Parameters.AddWithValue("@identifier", identifier);
                var resultUser = cmdUser.ExecuteScalar();

                if (resultUser != null)
                {
                    username = identifier;
                    email = resultUser.ToString();
                }
                else
                {
                    var cmdEmail = new MySqlCommand("SELECT Username FROM users WHERE Email=@identifier", conn);
                    cmdEmail.Parameters.AddWithValue("@identifier", identifier);
                    var resultEmail = cmdEmail.ExecuteScalar();

                    if (resultEmail != null)
                    {
                        username = resultEmail.ToString();
                        email = identifier;
                    }
                }
            }

            if (username != null && email != null)
            {
                var rng = new Random();
                resetOtp = rng.Next(100000, 999999).ToString();
                resetUser = username;

                SendEmail(email, "Password Reset OTP", $"Your OTP code is: {resetOtp}");

                lblError.Visible = false;
                ShowStep(2);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "No matching user found.";
            }
        }

        private void btnVerifyOtp_Click(object sender, EventArgs e)
        {
            if (txtOtp.Text.Trim() == resetOtp)
            {
                lblError.Visible = false;
                ShowStep(3);
            }
            else
            {
                lblError.Visible = true;
                lblMessage.Visible = false;
                lblError.Text = "Invalid OTP.";
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 3)
            {
                lblError.Visible = true;
                lblError.Text = "Password must be at least 3 characters.";
                return;
            }

            string hashed = HashPassword(newPassword);

            using (var conn = DatabaseHelper.GetConnection())
            {
                var cmd = new MySqlCommand("UPDATE users SET Password=@p WHERE Username=@u", conn);
                cmd.Parameters.AddWithValue("@p", hashed);
                cmd.Parameters.AddWithValue("@u", resetUser);
                cmd.ExecuteNonQuery();
            }

            resetOtp = null;
            resetUser = null;

            lblError.Visible = false;
            ShowStep(4);
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginForm = new LoginForm();
            loginForm.Show();
        }

        private void backToLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginForm = new LoginForm();
            loginForm.Show();
        }

        private void backToLogin_MouseEnter(object sender, EventArgs e)
        {
            backToLogin.ForeColor = Color.Blue;
            backToLogin.Cursor = Cursors.Hand;
        }

        private void backToLogin_MouseLeave(object sender, EventArgs e)
        {
            backToLogin.ForeColor = Color.Black;
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

        private void SendEmail(string toEmail, string subject, string body)
        {
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("your-email", "your-password"),
                EnableSsl = true
            };

            var message = new MailMessage("your-email", toEmail, subject, body);
            smtp.Send(message);
        }
    }
}
