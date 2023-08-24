using Infragistics.Win;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Libraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class DatabaseLoginForm : Form
	{
		public bool IsNewCompanyRequested;

		public bool IsRestoreCompanyRequested;

		public bool IsAttachCompanyRequested;

		private bool validateForm = true;

		private bool loginSuccessfull;

		private bool connectAutomatically;

		private string instanceName = "";

		private string portNumber = "0";

		private string databaseName = "";

		private string companyName = "";

		private bool isEventConnection = true;

		private MMLabel lblPassword;

		private MMLabel lblUserName;

		private XPButton buttonOK;

		private XPButton buttonCancel;

		private Line line1;

		private Panel panel1;

		private Label label1;

		private RadioButton radioButtonSample;

		private RadioButton radioButtonCompany;

		private Panel panel2;

		private UltraLabel editUserName;

		private UltraLabel editCompanyName;

		private XPButton buttonSelectDatabase;

		private ContextMenuStrip contextMenuOptions;

		private UltraPopupControlContainer ultraPopupControlContainer1;

		private ToolStripMenuItem changeUserToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem newDatabaseToolStripMenuItem;

		private ToolStripMenuItem restoreDatabaseToolStripMenuItem;

		private ToolStripMenuItem attachDatabaseToolStripMenuItem;

		private UltraLabel textBoxDatabaseName;

		private Button buttonOptions;

		private ToolStripMenuItem upgradeDatabaseToolStripMenuItem;

		private IContainer components;

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

		public DatabaseLoginForm()
		{
			InitializeComponent();
			Translator.Translators.Translate(this);
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            this.lblPassword = new Micromind.UISupport.MMLabel();
            this.lblUserName = new Micromind.UISupport.MMLabel();
            this.buttonOK = new Micromind.UISupport.XPButton();
            this.buttonCancel = new Micromind.UISupport.XPButton();
            this.line1 = new Micromind.UISupport.Line();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSelectDatabase = new Micromind.UISupport.XPButton();
            this.textBoxDatabaseName = new Infragistics.Win.Misc.UltraLabel();
            this.editCompanyName = new Infragistics.Win.Misc.UltraLabel();
            this.editUserName = new Infragistics.Win.Misc.UltraLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonSample = new System.Windows.Forms.RadioButton();
            this.radioButtonCompany = new System.Windows.Forms.RadioButton();
            this.ultraPopupControlContainer1 = new Infragistics.Win.Misc.UltraPopupControlContainer(this.components);
            this.contextMenuOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attachDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upgradeDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.contextMenuOptions.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            // buttonOK
            // 
            this.buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.BackColor = System.Drawing.Color.DarkGray;
            this.buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonOK.Location = new System.Drawing.Point(357, 259);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(80, 25);
            this.buttonOK.TabIndex = 2;
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
            this.buttonCancel.Location = new System.Drawing.Point(445, 259);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 25);
            this.buttonCancel.TabIndex = 3;
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
            this.line1.Location = new System.Drawing.Point(-2, 252);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(554, 1);
            this.line1.TabIndex = 9;
            this.line1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonSelectDatabase);
            this.panel1.Controls.Add(this.textBoxDatabaseName);
            this.panel1.Controls.Add(this.editCompanyName);
            this.panel1.Controls.Add(this.editUserName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.radioButtonSample);
            this.panel1.Controls.Add(this.radioButtonCompany);
            this.panel1.Location = new System.Drawing.Point(-2, 126);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 126);
            this.panel1.TabIndex = 0;
            // 
            // buttonSelectDatabase
            // 
            this.buttonSelectDatabase.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonSelectDatabase.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSelectDatabase.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonSelectDatabase.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonSelectDatabase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSelectDatabase.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSelectDatabase.Location = new System.Drawing.Point(482, 42);
            this.buttonSelectDatabase.Name = "buttonSelectDatabase";
            this.buttonSelectDatabase.Size = new System.Drawing.Size(25, 20);
            this.buttonSelectDatabase.TabIndex = 3;
            this.buttonSelectDatabase.Text = "...";
            this.buttonSelectDatabase.UseVisualStyleBackColor = false;
            this.buttonSelectDatabase.Click += new System.EventHandler(this.buttonSelectDatabase_Click);
            // 
            // textBoxDatabaseName
            // 
            appearance1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            appearance1.TextVAlignAsString = "Middle";
            this.textBoxDatabaseName.Appearance = appearance1;
            this.textBoxDatabaseName.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
            this.textBoxDatabaseName.Location = new System.Drawing.Point(319, 42);
            this.textBoxDatabaseName.Name = "textBoxDatabaseName";
            this.textBoxDatabaseName.Size = new System.Drawing.Size(162, 20);
            this.textBoxDatabaseName.TabIndex = 2;
            // 
            // editCompanyName
            // 
            appearance2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            appearance2.TextVAlignAsString = "Middle";
            this.editCompanyName.Appearance = appearance2;
            this.editCompanyName.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
            this.editCompanyName.Location = new System.Drawing.Point(319, 64);
            this.editCompanyName.Name = "editCompanyName";
            this.editCompanyName.Size = new System.Drawing.Size(162, 20);
            this.editCompanyName.TabIndex = 3;
            // 
            // editUserName
            // 
            appearance3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            appearance3.TextVAlignAsString = "Middle";
            this.editUserName.Appearance = appearance3;
            this.editUserName.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
            this.editUserName.Location = new System.Drawing.Point(319, 20);
            this.editUserName.Name = "editUserName";
            this.editUserName.Size = new System.Drawing.Size(162, 20);
            this.editUserName.TabIndex = 0;
            this.editUserName.Text = "SA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User ID:";
            // 
            // radioButtonSample
            // 
            this.radioButtonSample.AutoSize = true;
            this.radioButtonSample.Location = new System.Drawing.Point(241, 88);
            this.radioButtonSample.Name = "radioButtonSample";
            this.radioButtonSample.Size = new System.Drawing.Size(107, 17);
            this.radioButtonSample.TabIndex = 4;
            this.radioButtonSample.Text = "Sample Company";
            this.radioButtonSample.UseVisualStyleBackColor = true;
            // 
            // radioButtonCompany
            // 
            this.radioButtonCompany.AutoSize = true;
            this.radioButtonCompany.Checked = true;
            this.radioButtonCompany.Location = new System.Drawing.Point(241, 43);
            this.radioButtonCompany.Name = "radioButtonCompany";
            this.radioButtonCompany.Size = new System.Drawing.Size(72, 17);
            this.radioButtonCompany.TabIndex = 1;
            this.radioButtonCompany.TabStop = true;
            this.radioButtonCompany.Text = "Company:";
            this.radioButtonCompany.UseVisualStyleBackColor = true;
            // 
            // ultraPopupControlContainer1
            // 
            this.ultraPopupControlContainer1.PopupControl = this.contextMenuOptions;
            // 
            // contextMenuOptions
            // 
            this.contextMenuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeUserToolStripMenuItem,
            this.toolStripSeparator1,
            this.newDatabaseToolStripMenuItem,
            this.restoreDatabaseToolStripMenuItem,
            this.attachDatabaseToolStripMenuItem,
            this.upgradeDatabaseToolStripMenuItem});
            this.contextMenuOptions.Name = "contextMenuStrip1";
            this.contextMenuOptions.Size = new System.Drawing.Size(180, 120);
            // 
            // changeUserToolStripMenuItem
            // 
            this.changeUserToolStripMenuItem.Name = "changeUserToolStripMenuItem";
            this.changeUserToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.changeUserToolStripMenuItem.Text = "Change User";
            this.changeUserToolStripMenuItem.Click += new System.EventHandler(this.changeUserToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // newDatabaseToolStripMenuItem
            // 
            this.newDatabaseToolStripMenuItem.Name = "newDatabaseToolStripMenuItem";
            this.newDatabaseToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.newDatabaseToolStripMenuItem.Text = "New Database...";
            this.newDatabaseToolStripMenuItem.Click += new System.EventHandler(this.newDatabaseToolStripMenuItem_Click);
            // 
            // restoreDatabaseToolStripMenuItem
            // 
            this.restoreDatabaseToolStripMenuItem.Name = "restoreDatabaseToolStripMenuItem";
            this.restoreDatabaseToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.restoreDatabaseToolStripMenuItem.Text = "Restore Database...";
            this.restoreDatabaseToolStripMenuItem.Click += new System.EventHandler(this.restoreDatabaseToolStripMenuItem_Click);
            // 
            // attachDatabaseToolStripMenuItem
            // 
            this.attachDatabaseToolStripMenuItem.Name = "attachDatabaseToolStripMenuItem";
            this.attachDatabaseToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.attachDatabaseToolStripMenuItem.Text = "Attach Database...";
            this.attachDatabaseToolStripMenuItem.Click += new System.EventHandler(this.attachDatabaseToolStripMenuItem_Click);
            // 
            // upgradeDatabaseToolStripMenuItem
            // 
            this.upgradeDatabaseToolStripMenuItem.Name = "upgradeDatabaseToolStripMenuItem";
            this.upgradeDatabaseToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.upgradeDatabaseToolStripMenuItem.Text = "Upgrade Database...";
            this.upgradeDatabaseToolStripMenuItem.Click += new System.EventHandler(this.upgradeDatabaseToolStripMenuItem_Click);
            // 
            // buttonOptions
            // 
            this.buttonOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonOptions.Image = global::Micromind.ClientUI.Properties.Resources.dropdown21;
            this.buttonOptions.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOptions.Location = new System.Drawing.Point(10, 259);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(99, 26);
            this.buttonOptions.TabIndex = 1;
            this.buttonOptions.Text = "Options";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::Micromind.ClientUI.Properties.Resources.headerbg2;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(-2, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(534, 130);
            this.panel2.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(6, 63);
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
            this.label3.Location = new System.Drawing.Point(6, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(283, 31);
            this.label3.TabIndex = 135;
            this.label3.Text = "Starasoft © StarERP";
            // 
            // DatabaseLoginForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(531, 289);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseLoginForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Login";
            this.Activated += new System.EventHandler(this.DatabaseLoginForm_Activated);
            this.Load += new System.EventHandler(this.DatabaseLoginForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuOptions.ResumeLayout(false);
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

		private void LoadDatabases()
		{
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
			this.portNumber = Global.CurrentPortNumber.ToString();
			ShowDialog(parent);
			return loginSuccessfull;
		}

		public bool LoginDatabase(string databaseName, string companyName, string instanceName, string portNumber, string userID, string password)
		{
			this.instanceName = Global.CurrentInstanceName;
			editUserName.Text = userID;
			this.portNumber = Global.CurrentPortNumber.ToString();
			return loginSuccessfull;
		}

		public bool LoginDatabase(string databaseName, string companyName, string userID, string password)
		{
			editUserName.Text = userID;
			return loginSuccessfull;
		}

		private bool IsValid(bool suppressMessage)
		{
			if (editUserName.Text.Length == 0)
			{
				if (!suppressMessage)
				{
					ErrorHelper.WarningMessage("Please enter the user ID.");
					editUserName.Focus();
				}
				return false;
			}
			return true;
		}

		private bool GotoCompany()
		{
			bool flag = false;
			try
			{
				PublicFunctions.StartWaiting(this);
				Refresh();
				string text = textBoxDatabaseName.Text;
				if (text != null && text != string.Empty)
				{
					Global.ChangeApplicationStatusMessage("Connecting to " + text + "...");
				}
				else
				{
					Global.ChangeApplicationStatusMessage("Connecting...");
				}
				Application.DoEvents();
				flag = Connect(textBoxDatabaseName.Text, Global.CurrentInstanceName, Global.CurrentUser, Global.CurrentPassword, Global.CurrentPortNumber.ToString());
				if (!flag)
				{
					return flag;
				}
				bool flag2 = Factory.DatabaseSystem.IsUserAllowedToConnect(Global.CurrentUser);
				if (Global.CurrentUser.ToLower() != "sa" && !flag2)
				{
					ErrorHelper.ErrorMessage("This user does not have permission to connect to this database. Please contact your administrator.");
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
				Close();
				return flag;
			}
			catch (SqlException ex)
			{
				ErrorHelper.WarningMessage(ex);
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
			if (IsValid(suppressMessage: false))
			{
				bool flag = true;
				if (radioButtonSample.Checked)
				{
					ErrorHelper.InformationMessage("Sample company is not available.");
				}
				else if (flag && GotoCompany())
				{
					base.DialogResult = DialogResult.OK;
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
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private string GetEncyptedPassword()
		{
			return Factory.Encrypt(Global.CurrentPassword);
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
			Global.SaveRegistryOptionValue("CurDB", textBoxDatabaseName.Text, isEncrypt: true);
			Global.SaveRegistryOptionValue("CurCom", editCompanyName.Text, isEncrypt: true);
		}

		private void LoadRegistry()
		{
			textBoxDatabaseName.Text = Global.GetRegistryOptionValue("CurDB", isEncrypt: true);
			editCompanyName.Text = Global.GetRegistryOptionValue("CurCom", isEncrypt: true);
		}

		private bool CanConnect(bool suppressMessage, bool notify, bool validate)
		{
			bool flag = false;
			if (!suppressMessage && validate && validateForm && !IsValid(suppressMessage))
			{
				return false;
			}
			_ = Port;
			string currentInstanceName = Global.CurrentInstanceName;
			if (!GlobalRules.IsCorrectServerName(currentInstanceName.Trim()))
			{
				ErrorHelper.WarningMessage("Invalid server name.\nCannot connect to server: " + currentInstanceName.Trim());
				return flag;
			}
			try
			{
				Application.DoEvents();
				flag = Factory.SetLoginInfo(suppressMessage, base.ProductName, databaseName, editUserName.Text, GetEncyptedPassword());
				if (!flag)
				{
					return false;
				}
				if (!Factory.DatabaseSystem.IsCorrectDB())
				{
					flag = false;
					ErrorHelper.WarningMessage("This database is not a correct database.");
				}
				else if (!Factory.DatabaseSystem.IsCorrectDBVersion())
				{
					if (ErrorHelper.QuestionMessageYesNo("This database is an older version and require to upgrade. Do you want to upgrade it?") == DialogResult.Yes)
					{
						using (UpgradeDatabaseForm upgradeDatabaseForm = new UpgradeDatabaseForm())
						{
							upgradeDatabaseForm.DatabaseName = textBoxDatabaseName.Text;
							if (upgradeDatabaseForm.ShowDialog() != DialogResult.OK)
							{
								flag = false;
							}
						}
					}
					else
					{
						flag = false;
					}
				}
				if (flag && !Factory.DatabaseSystem.IsCorrectDBDataVersion())
				{
					flag &= Factory.DatabaseSystem.UpgradeDatabaseData(textBoxDatabaseName.Text, Global.CurrentUser, Global.CurrentPassword);
				}
				if (flag && Factory.DatabaseSystem.HasPendingDataPatches())
				{
					if (ErrorHelper.QuestionMessageYesNo("This database has some data updates that need to be applied. Do you want to update the data now?") == DialogResult.Yes)
					{
						if (Global.CurrentUser.ToLower() != "sa")
						{
							ErrorHelper.WarningMessage("You must login with 'sa' to perform this update.");
							flag = false;
						}
						else
						{
							using (UpgradeDBDataForm upgradeDBDataForm = new UpgradeDBDataForm())
							{
								upgradeDBDataForm.DatabaseName = textBoxDatabaseName.Text;
								if (upgradeDBDataForm.ShowDialog() != DialogResult.OK)
								{
									flag = false;
								}
							}
						}
					}
					else
					{
						flag = false;
					}
				}
			}
			catch (SqlException ex)
			{
				if (!suppressMessage)
				{
					ErrorHelper.ProcessError(ex);
				}
				flag = false;
			}
			catch (SocketException ex2)
			{
				if (!suppressMessage)
				{
					ErrorHelper.WarningMessage(ex2.Message, "Cannot connect server.");
				}
				SetEvents(isConnected: false, notify);
				flag = false;
			}
			catch (DBNotConnectedException)
			{
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
				if (validate)
				{
					try
					{
						if (!Global.IsCorrectDB())
						{
							return false;
						}
					}
					catch (DBNotConnectedException)
					{
					}
					catch (Exception e2)
					{
						ErrorHelper.ProcessError(e2);
						return false;
					}
				}
				if (notify && isEventConnection)
				{
					Global.Reset();
				}
				SaveRegistry();
				_ = isEventConnection;
				if (isEventConnection)
				{
					if (!Global.SetOpenCompany(databaseName.Trim()))
					{
						flag = false;
					}
					SetEvents(flag, notify);
				}
				if (companyName.Trim().Length > 0 && flag && isEventConnection)
				{
					Global.SetCompanyRegistryValue("LastDateAccessed", DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToLongTimeString());
					Global.SetCompanyRegistryValue("LastUserAccessed", editUserName.Text);
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
				buttonOK.Enabled = false;
				base.UseWaitCursor = true;
				Application.DoEvents();
				editUserName.Text = Global.CurrentUser;
				LoadRegistry();
				buttonOK.Enabled = true;
				buttonOK.Focus();
				isFirstActivated = false;
				base.UseWaitCursor = false;
				Application.DoEvents();
			}
		}

		private void DatabaseLoginForm_Load(object sender, EventArgs e)
		{
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
				return GotoCompany();
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

		private void buttonSelectDatabase_Click(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					DataSet databases = Factory.GetDatabases(Global.CurrentInstanceName);
					SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
					databases.Tables[0].Columns.Remove("Database_Size");
					databases.Tables[0].Columns.Remove("Remarks");
					databases.Tables[0].Columns["Database_Name"].ColumnName = "Database Name";
					databases.Tables[0].Columns["CompanyName"].ColumnName = "Company Name";
					for (int i = 0; i < databases.Tables[0].Columns.Count; i++)
					{
						if (databases.Tables[0].Columns[i].ColumnName != "Database Name" && databases.Tables[0].Columns[i].ColumnName != "Company Name")
						{
							databases.Tables[0].Columns.RemoveAt(i);
							i--;
						}
					}
					for (int j = 0; j < databases.Tables[0].Rows.Count; j++)
					{
						if (databases.Tables[0].Rows[j]["Company Name"].ToString() == "")
						{
							databases.Tables[0].Rows.RemoveAt(j);
							j--;
						}
					}
					selectDocumentDialog.Text = "Select Company";
					selectDocumentDialog.DataSource = databases;
					if (selectDocumentDialog.ShowDialog() == DialogResult.OK)
					{
						textBoxDatabaseName.Text = selectDocumentDialog.SelectedRow.Cells["Database Name"].Value.ToString();
						editCompanyName.Text = selectDocumentDialog.SelectedRow.Cells["Company Name"].Value.ToString();
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void changeUserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void newDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CurrentUser != "sa")
			{
				ErrorHelper.ErrorMessage("You must login with 'SA' user ID in order to create a new database.");
				return;
			}
			IsNewCompanyRequested = true;
			Close();
		}

		private void buttonOption_Click(object sender, EventArgs e)
		{
		}

		private void buttonOptions_Click(object sender, EventArgs e)
		{
			contextMenuOptions.Show(buttonOptions, new Point(0, buttonOptions.Height));
		}

		private void restoreDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CurrentUser != "sa")
			{
				ErrorHelper.ErrorMessage("You must login with 'SA' user ID in order to restore a new database.");
				return;
			}
			IsRestoreCompanyRequested = true;
			Close();
		}

		private void attachDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CurrentUser != "sa")
			{
				ErrorHelper.ErrorMessage("You must login with 'SA' user ID in order to attach a new database.");
				return;
			}
			IsAttachCompanyRequested = true;
			Close();
		}

		private void upgradeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new UpgradeDatabaseForm().ShowDialog();
		}
	}
}
