using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class ChangePasswordDialog : Form
	{
		private string userID = "";

		private IContainer components;

		private MMTextBox textBoxPassword;

		private XPButton buttonOK;

		private MMLabel mmLabel7;

		private MMLabel mmLabel2;

		private MMTextBox textBoxConfirmPass;

		public string UserID
		{
			get
			{
				return userID;
			}
			set
			{
				userID = value;
			}
		}

		public ChangePasswordDialog()
		{
			InitializeComponent();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (textBoxPassword.Text != textBoxConfirmPass.Text)
			{
				ErrorHelper.WarningMessage("Password does not match. Please enter again.");
				textBoxConfirmPass.Clear();
				textBoxPassword.Clear();
				textBoxPassword.Focus();
				base.DialogResult = DialogResult.None;
			}
			else
			{
				try
				{
					if (Factory.UserSystem.ChangePassword(userID, textBoxPassword.Text, string.Empty))
					{
						ErrorHelper.InformationMessage("Password changed successfully.");
						Close();
					}
				}
				catch (Exception ex)
				{
					ErrorHelper.ErrorMessage(ex.Message);
					base.DialogResult = DialogResult.None;
				}
			}
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
			textBoxPassword = new Micromind.UISupport.MMTextBox();
			buttonOK = new Micromind.UISupport.XPButton();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxConfirmPass = new Micromind.UISupport.MMTextBox();
			SuspendLayout();
			textBoxPassword.BackColor = System.Drawing.Color.White;
			textBoxPassword.IsComboTextBox = false;
			textBoxPassword.Location = new System.Drawing.Point(112, 15);
			textBoxPassword.MaxLength = 64;
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.Size = new System.Drawing.Size(171, 20);
			textBoxPassword.TabIndex = 0;
			textBoxPassword.UseSystemPasswordChar = true;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(191, 83);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 2;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(12, 40);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(94, 13);
			mmLabel7.TabIndex = 19;
			mmLabel7.Text = "Confirm Password:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(12, 18);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(81, 13);
			mmLabel2.TabIndex = 20;
			mmLabel2.Text = "New Password:";
			textBoxConfirmPass.BackColor = System.Drawing.Color.White;
			textBoxConfirmPass.IsComboTextBox = false;
			textBoxConfirmPass.Location = new System.Drawing.Point(112, 37);
			textBoxConfirmPass.MaxLength = 64;
			textBoxConfirmPass.Name = "textBoxConfirmPass";
			textBoxConfirmPass.Size = new System.Drawing.Size(171, 20);
			textBoxConfirmPass.TabIndex = 1;
			textBoxConfirmPass.UseSystemPasswordChar = true;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(299, 119);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(textBoxConfirmPass);
			base.Controls.Add(textBoxPassword);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChangePasswordDialog";
			Text = "Change Password";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
