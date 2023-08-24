using Micromind.ClientLibraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.UISupport;
using Micromind.UISupport.Controls;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class ConnectionSettingsDialog : Form
	{
		private MMLabel lblName;

		private MMTextBox editServerName;

		private MMLabel lblPortNumber;

		private MMTextBox editUserName;

		private MMLabel lblUserName;

		private MMTextBox editPassword;

		private MMLabel lblPassword;

		private XPButton buttonCancel;

		private MMLabel lblDatabaseName;

		private MMTextBox editDatabaseName;

		private CheckBox checkBoxRememberPassword;

		private ToolTip toolTip;

		private ImageList imageList1;

		private OpenFileDialog openFileDialog;

		private XPButton buttonOpenFileDialog;

		private MMTextBox textBoxFileName;

		private MMLabel label3;

		private MMLabel label4;

		private MMLabel label5;

		private MMLabel label6;

		private MMTextBox editPortNumber;

		private MMLabel label2;

		private ContextMenu contextMenu;

		private MenuItem menuItemRemoveCompanyName;

		private Micromind.UISupport.Controls.TabPage tabServer;

		private Micromind.UISupport.Controls.TabPage tabDatabase;

		private Micromind.UISupport.Controls.TabPage tabOpenFromFile;

		private MMTextBox editCompanyName;

		private Line line1;

		private MMLabel label16;

		private MMTextBox textBoxInstanceName;

		private XPButton buttonSelectCheque;

		private MMLabel mmLabel1;

		private XPButton buttonOK;

		private RadioButton radioButtonSingle;

		private Panel panelSingle;

		private RadioButton radioButtonMulti;

		private Panel panelMulti;

		private MMTextBox textBoxPort;

		private MMLabel mmLabel3;

		private MMTextBox textBoxServerName;

		private MMLabel mmLabel2;

		private IContainer components;

		private ScreenAccessRight screenRight;

		public ConnectionSettingsDialog()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.ConnectionSettingsDialog));
			editPortNumber = new Micromind.UISupport.MMTextBox();
			label6 = new Micromind.UISupport.MMLabel();
			lblPortNumber = new Micromind.UISupport.MMLabel();
			editServerName = new Micromind.UISupport.MMTextBox();
			lblName = new Micromind.UISupport.MMLabel();
			checkBoxRememberPassword = new System.Windows.Forms.CheckBox();
			editDatabaseName = new Micromind.UISupport.MMTextBox();
			lblDatabaseName = new Micromind.UISupport.MMLabel();
			editPassword = new Micromind.UISupport.MMTextBox();
			lblPassword = new Micromind.UISupport.MMLabel();
			editUserName = new Micromind.UISupport.MMTextBox();
			lblUserName = new Micromind.UISupport.MMLabel();
			label2 = new Micromind.UISupport.MMLabel();
			label5 = new Micromind.UISupport.MMLabel();
			label4 = new Micromind.UISupport.MMLabel();
			buttonOpenFileDialog = new Micromind.UISupport.XPButton();
			textBoxFileName = new Micromind.UISupport.MMTextBox();
			label3 = new Micromind.UISupport.MMLabel();
			buttonCancel = new Micromind.UISupport.XPButton();
			contextMenu = new System.Windows.Forms.ContextMenu();
			menuItemRemoveCompanyName = new System.Windows.Forms.MenuItem();
			imageList1 = new System.Windows.Forms.ImageList(components);
			toolTip = new System.Windows.Forms.ToolTip(components);
			tabServer = new Micromind.UISupport.Controls.TabPage();
			tabDatabase = new Micromind.UISupport.Controls.TabPage();
			editCompanyName = new Micromind.UISupport.MMTextBox();
			tabOpenFromFile = new Micromind.UISupport.Controls.TabPage();
			line1 = new Micromind.UISupport.Line();
			label16 = new Micromind.UISupport.MMLabel();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			textBoxInstanceName = new Micromind.UISupport.MMTextBox();
			buttonSelectCheque = new Micromind.UISupport.XPButton();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			buttonOK = new Micromind.UISupport.XPButton();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			panelSingle = new System.Windows.Forms.Panel();
			radioButtonMulti = new System.Windows.Forms.RadioButton();
			panelMulti = new System.Windows.Forms.Panel();
			textBoxPort = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxServerName = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			panelSingle.SuspendLayout();
			panelMulti.SuspendLayout();
			SuspendLayout();
			editPortNumber.BackColor = System.Drawing.Color.White;
			editPortNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editPortNumber.CustomReportFieldName = "";
			editPortNumber.CustomReportKey = "";
			editPortNumber.CustomReportValueType = 1;
			editPortNumber.IsComboTextBox = false;
			editPortNumber.Location = new System.Drawing.Point(0, 0);
			editPortNumber.Name = "editPortNumber";
			editPortNumber.Size = new System.Drawing.Size(200, 13);
			editPortNumber.TabIndex = 0;
			editPortNumber.Text = "textBox1";
			label6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label6.IsFieldHeader = false;
			label6.IsRequired = false;
			label6.Location = new System.Drawing.Point(0, 0);
			label6.Name = "label6";
			label6.PenWidth = 1f;
			label6.ShowBorder = false;
			label6.Size = new System.Drawing.Size(100, 23);
			label6.TabIndex = 0;
			lblPortNumber.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblPortNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblPortNumber.IsFieldHeader = false;
			lblPortNumber.IsRequired = false;
			lblPortNumber.Location = new System.Drawing.Point(0, 0);
			lblPortNumber.Name = "lblPortNumber";
			lblPortNumber.PenWidth = 1f;
			lblPortNumber.ShowBorder = false;
			lblPortNumber.Size = new System.Drawing.Size(100, 23);
			lblPortNumber.TabIndex = 0;
			editServerName.BackColor = System.Drawing.Color.White;
			editServerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editServerName.CustomReportFieldName = "";
			editServerName.CustomReportKey = "";
			editServerName.CustomReportValueType = 1;
			editServerName.IsComboTextBox = false;
			editServerName.Location = new System.Drawing.Point(0, 0);
			editServerName.Name = "editServerName";
			editServerName.Size = new System.Drawing.Size(200, 13);
			editServerName.TabIndex = 0;
			editServerName.Text = "textBox1";
			lblName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblName.IsFieldHeader = false;
			lblName.IsRequired = false;
			lblName.Location = new System.Drawing.Point(0, 0);
			lblName.Name = "lblName";
			lblName.PenWidth = 1f;
			lblName.ShowBorder = false;
			lblName.Size = new System.Drawing.Size(100, 23);
			lblName.TabIndex = 0;
			checkBoxRememberPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxRememberPassword.Location = new System.Drawing.Point(0, 0);
			checkBoxRememberPassword.Name = "checkBoxRememberPassword";
			checkBoxRememberPassword.Size = new System.Drawing.Size(104, 24);
			checkBoxRememberPassword.TabIndex = 0;
			editDatabaseName.BackColor = System.Drawing.Color.White;
			editDatabaseName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editDatabaseName.CustomReportFieldName = "";
			editDatabaseName.CustomReportKey = "";
			editDatabaseName.CustomReportValueType = 1;
			editDatabaseName.IsComboTextBox = false;
			editDatabaseName.Location = new System.Drawing.Point(0, 0);
			editDatabaseName.Name = "editDatabaseName";
			editDatabaseName.Size = new System.Drawing.Size(200, 13);
			editDatabaseName.TabIndex = 0;
			editDatabaseName.Text = "textBox1";
			lblDatabaseName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDatabaseName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDatabaseName.IsFieldHeader = false;
			lblDatabaseName.IsRequired = false;
			lblDatabaseName.Location = new System.Drawing.Point(0, 0);
			lblDatabaseName.Name = "lblDatabaseName";
			lblDatabaseName.PenWidth = 1f;
			lblDatabaseName.ShowBorder = false;
			lblDatabaseName.Size = new System.Drawing.Size(100, 23);
			lblDatabaseName.TabIndex = 0;
			editPassword.BackColor = System.Drawing.Color.White;
			editPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editPassword.CustomReportFieldName = "";
			editPassword.CustomReportKey = "";
			editPassword.CustomReportValueType = 1;
			editPassword.IsComboTextBox = false;
			editPassword.Location = new System.Drawing.Point(0, 0);
			editPassword.Name = "editPassword";
			editPassword.Size = new System.Drawing.Size(200, 13);
			editPassword.TabIndex = 0;
			editPassword.Text = "textBox1";
			lblPassword.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblPassword.IsFieldHeader = false;
			lblPassword.IsRequired = false;
			lblPassword.Location = new System.Drawing.Point(0, 0);
			lblPassword.Name = "lblPassword";
			lblPassword.PenWidth = 1f;
			lblPassword.ShowBorder = false;
			lblPassword.Size = new System.Drawing.Size(100, 23);
			lblPassword.TabIndex = 0;
			editUserName.BackColor = System.Drawing.Color.White;
			editUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editUserName.CustomReportFieldName = "";
			editUserName.CustomReportKey = "";
			editUserName.CustomReportValueType = 1;
			editUserName.IsComboTextBox = false;
			editUserName.Location = new System.Drawing.Point(0, 0);
			editUserName.Name = "editUserName";
			editUserName.Size = new System.Drawing.Size(200, 13);
			editUserName.TabIndex = 0;
			editUserName.Text = "textBox1";
			lblUserName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblUserName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblUserName.IsFieldHeader = false;
			lblUserName.IsRequired = false;
			lblUserName.Location = new System.Drawing.Point(0, 0);
			lblUserName.Name = "lblUserName";
			lblUserName.PenWidth = 1f;
			lblUserName.ShowBorder = false;
			lblUserName.Size = new System.Drawing.Size(100, 23);
			lblUserName.TabIndex = 0;
			label2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label2.IsFieldHeader = false;
			label2.IsRequired = false;
			label2.Location = new System.Drawing.Point(0, 0);
			label2.Name = "label2";
			label2.PenWidth = 1f;
			label2.ShowBorder = false;
			label2.Size = new System.Drawing.Size(100, 23);
			label2.TabIndex = 0;
			label5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label5.IsFieldHeader = false;
			label5.IsRequired = false;
			label5.Location = new System.Drawing.Point(0, 0);
			label5.Name = "label5";
			label5.PenWidth = 1f;
			label5.ShowBorder = false;
			label5.Size = new System.Drawing.Size(100, 23);
			label5.TabIndex = 0;
			label4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label4.IsFieldHeader = false;
			label4.IsRequired = false;
			label4.Location = new System.Drawing.Point(0, 0);
			label4.Name = "label4";
			label4.PenWidth = 1f;
			label4.ShowBorder = false;
			label4.Size = new System.Drawing.Size(100, 23);
			label4.TabIndex = 0;
			buttonOpenFileDialog.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpenFileDialog.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpenFileDialog.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpenFileDialog.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOpenFileDialog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpenFileDialog.Location = new System.Drawing.Point(0, 0);
			buttonOpenFileDialog.Name = "buttonOpenFileDialog";
			buttonOpenFileDialog.Size = new System.Drawing.Size(75, 23);
			buttonOpenFileDialog.TabIndex = 0;
			textBoxFileName.BackColor = System.Drawing.Color.White;
			textBoxFileName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxFileName.CustomReportFieldName = "";
			textBoxFileName.CustomReportKey = "";
			textBoxFileName.CustomReportValueType = 1;
			textBoxFileName.IsComboTextBox = false;
			textBoxFileName.Location = new System.Drawing.Point(0, 0);
			textBoxFileName.Name = "textBoxFileName";
			textBoxFileName.Size = new System.Drawing.Size(200, 13);
			textBoxFileName.TabIndex = 0;
			textBoxFileName.Text = "textBox1";
			label3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label3.IsFieldHeader = false;
			label3.IsRequired = false;
			label3.Location = new System.Drawing.Point(0, 0);
			label3.Name = "label3";
			label3.PenWidth = 1f;
			label3.ShowBorder = false;
			label3.Size = new System.Drawing.Size(100, 23);
			label3.TabIndex = 0;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ForeColor = System.Drawing.Color.Black;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(304, 245);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(81, 24);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[1]
			{
				menuItemRemoveCompanyName
			});
			menuItemRemoveCompanyName.Index = 0;
			menuItemRemoveCompanyName.Text = "Remove";
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "");
			tabServer.Location = new System.Drawing.Point(0, 0);
			tabServer.Name = "tabServer";
			tabServer.Selected = false;
			tabServer.Size = new System.Drawing.Size(200, 100);
			tabServer.TabCellWidth = 0;
			tabServer.TabIndex = 0;
			tabServer.TabVisible = true;
			tabDatabase.Location = new System.Drawing.Point(0, 0);
			tabDatabase.Name = "tabDatabase";
			tabDatabase.Selected = false;
			tabDatabase.Size = new System.Drawing.Size(200, 100);
			tabDatabase.TabCellWidth = 0;
			tabDatabase.TabIndex = 0;
			tabDatabase.TabVisible = true;
			editCompanyName.BackColor = System.Drawing.Color.White;
			editCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editCompanyName.CustomReportFieldName = "";
			editCompanyName.CustomReportKey = "";
			editCompanyName.CustomReportValueType = 1;
			editCompanyName.IsComboTextBox = false;
			editCompanyName.Location = new System.Drawing.Point(0, 0);
			editCompanyName.Name = "editCompanyName";
			editCompanyName.Size = new System.Drawing.Size(200, 13);
			editCompanyName.TabIndex = 0;
			editCompanyName.Text = "textBox1";
			tabOpenFromFile.Location = new System.Drawing.Point(0, 0);
			tabOpenFromFile.Name = "tabOpenFromFile";
			tabOpenFromFile.Selected = false;
			tabOpenFromFile.Size = new System.Drawing.Size(200, 100);
			tabOpenFromFile.TabCellWidth = 0;
			tabOpenFromFile.TabIndex = 0;
			tabOpenFromFile.TabVisible = true;
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.Font = new System.Drawing.Font("Tahoma", 8.25f);
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.LightSteelBlue;
			line1.Location = new System.Drawing.Point(-6, 238);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(467, 1);
			line1.TabIndex = 283;
			line1.TabStop = false;
			label16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label16.IsFieldHeader = false;
			label16.IsRequired = false;
			label16.Location = new System.Drawing.Point(16, 8);
			label16.Name = "label16";
			label16.PenWidth = 1f;
			label16.ShowBorder = false;
			label16.Size = new System.Drawing.Size(357, 23);
			label16.TabIndex = 287;
			label16.Text = "Enter the SQL Server address where your database exists:";
			textBoxInstanceName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxInstanceName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxInstanceName.BackColor = System.Drawing.Color.White;
			textBoxInstanceName.CustomReportFieldName = "";
			textBoxInstanceName.CustomReportKey = "";
			textBoxInstanceName.CustomReportValueType = 1;
			textBoxInstanceName.IsComboTextBox = false;
			textBoxInstanceName.IsRequired = true;
			textBoxInstanceName.Location = new System.Drawing.Point(60, 7);
			textBoxInstanceName.Name = "textBoxInstanceName";
			textBoxInstanceName.Size = new System.Drawing.Size(283, 20);
			textBoxInstanceName.TabIndex = 0;
			textBoxInstanceName.Text = "localhost\\sqlexpress";
			buttonSelectCheque.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectCheque.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectCheque.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectCheque.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectCheque.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSelectCheque.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSelectCheque.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectCheque.Location = new System.Drawing.Point(343, 6);
			buttonSelectCheque.Name = "buttonSelectCheque";
			buttonSelectCheque.Size = new System.Drawing.Size(25, 22);
			buttonSelectCheque.TabIndex = 1;
			buttonSelectCheque.Text = "...";
			buttonSelectCheque.UseVisualStyleBackColor = false;
			buttonSelectCheque.Click += new System.EventHandler(buttonSelectCheque_Click);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(11, 9);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(43, 13);
			mmLabel1.TabIndex = 289;
			mmLabel1.Text = "Server:";
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOK.ForeColor = System.Drawing.Color.Black;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(217, 245);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(81, 24);
			buttonOK.TabIndex = 2;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Checked = true;
			radioButtonSingle.Location = new System.Drawing.Point(12, 34);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(131, 17);
			radioButtonSingle.TabIndex = 290;
			radioButtonSingle.TabStop = true;
			radioButtonSingle.Text = "Single user application";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtonSingle_CheckedChanged);
			panelSingle.Controls.Add(textBoxInstanceName);
			panelSingle.Controls.Add(buttonSelectCheque);
			panelSingle.Controls.Add(mmLabel1);
			panelSingle.Location = new System.Drawing.Point(17, 50);
			panelSingle.Name = "panelSingle";
			panelSingle.Size = new System.Drawing.Size(392, 44);
			panelSingle.TabIndex = 291;
			radioButtonMulti.AutoSize = true;
			radioButtonMulti.Location = new System.Drawing.Point(12, 100);
			radioButtonMulti.Name = "radioButtonMulti";
			radioButtonMulti.Size = new System.Drawing.Size(265, 17);
			radioButtonMulti.TabIndex = 290;
			radioButtonMulti.Text = "Multi user application. Connect to a remote server";
			radioButtonMulti.UseVisualStyleBackColor = true;
			radioButtonMulti.CheckedChanged += new System.EventHandler(radioButtonMulti_CheckedChanged);
			panelMulti.Controls.Add(textBoxPort);
			panelMulti.Controls.Add(mmLabel3);
			panelMulti.Controls.Add(textBoxServerName);
			panelMulti.Controls.Add(mmLabel2);
			panelMulti.Enabled = false;
			panelMulti.Location = new System.Drawing.Point(17, 123);
			panelMulti.Name = "panelMulti";
			panelMulti.Size = new System.Drawing.Size(392, 70);
			panelMulti.TabIndex = 291;
			textBoxPort.BackColor = System.Drawing.Color.White;
			textBoxPort.CustomReportFieldName = "";
			textBoxPort.CustomReportKey = "";
			textBoxPort.CustomReportValueType = 1;
			textBoxPort.IsComboTextBox = false;
			textBoxPort.IsRequired = true;
			textBoxPort.Location = new System.Drawing.Point(60, 29);
			textBoxPort.Name = "textBoxPort";
			textBoxPort.Size = new System.Drawing.Size(82, 20);
			textBoxPort.TabIndex = 0;
			textBoxPort.Text = "6000";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(11, 31);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(31, 13);
			mmLabel3.TabIndex = 289;
			mmLabel3.Text = "Port:";
			textBoxServerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxServerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxServerName.BackColor = System.Drawing.Color.White;
			textBoxServerName.CustomReportFieldName = "";
			textBoxServerName.CustomReportKey = "";
			textBoxServerName.CustomReportValueType = 1;
			textBoxServerName.IsComboTextBox = false;
			textBoxServerName.IsRequired = true;
			textBoxServerName.Location = new System.Drawing.Point(60, 7);
			textBoxServerName.Name = "textBoxServerName";
			textBoxServerName.Size = new System.Drawing.Size(283, 20);
			textBoxServerName.TabIndex = 0;
			textBoxServerName.Text = "localhost\\Axolon";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(11, 9);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(43, 13);
			mmLabel2.TabIndex = 289;
			mmLabel2.Text = "Server:";
			base.AcceptButton = buttonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(400, 281);
			base.Controls.Add(panelMulti);
			base.Controls.Add(panelSingle);
			base.Controls.Add(radioButtonMulti);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(buttonOK);
			base.Controls.Add(label16);
			base.Controls.Add(line1);
			base.Controls.Add(buttonCancel);
			Font = new System.Drawing.Font("Tahoma", 8f);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ConnectionSettingsDialog";
			base.ShowIcon = false;
			Text = "Database Connection Settings";
			base.Load += new System.EventHandler(DataUtilitiesDialog_Load);
			panelSingle.ResumeLayout(false);
			panelSingle.PerformLayout();
			panelMulti.ResumeLayout(false);
			panelMulti.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		private void DataUtilitiesDialog_Load(object sender, EventArgs e)
		{
			try
			{
				base.DialogResult = DialogResult.None;
				LoadRegistry();
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					editPassword.PasswordChar = '●';
				}
				else
				{
					editPassword.PasswordChar = '*';
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
				Dispose();
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private bool IsValid()
		{
			return true;
		}

		private void SaveRegistry()
		{
			if (textBoxInstanceName.Text.Trim() != string.Empty)
			{
				Global.CurrentInstanceName = textBoxInstanceName.Text;
				Global.IsMultiUser = radioButtonMulti.Checked;
				Global.CurrentServerName = textBoxServerName.Text;
				Global.PortNumber = int.Parse(textBoxPort.Text);
			}
		}

		private void LoadRegistry()
		{
			textBoxInstanceName.Text = Global.SingleUserInstanceName;
			textBoxServerName.Text = Global.CurrentServerName;
			textBoxPort.Text = Global.PortNumber.ToString();
			radioButtonMulti.Checked = Global.IsMultiUser;
		}

		private void tabControl1_SelectionChanged(object sender, EventArgs e)
		{
		}

		private void textBoxInstanceName_Validated(object sender, EventArgs e)
		{
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (IsValid())
				{
					SaveRegistry();
					Close();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.WarningMessage("Unable to save settings.");
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonSelectCheque_Click(object sender, EventArgs e)
		{
			try
			{
				DataTable dataTable = SmoApplication.EnumAvailableSqlServers(localOnly: false);
				dataTable.Columns.Remove("IsClustered");
				dataTable.Columns.Remove("IsLocal");
				dataTable.Columns.Remove("Instance");
				DataSet dataSet = new DataSet();
				dataSet.Tables.Add(dataTable);
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = dataSet;
				selectDocumentDialog.Text = "Select SQL Sever";
				base.DialogResult = DialogResult.None;
				if (selectDocumentDialog.ShowDialog() == DialogResult.OK)
				{
					textBoxInstanceName.Text = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void radioButtonMulti_CheckedChanged(object sender, EventArgs e)
		{
			panelMulti.Enabled = radioButtonMulti.Checked;
		}

		private void radioButtonSingle_CheckedChanged(object sender, EventArgs e)
		{
			panelSingle.Enabled = radioButtonSingle.Checked;
		}
	}
}
