using DevExpress.XtraWizard;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using Microsoft.SqlServer.Dac;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class UpgradeDatabaseForm : Form, IForm
	{
		private CompanyDatabase database = new CompanyDatabase();

		private OpenFileDialog openFileDialog;

		private System.Windows.Forms.ToolTip toolTip;

		private WizardControl wizardControl1;

		private WelcomeWizardPage wizardWelcomePage;

		private WizardPage wizardUpgradeFilePage;

		private CompletionWizardPage wizardCompletionPage;

		private MMTextBox editFileName;

		private MMLabel label1;

		private XPButton buttonOpenFileDialog;

		private WizardPage wizardSelectDatabasePage;

		private Panel panelOverwrite;

		private DatabaseComboBox comboBoxDatabase;

		private MMLabel mmLabel1;

		private MMLabel label8;

		private MMTextBox editCompanyName;

		private WizardPage wizardUpgradeSummaryPage;

		private MMLabel mmLabel2;

		private MMTextBox txtSelectedUpgradeFile;

		private MMLabel lblName;

		private MMTextBox txtName;

		private MMLabel mmLabel3;

		private MMTextBox txtDatabaseName;

		private MMLabel lblRestoreMessage;

		private ImageList imageList;

		private MMLabel mmLabel4;

		private MMTextBox textBoxVersion;

		private MMLabel mmLabel6;

		private MMTextBox textBoxNewVersion;

		private MMLabel mmLabel5;

		private MMTextBox textBoxCurrentVersion;

		private Line line1;

		private MMTextBox editPassword;

		private MMLabel mmLabel7;

		private MMTextBox editUserName;

		private MMLabel label2;

		private MMLabel mmLabel8;

		private CheckBox checkBoxBackup;

		private PictureBox pictureBox1;

		private MMLabel mmLabel9;

		private MMTextBox textBoxRequiredVersion;

		private IContainer components;

		private ScreenAccessRight screenRight;

		public ScreenAreas ScreenArea => ScreenAreas.Company;

		public string DatabaseName
		{
			get
			{
				return comboBoxDatabase.SelectedID;
			}
			set
			{
				comboBoxDatabase.LoadData();
				comboBoxDatabase.SelectedID = value;
			}
		}

		public int ScreenID => 8005;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		public CompanyDatabase Database => database;

		public UpgradeDatabaseForm()
		{
			InitializeComponent();
			comboBoxDatabase.SelectedIndexChanged += comboBoxDatabase_SelectedIndexChanged;
			openFileDialog.FileOk += openFileDialog_FileOk;
		}

		private void openFileDialog_FileOk(object sender, CancelEventArgs e)
		{
		}

		public UpgradeDatabaseForm(Form form)
		{
			InitializeComponent();
			comboBoxDatabase.SelectedIndexChanged += comboBoxDatabase_SelectedIndexChanged;
		}

		private void comboBoxDatabase_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxVersion.Text = comboBoxDatabase.SelectedVersion;
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
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			toolTip = new System.Windows.Forms.ToolTip(components);
			wizardControl1 = new DevExpress.XtraWizard.WizardControl();
			wizardWelcomePage = new DevExpress.XtraWizard.WelcomeWizardPage();
			wizardUpgradeFilePage = new DevExpress.XtraWizard.WizardPage();
			editFileName = new Micromind.UISupport.MMTextBox();
			label1 = new Micromind.UISupport.MMLabel();
			buttonOpenFileDialog = new Micromind.UISupport.XPButton();
			wizardCompletionPage = new DevExpress.XtraWizard.CompletionWizardPage();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			lblRestoreMessage = new Micromind.UISupport.MMLabel();
			wizardSelectDatabasePage = new DevExpress.XtraWizard.WizardPage();
			panelOverwrite = new System.Windows.Forms.Panel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxVersion = new Micromind.UISupport.MMTextBox();
			comboBoxDatabase = new Micromind.DataControls.DatabaseComboBox();
			editCompanyName = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			label8 = new Micromind.UISupport.MMLabel();
			wizardUpgradeSummaryPage = new DevExpress.XtraWizard.WizardPage();
			checkBoxBackup = new System.Windows.Forms.CheckBox();
			line1 = new Micromind.UISupport.Line();
			editPassword = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			editUserName = new Micromind.UISupport.MMTextBox();
			label2 = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxNewVersion = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxCurrentVersion = new Micromind.UISupport.MMTextBox();
			lblName = new Micromind.UISupport.MMLabel();
			txtName = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			txtDatabaseName = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			txtSelectedUpgradeFile = new Micromind.UISupport.MMTextBox();
			imageList = new System.Windows.Forms.ImageList(components);
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxRequiredVersion = new Micromind.UISupport.MMTextBox();
			((System.ComponentModel.ISupportInitialize)wizardControl1).BeginInit();
			wizardControl1.SuspendLayout();
			wizardUpgradeFilePage.SuspendLayout();
			wizardCompletionPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			wizardSelectDatabasePage.SuspendLayout();
			panelOverwrite.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDatabase).BeginInit();
			wizardUpgradeSummaryPage.SuspendLayout();
			SuspendLayout();
			wizardControl1.Appearance.Page.BackColor = System.Drawing.Color.Transparent;
			wizardControl1.Appearance.Page.BackColor2 = System.Drawing.Color.Transparent;
			wizardControl1.Appearance.Page.Options.UseBackColor = true;
			wizardControl1.Controls.Add(wizardWelcomePage);
			wizardControl1.Controls.Add(wizardUpgradeFilePage);
			wizardControl1.Controls.Add(wizardCompletionPage);
			wizardControl1.Controls.Add(wizardSelectDatabasePage);
			wizardControl1.Controls.Add(wizardUpgradeSummaryPage);
			wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			wizardControl1.ImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			wizardControl1.Location = new System.Drawing.Point(0, 0);
			wizardControl1.Name = "wizardControl1";
			wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[5]
			{
				wizardWelcomePage,
				wizardSelectDatabasePage,
				wizardUpgradeFilePage,
				wizardUpgradeSummaryPage,
				wizardCompletionPage
			});
			wizardControl1.Size = new System.Drawing.Size(597, 384);
			wizardControl1.UseCancelButton = false;
			wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(wizardControl1_CancelClick);
			wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(wizardControl1_FinishClick);
			wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(wizardControl1_NextClick);
			wizardControl1.CustomizeCommandButtons += new DevExpress.XtraWizard.WizardCustomizeCommandButtonsEventHandler(wizardControl1_CustomizeCommandButtons);
			wizardWelcomePage.IntroductionText = "This wizard helps you to restore your database";
			wizardWelcomePage.Name = "wizardWelcomePage";
			wizardWelcomePage.Size = new System.Drawing.Size(380, 251);
			wizardWelcomePage.Visible = false;
			wizardUpgradeFilePage.Controls.Add(editFileName);
			wizardUpgradeFilePage.Controls.Add(label1);
			wizardUpgradeFilePage.Controls.Add(buttonOpenFileDialog);
			wizardUpgradeFilePage.DescriptionText = "Select the latest database upgrade file";
			wizardUpgradeFilePage.Name = "wizardUpgradeFilePage";
			wizardUpgradeFilePage.Size = new System.Drawing.Size(565, 239);
			wizardUpgradeFilePage.Text = "Database Upgrade File";
			editFileName.BackColor = System.Drawing.Color.WhiteSmoke;
			editFileName.IsComboTextBox = false;
			editFileName.Location = new System.Drawing.Point(26, 68);
			editFileName.MaxLength = 1000;
			editFileName.Name = "editFileName";
			editFileName.ReadOnly = true;
			editFileName.Size = new System.Drawing.Size(470, 20);
			editFileName.TabIndex = 20;
			label1.AutoSize = true;
			label1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			label1.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.IsFieldHeader = false;
			label1.IsRequired = false;
			label1.Location = new System.Drawing.Point(23, 52);
			label1.Name = "label1";
			label1.PenWidth = 1f;
			label1.ShowBorder = false;
			label1.Size = new System.Drawing.Size(116, 13);
			label1.TabIndex = 22;
			label1.Text = "Select the upgrade file:";
			buttonOpenFileDialog.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpenFileDialog.BackColor = System.Drawing.Color.DarkGray;
			buttonOpenFileDialog.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpenFileDialog.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpenFileDialog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpenFileDialog.Location = new System.Drawing.Point(496, 67);
			buttonOpenFileDialog.Name = "buttonOpenFileDialog";
			buttonOpenFileDialog.Size = new System.Drawing.Size(25, 22);
			buttonOpenFileDialog.TabIndex = 21;
			buttonOpenFileDialog.Text = "...";
			buttonOpenFileDialog.UseVisualStyleBackColor = false;
			buttonOpenFileDialog.Click += new System.EventHandler(buttonOpenFileDialog_Click);
			wizardCompletionPage.Controls.Add(pictureBox1);
			wizardCompletionPage.Controls.Add(lblRestoreMessage);
			wizardCompletionPage.FinishText = "";
			wizardCompletionPage.Name = "wizardCompletionPage";
			wizardCompletionPage.Size = new System.Drawing.Size(380, 251);
			pictureBox1.Image = Micromind.ClientUI.Properties.Resources.completed;
			pictureBox1.Location = new System.Drawing.Point(25, 63);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(20, 19);
			pictureBox1.TabIndex = 27;
			pictureBox1.TabStop = false;
			lblRestoreMessage.AutoSize = true;
			lblRestoreMessage.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblRestoreMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblRestoreMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f);
			lblRestoreMessage.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			lblRestoreMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblRestoreMessage.IsFieldHeader = false;
			lblRestoreMessage.IsRequired = false;
			lblRestoreMessage.Location = new System.Drawing.Point(46, 63);
			lblRestoreMessage.Name = "lblRestoreMessage";
			lblRestoreMessage.PenWidth = 1f;
			lblRestoreMessage.ShowBorder = false;
			lblRestoreMessage.Size = new System.Drawing.Size(65, 17);
			lblRestoreMessage.TabIndex = 26;
			lblRestoreMessage.Text = "Message";
			lblRestoreMessage.Visible = false;
			wizardSelectDatabasePage.Controls.Add(panelOverwrite);
			wizardSelectDatabasePage.DescriptionText = "Select database you would like to upgrade";
			wizardSelectDatabasePage.Name = "wizardSelectDatabasePage";
			wizardSelectDatabasePage.Size = new System.Drawing.Size(565, 239);
			wizardSelectDatabasePage.Text = "Database Restore Options";
			panelOverwrite.Controls.Add(mmLabel4);
			panelOverwrite.Controls.Add(textBoxVersion);
			panelOverwrite.Controls.Add(comboBoxDatabase);
			panelOverwrite.Controls.Add(mmLabel1);
			panelOverwrite.Controls.Add(editCompanyName);
			panelOverwrite.Controls.Add(label8);
			panelOverwrite.Location = new System.Drawing.Point(15, 37);
			panelOverwrite.Name = "panelOverwrite";
			panelOverwrite.Size = new System.Drawing.Size(514, 93);
			panelOverwrite.TabIndex = 5;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(8, 53);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(82, 13);
			mmLabel4.TabIndex = 104;
			mmLabel4.Text = "Current Version:";
			textBoxVersion.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVersion.IsComboTextBox = false;
			textBoxVersion.Location = new System.Drawing.Point(107, 51);
			textBoxVersion.MaxLength = 20;
			textBoxVersion.Name = "textBoxVersion";
			textBoxVersion.ReadOnly = true;
			textBoxVersion.Size = new System.Drawing.Size(307, 20);
			textBoxVersion.TabIndex = 2;
			textBoxVersion.TabStop = false;
			comboBoxDatabase.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDatabase.DescriptionTextBox = editCompanyName;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDatabase.DisplayLayout.Appearance = appearance;
			comboBoxDatabase.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDatabase.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDatabase.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDatabase.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxDatabase.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDatabase.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxDatabase.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDatabase.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDatabase.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDatabase.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxDatabase.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDatabase.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDatabase.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDatabase.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxDatabase.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDatabase.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDatabase.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxDatabase.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxDatabase.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDatabase.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxDatabase.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxDatabase.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDatabase.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxDatabase.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDatabase.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDatabase.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDatabase.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDatabase.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxDatabase.Editable = true;
			comboBoxDatabase.FilterString = "";
			comboBoxDatabase.HasAllAccount = false;
			comboBoxDatabase.HasCustom = false;
			comboBoxDatabase.Location = new System.Drawing.Point(107, 5);
			comboBoxDatabase.MaxDropDownItems = 12;
			comboBoxDatabase.Name = "comboBoxDatabase";
			comboBoxDatabase.ShowInactiveItems = false;
			comboBoxDatabase.ShowQuickAdd = true;
			comboBoxDatabase.Size = new System.Drawing.Size(307, 20);
			comboBoxDatabase.TabIndex = 0;
			comboBoxDatabase.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			editCompanyName.BackColor = System.Drawing.Color.WhiteSmoke;
			editCompanyName.IsComboTextBox = false;
			editCompanyName.Location = new System.Drawing.Point(107, 27);
			editCompanyName.MaxLength = 20;
			editCompanyName.Name = "editCompanyName";
			editCompanyName.ReadOnly = true;
			editCompanyName.Size = new System.Drawing.Size(307, 20);
			editCompanyName.TabIndex = 1;
			editCompanyName.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(8, 29);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(85, 13);
			mmLabel1.TabIndex = 101;
			mmLabel1.Text = "Company Name:";
			label8.AutoSize = true;
			label8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label8.IsFieldHeader = false;
			label8.IsRequired = false;
			label8.Location = new System.Drawing.Point(8, 8);
			label8.Name = "label8";
			label8.PenWidth = 1f;
			label8.ShowBorder = false;
			label8.Size = new System.Drawing.Size(87, 13);
			label8.TabIndex = 102;
			label8.Text = "Database Name:";
			wizardUpgradeSummaryPage.Controls.Add(mmLabel9);
			wizardUpgradeSummaryPage.Controls.Add(textBoxRequiredVersion);
			wizardUpgradeSummaryPage.Controls.Add(checkBoxBackup);
			wizardUpgradeSummaryPage.Controls.Add(line1);
			wizardUpgradeSummaryPage.Controls.Add(editPassword);
			wizardUpgradeSummaryPage.Controls.Add(mmLabel7);
			wizardUpgradeSummaryPage.Controls.Add(editUserName);
			wizardUpgradeSummaryPage.Controls.Add(label2);
			wizardUpgradeSummaryPage.Controls.Add(mmLabel6);
			wizardUpgradeSummaryPage.Controls.Add(textBoxNewVersion);
			wizardUpgradeSummaryPage.Controls.Add(mmLabel8);
			wizardUpgradeSummaryPage.Controls.Add(mmLabel5);
			wizardUpgradeSummaryPage.Controls.Add(textBoxCurrentVersion);
			wizardUpgradeSummaryPage.Controls.Add(lblName);
			wizardUpgradeSummaryPage.Controls.Add(txtName);
			wizardUpgradeSummaryPage.Controls.Add(mmLabel3);
			wizardUpgradeSummaryPage.Controls.Add(txtDatabaseName);
			wizardUpgradeSummaryPage.Controls.Add(mmLabel2);
			wizardUpgradeSummaryPage.Controls.Add(txtSelectedUpgradeFile);
			wizardUpgradeSummaryPage.DescriptionText = "Please review and verify your options then click Restore";
			wizardUpgradeSummaryPage.Name = "wizardUpgradeSummaryPage";
			wizardUpgradeSummaryPage.Size = new System.Drawing.Size(565, 239);
			wizardUpgradeSummaryPage.Text = "Upgrade Database Review Summary";
			checkBoxBackup.AutoSize = true;
			checkBoxBackup.Checked = true;
			checkBoxBackup.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxBackup.Location = new System.Drawing.Point(16, 162);
			checkBoxBackup.Name = "checkBoxBackup";
			checkBoxBackup.Size = new System.Drawing.Size(193, 17);
			checkBoxBackup.TabIndex = 37;
			checkBoxBackup.Text = "Backup database before upgrading";
			checkBoxBackup.UseVisualStyleBackColor = true;
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(7, 148);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(547, 1);
			line1.TabIndex = 36;
			line1.TabStop = false;
			editPassword.BackColor = System.Drawing.Color.White;
			editPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			editPassword.IsComboTextBox = false;
			editPassword.Location = new System.Drawing.Point(364, 211);
			editPassword.Name = "editPassword";
			editPassword.Size = new System.Drawing.Size(169, 20);
			editPassword.TabIndex = 35;
			editPassword.UseSystemPasswordChar = true;
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(302, 213);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(56, 13);
			mmLabel7.TabIndex = 34;
			mmLabel7.Text = "&Password:";
			editUserName.BackColor = System.Drawing.Color.White;
			editUserName.IsComboTextBox = false;
			editUserName.IsRequired = true;
			editUserName.Location = new System.Drawing.Point(364, 188);
			editUserName.Name = "editUserName";
			editUserName.Size = new System.Drawing.Size(169, 20);
			editUserName.TabIndex = 33;
			editUserName.Text = "sa";
			label2.AutoSize = true;
			label2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label2.IsFieldHeader = false;
			label2.IsRequired = false;
			label2.Location = new System.Drawing.Point(302, 192);
			label2.Name = "label2";
			label2.PenWidth = 1f;
			label2.ShowBorder = false;
			label2.Size = new System.Drawing.Size(46, 13);
			label2.TabIndex = 32;
			label2.Text = "&User ID:";
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(386, 120);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(70, 13);
			mmLabel6.TabIndex = 31;
			mmLabel6.Text = "New Version:";
			textBoxNewVersion.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxNewVersion.IsComboTextBox = false;
			textBoxNewVersion.Location = new System.Drawing.Point(458, 117);
			textBoxNewVersion.MaxLength = 1000;
			textBoxNewVersion.Name = "textBoxNewVersion";
			textBoxNewVersion.ReadOnly = true;
			textBoxNewVersion.Size = new System.Drawing.Size(95, 20);
			textBoxNewVersion.TabIndex = 5;
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(302, 162);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(154, 13);
			mmLabel8.TabIndex = 29;
			mmLabel8.Text = "Administrator Login Information:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(13, 120);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(82, 13);
			mmLabel5.TabIndex = 29;
			mmLabel5.Text = "Current Version:";
			textBoxCurrentVersion.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCurrentVersion.IsComboTextBox = false;
			textBoxCurrentVersion.Location = new System.Drawing.Point(106, 117);
			textBoxCurrentVersion.MaxLength = 1000;
			textBoxCurrentVersion.Name = "textBoxCurrentVersion";
			textBoxCurrentVersion.ReadOnly = true;
			textBoxCurrentVersion.Size = new System.Drawing.Size(89, 20);
			textBoxCurrentVersion.TabIndex = 3;
			lblName.AutoSize = true;
			lblName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblName.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			lblName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblName.IsFieldHeader = false;
			lblName.IsRequired = false;
			lblName.Location = new System.Drawing.Point(13, 80);
			lblName.Name = "lblName";
			lblName.PenWidth = 1f;
			lblName.ShowBorder = false;
			lblName.Size = new System.Drawing.Size(38, 13);
			lblName.TabIndex = 27;
			lblName.Text = "Name:";
			txtName.BackColor = System.Drawing.Color.WhiteSmoke;
			txtName.IsComboTextBox = false;
			txtName.Location = new System.Drawing.Point(106, 77);
			txtName.MaxLength = 1000;
			txtName.Name = "txtName";
			txtName.ReadOnly = true;
			txtName.Size = new System.Drawing.Size(323, 20);
			txtName.TabIndex = 2;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(13, 55);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(87, 13);
			mmLabel3.TabIndex = 25;
			mmLabel3.Text = "Database Name:";
			txtDatabaseName.BackColor = System.Drawing.Color.WhiteSmoke;
			txtDatabaseName.IsComboTextBox = false;
			txtDatabaseName.Location = new System.Drawing.Point(106, 52);
			txtDatabaseName.MaxLength = 1000;
			txtDatabaseName.Name = "txtDatabaseName";
			txtDatabaseName.ReadOnly = true;
			txtDatabaseName.Size = new System.Drawing.Size(323, 20);
			txtDatabaseName.TabIndex = 1;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(13, 5);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(134, 13);
			mmLabel2.TabIndex = 23;
			mmLabel2.Text = "Selected upgrade file path:";
			txtSelectedUpgradeFile.BackColor = System.Drawing.Color.WhiteSmoke;
			txtSelectedUpgradeFile.IsComboTextBox = false;
			txtSelectedUpgradeFile.Location = new System.Drawing.Point(16, 22);
			txtSelectedUpgradeFile.MaxLength = 1000;
			txtSelectedUpgradeFile.Name = "txtSelectedUpgradeFile";
			txtSelectedUpgradeFile.ReadOnly = true;
			txtSelectedUpgradeFile.Size = new System.Drawing.Size(537, 20);
			txtSelectedUpgradeFile.TabIndex = 0;
			imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			imageList.ImageSize = new System.Drawing.Size(16, 16);
			imageList.TransparentColor = System.Drawing.Color.Transparent;
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(200, 120);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(91, 13);
			mmLabel9.TabIndex = 39;
			mmLabel9.Text = "Required Version:";
			textBoxRequiredVersion.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRequiredVersion.IsComboTextBox = false;
			textBoxRequiredVersion.Location = new System.Drawing.Point(293, 117);
			textBoxRequiredVersion.MaxLength = 1000;
			textBoxRequiredVersion.Name = "textBoxRequiredVersion";
			textBoxRequiredVersion.ReadOnly = true;
			textBoxRequiredVersion.Size = new System.Drawing.Size(89, 20);
			textBoxRequiredVersion.TabIndex = 4;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(597, 384);
			base.Controls.Add(wizardControl1);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "UpgradeDatabaseForm";
			Text = "Upgrade Database";
			base.Activated += new System.EventHandler(UpgradeDatabaseForm_Activated);
			base.Closing += new System.ComponentModel.CancelEventHandler(OnClosing);
			base.Load += new System.EventHandler(DatabaseAttachmentForm_Load);
			((System.ComponentModel.ISupportInitialize)wizardControl1).EndInit();
			wizardControl1.ResumeLayout(false);
			wizardUpgradeFilePage.ResumeLayout(false);
			wizardUpgradeFilePage.PerformLayout();
			wizardCompletionPage.ResumeLayout(false);
			wizardCompletionPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			wizardSelectDatabasePage.ResumeLayout(false);
			panelOverwrite.ResumeLayout(false);
			panelOverwrite.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDatabase).EndInit();
			wizardUpgradeSummaryPage.ResumeLayout(false);
			wizardUpgradeSummaryPage.PerformLayout();
			ResumeLayout(false);
		}

		public void Init()
		{
			AddToolTips();
			Global.ChangeApplicationStatusMessage(Text);
			if (comboBoxDatabase.SelectedID == "")
			{
				comboBoxDatabase.SelectedID = Global.CurrentDatabaseName;
			}
			if (editFileName.Text == "")
			{
				editFileName.Text = Path.GetDirectoryName(Application.ExecutablePath) + "\\DB Upgrade\\Axolon_DB_Upgrade.dacpac";
			}
		}

		private string GetEncyptedPassword()
		{
			return "";
		}

		private bool ValidateEntries()
		{
			if (editFileName.Text.Trim() == string.Empty)
			{
				ErrorHelper.WarningMessage("Please specify a valid file path.");
				buttonOpenFileDialog.Focus();
				return false;
			}
			try
			{
				if (!File.Exists(editFileName.Text))
				{
					ErrorHelper.WarningMessage("The file name you have specified does not exist.", "Please specify a valid file path.");
					editFileName.SelectAll();
					editFileName.Focus();
					return false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				editFileName.SelectAll();
				editFileName.Focus();
				return false;
			}
			return true;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (ValidateEntries())
			{
				UpgradeDatabase();
			}
		}

		private bool UpgradeDatabase()
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				Application.DoEvents();
				_ = Global.CurrentInstanceName;
				DacPackage.Load(editFileName.Text);
				FileStream fileStream = File.OpenRead(editFileName.Text);
				bool result = Factory.DatabaseSystem.UpgradeDatabase(comboBoxDatabase.SelectedID, fileStream, editUserName.Text, editPassword.Text, checkBoxBackup.Checked);
				fileStream.Close();
				return result;
			}
			catch (SqlException ex)
			{
				if (ex.Number == 15007 || ex.Number == 15247 || ex.Number == 229)
				{
					ErrorHelper.WarningMessage(SR.GetString("00063"), SR.GetString("00064"));
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
				return false;
			}
			catch (CompanyException ex2)
			{
				if (ex2.Number == 1042)
				{
					ErrorHelper.ErrorMessage("Cannot connect to the database.", "Please make sure that the user id and password are correct.");
					return false;
				}
				return false;
			}
			catch (DacServicesException ex3)
			{
				ErrorHelper.ErrorMessage("Unable to upgrade the database. Make sure that the user id and password are correct and you have administrator right.", "Error is:" + ex3.Message);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonOpenFileDialog_Click(object sender, EventArgs e)
		{
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = true;
			openFileDialog.Filter = "Database Upgrade Files (*.dacpac)|*.dacpac";
			openFileDialog.DefaultExt = "dacpac";
			openFileDialog.FileName = "Axolon_DB_Upgrade.dacpac";
			openFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\DB Upgrade";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (!new FileInfo(openFileDialog.FileName).Exists)
				{
					ErrorHelper.WarningMessage("File does not exist.");
				}
				else
				{
					editFileName.Text = openFileDialog.FileName;
				}
			}
		}

		private void buttonOpenFileDialog2_Click(object sender, EventArgs e)
		{
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = false;
			openFileDialog.InitialDirectory = Application.StartupPath;
			openFileDialog.FileName = "";
			openFileDialog.Filter = "Database Files (*.mdf)|*.mdf";
			if (openFileDialog.ShowDialog() == DialogResult.OK && new FileInfo(openFileDialog.FileName).Exists)
			{
				ErrorHelper.QuestionMessageYesNo(SR.GetString("00059"), SR.GetString("00060"));
				_ = 7;
			}
		}

		private void tabDatabase_Click(object sender, EventArgs e)
		{
		}

		private void DatabaseAttachmentForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					Init();
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
				base.Visible = false;
				Close();
				Dispose();
			}
		}

		public void LoadForm()
		{
		}

		private void AddToolTips()
		{
		}

		private void OnFileNameEntered(object sender, EventArgs e)
		{
		}

		private void OnDestinationFileNameEntered(object sender, EventArgs e)
		{
		}

		private void OnDatabaseNameEntered(object sender, EventArgs e)
		{
		}

		private void OnLoginNameEntered(object sender, EventArgs e)
		{
		}

		private void OnPasswordEntered(object sender, EventArgs e)
		{
		}

		private void OnClosing(object sender, CancelEventArgs e)
		{
		}

		private void OnServerEntered(object sender, EventArgs e)
		{
		}

		private void OnDatabaseNameLeave(object sender, EventArgs e)
		{
		}

		private void linkLabelExsistingDB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		public CompanyDatabase BringUp(Form parent)
		{
			ShowDialog(parent);
			return database;
		}

		private void UpgradeDatabaseForm_Activated(object sender, EventArgs e)
		{
			Application.DoEvents();
		}

		private void wizardControl1_NextClick(object sender, WizardCommandButtonClickEventArgs e)
		{
			if (!SetWizardPage(e.Page))
			{
				e.Handled = true;
			}
		}

		private bool SetWizardPage(BaseWizardPage page)
		{
			if (page.Name == "wizardUpgradeFilePage")
			{
				wizardControl1.SelectedPage.AllowCancel = false;
				if (!ValidateEntries())
				{
					return false;
				}
				DacPackage dacPackage = DacPackage.Load(editFileName.Text);
				textBoxNewVersion.Text = dacPackage.Version.ToString();
				textBoxRequiredVersion.Text = Factory.DatabaseSystem.GetRequiredDBVersion();
			}
			else if (page.Name == "wizardSelectDatabasePage")
			{
				if (comboBoxDatabase.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Please select a database to upgrade.");
					return false;
				}
				wizardControl1.SelectedPage.AllowCancel = true;
				txtSelectedUpgradeFile.Text = editFileName.Text;
				txtDatabaseName.Text = comboBoxDatabase.Text;
				txtName.Text = editCompanyName.Text;
				textBoxCurrentVersion.Text = textBoxVersion.Text;
				lblName.Text = "Company Name:";
			}
			else if (page.Name == "wizardUpgradeSummaryPage")
			{
				Version value = new Version(1, 0, 0, 0);
				try
				{
					value = new Version(textBoxCurrentVersion.Text);
				}
				catch (Exception)
				{
				}
				DacPackage dacPackage = DacPackage.Load(editFileName.Text);
				if (dacPackage.Version.CompareTo(value) <= 0)
				{
					ErrorHelper.WarningMessage("The upgrade file that you have selected is similar or older than the current database version.", "Please select another upgrade file.");
					return false;
				}
				if (ErrorHelper.WarningMessageOkCancel("Upgrading database cannot be reverted. It is highly recommended to take a backup before proceeding.", "Do you still want to continue?") == DialogResult.Cancel)
				{
					return false;
				}
				wizardControl1.SelectedPage.AllowCancel = false;
				if (!UpgradeDatabase())
				{
					return false;
				}
				lblRestoreMessage.ForeColor = Color.Green;
				lblRestoreMessage.Text = "The database has been upgraded successfully!";
				lblRestoreMessage.Visible = true;
			}
			return true;
		}

		private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
		{
			Close();
		}

		private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to cancel the wizard?") == DialogResult.Yes)
			{
				Close();
			}
		}

		private void radioButtonOverwrite_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonNew_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void wizardControl1_CustomizeCommandButtons(object sender, CustomizeCommandButtonsEventArgs e)
		{
			if (e.Page.Name == "wizardUpgradeSummaryPage")
			{
				e.NextButton.Text = "Upgrade";
			}
			if (e.Page.Name == "wizardCompletionPage")
			{
				e.PrevButton.Visible = false;
				e.CancelButton.Visible = false;
			}
		}
	}
}
