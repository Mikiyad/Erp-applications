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
	public class HorseSummaryDetailsForm : Form, IForm
	{
		private HorseSummaryData currentData;

		private const string TABLENAME_CONST = "Horse_Summary";

		private const string IDFIELD_CONST = "HorseCode";

		private bool isNewRecord = true;

		private MMLabel lblDescriptions;

		private MMTextBox textBoxMicroChipNumber;

		private MMTextBox textBoxName;

		private MMLabel label1;

		private MMTextBox textBoxCode;

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

		private MMTextBox textBoxRegisterNumber;

		private MMLabel mmLabel6;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private Panel panel1;

		private MMLabel mmLabel5;

		private MMTextBox textBoxBrand;

		private MMLabel mmLabel31;

		private MMTextBox textBoxBreed;

		private MMLabel mmLabel49;

		private MMLabel mmLabel50;

		private MMLabel mmLabel51;

		private MMTextBox textBoxAge;

		private MMSDateTimePicker dateTimePickerBirthDate;

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

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private UltraFormattedLinkLabel linkLoadImage;

		private PictureBox pictureBoxPhoto;

		private OpenFileDialog openFileDialog1;

		private PictureBox pictureBoxNoImage;

		private ToolStripButton toolStripButtonShowPicture;

		private ToolStripButton toolStripButtonInformation;

		private MMLabel labelEmployeeNumber;

		private ComboBox comboBoxColor;

		private MMTextBox textBoxSire;

		private MMTextBox textBoxDam;

		private MMLabel mmLabel3;

		private CountryComboBox comboBoxCountry;

		private MMTextBox textBoxCurrentOwnerShip;

		private MMLabel mmLabel21;

		private MMTextBox textBoxPreviousOwnerShip;

		private MMLabel mmLabel22;

		private UltraGroupBox ultraGroupBox3;

		private MMLabel mmLabel24;

		private MMSDateTimePicker datetimePickerOwnerShipChanged;

		private LocationComboBox comboBoxLocation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private RiderComboBox comboBoxTrainer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private MMTextBox textBoxBreeder;

		private MMLabel mmLabel25;

		private RiderComboBox comboBoxCareTaker;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel1;

		private MMSDateTimePicker datetimePickerPassportIssue;

		private MMLabel mmLabel2;

		private MMSDateTimePicker datetimePickerPassportExpiry;

		private UltraGroupBox ultraGroupBox2;

		private MMLabel mmLabel4;

		private MMSDateTimePicker dateTimePickerImported;

		private MMLabel mmLabel8;

		private MMSDateTimePicker dateTimePickerRevalidation;

		private CountryComboBox comboBoxImportedFrom;

		private CountryComboBox comboBoxExportedTo;

		private CountryComboBox comboboxReceivedFrom;

		private MMLabel mmLabel14;

		private MMSDateTimePicker dateTimePickerReceived;

		private CountryComboBox comboBoxSoldAt;

		private MMLabel mmLabel18;

		private MMSDateTimePicker dateTimePickerSold;

		private CountryComboBox comboBoxTransferredTo;

		private MMLabel mmLabel16;

		private MMSDateTimePicker dateTimePickerTransferred;

		private MMLabel mmLabel26;

		private MMLabel mmLabel27;

		private MMSDateTimePicker dateTimePickerDead;

		private MMLabel mmLabel23;

		private MMSDateTimePicker dateTimePickerSexChange;

		private MMLabel mmLabel11;

		private MMTextBox textBoxPastPerformance;

		private MMTextBox textBoxSireOfDam;

		private MMLabel mmLabel9;

		private MMSDateTimePicker dateTimeOwnerShip;

		private CheckBox checkBoxInactive;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

		private UltraTabPageControl ultraTabPageControl2;

		private MMLabel mmLabel28;

		private MMLabel mmLabel29;

		private MMLabel mmLabel30;

		private MMLabel mmLabel32;

		private PictureBox pictureBox1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private PictureBox pictureBox2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel11;

		private WorkLocationComboBox workLocationComboBox1;

		private MMTextBox textBoxBirthPlace;

		private EmployeeTypeComboBox comboBoxType;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private MMTextBox mmTextBox2;

		private MMLabel mmLabel33;

		private SponsorComboBox comboBoxSponsor;

		private MMLabel mmLabel34;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private MaritalStatusComboBox comboBoxMaritalStatus;

		private MMLabel mmLabel35;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel9;

		private MMSDateTimePicker mmsDateTimePicker1;

		private NationalityComboBox comboBoxNationality;

		private MMLabel mmLabel36;

		private UltraFormattedLinkLabel linkLabelCountry;

		private GenderComboBox genderComboBox1;

		private MMSDateTimePicker dateTimePickerJoiningDate;

		private MMLabel mmLabel37;

		private MMTextBox textBoxServicePeriod;

		private MMTextBox textBoxNationalID;

		private MMLabel mmLabel38;

		private EmployeeComboBox comboBoxManager;

		private PositionComboBox comboBoxPosition;

		private DepartmentComboBox comboBoxDepartment;

		private DivisionComboBox comboBoxDivision;

		private EmployeeStatusComboBox comboBoxStatus;

		private CheckBox checkBoxOnVacation;

		private GradeComboBox comboBoxGrade;

		private MMLabel mmLabel39;

		private EmployeeGroupComboBox comboBoxGroup;

		private MMLabel mmLabel40;

		private MMLabel mmLabel41;

		private MMTextBox textBoxNickName;

		private MMTextBox mmTextBox3;

		private MMTextBox textBoxLastName;

		private MMTextBox textBoxFirstName;

		private MMLabel mmLabel42;

		private MMLabel mmLabel43;

		private MMTextBox textBoxMiddleName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel10;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel13;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel14;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel15;

		private UltraTabPageControl ultraTabPageControl3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel16;

		private MMTextBox textBoxAppraisalPoints;

		private MMLabel mmLabel44;

		private QualificationComboBox comboBoxQualification;

		private UltraGroupBox ultraGroupBox4;

		private MMLabel mmLabel45;

		private AllAccountsComboBox comboBoxAccount;

		private MMTextBox textBoxIBAN;

		private MMTextBox textBoxAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel17;

		private BankComboBox comboBoxBank;

		private MMTextBox textBoxBankName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel18;

		private MMTextBox textBoxLabourID;

		private UltraGroupBox ultraGroupBox5;

		private MMLabel mmLabel46;

		private MMTextBox textBoxComment;

		private XPButton buttonMoreAddress;

		private MMLabel mmLabel47;

		private MMTextBox textBoxPostalCode;

		private MMLabel mmLabel48;

		private MMTextBox textBoxEmail;

		private MMLabel mmLabel52;

		private MMTextBox textBoxMobile;

		private MMLabel mmLabel53;

		private MMTextBox textBoxFax;

		private MMLabel mmLabel54;

		private MMTextBox textBoxPhone2;

		private MMLabel mmLabel55;

		private MMTextBox textBoxPhone1;

		private MMLabel mmLabel56;

		private MMTextBox textBoxCountry;

		private MMLabel mmLabel57;

		private MMTextBox textBoxState;

		private MMLabel mmLabel58;

		private MMTextBox textBoxCity;

		private MMTextBox textBoxAddress3;

		private MMTextBox textBoxAddress2;

		private MMLabel mmLabel59;

		private MMTextBox textBoxAddress1;

		private MMLabel mmLabel60;

		private MMTextBox textBoxAddressID;

		private MMSDateTimePicker dateTimePickerConfirmation;

		private MMLabel mmLabel61;

		private MMTextBox textBoxSpouse;

		private MMLabel mmLabel62;

		private MMLabel mmLabel63;

		private MMTextBox textBoxBloodGroup;

		private ReligionComboBox comboBoxReligion;

		private MMLabel mmLabel64;

		private NumberTextBox textBoxProbation;

		private MMLabel mmLabel65;

		private DaysComboBox comboBoxDayOff;

		private MMLabel mmLabel66;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel19;

		private UltraTabPageControl tabPageUserDefined;

		private UltraTabControl ultraTabControl2;

		private UDFEntryControl udfEntryGrid1;

		private UDFEntryControl udfEntryGrid;

		private MMLabel mmLabel67;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripDropDownButton toolStripButton1;

		private ToolStripMenuItem documentsToolStripMenuItem1;

		private ToolStripMenuItem skillsToolStripMenuItem1;

		private HorseTypeComboBox comboBoxHorseType;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel12;

		private HorseSexComboBox comboBoxHorseGender;

		private HorseSexComboBox comboBoxSexChangedFrom;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel20;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel21;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel22;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel23;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel27;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel26;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel25;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel24;

		private GenericListComboBox comboBoxOwnershipType;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel28;

		private GenericListComboBox comboBoxcategory;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel29;

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
					linkAddPicture.Enabled = true;
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

		public HorseSummaryDetailsForm()
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.HorseSummaryDetailsForm));
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
			Infragistics.Win.Appearance appearance277 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance278 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance279 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance280 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance281 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance282 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance283 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance284 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance285 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance286 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance287 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance288 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance289 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance290 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance291 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance292 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance293 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance294 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance295 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance296 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance297 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance298 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance299 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance300 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance301 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance302 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance303 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance304 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance305 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance306 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance307 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance308 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance309 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance310 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance311 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance312 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance313 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance314 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance315 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance316 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance317 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance318 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance319 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance320 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance321 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance322 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance323 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance324 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance325 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance326 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance327 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance328 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance329 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance330 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance331 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance332 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance333 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance334 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance335 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance336 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance337 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance338 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance339 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance340 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance341 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance342 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance343 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance344 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance345 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance346 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance347 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance348 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance349 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance350 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance351 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance352 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance353 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance354 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance355 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance356 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance357 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance358 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance359 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance360 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance361 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance362 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance363 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance364 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance365 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance366 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance367 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance368 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance369 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance370 = new Infragistics.Win.Appearance();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraFormattedLinkLabel22 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel20 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel12 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			comboBoxColor = new System.Windows.Forms.ComboBox();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel27 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel26 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel25 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel24 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel23 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel21 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
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
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			documentsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			skillsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonShowPicture = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panel1 = new System.Windows.Forms.Panel();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			dependentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			documentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			skillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBox2 = new System.Windows.Forms.PictureBox();
			ultraFormattedLinkLabel11 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel9 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelCountry = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkBoxOnVacation = new System.Windows.Forms.CheckBox();
			ultraFormattedLinkLabel10 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel13 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel14 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel15 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraFormattedLinkLabel16 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel17 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel18 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox5 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel19 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraTabControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraFormattedLinkLabel28 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel29 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel67 = new Micromind.UISupport.MMLabel();
			textBoxSireOfDam = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxBreeder = new Micromind.UISupport.MMTextBox();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			datetimePickerOwnerShipChanged = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel21 = new Micromind.UISupport.MMLabel();
			textBoxPreviousOwnerShip = new Micromind.UISupport.MMTextBox();
			textBoxCurrentOwnerShip = new Micromind.UISupport.MMTextBox();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			textBoxDam = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxSire = new Micromind.UISupport.MMTextBox();
			labelEmployeeNumber = new Micromind.UISupport.MMLabel();
			textBoxAge = new Micromind.UISupport.MMTextBox();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			mmLabel51 = new Micromind.UISupport.MMLabel();
			dateTimePickerBirthDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel50 = new Micromind.UISupport.MMLabel();
			textBoxBreed = new Micromind.UISupport.MMTextBox();
			mmLabel49 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxBrand = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxRegisterNumber = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			label1 = new Micromind.UISupport.MMLabel();
			textBoxMicroChipNumber = new Micromind.UISupport.MMTextBox();
			dateTimeOwnerShip = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxPastPerformance = new Micromind.UISupport.MMTextBox();
			mmLabel26 = new Micromind.UISupport.MMLabel();
			mmLabel27 = new Micromind.UISupport.MMLabel();
			dateTimePickerDead = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel23 = new Micromind.UISupport.MMLabel();
			dateTimePickerSexChange = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel18 = new Micromind.UISupport.MMLabel();
			dateTimePickerSold = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel16 = new Micromind.UISupport.MMLabel();
			dateTimePickerTransferred = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel14 = new Micromind.UISupport.MMLabel();
			dateTimePickerReceived = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel4 = new Micromind.UISupport.MMLabel();
			dateTimePickerImported = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel8 = new Micromind.UISupport.MMLabel();
			dateTimePickerRevalidation = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel2 = new Micromind.UISupport.MMLabel();
			datetimePickerPassportExpiry = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel1 = new Micromind.UISupport.MMLabel();
			datetimePickerPassportIssue = new Micromind.UISupport.MMSDateTimePicker(components);
			labelCustomerNameHeader = new Micromind.UISupport.MMLabel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonClose = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			mmLabel28 = new Micromind.UISupport.MMLabel();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			mmLabel30 = new Micromind.UISupport.MMLabel();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			textBoxBirthPlace = new Micromind.UISupport.MMTextBox();
			mmTextBox2 = new Micromind.UISupport.MMTextBox();
			mmLabel33 = new Micromind.UISupport.MMLabel();
			mmLabel34 = new Micromind.UISupport.MMLabel();
			mmLabel35 = new Micromind.UISupport.MMLabel();
			mmsDateTimePicker1 = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel36 = new Micromind.UISupport.MMLabel();
			dateTimePickerJoiningDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel37 = new Micromind.UISupport.MMLabel();
			textBoxServicePeriod = new Micromind.UISupport.MMTextBox();
			textBoxNationalID = new Micromind.UISupport.MMTextBox();
			mmLabel38 = new Micromind.UISupport.MMLabel();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			mmLabel40 = new Micromind.UISupport.MMLabel();
			mmLabel41 = new Micromind.UISupport.MMLabel();
			textBoxNickName = new Micromind.UISupport.MMTextBox();
			mmTextBox3 = new Micromind.UISupport.MMTextBox();
			textBoxLastName = new Micromind.UISupport.MMTextBox();
			textBoxFirstName = new Micromind.UISupport.MMTextBox();
			mmLabel42 = new Micromind.UISupport.MMLabel();
			mmLabel43 = new Micromind.UISupport.MMLabel();
			textBoxMiddleName = new Micromind.UISupport.MMTextBox();
			textBoxAppraisalPoints = new Micromind.UISupport.MMTextBox();
			mmLabel44 = new Micromind.UISupport.MMLabel();
			mmLabel45 = new Micromind.UISupport.MMLabel();
			textBoxIBAN = new Micromind.UISupport.MMTextBox();
			textBoxAccountName = new Micromind.UISupport.MMTextBox();
			textBoxBankName = new Micromind.UISupport.MMTextBox();
			textBoxLabourID = new Micromind.UISupport.MMTextBox();
			mmLabel46 = new Micromind.UISupport.MMLabel();
			textBoxComment = new Micromind.UISupport.MMTextBox();
			buttonMoreAddress = new Micromind.UISupport.XPButton();
			mmLabel47 = new Micromind.UISupport.MMLabel();
			textBoxPostalCode = new Micromind.UISupport.MMTextBox();
			mmLabel48 = new Micromind.UISupport.MMLabel();
			textBoxEmail = new Micromind.UISupport.MMTextBox();
			mmLabel52 = new Micromind.UISupport.MMLabel();
			textBoxMobile = new Micromind.UISupport.MMTextBox();
			mmLabel53 = new Micromind.UISupport.MMLabel();
			textBoxFax = new Micromind.UISupport.MMTextBox();
			mmLabel54 = new Micromind.UISupport.MMLabel();
			textBoxPhone2 = new Micromind.UISupport.MMTextBox();
			mmLabel55 = new Micromind.UISupport.MMLabel();
			textBoxPhone1 = new Micromind.UISupport.MMTextBox();
			mmLabel56 = new Micromind.UISupport.MMLabel();
			textBoxCountry = new Micromind.UISupport.MMTextBox();
			mmLabel57 = new Micromind.UISupport.MMLabel();
			textBoxState = new Micromind.UISupport.MMTextBox();
			mmLabel58 = new Micromind.UISupport.MMLabel();
			textBoxCity = new Micromind.UISupport.MMTextBox();
			textBoxAddress3 = new Micromind.UISupport.MMTextBox();
			textBoxAddress2 = new Micromind.UISupport.MMTextBox();
			mmLabel59 = new Micromind.UISupport.MMLabel();
			textBoxAddress1 = new Micromind.UISupport.MMTextBox();
			mmLabel60 = new Micromind.UISupport.MMLabel();
			textBoxAddressID = new Micromind.UISupport.MMTextBox();
			dateTimePickerConfirmation = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel61 = new Micromind.UISupport.MMLabel();
			textBoxSpouse = new Micromind.UISupport.MMTextBox();
			mmLabel62 = new Micromind.UISupport.MMLabel();
			mmLabel63 = new Micromind.UISupport.MMLabel();
			textBoxBloodGroup = new Micromind.UISupport.MMTextBox();
			mmLabel64 = new Micromind.UISupport.MMLabel();
			textBoxProbation = new Micromind.UISupport.NumberTextBox();
			mmLabel65 = new Micromind.UISupport.MMLabel();
			mmLabel66 = new Micromind.UISupport.MMLabel();
			comboBoxcategory = new Micromind.DataControls.GenericListComboBox();
			comboBoxHorseGender = new Micromind.DataControls.HorseSexComboBox();
			comboBoxHorseType = new Micromind.DataControls.HorseTypeComboBox();
			comboBoxCareTaker = new Micromind.DataControls.RiderComboBox();
			comboBoxTrainer = new Micromind.DataControls.RiderComboBox();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxOwnershipType = new Micromind.DataControls.GenericListComboBox();
			comboBoxCountry = new Micromind.DataControls.CountryComboBox();
			comboBoxSexChangedFrom = new Micromind.DataControls.HorseSexComboBox();
			comboBoxSoldAt = new Micromind.DataControls.CountryComboBox();
			comboBoxTransferredTo = new Micromind.DataControls.CountryComboBox();
			comboboxReceivedFrom = new Micromind.DataControls.CountryComboBox();
			comboBoxExportedTo = new Micromind.DataControls.CountryComboBox();
			comboBoxImportedFrom = new Micromind.DataControls.CountryComboBox();
			udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
			formManager = new Micromind.DataControls.FormManager();
			workLocationComboBox1 = new Micromind.DataControls.WorkLocationComboBox();
			comboBoxType = new Micromind.DataControls.EmployeeTypeComboBox();
			comboBoxSponsor = new Micromind.DataControls.SponsorComboBox();
			comboBoxMaritalStatus = new Micromind.DataControls.MaritalStatusComboBox();
			comboBoxNationality = new Micromind.DataControls.NationalityComboBox();
			genderComboBox1 = new Micromind.DataControls.GenderComboBox();
			comboBoxManager = new Micromind.DataControls.EmployeeComboBox();
			comboBoxPosition = new Micromind.DataControls.PositionComboBox();
			comboBoxDepartment = new Micromind.DataControls.DepartmentComboBox();
			comboBoxDivision = new Micromind.DataControls.DivisionComboBox();
			comboBoxStatus = new Micromind.DataControls.EmployeeStatusComboBox();
			comboBoxGrade = new Micromind.DataControls.GradeComboBox();
			comboBoxGroup = new Micromind.DataControls.EmployeeGroupComboBox();
			comboBoxQualification = new Micromind.DataControls.QualificationComboBox();
			comboBoxAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxBank = new Micromind.DataControls.BankComboBox();
			comboBoxReligion = new Micromind.DataControls.ReligionComboBox();
			comboBoxDayOff = new Micromind.DataControls.DaysComboBox();
			udfEntryGrid1 = new Micromind.DataControls.UDFEntryControl();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			panel1.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			ultraTabPageControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).BeginInit();
			ultraGroupBox5.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl2).BeginInit();
			ultraTabControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxcategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxHorseGender).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxHorseType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCareTaker).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTrainer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOwnershipType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSexChangedFrom).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSoldAt).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTransferredTo).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboboxReceivedFrom).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxExportedTo).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxImportedFrom).BeginInit();
			((System.ComponentModel.ISupportInitialize)workLocationComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManager).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPosition).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepartment).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGrade).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxQualification).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReligion).BeginInit();
			SuspendLayout();
			tabPageGeneral.Controls.Add(comboBoxcategory);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel29);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel22);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel20);
			tabPageGeneral.Controls.Add(comboBoxHorseGender);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel12);
			tabPageGeneral.Controls.Add(comboBoxHorseType);
			tabPageGeneral.Controls.Add(mmLabel67);
			tabPageGeneral.Controls.Add(checkBoxInactive);
			tabPageGeneral.Controls.Add(textBoxSireOfDam);
			tabPageGeneral.Controls.Add(mmLabel9);
			tabPageGeneral.Controls.Add(comboBoxCareTaker);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel3);
			tabPageGeneral.Controls.Add(comboBoxTrainer);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel2);
			tabPageGeneral.Controls.Add(textBoxBreeder);
			tabPageGeneral.Controls.Add(mmLabel25);
			tabPageGeneral.Controls.Add(comboBoxLocation);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel1);
			tabPageGeneral.Controls.Add(ultraGroupBox3);
			tabPageGeneral.Controls.Add(comboBoxCountry);
			tabPageGeneral.Controls.Add(textBoxDam);
			tabPageGeneral.Controls.Add(mmLabel3);
			tabPageGeneral.Controls.Add(textBoxSire);
			tabPageGeneral.Controls.Add(comboBoxColor);
			tabPageGeneral.Controls.Add(labelEmployeeNumber);
			tabPageGeneral.Controls.Add(pictureBoxNoImage);
			tabPageGeneral.Controls.Add(linkRemovePicture);
			tabPageGeneral.Controls.Add(linkAddPicture);
			tabPageGeneral.Controls.Add(linkLoadImage);
			tabPageGeneral.Controls.Add(pictureBoxPhoto);
			tabPageGeneral.Controls.Add(textBoxAge);
			tabPageGeneral.Controls.Add(mmLabel31);
			tabPageGeneral.Controls.Add(mmLabel51);
			tabPageGeneral.Controls.Add(dateTimePickerBirthDate);
			tabPageGeneral.Controls.Add(mmLabel50);
			tabPageGeneral.Controls.Add(textBoxBreed);
			tabPageGeneral.Controls.Add(mmLabel49);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(lblDescriptions);
			tabPageGeneral.Controls.Add(textBoxBrand);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(textBoxRegisterNumber);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(mmLabel6);
			tabPageGeneral.Controls.Add(label1);
			tabPageGeneral.Controls.Add(textBoxMicroChipNumber);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(715, 489);
			tabPageGeneral.Paint += new System.Windows.Forms.PaintEventHandler(tabPageGeneral_Paint);
			ultraFormattedLinkLabel22.AutoSize = true;
			ultraFormattedLinkLabel22.Location = new System.Drawing.Point(9, 253);
			ultraFormattedLinkLabel22.Name = "ultraFormattedLinkLabel22";
			ultraFormattedLinkLabel22.Size = new System.Drawing.Size(46, 14);
			ultraFormattedLinkLabel22.TabIndex = 99;
			ultraFormattedLinkLabel22.TabStop = true;
			ultraFormattedLinkLabel22.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel22.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel22.Value = "Country:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel22.VisitedLinkAppearance = appearance;
			ultraFormattedLinkLabel22.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel22_LinkClicked);
			ultraFormattedLinkLabel20.AutoSize = true;
			ultraFormattedLinkLabel20.Location = new System.Drawing.Point(364, 185);
			ultraFormattedLinkLabel20.Name = "ultraFormattedLinkLabel20";
			ultraFormattedLinkLabel20.Size = new System.Drawing.Size(44, 14);
			ultraFormattedLinkLabel20.TabIndex = 11;
			ultraFormattedLinkLabel20.TabStop = true;
			ultraFormattedLinkLabel20.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel20.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel20.Value = "Gender:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel20.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel20.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel20_LinkClicked);
			ultraFormattedLinkLabel12.AutoSize = true;
			ultraFormattedLinkLabel12.Location = new System.Drawing.Point(10, 31);
			ultraFormattedLinkLabel12.Name = "ultraFormattedLinkLabel12";
			ultraFormattedLinkLabel12.Size = new System.Drawing.Size(64, 14);
			ultraFormattedLinkLabel12.TabIndex = 96;
			ultraFormattedLinkLabel12.TabStop = true;
			ultraFormattedLinkLabel12.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel12.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel12.Value = "Horse Type:";
			appearance3.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel12.VisitedLinkAppearance = appearance3;
			ultraFormattedLinkLabel12.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel12_LinkClicked);
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(310, 11);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(9, 297);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(60, 14);
			ultraFormattedLinkLabel3.TabIndex = 40;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "CareTaker:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(320, 275);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(42, 14);
			ultraFormattedLinkLabel2.TabIndex = 92;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Trainer:";
			appearance5.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance5;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(10, 275);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(49, 14);
			ultraFormattedLinkLabel1.TabIndex = 39;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Location:";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance6;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			ultraGroupBox3.Controls.Add(comboBoxOwnershipType);
			ultraGroupBox3.Controls.Add(ultraFormattedLinkLabel28);
			ultraGroupBox3.Controls.Add(mmLabel24);
			ultraGroupBox3.Controls.Add(datetimePickerOwnerShipChanged);
			ultraGroupBox3.Controls.Add(mmLabel21);
			ultraGroupBox3.Controls.Add(textBoxPreviousOwnerShip);
			ultraGroupBox3.Controls.Add(textBoxCurrentOwnerShip);
			ultraGroupBox3.Controls.Add(mmLabel22);
			ultraGroupBox3.Location = new System.Drawing.Point(0, 326);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(644, 88);
			ultraGroupBox3.TabIndex = 20;
			ultraGroupBox3.Text = "OwnerShip Details";
			comboBoxColor.FormattingEnabled = true;
			comboBoxColor.Items.AddRange(new object[18]
			{
				"OVERO",
				"BROWN & WHITE",
				"CREAM",
				"NEGRO",
				"UNKNOWN",
				"GREY",
				"CHESTNUT",
				"GREY(TORDO)",
				"BROWN",
				"ROAN",
				"DB/BR",
				"BAY",
				"PALOMINO",
				"SORREL",
				"BLACK & WHITE",
				"TRI COL",
				"BLACK",
				"PIEBALD"
			});
			comboBoxColor.Location = new System.Drawing.Point(132, 184);
			comboBoxColor.Name = "comboBoxColor";
			comboBoxColor.Size = new System.Drawing.Size(151, 21);
			comboBoxColor.TabIndex = 10;
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(588, 76);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 67;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(588, 145);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 26;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance7.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance7;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(549, 145);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 25;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance8.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance8;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(558, 59);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 27;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance9.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance9;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(529, 11);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(128, 128);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 65;
			pictureBoxPhoto.TabStop = false;
			tabPageDetails.Controls.Add(ultraGroupBox2);
			tabPageDetails.Controls.Add(ultraGroupBox1);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(715, 489);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel27);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel26);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel25);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel24);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel23);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel21);
			ultraGroupBox2.Controls.Add(comboBoxSexChangedFrom);
			ultraGroupBox2.Controls.Add(dateTimeOwnerShip);
			ultraGroupBox2.Controls.Add(mmLabel11);
			ultraGroupBox2.Controls.Add(textBoxPastPerformance);
			ultraGroupBox2.Controls.Add(mmLabel26);
			ultraGroupBox2.Controls.Add(mmLabel27);
			ultraGroupBox2.Controls.Add(dateTimePickerDead);
			ultraGroupBox2.Controls.Add(mmLabel23);
			ultraGroupBox2.Controls.Add(dateTimePickerSexChange);
			ultraGroupBox2.Controls.Add(comboBoxSoldAt);
			ultraGroupBox2.Controls.Add(mmLabel18);
			ultraGroupBox2.Controls.Add(dateTimePickerSold);
			ultraGroupBox2.Controls.Add(comboBoxTransferredTo);
			ultraGroupBox2.Controls.Add(mmLabel16);
			ultraGroupBox2.Controls.Add(dateTimePickerTransferred);
			ultraGroupBox2.Controls.Add(comboboxReceivedFrom);
			ultraGroupBox2.Controls.Add(mmLabel14);
			ultraGroupBox2.Controls.Add(dateTimePickerReceived);
			ultraGroupBox2.Controls.Add(comboBoxExportedTo);
			ultraGroupBox2.Controls.Add(comboBoxImportedFrom);
			ultraGroupBox2.Controls.Add(mmLabel4);
			ultraGroupBox2.Controls.Add(dateTimePickerImported);
			ultraGroupBox2.Controls.Add(mmLabel8);
			ultraGroupBox2.Controls.Add(dateTimePickerRevalidation);
			ultraGroupBox2.Location = new System.Drawing.Point(10, 97);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(567, 300);
			ultraGroupBox2.TabIndex = 1;
			ultraGroupBox2.Text = "Import/Export Details";
			ultraFormattedLinkLabel27.AutoSize = true;
			ultraFormattedLinkLabel27.Location = new System.Drawing.Point(17, 146);
			ultraFormattedLinkLabel27.Name = "ultraFormattedLinkLabel27";
			ultraFormattedLinkLabel27.Size = new System.Drawing.Size(42, 14);
			ultraFormattedLinkLabel27.TabIndex = 121;
			ultraFormattedLinkLabel27.TabStop = true;
			ultraFormattedLinkLabel27.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel27.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel27.Value = "Sold At:";
			appearance10.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel27.VisitedLinkAppearance = appearance10;
			ultraFormattedLinkLabel27.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel27_LinkClicked);
			ultraFormattedLinkLabel26.AutoSize = true;
			ultraFormattedLinkLabel26.Location = new System.Drawing.Point(17, 122);
			ultraFormattedLinkLabel26.Name = "ultraFormattedLinkLabel26";
			ultraFormattedLinkLabel26.Size = new System.Drawing.Size(80, 14);
			ultraFormattedLinkLabel26.TabIndex = 120;
			ultraFormattedLinkLabel26.TabStop = true;
			ultraFormattedLinkLabel26.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel26.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel26.Value = "Transferred To:";
			appearance11.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel26.VisitedLinkAppearance = appearance11;
			ultraFormattedLinkLabel26.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel26_LinkClicked);
			ultraFormattedLinkLabel25.AutoSize = true;
			ultraFormattedLinkLabel25.Location = new System.Drawing.Point(17, 100);
			ultraFormattedLinkLabel25.Name = "ultraFormattedLinkLabel25";
			ultraFormattedLinkLabel25.Size = new System.Drawing.Size(82, 14);
			ultraFormattedLinkLabel25.TabIndex = 119;
			ultraFormattedLinkLabel25.TabStop = true;
			ultraFormattedLinkLabel25.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel25.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel25.Value = "Received From:";
			appearance12.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel25.VisitedLinkAppearance = appearance12;
			ultraFormattedLinkLabel25.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel25_LinkClicked);
			ultraFormattedLinkLabel24.AutoSize = true;
			ultraFormattedLinkLabel24.Location = new System.Drawing.Point(17, 76);
			ultraFormattedLinkLabel24.Name = "ultraFormattedLinkLabel24";
			ultraFormattedLinkLabel24.Size = new System.Drawing.Size(67, 14);
			ultraFormattedLinkLabel24.TabIndex = 118;
			ultraFormattedLinkLabel24.TabStop = true;
			ultraFormattedLinkLabel24.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel24.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel24.Value = "Exported To:";
			appearance13.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel24.VisitedLinkAppearance = appearance13;
			ultraFormattedLinkLabel24.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel24_LinkClicked);
			ultraFormattedLinkLabel23.AutoSize = true;
			ultraFormattedLinkLabel23.Location = new System.Drawing.Point(17, 54);
			ultraFormattedLinkLabel23.Name = "ultraFormattedLinkLabel23";
			ultraFormattedLinkLabel23.Size = new System.Drawing.Size(79, 14);
			ultraFormattedLinkLabel23.TabIndex = 117;
			ultraFormattedLinkLabel23.TabStop = true;
			ultraFormattedLinkLabel23.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel23.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel23.Value = "Imported From:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel23.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel23.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel23_LinkClicked);
			ultraFormattedLinkLabel21.AutoSize = true;
			ultraFormattedLinkLabel21.Location = new System.Drawing.Point(17, 168);
			ultraFormattedLinkLabel21.Name = "ultraFormattedLinkLabel21";
			ultraFormattedLinkLabel21.Size = new System.Drawing.Size(102, 14);
			ultraFormattedLinkLabel21.TabIndex = 116;
			ultraFormattedLinkLabel21.TabStop = true;
			ultraFormattedLinkLabel21.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel21.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel21.Value = "Sex Changed From:";
			appearance15.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel21.VisitedLinkAppearance = appearance15;
			ultraFormattedLinkLabel21.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel21_LinkClicked);
			ultraGroupBox1.Controls.Add(mmLabel2);
			ultraGroupBox1.Controls.Add(datetimePickerPassportExpiry);
			ultraGroupBox1.Controls.Add(mmLabel1);
			ultraGroupBox1.Controls.Add(datetimePickerPassportIssue);
			ultraGroupBox1.Location = new System.Drawing.Point(10, 18);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(646, 73);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Passport Details";
			ultraTabPageControl1.Controls.Add(udfEntryGrid);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(715, 489);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 57);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(719, 512);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 0;
			appearance16.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance16;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Details";
			ultraTab3.TabPage = ultraTabPageControl1;
			ultraTab3.Text = "&User Defined";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3]
			{
				ultraTab,
				ultraTab2,
				ultraTab3
			});
			ultraTabControl1.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(ultraTabControl1_SelectedTabChanged);
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(715, 489);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonClose);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 569);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(719, 40);
			panelButtons.TabIndex = 1;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[18]
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
				toolStripSeparator5,
				toolStripButton1,
				toolStripSeparator2,
				toolStripButtonShowPicture,
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonInformation
			});
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(719, 27);
			toolStrip1.TabIndex = 2;
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
			toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				documentsToolStripMenuItem1,
				skillsToolStripMenuItem1
			});
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.salesperson;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(77, 24);
			toolStripButton1.Text = "More...";
			documentsToolStripMenuItem1.Name = "documentsToolStripMenuItem1";
			documentsToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
			documentsToolStripMenuItem1.Text = "Documents";
			skillsToolStripMenuItem1.Name = "skillsToolStripMenuItem1";
			skillsToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
			skillsToolStripMenuItem1.Text = "Skills";
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
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
			panel1.Size = new System.Drawing.Size(719, 30);
			panel1.TabIndex = 314;
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
			ultraTabSharedControlsPage2.Location = new System.Drawing.Point(1, 20);
			ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
			ultraTabSharedControlsPage2.Size = new System.Drawing.Size(196, 77);
			ultraTabPageControl2.Controls.Add(mmLabel28);
			ultraTabPageControl2.Controls.Add(mmLabel29);
			ultraTabPageControl2.Controls.Add(mmLabel30);
			ultraTabPageControl2.Controls.Add(mmLabel32);
			ultraTabPageControl2.Controls.Add(pictureBox1);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel4);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel5);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel6);
			ultraTabPageControl2.Controls.Add(pictureBox2);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel11);
			ultraTabPageControl2.Controls.Add(workLocationComboBox1);
			ultraTabPageControl2.Controls.Add(textBoxBirthPlace);
			ultraTabPageControl2.Controls.Add(comboBoxType);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel7);
			ultraTabPageControl2.Controls.Add(mmTextBox2);
			ultraTabPageControl2.Controls.Add(mmLabel33);
			ultraTabPageControl2.Controls.Add(comboBoxSponsor);
			ultraTabPageControl2.Controls.Add(mmLabel34);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel8);
			ultraTabPageControl2.Controls.Add(comboBoxMaritalStatus);
			ultraTabPageControl2.Controls.Add(mmLabel35);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel9);
			ultraTabPageControl2.Controls.Add(mmsDateTimePicker1);
			ultraTabPageControl2.Controls.Add(comboBoxNationality);
			ultraTabPageControl2.Controls.Add(mmLabel36);
			ultraTabPageControl2.Controls.Add(linkLabelCountry);
			ultraTabPageControl2.Controls.Add(genderComboBox1);
			ultraTabPageControl2.Controls.Add(dateTimePickerJoiningDate);
			ultraTabPageControl2.Controls.Add(mmLabel37);
			ultraTabPageControl2.Controls.Add(textBoxServicePeriod);
			ultraTabPageControl2.Controls.Add(textBoxNationalID);
			ultraTabPageControl2.Controls.Add(mmLabel38);
			ultraTabPageControl2.Controls.Add(comboBoxManager);
			ultraTabPageControl2.Controls.Add(comboBoxPosition);
			ultraTabPageControl2.Controls.Add(comboBoxDepartment);
			ultraTabPageControl2.Controls.Add(comboBoxDivision);
			ultraTabPageControl2.Controls.Add(comboBoxStatus);
			ultraTabPageControl2.Controls.Add(checkBoxOnVacation);
			ultraTabPageControl2.Controls.Add(comboBoxGrade);
			ultraTabPageControl2.Controls.Add(mmLabel39);
			ultraTabPageControl2.Controls.Add(comboBoxGroup);
			ultraTabPageControl2.Controls.Add(mmLabel40);
			ultraTabPageControl2.Controls.Add(mmLabel41);
			ultraTabPageControl2.Controls.Add(textBoxNickName);
			ultraTabPageControl2.Controls.Add(mmTextBox3);
			ultraTabPageControl2.Controls.Add(textBoxLastName);
			ultraTabPageControl2.Controls.Add(textBoxFirstName);
			ultraTabPageControl2.Controls.Add(mmLabel42);
			ultraTabPageControl2.Controls.Add(mmLabel43);
			ultraTabPageControl2.Controls.Add(textBoxMiddleName);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel10);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel13);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel14);
			ultraTabPageControl2.Controls.Add(ultraFormattedLinkLabel15);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(696, 420);
			pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBox1.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBox1.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBox1.Location = new System.Drawing.Point(623, 143);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(49, 48);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 67;
			pictureBox1.TabStop = false;
			pictureBox1.Visible = false;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(572, 143);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(45, 14);
			ultraFormattedLinkLabel4.TabIndex = 19;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Remove";
			appearance17.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance17;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(533, 143);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(23, 14);
			ultraFormattedLinkLabel5.TabIndex = 18;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Add";
			appearance18.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance18;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(558, 59);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(66, 14);
			ultraFormattedLinkLabel6.TabIndex = 66;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Load Picture";
			appearance19.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance19;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBox2.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBox2.Location = new System.Drawing.Point(529, 11);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new System.Drawing.Size(128, 128);
			pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBox2.TabIndex = 65;
			pictureBox2.TabStop = false;
			ultraFormattedLinkLabel11.AutoSize = true;
			ultraFormattedLinkLabel11.Location = new System.Drawing.Point(295, 122);
			ultraFormattedLinkLabel11.Name = "ultraFormattedLinkLabel11";
			ultraFormattedLinkLabel11.Size = new System.Drawing.Size(35, 14);
			ultraFormattedLinkLabel11.TabIndex = 64;
			ultraFormattedLinkLabel11.TabStop = true;
			ultraFormattedLinkLabel11.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel11.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel11.Value = "Class:";
			appearance20.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel11.VisitedLinkAppearance = appearance20;
			ultraFormattedLinkLabel11.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel11_LinkClicked);
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(9, 209);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(48, 14);
			ultraFormattedLinkLabel7.TabIndex = 58;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Sponsor:";
			appearance21.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance21;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(9, 249);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel8.TabIndex = 59;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Nationality:";
			appearance22.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance22;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			ultraFormattedLinkLabel9.AutoSize = true;
			ultraFormattedLinkLabel9.Location = new System.Drawing.Point(9, 185);
			ultraFormattedLinkLabel9.Name = "ultraFormattedLinkLabel9";
			ultraFormattedLinkLabel9.Size = new System.Drawing.Size(64, 14);
			ultraFormattedLinkLabel9.TabIndex = 58;
			ultraFormattedLinkLabel9.TabStop = true;
			ultraFormattedLinkLabel9.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel9.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel9.Value = "Department:";
			appearance23.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel9.VisitedLinkAppearance = appearance23;
			ultraFormattedLinkLabel9.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			linkLabelCountry.AutoSize = true;
			linkLabelCountry.Location = new System.Drawing.Point(9, 142);
			linkLabelCountry.Name = "linkLabelCountry";
			linkLabelCountry.Size = new System.Drawing.Size(49, 14);
			linkLabelCountry.TabIndex = 56;
			linkLabelCountry.TabStop = true;
			linkLabelCountry.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCountry.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCountry.Value = "Location:";
			appearance24.ForeColor = System.Drawing.Color.Blue;
			linkLabelCountry.VisitedLinkAppearance = appearance24;
			linkLabelCountry.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCountry_LinkClicked);
			checkBoxOnVacation.AutoSize = true;
			checkBoxOnVacation.Enabled = false;
			checkBoxOnVacation.Location = new System.Drawing.Point(437, 12);
			checkBoxOnVacation.Name = "checkBoxOnVacation";
			checkBoxOnVacation.Size = new System.Drawing.Size(85, 17);
			checkBoxOnVacation.TabIndex = 3;
			checkBoxOnVacation.Text = "On Vacation";
			checkBoxOnVacation.UseVisualStyleBackColor = true;
			ultraFormattedLinkLabel10.AutoSize = true;
			ultraFormattedLinkLabel10.Location = new System.Drawing.Point(9, 164);
			ultraFormattedLinkLabel10.Name = "ultraFormattedLinkLabel10";
			ultraFormattedLinkLabel10.Size = new System.Drawing.Size(46, 14);
			ultraFormattedLinkLabel10.TabIndex = 57;
			ultraFormattedLinkLabel10.TabStop = true;
			ultraFormattedLinkLabel10.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel10.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel10.Value = "Division:";
			appearance25.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.VisitedLinkAppearance = appearance25;
			ultraFormattedLinkLabel10.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			ultraFormattedLinkLabel13.AutoSize = true;
			ultraFormattedLinkLabel13.Location = new System.Drawing.Point(294, 144);
			ultraFormattedLinkLabel13.Name = "ultraFormattedLinkLabel13";
			ultraFormattedLinkLabel13.Size = new System.Drawing.Size(38, 14);
			ultraFormattedLinkLabel13.TabIndex = 59;
			ultraFormattedLinkLabel13.TabStop = true;
			ultraFormattedLinkLabel13.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel13.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel13.Value = "Group:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel13.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabel13.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			ultraFormattedLinkLabel14.AutoSize = true;
			ultraFormattedLinkLabel14.Location = new System.Drawing.Point(294, 165);
			ultraFormattedLinkLabel14.Name = "ultraFormattedLinkLabel14";
			ultraFormattedLinkLabel14.Size = new System.Drawing.Size(38, 14);
			ultraFormattedLinkLabel14.TabIndex = 60;
			ultraFormattedLinkLabel14.TabStop = true;
			ultraFormattedLinkLabel14.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel14.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel14.Value = "Grade:";
			appearance27.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel14.VisitedLinkAppearance = appearance27;
			ultraFormattedLinkLabel14.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			ultraFormattedLinkLabel15.AutoSize = true;
			ultraFormattedLinkLabel15.Location = new System.Drawing.Point(294, 188);
			ultraFormattedLinkLabel15.Name = "ultraFormattedLinkLabel15";
			ultraFormattedLinkLabel15.Size = new System.Drawing.Size(47, 14);
			ultraFormattedLinkLabel15.TabIndex = 61;
			ultraFormattedLinkLabel15.TabStop = true;
			ultraFormattedLinkLabel15.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel15.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel15.Value = "Position:";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel15.VisitedLinkAppearance = appearance28;
			ultraFormattedLinkLabel15.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel16);
			ultraTabPageControl3.Controls.Add(textBoxAppraisalPoints);
			ultraTabPageControl3.Controls.Add(mmLabel44);
			ultraTabPageControl3.Controls.Add(comboBoxQualification);
			ultraTabPageControl3.Controls.Add(ultraGroupBox4);
			ultraTabPageControl3.Controls.Add(textBoxLabourID);
			ultraTabPageControl3.Controls.Add(ultraGroupBox5);
			ultraTabPageControl3.Controls.Add(dateTimePickerConfirmation);
			ultraTabPageControl3.Controls.Add(mmLabel61);
			ultraTabPageControl3.Controls.Add(textBoxSpouse);
			ultraTabPageControl3.Controls.Add(mmLabel62);
			ultraTabPageControl3.Controls.Add(mmLabel63);
			ultraTabPageControl3.Controls.Add(textBoxBloodGroup);
			ultraTabPageControl3.Controls.Add(comboBoxReligion);
			ultraTabPageControl3.Controls.Add(mmLabel64);
			ultraTabPageControl3.Controls.Add(textBoxProbation);
			ultraTabPageControl3.Controls.Add(mmLabel65);
			ultraTabPageControl3.Controls.Add(comboBoxDayOff);
			ultraTabPageControl3.Controls.Add(mmLabel66);
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel19);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(696, 420);
			ultraFormattedLinkLabel16.AutoSize = true;
			ultraFormattedLinkLabel16.Location = new System.Drawing.Point(254, 61);
			ultraFormattedLinkLabel16.Name = "ultraFormattedLinkLabel16";
			ultraFormattedLinkLabel16.Size = new System.Drawing.Size(68, 14);
			ultraFormattedLinkLabel16.TabIndex = 70;
			ultraFormattedLinkLabel16.TabStop = true;
			ultraFormattedLinkLabel16.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel16.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel16.Value = "Qualification:";
			appearance29.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel16.VisitedLinkAppearance = appearance29;
			ultraFormattedLinkLabel16.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel15_LinkClicked);
			ultraGroupBox4.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox4.Controls.Add(mmLabel45);
			ultraGroupBox4.Controls.Add(comboBoxAccount);
			ultraGroupBox4.Controls.Add(textBoxIBAN);
			ultraGroupBox4.Controls.Add(textBoxAccountName);
			ultraGroupBox4.Controls.Add(ultraFormattedLinkLabel17);
			ultraGroupBox4.Controls.Add(comboBoxBank);
			ultraGroupBox4.Controls.Add(ultraFormattedLinkLabel18);
			ultraGroupBox4.Controls.Add(textBoxBankName);
			ultraGroupBox4.Location = new System.Drawing.Point(4, 97);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(687, 99);
			ultraGroupBox4.TabIndex = 7;
			ultraGroupBox4.Text = "Bank Details";
			ultraFormattedLinkLabel17.AutoSize = true;
			ultraFormattedLinkLabel17.Location = new System.Drawing.Point(9, 73);
			ultraFormattedLinkLabel17.Name = "ultraFormattedLinkLabel17";
			ultraFormattedLinkLabel17.Size = new System.Drawing.Size(47, 14);
			ultraFormattedLinkLabel17.TabIndex = 61;
			ultraFormattedLinkLabel17.TabStop = true;
			ultraFormattedLinkLabel17.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel17.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel17.Value = "Account:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel17.VisitedLinkAppearance = appearance30;
			ultraFormattedLinkLabel17.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			ultraFormattedLinkLabel18.AutoSize = true;
			ultraFormattedLinkLabel18.Location = new System.Drawing.Point(8, 51);
			ultraFormattedLinkLabel18.Name = "ultraFormattedLinkLabel18";
			ultraFormattedLinkLabel18.Size = new System.Drawing.Size(32, 14);
			ultraFormattedLinkLabel18.TabIndex = 64;
			ultraFormattedLinkLabel18.TabStop = true;
			ultraFormattedLinkLabel18.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel18.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel18.Value = "Bank:";
			appearance31.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel18.VisitedLinkAppearance = appearance31;
			ultraFormattedLinkLabel18.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel10_LinkClicked);
			ultraGroupBox5.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox5.Controls.Add(mmLabel46);
			ultraGroupBox5.Controls.Add(textBoxComment);
			ultraGroupBox5.Controls.Add(buttonMoreAddress);
			ultraGroupBox5.Controls.Add(mmLabel47);
			ultraGroupBox5.Controls.Add(textBoxPostalCode);
			ultraGroupBox5.Controls.Add(mmLabel48);
			ultraGroupBox5.Controls.Add(textBoxEmail);
			ultraGroupBox5.Controls.Add(mmLabel52);
			ultraGroupBox5.Controls.Add(textBoxMobile);
			ultraGroupBox5.Controls.Add(mmLabel53);
			ultraGroupBox5.Controls.Add(textBoxFax);
			ultraGroupBox5.Controls.Add(mmLabel54);
			ultraGroupBox5.Controls.Add(textBoxPhone2);
			ultraGroupBox5.Controls.Add(mmLabel55);
			ultraGroupBox5.Controls.Add(textBoxPhone1);
			ultraGroupBox5.Controls.Add(mmLabel56);
			ultraGroupBox5.Controls.Add(textBoxCountry);
			ultraGroupBox5.Controls.Add(mmLabel57);
			ultraGroupBox5.Controls.Add(textBoxState);
			ultraGroupBox5.Controls.Add(mmLabel58);
			ultraGroupBox5.Controls.Add(textBoxCity);
			ultraGroupBox5.Controls.Add(textBoxAddress3);
			ultraGroupBox5.Controls.Add(textBoxAddress2);
			ultraGroupBox5.Controls.Add(mmLabel59);
			ultraGroupBox5.Controls.Add(textBoxAddress1);
			ultraGroupBox5.Controls.Add(mmLabel60);
			ultraGroupBox5.Controls.Add(textBoxAddressID);
			ultraGroupBox5.Location = new System.Drawing.Point(3, 202);
			ultraGroupBox5.Name = "ultraGroupBox5";
			ultraGroupBox5.Size = new System.Drawing.Size(687, 215);
			ultraGroupBox5.TabIndex = 8;
			ultraGroupBox5.Text = "Primary Address";
			ultraFormattedLinkLabel19.AutoSize = true;
			ultraFormattedLinkLabel19.Location = new System.Drawing.Point(398, 15);
			ultraFormattedLinkLabel19.Name = "ultraFormattedLinkLabel19";
			ultraFormattedLinkLabel19.Size = new System.Drawing.Size(47, 14);
			ultraFormattedLinkLabel19.TabIndex = 60;
			ultraFormattedLinkLabel19.TabStop = true;
			ultraFormattedLinkLabel19.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel19.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel19.Value = "Religion:";
			appearance32.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel19.VisitedLinkAppearance = appearance32;
			ultraFormattedLinkLabel19.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel9_LinkClicked);
			tabPageUserDefined.Controls.Add(udfEntryGrid1);
			tabPageUserDefined.Location = new System.Drawing.Point(2, 21);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(696, 420);
			ultraTabControl2.Controls.Add(ultraTabPageControl2);
			ultraTabControl2.Controls.Add(ultraTabPageControl3);
			ultraTabControl2.Controls.Add(tabPageUserDefined);
			ultraTabControl2.Location = new System.Drawing.Point(0, 0);
			ultraTabControl2.Name = "ultraTabControl2";
			ultraTabControl2.SharedControlsPage = ultraTabSharedControlsPage2;
			ultraTabControl2.Size = new System.Drawing.Size(200, 100);
			ultraTabControl2.TabIndex = 0;
			ultraFormattedLinkLabel28.AutoSize = true;
			ultraFormattedLinkLabel28.Location = new System.Drawing.Point(304, 26);
			ultraFormattedLinkLabel28.Name = "ultraFormattedLinkLabel28";
			ultraFormattedLinkLabel28.Size = new System.Drawing.Size(32, 14);
			ultraFormattedLinkLabel28.TabIndex = 100;
			ultraFormattedLinkLabel28.TabStop = true;
			ultraFormattedLinkLabel28.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel28.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel28.Value = "Type:";
			appearance33.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel28.VisitedLinkAppearance = appearance33;
			ultraFormattedLinkLabel28.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel28_LinkClicked);
			ultraFormattedLinkLabel29.AutoSize = true;
			ultraFormattedLinkLabel29.Location = new System.Drawing.Point(321, 207);
			ultraFormattedLinkLabel29.Name = "ultraFormattedLinkLabel29";
			ultraFormattedLinkLabel29.Size = new System.Drawing.Size(52, 14);
			ultraFormattedLinkLabel29.TabIndex = 100;
			ultraFormattedLinkLabel29.TabStop = true;
			ultraFormattedLinkLabel29.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel29.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel29.Value = "Category:";
			appearance34.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel29.VisitedLinkAppearance = appearance34;
			ultraFormattedLinkLabel29.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel29_LinkClicked);
			mmLabel67.AutoSize = true;
			mmLabel67.BackColor = System.Drawing.Color.Transparent;
			mmLabel67.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel67.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel67.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel67.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel67.IsFieldHeader = false;
			mmLabel67.IsRequired = false;
			mmLabel67.Location = new System.Drawing.Point(9, 187);
			mmLabel67.Name = "mmLabel67";
			mmLabel67.PenWidth = 1f;
			mmLabel67.ShowBorder = false;
			mmLabel67.Size = new System.Drawing.Size(36, 13);
			mmLabel67.TabIndex = 93;
			mmLabel67.Text = "Color:";
			textBoxSireOfDam.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxSireOfDam.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxSireOfDam.BackColor = System.Drawing.Color.White;
			textBoxSireOfDam.CustomReportFieldName = "";
			textBoxSireOfDam.CustomReportKey = "";
			textBoxSireOfDam.CustomReportValueType = 1;
			textBoxSireOfDam.IsComboTextBox = false;
			textBoxSireOfDam.Location = new System.Drawing.Point(132, 228);
			textBoxSireOfDam.MaxLength = 30;
			textBoxSireOfDam.Name = "textBoxSireOfDam";
			textBoxSireOfDam.Size = new System.Drawing.Size(151, 20);
			textBoxSireOfDam.TabIndex = 14;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(9, 233);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(68, 13);
			mmLabel9.TabIndex = 37;
			mmLabel9.Text = "Sire Of Dam:";
			textBoxBreeder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxBreeder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxBreeder.BackColor = System.Drawing.Color.White;
			textBoxBreeder.CustomReportFieldName = "";
			textBoxBreeder.CustomReportKey = "";
			textBoxBreeder.CustomReportValueType = 1;
			textBoxBreeder.IsComboTextBox = false;
			textBoxBreeder.Location = new System.Drawing.Point(375, 251);
			textBoxBreeder.MaxLength = 30;
			textBoxBreeder.Name = "textBoxBreeder";
			textBoxBreeder.Size = new System.Drawing.Size(141, 20);
			textBoxBreeder.TabIndex = 17;
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(320, 254);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(49, 13);
			mmLabel25.TabIndex = 91;
			mmLabel25.Text = "Breeder:";
			mmLabel24.AutoSize = true;
			mmLabel24.BackColor = System.Drawing.Color.Transparent;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel24.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(302, 47);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(135, 13);
			mmLabel24.TabIndex = 88;
			mmLabel24.Text = "OwnerShip Changed Date:";
			datetimePickerOwnerShipChanged.Checked = false;
			datetimePickerOwnerShipChanged.CustomFormat = " ";
			datetimePickerOwnerShipChanged.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimePickerOwnerShipChanged.Location = new System.Drawing.Point(461, 44);
			datetimePickerOwnerShipChanged.Name = "datetimePickerOwnerShipChanged";
			datetimePickerOwnerShipChanged.ShowCheckBox = true;
			datetimePickerOwnerShipChanged.Size = new System.Drawing.Size(151, 20);
			datetimePickerOwnerShipChanged.TabIndex = 4;
			datetimePickerOwnerShipChanged.Value = new System.DateTime(0L);
			mmLabel21.AutoSize = true;
			mmLabel21.BackColor = System.Drawing.Color.Transparent;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel21.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(12, 25);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(103, 13);
			mmLabel21.TabIndex = 0;
			mmLabel21.Text = "Current OwnerShip:";
			textBoxPreviousOwnerShip.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxPreviousOwnerShip.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxPreviousOwnerShip.BackColor = System.Drawing.Color.White;
			textBoxPreviousOwnerShip.CustomReportFieldName = "";
			textBoxPreviousOwnerShip.CustomReportKey = "";
			textBoxPreviousOwnerShip.CustomReportValueType = 1;
			textBoxPreviousOwnerShip.IsComboTextBox = false;
			textBoxPreviousOwnerShip.Location = new System.Drawing.Point(123, 45);
			textBoxPreviousOwnerShip.MaxLength = 30;
			textBoxPreviousOwnerShip.Name = "textBoxPreviousOwnerShip";
			textBoxPreviousOwnerShip.Size = new System.Drawing.Size(151, 20);
			textBoxPreviousOwnerShip.TabIndex = 2;
			textBoxCurrentOwnerShip.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxCurrentOwnerShip.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxCurrentOwnerShip.BackColor = System.Drawing.Color.White;
			textBoxCurrentOwnerShip.CustomReportFieldName = "";
			textBoxCurrentOwnerShip.CustomReportKey = "";
			textBoxCurrentOwnerShip.CustomReportValueType = 1;
			textBoxCurrentOwnerShip.IsComboTextBox = false;
			textBoxCurrentOwnerShip.Location = new System.Drawing.Point(123, 22);
			textBoxCurrentOwnerShip.MaxLength = 30;
			textBoxCurrentOwnerShip.Name = "textBoxCurrentOwnerShip";
			textBoxCurrentOwnerShip.Size = new System.Drawing.Size(151, 20);
			textBoxCurrentOwnerShip.TabIndex = 1;
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(11, 47);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(107, 13);
			mmLabel22.TabIndex = 85;
			mmLabel22.Text = "Previous OwnerShip:";
			textBoxDam.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxDam.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxDam.BackColor = System.Drawing.Color.White;
			textBoxDam.CustomReportFieldName = "";
			textBoxDam.CustomReportKey = "";
			textBoxDam.CustomReportValueType = 1;
			textBoxDam.IsComboTextBox = false;
			textBoxDam.Location = new System.Drawing.Point(375, 228);
			textBoxDam.MaxLength = 30;
			textBoxDam.Name = "textBoxDam";
			textBoxDam.Size = new System.Drawing.Size(142, 20);
			textBoxDam.TabIndex = 15;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(326, 229);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(32, 13);
			mmLabel3.TabIndex = 79;
			mmLabel3.Text = "Dam:";
			textBoxSire.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxSire.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxSire.BackColor = System.Drawing.Color.White;
			textBoxSire.CustomReportFieldName = "";
			textBoxSire.CustomReportKey = "";
			textBoxSire.CustomReportValueType = 1;
			textBoxSire.IsComboTextBox = false;
			textBoxSire.Location = new System.Drawing.Point(132, 206);
			textBoxSire.MaxLength = 30;
			textBoxSire.Name = "textBoxSire";
			textBoxSire.Size = new System.Drawing.Size(151, 20);
			textBoxSire.TabIndex = 12;
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
			labelEmployeeNumber.Size = new System.Drawing.Size(74, 13);
			labelEmployeeNumber.TabIndex = 28;
			labelEmployeeNumber.Text = "Horse Code:";
			labelEmployeeNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxAge.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxAge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxAge.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAge.CustomReportFieldName = "";
			textBoxAge.CustomReportKey = "";
			textBoxAge.CustomReportValueType = 1;
			textBoxAge.IsComboTextBox = false;
			textBoxAge.Location = new System.Drawing.Point(447, 162);
			textBoxAge.MaxLength = 30;
			textBoxAge.Name = "textBoxAge";
			textBoxAge.ReadOnly = true;
			textBoxAge.Size = new System.Drawing.Size(69, 20);
			textBoxAge.TabIndex = 9;
			textBoxAge.TabStop = false;
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(9, 165);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(59, 13);
			mmLabel31.TabIndex = 34;
			mmLabel31.Text = "Birth Date:";
			mmLabel51.AutoSize = true;
			mmLabel51.BackColor = System.Drawing.Color.Transparent;
			mmLabel51.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel51.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel51.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel51.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel51.IsFieldHeader = false;
			mmLabel51.IsRequired = false;
			mmLabel51.Location = new System.Drawing.Point(412, 164);
			mmLabel51.Name = "mmLabel51";
			mmLabel51.PenWidth = 1f;
			mmLabel51.ShowBorder = false;
			mmLabel51.Size = new System.Drawing.Size(30, 13);
			mmLabel51.TabIndex = 8;
			mmLabel51.Text = "Age:";
			dateTimePickerBirthDate.Checked = false;
			dateTimePickerBirthDate.CustomFormat = " ";
			dateTimePickerBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerBirthDate.Location = new System.Drawing.Point(132, 162);
			dateTimePickerBirthDate.Name = "dateTimePickerBirthDate";
			dateTimePickerBirthDate.ShowCheckBox = true;
			dateTimePickerBirthDate.Size = new System.Drawing.Size(151, 20);
			dateTimePickerBirthDate.TabIndex = 8;
			dateTimePickerBirthDate.Value = new System.DateTime(0L);
			dateTimePickerBirthDate.ValueChanged += new System.EventHandler(dateTimePickerBirthDate_ValueChanged);
			mmLabel50.AutoSize = true;
			mmLabel50.BackColor = System.Drawing.Color.Transparent;
			mmLabel50.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel50.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel50.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel50.IsFieldHeader = false;
			mmLabel50.IsRequired = false;
			mmLabel50.Location = new System.Drawing.Point(9, 209);
			mmLabel50.Name = "mmLabel50";
			mmLabel50.PenWidth = 1f;
			mmLabel50.ShowBorder = false;
			mmLabel50.Size = new System.Drawing.Size(29, 13);
			mmLabel50.TabIndex = 36;
			mmLabel50.Text = "Sire:";
			textBoxBreed.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxBreed.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxBreed.BackColor = System.Drawing.Color.White;
			textBoxBreed.CustomReportFieldName = "";
			textBoxBreed.CustomReportKey = "";
			textBoxBreed.CustomReportValueType = 1;
			textBoxBreed.IsComboTextBox = false;
			textBoxBreed.Location = new System.Drawing.Point(132, 141);
			textBoxBreed.MaxLength = 30;
			textBoxBreed.Name = "textBoxBreed";
			textBoxBreed.Size = new System.Drawing.Size(385, 20);
			textBoxBreed.TabIndex = 7;
			mmLabel49.AutoSize = true;
			mmLabel49.BackColor = System.Drawing.Color.Transparent;
			mmLabel49.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel49.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel49.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel49.IsFieldHeader = false;
			mmLabel49.IsRequired = false;
			mmLabel49.Location = new System.Drawing.Point(9, 143);
			mmLabel49.Name = "mmLabel49";
			mmLabel49.PenWidth = 1f;
			mmLabel49.ShowBorder = false;
			mmLabel49.Size = new System.Drawing.Size(39, 13);
			mmLabel49.TabIndex = 33;
			mmLabel49.Text = "Breed:";
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(9, 121);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(39, 13);
			mmLabel5.TabIndex = 32;
			mmLabel5.Text = "Brand:";
			lblDescriptions.AutoSize = true;
			lblDescriptions.BackColor = System.Drawing.Color.Transparent;
			lblDescriptions.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblDescriptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDescriptions.IsFieldHeader = false;
			lblDescriptions.IsRequired = false;
			lblDescriptions.Location = new System.Drawing.Point(9, 99);
			lblDescriptions.Name = "lblDescriptions";
			lblDescriptions.PenWidth = 1f;
			lblDescriptions.ShowBorder = false;
			lblDescriptions.Size = new System.Drawing.Size(95, 13);
			lblDescriptions.TabIndex = 31;
			lblDescriptions.Text = "Microchip Number:";
			textBoxBrand.BackColor = System.Drawing.Color.White;
			textBoxBrand.CustomReportFieldName = "";
			textBoxBrand.CustomReportKey = "";
			textBoxBrand.CustomReportValueType = 1;
			textBoxBrand.IsComboTextBox = false;
			textBoxBrand.Location = new System.Drawing.Point(132, 119);
			textBoxBrand.MaxLength = 30;
			textBoxBrand.Name = "textBoxBrand";
			textBoxBrand.Size = new System.Drawing.Size(385, 20);
			textBoxBrand.TabIndex = 6;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(132, 9);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(161, 20);
			textBoxCode.TabIndex = 0;
			textBoxRegisterNumber.BackColor = System.Drawing.Color.White;
			textBoxRegisterNumber.CustomReportFieldName = "";
			textBoxRegisterNumber.CustomReportKey = "";
			textBoxRegisterNumber.CustomReportValueType = 1;
			textBoxRegisterNumber.IsComboTextBox = false;
			textBoxRegisterNumber.IsRequired = true;
			textBoxRegisterNumber.Location = new System.Drawing.Point(132, 75);
			textBoxRegisterNumber.MaxLength = 30;
			textBoxRegisterNumber.Name = "textBoxRegisterNumber";
			textBoxRegisterNumber.Size = new System.Drawing.Size(385, 20);
			textBoxRegisterNumber.TabIndex = 4;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsRequired = true;
			textBoxName.Location = new System.Drawing.Point(132, 53);
			textBoxName.MaxLength = 30;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(385, 20);
			textBoxName.TabIndex = 3;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(9, 76);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(91, 13);
			mmLabel6.TabIndex = 30;
			mmLabel6.Text = "Register Number:";
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f);
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.IsFieldHeader = false;
			label1.IsRequired = false;
			label1.Location = new System.Drawing.Point(9, 53);
			label1.Name = "label1";
			label1.PenWidth = 1f;
			label1.ShowBorder = false;
			label1.Size = new System.Drawing.Size(69, 13);
			label1.TabIndex = 29;
			label1.Text = "Horse Name:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxMicroChipNumber.BackColor = System.Drawing.Color.White;
			textBoxMicroChipNumber.CustomReportFieldName = "";
			textBoxMicroChipNumber.CustomReportKey = "";
			textBoxMicroChipNumber.CustomReportValueType = 1;
			textBoxMicroChipNumber.IsComboTextBox = false;
			textBoxMicroChipNumber.Location = new System.Drawing.Point(132, 97);
			textBoxMicroChipNumber.MaxLength = 30;
			textBoxMicroChipNumber.Name = "textBoxMicroChipNumber";
			textBoxMicroChipNumber.Size = new System.Drawing.Size(385, 20);
			textBoxMicroChipNumber.TabIndex = 5;
			dateTimeOwnerShip.Checked = false;
			dateTimeOwnerShip.CustomFormat = " ";
			dateTimeOwnerShip.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeOwnerShip.Location = new System.Drawing.Point(131, 189);
			dateTimeOwnerShip.Name = "dateTimeOwnerShip";
			dateTimeOwnerShip.ShowCheckBox = true;
			dateTimeOwnerShip.Size = new System.Drawing.Size(151, 20);
			dateTimeOwnerShip.TabIndex = 13;
			dateTimeOwnerShip.Value = new System.DateTime(0L);
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(17, 228);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(96, 13);
			mmLabel11.TabIndex = 28;
			mmLabel11.Text = "Past Performance:";
			textBoxPastPerformance.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxPastPerformance.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxPastPerformance.BackColor = System.Drawing.Color.White;
			textBoxPastPerformance.CustomReportFieldName = "";
			textBoxPastPerformance.CustomReportKey = "";
			textBoxPastPerformance.CustomReportValueType = 1;
			textBoxPastPerformance.IsComboTextBox = false;
			textBoxPastPerformance.Location = new System.Drawing.Point(128, 226);
			textBoxPastPerformance.MaxLength = 30;
			textBoxPastPerformance.Multiline = true;
			textBoxPastPerformance.Name = "textBoxPastPerformance";
			textBoxPastPerformance.Size = new System.Drawing.Size(429, 68);
			textBoxPastPerformance.TabIndex = 15;
			mmLabel26.AutoSize = true;
			mmLabel26.BackColor = System.Drawing.Color.Transparent;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(17, 188);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(85, 26);
			mmLabel26.TabIndex = 27;
			mmLabel26.Text = "OwnerShip \r\nTransferred On:";
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(304, 192);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(53, 13);
			mmLabel27.TabIndex = 114;
			mmLabel27.Text = "Dead On:";
			dateTimePickerDead.Checked = false;
			dateTimePickerDead.CustomFormat = " ";
			dateTimePickerDead.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerDead.Location = new System.Drawing.Point(416, 189);
			dateTimePickerDead.Name = "dateTimePickerDead";
			dateTimePickerDead.ShowCheckBox = true;
			dateTimePickerDead.Size = new System.Drawing.Size(144, 20);
			dateTimePickerDead.TabIndex = 14;
			dateTimePickerDead.Value = new System.DateTime(0L);
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(304, 169);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(101, 13);
			mmLabel23.TabIndex = 110;
			mmLabel23.Text = "Sex Changed Date:";
			dateTimePickerSexChange.Checked = false;
			dateTimePickerSexChange.CustomFormat = " ";
			dateTimePickerSexChange.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerSexChange.Location = new System.Drawing.Point(416, 166);
			dateTimePickerSexChange.Name = "dateTimePickerSexChange";
			dateTimePickerSexChange.ShowCheckBox = true;
			dateTimePickerSexChange.Size = new System.Drawing.Size(144, 20);
			dateTimePickerSexChange.TabIndex = 12;
			dateTimePickerSexChange.Value = new System.DateTime(0L);
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(304, 146);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(57, 13);
			mmLabel18.TabIndex = 106;
			mmLabel18.Text = "Sold Date:";
			dateTimePickerSold.Checked = false;
			dateTimePickerSold.CustomFormat = " ";
			dateTimePickerSold.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerSold.Location = new System.Drawing.Point(416, 143);
			dateTimePickerSold.Name = "dateTimePickerSold";
			dateTimePickerSold.ShowCheckBox = true;
			dateTimePickerSold.Size = new System.Drawing.Size(144, 20);
			dateTimePickerSold.TabIndex = 10;
			dateTimePickerSold.Value = new System.DateTime(0L);
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(304, 122);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(94, 13);
			mmLabel16.TabIndex = 102;
			mmLabel16.Text = "Transferred Date:";
			dateTimePickerTransferred.Checked = false;
			dateTimePickerTransferred.CustomFormat = " ";
			dateTimePickerTransferred.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerTransferred.Location = new System.Drawing.Point(416, 120);
			dateTimePickerTransferred.Name = "dateTimePickerTransferred";
			dateTimePickerTransferred.ShowCheckBox = true;
			dateTimePickerTransferred.Size = new System.Drawing.Size(144, 20);
			dateTimePickerTransferred.TabIndex = 8;
			dateTimePickerTransferred.Value = new System.DateTime(0L);
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(304, 100);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(81, 13);
			mmLabel14.TabIndex = 98;
			mmLabel14.Text = "Received Date:";
			dateTimePickerReceived.Checked = false;
			dateTimePickerReceived.CustomFormat = " ";
			dateTimePickerReceived.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerReceived.Location = new System.Drawing.Point(416, 97);
			dateTimePickerReceived.Name = "dateTimePickerReceived";
			dateTimePickerReceived.ShowCheckBox = true;
			dateTimePickerReceived.Size = new System.Drawing.Size(144, 20);
			dateTimePickerReceived.TabIndex = 6;
			dateTimePickerReceived.Value = new System.DateTime(0L);
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(304, 54);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(81, 13);
			mmLabel4.TabIndex = 90;
			mmLabel4.Text = "Imported Date:";
			dateTimePickerImported.Checked = false;
			dateTimePickerImported.CustomFormat = " ";
			dateTimePickerImported.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerImported.Location = new System.Drawing.Point(416, 51);
			dateTimePickerImported.Name = "dateTimePickerImported";
			dateTimePickerImported.ShowCheckBox = true;
			dateTimePickerImported.Size = new System.Drawing.Size(144, 20);
			dateTimePickerImported.TabIndex = 3;
			dateTimePickerImported.Value = new System.DateTime(0L);
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(17, 32);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(91, 13);
			mmLabel8.TabIndex = 20;
			mmLabel8.Text = "Revalidated until:";
			dateTimePickerRevalidation.Checked = false;
			dateTimePickerRevalidation.CustomFormat = " ";
			dateTimePickerRevalidation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerRevalidation.Location = new System.Drawing.Point(131, 29);
			dateTimePickerRevalidation.Name = "dateTimePickerRevalidation";
			dateTimePickerRevalidation.ShowCheckBox = true;
			dateTimePickerRevalidation.Size = new System.Drawing.Size(151, 20);
			dateTimePickerRevalidation.TabIndex = 1;
			dateTimePickerRevalidation.Value = new System.DateTime(0L);
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(286, 24);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(112, 13);
			mmLabel2.TabIndex = 90;
			mmLabel2.Text = "Passport Expiry Date:";
			datetimePickerPassportExpiry.Checked = false;
			datetimePickerPassportExpiry.CustomFormat = " ";
			datetimePickerPassportExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimePickerPassportExpiry.Location = new System.Drawing.Point(416, 22);
			datetimePickerPassportExpiry.Name = "datetimePickerPassportExpiry";
			datetimePickerPassportExpiry.ShowCheckBox = true;
			datetimePickerPassportExpiry.Size = new System.Drawing.Size(144, 20);
			datetimePickerPassportExpiry.TabIndex = 3;
			datetimePickerPassportExpiry.Value = new System.DateTime(0L);
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(17, 28);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(108, 13);
			mmLabel1.TabIndex = 1;
			mmLabel1.Text = "Passport Issue Date:";
			datetimePickerPassportIssue.Checked = false;
			datetimePickerPassportIssue.CustomFormat = " ";
			datetimePickerPassportIssue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimePickerPassportIssue.Location = new System.Drawing.Point(131, 25);
			datetimePickerPassportIssue.Name = "datetimePickerPassportIssue";
			datetimePickerPassportIssue.ShowCheckBox = true;
			datetimePickerPassportIssue.Size = new System.Drawing.Size(151, 20);
			datetimePickerPassportIssue.TabIndex = 2;
			datetimePickerPassportIssue.Value = new System.DateTime(0L);
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
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(719, 1);
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
			buttonDelete.TabIndex = 3;
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
			buttonClose.Location = new System.Drawing.Point(609, 8);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(95, 24);
			buttonClose.TabIndex = 4;
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
			buttonNew.TabIndex = 2;
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
			buttonSave.TabIndex = 1;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			mmLabel28.AutoSize = true;
			mmLabel28.BackColor = System.Drawing.Color.Transparent;
			mmLabel28.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel28.IsFieldHeader = false;
			mmLabel28.IsRequired = false;
			mmLabel28.Location = new System.Drawing.Point(442, 270);
			mmLabel28.Name = "mmLabel28";
			mmLabel28.PenWidth = 1f;
			mmLabel28.ShowBorder = false;
			mmLabel28.Size = new System.Drawing.Size(77, 13);
			mmLabel28.TabIndex = 74;
			mmLabel28.Text = "Marital Status:";
			mmLabel29.AutoSize = true;
			mmLabel29.BackColor = System.Drawing.Color.Transparent;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(299, 12);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(42, 13);
			mmLabel29.TabIndex = 73;
			mmLabel29.Text = "Status:";
			mmLabel30.AutoSize = true;
			mmLabel30.BackColor = System.Drawing.Color.Transparent;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel30.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(292, 210);
			mmLabel30.Name = "mmLabel30";
			mmLabel30.PenWidth = 1f;
			mmLabel30.ShowBorder = false;
			mmLabel30.Size = new System.Drawing.Size(53, 13);
			mmLabel30.TabIndex = 72;
			mmLabel30.Text = "Manager:";
			mmLabel32.AutoSize = true;
			mmLabel32.BackColor = System.Drawing.Color.Transparent;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(9, 9);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(96, 13);
			mmLabel32.TabIndex = 68;
			mmLabel32.Text = "Employee Code:";
			mmLabel32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxBirthPlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxBirthPlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxBirthPlace.BackColor = System.Drawing.Color.White;
			textBoxBirthPlace.CustomReportFieldName = "";
			textBoxBirthPlace.CustomReportKey = "";
			textBoxBirthPlace.CustomReportValueType = 1;
			textBoxBirthPlace.IsComboTextBox = false;
			textBoxBirthPlace.Location = new System.Drawing.Point(371, 244);
			textBoxBirthPlace.MaxLength = 30;
			textBoxBirthPlace.Name = "textBoxBirthPlace";
			textBoxBirthPlace.Size = new System.Drawing.Size(146, 20);
			textBoxBirthPlace.TabIndex = 21;
			mmTextBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			mmTextBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			mmTextBox2.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBox2.CustomReportFieldName = "";
			mmTextBox2.CustomReportKey = "";
			mmTextBox2.CustomReportValueType = 1;
			mmTextBox2.IsComboTextBox = false;
			mmTextBox2.Location = new System.Drawing.Point(371, 265);
			mmTextBox2.MaxLength = 30;
			mmTextBox2.Name = "mmTextBox2";
			mmTextBox2.ReadOnly = true;
			mmTextBox2.Size = new System.Drawing.Size(68, 20);
			mmTextBox2.TabIndex = 24;
			mmTextBox2.TabStop = false;
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
			mmLabel34.AutoSize = true;
			mmLabel34.BackColor = System.Drawing.Color.Transparent;
			mmLabel34.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel34.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel34.IsFieldHeader = false;
			mmLabel34.IsRequired = false;
			mmLabel34.Location = new System.Drawing.Point(9, 269);
			mmLabel34.Name = "mmLabel34";
			mmLabel34.PenWidth = 1f;
			mmLabel34.ShowBorder = false;
			mmLabel34.Size = new System.Drawing.Size(59, 13);
			mmLabel34.TabIndex = 29;
			mmLabel34.Text = "Birth Date:";
			mmLabel35.AutoSize = true;
			mmLabel35.BackColor = System.Drawing.Color.Transparent;
			mmLabel35.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel35.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel35.IsFieldHeader = false;
			mmLabel35.IsRequired = false;
			mmLabel35.Location = new System.Drawing.Point(290, 268);
			mmLabel35.Name = "mmLabel35";
			mmLabel35.PenWidth = 1f;
			mmLabel35.ShowBorder = false;
			mmLabel35.Size = new System.Drawing.Size(30, 13);
			mmLabel35.TabIndex = 57;
			mmLabel35.Text = "Age:";
			mmsDateTimePicker1.Checked = false;
			mmsDateTimePicker1.CustomFormat = " ";
			mmsDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			mmsDateTimePicker1.Location = new System.Drawing.Point(132, 266);
			mmsDateTimePicker1.Name = "mmsDateTimePicker1";
			mmsDateTimePicker1.ShowCheckBox = true;
			mmsDateTimePicker1.Size = new System.Drawing.Size(151, 20);
			mmsDateTimePicker1.TabIndex = 23;
			mmsDateTimePicker1.Value = new System.DateTime(0L);
			mmsDateTimePicker1.ValueChanged += new System.EventHandler(dateTimePickerBirthDate_ValueChanged);
			mmLabel36.AutoSize = true;
			mmLabel36.BackColor = System.Drawing.Color.Transparent;
			mmLabel36.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel36.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel36.IsFieldHeader = false;
			mmLabel36.IsRequired = false;
			mmLabel36.Location = new System.Drawing.Point(525, 247);
			mmLabel36.Name = "mmLabel36";
			mmLabel36.PenWidth = 1f;
			mmLabel36.ShowBorder = false;
			mmLabel36.Size = new System.Drawing.Size(46, 13);
			mmLabel36.TabIndex = 0;
			mmLabel36.Text = "Gender:";
			dateTimePickerJoiningDate.Checked = false;
			dateTimePickerJoiningDate.CustomFormat = " ";
			dateTimePickerJoiningDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerJoiningDate.Location = new System.Drawing.Point(132, 288);
			dateTimePickerJoiningDate.Name = "dateTimePickerJoiningDate";
			dateTimePickerJoiningDate.ShowCheckBox = true;
			dateTimePickerJoiningDate.Size = new System.Drawing.Size(151, 20);
			dateTimePickerJoiningDate.TabIndex = 26;
			dateTimePickerJoiningDate.Value = new System.DateTime(0L);
			dateTimePickerJoiningDate.ValueChanged += new System.EventHandler(dateTimePickerJoiningDate_ValueChanged);
			mmLabel37.AutoSize = true;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(290, 288);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(79, 13);
			mmLabel37.TabIndex = 55;
			mmLabel37.Text = "Service Period:";
			textBoxServicePeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxServicePeriod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxServicePeriod.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxServicePeriod.CustomReportFieldName = "";
			textBoxServicePeriod.CustomReportKey = "";
			textBoxServicePeriod.CustomReportValueType = 1;
			textBoxServicePeriod.IsComboTextBox = false;
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
			textBoxNationalID.Location = new System.Drawing.Point(132, 119);
			textBoxNationalID.MaxLength = 30;
			textBoxNationalID.Name = "textBoxNationalID";
			textBoxNationalID.Size = new System.Drawing.Size(151, 20);
			textBoxNationalID.TabIndex = 8;
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(9, 121);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(64, 13);
			mmLabel38.TabIndex = 53;
			mmLabel38.Text = "National ID:";
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(9, 288);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(70, 13);
			mmLabel39.TabIndex = 27;
			mmLabel39.Text = "Joining Date:";
			mmLabel40.AutoSize = true;
			mmLabel40.BackColor = System.Drawing.Color.Transparent;
			mmLabel40.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel40.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel40.IsFieldHeader = false;
			mmLabel40.IsRequired = false;
			mmLabel40.Location = new System.Drawing.Point(9, 99);
			mmLabel40.Name = "mmLabel40";
			mmLabel40.PenWidth = 1f;
			mmLabel40.ShowBorder = false;
			mmLabel40.Size = new System.Drawing.Size(60, 13);
			mmLabel40.TabIndex = 8;
			mmLabel40.Text = "Nick Name:";
			mmLabel41.AutoSize = true;
			mmLabel41.BackColor = System.Drawing.Color.Transparent;
			mmLabel41.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel41.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel41.IsFieldHeader = false;
			mmLabel41.IsRequired = false;
			mmLabel41.Location = new System.Drawing.Point(9, 77);
			mmLabel41.Name = "mmLabel41";
			mmLabel41.PenWidth = 1f;
			mmLabel41.ShowBorder = false;
			mmLabel41.Size = new System.Drawing.Size(71, 13);
			mmLabel41.TabIndex = 6;
			mmLabel41.Text = "Middle Name:";
			textBoxNickName.BackColor = System.Drawing.Color.White;
			textBoxNickName.CustomReportFieldName = "";
			textBoxNickName.CustomReportKey = "";
			textBoxNickName.CustomReportValueType = 1;
			textBoxNickName.IsComboTextBox = false;
			textBoxNickName.Location = new System.Drawing.Point(132, 97);
			textBoxNickName.MaxLength = 30;
			textBoxNickName.Name = "textBoxNickName";
			textBoxNickName.Size = new System.Drawing.Size(385, 20);
			textBoxNickName.TabIndex = 7;
			mmTextBox3.BackColor = System.Drawing.Color.White;
			mmTextBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			mmTextBox3.CustomReportFieldName = "";
			mmTextBox3.CustomReportKey = "";
			mmTextBox3.CustomReportValueType = 1;
			mmTextBox3.IsComboTextBox = false;
			mmTextBox3.Location = new System.Drawing.Point(132, 9);
			mmTextBox3.MaxLength = 64;
			mmTextBox3.Name = "mmTextBox3";
			mmTextBox3.Size = new System.Drawing.Size(161, 20);
			mmTextBox3.TabIndex = 0;
			textBoxLastName.BackColor = System.Drawing.Color.White;
			textBoxLastName.CustomReportFieldName = "";
			textBoxLastName.CustomReportKey = "";
			textBoxLastName.CustomReportValueType = 1;
			textBoxLastName.IsComboTextBox = false;
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
			textBoxFirstName.IsRequired = true;
			textBoxFirstName.Location = new System.Drawing.Point(132, 31);
			textBoxFirstName.MaxLength = 30;
			textBoxFirstName.Name = "textBoxFirstName";
			textBoxFirstName.Size = new System.Drawing.Size(385, 20);
			textBoxFirstName.TabIndex = 4;
			mmLabel42.AutoSize = true;
			mmLabel42.BackColor = System.Drawing.Color.Transparent;
			mmLabel42.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel42.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel42.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel42.IsFieldHeader = false;
			mmLabel42.IsRequired = false;
			mmLabel42.Location = new System.Drawing.Point(9, 54);
			mmLabel42.Name = "mmLabel42";
			mmLabel42.PenWidth = 1f;
			mmLabel42.ShowBorder = false;
			mmLabel42.Size = new System.Drawing.Size(61, 13);
			mmLabel42.TabIndex = 4;
			mmLabel42.Text = "Last Name:";
			mmLabel43.AutoSize = true;
			mmLabel43.BackColor = System.Drawing.Color.Transparent;
			mmLabel43.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel43.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel43.IsFieldHeader = false;
			mmLabel43.IsRequired = false;
			mmLabel43.Location = new System.Drawing.Point(9, 31);
			mmLabel43.Name = "mmLabel43";
			mmLabel43.PenWidth = 1f;
			mmLabel43.ShowBorder = false;
			mmLabel43.Size = new System.Drawing.Size(62, 13);
			mmLabel43.TabIndex = 2;
			mmLabel43.Text = "First Name:";
			mmLabel43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxMiddleName.BackColor = System.Drawing.Color.White;
			textBoxMiddleName.CustomReportFieldName = "";
			textBoxMiddleName.CustomReportKey = "";
			textBoxMiddleName.CustomReportValueType = 1;
			textBoxMiddleName.IsComboTextBox = false;
			textBoxMiddleName.Location = new System.Drawing.Point(132, 75);
			textBoxMiddleName.MaxLength = 30;
			textBoxMiddleName.Name = "textBoxMiddleName";
			textBoxMiddleName.Size = new System.Drawing.Size(385, 20);
			textBoxMiddleName.TabIndex = 6;
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
			textBoxAppraisalPoints.Location = new System.Drawing.Point(516, 57);
			textBoxAppraisalPoints.MaxLength = 5;
			textBoxAppraisalPoints.Name = "textBoxAppraisalPoints";
			textBoxAppraisalPoints.ReadOnly = true;
			textBoxAppraisalPoints.Size = new System.Drawing.Size(102, 20);
			textBoxAppraisalPoints.TabIndex = 69;
			mmLabel44.AutoSize = true;
			mmLabel44.BackColor = System.Drawing.Color.Transparent;
			mmLabel44.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel44.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel44.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel44.IsFieldHeader = false;
			mmLabel44.IsRequired = false;
			mmLabel44.Location = new System.Drawing.Point(461, 56);
			mmLabel44.Name = "mmLabel44";
			mmLabel44.PenWidth = 1f;
			mmLabel44.ShowBorder = false;
			mmLabel44.Size = new System.Drawing.Size(51, 26);
			mmLabel44.TabIndex = 68;
			mmLabel44.Text = "Appraisal\r\nPoints:";
			mmLabel45.AutoSize = true;
			mmLabel45.BackColor = System.Drawing.Color.Transparent;
			mmLabel45.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel45.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel45.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel45.IsFieldHeader = false;
			mmLabel45.IsRequired = false;
			mmLabel45.Location = new System.Drawing.Point(6, 26);
			mmLabel45.Name = "mmLabel45";
			mmLabel45.PenWidth = 1f;
			mmLabel45.ShowBorder = false;
			mmLabel45.Size = new System.Drawing.Size(43, 13);
			mmLabel45.TabIndex = 67;
			mmLabel45.Text = "IBAN#:";
			textBoxIBAN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxIBAN.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxIBAN.BackColor = System.Drawing.Color.White;
			textBoxIBAN.CustomReportFieldName = "";
			textBoxIBAN.CustomReportKey = "";
			textBoxIBAN.CustomReportValueType = 1;
			textBoxIBAN.IsComboTextBox = false;
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
			textBoxAccountName.Location = new System.Drawing.Point(246, 69);
			textBoxAccountName.MaxLength = 15;
			textBoxAccountName.Name = "textBoxAccountName";
			textBoxAccountName.Size = new System.Drawing.Size(347, 20);
			textBoxAccountName.TabIndex = 4;
			textBoxAccountName.TabStop = false;
			textBoxBankName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankName.CustomReportFieldName = "";
			textBoxBankName.CustomReportKey = "";
			textBoxBankName.CustomReportValueType = 1;
			textBoxBankName.Enabled = false;
			textBoxBankName.ForeColor = System.Drawing.Color.Black;
			textBoxBankName.IsComboTextBox = false;
			textBoxBankName.Location = new System.Drawing.Point(246, 46);
			textBoxBankName.MaxLength = 15;
			textBoxBankName.Name = "textBoxBankName";
			textBoxBankName.Size = new System.Drawing.Size(347, 20);
			textBoxBankName.TabIndex = 2;
			textBoxBankName.TabStop = false;
			textBoxLabourID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxLabourID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxLabourID.BackColor = System.Drawing.Color.White;
			textBoxLabourID.CustomReportFieldName = "";
			textBoxLabourID.CustomReportKey = "";
			textBoxLabourID.CustomReportValueType = 1;
			textBoxLabourID.IsComboTextBox = false;
			textBoxLabourID.Location = new System.Drawing.Point(90, 56);
			textBoxLabourID.MaxLength = 20;
			textBoxLabourID.Name = "textBoxLabourID";
			textBoxLabourID.Size = new System.Drawing.Size(158, 20);
			textBoxLabourID.TabIndex = 6;
			mmLabel46.AutoSize = true;
			mmLabel46.BackColor = System.Drawing.Color.Transparent;
			mmLabel46.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel46.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel46.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel46.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel46.IsFieldHeader = false;
			mmLabel46.IsRequired = false;
			mmLabel46.Location = new System.Drawing.Point(371, 132);
			mmLabel46.Name = "mmLabel46";
			mmLabel46.PenWidth = 1f;
			mmLabel46.ShowBorder = false;
			mmLabel46.Size = new System.Drawing.Size(56, 13);
			mmLabel46.TabIndex = 32;
			mmLabel46.Text = "Comment:";
			textBoxComment.BackColor = System.Drawing.Color.White;
			textBoxComment.CustomReportFieldName = "";
			textBoxComment.CustomReportKey = "";
			textBoxComment.CustomReportValueType = 1;
			textBoxComment.IsComboTextBox = false;
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
			mmLabel47.AutoSize = true;
			mmLabel47.BackColor = System.Drawing.Color.Transparent;
			mmLabel47.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel47.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel47.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel47.IsFieldHeader = false;
			mmLabel47.IsRequired = false;
			mmLabel47.Location = new System.Drawing.Point(9, 176);
			mmLabel47.Name = "mmLabel47";
			mmLabel47.PenWidth = 1f;
			mmLabel47.ShowBorder = false;
			mmLabel47.Size = new System.Drawing.Size(68, 13);
			mmLabel47.TabIndex = 14;
			mmLabel47.Text = "Postal Code:";
			textBoxPostalCode.BackColor = System.Drawing.Color.White;
			textBoxPostalCode.CustomReportFieldName = "";
			textBoxPostalCode.CustomReportKey = "";
			textBoxPostalCode.CustomReportValueType = 1;
			textBoxPostalCode.IsComboTextBox = false;
			textBoxPostalCode.Location = new System.Drawing.Point(132, 173);
			textBoxPostalCode.MaxLength = 30;
			textBoxPostalCode.Name = "textBoxPostalCode";
			textBoxPostalCode.Size = new System.Drawing.Size(229, 20);
			textBoxPostalCode.TabIndex = 7;
			mmLabel48.AutoSize = true;
			mmLabel48.BackColor = System.Drawing.Color.Transparent;
			mmLabel48.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel48.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel48.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel48.IsFieldHeader = false;
			mmLabel48.IsRequired = false;
			mmLabel48.Location = new System.Drawing.Point(371, 110);
			mmLabel48.Name = "mmLabel48";
			mmLabel48.PenWidth = 1f;
			mmLabel48.ShowBorder = false;
			mmLabel48.Size = new System.Drawing.Size(35, 13);
			mmLabel48.TabIndex = 28;
			mmLabel48.Text = "Email:";
			textBoxEmail.BackColor = System.Drawing.Color.White;
			textBoxEmail.CustomReportFieldName = "";
			textBoxEmail.CustomReportKey = "";
			textBoxEmail.CustomReportValueType = 1;
			textBoxEmail.IsComboTextBox = false;
			textBoxEmail.Location = new System.Drawing.Point(442, 107);
			textBoxEmail.MaxLength = 30;
			textBoxEmail.Name = "textBoxEmail";
			textBoxEmail.Size = new System.Drawing.Size(229, 20);
			textBoxEmail.TabIndex = 12;
			mmLabel52.AutoSize = true;
			mmLabel52.BackColor = System.Drawing.Color.Transparent;
			mmLabel52.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel52.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel52.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel52.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel52.IsFieldHeader = false;
			mmLabel52.IsRequired = false;
			mmLabel52.Location = new System.Drawing.Point(371, 88);
			mmLabel52.Name = "mmLabel52";
			mmLabel52.PenWidth = 1f;
			mmLabel52.ShowBorder = false;
			mmLabel52.Size = new System.Drawing.Size(41, 13);
			mmLabel52.TabIndex = 26;
			mmLabel52.Text = "Mobile:";
			textBoxMobile.BackColor = System.Drawing.Color.White;
			textBoxMobile.CustomReportFieldName = "";
			textBoxMobile.CustomReportKey = "";
			textBoxMobile.CustomReportValueType = 1;
			textBoxMobile.IsComboTextBox = false;
			textBoxMobile.Location = new System.Drawing.Point(442, 85);
			textBoxMobile.MaxLength = 30;
			textBoxMobile.Name = "textBoxMobile";
			textBoxMobile.Size = new System.Drawing.Size(229, 20);
			textBoxMobile.TabIndex = 11;
			mmLabel53.AutoSize = true;
			mmLabel53.BackColor = System.Drawing.Color.Transparent;
			mmLabel53.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel53.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel53.IsFieldHeader = false;
			mmLabel53.IsRequired = false;
			mmLabel53.Location = new System.Drawing.Point(371, 65);
			mmLabel53.Name = "mmLabel53";
			mmLabel53.PenWidth = 1f;
			mmLabel53.ShowBorder = false;
			mmLabel53.Size = new System.Drawing.Size(29, 13);
			mmLabel53.TabIndex = 24;
			mmLabel53.Text = "Fax:";
			textBoxFax.BackColor = System.Drawing.Color.White;
			textBoxFax.CustomReportFieldName = "";
			textBoxFax.CustomReportKey = "";
			textBoxFax.CustomReportValueType = 1;
			textBoxFax.IsComboTextBox = false;
			textBoxFax.Location = new System.Drawing.Point(442, 63);
			textBoxFax.MaxLength = 30;
			textBoxFax.Name = "textBoxFax";
			textBoxFax.Size = new System.Drawing.Size(229, 20);
			textBoxFax.TabIndex = 10;
			mmLabel54.AutoSize = true;
			mmLabel54.BackColor = System.Drawing.Color.Transparent;
			mmLabel54.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel54.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel54.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel54.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel54.IsFieldHeader = false;
			mmLabel54.IsRequired = false;
			mmLabel54.Location = new System.Drawing.Point(371, 44);
			mmLabel54.Name = "mmLabel54";
			mmLabel54.PenWidth = 1f;
			mmLabel54.ShowBorder = false;
			mmLabel54.Size = new System.Drawing.Size(50, 13);
			mmLabel54.TabIndex = 22;
			mmLabel54.Text = "Phone 2:";
			textBoxPhone2.BackColor = System.Drawing.Color.White;
			textBoxPhone2.CustomReportFieldName = "";
			textBoxPhone2.CustomReportKey = "";
			textBoxPhone2.CustomReportValueType = 1;
			textBoxPhone2.IsComboTextBox = false;
			textBoxPhone2.Location = new System.Drawing.Point(442, 41);
			textBoxPhone2.MaxLength = 30;
			textBoxPhone2.Name = "textBoxPhone2";
			textBoxPhone2.Size = new System.Drawing.Size(229, 20);
			textBoxPhone2.TabIndex = 9;
			mmLabel55.AutoSize = true;
			mmLabel55.BackColor = System.Drawing.Color.Transparent;
			mmLabel55.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel55.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel55.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel55.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel55.IsFieldHeader = false;
			mmLabel55.IsRequired = false;
			mmLabel55.Location = new System.Drawing.Point(371, 22);
			mmLabel55.Name = "mmLabel55";
			mmLabel55.PenWidth = 1f;
			mmLabel55.ShowBorder = false;
			mmLabel55.Size = new System.Drawing.Size(50, 13);
			mmLabel55.TabIndex = 20;
			mmLabel55.Text = "Phone 1:";
			textBoxPhone1.BackColor = System.Drawing.Color.White;
			textBoxPhone1.CustomReportFieldName = "";
			textBoxPhone1.CustomReportKey = "";
			textBoxPhone1.CustomReportValueType = 1;
			textBoxPhone1.IsComboTextBox = false;
			textBoxPhone1.Location = new System.Drawing.Point(442, 19);
			textBoxPhone1.MaxLength = 30;
			textBoxPhone1.Name = "textBoxPhone1";
			textBoxPhone1.Size = new System.Drawing.Size(229, 20);
			textBoxPhone1.TabIndex = 8;
			mmLabel56.AutoSize = true;
			mmLabel56.BackColor = System.Drawing.Color.Transparent;
			mmLabel56.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel56.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel56.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel56.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel56.IsFieldHeader = false;
			mmLabel56.IsRequired = false;
			mmLabel56.Location = new System.Drawing.Point(9, 154);
			mmLabel56.Name = "mmLabel56";
			mmLabel56.PenWidth = 1f;
			mmLabel56.ShowBorder = false;
			mmLabel56.Size = new System.Drawing.Size(50, 13);
			mmLabel56.TabIndex = 12;
			mmLabel56.Text = "Country:";
			textBoxCountry.BackColor = System.Drawing.Color.White;
			textBoxCountry.CustomReportFieldName = "";
			textBoxCountry.CustomReportKey = "";
			textBoxCountry.CustomReportValueType = 1;
			textBoxCountry.IsComboTextBox = false;
			textBoxCountry.Location = new System.Drawing.Point(132, 151);
			textBoxCountry.MaxLength = 30;
			textBoxCountry.Name = "textBoxCountry";
			textBoxCountry.Size = new System.Drawing.Size(229, 20);
			textBoxCountry.TabIndex = 6;
			mmLabel57.AutoSize = true;
			mmLabel57.BackColor = System.Drawing.Color.Transparent;
			mmLabel57.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel57.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel57.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel57.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel57.IsFieldHeader = false;
			mmLabel57.IsRequired = false;
			mmLabel57.Location = new System.Drawing.Point(9, 132);
			mmLabel57.Name = "mmLabel57";
			mmLabel57.PenWidth = 1f;
			mmLabel57.ShowBorder = false;
			mmLabel57.Size = new System.Drawing.Size(37, 13);
			mmLabel57.TabIndex = 10;
			mmLabel57.Text = "State:";
			textBoxState.BackColor = System.Drawing.Color.White;
			textBoxState.CustomReportFieldName = "";
			textBoxState.CustomReportKey = "";
			textBoxState.CustomReportValueType = 1;
			textBoxState.IsComboTextBox = false;
			textBoxState.Location = new System.Drawing.Point(132, 129);
			textBoxState.MaxLength = 30;
			textBoxState.Name = "textBoxState";
			textBoxState.Size = new System.Drawing.Size(229, 20);
			textBoxState.TabIndex = 5;
			mmLabel58.AutoSize = true;
			mmLabel58.BackColor = System.Drawing.Color.Transparent;
			mmLabel58.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel58.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel58.IsFieldHeader = false;
			mmLabel58.IsRequired = false;
			mmLabel58.Location = new System.Drawing.Point(9, 109);
			mmLabel58.Name = "mmLabel58";
			mmLabel58.PenWidth = 1f;
			mmLabel58.ShowBorder = false;
			mmLabel58.Size = new System.Drawing.Size(30, 13);
			mmLabel58.TabIndex = 8;
			mmLabel58.Text = "City:";
			textBoxCity.BackColor = System.Drawing.Color.White;
			textBoxCity.CustomReportFieldName = "";
			textBoxCity.CustomReportKey = "";
			textBoxCity.CustomReportValueType = 1;
			textBoxCity.IsComboTextBox = false;
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
			textBoxAddress2.Location = new System.Drawing.Point(132, 63);
			textBoxAddress2.MaxLength = 64;
			textBoxAddress2.Name = "textBoxAddress2";
			textBoxAddress2.Size = new System.Drawing.Size(229, 20);
			textBoxAddress2.TabIndex = 2;
			mmLabel59.AutoSize = true;
			mmLabel59.BackColor = System.Drawing.Color.Transparent;
			mmLabel59.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel59.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel59.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel59.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel59.IsFieldHeader = false;
			mmLabel59.IsRequired = false;
			mmLabel59.Location = new System.Drawing.Point(9, 43);
			mmLabel59.Name = "mmLabel59";
			mmLabel59.PenWidth = 1f;
			mmLabel59.ShowBorder = false;
			mmLabel59.Size = new System.Drawing.Size(50, 13);
			mmLabel59.TabIndex = 4;
			mmLabel59.Text = "Address:";
			textBoxAddress1.BackColor = System.Drawing.Color.White;
			textBoxAddress1.CustomReportFieldName = "";
			textBoxAddress1.CustomReportKey = "";
			textBoxAddress1.CustomReportValueType = 1;
			textBoxAddress1.IsComboTextBox = false;
			textBoxAddress1.Location = new System.Drawing.Point(132, 41);
			textBoxAddress1.MaxLength = 64;
			textBoxAddress1.Name = "textBoxAddress1";
			textBoxAddress1.Size = new System.Drawing.Size(229, 20);
			textBoxAddress1.TabIndex = 1;
			mmLabel60.AutoSize = true;
			mmLabel60.BackColor = System.Drawing.Color.Transparent;
			mmLabel60.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel60.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel60.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel60.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel60.IsFieldHeader = false;
			mmLabel60.IsRequired = false;
			mmLabel60.Location = new System.Drawing.Point(9, 22);
			mmLabel60.Name = "mmLabel60";
			mmLabel60.PenWidth = 1f;
			mmLabel60.ShowBorder = false;
			mmLabel60.Size = new System.Drawing.Size(64, 13);
			mmLabel60.TabIndex = 0;
			mmLabel60.Text = "Address ID:";
			textBoxAddressID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAddressID.CustomReportFieldName = "";
			textBoxAddressID.CustomReportKey = "";
			textBoxAddressID.CustomReportValueType = 1;
			textBoxAddressID.Enabled = false;
			textBoxAddressID.ForeColor = System.Drawing.Color.Black;
			textBoxAddressID.IsComboTextBox = false;
			textBoxAddressID.Location = new System.Drawing.Point(132, 19);
			textBoxAddressID.MaxLength = 15;
			textBoxAddressID.Name = "textBoxAddressID";
			textBoxAddressID.Size = new System.Drawing.Size(229, 20);
			textBoxAddressID.TabIndex = 0;
			textBoxAddressID.Text = "PRIMARY";
			dateTimePickerConfirmation.Checked = false;
			dateTimePickerConfirmation.CustomFormat = " ";
			dateTimePickerConfirmation.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerConfirmation.Location = new System.Drawing.Point(259, 13);
			dateTimePickerConfirmation.Name = "dateTimePickerConfirmation";
			dateTimePickerConfirmation.ShowCheckBox = true;
			dateTimePickerConfirmation.Size = new System.Drawing.Size(124, 20);
			dateTimePickerConfirmation.TabIndex = 1;
			dateTimePickerConfirmation.Value = new System.DateTime(0L);
			mmLabel61.AutoSize = true;
			mmLabel61.BackColor = System.Drawing.Color.Transparent;
			mmLabel61.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel61.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel61.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel61.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel61.IsFieldHeader = false;
			mmLabel61.IsRequired = false;
			mmLabel61.Location = new System.Drawing.Point(8, 59);
			mmLabel61.Name = "mmLabel61";
			mmLabel61.PenWidth = 1f;
			mmLabel61.ShowBorder = false;
			mmLabel61.Size = new System.Drawing.Size(58, 13);
			mmLabel61.TabIndex = 65;
			mmLabel61.Text = "Labour ID:";
			textBoxSpouse.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxSpouse.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxSpouse.BackColor = System.Drawing.Color.White;
			textBoxSpouse.CustomReportFieldName = "";
			textBoxSpouse.CustomReportKey = "";
			textBoxSpouse.CustomReportValueType = 1;
			textBoxSpouse.IsComboTextBox = false;
			textBoxSpouse.Location = new System.Drawing.Point(90, 35);
			textBoxSpouse.MaxLength = 30;
			textBoxSpouse.Name = "textBoxSpouse";
			textBoxSpouse.Size = new System.Drawing.Size(158, 20);
			textBoxSpouse.TabIndex = 3;
			mmLabel62.AutoSize = true;
			mmLabel62.BackColor = System.Drawing.Color.Transparent;
			mmLabel62.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel62.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel62.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel62.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel62.IsFieldHeader = false;
			mmLabel62.IsRequired = false;
			mmLabel62.Location = new System.Drawing.Point(9, 35);
			mmLabel62.Name = "mmLabel62";
			mmLabel62.PenWidth = 1f;
			mmLabel62.ShowBorder = false;
			mmLabel62.Size = new System.Drawing.Size(46, 13);
			mmLabel62.TabIndex = 49;
			mmLabel62.Text = "Spouse:";
			mmLabel63.AutoSize = true;
			mmLabel63.BackColor = System.Drawing.Color.Transparent;
			mmLabel63.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel63.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel63.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel63.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel63.IsFieldHeader = false;
			mmLabel63.IsRequired = false;
			mmLabel63.Location = new System.Drawing.Point(256, 37);
			mmLabel63.Name = "mmLabel63";
			mmLabel63.PenWidth = 1f;
			mmLabel63.ShowBorder = false;
			mmLabel63.Size = new System.Drawing.Size(69, 13);
			mmLabel63.TabIndex = 42;
			mmLabel63.Text = "Blood Group:";
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
			textBoxBloodGroup.Location = new System.Drawing.Point(338, 35);
			textBoxBloodGroup.MaxLength = 5;
			textBoxBloodGroup.Name = "textBoxBloodGroup";
			textBoxBloodGroup.Size = new System.Drawing.Size(117, 20);
			textBoxBloodGroup.TabIndex = 4;
			mmLabel64.AutoSize = true;
			mmLabel64.BackColor = System.Drawing.Color.Transparent;
			mmLabel64.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel64.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel64.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel64.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel64.IsFieldHeader = false;
			mmLabel64.IsRequired = false;
			mmLabel64.Location = new System.Drawing.Point(177, 15);
			mmLabel64.Name = "mmLabel64";
			mmLabel64.PenWidth = 1f;
			mmLabel64.ShowBorder = false;
			mmLabel64.Size = new System.Drawing.Size(72, 13);
			mmLabel64.TabIndex = 40;
			mmLabel64.Text = "Confirmation:";
			textBoxProbation.AllowDecimal = false;
			textBoxProbation.CustomReportFieldName = "";
			textBoxProbation.CustomReportKey = "";
			textBoxProbation.CustomReportValueType = 1;
			textBoxProbation.IsComboTextBox = false;
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
			mmLabel65.AutoSize = true;
			mmLabel65.BackColor = System.Drawing.Color.Transparent;
			mmLabel65.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel65.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel65.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel65.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel65.IsFieldHeader = false;
			mmLabel65.IsRequired = false;
			mmLabel65.Location = new System.Drawing.Point(8, 15);
			mmLabel65.Name = "mmLabel65";
			mmLabel65.PenWidth = 1f;
			mmLabel65.ShowBorder = false;
			mmLabel65.Size = new System.Drawing.Size(57, 13);
			mmLabel65.TabIndex = 37;
			mmLabel65.Text = "Probation:";
			mmLabel66.AutoSize = true;
			mmLabel66.BackColor = System.Drawing.Color.Transparent;
			mmLabel66.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel66.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel66.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel66.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel66.IsFieldHeader = false;
			mmLabel66.IsRequired = false;
			mmLabel66.Location = new System.Drawing.Point(461, 37);
			mmLabel66.Name = "mmLabel66";
			mmLabel66.PenWidth = 1f;
			mmLabel66.ShowBorder = false;
			mmLabel66.Size = new System.Drawing.Size(49, 13);
			mmLabel66.TabIndex = 30;
			mmLabel66.Text = "Day Off:";
			comboBoxcategory.Assigned = false;
			comboBoxcategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxcategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxcategory.CustomReportFieldName = "";
			comboBoxcategory.CustomReportKey = "";
			comboBoxcategory.CustomReportValueType = 1;
			comboBoxcategory.DescriptionTextBox = null;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxcategory.DisplayLayout.Appearance = appearance35;
			comboBoxcategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxcategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxcategory.DisplayLayout.GroupByBox.Appearance = appearance36;
			appearance37.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxcategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance37;
			comboBoxcategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance38.BackColor2 = System.Drawing.SystemColors.Control;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance38.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxcategory.DisplayLayout.GroupByBox.PromptAppearance = appearance38;
			comboBoxcategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxcategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxcategory.DisplayLayout.Override.ActiveCellAppearance = appearance39;
			appearance40.BackColor = System.Drawing.SystemColors.Highlight;
			appearance40.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxcategory.DisplayLayout.Override.ActiveRowAppearance = appearance40;
			comboBoxcategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxcategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			comboBoxcategory.DisplayLayout.Override.CardAreaAppearance = appearance41;
			appearance42.BorderColor = System.Drawing.Color.Silver;
			appearance42.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxcategory.DisplayLayout.Override.CellAppearance = appearance42;
			comboBoxcategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxcategory.DisplayLayout.Override.CellPadding = 0;
			appearance43.BackColor = System.Drawing.SystemColors.Control;
			appearance43.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance43.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxcategory.DisplayLayout.Override.GroupByRowAppearance = appearance43;
			appearance44.TextHAlignAsString = "Left";
			comboBoxcategory.DisplayLayout.Override.HeaderAppearance = appearance44;
			comboBoxcategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxcategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.Color.Silver;
			comboBoxcategory.DisplayLayout.Override.RowAppearance = appearance45;
			comboBoxcategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxcategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance46;
			comboBoxcategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxcategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxcategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxcategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxcategory.Editable = true;
			comboBoxcategory.FilterString = "";
			comboBoxcategory.GenericListType = Micromind.Common.Data.GenericListTypes.HorseCategory;
			comboBoxcategory.HasAllAccount = false;
			comboBoxcategory.HasCustom = false;
			comboBoxcategory.IsDataLoaded = false;
			comboBoxcategory.IsSingleColumn = false;
			comboBoxcategory.Location = new System.Drawing.Point(376, 205);
			comboBoxcategory.MaxDropDownItems = 12;
			comboBoxcategory.Name = "comboBoxcategory";
			comboBoxcategory.ShowInactiveItems = false;
			comboBoxcategory.ShowQuickAdd = true;
			comboBoxcategory.Size = new System.Drawing.Size(141, 20);
			comboBoxcategory.TabIndex = 13;
			comboBoxcategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxHorseGender.Assigned = false;
			comboBoxHorseGender.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxHorseGender.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxHorseGender.CustomReportFieldName = "";
			comboBoxHorseGender.CustomReportKey = "";
			comboBoxHorseGender.CustomReportValueType = 1;
			comboBoxHorseGender.DescriptionTextBox = null;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxHorseGender.DisplayLayout.Appearance = appearance47;
			comboBoxHorseGender.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxHorseGender.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance48.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxHorseGender.DisplayLayout.GroupByBox.Appearance = appearance48;
			appearance49.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxHorseGender.DisplayLayout.GroupByBox.BandLabelAppearance = appearance49;
			comboBoxHorseGender.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance50.BackColor2 = System.Drawing.SystemColors.Control;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance50.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxHorseGender.DisplayLayout.GroupByBox.PromptAppearance = appearance50;
			comboBoxHorseGender.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxHorseGender.DisplayLayout.MaxRowScrollRegions = 1;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxHorseGender.DisplayLayout.Override.ActiveCellAppearance = appearance51;
			appearance52.BackColor = System.Drawing.SystemColors.Highlight;
			appearance52.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxHorseGender.DisplayLayout.Override.ActiveRowAppearance = appearance52;
			comboBoxHorseGender.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxHorseGender.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			comboBoxHorseGender.DisplayLayout.Override.CardAreaAppearance = appearance53;
			appearance54.BorderColor = System.Drawing.Color.Silver;
			appearance54.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxHorseGender.DisplayLayout.Override.CellAppearance = appearance54;
			comboBoxHorseGender.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxHorseGender.DisplayLayout.Override.CellPadding = 0;
			appearance55.BackColor = System.Drawing.SystemColors.Control;
			appearance55.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance55.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance55.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxHorseGender.DisplayLayout.Override.GroupByRowAppearance = appearance55;
			appearance56.TextHAlignAsString = "Left";
			comboBoxHorseGender.DisplayLayout.Override.HeaderAppearance = appearance56;
			comboBoxHorseGender.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxHorseGender.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.BorderColor = System.Drawing.Color.Silver;
			comboBoxHorseGender.DisplayLayout.Override.RowAppearance = appearance57;
			comboBoxHorseGender.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxHorseGender.DisplayLayout.Override.TemplateAddRowAppearance = appearance58;
			comboBoxHorseGender.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxHorseGender.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxHorseGender.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxHorseGender.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxHorseGender.Editable = true;
			comboBoxHorseGender.FilterString = "";
			comboBoxHorseGender.HasAllAccount = false;
			comboBoxHorseGender.HasCustom = false;
			comboBoxHorseGender.IsDataLoaded = false;
			comboBoxHorseGender.Location = new System.Drawing.Point(414, 183);
			comboBoxHorseGender.MaxDropDownItems = 12;
			comboBoxHorseGender.Name = "comboBoxHorseGender";
			comboBoxHorseGender.ShowInactiveItems = false;
			comboBoxHorseGender.ShowQuickAdd = true;
			comboBoxHorseGender.Size = new System.Drawing.Size(102, 20);
			comboBoxHorseGender.TabIndex = 11;
			comboBoxHorseGender.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxHorseType.Assigned = false;
			comboBoxHorseType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxHorseType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxHorseType.CustomReportFieldName = "";
			comboBoxHorseType.CustomReportKey = "";
			comboBoxHorseType.CustomReportValueType = 1;
			comboBoxHorseType.DescriptionTextBox = null;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxHorseType.DisplayLayout.Appearance = appearance59;
			comboBoxHorseType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxHorseType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxHorseType.DisplayLayout.GroupByBox.Appearance = appearance60;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxHorseType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance61;
			comboBoxHorseType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance62.BackColor2 = System.Drawing.SystemColors.Control;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxHorseType.DisplayLayout.GroupByBox.PromptAppearance = appearance62;
			comboBoxHorseType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxHorseType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxHorseType.DisplayLayout.Override.ActiveCellAppearance = appearance63;
			appearance64.BackColor = System.Drawing.SystemColors.Highlight;
			appearance64.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxHorseType.DisplayLayout.Override.ActiveRowAppearance = appearance64;
			comboBoxHorseType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxHorseType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			comboBoxHorseType.DisplayLayout.Override.CardAreaAppearance = appearance65;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			appearance66.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxHorseType.DisplayLayout.Override.CellAppearance = appearance66;
			comboBoxHorseType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxHorseType.DisplayLayout.Override.CellPadding = 0;
			appearance67.BackColor = System.Drawing.SystemColors.Control;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxHorseType.DisplayLayout.Override.GroupByRowAppearance = appearance67;
			appearance68.TextHAlignAsString = "Left";
			comboBoxHorseType.DisplayLayout.Override.HeaderAppearance = appearance68;
			comboBoxHorseType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxHorseType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.Color.Silver;
			comboBoxHorseType.DisplayLayout.Override.RowAppearance = appearance69;
			comboBoxHorseType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxHorseType.DisplayLayout.Override.TemplateAddRowAppearance = appearance70;
			comboBoxHorseType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxHorseType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxHorseType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxHorseType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxHorseType.Editable = true;
			comboBoxHorseType.FilterString = "";
			comboBoxHorseType.HasAllAccount = false;
			comboBoxHorseType.HasCustom = false;
			comboBoxHorseType.IsDataLoaded = false;
			comboBoxHorseType.Location = new System.Drawing.Point(132, 31);
			comboBoxHorseType.MaxDropDownItems = 12;
			comboBoxHorseType.Name = "comboBoxHorseType";
			comboBoxHorseType.ShowInactiveItems = false;
			comboBoxHorseType.ShowQuickAdd = true;
			comboBoxHorseType.Size = new System.Drawing.Size(161, 20);
			comboBoxHorseType.TabIndex = 2;
			comboBoxHorseType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCareTaker.Assigned = false;
			comboBoxCareTaker.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCareTaker.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCareTaker.CustomReportFieldName = "";
			comboBoxCareTaker.CustomReportKey = "";
			comboBoxCareTaker.CustomReportValueType = 1;
			comboBoxCareTaker.DescriptionTextBox = null;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCareTaker.DisplayLayout.Appearance = appearance71;
			comboBoxCareTaker.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCareTaker.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance72.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance72.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCareTaker.DisplayLayout.GroupByBox.Appearance = appearance72;
			appearance73.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCareTaker.DisplayLayout.GroupByBox.BandLabelAppearance = appearance73;
			comboBoxCareTaker.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance74.BackColor2 = System.Drawing.SystemColors.Control;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance74.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCareTaker.DisplayLayout.GroupByBox.PromptAppearance = appearance74;
			comboBoxCareTaker.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCareTaker.DisplayLayout.MaxRowScrollRegions = 1;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCareTaker.DisplayLayout.Override.ActiveCellAppearance = appearance75;
			appearance76.BackColor = System.Drawing.SystemColors.Highlight;
			appearance76.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCareTaker.DisplayLayout.Override.ActiveRowAppearance = appearance76;
			comboBoxCareTaker.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCareTaker.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCareTaker.DisplayLayout.Override.CardAreaAppearance = appearance77;
			appearance78.BorderColor = System.Drawing.Color.Silver;
			appearance78.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCareTaker.DisplayLayout.Override.CellAppearance = appearance78;
			comboBoxCareTaker.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCareTaker.DisplayLayout.Override.CellPadding = 0;
			appearance79.BackColor = System.Drawing.SystemColors.Control;
			appearance79.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance79.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance79.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance79.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCareTaker.DisplayLayout.Override.GroupByRowAppearance = appearance79;
			appearance80.TextHAlignAsString = "Left";
			comboBoxCareTaker.DisplayLayout.Override.HeaderAppearance = appearance80;
			comboBoxCareTaker.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCareTaker.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.Color.Silver;
			comboBoxCareTaker.DisplayLayout.Override.RowAppearance = appearance81;
			comboBoxCareTaker.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCareTaker.DisplayLayout.Override.TemplateAddRowAppearance = appearance82;
			comboBoxCareTaker.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCareTaker.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCareTaker.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCareTaker.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCareTaker.Editable = true;
			comboBoxCareTaker.FilterString = "";
			comboBoxCareTaker.HasAllAccount = false;
			comboBoxCareTaker.HasCustom = false;
			comboBoxCareTaker.IsDataLoaded = false;
			comboBoxCareTaker.Location = new System.Drawing.Point(132, 295);
			comboBoxCareTaker.MaxDropDownItems = 12;
			comboBoxCareTaker.Name = "comboBoxCareTaker";
			comboBoxCareTaker.ShowInactiveItems = false;
			comboBoxCareTaker.ShowQuickAdd = true;
			comboBoxCareTaker.Size = new System.Drawing.Size(151, 20);
			comboBoxCareTaker.TabIndex = 20;
			comboBoxCareTaker.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxTrainer.Assigned = false;
			comboBoxTrainer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTrainer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTrainer.CustomReportFieldName = "";
			comboBoxTrainer.CustomReportKey = "";
			comboBoxTrainer.CustomReportValueType = 1;
			comboBoxTrainer.DescriptionTextBox = null;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTrainer.DisplayLayout.Appearance = appearance83;
			comboBoxTrainer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTrainer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance84.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance84.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTrainer.DisplayLayout.GroupByBox.Appearance = appearance84;
			appearance85.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTrainer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance85;
			comboBoxTrainer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance86.BackColor2 = System.Drawing.SystemColors.Control;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance86.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTrainer.DisplayLayout.GroupByBox.PromptAppearance = appearance86;
			comboBoxTrainer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTrainer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTrainer.DisplayLayout.Override.ActiveCellAppearance = appearance87;
			appearance88.BackColor = System.Drawing.SystemColors.Highlight;
			appearance88.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTrainer.DisplayLayout.Override.ActiveRowAppearance = appearance88;
			comboBoxTrainer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTrainer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTrainer.DisplayLayout.Override.CardAreaAppearance = appearance89;
			appearance90.BorderColor = System.Drawing.Color.Silver;
			appearance90.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTrainer.DisplayLayout.Override.CellAppearance = appearance90;
			comboBoxTrainer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTrainer.DisplayLayout.Override.CellPadding = 0;
			appearance91.BackColor = System.Drawing.SystemColors.Control;
			appearance91.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance91.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance91.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance91.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTrainer.DisplayLayout.Override.GroupByRowAppearance = appearance91;
			appearance92.TextHAlignAsString = "Left";
			comboBoxTrainer.DisplayLayout.Override.HeaderAppearance = appearance92;
			comboBoxTrainer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTrainer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.Color.Silver;
			comboBoxTrainer.DisplayLayout.Override.RowAppearance = appearance93;
			comboBoxTrainer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTrainer.DisplayLayout.Override.TemplateAddRowAppearance = appearance94;
			comboBoxTrainer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTrainer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTrainer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTrainer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTrainer.Editable = true;
			comboBoxTrainer.FilterString = "";
			comboBoxTrainer.HasAllAccount = false;
			comboBoxTrainer.HasCustom = false;
			comboBoxTrainer.IsDataLoaded = false;
			comboBoxTrainer.Location = new System.Drawing.Point(375, 273);
			comboBoxTrainer.MaxDropDownItems = 12;
			comboBoxTrainer.Name = "comboBoxTrainer";
			comboBoxTrainer.ShowInactiveItems = false;
			comboBoxTrainer.ShowQuickAdd = true;
			comboBoxTrainer.Size = new System.Drawing.Size(141, 20);
			comboBoxTrainer.TabIndex = 19;
			comboBoxTrainer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance95;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance96.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance96.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance96;
			appearance97.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance97;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance98.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance98.BackColor2 = System.Drawing.SystemColors.Control;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance98.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance98;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance99;
			appearance100.BackColor = System.Drawing.SystemColors.Highlight;
			appearance100.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance100;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance101;
			appearance102.BorderColor = System.Drawing.Color.Silver;
			appearance102.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance102;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance103.BackColor = System.Drawing.SystemColors.Control;
			appearance103.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance103.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance103.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance103.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance103;
			appearance104.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance104;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance105;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance106;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(132, 273);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowAll = false;
			comboBoxLocation.ShowConsignIn = false;
			comboBoxLocation.ShowConsignOut = false;
			comboBoxLocation.ShowDefaultLocationOnly = false;
			comboBoxLocation.ShowInactiveItems = false;
			comboBoxLocation.ShowNormalLocations = true;
			comboBoxLocation.ShowPOSOnly = false;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.ShowWarehouseOnly = false;
			comboBoxLocation.Size = new System.Drawing.Size(151, 20);
			comboBoxLocation.TabIndex = 18;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxOwnershipType.Assigned = false;
			comboBoxOwnershipType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxOwnershipType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxOwnershipType.CustomReportFieldName = "";
			comboBoxOwnershipType.CustomReportKey = "";
			comboBoxOwnershipType.CustomReportValueType = 1;
			comboBoxOwnershipType.DescriptionTextBox = null;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxOwnershipType.DisplayLayout.Appearance = appearance107;
			comboBoxOwnershipType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxOwnershipType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance108.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance108.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOwnershipType.DisplayLayout.GroupByBox.Appearance = appearance108;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOwnershipType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance109;
			comboBoxOwnershipType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance110.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance110.BackColor2 = System.Drawing.SystemColors.Control;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance110.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOwnershipType.DisplayLayout.GroupByBox.PromptAppearance = appearance110;
			comboBoxOwnershipType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxOwnershipType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxOwnershipType.DisplayLayout.Override.ActiveCellAppearance = appearance111;
			appearance112.BackColor = System.Drawing.SystemColors.Highlight;
			appearance112.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxOwnershipType.DisplayLayout.Override.ActiveRowAppearance = appearance112;
			comboBoxOwnershipType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxOwnershipType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			comboBoxOwnershipType.DisplayLayout.Override.CardAreaAppearance = appearance113;
			appearance114.BorderColor = System.Drawing.Color.Silver;
			appearance114.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxOwnershipType.DisplayLayout.Override.CellAppearance = appearance114;
			comboBoxOwnershipType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxOwnershipType.DisplayLayout.Override.CellPadding = 0;
			appearance115.BackColor = System.Drawing.SystemColors.Control;
			appearance115.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance115.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance115.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance115.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOwnershipType.DisplayLayout.Override.GroupByRowAppearance = appearance115;
			appearance116.TextHAlignAsString = "Left";
			comboBoxOwnershipType.DisplayLayout.Override.HeaderAppearance = appearance116;
			comboBoxOwnershipType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxOwnershipType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.Color.Silver;
			comboBoxOwnershipType.DisplayLayout.Override.RowAppearance = appearance117;
			comboBoxOwnershipType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxOwnershipType.DisplayLayout.Override.TemplateAddRowAppearance = appearance118;
			comboBoxOwnershipType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxOwnershipType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxOwnershipType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxOwnershipType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxOwnershipType.Editable = true;
			comboBoxOwnershipType.FilterString = "";
			comboBoxOwnershipType.GenericListType = Micromind.Common.Data.GenericListTypes.HorseOwnershipType;
			comboBoxOwnershipType.HasAllAccount = false;
			comboBoxOwnershipType.HasCustom = false;
			comboBoxOwnershipType.IsDataLoaded = false;
			comboBoxOwnershipType.IsSingleColumn = false;
			comboBoxOwnershipType.Location = new System.Drawing.Point(461, 20);
			comboBoxOwnershipType.MaxDropDownItems = 12;
			comboBoxOwnershipType.Name = "comboBoxOwnershipType";
			comboBoxOwnershipType.ShowInactiveItems = false;
			comboBoxOwnershipType.ShowQuickAdd = true;
			comboBoxOwnershipType.Size = new System.Drawing.Size(151, 20);
			comboBoxOwnershipType.TabIndex = 3;
			comboBoxOwnershipType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCountry.Assigned = false;
			comboBoxCountry.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCountry.CustomReportFieldName = "";
			comboBoxCountry.CustomReportKey = "";
			comboBoxCountry.CustomReportValueType = 1;
			comboBoxCountry.DescriptionTextBox = null;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCountry.DisplayLayout.Appearance = appearance119;
			comboBoxCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance120.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance120.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance120.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.GroupByBox.Appearance = appearance120;
			appearance121.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance121;
			comboBoxCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance122.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance122.BackColor2 = System.Drawing.SystemColors.Control;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance122.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance122;
			comboBoxCountry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCountry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			appearance123.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCountry.DisplayLayout.Override.ActiveCellAppearance = appearance123;
			appearance124.BackColor = System.Drawing.SystemColors.Highlight;
			appearance124.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCountry.DisplayLayout.Override.ActiveRowAppearance = appearance124;
			comboBoxCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.CardAreaAppearance = appearance125;
			appearance126.BorderColor = System.Drawing.Color.Silver;
			appearance126.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCountry.DisplayLayout.Override.CellAppearance = appearance126;
			comboBoxCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCountry.DisplayLayout.Override.CellPadding = 0;
			appearance127.BackColor = System.Drawing.SystemColors.Control;
			appearance127.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance127.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance127.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance127.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCountry.DisplayLayout.Override.GroupByRowAppearance = appearance127;
			appearance128.TextHAlignAsString = "Left";
			comboBoxCountry.DisplayLayout.Override.HeaderAppearance = appearance128;
			comboBoxCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.BorderColor = System.Drawing.Color.Silver;
			comboBoxCountry.DisplayLayout.Override.RowAppearance = appearance129;
			comboBoxCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance130.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance130;
			comboBoxCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCountry.Editable = true;
			comboBoxCountry.FilterString = "";
			comboBoxCountry.HasAllAccount = false;
			comboBoxCountry.HasCustom = false;
			comboBoxCountry.IsDataLoaded = false;
			comboBoxCountry.Location = new System.Drawing.Point(132, 250);
			comboBoxCountry.MaxDropDownItems = 12;
			comboBoxCountry.Name = "comboBoxCountry";
			comboBoxCountry.ShowInactiveItems = false;
			comboBoxCountry.ShowQuickAdd = true;
			comboBoxCountry.Size = new System.Drawing.Size(151, 20);
			comboBoxCountry.TabIndex = 16;
			comboBoxCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSexChangedFrom.Assigned = false;
			comboBoxSexChangedFrom.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSexChangedFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSexChangedFrom.CustomReportFieldName = "";
			comboBoxSexChangedFrom.CustomReportKey = "";
			comboBoxSexChangedFrom.CustomReportValueType = 1;
			comboBoxSexChangedFrom.DescriptionTextBox = null;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSexChangedFrom.DisplayLayout.Appearance = appearance131;
			comboBoxSexChangedFrom.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSexChangedFrom.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance132.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance132.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance132.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSexChangedFrom.DisplayLayout.GroupByBox.Appearance = appearance132;
			appearance133.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSexChangedFrom.DisplayLayout.GroupByBox.BandLabelAppearance = appearance133;
			comboBoxSexChangedFrom.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance134.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance134.BackColor2 = System.Drawing.SystemColors.Control;
			appearance134.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance134.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSexChangedFrom.DisplayLayout.GroupByBox.PromptAppearance = appearance134;
			comboBoxSexChangedFrom.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSexChangedFrom.DisplayLayout.MaxRowScrollRegions = 1;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			appearance135.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSexChangedFrom.DisplayLayout.Override.ActiveCellAppearance = appearance135;
			appearance136.BackColor = System.Drawing.SystemColors.Highlight;
			appearance136.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSexChangedFrom.DisplayLayout.Override.ActiveRowAppearance = appearance136;
			comboBoxSexChangedFrom.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSexChangedFrom.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance137.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSexChangedFrom.DisplayLayout.Override.CardAreaAppearance = appearance137;
			appearance138.BorderColor = System.Drawing.Color.Silver;
			appearance138.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSexChangedFrom.DisplayLayout.Override.CellAppearance = appearance138;
			comboBoxSexChangedFrom.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSexChangedFrom.DisplayLayout.Override.CellPadding = 0;
			appearance139.BackColor = System.Drawing.SystemColors.Control;
			appearance139.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance139.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance139.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance139.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSexChangedFrom.DisplayLayout.Override.GroupByRowAppearance = appearance139;
			appearance140.TextHAlignAsString = "Left";
			comboBoxSexChangedFrom.DisplayLayout.Override.HeaderAppearance = appearance140;
			comboBoxSexChangedFrom.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSexChangedFrom.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance141.BackColor = System.Drawing.SystemColors.Window;
			appearance141.BorderColor = System.Drawing.Color.Silver;
			comboBoxSexChangedFrom.DisplayLayout.Override.RowAppearance = appearance141;
			comboBoxSexChangedFrom.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance142.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSexChangedFrom.DisplayLayout.Override.TemplateAddRowAppearance = appearance142;
			comboBoxSexChangedFrom.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSexChangedFrom.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSexChangedFrom.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSexChangedFrom.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSexChangedFrom.Editable = true;
			comboBoxSexChangedFrom.FilterString = "";
			comboBoxSexChangedFrom.HasAllAccount = false;
			comboBoxSexChangedFrom.HasCustom = false;
			comboBoxSexChangedFrom.IsDataLoaded = false;
			comboBoxSexChangedFrom.Location = new System.Drawing.Point(131, 166);
			comboBoxSexChangedFrom.MaxDropDownItems = 12;
			comboBoxSexChangedFrom.Name = "comboBoxSexChangedFrom";
			comboBoxSexChangedFrom.ShowInactiveItems = false;
			comboBoxSexChangedFrom.ShowQuickAdd = true;
			comboBoxSexChangedFrom.Size = new System.Drawing.Size(151, 20);
			comboBoxSexChangedFrom.TabIndex = 11;
			comboBoxSexChangedFrom.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSoldAt.Assigned = false;
			comboBoxSoldAt.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSoldAt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSoldAt.CustomReportFieldName = "";
			comboBoxSoldAt.CustomReportKey = "";
			comboBoxSoldAt.CustomReportValueType = 1;
			comboBoxSoldAt.DescriptionTextBox = null;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSoldAt.DisplayLayout.Appearance = appearance143;
			comboBoxSoldAt.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSoldAt.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance144.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance144.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance144.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSoldAt.DisplayLayout.GroupByBox.Appearance = appearance144;
			appearance145.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSoldAt.DisplayLayout.GroupByBox.BandLabelAppearance = appearance145;
			comboBoxSoldAt.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance146.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance146.BackColor2 = System.Drawing.SystemColors.Control;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance146.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSoldAt.DisplayLayout.GroupByBox.PromptAppearance = appearance146;
			comboBoxSoldAt.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSoldAt.DisplayLayout.MaxRowScrollRegions = 1;
			appearance147.BackColor = System.Drawing.SystemColors.Window;
			appearance147.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSoldAt.DisplayLayout.Override.ActiveCellAppearance = appearance147;
			appearance148.BackColor = System.Drawing.SystemColors.Highlight;
			appearance148.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSoldAt.DisplayLayout.Override.ActiveRowAppearance = appearance148;
			comboBoxSoldAt.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSoldAt.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSoldAt.DisplayLayout.Override.CardAreaAppearance = appearance149;
			appearance150.BorderColor = System.Drawing.Color.Silver;
			appearance150.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSoldAt.DisplayLayout.Override.CellAppearance = appearance150;
			comboBoxSoldAt.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSoldAt.DisplayLayout.Override.CellPadding = 0;
			appearance151.BackColor = System.Drawing.SystemColors.Control;
			appearance151.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance151.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance151.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance151.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSoldAt.DisplayLayout.Override.GroupByRowAppearance = appearance151;
			appearance152.TextHAlignAsString = "Left";
			comboBoxSoldAt.DisplayLayout.Override.HeaderAppearance = appearance152;
			comboBoxSoldAt.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSoldAt.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance153.BackColor = System.Drawing.SystemColors.Window;
			appearance153.BorderColor = System.Drawing.Color.Silver;
			comboBoxSoldAt.DisplayLayout.Override.RowAppearance = appearance153;
			comboBoxSoldAt.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance154.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSoldAt.DisplayLayout.Override.TemplateAddRowAppearance = appearance154;
			comboBoxSoldAt.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSoldAt.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSoldAt.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSoldAt.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSoldAt.Editable = true;
			comboBoxSoldAt.FilterString = "";
			comboBoxSoldAt.HasAllAccount = false;
			comboBoxSoldAt.HasCustom = false;
			comboBoxSoldAt.IsDataLoaded = false;
			comboBoxSoldAt.Location = new System.Drawing.Point(131, 143);
			comboBoxSoldAt.MaxDropDownItems = 12;
			comboBoxSoldAt.Name = "comboBoxSoldAt";
			comboBoxSoldAt.ShowInactiveItems = false;
			comboBoxSoldAt.ShowQuickAdd = true;
			comboBoxSoldAt.Size = new System.Drawing.Size(151, 20);
			comboBoxSoldAt.TabIndex = 9;
			comboBoxSoldAt.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxTransferredTo.Assigned = false;
			comboBoxTransferredTo.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTransferredTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTransferredTo.CustomReportFieldName = "";
			comboBoxTransferredTo.CustomReportKey = "";
			comboBoxTransferredTo.CustomReportValueType = 1;
			comboBoxTransferredTo.DescriptionTextBox = null;
			appearance155.BackColor = System.Drawing.SystemColors.Window;
			appearance155.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTransferredTo.DisplayLayout.Appearance = appearance155;
			comboBoxTransferredTo.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTransferredTo.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance156.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance156.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance156.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance156.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTransferredTo.DisplayLayout.GroupByBox.Appearance = appearance156;
			appearance157.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTransferredTo.DisplayLayout.GroupByBox.BandLabelAppearance = appearance157;
			comboBoxTransferredTo.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance158.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance158.BackColor2 = System.Drawing.SystemColors.Control;
			appearance158.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance158.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTransferredTo.DisplayLayout.GroupByBox.PromptAppearance = appearance158;
			comboBoxTransferredTo.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTransferredTo.DisplayLayout.MaxRowScrollRegions = 1;
			appearance159.BackColor = System.Drawing.SystemColors.Window;
			appearance159.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTransferredTo.DisplayLayout.Override.ActiveCellAppearance = appearance159;
			appearance160.BackColor = System.Drawing.SystemColors.Highlight;
			appearance160.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTransferredTo.DisplayLayout.Override.ActiveRowAppearance = appearance160;
			comboBoxTransferredTo.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTransferredTo.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTransferredTo.DisplayLayout.Override.CardAreaAppearance = appearance161;
			appearance162.BorderColor = System.Drawing.Color.Silver;
			appearance162.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTransferredTo.DisplayLayout.Override.CellAppearance = appearance162;
			comboBoxTransferredTo.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTransferredTo.DisplayLayout.Override.CellPadding = 0;
			appearance163.BackColor = System.Drawing.SystemColors.Control;
			appearance163.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance163.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance163.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance163.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTransferredTo.DisplayLayout.Override.GroupByRowAppearance = appearance163;
			appearance164.TextHAlignAsString = "Left";
			comboBoxTransferredTo.DisplayLayout.Override.HeaderAppearance = appearance164;
			comboBoxTransferredTo.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTransferredTo.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance165.BackColor = System.Drawing.SystemColors.Window;
			appearance165.BorderColor = System.Drawing.Color.Silver;
			comboBoxTransferredTo.DisplayLayout.Override.RowAppearance = appearance165;
			comboBoxTransferredTo.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance166.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTransferredTo.DisplayLayout.Override.TemplateAddRowAppearance = appearance166;
			comboBoxTransferredTo.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTransferredTo.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTransferredTo.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTransferredTo.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTransferredTo.Editable = true;
			comboBoxTransferredTo.FilterString = "";
			comboBoxTransferredTo.HasAllAccount = false;
			comboBoxTransferredTo.HasCustom = false;
			comboBoxTransferredTo.IsDataLoaded = false;
			comboBoxTransferredTo.Location = new System.Drawing.Point(131, 120);
			comboBoxTransferredTo.MaxDropDownItems = 12;
			comboBoxTransferredTo.Name = "comboBoxTransferredTo";
			comboBoxTransferredTo.ShowInactiveItems = false;
			comboBoxTransferredTo.ShowQuickAdd = true;
			comboBoxTransferredTo.Size = new System.Drawing.Size(151, 20);
			comboBoxTransferredTo.TabIndex = 7;
			comboBoxTransferredTo.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboboxReceivedFrom.Assigned = false;
			comboboxReceivedFrom.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboboxReceivedFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboboxReceivedFrom.CustomReportFieldName = "";
			comboboxReceivedFrom.CustomReportKey = "";
			comboboxReceivedFrom.CustomReportValueType = 1;
			comboboxReceivedFrom.DescriptionTextBox = null;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			appearance167.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboboxReceivedFrom.DisplayLayout.Appearance = appearance167;
			comboboxReceivedFrom.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboboxReceivedFrom.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance168.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance168.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance168.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance168.BorderColor = System.Drawing.SystemColors.Window;
			comboboxReceivedFrom.DisplayLayout.GroupByBox.Appearance = appearance168;
			appearance169.ForeColor = System.Drawing.SystemColors.GrayText;
			comboboxReceivedFrom.DisplayLayout.GroupByBox.BandLabelAppearance = appearance169;
			comboboxReceivedFrom.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance170.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance170.BackColor2 = System.Drawing.SystemColors.Control;
			appearance170.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance170.ForeColor = System.Drawing.SystemColors.GrayText;
			comboboxReceivedFrom.DisplayLayout.GroupByBox.PromptAppearance = appearance170;
			comboboxReceivedFrom.DisplayLayout.MaxColScrollRegions = 1;
			comboboxReceivedFrom.DisplayLayout.MaxRowScrollRegions = 1;
			appearance171.BackColor = System.Drawing.SystemColors.Window;
			appearance171.ForeColor = System.Drawing.SystemColors.ControlText;
			comboboxReceivedFrom.DisplayLayout.Override.ActiveCellAppearance = appearance171;
			appearance172.BackColor = System.Drawing.SystemColors.Highlight;
			appearance172.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboboxReceivedFrom.DisplayLayout.Override.ActiveRowAppearance = appearance172;
			comboboxReceivedFrom.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboboxReceivedFrom.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance173.BackColor = System.Drawing.SystemColors.Window;
			comboboxReceivedFrom.DisplayLayout.Override.CardAreaAppearance = appearance173;
			appearance174.BorderColor = System.Drawing.Color.Silver;
			appearance174.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboboxReceivedFrom.DisplayLayout.Override.CellAppearance = appearance174;
			comboboxReceivedFrom.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboboxReceivedFrom.DisplayLayout.Override.CellPadding = 0;
			appearance175.BackColor = System.Drawing.SystemColors.Control;
			appearance175.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance175.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance175.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance175.BorderColor = System.Drawing.SystemColors.Window;
			comboboxReceivedFrom.DisplayLayout.Override.GroupByRowAppearance = appearance175;
			appearance176.TextHAlignAsString = "Left";
			comboboxReceivedFrom.DisplayLayout.Override.HeaderAppearance = appearance176;
			comboboxReceivedFrom.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboboxReceivedFrom.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance177.BackColor = System.Drawing.SystemColors.Window;
			appearance177.BorderColor = System.Drawing.Color.Silver;
			comboboxReceivedFrom.DisplayLayout.Override.RowAppearance = appearance177;
			comboboxReceivedFrom.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance178.BackColor = System.Drawing.SystemColors.ControlLight;
			comboboxReceivedFrom.DisplayLayout.Override.TemplateAddRowAppearance = appearance178;
			comboboxReceivedFrom.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboboxReceivedFrom.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboboxReceivedFrom.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboboxReceivedFrom.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboboxReceivedFrom.Editable = true;
			comboboxReceivedFrom.FilterString = "";
			comboboxReceivedFrom.HasAllAccount = false;
			comboboxReceivedFrom.HasCustom = false;
			comboboxReceivedFrom.IsDataLoaded = false;
			comboboxReceivedFrom.Location = new System.Drawing.Point(131, 97);
			comboboxReceivedFrom.MaxDropDownItems = 12;
			comboboxReceivedFrom.Name = "comboboxReceivedFrom";
			comboboxReceivedFrom.ShowInactiveItems = false;
			comboboxReceivedFrom.ShowQuickAdd = true;
			comboboxReceivedFrom.Size = new System.Drawing.Size(151, 20);
			comboboxReceivedFrom.TabIndex = 5;
			comboboxReceivedFrom.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxExportedTo.Assigned = false;
			comboBoxExportedTo.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxExportedTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxExportedTo.CustomReportFieldName = "";
			comboBoxExportedTo.CustomReportKey = "";
			comboBoxExportedTo.CustomReportValueType = 1;
			comboBoxExportedTo.DescriptionTextBox = null;
			appearance179.BackColor = System.Drawing.SystemColors.Window;
			appearance179.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxExportedTo.DisplayLayout.Appearance = appearance179;
			comboBoxExportedTo.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxExportedTo.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance180.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance180.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance180.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance180.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExportedTo.DisplayLayout.GroupByBox.Appearance = appearance180;
			appearance181.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExportedTo.DisplayLayout.GroupByBox.BandLabelAppearance = appearance181;
			comboBoxExportedTo.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance182.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance182.BackColor2 = System.Drawing.SystemColors.Control;
			appearance182.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance182.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExportedTo.DisplayLayout.GroupByBox.PromptAppearance = appearance182;
			comboBoxExportedTo.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxExportedTo.DisplayLayout.MaxRowScrollRegions = 1;
			appearance183.BackColor = System.Drawing.SystemColors.Window;
			appearance183.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxExportedTo.DisplayLayout.Override.ActiveCellAppearance = appearance183;
			appearance184.BackColor = System.Drawing.SystemColors.Highlight;
			appearance184.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxExportedTo.DisplayLayout.Override.ActiveRowAppearance = appearance184;
			comboBoxExportedTo.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxExportedTo.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance185.BackColor = System.Drawing.SystemColors.Window;
			comboBoxExportedTo.DisplayLayout.Override.CardAreaAppearance = appearance185;
			appearance186.BorderColor = System.Drawing.Color.Silver;
			appearance186.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxExportedTo.DisplayLayout.Override.CellAppearance = appearance186;
			comboBoxExportedTo.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxExportedTo.DisplayLayout.Override.CellPadding = 0;
			appearance187.BackColor = System.Drawing.SystemColors.Control;
			appearance187.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance187.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance187.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance187.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExportedTo.DisplayLayout.Override.GroupByRowAppearance = appearance187;
			appearance188.TextHAlignAsString = "Left";
			comboBoxExportedTo.DisplayLayout.Override.HeaderAppearance = appearance188;
			comboBoxExportedTo.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxExportedTo.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance189.BackColor = System.Drawing.SystemColors.Window;
			appearance189.BorderColor = System.Drawing.Color.Silver;
			comboBoxExportedTo.DisplayLayout.Override.RowAppearance = appearance189;
			comboBoxExportedTo.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance190.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxExportedTo.DisplayLayout.Override.TemplateAddRowAppearance = appearance190;
			comboBoxExportedTo.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxExportedTo.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxExportedTo.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxExportedTo.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxExportedTo.Editable = true;
			comboBoxExportedTo.FilterString = "";
			comboBoxExportedTo.HasAllAccount = false;
			comboBoxExportedTo.HasCustom = false;
			comboBoxExportedTo.IsDataLoaded = false;
			comboBoxExportedTo.Location = new System.Drawing.Point(131, 74);
			comboBoxExportedTo.MaxDropDownItems = 12;
			comboBoxExportedTo.Name = "comboBoxExportedTo";
			comboBoxExportedTo.ShowInactiveItems = false;
			comboBoxExportedTo.ShowQuickAdd = true;
			comboBoxExportedTo.Size = new System.Drawing.Size(151, 20);
			comboBoxExportedTo.TabIndex = 4;
			comboBoxExportedTo.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxImportedFrom.Assigned = false;
			comboBoxImportedFrom.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxImportedFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxImportedFrom.CustomReportFieldName = "";
			comboBoxImportedFrom.CustomReportKey = "";
			comboBoxImportedFrom.CustomReportValueType = 1;
			comboBoxImportedFrom.DescriptionTextBox = null;
			appearance191.BackColor = System.Drawing.SystemColors.Window;
			appearance191.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxImportedFrom.DisplayLayout.Appearance = appearance191;
			comboBoxImportedFrom.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxImportedFrom.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance192.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance192.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance192.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance192.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxImportedFrom.DisplayLayout.GroupByBox.Appearance = appearance192;
			appearance193.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxImportedFrom.DisplayLayout.GroupByBox.BandLabelAppearance = appearance193;
			comboBoxImportedFrom.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance194.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance194.BackColor2 = System.Drawing.SystemColors.Control;
			appearance194.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance194.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxImportedFrom.DisplayLayout.GroupByBox.PromptAppearance = appearance194;
			comboBoxImportedFrom.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxImportedFrom.DisplayLayout.MaxRowScrollRegions = 1;
			appearance195.BackColor = System.Drawing.SystemColors.Window;
			appearance195.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxImportedFrom.DisplayLayout.Override.ActiveCellAppearance = appearance195;
			appearance196.BackColor = System.Drawing.SystemColors.Highlight;
			appearance196.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxImportedFrom.DisplayLayout.Override.ActiveRowAppearance = appearance196;
			comboBoxImportedFrom.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxImportedFrom.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance197.BackColor = System.Drawing.SystemColors.Window;
			comboBoxImportedFrom.DisplayLayout.Override.CardAreaAppearance = appearance197;
			appearance198.BorderColor = System.Drawing.Color.Silver;
			appearance198.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxImportedFrom.DisplayLayout.Override.CellAppearance = appearance198;
			comboBoxImportedFrom.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxImportedFrom.DisplayLayout.Override.CellPadding = 0;
			appearance199.BackColor = System.Drawing.SystemColors.Control;
			appearance199.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance199.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance199.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance199.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxImportedFrom.DisplayLayout.Override.GroupByRowAppearance = appearance199;
			appearance200.TextHAlignAsString = "Left";
			comboBoxImportedFrom.DisplayLayout.Override.HeaderAppearance = appearance200;
			comboBoxImportedFrom.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxImportedFrom.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance201.BackColor = System.Drawing.SystemColors.Window;
			appearance201.BorderColor = System.Drawing.Color.Silver;
			comboBoxImportedFrom.DisplayLayout.Override.RowAppearance = appearance201;
			comboBoxImportedFrom.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance202.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxImportedFrom.DisplayLayout.Override.TemplateAddRowAppearance = appearance202;
			comboBoxImportedFrom.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxImportedFrom.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxImportedFrom.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxImportedFrom.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxImportedFrom.Editable = true;
			comboBoxImportedFrom.FilterString = "";
			comboBoxImportedFrom.HasAllAccount = false;
			comboBoxImportedFrom.HasCustom = false;
			comboBoxImportedFrom.IsDataLoaded = false;
			comboBoxImportedFrom.Location = new System.Drawing.Point(131, 51);
			comboBoxImportedFrom.MaxDropDownItems = 12;
			comboBoxImportedFrom.Name = "comboBoxImportedFrom";
			comboBoxImportedFrom.ShowInactiveItems = false;
			comboBoxImportedFrom.ShowQuickAdd = true;
			comboBoxImportedFrom.Size = new System.Drawing.Size(151, 20);
			comboBoxImportedFrom.TabIndex = 2;
			comboBoxImportedFrom.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(7, 4);
			udfEntryGrid.Margin = new System.Windows.Forms.Padding(4);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(702, 482);
			udfEntryGrid.TabIndex = 2;
			udfEntryGrid.Load += new System.EventHandler(udfEntryGrid_Load);
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
			workLocationComboBox1.Assigned = false;
			workLocationComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			workLocationComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			workLocationComboBox1.CustomReportFieldName = "";
			workLocationComboBox1.CustomReportKey = "";
			workLocationComboBox1.CustomReportValueType = 1;
			workLocationComboBox1.DescriptionTextBox = null;
			appearance203.BackColor = System.Drawing.SystemColors.Window;
			appearance203.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			workLocationComboBox1.DisplayLayout.Appearance = appearance203;
			workLocationComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			workLocationComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance204.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance204.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance204.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance204.BorderColor = System.Drawing.SystemColors.Window;
			workLocationComboBox1.DisplayLayout.GroupByBox.Appearance = appearance204;
			appearance205.ForeColor = System.Drawing.SystemColors.GrayText;
			workLocationComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance205;
			workLocationComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance206.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance206.BackColor2 = System.Drawing.SystemColors.Control;
			appearance206.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance206.ForeColor = System.Drawing.SystemColors.GrayText;
			workLocationComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance206;
			workLocationComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			workLocationComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance207.BackColor = System.Drawing.SystemColors.Window;
			appearance207.ForeColor = System.Drawing.SystemColors.ControlText;
			workLocationComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance207;
			appearance208.BackColor = System.Drawing.SystemColors.Highlight;
			appearance208.ForeColor = System.Drawing.SystemColors.HighlightText;
			workLocationComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance208;
			workLocationComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			workLocationComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance209.BackColor = System.Drawing.SystemColors.Window;
			workLocationComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance209;
			appearance210.BorderColor = System.Drawing.Color.Silver;
			appearance210.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			workLocationComboBox1.DisplayLayout.Override.CellAppearance = appearance210;
			workLocationComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			workLocationComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance211.BackColor = System.Drawing.SystemColors.Control;
			appearance211.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance211.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance211.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance211.BorderColor = System.Drawing.SystemColors.Window;
			workLocationComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance211;
			appearance212.TextHAlignAsString = "Left";
			workLocationComboBox1.DisplayLayout.Override.HeaderAppearance = appearance212;
			workLocationComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			workLocationComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance213.BackColor = System.Drawing.SystemColors.Window;
			appearance213.BorderColor = System.Drawing.Color.Silver;
			workLocationComboBox1.DisplayLayout.Override.RowAppearance = appearance213;
			workLocationComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance214.BackColor = System.Drawing.SystemColors.ControlLight;
			workLocationComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance214;
			workLocationComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			workLocationComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			workLocationComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			workLocationComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			workLocationComboBox1.Editable = true;
			workLocationComboBox1.FilterString = "";
			workLocationComboBox1.HasAllAccount = false;
			workLocationComboBox1.HasCustom = false;
			workLocationComboBox1.IsDataLoaded = false;
			workLocationComboBox1.Location = new System.Drawing.Point(132, 141);
			workLocationComboBox1.MaxDropDownItems = 12;
			workLocationComboBox1.Name = "workLocationComboBox1";
			workLocationComboBox1.ShowAll = false;
			workLocationComboBox1.ShowConsignIn = false;
			workLocationComboBox1.ShowConsignOut = false;
			workLocationComboBox1.ShowInactiveItems = false;
			workLocationComboBox1.ShowNormalLocations = true;
			workLocationComboBox1.ShowPOSOnly = false;
			workLocationComboBox1.ShowQuickAdd = true;
			workLocationComboBox1.ShowWarehouseOnly = false;
			workLocationComboBox1.Size = new System.Drawing.Size(151, 20);
			workLocationComboBox1.TabIndex = 9;
			workLocationComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxType.Assigned = false;
			comboBoxType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxType.CustomReportFieldName = "";
			comboBoxType.CustomReportKey = "";
			comboBoxType.CustomReportValueType = 1;
			comboBoxType.DescriptionTextBox = null;
			appearance215.BackColor = System.Drawing.SystemColors.Window;
			appearance215.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxType.DisplayLayout.Appearance = appearance215;
			comboBoxType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance216.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance216.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance216.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance216.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.GroupByBox.Appearance = appearance216;
			appearance217.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance217;
			comboBoxType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance218.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance218.BackColor2 = System.Drawing.SystemColors.Control;
			appearance218.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance218.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxType.DisplayLayout.GroupByBox.PromptAppearance = appearance218;
			comboBoxType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance219.BackColor = System.Drawing.SystemColors.Window;
			appearance219.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxType.DisplayLayout.Override.ActiveCellAppearance = appearance219;
			appearance220.BackColor = System.Drawing.SystemColors.Highlight;
			appearance220.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxType.DisplayLayout.Override.ActiveRowAppearance = appearance220;
			comboBoxType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance221.BackColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.Override.CardAreaAppearance = appearance221;
			appearance222.BorderColor = System.Drawing.Color.Silver;
			appearance222.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxType.DisplayLayout.Override.CellAppearance = appearance222;
			comboBoxType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxType.DisplayLayout.Override.CellPadding = 0;
			appearance223.BackColor = System.Drawing.SystemColors.Control;
			appearance223.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance223.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance223.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance223.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxType.DisplayLayout.Override.GroupByRowAppearance = appearance223;
			appearance224.TextHAlignAsString = "Left";
			comboBoxType.DisplayLayout.Override.HeaderAppearance = appearance224;
			comboBoxType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance225.BackColor = System.Drawing.SystemColors.Window;
			appearance225.BorderColor = System.Drawing.Color.Silver;
			comboBoxType.DisplayLayout.Override.RowAppearance = appearance225;
			comboBoxType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance226.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxType.DisplayLayout.Override.TemplateAddRowAppearance = appearance226;
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
			comboBoxType.TabIndex = 13;
			comboBoxType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSponsor.Assigned = false;
			comboBoxSponsor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSponsor.CustomReportFieldName = "";
			comboBoxSponsor.CustomReportKey = "";
			comboBoxSponsor.CustomReportValueType = 1;
			comboBoxSponsor.DescriptionTextBox = null;
			appearance227.BackColor = System.Drawing.SystemColors.Window;
			appearance227.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSponsor.DisplayLayout.Appearance = appearance227;
			comboBoxSponsor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSponsor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance228.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance228.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance228.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance228.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.GroupByBox.Appearance = appearance228;
			appearance229.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance229;
			comboBoxSponsor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance230.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance230.BackColor2 = System.Drawing.SystemColors.Control;
			appearance230.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance230.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSponsor.DisplayLayout.GroupByBox.PromptAppearance = appearance230;
			comboBoxSponsor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSponsor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance231.BackColor = System.Drawing.SystemColors.Window;
			appearance231.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSponsor.DisplayLayout.Override.ActiveCellAppearance = appearance231;
			appearance232.BackColor = System.Drawing.SystemColors.Highlight;
			appearance232.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSponsor.DisplayLayout.Override.ActiveRowAppearance = appearance232;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSponsor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance233.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.CardAreaAppearance = appearance233;
			appearance234.BorderColor = System.Drawing.Color.Silver;
			appearance234.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSponsor.DisplayLayout.Override.CellAppearance = appearance234;
			comboBoxSponsor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSponsor.DisplayLayout.Override.CellPadding = 0;
			appearance235.BackColor = System.Drawing.SystemColors.Control;
			appearance235.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance235.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance235.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance235.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSponsor.DisplayLayout.Override.GroupByRowAppearance = appearance235;
			appearance236.TextHAlignAsString = "Left";
			comboBoxSponsor.DisplayLayout.Override.HeaderAppearance = appearance236;
			comboBoxSponsor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSponsor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance237.BackColor = System.Drawing.SystemColors.Window;
			appearance237.BorderColor = System.Drawing.Color.Silver;
			comboBoxSponsor.DisplayLayout.Override.RowAppearance = appearance237;
			comboBoxSponsor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance238.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSponsor.DisplayLayout.Override.TemplateAddRowAppearance = appearance238;
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
			comboBoxMaritalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMaritalStatus.FormattingEnabled = true;
			comboBoxMaritalStatus.Location = new System.Drawing.Point(528, 266);
			comboBoxMaritalStatus.Name = "comboBoxMaritalStatus";
			comboBoxMaritalStatus.SelectedID = 0;
			comboBoxMaritalStatus.Size = new System.Drawing.Size(130, 21);
			comboBoxMaritalStatus.TabIndex = 25;
			comboBoxNationality.Assigned = false;
			comboBoxNationality.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxNationality.CustomReportFieldName = "";
			comboBoxNationality.CustomReportKey = "";
			comboBoxNationality.CustomReportValueType = 1;
			comboBoxNationality.DescriptionTextBox = null;
			appearance239.BackColor = System.Drawing.SystemColors.Window;
			appearance239.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxNationality.DisplayLayout.Appearance = appearance239;
			comboBoxNationality.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxNationality.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance240.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance240.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance240.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance240.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.GroupByBox.Appearance = appearance240;
			appearance241.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.BandLabelAppearance = appearance241;
			comboBoxNationality.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance242.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance242.BackColor2 = System.Drawing.SystemColors.Control;
			appearance242.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance242.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.PromptAppearance = appearance242;
			comboBoxNationality.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxNationality.DisplayLayout.MaxRowScrollRegions = 1;
			appearance243.BackColor = System.Drawing.SystemColors.Window;
			appearance243.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxNationality.DisplayLayout.Override.ActiveCellAppearance = appearance243;
			appearance244.BackColor = System.Drawing.SystemColors.Highlight;
			appearance244.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxNationality.DisplayLayout.Override.ActiveRowAppearance = appearance244;
			comboBoxNationality.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxNationality.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance245.BackColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.CardAreaAppearance = appearance245;
			appearance246.BorderColor = System.Drawing.Color.Silver;
			appearance246.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxNationality.DisplayLayout.Override.CellAppearance = appearance246;
			comboBoxNationality.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxNationality.DisplayLayout.Override.CellPadding = 0;
			appearance247.BackColor = System.Drawing.SystemColors.Control;
			appearance247.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance247.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance247.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance247.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.GroupByRowAppearance = appearance247;
			appearance248.TextHAlignAsString = "Left";
			comboBoxNationality.DisplayLayout.Override.HeaderAppearance = appearance248;
			comboBoxNationality.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxNationality.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance249.BackColor = System.Drawing.SystemColors.Window;
			appearance249.BorderColor = System.Drawing.Color.Silver;
			comboBoxNationality.DisplayLayout.Override.RowAppearance = appearance249;
			comboBoxNationality.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance250.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxNationality.DisplayLayout.Override.TemplateAddRowAppearance = appearance250;
			comboBoxNationality.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxNationality.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxNationality.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxNationality.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxNationality.Editable = true;
			comboBoxNationality.FilterString = "";
			comboBoxNationality.HasAllAccount = false;
			comboBoxNationality.HasCustom = false;
			comboBoxNationality.IsDataLoaded = false;
			comboBoxNationality.Location = new System.Drawing.Point(132, 243);
			comboBoxNationality.MaxDropDownItems = 12;
			comboBoxNationality.Name = "comboBoxNationality";
			comboBoxNationality.ShowInactiveItems = false;
			comboBoxNationality.ShowQuickAdd = true;
			comboBoxNationality.Size = new System.Drawing.Size(151, 20);
			comboBoxNationality.TabIndex = 20;
			comboBoxNationality.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			genderComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			genderComboBox1.FormattingEnabled = true;
			genderComboBox1.Location = new System.Drawing.Point(577, 243);
			genderComboBox1.Name = "genderComboBox1";
			genderComboBox1.SelectedID = 0;
			genderComboBox1.Size = new System.Drawing.Size(81, 21);
			genderComboBox1.TabIndex = 22;
			comboBoxManager.Assigned = false;
			comboBoxManager.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxManager.CustomReportFieldName = "";
			comboBoxManager.CustomReportKey = "";
			comboBoxManager.CustomReportValueType = 1;
			comboBoxManager.DescriptionTextBox = null;
			appearance251.BackColor = System.Drawing.SystemColors.Window;
			appearance251.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxManager.DisplayLayout.Appearance = appearance251;
			comboBoxManager.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxManager.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance252.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance252.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance252.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance252.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManager.DisplayLayout.GroupByBox.Appearance = appearance252;
			appearance253.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManager.DisplayLayout.GroupByBox.BandLabelAppearance = appearance253;
			comboBoxManager.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance254.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance254.BackColor2 = System.Drawing.SystemColors.Control;
			appearance254.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance254.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManager.DisplayLayout.GroupByBox.PromptAppearance = appearance254;
			comboBoxManager.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxManager.DisplayLayout.MaxRowScrollRegions = 1;
			appearance255.BackColor = System.Drawing.SystemColors.Window;
			appearance255.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxManager.DisplayLayout.Override.ActiveCellAppearance = appearance255;
			appearance256.BackColor = System.Drawing.SystemColors.Highlight;
			appearance256.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxManager.DisplayLayout.Override.ActiveRowAppearance = appearance256;
			comboBoxManager.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxManager.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance257.BackColor = System.Drawing.SystemColors.Window;
			comboBoxManager.DisplayLayout.Override.CardAreaAppearance = appearance257;
			appearance258.BorderColor = System.Drawing.Color.Silver;
			appearance258.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxManager.DisplayLayout.Override.CellAppearance = appearance258;
			comboBoxManager.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxManager.DisplayLayout.Override.CellPadding = 0;
			appearance259.BackColor = System.Drawing.SystemColors.Control;
			appearance259.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance259.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance259.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance259.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManager.DisplayLayout.Override.GroupByRowAppearance = appearance259;
			appearance260.TextHAlignAsString = "Left";
			comboBoxManager.DisplayLayout.Override.HeaderAppearance = appearance260;
			comboBoxManager.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxManager.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance261.BackColor = System.Drawing.SystemColors.Window;
			appearance261.BorderColor = System.Drawing.Color.Silver;
			comboBoxManager.DisplayLayout.Override.RowAppearance = appearance261;
			comboBoxManager.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance262.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxManager.DisplayLayout.Override.TemplateAddRowAppearance = appearance262;
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
			comboBoxManager.TabIndex = 17;
			comboBoxManager.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPosition.Assigned = false;
			comboBoxPosition.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPosition.CustomReportFieldName = "";
			comboBoxPosition.CustomReportKey = "";
			comboBoxPosition.CustomReportValueType = 1;
			comboBoxPosition.DescriptionTextBox = null;
			appearance263.BackColor = System.Drawing.SystemColors.Window;
			appearance263.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPosition.DisplayLayout.Appearance = appearance263;
			comboBoxPosition.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPosition.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance264.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance264.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance264.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance264.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPosition.DisplayLayout.GroupByBox.Appearance = appearance264;
			appearance265.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPosition.DisplayLayout.GroupByBox.BandLabelAppearance = appearance265;
			comboBoxPosition.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance266.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance266.BackColor2 = System.Drawing.SystemColors.Control;
			appearance266.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance266.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPosition.DisplayLayout.GroupByBox.PromptAppearance = appearance266;
			comboBoxPosition.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPosition.DisplayLayout.MaxRowScrollRegions = 1;
			appearance267.BackColor = System.Drawing.SystemColors.Window;
			appearance267.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPosition.DisplayLayout.Override.ActiveCellAppearance = appearance267;
			appearance268.BackColor = System.Drawing.SystemColors.Highlight;
			appearance268.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPosition.DisplayLayout.Override.ActiveRowAppearance = appearance268;
			comboBoxPosition.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPosition.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance269.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPosition.DisplayLayout.Override.CardAreaAppearance = appearance269;
			appearance270.BorderColor = System.Drawing.Color.Silver;
			appearance270.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPosition.DisplayLayout.Override.CellAppearance = appearance270;
			comboBoxPosition.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPosition.DisplayLayout.Override.CellPadding = 0;
			appearance271.BackColor = System.Drawing.SystemColors.Control;
			appearance271.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance271.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance271.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance271.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPosition.DisplayLayout.Override.GroupByRowAppearance = appearance271;
			appearance272.TextHAlignAsString = "Left";
			comboBoxPosition.DisplayLayout.Override.HeaderAppearance = appearance272;
			comboBoxPosition.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPosition.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance273.BackColor = System.Drawing.SystemColors.Window;
			appearance273.BorderColor = System.Drawing.Color.Silver;
			comboBoxPosition.DisplayLayout.Override.RowAppearance = appearance273;
			comboBoxPosition.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance274.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPosition.DisplayLayout.Override.TemplateAddRowAppearance = appearance274;
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
			comboBoxPosition.TabIndex = 16;
			comboBoxPosition.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPosition.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(comboBoxPosition_InitializeLayout);
			comboBoxDepartment.Assigned = false;
			comboBoxDepartment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDepartment.CustomReportFieldName = "";
			comboBoxDepartment.CustomReportKey = "";
			comboBoxDepartment.CustomReportValueType = 1;
			comboBoxDepartment.DescriptionTextBox = null;
			appearance275.BackColor = System.Drawing.SystemColors.Window;
			appearance275.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDepartment.DisplayLayout.Appearance = appearance275;
			comboBoxDepartment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDepartment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance276.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance276.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance276.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance276.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepartment.DisplayLayout.GroupByBox.Appearance = appearance276;
			appearance277.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepartment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance277;
			comboBoxDepartment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance278.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance278.BackColor2 = System.Drawing.SystemColors.Control;
			appearance278.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance278.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDepartment.DisplayLayout.GroupByBox.PromptAppearance = appearance278;
			comboBoxDepartment.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDepartment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance279.BackColor = System.Drawing.SystemColors.Window;
			appearance279.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDepartment.DisplayLayout.Override.ActiveCellAppearance = appearance279;
			appearance280.BackColor = System.Drawing.SystemColors.Highlight;
			appearance280.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDepartment.DisplayLayout.Override.ActiveRowAppearance = appearance280;
			comboBoxDepartment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDepartment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance281.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDepartment.DisplayLayout.Override.CardAreaAppearance = appearance281;
			appearance282.BorderColor = System.Drawing.Color.Silver;
			appearance282.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDepartment.DisplayLayout.Override.CellAppearance = appearance282;
			comboBoxDepartment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDepartment.DisplayLayout.Override.CellPadding = 0;
			appearance283.BackColor = System.Drawing.SystemColors.Control;
			appearance283.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance283.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance283.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance283.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDepartment.DisplayLayout.Override.GroupByRowAppearance = appearance283;
			appearance284.TextHAlignAsString = "Left";
			comboBoxDepartment.DisplayLayout.Override.HeaderAppearance = appearance284;
			comboBoxDepartment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDepartment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance285.BackColor = System.Drawing.SystemColors.Window;
			appearance285.BorderColor = System.Drawing.Color.Silver;
			comboBoxDepartment.DisplayLayout.Override.RowAppearance = appearance285;
			comboBoxDepartment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance286.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDepartment.DisplayLayout.Override.TemplateAddRowAppearance = appearance286;
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
			appearance287.BackColor = System.Drawing.SystemColors.Window;
			appearance287.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDivision.DisplayLayout.Appearance = appearance287;
			comboBoxDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance288.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance288.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance288.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance288.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.GroupByBox.Appearance = appearance288;
			appearance289.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance289;
			comboBoxDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance290.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance290.BackColor2 = System.Drawing.SystemColors.Control;
			appearance290.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance290.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance290;
			comboBoxDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance291.BackColor = System.Drawing.SystemColors.Window;
			appearance291.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDivision.DisplayLayout.Override.ActiveCellAppearance = appearance291;
			appearance292.BackColor = System.Drawing.SystemColors.Highlight;
			appearance292.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDivision.DisplayLayout.Override.ActiveRowAppearance = appearance292;
			comboBoxDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance293.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.CardAreaAppearance = appearance293;
			appearance294.BorderColor = System.Drawing.Color.Silver;
			appearance294.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDivision.DisplayLayout.Override.CellAppearance = appearance294;
			comboBoxDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDivision.DisplayLayout.Override.CellPadding = 0;
			appearance295.BackColor = System.Drawing.SystemColors.Control;
			appearance295.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance295.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance295.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance295.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.GroupByRowAppearance = appearance295;
			appearance296.TextHAlignAsString = "Left";
			comboBoxDivision.DisplayLayout.Override.HeaderAppearance = appearance296;
			comboBoxDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance297.BackColor = System.Drawing.SystemColors.Window;
			appearance297.BorderColor = System.Drawing.Color.Silver;
			comboBoxDivision.DisplayLayout.Override.RowAppearance = appearance297;
			comboBoxDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance298.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance298;
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
			comboBoxGrade.Assigned = false;
			comboBoxGrade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGrade.CustomReportFieldName = "";
			comboBoxGrade.CustomReportKey = "";
			comboBoxGrade.CustomReportValueType = 1;
			comboBoxGrade.DescriptionTextBox = null;
			appearance299.BackColor = System.Drawing.SystemColors.Window;
			appearance299.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGrade.DisplayLayout.Appearance = appearance299;
			comboBoxGrade.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGrade.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance300.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance300.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance300.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance300.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGrade.DisplayLayout.GroupByBox.Appearance = appearance300;
			appearance301.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGrade.DisplayLayout.GroupByBox.BandLabelAppearance = appearance301;
			comboBoxGrade.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance302.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance302.BackColor2 = System.Drawing.SystemColors.Control;
			appearance302.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance302.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGrade.DisplayLayout.GroupByBox.PromptAppearance = appearance302;
			comboBoxGrade.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGrade.DisplayLayout.MaxRowScrollRegions = 1;
			appearance303.BackColor = System.Drawing.SystemColors.Window;
			appearance303.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGrade.DisplayLayout.Override.ActiveCellAppearance = appearance303;
			appearance304.BackColor = System.Drawing.SystemColors.Highlight;
			appearance304.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGrade.DisplayLayout.Override.ActiveRowAppearance = appearance304;
			comboBoxGrade.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGrade.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance305.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGrade.DisplayLayout.Override.CardAreaAppearance = appearance305;
			appearance306.BorderColor = System.Drawing.Color.Silver;
			appearance306.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGrade.DisplayLayout.Override.CellAppearance = appearance306;
			comboBoxGrade.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGrade.DisplayLayout.Override.CellPadding = 0;
			appearance307.BackColor = System.Drawing.SystemColors.Control;
			appearance307.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance307.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance307.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance307.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGrade.DisplayLayout.Override.GroupByRowAppearance = appearance307;
			appearance308.TextHAlignAsString = "Left";
			comboBoxGrade.DisplayLayout.Override.HeaderAppearance = appearance308;
			comboBoxGrade.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGrade.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance309.BackColor = System.Drawing.SystemColors.Window;
			appearance309.BorderColor = System.Drawing.Color.Silver;
			comboBoxGrade.DisplayLayout.Override.RowAppearance = appearance309;
			comboBoxGrade.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance310.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGrade.DisplayLayout.Override.TemplateAddRowAppearance = appearance310;
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
			comboBoxGrade.TabIndex = 15;
			comboBoxGrade.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGroup.Assigned = false;
			comboBoxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGroup.CustomReportFieldName = "";
			comboBoxGroup.CustomReportKey = "";
			comboBoxGroup.CustomReportValueType = 1;
			comboBoxGroup.DescriptionTextBox = null;
			appearance311.BackColor = System.Drawing.SystemColors.Window;
			appearance311.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGroup.DisplayLayout.Appearance = appearance311;
			comboBoxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance312.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance312.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance312.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance312.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.GroupByBox.Appearance = appearance312;
			appearance313.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance313;
			comboBoxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance314.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance314.BackColor2 = System.Drawing.SystemColors.Control;
			appearance314.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance314.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance314;
			comboBoxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance315.BackColor = System.Drawing.SystemColors.Window;
			appearance315.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance315;
			appearance316.BackColor = System.Drawing.SystemColors.Highlight;
			appearance316.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance316;
			comboBoxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance317.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.Override.CardAreaAppearance = appearance317;
			appearance318.BorderColor = System.Drawing.Color.Silver;
			appearance318.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGroup.DisplayLayout.Override.CellAppearance = appearance318;
			comboBoxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance319.BackColor = System.Drawing.SystemColors.Control;
			appearance319.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance319.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance319.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance319.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance319;
			appearance320.TextHAlignAsString = "Left";
			comboBoxGroup.DisplayLayout.Override.HeaderAppearance = appearance320;
			comboBoxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance321.BackColor = System.Drawing.SystemColors.Window;
			appearance321.BorderColor = System.Drawing.Color.Silver;
			comboBoxGroup.DisplayLayout.Override.RowAppearance = appearance321;
			comboBoxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance322.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance322;
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
			comboBoxGroup.TabIndex = 14;
			comboBoxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxQualification.Assigned = false;
			comboBoxQualification.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxQualification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxQualification.CustomReportFieldName = "";
			comboBoxQualification.CustomReportKey = "";
			comboBoxQualification.CustomReportValueType = 1;
			comboBoxQualification.DescriptionTextBox = null;
			appearance323.BackColor = System.Drawing.SystemColors.Window;
			appearance323.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxQualification.DisplayLayout.Appearance = appearance323;
			comboBoxQualification.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxQualification.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance324.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance324.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance324.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance324.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.GroupByBox.Appearance = appearance324;
			appearance325.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxQualification.DisplayLayout.GroupByBox.BandLabelAppearance = appearance325;
			comboBoxQualification.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance326.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance326.BackColor2 = System.Drawing.SystemColors.Control;
			appearance326.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance326.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxQualification.DisplayLayout.GroupByBox.PromptAppearance = appearance326;
			comboBoxQualification.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxQualification.DisplayLayout.MaxRowScrollRegions = 1;
			appearance327.BackColor = System.Drawing.SystemColors.Window;
			appearance327.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxQualification.DisplayLayout.Override.ActiveCellAppearance = appearance327;
			appearance328.BackColor = System.Drawing.SystemColors.Highlight;
			appearance328.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxQualification.DisplayLayout.Override.ActiveRowAppearance = appearance328;
			comboBoxQualification.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxQualification.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance329.BackColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.Override.CardAreaAppearance = appearance329;
			appearance330.BorderColor = System.Drawing.Color.Silver;
			appearance330.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxQualification.DisplayLayout.Override.CellAppearance = appearance330;
			comboBoxQualification.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxQualification.DisplayLayout.Override.CellPadding = 0;
			appearance331.BackColor = System.Drawing.SystemColors.Control;
			appearance331.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance331.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance331.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance331.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxQualification.DisplayLayout.Override.GroupByRowAppearance = appearance331;
			appearance332.TextHAlignAsString = "Left";
			comboBoxQualification.DisplayLayout.Override.HeaderAppearance = appearance332;
			comboBoxQualification.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxQualification.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance333.BackColor = System.Drawing.SystemColors.Window;
			appearance333.BorderColor = System.Drawing.Color.Silver;
			comboBoxQualification.DisplayLayout.Override.RowAppearance = appearance333;
			comboBoxQualification.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance334.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxQualification.DisplayLayout.Override.TemplateAddRowAppearance = appearance334;
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
			comboBoxAccount.Assigned = false;
			comboBoxAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccount.CustomReportFieldName = "";
			comboBoxAccount.CustomReportKey = "";
			comboBoxAccount.CustomReportValueType = 1;
			comboBoxAccount.DescriptionTextBox = null;
			appearance335.BackColor = System.Drawing.SystemColors.Window;
			appearance335.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccount.DisplayLayout.Appearance = appearance335;
			comboBoxAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance336.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance336.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance336.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance336.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.GroupByBox.Appearance = appearance336;
			appearance337.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance337;
			comboBoxAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance338.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance338.BackColor2 = System.Drawing.SystemColors.Control;
			appearance338.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance338.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance338;
			comboBoxAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance339.BackColor = System.Drawing.SystemColors.Window;
			appearance339.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccount.DisplayLayout.Override.ActiveCellAppearance = appearance339;
			appearance340.BackColor = System.Drawing.SystemColors.Highlight;
			appearance340.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccount.DisplayLayout.Override.ActiveRowAppearance = appearance340;
			comboBoxAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance341.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.CardAreaAppearance = appearance341;
			appearance342.BorderColor = System.Drawing.Color.Silver;
			appearance342.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccount.DisplayLayout.Override.CellAppearance = appearance342;
			comboBoxAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccount.DisplayLayout.Override.CellPadding = 0;
			appearance343.BackColor = System.Drawing.SystemColors.Control;
			appearance343.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance343.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance343.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance343.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccount.DisplayLayout.Override.GroupByRowAppearance = appearance343;
			appearance344.TextHAlignAsString = "Left";
			comboBoxAccount.DisplayLayout.Override.HeaderAppearance = appearance344;
			comboBoxAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance345.BackColor = System.Drawing.SystemColors.Window;
			appearance345.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccount.DisplayLayout.Override.RowAppearance = appearance345;
			comboBoxAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance346.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance346;
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
			comboBoxBank.Assigned = false;
			comboBoxBank.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBank.CustomReportFieldName = "";
			comboBoxBank.CustomReportKey = "";
			comboBoxBank.CustomReportValueType = 1;
			comboBoxBank.DescriptionTextBox = textBoxBankName;
			appearance347.BackColor = System.Drawing.SystemColors.Window;
			appearance347.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBank.DisplayLayout.Appearance = appearance347;
			comboBoxBank.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBank.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance348.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance348.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance348.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance348.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.GroupByBox.Appearance = appearance348;
			appearance349.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.BandLabelAppearance = appearance349;
			comboBoxBank.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance350.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance350.BackColor2 = System.Drawing.SystemColors.Control;
			appearance350.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance350.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.PromptAppearance = appearance350;
			comboBoxBank.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBank.DisplayLayout.MaxRowScrollRegions = 1;
			appearance351.BackColor = System.Drawing.SystemColors.Window;
			appearance351.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBank.DisplayLayout.Override.ActiveCellAppearance = appearance351;
			appearance352.BackColor = System.Drawing.SystemColors.Highlight;
			appearance352.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBank.DisplayLayout.Override.ActiveRowAppearance = appearance352;
			comboBoxBank.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBank.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance353.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.CardAreaAppearance = appearance353;
			appearance354.BorderColor = System.Drawing.Color.Silver;
			appearance354.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBank.DisplayLayout.Override.CellAppearance = appearance354;
			comboBoxBank.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBank.DisplayLayout.Override.CellPadding = 0;
			appearance355.BackColor = System.Drawing.SystemColors.Control;
			appearance355.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance355.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance355.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance355.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.GroupByRowAppearance = appearance355;
			appearance356.TextHAlignAsString = "Left";
			comboBoxBank.DisplayLayout.Override.HeaderAppearance = appearance356;
			comboBoxBank.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBank.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance357.BackColor = System.Drawing.SystemColors.Window;
			appearance357.BorderColor = System.Drawing.Color.Silver;
			comboBoxBank.DisplayLayout.Override.RowAppearance = appearance357;
			comboBoxBank.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance358.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBank.DisplayLayout.Override.TemplateAddRowAppearance = appearance358;
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
			comboBoxReligion.Assigned = false;
			comboBoxReligion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxReligion.CustomReportFieldName = "";
			comboBoxReligion.CustomReportKey = "";
			comboBoxReligion.CustomReportValueType = 1;
			comboBoxReligion.DescriptionTextBox = null;
			appearance359.BackColor = System.Drawing.SystemColors.Window;
			appearance359.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxReligion.DisplayLayout.Appearance = appearance359;
			comboBoxReligion.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxReligion.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance360.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance360.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance360.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance360.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.GroupByBox.Appearance = appearance360;
			appearance361.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReligion.DisplayLayout.GroupByBox.BandLabelAppearance = appearance361;
			comboBoxReligion.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance362.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance362.BackColor2 = System.Drawing.SystemColors.Control;
			appearance362.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance362.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReligion.DisplayLayout.GroupByBox.PromptAppearance = appearance362;
			comboBoxReligion.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxReligion.DisplayLayout.MaxRowScrollRegions = 1;
			appearance363.BackColor = System.Drawing.SystemColors.Window;
			appearance363.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxReligion.DisplayLayout.Override.ActiveCellAppearance = appearance363;
			appearance364.BackColor = System.Drawing.SystemColors.Highlight;
			appearance364.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxReligion.DisplayLayout.Override.ActiveRowAppearance = appearance364;
			comboBoxReligion.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxReligion.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance365.BackColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.Override.CardAreaAppearance = appearance365;
			appearance366.BorderColor = System.Drawing.Color.Silver;
			appearance366.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxReligion.DisplayLayout.Override.CellAppearance = appearance366;
			comboBoxReligion.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxReligion.DisplayLayout.Override.CellPadding = 0;
			appearance367.BackColor = System.Drawing.SystemColors.Control;
			appearance367.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance367.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance367.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance367.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.Override.GroupByRowAppearance = appearance367;
			appearance368.TextHAlignAsString = "Left";
			comboBoxReligion.DisplayLayout.Override.HeaderAppearance = appearance368;
			comboBoxReligion.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxReligion.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance369.BackColor = System.Drawing.SystemColors.Window;
			appearance369.BorderColor = System.Drawing.Color.Silver;
			comboBoxReligion.DisplayLayout.Override.RowAppearance = appearance369;
			comboBoxReligion.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance370.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxReligion.DisplayLayout.Override.TemplateAddRowAppearance = appearance370;
			comboBoxReligion.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxReligion.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxReligion.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxReligion.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxReligion.Editable = true;
			comboBoxReligion.FilterString = "";
			comboBoxReligion.HasAllAccount = false;
			comboBoxReligion.HasCustom = false;
			comboBoxReligion.IsDataLoaded = false;
			comboBoxReligion.Location = new System.Drawing.Point(461, 13);
			comboBoxReligion.MaxDropDownItems = 12;
			comboBoxReligion.Name = "comboBoxReligion";
			comboBoxReligion.ShowInactiveItems = false;
			comboBoxReligion.ShowQuickAdd = true;
			comboBoxReligion.Size = new System.Drawing.Size(157, 20);
			comboBoxReligion.TabIndex = 2;
			comboBoxReligion.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			comboBoxDayOff.Location = new System.Drawing.Point(516, 35);
			comboBoxDayOff.Name = "comboBoxDayOff";
			comboBoxDayOff.Size = new System.Drawing.Size(102, 21);
			comboBoxDayOff.TabIndex = 5;
			comboBoxDayOff.Visible = false;
			udfEntryGrid1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid1.Location = new System.Drawing.Point(7, 3);
			udfEntryGrid1.Margin = new System.Windows.Forms.Padding(4);
			udfEntryGrid1.Name = "udfEntryGrid1";
			udfEntryGrid1.Size = new System.Drawing.Size(683, 413);
			udfEntryGrid1.TabIndex = 1;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(719, 609);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "HorseSummaryDetailsForm";
			Text = "Horse Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(EmployeeClassDetailsForm_FormClosing);
			base.Load += new System.EventHandler(HorseSummaryDetailsForm_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			ultraGroupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			tabPageDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			ultraGroupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).EndInit();
			ultraGroupBox5.ResumeLayout(false);
			ultraGroupBox5.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl2).EndInit();
			ultraTabControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxcategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxHorseGender).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxHorseType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCareTaker).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTrainer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOwnershipType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCountry).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSexChangedFrom).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSoldAt).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTransferredTo).EndInit();
			((System.ComponentModel.ISupportInitialize)comboboxReceivedFrom).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxExportedTo).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxImportedFrom).EndInit();
			((System.ComponentModel.ISupportInitialize)workLocationComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSponsor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManager).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPosition).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDepartment).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGrade).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxQualification).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReligion).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			AddEvents();
			comboBoxHorseGender.LoadData();
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
		}

		private void AddEvents()
		{
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxRegisterNumber.TextChanged += textBoxLastName_TextChanged;
			textBoxName.TextChanged += textBoxName_TextChanged;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
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
			labelCustomerNameHeader.Text = textBoxCode.Text + " - " + textBoxName.Text + " " + textBoxRegisterNumber.Text;
			if (textBoxCode.Text.Trim() == "" && textBoxName.Text.Trim() == "" && textBoxRegisterNumber.Text == "")
			{
				labelCustomerNameHeader.Text = "";
			}
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void comboBoxAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void EmployeeDetailsForm_Load(object sender, EventArgs e)
		{
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
			DataRow dataRow = currentData.HorseSummaryTable.Rows[0];
			if (dataRow["HorseCode"] != DBNull.Value)
			{
				textBoxCode.Text = dataRow["HorseCode"].ToString();
			}
			if (dataRow["HorseName"] != DBNull.Value)
			{
				textBoxName.Text = dataRow["HorseName"].ToString();
			}
			if (dataRow["HorseType"] != DBNull.Value)
			{
				comboBoxHorseType.SelectedID = dataRow["HorseType"].ToString();
			}
			if (dataRow["RegisterNumber"] != DBNull.Value)
			{
				textBoxRegisterNumber.Text = dataRow["RegisterNumber"].ToString();
			}
			textBoxMicroChipNumber.Text = dataRow["MicroChipNumber"].ToString();
			textBoxBrand.Text = dataRow["Brand"].ToString();
			textBoxBreed.Text = dataRow["Breed"].ToString();
			comboBoxColor.SelectedIndex = checked(int.Parse(dataRow["Colour"].ToString()) - 1);
			textBoxSire.Text = dataRow["Sire"].ToString();
			textBoxDam.Text = dataRow["Dam"].ToString();
			textBoxSireOfDam.Text = dataRow["SireOfDam"].ToString();
			comboBoxCountry.SelectedID = dataRow["CountryOfOrigin"].ToString();
			textBoxCurrentOwnerShip.Text = dataRow["CurrentOwnerShip"].ToString();
			textBoxPreviousOwnerShip.Text = dataRow["PreviousOwnership"].ToString();
			if (dataRow["OwnerShipChangedDate"] != DBNull.Value)
			{
				datetimePickerOwnerShipChanged.Value = DateTime.Parse(dataRow["OwnerShipChangedDate"].ToString());
				datetimePickerOwnerShipChanged.Checked = true;
			}
			else
			{
				datetimePickerOwnerShipChanged.Checked = false;
			}
			textBoxBreeder.Text = dataRow["Breeder"].ToString();
			comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
			comboBoxTrainer.SelectedID = dataRow["RiderID"].ToString();
			comboBoxCareTaker.SelectedID = dataRow["CareTaker"].ToString();
			comboBoxcategory.SelectedID = dataRow["CategoryID"].ToString();
			comboBoxOwnershipType.SelectedID = dataRow["OwnershipTypeID"].ToString();
			if (dataRow["PassportIssueDate"] != DBNull.Value)
			{
				datetimePickerPassportIssue.Value = DateTime.Parse(dataRow["PassportIssueDate"].ToString());
				datetimePickerPassportIssue.Checked = true;
			}
			else
			{
				datetimePickerPassportIssue.Checked = false;
			}
			if (dataRow["PassportExpiryDate"] != DBNull.Value)
			{
				datetimePickerPassportExpiry.Value = DateTime.Parse(dataRow["PassportExpiryDate"].ToString());
				datetimePickerPassportExpiry.Checked = true;
			}
			else
			{
				datetimePickerPassportExpiry.Checked = false;
			}
			if (dataRow["RevalidationDate"] != DBNull.Value)
			{
				dateTimePickerRevalidation.Value = DateTime.Parse(dataRow["RevalidationDate"].ToString());
				dateTimePickerRevalidation.Checked = true;
			}
			else
			{
				dateTimePickerRevalidation.Checked = false;
			}
			if (dataRow["ImportedDate"] != DBNull.Value)
			{
				dateTimePickerImported.Value = DateTime.Parse(dataRow["ImportedDate"].ToString());
				dateTimePickerImported.Checked = true;
			}
			else
			{
				dateTimePickerImported.Checked = false;
			}
			comboBoxImportedFrom.SelectedID = dataRow["ImportedFrom"].ToString();
			textBoxPastPerformance.Text = dataRow["PastPerformance"].ToString();
			comboBoxExportedTo.SelectedID = dataRow["ExportedTo"].ToString();
			comboboxReceivedFrom.SelectedID = dataRow["ReceivedFrom"].ToString();
			if (dataRow["ReceivedDate"] != DBNull.Value)
			{
				dateTimePickerReceived.Value = DateTime.Parse(dataRow["ReceivedDate"].ToString());
				dateTimePickerReceived.Checked = true;
			}
			else
			{
				dateTimePickerReceived.Checked = false;
			}
			comboBoxTransferredTo.SelectedID = dataRow["TransferredTo"].ToString();
			if (dataRow["TransferredDate"] != DBNull.Value)
			{
				dateTimePickerTransferred.Value = DateTime.Parse(dataRow["TransferredDate"].ToString());
				dateTimePickerTransferred.Checked = true;
			}
			else
			{
				dateTimePickerTransferred.Checked = false;
			}
			comboBoxSoldAt.SelectedID = dataRow["SoldAt"].ToString();
			if (dataRow["SoldDate"] != DBNull.Value)
			{
				dateTimePickerSold.Value = DateTime.Parse(dataRow["SoldDate"].ToString());
				dateTimePickerSold.Checked = true;
			}
			else
			{
				dateTimePickerSold.Checked = false;
			}
			comboBoxSexChangedFrom.SelectedID = dataRow["SexChangedFrom"].ToString();
			if (dataRow["SexChangedDate"] != DBNull.Value)
			{
				dateTimePickerSexChange.Value = DateTime.Parse(dataRow["SexChangedDate"].ToString());
				dateTimePickerSexChange.Checked = true;
			}
			else
			{
				dateTimePickerSexChange.Checked = false;
			}
			if (dataRow["OwnerShipTransferDate"] != DBNull.Value)
			{
				dateTimeOwnerShip.Value = DateTime.Parse(dataRow["OwnerShipTransferDate"].ToString());
				dateTimeOwnerShip.Checked = true;
			}
			else
			{
				dateTimeOwnerShip.Checked = false;
			}
			if (dataRow["DeadDate"] != DBNull.Value)
			{
				dateTimePickerDead.Value = DateTime.Parse(dataRow["DeadDate"].ToString());
				dateTimePickerDead.Checked = true;
			}
			else
			{
				dateTimePickerDead.Checked = false;
			}
			if (dataRow["Sex"] != DBNull.Value)
			{
				comboBoxHorseGender.SelectedID = dataRow["Sex"].ToString();
			}
			if (dataRow["DateOfBirth"] != DBNull.Value)
			{
				dateTimePickerBirthDate.Value = DateTime.Parse(dataRow["DateOfBirth"].ToString());
				dateTimePickerBirthDate.Checked = true;
			}
			else
			{
				dateTimePickerBirthDate.Checked = false;
			}
			if (dataRow["IsInactive"] != DBNull.Value)
			{
				checkBoxInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
			}
			else
			{
				checkBoxInactive.Checked = false;
			}
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
			SetHeaderName();
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
					currentData = new HorseSummaryData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.HorseSummaryTable.Rows[0] : currentData.HorseSummaryTable.NewRow();
				dataRow.BeginEdit();
				dataRow["HorseCode"] = textBoxCode.Text.Trim();
				dataRow["HorseName"] = textBoxName.Text.Trim();
				dataRow["IsInactive"] = checkBoxInactive.Checked;
				dataRow["HorseType"] = comboBoxHorseType.SelectedID;
				dataRow["RegisterNumber"] = textBoxRegisterNumber.Text.Trim();
				dataRow["MicroChipNumber"] = textBoxMicroChipNumber.Text.Trim();
				dataRow["Brand"] = textBoxBrand.Text.Trim();
				dataRow["Breed"] = textBoxBreed.Text.Trim();
				if (dateTimePickerBirthDate.Checked)
				{
					dataRow["DateOfBirth"] = dateTimePickerBirthDate.Value;
				}
				else
				{
					dataRow["DateOfBirth"] = DBNull.Value;
				}
				dataRow["Colour"] = checked(comboBoxColor.SelectedIndex + 1);
				dataRow["Sex"] = comboBoxHorseGender.SelectedID;
				dataRow["OwnershipTypeID"] = comboBoxOwnershipType.SelectedID;
				dataRow["CategoryID"] = comboBoxcategory.SelectedID;
				dataRow["Sire"] = textBoxSire.Text.Trim();
				dataRow["Dam"] = textBoxDam.Text.Trim();
				dataRow["SireOfDam"] = textBoxSireOfDam.Text.Trim();
				dataRow["CountryOfOrigin"] = comboBoxCountry.SelectedID;
				dataRow["CurrentOwnerShip"] = textBoxCurrentOwnerShip.Text.Trim();
				dataRow["PreviousOwnership"] = textBoxPreviousOwnerShip.Text.Trim();
				if (datetimePickerOwnerShipChanged.Checked)
				{
					dataRow["OwnerShipChangedDate"] = datetimePickerOwnerShipChanged.Value;
				}
				else
				{
					dataRow["OwnerShipChangedDate"] = DBNull.Value;
				}
				dataRow["Breeder"] = textBoxBreeder.Text.Trim();
				dataRow["LocationID"] = comboBoxLocation.SelectedID;
				dataRow["RiderID"] = comboBoxTrainer.SelectedID;
				dataRow["CareTaker"] = comboBoxTrainer.SelectedID;
				if (datetimePickerPassportIssue.Checked)
				{
					dataRow["PassportIssueDate"] = datetimePickerPassportIssue.Value;
				}
				else
				{
					dataRow["PassportIssueDate"] = DBNull.Value;
				}
				if (datetimePickerPassportExpiry.Checked)
				{
					dataRow["PassportExpiryDate"] = datetimePickerPassportExpiry.Value;
				}
				else
				{
					dataRow["PassportExpiryDate"] = DBNull.Value;
				}
				if (dateTimePickerRevalidation.Checked)
				{
					dataRow["RevalidationDate"] = dateTimePickerRevalidation.Value;
				}
				else
				{
					dataRow["RevalidationDate"] = DBNull.Value;
				}
				if (dateTimePickerRevalidation.Checked)
				{
					dataRow["RevalidationDate"] = dateTimePickerRevalidation.Value;
				}
				else
				{
					dataRow["RevalidationDate"] = DBNull.Value;
				}
				dataRow["ImportedFrom"] = comboBoxImportedFrom.Value;
				if (dateTimePickerImported.Checked)
				{
					dataRow["ImportedDate"] = dateTimePickerImported.Value;
				}
				else
				{
					dataRow["ImportedDate"] = DBNull.Value;
				}
				dataRow["PastPerformance"] = textBoxPastPerformance.Text;
				dataRow["ExportedTo"] = comboBoxExportedTo.SelectedID;
				dataRow["ReceivedFrom"] = comboboxReceivedFrom.SelectedID;
				if (dateTimePickerReceived.Checked)
				{
					dataRow["ReceivedDate"] = dateTimePickerReceived.Value;
				}
				else
				{
					dataRow["ReceivedDate"] = DBNull.Value;
				}
				dataRow["TransferredTo"] = comboBoxTransferredTo.SelectedID;
				if (dateTimePickerTransferred.Checked)
				{
					dataRow["TransferredDate"] = dateTimePickerTransferred.Value;
				}
				else
				{
					dataRow["TransferredDate"] = DBNull.Value;
				}
				dataRow["SoldAt"] = comboBoxSoldAt.SelectedID;
				if (dateTimePickerSold.Checked)
				{
					dataRow["SoldDate"] = dateTimePickerSold.Value;
				}
				else
				{
					dataRow["SoldDate"] = DBNull.Value;
				}
				dataRow["SexChangedFrom"] = comboBoxSexChangedFrom.SelectedID;
				if (dateTimePickerSexChange.Checked)
				{
					dataRow["SexChangedDate"] = dateTimePickerSexChange.Value;
				}
				else
				{
					dataRow["SexChangedDate"] = DBNull.Value;
				}
				if (dateTimeOwnerShip.Checked)
				{
					dataRow["OwnerShipTransferDate"] = dateTimeOwnerShip.Value;
				}
				else
				{
					dataRow["OwnerShipTransferDate"] = DBNull.Value;
				}
				if (dateTimePickerDead.Checked)
				{
					dataRow["DeadDate"] = dateTimePickerDead.Value;
				}
				else
				{
					dataRow["DeadDate"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.HorseSummaryTable.Rows.Add(dataRow);
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
					foreach (UltraGridCell field in udfEntryGrid1.Fields)
					{
						dataTable.Columns.Add(field.Column.Key, field.Column.DataType);
					}
				}
				dataRow2 = currentData.Tables["UDF"].NewRow();
				foreach (UltraGridCell field2 in udfEntryGrid1.Fields)
				{
					dataRow2[field2.Column.Key] = udfEntryGrid1.Fields[field2.Column.Key].Value;
				}
				dataRow2["EntityID"] = textBoxCode.Text;
				dataRow2.EndEdit();
				currentData.Tables["UDF"].Rows.Add(dataRow2);
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
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Horse_Summary", "HorseCode", textBoxCode.Text.Trim()))
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
				bool flag = (!isNewRecord) ? Factory.HorseSummarySystem.UpdateHorseSummary(currentData) : Factory.HorseSummarySystem.CreateHorseSummary(currentData);
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
					currentData = Factory.HorseSummarySystem.GetHorseSummaryByID(id);
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
			LoadData(DatabaseHelper.GetPreviousID("Horse_Summary", "HorseCode", textBoxCode.Text));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Horse_Summary", "HorseCode", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Horse_Summary", "HorseCode"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Horse_Summary", "HorseCode"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Horse_Summary", "HorseCode", toolStripTextBoxFind.Text.Trim()))
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
				return Factory.HorseSummarySystem.DeleteHorseSummary(textBoxCode.Text);
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Horse_Summary", "HorseCode");
			textBoxCode.Clear();
			textBoxAge.Clear();
			textBoxBrand.Clear();
			textBoxBreed.Clear();
			textBoxCurrentOwnerShip.Clear();
			textBoxMicroChipNumber.Clear();
			comboBoxCareTaker.Clear();
			textBoxDam.Clear();
			textBoxName.Clear();
			textBoxPastPerformance.Clear();
			textBoxPreviousOwnerShip.Clear();
			textBoxRegisterNumber.Clear();
			textBoxSire.Clear();
			textBoxBreeder.Clear();
			textBoxSireOfDam.Clear();
			dateTimePickerBirthDate.Clear();
			dateTimePickerDead.Clear();
			dateTimePickerImported.Clear();
			comboBoxColor.SelectedIndex = -1;
			comboBoxSexChangedFrom.Clear();
			comboBoxSoldAt.Clear();
			comboBoxTrainer.Clear();
			comboBoxTransferredTo.Clear();
			comboboxReceivedFrom.Clear();
			comboBoxLocation.Clear();
			comboBoxImportedFrom.Clear();
			comboBoxHorseGender.Clear();
			comboBoxExportedTo.Clear();
			comboBoxCountry.Clear();
			datetimePickerPassportExpiry.Clear();
			datetimePickerPassportIssue.Clear();
			dateTimePickerReceived.Clear();
			dateTimePickerRevalidation.Clear();
			dateTimePickerReceived.Clear();
			dateTimePickerSold.Clear();
			dateTimePickerTransferred.Clear();
			datetimePickerOwnerShipChanged.Clear();
			dateTimePickerSexChange.Clear();
			comboBoxHorseType.Clear();
			dateTimeOwnerShip.Clear();
			linkLoadImage.Visible = false;
			pictureBoxPhoto.Image = null;
			comboBoxOwnershipType.Clear();
			comboBoxcategory.Clear();
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
		}

		private void buttonMoreAddress_Click(object sender, EventArgs e)
		{
			new FormHelper();
		}

		private void linkLabelCountry_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxLocation.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditRiderSummary(comboBoxTrainer.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditRiderSummary(comboBoxCareTaker.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
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
			new FormHelper();
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
					DataSet horseSummaryReport = Factory.HorseSummarySystem.GetHorseSummaryReport(textBoxCode.Text, textBoxCode.Text, "", "", "", "", showInactive: true);
					if (horseSummaryReport == null || horseSummaryReport.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(horseSummaryReport, "", "Horse Summary", SysDocTypes.None, isPrint, showPrintDialog);
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
			new FormHelper().ShowList(DataComboType.HorseSummary);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = textBoxName.Text + " " + textBoxRegisterNumber.Text;
					docManagementForm.EntityType = EntityTypesEnum.Horse;
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
			new FormHelper();
		}

		private void ultraFormattedLinkLabel11_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddHorsePhoto(textBoxCode.Text, image))
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
					if (Factory.HorseSummarySystem.RemoveHorsePhoto(textBoxCode.Text))
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
					pictureBoxPhoto.Image = PublicFunctions.GetHorseThumbnailImage(textBoxCode.Text);
					linkLoadImage.Visible = false;
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

		private void salaryDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeSalaryDetailsFormObj);
			FormActivator.EmployeeSalaryDetailsFormObj.LoadData(textBoxCode.Text);
		}

		private void ultraFormattedLinkLabel14_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel15_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraTabControl1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void tabPageGeneral_Paint(object sender, PaintEventArgs e)
		{
		}

		private void HorseSummaryDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				dateTimePickerBirthDate.Value = DateTime.Now;
				dateTimeOwnerShip.Value = DateTime.Now;
				datetimePickerPassportIssue.Value = DateTime.Now;
				datetimePickerPassportExpiry.Value = DateTime.Now;
				dateTimePickerRevalidation.Value = DateTime.Now;
				dateTimePickerImported.Value = DateTime.Now;
				dateTimePickerReceived.Value = DateTime.Now;
				dateTimePickerTransferred.Value = DateTime.Now;
				dateTimePickerSold.Value = DateTime.Now;
				dateTimePickerSexChange.Value = DateTime.Now;
				datetimePickerOwnerShipChanged.Value = DateTime.Now;
				dateTimePickerDead.Value = DateTime.Now;
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxPosition_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void udfEntryGrid_Load(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel12_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditHorseType(comboBoxHorseType.SelectedID);
		}

		private void toolStripButtonShowPicture_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel20_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditHorseSex(comboBoxHorseGender.SelectedID);
		}

		private void ultraFormattedLinkLabel21_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditHorseSex(comboBoxSexChangedFrom.SelectedID);
		}

		private void ultraFormattedLinkLabel22_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboBoxCountry.SelectedID);
		}

		private void ultraFormattedLinkLabel23_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboBoxImportedFrom.SelectedID);
		}

		private void ultraFormattedLinkLabel24_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboBoxExportedTo.SelectedID);
		}

		private void ultraFormattedLinkLabel25_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboboxReceivedFrom.SelectedID);
		}

		private void ultraFormattedLinkLabel26_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboBoxTransferredTo.SelectedID);
		}

		private void ultraFormattedLinkLabel27_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCountry(comboBoxSoldAt.SelectedID);
		}

		private void ultraFormattedLinkLabel29_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditHorseCategory(comboBoxcategory.SelectedID);
		}

		private void ultraFormattedLinkLabel28_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditHorseOwnershipType(comboBoxOwnershipType.SelectedID);
		}
	}
}
