using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class EmployeeAbscondingEntryForm : Form, IDataForm, IDataEntry
	{
		private EmployeeActivityData currentData;

		private const string TABLENAME_CONST = "Employee_Absconding";

		private const string IDFIELD_CONST = "ActivityID";

		private bool isNewRecord = true;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonClose;

		private XPButton buttonSave;

		private FormManager formManager;

		private XPButton buttonNew;

		private XPButton buttonDelete;

		private ToolStrip toolStrip2;

		private ToolStripButton toolStripButtonPrint1;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl ultraTabPageControl1;

		private FormManager formManager1;

		private UltraTabPageControl ultraTabPageControl4;

		private MMTextBox textBoxFromDepartment;

		private MMTextBox textBoxFromDivision;

		private MMTextBox textBoxDesignation;

		private MMTextBox textBoxFromLocation;

		private MMLabel mmLabel30;

		private MMLabel mmLabel34;

		private MMLabel mmLabel35;

		private MMLabel mmLabel36;

		private PictureBox pictureBoxPhoto;

		private UltraFormattedLinkLabel ultraLinkVoucherNumber;

		private UltraGroupBox panelCancel;

		private UltraGroupBox ultraGroupBox4;

		private RadioButton radioButtonTicketAmountNo;

		private RadioButton radioButtonTicketAmountYes;

		private UltraGroupBox ultraGroupBox5;

		private RadioButton radioButtonPassportHeldNo;

		private RadioButton radioButtonPassportHeldYes;

		private MMLabel mmLabel39;

		private MMLabel mmLabel41;

		private MMTextBox textBoxAbscondingRefNo;

		private MMLabel mmLabel42;

		private DateTimePicker dateTimePickerLastWorkingDate;

		private MMLabel labelLastworking;

		private MMSDateTimePicker dateTimeAbsRegIMGDate;

		private MMSDateTimePicker dateTimeAbscondingRegMOLDate;

		private MMSDateTimePicker dateTimeRealAbscondingDate;

		private MMSDateTimePicker dateTimeAdviceReceivedOn;

		private MMLabel mmLabel43;

		private DateTimePicker dateTimeTransactionDate;

		private MMLabel mmLabel44;

		private MMLabel mmLabel45;

		private MMLabel mmLabel46;

		private MMTextBox textBoxMBRefNumber;

		private MMLabel mmLabel47;

		private MMLabel mmLabel52;

		private MMLabel labelAdviceReceivedOn;

		private MMTextBox textBoxRemarks;

		private MMLabel labelAbsconded;

		private MMTextBox mmTextBox1;

		private EmployeeComboBox comboBoxEmployee;

		private MMTextBox textBoxEmployeeName;

		private MMLabel mmLabel53;

		private DataEntryGrid dataGrid;

		private EmployeeDocTypeComboBox comboBoxDocumentType;

		private MMTextBox textBoxCode;

		private ToolStripButton toolStripButtonOpenList;

		private MMLabel mmLabel2;

		private MMSDateTimePicker dateTimeIMGCancellation;

		private MMLabel mmLabel1;

		private MMSDateTimePicker dateTimeMOLcancellation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator1;

		private IContainer components;

		private ScreenAccessRight screenRight;

		private bool isCancelled;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2008;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private string EmployeeNo
		{
			get;
			set;
		}

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
				if (value)
				{
					buttonDelete.Enabled = false;
				}
				else
				{
					buttonDelete.Enabled = true;
				}
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
			}
		}

		public EmployeeAbscondingEntryForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxEmployee.DescriptionTextBox = textBoxEmployeeName;
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
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.EmployeeAbscondingEntryForm));
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxFromDepartment = new Micromind.UISupport.MMTextBox();
			textBoxFromDivision = new Micromind.UISupport.MMTextBox();
			textBoxDesignation = new Micromind.UISupport.MMTextBox();
			textBoxFromLocation = new Micromind.UISupport.MMTextBox();
			mmLabel30 = new Micromind.UISupport.MMLabel();
			mmLabel34 = new Micromind.UISupport.MMLabel();
			mmLabel35 = new Micromind.UISupport.MMLabel();
			mmLabel36 = new Micromind.UISupport.MMLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			ultraLinkVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelCancel = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimeIMGCancellation = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel1 = new Micromind.UISupport.MMLabel();
			dateTimeMOLcancellation = new Micromind.UISupport.MMSDateTimePicker(components);
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			radioButtonTicketAmountNo = new System.Windows.Forms.RadioButton();
			radioButtonTicketAmountYes = new System.Windows.Forms.RadioButton();
			ultraGroupBox5 = new Infragistics.Win.Misc.UltraGroupBox();
			radioButtonPassportHeldNo = new System.Windows.Forms.RadioButton();
			radioButtonPassportHeldYes = new System.Windows.Forms.RadioButton();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			mmLabel41 = new Micromind.UISupport.MMLabel();
			textBoxAbscondingRefNo = new Micromind.UISupport.MMTextBox();
			mmLabel42 = new Micromind.UISupport.MMLabel();
			dateTimePickerLastWorkingDate = new System.Windows.Forms.DateTimePicker();
			labelLastworking = new Micromind.UISupport.MMLabel();
			dateTimeAbsRegIMGDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeAbscondingRegMOLDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeRealAbscondingDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeAdviceReceivedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel43 = new Micromind.UISupport.MMLabel();
			dateTimeTransactionDate = new System.Windows.Forms.DateTimePicker();
			mmLabel44 = new Micromind.UISupport.MMLabel();
			mmLabel45 = new Micromind.UISupport.MMLabel();
			mmLabel46 = new Micromind.UISupport.MMLabel();
			textBoxMBRefNumber = new Micromind.UISupport.MMTextBox();
			mmLabel47 = new Micromind.UISupport.MMLabel();
			mmLabel52 = new Micromind.UISupport.MMLabel();
			labelAdviceReceivedOn = new Micromind.UISupport.MMLabel();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			labelAbsconded = new Micromind.UISupport.MMLabel();
			mmTextBox1 = new Micromind.UISupport.MMTextBox();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			mmLabel53 = new Micromind.UISupport.MMLabel();
			formManager1 = new Micromind.DataControls.FormManager();
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxDocumentType = new Micromind.DataControls.EmployeeDocTypeComboBox();
			dataGrid = new Micromind.DataControls.DataEntryGrid();
			panelButtons = new System.Windows.Forms.Panel();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonClose = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			toolStrip2 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint1 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			formManager = new Micromind.DataControls.FormManager();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)panelCancel).BeginInit();
			panelCancel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).BeginInit();
			ultraGroupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			ultraTabPageControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDocumentType).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
			panelButtons.SuspendLayout();
			toolStrip2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			ultraTabPageControl1.Controls.Add(textBoxCode);
			ultraTabPageControl1.Controls.Add(textBoxFromDepartment);
			ultraTabPageControl1.Controls.Add(textBoxFromDivision);
			ultraTabPageControl1.Controls.Add(textBoxDesignation);
			ultraTabPageControl1.Controls.Add(textBoxFromLocation);
			ultraTabPageControl1.Controls.Add(mmLabel30);
			ultraTabPageControl1.Controls.Add(mmLabel34);
			ultraTabPageControl1.Controls.Add(mmLabel35);
			ultraTabPageControl1.Controls.Add(mmLabel36);
			ultraTabPageControl1.Controls.Add(pictureBoxPhoto);
			ultraTabPageControl1.Controls.Add(ultraLinkVoucherNumber);
			ultraTabPageControl1.Controls.Add(panelCancel);
			ultraTabPageControl1.Controls.Add(labelAbsconded);
			ultraTabPageControl1.Controls.Add(mmTextBox1);
			ultraTabPageControl1.Controls.Add(comboBoxEmployee);
			ultraTabPageControl1.Controls.Add(mmLabel53);
			ultraTabPageControl1.Controls.Add(textBoxEmployeeName);
			ultraTabPageControl1.Controls.Add(formManager1);
			ultraTabPageControl1.Location = new System.Drawing.Point(2, 21);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(622, 450);
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(291, 10);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(140, 20);
			textBoxCode.TabIndex = 425;
			textBoxCode.TabStop = false;
			textBoxFromDepartment.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDepartment.CustomReportFieldName = "";
			textBoxFromDepartment.CustomReportKey = "";
			textBoxFromDepartment.CustomReportValueType = 1;
			textBoxFromDepartment.IsComboTextBox = false;
			textBoxFromDepartment.IsModified = false;
			textBoxFromDepartment.Location = new System.Drawing.Point(329, 83);
			textBoxFromDepartment.MaxLength = 255;
			textBoxFromDepartment.Name = "textBoxFromDepartment";
			textBoxFromDepartment.ReadOnly = true;
			textBoxFromDepartment.Size = new System.Drawing.Size(141, 20);
			textBoxFromDepartment.TabIndex = 420;
			textBoxFromDepartment.TabStop = false;
			textBoxFromDivision.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDivision.CustomReportFieldName = "";
			textBoxFromDivision.CustomReportKey = "";
			textBoxFromDivision.CustomReportValueType = 1;
			textBoxFromDivision.IsComboTextBox = false;
			textBoxFromDivision.IsModified = false;
			textBoxFromDivision.Location = new System.Drawing.Point(114, 82);
			textBoxFromDivision.MaxLength = 64;
			textBoxFromDivision.Name = "textBoxFromDivision";
			textBoxFromDivision.ReadOnly = true;
			textBoxFromDivision.Size = new System.Drawing.Size(140, 20);
			textBoxFromDivision.TabIndex = 419;
			textBoxFromDivision.TabStop = false;
			textBoxDesignation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDesignation.CustomReportFieldName = "";
			textBoxDesignation.CustomReportKey = "";
			textBoxDesignation.CustomReportValueType = 1;
			textBoxDesignation.IsComboTextBox = false;
			textBoxDesignation.IsModified = false;
			textBoxDesignation.Location = new System.Drawing.Point(114, 58);
			textBoxDesignation.MaxLength = 15;
			textBoxDesignation.Name = "textBoxDesignation";
			textBoxDesignation.ReadOnly = true;
			textBoxDesignation.Size = new System.Drawing.Size(140, 20);
			textBoxDesignation.TabIndex = 417;
			textBoxDesignation.TabStop = false;
			textBoxFromLocation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFromLocation.CustomReportFieldName = "";
			textBoxFromLocation.CustomReportKey = "";
			textBoxFromLocation.CustomReportValueType = 1;
			textBoxFromLocation.IsComboTextBox = false;
			textBoxFromLocation.IsModified = false;
			textBoxFromLocation.Location = new System.Drawing.Point(329, 59);
			textBoxFromLocation.MaxLength = 15;
			textBoxFromLocation.Name = "textBoxFromLocation";
			textBoxFromLocation.ReadOnly = true;
			textBoxFromLocation.Size = new System.Drawing.Size(141, 20);
			textBoxFromLocation.TabIndex = 418;
			textBoxFromLocation.TabStop = false;
			mmLabel30.AutoSize = true;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(258, 87);
			mmLabel30.Name = "mmLabel30";
			mmLabel30.PenWidth = 1f;
			mmLabel30.ShowBorder = false;
			mmLabel30.Size = new System.Drawing.Size(65, 13);
			mmLabel30.TabIndex = 421;
			mmLabel30.Text = "Department:";
			mmLabel34.AutoSize = true;
			mmLabel34.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel34.IsFieldHeader = false;
			mmLabel34.IsRequired = false;
			mmLabel34.Location = new System.Drawing.Point(13, 86);
			mmLabel34.Name = "mmLabel34";
			mmLabel34.PenWidth = 1f;
			mmLabel34.ShowBorder = false;
			mmLabel34.Size = new System.Drawing.Size(47, 13);
			mmLabel34.TabIndex = 423;
			mmLabel34.Text = "Division:";
			mmLabel35.AutoSize = true;
			mmLabel35.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel35.IsFieldHeader = false;
			mmLabel35.IsRequired = false;
			mmLabel35.Location = new System.Drawing.Point(13, 62);
			mmLabel35.Name = "mmLabel35";
			mmLabel35.PenWidth = 1f;
			mmLabel35.ShowBorder = false;
			mmLabel35.Size = new System.Drawing.Size(66, 13);
			mmLabel35.TabIndex = 424;
			mmLabel35.Text = "Designation:";
			mmLabel36.AutoSize = true;
			mmLabel36.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel36.IsFieldHeader = false;
			mmLabel36.IsRequired = false;
			mmLabel36.Location = new System.Drawing.Point(258, 63);
			mmLabel36.Name = "mmLabel36";
			mmLabel36.PenWidth = 1f;
			mmLabel36.ShowBorder = false;
			mmLabel36.Size = new System.Drawing.Size(51, 13);
			mmLabel36.TabIndex = 422;
			mmLabel36.Text = "Location:";
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.Location = new System.Drawing.Point(476, 10);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(139, 131);
			pictureBoxPhoto.TabIndex = 416;
			pictureBoxPhoto.TabStop = false;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraLinkVoucherNumber.Appearance = appearance;
			ultraLinkVoucherNumber.AutoSize = true;
			ultraLinkVoucherNumber.Location = new System.Drawing.Point(13, 13);
			ultraLinkVoucherNumber.Name = "ultraLinkVoucherNumber";
			ultraLinkVoucherNumber.Size = new System.Drawing.Size(93, 15);
			ultraLinkVoucherNumber.TabIndex = 415;
			ultraLinkVoucherNumber.TabStop = true;
			ultraLinkVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraLinkVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraLinkVoucherNumber.Value = "Employee Code:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraLinkVoucherNumber.VisitedLinkAppearance = appearance2;
			ultraLinkVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraLinkVoucherNumber_LinkClicked);
			panelCancel.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelCancel.Controls.Add(mmLabel2);
			panelCancel.Controls.Add(dateTimeIMGCancellation);
			panelCancel.Controls.Add(mmLabel1);
			panelCancel.Controls.Add(dateTimeMOLcancellation);
			panelCancel.Controls.Add(ultraGroupBox4);
			panelCancel.Controls.Add(ultraGroupBox5);
			panelCancel.Controls.Add(mmLabel39);
			panelCancel.Controls.Add(mmLabel41);
			panelCancel.Controls.Add(textBoxAbscondingRefNo);
			panelCancel.Controls.Add(mmLabel42);
			panelCancel.Controls.Add(dateTimePickerLastWorkingDate);
			panelCancel.Controls.Add(labelLastworking);
			panelCancel.Controls.Add(dateTimeAbsRegIMGDate);
			panelCancel.Controls.Add(dateTimeAbscondingRegMOLDate);
			panelCancel.Controls.Add(dateTimeRealAbscondingDate);
			panelCancel.Controls.Add(dateTimeAdviceReceivedOn);
			panelCancel.Controls.Add(mmLabel43);
			panelCancel.Controls.Add(dateTimeTransactionDate);
			panelCancel.Controls.Add(mmLabel44);
			panelCancel.Controls.Add(mmLabel45);
			panelCancel.Controls.Add(mmLabel46);
			panelCancel.Controls.Add(textBoxMBRefNumber);
			panelCancel.Controls.Add(mmLabel47);
			panelCancel.Controls.Add(mmLabel52);
			panelCancel.Controls.Add(labelAdviceReceivedOn);
			panelCancel.Controls.Add(textBoxRemarks);
			panelCancel.Location = new System.Drawing.Point(1, 147);
			panelCancel.Name = "panelCancel";
			panelCancel.Size = new System.Drawing.Size(614, 299);
			panelCancel.TabIndex = 412;
			panelCancel.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2000;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(11, 160);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(142, 13);
			mmLabel2.TabIndex = 411;
			mmLabel2.Text = "CN Cancellation Date(IMG) :";
			dateTimeIMGCancellation.Checked = false;
			dateTimeIMGCancellation.CustomFormat = " dd-MMM-yyyy";
			dateTimeIMGCancellation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeIMGCancellation.Location = new System.Drawing.Point(162, 156);
			dateTimeIMGCancellation.Name = "dateTimeIMGCancellation";
			dateTimeIMGCancellation.ShowCheckBox = true;
			dateTimeIMGCancellation.Size = new System.Drawing.Size(128, 20);
			dateTimeIMGCancellation.TabIndex = 9;
			dateTimeIMGCancellation.Value = new System.DateTime(0L);
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(11, 88);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(127, 13);
			mmLabel1.TabIndex = 409;
			mmLabel1.Text = "Cancellation Date(MOL) :";
			dateTimeMOLcancellation.Checked = false;
			dateTimeMOLcancellation.CustomFormat = " dd-MMM-yyyy";
			dateTimeMOLcancellation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeMOLcancellation.Location = new System.Drawing.Point(162, 84);
			dateTimeMOLcancellation.Name = "dateTimeMOLcancellation";
			dateTimeMOLcancellation.ShowCheckBox = true;
			dateTimeMOLcancellation.Size = new System.Drawing.Size(128, 20);
			dateTimeMOLcancellation.TabIndex = 5;
			dateTimeMOLcancellation.Value = new System.DateTime(0L);
			ultraGroupBox4.Controls.Add(radioButtonTicketAmountNo);
			ultraGroupBox4.Controls.Add(radioButtonTicketAmountYes);
			ultraGroupBox4.Location = new System.Drawing.Point(374, 184);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(95, 26);
			ultraGroupBox4.TabIndex = 11;
			radioButtonTicketAmountNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			radioButtonTicketAmountNo.Location = new System.Drawing.Point(48, 4);
			radioButtonTicketAmountNo.Name = "radioButtonTicketAmountNo";
			radioButtonTicketAmountNo.Size = new System.Drawing.Size(40, 15);
			radioButtonTicketAmountNo.TabIndex = 11;
			radioButtonTicketAmountNo.Text = "No";
			radioButtonTicketAmountYes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			radioButtonTicketAmountYes.Location = new System.Drawing.Point(4, 3);
			radioButtonTicketAmountYes.Name = "radioButtonTicketAmountYes";
			radioButtonTicketAmountYes.Size = new System.Drawing.Size(52, 20);
			radioButtonTicketAmountYes.TabIndex = 0;
			radioButtonTicketAmountYes.Text = "Yes";
			ultraGroupBox5.Controls.Add(radioButtonPassportHeldNo);
			ultraGroupBox5.Controls.Add(radioButtonPassportHeldYes);
			ultraGroupBox5.Location = new System.Drawing.Point(161, 180);
			ultraGroupBox5.Name = "ultraGroupBox5";
			ultraGroupBox5.Size = new System.Drawing.Size(95, 26);
			ultraGroupBox5.TabIndex = 10;
			radioButtonPassportHeldNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			radioButtonPassportHeldNo.Location = new System.Drawing.Point(45, 5);
			radioButtonPassportHeldNo.Name = "radioButtonPassportHeldNo";
			radioButtonPassportHeldNo.Size = new System.Drawing.Size(47, 17);
			radioButtonPassportHeldNo.TabIndex = 1;
			radioButtonPassportHeldNo.Text = "No";
			radioButtonPassportHeldYes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			radioButtonPassportHeldYes.Location = new System.Drawing.Point(3, 5);
			radioButtonPassportHeldYes.Name = "radioButtonPassportHeldYes";
			radioButtonPassportHeldYes.Size = new System.Drawing.Size(64, 16);
			radioButtonPassportHeldYes.TabIndex = 0;
			radioButtonPassportHeldYes.Text = "Yes";
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(262, 188);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(105, 13);
			mmLabel39.TabIndex = 407;
			mmLabel39.Text = "Ticket Amount paid :";
			mmLabel41.AutoSize = true;
			mmLabel41.BackColor = System.Drawing.Color.Transparent;
			mmLabel41.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel41.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel41.IsFieldHeader = false;
			mmLabel41.IsRequired = false;
			mmLabel41.Location = new System.Drawing.Point(10, 186);
			mmLabel41.Name = "mmLabel41";
			mmLabel41.PenWidth = 1f;
			mmLabel41.ShowBorder = false;
			mmLabel41.Size = new System.Drawing.Size(113, 13);
			mmLabel41.TabIndex = 406;
			mmLabel41.Text = "Passport Held in IMG :";
			textBoxAbscondingRefNo.BackColor = System.Drawing.Color.White;
			textBoxAbscondingRefNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAbscondingRefNo.CustomReportFieldName = "";
			textBoxAbscondingRefNo.CustomReportKey = "";
			textBoxAbscondingRefNo.CustomReportValueType = 1;
			textBoxAbscondingRefNo.IsComboTextBox = false;
			textBoxAbscondingRefNo.IsModified = false;
			textBoxAbscondingRefNo.Location = new System.Drawing.Point(395, 131);
			textBoxAbscondingRefNo.MaxLength = 30;
			textBoxAbscondingRefNo.Name = "textBoxAbscondingRefNo";
			textBoxAbscondingRefNo.Size = new System.Drawing.Size(167, 20);
			textBoxAbscondingRefNo.TabIndex = 8;
			mmLabel42.AutoSize = true;
			mmLabel42.BackColor = System.Drawing.Color.Transparent;
			mmLabel42.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel42.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel42.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel42.IsFieldHeader = false;
			mmLabel42.IsRequired = false;
			mmLabel42.Location = new System.Drawing.Point(292, 134);
			mmLabel42.Name = "mmLabel42";
			mmLabel42.PenWidth = 1f;
			mmLabel42.ShowBorder = false;
			mmLabel42.Size = new System.Drawing.Size(105, 13);
			mmLabel42.TabIndex = 405;
			mmLabel42.Text = "Absconding Ref No :";
			dateTimePickerLastWorkingDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerLastWorkingDate.Location = new System.Drawing.Point(493, 34);
			dateTimePickerLastWorkingDate.Name = "dateTimePickerLastWorkingDate";
			dateTimePickerLastWorkingDate.Size = new System.Drawing.Size(113, 20);
			dateTimePickerLastWorkingDate.TabIndex = 14;
			dateTimePickerLastWorkingDate.Value = new System.DateTime(2015, 5, 18, 16, 35, 7, 0);
			labelLastworking.AutoSize = true;
			labelLastworking.BackColor = System.Drawing.Color.Transparent;
			labelLastworking.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelLastworking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelLastworking.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelLastworking.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelLastworking.IsFieldHeader = false;
			labelLastworking.IsRequired = false;
			labelLastworking.Location = new System.Drawing.Point(388, 35);
			labelLastworking.Name = "labelLastworking";
			labelLastworking.PenWidth = 1f;
			labelLastworking.ShowBorder = false;
			labelLastworking.Size = new System.Drawing.Size(102, 13);
			labelLastworking.TabIndex = 402;
			labelLastworking.Text = "Last Working Date :";
			dateTimeAbsRegIMGDate.Checked = false;
			dateTimeAbsRegIMGDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeAbsRegIMGDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeAbsRegIMGDate.Location = new System.Drawing.Point(162, 132);
			dateTimeAbsRegIMGDate.Name = "dateTimeAbsRegIMGDate";
			dateTimeAbsRegIMGDate.ShowCheckBox = true;
			dateTimeAbsRegIMGDate.Size = new System.Drawing.Size(128, 20);
			dateTimeAbsRegIMGDate.TabIndex = 7;
			dateTimeAbsRegIMGDate.Value = new System.DateTime(0L);
			dateTimeAbscondingRegMOLDate.Checked = false;
			dateTimeAbscondingRegMOLDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeAbscondingRegMOLDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeAbscondingRegMOLDate.Location = new System.Drawing.Point(162, 60);
			dateTimeAbscondingRegMOLDate.Name = "dateTimeAbscondingRegMOLDate";
			dateTimeAbscondingRegMOLDate.ShowCheckBox = true;
			dateTimeAbscondingRegMOLDate.Size = new System.Drawing.Size(128, 20);
			dateTimeAbscondingRegMOLDate.TabIndex = 4;
			dateTimeAbscondingRegMOLDate.Value = new System.DateTime(0L);
			dateTimeRealAbscondingDate.Checked = false;
			dateTimeRealAbscondingDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeRealAbscondingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeRealAbscondingDate.Location = new System.Drawing.Point(161, 36);
			dateTimeRealAbscondingDate.Name = "dateTimeRealAbscondingDate";
			dateTimeRealAbscondingDate.ShowCheckBox = true;
			dateTimeRealAbscondingDate.Size = new System.Drawing.Size(129, 20);
			dateTimeRealAbscondingDate.TabIndex = 3;
			dateTimeRealAbscondingDate.Value = new System.DateTime(0L);
			dateTimeAdviceReceivedOn.Checked = false;
			dateTimeAdviceReceivedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeAdviceReceivedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeAdviceReceivedOn.Location = new System.Drawing.Point(161, 12);
			dateTimeAdviceReceivedOn.Name = "dateTimeAdviceReceivedOn";
			dateTimeAdviceReceivedOn.ShowCheckBox = true;
			dateTimeAdviceReceivedOn.Size = new System.Drawing.Size(129, 20);
			dateTimeAdviceReceivedOn.TabIndex = 2;
			dateTimeAdviceReceivedOn.Value = new System.DateTime(0L);
			mmLabel43.AutoSize = true;
			mmLabel43.BackColor = System.Drawing.Color.Transparent;
			mmLabel43.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel43.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel43.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel43.IsFieldHeader = false;
			mmLabel43.IsRequired = false;
			mmLabel43.Location = new System.Drawing.Point(380, 14);
			mmLabel43.Name = "mmLabel43";
			mmLabel43.PenWidth = 1f;
			mmLabel43.ShowBorder = false;
			mmLabel43.Size = new System.Drawing.Size(110, 13);
			mmLabel43.TabIndex = 400;
			mmLabel43.Text = "Transaction Date :";
			dateTimeTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeTransactionDate.Location = new System.Drawing.Point(493, 12);
			dateTimeTransactionDate.Name = "dateTimeTransactionDate";
			dateTimeTransactionDate.Size = new System.Drawing.Size(113, 20);
			dateTimeTransactionDate.TabIndex = 13;
			dateTimeTransactionDate.Value = new System.DateTime(2015, 5, 18, 16, 35, 7, 0);
			mmLabel44.AutoSize = true;
			mmLabel44.BackColor = System.Drawing.Color.Transparent;
			mmLabel44.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel44.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel44.IsFieldHeader = false;
			mmLabel44.IsRequired = false;
			mmLabel44.Location = new System.Drawing.Point(10, 211);
			mmLabel44.Name = "mmLabel44";
			mmLabel44.PenWidth = 1f;
			mmLabel44.ShowBorder = false;
			mmLabel44.Size = new System.Drawing.Size(55, 13);
			mmLabel44.TabIndex = 396;
			mmLabel44.Text = "Remarks :";
			mmLabel45.AutoSize = true;
			mmLabel45.BackColor = System.Drawing.Color.Transparent;
			mmLabel45.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel45.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel45.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel45.IsFieldHeader = false;
			mmLabel45.IsRequired = false;
			mmLabel45.Location = new System.Drawing.Point(10, 136);
			mmLabel45.Name = "mmLabel45";
			mmLabel45.PenWidth = 1f;
			mmLabel45.ShowBorder = false;
			mmLabel45.Size = new System.Drawing.Size(148, 13);
			mmLabel45.TabIndex = 392;
			mmLabel45.Text = "Absconding Reg.Date (IMG) :";
			mmLabel46.AutoSize = true;
			mmLabel46.BackColor = System.Drawing.Color.Transparent;
			mmLabel46.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel46.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel46.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel46.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel46.IsFieldHeader = false;
			mmLabel46.IsRequired = false;
			mmLabel46.Location = new System.Drawing.Point(10, 64);
			mmLabel46.Name = "mmLabel46";
			mmLabel46.PenWidth = 1f;
			mmLabel46.ShowBorder = false;
			mmLabel46.Size = new System.Drawing.Size(147, 13);
			mmLabel46.TabIndex = 390;
			mmLabel46.Text = "Absconding Reg.Date(MOL) :";
			textBoxMBRefNumber.BackColor = System.Drawing.Color.White;
			textBoxMBRefNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxMBRefNumber.CustomReportFieldName = "";
			textBoxMBRefNumber.CustomReportKey = "";
			textBoxMBRefNumber.CustomReportValueType = 1;
			textBoxMBRefNumber.IsComboTextBox = false;
			textBoxMBRefNumber.IsModified = false;
			textBoxMBRefNumber.Location = new System.Drawing.Point(162, 108);
			textBoxMBRefNumber.MaxLength = 30;
			textBoxMBRefNumber.Name = "textBoxMBRefNumber";
			textBoxMBRefNumber.Size = new System.Drawing.Size(205, 20);
			textBoxMBRefNumber.TabIndex = 6;
			mmLabel47.AutoSize = true;
			mmLabel47.BackColor = System.Drawing.Color.Transparent;
			mmLabel47.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel47.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel47.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel47.IsFieldHeader = false;
			mmLabel47.IsRequired = false;
			mmLabel47.Location = new System.Drawing.Point(10, 112);
			mmLabel47.Name = "mmLabel47";
			mmLabel47.PenWidth = 1f;
			mmLabel47.ShowBorder = false;
			mmLabel47.Size = new System.Drawing.Size(64, 13);
			mmLabel47.TabIndex = 389;
			mmLabel47.Text = "MB Ref No :";
			mmLabel52.AutoSize = true;
			mmLabel52.BackColor = System.Drawing.Color.Transparent;
			mmLabel52.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel52.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel52.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel52.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel52.IsFieldHeader = false;
			mmLabel52.IsRequired = false;
			mmLabel52.Location = new System.Drawing.Point(10, 40);
			mmLabel52.Name = "mmLabel52";
			mmLabel52.PenWidth = 1f;
			mmLabel52.ShowBorder = false;
			mmLabel52.Size = new System.Drawing.Size(119, 13);
			mmLabel52.TabIndex = 386;
			mmLabel52.Text = "Real Absconding Date :";
			labelAdviceReceivedOn.AutoSize = true;
			labelAdviceReceivedOn.BackColor = System.Drawing.Color.Transparent;
			labelAdviceReceivedOn.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelAdviceReceivedOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelAdviceReceivedOn.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelAdviceReceivedOn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelAdviceReceivedOn.IsFieldHeader = false;
			labelAdviceReceivedOn.IsRequired = false;
			labelAdviceReceivedOn.Location = new System.Drawing.Point(10, 16);
			labelAdviceReceivedOn.Name = "labelAdviceReceivedOn";
			labelAdviceReceivedOn.PenWidth = 1f;
			labelAdviceReceivedOn.ShowBorder = false;
			labelAdviceReceivedOn.Size = new System.Drawing.Size(108, 13);
			labelAdviceReceivedOn.TabIndex = 385;
			labelAdviceReceivedOn.Text = "Advice Received on :";
			textBoxRemarks.BackColor = System.Drawing.Color.White;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.IsModified = false;
			textBoxRemarks.Location = new System.Drawing.Point(162, 210);
			textBoxRemarks.MaxLength = 1000;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxRemarks.Size = new System.Drawing.Size(400, 83);
			textBoxRemarks.TabIndex = 12;
			labelAbsconded.AutoSize = true;
			labelAbsconded.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelAbsconded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelAbsconded.ForeColor = System.Drawing.Color.Red;
			labelAbsconded.IsFieldHeader = false;
			labelAbsconded.IsRequired = false;
			labelAbsconded.Location = new System.Drawing.Point(287, 12);
			labelAbsconded.Name = "labelAbsconded";
			labelAbsconded.PenWidth = 1f;
			labelAbsconded.ShowBorder = false;
			labelAbsconded.Size = new System.Drawing.Size(156, 13);
			labelAbsconded.TabIndex = 414;
			labelAbsconded.Text = "Employee is already absconded";
			labelAbsconded.Visible = false;
			mmTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			mmTextBox1.CustomReportFieldName = "";
			mmTextBox1.CustomReportKey = "";
			mmTextBox1.CustomReportValueType = 1;
			mmTextBox1.IsComboTextBox = false;
			mmTextBox1.IsModified = false;
			mmTextBox1.Location = new System.Drawing.Point(476, 10);
			mmTextBox1.MaxLength = 15;
			mmTextBox1.Name = "mmTextBox1";
			mmTextBox1.ReadOnly = true;
			mmTextBox1.Size = new System.Drawing.Size(86, 20);
			mmTextBox1.TabIndex = 411;
			mmTextBox1.TabStop = false;
			mmTextBox1.Visible = false;
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = textBoxEmployeeName;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance3;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(114, 10);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(164, 20);
			comboBoxEmployee.TabIndex = 409;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxEmployeeName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxEmployeeName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.IsModified = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(114, 34);
			textBoxEmployeeName.MaxLength = 20;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(269, 20);
			textBoxEmployeeName.TabIndex = 410;
			textBoxEmployeeName.TabStop = false;
			mmLabel53.AutoSize = true;
			mmLabel53.BackColor = System.Drawing.Color.Transparent;
			mmLabel53.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel53.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel53.IsFieldHeader = false;
			mmLabel53.IsRequired = false;
			mmLabel53.Location = new System.Drawing.Point(13, 38);
			mmLabel53.Name = "mmLabel53";
			mmLabel53.PenWidth = 1f;
			mmLabel53.ShowBorder = false;
			mmLabel53.Size = new System.Drawing.Size(90, 13);
			mmLabel53.TabIndex = 413;
			mmLabel53.Text = "Employee Name :";
			formManager1.BackColor = System.Drawing.Color.RosyBrown;
			formManager1.Dock = System.Windows.Forms.DockStyle.Left;
			formManager1.IsForcedDirty = false;
			formManager1.Location = new System.Drawing.Point(0, 0);
			formManager1.MaximumSize = new System.Drawing.Size(20, 20);
			formManager1.MinimumSize = new System.Drawing.Size(20, 20);
			formManager1.Name = "formManager1";
			formManager1.Size = new System.Drawing.Size(20, 20);
			formManager1.TabIndex = 146;
			formManager1.Text = "formManager1";
			formManager1.Visible = false;
			ultraTabPageControl4.Controls.Add(comboBoxDocumentType);
			ultraTabPageControl4.Controls.Add(dataGrid);
			ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(622, 450);
			comboBoxDocumentType.Assigned = false;
			comboBoxDocumentType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDocumentType.CustomReportFieldName = "";
			comboBoxDocumentType.CustomReportKey = "";
			comboBoxDocumentType.CustomReportValueType = 1;
			comboBoxDocumentType.DescriptionTextBox = null;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDocumentType.DisplayLayout.Appearance = appearance15;
			comboBoxDocumentType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDocumentType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDocumentType.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDocumentType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxDocumentType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDocumentType.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxDocumentType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDocumentType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDocumentType.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDocumentType.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxDocumentType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDocumentType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDocumentType.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDocumentType.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxDocumentType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDocumentType.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDocumentType.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxDocumentType.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxDocumentType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDocumentType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxDocumentType.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxDocumentType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDocumentType.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			comboBoxDocumentType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDocumentType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDocumentType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDocumentType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDocumentType.Editable = true;
			comboBoxDocumentType.FilterString = "";
			comboBoxDocumentType.HasAllAccount = false;
			comboBoxDocumentType.HasCustom = false;
			comboBoxDocumentType.IsDataLoaded = false;
			comboBoxDocumentType.Location = new System.Drawing.Point(249, 195);
			comboBoxDocumentType.MaxDropDownItems = 12;
			comboBoxDocumentType.Name = "comboBoxDocumentType";
			comboBoxDocumentType.ShowInactiveItems = false;
			comboBoxDocumentType.ShowQuickAdd = true;
			comboBoxDocumentType.Size = new System.Drawing.Size(125, 20);
			comboBoxDocumentType.TabIndex = 19;
			comboBoxDocumentType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDocumentType.Visible = false;
			dataGrid.AllowAddNew = false;
			dataGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGrid.DisplayLayout.Appearance = appearance27;
			dataGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			dataGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			dataGrid.DisplayLayout.MaxColScrollRegions = 1;
			dataGrid.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGrid.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGrid.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			dataGrid.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGrid.DisplayLayout.Override.CellAppearance = appearance34;
			dataGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGrid.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			dataGrid.DisplayLayout.Override.HeaderAppearance = appearance36;
			dataGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			dataGrid.DisplayLayout.Override.RowAppearance = appearance37;
			dataGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			dataGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGrid.IncludeLotItems = false;
			dataGrid.LoadLayoutFailed = false;
			dataGrid.Location = new System.Drawing.Point(0, 3);
			dataGrid.Name = "dataGrid";
			dataGrid.ShowClearMenu = true;
			dataGrid.ShowDeleteMenu = true;
			dataGrid.ShowInsertMenu = true;
			dataGrid.ShowMoveRowsMenu = true;
			dataGrid.Size = new System.Drawing.Size(624, 443);
			dataGrid.TabIndex = 18;
			dataGrid.Text = "dataEntryGrid1";
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 504);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(626, 40);
			panelButtons.TabIndex = 11;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(214, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(112, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(626, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.BackColor = System.Drawing.Color.DarkGray;
			buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClose.Location = new System.Drawing.Point(516, 8);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(96, 24);
			buttonClose.TabIndex = 3;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = false;
			buttonClose.Click += new System.EventHandler(xpButton1_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(12, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
			{
				toolStripButtonPrint1,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator2,
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator3,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator1,
				toolStripButtonInformation
			});
			toolStrip2.Location = new System.Drawing.Point(0, 0);
			toolStrip2.Name = "toolStrip2";
			toolStrip2.Size = new System.Drawing.Size(626, 31);
			toolStrip2.TabIndex = 367;
			toolStrip2.Text = "toolStrip2";
			toolStripButtonPrint1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint1.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint1.Name = "toolStripButtonPrint1";
			toolStripButtonPrint1.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint1.Text = "&Print";
			toolStripButtonPrint1.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint1.Visible = false;
			toolStripButtonPrint1.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last Record";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator2.Visible = false;
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl4);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 31);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(626, 473);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			ultraTab.TabPage = ultraTabPageControl1;
			ultraTab.Text = "General";
			appearance39.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab2.ClientAreaAppearance = appearance39;
			ultraTab2.TabPage = ultraTabPageControl4;
			ultraTab2.Text = "Documents";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(622, 450);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 25);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 307;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(626, 544);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(toolStrip2);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "EmployeeAbscondingEntryForm";
			Text = "Employee Absconding Entry";
			base.Load += new System.EventHandler(EmployeeCancellationForm_Load);
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)panelCancel).EndInit();
			panelCancel.ResumeLayout(false);
			panelCancel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).EndInit();
			ultraGroupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			ultraTabPageControl4.ResumeLayout(false);
			ultraTabPageControl4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDocumentType).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
			panelButtons.ResumeLayout(false);
			toolStrip2.ResumeLayout(false);
			toolStrip2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
		}

		private void AddEvents()
		{
			base.Load += EmployeeCancellationForm_Load;
			comboBoxEmployee.SelectedIndexChanged += comboBoxEmployee_SelectedIndexChanged;
		}

		private void checkBoxIsCancelled_CheckStateChanged(object sender, EventArgs e)
		{
		}

		private void SetupGrid()
		{
			try
			{
				dataGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Document Type");
				dataTable.Columns.Add("Document Number").Unique = true;
				dataTable.Columns.Add("Issue Date");
				dataTable.Columns.Add("Issue Place");
				dataTable.Columns.Add("Expiry Date");
				dataTable.Columns.Add("Remarks");
				dataGrid.DataSource = dataTable;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Number"].MaxLength = 30;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Number"].CharacterCasing = CharacterCasing.Upper;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Type"].CharacterCasing = CharacterCasing.Upper;
				dataGrid.DisplayLayout.Bands[0].Columns["Issue Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
				dataGrid.DisplayLayout.Bands[0].Columns["Expiry Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
				dataGrid.DisplayLayout.Bands[0].Columns["Issue Place"].MaxLength = 15;
				dataGrid.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 255;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Type"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Type"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGrid.DisplayLayout.Bands[0].Columns["Document Type"].ValueList = comboBoxDocumentType;
			}
			catch (Exception e)
			{
				dataGrid.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void EmployeeCancellationForm_Load(object sender, EventArgs e)
		{
			try
			{
				dateTimeTransactionDate.Value = DateTime.Now;
				dateTimePickerLastWorkingDate.Value = DateTime.Now;
				dataGrid.SetupUI();
				SetupGrid();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					Init();
					ClearForm(newRecord: true);
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
			}
		}

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ClearForm(newRecord: false);
				textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
				DataSet employeeBriefInfoAbsconding = Factory.EmployeeSystem.GetEmployeeBriefInfoAbsconding(comboBoxEmployee.SelectedID);
				if (employeeBriefInfoAbsconding != null && employeeBriefInfoAbsconding.Tables.Count > 0 && employeeBriefInfoAbsconding.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = employeeBriefInfoAbsconding.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["ActivityID"].ToString();
					textBoxFromLocation.Text = dataRow["WorkLocationName"].ToString();
					textBoxFromDivision.Text = dataRow["DivisionName"].ToString();
					textBoxFromDepartment.Text = dataRow["DepartmentName"].ToString();
					textBoxDesignation.Text = dataRow["PositionName"].ToString();
					if (dataRow["Photo"] != DBNull.Value)
					{
						ImageConverter imageConverter = new ImageConverter();
						pictureBoxPhoto.Image = (Image)imageConverter.ConvertFrom(dataRow["Photo"]);
						pictureBoxPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
					}
				}
				DataSet employeeDocumentsByEmployeeID = Factory.EmployeeDocumentSystem.GetEmployeeDocumentsByEmployeeID(comboBoxEmployee.SelectedID);
				if (employeeDocumentsByEmployeeID != null && employeeDocumentsByEmployeeID.Tables.Count > 0 && employeeDocumentsByEmployeeID.Tables[0].Rows.Count > 0)
				{
					DataTable dataTable = dataGrid.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in employeeDocumentsByEmployeeID.Tables["Employee_Document"].Rows)
					{
						dataTable.Rows.Add(row["DocumentTypeID"].ToString(), row["DocumentNumber"].ToString(), row["IssueDate"].ToString(), row["IssuePlace"].ToString(), row["ExpiryDate"].ToString(), row["Remarks"].ToString());
					}
					dataTable.AcceptChanges();
				}
				if (textBoxCode.Text != string.Empty)
				{
					LoadData(int.Parse(textBoxCode.Text));
				}
				else
				{
					isNewRecord = true;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables["Employee_Activity"].Rows[0];
				textBoxCode.Text = dataRow["ActivityID"].ToString();
				comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
				textBoxRemarks.Text = dataRow["Note"].ToString();
				if (dataRow["TransactionDate"] != DBNull.Value)
				{
					dateTimeTransactionDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
				}
				dataRow = currentData.Tables["Employee_Absconding"].Rows[0];
				if (dataRow["AdviceReceivedOn"] != DBNull.Value)
				{
					dateTimeAdviceReceivedOn.Value = DateTime.Parse(dataRow["AdviceReceivedOn"].ToString());
					dateTimeAdviceReceivedOn.Checked = true;
				}
				else
				{
					dateTimeAdviceReceivedOn.Checked = false;
				}
				if (dataRow["RealAbscondingDate"] != DBNull.Value)
				{
					dateTimeRealAbscondingDate.Value = DateTime.Parse(dataRow["RealAbscondingDate"].ToString());
					dateTimeRealAbscondingDate.Checked = true;
				}
				else
				{
					dateTimeRealAbscondingDate.Checked = false;
				}
				if (dataRow["AbscondingRegDateMOL"] != DBNull.Value)
				{
					dateTimeAbscondingRegMOLDate.Value = DateTime.Parse(dataRow["AbscondingRegDateMOL"].ToString());
					dateTimeAbscondingRegMOLDate.Checked = true;
				}
				else
				{
					dateTimeAbscondingRegMOLDate.Checked = false;
				}
				textBoxMBRefNumber.Text = dataRow["MBReferenceNo"].ToString();
				textBoxAbscondingRefNo.Text = dataRow["AbscondingReferenceNo"].ToString();
				if (dataRow["AbscondingRegDateIMG"] != DBNull.Value)
				{
					dateTimeAbsRegIMGDate.Value = DateTime.Parse(dataRow["AbscondingRegDateIMG"].ToString());
					dateTimeAbsRegIMGDate.Checked = true;
				}
				else
				{
					dateTimeAbsRegIMGDate.Checked = false;
				}
				if (dataRow["MOLCancellationDate"] != DBNull.Value)
				{
					dateTimeMOLcancellation.Value = DateTime.Parse(dataRow["MOLCancellationDate"].ToString());
					dateTimeMOLcancellation.Checked = true;
				}
				else
				{
					dateTimeMOLcancellation.Checked = false;
				}
				if (dataRow["IMGCancellationDate"] != DBNull.Value)
				{
					dateTimeIMGCancellation.Value = DateTime.Parse(dataRow["IMGCancellationDate"].ToString());
					dateTimeIMGCancellation.Checked = true;
				}
				else
				{
					dateTimeIMGCancellation.Checked = false;
				}
				if (dataRow["LastWorkingDate"] != DBNull.Value)
				{
					dateTimePickerLastWorkingDate.Value = DateTime.Parse(dataRow["LastWorkingDate"].ToString());
				}
				if (dataRow["PassportHeldInIMG"].ToString().ToUpper() == "y".ToUpper())
				{
					radioButtonPassportHeldYes.Checked = true;
				}
				else
				{
					radioButtonPassportHeldNo.Checked = true;
				}
				if (dataRow["TicketAmountPaid"].ToString().ToUpper() == "y".ToUpper())
				{
					radioButtonTicketAmountYes.Checked = true;
				}
				else
				{
					radioButtonTicketAmountNo.Checked = true;
				}
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeActivityData(EmployeeActivityTypes.Absconding);
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeActivityTable.Rows[0] : currentData.EmployeeActivityTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["ActivityType"] = (byte)17;
				dataRow["TransactionDate"] = dateTimeTransactionDate.Value;
				dataRow["Note"] = textBoxRemarks.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeActivityTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.EmployeeAbscondingTable.Rows[0] : currentData.EmployeeAbscondingTable.NewRow());
				if (dateTimeAdviceReceivedOn.Checked)
				{
					dataRow["AdviceReceivedOn"] = dateTimeAdviceReceivedOn.Value;
				}
				else
				{
					dataRow["AdviceReceivedOn"] = DBNull.Value;
				}
				if (dateTimeRealAbscondingDate.Checked)
				{
					dataRow["RealAbscondingDate"] = dateTimeRealAbscondingDate.Value;
				}
				else
				{
					dataRow["RealAbscondingDate"] = DBNull.Value;
				}
				if (dateTimeAbscondingRegMOLDate.Checked)
				{
					dataRow["AbscondingRegDateMOL"] = dateTimeAbscondingRegMOLDate.Value;
				}
				else
				{
					dataRow["AbscondingRegDateMOL"] = DBNull.Value;
				}
				dataRow["MBReferenceNo"] = textBoxMBRefNumber.Text;
				dataRow["AbscondingReferenceNo"] = textBoxAbscondingRefNo.Text;
				if (radioButtonPassportHeldYes.Checked)
				{
					dataRow["PassportHeldInIMG"] = "Y".ToString();
				}
				else
				{
					dataRow["PassportHeldInIMG"] = "N".ToString();
				}
				if (radioButtonTicketAmountYes.Checked)
				{
					dataRow["TicketAmountPaid"] = "Y".ToString();
				}
				else
				{
					dataRow["TicketAmountPaid"] = "N".ToString();
				}
				if (dateTimeAbsRegIMGDate.Checked)
				{
					dataRow["AbscondingRegDateIMG"] = dateTimeAbsRegIMGDate.Value;
				}
				else
				{
					dataRow["AbscondingRegDateIMG"] = DBNull.Value;
				}
				if (dateTimeMOLcancellation.Checked)
				{
					dataRow["MOLCancellationDate"] = dateTimeMOLcancellation.Value;
				}
				else
				{
					dataRow["MOLCancellationDate"] = DBNull.Value;
				}
				if (dateTimeIMGCancellation.Checked)
				{
					dataRow["IMGCancellationDate"] = dateTimeIMGCancellation.Value;
				}
				else
				{
					dataRow["IMGCancellationDate"] = DBNull.Value;
				}
				dataRow["LastWorkingDate"] = dateTimePickerLastWorkingDate.Value;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeAbscondingTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			try
			{
				if (!screenRight.New && isNewRecord)
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionNew);
					return false;
				}
				if (!screenRight.Edit && !isNewRecord)
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
					return false;
				}
				if (comboBoxEmployee.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Please enter required fields.");
					return false;
				}
				if (dateTimePickerLastWorkingDate.Value >= dateTimeTransactionDate.Value)
				{
					ErrorHelper.InformationMessage("Last working Date should be Less than Transaction Date");
					return false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			return true;
		}

		private bool SaveData()
		{
			if (!IsNewRecord)
			{
				DialogResult dialogResult = DialogResult.Yes;
				switch (ErrorHelper.QuestionMessageYesNoCancel("Do you want to abscond this employee ?"))
				{
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				}
			}
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
		{
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				bool flag = (!isNewRecord) ? Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Absconding, isUpdate: true) : Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Absconding, isUpdate: false);
				dateTimePickerLastWorkingDate.Value = DateTime.Now;
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					IsNewRecord = true;
					ClearForm(newRecord: true);
				}
				return flag;
			}
			catch (CompanyException ex)
			{
				ErrorHelper.ErrorMessage(ex.Message);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void LoadData(int id)
		{
			try
			{
				currentData = Factory.EmployeeActivitySystem.GetEmployeeActivityByID(id);
				if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
				{
					ClearForm(newRecord: true);
					IsNewRecord = true;
					textBoxCode.Text = id.ToString();
					textBoxCode.Focus();
				}
				else
				{
					FillData();
					IsNewRecord = false;
					formManager.ResetDirty();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Sales;
		}

		public static int GetScreenID()
		{
			return 7003;
		}

		public void RefreshData()
		{
			Refresh();
			if (FormActivator.ProgramLoaded)
			{
				_ = Global.ConStatus;
				_ = 2;
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetPreviousID("Employee_Absconding", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetNextID("Employee_Absconding", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetLastID("Employee_Absconding", "ActivityID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetFirstID("Employee_Absconding", "ActivityID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ClearForm(bool newRecord)
		{
			if (newRecord)
			{
				comboBoxEmployee.SelectedID = "";
			}
			textBoxCode.Clear();
			textBoxEmployeeName.Clear();
			textBoxDesignation.Clear();
			textBoxFromDepartment.Clear();
			textBoxFromDivision.Clear();
			textBoxFromLocation.Clear();
			dateTimeTransactionDate.Value = DateTime.Now;
			MMSDateTimePicker mMSDateTimePicker = dateTimeAdviceReceivedOn;
			MMSDateTimePicker mMSDateTimePicker2 = dateTimeRealAbscondingDate;
			MMSDateTimePicker mMSDateTimePicker3 = dateTimeAbscondingRegMOLDate;
			MMSDateTimePicker mMSDateTimePicker4 = dateTimeAbsRegIMGDate;
			MMSDateTimePicker mMSDateTimePicker5 = dateTimeMOLcancellation;
			bool flag2 = dateTimeIMGCancellation.Checked = false;
			bool flag4 = mMSDateTimePicker5.Checked = flag2;
			bool flag6 = mMSDateTimePicker4.Checked = flag4;
			bool flag8 = mMSDateTimePicker3.Checked = flag6;
			bool @checked = mMSDateTimePicker2.Checked = flag8;
			mMSDateTimePicker.Checked = @checked;
			pictureBoxPhoto.Image = null;
			textBoxMBRefNumber.Clear();
			textBoxAbscondingRefNo.Clear();
			textBoxRemarks.Clear();
			dateTimeTransactionDate.Value = DateTime.Now;
			dateTimePickerLastWorkingDate.Value = DateTime.Now;
			radioButtonPassportHeldNo.Checked = true;
			radioButtonTicketAmountNo.Checked = true;
			(dataGrid.DataSource as DataTable).Rows.Clear();
			formManager.ResetDirty();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			ClearForm(newRecord: true);
			IsNewRecord = true;
		}

		public void OnActivated()
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		private void SalespersonGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		public bool CanClose()
		{
			if (IsDirty)
			{
				BringToFront();
				if (IsNewRecord)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
					{
					case DialogResult.Yes:
						if (!SaveData())
						{
							return false;
						}
						break;
					default:
						return false;
					case DialogResult.No:
						break;
					}
				}
				else if (!SaveData())
				{
					return false;
				}
			}
			return true;
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void linkLabelArea_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.EmployeeAbsconding);
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.EmployeeActivitySystem.DeleteActivity(textBoxCode.Text, EmployeeActivityTypes.Absconding);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm(newRecord: true);
				IsNewRecord = true;
			}
		}

		private void toolStripButtonOpenList_Click_1(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Employee);
		}

		private void panelCancel_Click(object sender, EventArgs e)
		{
		}

		private void EmployeeCancellationForm_Load_1(object sender, EventArgs e)
		{
		}

		private void dateTimePickerLastWorkingDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void ultraLinkVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Employee_Absconding", "ActivityID", toolStripTextBoxFind.Text.Trim()))
				{
					string text = toolStripTextBoxFind.Text.Trim();
					textBoxCode.Text = text;
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: false);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData()))
				{
					DataSet employeeActivityToPrint = Factory.EmployeeActivitySystem.GetEmployeeActivityToPrint(Convert.ToInt32(textBoxCode.Text));
					if (employeeActivityToPrint == null || employeeActivityToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(employeeActivityToPrint, "", "Absconding Employee", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = comboBoxEmployee.SelectedID + "-" + textBoxCode.Text;
					docManagementForm.EntityType = EntityTypesEnum.Absconding;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}
	}
}
