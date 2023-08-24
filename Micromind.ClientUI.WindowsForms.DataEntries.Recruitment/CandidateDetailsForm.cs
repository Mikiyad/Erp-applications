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
	public class CandidateDetailsForm : Form, IForm
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

		private PortComboBox comboBoxArrivalPort;

		private EmployeeTypeComboBox comboBoxCategory;

		private MMLabel mmLabel29;

		private MMLabel mmLabel88;

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

		private MMLabel mmLabel92;

		private ComboBox comboBoxECRStatus;

		private MMSDateTimePicker datetimePickerSystemdate;

		private MMLabel mmLabel93;

		private MMLabel mmLabel96;

		private ComboBox comboBoxApprovalStatusMOL;

		private MMTextBox textBoxRegnNumber;

		private MMLabel mmLabel94;

		private MMLabel mmLabel95;

		private MMSDateTimePicker datetimepickerAOtypingDate;

		private MMLabel mmLabel97;

		private MMTextBox textBoxIMGRemarks;

		private MMLabel mmLabel98;

		private MMTextBox textBoxMOLRemarks;

		private MMLabel mmLabel100;

		private MMSDateTimePicker datetimepickerExpectedArrivalDate;

		private MMLabel mmLabel99;

		private ComboBox comboBoxApprovalStatusIMG;

		private MMLabel mmLabel101;

		private ComboBox comboBoxApplicationType;

		private UltraTabPageControl ultraTabPageControl6;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

		private UltraTabPageControl ultraTabPageControl7;

		private DataEntryGrid dataGridPayrollItem;

		private PayrollItemComboBox comboBoxPayrollItem;

		private AmountTextBox textBoxTotalSalary;

		private Label label7;

		private UltraTabPageControl ultraTabPageControl8;

		private PayrollItemComboBox comboBoxDeduction;

		private DataEntryGrid dataGridDeduction;

		private UltraTabPageControl ultraTabPageControl9;

		private BenefitComboBox comboBoxBenefit;

		private DataEntryGrid dataGridBenefit;

		private UltraTabPageControl ultraTabPageControl10;

		private TextBox textBoxTicketRemarks;

		private Label label10;

		private NumberTextBox textBoxTicketPeriod;

		private NumberTextBox textBoxNumberOfTickets;

		private AmountTextBox textBoxTicketAmount;

		private Label label5;

		private Label label9;

		private Label label6;

		private Label label3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private DestinationComboBox comboBoxDestination;

		private Panel panelSalaryDetails;

		private SelectionStatusComboBox specialConditionComboBox;

		private MMLabel mmLabel103;

		private SelectionStatusComboBox agreementStatusComboBox;

		private MMLabel mmLabel102;

		private DivisionComboBox comboBoxDivision;

		private TextBox textBoxDivisionname;

		private MMLabel mmLabel104;

		private MMSDateTimePicker datetimeSignedAORcvd;

		private MMLabel mmLabel105;

		private MMSDateTimePicker dateTimeSignedAGTRecvd;

		private MMLabel mmLabel106;

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
				EnableDisableWorkFlowScreen(value);
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
				buttonNew.Enabled = !value;
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
			}
		}

		public CandidateDetailsForm()
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
			components = new System.ComponentModel.Container();
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab10 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab11 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab12 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab13 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Recruitment.CandidateDetailsForm));
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
			Infragistics.Win.Appearance appearance189 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance190 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance191 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance192 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance193 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance194 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance195 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance196 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance197 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance198 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance199 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance200 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance201 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance202 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance203 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance204 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance205 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance206 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance207 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance208 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance209 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance210 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance211 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance212 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance213 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance214 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance215 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance216 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance217 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance218 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance219 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance220 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance221 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance222 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance223 = new Infragistics.Win.Appearance();
			ultraTabPageControl7 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label7 = new System.Windows.Forms.Label();
			ultraTabPageControl8 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl9 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl10 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxTicketRemarks = new System.Windows.Forms.TextBox();
			label10 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelGeneral = new System.Windows.Forms.Panel();
			comboBoxApplicationType = new System.Windows.Forms.ComboBox();
			comboBoxECRStatus = new System.Windows.Forms.ComboBox();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelRecruitment = new System.Windows.Forms.Panel();
			textBoxDivisionname = new System.Windows.Forms.TextBox();
			textBoxLanguageName = new System.Windows.Forms.TextBox();
			textBoxQualificationName = new System.Windows.Forms.TextBox();
			numericExperienceAbroad = new System.Windows.Forms.NumericUpDown();
			numericExperienceLocal = new System.Windows.Forms.NumericUpDown();
			textBoxActualDesignationName = new System.Windows.Forms.TextBox();
			textBoxThroughAgentName = new System.Windows.Forms.TextBox();
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelVisaProcess = new System.Windows.Forms.Panel();
			panelVisaIMG = new Infragistics.Win.Misc.UltraGroupBox();
			comboBoxApprovalStatusIMG = new System.Windows.Forms.ComboBox();
			comboBoxVisaAppliedThroughIMG = new System.Windows.Forms.ComboBox();
			panelVisaMOL = new Infragistics.Win.Misc.UltraGroupBox();
			comboBoxApprovalStatusMOL = new System.Windows.Forms.ComboBox();
			textBoxVisaDesignationName = new System.Windows.Forms.TextBox();
			textBoxSponsorName = new System.Windows.Forms.TextBox();
			comboBoxBGTypeMOL = new System.Windows.Forms.ComboBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelArrival = new System.Windows.Forms.Panel();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelWPRP = new System.Windows.Forms.Panel();
			panelMedicalReport = new Infragistics.Win.Misc.UltraGroupBox();
			comboBoxProcessType = new System.Windows.Forms.ComboBox();
			panelAGT = new Infragistics.Win.Misc.UltraGroupBox();
			comboBoxAGTType = new System.Windows.Forms.ComboBox();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelMedicalEmirates = new System.Windows.Forms.Panel();
			panelEmirates = new Infragistics.Win.Misc.UltraGroupBox();
			panelMedicalDetail = new Infragistics.Win.Misc.UltraGroupBox();
			comboBoxMedicalResult = new System.Windows.Forms.ComboBox();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraTabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelSalaryDetails = new System.Windows.Forms.Panel();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraTabControl = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			panelButtons = new System.Windows.Forms.Panel();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButtonEmployee = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panel1 = new System.Windows.Forms.Panel();
			labelCancelled = new System.Windows.Forms.Label();
			labelCategory = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			dependentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			documentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			skillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			mmLabel101 = new Micromind.UISupport.MMLabel();
			datetimePickerSystemdate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel93 = new Micromind.UISupport.MMLabel();
			mmLabel92 = new Micromind.UISupport.MMLabel();
			mmLabel69 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel54 = new Micromind.UISupport.MMLabel();
			mmLabel53 = new Micromind.UISupport.MMLabel();
			textBoxPassportNo = new Micromind.UISupport.MMTextBox();
			textBoxPPAddress = new Micromind.UISupport.MMTextBox();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			textBoxBloodGroup = new Micromind.UISupport.MMTextBox();
			comboBoxReligion = new Micromind.DataControls.ReligionComboBox();
			comboBoxMaritalStatus = new Micromind.DataControls.MaritalStatusComboBox();
			mmLabel47 = new Micromind.UISupport.MMLabel();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			textBoxSpouseName = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxMotherName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxFatherName = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			dateTimePPExpiryDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel3 = new Micromind.UISupport.MMLabel();
			dateTimePPIssueDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxPPIssuePlace = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxBirthPlace = new Micromind.UISupport.MMTextBox();
			mmLabel33 = new Micromind.UISupport.MMLabel();
			mmLabel51 = new Micromind.UISupport.MMLabel();
			dateTimeBirthDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxAge = new Micromind.UISupport.MMTextBox();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			comboBoxGender = new Micromind.DataControls.GenderComboBox();
			comboBoxNationality = new Micromind.DataControls.NationalityComboBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			labelCandidateNumber = new Micromind.UISupport.MMLabel();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxSurName = new Micromind.UISupport.MMTextBox();
			textBoxGivenName = new Micromind.UISupport.MMTextBox();
			labelGivenName = new Micromind.UISupport.MMLabel();
			mmLabel104 = new Micromind.UISupport.MMLabel();
			specialConditionComboBox = new Micromind.DataControls.SelectionStatusComboBox();
			mmLabel103 = new Micromind.UISupport.MMLabel();
			agreementStatusComboBox = new Micromind.DataControls.SelectionStatusComboBox();
			mmLabel102 = new Micromind.UISupport.MMLabel();
			comboBoxDivision = new Micromind.DataControls.DivisionComboBox();
			mmLabel81 = new Micromind.UISupport.MMLabel();
			mmLabel76 = new Micromind.UISupport.MMLabel();
			comboBoxLanguage = new Micromind.DataControls.LanguageComboBox();
			comboBoxQualification = new Micromind.DataControls.QualificationComboBox();
			mmLabel75 = new Micromind.UISupport.MMLabel();
			mmLabel74 = new Micromind.UISupport.MMLabel();
			mmLabel73 = new Micromind.UISupport.MMLabel();
			mmLabel71 = new Micromind.UISupport.MMLabel();
			comboBoxPositionActual = new Micromind.DataControls.PositionComboBox();
			mmLabel68 = new Micromind.UISupport.MMLabel();
			comboBoxAgentThrough = new Micromind.DataControls.AgentComboBox();
			mmLabel55 = new Micromind.UISupport.MMLabel();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			comboBoxSelectionStatus = new Micromind.DataControls.SelectionStatusComboBox();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			mmLabel38 = new Micromind.UISupport.MMLabel();
			textBoxSelectedAt = new Micromind.UISupport.MMTextBox();
			dateTimeSelectedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel37 = new Micromind.UISupport.MMLabel();
			mmLabel36 = new Micromind.UISupport.MMLabel();
			udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
			buttonMakeEmployee = new Micromind.UISupport.XPButton();
			comboBoxArrivalPort = new Micromind.DataControls.PortComboBox();
			comboBoxCategory = new Micromind.DataControls.EmployeeTypeComboBox();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			mmLabel88 = new Micromind.UISupport.MMLabel();
			textBoxEmployeeNo = new Micromind.UISupport.MMTextBox();
			mmLabel34 = new Micromind.UISupport.MMLabel();
			dateTimeArrivedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel27 = new Micromind.UISupport.MMLabel();
			dateTimeValidityEID = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeCollectedOnEID = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel42 = new Micromind.UISupport.MMLabel();
			mmLabel43 = new Micromind.UISupport.MMLabel();
			dateTimeAttendedDateEID = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel44 = new Micromind.UISupport.MMLabel();
			textBoxNationalID = new Micromind.UISupport.MMTextBox();
			mmLabel45 = new Micromind.UISupport.MMLabel();
			dateTimeApplTypingDateEID = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel46 = new Micromind.UISupport.MMLabel();
			textBoxHealthCardNo = new Micromind.UISupport.MMTextBox();
			mmLabel72 = new Micromind.UISupport.MMLabel();
			mmLabel89 = new Micromind.UISupport.MMLabel();
			dateTimeMedicalCollectedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel41 = new Micromind.UISupport.MMLabel();
			dateTimeMedicalAttendedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel40 = new Micromind.UISupport.MMLabel();
			dateTimeMedicalTypingOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel35 = new Micromind.UISupport.MMLabel();
			textBoxMedicalNote = new Micromind.UISupport.MMTextBox();
			mmLabel78 = new Micromind.UISupport.MMLabel();
			dateTimeRPExpiryDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel56 = new Micromind.UISupport.MMLabel();
			mmLabel65 = new Micromind.UISupport.MMLabel();
			dateTimeRPIssueDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel64 = new Micromind.UISupport.MMLabel();
			textBoxRPIssuePlace = new Micromind.UISupport.MMTextBox();
			mmLabel63 = new Micromind.UISupport.MMLabel();
			dateTimePassportCollectedOnRP = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel52 = new Micromind.UISupport.MMLabel();
			dateTimeSubmittedZajilOnRP = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel50 = new Micromind.UISupport.MMLabel();
			dateTimeApplApprovedOnRP = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel49 = new Micromind.UISupport.MMLabel();
			dateTimeApplPostedOnRP = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel48 = new Micromind.UISupport.MMLabel();
			dateTimeSignedAGTRecvd = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel106 = new Micromind.UISupport.MMLabel();
			textBoxAGTMBNo = new Micromind.UISupport.MMTextBox();
			mmLabel91 = new Micromind.UISupport.MMLabel();
			mmLabel90 = new Micromind.UISupport.MMLabel();
			textBoxPersonIDNo = new Micromind.UISupport.MMTextBox();
			mmLabel66 = new Micromind.UISupport.MMLabel();
			textBoxWPIssuePlace = new Micromind.UISupport.MMTextBox();
			dateTimeWPExpiryDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeWPIssueDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel62 = new Micromind.UISupport.MMLabel();
			mmLabel61 = new Micromind.UISupport.MMLabel();
			mmLabel60 = new Micromind.UISupport.MMLabel();
			textBoxWPNo = new Micromind.UISupport.MMTextBox();
			dateTimeAGTSubmittedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel57 = new Micromind.UISupport.MMLabel();
			mmLabel58 = new Micromind.UISupport.MMLabel();
			dateTimeAGTTypedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel59 = new Micromind.UISupport.MMLabel();
			mmLabel100 = new Micromind.UISupport.MMLabel();
			datetimepickerExpectedArrivalDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel99 = new Micromind.UISupport.MMLabel();
			mmLabel97 = new Micromind.UISupport.MMLabel();
			textBoxIMGRemarks = new Micromind.UISupport.MMTextBox();
			mmLabel77 = new Micromind.UISupport.MMLabel();
			dateTimeVisaCopyToAgentOn = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeVisaExpiryDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeVisaIssueDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel87 = new Micromind.UISupport.MMLabel();
			textBoxVisaIssuePlaceIMG = new Micromind.UISupport.MMTextBox();
			mmLabel86 = new Micromind.UISupport.MMLabel();
			textBoxVisaNumber = new Micromind.UISupport.MMTextBox();
			mmLabel85 = new Micromind.UISupport.MMLabel();
			mmLabel84 = new Micromind.UISupport.MMLabel();
			textBoxUIDNumberIMG = new Micromind.UISupport.MMTextBox();
			mmLabel83 = new Micromind.UISupport.MMLabel();
			dateTimeVisaPostedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel26 = new Micromind.UISupport.MMLabel();
			dateTimeApprovedOn = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel28 = new Micromind.UISupport.MMLabel();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			datetimeSignedAORcvd = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel105 = new Micromind.UISupport.MMLabel();
			mmLabel98 = new Micromind.UISupport.MMLabel();
			textBoxMOLRemarks = new Micromind.UISupport.MMTextBox();
			datetimepickerAOtypingDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel96 = new Micromind.UISupport.MMLabel();
			textBoxRegnNumber = new Micromind.UISupport.MMTextBox();
			mmLabel94 = new Micromind.UISupport.MMLabel();
			mmLabel95 = new Micromind.UISupport.MMLabel();
			comboBoxPositionVisa = new Micromind.DataControls.PositionComboBox();
			mmLabel67 = new Micromind.UISupport.MMLabel();
			comboBoxSponsor = new Micromind.DataControls.SponsorComboBox();
			mmLabel70 = new Micromind.UISupport.MMLabel();
			mmLabel82 = new Micromind.UISupport.MMLabel();
			dateTimeApprovalFeePaidOnMOL = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxTempWPNo = new Micromind.UISupport.MMTextBox();
			mmLabel80 = new Micromind.UISupport.MMLabel();
			dateTimeApprovalValidTillMOL = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel79 = new Micromind.UISupport.MMLabel();
			dateTimeBGPaidOnMOL = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel25 = new Micromind.UISupport.MMLabel();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			dateTimeApprovalDateMOL = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel22 = new Micromind.UISupport.MMLabel();
			textBoxMOLMBNo = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			dateTimeApplTypingDateMOL = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel30 = new Micromind.UISupport.MMLabel();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			textBoxComment = new Micromind.UISupport.MMTextBox();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			textBoxPostalCode = new Micromind.UISupport.MMTextBox();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			textBoxEmail = new Micromind.UISupport.MMTextBox();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			textBoxMobile = new Micromind.UISupport.MMTextBox();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			textBoxFax = new Micromind.UISupport.MMTextBox();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			textBoxPhone2 = new Micromind.UISupport.MMTextBox();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			textBoxPhone1 = new Micromind.UISupport.MMTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			textBoxCountry = new Micromind.UISupport.MMTextBox();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxState = new Micromind.UISupport.MMTextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxCity = new Micromind.UISupport.MMTextBox();
			textBoxAddress3 = new Micromind.UISupport.MMTextBox();
			textBoxAddress2 = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxAddress1 = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxAddressID = new Micromind.UISupport.MMTextBox();
			dataGridPayrollItem = new Micromind.DataControls.DataEntryGrid();
			comboBoxPayrollItem = new Micromind.DataControls.PayrollItemComboBox();
			textBoxTotalSalary = new Micromind.UISupport.AmountTextBox();
			comboBoxDeduction = new Micromind.DataControls.PayrollItemComboBox();
			dataGridDeduction = new Micromind.DataControls.DataEntryGrid();
			comboBoxBenefit = new Micromind.DataControls.BenefitComboBox();
			dataGridBenefit = new Micromind.DataControls.DataEntryGrid();
			textBoxTicketPeriod = new Micromind.UISupport.NumberTextBox();
			textBoxNumberOfTickets = new Micromind.UISupport.NumberTextBox();
			textBoxTicketAmount = new Micromind.UISupport.AmountTextBox();
			comboBoxDestination = new Micromind.DataControls.DestinationComboBox();
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonClose = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabPageControl7.SuspendLayout();
			ultraTabPageControl8.SuspendLayout();
			ultraTabPageControl9.SuspendLayout();
			ultraTabPageControl10.SuspendLayout();
			tabPageGeneral.SuspendLayout();
			panelGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			tabPageDetails.SuspendLayout();
			panelRecruitment.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericExperienceAbroad).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericExperienceLocal).BeginInit();
			ultraTabPageControl4.SuspendLayout();
			panelVisaProcess.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelVisaIMG).BeginInit();
			panelVisaIMG.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelVisaMOL).BeginInit();
			panelVisaMOL.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			panelArrival.SuspendLayout();
			ultraTabPageControl3.SuspendLayout();
			panelWPRP.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelMedicalReport).BeginInit();
			panelMedicalReport.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelAGT).BeginInit();
			panelAGT.SuspendLayout();
			ultraTabPageControl2.SuspendLayout();
			panelMedicalEmirates.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelEmirates).BeginInit();
			panelEmirates.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelMedicalDetail).BeginInit();
			panelMedicalDetail.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			ultraTabPageControl5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			ultraTabPageControl6.SuspendLayout();
			panelSalaryDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl).BeginInit();
			ultraTabControl.SuspendLayout();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			panel1.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxReligion).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLanguage).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxQualification).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPositionActual).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAgentThrough).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArrivalPort).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPositionVisa).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridPayrollItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDeduction).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridDeduction).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBenefit).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridBenefit).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDestination).BeginInit();
			SuspendLayout();
			ultraTabPageControl7.Controls.Add(dataGridPayrollItem);
			ultraTabPageControl7.Controls.Add(comboBoxPayrollItem);
			ultraTabPageControl7.Controls.Add(textBoxTotalSalary);
			ultraTabPageControl7.Controls.Add(label7);
			ultraTabPageControl7.Location = new System.Drawing.Point(1, 20);
			ultraTabPageControl7.Name = "ultraTabPageControl7";
			ultraTabPageControl7.Size = new System.Drawing.Size(710, 308);
			label7.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(507, 287);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(68, 13);
			label7.TabIndex = 27;
			label7.Text = "Total Salary:";
			ultraTabPageControl8.Controls.Add(comboBoxDeduction);
			ultraTabPageControl8.Controls.Add(dataGridDeduction);
			ultraTabPageControl8.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl8.Name = "ultraTabPageControl8";
			ultraTabPageControl8.Size = new System.Drawing.Size(710, 308);
			ultraTabPageControl9.Controls.Add(comboBoxBenefit);
			ultraTabPageControl9.Controls.Add(dataGridBenefit);
			ultraTabPageControl9.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl9.Name = "ultraTabPageControl9";
			ultraTabPageControl9.Size = new System.Drawing.Size(710, 308);
			ultraTabPageControl10.Controls.Add(textBoxTicketRemarks);
			ultraTabPageControl10.Controls.Add(label10);
			ultraTabPageControl10.Controls.Add(textBoxTicketPeriod);
			ultraTabPageControl10.Controls.Add(textBoxNumberOfTickets);
			ultraTabPageControl10.Controls.Add(textBoxTicketAmount);
			ultraTabPageControl10.Controls.Add(label5);
			ultraTabPageControl10.Controls.Add(label9);
			ultraTabPageControl10.Controls.Add(label6);
			ultraTabPageControl10.Controls.Add(label3);
			ultraTabPageControl10.Controls.Add(ultraFormattedLinkLabel3);
			ultraTabPageControl10.Controls.Add(comboBoxDestination);
			ultraTabPageControl10.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl10.Name = "ultraTabPageControl10";
			ultraTabPageControl10.Size = new System.Drawing.Size(710, 308);
			textBoxTicketRemarks.Location = new System.Drawing.Point(127, 109);
			textBoxTicketRemarks.MaxLength = 255;
			textBoxTicketRemarks.Name = "textBoxTicketRemarks";
			textBoxTicketRemarks.Size = new System.Drawing.Size(396, 21);
			textBoxTicketRemarks.TabIndex = 4;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(10, 112);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(52, 13);
			label10.TabIndex = 64;
			label10.Text = "Remarks:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(10, 89);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(48, 13);
			label5.TabIndex = 60;
			label5.Text = "Amount:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(247, 66);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(31, 13);
			label9.TabIndex = 60;
			label9.Text = "Days";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(10, 66);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(72, 13);
			label6.TabIndex = 60;
			label6.Text = "Ticket Period:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(10, 43);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(97, 13);
			label3.TabIndex = 60;
			label3.Text = "Number of Tickets:";
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(10, 20);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(63, 15);
			ultraFormattedLinkLabel3.TabIndex = 59;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Destination:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance;
			tabPageGeneral.Controls.Add(panelGeneral);
			tabPageGeneral.Location = new System.Drawing.Point(-10000, -10000);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(810, 608);
			panelGeneral.Controls.Add(comboBoxApplicationType);
			panelGeneral.Controls.Add(mmLabel101);
			panelGeneral.Controls.Add(datetimePickerSystemdate);
			panelGeneral.Controls.Add(mmLabel93);
			panelGeneral.Controls.Add(mmLabel92);
			panelGeneral.Controls.Add(comboBoxECRStatus);
			panelGeneral.Controls.Add(mmLabel69);
			panelGeneral.Controls.Add(pictureBoxNoImage);
			panelGeneral.Controls.Add(linkLoadImage);
			panelGeneral.Controls.Add(textBoxNote);
			panelGeneral.Controls.Add(mmLabel54);
			panelGeneral.Controls.Add(mmLabel53);
			panelGeneral.Controls.Add(textBoxPassportNo);
			panelGeneral.Controls.Add(linkRemovePicture);
			panelGeneral.Controls.Add(linkAddPicture);
			panelGeneral.Controls.Add(pictureBoxPhoto);
			panelGeneral.Controls.Add(textBoxPPAddress);
			panelGeneral.Controls.Add(mmLabel21);
			panelGeneral.Controls.Add(textBoxBloodGroup);
			panelGeneral.Controls.Add(comboBoxReligion);
			panelGeneral.Controls.Add(ultraFormattedLinkLabel1);
			panelGeneral.Controls.Add(comboBoxMaritalStatus);
			panelGeneral.Controls.Add(mmLabel47);
			panelGeneral.Controls.Add(mmLabel19);
			panelGeneral.Controls.Add(textBoxSpouseName);
			panelGeneral.Controls.Add(mmLabel9);
			panelGeneral.Controls.Add(textBoxMotherName);
			panelGeneral.Controls.Add(mmLabel6);
			panelGeneral.Controls.Add(textBoxFatherName);
			panelGeneral.Controls.Add(mmLabel4);
			panelGeneral.Controls.Add(dateTimePPExpiryDate);
			panelGeneral.Controls.Add(mmLabel3);
			panelGeneral.Controls.Add(dateTimePPIssueDate);
			panelGeneral.Controls.Add(textBoxPPIssuePlace);
			panelGeneral.Controls.Add(mmLabel1);
			panelGeneral.Controls.Add(mmLabel2);
			panelGeneral.Controls.Add(textBoxBirthPlace);
			panelGeneral.Controls.Add(mmLabel33);
			panelGeneral.Controls.Add(mmLabel51);
			panelGeneral.Controls.Add(dateTimeBirthDate);
			panelGeneral.Controls.Add(textBoxAge);
			panelGeneral.Controls.Add(mmLabel31);
			panelGeneral.Controls.Add(comboBoxGender);
			panelGeneral.Controls.Add(comboBoxNationality);
			panelGeneral.Controls.Add(mmLabel5);
			panelGeneral.Controls.Add(labelCandidateNumber);
			panelGeneral.Controls.Add(lblDescriptions);
			panelGeneral.Controls.Add(textBoxCode);
			panelGeneral.Controls.Add(textBoxSurName);
			panelGeneral.Controls.Add(textBoxGivenName);
			panelGeneral.Controls.Add(labelGivenName);
			panelGeneral.Enabled = false;
			panelGeneral.Location = new System.Drawing.Point(-2, 0);
			panelGeneral.Name = "panelGeneral";
			panelGeneral.Size = new System.Drawing.Size(814, 529);
			panelGeneral.TabIndex = 0;
			comboBoxApplicationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxApplicationType.FormattingEnabled = true;
			comboBoxApplicationType.Items.AddRange(new object[6]
			{
				"New",
				"Replace",
				"Mission-Visa",
				"Transfer-Visa",
				"WP-6 Months",
				"WP-1 Year"
			});
			comboBoxApplicationType.Location = new System.Drawing.Point(391, 40);
			comboBoxApplicationType.Name = "comboBoxApplicationType";
			comboBoxApplicationType.Size = new System.Drawing.Size(92, 21);
			comboBoxApplicationType.TabIndex = 152;
			comboBoxECRStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxECRStatus.FormattingEnabled = true;
			comboBoxECRStatus.Items.AddRange(new object[2]
			{
				"ECR",
				"ECNR"
			});
			comboBoxECRStatus.Location = new System.Drawing.Point(168, 387);
			comboBoxECRStatus.Name = "comboBoxECRStatus";
			comboBoxECRStatus.Size = new System.Drawing.Size(118, 21);
			comboBoxECRStatus.TabIndex = 14;
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(751, 345);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 146;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(702, 99);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 22;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance2;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(733, 176);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 23;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance3.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance3;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(694, 176);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 24;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance4;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(672, 42);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(128, 128);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 143;
			pictureBoxPhoto.TabStop = false;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(526, 313);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(47, 14);
			ultraFormattedLinkLabel1.TabIndex = 141;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Religion:";
			ultraFormattedLinkLabel1.Visible = false;
			appearance5.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance5;
			tabPageDetails.Controls.Add(panelRecruitment);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(810, 608);
			panelRecruitment.Controls.Add(mmLabel104);
			panelRecruitment.Controls.Add(textBoxDivisionname);
			panelRecruitment.Controls.Add(specialConditionComboBox);
			panelRecruitment.Controls.Add(mmLabel103);
			panelRecruitment.Controls.Add(agreementStatusComboBox);
			panelRecruitment.Controls.Add(mmLabel102);
			panelRecruitment.Controls.Add(comboBoxDivision);
			panelRecruitment.Controls.Add(mmLabel81);
			panelRecruitment.Controls.Add(mmLabel76);
			panelRecruitment.Controls.Add(textBoxLanguageName);
			panelRecruitment.Controls.Add(textBoxQualificationName);
			panelRecruitment.Controls.Add(numericExperienceAbroad);
			panelRecruitment.Controls.Add(numericExperienceLocal);
			panelRecruitment.Controls.Add(comboBoxLanguage);
			panelRecruitment.Controls.Add(comboBoxQualification);
			panelRecruitment.Controls.Add(mmLabel75);
			panelRecruitment.Controls.Add(mmLabel74);
			panelRecruitment.Controls.Add(mmLabel73);
			panelRecruitment.Controls.Add(mmLabel71);
			panelRecruitment.Controls.Add(textBoxActualDesignationName);
			panelRecruitment.Controls.Add(textBoxThroughAgentName);
			panelRecruitment.Controls.Add(comboBoxPositionActual);
			panelRecruitment.Controls.Add(mmLabel68);
			panelRecruitment.Controls.Add(comboBoxAgentThrough);
			panelRecruitment.Controls.Add(mmLabel55);
			panelRecruitment.Controls.Add(textBoxRemarks);
			panelRecruitment.Controls.Add(comboBoxSelectionStatus);
			panelRecruitment.Controls.Add(mmLabel39);
			panelRecruitment.Controls.Add(mmLabel38);
			panelRecruitment.Controls.Add(textBoxSelectedAt);
			panelRecruitment.Controls.Add(dateTimeSelectedOn);
			panelRecruitment.Controls.Add(mmLabel37);
			panelRecruitment.Controls.Add(mmLabel36);
			panelRecruitment.Enabled = false;
			panelRecruitment.Location = new System.Drawing.Point(0, 0);
			panelRecruitment.Name = "panelRecruitment";
			panelRecruitment.Size = new System.Drawing.Size(810, 529);
			panelRecruitment.TabIndex = 0;
			textBoxDivisionname.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDivisionname.Location = new System.Drawing.Point(291, 173);
			textBoxDivisionname.MaxLength = 64;
			textBoxDivisionname.Name = "textBoxDivisionname";
			textBoxDivisionname.ReadOnly = true;
			textBoxDivisionname.Size = new System.Drawing.Size(287, 20);
			textBoxDivisionname.TabIndex = 12;
			textBoxDivisionname.TabStop = false;
			textBoxLanguageName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLanguageName.Location = new System.Drawing.Point(291, 151);
			textBoxLanguageName.MaxLength = 64;
			textBoxLanguageName.Name = "textBoxLanguageName";
			textBoxLanguageName.ReadOnly = true;
			textBoxLanguageName.Size = new System.Drawing.Size(287, 20);
			textBoxLanguageName.TabIndex = 10;
			textBoxLanguageName.TabStop = false;
			textBoxQualificationName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxQualificationName.Location = new System.Drawing.Point(291, 129);
			textBoxQualificationName.MaxLength = 64;
			textBoxQualificationName.Name = "textBoxQualificationName";
			textBoxQualificationName.ReadOnly = true;
			textBoxQualificationName.Size = new System.Drawing.Size(287, 20);
			textBoxQualificationName.TabIndex = 8;
			textBoxQualificationName.TabStop = false;
			numericExperienceAbroad.DecimalPlaces = 1;
			numericExperienceAbroad.Increment = new decimal(new int[4]
			{
				50,
				0,
				0,
				131072
			});
			numericExperienceAbroad.Location = new System.Drawing.Point(166, 263);
			numericExperienceAbroad.Name = "numericExperienceAbroad";
			numericExperienceAbroad.Size = new System.Drawing.Size(56, 20);
			numericExperienceAbroad.TabIndex = 16;
			numericExperienceLocal.DecimalPlaces = 1;
			numericExperienceLocal.Increment = new decimal(new int[4]
			{
				50,
				0,
				0,
				131072
			});
			numericExperienceLocal.Location = new System.Drawing.Point(166, 241);
			numericExperienceLocal.Name = "numericExperienceLocal";
			numericExperienceLocal.Size = new System.Drawing.Size(56, 20);
			numericExperienceLocal.TabIndex = 15;
			textBoxActualDesignationName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxActualDesignationName.Location = new System.Drawing.Point(291, 107);
			textBoxActualDesignationName.MaxLength = 64;
			textBoxActualDesignationName.Name = "textBoxActualDesignationName";
			textBoxActualDesignationName.ReadOnly = true;
			textBoxActualDesignationName.Size = new System.Drawing.Size(287, 20);
			textBoxActualDesignationName.TabIndex = 6;
			textBoxActualDesignationName.TabStop = false;
			textBoxThroughAgentName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxThroughAgentName.Location = new System.Drawing.Point(291, 85);
			textBoxThroughAgentName.MaxLength = 64;
			textBoxThroughAgentName.Name = "textBoxThroughAgentName";
			textBoxThroughAgentName.ReadOnly = true;
			textBoxThroughAgentName.Size = new System.Drawing.Size(287, 20);
			textBoxThroughAgentName.TabIndex = 4;
			textBoxThroughAgentName.TabStop = false;
			ultraTabPageControl4.Controls.Add(panelVisaProcess);
			ultraTabPageControl4.Location = new System.Drawing.Point(2, 21);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(810, 608);
			panelVisaProcess.Controls.Add(panelVisaIMG);
			panelVisaProcess.Controls.Add(panelVisaMOL);
			panelVisaProcess.Enabled = false;
			panelVisaProcess.Location = new System.Drawing.Point(-2, 0);
			panelVisaProcess.Name = "panelVisaProcess";
			panelVisaProcess.Size = new System.Drawing.Size(814, 607);
			panelVisaProcess.TabIndex = 0;
			panelVisaIMG.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelVisaIMG.Controls.Add(mmLabel100);
			panelVisaIMG.Controls.Add(datetimepickerExpectedArrivalDate);
			panelVisaIMG.Controls.Add(mmLabel99);
			panelVisaIMG.Controls.Add(comboBoxApprovalStatusIMG);
			panelVisaIMG.Controls.Add(mmLabel97);
			panelVisaIMG.Controls.Add(textBoxIMGRemarks);
			panelVisaIMG.Controls.Add(mmLabel77);
			panelVisaIMG.Controls.Add(dateTimeVisaCopyToAgentOn);
			panelVisaIMG.Controls.Add(comboBoxVisaAppliedThroughIMG);
			panelVisaIMG.Controls.Add(dateTimeVisaExpiryDate);
			panelVisaIMG.Controls.Add(dateTimeVisaIssueDate);
			panelVisaIMG.Controls.Add(mmLabel87);
			panelVisaIMG.Controls.Add(textBoxVisaIssuePlaceIMG);
			panelVisaIMG.Controls.Add(mmLabel86);
			panelVisaIMG.Controls.Add(textBoxVisaNumber);
			panelVisaIMG.Controls.Add(mmLabel85);
			panelVisaIMG.Controls.Add(mmLabel84);
			panelVisaIMG.Controls.Add(textBoxUIDNumberIMG);
			panelVisaIMG.Controls.Add(mmLabel83);
			panelVisaIMG.Controls.Add(dateTimeVisaPostedOn);
			panelVisaIMG.Controls.Add(mmLabel26);
			panelVisaIMG.Controls.Add(dateTimeApprovedOn);
			panelVisaIMG.Controls.Add(mmLabel28);
			panelVisaIMG.Controls.Add(mmLabel32);
			panelVisaIMG.Enabled = false;
			panelVisaIMG.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelVisaIMG.Location = new System.Drawing.Point(11, 334);
			panelVisaIMG.Name = "panelVisaIMG";
			panelVisaIMG.Size = new System.Drawing.Size(788, 268);
			panelVisaIMG.TabIndex = 102;
			panelVisaIMG.Text = "Visa Process (IMG)";
			comboBoxApprovalStatusIMG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxApprovalStatusIMG.FormattingEnabled = true;
			comboBoxApprovalStatusIMG.Items.AddRange(new object[4]
			{
				"Approved",
				"Rejected",
				"Hold",
				""
			});
			comboBoxApprovalStatusIMG.Location = new System.Drawing.Point(158, 78);
			comboBoxApprovalStatusIMG.Name = "comboBoxApprovalStatusIMG";
			comboBoxApprovalStatusIMG.Size = new System.Drawing.Size(123, 21);
			comboBoxApprovalStatusIMG.TabIndex = 4;
			comboBoxVisaAppliedThroughIMG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxVisaAppliedThroughIMG.FormattingEnabled = true;
			comboBoxVisaAppliedThroughIMG.Items.AddRange(new object[2]
			{
				"Online",
				"Manual"
			});
			comboBoxVisaAppliedThroughIMG.Location = new System.Drawing.Point(158, 32);
			comboBoxVisaAppliedThroughIMG.Name = "comboBoxVisaAppliedThroughIMG";
			comboBoxVisaAppliedThroughIMG.Size = new System.Drawing.Size(124, 21);
			comboBoxVisaAppliedThroughIMG.TabIndex = 0;
			panelVisaMOL.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelVisaMOL.Controls.Add(datetimeSignedAORcvd);
			panelVisaMOL.Controls.Add(mmLabel105);
			panelVisaMOL.Controls.Add(mmLabel98);
			panelVisaMOL.Controls.Add(textBoxMOLRemarks);
			panelVisaMOL.Controls.Add(datetimepickerAOtypingDate);
			panelVisaMOL.Controls.Add(mmLabel96);
			panelVisaMOL.Controls.Add(comboBoxApprovalStatusMOL);
			panelVisaMOL.Controls.Add(textBoxRegnNumber);
			panelVisaMOL.Controls.Add(mmLabel94);
			panelVisaMOL.Controls.Add(mmLabel95);
			panelVisaMOL.Controls.Add(textBoxVisaDesignationName);
			panelVisaMOL.Controls.Add(comboBoxPositionVisa);
			panelVisaMOL.Controls.Add(mmLabel67);
			panelVisaMOL.Controls.Add(textBoxSponsorName);
			panelVisaMOL.Controls.Add(comboBoxSponsor);
			panelVisaMOL.Controls.Add(mmLabel70);
			panelVisaMOL.Controls.Add(comboBoxBGTypeMOL);
			panelVisaMOL.Controls.Add(mmLabel82);
			panelVisaMOL.Controls.Add(dateTimeApprovalFeePaidOnMOL);
			panelVisaMOL.Controls.Add(textBoxTempWPNo);
			panelVisaMOL.Controls.Add(mmLabel80);
			panelVisaMOL.Controls.Add(dateTimeApprovalValidTillMOL);
			panelVisaMOL.Controls.Add(mmLabel79);
			panelVisaMOL.Controls.Add(dateTimeBGPaidOnMOL);
			panelVisaMOL.Controls.Add(mmLabel25);
			panelVisaMOL.Controls.Add(mmLabel24);
			panelVisaMOL.Controls.Add(dateTimeApprovalDateMOL);
			panelVisaMOL.Controls.Add(mmLabel22);
			panelVisaMOL.Controls.Add(textBoxMOLMBNo);
			panelVisaMOL.Controls.Add(mmLabel7);
			panelVisaMOL.Controls.Add(dateTimeApplTypingDateMOL);
			panelVisaMOL.Controls.Add(mmLabel30);
			panelVisaMOL.Enabled = false;
			panelVisaMOL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelVisaMOL.Location = new System.Drawing.Point(11, 13);
			panelVisaMOL.Name = "panelVisaMOL";
			panelVisaMOL.Size = new System.Drawing.Size(788, 321);
			panelVisaMOL.TabIndex = 101;
			panelVisaMOL.Text = "Visa Process (MOL)";
			comboBoxApprovalStatusMOL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxApprovalStatusMOL.FormattingEnabled = true;
			comboBoxApprovalStatusMOL.Items.AddRange(new object[4]
			{
				"Approved",
				"Rejected",
				"Hold",
				""
			});
			comboBoxApprovalStatusMOL.Location = new System.Drawing.Point(158, 173);
			comboBoxApprovalStatusMOL.Name = "comboBoxApprovalStatusMOL";
			comboBoxApprovalStatusMOL.Size = new System.Drawing.Size(123, 21);
			comboBoxApprovalStatusMOL.TabIndex = 8;
			textBoxVisaDesignationName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVisaDesignationName.Location = new System.Drawing.Point(283, 152);
			textBoxVisaDesignationName.MaxLength = 64;
			textBoxVisaDesignationName.Name = "textBoxVisaDesignationName";
			textBoxVisaDesignationName.ReadOnly = true;
			textBoxVisaDesignationName.Size = new System.Drawing.Size(287, 20);
			textBoxVisaDesignationName.TabIndex = 7;
			textBoxVisaDesignationName.TabStop = false;
			textBoxSponsorName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSponsorName.Location = new System.Drawing.Point(283, 130);
			textBoxSponsorName.MaxLength = 64;
			textBoxSponsorName.Name = "textBoxSponsorName";
			textBoxSponsorName.ReadOnly = true;
			textBoxSponsorName.Size = new System.Drawing.Size(287, 20);
			textBoxSponsorName.TabIndex = 5;
			textBoxSponsorName.TabStop = false;
			comboBoxBGTypeMOL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxBGTypeMOL.FormattingEnabled = true;
			comboBoxBGTypeMOL.Items.AddRange(new object[2]
			{
				"Fixed",
				"Individual"
			});
			comboBoxBGTypeMOL.Location = new System.Drawing.Point(446, 234);
			comboBoxBGTypeMOL.Name = "comboBoxBGTypeMOL";
			comboBoxBGTypeMOL.Size = new System.Drawing.Size(124, 21);
			comboBoxBGTypeMOL.TabIndex = 14;
			ultraTabPageControl1.Controls.Add(panelArrival);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(810, 608);
			panelArrival.Controls.Add(buttonMakeEmployee);
			panelArrival.Controls.Add(comboBoxArrivalPort);
			panelArrival.Controls.Add(comboBoxCategory);
			panelArrival.Controls.Add(mmLabel29);
			panelArrival.Controls.Add(mmLabel88);
			panelArrival.Controls.Add(textBoxEmployeeNo);
			panelArrival.Controls.Add(mmLabel34);
			panelArrival.Controls.Add(dateTimeArrivedOn);
			panelArrival.Controls.Add(mmLabel27);
			panelArrival.Enabled = false;
			panelArrival.Location = new System.Drawing.Point(0, 0);
			panelArrival.Name = "panelArrival";
			panelArrival.Size = new System.Drawing.Size(810, 524);
			panelArrival.TabIndex = 0;
			ultraTabPageControl3.Controls.Add(panelWPRP);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(810, 608);
			panelWPRP.Controls.Add(panelMedicalReport);
			panelWPRP.Controls.Add(panelAGT);
			panelWPRP.Enabled = false;
			panelWPRP.Location = new System.Drawing.Point(-2, 0);
			panelWPRP.Name = "panelWPRP";
			panelWPRP.Size = new System.Drawing.Size(814, 529);
			panelWPRP.TabIndex = 0;
			panelMedicalReport.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelMedicalReport.Controls.Add(comboBoxProcessType);
			panelMedicalReport.Controls.Add(dateTimeRPExpiryDate);
			panelMedicalReport.Controls.Add(mmLabel56);
			panelMedicalReport.Controls.Add(mmLabel65);
			panelMedicalReport.Controls.Add(dateTimeRPIssueDate);
			panelMedicalReport.Controls.Add(mmLabel64);
			panelMedicalReport.Controls.Add(textBoxRPIssuePlace);
			panelMedicalReport.Controls.Add(mmLabel63);
			panelMedicalReport.Controls.Add(dateTimePassportCollectedOnRP);
			panelMedicalReport.Controls.Add(mmLabel52);
			panelMedicalReport.Controls.Add(dateTimeSubmittedZajilOnRP);
			panelMedicalReport.Controls.Add(mmLabel50);
			panelMedicalReport.Controls.Add(dateTimeApplApprovedOnRP);
			panelMedicalReport.Controls.Add(mmLabel49);
			panelMedicalReport.Controls.Add(dateTimeApplPostedOnRP);
			panelMedicalReport.Controls.Add(mmLabel48);
			panelMedicalReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelMedicalReport.Location = new System.Drawing.Point(12, 298);
			panelMedicalReport.Name = "panelMedicalReport";
			panelMedicalReport.Size = new System.Drawing.Size(788, 221);
			panelMedicalReport.TabIndex = 140;
			panelMedicalReport.Text = "RP Submission Details";
			comboBoxProcessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxProcessType.FormattingEnabled = true;
			comboBoxProcessType.Items.AddRange(new object[2]
			{
				"Online",
				"Manual"
			});
			comboBoxProcessType.Location = new System.Drawing.Point(157, 32);
			comboBoxProcessType.Name = "comboBoxProcessType";
			comboBoxProcessType.Size = new System.Drawing.Size(124, 21);
			comboBoxProcessType.TabIndex = 7;
			panelAGT.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelAGT.Controls.Add(dateTimeSignedAGTRecvd);
			panelAGT.Controls.Add(mmLabel106);
			panelAGT.Controls.Add(textBoxAGTMBNo);
			panelAGT.Controls.Add(mmLabel91);
			panelAGT.Controls.Add(mmLabel90);
			panelAGT.Controls.Add(comboBoxAGTType);
			panelAGT.Controls.Add(textBoxPersonIDNo);
			panelAGT.Controls.Add(mmLabel66);
			panelAGT.Controls.Add(textBoxWPIssuePlace);
			panelAGT.Controls.Add(dateTimeWPExpiryDate);
			panelAGT.Controls.Add(dateTimeWPIssueDate);
			panelAGT.Controls.Add(mmLabel62);
			panelAGT.Controls.Add(mmLabel61);
			panelAGT.Controls.Add(mmLabel60);
			panelAGT.Controls.Add(textBoxWPNo);
			panelAGT.Controls.Add(dateTimeAGTSubmittedOn);
			panelAGT.Controls.Add(mmLabel57);
			panelAGT.Controls.Add(mmLabel58);
			panelAGT.Controls.Add(dateTimeAGTTypedOn);
			panelAGT.Controls.Add(mmLabel59);
			panelAGT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelAGT.Location = new System.Drawing.Point(12, 24);
			panelAGT.Name = "panelAGT";
			panelAGT.Size = new System.Drawing.Size(788, 254);
			panelAGT.TabIndex = 139;
			panelAGT.Text = "AGT/WP Submission Details";
			comboBoxAGTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxAGTType.FormattingEnabled = true;
			comboBoxAGTType.Items.AddRange(new object[2]
			{
				"Limited",
				"Unlimited"
			});
			comboBoxAGTType.Location = new System.Drawing.Point(157, 24);
			comboBoxAGTType.Name = "comboBoxAGTType";
			comboBoxAGTType.Size = new System.Drawing.Size(124, 21);
			comboBoxAGTType.TabIndex = 0;
			ultraTabPageControl2.Controls.Add(panelMedicalEmirates);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(810, 608);
			panelMedicalEmirates.Controls.Add(panelEmirates);
			panelMedicalEmirates.Controls.Add(panelMedicalDetail);
			panelMedicalEmirates.Controls.Add(textBoxMedicalNote);
			panelMedicalEmirates.Controls.Add(mmLabel78);
			panelMedicalEmirates.Enabled = false;
			panelMedicalEmirates.Location = new System.Drawing.Point(-2, 0);
			panelMedicalEmirates.Name = "panelMedicalEmirates";
			panelMedicalEmirates.Size = new System.Drawing.Size(814, 529);
			panelMedicalEmirates.TabIndex = 0;
			panelEmirates.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelEmirates.Controls.Add(dateTimeValidityEID);
			panelEmirates.Controls.Add(dateTimeCollectedOnEID);
			panelEmirates.Controls.Add(mmLabel42);
			panelEmirates.Controls.Add(mmLabel43);
			panelEmirates.Controls.Add(dateTimeAttendedDateEID);
			panelEmirates.Controls.Add(mmLabel44);
			panelEmirates.Controls.Add(textBoxNationalID);
			panelEmirates.Controls.Add(mmLabel45);
			panelEmirates.Controls.Add(dateTimeApplTypingDateEID);
			panelEmirates.Controls.Add(mmLabel46);
			panelEmirates.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelEmirates.Location = new System.Drawing.Point(12, 179);
			panelEmirates.Name = "panelEmirates";
			panelEmirates.Size = new System.Drawing.Size(788, 155);
			panelEmirates.TabIndex = 68;
			panelEmirates.Text = "Emirates ID Details";
			panelMedicalDetail.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			panelMedicalDetail.Controls.Add(textBoxHealthCardNo);
			panelMedicalDetail.Controls.Add(mmLabel72);
			panelMedicalDetail.Controls.Add(comboBoxMedicalResult);
			panelMedicalDetail.Controls.Add(mmLabel89);
			panelMedicalDetail.Controls.Add(dateTimeMedicalCollectedOn);
			panelMedicalDetail.Controls.Add(mmLabel41);
			panelMedicalDetail.Controls.Add(dateTimeMedicalAttendedOn);
			panelMedicalDetail.Controls.Add(mmLabel40);
			panelMedicalDetail.Controls.Add(dateTimeMedicalTypingOn);
			panelMedicalDetail.Controls.Add(mmLabel35);
			panelMedicalDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panelMedicalDetail.Location = new System.Drawing.Point(12, 21);
			panelMedicalDetail.Name = "panelMedicalDetail";
			panelMedicalDetail.Size = new System.Drawing.Size(788, 146);
			panelMedicalDetail.TabIndex = 67;
			panelMedicalDetail.Text = "Medical Details";
			comboBoxMedicalResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMedicalResult.FormattingEnabled = true;
			comboBoxMedicalResult.Items.AddRange(new object[2]
			{
				"Fit",
				"Unfit"
			});
			comboBoxMedicalResult.Location = new System.Drawing.Point(137, 119);
			comboBoxMedicalResult.Name = "comboBoxMedicalResult";
			comboBoxMedicalResult.Size = new System.Drawing.Size(124, 21);
			comboBoxMedicalResult.TabIndex = 4;
			tabPageUserDefined.Controls.Add(udfEntryGrid);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(810, 608);
			ultraTabPageControl5.Controls.Add(ultraGroupBox1);
			ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl5.Name = "ultraTabPageControl5";
			ultraTabPageControl5.Size = new System.Drawing.Size(810, 608);
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(mmLabel23);
			ultraGroupBox1.Controls.Add(textBoxComment);
			ultraGroupBox1.Controls.Add(mmLabel20);
			ultraGroupBox1.Controls.Add(textBoxPostalCode);
			ultraGroupBox1.Controls.Add(mmLabel18);
			ultraGroupBox1.Controls.Add(textBoxEmail);
			ultraGroupBox1.Controls.Add(mmLabel17);
			ultraGroupBox1.Controls.Add(textBoxMobile);
			ultraGroupBox1.Controls.Add(mmLabel16);
			ultraGroupBox1.Controls.Add(textBoxFax);
			ultraGroupBox1.Controls.Add(mmLabel15);
			ultraGroupBox1.Controls.Add(textBoxPhone2);
			ultraGroupBox1.Controls.Add(mmLabel14);
			ultraGroupBox1.Controls.Add(textBoxPhone1);
			ultraGroupBox1.Controls.Add(mmLabel12);
			ultraGroupBox1.Controls.Add(textBoxCountry);
			ultraGroupBox1.Controls.Add(mmLabel11);
			ultraGroupBox1.Controls.Add(textBoxState);
			ultraGroupBox1.Controls.Add(mmLabel13);
			ultraGroupBox1.Controls.Add(textBoxCity);
			ultraGroupBox1.Controls.Add(textBoxAddress3);
			ultraGroupBox1.Controls.Add(textBoxAddress2);
			ultraGroupBox1.Controls.Add(mmLabel10);
			ultraGroupBox1.Controls.Add(textBoxAddress1);
			ultraGroupBox1.Controls.Add(mmLabel8);
			ultraGroupBox1.Controls.Add(textBoxAddressID);
			ultraGroupBox1.Location = new System.Drawing.Point(10, 20);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(718, 205);
			ultraGroupBox1.TabIndex = 22;
			ultraGroupBox1.Text = "Address";
			ultraGroupBox1.Visible = false;
			ultraTabPageControl6.Controls.Add(panelSalaryDetails);
			ultraTabPageControl6.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl6.Name = "ultraTabPageControl6";
			ultraTabPageControl6.Size = new System.Drawing.Size(810, 608);
			panelSalaryDetails.Controls.Add(ultraTabControl1);
			panelSalaryDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			panelSalaryDetails.Enabled = false;
			panelSalaryDetails.Location = new System.Drawing.Point(0, 0);
			panelSalaryDetails.Name = "panelSalaryDetails";
			panelSalaryDetails.Size = new System.Drawing.Size(810, 608);
			panelSalaryDetails.TabIndex = 12;
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage2);
			ultraTabControl1.Controls.Add(ultraTabPageControl7);
			ultraTabControl1.Controls.Add(ultraTabPageControl8);
			ultraTabControl1.Controls.Add(ultraTabPageControl9);
			ultraTabControl1.Controls.Add(ultraTabPageControl10);
			ultraTabControl1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			ultraTabControl1.Location = new System.Drawing.Point(10, 22);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage2;
			ultraTabControl1.Size = new System.Drawing.Size(712, 329);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControl1.TabIndex = 11;
			appearance6.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance6;
			ultraTab.TabPage = ultraTabPageControl7;
			ultraTab.Text = "&PayrollItems";
			ultraTab2.TabPage = ultraTabPageControl8;
			ultraTab2.Text = "&Deductions";
			ultraTab3.TabPage = ultraTabPageControl9;
			ultraTab3.Text = "Other &Benefits";
			ultraTab4.TabPage = ultraTabPageControl10;
			ultraTab4.Text = "&Tickets";
			ultraTab4.Visible = false;
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4
			});
			ultraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Standard;
			ultraTabSharedControlsPage2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
			ultraTabSharedControlsPage2.Size = new System.Drawing.Size(710, 308);
			ultraTabControl.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl.Controls.Add(tabPageGeneral);
			ultraTabControl.Controls.Add(tabPageDetails);
			ultraTabControl.Controls.Add(tabPageUserDefined);
			ultraTabControl.Controls.Add(ultraTabPageControl1);
			ultraTabControl.Controls.Add(ultraTabPageControl2);
			ultraTabControl.Controls.Add(ultraTabPageControl3);
			ultraTabControl.Controls.Add(ultraTabPageControl4);
			ultraTabControl.Controls.Add(ultraTabPageControl5);
			ultraTabControl.Controls.Add(ultraTabPageControl6);
			ultraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl.Location = new System.Drawing.Point(0, 60);
			ultraTabControl.MinTabWidth = 80;
			ultraTabControl.Name = "ultraTabControl";
			ultraTabControl.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl.Size = new System.Drawing.Size(814, 631);
			ultraTabControl.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl.TabIndex = 4;
			appearance7.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab5.Appearance = appearance7;
			ultraTab5.TabPage = tabPageGeneral;
			ultraTab5.Text = "&General";
			ultraTab6.TabPage = tabPageDetails;
			ultraTab6.Text = "&Recruitment";
			ultraTab7.TabPage = ultraTabPageControl4;
			ultraTab7.Text = "Visa Process";
			ultraTab8.TabPage = ultraTabPageControl1;
			ultraTab8.Text = "&Arrival";
			ultraTab9.TabPage = ultraTabPageControl3;
			ultraTab9.Text = "&WP/RP";
			ultraTab10.TabPage = ultraTabPageControl2;
			ultraTab10.Text = "&Medical/Emirates";
			ultraTab11.TabPage = tabPageUserDefined;
			ultraTab11.Text = "&User Defined";
			ultraTab12.TabPage = ultraTabPageControl5;
			ultraTab12.Text = "Container";
			ultraTab12.Visible = false;
			ultraTab13.TabPage = ultraTabPageControl6;
			ultraTab13.Text = "Salary Details";
			ultraTabControl.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[9]
			{
				ultraTab5,
				ultraTab6,
				ultraTab7,
				ultraTab8,
				ultraTab9,
				ultraTab10,
				ultraTab11,
				ultraTab12,
				ultraTab13
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(810, 608);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 691);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(814, 40);
			panelButtons.TabIndex = 99;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripButtonEmployee,
				toolStripSeparator4,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(814, 31);
			toolStrip1.TabIndex = 2;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripButtonEmployee.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonEmployee.Image");
			toolStripButtonEmployee.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonEmployee.Name = "toolStripButtonEmployee";
			toolStripButtonEmployee.Size = new System.Drawing.Size(119, 28);
			toolStripButtonEmployee.Text = "Make Employee";
			toolStripButtonEmployee.Visible = false;
			toolStripButtonEmployee.Click += new System.EventHandler(toolStripBtnMakeEmployee_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panel1.Controls.Add(labelCancelled);
			panel1.Controls.Add(labelCategory);
			panel1.Controls.Add(labelCustomerNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(814, 29);
			panel1.TabIndex = 3;
			labelCancelled.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelCancelled.BackColor = System.Drawing.Color.White;
			labelCancelled.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelCancelled.ForeColor = System.Drawing.Color.DarkRed;
			labelCancelled.Location = new System.Drawing.Point(683, 3);
			labelCancelled.Name = "labelCancelled";
			labelCancelled.Size = new System.Drawing.Size(126, 23);
			labelCancelled.TabIndex = 148;
			labelCancelled.Text = "CANCELLED";
			labelCancelled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelCancelled.Visible = false;
			labelCategory.AutoSize = true;
			labelCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelCategory.Location = new System.Drawing.Point(550, 8);
			labelCategory.Name = "labelCategory";
			labelCategory.Size = new System.Drawing.Size(0, 13);
			labelCategory.TabIndex = 3;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3]
			{
				dependentsToolStripMenuItem,
				documentsToolStripMenuItem,
				skillsToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(138, 70);
			dependentsToolStripMenuItem.Name = "dependentsToolStripMenuItem";
			dependentsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			dependentsToolStripMenuItem.Text = "Dependents";
			dependentsToolStripMenuItem.Click += new System.EventHandler(dependentsToolStripMenuItem_Click);
			documentsToolStripMenuItem.Name = "documentsToolStripMenuItem";
			documentsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			documentsToolStripMenuItem.Text = "Documents";
			documentsToolStripMenuItem.Click += new System.EventHandler(documentsToolStripMenuItem_Click);
			skillsToolStripMenuItem.Name = "skillsToolStripMenuItem";
			skillsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
			skillsToolStripMenuItem.Text = "Skills";
			skillsToolStripMenuItem.Click += new System.EventHandler(skillsToolStripMenuItem_Click);
			openFileDialog1.DefaultExt = "JPG";
			openFileDialog1.Filter = "Picture Files|*.jpg";
			mmLabel101.AutoSize = true;
			mmLabel101.BackColor = System.Drawing.Color.Transparent;
			mmLabel101.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel101.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel101.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel101.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel101.IsFieldHeader = false;
			mmLabel101.IsRequired = false;
			mmLabel101.Location = new System.Drawing.Point(325, 43);
			mmLabel101.Name = "mmLabel101";
			mmLabel101.PenWidth = 1f;
			mmLabel101.ShowBorder = false;
			mmLabel101.Size = new System.Drawing.Size(60, 13);
			mmLabel101.TabIndex = 151;
			mmLabel101.Text = "Appl.Type:";
			datetimePickerSystemdate.Checked = false;
			datetimePickerSystemdate.CustomFormat = " dd-MMM-yyyy";
			datetimePickerSystemdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimePickerSystemdate.Location = new System.Drawing.Point(364, 387);
			datetimePickerSystemdate.Name = "datetimePickerSystemdate";
			datetimePickerSystemdate.ShowCheckBox = true;
			datetimePickerSystemdate.Size = new System.Drawing.Size(119, 20);
			datetimePickerSystemdate.TabIndex = 16;
			datetimePickerSystemdate.Value = new System.DateTime(0L);
			mmLabel93.AutoSize = true;
			mmLabel93.BackColor = System.Drawing.Color.Transparent;
			mmLabel93.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel93.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel93.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel93.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel93.IsFieldHeader = false;
			mmLabel93.IsRequired = false;
			mmLabel93.Location = new System.Drawing.Point(293, 391);
			mmLabel93.Name = "mmLabel93";
			mmLabel93.PenWidth = 1f;
			mmLabel93.ShowBorder = false;
			mmLabel93.Size = new System.Drawing.Size(75, 13);
			mmLabel93.TabIndex = 15;
			mmLabel93.Text = "System Date :";
			mmLabel92.AutoSize = true;
			mmLabel92.BackColor = System.Drawing.Color.Transparent;
			mmLabel92.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel92.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel92.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel92.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel92.IsFieldHeader = false;
			mmLabel92.IsRequired = false;
			mmLabel92.Location = new System.Drawing.Point(14, 387);
			mmLabel92.Name = "mmLabel92";
			mmLabel92.PenWidth = 1f;
			mmLabel92.ShowBorder = false;
			mmLabel92.Size = new System.Drawing.Size(65, 13);
			mmLabel92.TabIndex = 149;
			mmLabel92.Text = "ECR Status:";
			mmLabel69.AutoSize = true;
			mmLabel69.BackColor = System.Drawing.Color.Transparent;
			mmLabel69.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel69.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel69.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel69.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel69.IsFieldHeader = false;
			mmLabel69.IsRequired = false;
			mmLabel69.Location = new System.Drawing.Point(15, 89);
			mmLabel69.Name = "mmLabel69";
			mmLabel69.PenWidth = 1f;
			mmLabel69.ShowBorder = false;
			mmLabel69.Size = new System.Drawing.Size(67, 13);
			mmLabel69.TabIndex = 147;
			mmLabel69.Text = "Sur Name :";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(169, 434);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(631, 70);
			textBoxNote.TabIndex = 18;
			mmLabel54.AutoSize = true;
			mmLabel54.BackColor = System.Drawing.Color.Transparent;
			mmLabel54.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel54.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel54.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel54.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel54.IsFieldHeader = false;
			mmLabel54.IsRequired = false;
			mmLabel54.Location = new System.Drawing.Point(15, 439);
			mmLabel54.Name = "mmLabel54";
			mmLabel54.PenWidth = 1f;
			mmLabel54.ShowBorder = false;
			mmLabel54.Size = new System.Drawing.Size(77, 13);
			mmLabel54.TabIndex = 145;
			mmLabel54.Text = "General Note :";
			mmLabel53.AutoSize = true;
			mmLabel53.BackColor = System.Drawing.Color.Transparent;
			mmLabel53.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel53.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel53.IsFieldHeader = false;
			mmLabel53.IsRequired = false;
			mmLabel53.Location = new System.Drawing.Point(15, 42);
			mmLabel53.Name = "mmLabel53";
			mmLabel53.PenWidth = 1f;
			mmLabel53.ShowBorder = false;
			mmLabel53.Size = new System.Drawing.Size(80, 13);
			mmLabel53.TabIndex = 144;
			mmLabel53.Text = "Passport No :";
			textBoxPassportNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxPassportNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxPassportNo.BackColor = System.Drawing.Color.White;
			textBoxPassportNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxPassportNo.CustomReportFieldName = "";
			textBoxPassportNo.CustomReportKey = "";
			textBoxPassportNo.CustomReportValueType = 1;
			textBoxPassportNo.IsComboTextBox = false;
			textBoxPassportNo.IsModified = false;
			textBoxPassportNo.Location = new System.Drawing.Point(169, 42);
			textBoxPassportNo.MaxLength = 20;
			textBoxPassportNo.Name = "textBoxPassportNo";
			textBoxPassportNo.Size = new System.Drawing.Size(146, 20);
			textBoxPassportNo.TabIndex = 0;
			textBoxPPAddress.BackColor = System.Drawing.Color.White;
			textBoxPPAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxPPAddress.CustomReportFieldName = "";
			textBoxPPAddress.CustomReportKey = "";
			textBoxPPAddress.CustomReportValueType = 1;
			textBoxPPAddress.IsComboTextBox = false;
			textBoxPPAddress.IsModified = false;
			textBoxPPAddress.Location = new System.Drawing.Point(168, 330);
			textBoxPPAddress.MaxLength = 255;
			textBoxPPAddress.Multiline = true;
			textBoxPPAddress.Name = "textBoxPPAddress";
			textBoxPPAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxPPAddress.Size = new System.Drawing.Size(314, 55);
			textBoxPPAddress.TabIndex = 13;
			mmLabel21.AutoSize = true;
			mmLabel21.BackColor = System.Drawing.Color.Transparent;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel21.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(523, 292);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(69, 13);
			mmLabel21.TabIndex = 142;
			mmLabel21.Text = "Blood Group:";
			mmLabel21.Visible = false;
			textBoxBloodGroup.AutoCompleteCustomSource.AddRange(new string[8]
			{
				"A+",
				"A-",
				"B+",
				"B-",
				"AB+",
				"AB-",
				"O+",
				"O-"
			});
			textBoxBloodGroup.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxBloodGroup.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxBloodGroup.BackColor = System.Drawing.Color.White;
			textBoxBloodGroup.CustomReportFieldName = "";
			textBoxBloodGroup.CustomReportKey = "";
			textBoxBloodGroup.CustomReportValueType = 1;
			textBoxBloodGroup.IsComboTextBox = false;
			textBoxBloodGroup.IsModified = false;
			textBoxBloodGroup.Location = new System.Drawing.Point(604, 287);
			textBoxBloodGroup.MaxLength = 3;
			textBoxBloodGroup.Name = "textBoxBloodGroup";
			textBoxBloodGroup.Size = new System.Drawing.Size(72, 20);
			textBoxBloodGroup.TabIndex = 20;
			textBoxBloodGroup.Visible = false;
			comboBoxReligion.Assigned = false;
			comboBoxReligion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxReligion.CustomReportFieldName = "";
			comboBoxReligion.CustomReportKey = "";
			comboBoxReligion.CustomReportValueType = 1;
			comboBoxReligion.DescriptionTextBox = null;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			appearance8.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxReligion.DisplayLayout.Appearance = appearance8;
			comboBoxReligion.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxReligion.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance9.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.GroupByBox.Appearance = appearance9;
			appearance10.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReligion.DisplayLayout.GroupByBox.BandLabelAppearance = appearance10;
			comboBoxReligion.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance11.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance11.BackColor2 = System.Drawing.SystemColors.Control;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReligion.DisplayLayout.GroupByBox.PromptAppearance = appearance11;
			comboBoxReligion.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxReligion.DisplayLayout.MaxRowScrollRegions = 1;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxReligion.DisplayLayout.Override.ActiveCellAppearance = appearance12;
			appearance13.BackColor = System.Drawing.SystemColors.Highlight;
			appearance13.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxReligion.DisplayLayout.Override.ActiveRowAppearance = appearance13;
			comboBoxReligion.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxReligion.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.Override.CardAreaAppearance = appearance14;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			appearance15.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxReligion.DisplayLayout.Override.CellAppearance = appearance15;
			comboBoxReligion.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxReligion.DisplayLayout.Override.CellPadding = 0;
			appearance16.BackColor = System.Drawing.SystemColors.Control;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.Override.GroupByRowAppearance = appearance16;
			appearance17.TextHAlignAsString = "Left";
			comboBoxReligion.DisplayLayout.Override.HeaderAppearance = appearance17;
			comboBoxReligion.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxReligion.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.BorderColor = System.Drawing.Color.Silver;
			comboBoxReligion.DisplayLayout.Override.RowAppearance = appearance18;
			comboBoxReligion.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance19.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxReligion.DisplayLayout.Override.TemplateAddRowAppearance = appearance19;
			comboBoxReligion.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxReligion.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxReligion.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxReligion.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxReligion.Editable = true;
			comboBoxReligion.FilterString = "";
			comboBoxReligion.HasAllAccount = false;
			comboBoxReligion.HasCustom = false;
			comboBoxReligion.IsDataLoaded = false;
			comboBoxReligion.Location = new System.Drawing.Point(604, 311);
			comboBoxReligion.MaxDropDownItems = 12;
			comboBoxReligion.Name = "comboBoxReligion";
			comboBoxReligion.ShowInactiveItems = false;
			comboBoxReligion.ShowQuickAdd = true;
			comboBoxReligion.Size = new System.Drawing.Size(196, 20);
			comboBoxReligion.TabIndex = 21;
			comboBoxReligion.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxReligion.Visible = false;
			comboBoxMaritalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMaritalStatus.FormattingEnabled = true;
			comboBoxMaritalStatus.Location = new System.Drawing.Point(604, 263);
			comboBoxMaritalStatus.Name = "comboBoxMaritalStatus";
			comboBoxMaritalStatus.SelectedID = 0;
			comboBoxMaritalStatus.Size = new System.Drawing.Size(117, 21);
			comboBoxMaritalStatus.TabIndex = 19;
			comboBoxMaritalStatus.Visible = false;
			mmLabel47.AutoSize = true;
			mmLabel47.BackColor = System.Drawing.Color.Transparent;
			mmLabel47.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel47.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel47.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel47.IsFieldHeader = false;
			mmLabel47.IsRequired = false;
			mmLabel47.Location = new System.Drawing.Point(523, 268);
			mmLabel47.Name = "mmLabel47";
			mmLabel47.PenWidth = 1f;
			mmLabel47.ShowBorder = false;
			mmLabel47.Size = new System.Drawing.Size(77, 13);
			mmLabel47.TabIndex = 140;
			mmLabel47.Text = "Marital Status:";
			mmLabel47.Visible = false;
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(15, 333);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(68, 13);
			mmLabel19.TabIndex = 139;
			mmLabel19.Text = "PP Address :";
			textBoxSpouseName.BackColor = System.Drawing.Color.White;
			textBoxSpouseName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSpouseName.CustomReportFieldName = "";
			textBoxSpouseName.CustomReportKey = "";
			textBoxSpouseName.CustomReportValueType = 1;
			textBoxSpouseName.IsComboTextBox = false;
			textBoxSpouseName.IsModified = false;
			textBoxSpouseName.IsRequired = true;
			textBoxSpouseName.Location = new System.Drawing.Point(169, 308);
			textBoxSpouseName.MaxLength = 64;
			textBoxSpouseName.Name = "textBoxSpouseName";
			textBoxSpouseName.Size = new System.Drawing.Size(314, 20);
			textBoxSpouseName.TabIndex = 12;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(15, 311);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(79, 13);
			mmLabel9.TabIndex = 138;
			mmLabel9.Text = "Spouse Name :";
			textBoxMotherName.BackColor = System.Drawing.Color.White;
			textBoxMotherName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxMotherName.CustomReportFieldName = "";
			textBoxMotherName.CustomReportKey = "";
			textBoxMotherName.CustomReportValueType = 1;
			textBoxMotherName.IsComboTextBox = false;
			textBoxMotherName.IsModified = false;
			textBoxMotherName.IsRequired = true;
			textBoxMotherName.Location = new System.Drawing.Point(169, 286);
			textBoxMotherName.MaxLength = 64;
			textBoxMotherName.Name = "textBoxMotherName";
			textBoxMotherName.Size = new System.Drawing.Size(314, 20);
			textBoxMotherName.TabIndex = 11;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(15, 289);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(78, 13);
			mmLabel6.TabIndex = 137;
			mmLabel6.Text = "Mother Name :";
			textBoxFatherName.BackColor = System.Drawing.Color.White;
			textBoxFatherName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFatherName.CustomReportFieldName = "";
			textBoxFatherName.CustomReportKey = "";
			textBoxFatherName.CustomReportValueType = 1;
			textBoxFatherName.IsComboTextBox = false;
			textBoxFatherName.IsModified = false;
			textBoxFatherName.IsRequired = true;
			textBoxFatherName.Location = new System.Drawing.Point(169, 264);
			textBoxFatherName.MaxLength = 64;
			textBoxFatherName.Name = "textBoxFatherName";
			textBoxFatherName.Size = new System.Drawing.Size(314, 20);
			textBoxFatherName.TabIndex = 10;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(15, 267);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(76, 13);
			mmLabel4.TabIndex = 136;
			mmLabel4.Text = "Father Name :";
			dateTimePPExpiryDate.Checked = false;
			dateTimePPExpiryDate.CustomFormat = " dd-MMM-yyyy";
			dateTimePPExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePPExpiryDate.Location = new System.Drawing.Point(169, 242);
			dateTimePPExpiryDate.Name = "dateTimePPExpiryDate";
			dateTimePPExpiryDate.ShowCheckBox = true;
			dateTimePPExpiryDate.Size = new System.Drawing.Size(147, 20);
			dateTimePPExpiryDate.TabIndex = 9;
			dateTimePPExpiryDate.Value = new System.DateTime(0L);
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(15, 246);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(85, 13);
			mmLabel3.TabIndex = 135;
			mmLabel3.Text = "PP Expiry Date :";
			dateTimePPIssueDate.Checked = false;
			dateTimePPIssueDate.CustomFormat = " dd-MMM-yyyy";
			dateTimePPIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePPIssueDate.Location = new System.Drawing.Point(169, 219);
			dateTimePPIssueDate.Name = "dateTimePPIssueDate";
			dateTimePPIssueDate.ShowCheckBox = true;
			dateTimePPIssueDate.Size = new System.Drawing.Size(147, 20);
			dateTimePPIssueDate.TabIndex = 8;
			dateTimePPIssueDate.Value = new System.DateTime(0L);
			textBoxPPIssuePlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxPPIssuePlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxPPIssuePlace.BackColor = System.Drawing.Color.White;
			textBoxPPIssuePlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxPPIssuePlace.CustomReportFieldName = "";
			textBoxPPIssuePlace.CustomReportKey = "";
			textBoxPPIssuePlace.CustomReportValueType = 1;
			textBoxPPIssuePlace.IsComboTextBox = false;
			textBoxPPIssuePlace.IsModified = false;
			textBoxPPIssuePlace.Location = new System.Drawing.Point(169, 197);
			textBoxPPIssuePlace.MaxLength = 30;
			textBoxPPIssuePlace.Name = "textBoxPPIssuePlace";
			textBoxPPIssuePlace.Size = new System.Drawing.Size(314, 20);
			textBoxPPIssuePlace.TabIndex = 7;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(15, 201);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(96, 13);
			mmLabel1.TabIndex = 133;
			mmLabel1.Text = "PP Place of Issue :";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(15, 224);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(81, 13);
			mmLabel2.TabIndex = 134;
			mmLabel2.Text = "PP Issue Date :";
			textBoxBirthPlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxBirthPlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxBirthPlace.BackColor = System.Drawing.Color.White;
			textBoxBirthPlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxBirthPlace.CustomReportFieldName = "";
			textBoxBirthPlace.CustomReportKey = "";
			textBoxBirthPlace.CustomReportValueType = 1;
			textBoxBirthPlace.IsComboTextBox = false;
			textBoxBirthPlace.IsModified = false;
			textBoxBirthPlace.Location = new System.Drawing.Point(169, 175);
			textBoxBirthPlace.MaxLength = 30;
			textBoxBirthPlace.Name = "textBoxBirthPlace";
			textBoxBirthPlace.Size = new System.Drawing.Size(314, 20);
			textBoxBirthPlace.TabIndex = 6;
			mmLabel33.AutoSize = true;
			mmLabel33.BackColor = System.Drawing.Color.Transparent;
			mmLabel33.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel33.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel33.IsFieldHeader = false;
			mmLabel33.IsRequired = false;
			mmLabel33.Location = new System.Drawing.Point(15, 178);
			mmLabel33.Name = "mmLabel33";
			mmLabel33.PenWidth = 1f;
			mmLabel33.ShowBorder = false;
			mmLabel33.Size = new System.Drawing.Size(64, 13);
			mmLabel33.TabIndex = 132;
			mmLabel33.Text = "Birth Place :";
			mmLabel51.AutoSize = true;
			mmLabel51.BackColor = System.Drawing.Color.Transparent;
			mmLabel51.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel51.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel51.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel51.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel51.IsFieldHeader = false;
			mmLabel51.IsRequired = false;
			mmLabel51.Location = new System.Drawing.Point(288, 156);
			mmLabel51.Name = "mmLabel51";
			mmLabel51.PenWidth = 1f;
			mmLabel51.ShowBorder = false;
			mmLabel51.Size = new System.Drawing.Size(30, 13);
			mmLabel51.TabIndex = 131;
			mmLabel51.Text = "Age:";
			dateTimeBirthDate.Checked = false;
			dateTimeBirthDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeBirthDate.Location = new System.Drawing.Point(169, 153);
			dateTimeBirthDate.Name = "dateTimeBirthDate";
			dateTimeBirthDate.ShowCheckBox = true;
			dateTimeBirthDate.Size = new System.Drawing.Size(117, 20);
			dateTimeBirthDate.TabIndex = 5;
			dateTimeBirthDate.Value = new System.DateTime(0L);
			textBoxAge.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxAge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxAge.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAge.CustomReportFieldName = "";
			textBoxAge.CustomReportKey = "";
			textBoxAge.CustomReportValueType = 1;
			textBoxAge.IsComboTextBox = false;
			textBoxAge.IsModified = false;
			textBoxAge.Location = new System.Drawing.Point(319, 153);
			textBoxAge.MaxLength = 30;
			textBoxAge.Name = "textBoxAge";
			textBoxAge.ReadOnly = true;
			textBoxAge.Size = new System.Drawing.Size(50, 20);
			textBoxAge.TabIndex = 112;
			textBoxAge.TabStop = false;
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(15, 156);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(62, 13);
			mmLabel31.TabIndex = 130;
			mmLabel31.Text = "Birth Date :";
			comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxGender.FormattingEnabled = true;
			comboBoxGender.Location = new System.Drawing.Point(169, 130);
			comboBoxGender.Name = "comboBoxGender";
			comboBoxGender.SelectedID = 0;
			comboBoxGender.Size = new System.Drawing.Size(95, 21);
			comboBoxGender.TabIndex = 4;
			comboBoxNationality.Assigned = false;
			comboBoxNationality.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxNationality.CustomReportFieldName = "";
			comboBoxNationality.CustomReportKey = "";
			comboBoxNationality.CustomReportValueType = 1;
			comboBoxNationality.DescriptionTextBox = null;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			appearance20.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxNationality.DisplayLayout.Appearance = appearance20;
			comboBoxNationality.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxNationality.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance21.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.GroupByBox.Appearance = appearance21;
			appearance22.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.BandLabelAppearance = appearance22;
			comboBoxNationality.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance23.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance23.BackColor2 = System.Drawing.SystemColors.Control;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.PromptAppearance = appearance23;
			comboBoxNationality.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxNationality.DisplayLayout.MaxRowScrollRegions = 1;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			appearance24.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxNationality.DisplayLayout.Override.ActiveCellAppearance = appearance24;
			appearance25.BackColor = System.Drawing.SystemColors.Highlight;
			appearance25.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxNationality.DisplayLayout.Override.ActiveRowAppearance = appearance25;
			comboBoxNationality.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxNationality.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.CardAreaAppearance = appearance26;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			appearance27.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxNationality.DisplayLayout.Override.CellAppearance = appearance27;
			comboBoxNationality.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxNationality.DisplayLayout.Override.CellPadding = 0;
			appearance28.BackColor = System.Drawing.SystemColors.Control;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.GroupByRowAppearance = appearance28;
			appearance29.TextHAlignAsString = "Left";
			comboBoxNationality.DisplayLayout.Override.HeaderAppearance = appearance29;
			comboBoxNationality.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxNationality.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			appearance30.BorderColor = System.Drawing.Color.Silver;
			comboBoxNationality.DisplayLayout.Override.RowAppearance = appearance30;
			comboBoxNationality.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance31.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxNationality.DisplayLayout.Override.TemplateAddRowAppearance = appearance31;
			comboBoxNationality.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxNationality.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxNationality.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxNationality.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxNationality.Editable = true;
			comboBoxNationality.FilterString = "";
			comboBoxNationality.HasAllAccount = false;
			comboBoxNationality.HasCustom = false;
			comboBoxNationality.IsDataLoaded = false;
			comboBoxNationality.Location = new System.Drawing.Point(169, 108);
			comboBoxNationality.MaxDropDownItems = 12;
			comboBoxNationality.Name = "comboBoxNationality";
			comboBoxNationality.ShowInactiveItems = false;
			comboBoxNationality.ShowQuickAdd = true;
			comboBoxNationality.Size = new System.Drawing.Size(196, 20);
			comboBoxNationality.TabIndex = 3;
			comboBoxNationality.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(15, 133);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(32, 13);
			mmLabel5.TabIndex = 114;
			mmLabel5.Text = "Sex :";
			labelCandidateNumber.AutoSize = true;
			labelCandidateNumber.BackColor = System.Drawing.Color.Transparent;
			labelCandidateNumber.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCandidateNumber.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCandidateNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCandidateNumber.IsFieldHeader = false;
			labelCandidateNumber.IsRequired = true;
			labelCandidateNumber.Location = new System.Drawing.Point(15, 412);
			labelCandidateNumber.Name = "labelCandidateNumber";
			labelCandidateNumber.PenWidth = 1f;
			labelCandidateNumber.ShowBorder = false;
			labelCandidateNumber.Size = new System.Drawing.Size(124, 13);
			labelCandidateNumber.TabIndex = 103;
			labelCandidateNumber.Text = "VS Application Code :";
			labelCandidateNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			lblDescriptions.AutoSize = true;
			lblDescriptions.BackColor = System.Drawing.Color.Transparent;
			lblDescriptions.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblDescriptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDescriptions.IsFieldHeader = false;
			lblDescriptions.IsRequired = false;
			lblDescriptions.Location = new System.Drawing.Point(15, 112);
			lblDescriptions.Name = "lblDescriptions";
			lblDescriptions.PenWidth = 1f;
			lblDescriptions.ShowBorder = false;
			lblDescriptions.Size = new System.Drawing.Size(65, 13);
			lblDescriptions.TabIndex = 111;
			lblDescriptions.Text = "Nationality :";
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(168, 411);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(197, 20);
			textBoxCode.TabIndex = 17;
			textBoxSurName.BackColor = System.Drawing.Color.White;
			textBoxSurName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSurName.CustomReportFieldName = "";
			textBoxSurName.CustomReportKey = "";
			textBoxSurName.CustomReportValueType = 1;
			textBoxSurName.IsComboTextBox = false;
			textBoxSurName.IsModified = false;
			textBoxSurName.IsRequired = true;
			textBoxSurName.Location = new System.Drawing.Point(169, 86);
			textBoxSurName.MaxLength = 64;
			textBoxSurName.Name = "textBoxSurName";
			textBoxSurName.Size = new System.Drawing.Size(314, 20);
			textBoxSurName.TabIndex = 2;
			textBoxGivenName.BackColor = System.Drawing.Color.White;
			textBoxGivenName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxGivenName.CustomReportFieldName = "";
			textBoxGivenName.CustomReportKey = "";
			textBoxGivenName.CustomReportValueType = 1;
			textBoxGivenName.IsComboTextBox = false;
			textBoxGivenName.IsModified = false;
			textBoxGivenName.IsRequired = true;
			textBoxGivenName.Location = new System.Drawing.Point(169, 64);
			textBoxGivenName.MaxLength = 64;
			textBoxGivenName.Name = "textBoxGivenName";
			textBoxGivenName.Size = new System.Drawing.Size(314, 20);
			textBoxGivenName.TabIndex = 1;
			labelGivenName.AutoSize = true;
			labelGivenName.BackColor = System.Drawing.Color.Transparent;
			labelGivenName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelGivenName.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelGivenName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelGivenName.IsFieldHeader = false;
			labelGivenName.IsRequired = false;
			labelGivenName.Location = new System.Drawing.Point(15, 67);
			labelGivenName.Name = "labelGivenName";
			labelGivenName.PenWidth = 1f;
			labelGivenName.ShowBorder = false;
			labelGivenName.Size = new System.Drawing.Size(80, 13);
			labelGivenName.TabIndex = 106;
			labelGivenName.Text = "Given Name :";
			labelGivenName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel104.AutoSize = true;
			mmLabel104.BackColor = System.Drawing.Color.Transparent;
			mmLabel104.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel104.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel104.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel104.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel104.IsFieldHeader = false;
			mmLabel104.IsRequired = false;
			mmLabel104.Location = new System.Drawing.Point(16, 176);
			mmLabel104.Name = "mmLabel104";
			mmLabel104.PenWidth = 1f;
			mmLabel104.ShowBorder = false;
			mmLabel104.Size = new System.Drawing.Size(50, 13);
			mmLabel104.TabIndex = 142;
			mmLabel104.Text = "Division :";
			specialConditionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			specialConditionComboBox.FormattingEnabled = true;
			specialConditionComboBox.Items.AddRange(new object[2]
			{
				"Yes",
				"No"
			});
			specialConditionComboBox.Location = new System.Drawing.Point(166, 218);
			specialConditionComboBox.Name = "specialConditionComboBox";
			specialConditionComboBox.SelectedID = 0;
			specialConditionComboBox.Size = new System.Drawing.Size(124, 21);
			specialConditionComboBox.TabIndex = 14;
			mmLabel103.AutoSize = true;
			mmLabel103.BackColor = System.Drawing.Color.Transparent;
			mmLabel103.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel103.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel103.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel103.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel103.IsFieldHeader = false;
			mmLabel103.IsRequired = false;
			mmLabel103.Location = new System.Drawing.Point(15, 221);
			mmLabel103.Name = "mmLabel103";
			mmLabel103.PenWidth = 1f;
			mmLabel103.ShowBorder = false;
			mmLabel103.Size = new System.Drawing.Size(95, 13);
			mmLabel103.TabIndex = 141;
			mmLabel103.Text = "Special Condition :";
			agreementStatusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			agreementStatusComboBox.FormattingEnabled = true;
			agreementStatusComboBox.Items.AddRange(new object[2]
			{
				"Limited",
				"Unlimited"
			});
			agreementStatusComboBox.Location = new System.Drawing.Point(166, 195);
			agreementStatusComboBox.Name = "agreementStatusComboBox";
			agreementStatusComboBox.SelectedID = 0;
			agreementStatusComboBox.Size = new System.Drawing.Size(124, 21);
			agreementStatusComboBox.TabIndex = 13;
			mmLabel102.AutoSize = true;
			mmLabel102.BackColor = System.Drawing.Color.Transparent;
			mmLabel102.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel102.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel102.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel102.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel102.IsFieldHeader = false;
			mmLabel102.IsRequired = false;
			mmLabel102.Location = new System.Drawing.Point(15, 198);
			mmLabel102.Name = "mmLabel102";
			mmLabel102.PenWidth = 1f;
			mmLabel102.ShowBorder = false;
			mmLabel102.Size = new System.Drawing.Size(101, 13);
			mmLabel102.TabIndex = 139;
			mmLabel102.Text = "Agreement Status :";
			comboBoxDivision.Assigned = false;
			comboBoxDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDivision.CustomReportFieldName = "";
			comboBoxDivision.CustomReportKey = "";
			comboBoxDivision.CustomReportValueType = 1;
			comboBoxDivision.DescriptionTextBox = textBoxDivisionname;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDivision.DisplayLayout.Appearance = appearance32;
			comboBoxDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance33.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.GroupByBox.Appearance = appearance33;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance34;
			comboBoxDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance35.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance35.BackColor2 = System.Drawing.SystemColors.Control;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance35;
			comboBoxDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			appearance36.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDivision.DisplayLayout.Override.ActiveCellAppearance = appearance36;
			appearance37.BackColor = System.Drawing.SystemColors.Highlight;
			appearance37.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDivision.DisplayLayout.Override.ActiveRowAppearance = appearance37;
			comboBoxDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.CardAreaAppearance = appearance38;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			appearance39.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDivision.DisplayLayout.Override.CellAppearance = appearance39;
			comboBoxDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDivision.DisplayLayout.Override.CellPadding = 0;
			appearance40.BackColor = System.Drawing.SystemColors.Control;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.GroupByRowAppearance = appearance40;
			appearance41.TextHAlignAsString = "Left";
			comboBoxDivision.DisplayLayout.Override.HeaderAppearance = appearance41;
			comboBoxDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance42.BackColor = System.Drawing.SystemColors.Window;
			appearance42.BorderColor = System.Drawing.Color.Silver;
			comboBoxDivision.DisplayLayout.Override.RowAppearance = appearance42;
			comboBoxDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance43.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance43;
			comboBoxDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDivision.Editable = true;
			comboBoxDivision.FilterString = "";
			comboBoxDivision.HasAllAccount = false;
			comboBoxDivision.HasCustom = false;
			comboBoxDivision.IsDataLoaded = false;
			comboBoxDivision.Location = new System.Drawing.Point(166, 173);
			comboBoxDivision.MaxDropDownItems = 12;
			comboBoxDivision.Name = "comboBoxDivision";
			comboBoxDivision.ShowInactiveItems = false;
			comboBoxDivision.ShowQuickAdd = true;
			comboBoxDivision.Size = new System.Drawing.Size(124, 20);
			comboBoxDivision.TabIndex = 11;
			comboBoxDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel81.AutoSize = true;
			mmLabel81.BackColor = System.Drawing.Color.Transparent;
			mmLabel81.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel81.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel81.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel81.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel81.IsFieldHeader = false;
			mmLabel81.IsRequired = false;
			mmLabel81.Location = new System.Drawing.Point(228, 267);
			mmLabel81.Name = "mmLabel81";
			mmLabel81.PenWidth = 1f;
			mmLabel81.ShowBorder = false;
			mmLabel81.Size = new System.Drawing.Size(34, 13);
			mmLabel81.TabIndex = 135;
			mmLabel81.Text = "Years";
			mmLabel76.AutoSize = true;
			mmLabel76.BackColor = System.Drawing.Color.Transparent;
			mmLabel76.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel76.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel76.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel76.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel76.IsFieldHeader = false;
			mmLabel76.IsRequired = false;
			mmLabel76.Location = new System.Drawing.Point(227, 244);
			mmLabel76.Name = "mmLabel76";
			mmLabel76.PenWidth = 1f;
			mmLabel76.ShowBorder = false;
			mmLabel76.Size = new System.Drawing.Size(34, 13);
			mmLabel76.TabIndex = 134;
			mmLabel76.Text = "Years";
			comboBoxLanguage.Assigned = false;
			comboBoxLanguage.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLanguage.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLanguage.CustomReportFieldName = "";
			comboBoxLanguage.CustomReportKey = "";
			comboBoxLanguage.CustomReportValueType = 1;
			comboBoxLanguage.DescriptionTextBox = textBoxLanguageName;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLanguage.DisplayLayout.Appearance = appearance44;
			comboBoxLanguage.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLanguage.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance45.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLanguage.DisplayLayout.GroupByBox.Appearance = appearance45;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLanguage.DisplayLayout.GroupByBox.BandLabelAppearance = appearance46;
			comboBoxLanguage.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance47.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance47.BackColor2 = System.Drawing.SystemColors.Control;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLanguage.DisplayLayout.GroupByBox.PromptAppearance = appearance47;
			comboBoxLanguage.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLanguage.DisplayLayout.MaxRowScrollRegions = 1;
			appearance48.BackColor = System.Drawing.SystemColors.Window;
			appearance48.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLanguage.DisplayLayout.Override.ActiveCellAppearance = appearance48;
			appearance49.BackColor = System.Drawing.SystemColors.Highlight;
			appearance49.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLanguage.DisplayLayout.Override.ActiveRowAppearance = appearance49;
			comboBoxLanguage.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLanguage.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLanguage.DisplayLayout.Override.CardAreaAppearance = appearance50;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			appearance51.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLanguage.DisplayLayout.Override.CellAppearance = appearance51;
			comboBoxLanguage.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLanguage.DisplayLayout.Override.CellPadding = 0;
			appearance52.BackColor = System.Drawing.SystemColors.Control;
			appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance52.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLanguage.DisplayLayout.Override.GroupByRowAppearance = appearance52;
			appearance53.TextHAlignAsString = "Left";
			comboBoxLanguage.DisplayLayout.Override.HeaderAppearance = appearance53;
			comboBoxLanguage.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLanguage.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance54.BackColor = System.Drawing.SystemColors.Window;
			appearance54.BorderColor = System.Drawing.Color.Silver;
			comboBoxLanguage.DisplayLayout.Override.RowAppearance = appearance54;
			comboBoxLanguage.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance55.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLanguage.DisplayLayout.Override.TemplateAddRowAppearance = appearance55;
			comboBoxLanguage.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLanguage.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLanguage.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLanguage.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLanguage.Editable = true;
			comboBoxLanguage.FilterString = "";
			comboBoxLanguage.HasAllAccount = false;
			comboBoxLanguage.HasCustom = false;
			comboBoxLanguage.IsDataLoaded = false;
			comboBoxLanguage.Location = new System.Drawing.Point(166, 151);
			comboBoxLanguage.MaxDropDownItems = 12;
			comboBoxLanguage.Name = "comboBoxLanguage";
			comboBoxLanguage.ShowInactiveItems = false;
			comboBoxLanguage.ShowQuickAdd = true;
			comboBoxLanguage.Size = new System.Drawing.Size(124, 20);
			comboBoxLanguage.TabIndex = 9;
			comboBoxLanguage.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxQualification.Assigned = false;
			comboBoxQualification.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxQualification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxQualification.CustomReportFieldName = "";
			comboBoxQualification.CustomReportKey = "";
			comboBoxQualification.CustomReportValueType = 1;
			comboBoxQualification.DescriptionTextBox = textBoxQualificationName;
			appearance56.BackColor = System.Drawing.SystemColors.Window;
			appearance56.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxQualification.DisplayLayout.Appearance = appearance56;
			comboBoxQualification.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxQualification.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance57.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.GroupByBox.Appearance = appearance57;
			appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxQualification.DisplayLayout.GroupByBox.BandLabelAppearance = appearance58;
			comboBoxQualification.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance59.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance59.BackColor2 = System.Drawing.SystemColors.Control;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance59.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxQualification.DisplayLayout.GroupByBox.PromptAppearance = appearance59;
			comboBoxQualification.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxQualification.DisplayLayout.MaxRowScrollRegions = 1;
			appearance60.BackColor = System.Drawing.SystemColors.Window;
			appearance60.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxQualification.DisplayLayout.Override.ActiveCellAppearance = appearance60;
			appearance61.BackColor = System.Drawing.SystemColors.Highlight;
			appearance61.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxQualification.DisplayLayout.Override.ActiveRowAppearance = appearance61;
			comboBoxQualification.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxQualification.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance62.BackColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.Override.CardAreaAppearance = appearance62;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			appearance63.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxQualification.DisplayLayout.Override.CellAppearance = appearance63;
			comboBoxQualification.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxQualification.DisplayLayout.Override.CellPadding = 0;
			appearance64.BackColor = System.Drawing.SystemColors.Control;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.Override.GroupByRowAppearance = appearance64;
			appearance65.TextHAlignAsString = "Left";
			comboBoxQualification.DisplayLayout.Override.HeaderAppearance = appearance65;
			comboBoxQualification.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxQualification.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance66.BackColor = System.Drawing.SystemColors.Window;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			comboBoxQualification.DisplayLayout.Override.RowAppearance = appearance66;
			comboBoxQualification.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance67.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxQualification.DisplayLayout.Override.TemplateAddRowAppearance = appearance67;
			comboBoxQualification.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxQualification.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxQualification.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxQualification.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxQualification.Editable = true;
			comboBoxQualification.FilterString = "";
			comboBoxQualification.HasAllAccount = false;
			comboBoxQualification.HasCustom = false;
			comboBoxQualification.IsDataLoaded = false;
			comboBoxQualification.Location = new System.Drawing.Point(166, 129);
			comboBoxQualification.MaxDropDownItems = 12;
			comboBoxQualification.Name = "comboBoxQualification";
			comboBoxQualification.ShowInactiveItems = false;
			comboBoxQualification.ShowQuickAdd = true;
			comboBoxQualification.Size = new System.Drawing.Size(124, 20);
			comboBoxQualification.TabIndex = 7;
			comboBoxQualification.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel75.AutoSize = true;
			mmLabel75.BackColor = System.Drawing.Color.Transparent;
			mmLabel75.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel75.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel75.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel75.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel75.IsFieldHeader = false;
			mmLabel75.IsRequired = false;
			mmLabel75.Location = new System.Drawing.Point(15, 267);
			mmLabel75.Name = "mmLabel75";
			mmLabel75.PenWidth = 1f;
			mmLabel75.ShowBorder = false;
			mmLabel75.Size = new System.Drawing.Size(112, 13);
			mmLabel75.TabIndex = 131;
			mmLabel75.Text = "Experience - Abroad :";
			mmLabel74.AutoSize = true;
			mmLabel74.BackColor = System.Drawing.Color.Transparent;
			mmLabel74.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel74.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel74.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel74.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel74.IsFieldHeader = false;
			mmLabel74.IsRequired = false;
			mmLabel74.Location = new System.Drawing.Point(16, 154);
			mmLabel74.Name = "mmLabel74";
			mmLabel74.PenWidth = 1f;
			mmLabel74.ShowBorder = false;
			mmLabel74.Size = new System.Drawing.Size(61, 13);
			mmLabel74.TabIndex = 130;
			mmLabel74.Text = "Language :";
			mmLabel73.AutoSize = true;
			mmLabel73.BackColor = System.Drawing.Color.Transparent;
			mmLabel73.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel73.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel73.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel73.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel73.IsFieldHeader = false;
			mmLabel73.IsRequired = false;
			mmLabel73.Location = new System.Drawing.Point(15, 245);
			mmLabel73.Name = "mmLabel73";
			mmLabel73.PenWidth = 1f;
			mmLabel73.ShowBorder = false;
			mmLabel73.Size = new System.Drawing.Size(101, 13);
			mmLabel73.TabIndex = 129;
			mmLabel73.Text = "Experience - Local :";
			mmLabel71.AutoSize = true;
			mmLabel71.BackColor = System.Drawing.Color.Transparent;
			mmLabel71.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel71.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel71.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel71.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel71.IsFieldHeader = false;
			mmLabel71.IsRequired = false;
			mmLabel71.Location = new System.Drawing.Point(15, 132);
			mmLabel71.Name = "mmLabel71";
			mmLabel71.PenWidth = 1f;
			mmLabel71.ShowBorder = false;
			mmLabel71.Size = new System.Drawing.Size(73, 13);
			mmLabel71.TabIndex = 128;
			mmLabel71.Text = "Qualification :";
			comboBoxPositionActual.Assigned = false;
			comboBoxPositionActual.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPositionActual.CustomReportFieldName = "";
			comboBoxPositionActual.CustomReportKey = "";
			comboBoxPositionActual.CustomReportValueType = 1;
			comboBoxPositionActual.DescriptionTextBox = textBoxActualDesignationName;
			appearance68.BackColor = System.Drawing.SystemColors.Window;
			appearance68.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPositionActual.DisplayLayout.Appearance = appearance68;
			comboBoxPositionActual.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPositionActual.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance69.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPositionActual.DisplayLayout.GroupByBox.Appearance = appearance69;
			appearance70.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPositionActual.DisplayLayout.GroupByBox.BandLabelAppearance = appearance70;
			comboBoxPositionActual.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance71.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance71.BackColor2 = System.Drawing.SystemColors.Control;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPositionActual.DisplayLayout.GroupByBox.PromptAppearance = appearance71;
			comboBoxPositionActual.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPositionActual.DisplayLayout.MaxRowScrollRegions = 1;
			appearance72.BackColor = System.Drawing.SystemColors.Window;
			appearance72.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPositionActual.DisplayLayout.Override.ActiveCellAppearance = appearance72;
			appearance73.BackColor = System.Drawing.SystemColors.Highlight;
			appearance73.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPositionActual.DisplayLayout.Override.ActiveRowAppearance = appearance73;
			comboBoxPositionActual.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPositionActual.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance74.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPositionActual.DisplayLayout.Override.CardAreaAppearance = appearance74;
			appearance75.BorderColor = System.Drawing.Color.Silver;
			appearance75.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPositionActual.DisplayLayout.Override.CellAppearance = appearance75;
			comboBoxPositionActual.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPositionActual.DisplayLayout.Override.CellPadding = 0;
			appearance76.BackColor = System.Drawing.SystemColors.Control;
			appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance76.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPositionActual.DisplayLayout.Override.GroupByRowAppearance = appearance76;
			appearance77.TextHAlignAsString = "Left";
			comboBoxPositionActual.DisplayLayout.Override.HeaderAppearance = appearance77;
			comboBoxPositionActual.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPositionActual.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance78.BackColor = System.Drawing.SystemColors.Window;
			appearance78.BorderColor = System.Drawing.Color.Silver;
			comboBoxPositionActual.DisplayLayout.Override.RowAppearance = appearance78;
			comboBoxPositionActual.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance79.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPositionActual.DisplayLayout.Override.TemplateAddRowAppearance = appearance79;
			comboBoxPositionActual.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPositionActual.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPositionActual.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPositionActual.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPositionActual.Editable = true;
			comboBoxPositionActual.FilterString = "";
			comboBoxPositionActual.HasAllAccount = false;
			comboBoxPositionActual.HasCustom = false;
			comboBoxPositionActual.IsDataLoaded = false;
			comboBoxPositionActual.Location = new System.Drawing.Point(166, 107);
			comboBoxPositionActual.MaxDropDownItems = 12;
			comboBoxPositionActual.Name = "comboBoxPositionActual";
			comboBoxPositionActual.ShowInactiveItems = false;
			comboBoxPositionActual.ShowQuickAdd = true;
			comboBoxPositionActual.Size = new System.Drawing.Size(124, 20);
			comboBoxPositionActual.TabIndex = 5;
			comboBoxPositionActual.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel68.AutoSize = true;
			mmLabel68.BackColor = System.Drawing.Color.Transparent;
			mmLabel68.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel68.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel68.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel68.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel68.IsFieldHeader = false;
			mmLabel68.IsRequired = false;
			mmLabel68.Location = new System.Drawing.Point(15, 109);
			mmLabel68.Name = "mmLabel68";
			mmLabel68.PenWidth = 1f;
			mmLabel68.ShowBorder = false;
			mmLabel68.Size = new System.Drawing.Size(103, 13);
			mmLabel68.TabIndex = 127;
			mmLabel68.Text = "Actual Designation :";
			comboBoxAgentThrough.Assigned = false;
			comboBoxAgentThrough.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAgentThrough.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAgentThrough.CustomReportFieldName = "";
			comboBoxAgentThrough.CustomReportKey = "";
			comboBoxAgentThrough.CustomReportValueType = 1;
			comboBoxAgentThrough.DescriptionTextBox = textBoxThroughAgentName;
			appearance80.BackColor = System.Drawing.SystemColors.Window;
			appearance80.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAgentThrough.DisplayLayout.Appearance = appearance80;
			comboBoxAgentThrough.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAgentThrough.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance81.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAgentThrough.DisplayLayout.GroupByBox.Appearance = appearance81;
			appearance82.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAgentThrough.DisplayLayout.GroupByBox.BandLabelAppearance = appearance82;
			comboBoxAgentThrough.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance83.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance83.BackColor2 = System.Drawing.SystemColors.Control;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAgentThrough.DisplayLayout.GroupByBox.PromptAppearance = appearance83;
			comboBoxAgentThrough.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAgentThrough.DisplayLayout.MaxRowScrollRegions = 1;
			appearance84.BackColor = System.Drawing.SystemColors.Window;
			appearance84.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAgentThrough.DisplayLayout.Override.ActiveCellAppearance = appearance84;
			appearance85.BackColor = System.Drawing.SystemColors.Highlight;
			appearance85.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAgentThrough.DisplayLayout.Override.ActiveRowAppearance = appearance85;
			comboBoxAgentThrough.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAgentThrough.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAgentThrough.DisplayLayout.Override.CardAreaAppearance = appearance86;
			appearance87.BorderColor = System.Drawing.Color.Silver;
			appearance87.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAgentThrough.DisplayLayout.Override.CellAppearance = appearance87;
			comboBoxAgentThrough.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAgentThrough.DisplayLayout.Override.CellPadding = 0;
			appearance88.BackColor = System.Drawing.SystemColors.Control;
			appearance88.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance88.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance88.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAgentThrough.DisplayLayout.Override.GroupByRowAppearance = appearance88;
			appearance89.TextHAlignAsString = "Left";
			comboBoxAgentThrough.DisplayLayout.Override.HeaderAppearance = appearance89;
			comboBoxAgentThrough.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAgentThrough.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance90.BackColor = System.Drawing.SystemColors.Window;
			appearance90.BorderColor = System.Drawing.Color.Silver;
			comboBoxAgentThrough.DisplayLayout.Override.RowAppearance = appearance90;
			comboBoxAgentThrough.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance91.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAgentThrough.DisplayLayout.Override.TemplateAddRowAppearance = appearance91;
			comboBoxAgentThrough.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAgentThrough.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAgentThrough.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAgentThrough.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAgentThrough.Editable = true;
			comboBoxAgentThrough.FilterString = "";
			comboBoxAgentThrough.HasAllAccount = false;
			comboBoxAgentThrough.HasCustom = false;
			comboBoxAgentThrough.IsDataLoaded = false;
			comboBoxAgentThrough.Location = new System.Drawing.Point(166, 85);
			comboBoxAgentThrough.MaxDropDownItems = 12;
			comboBoxAgentThrough.MaxLength = 100;
			comboBoxAgentThrough.Name = "comboBoxAgentThrough";
			comboBoxAgentThrough.ShowInactiveItems = false;
			comboBoxAgentThrough.ShowQuickAdd = true;
			comboBoxAgentThrough.Size = new System.Drawing.Size(124, 20);
			comboBoxAgentThrough.TabIndex = 3;
			comboBoxAgentThrough.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel55.AutoSize = true;
			mmLabel55.BackColor = System.Drawing.Color.Transparent;
			mmLabel55.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel55.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel55.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel55.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel55.IsFieldHeader = false;
			mmLabel55.IsRequired = false;
			mmLabel55.Location = new System.Drawing.Point(15, 289);
			mmLabel55.Name = "mmLabel55";
			mmLabel55.PenWidth = 1f;
			mmLabel55.ShowBorder = false;
			mmLabel55.Size = new System.Drawing.Size(91, 13);
			mmLabel55.TabIndex = 126;
			mmLabel55.Text = "Special Remarks :";
			textBoxRemarks.BackColor = System.Drawing.Color.White;
			textBoxRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.IsModified = false;
			textBoxRemarks.Location = new System.Drawing.Point(166, 285);
			textBoxRemarks.MaxLength = 255;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxRemarks.Size = new System.Drawing.Size(631, 136);
			textBoxRemarks.TabIndex = 17;
			comboBoxSelectionStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxSelectionStatus.FormattingEnabled = true;
			comboBoxSelectionStatus.Location = new System.Drawing.Point(166, 18);
			comboBoxSelectionStatus.Name = "comboBoxSelectionStatus";
			comboBoxSelectionStatus.SelectedID = 0;
			comboBoxSelectionStatus.Size = new System.Drawing.Size(124, 21);
			comboBoxSelectionStatus.TabIndex = 0;
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(15, 89);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(86, 13);
			mmLabel39.TabIndex = 125;
			mmLabel39.Text = "Through Agent :";
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(15, 67);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(69, 13);
			mmLabel38.TabIndex = 124;
			mmLabel38.Text = "Selected At :";
			textBoxSelectedAt.AutoCompleteCustomSource.AddRange(new string[8]
			{
				"A+",
				"A-",
				"B+",
				"B-",
				"AB+",
				"AB-",
				"O+",
				"O-"
			});
			textBoxSelectedAt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxSelectedAt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxSelectedAt.BackColor = System.Drawing.Color.White;
			textBoxSelectedAt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSelectedAt.CustomReportFieldName = "";
			textBoxSelectedAt.CustomReportKey = "";
			textBoxSelectedAt.CustomReportValueType = 1;
			textBoxSelectedAt.IsComboTextBox = false;
			textBoxSelectedAt.IsModified = false;
			textBoxSelectedAt.Location = new System.Drawing.Point(166, 63);
			textBoxSelectedAt.MaxLength = 30;
			textBoxSelectedAt.Name = "textBoxSelectedAt";
			textBoxSelectedAt.Size = new System.Drawing.Size(209, 20);
			textBoxSelectedAt.TabIndex = 2;
			dateTimeSelectedOn.Checked = false;
			dateTimeSelectedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeSelectedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeSelectedOn.Location = new System.Drawing.Point(166, 41);
			dateTimeSelectedOn.Name = "dateTimeSelectedOn";
			dateTimeSelectedOn.ShowCheckBox = true;
			dateTimeSelectedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeSelectedOn.TabIndex = 1;
			dateTimeSelectedOn.Value = new System.DateTime(0L);
			mmLabel37.AutoSize = true;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(15, 44);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(72, 13);
			mmLabel37.TabIndex = 123;
			mmLabel37.Text = "Selected On :";
			mmLabel36.AutoSize = true;
			mmLabel36.BackColor = System.Drawing.Color.Transparent;
			mmLabel36.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel36.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel36.IsFieldHeader = false;
			mmLabel36.IsRequired = false;
			mmLabel36.Location = new System.Drawing.Point(15, 21);
			mmLabel36.Name = "mmLabel36";
			mmLabel36.PenWidth = 1f;
			mmLabel36.ShowBorder = false;
			mmLabel36.Size = new System.Drawing.Size(91, 13);
			mmLabel36.TabIndex = 122;
			mmLabel36.Text = "Selection Status :";
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(8, 18);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(790, 539);
			udfEntryGrid.TabIndex = 1;
			buttonMakeEmployee.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonMakeEmployee.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonMakeEmployee.BackColor = System.Drawing.Color.DarkGray;
			buttonMakeEmployee.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonMakeEmployee.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonMakeEmployee.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonMakeEmployee.Enabled = false;
			buttonMakeEmployee.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonMakeEmployee.Location = new System.Drawing.Point(271, 89);
			buttonMakeEmployee.Name = "buttonMakeEmployee";
			buttonMakeEmployee.Size = new System.Drawing.Size(102, 24);
			buttonMakeEmployee.TabIndex = 4;
			buttonMakeEmployee.Text = "Make Employee";
			buttonMakeEmployee.UseVisualStyleBackColor = false;
			buttonMakeEmployee.Click += new System.EventHandler(toolStripBtnMakeEmployee_Click);
			comboBoxArrivalPort.Assigned = false;
			comboBoxArrivalPort.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxArrivalPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxArrivalPort.CustomReportFieldName = "";
			comboBoxArrivalPort.CustomReportKey = "";
			comboBoxArrivalPort.CustomReportValueType = 1;
			comboBoxArrivalPort.DescriptionTextBox = null;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			appearance92.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxArrivalPort.DisplayLayout.Appearance = appearance92;
			comboBoxArrivalPort.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxArrivalPort.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance93.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance93.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance93.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.Appearance = appearance93;
			appearance94.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.BandLabelAppearance = appearance94;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance95.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance95.BackColor2 = System.Drawing.SystemColors.Control;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.PromptAppearance = appearance95;
			comboBoxArrivalPort.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxArrivalPort.DisplayLayout.MaxRowScrollRegions = 1;
			appearance96.BackColor = System.Drawing.SystemColors.Window;
			appearance96.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxArrivalPort.DisplayLayout.Override.ActiveCellAppearance = appearance96;
			appearance97.BackColor = System.Drawing.SystemColors.Highlight;
			appearance97.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxArrivalPort.DisplayLayout.Override.ActiveRowAppearance = appearance97;
			comboBoxArrivalPort.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxArrivalPort.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			comboBoxArrivalPort.DisplayLayout.Override.CardAreaAppearance = appearance98;
			appearance99.BorderColor = System.Drawing.Color.Silver;
			appearance99.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxArrivalPort.DisplayLayout.Override.CellAppearance = appearance99;
			comboBoxArrivalPort.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxArrivalPort.DisplayLayout.Override.CellPadding = 0;
			appearance100.BackColor = System.Drawing.SystemColors.Control;
			appearance100.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance100.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance100.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance100.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArrivalPort.DisplayLayout.Override.GroupByRowAppearance = appearance100;
			appearance101.TextHAlignAsString = "Left";
			comboBoxArrivalPort.DisplayLayout.Override.HeaderAppearance = appearance101;
			comboBoxArrivalPort.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxArrivalPort.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance102.BackColor = System.Drawing.SystemColors.Window;
			appearance102.BorderColor = System.Drawing.Color.Silver;
			comboBoxArrivalPort.DisplayLayout.Override.RowAppearance = appearance102;
			comboBoxArrivalPort.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance103.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxArrivalPort.DisplayLayout.Override.TemplateAddRowAppearance = appearance103;
			comboBoxArrivalPort.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxArrivalPort.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxArrivalPort.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxArrivalPort.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxArrivalPort.Editable = true;
			comboBoxArrivalPort.FilterString = "";
			comboBoxArrivalPort.HasAllAccount = false;
			comboBoxArrivalPort.HasCustom = false;
			comboBoxArrivalPort.IsDataLoaded = false;
			comboBoxArrivalPort.Location = new System.Drawing.Point(136, 48);
			comboBoxArrivalPort.MaxDropDownItems = 12;
			comboBoxArrivalPort.Name = "comboBoxArrivalPort";
			comboBoxArrivalPort.ShowInactiveItems = false;
			comboBoxArrivalPort.ShowQuickAdd = true;
			comboBoxArrivalPort.Size = new System.Drawing.Size(129, 20);
			comboBoxArrivalPort.TabIndex = 1;
			comboBoxArrivalPort.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCategory.Assigned = false;
			comboBoxCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCategory.CustomReportFieldName = "";
			comboBoxCategory.CustomReportKey = "";
			comboBoxCategory.CustomReportValueType = 1;
			comboBoxCategory.DescriptionTextBox = null;
			appearance104.BackColor = System.Drawing.SystemColors.Window;
			appearance104.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCategory.DisplayLayout.Appearance = appearance104;
			comboBoxCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance105.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance105.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance105.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance105.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.GroupByBox.Appearance = appearance105;
			appearance106.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance106;
			comboBoxCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance107.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance107.BackColor2 = System.Drawing.SystemColors.Control;
			appearance107.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance107.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance107;
			comboBoxCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance108.BackColor = System.Drawing.SystemColors.Window;
			appearance108.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCategory.DisplayLayout.Override.ActiveCellAppearance = appearance108;
			appearance109.BackColor = System.Drawing.SystemColors.Highlight;
			appearance109.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCategory.DisplayLayout.Override.ActiveRowAppearance = appearance109;
			comboBoxCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance110.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.CardAreaAppearance = appearance110;
			appearance111.BorderColor = System.Drawing.Color.Silver;
			appearance111.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCategory.DisplayLayout.Override.CellAppearance = appearance111;
			comboBoxCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCategory.DisplayLayout.Override.CellPadding = 0;
			appearance112.BackColor = System.Drawing.SystemColors.Control;
			appearance112.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance112.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance112.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCategory.DisplayLayout.Override.GroupByRowAppearance = appearance112;
			appearance113.TextHAlignAsString = "Left";
			comboBoxCategory.DisplayLayout.Override.HeaderAppearance = appearance113;
			comboBoxCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance114.BackColor = System.Drawing.SystemColors.Window;
			appearance114.BorderColor = System.Drawing.Color.Silver;
			comboBoxCategory.DisplayLayout.Override.RowAppearance = appearance114;
			comboBoxCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance115.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance115;
			comboBoxCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCategory.Editable = true;
			comboBoxCategory.FilterString = "";
			comboBoxCategory.HasAllAccount = false;
			comboBoxCategory.HasCustom = false;
			comboBoxCategory.IsDataLoaded = false;
			comboBoxCategory.Location = new System.Drawing.Point(136, 70);
			comboBoxCategory.MaxDropDownItems = 12;
			comboBoxCategory.Name = "comboBoxCategory";
			comboBoxCategory.ShowInactiveItems = false;
			comboBoxCategory.ShowQuickAdd = true;
			comboBoxCategory.Size = new System.Drawing.Size(129, 20);
			comboBoxCategory.TabIndex = 2;
			comboBoxCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel29.AutoSize = true;
			mmLabel29.BackColor = System.Drawing.Color.Transparent;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(27, 50);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(31, 13);
			mmLabel29.TabIndex = 145;
			mmLabel29.Text = "Port:";
			mmLabel88.AutoSize = true;
			mmLabel88.BackColor = System.Drawing.Color.Transparent;
			mmLabel88.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel88.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel88.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel88.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel88.IsFieldHeader = false;
			mmLabel88.IsRequired = false;
			mmLabel88.Location = new System.Drawing.Point(27, 73);
			mmLabel88.Name = "mmLabel88";
			mmLabel88.PenWidth = 1f;
			mmLabel88.ShowBorder = false;
			mmLabel88.Size = new System.Drawing.Size(56, 13);
			mmLabel88.TabIndex = 144;
			mmLabel88.Text = "Category:";
			textBoxEmployeeNo.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeNo.CustomReportFieldName = "";
			textBoxEmployeeNo.CustomReportKey = "";
			textBoxEmployeeNo.CustomReportValueType = 1;
			textBoxEmployeeNo.IsComboTextBox = false;
			textBoxEmployeeNo.IsModified = false;
			textBoxEmployeeNo.Location = new System.Drawing.Point(136, 92);
			textBoxEmployeeNo.MaxLength = 15;
			textBoxEmployeeNo.Name = "textBoxEmployeeNo";
			textBoxEmployeeNo.ReadOnly = true;
			textBoxEmployeeNo.Size = new System.Drawing.Size(129, 20);
			textBoxEmployeeNo.TabIndex = 3;
			mmLabel34.AutoSize = true;
			mmLabel34.BackColor = System.Drawing.Color.Transparent;
			mmLabel34.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel34.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel34.IsFieldHeader = false;
			mmLabel34.IsRequired = false;
			mmLabel34.Location = new System.Drawing.Point(27, 94);
			mmLabel34.Name = "mmLabel34";
			mmLabel34.PenWidth = 1f;
			mmLabel34.ShowBorder = false;
			mmLabel34.Size = new System.Drawing.Size(73, 13);
			mmLabel34.TabIndex = 143;
			mmLabel34.Text = "Employee No:";
			dateTimeArrivedOn.Checked = false;
			dateTimeArrivedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeArrivedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeArrivedOn.Location = new System.Drawing.Point(136, 25);
			dateTimeArrivedOn.Name = "dateTimeArrivedOn";
			dateTimeArrivedOn.ShowCheckBox = true;
			dateTimeArrivedOn.Size = new System.Drawing.Size(129, 20);
			dateTimeArrivedOn.TabIndex = 0;
			dateTimeArrivedOn.Value = new System.DateTime(0L);
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(27, 28);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(63, 13);
			mmLabel27.TabIndex = 142;
			mmLabel27.Text = "Arrived On:";
			dateTimeValidityEID.Checked = false;
			dateTimeValidityEID.CustomFormat = " dd-MMM-yyyy";
			dateTimeValidityEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeValidityEID.Location = new System.Drawing.Point(137, 122);
			dateTimeValidityEID.Name = "dateTimeValidityEID";
			dateTimeValidityEID.ShowCheckBox = true;
			dateTimeValidityEID.Size = new System.Drawing.Size(124, 20);
			dateTimeValidityEID.TabIndex = 9;
			dateTimeValidityEID.Value = new System.DateTime(0L);
			dateTimeCollectedOnEID.Checked = false;
			dateTimeCollectedOnEID.CustomFormat = " dd-MMM-yyyy";
			dateTimeCollectedOnEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeCollectedOnEID.Location = new System.Drawing.Point(137, 78);
			dateTimeCollectedOnEID.Name = "dateTimeCollectedOnEID";
			dateTimeCollectedOnEID.ShowCheckBox = true;
			dateTimeCollectedOnEID.Size = new System.Drawing.Size(124, 20);
			dateTimeCollectedOnEID.TabIndex = 7;
			dateTimeCollectedOnEID.Value = new System.DateTime(0L);
			mmLabel42.AutoSize = true;
			mmLabel42.BackColor = System.Drawing.Color.Transparent;
			mmLabel42.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel42.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel42.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel42.IsFieldHeader = false;
			mmLabel42.IsRequired = false;
			mmLabel42.Location = new System.Drawing.Point(20, 84);
			mmLabel42.Name = "mmLabel42";
			mmLabel42.PenWidth = 1f;
			mmLabel42.ShowBorder = false;
			mmLabel42.Size = new System.Drawing.Size(89, 13);
			mmLabel42.TabIndex = 75;
			mmLabel42.Text = "ID Collected On :";
			mmLabel43.AutoSize = true;
			mmLabel43.BackColor = System.Drawing.Color.Transparent;
			mmLabel43.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel43.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel43.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel43.IsFieldHeader = false;
			mmLabel43.IsRequired = false;
			mmLabel43.Location = new System.Drawing.Point(20, 127);
			mmLabel43.Name = "mmLabel43";
			mmLabel43.PenWidth = 1f;
			mmLabel43.ShowBorder = false;
			mmLabel43.Size = new System.Drawing.Size(62, 13);
			mmLabel43.TabIndex = 73;
			mmLabel43.Text = "ID Validity :";
			dateTimeAttendedDateEID.Checked = false;
			dateTimeAttendedDateEID.CustomFormat = " dd-MMM-yyyy";
			dateTimeAttendedDateEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeAttendedDateEID.Location = new System.Drawing.Point(137, 56);
			dateTimeAttendedDateEID.Name = "dateTimeAttendedDateEID";
			dateTimeAttendedDateEID.ShowCheckBox = true;
			dateTimeAttendedDateEID.Size = new System.Drawing.Size(124, 20);
			dateTimeAttendedDateEID.TabIndex = 6;
			dateTimeAttendedDateEID.Value = new System.DateTime(0L);
			mmLabel44.AutoSize = true;
			mmLabel44.BackColor = System.Drawing.Color.Transparent;
			mmLabel44.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel44.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel44.IsFieldHeader = false;
			mmLabel44.IsRequired = false;
			mmLabel44.Location = new System.Drawing.Point(21, 62);
			mmLabel44.Name = "mmLabel44";
			mmLabel44.PenWidth = 1f;
			mmLabel44.ShowBorder = false;
			mmLabel44.Size = new System.Drawing.Size(90, 13);
			mmLabel44.TabIndex = 71;
			mmLabel44.Text = "Attended for ID :";
			textBoxNationalID.BackColor = System.Drawing.Color.White;
			textBoxNationalID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxNationalID.CustomReportFieldName = "";
			textBoxNationalID.CustomReportKey = "";
			textBoxNationalID.CustomReportValueType = 1;
			textBoxNationalID.IsComboTextBox = false;
			textBoxNationalID.IsModified = false;
			textBoxNationalID.Location = new System.Drawing.Point(137, 100);
			textBoxNationalID.MaxLength = 50;
			textBoxNationalID.Name = "textBoxNationalID";
			textBoxNationalID.Size = new System.Drawing.Size(199, 20);
			textBoxNationalID.TabIndex = 8;
			mmLabel45.AutoSize = true;
			mmLabel45.BackColor = System.Drawing.Color.Transparent;
			mmLabel45.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel45.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel45.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel45.IsFieldHeader = false;
			mmLabel45.IsRequired = false;
			mmLabel45.Location = new System.Drawing.Point(20, 105);
			mmLabel45.Name = "mmLabel45";
			mmLabel45.PenWidth = 1f;
			mmLabel45.ShowBorder = false;
			mmLabel45.Size = new System.Drawing.Size(65, 13);
			mmLabel45.TabIndex = 69;
			mmLabel45.Text = "ID Number :";
			dateTimeApplTypingDateEID.Checked = false;
			dateTimeApplTypingDateEID.CustomFormat = " dd-MMM-yyyy";
			dateTimeApplTypingDateEID.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApplTypingDateEID.Location = new System.Drawing.Point(137, 33);
			dateTimeApplTypingDateEID.Name = "dateTimeApplTypingDateEID";
			dateTimeApplTypingDateEID.ShowCheckBox = true;
			dateTimeApplTypingDateEID.Size = new System.Drawing.Size(124, 20);
			dateTimeApplTypingDateEID.TabIndex = 5;
			dateTimeApplTypingDateEID.Value = new System.DateTime(0L);
			mmLabel46.AutoSize = true;
			mmLabel46.BackColor = System.Drawing.Color.Transparent;
			mmLabel46.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel46.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel46.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel46.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel46.IsFieldHeader = false;
			mmLabel46.IsRequired = false;
			mmLabel46.Location = new System.Drawing.Point(21, 39);
			mmLabel46.Name = "mmLabel46";
			mmLabel46.PenWidth = 1f;
			mmLabel46.ShowBorder = false;
			mmLabel46.Size = new System.Drawing.Size(89, 13);
			mmLabel46.TabIndex = 10;
			mmLabel46.Text = "Appl. Typed On :";
			textBoxHealthCardNo.BackColor = System.Drawing.Color.White;
			textBoxHealthCardNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxHealthCardNo.CustomReportFieldName = "";
			textBoxHealthCardNo.CustomReportKey = "";
			textBoxHealthCardNo.CustomReportValueType = 1;
			textBoxHealthCardNo.IsComboTextBox = false;
			textBoxHealthCardNo.IsModified = false;
			textBoxHealthCardNo.Location = new System.Drawing.Point(137, 32);
			textBoxHealthCardNo.MaxLength = 30;
			textBoxHealthCardNo.Name = "textBoxHealthCardNo";
			textBoxHealthCardNo.Size = new System.Drawing.Size(205, 20);
			textBoxHealthCardNo.TabIndex = 0;
			mmLabel72.AutoSize = true;
			mmLabel72.BackColor = System.Drawing.Color.Transparent;
			mmLabel72.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel72.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel72.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel72.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel72.IsFieldHeader = false;
			mmLabel72.IsRequired = false;
			mmLabel72.Location = new System.Drawing.Point(21, 35);
			mmLabel72.Name = "mmLabel72";
			mmLabel72.PenWidth = 1f;
			mmLabel72.ShowBorder = false;
			mmLabel72.Size = new System.Drawing.Size(87, 13);
			mmLabel72.TabIndex = 138;
			mmLabel72.Text = "Health Card No :";
			mmLabel89.AutoSize = true;
			mmLabel89.BackColor = System.Drawing.Color.Transparent;
			mmLabel89.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel89.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel89.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel89.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel89.IsFieldHeader = false;
			mmLabel89.IsRequired = false;
			mmLabel89.Location = new System.Drawing.Point(21, 125);
			mmLabel89.Name = "mmLabel89";
			mmLabel89.PenWidth = 1f;
			mmLabel89.ShowBorder = false;
			mmLabel89.Size = new System.Drawing.Size(82, 13);
			mmLabel89.TabIndex = 73;
			mmLabel89.Text = "Medical Result :";
			dateTimeMedicalCollectedOn.Checked = false;
			dateTimeMedicalCollectedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeMedicalCollectedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeMedicalCollectedOn.Location = new System.Drawing.Point(137, 97);
			dateTimeMedicalCollectedOn.Name = "dateTimeMedicalCollectedOn";
			dateTimeMedicalCollectedOn.ShowCheckBox = true;
			dateTimeMedicalCollectedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeMedicalCollectedOn.TabIndex = 3;
			dateTimeMedicalCollectedOn.Value = new System.DateTime(0L);
			mmLabel41.AutoSize = true;
			mmLabel41.BackColor = System.Drawing.Color.Transparent;
			mmLabel41.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel41.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel41.IsFieldHeader = false;
			mmLabel41.IsRequired = false;
			mmLabel41.Location = new System.Drawing.Point(21, 103);
			mmLabel41.Name = "mmLabel41";
			mmLabel41.PenWidth = 1f;
			mmLabel41.ShowBorder = false;
			mmLabel41.Size = new System.Drawing.Size(113, 13);
			mmLabel41.TabIndex = 72;
			mmLabel41.Text = "Medical Collected On :";
			dateTimeMedicalAttendedOn.Checked = false;
			dateTimeMedicalAttendedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeMedicalAttendedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeMedicalAttendedOn.Location = new System.Drawing.Point(137, 75);
			dateTimeMedicalAttendedOn.Name = "dateTimeMedicalAttendedOn";
			dateTimeMedicalAttendedOn.ShowCheckBox = true;
			dateTimeMedicalAttendedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeMedicalAttendedOn.TabIndex = 2;
			dateTimeMedicalAttendedOn.Value = new System.DateTime(0L);
			mmLabel40.AutoSize = true;
			mmLabel40.BackColor = System.Drawing.Color.Transparent;
			mmLabel40.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel40.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel40.IsFieldHeader = false;
			mmLabel40.IsRequired = false;
			mmLabel40.Location = new System.Drawing.Point(20, 81);
			mmLabel40.Name = "mmLabel40";
			mmLabel40.PenWidth = 1f;
			mmLabel40.ShowBorder = false;
			mmLabel40.Size = new System.Drawing.Size(114, 13);
			mmLabel40.TabIndex = 70;
			mmLabel40.Text = "Medical Attended On :";
			dateTimeMedicalTypingOn.Checked = false;
			dateTimeMedicalTypingOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeMedicalTypingOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeMedicalTypingOn.Location = new System.Drawing.Point(137, 53);
			dateTimeMedicalTypingOn.Name = "dateTimeMedicalTypingOn";
			dateTimeMedicalTypingOn.ShowCheckBox = true;
			dateTimeMedicalTypingOn.Size = new System.Drawing.Size(124, 20);
			dateTimeMedicalTypingOn.TabIndex = 1;
			dateTimeMedicalTypingOn.Value = new System.DateTime(0L);
			mmLabel35.AutoSize = true;
			mmLabel35.BackColor = System.Drawing.Color.Transparent;
			mmLabel35.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel35.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel35.IsFieldHeader = false;
			mmLabel35.IsRequired = false;
			mmLabel35.Location = new System.Drawing.Point(21, 58);
			mmLabel35.Name = "mmLabel35";
			mmLabel35.PenWidth = 1f;
			mmLabel35.ShowBorder = false;
			mmLabel35.Size = new System.Drawing.Size(101, 13);
			mmLabel35.TabIndex = 11;
			mmLabel35.Text = "Medical Typing On :";
			textBoxMedicalNote.BackColor = System.Drawing.Color.White;
			textBoxMedicalNote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxMedicalNote.CustomReportFieldName = "";
			textBoxMedicalNote.CustomReportKey = "";
			textBoxMedicalNote.CustomReportValueType = 1;
			textBoxMedicalNote.IsComboTextBox = false;
			textBoxMedicalNote.IsModified = false;
			textBoxMedicalNote.Location = new System.Drawing.Point(149, 349);
			textBoxMedicalNote.MaxLength = 255;
			textBoxMedicalNote.Multiline = true;
			textBoxMedicalNote.Name = "textBoxMedicalNote";
			textBoxMedicalNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxMedicalNote.Size = new System.Drawing.Size(651, 97);
			textBoxMedicalNote.TabIndex = 10;
			mmLabel78.AutoSize = true;
			mmLabel78.BackColor = System.Drawing.Color.Transparent;
			mmLabel78.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel78.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel78.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel78.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel78.IsFieldHeader = false;
			mmLabel78.IsRequired = false;
			mmLabel78.Location = new System.Drawing.Point(32, 352);
			mmLabel78.Name = "mmLabel78";
			mmLabel78.PenWidth = 1f;
			mmLabel78.ShowBorder = false;
			mmLabel78.Size = new System.Drawing.Size(120, 13);
			mmLabel78.TabIndex = 136;
			mmLabel78.Text = "Medical/Emirates Note :";
			dateTimeRPExpiryDate.Checked = false;
			dateTimeRPExpiryDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeRPExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeRPExpiryDate.Location = new System.Drawing.Point(157, 188);
			dateTimeRPExpiryDate.Name = "dateTimeRPExpiryDate";
			dateTimeRPExpiryDate.ShowCheckBox = true;
			dateTimeRPExpiryDate.Size = new System.Drawing.Size(124, 20);
			dateTimeRPExpiryDate.TabIndex = 14;
			dateTimeRPExpiryDate.Value = new System.DateTime(0L);
			mmLabel56.AutoSize = true;
			mmLabel56.BackColor = System.Drawing.Color.Transparent;
			mmLabel56.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel56.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel56.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel56.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel56.IsFieldHeader = false;
			mmLabel56.IsRequired = false;
			mmLabel56.Location = new System.Drawing.Point(17, 35);
			mmLabel56.Name = "mmLabel56";
			mmLabel56.PenWidth = 1f;
			mmLabel56.ShowBorder = false;
			mmLabel56.Size = new System.Drawing.Size(75, 13);
			mmLabel56.TabIndex = 137;
			mmLabel56.Text = "Process Type:";
			mmLabel65.AutoSize = true;
			mmLabel65.BackColor = System.Drawing.Color.Transparent;
			mmLabel65.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel65.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel65.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel65.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel65.IsFieldHeader = false;
			mmLabel65.IsRequired = false;
			mmLabel65.Location = new System.Drawing.Point(17, 194);
			mmLabel65.Name = "mmLabel65";
			mmLabel65.PenWidth = 1f;
			mmLabel65.ShowBorder = false;
			mmLabel65.Size = new System.Drawing.Size(83, 13);
			mmLabel65.TabIndex = 84;
			mmLabel65.Text = "RP Expiry Date:";
			dateTimeRPIssueDate.Checked = false;
			dateTimeRPIssueDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeRPIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeRPIssueDate.Location = new System.Drawing.Point(157, 165);
			dateTimeRPIssueDate.Name = "dateTimeRPIssueDate";
			dateTimeRPIssueDate.ShowCheckBox = true;
			dateTimeRPIssueDate.Size = new System.Drawing.Size(124, 20);
			dateTimeRPIssueDate.TabIndex = 13;
			dateTimeRPIssueDate.Value = new System.DateTime(0L);
			mmLabel64.AutoSize = true;
			mmLabel64.BackColor = System.Drawing.Color.Transparent;
			mmLabel64.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel64.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel64.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel64.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel64.IsFieldHeader = false;
			mmLabel64.IsRequired = false;
			mmLabel64.Location = new System.Drawing.Point(17, 171);
			mmLabel64.Name = "mmLabel64";
			mmLabel64.PenWidth = 1f;
			mmLabel64.ShowBorder = false;
			mmLabel64.Size = new System.Drawing.Size(79, 13);
			mmLabel64.TabIndex = 82;
			mmLabel64.Text = "RP Issue Date:";
			textBoxRPIssuePlace.BackColor = System.Drawing.Color.White;
			textBoxRPIssuePlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxRPIssuePlace.CustomReportFieldName = "";
			textBoxRPIssuePlace.CustomReportKey = "";
			textBoxRPIssuePlace.CustomReportValueType = 1;
			textBoxRPIssuePlace.IsComboTextBox = false;
			textBoxRPIssuePlace.IsModified = false;
			textBoxRPIssuePlace.Location = new System.Drawing.Point(157, 143);
			textBoxRPIssuePlace.MaxLength = 30;
			textBoxRPIssuePlace.Name = "textBoxRPIssuePlace";
			textBoxRPIssuePlace.Size = new System.Drawing.Size(417, 20);
			textBoxRPIssuePlace.TabIndex = 12;
			mmLabel63.AutoSize = true;
			mmLabel63.BackColor = System.Drawing.Color.Transparent;
			mmLabel63.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel63.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel63.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel63.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel63.IsFieldHeader = false;
			mmLabel63.IsRequired = false;
			mmLabel63.Location = new System.Drawing.Point(17, 148);
			mmLabel63.Name = "mmLabel63";
			mmLabel63.PenWidth = 1f;
			mmLabel63.ShowBorder = false;
			mmLabel63.Size = new System.Drawing.Size(81, 13);
			mmLabel63.TabIndex = 80;
			mmLabel63.Text = "RP Issue Place:";
			dateTimePassportCollectedOnRP.Checked = false;
			dateTimePassportCollectedOnRP.CustomFormat = " dd-MMM-yyyy";
			dateTimePassportCollectedOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePassportCollectedOnRP.Location = new System.Drawing.Point(157, 121);
			dateTimePassportCollectedOnRP.Name = "dateTimePassportCollectedOnRP";
			dateTimePassportCollectedOnRP.ShowCheckBox = true;
			dateTimePassportCollectedOnRP.Size = new System.Drawing.Size(124, 20);
			dateTimePassportCollectedOnRP.TabIndex = 11;
			dateTimePassportCollectedOnRP.Value = new System.DateTime(0L);
			mmLabel52.AutoSize = true;
			mmLabel52.BackColor = System.Drawing.Color.Transparent;
			mmLabel52.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel52.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel52.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel52.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel52.IsFieldHeader = false;
			mmLabel52.IsRequired = false;
			mmLabel52.Location = new System.Drawing.Point(17, 127);
			mmLabel52.Name = "mmLabel52";
			mmLabel52.PenWidth = 1f;
			mmLabel52.ShowBorder = false;
			mmLabel52.Size = new System.Drawing.Size(117, 13);
			mmLabel52.TabIndex = 76;
			mmLabel52.Text = "Passport Collected On:";
			dateTimeSubmittedZajilOnRP.Checked = false;
			dateTimeSubmittedZajilOnRP.CustomFormat = " dd-MMM-yyyy";
			dateTimeSubmittedZajilOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeSubmittedZajilOnRP.Location = new System.Drawing.Point(157, 99);
			dateTimeSubmittedZajilOnRP.Name = "dateTimeSubmittedZajilOnRP";
			dateTimeSubmittedZajilOnRP.ShowCheckBox = true;
			dateTimeSubmittedZajilOnRP.Size = new System.Drawing.Size(124, 20);
			dateTimeSubmittedZajilOnRP.TabIndex = 10;
			dateTimeSubmittedZajilOnRP.Value = new System.DateTime(0L);
			mmLabel50.AutoSize = true;
			mmLabel50.BackColor = System.Drawing.Color.Transparent;
			mmLabel50.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel50.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel50.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel50.IsFieldHeader = false;
			mmLabel50.IsRequired = false;
			mmLabel50.Location = new System.Drawing.Point(17, 105);
			mmLabel50.Name = "mmLabel50";
			mmLabel50.PenWidth = 1f;
			mmLabel50.ShowBorder = false;
			mmLabel50.Size = new System.Drawing.Size(111, 13);
			mmLabel50.TabIndex = 74;
			mmLabel50.Text = "Submitted to Zajil On:";
			dateTimeApplApprovedOnRP.Checked = false;
			dateTimeApplApprovedOnRP.CustomFormat = " dd-MMM-yyyy";
			dateTimeApplApprovedOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApplApprovedOnRP.Location = new System.Drawing.Point(157, 76);
			dateTimeApplApprovedOnRP.Name = "dateTimeApplApprovedOnRP";
			dateTimeApplApprovedOnRP.ShowCheckBox = true;
			dateTimeApplApprovedOnRP.Size = new System.Drawing.Size(124, 20);
			dateTimeApplApprovedOnRP.TabIndex = 9;
			dateTimeApplApprovedOnRP.Value = new System.DateTime(0L);
			mmLabel49.AutoSize = true;
			mmLabel49.BackColor = System.Drawing.Color.Transparent;
			mmLabel49.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel49.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel49.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel49.IsFieldHeader = false;
			mmLabel49.IsRequired = false;
			mmLabel49.Location = new System.Drawing.Point(17, 82);
			mmLabel49.Name = "mmLabel49";
			mmLabel49.PenWidth = 1f;
			mmLabel49.ShowBorder = false;
			mmLabel49.Size = new System.Drawing.Size(103, 13);
			mmLabel49.TabIndex = 72;
			mmLabel49.Text = "Appl. Approved On:";
			dateTimeApplPostedOnRP.Checked = false;
			dateTimeApplPostedOnRP.CustomFormat = " dd-MMM-yyyy";
			dateTimeApplPostedOnRP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApplPostedOnRP.Location = new System.Drawing.Point(157, 54);
			dateTimeApplPostedOnRP.Name = "dateTimeApplPostedOnRP";
			dateTimeApplPostedOnRP.ShowCheckBox = true;
			dateTimeApplPostedOnRP.Size = new System.Drawing.Size(124, 20);
			dateTimeApplPostedOnRP.TabIndex = 8;
			dateTimeApplPostedOnRP.Value = new System.DateTime(0L);
			mmLabel48.AutoSize = true;
			mmLabel48.BackColor = System.Drawing.Color.Transparent;
			mmLabel48.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel48.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel48.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel48.IsFieldHeader = false;
			mmLabel48.IsRequired = false;
			mmLabel48.Location = new System.Drawing.Point(17, 59);
			mmLabel48.Name = "mmLabel48";
			mmLabel48.PenWidth = 1f;
			mmLabel48.ShowBorder = false;
			mmLabel48.Size = new System.Drawing.Size(89, 13);
			mmLabel48.TabIndex = 70;
			mmLabel48.Text = "Appl. Posted On:";
			dateTimeSignedAGTRecvd.Checked = false;
			dateTimeSignedAGTRecvd.CustomFormat = "dd-MMM-yyyy";
			dateTimeSignedAGTRecvd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeSignedAGTRecvd.Location = new System.Drawing.Point(157, 91);
			dateTimeSignedAGTRecvd.Name = "dateTimeSignedAGTRecvd";
			dateTimeSignedAGTRecvd.ShowCheckBox = true;
			dateTimeSignedAGTRecvd.Size = new System.Drawing.Size(124, 20);
			dateTimeSignedAGTRecvd.TabIndex = 3;
			dateTimeSignedAGTRecvd.Value = new System.DateTime(0L);
			mmLabel106.AutoSize = true;
			mmLabel106.BackColor = System.Drawing.Color.Transparent;
			mmLabel106.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel106.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel106.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel106.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel106.IsFieldHeader = false;
			mmLabel106.IsRequired = false;
			mmLabel106.Location = new System.Drawing.Point(17, 93);
			mmLabel106.Name = "mmLabel106";
			mmLabel106.PenWidth = 1f;
			mmLabel106.ShowBorder = false;
			mmLabel106.Size = new System.Drawing.Size(99, 13);
			mmLabel106.TabIndex = 158;
			mmLabel106.Text = "Signed AGT Recvd:";
			textBoxAGTMBNo.BackColor = System.Drawing.Color.White;
			textBoxAGTMBNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAGTMBNo.CustomReportFieldName = "";
			textBoxAGTMBNo.CustomReportKey = "";
			textBoxAGTMBNo.CustomReportValueType = 1;
			textBoxAGTMBNo.IsComboTextBox = false;
			textBoxAGTMBNo.IsModified = false;
			textBoxAGTMBNo.Location = new System.Drawing.Point(157, 47);
			textBoxAGTMBNo.MaxLength = 30;
			textBoxAGTMBNo.Name = "textBoxAGTMBNo";
			textBoxAGTMBNo.Size = new System.Drawing.Size(234, 20);
			textBoxAGTMBNo.TabIndex = 1;
			mmLabel91.AutoSize = true;
			mmLabel91.BackColor = System.Drawing.Color.Transparent;
			mmLabel91.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel91.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel91.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel91.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel91.IsFieldHeader = false;
			mmLabel91.IsRequired = false;
			mmLabel91.Location = new System.Drawing.Point(17, 49);
			mmLabel91.Name = "mmLabel91";
			mmLabel91.PenWidth = 1f;
			mmLabel91.ShowBorder = false;
			mmLabel91.Size = new System.Drawing.Size(114, 13);
			mmLabel91.TabIndex = 86;
			mmLabel91.Text = "MB Ref No - AGT/WP :";
			mmLabel90.AutoSize = true;
			mmLabel90.BackColor = System.Drawing.Color.Transparent;
			mmLabel90.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel90.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel90.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel90.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel90.IsFieldHeader = false;
			mmLabel90.IsRequired = false;
			mmLabel90.Location = new System.Drawing.Point(17, 27);
			mmLabel90.Name = "mmLabel90";
			mmLabel90.PenWidth = 1f;
			mmLabel90.ShowBorder = false;
			mmLabel90.Size = new System.Drawing.Size(58, 13);
			mmLabel90.TabIndex = 84;
			mmLabel90.Text = "AGT Type:";
			textBoxPersonIDNo.BackColor = System.Drawing.Color.White;
			textBoxPersonIDNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxPersonIDNo.CustomReportFieldName = "";
			textBoxPersonIDNo.CustomReportKey = "";
			textBoxPersonIDNo.CustomReportValueType = 1;
			textBoxPersonIDNo.IsComboTextBox = false;
			textBoxPersonIDNo.IsModified = false;
			textBoxPersonIDNo.Location = new System.Drawing.Point(157, 157);
			textBoxPersonIDNo.MaxLength = 30;
			textBoxPersonIDNo.Name = "textBoxPersonIDNo";
			textBoxPersonIDNo.Size = new System.Drawing.Size(234, 20);
			textBoxPersonIDNo.TabIndex = 6;
			mmLabel66.AutoSize = true;
			mmLabel66.BackColor = System.Drawing.Color.Transparent;
			mmLabel66.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel66.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel66.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel66.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel66.IsFieldHeader = false;
			mmLabel66.IsRequired = false;
			mmLabel66.Location = new System.Drawing.Point(17, 159);
			mmLabel66.Name = "mmLabel66";
			mmLabel66.PenWidth = 1f;
			mmLabel66.ShowBorder = false;
			mmLabel66.Size = new System.Drawing.Size(82, 13);
			mmLabel66.TabIndex = 82;
			mmLabel66.Text = "Personal ID No:";
			textBoxWPIssuePlace.BackColor = System.Drawing.Color.White;
			textBoxWPIssuePlace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxWPIssuePlace.CustomReportFieldName = "";
			textBoxWPIssuePlace.CustomReportKey = "";
			textBoxWPIssuePlace.CustomReportValueType = 1;
			textBoxWPIssuePlace.IsComboTextBox = false;
			textBoxWPIssuePlace.IsModified = false;
			textBoxWPIssuePlace.Location = new System.Drawing.Point(157, 179);
			textBoxWPIssuePlace.MaxLength = 30;
			textBoxWPIssuePlace.Name = "textBoxWPIssuePlace";
			textBoxWPIssuePlace.Size = new System.Drawing.Size(417, 20);
			textBoxWPIssuePlace.TabIndex = 7;
			dateTimeWPExpiryDate.Checked = false;
			dateTimeWPExpiryDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeWPExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeWPExpiryDate.Location = new System.Drawing.Point(157, 223);
			dateTimeWPExpiryDate.Name = "dateTimeWPExpiryDate";
			dateTimeWPExpiryDate.ShowCheckBox = true;
			dateTimeWPExpiryDate.Size = new System.Drawing.Size(124, 20);
			dateTimeWPExpiryDate.TabIndex = 9;
			dateTimeWPExpiryDate.Value = new System.DateTime(0L);
			dateTimeWPIssueDate.Checked = false;
			dateTimeWPIssueDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeWPIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeWPIssueDate.Location = new System.Drawing.Point(157, 201);
			dateTimeWPIssueDate.Name = "dateTimeWPIssueDate";
			dateTimeWPIssueDate.ShowCheckBox = true;
			dateTimeWPIssueDate.Size = new System.Drawing.Size(124, 20);
			dateTimeWPIssueDate.TabIndex = 8;
			dateTimeWPIssueDate.Value = new System.DateTime(0L);
			mmLabel62.AutoSize = true;
			mmLabel62.BackColor = System.Drawing.Color.Transparent;
			mmLabel62.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel62.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel62.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel62.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel62.IsFieldHeader = false;
			mmLabel62.IsRequired = false;
			mmLabel62.Location = new System.Drawing.Point(17, 225);
			mmLabel62.Name = "mmLabel62";
			mmLabel62.PenWidth = 1f;
			mmLabel62.ShowBorder = false;
			mmLabel62.Size = new System.Drawing.Size(86, 13);
			mmLabel62.TabIndex = 81;
			mmLabel62.Text = "WP Expiry Date:";
			mmLabel61.AutoSize = true;
			mmLabel61.BackColor = System.Drawing.Color.Transparent;
			mmLabel61.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel61.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel61.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel61.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel61.IsFieldHeader = false;
			mmLabel61.IsRequired = false;
			mmLabel61.Location = new System.Drawing.Point(17, 203);
			mmLabel61.Name = "mmLabel61";
			mmLabel61.PenWidth = 1f;
			mmLabel61.ShowBorder = false;
			mmLabel61.Size = new System.Drawing.Size(82, 13);
			mmLabel61.TabIndex = 80;
			mmLabel61.Text = "WP Issue Date:";
			mmLabel60.AutoSize = true;
			mmLabel60.BackColor = System.Drawing.Color.Transparent;
			mmLabel60.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel60.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel60.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel60.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel60.IsFieldHeader = false;
			mmLabel60.IsRequired = false;
			mmLabel60.Location = new System.Drawing.Point(17, 181);
			mmLabel60.Name = "mmLabel60";
			mmLabel60.PenWidth = 1f;
			mmLabel60.ShowBorder = false;
			mmLabel60.Size = new System.Drawing.Size(84, 13);
			mmLabel60.TabIndex = 79;
			mmLabel60.Text = "WP Issue Place:";
			textBoxWPNo.BackColor = System.Drawing.Color.White;
			textBoxWPNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxWPNo.CustomReportFieldName = "";
			textBoxWPNo.CustomReportKey = "";
			textBoxWPNo.CustomReportValueType = 1;
			textBoxWPNo.IsComboTextBox = false;
			textBoxWPNo.IsModified = false;
			textBoxWPNo.Location = new System.Drawing.Point(157, 135);
			textBoxWPNo.MaxLength = 30;
			textBoxWPNo.Name = "textBoxWPNo";
			textBoxWPNo.Size = new System.Drawing.Size(234, 20);
			textBoxWPNo.TabIndex = 5;
			dateTimeAGTSubmittedOn.Checked = false;
			dateTimeAGTSubmittedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeAGTSubmittedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeAGTSubmittedOn.Location = new System.Drawing.Point(157, 113);
			dateTimeAGTSubmittedOn.Name = "dateTimeAGTSubmittedOn";
			dateTimeAGTSubmittedOn.ShowCheckBox = true;
			dateTimeAGTSubmittedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeAGTSubmittedOn.TabIndex = 4;
			dateTimeAGTSubmittedOn.Value = new System.DateTime(0L);
			mmLabel57.AutoSize = true;
			mmLabel57.BackColor = System.Drawing.Color.Transparent;
			mmLabel57.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel57.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel57.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel57.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel57.IsFieldHeader = false;
			mmLabel57.IsRequired = false;
			mmLabel57.Location = new System.Drawing.Point(17, 115);
			mmLabel57.Name = "mmLabel57";
			mmLabel57.PenWidth = 1f;
			mmLabel57.ShowBorder = false;
			mmLabel57.Size = new System.Drawing.Size(117, 13);
			mmLabel57.TabIndex = 74;
			mmLabel57.Text = "AGT/WP Submitted on:";
			mmLabel58.AutoSize = true;
			mmLabel58.BackColor = System.Drawing.Color.Transparent;
			mmLabel58.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel58.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel58.IsFieldHeader = false;
			mmLabel58.IsRequired = false;
			mmLabel58.Location = new System.Drawing.Point(17, 137);
			mmLabel58.Name = "mmLabel58";
			mmLabel58.PenWidth = 1f;
			mmLabel58.ShowBorder = false;
			mmLabel58.Size = new System.Drawing.Size(43, 13);
			mmLabel58.TabIndex = 72;
			mmLabel58.Text = "WP No:";
			dateTimeAGTTypedOn.Checked = false;
			dateTimeAGTTypedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeAGTTypedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeAGTTypedOn.Location = new System.Drawing.Point(157, 69);
			dateTimeAGTTypedOn.Name = "dateTimeAGTTypedOn";
			dateTimeAGTTypedOn.ShowCheckBox = true;
			dateTimeAGTTypedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeAGTTypedOn.TabIndex = 2;
			dateTimeAGTTypedOn.Value = new System.DateTime(0L);
			mmLabel59.AutoSize = true;
			mmLabel59.BackColor = System.Drawing.Color.Transparent;
			mmLabel59.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel59.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel59.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel59.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel59.IsFieldHeader = false;
			mmLabel59.IsRequired = false;
			mmLabel59.Location = new System.Drawing.Point(17, 71);
			mmLabel59.Name = "mmLabel59";
			mmLabel59.PenWidth = 1f;
			mmLabel59.ShowBorder = false;
			mmLabel59.Size = new System.Drawing.Size(128, 13);
			mmLabel59.TabIndex = 70;
			mmLabel59.Text = "AGT/WP Form Typed On:";
			mmLabel100.AutoSize = true;
			mmLabel100.BackColor = System.Drawing.Color.Transparent;
			mmLabel100.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel100.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel100.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel100.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel100.IsFieldHeader = false;
			mmLabel100.IsRequired = false;
			mmLabel100.Location = new System.Drawing.Point(295, 195);
			mmLabel100.Name = "mmLabel100";
			mmLabel100.PenWidth = 1f;
			mmLabel100.ShowBorder = false;
			mmLabel100.Size = new System.Drawing.Size(119, 13);
			mmLabel100.TabIndex = 155;
			mmLabel100.Text = "Expected Arrival Date :";
			datetimepickerExpectedArrivalDate.Checked = false;
			datetimepickerExpectedArrivalDate.CustomFormat = " dd-MMM-yyyy";
			datetimepickerExpectedArrivalDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimepickerExpectedArrivalDate.Location = new System.Drawing.Point(415, 191);
			datetimepickerExpectedArrivalDate.Name = "datetimepickerExpectedArrivalDate";
			datetimepickerExpectedArrivalDate.ShowCheckBox = true;
			datetimepickerExpectedArrivalDate.Size = new System.Drawing.Size(124, 20);
			datetimepickerExpectedArrivalDate.TabIndex = 11;
			datetimepickerExpectedArrivalDate.Value = new System.DateTime(0L);
			mmLabel99.AutoSize = true;
			mmLabel99.BackColor = System.Drawing.Color.Transparent;
			mmLabel99.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel99.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel99.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel99.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel99.IsFieldHeader = false;
			mmLabel99.IsRequired = false;
			mmLabel99.Location = new System.Drawing.Point(7, 81);
			mmLabel99.Name = "mmLabel99";
			mmLabel99.PenWidth = 1f;
			mmLabel99.ShowBorder = false;
			mmLabel99.Size = new System.Drawing.Size(88, 13);
			mmLabel99.TabIndex = 153;
			mmLabel99.Text = "Approval Status:";
			mmLabel97.AutoSize = true;
			mmLabel97.BackColor = System.Drawing.Color.Transparent;
			mmLabel97.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel97.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel97.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel97.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel97.IsFieldHeader = false;
			mmLabel97.IsRequired = false;
			mmLabel97.Location = new System.Drawing.Point(7, 213);
			mmLabel97.Name = "mmLabel97";
			mmLabel97.PenWidth = 1f;
			mmLabel97.ShowBorder = false;
			mmLabel97.Size = new System.Drawing.Size(77, 13);
			mmLabel97.TabIndex = 127;
			mmLabel97.Text = "IMG Remarks :";
			textBoxIMGRemarks.BackColor = System.Drawing.Color.White;
			textBoxIMGRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxIMGRemarks.CustomReportFieldName = "";
			textBoxIMGRemarks.CustomReportKey = "";
			textBoxIMGRemarks.CustomReportValueType = 1;
			textBoxIMGRemarks.IsComboTextBox = false;
			textBoxIMGRemarks.IsModified = false;
			textBoxIMGRemarks.Location = new System.Drawing.Point(159, 213);
			textBoxIMGRemarks.MaxLength = 255;
			textBoxIMGRemarks.Multiline = true;
			textBoxIMGRemarks.Name = "textBoxIMGRemarks";
			textBoxIMGRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxIMGRemarks.Size = new System.Drawing.Size(411, 51);
			textBoxIMGRemarks.TabIndex = 12;
			mmLabel77.AutoSize = true;
			mmLabel77.BackColor = System.Drawing.Color.Transparent;
			mmLabel77.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel77.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel77.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel77.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel77.IsFieldHeader = false;
			mmLabel77.IsRequired = false;
			mmLabel77.Location = new System.Drawing.Point(7, 191);
			mmLabel77.Name = "mmLabel77";
			mmLabel77.PenWidth = 1f;
			mmLabel77.ShowBorder = false;
			mmLabel77.Size = new System.Drawing.Size(148, 13);
			mmLabel77.TabIndex = 102;
			mmLabel77.Text = "Visa Copy Sent to Agent On :";
			dateTimeVisaCopyToAgentOn.Checked = false;
			dateTimeVisaCopyToAgentOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeVisaCopyToAgentOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeVisaCopyToAgentOn.Location = new System.Drawing.Point(158, 190);
			dateTimeVisaCopyToAgentOn.Name = "dateTimeVisaCopyToAgentOn";
			dateTimeVisaCopyToAgentOn.ShowCheckBox = true;
			dateTimeVisaCopyToAgentOn.Size = new System.Drawing.Size(124, 20);
			dateTimeVisaCopyToAgentOn.TabIndex = 10;
			dateTimeVisaCopyToAgentOn.Value = new System.DateTime(0L);
			dateTimeVisaExpiryDate.Checked = false;
			dateTimeVisaExpiryDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeVisaExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeVisaExpiryDate.Location = new System.Drawing.Point(415, 151);
			dateTimeVisaExpiryDate.Name = "dateTimeVisaExpiryDate";
			dateTimeVisaExpiryDate.ShowCheckBox = true;
			dateTimeVisaExpiryDate.Size = new System.Drawing.Size(124, 20);
			dateTimeVisaExpiryDate.TabIndex = 8;
			dateTimeVisaExpiryDate.Value = new System.DateTime(0L);
			dateTimeVisaIssueDate.Checked = false;
			dateTimeVisaIssueDate.CustomFormat = " dd-MMM-yyyy";
			dateTimeVisaIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeVisaIssueDate.Location = new System.Drawing.Point(158, 146);
			dateTimeVisaIssueDate.Name = "dateTimeVisaIssueDate";
			dateTimeVisaIssueDate.ShowCheckBox = true;
			dateTimeVisaIssueDate.Size = new System.Drawing.Size(124, 20);
			dateTimeVisaIssueDate.TabIndex = 7;
			dateTimeVisaIssueDate.Value = new System.DateTime(0L);
			mmLabel87.AutoSize = true;
			mmLabel87.BackColor = System.Drawing.Color.Transparent;
			mmLabel87.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel87.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel87.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel87.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel87.IsFieldHeader = false;
			mmLabel87.IsRequired = false;
			mmLabel87.Location = new System.Drawing.Point(7, 153);
			mmLabel87.Name = "mmLabel87";
			mmLabel87.PenWidth = 1f;
			mmLabel87.ShowBorder = false;
			mmLabel87.Size = new System.Drawing.Size(88, 13);
			mmLabel87.TabIndex = 107;
			mmLabel87.Text = "Visa Issue Date :";
			textBoxVisaIssuePlaceIMG.AutoCompleteCustomSource.AddRange(new string[8]
			{
				"A+",
				"A-",
				"B+",
				"B-",
				"AB+",
				"AB-",
				"O+",
				"O-"
			});
			textBoxVisaIssuePlaceIMG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxVisaIssuePlaceIMG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxVisaIssuePlaceIMG.BackColor = System.Drawing.Color.White;
			textBoxVisaIssuePlaceIMG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxVisaIssuePlaceIMG.CustomReportFieldName = "";
			textBoxVisaIssuePlaceIMG.CustomReportKey = "";
			textBoxVisaIssuePlaceIMG.CustomReportValueType = 1;
			textBoxVisaIssuePlaceIMG.IsComboTextBox = false;
			textBoxVisaIssuePlaceIMG.IsModified = false;
			textBoxVisaIssuePlaceIMG.Location = new System.Drawing.Point(158, 102);
			textBoxVisaIssuePlaceIMG.MaxLength = 30;
			textBoxVisaIssuePlaceIMG.Name = "textBoxVisaIssuePlaceIMG";
			textBoxVisaIssuePlaceIMG.Size = new System.Drawing.Size(414, 20);
			textBoxVisaIssuePlaceIMG.TabIndex = 5;
			mmLabel86.AutoSize = true;
			mmLabel86.BackColor = System.Drawing.Color.Transparent;
			mmLabel86.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel86.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel86.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel86.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel86.IsFieldHeader = false;
			mmLabel86.IsRequired = false;
			mmLabel86.Location = new System.Drawing.Point(7, 171);
			mmLabel86.Name = "mmLabel86";
			mmLabel86.PenWidth = 1f;
			mmLabel86.ShowBorder = false;
			mmLabel86.Size = new System.Drawing.Size(48, 13);
			mmLabel86.TabIndex = 106;
			mmLabel86.Text = "UID No :";
			textBoxVisaNumber.AutoCompleteCustomSource.AddRange(new string[8]
			{
				"A+",
				"A-",
				"B+",
				"B-",
				"AB+",
				"AB-",
				"O+",
				"O-"
			});
			textBoxVisaNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxVisaNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxVisaNumber.BackColor = System.Drawing.Color.White;
			textBoxVisaNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxVisaNumber.CustomReportFieldName = "";
			textBoxVisaNumber.CustomReportKey = "";
			textBoxVisaNumber.CustomReportValueType = 1;
			textBoxVisaNumber.IsComboTextBox = false;
			textBoxVisaNumber.IsModified = false;
			textBoxVisaNumber.Location = new System.Drawing.Point(158, 124);
			textBoxVisaNumber.MaxLength = 30;
			textBoxVisaNumber.Name = "textBoxVisaNumber";
			textBoxVisaNumber.Size = new System.Drawing.Size(205, 20);
			textBoxVisaNumber.TabIndex = 6;
			mmLabel85.AutoSize = true;
			mmLabel85.BackColor = System.Drawing.Color.Transparent;
			mmLabel85.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel85.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel85.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel85.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel85.IsFieldHeader = false;
			mmLabel85.IsRequired = false;
			mmLabel85.Location = new System.Drawing.Point(7, 131);
			mmLabel85.Name = "mmLabel85";
			mmLabel85.PenWidth = 1f;
			mmLabel85.ShowBorder = false;
			mmLabel85.Size = new System.Drawing.Size(49, 13);
			mmLabel85.TabIndex = 104;
			mmLabel85.Text = "Visa No :";
			mmLabel84.AutoSize = true;
			mmLabel84.BackColor = System.Drawing.Color.Transparent;
			mmLabel84.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel84.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel84.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel84.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel84.IsFieldHeader = false;
			mmLabel84.IsRequired = false;
			mmLabel84.Location = new System.Drawing.Point(295, 151);
			mmLabel84.Name = "mmLabel84";
			mmLabel84.PenWidth = 1f;
			mmLabel84.ShowBorder = false;
			mmLabel84.Size = new System.Drawing.Size(92, 13);
			mmLabel84.TabIndex = 103;
			mmLabel84.Text = "Visa Expiry Date :";
			textBoxUIDNumberIMG.AutoCompleteCustomSource.AddRange(new string[8]
			{
				"A+",
				"A-",
				"B+",
				"B-",
				"AB+",
				"AB-",
				"O+",
				"O-"
			});
			textBoxUIDNumberIMG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxUIDNumberIMG.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxUIDNumberIMG.BackColor = System.Drawing.Color.White;
			textBoxUIDNumberIMG.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxUIDNumberIMG.CustomReportFieldName = "";
			textBoxUIDNumberIMG.CustomReportKey = "";
			textBoxUIDNumberIMG.CustomReportValueType = 1;
			textBoxUIDNumberIMG.IsComboTextBox = false;
			textBoxUIDNumberIMG.IsModified = false;
			textBoxUIDNumberIMG.Location = new System.Drawing.Point(158, 168);
			textBoxUIDNumberIMG.MaxLength = 30;
			textBoxUIDNumberIMG.Name = "textBoxUIDNumberIMG";
			textBoxUIDNumberIMG.Size = new System.Drawing.Size(205, 20);
			textBoxUIDNumberIMG.TabIndex = 9;
			mmLabel83.AutoSize = true;
			mmLabel83.BackColor = System.Drawing.Color.Transparent;
			mmLabel83.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel83.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel83.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel83.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel83.IsFieldHeader = false;
			mmLabel83.IsRequired = false;
			mmLabel83.Location = new System.Drawing.Point(7, 109);
			mmLabel83.Name = "mmLabel83";
			mmLabel83.PenWidth = 1f;
			mmLabel83.ShowBorder = false;
			mmLabel83.Size = new System.Drawing.Size(90, 13);
			mmLabel83.TabIndex = 102;
			mmLabel83.Text = "Visa Issue Place :";
			dateTimeVisaPostedOn.Checked = false;
			dateTimeVisaPostedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeVisaPostedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeVisaPostedOn.Location = new System.Drawing.Point(158, 55);
			dateTimeVisaPostedOn.Name = "dateTimeVisaPostedOn";
			dateTimeVisaPostedOn.ShowCheckBox = true;
			dateTimeVisaPostedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeVisaPostedOn.TabIndex = 1;
			dateTimeVisaPostedOn.Value = new System.DateTime(0L);
			mmLabel26.AutoSize = true;
			mmLabel26.BackColor = System.Drawing.Color.Transparent;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(7, 58);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(86, 13);
			mmLabel26.TabIndex = 75;
			mmLabel26.Text = "Visa Posted On :";
			dateTimeApprovedOn.Checked = false;
			dateTimeApprovedOn.CustomFormat = " dd-MMM-yyyy";
			dateTimeApprovedOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApprovedOn.Location = new System.Drawing.Point(415, 55);
			dateTimeApprovedOn.Name = "dateTimeApprovedOn";
			dateTimeApprovedOn.ShowCheckBox = true;
			dateTimeApprovedOn.Size = new System.Drawing.Size(124, 20);
			dateTimeApprovedOn.TabIndex = 2;
			dateTimeApprovedOn.Value = new System.DateTime(0L);
			mmLabel28.AutoSize = true;
			mmLabel28.BackColor = System.Drawing.Color.Transparent;
			mmLabel28.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel28.IsFieldHeader = false;
			mmLabel28.IsRequired = false;
			mmLabel28.Location = new System.Drawing.Point(295, 58);
			mmLabel28.Name = "mmLabel28";
			mmLabel28.PenWidth = 1f;
			mmLabel28.ShowBorder = false;
			mmLabel28.Size = new System.Drawing.Size(100, 13);
			mmLabel28.TabIndex = 71;
			mmLabel28.Text = "Visa Approved On :";
			mmLabel32.AutoSize = true;
			mmLabel32.BackColor = System.Drawing.Color.Transparent;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel32.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(7, 36);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(114, 13);
			mmLabel32.TabIndex = 10;
			mmLabel32.Text = "Visa Applied Through :";
			datetimeSignedAORcvd.Checked = false;
			datetimeSignedAORcvd.CustomFormat = "dd-MMM-yyyy";
			datetimeSignedAORcvd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimeSignedAORcvd.Location = new System.Drawing.Point(158, 63);
			datetimeSignedAORcvd.Name = "datetimeSignedAORcvd";
			datetimeSignedAORcvd.ShowCheckBox = true;
			datetimeSignedAORcvd.Size = new System.Drawing.Size(124, 20);
			datetimeSignedAORcvd.TabIndex = 2;
			datetimeSignedAORcvd.Value = new System.DateTime(0L);
			mmLabel105.AutoSize = true;
			mmLabel105.BackColor = System.Drawing.Color.Transparent;
			mmLabel105.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel105.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel105.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel105.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel105.IsFieldHeader = false;
			mmLabel105.IsRequired = false;
			mmLabel105.Location = new System.Drawing.Point(10, 67);
			mmLabel105.Name = "mmLabel105";
			mmLabel105.PenWidth = 1f;
			mmLabel105.ShowBorder = false;
			mmLabel105.Size = new System.Drawing.Size(98, 13);
			mmLabel105.TabIndex = 156;
			mmLabel105.Text = "Signed A/O Recvd:";
			mmLabel98.AutoSize = true;
			mmLabel98.BackColor = System.Drawing.Color.Transparent;
			mmLabel98.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel98.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel98.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel98.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel98.IsFieldHeader = false;
			mmLabel98.IsRequired = false;
			mmLabel98.Location = new System.Drawing.Point(10, 265);
			mmLabel98.Name = "mmLabel98";
			mmLabel98.PenWidth = 1f;
			mmLabel98.ShowBorder = false;
			mmLabel98.Size = new System.Drawing.Size(79, 13);
			mmLabel98.TabIndex = 154;
			mmLabel98.Text = "MOL Remarks :";
			textBoxMOLRemarks.BackColor = System.Drawing.Color.White;
			textBoxMOLRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxMOLRemarks.CustomReportFieldName = "";
			textBoxMOLRemarks.CustomReportKey = "";
			textBoxMOLRemarks.CustomReportValueType = 1;
			textBoxMOLRemarks.IsComboTextBox = false;
			textBoxMOLRemarks.IsModified = false;
			textBoxMOLRemarks.Location = new System.Drawing.Point(159, 262);
			textBoxMOLRemarks.MaxLength = 255;
			textBoxMOLRemarks.Multiline = true;
			textBoxMOLRemarks.Name = "textBoxMOLRemarks";
			textBoxMOLRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxMOLRemarks.Size = new System.Drawing.Size(411, 51);
			textBoxMOLRemarks.TabIndex = 15;
			datetimepickerAOtypingDate.Checked = false;
			datetimepickerAOtypingDate.CustomFormat = "dd-MMM-yyyy";
			datetimepickerAOtypingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimepickerAOtypingDate.Location = new System.Drawing.Point(158, 19);
			datetimepickerAOtypingDate.Name = "datetimepickerAOtypingDate";
			datetimepickerAOtypingDate.ShowCheckBox = true;
			datetimepickerAOtypingDate.Size = new System.Drawing.Size(124, 20);
			datetimepickerAOtypingDate.TabIndex = 0;
			datetimepickerAOtypingDate.Value = new System.DateTime(0L);
			mmLabel96.AutoSize = true;
			mmLabel96.BackColor = System.Drawing.Color.Transparent;
			mmLabel96.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel96.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel96.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel96.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel96.IsFieldHeader = false;
			mmLabel96.IsRequired = false;
			mmLabel96.Location = new System.Drawing.Point(10, 177);
			mmLabel96.Name = "mmLabel96";
			mmLabel96.PenWidth = 1f;
			mmLabel96.ShowBorder = false;
			mmLabel96.Size = new System.Drawing.Size(88, 13);
			mmLabel96.TabIndex = 151;
			mmLabel96.Text = "Approval Status:";
			textBoxRegnNumber.AutoCompleteCustomSource.AddRange(new string[8]
			{
				"A+",
				"A-",
				"B+",
				"B-",
				"AB+",
				"AB-",
				"O+",
				"O-"
			});
			textBoxRegnNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxRegnNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxRegnNumber.BackColor = System.Drawing.Color.White;
			textBoxRegnNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxRegnNumber.CustomReportFieldName = "";
			textBoxRegnNumber.CustomReportKey = "";
			textBoxRegnNumber.CustomReportValueType = 1;
			textBoxRegnNumber.IsComboTextBox = false;
			textBoxRegnNumber.IsModified = false;
			textBoxRegnNumber.Location = new System.Drawing.Point(158, 41);
			textBoxRegnNumber.MaxLength = 30;
			textBoxRegnNumber.Name = "textBoxRegnNumber";
			textBoxRegnNumber.Size = new System.Drawing.Size(205, 20);
			textBoxRegnNumber.TabIndex = 1;
			mmLabel94.AutoSize = true;
			mmLabel94.BackColor = System.Drawing.Color.Transparent;
			mmLabel94.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel94.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel94.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel94.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel94.IsFieldHeader = false;
			mmLabel94.IsRequired = false;
			mmLabel94.Location = new System.Drawing.Point(10, 45);
			mmLabel94.Name = "mmLabel94";
			mmLabel94.PenWidth = 1f;
			mmLabel94.ShowBorder = false;
			mmLabel94.Size = new System.Drawing.Size(110, 13);
			mmLabel94.TabIndex = 111;
			mmLabel94.Text = "A/O Registration No :";
			mmLabel95.AutoSize = true;
			mmLabel95.BackColor = System.Drawing.Color.Transparent;
			mmLabel95.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel95.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel95.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel95.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel95.IsFieldHeader = false;
			mmLabel95.IsRequired = false;
			mmLabel95.Location = new System.Drawing.Point(10, 23);
			mmLabel95.Name = "mmLabel95";
			mmLabel95.PenWidth = 1f;
			mmLabel95.ShowBorder = false;
			mmLabel95.Size = new System.Drawing.Size(94, 13);
			mmLabel95.TabIndex = 110;
			mmLabel95.Text = "A/O  Typing Date:";
			comboBoxPositionVisa.Assigned = false;
			comboBoxPositionVisa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPositionVisa.CustomReportFieldName = "";
			comboBoxPositionVisa.CustomReportKey = "";
			comboBoxPositionVisa.CustomReportValueType = 1;
			comboBoxPositionVisa.DescriptionTextBox = textBoxVisaDesignationName;
			appearance116.BackColor = System.Drawing.SystemColors.Window;
			appearance116.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPositionVisa.DisplayLayout.Appearance = appearance116;
			comboBoxPositionVisa.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPositionVisa.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance117.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance117.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance117.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance117.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPositionVisa.DisplayLayout.GroupByBox.Appearance = appearance117;
			appearance118.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPositionVisa.DisplayLayout.GroupByBox.BandLabelAppearance = appearance118;
			comboBoxPositionVisa.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance119.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance119.BackColor2 = System.Drawing.SystemColors.Control;
			appearance119.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance119.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPositionVisa.DisplayLayout.GroupByBox.PromptAppearance = appearance119;
			comboBoxPositionVisa.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPositionVisa.DisplayLayout.MaxRowScrollRegions = 1;
			appearance120.BackColor = System.Drawing.SystemColors.Window;
			appearance120.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPositionVisa.DisplayLayout.Override.ActiveCellAppearance = appearance120;
			appearance121.BackColor = System.Drawing.SystemColors.Highlight;
			appearance121.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPositionVisa.DisplayLayout.Override.ActiveRowAppearance = appearance121;
			comboBoxPositionVisa.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPositionVisa.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance122.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPositionVisa.DisplayLayout.Override.CardAreaAppearance = appearance122;
			appearance123.BorderColor = System.Drawing.Color.Silver;
			appearance123.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPositionVisa.DisplayLayout.Override.CellAppearance = appearance123;
			comboBoxPositionVisa.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPositionVisa.DisplayLayout.Override.CellPadding = 0;
			appearance124.BackColor = System.Drawing.SystemColors.Control;
			appearance124.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance124.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance124.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPositionVisa.DisplayLayout.Override.GroupByRowAppearance = appearance124;
			appearance125.TextHAlignAsString = "Left";
			comboBoxPositionVisa.DisplayLayout.Override.HeaderAppearance = appearance125;
			comboBoxPositionVisa.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPositionVisa.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance126.BackColor = System.Drawing.SystemColors.Window;
			appearance126.BorderColor = System.Drawing.Color.Silver;
			comboBoxPositionVisa.DisplayLayout.Override.RowAppearance = appearance126;
			comboBoxPositionVisa.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance127.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPositionVisa.DisplayLayout.Override.TemplateAddRowAppearance = appearance127;
			comboBoxPositionVisa.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPositionVisa.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPositionVisa.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPositionVisa.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPositionVisa.Editable = true;
			comboBoxPositionVisa.FilterString = "";
			comboBoxPositionVisa.HasAllAccount = false;
			comboBoxPositionVisa.HasCustom = false;
			comboBoxPositionVisa.IsDataLoaded = false;
			comboBoxPositionVisa.Location = new System.Drawing.Point(158, 151);
			comboBoxPositionVisa.MaxDropDownItems = 12;
			comboBoxPositionVisa.Name = "comboBoxPositionVisa";
			comboBoxPositionVisa.ShowInactiveItems = false;
			comboBoxPositionVisa.ShowQuickAdd = true;
			comboBoxPositionVisa.Size = new System.Drawing.Size(124, 20);
			comboBoxPositionVisa.TabIndex = 6;
			comboBoxPositionVisa.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel67.AutoSize = true;
			mmLabel67.BackColor = System.Drawing.Color.Transparent;
			mmLabel67.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel67.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel67.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel67.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel67.IsFieldHeader = false;
			mmLabel67.IsRequired = false;
			mmLabel67.Location = new System.Drawing.Point(10, 155);
			mmLabel67.Name = "mmLabel67";
			mmLabel67.PenWidth = 1f;
			mmLabel67.ShowBorder = false;
			mmLabel67.Size = new System.Drawing.Size(92, 13);
			mmLabel67.TabIndex = 107;
			mmLabel67.Text = "Visa Designation :";
			comboBoxSponsor.Assigned = false;
			comboBoxSponsor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSponsor.CustomReportFieldName = "";
			comboBoxSponsor.CustomReportKey = "";
			comboBoxSponsor.CustomReportValueType = 1;
			comboBoxSponsor.DescriptionTextBox = textBoxSponsorName;
			appearance128.BackColor = System.Drawing.SystemColors.Window;
			appearance128.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSponsor.DisplayLayout.Appearance = appearance128;
			comboBoxSponsor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSponsor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance129.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance129.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance129.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance129.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.GroupByBox.Appearance = appearance129;
			appearance130.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance130;
			comboBoxSponsor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance131.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance131.BackColor2 = System.Drawing.SystemColors.Control;
			appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance131.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.PromptAppearance = appearance131;
			comboBoxSponsor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSponsor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance132.BackColor = System.Drawing.SystemColors.Window;
			appearance132.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSponsor.DisplayLayout.Override.ActiveCellAppearance = appearance132;
			appearance133.BackColor = System.Drawing.SystemColors.Highlight;
			appearance133.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSponsor.DisplayLayout.Override.ActiveRowAppearance = appearance133;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance134.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.CardAreaAppearance = appearance134;
			appearance135.BorderColor = System.Drawing.Color.Silver;
			appearance135.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSponsor.DisplayLayout.Override.CellAppearance = appearance135;
			comboBoxSponsor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSponsor.DisplayLayout.Override.CellPadding = 0;
			appearance136.BackColor = System.Drawing.SystemColors.Control;
			appearance136.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance136.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance136.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance136.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.GroupByRowAppearance = appearance136;
			appearance137.TextHAlignAsString = "Left";
			comboBoxSponsor.DisplayLayout.Override.HeaderAppearance = appearance137;
			comboBoxSponsor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSponsor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance138.BackColor = System.Drawing.SystemColors.Window;
			appearance138.BorderColor = System.Drawing.Color.Silver;
			comboBoxSponsor.DisplayLayout.Override.RowAppearance = appearance138;
			comboBoxSponsor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance139.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSponsor.DisplayLayout.Override.TemplateAddRowAppearance = appearance139;
			comboBoxSponsor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSponsor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSponsor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSponsor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSponsor.Editable = true;
			comboBoxSponsor.FilterString = "";
			comboBoxSponsor.HasAllAccount = false;
			comboBoxSponsor.HasCustom = false;
			comboBoxSponsor.IsDataLoaded = false;
			comboBoxSponsor.Location = new System.Drawing.Point(158, 129);
			comboBoxSponsor.MaxDropDownItems = 12;
			comboBoxSponsor.Name = "comboBoxSponsor";
			comboBoxSponsor.ShowInactiveItems = false;
			comboBoxSponsor.ShowQuickAdd = true;
			comboBoxSponsor.Size = new System.Drawing.Size(124, 20);
			comboBoxSponsor.TabIndex = 5;
			comboBoxSponsor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel70.AutoSize = true;
			mmLabel70.BackColor = System.Drawing.Color.Transparent;
			mmLabel70.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel70.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel70.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel70.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel70.IsFieldHeader = false;
			mmLabel70.IsRequired = false;
			mmLabel70.Location = new System.Drawing.Point(10, 133);
			mmLabel70.Name = "mmLabel70";
			mmLabel70.PenWidth = 1f;
			mmLabel70.ShowBorder = false;
			mmLabel70.Size = new System.Drawing.Size(83, 13);
			mmLabel70.TabIndex = 86;
			mmLabel70.Text = "Sponsor Name :";
			mmLabel82.AutoSize = true;
			mmLabel82.BackColor = System.Drawing.Color.Transparent;
			mmLabel82.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel82.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel82.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel82.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel82.IsFieldHeader = false;
			mmLabel82.IsRequired = false;
			mmLabel82.Location = new System.Drawing.Point(296, 248);
			mmLabel82.Name = "mmLabel82";
			mmLabel82.PenWidth = 1f;
			mmLabel82.ShowBorder = false;
			mmLabel82.Size = new System.Drawing.Size(58, 13);
			mmLabel82.TabIndex = 84;
			mmLabel82.Text = "B/G Type :";
			dateTimeApprovalFeePaidOnMOL.Checked = false;
			dateTimeApprovalFeePaidOnMOL.CustomFormat = " dd-MMM-yyyy";
			dateTimeApprovalFeePaidOnMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApprovalFeePaidOnMOL.Location = new System.Drawing.Point(446, 212);
			dateTimeApprovalFeePaidOnMOL.Name = "dateTimeApprovalFeePaidOnMOL";
			dateTimeApprovalFeePaidOnMOL.ShowCheckBox = true;
			dateTimeApprovalFeePaidOnMOL.Size = new System.Drawing.Size(124, 20);
			dateTimeApprovalFeePaidOnMOL.TabIndex = 12;
			dateTimeApprovalFeePaidOnMOL.Value = new System.DateTime(0L);
			textBoxTempWPNo.BackColor = System.Drawing.Color.White;
			textBoxTempWPNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxTempWPNo.CustomReportFieldName = "";
			textBoxTempWPNo.CustomReportKey = "";
			textBoxTempWPNo.CustomReportValueType = 1;
			textBoxTempWPNo.IsComboTextBox = false;
			textBoxTempWPNo.IsModified = false;
			textBoxTempWPNo.Location = new System.Drawing.Point(158, 218);
			textBoxTempWPNo.MaxLength = 30;
			textBoxTempWPNo.Name = "textBoxTempWPNo";
			textBoxTempWPNo.Size = new System.Drawing.Size(124, 20);
			textBoxTempWPNo.TabIndex = 11;
			mmLabel80.AutoSize = true;
			mmLabel80.BackColor = System.Drawing.Color.Transparent;
			mmLabel80.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel80.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel80.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel80.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel80.IsFieldHeader = false;
			mmLabel80.IsRequired = false;
			mmLabel80.Location = new System.Drawing.Point(10, 221);
			mmLabel80.Name = "mmLabel80";
			mmLabel80.PenWidth = 1f;
			mmLabel80.ShowBorder = false;
			mmLabel80.Size = new System.Drawing.Size(75, 13);
			mmLabel80.TabIndex = 80;
			mmLabel80.Text = "Temp WP No :";
			dateTimeApprovalValidTillMOL.Checked = false;
			dateTimeApprovalValidTillMOL.CustomFormat = "dd-MMM-yyyy";
			dateTimeApprovalValidTillMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApprovalValidTillMOL.Location = new System.Drawing.Point(445, 190);
			dateTimeApprovalValidTillMOL.Name = "dateTimeApprovalValidTillMOL";
			dateTimeApprovalValidTillMOL.ShowCheckBox = true;
			dateTimeApprovalValidTillMOL.Size = new System.Drawing.Size(124, 20);
			dateTimeApprovalValidTillMOL.TabIndex = 10;
			dateTimeApprovalValidTillMOL.Value = new System.DateTime(0L);
			mmLabel79.AutoSize = true;
			mmLabel79.BackColor = System.Drawing.Color.Transparent;
			mmLabel79.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel79.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel79.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel79.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel79.IsFieldHeader = false;
			mmLabel79.IsRequired = false;
			mmLabel79.Location = new System.Drawing.Point(296, 204);
			mmLabel79.Name = "mmLabel79";
			mmLabel79.PenWidth = 1f;
			mmLabel79.ShowBorder = false;
			mmLabel79.Size = new System.Drawing.Size(97, 13);
			mmLabel79.TabIndex = 78;
			mmLabel79.Text = "Approval Valid Till :";
			dateTimeBGPaidOnMOL.Checked = false;
			dateTimeBGPaidOnMOL.CustomFormat = " dd-MMM-yyyy";
			dateTimeBGPaidOnMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeBGPaidOnMOL.Location = new System.Drawing.Point(158, 240);
			dateTimeBGPaidOnMOL.Name = "dateTimeBGPaidOnMOL";
			dateTimeBGPaidOnMOL.ShowCheckBox = true;
			dateTimeBGPaidOnMOL.Size = new System.Drawing.Size(124, 20);
			dateTimeBGPaidOnMOL.TabIndex = 13;
			dateTimeBGPaidOnMOL.Value = new System.DateTime(0L);
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(10, 243);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(71, 13);
			mmLabel25.TabIndex = 75;
			mmLabel25.Text = "B/G Paid On :";
			mmLabel24.AutoSize = true;
			mmLabel24.BackColor = System.Drawing.Color.Transparent;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel24.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(296, 226);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(118, 13);
			mmLabel24.TabIndex = 73;
			mmLabel24.Text = "Approval Fee Paid On :";
			dateTimeApprovalDateMOL.Checked = false;
			dateTimeApprovalDateMOL.CustomFormat = " dd-MMM-yyyy";
			dateTimeApprovalDateMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApprovalDateMOL.Location = new System.Drawing.Point(158, 196);
			dateTimeApprovalDateMOL.Name = "dateTimeApprovalDateMOL";
			dateTimeApprovalDateMOL.ShowCheckBox = true;
			dateTimeApprovalDateMOL.Size = new System.Drawing.Size(124, 20);
			dateTimeApprovalDateMOL.TabIndex = 9;
			dateTimeApprovalDateMOL.Value = new System.DateTime(0L);
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(10, 199);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(107, 13);
			mmLabel22.TabIndex = 71;
			mmLabel22.Text = "Approval Date MOL :";
			textBoxMOLMBNo.BackColor = System.Drawing.Color.White;
			textBoxMOLMBNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxMOLMBNo.CustomReportFieldName = "";
			textBoxMOLMBNo.CustomReportKey = "";
			textBoxMOLMBNo.CustomReportValueType = 1;
			textBoxMOLMBNo.IsComboTextBox = false;
			textBoxMOLMBNo.IsModified = false;
			textBoxMOLMBNo.Location = new System.Drawing.Point(158, 107);
			textBoxMOLMBNo.MaxLength = 30;
			textBoxMOLMBNo.Name = "textBoxMOLMBNo";
			textBoxMOLMBNo.Size = new System.Drawing.Size(205, 20);
			textBoxMOLMBNo.TabIndex = 4;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(10, 111);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(93, 13);
			mmLabel7.TabIndex = 69;
			mmLabel7.Text = "MB Ref No - Visa :";
			dateTimeApplTypingDateMOL.Checked = false;
			dateTimeApplTypingDateMOL.CustomFormat = "dd-MMM-yyyy";
			dateTimeApplTypingDateMOL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeApplTypingDateMOL.Location = new System.Drawing.Point(158, 85);
			dateTimeApplTypingDateMOL.Name = "dateTimeApplTypingDateMOL";
			dateTimeApplTypingDateMOL.ShowCheckBox = true;
			dateTimeApplTypingDateMOL.Size = new System.Drawing.Size(124, 20);
			dateTimeApplTypingDateMOL.TabIndex = 3;
			dateTimeApplTypingDateMOL.Value = new System.DateTime(0L);
			mmLabel30.AutoSize = true;
			mmLabel30.BackColor = System.Drawing.Color.Transparent;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel30.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(10, 89);
			mmLabel30.Name = "mmLabel30";
			mmLabel30.PenWidth = 1f;
			mmLabel30.ShowBorder = false;
			mmLabel30.Size = new System.Drawing.Size(100, 13);
			mmLabel30.TabIndex = 10;
			mmLabel30.Text = "Appl. Typing Date :";
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(362, 132);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(56, 13);
			mmLabel23.TabIndex = 32;
			mmLabel23.Text = "Comment:";
			textBoxComment.BackColor = System.Drawing.Color.White;
			textBoxComment.CustomReportFieldName = "";
			textBoxComment.CustomReportKey = "";
			textBoxComment.CustomReportValueType = 1;
			textBoxComment.IsComboTextBox = false;
			textBoxComment.IsModified = false;
			textBoxComment.Location = new System.Drawing.Point(460, 129);
			textBoxComment.MaxLength = 255;
			textBoxComment.Name = "textBoxComment";
			textBoxComment.Size = new System.Drawing.Size(229, 20);
			textBoxComment.TabIndex = 13;
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(9, 176);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(68, 13);
			mmLabel20.TabIndex = 14;
			mmLabel20.Text = "Postal Code:";
			textBoxPostalCode.BackColor = System.Drawing.Color.White;
			textBoxPostalCode.CustomReportFieldName = "";
			textBoxPostalCode.CustomReportKey = "";
			textBoxPostalCode.CustomReportValueType = 1;
			textBoxPostalCode.IsComboTextBox = false;
			textBoxPostalCode.IsModified = false;
			textBoxPostalCode.Location = new System.Drawing.Point(123, 173);
			textBoxPostalCode.MaxLength = 30;
			textBoxPostalCode.Name = "textBoxPostalCode";
			textBoxPostalCode.Size = new System.Drawing.Size(229, 20);
			textBoxPostalCode.TabIndex = 7;
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(362, 110);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(35, 13);
			mmLabel18.TabIndex = 28;
			mmLabel18.Text = "Email:";
			textBoxEmail.BackColor = System.Drawing.Color.White;
			textBoxEmail.CustomReportFieldName = "";
			textBoxEmail.CustomReportKey = "";
			textBoxEmail.CustomReportValueType = 1;
			textBoxEmail.IsComboTextBox = false;
			textBoxEmail.IsModified = false;
			textBoxEmail.Location = new System.Drawing.Point(460, 107);
			textBoxEmail.MaxLength = 64;
			textBoxEmail.Name = "textBoxEmail";
			textBoxEmail.Size = new System.Drawing.Size(229, 20);
			textBoxEmail.TabIndex = 12;
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(362, 88);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(41, 13);
			mmLabel17.TabIndex = 26;
			mmLabel17.Text = "Mobile:";
			textBoxMobile.BackColor = System.Drawing.Color.White;
			textBoxMobile.CustomReportFieldName = "";
			textBoxMobile.CustomReportKey = "";
			textBoxMobile.CustomReportValueType = 1;
			textBoxMobile.IsComboTextBox = false;
			textBoxMobile.IsModified = false;
			textBoxMobile.Location = new System.Drawing.Point(460, 85);
			textBoxMobile.MaxLength = 30;
			textBoxMobile.Name = "textBoxMobile";
			textBoxMobile.Size = new System.Drawing.Size(229, 20);
			textBoxMobile.TabIndex = 11;
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(362, 65);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(29, 13);
			mmLabel16.TabIndex = 24;
			mmLabel16.Text = "Fax:";
			textBoxFax.BackColor = System.Drawing.Color.White;
			textBoxFax.CustomReportFieldName = "";
			textBoxFax.CustomReportKey = "";
			textBoxFax.CustomReportValueType = 1;
			textBoxFax.IsComboTextBox = false;
			textBoxFax.IsModified = false;
			textBoxFax.Location = new System.Drawing.Point(460, 63);
			textBoxFax.MaxLength = 30;
			textBoxFax.Name = "textBoxFax";
			textBoxFax.Size = new System.Drawing.Size(229, 20);
			textBoxFax.TabIndex = 10;
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(362, 44);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(50, 13);
			mmLabel15.TabIndex = 22;
			mmLabel15.Text = "Phone 2:";
			textBoxPhone2.BackColor = System.Drawing.Color.White;
			textBoxPhone2.CustomReportFieldName = "";
			textBoxPhone2.CustomReportKey = "";
			textBoxPhone2.CustomReportValueType = 1;
			textBoxPhone2.IsComboTextBox = false;
			textBoxPhone2.IsModified = false;
			textBoxPhone2.Location = new System.Drawing.Point(460, 41);
			textBoxPhone2.MaxLength = 30;
			textBoxPhone2.Name = "textBoxPhone2";
			textBoxPhone2.Size = new System.Drawing.Size(229, 20);
			textBoxPhone2.TabIndex = 9;
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(362, 22);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(50, 13);
			mmLabel14.TabIndex = 20;
			mmLabel14.Text = "Phone 1:";
			textBoxPhone1.BackColor = System.Drawing.Color.White;
			textBoxPhone1.CustomReportFieldName = "";
			textBoxPhone1.CustomReportKey = "";
			textBoxPhone1.CustomReportValueType = 1;
			textBoxPhone1.IsComboTextBox = false;
			textBoxPhone1.IsModified = false;
			textBoxPhone1.Location = new System.Drawing.Point(460, 19);
			textBoxPhone1.MaxLength = 30;
			textBoxPhone1.Name = "textBoxPhone1";
			textBoxPhone1.Size = new System.Drawing.Size(229, 20);
			textBoxPhone1.TabIndex = 8;
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(9, 154);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(50, 13);
			mmLabel12.TabIndex = 12;
			mmLabel12.Text = "Country:";
			textBoxCountry.BackColor = System.Drawing.Color.White;
			textBoxCountry.CustomReportFieldName = "";
			textBoxCountry.CustomReportKey = "";
			textBoxCountry.CustomReportValueType = 1;
			textBoxCountry.IsComboTextBox = false;
			textBoxCountry.IsModified = false;
			textBoxCountry.Location = new System.Drawing.Point(123, 151);
			textBoxCountry.MaxLength = 30;
			textBoxCountry.Name = "textBoxCountry";
			textBoxCountry.Size = new System.Drawing.Size(229, 20);
			textBoxCountry.TabIndex = 6;
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(9, 132);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(37, 13);
			mmLabel11.TabIndex = 10;
			mmLabel11.Text = "State:";
			textBoxState.BackColor = System.Drawing.Color.White;
			textBoxState.CustomReportFieldName = "";
			textBoxState.CustomReportKey = "";
			textBoxState.CustomReportValueType = 1;
			textBoxState.IsComboTextBox = false;
			textBoxState.IsModified = false;
			textBoxState.Location = new System.Drawing.Point(123, 129);
			textBoxState.MaxLength = 30;
			textBoxState.Name = "textBoxState";
			textBoxState.Size = new System.Drawing.Size(229, 20);
			textBoxState.TabIndex = 5;
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(9, 109);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(30, 13);
			mmLabel13.TabIndex = 8;
			mmLabel13.Text = "City:";
			textBoxCity.BackColor = System.Drawing.Color.White;
			textBoxCity.CustomReportFieldName = "";
			textBoxCity.CustomReportKey = "";
			textBoxCity.CustomReportValueType = 1;
			textBoxCity.IsComboTextBox = false;
			textBoxCity.IsModified = false;
			textBoxCity.Location = new System.Drawing.Point(123, 107);
			textBoxCity.MaxLength = 30;
			textBoxCity.Name = "textBoxCity";
			textBoxCity.Size = new System.Drawing.Size(229, 20);
			textBoxCity.TabIndex = 4;
			textBoxAddress3.BackColor = System.Drawing.Color.White;
			textBoxAddress3.CustomReportFieldName = "";
			textBoxAddress3.CustomReportKey = "";
			textBoxAddress3.CustomReportValueType = 1;
			textBoxAddress3.IsComboTextBox = false;
			textBoxAddress3.IsModified = false;
			textBoxAddress3.Location = new System.Drawing.Point(123, 85);
			textBoxAddress3.MaxLength = 64;
			textBoxAddress3.Name = "textBoxAddress3";
			textBoxAddress3.Size = new System.Drawing.Size(229, 20);
			textBoxAddress3.TabIndex = 3;
			textBoxAddress2.BackColor = System.Drawing.Color.White;
			textBoxAddress2.CustomReportFieldName = "";
			textBoxAddress2.CustomReportKey = "";
			textBoxAddress2.CustomReportValueType = 1;
			textBoxAddress2.IsComboTextBox = false;
			textBoxAddress2.IsModified = false;
			textBoxAddress2.Location = new System.Drawing.Point(123, 63);
			textBoxAddress2.MaxLength = 64;
			textBoxAddress2.Name = "textBoxAddress2";
			textBoxAddress2.Size = new System.Drawing.Size(229, 20);
			textBoxAddress2.TabIndex = 2;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(9, 43);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(50, 13);
			mmLabel10.TabIndex = 4;
			mmLabel10.Text = "Address:";
			textBoxAddress1.BackColor = System.Drawing.Color.White;
			textBoxAddress1.CustomReportFieldName = "";
			textBoxAddress1.CustomReportKey = "";
			textBoxAddress1.CustomReportValueType = 1;
			textBoxAddress1.IsComboTextBox = false;
			textBoxAddress1.IsModified = false;
			textBoxAddress1.Location = new System.Drawing.Point(123, 41);
			textBoxAddress1.MaxLength = 64;
			textBoxAddress1.Name = "textBoxAddress1";
			textBoxAddress1.Size = new System.Drawing.Size(229, 20);
			textBoxAddress1.TabIndex = 1;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(9, 22);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(64, 13);
			mmLabel8.TabIndex = 0;
			mmLabel8.Text = "Address ID:";
			textBoxAddressID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAddressID.CustomReportFieldName = "";
			textBoxAddressID.CustomReportKey = "";
			textBoxAddressID.CustomReportValueType = 1;
			textBoxAddressID.Enabled = false;
			textBoxAddressID.ForeColor = System.Drawing.Color.Black;
			textBoxAddressID.IsComboTextBox = false;
			textBoxAddressID.IsModified = false;
			textBoxAddressID.Location = new System.Drawing.Point(123, 19);
			textBoxAddressID.MaxLength = 15;
			textBoxAddressID.Name = "textBoxAddressID";
			textBoxAddressID.Size = new System.Drawing.Size(229, 20);
			textBoxAddressID.TabIndex = 0;
			textBoxAddressID.Text = "PRIMARY";
			dataGridPayrollItem.AllowAddNew = false;
			dataGridPayrollItem.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance140.BackColor = System.Drawing.SystemColors.Window;
			appearance140.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPayrollItem.DisplayLayout.Appearance = appearance140;
			dataGridPayrollItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPayrollItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance141.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance141.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance141.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance141.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayrollItem.DisplayLayout.GroupByBox.Appearance = appearance141;
			appearance142.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayrollItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance142;
			dataGridPayrollItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance143.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance143.BackColor2 = System.Drawing.SystemColors.Control;
			appearance143.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance143.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayrollItem.DisplayLayout.GroupByBox.PromptAppearance = appearance143;
			dataGridPayrollItem.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPayrollItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance144.BackColor = System.Drawing.SystemColors.Window;
			appearance144.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPayrollItem.DisplayLayout.Override.ActiveCellAppearance = appearance144;
			appearance145.BackColor = System.Drawing.SystemColors.Highlight;
			appearance145.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPayrollItem.DisplayLayout.Override.ActiveRowAppearance = appearance145;
			dataGridPayrollItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridPayrollItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPayrollItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance146.BackColor = System.Drawing.SystemColors.Window;
			dataGridPayrollItem.DisplayLayout.Override.CardAreaAppearance = appearance146;
			appearance147.BorderColor = System.Drawing.Color.Silver;
			appearance147.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPayrollItem.DisplayLayout.Override.CellAppearance = appearance147;
			dataGridPayrollItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPayrollItem.DisplayLayout.Override.CellPadding = 0;
			appearance148.BackColor = System.Drawing.SystemColors.Control;
			appearance148.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance148.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance148.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance148.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayrollItem.DisplayLayout.Override.GroupByRowAppearance = appearance148;
			appearance149.TextHAlignAsString = "Left";
			dataGridPayrollItem.DisplayLayout.Override.HeaderAppearance = appearance149;
			dataGridPayrollItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPayrollItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance150.BackColor = System.Drawing.SystemColors.Window;
			appearance150.BorderColor = System.Drawing.Color.Silver;
			dataGridPayrollItem.DisplayLayout.Override.RowAppearance = appearance150;
			dataGridPayrollItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance151.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPayrollItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance151;
			dataGridPayrollItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPayrollItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPayrollItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPayrollItem.IncludeLotItems = false;
			dataGridPayrollItem.LoadLayoutFailed = false;
			dataGridPayrollItem.Location = new System.Drawing.Point(12, 17);
			dataGridPayrollItem.Name = "dataGridPayrollItem";
			dataGridPayrollItem.ShowClearMenu = true;
			dataGridPayrollItem.ShowDeleteMenu = true;
			dataGridPayrollItem.ShowInsertMenu = true;
			dataGridPayrollItem.ShowMoveRowsMenu = true;
			dataGridPayrollItem.Size = new System.Drawing.Size(687, 267);
			dataGridPayrollItem.TabIndex = 17;
			dataGridPayrollItem.Text = "dataEntryGrid1";
			comboBoxPayrollItem.Assigned = false;
			comboBoxPayrollItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayrollItem.CustomReportFieldName = "";
			comboBoxPayrollItem.CustomReportKey = "";
			comboBoxPayrollItem.CustomReportValueType = 1;
			comboBoxPayrollItem.DescriptionTextBox = null;
			appearance152.BackColor = System.Drawing.SystemColors.Window;
			appearance152.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayrollItem.DisplayLayout.Appearance = appearance152;
			comboBoxPayrollItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayrollItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance153.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance153.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance153.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance153.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.Appearance = appearance153;
			appearance154.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance154;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance155.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance155.BackColor2 = System.Drawing.SystemColors.Control;
			appearance155.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance155.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.PromptAppearance = appearance155;
			comboBoxPayrollItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayrollItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance156.BackColor = System.Drawing.SystemColors.Window;
			appearance156.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayrollItem.DisplayLayout.Override.ActiveCellAppearance = appearance156;
			appearance157.BackColor = System.Drawing.SystemColors.Highlight;
			appearance157.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayrollItem.DisplayLayout.Override.ActiveRowAppearance = appearance157;
			comboBoxPayrollItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayrollItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance158.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItem.DisplayLayout.Override.CardAreaAppearance = appearance158;
			appearance159.BorderColor = System.Drawing.Color.Silver;
			appearance159.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayrollItem.DisplayLayout.Override.CellAppearance = appearance159;
			comboBoxPayrollItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayrollItem.DisplayLayout.Override.CellPadding = 0;
			appearance160.BackColor = System.Drawing.SystemColors.Control;
			appearance160.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance160.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance160.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance160.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItem.DisplayLayout.Override.GroupByRowAppearance = appearance160;
			appearance161.TextHAlignAsString = "Left";
			comboBoxPayrollItem.DisplayLayout.Override.HeaderAppearance = appearance161;
			comboBoxPayrollItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayrollItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance162.BackColor = System.Drawing.SystemColors.Window;
			appearance162.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayrollItem.DisplayLayout.Override.RowAppearance = appearance162;
			comboBoxPayrollItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance163.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayrollItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance163;
			comboBoxPayrollItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayrollItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayrollItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayrollItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayrollItem.Editable = true;
			comboBoxPayrollItem.FilterString = "";
			comboBoxPayrollItem.HasAllAccount = false;
			comboBoxPayrollItem.HasCustom = false;
			comboBoxPayrollItem.IsDataLoaded = false;
			comboBoxPayrollItem.IsDeduction = false;
			comboBoxPayrollItem.Location = new System.Drawing.Point(560, 3);
			comboBoxPayrollItem.MaxDropDownItems = 12;
			comboBoxPayrollItem.Name = "comboBoxPayrollItem";
			comboBoxPayrollItem.ShowInactiveItems = false;
			comboBoxPayrollItem.ShowQuickAdd = true;
			comboBoxPayrollItem.Size = new System.Drawing.Size(81, 21);
			comboBoxPayrollItem.TabIndex = 19;
			comboBoxPayrollItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPayrollItem.Visible = false;
			textBoxTotalSalary.AllowDecimal = true;
			textBoxTotalSalary.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalSalary.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalSalary.CustomReportFieldName = "";
			textBoxTotalSalary.CustomReportKey = "";
			textBoxTotalSalary.CustomReportValueType = 1;
			textBoxTotalSalary.ForeColor = System.Drawing.Color.Black;
			textBoxTotalSalary.IsComboTextBox = false;
			textBoxTotalSalary.IsModified = false;
			textBoxTotalSalary.Location = new System.Drawing.Point(585, 284);
			textBoxTotalSalary.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalSalary.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalSalary.Name = "textBoxTotalSalary";
			textBoxTotalSalary.NullText = "0";
			textBoxTotalSalary.ReadOnly = true;
			textBoxTotalSalary.Size = new System.Drawing.Size(114, 21);
			textBoxTotalSalary.TabIndex = 9;
			textBoxTotalSalary.TabStop = false;
			textBoxTotalSalary.Text = "0.00";
			textBoxTotalSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalSalary.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			comboBoxDeduction.Assigned = false;
			comboBoxDeduction.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDeduction.CustomReportFieldName = "";
			comboBoxDeduction.CustomReportKey = "";
			comboBoxDeduction.CustomReportValueType = 1;
			comboBoxDeduction.DescriptionTextBox = null;
			appearance164.BackColor = System.Drawing.SystemColors.Window;
			appearance164.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDeduction.DisplayLayout.Appearance = appearance164;
			comboBoxDeduction.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDeduction.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance165.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance165.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance165.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance165.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDeduction.DisplayLayout.GroupByBox.Appearance = appearance165;
			appearance166.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDeduction.DisplayLayout.GroupByBox.BandLabelAppearance = appearance166;
			comboBoxDeduction.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance167.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance167.BackColor2 = System.Drawing.SystemColors.Control;
			appearance167.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance167.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDeduction.DisplayLayout.GroupByBox.PromptAppearance = appearance167;
			comboBoxDeduction.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDeduction.DisplayLayout.MaxRowScrollRegions = 1;
			appearance168.BackColor = System.Drawing.SystemColors.Window;
			appearance168.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDeduction.DisplayLayout.Override.ActiveCellAppearance = appearance168;
			appearance169.BackColor = System.Drawing.SystemColors.Highlight;
			appearance169.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDeduction.DisplayLayout.Override.ActiveRowAppearance = appearance169;
			comboBoxDeduction.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDeduction.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance170.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDeduction.DisplayLayout.Override.CardAreaAppearance = appearance170;
			appearance171.BorderColor = System.Drawing.Color.Silver;
			appearance171.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDeduction.DisplayLayout.Override.CellAppearance = appearance171;
			comboBoxDeduction.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDeduction.DisplayLayout.Override.CellPadding = 0;
			appearance172.BackColor = System.Drawing.SystemColors.Control;
			appearance172.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance172.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance172.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance172.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDeduction.DisplayLayout.Override.GroupByRowAppearance = appearance172;
			appearance173.TextHAlignAsString = "Left";
			comboBoxDeduction.DisplayLayout.Override.HeaderAppearance = appearance173;
			comboBoxDeduction.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDeduction.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance174.BackColor = System.Drawing.SystemColors.Window;
			appearance174.BorderColor = System.Drawing.Color.Silver;
			comboBoxDeduction.DisplayLayout.Override.RowAppearance = appearance174;
			comboBoxDeduction.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance175.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDeduction.DisplayLayout.Override.TemplateAddRowAppearance = appearance175;
			comboBoxDeduction.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDeduction.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDeduction.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDeduction.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDeduction.Editable = true;
			comboBoxDeduction.FilterString = "";
			comboBoxDeduction.HasAllAccount = false;
			comboBoxDeduction.HasCustom = false;
			comboBoxDeduction.IsDataLoaded = false;
			comboBoxDeduction.IsDeduction = true;
			comboBoxDeduction.Location = new System.Drawing.Point(440, 3);
			comboBoxDeduction.MaxDropDownItems = 12;
			comboBoxDeduction.Name = "comboBoxDeduction";
			comboBoxDeduction.ShowInactiveItems = false;
			comboBoxDeduction.ShowQuickAdd = true;
			comboBoxDeduction.Size = new System.Drawing.Size(81, 21);
			comboBoxDeduction.TabIndex = 28;
			comboBoxDeduction.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDeduction.Visible = false;
			dataGridDeduction.AllowAddNew = false;
			dataGridDeduction.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance176.BackColor = System.Drawing.SystemColors.Window;
			appearance176.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridDeduction.DisplayLayout.Appearance = appearance176;
			dataGridDeduction.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridDeduction.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance177.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance177.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance177.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance177.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDeduction.DisplayLayout.GroupByBox.Appearance = appearance177;
			appearance178.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDeduction.DisplayLayout.GroupByBox.BandLabelAppearance = appearance178;
			dataGridDeduction.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance179.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance179.BackColor2 = System.Drawing.SystemColors.Control;
			appearance179.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance179.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDeduction.DisplayLayout.GroupByBox.PromptAppearance = appearance179;
			dataGridDeduction.DisplayLayout.MaxColScrollRegions = 1;
			dataGridDeduction.DisplayLayout.MaxRowScrollRegions = 1;
			appearance180.BackColor = System.Drawing.SystemColors.Window;
			appearance180.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridDeduction.DisplayLayout.Override.ActiveCellAppearance = appearance180;
			appearance181.BackColor = System.Drawing.SystemColors.Highlight;
			appearance181.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridDeduction.DisplayLayout.Override.ActiveRowAppearance = appearance181;
			dataGridDeduction.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridDeduction.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridDeduction.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance182.BackColor = System.Drawing.SystemColors.Window;
			dataGridDeduction.DisplayLayout.Override.CardAreaAppearance = appearance182;
			appearance183.BorderColor = System.Drawing.Color.Silver;
			appearance183.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridDeduction.DisplayLayout.Override.CellAppearance = appearance183;
			dataGridDeduction.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridDeduction.DisplayLayout.Override.CellPadding = 0;
			appearance184.BackColor = System.Drawing.SystemColors.Control;
			appearance184.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance184.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance184.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance184.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDeduction.DisplayLayout.Override.GroupByRowAppearance = appearance184;
			appearance185.TextHAlignAsString = "Left";
			dataGridDeduction.DisplayLayout.Override.HeaderAppearance = appearance185;
			dataGridDeduction.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridDeduction.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance186.BackColor = System.Drawing.SystemColors.Window;
			appearance186.BorderColor = System.Drawing.Color.Silver;
			dataGridDeduction.DisplayLayout.Override.RowAppearance = appearance186;
			dataGridDeduction.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance187.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridDeduction.DisplayLayout.Override.TemplateAddRowAppearance = appearance187;
			dataGridDeduction.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridDeduction.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridDeduction.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridDeduction.IncludeLotItems = false;
			dataGridDeduction.LoadLayoutFailed = false;
			dataGridDeduction.Location = new System.Drawing.Point(12, 17);
			dataGridDeduction.Name = "dataGridDeduction";
			dataGridDeduction.ShowClearMenu = true;
			dataGridDeduction.ShowDeleteMenu = true;
			dataGridDeduction.ShowInsertMenu = true;
			dataGridDeduction.ShowMoveRowsMenu = true;
			dataGridDeduction.Size = new System.Drawing.Size(687, 267);
			dataGridDeduction.TabIndex = 18;
			dataGridDeduction.Text = "dataEntryGrid1";
			comboBoxBenefit.Assigned = false;
			comboBoxBenefit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBenefit.CustomReportFieldName = "";
			comboBoxBenefit.CustomReportKey = "";
			comboBoxBenefit.CustomReportValueType = 1;
			comboBoxBenefit.DescriptionTextBox = null;
			appearance188.BackColor = System.Drawing.SystemColors.Window;
			appearance188.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBenefit.DisplayLayout.Appearance = appearance188;
			comboBoxBenefit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBenefit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance189.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance189.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance189.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance189.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBenefit.DisplayLayout.GroupByBox.Appearance = appearance189;
			appearance190.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBenefit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance190;
			comboBoxBenefit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance191.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance191.BackColor2 = System.Drawing.SystemColors.Control;
			appearance191.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance191.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBenefit.DisplayLayout.GroupByBox.PromptAppearance = appearance191;
			comboBoxBenefit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBenefit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance192.BackColor = System.Drawing.SystemColors.Window;
			appearance192.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBenefit.DisplayLayout.Override.ActiveCellAppearance = appearance192;
			appearance193.BackColor = System.Drawing.SystemColors.Highlight;
			appearance193.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBenefit.DisplayLayout.Override.ActiveRowAppearance = appearance193;
			comboBoxBenefit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBenefit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance194.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBenefit.DisplayLayout.Override.CardAreaAppearance = appearance194;
			appearance195.BorderColor = System.Drawing.Color.Silver;
			appearance195.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBenefit.DisplayLayout.Override.CellAppearance = appearance195;
			comboBoxBenefit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBenefit.DisplayLayout.Override.CellPadding = 0;
			appearance196.BackColor = System.Drawing.SystemColors.Control;
			appearance196.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance196.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance196.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance196.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBenefit.DisplayLayout.Override.GroupByRowAppearance = appearance196;
			appearance197.TextHAlignAsString = "Left";
			comboBoxBenefit.DisplayLayout.Override.HeaderAppearance = appearance197;
			comboBoxBenefit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBenefit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance198.BackColor = System.Drawing.SystemColors.Window;
			appearance198.BorderColor = System.Drawing.Color.Silver;
			comboBoxBenefit.DisplayLayout.Override.RowAppearance = appearance198;
			comboBoxBenefit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance199.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBenefit.DisplayLayout.Override.TemplateAddRowAppearance = appearance199;
			comboBoxBenefit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBenefit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBenefit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBenefit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBenefit.Editable = true;
			comboBoxBenefit.FilterString = "";
			comboBoxBenefit.HasAllAccount = false;
			comboBoxBenefit.HasCustom = false;
			comboBoxBenefit.IsDataLoaded = false;
			comboBoxBenefit.IsNonFinancial = true;
			comboBoxBenefit.Location = new System.Drawing.Point(510, 6);
			comboBoxBenefit.MaxDropDownItems = 12;
			comboBoxBenefit.Name = "comboBoxBenefit";
			comboBoxBenefit.ShowInactiveItems = false;
			comboBoxBenefit.ShowQuickAdd = true;
			comboBoxBenefit.Size = new System.Drawing.Size(92, 21);
			comboBoxBenefit.TabIndex = 19;
			comboBoxBenefit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBenefit.Visible = false;
			dataGridBenefit.AllowAddNew = false;
			dataGridBenefit.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance200.BackColor = System.Drawing.SystemColors.Window;
			appearance200.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridBenefit.DisplayLayout.Appearance = appearance200;
			dataGridBenefit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridBenefit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance201.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance201.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance201.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance201.BorderColor = System.Drawing.SystemColors.Window;
			dataGridBenefit.DisplayLayout.GroupByBox.Appearance = appearance201;
			appearance202.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridBenefit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance202;
			dataGridBenefit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance203.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance203.BackColor2 = System.Drawing.SystemColors.Control;
			appearance203.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance203.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridBenefit.DisplayLayout.GroupByBox.PromptAppearance = appearance203;
			dataGridBenefit.DisplayLayout.MaxColScrollRegions = 1;
			dataGridBenefit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance204.BackColor = System.Drawing.SystemColors.Window;
			appearance204.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridBenefit.DisplayLayout.Override.ActiveCellAppearance = appearance204;
			appearance205.BackColor = System.Drawing.SystemColors.Highlight;
			appearance205.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridBenefit.DisplayLayout.Override.ActiveRowAppearance = appearance205;
			dataGridBenefit.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridBenefit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridBenefit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance206.BackColor = System.Drawing.SystemColors.Window;
			dataGridBenefit.DisplayLayout.Override.CardAreaAppearance = appearance206;
			appearance207.BorderColor = System.Drawing.Color.Silver;
			appearance207.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridBenefit.DisplayLayout.Override.CellAppearance = appearance207;
			dataGridBenefit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridBenefit.DisplayLayout.Override.CellPadding = 0;
			appearance208.BackColor = System.Drawing.SystemColors.Control;
			appearance208.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance208.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance208.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance208.BorderColor = System.Drawing.SystemColors.Window;
			dataGridBenefit.DisplayLayout.Override.GroupByRowAppearance = appearance208;
			appearance209.TextHAlignAsString = "Left";
			dataGridBenefit.DisplayLayout.Override.HeaderAppearance = appearance209;
			dataGridBenefit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridBenefit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance210.BackColor = System.Drawing.SystemColors.Window;
			appearance210.BorderColor = System.Drawing.Color.Silver;
			dataGridBenefit.DisplayLayout.Override.RowAppearance = appearance210;
			dataGridBenefit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance211.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridBenefit.DisplayLayout.Override.TemplateAddRowAppearance = appearance211;
			dataGridBenefit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridBenefit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridBenefit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridBenefit.IncludeLotItems = false;
			dataGridBenefit.LoadLayoutFailed = false;
			dataGridBenefit.Location = new System.Drawing.Point(12, 17);
			dataGridBenefit.Name = "dataGridBenefit";
			dataGridBenefit.ShowClearMenu = true;
			dataGridBenefit.ShowDeleteMenu = true;
			dataGridBenefit.ShowInsertMenu = true;
			dataGridBenefit.ShowMoveRowsMenu = true;
			dataGridBenefit.Size = new System.Drawing.Size(687, 267);
			dataGridBenefit.TabIndex = 18;
			dataGridBenefit.Text = "dataEntryGrid1";
			textBoxTicketPeriod.AllowDecimal = false;
			textBoxTicketPeriod.CustomReportFieldName = "";
			textBoxTicketPeriod.CustomReportKey = "";
			textBoxTicketPeriod.CustomReportValueType = 1;
			textBoxTicketPeriod.IsComboTextBox = false;
			textBoxTicketPeriod.IsModified = false;
			textBoxTicketPeriod.Location = new System.Drawing.Point(127, 63);
			textBoxTicketPeriod.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTicketPeriod.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTicketPeriod.Name = "textBoxTicketPeriod";
			textBoxTicketPeriod.NullText = "0";
			textBoxTicketPeriod.Size = new System.Drawing.Size(114, 21);
			textBoxTicketPeriod.TabIndex = 2;
			textBoxTicketPeriod.Text = "0";
			textBoxTicketPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxNumberOfTickets.AllowDecimal = false;
			textBoxNumberOfTickets.CustomReportFieldName = "";
			textBoxNumberOfTickets.CustomReportKey = "";
			textBoxNumberOfTickets.CustomReportValueType = 1;
			textBoxNumberOfTickets.IsComboTextBox = false;
			textBoxNumberOfTickets.IsModified = false;
			textBoxNumberOfTickets.Location = new System.Drawing.Point(127, 40);
			textBoxNumberOfTickets.MaxLength = 2;
			textBoxNumberOfTickets.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxNumberOfTickets.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxNumberOfTickets.Name = "textBoxNumberOfTickets";
			textBoxNumberOfTickets.NullText = "0";
			textBoxNumberOfTickets.Size = new System.Drawing.Size(114, 21);
			textBoxNumberOfTickets.TabIndex = 1;
			textBoxNumberOfTickets.Text = "0";
			textBoxNumberOfTickets.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTicketAmount.AllowDecimal = true;
			textBoxTicketAmount.CustomReportFieldName = "";
			textBoxTicketAmount.CustomReportKey = "";
			textBoxTicketAmount.CustomReportValueType = 1;
			textBoxTicketAmount.IsComboTextBox = false;
			textBoxTicketAmount.IsModified = false;
			textBoxTicketAmount.Location = new System.Drawing.Point(127, 86);
			textBoxTicketAmount.MaxLength = 15;
			textBoxTicketAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTicketAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTicketAmount.Name = "textBoxTicketAmount";
			textBoxTicketAmount.NullText = "0";
			textBoxTicketAmount.Size = new System.Drawing.Size(114, 21);
			textBoxTicketAmount.TabIndex = 3;
			textBoxTicketAmount.Text = "0.00";
			textBoxTicketAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTicketAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			comboBoxDestination.Assigned = false;
			comboBoxDestination.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDestination.CustomReportFieldName = "";
			comboBoxDestination.CustomReportKey = "";
			comboBoxDestination.CustomReportValueType = 1;
			comboBoxDestination.DescriptionTextBox = null;
			appearance212.BackColor = System.Drawing.SystemColors.Window;
			appearance212.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDestination.DisplayLayout.Appearance = appearance212;
			comboBoxDestination.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDestination.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance213.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance213.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance213.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance213.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDestination.DisplayLayout.GroupByBox.Appearance = appearance213;
			appearance214.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDestination.DisplayLayout.GroupByBox.BandLabelAppearance = appearance214;
			comboBoxDestination.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance215.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance215.BackColor2 = System.Drawing.SystemColors.Control;
			appearance215.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance215.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDestination.DisplayLayout.GroupByBox.PromptAppearance = appearance215;
			comboBoxDestination.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDestination.DisplayLayout.MaxRowScrollRegions = 1;
			appearance216.BackColor = System.Drawing.SystemColors.Window;
			appearance216.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDestination.DisplayLayout.Override.ActiveCellAppearance = appearance216;
			appearance217.BackColor = System.Drawing.SystemColors.Highlight;
			appearance217.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDestination.DisplayLayout.Override.ActiveRowAppearance = appearance217;
			comboBoxDestination.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDestination.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance218.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDestination.DisplayLayout.Override.CardAreaAppearance = appearance218;
			appearance219.BorderColor = System.Drawing.Color.Silver;
			appearance219.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDestination.DisplayLayout.Override.CellAppearance = appearance219;
			comboBoxDestination.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDestination.DisplayLayout.Override.CellPadding = 0;
			appearance220.BackColor = System.Drawing.SystemColors.Control;
			appearance220.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance220.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance220.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance220.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDestination.DisplayLayout.Override.GroupByRowAppearance = appearance220;
			appearance221.TextHAlignAsString = "Left";
			comboBoxDestination.DisplayLayout.Override.HeaderAppearance = appearance221;
			comboBoxDestination.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDestination.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance222.BackColor = System.Drawing.SystemColors.Window;
			appearance222.BorderColor = System.Drawing.Color.Silver;
			comboBoxDestination.DisplayLayout.Override.RowAppearance = appearance222;
			comboBoxDestination.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance223.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDestination.DisplayLayout.Override.TemplateAddRowAppearance = appearance223;
			comboBoxDestination.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDestination.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDestination.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDestination.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDestination.Editable = true;
			comboBoxDestination.FilterString = "";
			comboBoxDestination.HasAllAccount = false;
			comboBoxDestination.HasCustom = false;
			comboBoxDestination.IsDataLoaded = false;
			comboBoxDestination.Location = new System.Drawing.Point(127, 17);
			comboBoxDestination.MaxDropDownItems = 12;
			comboBoxDestination.Name = "comboBoxDestination";
			comboBoxDestination.ShowInactiveItems = false;
			comboBoxDestination.ShowQuickAdd = true;
			comboBoxDestination.Size = new System.Drawing.Size(114, 21);
			comboBoxDestination.TabIndex = 0;
			comboBoxDestination.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			labelCustomerNameHeader.AutoSize = true;
			labelCustomerNameHeader.BackColor = System.Drawing.Color.Transparent;
			labelCustomerNameHeader.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCustomerNameHeader.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCustomerNameHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCustomerNameHeader.IsFieldHeader = false;
			labelCustomerNameHeader.IsRequired = true;
			labelCustomerNameHeader.Location = new System.Drawing.Point(29, 7);
			labelCustomerNameHeader.Name = "labelCustomerNameHeader";
			labelCustomerNameHeader.PenWidth = 1f;
			labelCustomerNameHeader.ShowBorder = false;
			labelCustomerNameHeader.Size = new System.Drawing.Size(0, 13);
			labelCustomerNameHeader.TabIndex = 2;
			labelCustomerNameHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(814, 1);
			linePanelDown.TabIndex = 0;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Visible = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.BackColor = System.Drawing.Color.DarkGray;
			buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClose.Location = new System.Drawing.Point(704, 8);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(96, 24);
			buttonClose.TabIndex = 3;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = false;
			buttonClose.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
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
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(814, 731);
			base.Controls.Add(ultraTabControl);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(830, 670);
			base.Name = "CandidateDetailsForm";
			Text = "Candidate Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(CandidateClassDetailsForm_FormClosing);
			base.Load += new System.EventHandler(CandidateDetailsForm_Load);
			ultraTabPageControl7.ResumeLayout(false);
			ultraTabPageControl7.PerformLayout();
			ultraTabPageControl8.ResumeLayout(false);
			ultraTabPageControl8.PerformLayout();
			ultraTabPageControl9.ResumeLayout(false);
			ultraTabPageControl9.PerformLayout();
			ultraTabPageControl10.ResumeLayout(false);
			ultraTabPageControl10.PerformLayout();
			tabPageGeneral.ResumeLayout(false);
			panelGeneral.ResumeLayout(false);
			panelGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			tabPageDetails.ResumeLayout(false);
			panelRecruitment.ResumeLayout(false);
			panelRecruitment.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericExperienceAbroad).EndInit();
			((System.ComponentModel.ISupportInitialize)numericExperienceLocal).EndInit();
			ultraTabPageControl4.ResumeLayout(false);
			panelVisaProcess.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)panelVisaIMG).EndInit();
			panelVisaIMG.ResumeLayout(false);
			panelVisaIMG.PerformLayout();
			((System.ComponentModel.ISupportInitialize)panelVisaMOL).EndInit();
			panelVisaMOL.ResumeLayout(false);
			panelVisaMOL.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			panelArrival.ResumeLayout(false);
			panelArrival.PerformLayout();
			ultraTabPageControl3.ResumeLayout(false);
			panelWPRP.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)panelMedicalReport).EndInit();
			panelMedicalReport.ResumeLayout(false);
			panelMedicalReport.PerformLayout();
			((System.ComponentModel.ISupportInitialize)panelAGT).EndInit();
			panelAGT.ResumeLayout(false);
			panelAGT.PerformLayout();
			ultraTabPageControl2.ResumeLayout(false);
			panelMedicalEmirates.ResumeLayout(false);
			panelMedicalEmirates.PerformLayout();
			((System.ComponentModel.ISupportInitialize)panelEmirates).EndInit();
			panelEmirates.ResumeLayout(false);
			panelEmirates.PerformLayout();
			((System.ComponentModel.ISupportInitialize)panelMedicalDetail).EndInit();
			panelMedicalDetail.ResumeLayout(false);
			panelMedicalDetail.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			ultraTabPageControl5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			ultraTabPageControl6.ResumeLayout(false);
			panelSalaryDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl).EndInit();
			ultraTabControl.ResumeLayout(false);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxReligion).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLanguage).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxQualification).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPositionActual).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAgentThrough).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxArrivalPort).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPositionVisa).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridPayrollItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDeduction).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridDeduction).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBenefit).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridBenefit).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDestination).EndInit();
			ResumeLayout(false);
			PerformLayout();
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
			dataGridPayrollItem.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridPayrollItem.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			comboBoxPayrollItem.SelectedIndexChanged += comboBoxPayrollItem_SelectedIndexChanged;
			dataGridPayrollItem.AfterCellUpdate += dataGridPayrollItem_AfterCellUpdate;
			dataGridPayrollItem.HeaderClicked += dataGridPayrollItem_HeaderClicked;
			dataGridDeduction.HeaderClicked += dataGridDeduction_HeaderClicked;
			dataGridDeduction.BeforeCellDeactivate += dataGridDeduction_BeforeCellDeactivate;
			dataGridDeduction.BeforeRowDeactivate += dataGridDeduction_BeforeRowDeactivate;
			dataGridDeduction.CellDataError += dataGridDeduction_CellDataError;
			dataGridDeduction.AfterCellUpdate += dataGridDeduction_AfterCellUpdate;
			dataGridBenefit.HeaderClicked += dataGridBenefit_HeaderClicked;
			dataGridBenefit.BeforeCellDeactivate += dataGridBenefit_BeforeCellDeactivate;
			dataGridBenefit.BeforeRowDeactivate += dataGridBenefit_BeforeRowDeactivate;
			dataGridBenefit.CellDataError += dataGridBenefit_CellDataError;
			dataGridBenefit.AfterCellUpdate += dataGridBenefit_AfterCellUpdate;
			comboBoxDestination.SelectedIndexChanged += comboBoxDestination_SelectedIndexChanged;
		}

		private void comboBoxDestination_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDestination.SelectedID != "")
			{
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Destination", "TicketFixedAmount", "DestinationID", comboBoxDestination.SelectedID);
				textBoxTicketAmount.Value = decimal.Parse(fieldValue.ToString());
			}
		}

		private void comboBoxPayrollItem_SelectedIndexChanged(object sender, EventArgs e)
		{
			_ = dataGridPayrollItem.ActiveRow;
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

		private void dataGridBenefit_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (ultraGridColumn != null && ultraGridColumn.Key == "Benefit")
			{
				string id = "";
				if (dataGridBenefit.ActiveRow != null)
				{
					id = dataGridBenefit.ActiveRow.Cells["Benefit"].Text;
				}
				new FormHelper().EditBenefit(id);
			}
		}

		private void dataGridDeduction_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (ultraGridColumn != null && ultraGridColumn.Key == "Deduction")
			{
				string id = "";
				if (dataGridDeduction.ActiveRow != null)
				{
					id = dataGridDeduction.ActiveRow.Cells["Deduction"].Text;
				}
				new FormHelper().EditDeduction(id);
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

		private void dataGridBenefit_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Benefit")
			{
				dataGridBenefit.ActiveRow.Cells["Description"].Value = comboBoxBenefit.SelectedName;
			}
		}

		private void dataGridDeduction_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Deduction")
			{
				dataGridDeduction.ActiveRow.Cells["Description"].Value = comboBoxDeduction.SelectedName;
			}
		}

		private void dataGridBenefit_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridBenefit.ActiveCell.Column.Key.ToString() == "Benefit")
			{
				e.RaiseErrorEvent = false;
				comboBoxBenefit.Text = dataGridBenefit.ActiveCell.Text;
				comboBoxBenefit.QuickAddItem();
			}
			else if (dataGridBenefit.ActiveCell.Column.Key.ToString() == "Amount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please enter a numeric value greater or equal to zero.");
			}
		}

		private void dataGridDeduction_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridDeduction.ActiveCell.Column.Key.ToString() == "Deduction")
			{
				e.RaiseErrorEvent = false;
				comboBoxDeduction.Text = dataGridDeduction.ActiveCell.Text;
				comboBoxDeduction.QuickAddItem();
			}
			else if (dataGridDeduction.ActiveCell.Column.Key.ToString() == "Amount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please enter a numeric value greater or equal to zero.");
			}
		}

		private void dataGridBenefit_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridBenefit.ActiveRow;
			if (activeRow != null && activeRow.Cells["Benefit"].Value.ToString() == "")
			{
				ErrorHelper.InformationMessage("Please select an benefit.");
				e.Cancel = true;
				activeRow.Cells["Benefit"].Activate();
			}
		}

		private void dataGridDeduction_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridDeduction.ActiveRow;
			if (activeRow != null && activeRow.Cells["Deduction"].Value.ToString() == "")
			{
				ErrorHelper.InformationMessage("Please select a deduction.");
				e.Cancel = true;
				activeRow.Cells["Deduction"].Activate();
			}
		}

		private void dataGridBenefit_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridBenefit.ActiveCell.Column.Key == "Benefit")
			{
				foreach (UltraGridRow row in dataGridBenefit.Rows)
				{
					if (row.Index != dataGridBenefit.ActiveRow.Index && !(row.Cells["Benefit"].Value.ToString() == "") && row.Cells["Benefit"].Value.ToString() == dataGridBenefit.ActiveCell.Value.ToString())
					{
						ErrorHelper.InformationMessage("This benefit is already selected for this employee.");
						e.Cancel = true;
						return;
					}
				}
				if (dataGridBenefit.ActiveRow.Cells["Benefit"].Value.ToString() == "")
				{
					dataGridBenefit.ActiveRow.Cells["Description"].Value = "";
				}
			}
			else if (dataGridBenefit.ActiveCell.Column.Key == "Amount")
			{
				if (dataGridBenefit.ActiveCell.Text == "")
				{
					dataGridBenefit.ActiveCell.Value = 0.ToString(Format.TotalAmountFormat);
				}
				else
				{
					dataGridBenefit.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridBenefit.ActiveCell.Text, NumberStyles.Any), 2).ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void dataGridDeduction_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridDeduction.ActiveCell.Column.Key == "Deduction")
			{
				foreach (UltraGridRow row in dataGridDeduction.Rows)
				{
					if (row.Index != dataGridDeduction.ActiveRow.Index && !(row.Cells["Deduction"].Value.ToString() == "") && row.Cells["Deduction"].Value.ToString() == dataGridDeduction.ActiveCell.Value.ToString())
					{
						ErrorHelper.InformationMessage("This deduction is already selected for this employee.");
						e.Cancel = true;
						return;
					}
				}
				if (dataGridDeduction.ActiveRow.Cells["Deduction"].Value.ToString() == "")
				{
					dataGridDeduction.ActiveRow.Cells["Description"].Value = "";
				}
			}
			else if (dataGridDeduction.ActiveCell.Column.Key == "Amount")
			{
				if (dataGridDeduction.ActiveCell.Text == "")
				{
					dataGridDeduction.ActiveCell.Value = 0.ToString(Format.TotalAmountFormat);
				}
				else
				{
					dataGridDeduction.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridDeduction.ActiveCell.Text, NumberStyles.Any), 2).ToString(Format.TotalAmountFormat);
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
			case WorkflowType.WP_RP:
				panelWPRP.Enabled = true;
				udfEntryGrid.Enabled = true;
				break;
			case WorkflowType.Medical_Emirates:
				panelMedicalEmirates.Enabled = true;
				break;
			case WorkflowType.SalaryDetails:
				panelSalaryDetails.Enabled = true;
				break;
			}
			if ((int)workFlowType < ultraTabControl.Tabs.Count)
			{
				ultraTabControl.Tabs[(int)workFlowType].Selected = true;
			}
		}

		private void dateTimeArrivedOn_ValueChanged(object sender, EventArgs e)
		{
			if (dateTimeArrivedOn.Checked)
			{
				if (!isExist)
				{
					textBoxEmployeeNo.Text = GetNextEmployeeNumber();
				}
			}
			else if (!isExist)
			{
				textBoxEmployeeNo.Clear();
			}
		}

		private void dateTimeApplTypingDateEID_ValueChanged(object sender, EventArgs e)
		{
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
		}

		private void textBoxEmployeeNo_TextChanged(object sender, EventArgs e)
		{
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
				ultraTabControl.Tabs[8].VisibleIndex = 2;
				comboBoxGender.LoadData();
				comboBoxMaritalStatus.LoadData();
				comboBoxSelectionStatus.LoadData();
				comboBoxAgentThrough.ShowQuickAdd = true;
				comboBoxAgentThrough.LoadData();
				dataGridPayrollItem.SetupUI();
				dataGridDeduction.SetupUI();
				dataGridBenefit.SetupUI();
				SetupPayrollItemGrid();
				SetupDeductionGrid();
				SetupBenefitGrid();
				SetSecurity();
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
			checked
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.CandidateTable.Rows[0];
					textBoxCode.Text = "VS" + dataRow["CandidateID"].ToString();
					if (!string.IsNullOrEmpty(dataRow["ApplType"].ToString()))
					{
						comboBoxApplicationType.SelectedIndex = int.Parse(dataRow["ApplType"].ToString()) - 1;
					}
					else
					{
						comboBoxApplicationType.SelectedIndex = -1;
					}
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
					if (dataRow["ECRStatus"] != DBNull.Value)
					{
						comboBoxECRStatus.SelectedIndex = int.Parse(dataRow["ECRStatus"].ToString()) - 1;
					}
					else
					{
						comboBoxECRStatus.SelectedIndex = -1;
					}
					if (dataRow["SystemDate"] != DBNull.Value)
					{
						datetimePickerSystemdate.Value = DateTime.Parse(dataRow["SystemDate"].ToString());
						datetimePickerSystemdate.Checked = true;
					}
					else
					{
						datetimePickerSystemdate.Checked = false;
					}
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
					textBoxRemarks.Text = dataRow["Remarks"].ToString();
					comboBoxQualification.SelectedID = dataRow["QualificationID"].ToString();
					comboBoxLanguage.SelectedID = dataRow["LanguageID"].ToString();
					numericExperienceLocal.Text = dataRow["ExperienceLocal"].ToString();
					numericExperienceAbroad.Text = dataRow["ExperienceAbroad"].ToString();
					if (dataRow["DivisionID"] != DBNull.Value)
					{
						comboBoxDivision.SelectedID = dataRow["DivisionID"].ToString();
					}
					else
					{
						comboBoxDivision.SelectedID = "";
					}
					if (dataRow["AgreementStatus"] != DBNull.Value)
					{
						agreementStatusComboBox.Text = dataRow["AgreementStatus"].ToString();
					}
					else
					{
						agreementStatusComboBox.SelectedIndex = -1;
					}
					if (dataRow["SpecialCondition"] != DBNull.Value)
					{
						specialConditionComboBox.Text = dataRow["SpecialCondition"].ToString();
					}
					else
					{
						specialConditionComboBox.SelectedIndex = -1;
					}
					if (dataRow["AOTypingDate"] != DBNull.Value)
					{
						datetimepickerAOtypingDate.Value = DateTime.Parse(dataRow["AOTypingDate"].ToString());
						datetimepickerAOtypingDate.Checked = true;
					}
					else
					{
						datetimepickerAOtypingDate.Checked = false;
					}
					textBoxRegnNumber.Text = dataRow["AORegNumber"].ToString();
					if (dataRow["ApprovalStatusMOL"] != DBNull.Value)
					{
						comboBoxApprovalStatusMOL.SelectedIndex = int.Parse(dataRow["ApprovalStatusMOL"].ToString()) - 1;
					}
					else
					{
						comboBoxApprovalStatusMOL.SelectedIndex = -1;
					}
					textBoxMOLRemarks.Text = dataRow["MOLRemarks"].ToString();
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
					if (dataRow["ApprovalStatusIMG"] != DBNull.Value)
					{
						comboBoxApprovalStatusIMG.SelectedIndex = int.Parse(dataRow["ApprovalStatusIMG"].ToString()) - 1;
					}
					else
					{
						comboBoxApprovalStatusIMG.SelectedIndex = -1;
					}
					if (dataRow["ExpectedArrivaldate"] != DBNull.Value)
					{
						datetimepickerExpectedArrivalDate.Value = DateTime.Parse(dataRow["ExpectedArrivaldate"].ToString());
						datetimepickerExpectedArrivalDate.Checked = true;
					}
					else
					{
						datetimepickerExpectedArrivalDate.Checked = false;
					}
					textBoxIMGRemarks.Text = dataRow["IMGRemarks"].ToString();
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
					textBoxEmployeeNo.Text = dataRow["EmployeeNo"].ToString();
					IsExist = bool.Parse(dataRow["IsExist"].ToString());
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
					if (dataRow["SignedAGTrecvdDate"] != DBNull.Value)
					{
						dateTimeSignedAGTRecvd.Value = DateTime.Parse(dataRow["SignedAGTrecvdDate"].ToString());
						dateTimeSignedAGTRecvd.Checked = true;
					}
					else
					{
						dateTimeSignedAGTRecvd.Checked = false;
					}
					if (dataRow["SignedAOrecvdDate"] != DBNull.Value)
					{
						datetimeSignedAORcvd.Value = DateTime.Parse(dataRow["SignedAOrecvdDate"].ToString());
						datetimeSignedAORcvd.Checked = true;
					}
					else
					{
						datetimeSignedAORcvd.Checked = false;
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
					DataTable dataTable2 = dataGridDeduction.DataSource as DataTable;
					dataTable.Rows.Clear();
					dataTable2.Rows.Clear();
					foreach (DataRow row in currentData.Tables["Candidate_Salary"].Rows)
					{
						byte result = 0;
						byte.TryParse(row["PayType"].ToString(), out result);
						switch (result)
						{
						case 1:
						{
							DataRow dataRow4 = dataTable.NewRow();
							foreach (DataColumn column2 in dataRow4.Table.Columns)
							{
								_ = column2;
								dataRow4["PayrollItem"] = row["PayrollItemID"];
								dataRow4["Description"] = row["PayrollItemName"];
								if (row["Amount"] != DBNull.Value)
								{
									object obj3 = dataRow4["Amount"] = (dataRow4["New Amount"] = decimal.Round(decimal.Parse(row["Amount"].ToString()), 2));
								}
							}
							dataRow4.EndEdit();
							dataTable.Rows.Add(dataRow4);
							dataTable.AcceptChanges();
							break;
						}
						case 2:
						{
							DataRow dataRow3 = dataTable2.NewRow();
							foreach (DataColumn column3 in dataRow3.Table.Columns)
							{
								_ = column3;
								dataRow3["Deduction"] = row["PayrollItemID"];
								dataRow3["Description"] = row["PayrollItemName"];
								if (row["Amount"] != DBNull.Value)
								{
									object obj3 = dataRow3["Amount"] = (dataRow3["New Amount"] = decimal.Round(decimal.Parse(row["Amount"].ToString()), 2));
								}
							}
							dataRow3.EndEdit();
							dataTable2.Rows.Add(dataRow3);
							dataTable2.AcceptChanges();
							break;
						}
						}
					}
					DataTable dataTable3 = dataGridBenefit.DataSource as DataTable;
					dataTable3.Clear();
					foreach (DataRow row2 in currentData.Tables["Candidate_Benefit_Detail"].Rows)
					{
						DataRow dataRow6 = dataTable3.NewRow();
						foreach (DataColumn column4 in dataRow6.Table.Columns)
						{
							_ = column4;
							dataRow6["Benefit"] = row2["BenefitID"];
							dataRow6["Description"] = row2["BenefitName"];
							dataRow6["Start Date"] = row2["StartDate"];
							dataRow6["End Date"] = row2["EndDate"];
							dataRow6["Remarks"] = row2["Remarks"];
							if (row2["Amount"] != DBNull.Value)
							{
								object obj3 = dataRow6["Amount"] = (dataRow6["New Amount"] = decimal.Round(decimal.Parse(row2["Amount"].ToString()), 2));
							}
						}
						dataRow6.EndEdit();
						dataTable3.Rows.Add(dataRow6);
						dataTable3.AcceptChanges();
					}
					ShowTotalSalary();
				}
			}
		}

		private void FillAddressData(DataRow row)
		{
		}

		private bool GetData()
		{
			checked
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
					dataRow["ApplType"] = comboBoxApplicationType.SelectedIndex + 1;
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
					dataRow["FatherName"] = textBoxFatherName.Text.Trim();
					dataRow["MotherName"] = textBoxMotherName.Text.Trim();
					dataRow["SpouseName"] = textBoxSpouseName.Text.Trim();
					dataRow["PassportAddress"] = textBoxPPAddress.Text.Trim();
					dataRow["CandidateID"] = textBoxCode.Text.Trim().Substring(2);
					dataRow["ECRStatus"] = comboBoxECRStatus.SelectedIndex + 1;
					if (datetimePickerSystemdate.Checked)
					{
						dataRow["SystemDate"] = datetimePickerSystemdate.Value;
					}
					dataRow["Notes"] = textBoxNote.Text.Trim();
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
					dataRow["DivisionID"] = comboBoxDivision.SelectedID;
					if (comboBoxDivision.SelectedID != "")
					{
						dataRow["DivisionID"] = comboBoxDivision.SelectedID;
					}
					else
					{
						dataRow["DivisionID"] = DBNull.Value;
					}
					if (specialConditionComboBox.Text != "")
					{
						dataRow["SpecialCondition"] = specialConditionComboBox.Text;
					}
					else
					{
						dataRow["SpecialCondition"] = DBNull.Value;
					}
					if (agreementStatusComboBox.Text != "")
					{
						dataRow["AgreementStatus"] = agreementStatusComboBox.Text;
					}
					else
					{
						dataRow["AgreementStatus"] = DBNull.Value;
					}
					if (datetimepickerAOtypingDate.Checked)
					{
						dataRow["AOTypingDate"] = datetimepickerAOtypingDate.Value;
					}
					else
					{
						dataRow["AOTypingDate"] = DBNull.Value;
					}
					dataRow["AORegNumber"] = textBoxRegnNumber.Text;
					if (comboBoxApprovalStatusMOL.SelectedIndex != -1)
					{
						dataRow["ApprovalStatusMOL"] = comboBoxApprovalStatusMOL.SelectedIndex + 1;
					}
					dataRow["MOLRemarks"] = textBoxMOLRemarks.Text;
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
					if (comboBoxApprovalStatusIMG.SelectedIndex != -1)
					{
						dataRow["ApprovalStatusIMG"] = comboBoxApprovalStatusIMG.SelectedIndex + 1;
					}
					else
					{
						dataRow["ApprovalStatusIMG"] = DBNull.Value;
					}
					if (datetimepickerExpectedArrivalDate.Checked)
					{
						dataRow["ExpectedArrivaldate"] = datetimepickerExpectedArrivalDate.Value;
					}
					else
					{
						dataRow["ExpectedArrivaldate"] = DBNull.Value;
					}
					dataRow["IMGRemarks"] = textBoxIMGRemarks.Text;
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
					if (dateTimeSignedAGTRecvd.Checked)
					{
						dataRow["SignedAGTrecvdDate"] = dateTimeSignedAGTRecvd.Value;
					}
					else
					{
						dataRow["SignedAGTrecvdDate"] = DBNull.Value;
					}
					if (datetimeSignedAORcvd.Checked)
					{
						dataRow["SignedAOrecvdDate"] = datetimeSignedAORcvd.Value;
					}
					else
					{
						dataRow["SignedAOrecvdDate"] = DBNull.Value;
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
					if (dateTimeArrivedOn.Checked && !string.IsNullOrEmpty(textBoxEmployeeNo.Text) && !IsExist)
					{
						dataRow = currentData.EmployeeTable.NewRow();
						dataRow.BeginEdit();
						dataRow["EmployeeID"] = textBoxEmployeeNo.Text;
						dataRow["FirstName"] = textBoxGivenName.Text;
						dataRow["MiddleName"] = textBoxSurName.Text;
						dataRow["LastName"] = textBoxFatherName.Text;
						dataRow["NationalID"] = textBoxNationalID.Text;
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
						if (comboBoxDivision.SelectedID != "")
						{
							dataRow["DivisionID"] = comboBoxDivision.SelectedID;
						}
						else
						{
							dataRow["DivisionID"] = DBNull.Value;
						}
						if (comboBoxAgentThrough.SelectedID != "")
						{
							dataRow["RecruitmentChannelID"] = comboBoxAgentThrough.SelectedID;
						}
						else
						{
							dataRow["RecruitmentChannelID"] = DBNull.Value;
						}
						if (comboBoxPositionActual.SelectedID != "")
						{
							dataRow["VisaDesignationID"] = comboBoxPositionActual.SelectedID;
						}
						else
						{
							dataRow["VisaDesignationID"] = DBNull.Value;
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
						foreach (UltraGridRow row in dataGridPayrollItem.Rows)
						{
							if (!(row.Cells["PayrollItem"].Value.ToString() == ""))
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
								dataRow["PayrollItemID"] = row.Cells["PayrollItem"].Value.ToString();
								dataRow["Amount"] = decimal.Parse(row.Cells["Amount"].Value.ToString());
								dataRow["RowIndex"] = row.Index;
								dataRow.EndEdit();
								currentData.Tables["Employee_PayrollItem_Detail"].Rows.Add(dataRow);
							}
						}
						foreach (UltraGridRow row2 in dataGridDeduction.Rows)
						{
							if (!(row2.Cells["Deduction"].Value.ToString() == ""))
							{
								if (!currentData.Tables.Contains("Employee_PayrollItem_Detail"))
								{
									EmployeePayrollItemDetailData employeePayrollItemDetailData2 = new EmployeePayrollItemDetailData();
									currentData.Tables.Add(employeePayrollItemDetailData2.Tables[0].Clone());
								}
								dataRow = currentData.Tables["Employee_PayrollItem_Detail"].NewRow();
								dataRow.BeginEdit();
								dataRow["PayType"] = 2;
								dataRow["EmployeeID"] = textBoxEmployeeNo.Text;
								dataRow["PayrollItemID"] = row2.Cells["Deduction"].Value.ToString();
								dataRow["Amount"] = decimal.Parse(row2.Cells["Amount"].Value.ToString());
								dataRow["RowIndex"] = row2.Index;
								dataRow.EndEdit();
								currentData.Tables["Employee_PayrollItem_Detail"].Rows.Add(dataRow);
							}
						}
						currentData.EmployeeBenefit.Rows.Clear();
						foreach (UltraGridRow row3 in dataGridBenefit.Rows)
						{
							if (!(row3.Cells["Benefit"].Value.ToString() == ""))
							{
								if (!currentData.Tables.Contains("Employee_Benefit_Detail"))
								{
									EmployeeBenefitDetailData employeeBenefitDetailData = new EmployeeBenefitDetailData();
									currentData.Tables.Add(employeeBenefitDetailData.Tables[0].Clone());
								}
								dataRow = currentData.Tables["Employee_Benefit_Detail"].NewRow();
								dataRow.BeginEdit();
								dataRow["EmployeeID"] = textBoxEmployeeNo.Text;
								dataRow["BenefitID"] = row3.Cells["Benefit"].Value.ToString();
								if (string.IsNullOrEmpty(row3.Cells["Amount"].Value.ToString()))
								{
									row3.Cells["Amount"].Value = 0;
								}
								dataRow["Amount"] = decimal.Parse(row3.Cells["Amount"].Value.ToString());
								if (row3.Cells["Start Date"].Value.ToString() != "")
								{
									dataRow["StartDate"] = row3.Cells["Start Date"].Value.ToString();
								}
								else
								{
									dataRow["StartDate"] = DBNull.Value;
								}
								if (row3.Cells["End Date"].Value.ToString() != "")
								{
									dataRow["EndDate"] = row3.Cells["End Date"].Value.ToString();
								}
								else
								{
									dataRow["EndDate"] = DBNull.Value;
								}
								dataRow["Remarks"] = row3.Cells["Remarks"].Value.ToString();
								dataRow["RowIndex"] = row3.Index;
								dataRow.EndEdit();
								currentData.Tables["Employee_Benefit_Detail"].Rows.Add(dataRow);
							}
						}
					}
					currentData.CandidateSalaryTable.Rows.Clear();
					foreach (UltraGridRow row4 in dataGridPayrollItem.Rows)
					{
						if (!(row4.Cells["PayrollItem"].Value.ToString() == ""))
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
							dataRow["PayrollItemID"] = row4.Cells["PayrollItem"].Value.ToString();
							dataRow["Amount"] = decimal.Parse(row4.Cells["Amount"].Value.ToString());
							dataRow["RowIndex"] = row4.Index;
							dataRow.EndEdit();
							currentData.Tables["Candidate_Salary"].Rows.Add(dataRow);
						}
					}
					foreach (UltraGridRow row5 in dataGridDeduction.Rows)
					{
						if (!(row5.Cells["Deduction"].Value.ToString() == ""))
						{
							if (!currentData.Tables.Contains("Candidate_Salary"))
							{
								CandidateData candidateData2 = new CandidateData();
								currentData.Tables.Add(candidateData2.Tables[0].Clone());
							}
							dataRow = currentData.Tables["Candidate_Salary"].NewRow();
							dataRow.BeginEdit();
							dataRow["PayType"] = 2;
							dataRow["CandidateID"] = textBoxCode.Text.Trim().Substring(2);
							dataRow["PayrollItemID"] = row5.Cells["Deduction"].Value.ToString();
							dataRow["Amount"] = decimal.Parse(row5.Cells["Amount"].Value.ToString());
							dataRow["RowIndex"] = row5.Index;
							dataRow.EndEdit();
							currentData.Tables["Candidate_Salary"].Rows.Add(dataRow);
						}
					}
					currentData.CandidateBenefitDetailTable.Rows.Clear();
					foreach (UltraGridRow row6 in dataGridBenefit.Rows)
					{
						if (!(row6.Cells["Benefit"].Value.ToString() == ""))
						{
							if (!currentData.Tables.Contains("Candidate_Benefit_Detail"))
							{
								CandidateData candidateData3 = new CandidateData();
								currentData.Tables.Add(candidateData3.Tables[0].Clone());
							}
							dataRow = currentData.Tables["Candidate_Benefit_Detail"].NewRow();
							dataRow.BeginEdit();
							dataRow["CandidateID"] = textBoxCode.Text.Trim().Substring(2);
							dataRow["BenefitID"] = row6.Cells["Benefit"].Value.ToString();
							if (string.IsNullOrEmpty(row6.Cells["Amount"].Value.ToString()))
							{
								row6.Cells["Amount"].Value = 0;
							}
							dataRow["Amount"] = decimal.Parse(row6.Cells["Amount"].Value.ToString());
							if (row6.Cells["Start Date"].Value.ToString() != "")
							{
								dataRow["StartDate"] = row6.Cells["Start Date"].Value.ToString();
							}
							else
							{
								dataRow["StartDate"] = DBNull.Value;
							}
							if (row6.Cells["End Date"].Value.ToString() != "")
							{
								dataRow["EndDate"] = row6.Cells["End Date"].Value.ToString();
							}
							else
							{
								dataRow["EndDate"] = DBNull.Value;
							}
							dataRow["Remarks"] = row6.Cells["Remarks"].Value.ToString();
							dataRow["RowIndex"] = row6.Index;
							dataRow.EndEdit();
							currentData.Tables["Candidate_Benefit_Detail"].Rows.Add(dataRow);
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
				return Factory.SystemDocumentSystem.GetNextEmployeeNumber();
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
				if (textBoxCode.Text.Trim() == string.Empty || textBoxGivenName.Text.Trim() == string.Empty || textBoxSurName.Text.Trim() == string.Empty || textBoxPassportNo.Text.Trim() == string.Empty)
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
			comboBoxApplicationType.SelectedIndex = -1;
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
			udfEntryGrid.ClearData();
			pictureBoxPhoto.Image = null;
			comboBoxECRStatus.SelectedIndex = -1;
			datetimePickerSystemdate.Clear();
			datetimepickerAOtypingDate.Clear();
			textBoxRegnNumber.Clear();
			comboBoxApprovalStatusMOL.SelectedIndex = -1;
			textBoxMOLRemarks.Clear();
			comboBoxApprovalStatusIMG.SelectedIndex = -1;
			datetimepickerExpectedArrivalDate.Clear();
			textBoxIMGRemarks.Clear();
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
			datetimeSignedAORcvd.Checked = false;
			dateTimeSignedAGTRecvd.Checked = false;
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
			comboBoxDivision.Clear();
			specialConditionComboBox.SelectedIndex = -1;
			agreementStatusComboBox.SelectedIndex = -1;
			comboBoxAGTType.SelectedIndex = -1;
			textBoxAGTMBNo.Clear();
			comboBoxQualification.Clear();
			comboBoxLanguage.Clear();
			numericExperienceLocal.Text = 0m.ToString();
			numericExperienceAbroad.Text = 0m.ToString();
			textBoxEmployeeNo.Clear();
			textBoxPassportNo.Focus();
			comboBoxSponsor.Clear();
			textBoxHealthCardNo.Clear();
			formManager.ResetDirty();
			(dataGridPayrollItem.DataSource as DataTable).Rows.Clear();
			(dataGridDeduction.DataSource as DataTable).Rows.Clear();
			(dataGridBenefit.DataSource as DataTable).Rows.Clear();
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
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
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
					DataSet candidateProfileReport = Factory.CandidateSystem.GetCandidateProfileReport(textBoxCode.Text.Trim().Substring(2), textBoxCode.Text.Trim().Substring(2), "", "", "", "", showInactive: true);
					if (candidateProfileReport == null || candidateProfileReport.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(candidateProfileReport, "", "Candidate Profile", SysDocTypes.None, isPrint, showPrintDialog);
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
			new FormHelper().ShowList(DataComboType.Candidate);
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

		private void SetupDeductionGrid()
		{
			dataGridDeduction.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable("Deduction");
			dataTable.Columns.Add("Deduction", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("Start Date", typeof(DateTime));
			dataTable.Columns.Add("End Date", typeof(DateTime));
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataTable.Columns.Add("New Amount", typeof(decimal));
			dataGridDeduction.DataSource = dataTable;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].CharacterCasing = CharacterCasing.Upper;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].ValueList = comboBoxDeduction;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Start Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["End Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			UltraGridColumn ultraGridColumn = dataGridDeduction.DisplayLayout.Bands[0].Columns["Start Date"];
			bool hidden = dataGridDeduction.DisplayLayout.Bands[0].Columns["End Date"].Hidden = true;
			ultraGridColumn.Hidden = hidden;
			ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridDeduction.DisplayLayout.Bands[0].Columns["Start Date"].ExcludeFromColumnChooser = (dataGridDeduction.DisplayLayout.Bands[0].Columns["End Date"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].Width = checked(25 * dataGridDeduction.Width) / 100;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Description"].Width = checked(50 * dataGridDeduction.Width) / 100;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(15 * dataGridDeduction.Width) / 100;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].Width = checked(20 * dataGridDeduction.Width) / 100;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].MinValue = 0;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].Format = Format.GridAmountFormat;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].Hidden = true;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].Header.Appearance.Cursor = Cursors.Hand;
		}

		private void SetupBenefitGrid()
		{
			dataGridBenefit.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable("Benefit");
			dataTable.Columns.Add("Benefit", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("Start Date", typeof(DateTime));
			dataTable.Columns.Add("End Date", typeof(DateTime));
			dataTable.Columns.Add("Remarks", typeof(string));
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataTable.Columns.Add("New Amount", typeof(decimal));
			dataGridBenefit.DataSource = dataTable;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Benefit"].CharacterCasing = CharacterCasing.Upper;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 255;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Benefit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Benefit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Benefit"].ValueList = comboBoxBenefit;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Start Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["End Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Benefit"].Width = checked(25 * dataGridBenefit.Width) / 100;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Description"].Width = checked(50 * dataGridBenefit.Width) / 100;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(15 * dataGridBenefit.Width) / 100;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].Width = checked(20 * dataGridBenefit.Width) / 100;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].MinValue = 0;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].Format = Format.GridAmountFormat;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].Hidden = true;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Benefit"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Benefit"].Header.Appearance.Cursor = Cursors.Hand;
		}
	}
}
