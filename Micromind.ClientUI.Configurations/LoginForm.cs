using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
using Micromind.Common.Libraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class LoginForm : Form
	{
		private bool validateForm = true;

		private bool loginSuccessfull;

		private bool connectAutomatically;

		private string instanceName = "";

		private string portNumber = "0";

		private string databaseName = "";

		private string companyName = "";

		private bool isEventConnection = true;

		private MMTextBox editUserName;

		private MMTextBox editPassword;

		private MMLabel lblPassword;

		private MMLabel lblUserName;

		private MMLabel label1;

		private MMLabel label2;

		private XPButton buttonOK;

		private XPButton buttonCancel;

		private Line line1;

		private MMTextBox editServerName;

		private CheckBox checkBoxPassword;

		private CheckBox checkBoxUserID;

		private XPButton xpButton1;

		private Panel panel1;

		private Panel panel2;

		private Container components;

		private bool isFirstActivated = true;

		private bool notify = true;

		private bool suppressMessage;
        private Label label4;
        private Label label3;
        private bool validate = true;

		internal bool IsLoginSuccessful => loginSuccessfull;

		private int Port => Global.CurrentPortNumber;

		public bool Notify
		{
			set
			{
				notify = value;
			}
		}

		public bool SuppressMessage
		{
			set
			{
				suppressMessage = value;
			}
		}

		public bool ValidateForm
		{
			set
			{
				validate = value;
			}
		}

		public bool ConnectAutomatically
		{
			get
			{
				return connectAutomatically;
			}
			set
			{
				connectAutomatically = value;
			}
		}

		public LoginForm()
		{
			InitializeComponent();
			LoadRegistry();
			base.FormClosing += LoginForm_FormClosing;
			Translator.Translators.Translate(this);
		}

		private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.Cancel && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Do you want to exit?") == DialogResult.No)
			{
				e.Cancel = true;
				base.DialogResult = DialogResult.None;
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
            this.editUserName = new Micromind.UISupport.MMTextBox();
            this.editPassword = new Micromind.UISupport.MMTextBox();
            this.lblPassword = new Micromind.UISupport.MMLabel();
            this.lblUserName = new Micromind.UISupport.MMLabel();
            this.label1 = new Micromind.UISupport.MMLabel();
            this.label2 = new Micromind.UISupport.MMLabel();
            this.checkBoxPassword = new System.Windows.Forms.CheckBox();
            this.checkBoxUserID = new System.Windows.Forms.CheckBox();
            this.buttonOK = new Micromind.UISupport.XPButton();
            this.buttonCancel = new Micromind.UISupport.XPButton();
            this.line1 = new Micromind.UISupport.Line();
            this.editServerName = new Micromind.UISupport.MMTextBox();
            this.xpButton1 = new Micromind.UISupport.XPButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // editUserName
            // 
            this.editUserName.BackColor = System.Drawing.Color.White;
            this.editUserName.CustomReportFieldName = "";
            this.editUserName.CustomReportKey = "";
            this.editUserName.CustomReportValueType = ((byte)(1));
            this.editUserName.IsComboTextBox = false;
            this.editUserName.IsModified = false;
            this.editUserName.IsRequired = true;
            this.editUserName.Location = new System.Drawing.Point(354, 16);
            this.editUserName.Name = "editUserName";
            this.editUserName.Size = new System.Drawing.Size(169, 20);
            this.editUserName.TabIndex = 1;
            this.editUserName.TextChanged += new System.EventHandler(this.editUserName_TextChanged);
            this.editUserName.Validated += new System.EventHandler(this.editUserName_Validated);
            // 
            // editPassword
            // 
            this.editPassword.BackColor = System.Drawing.Color.White;
            this.editPassword.CustomReportFieldName = "";
            this.editPassword.CustomReportKey = "";
            this.editPassword.CustomReportValueType = ((byte)(1));
            this.editPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.editPassword.IsComboTextBox = false;
            this.editPassword.IsModified = false;
            this.editPassword.Location = new System.Drawing.Point(354, 39);
            this.editPassword.Name = "editPassword";
            this.editPassword.Size = new System.Drawing.Size(169, 20);
            this.editPassword.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.lblPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPassword.IsFieldHeader = false;
            this.lblPassword.IsRequired = false;
            this.lblPassword.Location = new System.Drawing.Point(-76, 42);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.PenWidth = 1F;
            this.lblPassword.ShowBorder = false;
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.lblUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUserName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUserName.IsFieldHeader = false;
            this.lblUserName.IsRequired = false;
            this.lblUserName.Location = new System.Drawing.Point(-76, 26);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.PenWidth = 1F;
            this.lblUserName.ShowBorder = false;
            this.lblUserName.Size = new System.Drawing.Size(46, 13);
            this.lblUserName.TabIndex = 5;
            this.lblUserName.Text = "User ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.IsFieldHeader = false;
            this.label1.IsRequired = false;
            this.label1.Location = new System.Drawing.Point(292, 41);
            this.label1.Name = "label1";
            this.label1.PenWidth = 1F;
            this.label1.ShowBorder = false;
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "&Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.IsFieldHeader = false;
            this.label2.IsRequired = false;
            this.label2.Location = new System.Drawing.Point(292, 20);
            this.label2.Name = "label2";
            this.label2.PenWidth = 1F;
            this.label2.ShowBorder = false;
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "&User ID:";
            // 
            // checkBoxPassword
            // 
            this.checkBoxPassword.AutoSize = true;
            this.checkBoxPassword.Location = new System.Drawing.Point(354, 83);
            this.checkBoxPassword.Name = "checkBoxPassword";
            this.checkBoxPassword.Size = new System.Drawing.Size(126, 17);
            this.checkBoxPassword.TabIndex = 5;
            this.checkBoxPassword.Text = "Remember Password";
            this.checkBoxPassword.UseVisualStyleBackColor = true;
            this.checkBoxPassword.CheckedChanged += new System.EventHandler(this.checkBoxPassword_CheckedChanged);
            // 
            // checkBoxUserID
            // 
            this.checkBoxUserID.AutoSize = true;
            this.checkBoxUserID.Checked = true;
            this.checkBoxUserID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUserID.Location = new System.Drawing.Point(354, 65);
            this.checkBoxUserID.Name = "checkBoxUserID";
            this.checkBoxUserID.Size = new System.Drawing.Size(116, 17);
            this.checkBoxUserID.TabIndex = 4;
            this.checkBoxUserID.Text = "Remember User ID";
            this.checkBoxUserID.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.BackColor = System.Drawing.Color.DarkGray;
            this.buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOK.Location = new System.Drawing.Point(363, 259);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(77, 24);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.BackColor = System.Drawing.Color.DarkGray;
            this.buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonCancel.Location = new System.Drawing.Point(446, 259);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(77, 24);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // line1
            // 
            this.line1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.line1.BackColor = System.Drawing.Color.White;
            this.line1.DrawWidth = 1;
            this.line1.IsVertical = false;
            this.line1.LineBackColor = System.Drawing.Color.LightSteelBlue;
            this.line1.Location = new System.Drawing.Point(-9, 252);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(559, 1);
            this.line1.TabIndex = 9;
            this.line1.TabStop = false;
            // 
            // editServerName
            // 
            this.editServerName.BackColor = System.Drawing.Color.White;
            this.editServerName.CustomReportFieldName = "";
            this.editServerName.CustomReportKey = "";
            this.editServerName.CustomReportValueType = ((byte)(1));
            this.editServerName.IsComboTextBox = false;
            this.editServerName.IsModified = false;
            this.editServerName.IsRequired = true;
            this.editServerName.Location = new System.Drawing.Point(128, 39);
            this.editServerName.Name = "editServerName";
            this.editServerName.Size = new System.Drawing.Size(304, 20);
            this.editServerName.TabIndex = 1;
            this.editServerName.TextChanged += new System.EventHandler(this.editUserName_TextChanged);
            // 
            // xpButton1
            // 
            this.xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.xpButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xpButton1.BackColor = System.Drawing.Color.DarkGray;
            this.xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.xpButton1.Location = new System.Drawing.Point(12, 259);
            this.xpButton1.Name = "xpButton1";
            this.xpButton1.Size = new System.Drawing.Size(133, 24);
            this.xpButton1.TabIndex = 3;
            this.xpButton1.Text = "Connection Settings...";
            this.xpButton1.UseVisualStyleBackColor = false;
            this.xpButton1.Click += new System.EventHandler(this.xpButton1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.editPassword);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.editUserName);
            this.panel1.Controls.Add(this.checkBoxPassword);
            this.panel1.Controls.Add(this.checkBoxUserID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 126);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 126);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::Micromind.ClientUI.Properties.Resources.headerbg2;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(535, 126);
            this.panel2.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(6, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(282, 24);
            this.label4.TabIndex = 136;
            this.label4.Text = "Business Managment System";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(283, 31);
            this.label3.TabIndex = 135;
            this.label3.Text = "Starasoft © StarERP";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(535, 288);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xpButton1);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Login";
            this.Activated += new System.EventHandler(this.DatabaseLoginForm_Activated);
            this.Load += new System.EventHandler(this.DatabaseLoginForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private void baPanel1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		public bool LoginDatabase(Form parent, string databaseName, string companyName, string instanceName, string portNumber)
		{
			this.instanceName = Global.CurrentInstanceName;
			this.portNumber = Global.CurrentPortNumber.ToString();
			ShowDialog(parent);
			return loginSuccessfull;
		}

		public bool LoginDatabase(Form parent, string databaseName, string companyName, string instanceName, string portNumber, string userID)
		{
			this.instanceName = Global.CurrentInstanceName;
			editUserName.Text = userID;
			this.portNumber = Global.CurrentPortNumber.ToString();
			if (editUserName.Text.Trim() != string.Empty)
			{
				editPassword.Focus();
			}
			if (loginSuccessfull)
			{
				return true;
			}
			ShowDialog(parent);
			return loginSuccessfull;
		}

		public bool LoginDatabase(Form parent, string databaseName, string companyName, string instanceName, string portNumber, string userID, string password)
		{
			this.instanceName = Global.CurrentInstanceName;
			editUserName.Text = userID;
			editPassword.Text = password;
			this.portNumber = Global.CurrentPortNumber.ToString();
			if (editUserName.Text.Trim() != string.Empty)
			{
				editPassword.Focus();
			}
			ShowDialog(parent);
			return loginSuccessfull;
		}

		public bool LoginDatabase(string databaseName, string companyName, string instanceName, string portNumber, string userID, string password)
		{
			this.instanceName = Global.CurrentInstanceName;
			editUserName.Text = userID;
			editPassword.Text = password;
			this.portNumber = Global.CurrentPortNumber.ToString();
			if (editUserName.Text.Trim() != string.Empty)
			{
				editPassword.Focus();
			}
			return loginSuccessfull;
		}

		public bool LoginDatabase(string databaseName, string companyName, string userID, string password)
		{
			editUserName.Text = userID;
			editPassword.Text = password;
			if (editUserName.Text.Trim() != string.Empty)
			{
				editPassword.Focus();
			}
			return loginSuccessfull;
		}

		private bool IsValid(bool suppressMessage)
		{
			if (editUserName.Text.Length == 0)
			{
				if (!suppressMessage)
				{
					ErrorHelper.WarningMessage("Please enter your user ID");
					editUserName.Focus();
				}
				return false;
			}
			char[] array = editPassword.Text.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				_ = array[i];
			}
			return true;
		}

		private bool RequireUpdate()
		{
			try
			{
				if (Global.IsMultiUser)
				{
					ApplicationUpdateConfig currentAxolonServerVersion = Factory.CompanyInformationSystem.GetCurrentAxolonServerVersion();
					ApplicationUpdateConfig currentClientVersion = UIGlobal.GetCurrentClientVersion();
					if (currentAxolonServerVersion.GetVersionInteger() > currentClientVersion.GetVersionInteger())
					{
						return true;
					}
					return false;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		private bool Login()
		{
			bool flag = false;
			try
			{
				PublicFunctions.StartWaiting(this);
				flag = Connect("master", Global.CurrentInstanceName, editUserName.Text, editPassword.Text, Global.CurrentPortNumber.ToString());
				if (!flag)
				{
					return flag;
				}
				if (RequireUpdate() && Global.IsMultiUser)
				{
					new AxolonUpdateDialog().ShowDialog();
					return false;
				}
				if (companyName.Trim() == "")
				{
					try
					{
						SaveCompany(Global.CompanyName);
					}
					catch
					{
					}
				}
				else
				{
					SaveCompany(companyName);
				}
				loginSuccessfull = true;
				return flag;
			}
			 
			catch (Exception ex2)
			{
				ErrorHelper.WarningMessage(ex2);
				return flag;
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
				buttonOK.Enabled = true;
				buttonCancel.Enabled = true;
				Application.DoEvents();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.None;
			if (IsValid(suppressMessage: false) ? true : false)
			{
				if (Login())
				{
					base.DialogResult = DialogResult.OK;
					SaveRegistry();
					Close();
				}
				else
				{
					editPassword.SelectAll();
					editPassword.Focus();
				}
			}
		}

		public void SaveCompany(string companyName)
		{
			try
			{
				DatabaseHelper.RemoveCompanyHistory(companyName);
				DatabaseHelper.SaveCompany(companyName, Global.CurrentInstanceName, databaseName, editUserName.Text, portNumber);
			}
			catch (Exception ex)
			{
				ErrorHelper.WarningMessage(SR.GetString("00083"), ex.Message);
				ErrorHelper.ProcessError(ex);
			}
		}

		private string GetEncyptedPassword()
		{
			_ = (Global.CurrentUser.ToLower() == "sa");
			return Factory.Encrypt(editPassword.Text);
		}

		private void SetEvents(bool isConnected, bool notify)
		{
			if (notify && isEventConnection)
			{
				if (isConnected)
				{
					Global.ConStatus = ConnectionStatus.Connected;
				}
				else
				{
					Factory.DisconnectDB();
				}
			}
		}

		private void SaveRegistry()
		{
			Global.SaveRegistryOptionValue("LoginID", editUserName.Text, isEncrypt: true);
			Global.SaveRegistryOptionValue("LoginPass", Factory.Encrypt(editPassword.Text), isEncrypt: true);
			Global.SaveRegistryOptionValue("RemUserID", checkBoxUserID.Checked.ToString(), isEncrypt: false);
			Global.SaveRegistryOptionValue("RemPass", checkBoxPassword.Checked.ToString(), isEncrypt: false);
		}

		private void LoadRegistry()
		{
			bool result = false;
			bool.TryParse(Global.GetRegistryOptionValue("RemUserID", isEncrypt: false), out result);
			checkBoxUserID.Checked = result;
			result = false;
			bool.TryParse(Global.GetRegistryOptionValue("RemPass", isEncrypt: false), out result);
			checkBoxPassword.Checked = result;
			if (checkBoxUserID.Checked)
			{
				editUserName.Text = Global.GetRegistryOptionValue("LoginID", isEncrypt: true);
			}
			if (checkBoxPassword.Checked)
			{
				editPassword.Text = Factory.Decrypt(Global.GetRegistryOptionValue("LoginPass", isEncrypt: true));
			}
		}

		private bool CanConnect(bool suppressMessage, bool notify, bool validate)
		{
			bool flag = false;
			if (!suppressMessage && validate && validateForm && !IsValid(suppressMessage))
			{
				return false;
			}
			_ = Port;
			try
			{
				Application.DoEvents();
				flag = Factory.SetLoginInfo(suppressMessage, base.ProductName, databaseName, editUserName.Text, GetEncyptedPassword());
				if (!flag)
				{
					return false;
				}
			}
			
			catch (SocketException ex2)
			{
				if (!suppressMessage)
				{
					ErrorHelper.WarningMessage(ex2.Message, SR.GetString("00086"));
				}
				SetEvents(isConnected: false, notify);
				flag = false;
			}
			catch (DBNotConnectedException)
			{
			}
			catch (CompanyException ex4)
			{
				if (ex4.Number == 1024)
				{
					ErrorHelper.ErrorMessage(ex4.Message);
				}
				else
				{
					ErrorHelper.ProcessError(ex4);
				}
				flag = false;
			}
			catch (Exception e)
			{
				if (!suppressMessage)
				{
					ErrorHelper.ProcessError(e);
				}
				SetEvents(isConnected: false, notify);
				flag = false;
			}
			finally
			{
				Application.DoEvents();
			}
			if (flag)
			{
				if (isEventConnection)
				{
					Global.CurrentUser = editUserName.Text;
					Global.CurrentPassword = editPassword.Text;
				}
			}
			else if (isEventConnection && notify)
			{
				SetEvents(isConnected: false, notify);
			}
			else
			{
				Factory.DisconnectDB();
			}
			return flag;
		}

		public bool Connect(string userName, string password, bool savePassword)
		{
			bool flag = false;
			try
			{
				return CanConnect(suppressMessage: false, notify: false, validate: true);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void DatabaseLoginForm_Activated(object sender, EventArgs e)
		{
			if (isFirstActivated)
			{
				if (editUserName.Text == "")
				{
					editUserName.Focus();
				}
				else
				{
					editPassword.Focus();
				}
				isFirstActivated = false;
			}
		}

		public void LoadForm()
		{
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				editPassword.PasswordChar = '●';
			}
			else
			{
				editPassword.PasswordChar = '*';
			}
		}

		private void DatabaseLoginForm_Load(object sender, EventArgs e)
		{
			try
			{
				LoadForm();
				LoadRegistry();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public bool Connect(string userName, string password, bool savePassword, bool suppressMessage, bool notify, bool validate)
		{
			bool flag = false;
			try
			{
				return CanConnect(suppressMessage, notify, validate);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public bool Connect(string dbName, string instanceName, string userName, string password)
		{
			bool flag = false;
			databaseName = dbName;
			editUserName.Text = userName;
			try
			{
				return CanConnect(suppressMessage, notify, validate);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public bool Connect(string companyName, string dbName, string instanceName, string userName, string password, string portNumber)
		{
			bool flag = false;
			databaseName = dbName;
			editUserName.Text = userName;
			try
			{
				return CanConnect(suppressMessage, notify, validate);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public bool Connect(string dbName, string instanceName, string userName, string password, string portNumber)
		{
			bool flag = false;
			databaseName = dbName;
			editUserName.Text = userName;
			try
			{
				return CanConnect(suppressMessage, notify, validate);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public bool OpenPreviousCompany()
		{
			LoadLastCompanyDetails();
			instanceName = Global.CurrentInstanceName;
			portNumber = Global.CurrentPortNumber.ToString();
			if (Global.OpenPreviousCompany)
			{
				validateForm = false;
				return Login();
			}
			return false;
		}

		private void LoadLastCompanyDetails()
		{
			if (GlobalRules.IsMultiUser)
			{
				instanceName = Global.CurrentInstanceName;
				portNumber = Global.CurrentPortNumber.ToString();
			}
		}

		private void checkBoxRememberPassword_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			BrowseCompany();
		}

		private void BrowseCompany()
		{
		}

		private void editUserName_Validated(object sender, EventArgs e)
		{
		}

		private void EnableDisableOkButton()
		{
		}

		private void editUserName_TextChanged(object sender, EventArgs e)
		{
			EnableDisableOkButton();
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			new ConnectionSettingsDialog().ShowDialog(this);
		}

		private void checkBoxPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxPassword.Checked)
			{
				checkBoxUserID.Checked = checkBoxPassword.Checked;
			}
			checkBoxUserID.Enabled = !checkBoxPassword.Checked;
		}
	}
}
