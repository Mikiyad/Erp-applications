using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Utilities
{
	public class EmailServerOptionsDialog : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		private Label lblHost;

		private Label lblPassword;

		private XPButton buttonCancel;

		private XPButton xpButtonOK;

		private MMTextBox textBoxPassword;

		private Label lblUsername;

		private ToolTip toolTip1;

		private Line line1;

		private MMTextBox textBoxHost;

		private MMTextBox textBoxUserID;

		private CheckBox checkBoxSavePassword;

		private Label label1;

		private MMTextBox textBoxPort;

		private IContainer components;

		public string Host
		{
			get
			{
				return textBoxHost.Text.Trim();
			}
			set
			{
				textBoxHost.Text = value;
			}
		}

		public int Port
		{
			get
			{
				return int.Parse(textBoxPort.Text.Trim());
			}
			set
			{
				textBoxPort.Text = value.ToString();
			}
		}

		public string UserID
		{
			get
			{
				return textBoxUserID.Text.Trim();
			}
			set
			{
				textBoxUserID.Text = value;
			}
		}

		public string Password
		{
			get
			{
				return textBoxPassword.Text.Trim();
			}
			set
			{
				textBoxPassword.Text = value.Trim();
				if (value.Trim() == string.Empty)
				{
					textBoxPassword.Text = "";
				}
			}
		}

		public EmailServerOptionsDialog()
		{
			InitializeComponent();
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
			components = new System.ComponentModel.Container();
			lblHost = new System.Windows.Forms.Label();
			lblPassword = new System.Windows.Forms.Label();
			buttonCancel = new Micromind.UISupport.XPButton();
			xpButtonOK = new Micromind.UISupport.XPButton();
			textBoxHost = new Micromind.UISupport.MMTextBox();
			textBoxUserID = new Micromind.UISupport.MMTextBox();
			textBoxPassword = new Micromind.UISupport.MMTextBox();
			lblUsername = new System.Windows.Forms.Label();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			line1 = new Micromind.UISupport.Line();
			checkBoxSavePassword = new System.Windows.Forms.CheckBox();
			label1 = new System.Windows.Forms.Label();
			textBoxPort = new Micromind.UISupport.MMTextBox();
			SuspendLayout();
			lblHost.Location = new System.Drawing.Point(8, 7);
			lblHost.Name = "lblHost";
			lblHost.Size = new System.Drawing.Size(120, 15);
			lblHost.TabIndex = 0;
			lblHost.Text = "Email Server Address:";
			lblPassword.Location = new System.Drawing.Point(8, 59);
			lblPassword.Name = "lblPassword";
			lblPassword.Size = new System.Drawing.Size(64, 15);
			lblPassword.TabIndex = 6;
			lblPassword.Text = "Password:";
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(392, 133);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(56, 22);
			buttonCancel.TabIndex = 10;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			xpButtonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButtonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButtonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButtonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButtonOK.Location = new System.Drawing.Point(328, 133);
			xpButtonOK.Name = "xpButtonOK";
			xpButtonOK.Size = new System.Drawing.Size(56, 22);
			xpButtonOK.TabIndex = 9;
			xpButtonOK.Text = "&OK";
			xpButtonOK.Click += new System.EventHandler(xpButtonOK_Click);
			textBoxHost.AcceptsReturn = false;
			textBoxHost.AcceptsTab = false;
			textBoxHost.AutoSize = true;
			textBoxHost.BackColor = System.Drawing.Color.White;
			textBoxHost.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxHost.HideSelection = true;
			textBoxHost.IsComboTextBox = false;
			textBoxHost.Lines = new string[1]
			{
				"mail."
			};
			textBoxHost.Location = new System.Drawing.Point(128, 7);
			textBoxHost.MaxLength = 64;
			textBoxHost.Multiline = false;
			textBoxHost.Name = "textBoxHost";
			textBoxHost.PasswordChar = '\0';
			textBoxHost.ReadOnly = false;
			textBoxHost.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxHost.Size = new System.Drawing.Size(207, 20);
			textBoxHost.TabIndex = 1;
			textBoxHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			toolTip1.SetToolTip(textBoxHost, "For example: mail.yourdomainname.com");
			textBoxHost.WordWrap = true;
			textBoxUserID.AcceptsReturn = false;
			textBoxUserID.AcceptsTab = false;
			textBoxUserID.AutoSize = true;
			textBoxUserID.BackColor = System.Drawing.Color.White;
			textBoxUserID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxUserID.HideSelection = true;
			textBoxUserID.IsComboTextBox = false;
			textBoxUserID.Lines = new string[0];
			textBoxUserID.Location = new System.Drawing.Point(128, 33);
			textBoxUserID.MaxLength = 64;
			textBoxUserID.Multiline = false;
			textBoxUserID.Name = "textBoxUserID";
			textBoxUserID.PasswordChar = '\0';
			textBoxUserID.ReadOnly = false;
			textBoxUserID.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxUserID.Size = new System.Drawing.Size(320, 20);
			textBoxUserID.TabIndex = 5;
			textBoxUserID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			toolTip1.SetToolTip(textBoxUserID, "FTP server user ID");
			textBoxUserID.WordWrap = true;
			textBoxPassword.AcceptsReturn = false;
			textBoxPassword.AcceptsTab = false;
			textBoxPassword.AutoSize = true;
			textBoxPassword.BackColor = System.Drawing.Color.White;
			textBoxPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxPassword.HideSelection = true;
			textBoxPassword.IsComboTextBox = false;
			textBoxPassword.Lines = new string[0];
			textBoxPassword.Location = new System.Drawing.Point(128, 59);
			textBoxPassword.MaxLength = 64;
			textBoxPassword.Multiline = false;
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.PasswordChar = '\0';
			textBoxPassword.ReadOnly = false;
			textBoxPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxPassword.Size = new System.Drawing.Size(320, 20);
			textBoxPassword.TabIndex = 7;
			textBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			toolTip1.SetToolTip(textBoxPassword, "FTP server password");
			textBoxPassword.WordWrap = true;
			lblUsername.Location = new System.Drawing.Point(8, 33);
			lblUsername.Name = "lblUsername";
			lblUsername.Size = new System.Drawing.Size(88, 14);
			lblUsername.TabIndex = 4;
			lblUsername.Text = "User ID:";
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(8, 126);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(440, 1);
			line1.TabIndex = 16;
			line1.TabStop = false;
			checkBoxSavePassword.Location = new System.Drawing.Point(8, 90);
			checkBoxSavePassword.Name = "checkBoxSavePassword";
			checkBoxSavePassword.Size = new System.Drawing.Size(160, 30);
			checkBoxSavePassword.TabIndex = 8;
			checkBoxSavePassword.Text = "Save Password";
			checkBoxSavePassword.CheckedChanged += new System.EventHandler(checkBoxSavePassword_CheckedChanged);
			label1.Location = new System.Drawing.Point(341, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(34, 13);
			label1.TabIndex = 2;
			label1.Text = "Port:";
			textBoxPort.AcceptsReturn = false;
			textBoxPort.AcceptsTab = false;
			textBoxPort.AutoSize = true;
			textBoxPort.BackColor = System.Drawing.Color.White;
			textBoxPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxPort.HideSelection = true;
			textBoxPort.IsComboTextBox = false;
			textBoxPort.Lines = new string[1]
			{
				"25"
			};
			textBoxPort.Location = new System.Drawing.Point(381, 7);
			textBoxPort.MaxLength = 4;
			textBoxPort.Multiline = false;
			textBoxPort.Name = "textBoxPort";
			textBoxPort.PasswordChar = '\0';
			textBoxPort.ReadOnly = false;
			textBoxPort.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxPort.Size = new System.Drawing.Size(67, 20);
			textBoxPort.TabIndex = 3;
			textBoxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			toolTip1.SetToolTip(textBoxPort, "For example: mail.yourdomainname.com");
			textBoxPort.WordWrap = true;
			base.AcceptButton = xpButtonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(456, 164);
			base.ControlBox = false;
			base.Controls.Add(textBoxPort);
			base.Controls.Add(label1);
			base.Controls.Add(checkBoxSavePassword);
			base.Controls.Add(line1);
			base.Controls.Add(textBoxPassword);
			base.Controls.Add(textBoxUserID);
			base.Controls.Add(textBoxHost);
			base.Controls.Add(xpButtonOK);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(lblPassword);
			base.Controls.Add(lblUsername);
			base.Controls.Add(lblHost);
			base.Name = "EmailServerOptionsDialog";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			Text = "Email Server Settings";
			base.Load += new System.EventHandler(FTPConnProps_Load);
			ResumeLayout(false);
			PerformLayout();
		}

		private void GetUserPrefs()
		{
			try
			{
				Host = Global.CompanySettings.GetSetting(base.Name + textBoxHost.Name, textBoxHost.Text).ToString();
				try
				{
					Port = int.Parse(Global.CompanySettings.GetSetting(base.Name + textBoxPort.Name, textBoxPort.Text).ToString());
				}
				catch
				{
				}
				UserID = Global.CompanySettings.GetSetting(base.Name + textBoxUserID.Name, textBoxUserID.Text).ToString();
				checkBoxSavePassword.Checked = bool.Parse(Global.CompanySettings.GetSetting(base.Name + checkBoxSavePassword.Name, false).ToString());
				if (checkBoxSavePassword.Checked)
				{
					Password = Global.GetPassword("emailserver");
				}
			}
			catch
			{
			}
		}

		private void SaveUserPrefs()
		{
			try
			{
				Global.CompanySettings.SaveSetting(base.Name + textBoxHost.Name, textBoxHost.Text);
				Global.CompanySettings.SaveSetting(base.Name + textBoxPort.Name, textBoxPort.Text);
				Global.CompanySettings.SaveSetting(base.Name + textBoxUserID.Name, textBoxUserID.Text);
				if (checkBoxSavePassword.Checked)
				{
					Global.SavePassword(textBoxPassword.Text, "emailserver");
				}
				else
				{
					Global.SavePassword("", "emailserver");
				}
				Global.CompanySettings.SaveSetting(base.Name + checkBoxSavePassword.Name, checkBoxSavePassword.Checked);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadSettings()
		{
			try
			{
				Global.CompanySettings.LoadFormProperties(this);
			}
			catch
			{
			}
		}

		private void SaveSettings()
		{
			Global.CompanySettings.LoadFormProperties(this);
		}

		private void FTPConnProps_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					textBoxPassword.PasswordChar = '●';
				}
				else
				{
					textBoxPassword.PasswordChar = '*';
				}
				GetUserPrefs();
				textBoxHost.Select(textBoxHost.Text.Length, 1);
				LoadSettings();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void xpButtonOK_Click(object sender, EventArgs e)
		{
			if (Validate())
			{
				SaveUserPrefs();
				base.DialogResult = DialogResult.OK;
				SaveSettings();
				Close();
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private new bool Validate()
		{
			if (textBoxHost.Text.Trim() == string.Empty)
			{
				ErrorHelper.WarningMessage("Please type email server address.  For example: mail.yourdomainname.com");
				textBoxHost.Focus();
				return false;
			}
			if (textBoxPort.Text.Trim() != string.Empty)
			{
				try
				{
					int.Parse(textBoxPort.Text);
				}
				catch
				{
					ErrorHelper.WarningMessage("Please type a numeric port number.");
					textBoxPort.SelectAll();
					textBoxPort.Focus();
					return false;
				}
			}
			else
			{
				textBoxPort.Text = "25";
			}
			return true;
		}

		public void LoadServerSettings()
		{
			GetUserPrefs();
		}

		private void checkBoxSavePassword_CheckedChanged(object sender, EventArgs e)
		{
		}
	}
}
