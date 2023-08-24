using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Utilities
{
	public class FTPConnProps : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		private Label lblHost;

		private Label lblPort;

		private Label lblPassword;

		private XPButton buttonCancel;

		private XPButton xpButtonOK;

		private MMTextBox textBoxPassword;

		private Label lblUsername;

		private Label label1;

		private ToolTip toolTip1;

		private Line line1;

		private MMTextBox textBoxHost;

		private MMTextBox textBoxPort;

		private MMTextBox textBoxUserID;

		private MMTextBox textBoxDirectory;

		private MMTextBox textBoxFileName;

		private Label label2;

		private CheckBox checkBoxSavePassword;

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
				try
				{
					return Factory.Decrypt(textBoxPassword.Text);
				}
				catch
				{
					return "";
				}
			}
			set
			{
				try
				{
					textBoxPassword.Text = Factory.Encrypt(value);
					if (value.Trim() == string.Empty)
					{
						textBoxPassword.Text = "";
					}
				}
				catch
				{
				}
			}
		}

		public string Directory
		{
			get
			{
				return textBoxDirectory.Text.Trim();
			}
			set
			{
				textBoxDirectory.Text = value;
			}
		}

		public string FileName
		{
			get
			{
				return textBoxFileName.Text.Trim();
			}
			set
			{
				textBoxFileName.Text = value;
			}
		}

		public FTPConnProps()
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
			lblPort = new System.Windows.Forms.Label();
			lblPassword = new System.Windows.Forms.Label();
			buttonCancel = new Micromind.UISupport.XPButton();
			xpButtonOK = new Micromind.UISupport.XPButton();
			textBoxHost = new Micromind.UISupport.MMTextBox();
			textBoxPort = new Micromind.UISupport.MMTextBox();
			textBoxUserID = new Micromind.UISupport.MMTextBox();
			textBoxPassword = new Micromind.UISupport.MMTextBox();
			lblUsername = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			textBoxDirectory = new Micromind.UISupport.MMTextBox();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			textBoxFileName = new Micromind.UISupport.MMTextBox();
			line1 = new Micromind.UISupport.Line();
			label2 = new System.Windows.Forms.Label();
			checkBoxSavePassword = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			lblHost.Location = new System.Drawing.Point(8, 7);
			lblHost.Name = "lblHost";
			lblHost.Size = new System.Drawing.Size(104, 15);
			lblHost.TabIndex = 0;
			lblHost.Text = "Host or IP Address:";
			lblPort.Location = new System.Drawing.Point(368, 10);
			lblPort.Name = "lblPort";
			lblPort.Size = new System.Drawing.Size(32, 15);
			lblPort.TabIndex = 2;
			lblPort.Text = "Port:";
			lblPassword.Location = new System.Drawing.Point(256, 33);
			lblPassword.Name = "lblPassword";
			lblPassword.Size = new System.Drawing.Size(72, 15);
			lblPassword.TabIndex = 6;
			lblPassword.Text = "Password:";
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(392, 139);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(56, 23);
			buttonCancel.TabIndex = 14;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			xpButtonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButtonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButtonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButtonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButtonOK.Location = new System.Drawing.Point(328, 139);
			xpButtonOK.Name = "xpButtonOK";
			xpButtonOK.Size = new System.Drawing.Size(56, 23);
			xpButtonOK.TabIndex = 13;
			xpButtonOK.Text = "&OK";
			xpButtonOK.Click += new System.EventHandler(xpButtonOK_Click);
			textBoxHost.AcceptsReturn = false;
			textBoxHost.AcceptsTab = false;
			textBoxHost.AutoSize = true;
			textBoxHost.BackColor = System.Drawing.Color.White;
			textBoxHost.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxHost.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxHost.HideSelection = true;
			textBoxHost.IsComboTextBox = false;
			textBoxHost.Lines = new string[1]
			{
				"ftp."
			};
			textBoxHost.Location = new System.Drawing.Point(112, 7);
			textBoxHost.MaxLength = 32767;
			textBoxHost.Multiline = false;
			textBoxHost.Name = "textBoxHost";
			textBoxHost.PasswordChar = '\0';
			textBoxHost.ReadOnly = false;
			textBoxHost.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxHost.Size = new System.Drawing.Size(248, 20);
			textBoxHost.TabIndex = 1;
			textBoxHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			toolTip1.SetToolTip(textBoxHost, "FTP server address or IP address");
			textBoxHost.WordWrap = true;
			textBoxPort.AcceptsReturn = false;
			textBoxPort.AcceptsTab = false;
			textBoxPort.AutoSize = true;
			textBoxPort.BackColor = System.Drawing.Color.White;
			textBoxPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxPort.HideSelection = true;
			textBoxPort.IsComboTextBox = false;
			textBoxPort.Lines = new string[1]
			{
				"21"
			};
			textBoxPort.Location = new System.Drawing.Point(408, 7);
			textBoxPort.MaxLength = 32767;
			textBoxPort.Multiline = false;
			textBoxPort.Name = "textBoxPort";
			textBoxPort.PasswordChar = '\0';
			textBoxPort.ReadOnly = false;
			textBoxPort.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxPort.Size = new System.Drawing.Size(40, 20);
			textBoxPort.TabIndex = 3;
			textBoxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			toolTip1.SetToolTip(textBoxPort, "FTP server port number");
			textBoxPort.WordWrap = true;
			textBoxUserID.AcceptsReturn = false;
			textBoxUserID.AcceptsTab = false;
			textBoxUserID.AutoSize = true;
			textBoxUserID.BackColor = System.Drawing.Color.White;
			textBoxUserID.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxUserID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxUserID.HideSelection = true;
			textBoxUserID.IsComboTextBox = false;
			textBoxUserID.Lines = new string[0];
			textBoxUserID.Location = new System.Drawing.Point(112, 33);
			textBoxUserID.MaxLength = 32767;
			textBoxUserID.Multiline = false;
			textBoxUserID.Name = "textBoxUserID";
			textBoxUserID.PasswordChar = '\0';
			textBoxUserID.ReadOnly = false;
			textBoxUserID.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxUserID.Size = new System.Drawing.Size(136, 20);
			textBoxUserID.TabIndex = 5;
			textBoxUserID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			toolTip1.SetToolTip(textBoxUserID, "FTP server user ID");
			textBoxUserID.WordWrap = true;
			textBoxPassword.AcceptsReturn = false;
			textBoxPassword.AcceptsTab = false;
			textBoxPassword.AutoSize = true;
			textBoxPassword.BackColor = System.Drawing.Color.White;
			textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxPassword.HideSelection = true;
			textBoxPassword.IsComboTextBox = false;
			textBoxPassword.Lines = new string[0];
			textBoxPassword.Location = new System.Drawing.Point(336, 33);
			textBoxPassword.MaxLength = 32767;
			textBoxPassword.Multiline = false;
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.PasswordChar = '\0';
			textBoxPassword.ReadOnly = false;
			textBoxPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxPassword.Size = new System.Drawing.Size(112, 20);
			textBoxPassword.TabIndex = 7;
			textBoxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			toolTip1.SetToolTip(textBoxPassword, "FTP server password");
			textBoxPassword.WordWrap = true;
			textBoxPassword.Validated += new System.EventHandler(textBoxPassword_Validated);
			lblUsername.Location = new System.Drawing.Point(8, 33);
			lblUsername.Name = "lblUsername";
			lblUsername.Size = new System.Drawing.Size(88, 15);
			lblUsername.TabIndex = 4;
			lblUsername.Text = "User ID:";
			label1.Location = new System.Drawing.Point(8, 59);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(88, 15);
			label1.TabIndex = 8;
			label1.Text = "Directory:";
			textBoxDirectory.AcceptsReturn = false;
			textBoxDirectory.AcceptsTab = false;
			textBoxDirectory.AutoSize = true;
			textBoxDirectory.BackColor = System.Drawing.Color.White;
			textBoxDirectory.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxDirectory.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxDirectory.HideSelection = true;
			textBoxDirectory.IsComboTextBox = false;
			textBoxDirectory.Lines = new string[0];
			textBoxDirectory.Location = new System.Drawing.Point(112, 59);
			textBoxDirectory.MaxLength = 32767;
			textBoxDirectory.Multiline = false;
			textBoxDirectory.Name = "textBoxDirectory";
			textBoxDirectory.PasswordChar = '\0';
			textBoxDirectory.ReadOnly = false;
			textBoxDirectory.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxDirectory.Size = new System.Drawing.Size(336, 20);
			textBoxDirectory.TabIndex = 9;
			textBoxDirectory.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			toolTip1.SetToolTip(textBoxDirectory, "FTP directory, for example 'html'");
			textBoxDirectory.WordWrap = true;
			textBoxDirectory.Validated += new System.EventHandler(textBoxDirectory_Validated);
			textBoxFileName.AcceptsReturn = false;
			textBoxFileName.AcceptsTab = false;
			textBoxFileName.AutoSize = true;
			textBoxFileName.BackColor = System.Drawing.Color.White;
			textBoxFileName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxFileName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxFileName.HideSelection = true;
			textBoxFileName.IsComboTextBox = false;
			textBoxFileName.Lines = new string[0];
			textBoxFileName.Location = new System.Drawing.Point(112, 83);
			textBoxFileName.MaxLength = 32767;
			textBoxFileName.Multiline = false;
			textBoxFileName.Name = "textBoxFileName";
			textBoxFileName.PasswordChar = '\0';
			textBoxFileName.ReadOnly = false;
			textBoxFileName.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxFileName.Size = new System.Drawing.Size(336, 20);
			textBoxFileName.TabIndex = 11;
			textBoxFileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			toolTip1.SetToolTip(textBoxFileName, "File name: for example 'items.xml'");
			textBoxFileName.WordWrap = true;
			textBoxFileName.Validated += new System.EventHandler(textBoxFileName_Validated);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(8, 133);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(440, 1);
			line1.TabIndex = 16;
			line1.TabStop = false;
			label2.Location = new System.Drawing.Point(8, 83);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(88, 14);
			label2.TabIndex = 10;
			label2.Text = "File Name:";
			checkBoxSavePassword.Location = new System.Drawing.Point(8, 112);
			checkBoxSavePassword.Name = "checkBoxSavePassword";
			checkBoxSavePassword.Size = new System.Drawing.Size(160, 14);
			checkBoxSavePassword.TabIndex = 12;
			checkBoxSavePassword.Text = "Save Password";
			checkBoxSavePassword.CheckedChanged += new System.EventHandler(checkBoxSavePassword_CheckedChanged);
			base.AcceptButton = xpButtonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(456, 170);
			base.ControlBox = false;
			base.Controls.Add(checkBoxSavePassword);
			base.Controls.Add(textBoxFileName);
			base.Controls.Add(label2);
			base.Controls.Add(line1);
			base.Controls.Add(textBoxDirectory);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxPassword);
			base.Controls.Add(textBoxUserID);
			base.Controls.Add(textBoxPort);
			base.Controls.Add(textBoxHost);
			base.Controls.Add(xpButtonOK);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(lblPassword);
			base.Controls.Add(lblUsername);
			base.Controls.Add(lblPort);
			base.Controls.Add(lblHost);
			base.Name = "FTPConnProps";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			Text = "Connect to FTP server...";
			base.Load += new System.EventHandler(FTPConnProps_Load);
			ResumeLayout(false);
		}

		private void GetUserPrefs()
		{
			try
			{
				Host = Global.CompanySettings.GetSetting(base.Name + textBoxHost.Name, textBoxHost.Text).ToString();
				Port = int.Parse(Global.CompanySettings.GetSetting(base.Name + textBoxPort.Name, textBoxPort.Text).ToString());
				UserID = Global.CompanySettings.GetSetting(base.Name + textBoxUserID.Name, textBoxUserID.Text).ToString();
				checkBoxSavePassword.Checked = bool.Parse(Global.CompanySettings.GetSetting(base.Name + checkBoxSavePassword.Name, false).ToString());
				if (checkBoxSavePassword.Checked)
				{
					Password = Global.GetPassword("ftp");
				}
				Directory = Global.CompanySettings.GetSetting(base.Name + textBoxDirectory.Name, textBoxDirectory.Text).ToString();
				FileName = Global.CompanySettings.GetSetting(base.Name + textBoxFileName.Name, textBoxFileName.Text).ToString();
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
					Global.SavePassword(Password, "ftp");
				}
				else
				{
					Global.SavePassword("", "ftp");
				}
				Global.CompanySettings.SaveSetting(base.Name + textBoxDirectory.Name, textBoxDirectory.Text);
				Global.CompanySettings.SaveSetting(base.Name + textBoxFileName.Name, textBoxFileName.Text);
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
				ErrorHelper.WarningMessage(SR.GetString("00327"));
				textBoxHost.Focus();
				return false;
			}
			if (textBoxHost.Text.ToLower().IndexOf("ftp.") < 0)
			{
				ErrorHelper.WarningMessage(SR.GetString("00330"));
				textBoxHost.Focus();
				textBoxHost.SelectAll();
				return false;
			}
			if (textBoxPort.Text.Trim() == string.Empty)
			{
				ErrorHelper.WarningMessage(SR.GetString("00079"));
				textBoxPort.Focus();
				return false;
			}
			try
			{
				int.Parse(textBoxPort.Text);
			}
			catch
			{
				ErrorHelper.WarningMessage(SR.GetString("00079"));
				textBoxPort.Focus();
				return false;
			}
			if (textBoxDirectory.Text.Trim() == string.Empty)
			{
				ErrorHelper.WarningMessage(SR.GetString("00328"));
				textBoxDirectory.Focus();
				return false;
			}
			if (textBoxFileName.Text.Trim() == string.Empty)
			{
				ErrorHelper.WarningMessage(SR.GetString("00329"));
				textBoxFileName.Focus();
				return false;
			}
			return true;
		}

		private void checkBoxSavePassword_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void textBoxFileName_Validated(object sender, EventArgs e)
		{
			if (textBoxFileName.Text.Trim() != string.Empty && textBoxFileName.Text.Trim().IndexOf(".") < 0)
			{
				textBoxFileName.Text += ".xml";
			}
		}

		private void textBoxDirectory_Validated(object sender, EventArgs e)
		{
		}

		private void textBoxPassword_Validated(object sender, EventArgs e)
		{
			textBoxPassword.Text = Factory.Encrypt(textBoxPassword.Text);
		}
	}
}
