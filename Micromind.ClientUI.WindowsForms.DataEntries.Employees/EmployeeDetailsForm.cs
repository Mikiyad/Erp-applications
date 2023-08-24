using Infragistics.Win;
using Infragistics.Win.AppStyling.Runtime;
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
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class EmployeeDetailsForm : Form, IForm
	{
		private EmployeeData currentData;

		private const string TABLENAME_CONST = "Employee";

		private const string IDFIELD_CONST = "EmployeeID";

		private bool isNewRecord = true;

		private bool EnableHRAnalysis = CompanyPreferences.EnableHRAnalysisCode;

		private DataSet companyInformation;

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

		private Panel panel1;

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

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private XPButton buttonProject;

		private ToolStripDropDownButton toolStripButton1;

		private ToolStripMenuItem dependentsToolStripMenuItem1;

		private ToolStripMenuItem documentsToolStripMenuItem1;

		private ToolStripMenuItem skillsToolStripMenuItem1;

		private OpenFileDialog openFileDialog1;

		private ToolStripButton toolStripButtonShowPicture;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripMenuItem salaryDetailsToolStripMenuItem;

		private AppStylistRuntime appStylistRuntime1;

		private UltraTabPageControl ultraTabPageControl1;

		private MMTextBox textBoxNote;

		private UltraTabPageControl tabPageUserDefined;

		private UDFEntryControl udfEntryGrid;

		private UltraTabPageControl tabPageDetails;

		private UltraGroupBox ultraGroupBox3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel13;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel12;

		private NumberTextBox textBoxFamilyMembersCount;

		private MMLabel mmLabel26;

		private MMTextBox textBoxInsuranceNumber;

		private MMLabel mmLabel21;

		private MMSDateTimePicker datetimePickerValidTo;

		private MMLabel mmLabel28;

		private MMSDateTimePicker dateTimePickerValidFrom;

		private MMLabel mmLabel27;

		private GenericListComboBox comboBoxInsuranceCategory;

		private MMTextBox textBoxInsuranceCategory;

		private MMTextBox textBoxMedicalInsuranceProvider;

		private MedicalInsuranceProviderComboBox comboBoxMedicalInsuarnceProvider;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel15;

		private MMTextBox textBoxAppraisalPoints;

		private MMLabel mmLabel4;

		private QualificationComboBox comboBoxQualification;

		private UltraGroupBox ultraGroupBox2;

		private MMLabel mmLabel2;

		private AllAccountsComboBox comboBoxAccount;

		private MMTextBox textBoxIBAN;

		private MMTextBox textBoxAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private BankComboBox comboBoxBank;

		private MMTextBox textBoxBankName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel10;

		private MMTextBox textBoxLabourID;

		private MMSDateTimePicker dateTimePickerConfirmation;

		private MMLabel mmLabel1;

		private MMTextBox textBoxSpouse;

		private MMLabel mmLabel48;

		private MMLabel mmLabel40;

		private MMTextBox textBoxBloodGroup;

		private ReligionComboBox comboBoxReligion;

		private MMLabel mmLabel38;

		private NumberTextBox textBoxProbation;

		private MMLabel mmLabel37;

		private DaysComboBox comboBoxDayOff;

		private MMLabel mmLabel32;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel9;

		private UltraTabPageControl tabPageGeneral;

		private MMLabel mmLabel19;

		private MMLabel mmLabel9;

		private MMLabel mmLabel3;

		private MMLabel labelEmployeeNumber;

		private PictureBox pictureBoxNoImage;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private UltraFormattedLinkLabel linkLoadImage;

		private PictureBox pictureBoxPhoto;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel11;

		private WorkLocationComboBox comboBoxLocation;

		private MMTextBox textBoxBirthPlace;

		private EmployeeTypeComboBox comboBoxType;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private MMTextBox textBoxAge;

		private MMLabel mmLabel33;

		private SponsorComboBox comboBoxSponsor;

		private MMLabel mmLabel31;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private MaritalStatusComboBox comboBoxMaritalStatus;

		private MMLabel mmLabel51;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private MMSDateTimePicker dateTimePickerBirthDate;

		private NationalityComboBox comboBoxNationality;

		private MMLabel mmLabel7;

		private UltraFormattedLinkLabel linkLabelCountry;

		private GenderComboBox comboBoxGender;

		private MMSDateTimePicker dateTimePickerJoiningDate;

		private MMLabel mmLabel50;

		private MMTextBox textBoxServicePeriod;

		private MMTextBox textBoxNationalID;

		private MMLabel mmLabel49;

		private EmployeeComboBox comboBoxManager;

		private PositionComboBox comboBoxPosition;

		private DepartmentComboBox comboBoxDepartment;

		private DivisionComboBox comboBoxDivision;

		private EmployeeStatusComboBox comboBoxStatus;

		private CheckBox checkBoxOnVacation;

		private GradeComboBox comboBoxGrade;

		private MMLabel mmLabel25;

		private EmployeeGroupComboBox comboBoxGroup;

		private MMLabel mmLabel5;

		private MMLabel lblDescriptions;

		private MMTextBox textBoxNickName;

		private MMTextBox textBoxCode;

		private MMTextBox textBoxLastName;

		private MMTextBox textBoxFirstName;

		private MMLabel mmLabel6;

		private MMLabel label1;

		private MMTextBox textBoxMiddleName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabControl ultraTabControl1;

		private UltraTabPageControl ultraTabPageControl2;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel23;

		private MMTextBox textBoxComment;

		private XPButton buttonMoreAddress;

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

		private HolidayCalendarComboBox comboBoxHolidayCalendar;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel14;

		private MMTextBox textBoxVisaNumber;

		private MMLabel mmLabel24;

		private MMTextBox textBoxUID;

		private MMLabel mmLabel22;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel16;

		private AnalysisComboBox comboBoxAnalysis;

		private PositionComboBox comboBoxVisaDesignation;

		private AgentComboBox comboBoxRecruitmentChannel;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelRcrtmnt;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelVisaDesig;

		private MMLabel mmLabel29;

		private AmountTextBox textBoxInsuranceAmount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelCompanyDivision;

		private CompanyDivisionComboBox comboBoxCompanyDivision;

		private System.Windows.Forms.ToolTip toolTip1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel17;

		private GenericListComboBox comboBoxLegalPosition;

		private IContainer components;

		private ScreenAccessRight screenRight;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5011;

		public ScreenTypes ScreenType => ScreenTypes.Card;

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
					textBoxCode.ReadOnly = false;
					textBoxAddressID.Enabled = false;
					UltraFormattedLinkLabel ultraFormattedLinkLabel = linkLoadImage;
					UltraFormattedLinkLabel ultraFormattedLinkLabel2 = linkRemovePicture;
					bool flag2 = linkAddPicture.Enabled = false;
					bool visible = ultraFormattedLinkLabel2.Enabled = flag2;
					ultraFormattedLinkLabel.Visible = visible;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					textBoxAddressID.Enabled = false;
					linkAddPicture.Enabled = true;
				}
				toolStripButtonAttach.Enabled = !value;
				buttonProject.Enabled = !value;
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

		public EmployeeDetailsForm()
		{
			InitializeComponent();
			Init();
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
			Infragistics.Win.Appearance appearance224 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance225 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance226 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance227 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance228 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance229 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance230 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance231 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance232 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance233 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance234 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance235 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance236 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance237 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance238 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance239 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance240 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance241 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance242 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance243 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance244 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance245 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance246 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance247 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance248 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance249 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance250 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance251 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance252 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance253 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance254 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance255 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance256 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance257 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance258 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance259 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance260 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance261 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance262 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance263 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance264 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance265 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance266 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance267 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance268 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance269 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance270 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance271 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance272 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance273 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance274 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance275 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance276 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeDetailsForm));
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance277 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraFormattedLinkLabelCompanyDivision = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCompanyDivision = new Micromind.DataControls.CompanyDivisionComboBox();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			labelEmployeeNumber = new Micromind.UISupport.MMLabel();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			ultraFormattedLinkLabel11 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxLocation = new Micromind.DataControls.WorkLocationComboBox();
			textBoxBirthPlace = new Micromind.UISupport.MMTextBox();
			comboBoxType = new Micromind.DataControls.EmployeeTypeComboBox();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxAge = new Micromind.UISupport.MMTextBox();
			mmLabel33 = new Micromind.UISupport.MMLabel();
			comboBoxSponsor = new Micromind.DataControls.SponsorComboBox();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxMaritalStatus = new Micromind.DataControls.MaritalStatusComboBox();
			mmLabel51 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			dateTimePickerBirthDate = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxNationality = new Micromind.DataControls.NationalityComboBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			linkLabelCountry = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxGender = new Micromind.DataControls.GenderComboBox();
			dateTimePickerJoiningDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel50 = new Micromind.UISupport.MMLabel();
			textBoxServicePeriod = new Micromind.UISupport.MMTextBox();
			textBoxNationalID = new Micromind.UISupport.MMTextBox();
			mmLabel49 = new Micromind.UISupport.MMLabel();
			comboBoxManager = new Micromind.DataControls.EmployeeComboBox();
			comboBoxPosition = new Micromind.DataControls.PositionComboBox();
			comboBoxDepartment = new Micromind.DataControls.DepartmentComboBox();
			comboBoxDivision = new Micromind.DataControls.DivisionComboBox();
			comboBoxStatus = new Micromind.DataControls.EmployeeStatusComboBox();
			checkBoxOnVacation = new System.Windows.Forms.CheckBox();
			comboBoxGrade = new Micromind.DataControls.GradeComboBox();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			comboBoxGroup = new Micromind.DataControls.EmployeeGroupComboBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxNickName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxLastName = new Micromind.UISupport.MMTextBox();
			textBoxFirstName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			label1 = new Micromind.UISupport.MMLabel();
			textBoxMiddleName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraFormattedLinkLabel17 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxLegalPosition = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabelVisaDesig = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabelRcrtmnt = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxVisaDesignation = new Micromind.DataControls.PositionComboBox();
			comboBoxRecruitmentChannel = new Micromind.DataControls.AgentComboBox();
			ultraFormattedLinkLabel14 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxHolidayCalendar = new Micromind.DataControls.HolidayCalendarComboBox();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxInsuranceAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel13 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel12 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxFamilyMembersCount = new Micromind.UISupport.NumberTextBox();
			mmLabel26 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceNumber = new Micromind.UISupport.MMTextBox();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			datetimePickerValidTo = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel28 = new Micromind.UISupport.MMLabel();
			dateTimePickerValidFrom = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel27 = new Micromind.UISupport.MMLabel();
			comboBoxInsuranceCategory = new Micromind.DataControls.GenericListComboBox();
			textBoxInsuranceCategory = new Micromind.UISupport.MMTextBox();
			textBoxMedicalInsuranceProvider = new Micromind.UISupport.MMTextBox();
			comboBoxMedicalInsuarnceProvider = new Micromind.DataControls.MedicalInsuranceProviderComboBox();
			ultraFormattedLinkLabel15 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxAppraisalPoints = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			comboBoxQualification = new Micromind.DataControls.QualificationComboBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel16 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxAnalysis = new Micromind.DataControls.AnalysisComboBox();
			textBoxVisaNumber = new Micromind.UISupport.MMTextBox();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			textBoxUID = new Micromind.UISupport.MMTextBox();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxIBAN = new Micromind.UISupport.MMTextBox();
			textBoxAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxBank = new Micromind.DataControls.BankComboBox();
			textBoxBankName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel10 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxLabourID = new Micromind.UISupport.MMTextBox();
			dateTimePickerConfirmation = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxSpouse = new Micromind.UISupport.MMTextBox();
			mmLabel48 = new Micromind.UISupport.MMLabel();
			mmLabel40 = new Micromind.UISupport.MMLabel();
			textBoxBloodGroup = new Micromind.UISupport.MMTextBox();
			comboBoxReligion = new Micromind.DataControls.ReligionComboBox();
			mmLabel38 = new Micromind.UISupport.MMLabel();
			textBoxProbation = new Micromind.UISupport.NumberTextBox();
			mmLabel37 = new Micromind.UISupport.MMLabel();
			comboBoxDayOff = new Micromind.DataControls.DaysComboBox();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel9 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			textBoxComment = new Micromind.UISupport.MMTextBox();
			buttonMoreAddress = new Micromind.UISupport.XPButton();
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
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
			panelButtons = new System.Windows.Forms.Panel();
			buttonProject = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonClose = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
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
			toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			dependentsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			documentsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			skillsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			salaryDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonShowPicture = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panel1 = new System.Windows.Forms.Panel();
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			dependentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			documentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			skillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			appStylistRuntime1 = new Infragistics.Win.AppStyling.Runtime.AppStylistRuntime(components);
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			formManager = new Micromind.DataControls.FormManager();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCompanyDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManager).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPosition).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepartment).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGrade).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGroup).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLegalPosition).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVisaDesignation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRecruitmentChannel).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxHolidayCalendar).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxMedicalInsuarnceProvider).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxQualification).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReligion).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			panel1.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabelCompanyDivision);
			tabPageGeneral.Controls.Add(comboBoxCompanyDivision);
			tabPageGeneral.Controls.Add(mmLabel19);
			tabPageGeneral.Controls.Add(mmLabel9);
			tabPageGeneral.Controls.Add(mmLabel3);
			tabPageGeneral.Controls.Add(labelEmployeeNumber);
			tabPageGeneral.Controls.Add(pictureBoxNoImage);
			tabPageGeneral.Controls.Add(linkRemovePicture);
			tabPageGeneral.Controls.Add(linkAddPicture);
			tabPageGeneral.Controls.Add(linkLoadImage);
			tabPageGeneral.Controls.Add(pictureBoxPhoto);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel11);
			tabPageGeneral.Controls.Add(comboBoxLocation);
			tabPageGeneral.Controls.Add(textBoxBirthPlace);
			tabPageGeneral.Controls.Add(comboBoxType);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel7);
			tabPageGeneral.Controls.Add(textBoxAge);
			tabPageGeneral.Controls.Add(mmLabel33);
			tabPageGeneral.Controls.Add(comboBoxSponsor);
			tabPageGeneral.Controls.Add(mmLabel31);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel8);
			tabPageGeneral.Controls.Add(comboBoxMaritalStatus);
			tabPageGeneral.Controls.Add(mmLabel51);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel2);
			tabPageGeneral.Controls.Add(dateTimePickerBirthDate);
			tabPageGeneral.Controls.Add(comboBoxNationality);
			tabPageGeneral.Controls.Add(mmLabel7);
			tabPageGeneral.Controls.Add(linkLabelCountry);
			tabPageGeneral.Controls.Add(comboBoxGender);
			tabPageGeneral.Controls.Add(dateTimePickerJoiningDate);
			tabPageGeneral.Controls.Add(mmLabel50);
			tabPageGeneral.Controls.Add(textBoxServicePeriod);
			tabPageGeneral.Controls.Add(textBoxNationalID);
			tabPageGeneral.Controls.Add(mmLabel49);
			tabPageGeneral.Controls.Add(comboBoxManager);
			tabPageGeneral.Controls.Add(comboBoxPosition);
			tabPageGeneral.Controls.Add(comboBoxDepartment);
			tabPageGeneral.Controls.Add(comboBoxDivision);
			tabPageGeneral.Controls.Add(comboBoxStatus);
			tabPageGeneral.Controls.Add(checkBoxOnVacation);
			tabPageGeneral.Controls.Add(comboBoxGrade);
			tabPageGeneral.Controls.Add(mmLabel25);
			tabPageGeneral.Controls.Add(comboBoxGroup);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(lblDescriptions);
			tabPageGeneral.Controls.Add(textBoxNickName);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(textBoxLastName);
			tabPageGeneral.Controls.Add(textBoxFirstName);
			tabPageGeneral.Controls.Add(mmLabel6);
			tabPageGeneral.Controls.Add(label1);
			tabPageGeneral.Controls.Add(textBoxMiddleName);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel1);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel3);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel4);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel5);
			tabPageGeneral.Location = new System.Drawing.Point(-10000, -10000);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(696, 420);
			ultraFormattedLinkLabelCompanyDivision.AutoSize = true;
			ultraFormattedLinkLabelCompanyDivision.Location = new System.Drawing.Point(9, 232);
			ultraFormattedLinkLabelCompanyDivision.Name = "ultraFormattedLinkLabelCompanyDivision";
			ultraFormattedLinkLabelCompanyDivision.Size = new System.Drawing.Size(79, 14);
			ultraFormattedLinkLabelCompanyDivision.TabIndex = 129;
			ultraFormattedLinkLabelCompanyDivision.TabStop = true;
			toolTip1.SetToolTip(ultraFormattedLinkLabelCompanyDivision, "Company Division");
			ultraFormattedLinkLabelCompanyDivision.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelCompanyDivision.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelCompanyDivision.Value = "Comp.Division:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelCompanyDivision.VisitedLinkAppearance = appearance;
			ultraFormattedLinkLabelCompanyDivision.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelCompanyDivision_LinkClicked);
			comboBoxCompanyDivision.Assigned = false;
			comboBoxCompanyDivision.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCompanyDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCompanyDivision.CustomReportFieldName = "";
			comboBoxCompanyDivision.CustomReportKey = "";
			comboBoxCompanyDivision.CustomReportValueType = 1;
			comboBoxCompanyDivision.DescriptionTextBox = null;
			appearance2.BackColor = System.Drawing.SystemColors.Window;
			appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCompanyDivision.DisplayLayout.Appearance = appearance2;
			comboBoxCompanyDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCompanyDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance3.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.Appearance = appearance3;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance5.BackColor2 = System.Drawing.SystemColors.Control;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCompanyDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
			comboBoxCompanyDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCompanyDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCompanyDivision.DisplayLayout.Override.ActiveCellAppearance = appearance6;
			appearance7.BackColor = System.Drawing.SystemColors.Highlight;
			appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCompanyDivision.DisplayLayout.Override.ActiveRowAppearance = appearance7;
			comboBoxCompanyDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCompanyDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.Override.CardAreaAppearance = appearance8;
			appearance9.BorderColor = System.Drawing.Color.Silver;
			appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCompanyDivision.DisplayLayout.Override.CellAppearance = appearance9;
			comboBoxCompanyDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCompanyDivision.DisplayLayout.Override.CellPadding = 0;
			appearance10.BackColor = System.Drawing.SystemColors.Control;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCompanyDivision.DisplayLayout.Override.GroupByRowAppearance = appearance10;
			appearance11.TextHAlignAsString = "Left";
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderAppearance = appearance11;
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCompanyDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			comboBoxCompanyDivision.DisplayLayout.Override.RowAppearance = appearance12;
			comboBoxCompanyDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCompanyDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
			comboBoxCompanyDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCompanyDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCompanyDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCompanyDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCompanyDivision.Editable = true;
			comboBoxCompanyDivision.FilterString = "";
			comboBoxCompanyDivision.HasAllAccount = false;
			comboBoxCompanyDivision.HasCustom = false;
			comboBoxCompanyDivision.IsDataLoaded = false;
			comboBoxCompanyDivision.Location = new System.Drawing.Point(132, 229);
			comboBoxCompanyDivision.MaxDropDownItems = 12;
			comboBoxCompanyDivision.Name = "comboBoxCompanyDivision";
			comboBoxCompanyDivision.ShowInactiveItems = false;
			comboBoxCompanyDivision.ShowQuickAdd = true;
			comboBoxCompanyDivision.Size = new System.Drawing.Size(151, 20);
			comboBoxCompanyDivision.TabIndex = 13;
			comboBoxCompanyDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(442, 270);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(77, 13);
			mmLabel19.TabIndex = 74;
			mmLabel19.Text = "Marital Status:";
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(299, 12);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(42, 13);
			mmLabel9.TabIndex = 73;
			mmLabel9.Text = "Status:";
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(292, 210);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(53, 13);
			mmLabel3.TabIndex = 72;
			mmLabel3.Text = "Manager:";
			labelEmployeeNumber.AutoSize = true;
			labelEmployeeNumber.BackColor = System.Drawing.Color.Transparent;
			labelEmployeeNumber.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelEmployeeNumber.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelEmployeeNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelEmployeeNumber.IsFieldHeader = false;
			labelEmployeeNumber.IsRequired = false;
			labelEmployeeNumber.Location = new System.Drawing.Point(9, 9);
			labelEmployeeNumber.Name = "labelEmployeeNumber";
			labelEmployeeNumber.PenWidth = 1f;
			labelEmployeeNumber.ShowBorder = false;
			labelEmployeeNumber.Size = new System.Drawing.Size(96, 13);
			labelEmployeeNumber.TabIndex = 68;
			labelEmployeeNumber.Text = "Employee Code:";
			labelEmployeeNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(623, 143);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 67;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(572, 143);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 19;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance14;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(533, 143);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 18;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance15.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance15;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(558, 59);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 66;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance16;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(529, 11);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(128, 128);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 65;
			pictureBoxPhoto.TabStop = false;
			ultraFormattedLinkLabel11.AutoSize = true;
			ultraFormattedLinkLabel11.Location = new System.Drawing.Point(295, 122);
			ultraFormattedLinkLabel11.Name = "ultraFormattedLinkLabel11";
			ultraFormattedLinkLabel11.Size = new System.Drawing.Size(35, 14);
			ultraFormattedLinkLabel11.TabIndex = 64;
			ultraFormattedLinkLabel11.TabStop = true;
			ultraFormattedLinkLabel11.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel11.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel11.Value = "Class:";
			appearance17.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel11.VisitedLinkAppearance = appearance17;
			ultraFormattedLinkLabel11.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel11_LinkClicked);
			comboBoxLocation.Assigned = false;
			comboBoxLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance18;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance19.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance19.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance19.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance19;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance20;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance21.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance21.BackColor2 = System.Drawing.SystemColors.Control;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance21;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance22.BackColor = System.Drawing.SystemColors.Window;
			appearance22.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance22;
			appearance23.BackColor = System.Drawing.SystemColors.Highlight;
			appearance23.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance23;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance24;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			appearance25.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance25;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance26.BackColor = System.Drawing.SystemColors.Control;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance26;
			appearance27.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance27;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			appearance28.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance28;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance29.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance29;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(132, 141);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowAll = false;
			comboBoxLocation.ShowConsignIn = false;
			comboBoxLocation.ShowConsignOut = false;
			comboBoxLocation.ShowInactiveItems = false;
			comboBoxLocation.ShowNormalLocations = true;
			comboBoxLocation.ShowPOSOnly = false;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.ShowWarehouseOnly = false;
			comboBoxLocation.Size = new System.Drawing.Size(151, 20);
			comboBoxLocation.TabIndex = 9;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxBirthPlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxBirthPlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxBirthPlace.BackColor = System.Drawing.Color.White;
			textBoxBirthPlace.CustomReportFieldName = "";
			textBoxBirthPlace.CustomReportKey = "";
			textBoxBirthPlace.CustomReportValueType = 1;
			textBoxBirthPlace.IsComboTextBox = false;
			textBoxBirthPlace.IsModified = false;
			textBoxBirthPlace.Location = new System.Drawing.Point(371, 244);
			textBoxBirthPlace.MaxLength = 30;
			textBoxBirthPlace.Name = "textBoxBirthPlace";
			textBoxBirthPlace.Size = new System.Drawing.Size(146, 20);
			textBoxBirthPlace.TabIndex = 21;
			comboBoxType.Assigned = false;
			comboBoxType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxType.CustomReportFieldName = "";
			comboBoxType.CustomReportKey = "";
			comboBoxType.CustomReportValueType = 1;
			comboBoxType.DescriptionTextBox = null;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			appearance30.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxType.DisplayLayout.Appearance = appearance30;
			comboBoxType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance31.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance31.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance31.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.GroupByBox.Appearance = appearance31;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance32;
			comboBoxType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance33.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance33.BackColor2 = System.Drawing.SystemColors.Control;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxType.DisplayLayout.GroupByBox.PromptAppearance = appearance33;
			comboBoxType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			appearance34.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxType.DisplayLayout.Override.ActiveCellAppearance = appearance34;
			appearance35.BackColor = System.Drawing.SystemColors.Highlight;
			appearance35.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxType.DisplayLayout.Override.ActiveRowAppearance = appearance35;
			comboBoxType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.Override.CardAreaAppearance = appearance36;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			appearance37.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxType.DisplayLayout.Override.CellAppearance = appearance37;
			comboBoxType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxType.DisplayLayout.Override.CellPadding = 0;
			appearance38.BackColor = System.Drawing.SystemColors.Control;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.Override.GroupByRowAppearance = appearance38;
			appearance39.TextHAlignAsString = "Left";
			comboBoxType.DisplayLayout.Override.HeaderAppearance = appearance39;
			comboBoxType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			appearance40.BorderColor = System.Drawing.Color.Silver;
			comboBoxType.DisplayLayout.Override.RowAppearance = appearance40;
			comboBoxType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance41.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxType.DisplayLayout.Override.TemplateAddRowAppearance = appearance41;
			comboBoxType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxType.Editable = true;
			comboBoxType.FilterString = "";
			comboBoxType.HasAllAccount = false;
			comboBoxType.HasCustom = false;
			comboBoxType.IsDataLoaded = false;
			comboBoxType.Location = new System.Drawing.Point(368, 120);
			comboBoxType.MaxDropDownItems = 12;
			comboBoxType.Name = "comboBoxType";
			comboBoxType.ShowInactiveItems = false;
			comboBoxType.ShowQuickAdd = true;
			comboBoxType.Size = new System.Drawing.Size(149, 20);
			comboBoxType.TabIndex = 14;
			comboBoxType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(9, 210);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(48, 14);
			ultraFormattedLinkLabel7.TabIndex = 58;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Sponsor:";
			appearance42.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance42;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			textBoxAge.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxAge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxAge.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAge.CustomReportFieldName = "";
			textBoxAge.CustomReportKey = "";
			textBoxAge.CustomReportValueType = 1;
			textBoxAge.IsComboTextBox = false;
			textBoxAge.IsModified = false;
			textBoxAge.Location = new System.Drawing.Point(371, 265);
			textBoxAge.MaxLength = 30;
			textBoxAge.Name = "textBoxAge";
			textBoxAge.ReadOnly = true;
			textBoxAge.Size = new System.Drawing.Size(68, 20);
			textBoxAge.TabIndex = 24;
			textBoxAge.TabStop = false;
			mmLabel33.AutoSize = true;
			mmLabel33.BackColor = System.Drawing.Color.Transparent;
			mmLabel33.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel33.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel33.IsFieldHeader = false;
			mmLabel33.IsRequired = false;
			mmLabel33.Location = new System.Drawing.Point(290, 247);
			mmLabel33.Name = "mmLabel33";
			mmLabel33.PenWidth = 1f;
			mmLabel33.ShowBorder = false;
			mmLabel33.Size = new System.Drawing.Size(61, 13);
			mmLabel33.TabIndex = 30;
			mmLabel33.Text = "Birth Place:";
			comboBoxSponsor.Assigned = false;
			comboBoxSponsor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSponsor.CustomReportFieldName = "";
			comboBoxSponsor.CustomReportKey = "";
			comboBoxSponsor.CustomReportValueType = 1;
			comboBoxSponsor.DescriptionTextBox = null;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSponsor.DisplayLayout.Appearance = appearance43;
			comboBoxSponsor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSponsor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance44.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance44.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.GroupByBox.Appearance = appearance44;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance45;
			comboBoxSponsor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance46.BackColor2 = System.Drawing.SystemColors.Control;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.PromptAppearance = appearance46;
			comboBoxSponsor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSponsor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSponsor.DisplayLayout.Override.ActiveCellAppearance = appearance47;
			appearance48.BackColor = System.Drawing.SystemColors.Highlight;
			appearance48.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSponsor.DisplayLayout.Override.ActiveRowAppearance = appearance48;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.CardAreaAppearance = appearance49;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSponsor.DisplayLayout.Override.CellAppearance = appearance50;
			comboBoxSponsor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSponsor.DisplayLayout.Override.CellPadding = 0;
			appearance51.BackColor = System.Drawing.SystemColors.Control;
			appearance51.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance51.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance51.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.GroupByRowAppearance = appearance51;
			appearance52.TextHAlignAsString = "Left";
			comboBoxSponsor.DisplayLayout.Override.HeaderAppearance = appearance52;
			comboBoxSponsor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSponsor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			comboBoxSponsor.DisplayLayout.Override.RowAppearance = appearance53;
			comboBoxSponsor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSponsor.DisplayLayout.Override.TemplateAddRowAppearance = appearance54;
			comboBoxSponsor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSponsor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSponsor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSponsor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSponsor.Editable = true;
			comboBoxSponsor.FilterString = "";
			comboBoxSponsor.HasAllAccount = false;
			comboBoxSponsor.HasCustom = false;
			comboBoxSponsor.IsDataLoaded = false;
			comboBoxSponsor.Location = new System.Drawing.Point(132, 207);
			comboBoxSponsor.MaxDropDownItems = 12;
			comboBoxSponsor.Name = "comboBoxSponsor";
			comboBoxSponsor.ShowInactiveItems = false;
			comboBoxSponsor.ShowQuickAdd = true;
			comboBoxSponsor.Size = new System.Drawing.Size(151, 20);
			comboBoxSponsor.TabIndex = 12;
			comboBoxSponsor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(9, 277);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(59, 13);
			mmLabel31.TabIndex = 29;
			mmLabel31.Text = "Birth Date:";
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(9, 254);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel8.TabIndex = 59;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Nationality:";
			appearance55.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance55;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			comboBoxMaritalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMaritalStatus.FormattingEnabled = true;
			comboBoxMaritalStatus.Location = new System.Drawing.Point(528, 266);
			comboBoxMaritalStatus.Name = "comboBoxMaritalStatus";
			comboBoxMaritalStatus.SelectedID = 0;
			comboBoxMaritalStatus.Size = new System.Drawing.Size(130, 21);
			comboBoxMaritalStatus.TabIndex = 25;
			mmLabel51.AutoSize = true;
			mmLabel51.BackColor = System.Drawing.Color.Transparent;
			mmLabel51.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel51.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel51.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel51.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel51.IsFieldHeader = false;
			mmLabel51.IsRequired = false;
			mmLabel51.Location = new System.Drawing.Point(290, 268);
			mmLabel51.Name = "mmLabel51";
			mmLabel51.PenWidth = 1f;
			mmLabel51.ShowBorder = false;
			mmLabel51.Size = new System.Drawing.Size(30, 13);
			mmLabel51.TabIndex = 57;
			mmLabel51.Text = "Age:";
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(9, 185);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(64, 14);
			ultraFormattedLinkLabel2.TabIndex = 58;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Department:";
			appearance56.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance56;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			dateTimePickerBirthDate.Checked = false;
			dateTimePickerBirthDate.CustomFormat = " ";
			dateTimePickerBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerBirthDate.Location = new System.Drawing.Point(132, 273);
			dateTimePickerBirthDate.Name = "dateTimePickerBirthDate";
			dateTimePickerBirthDate.ShowCheckBox = true;
			dateTimePickerBirthDate.Size = new System.Drawing.Size(151, 20);
			dateTimePickerBirthDate.TabIndex = 23;
			dateTimePickerBirthDate.Value = new System.DateTime(0L);
			dateTimePickerBirthDate.ValueChanged += new System.EventHandler(dateTimePickerBirthDate_ValueChanged);
			comboBoxNationality.Assigned = false;
			comboBoxNationality.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxNationality.CustomReportFieldName = "";
			comboBoxNationality.CustomReportKey = "";
			comboBoxNationality.CustomReportValueType = 1;
			comboBoxNationality.DescriptionTextBox = null;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxNationality.DisplayLayout.Appearance = appearance57;
			comboBoxNationality.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxNationality.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance58.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance58.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance58.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.GroupByBox.Appearance = appearance58;
			appearance59.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.BandLabelAppearance = appearance59;
			comboBoxNationality.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance60.BackColor2 = System.Drawing.SystemColors.Control;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.PromptAppearance = appearance60;
			comboBoxNationality.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxNationality.DisplayLayout.MaxRowScrollRegions = 1;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxNationality.DisplayLayout.Override.ActiveCellAppearance = appearance61;
			appearance62.BackColor = System.Drawing.SystemColors.Highlight;
			appearance62.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxNationality.DisplayLayout.Override.ActiveRowAppearance = appearance62;
			comboBoxNationality.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxNationality.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.CardAreaAppearance = appearance63;
			appearance64.BorderColor = System.Drawing.Color.Silver;
			appearance64.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxNationality.DisplayLayout.Override.CellAppearance = appearance64;
			comboBoxNationality.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxNationality.DisplayLayout.Override.CellPadding = 0;
			appearance65.BackColor = System.Drawing.SystemColors.Control;
			appearance65.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance65.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance65.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.GroupByRowAppearance = appearance65;
			appearance66.TextHAlignAsString = "Left";
			comboBoxNationality.DisplayLayout.Override.HeaderAppearance = appearance66;
			comboBoxNationality.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxNationality.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.Color.Silver;
			comboBoxNationality.DisplayLayout.Override.RowAppearance = appearance67;
			comboBoxNationality.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxNationality.DisplayLayout.Override.TemplateAddRowAppearance = appearance68;
			comboBoxNationality.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxNationality.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxNationality.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxNationality.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxNationality.Editable = true;
			comboBoxNationality.FilterString = "";
			comboBoxNationality.HasAllAccount = false;
			comboBoxNationality.HasCustom = false;
			comboBoxNationality.IsDataLoaded = false;
			comboBoxNationality.Location = new System.Drawing.Point(132, 251);
			comboBoxNationality.MaxDropDownItems = 12;
			comboBoxNationality.Name = "comboBoxNationality";
			comboBoxNationality.ShowInactiveItems = false;
			comboBoxNationality.ShowQuickAdd = true;
			comboBoxNationality.Size = new System.Drawing.Size(151, 20);
			comboBoxNationality.TabIndex = 20;
			comboBoxNationality.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(525, 247);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(46, 13);
			mmLabel7.TabIndex = 0;
			mmLabel7.Text = "Gender:";
			linkLabelCountry.AutoSize = true;
			linkLabelCountry.Location = new System.Drawing.Point(9, 142);
			linkLabelCountry.Name = "linkLabelCountry";
			linkLabelCountry.Size = new System.Drawing.Size(49, 14);
			linkLabelCountry.TabIndex = 56;
			linkLabelCountry.TabStop = true;
			linkLabelCountry.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCountry.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCountry.Value = "Location:";
			appearance69.ForeColor = System.Drawing.Color.Blue;
			linkLabelCountry.VisitedLinkAppearance = appearance69;
			linkLabelCountry.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCountry_LinkClicked);
			comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxGender.FormattingEnabled = true;
			comboBoxGender.Location = new System.Drawing.Point(577, 243);
			comboBoxGender.Name = "comboBoxGender";
			comboBoxGender.SelectedID = 0;
			comboBoxGender.Size = new System.Drawing.Size(81, 21);
			comboBoxGender.TabIndex = 22;
			dateTimePickerJoiningDate.Checked = false;
			dateTimePickerJoiningDate.CustomFormat = " ";
			dateTimePickerJoiningDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerJoiningDate.Location = new System.Drawing.Point(132, 295);
			dateTimePickerJoiningDate.Name = "dateTimePickerJoiningDate";
			dateTimePickerJoiningDate.ShowCheckBox = true;
			dateTimePickerJoiningDate.Size = new System.Drawing.Size(151, 20);
			dateTimePickerJoiningDate.TabIndex = 26;
			dateTimePickerJoiningDate.Value = new System.DateTime(0L);
			dateTimePickerJoiningDate.ValueChanged += new System.EventHandler(dateTimePickerJoiningDate_ValueChanged);
			mmLabel50.AutoSize = true;
			mmLabel50.BackColor = System.Drawing.Color.Transparent;
			mmLabel50.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel50.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel50.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel50.IsFieldHeader = false;
			mmLabel50.IsRequired = false;
			mmLabel50.Location = new System.Drawing.Point(290, 288);
			mmLabel50.Name = "mmLabel50";
			mmLabel50.PenWidth = 1f;
			mmLabel50.ShowBorder = false;
			mmLabel50.Size = new System.Drawing.Size(79, 13);
			mmLabel50.TabIndex = 55;
			mmLabel50.Text = "Service Period:";
			textBoxServicePeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxServicePeriod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxServicePeriod.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxServicePeriod.CustomReportFieldName = "";
			textBoxServicePeriod.CustomReportKey = "";
			textBoxServicePeriod.CustomReportValueType = 1;
			textBoxServicePeriod.IsComboTextBox = false;
			textBoxServicePeriod.IsModified = false;
			textBoxServicePeriod.Location = new System.Drawing.Point(371, 286);
			textBoxServicePeriod.MaxLength = 30;
			textBoxServicePeriod.Name = "textBoxServicePeriod";
			textBoxServicePeriod.ReadOnly = true;
			textBoxServicePeriod.Size = new System.Drawing.Size(148, 20);
			textBoxServicePeriod.TabIndex = 27;
			textBoxServicePeriod.TabStop = false;
			textBoxNationalID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxNationalID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxNationalID.BackColor = System.Drawing.Color.White;
			textBoxNationalID.CustomReportFieldName = "";
			textBoxNationalID.CustomReportKey = "";
			textBoxNationalID.CustomReportValueType = 1;
			textBoxNationalID.IsComboTextBox = false;
			textBoxNationalID.IsModified = false;
			textBoxNationalID.Location = new System.Drawing.Point(132, 119);
			textBoxNationalID.MaxLength = 30;
			textBoxNationalID.Name = "textBoxNationalID";
			textBoxNationalID.Size = new System.Drawing.Size(151, 20);
			textBoxNationalID.TabIndex = 8;
			mmLabel49.AutoSize = true;
			mmLabel49.BackColor = System.Drawing.Color.Transparent;
			mmLabel49.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel49.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel49.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel49.IsFieldHeader = false;
			mmLabel49.IsRequired = false;
			mmLabel49.Location = new System.Drawing.Point(9, 121);
			mmLabel49.Name = "mmLabel49";
			mmLabel49.PenWidth = 1f;
			mmLabel49.ShowBorder = false;
			mmLabel49.Size = new System.Drawing.Size(64, 13);
			mmLabel49.TabIndex = 53;
			mmLabel49.Text = "National ID:";
			comboBoxManager.Assigned = false;
			comboBoxManager.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxManager.CustomReportFieldName = "";
			comboBoxManager.CustomReportKey = "";
			comboBoxManager.CustomReportValueType = 1;
			comboBoxManager.DescriptionTextBox = null;
			appearance70.BackColor = System.Drawing.SystemColors.Window;
			appearance70.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxManager.DisplayLayout.Appearance = appearance70;
			comboBoxManager.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxManager.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance71.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManager.DisplayLayout.GroupByBox.Appearance = appearance71;
			appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManager.DisplayLayout.GroupByBox.BandLabelAppearance = appearance72;
			comboBoxManager.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance73.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance73.BackColor2 = System.Drawing.SystemColors.Control;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManager.DisplayLayout.GroupByBox.PromptAppearance = appearance73;
			comboBoxManager.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxManager.DisplayLayout.MaxRowScrollRegions = 1;
			appearance74.BackColor = System.Drawing.SystemColors.Window;
			appearance74.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxManager.DisplayLayout.Override.ActiveCellAppearance = appearance74;
			appearance75.BackColor = System.Drawing.SystemColors.Highlight;
			appearance75.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxManager.DisplayLayout.Override.ActiveRowAppearance = appearance75;
			comboBoxManager.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxManager.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance76.BackColor = System.Drawing.SystemColors.Window;
			comboBoxManager.DisplayLayout.Override.CardAreaAppearance = appearance76;
			appearance77.BorderColor = System.Drawing.Color.Silver;
			appearance77.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxManager.DisplayLayout.Override.CellAppearance = appearance77;
			comboBoxManager.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxManager.DisplayLayout.Override.CellPadding = 0;
			appearance78.BackColor = System.Drawing.SystemColors.Control;
			appearance78.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance78.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManager.DisplayLayout.Override.GroupByRowAppearance = appearance78;
			appearance79.TextHAlignAsString = "Left";
			comboBoxManager.DisplayLayout.Override.HeaderAppearance = appearance79;
			comboBoxManager.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxManager.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance80.BackColor = System.Drawing.SystemColors.Window;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			comboBoxManager.DisplayLayout.Override.RowAppearance = appearance80;
			comboBoxManager.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance81.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxManager.DisplayLayout.Override.TemplateAddRowAppearance = appearance81;
			comboBoxManager.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxManager.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxManager.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxManager.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxManager.Editable = true;
			comboBoxManager.FilterString = "";
			comboBoxManager.HasAllAccount = false;
			comboBoxManager.HasCustom = false;
			comboBoxManager.IsDataLoaded = false;
			comboBoxManager.Location = new System.Drawing.Point(368, 208);
			comboBoxManager.MaxDropDownItems = 12;
			comboBoxManager.Name = "comboBoxManager";
			comboBoxManager.ShowInactiveItems = false;
			comboBoxManager.ShowQuickAdd = true;
			comboBoxManager.ShowTerminatedEmployees = true;
			comboBoxManager.Size = new System.Drawing.Size(149, 20);
			comboBoxManager.TabIndex = 18;
			comboBoxManager.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPosition.Assigned = false;
			comboBoxPosition.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPosition.CustomReportFieldName = "";
			comboBoxPosition.CustomReportKey = "";
			comboBoxPosition.CustomReportValueType = 1;
			comboBoxPosition.DescriptionTextBox = null;
			appearance82.BackColor = System.Drawing.SystemColors.Window;
			appearance82.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPosition.DisplayLayout.Appearance = appearance82;
			comboBoxPosition.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPosition.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance83.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPosition.DisplayLayout.GroupByBox.Appearance = appearance83;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPosition.DisplayLayout.GroupByBox.BandLabelAppearance = appearance84;
			comboBoxPosition.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance85.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance85.BackColor2 = System.Drawing.SystemColors.Control;
			appearance85.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance85.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPosition.DisplayLayout.GroupByBox.PromptAppearance = appearance85;
			comboBoxPosition.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPosition.DisplayLayout.MaxRowScrollRegions = 1;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			appearance86.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPosition.DisplayLayout.Override.ActiveCellAppearance = appearance86;
			appearance87.BackColor = System.Drawing.SystemColors.Highlight;
			appearance87.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPosition.DisplayLayout.Override.ActiveRowAppearance = appearance87;
			comboBoxPosition.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPosition.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance88.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPosition.DisplayLayout.Override.CardAreaAppearance = appearance88;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			appearance89.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPosition.DisplayLayout.Override.CellAppearance = appearance89;
			comboBoxPosition.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPosition.DisplayLayout.Override.CellPadding = 0;
			appearance90.BackColor = System.Drawing.SystemColors.Control;
			appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance90.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance90.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPosition.DisplayLayout.Override.GroupByRowAppearance = appearance90;
			appearance91.TextHAlignAsString = "Left";
			comboBoxPosition.DisplayLayout.Override.HeaderAppearance = appearance91;
			comboBoxPosition.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPosition.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			comboBoxPosition.DisplayLayout.Override.RowAppearance = appearance92;
			comboBoxPosition.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance93.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPosition.DisplayLayout.Override.TemplateAddRowAppearance = appearance93;
			comboBoxPosition.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPosition.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPosition.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPosition.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPosition.Editable = true;
			comboBoxPosition.FilterString = "";
			comboBoxPosition.HasAllAccount = false;
			comboBoxPosition.HasCustom = false;
			comboBoxPosition.IsDataLoaded = false;
			comboBoxPosition.Location = new System.Drawing.Point(368, 186);
			comboBoxPosition.MaxDropDownItems = 12;
			comboBoxPosition.Name = "comboBoxPosition";
			comboBoxPosition.ShowInactiveItems = false;
			comboBoxPosition.ShowQuickAdd = true;
			comboBoxPosition.Size = new System.Drawing.Size(149, 20);
			comboBoxPosition.TabIndex = 17;
			comboBoxPosition.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPosition.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(comboBoxPosition_InitializeLayout);
			comboBoxDepartment.Assigned = false;
			comboBoxDepartment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDepartment.CustomReportFieldName = "";
			comboBoxDepartment.CustomReportKey = "";
			comboBoxDepartment.CustomReportValueType = 1;
			comboBoxDepartment.DescriptionTextBox = null;
			appearance94.BackColor = System.Drawing.SystemColors.Window;
			appearance94.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDepartment.DisplayLayout.Appearance = appearance94;
			comboBoxDepartment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDepartment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance95.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance95.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance95.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepartment.DisplayLayout.GroupByBox.Appearance = appearance95;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepartment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance96;
			comboBoxDepartment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance97.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance97.BackColor2 = System.Drawing.SystemColors.Control;
			appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance97.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepartment.DisplayLayout.GroupByBox.PromptAppearance = appearance97;
			comboBoxDepartment.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDepartment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDepartment.DisplayLayout.Override.ActiveCellAppearance = appearance98;
			appearance99.BackColor = System.Drawing.SystemColors.Highlight;
			appearance99.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDepartment.DisplayLayout.Override.ActiveRowAppearance = appearance99;
			comboBoxDepartment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDepartment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance100.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDepartment.DisplayLayout.Override.CardAreaAppearance = appearance100;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			appearance101.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDepartment.DisplayLayout.Override.CellAppearance = appearance101;
			comboBoxDepartment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDepartment.DisplayLayout.Override.CellPadding = 0;
			appearance102.BackColor = System.Drawing.SystemColors.Control;
			appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance102.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance102.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepartment.DisplayLayout.Override.GroupByRowAppearance = appearance102;
			appearance103.TextHAlignAsString = "Left";
			comboBoxDepartment.DisplayLayout.Override.HeaderAppearance = appearance103;
			comboBoxDepartment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDepartment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance104.BackColor = System.Drawing.SystemColors.Window;
			appearance104.BorderColor = System.Drawing.Color.Silver;
			comboBoxDepartment.DisplayLayout.Override.RowAppearance = appearance104;
			comboBoxDepartment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance105.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDepartment.DisplayLayout.Override.TemplateAddRowAppearance = appearance105;
			comboBoxDepartment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDepartment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDepartment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDepartment.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDepartment.Editable = true;
			comboBoxDepartment.FilterString = "";
			comboBoxDepartment.HasAllAccount = false;
			comboBoxDepartment.HasCustom = false;
			comboBoxDepartment.IsDataLoaded = false;
			comboBoxDepartment.Location = new System.Drawing.Point(132, 185);
			comboBoxDepartment.MaxDropDownItems = 12;
			comboBoxDepartment.Name = "comboBoxDepartment";
			comboBoxDepartment.ShowInactiveItems = false;
			comboBoxDepartment.ShowQuickAdd = true;
			comboBoxDepartment.Size = new System.Drawing.Size(151, 20);
			comboBoxDepartment.TabIndex = 11;
			comboBoxDepartment.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDivision.Assigned = false;
			comboBoxDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDivision.CustomReportFieldName = "";
			comboBoxDivision.CustomReportKey = "";
			comboBoxDivision.CustomReportValueType = 1;
			comboBoxDivision.DescriptionTextBox = null;
			appearance106.BackColor = System.Drawing.SystemColors.Window;
			appearance106.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDivision.DisplayLayout.Appearance = appearance106;
			comboBoxDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance107.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance107.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance107.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance107.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.GroupByBox.Appearance = appearance107;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance108;
			comboBoxDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance109.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance109.BackColor2 = System.Drawing.SystemColors.Control;
			appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance109;
			comboBoxDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance110.BackColor = System.Drawing.SystemColors.Window;
			appearance110.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDivision.DisplayLayout.Override.ActiveCellAppearance = appearance110;
			appearance111.BackColor = System.Drawing.SystemColors.Highlight;
			appearance111.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDivision.DisplayLayout.Override.ActiveRowAppearance = appearance111;
			comboBoxDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance112.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.CardAreaAppearance = appearance112;
			appearance113.BorderColor = System.Drawing.Color.Silver;
			appearance113.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDivision.DisplayLayout.Override.CellAppearance = appearance113;
			comboBoxDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDivision.DisplayLayout.Override.CellPadding = 0;
			appearance114.BackColor = System.Drawing.SystemColors.Control;
			appearance114.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance114.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance114.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.GroupByRowAppearance = appearance114;
			appearance115.TextHAlignAsString = "Left";
			comboBoxDivision.DisplayLayout.Override.HeaderAppearance = appearance115;
			comboBoxDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance116.BackColor = System.Drawing.SystemColors.Window;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			comboBoxDivision.DisplayLayout.Override.RowAppearance = appearance116;
			comboBoxDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance117.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance117;
			comboBoxDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDivision.Editable = true;
			comboBoxDivision.FilterString = "";
			comboBoxDivision.HasAllAccount = false;
			comboBoxDivision.HasCustom = false;
			comboBoxDivision.IsDataLoaded = false;
			comboBoxDivision.Location = new System.Drawing.Point(132, 163);
			comboBoxDivision.MaxDropDownItems = 12;
			comboBoxDivision.Name = "comboBoxDivision";
			comboBoxDivision.ShowInactiveItems = false;
			comboBoxDivision.ShowQuickAdd = true;
			comboBoxDivision.Size = new System.Drawing.Size(151, 20);
			comboBoxDivision.TabIndex = 10;
			comboBoxDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Location = new System.Drawing.Point(347, 9);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.SelectedID = 0;
			comboBoxStatus.Size = new System.Drawing.Size(84, 21);
			comboBoxStatus.TabIndex = 2;
			checkBoxOnVacation.AutoSize = true;
			checkBoxOnVacation.Enabled = false;
			checkBoxOnVacation.Location = new System.Drawing.Point(437, 12);
			checkBoxOnVacation.Name = "checkBoxOnVacation";
			checkBoxOnVacation.Size = new System.Drawing.Size(85, 17);
			checkBoxOnVacation.TabIndex = 3;
			checkBoxOnVacation.Text = "On Vacation";
			checkBoxOnVacation.UseVisualStyleBackColor = true;
			comboBoxGrade.Assigned = false;
			comboBoxGrade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGrade.CustomReportFieldName = "";
			comboBoxGrade.CustomReportKey = "";
			comboBoxGrade.CustomReportValueType = 1;
			comboBoxGrade.DescriptionTextBox = null;
			appearance118.BackColor = System.Drawing.SystemColors.Window;
			appearance118.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGrade.DisplayLayout.Appearance = appearance118;
			comboBoxGrade.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGrade.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance119.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance119.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance119.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance119.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGrade.DisplayLayout.GroupByBox.Appearance = appearance119;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGrade.DisplayLayout.GroupByBox.BandLabelAppearance = appearance120;
			comboBoxGrade.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance121.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance121.BackColor2 = System.Drawing.SystemColors.Control;
			appearance121.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance121.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGrade.DisplayLayout.GroupByBox.PromptAppearance = appearance121;
			comboBoxGrade.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGrade.DisplayLayout.MaxRowScrollRegions = 1;
			appearance122.BackColor = System.Drawing.SystemColors.Window;
			appearance122.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGrade.DisplayLayout.Override.ActiveCellAppearance = appearance122;
			appearance123.BackColor = System.Drawing.SystemColors.Highlight;
			appearance123.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGrade.DisplayLayout.Override.ActiveRowAppearance = appearance123;
			comboBoxGrade.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGrade.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance124.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGrade.DisplayLayout.Override.CardAreaAppearance = appearance124;
			appearance125.BorderColor = System.Drawing.Color.Silver;
			appearance125.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGrade.DisplayLayout.Override.CellAppearance = appearance125;
			comboBoxGrade.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGrade.DisplayLayout.Override.CellPadding = 0;
			appearance126.BackColor = System.Drawing.SystemColors.Control;
			appearance126.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance126.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance126.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGrade.DisplayLayout.Override.GroupByRowAppearance = appearance126;
			appearance127.TextHAlignAsString = "Left";
			comboBoxGrade.DisplayLayout.Override.HeaderAppearance = appearance127;
			comboBoxGrade.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGrade.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance128.BackColor = System.Drawing.SystemColors.Window;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			comboBoxGrade.DisplayLayout.Override.RowAppearance = appearance128;
			comboBoxGrade.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance129.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGrade.DisplayLayout.Override.TemplateAddRowAppearance = appearance129;
			comboBoxGrade.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGrade.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGrade.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGrade.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGrade.Editable = true;
			comboBoxGrade.FilterString = "";
			comboBoxGrade.HasAllAccount = false;
			comboBoxGrade.HasCustom = false;
			comboBoxGrade.IsDataLoaded = false;
			comboBoxGrade.Location = new System.Drawing.Point(368, 164);
			comboBoxGrade.MaxDropDownItems = 12;
			comboBoxGrade.Name = "comboBoxGrade";
			comboBoxGrade.ShowInactiveItems = false;
			comboBoxGrade.ShowQuickAdd = true;
			comboBoxGrade.Size = new System.Drawing.Size(149, 20);
			comboBoxGrade.TabIndex = 16;
			comboBoxGrade.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(9, 299);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(70, 13);
			mmLabel25.TabIndex = 27;
			mmLabel25.Text = "Joining Date:";
			comboBoxGroup.Assigned = false;
			comboBoxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGroup.CustomReportFieldName = "";
			comboBoxGroup.CustomReportKey = "";
			comboBoxGroup.CustomReportValueType = 1;
			comboBoxGroup.DescriptionTextBox = null;
			appearance130.BackColor = System.Drawing.SystemColors.Window;
			appearance130.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGroup.DisplayLayout.Appearance = appearance130;
			comboBoxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance131.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance131.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance131.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.GroupByBox.Appearance = appearance131;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance132;
			comboBoxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance133.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance133.BackColor2 = System.Drawing.SystemColors.Control;
			appearance133.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance133.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance133;
			comboBoxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance134.BackColor = System.Drawing.SystemColors.Window;
			appearance134.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance134;
			appearance135.BackColor = System.Drawing.SystemColors.Highlight;
			appearance135.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance135;
			comboBoxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance136.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.Override.CardAreaAppearance = appearance136;
			appearance137.BorderColor = System.Drawing.Color.Silver;
			appearance137.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGroup.DisplayLayout.Override.CellAppearance = appearance137;
			comboBoxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance138.BackColor = System.Drawing.SystemColors.Control;
			appearance138.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance138.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance138.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance138.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance138;
			appearance139.TextHAlignAsString = "Left";
			comboBoxGroup.DisplayLayout.Override.HeaderAppearance = appearance139;
			comboBoxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance140.BackColor = System.Drawing.SystemColors.Window;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			comboBoxGroup.DisplayLayout.Override.RowAppearance = appearance140;
			comboBoxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance141.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance141;
			comboBoxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGroup.Editable = true;
			comboBoxGroup.FilterString = "";
			comboBoxGroup.HasAllAccount = false;
			comboBoxGroup.HasCustom = false;
			comboBoxGroup.IsDataLoaded = false;
			comboBoxGroup.Location = new System.Drawing.Point(368, 142);
			comboBoxGroup.MaxDropDownItems = 12;
			comboBoxGroup.Name = "comboBoxGroup";
			comboBoxGroup.ShowInactiveItems = false;
			comboBoxGroup.ShowQuickAdd = true;
			comboBoxGroup.Size = new System.Drawing.Size(149, 20);
			comboBoxGroup.TabIndex = 15;
			comboBoxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(9, 99);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(60, 13);
			mmLabel5.TabIndex = 8;
			mmLabel5.Text = "Nick Name:";
			lblDescriptions.AutoSize = true;
			lblDescriptions.BackColor = System.Drawing.Color.Transparent;
			lblDescriptions.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblDescriptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDescriptions.IsFieldHeader = false;
			lblDescriptions.IsRequired = false;
			lblDescriptions.Location = new System.Drawing.Point(9, 77);
			lblDescriptions.Name = "lblDescriptions";
			lblDescriptions.PenWidth = 1f;
			lblDescriptions.ShowBorder = false;
			lblDescriptions.Size = new System.Drawing.Size(71, 13);
			lblDescriptions.TabIndex = 6;
			lblDescriptions.Text = "Middle Name:";
			textBoxNickName.BackColor = System.Drawing.Color.White;
			textBoxNickName.CustomReportFieldName = "";
			textBoxNickName.CustomReportKey = "";
			textBoxNickName.CustomReportValueType = 1;
			textBoxNickName.IsComboTextBox = false;
			textBoxNickName.IsModified = false;
			textBoxNickName.Location = new System.Drawing.Point(132, 97);
			textBoxNickName.MaxLength = 30;
			textBoxNickName.Name = "textBoxNickName";
			textBoxNickName.Size = new System.Drawing.Size(385, 20);
			textBoxNickName.TabIndex = 7;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(132, 9);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(161, 20);
			textBoxCode.TabIndex = 0;
			textBoxLastName.BackColor = System.Drawing.Color.White;
			textBoxLastName.CustomReportFieldName = "";
			textBoxLastName.CustomReportKey = "";
			textBoxLastName.CustomReportValueType = 1;
			textBoxLastName.IsComboTextBox = false;
			textBoxLastName.IsModified = false;
			textBoxLastName.IsRequired = true;
			textBoxLastName.Location = new System.Drawing.Point(132, 53);
			textBoxLastName.MaxLength = 30;
			textBoxLastName.Name = "textBoxLastName";
			textBoxLastName.Size = new System.Drawing.Size(385, 20);
			textBoxLastName.TabIndex = 5;
			textBoxFirstName.BackColor = System.Drawing.Color.White;
			textBoxFirstName.CustomReportFieldName = "";
			textBoxFirstName.CustomReportKey = "";
			textBoxFirstName.CustomReportValueType = 1;
			textBoxFirstName.IsComboTextBox = false;
			textBoxFirstName.IsModified = false;
			textBoxFirstName.IsRequired = true;
			textBoxFirstName.Location = new System.Drawing.Point(132, 31);
			textBoxFirstName.MaxLength = 30;
			textBoxFirstName.Name = "textBoxFirstName";
			textBoxFirstName.Size = new System.Drawing.Size(385, 20);
			textBoxFirstName.TabIndex = 4;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(9, 54);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(61, 13);
			mmLabel6.TabIndex = 4;
			mmLabel6.Text = "Last Name:";
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f);
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.IsFieldHeader = false;
			label1.IsRequired = false;
			label1.Location = new System.Drawing.Point(9, 31);
			label1.Name = "label1";
			label1.PenWidth = 1f;
			label1.ShowBorder = false;
			label1.Size = new System.Drawing.Size(62, 13);
			label1.TabIndex = 2;
			label1.Text = "First Name:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxMiddleName.BackColor = System.Drawing.Color.White;
			textBoxMiddleName.CustomReportFieldName = "";
			textBoxMiddleName.CustomReportKey = "";
			textBoxMiddleName.CustomReportValueType = 1;
			textBoxMiddleName.IsComboTextBox = false;
			textBoxMiddleName.IsModified = false;
			textBoxMiddleName.Location = new System.Drawing.Point(132, 75);
			textBoxMiddleName.MaxLength = 30;
			textBoxMiddleName.Name = "textBoxMiddleName";
			textBoxMiddleName.Size = new System.Drawing.Size(385, 20);
			textBoxMiddleName.TabIndex = 6;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(9, 164);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(46, 14);
			ultraFormattedLinkLabel1.TabIndex = 57;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Division:";
			appearance142.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance142;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(294, 144);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(38, 14);
			ultraFormattedLinkLabel3.TabIndex = 59;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Group:";
			appearance143.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance143;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(294, 165);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(38, 14);
			ultraFormattedLinkLabel4.TabIndex = 60;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Grade:";
			appearance144.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance144;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(294, 188);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(47, 14);
			ultraFormattedLinkLabel5.TabIndex = 61;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Position:";
			appearance145.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance145;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel17);
			tabPageDetails.Controls.Add(comboBoxLegalPosition);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabelVisaDesig);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabelRcrtmnt);
			tabPageDetails.Controls.Add(comboBoxVisaDesignation);
			tabPageDetails.Controls.Add(comboBoxRecruitmentChannel);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel14);
			tabPageDetails.Controls.Add(comboBoxHolidayCalendar);
			tabPageDetails.Controls.Add(ultraGroupBox3);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel15);
			tabPageDetails.Controls.Add(textBoxAppraisalPoints);
			tabPageDetails.Controls.Add(mmLabel4);
			tabPageDetails.Controls.Add(comboBoxQualification);
			tabPageDetails.Controls.Add(ultraGroupBox2);
			tabPageDetails.Controls.Add(textBoxLabourID);
			tabPageDetails.Controls.Add(dateTimePickerConfirmation);
			tabPageDetails.Controls.Add(mmLabel1);
			tabPageDetails.Controls.Add(textBoxSpouse);
			tabPageDetails.Controls.Add(mmLabel48);
			tabPageDetails.Controls.Add(mmLabel40);
			tabPageDetails.Controls.Add(textBoxBloodGroup);
			tabPageDetails.Controls.Add(comboBoxReligion);
			tabPageDetails.Controls.Add(mmLabel38);
			tabPageDetails.Controls.Add(textBoxProbation);
			tabPageDetails.Controls.Add(mmLabel37);
			tabPageDetails.Controls.Add(comboBoxDayOff);
			tabPageDetails.Controls.Add(mmLabel32);
			tabPageDetails.Controls.Add(ultraFormattedLinkLabel9);
			tabPageDetails.Location = new System.Drawing.Point(2, 21);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(696, 420);
			ultraFormattedLinkLabel17.AutoSize = true;
			ultraFormattedLinkLabel17.Location = new System.Drawing.Point(460, 81);
			ultraFormattedLinkLabel17.Name = "ultraFormattedLinkLabel17";
			ultraFormattedLinkLabel17.Size = new System.Drawing.Size(77, 14);
			ultraFormattedLinkLabel17.TabIndex = 135;
			ultraFormattedLinkLabel17.TabStop = true;
			ultraFormattedLinkLabel17.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel17.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel17.Value = "Legal Position:";
			appearance146.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel17.VisitedLinkAppearance = appearance146;
			ultraFormattedLinkLabel17.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel17_LinkClicked);
			comboBoxLegalPosition.Assigned = false;
			comboBoxLegalPosition.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLegalPosition.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLegalPosition.CustomReportFieldName = "";
			comboBoxLegalPosition.CustomReportKey = "";
			comboBoxLegalPosition.CustomReportValueType = 1;
			comboBoxLegalPosition.DescriptionTextBox = null;
			comboBoxLegalPosition.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLegalPosition.Editable = true;
			comboBoxLegalPosition.FilterString = "";
			comboBoxLegalPosition.GenericListType = Micromind.Common.Data.GenericListTypes.LegalPosition;
			comboBoxLegalPosition.HasAllAccount = false;
			comboBoxLegalPosition.HasCustom = false;
			comboBoxLegalPosition.IsDataLoaded = false;
			comboBoxLegalPosition.IsSingleColumn = false;
			comboBoxLegalPosition.Location = new System.Drawing.Point(538, 77);
			comboBoxLegalPosition.MaxDropDownItems = 12;
			comboBoxLegalPosition.Name = "comboBoxLegalPosition";
			comboBoxLegalPosition.ShowInactiveItems = false;
			comboBoxLegalPosition.ShowQuickAdd = true;
			comboBoxLegalPosition.Size = new System.Drawing.Size(132, 20);
			comboBoxLegalPosition.TabIndex = 12;
			comboBoxLegalPosition.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabelVisaDesig.AutoSize = true;
			ultraFormattedLinkLabelVisaDesig.Location = new System.Drawing.Point(257, 81);
			ultraFormattedLinkLabelVisaDesig.Name = "ultraFormattedLinkLabelVisaDesig";
			ultraFormattedLinkLabelVisaDesig.Size = new System.Drawing.Size(71, 14);
			ultraFormattedLinkLabelVisaDesig.TabIndex = 133;
			ultraFormattedLinkLabelVisaDesig.TabStop = true;
			ultraFormattedLinkLabelVisaDesig.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelVisaDesig.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelVisaDesig.Value = "Visa Position:";
			appearance147.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelVisaDesig.VisitedLinkAppearance = appearance147;
			ultraFormattedLinkLabelVisaDesig.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelVisaDesig_LinkClicked);
			ultraFormattedLinkLabelRcrtmnt.AutoSize = true;
			ultraFormattedLinkLabelRcrtmnt.Location = new System.Drawing.Point(6, 81);
			ultraFormattedLinkLabelRcrtmnt.Name = "ultraFormattedLinkLabelRcrtmnt";
			ultraFormattedLinkLabelRcrtmnt.Size = new System.Drawing.Size(82, 14);
			ultraFormattedLinkLabelRcrtmnt.TabIndex = 132;
			ultraFormattedLinkLabelRcrtmnt.TabStop = true;
			toolTip1.SetToolTip(ultraFormattedLinkLabelRcrtmnt, "Recruitment Channel");
			ultraFormattedLinkLabelRcrtmnt.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelRcrtmnt.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelRcrtmnt.Value = "RCRT Channel:";
			appearance148.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelRcrtmnt.VisitedLinkAppearance = appearance148;
			ultraFormattedLinkLabelRcrtmnt.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelRcrtmnt_LinkClicked);
			comboBoxVisaDesignation.Assigned = false;
			comboBoxVisaDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVisaDesignation.CustomReportFieldName = "";
			comboBoxVisaDesignation.CustomReportKey = "";
			comboBoxVisaDesignation.CustomReportValueType = 1;
			comboBoxVisaDesignation.DescriptionTextBox = null;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			appearance149.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVisaDesignation.DisplayLayout.Appearance = appearance149;
			comboBoxVisaDesignation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVisaDesignation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance150.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance150.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance150.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance150.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVisaDesignation.DisplayLayout.GroupByBox.Appearance = appearance150;
			appearance151.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVisaDesignation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance151;
			comboBoxVisaDesignation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance152.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance152.BackColor2 = System.Drawing.SystemColors.Control;
			appearance152.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance152.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVisaDesignation.DisplayLayout.GroupByBox.PromptAppearance = appearance152;
			comboBoxVisaDesignation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVisaDesignation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance153.BackColor = System.Drawing.SystemColors.Window;
			appearance153.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVisaDesignation.DisplayLayout.Override.ActiveCellAppearance = appearance153;
			appearance154.BackColor = System.Drawing.SystemColors.Highlight;
			appearance154.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVisaDesignation.DisplayLayout.Override.ActiveRowAppearance = appearance154;
			comboBoxVisaDesignation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVisaDesignation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance155.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVisaDesignation.DisplayLayout.Override.CardAreaAppearance = appearance155;
			appearance156.BorderColor = System.Drawing.Color.Silver;
			appearance156.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVisaDesignation.DisplayLayout.Override.CellAppearance = appearance156;
			comboBoxVisaDesignation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVisaDesignation.DisplayLayout.Override.CellPadding = 0;
			appearance157.BackColor = System.Drawing.SystemColors.Control;
			appearance157.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance157.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance157.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance157.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVisaDesignation.DisplayLayout.Override.GroupByRowAppearance = appearance157;
			appearance158.TextHAlignAsString = "Left";
			comboBoxVisaDesignation.DisplayLayout.Override.HeaderAppearance = appearance158;
			comboBoxVisaDesignation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVisaDesignation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance159.BackColor = System.Drawing.SystemColors.Window;
			appearance159.BorderColor = System.Drawing.Color.Silver;
			comboBoxVisaDesignation.DisplayLayout.Override.RowAppearance = appearance159;
			comboBoxVisaDesignation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance160.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVisaDesignation.DisplayLayout.Override.TemplateAddRowAppearance = appearance160;
			comboBoxVisaDesignation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVisaDesignation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVisaDesignation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVisaDesignation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVisaDesignation.Editable = true;
			comboBoxVisaDesignation.FilterString = "";
			comboBoxVisaDesignation.HasAllAccount = false;
			comboBoxVisaDesignation.HasCustom = false;
			comboBoxVisaDesignation.IsDataLoaded = false;
			comboBoxVisaDesignation.Location = new System.Drawing.Point(338, 77);
			comboBoxVisaDesignation.MaxDropDownItems = 12;
			comboBoxVisaDesignation.Name = "comboBoxVisaDesignation";
			comboBoxVisaDesignation.ShowInactiveItems = false;
			comboBoxVisaDesignation.ShowQuickAdd = true;
			comboBoxVisaDesignation.Size = new System.Drawing.Size(117, 20);
			comboBoxVisaDesignation.TabIndex = 11;
			comboBoxVisaDesignation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxRecruitmentChannel.Assigned = false;
			comboBoxRecruitmentChannel.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxRecruitmentChannel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRecruitmentChannel.CustomReportFieldName = "";
			comboBoxRecruitmentChannel.CustomReportKey = "";
			comboBoxRecruitmentChannel.CustomReportValueType = 1;
			comboBoxRecruitmentChannel.DescriptionTextBox = null;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			appearance161.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRecruitmentChannel.DisplayLayout.Appearance = appearance161;
			comboBoxRecruitmentChannel.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRecruitmentChannel.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance162.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance162.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance162.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance162.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRecruitmentChannel.DisplayLayout.GroupByBox.Appearance = appearance162;
			appearance163.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRecruitmentChannel.DisplayLayout.GroupByBox.BandLabelAppearance = appearance163;
			comboBoxRecruitmentChannel.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance164.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance164.BackColor2 = System.Drawing.SystemColors.Control;
			appearance164.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance164.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRecruitmentChannel.DisplayLayout.GroupByBox.PromptAppearance = appearance164;
			comboBoxRecruitmentChannel.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRecruitmentChannel.DisplayLayout.MaxRowScrollRegions = 1;
			appearance165.BackColor = System.Drawing.SystemColors.Window;
			appearance165.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRecruitmentChannel.DisplayLayout.Override.ActiveCellAppearance = appearance165;
			appearance166.BackColor = System.Drawing.SystemColors.Highlight;
			appearance166.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRecruitmentChannel.DisplayLayout.Override.ActiveRowAppearance = appearance166;
			comboBoxRecruitmentChannel.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRecruitmentChannel.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRecruitmentChannel.DisplayLayout.Override.CardAreaAppearance = appearance167;
			appearance168.BorderColor = System.Drawing.Color.Silver;
			appearance168.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRecruitmentChannel.DisplayLayout.Override.CellAppearance = appearance168;
			comboBoxRecruitmentChannel.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRecruitmentChannel.DisplayLayout.Override.CellPadding = 0;
			appearance169.BackColor = System.Drawing.SystemColors.Control;
			appearance169.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance169.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance169.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance169.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRecruitmentChannel.DisplayLayout.Override.GroupByRowAppearance = appearance169;
			appearance170.TextHAlignAsString = "Left";
			comboBoxRecruitmentChannel.DisplayLayout.Override.HeaderAppearance = appearance170;
			comboBoxRecruitmentChannel.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRecruitmentChannel.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance171.BackColor = System.Drawing.SystemColors.Window;
			appearance171.BorderColor = System.Drawing.Color.Silver;
			comboBoxRecruitmentChannel.DisplayLayout.Override.RowAppearance = appearance171;
			comboBoxRecruitmentChannel.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance172.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRecruitmentChannel.DisplayLayout.Override.TemplateAddRowAppearance = appearance172;
			comboBoxRecruitmentChannel.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRecruitmentChannel.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRecruitmentChannel.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRecruitmentChannel.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRecruitmentChannel.Editable = true;
			comboBoxRecruitmentChannel.FilterString = "";
			comboBoxRecruitmentChannel.HasAllAccount = false;
			comboBoxRecruitmentChannel.HasCustom = false;
			comboBoxRecruitmentChannel.IsDataLoaded = false;
			comboBoxRecruitmentChannel.Location = new System.Drawing.Point(90, 77);
			comboBoxRecruitmentChannel.MaxDropDownItems = 12;
			comboBoxRecruitmentChannel.MaxLength = 100;
			comboBoxRecruitmentChannel.Name = "comboBoxRecruitmentChannel";
			comboBoxRecruitmentChannel.ShowInactiveItems = false;
			comboBoxRecruitmentChannel.ShowQuickAdd = true;
			comboBoxRecruitmentChannel.Size = new System.Drawing.Size(158, 20);
			comboBoxRecruitmentChannel.TabIndex = 10;
			comboBoxRecruitmentChannel.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel14.AutoSize = true;
			ultraFormattedLinkLabel14.Location = new System.Drawing.Point(460, 58);
			ultraFormattedLinkLabel14.Name = "ultraFormattedLinkLabel14";
			ultraFormattedLinkLabel14.Size = new System.Drawing.Size(64, 14);
			ultraFormattedLinkLabel14.TabIndex = 8;
			ultraFormattedLinkLabel14.TabStop = true;
			toolTip1.SetToolTip(ultraFormattedLinkLabel14, "Holiday Calendar");
			ultraFormattedLinkLabel14.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel14.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel14.Value = "Holiday Cal:";
			appearance173.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel14.VisitedLinkAppearance = appearance173;
			ultraFormattedLinkLabel14.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel14_LinkClicked);
			comboBoxHolidayCalendar.Assigned = false;
			comboBoxHolidayCalendar.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxHolidayCalendar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxHolidayCalendar.CustomReportFieldName = "";
			comboBoxHolidayCalendar.CustomReportKey = "";
			comboBoxHolidayCalendar.CustomReportValueType = 1;
			comboBoxHolidayCalendar.DescriptionTextBox = null;
			appearance174.BackColor = System.Drawing.SystemColors.Window;
			appearance174.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxHolidayCalendar.DisplayLayout.Appearance = appearance174;
			comboBoxHolidayCalendar.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxHolidayCalendar.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance175.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance175.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance175.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance175.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxHolidayCalendar.DisplayLayout.GroupByBox.Appearance = appearance175;
			appearance176.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxHolidayCalendar.DisplayLayout.GroupByBox.BandLabelAppearance = appearance176;
			comboBoxHolidayCalendar.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance177.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance177.BackColor2 = System.Drawing.SystemColors.Control;
			appearance177.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance177.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxHolidayCalendar.DisplayLayout.GroupByBox.PromptAppearance = appearance177;
			comboBoxHolidayCalendar.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxHolidayCalendar.DisplayLayout.MaxRowScrollRegions = 1;
			appearance178.BackColor = System.Drawing.SystemColors.Window;
			appearance178.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxHolidayCalendar.DisplayLayout.Override.ActiveCellAppearance = appearance178;
			appearance179.BackColor = System.Drawing.SystemColors.Highlight;
			appearance179.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxHolidayCalendar.DisplayLayout.Override.ActiveRowAppearance = appearance179;
			comboBoxHolidayCalendar.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxHolidayCalendar.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance180.BackColor = System.Drawing.SystemColors.Window;
			comboBoxHolidayCalendar.DisplayLayout.Override.CardAreaAppearance = appearance180;
			appearance181.BorderColor = System.Drawing.Color.Silver;
			appearance181.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxHolidayCalendar.DisplayLayout.Override.CellAppearance = appearance181;
			comboBoxHolidayCalendar.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxHolidayCalendar.DisplayLayout.Override.CellPadding = 0;
			appearance182.BackColor = System.Drawing.SystemColors.Control;
			appearance182.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance182.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance182.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance182.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxHolidayCalendar.DisplayLayout.Override.GroupByRowAppearance = appearance182;
			appearance183.TextHAlignAsString = "Left";
			comboBoxHolidayCalendar.DisplayLayout.Override.HeaderAppearance = appearance183;
			comboBoxHolidayCalendar.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxHolidayCalendar.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance184.BackColor = System.Drawing.SystemColors.Window;
			appearance184.BorderColor = System.Drawing.Color.Silver;
			comboBoxHolidayCalendar.DisplayLayout.Override.RowAppearance = appearance184;
			comboBoxHolidayCalendar.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance185.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxHolidayCalendar.DisplayLayout.Override.TemplateAddRowAppearance = appearance185;
			comboBoxHolidayCalendar.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxHolidayCalendar.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxHolidayCalendar.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxHolidayCalendar.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxHolidayCalendar.Editable = true;
			comboBoxHolidayCalendar.FilterString = "";
			comboBoxHolidayCalendar.HasAllAccount = false;
			comboBoxHolidayCalendar.HasCustom = false;
			comboBoxHolidayCalendar.IsDataLoaded = false;
			comboBoxHolidayCalendar.Location = new System.Drawing.Point(538, 56);
			comboBoxHolidayCalendar.MaxDropDownItems = 12;
			comboBoxHolidayCalendar.Name = "comboBoxHolidayCalendar";
			comboBoxHolidayCalendar.ShowInactiveItems = false;
			comboBoxHolidayCalendar.ShowQuickAdd = true;
			comboBoxHolidayCalendar.Size = new System.Drawing.Size(132, 20);
			comboBoxHolidayCalendar.TabIndex = 9;
			comboBoxHolidayCalendar.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox3.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox3.Controls.Add(textBoxInsuranceAmount);
			ultraGroupBox3.Controls.Add(mmLabel29);
			ultraGroupBox3.Controls.Add(ultraFormattedLinkLabel13);
			ultraGroupBox3.Controls.Add(ultraFormattedLinkLabel12);
			ultraGroupBox3.Controls.Add(textBoxFamilyMembersCount);
			ultraGroupBox3.Controls.Add(mmLabel26);
			ultraGroupBox3.Controls.Add(textBoxInsuranceNumber);
			ultraGroupBox3.Controls.Add(mmLabel21);
			ultraGroupBox3.Controls.Add(datetimePickerValidTo);
			ultraGroupBox3.Controls.Add(mmLabel28);
			ultraGroupBox3.Controls.Add(dateTimePickerValidFrom);
			ultraGroupBox3.Controls.Add(mmLabel27);
			ultraGroupBox3.Controls.Add(comboBoxInsuranceCategory);
			ultraGroupBox3.Controls.Add(textBoxInsuranceCategory);
			ultraGroupBox3.Controls.Add(textBoxMedicalInsuranceProvider);
			ultraGroupBox3.Controls.Add(comboBoxMedicalInsuarnceProvider);
			ultraGroupBox3.Location = new System.Drawing.Point(6, 274);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(687, 142);
			ultraGroupBox3.TabIndex = 14;
			ultraGroupBox3.Text = "Medical Insurance Details";
			textBoxInsuranceAmount.AllowDecimal = true;
			textBoxInsuranceAmount.CustomReportFieldName = "";
			textBoxInsuranceAmount.CustomReportKey = "";
			textBoxInsuranceAmount.CustomReportValueType = 1;
			textBoxInsuranceAmount.IsComboTextBox = false;
			textBoxInsuranceAmount.IsModified = false;
			textBoxInsuranceAmount.Location = new System.Drawing.Point(137, 118);
			textBoxInsuranceAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxInsuranceAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxInsuranceAmount.Name = "textBoxInsuranceAmount";
			textBoxInsuranceAmount.NullText = "0";
			textBoxInsuranceAmount.Size = new System.Drawing.Size(141, 20);
			textBoxInsuranceAmount.TabIndex = 8;
			textBoxInsuranceAmount.Text = "0.00";
			textBoxInsuranceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInsuranceAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel29.AutoSize = true;
			mmLabel29.BackColor = System.Drawing.Color.Transparent;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(7, 120);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(99, 13);
			mmLabel29.TabIndex = 83;
			mmLabel29.Text = "Insurance Amount:";
			ultraFormattedLinkLabel13.AutoSize = true;
			ultraFormattedLinkLabel13.Location = new System.Drawing.Point(7, 52);
			ultraFormattedLinkLabel13.Name = "ultraFormattedLinkLabel13";
			ultraFormattedLinkLabel13.Size = new System.Drawing.Size(103, 14);
			ultraFormattedLinkLabel13.TabIndex = 81;
			ultraFormattedLinkLabel13.TabStop = true;
			ultraFormattedLinkLabel13.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel13.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel13.Value = "Insurance Category:";
			appearance186.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel13.VisitedLinkAppearance = appearance186;
			ultraFormattedLinkLabel13.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel13_LinkClicked);
			ultraFormattedLinkLabel12.AutoSize = true;
			ultraFormattedLinkLabel12.Location = new System.Drawing.Point(7, 29);
			ultraFormattedLinkLabel12.Name = "ultraFormattedLinkLabel12";
			ultraFormattedLinkLabel12.Size = new System.Drawing.Size(100, 14);
			ultraFormattedLinkLabel12.TabIndex = 68;
			ultraFormattedLinkLabel12.TabStop = true;
			ultraFormattedLinkLabel12.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel12.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel12.Value = "Insurance Provider:";
			appearance187.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel12.VisitedLinkAppearance = appearance187;
			ultraFormattedLinkLabel12.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel12_LinkClicked);
			textBoxFamilyMembersCount.AllowDecimal = false;
			textBoxFamilyMembersCount.BackColor = System.Drawing.Color.White;
			textBoxFamilyMembersCount.CustomReportFieldName = "";
			textBoxFamilyMembersCount.CustomReportKey = "";
			textBoxFamilyMembersCount.CustomReportValueType = 1;
			textBoxFamilyMembersCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxFamilyMembersCount.IsComboTextBox = false;
			textBoxFamilyMembersCount.IsModified = false;
			textBoxFamilyMembersCount.Location = new System.Drawing.Point(351, 95);
			textBoxFamilyMembersCount.MaxLength = 2;
			textBoxFamilyMembersCount.MaxValue = new decimal(new int[4]
			{
				10,
				0,
				0,
				0
			});
			textBoxFamilyMembersCount.MinValue = new decimal(new int[4]);
			textBoxFamilyMembersCount.Name = "textBoxFamilyMembersCount";
			textBoxFamilyMembersCount.NullText = "0";
			textBoxFamilyMembersCount.Size = new System.Drawing.Size(56, 20);
			textBoxFamilyMembersCount.TabIndex = 7;
			textBoxFamilyMembersCount.Text = "0";
			textBoxFamilyMembersCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel26.AutoSize = true;
			mmLabel26.BackColor = System.Drawing.Color.Transparent;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(7, 99);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(99, 13);
			mmLabel26.TabIndex = 80;
			mmLabel26.Text = "Insurance Number:";
			textBoxInsuranceNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxInsuranceNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxInsuranceNumber.BackColor = System.Drawing.Color.White;
			textBoxInsuranceNumber.CustomReportFieldName = "";
			textBoxInsuranceNumber.CustomReportKey = "";
			textBoxInsuranceNumber.CustomReportValueType = 1;
			textBoxInsuranceNumber.IsComboTextBox = false;
			textBoxInsuranceNumber.IsModified = false;
			textBoxInsuranceNumber.Location = new System.Drawing.Point(137, 95);
			textBoxInsuranceNumber.MaxLength = 50;
			textBoxInsuranceNumber.Name = "textBoxInsuranceNumber";
			textBoxInsuranceNumber.Size = new System.Drawing.Size(120, 20);
			textBoxInsuranceNumber.TabIndex = 6;
			mmLabel21.AutoSize = true;
			mmLabel21.BackColor = System.Drawing.Color.Transparent;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel21.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(263, 99);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(87, 13);
			mmLabel21.TabIndex = 78;
			mmLabel21.Text = "Family Members:";
			datetimePickerValidTo.Checked = false;
			datetimePickerValidTo.CustomFormat = " ";
			datetimePickerValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimePickerValidTo.Location = new System.Drawing.Point(351, 72);
			datetimePickerValidTo.Name = "datetimePickerValidTo";
			datetimePickerValidTo.ShowCheckBox = true;
			datetimePickerValidTo.Size = new System.Drawing.Size(141, 20);
			datetimePickerValidTo.TabIndex = 5;
			datetimePickerValidTo.Value = new System.DateTime(0L);
			mmLabel28.AutoSize = true;
			mmLabel28.BackColor = System.Drawing.Color.Transparent;
			mmLabel28.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel28.IsFieldHeader = false;
			mmLabel28.IsRequired = false;
			mmLabel28.Location = new System.Drawing.Point(288, 77);
			mmLabel28.Name = "mmLabel28";
			mmLabel28.PenWidth = 1f;
			mmLabel28.ShowBorder = false;
			mmLabel28.Size = new System.Drawing.Size(57, 13);
			mmLabel28.TabIndex = 76;
			mmLabel28.Text = "Valid Until:";
			dateTimePickerValidFrom.Checked = false;
			dateTimePickerValidFrom.CustomFormat = " ";
			dateTimePickerValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerValidFrom.Location = new System.Drawing.Point(137, 72);
			dateTimePickerValidFrom.Name = "dateTimePickerValidFrom";
			dateTimePickerValidFrom.ShowCheckBox = true;
			dateTimePickerValidFrom.Size = new System.Drawing.Size(141, 20);
			dateTimePickerValidFrom.TabIndex = 4;
			dateTimePickerValidFrom.Value = new System.DateTime(0L);
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(7, 77);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(60, 13);
			mmLabel27.TabIndex = 73;
			mmLabel27.Text = "Valid From:";
			comboBoxInsuranceCategory.Assigned = false;
			comboBoxInsuranceCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxInsuranceCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxInsuranceCategory.CustomReportFieldName = "";
			comboBoxInsuranceCategory.CustomReportKey = "";
			comboBoxInsuranceCategory.CustomReportValueType = 1;
			comboBoxInsuranceCategory.DescriptionTextBox = textBoxInsuranceCategory;
			appearance188.BackColor = System.Drawing.SystemColors.Window;
			appearance188.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxInsuranceCategory.DisplayLayout.Appearance = appearance188;
			comboBoxInsuranceCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxInsuranceCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance189.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance189.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance189.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance189.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInsuranceCategory.DisplayLayout.GroupByBox.Appearance = appearance189;
			appearance190.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInsuranceCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance190;
			comboBoxInsuranceCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance191.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance191.BackColor2 = System.Drawing.SystemColors.Control;
			appearance191.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance191.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInsuranceCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance191;
			comboBoxInsuranceCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxInsuranceCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance192.BackColor = System.Drawing.SystemColors.Window;
			appearance192.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxInsuranceCategory.DisplayLayout.Override.ActiveCellAppearance = appearance192;
			appearance193.BackColor = System.Drawing.SystemColors.Highlight;
			appearance193.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxInsuranceCategory.DisplayLayout.Override.ActiveRowAppearance = appearance193;
			comboBoxInsuranceCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxInsuranceCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance194.BackColor = System.Drawing.SystemColors.Window;
			comboBoxInsuranceCategory.DisplayLayout.Override.CardAreaAppearance = appearance194;
			appearance195.BorderColor = System.Drawing.Color.Silver;
			appearance195.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxInsuranceCategory.DisplayLayout.Override.CellAppearance = appearance195;
			comboBoxInsuranceCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxInsuranceCategory.DisplayLayout.Override.CellPadding = 0;
			appearance196.BackColor = System.Drawing.SystemColors.Control;
			appearance196.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance196.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance196.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance196.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInsuranceCategory.DisplayLayout.Override.GroupByRowAppearance = appearance196;
			appearance197.TextHAlignAsString = "Left";
			comboBoxInsuranceCategory.DisplayLayout.Override.HeaderAppearance = appearance197;
			comboBoxInsuranceCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxInsuranceCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance198.BackColor = System.Drawing.SystemColors.Window;
			appearance198.BorderColor = System.Drawing.Color.Silver;
			comboBoxInsuranceCategory.DisplayLayout.Override.RowAppearance = appearance198;
			comboBoxInsuranceCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance199.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxInsuranceCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance199;
			comboBoxInsuranceCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxInsuranceCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxInsuranceCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxInsuranceCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxInsuranceCategory.Editable = true;
			comboBoxInsuranceCategory.FilterString = "";
			comboBoxInsuranceCategory.GenericListType = Micromind.Common.Data.GenericListTypes.MedicalInsuranceCategory;
			comboBoxInsuranceCategory.HasAllAccount = false;
			comboBoxInsuranceCategory.HasCustom = false;
			comboBoxInsuranceCategory.IsDataLoaded = false;
			comboBoxInsuranceCategory.IsSingleColumn = false;
			comboBoxInsuranceCategory.Location = new System.Drawing.Point(137, 49);
			comboBoxInsuranceCategory.MaxDropDownItems = 12;
			comboBoxInsuranceCategory.Name = "comboBoxInsuranceCategory";
			comboBoxInsuranceCategory.ShowInactiveItems = false;
			comboBoxInsuranceCategory.ShowQuickAdd = true;
			comboBoxInsuranceCategory.Size = new System.Drawing.Size(141, 20);
			comboBoxInsuranceCategory.TabIndex = 2;
			comboBoxInsuranceCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxInsuranceCategory.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxInsuranceCategory.CustomReportFieldName = "";
			textBoxInsuranceCategory.CustomReportKey = "";
			textBoxInsuranceCategory.CustomReportValueType = 1;
			textBoxInsuranceCategory.Enabled = false;
			textBoxInsuranceCategory.ForeColor = System.Drawing.Color.Black;
			textBoxInsuranceCategory.IsComboTextBox = false;
			textBoxInsuranceCategory.IsModified = false;
			textBoxInsuranceCategory.Location = new System.Drawing.Point(279, 49);
			textBoxInsuranceCategory.MaxLength = 15;
			textBoxInsuranceCategory.Name = "textBoxInsuranceCategory";
			textBoxInsuranceCategory.Size = new System.Drawing.Size(283, 20);
			textBoxInsuranceCategory.TabIndex = 3;
			textBoxInsuranceCategory.TabStop = false;
			textBoxMedicalInsuranceProvider.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxMedicalInsuranceProvider.CustomReportFieldName = "";
			textBoxMedicalInsuranceProvider.CustomReportKey = "";
			textBoxMedicalInsuranceProvider.CustomReportValueType = 1;
			textBoxMedicalInsuranceProvider.Enabled = false;
			textBoxMedicalInsuranceProvider.ForeColor = System.Drawing.Color.Black;
			textBoxMedicalInsuranceProvider.IsComboTextBox = false;
			textBoxMedicalInsuranceProvider.IsModified = false;
			textBoxMedicalInsuranceProvider.Location = new System.Drawing.Point(279, 26);
			textBoxMedicalInsuranceProvider.MaxLength = 15;
			textBoxMedicalInsuranceProvider.Name = "textBoxMedicalInsuranceProvider";
			textBoxMedicalInsuranceProvider.Size = new System.Drawing.Size(283, 20);
			textBoxMedicalInsuranceProvider.TabIndex = 1;
			textBoxMedicalInsuranceProvider.TabStop = false;
			comboBoxMedicalInsuarnceProvider.Assigned = false;
			comboBoxMedicalInsuarnceProvider.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxMedicalInsuarnceProvider.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxMedicalInsuarnceProvider.CustomReportFieldName = "";
			comboBoxMedicalInsuarnceProvider.CustomReportKey = "";
			comboBoxMedicalInsuarnceProvider.CustomReportValueType = 1;
			comboBoxMedicalInsuarnceProvider.DescriptionTextBox = textBoxMedicalInsuranceProvider;
			appearance200.BackColor = System.Drawing.SystemColors.Window;
			appearance200.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Appearance = appearance200;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance201.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance201.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance201.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance201.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.GroupByBox.Appearance = appearance201;
			appearance202.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.GroupByBox.BandLabelAppearance = appearance202;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance203.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance203.BackColor2 = System.Drawing.SystemColors.Control;
			appearance203.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance203.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.GroupByBox.PromptAppearance = appearance203;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.MaxRowScrollRegions = 1;
			appearance204.BackColor = System.Drawing.SystemColors.Window;
			appearance204.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.ActiveCellAppearance = appearance204;
			appearance205.BackColor = System.Drawing.SystemColors.Highlight;
			appearance205.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.ActiveRowAppearance = appearance205;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance206.BackColor = System.Drawing.SystemColors.Window;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.CardAreaAppearance = appearance206;
			appearance207.BorderColor = System.Drawing.Color.Silver;
			appearance207.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.CellAppearance = appearance207;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.CellPadding = 0;
			appearance208.BackColor = System.Drawing.SystemColors.Control;
			appearance208.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance208.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance208.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance208.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.GroupByRowAppearance = appearance208;
			appearance209.TextHAlignAsString = "Left";
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.HeaderAppearance = appearance209;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance210.BackColor = System.Drawing.SystemColors.Window;
			appearance210.BorderColor = System.Drawing.Color.Silver;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.RowAppearance = appearance210;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance211.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.Override.TemplateAddRowAppearance = appearance211;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxMedicalInsuarnceProvider.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxMedicalInsuarnceProvider.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxMedicalInsuarnceProvider.Editable = true;
			comboBoxMedicalInsuarnceProvider.FilterString = "";
			comboBoxMedicalInsuarnceProvider.HasAllAccount = false;
			comboBoxMedicalInsuarnceProvider.HasCustom = false;
			comboBoxMedicalInsuarnceProvider.IsDataLoaded = false;
			comboBoxMedicalInsuarnceProvider.Location = new System.Drawing.Point(137, 26);
			comboBoxMedicalInsuarnceProvider.MaxDropDownItems = 12;
			comboBoxMedicalInsuarnceProvider.Name = "comboBoxMedicalInsuarnceProvider";
			comboBoxMedicalInsuarnceProvider.ShowInactiveItems = false;
			comboBoxMedicalInsuarnceProvider.ShowQuickAdd = true;
			comboBoxMedicalInsuarnceProvider.Size = new System.Drawing.Size(141, 20);
			comboBoxMedicalInsuarnceProvider.TabIndex = 0;
			comboBoxMedicalInsuarnceProvider.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel15.AutoSize = true;
			ultraFormattedLinkLabel15.Location = new System.Drawing.Point(256, 59);
			ultraFormattedLinkLabel15.Name = "ultraFormattedLinkLabel15";
			ultraFormattedLinkLabel15.Size = new System.Drawing.Size(71, 14);
			ultraFormattedLinkLabel15.TabIndex = 70;
			ultraFormattedLinkLabel15.TabStop = true;
			ultraFormattedLinkLabel15.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel15.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel15.Value = "Qualification :";
			appearance212.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel15.VisitedLinkAppearance = appearance212;
			ultraFormattedLinkLabel15.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel15_LinkClicked);
			textBoxAppraisalPoints.AutoCompleteCustomSource.AddRange(new string[8]
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
			textBoxAppraisalPoints.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxAppraisalPoints.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBoxAppraisalPoints.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAppraisalPoints.CustomReportFieldName = "";
			textBoxAppraisalPoints.CustomReportKey = "";
			textBoxAppraisalPoints.CustomReportValueType = 1;
			textBoxAppraisalPoints.IsComboTextBox = false;
			textBoxAppraisalPoints.IsModified = false;
			textBoxAppraisalPoints.Location = new System.Drawing.Point(530, 34);
			textBoxAppraisalPoints.MaxLength = 5;
			textBoxAppraisalPoints.Name = "textBoxAppraisalPoints";
			textBoxAppraisalPoints.ReadOnly = true;
			textBoxAppraisalPoints.Size = new System.Drawing.Size(140, 20);
			textBoxAppraisalPoints.TabIndex = 5;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(423, 38);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(87, 13);
			mmLabel4.TabIndex = 68;
			mmLabel4.Text = "Appraisal Points:";
			comboBoxQualification.Assigned = false;
			comboBoxQualification.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxQualification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxQualification.CustomReportFieldName = "";
			comboBoxQualification.CustomReportKey = "";
			comboBoxQualification.CustomReportValueType = 1;
			comboBoxQualification.DescriptionTextBox = null;
			appearance213.BackColor = System.Drawing.SystemColors.Window;
			appearance213.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxQualification.DisplayLayout.Appearance = appearance213;
			comboBoxQualification.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxQualification.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance214.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance214.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance214.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance214.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.GroupByBox.Appearance = appearance214;
			appearance215.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxQualification.DisplayLayout.GroupByBox.BandLabelAppearance = appearance215;
			comboBoxQualification.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance216.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance216.BackColor2 = System.Drawing.SystemColors.Control;
			appearance216.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance216.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxQualification.DisplayLayout.GroupByBox.PromptAppearance = appearance216;
			comboBoxQualification.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxQualification.DisplayLayout.MaxRowScrollRegions = 1;
			appearance217.BackColor = System.Drawing.SystemColors.Window;
			appearance217.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxQualification.DisplayLayout.Override.ActiveCellAppearance = appearance217;
			appearance218.BackColor = System.Drawing.SystemColors.Highlight;
			appearance218.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxQualification.DisplayLayout.Override.ActiveRowAppearance = appearance218;
			comboBoxQualification.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxQualification.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance219.BackColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.Override.CardAreaAppearance = appearance219;
			appearance220.BorderColor = System.Drawing.Color.Silver;
			appearance220.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxQualification.DisplayLayout.Override.CellAppearance = appearance220;
			comboBoxQualification.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxQualification.DisplayLayout.Override.CellPadding = 0;
			appearance221.BackColor = System.Drawing.SystemColors.Control;
			appearance221.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance221.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance221.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance221.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.Override.GroupByRowAppearance = appearance221;
			appearance222.TextHAlignAsString = "Left";
			comboBoxQualification.DisplayLayout.Override.HeaderAppearance = appearance222;
			comboBoxQualification.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxQualification.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance223.BackColor = System.Drawing.SystemColors.Window;
			appearance223.BorderColor = System.Drawing.Color.Silver;
			comboBoxQualification.DisplayLayout.Override.RowAppearance = appearance223;
			comboBoxQualification.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance224.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxQualification.DisplayLayout.Override.TemplateAddRowAppearance = appearance224;
			comboBoxQualification.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxQualification.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxQualification.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxQualification.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxQualification.Editable = true;
			comboBoxQualification.FilterString = "";
			comboBoxQualification.HasAllAccount = false;
			comboBoxQualification.HasCustom = false;
			comboBoxQualification.IsDataLoaded = false;
			comboBoxQualification.Location = new System.Drawing.Point(338, 56);
			comboBoxQualification.MaxDropDownItems = 12;
			comboBoxQualification.Name = "comboBoxQualification";
			comboBoxQualification.ShowInactiveItems = false;
			comboBoxQualification.ShowQuickAdd = true;
			comboBoxQualification.Size = new System.Drawing.Size(117, 20);
			comboBoxQualification.TabIndex = 7;
			comboBoxQualification.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel16);
			ultraGroupBox2.Controls.Add(comboBoxAnalysis);
			ultraGroupBox2.Controls.Add(textBoxVisaNumber);
			ultraGroupBox2.Controls.Add(mmLabel24);
			ultraGroupBox2.Controls.Add(textBoxUID);
			ultraGroupBox2.Controls.Add(mmLabel22);
			ultraGroupBox2.Controls.Add(mmLabel2);
			ultraGroupBox2.Controls.Add(comboBoxAccount);
			ultraGroupBox2.Controls.Add(textBoxIBAN);
			ultraGroupBox2.Controls.Add(textBoxAccountName);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel6);
			ultraGroupBox2.Controls.Add(comboBoxBank);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel10);
			ultraGroupBox2.Controls.Add(textBoxBankName);
			ultraGroupBox2.Location = new System.Drawing.Point(7, 127);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(687, 142);
			ultraGroupBox2.TabIndex = 13;
			ultraGroupBox2.Text = "Bank Details";
			ultraFormattedLinkLabel16.AutoSize = true;
			ultraFormattedLinkLabel16.Location = new System.Drawing.Point(6, 93);
			ultraFormattedLinkLabel16.Name = "ultraFormattedLinkLabel16";
			ultraFormattedLinkLabel16.Size = new System.Drawing.Size(78, 14);
			ultraFormattedLinkLabel16.TabIndex = 77;
			ultraFormattedLinkLabel16.TabStop = true;
			ultraFormattedLinkLabel16.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.Never;
			ultraFormattedLinkLabel16.Value = "Analysis Code:";
			appearance225.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel16.VisitedLinkAppearance = appearance225;
			ultraFormattedLinkLabel16.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel16_LinkClicked);
			comboBoxAnalysis.Assigned = false;
			comboBoxAnalysis.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAnalysis.CustomReportFieldName = "";
			comboBoxAnalysis.CustomReportKey = "";
			comboBoxAnalysis.CustomReportValueType = 1;
			comboBoxAnalysis.DescriptionTextBox = null;
			appearance226.BackColor = System.Drawing.SystemColors.Window;
			appearance226.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAnalysis.DisplayLayout.Appearance = appearance226;
			comboBoxAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance227.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance227.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance227.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance227.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.GroupByBox.Appearance = appearance227;
			appearance228.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance228;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance229.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance229.BackColor2 = System.Drawing.SystemColors.Control;
			appearance229.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance229.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance229;
			comboBoxAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance230.BackColor = System.Drawing.SystemColors.Window;
			appearance230.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance230;
			appearance231.BackColor = System.Drawing.SystemColors.Highlight;
			appearance231.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance231;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance232.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance232;
			appearance233.BorderColor = System.Drawing.Color.Silver;
			appearance233.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAnalysis.DisplayLayout.Override.CellAppearance = appearance233;
			comboBoxAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance234.BackColor = System.Drawing.SystemColors.Control;
			appearance234.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance234.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance234.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance234.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance234;
			appearance235.TextHAlignAsString = "Left";
			comboBoxAnalysis.DisplayLayout.Override.HeaderAppearance = appearance235;
			comboBoxAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance236.BackColor = System.Drawing.SystemColors.Window;
			appearance236.BorderColor = System.Drawing.Color.Silver;
			comboBoxAnalysis.DisplayLayout.Override.RowAppearance = appearance236;
			comboBoxAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance237.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance237;
			comboBoxAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAnalysis.Editable = true;
			comboBoxAnalysis.FilterString = "";
			comboBoxAnalysis.HasAllAccount = false;
			comboBoxAnalysis.HasCustom = false;
			comboBoxAnalysis.IsDataLoaded = false;
			comboBoxAnalysis.Location = new System.Drawing.Point(88, 90);
			comboBoxAnalysis.MaxDropDownItems = 12;
			comboBoxAnalysis.Name = "comboBoxAnalysis";
			comboBoxAnalysis.ReadOnly = true;
			comboBoxAnalysis.ShowInactiveItems = false;
			comboBoxAnalysis.ShowQuickAdd = true;
			comboBoxAnalysis.Size = new System.Drawing.Size(156, 20);
			comboBoxAnalysis.TabIndex = 5;
			comboBoxAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxVisaNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxVisaNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxVisaNumber.BackColor = System.Drawing.Color.White;
			textBoxVisaNumber.CustomReportFieldName = "";
			textBoxVisaNumber.CustomReportKey = "";
			textBoxVisaNumber.CustomReportValueType = 1;
			textBoxVisaNumber.IsComboTextBox = false;
			textBoxVisaNumber.IsModified = false;
			textBoxVisaNumber.Location = new System.Drawing.Point(391, 112);
			textBoxVisaNumber.MaxLength = 20;
			textBoxVisaNumber.Name = "textBoxVisaNumber";
			textBoxVisaNumber.Size = new System.Drawing.Size(202, 20);
			textBoxVisaNumber.TabIndex = 7;
			mmLabel24.AutoSize = true;
			mmLabel24.BackColor = System.Drawing.Color.Transparent;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel24.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(315, 115);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(70, 13);
			mmLabel24.TabIndex = 74;
			mmLabel24.Text = "Visa Number:";
			textBoxUID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxUID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxUID.BackColor = System.Drawing.Color.White;
			textBoxUID.CustomReportFieldName = "";
			textBoxUID.CustomReportKey = "";
			textBoxUID.CustomReportValueType = 1;
			textBoxUID.IsComboTextBox = false;
			textBoxUID.IsModified = false;
			textBoxUID.Location = new System.Drawing.Point(88, 112);
			textBoxUID.MaxLength = 20;
			textBoxUID.Name = "textBoxUID";
			textBoxUID.Size = new System.Drawing.Size(226, 20);
			textBoxUID.TabIndex = 6;
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(7, 116);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(29, 13);
			mmLabel22.TabIndex = 72;
			mmLabel22.Text = "UID:";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(7, 26);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(43, 13);
			mmLabel2.TabIndex = 67;
			mmLabel2.Text = "IBAN#:";
			comboBoxAccount.Assigned = false;
			comboBoxAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccount.CustomReportFieldName = "";
			comboBoxAccount.CustomReportKey = "";
			comboBoxAccount.CustomReportValueType = 1;
			comboBoxAccount.DescriptionTextBox = null;
			appearance238.BackColor = System.Drawing.SystemColors.Window;
			appearance238.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccount.DisplayLayout.Appearance = appearance238;
			comboBoxAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance239.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance239.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance239.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance239.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.GroupByBox.Appearance = appearance239;
			appearance240.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance240;
			comboBoxAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance241.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance241.BackColor2 = System.Drawing.SystemColors.Control;
			appearance241.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance241.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance241;
			comboBoxAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance242.BackColor = System.Drawing.SystemColors.Window;
			appearance242.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccount.DisplayLayout.Override.ActiveCellAppearance = appearance242;
			appearance243.BackColor = System.Drawing.SystemColors.Highlight;
			appearance243.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccount.DisplayLayout.Override.ActiveRowAppearance = appearance243;
			comboBoxAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance244.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.CardAreaAppearance = appearance244;
			appearance245.BorderColor = System.Drawing.Color.Silver;
			appearance245.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccount.DisplayLayout.Override.CellAppearance = appearance245;
			comboBoxAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccount.DisplayLayout.Override.CellPadding = 0;
			appearance246.BackColor = System.Drawing.SystemColors.Control;
			appearance246.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance246.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance246.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance246.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.GroupByRowAppearance = appearance246;
			appearance247.TextHAlignAsString = "Left";
			comboBoxAccount.DisplayLayout.Override.HeaderAppearance = appearance247;
			comboBoxAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance248.BackColor = System.Drawing.SystemColors.Window;
			appearance248.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccount.DisplayLayout.Override.RowAppearance = appearance248;
			comboBoxAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance249.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance249;
			comboBoxAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAccount.Editable = true;
			comboBoxAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAccount.FilterString = "";
			comboBoxAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAccount.FilterSysDocID = "";
			comboBoxAccount.HasAllAccount = false;
			comboBoxAccount.HasCustom = false;
			comboBoxAccount.IsDataLoaded = false;
			comboBoxAccount.Location = new System.Drawing.Point(88, 69);
			comboBoxAccount.MaxDropDownItems = 12;
			comboBoxAccount.Name = "comboBoxAccount";
			comboBoxAccount.ShowInactiveItems = false;
			comboBoxAccount.ShowQuickAdd = true;
			comboBoxAccount.Size = new System.Drawing.Size(156, 20);
			comboBoxAccount.TabIndex = 3;
			comboBoxAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxIBAN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxIBAN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxIBAN.BackColor = System.Drawing.Color.White;
			textBoxIBAN.CustomReportFieldName = "";
			textBoxIBAN.CustomReportKey = "";
			textBoxIBAN.CustomReportValueType = 1;
			textBoxIBAN.IsComboTextBox = false;
			textBoxIBAN.IsModified = false;
			textBoxIBAN.Location = new System.Drawing.Point(88, 23);
			textBoxIBAN.MaxLength = 50;
			textBoxIBAN.Name = "textBoxIBAN";
			textBoxIBAN.Size = new System.Drawing.Size(226, 20);
			textBoxIBAN.TabIndex = 0;
			textBoxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccountName.CustomReportFieldName = "";
			textBoxAccountName.CustomReportKey = "";
			textBoxAccountName.CustomReportValueType = 1;
			textBoxAccountName.Enabled = false;
			textBoxAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxAccountName.IsComboTextBox = false;
			textBoxAccountName.IsModified = false;
			textBoxAccountName.Location = new System.Drawing.Point(246, 69);
			textBoxAccountName.MaxLength = 15;
			textBoxAccountName.Name = "textBoxAccountName";
			textBoxAccountName.Size = new System.Drawing.Size(347, 20);
			textBoxAccountName.TabIndex = 4;
			textBoxAccountName.TabStop = false;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(7, 73);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(47, 14);
			ultraFormattedLinkLabel6.TabIndex = 61;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Account:";
			appearance250.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance250;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			comboBoxBank.Assigned = false;
			comboBoxBank.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBank.CustomReportFieldName = "";
			comboBoxBank.CustomReportKey = "";
			comboBoxBank.CustomReportValueType = 1;
			comboBoxBank.DescriptionTextBox = textBoxBankName;
			appearance251.BackColor = System.Drawing.SystemColors.Window;
			appearance251.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBank.DisplayLayout.Appearance = appearance251;
			comboBoxBank.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBank.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance252.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance252.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance252.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance252.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.GroupByBox.Appearance = appearance252;
			appearance253.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.BandLabelAppearance = appearance253;
			comboBoxBank.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance254.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance254.BackColor2 = System.Drawing.SystemColors.Control;
			appearance254.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance254.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.PromptAppearance = appearance254;
			comboBoxBank.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBank.DisplayLayout.MaxRowScrollRegions = 1;
			appearance255.BackColor = System.Drawing.SystemColors.Window;
			appearance255.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBank.DisplayLayout.Override.ActiveCellAppearance = appearance255;
			appearance256.BackColor = System.Drawing.SystemColors.Highlight;
			appearance256.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBank.DisplayLayout.Override.ActiveRowAppearance = appearance256;
			comboBoxBank.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBank.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance257.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.CardAreaAppearance = appearance257;
			appearance258.BorderColor = System.Drawing.Color.Silver;
			appearance258.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBank.DisplayLayout.Override.CellAppearance = appearance258;
			comboBoxBank.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBank.DisplayLayout.Override.CellPadding = 0;
			appearance259.BackColor = System.Drawing.SystemColors.Control;
			appearance259.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance259.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance259.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance259.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.GroupByRowAppearance = appearance259;
			appearance260.TextHAlignAsString = "Left";
			comboBoxBank.DisplayLayout.Override.HeaderAppearance = appearance260;
			comboBoxBank.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBank.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance261.BackColor = System.Drawing.SystemColors.Window;
			appearance261.BorderColor = System.Drawing.Color.Silver;
			comboBoxBank.DisplayLayout.Override.RowAppearance = appearance261;
			comboBoxBank.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance262.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBank.DisplayLayout.Override.TemplateAddRowAppearance = appearance262;
			comboBoxBank.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBank.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBank.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBank.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBank.Editable = true;
			comboBoxBank.FilterString = "";
			comboBoxBank.HasAllAccount = false;
			comboBoxBank.HasCustom = false;
			comboBoxBank.IsDataLoaded = false;
			comboBoxBank.Location = new System.Drawing.Point(88, 46);
			comboBoxBank.MaxDropDownItems = 12;
			comboBoxBank.Name = "comboBoxBank";
			comboBoxBank.ShowInactiveItems = false;
			comboBoxBank.ShowQuickAdd = true;
			comboBoxBank.Size = new System.Drawing.Size(156, 20);
			comboBoxBank.TabIndex = 1;
			comboBoxBank.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxBankName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankName.CustomReportFieldName = "";
			textBoxBankName.CustomReportKey = "";
			textBoxBankName.CustomReportValueType = 1;
			textBoxBankName.Enabled = false;
			textBoxBankName.ForeColor = System.Drawing.Color.Black;
			textBoxBankName.IsComboTextBox = false;
			textBoxBankName.IsModified = false;
			textBoxBankName.Location = new System.Drawing.Point(246, 46);
			textBoxBankName.MaxLength = 15;
			textBoxBankName.Name = "textBoxBankName";
			textBoxBankName.Size = new System.Drawing.Size(347, 20);
			textBoxBankName.TabIndex = 2;
			textBoxBankName.TabStop = false;
			ultraFormattedLinkLabel10.AutoSize = true;
			ultraFormattedLinkLabel10.Location = new System.Drawing.Point(7, 51);
			ultraFormattedLinkLabel10.Name = "ultraFormattedLinkLabel10";
			ultraFormattedLinkLabel10.Size = new System.Drawing.Size(32, 14);
			ultraFormattedLinkLabel10.TabIndex = 64;
			ultraFormattedLinkLabel10.TabStop = true;
			ultraFormattedLinkLabel10.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel10.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel10.Value = "Bank:";
			appearance263.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.VisitedLinkAppearance = appearance263;
			ultraFormattedLinkLabel10.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel10_LinkClicked);
			textBoxLabourID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxLabourID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxLabourID.BackColor = System.Drawing.Color.White;
			textBoxLabourID.CustomReportFieldName = "";
			textBoxLabourID.CustomReportKey = "";
			textBoxLabourID.CustomReportValueType = 1;
			textBoxLabourID.IsComboTextBox = false;
			textBoxLabourID.IsModified = false;
			textBoxLabourID.Location = new System.Drawing.Point(90, 56);
			textBoxLabourID.MaxLength = 20;
			textBoxLabourID.Name = "textBoxLabourID";
			textBoxLabourID.Size = new System.Drawing.Size(158, 20);
			textBoxLabourID.TabIndex = 6;
			dateTimePickerConfirmation.Checked = false;
			dateTimePickerConfirmation.CustomFormat = " ";
			dateTimePickerConfirmation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerConfirmation.Location = new System.Drawing.Point(259, 13);
			dateTimePickerConfirmation.Name = "dateTimePickerConfirmation";
			dateTimePickerConfirmation.ShowCheckBox = true;
			dateTimePickerConfirmation.Size = new System.Drawing.Size(124, 20);
			dateTimePickerConfirmation.TabIndex = 1;
			dateTimePickerConfirmation.Value = new System.DateTime(0L);
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(7, 59);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(58, 13);
			mmLabel1.TabIndex = 65;
			mmLabel1.Text = "Labour ID:";
			textBoxSpouse.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxSpouse.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxSpouse.BackColor = System.Drawing.Color.White;
			textBoxSpouse.CustomReportFieldName = "";
			textBoxSpouse.CustomReportKey = "";
			textBoxSpouse.CustomReportValueType = 1;
			textBoxSpouse.IsComboTextBox = false;
			textBoxSpouse.IsModified = false;
			textBoxSpouse.Location = new System.Drawing.Point(90, 35);
			textBoxSpouse.MaxLength = 30;
			textBoxSpouse.Name = "textBoxSpouse";
			textBoxSpouse.Size = new System.Drawing.Size(158, 20);
			textBoxSpouse.TabIndex = 3;
			mmLabel48.AutoSize = true;
			mmLabel48.BackColor = System.Drawing.Color.Transparent;
			mmLabel48.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel48.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel48.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel48.IsFieldHeader = false;
			mmLabel48.IsRequired = false;
			mmLabel48.Location = new System.Drawing.Point(7, 37);
			mmLabel48.Name = "mmLabel48";
			mmLabel48.PenWidth = 1f;
			mmLabel48.ShowBorder = false;
			mmLabel48.Size = new System.Drawing.Size(46, 13);
			mmLabel48.TabIndex = 49;
			mmLabel48.Text = "Spouse:";
			mmLabel40.AutoSize = true;
			mmLabel40.BackColor = System.Drawing.Color.Transparent;
			mmLabel40.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel40.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel40.IsFieldHeader = false;
			mmLabel40.IsRequired = false;
			mmLabel40.Location = new System.Drawing.Point(256, 37);
			mmLabel40.Name = "mmLabel40";
			mmLabel40.PenWidth = 1f;
			mmLabel40.ShowBorder = false;
			mmLabel40.Size = new System.Drawing.Size(69, 13);
			mmLabel40.TabIndex = 42;
			mmLabel40.Text = "Blood Group:";
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
			textBoxBloodGroup.Location = new System.Drawing.Point(338, 35);
			textBoxBloodGroup.MaxLength = 5;
			textBoxBloodGroup.Name = "textBoxBloodGroup";
			textBoxBloodGroup.Size = new System.Drawing.Size(69, 20);
			textBoxBloodGroup.TabIndex = 4;
			comboBoxReligion.Assigned = false;
			comboBoxReligion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxReligion.CustomReportFieldName = "";
			comboBoxReligion.CustomReportKey = "";
			comboBoxReligion.CustomReportValueType = 1;
			comboBoxReligion.DescriptionTextBox = null;
			appearance264.BackColor = System.Drawing.SystemColors.Window;
			appearance264.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxReligion.DisplayLayout.Appearance = appearance264;
			comboBoxReligion.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxReligion.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance265.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance265.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance265.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance265.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.GroupByBox.Appearance = appearance265;
			appearance266.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReligion.DisplayLayout.GroupByBox.BandLabelAppearance = appearance266;
			comboBoxReligion.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance267.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance267.BackColor2 = System.Drawing.SystemColors.Control;
			appearance267.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance267.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReligion.DisplayLayout.GroupByBox.PromptAppearance = appearance267;
			comboBoxReligion.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxReligion.DisplayLayout.MaxRowScrollRegions = 1;
			appearance268.BackColor = System.Drawing.SystemColors.Window;
			appearance268.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxReligion.DisplayLayout.Override.ActiveCellAppearance = appearance268;
			appearance269.BackColor = System.Drawing.SystemColors.Highlight;
			appearance269.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxReligion.DisplayLayout.Override.ActiveRowAppearance = appearance269;
			comboBoxReligion.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxReligion.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance270.BackColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.Override.CardAreaAppearance = appearance270;
			appearance271.BorderColor = System.Drawing.Color.Silver;
			appearance271.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxReligion.DisplayLayout.Override.CellAppearance = appearance271;
			comboBoxReligion.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxReligion.DisplayLayout.Override.CellPadding = 0;
			appearance272.BackColor = System.Drawing.SystemColors.Control;
			appearance272.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance272.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance272.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance272.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.Override.GroupByRowAppearance = appearance272;
			appearance273.TextHAlignAsString = "Left";
			comboBoxReligion.DisplayLayout.Override.HeaderAppearance = appearance273;
			comboBoxReligion.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxReligion.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance274.BackColor = System.Drawing.SystemColors.Window;
			appearance274.BorderColor = System.Drawing.Color.Silver;
			comboBoxReligion.DisplayLayout.Override.RowAppearance = appearance274;
			comboBoxReligion.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance275.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxReligion.DisplayLayout.Override.TemplateAddRowAppearance = appearance275;
			comboBoxReligion.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxReligion.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxReligion.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxReligion.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxReligion.Editable = true;
			comboBoxReligion.FilterString = "";
			comboBoxReligion.HasAllAccount = false;
			comboBoxReligion.HasCustom = false;
			comboBoxReligion.IsDataLoaded = false;
			comboBoxReligion.Location = new System.Drawing.Point(475, 13);
			comboBoxReligion.MaxDropDownItems = 12;
			comboBoxReligion.Name = "comboBoxReligion";
			comboBoxReligion.ShowInactiveItems = false;
			comboBoxReligion.ShowQuickAdd = true;
			comboBoxReligion.Size = new System.Drawing.Size(195, 20);
			comboBoxReligion.TabIndex = 2;
			comboBoxReligion.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(177, 15);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(72, 13);
			mmLabel38.TabIndex = 40;
			mmLabel38.Text = "Confirmation:";
			textBoxProbation.AllowDecimal = false;
			textBoxProbation.CustomReportFieldName = "";
			textBoxProbation.CustomReportKey = "";
			textBoxProbation.CustomReportValueType = 1;
			textBoxProbation.IsComboTextBox = false;
			textBoxProbation.IsModified = false;
			textBoxProbation.Location = new System.Drawing.Point(90, 13);
			textBoxProbation.MaxLength = 5;
			textBoxProbation.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxProbation.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxProbation.Name = "textBoxProbation";
			textBoxProbation.NullText = "0";
			textBoxProbation.Size = new System.Drawing.Size(81, 20);
			textBoxProbation.TabIndex = 0;
			textBoxProbation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel37.AutoSize = true;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(7, 15);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(57, 13);
			mmLabel37.TabIndex = 37;
			mmLabel37.Text = "Probation:";
			comboBoxDayOff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxDayOff.FormattingEnabled = true;
			comboBoxDayOff.Items.AddRange(new object[8]
			{
				"N/A",
				"Saturday",
				"Sunday",
				"Monday",
				"Tuesday",
				"Wednesday",
				"Thursday",
				"Friday"
			});
			comboBoxDayOff.Location = new System.Drawing.Point(252, 103);
			comboBoxDayOff.Name = "comboBoxDayOff";
			comboBoxDayOff.Size = new System.Drawing.Size(99, 21);
			comboBoxDayOff.TabIndex = 5;
			comboBoxDayOff.Visible = false;
			mmLabel32.AutoSize = true;
			mmLabel32.BackColor = System.Drawing.Color.Transparent;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel32.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(198, 107);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(49, 13);
			mmLabel32.TabIndex = 30;
			mmLabel32.Text = "Day Off:";
			mmLabel32.Visible = false;
			ultraFormattedLinkLabel9.AutoSize = true;
			ultraFormattedLinkLabel9.Location = new System.Drawing.Point(398, 15);
			ultraFormattedLinkLabel9.Name = "ultraFormattedLinkLabel9";
			ultraFormattedLinkLabel9.Size = new System.Drawing.Size(47, 14);
			ultraFormattedLinkLabel9.TabIndex = 60;
			ultraFormattedLinkLabel9.TabStop = true;
			ultraFormattedLinkLabel9.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel9.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel9.Value = "Religion:";
			appearance276.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel9.VisitedLinkAppearance = appearance276;
			ultraFormattedLinkLabel9.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel9_LinkClicked);
			ultraTabPageControl2.Controls.Add(ultraGroupBox1);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(696, 420);
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(mmLabel23);
			ultraGroupBox1.Controls.Add(textBoxComment);
			ultraGroupBox1.Controls.Add(buttonMoreAddress);
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
			ultraGroupBox1.Location = new System.Drawing.Point(1, 16);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(687, 199);
			ultraGroupBox1.TabIndex = 10;
			ultraGroupBox1.Text = "Primary Address";
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(371, 132);
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
			textBoxComment.Location = new System.Drawing.Point(442, 129);
			textBoxComment.MaxLength = 255;
			textBoxComment.Name = "textBoxComment";
			textBoxComment.Size = new System.Drawing.Size(229, 20);
			textBoxComment.TabIndex = 13;
			buttonMoreAddress.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonMoreAddress.BackColor = System.Drawing.Color.DarkGray;
			buttonMoreAddress.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonMoreAddress.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonMoreAddress.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonMoreAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonMoreAddress.Location = new System.Drawing.Point(537, 155);
			buttonMoreAddress.Name = "buttonMoreAddress";
			buttonMoreAddress.Size = new System.Drawing.Size(134, 24);
			buttonMoreAddress.TabIndex = 14;
			buttonMoreAddress.Text = "More Addresses...";
			buttonMoreAddress.UseVisualStyleBackColor = false;
			buttonMoreAddress.Click += new System.EventHandler(buttonMoreAddress_Click);
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(7, 176);
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
			textBoxPostalCode.Location = new System.Drawing.Point(132, 173);
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
			mmLabel18.Location = new System.Drawing.Point(371, 110);
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
			textBoxEmail.Location = new System.Drawing.Point(442, 107);
			textBoxEmail.MaxLength = 30;
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
			mmLabel17.Location = new System.Drawing.Point(371, 88);
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
			textBoxMobile.Location = new System.Drawing.Point(442, 85);
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
			mmLabel16.Location = new System.Drawing.Point(371, 65);
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
			textBoxFax.Location = new System.Drawing.Point(442, 63);
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
			mmLabel15.Location = new System.Drawing.Point(371, 44);
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
			textBoxPhone2.Location = new System.Drawing.Point(442, 41);
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
			mmLabel14.Location = new System.Drawing.Point(371, 22);
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
			textBoxPhone1.Location = new System.Drawing.Point(442, 19);
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
			mmLabel12.Location = new System.Drawing.Point(7, 154);
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
			textBoxCountry.Location = new System.Drawing.Point(132, 151);
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
			mmLabel11.Location = new System.Drawing.Point(7, 132);
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
			textBoxState.Location = new System.Drawing.Point(132, 129);
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
			mmLabel13.Location = new System.Drawing.Point(7, 109);
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
			textBoxCity.Location = new System.Drawing.Point(132, 107);
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
			textBoxAddress3.Location = new System.Drawing.Point(132, 85);
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
			textBoxAddress2.Location = new System.Drawing.Point(132, 63);
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
			mmLabel10.Location = new System.Drawing.Point(7, 43);
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
			textBoxAddress1.Location = new System.Drawing.Point(132, 41);
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
			mmLabel8.Location = new System.Drawing.Point(7, 22);
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
			textBoxAddressID.Location = new System.Drawing.Point(132, 19);
			textBoxAddressID.MaxLength = 15;
			textBoxAddressID.Name = "textBoxAddressID";
			textBoxAddressID.Size = new System.Drawing.Size(229, 20);
			textBoxAddressID.TabIndex = 0;
			textBoxAddressID.Text = "PRIMARY";
			ultraTabPageControl1.Controls.Add(textBoxNote);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(696, 420);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(10, 14);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(674, 392);
			textBoxNote.TabIndex = 21;
			tabPageUserDefined.Controls.Add(udfEntryGrid);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(696, 420);
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(7, 3);
			udfEntryGrid.Margin = new System.Windows.Forms.Padding(4);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(683, 413);
			udfEntryGrid.TabIndex = 1;
			udfEntryGrid.TableName = "";
			udfEntryGrid.Load += new System.EventHandler(udfEntryGrid_Load);
			panelButtons.Controls.Add(buttonProject);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 500);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(700, 41);
			panelButtons.TabIndex = 1;
			buttonProject.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonProject.BackColor = System.Drawing.Color.DarkGray;
			buttonProject.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonProject.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonProject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonProject.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonProject.Location = new System.Drawing.Point(319, 8);
			buttonProject.Name = "buttonProject";
			buttonProject.Size = new System.Drawing.Size(96, 24);
			buttonProject.TabIndex = 16;
			buttonProject.Text = "Project";
			buttonProject.UseVisualStyleBackColor = false;
			buttonProject.Visible = false;
			buttonProject.Click += new System.EventHandler(buttonProject_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(700, 1);
			linePanelDown.TabIndex = 14;
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
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.BackColor = System.Drawing.Color.DarkGray;
			buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClose.Location = new System.Drawing.Point(590, 8);
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
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
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
				toolStripButton1,
				toolStripButtonShowPicture,
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(700, 27);
			toolStrip1.TabIndex = 306;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(24, 24);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(24, 24);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(24, 24);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(24, 24);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(24, 24);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 27);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(54, 24);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
			toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				dependentsToolStripMenuItem1,
				documentsToolStripMenuItem1,
				skillsToolStripMenuItem1,
				salaryDetailsToolStripMenuItem
			});
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.salesperson;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(77, 24);
			toolStripButton1.Text = "More...";
			dependentsToolStripMenuItem1.Name = "dependentsToolStripMenuItem1";
			dependentsToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
			dependentsToolStripMenuItem1.Text = "Dependents";
			dependentsToolStripMenuItem1.Click += new System.EventHandler(dependentsToolStripMenuItem_Click);
			documentsToolStripMenuItem1.Name = "documentsToolStripMenuItem1";
			documentsToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
			documentsToolStripMenuItem1.Text = "Documents";
			documentsToolStripMenuItem1.Click += new System.EventHandler(documentsToolStripMenuItem_Click);
			skillsToolStripMenuItem1.Name = "skillsToolStripMenuItem1";
			skillsToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
			skillsToolStripMenuItem1.Text = "Skills";
			skillsToolStripMenuItem1.Click += new System.EventHandler(skillsToolStripMenuItem_Click);
			salaryDetailsToolStripMenuItem.Name = "salaryDetailsToolStripMenuItem";
			salaryDetailsToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			salaryDetailsToolStripMenuItem.Text = "Salary Details";
			salaryDetailsToolStripMenuItem.Click += new System.EventHandler(salaryDetailsToolStripMenuItem_Click);
			toolStripButtonShowPicture.CheckOnClick = true;
			toolStripButtonShowPicture.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonShowPicture.Image");
			toolStripButtonShowPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowPicture.Name = "toolStripButtonShowPicture";
			toolStripButtonShowPicture.Size = new System.Drawing.Size(100, 24);
			toolStripButtonShowPicture.Text = "Show Picture";
			toolStripButtonShowPicture.ToolTipText = "Auto load pictures";
			toolStripButtonShowPicture.Click += new System.EventHandler(toolStripButtonShowPicture_Click);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(87, 24);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
			toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(24, 24);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(24, 24);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(24, 24);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panel1.Controls.Add(labelCustomerNameHeader);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 27);
			panel1.MinimumSize = new System.Drawing.Size(0, 8);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(700, 30);
			panel1.TabIndex = 314;
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
			contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
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
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(696, 420);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 57);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(700, 443);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			appearance277.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance277;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Details";
			ultraTab3.TabPage = ultraTabPageControl2;
			ultraTab3.Text = "&Address";
			ultraTab4.TabPage = ultraTabPageControl1;
			ultraTab4.Text = "Note";
			ultraTab5.TabPage = tabPageUserDefined;
			ultraTab5.Text = "&User Defined";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[5]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5
			});
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
			base.ClientSize = new System.Drawing.Size(700, 541);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "EmployeeDetailsForm";
			Text = "Employee Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(EmployeeClassDetailsForm_FormClosing);
			base.Load += new System.EventHandler(EmployeeDetailsForm_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCompanyDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManager).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPosition).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepartment).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGrade).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGroup).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLegalPosition).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVisaDesignation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRecruitmentChannel).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxHolidayCalendar).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			ultraGroupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxMedicalInsuarnceProvider).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxQualification).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReligion).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			AddEvents();
			comboBoxDayOff.LoadData();
			comboBoxGender.LoadData();
			comboBoxMaritalStatus.LoadData();
			comboBoxStatus.LoadData();
			companyInformation = Factory.CompanyInformationSystem.GetCompanyInformation();
			comboBoxAnalysis.FilterByAccount(companyInformation.Tables[0].Rows[0]["HRAnalysisGroup"].ToString());
		}

		private void AddEvents()
		{
			FormActivator.EmployeeAddressDetailsFormObj.EmployeeAddressChanged += EventHelper_EmployeeAddressChanged;
			comboBoxAccount.SelectedIndexChanged += comboBoxAccount_SelectedIndexChanged;
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxLastName.TextChanged += textBoxLastName_TextChanged;
			textBoxFirstName.TextChanged += textBoxName_TextChanged;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
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

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			SetHeaderName();
		}

		private void SetHeaderName()
		{
			labelCustomerNameHeader.Text = textBoxCode.Text + " - " + textBoxFirstName.Text + " " + textBoxLastName.Text;
			if (textBoxCode.Text.Trim() == "" && textBoxFirstName.Text.Trim() == "" && textBoxLastName.Text == "")
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
			textBoxAccountName.Text = comboBoxAccount.SelectedName;
		}

		private void EventHelper_EmployeeAddressChanged(object sender, EventArgs e)
		{
			DataSet dataSet = sender as DataSet;
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				if (dataRow["EmployeeID"].ToString() == textBoxCode.Text && dataRow["AddressID"].ToString() == textBoxAddressID.Text && !isNewRecord)
				{
					FillAddressData(dataRow);
				}
			}
		}

		private void EmployeeDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
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
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.EmployeeTable.Rows[0];
			textBoxCode.Text = dataRow["EmployeeID"].ToString();
			textBoxFirstName.Text = dataRow["FirstName"].ToString();
			textBoxLastName.Text = dataRow["LastName"].ToString();
			textBoxMiddleName.Text = dataRow["MiddleName"].ToString();
			textBoxNickName.Text = dataRow["NickName"].ToString();
			textBoxNationalID.Text = dataRow["NationalID"].ToString();
			comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
			comboBoxDivision.SelectedID = dataRow["DivisionID"].ToString();
			comboBoxCompanyDivision.SelectedID = dataRow["CompanyDivisionID"].ToString();
			comboBoxDepartment.SelectedID = dataRow["DepartmentID"].ToString();
			comboBoxLegalPosition.SelectedID = dataRow["LegalPositionID"].ToString();
			if (dataRow["Status"] != DBNull.Value)
			{
				comboBoxStatus.SelectedID = int.Parse(dataRow["Status"].ToString());
			}
			else
			{
				comboBoxStatus.SelectedID = -1;
			}
			if (dataRow["ContractType"] != DBNull.Value)
			{
				comboBoxType.SelectedID = dataRow["ContractType"].ToString();
			}
			else
			{
				comboBoxType.Clear();
			}
			comboBoxGroup.SelectedID = dataRow["GroupID"].ToString();
			comboBoxGrade.SelectedID = dataRow["GradeID"].ToString();
			comboBoxPosition.SelectedID = dataRow["PositionID"].ToString();
			comboBoxManager.SelectedID = dataRow["ReportToID"].ToString();
			if (dataRow["JoiningDate"] != DBNull.Value)
			{
				dateTimePickerJoiningDate.Value = DateTime.Parse(dataRow["JoiningDate"].ToString());
				dateTimePickerJoiningDate.Checked = true;
			}
			else
			{
				dateTimePickerJoiningDate.Clear();
			}
			textBoxAddressID.Text = dataRow["PrimaryAddressID"].ToString();
			if (dataRow["HasPhoto"] != DBNull.Value)
			{
				bool flag = bool.Parse(dataRow["HasPhoto"].ToString());
				linkLoadImage.Visible = flag;
				linkRemovePicture.Enabled = flag;
				if (flag)
				{
					pictureBoxPhoto.Image = null;
				}
				else
				{
					pictureBoxPhoto.Image = pictureBoxNoImage.Image;
				}
			}
			else
			{
				linkLoadImage.Visible = false;
				pictureBoxPhoto.Image = pictureBoxNoImage.Image;
				linkRemovePicture.Enabled = false;
			}
			if (dataRow["Gender"] != DBNull.Value)
			{
				comboBoxGender.SelectedGender = char.Parse(dataRow["Gender"].ToString());
			}
			if (dataRow["BirthDate"] != DBNull.Value)
			{
				dateTimePickerBirthDate.Value = DateTime.Parse(dataRow["BirthDate"].ToString());
				dateTimePickerBirthDate.Checked = true;
			}
			else
			{
				dateTimePickerBirthDate.Checked = false;
			}
			if (dataRow["RecruitmentChannelID"] != DBNull.Value)
			{
				comboBoxRecruitmentChannel.SelectedID = dataRow["RecruitmentChannelID"].ToString();
			}
			else
			{
				comboBoxRecruitmentChannel.SelectedID = "";
			}
			if (dataRow["VisaDesignationID"] != DBNull.Value)
			{
				comboBoxVisaDesignation.SelectedID = dataRow["VisaDesignationID"].ToString();
			}
			else
			{
				comboBoxVisaDesignation.SelectedID = "";
			}
			textBoxBirthPlace.Text = dataRow["BirthPlace"].ToString();
			comboBoxSponsor.SelectedID = dataRow["SponsorID"].ToString();
			textBoxProbation.Text = dataRow["Probation"].ToString();
			if (dataRow["ConfirmationDate"] != DBNull.Value)
			{
				dateTimePickerConfirmation.Value = DateTime.Parse(dataRow["ConfirmationDate"].ToString());
				dateTimePickerConfirmation.Checked = true;
			}
			else
			{
				dateTimePickerConfirmation.Checked = false;
			}
			if (dataRow["CalendarID"] != DBNull.Value)
			{
				comboBoxHolidayCalendar.SelectedID = dataRow["CalendarID"].ToString();
			}
			else
			{
				comboBoxHolidayCalendar.Clear();
			}
			if (dataRow["EmpAnalysisID"] != DBNull.Value)
			{
				comboBoxAnalysis.SelectedID = dataRow["EmpAnalysisID"].ToString();
			}
			else
			{
				comboBoxAnalysis.Clear();
			}
			comboBoxNationality.SelectedID = dataRow["NationalityID"].ToString();
			textBoxSpouse.Text = dataRow["SpouseName"].ToString();
			if (dataRow["MaritalStatus"] != DBNull.Value)
			{
				comboBoxMaritalStatus.SelectedID = int.Parse(dataRow["MaritalStatus"].ToString());
			}
			else
			{
				comboBoxMaritalStatus.SelectedID = -1;
			}
			comboBoxReligion.SelectedID = dataRow["ReligionID"].ToString();
			textBoxBloodGroup.Text = dataRow["BloodGroup"].ToString();
			comboBoxQualification.SelectedID = dataRow["Qualification"].ToString();
			if (dataRow["DayOff"] != DBNull.Value)
			{
				comboBoxDayOff.SelectedID = int.Parse(dataRow["DayOff"].ToString());
			}
			else
			{
				comboBoxDayOff.SelectedID = -1;
			}
			textBoxNote.Text = dataRow["Notes"].ToString();
			comboBoxAccount.SelectedID = dataRow["AccountID"].ToString();
			comboBoxBank.SelectedID = dataRow["BankID"].ToString();
			textBoxLabourID.Text = dataRow["LabourID"].ToString();
			textBoxIBAN.Text = dataRow["IBAN"].ToString();
			textBoxUID.Text = dataRow["UID"].ToString();
			textBoxVisaNumber.Text = dataRow["VisaNumber"].ToString();
			textBoxAppraisalPoints.Text = dataRow["AppriasalPoints"].ToString();
			if (dataRow["MedicalInsValidFrom"] != DBNull.Value)
			{
				dateTimePickerValidFrom.Value = DateTime.Parse(dataRow["MedicalInsValidFrom"].ToString());
				dateTimePickerValidFrom.Checked = true;
			}
			else
			{
				dateTimePickerValidFrom.Checked = false;
			}
			if (dataRow["MedicalInsValidTo"] != DBNull.Value)
			{
				datetimePickerValidTo.Value = DateTime.Parse(dataRow["MedicalInsValidTo"].ToString());
				datetimePickerValidTo.Checked = true;
			}
			else
			{
				datetimePickerValidTo.Checked = false;
			}
			if (dataRow["MedicalInsuranceProviderID"] != DBNull.Value)
			{
				comboBoxMedicalInsuarnceProvider.SelectedID = dataRow["MedicalInsuranceProviderID"].ToString();
			}
			else
			{
				comboBoxDayOff.SelectedID = -1;
			}
			if (dataRow["MedicalInsuranceCategoryID"] != DBNull.Value)
			{
				comboBoxInsuranceCategory.SelectedID = dataRow["MedicalInsuranceCategoryID"].ToString();
			}
			else
			{
				comboBoxInsuranceCategory.SelectedID = "";
			}
			textBoxFamilyMembersCount.Text = dataRow["NumberOfDependants"].ToString();
			textBoxInsuranceNumber.Text = dataRow["MedicalInsuranceNumber"].ToString();
			textBoxServicePeriod.Text = dataRow["ServicePeriodMonth"].ToString();
			if (dataRow["MedicalInsuranceAmount"] != DBNull.Value)
			{
				textBoxInsuranceAmount.Text = decimal.Parse(dataRow["MedicalInsuranceAmount"].ToString()).ToString(Format.TotalAmountFormat);
			}
			else
			{
				textBoxInsuranceAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			SetHeaderName();
			if (currentData.Tables.Contains("Employee_Address") && currentData.EmployeeAddressTable.Rows.Count > 0)
			{
				dataRow = currentData.EmployeeAddressTable.Rows[0];
				FillAddressData(dataRow);
			}
			else
			{
				MMTextBox mMTextBox = textBoxAddressID;
				MMTextBox mMTextBox2 = textBoxAddress1;
				MMTextBox mMTextBox3 = textBoxAddress2;
				MMTextBox mMTextBox4 = textBoxAddress3;
				MMTextBox mMTextBox5 = textBoxCity;
				MMTextBox mMTextBox6 = textBoxState;
				MMTextBox mMTextBox7 = textBoxCountry;
				MMTextBox mMTextBox8 = textBoxPostalCode;
				MMTextBox mMTextBox9 = textBoxPhone1;
				MMTextBox mMTextBox10 = textBoxPhone2;
				MMTextBox mMTextBox11 = textBoxFax;
				MMTextBox mMTextBox12 = textBoxMobile;
				MMTextBox mMTextBox13 = textBoxEmail;
				string text2 = textBoxComment.Text = "";
				string text4 = mMTextBox13.Text = text2;
				string text6 = mMTextBox12.Text = text4;
				string text8 = mMTextBox11.Text = text6;
				string text10 = mMTextBox10.Text = text8;
				string text12 = mMTextBox9.Text = text10;
				string text14 = mMTextBox8.Text = text12;
				string text16 = mMTextBox7.Text = text14;
				string text18 = mMTextBox6.Text = text16;
				string text20 = mMTextBox5.Text = text18;
				string text22 = mMTextBox4.Text = text20;
				string text24 = mMTextBox3.Text = text22;
				string text27 = mMTextBox.Text = (mMTextBox2.Text = text24);
			}
			if (currentData.Tables.Contains("UDF"))
			{
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
			}
		}

		private void FillAddressData(DataRow row)
		{
			textBoxAddressID.Text = row["AddressID"].ToString();
			textBoxAddress1.Text = row["Address1"].ToString();
			textBoxAddress2.Text = row["Address2"].ToString();
			textBoxAddress3.Text = row["Address3"].ToString();
			textBoxCity.Text = row["City"].ToString();
			textBoxState.Text = row["State"].ToString();
			textBoxCountry.Text = row["Country"].ToString();
			textBoxPostalCode.Text = row["PostalCode"].ToString();
			textBoxPhone1.Text = row["Phone1"].ToString();
			textBoxPhone2.Text = row["Phone2"].ToString();
			textBoxFax.Text = row["Fax"].ToString();
			textBoxMobile.Text = row["Mobile"].ToString();
			textBoxEmail.Text = row["Email"].ToString();
			textBoxComment.Text = row["Comment"].ToString();
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeData();
				}
				string text = "";
				string text2 = "";
				string text3 = "";
				AnalysisData analysisData = new AnalysisData();
				if (isNewRecord && EnableHRAnalysis)
				{
					companyInformation = Factory.CompanyInformationSystem.GetCompanyInformation();
					text = companyInformation.Tables[0].Rows[0]["HRAnalysisGroup"].ToString();
					text2 = companyInformation.Tables[0].Rows[0]["HRAnalysisPrefix"].ToString();
					if (string.IsNullOrEmpty(text2) || string.IsNullOrWhiteSpace(text2))
					{
						ErrorHelper.WarningMessage("Set AnalysisPrefix on Company settings");
					}
					text3 = text2 + textBoxCode.Text;
					DataRow dataRow = analysisData.AnalysisTable.NewRow();
					dataRow["AnalysisID"] = text3;
					dataRow["AnalysisName"] = textBoxFirstName.Text;
					dataRow["GroupID"] = text;
					dataRow.EndEdit();
					analysisData.AnalysisTable.Rows.Add(dataRow);
				}
				DataRow dataRow2 = (!isNewRecord) ? currentData.EmployeeTable.Rows[0] : currentData.EmployeeTable.NewRow();
				dataRow2.BeginEdit();
				dataRow2["EmployeeID"] = textBoxCode.Text;
				dataRow2["FirstName"] = textBoxFirstName.Text;
				dataRow2["LastName"] = textBoxLastName.Text;
				dataRow2["MiddleName"] = textBoxMiddleName.Text;
				dataRow2["NickName"] = textBoxNickName.Text;
				dataRow2["NationalID"] = textBoxNationalID.Text;
				if (comboBoxLocation.SelectedID != "")
				{
					dataRow2["LocationID"] = comboBoxLocation.SelectedID;
				}
				else
				{
					dataRow2["LocationID"] = DBNull.Value;
				}
				if (comboBoxDivision.SelectedID != "")
				{
					dataRow2["DivisionID"] = comboBoxDivision.SelectedID;
				}
				else
				{
					dataRow2["DivisionID"] = DBNull.Value;
				}
				if (comboBoxCompanyDivision.SelectedID != "")
				{
					dataRow2["CompanyDivisionID"] = comboBoxCompanyDivision.SelectedID;
				}
				else
				{
					dataRow2["CompanyDivisionID"] = DBNull.Value;
				}
				if (comboBoxAccount.SelectedID != "")
				{
					dataRow2["AccountID"] = comboBoxAccount.SelectedID;
				}
				else
				{
					dataRow2["AccountID"] = DBNull.Value;
				}
				if (comboBoxAnalysis.SelectedID != "")
				{
					dataRow2["EmpAnalysisID"] = comboBoxAnalysis.SelectedID;
				}
				else if (text3 != "")
				{
					dataRow2["EmpAnalysisID"] = text3;
				}
				else
				{
					dataRow2["EmpAnalysisID"] = DBNull.Value;
				}
				if (comboBoxBank.SelectedID != "")
				{
					dataRow2["BankID"] = comboBoxBank.SelectedID;
				}
				else
				{
					dataRow2["BankID"] = DBNull.Value;
				}
				if (comboBoxDepartment.SelectedID != "")
				{
					dataRow2["DepartmentID"] = comboBoxDepartment.SelectedID;
				}
				else
				{
					dataRow2["DepartmentID"] = DBNull.Value;
				}
				if (comboBoxStatus.SelectedID > 0)
				{
					dataRow2["Status"] = comboBoxStatus.SelectedID;
				}
				else
				{
					dataRow2["Status"] = 1;
				}
				if (comboBoxType.SelectedID != "")
				{
					dataRow2["ContractType"] = comboBoxType.SelectedID;
				}
				else
				{
					dataRow2["ContractType"] = DBNull.Value;
				}
				if (comboBoxGroup.SelectedID != "")
				{
					dataRow2["GroupID"] = comboBoxGroup.SelectedID;
				}
				else
				{
					dataRow2["GroupID"] = DBNull.Value;
				}
				if (comboBoxGrade.SelectedID != "")
				{
					dataRow2["GradeID"] = comboBoxGrade.SelectedID;
				}
				else
				{
					dataRow2["GradeID"] = DBNull.Value;
				}
				if (comboBoxPosition.SelectedID != "")
				{
					dataRow2["PositionID"] = comboBoxPosition.SelectedID;
				}
				else
				{
					dataRow2["PositionID"] = DBNull.Value;
				}
				if (comboBoxManager.SelectedID != "")
				{
					dataRow2["ReportToID"] = comboBoxManager.SelectedID;
				}
				else
				{
					dataRow2["ReportToID"] = DBNull.Value;
				}
				if (dateTimePickerJoiningDate.Checked)
				{
					dataRow2["JoiningDate"] = dateTimePickerJoiningDate.Value;
				}
				else
				{
					dataRow2["JoiningDate"] = DBNull.Value;
				}
				if (string.IsNullOrWhiteSpace(textBoxAddressID.Text))
				{
					dataRow2["PrimaryAddressID"] = "PRIMARY";
				}
				else
				{
					dataRow2["PrimaryAddressID"] = textBoxAddressID.Text;
				}
				if (comboBoxGender.SelectedID.ToString() != "")
				{
					dataRow2["Gender"] = comboBoxGender.SelectedGender;
				}
				else
				{
					dataRow2["Gender"] = 'M';
				}
				if (dateTimePickerBirthDate.Checked)
				{
					dataRow2["BirthDate"] = dateTimePickerBirthDate.Value;
				}
				else
				{
					dataRow2["BirthDate"] = DBNull.Value;
				}
				dataRow2["BirthPlace"] = textBoxBirthPlace.Text;
				if (comboBoxSponsor.SelectedID != "")
				{
					dataRow2["SponsorID"] = comboBoxSponsor.SelectedID;
				}
				else
				{
					dataRow2["SponsorID"] = DBNull.Value;
				}
				if (textBoxProbation.Text != "")
				{
					dataRow2["Probation"] = textBoxProbation.Text;
				}
				else
				{
					dataRow2["Probation"] = 0;
				}
				if (dateTimePickerConfirmation.Checked)
				{
					dataRow2["ConfirmationDate"] = dateTimePickerConfirmation.Value;
				}
				else
				{
					dataRow2["ConfirmationDate"] = DBNull.Value;
				}
				if (comboBoxNationality.SelectedID != "")
				{
					dataRow2["NationalityID"] = comboBoxNationality.SelectedID;
				}
				else
				{
					dataRow2["NationalityID"] = DBNull.Value;
				}
				dataRow2["SpouseName"] = textBoxSpouse.Text;
				dataRow2["UID"] = textBoxUID.Text;
				dataRow2["VisaNumber"] = textBoxVisaNumber.Text;
				if (comboBoxMaritalStatus.SelectedID > 0)
				{
					dataRow2["MaritalStatus"] = comboBoxMaritalStatus.SelectedID;
				}
				else
				{
					dataRow2["MaritalStatus"] = DBNull.Value;
				}
				if (comboBoxReligion.SelectedID != "")
				{
					dataRow2["ReligionID"] = comboBoxReligion.SelectedID;
				}
				else
				{
					dataRow2["ReligionID"] = DBNull.Value;
				}
				dataRow2["BloodGroup"] = textBoxBloodGroup.Text;
				dataRow2["Qualification"] = comboBoxQualification.SelectedID;
				if (comboBoxDayOff.SelectedID > 0)
				{
					dataRow2["DayOff"] = comboBoxDayOff.SelectedID;
				}
				else
				{
					dataRow2["DayOff"] = DBNull.Value;
				}
				if (comboBoxRecruitmentChannel.SelectedID != "")
				{
					dataRow2["RecruitmentChannelID"] = comboBoxRecruitmentChannel.SelectedID;
				}
				else
				{
					dataRow2["RecruitmentChannelID"] = DBNull.Value;
				}
				if (comboBoxVisaDesignation.SelectedID != "")
				{
					dataRow2["VisaDesignationID"] = comboBoxVisaDesignation.SelectedID;
				}
				else
				{
					dataRow2["VisaDesignationID"] = DBNull.Value;
				}
				dataRow2["Notes"] = textBoxNote.Text;
				dataRow2["LegalPositionID"] = comboBoxLegalPosition.SelectedID;
				dataRow2["LabourID"] = textBoxLabourID.Text.Trim();
				dataRow2["IBAN"] = textBoxIBAN.Text.Trim();
				if (comboBoxHolidayCalendar.SelectedID != "")
				{
					dataRow2["CalendarID"] = comboBoxHolidayCalendar.SelectedID;
				}
				else
				{
					dataRow2["CalendarID"] = DBNull.Value;
				}
				if (comboBoxMedicalInsuarnceProvider.SelectedID != "")
				{
					dataRow2["MedicalInsuranceProviderID"] = comboBoxMedicalInsuarnceProvider.SelectedID;
				}
				else
				{
					dataRow2["MedicalInsuranceProviderID"] = DBNull.Value;
				}
				if (comboBoxInsuranceCategory.SelectedID != "")
				{
					dataRow2["MedicalInsuranceCategoryID"] = comboBoxInsuranceCategory.SelectedID;
				}
				else
				{
					dataRow2["MedicalInsuranceCategoryID"] = DBNull.Value;
				}
				dataRow2["MedicalInsuranceNumber"] = textBoxInsuranceNumber.Text;
				if (textBoxFamilyMembersCount.Text != "" && textBoxFamilyMembersCount.Text != null)
				{
					dataRow2["NumberOfDependants"] = textBoxFamilyMembersCount.Text;
				}
				else
				{
					dataRow2["NumberOfDependants"] = 0;
				}
				if (dateTimePickerValidFrom.Checked)
				{
					dataRow2["MedicalInsValidFrom"] = dateTimePickerValidFrom.Value;
				}
				else
				{
					dataRow2["MedicalInsValidFrom"] = DBNull.Value;
				}
				if (datetimePickerValidTo.Checked)
				{
					dataRow2["MedicalInsValidTo"] = datetimePickerValidTo.Value;
				}
				else
				{
					dataRow2["MedicalInsValidTo"] = DBNull.Value;
				}
				if (textBoxInsuranceAmount.Text != "")
				{
					dataRow2["MedicalInsuranceAmount"] = textBoxInsuranceAmount.Text;
				}
				else
				{
					dataRow2["MedicalInsuranceAmount"] = 0;
				}
				dataRow2.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeTable.Rows.Add(dataRow2);
				}
				if (analysisData != null)
				{
					currentData.Merge(analysisData);
				}
				dataRow2 = ((!isNewRecord) ? ((currentData.EmployeeAddressTable.Rows.Count > 0) ? currentData.EmployeeAddressTable.Rows[0] : currentData.EmployeeAddressTable.NewRow()) : currentData.EmployeeAddressTable.NewRow());
				dataRow2.BeginEdit();
				dataRow2["EmployeeID"] = textBoxCode.Text;
				dataRow2["AddressID"] = textBoxAddressID.Text.Trim();
				dataRow2["Address1"] = textBoxAddress1.Text;
				dataRow2["Address2"] = textBoxAddress2.Text;
				dataRow2["Address3"] = textBoxAddress3.Text;
				dataRow2["City"] = textBoxCity.Text;
				dataRow2["State"] = textBoxState.Text;
				dataRow2["Country"] = textBoxCountry.Text;
				dataRow2["PostalCode"] = textBoxPostalCode.Text;
				dataRow2["Phone1"] = textBoxPhone1.Text;
				dataRow2["Phone2"] = textBoxPhone2.Text;
				dataRow2["Fax"] = textBoxFax.Text;
				dataRow2["Mobile"] = textBoxMobile.Text;
				dataRow2["Email"] = textBoxEmail.Text;
				dataRow2["Comment"] = textBoxComment.Text;
				dataRow2.EndEdit();
				if (isNewRecord || dataRow2.RowState == DataRowState.Detached)
				{
					currentData.EmployeeAddressTable.Rows.Add(dataRow2);
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
				textBoxCode.Text = textBoxCode.Text.Trim();
				if (textBoxCode.Text.Trim() == "")
				{
					ErrorHelper.WarningMessage("Please enter required fields.");
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
					textBoxCode.SelectAll();
					return false;
				}
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Employee", "EmployeeID", textBoxCode.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Code already exist.");
					tabPageGeneral.Tab.Selected = true;
					textBoxCode.Focus();
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
			if (!IsDirty)
			{
				if (!IsNewRecord)
				{
					IsNewRecord = true;
					ClearForm();
				}
				return true;
			}
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
					flag = Factory.EmployeeSystem.CreateEmployee(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Employee, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.EmployeeSystem.UpdateEmployee(currentData);
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
				if (!base.IsDisposed && !(id == "") && CanClose())
				{
					PublicFunctions.StartWaiting(this);
					currentData = Factory.EmployeeSystem.GetEmployeeByID(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
						textBoxCode.Focus();
					}
					else
					{
						FillData();
						if (toolStripButtonShowPicture.Checked && linkLoadImage.Visible)
						{
							LoadPhoto();
						}
						IsNewRecord = false;
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

		public void LoadData(EmployeeData employeeData)
		{
			try
			{
				if (employeeData != null)
				{
					currentData = employeeData;
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Focus();
					}
					else
					{
						FillData();
						comboBoxStatus.SelectedID = 1;
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
			LoadData(DatabaseHelper.GetPreviousID("Employee", "EmployeeID", textBoxCode.Text));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Employee", "EmployeeID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Employee", "EmployeeID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Employee", "EmployeeID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Employee", "EmployeeID", toolStripTextBoxFind.Text.Trim()))
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
				Factory.AnalysisSystem.DeleteAnalysis(comboBoxAnalysis.SelectedID);
				return Factory.EmployeeSystem.DeleteEmployee(textBoxCode.Text);
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Employee", "EmployeeID");
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
			textBoxInsuranceNumber.Clear();
			textBoxFamilyMembersCount.Clear();
			comboBoxInsuranceCategory.Clear();
			comboBoxMedicalInsuarnceProvider.Clear();
			dateTimePickerValidFrom.Clear();
			datetimePickerValidTo.Clear();
			textBoxInsuranceAmount.Text = 0.ToString(Format.TotalAmountFormat);
			comboBoxCompanyDivision.Clear();
			udfEntryGrid.ClearData();
			textBoxFirstName.Clear();
			textBoxLastName.Clear();
			textBoxMiddleName.Clear();
			textBoxNickName.Clear();
			textBoxNationalID.Clear();
			comboBoxHolidayCalendar.Clear();
			comboBoxLocation.Clear();
			comboBoxDivision.Clear();
			comboBoxDepartment.Clear();
			comboBoxStatus.Clear();
			comboBoxType.Clear();
			comboBoxGroup.Clear();
			comboBoxGrade.Clear();
			comboBoxPosition.Clear();
			comboBoxManager.Clear();
			comboBoxAnalysis.Clear();
			comboBoxGender.SelectedGender = 'M';
			dateTimePickerBirthDate.Checked = false;
			comboBoxLegalPosition.Clear();
			textBoxBirthPlace.Clear();
			comboBoxSponsor.Clear();
			textBoxProbation.Text = "0";
			dateTimePickerConfirmation.Checked = false;
			comboBoxRecruitmentChannel.Clear();
			comboBoxVisaDesignation.Clear();
			comboBoxNationality.Clear();
			textBoxSpouse.Clear();
			comboBoxMaritalStatus.Clear();
			comboBoxReligion.Clear();
			textBoxBloodGroup.Clear();
			comboBoxQualification.Clear();
			comboBoxDayOff.Clear();
			textBoxNote.Clear();
			comboBoxAccount.Clear();
			textBoxAccountName.Clear();
			comboBoxBank.Clear();
			textBoxBankName.Clear();
			textBoxLabourID.Clear();
			textBoxIBAN.Clear();
			textBoxUID.Clear();
			textBoxVisaNumber.Clear();
			textBoxAppraisalPoints.Clear();
			dateTimePickerJoiningDate.Checked = false;
			textBoxServicePeriod.Clear();
			linkLoadImage.Visible = false;
			pictureBoxPhoto.Image = null;
			textBoxCode.Focus();
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

		private void EmployeeClassDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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
			if (!dateTimePickerBirthDate.Checked)
			{
				textBoxAge.Clear();
				return;
			}
			DateTime value = dateTimePickerBirthDate.Value;
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
			if (!dateTimePickerJoiningDate.Checked)
			{
				textBoxServicePeriod.Clear();
				return;
			}
			DateTime value = dateTimePickerJoiningDate.Value;
			TimeSpan timeSpan = DateTime.Today - value;
			int num = 0;
			int num2 = 0;
			if (timeSpan.Days > 0)
			{
				num = timeSpan.Days / 365;
			}
			int result = 0;
			if (timeSpan.Days > 0)
			{
				num2 = Math.DivRem(timeSpan.Days, 365, out result);
				num2 = result / 30;
			}
			if (num > 0 || num2 > 0)
			{
				textBoxServicePeriod.Clear();
				if (num > 0)
				{
					textBoxServicePeriod.Text = num.ToString() + " Years";
				}
				if (num2 > 0)
				{
					if (textBoxServicePeriod.Text != "")
					{
						textBoxServicePeriod.Text += " & ";
					}
					MMTextBox mMTextBox = textBoxServicePeriod;
					mMTextBox.Text = mMTextBox.Text + num2.ToString() + " Months";
				}
			}
			else
			{
				textBoxServicePeriod.Clear();
			}
		}

		private void buttonMoreAddress_Click(object sender, EventArgs e)
		{
			new FormHelper().EditEmployeeAddress(textBoxCode.Text, textBoxAddressID.Text);
		}

		private void linkLabelCountry_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditWorkLocation(comboBoxLocation.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditDivision(comboBoxDivision.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditDepartment(comboBoxDepartment.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployeeGroup(comboBoxGroup.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGrade(comboBoxGrade.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPosition(comboBoxPosition.SelectedID);
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSponsor(comboBoxSponsor.SelectedID);
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditNationality(comboBoxNationality.SelectedID);
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditReligion(comboBoxReligion.SelectedID);
		}

		private void buttonMore_Click(object sender, EventArgs e)
		{
		}

		private void dependentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeDependentDetailsFormObj);
			FormActivator.EmployeeDependentDetailsFormObj.LoadData(textBoxCode.Text, "");
		}

		private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeDocumentsFormObj);
			FormActivator.EmployeeDocumentsFormObj.LoadData(textBoxCode.Text);
		}

		private void skillsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeSkillsFormObj);
			FormActivator.EmployeeSkillsFormObj.LoadData(textBoxCode.Text);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAccount.SelectedID);
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
					DataSet employeeProfileReport = Factory.EmployeeSystem.GetEmployeeProfileReport(textBoxCode.Text, textBoxCode.Text, "", "", "", "", showInactive: true);
					if (employeeProfileReport == null || employeeProfileReport.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(employeeProfileReport, "", "Employee Profile", SysDocTypes.None, isPrint, showPrintDialog);
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
			new FormHelper().ShowList(DataComboType.Employee);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = textBoxFirstName.Text + " " + textBoxMiddleName.Text + " " + textBoxLastName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Employees;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel10_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBank(comboBoxBank.SelectedID);
		}

		private void ultraFormattedLinkLabel11_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployeeType(comboBoxType.SelectedID);
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddEmployeePhoto(textBoxCode.Text, image))
					{
						pictureBoxPhoto.Image = image;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot add picture.");
			}
		}

		private void linkRemovePicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to remove the item image?") == DialogResult.Yes)
				{
					if (Factory.EmployeeSystem.RemoveEmployeePhoto(textBoxCode.Text))
					{
						pictureBoxPhoto.Image = pictureBoxNoImage.Image;
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
					pictureBoxPhoto.Image = PublicFunctions.GetEmployeeThumbnailImage(textBoxCode.Text);
					linkLoadImage.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonProject_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void salaryDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeSalaryDetailsFormObj);
			FormActivator.EmployeeSalaryDetailsFormObj.LoadData(textBoxCode.Text);
		}

		private void ultraFormattedLinkLabel12_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditInsuranceProvider(comboBoxMedicalInsuarnceProvider.SelectedID);
		}

		private void comboBoxPosition_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel13_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditInsuranceCategory(comboBoxInsuranceCategory.SelectedID);
		}

		private void ultraFormattedLinkLabel14_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditHolidayCalendar(comboBoxHolidayCalendar.SelectedID);
		}

		private void ultraFormattedLinkLabel15_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditQualification(comboBoxQualification.SelectedID);
		}

		private void udfEntryGrid_Load(object sender, EventArgs e)
		{
		}

		private void toolStripButtonShowPicture_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel16_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAnalysis(comboBoxAnalysis.SelectedID);
		}

		private void ultraFormattedLinkLabelRcrtmnt_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAgent(comboBoxRecruitmentChannel.SelectedID);
		}

		private void ultraFormattedLinkLabelVisaDesig_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPosition(comboBoxVisaDesignation.SelectedID);
		}

		private void ultraFormattedLinkLabelCompanyDivision_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCompanyDivision(comboBoxCompanyDivision.SelectedID);
		}

		private void ultraFormattedLinkLabel17_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.LegalPosition, comboBoxLegalPosition.SelectedID);
		}
	}
}
