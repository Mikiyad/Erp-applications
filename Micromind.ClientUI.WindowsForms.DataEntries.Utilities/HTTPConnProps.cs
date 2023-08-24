using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Utilities
{
	public class HTTPConnProps : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		private Label lblHost;

		private XPButton buttonCancel;

		private XPButton xpButtonOK;

		private ToolTip toolTip1;

		private Line line1;

		private MMTextBox textBoxHost;

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

		public string UserID => null;

		public string Password => null;

		public HTTPConnProps()
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
			buttonCancel = new Micromind.UISupport.XPButton();
			xpButtonOK = new Micromind.UISupport.XPButton();
			textBoxHost = new Micromind.UISupport.MMTextBox();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			line1 = new Micromind.UISupport.Line();
			SuspendLayout();
			lblHost.Location = new System.Drawing.Point(8, 8);
			lblHost.Name = "lblHost";
			lblHost.Size = new System.Drawing.Size(104, 16);
			lblHost.TabIndex = 2;
			lblHost.Text = "URL Address:";
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(392, 48);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(56, 24);
			buttonCancel.TabIndex = 7;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			xpButtonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButtonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButtonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButtonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButtonOK.Location = new System.Drawing.Point(328, 48);
			xpButtonOK.Name = "xpButtonOK";
			xpButtonOK.Size = new System.Drawing.Size(56, 24);
			xpButtonOK.TabIndex = 6;
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
				"http://www."
			};
			textBoxHost.Location = new System.Drawing.Point(112, 8);
			textBoxHost.MaxLength = 32767;
			textBoxHost.Multiline = false;
			textBoxHost.Name = "textBoxHost";
			textBoxHost.PasswordChar = '\0';
			textBoxHost.ReadOnly = false;
			textBoxHost.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxHost.Size = new System.Drawing.Size(336, 20);
			textBoxHost.TabIndex = 0;
			textBoxHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			toolTip1.SetToolTip(textBoxHost, "HTTP server address or IP address");
			textBoxHost.WordWrap = true;
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(8, 40);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(440, 1);
			line1.TabIndex = 16;
			line1.TabStop = false;
			base.AcceptButton = xpButtonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(456, 77);
			base.ControlBox = false;
			base.Controls.Add(line1);
			base.Controls.Add(textBoxHost);
			base.Controls.Add(xpButtonOK);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(lblHost);
			base.Name = "HTTPConnProps";
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			Text = "Connect to HTTP server...";
			base.Load += new System.EventHandler(HTTPConnProps_Load);
			ResumeLayout(false);
		}

		private void GetUserPrefs()
		{
			try
			{
				Host = Global.CompanySettings.GetSetting(base.Name + textBoxHost.Name, textBoxHost.Text).ToString();
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

		private void HTTPConnProps_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
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
				ErrorHelper.WarningMessage(SR.GetString("00333"));
				textBoxHost.Focus();
				return false;
			}
			return true;
		}

		private void checkBoxSavePassword_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void textBoxFileName_Validated(object sender, EventArgs e)
		{
		}

		private void textBoxDirectory_Validated(object sender, EventArgs e)
		{
		}
	}
}
