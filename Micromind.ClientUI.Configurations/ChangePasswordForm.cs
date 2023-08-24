using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class ChangePasswordForm : Form
	{
		private string userId = Global.CurrentUser;

		private IContainer components;

		private MMTextBox textBoxNewPassword;

		private XPButton buttonOK;

		private MMLabel mmLabel7;

		private MMLabel mmLabel2;

		private MMTextBox textBoxRetypePassword;

		private MMLabel mmLabel1;

		private MMTextBox textBoxCurrentPassword;

		private MMLabel lbelUserInfo;

		private TextBox textBoxUserId;

		public string UserId
		{
			get
			{
				return userId;
			}
			set
			{
				userId = value;
			}
		}

		public ChangePasswordForm()
		{
			InitializeComponent();
			textBoxUserId.Text = UserId;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (ValidateData())
			{
				try
				{
					if (Factory.UserSystem.ChangePassword(UserId, textBoxNewPassword.Text.Trim(), textBoxCurrentPassword.Text.Trim()))
					{
						ErrorHelper.InformationMessage("Password changed successfully! You must log out from current session and login again");
						Factory.SetLoginInfo(base.ProductName, Global.CurrentDatabaseName, textBoxUserId.Text, Factory.Encrypt(textBoxNewPassword.Text));
						Close();
					}
				}
				catch (SqlException ex)
				{
					if (ex.Number == 15151)
					{
						ErrorHelper.WarningMessage("Cannot change the password. The old password you have entered is incorrect.");
					}
					else if (ex.Number == 15115)
					{
						ErrorHelper.WarningMessage("This password cannot be accepted at this time. Please enter another password.");
					}
					else
					{
						ErrorHelper.ErrorMessage(ex.Message);
					}
				}
				catch (Exception ex2)
				{
					ErrorHelper.ErrorMessage(ex2.Message);
					base.DialogResult = DialogResult.None;
				}
			}
		}

		private bool ValidateData()
		{
			if (textBoxCurrentPassword.Text.Trim().Length == 0 || textBoxNewPassword.Text.Trim().Length == 0 || textBoxRetypePassword.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (textBoxNewPassword.Text != textBoxRetypePassword.Text)
			{
				ErrorHelper.WarningMessage("Password does not match. Please try again.");
				textBoxRetypePassword.Clear();
				textBoxNewPassword.Clear();
				textBoxNewPassword.Focus();
				base.DialogResult = DialogResult.None;
				return false;
			}
			return true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			textBoxNewPassword = new Micromind.UISupport.MMTextBox();
			buttonOK = new Micromind.UISupport.XPButton();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxRetypePassword = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxCurrentPassword = new Micromind.UISupport.MMTextBox();
			lbelUserInfo = new Micromind.UISupport.MMLabel();
			textBoxUserId = new System.Windows.Forms.TextBox();
			SuspendLayout();
			textBoxNewPassword.BackColor = System.Drawing.Color.White;
			textBoxNewPassword.CustomReportFieldName = "";
			textBoxNewPassword.CustomReportKey = "";
			textBoxNewPassword.CustomReportValueType = 1;
			textBoxNewPassword.IsComboTextBox = false;
			textBoxNewPassword.Location = new System.Drawing.Point(132, 69);
			textBoxNewPassword.MaxLength = 64;
			textBoxNewPassword.Name = "textBoxNewPassword";
			textBoxNewPassword.Size = new System.Drawing.Size(195, 20);
			textBoxNewPassword.TabIndex = 1;
			textBoxNewPassword.UseSystemPasswordChar = true;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(239, 118);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(88, 24);
			buttonOK.TabIndex = 3;
			buttonOK.Text = "Chan&ge";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(12, 94);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(110, 15);
			mmLabel7.TabIndex = 19;
			mmLabel7.Text = "Retype Password:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(12, 72);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(96, 15);
			mmLabel2.TabIndex = 20;
			mmLabel2.Text = "New Password:";
			textBoxRetypePassword.BackColor = System.Drawing.Color.White;
			textBoxRetypePassword.CustomReportFieldName = "";
			textBoxRetypePassword.CustomReportKey = "";
			textBoxRetypePassword.CustomReportValueType = 1;
			textBoxRetypePassword.IsComboTextBox = false;
			textBoxRetypePassword.Location = new System.Drawing.Point(132, 91);
			textBoxRetypePassword.MaxLength = 64;
			textBoxRetypePassword.Name = "textBoxRetypePassword";
			textBoxRetypePassword.Size = new System.Drawing.Size(195, 20);
			textBoxRetypePassword.TabIndex = 2;
			textBoxRetypePassword.UseSystemPasswordChar = true;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(12, 47);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(114, 15);
			mmLabel1.TabIndex = 22;
			mmLabel1.Text = "Current Password:";
			textBoxCurrentPassword.BackColor = System.Drawing.Color.White;
			textBoxCurrentPassword.CustomReportFieldName = "";
			textBoxCurrentPassword.CustomReportKey = "";
			textBoxCurrentPassword.CustomReportValueType = 1;
			textBoxCurrentPassword.IsComboTextBox = false;
			textBoxCurrentPassword.Location = new System.Drawing.Point(132, 44);
			textBoxCurrentPassword.MaxLength = 64;
			textBoxCurrentPassword.Name = "textBoxCurrentPassword";
			textBoxCurrentPassword.Size = new System.Drawing.Size(195, 20);
			textBoxCurrentPassword.TabIndex = 0;
			textBoxCurrentPassword.UseSystemPasswordChar = true;
			lbelUserInfo.AutoSize = true;
			lbelUserInfo.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lbelUserInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			lbelUserInfo.IsFieldHeader = false;
			lbelUserInfo.IsRequired = false;
			lbelUserInfo.Location = new System.Drawing.Point(12, 13);
			lbelUserInfo.Name = "lbelUserInfo";
			lbelUserInfo.PenWidth = 1f;
			lbelUserInfo.ShowBorder = false;
			lbelUserInfo.Size = new System.Drawing.Size(83, 13);
			lbelUserInfo.TabIndex = 23;
			lbelUserInfo.Text = "Logged In User:";
			textBoxUserId.Location = new System.Drawing.Point(132, 11);
			textBoxUserId.Name = "textBoxUserId";
			textBoxUserId.ReadOnly = true;
			textBoxUserId.Size = new System.Drawing.Size(118, 20);
			textBoxUserId.TabIndex = 24;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(338, 150);
			base.Controls.Add(textBoxUserId);
			base.Controls.Add(lbelUserInfo);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(textBoxCurrentPassword);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(textBoxRetypePassword);
			base.Controls.Add(textBoxNewPassword);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChangePasswordForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Change Password";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
