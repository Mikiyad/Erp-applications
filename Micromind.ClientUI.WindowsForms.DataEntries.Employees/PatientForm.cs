using Infragistics.Win;
using Infragistics.Win.AppStyling.Runtime;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
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
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class PatientForm : Form, IForm
	{
		private PatientData currentData;

		private const string TABLENAME_CONST = "Patient";

		private const string IDFIELD_CONST = "CustomerID";

		private bool isNewRecord = true;

		private bool EnablePatientAnalysis = CompanyPreferences.EnablePatientAnalysisCode;

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

		private ToolStripMenuItem documentsToolStripMenuItem1;

		private OpenFileDialog openFileDialog1;

		private ToolStripButton toolStripButtonShowPicture;

		private ToolStripButton toolStripButtonInformation;

		private AppStylistRuntime appStylistRuntime1;

		private UltraTabPageControl ultraTabPageControl1;

		private MMTextBox textBoxNote;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl tabPageGeneral;

		private MMLabel mmLabel19;

		private MMLabel labelEmployeeNumber;

		private PictureBox pictureBoxNoImage;

		private UltraFormattedLinkLabel linkRemovePicture;

		private UltraFormattedLinkLabel linkAddPicture;

		private UltraFormattedLinkLabel linkLoadImage;

		private PictureBox pictureBoxPhoto;

		private WorkLocationComboBox comboBoxLocation;

		private MMTextBox textBoxBirthPlace;

		private MMTextBox textBoxAge;

		private MMLabel mmLabel33;

		private MMLabel mmLabel31;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private MaritalStatusComboBox comboBoxMaritalStatus;

		private MMLabel mmLabel51;

		private MMSDateTimePicker dateTimePickerBirthDate;

		private NationalityComboBox comboBoxNationality;

		private MMLabel mmLabel7;

		private UltraFormattedLinkLabel linkLabelCountry;

		private GenderComboBox comboBoxGender;

		private MMTextBox textBoxNationalID;

		private MMLabel mmLabel49;

		private MMLabel mmLabel5;

		private MMLabel lblDescriptions;

		private MMTextBox textBoxNickName;

		private MMTextBox textBoxCode;

		private MMTextBox textBoxLastName;

		private MMTextBox textBoxFirstName;

		private MMLabel mmLabel6;

		private MMLabel label1;

		private MMTextBox textBoxMiddleName;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabControl ultraTabControl1;

		private UltraTabPageControl ultraTabPageControl2;

		private MaritalStatusComboBox comboBoxBloodGroup;

		private MMLabel mmLabel30;

		private MMLabel mmLabel9;

		private MMTextBox textBoxFileNo;

		private MMLabel mmLabel34;

		private DateTimePicker dateTimePickerDate;

		private MMLabel mmLabel1;

		private DataEntryGrid dataGridRelation;

		private CheckedListBox checkedListBoxAllergy;

		private UltraFormattedLinkLabel linkLabelAllergy;

		private UltraFormattedLinkLabel linkLabelChronics;

		private CheckedListBox checkedListBoxChronics;

		private CheckedListBox checkedListBox4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

		private UltraTabPageControl ultraTabPageControl3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private CheckedListBox checkedListBox5;

		private MMTextBox mmTextBox2;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePicker1;

		private MMLabel mmLabel4;

		private MaritalStatusComboBox maritalStatusComboBox2;

		private MMLabel mmLabel22;

		private MMLabel mmLabel24;

		private NationalityComboBox nationalityComboBox2;

		private MMLabel mmLabel25;

		private MMLabel mmLabel32;

		private PictureBox pictureBox1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel9;

		private PictureBox pictureBox2;

		private WorkLocationComboBox workLocationComboBox1;

		private MMTextBox mmTextBox3;

		private MMTextBox mmTextBox4;

		private MMLabel mmLabel35;

		private MMLabel mmLabel36;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel10;

		private MaritalStatusComboBox maritalStatusComboBox3;

		private MMLabel mmLabel37;

		private MMSDateTimePicker mmsDateTimePicker1;

		private NationalityComboBox nationalityComboBox3;

		private MMLabel mmLabel38;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel11;

		private GenderComboBox genderComboBox1;

		private MMTextBox mmTextBox5;

		private MMLabel mmLabel39;

		private MMLabel mmLabel40;

		private MMLabel mmLabel41;

		private MMTextBox mmTextBox6;

		private MMTextBox mmTextBox7;

		private MMTextBox mmTextBox8;

		private MMTextBox mmTextBox9;

		private MMLabel mmLabel42;

		private MMLabel mmLabel43;

		private MMTextBox mmTextBox10;

		private UltraTabPageControl ultraTabPageControl4;

		private MMLabel mmLabel44;

		private DataEntryGrid dataEntryGrid1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel14;

		private CheckedListBox checkedListBox6;

		private UltraGroupBox ultraGroupBox2;

		private AmountTextBox amountTextBox1;

		private MMLabel mmLabel45;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel15;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel16;

		private NumberTextBox numberTextBox1;

		private MMLabel mmLabel46;

		private MMTextBox mmTextBox11;

		private MMLabel mmLabel47;

		private MMSDateTimePicker mmsDateTimePicker2;

		private MMLabel mmLabel48;

		private MMSDateTimePicker mmsDateTimePicker3;

		private MMLabel mmLabel50;

		private GenericListComboBox genericListComboBox1;

		private MMTextBox mmTextBox12;

		private MMTextBox mmTextBox13;

		private MedicalInsuranceProviderComboBox medicalInsuranceProviderComboBox1;

		private UltraTabPageControl ultraTabPageControl5;

		private UDFEntryControl udfEntryControl1;

		private UltraTabPageControl ultraTabPageControl6;

		private MMTextBox mmTextBox14;

		private UltraTabControl ultraTabControl2;

		private UltraGroupBox ultraGroupBox1;

		private MMTextBox textBoxLongitude;

		private MMLabel mmLabel57;

		private MMLabel mmLabel56;

		private MMTextBox textBoxLatitude;

		private LinkLabel linkLabel1;

		private UltraPictureBox ultraPictureBox1;

		private MMLabel mmLabel23;

		private MMTextBox textBoxComment;

		private MMLabel mmLabel8;

		private MMTextBox textBoxDepartment;

		private MMLabel mmLabel10;

		private MMTextBox textBoxWebsite;

		private XPButton xpButton1;

		private MMLabel mmLabel11;

		private MMTextBox textBoxAddressPrintFormat;

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

		private MMLabel mmLabel13;

		private MMTextBox textBoxState;

		private MMLabel mmLabel52;

		private MMTextBox textBoxCity;

		private MMTextBox textBoxAddress3;

		private MMTextBox textBoxAddress2;

		private MMLabel mmLabel53;

		private MMTextBox textBoxAddress1;

		private MMLabel textBoxLocation;

		private MMTextBox textBoxContactName;

		private MMLabel mmLabel54;

		private MMTextBox textBoxAddressID;

		private ReligionComboBox comboBoxReligion;

		private MMTextBox textBoxUID;

		private MMLabel mmLabel55;

		private UltraGroupBox ultraGroupBox5;

		private InsuranceProviderComboBox comboBoxInsuranceProvider;

		private MMTextBox textBoxProvider;

		private Panel panel2;

		private MMLabel mmLabel26;

		private MMSDateTimePicker dateTimePickerValidTo;

		private MMLabel mmLabel27;

		private MMSDateTimePicker datetimePickerEffectiveDate;

		private MMLabel mmLabel28;

		private MMTextBox textBoxInsuranceID;

		private MMLabel mmLabel29;

		private UltraComboEditor comboBoxInsuranceRating;

		private MMLabel mmLabel58;

		private MMTextBox textBoxInsuranceRemarks;

		private MMLabel mmLabel59;

		private MMLabel mmLabel60;

		private MMTextBox textBoxInsuranceNumber;

		private AmountTextBox textBoxInsuranceApprovedAmount;

		private MMLabel mmLabel61;

		private AmountTextBox textBoxInsuranceReqAmount;

		private MMSDateTimePicker dateTimePickerInsuranceDate;

		private MMLabel mmLabel62;

		private MMLabel mmLabel63;

		private ComboBox comboBoxInsuranceStatus;

		private CustomerClassComboBox comboBoxCustomerClass;

		private UltraFormattedLinkLabel linkLabelCustomerClass;

		private UltraFormattedLinkLabel linkLabelProvider;

		private UltraFormattedLinkLabel linkLabelReligion;

		private IContainer components;

		private int slNo = 1;

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

		public PatientForm()
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
			Infragistics.Win.ValueListItem valueListItem = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem10 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem valueListItem11 = new Infragistics.Win.ValueListItem();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.PatientForm));
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			linkLabelReligion = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCustomerClass = new Micromind.DataControls.CustomerClassComboBox();
			linkLabelCustomerClass = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxUID = new Micromind.UISupport.MMTextBox();
			mmLabel55 = new Micromind.UISupport.MMLabel();
			comboBoxReligion = new Micromind.DataControls.ReligionComboBox();
			checkedListBoxAllergy = new System.Windows.Forms.CheckedListBox();
			linkLabelAllergy = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelChronics = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkedListBoxChronics = new System.Windows.Forms.CheckedListBox();
			textBoxFileNo = new Micromind.UISupport.MMTextBox();
			mmLabel34 = new Micromind.UISupport.MMLabel();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			comboBoxBloodGroup = new Micromind.DataControls.MaritalStatusComboBox();
			mmLabel30 = new Micromind.UISupport.MMLabel();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			labelEmployeeNumber = new Micromind.UISupport.MMLabel();
			linkRemovePicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkAddPicture = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLoadImage = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBoxPhoto = new System.Windows.Forms.PictureBox();
			comboBoxLocation = new Micromind.DataControls.WorkLocationComboBox();
			textBoxBirthPlace = new Micromind.UISupport.MMTextBox();
			textBoxAge = new Micromind.UISupport.MMTextBox();
			mmLabel33 = new Micromind.UISupport.MMLabel();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxMaritalStatus = new Micromind.DataControls.MaritalStatusComboBox();
			mmLabel51 = new Micromind.UISupport.MMLabel();
			dateTimePickerBirthDate = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxNationality = new Micromind.DataControls.NationalityComboBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			linkLabelCountry = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxGender = new Micromind.DataControls.GenderComboBox();
			textBoxNationalID = new Micromind.UISupport.MMTextBox();
			mmLabel49 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxNickName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxLastName = new Micromind.UISupport.MMTextBox();
			textBoxFirstName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			label1 = new Micromind.UISupport.MMLabel();
			textBoxMiddleName = new Micromind.UISupport.MMTextBox();
			pictureBoxNoImage = new System.Windows.Forms.PictureBox();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox5 = new Infragistics.Win.Misc.UltraGroupBox();
			linkLabelProvider = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxInsuranceProvider = new Micromind.DataControls.InsuranceProviderComboBox();
			textBoxProvider = new Micromind.UISupport.MMTextBox();
			panel2 = new System.Windows.Forms.Panel();
			mmLabel26 = new Micromind.UISupport.MMLabel();
			dateTimePickerValidTo = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel27 = new Micromind.UISupport.MMLabel();
			datetimePickerEffectiveDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel28 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceID = new Micromind.UISupport.MMTextBox();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			comboBoxInsuranceRating = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			mmLabel58 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceRemarks = new Micromind.UISupport.MMTextBox();
			mmLabel59 = new Micromind.UISupport.MMLabel();
			mmLabel60 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceNumber = new Micromind.UISupport.MMTextBox();
			textBoxInsuranceApprovedAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel61 = new Micromind.UISupport.MMLabel();
			textBoxInsuranceReqAmount = new Micromind.UISupport.AmountTextBox();
			dateTimePickerInsuranceDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel62 = new Micromind.UISupport.MMLabel();
			mmLabel63 = new Micromind.UISupport.MMLabel();
			comboBoxInsuranceStatus = new System.Windows.Forms.ComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			dataGridRelation = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxLongitude = new Micromind.UISupport.MMTextBox();
			mmLabel57 = new Micromind.UISupport.MMLabel();
			mmLabel56 = new Micromind.UISupport.MMLabel();
			textBoxLatitude = new Micromind.UISupport.MMTextBox();
			linkLabel1 = new System.Windows.Forms.LinkLabel();
			ultraPictureBox1 = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			textBoxComment = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxDepartment = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxWebsite = new Micromind.UISupport.MMTextBox();
			xpButton1 = new Micromind.UISupport.XPButton();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxAddressPrintFormat = new Micromind.UISupport.MMTextBox();
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
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxState = new Micromind.UISupport.MMTextBox();
			mmLabel52 = new Micromind.UISupport.MMLabel();
			textBoxCity = new Micromind.UISupport.MMTextBox();
			textBoxAddress3 = new Micromind.UISupport.MMTextBox();
			textBoxAddress2 = new Micromind.UISupport.MMTextBox();
			mmLabel53 = new Micromind.UISupport.MMLabel();
			textBoxAddress1 = new Micromind.UISupport.MMTextBox();
			textBoxLocation = new Micromind.UISupport.MMLabel();
			textBoxContactName = new Micromind.UISupport.MMTextBox();
			mmLabel54 = new Micromind.UISupport.MMLabel();
			textBoxAddressID = new Micromind.UISupport.MMTextBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxNote = new Micromind.UISupport.MMTextBox();
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
			documentsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
			checkedListBox4 = new System.Windows.Forms.CheckedListBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraTabSharedControlsPage2 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkedListBox5 = new System.Windows.Forms.CheckedListBox();
			mmTextBox2 = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			maritalStatusComboBox2 = new Micromind.DataControls.MaritalStatusComboBox();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			nationalityComboBox2 = new Micromind.DataControls.NationalityComboBox();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel9 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			pictureBox2 = new System.Windows.Forms.PictureBox();
			workLocationComboBox1 = new Micromind.DataControls.WorkLocationComboBox();
			mmTextBox3 = new Micromind.UISupport.MMTextBox();
			mmTextBox4 = new Micromind.UISupport.MMTextBox();
			mmLabel35 = new Micromind.UISupport.MMLabel();
			mmLabel36 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel10 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			maritalStatusComboBox3 = new Micromind.DataControls.MaritalStatusComboBox();
			mmLabel37 = new Micromind.UISupport.MMLabel();
			mmsDateTimePicker1 = new Micromind.UISupport.MMSDateTimePicker(components);
			nationalityComboBox3 = new Micromind.DataControls.NationalityComboBox();
			mmLabel38 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel11 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			genderComboBox1 = new Micromind.DataControls.GenderComboBox();
			mmTextBox5 = new Micromind.UISupport.MMTextBox();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			mmLabel40 = new Micromind.UISupport.MMLabel();
			mmLabel41 = new Micromind.UISupport.MMLabel();
			mmTextBox6 = new Micromind.UISupport.MMTextBox();
			mmTextBox7 = new Micromind.UISupport.MMTextBox();
			mmTextBox8 = new Micromind.UISupport.MMTextBox();
			mmTextBox9 = new Micromind.UISupport.MMTextBox();
			mmLabel42 = new Micromind.UISupport.MMLabel();
			mmLabel43 = new Micromind.UISupport.MMLabel();
			mmTextBox10 = new Micromind.UISupport.MMTextBox();
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel44 = new Micromind.UISupport.MMLabel();
			dataEntryGrid1 = new Micromind.DataControls.DataEntryGrid();
			ultraFormattedLinkLabel14 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkedListBox6 = new System.Windows.Forms.CheckedListBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			amountTextBox1 = new Micromind.UISupport.AmountTextBox();
			mmLabel45 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel15 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel16 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			numberTextBox1 = new Micromind.UISupport.NumberTextBox();
			mmLabel46 = new Micromind.UISupport.MMLabel();
			mmTextBox11 = new Micromind.UISupport.MMTextBox();
			mmLabel47 = new Micromind.UISupport.MMLabel();
			mmsDateTimePicker2 = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel48 = new Micromind.UISupport.MMLabel();
			mmsDateTimePicker3 = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel50 = new Micromind.UISupport.MMLabel();
			genericListComboBox1 = new Micromind.DataControls.GenericListComboBox();
			mmTextBox12 = new Micromind.UISupport.MMTextBox();
			mmTextBox13 = new Micromind.UISupport.MMTextBox();
			medicalInsuranceProviderComboBox1 = new Micromind.DataControls.MedicalInsuranceProviderComboBox();
			ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			udfEntryControl1 = new Micromind.DataControls.UDFEntryControl();
			ultraTabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmTextBox14 = new Micromind.UISupport.MMTextBox();
			ultraTabControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomerClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReligion).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).BeginInit();
			ultraGroupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceProvider).BeginInit();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceRating).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridRelation).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			panel1.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			ultraTabPageControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nationalityComboBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)workLocationComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)nationalityComboBox3).BeginInit();
			ultraTabPageControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataEntryGrid1).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)genericListComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)medicalInsuranceProviderComboBox1).BeginInit();
			ultraTabPageControl5.SuspendLayout();
			ultraTabPageControl6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl2).BeginInit();
			ultraTabControl2.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(linkLabelReligion);
			tabPageGeneral.Controls.Add(comboBoxCustomerClass);
			tabPageGeneral.Controls.Add(linkLabelCustomerClass);
			tabPageGeneral.Controls.Add(textBoxUID);
			tabPageGeneral.Controls.Add(mmLabel55);
			tabPageGeneral.Controls.Add(comboBoxReligion);
			tabPageGeneral.Controls.Add(checkedListBoxAllergy);
			tabPageGeneral.Controls.Add(linkLabelAllergy);
			tabPageGeneral.Controls.Add(linkLabelChronics);
			tabPageGeneral.Controls.Add(checkedListBoxChronics);
			tabPageGeneral.Controls.Add(textBoxFileNo);
			tabPageGeneral.Controls.Add(mmLabel34);
			tabPageGeneral.Controls.Add(dateTimePickerDate);
			tabPageGeneral.Controls.Add(mmLabel9);
			tabPageGeneral.Controls.Add(comboBoxBloodGroup);
			tabPageGeneral.Controls.Add(mmLabel30);
			tabPageGeneral.Controls.Add(mmLabel19);
			tabPageGeneral.Controls.Add(labelEmployeeNumber);
			tabPageGeneral.Controls.Add(linkRemovePicture);
			tabPageGeneral.Controls.Add(linkAddPicture);
			tabPageGeneral.Controls.Add(linkLoadImage);
			tabPageGeneral.Controls.Add(pictureBoxPhoto);
			tabPageGeneral.Controls.Add(comboBoxLocation);
			tabPageGeneral.Controls.Add(textBoxBirthPlace);
			tabPageGeneral.Controls.Add(textBoxAge);
			tabPageGeneral.Controls.Add(mmLabel33);
			tabPageGeneral.Controls.Add(mmLabel31);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel8);
			tabPageGeneral.Controls.Add(comboBoxMaritalStatus);
			tabPageGeneral.Controls.Add(mmLabel51);
			tabPageGeneral.Controls.Add(dateTimePickerBirthDate);
			tabPageGeneral.Controls.Add(comboBoxNationality);
			tabPageGeneral.Controls.Add(mmLabel7);
			tabPageGeneral.Controls.Add(linkLabelCountry);
			tabPageGeneral.Controls.Add(comboBoxGender);
			tabPageGeneral.Controls.Add(textBoxNationalID);
			tabPageGeneral.Controls.Add(mmLabel49);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(lblDescriptions);
			tabPageGeneral.Controls.Add(textBoxNickName);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(textBoxLastName);
			tabPageGeneral.Controls.Add(textBoxFirstName);
			tabPageGeneral.Controls.Add(mmLabel6);
			tabPageGeneral.Controls.Add(label1);
			tabPageGeneral.Controls.Add(textBoxMiddleName);
			tabPageGeneral.Controls.Add(pictureBoxNoImage);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(696, 420);
			tabPageGeneral.Paint += new System.Windows.Forms.PaintEventHandler(tabPageGeneral_Paint);
			linkLabelReligion.AutoSize = true;
			linkLabelReligion.Location = new System.Drawing.Point(9, 203);
			linkLabelReligion.Name = "linkLabelReligion";
			linkLabelReligion.Size = new System.Drawing.Size(47, 14);
			linkLabelReligion.TabIndex = 92;
			linkLabelReligion.TabStop = true;
			linkLabelReligion.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelReligion.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelReligion.Value = "Religion:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			linkLabelReligion.VisitedLinkAppearance = appearance;
			linkLabelReligion.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelReligion_LinkClicked);
			comboBoxCustomerClass.Assigned = false;
			comboBoxCustomerClass.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCustomerClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomerClass.CustomReportFieldName = "";
			comboBoxCustomerClass.CustomReportKey = "";
			comboBoxCustomerClass.CustomReportValueType = 1;
			comboBoxCustomerClass.DescriptionTextBox = null;
			appearance2.BackColor = System.Drawing.SystemColors.Window;
			appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomerClass.DisplayLayout.Appearance = appearance2;
			comboBoxCustomerClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomerClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance3.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.Appearance = appearance3;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance5.BackColor2 = System.Drawing.SystemColors.Control;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomerClass.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
			comboBoxCustomerClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomerClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomerClass.DisplayLayout.Override.ActiveCellAppearance = appearance6;
			appearance7.BackColor = System.Drawing.SystemColors.Highlight;
			appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomerClass.DisplayLayout.Override.ActiveRowAppearance = appearance7;
			comboBoxCustomerClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomerClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerClass.DisplayLayout.Override.CardAreaAppearance = appearance8;
			appearance9.BorderColor = System.Drawing.Color.Silver;
			appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomerClass.DisplayLayout.Override.CellAppearance = appearance9;
			comboBoxCustomerClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomerClass.DisplayLayout.Override.CellPadding = 0;
			appearance10.BackColor = System.Drawing.SystemColors.Control;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomerClass.DisplayLayout.Override.GroupByRowAppearance = appearance10;
			appearance11.TextHAlignAsString = "Left";
			comboBoxCustomerClass.DisplayLayout.Override.HeaderAppearance = appearance11;
			comboBoxCustomerClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomerClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomerClass.DisplayLayout.Override.RowAppearance = appearance12;
			comboBoxCustomerClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomerClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
			comboBoxCustomerClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomerClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomerClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomerClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomerClass.Editable = true;
			comboBoxCustomerClass.FilterString = "";
			comboBoxCustomerClass.HasAllAccount = false;
			comboBoxCustomerClass.HasCustom = false;
			comboBoxCustomerClass.IsDataLoaded = false;
			comboBoxCustomerClass.Location = new System.Drawing.Point(381, 155);
			comboBoxCustomerClass.MaxDropDownItems = 12;
			comboBoxCustomerClass.Name = "comboBoxCustomerClass";
			comboBoxCustomerClass.ShowInactiveItems = false;
			comboBoxCustomerClass.ShowQuickAdd = true;
			comboBoxCustomerClass.Size = new System.Drawing.Size(136, 20);
			comboBoxCustomerClass.TabIndex = 11;
			comboBoxCustomerClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelCustomerClass.AutoSize = true;
			linkLabelCustomerClass.Location = new System.Drawing.Point(291, 157);
			linkLabelCustomerClass.Name = "linkLabelCustomerClass";
			linkLabelCustomerClass.Size = new System.Drawing.Size(86, 14);
			linkLabelCustomerClass.TabIndex = 91;
			linkLabelCustomerClass.TabStop = true;
			linkLabelCustomerClass.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCustomerClass.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCustomerClass.Value = "Customer Class:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			linkLabelCustomerClass.VisitedLinkAppearance = appearance14;
			linkLabelCustomerClass.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCustomerClass_LinkClicked);
			textBoxUID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxUID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxUID.BackColor = System.Drawing.Color.White;
			textBoxUID.CustomReportFieldName = "";
			textBoxUID.CustomReportKey = "";
			textBoxUID.CustomReportValueType = 1;
			textBoxUID.IsComboTextBox = false;
			textBoxUID.IsModified = false;
			textBoxUID.Location = new System.Drawing.Point(321, 131);
			textBoxUID.MaxLength = 20;
			textBoxUID.Name = "textBoxUID";
			textBoxUID.Size = new System.Drawing.Size(196, 20);
			textBoxUID.TabIndex = 9;
			mmLabel55.AutoSize = true;
			mmLabel55.BackColor = System.Drawing.Color.Transparent;
			mmLabel55.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel55.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel55.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel55.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel55.IsFieldHeader = false;
			mmLabel55.IsRequired = false;
			mmLabel55.Location = new System.Drawing.Point(291, 133);
			mmLabel55.Name = "mmLabel55";
			mmLabel55.PenWidth = 1f;
			mmLabel55.ShowBorder = false;
			mmLabel55.Size = new System.Drawing.Size(29, 13);
			mmLabel55.TabIndex = 90;
			mmLabel55.Text = "UID:";
			comboBoxReligion.Assigned = false;
			comboBoxReligion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxReligion.CustomReportFieldName = "";
			comboBoxReligion.CustomReportKey = "";
			comboBoxReligion.CustomReportValueType = 1;
			comboBoxReligion.DescriptionTextBox = null;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxReligion.DisplayLayout.Appearance = appearance15;
			comboBoxReligion.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxReligion.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReligion.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxReligion.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReligion.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxReligion.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxReligion.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxReligion.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxReligion.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxReligion.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxReligion.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxReligion.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxReligion.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxReligion.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReligion.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxReligion.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxReligion.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxReligion.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxReligion.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxReligion.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxReligion.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			comboBoxReligion.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxReligion.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxReligion.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxReligion.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxReligion.Editable = true;
			comboBoxReligion.FilterString = "";
			comboBoxReligion.HasAllAccount = false;
			comboBoxReligion.HasCustom = false;
			comboBoxReligion.IsDataLoaded = false;
			comboBoxReligion.Location = new System.Drawing.Point(132, 200);
			comboBoxReligion.MaxDropDownItems = 12;
			comboBoxReligion.Name = "comboBoxReligion";
			comboBoxReligion.ShowInactiveItems = false;
			comboBoxReligion.ShowQuickAdd = true;
			comboBoxReligion.Size = new System.Drawing.Size(151, 20);
			comboBoxReligion.TabIndex = 13;
			comboBoxReligion.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			checkedListBoxAllergy.FormattingEnabled = true;
			checkedListBoxAllergy.Location = new System.Drawing.Point(372, 324);
			checkedListBoxAllergy.Name = "checkedListBoxAllergy";
			checkedListBoxAllergy.Size = new System.Drawing.Size(176, 79);
			checkedListBoxAllergy.TabIndex = 21;
			checkedListBoxAllergy.SelectedIndexChanged += new System.EventHandler(checkedListBoxAllergy_SelectedIndexChanged);
			linkLabelAllergy.AutoSize = true;
			linkLabelAllergy.Location = new System.Drawing.Point(321, 324);
			linkLabelAllergy.Name = "linkLabelAllergy";
			linkLabelAllergy.Size = new System.Drawing.Size(41, 14);
			linkLabelAllergy.TabIndex = 86;
			linkLabelAllergy.TabStop = true;
			linkLabelAllergy.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelAllergy.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelAllergy.Value = "Allergy:";
			appearance27.ForeColor = System.Drawing.Color.Blue;
			linkLabelAllergy.VisitedLinkAppearance = appearance27;
			linkLabelAllergy.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelAllergy_LinkClicked);
			linkLabelChronics.AutoSize = true;
			linkLabelChronics.Location = new System.Drawing.Point(9, 323);
			linkLabelChronics.Name = "linkLabelChronics";
			linkLabelChronics.Size = new System.Drawing.Size(51, 14);
			linkLabelChronics.TabIndex = 85;
			linkLabelChronics.TabStop = true;
			linkLabelChronics.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelChronics.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelChronics.Value = "Chronics:";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			linkLabelChronics.VisitedLinkAppearance = appearance28;
			linkLabelChronics.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelChronics_LinkClicked);
			checkedListBoxChronics.FormattingEnabled = true;
			checkedListBoxChronics.Location = new System.Drawing.Point(132, 324);
			checkedListBoxChronics.Name = "checkedListBoxChronics";
			checkedListBoxChronics.Size = new System.Drawing.Size(182, 79);
			checkedListBoxChronics.TabIndex = 20;
			checkedListBoxChronics.SelectedIndexChanged += new System.EventHandler(checkedListBoxChronics_SelectedIndexChanged);
			textBoxFileNo.BackColor = System.Drawing.Color.White;
			textBoxFileNo.CustomReportFieldName = "";
			textBoxFileNo.CustomReportKey = "";
			textBoxFileNo.CustomReportValueType = 1;
			textBoxFileNo.IsComboTextBox = false;
			textBoxFileNo.IsModified = false;
			textBoxFileNo.IsRequired = true;
			textBoxFileNo.Location = new System.Drawing.Point(529, 5);
			textBoxFileNo.MaxLength = 30;
			textBoxFileNo.Name = "textBoxFileNo";
			textBoxFileNo.Size = new System.Drawing.Size(164, 20);
			textBoxFileNo.TabIndex = 3;
			mmLabel34.AutoSize = true;
			mmLabel34.BackColor = System.Drawing.Color.Transparent;
			mmLabel34.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel34.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel34.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel34.IsFieldHeader = false;
			mmLabel34.IsRequired = false;
			mmLabel34.Location = new System.Drawing.Point(497, 9);
			mmLabel34.Name = "mmLabel34";
			mmLabel34.PenWidth = 1f;
			mmLabel34.ShowBorder = false;
			mmLabel34.Size = new System.Drawing.Size(35, 13);
			mmLabel34.TabIndex = 82;
			mmLabel34.Text = "File#:";
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(389, 5);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(105, 20);
			dateTimePickerDate.TabIndex = 2;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(300, 9);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(82, 13);
			mmLabel9.TabIndex = 1;
			mmLabel9.Text = "File Open Date:";
			comboBoxBloodGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxBloodGroup.FormattingEnabled = true;
			comboBoxBloodGroup.Items.AddRange(new object[9]
			{
				"",
				"A+",
				"B+",
				"O+",
				"AB+",
				"A-",
				"B-",
				"O-",
				"AB-"
			});
			comboBoxBloodGroup.Location = new System.Drawing.Point(132, 298);
			comboBoxBloodGroup.Name = "comboBoxBloodGroup";
			comboBoxBloodGroup.SelectedID = 0;
			comboBoxBloodGroup.Size = new System.Drawing.Size(151, 21);
			comboBoxBloodGroup.TabIndex = 19;
			mmLabel30.AutoSize = true;
			mmLabel30.BackColor = System.Drawing.Color.Transparent;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel30.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(9, 299);
			mmLabel30.Name = "mmLabel30";
			mmLabel30.PenWidth = 1f;
			mmLabel30.ShowBorder = false;
			mmLabel30.Size = new System.Drawing.Size(69, 13);
			mmLabel30.TabIndex = 77;
			mmLabel30.Text = "Blood Group:";
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(9, 275);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(77, 13);
			mmLabel19.TabIndex = 74;
			mmLabel19.Text = "Marital Status:";
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
			labelEmployeeNumber.Size = new System.Drawing.Size(82, 13);
			labelEmployeeNumber.TabIndex = 68;
			labelEmployeeNumber.Text = "Patient Code:";
			labelEmployeeNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			linkRemovePicture.AutoSize = true;
			linkRemovePicture.Location = new System.Drawing.Point(572, 167);
			linkRemovePicture.Name = "linkRemovePicture";
			linkRemovePicture.Size = new System.Drawing.Size(45, 14);
			linkRemovePicture.TabIndex = 19;
			linkRemovePicture.TabStop = true;
			linkRemovePicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkRemovePicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkRemovePicture.Value = "Remove";
			appearance29.ForeColor = System.Drawing.Color.Blue;
			linkRemovePicture.VisitedLinkAppearance = appearance29;
			linkRemovePicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			linkAddPicture.AutoSize = true;
			linkAddPicture.Location = new System.Drawing.Point(533, 167);
			linkAddPicture.Name = "linkAddPicture";
			linkAddPicture.Size = new System.Drawing.Size(23, 14);
			linkAddPicture.TabIndex = 18;
			linkAddPicture.TabStop = true;
			linkAddPicture.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkAddPicture.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkAddPicture.Value = "Add";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			linkAddPicture.VisitedLinkAppearance = appearance30;
			linkAddPicture.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			linkLoadImage.AutoSize = true;
			linkLoadImage.Location = new System.Drawing.Point(558, 83);
			linkLoadImage.Name = "linkLoadImage";
			linkLoadImage.Size = new System.Drawing.Size(66, 14);
			linkLoadImage.TabIndex = 66;
			linkLoadImage.TabStop = true;
			linkLoadImage.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLoadImage.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLoadImage.Value = "Load Picture";
			appearance31.ForeColor = System.Drawing.Color.Blue;
			linkLoadImage.VisitedLinkAppearance = appearance31;
			linkLoadImage.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxPhoto.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxPhoto.Location = new System.Drawing.Point(529, 35);
			pictureBoxPhoto.Name = "pictureBoxPhoto";
			pictureBoxPhoto.Size = new System.Drawing.Size(128, 128);
			pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxPhoto.TabIndex = 65;
			pictureBoxPhoto.TabStop = false;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance32;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance33.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance33;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance34;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance35.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance35.BackColor2 = System.Drawing.SystemColors.Control;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance35;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			appearance36.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance36;
			appearance37.BackColor = System.Drawing.SystemColors.Highlight;
			appearance37.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance37;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance38;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			appearance39.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance39;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance40.BackColor = System.Drawing.SystemColors.Control;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance40;
			appearance41.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance41;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance42.BackColor = System.Drawing.SystemColors.Window;
			appearance42.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance42;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance43.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance43;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(132, 153);
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
			comboBoxLocation.TabIndex = 10;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxBirthPlace.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxBirthPlace.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxBirthPlace.BackColor = System.Drawing.Color.White;
			textBoxBirthPlace.CustomReportFieldName = "";
			textBoxBirthPlace.CustomReportKey = "";
			textBoxBirthPlace.CustomReportValueType = 1;
			textBoxBirthPlace.IsComboTextBox = false;
			textBoxBirthPlace.IsModified = false;
			textBoxBirthPlace.Location = new System.Drawing.Point(132, 248);
			textBoxBirthPlace.MaxLength = 30;
			textBoxBirthPlace.Name = "textBoxBirthPlace";
			textBoxBirthPlace.Size = new System.Drawing.Size(151, 20);
			textBoxBirthPlace.TabIndex = 16;
			textBoxAge.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxAge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxAge.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAge.CustomReportFieldName = "";
			textBoxAge.CustomReportKey = "";
			textBoxAge.CustomReportValueType = 1;
			textBoxAge.IsComboTextBox = false;
			textBoxAge.IsModified = false;
			textBoxAge.Location = new System.Drawing.Point(340, 226);
			textBoxAge.MaxLength = 30;
			textBoxAge.Name = "textBoxAge";
			textBoxAge.ReadOnly = true;
			textBoxAge.Size = new System.Drawing.Size(76, 20);
			textBoxAge.TabIndex = 15;
			textBoxAge.TabStop = false;
			mmLabel33.AutoSize = true;
			mmLabel33.BackColor = System.Drawing.Color.Transparent;
			mmLabel33.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel33.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel33.IsFieldHeader = false;
			mmLabel33.IsRequired = false;
			mmLabel33.Location = new System.Drawing.Point(9, 250);
			mmLabel33.Name = "mmLabel33";
			mmLabel33.PenWidth = 1f;
			mmLabel33.ShowBorder = false;
			mmLabel33.Size = new System.Drawing.Size(61, 13);
			mmLabel33.TabIndex = 30;
			mmLabel33.Text = "Birth Place:";
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(9, 226);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(59, 13);
			mmLabel31.TabIndex = 29;
			mmLabel31.Text = "Birth Date:";
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(9, 178);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel8.TabIndex = 59;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Nationality:";
			appearance44.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance44;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			comboBoxMaritalStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMaritalStatus.FormattingEnabled = true;
			comboBoxMaritalStatus.Location = new System.Drawing.Point(132, 273);
			comboBoxMaritalStatus.Name = "comboBoxMaritalStatus";
			comboBoxMaritalStatus.SelectedID = 0;
			comboBoxMaritalStatus.Size = new System.Drawing.Size(151, 21);
			comboBoxMaritalStatus.TabIndex = 18;
			mmLabel51.AutoSize = true;
			mmLabel51.BackColor = System.Drawing.Color.Transparent;
			mmLabel51.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel51.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel51.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel51.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel51.IsFieldHeader = false;
			mmLabel51.IsRequired = false;
			mmLabel51.Location = new System.Drawing.Point(292, 227);
			mmLabel51.Name = "mmLabel51";
			mmLabel51.PenWidth = 1f;
			mmLabel51.ShowBorder = false;
			mmLabel51.Size = new System.Drawing.Size(30, 13);
			mmLabel51.TabIndex = 57;
			mmLabel51.Text = "Age:";
			dateTimePickerBirthDate.Checked = false;
			dateTimePickerBirthDate.CustomFormat = " ";
			dateTimePickerBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerBirthDate.Location = new System.Drawing.Point(132, 224);
			dateTimePickerBirthDate.Name = "dateTimePickerBirthDate";
			dateTimePickerBirthDate.ShowCheckBox = true;
			dateTimePickerBirthDate.Size = new System.Drawing.Size(151, 20);
			dateTimePickerBirthDate.TabIndex = 14;
			dateTimePickerBirthDate.Value = new System.DateTime(0L);
			dateTimePickerBirthDate.ValueChanged += new System.EventHandler(dateTimePickerBirthDate_ValueChanged);
			comboBoxNationality.Assigned = false;
			comboBoxNationality.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxNationality.CustomReportFieldName = "";
			comboBoxNationality.CustomReportKey = "";
			comboBoxNationality.CustomReportValueType = 1;
			comboBoxNationality.DescriptionTextBox = null;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxNationality.DisplayLayout.Appearance = appearance45;
			comboBoxNationality.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxNationality.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			comboBoxNationality.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNationality.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			comboBoxNationality.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxNationality.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxNationality.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxNationality.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			comboBoxNationality.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxNationality.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxNationality.DisplayLayout.Override.CellAppearance = appearance52;
			comboBoxNationality.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxNationality.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNationality.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			comboBoxNationality.DisplayLayout.Override.HeaderAppearance = appearance54;
			comboBoxNationality.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxNationality.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			comboBoxNationality.DisplayLayout.Override.RowAppearance = appearance55;
			comboBoxNationality.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxNationality.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
			comboBoxNationality.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxNationality.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxNationality.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxNationality.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxNationality.Editable = true;
			comboBoxNationality.FilterString = "";
			comboBoxNationality.HasAllAccount = false;
			comboBoxNationality.HasCustom = false;
			comboBoxNationality.IsDataLoaded = false;
			comboBoxNationality.Location = new System.Drawing.Point(132, 177);
			comboBoxNationality.MaxDropDownItems = 12;
			comboBoxNationality.Name = "comboBoxNationality";
			comboBoxNationality.ShowInactiveItems = false;
			comboBoxNationality.ShowQuickAdd = true;
			comboBoxNationality.Size = new System.Drawing.Size(151, 20);
			comboBoxNationality.TabIndex = 12;
			comboBoxNationality.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(292, 253);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(46, 13);
			mmLabel7.TabIndex = 0;
			mmLabel7.Text = "Gender:";
			linkLabelCountry.AutoSize = true;
			linkLabelCountry.Location = new System.Drawing.Point(9, 153);
			linkLabelCountry.Name = "linkLabelCountry";
			linkLabelCountry.Size = new System.Drawing.Size(49, 14);
			linkLabelCountry.TabIndex = 56;
			linkLabelCountry.TabStop = true;
			linkLabelCountry.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCountry.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCountry.Value = "Location:";
			appearance57.ForeColor = System.Drawing.Color.Blue;
			linkLabelCountry.VisitedLinkAppearance = appearance57;
			linkLabelCountry.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCountry_LinkClicked);
			comboBoxGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxGender.FormattingEnabled = true;
			comboBoxGender.Location = new System.Drawing.Point(340, 250);
			comboBoxGender.Name = "comboBoxGender";
			comboBoxGender.SelectedID = 0;
			comboBoxGender.Size = new System.Drawing.Size(76, 21);
			comboBoxGender.TabIndex = 17;
			textBoxNationalID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxNationalID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxNationalID.BackColor = System.Drawing.Color.White;
			textBoxNationalID.CustomReportFieldName = "";
			textBoxNationalID.CustomReportKey = "";
			textBoxNationalID.CustomReportValueType = 1;
			textBoxNationalID.IsComboTextBox = false;
			textBoxNationalID.IsModified = false;
			textBoxNationalID.Location = new System.Drawing.Point(132, 129);
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
			mmLabel49.Location = new System.Drawing.Point(9, 129);
			mmLabel49.Name = "mmLabel49";
			mmLabel49.PenWidth = 1f;
			mmLabel49.ShowBorder = false;
			mmLabel49.Size = new System.Drawing.Size(64, 13);
			mmLabel49.TabIndex = 53;
			mmLabel49.Text = "National ID:";
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(9, 105);
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
			lblDescriptions.Location = new System.Drawing.Point(9, 81);
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
			textBoxNickName.Location = new System.Drawing.Point(132, 105);
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
			textBoxLastName.Location = new System.Drawing.Point(132, 57);
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
			textBoxFirstName.Location = new System.Drawing.Point(132, 33);
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
			mmLabel6.Location = new System.Drawing.Point(9, 57);
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
			label1.Location = new System.Drawing.Point(9, 33);
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
			textBoxMiddleName.Location = new System.Drawing.Point(132, 81);
			textBoxMiddleName.MaxLength = 30;
			textBoxMiddleName.Name = "textBoxMiddleName";
			textBoxMiddleName.Size = new System.Drawing.Size(385, 20);
			textBoxMiddleName.TabIndex = 6;
			pictureBoxNoImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBoxNoImage.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBoxNoImage.Location = new System.Drawing.Point(608, 103);
			pictureBoxNoImage.Name = "pictureBoxNoImage";
			pictureBoxNoImage.Size = new System.Drawing.Size(49, 48);
			pictureBoxNoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBoxNoImage.TabIndex = 67;
			pictureBoxNoImage.TabStop = false;
			pictureBoxNoImage.Visible = false;
			tabPageDetails.Controls.Add(ultraGroupBox5);
			tabPageDetails.Controls.Add(mmLabel1);
			tabPageDetails.Controls.Add(dataGridRelation);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(696, 420);
			ultraGroupBox5.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox5.Controls.Add(linkLabelProvider);
			ultraGroupBox5.Controls.Add(comboBoxInsuranceProvider);
			ultraGroupBox5.Controls.Add(textBoxProvider);
			ultraGroupBox5.Controls.Add(panel2);
			ultraGroupBox5.Controls.Add(mmLabel63);
			ultraGroupBox5.Controls.Add(comboBoxInsuranceStatus);
			ultraGroupBox5.Location = new System.Drawing.Point(7, 13);
			ultraGroupBox5.Name = "ultraGroupBox5";
			ultraGroupBox5.Size = new System.Drawing.Size(686, 205);
			ultraGroupBox5.TabIndex = 74;
			ultraGroupBox5.Text = "Insurance Info";
			linkLabelProvider.AutoSize = true;
			linkLabelProvider.Location = new System.Drawing.Point(7, 47);
			linkLabelProvider.Name = "linkLabelProvider";
			linkLabelProvider.Size = new System.Drawing.Size(49, 14);
			linkLabelProvider.TabIndex = 86;
			linkLabelProvider.TabStop = true;
			linkLabelProvider.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelProvider.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelProvider.Value = "Provider:";
			appearance58.ForeColor = System.Drawing.Color.Blue;
			linkLabelProvider.VisitedLinkAppearance = appearance58;
			linkLabelProvider.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelProvider_LinkClicked);
			comboBoxInsuranceProvider.Assigned = false;
			comboBoxInsuranceProvider.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxInsuranceProvider.CustomReportFieldName = "";
			comboBoxInsuranceProvider.CustomReportKey = "";
			comboBoxInsuranceProvider.CustomReportValueType = 1;
			comboBoxInsuranceProvider.DescriptionTextBox = textBoxProvider;
			comboBoxInsuranceProvider.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxInsuranceProvider.Editable = true;
			comboBoxInsuranceProvider.FilterString = "";
			comboBoxInsuranceProvider.HasAllAccount = false;
			comboBoxInsuranceProvider.HasCustom = false;
			comboBoxInsuranceProvider.IsDataLoaded = false;
			comboBoxInsuranceProvider.Location = new System.Drawing.Point(115, 46);
			comboBoxInsuranceProvider.MaxDropDownItems = 12;
			comboBoxInsuranceProvider.Name = "comboBoxInsuranceProvider";
			comboBoxInsuranceProvider.ShowInactiveItems = false;
			comboBoxInsuranceProvider.ShowQuickAdd = true;
			comboBoxInsuranceProvider.Size = new System.Drawing.Size(156, 20);
			comboBoxInsuranceProvider.TabIndex = 1;
			comboBoxInsuranceProvider.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxProvider.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProvider.CustomReportFieldName = "";
			textBoxProvider.CustomReportKey = "";
			textBoxProvider.CustomReportValueType = 1;
			textBoxProvider.Enabled = false;
			textBoxProvider.IsComboTextBox = false;
			textBoxProvider.IsModified = false;
			textBoxProvider.Location = new System.Drawing.Point(276, 47);
			textBoxProvider.MaxLength = 64;
			textBoxProvider.Name = "textBoxProvider";
			textBoxProvider.ReadOnly = true;
			textBoxProvider.Size = new System.Drawing.Size(261, 20);
			textBoxProvider.TabIndex = 2;
			textBoxProvider.TabStop = false;
			panel2.Controls.Add(mmLabel26);
			panel2.Controls.Add(dateTimePickerValidTo);
			panel2.Controls.Add(mmLabel27);
			panel2.Controls.Add(datetimePickerEffectiveDate);
			panel2.Controls.Add(mmLabel28);
			panel2.Controls.Add(textBoxInsuranceID);
			panel2.Controls.Add(mmLabel29);
			panel2.Controls.Add(comboBoxInsuranceRating);
			panel2.Controls.Add(mmLabel58);
			panel2.Controls.Add(textBoxInsuranceRemarks);
			panel2.Controls.Add(mmLabel59);
			panel2.Controls.Add(mmLabel60);
			panel2.Controls.Add(textBoxInsuranceNumber);
			panel2.Controls.Add(textBoxInsuranceApprovedAmount);
			panel2.Controls.Add(mmLabel61);
			panel2.Controls.Add(textBoxInsuranceReqAmount);
			panel2.Controls.Add(dateTimePickerInsuranceDate);
			panel2.Controls.Add(mmLabel62);
			panel2.Location = new System.Drawing.Point(1, 69);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(627, 135);
			panel2.TabIndex = 1;
			mmLabel26.AutoSize = true;
			mmLabel26.BackColor = System.Drawing.Color.Transparent;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(277, 75);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(48, 13);
			mmLabel26.TabIndex = 82;
			mmLabel26.Text = "Valid To:";
			dateTimePickerValidTo.Checked = false;
			dateTimePickerValidTo.CustomFormat = " ";
			dateTimePickerValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerValidTo.Location = new System.Drawing.Point(386, 71);
			dateTimePickerValidTo.Name = "dateTimePickerValidTo";
			dateTimePickerValidTo.ShowCheckBox = true;
			dateTimePickerValidTo.Size = new System.Drawing.Size(149, 20);
			dateTimePickerValidTo.TabIndex = 7;
			dateTimePickerValidTo.Value = new System.DateTime(0L);
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(7, 75);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(80, 13);
			mmLabel27.TabIndex = 81;
			mmLabel27.Text = "Effective Date:";
			datetimePickerEffectiveDate.Checked = false;
			datetimePickerEffectiveDate.CustomFormat = " ";
			datetimePickerEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			datetimePickerEffectiveDate.Location = new System.Drawing.Point(115, 71);
			datetimePickerEffectiveDate.Name = "datetimePickerEffectiveDate";
			datetimePickerEffectiveDate.ShowCheckBox = true;
			datetimePickerEffectiveDate.Size = new System.Drawing.Size(156, 20);
			datetimePickerEffectiveDate.TabIndex = 6;
			datetimePickerEffectiveDate.Value = new System.DateTime(0L);
			mmLabel28.AutoSize = true;
			mmLabel28.BackColor = System.Drawing.Color.Transparent;
			mmLabel28.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel28.IsFieldHeader = false;
			mmLabel28.IsRequired = false;
			mmLabel28.Location = new System.Drawing.Point(277, 50);
			mmLabel28.Name = "mmLabel28";
			mmLabel28.PenWidth = 1f;
			mmLabel28.ShowBorder = false;
			mmLabel28.Size = new System.Drawing.Size(73, 13);
			mmLabel28.TabIndex = 74;
			mmLabel28.Text = "Insurance ID:";
			textBoxInsuranceID.BackColor = System.Drawing.Color.White;
			textBoxInsuranceID.CustomReportFieldName = "";
			textBoxInsuranceID.CustomReportKey = "";
			textBoxInsuranceID.CustomReportValueType = 1;
			textBoxInsuranceID.IsComboTextBox = false;
			textBoxInsuranceID.IsModified = false;
			textBoxInsuranceID.Location = new System.Drawing.Point(387, 47);
			textBoxInsuranceID.MaxLength = 30;
			textBoxInsuranceID.Name = "textBoxInsuranceID";
			textBoxInsuranceID.Size = new System.Drawing.Size(149, 20);
			textBoxInsuranceID.TabIndex = 5;
			mmLabel29.AutoSize = true;
			mmLabel29.BackColor = System.Drawing.Color.Transparent;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(7, 50);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(42, 13);
			mmLabel29.TabIndex = 71;
			mmLabel29.Text = "Rating:";
			comboBoxInsuranceRating.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			valueListItem.DataValue = (byte)0;
			valueListItem.DisplayText = "N/A";
			valueListItem2.DataValue = (byte)1;
			valueListItem2.DisplayText = "1";
			valueListItem3.DataValue = "2";
			valueListItem3.DisplayText = "2";
			valueListItem4.DataValue = (byte)3;
			valueListItem4.DisplayText = "3";
			valueListItem5.DataValue = "4";
			valueListItem5.DisplayText = "4";
			valueListItem6.DataValue = (byte)5;
			valueListItem6.DisplayText = "5";
			valueListItem7.DataValue = (byte)6;
			valueListItem7.DisplayText = "6";
			valueListItem8.DataValue = "7";
			valueListItem8.DisplayText = "7";
			valueListItem9.DataValue = "8";
			valueListItem9.DisplayText = "8";
			valueListItem10.DataValue = "9";
			valueListItem10.DisplayText = "9";
			valueListItem11.DataValue = "10";
			valueListItem11.DisplayText = "10";
			comboBoxInsuranceRating.Items.AddRange(new Infragistics.Win.ValueListItem[11]
			{
				valueListItem,
				valueListItem2,
				valueListItem3,
				valueListItem4,
				valueListItem5,
				valueListItem6,
				valueListItem7,
				valueListItem8,
				valueListItem9,
				valueListItem10,
				valueListItem11
			});
			comboBoxInsuranceRating.Location = new System.Drawing.Point(115, 47);
			comboBoxInsuranceRating.Name = "comboBoxInsuranceRating";
			comboBoxInsuranceRating.Size = new System.Drawing.Size(106, 21);
			comboBoxInsuranceRating.TabIndex = 4;
			mmLabel58.AutoSize = true;
			mmLabel58.BackColor = System.Drawing.Color.Transparent;
			mmLabel58.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel58.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel58.IsFieldHeader = false;
			mmLabel58.IsRequired = false;
			mmLabel58.Location = new System.Drawing.Point(7, 98);
			mmLabel58.Name = "mmLabel58";
			mmLabel58.PenWidth = 1f;
			mmLabel58.ShowBorder = false;
			mmLabel58.Size = new System.Drawing.Size(52, 13);
			mmLabel58.TabIndex = 69;
			mmLabel58.Text = "Remarks:";
			textBoxInsuranceRemarks.BackColor = System.Drawing.Color.White;
			textBoxInsuranceRemarks.CustomReportFieldName = "";
			textBoxInsuranceRemarks.CustomReportKey = "";
			textBoxInsuranceRemarks.CustomReportValueType = 1;
			textBoxInsuranceRemarks.IsComboTextBox = false;
			textBoxInsuranceRemarks.IsModified = false;
			textBoxInsuranceRemarks.Location = new System.Drawing.Point(115, 94);
			textBoxInsuranceRemarks.MaxLength = 255;
			textBoxInsuranceRemarks.Multiline = true;
			textBoxInsuranceRemarks.Name = "textBoxInsuranceRemarks";
			textBoxInsuranceRemarks.Size = new System.Drawing.Size(421, 30);
			textBoxInsuranceRemarks.TabIndex = 8;
			mmLabel59.AutoSize = true;
			mmLabel59.BackColor = System.Drawing.Color.Transparent;
			mmLabel59.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel59.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel59.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel59.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel59.IsFieldHeader = false;
			mmLabel59.IsRequired = false;
			mmLabel59.Location = new System.Drawing.Point(277, 6);
			mmLabel59.Name = "mmLabel59";
			mmLabel59.PenWidth = 1f;
			mmLabel59.ShowBorder = false;
			mmLabel59.Size = new System.Drawing.Size(103, 13);
			mmLabel59.TabIndex = 67;
			mmLabel59.Text = "Application Number:";
			mmLabel60.AutoSize = true;
			mmLabel60.BackColor = System.Drawing.Color.Transparent;
			mmLabel60.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel60.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel60.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel60.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel60.IsFieldHeader = false;
			mmLabel60.IsRequired = false;
			mmLabel60.Location = new System.Drawing.Point(277, 29);
			mmLabel60.Name = "mmLabel60";
			mmLabel60.PenWidth = 1f;
			mmLabel60.ShowBorder = false;
			mmLabel60.Size = new System.Drawing.Size(98, 13);
			mmLabel60.TabIndex = 65;
			mmLabel60.Text = "Approved Amount:";
			textBoxInsuranceNumber.BackColor = System.Drawing.Color.White;
			textBoxInsuranceNumber.CustomReportFieldName = "";
			textBoxInsuranceNumber.CustomReportKey = "";
			textBoxInsuranceNumber.CustomReportValueType = 1;
			textBoxInsuranceNumber.IsComboTextBox = false;
			textBoxInsuranceNumber.IsModified = false;
			textBoxInsuranceNumber.Location = new System.Drawing.Point(387, 3);
			textBoxInsuranceNumber.MaxLength = 30;
			textBoxInsuranceNumber.Name = "textBoxInsuranceNumber";
			textBoxInsuranceNumber.Size = new System.Drawing.Size(149, 20);
			textBoxInsuranceNumber.TabIndex = 1;
			textBoxInsuranceApprovedAmount.AllowDecimal = true;
			textBoxInsuranceApprovedAmount.BackColor = System.Drawing.Color.White;
			textBoxInsuranceApprovedAmount.CustomReportFieldName = "";
			textBoxInsuranceApprovedAmount.CustomReportKey = "";
			textBoxInsuranceApprovedAmount.CustomReportValueType = 1;
			textBoxInsuranceApprovedAmount.IsComboTextBox = false;
			textBoxInsuranceApprovedAmount.IsModified = false;
			textBoxInsuranceApprovedAmount.Location = new System.Drawing.Point(387, 25);
			textBoxInsuranceApprovedAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxInsuranceApprovedAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxInsuranceApprovedAmount.Name = "textBoxInsuranceApprovedAmount";
			textBoxInsuranceApprovedAmount.NullText = "0";
			textBoxInsuranceApprovedAmount.Size = new System.Drawing.Size(149, 20);
			textBoxInsuranceApprovedAmount.TabIndex = 3;
			textBoxInsuranceApprovedAmount.Text = "0.00";
			textBoxInsuranceApprovedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInsuranceApprovedAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel61.AutoSize = true;
			mmLabel61.BackColor = System.Drawing.Color.Transparent;
			mmLabel61.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel61.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel61.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel61.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel61.IsFieldHeader = false;
			mmLabel61.IsRequired = false;
			mmLabel61.Location = new System.Drawing.Point(7, 29);
			mmLabel61.Name = "mmLabel61";
			mmLabel61.PenWidth = 1f;
			mmLabel61.ShowBorder = false;
			mmLabel61.Size = new System.Drawing.Size(103, 13);
			mmLabel61.TabIndex = 63;
			mmLabel61.Text = "Requested Amount:";
			textBoxInsuranceReqAmount.AllowDecimal = true;
			textBoxInsuranceReqAmount.BackColor = System.Drawing.Color.White;
			textBoxInsuranceReqAmount.CustomReportFieldName = "";
			textBoxInsuranceReqAmount.CustomReportKey = "";
			textBoxInsuranceReqAmount.CustomReportValueType = 1;
			textBoxInsuranceReqAmount.IsComboTextBox = false;
			textBoxInsuranceReqAmount.IsModified = false;
			textBoxInsuranceReqAmount.Location = new System.Drawing.Point(115, 25);
			textBoxInsuranceReqAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxInsuranceReqAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxInsuranceReqAmount.Name = "textBoxInsuranceReqAmount";
			textBoxInsuranceReqAmount.NullText = "0";
			textBoxInsuranceReqAmount.Size = new System.Drawing.Size(156, 20);
			textBoxInsuranceReqAmount.TabIndex = 2;
			textBoxInsuranceReqAmount.Text = "0.00";
			textBoxInsuranceReqAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInsuranceReqAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			dateTimePickerInsuranceDate.Checked = false;
			dateTimePickerInsuranceDate.CustomFormat = " ";
			dateTimePickerInsuranceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerInsuranceDate.Location = new System.Drawing.Point(115, 3);
			dateTimePickerInsuranceDate.Name = "dateTimePickerInsuranceDate";
			dateTimePickerInsuranceDate.ShowCheckBox = true;
			dateTimePickerInsuranceDate.Size = new System.Drawing.Size(156, 20);
			dateTimePickerInsuranceDate.TabIndex = 0;
			dateTimePickerInsuranceDate.Value = new System.DateTime(0L);
			mmLabel62.AutoSize = true;
			mmLabel62.BackColor = System.Drawing.Color.Transparent;
			mmLabel62.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel62.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel62.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel62.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel62.IsFieldHeader = false;
			mmLabel62.IsRequired = false;
			mmLabel62.Location = new System.Drawing.Point(7, 6);
			mmLabel62.Name = "mmLabel62";
			mmLabel62.PenWidth = 1f;
			mmLabel62.ShowBorder = false;
			mmLabel62.Size = new System.Drawing.Size(89, 13);
			mmLabel62.TabIndex = 61;
			mmLabel62.Text = "Application Date:";
			mmLabel63.AutoSize = true;
			mmLabel63.BackColor = System.Drawing.Color.Transparent;
			mmLabel63.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel63.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel63.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel63.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel63.IsFieldHeader = false;
			mmLabel63.IsRequired = false;
			mmLabel63.Location = new System.Drawing.Point(7, 25);
			mmLabel63.Name = "mmLabel63";
			mmLabel63.PenWidth = 1f;
			mmLabel63.ShowBorder = false;
			mmLabel63.Size = new System.Drawing.Size(93, 13);
			mmLabel63.TabIndex = 65;
			mmLabel63.Text = "Insurance Status:";
			comboBoxInsuranceStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxInsuranceStatus.FormattingEnabled = true;
			comboBoxInsuranceStatus.Items.AddRange(new object[7]
			{
				"Not Insured",
				"Under Process",
				"Insured",
				"Insured-Sublimit of Parent",
				"Rejected",
				"On Hold",
				"Cancelled"
			});
			comboBoxInsuranceStatus.Location = new System.Drawing.Point(115, 22);
			comboBoxInsuranceStatus.Name = "comboBoxInsuranceStatus";
			comboBoxInsuranceStatus.Size = new System.Drawing.Size(156, 21);
			comboBoxInsuranceStatus.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(10, 174);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(73, 13);
			mmLabel1.TabIndex = 73;
			mmLabel1.Text = "Relationships:";
			dataGridRelation.AllowAddNew = false;
			dataGridRelation.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridRelation.DisplayLayout.Appearance = appearance59;
			dataGridRelation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridRelation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			dataGridRelation.DisplayLayout.GroupByBox.Appearance = appearance60;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridRelation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance61;
			dataGridRelation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance62.BackColor2 = System.Drawing.SystemColors.Control;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridRelation.DisplayLayout.GroupByBox.PromptAppearance = appearance62;
			dataGridRelation.DisplayLayout.MaxColScrollRegions = 1;
			dataGridRelation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridRelation.DisplayLayout.Override.ActiveCellAppearance = appearance63;
			appearance64.BackColor = System.Drawing.SystemColors.Highlight;
			appearance64.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridRelation.DisplayLayout.Override.ActiveRowAppearance = appearance64;
			dataGridRelation.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridRelation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridRelation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			dataGridRelation.DisplayLayout.Override.CardAreaAppearance = appearance65;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			appearance66.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridRelation.DisplayLayout.Override.CellAppearance = appearance66;
			dataGridRelation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridRelation.DisplayLayout.Override.CellPadding = 0;
			appearance67.BackColor = System.Drawing.SystemColors.Control;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			dataGridRelation.DisplayLayout.Override.GroupByRowAppearance = appearance67;
			appearance68.TextHAlignAsString = "Left";
			dataGridRelation.DisplayLayout.Override.HeaderAppearance = appearance68;
			dataGridRelation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridRelation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.Color.Silver;
			dataGridRelation.DisplayLayout.Override.RowAppearance = appearance69;
			dataGridRelation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridRelation.DisplayLayout.Override.TemplateAddRowAppearance = appearance70;
			dataGridRelation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridRelation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridRelation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridRelation.IncludeLotItems = false;
			dataGridRelation.LoadLayoutFailed = false;
			dataGridRelation.Location = new System.Drawing.Point(13, 225);
			dataGridRelation.Name = "dataGridRelation";
			dataGridRelation.ShowClearMenu = true;
			dataGridRelation.ShowDeleteMenu = true;
			dataGridRelation.ShowInsertMenu = true;
			dataGridRelation.ShowMoveRowsMenu = true;
			dataGridRelation.Size = new System.Drawing.Size(671, 191);
			dataGridRelation.TabIndex = 72;
			ultraTabPageControl2.Controls.Add(ultraGroupBox1);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(696, 420);
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(textBoxLongitude);
			ultraGroupBox1.Controls.Add(mmLabel57);
			ultraGroupBox1.Controls.Add(mmLabel56);
			ultraGroupBox1.Controls.Add(textBoxLatitude);
			ultraGroupBox1.Controls.Add(linkLabel1);
			ultraGroupBox1.Controls.Add(ultraPictureBox1);
			ultraGroupBox1.Controls.Add(mmLabel23);
			ultraGroupBox1.Controls.Add(textBoxComment);
			ultraGroupBox1.Controls.Add(mmLabel8);
			ultraGroupBox1.Controls.Add(textBoxDepartment);
			ultraGroupBox1.Controls.Add(mmLabel10);
			ultraGroupBox1.Controls.Add(textBoxWebsite);
			ultraGroupBox1.Controls.Add(xpButton1);
			ultraGroupBox1.Controls.Add(mmLabel11);
			ultraGroupBox1.Controls.Add(textBoxAddressPrintFormat);
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
			ultraGroupBox1.Controls.Add(mmLabel13);
			ultraGroupBox1.Controls.Add(textBoxState);
			ultraGroupBox1.Controls.Add(mmLabel52);
			ultraGroupBox1.Controls.Add(textBoxCity);
			ultraGroupBox1.Controls.Add(textBoxAddress3);
			ultraGroupBox1.Controls.Add(textBoxAddress2);
			ultraGroupBox1.Controls.Add(mmLabel53);
			ultraGroupBox1.Controls.Add(textBoxAddress1);
			ultraGroupBox1.Controls.Add(textBoxLocation);
			ultraGroupBox1.Controls.Add(textBoxContactName);
			ultraGroupBox1.Controls.Add(mmLabel54);
			ultraGroupBox1.Controls.Add(textBoxAddressID);
			ultraGroupBox1.Location = new System.Drawing.Point(0, 3);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(687, 297);
			ultraGroupBox1.TabIndex = 17;
			ultraGroupBox1.Text = "Primary Address";
			textBoxLongitude.BackColor = System.Drawing.Color.White;
			textBoxLongitude.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLongitude.CustomReportFieldName = "";
			textBoxLongitude.CustomReportKey = "";
			textBoxLongitude.CustomReportValueType = 1;
			textBoxLongitude.IsComboTextBox = false;
			textBoxLongitude.IsModified = false;
			textBoxLongitude.Location = new System.Drawing.Point(442, 219);
			textBoxLongitude.MaxLength = 64;
			textBoxLongitude.Name = "textBoxLongitude";
			textBoxLongitude.Size = new System.Drawing.Size(146, 20);
			textBoxLongitude.TabIndex = 42;
			mmLabel57.AutoSize = true;
			mmLabel57.BackColor = System.Drawing.Color.Transparent;
			mmLabel57.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel57.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel57.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel57.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel57.IsFieldHeader = false;
			mmLabel57.IsRequired = false;
			mmLabel57.Location = new System.Drawing.Point(373, 223);
			mmLabel57.Name = "mmLabel57";
			mmLabel57.PenWidth = 1f;
			mmLabel57.ShowBorder = false;
			mmLabel57.Size = new System.Drawing.Size(58, 13);
			mmLabel57.TabIndex = 44;
			mmLabel57.Text = "Longitude:";
			mmLabel56.AutoSize = true;
			mmLabel56.BackColor = System.Drawing.Color.Transparent;
			mmLabel56.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel56.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel56.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel56.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel56.IsFieldHeader = false;
			mmLabel56.IsRequired = false;
			mmLabel56.Location = new System.Drawing.Point(373, 200);
			mmLabel56.Name = "mmLabel56";
			mmLabel56.PenWidth = 1f;
			mmLabel56.ShowBorder = false;
			mmLabel56.Size = new System.Drawing.Size(50, 13);
			mmLabel56.TabIndex = 43;
			mmLabel56.Text = "Latitude:";
			textBoxLatitude.BackColor = System.Drawing.Color.White;
			textBoxLatitude.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLatitude.CustomReportFieldName = "";
			textBoxLatitude.CustomReportKey = "";
			textBoxLatitude.CustomReportValueType = 1;
			textBoxLatitude.IsComboTextBox = false;
			textBoxLatitude.IsModified = false;
			textBoxLatitude.Location = new System.Drawing.Point(442, 196);
			textBoxLatitude.MaxLength = 64;
			textBoxLatitude.Name = "textBoxLatitude";
			textBoxLatitude.Size = new System.Drawing.Size(146, 20);
			textBoxLatitude.TabIndex = 41;
			linkLabel1.AutoSize = true;
			linkLabel1.Location = new System.Drawing.Point(481, 223);
			linkLabel1.Name = "linkLabel1";
			linkLabel1.Size = new System.Drawing.Size(55, 13);
			linkLabel1.TabIndex = 40;
			linkLabel1.TabStop = true;
			linkLabel1.Text = "linkLabel1";
			linkLabel1.Visible = false;
			ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
			ultraPictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			ultraPictureBox1.Image = resources.GetObject("ultraPictureBox1.Image");
			ultraPictureBox1.Location = new System.Drawing.Point(582, 197);
			ultraPictureBox1.Name = "ultraPictureBox1";
			ultraPictureBox1.Size = new System.Drawing.Size(89, 42);
			ultraPictureBox1.TabIndex = 35;
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(373, 176);
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
			textBoxComment.Location = new System.Drawing.Point(442, 173);
			textBoxComment.MaxLength = 255;
			textBoxComment.Name = "textBoxComment";
			textBoxComment.Size = new System.Drawing.Size(229, 20);
			textBoxComment.TabIndex = 33;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(373, 22);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(68, 13);
			mmLabel8.TabIndex = 18;
			mmLabel8.Text = "Department:";
			textBoxDepartment.BackColor = System.Drawing.Color.White;
			textBoxDepartment.CustomReportFieldName = "";
			textBoxDepartment.CustomReportKey = "";
			textBoxDepartment.CustomReportValueType = 1;
			textBoxDepartment.IsComboTextBox = false;
			textBoxDepartment.IsModified = false;
			textBoxDepartment.Location = new System.Drawing.Point(442, 19);
			textBoxDepartment.MaxLength = 30;
			textBoxDepartment.Name = "textBoxDepartment";
			textBoxDepartment.Size = new System.Drawing.Size(229, 20);
			textBoxDepartment.TabIndex = 19;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(373, 154);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(50, 13);
			mmLabel10.TabIndex = 30;
			mmLabel10.Text = "Website:";
			textBoxWebsite.BackColor = System.Drawing.Color.White;
			textBoxWebsite.CustomReportFieldName = "";
			textBoxWebsite.CustomReportKey = "";
			textBoxWebsite.CustomReportValueType = 1;
			textBoxWebsite.IsComboTextBox = false;
			textBoxWebsite.IsModified = false;
			textBoxWebsite.Location = new System.Drawing.Point(442, 151);
			textBoxWebsite.MaxLength = 255;
			textBoxWebsite.Name = "textBoxWebsite";
			textBoxWebsite.Size = new System.Drawing.Size(229, 20);
			textBoxWebsite.TabIndex = 31;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(537, 267);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(134, 24);
			xpButton1.TabIndex = 34;
			xpButton1.Text = "More Addresses...";
			xpButton1.UseVisualStyleBackColor = false;
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(9, 218);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(112, 13);
			mmLabel11.TabIndex = 16;
			mmLabel11.Text = "Address Print Format:";
			textBoxAddressPrintFormat.BackColor = System.Drawing.Color.White;
			textBoxAddressPrintFormat.CustomReportFieldName = "";
			textBoxAddressPrintFormat.CustomReportKey = "";
			textBoxAddressPrintFormat.CustomReportValueType = 1;
			textBoxAddressPrintFormat.IsComboTextBox = false;
			textBoxAddressPrintFormat.IsModified = false;
			textBoxAddressPrintFormat.Location = new System.Drawing.Point(132, 217);
			textBoxAddressPrintFormat.MaxLength = 255;
			textBoxAddressPrintFormat.Multiline = true;
			textBoxAddressPrintFormat.Name = "textBoxAddressPrintFormat";
			textBoxAddressPrintFormat.Size = new System.Drawing.Size(229, 74);
			textBoxAddressPrintFormat.TabIndex = 17;
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(9, 198);
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
			textBoxPostalCode.Location = new System.Drawing.Point(132, 195);
			textBoxPostalCode.MaxLength = 30;
			textBoxPostalCode.Name = "textBoxPostalCode";
			textBoxPostalCode.Size = new System.Drawing.Size(229, 20);
			textBoxPostalCode.TabIndex = 15;
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(373, 132);
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
			textBoxEmail.Location = new System.Drawing.Point(442, 129);
			textBoxEmail.MaxLength = 64;
			textBoxEmail.Name = "textBoxEmail";
			textBoxEmail.Size = new System.Drawing.Size(229, 20);
			textBoxEmail.TabIndex = 29;
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(373, 110);
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
			textBoxMobile.Location = new System.Drawing.Point(442, 107);
			textBoxMobile.MaxLength = 30;
			textBoxMobile.Name = "textBoxMobile";
			textBoxMobile.Size = new System.Drawing.Size(229, 20);
			textBoxMobile.TabIndex = 27;
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(373, 87);
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
			textBoxFax.Location = new System.Drawing.Point(442, 85);
			textBoxFax.MaxLength = 30;
			textBoxFax.Name = "textBoxFax";
			textBoxFax.Size = new System.Drawing.Size(229, 20);
			textBoxFax.TabIndex = 25;
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(373, 66);
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
			textBoxPhone2.Location = new System.Drawing.Point(442, 63);
			textBoxPhone2.MaxLength = 30;
			textBoxPhone2.Name = "textBoxPhone2";
			textBoxPhone2.Size = new System.Drawing.Size(229, 20);
			textBoxPhone2.TabIndex = 23;
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(373, 44);
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
			textBoxPhone1.Location = new System.Drawing.Point(442, 41);
			textBoxPhone1.MaxLength = 30;
			textBoxPhone1.Name = "textBoxPhone1";
			textBoxPhone1.Size = new System.Drawing.Size(229, 20);
			textBoxPhone1.TabIndex = 21;
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(9, 176);
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
			textBoxCountry.Location = new System.Drawing.Point(132, 173);
			textBoxCountry.MaxLength = 30;
			textBoxCountry.Name = "textBoxCountry";
			textBoxCountry.Size = new System.Drawing.Size(229, 20);
			textBoxCountry.TabIndex = 13;
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(9, 154);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(37, 13);
			mmLabel13.TabIndex = 10;
			mmLabel13.Text = "State:";
			textBoxState.BackColor = System.Drawing.Color.White;
			textBoxState.CustomReportFieldName = "";
			textBoxState.CustomReportKey = "";
			textBoxState.CustomReportValueType = 1;
			textBoxState.IsComboTextBox = false;
			textBoxState.IsModified = false;
			textBoxState.Location = new System.Drawing.Point(132, 151);
			textBoxState.MaxLength = 30;
			textBoxState.Name = "textBoxState";
			textBoxState.Size = new System.Drawing.Size(229, 20);
			textBoxState.TabIndex = 11;
			mmLabel52.AutoSize = true;
			mmLabel52.BackColor = System.Drawing.Color.Transparent;
			mmLabel52.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel52.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel52.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel52.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel52.IsFieldHeader = false;
			mmLabel52.IsRequired = false;
			mmLabel52.Location = new System.Drawing.Point(9, 131);
			mmLabel52.Name = "mmLabel52";
			mmLabel52.PenWidth = 1f;
			mmLabel52.ShowBorder = false;
			mmLabel52.Size = new System.Drawing.Size(30, 13);
			mmLabel52.TabIndex = 8;
			mmLabel52.Text = "City:";
			textBoxCity.BackColor = System.Drawing.Color.White;
			textBoxCity.CustomReportFieldName = "";
			textBoxCity.CustomReportKey = "";
			textBoxCity.CustomReportValueType = 1;
			textBoxCity.IsComboTextBox = false;
			textBoxCity.IsModified = false;
			textBoxCity.Location = new System.Drawing.Point(132, 129);
			textBoxCity.MaxLength = 30;
			textBoxCity.Name = "textBoxCity";
			textBoxCity.Size = new System.Drawing.Size(229, 20);
			textBoxCity.TabIndex = 9;
			textBoxAddress3.BackColor = System.Drawing.Color.White;
			textBoxAddress3.CustomReportFieldName = "";
			textBoxAddress3.CustomReportKey = "";
			textBoxAddress3.CustomReportValueType = 1;
			textBoxAddress3.IsComboTextBox = false;
			textBoxAddress3.IsModified = false;
			textBoxAddress3.Location = new System.Drawing.Point(132, 107);
			textBoxAddress3.MaxLength = 64;
			textBoxAddress3.Name = "textBoxAddress3";
			textBoxAddress3.Size = new System.Drawing.Size(229, 20);
			textBoxAddress3.TabIndex = 7;
			textBoxAddress2.BackColor = System.Drawing.Color.White;
			textBoxAddress2.CustomReportFieldName = "";
			textBoxAddress2.CustomReportKey = "";
			textBoxAddress2.CustomReportValueType = 1;
			textBoxAddress2.IsComboTextBox = false;
			textBoxAddress2.IsModified = false;
			textBoxAddress2.Location = new System.Drawing.Point(132, 85);
			textBoxAddress2.MaxLength = 64;
			textBoxAddress2.Name = "textBoxAddress2";
			textBoxAddress2.Size = new System.Drawing.Size(229, 20);
			textBoxAddress2.TabIndex = 6;
			mmLabel53.AutoSize = true;
			mmLabel53.BackColor = System.Drawing.Color.Transparent;
			mmLabel53.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel53.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel53.IsFieldHeader = false;
			mmLabel53.IsRequired = false;
			mmLabel53.Location = new System.Drawing.Point(9, 65);
			mmLabel53.Name = "mmLabel53";
			mmLabel53.PenWidth = 1f;
			mmLabel53.ShowBorder = false;
			mmLabel53.Size = new System.Drawing.Size(50, 13);
			mmLabel53.TabIndex = 4;
			mmLabel53.Text = "Address:";
			textBoxAddress1.BackColor = System.Drawing.Color.White;
			textBoxAddress1.CustomReportFieldName = "";
			textBoxAddress1.CustomReportKey = "";
			textBoxAddress1.CustomReportValueType = 1;
			textBoxAddress1.IsComboTextBox = false;
			textBoxAddress1.IsModified = false;
			textBoxAddress1.Location = new System.Drawing.Point(132, 63);
			textBoxAddress1.MaxLength = 64;
			textBoxAddress1.Name = "textBoxAddress1";
			textBoxAddress1.Size = new System.Drawing.Size(229, 20);
			textBoxAddress1.TabIndex = 5;
			textBoxLocation.AutoSize = true;
			textBoxLocation.BackColor = System.Drawing.Color.Transparent;
			textBoxLocation.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			textBoxLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			textBoxLocation.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			textBoxLocation.IsFieldHeader = false;
			textBoxLocation.IsRequired = false;
			textBoxLocation.Location = new System.Drawing.Point(9, 43);
			textBoxLocation.Name = "textBoxLocation";
			textBoxLocation.PenWidth = 1f;
			textBoxLocation.ShowBorder = false;
			textBoxLocation.Size = new System.Drawing.Size(79, 13);
			textBoxLocation.TabIndex = 2;
			textBoxLocation.Text = "Contact Name:";
			textBoxContactName.BackColor = System.Drawing.Color.White;
			textBoxContactName.CustomReportFieldName = "";
			textBoxContactName.CustomReportKey = "";
			textBoxContactName.CustomReportValueType = 1;
			textBoxContactName.IsComboTextBox = false;
			textBoxContactName.IsModified = false;
			textBoxContactName.Location = new System.Drawing.Point(132, 41);
			textBoxContactName.MaxLength = 64;
			textBoxContactName.Name = "textBoxContactName";
			textBoxContactName.Size = new System.Drawing.Size(229, 20);
			textBoxContactName.TabIndex = 3;
			mmLabel54.AutoSize = true;
			mmLabel54.BackColor = System.Drawing.Color.Transparent;
			mmLabel54.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel54.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel54.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel54.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel54.IsFieldHeader = false;
			mmLabel54.IsRequired = false;
			mmLabel54.Location = new System.Drawing.Point(9, 22);
			mmLabel54.Name = "mmLabel54";
			mmLabel54.PenWidth = 1f;
			mmLabel54.ShowBorder = false;
			mmLabel54.Size = new System.Drawing.Size(64, 13);
			mmLabel54.TabIndex = 0;
			mmLabel54.Text = "Address ID:";
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
			textBoxAddressID.TabIndex = 1;
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
			toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				documentsToolStripMenuItem1
			});
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.salesperson;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(77, 24);
			toolStripButton1.Text = "More...";
			documentsToolStripMenuItem1.Name = "documentsToolStripMenuItem1";
			documentsToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
			documentsToolStripMenuItem1.Text = "Documents";
			documentsToolStripMenuItem1.Click += new System.EventHandler(documentsToolStripMenuItem_Click);
			toolStripButtonShowPicture.CheckOnClick = true;
			toolStripButtonShowPicture.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonShowPicture.Image");
			toolStripButtonShowPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowPicture.Name = "toolStripButtonShowPicture";
			toolStripButtonShowPicture.Size = new System.Drawing.Size(100, 24);
			toolStripButtonShowPicture.Text = "Show Picture";
			toolStripButtonShowPicture.ToolTipText = "Auto load pictures";
			toolStripButtonShowPicture.Visible = false;
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
			appearance71.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance71;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Details";
			ultraTab3.TabPage = ultraTabPageControl2;
			ultraTab3.Text = "&Address";
			ultraTab4.TabPage = ultraTabPageControl1;
			ultraTab4.Text = "Note";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4
			});
			checkedListBox4.FormattingEnabled = true;
			checkedListBox4.Location = new System.Drawing.Point(389, 160);
			checkedListBox4.Name = "checkedListBox4";
			checkedListBox4.Size = new System.Drawing.Size(176, 79);
			checkedListBox4.TabIndex = 71;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(338, 160);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(41, 14);
			ultraFormattedLinkLabel4.TabIndex = 70;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Allergy:";
			appearance72.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance72;
			ultraTabSharedControlsPage2.Location = new System.Drawing.Point(1, 20);
			ultraTabSharedControlsPage2.Name = "ultraTabSharedControlsPage2";
			ultraTabSharedControlsPage2.Size = new System.Drawing.Size(196, 77);
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel5);
			ultraTabPageControl3.Controls.Add(checkedListBox5);
			ultraTabPageControl3.Controls.Add(mmTextBox2);
			ultraTabPageControl3.Controls.Add(mmLabel2);
			ultraTabPageControl3.Controls.Add(dateTimePicker1);
			ultraTabPageControl3.Controls.Add(mmLabel4);
			ultraTabPageControl3.Controls.Add(maritalStatusComboBox2);
			ultraTabPageControl3.Controls.Add(mmLabel22);
			ultraTabPageControl3.Controls.Add(mmLabel24);
			ultraTabPageControl3.Controls.Add(nationalityComboBox2);
			ultraTabPageControl3.Controls.Add(mmLabel25);
			ultraTabPageControl3.Controls.Add(mmLabel32);
			ultraTabPageControl3.Controls.Add(pictureBox1);
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel6);
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel7);
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel9);
			ultraTabPageControl3.Controls.Add(pictureBox2);
			ultraTabPageControl3.Controls.Add(workLocationComboBox1);
			ultraTabPageControl3.Controls.Add(mmTextBox3);
			ultraTabPageControl3.Controls.Add(mmTextBox4);
			ultraTabPageControl3.Controls.Add(mmLabel35);
			ultraTabPageControl3.Controls.Add(mmLabel36);
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel10);
			ultraTabPageControl3.Controls.Add(maritalStatusComboBox3);
			ultraTabPageControl3.Controls.Add(mmLabel37);
			ultraTabPageControl3.Controls.Add(mmsDateTimePicker1);
			ultraTabPageControl3.Controls.Add(nationalityComboBox3);
			ultraTabPageControl3.Controls.Add(mmLabel38);
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel11);
			ultraTabPageControl3.Controls.Add(genderComboBox1);
			ultraTabPageControl3.Controls.Add(mmTextBox5);
			ultraTabPageControl3.Controls.Add(mmLabel39);
			ultraTabPageControl3.Controls.Add(mmLabel40);
			ultraTabPageControl3.Controls.Add(mmLabel41);
			ultraTabPageControl3.Controls.Add(mmTextBox6);
			ultraTabPageControl3.Controls.Add(mmTextBox7);
			ultraTabPageControl3.Controls.Add(mmTextBox8);
			ultraTabPageControl3.Controls.Add(mmTextBox9);
			ultraTabPageControl3.Controls.Add(mmLabel42);
			ultraTabPageControl3.Controls.Add(mmLabel43);
			ultraTabPageControl3.Controls.Add(mmTextBox10);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(696, 420);
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(2, 295);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(51, 14);
			ultraFormattedLinkLabel5.TabIndex = 85;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Chronics:";
			appearance73.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance73;
			checkedListBox5.FormattingEnabled = true;
			checkedListBox5.Location = new System.Drawing.Point(132, 295);
			checkedListBox5.Name = "checkedListBox5";
			checkedListBox5.Size = new System.Drawing.Size(182, 79);
			checkedListBox5.TabIndex = 84;
			mmTextBox2.BackColor = System.Drawing.Color.White;
			mmTextBox2.CustomReportFieldName = "";
			mmTextBox2.CustomReportKey = "";
			mmTextBox2.CustomReportValueType = 1;
			mmTextBox2.IsComboTextBox = false;
			mmTextBox2.IsModified = false;
			mmTextBox2.IsRequired = true;
			mmTextBox2.Location = new System.Drawing.Point(529, 5);
			mmTextBox2.MaxLength = 30;
			mmTextBox2.Name = "mmTextBox2";
			mmTextBox2.Size = new System.Drawing.Size(164, 20);
			mmTextBox2.TabIndex = 83;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(497, 9);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(35, 13);
			mmLabel2.TabIndex = 82;
			mmLabel2.Text = "File#:";
			dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePicker1.Location = new System.Drawing.Point(389, 5);
			dateTimePicker1.Name = "dateTimePicker1";
			dateTimePicker1.Size = new System.Drawing.Size(105, 20);
			dateTimePicker1.TabIndex = 81;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(300, 9);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(82, 13);
			mmLabel4.TabIndex = 80;
			mmLabel4.Text = "File Open Date:";
			maritalStatusComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			maritalStatusComboBox2.FormattingEnabled = true;
			maritalStatusComboBox2.Location = new System.Drawing.Point(132, 268);
			maritalStatusComboBox2.Name = "maritalStatusComboBox2";
			maritalStatusComboBox2.SelectedID = 0;
			maritalStatusComboBox2.Size = new System.Drawing.Size(130, 21);
			maritalStatusComboBox2.TabIndex = 78;
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(11, 271);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(69, 13);
			mmLabel22.TabIndex = 77;
			mmLabel22.Text = "Blood Group:";
			mmLabel24.AutoSize = true;
			mmLabel24.BackColor = System.Drawing.Color.Transparent;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel24.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(287, 169);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(48, 13);
			mmLabel24.TabIndex = 76;
			mmLabel24.Text = "Religion:";
			nationalityComboBox2.Assigned = false;
			nationalityComboBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			nationalityComboBox2.CustomReportFieldName = "";
			nationalityComboBox2.CustomReportKey = "";
			nationalityComboBox2.CustomReportValueType = 1;
			nationalityComboBox2.DescriptionTextBox = null;
			appearance74.BackColor = System.Drawing.SystemColors.Window;
			appearance74.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			nationalityComboBox2.DisplayLayout.Appearance = appearance74;
			nationalityComboBox2.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			nationalityComboBox2.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance75.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance75.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance75.BorderColor = System.Drawing.SystemColors.Window;
			nationalityComboBox2.DisplayLayout.GroupByBox.Appearance = appearance75;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			nationalityComboBox2.DisplayLayout.GroupByBox.BandLabelAppearance = appearance76;
			nationalityComboBox2.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance77.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance77.BackColor2 = System.Drawing.SystemColors.Control;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			nationalityComboBox2.DisplayLayout.GroupByBox.PromptAppearance = appearance77;
			nationalityComboBox2.DisplayLayout.MaxColScrollRegions = 1;
			nationalityComboBox2.DisplayLayout.MaxRowScrollRegions = 1;
			appearance78.BackColor = System.Drawing.SystemColors.Window;
			appearance78.ForeColor = System.Drawing.SystemColors.ControlText;
			nationalityComboBox2.DisplayLayout.Override.ActiveCellAppearance = appearance78;
			appearance79.BackColor = System.Drawing.SystemColors.Highlight;
			appearance79.ForeColor = System.Drawing.SystemColors.HighlightText;
			nationalityComboBox2.DisplayLayout.Override.ActiveRowAppearance = appearance79;
			nationalityComboBox2.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			nationalityComboBox2.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance80.BackColor = System.Drawing.SystemColors.Window;
			nationalityComboBox2.DisplayLayout.Override.CardAreaAppearance = appearance80;
			appearance81.BorderColor = System.Drawing.Color.Silver;
			appearance81.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			nationalityComboBox2.DisplayLayout.Override.CellAppearance = appearance81;
			nationalityComboBox2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			nationalityComboBox2.DisplayLayout.Override.CellPadding = 0;
			appearance82.BackColor = System.Drawing.SystemColors.Control;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			nationalityComboBox2.DisplayLayout.Override.GroupByRowAppearance = appearance82;
			appearance83.TextHAlignAsString = "Left";
			nationalityComboBox2.DisplayLayout.Override.HeaderAppearance = appearance83;
			nationalityComboBox2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			nationalityComboBox2.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance84.BackColor = System.Drawing.SystemColors.Window;
			appearance84.BorderColor = System.Drawing.Color.Silver;
			nationalityComboBox2.DisplayLayout.Override.RowAppearance = appearance84;
			nationalityComboBox2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance85.BackColor = System.Drawing.SystemColors.ControlLight;
			nationalityComboBox2.DisplayLayout.Override.TemplateAddRowAppearance = appearance85;
			nationalityComboBox2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			nationalityComboBox2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			nationalityComboBox2.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			nationalityComboBox2.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			nationalityComboBox2.Editable = true;
			nationalityComboBox2.FilterString = "";
			nationalityComboBox2.HasAllAccount = false;
			nationalityComboBox2.HasCustom = false;
			nationalityComboBox2.IsDataLoaded = false;
			nationalityComboBox2.Location = new System.Drawing.Point(347, 165);
			nationalityComboBox2.MaxDropDownItems = 12;
			nationalityComboBox2.Name = "nationalityComboBox2";
			nationalityComboBox2.ShowInactiveItems = false;
			nationalityComboBox2.ShowQuickAdd = true;
			nationalityComboBox2.Size = new System.Drawing.Size(151, 20);
			nationalityComboBox2.TabIndex = 75;
			nationalityComboBox2.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(11, 244);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(77, 13);
			mmLabel25.TabIndex = 74;
			mmLabel25.Text = "Marital Status:";
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
			mmLabel32.Size = new System.Drawing.Size(82, 13);
			mmLabel32.TabIndex = 68;
			mmLabel32.Text = "Patient Code:";
			mmLabel32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBox1.Image = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBox1.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBox1.Location = new System.Drawing.Point(623, 167);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(49, 48);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 67;
			pictureBox1.TabStop = false;
			pictureBox1.Visible = false;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(572, 167);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(45, 14);
			ultraFormattedLinkLabel6.TabIndex = 19;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Remove";
			appearance86.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance86;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkRemovePicture_LinkClicked);
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(533, 167);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(23, 14);
			ultraFormattedLinkLabel7.TabIndex = 18;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Add";
			appearance87.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance87;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkAddPicture_LinkClicked);
			ultraFormattedLinkLabel9.AutoSize = true;
			ultraFormattedLinkLabel9.Location = new System.Drawing.Point(558, 83);
			ultraFormattedLinkLabel9.Name = "ultraFormattedLinkLabel9";
			ultraFormattedLinkLabel9.Size = new System.Drawing.Size(66, 14);
			ultraFormattedLinkLabel9.TabIndex = 66;
			ultraFormattedLinkLabel9.TabStop = true;
			ultraFormattedLinkLabel9.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel9.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel9.Value = "Load Picture";
			appearance88.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel9.VisitedLinkAppearance = appearance88;
			ultraFormattedLinkLabel9.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLoadImage_LinkClicked);
			pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			pictureBox2.InitialImage = Micromind.ClientUI.Properties.Resources.noimage;
			pictureBox2.Location = new System.Drawing.Point(529, 35);
			pictureBox2.Name = "pictureBox2";
			pictureBox2.Size = new System.Drawing.Size(128, 128);
			pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBox2.TabIndex = 65;
			pictureBox2.TabStop = false;
			workLocationComboBox1.Assigned = false;
			workLocationComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			workLocationComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			workLocationComboBox1.CustomReportFieldName = "";
			workLocationComboBox1.CustomReportKey = "";
			workLocationComboBox1.CustomReportValueType = 1;
			workLocationComboBox1.DescriptionTextBox = null;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			workLocationComboBox1.DisplayLayout.Appearance = appearance89;
			workLocationComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			workLocationComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance90.BorderColor = System.Drawing.SystemColors.Window;
			workLocationComboBox1.DisplayLayout.GroupByBox.Appearance = appearance90;
			appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
			workLocationComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance91;
			workLocationComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance92.BackColor2 = System.Drawing.SystemColors.Control;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance92.ForeColor = System.Drawing.SystemColors.GrayText;
			workLocationComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance92;
			workLocationComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			workLocationComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.ForeColor = System.Drawing.SystemColors.ControlText;
			workLocationComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance93;
			appearance94.BackColor = System.Drawing.SystemColors.Highlight;
			appearance94.ForeColor = System.Drawing.SystemColors.HighlightText;
			workLocationComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance94;
			workLocationComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			workLocationComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			workLocationComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance95;
			appearance96.BorderColor = System.Drawing.Color.Silver;
			appearance96.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			workLocationComboBox1.DisplayLayout.Override.CellAppearance = appearance96;
			workLocationComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			workLocationComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance97.BackColor = System.Drawing.SystemColors.Control;
			appearance97.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance97.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance97.BorderColor = System.Drawing.SystemColors.Window;
			workLocationComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance97;
			appearance98.TextHAlignAsString = "Left";
			workLocationComboBox1.DisplayLayout.Override.HeaderAppearance = appearance98;
			workLocationComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			workLocationComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.BorderColor = System.Drawing.Color.Silver;
			workLocationComboBox1.DisplayLayout.Override.RowAppearance = appearance99;
			workLocationComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLight;
			workLocationComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance100;
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
			mmTextBox3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			mmTextBox3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			mmTextBox3.BackColor = System.Drawing.Color.White;
			mmTextBox3.CustomReportFieldName = "";
			mmTextBox3.CustomReportKey = "";
			mmTextBox3.CustomReportValueType = 1;
			mmTextBox3.IsComboTextBox = false;
			mmTextBox3.IsModified = false;
			mmTextBox3.Location = new System.Drawing.Point(349, 191);
			mmTextBox3.MaxLength = 30;
			mmTextBox3.Name = "mmTextBox3";
			mmTextBox3.Size = new System.Drawing.Size(153, 20);
			mmTextBox3.TabIndex = 21;
			mmTextBox4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			mmTextBox4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			mmTextBox4.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBox4.CustomReportFieldName = "";
			mmTextBox4.CustomReportKey = "";
			mmTextBox4.CustomReportValueType = 1;
			mmTextBox4.IsComboTextBox = false;
			mmTextBox4.IsModified = false;
			mmTextBox4.Location = new System.Drawing.Point(533, 192);
			mmTextBox4.MaxLength = 30;
			mmTextBox4.Name = "mmTextBox4";
			mmTextBox4.ReadOnly = true;
			mmTextBox4.Size = new System.Drawing.Size(68, 20);
			mmTextBox4.TabIndex = 24;
			mmTextBox4.TabStop = false;
			mmLabel35.AutoSize = true;
			mmLabel35.BackColor = System.Drawing.Color.Transparent;
			mmLabel35.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel35.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel35.IsFieldHeader = false;
			mmLabel35.IsRequired = false;
			mmLabel35.Location = new System.Drawing.Point(282, 194);
			mmLabel35.Name = "mmLabel35";
			mmLabel35.PenWidth = 1f;
			mmLabel35.ShowBorder = false;
			mmLabel35.Size = new System.Drawing.Size(61, 13);
			mmLabel35.TabIndex = 30;
			mmLabel35.Text = "Birth Place:";
			mmLabel36.AutoSize = true;
			mmLabel36.BackColor = System.Drawing.Color.Transparent;
			mmLabel36.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel36.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel36.IsFieldHeader = false;
			mmLabel36.IsRequired = false;
			mmLabel36.Location = new System.Drawing.Point(9, 191);
			mmLabel36.Name = "mmLabel36";
			mmLabel36.PenWidth = 1f;
			mmLabel36.ShowBorder = false;
			mmLabel36.Size = new System.Drawing.Size(59, 13);
			mmLabel36.TabIndex = 29;
			mmLabel36.Text = "Birth Date:";
			ultraFormattedLinkLabel10.AutoSize = true;
			ultraFormattedLinkLabel10.Location = new System.Drawing.Point(9, 171);
			ultraFormattedLinkLabel10.Name = "ultraFormattedLinkLabel10";
			ultraFormattedLinkLabel10.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel10.TabIndex = 59;
			ultraFormattedLinkLabel10.TabStop = true;
			ultraFormattedLinkLabel10.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel10.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel10.Value = "Nationality:";
			appearance101.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.VisitedLinkAppearance = appearance101;
			ultraFormattedLinkLabel10.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			maritalStatusComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			maritalStatusComboBox3.FormattingEnabled = true;
			maritalStatusComboBox3.Location = new System.Drawing.Point(132, 241);
			maritalStatusComboBox3.Name = "maritalStatusComboBox3";
			maritalStatusComboBox3.SelectedID = 0;
			maritalStatusComboBox3.Size = new System.Drawing.Size(130, 21);
			maritalStatusComboBox3.TabIndex = 25;
			mmLabel37.AutoSize = true;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(506, 194);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(30, 13);
			mmLabel37.TabIndex = 57;
			mmLabel37.Text = "Age:";
			mmsDateTimePicker1.Checked = false;
			mmsDateTimePicker1.CustomFormat = " ";
			mmsDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			mmsDateTimePicker1.Location = new System.Drawing.Point(132, 188);
			mmsDateTimePicker1.Name = "mmsDateTimePicker1";
			mmsDateTimePicker1.ShowCheckBox = true;
			mmsDateTimePicker1.Size = new System.Drawing.Size(149, 20);
			mmsDateTimePicker1.TabIndex = 23;
			mmsDateTimePicker1.Value = new System.DateTime(0L);
			mmsDateTimePicker1.ValueChanged += new System.EventHandler(dateTimePickerBirthDate_ValueChanged);
			nationalityComboBox3.Assigned = false;
			nationalityComboBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			nationalityComboBox3.CustomReportFieldName = "";
			nationalityComboBox3.CustomReportKey = "";
			nationalityComboBox3.CustomReportValueType = 1;
			nationalityComboBox3.DescriptionTextBox = null;
			appearance102.BackColor = System.Drawing.SystemColors.Window;
			appearance102.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			nationalityComboBox3.DisplayLayout.Appearance = appearance102;
			nationalityComboBox3.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			nationalityComboBox3.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance103.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance103.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance103.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance103.BorderColor = System.Drawing.SystemColors.Window;
			nationalityComboBox3.DisplayLayout.GroupByBox.Appearance = appearance103;
			appearance104.ForeColor = System.Drawing.SystemColors.GrayText;
			nationalityComboBox3.DisplayLayout.GroupByBox.BandLabelAppearance = appearance104;
			nationalityComboBox3.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance105.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance105.BackColor2 = System.Drawing.SystemColors.Control;
			appearance105.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance105.ForeColor = System.Drawing.SystemColors.GrayText;
			nationalityComboBox3.DisplayLayout.GroupByBox.PromptAppearance = appearance105;
			nationalityComboBox3.DisplayLayout.MaxColScrollRegions = 1;
			nationalityComboBox3.DisplayLayout.MaxRowScrollRegions = 1;
			appearance106.BackColor = System.Drawing.SystemColors.Window;
			appearance106.ForeColor = System.Drawing.SystemColors.ControlText;
			nationalityComboBox3.DisplayLayout.Override.ActiveCellAppearance = appearance106;
			appearance107.BackColor = System.Drawing.SystemColors.Highlight;
			appearance107.ForeColor = System.Drawing.SystemColors.HighlightText;
			nationalityComboBox3.DisplayLayout.Override.ActiveRowAppearance = appearance107;
			nationalityComboBox3.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			nationalityComboBox3.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance108.BackColor = System.Drawing.SystemColors.Window;
			nationalityComboBox3.DisplayLayout.Override.CardAreaAppearance = appearance108;
			appearance109.BorderColor = System.Drawing.Color.Silver;
			appearance109.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			nationalityComboBox3.DisplayLayout.Override.CellAppearance = appearance109;
			nationalityComboBox3.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			nationalityComboBox3.DisplayLayout.Override.CellPadding = 0;
			appearance110.BackColor = System.Drawing.SystemColors.Control;
			appearance110.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance110.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance110.BorderColor = System.Drawing.SystemColors.Window;
			nationalityComboBox3.DisplayLayout.Override.GroupByRowAppearance = appearance110;
			appearance111.TextHAlignAsString = "Left";
			nationalityComboBox3.DisplayLayout.Override.HeaderAppearance = appearance111;
			nationalityComboBox3.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			nationalityComboBox3.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance112.BackColor = System.Drawing.SystemColors.Window;
			appearance112.BorderColor = System.Drawing.Color.Silver;
			nationalityComboBox3.DisplayLayout.Override.RowAppearance = appearance112;
			nationalityComboBox3.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance113.BackColor = System.Drawing.SystemColors.ControlLight;
			nationalityComboBox3.DisplayLayout.Override.TemplateAddRowAppearance = appearance113;
			nationalityComboBox3.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			nationalityComboBox3.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			nationalityComboBox3.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			nationalityComboBox3.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			nationalityComboBox3.Editable = true;
			nationalityComboBox3.FilterString = "";
			nationalityComboBox3.HasAllAccount = false;
			nationalityComboBox3.HasCustom = false;
			nationalityComboBox3.IsDataLoaded = false;
			nationalityComboBox3.Location = new System.Drawing.Point(132, 165);
			nationalityComboBox3.MaxDropDownItems = 12;
			nationalityComboBox3.Name = "nationalityComboBox3";
			nationalityComboBox3.ShowInactiveItems = false;
			nationalityComboBox3.ShowQuickAdd = true;
			nationalityComboBox3.Size = new System.Drawing.Size(151, 20);
			nationalityComboBox3.TabIndex = 20;
			nationalityComboBox3.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(11, 217);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(46, 13);
			mmLabel38.TabIndex = 0;
			mmLabel38.Text = "Gender:";
			ultraFormattedLinkLabel11.AutoSize = true;
			ultraFormattedLinkLabel11.Location = new System.Drawing.Point(9, 142);
			ultraFormattedLinkLabel11.Name = "ultraFormattedLinkLabel11";
			ultraFormattedLinkLabel11.Size = new System.Drawing.Size(49, 14);
			ultraFormattedLinkLabel11.TabIndex = 56;
			ultraFormattedLinkLabel11.TabStop = true;
			ultraFormattedLinkLabel11.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel11.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel11.Value = "Location:";
			appearance114.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel11.VisitedLinkAppearance = appearance114;
			ultraFormattedLinkLabel11.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCountry_LinkClicked);
			genderComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			genderComboBox1.FormattingEnabled = true;
			genderComboBox1.Location = new System.Drawing.Point(132, 214);
			genderComboBox1.Name = "genderComboBox1";
			genderComboBox1.SelectedID = 0;
			genderComboBox1.Size = new System.Drawing.Size(130, 21);
			genderComboBox1.TabIndex = 22;
			mmTextBox5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			mmTextBox5.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			mmTextBox5.BackColor = System.Drawing.Color.White;
			mmTextBox5.CustomReportFieldName = "";
			mmTextBox5.CustomReportKey = "";
			mmTextBox5.CustomReportValueType = 1;
			mmTextBox5.IsComboTextBox = false;
			mmTextBox5.IsModified = false;
			mmTextBox5.Location = new System.Drawing.Point(132, 119);
			mmTextBox5.MaxLength = 30;
			mmTextBox5.Name = "mmTextBox5";
			mmTextBox5.Size = new System.Drawing.Size(151, 20);
			mmTextBox5.TabIndex = 8;
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(9, 121);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(64, 13);
			mmLabel39.TabIndex = 53;
			mmLabel39.Text = "National ID:";
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
			mmTextBox6.BackColor = System.Drawing.Color.White;
			mmTextBox6.CustomReportFieldName = "";
			mmTextBox6.CustomReportKey = "";
			mmTextBox6.CustomReportValueType = 1;
			mmTextBox6.IsComboTextBox = false;
			mmTextBox6.IsModified = false;
			mmTextBox6.Location = new System.Drawing.Point(132, 97);
			mmTextBox6.MaxLength = 30;
			mmTextBox6.Name = "mmTextBox6";
			mmTextBox6.Size = new System.Drawing.Size(385, 20);
			mmTextBox6.TabIndex = 7;
			mmTextBox7.BackColor = System.Drawing.Color.White;
			mmTextBox7.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			mmTextBox7.CustomReportFieldName = "";
			mmTextBox7.CustomReportKey = "";
			mmTextBox7.CustomReportValueType = 1;
			mmTextBox7.IsComboTextBox = false;
			mmTextBox7.IsModified = false;
			mmTextBox7.Location = new System.Drawing.Point(132, 9);
			mmTextBox7.MaxLength = 64;
			mmTextBox7.Name = "mmTextBox7";
			mmTextBox7.Size = new System.Drawing.Size(161, 20);
			mmTextBox7.TabIndex = 0;
			mmTextBox8.BackColor = System.Drawing.Color.White;
			mmTextBox8.CustomReportFieldName = "";
			mmTextBox8.CustomReportKey = "";
			mmTextBox8.CustomReportValueType = 1;
			mmTextBox8.IsComboTextBox = false;
			mmTextBox8.IsModified = false;
			mmTextBox8.IsRequired = true;
			mmTextBox8.Location = new System.Drawing.Point(132, 53);
			mmTextBox8.MaxLength = 30;
			mmTextBox8.Name = "mmTextBox8";
			mmTextBox8.Size = new System.Drawing.Size(385, 20);
			mmTextBox8.TabIndex = 5;
			mmTextBox9.BackColor = System.Drawing.Color.White;
			mmTextBox9.CustomReportFieldName = "";
			mmTextBox9.CustomReportKey = "";
			mmTextBox9.CustomReportValueType = 1;
			mmTextBox9.IsComboTextBox = false;
			mmTextBox9.IsModified = false;
			mmTextBox9.IsRequired = true;
			mmTextBox9.Location = new System.Drawing.Point(132, 31);
			mmTextBox9.MaxLength = 30;
			mmTextBox9.Name = "mmTextBox9";
			mmTextBox9.Size = new System.Drawing.Size(385, 20);
			mmTextBox9.TabIndex = 4;
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
			mmTextBox10.BackColor = System.Drawing.Color.White;
			mmTextBox10.CustomReportFieldName = "";
			mmTextBox10.CustomReportKey = "";
			mmTextBox10.CustomReportValueType = 1;
			mmTextBox10.IsComboTextBox = false;
			mmTextBox10.IsModified = false;
			mmTextBox10.Location = new System.Drawing.Point(132, 75);
			mmTextBox10.MaxLength = 30;
			mmTextBox10.Name = "mmTextBox10";
			mmTextBox10.Size = new System.Drawing.Size(385, 20);
			mmTextBox10.TabIndex = 6;
			ultraTabPageControl4.Controls.Add(mmLabel44);
			ultraTabPageControl4.Controls.Add(dataEntryGrid1);
			ultraTabPageControl4.Controls.Add(checkedListBox4);
			ultraTabPageControl4.Controls.Add(ultraFormattedLinkLabel4);
			ultraTabPageControl4.Controls.Add(ultraFormattedLinkLabel14);
			ultraTabPageControl4.Controls.Add(checkedListBox6);
			ultraTabPageControl4.Controls.Add(ultraGroupBox2);
			ultraTabPageControl4.Location = new System.Drawing.Point(2, 21);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(696, 420);
			mmLabel44.AutoSize = true;
			mmLabel44.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel44.IsFieldHeader = false;
			mmLabel44.IsRequired = false;
			mmLabel44.Location = new System.Drawing.Point(7, 248);
			mmLabel44.Name = "mmLabel44";
			mmLabel44.PenWidth = 1f;
			mmLabel44.ShowBorder = false;
			mmLabel44.Size = new System.Drawing.Size(73, 13);
			mmLabel44.TabIndex = 73;
			mmLabel44.Text = "Relationships:";
			dataEntryGrid1.AllowAddNew = false;
			dataEntryGrid1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGrid1.DisplayLayout.Appearance = appearance115;
			dataEntryGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataEntryGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance116.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance116.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance116.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance116.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGrid1.DisplayLayout.GroupByBox.Appearance = appearance116;
			appearance117.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance117;
			dataEntryGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance118.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance118.BackColor2 = System.Drawing.SystemColors.Control;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance118.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGrid1.DisplayLayout.GroupByBox.PromptAppearance = appearance118;
			dataEntryGrid1.DisplayLayout.MaxColScrollRegions = 1;
			dataEntryGrid1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.ForeColor = System.Drawing.SystemColors.ControlText;
			dataEntryGrid1.DisplayLayout.Override.ActiveCellAppearance = appearance119;
			appearance120.BackColor = System.Drawing.SystemColors.Highlight;
			appearance120.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataEntryGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance120;
			dataEntryGrid1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataEntryGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataEntryGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			dataEntryGrid1.DisplayLayout.Override.CardAreaAppearance = appearance121;
			appearance122.BorderColor = System.Drawing.Color.Silver;
			appearance122.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataEntryGrid1.DisplayLayout.Override.CellAppearance = appearance122;
			dataEntryGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataEntryGrid1.DisplayLayout.Override.CellPadding = 0;
			appearance123.BackColor = System.Drawing.SystemColors.Control;
			appearance123.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance123.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance123.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance123.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGrid1.DisplayLayout.Override.GroupByRowAppearance = appearance123;
			appearance124.TextHAlignAsString = "Left";
			dataEntryGrid1.DisplayLayout.Override.HeaderAppearance = appearance124;
			dataEntryGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataEntryGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.BorderColor = System.Drawing.Color.Silver;
			dataEntryGrid1.DisplayLayout.Override.RowAppearance = appearance125;
			dataEntryGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance126.BackColor = System.Drawing.SystemColors.ControlLight;
			dataEntryGrid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance126;
			dataEntryGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGrid1.IncludeLotItems = false;
			dataEntryGrid1.LoadLayoutFailed = false;
			dataEntryGrid1.Location = new System.Drawing.Point(3, 264);
			dataEntryGrid1.Name = "dataEntryGrid1";
			dataEntryGrid1.ShowClearMenu = true;
			dataEntryGrid1.ShowDeleteMenu = true;
			dataEntryGrid1.ShowInsertMenu = true;
			dataEntryGrid1.ShowMoveRowsMenu = true;
			dataEntryGrid1.Size = new System.Drawing.Size(693, 152);
			dataEntryGrid1.TabIndex = 72;
			ultraFormattedLinkLabel14.AutoSize = true;
			ultraFormattedLinkLabel14.Location = new System.Drawing.Point(10, 160);
			ultraFormattedLinkLabel14.Name = "ultraFormattedLinkLabel14";
			ultraFormattedLinkLabel14.Size = new System.Drawing.Size(51, 14);
			ultraFormattedLinkLabel14.TabIndex = 69;
			ultraFormattedLinkLabel14.TabStop = true;
			ultraFormattedLinkLabel14.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel14.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel14.Value = "Chronics:";
			appearance127.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel14.VisitedLinkAppearance = appearance127;
			checkedListBox6.FormattingEnabled = true;
			checkedListBox6.Location = new System.Drawing.Point(140, 160);
			checkedListBox6.Name = "checkedListBox6";
			checkedListBox6.Size = new System.Drawing.Size(182, 79);
			checkedListBox6.TabIndex = 13;
			ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox2.Controls.Add(amountTextBox1);
			ultraGroupBox2.Controls.Add(mmLabel45);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel15);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel16);
			ultraGroupBox2.Controls.Add(numberTextBox1);
			ultraGroupBox2.Controls.Add(mmLabel46);
			ultraGroupBox2.Controls.Add(mmTextBox11);
			ultraGroupBox2.Controls.Add(mmLabel47);
			ultraGroupBox2.Controls.Add(mmsDateTimePicker2);
			ultraGroupBox2.Controls.Add(mmLabel48);
			ultraGroupBox2.Controls.Add(mmsDateTimePicker3);
			ultraGroupBox2.Controls.Add(mmLabel50);
			ultraGroupBox2.Controls.Add(genericListComboBox1);
			ultraGroupBox2.Controls.Add(mmTextBox12);
			ultraGroupBox2.Controls.Add(mmTextBox13);
			ultraGroupBox2.Controls.Add(medicalInsuranceProviderComboBox1);
			ultraGroupBox2.Location = new System.Drawing.Point(3, 12);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(687, 142);
			ultraGroupBox2.TabIndex = 12;
			ultraGroupBox2.Text = "Medical Insurance Details";
			amountTextBox1.AllowDecimal = true;
			amountTextBox1.CustomReportFieldName = "";
			amountTextBox1.CustomReportKey = "";
			amountTextBox1.CustomReportValueType = 1;
			amountTextBox1.IsComboTextBox = false;
			amountTextBox1.IsModified = false;
			amountTextBox1.Location = new System.Drawing.Point(137, 118);
			amountTextBox1.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			amountTextBox1.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			amountTextBox1.Name = "amountTextBox1";
			amountTextBox1.NullText = "0";
			amountTextBox1.Size = new System.Drawing.Size(141, 20);
			amountTextBox1.TabIndex = 8;
			amountTextBox1.Text = "0.00";
			amountTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			amountTextBox1.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel45.AutoSize = true;
			mmLabel45.BackColor = System.Drawing.Color.Transparent;
			mmLabel45.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel45.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel45.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel45.IsFieldHeader = false;
			mmLabel45.IsRequired = false;
			mmLabel45.Location = new System.Drawing.Point(7, 120);
			mmLabel45.Name = "mmLabel45";
			mmLabel45.PenWidth = 1f;
			mmLabel45.ShowBorder = false;
			mmLabel45.Size = new System.Drawing.Size(99, 13);
			mmLabel45.TabIndex = 83;
			mmLabel45.Text = "Insurance Amount:";
			ultraFormattedLinkLabel15.AutoSize = true;
			ultraFormattedLinkLabel15.Location = new System.Drawing.Point(7, 52);
			ultraFormattedLinkLabel15.Name = "ultraFormattedLinkLabel15";
			ultraFormattedLinkLabel15.Size = new System.Drawing.Size(103, 14);
			ultraFormattedLinkLabel15.TabIndex = 81;
			ultraFormattedLinkLabel15.TabStop = true;
			ultraFormattedLinkLabel15.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel15.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel15.Value = "Insurance Category:";
			appearance128.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel15.VisitedLinkAppearance = appearance128;
			ultraFormattedLinkLabel15.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel13_LinkClicked);
			ultraFormattedLinkLabel16.AutoSize = true;
			ultraFormattedLinkLabel16.Location = new System.Drawing.Point(7, 29);
			ultraFormattedLinkLabel16.Name = "ultraFormattedLinkLabel16";
			ultraFormattedLinkLabel16.Size = new System.Drawing.Size(100, 14);
			ultraFormattedLinkLabel16.TabIndex = 68;
			ultraFormattedLinkLabel16.TabStop = true;
			ultraFormattedLinkLabel16.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel16.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel16.Value = "Insurance Provider:";
			appearance129.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel16.VisitedLinkAppearance = appearance129;
			ultraFormattedLinkLabel16.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel12_LinkClicked);
			numberTextBox1.AllowDecimal = false;
			numberTextBox1.BackColor = System.Drawing.Color.White;
			numberTextBox1.CustomReportFieldName = "";
			numberTextBox1.CustomReportKey = "";
			numberTextBox1.CustomReportValueType = 1;
			numberTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			numberTextBox1.IsComboTextBox = false;
			numberTextBox1.IsModified = false;
			numberTextBox1.Location = new System.Drawing.Point(351, 95);
			numberTextBox1.MaxLength = 2;
			numberTextBox1.MaxValue = new decimal(new int[4]
			{
				10,
				0,
				0,
				0
			});
			numberTextBox1.MinValue = new decimal(new int[4]);
			numberTextBox1.Name = "numberTextBox1";
			numberTextBox1.NullText = "0";
			numberTextBox1.Size = new System.Drawing.Size(56, 20);
			numberTextBox1.TabIndex = 7;
			numberTextBox1.Text = "0";
			numberTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel46.AutoSize = true;
			mmLabel46.BackColor = System.Drawing.Color.Transparent;
			mmLabel46.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel46.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel46.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel46.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel46.IsFieldHeader = false;
			mmLabel46.IsRequired = false;
			mmLabel46.Location = new System.Drawing.Point(7, 99);
			mmLabel46.Name = "mmLabel46";
			mmLabel46.PenWidth = 1f;
			mmLabel46.ShowBorder = false;
			mmLabel46.Size = new System.Drawing.Size(99, 13);
			mmLabel46.TabIndex = 80;
			mmLabel46.Text = "Insurance Number:";
			mmTextBox11.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			mmTextBox11.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			mmTextBox11.BackColor = System.Drawing.Color.White;
			mmTextBox11.CustomReportFieldName = "";
			mmTextBox11.CustomReportKey = "";
			mmTextBox11.CustomReportValueType = 1;
			mmTextBox11.IsComboTextBox = false;
			mmTextBox11.IsModified = false;
			mmTextBox11.Location = new System.Drawing.Point(137, 95);
			mmTextBox11.MaxLength = 50;
			mmTextBox11.Name = "mmTextBox11";
			mmTextBox11.Size = new System.Drawing.Size(120, 20);
			mmTextBox11.TabIndex = 6;
			mmLabel47.AutoSize = true;
			mmLabel47.BackColor = System.Drawing.Color.Transparent;
			mmLabel47.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel47.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel47.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel47.IsFieldHeader = false;
			mmLabel47.IsRequired = false;
			mmLabel47.Location = new System.Drawing.Point(263, 99);
			mmLabel47.Name = "mmLabel47";
			mmLabel47.PenWidth = 1f;
			mmLabel47.ShowBorder = false;
			mmLabel47.Size = new System.Drawing.Size(87, 13);
			mmLabel47.TabIndex = 78;
			mmLabel47.Text = "Family Members:";
			mmsDateTimePicker2.Checked = false;
			mmsDateTimePicker2.CustomFormat = " ";
			mmsDateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			mmsDateTimePicker2.Location = new System.Drawing.Point(351, 72);
			mmsDateTimePicker2.Name = "mmsDateTimePicker2";
			mmsDateTimePicker2.ShowCheckBox = true;
			mmsDateTimePicker2.Size = new System.Drawing.Size(141, 20);
			mmsDateTimePicker2.TabIndex = 5;
			mmsDateTimePicker2.Value = new System.DateTime(0L);
			mmLabel48.AutoSize = true;
			mmLabel48.BackColor = System.Drawing.Color.Transparent;
			mmLabel48.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel48.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel48.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel48.IsFieldHeader = false;
			mmLabel48.IsRequired = false;
			mmLabel48.Location = new System.Drawing.Point(288, 77);
			mmLabel48.Name = "mmLabel48";
			mmLabel48.PenWidth = 1f;
			mmLabel48.ShowBorder = false;
			mmLabel48.Size = new System.Drawing.Size(57, 13);
			mmLabel48.TabIndex = 76;
			mmLabel48.Text = "Valid Until:";
			mmsDateTimePicker3.Checked = false;
			mmsDateTimePicker3.CustomFormat = " ";
			mmsDateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			mmsDateTimePicker3.Location = new System.Drawing.Point(137, 72);
			mmsDateTimePicker3.Name = "mmsDateTimePicker3";
			mmsDateTimePicker3.ShowCheckBox = true;
			mmsDateTimePicker3.Size = new System.Drawing.Size(141, 20);
			mmsDateTimePicker3.TabIndex = 4;
			mmsDateTimePicker3.Value = new System.DateTime(0L);
			mmLabel50.AutoSize = true;
			mmLabel50.BackColor = System.Drawing.Color.Transparent;
			mmLabel50.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel50.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel50.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel50.IsFieldHeader = false;
			mmLabel50.IsRequired = false;
			mmLabel50.Location = new System.Drawing.Point(7, 77);
			mmLabel50.Name = "mmLabel50";
			mmLabel50.PenWidth = 1f;
			mmLabel50.ShowBorder = false;
			mmLabel50.Size = new System.Drawing.Size(60, 13);
			mmLabel50.TabIndex = 73;
			mmLabel50.Text = "Valid From:";
			genericListComboBox1.Assigned = false;
			genericListComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			genericListComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			genericListComboBox1.CustomReportFieldName = "";
			genericListComboBox1.CustomReportKey = "";
			genericListComboBox1.CustomReportValueType = 1;
			genericListComboBox1.DescriptionTextBox = mmTextBox12;
			appearance130.BackColor = System.Drawing.SystemColors.Window;
			appearance130.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			genericListComboBox1.DisplayLayout.Appearance = appearance130;
			genericListComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			genericListComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance131.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance131.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance131.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBox1.DisplayLayout.GroupByBox.Appearance = appearance131;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance132;
			genericListComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance133.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance133.BackColor2 = System.Drawing.SystemColors.Control;
			appearance133.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance133.ForeColor = System.Drawing.SystemColors.GrayText;
			genericListComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance133;
			genericListComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			genericListComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance134.BackColor = System.Drawing.SystemColors.Window;
			appearance134.ForeColor = System.Drawing.SystemColors.ControlText;
			genericListComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance134;
			appearance135.BackColor = System.Drawing.SystemColors.Highlight;
			appearance135.ForeColor = System.Drawing.SystemColors.HighlightText;
			genericListComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance135;
			genericListComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			genericListComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance136.BackColor = System.Drawing.SystemColors.Window;
			genericListComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance136;
			appearance137.BorderColor = System.Drawing.Color.Silver;
			appearance137.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			genericListComboBox1.DisplayLayout.Override.CellAppearance = appearance137;
			genericListComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			genericListComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance138.BackColor = System.Drawing.SystemColors.Control;
			appearance138.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance138.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance138.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance138.BorderColor = System.Drawing.SystemColors.Window;
			genericListComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance138;
			appearance139.TextHAlignAsString = "Left";
			genericListComboBox1.DisplayLayout.Override.HeaderAppearance = appearance139;
			genericListComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			genericListComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance140.BackColor = System.Drawing.SystemColors.Window;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			genericListComboBox1.DisplayLayout.Override.RowAppearance = appearance140;
			genericListComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance141.BackColor = System.Drawing.SystemColors.ControlLight;
			genericListComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance141;
			genericListComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			genericListComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			genericListComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			genericListComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			genericListComboBox1.Editable = true;
			genericListComboBox1.FilterString = "";
			genericListComboBox1.GenericListType = Micromind.Common.Data.GenericListTypes.MedicalInsuranceCategory;
			genericListComboBox1.HasAllAccount = false;
			genericListComboBox1.HasCustom = false;
			genericListComboBox1.IsDataLoaded = false;
			genericListComboBox1.IsSingleColumn = false;
			genericListComboBox1.Location = new System.Drawing.Point(137, 49);
			genericListComboBox1.MaxDropDownItems = 12;
			genericListComboBox1.Name = "genericListComboBox1";
			genericListComboBox1.ShowInactiveItems = false;
			genericListComboBox1.ShowQuickAdd = true;
			genericListComboBox1.Size = new System.Drawing.Size(141, 20);
			genericListComboBox1.TabIndex = 2;
			genericListComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmTextBox12.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBox12.CustomReportFieldName = "";
			mmTextBox12.CustomReportKey = "";
			mmTextBox12.CustomReportValueType = 1;
			mmTextBox12.Enabled = false;
			mmTextBox12.ForeColor = System.Drawing.Color.Black;
			mmTextBox12.IsComboTextBox = false;
			mmTextBox12.IsModified = false;
			mmTextBox12.Location = new System.Drawing.Point(279, 49);
			mmTextBox12.MaxLength = 15;
			mmTextBox12.Name = "mmTextBox12";
			mmTextBox12.Size = new System.Drawing.Size(283, 20);
			mmTextBox12.TabIndex = 3;
			mmTextBox12.TabStop = false;
			mmTextBox13.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBox13.CustomReportFieldName = "";
			mmTextBox13.CustomReportKey = "";
			mmTextBox13.CustomReportValueType = 1;
			mmTextBox13.Enabled = false;
			mmTextBox13.ForeColor = System.Drawing.Color.Black;
			mmTextBox13.IsComboTextBox = false;
			mmTextBox13.IsModified = false;
			mmTextBox13.Location = new System.Drawing.Point(279, 26);
			mmTextBox13.MaxLength = 15;
			mmTextBox13.Name = "mmTextBox13";
			mmTextBox13.Size = new System.Drawing.Size(283, 20);
			mmTextBox13.TabIndex = 1;
			mmTextBox13.TabStop = false;
			medicalInsuranceProviderComboBox1.Assigned = false;
			medicalInsuranceProviderComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			medicalInsuranceProviderComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			medicalInsuranceProviderComboBox1.CustomReportFieldName = "";
			medicalInsuranceProviderComboBox1.CustomReportKey = "";
			medicalInsuranceProviderComboBox1.CustomReportValueType = 1;
			medicalInsuranceProviderComboBox1.DescriptionTextBox = mmTextBox13;
			appearance142.BackColor = System.Drawing.SystemColors.Window;
			appearance142.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			medicalInsuranceProviderComboBox1.DisplayLayout.Appearance = appearance142;
			medicalInsuranceProviderComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			medicalInsuranceProviderComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance143.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance143.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance143.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance143.BorderColor = System.Drawing.SystemColors.Window;
			medicalInsuranceProviderComboBox1.DisplayLayout.GroupByBox.Appearance = appearance143;
			appearance144.ForeColor = System.Drawing.SystemColors.GrayText;
			medicalInsuranceProviderComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance144;
			medicalInsuranceProviderComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance145.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance145.BackColor2 = System.Drawing.SystemColors.Control;
			appearance145.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance145.ForeColor = System.Drawing.SystemColors.GrayText;
			medicalInsuranceProviderComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance145;
			medicalInsuranceProviderComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			medicalInsuranceProviderComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance146.BackColor = System.Drawing.SystemColors.Window;
			appearance146.ForeColor = System.Drawing.SystemColors.ControlText;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance146;
			appearance147.BackColor = System.Drawing.SystemColors.Highlight;
			appearance147.ForeColor = System.Drawing.SystemColors.HighlightText;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance147;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance148.BackColor = System.Drawing.SystemColors.Window;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance148;
			appearance149.BorderColor = System.Drawing.Color.Silver;
			appearance149.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.CellAppearance = appearance149;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance150.BackColor = System.Drawing.SystemColors.Control;
			appearance150.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance150.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance150.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance150.BorderColor = System.Drawing.SystemColors.Window;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance150;
			appearance151.TextHAlignAsString = "Left";
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.HeaderAppearance = appearance151;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance152.BackColor = System.Drawing.SystemColors.Window;
			appearance152.BorderColor = System.Drawing.Color.Silver;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.RowAppearance = appearance152;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance153.BackColor = System.Drawing.SystemColors.ControlLight;
			medicalInsuranceProviderComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance153;
			medicalInsuranceProviderComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			medicalInsuranceProviderComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			medicalInsuranceProviderComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			medicalInsuranceProviderComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			medicalInsuranceProviderComboBox1.Editable = true;
			medicalInsuranceProviderComboBox1.FilterString = "";
			medicalInsuranceProviderComboBox1.HasAllAccount = false;
			medicalInsuranceProviderComboBox1.HasCustom = false;
			medicalInsuranceProviderComboBox1.IsDataLoaded = false;
			medicalInsuranceProviderComboBox1.Location = new System.Drawing.Point(137, 26);
			medicalInsuranceProviderComboBox1.MaxDropDownItems = 12;
			medicalInsuranceProviderComboBox1.Name = "medicalInsuranceProviderComboBox1";
			medicalInsuranceProviderComboBox1.ShowInactiveItems = false;
			medicalInsuranceProviderComboBox1.ShowQuickAdd = true;
			medicalInsuranceProviderComboBox1.Size = new System.Drawing.Size(141, 20);
			medicalInsuranceProviderComboBox1.TabIndex = 0;
			medicalInsuranceProviderComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraTabPageControl5.Controls.Add(udfEntryControl1);
			ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl5.Name = "ultraTabPageControl5";
			ultraTabPageControl5.Size = new System.Drawing.Size(696, 420);
			udfEntryControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryControl1.Location = new System.Drawing.Point(7, 3);
			udfEntryControl1.Margin = new System.Windows.Forms.Padding(4);
			udfEntryControl1.Name = "udfEntryControl1";
			udfEntryControl1.Size = new System.Drawing.Size(683, 413);
			udfEntryControl1.TabIndex = 1;
			udfEntryControl1.Load += new System.EventHandler(udfEntryGrid_Load);
			ultraTabPageControl6.Controls.Add(mmTextBox14);
			ultraTabPageControl6.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl6.Name = "ultraTabPageControl6";
			ultraTabPageControl6.Size = new System.Drawing.Size(696, 420);
			mmTextBox14.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			mmTextBox14.BackColor = System.Drawing.Color.White;
			mmTextBox14.CustomReportFieldName = "";
			mmTextBox14.CustomReportKey = "";
			mmTextBox14.CustomReportValueType = 1;
			mmTextBox14.IsComboTextBox = false;
			mmTextBox14.IsModified = false;
			mmTextBox14.Location = new System.Drawing.Point(10, 14);
			mmTextBox14.MaxLength = 5000;
			mmTextBox14.Multiline = true;
			mmTextBox14.Name = "mmTextBox14";
			mmTextBox14.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			mmTextBox14.Size = new System.Drawing.Size(674, 392);
			mmTextBox14.TabIndex = 21;
			ultraTabControl2.Controls.Add(ultraTabPageControl3);
			ultraTabControl2.Controls.Add(ultraTabPageControl4);
			ultraTabControl2.Controls.Add(ultraTabPageControl5);
			ultraTabControl2.Controls.Add(ultraTabPageControl6);
			ultraTabControl2.Location = new System.Drawing.Point(0, 0);
			ultraTabControl2.Name = "ultraTabControl2";
			ultraTabControl2.SharedControlsPage = ultraTabSharedControlsPage2;
			ultraTabControl2.Size = new System.Drawing.Size(200, 100);
			ultraTabControl2.TabIndex = 0;
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
			base.Name = "PatientForm";
			Text = "Patient Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(EmployeeClassDetailsForm_FormClosing);
			base.Load += new System.EventHandler(EmployeeDetailsForm_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomerClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReligion).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNationality).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxNoImage).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).EndInit();
			ultraGroupBox5.ResumeLayout(false);
			ultraGroupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceProvider).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxInsuranceRating).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridRelation).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)nationalityComboBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)workLocationComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)nationalityComboBox3).EndInit();
			ultraTabPageControl4.ResumeLayout(false);
			ultraTabPageControl4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataEntryGrid1).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)genericListComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)medicalInsuranceProviderComboBox1).EndInit();
			ultraTabPageControl5.ResumeLayout(false);
			ultraTabPageControl6.ResumeLayout(false);
			ultraTabPageControl6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl2).EndInit();
			ultraTabControl2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			AddEvents();
			comboBoxGender.LoadData();
			comboBoxMaritalStatus.LoadData();
			companyInformation = Factory.CompanyInformationSystem.GetCompanyInformation();
			dataGridRelation.AfterRowActivate += dataGridItems_AfterRowActivate;
		}

		private void AddEvents()
		{
			FormActivator.EmployeeAddressDetailsFormObj.EmployeeAddressChanged += EventHelper_EmployeeAddressChanged;
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxLastName.TextChanged += textBoxLastName_TextChanged;
			textBoxFirstName.TextChanged += textBoxName_TextChanged;
			textBoxCode.TextChanged += textBoxCode_TextChanged;
			dataGridRelation.GotFocus += dataGridItems_GotFocus;
			dataGridRelation.AfterRowActivate += dataGridItems_AfterRowActivate;
		}

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			checked
			{
				if (dataGridRelation.ActiveRow != null && (dataGridRelation.ActiveRow.Cells["SlNo"].Value == null || dataGridRelation.ActiveRow.Cells["SlNo"].Value.ToString() == ""))
				{
					if (dataGridRelation.ActiveRow.Index > 0)
					{
						int.TryParse(dataGridRelation.Rows[dataGridRelation.ActiveRow.Index - 1].Cells["SlNo"].Text.ToString(), out slNo);
						dataGridRelation.ActiveRow.Cells["SlNo"].Value = slNo + 1;
					}
					else if ((dataGridRelation.ActiveRow.Index == 0 && dataGridRelation.ActiveRow.Cells["SlNo"].Value == null) || dataGridRelation.ActiveRow.Cells["SlNo"].Value.ToString() == "")
					{
						dataGridRelation.Rows[0].Cells["SlNo"].Value = slNo;
					}
				}
			}
		}

		private void dataGridItems_GotFocus(object sender, EventArgs e)
		{
			if (!dataGridRelation.Focused)
			{
				dataGridRelation.DisplayLayout.Bands[0].AddNew();
			}
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
				SetupGrid();
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

		private void SetupGrid()
		{
			try
			{
				dataGridRelation.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("SlNo");
				dataTable.Columns.Add("Name");
				dataTable.Columns.Add("Age");
				dataTable.Columns.Add("Relation");
				dataTable.Columns.Add("Remarks");
				dataGridRelation.DataSource = dataTable;
				dataGridRelation.DisplayLayout.Bands[0].Columns["SlNo"].Width = 50;
				dataGridRelation.DisplayLayout.Bands[0].Columns["Remarks"].Width = checked(40 * dataGridRelation.Width) / 100;
				dataGridRelation.DisplayLayout.Bands[0].Columns["Age"].MaxLength = 20;
				dataGridRelation.DisplayLayout.Bands[0].Columns["Age"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridRelation.DisplayLayout.Bands[0].Columns["Age"].Format = "0";
				dataGridRelation.DisplayLayout.Bands[0].Columns["Age"].MinValue = 0;
				dataGridRelation.DisplayLayout.Bands[0].Columns["Age"].MaxValue = 99999999m;
				dataGridRelation.DisplayLayout.Bands[0].Columns["SlNo"].CellActivation = Activation.NoEdit;
				dataGridRelation.DisplayLayout.Bands[0].Columns["SlNo"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridRelation.DisplayLayout.Bands[0].Columns["SlNo"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(1, "Father");
				valueList.ValueListItems.Add(2, "Mother");
				valueList.ValueListItems.Add(3, "Brother");
				valueList.ValueListItems.Add(4, "Sister");
				valueList.ValueListItems.Add(4, "Son");
				valueList.ValueListItems.Add(4, "Daughter");
				valueList.ValueListItems.Add(5, "Spouse");
				dataGridRelation.DisplayLayout.Bands[0].Columns["Relation"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridRelation.DisplayLayout.Bands[0].Columns["Relation"].ValueList = valueList;
				dataGridRelation.SetupUI();
			}
			catch (Exception e)
			{
				dataGridRelation.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.CustomerTable.Rows[0];
			textBoxCode.Text = dataRow["CustomerID"].ToString();
			textBoxFirstName.Text = dataRow["CustomerName"].ToString();
			textBoxMiddleName.Text = dataRow["ForeignName"].ToString();
			textBoxNickName.Text = dataRow["ShortName"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			textBoxAddressID.Text = dataRow["PrimaryAddressID"].ToString();
			comboBoxCustomerClass.SelectedID = dataRow["CustomerClassID"].ToString();
			if (dataRow["InsApprovedAmount"] != DBNull.Value)
			{
				textBoxInsuranceApprovedAmount.Text = decimal.Parse(dataRow["InsApprovedAmount"].ToString()).ToString(Format.TotalAmountFormat);
			}
			else
			{
				textBoxInsuranceApprovedAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			if (dataRow["InsRequestedAmount"] != DBNull.Value)
			{
				textBoxInsuranceReqAmount.Text = decimal.Parse(dataRow["InsRequestedAmount"].ToString()).ToString(Format.TotalAmountFormat);
			}
			else
			{
				textBoxInsuranceReqAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			if (dataRow["InsApplicationDate"] != DBNull.Value)
			{
				dateTimePickerInsuranceDate.Value = DateTime.Parse(dataRow["InsApplicationDate"].ToString());
				dateTimePickerInsuranceDate.Checked = true;
			}
			else
			{
				dateTimePickerInsuranceDate.IsNull = true;
				dateTimePickerInsuranceDate.Checked = false;
			}
			if (dataRow["InsRating"] != DBNull.Value)
			{
				comboBoxInsuranceRating.SelectedIndex = int.Parse(dataRow["InsRating"].ToString());
			}
			else
			{
				comboBoxInsuranceRating.SelectedIndex = 0;
			}
			textBoxInsuranceNumber.Text = dataRow["InsPolicyNumber"].ToString();
			textBoxInsuranceRemarks.Text = dataRow["InsRemarks"].ToString();
			textBoxInsuranceID.Text = dataRow["InsuranceID"].ToString();
			checked
			{
				if (dataRow["InsStatus"] != DBNull.Value)
				{
					comboBoxInsuranceStatus.SelectedIndex = unchecked((int)byte.Parse(dataRow["InsStatus"].ToString())) - 1;
				}
				else
				{
					comboBoxInsuranceStatus.SelectedIndex = 0;
				}
				if (dataRow["InsProviderID"] != DBNull.Value)
				{
					comboBoxInsuranceProvider.SelectedID = dataRow["InsProviderID"].ToString().TrimStart();
				}
				DateTime result = new DateTime(1753, 1, 1);
				DateTime result2 = new DateTime(1753, 1, 1);
				if (dataRow["InsEffectiveDate"] != DBNull.Value)
				{
					DateTime.TryParse(dataRow["InsEffectiveDate"].ToString(), out result);
					if ((SqlBoolean)true && result > SqlDateTime.MinValue)
					{
						datetimePickerEffectiveDate.Value = DateTime.Parse(dataRow["InsEffectiveDate"].ToString());
						datetimePickerEffectiveDate.Checked = true;
					}
					else
					{
						datetimePickerEffectiveDate.IsNull = true;
						datetimePickerEffectiveDate.Checked = false;
					}
				}
				else
				{
					datetimePickerEffectiveDate.IsNull = true;
					datetimePickerEffectiveDate.Checked = false;
				}
				if (dataRow["InsExpiryDate"] != DBNull.Value)
				{
					DateTime.TryParse(dataRow["InsExpiryDate"].ToString(), out result2);
					if ((SqlBoolean)true && result2 > SqlDateTime.MinValue)
					{
						dateTimePickerValidTo.Value = DateTime.Parse(dataRow["InsExpiryDate"].ToString());
						dateTimePickerValidTo.Checked = true;
					}
					else
					{
						dateTimePickerValidTo.IsNull = true;
						dateTimePickerValidTo.Checked = false;
					}
				}
				else
				{
					dateTimePickerValidTo.IsNull = true;
					dateTimePickerValidTo.Checked = false;
				}
				dataRow = currentData.PatientTable.Rows[0];
				textBoxNationalID.Text = dataRow["NationalID"].ToString();
				textBoxFirstName.Text = dataRow["FirstName"].ToString();
				textBoxLastName.Text = dataRow["LastName"].ToString();
				textBoxMiddleName.Text = dataRow["MiddleName"].ToString();
				comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
				dateTimePickerDate.Value = DateTime.Parse(dataRow["FileOpenDate"].ToString());
				textBoxFileNo.Text = dataRow["FileNo"].ToString();
				comboBoxGender.SelectedGender = char.Parse(dataRow["Gender"].ToString());
				if (!string.IsNullOrEmpty(dataRow["BirthDate"].ToString()))
				{
					dateTimePickerBirthDate.Value = DateTime.Parse(dataRow["BirthDate"].ToString());
					dateTimePickerBirthDate.Checked = true;
				}
				else
				{
					dateTimePickerBirthDate.Checked = true;
				}
				textBoxBirthPlace.Text = dataRow["BirthPlace"].ToString();
				if (!string.IsNullOrEmpty(dataRow["NationalityID"].ToString()))
				{
					comboBoxNationality.SelectedID = dataRow["NationalityID"].ToString();
				}
				if (!string.IsNullOrEmpty(dataRow["MaritalStatus"].ToString()))
				{
					comboBoxMaritalStatus.SelectedID = int.Parse(dataRow["MaritalStatus"].ToString());
				}
				comboBoxReligion.SelectedID = dataRow["ReligionID"].ToString();
				if (!string.IsNullOrEmpty(dataRow["BloodGroup"].ToString()))
				{
					comboBoxBloodGroup.SelectedID = int.Parse(dataRow["BloodGroup"].ToString());
				}
				textBoxUID.Text = dataRow["UID"].ToString();
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
				if (currentData.FormListDetailsTable.Rows.Count > 0)
				{
					DataRow[] array = currentData.FormListDetailsTable.Select("ControlID= '" + checkedListBoxChronics.Name + "'");
					if (array.Length != 0)
					{
						DataRow[] array2 = array;
						foreach (DataRow dataRow2 in array2)
						{
							new NameValue(dataRow2["ListValue"].ToString(), dataRow2["ListValue"].ToString());
							int index = int.Parse(dataRow2["ValueIndex"].ToString());
							checkedListBoxChronics.SetItemChecked(index, value: true);
						}
					}
					DataRow[] array3 = currentData.FormListDetailsTable.Select("ControlID= '" + checkedListBoxAllergy.Name + "'");
					if (array3.Length != 0)
					{
						DataRow[] array2 = array3;
						foreach (DataRow dataRow3 in array2)
						{
							new NameValue(dataRow3["ListValue"].ToString(), dataRow3["ListValue"].ToString());
							int index2 = int.Parse(dataRow3["ValueIndex"].ToString());
							checkedListBoxAllergy.SetItemChecked(index2, value: true);
						}
					}
				}
				DataTable dataTable = dataGridRelation.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in currentData.Tables["Patient_Detail_Table"].Rows)
				{
					DataRow dataRow5 = dataTable.NewRow();
					dataRow5["Name"] = row["RelativeName"];
					dataRow5["Relation"] = int.Parse(row["Relation"].ToString());
					dataRow5["Age"] = row["Age"];
					dataRow5["Remarks"] = row["Remarks"];
					dataRow5["SlNo"] = row["SlNo"];
					dataRow5.EndEdit();
					dataTable.Rows.Add(dataRow5);
				}
				dataTable.AcceptChanges();
				if (currentData.Tables.Contains("Customer_Address") && currentData.CustomerAddressTable.Rows.Count > 0)
				{
					dataRow = currentData.CustomerAddressTable.Rows[0];
					FillAddressData(dataRow);
					return;
				}
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
		}

		private void FillAddressData(DataRow row)
		{
			textBoxAddressID.Text = row["AddressID"].ToString();
			textBoxContactName.Text = row["ContactName"].ToString();
			textBoxAddress1.Text = row["Address1"].ToString();
			textBoxAddress2.Text = row["Address2"].ToString();
			textBoxAddress3.Text = row["Address3"].ToString();
			textBoxAddressPrintFormat.Text = row["AddressPrintFormat"].ToString();
			textBoxCity.Text = row["City"].ToString();
			textBoxState.Text = row["State"].ToString();
			textBoxCountry.Text = row["Country"].ToString();
			textBoxPostalCode.Text = row["PostalCode"].ToString();
			if (!string.IsNullOrEmpty(row["Latitude"].ToString()))
			{
				textBoxLatitude.Text = row["Latitude"].ToString();
				textBoxLatitude.ForeColor = Color.Black;
			}
			if (!string.IsNullOrEmpty(row["Longitude"].ToString()))
			{
				textBoxLongitude.Text = row["Longitude"].ToString();
				textBoxLongitude.ForeColor = Color.Black;
			}
			textBoxDepartment.Text = row["Department"].ToString();
			textBoxPhone1.Text = row["Phone1"].ToString();
			textBoxPhone2.Text = row["Phone2"].ToString();
			textBoxFax.Text = row["Fax"].ToString();
			textBoxMobile.Text = row["Mobile"].ToString();
			textBoxEmail.Text = row["Email"].ToString();
			textBoxWebsite.Text = row["Website"].ToString();
			textBoxComment.Text = row["Comment"].ToString();
		}

		private void FillChronics()
		{
			DataSet genericListList = Factory.GenericListSystem.GetGenericListList(GenericListTypes.Chronics);
			if (genericListList != null && genericListList.Tables.Count > 0 && genericListList.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in genericListList.Tables[0].Rows)
				{
					NameValue item = new NameValue(row["Name"].ToString(), row["Code"].ToString());
					checkedListBoxChronics.Items.Add(item);
				}
			}
		}

		private void FillAllergy()
		{
			DataSet genericListList = Factory.GenericListSystem.GetGenericListList(GenericListTypes.Allergy);
			if (genericListList != null && genericListList.Tables.Count > 0 && genericListList.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow row in genericListList.Tables[0].Rows)
				{
					NameValue item = new NameValue(row["Name"].ToString(), row["Code"].ToString());
					checkedListBoxAllergy.Items.Add(item);
				}
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PatientData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CustomerTable.Rows[0] : currentData.CustomerTable.NewRow();
				dataRow.BeginEdit();
				dataRow["CustomerID"] = textBoxCode.Text;
				dataRow["CustomerName"] = textBoxFirstName.Text;
				dataRow["ForeignName"] = textBoxMiddleName.Text;
				dataRow["ShortName"] = textBoxNickName.Text;
				if (comboBoxCustomerClass.SelectedID != "")
				{
					dataRow["CustomerClassID"] = comboBoxCustomerClass.SelectedID;
				}
				else
				{
					dataRow["CustomerClassID"] = DBNull.Value;
				}
				dataRow["Note"] = textBoxNote.Text;
				if (dateTimePickerInsuranceDate.Checked)
				{
					dataRow["InsApplicationDate"] = dateTimePickerInsuranceDate.Value;
				}
				else
				{
					dataRow["InsApplicationDate"] = DBNull.Value;
				}
				dataRow["InsApprovedAmount"] = textBoxInsuranceApprovedAmount.Text;
				dataRow["InsPolicyNumber"] = textBoxInsuranceNumber.Text;
				dataRow["InsRemarks"] = textBoxInsuranceRemarks.Text.Trim();
				dataRow["InsRequestedAmount"] = textBoxInsuranceReqAmount.Text;
				dataRow["InsStatus"] = checked(comboBoxInsuranceStatus.SelectedIndex + 1);
				dataRow["InsProviderID"] = comboBoxInsuranceProvider.SelectedID;
				dataRow["InsuranceID"] = textBoxInsuranceID.Text;
				if (comboBoxInsuranceRating.SelectedIndex != -1)
				{
					dataRow["InsRating"] = comboBoxInsuranceRating.SelectedIndex;
				}
				else
				{
					dataRow["InsRating"] = DBNull.Value;
				}
				if (datetimePickerEffectiveDate.Checked)
				{
					dataRow["InsEffectiveDate"] = datetimePickerEffectiveDate.Value;
				}
				if (dateTimePickerValidTo.Checked)
				{
					dataRow["InsExpiryDate"] = dateTimePickerValidTo.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomerTable.Rows.Add(dataRow);
				}
				string text = "";
				string text2 = "";
				string text3 = "";
				AnalysisData analysisData = new AnalysisData();
				if (isNewRecord && EnablePatientAnalysis)
				{
					companyInformation = Factory.CompanyInformationSystem.GetCompanyInformation();
					text = companyInformation.Tables[0].Rows[0]["PatientAnalysisGroup"].ToString();
					text2 = companyInformation.Tables[0].Rows[0]["PatientAnalysisPrefix"].ToString();
					if (string.IsNullOrEmpty(text2) || string.IsNullOrWhiteSpace(text2))
					{
						ErrorHelper.WarningMessage("Set AnalysisPrefix on Company settings");
					}
					text3 = text2 + textBoxCode.Text;
					DataRow dataRow2 = analysisData.AnalysisTable.NewRow();
					dataRow2["AnalysisID"] = text3;
					dataRow2["AnalysisName"] = textBoxFirstName.Text;
					dataRow2["GroupID"] = text;
					dataRow2.EndEdit();
					analysisData.AnalysisTable.Rows.Add(dataRow2);
				}
				dataRow = ((!isNewRecord) ? currentData.CustomerAddressTable.Rows[0] : currentData.CustomerAddressTable.NewRow());
				dataRow.BeginEdit();
				dataRow["CustomerID"] = textBoxCode.Text;
				dataRow["AddressID"] = textBoxAddressID.Text.Trim();
				dataRow["ContactName"] = textBoxContactName.Text;
				dataRow["Address1"] = textBoxAddress1.Text;
				dataRow["Address2"] = textBoxAddress2.Text;
				dataRow["Address3"] = textBoxAddress3.Text;
				dataRow["AddressPrintFormat"] = textBoxAddressPrintFormat.Text;
				dataRow["City"] = textBoxCity.Text;
				dataRow["State"] = textBoxState.Text;
				dataRow["Country"] = textBoxCountry.Text;
				dataRow["PostalCode"] = textBoxPostalCode.Text;
				if (textBoxLatitude.Text != "25.278306" && textBoxLongitude.Text != "55.322663")
				{
					dataRow["Latitude"] = textBoxLatitude.Text;
					dataRow["Longitude"] = textBoxLongitude.Text;
				}
				else
				{
					dataRow["Longitude"] = DBNull.Value;
					dataRow["Latitude"] = DBNull.Value;
				}
				dataRow["Department"] = textBoxDepartment.Text;
				dataRow["Phone1"] = textBoxPhone1.Text;
				dataRow["Phone2"] = textBoxPhone2.Text;
				dataRow["Fax"] = textBoxFax.Text;
				dataRow["Mobile"] = textBoxMobile.Text;
				dataRow["Email"] = textBoxEmail.Text;
				dataRow["Website"] = textBoxWebsite.Text;
				dataRow["Comment"] = textBoxComment.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomerAddressTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.PatientTable.Rows[0] : currentData.PatientTable.NewRow());
				dataRow.BeginEdit();
				dataRow["CustomerID"] = textBoxCode.Text;
				dataRow["FileOpenDate"] = dateTimePickerDate.Value;
				dataRow["FileNo"] = textBoxFileNo.Text;
				dataRow["FirstName"] = textBoxFirstName.Text;
				dataRow["LastName"] = textBoxLastName.Text;
				dataRow["MiddleName"] = textBoxMiddleName.Text;
				dataRow["NationalID"] = textBoxNationalID.Text;
				if (comboBoxLocation.SelectedID != "")
				{
					dataRow["LocationID"] = comboBoxLocation.SelectedID;
				}
				else
				{
					dataRow["LocationID"] = DBNull.Value;
				}
				if (comboBoxGender.SelectedID.ToString() != "")
				{
					dataRow["Gender"] = comboBoxGender.SelectedGender;
				}
				else
				{
					dataRow["Gender"] = 'M';
				}
				if (dateTimePickerBirthDate.Checked)
				{
					dataRow["BirthDate"] = dateTimePickerBirthDate.Value;
				}
				else
				{
					dataRow["BirthDate"] = DBNull.Value;
				}
				dataRow["BirthPlace"] = textBoxBirthPlace.Text;
				if (comboBoxNationality.SelectedID != "")
				{
					dataRow["NationalityID"] = comboBoxNationality.SelectedID;
				}
				else
				{
					dataRow["NationalityID"] = DBNull.Value;
				}
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
				if (comboBoxBloodGroup.SelectedText != "")
				{
					dataRow["BloodGroup"] = comboBoxBloodGroup.SelectedID;
				}
				dataRow["UID"] = textBoxUID.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.PatientTable.Rows.Add(dataRow);
				}
				currentData.FormListDetailsTable.Rows.Clear();
				foreach (object item in checkedListBoxChronics.Items)
				{
					if (checkedListBoxChronics.GetItemChecked(checkedListBoxChronics.Items.IndexOf(item)))
					{
						NameValue nameValue = item as NameValue;
						dataRow = currentData.FormListDetailsTable.NewRow();
						dataRow["FormType"] = (byte)1;
						dataRow["FormID"] = textBoxCode.Text;
						dataRow["ListValue"] = nameValue.ID;
						dataRow["ListName"] = nameValue.Name;
						dataRow["ValueIndex"] = checkedListBoxChronics.Items.IndexOf(item);
						dataRow["ControlID"] = checkedListBoxChronics.Name;
						currentData.FormListDetailsTable.Rows.Add(dataRow);
					}
				}
				foreach (object item2 in checkedListBoxAllergy.Items)
				{
					if (checkedListBoxAllergy.GetItemChecked(checkedListBoxAllergy.Items.IndexOf(item2)))
					{
						dataRow = currentData.FormListDetailsTable.NewRow();
						dataRow.BeginEdit();
						NameValue nameValue2 = item2 as NameValue;
						dataRow["FormType"] = FormType.Patient;
						dataRow["FormID"] = textBoxCode.Text;
						dataRow["ListValue"] = nameValue2.ID;
						dataRow["ListName"] = nameValue2.Name;
						dataRow["ValueIndex"] = checkedListBoxAllergy.Items.IndexOf(item2);
						dataRow["ControlID"] = checkedListBoxAllergy.Name;
						dataRow.EndEdit();
						currentData.FormListDetailsTable.Rows.Add(dataRow);
					}
				}
				currentData.PatientDetailsTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridRelation.Rows)
				{
					DataRow dataRow3 = currentData.PatientDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["CustomerID"] = textBoxCode.Text;
					dataRow3["RelativeName"] = row.Cells["Name"].Value.ToString();
					dataRow3["Age"] = row.Cells["Age"].Value.ToString();
					dataRow3["Relation"] = int.Parse(row.Cells["Relation"].Value.ToString());
					dataRow3["Remarks"] = row.Cells["Remarks"].Value.ToString();
					dataRow3["RowIndex"] = row.Index;
					dataRow3["SlNo"] = row.Cells["SlNo"].Value.ToString();
					currentData.PatientDetailsTable.Rows.Add(dataRow3);
					dataRow3.EndEdit();
				}
				if (analysisData != null)
				{
					currentData.Merge(analysisData);
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
				if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Patient", "CustomerID", textBoxCode.Text.Trim()))
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
					flag = Factory.PatientSystem.CreateCustomer(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Employee, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.PatientSystem.UpdateCustomer(currentData);
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
					currentData = Factory.PatientSystem.GetCustomerByID(id);
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

		public void LoadData(PatientData patientData)
		{
			try
			{
				if (patientData != null)
				{
					currentData = patientData;
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Focus();
					}
					else
					{
						FillData();
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
			LoadData(DatabaseHelper.GetPreviousID("Patient", "CustomerID", textBoxCode.Text));
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Patient", "CustomerID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Patient", "CustomerID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Patient", "CustomerID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Patient", "CustomerID", toolStripTextBoxFind.Text.Trim()))
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
				return Factory.PatientSystem.DeleteCustomer(textBoxCode.Text);
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Patient", "CustomerID");
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
			comboBoxCustomerClass.Clear();
			comboBoxLocation.Clear();
			comboBoxReligion.Clear();
			comboBoxInsuranceProvider.Clear();
			textBoxFirstName.Clear();
			textBoxLastName.Clear();
			textBoxMiddleName.Clear();
			textBoxNickName.Clear();
			textBoxNationalID.Clear();
			textBoxFileNo.Clear();
			textBoxUID.Clear();
			comboBoxGender.Clear();
			comboBoxMaritalStatus.Clear();
			comboBoxNationality.Clear();
			comboBoxBloodGroup.Clear();
			textBoxContactName.Clear();
			comboBoxInsuranceRating.SelectedIndex = 0;
			datetimePickerEffectiveDate.Clear();
			datetimePickerEffectiveDate.Checked = false;
			dateTimePickerValidTo.Clear();
			dateTimePickerValidTo.Checked = false;
			comboBoxInsuranceStatus.SelectedIndex = 0;
			dateTimePickerInsuranceDate.Checked = false;
			textBoxInsuranceApprovedAmount.SetZero();
			textBoxInsuranceNumber.Clear();
			textBoxInsuranceRemarks.Clear();
			textBoxInsuranceReqAmount.SetZero();
			textBoxInsuranceID.Clear();
			comboBoxGender.SelectedGender = 'M';
			dateTimePickerBirthDate.Checked = false;
			textBoxBirthPlace.Clear();
			linkLoadImage.Visible = false;
			pictureBoxPhoto.Image = null;
			checkedListBoxChronics.Items.Clear();
			checkedListBoxAllergy.Items.Clear();
			FillChronics();
			FillAllergy();
			dataGridRelation.Clear();
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
			new FormHelper().EditEmployeeAddress(textBoxCode.Text, textBoxAddressID.Text);
		}

		private void linkLabelCountry_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditWorkLocation(comboBoxLocation.SelectedID);
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
			new FormHelper().EditNationality(comboBoxNationality.SelectedID);
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
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
			FormActivator.BringFormToFront(FormActivator.PatientDocumentsFormObj);
			FormActivator.PatientDocumentsFormObj.LoadData(textBoxCode.Text);
		}

		private void skillsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeSkillsFormObj);
			FormActivator.EmployeeSkillsFormObj.LoadData(textBoxCode.Text);
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
					DataSet customerDataToPrint = Factory.PatientSystem.GetCustomerDataToPrint(textBoxCode.Text, textBoxCode.Text);
					if (customerDataToPrint == null || customerDataToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(customerDataToPrint, "", "Patient", SysDocTypes.None, isPrint, showPrintDialog);
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
			new FormHelper().ShowList(DataComboType.Patient);
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
					docManagementForm.EntityType = EntityTypesEnum.Patients;
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
		}

		private void ultraFormattedLinkLabel11_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void linkAddPicture_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (!(textBoxCode.Text == "") && !IsNewRecord && openFileDialog1.ShowDialog(this) == DialogResult.OK)
				{
					Image image = Image.FromFile(openFileDialog1.FileName);
					if (PublicFunctions.AddPatientPhoto(textBoxCode.Text, image))
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
					if (Factory.PatientSystem.RemovePatientPhoto(textBoxCode.Text))
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
					pictureBoxPhoto.Image = PublicFunctions.GetPatientThumbnailImage(textBoxCode.Text);
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
			new FormHelper();
		}

		private void comboBoxPosition_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel13_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel14_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel15_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void udfEntryGrid_Load(object sender, EventArgs e)
		{
		}

		private void toolStripButtonShowPicture_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabelRcrtmnt_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabelVisaDesig_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void tabPageGeneral_Paint(object sender, PaintEventArgs e)
		{
		}

		private void checkedListBoxChronics_SelectedIndexChanged(object sender, EventArgs e)
		{
			formManager.IsForcedDirty = true;
		}

		private void checkedListBoxAllergy_SelectedIndexChanged(object sender, EventArgs e)
		{
			formManager.IsForcedDirty = true;
		}

		private void linkLabelChronics_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditChronics("");
		}

		private void linkLabelAllergy_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAllergy("");
		}

		private void linkLabelCustomerClass_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomerClass(comboBoxCustomerClass.SelectedID);
		}

		private void linkLabelProvider_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditInsuranceProvider(comboBoxInsuranceProvider.SelectedID);
		}

		private void linkLabelReligion_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditReligion(comboBoxReligion.SelectedID);
		}
	}
}
