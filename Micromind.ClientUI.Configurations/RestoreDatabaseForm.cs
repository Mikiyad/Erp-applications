using DevExpress.XtraWizard;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class RestoreDatabaseForm : Form, IForm
	{
		private CompanyDatabase database = new CompanyDatabase();

		private OpenFileDialog openFileDialog;

		private System.Windows.Forms.ToolTip toolTip;

		private WizardControl wizardControl1;

		private WelcomeWizardPage wizardWelcomePage;

		private WizardPage wizardBackupFilePage;

		private CompletionWizardPage wizardCompletionPage;

		private MMTextBox editFileName;

		private MMLabel label1;

		private XPButton buttonOpenFileDialog;

		private WizardPage wizardRestoreOptionsPage;

		private Panel panelOverwrite;

		private DatabaseComboBox comboBoxDatabase;

		private MMLabel mmLabel1;

		private MMLabel label8;

		private RadioButton radioButtonOverwrite;

		private Panel panelNew;

		private MMTextBox editDestinationFileName;

		private MMLabel label4;

		private MMTextBox editDatabaseName;

		private MMLabel lblDatabaseName;

		private XPButton buttonOpenFileDialog2;

		private RadioButton radioButtonNew;

		private MMTextBox editCompanyName;

		private WizardPage wizardRestoreDbPage;

		private MMLabel mmLabel2;

		private MMTextBox txtSelectedBackupFile;

		private MMLabel lblName;

		private MMTextBox txtName;

		private MMLabel mmLabel3;

		private MMTextBox txtDatabaseName;

		private MMLabel lblRestoreMessage;

		private ImageList imageList;

		private IContainer components;

		private ScreenAccessRight screenRight;

		public ScreenAreas ScreenArea => ScreenAreas.Company;

		public int ScreenID => 8005;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		public CompanyDatabase Database => database;

		public RestoreDatabaseForm()
		{
			InitializeComponent();
		}

		public RestoreDatabaseForm(Form form)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.RestoreDatabaseForm));
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
			wizardBackupFilePage = new DevExpress.XtraWizard.WizardPage();
			editFileName = new Micromind.UISupport.MMTextBox();
			label1 = new Micromind.UISupport.MMLabel();
			buttonOpenFileDialog = new Micromind.UISupport.XPButton();
			wizardCompletionPage = new DevExpress.XtraWizard.CompletionWizardPage();
			lblRestoreMessage = new Micromind.UISupport.MMLabel();
			wizardRestoreOptionsPage = new DevExpress.XtraWizard.WizardPage();
			panelNew = new System.Windows.Forms.Panel();
			editDestinationFileName = new Micromind.UISupport.MMTextBox();
			label4 = new Micromind.UISupport.MMLabel();
			editDatabaseName = new Micromind.UISupport.MMTextBox();
			lblDatabaseName = new Micromind.UISupport.MMLabel();
			buttonOpenFileDialog2 = new Micromind.UISupport.XPButton();
			radioButtonNew = new System.Windows.Forms.RadioButton();
			panelOverwrite = new System.Windows.Forms.Panel();
			comboBoxDatabase = new Micromind.DataControls.DatabaseComboBox();
			editCompanyName = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			label8 = new Micromind.UISupport.MMLabel();
			radioButtonOverwrite = new System.Windows.Forms.RadioButton();
			wizardRestoreDbPage = new DevExpress.XtraWizard.WizardPage();
			lblName = new Micromind.UISupport.MMLabel();
			txtName = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			txtDatabaseName = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			txtSelectedBackupFile = new Micromind.UISupport.MMTextBox();
			imageList = new System.Windows.Forms.ImageList(components);
			((System.ComponentModel.ISupportInitialize)wizardControl1).BeginInit();
			wizardControl1.SuspendLayout();
			wizardBackupFilePage.SuspendLayout();
			wizardCompletionPage.SuspendLayout();
			wizardRestoreOptionsPage.SuspendLayout();
			panelNew.SuspendLayout();
			panelOverwrite.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDatabase).BeginInit();
			wizardRestoreDbPage.SuspendLayout();
			SuspendLayout();
			wizardControl1.Appearance.Page.BackColor = System.Drawing.Color.Transparent;
			wizardControl1.Appearance.Page.BackColor2 = System.Drawing.Color.Transparent;
			wizardControl1.Appearance.Page.Options.UseBackColor = true;
			wizardControl1.Controls.Add(wizardWelcomePage);
			wizardControl1.Controls.Add(wizardBackupFilePage);
			wizardControl1.Controls.Add(wizardCompletionPage);
			wizardControl1.Controls.Add(wizardRestoreOptionsPage);
			wizardControl1.Controls.Add(wizardRestoreDbPage);
			wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			wizardControl1.Image = (System.Drawing.Image)resources.GetObject("wizardControl1.Image");
			wizardControl1.ImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			wizardControl1.Location = new System.Drawing.Point(0, 0);
			wizardControl1.Name = "wizardControl1";
			wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[5]
			{
				wizardWelcomePage,
				wizardBackupFilePage,
				wizardRestoreOptionsPage,
				wizardRestoreDbPage,
				wizardCompletionPage
			});
			wizardControl1.Size = new System.Drawing.Size(657, 434);
			wizardControl1.UseCancelButton = false;
			wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(wizardControl1_CancelClick);
			wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(wizardControl1_FinishClick);
			wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(wizardControl1_NextClick);
			wizardControl1.CustomizeCommandButtons += new DevExpress.XtraWizard.WizardCustomizeCommandButtonsEventHandler(wizardControl1_CustomizeCommandButtons);
			wizardWelcomePage.IntroductionText = "This wizard helps you to restore your database";
			wizardWelcomePage.Name = "wizardWelcomePage";
			wizardWelcomePage.Size = new System.Drawing.Size(440, 301);
			wizardBackupFilePage.Controls.Add(editFileName);
			wizardBackupFilePage.Controls.Add(label1);
			wizardBackupFilePage.Controls.Add(buttonOpenFileDialog);
			wizardBackupFilePage.DescriptionText = "Select a backup database file you want to restore";
			wizardBackupFilePage.Name = "wizardBackupFilePage";
			wizardBackupFilePage.Size = new System.Drawing.Size(625, 289);
			wizardBackupFilePage.Text = "Backup Database File";
			editFileName.BackColor = System.Drawing.Color.WhiteSmoke;
			editFileName.IsComboTextBox = false;
			editFileName.Location = new System.Drawing.Point(83, 68);
			editFileName.MaxLength = 1000;
			editFileName.Name = "editFileName";
			editFileName.ReadOnly = true;
			editFileName.Size = new System.Drawing.Size(413, 20);
			editFileName.TabIndex = 20;
			label1.AutoSize = true;
			label1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			label1.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.IsFieldHeader = false;
			label1.IsRequired = false;
			label1.Location = new System.Drawing.Point(83, 52);
			label1.Name = "label1";
			label1.PenWidth = 1f;
			label1.ShowBorder = false;
			label1.Size = new System.Drawing.Size(160, 13);
			label1.TabIndex = 22;
			label1.Text = "Select the backup file to restore:";
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
			wizardCompletionPage.Controls.Add(lblRestoreMessage);
			wizardCompletionPage.FinishText = "";
			wizardCompletionPage.Name = "wizardCompletionPage";
			wizardCompletionPage.Size = new System.Drawing.Size(440, 301);
			lblRestoreMessage.AutoSize = true;
			lblRestoreMessage.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblRestoreMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblRestoreMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f);
			lblRestoreMessage.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			lblRestoreMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblRestoreMessage.IsFieldHeader = false;
			lblRestoreMessage.IsRequired = false;
			lblRestoreMessage.Location = new System.Drawing.Point(46, 63);
			lblRestoreMessage.Name = "lblRestoreMessage";
			lblRestoreMessage.PenWidth = 1f;
			lblRestoreMessage.ShowBorder = false;
			lblRestoreMessage.Size = new System.Drawing.Size(69, 18);
			lblRestoreMessage.TabIndex = 26;
			lblRestoreMessage.Text = "Message";
			lblRestoreMessage.Visible = false;
			wizardRestoreOptionsPage.Controls.Add(panelNew);
			wizardRestoreOptionsPage.Controls.Add(radioButtonNew);
			wizardRestoreOptionsPage.Controls.Add(panelOverwrite);
			wizardRestoreOptionsPage.Controls.Add(radioButtonOverwrite);
			wizardRestoreOptionsPage.DescriptionText = "Select database to overwrite with your backup file or create a new one";
			wizardRestoreOptionsPage.Name = "wizardRestoreOptionsPage";
			wizardRestoreOptionsPage.Size = new System.Drawing.Size(625, 289);
			wizardRestoreOptionsPage.Text = "Database Restore Options";
			panelNew.Controls.Add(editDestinationFileName);
			panelNew.Controls.Add(label4);
			panelNew.Controls.Add(editDatabaseName);
			panelNew.Controls.Add(lblDatabaseName);
			panelNew.Controls.Add(buttonOpenFileDialog2);
			panelNew.Enabled = false;
			panelNew.Location = new System.Drawing.Point(85, 183);
			panelNew.Name = "panelNew";
			panelNew.Size = new System.Drawing.Size(451, 54);
			panelNew.TabIndex = 7;
			editDestinationFileName.BackColor = System.Drawing.Color.WhiteSmoke;
			editDestinationFileName.IsComboTextBox = false;
			editDestinationFileName.Location = new System.Drawing.Point(107, 28);
			editDestinationFileName.MaxLength = 255;
			editDestinationFileName.Name = "editDestinationFileName";
			editDestinationFileName.ReadOnly = true;
			editDestinationFileName.Size = new System.Drawing.Size(307, 20);
			editDestinationFileName.TabIndex = 1;
			label4.AutoSize = true;
			label4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			label4.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label4.IsFieldHeader = false;
			label4.IsRequired = false;
			label4.Location = new System.Drawing.Point(49, 30);
			label4.Name = "label4";
			label4.PenWidth = 1f;
			label4.ShowBorder = false;
			label4.Size = new System.Drawing.Size(57, 13);
			label4.TabIndex = 23;
			label4.Text = "File Name:";
			editDatabaseName.BackColor = System.Drawing.Color.White;
			editDatabaseName.IsComboTextBox = false;
			editDatabaseName.Location = new System.Drawing.Point(107, 5);
			editDatabaseName.MaxLength = 20;
			editDatabaseName.Name = "editDatabaseName";
			editDatabaseName.Size = new System.Drawing.Size(307, 20);
			editDatabaseName.TabIndex = 0;
			editDatabaseName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(editDatabaseName_KeyPress);
			editDatabaseName.Validating += new System.ComponentModel.CancelEventHandler(editDatabaseName_Validating);
			lblDatabaseName.AutoSize = true;
			lblDatabaseName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDatabaseName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDatabaseName.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			lblDatabaseName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDatabaseName.IsFieldHeader = false;
			lblDatabaseName.IsRequired = false;
			lblDatabaseName.Location = new System.Drawing.Point(19, 7);
			lblDatabaseName.Name = "lblDatabaseName";
			lblDatabaseName.PenWidth = 1f;
			lblDatabaseName.ShowBorder = false;
			lblDatabaseName.Size = new System.Drawing.Size(87, 13);
			lblDatabaseName.TabIndex = 0;
			lblDatabaseName.Text = "Database Name:";
			lblDatabaseName.Click += new System.EventHandler(radioButtonNew_CheckedChanged);
			buttonOpenFileDialog2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpenFileDialog2.BackColor = System.Drawing.Color.DarkGray;
			buttonOpenFileDialog2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpenFileDialog2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpenFileDialog2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpenFileDialog2.Location = new System.Drawing.Point(417, 27);
			buttonOpenFileDialog2.Name = "buttonOpenFileDialog2";
			buttonOpenFileDialog2.Size = new System.Drawing.Size(25, 22);
			buttonOpenFileDialog2.TabIndex = 2;
			buttonOpenFileDialog2.Text = "...";
			buttonOpenFileDialog2.UseVisualStyleBackColor = false;
			buttonOpenFileDialog2.Click += new System.EventHandler(buttonOpenFileDialog2_Click);
			radioButtonNew.AutoSize = true;
			radioButtonNew.Location = new System.Drawing.Point(86, 160);
			radioButtonNew.Name = "radioButtonNew";
			radioButtonNew.Size = new System.Drawing.Size(138, 17);
			radioButtonNew.TabIndex = 6;
			radioButtonNew.TabStop = true;
			radioButtonNew.Text = "Create a new database:";
			radioButtonNew.UseVisualStyleBackColor = true;
			radioButtonNew.CheckedChanged += new System.EventHandler(radioButtonNew_CheckedChanged);
			panelOverwrite.Controls.Add(comboBoxDatabase);
			panelOverwrite.Controls.Add(mmLabel1);
			panelOverwrite.Controls.Add(editCompanyName);
			panelOverwrite.Controls.Add(label8);
			panelOverwrite.Location = new System.Drawing.Point(85, 68);
			panelOverwrite.Name = "panelOverwrite";
			panelOverwrite.Size = new System.Drawing.Size(450, 53);
			panelOverwrite.TabIndex = 5;
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
			editCompanyName.TabIndex = 0;
			editCompanyName.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(21, 29);
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
			label8.Location = new System.Drawing.Point(19, 8);
			label8.Name = "label8";
			label8.PenWidth = 1f;
			label8.ShowBorder = false;
			label8.Size = new System.Drawing.Size(87, 13);
			label8.TabIndex = 102;
			label8.Text = "Database Name:";
			radioButtonOverwrite.AutoSize = true;
			radioButtonOverwrite.Checked = true;
			radioButtonOverwrite.Location = new System.Drawing.Point(85, 45);
			radioButtonOverwrite.Name = "radioButtonOverwrite";
			radioButtonOverwrite.Size = new System.Drawing.Size(175, 17);
			radioButtonOverwrite.TabIndex = 4;
			radioButtonOverwrite.TabStop = true;
			radioButtonOverwrite.Text = "Overwrite an exisiting database:";
			radioButtonOverwrite.UseVisualStyleBackColor = true;
			radioButtonOverwrite.CheckedChanged += new System.EventHandler(radioButtonOverwrite_CheckedChanged);
			wizardRestoreDbPage.Controls.Add(lblName);
			wizardRestoreDbPage.Controls.Add(txtName);
			wizardRestoreDbPage.Controls.Add(mmLabel3);
			wizardRestoreDbPage.Controls.Add(txtDatabaseName);
			wizardRestoreDbPage.Controls.Add(mmLabel2);
			wizardRestoreDbPage.Controls.Add(txtSelectedBackupFile);
			wizardRestoreDbPage.DescriptionText = "Please review and verify your options then click Restore";
			wizardRestoreDbPage.Name = "wizardRestoreDbPage";
			wizardRestoreDbPage.Size = new System.Drawing.Size(625, 289);
			wizardRestoreDbPage.Text = "Restore Database Review Summary";
			lblName.AutoSize = true;
			lblName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblName.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			lblName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblName.IsFieldHeader = false;
			lblName.IsRequired = false;
			lblName.Location = new System.Drawing.Point(106, 158);
			lblName.Name = "lblName";
			lblName.PenWidth = 1f;
			lblName.ShowBorder = false;
			lblName.Size = new System.Drawing.Size(38, 13);
			lblName.TabIndex = 27;
			lblName.Text = "Name:";
			txtName.BackColor = System.Drawing.Color.WhiteSmoke;
			txtName.IsComboTextBox = false;
			txtName.Location = new System.Drawing.Point(199, 155);
			txtName.MaxLength = 1000;
			txtName.Name = "txtName";
			txtName.ReadOnly = true;
			txtName.Size = new System.Drawing.Size(323, 20);
			txtName.TabIndex = 26;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(106, 130);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(87, 13);
			mmLabel3.TabIndex = 25;
			mmLabel3.Text = "Database Name:";
			txtDatabaseName.BackColor = System.Drawing.Color.WhiteSmoke;
			txtDatabaseName.IsComboTextBox = false;
			txtDatabaseName.Location = new System.Drawing.Point(199, 127);
			txtDatabaseName.MaxLength = 1000;
			txtDatabaseName.Name = "txtDatabaseName";
			txtDatabaseName.ReadOnly = true;
			txtDatabaseName.Size = new System.Drawing.Size(323, 20);
			txtDatabaseName.TabIndex = 24;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(106, 39);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(131, 13);
			mmLabel2.TabIndex = 23;
			mmLabel2.Text = "Selected backup file path:";
			txtSelectedBackupFile.BackColor = System.Drawing.Color.WhiteSmoke;
			txtSelectedBackupFile.IsComboTextBox = false;
			txtSelectedBackupFile.Location = new System.Drawing.Point(109, 56);
			txtSelectedBackupFile.MaxLength = 1000;
			txtSelectedBackupFile.Name = "txtSelectedBackupFile";
			txtSelectedBackupFile.ReadOnly = true;
			txtSelectedBackupFile.Size = new System.Drawing.Size(413, 20);
			txtSelectedBackupFile.TabIndex = 21;
			imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
			imageList.TransparentColor = System.Drawing.Color.Transparent;
			imageList.Images.SetKeyName(0, "tick.png");
			imageList.Images.SetKeyName(1, "Delete-icon.png");
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(657, 434);
			base.Controls.Add(wizardControl1);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RestoreDatabaseForm";
			Text = "Restore Database";
			base.Activated += new System.EventHandler(RestoreDatabaseForm_Activated);
			base.Closing += new System.ComponentModel.CancelEventHandler(OnClosing);
			base.Load += new System.EventHandler(DatabaseAttachmentForm_Load);
			((System.ComponentModel.ISupportInitialize)wizardControl1).EndInit();
			wizardControl1.ResumeLayout(false);
			wizardBackupFilePage.ResumeLayout(false);
			wizardBackupFilePage.PerformLayout();
			wizardCompletionPage.ResumeLayout(false);
			wizardCompletionPage.PerformLayout();
			wizardRestoreOptionsPage.ResumeLayout(false);
			wizardRestoreOptionsPage.PerformLayout();
			panelNew.ResumeLayout(false);
			panelNew.PerformLayout();
			panelOverwrite.ResumeLayout(false);
			panelOverwrite.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDatabase).EndInit();
			wizardRestoreDbPage.ResumeLayout(false);
			wizardRestoreDbPage.PerformLayout();
			ResumeLayout(false);
		}

		public void Init()
		{
			AddToolTips();
			Global.ChangeApplicationStatusMessage(Text);
			object setting = Global.CompanySettings.GetSetting("InitBackupFile");
			if (setting != null && setting.ToString().Trim() != string.Empty && File.Exists(setting.ToString()))
			{
				editFileName.Text = setting.ToString();
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

		private bool ValidateMappings()
		{
			if (radioButtonNew.Checked)
			{
				if (editDestinationFileName.Text.Trim() == string.Empty)
				{
					ErrorHelper.InformationMessage("Please specify a destination file name.");
					buttonOpenFileDialog2.Focus();
					return false;
				}
				editDatabaseName.Text = editDatabaseName.Text.Trim();
				if (editDatabaseName.Text.Trim().Length == 0)
				{
					ErrorHelper.InformationMessage("Please enter a name for the database.");
					editDatabaseName.Focus();
					return false;
				}
				if (!Directory.Exists(Path.GetDirectoryName(editDestinationFileName.Text)))
				{
					ErrorHelper.WarningMessage("The destination path specified does not exist.", "Please specify a valid location.");
					editDestinationFileName.SelectAll();
					editDestinationFileName.Focus();
					return false;
				}
			}
			else if (comboBoxDatabase.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a database.");
				return false;
			}
			return true;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (ValidateEntries())
			{
				RestoreDatabase();
			}
		}

		private bool RestoreDatabase()
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				Application.DoEvents();
				_ = Global.CurrentInstanceName;
				bool flag = true;
				if (radioButtonOverwrite.Checked)
				{
					if (ErrorHelper.QuestionMessageYesNo("This will overwrite the selected database and all the data will be lost. Are you sure to continue?") == DialogResult.No)
					{
						return false;
					}
					flag &= Factory.DatabaseSystem.RestoreDatabaseToDisk(editFileName.Text, "", comboBoxDatabase.SelectedID, replace: true);
				}
				else
				{
					flag &= Factory.DatabaseSystem.RestoreDatabaseToDisk(editFileName.Text, editDestinationFileName.Text, editDatabaseName.Text, replace: false);
				}
				if (flag)
				{
					Global.CompanySettings.SaveSetting("InitBackupFile", editFileName.Text);
					ErrorHelper.InformationMessage("Database restored successfully.");
				}
				return flag;
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
				if (ex2.Number == 1014)
				{
					ErrorHelper.ErrorMessage("This database name is already exist. Please enter another database name.");
					return false;
				}
				if (ex2.Number == 1015)
				{
					ErrorHelper.ErrorMessage("Cannot find the database location. The database may not exists or access denied.", "Try restoring to a new database.");
					return false;
				}
				ErrorHelper.ErrorMessage(ex2.Message);
				return false;
			}
			catch (Exception)
			{
				ErrorHelper.ErrorMessage("Unable to restore the database. Please try again later!");
				editDatabaseName.Focus();
				editDatabaseName.SelectAll();
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
			openFileDialog.CheckFileExists = false;
			openFileDialog.Filter = "Database Files (*.bak)|*.bak";
			openFileDialog.DefaultExt = "bak";
			openFileDialog.FileName = "";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (!new FileInfo(openFileDialog.FileName).Exists)
				{
					ErrorHelper.WarningMessage(SR.GetString("00062"));
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
			if (openFileDialog.ShowDialog() == DialogResult.OK && (!new FileInfo(openFileDialog.FileName).Exists || ErrorHelper.QuestionMessageYesNo(SR.GetString("00059"), SR.GetString("00060")) != DialogResult.No))
			{
				editDestinationFileName.Text = openFileDialog.FileName;
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
			editDatabaseName.Text = Global.MakeIdentifier(editDatabaseName.Text);
		}

		private void linkLabelExsistingDB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		public CompanyDatabase BringUp(Form parent)
		{
			ShowDialog(parent);
			return database;
		}

		private void RestoreDatabaseForm_Activated(object sender, EventArgs e)
		{
			Application.DoEvents();
		}

		private void editDatabaseName_Validating(object sender, CancelEventArgs e)
		{
			editDatabaseName.Text = editDatabaseName.Text.Replace(" ", "");
			editDatabaseName.Text = editDatabaseName.Text.Trim();
			if (Format.StartsWithDigit(editDatabaseName.Text))
			{
				ErrorHelper.InformationMessage("The database name cannot start with numbers or special characters.");
				e.Cancel = true;
			}
		}

		private void editDatabaseName_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (editDatabaseName.SelectionStart == 0 && char.IsNumber(e.KeyChar))
			{
				e.Handled = true;
			}
			if (char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsPunctuation(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void wizardControl1_NextClick(object sender, WizardCommandButtonClickEventArgs e)
		{
			if (e.Page.Name == "wizardBackupFilePage")
			{
				wizardControl1.SelectedPage.AllowCancel = false;
				if (!ValidateEntries())
				{
					e.Handled = true;
				}
			}
			else if (e.Page.Name == "wizardRestoreOptionsPage")
			{
				wizardControl1.SelectedPage.AllowCancel = false;
				if (!ValidateMappings())
				{
					e.Handled = true;
					return;
				}
				txtSelectedBackupFile.Text = editFileName.Text;
				if (radioButtonOverwrite.Checked)
				{
					txtDatabaseName.Text = comboBoxDatabase.Text;
					txtName.Text = editCompanyName.Text;
					lblName.Text = "Company Name:";
				}
				else
				{
					txtDatabaseName.Text = editDatabaseName.Text;
					txtName.Text = editDestinationFileName.Text;
					lblName.Text = "File Name:";
				}
			}
			else if (e.Page.Name == "wizardRestoreDbPage")
			{
				wizardControl1.SelectedPage.AllowCancel = false;
				if (RestoreDatabase())
				{
					lblRestoreMessage.ForeColor = Color.Green;
					lblRestoreMessage.Text = "The database has been restored successfully!";
					lblRestoreMessage.Visible = true;
				}
				else
				{
					lblRestoreMessage.ForeColor = Color.Maroon;
					lblRestoreMessage.Text = "Database restore Failed. Please try again later!";
					lblRestoreMessage.Visible = true;
				}
			}
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
			panelOverwrite.Enabled = radioButtonOverwrite.Checked;
		}

		private void radioButtonNew_CheckedChanged(object sender, EventArgs e)
		{
			panelNew.Enabled = radioButtonNew.Checked;
		}

		private void wizardControl1_CustomizeCommandButtons(object sender, CustomizeCommandButtonsEventArgs e)
		{
			if (e.Page.Name == "wizardRestoreDbPage")
			{
				e.NextButton.Text = "Restore";
			}
			if (e.Page.Name == "wizardCompletionPage")
			{
				e.PrevButton.Visible = false;
				e.CancelButton.Visible = false;
			}
		}
	}
}
