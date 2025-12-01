
namespace AppointmentBookingSystemWFA.Forms.Account
{
    partial class ResetPasswordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelStep1 = new System.Windows.Forms.Panel();
            this.btnSendOtp = new System.Windows.Forms.Button();
            this.txtIdentifier = new System.Windows.Forms.TextBox();
            this.lblStep1Message = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.panelStep2 = new System.Windows.Forms.Panel();
            this.btnVerifyOtp = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtOtp = new System.Windows.Forms.TextBox();
            this.lblStep2Message = new System.Windows.Forms.Label();
            this.panelStep3 = new System.Windows.Forms.Panel();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblStep3Message = new System.Windows.Forms.Label();
            this.panelStep4 = new System.Windows.Forms.Panel();
            this.btnBackToLogin = new System.Windows.Forms.Button();
            this.lblSuccess = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.backToLogin = new System.Windows.Forms.Label();
            this.panelStep1.SuspendLayout();
            this.panelStep2.SuspendLayout();
            this.panelStep3.SuspendLayout();
            this.panelStep4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelStep1
            // 
            this.panelStep1.Controls.Add(this.btnSendOtp);
            this.panelStep1.Controls.Add(this.txtIdentifier);
            this.panelStep1.Controls.Add(this.lblStep1Message);
            this.panelStep1.Location = new System.Drawing.Point(0, 124);
            this.panelStep1.Name = "panelStep1";
            this.panelStep1.Size = new System.Drawing.Size(800, 285);
            this.panelStep1.TabIndex = 0;
            // 
            // btnSendOtp
            // 
            this.btnSendOtp.AutoSize = true;
            this.btnSendOtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendOtp.Location = new System.Drawing.Point(323, 155);
            this.btnSendOtp.Name = "btnSendOtp";
            this.btnSendOtp.Size = new System.Drawing.Size(154, 28);
            this.btnSendOtp.TabIndex = 2;
            this.btnSendOtp.Text = "Send OTP";
            this.btnSendOtp.UseVisualStyleBackColor = true;
            this.btnSendOtp.Click += new System.EventHandler(this.btnSendOtp_Click);
            // 
            // txtIdentifier
            // 
            this.txtIdentifier.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdentifier.Location = new System.Drawing.Point(273, 95);
            this.txtIdentifier.Name = "txtIdentifier";
            this.txtIdentifier.Size = new System.Drawing.Size(256, 27);
            this.txtIdentifier.TabIndex = 1;
            this.txtIdentifier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblStep1Message
            // 
            this.lblStep1Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep1Message.Location = new System.Drawing.Point(0, 48);
            this.lblStep1Message.Name = "lblStep1Message";
            this.lblStep1Message.Size = new System.Drawing.Size(800, 20);
            this.lblStep1Message.TabIndex = 0;
            this.lblStep1Message.Text = "Enter Username / Email";
            this.lblStep1Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblError
            // 
            this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(0, 255);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(800, 18);
            this.lblError.TabIndex = 6;
            this.lblError.Text = "Error Label";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelStep2
            // 
            this.panelStep2.Controls.Add(this.btnVerifyOtp);
            this.panelStep2.Controls.Add(this.lblMessage);
            this.panelStep2.Controls.Add(this.txtOtp);
            this.panelStep2.Controls.Add(this.lblStep2Message);
            this.panelStep2.Location = new System.Drawing.Point(0, 124);
            this.panelStep2.Name = "panelStep2";
            this.panelStep2.Size = new System.Drawing.Size(800, 285);
            this.panelStep2.TabIndex = 3;
            // 
            // btnVerifyOtp
            // 
            this.btnVerifyOtp.AutoSize = true;
            this.btnVerifyOtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerifyOtp.Location = new System.Drawing.Point(323, 155);
            this.btnVerifyOtp.Name = "btnVerifyOtp";
            this.btnVerifyOtp.Size = new System.Drawing.Size(154, 28);
            this.btnVerifyOtp.TabIndex = 2;
            this.btnVerifyOtp.Text = "Verify OTP";
            this.btnVerifyOtp.UseVisualStyleBackColor = true;
            this.btnVerifyOtp.Click += new System.EventHandler(this.btnVerifyOtp_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblMessage.Location = new System.Drawing.Point(0, 128);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(800, 18);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.Text = "OTP has been sent to your email.";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtOtp
            // 
            this.txtOtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtp.Location = new System.Drawing.Point(353, 95);
            this.txtOtp.Name = "txtOtp";
            this.txtOtp.Size = new System.Drawing.Size(94, 27);
            this.txtOtp.TabIndex = 1;
            this.txtOtp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblStep2Message
            // 
            this.lblStep2Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep2Message.Location = new System.Drawing.Point(0, 48);
            this.lblStep2Message.Name = "lblStep2Message";
            this.lblStep2Message.Size = new System.Drawing.Size(800, 20);
            this.lblStep2Message.TabIndex = 0;
            this.lblStep2Message.Text = "Enter OTP";
            this.lblStep2Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelStep3
            // 
            this.panelStep3.Controls.Add(this.btnResetPassword);
            this.panelStep3.Controls.Add(this.txtNewPassword);
            this.panelStep3.Controls.Add(this.lblStep3Message);
            this.panelStep3.Location = new System.Drawing.Point(0, 124);
            this.panelStep3.Name = "panelStep3";
            this.panelStep3.Size = new System.Drawing.Size(800, 285);
            this.panelStep3.TabIndex = 4;
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.AutoSize = true;
            this.btnResetPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetPassword.Location = new System.Drawing.Point(322, 155);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(154, 28);
            this.btnResetPassword.TabIndex = 2;
            this.btnResetPassword.Text = "Reset";
            this.btnResetPassword.UseVisualStyleBackColor = true;
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPassword.Location = new System.Drawing.Point(318, 95);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(164, 27);
            this.txtNewPassword.TabIndex = 1;
            this.txtNewPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblStep3Message
            // 
            this.lblStep3Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep3Message.Location = new System.Drawing.Point(0, 48);
            this.lblStep3Message.Name = "lblStep3Message";
            this.lblStep3Message.Size = new System.Drawing.Size(800, 20);
            this.lblStep3Message.TabIndex = 0;
            this.lblStep3Message.Text = "Set New Password";
            this.lblStep3Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelStep4
            // 
            this.panelStep4.Controls.Add(this.btnBackToLogin);
            this.panelStep4.Controls.Add(this.lblSuccess);
            this.panelStep4.Location = new System.Drawing.Point(0, 124);
            this.panelStep4.Name = "panelStep4";
            this.panelStep4.Size = new System.Drawing.Size(800, 285);
            this.panelStep4.TabIndex = 5;
            // 
            // btnBackToLogin
            // 
            this.btnBackToLogin.AutoSize = true;
            this.btnBackToLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackToLogin.Location = new System.Drawing.Point(318, 123);
            this.btnBackToLogin.Name = "btnBackToLogin";
            this.btnBackToLogin.Size = new System.Drawing.Size(154, 28);
            this.btnBackToLogin.TabIndex = 2;
            this.btnBackToLogin.Text = "Back to Login";
            this.btnBackToLogin.UseVisualStyleBackColor = true;
            this.btnBackToLogin.Click += new System.EventHandler(this.btnBackToLogin_Click);
            // 
            // lblSuccess
            // 
            this.lblSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuccess.Location = new System.Drawing.Point(0, 70);
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(800, 25);
            this.lblSuccess.TabIndex = 0;
            this.lblSuccess.Text = "Password reset successful !";
            this.lblSuccess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 51);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(800, 36);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Reset Password";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backToLogin
            // 
            this.backToLogin.AutoSize = true;
            this.backToLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backToLogin.Location = new System.Drawing.Point(24, 19);
            this.backToLogin.Name = "backToLogin";
            this.backToLogin.Size = new System.Drawing.Size(104, 17);
            this.backToLogin.TabIndex = 9;
            this.backToLogin.Text = "< Login Page";
            this.backToLogin.Click += new System.EventHandler(this.backToLogin_Click);
            this.backToLogin.MouseEnter += new System.EventHandler(this.backToLogin_MouseEnter);
            this.backToLogin.MouseLeave += new System.EventHandler(this.backToLogin_MouseLeave);
            // 
            // ResetPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.backToLogin);
            this.Controls.Add(this.panelStep1);
            this.Controls.Add(this.panelStep2);
            this.Controls.Add(this.panelStep3);
            this.Controls.Add(this.panelStep4);
            this.Controls.Add(this.lblTitle);
            this.Name = "ResetPasswordForm";
            this.Text = "ResetPasswordForm";
            this.Load += new System.EventHandler(this.ResetPasswordForm_Load);
            this.panelStep1.ResumeLayout(false);
            this.panelStep1.PerformLayout();
            this.panelStep2.ResumeLayout(false);
            this.panelStep2.PerformLayout();
            this.panelStep3.ResumeLayout(false);
            this.panelStep3.PerformLayout();
            this.panelStep4.ResumeLayout(false);
            this.panelStep4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelStep1;
        private System.Windows.Forms.Button btnSendOtp;
        private System.Windows.Forms.TextBox txtIdentifier;
        private System.Windows.Forms.Label lblStep1Message;
        private System.Windows.Forms.Panel panelStep2;
        private System.Windows.Forms.Button btnVerifyOtp;
        private System.Windows.Forms.TextBox txtOtp;
        private System.Windows.Forms.Label lblStep2Message;
        private System.Windows.Forms.Panel panelStep3;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblStep3Message;
        private System.Windows.Forms.Panel panelStep4;
        private System.Windows.Forms.Button btnBackToLogin;
        private System.Windows.Forms.Label lblSuccess;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label backToLogin;
    }
}