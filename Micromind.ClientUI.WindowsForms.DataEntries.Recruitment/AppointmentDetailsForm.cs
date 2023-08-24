using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Recruitment
{
	public class AppointmentDetailsForm : Form, IForm
	{
		private CandidateData currentData;

		private EmployeeData employeeData;

		private const string TABLENAME_CONST = "Candidate";

		private const string IDFIELD_CONST = "CandidateID";

		private const string PASSPORTFIELD_CONST = "PassportNo";

		private bool isNewRecord = true;

		private List<MMSDateTimePicker> dateTimePickersToValidate = new List<MMSDateTimePicker>();

		private List<MMSDateTimePicker> dateTimePickers = new List<MMSDateTimePicker>();

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton buttonClose;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private FormManager formManager;

		private UltraTabControl ultraTabControl;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private Panel panel1;

		private UltraTabPageControl tabPageUserDefined;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem dependentsToolStripMenuItem;

		private ToolStripMenuItem documentsToolStripMenuItem;

		private ToolStripMenuItem skillsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMLabel labelCustomerNameHeader;

		private UDFEntryControl udfEntryGrid;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private UltraTabPageControl ultraTabPageControl1;

		private OpenFileDialog openFileDialog1;

		private ToolStripButton toolStripButtonEmployee;

		private UltraTabPageControl ultraTabPageControl2;

		private UltraTabPageControl ultraTabPageControl3;

		private UltraTabPageControl ultraTabPageControl4;

		private UltraTabPageControl ultraTabPageControl5;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel23;

		private MMTextBox textBoxComment;

		private MMLabel mmLabel20;

		private MMTextBox textBoxPostalCode;

		private MMLabel mmLabel18;

		private MMTextBox textBoxEmail;

		private MMLabel mmLabel17;

		private MMTextBox textBoxMobile;

		private MMLabel mmLabel16;

		private MMTextBox textBoxFax;

		private MMLabel mmLabel15;

		private MMTextBox textBoxPhone2;

		private MMLabel mmLabel14;

		private MMTextBox textBoxPhone1;

		private MMLabel mmLabel12;

		private MMTextBox textBoxCountry;

		private MMLabel mmLabel11;

		private MMTextBox textBoxState;

		private MMLabel mmLabel13;

		private MMTextBox textBoxCity;

		private MMTextBox textBoxAddress3;

		private MMTextBox textBoxAddress2;

		private MMLabel mmLabel10;

		private MMTextBox textBoxAddress1;

		private MMLabel mmLabel8;

		private MMTextBox textBoxAddressID;

		private Panel panelGeneral;

		private MMLabel mmLabel69;

		private PictureBox pictureBoxNoImage;

		private UltraFormattedLinkLabel linkLoadImage;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel54;

		private MMLabel mmLabel53;

		private MMTextBox textBoxPassportNo;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private PictureBox pictureBoxPhoto;

		private MMTextBox textBoxPPAddress;

		private MMLabel mmLabel21;

		private MMTextBox textBoxBloodGroup;

		private ReligionComboBox comboBoxReligion;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MaritalStatusComboBox comboBoxMaritalStatus;

		private MMLabel mmLabel47;

		private MMLabel mmLabel19;

		private MMTextBox textBoxSpouseName;

		private MMLabel mmLabel9;

		private MMTextBox textBoxMotherName;

		private MMLabel mmLabel6;

		private MMTextBox textBoxFatherName;

		private MMLabel mmLabel4;

		private MMSDateTimePicker dateTimePPExpiryDate;

		private MMLabel mmLabel3;

		private MMSDateTimePicker dateTimePPIssueDate;

		private MMTextBox textBoxPPIssuePlace;

		private MMLabel mmLabel1;

		private MMLabel mmLabel2;

		private MMTextBox textBoxBirthPlace;

		private MMLabel mmLabel33;

		private MMLabel mmLabel51;

		private MMSDateTimePicker dateTimeBirthDate;

		private MMTextBox textBoxAge;

		private MMLabel mmLabel31;

		private GenderComboBox comboBoxGender;

		private NationalityComboBox comboBoxNationality;

		private MMLabel mmLabel5;

		private MMLabel labelCandidateNumber;

		private MMLabel lblDescriptions;

		private MMTextBox textBoxCode;

		private MMTextBox textBoxSurName;

		private MMTextBox textBoxGivenName;

		private MMLabel labelGivenName;

		private Panel panelRecruitment;

		private MMLabel mmLabel81;

		private MMLabel mmLabel76;

		private TextBox textBoxLanguageName;

		private TextBox textBoxQualificationName;

		private NumericUpDown numericExperienceAbroad;

		private NumericUpDown numericExperienceLocal;

		private LanguageComboBox comboBoxLanguage;

		private QualificationComboBox comboBoxQualification;

		private MMLabel mmLabel75;

		private MMLabel mmLabel74;

		private MMLabel mmLabel73;

		private MMLabel mmLabel71;

		private TextBox textBoxActualDesignationName;

		private TextBox textBoxThroughAgentName;

		private PositionComboBox comboBoxPositionActual;

		private MMLabel mmLabel68;

		private AgentComboBox comboBoxAgentThrough;

		private MMLabel mmLabel55;

		private MMTextBox textBoxRemarks;

		private SelectionStatusComboBox comboBoxSelectionStatus;

		private MMLabel mmLabel39;

		private MMLabel mmLabel38;

		private MMTextBox textBoxSelectedAt;

		private MMSDateTimePicker dateTimeSelectedOn;

		private MMLabel mmLabel37;

		private MMLabel mmLabel36;

		private Panel panelVisaProcess;

		private UltraGroupBox panelVisaIMG;

		private MMLabel mmLabel77;

		private MMSDateTimePicker dateTimeVisaCopyToAgentOn;

		private ComboBox comboBoxVisaAppliedThroughIMG;

		private MMSDateTimePicker dateTimeVisaExpiryDate;

		private MMSDateTimePicker dateTimeVisaIssueDate;

		private MMLabel mmLabel87;

		private MMTextBox textBoxVisaIssuePlaceIMG;

		private MMLabel mmLabel86;

		private MMTextBox textBoxVisaNumber;

		private MMLabel mmLabel85;

		private MMLabel mmLabel84;

		private MMTextBox textBoxUIDNumberIMG;

		private MMLabel mmLabel83;

		private MMSDateTimePicker dateTimeVisaPostedOn;

		private MMLabel mmLabel26;

		private MMSDateTimePicker dateTimeApprovedOn;

		private MMLabel mmLabel28;

		private MMLabel mmLabel32;

		private UltraGroupBox panelVisaMOL;

		private TextBox textBoxVisaDesignationName;

		private PositionComboBox comboBoxPositionVisa;

		private MMLabel mmLabel67;

		private TextBox textBoxSponsorName;

		private SponsorComboBox comboBoxSponsor;

		private MMLabel mmLabel70;

		private ComboBox comboBoxBGTypeMOL;

		private MMLabel mmLabel82;

		private MMSDateTimePicker dateTimeApprovalFeePaidOnMOL;

		private MMTextBox textBoxTempWPNo;

		private MMLabel mmLabel80;

		private MMSDateTimePicker dateTimeApprovalValidTillMOL;

		private MMLabel mmLabel79;

		private MMSDateTimePicker dateTimeBGPaidOnMOL;

		private MMLabel mmLabel25;

		private MMLabel mmLabel24;

		private MMSDateTimePicker dateTimeApprovalDateMOL;

		private MMLabel mmLabel22;

		private MMTextBox textBoxMOLMBNo;

		private MMLabel mmLabel7;

		private MMSDateTimePicker dateTimeApplTypingDateMOL;

		private MMLabel mmLabel30;

		private Panel panelArrival;

		private XPButton buttonMakeEmployee;

		private MMTextBox textBoxEmployeeNo;

		private MMLabel mmLabel34;

		private MMSDateTimePicker dateTimeArrivedOn;

		private MMLabel mmLabel27;

		private Panel panelMedicalEmirates;

		private UltraGroupBox panelEmirates;

		private MMSDateTimePicker dateTimeValidityEID;

		private MMSDateTimePicker dateTimeCollectedOnEID;

		private MMLabel mmLabel42;

		private MMLabel mmLabel43;

		private MMSDateTimePicker dateTimeAttendedDateEID;

		private MMLabel mmLabel44;

		private MMTextBox textBoxNationalID;

		private MMLabel mmLabel45;

		private MMSDateTimePicker dateTimeApplTypingDateEID;

		private MMLabel mmLabel46;

		private UltraGroupBox panelMedicalDetail;

		private MMTextBox textBoxHealthCardNo;

		private MMLabel mmLabel72;

		private ComboBox comboBoxMedicalResult;

		private MMTextBox textBoxMedicalNote;

		private MMLabel mmLabel78;

		private MMLabel mmLabel89;

		private MMSDateTimePicker dateTimeMedicalCollectedOn;

		private MMLabel mmLabel41;

		private MMSDateTimePicker dateTimeMedicalAttendedOn;

		private MMLabel mmLabel40;

		private MMSDateTimePicker dateTimeMedicalTypingOn;

		private MMLabel mmLabel35;

		private Panel panelWPRP;

		private UltraGroupBox panelMedicalReport;

		private ComboBox comboBoxProcessType;

		private MMSDateTimePicker dateTimeRPExpiryDate;

		private MMLabel mmLabel56;

		private MMLabel mmLabel65;

		private MMSDateTimePicker dateTimeRPIssueDate;

		private MMLabel mmLabel64;

		private MMTextBox textBoxRPIssuePlace;

		private MMLabel mmLabel63;

		private MMSDateTimePicker dateTimePassportCollectedOnRP;

		private MMLabel mmLabel52;

		private MMSDateTimePicker dateTimeSubmittedZajilOnRP;

		private MMLabel mmLabel50;

		private MMSDateTimePicker dateTimeApplApprovedOnRP;

		private MMLabel mmLabel49;

		private MMSDateTimePicker dateTimeApplPostedOnRP;

		private MMLabel mmLabel48;

		private UltraGroupBox panelAGT;

		private MMTextBox textBoxPersonIDNo;

		private MMLabel mmLabel66;

		private MMTextBox textBoxWPIssuePlace;

		private MMSDateTimePicker dateTimeWPExpiryDate;

		private MMSDateTimePicker dateTimeWPIssueDate;

		private MMLabel mmLabel62;

		private MMLabel mmLabel61;

		private MMLabel mmLabel60;

		private MMTextBox textBoxWPNo;

		private MMSDateTimePicker dateTimeAGTSubmittedOn;

		private MMLabel mmLabel57;

		private MMLabel mmLabel58;

		private MMSDateTimePicker dateTimeAGTTypedOn;

		private MMLabel mmLabel59;

		private Label labelCategory;

		private MMTextBox textBoxAGTMBNo;

		private MMLabel mmLabel91;

		private MMLabel mmLabel90;

		private ComboBox comboBoxAGTType;

		private Label labelCancelled;

		private ToolStripButton toolStripButtonInformation;

		private TextBox textBoxDesignation;

		private PositionComboBox comboBoxDesignation;

		private VisaTypeComboBox comboBoxvisaType;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

		private UltraTabPageControl ultraTabPageControl6;

		private DataEntryGrid dataGridPayrollItem;

		private PayrollItemComboBox comboBoxPayrollItem;

		private AmountTextBox textBoxTotalSalary;

		private Label label7;

		private TextBox textBox1;

		private SponsorComboBox sponsorComboBox;

		private MMLabel mmLabel92;

		private PortComboBox comboBoxArrivalPort;

		private EmployeeTypeComboBox comboBoxCategory;

		private MMLabel mmLabel29;

		private MMLabel mmLabel88;

		private EmployeeGroupComboBox comboBoxGroup;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private MMLabel mmLabel93;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem morePrintToolStripMenuItem;

		private IContainer components;

		private WorkflowType _workFlowType = WorkflowType.None;

		private ScreenAccessRight screenRight;

		private bool isExist;

		private bool isCancelled;

		private string prefix = string.Empty;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5011;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public WorkflowType WorkflowStep
		{
			get
			{
				return _workFlowType;
			}
			set
			{
				_workFlowType = value;
			}
		}

		public bool IsExist
		{
			get
			{
				return isExist;
			}
			set
			{
				buttonMakeEmployee.Enabled = value;
				isExist = value;
			}
		}

		private bool IsCancelled
		{
			get
			{
				return isCancelled;
			}
			set
			{
				labelCancelled.Visible = value;
				ultraTabControl.Enabled = !value;
				buttonNew.Enabled = !value;
				buttonSave.Enabled = !value;
				isCancelled = value;
			}
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
					buttonNew.Text = UIMessages.ClearButtonText;
					buttonDelete.Enabled = false;
					textBoxAddressID.Enabled = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxAddressID.Enabled = false;
				}
				toolStripButtonAttach.Enabled = !value;
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
				ToolStripButton toolStripButton = toolStripButtonPrint;
				bool enabled = toolStripButtonPreview.Enabled = !isNewRecord;
				toolStripButton.Enabled = enabled;
			}
		}

		public AppointmentDetailsForm()
		{
			InitializeComponent();
			AddDateTimePickersToValidate();
		}

		private void AddDateTimePickersToValidate()
		{
			dateTimePickersToValidate.Add(dateTimeSelectedOn);
			dateTimePickersToValidate.Add(dateTimeApplTypingDateMOL);
			dateTimePickersToValidate.Add(dateTimeApprovalDateMOL);
			dateTimePickersToValidate.Add(dateTimeApprovalValidTillMOL);
			dateTimePickersToValidate.Add(dateTimeApprovalFeePaidOnMOL);
			dateTimePickersToValidate.Add(dateTimeBGPaidOnMOL);
			dateTimePickersToValidate.Add(dateTimeVisaPostedOn);
			dateTimePickersToValidate.Add(dateTimeApprovedOn);
			dateTimePickersToValidate.Add(dateTimeVisaIssueDate);
			dateTimePickersToValidate.Add(dateTimeVisaExpiryDate);
			dateTimePickersToValidate.Add(dateTimeArrivedOn);
			dateTimePickersToValidate.Add(dateTimeMedicalTypingOn);
			dateTimePickersToValidate.Add(dateTimeMedicalAttendedOn);
			dateTimePickersToValidate.Add(dateTimeMedicalCollectedOn);
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
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance148 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance149 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance150 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance151 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance152 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance153 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance154 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance155 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance156 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance157 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance158 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance159 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance160 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance161 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance162 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance163 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance164 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance165 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance166 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance167 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance168 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance169 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance170 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance171 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance172 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance173 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance174 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance175 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance176 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance177 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance178 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance179 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance180 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance181 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance182 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance183 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance184 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance185 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance186 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance187 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance188 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance189 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppointmentDetailsForm));
            this.ultraTabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.dataGridPayrollItem = new Micromind.DataControls.DataEntryGrid();
            this.comboBoxPayrollItem = new Micromind.DataControls.PayrollItemComboBox();
            this.textBoxTotalSalary = new Micromind.UISupport.AmountTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.panelGeneral = new System.Windows.Forms.Panel();
            this.mmLabel93 = new Micromind.UISupport.MMLabel();
            this.ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxGroup = new Micromind.DataControls.EmployeeGroupComboBox();
            this.ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxvisaType = new Micromind.DataControls.VisaTypeComboBox();
            this.textBoxDesignation = new System.Windows.Forms.TextBox();
            this.comboBoxDesignation = new Micromind.DataControls.PositionComboBox();
            this.mmLabel69 = new Micromind.UISupport.MMLabel();
            this.pictureBoxNoImage = new System.Windows.Forms.PictureBox();
            this.linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.textBoxNote = new Micromind.UISupport.MMTextBox();
            this.mmLabel54 = new Micromind.UISupport.MMLabel();
            this.mmLabel53 = new Micromind.UISupport.MMLabel();
            this.textBoxPassportNo = new Micromind.UISupport.MMTextBox();
            this.linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.pictureBoxPhoto = new System.Windows.Forms.PictureBox();
            this.textBoxPPAddress = new Micromind.UISupport.MMTextBox();
            this.mmLabel21 = new Micromind.UISupport.MMLabel();
            this.textBoxBloodGroup = new Micromind.UISupport.MMTextBox();
            this.comboBoxReligion = new Micromind.DataControls.ReligionComboBox();
            this.ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.comboBoxMaritalStatus = new Micromind.DataControls.MaritalStatusComboBox();
            this.mmLabel47 = new Micromind.UISupport.MMLabel();
            this.mmLabel19 = new Micromind.UISupport.MMLabel();
            this.textBoxSpouseName = new Micromind.UISupport.MMTextBox();
            this.mmLabel9 = new Micromind.UISupport.MMLabel();
            this.textBoxMotherName = new Micromind.UISupport.MMTextBox();
            this.mmLabel6 = new Micromind.UISupport.MMLabel();
            this.textBoxFatherName = new Micromind.UISupport.MMTextBox();
            this.mmLabel4 = new Micromind.UISupport.MMLabel();
            this.dateTimePPExpiryDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel3 = new Micromind.UISupport.MMLabel();
            this.dateTimePPIssueDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.textBoxPPIssuePlace = new Micromind.UISupport.MMTextBox();
            this.mmLabel1 = new Micromind.UISupport.MMLabel();
            this.mmLabel2 = new Micromind.UISupport.MMLabel();
            this.textBoxBirthPlace = new Micromind.UISupport.MMTextBox();
            this.mmLabel33 = new Micromind.UISupport.MMLabel();
            this.mmLabel51 = new Micromind.UISupport.MMLabel();
            this.dateTimeBirthDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.textBoxAge = new Micromind.UISupport.MMTextBox();
            this.mmLabel31 = new Micromind.UISupport.MMLabel();
            this.comboBoxGender = new Micromind.DataControls.GenderComboBox();
            this.comboBoxNationality = new Micromind.DataControls.NationalityComboBox();
            this.mmLabel5 = new Micromind.UISupport.MMLabel();
            this.labelCandidateNumber = new Micromind.UISupport.MMLabel();
            this.lblDescriptions = new Micromind.UISupport.MMLabel();
            this.textBoxCode = new Micromind.UISupport.MMTextBox();
            this.textBoxSurName = new Micromind.UISupport.MMTextBox();
            this.textBoxGivenName = new Micromind.UISupport.MMTextBox();
            this.labelGivenName = new Micromind.UISupport.MMLabel();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.panelArrival = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.sponsorComboBox = new Micromind.DataControls.SponsorComboBox();
            this.mmLabel92 = new Micromind.UISupport.MMLabel();
            this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.buttonMakeEmployee = new Micromind.UISupport.XPButton();
            this.textBoxEmployeeNo = new Micromind.UISupport.MMTextBox();
            this.mmLabel34 = new Micromind.UISupport.MMLabel();
            this.dateTimeArrivedOn = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel27 = new Micromind.UISupport.MMLabel();
            this.tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.panelRecruitment = new System.Windows.Forms.Panel();
            this.comboBoxArrivalPort = new Micromind.DataControls.PortComboBox();
            this.comboBoxCategory = new Micromind.DataControls.EmployeeTypeComboBox();
            this.mmLabel29 = new Micromind.UISupport.MMLabel();
            this.mmLabel88 = new Micromind.UISupport.MMLabel();
            this.mmLabel81 = new Micromind.UISupport.MMLabel();
            this.mmLabel76 = new Micromind.UISupport.MMLabel();
            this.textBoxLanguageName = new System.Windows.Forms.TextBox();
            this.textBoxQualificationName = new System.Windows.Forms.TextBox();
            this.numericExperienceAbroad = new System.Windows.Forms.NumericUpDown();
            this.numericExperienceLocal = new System.Windows.Forms.NumericUpDown();
            this.comboBoxLanguage = new Micromind.DataControls.LanguageComboBox();
            this.comboBoxQualification = new Micromind.DataControls.QualificationComboBox();
            this.mmLabel75 = new Micromind.UISupport.MMLabel();
            this.mmLabel74 = new Micromind.UISupport.MMLabel();
            this.mmLabel73 = new Micromind.UISupport.MMLabel();
            this.mmLabel71 = new Micromind.UISupport.MMLabel();
            this.textBoxActualDesignationName = new System.Windows.Forms.TextBox();
            this.textBoxThroughAgentName = new System.Windows.Forms.TextBox();
            this.comboBoxPositionActual = new Micromind.DataControls.PositionComboBox();
            this.mmLabel68 = new Micromind.UISupport.MMLabel();
            this.comboBoxAgentThrough = new Micromind.DataControls.AgentComboBox();
            this.mmLabel55 = new Micromind.UISupport.MMLabel();
            this.textBoxRemarks = new Micromind.UISupport.MMTextBox();
            this.comboBoxSelectionStatus = new Micromind.DataControls.SelectionStatusComboBox();
            this.mmLabel39 = new Micromind.UISupport.MMLabel();
            this.mmLabel38 = new Micromind.UISupport.MMLabel();
            this.textBoxSelectedAt = new Micromind.UISupport.MMTextBox();
            this.dateTimeSelectedOn = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel37 = new Micromind.UISupport.MMLabel();
            this.mmLabel36 = new Micromind.UISupport.MMLabel();
            this.ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.panelVisaProcess = new System.Windows.Forms.Panel();
            this.panelVisaIMG = new Infragistics.Win.Misc.UltraGroupBox();
            this.mmLabel77 = new Micromind.UISupport.MMLabel();
            this.dateTimeVisaCopyToAgentOn = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.comboBoxVisaAppliedThroughIMG = new System.Windows.Forms.ComboBox();
            this.dateTimeVisaExpiryDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.dateTimeVisaIssueDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel87 = new Micromind.UISupport.MMLabel();
            this.textBoxVisaIssuePlaceIMG = new Micromind.UISupport.MMTextBox();
            this.mmLabel86 = new Micromind.UISupport.MMLabel();
            this.textBoxVisaNumber = new Micromind.UISupport.MMTextBox();
            this.mmLabel85 = new Micromind.UISupport.MMLabel();
            this.mmLabel84 = new Micromind.UISupport.MMLabel();
            this.textBoxUIDNumberIMG = new Micromind.UISupport.MMTextBox();
            this.mmLabel83 = new Micromind.UISupport.MMLabel();
            this.dateTimeVisaPostedOn = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel26 = new Micromind.UISupport.MMLabel();
            this.dateTimeApprovedOn = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel28 = new Micromind.UISupport.MMLabel();
            this.mmLabel32 = new Micromind.UISupport.MMLabel();
            this.panelVisaMOL = new Infragistics.Win.Misc.UltraGroupBox();
            this.textBoxVisaDesignationName = new System.Windows.Forms.TextBox();
            this.comboBoxPositionVisa = new Micromind.DataControls.PositionComboBox();
            this.mmLabel67 = new Micromind.UISupport.MMLabel();
            this.textBoxSponsorName = new System.Windows.Forms.TextBox();
            this.comboBoxSponsor = new Micromind.DataControls.SponsorComboBox();
            this.mmLabel70 = new Micromind.UISupport.MMLabel();
            this.comboBoxBGTypeMOL = new System.Windows.Forms.ComboBox();
            this.mmLabel82 = new Micromind.UISupport.MMLabel();
            this.dateTimeApprovalFeePaidOnMOL = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.textBoxTempWPNo = new Micromind.UISupport.MMTextBox();
            this.mmLabel80 = new Micromind.UISupport.MMLabel();
            this.dateTimeApprovalValidTillMOL = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel79 = new Micromind.UISupport.MMLabel();
            this.dateTimeBGPaidOnMOL = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel25 = new Micromind.UISupport.MMLabel();
            this.mmLabel24 = new Micromind.UISupport.MMLabel();
            this.dateTimeApprovalDateMOL = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel22 = new Micromind.UISupport.MMLabel();
            this.textBoxMOLMBNo = new Micromind.UISupport.MMTextBox();
            this.mmLabel7 = new Micromind.UISupport.MMLabel();
            this.dateTimeApplTypingDateMOL = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel30 = new Micromind.UISupport.MMLabel();
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.panelMedicalEmirates = new System.Windows.Forms.Panel();
            this.panelEmirates = new Infragistics.Win.Misc.UltraGroupBox();
            this.dateTimeValidityEID = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.dateTimeCollectedOnEID = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel42 = new Micromind.UISupport.MMLabel();
            this.mmLabel43 = new Micromind.UISupport.MMLabel();
            this.dateTimeAttendedDateEID = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel44 = new Micromind.UISupport.MMLabel();
            this.textBoxNationalID = new Micromind.UISupport.MMTextBox();
            this.mmLabel45 = new Micromind.UISupport.MMLabel();
            this.dateTimeApplTypingDateEID = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel46 = new Micromind.UISupport.MMLabel();
            this.panelMedicalDetail = new Infragistics.Win.Misc.UltraGroupBox();
            this.textBoxHealthCardNo = new Micromind.UISupport.MMTextBox();
            this.mmLabel72 = new Micromind.UISupport.MMLabel();
            this.comboBoxMedicalResult = new System.Windows.Forms.ComboBox();
            this.mmLabel89 = new Micromind.UISupport.MMLabel();
            this.dateTimeMedicalCollectedOn = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel41 = new Micromind.UISupport.MMLabel();
            this.dateTimeMedicalAttendedOn = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel40 = new Micromind.UISupport.MMLabel();
            this.dateTimeMedicalTypingOn = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel35 = new Micromind.UISupport.MMLabel();
            this.textBoxMedicalNote = new Micromind.UISupport.MMTextBox();
            this.mmLabel78 = new Micromind.UISupport.MMLabel();
            this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.panelWPRP = new System.Windows.Forms.Panel();
            this.panelMedicalReport = new Infragistics.Win.Misc.UltraGroupBox();
            this.comboBoxProcessType = new System.Windows.Forms.ComboBox();
            this.dateTimeRPExpiryDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel56 = new Micromind.UISupport.MMLabel();
            this.mmLabel65 = new Micromind.UISupport.MMLabel();
            this.dateTimeRPIssueDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel64 = new Micromind.UISupport.MMLabel();
            this.textBoxRPIssuePlace = new Micromind.UISupport.MMTextBox();
            this.mmLabel63 = new Micromind.UISupport.MMLabel();
            this.dateTimePassportCollectedOnRP = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel52 = new Micromind.UISupport.MMLabel();
            this.dateTimeSubmittedZajilOnRP = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel50 = new Micromind.UISupport.MMLabel();
            this.dateTimeApplApprovedOnRP = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel49 = new Micromind.UISupport.MMLabel();
            this.dateTimeApplPostedOnRP = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel48 = new Micromind.UISupport.MMLabel();
            this.panelAGT = new Infragistics.Win.Misc.UltraGroupBox();
            this.textBoxAGTMBNo = new Micromind.UISupport.MMTextBox();
            this.mmLabel91 = new Micromind.UISupport.MMLabel();
            this.mmLabel90 = new Micromind.UISupport.MMLabel();
            this.comboBoxAGTType = new System.Windows.Forms.ComboBox();
            this.textBoxPersonIDNo = new Micromind.UISupport.MMTextBox();
            this.mmLabel66 = new Micromind.UISupport.MMLabel();
            this.textBoxWPIssuePlace = new Micromind.UISupport.MMTextBox();
            this.dateTimeWPExpiryDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.dateTimeWPIssueDate = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel62 = new Micromind.UISupport.MMLabel();
            this.mmLabel61 = new Micromind.UISupport.MMLabel();
            this.mmLabel60 = new Micromind.UISupport.MMLabel();
            this.textBoxWPNo = new Micromind.UISupport.MMTextBox();
            this.dateTimeAGTSubmittedOn = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel57 = new Micromind.UISupport.MMLabel();
            this.mmLabel58 = new Micromind.UISupport.MMLabel();
            this.dateTimeAGTTypedOn = new Micromind.UISupport.MMSDateTimePicker(this.components);
            this.mmLabel59 = new Micromind.UISupport.MMLabel();
            this.tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
            this.ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.mmLabel23 = new Micromind.UISupport.MMLabel();
            this.textBoxComment = new Micromind.UISupport.MMTextBox();
            this.mmLabel20 = new Micromind.UISupport.MMLabel();
            this.textBoxPostalCode = new Micromind.UISupport.MMTextBox();
            this.mmLabel18 = new Micromind.UISupport.MMLabel();
            this.textBoxEmail = new Micromind.UISupport.MMTextBox();
            this.mmLabel17 = new Micromind.UISupport.MMLabel();
            this.textBoxMobile = new Micromind.UISupport.MMTextBox();
            this.mmLabel16 = new Micromind.UISupport.MMLabel();
            this.textBoxFax = new Micromind.UISupport.MMTextBox();
            this.mmLabel15 = new Micromind.UISupport.MMLabel();
            this.textBoxPhone2 = new Micromind.UISupport.MMTextBox();
            this.mmLabel14 = new Micromind.UISupport.MMLabel();
            this.textBoxPhone1 = new Micromind.UISupport.MMTextBox();
            this.mmLabel12 = new Micromind.UISupport.MMLabel();
            this.textBoxCountry = new Micromind.UISupport.MMTextBox();
            this.mmLabel11 = new Micromind.UISupport.MMLabel();
            this.textBoxState = new Micromind.UISupport.MMTextBox();
            this.mmLabel13 = new Micromind.UISupport.MMLabel();
            this.textBoxCity = new Micromind.UISupport.MMTextBox();
            this.textBoxAddress3 = new Micromind.UISupport.MMTextBox();
            this.textBoxAddress2 = new Micromind.UISupport.MMTextBox();
            this.mmLabel10 = new Micromind.UISupport.MMLabel();
            this.textBoxAddress1 = new Micromind.UISupport.MMTextBox();
            this.mmLabel8 = new Micromind.UISupport.MMLabel();
            this.textBoxAddressID = new Micromind.UISupport.MMTextBox();
            this.ultraTabControl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.linePanelDown = new Micromind.UISupport.Line();
            this.buttonDelete = new Micromind.UISupport.XPButton();
            this.buttonClose = new Micromind.UISupport.XPButton();
            this.buttonNew = new Micromind.UISupport.XPButton();
            this.buttonSave = new Micromind.UISupport.XPButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEmployee = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.morePrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelCancelled = new System.Windows.Forms.Label();
            this.labelCategory = new System.Windows.Forms.Label();
            this.labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dependentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.formManager = new Micromind.DataControls.FormManager();
            this.ultraTabPageControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPayrollItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPayrollItem)).BeginInit();
            this.tabPageGeneral.SuspendLayout();
            this.panelGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDesignation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNoImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxReligion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxNationality)).BeginInit();
            this.ultraTabPageControl1.SuspendLayout();
            this.panelArrival.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sponsorComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
            this.tabPageDetails.SuspendLayout();
            this.panelRecruitment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxArrivalPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericExperienceAbroad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericExperienceLocal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxQualification)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPositionActual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAgentThrough)).BeginInit();
            this.ultraTabPageControl4.SuspendLayout();
            this.panelVisaProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelVisaIMG)).BeginInit();
            this.panelVisaIMG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelVisaMOL)).BeginInit();
            this.panelVisaMOL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPositionVisa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSponsor)).BeginInit();
            this.ultraTabPageControl2.SuspendLayout();
            this.panelMedicalEmirates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelEmirates)).BeginInit();
            this.panelEmirates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelMedicalDetail)).BeginInit();
            this.panelMedicalDetail.SuspendLayout();
            this.ultraTabPageControl3.SuspendLayout();
            this.panelWPRP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelMedicalReport)).BeginInit();
            this.panelMedicalReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelAGT)).BeginInit();
            this.panelAGT.SuspendLayout();
            this.tabPageUserDefined.SuspendLayout();
            this.ultraTabPageControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ultraTabPageControl6
            // 
            this.ultraTabPageControl6.Controls.Add(this.dataGridPayrollItem);
            this.ultraTabPageControl6.Controls.Add(this.comboBoxPayrollItem);
            this.ultraTabPageControl6.Controls.Add(this.textBoxTotalSalary);
            this.ultraTabPageControl6.Controls.Add(this.label7);
            this.ultraTabPageControl6.Location = new System.Drawing.Point(1, 23);
            this.ultraTabPageControl6.Name = "ultraTabPageControl6";
            this.ultraTabPageControl6.Size = new System.Drawing.Size(946, 422);
            // 
            // dataGridPayrollItem
            // 
            this.dataGridPayrollItem.AllowAddNew = false;
            this.dataGridPayrollItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.dataGridPayrollItem.DisplayLayout.Appearance = appearance1;
            this.dataGridPayrollItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.dataGridPayrollItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridPayrollItem.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridPayrollItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.dataGridPayrollItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dataGridPayrollItem.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.dataGridPayrollItem.DisplayLayout.MaxColScrollRegions = 1;
            this.dataGridPayrollItem.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridPayrollItem.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridPayrollItem.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.dataGridPayrollItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.dataGridPayrollItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.dataGridPayrollItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridPayrollItem.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.dataGridPayrollItem.DisplayLayout.Override.CellAppearance = appearance8;
            this.dataGridPayrollItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.dataGridPayrollItem.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.dataGridPayrollItem.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.dataGridPayrollItem.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.dataGridPayrollItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.dataGridPayrollItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.dataGridPayrollItem.DisplayLayout.Override.RowAppearance = appearance11;
            this.dataGridPayrollItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridPayrollItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.dataGridPayrollItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.dataGridPayrollItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.dataGridPayrollItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.dataGridPayrollItem.IncludeLotItems = false;
            this.dataGridPayrollItem.LoadLayoutFailed = false;
            this.dataGridPayrollItem.Location = new System.Drawing.Point(14, 3);
            this.dataGridPayrollItem.Name = "dataGridPayrollItem";
            this.dataGridPayrollItem.ShowClearMenu = true;
            this.dataGridPayrollItem.ShowDeleteMenu = true;
            this.dataGridPayrollItem.ShowInsertMenu = true;
            this.dataGridPayrollItem.ShowMoveRowsMenu = true;
            this.dataGridPayrollItem.Size = new System.Drawing.Size(919, 392);
            this.dataGridPayrollItem.TabIndex = 0;
            this.dataGridPayrollItem.Text = "dataEntryGrid1";
            // 
            // comboBoxPayrollItem
            // 
            this.comboBoxPayrollItem.Assigned = false;
            this.comboBoxPayrollItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxPayrollItem.CustomReportFieldName = "";
            this.comboBoxPayrollItem.CustomReportKey = "";
            this.comboBoxPayrollItem.CustomReportValueType = ((byte)(1));
            this.comboBoxPayrollItem.DescriptionTextBox = null;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxPayrollItem.DisplayLayout.Appearance = appearance13;
            this.comboBoxPayrollItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxPayrollItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance14.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPayrollItem.DisplayLayout.GroupByBox.Appearance = appearance14;
            appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPayrollItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
            this.comboBoxPayrollItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance16.BackColor2 = System.Drawing.SystemColors.Control;
            appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPayrollItem.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
            this.comboBoxPayrollItem.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxPayrollItem.DisplayLayout.MaxRowScrollRegions = 1;
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxPayrollItem.DisplayLayout.Override.ActiveCellAppearance = appearance17;
            appearance18.BackColor = System.Drawing.SystemColors.Highlight;
            appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxPayrollItem.DisplayLayout.Override.ActiveRowAppearance = appearance18;
            this.comboBoxPayrollItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxPayrollItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxPayrollItem.DisplayLayout.Override.CardAreaAppearance = appearance19;
            appearance20.BorderColor = System.Drawing.Color.Silver;
            appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxPayrollItem.DisplayLayout.Override.CellAppearance = appearance20;
            this.comboBoxPayrollItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxPayrollItem.DisplayLayout.Override.CellPadding = 0;
            appearance21.BackColor = System.Drawing.SystemColors.Control;
            appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance21.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPayrollItem.DisplayLayout.Override.GroupByRowAppearance = appearance21;
            appearance22.TextHAlignAsString = "Left";
            this.comboBoxPayrollItem.DisplayLayout.Override.HeaderAppearance = appearance22;
            this.comboBoxPayrollItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxPayrollItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance23.BackColor = System.Drawing.SystemColors.Window;
            appearance23.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxPayrollItem.DisplayLayout.Override.RowAppearance = appearance23;
            this.comboBoxPayrollItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxPayrollItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
            this.comboBoxPayrollItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxPayrollItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxPayrollItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxPayrollItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxPayrollItem.Editable = true;
            this.comboBoxPayrollItem.FilterString = "";
            this.comboBoxPayrollItem.HasAllAccount = false;
            this.comboBoxPayrollItem.HasCustom = false;
            this.comboBoxPayrollItem.IsDataLoaded = false;
            this.comboBoxPayrollItem.IsDeduction = false;
            this.comboBoxPayrollItem.Location = new System.Drawing.Point(672, 3);
            this.comboBoxPayrollItem.MaxDropDownItems = 12;
            this.comboBoxPayrollItem.Name = "comboBoxPayrollItem";
            this.comboBoxPayrollItem.ShowInactiveItems = false;
            this.comboBoxPayrollItem.ShowQuickAdd = true;
            this.comboBoxPayrollItem.Size = new System.Drawing.Size(97, 25);
            this.comboBoxPayrollItem.TabIndex = 19;
            this.comboBoxPayrollItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxPayrollItem.Visible = false;
            // 
            // textBoxTotalSalary
            // 
            this.textBoxTotalSalary.AllowDecimal = true;
            this.textBoxTotalSalary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTotalSalary.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxTotalSalary.CustomReportFieldName = "";
            this.textBoxTotalSalary.CustomReportKey = "";
            this.textBoxTotalSalary.CustomReportValueType = ((byte)(1));
            this.textBoxTotalSalary.ForeColor = System.Drawing.Color.Black;
            this.textBoxTotalSalary.IsComboTextBox = false;
            this.textBoxTotalSalary.IsModified = false;
            this.textBoxTotalSalary.Location = new System.Drawing.Point(796, 395);
            this.textBoxTotalSalary.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.textBoxTotalSalary.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.textBoxTotalSalary.Name = "textBoxTotalSalary";
            this.textBoxTotalSalary.NullText = "0";
            this.textBoxTotalSalary.ReadOnly = true;
            this.textBoxTotalSalary.Size = new System.Drawing.Size(137, 24);
            this.textBoxTotalSalary.TabIndex = 9;
            this.textBoxTotalSalary.TabStop = false;
            this.textBoxTotalSalary.Text = "0.00";
            this.textBoxTotalSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxTotalSalary.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(703, 399);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 17);
            this.label7.TabIndex = 27;
            this.label7.Text = "Total Salary:";
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.panelGeneral);
            this.tabPageGeneral.Location = new System.Drawing.Point(2, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Size = new System.Drawing.Size(974, 592);
            // 
            // panelGeneral
            // 
            this.panelGeneral.Controls.Add(this.mmLabel93);
            this.panelGeneral.Controls.Add(this.ultraFormattedLinkLabel2);
            this.panelGeneral.Controls.Add(this.comboBoxGroup);
            this.panelGeneral.Controls.Add(this.ultraFormattedLinkLabel3);
            this.panelGeneral.Controls.Add(this.comboBoxvisaType);
            this.panelGeneral.Controls.Add(this.textBoxDesignation);
            this.panelGeneral.Controls.Add(this.comboBoxDesignation);
            this.panelGeneral.Controls.Add(this.mmLabel69);
            this.panelGeneral.Controls.Add(this.pictureBoxNoImage);
            this.panelGeneral.Controls.Add(this.linkLoadImage);
            this.panelGeneral.Controls.Add(this.textBoxNote);
            this.panelGeneral.Controls.Add(this.mmLabel54);
            this.panelGeneral.Controls.Add(this.mmLabel53);
            this.panelGeneral.Controls.Add(this.textBoxPassportNo);
            this.panelGeneral.Controls.Add(this.linkRemovePicture);
            this.panelGeneral.Controls.Add(this.linkAddPicture);
            this.panelGeneral.Controls.Add(this.pictureBoxPhoto);
            this.panelGeneral.Controls.Add(this.textBoxPPAddress);
            this.panelGeneral.Controls.Add(this.mmLabel21);
            this.panelGeneral.Controls.Add(this.textBoxBloodGroup);
            this.panelGeneral.Controls.Add(this.comboBoxReligion);
            this.panelGeneral.Controls.Add(this.ultraFormattedLinkLabel1);
            this.panelGeneral.Controls.Add(this.comboBoxMaritalStatus);
            this.panelGeneral.Controls.Add(this.mmLabel47);
            this.panelGeneral.Controls.Add(this.mmLabel19);
            this.panelGeneral.Controls.Add(this.textBoxSpouseName);
            this.panelGeneral.Controls.Add(this.mmLabel9);
            this.panelGeneral.Controls.Add(this.textBoxMotherName);
            this.panelGeneral.Controls.Add(this.mmLabel6);
            this.panelGeneral.Controls.Add(this.textBoxFatherName);
            this.panelGeneral.Controls.Add(this.mmLabel4);
            this.panelGeneral.Controls.Add(this.dateTimePPExpiryDate);
            this.panelGeneral.Controls.Add(this.mmLabel3);
            this.panelGeneral.Controls.Add(this.dateTimePPIssueDate);
            this.panelGeneral.Controls.Add(this.textBoxPPIssuePlace);
            this.panelGeneral.Controls.Add(this.mmLabel1);
            this.panelGeneral.Controls.Add(this.mmLabel2);
            this.panelGeneral.Controls.Add(this.textBoxBirthPlace);
            this.panelGeneral.Controls.Add(this.mmLabel33);
            this.panelGeneral.Controls.Add(this.mmLabel51);
            this.panelGeneral.Controls.Add(this.dateTimeBirthDate);
            this.panelGeneral.Controls.Add(this.textBoxAge);
            this.panelGeneral.Controls.Add(this.mmLabel31);
            this.panelGeneral.Controls.Add(this.comboBoxGender);
            this.panelGeneral.Controls.Add(this.comboBoxNationality);
            this.panelGeneral.Controls.Add(this.mmLabel5);
            this.panelGeneral.Controls.Add(this.labelCandidateNumber);
            this.panelGeneral.Controls.Add(this.lblDescriptions);
            this.panelGeneral.Controls.Add(this.textBoxCode);
            this.panelGeneral.Controls.Add(this.textBoxSurName);
            this.panelGeneral.Controls.Add(this.textBoxGivenName);
            this.panelGeneral.Controls.Add(this.labelGivenName);
            this.panelGeneral.Location = new System.Drawing.Point(-2, 0);
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Size = new System.Drawing.Size(976, 610);
            this.panelGeneral.TabIndex = 0;
            // 
            // mmLabel93
            // 
            this.mmLabel93.AutoSize = true;
            this.mmLabel93.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel93.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel93.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel93.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel93.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel93.IsFieldHeader = false;
            this.mmLabel93.IsRequired = false;
            this.mmLabel93.Location = new System.Drawing.Point(16, 145);
            this.mmLabel93.Name = "mmLabel93";
            this.mmLabel93.PenWidth = 1F;
            this.mmLabel93.ShowBorder = false;
            this.mmLabel93.Size = new System.Drawing.Size(62, 17);
            this.mmLabel93.TabIndex = 157;
            this.mmLabel93.Text = "Status :";
            // 
            // ultraFormattedLinkLabel2
            // 
            appearance25.FontData.BoldAsString = "True";
            appearance25.FontData.Name = "Tahoma";
            this.ultraFormattedLinkLabel2.Appearance = appearance25;
            this.ultraFormattedLinkLabel2.AutoSize = true;
            this.ultraFormattedLinkLabel2.Location = new System.Drawing.Point(16, 120);
            this.ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
            this.ultraFormattedLinkLabel2.Size = new System.Drawing.Size(106, 20);
            this.ultraFormattedLinkLabel2.TabIndex = 155;
            this.ultraFormattedLinkLabel2.TabStop = true;
            this.ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel2.Value = "Designation:";
            appearance26.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance26;
            this.ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel2_LinkClicked);
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.Assigned = false;
            this.comboBoxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxGroup.CustomReportFieldName = "";
            this.comboBoxGroup.CustomReportKey = "";
            this.comboBoxGroup.CustomReportValueType = ((byte)(1));
            this.comboBoxGroup.DescriptionTextBox = null;
            appearance27.BackColor = System.Drawing.SystemColors.Window;
            appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxGroup.DisplayLayout.Appearance = appearance27;
            this.comboBoxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance28.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGroup.DisplayLayout.GroupByBox.Appearance = appearance28;
            appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
            this.comboBoxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance30.BackColor2 = System.Drawing.SystemColors.Control;
            appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
            this.comboBoxGroup.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxGroup.DisplayLayout.MaxRowScrollRegions = 1;
            appearance31.BackColor = System.Drawing.SystemColors.Window;
            appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance31;
            appearance32.BackColor = System.Drawing.SystemColors.Highlight;
            appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance32;
            this.comboBoxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance33.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxGroup.DisplayLayout.Override.CardAreaAppearance = appearance33;
            appearance34.BorderColor = System.Drawing.Color.Silver;
            appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxGroup.DisplayLayout.Override.CellAppearance = appearance34;
            this.comboBoxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxGroup.DisplayLayout.Override.CellPadding = 0;
            appearance35.BackColor = System.Drawing.SystemColors.Control;
            appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance35.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance35;
            appearance36.TextHAlignAsString = "Left";
            this.comboBoxGroup.DisplayLayout.Override.HeaderAppearance = appearance36;
            this.comboBoxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance37.BackColor = System.Drawing.SystemColors.Window;
            appearance37.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxGroup.DisplayLayout.Override.RowAppearance = appearance37;
            this.comboBoxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
            this.comboBoxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxGroup.Editable = true;
            this.comboBoxGroup.FilterString = "";
            this.comboBoxGroup.HasAllAccount = false;
            this.comboBoxGroup.HasCustom = false;
            this.comboBoxGroup.IsDataLoaded = false;
            this.comboBoxGroup.Location = new System.Drawing.Point(416, 143);
            this.comboBoxGroup.MaxDropDownItems = 12;
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.ShowInactiveItems = false;
            this.comboBoxGroup.ShowQuickAdd = true;
            this.comboBoxGroup.Size = new System.Drawing.Size(164, 23);
            this.comboBoxGroup.TabIndex = 6;
            this.comboBoxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ultraFormattedLinkLabel3
            // 
            this.ultraFormattedLinkLabel3.AutoSize = true;
            this.ultraFormattedLinkLabel3.Location = new System.Drawing.Point(364, 145);
            this.ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
            this.ultraFormattedLinkLabel3.Size = new System.Drawing.Size(54, 19);
            this.ultraFormattedLinkLabel3.TabIndex = 154;
            this.ultraFormattedLinkLabel3.TabStop = true;
            this.ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel3.Value = "Group:";
            appearance39.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance39;
            this.ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.ultraFormattedLinkLabel3_LinkClicked);
            // 
            // comboBoxvisaType
            // 
            this.comboBoxvisaType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxvisaType.FormattingEnabled = true;
            this.comboBoxvisaType.Location = new System.Drawing.Point(203, 142);
            this.comboBoxvisaType.Name = "comboBoxvisaType";
            this.comboBoxvisaType.SelectedID = 0;
            this.comboBoxvisaType.Size = new System.Drawing.Size(149, 24);
            this.comboBoxvisaType.TabIndex = 5;
            // 
            // textBoxDesignation
            // 
            this.textBoxDesignation.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxDesignation.Location = new System.Drawing.Point(354, 117);
            this.textBoxDesignation.MaxLength = 64;
            this.textBoxDesignation.Name = "textBoxDesignation";
            this.textBoxDesignation.ReadOnly = true;
            this.textBoxDesignation.Size = new System.Drawing.Size(226, 22);
            this.textBoxDesignation.TabIndex = 4;
            this.textBoxDesignation.TabStop = false;
            // 
            // comboBoxDesignation
            // 
            this.comboBoxDesignation.Assigned = false;
            this.comboBoxDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxDesignation.CustomReportFieldName = "";
            this.comboBoxDesignation.CustomReportKey = "";
            this.comboBoxDesignation.CustomReportValueType = ((byte)(1));
            this.comboBoxDesignation.DescriptionTextBox = this.textBoxDesignation;
            appearance40.BackColor = System.Drawing.SystemColors.Window;
            appearance40.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxDesignation.DisplayLayout.Appearance = appearance40;
            this.comboBoxDesignation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxDesignation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance41.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance41.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxDesignation.DisplayLayout.GroupByBox.Appearance = appearance41;
            appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxDesignation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance42;
            this.comboBoxDesignation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance43.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance43.BackColor2 = System.Drawing.SystemColors.Control;
            appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxDesignation.DisplayLayout.GroupByBox.PromptAppearance = appearance43;
            this.comboBoxDesignation.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxDesignation.DisplayLayout.MaxRowScrollRegions = 1;
            appearance44.BackColor = System.Drawing.SystemColors.Window;
            appearance44.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxDesignation.DisplayLayout.Override.ActiveCellAppearance = appearance44;
            appearance45.BackColor = System.Drawing.SystemColors.Highlight;
            appearance45.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxDesignation.DisplayLayout.Override.ActiveRowAppearance = appearance45;
            this.comboBoxDesignation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxDesignation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance46.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxDesignation.DisplayLayout.Override.CardAreaAppearance = appearance46;
            appearance47.BorderColor = System.Drawing.Color.Silver;
            appearance47.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxDesignation.DisplayLayout.Override.CellAppearance = appearance47;
            this.comboBoxDesignation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxDesignation.DisplayLayout.Override.CellPadding = 0;
            appearance48.BackColor = System.Drawing.SystemColors.Control;
            appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance48.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance48.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxDesignation.DisplayLayout.Override.GroupByRowAppearance = appearance48;
            appearance49.TextHAlignAsString = "Left";
            this.comboBoxDesignation.DisplayLayout.Override.HeaderAppearance = appearance49;
            this.comboBoxDesignation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxDesignation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance50.BackColor = System.Drawing.SystemColors.Window;
            appearance50.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxDesignation.DisplayLayout.Override.RowAppearance = appearance50;
            this.comboBoxDesignation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance51.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxDesignation.DisplayLayout.Override.TemplateAddRowAppearance = appearance51;
            this.comboBoxDesignation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxDesignation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxDesignation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxDesignation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxDesignation.Editable = true;
            this.comboBoxDesignation.FilterString = "";
            this.comboBoxDesignation.HasAllAccount = false;
            this.comboBoxDesignation.HasCustom = false;
            this.comboBoxDesignation.IsDataLoaded = false;
            this.comboBoxDesignation.Location = new System.Drawing.Point(203, 117);
            this.comboBoxDesignation.MaxDropDownItems = 12;
            this.comboBoxDesignation.Name = "comboBoxDesignation";
            this.comboBoxDesignation.ShowInactiveItems = false;
            this.comboBoxDesignation.ShowQuickAdd = true;
            this.comboBoxDesignation.Size = new System.Drawing.Size(149, 23);
            this.comboBoxDesignation.TabIndex = 3;
            this.comboBoxDesignation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // mmLabel69
            // 
            this.mmLabel69.AutoSize = true;
            this.mmLabel69.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel69.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel69.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel69.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel69.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel69.IsFieldHeader = false;
            this.mmLabel69.IsRequired = false;
            this.mmLabel69.Location = new System.Drawing.Point(16, 95);
            this.mmLabel69.Name = "mmLabel69";
            this.mmLabel69.PenWidth = 1F;
            this.mmLabel69.ShowBorder = false;
            this.mmLabel69.Size = new System.Drawing.Size(105, 17);
            this.mmLabel69.TabIndex = 147;
            this.mmLabel69.Text = "Middle Name :";
            // 
            // pictureBoxNoImage
            // 
            this.pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxNoImage.Image = global::Micromind.ClientUI.Properties.Resources.noimage;
            this.pictureBoxNoImage.InitialImage = global::Micromind.ClientUI.Properties.Resources.noimage;
            this.pictureBoxNoImage.Location = new System.Drawing.Point(901, 398);
            this.pictureBoxNoImage.Name = "pictureBoxNoImage";
            this.pictureBoxNoImage.Size = new System.Drawing.Size(59, 55);
            this.pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxNoImage.TabIndex = 146;
            this.pictureBoxNoImage.TabStop = false;
            this.pictureBoxNoImage.Visible = false;
            // 
            // linkLoadImage
            // 
            this.linkLoadImage.AutoSize = true;
            this.linkLoadImage.Location = new System.Drawing.Point(842, 114);
            this.linkLoadImage.Name = "linkLoadImage";
            this.linkLoadImage.Size = new System.Drawing.Size(94, 19);
            this.linkLoadImage.TabIndex = 126;
            this.linkLoadImage.TabStop = true;
            this.linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.linkLoadImage.Value = "Load Picture";
            appearance52.ForeColor = System.Drawing.Color.Blue;
            this.linkLoadImage.VisitedLinkAppearance = appearance52;
            this.linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.linkLoadImage_LinkClicked);
            // 
            // textBoxNote
            // 
            this.textBoxNote.BackColor = System.Drawing.Color.White;
            this.textBoxNote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxNote.CustomReportFieldName = "";
            this.textBoxNote.CustomReportKey = "";
            this.textBoxNote.CustomReportValueType = ((byte)(1));
            this.textBoxNote.IsComboTextBox = false;
            this.textBoxNote.IsModified = false;
            this.textBoxNote.Location = new System.Drawing.Point(203, 497);
            this.textBoxNote.MaxLength = 255;
            this.textBoxNote.Multiline = true;
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNote.Size = new System.Drawing.Size(757, 103);
            this.textBoxNote.TabIndex = 21;
            // 
            // mmLabel54
            // 
            this.mmLabel54.AutoSize = true;
            this.mmLabel54.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel54.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel54.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel54.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel54.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel54.IsFieldHeader = false;
            this.mmLabel54.IsRequired = false;
            this.mmLabel54.Location = new System.Drawing.Point(18, 525);
            this.mmLabel54.Name = "mmLabel54";
            this.mmLabel54.PenWidth = 1F;
            this.mmLabel54.ShowBorder = false;
            this.mmLabel54.Size = new System.Drawing.Size(46, 17);
            this.mmLabel54.TabIndex = 145;
            this.mmLabel54.Text = "Note :";
            // 
            // mmLabel53
            // 
            this.mmLabel53.AutoSize = true;
            this.mmLabel53.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel53.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel53.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel53.IsFieldHeader = false;
            this.mmLabel53.IsRequired = false;
            this.mmLabel53.Location = new System.Drawing.Point(16, 39);
            this.mmLabel53.Name = "mmLabel53";
            this.mmLabel53.PenWidth = 1F;
            this.mmLabel53.ShowBorder = false;
            this.mmLabel53.Size = new System.Drawing.Size(102, 17);
            this.mmLabel53.TabIndex = 144;
            this.mmLabel53.Text = "Passport No :";
            // 
            // textBoxPassportNo
            // 
            this.textBoxPassportNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxPassportNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.textBoxPassportNo.BackColor = System.Drawing.Color.White;
            this.textBoxPassportNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxPassportNo.CustomReportFieldName = "";
            this.textBoxPassportNo.CustomReportKey = "";
            this.textBoxPassportNo.CustomReportValueType = ((byte)(1));
            this.textBoxPassportNo.IsComboTextBox = false;
            this.textBoxPassportNo.IsModified = false;
            this.textBoxPassportNo.Location = new System.Drawing.Point(203, 39);
            this.textBoxPassportNo.MaxLength = 20;
            this.textBoxPassportNo.Name = "textBoxPassportNo";
            this.textBoxPassportNo.Size = new System.Drawing.Size(175, 22);
            this.textBoxPassportNo.TabIndex = 0;
            // 
            // linkRemovePicture
            // 
            this.linkRemovePicture.AutoSize = true;
            this.linkRemovePicture.Location = new System.Drawing.Point(880, 203);
            this.linkRemovePicture.Name = "linkRemovePicture";
            this.linkRemovePicture.Size = new System.Drawing.Size(64, 19);
            this.linkRemovePicture.TabIndex = 129;
            this.linkRemovePicture.TabStop = true;
            this.linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.linkRemovePicture.Value = "Remove";
            appearance53.ForeColor = System.Drawing.Color.Blue;
            this.linkRemovePicture.VisitedLinkAppearance = appearance53;
            this.linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.linkRemovePicture_LinkClicked);
            // 
            // linkAddPicture
            // 
            this.linkAddPicture.AutoSize = true;
            this.linkAddPicture.Location = new System.Drawing.Point(833, 203);
            this.linkAddPicture.Name = "linkAddPicture";
            this.linkAddPicture.Size = new System.Drawing.Size(35, 19);
            this.linkAddPicture.TabIndex = 127;
            this.linkAddPicture.TabStop = true;
            this.linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.linkAddPicture.Value = "Add";
            appearance54.ForeColor = System.Drawing.Color.Blue;
            this.linkAddPicture.VisitedLinkAppearance = appearance54;
            this.linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.linkAddPicture_LinkClicked);
            // 
            // pictureBoxPhoto
            // 
            this.pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPhoto.InitialImage = global::Micromind.ClientUI.Properties.Resources.noimage;
            this.pictureBoxPhoto.Location = new System.Drawing.Point(806, 48);
            this.pictureBoxPhoto.Name = "pictureBoxPhoto";
            this.pictureBoxPhoto.Size = new System.Drawing.Size(154, 148);
            this.pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPhoto.TabIndex = 143;
            this.pictureBoxPhoto.TabStop = false;
            // 
            // textBoxPPAddress
            // 
            this.textBoxPPAddress.BackColor = System.Drawing.Color.White;
            this.textBoxPPAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxPPAddress.CustomReportFieldName = "";
            this.textBoxPPAddress.CustomReportKey = "";
            this.textBoxPPAddress.CustomReportValueType = ((byte)(1));
            this.textBoxPPAddress.IsComboTextBox = false;
            this.textBoxPPAddress.IsModified = false;
            this.textBoxPPAddress.Location = new System.Drawing.Point(203, 427);
            this.textBoxPPAddress.MaxLength = 255;
            this.textBoxPPAddress.Multiline = true;
            this.textBoxPPAddress.Name = "textBoxPPAddress";
            this.textBoxPPAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxPPAddress.Size = new System.Drawing.Size(377, 63);
            this.textBoxPPAddress.TabIndex = 20;
            // 
            // mmLabel21
            // 
            this.mmLabel21.AutoSize = true;
            this.mmLabel21.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel21.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel21.IsFieldHeader = false;
            this.mmLabel21.IsRequired = false;
            this.mmLabel21.Location = new System.Drawing.Point(631, 337);
            this.mmLabel21.Name = "mmLabel21";
            this.mmLabel21.PenWidth = 1F;
            this.mmLabel21.ShowBorder = false;
            this.mmLabel21.Size = new System.Drawing.Size(89, 17);
            this.mmLabel21.TabIndex = 142;
            this.mmLabel21.Text = "Blood Group:";
            this.mmLabel21.Visible = false;
            // 
            // textBoxBloodGroup
            // 
            this.textBoxBloodGroup.AutoCompleteCustomSource.AddRange(new string[] {
            "A+",
            "A-",
            "B+",
            "B-",
            "AB+",
            "AB-",
            "O+",
            "O-"});
            this.textBoxBloodGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxBloodGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxBloodGroup.BackColor = System.Drawing.Color.White;
            this.textBoxBloodGroup.CustomReportFieldName = "";
            this.textBoxBloodGroup.CustomReportKey = "";
            this.textBoxBloodGroup.CustomReportValueType = ((byte)(1));
            this.textBoxBloodGroup.IsComboTextBox = false;
            this.textBoxBloodGroup.IsModified = false;
            this.textBoxBloodGroup.Location = new System.Drawing.Point(725, 331);
            this.textBoxBloodGroup.MaxLength = 3;
            this.textBoxBloodGroup.Name = "textBoxBloodGroup";
            this.textBoxBloodGroup.Size = new System.Drawing.Size(86, 22);
            this.textBoxBloodGroup.TabIndex = 15;
            this.textBoxBloodGroup.Visible = false;
            // 
            // comboBoxReligion
            // 
            this.comboBoxReligion.Assigned = false;
            this.comboBoxReligion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxReligion.CustomReportFieldName = "";
            this.comboBoxReligion.CustomReportKey = "";
            this.comboBoxReligion.CustomReportValueType = ((byte)(1));
            this.comboBoxReligion.DescriptionTextBox = null;
            appearance55.BackColor = System.Drawing.SystemColors.Window;
            appearance55.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxReligion.DisplayLayout.Appearance = appearance55;
            this.comboBoxReligion.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxReligion.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance56.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance56.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxReligion.DisplayLayout.GroupByBox.Appearance = appearance56;
            appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxReligion.DisplayLayout.GroupByBox.BandLabelAppearance = appearance57;
            this.comboBoxReligion.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance58.BackColor2 = System.Drawing.SystemColors.Control;
            appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxReligion.DisplayLayout.GroupByBox.PromptAppearance = appearance58;
            this.comboBoxReligion.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxReligion.DisplayLayout.MaxRowScrollRegions = 1;
            appearance59.BackColor = System.Drawing.SystemColors.Window;
            appearance59.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxReligion.DisplayLayout.Override.ActiveCellAppearance = appearance59;
            appearance60.BackColor = System.Drawing.SystemColors.Highlight;
            appearance60.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxReligion.DisplayLayout.Override.ActiveRowAppearance = appearance60;
            this.comboBoxReligion.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxReligion.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance61.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxReligion.DisplayLayout.Override.CardAreaAppearance = appearance61;
            appearance62.BorderColor = System.Drawing.Color.Silver;
            appearance62.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxReligion.DisplayLayout.Override.CellAppearance = appearance62;
            this.comboBoxReligion.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxReligion.DisplayLayout.Override.CellPadding = 0;
            appearance63.BackColor = System.Drawing.SystemColors.Control;
            appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance63.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance63.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxReligion.DisplayLayout.Override.GroupByRowAppearance = appearance63;
            appearance64.TextHAlignAsString = "Left";
            this.comboBoxReligion.DisplayLayout.Override.HeaderAppearance = appearance64;
            this.comboBoxReligion.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxReligion.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance65.BackColor = System.Drawing.SystemColors.Window;
            appearance65.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxReligion.DisplayLayout.Override.RowAppearance = appearance65;
            this.comboBoxReligion.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance66.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxReligion.DisplayLayout.Override.TemplateAddRowAppearance = appearance66;
            this.comboBoxReligion.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxReligion.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxReligion.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxReligion.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxReligion.Editable = true;
            this.comboBoxReligion.FilterString = "";
            this.comboBoxReligion.HasAllAccount = false;
            this.comboBoxReligion.HasCustom = false;
            this.comboBoxReligion.IsDataLoaded = false;
            this.comboBoxReligion.Location = new System.Drawing.Point(725, 359);
            this.comboBoxReligion.MaxDropDownItems = 12;
            this.comboBoxReligion.Name = "comboBoxReligion";
            this.comboBoxReligion.ShowInactiveItems = false;
            this.comboBoxReligion.ShowQuickAdd = true;
            this.comboBoxReligion.Size = new System.Drawing.Size(235, 23);
            this.comboBoxReligion.TabIndex = 17;
            this.comboBoxReligion.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxReligion.Visible = false;
            // 
            // ultraFormattedLinkLabel1
            // 
            this.ultraFormattedLinkLabel1.AutoSize = true;
            this.ultraFormattedLinkLabel1.Location = new System.Drawing.Point(631, 361);
            this.ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
            this.ultraFormattedLinkLabel1.Size = new System.Drawing.Size(67, 19);
            this.ultraFormattedLinkLabel1.TabIndex = 141;
            this.ultraFormattedLinkLabel1.TabStop = true;
            this.ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
            this.ultraFormattedLinkLabel1.Value = "Religion:";
            this.ultraFormattedLinkLabel1.Visible = false;
            appearance67.ForeColor = System.Drawing.Color.Blue;
            this.ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance67;
            // 
            // comboBoxMaritalStatus
            // 
            this.comboBoxMaritalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaritalStatus.FormattingEnabled = true;
            this.comboBoxMaritalStatus.Location = new System.Drawing.Point(725, 303);
            this.comboBoxMaritalStatus.Name = "comboBoxMaritalStatus";
            this.comboBoxMaritalStatus.SelectedID = 0;
            this.comboBoxMaritalStatus.Size = new System.Drawing.Size(140, 24);
            this.comboBoxMaritalStatus.TabIndex = 13;
            this.comboBoxMaritalStatus.Visible = false;
            // 
            // mmLabel47
            // 
            this.mmLabel47.AutoSize = true;
            this.mmLabel47.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel47.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel47.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel47.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel47.IsFieldHeader = false;
            this.mmLabel47.IsRequired = false;
            this.mmLabel47.Location = new System.Drawing.Point(631, 309);
            this.mmLabel47.Name = "mmLabel47";
            this.mmLabel47.PenWidth = 1F;
            this.mmLabel47.ShowBorder = false;
            this.mmLabel47.Size = new System.Drawing.Size(94, 17);
            this.mmLabel47.TabIndex = 140;
            this.mmLabel47.Text = "Marital Status:";
            this.mmLabel47.Visible = false;
            // 
            // mmLabel19
            // 
            this.mmLabel19.AutoSize = true;
            this.mmLabel19.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel19.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel19.IsFieldHeader = false;
            this.mmLabel19.IsRequired = false;
            this.mmLabel19.Location = new System.Drawing.Point(16, 430);
            this.mmLabel19.Name = "mmLabel19";
            this.mmLabel19.PenWidth = 1F;
            this.mmLabel19.ShowBorder = false;
            this.mmLabel19.Size = new System.Drawing.Size(85, 17);
            this.mmLabel19.TabIndex = 139;
            this.mmLabel19.Text = "PP Address :";
            // 
            // textBoxSpouseName
            // 
            this.textBoxSpouseName.BackColor = System.Drawing.Color.White;
            this.textBoxSpouseName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxSpouseName.CustomReportFieldName = "";
            this.textBoxSpouseName.CustomReportKey = "";
            this.textBoxSpouseName.CustomReportValueType = ((byte)(1));
            this.textBoxSpouseName.IsComboTextBox = false;
            this.textBoxSpouseName.IsModified = false;
            this.textBoxSpouseName.IsRequired = true;
            this.textBoxSpouseName.Location = new System.Drawing.Point(203, 402);
            this.textBoxSpouseName.MaxLength = 64;
            this.textBoxSpouseName.Name = "textBoxSpouseName";
            this.textBoxSpouseName.Size = new System.Drawing.Size(377, 22);
            this.textBoxSpouseName.TabIndex = 19;
            // 
            // mmLabel9
            // 
            this.mmLabel9.AutoSize = true;
            this.mmLabel9.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel9.IsFieldHeader = false;
            this.mmLabel9.IsRequired = false;
            this.mmLabel9.Location = new System.Drawing.Point(16, 405);
            this.mmLabel9.Name = "mmLabel9";
            this.mmLabel9.PenWidth = 1F;
            this.mmLabel9.ShowBorder = false;
            this.mmLabel9.Size = new System.Drawing.Size(101, 17);
            this.mmLabel9.TabIndex = 138;
            this.mmLabel9.Text = "Spouse Name :";
            // 
            // textBoxMotherName
            // 
            this.textBoxMotherName.BackColor = System.Drawing.Color.White;
            this.textBoxMotherName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxMotherName.CustomReportFieldName = "";
            this.textBoxMotherName.CustomReportKey = "";
            this.textBoxMotherName.CustomReportValueType = ((byte)(1));
            this.textBoxMotherName.IsComboTextBox = false;
            this.textBoxMotherName.IsModified = false;
            this.textBoxMotherName.IsRequired = true;
            this.textBoxMotherName.Location = new System.Drawing.Point(203, 376);
            this.textBoxMotherName.MaxLength = 64;
            this.textBoxMotherName.Name = "textBoxMotherName";
            this.textBoxMotherName.Size = new System.Drawing.Size(377, 22);
            this.textBoxMotherName.TabIndex = 18;
            // 
            // mmLabel6
            // 
            this.mmLabel6.AutoSize = true;
            this.mmLabel6.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel6.IsFieldHeader = false;
            this.mmLabel6.IsRequired = false;
            this.mmLabel6.Location = new System.Drawing.Point(16, 380);
            this.mmLabel6.Name = "mmLabel6";
            this.mmLabel6.PenWidth = 1F;
            this.mmLabel6.ShowBorder = false;
            this.mmLabel6.Size = new System.Drawing.Size(99, 17);
            this.mmLabel6.TabIndex = 137;
            this.mmLabel6.Text = "Mother Name :";
            // 
            // textBoxFatherName
            // 
            this.textBoxFatherName.BackColor = System.Drawing.Color.White;
            this.textBoxFatherName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxFatherName.CustomReportFieldName = "";
            this.textBoxFatherName.CustomReportKey = "";
            this.textBoxFatherName.CustomReportValueType = ((byte)(1));
            this.textBoxFatherName.IsComboTextBox = false;
            this.textBoxFatherName.IsModified = false;
            this.textBoxFatherName.IsRequired = true;
            this.textBoxFatherName.Location = new System.Drawing.Point(203, 351);
            this.textBoxFatherName.MaxLength = 64;
            this.textBoxFatherName.Name = "textBoxFatherName";
            this.textBoxFatherName.Size = new System.Drawing.Size(377, 22);
            this.textBoxFatherName.TabIndex = 16;
            // 
            // mmLabel4
            // 
            this.mmLabel4.AutoSize = true;
            this.mmLabel4.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel4.IsFieldHeader = false;
            this.mmLabel4.IsRequired = false;
            this.mmLabel4.Location = new System.Drawing.Point(16, 354);
            this.mmLabel4.Name = "mmLabel4";
            this.mmLabel4.PenWidth = 1F;
            this.mmLabel4.ShowBorder = false;
            this.mmLabel4.Size = new System.Drawing.Size(95, 17);
            this.mmLabel4.TabIndex = 136;
            this.mmLabel4.Text = "Father Name :";
            // 
            // dateTimePPExpiryDate
            // 
            this.dateTimePPExpiryDate.Checked = false;
            this.dateTimePPExpiryDate.CustomFormat = " dd-MMM-yyyy";
            this.dateTimePPExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePPExpiryDate.Location = new System.Drawing.Point(203, 325);
            this.dateTimePPExpiryDate.Name = "dateTimePPExpiryDate";
            this.dateTimePPExpiryDate.ShowCheckBox = true;
            this.dateTimePPExpiryDate.Size = new System.Drawing.Size(176, 22);
            this.dateTimePPExpiryDate.TabIndex = 14;
            this.dateTimePPExpiryDate.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel3
            // 
            this.mmLabel3.AutoSize = true;
            this.mmLabel3.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel3.IsFieldHeader = false;
            this.mmLabel3.IsRequired = false;
            this.mmLabel3.Location = new System.Drawing.Point(16, 330);
            this.mmLabel3.Name = "mmLabel3";
            this.mmLabel3.PenWidth = 1F;
            this.mmLabel3.ShowBorder = false;
            this.mmLabel3.Size = new System.Drawing.Size(109, 17);
            this.mmLabel3.TabIndex = 135;
            this.mmLabel3.Text = "PP Expiry Date :";
            // 
            // dateTimePPIssueDate
            // 
            this.dateTimePPIssueDate.Checked = false;
            this.dateTimePPIssueDate.CustomFormat = " dd-MMM-yyyy";
            this.dateTimePPIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePPIssueDate.Location = new System.Drawing.Point(203, 299);
            this.dateTimePPIssueDate.Name = "dateTimePPIssueDate";
            this.dateTimePPIssueDate.ShowCheckBox = true;
            this.dateTimePPIssueDate.Size = new System.Drawing.Size(176, 22);
            this.dateTimePPIssueDate.TabIndex = 12;
            this.dateTimePPIssueDate.Value = new System.DateTime(((long)(0)));
            // 
            // textBoxPPIssuePlace
            // 
            this.textBoxPPIssuePlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxPPIssuePlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.textBoxPPIssuePlace.BackColor = System.Drawing.Color.White;
            this.textBoxPPIssuePlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxPPIssuePlace.CustomReportFieldName = "";
            this.textBoxPPIssuePlace.CustomReportKey = "";
            this.textBoxPPIssuePlace.CustomReportValueType = ((byte)(1));
            this.textBoxPPIssuePlace.IsComboTextBox = false;
            this.textBoxPPIssuePlace.IsModified = false;
            this.textBoxPPIssuePlace.Location = new System.Drawing.Point(203, 273);
            this.textBoxPPIssuePlace.MaxLength = 30;
            this.textBoxPPIssuePlace.Name = "textBoxPPIssuePlace";
            this.textBoxPPIssuePlace.Size = new System.Drawing.Size(377, 22);
            this.textBoxPPIssuePlace.TabIndex = 11;
            // 
            // mmLabel1
            // 
            this.mmLabel1.AutoSize = true;
            this.mmLabel1.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel1.IsFieldHeader = false;
            this.mmLabel1.IsRequired = false;
            this.mmLabel1.Location = new System.Drawing.Point(16, 278);
            this.mmLabel1.Name = "mmLabel1";
            this.mmLabel1.PenWidth = 1F;
            this.mmLabel1.ShowBorder = false;
            this.mmLabel1.Size = new System.Drawing.Size(119, 17);
            this.mmLabel1.TabIndex = 133;
            this.mmLabel1.Text = "PP Place of Issue :";
            // 
            // mmLabel2
            // 
            this.mmLabel2.AutoSize = true;
            this.mmLabel2.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel2.IsFieldHeader = false;
            this.mmLabel2.IsRequired = false;
            this.mmLabel2.Location = new System.Drawing.Point(16, 305);
            this.mmLabel2.Name = "mmLabel2";
            this.mmLabel2.PenWidth = 1F;
            this.mmLabel2.ShowBorder = false;
            this.mmLabel2.Size = new System.Drawing.Size(101, 17);
            this.mmLabel2.TabIndex = 134;
            this.mmLabel2.Text = "PP Issue Date :";
            // 
            // textBoxBirthPlace
            // 
            this.textBoxBirthPlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxBirthPlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.textBoxBirthPlace.BackColor = System.Drawing.Color.White;
            this.textBoxBirthPlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxBirthPlace.CustomReportFieldName = "";
            this.textBoxBirthPlace.CustomReportKey = "";
            this.textBoxBirthPlace.CustomReportValueType = ((byte)(1));
            this.textBoxBirthPlace.IsComboTextBox = false;
            this.textBoxBirthPlace.IsModified = false;
            this.textBoxBirthPlace.Location = new System.Drawing.Point(203, 248);
            this.textBoxBirthPlace.MaxLength = 30;
            this.textBoxBirthPlace.Name = "textBoxBirthPlace";
            this.textBoxBirthPlace.Size = new System.Drawing.Size(377, 22);
            this.textBoxBirthPlace.TabIndex = 10;
            // 
            // mmLabel33
            // 
            this.mmLabel33.AutoSize = true;
            this.mmLabel33.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel33.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel33.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel33.IsFieldHeader = false;
            this.mmLabel33.IsRequired = false;
            this.mmLabel33.Location = new System.Drawing.Point(16, 252);
            this.mmLabel33.Name = "mmLabel33";
            this.mmLabel33.PenWidth = 1F;
            this.mmLabel33.ShowBorder = false;
            this.mmLabel33.Size = new System.Drawing.Size(80, 17);
            this.mmLabel33.TabIndex = 132;
            this.mmLabel33.Text = "Birth Place :";
            // 
            // mmLabel51
            // 
            this.mmLabel51.AutoSize = true;
            this.mmLabel51.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel51.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel51.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel51.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel51.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel51.IsFieldHeader = false;
            this.mmLabel51.IsRequired = false;
            this.mmLabel51.Location = new System.Drawing.Point(346, 226);
            this.mmLabel51.Name = "mmLabel51";
            this.mmLabel51.PenWidth = 1F;
            this.mmLabel51.ShowBorder = false;
            this.mmLabel51.Size = new System.Drawing.Size(36, 17);
            this.mmLabel51.TabIndex = 131;
            this.mmLabel51.Text = "Age:";
            // 
            // dateTimeBirthDate
            // 
            this.dateTimeBirthDate.Checked = false;
            this.dateTimeBirthDate.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeBirthDate.Location = new System.Drawing.Point(203, 223);
            this.dateTimeBirthDate.Name = "dateTimeBirthDate";
            this.dateTimeBirthDate.ShowCheckBox = true;
            this.dateTimeBirthDate.Size = new System.Drawing.Size(140, 22);
            this.dateTimeBirthDate.TabIndex = 9;
            this.dateTimeBirthDate.Value = new System.DateTime(((long)(0)));
            // 
            // textBoxAge
            // 
            this.textBoxAge.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxAge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.textBoxAge.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxAge.CustomReportFieldName = "";
            this.textBoxAge.CustomReportKey = "";
            this.textBoxAge.CustomReportValueType = ((byte)(1));
            this.textBoxAge.IsComboTextBox = false;
            this.textBoxAge.IsModified = false;
            this.textBoxAge.Location = new System.Drawing.Point(383, 223);
            this.textBoxAge.MaxLength = 30;
            this.textBoxAge.Name = "textBoxAge";
            this.textBoxAge.ReadOnly = true;
            this.textBoxAge.Size = new System.Drawing.Size(60, 22);
            this.textBoxAge.TabIndex = 112;
            this.textBoxAge.TabStop = false;
            // 
            // mmLabel31
            // 
            this.mmLabel31.AutoSize = true;
            this.mmLabel31.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel31.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel31.IsFieldHeader = false;
            this.mmLabel31.IsRequired = false;
            this.mmLabel31.Location = new System.Drawing.Point(16, 226);
            this.mmLabel31.Name = "mmLabel31";
            this.mmLabel31.PenWidth = 1F;
            this.mmLabel31.ShowBorder = false;
            this.mmLabel31.Size = new System.Drawing.Size(78, 17);
            this.mmLabel31.TabIndex = 130;
            this.mmLabel31.Text = "Birth Date :";
            // 
            // comboBoxGender
            // 
            this.comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Location = new System.Drawing.Point(203, 195);
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.SelectedID = 0;
            this.comboBoxGender.Size = new System.Drawing.Size(114, 24);
            this.comboBoxGender.TabIndex = 8;
            // 
            // comboBoxNationality
            // 
            this.comboBoxNationality.Assigned = false;
            this.comboBoxNationality.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxNationality.CustomReportFieldName = "";
            this.comboBoxNationality.CustomReportKey = "";
            this.comboBoxNationality.CustomReportValueType = ((byte)(1));
            this.comboBoxNationality.DescriptionTextBox = null;
            appearance68.BackColor = System.Drawing.SystemColors.Window;
            appearance68.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxNationality.DisplayLayout.Appearance = appearance68;
            this.comboBoxNationality.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxNationality.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance69.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance69.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxNationality.DisplayLayout.GroupByBox.Appearance = appearance69;
            appearance70.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxNationality.DisplayLayout.GroupByBox.BandLabelAppearance = appearance70;
            this.comboBoxNationality.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance71.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance71.BackColor2 = System.Drawing.SystemColors.Control;
            appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxNationality.DisplayLayout.GroupByBox.PromptAppearance = appearance71;
            this.comboBoxNationality.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxNationality.DisplayLayout.MaxRowScrollRegions = 1;
            appearance72.BackColor = System.Drawing.SystemColors.Window;
            appearance72.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxNationality.DisplayLayout.Override.ActiveCellAppearance = appearance72;
            appearance73.BackColor = System.Drawing.SystemColors.Highlight;
            appearance73.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxNationality.DisplayLayout.Override.ActiveRowAppearance = appearance73;
            this.comboBoxNationality.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxNationality.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance74.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxNationality.DisplayLayout.Override.CardAreaAppearance = appearance74;
            appearance75.BorderColor = System.Drawing.Color.Silver;
            appearance75.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxNationality.DisplayLayout.Override.CellAppearance = appearance75;
            this.comboBoxNationality.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxNationality.DisplayLayout.Override.CellPadding = 0;
            appearance76.BackColor = System.Drawing.SystemColors.Control;
            appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance76.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance76.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxNationality.DisplayLayout.Override.GroupByRowAppearance = appearance76;
            appearance77.TextHAlignAsString = "Left";
            this.comboBoxNationality.DisplayLayout.Override.HeaderAppearance = appearance77;
            this.comboBoxNationality.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxNationality.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance78.BackColor = System.Drawing.SystemColors.Window;
            appearance78.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxNationality.DisplayLayout.Override.RowAppearance = appearance78;
            this.comboBoxNationality.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance79.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxNationality.DisplayLayout.Override.TemplateAddRowAppearance = appearance79;
            this.comboBoxNationality.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxNationality.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxNationality.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxNationality.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxNationality.Editable = true;
            this.comboBoxNationality.FilterString = "";
            this.comboBoxNationality.HasAllAccount = false;
            this.comboBoxNationality.HasCustom = false;
            this.comboBoxNationality.IsDataLoaded = false;
            this.comboBoxNationality.Location = new System.Drawing.Point(203, 170);
            this.comboBoxNationality.MaxDropDownItems = 12;
            this.comboBoxNationality.Name = "comboBoxNationality";
            this.comboBoxNationality.ShowInactiveItems = false;
            this.comboBoxNationality.ShowQuickAdd = true;
            this.comboBoxNationality.Size = new System.Drawing.Size(235, 23);
            this.comboBoxNationality.TabIndex = 7;
            this.comboBoxNationality.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // mmLabel5
            // 
            this.mmLabel5.AutoSize = true;
            this.mmLabel5.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel5.IsFieldHeader = false;
            this.mmLabel5.IsRequired = false;
            this.mmLabel5.Location = new System.Drawing.Point(16, 200);
            this.mmLabel5.Name = "mmLabel5";
            this.mmLabel5.PenWidth = 1F;
            this.mmLabel5.ShowBorder = false;
            this.mmLabel5.Size = new System.Drawing.Size(40, 17);
            this.mmLabel5.TabIndex = 114;
            this.mmLabel5.Text = "Sex :";
            // 
            // labelCandidateNumber
            // 
            this.labelCandidateNumber.AutoSize = true;
            this.labelCandidateNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelCandidateNumber.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.labelCandidateNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelCandidateNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCandidateNumber.IsFieldHeader = false;
            this.labelCandidateNumber.IsRequired = true;
            this.labelCandidateNumber.Location = new System.Drawing.Point(16, 14);
            this.labelCandidateNumber.Name = "labelCandidateNumber";
            this.labelCandidateNumber.PenWidth = 1F;
            this.labelCandidateNumber.ShowBorder = false;
            this.labelCandidateNumber.Size = new System.Drawing.Size(156, 17);
            this.labelCandidateNumber.TabIndex = 103;
            this.labelCandidateNumber.Text = "VS Application Code :";
            this.labelCandidateNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescriptions
            // 
            this.lblDescriptions.AutoSize = true;
            this.lblDescriptions.BackColor = System.Drawing.Color.Transparent;
            this.lblDescriptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.lblDescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescriptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDescriptions.IsFieldHeader = false;
            this.lblDescriptions.IsRequired = false;
            this.lblDescriptions.Location = new System.Drawing.Point(16, 175);
            this.lblDescriptions.Name = "lblDescriptions";
            this.lblDescriptions.PenWidth = 1F;
            this.lblDescriptions.ShowBorder = false;
            this.lblDescriptions.Size = new System.Drawing.Size(80, 17);
            this.lblDescriptions.TabIndex = 111;
            this.lblDescriptions.Text = "Nationality :";
            // 
            // textBoxCode
            // 
            this.textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxCode.CustomReportFieldName = "";
            this.textBoxCode.CustomReportKey = "";
            this.textBoxCode.CustomReportValueType = ((byte)(1));
            this.textBoxCode.IsComboTextBox = false;
            this.textBoxCode.IsModified = false;
            this.textBoxCode.Location = new System.Drawing.Point(203, 13);
            this.textBoxCode.MaxLength = 64;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.ReadOnly = true;
            this.textBoxCode.Size = new System.Drawing.Size(236, 22);
            this.textBoxCode.TabIndex = 124;
            this.textBoxCode.TabStop = false;
            // 
            // textBoxSurName
            // 
            this.textBoxSurName.BackColor = System.Drawing.Color.White;
            this.textBoxSurName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxSurName.CustomReportFieldName = "";
            this.textBoxSurName.CustomReportKey = "";
            this.textBoxSurName.CustomReportValueType = ((byte)(1));
            this.textBoxSurName.IsComboTextBox = false;
            this.textBoxSurName.IsModified = false;
            this.textBoxSurName.IsRequired = true;
            this.textBoxSurName.Location = new System.Drawing.Point(203, 91);
            this.textBoxSurName.MaxLength = 64;
            this.textBoxSurName.Name = "textBoxSurName";
            this.textBoxSurName.Size = new System.Drawing.Size(377, 22);
            this.textBoxSurName.TabIndex = 2;
            // 
            // textBoxGivenName
            // 
            this.textBoxGivenName.BackColor = System.Drawing.Color.White;
            this.textBoxGivenName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxGivenName.CustomReportFieldName = "";
            this.textBoxGivenName.CustomReportKey = "";
            this.textBoxGivenName.CustomReportValueType = ((byte)(1));
            this.textBoxGivenName.IsComboTextBox = false;
            this.textBoxGivenName.IsModified = false;
            this.textBoxGivenName.IsRequired = true;
            this.textBoxGivenName.Location = new System.Drawing.Point(203, 66);
            this.textBoxGivenName.MaxLength = 64;
            this.textBoxGivenName.Name = "textBoxGivenName";
            this.textBoxGivenName.Size = new System.Drawing.Size(377, 22);
            this.textBoxGivenName.TabIndex = 1;
            // 
            // labelGivenName
            // 
            this.labelGivenName.AutoSize = true;
            this.labelGivenName.BackColor = System.Drawing.Color.Transparent;
            this.labelGivenName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.labelGivenName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelGivenName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelGivenName.IsFieldHeader = false;
            this.labelGivenName.IsRequired = false;
            this.labelGivenName.Location = new System.Drawing.Point(16, 68);
            this.labelGivenName.Name = "labelGivenName";
            this.labelGivenName.PenWidth = 1F;
            this.labelGivenName.ShowBorder = false;
            this.labelGivenName.Size = new System.Drawing.Size(91, 17);
            this.labelGivenName.TabIndex = 106;
            this.labelGivenName.Text = "First Name :";
            this.labelGivenName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.panelArrival);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(-12000, -11538);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(974, 592);
            // 
            // panelArrival
            // 
            this.panelArrival.Controls.Add(this.textBox1);
            this.panelArrival.Controls.Add(this.sponsorComboBox);
            this.panelArrival.Controls.Add(this.mmLabel92);
            this.panelArrival.Controls.Add(this.ultraTabControl1);
            this.panelArrival.Controls.Add(this.buttonMakeEmployee);
            this.panelArrival.Controls.Add(this.textBoxEmployeeNo);
            this.panelArrival.Controls.Add(this.mmLabel34);
            this.panelArrival.Controls.Add(this.dateTimeArrivedOn);
            this.panelArrival.Controls.Add(this.mmLabel27);
            this.panelArrival.Location = new System.Drawing.Point(0, 0);
            this.panelArrival.Name = "panelArrival";
            this.panelArrival.Size = new System.Drawing.Size(972, 605);
            this.panelArrival.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.Location = new System.Drawing.Point(325, 55);
            this.textBox1.MaxLength = 64;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(279, 22);
            this.textBox1.TabIndex = 148;
            this.textBox1.TabStop = false;
            // 
            // sponsorComboBox
            // 
            this.sponsorComboBox.Assigned = false;
            this.sponsorComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.sponsorComboBox.CustomReportFieldName = "";
            this.sponsorComboBox.CustomReportKey = "";
            this.sponsorComboBox.CustomReportValueType = ((byte)(1));
            this.sponsorComboBox.DescriptionTextBox = this.textBox1;
            appearance80.BackColor = System.Drawing.SystemColors.Window;
            appearance80.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.sponsorComboBox.DisplayLayout.Appearance = appearance80;
            this.sponsorComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.sponsorComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance81.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance81.BorderColor = System.Drawing.SystemColors.Window;
            this.sponsorComboBox.DisplayLayout.GroupByBox.Appearance = appearance81;
            appearance82.ForeColor = System.Drawing.SystemColors.GrayText;
            this.sponsorComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance82;
            this.sponsorComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance83.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance83.BackColor2 = System.Drawing.SystemColors.Control;
            appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
            this.sponsorComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance83;
            this.sponsorComboBox.DisplayLayout.MaxColScrollRegions = 1;
            this.sponsorComboBox.DisplayLayout.MaxRowScrollRegions = 1;
            appearance84.BackColor = System.Drawing.SystemColors.Window;
            appearance84.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sponsorComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance84;
            appearance85.BackColor = System.Drawing.SystemColors.Highlight;
            appearance85.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.sponsorComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance85;
            this.sponsorComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.sponsorComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance86.BackColor = System.Drawing.SystemColors.Window;
            this.sponsorComboBox.DisplayLayout.Override.CardAreaAppearance = appearance86;
            appearance87.BorderColor = System.Drawing.Color.Silver;
            appearance87.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.sponsorComboBox.DisplayLayout.Override.CellAppearance = appearance87;
            this.sponsorComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.sponsorComboBox.DisplayLayout.Override.CellPadding = 0;
            appearance88.BackColor = System.Drawing.SystemColors.Control;
            appearance88.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance88.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance88.BorderColor = System.Drawing.SystemColors.Window;
            this.sponsorComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance88;
            appearance89.TextHAlignAsString = "Left";
            this.sponsorComboBox.DisplayLayout.Override.HeaderAppearance = appearance89;
            this.sponsorComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.sponsorComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance90.BackColor = System.Drawing.SystemColors.Window;
            appearance90.BorderColor = System.Drawing.Color.Silver;
            this.sponsorComboBox.DisplayLayout.Override.RowAppearance = appearance90;
            this.sponsorComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance91.BackColor = System.Drawing.SystemColors.ControlLight;
            this.sponsorComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance91;
            this.sponsorComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.sponsorComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.sponsorComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.sponsorComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.sponsorComboBox.Editable = true;
            this.sponsorComboBox.FilterString = "";
            this.sponsorComboBox.HasAllAccount = false;
            this.sponsorComboBox.HasCustom = false;
            this.sponsorComboBox.IsDataLoaded = false;
            this.sponsorComboBox.Location = new System.Drawing.Point(163, 55);
            this.sponsorComboBox.MaxDropDownItems = 12;
            this.sponsorComboBox.Name = "sponsorComboBox";
            this.sponsorComboBox.ShowInactiveItems = false;
            this.sponsorComboBox.ShowQuickAdd = true;
            this.sponsorComboBox.Size = new System.Drawing.Size(155, 23);
            this.sponsorComboBox.TabIndex = 147;
            this.sponsorComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.sponsorComboBox.SelectedIndexChanged += new System.EventHandler(this.sponsorComboBox_SelectedIndexChanged);
            // 
            // mmLabel92
            // 
            this.mmLabel92.AutoSize = true;
            this.mmLabel92.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel92.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel92.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel92.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel92.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel92.IsFieldHeader = false;
            this.mmLabel92.IsRequired = false;
            this.mmLabel92.Location = new System.Drawing.Point(32, 60);
            this.mmLabel92.Name = "mmLabel92";
            this.mmLabel92.PenWidth = 1F;
            this.mmLabel92.ShowBorder = false;
            this.mmLabel92.Size = new System.Drawing.Size(107, 17);
            this.mmLabel92.TabIndex = 149;
            this.mmLabel92.Text = "Sponsor Name :";
            // 
            // ultraTabControl1
            // 
            this.ultraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage2);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl6);
            this.ultraTabControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraTabControl1.Location = new System.Drawing.Point(12, 155);
            this.ultraTabControl1.MinTabWidth = 80;
            this.ultraTabControl1.Name = "ultraTabControl1";
            this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage2;
            this.ultraTabControl1.Size = new System.Drawing.Size(948, 446);
            this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
            this.ultraTabControl1.TabIndex = 146;
            appearance92.BackColor = System.Drawing.Color.WhiteSmoke;
            ultraTab1.Appearance = appearance92;
            ultraTab1.TabPage = this.ultraTabPageControl6;
            ultraTab1.Text = "&PayrollItems";
            this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1});
            this.ultraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Standard;
            // 
            // ultraTabSharedControlsPage2
            // 
            this.ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-12000, -11538);
            this.ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
            this.ultraTabSharedControlsPage2.Size = new System.Drawing.Size(946, 422);
            // 
            // buttonMakeEmployee
            // 
            this.buttonMakeEmployee.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonMakeEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMakeEmployee.BackColor = System.Drawing.Color.DarkGray;
            this.buttonMakeEmployee.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonMakeEmployee.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonMakeEmployee.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonMakeEmployee.Enabled = false;
            this.buttonMakeEmployee.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonMakeEmployee.Location = new System.Drawing.Point(325, 80);
            this.buttonMakeEmployee.Name = "buttonMakeEmployee";
            this.buttonMakeEmployee.Size = new System.Drawing.Size(123, 27);
            this.buttonMakeEmployee.TabIndex = 2;
            this.buttonMakeEmployee.Text = "Make Employee";
            this.buttonMakeEmployee.UseVisualStyleBackColor = false;
            this.buttonMakeEmployee.Click += new System.EventHandler(this.toolStripBtnMakeEmployee_Click);
            // 
            // textBoxEmployeeNo
            // 
            this.textBoxEmployeeNo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxEmployeeNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxEmployeeNo.CustomReportFieldName = "";
            this.textBoxEmployeeNo.CustomReportKey = "";
            this.textBoxEmployeeNo.CustomReportValueType = ((byte)(1));
            this.textBoxEmployeeNo.IsComboTextBox = false;
            this.textBoxEmployeeNo.IsModified = false;
            this.textBoxEmployeeNo.Location = new System.Drawing.Point(163, 82);
            this.textBoxEmployeeNo.MaxLength = 15;
            this.textBoxEmployeeNo.Name = "textBoxEmployeeNo";
            this.textBoxEmployeeNo.ReadOnly = true;
            this.textBoxEmployeeNo.Size = new System.Drawing.Size(155, 22);
            this.textBoxEmployeeNo.TabIndex = 3;
            // 
            // mmLabel34
            // 
            this.mmLabel34.AutoSize = true;
            this.mmLabel34.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel34.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel34.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel34.IsFieldHeader = false;
            this.mmLabel34.IsRequired = false;
            this.mmLabel34.Location = new System.Drawing.Point(32, 87);
            this.mmLabel34.Name = "mmLabel34";
            this.mmLabel34.PenWidth = 1F;
            this.mmLabel34.ShowBorder = false;
            this.mmLabel34.Size = new System.Drawing.Size(94, 17);
            this.mmLabel34.TabIndex = 143;
            this.mmLabel34.Text = "Employee No:";
            // 
            // dateTimeArrivedOn
            // 
            this.dateTimeArrivedOn.Checked = false;
            this.dateTimeArrivedOn.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeArrivedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeArrivedOn.Location = new System.Drawing.Point(163, 29);
            this.dateTimeArrivedOn.Name = "dateTimeArrivedOn";
            this.dateTimeArrivedOn.ShowCheckBox = true;
            this.dateTimeArrivedOn.Size = new System.Drawing.Size(155, 22);
            this.dateTimeArrivedOn.TabIndex = 0;
            this.dateTimeArrivedOn.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel27
            // 
            this.mmLabel27.AutoSize = true;
            this.mmLabel27.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel27.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel27.IsFieldHeader = false;
            this.mmLabel27.IsRequired = false;
            this.mmLabel27.Location = new System.Drawing.Point(32, 31);
            this.mmLabel27.Name = "mmLabel27";
            this.mmLabel27.PenWidth = 1F;
            this.mmLabel27.ShowBorder = false;
            this.mmLabel27.Size = new System.Drawing.Size(78, 17);
            this.mmLabel27.TabIndex = 142;
            this.mmLabel27.Text = "Arrived On:";
            // 
            // tabPageDetails
            // 
            this.tabPageDetails.Controls.Add(this.panelRecruitment);
            this.tabPageDetails.Location = new System.Drawing.Point(-12000, -11538);
            this.tabPageDetails.Name = "tabPageDetails";
            this.tabPageDetails.Size = new System.Drawing.Size(974, 592);
            // 
            // panelRecruitment
            // 
            this.panelRecruitment.Controls.Add(this.comboBoxArrivalPort);
            this.panelRecruitment.Controls.Add(this.comboBoxCategory);
            this.panelRecruitment.Controls.Add(this.mmLabel29);
            this.panelRecruitment.Controls.Add(this.mmLabel88);
            this.panelRecruitment.Controls.Add(this.mmLabel81);
            this.panelRecruitment.Controls.Add(this.mmLabel76);
            this.panelRecruitment.Controls.Add(this.textBoxLanguageName);
            this.panelRecruitment.Controls.Add(this.textBoxQualificationName);
            this.panelRecruitment.Controls.Add(this.numericExperienceAbroad);
            this.panelRecruitment.Controls.Add(this.numericExperienceLocal);
            this.panelRecruitment.Controls.Add(this.comboBoxLanguage);
            this.panelRecruitment.Controls.Add(this.comboBoxQualification);
            this.panelRecruitment.Controls.Add(this.mmLabel75);
            this.panelRecruitment.Controls.Add(this.mmLabel74);
            this.panelRecruitment.Controls.Add(this.mmLabel73);
            this.panelRecruitment.Controls.Add(this.mmLabel71);
            this.panelRecruitment.Controls.Add(this.textBoxActualDesignationName);
            this.panelRecruitment.Controls.Add(this.textBoxThroughAgentName);
            this.panelRecruitment.Controls.Add(this.comboBoxPositionActual);
            this.panelRecruitment.Controls.Add(this.mmLabel68);
            this.panelRecruitment.Controls.Add(this.comboBoxAgentThrough);
            this.panelRecruitment.Controls.Add(this.mmLabel55);
            this.panelRecruitment.Controls.Add(this.textBoxRemarks);
            this.panelRecruitment.Controls.Add(this.comboBoxSelectionStatus);
            this.panelRecruitment.Controls.Add(this.mmLabel39);
            this.panelRecruitment.Controls.Add(this.mmLabel38);
            this.panelRecruitment.Controls.Add(this.textBoxSelectedAt);
            this.panelRecruitment.Controls.Add(this.dateTimeSelectedOn);
            this.panelRecruitment.Controls.Add(this.mmLabel37);
            this.panelRecruitment.Controls.Add(this.mmLabel36);
            this.panelRecruitment.Location = new System.Drawing.Point(0, 0);
            this.panelRecruitment.Name = "panelRecruitment";
            this.panelRecruitment.Size = new System.Drawing.Size(972, 610);
            this.panelRecruitment.TabIndex = 0;
            // 
            // comboBoxArrivalPort
            // 
            this.comboBoxArrivalPort.Assigned = false;
            this.comboBoxArrivalPort.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxArrivalPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxArrivalPort.CustomReportFieldName = "";
            this.comboBoxArrivalPort.CustomReportKey = "";
            this.comboBoxArrivalPort.CustomReportValueType = ((byte)(1));
            this.comboBoxArrivalPort.DescriptionTextBox = null;
            appearance93.BackColor = System.Drawing.SystemColors.Window;
            appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxArrivalPort.DisplayLayout.Appearance = appearance93;
            this.comboBoxArrivalPort.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxArrivalPort.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance94.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxArrivalPort.DisplayLayout.GroupByBox.Appearance = appearance94;
            appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxArrivalPort.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
            this.comboBoxArrivalPort.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance96.BackColor2 = System.Drawing.SystemColors.Control;
            appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxArrivalPort.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
            this.comboBoxArrivalPort.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxArrivalPort.DisplayLayout.MaxRowScrollRegions = 1;
            appearance97.BackColor = System.Drawing.SystemColors.Window;
            appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxArrivalPort.DisplayLayout.Override.ActiveCellAppearance = appearance97;
            appearance98.BackColor = System.Drawing.SystemColors.Highlight;
            appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxArrivalPort.DisplayLayout.Override.ActiveRowAppearance = appearance98;
            this.comboBoxArrivalPort.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxArrivalPort.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance99.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxArrivalPort.DisplayLayout.Override.CardAreaAppearance = appearance99;
            appearance100.BorderColor = System.Drawing.Color.Silver;
            appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxArrivalPort.DisplayLayout.Override.CellAppearance = appearance100;
            this.comboBoxArrivalPort.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxArrivalPort.DisplayLayout.Override.CellPadding = 0;
            appearance101.BackColor = System.Drawing.SystemColors.Control;
            appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance101.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxArrivalPort.DisplayLayout.Override.GroupByRowAppearance = appearance101;
            appearance102.TextHAlignAsString = "Left";
            this.comboBoxArrivalPort.DisplayLayout.Override.HeaderAppearance = appearance102;
            this.comboBoxArrivalPort.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxArrivalPort.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance103.BackColor = System.Drawing.SystemColors.Window;
            appearance103.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxArrivalPort.DisplayLayout.Override.RowAppearance = appearance103;
            this.comboBoxArrivalPort.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxArrivalPort.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
            this.comboBoxArrivalPort.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxArrivalPort.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxArrivalPort.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxArrivalPort.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxArrivalPort.Editable = true;
            this.comboBoxArrivalPort.FilterString = "";
            this.comboBoxArrivalPort.HasAllAccount = false;
            this.comboBoxArrivalPort.HasCustom = false;
            this.comboBoxArrivalPort.IsDataLoaded = false;
            this.comboBoxArrivalPort.Location = new System.Drawing.Point(199, 282);
            this.comboBoxArrivalPort.MaxDropDownItems = 12;
            this.comboBoxArrivalPort.Name = "comboBoxArrivalPort";
            this.comboBoxArrivalPort.ShowInactiveItems = false;
            this.comboBoxArrivalPort.ShowQuickAdd = true;
            this.comboBoxArrivalPort.Size = new System.Drawing.Size(155, 23);
            this.comboBoxArrivalPort.TabIndex = 146;
            this.comboBoxArrivalPort.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxArrivalPort.Visible = false;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Assigned = false;
            this.comboBoxCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxCategory.CustomReportFieldName = "";
            this.comboBoxCategory.CustomReportKey = "";
            this.comboBoxCategory.CustomReportValueType = ((byte)(1));
            this.comboBoxCategory.DescriptionTextBox = null;
            appearance105.BackColor = System.Drawing.SystemColors.Window;
            appearance105.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxCategory.DisplayLayout.Appearance = appearance105;
            this.comboBoxCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance106.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance106.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCategory.DisplayLayout.GroupByBox.Appearance = appearance106;
            appearance107.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance107;
            this.comboBoxCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance108.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance108.BackColor2 = System.Drawing.SystemColors.Control;
            appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance108;
            this.comboBoxCategory.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxCategory.DisplayLayout.MaxRowScrollRegions = 1;
            appearance109.BackColor = System.Drawing.SystemColors.Window;
            appearance109.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxCategory.DisplayLayout.Override.ActiveCellAppearance = appearance109;
            appearance110.BackColor = System.Drawing.SystemColors.Highlight;
            appearance110.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxCategory.DisplayLayout.Override.ActiveRowAppearance = appearance110;
            this.comboBoxCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance111.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxCategory.DisplayLayout.Override.CardAreaAppearance = appearance111;
            appearance112.BorderColor = System.Drawing.Color.Silver;
            appearance112.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxCategory.DisplayLayout.Override.CellAppearance = appearance112;
            this.comboBoxCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxCategory.DisplayLayout.Override.CellPadding = 0;
            appearance113.BackColor = System.Drawing.SystemColors.Control;
            appearance113.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance113.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance113.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxCategory.DisplayLayout.Override.GroupByRowAppearance = appearance113;
            appearance114.TextHAlignAsString = "Left";
            this.comboBoxCategory.DisplayLayout.Override.HeaderAppearance = appearance114;
            this.comboBoxCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance115.BackColor = System.Drawing.SystemColors.Window;
            appearance115.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxCategory.DisplayLayout.Override.RowAppearance = appearance115;
            this.comboBoxCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance116.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance116;
            this.comboBoxCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxCategory.Editable = true;
            this.comboBoxCategory.FilterString = "";
            this.comboBoxCategory.HasAllAccount = false;
            this.comboBoxCategory.HasCustom = false;
            this.comboBoxCategory.IsDataLoaded = false;
            this.comboBoxCategory.Location = new System.Drawing.Point(199, 307);
            this.comboBoxCategory.MaxDropDownItems = 12;
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.ShowInactiveItems = false;
            this.comboBoxCategory.ShowQuickAdd = true;
            this.comboBoxCategory.Size = new System.Drawing.Size(155, 23);
            this.comboBoxCategory.TabIndex = 147;
            this.comboBoxCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.comboBoxCategory.Visible = false;
            // 
            // mmLabel29
            // 
            this.mmLabel29.AutoSize = true;
            this.mmLabel29.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel29.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel29.IsFieldHeader = false;
            this.mmLabel29.IsRequired = false;
            this.mmLabel29.Location = new System.Drawing.Point(18, 290);
            this.mmLabel29.Name = "mmLabel29";
            this.mmLabel29.PenWidth = 1F;
            this.mmLabel29.ShowBorder = false;
            this.mmLabel29.Size = new System.Drawing.Size(39, 17);
            this.mmLabel29.TabIndex = 149;
            this.mmLabel29.Text = "Port:";
            this.mmLabel29.Visible = false;
            // 
            // mmLabel88
            // 
            this.mmLabel88.AutoSize = true;
            this.mmLabel88.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel88.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel88.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel88.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel88.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel88.IsFieldHeader = false;
            this.mmLabel88.IsRequired = false;
            this.mmLabel88.Location = new System.Drawing.Point(18, 316);
            this.mmLabel88.Name = "mmLabel88";
            this.mmLabel88.PenWidth = 1F;
            this.mmLabel88.ShowBorder = false;
            this.mmLabel88.Size = new System.Drawing.Size(70, 17);
            this.mmLabel88.TabIndex = 148;
            this.mmLabel88.Text = "Category:";
            this.mmLabel88.Visible = false;
            // 
            // mmLabel81
            // 
            this.mmLabel81.AutoSize = true;
            this.mmLabel81.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel81.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel81.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel81.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel81.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel81.IsFieldHeader = false;
            this.mmLabel81.IsRequired = false;
            this.mmLabel81.Location = new System.Drawing.Point(274, 230);
            this.mmLabel81.Name = "mmLabel81";
            this.mmLabel81.PenWidth = 1F;
            this.mmLabel81.ShowBorder = false;
            this.mmLabel81.Size = new System.Drawing.Size(41, 17);
            this.mmLabel81.TabIndex = 135;
            this.mmLabel81.Text = "Years";
            // 
            // mmLabel76
            // 
            this.mmLabel76.AutoSize = true;
            this.mmLabel76.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel76.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel76.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel76.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel76.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel76.IsFieldHeader = false;
            this.mmLabel76.IsRequired = false;
            this.mmLabel76.Location = new System.Drawing.Point(272, 203);
            this.mmLabel76.Name = "mmLabel76";
            this.mmLabel76.PenWidth = 1F;
            this.mmLabel76.ShowBorder = false;
            this.mmLabel76.Size = new System.Drawing.Size(41, 17);
            this.mmLabel76.TabIndex = 134;
            this.mmLabel76.Text = "Years";
            // 
            // textBoxLanguageName
            // 
            this.textBoxLanguageName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxLanguageName.Location = new System.Drawing.Point(349, 174);
            this.textBoxLanguageName.MaxLength = 64;
            this.textBoxLanguageName.Name = "textBoxLanguageName";
            this.textBoxLanguageName.ReadOnly = true;
            this.textBoxLanguageName.Size = new System.Drawing.Size(345, 22);
            this.textBoxLanguageName.TabIndex = 10;
            this.textBoxLanguageName.TabStop = false;
            // 
            // textBoxQualificationName
            // 
            this.textBoxQualificationName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxQualificationName.Location = new System.Drawing.Point(349, 149);
            this.textBoxQualificationName.MaxLength = 64;
            this.textBoxQualificationName.Name = "textBoxQualificationName";
            this.textBoxQualificationName.ReadOnly = true;
            this.textBoxQualificationName.Size = new System.Drawing.Size(345, 22);
            this.textBoxQualificationName.TabIndex = 8;
            this.textBoxQualificationName.TabStop = false;
            // 
            // numericExperienceAbroad
            // 
            this.numericExperienceAbroad.DecimalPlaces = 1;
            this.numericExperienceAbroad.Increment = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.numericExperienceAbroad.Location = new System.Drawing.Point(199, 225);
            this.numericExperienceAbroad.Name = "numericExperienceAbroad";
            this.numericExperienceAbroad.Size = new System.Drawing.Size(67, 22);
            this.numericExperienceAbroad.TabIndex = 12;
            // 
            // numericExperienceLocal
            // 
            this.numericExperienceLocal.DecimalPlaces = 1;
            this.numericExperienceLocal.Increment = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.numericExperienceLocal.Location = new System.Drawing.Point(199, 200);
            this.numericExperienceLocal.Name = "numericExperienceLocal";
            this.numericExperienceLocal.Size = new System.Drawing.Size(67, 22);
            this.numericExperienceLocal.TabIndex = 11;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.Assigned = false;
            this.comboBoxLanguage.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxLanguage.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxLanguage.CustomReportFieldName = "";
            this.comboBoxLanguage.CustomReportKey = "";
            this.comboBoxLanguage.CustomReportValueType = ((byte)(1));
            this.comboBoxLanguage.DescriptionTextBox = this.textBoxLanguageName;
            appearance117.BackColor = System.Drawing.SystemColors.Window;
            appearance117.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxLanguage.DisplayLayout.Appearance = appearance117;
            this.comboBoxLanguage.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxLanguage.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance118.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance118.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxLanguage.DisplayLayout.GroupByBox.Appearance = appearance118;
            appearance119.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxLanguage.DisplayLayout.GroupByBox.BandLabelAppearance = appearance119;
            this.comboBoxLanguage.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance120.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance120.BackColor2 = System.Drawing.SystemColors.Control;
            appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxLanguage.DisplayLayout.GroupByBox.PromptAppearance = appearance120;
            this.comboBoxLanguage.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxLanguage.DisplayLayout.MaxRowScrollRegions = 1;
            appearance121.BackColor = System.Drawing.SystemColors.Window;
            appearance121.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxLanguage.DisplayLayout.Override.ActiveCellAppearance = appearance121;
            appearance122.BackColor = System.Drawing.SystemColors.Highlight;
            appearance122.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxLanguage.DisplayLayout.Override.ActiveRowAppearance = appearance122;
            this.comboBoxLanguage.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxLanguage.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance123.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxLanguage.DisplayLayout.Override.CardAreaAppearance = appearance123;
            appearance124.BorderColor = System.Drawing.Color.Silver;
            appearance124.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxLanguage.DisplayLayout.Override.CellAppearance = appearance124;
            this.comboBoxLanguage.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxLanguage.DisplayLayout.Override.CellPadding = 0;
            appearance125.BackColor = System.Drawing.SystemColors.Control;
            appearance125.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance125.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance125.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxLanguage.DisplayLayout.Override.GroupByRowAppearance = appearance125;
            appearance126.TextHAlignAsString = "Left";
            this.comboBoxLanguage.DisplayLayout.Override.HeaderAppearance = appearance126;
            this.comboBoxLanguage.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxLanguage.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance127.BackColor = System.Drawing.SystemColors.Window;
            appearance127.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxLanguage.DisplayLayout.Override.RowAppearance = appearance127;
            this.comboBoxLanguage.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance128.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxLanguage.DisplayLayout.Override.TemplateAddRowAppearance = appearance128;
            this.comboBoxLanguage.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxLanguage.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxLanguage.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxLanguage.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxLanguage.Editable = true;
            this.comboBoxLanguage.FilterString = "";
            this.comboBoxLanguage.HasAllAccount = false;
            this.comboBoxLanguage.HasCustom = false;
            this.comboBoxLanguage.IsDataLoaded = false;
            this.comboBoxLanguage.Location = new System.Drawing.Point(199, 174);
            this.comboBoxLanguage.MaxDropDownItems = 12;
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.ShowInactiveItems = false;
            this.comboBoxLanguage.ShowQuickAdd = true;
            this.comboBoxLanguage.Size = new System.Drawing.Size(149, 23);
            this.comboBoxLanguage.TabIndex = 9;
            this.comboBoxLanguage.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // comboBoxQualification
            // 
            this.comboBoxQualification.Assigned = false;
            this.comboBoxQualification.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxQualification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxQualification.CustomReportFieldName = "";
            this.comboBoxQualification.CustomReportKey = "";
            this.comboBoxQualification.CustomReportValueType = ((byte)(1));
            this.comboBoxQualification.DescriptionTextBox = this.textBoxQualificationName;
            appearance129.BackColor = System.Drawing.SystemColors.Window;
            appearance129.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxQualification.DisplayLayout.Appearance = appearance129;
            this.comboBoxQualification.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxQualification.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance130.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance130.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance130.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxQualification.DisplayLayout.GroupByBox.Appearance = appearance130;
            appearance131.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxQualification.DisplayLayout.GroupByBox.BandLabelAppearance = appearance131;
            this.comboBoxQualification.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance132.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance132.BackColor2 = System.Drawing.SystemColors.Control;
            appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxQualification.DisplayLayout.GroupByBox.PromptAppearance = appearance132;
            this.comboBoxQualification.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxQualification.DisplayLayout.MaxRowScrollRegions = 1;
            appearance133.BackColor = System.Drawing.SystemColors.Window;
            appearance133.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxQualification.DisplayLayout.Override.ActiveCellAppearance = appearance133;
            appearance134.BackColor = System.Drawing.SystemColors.Highlight;
            appearance134.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxQualification.DisplayLayout.Override.ActiveRowAppearance = appearance134;
            this.comboBoxQualification.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxQualification.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance135.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxQualification.DisplayLayout.Override.CardAreaAppearance = appearance135;
            appearance136.BorderColor = System.Drawing.Color.Silver;
            appearance136.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxQualification.DisplayLayout.Override.CellAppearance = appearance136;
            this.comboBoxQualification.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxQualification.DisplayLayout.Override.CellPadding = 0;
            appearance137.BackColor = System.Drawing.SystemColors.Control;
            appearance137.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance137.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance137.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance137.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxQualification.DisplayLayout.Override.GroupByRowAppearance = appearance137;
            appearance138.TextHAlignAsString = "Left";
            this.comboBoxQualification.DisplayLayout.Override.HeaderAppearance = appearance138;
            this.comboBoxQualification.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxQualification.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance139.BackColor = System.Drawing.SystemColors.Window;
            appearance139.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxQualification.DisplayLayout.Override.RowAppearance = appearance139;
            this.comboBoxQualification.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance140.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxQualification.DisplayLayout.Override.TemplateAddRowAppearance = appearance140;
            this.comboBoxQualification.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxQualification.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxQualification.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxQualification.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxQualification.Editable = true;
            this.comboBoxQualification.FilterString = "";
            this.comboBoxQualification.HasAllAccount = false;
            this.comboBoxQualification.HasCustom = false;
            this.comboBoxQualification.IsDataLoaded = false;
            this.comboBoxQualification.Location = new System.Drawing.Point(199, 149);
            this.comboBoxQualification.MaxDropDownItems = 12;
            this.comboBoxQualification.Name = "comboBoxQualification";
            this.comboBoxQualification.ShowInactiveItems = false;
            this.comboBoxQualification.ShowQuickAdd = true;
            this.comboBoxQualification.Size = new System.Drawing.Size(149, 23);
            this.comboBoxQualification.TabIndex = 7;
            this.comboBoxQualification.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // mmLabel75
            // 
            this.mmLabel75.AutoSize = true;
            this.mmLabel75.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel75.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel75.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel75.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel75.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel75.IsFieldHeader = false;
            this.mmLabel75.IsRequired = false;
            this.mmLabel75.Location = new System.Drawing.Point(18, 230);
            this.mmLabel75.Name = "mmLabel75";
            this.mmLabel75.PenWidth = 1F;
            this.mmLabel75.ShowBorder = false;
            this.mmLabel75.Size = new System.Drawing.Size(141, 17);
            this.mmLabel75.TabIndex = 131;
            this.mmLabel75.Text = "Experience - Abroad :";
            // 
            // mmLabel74
            // 
            this.mmLabel74.AutoSize = true;
            this.mmLabel74.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel74.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel74.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel74.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel74.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel74.IsFieldHeader = false;
            this.mmLabel74.IsRequired = false;
            this.mmLabel74.Location = new System.Drawing.Point(18, 179);
            this.mmLabel74.Name = "mmLabel74";
            this.mmLabel74.PenWidth = 1F;
            this.mmLabel74.ShowBorder = false;
            this.mmLabel74.Size = new System.Drawing.Size(77, 17);
            this.mmLabel74.TabIndex = 130;
            this.mmLabel74.Text = "Language :";
            // 
            // mmLabel73
            // 
            this.mmLabel73.AutoSize = true;
            this.mmLabel73.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel73.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel73.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel73.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel73.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel73.IsFieldHeader = false;
            this.mmLabel73.IsRequired = false;
            this.mmLabel73.Location = new System.Drawing.Point(18, 204);
            this.mmLabel73.Name = "mmLabel73";
            this.mmLabel73.PenWidth = 1F;
            this.mmLabel73.ShowBorder = false;
            this.mmLabel73.Size = new System.Drawing.Size(128, 17);
            this.mmLabel73.TabIndex = 129;
            this.mmLabel73.Text = "Experience - Local :";
            // 
            // mmLabel71
            // 
            this.mmLabel71.AutoSize = true;
            this.mmLabel71.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel71.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel71.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel71.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel71.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel71.IsFieldHeader = false;
            this.mmLabel71.IsRequired = false;
            this.mmLabel71.Location = new System.Drawing.Point(18, 152);
            this.mmLabel71.Name = "mmLabel71";
            this.mmLabel71.PenWidth = 1F;
            this.mmLabel71.ShowBorder = false;
            this.mmLabel71.Size = new System.Drawing.Size(89, 17);
            this.mmLabel71.TabIndex = 128;
            this.mmLabel71.Text = "Qualification :";
            // 
            // textBoxActualDesignationName
            // 
            this.textBoxActualDesignationName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxActualDesignationName.Location = new System.Drawing.Point(349, 123);
            this.textBoxActualDesignationName.MaxLength = 64;
            this.textBoxActualDesignationName.Name = "textBoxActualDesignationName";
            this.textBoxActualDesignationName.ReadOnly = true;
            this.textBoxActualDesignationName.Size = new System.Drawing.Size(345, 22);
            this.textBoxActualDesignationName.TabIndex = 6;
            this.textBoxActualDesignationName.TabStop = false;
            // 
            // textBoxThroughAgentName
            // 
            this.textBoxThroughAgentName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxThroughAgentName.Location = new System.Drawing.Point(349, 98);
            this.textBoxThroughAgentName.MaxLength = 64;
            this.textBoxThroughAgentName.Name = "textBoxThroughAgentName";
            this.textBoxThroughAgentName.ReadOnly = true;
            this.textBoxThroughAgentName.Size = new System.Drawing.Size(345, 22);
            this.textBoxThroughAgentName.TabIndex = 4;
            this.textBoxThroughAgentName.TabStop = false;
            // 
            // comboBoxPositionActual
            // 
            this.comboBoxPositionActual.Assigned = false;
            this.comboBoxPositionActual.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxPositionActual.CustomReportFieldName = "";
            this.comboBoxPositionActual.CustomReportKey = "";
            this.comboBoxPositionActual.CustomReportValueType = ((byte)(1));
            this.comboBoxPositionActual.DescriptionTextBox = this.textBoxActualDesignationName;
            appearance141.BackColor = System.Drawing.SystemColors.Window;
            appearance141.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxPositionActual.DisplayLayout.Appearance = appearance141;
            this.comboBoxPositionActual.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxPositionActual.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance142.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance142.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance142.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance142.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPositionActual.DisplayLayout.GroupByBox.Appearance = appearance142;
            appearance143.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPositionActual.DisplayLayout.GroupByBox.BandLabelAppearance = appearance143;
            this.comboBoxPositionActual.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance144.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance144.BackColor2 = System.Drawing.SystemColors.Control;
            appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance144.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPositionActual.DisplayLayout.GroupByBox.PromptAppearance = appearance144;
            this.comboBoxPositionActual.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxPositionActual.DisplayLayout.MaxRowScrollRegions = 1;
            appearance145.BackColor = System.Drawing.SystemColors.Window;
            appearance145.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxPositionActual.DisplayLayout.Override.ActiveCellAppearance = appearance145;
            appearance146.BackColor = System.Drawing.SystemColors.Highlight;
            appearance146.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxPositionActual.DisplayLayout.Override.ActiveRowAppearance = appearance146;
            this.comboBoxPositionActual.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxPositionActual.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance147.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxPositionActual.DisplayLayout.Override.CardAreaAppearance = appearance147;
            appearance148.BorderColor = System.Drawing.Color.Silver;
            appearance148.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxPositionActual.DisplayLayout.Override.CellAppearance = appearance148;
            this.comboBoxPositionActual.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxPositionActual.DisplayLayout.Override.CellPadding = 0;
            appearance149.BackColor = System.Drawing.SystemColors.Control;
            appearance149.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance149.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance149.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance149.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPositionActual.DisplayLayout.Override.GroupByRowAppearance = appearance149;
            appearance150.TextHAlignAsString = "Left";
            this.comboBoxPositionActual.DisplayLayout.Override.HeaderAppearance = appearance150;
            this.comboBoxPositionActual.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxPositionActual.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance151.BackColor = System.Drawing.SystemColors.Window;
            appearance151.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxPositionActual.DisplayLayout.Override.RowAppearance = appearance151;
            this.comboBoxPositionActual.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance152.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxPositionActual.DisplayLayout.Override.TemplateAddRowAppearance = appearance152;
            this.comboBoxPositionActual.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxPositionActual.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxPositionActual.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxPositionActual.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxPositionActual.Editable = true;
            this.comboBoxPositionActual.FilterString = "";
            this.comboBoxPositionActual.HasAllAccount = false;
            this.comboBoxPositionActual.HasCustom = false;
            this.comboBoxPositionActual.IsDataLoaded = false;
            this.comboBoxPositionActual.Location = new System.Drawing.Point(199, 123);
            this.comboBoxPositionActual.MaxDropDownItems = 12;
            this.comboBoxPositionActual.Name = "comboBoxPositionActual";
            this.comboBoxPositionActual.ShowInactiveItems = false;
            this.comboBoxPositionActual.ShowQuickAdd = true;
            this.comboBoxPositionActual.Size = new System.Drawing.Size(149, 23);
            this.comboBoxPositionActual.TabIndex = 5;
            this.comboBoxPositionActual.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // mmLabel68
            // 
            this.mmLabel68.AutoSize = true;
            this.mmLabel68.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel68.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel68.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel68.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel68.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel68.IsFieldHeader = false;
            this.mmLabel68.IsRequired = false;
            this.mmLabel68.Location = new System.Drawing.Point(18, 126);
            this.mmLabel68.Name = "mmLabel68";
            this.mmLabel68.PenWidth = 1F;
            this.mmLabel68.ShowBorder = false;
            this.mmLabel68.Size = new System.Drawing.Size(129, 17);
            this.mmLabel68.TabIndex = 127;
            this.mmLabel68.Text = "Actual Designation :";
            // 
            // comboBoxAgentThrough
            // 
            this.comboBoxAgentThrough.Assigned = false;
            this.comboBoxAgentThrough.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
            this.comboBoxAgentThrough.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxAgentThrough.CustomReportFieldName = "";
            this.comboBoxAgentThrough.CustomReportKey = "";
            this.comboBoxAgentThrough.CustomReportValueType = ((byte)(1));
            this.comboBoxAgentThrough.DescriptionTextBox = this.textBoxThroughAgentName;
            appearance153.BackColor = System.Drawing.SystemColors.Window;
            appearance153.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxAgentThrough.DisplayLayout.Appearance = appearance153;
            this.comboBoxAgentThrough.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxAgentThrough.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance154.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance154.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance154.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance154.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxAgentThrough.DisplayLayout.GroupByBox.Appearance = appearance154;
            appearance155.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxAgentThrough.DisplayLayout.GroupByBox.BandLabelAppearance = appearance155;
            this.comboBoxAgentThrough.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance156.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance156.BackColor2 = System.Drawing.SystemColors.Control;
            appearance156.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance156.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxAgentThrough.DisplayLayout.GroupByBox.PromptAppearance = appearance156;
            this.comboBoxAgentThrough.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxAgentThrough.DisplayLayout.MaxRowScrollRegions = 1;
            appearance157.BackColor = System.Drawing.SystemColors.Window;
            appearance157.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxAgentThrough.DisplayLayout.Override.ActiveCellAppearance = appearance157;
            appearance158.BackColor = System.Drawing.SystemColors.Highlight;
            appearance158.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxAgentThrough.DisplayLayout.Override.ActiveRowAppearance = appearance158;
            this.comboBoxAgentThrough.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxAgentThrough.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance159.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxAgentThrough.DisplayLayout.Override.CardAreaAppearance = appearance159;
            appearance160.BorderColor = System.Drawing.Color.Silver;
            appearance160.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxAgentThrough.DisplayLayout.Override.CellAppearance = appearance160;
            this.comboBoxAgentThrough.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxAgentThrough.DisplayLayout.Override.CellPadding = 0;
            appearance161.BackColor = System.Drawing.SystemColors.Control;
            appearance161.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance161.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance161.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance161.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxAgentThrough.DisplayLayout.Override.GroupByRowAppearance = appearance161;
            appearance162.TextHAlignAsString = "Left";
            this.comboBoxAgentThrough.DisplayLayout.Override.HeaderAppearance = appearance162;
            this.comboBoxAgentThrough.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxAgentThrough.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance163.BackColor = System.Drawing.SystemColors.Window;
            appearance163.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxAgentThrough.DisplayLayout.Override.RowAppearance = appearance163;
            this.comboBoxAgentThrough.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance164.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxAgentThrough.DisplayLayout.Override.TemplateAddRowAppearance = appearance164;
            this.comboBoxAgentThrough.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxAgentThrough.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxAgentThrough.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxAgentThrough.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxAgentThrough.Editable = true;
            this.comboBoxAgentThrough.FilterString = "";
            this.comboBoxAgentThrough.HasAllAccount = false;
            this.comboBoxAgentThrough.HasCustom = false;
            this.comboBoxAgentThrough.IsDataLoaded = false;
            this.comboBoxAgentThrough.Location = new System.Drawing.Point(199, 98);
            this.comboBoxAgentThrough.MaxDropDownItems = 12;
            this.comboBoxAgentThrough.MaxLength = 100;
            this.comboBoxAgentThrough.Name = "comboBoxAgentThrough";
            this.comboBoxAgentThrough.ShowInactiveItems = false;
            this.comboBoxAgentThrough.ShowQuickAdd = true;
            this.comboBoxAgentThrough.Size = new System.Drawing.Size(149, 23);
            this.comboBoxAgentThrough.TabIndex = 3;
            this.comboBoxAgentThrough.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // mmLabel55
            // 
            this.mmLabel55.AutoSize = true;
            this.mmLabel55.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel55.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel55.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel55.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel55.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel55.IsFieldHeader = false;
            this.mmLabel55.IsRequired = false;
            this.mmLabel55.Location = new System.Drawing.Point(18, 255);
            this.mmLabel55.Name = "mmLabel55";
            this.mmLabel55.PenWidth = 1F;
            this.mmLabel55.ShowBorder = false;
            this.mmLabel55.Size = new System.Drawing.Size(115, 17);
            this.mmLabel55.TabIndex = 126;
            this.mmLabel55.Text = "Special Remarks :";
            // 
            // textBoxRemarks
            // 
            this.textBoxRemarks.BackColor = System.Drawing.Color.White;
            this.textBoxRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxRemarks.CustomReportFieldName = "";
            this.textBoxRemarks.CustomReportKey = "";
            this.textBoxRemarks.CustomReportValueType = ((byte)(1));
            this.textBoxRemarks.IsComboTextBox = false;
            this.textBoxRemarks.IsModified = false;
            this.textBoxRemarks.Location = new System.Drawing.Point(199, 357);
            this.textBoxRemarks.MaxLength = 255;
            this.textBoxRemarks.Multiline = true;
            this.textBoxRemarks.Name = "textBoxRemarks";
            this.textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxRemarks.Size = new System.Drawing.Size(757, 129);
            this.textBoxRemarks.TabIndex = 13;
            // 
            // comboBoxSelectionStatus
            // 
            this.comboBoxSelectionStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectionStatus.FormattingEnabled = true;
            this.comboBoxSelectionStatus.Location = new System.Drawing.Point(199, 21);
            this.comboBoxSelectionStatus.Name = "comboBoxSelectionStatus";
            this.comboBoxSelectionStatus.SelectedID = 0;
            this.comboBoxSelectionStatus.Size = new System.Drawing.Size(149, 24);
            this.comboBoxSelectionStatus.TabIndex = 0;
            // 
            // mmLabel39
            // 
            this.mmLabel39.AutoSize = true;
            this.mmLabel39.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel39.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel39.IsFieldHeader = false;
            this.mmLabel39.IsRequired = false;
            this.mmLabel39.Location = new System.Drawing.Point(18, 103);
            this.mmLabel39.Name = "mmLabel39";
            this.mmLabel39.PenWidth = 1F;
            this.mmLabel39.ShowBorder = false;
            this.mmLabel39.Size = new System.Drawing.Size(110, 17);
            this.mmLabel39.TabIndex = 125;
            this.mmLabel39.Text = "Through Agent :";
            // 
            // mmLabel38
            // 
            this.mmLabel38.AutoSize = true;
            this.mmLabel38.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel38.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel38.IsFieldHeader = false;
            this.mmLabel38.IsRequired = false;
            this.mmLabel38.Location = new System.Drawing.Point(18, 77);
            this.mmLabel38.Name = "mmLabel38";
            this.mmLabel38.PenWidth = 1F;
            this.mmLabel38.ShowBorder = false;
            this.mmLabel38.Size = new System.Drawing.Size(85, 17);
            this.mmLabel38.TabIndex = 124;
            this.mmLabel38.Text = "Selected At :";
            // 
            // textBoxSelectedAt
            // 
            this.textBoxSelectedAt.AutoCompleteCustomSource.AddRange(new string[] {
            "A+",
            "A-",
            "B+",
            "B-",
            "AB+",
            "AB-",
            "O+",
            "O-"});
            this.textBoxSelectedAt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxSelectedAt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxSelectedAt.BackColor = System.Drawing.Color.White;
            this.textBoxSelectedAt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxSelectedAt.CustomReportFieldName = "";
            this.textBoxSelectedAt.CustomReportKey = "";
            this.textBoxSelectedAt.CustomReportValueType = ((byte)(1));
            this.textBoxSelectedAt.IsComboTextBox = false;
            this.textBoxSelectedAt.IsModified = false;
            this.textBoxSelectedAt.Location = new System.Drawing.Point(199, 73);
            this.textBoxSelectedAt.MaxLength = 30;
            this.textBoxSelectedAt.Name = "textBoxSelectedAt";
            this.textBoxSelectedAt.Size = new System.Drawing.Size(251, 22);
            this.textBoxSelectedAt.TabIndex = 2;
            // 
            // dateTimeSelectedOn
            // 
            this.dateTimeSelectedOn.Checked = false;
            this.dateTimeSelectedOn.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeSelectedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeSelectedOn.Location = new System.Drawing.Point(199, 47);
            this.dateTimeSelectedOn.Name = "dateTimeSelectedOn";
            this.dateTimeSelectedOn.ShowCheckBox = true;
            this.dateTimeSelectedOn.Size = new System.Drawing.Size(149, 22);
            this.dateTimeSelectedOn.TabIndex = 1;
            this.dateTimeSelectedOn.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel37
            // 
            this.mmLabel37.AutoSize = true;
            this.mmLabel37.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel37.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel37.IsFieldHeader = false;
            this.mmLabel37.IsRequired = false;
            this.mmLabel37.Location = new System.Drawing.Point(18, 51);
            this.mmLabel37.Name = "mmLabel37";
            this.mmLabel37.PenWidth = 1F;
            this.mmLabel37.ShowBorder = false;
            this.mmLabel37.Size = new System.Drawing.Size(90, 17);
            this.mmLabel37.TabIndex = 123;
            this.mmLabel37.Text = "Selected On :";
            // 
            // mmLabel36
            // 
            this.mmLabel36.AutoSize = true;
            this.mmLabel36.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel36.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel36.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel36.IsFieldHeader = false;
            this.mmLabel36.IsRequired = false;
            this.mmLabel36.Location = new System.Drawing.Point(18, 24);
            this.mmLabel36.Name = "mmLabel36";
            this.mmLabel36.PenWidth = 1F;
            this.mmLabel36.ShowBorder = false;
            this.mmLabel36.Size = new System.Drawing.Size(114, 17);
            this.mmLabel36.TabIndex = 122;
            this.mmLabel36.Text = "Selection Status :";
            // 
            // ultraTabPageControl4
            // 
            this.ultraTabPageControl4.Controls.Add(this.panelVisaProcess);
            this.ultraTabPageControl4.Location = new System.Drawing.Point(-12000, -11538);
            this.ultraTabPageControl4.Name = "ultraTabPageControl4";
            this.ultraTabPageControl4.Size = new System.Drawing.Size(974, 592);
            // 
            // panelVisaProcess
            // 
            this.panelVisaProcess.Controls.Add(this.panelVisaIMG);
            this.panelVisaProcess.Controls.Add(this.panelVisaMOL);
            this.panelVisaProcess.Enabled = false;
            this.panelVisaProcess.Location = new System.Drawing.Point(-2, 0);
            this.panelVisaProcess.Name = "panelVisaProcess";
            this.panelVisaProcess.Size = new System.Drawing.Size(976, 608);
            this.panelVisaProcess.TabIndex = 0;
            // 
            // panelVisaIMG
            // 
            this.panelVisaIMG.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            this.panelVisaIMG.Controls.Add(this.mmLabel77);
            this.panelVisaIMG.Controls.Add(this.dateTimeVisaCopyToAgentOn);
            this.panelVisaIMG.Controls.Add(this.comboBoxVisaAppliedThroughIMG);
            this.panelVisaIMG.Controls.Add(this.dateTimeVisaExpiryDate);
            this.panelVisaIMG.Controls.Add(this.dateTimeVisaIssueDate);
            this.panelVisaIMG.Controls.Add(this.mmLabel87);
            this.panelVisaIMG.Controls.Add(this.textBoxVisaIssuePlaceIMG);
            this.panelVisaIMG.Controls.Add(this.mmLabel86);
            this.panelVisaIMG.Controls.Add(this.textBoxVisaNumber);
            this.panelVisaIMG.Controls.Add(this.mmLabel85);
            this.panelVisaIMG.Controls.Add(this.mmLabel84);
            this.panelVisaIMG.Controls.Add(this.textBoxUIDNumberIMG);
            this.panelVisaIMG.Controls.Add(this.mmLabel83);
            this.panelVisaIMG.Controls.Add(this.dateTimeVisaPostedOn);
            this.panelVisaIMG.Controls.Add(this.mmLabel26);
            this.panelVisaIMG.Controls.Add(this.dateTimeApprovedOn);
            this.panelVisaIMG.Controls.Add(this.mmLabel28);
            this.panelVisaIMG.Controls.Add(this.mmLabel32);
            this.panelVisaIMG.Enabled = false;
            this.panelVisaIMG.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelVisaIMG.Location = new System.Drawing.Point(13, 317);
            this.panelVisaIMG.Name = "panelVisaIMG";
            this.panelVisaIMG.Size = new System.Drawing.Size(946, 276);
            this.panelVisaIMG.TabIndex = 102;
            this.panelVisaIMG.Text = "Visa Process (IMG)";
            // 
            // mmLabel77
            // 
            this.mmLabel77.AutoSize = true;
            this.mmLabel77.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel77.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel77.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel77.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel77.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel77.IsFieldHeader = false;
            this.mmLabel77.IsRequired = false;
            this.mmLabel77.Location = new System.Drawing.Point(10, 242);
            this.mmLabel77.Name = "mmLabel77";
            this.mmLabel77.PenWidth = 1F;
            this.mmLabel77.ShowBorder = false;
            this.mmLabel77.Size = new System.Drawing.Size(188, 17);
            this.mmLabel77.TabIndex = 102;
            this.mmLabel77.Text = "Visa Copy Sent to Agent On :";
            // 
            // dateTimeVisaCopyToAgentOn
            // 
            this.dateTimeVisaCopyToAgentOn.Checked = false;
            this.dateTimeVisaCopyToAgentOn.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeVisaCopyToAgentOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeVisaCopyToAgentOn.Location = new System.Drawing.Point(190, 241);
            this.dateTimeVisaCopyToAgentOn.Name = "dateTimeVisaCopyToAgentOn";
            this.dateTimeVisaCopyToAgentOn.ShowCheckBox = true;
            this.dateTimeVisaCopyToAgentOn.Size = new System.Drawing.Size(148, 23);
            this.dateTimeVisaCopyToAgentOn.TabIndex = 20;
            this.dateTimeVisaCopyToAgentOn.Value = new System.DateTime(((long)(0)));
            // 
            // comboBoxVisaAppliedThroughIMG
            // 
            this.comboBoxVisaAppliedThroughIMG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVisaAppliedThroughIMG.FormattingEnabled = true;
            this.comboBoxVisaAppliedThroughIMG.Items.AddRange(new object[] {
            "Online",
            "Manual"});
            this.comboBoxVisaAppliedThroughIMG.Location = new System.Drawing.Point(190, 37);
            this.comboBoxVisaAppliedThroughIMG.Name = "comboBoxVisaAppliedThroughIMG";
            this.comboBoxVisaAppliedThroughIMG.Size = new System.Drawing.Size(148, 25);
            this.comboBoxVisaAppliedThroughIMG.TabIndex = 12;
            // 
            // dateTimeVisaExpiryDate
            // 
            this.dateTimeVisaExpiryDate.Checked = false;
            this.dateTimeVisaExpiryDate.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeVisaExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeVisaExpiryDate.Location = new System.Drawing.Point(190, 190);
            this.dateTimeVisaExpiryDate.Name = "dateTimeVisaExpiryDate";
            this.dateTimeVisaExpiryDate.ShowCheckBox = true;
            this.dateTimeVisaExpiryDate.Size = new System.Drawing.Size(148, 23);
            this.dateTimeVisaExpiryDate.TabIndex = 18;
            this.dateTimeVisaExpiryDate.Value = new System.DateTime(((long)(0)));
            // 
            // dateTimeVisaIssueDate
            // 
            this.dateTimeVisaIssueDate.Checked = false;
            this.dateTimeVisaIssueDate.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeVisaIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeVisaIssueDate.Location = new System.Drawing.Point(190, 165);
            this.dateTimeVisaIssueDate.Name = "dateTimeVisaIssueDate";
            this.dateTimeVisaIssueDate.ShowCheckBox = true;
            this.dateTimeVisaIssueDate.Size = new System.Drawing.Size(148, 23);
            this.dateTimeVisaIssueDate.TabIndex = 17;
            this.dateTimeVisaIssueDate.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel87
            // 
            this.mmLabel87.AutoSize = true;
            this.mmLabel87.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel87.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel87.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel87.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel87.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel87.IsFieldHeader = false;
            this.mmLabel87.IsRequired = false;
            this.mmLabel87.Location = new System.Drawing.Point(8, 173);
            this.mmLabel87.Name = "mmLabel87";
            this.mmLabel87.PenWidth = 1F;
            this.mmLabel87.ShowBorder = false;
            this.mmLabel87.Size = new System.Drawing.Size(108, 17);
            this.mmLabel87.TabIndex = 107;
            this.mmLabel87.Text = "Visa Issue Date :";
            // 
            // textBoxVisaIssuePlaceIMG
            // 
            this.textBoxVisaIssuePlaceIMG.AutoCompleteCustomSource.AddRange(new string[] {
            "A+",
            "A-",
            "B+",
            "B-",
            "AB+",
            "AB-",
            "O+",
            "O-"});
            this.textBoxVisaIssuePlaceIMG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxVisaIssuePlaceIMG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxVisaIssuePlaceIMG.BackColor = System.Drawing.Color.White;
            this.textBoxVisaIssuePlaceIMG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxVisaIssuePlaceIMG.CustomReportFieldName = "";
            this.textBoxVisaIssuePlaceIMG.CustomReportKey = "";
            this.textBoxVisaIssuePlaceIMG.CustomReportValueType = ((byte)(1));
            this.textBoxVisaIssuePlaceIMG.IsComboTextBox = false;
            this.textBoxVisaIssuePlaceIMG.IsModified = false;
            this.textBoxVisaIssuePlaceIMG.Location = new System.Drawing.Point(190, 114);
            this.textBoxVisaIssuePlaceIMG.MaxLength = 30;
            this.textBoxVisaIssuePlaceIMG.Name = "textBoxVisaIssuePlaceIMG";
            this.textBoxVisaIssuePlaceIMG.Size = new System.Drawing.Size(496, 23);
            this.textBoxVisaIssuePlaceIMG.TabIndex = 15;
            // 
            // mmLabel86
            // 
            this.mmLabel86.AutoSize = true;
            this.mmLabel86.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel86.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel86.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel86.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel86.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel86.IsFieldHeader = false;
            this.mmLabel86.IsRequired = false;
            this.mmLabel86.Location = new System.Drawing.Point(10, 219);
            this.mmLabel86.Name = "mmLabel86";
            this.mmLabel86.PenWidth = 1F;
            this.mmLabel86.ShowBorder = false;
            this.mmLabel86.Size = new System.Drawing.Size(61, 17);
            this.mmLabel86.TabIndex = 106;
            this.mmLabel86.Text = "UID No :";
            // 
            // textBoxVisaNumber
            // 
            this.textBoxVisaNumber.AutoCompleteCustomSource.AddRange(new string[] {
            "A+",
            "A-",
            "B+",
            "B-",
            "AB+",
            "AB-",
            "O+",
            "O-"});
            this.textBoxVisaNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxVisaNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxVisaNumber.BackColor = System.Drawing.Color.White;
            this.textBoxVisaNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxVisaNumber.CustomReportFieldName = "";
            this.textBoxVisaNumber.CustomReportKey = "";
            this.textBoxVisaNumber.CustomReportValueType = ((byte)(1));
            this.textBoxVisaNumber.IsComboTextBox = false;
            this.textBoxVisaNumber.IsModified = false;
            this.textBoxVisaNumber.Location = new System.Drawing.Point(190, 140);
            this.textBoxVisaNumber.MaxLength = 30;
            this.textBoxVisaNumber.Name = "textBoxVisaNumber";
            this.textBoxVisaNumber.Size = new System.Drawing.Size(246, 23);
            this.textBoxVisaNumber.TabIndex = 16;
            // 
            // mmLabel85
            // 
            this.mmLabel85.AutoSize = true;
            this.mmLabel85.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel85.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel85.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel85.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel85.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel85.IsFieldHeader = false;
            this.mmLabel85.IsRequired = false;
            this.mmLabel85.Location = new System.Drawing.Point(10, 148);
            this.mmLabel85.Name = "mmLabel85";
            this.mmLabel85.PenWidth = 1F;
            this.mmLabel85.ShowBorder = false;
            this.mmLabel85.Size = new System.Drawing.Size(61, 17);
            this.mmLabel85.TabIndex = 104;
            this.mmLabel85.Text = "Visa No :";
            // 
            // mmLabel84
            // 
            this.mmLabel84.AutoSize = true;
            this.mmLabel84.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel84.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel84.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel84.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel84.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel84.IsFieldHeader = false;
            this.mmLabel84.IsRequired = false;
            this.mmLabel84.Location = new System.Drawing.Point(10, 196);
            this.mmLabel84.Name = "mmLabel84";
            this.mmLabel84.PenWidth = 1F;
            this.mmLabel84.ShowBorder = false;
            this.mmLabel84.Size = new System.Drawing.Size(116, 17);
            this.mmLabel84.TabIndex = 103;
            this.mmLabel84.Text = "Visa Expiry Date :";
            // 
            // textBoxUIDNumberIMG
            // 
            this.textBoxUIDNumberIMG.AutoCompleteCustomSource.AddRange(new string[] {
            "A+",
            "A-",
            "B+",
            "B-",
            "AB+",
            "AB-",
            "O+",
            "O-"});
            this.textBoxUIDNumberIMG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxUIDNumberIMG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxUIDNumberIMG.BackColor = System.Drawing.Color.White;
            this.textBoxUIDNumberIMG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxUIDNumberIMG.CustomReportFieldName = "";
            this.textBoxUIDNumberIMG.CustomReportKey = "";
            this.textBoxUIDNumberIMG.CustomReportValueType = ((byte)(1));
            this.textBoxUIDNumberIMG.IsComboTextBox = false;
            this.textBoxUIDNumberIMG.IsModified = false;
            this.textBoxUIDNumberIMG.Location = new System.Drawing.Point(190, 216);
            this.textBoxUIDNumberIMG.MaxLength = 30;
            this.textBoxUIDNumberIMG.Name = "textBoxUIDNumberIMG";
            this.textBoxUIDNumberIMG.Size = new System.Drawing.Size(246, 23);
            this.textBoxUIDNumberIMG.TabIndex = 19;
            // 
            // mmLabel83
            // 
            this.mmLabel83.AutoSize = true;
            this.mmLabel83.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel83.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel83.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel83.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel83.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel83.IsFieldHeader = false;
            this.mmLabel83.IsRequired = false;
            this.mmLabel83.Location = new System.Drawing.Point(8, 122);
            this.mmLabel83.Name = "mmLabel83";
            this.mmLabel83.PenWidth = 1F;
            this.mmLabel83.ShowBorder = false;
            this.mmLabel83.Size = new System.Drawing.Size(110, 17);
            this.mmLabel83.TabIndex = 102;
            this.mmLabel83.Text = "Visa Issue Place :";
            // 
            // dateTimeVisaPostedOn
            // 
            this.dateTimeVisaPostedOn.Checked = false;
            this.dateTimeVisaPostedOn.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeVisaPostedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeVisaPostedOn.Location = new System.Drawing.Point(190, 63);
            this.dateTimeVisaPostedOn.Name = "dateTimeVisaPostedOn";
            this.dateTimeVisaPostedOn.ShowCheckBox = true;
            this.dateTimeVisaPostedOn.Size = new System.Drawing.Size(148, 23);
            this.dateTimeVisaPostedOn.TabIndex = 13;
            this.dateTimeVisaPostedOn.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel26
            // 
            this.mmLabel26.AutoSize = true;
            this.mmLabel26.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel26.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel26.IsFieldHeader = false;
            this.mmLabel26.IsRequired = false;
            this.mmLabel26.Location = new System.Drawing.Point(8, 67);
            this.mmLabel26.Name = "mmLabel26";
            this.mmLabel26.PenWidth = 1F;
            this.mmLabel26.ShowBorder = false;
            this.mmLabel26.Size = new System.Drawing.Size(108, 17);
            this.mmLabel26.TabIndex = 75;
            this.mmLabel26.Text = "Visa Posted On :";
            // 
            // dateTimeApprovedOn
            // 
            this.dateTimeApprovedOn.Checked = false;
            this.dateTimeApprovedOn.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeApprovedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeApprovedOn.Location = new System.Drawing.Point(190, 89);
            this.dateTimeApprovedOn.Name = "dateTimeApprovedOn";
            this.dateTimeApprovedOn.ShowCheckBox = true;
            this.dateTimeApprovedOn.Size = new System.Drawing.Size(148, 23);
            this.dateTimeApprovedOn.TabIndex = 14;
            this.dateTimeApprovedOn.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel28
            // 
            this.mmLabel28.AutoSize = true;
            this.mmLabel28.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel28.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel28.IsFieldHeader = false;
            this.mmLabel28.IsRequired = false;
            this.mmLabel28.Location = new System.Drawing.Point(8, 96);
            this.mmLabel28.Name = "mmLabel28";
            this.mmLabel28.PenWidth = 1F;
            this.mmLabel28.ShowBorder = false;
            this.mmLabel28.Size = new System.Drawing.Size(126, 17);
            this.mmLabel28.TabIndex = 71;
            this.mmLabel28.Text = "Visa Approved On :";
            // 
            // mmLabel32
            // 
            this.mmLabel32.AutoSize = true;
            this.mmLabel32.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel32.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel32.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel32.IsFieldHeader = false;
            this.mmLabel32.IsRequired = false;
            this.mmLabel32.Location = new System.Drawing.Point(8, 40);
            this.mmLabel32.Name = "mmLabel32";
            this.mmLabel32.PenWidth = 1F;
            this.mmLabel32.ShowBorder = false;
            this.mmLabel32.Size = new System.Drawing.Size(144, 17);
            this.mmLabel32.TabIndex = 10;
            this.mmLabel32.Text = "Visa Applied Through :";
            // 
            // panelVisaMOL
            // 
            this.panelVisaMOL.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            this.panelVisaMOL.Controls.Add(this.textBoxVisaDesignationName);
            this.panelVisaMOL.Controls.Add(this.comboBoxPositionVisa);
            this.panelVisaMOL.Controls.Add(this.mmLabel67);
            this.panelVisaMOL.Controls.Add(this.textBoxSponsorName);
            this.panelVisaMOL.Controls.Add(this.comboBoxSponsor);
            this.panelVisaMOL.Controls.Add(this.mmLabel70);
            this.panelVisaMOL.Controls.Add(this.comboBoxBGTypeMOL);
            this.panelVisaMOL.Controls.Add(this.mmLabel82);
            this.panelVisaMOL.Controls.Add(this.dateTimeApprovalFeePaidOnMOL);
            this.panelVisaMOL.Controls.Add(this.textBoxTempWPNo);
            this.panelVisaMOL.Controls.Add(this.mmLabel80);
            this.panelVisaMOL.Controls.Add(this.dateTimeApprovalValidTillMOL);
            this.panelVisaMOL.Controls.Add(this.mmLabel79);
            this.panelVisaMOL.Controls.Add(this.dateTimeBGPaidOnMOL);
            this.panelVisaMOL.Controls.Add(this.mmLabel25);
            this.panelVisaMOL.Controls.Add(this.mmLabel24);
            this.panelVisaMOL.Controls.Add(this.dateTimeApprovalDateMOL);
            this.panelVisaMOL.Controls.Add(this.mmLabel22);
            this.panelVisaMOL.Controls.Add(this.textBoxMOLMBNo);
            this.panelVisaMOL.Controls.Add(this.mmLabel7);
            this.panelVisaMOL.Controls.Add(this.dateTimeApplTypingDateMOL);
            this.panelVisaMOL.Controls.Add(this.mmLabel30);
            this.panelVisaMOL.Enabled = false;
            this.panelVisaMOL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelVisaMOL.Location = new System.Drawing.Point(13, 15);
            this.panelVisaMOL.Name = "panelVisaMOL";
            this.panelVisaMOL.Size = new System.Drawing.Size(946, 288);
            this.panelVisaMOL.TabIndex = 101;
            this.panelVisaMOL.Text = "Visa Process (MOL)";
            // 
            // textBoxVisaDesignationName
            // 
            this.textBoxVisaDesignationName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxVisaDesignationName.Location = new System.Drawing.Point(340, 110);
            this.textBoxVisaDesignationName.MaxLength = 64;
            this.textBoxVisaDesignationName.Name = "textBoxVisaDesignationName";
            this.textBoxVisaDesignationName.ReadOnly = true;
            this.textBoxVisaDesignationName.Size = new System.Drawing.Size(344, 23);
            this.textBoxVisaDesignationName.TabIndex = 5;
            this.textBoxVisaDesignationName.TabStop = false;
            // 
            // comboBoxPositionVisa
            // 
            this.comboBoxPositionVisa.Assigned = false;
            this.comboBoxPositionVisa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxPositionVisa.CustomReportFieldName = "";
            this.comboBoxPositionVisa.CustomReportKey = "";
            this.comboBoxPositionVisa.CustomReportValueType = ((byte)(1));
            this.comboBoxPositionVisa.DescriptionTextBox = this.textBoxVisaDesignationName;
            appearance165.BackColor = System.Drawing.SystemColors.Window;
            appearance165.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxPositionVisa.DisplayLayout.Appearance = appearance165;
            this.comboBoxPositionVisa.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxPositionVisa.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance166.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance166.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance166.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance166.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPositionVisa.DisplayLayout.GroupByBox.Appearance = appearance166;
            appearance167.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPositionVisa.DisplayLayout.GroupByBox.BandLabelAppearance = appearance167;
            this.comboBoxPositionVisa.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance168.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance168.BackColor2 = System.Drawing.SystemColors.Control;
            appearance168.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance168.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxPositionVisa.DisplayLayout.GroupByBox.PromptAppearance = appearance168;
            this.comboBoxPositionVisa.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxPositionVisa.DisplayLayout.MaxRowScrollRegions = 1;
            appearance169.BackColor = System.Drawing.SystemColors.Window;
            appearance169.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxPositionVisa.DisplayLayout.Override.ActiveCellAppearance = appearance169;
            appearance170.BackColor = System.Drawing.SystemColors.Highlight;
            appearance170.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxPositionVisa.DisplayLayout.Override.ActiveRowAppearance = appearance170;
            this.comboBoxPositionVisa.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxPositionVisa.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance171.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxPositionVisa.DisplayLayout.Override.CardAreaAppearance = appearance171;
            appearance172.BorderColor = System.Drawing.Color.Silver;
            appearance172.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxPositionVisa.DisplayLayout.Override.CellAppearance = appearance172;
            this.comboBoxPositionVisa.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxPositionVisa.DisplayLayout.Override.CellPadding = 0;
            appearance173.BackColor = System.Drawing.SystemColors.Control;
            appearance173.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance173.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance173.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance173.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxPositionVisa.DisplayLayout.Override.GroupByRowAppearance = appearance173;
            appearance174.TextHAlignAsString = "Left";
            this.comboBoxPositionVisa.DisplayLayout.Override.HeaderAppearance = appearance174;
            this.comboBoxPositionVisa.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxPositionVisa.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance175.BackColor = System.Drawing.SystemColors.Window;
            appearance175.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxPositionVisa.DisplayLayout.Override.RowAppearance = appearance175;
            this.comboBoxPositionVisa.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance176.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxPositionVisa.DisplayLayout.Override.TemplateAddRowAppearance = appearance176;
            this.comboBoxPositionVisa.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxPositionVisa.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxPositionVisa.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxPositionVisa.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxPositionVisa.Editable = true;
            this.comboBoxPositionVisa.FilterString = "";
            this.comboBoxPositionVisa.HasAllAccount = false;
            this.comboBoxPositionVisa.HasCustom = false;
            this.comboBoxPositionVisa.IsDataLoaded = false;
            this.comboBoxPositionVisa.Location = new System.Drawing.Point(190, 110);
            this.comboBoxPositionVisa.MaxDropDownItems = 12;
            this.comboBoxPositionVisa.Name = "comboBoxPositionVisa";
            this.comboBoxPositionVisa.ShowInactiveItems = false;
            this.comboBoxPositionVisa.ShowQuickAdd = true;
            this.comboBoxPositionVisa.Size = new System.Drawing.Size(148, 24);
            this.comboBoxPositionVisa.TabIndex = 4;
            this.comboBoxPositionVisa.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // mmLabel67
            // 
            this.mmLabel67.AutoSize = true;
            this.mmLabel67.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel67.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel67.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel67.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel67.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel67.IsFieldHeader = false;
            this.mmLabel67.IsRequired = false;
            this.mmLabel67.Location = new System.Drawing.Point(8, 115);
            this.mmLabel67.Name = "mmLabel67";
            this.mmLabel67.PenWidth = 1F;
            this.mmLabel67.ShowBorder = false;
            this.mmLabel67.Size = new System.Drawing.Size(115, 17);
            this.mmLabel67.TabIndex = 107;
            this.mmLabel67.Text = "Visa Designation :";
            // 
            // textBoxSponsorName
            // 
            this.textBoxSponsorName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxSponsorName.Location = new System.Drawing.Point(340, 84);
            this.textBoxSponsorName.MaxLength = 64;
            this.textBoxSponsorName.Name = "textBoxSponsorName";
            this.textBoxSponsorName.ReadOnly = true;
            this.textBoxSponsorName.Size = new System.Drawing.Size(344, 23);
            this.textBoxSponsorName.TabIndex = 3;
            this.textBoxSponsorName.TabStop = false;
            // 
            // comboBoxSponsor
            // 
            this.comboBoxSponsor.Assigned = false;
            this.comboBoxSponsor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.comboBoxSponsor.CustomReportFieldName = "";
            this.comboBoxSponsor.CustomReportKey = "";
            this.comboBoxSponsor.CustomReportValueType = ((byte)(1));
            this.comboBoxSponsor.DescriptionTextBox = this.textBoxSponsorName;
            appearance177.BackColor = System.Drawing.SystemColors.Window;
            appearance177.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.comboBoxSponsor.DisplayLayout.Appearance = appearance177;
            this.comboBoxSponsor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.comboBoxSponsor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance178.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance178.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance178.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance178.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSponsor.DisplayLayout.GroupByBox.Appearance = appearance178;
            appearance179.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSponsor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance179;
            this.comboBoxSponsor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance180.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance180.BackColor2 = System.Drawing.SystemColors.Control;
            appearance180.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance180.ForeColor = System.Drawing.SystemColors.GrayText;
            this.comboBoxSponsor.DisplayLayout.GroupByBox.PromptAppearance = appearance180;
            this.comboBoxSponsor.DisplayLayout.MaxColScrollRegions = 1;
            this.comboBoxSponsor.DisplayLayout.MaxRowScrollRegions = 1;
            appearance181.BackColor = System.Drawing.SystemColors.Window;
            appearance181.ForeColor = System.Drawing.SystemColors.ControlText;
            this.comboBoxSponsor.DisplayLayout.Override.ActiveCellAppearance = appearance181;
            appearance182.BackColor = System.Drawing.SystemColors.Highlight;
            appearance182.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBoxSponsor.DisplayLayout.Override.ActiveRowAppearance = appearance182;
            this.comboBoxSponsor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.comboBoxSponsor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance183.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxSponsor.DisplayLayout.Override.CardAreaAppearance = appearance183;
            appearance184.BorderColor = System.Drawing.Color.Silver;
            appearance184.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.comboBoxSponsor.DisplayLayout.Override.CellAppearance = appearance184;
            this.comboBoxSponsor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.comboBoxSponsor.DisplayLayout.Override.CellPadding = 0;
            appearance185.BackColor = System.Drawing.SystemColors.Control;
            appearance185.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance185.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance185.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance185.BorderColor = System.Drawing.SystemColors.Window;
            this.comboBoxSponsor.DisplayLayout.Override.GroupByRowAppearance = appearance185;
            appearance186.TextHAlignAsString = "Left";
            this.comboBoxSponsor.DisplayLayout.Override.HeaderAppearance = appearance186;
            this.comboBoxSponsor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.comboBoxSponsor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance187.BackColor = System.Drawing.SystemColors.Window;
            appearance187.BorderColor = System.Drawing.Color.Silver;
            this.comboBoxSponsor.DisplayLayout.Override.RowAppearance = appearance187;
            this.comboBoxSponsor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance188.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxSponsor.DisplayLayout.Override.TemplateAddRowAppearance = appearance188;
            this.comboBoxSponsor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.comboBoxSponsor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.comboBoxSponsor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.comboBoxSponsor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
            this.comboBoxSponsor.Editable = true;
            this.comboBoxSponsor.FilterString = "";
            this.comboBoxSponsor.HasAllAccount = false;
            this.comboBoxSponsor.HasCustom = false;
            this.comboBoxSponsor.IsDataLoaded = false;
            this.comboBoxSponsor.Location = new System.Drawing.Point(190, 84);
            this.comboBoxSponsor.MaxDropDownItems = 12;
            this.comboBoxSponsor.Name = "comboBoxSponsor";
            this.comboBoxSponsor.ShowInactiveItems = false;
            this.comboBoxSponsor.ShowQuickAdd = true;
            this.comboBoxSponsor.Size = new System.Drawing.Size(148, 24);
            this.comboBoxSponsor.TabIndex = 2;
            this.comboBoxSponsor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // mmLabel70
            // 
            this.mmLabel70.AutoSize = true;
            this.mmLabel70.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel70.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel70.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel70.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel70.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel70.IsFieldHeader = false;
            this.mmLabel70.IsRequired = false;
            this.mmLabel70.Location = new System.Drawing.Point(10, 90);
            this.mmLabel70.Name = "mmLabel70";
            this.mmLabel70.PenWidth = 1F;
            this.mmLabel70.ShowBorder = false;
            this.mmLabel70.Size = new System.Drawing.Size(107, 17);
            this.mmLabel70.TabIndex = 86;
            this.mmLabel70.Text = "Sponsor Name :";
            // 
            // comboBoxBGTypeMOL
            // 
            this.comboBoxBGTypeMOL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBGTypeMOL.FormattingEnabled = true;
            this.comboBoxBGTypeMOL.Items.AddRange(new object[] {
            "Fixed",
            "Individual"});
            this.comboBoxBGTypeMOL.Location = new System.Drawing.Point(190, 263);
            this.comboBoxBGTypeMOL.Name = "comboBoxBGTypeMOL";
            this.comboBoxBGTypeMOL.Size = new System.Drawing.Size(148, 25);
            this.comboBoxBGTypeMOL.TabIndex = 11;
            // 
            // mmLabel82
            // 
            this.mmLabel82.AutoSize = true;
            this.mmLabel82.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel82.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel82.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel82.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel82.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel82.IsFieldHeader = false;
            this.mmLabel82.IsRequired = false;
            this.mmLabel82.Location = new System.Drawing.Point(10, 267);
            this.mmLabel82.Name = "mmLabel82";
            this.mmLabel82.PenWidth = 1F;
            this.mmLabel82.ShowBorder = false;
            this.mmLabel82.Size = new System.Drawing.Size(74, 17);
            this.mmLabel82.TabIndex = 84;
            this.mmLabel82.Text = "B/G Type :";
            // 
            // dateTimeApprovalFeePaidOnMOL
            // 
            this.dateTimeApprovalFeePaidOnMOL.Checked = false;
            this.dateTimeApprovalFeePaidOnMOL.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeApprovalFeePaidOnMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeApprovalFeePaidOnMOL.Location = new System.Drawing.Point(190, 211);
            this.dateTimeApprovalFeePaidOnMOL.Name = "dateTimeApprovalFeePaidOnMOL";
            this.dateTimeApprovalFeePaidOnMOL.ShowCheckBox = true;
            this.dateTimeApprovalFeePaidOnMOL.Size = new System.Drawing.Size(148, 23);
            this.dateTimeApprovalFeePaidOnMOL.TabIndex = 9;
            this.dateTimeApprovalFeePaidOnMOL.Value = new System.DateTime(((long)(0)));
            // 
            // textBoxTempWPNo
            // 
            this.textBoxTempWPNo.BackColor = System.Drawing.Color.White;
            this.textBoxTempWPNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxTempWPNo.CustomReportFieldName = "";
            this.textBoxTempWPNo.CustomReportKey = "";
            this.textBoxTempWPNo.CustomReportValueType = ((byte)(1));
            this.textBoxTempWPNo.IsComboTextBox = false;
            this.textBoxTempWPNo.IsModified = false;
            this.textBoxTempWPNo.Location = new System.Drawing.Point(190, 186);
            this.textBoxTempWPNo.MaxLength = 30;
            this.textBoxTempWPNo.Name = "textBoxTempWPNo";
            this.textBoxTempWPNo.Size = new System.Drawing.Size(148, 23);
            this.textBoxTempWPNo.TabIndex = 8;
            // 
            // mmLabel80
            // 
            this.mmLabel80.AutoSize = true;
            this.mmLabel80.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel80.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel80.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel80.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel80.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel80.IsFieldHeader = false;
            this.mmLabel80.IsRequired = false;
            this.mmLabel80.Location = new System.Drawing.Point(10, 194);
            this.mmLabel80.Name = "mmLabel80";
            this.mmLabel80.PenWidth = 1F;
            this.mmLabel80.ShowBorder = false;
            this.mmLabel80.Size = new System.Drawing.Size(99, 17);
            this.mmLabel80.TabIndex = 80;
            this.mmLabel80.Text = "Temp WP No :";
            // 
            // dateTimeApprovalValidTillMOL
            // 
            this.dateTimeApprovalValidTillMOL.Checked = false;
            this.dateTimeApprovalValidTillMOL.CustomFormat = "dd-MMM-yyyy";
            this.dateTimeApprovalValidTillMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeApprovalValidTillMOL.Location = new System.Drawing.Point(190, 160);
            this.dateTimeApprovalValidTillMOL.Name = "dateTimeApprovalValidTillMOL";
            this.dateTimeApprovalValidTillMOL.ShowCheckBox = true;
            this.dateTimeApprovalValidTillMOL.Size = new System.Drawing.Size(148, 23);
            this.dateTimeApprovalValidTillMOL.TabIndex = 7;
            this.dateTimeApprovalValidTillMOL.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel79
            // 
            this.mmLabel79.AutoSize = true;
            this.mmLabel79.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel79.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel79.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel79.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel79.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel79.IsFieldHeader = false;
            this.mmLabel79.IsRequired = false;
            this.mmLabel79.Location = new System.Drawing.Point(10, 167);
            this.mmLabel79.Name = "mmLabel79";
            this.mmLabel79.PenWidth = 1F;
            this.mmLabel79.ShowBorder = false;
            this.mmLabel79.Size = new System.Drawing.Size(120, 17);
            this.mmLabel79.TabIndex = 78;
            this.mmLabel79.Text = "Approval Valid Till :";
            // 
            // dateTimeBGPaidOnMOL
            // 
            this.dateTimeBGPaidOnMOL.Checked = false;
            this.dateTimeBGPaidOnMOL.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeBGPaidOnMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeBGPaidOnMOL.Location = new System.Drawing.Point(190, 237);
            this.dateTimeBGPaidOnMOL.Name = "dateTimeBGPaidOnMOL";
            this.dateTimeBGPaidOnMOL.ShowCheckBox = true;
            this.dateTimeBGPaidOnMOL.Size = new System.Drawing.Size(148, 23);
            this.dateTimeBGPaidOnMOL.TabIndex = 10;
            this.dateTimeBGPaidOnMOL.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel25
            // 
            this.mmLabel25.AutoSize = true;
            this.mmLabel25.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel25.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel25.IsFieldHeader = false;
            this.mmLabel25.IsRequired = false;
            this.mmLabel25.Location = new System.Drawing.Point(10, 243);
            this.mmLabel25.Name = "mmLabel25";
            this.mmLabel25.PenWidth = 1F;
            this.mmLabel25.ShowBorder = false;
            this.mmLabel25.Size = new System.Drawing.Size(90, 17);
            this.mmLabel25.TabIndex = 75;
            this.mmLabel25.Text = "B/G Paid On :";
            // 
            // mmLabel24
            // 
            this.mmLabel24.AutoSize = true;
            this.mmLabel24.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel24.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel24.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel24.IsFieldHeader = false;
            this.mmLabel24.IsRequired = false;
            this.mmLabel24.Location = new System.Drawing.Point(10, 218);
            this.mmLabel24.Name = "mmLabel24";
            this.mmLabel24.PenWidth = 1F;
            this.mmLabel24.ShowBorder = false;
            this.mmLabel24.Size = new System.Drawing.Size(147, 17);
            this.mmLabel24.TabIndex = 73;
            this.mmLabel24.Text = "Approval Fee Paid On :";
            // 
            // dateTimeApprovalDateMOL
            // 
            this.dateTimeApprovalDateMOL.Checked = false;
            this.dateTimeApprovalDateMOL.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeApprovalDateMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeApprovalDateMOL.Location = new System.Drawing.Point(190, 135);
            this.dateTimeApprovalDateMOL.Name = "dateTimeApprovalDateMOL";
            this.dateTimeApprovalDateMOL.ShowCheckBox = true;
            this.dateTimeApprovalDateMOL.Size = new System.Drawing.Size(148, 23);
            this.dateTimeApprovalDateMOL.TabIndex = 6;
            this.dateTimeApprovalDateMOL.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel22
            // 
            this.mmLabel22.AutoSize = true;
            this.mmLabel22.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel22.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel22.IsFieldHeader = false;
            this.mmLabel22.IsRequired = false;
            this.mmLabel22.Location = new System.Drawing.Point(10, 141);
            this.mmLabel22.Name = "mmLabel22";
            this.mmLabel22.PenWidth = 1F;
            this.mmLabel22.ShowBorder = false;
            this.mmLabel22.Size = new System.Drawing.Size(135, 17);
            this.mmLabel22.TabIndex = 71;
            this.mmLabel22.Text = "Approval Date MOL :";
            // 
            // textBoxMOLMBNo
            // 
            this.textBoxMOLMBNo.BackColor = System.Drawing.Color.White;
            this.textBoxMOLMBNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxMOLMBNo.CustomReportFieldName = "";
            this.textBoxMOLMBNo.CustomReportKey = "";
            this.textBoxMOLMBNo.CustomReportValueType = ((byte)(1));
            this.textBoxMOLMBNo.IsComboTextBox = false;
            this.textBoxMOLMBNo.IsModified = false;
            this.textBoxMOLMBNo.Location = new System.Drawing.Point(190, 58);
            this.textBoxMOLMBNo.MaxLength = 30;
            this.textBoxMOLMBNo.Name = "textBoxMOLMBNo";
            this.textBoxMOLMBNo.Size = new System.Drawing.Size(246, 23);
            this.textBoxMOLMBNo.TabIndex = 1;
            // 
            // mmLabel7
            // 
            this.mmLabel7.AutoSize = true;
            this.mmLabel7.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel7.IsFieldHeader = false;
            this.mmLabel7.IsRequired = false;
            this.mmLabel7.Location = new System.Drawing.Point(10, 66);
            this.mmLabel7.Name = "mmLabel7";
            this.mmLabel7.PenWidth = 1F;
            this.mmLabel7.ShowBorder = false;
            this.mmLabel7.Size = new System.Drawing.Size(116, 17);
            this.mmLabel7.TabIndex = 69;
            this.mmLabel7.Text = "MB Ref No - Visa :";
            // 
            // dateTimeApplTypingDateMOL
            // 
            this.dateTimeApplTypingDateMOL.Checked = false;
            this.dateTimeApplTypingDateMOL.CustomFormat = "dd-MMM-yyyy";
            this.dateTimeApplTypingDateMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeApplTypingDateMOL.Location = new System.Drawing.Point(190, 33);
            this.dateTimeApplTypingDateMOL.Name = "dateTimeApplTypingDateMOL";
            this.dateTimeApplTypingDateMOL.ShowCheckBox = true;
            this.dateTimeApplTypingDateMOL.Size = new System.Drawing.Size(148, 23);
            this.dateTimeApplTypingDateMOL.TabIndex = 0;
            this.dateTimeApplTypingDateMOL.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel30
            // 
            this.mmLabel30.AutoSize = true;
            this.mmLabel30.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel30.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel30.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel30.IsFieldHeader = false;
            this.mmLabel30.IsRequired = false;
            this.mmLabel30.Location = new System.Drawing.Point(10, 38);
            this.mmLabel30.Name = "mmLabel30";
            this.mmLabel30.PenWidth = 1F;
            this.mmLabel30.ShowBorder = false;
            this.mmLabel30.Size = new System.Drawing.Size(126, 17);
            this.mmLabel30.TabIndex = 10;
            this.mmLabel30.Text = "Appl. Typing Date :";
            // 
            // ultraTabPageControl2
            // 
            this.ultraTabPageControl2.Controls.Add(this.panelMedicalEmirates);
            this.ultraTabPageControl2.Location = new System.Drawing.Point(-12000, -11538);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            this.ultraTabPageControl2.Size = new System.Drawing.Size(974, 592);
            // 
            // panelMedicalEmirates
            // 
            this.panelMedicalEmirates.Controls.Add(this.panelEmirates);
            this.panelMedicalEmirates.Controls.Add(this.panelMedicalDetail);
            this.panelMedicalEmirates.Controls.Add(this.textBoxMedicalNote);
            this.panelMedicalEmirates.Controls.Add(this.mmLabel78);
            this.panelMedicalEmirates.Enabled = false;
            this.panelMedicalEmirates.Location = new System.Drawing.Point(-2, 0);
            this.panelMedicalEmirates.Name = "panelMedicalEmirates";
            this.panelMedicalEmirates.Size = new System.Drawing.Size(976, 610);
            this.panelMedicalEmirates.TabIndex = 0;
            // 
            // panelEmirates
            // 
            this.panelEmirates.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            this.panelEmirates.Controls.Add(this.dateTimeValidityEID);
            this.panelEmirates.Controls.Add(this.dateTimeCollectedOnEID);
            this.panelEmirates.Controls.Add(this.mmLabel42);
            this.panelEmirates.Controls.Add(this.mmLabel43);
            this.panelEmirates.Controls.Add(this.dateTimeAttendedDateEID);
            this.panelEmirates.Controls.Add(this.mmLabel44);
            this.panelEmirates.Controls.Add(this.textBoxNationalID);
            this.panelEmirates.Controls.Add(this.mmLabel45);
            this.panelEmirates.Controls.Add(this.dateTimeApplTypingDateEID);
            this.panelEmirates.Controls.Add(this.mmLabel46);
            this.panelEmirates.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelEmirates.Location = new System.Drawing.Point(14, 207);
            this.panelEmirates.Name = "panelEmirates";
            this.panelEmirates.Size = new System.Drawing.Size(946, 178);
            this.panelEmirates.TabIndex = 68;
            this.panelEmirates.Text = "Emirates ID Details";
            // 
            // dateTimeValidityEID
            // 
            this.dateTimeValidityEID.Checked = false;
            this.dateTimeValidityEID.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeValidityEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeValidityEID.Location = new System.Drawing.Point(164, 141);
            this.dateTimeValidityEID.Name = "dateTimeValidityEID";
            this.dateTimeValidityEID.ShowCheckBox = true;
            this.dateTimeValidityEID.Size = new System.Drawing.Size(149, 23);
            this.dateTimeValidityEID.TabIndex = 9;
            this.dateTimeValidityEID.Value = new System.DateTime(((long)(0)));
            // 
            // dateTimeCollectedOnEID
            // 
            this.dateTimeCollectedOnEID.Checked = false;
            this.dateTimeCollectedOnEID.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeCollectedOnEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeCollectedOnEID.Location = new System.Drawing.Point(164, 90);
            this.dateTimeCollectedOnEID.Name = "dateTimeCollectedOnEID";
            this.dateTimeCollectedOnEID.ShowCheckBox = true;
            this.dateTimeCollectedOnEID.Size = new System.Drawing.Size(149, 23);
            this.dateTimeCollectedOnEID.TabIndex = 7;
            this.dateTimeCollectedOnEID.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel42
            // 
            this.mmLabel42.AutoSize = true;
            this.mmLabel42.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel42.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel42.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel42.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel42.IsFieldHeader = false;
            this.mmLabel42.IsRequired = false;
            this.mmLabel42.Location = new System.Drawing.Point(24, 97);
            this.mmLabel42.Name = "mmLabel42";
            this.mmLabel42.PenWidth = 1F;
            this.mmLabel42.ShowBorder = false;
            this.mmLabel42.Size = new System.Drawing.Size(112, 17);
            this.mmLabel42.TabIndex = 75;
            this.mmLabel42.Text = "ID Collected On :";
            // 
            // mmLabel43
            // 
            this.mmLabel43.AutoSize = true;
            this.mmLabel43.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel43.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel43.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel43.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel43.IsFieldHeader = false;
            this.mmLabel43.IsRequired = false;
            this.mmLabel43.Location = new System.Drawing.Point(24, 147);
            this.mmLabel43.Name = "mmLabel43";
            this.mmLabel43.PenWidth = 1F;
            this.mmLabel43.ShowBorder = false;
            this.mmLabel43.Size = new System.Drawing.Size(77, 17);
            this.mmLabel43.TabIndex = 73;
            this.mmLabel43.Text = "ID Validity :";
            // 
            // dateTimeAttendedDateEID
            // 
            this.dateTimeAttendedDateEID.Checked = false;
            this.dateTimeAttendedDateEID.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeAttendedDateEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeAttendedDateEID.Location = new System.Drawing.Point(164, 65);
            this.dateTimeAttendedDateEID.Name = "dateTimeAttendedDateEID";
            this.dateTimeAttendedDateEID.ShowCheckBox = true;
            this.dateTimeAttendedDateEID.Size = new System.Drawing.Size(149, 23);
            this.dateTimeAttendedDateEID.TabIndex = 6;
            this.dateTimeAttendedDateEID.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel44
            // 
            this.mmLabel44.AutoSize = true;
            this.mmLabel44.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel44.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel44.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel44.IsFieldHeader = false;
            this.mmLabel44.IsRequired = false;
            this.mmLabel44.Location = new System.Drawing.Point(25, 72);
            this.mmLabel44.Name = "mmLabel44";
            this.mmLabel44.PenWidth = 1F;
            this.mmLabel44.ShowBorder = false;
            this.mmLabel44.Size = new System.Drawing.Size(112, 17);
            this.mmLabel44.TabIndex = 71;
            this.mmLabel44.Text = "Attended for ID :";
            // 
            // textBoxNationalID
            // 
            this.textBoxNationalID.BackColor = System.Drawing.Color.White;
            this.textBoxNationalID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxNationalID.CustomReportFieldName = "";
            this.textBoxNationalID.CustomReportKey = "";
            this.textBoxNationalID.CustomReportValueType = ((byte)(1));
            this.textBoxNationalID.IsComboTextBox = false;
            this.textBoxNationalID.IsModified = false;
            this.textBoxNationalID.Location = new System.Drawing.Point(164, 115);
            this.textBoxNationalID.MaxLength = 50;
            this.textBoxNationalID.Name = "textBoxNationalID";
            this.textBoxNationalID.Size = new System.Drawing.Size(239, 23);
            this.textBoxNationalID.TabIndex = 8;
            // 
            // mmLabel45
            // 
            this.mmLabel45.AutoSize = true;
            this.mmLabel45.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel45.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel45.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel45.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel45.IsFieldHeader = false;
            this.mmLabel45.IsRequired = false;
            this.mmLabel45.Location = new System.Drawing.Point(24, 121);
            this.mmLabel45.Name = "mmLabel45";
            this.mmLabel45.PenWidth = 1F;
            this.mmLabel45.ShowBorder = false;
            this.mmLabel45.Size = new System.Drawing.Size(84, 17);
            this.mmLabel45.TabIndex = 69;
            this.mmLabel45.Text = "ID Number :";
            // 
            // dateTimeApplTypingDateEID
            // 
            this.dateTimeApplTypingDateEID.Checked = false;
            this.dateTimeApplTypingDateEID.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeApplTypingDateEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeApplTypingDateEID.Location = new System.Drawing.Point(164, 38);
            this.dateTimeApplTypingDateEID.Name = "dateTimeApplTypingDateEID";
            this.dateTimeApplTypingDateEID.ShowCheckBox = true;
            this.dateTimeApplTypingDateEID.Size = new System.Drawing.Size(149, 23);
            this.dateTimeApplTypingDateEID.TabIndex = 5;
            this.dateTimeApplTypingDateEID.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel46
            // 
            this.mmLabel46.AutoSize = true;
            this.mmLabel46.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel46.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel46.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel46.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel46.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel46.IsFieldHeader = false;
            this.mmLabel46.IsRequired = false;
            this.mmLabel46.Location = new System.Drawing.Point(25, 45);
            this.mmLabel46.Name = "mmLabel46";
            this.mmLabel46.PenWidth = 1F;
            this.mmLabel46.ShowBorder = false;
            this.mmLabel46.Size = new System.Drawing.Size(112, 17);
            this.mmLabel46.TabIndex = 10;
            this.mmLabel46.Text = "Appl. Typed On :";
            // 
            // panelMedicalDetail
            // 
            this.panelMedicalDetail.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            this.panelMedicalDetail.Controls.Add(this.textBoxHealthCardNo);
            this.panelMedicalDetail.Controls.Add(this.mmLabel72);
            this.panelMedicalDetail.Controls.Add(this.comboBoxMedicalResult);
            this.panelMedicalDetail.Controls.Add(this.mmLabel89);
            this.panelMedicalDetail.Controls.Add(this.dateTimeMedicalCollectedOn);
            this.panelMedicalDetail.Controls.Add(this.mmLabel41);
            this.panelMedicalDetail.Controls.Add(this.dateTimeMedicalAttendedOn);
            this.panelMedicalDetail.Controls.Add(this.mmLabel40);
            this.panelMedicalDetail.Controls.Add(this.dateTimeMedicalTypingOn);
            this.panelMedicalDetail.Controls.Add(this.mmLabel35);
            this.panelMedicalDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelMedicalDetail.Location = new System.Drawing.Point(14, 24);
            this.panelMedicalDetail.Name = "panelMedicalDetail";
            this.panelMedicalDetail.Size = new System.Drawing.Size(946, 169);
            this.panelMedicalDetail.TabIndex = 67;
            this.panelMedicalDetail.Text = "Medical Details";
            // 
            // textBoxHealthCardNo
            // 
            this.textBoxHealthCardNo.BackColor = System.Drawing.Color.White;
            this.textBoxHealthCardNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxHealthCardNo.CustomReportFieldName = "";
            this.textBoxHealthCardNo.CustomReportKey = "";
            this.textBoxHealthCardNo.CustomReportValueType = ((byte)(1));
            this.textBoxHealthCardNo.IsComboTextBox = false;
            this.textBoxHealthCardNo.IsModified = false;
            this.textBoxHealthCardNo.Location = new System.Drawing.Point(164, 37);
            this.textBoxHealthCardNo.MaxLength = 30;
            this.textBoxHealthCardNo.Name = "textBoxHealthCardNo";
            this.textBoxHealthCardNo.Size = new System.Drawing.Size(246, 23);
            this.textBoxHealthCardNo.TabIndex = 0;
            // 
            // mmLabel72
            // 
            this.mmLabel72.AutoSize = true;
            this.mmLabel72.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel72.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel72.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel72.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel72.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel72.IsFieldHeader = false;
            this.mmLabel72.IsRequired = false;
            this.mmLabel72.Location = new System.Drawing.Point(25, 40);
            this.mmLabel72.Name = "mmLabel72";
            this.mmLabel72.PenWidth = 1F;
            this.mmLabel72.ShowBorder = false;
            this.mmLabel72.Size = new System.Drawing.Size(109, 17);
            this.mmLabel72.TabIndex = 138;
            this.mmLabel72.Text = "Health Card No :";
            // 
            // comboBoxMedicalResult
            // 
            this.comboBoxMedicalResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMedicalResult.FormattingEnabled = true;
            this.comboBoxMedicalResult.Items.AddRange(new object[] {
            "Fit",
            "Unfit"});
            this.comboBoxMedicalResult.Location = new System.Drawing.Point(164, 137);
            this.comboBoxMedicalResult.Name = "comboBoxMedicalResult";
            this.comboBoxMedicalResult.Size = new System.Drawing.Size(149, 25);
            this.comboBoxMedicalResult.TabIndex = 4;
            // 
            // mmLabel89
            // 
            this.mmLabel89.AutoSize = true;
            this.mmLabel89.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel89.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel89.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel89.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel89.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel89.IsFieldHeader = false;
            this.mmLabel89.IsRequired = false;
            this.mmLabel89.Location = new System.Drawing.Point(25, 144);
            this.mmLabel89.Name = "mmLabel89";
            this.mmLabel89.PenWidth = 1F;
            this.mmLabel89.ShowBorder = false;
            this.mmLabel89.Size = new System.Drawing.Size(101, 17);
            this.mmLabel89.TabIndex = 73;
            this.mmLabel89.Text = "Medical Result :";
            // 
            // dateTimeMedicalCollectedOn
            // 
            this.dateTimeMedicalCollectedOn.Checked = false;
            this.dateTimeMedicalCollectedOn.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeMedicalCollectedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeMedicalCollectedOn.Location = new System.Drawing.Point(164, 112);
            this.dateTimeMedicalCollectedOn.Name = "dateTimeMedicalCollectedOn";
            this.dateTimeMedicalCollectedOn.ShowCheckBox = true;
            this.dateTimeMedicalCollectedOn.Size = new System.Drawing.Size(149, 23);
            this.dateTimeMedicalCollectedOn.TabIndex = 3;
            this.dateTimeMedicalCollectedOn.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel41
            // 
            this.mmLabel41.AutoSize = true;
            this.mmLabel41.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel41.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel41.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel41.IsFieldHeader = false;
            this.mmLabel41.IsRequired = false;
            this.mmLabel41.Location = new System.Drawing.Point(25, 119);
            this.mmLabel41.Name = "mmLabel41";
            this.mmLabel41.PenWidth = 1F;
            this.mmLabel41.ShowBorder = false;
            this.mmLabel41.Size = new System.Drawing.Size(141, 17);
            this.mmLabel41.TabIndex = 72;
            this.mmLabel41.Text = "Medical Collected On :";
            // 
            // dateTimeMedicalAttendedOn
            // 
            this.dateTimeMedicalAttendedOn.Checked = false;
            this.dateTimeMedicalAttendedOn.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeMedicalAttendedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeMedicalAttendedOn.Location = new System.Drawing.Point(164, 87);
            this.dateTimeMedicalAttendedOn.Name = "dateTimeMedicalAttendedOn";
            this.dateTimeMedicalAttendedOn.ShowCheckBox = true;
            this.dateTimeMedicalAttendedOn.Size = new System.Drawing.Size(149, 23);
            this.dateTimeMedicalAttendedOn.TabIndex = 2;
            this.dateTimeMedicalAttendedOn.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel40
            // 
            this.mmLabel40.AutoSize = true;
            this.mmLabel40.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel40.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel40.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel40.IsFieldHeader = false;
            this.mmLabel40.IsRequired = false;
            this.mmLabel40.Location = new System.Drawing.Point(24, 93);
            this.mmLabel40.Name = "mmLabel40";
            this.mmLabel40.PenWidth = 1F;
            this.mmLabel40.ShowBorder = false;
            this.mmLabel40.Size = new System.Drawing.Size(142, 17);
            this.mmLabel40.TabIndex = 70;
            this.mmLabel40.Text = "Medical Attended On :";
            // 
            // dateTimeMedicalTypingOn
            // 
            this.dateTimeMedicalTypingOn.Checked = false;
            this.dateTimeMedicalTypingOn.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeMedicalTypingOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeMedicalTypingOn.Location = new System.Drawing.Point(164, 61);
            this.dateTimeMedicalTypingOn.Name = "dateTimeMedicalTypingOn";
            this.dateTimeMedicalTypingOn.ShowCheckBox = true;
            this.dateTimeMedicalTypingOn.Size = new System.Drawing.Size(149, 23);
            this.dateTimeMedicalTypingOn.TabIndex = 1;
            this.dateTimeMedicalTypingOn.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel35
            // 
            this.mmLabel35.AutoSize = true;
            this.mmLabel35.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel35.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel35.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel35.IsFieldHeader = false;
            this.mmLabel35.IsRequired = false;
            this.mmLabel35.Location = new System.Drawing.Point(25, 67);
            this.mmLabel35.Name = "mmLabel35";
            this.mmLabel35.PenWidth = 1F;
            this.mmLabel35.ShowBorder = false;
            this.mmLabel35.Size = new System.Drawing.Size(128, 17);
            this.mmLabel35.TabIndex = 11;
            this.mmLabel35.Text = "Medical Typing On :";
            // 
            // textBoxMedicalNote
            // 
            this.textBoxMedicalNote.BackColor = System.Drawing.Color.White;
            this.textBoxMedicalNote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxMedicalNote.CustomReportFieldName = "";
            this.textBoxMedicalNote.CustomReportKey = "";
            this.textBoxMedicalNote.CustomReportValueType = ((byte)(1));
            this.textBoxMedicalNote.IsComboTextBox = false;
            this.textBoxMedicalNote.IsModified = false;
            this.textBoxMedicalNote.Location = new System.Drawing.Point(179, 403);
            this.textBoxMedicalNote.MaxLength = 255;
            this.textBoxMedicalNote.Multiline = true;
            this.textBoxMedicalNote.Name = "textBoxMedicalNote";
            this.textBoxMedicalNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMedicalNote.Size = new System.Drawing.Size(781, 112);
            this.textBoxMedicalNote.TabIndex = 10;
            // 
            // mmLabel78
            // 
            this.mmLabel78.AutoSize = true;
            this.mmLabel78.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel78.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel78.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel78.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel78.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel78.IsFieldHeader = false;
            this.mmLabel78.IsRequired = false;
            this.mmLabel78.Location = new System.Drawing.Point(38, 406);
            this.mmLabel78.Name = "mmLabel78";
            this.mmLabel78.PenWidth = 1F;
            this.mmLabel78.ShowBorder = false;
            this.mmLabel78.Size = new System.Drawing.Size(150, 17);
            this.mmLabel78.TabIndex = 136;
            this.mmLabel78.Text = "Medical/Emirates Note :";
            // 
            // ultraTabPageControl3
            // 
            this.ultraTabPageControl3.Controls.Add(this.panelWPRP);
            this.ultraTabPageControl3.Location = new System.Drawing.Point(-12000, -11538);
            this.ultraTabPageControl3.Name = "ultraTabPageControl3";
            this.ultraTabPageControl3.Size = new System.Drawing.Size(974, 592);
            // 
            // panelWPRP
            // 
            this.panelWPRP.Controls.Add(this.panelMedicalReport);
            this.panelWPRP.Controls.Add(this.panelAGT);
            this.panelWPRP.Enabled = false;
            this.panelWPRP.Location = new System.Drawing.Point(-2, 0);
            this.panelWPRP.Name = "panelWPRP";
            this.panelWPRP.Size = new System.Drawing.Size(976, 610);
            this.panelWPRP.TabIndex = 0;
            // 
            // panelMedicalReport
            // 
            this.panelMedicalReport.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            this.panelMedicalReport.Controls.Add(this.comboBoxProcessType);
            this.panelMedicalReport.Controls.Add(this.dateTimeRPExpiryDate);
            this.panelMedicalReport.Controls.Add(this.mmLabel56);
            this.panelMedicalReport.Controls.Add(this.mmLabel65);
            this.panelMedicalReport.Controls.Add(this.dateTimeRPIssueDate);
            this.panelMedicalReport.Controls.Add(this.mmLabel64);
            this.panelMedicalReport.Controls.Add(this.textBoxRPIssuePlace);
            this.panelMedicalReport.Controls.Add(this.mmLabel63);
            this.panelMedicalReport.Controls.Add(this.dateTimePassportCollectedOnRP);
            this.panelMedicalReport.Controls.Add(this.mmLabel52);
            this.panelMedicalReport.Controls.Add(this.dateTimeSubmittedZajilOnRP);
            this.panelMedicalReport.Controls.Add(this.mmLabel50);
            this.panelMedicalReport.Controls.Add(this.dateTimeApplApprovedOnRP);
            this.panelMedicalReport.Controls.Add(this.mmLabel49);
            this.panelMedicalReport.Controls.Add(this.dateTimeApplPostedOnRP);
            this.panelMedicalReport.Controls.Add(this.mmLabel48);
            this.panelMedicalReport.Enabled = false;
            this.panelMedicalReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelMedicalReport.Location = new System.Drawing.Point(14, 306);
            this.panelMedicalReport.Name = "panelMedicalReport";
            this.panelMedicalReport.Size = new System.Drawing.Size(946, 256);
            this.panelMedicalReport.TabIndex = 140;
            this.panelMedicalReport.Text = "RP Submission Details";
            // 
            // comboBoxProcessType
            // 
            this.comboBoxProcessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcessType.FormattingEnabled = true;
            this.comboBoxProcessType.Items.AddRange(new object[] {
            "Online",
            "Manual"});
            this.comboBoxProcessType.Location = new System.Drawing.Point(188, 37);
            this.comboBoxProcessType.Name = "comboBoxProcessType";
            this.comboBoxProcessType.Size = new System.Drawing.Size(149, 25);
            this.comboBoxProcessType.TabIndex = 7;
            // 
            // dateTimeRPExpiryDate
            // 
            this.dateTimeRPExpiryDate.Checked = false;
            this.dateTimeRPExpiryDate.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeRPExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeRPExpiryDate.Location = new System.Drawing.Point(188, 217);
            this.dateTimeRPExpiryDate.Name = "dateTimeRPExpiryDate";
            this.dateTimeRPExpiryDate.ShowCheckBox = true;
            this.dateTimeRPExpiryDate.Size = new System.Drawing.Size(149, 23);
            this.dateTimeRPExpiryDate.TabIndex = 14;
            this.dateTimeRPExpiryDate.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel56
            // 
            this.mmLabel56.AutoSize = true;
            this.mmLabel56.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel56.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel56.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel56.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel56.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel56.IsFieldHeader = false;
            this.mmLabel56.IsRequired = false;
            this.mmLabel56.Location = new System.Drawing.Point(20, 40);
            this.mmLabel56.Name = "mmLabel56";
            this.mmLabel56.PenWidth = 1F;
            this.mmLabel56.ShowBorder = false;
            this.mmLabel56.Size = new System.Drawing.Size(95, 17);
            this.mmLabel56.TabIndex = 137;
            this.mmLabel56.Text = "Process Type:";
            // 
            // mmLabel65
            // 
            this.mmLabel65.AutoSize = true;
            this.mmLabel65.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel65.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel65.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel65.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel65.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel65.IsFieldHeader = false;
            this.mmLabel65.IsRequired = false;
            this.mmLabel65.Location = new System.Drawing.Point(20, 224);
            this.mmLabel65.Name = "mmLabel65";
            this.mmLabel65.PenWidth = 1F;
            this.mmLabel65.ShowBorder = false;
            this.mmLabel65.Size = new System.Drawing.Size(106, 17);
            this.mmLabel65.TabIndex = 84;
            this.mmLabel65.Text = "RP Expiry Date:";
            // 
            // dateTimeRPIssueDate
            // 
            this.dateTimeRPIssueDate.Checked = false;
            this.dateTimeRPIssueDate.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeRPIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeRPIssueDate.Location = new System.Drawing.Point(188, 190);
            this.dateTimeRPIssueDate.Name = "dateTimeRPIssueDate";
            this.dateTimeRPIssueDate.ShowCheckBox = true;
            this.dateTimeRPIssueDate.Size = new System.Drawing.Size(149, 23);
            this.dateTimeRPIssueDate.TabIndex = 13;
            this.dateTimeRPIssueDate.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel64
            // 
            this.mmLabel64.AutoSize = true;
            this.mmLabel64.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel64.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel64.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel64.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel64.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel64.IsFieldHeader = false;
            this.mmLabel64.IsRequired = false;
            this.mmLabel64.Location = new System.Drawing.Point(20, 197);
            this.mmLabel64.Name = "mmLabel64";
            this.mmLabel64.PenWidth = 1F;
            this.mmLabel64.ShowBorder = false;
            this.mmLabel64.Size = new System.Drawing.Size(98, 17);
            this.mmLabel64.TabIndex = 82;
            this.mmLabel64.Text = "RP Issue Date:";
            // 
            // textBoxRPIssuePlace
            // 
            this.textBoxRPIssuePlace.BackColor = System.Drawing.Color.White;
            this.textBoxRPIssuePlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxRPIssuePlace.CustomReportFieldName = "";
            this.textBoxRPIssuePlace.CustomReportKey = "";
            this.textBoxRPIssuePlace.CustomReportValueType = ((byte)(1));
            this.textBoxRPIssuePlace.IsComboTextBox = false;
            this.textBoxRPIssuePlace.IsModified = false;
            this.textBoxRPIssuePlace.Location = new System.Drawing.Point(188, 165);
            this.textBoxRPIssuePlace.MaxLength = 30;
            this.textBoxRPIssuePlace.Name = "textBoxRPIssuePlace";
            this.textBoxRPIssuePlace.Size = new System.Drawing.Size(501, 23);
            this.textBoxRPIssuePlace.TabIndex = 12;
            // 
            // mmLabel63
            // 
            this.mmLabel63.AutoSize = true;
            this.mmLabel63.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel63.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel63.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel63.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel63.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel63.IsFieldHeader = false;
            this.mmLabel63.IsRequired = false;
            this.mmLabel63.Location = new System.Drawing.Point(20, 171);
            this.mmLabel63.Name = "mmLabel63";
            this.mmLabel63.PenWidth = 1F;
            this.mmLabel63.ShowBorder = false;
            this.mmLabel63.Size = new System.Drawing.Size(100, 17);
            this.mmLabel63.TabIndex = 80;
            this.mmLabel63.Text = "RP Issue Place:";
            // 
            // dateTimePassportCollectedOnRP
            // 
            this.dateTimePassportCollectedOnRP.Checked = false;
            this.dateTimePassportCollectedOnRP.CustomFormat = " dd-MMM-yyyy";
            this.dateTimePassportCollectedOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePassportCollectedOnRP.Location = new System.Drawing.Point(188, 140);
            this.dateTimePassportCollectedOnRP.Name = "dateTimePassportCollectedOnRP";
            this.dateTimePassportCollectedOnRP.ShowCheckBox = true;
            this.dateTimePassportCollectedOnRP.Size = new System.Drawing.Size(149, 23);
            this.dateTimePassportCollectedOnRP.TabIndex = 11;
            this.dateTimePassportCollectedOnRP.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel52
            // 
            this.mmLabel52.AutoSize = true;
            this.mmLabel52.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel52.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel52.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel52.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel52.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel52.IsFieldHeader = false;
            this.mmLabel52.IsRequired = false;
            this.mmLabel52.Location = new System.Drawing.Point(20, 147);
            this.mmLabel52.Name = "mmLabel52";
            this.mmLabel52.PenWidth = 1F;
            this.mmLabel52.ShowBorder = false;
            this.mmLabel52.Size = new System.Drawing.Size(147, 17);
            this.mmLabel52.TabIndex = 76;
            this.mmLabel52.Text = "Passport Collected On:";
            // 
            // dateTimeSubmittedZajilOnRP
            // 
            this.dateTimeSubmittedZajilOnRP.Checked = false;
            this.dateTimeSubmittedZajilOnRP.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeSubmittedZajilOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeSubmittedZajilOnRP.Location = new System.Drawing.Point(188, 114);
            this.dateTimeSubmittedZajilOnRP.Name = "dateTimeSubmittedZajilOnRP";
            this.dateTimeSubmittedZajilOnRP.ShowCheckBox = true;
            this.dateTimeSubmittedZajilOnRP.Size = new System.Drawing.Size(149, 23);
            this.dateTimeSubmittedZajilOnRP.TabIndex = 10;
            this.dateTimeSubmittedZajilOnRP.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel50
            // 
            this.mmLabel50.AutoSize = true;
            this.mmLabel50.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel50.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel50.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel50.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel50.IsFieldHeader = false;
            this.mmLabel50.IsRequired = false;
            this.mmLabel50.Location = new System.Drawing.Point(20, 121);
            this.mmLabel50.Name = "mmLabel50";
            this.mmLabel50.PenWidth = 1F;
            this.mmLabel50.ShowBorder = false;
            this.mmLabel50.Size = new System.Drawing.Size(142, 17);
            this.mmLabel50.TabIndex = 74;
            this.mmLabel50.Text = "Submitted to Zajil On:";
            // 
            // dateTimeApplApprovedOnRP
            // 
            this.dateTimeApplApprovedOnRP.Checked = false;
            this.dateTimeApplApprovedOnRP.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeApplApprovedOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeApplApprovedOnRP.Location = new System.Drawing.Point(188, 88);
            this.dateTimeApplApprovedOnRP.Name = "dateTimeApplApprovedOnRP";
            this.dateTimeApplApprovedOnRP.ShowCheckBox = true;
            this.dateTimeApplApprovedOnRP.Size = new System.Drawing.Size(149, 23);
            this.dateTimeApplApprovedOnRP.TabIndex = 9;
            this.dateTimeApplApprovedOnRP.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel49
            // 
            this.mmLabel49.AutoSize = true;
            this.mmLabel49.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel49.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel49.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel49.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel49.IsFieldHeader = false;
            this.mmLabel49.IsRequired = false;
            this.mmLabel49.Location = new System.Drawing.Point(20, 95);
            this.mmLabel49.Name = "mmLabel49";
            this.mmLabel49.PenWidth = 1F;
            this.mmLabel49.ShowBorder = false;
            this.mmLabel49.Size = new System.Drawing.Size(129, 17);
            this.mmLabel49.TabIndex = 72;
            this.mmLabel49.Text = "Appl. Approved On:";
            // 
            // dateTimeApplPostedOnRP
            // 
            this.dateTimeApplPostedOnRP.Checked = false;
            this.dateTimeApplPostedOnRP.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeApplPostedOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeApplPostedOnRP.Location = new System.Drawing.Point(188, 62);
            this.dateTimeApplPostedOnRP.Name = "dateTimeApplPostedOnRP";
            this.dateTimeApplPostedOnRP.ShowCheckBox = true;
            this.dateTimeApplPostedOnRP.Size = new System.Drawing.Size(149, 23);
            this.dateTimeApplPostedOnRP.TabIndex = 8;
            this.dateTimeApplPostedOnRP.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel48
            // 
            this.mmLabel48.AutoSize = true;
            this.mmLabel48.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel48.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel48.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel48.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel48.IsFieldHeader = false;
            this.mmLabel48.IsRequired = false;
            this.mmLabel48.Location = new System.Drawing.Point(20, 68);
            this.mmLabel48.Name = "mmLabel48";
            this.mmLabel48.PenWidth = 1F;
            this.mmLabel48.ShowBorder = false;
            this.mmLabel48.Size = new System.Drawing.Size(111, 17);
            this.mmLabel48.TabIndex = 70;
            this.mmLabel48.Text = "Appl. Posted On:";
            // 
            // panelAGT
            // 
            this.panelAGT.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            this.panelAGT.Controls.Add(this.textBoxAGTMBNo);
            this.panelAGT.Controls.Add(this.mmLabel91);
            this.panelAGT.Controls.Add(this.mmLabel90);
            this.panelAGT.Controls.Add(this.comboBoxAGTType);
            this.panelAGT.Controls.Add(this.textBoxPersonIDNo);
            this.panelAGT.Controls.Add(this.mmLabel66);
            this.panelAGT.Controls.Add(this.textBoxWPIssuePlace);
            this.panelAGT.Controls.Add(this.dateTimeWPExpiryDate);
            this.panelAGT.Controls.Add(this.dateTimeWPIssueDate);
            this.panelAGT.Controls.Add(this.mmLabel62);
            this.panelAGT.Controls.Add(this.mmLabel61);
            this.panelAGT.Controls.Add(this.mmLabel60);
            this.panelAGT.Controls.Add(this.textBoxWPNo);
            this.panelAGT.Controls.Add(this.dateTimeAGTSubmittedOn);
            this.panelAGT.Controls.Add(this.mmLabel57);
            this.panelAGT.Controls.Add(this.mmLabel58);
            this.panelAGT.Controls.Add(this.dateTimeAGTTypedOn);
            this.panelAGT.Controls.Add(this.mmLabel59);
            this.panelAGT.Enabled = false;
            this.panelAGT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelAGT.Location = new System.Drawing.Point(14, 28);
            this.panelAGT.Name = "panelAGT";
            this.panelAGT.Size = new System.Drawing.Size(946, 263);
            this.panelAGT.TabIndex = 139;
            this.panelAGT.Text = "AGT/WP Submission Details";
            // 
            // textBoxAGTMBNo
            // 
            this.textBoxAGTMBNo.BackColor = System.Drawing.Color.White;
            this.textBoxAGTMBNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxAGTMBNo.CustomReportFieldName = "";
            this.textBoxAGTMBNo.CustomReportKey = "";
            this.textBoxAGTMBNo.CustomReportValueType = ((byte)(1));
            this.textBoxAGTMBNo.IsComboTextBox = false;
            this.textBoxAGTMBNo.IsModified = false;
            this.textBoxAGTMBNo.Location = new System.Drawing.Point(188, 53);
            this.textBoxAGTMBNo.MaxLength = 30;
            this.textBoxAGTMBNo.Name = "textBoxAGTMBNo";
            this.textBoxAGTMBNo.Size = new System.Drawing.Size(281, 23);
            this.textBoxAGTMBNo.TabIndex = 1;
            // 
            // mmLabel91
            // 
            this.mmLabel91.AutoSize = true;
            this.mmLabel91.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel91.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel91.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel91.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel91.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel91.IsFieldHeader = false;
            this.mmLabel91.IsRequired = false;
            this.mmLabel91.Location = new System.Drawing.Point(20, 57);
            this.mmLabel91.Name = "mmLabel91";
            this.mmLabel91.PenWidth = 1F;
            this.mmLabel91.ShowBorder = false;
            this.mmLabel91.Size = new System.Drawing.Size(145, 17);
            this.mmLabel91.TabIndex = 86;
            this.mmLabel91.Text = "MB Ref No - AGT/WP :";
            // 
            // mmLabel90
            // 
            this.mmLabel90.AutoSize = true;
            this.mmLabel90.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel90.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel90.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel90.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel90.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel90.IsFieldHeader = false;
            this.mmLabel90.IsRequired = false;
            this.mmLabel90.Location = new System.Drawing.Point(20, 31);
            this.mmLabel90.Name = "mmLabel90";
            this.mmLabel90.PenWidth = 1F;
            this.mmLabel90.ShowBorder = false;
            this.mmLabel90.Size = new System.Drawing.Size(73, 17);
            this.mmLabel90.TabIndex = 84;
            this.mmLabel90.Text = "AGT Type:";
            // 
            // comboBoxAGTType
            // 
            this.comboBoxAGTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAGTType.FormattingEnabled = true;
            this.comboBoxAGTType.Items.AddRange(new object[] {
            "Limited",
            "Unlimited"});
            this.comboBoxAGTType.Location = new System.Drawing.Point(188, 28);
            this.comboBoxAGTType.Name = "comboBoxAGTType";
            this.comboBoxAGTType.Size = new System.Drawing.Size(149, 25);
            this.comboBoxAGTType.TabIndex = 0;
            // 
            // textBoxPersonIDNo
            // 
            this.textBoxPersonIDNo.BackColor = System.Drawing.Color.White;
            this.textBoxPersonIDNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxPersonIDNo.CustomReportFieldName = "";
            this.textBoxPersonIDNo.CustomReportKey = "";
            this.textBoxPersonIDNo.CustomReportValueType = ((byte)(1));
            this.textBoxPersonIDNo.IsComboTextBox = false;
            this.textBoxPersonIDNo.IsModified = false;
            this.textBoxPersonIDNo.Location = new System.Drawing.Point(188, 155);
            this.textBoxPersonIDNo.MaxLength = 30;
            this.textBoxPersonIDNo.Name = "textBoxPersonIDNo";
            this.textBoxPersonIDNo.Size = new System.Drawing.Size(281, 23);
            this.textBoxPersonIDNo.TabIndex = 5;
            // 
            // mmLabel66
            // 
            this.mmLabel66.AutoSize = true;
            this.mmLabel66.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel66.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel66.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel66.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel66.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel66.IsFieldHeader = false;
            this.mmLabel66.IsRequired = false;
            this.mmLabel66.Location = new System.Drawing.Point(20, 158);
            this.mmLabel66.Name = "mmLabel66";
            this.mmLabel66.PenWidth = 1F;
            this.mmLabel66.ShowBorder = false;
            this.mmLabel66.Size = new System.Drawing.Size(103, 17);
            this.mmLabel66.TabIndex = 82;
            this.mmLabel66.Text = "Personal ID No:";
            // 
            // textBoxWPIssuePlace
            // 
            this.textBoxWPIssuePlace.BackColor = System.Drawing.Color.White;
            this.textBoxWPIssuePlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxWPIssuePlace.CustomReportFieldName = "";
            this.textBoxWPIssuePlace.CustomReportKey = "";
            this.textBoxWPIssuePlace.CustomReportValueType = ((byte)(1));
            this.textBoxWPIssuePlace.IsComboTextBox = false;
            this.textBoxWPIssuePlace.IsModified = false;
            this.textBoxWPIssuePlace.Location = new System.Drawing.Point(188, 180);
            this.textBoxWPIssuePlace.MaxLength = 30;
            this.textBoxWPIssuePlace.Name = "textBoxWPIssuePlace";
            this.textBoxWPIssuePlace.Size = new System.Drawing.Size(501, 23);
            this.textBoxWPIssuePlace.TabIndex = 6;
            // 
            // dateTimeWPExpiryDate
            // 
            this.dateTimeWPExpiryDate.Checked = false;
            this.dateTimeWPExpiryDate.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeWPExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeWPExpiryDate.Location = new System.Drawing.Point(188, 231);
            this.dateTimeWPExpiryDate.Name = "dateTimeWPExpiryDate";
            this.dateTimeWPExpiryDate.ShowCheckBox = true;
            this.dateTimeWPExpiryDate.Size = new System.Drawing.Size(149, 23);
            this.dateTimeWPExpiryDate.TabIndex = 8;
            this.dateTimeWPExpiryDate.Value = new System.DateTime(((long)(0)));
            // 
            // dateTimeWPIssueDate
            // 
            this.dateTimeWPIssueDate.Checked = false;
            this.dateTimeWPIssueDate.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeWPIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeWPIssueDate.Location = new System.Drawing.Point(188, 205);
            this.dateTimeWPIssueDate.Name = "dateTimeWPIssueDate";
            this.dateTimeWPIssueDate.ShowCheckBox = true;
            this.dateTimeWPIssueDate.Size = new System.Drawing.Size(149, 23);
            this.dateTimeWPIssueDate.TabIndex = 7;
            this.dateTimeWPIssueDate.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel62
            // 
            this.mmLabel62.AutoSize = true;
            this.mmLabel62.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel62.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel62.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel62.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel62.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel62.IsFieldHeader = false;
            this.mmLabel62.IsRequired = false;
            this.mmLabel62.Location = new System.Drawing.Point(20, 237);
            this.mmLabel62.Name = "mmLabel62";
            this.mmLabel62.PenWidth = 1F;
            this.mmLabel62.ShowBorder = false;
            this.mmLabel62.Size = new System.Drawing.Size(111, 17);
            this.mmLabel62.TabIndex = 81;
            this.mmLabel62.Text = "WP Expiry Date:";
            // 
            // mmLabel61
            // 
            this.mmLabel61.AutoSize = true;
            this.mmLabel61.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel61.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel61.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel61.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel61.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel61.IsFieldHeader = false;
            this.mmLabel61.IsRequired = false;
            this.mmLabel61.Location = new System.Drawing.Point(20, 211);
            this.mmLabel61.Name = "mmLabel61";
            this.mmLabel61.PenWidth = 1F;
            this.mmLabel61.ShowBorder = false;
            this.mmLabel61.Size = new System.Drawing.Size(103, 17);
            this.mmLabel61.TabIndex = 80;
            this.mmLabel61.Text = "WP Issue Date:";
            // 
            // mmLabel60
            // 
            this.mmLabel60.AutoSize = true;
            this.mmLabel60.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel60.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel60.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel60.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel60.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel60.IsFieldHeader = false;
            this.mmLabel60.IsRequired = false;
            this.mmLabel60.Location = new System.Drawing.Point(20, 185);
            this.mmLabel60.Name = "mmLabel60";
            this.mmLabel60.PenWidth = 1F;
            this.mmLabel60.ShowBorder = false;
            this.mmLabel60.Size = new System.Drawing.Size(105, 17);
            this.mmLabel60.TabIndex = 79;
            this.mmLabel60.Text = "WP Issue Place:";
            // 
            // textBoxWPNo
            // 
            this.textBoxWPNo.BackColor = System.Drawing.Color.White;
            this.textBoxWPNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxWPNo.CustomReportFieldName = "";
            this.textBoxWPNo.CustomReportKey = "";
            this.textBoxWPNo.CustomReportValueType = ((byte)(1));
            this.textBoxWPNo.IsComboTextBox = false;
            this.textBoxWPNo.IsModified = false;
            this.textBoxWPNo.Location = new System.Drawing.Point(188, 129);
            this.textBoxWPNo.MaxLength = 30;
            this.textBoxWPNo.Name = "textBoxWPNo";
            this.textBoxWPNo.Size = new System.Drawing.Size(281, 23);
            this.textBoxWPNo.TabIndex = 4;
            // 
            // dateTimeAGTSubmittedOn
            // 
            this.dateTimeAGTSubmittedOn.Checked = false;
            this.dateTimeAGTSubmittedOn.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeAGTSubmittedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeAGTSubmittedOn.Location = new System.Drawing.Point(188, 104);
            this.dateTimeAGTSubmittedOn.Name = "dateTimeAGTSubmittedOn";
            this.dateTimeAGTSubmittedOn.ShowCheckBox = true;
            this.dateTimeAGTSubmittedOn.Size = new System.Drawing.Size(149, 23);
            this.dateTimeAGTSubmittedOn.TabIndex = 3;
            this.dateTimeAGTSubmittedOn.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel57
            // 
            this.mmLabel57.AutoSize = true;
            this.mmLabel57.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel57.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel57.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel57.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel57.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel57.IsFieldHeader = false;
            this.mmLabel57.IsRequired = false;
            this.mmLabel57.Location = new System.Drawing.Point(20, 108);
            this.mmLabel57.Name = "mmLabel57";
            this.mmLabel57.PenWidth = 1F;
            this.mmLabel57.ShowBorder = false;
            this.mmLabel57.Size = new System.Drawing.Size(152, 17);
            this.mmLabel57.TabIndex = 74;
            this.mmLabel57.Text = "AGT/WP Submitted on:";
            // 
            // mmLabel58
            // 
            this.mmLabel58.AutoSize = true;
            this.mmLabel58.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel58.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel58.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel58.IsFieldHeader = false;
            this.mmLabel58.IsRequired = false;
            this.mmLabel58.Location = new System.Drawing.Point(20, 134);
            this.mmLabel58.Name = "mmLabel58";
            this.mmLabel58.PenWidth = 1F;
            this.mmLabel58.ShowBorder = false;
            this.mmLabel58.Size = new System.Drawing.Size(56, 17);
            this.mmLabel58.TabIndex = 72;
            this.mmLabel58.Text = "WP No:";
            // 
            // dateTimeAGTTypedOn
            // 
            this.dateTimeAGTTypedOn.Checked = false;
            this.dateTimeAGTTypedOn.CustomFormat = " dd-MMM-yyyy";
            this.dateTimeAGTTypedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeAGTTypedOn.Location = new System.Drawing.Point(188, 78);
            this.dateTimeAGTTypedOn.Name = "dateTimeAGTTypedOn";
            this.dateTimeAGTTypedOn.ShowCheckBox = true;
            this.dateTimeAGTTypedOn.Size = new System.Drawing.Size(149, 23);
            this.dateTimeAGTTypedOn.TabIndex = 2;
            this.dateTimeAGTTypedOn.Value = new System.DateTime(((long)(0)));
            // 
            // mmLabel59
            // 
            this.mmLabel59.AutoSize = true;
            this.mmLabel59.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel59.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel59.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel59.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel59.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel59.IsFieldHeader = false;
            this.mmLabel59.IsRequired = false;
            this.mmLabel59.Location = new System.Drawing.Point(20, 83);
            this.mmLabel59.Name = "mmLabel59";
            this.mmLabel59.PenWidth = 1F;
            this.mmLabel59.ShowBorder = false;
            this.mmLabel59.Size = new System.Drawing.Size(166, 17);
            this.mmLabel59.TabIndex = 70;
            this.mmLabel59.Text = "AGT/WP Form Typed On:";
            // 
            // tabPageUserDefined
            // 
            this.tabPageUserDefined.Controls.Add(this.udfEntryGrid);
            this.tabPageUserDefined.Location = new System.Drawing.Point(-12000, -11538);
            this.tabPageUserDefined.Name = "tabPageUserDefined";
            this.tabPageUserDefined.Size = new System.Drawing.Size(974, 592);
            // 
            // udfEntryGrid
            // 
            this.udfEntryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.udfEntryGrid.Enabled = false;
            this.udfEntryGrid.Location = new System.Drawing.Point(10, 21);
            this.udfEntryGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.udfEntryGrid.Name = "udfEntryGrid";
            this.udfEntryGrid.Size = new System.Drawing.Size(950, 513);
            this.udfEntryGrid.TabIndex = 1;
            this.udfEntryGrid.TableName = "";
            // 
            // ultraTabPageControl5
            // 
            this.ultraTabPageControl5.Controls.Add(this.ultraGroupBox1);
            this.ultraTabPageControl5.Location = new System.Drawing.Point(-12000, -11538);
            this.ultraTabPageControl5.Name = "ultraTabPageControl5";
            this.ultraTabPageControl5.Size = new System.Drawing.Size(974, 592);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
            this.ultraGroupBox1.Controls.Add(this.mmLabel23);
            this.ultraGroupBox1.Controls.Add(this.textBoxComment);
            this.ultraGroupBox1.Controls.Add(this.mmLabel20);
            this.ultraGroupBox1.Controls.Add(this.textBoxPostalCode);
            this.ultraGroupBox1.Controls.Add(this.mmLabel18);
            this.ultraGroupBox1.Controls.Add(this.textBoxEmail);
            this.ultraGroupBox1.Controls.Add(this.mmLabel17);
            this.ultraGroupBox1.Controls.Add(this.textBoxMobile);
            this.ultraGroupBox1.Controls.Add(this.mmLabel16);
            this.ultraGroupBox1.Controls.Add(this.textBoxFax);
            this.ultraGroupBox1.Controls.Add(this.mmLabel15);
            this.ultraGroupBox1.Controls.Add(this.textBoxPhone2);
            this.ultraGroupBox1.Controls.Add(this.mmLabel14);
            this.ultraGroupBox1.Controls.Add(this.textBoxPhone1);
            this.ultraGroupBox1.Controls.Add(this.mmLabel12);
            this.ultraGroupBox1.Controls.Add(this.textBoxCountry);
            this.ultraGroupBox1.Controls.Add(this.mmLabel11);
            this.ultraGroupBox1.Controls.Add(this.textBoxState);
            this.ultraGroupBox1.Controls.Add(this.mmLabel13);
            this.ultraGroupBox1.Controls.Add(this.textBoxCity);
            this.ultraGroupBox1.Controls.Add(this.textBoxAddress3);
            this.ultraGroupBox1.Controls.Add(this.textBoxAddress2);
            this.ultraGroupBox1.Controls.Add(this.mmLabel10);
            this.ultraGroupBox1.Controls.Add(this.textBoxAddress1);
            this.ultraGroupBox1.Controls.Add(this.mmLabel8);
            this.ultraGroupBox1.Controls.Add(this.textBoxAddressID);
            this.ultraGroupBox1.Location = new System.Drawing.Point(12, 23);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(862, 237);
            this.ultraGroupBox1.TabIndex = 22;
            this.ultraGroupBox1.Text = "Address";
            this.ultraGroupBox1.Visible = false;
            // 
            // mmLabel23
            // 
            this.mmLabel23.AutoSize = true;
            this.mmLabel23.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel23.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel23.IsFieldHeader = false;
            this.mmLabel23.IsRequired = false;
            this.mmLabel23.Location = new System.Drawing.Point(434, 152);
            this.mmLabel23.Name = "mmLabel23";
            this.mmLabel23.PenWidth = 1F;
            this.mmLabel23.ShowBorder = false;
            this.mmLabel23.Size = new System.Drawing.Size(74, 17);
            this.mmLabel23.TabIndex = 32;
            this.mmLabel23.Text = "Comment:";
            // 
            // textBoxComment
            // 
            this.textBoxComment.BackColor = System.Drawing.Color.White;
            this.textBoxComment.CustomReportFieldName = "";
            this.textBoxComment.CustomReportKey = "";
            this.textBoxComment.CustomReportValueType = ((byte)(1));
            this.textBoxComment.IsComboTextBox = false;
            this.textBoxComment.IsModified = false;
            this.textBoxComment.Location = new System.Drawing.Point(552, 149);
            this.textBoxComment.MaxLength = 255;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(275, 22);
            this.textBoxComment.TabIndex = 13;
            // 
            // mmLabel20
            // 
            this.mmLabel20.AutoSize = true;
            this.mmLabel20.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel20.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel20.IsFieldHeader = false;
            this.mmLabel20.IsRequired = false;
            this.mmLabel20.Location = new System.Drawing.Point(11, 203);
            this.mmLabel20.Name = "mmLabel20";
            this.mmLabel20.PenWidth = 1F;
            this.mmLabel20.ShowBorder = false;
            this.mmLabel20.Size = new System.Drawing.Size(85, 17);
            this.mmLabel20.TabIndex = 14;
            this.mmLabel20.Text = "Postal Code:";
            // 
            // textBoxPostalCode
            // 
            this.textBoxPostalCode.BackColor = System.Drawing.Color.White;
            this.textBoxPostalCode.CustomReportFieldName = "";
            this.textBoxPostalCode.CustomReportKey = "";
            this.textBoxPostalCode.CustomReportValueType = ((byte)(1));
            this.textBoxPostalCode.IsComboTextBox = false;
            this.textBoxPostalCode.IsModified = false;
            this.textBoxPostalCode.Location = new System.Drawing.Point(148, 200);
            this.textBoxPostalCode.MaxLength = 30;
            this.textBoxPostalCode.Name = "textBoxPostalCode";
            this.textBoxPostalCode.Size = new System.Drawing.Size(274, 22);
            this.textBoxPostalCode.TabIndex = 7;
            // 
            // mmLabel18
            // 
            this.mmLabel18.AutoSize = true;
            this.mmLabel18.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel18.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel18.IsFieldHeader = false;
            this.mmLabel18.IsRequired = false;
            this.mmLabel18.Location = new System.Drawing.Point(434, 127);
            this.mmLabel18.Name = "mmLabel18";
            this.mmLabel18.PenWidth = 1F;
            this.mmLabel18.ShowBorder = false;
            this.mmLabel18.Size = new System.Drawing.Size(44, 17);
            this.mmLabel18.TabIndex = 28;
            this.mmLabel18.Text = "Email:";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.BackColor = System.Drawing.Color.White;
            this.textBoxEmail.CustomReportFieldName = "";
            this.textBoxEmail.CustomReportKey = "";
            this.textBoxEmail.CustomReportValueType = ((byte)(1));
            this.textBoxEmail.IsComboTextBox = false;
            this.textBoxEmail.IsModified = false;
            this.textBoxEmail.Location = new System.Drawing.Point(552, 123);
            this.textBoxEmail.MaxLength = 64;
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(275, 22);
            this.textBoxEmail.TabIndex = 12;
            // 
            // mmLabel17
            // 
            this.mmLabel17.AutoSize = true;
            this.mmLabel17.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel17.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel17.IsFieldHeader = false;
            this.mmLabel17.IsRequired = false;
            this.mmLabel17.Location = new System.Drawing.Point(434, 102);
            this.mmLabel17.Name = "mmLabel17";
            this.mmLabel17.PenWidth = 1F;
            this.mmLabel17.ShowBorder = false;
            this.mmLabel17.Size = new System.Drawing.Size(50, 17);
            this.mmLabel17.TabIndex = 26;
            this.mmLabel17.Text = "Mobile:";
            // 
            // textBoxMobile
            // 
            this.textBoxMobile.BackColor = System.Drawing.Color.White;
            this.textBoxMobile.CustomReportFieldName = "";
            this.textBoxMobile.CustomReportKey = "";
            this.textBoxMobile.CustomReportValueType = ((byte)(1));
            this.textBoxMobile.IsComboTextBox = false;
            this.textBoxMobile.IsModified = false;
            this.textBoxMobile.Location = new System.Drawing.Point(552, 98);
            this.textBoxMobile.MaxLength = 30;
            this.textBoxMobile.Name = "textBoxMobile";
            this.textBoxMobile.Size = new System.Drawing.Size(275, 22);
            this.textBoxMobile.TabIndex = 11;
            // 
            // mmLabel16
            // 
            this.mmLabel16.AutoSize = true;
            this.mmLabel16.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel16.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel16.IsFieldHeader = false;
            this.mmLabel16.IsRequired = false;
            this.mmLabel16.Location = new System.Drawing.Point(434, 75);
            this.mmLabel16.Name = "mmLabel16";
            this.mmLabel16.PenWidth = 1F;
            this.mmLabel16.ShowBorder = false;
            this.mmLabel16.Size = new System.Drawing.Size(35, 17);
            this.mmLabel16.TabIndex = 24;
            this.mmLabel16.Text = "Fax:";
            // 
            // textBoxFax
            // 
            this.textBoxFax.BackColor = System.Drawing.Color.White;
            this.textBoxFax.CustomReportFieldName = "";
            this.textBoxFax.CustomReportKey = "";
            this.textBoxFax.CustomReportValueType = ((byte)(1));
            this.textBoxFax.IsComboTextBox = false;
            this.textBoxFax.IsModified = false;
            this.textBoxFax.Location = new System.Drawing.Point(552, 73);
            this.textBoxFax.MaxLength = 30;
            this.textBoxFax.Name = "textBoxFax";
            this.textBoxFax.Size = new System.Drawing.Size(275, 22);
            this.textBoxFax.TabIndex = 10;
            // 
            // mmLabel15
            // 
            this.mmLabel15.AutoSize = true;
            this.mmLabel15.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel15.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel15.IsFieldHeader = false;
            this.mmLabel15.IsRequired = false;
            this.mmLabel15.Location = new System.Drawing.Point(434, 51);
            this.mmLabel15.Name = "mmLabel15";
            this.mmLabel15.PenWidth = 1F;
            this.mmLabel15.ShowBorder = false;
            this.mmLabel15.Size = new System.Drawing.Size(64, 17);
            this.mmLabel15.TabIndex = 22;
            this.mmLabel15.Text = "Phone 2:";
            // 
            // textBoxPhone2
            // 
            this.textBoxPhone2.BackColor = System.Drawing.Color.White;
            this.textBoxPhone2.CustomReportFieldName = "";
            this.textBoxPhone2.CustomReportKey = "";
            this.textBoxPhone2.CustomReportValueType = ((byte)(1));
            this.textBoxPhone2.IsComboTextBox = false;
            this.textBoxPhone2.IsModified = false;
            this.textBoxPhone2.Location = new System.Drawing.Point(552, 47);
            this.textBoxPhone2.MaxLength = 30;
            this.textBoxPhone2.Name = "textBoxPhone2";
            this.textBoxPhone2.Size = new System.Drawing.Size(275, 22);
            this.textBoxPhone2.TabIndex = 9;
            // 
            // mmLabel14
            // 
            this.mmLabel14.AutoSize = true;
            this.mmLabel14.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel14.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel14.IsFieldHeader = false;
            this.mmLabel14.IsRequired = false;
            this.mmLabel14.Location = new System.Drawing.Point(434, 25);
            this.mmLabel14.Name = "mmLabel14";
            this.mmLabel14.PenWidth = 1F;
            this.mmLabel14.ShowBorder = false;
            this.mmLabel14.Size = new System.Drawing.Size(64, 17);
            this.mmLabel14.TabIndex = 20;
            this.mmLabel14.Text = "Phone 1:";
            // 
            // textBoxPhone1
            // 
            this.textBoxPhone1.BackColor = System.Drawing.Color.White;
            this.textBoxPhone1.CustomReportFieldName = "";
            this.textBoxPhone1.CustomReportKey = "";
            this.textBoxPhone1.CustomReportValueType = ((byte)(1));
            this.textBoxPhone1.IsComboTextBox = false;
            this.textBoxPhone1.IsModified = false;
            this.textBoxPhone1.Location = new System.Drawing.Point(552, 22);
            this.textBoxPhone1.MaxLength = 30;
            this.textBoxPhone1.Name = "textBoxPhone1";
            this.textBoxPhone1.Size = new System.Drawing.Size(275, 22);
            this.textBoxPhone1.TabIndex = 8;
            // 
            // mmLabel12
            // 
            this.mmLabel12.AutoSize = true;
            this.mmLabel12.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel12.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel12.IsFieldHeader = false;
            this.mmLabel12.IsRequired = false;
            this.mmLabel12.Location = new System.Drawing.Point(11, 178);
            this.mmLabel12.Name = "mmLabel12";
            this.mmLabel12.PenWidth = 1F;
            this.mmLabel12.ShowBorder = false;
            this.mmLabel12.Size = new System.Drawing.Size(64, 17);
            this.mmLabel12.TabIndex = 12;
            this.mmLabel12.Text = "Country:";
            // 
            // textBoxCountry
            // 
            this.textBoxCountry.BackColor = System.Drawing.Color.White;
            this.textBoxCountry.CustomReportFieldName = "";
            this.textBoxCountry.CustomReportKey = "";
            this.textBoxCountry.CustomReportValueType = ((byte)(1));
            this.textBoxCountry.IsComboTextBox = false;
            this.textBoxCountry.IsModified = false;
            this.textBoxCountry.Location = new System.Drawing.Point(148, 174);
            this.textBoxCountry.MaxLength = 30;
            this.textBoxCountry.Name = "textBoxCountry";
            this.textBoxCountry.Size = new System.Drawing.Size(274, 22);
            this.textBoxCountry.TabIndex = 6;
            // 
            // mmLabel11
            // 
            this.mmLabel11.AutoSize = true;
            this.mmLabel11.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel11.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel11.IsFieldHeader = false;
            this.mmLabel11.IsRequired = false;
            this.mmLabel11.Location = new System.Drawing.Point(11, 152);
            this.mmLabel11.Name = "mmLabel11";
            this.mmLabel11.PenWidth = 1F;
            this.mmLabel11.ShowBorder = false;
            this.mmLabel11.Size = new System.Drawing.Size(45, 17);
            this.mmLabel11.TabIndex = 10;
            this.mmLabel11.Text = "State:";
            // 
            // textBoxState
            // 
            this.textBoxState.BackColor = System.Drawing.Color.White;
            this.textBoxState.CustomReportFieldName = "";
            this.textBoxState.CustomReportKey = "";
            this.textBoxState.CustomReportValueType = ((byte)(1));
            this.textBoxState.IsComboTextBox = false;
            this.textBoxState.IsModified = false;
            this.textBoxState.Location = new System.Drawing.Point(148, 149);
            this.textBoxState.MaxLength = 30;
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.Size = new System.Drawing.Size(274, 22);
            this.textBoxState.TabIndex = 5;
            // 
            // mmLabel13
            // 
            this.mmLabel13.AutoSize = true;
            this.mmLabel13.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel13.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel13.IsFieldHeader = false;
            this.mmLabel13.IsRequired = false;
            this.mmLabel13.Location = new System.Drawing.Point(11, 126);
            this.mmLabel13.Name = "mmLabel13";
            this.mmLabel13.PenWidth = 1F;
            this.mmLabel13.ShowBorder = false;
            this.mmLabel13.Size = new System.Drawing.Size(37, 17);
            this.mmLabel13.TabIndex = 8;
            this.mmLabel13.Text = "City:";
            // 
            // textBoxCity
            // 
            this.textBoxCity.BackColor = System.Drawing.Color.White;
            this.textBoxCity.CustomReportFieldName = "";
            this.textBoxCity.CustomReportKey = "";
            this.textBoxCity.CustomReportValueType = ((byte)(1));
            this.textBoxCity.IsComboTextBox = false;
            this.textBoxCity.IsModified = false;
            this.textBoxCity.Location = new System.Drawing.Point(148, 123);
            this.textBoxCity.MaxLength = 30;
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(274, 22);
            this.textBoxCity.TabIndex = 4;
            // 
            // textBoxAddress3
            // 
            this.textBoxAddress3.BackColor = System.Drawing.Color.White;
            this.textBoxAddress3.CustomReportFieldName = "";
            this.textBoxAddress3.CustomReportKey = "";
            this.textBoxAddress3.CustomReportValueType = ((byte)(1));
            this.textBoxAddress3.IsComboTextBox = false;
            this.textBoxAddress3.IsModified = false;
            this.textBoxAddress3.Location = new System.Drawing.Point(148, 98);
            this.textBoxAddress3.MaxLength = 64;
            this.textBoxAddress3.Name = "textBoxAddress3";
            this.textBoxAddress3.Size = new System.Drawing.Size(274, 22);
            this.textBoxAddress3.TabIndex = 3;
            // 
            // textBoxAddress2
            // 
            this.textBoxAddress2.BackColor = System.Drawing.Color.White;
            this.textBoxAddress2.CustomReportFieldName = "";
            this.textBoxAddress2.CustomReportKey = "";
            this.textBoxAddress2.CustomReportValueType = ((byte)(1));
            this.textBoxAddress2.IsComboTextBox = false;
            this.textBoxAddress2.IsModified = false;
            this.textBoxAddress2.Location = new System.Drawing.Point(148, 73);
            this.textBoxAddress2.MaxLength = 64;
            this.textBoxAddress2.Name = "textBoxAddress2";
            this.textBoxAddress2.Size = new System.Drawing.Size(274, 22);
            this.textBoxAddress2.TabIndex = 2;
            // 
            // mmLabel10
            // 
            this.mmLabel10.AutoSize = true;
            this.mmLabel10.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel10.IsFieldHeader = false;
            this.mmLabel10.IsRequired = false;
            this.mmLabel10.Location = new System.Drawing.Point(11, 50);
            this.mmLabel10.Name = "mmLabel10";
            this.mmLabel10.PenWidth = 1F;
            this.mmLabel10.ShowBorder = false;
            this.mmLabel10.Size = new System.Drawing.Size(61, 17);
            this.mmLabel10.TabIndex = 4;
            this.mmLabel10.Text = "Address:";
            // 
            // textBoxAddress1
            // 
            this.textBoxAddress1.BackColor = System.Drawing.Color.White;
            this.textBoxAddress1.CustomReportFieldName = "";
            this.textBoxAddress1.CustomReportKey = "";
            this.textBoxAddress1.CustomReportValueType = ((byte)(1));
            this.textBoxAddress1.IsComboTextBox = false;
            this.textBoxAddress1.IsModified = false;
            this.textBoxAddress1.Location = new System.Drawing.Point(148, 47);
            this.textBoxAddress1.MaxLength = 64;
            this.textBoxAddress1.Name = "textBoxAddress1";
            this.textBoxAddress1.Size = new System.Drawing.Size(274, 22);
            this.textBoxAddress1.TabIndex = 1;
            // 
            // mmLabel8
            // 
            this.mmLabel8.AutoSize = true;
            this.mmLabel8.BackColor = System.Drawing.Color.Transparent;
            this.mmLabel8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mmLabel8.IsFieldHeader = false;
            this.mmLabel8.IsRequired = false;
            this.mmLabel8.Location = new System.Drawing.Point(11, 25);
            this.mmLabel8.Name = "mmLabel8";
            this.mmLabel8.PenWidth = 1F;
            this.mmLabel8.ShowBorder = false;
            this.mmLabel8.Size = new System.Drawing.Size(79, 17);
            this.mmLabel8.TabIndex = 0;
            this.mmLabel8.Text = "Address ID:";
            // 
            // textBoxAddressID
            // 
            this.textBoxAddressID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxAddressID.CustomReportFieldName = "";
            this.textBoxAddressID.CustomReportKey = "";
            this.textBoxAddressID.CustomReportValueType = ((byte)(1));
            this.textBoxAddressID.Enabled = false;
            this.textBoxAddressID.ForeColor = System.Drawing.Color.Black;
            this.textBoxAddressID.IsComboTextBox = false;
            this.textBoxAddressID.IsModified = false;
            this.textBoxAddressID.Location = new System.Drawing.Point(148, 22);
            this.textBoxAddressID.MaxLength = 15;
            this.textBoxAddressID.Name = "textBoxAddressID";
            this.textBoxAddressID.Size = new System.Drawing.Size(274, 22);
            this.textBoxAddressID.TabIndex = 0;
            this.textBoxAddressID.Text = "PRIMARY";
            // 
            // ultraTabControl
            // 
            this.ultraTabControl.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabControl.Controls.Add(this.tabPageGeneral);
            this.ultraTabControl.Controls.Add(this.tabPageDetails);
            this.ultraTabControl.Controls.Add(this.tabPageUserDefined);
            this.ultraTabControl.Controls.Add(this.ultraTabPageControl1);
            this.ultraTabControl.Controls.Add(this.ultraTabPageControl2);
            this.ultraTabControl.Controls.Add(this.ultraTabPageControl3);
            this.ultraTabControl.Controls.Add(this.ultraTabPageControl4);
            this.ultraTabControl.Controls.Add(this.ultraTabPageControl5);
            this.ultraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraTabControl.Location = new System.Drawing.Point(0, 64);
            this.ultraTabControl.MinTabWidth = 80;
            this.ultraTabControl.Name = "ultraTabControl";
            this.ultraTabControl.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl.Size = new System.Drawing.Size(978, 616);
            this.ultraTabControl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
            this.ultraTabControl.TabIndex = 6;
            appearance189.BackColor = System.Drawing.Color.WhiteSmoke;
            ultraTab2.Appearance = appearance189;
            ultraTab2.TabPage = this.tabPageGeneral;
            ultraTab2.Text = "&General";
            ultraTab3.TabPage = this.ultraTabPageControl1;
            ultraTab3.Text = "&Renumeration";
            ultraTab4.TabPage = this.tabPageDetails;
            ultraTab4.Text = "&Recruitment";
            ultraTab4.Visible = false;
            ultraTab5.TabPage = this.ultraTabPageControl4;
            ultraTab5.Text = "Visa Process";
            ultraTab5.Visible = false;
            ultraTab6.TabPage = this.ultraTabPageControl2;
            ultraTab6.Text = "&Medical/Emirates";
            ultraTab6.Visible = false;
            ultraTab7.TabPage = this.ultraTabPageControl3;
            ultraTab7.Text = "&WP/RP";
            ultraTab7.Visible = false;
            ultraTab8.TabPage = this.tabPageUserDefined;
            ultraTab8.Text = "&User Defined";
            ultraTab8.Visible = false;
            ultraTab9.TabPage = this.ultraTabPageControl5;
            ultraTab9.Text = "Container";
            ultraTab9.Visible = false;
            this.ultraTabControl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab2,
            ultraTab3,
            ultraTab4,
            ultraTab5,
            ultraTab6,
            ultraTab7,
            ultraTab8,
            ultraTab9});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(974, 592);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.linePanelDown);
            this.panelButtons.Controls.Add(this.buttonDelete);
            this.panelButtons.Controls.Add(this.buttonClose);
            this.panelButtons.Controls.Add(this.buttonNew);
            this.panelButtons.Controls.Add(this.buttonSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 680);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(978, 46);
            this.panelButtons.TabIndex = 99;
            // 
            // linePanelDown
            // 
            this.linePanelDown.BackColor = System.Drawing.Color.White;
            this.linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.linePanelDown.DrawWidth = 1;
            this.linePanelDown.IsVertical = false;
            this.linePanelDown.LineBackColor = System.Drawing.Color.Silver;
            this.linePanelDown.Location = new System.Drawing.Point(0, 0);
            this.linePanelDown.Name = "linePanelDown";
            this.linePanelDown.Size = new System.Drawing.Size(978, 1);
            this.linePanelDown.TabIndex = 14;
            this.linePanelDown.TabStop = false;
            // 
            // buttonDelete
            // 
            this.buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonDelete.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonDelete.Location = new System.Drawing.Point(259, 9);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(115, 28);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "De&lete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Visible = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.Color.DarkGray;
            this.buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonClose.Location = new System.Drawing.Point(846, 9);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(115, 28);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.xpButton1_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonNew.BackColor = System.Drawing.Color.DarkGray;
            this.buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonNew.Location = new System.Drawing.Point(137, 9);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(115, 28);
            this.buttonNew.TabIndex = 1;
            this.buttonNew.Text = "Ne&w...";
            this.buttonNew.UseVisualStyleBackColor = false;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonSave.BackColor = System.Drawing.Color.Silver;
            this.buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSave.Location = new System.Drawing.Point(14, 9);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(116, 28);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "&Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonFirst,
            this.toolStripButtonPrevious,
            this.toolStripButtonNext,
            this.toolStripButtonLast,
            this.toolStripSeparator1,
            this.toolStripButtonOpenList,
            this.toolStripSeparator3,
            this.toolStripTextBoxFind,
            this.toolStripButtonFind,
            this.toolStripSeparator2,
            this.toolStripButtonAttach,
            this.toolStripButtonEmployee,
            this.toolStripSeparator4,
            this.toolStripButtonPrint,
            this.toolStripButtonPreview,
            this.toolStripButtonInformation,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(978, 31);
            this.toolStrip1.TabIndex = 306;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonFirst
            // 
            this.toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFirst.Image = global::Micromind.ClientUI.Properties.Resources.first;
            this.toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFirst.Name = "toolStripButtonFirst";
            this.toolStripButtonFirst.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonFirst.Text = "First";
            this.toolStripButtonFirst.Click += new System.EventHandler(this.toolStripButtonFirst_Click);
            // 
            // toolStripButtonPrevious
            // 
            this.toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrevious.Image = global::Micromind.ClientUI.Properties.Resources.prev;
            this.toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrevious.Name = "toolStripButtonPrevious";
            this.toolStripButtonPrevious.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonPrevious.Text = "Previous";
            this.toolStripButtonPrevious.Click += new System.EventHandler(this.toolStripButtonPrevious_Click);
            // 
            // toolStripButtonNext
            // 
            this.toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNext.Image = global::Micromind.ClientUI.Properties.Resources.next;
            this.toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNext.Name = "toolStripButtonNext";
            this.toolStripButtonNext.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonNext.Text = "Next";
            this.toolStripButtonNext.Click += new System.EventHandler(this.toolStripButtonNext_Click);
            // 
            // toolStripButtonLast
            // 
            this.toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLast.Image = global::Micromind.ClientUI.Properties.Resources.last;
            this.toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLast.Name = "toolStripButtonLast";
            this.toolStripButtonLast.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonLast.Text = "Last";
            this.toolStripButtonLast.Click += new System.EventHandler(this.toolStripButtonLast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonOpenList
            // 
            this.toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenList.Image = global::Micromind.ClientUI.Properties.Resources.list;
            this.toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenList.Name = "toolStripButtonOpenList";
            this.toolStripButtonOpenList.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonOpenList.Text = "Open List";
            this.toolStripButtonOpenList.Click += new System.EventHandler(this.toolStripButtonOpenList_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripTextBoxFind
            // 
            this.toolStripTextBoxFind.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBoxFind.Name = "toolStripTextBoxFind";
            this.toolStripTextBoxFind.Size = new System.Drawing.Size(120, 31);
            // 
            // toolStripButtonFind
            // 
            this.toolStripButtonFind.Image = global::Micromind.ClientUI.Properties.Resources.find;
            this.toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFind.Name = "toolStripButtonFind";
            this.toolStripButtonFind.Size = new System.Drawing.Size(65, 28);
            this.toolStripButtonFind.Text = "Find";
            this.toolStripButtonFind.Click += new System.EventHandler(this.toolStripButtonFind_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonAttach
            // 
            this.toolStripButtonAttach.Image = global::Micromind.ClientUI.Properties.Resources.attach_24x24;
            this.toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAttach.Name = "toolStripButtonAttach";
            this.toolStripButtonAttach.Size = new System.Drawing.Size(107, 28);
            this.toolStripButtonAttach.Text = "Attach File";
            this.toolStripButtonAttach.Click += new System.EventHandler(this.toolStripButtonAttach_Click);
            // 
            // toolStripButtonEmployee
            // 
            this.toolStripButtonEmployee.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEmployee.Image")));
            this.toolStripButtonEmployee.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEmployee.Name = "toolStripButtonEmployee";
            this.toolStripButtonEmployee.Size = new System.Drawing.Size(143, 28);
            this.toolStripButtonEmployee.Text = "Make Employee";
            this.toolStripButtonEmployee.Visible = false;
            this.toolStripButtonEmployee.Click += new System.EventHandler(this.toolStripBtnMakeEmployee_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripButtonPrint
            // 
            this.toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrint.Image = global::Micromind.ClientUI.Properties.Resources.printer;
            this.toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonPrint.Text = "&Print";
            this.toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // toolStripButtonPreview
            // 
            this.toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPreview.Image = global::Micromind.ClientUI.Properties.Resources.preview;
            this.toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPreview.Name = "toolStripButtonPreview";
            this.toolStripButtonPreview.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonPreview.Text = "Preview";
            this.toolStripButtonPreview.ToolTipText = "Preview";
            this.toolStripButtonPreview.Click += new System.EventHandler(this.toolStripButtonPreview_Click);
            // 
            // toolStripButtonInformation
            // 
            this.toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInformation.Image = global::Micromind.ClientUI.Properties.Resources.docinfo_24x24;
            this.toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInformation.Name = "toolStripButtonInformation";
            this.toolStripButtonInformation.Size = new System.Drawing.Size(29, 28);
            this.toolStripButtonInformation.Text = "Document Information";
            this.toolStripButtonInformation.Click += new System.EventHandler(this.toolStripButtonInformation_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.morePrintToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(72, 28);
            this.toolStripDropDownButton1.Text = "Actions";
            // 
            // morePrintToolStripMenuItem
            // 
            this.morePrintToolStripMenuItem.Name = "morePrintToolStripMenuItem";
            this.morePrintToolStripMenuItem.Size = new System.Drawing.Size(161, 26);
            this.morePrintToolStripMenuItem.Text = "More Print";
            this.morePrintToolStripMenuItem.Click += new System.EventHandler(this.morePrintToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelCancelled);
            this.panel1.Controls.Add(this.labelCategory);
            this.panel1.Controls.Add(this.labelCustomerNameHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.MinimumSize = new System.Drawing.Size(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 33);
            this.panel1.TabIndex = 314;
            // 
            // labelCancelled
            // 
            this.labelCancelled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCancelled.BackColor = System.Drawing.Color.White;
            this.labelCancelled.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCancelled.ForeColor = System.Drawing.Color.DarkRed;
            this.labelCancelled.Location = new System.Drawing.Point(820, 3);
            this.labelCancelled.Name = "labelCancelled";
            this.labelCancelled.Size = new System.Drawing.Size(152, 27);
            this.labelCancelled.TabIndex = 148;
            this.labelCancelled.Text = "CANCELLED";
            this.labelCancelled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCancelled.Visible = false;
            // 
            // labelCategory
            // 
            this.labelCategory.AutoSize = true;
            this.labelCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCategory.Location = new System.Drawing.Point(660, 9);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(0, 17);
            this.labelCategory.TabIndex = 3;
            // 
            // labelCustomerNameHeader
            // 
            this.labelCustomerNameHeader.AutoSize = true;
            this.labelCustomerNameHeader.BackColor = System.Drawing.Color.Transparent;
            this.labelCustomerNameHeader.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(122)))), ((int)(((byte)(171)))));
            this.labelCustomerNameHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelCustomerNameHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCustomerNameHeader.IsFieldHeader = false;
            this.labelCustomerNameHeader.IsRequired = true;
            this.labelCustomerNameHeader.Location = new System.Drawing.Point(35, 8);
            this.labelCustomerNameHeader.Name = "labelCustomerNameHeader";
            this.labelCustomerNameHeader.PenWidth = 1F;
            this.labelCustomerNameHeader.ShowBorder = false;
            this.labelCustomerNameHeader.Size = new System.Drawing.Size(0, 17);
            this.labelCustomerNameHeader.TabIndex = 2;
            this.labelCustomerNameHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dependentsToolStripMenuItem,
            this.documentsToolStripMenuItem,
            this.skillsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 76);
            // 
            // dependentsToolStripMenuItem
            // 
            this.dependentsToolStripMenuItem.Name = "dependentsToolStripMenuItem";
            this.dependentsToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.dependentsToolStripMenuItem.Text = "Dependents";
            this.dependentsToolStripMenuItem.Click += new System.EventHandler(this.dependentsToolStripMenuItem_Click);
            // 
            // documentsToolStripMenuItem
            // 
            this.documentsToolStripMenuItem.Name = "documentsToolStripMenuItem";
            this.documentsToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.documentsToolStripMenuItem.Text = "Documents";
            this.documentsToolStripMenuItem.Click += new System.EventHandler(this.documentsToolStripMenuItem_Click);
            // 
            // skillsToolStripMenuItem
            // 
            this.skillsToolStripMenuItem.Name = "skillsToolStripMenuItem";
            this.skillsToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.skillsToolStripMenuItem.Text = "Skills";
            this.skillsToolStripMenuItem.Click += new System.EventHandler(this.skillsToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "JPG";
            this.openFileDialog1.Filter = "Picture Files|*.jpg";
            // 
            // formManager
            // 
            this.formManager.BackColor = System.Drawing.Color.RosyBrown;
            this.formManager.IsForcedDirty = false;
            this.formManager.Location = new System.Drawing.Point(0, 29);
            this.formManager.MaximumSize = new System.Drawing.Size(20, 20);
            this.formManager.MinimumSize = new System.Drawing.Size(20, 20);
            this.formManager.Name = "formManager";
            this.formManager.Size = new System.Drawing.Size(20, 20);
            this.formManager.TabIndex = 307;
            this.formManager.Text = "formManager1";
            this.formManager.Visible = false;
            // 
            // AppointmentDetailsForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(978, 726);
            this.Controls.Add(this.ultraTabControl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.formManager);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panelButtons);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(996, 773);
            this.Name = "AppointmentDetailsForm";
            this.Text = "Appointment Detail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CandidateClassDetailsForm_FormClosing);
            this.Load += new System.EventHandler(this.CandidateDetailsForm_Load);
            this.ultraTabPageControl6.ResumeLayout(false);
            this.ultraTabPageControl6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPayrollItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPayrollItem)).EndInit();
            this.tabPageGeneral.ResumeLayout(false);
            this.panelGeneral.ResumeLayout(false);
            this.panelGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxDesignation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNoImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxReligion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxNationality)).EndInit();
            this.ultraTabPageControl1.ResumeLayout(false);
            this.panelArrival.ResumeLayout(false);
            this.panelArrival.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sponsorComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
            this.tabPageDetails.ResumeLayout(false);
            this.panelRecruitment.ResumeLayout(false);
            this.panelRecruitment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxArrivalPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericExperienceAbroad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericExperienceLocal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxQualification)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPositionActual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAgentThrough)).EndInit();
            this.ultraTabPageControl4.ResumeLayout(false);
            this.panelVisaProcess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelVisaIMG)).EndInit();
            this.panelVisaIMG.ResumeLayout(false);
            this.panelVisaIMG.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelVisaMOL)).EndInit();
            this.panelVisaMOL.ResumeLayout(false);
            this.panelVisaMOL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxPositionVisa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxSponsor)).EndInit();
            this.ultraTabPageControl2.ResumeLayout(false);
            this.panelMedicalEmirates.ResumeLayout(false);
            this.panelMedicalEmirates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelEmirates)).EndInit();
            this.panelEmirates.ResumeLayout(false);
            this.panelEmirates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelMedicalDetail)).EndInit();
            this.panelMedicalDetail.ResumeLayout(false);
            this.panelMedicalDetail.PerformLayout();
            this.ultraTabPageControl3.ResumeLayout(false);
            this.panelWPRP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelMedicalReport)).EndInit();
            this.panelMedicalReport.ResumeLayout(false);
            this.panelMedicalReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelAGT)).EndInit();
            this.panelAGT.ResumeLayout(false);
            this.panelAGT.PerformLayout();
            this.tabPageUserDefined.ResumeLayout(false);
            this.ultraTabPageControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private void Init()
		{
			AddEvents();
		}

		private void AddEvents()
		{
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
			textBoxGivenName.TextChanged += textBoxCode_TextChanged;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
			textBoxEmployeeNo.TextChanged += textBoxEmployeeNo_TextChanged;
			textBoxNationalID.TextChanged += textBoxNationalID_TextChanged;
			comboBoxSelectionStatus.SelectedIndexChanged += comboBoxSelectionStatus_SelectedIndexChanged;
			dateTimeBirthDate.ValueChanged += dateTimeBirthDate_ValueChanged;
			textBoxPassportNo.Leave += textBoxPassportNo_Leave;
			textBoxPassportNo.KeyPress += textBoxPassportNo_KeyPress;
			dateTimeApplTypingDateEID.ValueChanged += dateTimeApplTypingDateEID_ValueChanged;
			dateTimeArrivedOn.ValueChanged += dateTimeArrivedOn_ValueChanged;
			dateTimeApprovalDateMOL.ValueChanged += dateTimeApprovalDateMOL_ValueChanged;
			dateTimeVisaIssueDate.ValueChanged += dateTimeVisaIssueDate_ValueChanged;
			dateTimeWPIssueDate.ValueChanged += dateTimeWPIssueDate_ValueChanged;
			comboBoxCategory.SelectedIndexChanged += comboBoxCategory_SelectedIndexChanged;
			dataGridPayrollItem.CellDataError += dataGrid_CellDataError;
			dataGridPayrollItem.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridPayrollItem.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridPayrollItem.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			comboBoxPayrollItem.SelectedIndexChanged += comboBoxPayrollItem_SelectedIndexChanged;
			dataGridPayrollItem.AfterCellUpdate += dataGridPayrollItem_AfterCellUpdate;
			dataGridPayrollItem.HeaderClicked += dataGridPayrollItem_HeaderClicked;
		}

		private void dataGridPayrollItem_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (ultraGridColumn != null && ultraGridColumn.Key == "PayrollItem")
			{
				string id = "";
				if (dataGridPayrollItem.ActiveRow != null)
				{
					id = dataGridPayrollItem.ActiveRow.Cells["PayrollItem"].Text;
				}
				new FormHelper().EditPayrollItem(id);
			}
		}

		private void dataGridPayrollItem_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Amount")
			{
				ShowTotalSalary();
			}
			else if (e.Cell.Column.Key == "PayrollItem")
			{
				dataGridPayrollItem.ActiveRow.Cells["Description"].Value = comboBoxPayrollItem.SelectedName;
			}
		}

		private void ShowTotalSalary()
		{
			decimal num = default(decimal);
			foreach (UltraGridRow row in dataGridPayrollItem.Rows)
			{
				decimal result = default(decimal);
				decimal.TryParse(row.Cells["Amount"].Text, out result);
				num += result;
			}
			textBoxTotalSalary.Text = num.ToString(Format.TotalAmountFormat);
		}

		private void comboBoxPayrollItem_SelectedIndexChanged(object sender, EventArgs e)
		{
			_ = dataGridPayrollItem.ActiveRow;
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridPayrollItem.ActiveCell.Column.Key.ToString() == "PayrollItem")
			{
				e.RaiseErrorEvent = false;
				comboBoxPayrollItem.Text = dataGridPayrollItem.ActiveCell.Text;
				comboBoxPayrollItem.QuickAddItem();
			}
			else if (dataGridPayrollItem.ActiveCell.Column.Key.ToString() == "Amount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please enter a numeric value greater or equal to zero.");
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridPayrollItem.ActiveRow;
			if (activeRow != null && activeRow.Cells["PayrollItem"].Value.ToString() == "")
			{
				ErrorHelper.InformationMessage("Please select an payrollItem.");
				e.Cancel = true;
				activeRow.Cells["PayrollItem"].Activate();
			}
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridPayrollItem.ActiveCell.Column.Key == "PayrollItem")
			{
				foreach (UltraGridRow row in dataGridPayrollItem.Rows)
				{
					if (row.Index != dataGridPayrollItem.ActiveRow.Index && !(row.Cells["PayrollItem"].Value.ToString() == "") && row.Cells["PayrollItem"].Value.ToString() == dataGridPayrollItem.ActiveCell.Value.ToString())
					{
						ErrorHelper.InformationMessage("This payrollItem is already selected for this employee.");
						e.Cancel = true;
						break;
					}
				}
			}
			else if (dataGridPayrollItem.ActiveCell.Column.Key == "Amount")
			{
				if (dataGridPayrollItem.ActiveCell.Text == "")
				{
					dataGridPayrollItem.ActiveCell.Value = 0.ToString(Format.TotalAmountFormat);
				}
				else
				{
					dataGridPayrollItem.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridPayrollItem.ActiveCell.Text, NumberStyles.Any), 2).ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			labelCategory.Text = comboBoxCategory.SelectedName;
		}

		private void dateTimeWPIssueDate_ValueChanged(object sender, EventArgs e)
		{
			dateTimeWPExpiryDate.Checked = dateTimeWPIssueDate.Checked;
			if (dateTimeWPIssueDate.Checked)
			{
				dateTimeWPExpiryDate.Value = dateTimeWPIssueDate.Value.AddYears(2);
			}
		}

		private void dateTimeVisaIssueDate_ValueChanged(object sender, EventArgs e)
		{
			dateTimeVisaExpiryDate.Checked = dateTimeVisaIssueDate.Checked;
			if (dateTimeVisaIssueDate.Checked)
			{
				dateTimeVisaExpiryDate.Value = dateTimeVisaIssueDate.Value.AddMonths(2);
			}
		}

		private void dateTimeApprovalDateMOL_ValueChanged(object sender, EventArgs e)
		{
			dateTimeApprovalValidTillMOL.Checked = dateTimeApprovalDateMOL.Checked;
			if (dateTimeApprovalDateMOL.Checked)
			{
				dateTimeApprovalValidTillMOL.Value = dateTimeApprovalDateMOL.Value.AddMonths(2);
			}
		}

		public void EnableDisableWorkFlowScreen(WorkflowType workFlowType)
		{
			switch (workFlowType)
			{
			case WorkflowType.General:
				panelGeneral.Enabled = true;
				break;
			case WorkflowType.Recruitment:
				panelRecruitment.Enabled = true;
				break;
			case WorkflowType.VisaProcess:
				panelVisaProcess.Enabled = true;
				break;
			case WorkflowType.Arrival:
				panelArrival.Enabled = true;
				break;
			case WorkflowType.Medical_Emirates:
				panelMedicalEmirates.Enabled = true;
				break;
			case WorkflowType.WP_RP:
				panelWPRP.Enabled = true;
				udfEntryGrid.Enabled = true;
				break;
			}
			if ((int)workFlowType < ultraTabControl.Tabs.Count)
			{
				ultraTabControl.Tabs[(int)workFlowType].Selected = true;
			}
		}

		private void sponsorComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (dateTimeArrivedOn.Checked)
			{
				if (sponsorComboBox.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Select Sponsor");
				}
				else if (!isExist)
				{
					textBoxEmployeeNo.Text = GetNextEmployeeNumber();
				}
			}
			else if (!isExist)
			{
				textBoxEmployeeNo.Clear();
			}
			Console.WriteLine("");
		}

		private void dateTimeArrivedOn_ValueChanged(object sender, EventArgs e)
		{
			if (dateTimeArrivedOn.Checked)
			{
				if (sponsorComboBox.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Select Sponsor");
				}
				else if (!isExist)
				{
					textBoxEmployeeNo.Text = GetNextEmployeeNumber();
				}
			}
			else if (!isExist)
			{
				textBoxEmployeeNo.Clear();
			}
			Console.WriteLine("");
		}

		private void dateTimeApplTypingDateEID_ValueChanged(object sender, EventArgs e)
		{
			if (dateTimeApplTypingDateEID.Checked)
			{
				UltraGroupBox ultraGroupBox = panelMedicalReport;
				bool enabled = panelAGT.Enabled = true;
				ultraGroupBox.Enabled = enabled;
			}
			else
			{
				UltraGroupBox ultraGroupBox2 = panelMedicalReport;
				bool enabled = panelAGT.Enabled = false;
				ultraGroupBox2.Enabled = enabled;
			}
		}

		private void textBoxPassportNo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (char.IsWhiteSpace(e.KeyChar) || char.IsPunctuation(e.KeyChar))
			{
				e.Handled = true;
			}
			else
			{
				e.Handled = false;
			}
		}

		private void textBoxPassportNo_Leave(object sender, EventArgs e)
		{
			if (!(textBoxPassportNo.Text.Trim() == string.Empty) && isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Candidate", "PassportNo", textBoxPassportNo.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Passport Number already exist.");
				tabPageGeneral.Tab.Selected = true;
			}
		}

		private void textBoxNationalID_TextChanged(object sender, EventArgs e)
		{
			if (textBoxNationalID.Text.Trim().Length > 0)
			{
				UltraGroupBox ultraGroupBox = panelMedicalDetail;
				bool enabled = panelMedicalReport.Enabled = true;
				ultraGroupBox.Enabled = enabled;
			}
			else
			{
				UltraGroupBox ultraGroupBox2 = panelMedicalDetail;
				bool enabled = panelMedicalReport.Enabled = false;
				ultraGroupBox2.Enabled = enabled;
			}
		}

		private void textBoxEmployeeNo_TextChanged(object sender, EventArgs e)
		{
			if (textBoxEmployeeNo.Text.Trim().Length > 0)
			{
				panelEmirates.Enabled = true;
			}
			else
			{
				panelEmirates.Enabled = false;
			}
		}

		private void dateTimeBirthDate_ValueChanged(object sender, EventArgs e)
		{
			if (!dateTimeBirthDate.Checked)
			{
				textBoxAge.Clear();
				return;
			}
			DateTime value = dateTimeBirthDate.Value;
			TimeSpan timeSpan = DateTime.Today - value;
			int num = 0;
			if (timeSpan.Days > 0)
			{
				num = timeSpan.Days / 365;
			}
			if (num > 0)
			{
				textBoxAge.Text = num.ToString() + " Years";
			}
			else
			{
				textBoxAge.Clear();
			}
		}

		private void comboBoxSelectionStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxSelectionStatus.SelectedID == 3 || comboBoxSelectionStatus.SelectedID == 4)
			{
				UltraGroupBox ultraGroupBox = panelVisaMOL;
				bool enabled = panelVisaIMG.Enabled = true;
				ultraGroupBox.Enabled = enabled;
			}
			else
			{
				UltraGroupBox ultraGroupBox2 = panelVisaMOL;
				bool enabled = panelVisaIMG.Enabled = false;
				ultraGroupBox2.Enabled = enabled;
			}
		}

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private void textBoxLastName_TextChanged(object sender, EventArgs e)
		{
			SetHeaderName();
		}

		private void textBoxCode_TextChanged(object sender, EventArgs e)
		{
			SetHeaderName();
		}

		private void SetHeaderName()
		{
			labelCustomerNameHeader.Text = textBoxCode.Text + " - " + textBoxGivenName.Text + " " + textBoxSurName.Text + " " + textBoxFatherName.Text;
			if (textBoxCode.Text.Trim() == "" && textBoxGivenName.Text.Trim() == "" && textBoxSurName.Text == "" && textBoxFatherName.Text == "")
			{
				labelCustomerNameHeader.Text = "";
			}
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void comboBoxAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void EventHelper_CandidateAddressChanged(object sender, EventArgs e)
		{
			DataSet dataSet = sender as DataSet;
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				_ = dataSet.Tables[0].Rows[0];
			}
		}

		private void CandidateDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxGender.LoadData();
				comboBoxMaritalStatus.LoadData();
				comboBoxSelectionStatus.LoadData();
				comboBoxAgentThrough.ShowQuickAdd = true;
				comboBoxAgentThrough.LoadData();
				comboBoxvisaType.LoadData();
				SetSecurity();
				dataGridPayrollItem.SetupUI();
				SetupPayrollItemGrid();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					Init();
					ClearForm();
					textBoxCode.Focus();
				}
			}
			catch (Exception e2)
			{
				dataGridPayrollItem.LoadLayoutFailed = true;
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

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.CandidateTable.Rows[0];
				textBoxCode.Text = "VS" + dataRow["CandidateID"].ToString();
				textBoxPassportNo.Text = dataRow["PassportNo"].ToString();
				textBoxGivenName.Text = dataRow["GivenName"].ToString();
				textBoxSurName.Text = dataRow["SurName"].ToString();
				comboBoxNationality.SelectedID = dataRow["NationalityID"].ToString();
				comboBoxGender.SelectedGender = char.Parse(dataRow["Gender"].ToString());
				if (dataRow["BirthDate"] != DBNull.Value)
				{
					dateTimeBirthDate.Value = DateTime.Parse(dataRow["BirthDate"].ToString());
					dateTimeBirthDate.Checked = true;
				}
				else
				{
					dateTimeBirthDate.Checked = false;
				}
				textBoxBirthPlace.Text = dataRow["BirthPlace"].ToString();
				textBoxPPIssuePlace.Text = dataRow["PassportPlaceOfIssue"].ToString();
				if (dataRow["PassportIssueDate"] != DBNull.Value)
				{
					dateTimePPIssueDate.Value = DateTime.Parse(dataRow["PassportIssueDate"].ToString());
					dateTimePPIssueDate.Checked = true;
				}
				else
				{
					dateTimePPIssueDate.Checked = false;
				}
				if (dataRow["PassportExpiryDate"] != DBNull.Value)
				{
					dateTimePPExpiryDate.Value = DateTime.Parse(dataRow["PassportExpiryDate"].ToString());
					dateTimePPExpiryDate.Checked = true;
				}
				else
				{
					dateTimePPExpiryDate.Checked = false;
				}
				textBoxFatherName.Text = dataRow["FatherName"].ToString();
				textBoxMotherName.Text = dataRow["MotherName"].ToString();
				textBoxSpouseName.Text = dataRow["SpouseName"].ToString();
				textBoxPPAddress.Text = dataRow["PassportAddress"].ToString();
				textBoxNote.Text = dataRow["Notes"].ToString();
				if (dataRow["SelectionStatus"] != DBNull.Value)
				{
					comboBoxSelectionStatus.SelectedID = int.Parse(dataRow["SelectionStatus"].ToString());
				}
				else
				{
					comboBoxSelectionStatus.SelectedID = -1;
				}
				if (dataRow["SelectedOn"] != DBNull.Value)
				{
					dateTimeSelectedOn.Value = DateTime.Parse(dataRow["SelectedOn"].ToString());
					dateTimeSelectedOn.Checked = true;
				}
				else
				{
					dateTimeSelectedOn.Checked = false;
				}
				textBoxSelectedAt.Text = dataRow["SelectedAt"].ToString();
				if (dataRow["ThroughAgent"] != DBNull.Value)
				{
					comboBoxAgentThrough.SelectedID = dataRow["ThroughAgent"].ToString();
				}
				else
				{
					comboBoxAgentThrough.Clear();
				}
				comboBoxPositionActual.SelectedID = dataRow["ActualDesignation"].ToString();
				comboBoxDesignation.SelectedID = dataRow["DesignationID"].ToString();
				comboBoxvisaType.SelectedID = byte.Parse(dataRow["VisaSelectStatus"].ToString());
				textBoxRemarks.Text = dataRow["Remarks"].ToString();
				comboBoxQualification.SelectedID = dataRow["QualificationID"].ToString();
				comboBoxLanguage.SelectedID = dataRow["LanguageID"].ToString();
				numericExperienceLocal.Text = dataRow["ExperienceLocal"].ToString();
				numericExperienceAbroad.Text = dataRow["ExperienceAbroad"].ToString();
				if (dataRow["ApplicationTypingDateMOL"] != DBNull.Value)
				{
					dateTimeApplTypingDateMOL.Value = DateTime.Parse(dataRow["ApplicationTypingDateMOL"].ToString());
					dateTimeApplTypingDateMOL.Checked = true;
				}
				else
				{
					dateTimeApplTypingDateMOL.Checked = false;
				}
				textBoxMOLMBNo.Text = dataRow["MBNumberMOL"].ToString();
				comboBoxSponsor.SelectedID = dataRow["SponsorID"].ToString();
				comboBoxGroup.SelectedID = dataRow["GroupID"].ToString();
				comboBoxPositionVisa.SelectedID = dataRow["VisaDesignation"].ToString();
				if (dataRow["ApprovalDateMOL"] != DBNull.Value)
				{
					dateTimeApprovalDateMOL.Value = DateTime.Parse(dataRow["ApprovalDateMOL"].ToString());
					dateTimeApprovalDateMOL.Checked = true;
				}
				else
				{
					dateTimeApprovalDateMOL.Checked = false;
				}
				if (dataRow["ApprovalValidTillMOL"] != DBNull.Value)
				{
					dateTimeApprovalValidTillMOL.Value = DateTime.Parse(dataRow["ApprovalValidTillMOL"].ToString());
					dateTimeApprovalValidTillMOL.Checked = true;
				}
				else
				{
					dateTimeApprovalValidTillMOL.Checked = false;
				}
				textBoxTempWPNo.Text = dataRow["TempWPNumber"].ToString();
				if (dataRow["ApprovalFeePaidOnMOL"] != DBNull.Value)
				{
					dateTimeApprovalFeePaidOnMOL.Value = DateTime.Parse(dataRow["ApprovalFeePaidOnMOL"].ToString());
					dateTimeApprovalFeePaidOnMOL.Checked = true;
				}
				else
				{
					dateTimeApprovalFeePaidOnMOL.Checked = false;
				}
				if (dataRow["BGPaidOnMOL"] != DBNull.Value)
				{
					dateTimeBGPaidOnMOL.Value = DateTime.Parse(dataRow["BGPaidOnMOL"].ToString());
					dateTimeBGPaidOnMOL.Checked = true;
				}
				else
				{
					dateTimeBGPaidOnMOL.Checked = false;
				}
				if (dataRow["BGTypeMOL"] != DBNull.Value)
				{
					comboBoxBGTypeMOL.SelectedItem = dataRow["BGTypeMOL"].ToString();
				}
				else
				{
					comboBoxBGTypeMOL.SelectedIndex = -1;
				}
				if (dataRow["VisaAppliedThroughIMG"] != DBNull.Value)
				{
					comboBoxVisaAppliedThroughIMG.SelectedItem = dataRow["VisaAppliedThroughIMG"].ToString();
				}
				else
				{
					comboBoxVisaAppliedThroughIMG.SelectedIndex = -1;
				}
				if (dataRow["VisaPostedOnIMG"] != DBNull.Value)
				{
					dateTimeVisaPostedOn.Value = DateTime.Parse(dataRow["VisaPostedOnIMG"].ToString());
					dateTimeVisaPostedOn.Checked = true;
				}
				else
				{
					dateTimeVisaPostedOn.Checked = false;
				}
				if (dataRow["VisaApprovedOnIMG"] != DBNull.Value)
				{
					dateTimeApprovedOn.Value = DateTime.Parse(dataRow["VisaApprovedOnIMG"].ToString());
					dateTimeApprovedOn.Checked = true;
				}
				else
				{
					dateTimeApprovedOn.Checked = false;
				}
				textBoxVisaIssuePlaceIMG.Text = dataRow["VisaIssuePlaceIMG"].ToString();
				textBoxVisaNumber.Text = dataRow["VisaNumber"].ToString();
				if (dataRow["VisaIssueDateIMG"] != DBNull.Value)
				{
					dateTimeVisaIssueDate.Value = DateTime.Parse(dataRow["VisaIssueDateIMG"].ToString());
					dateTimeVisaIssueDate.Checked = true;
				}
				else
				{
					dateTimeVisaIssueDate.Checked = false;
				}
				if (dataRow["VisaExpiryDateIMG"] != DBNull.Value)
				{
					dateTimeVisaExpiryDate.Value = DateTime.Parse(dataRow["VisaExpiryDateIMG"].ToString());
					dateTimeVisaExpiryDate.Checked = true;
				}
				else
				{
					dateTimeVisaExpiryDate.Checked = false;
				}
				textBoxUIDNumberIMG.Text = dataRow["UIDNumberIMG"].ToString();
				if (dataRow["VisaCopyToAgentOn"] != DBNull.Value)
				{
					dateTimeVisaCopyToAgentOn.Value = DateTime.Parse(dataRow["VisaCopyToAgentOn"].ToString());
					dateTimeVisaCopyToAgentOn.Checked = true;
				}
				else
				{
					dateTimeVisaCopyToAgentOn.Checked = false;
				}
				textBoxEmployeeNo.Text = dataRow["EmployeeNo"].ToString();
				IsExist = bool.Parse(dataRow["IsExist"].ToString());
				sponsorComboBox.SelectedID = dataRow["SponsorID"].ToString();
				if (dataRow["ArrivedOn"] != DBNull.Value)
				{
					dateTimeArrivedOn.Value = DateTime.Parse(dataRow["ArrivedOn"].ToString());
					dateTimeArrivedOn.Checked = true;
				}
				else
				{
					dateTimeArrivedOn.Checked = false;
				}
				if (dataRow["ArrivalPort"] != DBNull.Value)
				{
					comboBoxArrivalPort.SelectedID = dataRow["ArrivalPort"].ToString();
				}
				else
				{
					comboBoxArrivalPort.Clear();
				}
				if (dataRow["Category"] != DBNull.Value)
				{
					comboBoxCategory.SelectedID = dataRow["Category"].ToString();
				}
				else
				{
					comboBoxCategory.Clear();
				}
				textBoxHealthCardNo.Text = dataRow["HealthCardNo"].ToString();
				if (dataRow["MedicalTypingOn"] != DBNull.Value)
				{
					dateTimeMedicalTypingOn.Value = DateTime.Parse(dataRow["MedicalTypingOn"].ToString());
					dateTimeMedicalTypingOn.Checked = true;
				}
				else
				{
					dateTimeMedicalTypingOn.Checked = false;
				}
				if (dataRow["MedicalAttendedOn"] != DBNull.Value)
				{
					dateTimeMedicalAttendedOn.Value = DateTime.Parse(dataRow["MedicalAttendedOn"].ToString());
					dateTimeMedicalAttendedOn.Checked = true;
				}
				else
				{
					dateTimeMedicalAttendedOn.Checked = false;
				}
				if (dataRow["MedicalCollectedOn"] != DBNull.Value)
				{
					dateTimeMedicalCollectedOn.Value = DateTime.Parse(dataRow["MedicalCollectedOn"].ToString());
					dateTimeMedicalCollectedOn.Checked = true;
				}
				else
				{
					dateTimeMedicalCollectedOn.Checked = false;
				}
				if (dataRow["MedicalResult"] != DBNull.Value)
				{
					comboBoxMedicalResult.SelectedItem = dataRow["MedicalResult"].ToString();
				}
				else
				{
					comboBoxMedicalResult.SelectedIndex = -1;
				}
				textBoxMedicalNote.Text = dataRow["MedicalNote"].ToString();
				if (dataRow["ApplicationTypedOnEID"] != DBNull.Value)
				{
					dateTimeApplTypingDateEID.Value = DateTime.Parse(dataRow["ApplicationTypedOnEID"].ToString());
					dateTimeApplTypingDateEID.Checked = true;
				}
				else
				{
					dateTimeApplTypingDateEID.Checked = false;
				}
				if (dataRow["AttendedForEID"] != DBNull.Value)
				{
					dateTimeAttendedDateEID.Value = DateTime.Parse(dataRow["AttendedForEID"].ToString());
					dateTimeAttendedDateEID.Checked = true;
				}
				else
				{
					dateTimeAttendedDateEID.Checked = false;
				}
				if (dataRow["CollectedOnEID"] != DBNull.Value)
				{
					dateTimeCollectedOnEID.Value = DateTime.Parse(dataRow["CollectedOnEID"].ToString());
					dateTimeCollectedOnEID.Checked = true;
				}
				else
				{
					dateTimeCollectedOnEID.Checked = false;
				}
				textBoxNationalID.Text = dataRow["NationalID"].ToString();
				if (dataRow["NationalIDValidity"] != DBNull.Value)
				{
					dateTimeValidityEID.Value = DateTime.Parse(dataRow["NationalIDValidity"].ToString());
					dateTimeValidityEID.Checked = true;
				}
				else
				{
					dateTimeValidityEID.Checked = false;
				}
				if (dataRow["AGTType"] != DBNull.Value)
				{
					comboBoxAGTType.SelectedItem = dataRow["AGTType"].ToString();
				}
				else
				{
					comboBoxAGTType.SelectedIndex = -1;
				}
				textBoxAGTMBNo.Text = dataRow["MBNumberAGT"].ToString();
				if (dataRow["AGTTypedOn"] != DBNull.Value)
				{
					dateTimeAGTTypedOn.Value = DateTime.Parse(dataRow["AGTTypedOn"].ToString());
					dateTimeAGTTypedOn.Checked = true;
				}
				else
				{
					dateTimeAGTTypedOn.Checked = false;
				}
				if (dataRow["AGTSubmittedOn"] != DBNull.Value)
				{
					dateTimeAGTSubmittedOn.Value = DateTime.Parse(dataRow["AGTSubmittedOn"].ToString());
					dateTimeAGTSubmittedOn.Checked = true;
				}
				else
				{
					dateTimeAGTSubmittedOn.Checked = false;
				}
				textBoxWPNo.Text = dataRow["WPNumber"].ToString();
				textBoxPersonIDNo.Text = dataRow["PersonalIDNo"].ToString();
				textBoxWPIssuePlace.Text = dataRow["WPIssuePlace"].ToString();
				if (dataRow["WPIssueDate"] != DBNull.Value)
				{
					dateTimeWPIssueDate.Value = DateTime.Parse(dataRow["WPIssueDate"].ToString());
					dateTimeWPIssueDate.Checked = true;
				}
				else
				{
					dateTimeWPIssueDate.Checked = false;
				}
				if (dataRow["WPExpiryDate"] != DBNull.Value)
				{
					dateTimeWPExpiryDate.Value = DateTime.Parse(dataRow["WPExpiryDate"].ToString());
					dateTimeWPExpiryDate.Checked = true;
				}
				else
				{
					dateTimeWPExpiryDate.Checked = false;
				}
				if (dataRow["RPProcessType"] != DBNull.Value)
				{
					comboBoxProcessType.SelectedItem = dataRow["RPProcessType"].ToString();
				}
				else
				{
					comboBoxProcessType.SelectedIndex = -1;
				}
				if (dataRow["ApplicationPostedOnRP"] != DBNull.Value)
				{
					dateTimeApplPostedOnRP.Value = DateTime.Parse(dataRow["ApplicationPostedOnRP"].ToString());
					dateTimeApplPostedOnRP.Checked = true;
				}
				else
				{
					dateTimeApplPostedOnRP.Checked = false;
				}
				if (dataRow["ApplicationApprovedOnRP"] != DBNull.Value)
				{
					dateTimeApplApprovedOnRP.Value = DateTime.Parse(dataRow["ApplicationApprovedOnRP"].ToString());
					dateTimeApplApprovedOnRP.Checked = true;
				}
				else
				{
					dateTimeApplApprovedOnRP.Checked = false;
				}
				if (dataRow["SubmittedToZajil"] != DBNull.Value)
				{
					dateTimeSubmittedZajilOnRP.Value = DateTime.Parse(dataRow["SubmittedToZajil"].ToString());
					dateTimeSubmittedZajilOnRP.Checked = true;
				}
				else
				{
					dateTimeSubmittedZajilOnRP.Checked = false;
				}
				if (dataRow["PassportCollectedOn"] != DBNull.Value)
				{
					dateTimePassportCollectedOnRP.Value = DateTime.Parse(dataRow["PassportCollectedOn"].ToString());
					dateTimePassportCollectedOnRP.Checked = true;
				}
				else
				{
					dateTimePassportCollectedOnRP.Checked = false;
				}
				textBoxRPIssuePlace.Text = dataRow["RPIssuePlace"].ToString();
				if (dataRow["RPIssueDate"] != DBNull.Value)
				{
					dateTimeRPIssueDate.Value = DateTime.Parse(dataRow["RPIssueDate"].ToString());
					dateTimeRPIssueDate.Checked = true;
				}
				else
				{
					dateTimeRPIssueDate.Checked = false;
				}
				if (dataRow["RPExpiryDate"] != DBNull.Value)
				{
					dateTimeRPExpiryDate.Value = DateTime.Parse(dataRow["RPExpiryDate"].ToString());
					dateTimeRPExpiryDate.Checked = true;
				}
				else
				{
					dateTimeRPExpiryDate.Checked = false;
				}
				if (dataRow["Photo"] != DBNull.Value)
				{
					UltraFormattedLinkLabel ultraFormattedLinkLabel = linkLoadImage;
					bool visible = linkRemovePicture.Enabled = true;
					ultraFormattedLinkLabel.Visible = visible;
				}
				else
				{
					UltraFormattedLinkLabel ultraFormattedLinkLabel2 = linkLoadImage;
					bool visible = linkRemovePicture.Enabled = false;
					ultraFormattedLinkLabel2.Visible = visible;
				}
				pictureBoxPhoto.Image = null;
				SetHeaderName();
				if (currentData.Tables["UDF"].Rows.Count > 0)
				{
					_ = currentData.Tables["UDF"].Rows[0];
					foreach (DataColumn column in currentData.Tables["UDF"].Columns)
					{
						_ = (column.ColumnName == "EntityID");
					}
				}
				else
				{
					udfEntryGrid.ClearData();
				}
				if (dataRow["IsCancelled"] != DBNull.Value)
				{
					IsCancelled = bool.Parse(dataRow["IsCancelled"].ToString());
				}
				else
				{
					IsCancelled = false;
				}
				DataTable dataTable = dataGridPayrollItem.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in currentData.Tables["Candidate_Salary"].Rows)
				{
					byte result = 0;
					byte.TryParse(row["PayType"].ToString(), out result);
					if (result == 1)
					{
						DataRow dataRow3 = dataTable.NewRow();
						foreach (DataColumn column2 in dataRow3.Table.Columns)
						{
							_ = column2;
							dataRow3["PayrollItem"] = row["PayrollItemID"];
							dataRow3["Description"] = row["PayrollItemName"];
							if (row["Amount"] != DBNull.Value)
							{
								object obj3 = dataRow3["Amount"] = (dataRow3["New Amount"] = decimal.Round(decimal.Parse(row["Amount"].ToString()), 2));
							}
						}
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
				}
				dataTable.AcceptChanges();
				ShowTotalSalary();
			}
		}

		private void FillAddressData(DataRow row)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CandidateData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CandidateTable.Rows[0] : currentData.CandidateTable.NewRow();
				dataRow.BeginEdit();
				dataRow["PassportNo"] = textBoxPassportNo.Text.Trim();
				dataRow["GivenName"] = textBoxGivenName.Text.Trim();
				dataRow["SurName"] = textBoxSurName.Text.Trim();
				if (comboBoxNationality.SelectedID != "")
				{
					dataRow["NationalityID"] = comboBoxNationality.SelectedID;
				}
				else
				{
					dataRow["NationalityID"] = DBNull.Value;
				}
				if (comboBoxGender.SelectedID.ToString() != "")
				{
					dataRow["Gender"] = comboBoxGender.SelectedGender;
				}
				else
				{
					dataRow["Gender"] = 'M';
				}
				if (dateTimeBirthDate.Checked)
				{
					dataRow["BirthDate"] = dateTimeBirthDate.Value;
				}
				else
				{
					dataRow["BirthDate"] = DBNull.Value;
				}
				dataRow["GroupID"] = comboBoxGroup.SelectedID;
				dataRow["BirthPlace"] = textBoxBirthPlace.Text.Trim();
				dataRow["PassportPlaceOfIssue"] = textBoxPPIssuePlace.Text.Trim();
				if (dateTimePPIssueDate.Checked)
				{
					dataRow["PassportIssueDate"] = dateTimePPIssueDate.Value;
				}
				else
				{
					dataRow["PassportIssueDate"] = DBNull.Value;
				}
				if (dateTimePPExpiryDate.Checked)
				{
					dataRow["PassportExpiryDate"] = dateTimePPExpiryDate.Value;
				}
				else
				{
					dataRow["PassportExpiryDate"] = DBNull.Value;
				}
				dataRow["VisaStatus"] = comboBoxvisaType.SelectedID;
				dataRow["FatherName"] = textBoxFatherName.Text.Trim();
				dataRow["MotherName"] = textBoxMotherName.Text.Trim();
				dataRow["SpouseName"] = textBoxSpouseName.Text.Trim();
				dataRow["PassportAddress"] = textBoxPPAddress.Text.Trim();
				dataRow["CandidateID"] = textBoxCode.Text.Trim().Substring(2);
				dataRow["Notes"] = textBoxNote.Text.Trim();
				if (comboBoxDesignation.SelectedID != "")
				{
					dataRow["DesignationID"] = comboBoxDesignation.SelectedID;
				}
				else
				{
					dataRow["DesignationID"] = DBNull.Value;
				}
				dataRow["IsExist"] = isExist;
				if (comboBoxSelectionStatus.SelectedID > 0)
				{
					dataRow["SelectionStatus"] = comboBoxSelectionStatus.SelectedID;
				}
				else
				{
					dataRow["SelectionStatus"] = DBNull.Value;
				}
				if (dateTimeSelectedOn.Checked)
				{
					dataRow["SelectedOn"] = dateTimeSelectedOn.Value;
				}
				else
				{
					dataRow["SelectedOn"] = DBNull.Value;
				}
				dataRow["SelectedAt"] = textBoxSelectedAt.Text.Trim();
				dataRow["ThroughAgent"] = comboBoxAgentThrough.SelectedID;
				if (comboBoxPositionActual.SelectedID != "")
				{
					dataRow["ActualDesignation"] = comboBoxPositionActual.SelectedID;
				}
				else
				{
					dataRow["ActualDesignation"] = DBNull.Value;
				}
				dataRow["Remarks"] = textBoxRemarks.Text.Trim();
				dataRow["QualificationID"] = comboBoxQualification.SelectedID;
				dataRow["LanguageID"] = comboBoxLanguage.SelectedID;
				dataRow["ExperienceLocal"] = ((numericExperienceLocal.Text != string.Empty) ? Convert.ToDecimal(numericExperienceLocal.Text) : 0m);
				dataRow["ExperienceAbroad"] = ((numericExperienceAbroad.Text != string.Empty) ? Convert.ToDecimal(numericExperienceAbroad.Text) : 0m);
				if (dateTimeApplTypingDateMOL.Checked)
				{
					dataRow["ApplicationTypingDateMOL"] = dateTimeApplTypingDateMOL.Value;
				}
				else
				{
					dataRow["ApplicationTypingDateMOL"] = DBNull.Value;
				}
				dataRow["MBNumberMOL"] = textBoxMOLMBNo.Text.Trim();
				dataRow["SponsorID"] = comboBoxSponsor.SelectedID;
				dataRow["SponsorID"] = sponsorComboBox.SelectedID;
				if (comboBoxPositionVisa.SelectedID != "")
				{
					dataRow["VisaDesignation"] = comboBoxPositionVisa.SelectedID;
				}
				else
				{
					dataRow["VisaDesignation"] = DBNull.Value;
				}
				if (dateTimeApprovalDateMOL.Checked)
				{
					dataRow["ApprovalDateMOL"] = dateTimeApprovalDateMOL.Value;
				}
				else
				{
					dataRow["ApprovalDateMOL"] = DBNull.Value;
				}
				if (dateTimeApprovalValidTillMOL.Checked)
				{
					dataRow["ApprovalValidTillMOL"] = dateTimeApprovalValidTillMOL.Value;
				}
				else
				{
					dataRow["ApprovalValidTillMOL"] = DBNull.Value;
				}
				dataRow["TempWPNumber"] = textBoxTempWPNo.Text.Trim();
				if (dateTimeApprovalFeePaidOnMOL.Checked)
				{
					dataRow["ApprovalFeePaidOnMOL"] = dateTimeApprovalFeePaidOnMOL.Value;
				}
				else
				{
					dataRow["ApprovalFeePaidOnMOL"] = DBNull.Value;
				}
				if (dateTimeBGPaidOnMOL.Checked)
				{
					dataRow["BGPaidOnMOL"] = dateTimeBGPaidOnMOL.Value;
				}
				else
				{
					dataRow["BGPaidOnMOL"] = DBNull.Value;
				}
				dataRow["BGTypeMOL"] = comboBoxBGTypeMOL.SelectedItem;
				dataRow["VisaAppliedThroughIMG"] = comboBoxVisaAppliedThroughIMG.SelectedItem;
				if (dateTimeVisaPostedOn.Checked)
				{
					dataRow["VisaPostedOnIMG"] = dateTimeVisaPostedOn.Value;
				}
				else
				{
					dataRow["VisaPostedOnIMG"] = DBNull.Value;
				}
				if (dateTimeApprovedOn.Checked)
				{
					dataRow["VisaApprovedOnIMG"] = dateTimeApprovedOn.Value;
				}
				else
				{
					dataRow["VisaApprovedOnIMG"] = DBNull.Value;
				}
				dataRow["VisaIssuePlaceIMG"] = textBoxVisaIssuePlaceIMG.Text.Trim();
				dataRow["VisaNumber"] = textBoxVisaNumber.Text.Trim();
				if (dateTimeVisaIssueDate.Checked)
				{
					dataRow["VisaIssueDateIMG"] = dateTimeVisaIssueDate.Value;
				}
				else
				{
					dataRow["VisaIssueDateIMG"] = DBNull.Value;
				}
				if (dateTimeVisaExpiryDate.Checked)
				{
					dataRow["VisaExpiryDateIMG"] = dateTimeVisaExpiryDate.Value;
				}
				else
				{
					dataRow["VisaExpiryDateIMG"] = DBNull.Value;
				}
				dataRow["UIDNumberIMG"] = textBoxUIDNumberIMG.Text.Trim();
				if (dateTimeVisaCopyToAgentOn.Checked)
				{
					dataRow["VisaCopyToAgentOn"] = dateTimeVisaCopyToAgentOn.Value;
				}
				else
				{
					dataRow["VisaCopyToAgentOn"] = DBNull.Value;
				}
				if (dateTimeArrivedOn.Checked)
				{
					dataRow["ArrivedOn"] = dateTimeArrivedOn.Value;
				}
				else
				{
					dataRow["ArrivedOn"] = DBNull.Value;
				}
				dataRow["ArrivalPort"] = comboBoxArrivalPort.SelectedID;
				dataRow["Category"] = comboBoxCategory.SelectedID;
				dataRow["EmployeeNo"] = textBoxEmployeeNo.Text.Trim();
				dataRow["HealthCardNo"] = textBoxHealthCardNo.Text;
				if (dateTimeMedicalTypingOn.Checked)
				{
					dataRow["MedicalTypingOn"] = dateTimeMedicalTypingOn.Value;
				}
				else
				{
					dataRow["MedicalTypingOn"] = DBNull.Value;
				}
				if (dateTimeMedicalAttendedOn.Checked)
				{
					dataRow["MedicalAttendedOn"] = dateTimeMedicalAttendedOn.Value;
				}
				else
				{
					dataRow["MedicalAttendedOn"] = DBNull.Value;
				}
				if (dateTimeMedicalCollectedOn.Checked)
				{
					dataRow["MedicalCollectedOn"] = dateTimeMedicalCollectedOn.Value;
				}
				else
				{
					dataRow["MedicalCollectedOn"] = DBNull.Value;
				}
				dataRow["MedicalResult"] = comboBoxMedicalResult.SelectedItem;
				dataRow["MedicalNote"] = textBoxMedicalNote.Text.Trim();
				if (dateTimeApplTypingDateEID.Checked)
				{
					dataRow["ApplicationTypedOnEID"] = dateTimeApplTypingDateEID.Value;
				}
				else
				{
					dataRow["ApplicationTypedOnEID"] = DBNull.Value;
				}
				if (dateTimeAttendedDateEID.Checked)
				{
					dataRow["AttendedForEID"] = dateTimeAttendedDateEID.Value;
				}
				else
				{
					dataRow["AttendedForEID"] = DBNull.Value;
				}
				if (dateTimeCollectedOnEID.Checked)
				{
					dataRow["CollectedOnEID"] = dateTimeCollectedOnEID.Value;
				}
				else
				{
					dataRow["CollectedOnEID"] = DBNull.Value;
				}
				dataRow["NationalID"] = textBoxNationalID.Text.Trim();
				if (dateTimeValidityEID.Checked)
				{
					dataRow["NationalIDValidity"] = dateTimeValidityEID.Value;
				}
				else
				{
					dataRow["NationalIDValidity"] = DBNull.Value;
				}
				dataRow["AGTType"] = comboBoxAGTType.SelectedItem;
				dataRow["MBNumberAGT"] = textBoxAGTMBNo.Text.Trim();
				if (dateTimeAGTTypedOn.Checked)
				{
					dataRow["AGTTypedOn"] = dateTimeAGTTypedOn.Value;
				}
				else
				{
					dataRow["AGTTypedOn"] = DBNull.Value;
				}
				if (dateTimeAGTSubmittedOn.Checked)
				{
					dataRow["AGTSubmittedOn"] = dateTimeAGTSubmittedOn.Value;
				}
				else
				{
					dataRow["AGTSubmittedOn"] = DBNull.Value;
				}
				dataRow["WPNumber"] = textBoxWPNo.Text.Trim();
				dataRow["PersonalIDNo"] = textBoxPersonIDNo.Text.Trim();
				dataRow["WPIssuePlace"] = textBoxWPIssuePlace.Text.Trim();
				if (dateTimeWPIssueDate.Checked)
				{
					dataRow["WPIssueDate"] = dateTimeWPIssueDate.Value;
				}
				else
				{
					dataRow["WPIssueDate"] = DBNull.Value;
				}
				if (dateTimeWPExpiryDate.Checked)
				{
					dataRow["WPExpiryDate"] = dateTimeWPExpiryDate.Value;
				}
				else
				{
					dataRow["WPExpiryDate"] = DBNull.Value;
				}
				dataRow["RPProcessType"] = comboBoxProcessType.SelectedItem;
				if (dateTimeApplPostedOnRP.Checked)
				{
					dataRow["ApplicationPostedOnRP"] = dateTimeApplPostedOnRP.Value;
				}
				else
				{
					dataRow["ApplicationPostedOnRP"] = DBNull.Value;
				}
				if (dateTimeApplApprovedOnRP.Checked)
				{
					dataRow["ApplicationApprovedOnRP"] = dateTimeApplApprovedOnRP.Value;
				}
				else
				{
					dataRow["ApplicationApprovedOnRP"] = DBNull.Value;
				}
				if (dateTimeSubmittedZajilOnRP.Checked)
				{
					dataRow["SubmittedToZajil"] = dateTimeSubmittedZajilOnRP.Value;
				}
				else
				{
					dataRow["SubmittedToZajil"] = DBNull.Value;
				}
				if (dateTimePassportCollectedOnRP.Checked)
				{
					dataRow["PassportCollectedOn"] = dateTimePassportCollectedOnRP.Value;
				}
				else
				{
					dataRow["PassportCollectedOn"] = DBNull.Value;
				}
				dataRow["RPIssuePlace"] = textBoxRPIssuePlace.Text.Trim();
				if (dateTimeRPIssueDate.Checked)
				{
					dataRow["RPIssueDate"] = dateTimeRPIssueDate.Value;
				}
				else
				{
					dataRow["RPIssueDate"] = DBNull.Value;
				}
				if (dateTimeRPExpiryDate.Checked)
				{
					dataRow["RPExpiryDate"] = dateTimeRPExpiryDate.Value;
				}
				else
				{
					dataRow["RPExpiryDate"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CandidateTable.Rows.Add(dataRow);
				}
				currentData.CandidateSalaryTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridPayrollItem.Rows)
				{
					if (!(row.Cells["PayrollItem"].Value.ToString() == ""))
					{
						if (!currentData.Tables.Contains("Candidate_Salary"))
						{
							CandidateData candidateData = new CandidateData();
							currentData.Tables.Add(candidateData.Tables[0].Clone());
						}
						dataRow = currentData.Tables["Candidate_Salary"].NewRow();
						dataRow.BeginEdit();
						dataRow["PayType"] = 1;
						dataRow["CandidateID"] = textBoxCode.Text.Trim().Substring(2);
						dataRow["PayrollItemID"] = row.Cells["PayrollItem"].Value.ToString();
						dataRow["Amount"] = decimal.Parse(row.Cells["Amount"].Value.ToString());
						dataRow["RowIndex"] = row.Index;
						dataRow.EndEdit();
						currentData.Tables["Candidate_Salary"].Rows.Add(dataRow);
					}
				}
				DataRow dataRow2 = null;
				DataTable dataTable = null;
				if (!isNewRecord)
				{
					dataTable = currentData.Tables["UDF"];
				}
				else
				{
					dataTable = currentData.Tables.Add("UDF");
					foreach (UltraGridCell field in udfEntryGrid.Fields)
					{
						dataTable.Columns.Add(field.Column.Key, field.Column.DataType);
					}
				}
				dataRow2 = currentData.Tables["UDF"].NewRow();
				foreach (UltraGridCell field2 in udfEntryGrid.Fields)
				{
					dataRow2[field2.Column.Key] = udfEntryGrid.Fields[field2.Column.Key].Value;
				}
				dataRow2["EntityID"] = textBoxCode.Text;
				dataRow2.EndEdit();
				currentData.Tables["UDF"].Rows.Add(dataRow2);
				if (dateTimeArrivedOn.Checked && !string.IsNullOrEmpty(textBoxEmployeeNo.Text) && !IsExist)
				{
					dataRow = currentData.EmployeeTable.NewRow();
					dataRow.BeginEdit();
					dataRow["EmployeeID"] = textBoxEmployeeNo.Text;
					dataRow["FirstName"] = textBoxGivenName.Text;
					dataRow["MiddleName"] = textBoxSurName.Text;
					dataRow["LastName"] = textBoxFatherName.Text;
					dataRow["NationalID"] = textBoxNationalID.Text;
					dataRow["Status"] = 1;
					if (comboBoxGender.SelectedID.ToString() != "")
					{
						dataRow["Gender"] = comboBoxGender.SelectedGender;
					}
					else
					{
						dataRow["Gender"] = 'M';
					}
					if (dateTimeBirthDate.Checked)
					{
						dataRow["BirthDate"] = dateTimeBirthDate.Value;
					}
					else
					{
						dataRow["BirthDate"] = DBNull.Value;
					}
					dataRow["BirthPlace"] = textBoxBirthPlace.Text;
					if (dateTimeArrivedOn.Checked)
					{
						dataRow["JoiningDate"] = dateTimeArrivedOn.Value;
					}
					else
					{
						dataRow["JoiningDate"] = DBNull.Value;
					}
					if (comboBoxGroup.SelectedID != "")
					{
						dataRow["GroupID"] = comboBoxGroup.SelectedID;
					}
					else
					{
						dataRow["GroupID"] = DBNull.Value;
					}
					if (comboBoxNationality.SelectedID != "")
					{
						dataRow["NationalityID"] = comboBoxNationality.SelectedID;
					}
					else
					{
						dataRow["NationalityID"] = DBNull.Value;
					}
					dataRow["SpouseName"] = textBoxSpouseName.Text;
					if (comboBoxMaritalStatus.SelectedID > 0)
					{
						dataRow["MaritalStatus"] = comboBoxMaritalStatus.SelectedID;
					}
					else
					{
						dataRow["MaritalStatus"] = DBNull.Value;
					}
					if (comboBoxReligion.SelectedID != "")
					{
						dataRow["ReligionID"] = comboBoxReligion.SelectedID;
					}
					else
					{
						dataRow["ReligionID"] = DBNull.Value;
					}
					dataRow["BloodGroup"] = textBoxBloodGroup.Text;
					dataRow["Notes"] = textBoxNote.Text;
					dataRow["ContractType"] = comboBoxCategory.SelectedID;
					dataRow["PositionID"] = comboBoxPositionActual.SelectedID;
					dataRow["PrimaryAddressID"] = "PRIMARY";
					if (comboBoxQualification.SelectedID != "")
					{
						dataRow["Qualification"] = comboBoxQualification.SelectedID;
					}
					else
					{
						dataRow["Qualification"] = DBNull.Value;
					}
					dataRow["SponsorID"] = sponsorComboBox.SelectedID;
					if (!isExist)
					{
						dataRow["DateCreated"] = DateTime.Now;
						dataRow["CreatedBy"] = Global.CurrentUser;
					}
					dataRow.EndEdit();
					currentData.EmployeeTable.Rows.Add(dataRow);
					dataRow = currentData.EmployeeAddressTable.NewRow();
					dataRow.BeginEdit();
					dataRow["EmployeeID"] = textBoxEmployeeNo.Text;
					dataRow["AddressID"] = "PRIMARY";
					dataRow["Address1"] = textBoxPPAddress.Text;
					dataRow.EndEdit();
					currentData.EmployeeAddressTable.Rows.Add(dataRow);
					currentData.EmployeePayrollItemDetail.Rows.Clear();
					foreach (UltraGridRow row2 in dataGridPayrollItem.Rows)
					{
						if (!(row2.Cells["PayrollItem"].Value.ToString() == ""))
						{
							if (!currentData.Tables.Contains("Employee_PayrollItem_Detail"))
							{
								EmployeePayrollItemDetailData employeePayrollItemDetailData = new EmployeePayrollItemDetailData();
								currentData.Tables.Add(employeePayrollItemDetailData.Tables[0].Clone());
							}
							dataRow = currentData.Tables["Employee_PayrollItem_Detail"].NewRow();
							dataRow.BeginEdit();
							dataRow["PayType"] = 1;
							dataRow["EmployeeID"] = textBoxEmployeeNo.Text;
							dataRow["PayrollItemID"] = row2.Cells["PayrollItem"].Value.ToString();
							dataRow["Amount"] = decimal.Parse(row2.Cells["Amount"].Value.ToString());
							dataRow["RowIndex"] = row2.Index;
							dataRow.EndEdit();
							currentData.Tables["Employee_PayrollItem_Detail"].Rows.Add(dataRow);
						}
					}
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private string GetNextSequenceNumber()
		{
			try
			{
				return Factory.CandidateSystem.GetNextSequenceNumber("Candidate", "CandidateID");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private string GetNextEmployeeNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextSponserEmployeeNumber(sponsorComboBox.SelectedID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
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
				textBoxCode.Text = textBoxCode.Text.Trim();
				if (textBoxCode.Text.Trim() == string.Empty || textBoxGivenName.Text.Trim() == string.Empty || textBoxSurName.Text.Trim() == string.Empty || textBoxPassportNo.Text.Trim() == string.Empty || comboBoxDesignation.SelectedID == "")
				{
					ErrorHelper.WarningMessage("Please enter required fields.");
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
					textBoxCode.SelectAll();
					return false;
				}
				if (dateTimePPIssueDate.Value > dateTimePPExpiryDate.Value)
				{
					ErrorHelper.InformationMessage("Passport - Expiry Date should not be less than Issue Date");
					dateTimePPIssueDate.Focus();
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Candidate", "CandidateID", textBoxCode.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Code already exist.");
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Candidate", "PassportNo", textBoxPassportNo.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Passport Number already exist.");
					tabPageGeneral.Tab.Selected = true;
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
				switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.CandidateSystem.CreateCandidate(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Candidate, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.CandidateSystem.UpdateCandidate(currentData);
				}
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else if (clearAfter)
				{
					ClearForm();
					IsNewRecord = true;
				}
				else
				{
					formManager.ResetDirty();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id == ""))
				{
					currentData = Factory.CandidateSystem.GetCandidateByID(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
						textBoxCode.Focus();
					}
					else
					{
						IsNewRecord = false;
						FillData();
						formManager.ResetDirty();
					}
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
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

		private void SetupPayrollItemGrid()
		{
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable("PayrollItem");
			dataTable.Columns.Add("PayrollItem", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataTable.Columns.Add("New Amount", typeof(decimal));
			dataGridPayrollItem.DataSource = dataTable;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].CharacterCasing = CharacterCasing.Upper;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].ValueList = comboBoxPayrollItem;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Width = checked(25 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Description"].Width = checked(50 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(15 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].Format = "n";
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].Width = checked(20 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].MinValue = 0;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].Format = Format.GridAmountFormat;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].Hidden = true;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Header.Appearance.Cursor = Cursors.Hand;
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
			string currentID = textBoxCode.Text.Substring(2);
			LoadData(DatabaseHelper.GetPreviousID("Candidate", "CandidateID", currentID));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string currentID = textBoxCode.Text.Substring(2);
			LoadData(DatabaseHelper.GetNextID("Candidate", "CandidateID", currentID));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Candidate", "CandidateID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Candidate", "CandidateID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Candidate", "CandidateID", toolStripTextBoxFind.Text.Trim()))
				{
					LoadData(toolStripTextBoxFind.Text.Trim());
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

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this record?") == DialogResult.No)
				{
					return false;
				}
				return Factory.CandidateSystem.DeleteCandidate(textBoxCode.Text);
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ClearForm()
		{
			textBoxCode.Text = "VS" + GetNextSequenceNumber();
			textBoxNote.Clear();
			textBoxAddress1.Clear();
			textBoxAddress2.Clear();
			textBoxAddress3.Clear();
			textBoxAddressID.Text = "PRIMARY";
			textBoxCity.Clear();
			textBoxComment.Clear();
			textBoxCountry.Clear();
			textBoxEmail.Clear();
			textBoxFax.Clear();
			textBoxMobile.Clear();
			textBoxPhone1.Clear();
			textBoxPhone2.Clear();
			textBoxPostalCode.Clear();
			textBoxState.Clear();
			textBoxTotalSalary.Clear();
			udfEntryGrid.ClearData();
			pictureBoxPhoto.Image = null;
			comboBoxBGTypeMOL.SelectedIndex = -1;
			comboBoxVisaAppliedThroughIMG.SelectedIndex = -1;
			comboBoxAgentThrough.SelectedIndex = -1;
			textBoxGivenName.Clear();
			textBoxSurName.Clear();
			textBoxFatherName.Clear();
			textBoxMotherName.Clear();
			textBoxSpouseName.Clear();
			textBoxBirthPlace.Clear();
			textBoxAge.Clear();
			dateTimeBirthDate.Checked = false;
			comboBoxGroup.Clear();
			comboBoxNationality.Clear();
			comboBoxReligion.Clear();
			comboBoxGender.SelectedGender = 'M';
			comboBoxMaritalStatus.Clear();
			textBoxBloodGroup.Clear();
			textBoxPassportNo.Clear();
			textBoxPPIssuePlace.Clear();
			textBoxPPAddress.Clear();
			dateTimePPIssueDate.Checked = false;
			dateTimePPExpiryDate.Checked = false;
			comboBoxSelectionStatus.SelectedID = 1;
			textBoxSelectedAt.Clear();
			dateTimeSelectedOn.Checked = false;
			dateTimeApplTypingDateMOL.Checked = false;
			dateTimeBGPaidOnMOL.Checked = false;
			textBoxMOLMBNo.Clear();
			dateTimeApprovalDateMOL.Checked = false;
			dateTimeVisaCopyToAgentOn.Checked = false;
			textBoxUIDNumberIMG.Clear();
			dateTimeVisaPostedOn.Checked = false;
			dateTimeApprovedOn.Checked = false;
			textBoxRemarks.Clear();
			comboBoxDesignation.Clear();
			comboBoxvisaType.SelectedID = 1;
			comboBoxSelectionStatus.SelectedIndex = -1;
			comboBoxArrivalPort.Clear();
			comboBoxCategory.Clear();
			dateTimeApprovalValidTillMOL.Checked = false;
			dateTimeVisaIssueDate.Checked = false;
			dateTimeVisaExpiryDate.Checked = false;
			textBoxVisaNumber.Clear();
			textBoxTempWPNo.Clear();
			comboBoxMedicalResult.SelectedIndex = -1;
			textBoxMedicalNote.Clear();
			dateTimeArrivedOn.Checked = false;
			comboBoxProcessType.SelectedIndex = -1;
			dateTimeApplTypingDateEID.Checked = false;
			dateTimeCollectedOnEID.Checked = false;
			textBoxNationalID.Clear();
			dateTimeAttendedDateEID.Checked = false;
			dateTimeValidityEID.Checked = false;
			dateTimeMedicalTypingOn.Checked = false;
			dateTimeMedicalCollectedOn.Checked = false;
			dateTimeMedicalAttendedOn.Checked = false;
			dateTimeApplPostedOnRP.Checked = false;
			dateTimeApplApprovedOnRP.Checked = false;
			dateTimeSubmittedZajilOnRP.Checked = false;
			dateTimePassportCollectedOnRP.Checked = false;
			dateTimeRPIssueDate.Checked = false;
			dateTimeRPExpiryDate.Checked = false;
			textBoxRPIssuePlace.Clear();
			dateTimeAGTTypedOn.Checked = false;
			dateTimeAGTSubmittedOn.Checked = false;
			textBoxWPNo.Clear();
			textBoxPersonIDNo.Clear();
			dateTimeWPIssueDate.Checked = false;
			dateTimeWPExpiryDate.Checked = false;
			textBoxWPIssuePlace.Clear();
			labelCustomerNameHeader.Text = string.Empty;
			dateTimeRPIssueDate.Checked = false;
			dateTimeRPExpiryDate.Checked = false;
			textBoxRPIssuePlace.Clear();
			dateTimeAGTTypedOn.Checked = false;
			dateTimeAGTSubmittedOn.Checked = false;
			textBoxWPNo.Clear();
			comboBoxPositionVisa.Clear();
			comboBoxPositionActual.Clear();
			comboBoxAgentThrough.Clear();
			dateTimeWPIssueDate.Checked = false;
			dateTimeWPExpiryDate.Checked = false;
			textBoxWPIssuePlace.Clear();
			comboBoxAGTType.SelectedIndex = -1;
			textBoxAGTMBNo.Clear();
			comboBoxQualification.Clear();
			comboBoxLanguage.Clear();
			numericExperienceLocal.Text = 0m.ToString();
			numericExperienceAbroad.Text = 0m.ToString();
			textBoxEmployeeNo.Clear();
			textBoxPassportNo.Focus();
			comboBoxSponsor.Clear();
			sponsorComboBox.Clear();
			textBoxHealthCardNo.Clear();
			(dataGridPayrollItem.DataSource as DataTable).Rows.Clear();
			isExist = false;
			formManager.ResetDirty();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				ClearForm();
			}
			else if (SaveData())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		public void OnActivated()
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxCode.Focus();
		}

		private void CandidateClassDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void dateTimePickerBirthDate_ValueChanged(object sender, EventArgs e)
		{
			if (!dateTimeBirthDate.Checked)
			{
				textBoxAge.Clear();
				return;
			}
			DateTime value = dateTimeBirthDate.Value;
			TimeSpan timeSpan = DateTime.Today - value;
			int num = 0;
			if (timeSpan.Days > 0)
			{
				num = timeSpan.Days / 365;
			}
			if (num > 0)
			{
				textBoxAge.Text = num.ToString() + " Years";
			}
			else
			{
				textBoxAge.Clear();
			}
		}

		private void dateTimePickerJoiningDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void buttonMoreAddress_Click(object sender, EventArgs e)
		{
		}

		private void linkLabelCountry_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPosition(comboBoxDesignation.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployeeGroup(comboBoxGroup.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void buttonMore_Click(object sender, EventArgs e)
		{
		}

		private void dependentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void skillsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(textBoxCode.Text == "") && (!IsDirty || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false))))
				{
					DataSet candidateAppointmentDetails = Factory.CandidateSystem.GetCandidateAppointmentDetails(textBoxCode.Text.Substring(2), textBoxCode.Text.Substring(2), "", "", "", "", showInactive: true);
					if (candidateAppointmentDetails == null || candidateAppointmentDetails.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(candidateAppointmentDetails, "", "Offer Letter", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Appointment);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = textBoxGivenName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Candidates;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddCandidatePhoto(textBoxCode.Text.Substring(2), image))
					{
						pictureBoxPhoto.Image = image;
						linkLoadImage.Visible = false;
						linkRemovePicture.Enabled = true;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot add image.");
			}
		}

		private void linkLoadImage_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			LoadPhoto();
		}

		private void LoadPhoto()
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord)
				{
					pictureBoxPhoto.Image = PublicFunctions.GetCandidateThumbnailImage(textBoxCode.Text.Substring(2));
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void linkRemovePicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to remove the item image?") == DialogResult.Yes)
				{
					if (Factory.CandidateSystem.RemoveCandidatePhoto(textBoxCode.Text.Substring(2)))
					{
						pictureBoxPhoto.Image = null;
						linkLoadImage.Visible = false;
						linkRemovePicture.Enabled = false;
					}
					else
					{
						ErrorHelper.ErrorMessage("Cannot remove the image.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot remove image.");
			}
		}

		private void toolStripBtnMakeEmployee_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeDetailsFormObj);
			FormActivator.EmployeeDetailsFormObj.LoadData(textBoxEmployeeNo.Text);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
		}

		private void morePrintToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && (!IsDirty || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false))))
				{
					DataSet candidateAppointmentDetails = Factory.CandidateSystem.GetCandidateAppointmentDetails(textBoxCode.Text.Substring(2), textBoxCode.Text.Substring(2), "", "", "", "", showInactive: true);
					if (candidateAppointmentDetails == null || candidateAppointmentDetails.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(candidateAppointmentDetails, "", "Appointment Details", SysDocTypes.None, isPrint: true, showPrintDialog: true);
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}
	}
}
