using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class CompanyOptionsForm : Form, IForm
	{
		private CompanyInformationData currentData;

		private CompanyOptionData companyOptionData;

		private CurrencyData currencyData;

		private string TABLENAME_CONST = "";

		private string IDFIELD_CONST = "";

		private bool isNewRecord = true;

		private string cusPurpleFlag = "Purple";

		private string cusRedFlag = "Red";

		private string cusOrangeFlag = "Orange";

		private string cusYellowFlag = "Yellow";

		private string cusGreenFlag = "Green";

		private string cusBlueFlag = "Blue";

		private string venPurpleFlag = "Purple";

		private string venRedFlag = "Red";

		private string venOrangeFlag = "Orange";

		private string venYellowFlag = "Yellow";

		private string venGreenFlag = "Green";

		private string venBlueFlag = "Blue";

		private string itmPurpleFlag = "Purple";

		private string itmRedFlag = "Red";

		private string itmOrangeFlag = "Orange";

		private string itmYellowFlag = "Yellow";

		private string itmGreenFlag = "Green";

		private string itmBlueFlag = "Blue";

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private FormManager formManager;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl tabPageUserDefined;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraTabPageControl ultraTabPageControl2;

		private UltraTabPageControl ultraTabPageControl3;

		private GroupBox groupBox1;

		private MMLabel lblDescriptions;

		private MMTextBox textBoxItemPrice2;

		private MMTextBox textBoxItemPrice1;

		private MMLabel mmLabel6;

		private MMLabel label1;

		private MMTextBox textBoxItemPrice3;

		private CheckBox checkBoxTax;

		private OptionsAllowComboBox comboBoxMinPriceAction;

		private MMTextBox textBoxMinPricePassword;

		private MMLabel mmLabel20;

		private MMLabel mmLabel18;

		private Panel panelMinPricePassword;

		private MMLabel mmLabel19;

		private GroupBox groupBox2;

		private Panel panelOverCLPassword;

		private MMTextBox textBoxOverCLPassword;

		private MMLabel mmLabel23;

		private MMLabel mmLabel22;

		private OptionsAllowComboBox comboBoxOverCLAction;

		private MMLabel mmLabel21;

		private Panel panelNegativeQuantityPassword;

		private MMTextBox textBoxNegativeQuantityPassword;

		private MMLabel mmLabel26;

		private MMLabel mmLabel25;

		private OptionsAllowComboBox comboBoxMinusNegativeQuantityAction;

		private MMLabel mmLabel24;

		private GroupBox groupBox4;

		private CheckBox checkBoxIncludePDC;

		private CheckBox checkBoxMultiCurrency;

		private UltraTabPageControl ultraTabPageControl4;

		private GroupBox groupBox8;

		private MMLabel mmLabel31;

		private CheckBox checkBoxImportAllowNewInGRN;

		private CheckBox checkBoxLocalAllowNewInGRN;

		private CheckBox checkBoxImportAllowGRNWithoutPO;

		private CheckBox checkBoxLocalAllowGRNWithoutPO;

		private CheckBox checkBoxImportMoreThanPOQTY;

		private CheckBox checkBoxLocalMoreThanPOQTY;

		private MMLabel mmLabel33;

		private ComboBox comboBoxImportPurchaseFlow;

		private ComboBox comboBoxLocalPurchaseFlow;

		private CheckBox checkBoxESAllowMoreQTY;

		private CheckBox checkBoxESAllowWithoutSO;

		private CheckBox checkBoxESAllowAddNew;

		private MMLabel mmLabel34;

		private CheckBox checkBoxLSAllowInvoiceMore;

		private CheckBox checkBoxLSAllowWithoutSO;

		private ComboBox comboBoxLocalSalesFlow;

		private CheckBox checkBoxLSAllowAddNew;

		private MMLabel mmLabel32;

		private ComboBox comboBoxExportSalesFlow;

		private CheckBox checkBoxLSAllowDNWithoutInvoice;

		private CheckBox checkBoxESAllowDNWithoutInvoice;

		private CheckBox checkBoxAllowLSReturnWithoutInvoice;

		private GroupBox groupBox10;

		private CheckBox checkBoxLSAllowReturnChangePrice;

		private CheckBox checkBoxShowLCAmount;

		private GroupBox groupBox11;

		private CheckBox checkBoxLPurchaseLandingCost;

		private GroupBox groupBox12;

		private GroupBox groupBox3;

		private RadioButton radioButtonMatrixDescAttribute;

		private RadioButton radioButtonMatrixDescOnly;

		private MMLabel mmLabel4;

		private CheckBox checkBoxInvoiceNegativeQty;

		private CheckBox checkBoxPurchaseNegativeQty;

		private UltraTabPageControl ultraTabPageControl5;

		private GroupBox groupBox9;

		private NumberTextBox textBoxToMonth6;

		private NumberTextBox textBoxToMonth4;

		private NumberTextBox textBoxFromMonth6;

		private TextBox textBoxAgingName6;

		private CheckBox checkBoxMonth6;

		private NumberTextBox textBoxToMonth5;

		private NumberTextBox textBoxFromMonth5;

		private TextBox textBoxAgingName5;

		private CheckBox checkBoxMonth5;

		private NumberTextBox FromtextBoxToMonth4;

		private NumberTextBox textBoxFromMonth4;

		private TextBox textBoxAgingName4;

		private CheckBox checkBoxMonth4;

		private NumberTextBox textBoxToMonth3;

		private NumberTextBox textBoxFromMonth3;

		private TextBox textBoxAgingName3;

		private CheckBox checkBoxMonth3;

		private NumberTextBox textBoxToMonth2;

		private NumberTextBox textBoxFromMonth2;

		private TextBox textBoxAgingName2;

		private CheckBox checkBoxMonth2;

		private NumberTextBox textBoxToMonth1;

		private NumberTextBox textBoxFromMonth1;

		private TextBox textBoxAgingName1;

		private CheckBox checkBoxMonth1;

		private Label label4;

		private NumberTextBox textBoxToMonth0;

		private NumberTextBox textBoxFromMonth0;

		private Label label3;

		private RadioButton radioButtonAgingDate;

		private Label label2;

		private TextBox textBoxAgingName0;

		private RadioButton radioButtonAgingDueDate;

		private Panel panelMonth6;

		private Panel panelMonth5;

		private Panel panelMonth4;

		private Panel panelMonth3;

		private Panel panelMonth2;

		private Panel panelMonth1;

		private Panel panelCurrent;

		private GroupBox groupBox5;

		private MMLabel mmLabel1;

		private MMTextBox textBoxAttribute2Name;

		private MMTextBox textBoxAttribute1Name;

		private MMLabel mmLabel2;

		private MMLabel mmLabel3;

		private MMTextBox textBoxAttribute3Name;

		private GroupBox groupBox6;

		private RadioButton radioButtonPDCByMaturity;

		private RadioButton radioButtonPDCByTransaction;

		private MMLabel mmLabel5;

		private XPButton buttonOpenFileDialog;

		private MMTextBox textFilePath;

		private MMLabel labelFileSavingPath;

		private UltraTabPageControl tabPagePOS;

		private CheckBox checkBoxPOSSufficientQuantity;

		private GroupBox groupBox7;

		private CheckBox checkBoxJobCosting;

		private GroupBox groupBox13;

		private ComboBox comboBoxTemplatePath;

		private MMTextBox textBoxTemplatePath;

		private MMLabel mmLabel7;

		private XPButton buttonSelectTemplatePath;

		private GroupBox groupBox14;

		private RadioButton radioButtonDirectMaturity;

		private RadioButton radioButtonIndirectMaturity;

		private MMLabel mmLabel8;

		private GroupBox groupBox15;

		private MMLabel mmLabel10;

		private MMLabel mmLabel11;

		private DaysComboBox comboBoxDays;

		private MMLabel mmLabel9;

		private GroupBox groupBox16;

		private CheckBox checkBoxLoadDescFromPriceList;

		private NumberTextBox textBoxMonthHours;

		private NumberTextBox textBoxDayHours;

		private CheckBox checkBoxInlineDiscount;

		private CheckBox checkBoxTrackConsignOutItemsSale;

		private GroupBox groupBox17;

		private CheckBox checkBoxConsignInFIFO;

		private CheckBox checkBoxTrackConsignInDetail;

		private CheckBox checkBoxTrackConsignInExpenses;

		private MMLabel mmLabel13;

		private MMTextBox textBoxCompanyWPSID;

		private MMLabel mmLabel12;

		private MMTextBox textBoxBankName;

		private BankComboBox comboBoxBank;

		private CheckBox checkBoxPurchaseOrderChangePrice;

		private Panel panelPurchaseLandingCostMethod;

		private RadioButton radioButtonWeight;

		private RadioButton radioButtonValue;

		private MMLabel mmLabel14;

		private CheckBox checkBoxImportAllowNewInPackingList;

		private CheckBox checkBoxCostCenter;

		private GroupBox groupBox18;

		private MMTextBox textBoxServerTemplatePath;

		private MMLabel mmLabel15;

		private XPButton btnOpenFileDialog2;

		private CheckBox checkBoxAllocationForm;

		private CheckBox checkBoxImportMoreThanPLQTY;

		private CheckBox checkBoxShowQuantityInProductCombo;

		private UltraTabPageControl ultraTabPageProject;

		private CheckBox checkBoxUseProjectPhase;

		private CheckBox checkBoxAllowChangeChequePayee;

		private GroupBox groupBox19;

		private CheckBox checkBoxAllowPReturnChangePrice;

		private CheckBox checkBoxAllowPReturnWithoutInvoice;

		private MMLabel mmLabel16;

		private Panel panel1;

		private RadioButton radioButtonEmployee;

		private RadioButton radioButtonDate;

		private GroupBox groupBox20;

		private CheckBox checkBoxShowUnitInProductCombo;

		private CheckBox checkBoxGRNZeroQty;

		private CheckBox checkBoxAutoCode;

		private CheckBox checkBoxIssueGRNtoProject;

		private CheckBox checkBoxShowLotdetailinPrintout;

		private GroupBox groupBox21;

		private Panel panel4;

		private NumberTextBox textBoxInvToMonth4;

		private NumberTextBox textBoxInvFromMonth4;

		private TextBox textBoxInvAgingName4;

		private Panel panel5;

		private NumberTextBox textBoxInvToMonth3;

		private NumberTextBox textBoxInvFromMonth3;

		private TextBox textBoxInvAgingName3;

		private Panel panel6;

		private NumberTextBox textBoxInvToMonth2;

		private NumberTextBox textBoxInvFromMonth2;

		private TextBox textBoxInvAgingName2;

		private Panel panel7;

		private NumberTextBox textBoxInvToMonth1;

		private NumberTextBox textBoxInvFromMonth1;

		private TextBox textBoxInvAgingName1;

		private CheckBox checkBoxInvMonth4;

		private CheckBox checkBoxInvMonth3;

		private CheckBox checkBoxInvMonth2;

		private CheckBox checkBoxInvMonth1;

		private Label label5;

		private Label label6;

		private Label label7;

		private CheckBox checkBoxShowItemdetail;

		private GroupBox groupBox22;

		private RadioButton radioButtonPDCIssuedByMaturity;

		private RadioButton radioButtonPDCIssuedByTransaction;

		private MMLabel mmLabel17;

		private CheckBox checkBoxShowCostInProductCombo;

		private CheckBox checkBoxTakelastSalesprice;

		private CheckBox checkBoxShowitemFeatures;

		private UltraTabPageControl ultraTabPageControl6;

		private CheckBox checkBoxAllowdofollowuponlead;

		private UltraTabPageControl ultraTabPageControl7;

		private MMLabel mmLabel29;

		private MMLabel mmLabel28;

		private MMTextBox textBoxSMSMobileNo;

		private MMTextBox textBoxSMSUserName;

		private MMTextBox textBoxSMSPassword;

		private MMLabel mmLabel27;

		private CheckBox checkBoxAllowcreditsaleinSalesReceipt;

		private CheckBox checkBoxOnlyOpenInvoice;

		private Button buttonCustomerFlagNames;

		private Button buttonVenFlags;

		private Button buttonItmFlags;

		private MMLabel mmLabel30;

		private OptionsAllowComboBox comboBoxsaleslessthanCostAction;

		private MMLabel mmLabel35;

		private Panel panelsalespricelessthancost;

		private MMTextBox textBoxPricelessthancostpassword;

		private MMLabel mmLabel36;

		private CheckBox checkBoxDeliveryNoteCL;

		private GroupBox groupBox23;

		private MMLabel mmLabel40;

		private NumberTextBox textBoxRangeTo;

		private NumberTextBox textBoxRemarkvalidationpoint;

		private NumberTextBox textBoxRangeFrom;

		private MMLabel mmLabel38;

		private MMLabel mmLabel39;

		private CheckBox checkBoxAllowtoeditreqdateinPO;

		private CheckBox checkBoxNotallowzeroinSales;

		private GroupBox groupBoxLotDetails;

		private CheckBox checkBoxActiveBinField;

		private Label label9;

		private MMTextBox textBoxRenameRef2;

		private Label label8;

		private MMTextBox textBoxRenameLotNo;

		private CheckBox checkBoxESAllowtocreatepicklist;

		private CheckBox checkBoxCreateProjectwithSO;

		private CheckBox checkBoxShowUPCInProductCombo;

		private RadioButton radioButtonAgingEOMDueDate;

		private GroupBox groupBox24;

		private RadioButton radioButton1;

		private RadioButton radioButton2;

		private MMLabel mmLabel37;

		private GroupBox groupBox25;

		private Panel panel3;

		private RadioButton radioButtonAnnual;

		private RadioButton radioButtonThirtyDays;

		private RadioButton radioButtonDaysInMonth;

		private GroupBox groupBox26;

		private RadioButton radioButton3;

		private RadioButton radioButton4;

		private MMLabel mmLabel42;

		private GroupBox groupBox27;

		private Panel panel2;

		private RadioButton radioButtondeductiononNetDaysSal;

		private RadioButton radioButtonDeductionoonNetDays;

		private GroupBox groupBox28;

		private RadioButton radioButton5;

		private RadioButton radioButton6;

		private MMLabel mmLabel41;

		private MMLabel mmLabel43;

		private NumberTextBox textBoxAutoresumptionDays;

		private CheckBox checkBoxOrderandShipment;

		private CheckBox checkBoxRoundOffSalaryCalculation;

		private CheckBox checkBoxShowBOLinPL;

		private CheckBox checkBoxConsiderStockinMRPQ;

		private CheckBox checkBoxFinancialTransactionPosting;

		private CheckBox checkBoxAllowJobChange;

		private CheckBox checkBoxActivateAutoService;

		private CheckBox checkBoxAllowCustomerChangeInDN;

		private GroupBox groupBox29;

		private CheckBox checkBoxHRnalaysis;

		private Panel panelHRanalysis;

		private GroupBox groupBox30;

		private RadioButton radioButton9;

		private RadioButton radioButton10;

		private MMLabel mmLabel44;

		private MMLabel mmLabel46;

		private MMTextBox textboxHRanalysisPrefix;

		private MMLabel mmLabel45;

		private AnalysisGroupComboBox comboBoxHRAnalysis;

		private GroupBox groupBox31;

		private CheckBox checkBoxVehicleAnalysis;

		private Panel panelVehicleanalysis;

		private AnalysisGroupComboBox comboBoxVehicleAnalysis;

		private MMLabel mmLabel47;

		private MMTextBox textboxVehicleanalysisPrefix;

		private MMLabel mmLabel48;

		private GroupBox groupBox32;

		private RadioButton radioButton7;

		private RadioButton radioButton8;

		private MMLabel mmLabel49;

		private UltraTabPageControl ultraTabPageControl8;

		private GroupBox groupBox33;

		private CheckBox checkBoxLegalanalaysis;

		private Panel panelLegalAnalysis;

		private AnalysisGroupComboBox comboBoxLegalAnalysis;

		private MMLabel mmLabel50;

		private MMTextBox textboxLegalanalysisPrefix;

		private MMLabel mmLabel51;

		private GroupBox groupBox34;

		private RadioButton radioButton11;

		private RadioButton radioButton12;

		private MMLabel mmLabel52;

		private GroupBox groupBox35;

		private MMTextBox mmTextBox1;

		private MMTextBox txtBoxSpecificationName;

		private MMLabel mmLabel54;

		private MMLabel mmLabel55;

		private CheckBox checkBoxExcludeZeroQtyInDN;

		private GroupBox groupBox36;

		private MMTextBox textBoxDescription3;

		private MMLabel mmLabel57;

		private CheckBox checkBoxActivatePartsDetails;

		private ComboBox comboBoxItemCreationOption;

		private MMLabel mmLabel53;

		private CheckBox checkBoxEnableTempSaving;

		private Label label10;

		private MMLabel mmLabel56;

		private NumberTextBox textboxDiscountWriteoffPerc;

		private MMLabel labelperc;

		private NumberTextBox textBoxTax;

		private CheckBox checkBoxShowMultiDimension;

		private RadioButton radioButtonEOMInvoiceDate;

		private MMLabel mmLabel58;

		private OptionsAllowComboBox comboBoxoptionsAllocation;

		private MMLabel mmLabel59;

		private CheckBox checkBoxenablecostondelete;

		private CheckBox checkBoxUpdateSalesPersonWhileSave;

		private CheckBox checkBoxDisplayItemFeatures;

		private GroupBox groupBox37;

		private RadioButton radioButtonDirectTREntry;

		private RadioButton radioButtonTRAppTREntry;

		private CheckBox checkBoxfutureCosting;

		private CheckBox checkBoxDirectChequeReturn;

		private CheckBox checkBoxManadatoryPO;

		private GroupBox groupBox38;

		private Panel panel8;

		private MMLabel mmLabel61;

		private GroupBox groupBox39;

		private RadioButton radioButton13;

		private RadioButton radioButton14;

		private MMLabel mmLabel62;

		private ComboBox comboBoxDefaultTaxOption;

		private MMLabel mmLabel63;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelTaxGroup;

		private TaxGroupComboBox comboBoxDefaultTaxGroup;

		private CheckBox checkBoxVendor;

		private CheckBox checkBoxProduct;

		private CheckBox checkBoxCustomer;

		private CheckBox checkBoxUnit;

		private CheckBox checkBoxTenant;

		private CheckBox checkBoxProperty;

		private MMLabel mmLabel60;

		private OptionsAllowComboBox ComboBoxPackingListQtyAction;

		private Label label11;

		private CheckBox checkBoxVehicleAnlysis;

		private CheckBox checkBoxB2Cbased;

		private GroupBox grpCostSetting;

		private CheckBox checkBoxAllCosting;

		private CheckBox checkBoxMaterialReservationOnSo;

		private CheckBox checkBoxDocumentVersioning;

		private MMLabel mmLabel64;

		private MMTextBox textBoxPType2Name;

		private MMTextBox textBoxPType1Name;

		private MMLabel mmLabel65;

		private MMLabel mmLabel66;

		private MMTextBox textBoxPType3Name;

		private UltraExpandableGroupBox expandableGroupBoxTypesName;

		private UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel1;

		private MMLabel mmLabel70;

		private MMTextBox textBoxPType7Name;

		private MMTextBox textBoxPType8Name;

		private MMLabel mmLabel71;

		private MMLabel mmLabel67;

		private MMLabel mmLabel68;

		private MMTextBox textBoxPType5Name;

		private MMTextBox textBoxPType6Name;

		private MMTextBox textBoxPType4Name;

		private MMLabel mmLabel69;

		private CheckBox checkBoxDisableCustomerCreditLimit;

		private UltraExpandableGroupBox ultraExpandableGroupBoxGridFields;

		private UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel2;

		private MMLabel mmLabel73;

		private MMLabel mmLabel74;

		private MMLabel mmLabel75;

		private MMLabel mmLabel76;

		private MMLabel mmLabel77;

		private MMLabel mmLabel78;

		private MMTextBox textBoxRefText1;

		private MMTextBox textBoxRefText2;

		private MMTextBox textBoxRefSlNo;

		private MMLabel mmLabel79;

		private MMTextBox textBoxRefDate2;

		private MMTextBox textBoxRefDate1;

		private MMTextBox textBoxRefNum2;

		private MMTextBox textBoxRefNum1;

		private GroupBox panelPatientanalysis;

		private CheckBox checkBoxPatientAnalysis;

		private Panel panel9;

		private AnalysisGroupComboBox comboBoxPatientAnalysisGroup;

		private MMLabel mmLabel72;

		private MMTextBox textboxPatientanalysisPrefix;

		private MMLabel mmLabel80;

		private GroupBox groupBox41;

		private RadioButton radioButton15;

		private RadioButton radioButton16;

		private MMLabel mmLabel81;

		private ComboBox comboBoxSoftClosePeriod;

		private MMLabel mmLabel82;

		private UltraPictureBox ultraPictureBoxInformation;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6002;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return false;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public CompanyOptionsForm()
		{
			InitializeComponent();
			comboBoxLocalSalesFlow.SelectedIndexChanged += comboBoxLocalSalesFlow_SelectedIndexChanged;
			comboBoxExportSalesFlow.SelectedIndexChanged += comboBoxExportSalesFlow_SelectedIndexChanged;
			comboBoxImportPurchaseFlow.SelectedIndexChanged += comboBoxImportPurchaseFlow_SelectedIndexChanged;
			comboBoxLocalPurchaseFlow.SelectedIndexChanged += comboBoxLocalPurchaseFlow_SelectedIndexChanged;
			checkBoxTax.CheckStateChanged += checkBoxTax_CheckStateChanged;
			checkBoxLPurchaseLandingCost.CheckedChanged += checkBoxLPurchaseLandingCost_CheckedChanged;
			checkBoxHRnalaysis.CheckStateChanged += checkBoxHRnalaysis_CheckStateChanged;
			checkBoxVehicleAnlysis.CheckStateChanged += checkBoxVehicleAnalysis_CheckStateChanged;
			checkBoxLegalanalaysis.CheckStateChanged += checkBoxLegalAnalysis_CheckStateChanged;
			comboBoxLocalSalesFlow.DropDownWidth = 500;
			comboBoxExportSalesFlow.DropDownWidth = 500;
			comboBoxImportPurchaseFlow.DropDownWidth = 500;
			comboBoxLocalPurchaseFlow.DropDownWidth = 500;
		}

		private void checkBoxTax_CheckStateChanged(object sender, EventArgs e)
		{
			if (checkBoxTax.Checked)
			{
				textBoxTax.ReadOnly = false;
				return;
			}
			textBoxTax.Text = "0.00";
			textBoxTax.ReadOnly = true;
		}

		private void checkBoxHRnalaysis_CheckStateChanged(object sender, EventArgs e)
		{
			if (checkBoxHRnalaysis.Checked)
			{
				panelHRanalysis.Enabled = true;
				return;
			}
			panelHRanalysis.Enabled = false;
			comboBoxHRAnalysis.Clear();
			textboxHRanalysisPrefix.Clear();
		}

		private void checkBoxVehicleAnalysis_CheckStateChanged(object sender, EventArgs e)
		{
			if (checkBoxVehicleAnlysis.Checked)
			{
				panelVehicleanalysis.Enabled = true;
				return;
			}
			panelVehicleanalysis.Enabled = false;
			comboBoxVehicleAnalysis.Clear();
			textboxVehicleanalysisPrefix.Clear();
		}

		private void checkBoxLegalAnalysis_CheckStateChanged(object sender, EventArgs e)
		{
			if (checkBoxLegalanalaysis.Checked)
			{
				panelLegalAnalysis.Enabled = true;
				return;
			}
			panelLegalAnalysis.Enabled = false;
			comboBoxLegalAnalysis.Clear();
			textboxLegalanalysisPrefix.Clear();
		}

		private void checkBoxLPurchaseLandingCost_CheckedChanged(object sender, EventArgs e)
		{
			panelPurchaseLandingCostMethod.Enabled = checkBoxLPurchaseLandingCost.Checked;
		}

		private void comboBoxExportSalesFlow_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = comboBoxExportSalesFlow.SelectedIndex;
			CheckBox checkBox = checkBoxESAllowAddNew;
			CheckBox checkBox2 = checkBoxESAllowMoreQTY;
			bool flag2 = checkBoxESAllowWithoutSO.Enabled = (selectedIndex == 2 || selectedIndex == 1);
			bool enabled = checkBox2.Enabled = flag2;
			checkBox.Enabled = enabled;
			checkBoxESAllowtocreatepicklist.Enabled = (selectedIndex == 2);
		}

		private void comboBoxLocalSalesFlow_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = comboBoxLocalSalesFlow.SelectedIndex;
			CheckBox checkBox = checkBoxLSAllowAddNew;
			CheckBox checkBox2 = checkBoxLSAllowInvoiceMore;
			bool flag2 = checkBoxLSAllowWithoutSO.Enabled = (selectedIndex == 2 || selectedIndex == 1);
			bool enabled = checkBox2.Enabled = flag2;
			checkBox.Enabled = enabled;
		}

		private void comboBoxLocalPurchaseFlow_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = comboBoxLocalPurchaseFlow.SelectedIndex;
			CheckBox checkBox = checkBoxLocalAllowGRNWithoutPO;
			CheckBox checkBox2 = checkBoxLocalAllowNewInGRN;
			bool flag2 = checkBoxLocalMoreThanPOQTY.Enabled = (selectedIndex == 3 || selectedIndex == 1);
			bool enabled = checkBox2.Enabled = flag2;
			checkBox.Enabled = enabled;
		}

		private void comboBoxImportPurchaseFlow_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = comboBoxImportPurchaseFlow.SelectedIndex;
			CheckBox checkBox = checkBoxImportAllowGRNWithoutPO;
			CheckBox checkBox2 = checkBoxImportAllowNewInGRN;
			CheckBox checkBox3 = checkBoxImportAllowNewInPackingList;
			CheckBox checkBox4 = checkBoxImportMoreThanPOQTY;
			bool flag2 = checkBoxImportMoreThanPLQTY.Enabled = (selectedIndex == 3 || selectedIndex == 1);
			bool flag4 = checkBox4.Enabled = flag2;
			bool flag6 = checkBox3.Enabled = flag4;
			bool enabled = checkBox2.Enabled = flag6;
			checkBox.Enabled = enabled;
		}

		private bool GetData()
		{
			checked
			{
				try
				{
					if (currentData == null || isNewRecord)
					{
						currentData = new CompanyInformationData();
					}
					companyOptionData = new CompanyOptionData();
					DataTable companyOptionTable = companyOptionData.CompanyOptionTable;
					companyOptionTable.Rows.Add(50, checkBoxLocalAllowNewInGRN.Checked, 1);
					companyOptionTable.Rows.Add(51, checkBoxImportAllowNewInGRN.Checked, 1);
					companyOptionTable.Rows.Add(127, checkBoxImportAllowNewInPackingList.Checked, 1);
					companyOptionTable.Rows.Add(52, checkBoxLocalAllowGRNWithoutPO.Checked, 1);
					companyOptionTable.Rows.Add(53, checkBoxImportAllowGRNWithoutPO.Checked, 1);
					companyOptionTable.Rows.Add(54, checkBoxLocalMoreThanPOQTY.Checked, 1);
					companyOptionTable.Rows.Add(55, checkBoxImportMoreThanPOQTY.Checked, 1);
					companyOptionTable.Rows.Add(130, checkBoxImportMoreThanPLQTY.Checked, 1);
					companyOptionTable.Rows.Add(58, comboBoxLocalPurchaseFlow.SelectedIndex, 1);
					companyOptionTable.Rows.Add(59, comboBoxImportPurchaseFlow.SelectedIndex, 1);
					companyOptionTable.Rows.Add(70, checkBoxLPurchaseLandingCost.Checked, 1);
					companyOptionTable.Rows.Add(188, checkBoxAllowtoeditreqdateinPO.Checked, 1);
					companyOptionTable.Rows.Add(204, checkBoxConsiderStockinMRPQ.Checked, 1);
					companyOptionTable.Rows.Add(126, radioButtonValue.Checked, 1);
					companyOptionTable.Rows.Add(71, checkBoxShowLCAmount.Checked, 1);
					companyOptionTable.Rows.Add(72, checkBoxConsignInFIFO.Checked, 1);
					companyOptionTable.Rows.Add(121, checkBoxTrackConsignInDetail.Checked, 1);
					companyOptionTable.Rows.Add(122, checkBoxTrackConsignInExpenses.Checked, 1);
					companyOptionTable.Rows.Add(125, checkBoxPurchaseOrderChangePrice.Checked, 1);
					companyOptionTable.Rows.Add(135, checkBoxAllowPReturnWithoutInvoice.Checked, 1);
					companyOptionTable.Rows.Add(136, checkBoxAllowPReturnChangePrice.Checked, 1);
					companyOptionTable.Rows.Add(201, checkBoxOrderandShipment.Checked, 1);
					companyOptionTable.Rows.Add(203, checkBoxShowBOLinPL.Checked, 1);
					companyOptionTable.Rows.Add(227, checkBoxManadatoryPO.Checked, 1);
					companyOptionTable.Rows.Add(111, radioButtonPDCByMaturity.Checked, 1);
					companyOptionTable.Rows.Add(114, radioButtonDirectMaturity.Checked, 1);
					companyOptionTable.Rows.Add(160, radioButtonPDCIssuedByMaturity.Checked, 1);
					companyOptionTable.Rows.Add(128, checkBoxCostCenter.Checked, 1);
					companyOptionTable.Rows.Add(129, checkBoxAllocationForm.Checked, 1);
					companyOptionTable.Rows.Add(134, checkBoxAllowChangeChequePayee.Checked, 1);
					companyOptionTable.Rows.Add(205, checkBoxFinancialTransactionPosting.Checked, 1);
					companyOptionTable.Rows.Add(224, radioButtonDirectTREntry.Checked, 1);
					companyOptionTable.Rows.Add(226, checkBoxDirectChequeReturn.Checked, 1);
					companyOptionTable.Rows.Add(10102, comboBoxSoftClosePeriod.SelectedIndex, 1);
					companyOptionTable.Rows.Add(60, checkBoxLSAllowAddNew.Checked, 1);
					companyOptionTable.Rows.Add(62, checkBoxLSAllowWithoutSO.Checked, 1);
					companyOptionTable.Rows.Add(64, checkBoxLSAllowInvoiceMore.Checked, 1);
					companyOptionTable.Rows.Add(61, checkBoxESAllowAddNew.Checked, 1);
					companyOptionTable.Rows.Add(65, checkBoxESAllowMoreQTY.Checked, 1);
					companyOptionTable.Rows.Add(63, checkBoxESAllowWithoutSO.Checked, 1);
					companyOptionTable.Rows.Add(191, checkBoxESAllowtocreatepicklist.Checked, 1);
					companyOptionTable.Rows.Add(69, checkBoxLSAllowReturnChangePrice.Checked, 1);
					companyOptionTable.Rows.Add(56, comboBoxLocalSalesFlow.SelectedIndex, 1);
					companyOptionTable.Rows.Add(57, comboBoxExportSalesFlow.SelectedIndex, 1);
					companyOptionTable.Rows.Add(66, checkBoxLSAllowDNWithoutInvoice.Checked, 1);
					companyOptionTable.Rows.Add(67, checkBoxESAllowDNWithoutInvoice.Checked, 1);
					companyOptionTable.Rows.Add(68, checkBoxAllowLSReturnWithoutInvoice.Checked, 1);
					companyOptionTable.Rows.Add(74, checkBoxInvoiceNegativeQty.Checked, 1);
					companyOptionTable.Rows.Add(75, checkBoxPurchaseNegativeQty.Checked, 1);
					companyOptionTable.Rows.Add(139, checkBoxGRNZeroQty.Checked, 1);
					companyOptionTable.Rows.Add(107, radioButtonAgingDueDate.Checked, 1);
					companyOptionTable.Rows.Add(194, radioButtonAgingEOMDueDate.Checked, 1);
					companyOptionTable.Rows.Add(220, radioButtonEOMInvoiceDate.Checked, 1);
					companyOptionTable.Rows.Add(113, checkBoxJobCosting.Checked, 1);
					companyOptionTable.Rows.Add(208, checkBoxAllowCustomerChangeInDN.Checked, 1);
					companyOptionTable.Rows.Add(213, checkBoxExcludeZeroQtyInDN.Checked, 1);
					companyOptionTable.Rows.Add(217, checkBoxEnableTempSaving.Checked, 1);
					companyOptionTable.Rows.Add(231, checkBoxB2Cbased.Checked, 1);
					companyOptionTable.Rows.Add(233, checkBoxMaterialReservationOnSo.Checked, 1);
					companyOptionTable.Rows.Add(243, checkBoxDisableCustomerCreditLimit.Checked, 1);
					companyOptionTable.Rows.Add(120, checkBoxTrackConsignOutItemsSale.Checked, 1);
					companyOptionTable.Rows.Add(119, checkBoxInlineDiscount.Checked, 1);
					companyOptionTable.Rows.Add(142, checkBoxShowLotdetailinPrintout.Checked, 1);
					companyOptionTable.Rows.Add(131, checkBoxDeliveryNoteCL.Checked, 1);
					companyOptionTable.Rows.Add(189, checkBoxNotallowzeroinSales.Checked, 1);
					companyOptionTable.Rows.Add(132, checkBoxShowQuantityInProductCombo.Checked, 1);
					companyOptionTable.Rows.Add(138, checkBoxShowUnitInProductCombo.Checked, 1);
					companyOptionTable.Rows.Add(161, checkBoxShowCostInProductCombo.Checked, 1);
					companyOptionTable.Rows.Add(193, checkBoxShowUPCInProductCombo.Checked, 1);
					companyOptionTable.Rows.Add(159, checkBoxShowItemdetail.Checked, 1);
					companyOptionTable.Rows.Add(162, checkBoxTakelastSalesprice.Checked, 1);
					companyOptionTable.Rows.Add(163, checkBoxShowitemFeatures.Checked, 1);
					companyOptionTable.Rows.Add(165, checkBoxAllowcreditsaleinSalesReceipt.Checked, 1);
					companyOptionTable.Rows.Add(166, checkBoxOnlyOpenInvoice.Checked, 1);
					companyOptionTable.Rows.Add(251, checkBoxPatientAnalysis.Checked, 1);
					companyOptionTable.Rows.Add(76, checkBoxMonth1.Checked, 1);
					companyOptionTable.Rows.Add(77, checkBoxMonth2.Checked, 1);
					companyOptionTable.Rows.Add(78, checkBoxMonth3.Checked, 1);
					companyOptionTable.Rows.Add(79, checkBoxMonth4.Checked, 1);
					companyOptionTable.Rows.Add(80, checkBoxMonth5.Checked, 1);
					companyOptionTable.Rows.Add(81, checkBoxMonth6.Checked, 1);
					companyOptionTable.Rows.Add(83, textBoxAgingName0.Text, 1);
					companyOptionTable.Rows.Add(84, textBoxAgingName1.Text, 1);
					companyOptionTable.Rows.Add(85, textBoxAgingName2.Text, 1);
					companyOptionTable.Rows.Add(86, textBoxAgingName3.Text, 1);
					companyOptionTable.Rows.Add(87, textBoxAgingName4.Text, 1);
					companyOptionTable.Rows.Add(88, textBoxAgingName5.Text, 1);
					companyOptionTable.Rows.Add(89, textBoxAgingName6.Text, 1);
					companyOptionTable.Rows.Add(91, textBoxFromMonth0.Text, 1);
					companyOptionTable.Rows.Add(92, textBoxFromMonth1.Text, 1);
					companyOptionTable.Rows.Add(93, textBoxFromMonth2.Text, 1);
					companyOptionTable.Rows.Add(94, textBoxFromMonth3.Text, 1);
					companyOptionTable.Rows.Add(95, textBoxFromMonth4.Text, 1);
					companyOptionTable.Rows.Add(96, textBoxFromMonth5.Text, 1);
					companyOptionTable.Rows.Add(97, textBoxFromMonth6.Text, 1);
					companyOptionTable.Rows.Add(99, textBoxToMonth0.Text, 1);
					companyOptionTable.Rows.Add(100, textBoxToMonth1.Text, 1);
					companyOptionTable.Rows.Add(101, textBoxToMonth2.Text, 1);
					companyOptionTable.Rows.Add(102, textBoxToMonth3.Text, 1);
					companyOptionTable.Rows.Add(103, textBoxToMonth4.Text, 1);
					companyOptionTable.Rows.Add(104, textBoxToMonth5.Text, 1);
					companyOptionTable.Rows.Add(105, textBoxToMonth6.Text, 1);
					if (radioButtonMatrixDescOnly.Checked)
					{
						companyOptionTable.Rows.Add(73, 1, 1);
					}
					else
					{
						companyOptionTable.Rows.Add(73, 2, 1);
					}
					companyOptionTable.Rows.Add(108, textBoxAttribute1Name.Text, 1);
					companyOptionTable.Rows.Add(109, textBoxAttribute2Name.Text, 1);
					companyOptionTable.Rows.Add(110, textBoxAttribute3Name.Text, 1);
					companyOptionTable.Rows.Add(235, textBoxPType1Name.Text, 1);
					companyOptionTable.Rows.Add(236, textBoxPType2Name.Text, 1);
					companyOptionTable.Rows.Add(237, textBoxPType3Name.Text, 1);
					companyOptionTable.Rows.Add(238, textBoxPType4Name.Text, 1);
					companyOptionTable.Rows.Add(239, textBoxPType5Name.Text, 1);
					companyOptionTable.Rows.Add(240, textBoxPType6Name.Text, 1);
					companyOptionTable.Rows.Add(241, textBoxPType7Name.Text, 1);
					companyOptionTable.Rows.Add(242, textBoxPType8Name.Text, 1);
					companyOptionTable.Rows.Add(244, textBoxRefSlNo.Text, 1);
					companyOptionTable.Rows.Add(245, textBoxRefText1.Text, 1);
					companyOptionTable.Rows.Add(246, textBoxRefText2.Text, 1);
					companyOptionTable.Rows.Add(247, textBoxRefNum1.Text, 1);
					companyOptionTable.Rows.Add(248, textBoxRefNum2.Text, 1);
					companyOptionTable.Rows.Add(249, textBoxRefDate1.Text, 1);
					companyOptionTable.Rows.Add(250, textBoxRefDate2.Text, 1);
					companyOptionTable.Rows.Add(212, txtBoxSpecificationName.Text, 1);
					companyOptionTable.Rows.Add(118, checkBoxLoadDescFromPriceList.Checked, 1);
					companyOptionTable.Rows.Add(221, checkBoxenablecostondelete.Checked, 1);
					companyOptionTable.Rows.Add(190, checkBoxActiveBinField.Checked, 1);
					companyOptionTable.Rows.Add(210, checkBoxVehicleAnalysis.Checked, 1);
					companyOptionTable.Rows.Add(214, checkBoxActivatePartsDetails.Checked, 1);
					companyOptionTable.Rows.Add(219, checkBoxShowMultiDimension.Checked, 1);
					companyOptionTable.Rows.Add(215, textBoxDescription3.Text, 1);
					companyOptionTable.Rows.Add(216, comboBoxItemCreationOption.SelectedIndex, 1);
					companyOptionTable.Rows.Add(225, checkBoxfutureCosting.Checked, 1);
					companyOptionTable.Rows.Add(232, checkBoxAllCosting.Checked, 1);
					companyOptionTable.Rows.Add(112, checkBoxPOSSufficientQuantity.Checked, 1);
					companyOptionTable.Rows.Add(222, checkBoxDisplayItemFeatures.Checked, 1);
					companyOptionTable.Rows.Add(223, checkBoxUpdateSalesPersonWhileSave.Checked, 1);
					companyOptionTable.Rows.Add(133, checkBoxUseProjectPhase.Checked, 1);
					companyOptionTable.Rows.Add(141, checkBoxIssueGRNtoProject.Checked, 1);
					companyOptionTable.Rows.Add(192, checkBoxCreateProjectwithSO.Checked, 1);
					companyOptionTable.Rows.Add(206, checkBoxAllowJobChange.Checked, 1);
					companyOptionTable.Rows.Add(207, checkBoxActivateAutoService.Checked, 1);
					companyOptionTable.Rows.Add(164, checkBoxAllowdofollowuponlead.Checked, 1);
					companyOptionTable.Rows.Add(116, textBoxMonthHours.Text.Trim(), 1);
					companyOptionTable.Rows.Add(115, textBoxDayHours.Text.Trim(), 1);
					companyOptionTable.Rows.Add(117, comboBoxDays.SelectedID - 1, 1);
					companyOptionTable.Rows.Add(137, radioButtonDate.Checked, 1);
					companyOptionTable.Rows.Add(199, radioButtonDeductionoonNetDays.Checked, 1);
					companyOptionTable.Rows.Add(123, textBoxCompanyWPSID.Text.Trim(), 1);
					companyOptionTable.Rows.Add(124, comboBoxBank.SelectedID, 1);
					companyOptionTable.Rows.Add(185, textBoxRangeFrom.Text.Trim(), 1);
					companyOptionTable.Rows.Add(186, textBoxRangeTo.Text.Trim(), 1);
					companyOptionTable.Rows.Add(187, textBoxRemarkvalidationpoint.Text.Trim(), 1);
					companyOptionTable.Rows.Add(198m, textBoxTax.Text.Trim(), 1m);
					companyOptionTable.Rows.Add(200, textBoxAutoresumptionDays.Text.Trim(), 1);
					companyOptionTable.Rows.Add(195, radioButtonDaysInMonth.Checked, 1);
					companyOptionTable.Rows.Add(196, radioButtonThirtyDays.Checked, 1);
					companyOptionTable.Rows.Add(197, radioButtonAnnual.Checked, 1);
					companyOptionTable.Rows.Add(140, checkBoxAutoCode.Checked, 1);
					companyOptionTable.Rows.Add(202, checkBoxRoundOffSalaryCalculation.Checked, 1);
					companyOptionTable.Rows.Add(209, checkBoxHRnalaysis.Checked, 1);
					string text = string.Empty;
					if (checkBoxCustomer.Checked)
					{
						text = "C";
					}
					if (checkBoxProduct.Checked)
					{
						text += ",P";
					}
					if (checkBoxVendor.Checked)
					{
						text += ",V";
					}
					if (checkBoxProperty.Checked)
					{
						text += ",Pr";
					}
					if (checkBoxTenant.Checked)
					{
						text += ",T";
					}
					if (checkBoxUnit.Checked)
					{
						text += ",U";
					}
					text = text.TrimStart(',');
					companyOptionTable.Rows.Add(228, text, 1);
					companyOptionTable.Rows.Add(229, comboBoxDefaultTaxOption.SelectedIndex, 1);
					companyOptionTable.Rows.Add(230, comboBoxDefaultTaxGroup.SelectedID, 1);
					companyOptionTable.Rows.Add(143, checkBoxInvMonth1.Checked, 1);
					companyOptionTable.Rows.Add(144, checkBoxInvMonth2.Checked, 1);
					companyOptionTable.Rows.Add(145, checkBoxInvMonth3.Checked, 1);
					companyOptionTable.Rows.Add(146, checkBoxInvMonth4.Checked, 1);
					companyOptionTable.Rows.Add(147, textBoxInvAgingName1.Text, 1);
					companyOptionTable.Rows.Add(148, textBoxInvAgingName2.Text, 1);
					companyOptionTable.Rows.Add(149, textBoxInvAgingName3.Text, 1);
					companyOptionTable.Rows.Add(150, textBoxInvAgingName4.Text, 1);
					companyOptionTable.Rows.Add(151, textBoxInvFromMonth1.Text, 1);
					companyOptionTable.Rows.Add(152, textBoxInvFromMonth2.Text, 1);
					companyOptionTable.Rows.Add(153, textBoxInvFromMonth3.Text, 1);
					companyOptionTable.Rows.Add(154, textBoxInvFromMonth4.Text, 1);
					companyOptionTable.Rows.Add(155, textBoxInvToMonth1.Text, 1);
					companyOptionTable.Rows.Add(156, textBoxInvToMonth2.Text, 1);
					companyOptionTable.Rows.Add(157, textBoxInvToMonth3.Text, 1);
					companyOptionTable.Rows.Add(158, textBoxInvToMonth4.Text, 1);
					companyOptionTable.Rows.Add(211, checkBoxLegalanalaysis.Checked, 1);
					DataRow dataRow = (!isNewRecord) ? currentData.CompanyInformationTable.Rows[0] : currentData.CompanyInformationTable.NewRow();
					dataRow.BeginEdit();
					dataRow["IsTax"] = checkBoxTax.Checked;
					dataRow["TaxPercent"] = textBoxTax.Text;
					dataRow["UseMultiCurrency"] = checkBoxMultiCurrency.Checked;
					dataRow["UseJobCosting"] = checkBoxJobCosting.Checked;
					dataRow["TemplatePathLocation"] = comboBoxTemplatePath.SelectedIndex + 1;
					dataRow["TemplatePathFolder"] = textBoxTemplatePath.Text;
					dataRow["FileSavingPath"] = textFilePath.Text;
					dataRow["TemplatePathServer"] = textBoxServerTemplatePath.Text;
					dataRow["DiscountWriteoffPercent"] = textboxDiscountWriteoffPerc.Text;
					dataRow["IsTax"] = checkBoxTax.Checked;
					companyOptionTable.Rows.Add(234, checkBoxDocumentVersioning.Checked, 1);
					dataRow["DaysInMonth"] = radioButtonDaysInMonth.Checked;
					dataRow["ThirtyDays"] = radioButtonThirtyDays.Checked;
					dataRow["Annual"] = radioButtonAnnual.Checked;
					if (!string.IsNullOrEmpty(textBoxAutoresumptionDays.Text))
					{
						dataRow["AutoResumptionDays"] = textBoxAutoresumptionDays.Text;
					}
					else
					{
						dataRow["AutoResumptionDays"] = 0;
					}
					dataRow["HRAnalysisGroup"] = comboBoxHRAnalysis.SelectedID;
					dataRow["HRAnalysisPrefix"] = textboxHRanalysisPrefix.Text;
					dataRow["MinPriceSaleAction"] = comboBoxMinPriceAction.SelectedID;
					dataRow["OverCLAction"] = comboBoxOverCLAction.SelectedID;
					dataRow["NegativeQuantityAction"] = comboBoxMinusNegativeQuantityAction.SelectedID;
					dataRow["PricelessCostAction"] = comboBoxsaleslessthanCostAction.SelectedID;
					dataRow["RemoveAllocationAction"] = comboBoxoptionsAllocation.SelectedID;
					dataRow["IncludePDC"] = checkBoxIncludePDC.Checked;
					dataRow["AgingByDate"] = radioButtonAgingDate.Checked;
					dataRow["MinPriceSalePass"] = textBoxMinPricePassword.Text;
					dataRow["OverCLPass"] = textBoxOverCLPassword.Text;
					dataRow["NegativeQuantityPass"] = textBoxNegativeQuantityPassword.Text;
					dataRow["PricelessCostPass"] = textBoxPricelessthancostpassword.Text;
					dataRow["PatientAnalysisGroup"] = comboBoxPatientAnalysisGroup.SelectedID;
					dataRow["PatientAnalysisPrefix"] = textboxPatientanalysisPrefix.Text;
					dataRow["MinQtyPackingAction"] = ComboBoxPackingListQtyAction.SelectedID;
					dataRow["ItemPrice1Name"] = textBoxItemPrice1.Text;
					dataRow["ItemPrice2Name"] = textBoxItemPrice2.Text;
					dataRow["ItemPrice3Name"] = textBoxItemPrice3.Text;
					dataRow["VehicleAnalysisGroup"] = comboBoxVehicleAnalysis.SelectedID;
					dataRow["VehicleAnalysisPrefix"] = textboxVehicleanalysisPrefix.Text;
					dataRow["SMSUserName"] = textBoxSMSUserName.Text;
					dataRow["SMSPassword"] = textBoxSMSPassword.Text;
					dataRow["SMSMobileNo"] = textBoxSMSMobileNo.Text;
					dataRow["LotNoIdentity"] = textBoxRenameLotNo.Text;
					dataRow["Reference2"] = textBoxRenameRef2.Text;
					dataRow["TaxEntityTypes"] = text;
					dataRow["DefaultTaxGroupID"] = comboBoxDefaultTaxGroup.SelectedID;
					dataRow["DefaultTaxOption"] = comboBoxDefaultTaxOption.SelectedIndex;
					dataRow["LegalAnalysisGroup"] = comboBoxLegalAnalysis.SelectedID;
					dataRow["LegalAnalysisPrefix"] = textboxLegalanalysisPrefix.Text;
					dataRow.EndEdit();
					if (isNewRecord)
					{
						currentData.CompanyInformationTable.Rows.Add(dataRow);
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

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.CompanyInformationSystem.GetCompanyInformation();
					companyOptionData = (Factory.CompanyOptionSystem.GetCompanyOptionList() as CompanyOptionData);
					if (currentData != null)
					{
						FillData();
						IsNewRecord = false;
						formManager.ResetDirty();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.Tables[0].Rows[0];
			checkBoxLocalAllowNewInGRN.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLocalGRNAddNew, defaultValue: false);
			checkBoxImportAllowNewInGRN.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowImportGRNAddNew, defaultValue: false);
			checkBoxImportAllowNewInPackingList.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowImportGRNPackingListAddNew, defaultValue: false);
			checkBoxLocalAllowGRNWithoutPO.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLocalGRNWithoutPO, defaultValue: false);
			checkBoxImportAllowGRNWithoutPO.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowImportGRNWithoutPO, defaultValue: false);
			checkBoxLocalMoreThanPOQTY.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLocalQtyMoreThanPO, defaultValue: false);
			checkBoxImportMoreThanPOQTY.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowImportQtyMoreThanPO, defaultValue: false);
			checkBoxImportMoreThanPLQTY.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowImportQtyMoreThanPL, defaultValue: false);
			checkBoxLPurchaseLandingCost.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLandingCostInLocalPurchase, defaultValue: true);
			checkBoxAllowtoeditreqdateinPO.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowtoeditPOReqDate, defaultValue: false);
			checkBoxConsiderStockinMRPQ.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ConsiderStockinMRPQ, defaultValue: false);
			radioButtonWeight.Checked = !CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PurchaseLandingCostCalculationMethod, defaultValue: false);
			checkBoxShowLCAmount.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowLandingCostAmountInGrid, defaultValue: false);
			checkBoxPurchaseNegativeQty.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowPurchaseInvoiceNegativeQty, defaultValue: true);
			checkBoxGRNZeroQty.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.LoadZeroQuantityinGRN, defaultValue: false);
			checkBoxConsignInFIFO.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ConsignInFIFO, defaultValue: true);
			checkBoxTrackConsignInDetail.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TrackConsignInDetailedSales, defaultValue: true);
			checkBoxTrackConsignInExpenses.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TrackConsignInExpenses, defaultValue: false);
			checkBoxPurchaseOrderChangePrice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowPurchaseInvoiceChangePrice, defaultValue: false);
			checkBoxOrderandShipment.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowOrderAndShipmentDetailInGRN, defaultValue: false);
			checkBoxManadatoryPO.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.MandatoryPOBOL, defaultValue: false);
			if (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PDCByMaturity, defaultValue: true))
			{
				radioButtonPDCByMaturity.Checked = true;
			}
			else
			{
				radioButtonPDCByTransaction.Checked = true;
			}
			if (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PDCIssuedByMaturity, defaultValue: false))
			{
				radioButtonPDCIssuedByMaturity.Checked = true;
			}
			else
			{
				radioButtonPDCIssuedByTransaction.Checked = true;
			}
			if (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PDCDirectMaturity, defaultValue: true))
			{
				radioButtonDirectMaturity.Checked = true;
			}
			else
			{
				radioButtonIndirectMaturity.Checked = true;
			}
			if (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OTBasedOn, defaultValue: true))
			{
				radioButtonDate.Checked = true;
			}
			else
			{
				radioButtonEmployee.Checked = true;
			}
			if (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DirectTREntry, defaultValue: true))
			{
				radioButtonDirectTREntry.Checked = true;
			}
			else
			{
				radioButtonTRAppTREntry.Checked = true;
			}
			if (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DeductiononNetDays, defaultValue: true))
			{
				radioButtonDeductionoonNetDays.Checked = true;
			}
			else
			{
				radioButtondeductiononNetDaysSal.Checked = true;
			}
			checkBoxCostCenter.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.IsCostCenterMandatory, defaultValue: false);
			checkBoxAllocationForm.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAllocationForm, defaultValue: true);
			checkBoxAllowChangeChequePayee.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowChangeChequePrintPayee, defaultValue: true);
			checkBoxFinancialTransactionPosting.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.FinancialTransactionPosting, defaultValue: false);
			checkBoxDirectChequeReturn.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DirectChequeReturn, defaultValue: false);
			int num = 0;
			num = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.SoftClosePeriod, 0);
			comboBoxSoftClosePeriod.SelectedIndex = num;
			int num2 = 0;
			num2 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.LocalPurchaseFlow, 0);
			comboBoxLocalPurchaseFlow.SelectedIndex = num2;
			CheckBox checkBox = checkBoxLocalAllowGRNWithoutPO;
			CheckBox checkBox2 = checkBoxLocalAllowNewInGRN;
			bool flag2 = checkBoxLocalMoreThanPOQTY.Enabled = (num2 == 3 || num2 == 1);
			bool enabled = checkBox2.Enabled = flag2;
			checkBox.Enabled = enabled;
			int num3 = 0;
			num3 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ImportPurchaseFlow, 0);
			comboBoxImportPurchaseFlow.SelectedIndex = num3;
			CheckBox checkBox3 = checkBoxImportAllowGRNWithoutPO;
			CheckBox checkBox4 = checkBoxImportAllowNewInGRN;
			CheckBox checkBox5 = checkBoxImportAllowNewInPackingList;
			CheckBox checkBox6 = checkBoxImportMoreThanPOQTY;
			bool flag5 = checkBoxImportMoreThanPLQTY.Enabled = (num3 == 3 || num3 == 1);
			bool flag7 = checkBox6.Enabled = flag5;
			flag2 = (checkBox5.Enabled = flag7);
			enabled = (checkBox4.Enabled = flag2);
			checkBox3.Enabled = enabled;
			checkBoxAllowPReturnWithoutInvoice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowPurchaseReturnWithoutInvoice, defaultValue: false);
			checkBoxAllowPReturnChangePrice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowChangePriceInPurchaseReturn, defaultValue: false);
			checkBoxLSAllowAddNew.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLSAddNew, defaultValue: false);
			checkBoxLSAllowInvoiceMore.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLSQtyMoreThanSO, defaultValue: false);
			checkBoxLSAllowWithoutSO.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLSWithoutSO, defaultValue: true);
			checkBoxESAllowAddNew.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowESAddNew, defaultValue: false);
			checkBoxESAllowMoreQTY.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowESQtyMoreThanSO, defaultValue: false);
			checkBoxLSAllowReturnChangePrice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowChangePriceInSalesReturn, defaultValue: false);
			checkBoxESAllowWithoutSO.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowESWithoutSO, defaultValue: true);
			checkBoxESAllowtocreatepicklist.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowESCreatefromPickList, defaultValue: false);
			checkBoxAllowLSReturnWithoutInvoice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLSReturnWithoutInvoice, defaultValue: false);
			checkBoxLSAllowDNWithoutInvoice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowLSDNoteWithoutInvoice, defaultValue: false);
			checkBoxESAllowDNWithoutInvoice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowESDNoteWithoutInvoice, defaultValue: false);
			checkBoxTrackConsignOutItemsSale.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TrackConsignOutDetailedSales, defaultValue: true);
			checkBoxInvoiceNegativeQty.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowSalesInvoiceNegativeQty, defaultValue: true);
			checkBoxAllowCustomerChangeInDN.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowCustomerChangeInDN, defaultValue: false);
			checkBoxExcludeZeroQtyInDN.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ExcludeZeroQtyInDN, defaultValue: false);
			checkBoxEnableTempSaving.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.EnableTempSaving, defaultValue: false);
			checkBoxB2Cbased.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.BasedonB2C, defaultValue: false);
			checkBoxMaterialReservationOnSo.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.MaterialReservationOnSo, defaultValue: false);
			checkBoxDisableCustomerCreditLimit.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DisableCustomerCreditLimit, defaultValue: false);
			if (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingByDueDate, defaultValue: true))
			{
				radioButtonAgingDueDate.Checked = true;
			}
			else
			{
				radioButtonAgingDate.Checked = true;
			}
			if (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingByEOMDueDate, defaultValue: false))
			{
				radioButtonAgingEOMDueDate.Checked = true;
			}
			else
			{
				radioButtonAgingEOMDueDate.Checked = false;
			}
			if (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingByEOMInvoiceDate, defaultValue: false))
			{
				radioButtonEOMInvoiceDate.Checked = true;
			}
			else
			{
				radioButtonEOMInvoiceDate.Checked = false;
			}
			checkBoxPOSSufficientQuantity.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.POSCheckSufficientQty, defaultValue: true);
			checkBoxDisplayItemFeatures.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.POSDisplayItemFeatures, defaultValue: false);
			checkBoxUpdateSalesPersonWhileSave.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.POSChangeSalesPersonWhileSaving, defaultValue: false);
			checkBoxUseProjectPhase.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.UseProjectPhase, defaultValue: false);
			checkBoxIssueGRNtoProject.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowIssueGRNtoProject, defaultValue: false);
			checkBoxCreateProjectwithSO.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowtoCreateProjectwithSO, defaultValue: false);
			checkBoxAllowJobChange.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowJobChangeInMRPQ, defaultValue: true);
			checkBoxActivateAutoService.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivateAutoservice, defaultValue: false);
			checkBoxAllowdofollowuponlead.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowDoFollowUponLead, defaultValue: false);
			checkBoxMonth1.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging1, defaultValue: true);
			checkBoxMonth2.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging2, defaultValue: true);
			checkBoxMonth3.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging3, defaultValue: true);
			checkBoxMonth4.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging4, defaultValue: true);
			checkBoxMonth5.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging5, defaultValue: true);
			checkBoxMonth6.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging6, defaultValue: true);
			textBoxAgingName0.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName0, "Current");
			textBoxAgingName1.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName1, "1 - 30 Days");
			textBoxAgingName2.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName2, "31 - 60 Days");
			textBoxAgingName3.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName3, "61 - 90 Days");
			textBoxAgingName4.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName4, "91 - 120 Days");
			textBoxAgingName5.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName5, "121 - 150 Days");
			textBoxAgingName6.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName6, "151 - 180 Days");
			textBoxFromMonth0.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom0, "0");
			textBoxFromMonth1.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom1, "1");
			textBoxFromMonth2.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom2, "31");
			textBoxFromMonth3.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom3, "61");
			textBoxFromMonth4.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom4, "91");
			textBoxFromMonth5.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom5, "121");
			textBoxFromMonth6.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom6, "151");
			textBoxToMonth0.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo0, "0");
			textBoxToMonth1.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo1, "30");
			textBoxToMonth2.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo2, "60");
			textBoxToMonth3.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo3, "90");
			textBoxToMonth4.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo4, "120");
			textBoxToMonth5.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo5, "150");
			textBoxToMonth6.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo6, "180");
			cusRedFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.CusFlagRed, "Red");
			cusBlueFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.CusFlagBlue, "Blue");
			cusOrangeFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.CusFlagOrange, "Orange");
			cusGreenFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.CusFlagGreen, "Green");
			cusYellowFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.CusFlagYellow, "Yellow");
			cusPurpleFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.CusFlagPurple, "Purple");
			int num4 = 0;
			num4 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.LocalSalesFlow, 0);
			comboBoxLocalSalesFlow.SelectedIndex = num4;
			CheckBox checkBox7 = checkBoxLSAllowAddNew;
			CheckBox checkBox8 = checkBoxLSAllowInvoiceMore;
			flag2 = (checkBoxLSAllowWithoutSO.Enabled = (num4 == 2 || num4 == 1));
			enabled = (checkBox8.Enabled = flag2);
			checkBox7.Enabled = enabled;
			int num5 = 0;
			num5 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ExportSalesFlow, 0);
			comboBoxExportSalesFlow.SelectedIndex = num5;
			CheckBox checkBox9 = checkBoxESAllowAddNew;
			CheckBox checkBox10 = checkBoxESAllowMoreQTY;
			flag2 = (checkBoxESAllowWithoutSO.Enabled = (num5 == 2 || num5 == 1));
			enabled = (checkBox10.Enabled = flag2);
			checkBox9.Enabled = enabled;
			checkBoxESAllowtocreatepicklist.Enabled = (num5 == 2);
			checkBoxInlineDiscount.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowInlineDiscount, defaultValue: false);
			checkBoxShowLotdetailinPrintout.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowLotdetailinPrintout, defaultValue: false);
			checkBoxDeliveryNoteCL.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.CheckCLOnDeliveryNote, defaultValue: false);
			checkBoxNotallowzeroinSales.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowzeroinSales, defaultValue: false);
			checkBoxShowItemdetail.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemDetail, defaultValue: false);
			checkBoxTakelastSalesprice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TakeLastSalesPrice, defaultValue: false);
			checkBoxAllowcreditsaleinSalesReceipt.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AllowCreditSaleInSalesReceipt, defaultValue: false);
			checkBoxOnlyOpenInvoice.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowOnlytOpenInvoices, defaultValue: false);
			if (CompanyOptions.GetCompanyOption(CompanyOptionsEnum.MatrixDescriptionGenerationMethod, 2) == 1)
			{
				radioButtonMatrixDescOnly.Checked = true;
			}
			else
			{
				radioButtonMatrixDescAttribute.Checked = true;
			}
			textBoxAttribute1Name.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.Attribute1Name, "Attribute 1");
			textBoxAttribute2Name.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.Attribute2Name, "Attribute 2");
			textBoxAttribute3Name.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.Attribute3Name, "Attribute 3");
			textBoxPType1Name.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType1Name, "P.Type 1");
			textBoxPType2Name.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType2Name, "P.Type 2");
			textBoxPType3Name.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType3Name, "P.Type 3");
			textBoxPType4Name.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType4Name, "P.Type 4");
			textBoxPType5Name.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType5Name, "P.Type 5");
			textBoxPType6Name.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType6Name, "P.Type 6");
			textBoxPType7Name.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType7Name, "P.Type 7");
			textBoxPType8Name.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ProductType8Name, "P.Type 8");
			textBoxRefSlNo.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefSlNo, "");
			textBoxRefText1.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefText1, "");
			textBoxRefText2.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefText2, "");
			textBoxRefNum1.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefNum1, "");
			textBoxRefNum2.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefNum2, "");
			textBoxRefDate1.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefDate1, "");
			textBoxRefDate2.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RefDate2, "");
			txtBoxSpecificationName.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.SpecificationName, "Specification ID");
			checkBoxShowQuantityInProductCombo.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemQuantityInCombo, defaultValue: false);
			checkBoxShowUnitInProductCombo.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemUnitInCombo, defaultValue: false);
			checkBoxShowCostInProductCombo.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemCostInCombo, defaultValue: false);
			checkBoxShowUPCInProductCombo.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemUPCInCombo, defaultValue: false);
			checkBoxShowitemFeatures.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowItemFeatures, defaultValue: false);
			checkBoxShowBOLinPL.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowBOLListinPackingList, defaultValue: false);
			textBoxDayHours.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TotalWorkingDayHours, "8");
			textBoxMonthHours.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TotalWorkingMonthHours, "270");
			textBoxRangeFrom.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AppraisalPointFrom, "1");
			textBoxRangeTo.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AppraisalPointTo, "");
			textBoxRemarkvalidationpoint.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ReamrkValidationPoint, "");
			textBoxTax.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TaxPercentValue, "0.00").ToString();
			textBoxAutoresumptionDays.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AutoResumptionDays, "0").ToString();
			textboxDiscountWriteoffPerc.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DiscountWriteoffPercent, "1.00").ToString();
			checked
			{
				comboBoxDays.SelectedID = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OffDay, 7) + 1;
				checkBoxLoadDescFromPriceList.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.LoadItemDescFromPriceList, defaultValue: false);
				checkBoxenablecostondelete.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.Enablecostingondelete, defaultValue: true);
				checkBoxActiveBinField.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivateBinField, defaultValue: false);
				textBoxCompanyWPSID.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.CompanyWPSID, "");
				comboBoxBank.SelectedID = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.BankID, "");
				checkBoxShowMultiDimension.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowMultidimensionOnGrid, defaultValue: false);
				checkBoxVehicleAnlysis.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.EnableVehicleAnalysis, defaultValue: false);
				if (checkBoxVehicleAnlysis.Checked)
				{
					panelVehicleanalysis.Enabled = true;
				}
				else
				{
					panelVehicleanalysis.Enabled = false;
				}
				comboBoxVehicleAnalysis.SelectedID = dataRow["VehicleAnalysisGroup"].ToString();
				textboxVehicleanalysisPrefix.Text = dataRow["VehicleAnalysisPrefix"].ToString();
				checkBoxActivatePartsDetails.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivatePartsDetails, defaultValue: false);
				checkBoxfutureCosting.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.FutureCosting, defaultValue: false);
				checkBoxAllCosting.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.EnableCostRunning, defaultValue: false);
				textBoxDescription3.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.OtherDescription, "Description");
				int num6 = 0;
				num6 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ItemCodeCreationBasedOn, 0);
				comboBoxItemCreationOption.SelectedIndex = num6;
				checkBoxAutoCode.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AutoCardCode, defaultValue: false);
				checkBoxDocumentVersioning.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DocumentVersioning, defaultValue: false);
				checkBoxShowMultiDimension.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowMultidimensionOnGrid, defaultValue: false);
				string[] array = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.TaxEntityTypes, "").Trim().Split(',');
				foreach (string a in array)
				{
					if (a == "C")
					{
						checkBoxCustomer.Checked = true;
					}
					if (a == "P")
					{
						checkBoxProduct.Checked = true;
					}
					if (a == "V")
					{
						checkBoxVendor.Checked = true;
					}
					if (a == "Pr")
					{
						checkBoxProperty.Checked = true;
					}
					if (a == "T")
					{
						checkBoxTenant.Checked = true;
					}
					if (a == "U")
					{
						checkBoxUnit.Checked = true;
					}
				}
				comboBoxDefaultTaxOption.SelectedIndex = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DefaultTaxOption, 0);
				comboBoxDefaultTaxGroup.SelectedID = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DefaultTaxGroup, "");
				checkBoxInvMonth1.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowInvAging1, defaultValue: true);
				checkBoxInvMonth2.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowInvAging2, defaultValue: true);
				checkBoxInvMonth3.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowInvAging3, defaultValue: true);
				checkBoxInvMonth4.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowInvAging4, defaultValue: true);
				textBoxAgingName1.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvName1, "0 - 30 Days");
				textBoxAgingName2.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvName2, "31 - 60 Days");
				textBoxAgingName3.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvName3, "61 - 90 Days");
				textBoxAgingName4.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvName4, "91- 120 Days");
				textBoxInvFromMonth1.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvFrom1, "0");
				textBoxInvFromMonth2.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvFrom2, "31");
				textBoxInvFromMonth3.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvFrom3, "61");
				textBoxInvFromMonth4.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvFrom4, "91");
				textBoxInvToMonth1.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvTo1, "30");
				textBoxInvToMonth2.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvTo2, "60");
				textBoxInvToMonth3.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvTo3, "90");
				textBoxInvToMonth4.Text = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvTo4, "120");
				if (dataRow["IsTax"] != DBNull.Value)
				{
					checkBoxTax.Checked = bool.Parse(dataRow["IsTax"].ToString());
					string text = "0.00";
					if (dataRow["TaxPercent"].ToString() != "" && dataRow["TaxPercent"].ToString() != string.Empty)
					{
						textBoxTax.Text = dataRow["TaxPercent"].ToString();
					}
					else
					{
						textBoxTax.Text = text;
					}
				}
				else
				{
					checkBoxTax.Checked = false;
				}
				if (dataRow["DiscountWriteoffPercent"] != DBNull.Value)
				{
					textboxDiscountWriteoffPerc.Text = dataRow["DiscountWriteoffPercent"].ToString();
				}
				if (checkBoxTax.Checked)
				{
					textBoxTax.ReadOnly = false;
				}
				else
				{
					textBoxTax.ReadOnly = true;
				}
				if (dataRow["UseMultiCurrency"] != DBNull.Value)
				{
					checkBoxMultiCurrency.Checked = bool.Parse(dataRow["UseMultiCurrency"].ToString());
				}
				else
				{
					checkBoxMultiCurrency.Checked = false;
				}
				if (dataRow["UseJobCosting"] != DBNull.Value)
				{
					checkBoxJobCosting.Checked = bool.Parse(dataRow["UseJobCosting"].ToString());
				}
				else
				{
					checkBoxJobCosting.Checked = false;
				}
				textFilePath.Text = dataRow["FileSavingPath"].ToString();
				if (dataRow["TemplatePathLocation"] != DBNull.Value)
				{
					comboBoxTemplatePath.SelectedIndex = int.Parse(dataRow["TemplatePathLocation"].ToString()) - 1;
				}
				else
				{
					comboBoxTemplatePath.SelectedIndex = 0;
				}
				if (dataRow["TemplatePathFolder"] != DBNull.Value)
				{
					textBoxTemplatePath.Text = dataRow["TemplatePathFolder"].ToString();
				}
				else
				{
					textBoxTemplatePath.Text = "\\Print Templates";
				}
				textBoxServerTemplatePath.Text = dataRow["TemplatePathServer"].ToString();
				textBoxAutoresumptionDays.Text = dataRow["AutoResumptionDays"].ToString();
				if (dataRow["MinPriceSaleAction"] != DBNull.Value)
				{
					comboBoxMinPriceAction.SelectedID = int.Parse(dataRow["MinPriceSaleAction"].ToString());
				}
				else
				{
					comboBoxMinPriceAction.SelectedID = 2;
				}
				if (dataRow["OverCLAction"] != DBNull.Value)
				{
					comboBoxOverCLAction.SelectedID = int.Parse(dataRow["OverCLAction"].ToString());
				}
				else
				{
					comboBoxOverCLAction.SelectedID = 2;
				}
				if (dataRow["NegativeQuantityAction"] != DBNull.Value)
				{
					comboBoxMinusNegativeQuantityAction.SelectedID = int.Parse(dataRow["NegativeQuantityAction"].ToString());
				}
				else
				{
					comboBoxMinusNegativeQuantityAction.SelectedID = 2;
				}
				if (dataRow["PricelessCostAction"] != DBNull.Value)
				{
					comboBoxsaleslessthanCostAction.SelectedID = int.Parse(dataRow["PricelessCostAction"].ToString());
				}
				else
				{
					comboBoxsaleslessthanCostAction.SelectedID = 2;
				}
				if (dataRow["IncludePDC"] != DBNull.Value)
				{
					checkBoxIncludePDC.Checked = bool.Parse(dataRow["IncludePDC"].ToString());
				}
				else
				{
					checkBoxIncludePDC.Checked = true;
				}
				if (dataRow["RemoveAllocationAction"] != DBNull.Value)
				{
					comboBoxoptionsAllocation.SelectedID = int.Parse(dataRow["RemoveAllocationAction"].ToString());
				}
				else
				{
					comboBoxoptionsAllocation.SelectedID = 1;
				}
				textBoxMinPricePassword.Text = dataRow["MinPriceSalePass"].ToString();
				textBoxOverCLPassword.Text = dataRow["OverCLPass"].ToString();
				textBoxNegativeQuantityPassword.Text = dataRow["NegativeQuantityPass"].ToString();
				textBoxPricelessthancostpassword.Text = dataRow["PricelessCostPass"].ToString();
				checkBoxPatientAnalysis.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.EnablePatientAnalysis, defaultValue: false);
				comboBoxPatientAnalysisGroup.SelectedID = dataRow["PatientAnalysisGroup"].ToString();
				textboxPatientanalysisPrefix.Text = dataRow["PatientAnalysisPrefix"].ToString();
				if (dataRow["MinQtyPackingAction"] != DBNull.Value)
				{
					ComboBoxPackingListQtyAction.SelectedID = int.Parse(dataRow["MinQtyPackingAction"].ToString());
				}
				else
				{
					ComboBoxPackingListQtyAction.SelectedID = 3;
				}
				venRedFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.VenFlagRed, "Red");
				venBlueFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.VenFlagBlue, "Blue");
				venOrangeFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.VenFlagOrange, "Orange");
				venGreenFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.VenFlagGreen, "Green");
				venYellowFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.VenFlagYellow, "Yellow");
				venPurpleFlag = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.VenFlagPurple, "Purple");
				textBoxItemPrice1.Text = dataRow["ItemPrice1Name"].ToString();
				textBoxItemPrice2.Text = dataRow["ItemPrice2Name"].ToString();
				textBoxItemPrice3.Text = dataRow["ItemPrice3Name"].ToString();
				if (dataRow["DaysInMonth"] != DBNull.Value)
				{
					radioButtonDaysInMonth.Checked = bool.Parse(dataRow["DaysInMonth"].ToString());
				}
				else
				{
					radioButtonDaysInMonth.Checked = true;
				}
				if (dataRow["ThirtyDays"] != DBNull.Value)
				{
					radioButtonThirtyDays.Checked = bool.Parse(dataRow["ThirtyDays"].ToString());
				}
				else
				{
					radioButtonThirtyDays.Checked = false;
				}
				if (dataRow["Annual"] != DBNull.Value)
				{
					radioButtonAnnual.Checked = bool.Parse(dataRow["Annual"].ToString());
				}
				else
				{
					radioButtonAnnual.Checked = false;
				}
				checkBoxRoundOffSalaryCalculation.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.RoundOffSalaryCalculation, defaultValue: true);
				checkBoxHRnalaysis.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.EnableHRAnalysis, defaultValue: false);
				comboBoxHRAnalysis.SelectedID = dataRow["HRAnalysisGroup"].ToString();
				textboxHRanalysisPrefix.Text = dataRow["HRAnalysisPrefix"].ToString();
				if (checkBoxHRnalaysis.Checked)
				{
					panelHRanalysis.Enabled = true;
				}
				else
				{
					panelHRanalysis.Enabled = false;
				}
				textBoxSMSUserName.Text = dataRow["SMSUserName"].ToString();
				textBoxSMSPassword.Text = dataRow["SMSPassword"].ToString();
				textBoxSMSMobileNo.Text = dataRow["SMSMobileNo"].ToString();
				textBoxRenameLotNo.Text = dataRow["LotNoIdentity"].ToString();
				textBoxRenameRef2.Text = dataRow["Reference2"].ToString();
				checkBoxLegalanalaysis.Checked = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.EnableLegalAnalysis, defaultValue: false);
				if (checkBoxLegalanalaysis.Checked)
				{
					panelLegalAnalysis.Enabled = true;
				}
				else
				{
					panelLegalAnalysis.Enabled = false;
				}
				comboBoxLegalAnalysis.SelectedID = dataRow["LegalAnalysisGroup"].ToString();
				textboxLegalanalysisPrefix.Text = dataRow["LegalAnalysisPrefix"].ToString();
			}
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				Close();
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
				bool flag = Factory.CompanyInformationSystem.UpdateCompanyOptions(currentData);
				flag &= Factory.CompanyOptionSystem.CreateCompanyOption(companyOptionData, 1);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					CompanyPreferences.LoadCompanyPreferences();
					CompanyOptions.LoadCompanyOptions();
					ErrorHelper.InformationMessage("Some changes may need to restart the application in order to take effect.");
					ClearForm();
					Close();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (!screenRight.Edit)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
				return false;
			}
			return true;
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

		private void ClearForm()
		{
			formManager.ResetDirty();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					if (!GlobalRules.IsFeatureAllowed(EditionFeatures.MultiCurrency))
					{
						checkBoxMultiCurrency.Enabled = false;
					}
					IsNewRecord = true;
					comboBoxMinPriceAction.LoadData();
					ComboBoxPackingListQtyAction.LoadData();
					comboBoxDays.LoadData();
					comboBoxsaleslessthanCostAction.LoadData();
					comboBoxoptionsAllocation.SelectedIndex = 1;
					expandableGroupBoxTypesName.Expanded = false;
					ClearForm();
					LoadData("1");
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

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
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

		private void optionsAllowComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxMinPriceAction.SelectedID == 4)
			{
				panelMinPricePassword.Visible = true;
			}
			else
			{
				panelMinPricePassword.Visible = false;
			}
		}

		private void comboBoxOverCLAction_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxOverCLAction.SelectedID == 4)
			{
				panelOverCLPassword.Visible = true;
			}
			else
			{
				panelOverCLPassword.Visible = false;
			}
		}

		private void comboBoxMinusNegativeQuantityAction_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxMinusNegativeQuantityAction.SelectedID == 4)
			{
				panelNegativeQuantityPassword.Visible = true;
			}
			else
			{
				panelNegativeQuantityPassword.Visible = false;
			}
		}

		private void comboBoxsaleslessthanCostAction_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxsaleslessthanCostAction.SelectedID == 4)
			{
				panelsalespricelessthancost.Visible = true;
			}
			else
			{
				panelsalespricelessthancost.Visible = false;
			}
		}

		private void radioButtonAgingDate_CheckedChanged(object sender, EventArgs e)
		{
			NumberTextBox numberTextBox = textBoxFromMonth0;
			bool readOnly = textBoxToMonth0.ReadOnly = false;
			numberTextBox.ReadOnly = readOnly;
		}

		private void radioButtonAgingDueDate_CheckedChanged(object sender, EventArgs e)
		{
			textBoxFromMonth0.Text = "0";
			textBoxToMonth0.Text = "0";
			NumberTextBox numberTextBox = textBoxFromMonth0;
			bool readOnly = textBoxToMonth0.ReadOnly = true;
			numberTextBox.ReadOnly = readOnly;
		}

		private void checkBoxMonth6_CheckedChanged(object sender, EventArgs e)
		{
			panelMonth6.Enabled = checkBoxMonth6.Checked;
			if (checkBoxMonth6.Checked)
			{
				checkBoxMonth5.Checked = true;
			}
		}

		private void checkBoxMonth5_CheckedChanged(object sender, EventArgs e)
		{
			panelMonth5.Enabled = checkBoxMonth5.Checked;
			if (!checkBoxMonth5.Checked)
			{
				checkBoxMonth6.Checked = false;
			}
			else
			{
				checkBoxMonth4.Checked = true;
			}
		}

		private void checkBoxMonth4_CheckedChanged(object sender, EventArgs e)
		{
			panelMonth4.Enabled = checkBoxMonth4.Checked;
			if (!checkBoxMonth4.Checked)
			{
				checkBoxMonth5.Checked = false;
			}
			else
			{
				checkBoxMonth3.Checked = true;
			}
		}

		private void checkBoxMonth3_CheckedChanged(object sender, EventArgs e)
		{
			panelMonth3.Enabled = checkBoxMonth3.Checked;
			if (!checkBoxMonth3.Checked)
			{
				checkBoxMonth4.Checked = false;
			}
			else
			{
				checkBoxMonth2.Checked = true;
			}
		}

		private void checkBoxMonth2_CheckedChanged(object sender, EventArgs e)
		{
			panelMonth2.Enabled = checkBoxMonth2.Checked;
			if (!checkBoxMonth2.Checked)
			{
				checkBoxMonth3.Checked = false;
			}
			else
			{
				checkBoxMonth1.Checked = true;
			}
		}

		private void checkBoxMonth1_CheckedChanged(object sender, EventArgs e)
		{
			panelMonth1.Enabled = checkBoxMonth1.Checked;
			if (!checkBoxMonth1.Checked)
			{
				checkBoxMonth2.Checked = false;
			}
		}

		private void buttonOpenFileDialog_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				textFilePath.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void buttonSelectTemplatePath_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			string text = "";
			text = ((comboBoxTemplatePath.SelectedIndex != 0) ? Factory.DatabaseSystem.GetServerPath() : Path.GetDirectoryName(Application.ExecutablePath));
			if (textBoxTemplatePath.Text != "" && Directory.Exists(text + textBoxTemplatePath.Text))
			{
				folderBrowserDialog.SelectedPath = text + textBoxTemplatePath.Text;
			}
			else
			{
				folderBrowserDialog.SelectedPath = text;
			}
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				string selectedPath = folderBrowserDialog.SelectedPath;
				if (!selectedPath.Contains(text))
				{
					ErrorHelper.WarningMessage("You must select a path in the installation directory of Client or Server.");
				}
				else
				{
					textBoxTemplatePath.Text = selectedPath.Replace(text, "");
				}
			}
		}

		private void btnOpenFileDialog2_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxServerTemplatePath.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void buttonCustomerFlagNames_Click(object sender, EventArgs e)
		{
			FlagNameDialog flagNameDialog = new FlagNameDialog();
			flagNameDialog.PurpleName = cusPurpleFlag;
			flagNameDialog.RedName = cusRedFlag;
			flagNameDialog.OrangeName = cusOrangeFlag;
			flagNameDialog.YellowName = cusYellowFlag;
			flagNameDialog.GreenName = cusGreenFlag;
			flagNameDialog.BlueName = cusBlueFlag;
			if (flagNameDialog.ShowDialog() == DialogResult.OK)
			{
				cusPurpleFlag = flagNameDialog.PurpleName;
				cusRedFlag = flagNameDialog.RedName;
				cusOrangeFlag = flagNameDialog.OrangeName;
				cusYellowFlag = flagNameDialog.YellowName;
				cusGreenFlag = flagNameDialog.GreenName;
				cusBlueFlag = flagNameDialog.BlueName;
				formManager.IsForcedDirty = true;
			}
		}

		private void buttonVenFlags_Click(object sender, EventArgs e)
		{
			FlagNameDialog flagNameDialog = new FlagNameDialog();
			flagNameDialog.PurpleName = venPurpleFlag;
			flagNameDialog.RedName = venRedFlag;
			flagNameDialog.OrangeName = venOrangeFlag;
			flagNameDialog.YellowName = venYellowFlag;
			flagNameDialog.GreenName = venGreenFlag;
			flagNameDialog.BlueName = venBlueFlag;
			if (flagNameDialog.ShowDialog() == DialogResult.OK)
			{
				venPurpleFlag = flagNameDialog.PurpleName;
				venRedFlag = flagNameDialog.RedName;
				venOrangeFlag = flagNameDialog.OrangeName;
				venYellowFlag = flagNameDialog.YellowName;
				venGreenFlag = flagNameDialog.GreenName;
				venBlueFlag = flagNameDialog.BlueName;
				formManager.IsForcedDirty = true;
			}
		}

		private void buttonItmFlags_Click(object sender, EventArgs e)
		{
			FlagNameDialog flagNameDialog = new FlagNameDialog();
			flagNameDialog.PurpleName = itmPurpleFlag;
			flagNameDialog.RedName = itmRedFlag;
			flagNameDialog.OrangeName = itmOrangeFlag;
			flagNameDialog.YellowName = itmYellowFlag;
			flagNameDialog.GreenName = itmGreenFlag;
			flagNameDialog.BlueName = itmBlueFlag;
			if (flagNameDialog.ShowDialog() == DialogResult.OK)
			{
				itmPurpleFlag = flagNameDialog.PurpleName;
				itmRedFlag = flagNameDialog.RedName;
				itmOrangeFlag = flagNameDialog.OrangeName;
				itmYellowFlag = flagNameDialog.YellowName;
				itmGreenFlag = flagNameDialog.GreenName;
				itmBlueFlag = flagNameDialog.BlueName;
				formManager.IsForcedDirty = true;
			}
		}

		private void ultraTabControl1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void checkBoxShowMultiDimension_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void checkBoxActivatePartsDetails_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void groupBoxLotDetails_Enter(object sender, EventArgs e)
		{
		}

		private void groupBox21_Enter(object sender, EventArgs e)
		{
		}

		private void groupBox16_Enter(object sender, EventArgs e)
		{
		}

		private void groupBox31_Enter(object sender, EventArgs e)
		{
		}

		private void mmLabel53_Click(object sender, EventArgs e)
		{
		}

		private void comboBoxItemCreationOption_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxDefaultTaxOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDefaultTaxOption.SelectedIndex == 0)
			{
				comboBoxDefaultTaxGroup.ReadOnly = false;
				return;
			}
			comboBoxDefaultTaxGroup.ReadOnly = true;
			comboBoxDefaultTaxGroup.Clear();
		}

		private void ultraExpandableGroupBox1_ExpandedStateChanged(object sender, EventArgs e)
		{
			if (expandableGroupBoxTypesName.Expanded)
			{
				base.Height = 864;
				groupBox36.Location = new Point(13, 398);
				groupBox20.Location = new Point(13, 455);
				groupBox35.Location = new Point(13, 587);
				groupBox3.Location = new Point(13, 649);
				ultraExpandableGroupBoxGridFields.Location = new Point(13, 749);
			}
			else
			{
				base.Height = 740;
				groupBox36.Location = new Point(13, 224);
				groupBox20.Location = new Point(13, 281);
				groupBox35.Location = new Point(13, 413);
				groupBox3.Location = new Point(13, 475);
				ultraExpandableGroupBoxGridFields.Location = new Point(13, 576);
			}
		}

		private void ultraExpandableGroupBoxGridFields_ExpandedStateChanged(object sender, EventArgs e)
		{
			if (expandableGroupBoxTypesName.Expanded)
			{
				AutoScroll = true;
				return;
			}
			base.Height = 740;
			AutoScroll = false;
		}

		private void checkBoxPatientAnalysis_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxPatientAnalysis.Checked)
			{
				panelPatientanalysis.Enabled = true;
				return;
			}
			panelPatientanalysis.Enabled = false;
			comboBoxPatientAnalysisGroup.Clear();
			textboxPatientanalysisPrefix.Clear();
		}

		private void ultraPictureBoxInformation_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				DocumentVersionList documentVersionList = new DocumentVersionList();
				DataSet dataSet = new DataSet();
				dataSet = Factory.CompanyOptionSystem.GetCompanyOptionList();
				documentVersionList.LoadData(dataSet, ScreenTypes.Setup, 0, "", "CompanyOptionsForm");
				documentVersionList.ShowDialog();
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.CompanyOptionsForm));
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab8 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab9 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab10 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab11 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab12 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab13 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraPictureBoxInformation = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
			checkBoxDocumentVersioning = new System.Windows.Forms.CheckBox();
			checkBoxAutoCode = new System.Windows.Forms.CheckBox();
			groupBox18 = new System.Windows.Forms.GroupBox();
			textBoxServerTemplatePath = new Micromind.UISupport.MMTextBox();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			btnOpenFileDialog2 = new Micromind.UISupport.XPButton();
			groupBox13 = new System.Windows.Forms.GroupBox();
			comboBoxTemplatePath = new System.Windows.Forms.ComboBox();
			textBoxTemplatePath = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			buttonSelectTemplatePath = new Micromind.UISupport.XPButton();
			groupBox7 = new System.Windows.Forms.GroupBox();
			textFilePath = new Micromind.UISupport.MMTextBox();
			labelFileSavingPath = new Micromind.UISupport.MMLabel();
			buttonOpenFileDialog = new Micromind.UISupport.XPButton();
			checkBoxJobCosting = new System.Windows.Forms.CheckBox();
			checkBoxMultiCurrency = new System.Windows.Forms.CheckBox();
			checkBoxTax = new System.Windows.Forms.CheckBox();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxSoftClosePeriod = new System.Windows.Forms.ComboBox();
			mmLabel82 = new Micromind.UISupport.MMLabel();
			checkBoxDirectChequeReturn = new System.Windows.Forms.CheckBox();
			groupBox37 = new System.Windows.Forms.GroupBox();
			radioButtonDirectTREntry = new System.Windows.Forms.RadioButton();
			radioButtonTRAppTREntry = new System.Windows.Forms.RadioButton();
			checkBoxFinancialTransactionPosting = new System.Windows.Forms.CheckBox();
			groupBox22 = new System.Windows.Forms.GroupBox();
			radioButtonPDCIssuedByMaturity = new System.Windows.Forms.RadioButton();
			radioButtonPDCIssuedByTransaction = new System.Windows.Forms.RadioButton();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			checkBoxAllowChangeChequePayee = new System.Windows.Forms.CheckBox();
			checkBoxAllocationForm = new System.Windows.Forms.CheckBox();
			checkBoxCostCenter = new System.Windows.Forms.CheckBox();
			groupBox14 = new System.Windows.Forms.GroupBox();
			radioButtonDirectMaturity = new System.Windows.Forms.RadioButton();
			radioButtonIndirectMaturity = new System.Windows.Forms.RadioButton();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			groupBox6 = new System.Windows.Forms.GroupBox();
			radioButtonPDCByMaturity = new System.Windows.Forms.RadioButton();
			radioButtonPDCByTransaction = new System.Windows.Forms.RadioButton();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelPatientanalysis = new System.Windows.Forms.GroupBox();
			checkBoxPatientAnalysis = new System.Windows.Forms.CheckBox();
			panel9 = new System.Windows.Forms.Panel();
			comboBoxPatientAnalysisGroup = new Micromind.DataControls.AnalysisGroupComboBox();
			mmLabel72 = new Micromind.UISupport.MMLabel();
			textboxPatientanalysisPrefix = new Micromind.UISupport.MMTextBox();
			mmLabel80 = new Micromind.UISupport.MMLabel();
			groupBox41 = new System.Windows.Forms.GroupBox();
			radioButton15 = new System.Windows.Forms.RadioButton();
			radioButton16 = new System.Windows.Forms.RadioButton();
			mmLabel81 = new Micromind.UISupport.MMLabel();
			buttonCustomerFlagNames = new System.Windows.Forms.Button();
			groupBox12 = new System.Windows.Forms.GroupBox();
			checkBoxTrackConsignOutItemsSale = new System.Windows.Forms.CheckBox();
			groupBox10 = new System.Windows.Forms.GroupBox();
			checkBoxInvoiceNegativeQty = new System.Windows.Forms.CheckBox();
			checkBoxLSAllowReturnChangePrice = new System.Windows.Forms.CheckBox();
			checkBoxAllowLSReturnWithoutInvoice = new System.Windows.Forms.CheckBox();
			groupBox4 = new System.Windows.Forms.GroupBox();
			checkBoxDisableCustomerCreditLimit = new System.Windows.Forms.CheckBox();
			checkBoxMaterialReservationOnSo = new System.Windows.Forms.CheckBox();
			checkBoxB2Cbased = new System.Windows.Forms.CheckBox();
			checkBoxEnableTempSaving = new System.Windows.Forms.CheckBox();
			checkBoxExcludeZeroQtyInDN = new System.Windows.Forms.CheckBox();
			checkBoxAllowCustomerChangeInDN = new System.Windows.Forms.CheckBox();
			checkBoxOnlyOpenInvoice = new System.Windows.Forms.CheckBox();
			checkBoxAllowcreditsaleinSalesReceipt = new System.Windows.Forms.CheckBox();
			checkBoxTakelastSalesprice = new System.Windows.Forms.CheckBox();
			checkBoxShowItemdetail = new System.Windows.Forms.CheckBox();
			checkBoxShowLotdetailinPrintout = new System.Windows.Forms.CheckBox();
			checkBoxInlineDiscount = new System.Windows.Forms.CheckBox();
			checkBoxIncludePDC = new System.Windows.Forms.CheckBox();
			groupBox2 = new System.Windows.Forms.GroupBox();
			checkBoxNotallowzeroinSales = new System.Windows.Forms.CheckBox();
			mmLabel30 = new Micromind.UISupport.MMLabel();
			comboBoxsaleslessthanCostAction = new Micromind.DataControls.OptionsAllowComboBox();
			mmLabel35 = new Micromind.UISupport.MMLabel();
			panelsalespricelessthancost = new System.Windows.Forms.Panel();
			textBoxPricelessthancostpassword = new Micromind.UISupport.MMTextBox();
			mmLabel36 = new Micromind.UISupport.MMLabel();
			checkBoxDeliveryNoteCL = new System.Windows.Forms.CheckBox();
			panelNegativeQuantityPassword = new System.Windows.Forms.Panel();
			textBoxNegativeQuantityPassword = new Micromind.UISupport.MMTextBox();
			mmLabel26 = new Micromind.UISupport.MMLabel();
			panelOverCLPassword = new System.Windows.Forms.Panel();
			textBoxOverCLPassword = new Micromind.UISupport.MMTextBox();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			panelMinPricePassword = new System.Windows.Forms.Panel();
			textBoxMinPricePassword = new Micromind.UISupport.MMTextBox();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			comboBoxMinusNegativeQuantityAction = new Micromind.DataControls.OptionsAllowComboBox();
			comboBoxOverCLAction = new Micromind.DataControls.OptionsAllowComboBox();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			comboBoxMinPriceAction = new Micromind.DataControls.OptionsAllowComboBox();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			groupBox1 = new System.Windows.Forms.GroupBox();
			checkBoxESAllowtocreatepicklist = new System.Windows.Forms.CheckBox();
			checkBoxESAllowDNWithoutInvoice = new System.Windows.Forms.CheckBox();
			checkBoxLSAllowDNWithoutInvoice = new System.Windows.Forms.CheckBox();
			comboBoxExportSalesFlow = new System.Windows.Forms.ComboBox();
			checkBoxESAllowMoreQTY = new System.Windows.Forms.CheckBox();
			checkBoxESAllowWithoutSO = new System.Windows.Forms.CheckBox();
			checkBoxESAllowAddNew = new System.Windows.Forms.CheckBox();
			mmLabel34 = new Micromind.UISupport.MMLabel();
			checkBoxLSAllowInvoiceMore = new System.Windows.Forms.CheckBox();
			checkBoxLSAllowWithoutSO = new System.Windows.Forms.CheckBox();
			comboBoxLocalSalesFlow = new System.Windows.Forms.ComboBox();
			checkBoxLSAllowAddNew = new System.Windows.Forms.CheckBox();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel58 = new Micromind.UISupport.MMLabel();
			comboBoxoptionsAllocation = new Micromind.DataControls.OptionsAllowComboBox();
			mmLabel59 = new Micromind.UISupport.MMLabel();
			label10 = new System.Windows.Forms.Label();
			mmLabel56 = new Micromind.UISupport.MMLabel();
			textboxDiscountWriteoffPerc = new Micromind.UISupport.NumberTextBox();
			groupBox9 = new System.Windows.Forms.GroupBox();
			radioButtonEOMInvoiceDate = new System.Windows.Forms.RadioButton();
			radioButtonAgingEOMDueDate = new System.Windows.Forms.RadioButton();
			panelMonth6 = new System.Windows.Forms.Panel();
			textBoxToMonth6 = new Micromind.UISupport.NumberTextBox();
			textBoxFromMonth6 = new Micromind.UISupport.NumberTextBox();
			textBoxAgingName6 = new System.Windows.Forms.TextBox();
			panelMonth5 = new System.Windows.Forms.Panel();
			textBoxToMonth5 = new Micromind.UISupport.NumberTextBox();
			textBoxFromMonth5 = new Micromind.UISupport.NumberTextBox();
			textBoxAgingName5 = new System.Windows.Forms.TextBox();
			panelMonth4 = new System.Windows.Forms.Panel();
			textBoxToMonth4 = new Micromind.UISupport.NumberTextBox();
			textBoxFromMonth4 = new Micromind.UISupport.NumberTextBox();
			textBoxAgingName4 = new System.Windows.Forms.TextBox();
			panelMonth3 = new System.Windows.Forms.Panel();
			textBoxToMonth3 = new Micromind.UISupport.NumberTextBox();
			textBoxFromMonth3 = new Micromind.UISupport.NumberTextBox();
			textBoxAgingName3 = new System.Windows.Forms.TextBox();
			panelMonth2 = new System.Windows.Forms.Panel();
			textBoxToMonth2 = new Micromind.UISupport.NumberTextBox();
			textBoxFromMonth2 = new Micromind.UISupport.NumberTextBox();
			textBoxAgingName2 = new System.Windows.Forms.TextBox();
			panelMonth1 = new System.Windows.Forms.Panel();
			textBoxToMonth1 = new Micromind.UISupport.NumberTextBox();
			textBoxFromMonth1 = new Micromind.UISupport.NumberTextBox();
			textBoxAgingName1 = new System.Windows.Forms.TextBox();
			panelCurrent = new System.Windows.Forms.Panel();
			textBoxToMonth0 = new Micromind.UISupport.NumberTextBox();
			textBoxFromMonth0 = new Micromind.UISupport.NumberTextBox();
			textBoxAgingName0 = new System.Windows.Forms.TextBox();
			checkBoxMonth6 = new System.Windows.Forms.CheckBox();
			checkBoxMonth5 = new System.Windows.Forms.CheckBox();
			checkBoxMonth4 = new System.Windows.Forms.CheckBox();
			checkBoxMonth3 = new System.Windows.Forms.CheckBox();
			checkBoxMonth2 = new System.Windows.Forms.CheckBox();
			checkBoxMonth1 = new System.Windows.Forms.CheckBox();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			radioButtonAgingDate = new System.Windows.Forms.RadioButton();
			label2 = new System.Windows.Forms.Label();
			radioButtonAgingDueDate = new System.Windows.Forms.RadioButton();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel60 = new Micromind.UISupport.MMLabel();
			ComboBoxPackingListQtyAction = new Micromind.DataControls.OptionsAllowComboBox();
			label11 = new System.Windows.Forms.Label();
			checkBoxManadatoryPO = new System.Windows.Forms.CheckBox();
			checkBoxConsiderStockinMRPQ = new System.Windows.Forms.CheckBox();
			checkBoxShowBOLinPL = new System.Windows.Forms.CheckBox();
			checkBoxOrderandShipment = new System.Windows.Forms.CheckBox();
			checkBoxAllowtoeditreqdateinPO = new System.Windows.Forms.CheckBox();
			buttonVenFlags = new System.Windows.Forms.Button();
			checkBoxGRNZeroQty = new System.Windows.Forms.CheckBox();
			groupBox19 = new System.Windows.Forms.GroupBox();
			checkBoxAllowPReturnChangePrice = new System.Windows.Forms.CheckBox();
			checkBoxAllowPReturnWithoutInvoice = new System.Windows.Forms.CheckBox();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			panelPurchaseLandingCostMethod = new System.Windows.Forms.Panel();
			radioButtonWeight = new System.Windows.Forms.RadioButton();
			radioButtonValue = new System.Windows.Forms.RadioButton();
			groupBox17 = new System.Windows.Forms.GroupBox();
			checkBoxTrackConsignInExpenses = new System.Windows.Forms.CheckBox();
			checkBoxTrackConsignInDetail = new System.Windows.Forms.CheckBox();
			checkBoxConsignInFIFO = new System.Windows.Forms.CheckBox();
			checkBoxPurchaseNegativeQty = new System.Windows.Forms.CheckBox();
			checkBoxShowLCAmount = new System.Windows.Forms.CheckBox();
			groupBox11 = new System.Windows.Forms.GroupBox();
			checkBoxImportMoreThanPLQTY = new System.Windows.Forms.CheckBox();
			checkBoxImportAllowNewInPackingList = new System.Windows.Forms.CheckBox();
			checkBoxPurchaseOrderChangePrice = new System.Windows.Forms.CheckBox();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			checkBoxLocalAllowNewInGRN = new System.Windows.Forms.CheckBox();
			checkBoxImportMoreThanPOQTY = new System.Windows.Forms.CheckBox();
			comboBoxLocalPurchaseFlow = new System.Windows.Forms.ComboBox();
			mmLabel33 = new Micromind.UISupport.MMLabel();
			checkBoxLocalAllowGRNWithoutPO = new System.Windows.Forms.CheckBox();
			checkBoxImportAllowGRNWithoutPO = new System.Windows.Forms.CheckBox();
			checkBoxLocalMoreThanPOQTY = new System.Windows.Forms.CheckBox();
			comboBoxImportPurchaseFlow = new System.Windows.Forms.ComboBox();
			checkBoxImportAllowNewInGRN = new System.Windows.Forms.CheckBox();
			checkBoxLPurchaseLandingCost = new System.Windows.Forms.CheckBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraExpandableGroupBoxGridFields = new Infragistics.Win.Misc.UltraExpandableGroupBox();
			ultraExpandableGroupBoxPanel2 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
			textBoxRefDate2 = new Micromind.UISupport.MMTextBox();
			textBoxRefDate1 = new Micromind.UISupport.MMTextBox();
			textBoxRefNum2 = new Micromind.UISupport.MMTextBox();
			textBoxRefNum1 = new Micromind.UISupport.MMTextBox();
			mmLabel73 = new Micromind.UISupport.MMLabel();
			mmLabel74 = new Micromind.UISupport.MMLabel();
			mmLabel75 = new Micromind.UISupport.MMLabel();
			mmLabel76 = new Micromind.UISupport.MMLabel();
			mmLabel77 = new Micromind.UISupport.MMLabel();
			mmLabel78 = new Micromind.UISupport.MMLabel();
			textBoxRefText1 = new Micromind.UISupport.MMTextBox();
			textBoxRefText2 = new Micromind.UISupport.MMTextBox();
			textBoxRefSlNo = new Micromind.UISupport.MMTextBox();
			mmLabel79 = new Micromind.UISupport.MMLabel();
			expandableGroupBoxTypesName = new Infragistics.Win.Misc.UltraExpandableGroupBox();
			ultraExpandableGroupBoxPanel1 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
			mmLabel70 = new Micromind.UISupport.MMLabel();
			textBoxPType7Name = new Micromind.UISupport.MMTextBox();
			textBoxPType8Name = new Micromind.UISupport.MMTextBox();
			mmLabel71 = new Micromind.UISupport.MMLabel();
			mmLabel67 = new Micromind.UISupport.MMLabel();
			mmLabel68 = new Micromind.UISupport.MMLabel();
			textBoxPType5Name = new Micromind.UISupport.MMTextBox();
			textBoxPType6Name = new Micromind.UISupport.MMTextBox();
			textBoxPType4Name = new Micromind.UISupport.MMTextBox();
			mmLabel69 = new Micromind.UISupport.MMLabel();
			mmLabel64 = new Micromind.UISupport.MMLabel();
			mmLabel66 = new Micromind.UISupport.MMLabel();
			textBoxPType2Name = new Micromind.UISupport.MMTextBox();
			textBoxPType3Name = new Micromind.UISupport.MMTextBox();
			textBoxPType1Name = new Micromind.UISupport.MMTextBox();
			mmLabel65 = new Micromind.UISupport.MMLabel();
			checkBoxShowMultiDimension = new System.Windows.Forms.CheckBox();
			comboBoxItemCreationOption = new System.Windows.Forms.ComboBox();
			mmLabel53 = new Micromind.UISupport.MMLabel();
			groupBox36 = new System.Windows.Forms.GroupBox();
			textBoxDescription3 = new Micromind.UISupport.MMTextBox();
			mmLabel57 = new Micromind.UISupport.MMLabel();
			checkBoxActivatePartsDetails = new System.Windows.Forms.CheckBox();
			groupBox35 = new System.Windows.Forms.GroupBox();
			mmTextBox1 = new Micromind.UISupport.MMTextBox();
			txtBoxSpecificationName = new Micromind.UISupport.MMTextBox();
			mmLabel54 = new Micromind.UISupport.MMLabel();
			mmLabel55 = new Micromind.UISupport.MMLabel();
			groupBox31 = new System.Windows.Forms.GroupBox();
			checkBoxVehicleAnlysis = new System.Windows.Forms.CheckBox();
			checkBoxVehicleAnalysis = new System.Windows.Forms.CheckBox();
			panelVehicleanalysis = new System.Windows.Forms.Panel();
			comboBoxVehicleAnalysis = new Micromind.DataControls.AnalysisGroupComboBox();
			mmLabel47 = new Micromind.UISupport.MMLabel();
			textboxVehicleanalysisPrefix = new Micromind.UISupport.MMTextBox();
			mmLabel48 = new Micromind.UISupport.MMLabel();
			groupBox32 = new System.Windows.Forms.GroupBox();
			radioButton7 = new System.Windows.Forms.RadioButton();
			radioButton8 = new System.Windows.Forms.RadioButton();
			mmLabel49 = new Micromind.UISupport.MMLabel();
			groupBoxLotDetails = new System.Windows.Forms.GroupBox();
			checkBoxActiveBinField = new System.Windows.Forms.CheckBox();
			label9 = new System.Windows.Forms.Label();
			textBoxRenameRef2 = new Micromind.UISupport.MMTextBox();
			label8 = new System.Windows.Forms.Label();
			textBoxRenameLotNo = new Micromind.UISupport.MMTextBox();
			buttonItmFlags = new System.Windows.Forms.Button();
			groupBox21 = new System.Windows.Forms.GroupBox();
			panel4 = new System.Windows.Forms.Panel();
			textBoxInvToMonth4 = new Micromind.UISupport.NumberTextBox();
			textBoxInvFromMonth4 = new Micromind.UISupport.NumberTextBox();
			textBoxInvAgingName4 = new System.Windows.Forms.TextBox();
			panel5 = new System.Windows.Forms.Panel();
			textBoxInvToMonth3 = new Micromind.UISupport.NumberTextBox();
			textBoxInvFromMonth3 = new Micromind.UISupport.NumberTextBox();
			textBoxInvAgingName3 = new System.Windows.Forms.TextBox();
			panel6 = new System.Windows.Forms.Panel();
			textBoxInvToMonth2 = new Micromind.UISupport.NumberTextBox();
			textBoxInvFromMonth2 = new Micromind.UISupport.NumberTextBox();
			textBoxInvAgingName2 = new System.Windows.Forms.TextBox();
			panel7 = new System.Windows.Forms.Panel();
			textBoxInvToMonth1 = new Micromind.UISupport.NumberTextBox();
			textBoxInvFromMonth1 = new Micromind.UISupport.NumberTextBox();
			textBoxInvAgingName1 = new System.Windows.Forms.TextBox();
			checkBoxInvMonth4 = new System.Windows.Forms.CheckBox();
			checkBoxInvMonth3 = new System.Windows.Forms.CheckBox();
			checkBoxInvMonth2 = new System.Windows.Forms.CheckBox();
			checkBoxInvMonth1 = new System.Windows.Forms.CheckBox();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			groupBox16 = new System.Windows.Forms.GroupBox();
			checkBoxenablecostondelete = new System.Windows.Forms.CheckBox();
			checkBoxLoadDescFromPriceList = new System.Windows.Forms.CheckBox();
			groupBox5 = new System.Windows.Forms.GroupBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxAttribute2Name = new Micromind.UISupport.MMTextBox();
			textBoxAttribute1Name = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxAttribute3Name = new Micromind.UISupport.MMTextBox();
			groupBox3 = new System.Windows.Forms.GroupBox();
			radioButtonMatrixDescAttribute = new System.Windows.Forms.RadioButton();
			radioButtonMatrixDescOnly = new System.Windows.Forms.RadioButton();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			groupBox8 = new System.Windows.Forms.GroupBox();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxItemPrice2 = new Micromind.UISupport.MMTextBox();
			textBoxItemPrice1 = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			label1 = new Micromind.UISupport.MMLabel();
			textBoxItemPrice3 = new Micromind.UISupport.MMTextBox();
			groupBox20 = new System.Windows.Forms.GroupBox();
			checkBoxShowUPCInProductCombo = new System.Windows.Forms.CheckBox();
			checkBoxShowitemFeatures = new System.Windows.Forms.CheckBox();
			checkBoxShowCostInProductCombo = new System.Windows.Forms.CheckBox();
			checkBoxShowUnitInProductCombo = new System.Windows.Forms.CheckBox();
			checkBoxShowQuantityInProductCombo = new System.Windows.Forms.CheckBox();
			grpCostSetting = new System.Windows.Forms.GroupBox();
			checkBoxAllCosting = new System.Windows.Forms.CheckBox();
			checkBoxfutureCosting = new System.Windows.Forms.CheckBox();
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			groupBox29 = new System.Windows.Forms.GroupBox();
			checkBoxHRnalaysis = new System.Windows.Forms.CheckBox();
			panelHRanalysis = new System.Windows.Forms.Panel();
			comboBoxHRAnalysis = new Micromind.DataControls.AnalysisGroupComboBox();
			mmLabel46 = new Micromind.UISupport.MMLabel();
			textboxHRanalysisPrefix = new Micromind.UISupport.MMTextBox();
			mmLabel45 = new Micromind.UISupport.MMLabel();
			groupBox30 = new System.Windows.Forms.GroupBox();
			radioButton9 = new System.Windows.Forms.RadioButton();
			radioButton10 = new System.Windows.Forms.RadioButton();
			mmLabel44 = new Micromind.UISupport.MMLabel();
			mmLabel43 = new Micromind.UISupport.MMLabel();
			textBoxAutoresumptionDays = new Micromind.UISupport.NumberTextBox();
			groupBox27 = new System.Windows.Forms.GroupBox();
			panel2 = new System.Windows.Forms.Panel();
			radioButtondeductiononNetDaysSal = new System.Windows.Forms.RadioButton();
			radioButtonDeductionoonNetDays = new System.Windows.Forms.RadioButton();
			groupBox28 = new System.Windows.Forms.GroupBox();
			radioButton5 = new System.Windows.Forms.RadioButton();
			radioButton6 = new System.Windows.Forms.RadioButton();
			mmLabel41 = new Micromind.UISupport.MMLabel();
			groupBox25 = new System.Windows.Forms.GroupBox();
			checkBoxRoundOffSalaryCalculation = new System.Windows.Forms.CheckBox();
			panel3 = new System.Windows.Forms.Panel();
			radioButtonAnnual = new System.Windows.Forms.RadioButton();
			radioButtonThirtyDays = new System.Windows.Forms.RadioButton();
			radioButtonDaysInMonth = new System.Windows.Forms.RadioButton();
			groupBox26 = new System.Windows.Forms.GroupBox();
			radioButton3 = new System.Windows.Forms.RadioButton();
			radioButton4 = new System.Windows.Forms.RadioButton();
			mmLabel42 = new Micromind.UISupport.MMLabel();
			groupBox23 = new System.Windows.Forms.GroupBox();
			groupBox24 = new System.Windows.Forms.GroupBox();
			radioButton1 = new System.Windows.Forms.RadioButton();
			radioButton2 = new System.Windows.Forms.RadioButton();
			mmLabel37 = new Micromind.UISupport.MMLabel();
			mmLabel40 = new Micromind.UISupport.MMLabel();
			textBoxRangeTo = new Micromind.UISupport.NumberTextBox();
			textBoxRemarkvalidationpoint = new Micromind.UISupport.NumberTextBox();
			textBoxRangeFrom = new Micromind.UISupport.NumberTextBox();
			mmLabel38 = new Micromind.UISupport.MMLabel();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			panel1 = new System.Windows.Forms.Panel();
			radioButtonEmployee = new System.Windows.Forms.RadioButton();
			radioButtonDate = new System.Windows.Forms.RadioButton();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			textBoxBankName = new Micromind.UISupport.MMTextBox();
			comboBoxBank = new Micromind.DataControls.BankComboBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxCompanyWPSID = new Micromind.UISupport.MMTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			groupBox15 = new System.Windows.Forms.GroupBox();
			textBoxMonthHours = new Micromind.UISupport.NumberTextBox();
			textBoxDayHours = new Micromind.UISupport.NumberTextBox();
			comboBoxDays = new Micromind.DataControls.DaysComboBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			tabPagePOS = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxUpdateSalesPersonWhileSave = new System.Windows.Forms.CheckBox();
			checkBoxDisplayItemFeatures = new System.Windows.Forms.CheckBox();
			checkBoxPOSSufficientQuantity = new System.Windows.Forms.CheckBox();
			ultraTabPageProject = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxActivateAutoService = new System.Windows.Forms.CheckBox();
			checkBoxAllowJobChange = new System.Windows.Forms.CheckBox();
			checkBoxCreateProjectwithSO = new System.Windows.Forms.CheckBox();
			checkBoxIssueGRNtoProject = new System.Windows.Forms.CheckBox();
			checkBoxUseProjectPhase = new System.Windows.Forms.CheckBox();
			ultraTabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxAllowdofollowuponlead = new System.Windows.Forms.CheckBox();
			ultraTabPageControl7 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			groupBox38 = new System.Windows.Forms.GroupBox();
			panel8 = new System.Windows.Forms.Panel();
			checkBoxUnit = new System.Windows.Forms.CheckBox();
			checkBoxTenant = new System.Windows.Forms.CheckBox();
			checkBoxProperty = new System.Windows.Forms.CheckBox();
			checkBoxVendor = new System.Windows.Forms.CheckBox();
			ultraFormattedLinkLabelTaxGroup = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkBoxProduct = new System.Windows.Forms.CheckBox();
			checkBoxCustomer = new System.Windows.Forms.CheckBox();
			comboBoxDefaultTaxOption = new System.Windows.Forms.ComboBox();
			comboBoxDefaultTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			mmLabel63 = new Micromind.UISupport.MMLabel();
			mmLabel61 = new Micromind.UISupport.MMLabel();
			groupBox39 = new System.Windows.Forms.GroupBox();
			radioButton13 = new System.Windows.Forms.RadioButton();
			radioButton14 = new System.Windows.Forms.RadioButton();
			mmLabel62 = new Micromind.UISupport.MMLabel();
			labelperc = new Micromind.UISupport.MMLabel();
			textBoxTax = new Micromind.UISupport.NumberTextBox();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			mmLabel28 = new Micromind.UISupport.MMLabel();
			textBoxSMSMobileNo = new Micromind.UISupport.MMTextBox();
			textBoxSMSUserName = new Micromind.UISupport.MMTextBox();
			textBoxSMSPassword = new Micromind.UISupport.MMTextBox();
			mmLabel27 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl8 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			groupBox33 = new System.Windows.Forms.GroupBox();
			checkBoxLegalanalaysis = new System.Windows.Forms.CheckBox();
			panelLegalAnalysis = new System.Windows.Forms.Panel();
			comboBoxLegalAnalysis = new Micromind.DataControls.AnalysisGroupComboBox();
			mmLabel50 = new Micromind.UISupport.MMLabel();
			textboxLegalanalysisPrefix = new Micromind.UISupport.MMTextBox();
			mmLabel51 = new Micromind.UISupport.MMLabel();
			groupBox34 = new System.Windows.Forms.GroupBox();
			radioButton11 = new System.Windows.Forms.RadioButton();
			radioButton12 = new System.Windows.Forms.RadioButton();
			mmLabel52 = new Micromind.UISupport.MMLabel();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			groupBox18.SuspendLayout();
			groupBox13.SuspendLayout();
			groupBox7.SuspendLayout();
			ultraTabPageControl2.SuspendLayout();
			groupBox37.SuspendLayout();
			groupBox22.SuspendLayout();
			groupBox14.SuspendLayout();
			groupBox6.SuspendLayout();
			tabPageDetails.SuspendLayout();
			panelPatientanalysis.SuspendLayout();
			panel9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPatientAnalysisGroup).BeginInit();
			groupBox41.SuspendLayout();
			groupBox12.SuspendLayout();
			groupBox10.SuspendLayout();
			groupBox4.SuspendLayout();
			groupBox2.SuspendLayout();
			panelsalespricelessthancost.SuspendLayout();
			panelNegativeQuantityPassword.SuspendLayout();
			panelOverCLPassword.SuspendLayout();
			panelMinPricePassword.SuspendLayout();
			groupBox1.SuspendLayout();
			ultraTabPageControl5.SuspendLayout();
			groupBox9.SuspendLayout();
			panelMonth6.SuspendLayout();
			panelMonth5.SuspendLayout();
			panelMonth4.SuspendLayout();
			panelMonth3.SuspendLayout();
			panelMonth2.SuspendLayout();
			panelMonth1.SuspendLayout();
			panelCurrent.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			groupBox19.SuspendLayout();
			panelPurchaseLandingCostMethod.SuspendLayout();
			groupBox17.SuspendLayout();
			groupBox11.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraExpandableGroupBoxGridFields).BeginInit();
			ultraExpandableGroupBoxGridFields.SuspendLayout();
			ultraExpandableGroupBoxPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)expandableGroupBoxTypesName).BeginInit();
			expandableGroupBoxTypesName.SuspendLayout();
			ultraExpandableGroupBoxPanel1.SuspendLayout();
			groupBox36.SuspendLayout();
			groupBox35.SuspendLayout();
			groupBox31.SuspendLayout();
			panelVehicleanalysis.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicleAnalysis).BeginInit();
			groupBox32.SuspendLayout();
			groupBoxLotDetails.SuspendLayout();
			groupBox21.SuspendLayout();
			panel4.SuspendLayout();
			panel5.SuspendLayout();
			panel6.SuspendLayout();
			panel7.SuspendLayout();
			groupBox16.SuspendLayout();
			groupBox5.SuspendLayout();
			groupBox3.SuspendLayout();
			groupBox8.SuspendLayout();
			groupBox20.SuspendLayout();
			grpCostSetting.SuspendLayout();
			ultraTabPageControl4.SuspendLayout();
			groupBox29.SuspendLayout();
			panelHRanalysis.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxHRAnalysis).BeginInit();
			groupBox30.SuspendLayout();
			groupBox27.SuspendLayout();
			panel2.SuspendLayout();
			groupBox28.SuspendLayout();
			groupBox25.SuspendLayout();
			panel3.SuspendLayout();
			groupBox26.SuspendLayout();
			groupBox23.SuspendLayout();
			groupBox24.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).BeginInit();
			groupBox15.SuspendLayout();
			tabPagePOS.SuspendLayout();
			ultraTabPageProject.SuspendLayout();
			ultraTabPageControl6.SuspendLayout();
			ultraTabPageControl7.SuspendLayout();
			groupBox38.SuspendLayout();
			panel8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultTaxGroup).BeginInit();
			groupBox39.SuspendLayout();
			ultraTabPageControl8.SuspendLayout();
			groupBox33.SuspendLayout();
			panelLegalAnalysis.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLegalAnalysis).BeginInit();
			groupBox34.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(ultraPictureBoxInformation);
			tabPageGeneral.Controls.Add(checkBoxDocumentVersioning);
			tabPageGeneral.Controls.Add(checkBoxAutoCode);
			tabPageGeneral.Controls.Add(groupBox18);
			tabPageGeneral.Controls.Add(groupBox13);
			tabPageGeneral.Controls.Add(groupBox7);
			tabPageGeneral.Controls.Add(checkBoxJobCosting);
			tabPageGeneral.Controls.Add(checkBoxMultiCurrency);
			tabPageGeneral.Controls.Add(checkBoxTax);
			tabPageGeneral.Location = new System.Drawing.Point(1, 20);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(807, 626);
			ultraPictureBoxInformation.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			ultraPictureBoxInformation.BorderShadowColor = System.Drawing.Color.Empty;
			ultraPictureBoxInformation.Image = resources.GetObject("ultraPictureBoxInformation.Image");
			ultraPictureBoxInformation.Location = new System.Drawing.Point(788, 0);
			ultraPictureBoxInformation.Name = "ultraPictureBoxInformation";
			ultraPictureBoxInformation.Size = new System.Drawing.Size(17, 19);
			ultraPictureBoxInformation.TabIndex = 297;
			ultraPictureBoxInformation.Click += new System.EventHandler(ultraPictureBoxInformation_Click);
			checkBoxDocumentVersioning.AutoSize = true;
			checkBoxDocumentVersioning.Location = new System.Drawing.Point(220, 38);
			checkBoxDocumentVersioning.Name = "checkBoxDocumentVersioning";
			checkBoxDocumentVersioning.Size = new System.Drawing.Size(155, 17);
			checkBoxDocumentVersioning.TabIndex = 4;
			checkBoxDocumentVersioning.Text = "Track document versioning";
			checkBoxDocumentVersioning.UseVisualStyleBackColor = true;
			checkBoxAutoCode.AutoSize = true;
			checkBoxAutoCode.Location = new System.Drawing.Point(220, 15);
			checkBoxAutoCode.Name = "checkBoxAutoCode";
			checkBoxAutoCode.Size = new System.Drawing.Size(257, 17);
			checkBoxAutoCode.TabIndex = 3;
			checkBoxAutoCode.Text = "Auto generate code for cards based on last code";
			checkBoxAutoCode.UseVisualStyleBackColor = true;
			groupBox18.Controls.Add(textBoxServerTemplatePath);
			groupBox18.Controls.Add(mmLabel15);
			groupBox18.Controls.Add(btnOpenFileDialog2);
			groupBox18.Location = new System.Drawing.Point(21, 305);
			groupBox18.Name = "groupBox18";
			groupBox18.Size = new System.Drawing.Size(546, 69);
			groupBox18.TabIndex = 7;
			groupBox18.TabStop = false;
			groupBox18.Text = "Server Print Templates Path";
			textBoxServerTemplatePath.BackColor = System.Drawing.Color.White;
			textBoxServerTemplatePath.CustomReportFieldName = "";
			textBoxServerTemplatePath.CustomReportKey = "";
			textBoxServerTemplatePath.CustomReportValueType = 1;
			textBoxServerTemplatePath.IsComboTextBox = false;
			textBoxServerTemplatePath.IsModified = false;
			textBoxServerTemplatePath.Location = new System.Drawing.Point(44, 28);
			textBoxServerTemplatePath.MaxLength = 1000;
			textBoxServerTemplatePath.Name = "textBoxServerTemplatePath";
			textBoxServerTemplatePath.Size = new System.Drawing.Size(459, 20);
			textBoxServerTemplatePath.TabIndex = 13;
			textBoxServerTemplatePath.TabStop = false;
			mmLabel15.AutoSize = true;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(6, 31);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(32, 13);
			mmLabel15.TabIndex = 14;
			mmLabel15.Text = "Path:";
			btnOpenFileDialog2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			btnOpenFileDialog2.BackColor = System.Drawing.Color.DarkGray;
			btnOpenFileDialog2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			btnOpenFileDialog2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			btnOpenFileDialog2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnOpenFileDialog2.Location = new System.Drawing.Point(506, 27);
			btnOpenFileDialog2.Name = "btnOpenFileDialog2";
			btnOpenFileDialog2.Size = new System.Drawing.Size(25, 22);
			btnOpenFileDialog2.TabIndex = 12;
			btnOpenFileDialog2.Text = "...";
			btnOpenFileDialog2.UseVisualStyleBackColor = false;
			btnOpenFileDialog2.Click += new System.EventHandler(btnOpenFileDialog2_Click);
			groupBox13.Controls.Add(comboBoxTemplatePath);
			groupBox13.Controls.Add(textBoxTemplatePath);
			groupBox13.Controls.Add(mmLabel7);
			groupBox13.Controls.Add(buttonSelectTemplatePath);
			groupBox13.Location = new System.Drawing.Point(21, 227);
			groupBox13.Name = "groupBox13";
			groupBox13.Size = new System.Drawing.Size(546, 69);
			groupBox13.TabIndex = 6;
			groupBox13.TabStop = false;
			groupBox13.Text = "Print Templates";
			comboBoxTemplatePath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTemplatePath.FormattingEnabled = true;
			comboBoxTemplatePath.Items.AddRange(new object[2]
			{
				"Client Directory",
				"Server Directory"
			});
			comboBoxTemplatePath.Location = new System.Drawing.Point(44, 27);
			comboBoxTemplatePath.Name = "comboBoxTemplatePath";
			comboBoxTemplatePath.Size = new System.Drawing.Size(113, 21);
			comboBoxTemplatePath.TabIndex = 18;
			textBoxTemplatePath.BackColor = System.Drawing.Color.White;
			textBoxTemplatePath.CustomReportFieldName = "";
			textBoxTemplatePath.CustomReportKey = "";
			textBoxTemplatePath.CustomReportValueType = 1;
			textBoxTemplatePath.IsComboTextBox = false;
			textBoxTemplatePath.IsModified = false;
			textBoxTemplatePath.Location = new System.Drawing.Point(160, 28);
			textBoxTemplatePath.MaxLength = 1000;
			textBoxTemplatePath.Name = "textBoxTemplatePath";
			textBoxTemplatePath.Size = new System.Drawing.Size(344, 20);
			textBoxTemplatePath.TabIndex = 13;
			textBoxTemplatePath.TabStop = false;
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(6, 31);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(32, 13);
			mmLabel7.TabIndex = 14;
			mmLabel7.Text = "Path:";
			buttonSelectTemplatePath.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectTemplatePath.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectTemplatePath.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectTemplatePath.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectTemplatePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectTemplatePath.Location = new System.Drawing.Point(506, 27);
			buttonSelectTemplatePath.Name = "buttonSelectTemplatePath";
			buttonSelectTemplatePath.Size = new System.Drawing.Size(25, 22);
			buttonSelectTemplatePath.TabIndex = 12;
			buttonSelectTemplatePath.Text = "...";
			buttonSelectTemplatePath.UseVisualStyleBackColor = false;
			buttonSelectTemplatePath.Click += new System.EventHandler(buttonSelectTemplatePath_Click);
			groupBox7.Controls.Add(textFilePath);
			groupBox7.Controls.Add(labelFileSavingPath);
			groupBox7.Controls.Add(buttonOpenFileDialog);
			groupBox7.Location = new System.Drawing.Point(21, 153);
			groupBox7.Name = "groupBox7";
			groupBox7.Size = new System.Drawing.Size(546, 69);
			groupBox7.TabIndex = 5;
			groupBox7.TabStop = false;
			groupBox7.Text = "Attachment Files Saving";
			textFilePath.BackColor = System.Drawing.Color.White;
			textFilePath.CustomReportFieldName = "";
			textFilePath.CustomReportKey = "";
			textFilePath.CustomReportValueType = 1;
			textFilePath.IsComboTextBox = false;
			textFilePath.IsModified = false;
			textFilePath.Location = new System.Drawing.Point(44, 28);
			textFilePath.MaxLength = 1000;
			textFilePath.Name = "textFilePath";
			textFilePath.Size = new System.Drawing.Size(459, 20);
			textFilePath.TabIndex = 13;
			textFilePath.TabStop = false;
			labelFileSavingPath.AutoSize = true;
			labelFileSavingPath.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelFileSavingPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelFileSavingPath.IsFieldHeader = false;
			labelFileSavingPath.IsRequired = false;
			labelFileSavingPath.Location = new System.Drawing.Point(6, 31);
			labelFileSavingPath.Name = "labelFileSavingPath";
			labelFileSavingPath.PenWidth = 1f;
			labelFileSavingPath.ShowBorder = false;
			labelFileSavingPath.Size = new System.Drawing.Size(32, 13);
			labelFileSavingPath.TabIndex = 14;
			labelFileSavingPath.Text = "Path:";
			buttonOpenFileDialog.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpenFileDialog.BackColor = System.Drawing.Color.DarkGray;
			buttonOpenFileDialog.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpenFileDialog.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpenFileDialog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpenFileDialog.Location = new System.Drawing.Point(506, 27);
			buttonOpenFileDialog.Name = "buttonOpenFileDialog";
			buttonOpenFileDialog.Size = new System.Drawing.Size(25, 22);
			buttonOpenFileDialog.TabIndex = 12;
			buttonOpenFileDialog.Text = "...";
			buttonOpenFileDialog.UseVisualStyleBackColor = false;
			buttonOpenFileDialog.Click += new System.EventHandler(buttonOpenFileDialog_Click);
			checkBoxJobCosting.AutoSize = true;
			checkBoxJobCosting.Location = new System.Drawing.Point(21, 58);
			checkBoxJobCosting.Name = "checkBoxJobCosting";
			checkBoxJobCosting.Size = new System.Drawing.Size(99, 17);
			checkBoxJobCosting.TabIndex = 2;
			checkBoxJobCosting.Text = "Use job costing";
			checkBoxJobCosting.UseVisualStyleBackColor = true;
			checkBoxMultiCurrency.AutoSize = true;
			checkBoxMultiCurrency.Location = new System.Drawing.Point(21, 36);
			checkBoxMultiCurrency.Name = "checkBoxMultiCurrency";
			checkBoxMultiCurrency.Size = new System.Drawing.Size(113, 17);
			checkBoxMultiCurrency.TabIndex = 1;
			checkBoxMultiCurrency.Text = "Use multi currency";
			checkBoxMultiCurrency.UseVisualStyleBackColor = true;
			checkBoxTax.AutoSize = true;
			checkBoxTax.Location = new System.Drawing.Point(21, 14);
			checkBoxTax.Name = "checkBoxTax";
			checkBoxTax.Size = new System.Drawing.Size(135, 17);
			checkBoxTax.TabIndex = 0;
			checkBoxTax.Text = "Your company has Tax";
			checkBoxTax.UseVisualStyleBackColor = true;
			ultraTabPageControl2.Controls.Add(comboBoxSoftClosePeriod);
			ultraTabPageControl2.Controls.Add(mmLabel82);
			ultraTabPageControl2.Controls.Add(checkBoxDirectChequeReturn);
			ultraTabPageControl2.Controls.Add(groupBox37);
			ultraTabPageControl2.Controls.Add(checkBoxFinancialTransactionPosting);
			ultraTabPageControl2.Controls.Add(groupBox22);
			ultraTabPageControl2.Controls.Add(checkBoxAllowChangeChequePayee);
			ultraTabPageControl2.Controls.Add(checkBoxAllocationForm);
			ultraTabPageControl2.Controls.Add(checkBoxCostCenter);
			ultraTabPageControl2.Controls.Add(groupBox14);
			ultraTabPageControl2.Controls.Add(groupBox6);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(807, 626);
			comboBoxSoftClosePeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxSoftClosePeriod.FormattingEnabled = true;
			comboBoxSoftClosePeriod.Items.AddRange(new object[4]
			{
				"Yearly",
				"Half Year",
				"Quarterly",
				"Monthly"
			});
			comboBoxSoftClosePeriod.Location = new System.Drawing.Point(565, 86);
			comboBoxSoftClosePeriod.Name = "comboBoxSoftClosePeriod";
			comboBoxSoftClosePeriod.Size = new System.Drawing.Size(195, 21);
			comboBoxSoftClosePeriod.TabIndex = 9;
			mmLabel82.AutoSize = true;
			mmLabel82.BackColor = System.Drawing.Color.Transparent;
			mmLabel82.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel82.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel82.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel82.IsFieldHeader = false;
			mmLabel82.IsRequired = false;
			mmLabel82.Location = new System.Drawing.Point(429, 89);
			mmLabel82.Name = "mmLabel82";
			mmLabel82.PenWidth = 1f;
			mmLabel82.ShowBorder = false;
			mmLabel82.Size = new System.Drawing.Size(96, 13);
			mmLabel82.TabIndex = 10;
			mmLabel82.Text = "Soft Close period :";
			mmLabel82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxDirectChequeReturn.AutoSize = true;
			checkBoxDirectChequeReturn.Location = new System.Drawing.Point(30, 394);
			checkBoxDirectChequeReturn.Name = "checkBoxDirectChequeReturn";
			checkBoxDirectChequeReturn.Size = new System.Drawing.Size(138, 17);
			checkBoxDirectChequeReturn.TabIndex = 8;
			checkBoxDirectChequeReturn.Text = "Return cheques directly";
			checkBoxDirectChequeReturn.UseVisualStyleBackColor = true;
			groupBox37.Controls.Add(radioButtonDirectTREntry);
			groupBox37.Controls.Add(radioButtonTRAppTREntry);
			groupBox37.Location = new System.Drawing.Point(432, 13);
			groupBox37.Name = "groupBox37";
			groupBox37.Size = new System.Drawing.Size(328, 62);
			groupBox37.TabIndex = 7;
			groupBox37.TabStop = false;
			groupBox37.Text = "Bank Facility Payment Option";
			radioButtonDirectTREntry.AutoSize = true;
			radioButtonDirectTREntry.Checked = true;
			radioButtonDirectTREntry.Location = new System.Drawing.Point(13, 17);
			radioButtonDirectTREntry.Name = "radioButtonDirectTREntry";
			radioButtonDirectTREntry.Size = new System.Drawing.Size(98, 17);
			radioButtonDirectTREntry.TabIndex = 1;
			radioButtonDirectTREntry.TabStop = true;
			radioButtonDirectTREntry.Text = "Direct TR Entry";
			radioButtonDirectTREntry.UseVisualStyleBackColor = true;
			radioButtonTRAppTREntry.AutoSize = true;
			radioButtonTRAppTREntry.Location = new System.Drawing.Point(13, 40);
			radioButtonTRAppTREntry.Name = "radioButtonTRAppTREntry";
			radioButtonTRAppTREntry.Size = new System.Drawing.Size(140, 17);
			radioButtonTRAppTREntry.TabIndex = 2;
			radioButtonTRAppTREntry.Text = "TR Application-TR Entry";
			radioButtonTRAppTREntry.UseVisualStyleBackColor = true;
			checkBoxFinancialTransactionPosting.AutoSize = true;
			checkBoxFinancialTransactionPosting.Location = new System.Drawing.Point(30, 371);
			checkBoxFinancialTransactionPosting.Name = "checkBoxFinancialTransactionPosting";
			checkBoxFinancialTransactionPosting.Size = new System.Drawing.Size(214, 17);
			checkBoxFinancialTransactionPosting.TabIndex = 6;
			checkBoxFinancialTransactionPosting.Text = "Posting required for financial transaction";
			checkBoxFinancialTransactionPosting.UseVisualStyleBackColor = true;
			groupBox22.Controls.Add(radioButtonPDCIssuedByMaturity);
			groupBox22.Controls.Add(radioButtonPDCIssuedByTransaction);
			groupBox22.Controls.Add(mmLabel17);
			groupBox22.Location = new System.Drawing.Point(17, 108);
			groupBox22.Name = "groupBox22";
			groupBox22.Size = new System.Drawing.Size(395, 89);
			groupBox22.TabIndex = 5;
			groupBox22.TabStop = false;
			groupBox22.Text = "Post Dated Cheques - Issued";
			radioButtonPDCIssuedByMaturity.AutoSize = true;
			radioButtonPDCIssuedByMaturity.Checked = true;
			radioButtonPDCIssuedByMaturity.Location = new System.Drawing.Point(24, 57);
			radioButtonPDCIssuedByMaturity.Name = "radioButtonPDCIssuedByMaturity";
			radioButtonPDCIssuedByMaturity.Size = new System.Drawing.Size(100, 17);
			radioButtonPDCIssuedByMaturity.TabIndex = 2;
			radioButtonPDCIssuedByMaturity.TabStop = true;
			radioButtonPDCIssuedByMaturity.Text = "By maturity date";
			radioButtonPDCIssuedByMaturity.UseVisualStyleBackColor = true;
			radioButtonPDCIssuedByTransaction.AutoSize = true;
			radioButtonPDCIssuedByTransaction.Location = new System.Drawing.Point(24, 34);
			radioButtonPDCIssuedByTransaction.Name = "radioButtonPDCIssuedByTransaction";
			radioButtonPDCIssuedByTransaction.Size = new System.Drawing.Size(116, 17);
			radioButtonPDCIssuedByTransaction.TabIndex = 2;
			radioButtonPDCIssuedByTransaction.Text = "By transaction date";
			radioButtonPDCIssuedByTransaction.UseVisualStyleBackColor = true;
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(10, 16);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(357, 13);
			mmLabel17.TabIndex = 13;
			mmLabel17.Text = "How do you want to record issued PDCs transactions in the party ledger:";
			mmLabel17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxAllowChangeChequePayee.AutoSize = true;
			checkBoxAllowChangeChequePayee.Location = new System.Drawing.Point(30, 348);
			checkBoxAllowChangeChequePayee.Name = "checkBoxAllowChangeChequePayee";
			checkBoxAllowChangeChequePayee.Size = new System.Drawing.Size(273, 17);
			checkBoxAllowChangeChequePayee.TabIndex = 4;
			checkBoxAllowChangeChequePayee.Text = "Allow to change payee name when printing cheques";
			checkBoxAllowChangeChequePayee.UseVisualStyleBackColor = true;
			checkBoxAllocationForm.AutoSize = true;
			checkBoxAllocationForm.Location = new System.Drawing.Point(30, 325);
			checkBoxAllocationForm.Name = "checkBoxAllocationForm";
			checkBoxAllocationForm.Size = new System.Drawing.Size(208, 17);
			checkBoxAllocationForm.TabIndex = 3;
			checkBoxAllocationForm.Text = "Show allocation screen after payments";
			checkBoxAllocationForm.UseVisualStyleBackColor = true;
			checkBoxCostCenter.AutoSize = true;
			checkBoxCostCenter.Location = new System.Drawing.Point(30, 302);
			checkBoxCostCenter.Name = "checkBoxCostCenter";
			checkBoxCostCenter.Size = new System.Drawing.Size(234, 17);
			checkBoxCostCenter.TabIndex = 2;
			checkBoxCostCenter.Text = "Make Cost Center mandatory in transactions";
			checkBoxCostCenter.UseVisualStyleBackColor = true;
			groupBox14.Controls.Add(radioButtonDirectMaturity);
			groupBox14.Controls.Add(radioButtonIndirectMaturity);
			groupBox14.Controls.Add(mmLabel8);
			groupBox14.Location = new System.Drawing.Point(17, 203);
			groupBox14.Name = "groupBox14";
			groupBox14.Size = new System.Drawing.Size(395, 89);
			groupBox14.TabIndex = 1;
			groupBox14.TabStop = false;
			groupBox14.Text = "PDC Maturity Process";
			radioButtonDirectMaturity.AutoSize = true;
			radioButtonDirectMaturity.Checked = true;
			radioButtonDirectMaturity.Location = new System.Drawing.Point(13, 36);
			radioButtonDirectMaturity.Name = "radioButtonDirectMaturity";
			radioButtonDirectMaturity.Size = new System.Drawing.Size(120, 17);
			radioButtonDirectMaturity.TabIndex = 1;
			radioButtonDirectMaturity.TabStop = true;
			radioButtonDirectMaturity.Text = "Direct Maturity Entry";
			radioButtonDirectMaturity.UseVisualStyleBackColor = true;
			radioButtonIndirectMaturity.AutoSize = true;
			radioButtonIndirectMaturity.Location = new System.Drawing.Point(13, 59);
			radioButtonIndirectMaturity.Name = "radioButtonIndirectMaturity";
			radioButtonIndirectMaturity.Size = new System.Drawing.Size(214, 17);
			radioButtonIndirectMaturity.TabIndex = 2;
			radioButtonIndirectMaturity.Text = "Send Cheques to Bank -> Maturity Entry";
			radioButtonIndirectMaturity.UseVisualStyleBackColor = true;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(10, 16);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(214, 13);
			mmLabel8.TabIndex = 0;
			mmLabel8.Text = "How do you want to process PDC maturity:";
			mmLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox6.Controls.Add(radioButtonPDCByMaturity);
			groupBox6.Controls.Add(radioButtonPDCByTransaction);
			groupBox6.Controls.Add(mmLabel5);
			groupBox6.Location = new System.Drawing.Point(17, 13);
			groupBox6.Name = "groupBox6";
			groupBox6.Size = new System.Drawing.Size(395, 89);
			groupBox6.TabIndex = 0;
			groupBox6.TabStop = false;
			groupBox6.Text = "Post Dated Cheques - Received";
			radioButtonPDCByMaturity.AutoSize = true;
			radioButtonPDCByMaturity.Checked = true;
			radioButtonPDCByMaturity.Location = new System.Drawing.Point(24, 57);
			radioButtonPDCByMaturity.Name = "radioButtonPDCByMaturity";
			radioButtonPDCByMaturity.Size = new System.Drawing.Size(100, 17);
			radioButtonPDCByMaturity.TabIndex = 2;
			radioButtonPDCByMaturity.TabStop = true;
			radioButtonPDCByMaturity.Text = "By maturity date";
			radioButtonPDCByMaturity.UseVisualStyleBackColor = true;
			radioButtonPDCByTransaction.AutoSize = true;
			radioButtonPDCByTransaction.Location = new System.Drawing.Point(24, 34);
			radioButtonPDCByTransaction.Name = "radioButtonPDCByTransaction";
			radioButtonPDCByTransaction.Size = new System.Drawing.Size(116, 17);
			radioButtonPDCByTransaction.TabIndex = 2;
			radioButtonPDCByTransaction.Text = "By transaction date";
			radioButtonPDCByTransaction.UseVisualStyleBackColor = true;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(10, 16);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(368, 13);
			mmLabel5.TabIndex = 13;
			mmLabel5.Text = "How do you want to record received PDCs transactions in the party ledger:";
			mmLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			tabPageDetails.AutoScroll = true;
			tabPageDetails.Controls.Add(panelPatientanalysis);
			tabPageDetails.Controls.Add(buttonCustomerFlagNames);
			tabPageDetails.Controls.Add(groupBox12);
			tabPageDetails.Controls.Add(groupBox10);
			tabPageDetails.Controls.Add(groupBox4);
			tabPageDetails.Controls.Add(groupBox2);
			tabPageDetails.Controls.Add(groupBox1);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(807, 626);
			panelPatientanalysis.Controls.Add(checkBoxPatientAnalysis);
			panelPatientanalysis.Controls.Add(panel9);
			panelPatientanalysis.Controls.Add(groupBox41);
			panelPatientanalysis.Location = new System.Drawing.Point(410, 433);
			panelPatientanalysis.Name = "panelPatientanalysis";
			panelPatientanalysis.Size = new System.Drawing.Size(372, 83);
			panelPatientanalysis.TabIndex = 138;
			panelPatientanalysis.TabStop = false;
			checkBoxPatientAnalysis.AutoSize = true;
			checkBoxPatientAnalysis.Location = new System.Drawing.Point(11, 0);
			checkBoxPatientAnalysis.Name = "checkBoxPatientAnalysis";
			checkBoxPatientAnalysis.Size = new System.Drawing.Size(161, 17);
			checkBoxPatientAnalysis.TabIndex = 138;
			checkBoxPatientAnalysis.Text = "Track Expense with Analysis";
			checkBoxPatientAnalysis.UseVisualStyleBackColor = true;
			checkBoxPatientAnalysis.CheckedChanged += new System.EventHandler(checkBoxPatientAnalysis_CheckedChanged);
			panel9.Controls.Add(comboBoxPatientAnalysisGroup);
			panel9.Controls.Add(mmLabel72);
			panel9.Controls.Add(textboxPatientanalysisPrefix);
			panel9.Controls.Add(mmLabel80);
			panel9.Location = new System.Drawing.Point(7, 19);
			panel9.Name = "panel9";
			panel9.Size = new System.Drawing.Size(283, 53);
			panel9.TabIndex = 135;
			comboBoxPatientAnalysisGroup.Assigned = false;
			comboBoxPatientAnalysisGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPatientAnalysisGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPatientAnalysisGroup.CustomReportFieldName = "";
			comboBoxPatientAnalysisGroup.CustomReportKey = "";
			comboBoxPatientAnalysisGroup.CustomReportValueType = 1;
			comboBoxPatientAnalysisGroup.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPatientAnalysisGroup.DisplayLayout.Appearance = appearance;
			comboBoxPatientAnalysisGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPatientAnalysisGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPatientAnalysisGroup.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPatientAnalysisGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxPatientAnalysisGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPatientAnalysisGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxPatientAnalysisGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPatientAnalysisGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPatientAnalysisGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxPatientAnalysisGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPatientAnalysisGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPatientAnalysisGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPatientAnalysisGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPatientAnalysisGroup.Editable = true;
			comboBoxPatientAnalysisGroup.FilterString = "";
			comboBoxPatientAnalysisGroup.HasAllAccount = false;
			comboBoxPatientAnalysisGroup.HasCustom = false;
			comboBoxPatientAnalysisGroup.IsDataLoaded = false;
			comboBoxPatientAnalysisGroup.Location = new System.Drawing.Point(91, 4);
			comboBoxPatientAnalysisGroup.MaxDropDownItems = 12;
			comboBoxPatientAnalysisGroup.Name = "comboBoxPatientAnalysisGroup";
			comboBoxPatientAnalysisGroup.ShowInactiveItems = false;
			comboBoxPatientAnalysisGroup.ShowQuickAdd = true;
			comboBoxPatientAnalysisGroup.Size = new System.Drawing.Size(114, 20);
			comboBoxPatientAnalysisGroup.TabIndex = 140;
			comboBoxPatientAnalysisGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel72.AutoSize = true;
			mmLabel72.BackColor = System.Drawing.Color.Transparent;
			mmLabel72.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel72.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel72.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel72.IsFieldHeader = false;
			mmLabel72.IsRequired = false;
			mmLabel72.Location = new System.Drawing.Point(6, 28);
			mmLabel72.Name = "mmLabel72";
			mmLabel72.PenWidth = 1f;
			mmLabel72.ShowBorder = false;
			mmLabel72.Size = new System.Drawing.Size(39, 13);
			mmLabel72.TabIndex = 139;
			mmLabel72.Text = "Prefix:";
			mmLabel72.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			textboxPatientanalysisPrefix.BackColor = System.Drawing.Color.White;
			textboxPatientanalysisPrefix.CustomReportFieldName = "";
			textboxPatientanalysisPrefix.CustomReportKey = "";
			textboxPatientanalysisPrefix.CustomReportValueType = 1;
			textboxPatientanalysisPrefix.IsComboTextBox = false;
			textboxPatientanalysisPrefix.IsModified = false;
			textboxPatientanalysisPrefix.IsRequired = true;
			textboxPatientanalysisPrefix.Location = new System.Drawing.Point(91, 25);
			textboxPatientanalysisPrefix.MaxLength = 15;
			textboxPatientanalysisPrefix.Name = "textboxPatientanalysisPrefix";
			textboxPatientanalysisPrefix.Size = new System.Drawing.Size(114, 20);
			textboxPatientanalysisPrefix.TabIndex = 138;
			mmLabel80.AutoSize = true;
			mmLabel80.BackColor = System.Drawing.Color.Transparent;
			mmLabel80.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel80.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel80.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel80.IsFieldHeader = false;
			mmLabel80.IsRequired = false;
			mmLabel80.Location = new System.Drawing.Point(5, 6);
			mmLabel80.Name = "mmLabel80";
			mmLabel80.PenWidth = 1f;
			mmLabel80.ShowBorder = false;
			mmLabel80.Size = new System.Drawing.Size(82, 13);
			mmLabel80.TabIndex = 12;
			mmLabel80.Text = "Analysis Group:";
			mmLabel80.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox41.Controls.Add(radioButton15);
			groupBox41.Controls.Add(radioButton16);
			groupBox41.Controls.Add(mmLabel81);
			groupBox41.Location = new System.Drawing.Point(0, 107);
			groupBox41.Name = "groupBox41";
			groupBox41.Size = new System.Drawing.Size(372, 89);
			groupBox41.TabIndex = 135;
			groupBox41.TabStop = false;
			groupBox41.Text = "PDC Maturity Process";
			radioButton15.AutoSize = true;
			radioButton15.Checked = true;
			radioButton15.Location = new System.Drawing.Point(13, 36);
			radioButton15.Name = "radioButton15";
			radioButton15.Size = new System.Drawing.Size(120, 17);
			radioButton15.TabIndex = 1;
			radioButton15.TabStop = true;
			radioButton15.Text = "Direct Maturity Entry";
			radioButton15.UseVisualStyleBackColor = true;
			radioButton16.AutoSize = true;
			radioButton16.Location = new System.Drawing.Point(13, 59);
			radioButton16.Name = "radioButton16";
			radioButton16.Size = new System.Drawing.Size(214, 17);
			radioButton16.TabIndex = 2;
			radioButton16.Text = "Send Cheques to Bank -> Maturity Entry";
			radioButton16.UseVisualStyleBackColor = true;
			mmLabel81.AutoSize = true;
			mmLabel81.BackColor = System.Drawing.Color.Transparent;
			mmLabel81.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel81.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel81.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel81.IsFieldHeader = false;
			mmLabel81.IsRequired = false;
			mmLabel81.Location = new System.Drawing.Point(10, 16);
			mmLabel81.Name = "mmLabel81";
			mmLabel81.PenWidth = 1f;
			mmLabel81.ShowBorder = false;
			mmLabel81.Size = new System.Drawing.Size(214, 13);
			mmLabel81.TabIndex = 0;
			mmLabel81.Text = "How do you want to process PDC maturity:";
			mmLabel81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCustomerFlagNames.Image = Micromind.ClientUI.Properties.Resources.flagred;
			buttonCustomerFlagNames.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			buttonCustomerFlagNames.Location = new System.Drawing.Point(409, 396);
			buttonCustomerFlagNames.Name = "buttonCustomerFlagNames";
			buttonCustomerFlagNames.Size = new System.Drawing.Size(124, 23);
			buttonCustomerFlagNames.TabIndex = 15;
			buttonCustomerFlagNames.Text = "Flag Names...";
			buttonCustomerFlagNames.UseVisualStyleBackColor = true;
			buttonCustomerFlagNames.Click += new System.EventHandler(buttonCustomerFlagNames_Click);
			groupBox12.Controls.Add(checkBoxTrackConsignOutItemsSale);
			groupBox12.Location = new System.Drawing.Point(409, 15);
			groupBox12.Name = "groupBox12";
			groupBox12.Size = new System.Drawing.Size(302, 60);
			groupBox12.TabIndex = 14;
			groupBox12.TabStop = false;
			groupBox12.Text = "Consignment Out";
			checkBoxTrackConsignOutItemsSale.Location = new System.Drawing.Point(6, 18);
			checkBoxTrackConsignOutItemsSale.Name = "checkBoxTrackConsignOutItemsSale";
			checkBoxTrackConsignOutItemsSale.Size = new System.Drawing.Size(289, 24);
			checkBoxTrackConsignOutItemsSale.TabIndex = 2;
			checkBoxTrackConsignOutItemsSale.Text = "Track detailed sales of consignment out items";
			checkBoxTrackConsignOutItemsSale.UseVisualStyleBackColor = true;
			groupBox10.Controls.Add(checkBoxInvoiceNegativeQty);
			groupBox10.Controls.Add(checkBoxLSAllowReturnChangePrice);
			groupBox10.Controls.Add(checkBoxAllowLSReturnWithoutInvoice);
			groupBox10.Location = new System.Drawing.Point(8, 237);
			groupBox10.Name = "groupBox10";
			groupBox10.Size = new System.Drawing.Size(396, 83);
			groupBox10.TabIndex = 13;
			groupBox10.TabStop = false;
			groupBox10.Text = "Sales Return";
			checkBoxInvoiceNegativeQty.AutoSize = true;
			checkBoxInvoiceNegativeQty.Location = new System.Drawing.Point(6, 53);
			checkBoxInvoiceNegativeQty.Name = "checkBoxInvoiceNegativeQty";
			checkBoxInvoiceNegativeQty.Size = new System.Drawing.Size(249, 17);
			checkBoxInvoiceNegativeQty.TabIndex = 2;
			checkBoxInvoiceNegativeQty.Text = "Allow to enter negative quantity in sales invoice";
			checkBoxInvoiceNegativeQty.UseVisualStyleBackColor = true;
			checkBoxLSAllowReturnChangePrice.AutoSize = true;
			checkBoxLSAllowReturnChangePrice.Location = new System.Drawing.Point(6, 36);
			checkBoxLSAllowReturnChangePrice.Name = "checkBoxLSAllowReturnChangePrice";
			checkBoxLSAllowReturnChangePrice.Size = new System.Drawing.Size(196, 17);
			checkBoxLSAllowReturnChangePrice.TabIndex = 1;
			checkBoxLSAllowReturnChangePrice.Text = "Allow to change price in sales return";
			checkBoxLSAllowReturnChangePrice.UseVisualStyleBackColor = true;
			checkBoxAllowLSReturnWithoutInvoice.AutoSize = true;
			checkBoxAllowLSReturnWithoutInvoice.Location = new System.Drawing.Point(6, 19);
			checkBoxAllowLSReturnWithoutInvoice.Name = "checkBoxAllowLSReturnWithoutInvoice";
			checkBoxAllowLSReturnWithoutInvoice.Size = new System.Drawing.Size(276, 17);
			checkBoxAllowLSReturnWithoutInvoice.TabIndex = 0;
			checkBoxAllowLSReturnWithoutInvoice.Text = "Allow entring returns without selecting a sales invoice";
			checkBoxAllowLSReturnWithoutInvoice.UseVisualStyleBackColor = true;
			groupBox4.Controls.Add(checkBoxDisableCustomerCreditLimit);
			groupBox4.Controls.Add(checkBoxMaterialReservationOnSo);
			groupBox4.Controls.Add(checkBoxB2Cbased);
			groupBox4.Controls.Add(checkBoxEnableTempSaving);
			groupBox4.Controls.Add(checkBoxExcludeZeroQtyInDN);
			groupBox4.Controls.Add(checkBoxAllowCustomerChangeInDN);
			groupBox4.Controls.Add(checkBoxOnlyOpenInvoice);
			groupBox4.Controls.Add(checkBoxAllowcreditsaleinSalesReceipt);
			groupBox4.Controls.Add(checkBoxTakelastSalesprice);
			groupBox4.Controls.Add(checkBoxShowItemdetail);
			groupBox4.Controls.Add(checkBoxShowLotdetailinPrintout);
			groupBox4.Controls.Add(checkBoxInlineDiscount);
			groupBox4.Controls.Add(checkBoxIncludePDC);
			groupBox4.Location = new System.Drawing.Point(409, 81);
			groupBox4.Name = "groupBox4";
			groupBox4.Size = new System.Drawing.Size(302, 304);
			groupBox4.TabIndex = 3;
			groupBox4.TabStop = false;
			groupBox4.Text = "Other Options";
			checkBoxDisableCustomerCreditLimit.Location = new System.Drawing.Point(11, 263);
			checkBoxDisableCustomerCreditLimit.Name = "checkBoxDisableCustomerCreditLimit";
			checkBoxDisableCustomerCreditLimit.Size = new System.Drawing.Size(221, 32);
			checkBoxDisableCustomerCreditLimit.TabIndex = 12;
			checkBoxDisableCustomerCreditLimit.Text = "Disable Customer Credit Limit Details on Customer Master";
			checkBoxDisableCustomerCreditLimit.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			checkBoxDisableCustomerCreditLimit.UseVisualStyleBackColor = true;
			checkBoxMaterialReservationOnSo.Location = new System.Drawing.Point(11, 247);
			checkBoxMaterialReservationOnSo.Name = "checkBoxMaterialReservationOnSo";
			checkBoxMaterialReservationOnSo.Size = new System.Drawing.Size(158, 17);
			checkBoxMaterialReservationOnSo.TabIndex = 11;
			checkBoxMaterialReservationOnSo.Text = "Material Reservation on SO";
			checkBoxMaterialReservationOnSo.UseVisualStyleBackColor = true;
			checkBoxB2Cbased.Location = new System.Drawing.Point(11, 227);
			checkBoxB2Cbased.Name = "checkBoxB2Cbased";
			checkBoxB2Cbased.Size = new System.Drawing.Size(158, 17);
			checkBoxB2Cbased.TabIndex = 10;
			checkBoxB2Cbased.Text = "Based on B2C ";
			checkBoxB2Cbased.UseVisualStyleBackColor = true;
			checkBoxEnableTempSaving.Location = new System.Drawing.Point(11, 210);
			checkBoxEnableTempSaving.Name = "checkBoxEnableTempSaving";
			checkBoxEnableTempSaving.Size = new System.Drawing.Size(221, 17);
			checkBoxEnableTempSaving.TabIndex = 9;
			checkBoxEnableTempSaving.Text = "Enable Temporary Saving";
			checkBoxEnableTempSaving.UseVisualStyleBackColor = true;
			checkBoxExcludeZeroQtyInDN.Location = new System.Drawing.Point(11, 192);
			checkBoxExcludeZeroQtyInDN.Name = "checkBoxExcludeZeroQtyInDN";
			checkBoxExcludeZeroQtyInDN.Size = new System.Drawing.Size(221, 17);
			checkBoxExcludeZeroQtyInDN.TabIndex = 8;
			checkBoxExcludeZeroQtyInDN.Text = "Exclude Zero Quantity in DN";
			checkBoxExcludeZeroQtyInDN.UseVisualStyleBackColor = true;
			checkBoxAllowCustomerChangeInDN.Location = new System.Drawing.Point(11, 174);
			checkBoxAllowCustomerChangeInDN.Name = "checkBoxAllowCustomerChangeInDN";
			checkBoxAllowCustomerChangeInDN.Size = new System.Drawing.Size(221, 17);
			checkBoxAllowCustomerChangeInDN.TabIndex = 7;
			checkBoxAllowCustomerChangeInDN.Text = "Allow to change customer in DN";
			checkBoxAllowCustomerChangeInDN.UseVisualStyleBackColor = true;
			checkBoxOnlyOpenInvoice.Location = new System.Drawing.Point(11, 156);
			checkBoxOnlyOpenInvoice.Name = "checkBoxOnlyOpenInvoice";
			checkBoxOnlyOpenInvoice.Size = new System.Drawing.Size(221, 17);
			checkBoxOnlyOpenInvoice.TabIndex = 6;
			checkBoxOnlyOpenInvoice.Text = "Show Only Open Invoice in Statement";
			checkBoxOnlyOpenInvoice.UseVisualStyleBackColor = true;
			checkBoxAllowcreditsaleinSalesReceipt.Location = new System.Drawing.Point(11, 137);
			checkBoxAllowcreditsaleinSalesReceipt.Name = "checkBoxAllowcreditsaleinSalesReceipt";
			checkBoxAllowcreditsaleinSalesReceipt.Size = new System.Drawing.Size(221, 17);
			checkBoxAllowcreditsaleinSalesReceipt.TabIndex = 5;
			checkBoxAllowcreditsaleinSalesReceipt.Text = "Allow credit Sale in Sales receipt";
			checkBoxAllowcreditsaleinSalesReceipt.UseVisualStyleBackColor = true;
			checkBoxTakelastSalesprice.Location = new System.Drawing.Point(11, 117);
			checkBoxTakelastSalesprice.Name = "checkBoxTakelastSalesprice";
			checkBoxTakelastSalesprice.Size = new System.Drawing.Size(221, 17);
			checkBoxTakelastSalesprice.TabIndex = 4;
			checkBoxTakelastSalesprice.Text = "Take last price in Sale transactions";
			checkBoxTakelastSalesprice.UseVisualStyleBackColor = true;
			checkBoxShowItemdetail.Location = new System.Drawing.Point(11, 92);
			checkBoxShowItemdetail.Name = "checkBoxShowItemdetail";
			checkBoxShowItemdetail.Size = new System.Drawing.Size(207, 26);
			checkBoxShowItemdetail.TabIndex = 3;
			checkBoxShowItemdetail.Text = "Show Transaction History";
			checkBoxShowItemdetail.UseVisualStyleBackColor = true;
			checkBoxShowLotdetailinPrintout.Location = new System.Drawing.Point(11, 76);
			checkBoxShowLotdetailinPrintout.Name = "checkBoxShowLotdetailinPrintout";
			checkBoxShowLotdetailinPrintout.Size = new System.Drawing.Size(182, 15);
			checkBoxShowLotdetailinPrintout.TabIndex = 2;
			checkBoxShowLotdetailinPrintout.Text = "Show Lot detail in Printout";
			checkBoxShowLotdetailinPrintout.UseVisualStyleBackColor = true;
			checkBoxInlineDiscount.Location = new System.Drawing.Point(11, 50);
			checkBoxInlineDiscount.Name = "checkBoxInlineDiscount";
			checkBoxInlineDiscount.Size = new System.Drawing.Size(159, 24);
			checkBoxInlineDiscount.TabIndex = 1;
			checkBoxInlineDiscount.Text = "Use Inline Discount";
			checkBoxInlineDiscount.UseVisualStyleBackColor = true;
			checkBoxIncludePDC.Location = new System.Drawing.Point(11, 16);
			checkBoxIncludePDC.Name = "checkBoxIncludePDC";
			checkBoxIncludePDC.Size = new System.Drawing.Size(222, 35);
			checkBoxIncludePDC.TabIndex = 0;
			checkBoxIncludePDC.Text = "Do not Include Post-Dated cheques when checking for credit limits";
			checkBoxIncludePDC.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			checkBoxIncludePDC.UseVisualStyleBackColor = true;
			groupBox2.Controls.Add(checkBoxNotallowzeroinSales);
			groupBox2.Controls.Add(mmLabel30);
			groupBox2.Controls.Add(comboBoxsaleslessthanCostAction);
			groupBox2.Controls.Add(mmLabel35);
			groupBox2.Controls.Add(panelsalespricelessthancost);
			groupBox2.Controls.Add(checkBoxDeliveryNoteCL);
			groupBox2.Controls.Add(panelNegativeQuantityPassword);
			groupBox2.Controls.Add(panelOverCLPassword);
			groupBox2.Controls.Add(panelMinPricePassword);
			groupBox2.Controls.Add(mmLabel25);
			groupBox2.Controls.Add(mmLabel22);
			groupBox2.Controls.Add(mmLabel19);
			groupBox2.Controls.Add(comboBoxMinusNegativeQuantityAction);
			groupBox2.Controls.Add(comboBoxOverCLAction);
			groupBox2.Controls.Add(mmLabel24);
			groupBox2.Controls.Add(mmLabel21);
			groupBox2.Controls.Add(comboBoxMinPriceAction);
			groupBox2.Controls.Add(mmLabel18);
			groupBox2.Location = new System.Drawing.Point(8, 326);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(396, 248);
			groupBox2.TabIndex = 1;
			groupBox2.TabStop = false;
			groupBox2.Text = "Sales Invoicing Options";
			checkBoxNotallowzeroinSales.AutoSize = true;
			checkBoxNotallowzeroinSales.Location = new System.Drawing.Point(6, 225);
			checkBoxNotallowzeroinSales.Name = "checkBoxNotallowzeroinSales";
			checkBoxNotallowzeroinSales.Size = new System.Drawing.Size(215, 17);
			checkBoxNotallowzeroinSales.TabIndex = 25;
			checkBoxNotallowzeroinSales.Text = "Do not Allow zero price in sales invoices";
			checkBoxNotallowzeroinSales.UseVisualStyleBackColor = true;
			mmLabel30.AutoSize = true;
			mmLabel30.BackColor = System.Drawing.Color.Transparent;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(18, 171);
			mmLabel30.Name = "mmLabel30";
			mmLabel30.PenWidth = 1f;
			mmLabel30.ShowBorder = false;
			mmLabel30.Size = new System.Drawing.Size(41, 13);
			mmLabel30.TabIndex = 23;
			mmLabel30.Text = "Action:";
			mmLabel30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			comboBoxsaleslessthanCostAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxsaleslessthanCostAction.FormattingEnabled = true;
			comboBoxsaleslessthanCostAction.HasPasswordItem = true;
			comboBoxsaleslessthanCostAction.Location = new System.Drawing.Point(64, 168);
			comboBoxsaleslessthanCostAction.Name = "comboBoxsaleslessthanCostAction";
			comboBoxsaleslessthanCostAction.Size = new System.Drawing.Size(119, 21);
			comboBoxsaleslessthanCostAction.TabIndex = 21;
			comboBoxsaleslessthanCostAction.SelectedIndexChanged += new System.EventHandler(comboBoxsaleslessthanCostAction_SelectedIndexChanged);
			mmLabel35.AutoSize = true;
			mmLabel35.BackColor = System.Drawing.Color.Transparent;
			mmLabel35.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel35.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel35.IsFieldHeader = false;
			mmLabel35.IsRequired = false;
			mmLabel35.Location = new System.Drawing.Point(8, 152);
			mmLabel35.Name = "mmLabel35";
			mmLabel35.PenWidth = 1f;
			mmLabel35.ShowBorder = false;
			mmLabel35.Size = new System.Drawing.Size(129, 13);
			mmLabel35.TabIndex = 24;
			mmLabel35.Text = "Sales price less than Cost";
			mmLabel35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			panelsalespricelessthancost.Controls.Add(textBoxPricelessthancostpassword);
			panelsalespricelessthancost.Controls.Add(mmLabel36);
			panelsalespricelessthancost.Location = new System.Drawing.Point(186, 167);
			panelsalespricelessthancost.Name = "panelsalespricelessthancost";
			panelsalespricelessthancost.Size = new System.Drawing.Size(178, 23);
			panelsalespricelessthancost.TabIndex = 22;
			panelsalespricelessthancost.Visible = false;
			textBoxPricelessthancostpassword.BackColor = System.Drawing.Color.White;
			textBoxPricelessthancostpassword.CustomReportFieldName = "";
			textBoxPricelessthancostpassword.CustomReportKey = "";
			textBoxPricelessthancostpassword.CustomReportValueType = 1;
			textBoxPricelessthancostpassword.IsComboTextBox = false;
			textBoxPricelessthancostpassword.IsModified = false;
			textBoxPricelessthancostpassword.IsRequired = true;
			textBoxPricelessthancostpassword.Location = new System.Drawing.Point(59, 1);
			textBoxPricelessthancostpassword.MaxLength = 5;
			textBoxPricelessthancostpassword.Name = "textBoxPricelessthancostpassword";
			textBoxPricelessthancostpassword.Size = new System.Drawing.Size(109, 20);
			textBoxPricelessthancostpassword.TabIndex = 0;
			textBoxPricelessthancostpassword.UseSystemPasswordChar = true;
			mmLabel36.AutoSize = true;
			mmLabel36.BackColor = System.Drawing.Color.Transparent;
			mmLabel36.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel36.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel36.IsFieldHeader = false;
			mmLabel36.IsRequired = false;
			mmLabel36.Location = new System.Drawing.Point(3, 4);
			mmLabel36.Name = "mmLabel36";
			mmLabel36.PenWidth = 1f;
			mmLabel36.ShowBorder = false;
			mmLabel36.Size = new System.Drawing.Size(57, 13);
			mmLabel36.TabIndex = 19;
			mmLabel36.Text = "Password:";
			mmLabel36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxDeliveryNoteCL.AutoSize = true;
			checkBoxDeliveryNoteCL.Location = new System.Drawing.Point(6, 203);
			checkBoxDeliveryNoteCL.Name = "checkBoxDeliveryNoteCL";
			checkBoxDeliveryNoteCL.Size = new System.Drawing.Size(230, 17);
			checkBoxDeliveryNoteCL.TabIndex = 20;
			checkBoxDeliveryNoteCL.Text = "Check customer credit limit in Delivery Note";
			checkBoxDeliveryNoteCL.UseVisualStyleBackColor = true;
			panelNegativeQuantityPassword.Controls.Add(textBoxNegativeQuantityPassword);
			panelNegativeQuantityPassword.Controls.Add(mmLabel26);
			panelNegativeQuantityPassword.Location = new System.Drawing.Point(185, 126);
			panelNegativeQuantityPassword.Name = "panelNegativeQuantityPassword";
			panelNegativeQuantityPassword.Size = new System.Drawing.Size(178, 23);
			panelNegativeQuantityPassword.TabIndex = 5;
			panelNegativeQuantityPassword.Visible = false;
			textBoxNegativeQuantityPassword.BackColor = System.Drawing.Color.White;
			textBoxNegativeQuantityPassword.CustomReportFieldName = "";
			textBoxNegativeQuantityPassword.CustomReportKey = "";
			textBoxNegativeQuantityPassword.CustomReportValueType = 1;
			textBoxNegativeQuantityPassword.IsComboTextBox = false;
			textBoxNegativeQuantityPassword.IsModified = false;
			textBoxNegativeQuantityPassword.IsRequired = true;
			textBoxNegativeQuantityPassword.Location = new System.Drawing.Point(59, 1);
			textBoxNegativeQuantityPassword.MaxLength = 5;
			textBoxNegativeQuantityPassword.Name = "textBoxNegativeQuantityPassword";
			textBoxNegativeQuantityPassword.Size = new System.Drawing.Size(109, 20);
			textBoxNegativeQuantityPassword.TabIndex = 0;
			textBoxNegativeQuantityPassword.UseSystemPasswordChar = true;
			mmLabel26.AutoSize = true;
			mmLabel26.BackColor = System.Drawing.Color.Transparent;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(3, 4);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(57, 13);
			mmLabel26.TabIndex = 19;
			mmLabel26.Text = "Password:";
			mmLabel26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			panelOverCLPassword.Controls.Add(textBoxOverCLPassword);
			panelOverCLPassword.Controls.Add(mmLabel23);
			panelOverCLPassword.Location = new System.Drawing.Point(185, 73);
			panelOverCLPassword.Name = "panelOverCLPassword";
			panelOverCLPassword.Size = new System.Drawing.Size(178, 30);
			panelOverCLPassword.TabIndex = 3;
			panelOverCLPassword.Visible = false;
			textBoxOverCLPassword.BackColor = System.Drawing.Color.White;
			textBoxOverCLPassword.CustomReportFieldName = "";
			textBoxOverCLPassword.CustomReportKey = "";
			textBoxOverCLPassword.CustomReportValueType = 1;
			textBoxOverCLPassword.IsComboTextBox = false;
			textBoxOverCLPassword.IsModified = false;
			textBoxOverCLPassword.IsRequired = true;
			textBoxOverCLPassword.Location = new System.Drawing.Point(59, 8);
			textBoxOverCLPassword.MaxLength = 5;
			textBoxOverCLPassword.Name = "textBoxOverCLPassword";
			textBoxOverCLPassword.Size = new System.Drawing.Size(109, 20);
			textBoxOverCLPassword.TabIndex = 0;
			textBoxOverCLPassword.UseSystemPasswordChar = true;
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(3, 11);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(57, 13);
			mmLabel23.TabIndex = 19;
			mmLabel23.Text = "Password:";
			mmLabel23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			panelMinPricePassword.Controls.Add(textBoxMinPricePassword);
			panelMinPricePassword.Controls.Add(mmLabel20);
			panelMinPricePassword.Location = new System.Drawing.Point(185, 29);
			panelMinPricePassword.Name = "panelMinPricePassword";
			panelMinPricePassword.Size = new System.Drawing.Size(178, 30);
			panelMinPricePassword.TabIndex = 1;
			panelMinPricePassword.Visible = false;
			textBoxMinPricePassword.BackColor = System.Drawing.Color.White;
			textBoxMinPricePassword.CustomReportFieldName = "";
			textBoxMinPricePassword.CustomReportKey = "";
			textBoxMinPricePassword.CustomReportValueType = 1;
			textBoxMinPricePassword.IsComboTextBox = false;
			textBoxMinPricePassword.IsModified = false;
			textBoxMinPricePassword.IsRequired = true;
			textBoxMinPricePassword.Location = new System.Drawing.Point(59, 8);
			textBoxMinPricePassword.MaxLength = 5;
			textBoxMinPricePassword.Name = "textBoxMinPricePassword";
			textBoxMinPricePassword.Size = new System.Drawing.Size(109, 20);
			textBoxMinPricePassword.TabIndex = 0;
			textBoxMinPricePassword.UseSystemPasswordChar = true;
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(3, 11);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(57, 13);
			mmLabel20.TabIndex = 19;
			mmLabel20.Text = "Password:";
			mmLabel20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(17, 130);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(41, 13);
			mmLabel25.TabIndex = 19;
			mmLabel25.Text = "Action:";
			mmLabel25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(17, 84);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(41, 13);
			mmLabel22.TabIndex = 19;
			mmLabel22.Text = "Action:";
			mmLabel22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(17, 40);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(41, 13);
			mmLabel19.TabIndex = 19;
			mmLabel19.Text = "Action:";
			mmLabel19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			comboBoxMinusNegativeQuantityAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMinusNegativeQuantityAction.FormattingEnabled = true;
			comboBoxMinusNegativeQuantityAction.HasPasswordItem = false;
			comboBoxMinusNegativeQuantityAction.Location = new System.Drawing.Point(63, 127);
			comboBoxMinusNegativeQuantityAction.Name = "comboBoxMinusNegativeQuantityAction";
			comboBoxMinusNegativeQuantityAction.Size = new System.Drawing.Size(119, 21);
			comboBoxMinusNegativeQuantityAction.TabIndex = 4;
			comboBoxMinusNegativeQuantityAction.SelectedIndexChanged += new System.EventHandler(comboBoxMinusNegativeQuantityAction_SelectedIndexChanged);
			comboBoxOverCLAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxOverCLAction.FormattingEnabled = true;
			comboBoxOverCLAction.HasPasswordItem = true;
			comboBoxOverCLAction.Location = new System.Drawing.Point(63, 81);
			comboBoxOverCLAction.Name = "comboBoxOverCLAction";
			comboBoxOverCLAction.Size = new System.Drawing.Size(119, 21);
			comboBoxOverCLAction.TabIndex = 2;
			comboBoxOverCLAction.SelectedIndexChanged += new System.EventHandler(comboBoxOverCLAction_SelectedIndexChanged);
			mmLabel24.AutoSize = true;
			mmLabel24.BackColor = System.Drawing.Color.Transparent;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(7, 111);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(249, 13);
			mmLabel24.TabIndex = 19;
			mmLabel24.Text = "Insufficient Quantity Onhand - Negative Quantity:";
			mmLabel24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel21.AutoSize = true;
			mmLabel21.BackColor = System.Drawing.Color.Transparent;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(7, 65);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(175, 13);
			mmLabel21.TabIndex = 19;
			mmLabel21.Text = "Sales Over Customer's Credit Limit:";
			mmLabel21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			comboBoxMinPriceAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMinPriceAction.FormattingEnabled = true;
			comboBoxMinPriceAction.HasPasswordItem = true;
			comboBoxMinPriceAction.Location = new System.Drawing.Point(63, 37);
			comboBoxMinPriceAction.Name = "comboBoxMinPriceAction";
			comboBoxMinPriceAction.Size = new System.Drawing.Size(119, 21);
			comboBoxMinPriceAction.TabIndex = 0;
			comboBoxMinPriceAction.SelectedIndexChanged += new System.EventHandler(optionsAllowComboBox1_SelectedIndexChanged);
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(7, 21);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(136, 13);
			mmLabel18.TabIndex = 19;
			mmLabel18.Text = "Sales Below Minimum Price:";
			mmLabel18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox1.Controls.Add(checkBoxESAllowtocreatepicklist);
			groupBox1.Controls.Add(checkBoxESAllowDNWithoutInvoice);
			groupBox1.Controls.Add(checkBoxLSAllowDNWithoutInvoice);
			groupBox1.Controls.Add(comboBoxExportSalesFlow);
			groupBox1.Controls.Add(checkBoxESAllowMoreQTY);
			groupBox1.Controls.Add(checkBoxESAllowWithoutSO);
			groupBox1.Controls.Add(checkBoxESAllowAddNew);
			groupBox1.Controls.Add(mmLabel34);
			groupBox1.Controls.Add(checkBoxLSAllowInvoiceMore);
			groupBox1.Controls.Add(checkBoxLSAllowWithoutSO);
			groupBox1.Controls.Add(comboBoxLocalSalesFlow);
			groupBox1.Controls.Add(checkBoxLSAllowAddNew);
			groupBox1.Controls.Add(mmLabel32);
			groupBox1.Location = new System.Drawing.Point(10, 3);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(393, 232);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Sales Flow";
			checkBoxESAllowtocreatepicklist.AutoSize = true;
			checkBoxESAllowtocreatepicklist.Location = new System.Drawing.Point(101, 211);
			checkBoxESAllowtocreatepicklist.Name = "checkBoxESAllowtocreatepicklist";
			checkBoxESAllowtocreatepicklist.Size = new System.Drawing.Size(213, 17);
			checkBoxESAllowtocreatepicklist.TabIndex = 12;
			checkBoxESAllowtocreatepicklist.Text = "Allow to create Picklist from Sales Order";
			checkBoxESAllowtocreatepicklist.UseVisualStyleBackColor = true;
			checkBoxESAllowDNWithoutInvoice.AutoSize = true;
			checkBoxESAllowDNWithoutInvoice.Location = new System.Drawing.Point(101, 194);
			checkBoxESAllowDNWithoutInvoice.Name = "checkBoxESAllowDNWithoutInvoice";
			checkBoxESAllowDNWithoutInvoice.Size = new System.Drawing.Size(227, 17);
			checkBoxESAllowDNWithoutInvoice.TabIndex = 11;
			checkBoxESAllowDNWithoutInvoice.Text = "Allow to issue delivery note without invoice";
			checkBoxESAllowDNWithoutInvoice.UseVisualStyleBackColor = true;
			checkBoxLSAllowDNWithoutInvoice.AutoSize = true;
			checkBoxLSAllowDNWithoutInvoice.Location = new System.Drawing.Point(101, 95);
			checkBoxLSAllowDNWithoutInvoice.Name = "checkBoxLSAllowDNWithoutInvoice";
			checkBoxLSAllowDNWithoutInvoice.Size = new System.Drawing.Size(227, 17);
			checkBoxLSAllowDNWithoutInvoice.TabIndex = 4;
			checkBoxLSAllowDNWithoutInvoice.Text = "Allow to issue delivery note without invoice";
			checkBoxLSAllowDNWithoutInvoice.UseVisualStyleBackColor = true;
			comboBoxExportSalesFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxExportSalesFlow.FormattingEnabled = true;
			comboBoxExportSalesFlow.Items.AddRange(new object[3]
			{
				"Direct Sales Invoice",
				"Sales Order -> Sales Invoice ->Delivery Note",
				"Sales Order -> Delivery Note -> Sales Invoice"
			});
			comboBoxExportSalesFlow.Location = new System.Drawing.Point(101, 114);
			comboBoxExportSalesFlow.Name = "comboBoxExportSalesFlow";
			comboBoxExportSalesFlow.Size = new System.Drawing.Size(261, 21);
			comboBoxExportSalesFlow.TabIndex = 5;
			checkBoxESAllowMoreQTY.AutoSize = true;
			checkBoxESAllowMoreQTY.Location = new System.Drawing.Point(101, 177);
			checkBoxESAllowMoreQTY.Name = "checkBoxESAllowMoreQTY";
			checkBoxESAllowMoreQTY.Size = new System.Drawing.Size(229, 17);
			checkBoxESAllowMoreQTY.TabIndex = 8;
			checkBoxESAllowMoreQTY.Text = "Allow to sell more than Sales Order quantity";
			checkBoxESAllowMoreQTY.UseVisualStyleBackColor = true;
			checkBoxESAllowWithoutSO.AutoSize = true;
			checkBoxESAllowWithoutSO.Location = new System.Drawing.Point(101, 159);
			checkBoxESAllowWithoutSO.Name = "checkBoxESAllowWithoutSO";
			checkBoxESAllowWithoutSO.Size = new System.Drawing.Size(250, 17);
			checkBoxESAllowWithoutSO.TabIndex = 7;
			checkBoxESAllowWithoutSO.Text = "Allow to create DN/Invoice without Sales Order";
			checkBoxESAllowWithoutSO.UseVisualStyleBackColor = true;
			checkBoxESAllowAddNew.AutoSize = true;
			checkBoxESAllowAddNew.Location = new System.Drawing.Point(101, 141);
			checkBoxESAllowAddNew.Name = "checkBoxESAllowAddNew";
			checkBoxESAllowAddNew.Size = new System.Drawing.Size(237, 17);
			checkBoxESAllowAddNew.TabIndex = 6;
			checkBoxESAllowAddNew.Text = "Allow to add items that are not in Sales Order";
			checkBoxESAllowAddNew.UseVisualStyleBackColor = true;
			mmLabel34.AutoSize = true;
			mmLabel34.BackColor = System.Drawing.Color.Transparent;
			mmLabel34.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel34.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel34.IsFieldHeader = false;
			mmLabel34.IsRequired = false;
			mmLabel34.Location = new System.Drawing.Point(6, 117);
			mmLabel34.Name = "mmLabel34";
			mmLabel34.PenWidth = 1f;
			mmLabel34.ShowBorder = false;
			mmLabel34.Size = new System.Drawing.Size(96, 13);
			mmLabel34.TabIndex = 10;
			mmLabel34.Text = "Export Sales Flow:";
			mmLabel34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxLSAllowInvoiceMore.AutoSize = true;
			checkBoxLSAllowInvoiceMore.Location = new System.Drawing.Point(101, 79);
			checkBoxLSAllowInvoiceMore.Name = "checkBoxLSAllowInvoiceMore";
			checkBoxLSAllowInvoiceMore.Size = new System.Drawing.Size(229, 17);
			checkBoxLSAllowInvoiceMore.TabIndex = 3;
			checkBoxLSAllowInvoiceMore.Text = "Allow to sell more than Sales Order quantity";
			checkBoxLSAllowInvoiceMore.UseVisualStyleBackColor = true;
			checkBoxLSAllowWithoutSO.AutoSize = true;
			checkBoxLSAllowWithoutSO.Location = new System.Drawing.Point(101, 63);
			checkBoxLSAllowWithoutSO.Name = "checkBoxLSAllowWithoutSO";
			checkBoxLSAllowWithoutSO.Size = new System.Drawing.Size(250, 17);
			checkBoxLSAllowWithoutSO.TabIndex = 2;
			checkBoxLSAllowWithoutSO.Text = "Allow to create DN/Invoice without Sales Order";
			checkBoxLSAllowWithoutSO.UseVisualStyleBackColor = true;
			comboBoxLocalSalesFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxLocalSalesFlow.FormattingEnabled = true;
			comboBoxLocalSalesFlow.Items.AddRange(new object[3]
			{
				"Direct Sales Invoice",
				"Sales Order -> Sales Invoice ->Delivery Note",
				"Sales Order -> Delivery Note -> Sales Invoice"
			});
			comboBoxLocalSalesFlow.Location = new System.Drawing.Point(101, 22);
			comboBoxLocalSalesFlow.Name = "comboBoxLocalSalesFlow";
			comboBoxLocalSalesFlow.Size = new System.Drawing.Size(261, 21);
			comboBoxLocalSalesFlow.TabIndex = 0;
			checkBoxLSAllowAddNew.AutoSize = true;
			checkBoxLSAllowAddNew.Location = new System.Drawing.Point(101, 47);
			checkBoxLSAllowAddNew.Name = "checkBoxLSAllowAddNew";
			checkBoxLSAllowAddNew.Size = new System.Drawing.Size(237, 17);
			checkBoxLSAllowAddNew.TabIndex = 1;
			checkBoxLSAllowAddNew.Text = "Allow to add items that are not in Sales Order";
			checkBoxLSAllowAddNew.UseVisualStyleBackColor = true;
			mmLabel32.AutoSize = true;
			mmLabel32.BackColor = System.Drawing.Color.Transparent;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel32.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(6, 24);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(88, 13);
			mmLabel32.TabIndex = 5;
			mmLabel32.Text = "Local Sales Flow:";
			mmLabel32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ultraTabPageControl5.Controls.Add(mmLabel58);
			ultraTabPageControl5.Controls.Add(comboBoxoptionsAllocation);
			ultraTabPageControl5.Controls.Add(mmLabel59);
			ultraTabPageControl5.Controls.Add(label10);
			ultraTabPageControl5.Controls.Add(mmLabel56);
			ultraTabPageControl5.Controls.Add(textboxDiscountWriteoffPerc);
			ultraTabPageControl5.Controls.Add(groupBox9);
			ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl5.Name = "ultraTabPageControl5";
			ultraTabPageControl5.Size = new System.Drawing.Size(807, 626);
			mmLabel58.AutoSize = true;
			mmLabel58.BackColor = System.Drawing.Color.Transparent;
			mmLabel58.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel58.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel58.IsFieldHeader = false;
			mmLabel58.IsRequired = false;
			mmLabel58.Location = new System.Drawing.Point(24, 376);
			mmLabel58.Name = "mmLabel58";
			mmLabel58.PenWidth = 1f;
			mmLabel58.ShowBorder = false;
			mmLabel58.Size = new System.Drawing.Size(41, 13);
			mmLabel58.TabIndex = 23;
			mmLabel58.Text = "Action:";
			mmLabel58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			comboBoxoptionsAllocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxoptionsAllocation.FormattingEnabled = true;
			comboBoxoptionsAllocation.HasPasswordItem = false;
			comboBoxoptionsAllocation.Items.AddRange(new object[2]
			{
				"Allow",
				"Don't Allow "
			});
			comboBoxoptionsAllocation.Location = new System.Drawing.Point(70, 373);
			comboBoxoptionsAllocation.Name = "comboBoxoptionsAllocation";
			comboBoxoptionsAllocation.Size = new System.Drawing.Size(119, 21);
			comboBoxoptionsAllocation.TabIndex = 22;
			mmLabel59.AutoSize = true;
			mmLabel59.BackColor = System.Drawing.Color.Transparent;
			mmLabel59.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel59.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel59.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel59.IsFieldHeader = false;
			mmLabel59.IsRequired = false;
			mmLabel59.Location = new System.Drawing.Point(14, 357);
			mmLabel59.Name = "mmLabel59";
			mmLabel59.PenWidth = 1f;
			mmLabel59.ShowBorder = false;
			mmLabel59.Size = new System.Drawing.Size(255, 13);
			mmLabel59.TabIndex = 24;
			mmLabel59.Text = "Automatically remove allocation on transaction edit:";
			mmLabel59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(14, 327);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(167, 13);
			label10.TabIndex = 13;
			label10.Text = "Discount+Write off Allocation Max";
			mmLabel56.AutoSize = true;
			mmLabel56.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel56.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel56.IsFieldHeader = false;
			mmLabel56.IsRequired = false;
			mmLabel56.Location = new System.Drawing.Point(217, 329);
			mmLabel56.Name = "mmLabel56";
			mmLabel56.PenWidth = 1f;
			mmLabel56.ShowBorder = false;
			mmLabel56.Size = new System.Drawing.Size(15, 13);
			mmLabel56.TabIndex = 21;
			mmLabel56.Text = "%";
			textboxDiscountWriteoffPerc.AllowDecimal = true;
			textboxDiscountWriteoffPerc.CustomReportFieldName = "";
			textboxDiscountWriteoffPerc.CustomReportKey = "";
			textboxDiscountWriteoffPerc.CustomReportValueType = 1;
			textboxDiscountWriteoffPerc.IsComboTextBox = false;
			textboxDiscountWriteoffPerc.IsModified = false;
			textboxDiscountWriteoffPerc.Location = new System.Drawing.Point(186, 324);
			textboxDiscountWriteoffPerc.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textboxDiscountWriteoffPerc.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textboxDiscountWriteoffPerc.Name = "textboxDiscountWriteoffPerc";
			textboxDiscountWriteoffPerc.NullText = "0";
			textboxDiscountWriteoffPerc.Size = new System.Drawing.Size(29, 20);
			textboxDiscountWriteoffPerc.TabIndex = 20;
			textboxDiscountWriteoffPerc.Text = "1";
			groupBox9.Controls.Add(radioButtonEOMInvoiceDate);
			groupBox9.Controls.Add(radioButtonAgingEOMDueDate);
			groupBox9.Controls.Add(panelMonth6);
			groupBox9.Controls.Add(panelMonth5);
			groupBox9.Controls.Add(panelMonth4);
			groupBox9.Controls.Add(panelMonth3);
			groupBox9.Controls.Add(panelMonth2);
			groupBox9.Controls.Add(panelMonth1);
			groupBox9.Controls.Add(panelCurrent);
			groupBox9.Controls.Add(checkBoxMonth6);
			groupBox9.Controls.Add(checkBoxMonth5);
			groupBox9.Controls.Add(checkBoxMonth4);
			groupBox9.Controls.Add(checkBoxMonth3);
			groupBox9.Controls.Add(checkBoxMonth2);
			groupBox9.Controls.Add(checkBoxMonth1);
			groupBox9.Controls.Add(label4);
			groupBox9.Controls.Add(label3);
			groupBox9.Controls.Add(radioButtonAgingDate);
			groupBox9.Controls.Add(label2);
			groupBox9.Controls.Add(radioButtonAgingDueDate);
			groupBox9.Location = new System.Drawing.Point(17, 18);
			groupBox9.Name = "groupBox9";
			groupBox9.Size = new System.Drawing.Size(360, 299);
			groupBox9.TabIndex = 0;
			groupBox9.TabStop = false;
			groupBox9.Text = "Aging Setup";
			radioButtonEOMInvoiceDate.AutoSize = true;
			radioButtonEOMInvoiceDate.Location = new System.Drawing.Point(153, 61);
			radioButtonEOMInvoiceDate.Name = "radioButtonEOMInvoiceDate";
			radioButtonEOMInvoiceDate.Size = new System.Drawing.Size(155, 17);
			radioButtonEOMInvoiceDate.TabIndex = 13;
			radioButtonEOMInvoiceDate.Text = "Aging by EOM Invoice date";
			radioButtonEOMInvoiceDate.UseVisualStyleBackColor = true;
			radioButtonAgingEOMDueDate.AutoSize = true;
			radioButtonAgingEOMDueDate.Location = new System.Drawing.Point(9, 61);
			radioButtonAgingEOMDueDate.Name = "radioButtonAgingEOMDueDate";
			radioButtonAgingEOMDueDate.Size = new System.Drawing.Size(138, 17);
			radioButtonAgingEOMDueDate.TabIndex = 12;
			radioButtonAgingEOMDueDate.Text = "Aging by EOM due date";
			radioButtonAgingEOMDueDate.UseVisualStyleBackColor = true;
			panelMonth6.Controls.Add(textBoxToMonth6);
			panelMonth6.Controls.Add(textBoxFromMonth6);
			panelMonth6.Controls.Add(textBoxAgingName6);
			panelMonth6.Location = new System.Drawing.Point(23, 262);
			panelMonth6.Name = "panelMonth6";
			panelMonth6.Size = new System.Drawing.Size(273, 24);
			panelMonth6.TabIndex = 6;
			textBoxToMonth6.AllowDecimal = true;
			textBoxToMonth6.CustomReportFieldName = "";
			textBoxToMonth6.CustomReportKey = "";
			textBoxToMonth6.CustomReportValueType = 1;
			textBoxToMonth6.IsComboTextBox = false;
			textBoxToMonth6.IsModified = false;
			textBoxToMonth6.Location = new System.Drawing.Point(196, 0);
			textBoxToMonth6.MaxLength = 3;
			textBoxToMonth6.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxToMonth6.MinValue = new decimal(new int[4]);
			textBoxToMonth6.Name = "textBoxToMonth6";
			textBoxToMonth6.NullText = "0";
			textBoxToMonth6.Size = new System.Drawing.Size(66, 20);
			textBoxToMonth6.TabIndex = 2;
			textBoxToMonth6.Text = "180";
			textBoxToMonth6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxFromMonth6.AllowDecimal = true;
			textBoxFromMonth6.CustomReportFieldName = "";
			textBoxFromMonth6.CustomReportKey = "";
			textBoxFromMonth6.CustomReportValueType = 1;
			textBoxFromMonth6.IsComboTextBox = false;
			textBoxFromMonth6.IsModified = false;
			textBoxFromMonth6.Location = new System.Drawing.Point(127, 0);
			textBoxFromMonth6.MaxLength = 3;
			textBoxFromMonth6.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxFromMonth6.MinValue = new decimal(new int[4]);
			textBoxFromMonth6.Name = "textBoxFromMonth6";
			textBoxFromMonth6.NullText = "0";
			textBoxFromMonth6.Size = new System.Drawing.Size(66, 20);
			textBoxFromMonth6.TabIndex = 1;
			textBoxFromMonth6.Text = "151";
			textBoxFromMonth6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAgingName6.Location = new System.Drawing.Point(3, 0);
			textBoxAgingName6.Name = "textBoxAgingName6";
			textBoxAgingName6.Size = new System.Drawing.Size(123, 20);
			textBoxAgingName6.TabIndex = 0;
			textBoxAgingName6.Text = "151 - 180 Days";
			panelMonth5.Controls.Add(textBoxToMonth5);
			panelMonth5.Controls.Add(textBoxFromMonth5);
			panelMonth5.Controls.Add(textBoxAgingName5);
			panelMonth5.Location = new System.Drawing.Point(23, 236);
			panelMonth5.Name = "panelMonth5";
			panelMonth5.Size = new System.Drawing.Size(273, 24);
			panelMonth5.TabIndex = 5;
			textBoxToMonth5.AllowDecimal = true;
			textBoxToMonth5.CustomReportFieldName = "";
			textBoxToMonth5.CustomReportKey = "";
			textBoxToMonth5.CustomReportValueType = 1;
			textBoxToMonth5.IsComboTextBox = false;
			textBoxToMonth5.IsModified = false;
			textBoxToMonth5.Location = new System.Drawing.Point(196, 2);
			textBoxToMonth5.MaxLength = 3;
			textBoxToMonth5.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxToMonth5.MinValue = new decimal(new int[4]);
			textBoxToMonth5.Name = "textBoxToMonth5";
			textBoxToMonth5.NullText = "0";
			textBoxToMonth5.Size = new System.Drawing.Size(66, 20);
			textBoxToMonth5.TabIndex = 2;
			textBoxToMonth5.Text = "150";
			textBoxToMonth5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxFromMonth5.AllowDecimal = true;
			textBoxFromMonth5.CustomReportFieldName = "";
			textBoxFromMonth5.CustomReportKey = "";
			textBoxFromMonth5.CustomReportValueType = 1;
			textBoxFromMonth5.IsComboTextBox = false;
			textBoxFromMonth5.IsModified = false;
			textBoxFromMonth5.Location = new System.Drawing.Point(127, 2);
			textBoxFromMonth5.MaxLength = 3;
			textBoxFromMonth5.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxFromMonth5.MinValue = new decimal(new int[4]);
			textBoxFromMonth5.Name = "textBoxFromMonth5";
			textBoxFromMonth5.NullText = "0";
			textBoxFromMonth5.Size = new System.Drawing.Size(66, 20);
			textBoxFromMonth5.TabIndex = 1;
			textBoxFromMonth5.Text = "121";
			textBoxFromMonth5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAgingName5.Location = new System.Drawing.Point(3, 2);
			textBoxAgingName5.Name = "textBoxAgingName5";
			textBoxAgingName5.Size = new System.Drawing.Size(123, 20);
			textBoxAgingName5.TabIndex = 0;
			textBoxAgingName5.Text = "121 - 150 Days";
			panelMonth4.Controls.Add(textBoxToMonth4);
			panelMonth4.Controls.Add(textBoxFromMonth4);
			panelMonth4.Controls.Add(textBoxAgingName4);
			panelMonth4.Location = new System.Drawing.Point(23, 211);
			panelMonth4.Name = "panelMonth4";
			panelMonth4.Size = new System.Drawing.Size(273, 24);
			panelMonth4.TabIndex = 4;
			textBoxToMonth4.AllowDecimal = true;
			textBoxToMonth4.CustomReportFieldName = "";
			textBoxToMonth4.CustomReportKey = "";
			textBoxToMonth4.CustomReportValueType = 1;
			textBoxToMonth4.IsComboTextBox = false;
			textBoxToMonth4.IsModified = false;
			textBoxToMonth4.Location = new System.Drawing.Point(196, 2);
			textBoxToMonth4.MaxLength = 3;
			textBoxToMonth4.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxToMonth4.MinValue = new decimal(new int[4]);
			textBoxToMonth4.Name = "textBoxToMonth4";
			textBoxToMonth4.NullText = "0";
			textBoxToMonth4.Size = new System.Drawing.Size(66, 20);
			textBoxToMonth4.TabIndex = 2;
			textBoxToMonth4.Text = "120";
			textBoxToMonth4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxFromMonth4.AllowDecimal = true;
			textBoxFromMonth4.CustomReportFieldName = "";
			textBoxFromMonth4.CustomReportKey = "";
			textBoxFromMonth4.CustomReportValueType = 1;
			textBoxFromMonth4.IsComboTextBox = false;
			textBoxFromMonth4.IsModified = false;
			textBoxFromMonth4.Location = new System.Drawing.Point(127, 2);
			textBoxFromMonth4.MaxLength = 3;
			textBoxFromMonth4.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxFromMonth4.MinValue = new decimal(new int[4]);
			textBoxFromMonth4.Name = "textBoxFromMonth4";
			textBoxFromMonth4.NullText = "0";
			textBoxFromMonth4.Size = new System.Drawing.Size(66, 20);
			textBoxFromMonth4.TabIndex = 1;
			textBoxFromMonth4.Text = "91";
			textBoxFromMonth4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAgingName4.Location = new System.Drawing.Point(3, 2);
			textBoxAgingName4.Name = "textBoxAgingName4";
			textBoxAgingName4.Size = new System.Drawing.Size(123, 20);
			textBoxAgingName4.TabIndex = 0;
			textBoxAgingName4.Text = "91 - 120 Days";
			panelMonth3.Controls.Add(textBoxToMonth3);
			panelMonth3.Controls.Add(textBoxFromMonth3);
			panelMonth3.Controls.Add(textBoxAgingName3);
			panelMonth3.Location = new System.Drawing.Point(23, 187);
			panelMonth3.Name = "panelMonth3";
			panelMonth3.Size = new System.Drawing.Size(273, 24);
			panelMonth3.TabIndex = 3;
			textBoxToMonth3.AllowDecimal = true;
			textBoxToMonth3.CustomReportFieldName = "";
			textBoxToMonth3.CustomReportKey = "";
			textBoxToMonth3.CustomReportValueType = 1;
			textBoxToMonth3.IsComboTextBox = false;
			textBoxToMonth3.IsModified = false;
			textBoxToMonth3.Location = new System.Drawing.Point(196, 3);
			textBoxToMonth3.MaxLength = 3;
			textBoxToMonth3.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxToMonth3.MinValue = new decimal(new int[4]);
			textBoxToMonth3.Name = "textBoxToMonth3";
			textBoxToMonth3.NullText = "0";
			textBoxToMonth3.Size = new System.Drawing.Size(66, 20);
			textBoxToMonth3.TabIndex = 2;
			textBoxToMonth3.Text = "90";
			textBoxToMonth3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxFromMonth3.AllowDecimal = true;
			textBoxFromMonth3.CustomReportFieldName = "";
			textBoxFromMonth3.CustomReportKey = "";
			textBoxFromMonth3.CustomReportValueType = 1;
			textBoxFromMonth3.IsComboTextBox = false;
			textBoxFromMonth3.IsModified = false;
			textBoxFromMonth3.Location = new System.Drawing.Point(127, 3);
			textBoxFromMonth3.MaxLength = 3;
			textBoxFromMonth3.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxFromMonth3.MinValue = new decimal(new int[4]);
			textBoxFromMonth3.Name = "textBoxFromMonth3";
			textBoxFromMonth3.NullText = "0";
			textBoxFromMonth3.Size = new System.Drawing.Size(66, 20);
			textBoxFromMonth3.TabIndex = 1;
			textBoxFromMonth3.Text = "61";
			textBoxFromMonth3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAgingName3.Location = new System.Drawing.Point(3, 3);
			textBoxAgingName3.Name = "textBoxAgingName3";
			textBoxAgingName3.Size = new System.Drawing.Size(123, 20);
			textBoxAgingName3.TabIndex = 0;
			textBoxAgingName3.Text = "61 - 90 Days";
			panelMonth2.Controls.Add(textBoxToMonth2);
			panelMonth2.Controls.Add(textBoxFromMonth2);
			panelMonth2.Controls.Add(textBoxAgingName2);
			panelMonth2.Location = new System.Drawing.Point(23, 163);
			panelMonth2.Name = "panelMonth2";
			panelMonth2.Size = new System.Drawing.Size(273, 24);
			panelMonth2.TabIndex = 2;
			textBoxToMonth2.AllowDecimal = true;
			textBoxToMonth2.CustomReportFieldName = "";
			textBoxToMonth2.CustomReportKey = "";
			textBoxToMonth2.CustomReportValueType = 1;
			textBoxToMonth2.IsComboTextBox = false;
			textBoxToMonth2.IsModified = false;
			textBoxToMonth2.Location = new System.Drawing.Point(196, 2);
			textBoxToMonth2.MaxLength = 3;
			textBoxToMonth2.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxToMonth2.MinValue = new decimal(new int[4]);
			textBoxToMonth2.Name = "textBoxToMonth2";
			textBoxToMonth2.NullText = "0";
			textBoxToMonth2.Size = new System.Drawing.Size(66, 20);
			textBoxToMonth2.TabIndex = 2;
			textBoxToMonth2.Text = "60";
			textBoxToMonth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxFromMonth2.AllowDecimal = true;
			textBoxFromMonth2.CustomReportFieldName = "";
			textBoxFromMonth2.CustomReportKey = "";
			textBoxFromMonth2.CustomReportValueType = 1;
			textBoxFromMonth2.IsComboTextBox = false;
			textBoxFromMonth2.IsModified = false;
			textBoxFromMonth2.Location = new System.Drawing.Point(127, 2);
			textBoxFromMonth2.MaxLength = 3;
			textBoxFromMonth2.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxFromMonth2.MinValue = new decimal(new int[4]);
			textBoxFromMonth2.Name = "textBoxFromMonth2";
			textBoxFromMonth2.NullText = "0";
			textBoxFromMonth2.Size = new System.Drawing.Size(66, 20);
			textBoxFromMonth2.TabIndex = 1;
			textBoxFromMonth2.Text = "31";
			textBoxFromMonth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAgingName2.Location = new System.Drawing.Point(3, 2);
			textBoxAgingName2.Name = "textBoxAgingName2";
			textBoxAgingName2.Size = new System.Drawing.Size(123, 20);
			textBoxAgingName2.TabIndex = 0;
			textBoxAgingName2.Text = "31- 60 Days";
			panelMonth1.Controls.Add(textBoxToMonth1);
			panelMonth1.Controls.Add(textBoxFromMonth1);
			panelMonth1.Controls.Add(textBoxAgingName1);
			panelMonth1.Location = new System.Drawing.Point(23, 137);
			panelMonth1.Name = "panelMonth1";
			panelMonth1.Size = new System.Drawing.Size(273, 24);
			panelMonth1.TabIndex = 1;
			textBoxToMonth1.AllowDecimal = true;
			textBoxToMonth1.CustomReportFieldName = "";
			textBoxToMonth1.CustomReportKey = "";
			textBoxToMonth1.CustomReportValueType = 1;
			textBoxToMonth1.IsComboTextBox = false;
			textBoxToMonth1.IsModified = false;
			textBoxToMonth1.Location = new System.Drawing.Point(196, 2);
			textBoxToMonth1.MaxLength = 3;
			textBoxToMonth1.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxToMonth1.MinValue = new decimal(new int[4]);
			textBoxToMonth1.Name = "textBoxToMonth1";
			textBoxToMonth1.NullText = "0";
			textBoxToMonth1.Size = new System.Drawing.Size(66, 20);
			textBoxToMonth1.TabIndex = 2;
			textBoxToMonth1.Text = "30";
			textBoxToMonth1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxFromMonth1.AllowDecimal = true;
			textBoxFromMonth1.CustomReportFieldName = "";
			textBoxFromMonth1.CustomReportKey = "";
			textBoxFromMonth1.CustomReportValueType = 1;
			textBoxFromMonth1.IsComboTextBox = false;
			textBoxFromMonth1.IsModified = false;
			textBoxFromMonth1.Location = new System.Drawing.Point(127, 2);
			textBoxFromMonth1.MaxLength = 3;
			textBoxFromMonth1.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxFromMonth1.MinValue = new decimal(new int[4]);
			textBoxFromMonth1.Name = "textBoxFromMonth1";
			textBoxFromMonth1.NullText = "0";
			textBoxFromMonth1.Size = new System.Drawing.Size(66, 20);
			textBoxFromMonth1.TabIndex = 1;
			textBoxFromMonth1.Text = "1";
			textBoxFromMonth1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAgingName1.Location = new System.Drawing.Point(3, 2);
			textBoxAgingName1.Name = "textBoxAgingName1";
			textBoxAgingName1.Size = new System.Drawing.Size(123, 20);
			textBoxAgingName1.TabIndex = 0;
			textBoxAgingName1.Text = "1 - 30 Days";
			panelCurrent.Controls.Add(textBoxToMonth0);
			panelCurrent.Controls.Add(textBoxFromMonth0);
			panelCurrent.Controls.Add(textBoxAgingName0);
			panelCurrent.Location = new System.Drawing.Point(23, 112);
			panelCurrent.Name = "panelCurrent";
			panelCurrent.Size = new System.Drawing.Size(273, 24);
			panelCurrent.TabIndex = 0;
			textBoxToMonth0.AllowDecimal = true;
			textBoxToMonth0.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxToMonth0.CustomReportFieldName = "";
			textBoxToMonth0.CustomReportKey = "";
			textBoxToMonth0.CustomReportValueType = 1;
			textBoxToMonth0.IsComboTextBox = false;
			textBoxToMonth0.IsModified = false;
			textBoxToMonth0.Location = new System.Drawing.Point(196, 3);
			textBoxToMonth0.MaxLength = 3;
			textBoxToMonth0.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxToMonth0.MinValue = new decimal(new int[4]);
			textBoxToMonth0.Name = "textBoxToMonth0";
			textBoxToMonth0.NullText = "0";
			textBoxToMonth0.ReadOnly = true;
			textBoxToMonth0.Size = new System.Drawing.Size(66, 20);
			textBoxToMonth0.TabIndex = 2;
			textBoxToMonth0.Text = "0";
			textBoxToMonth0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxFromMonth0.AllowDecimal = true;
			textBoxFromMonth0.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromMonth0.CustomReportFieldName = "";
			textBoxFromMonth0.CustomReportKey = "";
			textBoxFromMonth0.CustomReportValueType = 1;
			textBoxFromMonth0.IsComboTextBox = false;
			textBoxFromMonth0.IsModified = false;
			textBoxFromMonth0.Location = new System.Drawing.Point(127, 2);
			textBoxFromMonth0.MaxLength = 3;
			textBoxFromMonth0.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxFromMonth0.MinValue = new decimal(new int[4]);
			textBoxFromMonth0.Name = "textBoxFromMonth0";
			textBoxFromMonth0.NullText = "0";
			textBoxFromMonth0.ReadOnly = true;
			textBoxFromMonth0.Size = new System.Drawing.Size(66, 20);
			textBoxFromMonth0.TabIndex = 1;
			textBoxFromMonth0.Text = "0";
			textBoxFromMonth0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAgingName0.Location = new System.Drawing.Point(3, 2);
			textBoxAgingName0.Name = "textBoxAgingName0";
			textBoxAgingName0.Size = new System.Drawing.Size(123, 20);
			textBoxAgingName0.TabIndex = 0;
			textBoxAgingName0.Text = "Current";
			checkBoxMonth6.AutoSize = true;
			checkBoxMonth6.Checked = true;
			checkBoxMonth6.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxMonth6.Location = new System.Drawing.Point(9, 265);
			checkBoxMonth6.Name = "checkBoxMonth6";
			checkBoxMonth6.Size = new System.Drawing.Size(15, 14);
			checkBoxMonth6.TabIndex = 11;
			checkBoxMonth6.UseVisualStyleBackColor = true;
			checkBoxMonth6.CheckedChanged += new System.EventHandler(checkBoxMonth6_CheckedChanged);
			checkBoxMonth5.AutoSize = true;
			checkBoxMonth5.Checked = true;
			checkBoxMonth5.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxMonth5.Location = new System.Drawing.Point(9, 241);
			checkBoxMonth5.Name = "checkBoxMonth5";
			checkBoxMonth5.Size = new System.Drawing.Size(15, 14);
			checkBoxMonth5.TabIndex = 9;
			checkBoxMonth5.UseVisualStyleBackColor = true;
			checkBoxMonth5.CheckedChanged += new System.EventHandler(checkBoxMonth5_CheckedChanged);
			checkBoxMonth4.AutoSize = true;
			checkBoxMonth4.Checked = true;
			checkBoxMonth4.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxMonth4.Location = new System.Drawing.Point(9, 216);
			checkBoxMonth4.Name = "checkBoxMonth4";
			checkBoxMonth4.Size = new System.Drawing.Size(15, 14);
			checkBoxMonth4.TabIndex = 8;
			checkBoxMonth4.UseVisualStyleBackColor = true;
			checkBoxMonth4.CheckedChanged += new System.EventHandler(checkBoxMonth4_CheckedChanged);
			checkBoxMonth3.AutoSize = true;
			checkBoxMonth3.Checked = true;
			checkBoxMonth3.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxMonth3.Location = new System.Drawing.Point(9, 193);
			checkBoxMonth3.Name = "checkBoxMonth3";
			checkBoxMonth3.Size = new System.Drawing.Size(15, 14);
			checkBoxMonth3.TabIndex = 7;
			checkBoxMonth3.UseVisualStyleBackColor = true;
			checkBoxMonth3.CheckedChanged += new System.EventHandler(checkBoxMonth3_CheckedChanged);
			checkBoxMonth2.AutoSize = true;
			checkBoxMonth2.Checked = true;
			checkBoxMonth2.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxMonth2.Location = new System.Drawing.Point(9, 168);
			checkBoxMonth2.Name = "checkBoxMonth2";
			checkBoxMonth2.Size = new System.Drawing.Size(15, 14);
			checkBoxMonth2.TabIndex = 5;
			checkBoxMonth2.UseVisualStyleBackColor = true;
			checkBoxMonth2.CheckedChanged += new System.EventHandler(checkBoxMonth2_CheckedChanged);
			checkBoxMonth1.AutoSize = true;
			checkBoxMonth1.Checked = true;
			checkBoxMonth1.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxMonth1.Location = new System.Drawing.Point(9, 142);
			checkBoxMonth1.Name = "checkBoxMonth1";
			checkBoxMonth1.Size = new System.Drawing.Size(15, 14);
			checkBoxMonth1.TabIndex = 3;
			checkBoxMonth1.UseVisualStyleBackColor = true;
			checkBoxMonth1.CheckedChanged += new System.EventHandler(checkBoxMonth1_CheckedChanged);
			label4.Location = new System.Drawing.Point(216, 94);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(68, 18);
			label4.TabIndex = 5;
			label4.Text = "To";
			label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label3.Location = new System.Drawing.Point(148, 94);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(69, 18);
			label3.TabIndex = 5;
			label3.Text = "From";
			label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			radioButtonAgingDate.AutoSize = true;
			radioButtonAgingDate.Location = new System.Drawing.Point(126, 29);
			radioButtonAgingDate.Name = "radioButtonAgingDate";
			radioButtonAgingDate.Size = new System.Drawing.Size(90, 17);
			radioButtonAgingDate.TabIndex = 1;
			radioButtonAgingDate.Text = "Aging by date";
			radioButtonAgingDate.UseVisualStyleBackColor = true;
			radioButtonAgingDate.CheckedChanged += new System.EventHandler(radioButtonAgingDate_CheckedChanged);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(23, 95);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(67, 13);
			label2.TabIndex = 5;
			label2.Text = "Aging Period";
			radioButtonAgingDueDate.AutoSize = true;
			radioButtonAgingDueDate.Checked = true;
			radioButtonAgingDueDate.Location = new System.Drawing.Point(9, 29);
			radioButtonAgingDueDate.Name = "radioButtonAgingDueDate";
			radioButtonAgingDueDate.Size = new System.Drawing.Size(111, 17);
			radioButtonAgingDueDate.TabIndex = 0;
			radioButtonAgingDueDate.TabStop = true;
			radioButtonAgingDueDate.Text = "Aging by due date";
			radioButtonAgingDueDate.UseVisualStyleBackColor = true;
			radioButtonAgingDueDate.CheckedChanged += new System.EventHandler(radioButtonAgingDueDate_CheckedChanged);
			tabPageUserDefined.AutoScroll = true;
			tabPageUserDefined.Controls.Add(mmLabel60);
			tabPageUserDefined.Controls.Add(ComboBoxPackingListQtyAction);
			tabPageUserDefined.Controls.Add(label11);
			tabPageUserDefined.Controls.Add(checkBoxManadatoryPO);
			tabPageUserDefined.Controls.Add(checkBoxConsiderStockinMRPQ);
			tabPageUserDefined.Controls.Add(checkBoxShowBOLinPL);
			tabPageUserDefined.Controls.Add(checkBoxOrderandShipment);
			tabPageUserDefined.Controls.Add(checkBoxAllowtoeditreqdateinPO);
			tabPageUserDefined.Controls.Add(buttonVenFlags);
			tabPageUserDefined.Controls.Add(checkBoxGRNZeroQty);
			tabPageUserDefined.Controls.Add(groupBox19);
			tabPageUserDefined.Controls.Add(mmLabel14);
			tabPageUserDefined.Controls.Add(panelPurchaseLandingCostMethod);
			tabPageUserDefined.Controls.Add(groupBox17);
			tabPageUserDefined.Controls.Add(checkBoxPurchaseNegativeQty);
			tabPageUserDefined.Controls.Add(checkBoxShowLCAmount);
			tabPageUserDefined.Controls.Add(groupBox11);
			tabPageUserDefined.Controls.Add(checkBoxLPurchaseLandingCost);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(807, 626);
			mmLabel60.AutoSize = true;
			mmLabel60.BackColor = System.Drawing.Color.Transparent;
			mmLabel60.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel60.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel60.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel60.IsFieldHeader = false;
			mmLabel60.IsRequired = false;
			mmLabel60.Location = new System.Drawing.Point(507, 225);
			mmLabel60.Name = "mmLabel60";
			mmLabel60.PenWidth = 1f;
			mmLabel60.ShowBorder = false;
			mmLabel60.Size = new System.Drawing.Size(41, 13);
			mmLabel60.TabIndex = 24;
			mmLabel60.Text = "Action:";
			mmLabel60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel60.Visible = false;
			ComboBoxPackingListQtyAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			ComboBoxPackingListQtyAction.FormattingEnabled = true;
			ComboBoxPackingListQtyAction.HasPasswordItem = true;
			ComboBoxPackingListQtyAction.Location = new System.Drawing.Point(553, 222);
			ComboBoxPackingListQtyAction.Name = "ComboBoxPackingListQtyAction";
			ComboBoxPackingListQtyAction.Size = new System.Drawing.Size(119, 21);
			ComboBoxPackingListQtyAction.TabIndex = 23;
			ComboBoxPackingListQtyAction.Visible = false;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(453, 204);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(226, 13);
			label11.TabIndex = 22;
			label11.Text = "Packing list quantity more than order Quantity :";
			label11.Visible = false;
			checkBoxManadatoryPO.AutoSize = true;
			checkBoxManadatoryPO.Location = new System.Drawing.Point(21, 405);
			checkBoxManadatoryPO.Name = "checkBoxManadatoryPO";
			checkBoxManadatoryPO.Size = new System.Drawing.Size(270, 17);
			checkBoxManadatoryPO.TabIndex = 21;
			checkBoxManadatoryPO.Text = "Mandatory to select Purchase Order in Bill of Lading";
			checkBoxManadatoryPO.UseVisualStyleBackColor = true;
			checkBoxConsiderStockinMRPQ.AutoSize = true;
			checkBoxConsiderStockinMRPQ.Location = new System.Drawing.Point(21, 333);
			checkBoxConsiderStockinMRPQ.Name = "checkBoxConsiderStockinMRPQ";
			checkBoxConsiderStockinMRPQ.Size = new System.Drawing.Size(189, 17);
			checkBoxConsiderStockinMRPQ.TabIndex = 20;
			checkBoxConsiderStockinMRPQ.Text = "Consider Stock in MR-PQ Creation";
			checkBoxConsiderStockinMRPQ.UseVisualStyleBackColor = true;
			checkBoxShowBOLinPL.AutoSize = true;
			checkBoxShowBOLinPL.Location = new System.Drawing.Point(21, 385);
			checkBoxShowBOLinPL.Name = "checkBoxShowBOLinPL";
			checkBoxShowBOLinPL.Size = new System.Drawing.Size(168, 17);
			checkBoxShowBOLinPL.TabIndex = 19;
			checkBoxShowBOLinPL.Text = "Show BOL List in Packing List";
			checkBoxShowBOLinPL.UseVisualStyleBackColor = true;
			checkBoxOrderandShipment.AutoSize = true;
			checkBoxOrderandShipment.Location = new System.Drawing.Point(21, 368);
			checkBoxOrderandShipment.Name = "checkBoxOrderandShipment";
			checkBoxOrderandShipment.Size = new System.Drawing.Size(212, 17);
			checkBoxOrderandShipment.TabIndex = 18;
			checkBoxOrderandShipment.Text = "Show order and shipment detail in GRN";
			checkBoxOrderandShipment.UseVisualStyleBackColor = true;
			checkBoxAllowtoeditreqdateinPO.AutoSize = true;
			checkBoxAllowtoeditreqdateinPO.Location = new System.Drawing.Point(21, 350);
			checkBoxAllowtoeditreqdateinPO.Name = "checkBoxAllowtoeditreqdateinPO";
			checkBoxAllowtoeditreqdateinPO.Size = new System.Drawing.Size(159, 17);
			checkBoxAllowtoeditreqdateinPO.TabIndex = 17;
			checkBoxAllowtoeditreqdateinPO.Text = "Allow to edit Req date in PO";
			checkBoxAllowtoeditreqdateinPO.UseVisualStyleBackColor = true;
			buttonVenFlags.Image = Micromind.ClientUI.Properties.Resources.flagred;
			buttonVenFlags.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			buttonVenFlags.Location = new System.Drawing.Point(448, 255);
			buttonVenFlags.Name = "buttonVenFlags";
			buttonVenFlags.Size = new System.Drawing.Size(124, 23);
			buttonVenFlags.TabIndex = 16;
			buttonVenFlags.Text = "Flag Names...";
			buttonVenFlags.UseVisualStyleBackColor = true;
			buttonVenFlags.Click += new System.EventHandler(buttonVenFlags_Click);
			checkBoxGRNZeroQty.AutoSize = true;
			checkBoxGRNZeroQty.Location = new System.Drawing.Point(21, 317);
			checkBoxGRNZeroQty.Name = "checkBoxGRNZeroQty";
			checkBoxGRNZeroQty.Size = new System.Drawing.Size(153, 17);
			checkBoxGRNZeroQty.TabIndex = 8;
			checkBoxGRNZeroQty.Text = "Load zero Quantity in GRN";
			checkBoxGRNZeroQty.UseVisualStyleBackColor = true;
			groupBox19.Controls.Add(checkBoxAllowPReturnChangePrice);
			groupBox19.Controls.Add(checkBoxAllowPReturnWithoutInvoice);
			groupBox19.Location = new System.Drawing.Point(448, 138);
			groupBox19.Name = "groupBox19";
			groupBox19.Size = new System.Drawing.Size(322, 62);
			groupBox19.TabIndex = 2;
			groupBox19.TabStop = false;
			groupBox19.Text = "Purchase Return";
			checkBoxAllowPReturnChangePrice.AutoSize = true;
			checkBoxAllowPReturnChangePrice.Location = new System.Drawing.Point(6, 36);
			checkBoxAllowPReturnChangePrice.Name = "checkBoxAllowPReturnChangePrice";
			checkBoxAllowPReturnChangePrice.Size = new System.Drawing.Size(216, 17);
			checkBoxAllowPReturnChangePrice.TabIndex = 1;
			checkBoxAllowPReturnChangePrice.Text = "Allow to change price in purchase return";
			checkBoxAllowPReturnChangePrice.UseVisualStyleBackColor = true;
			checkBoxAllowPReturnWithoutInvoice.AutoSize = true;
			checkBoxAllowPReturnWithoutInvoice.Location = new System.Drawing.Point(6, 19);
			checkBoxAllowPReturnWithoutInvoice.Name = "checkBoxAllowPReturnWithoutInvoice";
			checkBoxAllowPReturnWithoutInvoice.Size = new System.Drawing.Size(296, 17);
			checkBoxAllowPReturnWithoutInvoice.TabIndex = 0;
			checkBoxAllowPReturnWithoutInvoice.Text = "Allow entring returns without selecting a purchase invoice";
			checkBoxAllowPReturnWithoutInvoice.UseVisualStyleBackColor = true;
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(18, 452);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(162, 13);
			mmLabel14.TabIndex = 6;
			mmLabel14.Text = "Calculate purchase landing cost:";
			mmLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			panelPurchaseLandingCostMethod.Controls.Add(radioButtonWeight);
			panelPurchaseLandingCostMethod.Controls.Add(radioButtonValue);
			panelPurchaseLandingCostMethod.Location = new System.Drawing.Point(186, 444);
			panelPurchaseLandingCostMethod.Name = "panelPurchaseLandingCostMethod";
			panelPurchaseLandingCostMethod.Size = new System.Drawing.Size(157, 27);
			panelPurchaseLandingCostMethod.TabIndex = 7;
			radioButtonWeight.AutoSize = true;
			radioButtonWeight.Location = new System.Drawing.Point(80, 4);
			radioButtonWeight.Name = "radioButtonWeight";
			radioButtonWeight.Size = new System.Drawing.Size(71, 17);
			radioButtonWeight.TabIndex = 1;
			radioButtonWeight.Text = "By weight";
			radioButtonWeight.UseVisualStyleBackColor = true;
			radioButtonValue.AutoSize = true;
			radioButtonValue.Checked = true;
			radioButtonValue.Location = new System.Drawing.Point(8, 4);
			radioButtonValue.Name = "radioButtonValue";
			radioButtonValue.Size = new System.Drawing.Size(66, 17);
			radioButtonValue.TabIndex = 0;
			radioButtonValue.TabStop = true;
			radioButtonValue.Text = "By value";
			radioButtonValue.UseVisualStyleBackColor = true;
			groupBox17.Controls.Add(checkBoxTrackConsignInExpenses);
			groupBox17.Controls.Add(checkBoxTrackConsignInDetail);
			groupBox17.Controls.Add(checkBoxConsignInFIFO);
			groupBox17.Location = new System.Drawing.Point(448, 13);
			groupBox17.Name = "groupBox17";
			groupBox17.Size = new System.Drawing.Size(322, 119);
			groupBox17.TabIndex = 1;
			groupBox17.TabStop = false;
			groupBox17.Text = "Consignment In";
			checkBoxTrackConsignInExpenses.Location = new System.Drawing.Point(7, 76);
			checkBoxTrackConsignInExpenses.Name = "checkBoxTrackConsignInExpenses";
			checkBoxTrackConsignInExpenses.Size = new System.Drawing.Size(289, 17);
			checkBoxTrackConsignInExpenses.TabIndex = 4;
			checkBoxTrackConsignInExpenses.Text = "Track Consignment In Expenses";
			checkBoxTrackConsignInExpenses.UseVisualStyleBackColor = true;
			checkBoxTrackConsignInDetail.Location = new System.Drawing.Point(7, 54);
			checkBoxTrackConsignInDetail.Name = "checkBoxTrackConsignInDetail";
			checkBoxTrackConsignInDetail.Size = new System.Drawing.Size(289, 24);
			checkBoxTrackConsignInDetail.TabIndex = 3;
			checkBoxTrackConsignInDetail.Text = "Provide detailed sales in settlement";
			checkBoxTrackConsignInDetail.UseVisualStyleBackColor = true;
			checkBoxConsignInFIFO.Location = new System.Drawing.Point(7, 20);
			checkBoxConsignInFIFO.Name = "checkBoxConsignInFIFO";
			checkBoxConsignInFIFO.Size = new System.Drawing.Size(289, 35);
			checkBoxConsignInFIFO.TabIndex = 1;
			checkBoxConsignInFIFO.Text = "Allocated sales of Consignment items to open consignments on FIFO basis";
			checkBoxConsignInFIFO.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			checkBoxConsignInFIFO.UseVisualStyleBackColor = true;
			checkBoxPurchaseNegativeQty.AutoSize = true;
			checkBoxPurchaseNegativeQty.Location = new System.Drawing.Point(21, 300);
			checkBoxPurchaseNegativeQty.Name = "checkBoxPurchaseNegativeQty";
			checkBoxPurchaseNegativeQty.Size = new System.Drawing.Size(269, 17);
			checkBoxPurchaseNegativeQty.TabIndex = 5;
			checkBoxPurchaseNegativeQty.Text = "Allow to enter negative quantity in purchase invoice";
			checkBoxPurchaseNegativeQty.UseVisualStyleBackColor = true;
			checkBoxShowLCAmount.AutoSize = true;
			checkBoxShowLCAmount.Location = new System.Drawing.Point(21, 282);
			checkBoxShowLCAmount.Name = "checkBoxShowLCAmount";
			checkBoxShowLCAmount.Size = new System.Drawing.Size(262, 17);
			checkBoxShowLCAmount.TabIndex = 4;
			checkBoxShowLCAmount.Text = "Show landing cost calculation in purchase invoice";
			checkBoxShowLCAmount.UseVisualStyleBackColor = true;
			groupBox11.Controls.Add(checkBoxImportMoreThanPLQTY);
			groupBox11.Controls.Add(checkBoxImportAllowNewInPackingList);
			groupBox11.Controls.Add(checkBoxPurchaseOrderChangePrice);
			groupBox11.Controls.Add(mmLabel31);
			groupBox11.Controls.Add(checkBoxLocalAllowNewInGRN);
			groupBox11.Controls.Add(checkBoxImportMoreThanPOQTY);
			groupBox11.Controls.Add(comboBoxLocalPurchaseFlow);
			groupBox11.Controls.Add(mmLabel33);
			groupBox11.Controls.Add(checkBoxLocalAllowGRNWithoutPO);
			groupBox11.Controls.Add(checkBoxImportAllowGRNWithoutPO);
			groupBox11.Controls.Add(checkBoxLocalMoreThanPOQTY);
			groupBox11.Controls.Add(comboBoxImportPurchaseFlow);
			groupBox11.Controls.Add(checkBoxImportAllowNewInGRN);
			groupBox11.Location = new System.Drawing.Point(12, 13);
			groupBox11.Name = "groupBox11";
			groupBox11.Size = new System.Drawing.Size(430, 245);
			groupBox11.TabIndex = 0;
			groupBox11.TabStop = false;
			groupBox11.Text = "Purchase Flow";
			checkBoxImportMoreThanPLQTY.AutoSize = true;
			checkBoxImportMoreThanPLQTY.Location = new System.Drawing.Point(122, 221);
			checkBoxImportMoreThanPLQTY.Name = "checkBoxImportMoreThanPLQTY";
			checkBoxImportMoreThanPLQTY.Size = new System.Drawing.Size(247, 17);
			checkBoxImportMoreThanPLQTY.TabIndex = 11;
			checkBoxImportMoreThanPLQTY.Text = "Allow to receive more quantity than PL quantity";
			checkBoxImportMoreThanPLQTY.UseVisualStyleBackColor = true;
			checkBoxImportAllowNewInPackingList.AutoSize = true;
			checkBoxImportAllowNewInPackingList.Location = new System.Drawing.Point(122, 164);
			checkBoxImportAllowNewInPackingList.Name = "checkBoxImportAllowNewInPackingList";
			checkBoxImportAllowNewInPackingList.Size = new System.Drawing.Size(269, 17);
			checkBoxImportAllowNewInPackingList.TabIndex = 8;
			checkBoxImportAllowNewInPackingList.Text = "Allow to add items in Packing List that are not in PO";
			checkBoxImportAllowNewInPackingList.UseVisualStyleBackColor = true;
			checkBoxPurchaseOrderChangePrice.AutoSize = true;
			checkBoxPurchaseOrderChangePrice.Location = new System.Drawing.Point(122, 101);
			checkBoxPurchaseOrderChangePrice.Name = "checkBoxPurchaseOrderChangePrice";
			checkBoxPurchaseOrderChangePrice.Size = new System.Drawing.Size(296, 17);
			checkBoxPurchaseOrderChangePrice.TabIndex = 5;
			checkBoxPurchaseOrderChangePrice.Text = "Allow to change price when created from Purchase Order";
			checkBoxPurchaseOrderChangePrice.UseVisualStyleBackColor = true;
			mmLabel31.AutoSize = true;
			mmLabel31.BackColor = System.Drawing.Color.Transparent;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(6, 25);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(107, 13);
			mmLabel31.TabIndex = 0;
			mmLabel31.Text = "Local Purchase Flow:";
			mmLabel31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxLocalAllowNewInGRN.AutoSize = true;
			checkBoxLocalAllowNewInGRN.Location = new System.Drawing.Point(122, 47);
			checkBoxLocalAllowNewInGRN.Name = "checkBoxLocalAllowNewInGRN";
			checkBoxLocalAllowNewInGRN.Size = new System.Drawing.Size(256, 17);
			checkBoxLocalAllowNewInGRN.TabIndex = 2;
			checkBoxLocalAllowNewInGRN.Text = "Allow to add items that are not in Purchase Order";
			checkBoxLocalAllowNewInGRN.UseVisualStyleBackColor = true;
			checkBoxImportMoreThanPOQTY.AutoSize = true;
			checkBoxImportMoreThanPOQTY.Location = new System.Drawing.Point(122, 202);
			checkBoxImportMoreThanPOQTY.Name = "checkBoxImportMoreThanPOQTY";
			checkBoxImportMoreThanPOQTY.Size = new System.Drawing.Size(249, 17);
			checkBoxImportMoreThanPOQTY.TabIndex = 10;
			checkBoxImportMoreThanPOQTY.Text = "Allow to receive more quantity than PO quantity";
			checkBoxImportMoreThanPOQTY.UseVisualStyleBackColor = true;
			comboBoxLocalPurchaseFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxLocalPurchaseFlow.FormattingEnabled = true;
			comboBoxLocalPurchaseFlow.Items.AddRange(new object[4]
			{
				"Direct Purchase Invoice",
				"Purchase Order -> Purchase Invoice",
				"Goods Received Note (GRN) -> Purchase Invoice",
				"Purchase Order ->Goods Received Note (GRN) -> Purchase Invoice"
			});
			comboBoxLocalPurchaseFlow.Location = new System.Drawing.Point(124, 22);
			comboBoxLocalPurchaseFlow.Name = "comboBoxLocalPurchaseFlow";
			comboBoxLocalPurchaseFlow.Size = new System.Drawing.Size(260, 21);
			comboBoxLocalPurchaseFlow.TabIndex = 1;
			mmLabel33.AutoSize = true;
			mmLabel33.BackColor = System.Drawing.Color.Transparent;
			mmLabel33.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel33.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel33.IsFieldHeader = false;
			mmLabel33.IsRequired = false;
			mmLabel33.Location = new System.Drawing.Point(6, 123);
			mmLabel33.Name = "mmLabel33";
			mmLabel33.PenWidth = 1f;
			mmLabel33.ShowBorder = false;
			mmLabel33.Size = new System.Drawing.Size(115, 13);
			mmLabel33.TabIndex = 34;
			mmLabel33.Text = "Import Purchase Flow:";
			mmLabel33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxLocalAllowGRNWithoutPO.AutoSize = true;
			checkBoxLocalAllowGRNWithoutPO.Location = new System.Drawing.Point(122, 65);
			checkBoxLocalAllowGRNWithoutPO.Name = "checkBoxLocalAllowGRNWithoutPO";
			checkBoxLocalAllowGRNWithoutPO.Size = new System.Drawing.Size(277, 17);
			checkBoxLocalAllowGRNWithoutPO.TabIndex = 3;
			checkBoxLocalAllowGRNWithoutPO.Text = "Allow to create GRN/Invoice without Purchase Order";
			checkBoxLocalAllowGRNWithoutPO.UseVisualStyleBackColor = true;
			checkBoxImportAllowGRNWithoutPO.AutoSize = true;
			checkBoxImportAllowGRNWithoutPO.Location = new System.Drawing.Point(122, 183);
			checkBoxImportAllowGRNWithoutPO.Name = "checkBoxImportAllowGRNWithoutPO";
			checkBoxImportAllowGRNWithoutPO.Size = new System.Drawing.Size(277, 17);
			checkBoxImportAllowGRNWithoutPO.TabIndex = 9;
			checkBoxImportAllowGRNWithoutPO.Text = "Allow to create GRN/Invoice without Purchase Order";
			checkBoxImportAllowGRNWithoutPO.UseVisualStyleBackColor = true;
			checkBoxLocalMoreThanPOQTY.AutoSize = true;
			checkBoxLocalMoreThanPOQTY.Location = new System.Drawing.Point(122, 83);
			checkBoxLocalMoreThanPOQTY.Name = "checkBoxLocalMoreThanPOQTY";
			checkBoxLocalMoreThanPOQTY.Size = new System.Drawing.Size(249, 17);
			checkBoxLocalMoreThanPOQTY.TabIndex = 4;
			checkBoxLocalMoreThanPOQTY.Text = "Allow to receive more quantity than PO quantity";
			checkBoxLocalMoreThanPOQTY.UseVisualStyleBackColor = true;
			comboBoxImportPurchaseFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxImportPurchaseFlow.FormattingEnabled = true;
			comboBoxImportPurchaseFlow.Items.AddRange(new object[4]
			{
				"Direct Purchase Invoice",
				"Purchase Order -> Purchase Invoice",
				"Goods Received Note (GRN) -> Purchase Invoice",
				"Purchase Order ->Goods Received Note (GRN) -> Purchase Invoice"
			});
			comboBoxImportPurchaseFlow.Location = new System.Drawing.Point(124, 120);
			comboBoxImportPurchaseFlow.Name = "comboBoxImportPurchaseFlow";
			comboBoxImportPurchaseFlow.Size = new System.Drawing.Size(260, 21);
			comboBoxImportPurchaseFlow.TabIndex = 6;
			checkBoxImportAllowNewInGRN.AutoSize = true;
			checkBoxImportAllowNewInGRN.Location = new System.Drawing.Point(122, 145);
			checkBoxImportAllowNewInGRN.Name = "checkBoxImportAllowNewInGRN";
			checkBoxImportAllowNewInGRN.Size = new System.Drawing.Size(256, 17);
			checkBoxImportAllowNewInGRN.TabIndex = 7;
			checkBoxImportAllowNewInGRN.Text = "Allow to add items that are not in Purchase Order";
			checkBoxImportAllowNewInGRN.UseVisualStyleBackColor = true;
			checkBoxLPurchaseLandingCost.AutoSize = true;
			checkBoxLPurchaseLandingCost.Location = new System.Drawing.Point(21, 264);
			checkBoxLPurchaseLandingCost.Name = "checkBoxLPurchaseLandingCost";
			checkBoxLPurchaseLandingCost.Size = new System.Drawing.Size(264, 17);
			checkBoxLPurchaseLandingCost.TabIndex = 3;
			checkBoxLPurchaseLandingCost.Text = "Allow adding landing cost in local purchase screen";
			checkBoxLPurchaseLandingCost.UseVisualStyleBackColor = true;
			ultraTabPageControl1.AutoScroll = true;
			ultraTabPageControl1.Controls.Add(ultraExpandableGroupBoxGridFields);
			ultraTabPageControl1.Controls.Add(expandableGroupBoxTypesName);
			ultraTabPageControl1.Controls.Add(checkBoxShowMultiDimension);
			ultraTabPageControl1.Controls.Add(comboBoxItemCreationOption);
			ultraTabPageControl1.Controls.Add(mmLabel53);
			ultraTabPageControl1.Controls.Add(groupBox36);
			ultraTabPageControl1.Controls.Add(checkBoxActivatePartsDetails);
			ultraTabPageControl1.Controls.Add(groupBox35);
			ultraTabPageControl1.Controls.Add(groupBox31);
			ultraTabPageControl1.Controls.Add(groupBoxLotDetails);
			ultraTabPageControl1.Controls.Add(buttonItmFlags);
			ultraTabPageControl1.Controls.Add(groupBox21);
			ultraTabPageControl1.Controls.Add(groupBox16);
			ultraTabPageControl1.Controls.Add(groupBox5);
			ultraTabPageControl1.Controls.Add(groupBox3);
			ultraTabPageControl1.Controls.Add(groupBox8);
			ultraTabPageControl1.Controls.Add(groupBox20);
			ultraTabPageControl1.Controls.Add(grpCostSetting);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(807, 626);
			ultraExpandableGroupBoxGridFields.Controls.Add(ultraExpandableGroupBoxPanel2);
			ultraExpandableGroupBoxGridFields.ExpandedSize = new System.Drawing.Size(377, 203);
			ultraExpandableGroupBoxGridFields.Location = new System.Drawing.Point(12, 576);
			ultraExpandableGroupBoxGridFields.Name = "ultraExpandableGroupBoxGridFields";
			ultraExpandableGroupBoxGridFields.Size = new System.Drawing.Size(377, 203);
			ultraExpandableGroupBoxGridFields.TabIndex = 146;
			ultraExpandableGroupBoxGridFields.Text = "Grid Fields";
			ultraExpandableGroupBoxGridFields.ExpandedStateChanged += new System.EventHandler(ultraExpandableGroupBoxGridFields_ExpandedStateChanged);
			ultraExpandableGroupBoxPanel2.Controls.Add(textBoxRefDate2);
			ultraExpandableGroupBoxPanel2.Controls.Add(textBoxRefDate1);
			ultraExpandableGroupBoxPanel2.Controls.Add(textBoxRefNum2);
			ultraExpandableGroupBoxPanel2.Controls.Add(textBoxRefNum1);
			ultraExpandableGroupBoxPanel2.Controls.Add(mmLabel73);
			ultraExpandableGroupBoxPanel2.Controls.Add(mmLabel74);
			ultraExpandableGroupBoxPanel2.Controls.Add(mmLabel75);
			ultraExpandableGroupBoxPanel2.Controls.Add(mmLabel76);
			ultraExpandableGroupBoxPanel2.Controls.Add(mmLabel77);
			ultraExpandableGroupBoxPanel2.Controls.Add(mmLabel78);
			ultraExpandableGroupBoxPanel2.Controls.Add(textBoxRefText1);
			ultraExpandableGroupBoxPanel2.Controls.Add(textBoxRefText2);
			ultraExpandableGroupBoxPanel2.Controls.Add(textBoxRefSlNo);
			ultraExpandableGroupBoxPanel2.Controls.Add(mmLabel79);
			ultraExpandableGroupBoxPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraExpandableGroupBoxPanel2.Location = new System.Drawing.Point(3, 19);
			ultraExpandableGroupBoxPanel2.Name = "ultraExpandableGroupBoxPanel2";
			ultraExpandableGroupBoxPanel2.Size = new System.Drawing.Size(371, 181);
			ultraExpandableGroupBoxPanel2.TabIndex = 0;
			textBoxRefDate2.BackColor = System.Drawing.Color.White;
			textBoxRefDate2.CustomReportFieldName = "";
			textBoxRefDate2.CustomReportKey = "";
			textBoxRefDate2.CustomReportValueType = 1;
			textBoxRefDate2.IsComboTextBox = false;
			textBoxRefDate2.IsModified = false;
			textBoxRefDate2.Location = new System.Drawing.Point(127, 145);
			textBoxRefDate2.MaxLength = 15;
			textBoxRefDate2.Name = "textBoxRefDate2";
			textBoxRefDate2.Size = new System.Drawing.Size(229, 20);
			textBoxRefDate2.TabIndex = 25;
			textBoxRefDate1.BackColor = System.Drawing.Color.White;
			textBoxRefDate1.CustomReportFieldName = "";
			textBoxRefDate1.CustomReportKey = "";
			textBoxRefDate1.CustomReportValueType = 1;
			textBoxRefDate1.IsComboTextBox = false;
			textBoxRefDate1.IsModified = false;
			textBoxRefDate1.Location = new System.Drawing.Point(127, 121);
			textBoxRefDate1.MaxLength = 15;
			textBoxRefDate1.Name = "textBoxRefDate1";
			textBoxRefDate1.Size = new System.Drawing.Size(229, 20);
			textBoxRefDate1.TabIndex = 24;
			textBoxRefNum2.BackColor = System.Drawing.Color.White;
			textBoxRefNum2.CustomReportFieldName = "";
			textBoxRefNum2.CustomReportKey = "";
			textBoxRefNum2.CustomReportValueType = 1;
			textBoxRefNum2.IsComboTextBox = false;
			textBoxRefNum2.IsModified = false;
			textBoxRefNum2.Location = new System.Drawing.Point(127, 97);
			textBoxRefNum2.MaxLength = 15;
			textBoxRefNum2.Name = "textBoxRefNum2";
			textBoxRefNum2.Size = new System.Drawing.Size(229, 20);
			textBoxRefNum2.TabIndex = 23;
			textBoxRefNum1.BackColor = System.Drawing.Color.White;
			textBoxRefNum1.CustomReportFieldName = "";
			textBoxRefNum1.CustomReportKey = "";
			textBoxRefNum1.CustomReportValueType = 1;
			textBoxRefNum1.IsComboTextBox = false;
			textBoxRefNum1.IsModified = false;
			textBoxRefNum1.Location = new System.Drawing.Point(127, 75);
			textBoxRefNum1.MaxLength = 15;
			textBoxRefNum1.Name = "textBoxRefNum1";
			textBoxRefNum1.Size = new System.Drawing.Size(229, 20);
			textBoxRefNum1.TabIndex = 22;
			mmLabel73.AutoSize = true;
			mmLabel73.BackColor = System.Drawing.Color.Transparent;
			mmLabel73.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel73.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel73.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel73.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel73.IsFieldHeader = false;
			mmLabel73.IsRequired = false;
			mmLabel73.Location = new System.Drawing.Point(3, 149);
			mmLabel73.Name = "mmLabel73";
			mmLabel73.PenWidth = 1f;
			mmLabel73.ShowBorder = false;
			mmLabel73.Size = new System.Drawing.Size(57, 13);
			mmLabel73.TabIndex = 21;
			mmLabel73.Text = "RefDate2:";
			mmLabel74.AutoSize = true;
			mmLabel74.BackColor = System.Drawing.Color.Transparent;
			mmLabel74.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel74.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel74.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel74.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel74.IsFieldHeader = false;
			mmLabel74.IsRequired = false;
			mmLabel74.Location = new System.Drawing.Point(3, 125);
			mmLabel74.Name = "mmLabel74";
			mmLabel74.PenWidth = 1f;
			mmLabel74.ShowBorder = false;
			mmLabel74.Size = new System.Drawing.Size(57, 13);
			mmLabel74.TabIndex = 18;
			mmLabel74.Text = "RefDate1:";
			mmLabel75.AutoSize = true;
			mmLabel75.BackColor = System.Drawing.Color.Transparent;
			mmLabel75.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel75.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel75.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel75.IsFieldHeader = false;
			mmLabel75.IsRequired = false;
			mmLabel75.Location = new System.Drawing.Point(3, 79);
			mmLabel75.Name = "mmLabel75";
			mmLabel75.PenWidth = 1f;
			mmLabel75.ShowBorder = false;
			mmLabel75.Size = new System.Drawing.Size(59, 13);
			mmLabel75.TabIndex = 16;
			mmLabel75.Text = "Ref.Num1:";
			mmLabel75.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel76.AutoSize = true;
			mmLabel76.BackColor = System.Drawing.Color.Transparent;
			mmLabel76.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel76.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel76.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel76.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel76.IsFieldHeader = false;
			mmLabel76.IsRequired = false;
			mmLabel76.Location = new System.Drawing.Point(3, 101);
			mmLabel76.Name = "mmLabel76";
			mmLabel76.PenWidth = 1f;
			mmLabel76.ShowBorder = false;
			mmLabel76.Size = new System.Drawing.Size(55, 13);
			mmLabel76.TabIndex = 17;
			mmLabel76.Text = "RefNum2:";
			mmLabel77.AutoSize = true;
			mmLabel77.BackColor = System.Drawing.Color.Transparent;
			mmLabel77.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel77.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel77.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel77.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel77.IsFieldHeader = false;
			mmLabel77.IsRequired = false;
			mmLabel77.Location = new System.Drawing.Point(3, 56);
			mmLabel77.Name = "mmLabel77";
			mmLabel77.PenWidth = 1f;
			mmLabel77.ShowBorder = false;
			mmLabel77.Size = new System.Drawing.Size(60, 13);
			mmLabel77.TabIndex = 12;
			mmLabel77.Text = "Ref.Text2:";
			mmLabel78.AutoSize = true;
			mmLabel78.BackColor = System.Drawing.Color.Transparent;
			mmLabel78.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel78.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel78.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel78.IsFieldHeader = false;
			mmLabel78.IsRequired = false;
			mmLabel78.Location = new System.Drawing.Point(3, 8);
			mmLabel78.Name = "mmLabel78";
			mmLabel78.PenWidth = 1f;
			mmLabel78.ShowBorder = false;
			mmLabel78.Size = new System.Drawing.Size(49, 13);
			mmLabel78.TabIndex = 7;
			mmLabel78.Text = "RefSlNo.";
			mmLabel78.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxRefText1.BackColor = System.Drawing.Color.White;
			textBoxRefText1.CustomReportFieldName = "";
			textBoxRefText1.CustomReportKey = "";
			textBoxRefText1.CustomReportValueType = 1;
			textBoxRefText1.IsComboTextBox = false;
			textBoxRefText1.IsModified = false;
			textBoxRefText1.IsRequired = true;
			textBoxRefText1.Location = new System.Drawing.Point(127, 28);
			textBoxRefText1.MaxLength = 15;
			textBoxRefText1.Name = "textBoxRefText1";
			textBoxRefText1.Size = new System.Drawing.Size(229, 20);
			textBoxRefText1.TabIndex = 1;
			textBoxRefText2.BackColor = System.Drawing.Color.White;
			textBoxRefText2.CustomReportFieldName = "";
			textBoxRefText2.CustomReportKey = "";
			textBoxRefText2.CustomReportValueType = 1;
			textBoxRefText2.IsComboTextBox = false;
			textBoxRefText2.IsModified = false;
			textBoxRefText2.Location = new System.Drawing.Point(127, 52);
			textBoxRefText2.MaxLength = 15;
			textBoxRefText2.Name = "textBoxRefText2";
			textBoxRefText2.Size = new System.Drawing.Size(229, 20);
			textBoxRefText2.TabIndex = 2;
			textBoxRefSlNo.BackColor = System.Drawing.Color.White;
			textBoxRefSlNo.CustomReportFieldName = "";
			textBoxRefSlNo.CustomReportKey = "";
			textBoxRefSlNo.CustomReportValueType = 1;
			textBoxRefSlNo.IsComboTextBox = false;
			textBoxRefSlNo.IsModified = false;
			textBoxRefSlNo.IsRequired = true;
			textBoxRefSlNo.Location = new System.Drawing.Point(127, 4);
			textBoxRefSlNo.MaxLength = 15;
			textBoxRefSlNo.Name = "textBoxRefSlNo";
			textBoxRefSlNo.Size = new System.Drawing.Size(229, 20);
			textBoxRefSlNo.TabIndex = 0;
			mmLabel79.AutoSize = true;
			mmLabel79.BackColor = System.Drawing.Color.Transparent;
			mmLabel79.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel79.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel79.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel79.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel79.IsFieldHeader = false;
			mmLabel79.IsRequired = false;
			mmLabel79.Location = new System.Drawing.Point(3, 32);
			mmLabel79.Name = "mmLabel79";
			mmLabel79.PenWidth = 1f;
			mmLabel79.ShowBorder = false;
			mmLabel79.Size = new System.Drawing.Size(56, 13);
			mmLabel79.TabIndex = 10;
			mmLabel79.Text = "Ref.Text1";
			expandableGroupBoxTypesName.Controls.Add(ultraExpandableGroupBoxPanel1);
			expandableGroupBoxTypesName.Expanded = false;
			expandableGroupBoxTypesName.ExpandedSize = new System.Drawing.Size(377, 203);
			expandableGroupBoxTypesName.Location = new System.Drawing.Point(12, 194);
			expandableGroupBoxTypesName.Name = "expandableGroupBoxTypesName";
			expandableGroupBoxTypesName.Size = new System.Drawing.Size(377, 21);
			expandableGroupBoxTypesName.TabIndex = 7;
			expandableGroupBoxTypesName.Text = "Product Types";
			expandableGroupBoxTypesName.ExpandedStateChanged += new System.EventHandler(ultraExpandableGroupBox1_ExpandedStateChanged);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel70);
			ultraExpandableGroupBoxPanel1.Controls.Add(textBoxPType7Name);
			ultraExpandableGroupBoxPanel1.Controls.Add(textBoxPType8Name);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel71);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel67);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel68);
			ultraExpandableGroupBoxPanel1.Controls.Add(textBoxPType5Name);
			ultraExpandableGroupBoxPanel1.Controls.Add(textBoxPType6Name);
			ultraExpandableGroupBoxPanel1.Controls.Add(textBoxPType4Name);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel69);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel64);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel66);
			ultraExpandableGroupBoxPanel1.Controls.Add(textBoxPType2Name);
			ultraExpandableGroupBoxPanel1.Controls.Add(textBoxPType3Name);
			ultraExpandableGroupBoxPanel1.Controls.Add(textBoxPType1Name);
			ultraExpandableGroupBoxPanel1.Controls.Add(mmLabel65);
			ultraExpandableGroupBoxPanel1.Location = new System.Drawing.Point(-10000, -10000);
			ultraExpandableGroupBoxPanel1.Name = "ultraExpandableGroupBoxPanel1";
			ultraExpandableGroupBoxPanel1.Size = new System.Drawing.Size(371, 181);
			ultraExpandableGroupBoxPanel1.TabIndex = 0;
			ultraExpandableGroupBoxPanel1.Visible = false;
			mmLabel70.AutoSize = true;
			mmLabel70.BackColor = System.Drawing.Color.Transparent;
			mmLabel70.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel70.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel70.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel70.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel70.IsFieldHeader = false;
			mmLabel70.IsRequired = false;
			mmLabel70.Location = new System.Drawing.Point(3, 162);
			mmLabel70.Name = "mmLabel70";
			mmLabel70.PenWidth = 1f;
			mmLabel70.ShowBorder = false;
			mmLabel70.Size = new System.Drawing.Size(54, 13);
			mmLabel70.TabIndex = 22;
			mmLabel70.Text = "P.Type 8:";
			textBoxPType7Name.BackColor = System.Drawing.Color.White;
			textBoxPType7Name.CustomReportFieldName = "";
			textBoxPType7Name.CustomReportKey = "";
			textBoxPType7Name.CustomReportValueType = 1;
			textBoxPType7Name.IsComboTextBox = false;
			textBoxPType7Name.IsModified = false;
			textBoxPType7Name.IsRequired = true;
			textBoxPType7Name.Location = new System.Drawing.Point(127, 137);
			textBoxPType7Name.MaxLength = 15;
			textBoxPType7Name.Name = "textBoxPType7Name";
			textBoxPType7Name.Size = new System.Drawing.Size(229, 20);
			textBoxPType7Name.TabIndex = 19;
			textBoxPType7Name.Text = "P.Type 7";
			textBoxPType8Name.BackColor = System.Drawing.Color.White;
			textBoxPType8Name.CustomReportFieldName = "";
			textBoxPType8Name.CustomReportKey = "";
			textBoxPType8Name.CustomReportValueType = 1;
			textBoxPType8Name.IsComboTextBox = false;
			textBoxPType8Name.IsModified = false;
			textBoxPType8Name.Location = new System.Drawing.Point(127, 159);
			textBoxPType8Name.MaxLength = 15;
			textBoxPType8Name.Name = "textBoxPType8Name";
			textBoxPType8Name.Size = new System.Drawing.Size(229, 20);
			textBoxPType8Name.TabIndex = 20;
			textBoxPType8Name.Text = "P.Type 8";
			mmLabel71.AutoSize = true;
			mmLabel71.BackColor = System.Drawing.Color.Transparent;
			mmLabel71.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel71.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel71.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel71.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel71.IsFieldHeader = false;
			mmLabel71.IsRequired = false;
			mmLabel71.Location = new System.Drawing.Point(3, 139);
			mmLabel71.Name = "mmLabel71";
			mmLabel71.PenWidth = 1f;
			mmLabel71.ShowBorder = false;
			mmLabel71.Size = new System.Drawing.Size(54, 13);
			mmLabel71.TabIndex = 21;
			mmLabel71.Text = "P.Type 7:";
			mmLabel67.AutoSize = true;
			mmLabel67.BackColor = System.Drawing.Color.Transparent;
			mmLabel67.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel67.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel67.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel67.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel67.IsFieldHeader = false;
			mmLabel67.IsRequired = false;
			mmLabel67.Location = new System.Drawing.Point(3, 118);
			mmLabel67.Name = "mmLabel67";
			mmLabel67.PenWidth = 1f;
			mmLabel67.ShowBorder = false;
			mmLabel67.Size = new System.Drawing.Size(54, 13);
			mmLabel67.TabIndex = 18;
			mmLabel67.Text = "P.Type 6:";
			mmLabel68.AutoSize = true;
			mmLabel68.BackColor = System.Drawing.Color.Transparent;
			mmLabel68.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel68.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel68.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel68.IsFieldHeader = false;
			mmLabel68.IsRequired = false;
			mmLabel68.Location = new System.Drawing.Point(3, 72);
			mmLabel68.Name = "mmLabel68";
			mmLabel68.PenWidth = 1f;
			mmLabel68.ShowBorder = false;
			mmLabel68.Size = new System.Drawing.Size(54, 13);
			mmLabel68.TabIndex = 16;
			mmLabel68.Text = "P.Type 4:";
			mmLabel68.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxPType5Name.BackColor = System.Drawing.Color.White;
			textBoxPType5Name.CustomReportFieldName = "";
			textBoxPType5Name.CustomReportKey = "";
			textBoxPType5Name.CustomReportValueType = 1;
			textBoxPType5Name.IsComboTextBox = false;
			textBoxPType5Name.IsModified = false;
			textBoxPType5Name.IsRequired = true;
			textBoxPType5Name.Location = new System.Drawing.Point(127, 93);
			textBoxPType5Name.MaxLength = 15;
			textBoxPType5Name.Name = "textBoxPType5Name";
			textBoxPType5Name.Size = new System.Drawing.Size(229, 20);
			textBoxPType5Name.TabIndex = 14;
			textBoxPType5Name.Text = "P.Type 5";
			textBoxPType6Name.BackColor = System.Drawing.Color.White;
			textBoxPType6Name.CustomReportFieldName = "";
			textBoxPType6Name.CustomReportKey = "";
			textBoxPType6Name.CustomReportValueType = 1;
			textBoxPType6Name.IsComboTextBox = false;
			textBoxPType6Name.IsModified = false;
			textBoxPType6Name.Location = new System.Drawing.Point(127, 115);
			textBoxPType6Name.MaxLength = 15;
			textBoxPType6Name.Name = "textBoxPType6Name";
			textBoxPType6Name.Size = new System.Drawing.Size(229, 20);
			textBoxPType6Name.TabIndex = 15;
			textBoxPType6Name.Text = "P.Type 6";
			textBoxPType4Name.BackColor = System.Drawing.Color.White;
			textBoxPType4Name.CustomReportFieldName = "";
			textBoxPType4Name.CustomReportKey = "";
			textBoxPType4Name.CustomReportValueType = 1;
			textBoxPType4Name.IsComboTextBox = false;
			textBoxPType4Name.IsModified = false;
			textBoxPType4Name.IsRequired = true;
			textBoxPType4Name.Location = new System.Drawing.Point(127, 71);
			textBoxPType4Name.MaxLength = 15;
			textBoxPType4Name.Name = "textBoxPType4Name";
			textBoxPType4Name.Size = new System.Drawing.Size(229, 20);
			textBoxPType4Name.TabIndex = 13;
			textBoxPType4Name.Text = "P.Type 4";
			mmLabel69.AutoSize = true;
			mmLabel69.BackColor = System.Drawing.Color.Transparent;
			mmLabel69.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel69.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel69.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel69.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel69.IsFieldHeader = false;
			mmLabel69.IsRequired = false;
			mmLabel69.Location = new System.Drawing.Point(3, 95);
			mmLabel69.Name = "mmLabel69";
			mmLabel69.PenWidth = 1f;
			mmLabel69.ShowBorder = false;
			mmLabel69.Size = new System.Drawing.Size(54, 13);
			mmLabel69.TabIndex = 17;
			mmLabel69.Text = "P.Type 5:";
			mmLabel64.AutoSize = true;
			mmLabel64.BackColor = System.Drawing.Color.Transparent;
			mmLabel64.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel64.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel64.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel64.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel64.IsFieldHeader = false;
			mmLabel64.IsRequired = false;
			mmLabel64.Location = new System.Drawing.Point(3, 51);
			mmLabel64.Name = "mmLabel64";
			mmLabel64.PenWidth = 1f;
			mmLabel64.ShowBorder = false;
			mmLabel64.Size = new System.Drawing.Size(54, 13);
			mmLabel64.TabIndex = 12;
			mmLabel64.Text = "P.Type 3:";
			mmLabel66.AutoSize = true;
			mmLabel66.BackColor = System.Drawing.Color.Transparent;
			mmLabel66.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel66.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel66.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel66.IsFieldHeader = false;
			mmLabel66.IsRequired = false;
			mmLabel66.Location = new System.Drawing.Point(3, 5);
			mmLabel66.Name = "mmLabel66";
			mmLabel66.PenWidth = 1f;
			mmLabel66.ShowBorder = false;
			mmLabel66.Size = new System.Drawing.Size(54, 13);
			mmLabel66.TabIndex = 7;
			mmLabel66.Text = "P.Type 1:";
			mmLabel66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxPType2Name.BackColor = System.Drawing.Color.White;
			textBoxPType2Name.CustomReportFieldName = "";
			textBoxPType2Name.CustomReportKey = "";
			textBoxPType2Name.CustomReportValueType = 1;
			textBoxPType2Name.IsComboTextBox = false;
			textBoxPType2Name.IsModified = false;
			textBoxPType2Name.IsRequired = true;
			textBoxPType2Name.Location = new System.Drawing.Point(127, 26);
			textBoxPType2Name.MaxLength = 15;
			textBoxPType2Name.Name = "textBoxPType2Name";
			textBoxPType2Name.Size = new System.Drawing.Size(229, 20);
			textBoxPType2Name.TabIndex = 1;
			textBoxPType2Name.Text = "P.Type 2";
			textBoxPType3Name.BackColor = System.Drawing.Color.White;
			textBoxPType3Name.CustomReportFieldName = "";
			textBoxPType3Name.CustomReportKey = "";
			textBoxPType3Name.CustomReportValueType = 1;
			textBoxPType3Name.IsComboTextBox = false;
			textBoxPType3Name.IsModified = false;
			textBoxPType3Name.Location = new System.Drawing.Point(127, 48);
			textBoxPType3Name.MaxLength = 15;
			textBoxPType3Name.Name = "textBoxPType3Name";
			textBoxPType3Name.Size = new System.Drawing.Size(229, 20);
			textBoxPType3Name.TabIndex = 2;
			textBoxPType3Name.Text = "P.Type 3";
			textBoxPType1Name.BackColor = System.Drawing.Color.White;
			textBoxPType1Name.CustomReportFieldName = "";
			textBoxPType1Name.CustomReportKey = "";
			textBoxPType1Name.CustomReportValueType = 1;
			textBoxPType1Name.IsComboTextBox = false;
			textBoxPType1Name.IsModified = false;
			textBoxPType1Name.IsRequired = true;
			textBoxPType1Name.Location = new System.Drawing.Point(127, 4);
			textBoxPType1Name.MaxLength = 15;
			textBoxPType1Name.Name = "textBoxPType1Name";
			textBoxPType1Name.Size = new System.Drawing.Size(229, 20);
			textBoxPType1Name.TabIndex = 0;
			textBoxPType1Name.Text = "P.Type 1";
			mmLabel65.AutoSize = true;
			mmLabel65.BackColor = System.Drawing.Color.Transparent;
			mmLabel65.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel65.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel65.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel65.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel65.IsFieldHeader = false;
			mmLabel65.IsRequired = false;
			mmLabel65.Location = new System.Drawing.Point(3, 28);
			mmLabel65.Name = "mmLabel65";
			mmLabel65.PenWidth = 1f;
			mmLabel65.ShowBorder = false;
			mmLabel65.Size = new System.Drawing.Size(54, 13);
			mmLabel65.TabIndex = 10;
			mmLabel65.Text = "P.Type 2:";
			checkBoxShowMultiDimension.Location = new System.Drawing.Point(409, 438);
			checkBoxShowMultiDimension.Name = "checkBoxShowMultiDimension";
			checkBoxShowMultiDimension.Size = new System.Drawing.Size(349, 21);
			checkBoxShowMultiDimension.TabIndex = 143;
			checkBoxShowMultiDimension.Text = "Show multidimension in grid";
			checkBoxShowMultiDimension.UseVisualStyleBackColor = true;
			checkBoxShowMultiDimension.CheckedChanged += new System.EventHandler(checkBoxShowMultiDimension_CheckedChanged);
			comboBoxItemCreationOption.FormattingEnabled = true;
			comboBoxItemCreationOption.Items.AddRange(new object[4]
			{
				"General",
				"Category",
				"Item Class",
				"Brand"
			});
			comboBoxItemCreationOption.Location = new System.Drawing.Point(565, 573);
			comboBoxItemCreationOption.Name = "comboBoxItemCreationOption";
			comboBoxItemCreationOption.Size = new System.Drawing.Size(121, 21);
			comboBoxItemCreationOption.TabIndex = 142;
			comboBoxItemCreationOption.SelectedIndexChanged += new System.EventHandler(comboBoxItemCreationOption_SelectedIndexChanged);
			mmLabel53.AutoSize = true;
			mmLabel53.BackColor = System.Drawing.Color.Transparent;
			mmLabel53.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel53.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel53.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel53.IsFieldHeader = false;
			mmLabel53.IsRequired = false;
			mmLabel53.Location = new System.Drawing.Point(400, 576);
			mmLabel53.Name = "mmLabel53";
			mmLabel53.PenWidth = 1f;
			mmLabel53.ShowBorder = false;
			mmLabel53.Size = new System.Drawing.Size(154, 13);
			mmLabel53.TabIndex = 141;
			mmLabel53.Text = "Item Code Creation Based On:";
			mmLabel53.Click += new System.EventHandler(mmLabel53_Click);
			groupBox36.Controls.Add(textBoxDescription3);
			groupBox36.Controls.Add(mmLabel57);
			groupBox36.Location = new System.Drawing.Point(13, 224);
			groupBox36.Name = "groupBox36";
			groupBox36.Size = new System.Drawing.Size(377, 54);
			groupBox36.TabIndex = 140;
			groupBox36.TabStop = false;
			groupBox36.Text = "Other Names";
			textBoxDescription3.BackColor = System.Drawing.Color.White;
			textBoxDescription3.CustomReportFieldName = "";
			textBoxDescription3.CustomReportKey = "";
			textBoxDescription3.CustomReportValueType = 1;
			textBoxDescription3.IsComboTextBox = false;
			textBoxDescription3.IsModified = false;
			textBoxDescription3.IsRequired = true;
			textBoxDescription3.Location = new System.Drawing.Point(133, 11);
			textBoxDescription3.MaxLength = 15;
			textBoxDescription3.Name = "textBoxDescription3";
			textBoxDescription3.Size = new System.Drawing.Size(229, 20);
			textBoxDescription3.TabIndex = 0;
			textBoxDescription3.Text = "Description 3";
			mmLabel57.AutoSize = true;
			mmLabel57.BackColor = System.Drawing.Color.Transparent;
			mmLabel57.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel57.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel57.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel57.IsFieldHeader = false;
			mmLabel57.IsRequired = false;
			mmLabel57.Location = new System.Drawing.Point(10, 18);
			mmLabel57.Name = "mmLabel57";
			mmLabel57.PenWidth = 1f;
			mmLabel57.ShowBorder = false;
			mmLabel57.Size = new System.Drawing.Size(73, 13);
			mmLabel57.TabIndex = 7;
			mmLabel57.Text = "Description 3:";
			mmLabel57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			checkBoxActivatePartsDetails.Location = new System.Drawing.Point(409, 416);
			checkBoxActivatePartsDetails.Name = "checkBoxActivatePartsDetails";
			checkBoxActivatePartsDetails.Size = new System.Drawing.Size(349, 21);
			checkBoxActivatePartsDetails.TabIndex = 139;
			checkBoxActivatePartsDetails.Text = "Activate Parts Details";
			checkBoxActivatePartsDetails.UseVisualStyleBackColor = true;
			checkBoxActivatePartsDetails.CheckedChanged += new System.EventHandler(checkBoxActivatePartsDetails_CheckedChanged);
			groupBox35.Controls.Add(mmTextBox1);
			groupBox35.Controls.Add(txtBoxSpecificationName);
			groupBox35.Controls.Add(mmLabel54);
			groupBox35.Controls.Add(mmLabel55);
			groupBox35.Location = new System.Drawing.Point(13, 413);
			groupBox35.Name = "groupBox35";
			groupBox35.Size = new System.Drawing.Size(377, 60);
			groupBox35.TabIndex = 13;
			groupBox35.TabStop = false;
			groupBox35.Text = "Specification on Grid";
			mmTextBox1.BackColor = System.Drawing.Color.White;
			mmTextBox1.CustomReportFieldName = "";
			mmTextBox1.CustomReportKey = "";
			mmTextBox1.CustomReportValueType = 1;
			mmTextBox1.IsComboTextBox = false;
			mmTextBox1.IsModified = false;
			mmTextBox1.IsRequired = true;
			mmTextBox1.Location = new System.Drawing.Point(133, 33);
			mmTextBox1.MaxLength = 15;
			mmTextBox1.Name = "mmTextBox1";
			mmTextBox1.Size = new System.Drawing.Size(229, 20);
			mmTextBox1.TabIndex = 1;
			mmTextBox1.Text = "Attribute 2";
			mmTextBox1.Visible = false;
			txtBoxSpecificationName.BackColor = System.Drawing.Color.White;
			txtBoxSpecificationName.CustomReportFieldName = "";
			txtBoxSpecificationName.CustomReportKey = "";
			txtBoxSpecificationName.CustomReportValueType = 1;
			txtBoxSpecificationName.IsComboTextBox = false;
			txtBoxSpecificationName.IsModified = false;
			txtBoxSpecificationName.IsRequired = true;
			txtBoxSpecificationName.Location = new System.Drawing.Point(133, 11);
			txtBoxSpecificationName.MaxLength = 15;
			txtBoxSpecificationName.Name = "txtBoxSpecificationName";
			txtBoxSpecificationName.Size = new System.Drawing.Size(229, 20);
			txtBoxSpecificationName.TabIndex = 0;
			txtBoxSpecificationName.Text = "SpecificationID";
			mmLabel54.AutoSize = true;
			mmLabel54.BackColor = System.Drawing.Color.Transparent;
			mmLabel54.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel54.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel54.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel54.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel54.IsFieldHeader = false;
			mmLabel54.IsRequired = false;
			mmLabel54.Location = new System.Drawing.Point(10, 34);
			mmLabel54.Name = "mmLabel54";
			mmLabel54.PenWidth = 1f;
			mmLabel54.ShowBorder = false;
			mmLabel54.Size = new System.Drawing.Size(63, 13);
			mmLabel54.TabIndex = 10;
			mmLabel54.Text = "Attribute 2:";
			mmLabel54.Visible = false;
			mmLabel55.AutoSize = true;
			mmLabel55.BackColor = System.Drawing.Color.Transparent;
			mmLabel55.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel55.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel55.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel55.IsFieldHeader = false;
			mmLabel55.IsRequired = false;
			mmLabel55.Location = new System.Drawing.Point(10, 16);
			mmLabel55.Name = "mmLabel55";
			mmLabel55.PenWidth = 1f;
			mmLabel55.ShowBorder = false;
			mmLabel55.Size = new System.Drawing.Size(101, 13);
			mmLabel55.TabIndex = 7;
			mmLabel55.Text = "Specification Name:";
			mmLabel55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox31.Controls.Add(checkBoxVehicleAnlysis);
			groupBox31.Controls.Add(checkBoxVehicleAnalysis);
			groupBox31.Controls.Add(panelVehicleanalysis);
			groupBox31.Controls.Add(groupBox32);
			groupBox31.Location = new System.Drawing.Point(396, 490);
			groupBox31.Name = "groupBox31";
			groupBox31.Size = new System.Drawing.Size(369, 83);
			groupBox31.TabIndex = 138;
			groupBox31.TabStop = false;
			groupBox31.Enter += new System.EventHandler(groupBox31_Enter);
			checkBoxVehicleAnlysis.AutoSize = true;
			checkBoxVehicleAnlysis.Location = new System.Drawing.Point(13, 0);
			checkBoxVehicleAnlysis.Name = "checkBoxVehicleAnlysis";
			checkBoxVehicleAnlysis.Size = new System.Drawing.Size(161, 17);
			checkBoxVehicleAnlysis.TabIndex = 139;
			checkBoxVehicleAnlysis.Text = "Track Expense with Analysis";
			checkBoxVehicleAnlysis.UseVisualStyleBackColor = true;
			checkBoxVehicleAnalysis.AutoSize = true;
			checkBoxVehicleAnalysis.Location = new System.Drawing.Point(396, 0);
			checkBoxVehicleAnalysis.Name = "checkBoxVehicleAnalysis";
			checkBoxVehicleAnalysis.Size = new System.Drawing.Size(161, 17);
			checkBoxVehicleAnalysis.TabIndex = 138;
			checkBoxVehicleAnalysis.Text = "Track Expense with Analysis";
			checkBoxVehicleAnalysis.UseVisualStyleBackColor = true;
			panelVehicleanalysis.Controls.Add(comboBoxVehicleAnalysis);
			panelVehicleanalysis.Controls.Add(mmLabel47);
			panelVehicleanalysis.Controls.Add(textboxVehicleanalysisPrefix);
			panelVehicleanalysis.Controls.Add(mmLabel48);
			panelVehicleanalysis.Location = new System.Drawing.Point(7, 19);
			panelVehicleanalysis.Name = "panelVehicleanalysis";
			panelVehicleanalysis.Size = new System.Drawing.Size(283, 53);
			panelVehicleanalysis.TabIndex = 135;
			comboBoxVehicleAnalysis.Assigned = false;
			comboBoxVehicleAnalysis.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVehicleAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVehicleAnalysis.CustomReportFieldName = "";
			comboBoxVehicleAnalysis.CustomReportKey = "";
			comboBoxVehicleAnalysis.CustomReportValueType = 1;
			comboBoxVehicleAnalysis.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVehicleAnalysis.DisplayLayout.Appearance = appearance13;
			comboBoxVehicleAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVehicleAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleAnalysis.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicleAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxVehicleAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicleAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxVehicleAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVehicleAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVehicleAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVehicleAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxVehicleAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVehicleAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVehicleAnalysis.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxVehicleAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVehicleAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicleAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxVehicleAnalysis.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxVehicleAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVehicleAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxVehicleAnalysis.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxVehicleAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVehicleAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxVehicleAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVehicleAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVehicleAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVehicleAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVehicleAnalysis.Editable = true;
			comboBoxVehicleAnalysis.FilterString = "";
			comboBoxVehicleAnalysis.HasAllAccount = false;
			comboBoxVehicleAnalysis.HasCustom = false;
			comboBoxVehicleAnalysis.IsDataLoaded = false;
			comboBoxVehicleAnalysis.Location = new System.Drawing.Point(91, 4);
			comboBoxVehicleAnalysis.MaxDropDownItems = 12;
			comboBoxVehicleAnalysis.Name = "comboBoxVehicleAnalysis";
			comboBoxVehicleAnalysis.ShowInactiveItems = false;
			comboBoxVehicleAnalysis.ShowQuickAdd = true;
			comboBoxVehicleAnalysis.Size = new System.Drawing.Size(114, 20);
			comboBoxVehicleAnalysis.TabIndex = 140;
			comboBoxVehicleAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel47.AutoSize = true;
			mmLabel47.BackColor = System.Drawing.Color.Transparent;
			mmLabel47.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel47.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel47.IsFieldHeader = false;
			mmLabel47.IsRequired = false;
			mmLabel47.Location = new System.Drawing.Point(6, 28);
			mmLabel47.Name = "mmLabel47";
			mmLabel47.PenWidth = 1f;
			mmLabel47.ShowBorder = false;
			mmLabel47.Size = new System.Drawing.Size(39, 13);
			mmLabel47.TabIndex = 139;
			mmLabel47.Text = "Prefix:";
			mmLabel47.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			textboxVehicleanalysisPrefix.BackColor = System.Drawing.Color.White;
			textboxVehicleanalysisPrefix.CustomReportFieldName = "";
			textboxVehicleanalysisPrefix.CustomReportKey = "";
			textboxVehicleanalysisPrefix.CustomReportValueType = 1;
			textboxVehicleanalysisPrefix.IsComboTextBox = false;
			textboxVehicleanalysisPrefix.IsModified = false;
			textboxVehicleanalysisPrefix.IsRequired = true;
			textboxVehicleanalysisPrefix.Location = new System.Drawing.Point(91, 25);
			textboxVehicleanalysisPrefix.MaxLength = 15;
			textboxVehicleanalysisPrefix.Name = "textboxVehicleanalysisPrefix";
			textboxVehicleanalysisPrefix.Size = new System.Drawing.Size(114, 20);
			textboxVehicleanalysisPrefix.TabIndex = 138;
			mmLabel48.AutoSize = true;
			mmLabel48.BackColor = System.Drawing.Color.Transparent;
			mmLabel48.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel48.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel48.IsFieldHeader = false;
			mmLabel48.IsRequired = false;
			mmLabel48.Location = new System.Drawing.Point(5, 6);
			mmLabel48.Name = "mmLabel48";
			mmLabel48.PenWidth = 1f;
			mmLabel48.ShowBorder = false;
			mmLabel48.Size = new System.Drawing.Size(82, 13);
			mmLabel48.TabIndex = 12;
			mmLabel48.Text = "Analysis Group:";
			mmLabel48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox32.Controls.Add(radioButton7);
			groupBox32.Controls.Add(radioButton8);
			groupBox32.Controls.Add(mmLabel49);
			groupBox32.Location = new System.Drawing.Point(0, 107);
			groupBox32.Name = "groupBox32";
			groupBox32.Size = new System.Drawing.Size(372, 89);
			groupBox32.TabIndex = 135;
			groupBox32.TabStop = false;
			groupBox32.Text = "PDC Maturity Process";
			radioButton7.AutoSize = true;
			radioButton7.Checked = true;
			radioButton7.Location = new System.Drawing.Point(13, 36);
			radioButton7.Name = "radioButton7";
			radioButton7.Size = new System.Drawing.Size(120, 17);
			radioButton7.TabIndex = 1;
			radioButton7.TabStop = true;
			radioButton7.Text = "Direct Maturity Entry";
			radioButton7.UseVisualStyleBackColor = true;
			radioButton8.AutoSize = true;
			radioButton8.Location = new System.Drawing.Point(13, 59);
			radioButton8.Name = "radioButton8";
			radioButton8.Size = new System.Drawing.Size(214, 17);
			radioButton8.TabIndex = 2;
			radioButton8.Text = "Send Cheques to Bank -> Maturity Entry";
			radioButton8.UseVisualStyleBackColor = true;
			mmLabel49.AutoSize = true;
			mmLabel49.BackColor = System.Drawing.Color.Transparent;
			mmLabel49.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel49.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel49.IsFieldHeader = false;
			mmLabel49.IsRequired = false;
			mmLabel49.Location = new System.Drawing.Point(10, 16);
			mmLabel49.Name = "mmLabel49";
			mmLabel49.PenWidth = 1f;
			mmLabel49.ShowBorder = false;
			mmLabel49.Size = new System.Drawing.Size(214, 13);
			mmLabel49.TabIndex = 0;
			mmLabel49.Text = "How do you want to process PDC maturity:";
			mmLabel49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBoxLotDetails.Controls.Add(checkBoxActiveBinField);
			groupBoxLotDetails.Controls.Add(label9);
			groupBoxLotDetails.Controls.Add(textBoxRenameRef2);
			groupBoxLotDetails.Controls.Add(label8);
			groupBoxLotDetails.Controls.Add(textBoxRenameLotNo);
			groupBoxLotDetails.Location = new System.Drawing.Point(396, 297);
			groupBoxLotDetails.Name = "groupBoxLotDetails";
			groupBoxLotDetails.Size = new System.Drawing.Size(360, 115);
			groupBoxLotDetails.TabIndex = 18;
			groupBoxLotDetails.TabStop = false;
			groupBoxLotDetails.Text = "Lot Settings";
			groupBoxLotDetails.Enter += new System.EventHandler(groupBoxLotDetails_Enter);
			checkBoxActiveBinField.Location = new System.Drawing.Point(14, 78);
			checkBoxActiveBinField.Name = "checkBoxActiveBinField";
			checkBoxActiveBinField.Size = new System.Drawing.Size(210, 35);
			checkBoxActiveBinField.TabIndex = 3;
			checkBoxActiveBinField.Text = "Activate Bin ";
			checkBoxActiveBinField.UseVisualStyleBackColor = true;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(11, 51);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(82, 26);
			label9.TabIndex = 17;
			label9.Text = "Identification of \r\nRef 2 :";
			textBoxRenameRef2.BackColor = System.Drawing.Color.White;
			textBoxRenameRef2.CustomReportFieldName = "";
			textBoxRenameRef2.CustomReportKey = "";
			textBoxRenameRef2.CustomReportValueType = 1;
			textBoxRenameRef2.IsComboTextBox = false;
			textBoxRenameRef2.IsModified = false;
			textBoxRenameRef2.Location = new System.Drawing.Point(99, 54);
			textBoxRenameRef2.MaxLength = 15;
			textBoxRenameRef2.Name = "textBoxRenameRef2";
			textBoxRenameRef2.Size = new System.Drawing.Size(106, 20);
			textBoxRenameRef2.TabIndex = 16;
			textBoxRenameRef2.Text = "Reference2";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(11, 16);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(82, 26);
			label8.TabIndex = 15;
			label8.Text = "Identification of \r\nLot No :";
			textBoxRenameLotNo.BackColor = System.Drawing.Color.White;
			textBoxRenameLotNo.CustomReportFieldName = "";
			textBoxRenameLotNo.CustomReportKey = "";
			textBoxRenameLotNo.CustomReportValueType = 1;
			textBoxRenameLotNo.IsComboTextBox = false;
			textBoxRenameLotNo.IsModified = false;
			textBoxRenameLotNo.Location = new System.Drawing.Point(99, 19);
			textBoxRenameLotNo.MaxLength = 15;
			textBoxRenameLotNo.Name = "textBoxRenameLotNo";
			textBoxRenameLotNo.Size = new System.Drawing.Size(106, 20);
			textBoxRenameLotNo.TabIndex = 14;
			textBoxRenameLotNo.Text = "LotNumber";
			buttonItmFlags.Image = Micromind.ClientUI.Properties.Resources.flagred;
			buttonItmFlags.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			buttonItmFlags.Location = new System.Drawing.Point(649, 462);
			buttonItmFlags.Name = "buttonItmFlags";
			buttonItmFlags.Size = new System.Drawing.Size(124, 23);
			buttonItmFlags.TabIndex = 17;
			buttonItmFlags.Text = "Flag Names...";
			buttonItmFlags.UseVisualStyleBackColor = true;
			buttonItmFlags.Click += new System.EventHandler(buttonItmFlags_Click);
			groupBox21.Controls.Add(panel4);
			groupBox21.Controls.Add(panel5);
			groupBox21.Controls.Add(panel6);
			groupBox21.Controls.Add(panel7);
			groupBox21.Controls.Add(checkBoxInvMonth4);
			groupBox21.Controls.Add(checkBoxInvMonth3);
			groupBox21.Controls.Add(checkBoxInvMonth2);
			groupBox21.Controls.Add(checkBoxInvMonth1);
			groupBox21.Controls.Add(label5);
			groupBox21.Controls.Add(label6);
			groupBox21.Controls.Add(label7);
			groupBox21.Location = new System.Drawing.Point(396, 129);
			groupBox21.Name = "groupBox21";
			groupBox21.Size = new System.Drawing.Size(316, 162);
			groupBox21.TabIndex = 6;
			groupBox21.TabStop = false;
			groupBox21.Text = "Aging Setup";
			groupBox21.Enter += new System.EventHandler(groupBox21_Enter);
			panel4.Controls.Add(textBoxInvToMonth4);
			panel4.Controls.Add(textBoxInvFromMonth4);
			panel4.Controls.Add(textBoxInvAgingName4);
			panel4.Location = new System.Drawing.Point(23, 114);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(273, 24);
			panel4.TabIndex = 4;
			textBoxInvToMonth4.AllowDecimal = true;
			textBoxInvToMonth4.CustomReportFieldName = "";
			textBoxInvToMonth4.CustomReportKey = "";
			textBoxInvToMonth4.CustomReportValueType = 1;
			textBoxInvToMonth4.IsComboTextBox = false;
			textBoxInvToMonth4.IsModified = false;
			textBoxInvToMonth4.Location = new System.Drawing.Point(195, 2);
			textBoxInvToMonth4.MaxLength = 3;
			textBoxInvToMonth4.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxInvToMonth4.MinValue = new decimal(new int[4]);
			textBoxInvToMonth4.Name = "textBoxInvToMonth4";
			textBoxInvToMonth4.NullText = "0";
			textBoxInvToMonth4.Size = new System.Drawing.Size(66, 20);
			textBoxInvToMonth4.TabIndex = 2;
			textBoxInvToMonth4.Text = "120";
			textBoxInvToMonth4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInvFromMonth4.AllowDecimal = true;
			textBoxInvFromMonth4.CustomReportFieldName = "";
			textBoxInvFromMonth4.CustomReportKey = "";
			textBoxInvFromMonth4.CustomReportValueType = 1;
			textBoxInvFromMonth4.IsComboTextBox = false;
			textBoxInvFromMonth4.IsModified = false;
			textBoxInvFromMonth4.Location = new System.Drawing.Point(127, 2);
			textBoxInvFromMonth4.MaxLength = 3;
			textBoxInvFromMonth4.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxInvFromMonth4.MinValue = new decimal(new int[4]);
			textBoxInvFromMonth4.Name = "textBoxInvFromMonth4";
			textBoxInvFromMonth4.NullText = "0";
			textBoxInvFromMonth4.Size = new System.Drawing.Size(66, 20);
			textBoxInvFromMonth4.TabIndex = 1;
			textBoxInvFromMonth4.Text = "91";
			textBoxInvFromMonth4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInvAgingName4.Location = new System.Drawing.Point(3, 2);
			textBoxInvAgingName4.Name = "textBoxInvAgingName4";
			textBoxInvAgingName4.Size = new System.Drawing.Size(123, 20);
			textBoxInvAgingName4.TabIndex = 0;
			textBoxInvAgingName4.Text = "91 - 120 Days";
			panel5.Controls.Add(textBoxInvToMonth3);
			panel5.Controls.Add(textBoxInvFromMonth3);
			panel5.Controls.Add(textBoxInvAgingName3);
			panel5.Location = new System.Drawing.Point(23, 87);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(273, 24);
			panel5.TabIndex = 3;
			textBoxInvToMonth3.AllowDecimal = true;
			textBoxInvToMonth3.CustomReportFieldName = "";
			textBoxInvToMonth3.CustomReportKey = "";
			textBoxInvToMonth3.CustomReportValueType = 1;
			textBoxInvToMonth3.IsComboTextBox = false;
			textBoxInvToMonth3.IsModified = false;
			textBoxInvToMonth3.Location = new System.Drawing.Point(195, 3);
			textBoxInvToMonth3.MaxLength = 3;
			textBoxInvToMonth3.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxInvToMonth3.MinValue = new decimal(new int[4]);
			textBoxInvToMonth3.Name = "textBoxInvToMonth3";
			textBoxInvToMonth3.NullText = "0";
			textBoxInvToMonth3.Size = new System.Drawing.Size(66, 20);
			textBoxInvToMonth3.TabIndex = 2;
			textBoxInvToMonth3.Text = "90";
			textBoxInvToMonth3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInvFromMonth3.AllowDecimal = true;
			textBoxInvFromMonth3.CustomReportFieldName = "";
			textBoxInvFromMonth3.CustomReportKey = "";
			textBoxInvFromMonth3.CustomReportValueType = 1;
			textBoxInvFromMonth3.IsComboTextBox = false;
			textBoxInvFromMonth3.IsModified = false;
			textBoxInvFromMonth3.Location = new System.Drawing.Point(127, 3);
			textBoxInvFromMonth3.MaxLength = 3;
			textBoxInvFromMonth3.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxInvFromMonth3.MinValue = new decimal(new int[4]);
			textBoxInvFromMonth3.Name = "textBoxInvFromMonth3";
			textBoxInvFromMonth3.NullText = "0";
			textBoxInvFromMonth3.Size = new System.Drawing.Size(66, 20);
			textBoxInvFromMonth3.TabIndex = 1;
			textBoxInvFromMonth3.Text = "61";
			textBoxInvFromMonth3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInvAgingName3.Location = new System.Drawing.Point(3, 3);
			textBoxInvAgingName3.Name = "textBoxInvAgingName3";
			textBoxInvAgingName3.Size = new System.Drawing.Size(123, 20);
			textBoxInvAgingName3.TabIndex = 0;
			textBoxInvAgingName3.Text = "61 - 90 Days";
			panel6.Controls.Add(textBoxInvToMonth2);
			panel6.Controls.Add(textBoxInvFromMonth2);
			panel6.Controls.Add(textBoxInvAgingName2);
			panel6.Location = new System.Drawing.Point(23, 61);
			panel6.Name = "panel6";
			panel6.Size = new System.Drawing.Size(273, 24);
			panel6.TabIndex = 2;
			textBoxInvToMonth2.AllowDecimal = true;
			textBoxInvToMonth2.CustomReportFieldName = "";
			textBoxInvToMonth2.CustomReportKey = "";
			textBoxInvToMonth2.CustomReportValueType = 1;
			textBoxInvToMonth2.IsComboTextBox = false;
			textBoxInvToMonth2.IsModified = false;
			textBoxInvToMonth2.Location = new System.Drawing.Point(196, 3);
			textBoxInvToMonth2.MaxLength = 3;
			textBoxInvToMonth2.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxInvToMonth2.MinValue = new decimal(new int[4]);
			textBoxInvToMonth2.Name = "textBoxInvToMonth2";
			textBoxInvToMonth2.NullText = "0";
			textBoxInvToMonth2.Size = new System.Drawing.Size(66, 20);
			textBoxInvToMonth2.TabIndex = 2;
			textBoxInvToMonth2.Text = "60";
			textBoxInvToMonth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInvFromMonth2.AllowDecimal = true;
			textBoxInvFromMonth2.CustomReportFieldName = "";
			textBoxInvFromMonth2.CustomReportKey = "";
			textBoxInvFromMonth2.CustomReportValueType = 1;
			textBoxInvFromMonth2.IsComboTextBox = false;
			textBoxInvFromMonth2.IsModified = false;
			textBoxInvFromMonth2.Location = new System.Drawing.Point(127, 3);
			textBoxInvFromMonth2.MaxLength = 3;
			textBoxInvFromMonth2.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxInvFromMonth2.MinValue = new decimal(new int[4]);
			textBoxInvFromMonth2.Name = "textBoxInvFromMonth2";
			textBoxInvFromMonth2.NullText = "0";
			textBoxInvFromMonth2.Size = new System.Drawing.Size(66, 20);
			textBoxInvFromMonth2.TabIndex = 1;
			textBoxInvFromMonth2.Text = "31";
			textBoxInvFromMonth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInvAgingName2.Location = new System.Drawing.Point(3, 3);
			textBoxInvAgingName2.Name = "textBoxInvAgingName2";
			textBoxInvAgingName2.Size = new System.Drawing.Size(123, 20);
			textBoxInvAgingName2.TabIndex = 0;
			textBoxInvAgingName2.Text = "31- 60 Days";
			panel7.Controls.Add(textBoxInvToMonth1);
			panel7.Controls.Add(textBoxInvFromMonth1);
			panel7.Controls.Add(textBoxInvAgingName1);
			panel7.Location = new System.Drawing.Point(23, 34);
			panel7.Name = "panel7";
			panel7.Size = new System.Drawing.Size(273, 24);
			panel7.TabIndex = 1;
			textBoxInvToMonth1.AllowDecimal = true;
			textBoxInvToMonth1.CustomReportFieldName = "";
			textBoxInvToMonth1.CustomReportKey = "";
			textBoxInvToMonth1.CustomReportValueType = 1;
			textBoxInvToMonth1.IsComboTextBox = false;
			textBoxInvToMonth1.IsModified = false;
			textBoxInvToMonth1.Location = new System.Drawing.Point(195, 2);
			textBoxInvToMonth1.MaxLength = 3;
			textBoxInvToMonth1.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxInvToMonth1.MinValue = new decimal(new int[4]);
			textBoxInvToMonth1.Name = "textBoxInvToMonth1";
			textBoxInvToMonth1.NullText = "0";
			textBoxInvToMonth1.Size = new System.Drawing.Size(66, 20);
			textBoxInvToMonth1.TabIndex = 2;
			textBoxInvToMonth1.Text = "30";
			textBoxInvToMonth1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInvFromMonth1.AllowDecimal = true;
			textBoxInvFromMonth1.CustomReportFieldName = "";
			textBoxInvFromMonth1.CustomReportKey = "";
			textBoxInvFromMonth1.CustomReportValueType = 1;
			textBoxInvFromMonth1.IsComboTextBox = false;
			textBoxInvFromMonth1.IsModified = false;
			textBoxInvFromMonth1.Location = new System.Drawing.Point(127, 2);
			textBoxInvFromMonth1.MaxLength = 3;
			textBoxInvFromMonth1.MaxValue = new decimal(new int[4]
			{
				999,
				0,
				0,
				0
			});
			textBoxInvFromMonth1.MinValue = new decimal(new int[4]);
			textBoxInvFromMonth1.Name = "textBoxInvFromMonth1";
			textBoxInvFromMonth1.NullText = "0";
			textBoxInvFromMonth1.Size = new System.Drawing.Size(66, 20);
			textBoxInvFromMonth1.TabIndex = 1;
			textBoxInvFromMonth1.Text = "0";
			textBoxInvFromMonth1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInvAgingName1.Location = new System.Drawing.Point(3, 2);
			textBoxInvAgingName1.Name = "textBoxInvAgingName1";
			textBoxInvAgingName1.Size = new System.Drawing.Size(123, 20);
			textBoxInvAgingName1.TabIndex = 0;
			textBoxInvAgingName1.Text = "0 - 30 Days";
			checkBoxInvMonth4.AutoSize = true;
			checkBoxInvMonth4.Checked = true;
			checkBoxInvMonth4.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxInvMonth4.Location = new System.Drawing.Point(7, 118);
			checkBoxInvMonth4.Name = "checkBoxInvMonth4";
			checkBoxInvMonth4.Size = new System.Drawing.Size(15, 14);
			checkBoxInvMonth4.TabIndex = 8;
			checkBoxInvMonth4.UseVisualStyleBackColor = true;
			checkBoxInvMonth3.AutoSize = true;
			checkBoxInvMonth3.Checked = true;
			checkBoxInvMonth3.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxInvMonth3.Location = new System.Drawing.Point(7, 93);
			checkBoxInvMonth3.Name = "checkBoxInvMonth3";
			checkBoxInvMonth3.Size = new System.Drawing.Size(15, 14);
			checkBoxInvMonth3.TabIndex = 7;
			checkBoxInvMonth3.UseVisualStyleBackColor = true;
			checkBoxInvMonth2.AutoSize = true;
			checkBoxInvMonth2.Checked = true;
			checkBoxInvMonth2.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxInvMonth2.Location = new System.Drawing.Point(6, 67);
			checkBoxInvMonth2.Name = "checkBoxInvMonth2";
			checkBoxInvMonth2.Size = new System.Drawing.Size(15, 14);
			checkBoxInvMonth2.TabIndex = 5;
			checkBoxInvMonth2.UseVisualStyleBackColor = true;
			checkBoxInvMonth1.AutoSize = true;
			checkBoxInvMonth1.Checked = true;
			checkBoxInvMonth1.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxInvMonth1.Location = new System.Drawing.Point(6, 41);
			checkBoxInvMonth1.Name = "checkBoxInvMonth1";
			checkBoxInvMonth1.Size = new System.Drawing.Size(15, 14);
			checkBoxInvMonth1.TabIndex = 3;
			checkBoxInvMonth1.UseVisualStyleBackColor = true;
			label5.Location = new System.Drawing.Point(216, 14);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(68, 18);
			label5.TabIndex = 5;
			label5.Text = "To";
			label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label6.Location = new System.Drawing.Point(147, 14);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(69, 18);
			label6.TabIndex = 5;
			label6.Text = "From";
			label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(27, 19);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(67, 13);
			label7.TabIndex = 5;
			label7.Text = "Aging Period";
			groupBox16.Controls.Add(checkBoxenablecostondelete);
			groupBox16.Controls.Add(checkBoxLoadDescFromPriceList);
			groupBox16.Location = new System.Drawing.Point(396, 73);
			groupBox16.Name = "groupBox16";
			groupBox16.Size = new System.Drawing.Size(377, 54);
			groupBox16.TabIndex = 4;
			groupBox16.TabStop = false;
			groupBox16.Text = "Price List";
			groupBox16.Enter += new System.EventHandler(groupBox16_Enter);
			checkBoxenablecostondelete.Checked = true;
			checkBoxenablecostondelete.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxenablecostondelete.Location = new System.Drawing.Point(238, 13);
			checkBoxenablecostondelete.Name = "checkBoxenablecostondelete";
			checkBoxenablecostondelete.Size = new System.Drawing.Size(128, 35);
			checkBoxenablecostondelete.TabIndex = 3;
			checkBoxenablecostondelete.Text = "Enable Cost on Delete";
			checkBoxenablecostondelete.UseVisualStyleBackColor = true;
			checkBoxLoadDescFromPriceList.Location = new System.Drawing.Point(7, 13);
			checkBoxLoadDescFromPriceList.Name = "checkBoxLoadDescFromPriceList";
			checkBoxLoadDescFromPriceList.Size = new System.Drawing.Size(210, 35);
			checkBoxLoadDescFromPriceList.TabIndex = 2;
			checkBoxLoadDescFromPriceList.Text = "Load Item Description from Price List";
			checkBoxLoadDescFromPriceList.UseVisualStyleBackColor = true;
			groupBox5.Controls.Add(mmLabel1);
			groupBox5.Controls.Add(textBoxAttribute2Name);
			groupBox5.Controls.Add(textBoxAttribute1Name);
			groupBox5.Controls.Add(mmLabel2);
			groupBox5.Controls.Add(mmLabel3);
			groupBox5.Controls.Add(textBoxAttribute3Name);
			groupBox5.Location = new System.Drawing.Point(13, 103);
			groupBox5.Name = "groupBox5";
			groupBox5.Size = new System.Drawing.Size(377, 89);
			groupBox5.TabIndex = 1;
			groupBox5.TabStop = false;
			groupBox5.Text = "Attributes Name";
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(10, 64);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(63, 13);
			mmLabel1.TabIndex = 12;
			mmLabel1.Text = "Attribute 3:";
			textBoxAttribute2Name.BackColor = System.Drawing.Color.White;
			textBoxAttribute2Name.CustomReportFieldName = "";
			textBoxAttribute2Name.CustomReportKey = "";
			textBoxAttribute2Name.CustomReportValueType = 1;
			textBoxAttribute2Name.IsComboTextBox = false;
			textBoxAttribute2Name.IsModified = false;
			textBoxAttribute2Name.IsRequired = true;
			textBoxAttribute2Name.Location = new System.Drawing.Point(133, 40);
			textBoxAttribute2Name.MaxLength = 15;
			textBoxAttribute2Name.Name = "textBoxAttribute2Name";
			textBoxAttribute2Name.Size = new System.Drawing.Size(229, 20);
			textBoxAttribute2Name.TabIndex = 1;
			textBoxAttribute2Name.Text = "Attribute 2";
			textBoxAttribute1Name.BackColor = System.Drawing.Color.White;
			textBoxAttribute1Name.CustomReportFieldName = "";
			textBoxAttribute1Name.CustomReportKey = "";
			textBoxAttribute1Name.CustomReportValueType = 1;
			textBoxAttribute1Name.IsComboTextBox = false;
			textBoxAttribute1Name.IsModified = false;
			textBoxAttribute1Name.IsRequired = true;
			textBoxAttribute1Name.Location = new System.Drawing.Point(133, 18);
			textBoxAttribute1Name.MaxLength = 15;
			textBoxAttribute1Name.Name = "textBoxAttribute1Name";
			textBoxAttribute1Name.Size = new System.Drawing.Size(229, 20);
			textBoxAttribute1Name.TabIndex = 0;
			textBoxAttribute1Name.Text = "Attribute 1";
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(10, 41);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(63, 13);
			mmLabel2.TabIndex = 10;
			mmLabel2.Text = "Attribute 2:";
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(10, 18);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(63, 13);
			mmLabel3.TabIndex = 7;
			mmLabel3.Text = "Attribute 1:";
			mmLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxAttribute3Name.BackColor = System.Drawing.Color.White;
			textBoxAttribute3Name.CustomReportFieldName = "";
			textBoxAttribute3Name.CustomReportKey = "";
			textBoxAttribute3Name.CustomReportValueType = 1;
			textBoxAttribute3Name.IsComboTextBox = false;
			textBoxAttribute3Name.IsModified = false;
			textBoxAttribute3Name.Location = new System.Drawing.Point(133, 62);
			textBoxAttribute3Name.MaxLength = 15;
			textBoxAttribute3Name.Name = "textBoxAttribute3Name";
			textBoxAttribute3Name.Size = new System.Drawing.Size(229, 20);
			textBoxAttribute3Name.TabIndex = 2;
			textBoxAttribute3Name.Text = "Attribute 3";
			groupBox3.Controls.Add(radioButtonMatrixDescAttribute);
			groupBox3.Controls.Add(radioButtonMatrixDescOnly);
			groupBox3.Controls.Add(mmLabel4);
			groupBox3.Location = new System.Drawing.Point(13, 475);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(377, 94);
			groupBox3.TabIndex = 3;
			groupBox3.TabStop = false;
			groupBox3.Text = "Matrix Item";
			radioButtonMatrixDescAttribute.AutoSize = true;
			radioButtonMatrixDescAttribute.Checked = true;
			radioButtonMatrixDescAttribute.Location = new System.Drawing.Point(24, 62);
			radioButtonMatrixDescAttribute.Name = "radioButtonMatrixDescAttribute";
			radioButtonMatrixDescAttribute.Size = new System.Drawing.Size(207, 17);
			radioButtonMatrixDescAttribute.TabIndex = 2;
			radioButtonMatrixDescAttribute.TabStop = true;
			radioButtonMatrixDescAttribute.Text = "Matrix parent description and attributes";
			radioButtonMatrixDescAttribute.UseVisualStyleBackColor = true;
			radioButtonMatrixDescOnly.AutoSize = true;
			radioButtonMatrixDescOnly.Location = new System.Drawing.Point(24, 39);
			radioButtonMatrixDescOnly.Name = "radioButtonMatrixDescOnly";
			radioButtonMatrixDescOnly.Size = new System.Drawing.Size(162, 17);
			radioButtonMatrixDescOnly.TabIndex = 2;
			radioButtonMatrixDescOnly.Text = "Matrix parent description only";
			radioButtonMatrixDescOnly.UseVisualStyleBackColor = true;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(10, 21);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(308, 13);
			mmLabel4.TabIndex = 13;
			mmLabel4.Text = "Select the elements you want to use in component description:";
			mmLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox8.Controls.Add(lblDescriptions);
			groupBox8.Controls.Add(textBoxItemPrice2);
			groupBox8.Controls.Add(textBoxItemPrice1);
			groupBox8.Controls.Add(mmLabel6);
			groupBox8.Controls.Add(label1);
			groupBox8.Controls.Add(textBoxItemPrice3);
			groupBox8.Location = new System.Drawing.Point(13, 11);
			groupBox8.Name = "groupBox8";
			groupBox8.Size = new System.Drawing.Size(377, 89);
			groupBox8.TabIndex = 0;
			groupBox8.TabStop = false;
			groupBox8.Text = "Unit Prices";
			lblDescriptions.AutoSize = true;
			lblDescriptions.BackColor = System.Drawing.Color.Transparent;
			lblDescriptions.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblDescriptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDescriptions.IsFieldHeader = false;
			lblDescriptions.IsRequired = false;
			lblDescriptions.Location = new System.Drawing.Point(10, 64);
			lblDescriptions.Name = "lblDescriptions";
			lblDescriptions.PenWidth = 1f;
			lblDescriptions.ShowBorder = false;
			lblDescriptions.Size = new System.Drawing.Size(95, 13);
			lblDescriptions.TabIndex = 12;
			lblDescriptions.Text = "Unit Price 3 Name:";
			textBoxItemPrice2.BackColor = System.Drawing.Color.White;
			textBoxItemPrice2.CustomReportFieldName = "";
			textBoxItemPrice2.CustomReportKey = "";
			textBoxItemPrice2.CustomReportValueType = 1;
			textBoxItemPrice2.IsComboTextBox = false;
			textBoxItemPrice2.IsModified = false;
			textBoxItemPrice2.IsRequired = true;
			textBoxItemPrice2.Location = new System.Drawing.Point(133, 40);
			textBoxItemPrice2.MaxLength = 15;
			textBoxItemPrice2.Name = "textBoxItemPrice2";
			textBoxItemPrice2.Size = new System.Drawing.Size(229, 20);
			textBoxItemPrice2.TabIndex = 1;
			textBoxItemPrice1.BackColor = System.Drawing.Color.White;
			textBoxItemPrice1.CustomReportFieldName = "";
			textBoxItemPrice1.CustomReportKey = "";
			textBoxItemPrice1.CustomReportValueType = 1;
			textBoxItemPrice1.IsComboTextBox = false;
			textBoxItemPrice1.IsModified = false;
			textBoxItemPrice1.IsRequired = true;
			textBoxItemPrice1.Location = new System.Drawing.Point(133, 18);
			textBoxItemPrice1.MaxLength = 15;
			textBoxItemPrice1.Name = "textBoxItemPrice1";
			textBoxItemPrice1.Size = new System.Drawing.Size(229, 20);
			textBoxItemPrice1.TabIndex = 0;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(10, 41);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(95, 13);
			mmLabel6.TabIndex = 10;
			mmLabel6.Text = "Unit Price 2 Name:";
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f);
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.IsFieldHeader = false;
			label1.IsRequired = false;
			label1.Location = new System.Drawing.Point(10, 18);
			label1.Name = "label1";
			label1.PenWidth = 1f;
			label1.ShowBorder = false;
			label1.Size = new System.Drawing.Size(95, 13);
			label1.TabIndex = 7;
			label1.Text = "Unit Price 1 Name:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxItemPrice3.BackColor = System.Drawing.Color.White;
			textBoxItemPrice3.CustomReportFieldName = "";
			textBoxItemPrice3.CustomReportKey = "";
			textBoxItemPrice3.CustomReportValueType = 1;
			textBoxItemPrice3.IsComboTextBox = false;
			textBoxItemPrice3.IsModified = false;
			textBoxItemPrice3.Location = new System.Drawing.Point(133, 62);
			textBoxItemPrice3.MaxLength = 15;
			textBoxItemPrice3.Name = "textBoxItemPrice3";
			textBoxItemPrice3.Size = new System.Drawing.Size(229, 20);
			textBoxItemPrice3.TabIndex = 2;
			groupBox20.Controls.Add(checkBoxShowUPCInProductCombo);
			groupBox20.Controls.Add(checkBoxShowitemFeatures);
			groupBox20.Controls.Add(checkBoxShowCostInProductCombo);
			groupBox20.Controls.Add(checkBoxShowUnitInProductCombo);
			groupBox20.Controls.Add(checkBoxShowQuantityInProductCombo);
			groupBox20.Location = new System.Drawing.Point(13, 281);
			groupBox20.Name = "groupBox20";
			groupBox20.Size = new System.Drawing.Size(377, 129);
			groupBox20.TabIndex = 5;
			groupBox20.TabStop = false;
			groupBox20.Text = "Item Dimension";
			checkBoxShowUPCInProductCombo.Location = new System.Drawing.Point(10, 84);
			checkBoxShowUPCInProductCombo.Name = "checkBoxShowUPCInProductCombo";
			checkBoxShowUPCInProductCombo.Size = new System.Drawing.Size(349, 21);
			checkBoxShowUPCInProductCombo.TabIndex = 6;
			checkBoxShowUPCInProductCombo.Text = "Show item available UPC in drop down lists";
			checkBoxShowUPCInProductCombo.UseVisualStyleBackColor = true;
			checkBoxShowitemFeatures.Location = new System.Drawing.Point(10, 104);
			checkBoxShowitemFeatures.Name = "checkBoxShowitemFeatures";
			checkBoxShowitemFeatures.Size = new System.Drawing.Size(349, 21);
			checkBoxShowitemFeatures.TabIndex = 5;
			checkBoxShowitemFeatures.Text = "Show detailed item features. (F3 button Feature)";
			checkBoxShowitemFeatures.UseVisualStyleBackColor = true;
			checkBoxShowCostInProductCombo.Location = new System.Drawing.Point(10, 64);
			checkBoxShowCostInProductCombo.Name = "checkBoxShowCostInProductCombo";
			checkBoxShowCostInProductCombo.Size = new System.Drawing.Size(349, 21);
			checkBoxShowCostInProductCombo.TabIndex = 4;
			checkBoxShowCostInProductCombo.Text = "Show item available cost in drop down lists";
			checkBoxShowCostInProductCombo.UseVisualStyleBackColor = true;
			checkBoxShowUnitInProductCombo.Location = new System.Drawing.Point(9, 38);
			checkBoxShowUnitInProductCombo.Name = "checkBoxShowUnitInProductCombo";
			checkBoxShowUnitInProductCombo.Size = new System.Drawing.Size(349, 27);
			checkBoxShowUnitInProductCombo.TabIndex = 3;
			checkBoxShowUnitInProductCombo.Text = "Show item available unit in drop down lists";
			checkBoxShowUnitInProductCombo.UseVisualStyleBackColor = true;
			checkBoxShowQuantityInProductCombo.Location = new System.Drawing.Point(9, 15);
			checkBoxShowQuantityInProductCombo.Name = "checkBoxShowQuantityInProductCombo";
			checkBoxShowQuantityInProductCombo.Size = new System.Drawing.Size(349, 27);
			checkBoxShowQuantityInProductCombo.TabIndex = 2;
			checkBoxShowQuantityInProductCombo.Text = "Show item available quantity in drop down lists";
			checkBoxShowQuantityInProductCombo.UseVisualStyleBackColor = true;
			grpCostSetting.Controls.Add(checkBoxAllCosting);
			grpCostSetting.Controls.Add(checkBoxfutureCosting);
			grpCostSetting.Location = new System.Drawing.Point(396, 8);
			grpCostSetting.Name = "grpCostSetting";
			grpCostSetting.Size = new System.Drawing.Size(377, 63);
			grpCostSetting.TabIndex = 145;
			grpCostSetting.TabStop = false;
			grpCostSetting.Text = "Cost Settings";
			checkBoxAllCosting.Location = new System.Drawing.Point(7, 35);
			checkBoxAllCosting.Name = "checkBoxAllCosting";
			checkBoxAllCosting.Size = new System.Drawing.Size(290, 22);
			checkBoxAllCosting.TabIndex = 145;
			checkBoxAllCosting.Text = "Disable All  running cost and location Qty";
			checkBoxAllCosting.UseVisualStyleBackColor = true;
			checkBoxfutureCosting.Location = new System.Drawing.Point(7, 15);
			checkBoxfutureCosting.Name = "checkBoxfutureCosting";
			checkBoxfutureCosting.Size = new System.Drawing.Size(290, 22);
			checkBoxfutureCosting.TabIndex = 144;
			checkBoxfutureCosting.Text = "Disable future costing";
			checkBoxfutureCosting.UseVisualStyleBackColor = true;
			ultraTabPageControl4.Controls.Add(groupBox29);
			ultraTabPageControl4.Controls.Add(mmLabel43);
			ultraTabPageControl4.Controls.Add(textBoxAutoresumptionDays);
			ultraTabPageControl4.Controls.Add(groupBox27);
			ultraTabPageControl4.Controls.Add(groupBox25);
			ultraTabPageControl4.Controls.Add(groupBox23);
			ultraTabPageControl4.Controls.Add(panel1);
			ultraTabPageControl4.Controls.Add(mmLabel16);
			ultraTabPageControl4.Controls.Add(textBoxBankName);
			ultraTabPageControl4.Controls.Add(comboBoxBank);
			ultraTabPageControl4.Controls.Add(mmLabel13);
			ultraTabPageControl4.Controls.Add(textBoxCompanyWPSID);
			ultraTabPageControl4.Controls.Add(mmLabel12);
			ultraTabPageControl4.Controls.Add(groupBox15);
			ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(807, 626);
			groupBox29.Controls.Add(checkBoxHRnalaysis);
			groupBox29.Controls.Add(panelHRanalysis);
			groupBox29.Controls.Add(groupBox30);
			groupBox29.Location = new System.Drawing.Point(401, 267);
			groupBox29.Name = "groupBox29";
			groupBox29.Size = new System.Drawing.Size(372, 83);
			groupBox29.TabIndex = 137;
			groupBox29.TabStop = false;
			checkBoxHRnalaysis.AutoSize = true;
			checkBoxHRnalaysis.Location = new System.Drawing.Point(11, 0);
			checkBoxHRnalaysis.Name = "checkBoxHRnalaysis";
			checkBoxHRnalaysis.Size = new System.Drawing.Size(161, 17);
			checkBoxHRnalaysis.TabIndex = 138;
			checkBoxHRnalaysis.Text = "Track Expense with Analysis";
			checkBoxHRnalaysis.UseVisualStyleBackColor = true;
			panelHRanalysis.Controls.Add(comboBoxHRAnalysis);
			panelHRanalysis.Controls.Add(mmLabel46);
			panelHRanalysis.Controls.Add(textboxHRanalysisPrefix);
			panelHRanalysis.Controls.Add(mmLabel45);
			panelHRanalysis.Location = new System.Drawing.Point(7, 19);
			panelHRanalysis.Name = "panelHRanalysis";
			panelHRanalysis.Size = new System.Drawing.Size(283, 53);
			panelHRanalysis.TabIndex = 135;
			comboBoxHRAnalysis.Assigned = false;
			comboBoxHRAnalysis.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxHRAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxHRAnalysis.CustomReportFieldName = "";
			comboBoxHRAnalysis.CustomReportKey = "";
			comboBoxHRAnalysis.CustomReportValueType = 1;
			comboBoxHRAnalysis.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxHRAnalysis.DisplayLayout.Appearance = appearance25;
			comboBoxHRAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxHRAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxHRAnalysis.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxHRAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxHRAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxHRAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxHRAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxHRAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxHRAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxHRAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxHRAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxHRAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxHRAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxHRAnalysis.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxHRAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxHRAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxHRAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxHRAnalysis.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxHRAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxHRAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxHRAnalysis.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxHRAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxHRAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxHRAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxHRAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxHRAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxHRAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxHRAnalysis.Editable = true;
			comboBoxHRAnalysis.FilterString = "";
			comboBoxHRAnalysis.HasAllAccount = false;
			comboBoxHRAnalysis.HasCustom = false;
			comboBoxHRAnalysis.IsDataLoaded = false;
			comboBoxHRAnalysis.Location = new System.Drawing.Point(91, 4);
			comboBoxHRAnalysis.MaxDropDownItems = 12;
			comboBoxHRAnalysis.Name = "comboBoxHRAnalysis";
			comboBoxHRAnalysis.ShowInactiveItems = false;
			comboBoxHRAnalysis.ShowQuickAdd = true;
			comboBoxHRAnalysis.Size = new System.Drawing.Size(114, 20);
			comboBoxHRAnalysis.TabIndex = 140;
			comboBoxHRAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel46.AutoSize = true;
			mmLabel46.BackColor = System.Drawing.Color.Transparent;
			mmLabel46.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel46.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel46.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel46.IsFieldHeader = false;
			mmLabel46.IsRequired = false;
			mmLabel46.Location = new System.Drawing.Point(6, 28);
			mmLabel46.Name = "mmLabel46";
			mmLabel46.PenWidth = 1f;
			mmLabel46.ShowBorder = false;
			mmLabel46.Size = new System.Drawing.Size(39, 13);
			mmLabel46.TabIndex = 139;
			mmLabel46.Text = "Prefix:";
			mmLabel46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			textboxHRanalysisPrefix.BackColor = System.Drawing.Color.White;
			textboxHRanalysisPrefix.CustomReportFieldName = "";
			textboxHRanalysisPrefix.CustomReportKey = "";
			textboxHRanalysisPrefix.CustomReportValueType = 1;
			textboxHRanalysisPrefix.IsComboTextBox = false;
			textboxHRanalysisPrefix.IsModified = false;
			textboxHRanalysisPrefix.IsRequired = true;
			textboxHRanalysisPrefix.Location = new System.Drawing.Point(91, 25);
			textboxHRanalysisPrefix.MaxLength = 15;
			textboxHRanalysisPrefix.Name = "textboxHRanalysisPrefix";
			textboxHRanalysisPrefix.Size = new System.Drawing.Size(114, 20);
			textboxHRanalysisPrefix.TabIndex = 138;
			mmLabel45.AutoSize = true;
			mmLabel45.BackColor = System.Drawing.Color.Transparent;
			mmLabel45.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel45.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel45.IsFieldHeader = false;
			mmLabel45.IsRequired = false;
			mmLabel45.Location = new System.Drawing.Point(5, 6);
			mmLabel45.Name = "mmLabel45";
			mmLabel45.PenWidth = 1f;
			mmLabel45.ShowBorder = false;
			mmLabel45.Size = new System.Drawing.Size(82, 13);
			mmLabel45.TabIndex = 12;
			mmLabel45.Text = "Analysis Group:";
			mmLabel45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox30.Controls.Add(radioButton9);
			groupBox30.Controls.Add(radioButton10);
			groupBox30.Controls.Add(mmLabel44);
			groupBox30.Location = new System.Drawing.Point(0, 107);
			groupBox30.Name = "groupBox30";
			groupBox30.Size = new System.Drawing.Size(372, 89);
			groupBox30.TabIndex = 135;
			groupBox30.TabStop = false;
			groupBox30.Text = "PDC Maturity Process";
			radioButton9.AutoSize = true;
			radioButton9.Checked = true;
			radioButton9.Location = new System.Drawing.Point(13, 36);
			radioButton9.Name = "radioButton9";
			radioButton9.Size = new System.Drawing.Size(120, 17);
			radioButton9.TabIndex = 1;
			radioButton9.TabStop = true;
			radioButton9.Text = "Direct Maturity Entry";
			radioButton9.UseVisualStyleBackColor = true;
			radioButton10.AutoSize = true;
			radioButton10.Location = new System.Drawing.Point(13, 59);
			radioButton10.Name = "radioButton10";
			radioButton10.Size = new System.Drawing.Size(214, 17);
			radioButton10.TabIndex = 2;
			radioButton10.Text = "Send Cheques to Bank -> Maturity Entry";
			radioButton10.UseVisualStyleBackColor = true;
			mmLabel44.AutoSize = true;
			mmLabel44.BackColor = System.Drawing.Color.Transparent;
			mmLabel44.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel44.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel44.IsFieldHeader = false;
			mmLabel44.IsRequired = false;
			mmLabel44.Location = new System.Drawing.Point(10, 16);
			mmLabel44.Name = "mmLabel44";
			mmLabel44.PenWidth = 1f;
			mmLabel44.ShowBorder = false;
			mmLabel44.Size = new System.Drawing.Size(214, 13);
			mmLabel44.TabIndex = 0;
			mmLabel44.Text = "How do you want to process PDC maturity:";
			mmLabel44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel43.AutoSize = true;
			mmLabel43.BackColor = System.Drawing.Color.Transparent;
			mmLabel43.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel43.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel43.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel43.IsFieldHeader = false;
			mmLabel43.IsRequired = false;
			mmLabel43.Location = new System.Drawing.Point(20, 351);
			mmLabel43.Name = "mmLabel43";
			mmLabel43.PenWidth = 1f;
			mmLabel43.ShowBorder = false;
			mmLabel43.Size = new System.Drawing.Size(207, 13);
			mmLabel43.TabIndex = 136;
			mmLabel43.Text = "Auto Resumption for leave days less than";
			textBoxAutoresumptionDays.AllowDecimal = true;
			textBoxAutoresumptionDays.CustomReportFieldName = "";
			textBoxAutoresumptionDays.CustomReportKey = "";
			textBoxAutoresumptionDays.CustomReportValueType = 1;
			textBoxAutoresumptionDays.IsComboTextBox = false;
			textBoxAutoresumptionDays.IsModified = false;
			textBoxAutoresumptionDays.Location = new System.Drawing.Point(231, 347);
			textBoxAutoresumptionDays.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAutoresumptionDays.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAutoresumptionDays.Name = "textBoxAutoresumptionDays";
			textBoxAutoresumptionDays.NullText = "0";
			textBoxAutoresumptionDays.Size = new System.Drawing.Size(37, 20);
			textBoxAutoresumptionDays.TabIndex = 136;
			groupBox27.Controls.Add(panel2);
			groupBox27.Controls.Add(groupBox28);
			groupBox27.Location = new System.Drawing.Point(401, 180);
			groupBox27.Name = "groupBox27";
			groupBox27.Size = new System.Drawing.Size(372, 83);
			groupBox27.TabIndex = 136;
			groupBox27.TabStop = false;
			groupBox27.Text = "Salary Deduction";
			panel2.Controls.Add(radioButtondeductiononNetDaysSal);
			panel2.Controls.Add(radioButtonDeductionoonNetDays);
			panel2.Location = new System.Drawing.Point(7, 19);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(283, 53);
			panel2.TabIndex = 135;
			radioButtondeductiononNetDaysSal.AutoSize = true;
			radioButtondeductiononNetDaysSal.Location = new System.Drawing.Point(5, 29);
			radioButtondeductiononNetDaysSal.Name = "radioButtondeductiononNetDaysSal";
			radioButtondeductiononNetDaysSal.Size = new System.Drawing.Size(182, 17);
			radioButtondeductiononNetDaysSal.TabIndex = 1;
			radioButtondeductiononNetDaysSal.Text = "Month Salary -Leave Days Salary";
			radioButtondeductiononNetDaysSal.UseVisualStyleBackColor = true;
			radioButtonDeductionoonNetDays.AutoSize = true;
			radioButtonDeductionoonNetDays.Checked = true;
			radioButtonDeductionoonNetDays.Location = new System.Drawing.Point(6, 8);
			radioButtonDeductionoonNetDays.Name = "radioButtonDeductionoonNetDays";
			radioButtonDeductionoonNetDays.Size = new System.Drawing.Size(146, 17);
			radioButtonDeductionoonNetDays.TabIndex = 0;
			radioButtonDeductionoonNetDays.TabStop = true;
			radioButtonDeductionoonNetDays.Text = "PerDay Salary * Net Days";
			radioButtonDeductionoonNetDays.UseVisualStyleBackColor = true;
			groupBox28.Controls.Add(radioButton5);
			groupBox28.Controls.Add(radioButton6);
			groupBox28.Controls.Add(mmLabel41);
			groupBox28.Location = new System.Drawing.Point(0, 107);
			groupBox28.Name = "groupBox28";
			groupBox28.Size = new System.Drawing.Size(372, 89);
			groupBox28.TabIndex = 135;
			groupBox28.TabStop = false;
			groupBox28.Text = "PDC Maturity Process";
			radioButton5.AutoSize = true;
			radioButton5.Checked = true;
			radioButton5.Location = new System.Drawing.Point(13, 36);
			radioButton5.Name = "radioButton5";
			radioButton5.Size = new System.Drawing.Size(120, 17);
			radioButton5.TabIndex = 1;
			radioButton5.TabStop = true;
			radioButton5.Text = "Direct Maturity Entry";
			radioButton5.UseVisualStyleBackColor = true;
			radioButton6.AutoSize = true;
			radioButton6.Location = new System.Drawing.Point(13, 59);
			radioButton6.Name = "radioButton6";
			radioButton6.Size = new System.Drawing.Size(214, 17);
			radioButton6.TabIndex = 2;
			radioButton6.Text = "Send Cheques to Bank -> Maturity Entry";
			radioButton6.UseVisualStyleBackColor = true;
			mmLabel41.AutoSize = true;
			mmLabel41.BackColor = System.Drawing.Color.Transparent;
			mmLabel41.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel41.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel41.IsFieldHeader = false;
			mmLabel41.IsRequired = false;
			mmLabel41.Location = new System.Drawing.Point(10, 16);
			mmLabel41.Name = "mmLabel41";
			mmLabel41.PenWidth = 1f;
			mmLabel41.ShowBorder = false;
			mmLabel41.Size = new System.Drawing.Size(214, 13);
			mmLabel41.TabIndex = 0;
			mmLabel41.Text = "How do you want to process PDC maturity:";
			mmLabel41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox25.Controls.Add(checkBoxRoundOffSalaryCalculation);
			groupBox25.Controls.Add(panel3);
			groupBox25.Controls.Add(groupBox26);
			groupBox25.Location = new System.Drawing.Point(401, 80);
			groupBox25.Name = "groupBox25";
			groupBox25.Size = new System.Drawing.Size(375, 101);
			groupBox25.TabIndex = 136;
			groupBox25.TabStop = false;
			groupBox25.Text = "Salary Calculation";
			checkBoxRoundOffSalaryCalculation.AutoSize = true;
			checkBoxRoundOffSalaryCalculation.Location = new System.Drawing.Point(154, 22);
			checkBoxRoundOffSalaryCalculation.Name = "checkBoxRoundOffSalaryCalculation";
			checkBoxRoundOffSalaryCalculation.Size = new System.Drawing.Size(138, 17);
			checkBoxRoundOffSalaryCalculation.TabIndex = 137;
			checkBoxRoundOffSalaryCalculation.Text = "Round Off Salary Sheet";
			checkBoxRoundOffSalaryCalculation.UseVisualStyleBackColor = true;
			panel3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			panel3.Controls.Add(radioButtonAnnual);
			panel3.Controls.Add(radioButtonThirtyDays);
			panel3.Controls.Add(radioButtonDaysInMonth);
			panel3.Location = new System.Drawing.Point(6, 17);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(144, 70);
			panel3.TabIndex = 136;
			radioButtonAnnual.AutoSize = true;
			radioButtonAnnual.Checked = true;
			radioButtonAnnual.Location = new System.Drawing.Point(12, 48);
			radioButtonAnnual.Name = "radioButtonAnnual";
			radioButtonAnnual.Size = new System.Drawing.Size(58, 17);
			radioButtonAnnual.TabIndex = 2;
			radioButtonAnnual.TabStop = true;
			radioButtonAnnual.Text = "Annual";
			radioButtonAnnual.UseVisualStyleBackColor = true;
			radioButtonThirtyDays.AutoSize = true;
			radioButtonThirtyDays.Location = new System.Drawing.Point(12, 28);
			radioButtonThirtyDays.Name = "radioButtonThirtyDays";
			radioButtonThirtyDays.Size = new System.Drawing.Size(64, 17);
			radioButtonThirtyDays.TabIndex = 1;
			radioButtonThirtyDays.Text = "30 Days";
			radioButtonThirtyDays.UseVisualStyleBackColor = true;
			radioButtonDaysInMonth.Anchor = System.Windows.Forms.AnchorStyles.Left;
			radioButtonDaysInMonth.AutoSize = true;
			radioButtonDaysInMonth.Location = new System.Drawing.Point(12, 4);
			radioButtonDaysInMonth.Name = "radioButtonDaysInMonth";
			radioButtonDaysInMonth.Size = new System.Drawing.Size(94, 17);
			radioButtonDaysInMonth.TabIndex = 0;
			radioButtonDaysInMonth.Text = "Days In Month";
			radioButtonDaysInMonth.UseVisualStyleBackColor = true;
			groupBox26.Controls.Add(radioButton3);
			groupBox26.Controls.Add(radioButton4);
			groupBox26.Controls.Add(mmLabel42);
			groupBox26.Location = new System.Drawing.Point(0, 107);
			groupBox26.Name = "groupBox26";
			groupBox26.Size = new System.Drawing.Size(372, 89);
			groupBox26.TabIndex = 135;
			groupBox26.TabStop = false;
			groupBox26.Text = "PDC Maturity Process";
			radioButton3.AutoSize = true;
			radioButton3.Checked = true;
			radioButton3.Location = new System.Drawing.Point(13, 36);
			radioButton3.Name = "radioButton3";
			radioButton3.Size = new System.Drawing.Size(120, 17);
			radioButton3.TabIndex = 1;
			radioButton3.TabStop = true;
			radioButton3.Text = "Direct Maturity Entry";
			radioButton3.UseVisualStyleBackColor = true;
			radioButton4.AutoSize = true;
			radioButton4.Location = new System.Drawing.Point(13, 59);
			radioButton4.Name = "radioButton4";
			radioButton4.Size = new System.Drawing.Size(214, 17);
			radioButton4.TabIndex = 2;
			radioButton4.Text = "Send Cheques to Bank -> Maturity Entry";
			radioButton4.UseVisualStyleBackColor = true;
			mmLabel42.AutoSize = true;
			mmLabel42.BackColor = System.Drawing.Color.Transparent;
			mmLabel42.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel42.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel42.IsFieldHeader = false;
			mmLabel42.IsRequired = false;
			mmLabel42.Location = new System.Drawing.Point(10, 16);
			mmLabel42.Name = "mmLabel42";
			mmLabel42.PenWidth = 1f;
			mmLabel42.ShowBorder = false;
			mmLabel42.Size = new System.Drawing.Size(214, 13);
			mmLabel42.TabIndex = 0;
			mmLabel42.Text = "How do you want to process PDC maturity:";
			mmLabel42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox23.Controls.Add(groupBox24);
			groupBox23.Controls.Add(mmLabel40);
			groupBox23.Controls.Add(textBoxRangeTo);
			groupBox23.Controls.Add(textBoxRemarkvalidationpoint);
			groupBox23.Controls.Add(textBoxRangeFrom);
			groupBox23.Controls.Add(mmLabel38);
			groupBox23.Controls.Add(mmLabel39);
			groupBox23.Location = new System.Drawing.Point(20, 232);
			groupBox23.Name = "groupBox23";
			groupBox23.Size = new System.Drawing.Size(372, 101);
			groupBox23.TabIndex = 12;
			groupBox23.TabStop = false;
			groupBox23.Text = "Appraisal";
			groupBox24.Controls.Add(radioButton1);
			groupBox24.Controls.Add(radioButton2);
			groupBox24.Controls.Add(mmLabel37);
			groupBox24.Location = new System.Drawing.Point(0, 107);
			groupBox24.Name = "groupBox24";
			groupBox24.Size = new System.Drawing.Size(372, 89);
			groupBox24.TabIndex = 135;
			groupBox24.TabStop = false;
			groupBox24.Text = "PDC Maturity Process";
			radioButton1.AutoSize = true;
			radioButton1.Checked = true;
			radioButton1.Location = new System.Drawing.Point(13, 36);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new System.Drawing.Size(120, 17);
			radioButton1.TabIndex = 1;
			radioButton1.TabStop = true;
			radioButton1.Text = "Direct Maturity Entry";
			radioButton1.UseVisualStyleBackColor = true;
			radioButton2.AutoSize = true;
			radioButton2.Location = new System.Drawing.Point(13, 59);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new System.Drawing.Size(214, 17);
			radioButton2.TabIndex = 2;
			radioButton2.Text = "Send Cheques to Bank -> Maturity Entry";
			radioButton2.UseVisualStyleBackColor = true;
			mmLabel37.AutoSize = true;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(10, 16);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(214, 13);
			mmLabel37.TabIndex = 0;
			mmLabel37.Text = "How do you want to process PDC maturity:";
			mmLabel37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel40.AutoSize = true;
			mmLabel40.BackColor = System.Drawing.Color.Transparent;
			mmLabel40.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel40.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel40.IsFieldHeader = false;
			mmLabel40.IsRequired = false;
			mmLabel40.Location = new System.Drawing.Point(185, 23);
			mmLabel40.Name = "mmLabel40";
			mmLabel40.PenWidth = 1f;
			mmLabel40.ShowBorder = false;
			mmLabel40.Size = new System.Drawing.Size(11, 13);
			mmLabel40.TabIndex = 13;
			mmLabel40.Text = "-";
			mmLabel40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxRangeTo.AllowDecimal = true;
			textBoxRangeTo.CustomReportFieldName = "";
			textBoxRangeTo.CustomReportKey = "";
			textBoxRangeTo.CustomReportValueType = 1;
			textBoxRangeTo.IsComboTextBox = false;
			textBoxRangeTo.IsModified = false;
			textBoxRangeTo.Location = new System.Drawing.Point(203, 20);
			textBoxRangeTo.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxRangeTo.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxRangeTo.Name = "textBoxRangeTo";
			textBoxRangeTo.NullText = "0";
			textBoxRangeTo.Size = new System.Drawing.Size(46, 20);
			textBoxRangeTo.TabIndex = 12;
			textBoxRemarkvalidationpoint.AllowDecimal = true;
			textBoxRemarkvalidationpoint.CustomReportFieldName = "";
			textBoxRemarkvalidationpoint.CustomReportKey = "";
			textBoxRemarkvalidationpoint.CustomReportValueType = 1;
			textBoxRemarkvalidationpoint.IsComboTextBox = false;
			textBoxRemarkvalidationpoint.IsModified = false;
			textBoxRemarkvalidationpoint.Location = new System.Drawing.Point(134, 43);
			textBoxRemarkvalidationpoint.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxRemarkvalidationpoint.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxRemarkvalidationpoint.Name = "textBoxRemarkvalidationpoint";
			textBoxRemarkvalidationpoint.NullText = "0";
			textBoxRemarkvalidationpoint.Size = new System.Drawing.Size(78, 20);
			textBoxRemarkvalidationpoint.TabIndex = 1;
			textBoxRangeFrom.AllowDecimal = true;
			textBoxRangeFrom.CustomReportFieldName = "";
			textBoxRangeFrom.CustomReportKey = "";
			textBoxRangeFrom.CustomReportValueType = 1;
			textBoxRangeFrom.IsComboTextBox = false;
			textBoxRangeFrom.IsModified = false;
			textBoxRangeFrom.Location = new System.Drawing.Point(133, 20);
			textBoxRangeFrom.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxRangeFrom.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxRangeFrom.Name = "textBoxRangeFrom";
			textBoxRangeFrom.NullText = "1";
			textBoxRangeFrom.Size = new System.Drawing.Size(46, 20);
			textBoxRangeFrom.TabIndex = 0;
			textBoxRangeFrom.Text = "1";
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(4, 46);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(130, 13);
			mmLabel38.TabIndex = 10;
			mmLabel38.Text = "Remarks Mandatory Point";
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(4, 23);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(72, 13);
			mmLabel39.TabIndex = 7;
			mmLabel39.Text = "Rating Range";
			mmLabel39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			panel1.Controls.Add(radioButtonEmployee);
			panel1.Controls.Add(radioButtonDate);
			panel1.Location = new System.Drawing.Point(146, 190);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(157, 27);
			panel1.TabIndex = 134;
			radioButtonEmployee.AutoSize = true;
			radioButtonEmployee.Location = new System.Drawing.Point(80, 4);
			radioButtonEmployee.Name = "radioButtonEmployee";
			radioButtonEmployee.Size = new System.Drawing.Size(71, 17);
			radioButtonEmployee.TabIndex = 1;
			radioButtonEmployee.Text = "Employee";
			radioButtonEmployee.UseVisualStyleBackColor = true;
			radioButtonDate.AutoSize = true;
			radioButtonDate.Checked = true;
			radioButtonDate.Location = new System.Drawing.Point(8, 4);
			radioButtonDate.Name = "radioButtonDate";
			radioButtonDate.Size = new System.Drawing.Size(48, 17);
			radioButtonDate.TabIndex = 0;
			radioButtonDate.TabStop = true;
			radioButtonDate.Text = "Date";
			radioButtonDate.UseVisualStyleBackColor = true;
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(20, 198);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(112, 13);
			mmLabel16.TabIndex = 23;
			mmLabel16.Text = "Over Time Entry Form";
			mmLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxBankName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankName.CustomReportFieldName = "";
			textBoxBankName.CustomReportKey = "";
			textBoxBankName.CustomReportValueType = 1;
			textBoxBankName.Enabled = false;
			textBoxBankName.ForeColor = System.Drawing.Color.Black;
			textBoxBankName.IsComboTextBox = false;
			textBoxBankName.IsModified = false;
			textBoxBankName.Location = new System.Drawing.Point(308, 42);
			textBoxBankName.MaxLength = 15;
			textBoxBankName.Name = "textBoxBankName";
			textBoxBankName.Size = new System.Drawing.Size(293, 20);
			textBoxBankName.TabIndex = 2;
			textBoxBankName.TabStop = false;
			comboBoxBank.Assigned = false;
			comboBoxBank.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBank.CustomReportFieldName = "";
			comboBoxBank.CustomReportKey = "";
			comboBoxBank.CustomReportValueType = 1;
			comboBoxBank.DescriptionTextBox = textBoxBankName;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBank.DisplayLayout.Appearance = appearance37;
			comboBoxBank.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBank.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxBank.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxBank.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBank.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBank.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBank.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxBank.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBank.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBank.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxBank.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBank.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxBank.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxBank.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBank.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxBank.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxBank.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBank.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxBank.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBank.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBank.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBank.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBank.Editable = true;
			comboBoxBank.FilterString = "";
			comboBoxBank.HasAllAccount = false;
			comboBoxBank.HasCustom = false;
			comboBoxBank.IsDataLoaded = false;
			comboBoxBank.Location = new System.Drawing.Point(146, 42);
			comboBoxBank.MaxDropDownItems = 12;
			comboBoxBank.Name = "comboBoxBank";
			comboBoxBank.ShowInactiveItems = false;
			comboBoxBank.ShowQuickAdd = true;
			comboBoxBank.Size = new System.Drawing.Size(161, 20);
			comboBoxBank.TabIndex = 1;
			comboBoxBank.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(20, 46);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(48, 13);
			mmLabel13.TabIndex = 22;
			mmLabel13.Text = "Bank ID:";
			mmLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxCompanyWPSID.BackColor = System.Drawing.Color.White;
			textBoxCompanyWPSID.CustomReportFieldName = "";
			textBoxCompanyWPSID.CustomReportKey = "";
			textBoxCompanyWPSID.CustomReportValueType = 1;
			textBoxCompanyWPSID.IsComboTextBox = false;
			textBoxCompanyWPSID.IsModified = false;
			textBoxCompanyWPSID.IsRequired = true;
			textBoxCompanyWPSID.Location = new System.Drawing.Point(146, 19);
			textBoxCompanyWPSID.MaxLength = 15;
			textBoxCompanyWPSID.Name = "textBoxCompanyWPSID";
			textBoxCompanyWPSID.Size = new System.Drawing.Size(161, 20);
			textBoxCompanyWPSID.TabIndex = 0;
			mmLabel12.AutoSize = true;
			mmLabel12.BackColor = System.Drawing.Color.Transparent;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(20, 22);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(120, 13);
			mmLabel12.TabIndex = 21;
			mmLabel12.Text = "Company ID (for WPS):";
			mmLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox15.Controls.Add(textBoxMonthHours);
			groupBox15.Controls.Add(textBoxDayHours);
			groupBox15.Controls.Add(comboBoxDays);
			groupBox15.Controls.Add(mmLabel9);
			groupBox15.Controls.Add(mmLabel10);
			groupBox15.Controls.Add(mmLabel11);
			groupBox15.Location = new System.Drawing.Point(20, 80);
			groupBox15.Name = "groupBox15";
			groupBox15.Size = new System.Drawing.Size(372, 101);
			groupBox15.TabIndex = 3;
			groupBox15.TabStop = false;
			groupBox15.Text = "Over Time Calculation";
			textBoxMonthHours.AllowDecimal = true;
			textBoxMonthHours.CustomReportFieldName = "";
			textBoxMonthHours.CustomReportKey = "";
			textBoxMonthHours.CustomReportValueType = 1;
			textBoxMonthHours.IsComboTextBox = false;
			textBoxMonthHours.IsModified = false;
			textBoxMonthHours.Location = new System.Drawing.Point(133, 46);
			textBoxMonthHours.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxMonthHours.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxMonthHours.Name = "textBoxMonthHours";
			textBoxMonthHours.NullText = "0";
			textBoxMonthHours.Size = new System.Drawing.Size(120, 20);
			textBoxMonthHours.TabIndex = 1;
			textBoxDayHours.AllowDecimal = true;
			textBoxDayHours.CustomReportFieldName = "";
			textBoxDayHours.CustomReportKey = "";
			textBoxDayHours.CustomReportValueType = 1;
			textBoxDayHours.IsComboTextBox = false;
			textBoxDayHours.IsModified = false;
			textBoxDayHours.Location = new System.Drawing.Point(133, 24);
			textBoxDayHours.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDayHours.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDayHours.Name = "textBoxDayHours";
			textBoxDayHours.NullText = "0";
			textBoxDayHours.Size = new System.Drawing.Size(120, 20);
			textBoxDayHours.TabIndex = 0;
			comboBoxDays.FormattingEnabled = true;
			comboBoxDays.Location = new System.Drawing.Point(133, 68);
			comboBoxDays.Name = "comboBoxDays";
			comboBoxDays.Size = new System.Drawing.Size(120, 21);
			comboBoxDays.TabIndex = 2;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(10, 70);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(49, 13);
			mmLabel9.TabIndex = 11;
			mmLabel9.Text = "Off Day:";
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(10, 48);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(119, 13);
			mmLabel10.TabIndex = 10;
			mmLabel10.Text = "Total Hours in a Month:";
			mmLabel11.AutoSize = true;
			mmLabel11.BackColor = System.Drawing.Color.Transparent;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(10, 26);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(108, 13);
			mmLabel11.TabIndex = 7;
			mmLabel11.Text = "Total Hours in a Day:";
			mmLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(807, 626);
			tabPagePOS.Controls.Add(checkBoxUpdateSalesPersonWhileSave);
			tabPagePOS.Controls.Add(checkBoxDisplayItemFeatures);
			tabPagePOS.Controls.Add(checkBoxPOSSufficientQuantity);
			tabPagePOS.Location = new System.Drawing.Point(-10000, -10000);
			tabPagePOS.Name = "tabPagePOS";
			tabPagePOS.Size = new System.Drawing.Size(807, 626);
			checkBoxUpdateSalesPersonWhileSave.AutoSize = true;
			checkBoxUpdateSalesPersonWhileSave.Location = new System.Drawing.Point(17, 72);
			checkBoxUpdateSalesPersonWhileSave.Name = "checkBoxUpdateSalesPersonWhileSave";
			checkBoxUpdateSalesPersonWhileSave.Size = new System.Drawing.Size(185, 17);
			checkBoxUpdateSalesPersonWhileSave.TabIndex = 2;
			checkBoxUpdateSalesPersonWhileSave.Text = "Change Salesperson while saving";
			checkBoxUpdateSalesPersonWhileSave.UseVisualStyleBackColor = true;
			checkBoxDisplayItemFeatures.AutoSize = true;
			checkBoxDisplayItemFeatures.Location = new System.Drawing.Point(17, 43);
			checkBoxDisplayItemFeatures.Name = "checkBoxDisplayItemFeatures";
			checkBoxDisplayItemFeatures.Size = new System.Drawing.Size(127, 17);
			checkBoxDisplayItemFeatures.TabIndex = 1;
			checkBoxDisplayItemFeatures.Text = "Display Item Features";
			checkBoxDisplayItemFeatures.UseVisualStyleBackColor = true;
			checkBoxPOSSufficientQuantity.AutoSize = true;
			checkBoxPOSSufficientQuantity.Location = new System.Drawing.Point(17, 17);
			checkBoxPOSSufficientQuantity.Name = "checkBoxPOSSufficientQuantity";
			checkBoxPOSSufficientQuantity.Size = new System.Drawing.Size(157, 17);
			checkBoxPOSSufficientQuantity.TabIndex = 0;
			checkBoxPOSSufficientQuantity.Text = "Check for sufficient quantity";
			checkBoxPOSSufficientQuantity.UseVisualStyleBackColor = true;
			ultraTabPageProject.Controls.Add(checkBoxActivateAutoService);
			ultraTabPageProject.Controls.Add(checkBoxAllowJobChange);
			ultraTabPageProject.Controls.Add(checkBoxCreateProjectwithSO);
			ultraTabPageProject.Controls.Add(checkBoxIssueGRNtoProject);
			ultraTabPageProject.Controls.Add(checkBoxUseProjectPhase);
			ultraTabPageProject.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageProject.Name = "ultraTabPageProject";
			ultraTabPageProject.Size = new System.Drawing.Size(807, 626);
			checkBoxActivateAutoService.AutoSize = true;
			checkBoxActivateAutoService.Location = new System.Drawing.Point(18, 109);
			checkBoxActivateAutoService.Name = "checkBoxActivateAutoService";
			checkBoxActivateAutoService.Size = new System.Drawing.Size(123, 17);
			checkBoxActivateAutoService.TabIndex = 5;
			checkBoxActivateAutoService.Text = "ActivateAutoService";
			checkBoxActivateAutoService.UseVisualStyleBackColor = true;
			checkBoxAllowJobChange.AutoSize = true;
			checkBoxAllowJobChange.Location = new System.Drawing.Point(18, 86);
			checkBoxAllowJobChange.Name = "checkBoxAllowJobChange";
			checkBoxAllowJobChange.Size = new System.Drawing.Size(214, 17);
			checkBoxAllowJobChange.TabIndex = 4;
			checkBoxAllowJobChange.Text = "Allow Job change in MR-PQ transaction";
			checkBoxAllowJobChange.UseVisualStyleBackColor = true;
			checkBoxCreateProjectwithSO.AutoSize = true;
			checkBoxCreateProjectwithSO.Location = new System.Drawing.Point(18, 63);
			checkBoxCreateProjectwithSO.Name = "checkBoxCreateProjectwithSO";
			checkBoxCreateProjectwithSO.Size = new System.Drawing.Size(133, 17);
			checkBoxCreateProjectwithSO.TabIndex = 3;
			checkBoxCreateProjectwithSO.Text = "Create Project with SO";
			checkBoxCreateProjectwithSO.UseVisualStyleBackColor = true;
			checkBoxIssueGRNtoProject.AutoSize = true;
			checkBoxIssueGRNtoProject.Location = new System.Drawing.Point(18, 40);
			checkBoxIssueGRNtoProject.Name = "checkBoxIssueGRNtoProject";
			checkBoxIssueGRNtoProject.Size = new System.Drawing.Size(126, 17);
			checkBoxIssueGRNtoProject.TabIndex = 2;
			checkBoxIssueGRNtoProject.Text = "Issue GRN to Project";
			checkBoxIssueGRNtoProject.UseVisualStyleBackColor = true;
			checkBoxUseProjectPhase.AutoSize = true;
			checkBoxUseProjectPhase.Location = new System.Drawing.Point(18, 17);
			checkBoxUseProjectPhase.Name = "checkBoxUseProjectPhase";
			checkBoxUseProjectPhase.Size = new System.Drawing.Size(133, 17);
			checkBoxUseProjectPhase.TabIndex = 1;
			checkBoxUseProjectPhase.Text = "Use phases in projects";
			checkBoxUseProjectPhase.UseVisualStyleBackColor = true;
			ultraTabPageControl6.Controls.Add(checkBoxAllowdofollowuponlead);
			ultraTabPageControl6.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl6.Name = "ultraTabPageControl6";
			ultraTabPageControl6.Size = new System.Drawing.Size(807, 626);
			checkBoxAllowdofollowuponlead.AutoSize = true;
			checkBoxAllowdofollowuponlead.Location = new System.Drawing.Point(15, 16);
			checkBoxAllowdofollowuponlead.Name = "checkBoxAllowdofollowuponlead";
			checkBoxAllowdofollowuponlead.Size = new System.Drawing.Size(170, 17);
			checkBoxAllowdofollowuponlead.TabIndex = 2;
			checkBoxAllowdofollowuponlead.Text = "Allow to Do Follow up on Lead";
			checkBoxAllowdofollowuponlead.UseVisualStyleBackColor = true;
			ultraTabPageControl7.Controls.Add(groupBox38);
			ultraTabPageControl7.Controls.Add(labelperc);
			ultraTabPageControl7.Controls.Add(textBoxTax);
			ultraTabPageControl7.Controls.Add(mmLabel29);
			ultraTabPageControl7.Controls.Add(mmLabel28);
			ultraTabPageControl7.Controls.Add(textBoxSMSMobileNo);
			ultraTabPageControl7.Controls.Add(textBoxSMSUserName);
			ultraTabPageControl7.Controls.Add(textBoxSMSPassword);
			ultraTabPageControl7.Controls.Add(mmLabel27);
			ultraTabPageControl7.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl7.Name = "ultraTabPageControl7";
			ultraTabPageControl7.Size = new System.Drawing.Size(807, 626);
			groupBox38.Controls.Add(panel8);
			groupBox38.Controls.Add(groupBox39);
			groupBox38.Location = new System.Drawing.Point(333, 35);
			groupBox38.Name = "groupBox38";
			groupBox38.Size = new System.Drawing.Size(372, 120);
			groupBox38.TabIndex = 138;
			groupBox38.TabStop = false;
			groupBox38.Text = "Default Tax Details";
			panel8.Controls.Add(checkBoxUnit);
			panel8.Controls.Add(checkBoxTenant);
			panel8.Controls.Add(checkBoxProperty);
			panel8.Controls.Add(checkBoxVendor);
			panel8.Controls.Add(ultraFormattedLinkLabelTaxGroup);
			panel8.Controls.Add(checkBoxProduct);
			panel8.Controls.Add(checkBoxCustomer);
			panel8.Controls.Add(comboBoxDefaultTaxOption);
			panel8.Controls.Add(comboBoxDefaultTaxGroup);
			panel8.Controls.Add(mmLabel63);
			panel8.Controls.Add(mmLabel61);
			panel8.Location = new System.Drawing.Point(7, 19);
			panel8.Name = "panel8";
			panel8.Size = new System.Drawing.Size(359, 95);
			panel8.TabIndex = 135;
			checkBoxUnit.AutoSize = true;
			checkBoxUnit.Location = new System.Drawing.Point(263, 26);
			checkBoxUnit.Name = "checkBoxUnit";
			checkBoxUnit.Size = new System.Drawing.Size(45, 17);
			checkBoxUnit.TabIndex = 147;
			checkBoxUnit.Text = "Unit";
			checkBoxUnit.UseVisualStyleBackColor = true;
			checkBoxTenant.AutoSize = true;
			checkBoxTenant.Location = new System.Drawing.Point(194, 26);
			checkBoxTenant.Name = "checkBoxTenant";
			checkBoxTenant.Size = new System.Drawing.Size(60, 17);
			checkBoxTenant.TabIndex = 145;
			checkBoxTenant.Text = "Tenant";
			checkBoxTenant.UseVisualStyleBackColor = true;
			checkBoxProperty.AutoSize = true;
			checkBoxProperty.Location = new System.Drawing.Point(111, 26);
			checkBoxProperty.Name = "checkBoxProperty";
			checkBoxProperty.Size = new System.Drawing.Size(65, 17);
			checkBoxProperty.TabIndex = 146;
			checkBoxProperty.Text = "Property";
			checkBoxProperty.UseVisualStyleBackColor = true;
			checkBoxVendor.AutoSize = true;
			checkBoxVendor.Location = new System.Drawing.Point(263, 5);
			checkBoxVendor.Name = "checkBoxVendor";
			checkBoxVendor.Size = new System.Drawing.Size(60, 17);
			checkBoxVendor.TabIndex = 144;
			checkBoxVendor.Text = "Vendor";
			checkBoxVendor.UseVisualStyleBackColor = true;
			ultraFormattedLinkLabelTaxGroup.AutoSize = true;
			appearance49.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelTaxGroup.LinkAppearance = appearance49;
			ultraFormattedLinkLabelTaxGroup.Location = new System.Drawing.Point(5, 72);
			ultraFormattedLinkLabelTaxGroup.Name = "ultraFormattedLinkLabelTaxGroup";
			ultraFormattedLinkLabelTaxGroup.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabelTaxGroup.TabIndex = 72;
			ultraFormattedLinkLabelTaxGroup.TabStop = true;
			ultraFormattedLinkLabelTaxGroup.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelTaxGroup.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelTaxGroup.Value = "Tax Group:";
			appearance50.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelTaxGroup.VisitedLinkAppearance = appearance50;
			checkBoxProduct.AutoSize = true;
			checkBoxProduct.Location = new System.Drawing.Point(194, 5);
			checkBoxProduct.Name = "checkBoxProduct";
			checkBoxProduct.Size = new System.Drawing.Size(63, 17);
			checkBoxProduct.TabIndex = 142;
			checkBoxProduct.Text = "Product";
			checkBoxProduct.UseVisualStyleBackColor = true;
			checkBoxCustomer.AutoSize = true;
			checkBoxCustomer.Location = new System.Drawing.Point(111, 5);
			checkBoxCustomer.Name = "checkBoxCustomer";
			checkBoxCustomer.Size = new System.Drawing.Size(70, 17);
			checkBoxCustomer.TabIndex = 143;
			checkBoxCustomer.Text = "Customer";
			checkBoxCustomer.UseVisualStyleBackColor = true;
			comboBoxDefaultTaxOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxDefaultTaxOption.ForeColor = System.Drawing.SystemColors.WindowText;
			comboBoxDefaultTaxOption.FormattingEnabled = true;
			comboBoxDefaultTaxOption.Items.AddRange(new object[2]
			{
				"Taxable",
				"Non Taxable"
			});
			comboBoxDefaultTaxOption.Location = new System.Drawing.Point(111, 47);
			comboBoxDefaultTaxOption.Name = "comboBoxDefaultTaxOption";
			comboBoxDefaultTaxOption.Size = new System.Drawing.Size(141, 21);
			comboBoxDefaultTaxOption.TabIndex = 141;
			comboBoxDefaultTaxOption.SelectedIndexChanged += new System.EventHandler(comboBoxDefaultTaxOption_SelectedIndexChanged);
			comboBoxDefaultTaxGroup.Assigned = false;
			comboBoxDefaultTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDefaultTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDefaultTaxGroup.CustomReportFieldName = "";
			comboBoxDefaultTaxGroup.CustomReportKey = "";
			comboBoxDefaultTaxGroup.CustomReportValueType = 1;
			comboBoxDefaultTaxGroup.DescriptionTextBox = null;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDefaultTaxGroup.DisplayLayout.Appearance = appearance51;
			comboBoxDefaultTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDefaultTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance52.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance52;
			appearance53.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance53;
			comboBoxDefaultTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance54.BackColor2 = System.Drawing.SystemColors.Control;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance54;
			comboBoxDefaultTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDefaultTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance55;
			appearance56.BackColor = System.Drawing.SystemColors.Highlight;
			appearance56.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance56;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance57;
			appearance58.BorderColor = System.Drawing.Color.Silver;
			appearance58.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.CellAppearance = appearance58;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance59.BackColor = System.Drawing.SystemColors.Control;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance59;
			appearance60.TextHAlignAsString = "Left";
			comboBoxDefaultTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance60;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.RowAppearance = appearance61;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDefaultTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance62;
			comboBoxDefaultTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDefaultTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDefaultTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDefaultTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDefaultTaxGroup.Editable = true;
			comboBoxDefaultTaxGroup.FilterString = "";
			comboBoxDefaultTaxGroup.HasAllAccount = false;
			comboBoxDefaultTaxGroup.HasCustom = false;
			comboBoxDefaultTaxGroup.IsDataLoaded = false;
			comboBoxDefaultTaxGroup.Location = new System.Drawing.Point(111, 69);
			comboBoxDefaultTaxGroup.MaxDropDownItems = 12;
			comboBoxDefaultTaxGroup.Name = "comboBoxDefaultTaxGroup";
			comboBoxDefaultTaxGroup.ReadOnly = true;
			comboBoxDefaultTaxGroup.ShowInactiveItems = false;
			comboBoxDefaultTaxGroup.ShowQuickAdd = true;
			comboBoxDefaultTaxGroup.Size = new System.Drawing.Size(141, 20);
			comboBoxDefaultTaxGroup.TabIndex = 1;
			comboBoxDefaultTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel63.AutoSize = true;
			mmLabel63.BackColor = System.Drawing.Color.Transparent;
			mmLabel63.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel63.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel63.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel63.IsFieldHeader = false;
			mmLabel63.IsRequired = false;
			mmLabel63.Location = new System.Drawing.Point(5, 50);
			mmLabel63.Name = "mmLabel63";
			mmLabel63.PenWidth = 1f;
			mmLabel63.ShowBorder = false;
			mmLabel63.Size = new System.Drawing.Size(64, 13);
			mmLabel63.TabIndex = 140;
			mmLabel63.Text = "Tax Option:";
			mmLabel63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel61.AutoSize = true;
			mmLabel61.BackColor = System.Drawing.Color.Transparent;
			mmLabel61.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel61.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel61.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel61.IsFieldHeader = false;
			mmLabel61.IsRequired = false;
			mmLabel61.Location = new System.Drawing.Point(5, 6);
			mmLabel61.Name = "mmLabel61";
			mmLabel61.PenWidth = 1f;
			mmLabel61.ShowBorder = false;
			mmLabel61.Size = new System.Drawing.Size(88, 13);
			mmLabel61.TabIndex = 12;
			mmLabel61.Text = "Tax Entity Types";
			mmLabel61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox39.Controls.Add(radioButton13);
			groupBox39.Controls.Add(radioButton14);
			groupBox39.Controls.Add(mmLabel62);
			groupBox39.Location = new System.Drawing.Point(0, 124);
			groupBox39.Name = "groupBox39";
			groupBox39.Size = new System.Drawing.Size(372, 89);
			groupBox39.TabIndex = 135;
			groupBox39.TabStop = false;
			groupBox39.Text = "PDC Maturity Process";
			radioButton13.AutoSize = true;
			radioButton13.Checked = true;
			radioButton13.Location = new System.Drawing.Point(13, 36);
			radioButton13.Name = "radioButton13";
			radioButton13.Size = new System.Drawing.Size(120, 17);
			radioButton13.TabIndex = 1;
			radioButton13.TabStop = true;
			radioButton13.Text = "Direct Maturity Entry";
			radioButton13.UseVisualStyleBackColor = true;
			radioButton14.AutoSize = true;
			radioButton14.Location = new System.Drawing.Point(13, 59);
			radioButton14.Name = "radioButton14";
			radioButton14.Size = new System.Drawing.Size(214, 17);
			radioButton14.TabIndex = 2;
			radioButton14.Text = "Send Cheques to Bank -> Maturity Entry";
			radioButton14.UseVisualStyleBackColor = true;
			mmLabel62.AutoSize = true;
			mmLabel62.BackColor = System.Drawing.Color.Transparent;
			mmLabel62.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel62.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel62.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel62.IsFieldHeader = false;
			mmLabel62.IsRequired = false;
			mmLabel62.Location = new System.Drawing.Point(10, 16);
			mmLabel62.Name = "mmLabel62";
			mmLabel62.PenWidth = 1f;
			mmLabel62.ShowBorder = false;
			mmLabel62.Size = new System.Drawing.Size(214, 13);
			mmLabel62.TabIndex = 0;
			mmLabel62.Text = "How do you want to process PDC maturity:";
			mmLabel62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			labelperc.AutoSize = true;
			labelperc.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelperc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelperc.IsFieldHeader = false;
			labelperc.IsRequired = false;
			labelperc.Location = new System.Drawing.Point(132, 121);
			labelperc.Name = "labelperc";
			labelperc.PenWidth = 1f;
			labelperc.ShowBorder = false;
			labelperc.Size = new System.Drawing.Size(15, 13);
			labelperc.TabIndex = 27;
			labelperc.Text = "%";
			labelperc.Visible = false;
			textBoxTax.AllowDecimal = true;
			textBoxTax.CustomReportFieldName = "";
			textBoxTax.CustomReportKey = "";
			textBoxTax.CustomReportValueType = 1;
			textBoxTax.IsComboTextBox = false;
			textBoxTax.IsModified = false;
			textBoxTax.Location = new System.Drawing.Point(96, 116);
			textBoxTax.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTax.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTax.Name = "textBoxTax";
			textBoxTax.NullText = "0";
			textBoxTax.Size = new System.Drawing.Size(29, 20);
			textBoxTax.TabIndex = 26;
			textBoxTax.Visible = false;
			mmLabel29.AutoSize = true;
			mmLabel29.BackColor = System.Drawing.Color.Transparent;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(7, 52);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(86, 13);
			mmLabel29.TabIndex = 25;
			mmLabel29.Text = "SMS User Name:";
			mmLabel29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel28.AutoSize = true;
			mmLabel28.BackColor = System.Drawing.Color.Transparent;
			mmLabel28.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel28.IsFieldHeader = false;
			mmLabel28.IsRequired = false;
			mmLabel28.Location = new System.Drawing.Point(7, 94);
			mmLabel28.Name = "mmLabel28";
			mmLabel28.PenWidth = 1f;
			mmLabel28.ShowBorder = false;
			mmLabel28.Size = new System.Drawing.Size(79, 13);
			mmLabel28.TabIndex = 24;
			mmLabel28.Text = "Reg Mobile No:";
			mmLabel28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			mmLabel28.Visible = false;
			textBoxSMSMobileNo.BackColor = System.Drawing.Color.White;
			textBoxSMSMobileNo.CustomReportFieldName = "";
			textBoxSMSMobileNo.CustomReportKey = "";
			textBoxSMSMobileNo.CustomReportValueType = 1;
			textBoxSMSMobileNo.IsComboTextBox = false;
			textBoxSMSMobileNo.IsModified = false;
			textBoxSMSMobileNo.IsRequired = true;
			textBoxSMSMobileNo.Location = new System.Drawing.Point(96, 90);
			textBoxSMSMobileNo.MaxLength = 20;
			textBoxSMSMobileNo.Name = "textBoxSMSMobileNo";
			textBoxSMSMobileNo.Size = new System.Drawing.Size(160, 20);
			textBoxSMSMobileNo.TabIndex = 23;
			textBoxSMSMobileNo.Visible = false;
			textBoxSMSUserName.BackColor = System.Drawing.Color.White;
			textBoxSMSUserName.CustomReportFieldName = "";
			textBoxSMSUserName.CustomReportKey = "";
			textBoxSMSUserName.CustomReportValueType = 1;
			textBoxSMSUserName.IsComboTextBox = false;
			textBoxSMSUserName.IsModified = false;
			textBoxSMSUserName.IsRequired = true;
			textBoxSMSUserName.Location = new System.Drawing.Point(96, 48);
			textBoxSMSUserName.MaxLength = 20;
			textBoxSMSUserName.Name = "textBoxSMSUserName";
			textBoxSMSUserName.Size = new System.Drawing.Size(160, 20);
			textBoxSMSUserName.TabIndex = 22;
			textBoxSMSPassword.BackColor = System.Drawing.Color.White;
			textBoxSMSPassword.CustomReportFieldName = "";
			textBoxSMSPassword.CustomReportKey = "";
			textBoxSMSPassword.CustomReportValueType = 1;
			textBoxSMSPassword.IsComboTextBox = false;
			textBoxSMSPassword.IsModified = false;
			textBoxSMSPassword.IsRequired = true;
			textBoxSMSPassword.Location = new System.Drawing.Point(96, 69);
			textBoxSMSPassword.MaxLength = 15;
			textBoxSMSPassword.Name = "textBoxSMSPassword";
			textBoxSMSPassword.Size = new System.Drawing.Size(160, 20);
			textBoxSMSPassword.TabIndex = 20;
			textBoxSMSPassword.UseSystemPasswordChar = true;
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(7, 73);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(80, 13);
			mmLabel27.TabIndex = 21;
			mmLabel27.Text = "SMS Password:";
			mmLabel27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ultraTabPageControl8.Controls.Add(groupBox33);
			ultraTabPageControl8.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl8.Name = "ultraTabPageControl8";
			ultraTabPageControl8.Size = new System.Drawing.Size(807, 626);
			groupBox33.Controls.Add(checkBoxLegalanalaysis);
			groupBox33.Controls.Add(panelLegalAnalysis);
			groupBox33.Controls.Add(groupBox34);
			groupBox33.Location = new System.Drawing.Point(13, 14);
			groupBox33.Name = "groupBox33";
			groupBox33.Size = new System.Drawing.Size(372, 83);
			groupBox33.TabIndex = 138;
			groupBox33.TabStop = false;
			checkBoxLegalanalaysis.AutoSize = true;
			checkBoxLegalanalaysis.Location = new System.Drawing.Point(11, 0);
			checkBoxLegalanalaysis.Name = "checkBoxLegalanalaysis";
			checkBoxLegalanalaysis.Size = new System.Drawing.Size(161, 17);
			checkBoxLegalanalaysis.TabIndex = 138;
			checkBoxLegalanalaysis.Text = "Track Expense with Analysis";
			checkBoxLegalanalaysis.UseVisualStyleBackColor = true;
			panelLegalAnalysis.Controls.Add(comboBoxLegalAnalysis);
			panelLegalAnalysis.Controls.Add(mmLabel50);
			panelLegalAnalysis.Controls.Add(textboxLegalanalysisPrefix);
			panelLegalAnalysis.Controls.Add(mmLabel51);
			panelLegalAnalysis.Location = new System.Drawing.Point(7, 19);
			panelLegalAnalysis.Name = "panelLegalAnalysis";
			panelLegalAnalysis.Size = new System.Drawing.Size(283, 53);
			panelLegalAnalysis.TabIndex = 135;
			comboBoxLegalAnalysis.Assigned = false;
			comboBoxLegalAnalysis.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLegalAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLegalAnalysis.CustomReportFieldName = "";
			comboBoxLegalAnalysis.CustomReportKey = "";
			comboBoxLegalAnalysis.CustomReportValueType = 1;
			comboBoxLegalAnalysis.DescriptionTextBox = null;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLegalAnalysis.DisplayLayout.Appearance = appearance63;
			comboBoxLegalAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLegalAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLegalAnalysis.DisplayLayout.GroupByBox.Appearance = appearance64;
			appearance65.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLegalAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance65;
			comboBoxLegalAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance66.BackColor2 = System.Drawing.SystemColors.Control;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLegalAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance66;
			comboBoxLegalAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLegalAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLegalAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance67;
			appearance68.BackColor = System.Drawing.SystemColors.Highlight;
			appearance68.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLegalAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance68;
			comboBoxLegalAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLegalAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLegalAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance69;
			appearance70.BorderColor = System.Drawing.Color.Silver;
			appearance70.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLegalAnalysis.DisplayLayout.Override.CellAppearance = appearance70;
			comboBoxLegalAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLegalAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance71.BackColor = System.Drawing.SystemColors.Control;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLegalAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance71;
			appearance72.TextHAlignAsString = "Left";
			comboBoxLegalAnalysis.DisplayLayout.Override.HeaderAppearance = appearance72;
			comboBoxLegalAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLegalAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			comboBoxLegalAnalysis.DisplayLayout.Override.RowAppearance = appearance73;
			comboBoxLegalAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLegalAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance74;
			comboBoxLegalAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLegalAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLegalAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLegalAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLegalAnalysis.Editable = true;
			comboBoxLegalAnalysis.FilterString = "";
			comboBoxLegalAnalysis.HasAllAccount = false;
			comboBoxLegalAnalysis.HasCustom = false;
			comboBoxLegalAnalysis.IsDataLoaded = false;
			comboBoxLegalAnalysis.Location = new System.Drawing.Point(91, 4);
			comboBoxLegalAnalysis.MaxDropDownItems = 12;
			comboBoxLegalAnalysis.Name = "comboBoxLegalAnalysis";
			comboBoxLegalAnalysis.ShowInactiveItems = false;
			comboBoxLegalAnalysis.ShowQuickAdd = true;
			comboBoxLegalAnalysis.Size = new System.Drawing.Size(114, 20);
			comboBoxLegalAnalysis.TabIndex = 140;
			comboBoxLegalAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel50.AutoSize = true;
			mmLabel50.BackColor = System.Drawing.Color.Transparent;
			mmLabel50.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel50.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel50.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel50.IsFieldHeader = false;
			mmLabel50.IsRequired = false;
			mmLabel50.Location = new System.Drawing.Point(6, 28);
			mmLabel50.Name = "mmLabel50";
			mmLabel50.PenWidth = 1f;
			mmLabel50.ShowBorder = false;
			mmLabel50.Size = new System.Drawing.Size(39, 13);
			mmLabel50.TabIndex = 139;
			mmLabel50.Text = "Prefix:";
			mmLabel50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			textboxLegalanalysisPrefix.BackColor = System.Drawing.Color.White;
			textboxLegalanalysisPrefix.CustomReportFieldName = "";
			textboxLegalanalysisPrefix.CustomReportKey = "";
			textboxLegalanalysisPrefix.CustomReportValueType = 1;
			textboxLegalanalysisPrefix.IsComboTextBox = false;
			textboxLegalanalysisPrefix.IsModified = false;
			textboxLegalanalysisPrefix.IsRequired = true;
			textboxLegalanalysisPrefix.Location = new System.Drawing.Point(91, 25);
			textboxLegalanalysisPrefix.MaxLength = 15;
			textboxLegalanalysisPrefix.Name = "textboxLegalanalysisPrefix";
			textboxLegalanalysisPrefix.Size = new System.Drawing.Size(114, 20);
			textboxLegalanalysisPrefix.TabIndex = 138;
			mmLabel51.AutoSize = true;
			mmLabel51.BackColor = System.Drawing.Color.Transparent;
			mmLabel51.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel51.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel51.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel51.IsFieldHeader = false;
			mmLabel51.IsRequired = false;
			mmLabel51.Location = new System.Drawing.Point(5, 6);
			mmLabel51.Name = "mmLabel51";
			mmLabel51.PenWidth = 1f;
			mmLabel51.ShowBorder = false;
			mmLabel51.Size = new System.Drawing.Size(82, 13);
			mmLabel51.TabIndex = 12;
			mmLabel51.Text = "Analysis Group:";
			mmLabel51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			groupBox34.Controls.Add(radioButton11);
			groupBox34.Controls.Add(radioButton12);
			groupBox34.Controls.Add(mmLabel52);
			groupBox34.Location = new System.Drawing.Point(0, 107);
			groupBox34.Name = "groupBox34";
			groupBox34.Size = new System.Drawing.Size(372, 89);
			groupBox34.TabIndex = 135;
			groupBox34.TabStop = false;
			groupBox34.Text = "PDC Maturity Process";
			radioButton11.AutoSize = true;
			radioButton11.Checked = true;
			radioButton11.Location = new System.Drawing.Point(13, 36);
			radioButton11.Name = "radioButton11";
			radioButton11.Size = new System.Drawing.Size(120, 17);
			radioButton11.TabIndex = 1;
			radioButton11.TabStop = true;
			radioButton11.Text = "Direct Maturity Entry";
			radioButton11.UseVisualStyleBackColor = true;
			radioButton12.AutoSize = true;
			radioButton12.Location = new System.Drawing.Point(13, 59);
			radioButton12.Name = "radioButton12";
			radioButton12.Size = new System.Drawing.Size(214, 17);
			radioButton12.TabIndex = 2;
			radioButton12.Text = "Send Cheques to Bank -> Maturity Entry";
			radioButton12.UseVisualStyleBackColor = true;
			mmLabel52.AutoSize = true;
			mmLabel52.BackColor = System.Drawing.Color.Transparent;
			mmLabel52.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel52.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel52.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel52.IsFieldHeader = false;
			mmLabel52.IsRequired = false;
			mmLabel52.Location = new System.Drawing.Point(10, 16);
			mmLabel52.Name = "mmLabel52";
			mmLabel52.PenWidth = 1f;
			mmLabel52.ShowBorder = false;
			mmLabel52.Size = new System.Drawing.Size(214, 13);
			mmLabel52.TabIndex = 0;
			mmLabel52.Text = "How do you want to process PDC maturity:";
			mmLabel52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 661);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(829, 40);
			panelButtons.TabIndex = 14;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(829, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(719, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Cancel";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(buttonClose_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(619, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&OK";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Controls.Add(ultraTabPageControl4);
			ultraTabControl1.Controls.Add(ultraTabPageControl5);
			ultraTabControl1.Controls.Add(tabPagePOS);
			ultraTabControl1.Controls.Add(ultraTabPageProject);
			ultraTabControl1.Controls.Add(ultraTabPageControl6);
			ultraTabControl1.Controls.Add(ultraTabPageControl7);
			ultraTabControl1.Controls.Add(ultraTabPageControl8);
			ultraTabControl1.Location = new System.Drawing.Point(8, 8);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(809, 647);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControl1.TabIndex = 0;
			appearance75.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance75;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&Company";
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "&Accounts";
			ultraTab3.TabPage = tabPageDetails;
			ultraTab3.Text = "C&ustomers && Sales";
			ultraTab4.TabPage = ultraTabPageControl5;
			ultraTab4.Text = "Receivables && &Collection";
			ultraTab5.TabPage = tabPageUserDefined;
			ultraTab5.Text = "&Vendors && Purchasing";
			ultraTab6.TabPage = ultraTabPageControl1;
			ultraTab6.Text = "&Inventory";
			ultraTab7.TabPage = ultraTabPageControl4;
			ultraTab7.Text = "&HR && Admin";
			ultraTab8.TabPage = ultraTabPageControl3;
			ultraTab8.Text = "&Reports";
			ultraTab9.TabPage = tabPagePOS;
			ultraTab9.Text = "PO&S";
			ultraTab10.TabPage = ultraTabPageProject;
			ultraTab10.Text = "&Project";
			ultraTab11.TabPage = ultraTabPageControl6;
			ultraTab11.Text = "CR&M";
			ultraTab12.TabPage = ultraTabPageControl7;
			ultraTab12.Text = "General";
			ultraTab13.TabPage = ultraTabPageControl8;
			ultraTab13.Text = "Legal";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[13]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
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
			ultraTabControl1.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(ultraTabControl1_SelectedTabChanged);
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(807, 626);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 15;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(829, 701);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(formManager);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(830, 515);
			base.Name = "CompanyOptionsForm";
			Text = "Company Preferences";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(AccountGroupDetailsForm_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			groupBox18.ResumeLayout(false);
			groupBox18.PerformLayout();
			groupBox13.ResumeLayout(false);
			groupBox13.PerformLayout();
			groupBox7.ResumeLayout(false);
			groupBox7.PerformLayout();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			groupBox37.ResumeLayout(false);
			groupBox37.PerformLayout();
			groupBox22.ResumeLayout(false);
			groupBox22.PerformLayout();
			groupBox14.ResumeLayout(false);
			groupBox14.PerformLayout();
			groupBox6.ResumeLayout(false);
			groupBox6.PerformLayout();
			tabPageDetails.ResumeLayout(false);
			panelPatientanalysis.ResumeLayout(false);
			panelPatientanalysis.PerformLayout();
			panel9.ResumeLayout(false);
			panel9.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPatientAnalysisGroup).EndInit();
			groupBox41.ResumeLayout(false);
			groupBox41.PerformLayout();
			groupBox12.ResumeLayout(false);
			groupBox10.ResumeLayout(false);
			groupBox10.PerformLayout();
			groupBox4.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			panelsalespricelessthancost.ResumeLayout(false);
			panelsalespricelessthancost.PerformLayout();
			panelNegativeQuantityPassword.ResumeLayout(false);
			panelNegativeQuantityPassword.PerformLayout();
			panelOverCLPassword.ResumeLayout(false);
			panelOverCLPassword.PerformLayout();
			panelMinPricePassword.ResumeLayout(false);
			panelMinPricePassword.PerformLayout();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ultraTabPageControl5.ResumeLayout(false);
			ultraTabPageControl5.PerformLayout();
			groupBox9.ResumeLayout(false);
			groupBox9.PerformLayout();
			panelMonth6.ResumeLayout(false);
			panelMonth6.PerformLayout();
			panelMonth5.ResumeLayout(false);
			panelMonth5.PerformLayout();
			panelMonth4.ResumeLayout(false);
			panelMonth4.PerformLayout();
			panelMonth3.ResumeLayout(false);
			panelMonth3.PerformLayout();
			panelMonth2.ResumeLayout(false);
			panelMonth2.PerformLayout();
			panelMonth1.ResumeLayout(false);
			panelMonth1.PerformLayout();
			panelCurrent.ResumeLayout(false);
			panelCurrent.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			tabPageUserDefined.PerformLayout();
			groupBox19.ResumeLayout(false);
			groupBox19.PerformLayout();
			panelPurchaseLandingCostMethod.ResumeLayout(false);
			panelPurchaseLandingCostMethod.PerformLayout();
			groupBox17.ResumeLayout(false);
			groupBox11.ResumeLayout(false);
			groupBox11.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraExpandableGroupBoxGridFields).EndInit();
			ultraExpandableGroupBoxGridFields.ResumeLayout(false);
			ultraExpandableGroupBoxPanel2.ResumeLayout(false);
			ultraExpandableGroupBoxPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)expandableGroupBoxTypesName).EndInit();
			expandableGroupBoxTypesName.ResumeLayout(false);
			ultraExpandableGroupBoxPanel1.ResumeLayout(false);
			ultraExpandableGroupBoxPanel1.PerformLayout();
			groupBox36.ResumeLayout(false);
			groupBox36.PerformLayout();
			groupBox35.ResumeLayout(false);
			groupBox35.PerformLayout();
			groupBox31.ResumeLayout(false);
			groupBox31.PerformLayout();
			panelVehicleanalysis.ResumeLayout(false);
			panelVehicleanalysis.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicleAnalysis).EndInit();
			groupBox32.ResumeLayout(false);
			groupBox32.PerformLayout();
			groupBoxLotDetails.ResumeLayout(false);
			groupBoxLotDetails.PerformLayout();
			groupBox21.ResumeLayout(false);
			groupBox21.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			panel6.ResumeLayout(false);
			panel6.PerformLayout();
			panel7.ResumeLayout(false);
			panel7.PerformLayout();
			groupBox16.ResumeLayout(false);
			groupBox5.ResumeLayout(false);
			groupBox5.PerformLayout();
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			groupBox8.ResumeLayout(false);
			groupBox8.PerformLayout();
			groupBox20.ResumeLayout(false);
			grpCostSetting.ResumeLayout(false);
			ultraTabPageControl4.ResumeLayout(false);
			ultraTabPageControl4.PerformLayout();
			groupBox29.ResumeLayout(false);
			groupBox29.PerformLayout();
			panelHRanalysis.ResumeLayout(false);
			panelHRanalysis.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxHRAnalysis).EndInit();
			groupBox30.ResumeLayout(false);
			groupBox30.PerformLayout();
			groupBox27.ResumeLayout(false);
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			groupBox28.ResumeLayout(false);
			groupBox28.PerformLayout();
			groupBox25.ResumeLayout(false);
			groupBox25.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			groupBox26.ResumeLayout(false);
			groupBox26.PerformLayout();
			groupBox23.ResumeLayout(false);
			groupBox23.PerformLayout();
			groupBox24.ResumeLayout(false);
			groupBox24.PerformLayout();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).EndInit();
			groupBox15.ResumeLayout(false);
			groupBox15.PerformLayout();
			tabPagePOS.ResumeLayout(false);
			tabPagePOS.PerformLayout();
			ultraTabPageProject.ResumeLayout(false);
			ultraTabPageProject.PerformLayout();
			ultraTabPageControl6.ResumeLayout(false);
			ultraTabPageControl6.PerformLayout();
			ultraTabPageControl7.ResumeLayout(false);
			ultraTabPageControl7.PerformLayout();
			groupBox38.ResumeLayout(false);
			panel8.ResumeLayout(false);
			panel8.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultTaxGroup).EndInit();
			groupBox39.ResumeLayout(false);
			groupBox39.PerformLayout();
			ultraTabPageControl8.ResumeLayout(false);
			groupBox33.ResumeLayout(false);
			groupBox33.PerformLayout();
			panelLegalAnalysis.ResumeLayout(false);
			panelLegalAnalysis.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLegalAnalysis).EndInit();
			groupBox34.ResumeLayout(false);
			groupBox34.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
