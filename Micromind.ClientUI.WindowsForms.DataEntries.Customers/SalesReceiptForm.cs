using DevExpress.XtraEditors;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinToolTip;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.ClientUI.WindowsForms.DataEntries.Inventory;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class SalesReceiptForm : Form, IForm
	{
		public class translist
		{
			private string itemcode = "";

			private string itemdescription = "";

			private decimal trqty;

			private bool istaken;

			private int trindex;

			public string ItemCode
			{
				get
				{
					return itemcode;
				}
				set
				{
					itemcode = value;
				}
			}

			public string ItemDescription
			{
				get
				{
					return itemdescription;
				}
				set
				{
					itemdescription = value;
				}
			}

			public decimal Trqty
			{
				get
				{
					return trqty;
				}
				set
				{
					trqty = value;
				}
			}

			public bool Istaken
			{
				get
				{
					return istaken;
				}
				set
				{
					istaken = value;
				}
			}

			public int Trindex
			{
				get
				{
					return trindex;
				}
				set
				{
					trindex = value;
				}
			}

			public translist(string Dtstart, decimal Trqty, bool Istaken, string ItemDescription, int Trindex)
			{
				itemcode = Dtstart;
				trqty = Trqty;
				istaken = Istaken;
				itemdescription = ItemDescription;
				trindex = Trindex;
			}
		}

		private bool mergeMatrixPrint;

		private bool allowEdit = true;

		private bool allowNegativeQty = true;

		private bool allowMultiTemplate;

		private SalesInvoiceData currentData;

		private const string TABLENAME_CONST = "Sales_Invoice";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentCustomerAddressID = "";

		private DataSet priceListData;

		private DataSet timeStampStatus;

		private bool loadItemDescFromPriceList = CompanyPreferences.LoadItemDescFromPriceList;

		private bool useInlineDiscount = CompanyPreferences.UseInlineDiscount;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool showLotDetail = CompanyPreferences.ShowLotdetailinPrintout;

		private bool setlastSalesprice = CompanyPreferences.SetlastSalesprice;

		private bool allowzeroprice = CompanyPreferences.Allowzeropriceinsale;

		private decimal taxPercent = CompanyPreferences.TaxPercent;

		private bool loadItemFeatures = CompanyPreferences.LoadItemFeatures;

		private bool activatePartsDetails = CompanyPreferences.ActivatePartsDetails;

		private bool showLoadDraft = CompanyPreferences.EnableDocTempSaving;

		private bool setCustomerID;

		private bool setRegisterID;

		private string strLocation = "";

		private string currentCustomerID;

		private bool hideRegister;

		private bool allowEditTransaction;

		private bool allowEditTransDiffLocation;

		private decimal priceDiscountPercAllowed;

		private decimal totalDiscountPercAllowed;

		private string refDocID = "";

		private string refVoucherID = "";

		private DateTime refDateTime = DateTime.MinValue;

		private DateTime lastUpdateDateTime = DateTime.MinValue;

		private DataSet timestampStatus;

		private ItemSourceTypes sourceDocType;

		private bool isChecked;

		private int tempAutoKeyID;

		private bool restrictTransaction;

		private bool supressInventoryMessage;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool isVoid;

		private bool isDiscountPercent;

		private bool showednotdiscount;

		private bool totalChanged;

		private decimal _amount;

		private bool isTaxPercent;

		private DataTable dtTransaction = new DataTable();

		private DataTable dtPO = new DataTable();

		private bool IsTempRecordexists;

		private bool IsDocExists;

		private bool IsTemporaryTransaction;

		public string currentkey = "";

		private IContainer components;

		private ToolStrip toolStrip1;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private DataEntryGrid dataGridItems;

		private DateTimePicker dateTimePickerDate;

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private customersFlatComboBox comboBoxCustomer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TextBox textBoxCustomerName;

		private Label label2;

		private TextBox textBoxPONumber;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private AmountTextBox textBoxSubtotal;

		private Panel panel1;

		private Label label5;

		private Label label8;

		private AmountTextBox textBoxTotal;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private SalespersonComboBox comboBoxSalesperson;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private ShippingMethodsComboBox comboBoxShippingMethod;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private LocationComboBox comboBoxGridLocation;

		private Label label11;

		private CurrencySelector comboBoxCurrency;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private RegisterComboBox comboBoxRegister;

		private PaymentMethodTypesComboBox comboBoxPaymentMethod;

		private Panel panelNonTax;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonInformation;

		private Label label4;

		private TextBox textBoxRef2;

		private ToolStripButton toolStripButtonPrint;

		private CostCategoryComboBox comboBoxCostCategory;

		private JobComboBox comboBoxJob;

		private MMTextBox textBoxShipto;

		private CustomerAddressComboBox comboBoxBillingAddress;

		private CustomerAddressComboBox comboBoxShippingAddressID;

		private MMTextBox textBoxBilltoAddress;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonMultiPreview;

		private UltraFormattedLinkLabel labelCurrency;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelShipment;

		private UltraFormattedLinkLabel labelJob;

		private UltraFormattedLinkLabel labelcostcategory;

		private UltraToolTipManager ultraToolTipManager1;

		private ToolStripMenuItem createFromTransactionToolStripMenuItem;

		private Label label7;

		private PercentTextBox textBoxDiscountPercent;

		private AmountTextBox textBoxDiscountAmount;

		private AmountTextBox textBoxTaxAmount;

		private ProductStyleComboBox comboBoxStyle;

		private ProductSpecificationComboBox comboBoxSpecification;

		private Panel panelxml;

		private SimpleButton simpleButtonFind;

		private SimpleButton simpleButtonLast;

		private SimpleButton simpleButtonNext;

		private SimpleButton simpleButtonPrevious;

		private SimpleButton simpleButtonFirst;

		private SimpleButton simpleButtonSave;

		private ProductComboBox comboBoxGridItem;

		private ToolStripMenuItem allowUpdateToolStripMenuItem;

		private UltraPanel panelExpense;

		private UltraFormattedLinkLabel labelExpenseCode;

		private ExpenseCodeComboBox comboBoxExpenseCode;

		private PercentTextBox textboxPercent;

		private Label labelPercent;

		private Label labelExpAmount;

		private AmountTextBox textBoxExpAmount;

		private ToolStripMenuItem createFromProformaInvoiceToolStripMenuItem;

		private Label labelSelectedDocs;

		private ListBox checkedListBoxPI;

		private UltraFormattedLinkLabel labelTaxGroup;

		private TaxGroupComboBox comboBoxPayeeTaxGroup;

		private Label label10;

		private AmountTextBox textBoxRoundOff;

		private ToolStripMenuItem deleteTempToolStripMenuItem;

		private UltraFormattedLinkLabel linkLabelTax;

		private ToolStripSeparator toolStripSeparator6;

		private CheckBox checkBoxPriceIncludeTax;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private ToolStripMenuItem createFromSalesQuoteToolStripMenuItem;

		private Label labelReportTo;

		private Label label6;

		private TextBox textBoxVechicleName;

		private VehicleComboBox comboBoxVehicle;

		private DriverComboBox comboBoxDriver;

		private UltraFormattedLinkLabel linkLabelDriver;

		private UltraFormattedLinkLabel linkLabelVehicle;

		private ToolStripMenuItem createFromSalesOrderToolStripMenuItem;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2013;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		public string CurrentCustomerID
		{
			get
			{
				return currentCustomerID;
			}
			set
			{
				currentCustomerID = value;
			}
		}

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		private bool IsDirty
		{
			get
			{
				if (IsVoid)
				{
					return false;
				}
				return formManager.GetDirtyStatus();
			}
		}

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
				bool flag5;
				bool enabled;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
					CurrencySelector currencySelector = comboBoxCurrency;
					customersFlatComboBox customersFlatComboBox = comboBoxCustomer;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					bool flag3 = textBoxVoucherNumber.Enabled = true;
					flag5 = (sysDocComboBox.Enabled = flag3);
					enabled = (customersFlatComboBox.Enabled = flag5);
					currencySelector.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					if (!IsVoid)
					{
						buttonVoid.Enabled = true;
					}
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = false);
					sysDocComboBox2.Enabled = enabled;
				}
				toolStripButtonDistribution.Enabled = !value;
				toolStripButtonInformation.Enabled = !value;
				toolStripButtonAttach.Enabled = !value;
				ToolStripButton toolStripButton = toolStripButtonPrint;
				ToolStripButton toolStripButton2 = toolStripButtonPreview;
				flag5 = (toolStripButtonMultiPreview.Enabled = !isNewRecord);
				enabled = (toolStripButton2.Enabled = flag5);
				toolStripButton.Enabled = enabled;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
					allowUpdateToolStripMenuItem.Visible = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
					deleteTempToolStripMenuItem.Visible = false;
				}
				if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.BlockPrint))
				{
					ToolStripButton toolStripButton3 = toolStripButtonPrint;
					ToolStripButton toolStripButton4 = toolStripButtonPreview;
					flag5 = (toolStripButtonMultiPreview.Enabled = !Security.IsAllowedSecurityRole(GeneralSecurityRoles.BlockPrint));
					enabled = (toolStripButton4.Enabled = flag5);
					toolStripButton3.Enabled = enabled;
				}
				else
				{
					ToolStripButton toolStripButton5 = toolStripButtonPrint;
					ToolStripButton toolStripButton6 = toolStripButtonPreview;
					flag5 = (toolStripButtonMultiPreview.Enabled = true);
					enabled = (toolStripButton6.Enabled = flag5);
					toolStripButton5.Enabled = enabled;
				}
				if (Global.isUserAdmin)
				{
					ToolStripButton toolStripButton7 = toolStripButtonPrint;
					ToolStripButton toolStripButton8 = toolStripButtonPreview;
					flag5 = (toolStripButtonMultiPreview.Enabled = true);
					enabled = (toolStripButton8.Enabled = flag5);
					toolStripButton7.Enabled = enabled;
				}
			}
		}

		private bool IsVoid
		{
			get
			{
				return isVoid;
			}
			set
			{
				if (isVoid == value)
				{
					return;
				}
				isVoid = value;
				panelDetails.Enabled = !value;
				dataGridItems.Enabled = !value;
				buttonSave.Enabled = !value;
				panel1.Enabled = !value;
				textBoxNote.Enabled = !value;
				labelVoided.Visible = value;
				if (value)
				{
					buttonVoid.Enabled = false;
					return;
				}
				buttonVoid.Text = UIMessages.Void;
				if (!IsNewRecord)
				{
					buttonVoid.Enabled = true;
				}
				else
				{
					buttonVoid.Enabled = false;
				}
			}
		}

		public decimal Amount
		{
			private get
			{
				return _amount;
			}
			set
			{
				_amount = value;
			}
		}

		public string CurrentKey
		{
			get
			{
				return currentkey;
			}
			set
			{
				currentkey = value;
			}
		}

		public int TempAutoKeyID
		{
			get
			{
				return tempAutoKeyID;
			}
			set
			{
				tempAutoKeyID = value;
			}
		}

		public SalesReceiptForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			allowNegativeQty = CompanyPreferences.AllowSalesInvoiceNegativeQty;
			labelJob.Visible = (labelcostcategory.Visible = (comboBoxJob.Visible = (comboBoxCostCategory.Visible = useJobCosting)));
			comboBoxPaymentMethod.HideARAccount = CompanyPreferences.AllowCreditSaleinSalesreceipt;
			panelxml.Visible = showLoadDraft;
			panelExpense.Visible = false;
		}

		private void AddEvents()
		{
			base.Load += Form_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			productPhotoViewer.EnlargeRequested += productPhotoViewer_EnlargeRequested;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			textBoxDiscountAmount.Validating += textBoxDiscountAmount_Validating;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			base.FormClosing += ItemGroupDetailsForm_FormClosing;
			comboBoxBillingAddress.SelectedIndexChanged += comboBoxBillingAddress_SelectedIndexChanged;
			comboBoxShippingAddressID.SelectedIndexChanged += comboBoxShippingAddressID_SelectedIndexChanged;
			base.KeyDown += SalesInvoiceForm_KeyDown;
			textBoxDiscountAmount.Validated += textBoxDiscountAmount_Validated;
			textBoxDiscountPercent.Validated += textBoxDiscountPercent_Validated;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			dataGridItems.KeyPress += dataGridItems_KeyDown;
			comboBoxPaymentMethod.SelectionChanged += comboBoxPaymentMethod_SelectionChanged;
			comboBoxPayeeTaxGroup.SelectedIndexChanged += comboBoxPayeeTaxGroup_SelectedIndexChanged;
		}

		private void comboBoxPaymentMethod_SelectionChanged(object sender, EventArgs e)
		{
			if (comboBoxPaymentMethod.SelectedType == PaymentMethodTypes.AccountReceivable)
			{
				hideRegister = true;
				comboBoxRegister.Enabled = false;
			}
			else
			{
				hideRegister = false;
				comboBoxRegister.Enabled = true;
			}
		}

		private void comboBoxShippingAddressID_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string selectedID = comboBoxShippingAddressID.SelectedID;
				string selectedID2 = comboBoxCustomer.SelectedID;
				if (selectedID == "" || selectedID2 == "")
				{
					textBoxShipto.Clear();
				}
				else
				{
					textBoxShipto.Text = Factory.CustomerSystem.GetCustomerAddressPrintFormat(selectedID2, selectedID);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxBillingAddress_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string selectedID = comboBoxBillingAddress.SelectedID;
				string selectedID2 = comboBoxCustomer.SelectedID;
				if (selectedID == "" || selectedID2 == "")
				{
					textBoxBilltoAddress.Clear();
				}
				else
				{
					textBoxBilltoAddress.Text = Factory.CustomerSystem.GetCustomerAddressPrintFormat(selectedID2, selectedID);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity")
			{
				AllocateQuantityToLot(e.Cell);
			}
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Text.ToString(), out result);
				if (result < 0m)
				{
					ErrorHelper.InformationMessage("Negative quantity is not allowed.", "Please enter a number greater than or equal to zero.");
					e.Cancel = true;
				}
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
			{
				decimal result2 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Text.ToString(), out result2);
				int result3 = 1;
				int.TryParse(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString(), out result3);
				ItemTypes itemTypes = (ItemTypes)checked((byte)result3);
				if (result2 < 0m && itemTypes != ItemTypes.Discount)
				{
					ErrorHelper.InformationMessage("Negative amount is not allowed. Please enter a number greater than or equal to zero.");
					e.Cancel = true;
				}
			}
		}

		private void textBoxDiscountPercent_Validated(object sender, EventArgs e)
		{
			if (textBoxDiscountPercent.Modified)
			{
				textBoxDiscountPercent.Modified = false;
				CalculateAllRowsTaxes();
			}
			CalculateTotal();
		}

		private void textBoxDiscountAmount_Validated(object sender, EventArgs e)
		{
			if (textBoxDiscountAmount.Modified)
			{
				textBoxDiscountAmount.Modified = false;
				CalculateAllRowsTaxes();
			}
			CalculateTotal();
		}

		private void dataGridItems_KeyDown(object sender, KeyPressEventArgs e)
		{
			if (dataGridItems.ActiveCell == null)
			{
				return;
			}
			UltraGridColumn column = dataGridItems.ActiveCell.Column;
			if (dataGridItems.ActiveRow == null || column == null || !(column.Key == "Price") || !char.IsLetter(e.KeyChar))
			{
				return;
			}
			ProductPriceListForm productPriceListForm = new ProductPriceListForm();
			productPriceListForm.LoadData(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
			if (productPriceListForm.ShowDialog(this) == DialogResult.OK)
			{
				e.Handled = true;
				string text = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
				string text2 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				if (text2 != "" && text != "")
				{
					DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text2, text, comboBoxSysDoc.LocationID);
					if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
					{
						DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
						string a = dataRow["FactorType"].ToString();
						decimal num = decimal.Parse(dataRow["Factor"].ToString());
						decimal num2 = default(decimal);
						num2 = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
						if (a == "M")
						{
							num2 /= num;
						}
						else
						{
							num2 *= num;
						}
						dataGridItems.ActiveRow.Cells["Price"].Value = num2;
					}
					else
					{
						dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
					}
				}
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result2);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
				result3 = Math.Round(result * result2, Global.CurDecimalPoints);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
			}
			else
			{
				e.Handled = true;
			}
		}

		private void SalesInvoiceForm_KeyDown(object sender, KeyEventArgs e)
		{
			if ((!Security.IsAllowedSecurityRole(GeneralSecurityRoles.BlockPrint) || Global.isUserAdmin) && e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
			if (e.KeyCode == Keys.G && e.Alt)
			{
				dataGridItems.Focus();
				comboBoxGridItem.Focus();
			}
			if (e.KeyCode == Keys.F6 && dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				ProductQuantityForm productQuantityForm = new ProductQuantityForm();
				productQuantityForm.LoadData(productID);
				productQuantityForm.ShowDialog();
			}
			if (loadItemFeatures && e.KeyCode == Keys.F3 && !activatePartsDetails)
			{
				ComboSearchDialogNew comboSearchDialogNew = new ComboSearchDialogNew();
				comboSearchDialogNew.IsMultiSelect = false;
				_ = dataGridItems.ActiveRow;
				DataSet dataSet = new DataSet();
				dataSet = (comboSearchDialogNew.DataSource = Factory.ProductSystem.GetProducts());
				if (dataGridItems.ActiveCell == null)
				{
					comboSearchDialogNew.SelectedItem = "";
				}
				else
				{
					comboBoxGridItem.LoadData();
					comboBoxGridItem.SelectedID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					comboSearchDialogNew.SelectedItem = comboBoxGridItem.SelectedID;
				}
				if (comboBoxCustomer.SelectedID != "" && comboBoxCustomer.SelectedID != null)
				{
					comboSearchDialogNew.SelectedProvider = comboBoxCustomer.SelectedID;
				}
				comboSearchDialogNew.ShowDialog();
				_ = 1;
			}
		}

		private void SalesInvoiceForm_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			try
			{
				UltraGridRow activeRow = dataGridItems.ActiveRow;
				bool flag;
				decimal result2;
				decimal num2;
				if (activeRow != null && activeRow.Cells["Item Code"].Value != null && !(activeRow.Cells["Item Code"].Value.ToString() == ""))
				{
					if (activeRow != null && activeRow.DataChanged && Factory.ProductSystem.IsHoldSaleonProduct(activeRow.Cells["Item Code"].Value.ToString()))
					{
						ErrorHelper.WarningMessage("Sale Hold for product-" + activeRow.Cells["Item Code"].Value.ToString() + ".");
						activeRow.CancelUpdate();
						e.Cancel = true;
					}
					else
					{
						flag = false;
						decimal result = default(decimal);
						result2 = default(decimal);
						if (activeRow == null || !activeRow.DataChanged)
						{
							goto IL_04da;
						}
						decimal.TryParse(activeRow.Cells["Price"].Value.ToString(), out result2);
						decimal num = default(decimal);
						num2 = default(decimal);
						string text = "MinPrice";
						text = (Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice) ? "MinPrice" : "UnitPrice3");
						decimal.TryParse(Factory.DatabaseSystem.GetFieldValue("Product", text, "ProductID", activeRow.Cells["Item Code"].Value.ToString()).ToString(), out result);
						object fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_PriceList_Detail", text, "ProductID", activeRow.Cells["Item Code"].Value.ToString(), "UnitID", activeRow.Cells["Unit"].Value.ToString(), "LocationID", comboBoxSysDoc.LocationID);
						if (fieldValue == null)
						{
							fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_PriceList_Detail", text, "ProductID", activeRow.Cells["Item Code"].Value.ToString(), "UnitID", activeRow.Cells["Unit"].Value.ToString(), "LocationID", "");
						}
						if (fieldValue != null)
						{
							decimal.TryParse(fieldValue.ToString(), out result);
						}
						string text2 = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
						string text3 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
						dataGridItems.ActiveRow.Cells["Location"].Value.ToString();
						if (text3 != "" && text2 != "")
						{
							DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text3, text2, comboBoxSysDoc.LocationID);
							if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
							{
								DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
								string a = dataRow["FactorType"].ToString();
								decimal num3 = decimal.Parse(dataRow["Factor"].ToString());
								if (a == "M")
								{
									result /= num3;
								}
								else
								{
									result *= num3;
								}
							}
						}
						num = result * (priceDiscountPercAllowed / 100m);
						num2 = result - num;
						if (!(result2 < num2))
						{
							goto IL_04a7;
						}
						flag = true;
						if (!(priceDiscountPercAllowed > 0m))
						{
							goto IL_04a7;
						}
						ErrorHelper.WarningMessage("More discount not allowed for the  product-" + activeRow.Cells["Item Code"].Value.ToString() + ".");
						e.Row.Cells["Price"].Value = 0;
						e.Row.Cells["Amount"].Value = 0;
						e.Row.Cells["TaxTotal"].Value = 0;
						if (!e.Row.IsAddRow)
						{
							e.Cancel = true;
						}
					}
				}
				goto end_IL_0000;
				IL_04da:
				if (activeRow == null || !activeRow.DataChanged || CompanyPreferences.MinPriceSaleAction == 1)
				{
					goto IL_0736;
				}
				bool flag2 = Factory.SalesInvoiceSystem.IsBelowMinPrice(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(activeRow.Cells["Price"].Value.ToString()), comboBoxSysDoc.LocationID);
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
				{
					flag2 = Factory.SalesInvoiceSystem.IsBelowMinAllowedPrice(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(activeRow.Cells["Price"].Value.ToString()));
				}
				if (!flag2)
				{
					goto IL_0736;
				}
				if (CompanyPreferences.MinPriceSaleAction == 2)
				{
					if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the minimum price. Do you want to continue?") == DialogResult.Yes)
					{
						goto IL_0736;
					}
					if (!e.Row.IsAddRow)
					{
						e.Cancel = true;
					}
				}
				else if (CompanyPreferences.MinPriceSaleAction == 3 && flag)
				{
					ErrorHelper.WarningMessage("The price you have entered is below the minimum price. You are not allowed to sell at this price.");
					if (!e.Row.IsAddRow)
					{
						e.Cancel = true;
					}
					if (flag)
					{
						activeRow.Cells["price"].Value = 0;
						activeRow.Cells["Amount"].Value = 0;
						activeRow.Cells["TaxTotal"].Value = 0;
					}
				}
				else
				{
					if (CompanyPreferences.MinPriceSaleAction != 4)
					{
						goto IL_0736;
					}
					if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the minimum price. You need to enter password to continue. \nDo you want to continue?") == DialogResult.Yes)
					{
						if (new ApprovalPasswordForm
						{
							CorrectPassword = CompanyPreferences.MinPriceSalePassword
						}.ShowDialog() == DialogResult.OK)
						{
							goto IL_0736;
						}
						if (!e.Row.IsAddRow)
						{
							e.Cancel = true;
						}
					}
					else if (!e.Row.IsAddRow)
					{
						e.Cancel = true;
					}
				}
				goto end_IL_0000;
				IL_04a7:
				if (Global.IsUserAdmin || priceDiscountPercAllowed == 0m)
				{
					flag = false;
				}
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowPriceDiscount) && result2 < num2)
				{
					flag = true;
				}
				goto IL_04da;
				IL_08f2:
				activeRow.Cells["Price"].Appearance.BackColor = Color.Transparent;
				goto end_IL_0000;
				IL_0736:
				if (CompanyPreferences.PricelessCostAction == 1 || !Factory.SalesInvoiceSystem.IsBelowAverageCost(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(activeRow.Cells["Price"].Value.ToString())))
				{
					goto IL_08f2;
				}
				if (CompanyPreferences.PricelessCostAction == 2)
				{
					if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the cost. Do you want to continue?") == DialogResult.Yes)
					{
						goto IL_08f2;
					}
					if (!e.Row.IsAddRow)
					{
						e.Cancel = true;
					}
				}
				else if (CompanyPreferences.PricelessCostAction == 3)
				{
					ErrorHelper.WarningMessage("The price you have entered is below the cost. You are not allowed to sell at this price.");
					if (!e.Row.IsAddRow)
					{
						e.Cancel = true;
					}
				}
				else
				{
					if (CompanyPreferences.PricelessCostAction != 4)
					{
						goto IL_08f2;
					}
					if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the cost. You need to enter password to continue. \nDo you want to continue?") == DialogResult.Yes)
					{
						if (new ApprovalPasswordForm
						{
							CorrectPassword = CompanyPreferences.PricelessCostPassword
						}.ShowDialog() == DialogResult.OK)
						{
							goto IL_08f2;
						}
						if (!e.Row.IsAddRow)
						{
							e.Cancel = true;
						}
					}
					else
					{
						if (!e.Row.IsAddRow)
						{
							e.Cancel = true;
						}
						if (e.Row.IsAddRow)
						{
							activeRow.Cells["price"].Value = 0;
							activeRow.Cells["Amount"].Value = 0;
							activeRow.Cells["TaxTotal"].Value = 0;
						}
					}
				}
				end_IL_0000:;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridItems_GotFocus(object sender, EventArgs e)
		{
			if (!dataGridItems.Focused)
			{
				dataGridItems.DisplayLayout.Bands[0].AddNew();
			}
		}

		private void textBoxDiscountAmount_Validating(object sender, CancelEventArgs e)
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			num = decimal.Parse(textBoxDiscountAmount.Text, NumberStyles.Any);
			num2 = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num3 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			if (!(num == 0m))
			{
				if (num > num2 + num3)
				{
					ErrorHelper.InformationMessage("Discount amount should be less or equal to the subtotal.");
					e.Cancel = true;
				}
				else if (num < 0m)
				{
					ErrorHelper.InformationMessage("Discount amount should be greater than or equal to zero.");
					e.Cancel = true;
				}
			}
		}

		private void productPhotoViewer_EnlargeRequested(object sender, EventArgs e)
		{
			FormActivator.ImageViewerFormObj.Image = productPhotoViewer.Image;
			FormActivator.BringFormToFront(FormActivator.ImageViewerFormObj);
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridItems.ActiveRow == null || ultraGridColumn == null)
			{
				return;
			}
			if (ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip1.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
			else if (ultraGridColumn.Key == "Price")
			{
				ProductPriceListForm productPriceListForm = new ProductPriceListForm();
				productPriceListForm.LoadData(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
				if (productPriceListForm.ShowDialog(this) == DialogResult.OK)
				{
					string text = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
					string text2 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					if (text2 != "" && text != "")
					{
						DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text2, text, comboBoxSysDoc.LocationID);
						if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
						{
							DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
							string a = dataRow["FactorType"].ToString();
							decimal num = decimal.Parse(dataRow["Factor"].ToString());
							decimal num2 = default(decimal);
							num2 = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
							if (a == "M")
							{
								num2 /= num;
							}
							else
							{
								num2 *= num;
							}
							dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(num2, Global.CurDecimalPoints);
						}
						else
						{
							dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
						}
					}
				}
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result2);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
				result3 = Math.Round(result * result2, 2);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
			}
			else if (ultraGridColumn.Key == "Tax" && dataGridItems.ActiveRow != null)
			{
				TaxTransactionData taxData = new TaxTransactionData();
				if (dataGridItems.ActiveRow.Cells["Tax"].Tag != null)
				{
					taxData = (dataGridItems.ActiveRow.Cells["Tax"].Tag as TaxTransactionData);
				}
				TaxDistibutionDialog taxDistibutionDialog = new TaxDistibutionDialog();
				taxDistibutionDialog.TaxData = taxData;
				taxDistibutionDialog.ShowDialog();
			}
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxCustomerName.Text = comboBoxCustomer.SelectedName;
			CurrentCustomerID = comboBoxCustomer.SelectedID;
			LoadCustomerPriceList();
			comboBoxBillingAddress.Clear();
			comboBoxShippingAddressID.Clear();
			try
			{
				if (!isDataLoading && comboBoxCustomer.SelectedID != "")
				{
					string text = Factory.DatabaseSystem.GetFieldValue("Customer", "CurrencyID", "CustomerID", comboBoxCustomer.SelectedID).ToString();
					if (text != "")
					{
						comboBoxCurrency.SelectedID = text;
						comboBoxCurrency.GetLastRate();
					}
					comboBoxSalesperson.SelectedID = comboBoxCustomer.DefaultSalesPersonID;
					if (comboBoxSalesperson.SelectedID == "")
					{
						comboBoxSalesperson.SelectedID = Security.DefaultSalespersonID;
					}
					comboBoxShippingMethod.SelectedID = comboBoxCustomer.DefaultShippingMethod;
					string defaultShipToAddress = comboBoxCustomer.DefaultShipToAddress;
					comboBoxShippingAddressID.IsDataLoaded = true;
					comboBoxShippingAddressID.Filter(comboBoxCustomer.SelectedID);
					if (defaultShipToAddress == "")
					{
						comboBoxShippingAddressID.SelectedID = "PRIMARY";
					}
					else
					{
						comboBoxShippingAddressID.SelectedID = defaultShipToAddress;
					}
					defaultShipToAddress = comboBoxCustomer.DefaultBillToAddress;
					comboBoxBillingAddress.IsDataLoaded = true;
					comboBoxBillingAddress.Filter(comboBoxCustomer.SelectedID);
					if (defaultShipToAddress == "")
					{
						comboBoxBillingAddress.SelectedID = "PRIMARY";
					}
					else
					{
						comboBoxBillingAddress.SelectedID = defaultShipToAddress;
					}
					comboBoxPayeeTaxGroup.Clear();
					comboBoxPayeeTaxGroup.SelectedID = comboBoxCustomer.DefaultTaxGroupID;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadCustomerPriceList()
		{
			try
			{
				if (!isDataLoading)
				{
					priceListData = Factory.PriceListSystem.GetActivePriceListByCustomerID(comboBoxCustomer.SelectedID);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void LoadValidItemsforCustomer(string customerID)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				if (isNewRecord)
				{
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
				comboBoxCustomer.FilterSysDocID = comboBoxSysDoc.SelectedID;
				comboBoxGridItem.FilterSysDocID = comboBoxSysDoc.SelectedID;
				allowMultiTemplate = false;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("System_Document", "AllowMultiTemplate", "SysDocID", comboBoxSysDoc.SelectedID);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					allowMultiTemplate = bool.Parse(fieldValue.ToString());
				}
				if (allowMultiTemplate)
				{
					toolStripButtonMultiPreview.Visible = true;
				}
				else
				{
					toolStripButtonMultiPreview.Visible = false;
				}
				strLocation = Factory.DatabaseSystem.GetFieldValue("System_Document", "LocationID", "SysDocID", comboBoxSysDoc.SelectedID).ToString();
				string text = "";
				string text2 = "";
				setCustomerID = false;
				setRegisterID = false;
				text = Factory.DatabaseSystem.GetFieldValue("Location", "DefaultCustomerID", "LocationID", strLocation).ToString();
				text2 = Factory.DatabaseSystem.GetFieldValue("Location", "DefaultRegisterID", "LocationID", strLocation).ToString();
				string text3 = "";
				textBoxCustomerName.Clear();
				comboBoxRegister.Clear();
				if (text != "")
				{
					text3 = Factory.DatabaseSystem.GetFieldValue("Customer", "CustomerName", "CustomerID", text).ToString();
					comboBoxCustomer.Select();
					comboBoxSysDoc.TabStop = false;
					textBoxVoucherNumber.TabStop = false;
					comboBoxRegister.TabStop = false;
					comboBoxBillingAddress.TabStop = false;
					comboBoxShippingAddressID.TabStop = false;
					LoadCustomerPriceList();
				}
				else
				{
					comboBoxSysDoc.TabStop = true;
					textBoxVoucherNumber.TabStop = true;
					comboBoxRegister.TabStop = true;
					comboBoxBillingAddress.TabStop = true;
					comboBoxShippingAddressID.TabStop = true;
				}
				if (text != "")
				{
					setCustomerID = true;
					comboBoxCustomer.Clear();
					comboBoxCustomer.SelectedID = text;
					textBoxCustomerName.Text = text3;
				}
				if (text2 != "")
				{
					setRegisterID = true;
					comboBoxRegister.SelectedID = text2;
				}
				if (strLocation != "")
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Location"].DefaultCellValue = strLocation;
				}
				checkBoxPriceIncludeTax.Visible = comboBoxSysDoc.IsPriceIncludeTax;
				checkBoxPriceIncludeTax.Checked = comboBoxSysDoc.IsPriceIncludeTax;
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			checked
			{
				try
				{
					if (dataGridItems.ActiveRow == null)
					{
						return;
					}
					if (e.Cell.Column.Key == "Item Code")
					{
						if (comboBoxGridItem.SelectedRow == null && e.Cell.Value != null && e.Cell.Value.ToString() != "")
						{
							comboBoxGridItem.SelectedID = e.Cell.Value.ToString();
						}
						else if (comboBoxGridItem.SelectedRow == null)
						{
							return;
						}
						ItemTypes itemTypes = ItemTypes.None;
						unchecked
						{
							if (comboBoxGridItem.SelectedRow != null && !(comboBoxGridItem.SelectedID == "") && comboBoxGridItem.SelectedRow != null && comboBoxGridItem.SelectedRow.Cells["ItemType"].Value != null)
							{
								itemTypes = (ItemTypes)checked((byte)int.Parse(comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString()));
							}
							if (itemTypes == ItemTypes.Matrix)
							{
								supressInventoryMessage = true;
								MatrixSelectionForm matrixSelectionForm = new MatrixSelectionForm();
								matrixSelectionForm.LoadMatrixData(comboBoxGridItem.SelectedID, comboBoxGridItem.SelectedName);
								if (!allowNegativeQty)
								{
									matrixSelectionForm.AllowNegativeQuantity = false;
								}
								string text = "";
								if (dataGridItems.ActiveRow.Index > 0)
								{
									text = dataGridItems.Rows[0].Cells["Location"].Value.ToString();
								}
								dataGridItems.ActiveRow.Delete(displayPrompt: false);
								if (matrixSelectionForm.ShowDialog(this) == DialogResult.OK)
								{
									foreach (DataRow row in matrixSelectionForm.SelectedItems.Tables[0].Rows)
									{
										UltraGridRow ultraGridRow = dataGridItems.DisplayLayout.Bands[0].AddNew();
										ultraGridRow.Cells["Item Code"].Value = row["ProductID"].ToString();
										ultraGridRow.Cells["Description"].Value = row["Description"].ToString();
										if (text == "")
										{
											text = Security.DefaultInventoryLocationID;
										}
										ultraGridRow.Cells["Location"].Value = text;
										ultraGridRow.Cells["Quantity"].Value = row["Quantity"].ToString();
										ultraGridRow.Cells["Price"].Value = row["UnitPrice"].ToString();
										decimal result = default(decimal);
										decimal result2 = default(decimal);
										decimal.TryParse(ultraGridRow.Cells["Quantity"].Value.ToString(), out result);
										decimal.TryParse(ultraGridRow.Cells["Price"].Value.ToString(), out result2);
										ultraGridRow.Cells["Amount"].Value = (result * result2).ToString(Format.TotalAmountFormat);
										ultraGridRow.Update();
									}
								}
								CalculateTotal();
								supressInventoryMessage = false;
								return;
							}
							object obj2 = dataGridItems.ActiveRow.Cells["Description"].Value = (dataGridItems.ActiveRow.Cells["DefaultDescription"].Value = comboBoxGridItem.SelectedName);
							if (!string.IsNullOrEmpty(comboBoxGridItem.SelectedRow.Cells["Attribute1"].Value.ToString()))
							{
								dataGridItems.ActiveRow.Cells["Attribute1"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute1"].Value.ToString();
							}
							if (!string.IsNullOrEmpty(comboBoxGridItem.SelectedRow.Cells["Attribute2"].Value.ToString()))
							{
								dataGridItems.ActiveRow.Cells["Attribute2"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute2"].Value.ToString();
							}
							if (!string.IsNullOrEmpty(comboBoxGridItem.SelectedRow.Cells["Attribute3"].Value.ToString()))
							{
								dataGridItems.ActiveRow.Cells["Attribute3"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute3"].Value.ToString();
							}
							dataGridItems.ActiveRow.Cells["MatrixParentID"].Value = comboBoxGridItem.SelectedRow.Cells["MatrixParentID"].Value.ToString();
							dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
							dataGridItems.ActiveRow.Cells["Quantity"].Tag = null;
							dataGridItems.ActiveRow.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
							if (comboBoxGridItem.SelectedRow != null)
							{
								dataGridItems.ActiveRow.Cells["IsTrackLot"].Value = comboBoxGridItem.SelectedRow.Cells["IsTrackLot"].Value.ToString();
							}
							else
							{
								dataGridItems.ActiveRow.Cells["IsTrackLot"].Value = null;
							}
							if (comboBoxGridItem.SelectedRow != null)
							{
								dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value = comboBoxGridItem.SelectedRow.Cells["IsTrackSerial"].Value.ToString();
							}
							else
							{
								dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value = null;
							}
						}
						if (comboBoxGridItem.SelectedID != "")
						{
							dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
							dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
							dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
							ItemTaxOptions taxOption = comboBoxGridItem.TaxOption;
							dataGridItems.ActiveRow.Cells["TaxOption"].Value = taxOption;
							switch (taxOption)
							{
							case ItemTaxOptions.BasedOnCustomer:
								dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
								break;
							case ItemTaxOptions.Taxable:
								dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = comboBoxGridItem.TaxGroupID;
								break;
							case ItemTaxOptions.NonTaxable:
								dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = DBNull.Value;
								break;
							}
							if (priceListData != null)
							{
								SetPriceAndDescription();
							}
							else
							{
								decimal productSalesPrice = Factory.ProductSystem.GetProductSalesPrice(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
								dataGridItems.ActiveRow.Cells["Price"].Value = productSalesPrice;
							}
							if (setlastSalesprice)
							{
								decimal lastSaleTransationByCustomerID = Factory.ProductSystem.GetLastSaleTransationByCustomerID(comboBoxGridItem.SelectedID, comboBoxCustomer.SelectedID);
								dataGridItems.ActiveRow.Cells["Price"].Value = lastSaleTransationByCustomerID;
							}
							if (comboBoxGridItem.SelectedRow != null)
							{
								dataGridItems.ActiveRow.Cells["ItemType"].Value = comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString();
							}
							else
							{
								dataGridItems.ActiveRow.Cells["ItemType"].Value = null;
							}
							if ((dataGridItems.ActiveRow.Cells["Location"].Value == null || dataGridItems.ActiveRow.Cells["Location"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
							{
								dataGridItems.ActiveRow.Cells["Location"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["Location"].Value;
							}
							if (comboBoxGridItem.SelectedItemType == ItemTypes.Discount)
							{
								dataGridItems.ActiveRow.Cells["Quantity"].Value = -1;
								dataGridItems.ActiveRow.Cells["Quantity"].Activation = Activation.Disabled;
							}
							else
							{
								dataGridItems.ActiveRow.Cells["Quantity"].Activation = Activation.AllowEdit;
							}
							if ((dataGridItems.ActiveRow.Cells["SpecificationID"].Value == null || dataGridItems.ActiveRow.Cells["SpecificationID"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
							{
								dataGridItems.ActiveRow.Cells["SpecificationID"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["SpecificationID"].Value;
							}
							if ((dataGridItems.ActiveRow.Cells["Style"].Value == null || dataGridItems.ActiveRow.Cells["Style"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
							{
								dataGridItems.ActiveRow.Cells["Style"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["Style"].Value;
							}
						}
					}
					else if (e.Cell.Column.Key == "TaxGroupID")
					{
						ItemTaxOptions itemTaxOption = ItemTaxOptions.BasedOnCustomer;
						if (!e.Cell.Row.Cells["TaxOption"].Value.IsNullOrEmpty())
						{
							itemTaxOption = unchecked((ItemTaxOptions)byte.Parse(e.Cell.Row.Cells["TaxOption"].Value.ToString()));
						}
						TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOption, comboBoxGridItem.TaxGroupID);
						e.Cell.Row.Cells["Tax"].Tag = tag;
					}
					else if (e.Cell.Column.Key == "Location")
					{
						if (e.Cell.Row.Cells["Quantity"].Tag != null)
						{
							e.Cell.Row.Cells["Quantity"].Tag = null;
							e.Cell.Row.Cells["Quantity"].Value = 0;
							e.Cell.Row.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
						}
						if (priceListData != null)
						{
							SetPriceAndDescription();
						}
					}
					if (e.Cell.Column.Key == "Unit" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Unit")
					{
						if (priceListData == null || (dataGridItems.ActiveRow.Cells["Unit"].Value == null && e.Cell.Value.ToString() == ""))
						{
							return;
						}
						SetPriceAndDescription();
					}
					if (e.Cell.Column.Key == "Amount")
					{
						decimal result3 = default(decimal);
						decimal.TryParse(e.Cell.Value.ToString(), out result3);
						decimal subtotal = decimal.Parse(textBoxSubtotal.Text);
						decimal tradeDiscount = decimal.Parse(textBoxDiscountAmount.Text);
						UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result3, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
						CalculateTotal();
					}
					if (e.Cell.Column.Key == "SpecificationID")
					{
						for (int i = e.Cell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
						{
							if (dataGridItems.Rows[i].Cells["SpecificationID"].Value.ToString() == "")
							{
								dataGridItems.Rows[i].Cells["SpecificationID"].Value = e.Cell.Value.ToString();
							}
						}
					}
					if (e.Cell.Column.Key == "Style")
					{
						for (int j = e.Cell.Row.Index + 1; j < dataGridItems.Rows.Count; j++)
						{
							if (dataGridItems.Rows[j].Cells["Style"].Value.ToString() == "")
							{
								dataGridItems.Rows[j].Cells["Style"].Value = e.Cell.Value.ToString();
							}
						}
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
				try
				{
					if (dataGridItems.ActiveRow != null)
					{
						string key = e.Cell.Column.Key;
						decimal result4 = default(decimal);
						decimal result5 = default(decimal);
						decimal result6 = default(decimal);
						decimal result7 = default(decimal);
						decimal result8 = default(decimal);
						decimal result9 = default(decimal);
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].OriginalValue.ToString(), out result8);
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].OriginalValue.ToString(), out result9);
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result4);
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result5);
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Discount"].Value.ToString(), out result7);
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result6);
						dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
						dataGridItems.ActiveRow.Cells["Unit"].OriginalValue.ToString();
						if (key == "Quantity" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Quantity")
						{
							result6 = (useInlineDiscount ? Math.Round(result4 * result5 - result7, Global.CurDecimalPoints) : Math.Round(result4 * result5, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result6;
						}
						else if (key == "Price" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Price")
						{
							result6 = (useInlineDiscount ? Math.Round(result4 * result5 - result7, Global.CurDecimalPoints) : Math.Round(result4 * result5, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result6;
						}
						else if (key == "Unit" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Unit")
						{
							result6 = (useInlineDiscount ? Math.Round(result4 * result5 - result7, Global.CurDecimalPoints) : Math.Round(result4 * result5, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result6;
						}
						else if (key == "Discount" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Discount")
						{
							result6 = (useInlineDiscount ? Math.Round(result4 * result5 - result7, Global.CurDecimalPoints) : Math.Round(result4 * result5, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result6;
						}
						else if (key == "Amount" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Amount")
						{
							if (result4 == 0m)
							{
								result4 = 1m;
								dataGridItems.ActiveRow.Cells["Quantity"].Value = result4;
							}
							if (((byte)int.Parse(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString()) == 4 && result6 > 0m) || (result4 < 0m && result6 > 0m))
							{
								result6 = -1m * Math.Abs(result6);
								dataGridItems.ActiveRow.Cells["Amount"].Value = result6;
							}
							result5 = Math.Round((result6 + result7) / result4, 4);
							if (result6 < 0m)
							{
								result4 = -1m * Math.Abs(result4);
								dataGridItems.ActiveRow.Cells["Quantity"].Value = result4;
							}
							dataGridItems.ActiveRow.Cells["Price"].Value = Math.Abs(result5);
						}
						else if (key == "Item Code")
						{
							if (dataGridItems.ActiveRow.Cells["Quantity"].Value == null || dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString() == "")
							{
								dataGridItems.ActiveRow.Cells["Quantity"].Value = result4;
							}
							result6 = (useInlineDiscount ? Math.Round(result4 * result5 - result7, Global.CurDecimalPoints) : Math.Round(result4 * result5, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result6;
						}
						if (key == "Amount")
						{
							decimal result10 = default(decimal);
							decimal.TryParse(e.Cell.Value.ToString(), out result10);
							decimal subtotal2 = decimal.Parse(textBoxSubtotal.Text);
							decimal tradeDiscount2 = decimal.Parse(textBoxDiscountAmount.Text);
							UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result10, subtotal2, tradeDiscount2, checkBoxPriceIncludeTax.Checked);
							CalculateTotal();
						}
					}
				}
				catch (Exception e3)
				{
					dataGridItems.ActiveRow.Cells["Price"].Value = 0;
					dataGridItems.ActiveRow.Cells["Amount"].Value = 0;
					ErrorHelper.ProcessError(e3);
				}
			}
		}

		private void SetPriceAndDescription()
		{
			string text = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
			string text2 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
			string text3 = "ProductID='" + text2 + "' ";
			if (text != "")
			{
				text3 = text3 + " AND UnitID='" + text + "'";
			}
			DataRow[] array = priceListData.Tables[0].Select(text3);
			if (array.Length != 0)
			{
				if (loadItemDescFromPriceList)
				{
					dataGridItems.ActiveRow.Cells["Description"].Value = array[0]["Description"].ToString();
				}
				dataGridItems.ActiveRow.Cells["Price"].Value = decimal.Parse(array[0]["UnitPrice"].ToString());
			}
			else
			{
				decimal productSalesPrice = Factory.ProductSystem.GetProductSalesPrice(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
				dataGridItems.ActiveRow.Cells["Price"].Value = productSalesPrice;
			}
			if (!(text2 != "") || !(text != ""))
			{
				return;
			}
			DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text2, text, comboBoxSysDoc.LocationID);
			if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
			{
				DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
				string a = dataRow["FactorType"].ToString();
				decimal num = decimal.Parse(dataRow["Factor"].ToString());
				decimal num2 = decimal.Parse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString());
				if (a == "M")
				{
					num2 /= num;
				}
				else
				{
					num2 *= num;
				}
				dataGridItems.ActiveRow.Cells["Price"].Value = num2;
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotal();
			CalculateAllRowsTaxes();
			CalculateTotal();
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			comboBoxGridItem.IsLoadingData = true;
		}

		private void comboBoxGridItem_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (dataGridItems.ActiveCell.Column.Key == "Item Code" && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() == "")
			{
				dataGridItems.ActiveRow.Cells["Description"].Value = "";
			}
			checked
			{
				if (dataGridItems.ActiveCell.Column.Key.ToString() == "Price")
				{
					decimal result = default(decimal);
					decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result);
					result = Math.Round(result, 5);
					dataGridItems.ActiveCell.Value = result;
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
				{
					decimal result2 = default(decimal);
					decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result2);
					result2 = Math.Round(result2, 4);
					dataGridItems.ActiveCell.Value = result2;
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
				{
					decimal result3 = default(decimal);
					decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result3);
					result3 = Math.Round(result3, Global.CurDecimalPoints);
					dataGridItems.ActiveCell.Value = result3;
				}
				else
				{
					if (!(dataGridItems.ActiveCell.Column.Key.ToString() == "Location"))
					{
						return;
					}
					for (int i = dataGridItems.ActiveCell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
					{
						if (dataGridItems.Rows[i].Cells["Location"].Value.ToString() == "")
						{
							dataGridItems.Rows[i].Cells["Location"].Value = dataGridItems.ActiveCell.Value;
						}
					}
				}
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow == null)
			{
				return;
			}
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!row.IsActiveRow && row.Cells["Item Code"].Value == dataGridItems.ActiveRow.Cells["Item Code"].Value && !isChecked)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel(row.Cells["Item Code"].Value.ToString() + " item is already in the list. Do you want to continue?"))
					{
					case DialogResult.Cancel:
					case DialogResult.No:
						row.Delete();
						break;
					case DialogResult.Yes:
						isChecked = true;
						break;
					}
				}
			}
			if (activeRow.Cells["Item Code"].Value.ToString() == "" && activeRow.Cells["Quantity"].Value.ToString() != "")
			{
				ErrorHelper.InformationMessage("Please select an item.");
				e.Cancel = true;
				activeRow.Cells["Item Code"].Activate();
				return;
			}
			if (activeRow.Cells["Quantity"].Value.ToString() == "")
			{
				activeRow.Cells["Quantity"].Value = 0;
			}
			if (decimal.Parse(activeRow.Cells["Quantity"].Value.ToString()) == 0m)
			{
				activeRow.Cells["Amount"].Value = 0;
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && activeRow.DataChanged && activeRow.IsAddRow && e.Cell.Column.Key == "Quantity" && CompanyPreferences.NegativeQuantityAction != 1)
			{
				string sysDocID = "";
				string voucherID = "";
				if (!IsNewRecord)
				{
					sysDocID = comboBoxSysDoc.SelectedID;
					voucherID = textBoxVoucherNumber.Text;
				}
				if (!Factory.ProductSystem.IsSufficientQuantityOnhand(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), activeRow.Cells["Location"].Value.ToString(), "Sales_Invoice_Detail", sysDocID, voucherID, decimal.Parse(e.NewValue.ToString())) && !supressInventoryMessage)
				{
					if (CompanyPreferences.NegativeQuantityAction == 2)
					{
						if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You do not have sufficient quantity in this location. Do you want to continue?") != DialogResult.Yes)
						{
							e.Cancel = true;
						}
					}
					else if (CompanyPreferences.NegativeQuantityAction == 3)
					{
						ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
						e.Cancel = true;
						return;
					}
				}
			}
			if (dataGridItems.ActiveCell == null)
			{
				return;
			}
			if (activeRow != null && e.Cell.Column.Key == "Quantity" && dataGridItems.ActiveCell.Column.Key == "Quantity" && !AllocateQuantityToLot(e.Cell))
			{
				e.Cancel = true;
			}
			if (activeRow != null && e.Cell.Column.Key == "Discount" && dataGridItems.ActiveCell.Column.Key == "Discount")
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(e.Cell.Row.Cells["Price"].Value.ToString(), out result);
				decimal.TryParse(e.NewValue.ToString(), out result2);
				if (result2 > result)
				{
					ErrorHelper.InformationMessage("Discount can not be greater than Price!");
					e.Cancel = true;
				}
			}
		}

		private bool AllocateQuantityToLot(UltraGridCell cell)
		{
			bool result = false;
			bool result2 = false;
			if (dataGridItems.ActiveRow.Cells["IsTrackLot"].Value != null)
			{
				bool.TryParse(dataGridItems.ActiveRow.Cells["IsTrackLot"].Value.ToString(), out result);
			}
			if (dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value != null)
			{
				bool.TryParse(dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value.ToString(), out result2);
			}
			if (result)
			{
				if (dataGridItems.ActiveRow.Cells["Location"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select a location first.");
					return false;
				}
				IssueLotSelectionForm issueLotSelectionForm = new IssueLotSelectionForm();
				issueLotSelectionForm.ProductID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				issueLotSelectionForm.ProductDescription = dataGridItems.ActiveRow.Cells["Description"].Value.ToString();
				issueLotSelectionForm.LocationID = dataGridItems.ActiveRow.Cells["Location"].Value.ToString();
				issueLotSelectionForm.RowQuantity = float.Parse(dataGridItems.ActiveCell.Text);
				if (!isNewRecord)
				{
					issueLotSelectionForm.SysDocID = comboBoxSysDoc.SelectedID;
					issueLotSelectionForm.VoucherID = textBoxVoucherNumber.Text;
				}
				if (cell.Tag != null)
				{
					issueLotSelectionForm.ProductLotTable = (DataTable)cell.Tag;
				}
				if (issueLotSelectionForm.ShowDialog() != DialogResult.OK)
				{
					return false;
				}
				cell.Tag = issueLotSelectionForm.ProductLotTable;
				cell.Appearance.FontData.Underline = DefaultableBoolean.True;
			}
			return true;
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Item Code")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridItem.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridItem.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Cost")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage(UIMessages.InvalidAmount);
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "New Qty")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage(UIMessages.AnalysisNotAdded);
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid quantity. Please enter a numeric value.");
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Unit")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please select a valid UOM for the item.");
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Location")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please select a valid location for the item.");
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Discount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid discount! Please enter a valid value.");
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new SalesInvoiceData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.SalesInvoiceTable.Rows[0] : currentData.SalesInvoiceTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["SalesFlow"] = SalesFlows.DirectInvoice;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Reference2"] = textBoxRef2.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["CustomerAddress"] = textBoxBilltoAddress.Text;
				dataRow["ShipToAddress"] = textBoxShipto.Text;
				dataRow["IsCash"] = true;
				if (comboBoxShippingAddressID.SelectedID != "")
				{
					dataRow["ShippingAddressID"] = comboBoxShippingAddressID.SelectedID;
				}
				else
				{
					dataRow["ShippingAddressID"] = DBNull.Value;
				}
				if (comboBoxRegister.SelectedID != "")
				{
					dataRow["RegisterID"] = comboBoxRegister.SelectedID;
				}
				else
				{
					dataRow["RegisterID"] = DBNull.Value;
				}
				if (comboBoxShippingMethod.SelectedID != "")
				{
					dataRow["ShippingMethodID"] = comboBoxShippingMethod.SelectedID;
				}
				else
				{
					dataRow["ShippingMethodID"] = DBNull.Value;
				}
				if (comboBoxBillingAddress.SelectedID != "")
				{
					dataRow["BillingAddressID"] = comboBoxBillingAddress.SelectedID;
				}
				else
				{
					dataRow["BillingAddressID"] = DBNull.Value;
				}
				if (comboBoxSalesperson.SelectedID != "")
				{
					dataRow["SalespersonID"] = comboBoxSalesperson.SelectedID;
				}
				else
				{
					dataRow["SalespersonID"] = DBNull.Value;
				}
				if (comboBoxCurrency.SelectedID != "")
				{
					dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
					dataRow["CurrencyRate"] = comboBoxCurrency.Rate;
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
					dataRow["CurrencyRate"] = DBNull.Value;
				}
				if (comboBoxPayeeTaxGroup.SelectedID != "")
				{
					dataRow["PayeeTaxGroupID"] = comboBoxPayeeTaxGroup.SelectedID;
				}
				else
				{
					dataRow["PayeeTaxGroupID"] = DBNull.Value;
				}
				if (CompanyPreferences.IsTax)
				{
					dataRow["TaxOption"] = (byte)comboBoxCustomer.TaxOption;
					dataRow["PriceIncludeTax"] = checkBoxPriceIncludeTax.Checked;
				}
				else
				{
					dataRow["TaxOption"] = PayeeTaxOptions.NonTaxable;
					dataRow["PriceIncludeTax"] = false;
				}
				dataRow["PONumber"] = textBoxPONumber.Text;
				dataRow["Discount"] = textBoxDiscountAmount.Text;
				dataRow["TaxAmount"] = textBoxTaxAmount.Text;
				dataRow["Total"] = textBoxTotal.Text;
				checked
				{
					dataRow["PaymentMethodType"] = (byte)comboBoxPaymentMethod.SelectedType;
					decimal num = default(decimal);
					num = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
					dataRow["RoundOff"] = num;
					if (comboBoxExpenseCode.SelectedID != "" && comboBoxPaymentMethod.SelectedType == PaymentMethodTypes.CreditCard)
					{
						dataRow["ExpAmount"] = textBoxExpAmount.Text;
						dataRow["ExpCode"] = comboBoxExpenseCode.SelectedID;
					}
					else
					{
						dataRow["ExpAmount"] = DBNull.Value;
						dataRow["ExpCode"] = comboBoxExpenseCode.SelectedID;
					}
					if (comboBoxJob.SelectedID != "")
					{
						dataRow["JobID"] = comboBoxJob.SelectedID;
					}
					else
					{
						dataRow["JobID"] = DBNull.Value;
					}
					if (comboBoxCostCategory.SelectedID != "")
					{
						dataRow["CostCategoryID"] = comboBoxCostCategory.SelectedID;
					}
					else
					{
						dataRow["CostCategoryID"] = DBNull.Value;
					}
					dataRow["SourceDocType"] = sourceDocType;
					dataRow["TempKey"] = CurrentKey;
					dataRow["AutoKeyID"] = TempAutoKeyID;
					dataRow["ReportTo"] = labelReportTo.Text;
					dataRow["CurrentUser"] = Global.CurrentUser.ToString();
					if (comboBoxDriver.SelectedID != "")
					{
						dataRow["DriverID"] = comboBoxDriver.SelectedID;
					}
					else
					{
						dataRow["DriverID"] = DBNull.Value;
					}
					if (comboBoxVehicle.SelectedID != "")
					{
						dataRow["VehicleID"] = comboBoxVehicle.SelectedID;
					}
					else if (comboBoxVehicle.SelectedID == "")
					{
						dataRow["VehicleID"] = textBoxVechicleName.Text;
					}
					else
					{
						dataRow["VehicleID"] = DBNull.Value;
					}
					dataRow.EndEdit();
					if (IsNewRecord)
					{
						currentData.SalesInvoiceTable.Rows.Add(dataRow);
					}
					foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
					{
						if (!currentData.SalesInvoiceDetailTable.Columns.Contains(column.Key))
						{
							currentData.SalesInvoiceDetailTable.Columns.Add(column.Key, column.DataType);
						}
					}
					currentData.SalesInvoiceDetailTable.Rows.Clear();
					currentData.Tables["Product_Lot_Issue_Detail"].Rows.Clear();
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						DataRow dataRow2 = currentData.SalesInvoiceDetailTable.NewRow();
						dataRow2.BeginEdit();
						dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
						dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
						dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
						if (row.Cells["Discount"].Value != null && row.Cells["Discount"].Value.ToString() != "")
						{
							dataRow2["Discount"] = row.Cells["Discount"].Value.ToString();
						}
						else
						{
							dataRow2["Discount"] = DBNull.Value;
						}
						if (!row.Cells["TaxOption"].Value.IsNullOrEmpty())
						{
							dataRow2["TaxOption"] = row.Cells["TaxOption"].Value.ToString();
						}
						else
						{
							dataRow2["TaxOption"] = (byte)2;
						}
						if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
						{
							dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
						}
						dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
						dataRow2["UnitPrice"] = row.Cells["Price"].Value.ToString();
						dataRow2["Description"] = row.Cells["Description"].Value.ToString();
						dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
						dataRow2["SpecificationID"] = row.Cells["SpecificationID"].Value.ToString();
						dataRow2["StyleID"] = row.Cells["Style"].Value.ToString();
						if (row.Cells["Tax"].Value != null && row.Cells["Tax"].Value.ToString() != "")
						{
							dataRow2["TaxAmount"] = row.Cells["Tax"].Value.ToString();
						}
						else
						{
							dataRow2["TaxAmount"] = DBNull.Value;
						}
						if (row.Cells["TaxGroupID"].Value != null && row.Cells["TaxGroupID"].Value.ToString() != "")
						{
							dataRow2["TaxGroupID"] = row.Cells["TaxGroupID"].Value.ToString();
						}
						else
						{
							dataRow2["TaxGroupID"] = DBNull.Value;
						}
						dataRow2["Attribute1"] = row.Cells["Attribute1"].Value.ToString();
						dataRow2["Attribute2"] = row.Cells["Attribute2"].Value.ToString();
						dataRow2["Attribute3"] = row.Cells["Attribute3"].Value.ToString();
						dataRow2["MatrixParentID"] = row.Cells["MatrixParentID"].Value.ToString();
						if (row.Cells["ListRowIndex"].Value != null && row.Cells["ListRowIndex"].Value.ToString() != "")
						{
							dataRow2["ListRowIndex"] = row.Cells["ListRowIndex"].Value.ToString();
						}
						else
						{
							dataRow2["ListRowIndex"] = DBNull.Value;
						}
						dataRow2["ListSysDocID"] = row.Cells["ListSysDocID"].Value.ToString();
						dataRow2["ListVoucherID"] = row.Cells["ListVoucherID"].Value.ToString();
						if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SOSysDocID"))
						{
							if (row.Cells["SOSysDocID"].Value.ToString() != "")
							{
								dataRow2["OrderSysDocID"] = row.Cells["SOSysDocID"].Value.ToString();
							}
							if (row.Cells["SOVoucherID"].Value.ToString() != "")
							{
								dataRow2["OrderVoucherID"] = row.Cells["SOVoucherID"].Value.ToString();
							}
							if (row.Cells["SORowIndex"].Value.ToString() != "")
							{
								dataRow2["OrderRowIndex"] = row.Cells["SORowIndex"].Value.ToString();
							}
							if (row.Cells.Exists("IsDNRow") && row.Cells["IsDNRow"].Value.ToString() != "")
							{
								dataRow2["IsDNRow"] = true;
							}
						}
						dataRow2["RowIndex"] = row.Index;
						dataRow2.EndEdit();
						currentData.SalesInvoiceDetailTable.Rows.Add(dataRow2);
						string b = row.Cells["Location"].Value.ToString();
						string text = row.Cells["Item Code"].Value.ToString();
						if (row.Cells["Quantity"].Tag != null)
						{
							foreach (DataRow row2 in (row.Cells["Quantity"].Tag as DataTable).Rows)
							{
								if (row2["LocationID"].ToString() != b || row2["ProductID"].ToString() != text)
								{
									ErrorHelper.WarningMessage("Location or Product code mismatch with selected lots for item code: '" + text + "'", "Please reallocate the lots for this item.");
									return false;
								}
								DataRow dataRow4 = currentData.Tables["Product_Lot_Issue_Detail"].NewRow();
								dataRow4["ProductID"] = row2["ProductID"];
								dataRow4["LocationID"] = row2["LocationID"];
								dataRow4["LotNumber"] = row2["LotNumber"];
								dataRow4["Reference"] = row2["Reference"];
								dataRow4["SourceLotNumber"] = row2["SourceLotNumber"];
								dataRow4["SoldQty"] = row2["SoldQty"];
								dataRow4["Cost"] = row2["Cost"];
								dataRow4["BinID"] = row2["BinID"];
								dataRow4["Reference2"] = row2["Reference2"];
								dataRow4["SysDocID"] = comboBoxSysDoc.SelectedID;
								dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
								dataRow4["UnitPrice"] = row.Cells["Price"].Value.ToString();
								dataRow4["RowIndex"] = row.Index;
								currentData.Tables["Product_Lot_Issue_Detail"].Rows.Add(dataRow4);
							}
						}
					}
					currentData.PaymentTable.Rows.Clear();
					dataRow = currentData.PaymentTable.NewRow();
					dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow["RegisterID"] = comboBoxRegister.SelectedID;
					dataRow["TransactionDate"] = dateTimePickerDate.Value;
					dataRow["Amount"] = textBoxTotal.Text;
					dataRow["PaymentMethodType"] = (byte)comboBoxPaymentMethod.SelectedType;
					currentData.PaymentTable.Rows.Add(dataRow);
					currentData.Tables["Tax_Detail"].Rows.Clear();
					string selectedID = comboBoxSysDoc.SelectedID;
					string text2 = textBoxVoucherNumber.Text;
					int num2 = 0;
					foreach (UltraGridRow row3 in dataGridItems.Rows)
					{
						if (row3.Cells["Tax"].Tag != null)
						{
							TaxHelper.CreateTaxRows(currentData, row3.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Items, selectedID, text2, num2, comboBoxCurrency.SelectedID, comboBoxCurrency.Rate);
						}
						num2++;
					}
					if (textBoxTaxAmount.Tag != null)
					{
						TaxHelper.CreateTaxRows(currentData, textBoxTaxAmount.Tag as TaxTransactionData, TaxDetailLevel.Transaction, selectedID, text2, -1, comboBoxCurrency.SelectedID, comboBoxCurrency.Rate);
					}
					return true;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Remarks");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("TaxGroupID");
				dataTable.Columns.Add("TaxOption", typeof(byte));
				dataTable.Columns.Add("Tax");
				dataTable.Columns.Add("IsTrackLot");
				dataTable.Columns.Add("IsTrackSerial");
				dataTable.Columns.Add("Attribute1");
				dataTable.Columns.Add("Attribute2");
				dataTable.Columns.Add("Attribute3");
				dataTable.Columns.Add("SpecificationID");
				dataTable.Columns.Add("Style");
				dataTable.Columns.Add("MatrixParentID");
				dataTable.Columns.Add("ItemType");
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Discount", typeof(decimal));
				dataTable.Columns.Add("DefaultDescription");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("ListSysDocID");
				dataTable.Columns.Add("ListVoucherID");
				dataTable.Columns.Add("ListRowIndex", typeof(int));
				dataTable.Columns.Add("SOSysDocID");
				dataTable.Columns.Add("SOVoucherID");
				dataTable.Columns.Add("SORowIndex", typeof(int));
				dataTable.Columns.Add("RowSourceType", typeof(int));
				dataTable.Columns.Add("TaxTotal", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].Hidden = true;
				bool flag4 = ultraGridColumn3.Hidden = flag2;
				bool hidden = ultraGridColumn2.Hidden = flag4;
				ultraGridColumn.Hidden = hidden;
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Hidden = true);
				ultraGridColumn4.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].Hidden = true;
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["SOSysDocID"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["SOVoucherID"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["SORowIndex"];
				flag2 = (dataGridItems.DisplayLayout.Bands[0].Columns["RowSourceType"].Hidden = true);
				flag4 = (ultraGridColumn7.Hidden = flag2);
				hidden = (ultraGridColumn6.Hidden = flag4);
				ultraGridColumn5.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.LoadLayout();
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn10 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				Activation activation2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].CellActivation = Activation.Disabled;
				Activation activation4 = ultraGridColumn10.CellActivation = activation2;
				Activation activation7 = ultraGridColumn8.CellActivation = (ultraGridColumn9.CellActivation = activation4);
				dataGridItems.DisplayLayout.Bands[0].Columns["RowSourceType"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOSysDocID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOVoucherID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SORowIndex"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowSourceType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance;
				AppearanceBase cellAppearance4 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].CellAppearance;
				Color color = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				Color color3 = cellAppearance4.BackColorDisabled = color;
				Color color5 = cellAppearance3.BackColorDisabled = color3;
				Color color8 = cellAppearance.BackColorDisabled = (cellAppearance2.BackColorDisabled = color5);
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].ValueList = comboBoxStyle;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].ValueList = comboBoxSpecification;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].Hidden = !useInlineDiscount;
				if (useInlineDiscount)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				}
				UltraGridColumn ultraGridColumn11 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].Hidden = true);
				ultraGridColumn11.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].Header.Caption = "Default Description";
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Caption = "Tax";
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].Header.Caption = "Tax Group";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].Header.Caption = "Net Amount";
				AppearanceBase cellAppearance5 = dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance;
				AppearanceBase cellAppearance6 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance;
				AppearanceBase cellAppearance7 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance;
				color3 = (dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColor = Color.WhiteSmoke);
				color5 = (cellAppearance7.BackColorDisabled = color3);
				color8 = (cellAppearance5.BackColor = (cellAppearance6.BackColor = color5));
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].Header.Caption = CompanyPreferences.Attribute1Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].Header.Caption = CompanyPreferences.Attribute2Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].Header.Caption = CompanyPreferences.Attribute3Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].Header.Caption = CompanyPreferences.SpecificationID;
				AdjustGridColumnSettings();
				dataGridItems.SetupUI();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void AdjustGridColumnSettings()
		{
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = comboBoxGridItem;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].ValueList = comboBoxGridLocation;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MaxLength = 15;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].DefaultCellValue = Security.DefaultInventoryLocationID;
			if (Security.DefaultInventoryLocationID == "")
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].DefaultCellValue = strLocation;
			}
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SOSysDocID"))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["SOSysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOVoucherID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SORowIndex"].Hidden = true;
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
			dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = true;
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Ordered"))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOSysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOVoucherID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SORowIndex"].Hidden = true;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					row.Cells["Item Code"].Activation = Activation.Disabled;
					row.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Unit"].Activation = Activation.Disabled;
					row.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
				}
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = -99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Format = "#,0.00##";
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].Format = "#,0.00##";
			dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = new decimal(-999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].Format = Format.GridAmountFormat;
			dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].MinValue = new decimal(-999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellActivation = Activation.NoEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].TabStop = false;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.Cursor = Cursors.Hand;
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("TotalQty"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"]);
			}
			dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Ordered"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"]);
			}
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Shipped"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"]);
			}
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Ordered"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Ordered", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Shipped", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].DisplayFormat = "{0:n}";
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeInventoryLocation))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.Disabled;
				comboBoxGridLocation.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangePrice))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.NoEdit;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.AllowEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].CellActivation = Activation.AllowEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.AllowEdit;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDescription))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 3000;
			UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["ListSysDocID"];
			UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["ListRowIndex"];
			bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["ListVoucherID"].Hidden = true;
			bool hidden = ultraGridColumn2.Hidden = flag2;
			ultraGridColumn.Hidden = hidden;
			dataGridItems.DisplayLayout.Bands[0].Columns["Style"].Hidden = true;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			comboBoxCustomer.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.SalesInvoiceSystem.GetSalesInvoiceByID(SystemDocID, voucherID);
					if (currentData != null)
					{
						IsNewRecord = false;
						FillData();
						formManager.ResetDirty();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				ClearForm();
			}
		}

		private void FillData()
		{
			try
			{
				isDataLoading = true;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Sales_Invoice"].Rows[0];
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxRef2.Text = dataRow["Reference2"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					if (dataRow["CurrencyRate"] != DBNull.Value)
					{
						comboBoxCurrency.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
					}
					else
					{
						comboBoxCurrency.Rate = 1m;
					}
					if (dataRow["PriceIncludeTax"] != DBNull.Value && CompanyPreferences.IsTax)
					{
						checkBoxPriceIncludeTax.Checked = bool.Parse(dataRow["PriceIncludeTax"].ToString());
					}
					else
					{
						checkBoxPriceIncludeTax.Checked = false;
					}
					comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
					comboBoxBillingAddress.SelectedID = dataRow["BillingAddressID"].ToString();
					textBoxShipto.Text = dataRow["ShipToAddress"].ToString();
					textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
					comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
					comboBoxRegister.SelectedID = dataRow["RegisterID"].ToString();
					comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString().Trim();
					labelReportTo.Text = dataRow["ReportTo"].ToString();
					textBoxPONumber.Text = dataRow["PONumber"].ToString();
					comboBoxJob.SelectedID = dataRow["JobID"].ToString();
					comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
					int selectedType = int.Parse(dataRow["PaymentMethodType"].ToString());
					comboBoxPaymentMethod.SelectedType = (PaymentMethodTypes)selectedType;
					comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
					if (comboBoxPaymentMethod.SelectedType == PaymentMethodTypes.CreditCard)
					{
						comboBoxExpenseCode.SelectedID = dataRow["ExpCode"].ToString();
						textBoxExpAmount.Text = dataRow["ExpAmount"].ToString();
					}
					if (textBoxTaxAmount.Text == "0.00")
					{
						isTaxPercent = false;
					}
					if (dataRow["DiscountFC"] != DBNull.Value)
					{
						textBoxDiscountAmount.Text = decimal.Parse(dataRow["DiscountFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxDiscountAmount.Text = decimal.Parse(dataRow["Discount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (dataRow["TaxAmountFC"] != DBNull.Value)
					{
						textBoxTaxAmount.Text = decimal.Parse(dataRow["TaxAmountFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else if (dataRow["TaxAmount"] != DBNull.Value)
					{
						textBoxTaxAmount.Text = decimal.Parse(dataRow["TaxAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTaxAmount.Text = decimal.Parse("0").ToString(Format.TotalAmountFormat);
					}
					if (dataRow["TotalFC"] != DBNull.Value)
					{
						textBoxTotal.Text = decimal.Parse(dataRow["TotalFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTotal.Text = decimal.Parse(dataRow["Total"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (dataRow["RoundOff"] != DBNull.Value)
					{
						textBoxRoundOff.Text = decimal.Parse(dataRow["RoundOff"].ToString()).ToString(Format.TotalAmountFormat);
					}
					comboBoxDriver.SelectedID = dataRow["DriverID"].ToString();
					DataRow[] array = Factory.VehicleSystem.GetVehicleComboList().Tables[0].Select("Code = '" + dataRow["VehicleID"].ToString() + "'");
					if (array.Length != 0)
					{
						comboBoxVehicle.SelectedID = dataRow["VehicleID"].ToString();
					}
					else if (array.Length == 0)
					{
						comboBoxVehicle.Clear();
						textBoxVechicleName.ReadOnly = false;
						textBoxVechicleName.Text = dataRow["VehicleID"].ToString();
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Sales_Invoice_Detail") && currentData.SalesInvoiceDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Sales_Invoice_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Item Code"] = row["ProductID"];
							if (row["UnitQuantity"] != DBNull.Value)
							{
								dataRow3["Quantity"] = row["UnitQuantity"];
							}
							else
							{
								dataRow3["Quantity"] = row["Quantity"];
							}
							dataRow3["Attribute1"] = row["Attribute1"];
							dataRow3["Attribute2"] = row["Attribute2"];
							dataRow3["Attribute3"] = row["Attribute3"];
							dataRow3["MatrixParentID"] = row["MatrixParentID"];
							dataRow3["IsTrackLot"] = row["IsTrackLot"];
							dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
							dataRow3["Description"] = row["Description"];
							dataRow3["Remarks"] = row["Remarks"];
							dataRow3["Location"] = row["LocationID"];
							dataRow3["Unit"] = row["UnitID"];
							dataRow3["ItemType"] = row["ItemType"];
							if (string.IsNullOrEmpty(dataRow3["ItemType"].ToString()))
							{
								string text2 = (string)(dataRow3["ItemType"] = Factory.DatabaseSystem.GetFieldValue("Product", "ItemType", "ProductID", row["ProductID"].ToString()).ToString());
							}
							dataRow3["SpecificationID"] = row["SpecificationID"];
							dataRow3["Style"] = row["StyleID"];
							dataRow3["ListSysDocID"] = row["ListSysDocID"];
							dataRow3["ListVoucherID"] = row["ListVoucherID"];
							dataRow3["ListRowIndex"] = row["ListRowIndex"];
							dataRow3["SOSysDocID"] = row["OrderSysDocID"];
							dataRow3["SOVoucherID"] = row["OrderVoucherID"];
							dataRow3["SORowIndex"] = row["OrderRowIndex"];
							dataRow3["TaxOption"] = row["TaxOption"];
							if (row["UnitPriceFC"] != DBNull.Value)
							{
								dataRow3["Price"] = decimal.Parse(row["UnitPriceFC"].ToString()).ToString(Format.UnitPriceFormat);
							}
							else
							{
								dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
							}
							if (row["Discount"] != DBNull.Value)
							{
								dataRow3["Discount"] = decimal.Parse(row["Discount"].ToString()).ToString(Format.UnitPriceFormat);
							}
							if (row["TaxAmount"] != DBNull.Value)
							{
								dataRow3["Tax"] = decimal.Parse(row["TaxAmount"].ToString()).ToString(Format.UnitPriceFormat);
							}
							if (currentData.Tables["Sales_Invoice_Detail"].Columns.Contains("TaxGroupID"))
							{
								dataRow3["TaxGroupID"] = row["TaxGroupID"];
							}
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal result3 = default(decimal);
							decimal result4 = default(decimal);
							decimal.TryParse(dataRow3["Quantity"].ToString(), out result);
							decimal.TryParse(dataRow3["Price"].ToString(), out result2);
							decimal.TryParse(dataRow3["Discount"].ToString(), out result3);
							decimal.TryParse(dataRow3["Tax"].ToString(), out result4);
							dataRow3["Amount"] = Math.Round(decimal.Parse(row["Amount"].ToString()), Global.CurDecimalPoints);
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						if (dataRow["IsVoid"] != DBNull.Value)
						{
							IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
						}
						else
						{
							IsVoid = false;
						}
						foreach (UltraGridRow row2 in dataGridItems.Rows)
						{
							if (!string.IsNullOrEmpty(row2.Cells["ItemType"].Value.ToString()) && checked((byte)int.Parse(row2.Cells["ItemType"].Value.ToString())) == 4)
							{
								row2.Cells["Quantity"].Activation = Activation.Disabled;
							}
							string productID = row2.Cells["Item Code"].Value.ToString();
							row2.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
							if (row2.Cells["IsTrackLot"].Value != DBNull.Value && bool.Parse(row2.Cells["IsTrackLot"].Value.ToString()))
							{
								DataRow[] array2 = currentData.Tables["Product_Lot_Issue_Detail"].Select("ProductID = '" + row2.Cells["Item Code"].Value.ToString() + "' AND RowIndex = " + row2.Index);
								if (array2.Length != 0)
								{
									DataSet dataSet = new DataSet();
									dataSet.Merge(array2);
									DataTable tag = dataSet.Tables[0];
									row2.Cells["Quantity"].Tag = tag;
									row2.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
								}
							}
							DataRow[] array3 = currentData.TaxDetailsTable.Select("RowIndex = " + row2.Index + " AND TaxLevel = " + (byte)2);
							if (array3.Length != 0)
							{
								TaxTransactionData taxTransactionData = new TaxTransactionData();
								taxTransactionData.Merge(array3);
								row2.Cells["Tax"].Tag = taxTransactionData;
							}
						}
						DataRow[] array4 = currentData.TaxDetailsTable.Select("RowIndex = -1 AND TaxLevel = " + (byte)1);
						if (array4.Length != 0)
						{
							TaxTransactionData taxTransactionData2 = new TaxTransactionData();
							taxTransactionData2.Merge(array4);
							textBoxTaxAmount.Tag = taxTransactionData2;
						}
						CalculateTotal();
						EnableButton();
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				isDataLoading = false;
			}
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
			if (CompanyPreferences.EnableDocTempSaving)
			{
				GetUpdatedDocumentNo();
			}
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			bool flag = false;
			try
			{
				bool flag2 = Factory.SalesInvoiceSystem.CreateSalesInvoice(currentData, !isNewRecord, showLoadDraft);
				if (flag2)
				{
					flag = true;
				}
				if (!flag2)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					bool result = false;
					bool result2 = false;
					bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("DoPrint").ToString(), out result);
					if (result)
					{
						bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("PrintAfterSave").ToString(), out result2);
						if (result2)
						{
							Print(isPrint: true, showPrintDialog: true, saveChanges: false);
						}
						else
						{
							Print(isPrint: false, showPrintDialog: true, saveChanges: false);
						}
					}
				}
				return flag2;
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1046)
				{
					string nextVoucherNumber = GetNextVoucherNumber();
					if (nextVoucherNumber == textBoxVoucherNumber.Text)
					{
						ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
						return false;
					}
					if (nextVoucherNumber != "")
					{
						textBoxVoucherNumber.Text = nextVoucherNumber;
					}
					formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
					return SaveData();
				}
				ErrorHelper.ProcessError(ex);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			finally
			{
				if (flag)
				{
					if (clearAfter)
					{
						ClearForm();
						IsNewRecord = true;
					}
					else
					{
						formManager.ResetDirty();
					}
				}
			}
		}

		private void EnableButton()
		{
			bool flag = Factory.SalesInvoiceSystem.ModifyTransactions(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, Global.CurrentUser.ToUpper(), isModify: false, "");
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.Edit && flag)
			{
				buttonSave.Enabled = true;
				screenRight.Edit = true;
			}
			else if (!screenRight.Edit)
			{
				buttonSave.Enabled = false;
				screenRight.Edit = false;
			}
			if (Factory.SystemDocumentSystem.ExistDocumentNumber("Sales_Invoice", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				simpleButtonSave.Enabled = false;
			}
			else
			{
				simpleButtonSave.Enabled = true;
			}
		}

		public bool ValidationwithColor()
		{
			bool flag = true;
			try
			{
				object[] all = dataGridItems.Rows.All;
				for (int i = 0; i < all.Length; i++)
				{
					UltraGridRow ultraGridRow = (UltraGridRow)all[i];
					ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Transparent;
					bool flag2 = false;
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					if (ultraGridRow != null && ultraGridRow.DataChanged)
					{
						decimal.TryParse(ultraGridRow.Cells["Price"].Value.ToString(), out result2);
						decimal num = default(decimal);
						decimal num2 = default(decimal);
						string text = "MinPrice";
						text = (Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice) ? "MinPrice" : "UnitPrice3");
						decimal.TryParse(Factory.DatabaseSystem.GetFieldValue("Product", text, "ProductID", ultraGridRow.Cells["Item Code"].Value.ToString()).ToString(), out result);
						object fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_PriceList_Detail", text, "ProductID", ultraGridRow.Cells["Item Code"].Value.ToString(), "UnitID", ultraGridRow.Cells["Unit"].Value.ToString(), "LocationID", comboBoxSysDoc.LocationID);
						if (fieldValue == null)
						{
							fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_PriceList_Detail", text, "ProductID", ultraGridRow.Cells["Item Code"].Value.ToString(), "UnitID", ultraGridRow.Cells["Unit"].Value.ToString(), "LocationID", "");
						}
						if (fieldValue != null)
						{
							decimal.TryParse(fieldValue.ToString(), out result);
						}
						string text2 = ultraGridRow.Cells["Unit"].Value.ToString();
						string text3 = ultraGridRow.Cells["Item Code"].Value.ToString();
						ultraGridRow.Cells["Location"].Value.ToString();
						if (text3 != "" && text2 != "")
						{
							DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text3, text2, comboBoxSysDoc.LocationID);
							if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
							{
								DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
								string a = dataRow["FactorType"].ToString();
								decimal num3 = decimal.Parse(dataRow["Factor"].ToString());
								if (a == "M")
								{
									result /= num3;
								}
								else
								{
									result *= num3;
								}
							}
						}
						num = result * (priceDiscountPercAllowed / 100m);
						num2 = result - num;
						if (result2 < num2)
						{
							flag2 = true;
							if (priceDiscountPercAllowed > 0m)
							{
								ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
								flag = false;
							}
						}
						if (Global.IsUserAdmin || priceDiscountPercAllowed == 0m)
						{
							flag2 = false;
						}
						if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowPriceDiscount) && result2 < num2)
						{
							flag2 = true;
						}
					}
					checked
					{
						if (CompanyPreferences.LocalSalesFlow != SalesFlows.SOThenDNThenInvoice && ultraGridRow != null && ultraGridRow.DataChanged && ultraGridRow.Cells["ItemType"].Value.ToString() != "")
						{
							_ = (byte)int.Parse(ultraGridRow.Cells["ItemType"].Value.ToString());
						}
						if (ultraGridRow != null && ultraGridRow.DataChanged && ultraGridRow.Cells["Price"].Value != null && ultraGridRow.Cells["Price"].Value.ToString() != "")
						{
							if (CompanyPreferences.MinPriceSaleAction != 1)
							{
								bool flag3 = Factory.SalesInvoiceSystem.IsBelowMinPrice(ultraGridRow.Cells["Item Code"].Value.ToString(), ultraGridRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString()), comboBoxSysDoc.LocationID);
								if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
								{
									flag3 = Factory.SalesInvoiceSystem.IsBelowMinAllowedPrice(ultraGridRow.Cells["Item Code"].Value.ToString(), ultraGridRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString()));
								}
								if (flag3)
								{
									if (CompanyPreferences.MinPriceSaleAction == 2)
									{
										ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
										flag = false;
									}
									else if (CompanyPreferences.MinPriceSaleAction == 3 && flag2)
									{
										ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
										flag = false;
									}
									else if (CompanyPreferences.MinPriceSaleAction == 4)
									{
										ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
										flag = false;
										ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
										flag = false;
									}
								}
							}
							if (CompanyPreferences.PricelessCostAction != 1 && Factory.SalesInvoiceSystem.IsBelowAverageCost(ultraGridRow.Cells["Item Code"].Value.ToString(), ultraGridRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString())))
							{
								if (CompanyPreferences.PricelessCostAction == 2)
								{
									ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
									flag = false;
								}
								else if (CompanyPreferences.PricelessCostAction == 3)
								{
									ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
									flag = false;
								}
								else if (CompanyPreferences.PricelessCostAction == 4)
								{
									ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
									flag = false;
									ultraGridRow.Cells["Price"].Appearance.BackColor = Color.Red;
									flag = false;
								}
							}
						}
						flag = (flag && flag);
					}
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return flag;
			}
		}

		private bool ValidateData()
		{
			CalculateTotal();
			CalculateAllRowsTaxes();
			CalculateTotal();
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
			if (!IsNewRecord && !Global.IsUserAdmin && !allowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Sales_Invoice", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !allowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
			int num = 0;
			num = Security.AllowedDays(GeneralSecurityRoles.EnterBackDatedTransaction);
			DateTime value = dateTimePickerDate.Value;
			TimeSpan timeSpan = t.Add(TimeSpan.FromDays(1.0)) - value;
			bool flag = false;
			checked
			{
				if (timeSpan.Days <= num + 1)
				{
					flag = true;
				}
				else if (Global.isUserAdmin)
				{
					flag = true;
				}
				else if (num == 0)
				{
					flag = true;
				}
				if (isNewRecord && dateTimePickerDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
					return false;
				}
				if (!flag)
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions not more than " + num + " days.");
					return false;
				}
				if (isNewRecord && dateTimePickerDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
					return false;
				}
				if (!IsNewRecord && Factory.SalesInvoiceSystem.InvoiceHasShippedQuantity(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this invoice has been already delivered. You are not able to modify.");
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxCustomer.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				if (!hideRegister && comboBoxRegister.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				decimal result = -1m;
				if (!decimal.TryParse(textBoxTotal.Text, out result) || result < 0m)
				{
					ErrorHelper.InformationMessage("Cannot save an invoice with negative total.");
					return false;
				}
				bool result2 = false;
				bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Customer", "IsHold", "CustomerID", comboBoxCustomer.SelectedID).ToString(), out result2);
				if (comboBoxCustomer.IsCustomerOnHold | result2)
				{
					ErrorHelper.WarningMessage("This customer is on hold status and does not allow transaction.");
					return false;
				}
				showednotdiscount = false;
				ValidateAllowedDiscount();
				if (showednotdiscount)
				{
					ErrorHelper.WarningMessage("Higher Discount is not applicable.", "Please enter a proper discount.");
					return false;
				}
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
					}
					else
					{
						if (dataGridItems.Rows[i].Cells["Item Code"].Value.ToString() == "")
						{
							ErrorHelper.InformationMessage("Please select an item.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
						if (dataGridItems.Rows[i].Cells["Location"].Value.ToString() == "")
						{
							ErrorHelper.InformationMessage("Please select a location for all the items.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
						if (allowzeroprice && (dataGridItems.Rows[i].Cells["Amount"].Value.ToString() == "0" || dataGridItems.Rows[i].Cells["Price"].Value.ToString() == "0" || dataGridItems.Rows[i].Cells["Amount"].Value.ToString() == "0.00" || dataGridItems.Rows[i].Cells["Price"].Value.ToString() == "0.00"))
						{
							ErrorHelper.InformationMessage("Do not allowed to save with zero price.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
					}
					UltraGridRow ultraGridRow = dataGridItems.Rows[i];
					decimal d = decimal.Parse(ultraGridRow.Cells["Quantity"].Value.ToString());
					decimal d2 = decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString());
					decimal d3 = decimal.Parse(ultraGridRow.Cells["Amount"].Value.ToString());
					decimal d4 = default(decimal);
					decimal result3 = default(decimal);
					if (ultraGridRow.Cells["Discount"].Value != DBNull.Value)
					{
						decimal.TryParse(ultraGridRow.Cells["Discount"].Value.ToString(), out result3);
					}
					if (Math.Abs(Math.Round(d * d2 - result3 + d4, Global.CurDecimalPoints) - d3) > 0.1m)
					{
						ErrorHelper.WarningMessage("Amount does not match the quantity and price for row no:" + ultraGridRow.Index + 1 + "\nItem Code:" + ultraGridRow.Cells["Item Code"].Value.ToString());
						return false;
					}
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one row of item.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Sales_Invoice", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
					textBoxVoucherNumber.Focus();
					return false;
				}
				return true;
			}
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["Quantity"].Value != null && row.Cells["Quantity"].Value.ToString() != "")
				{
					result += decimal.Parse(row.Cells["Quantity"].Value.ToString());
				}
			}
			return result;
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

		private void setTab()
		{
			textBoxShipto.TabStop = false;
			comboBoxBillingAddress.TabStop = false;
			textBoxNote.TabStop = false;
			textBoxRef1.TabStop = false;
			textBoxRef2.TabStop = false;
			comboBoxJob.TabStop = false;
			comboBoxCostCategory.TabStop = false;
			dateTimePickerDate.TabStop = false;
			textBoxVoucherNumber.TabStop = false;
			comboBoxGridItem.TabStop = false;
			textBoxCustomerName.TabStop = false;
			comboBoxCustomer.TabStop = false;
			textBoxPONumber.TabStop = false;
			comboBoxSalesperson.TabStop = false;
			comboBoxShippingAddressID.TabStop = false;
			comboBoxShippingMethod.TabStop = false;
		}

		private void ClearForm()
		{
			try
			{
				allowEdit = true;
				textBoxShipto.Clear();
				textBoxNote.Clear();
				textBoxRef1.Clear();
				textBoxRef2.Clear();
				comboBoxJob.Clear();
				comboBoxCostCategory.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				checkedListBoxPI.Items.Clear();
				textBoxBilltoAddress.Clear();
				textBoxShipto.Clear();
				CurrentKey = "";
				TempAutoKeyID = 0;
				if (!setCustomerID)
				{
					comboBoxCustomer.Clear();
					textBoxCustomerName.Clear();
					comboBoxBillingAddress.Clear();
					comboBoxShippingAddressID.Clear();
					textBoxBilltoAddress.Clear();
					priceListData = null;
					comboBoxShippingMethod.Clear();
					comboBoxPayeeTaxGroup.Clear();
				}
				if (!setRegisterID)
				{
					comboBoxRegister.Clear();
				}
				textBoxPONumber.Clear();
				comboBoxSalesperson.Clear();
				textBoxSubtotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxRoundOff.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscountAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscountPercent.Text = "0";
				isDiscountPercent = false;
				isDiscountPercent = false;
				comboBoxPaymentMethod.SelectedType = PaymentMethodTypes.Cash;
				labelReportTo.Text = "";
				comboBoxExpenseCode.Clear();
				textBoxExpAmount.Clear();
				textboxPercent.Clear();
				simpleButtonSave.Enabled = true;
				IsTemporaryTransaction = false;
				isChecked = false;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				comboBoxSalesperson.SetDefault(Security.DefaultSalespersonID);
				comboBoxRegister.SelectedID = Security.DefaultTransactionRegisterID;
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeSysDoc))
				{
					comboBoxSysDoc.ReadOnly = true;
				}
				else
				{
					comboBoxSysDoc.ReadOnly = false;
				}
				comboBoxDriver.Clear();
				comboBoxVehicle.Clear();
				if (comboBoxVehicle.SelectedID == "")
				{
					textBoxVechicleName.ReadOnly = false;
				}
				else
				{
					textBoxVechicleName.ReadOnly = true;
				}
				checkBoxPriceIncludeTax.Checked = comboBoxSysDoc.IsPriceIncludeTax;
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				if (dataTable.Columns.Contains("Ordered"))
				{
					dataTable.Columns.Remove("Ordered");
					dataTable.Columns.Remove("Shipped");
				}
				dataTable.Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
				comboBoxCustomer.Focus();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void JournalLeaveGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void JournalLeaveGroupDetailsForm_Validated(object sender, EventArgs e)
		{
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
				if (!allowEdit)
				{
					ErrorHelper.InformationMessage(Application.ProductName, "You cannot delete this transfer transaction because it is already accepted or rejected.", "Document is in use.");
					return false;
				}
				if (!IsNewRecord && Factory.SalesInvoiceSystem.InvoiceHasShippedQuantity(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this invoice has been already delivered. You are not able to delete.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.SalesInvoiceSystem.DeleteSalesInvoice(SystemDocID, textBoxVoucherNumber.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("Sales_Invoice", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Sales_Invoice", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Sales_Invoice", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Sales_Invoice", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID);
			}
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else
				{
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Sales_Invoice", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
					if (text != "")
					{
						LoadData(text);
					}
					else
					{
						ErrorHelper.InformationMessage(UIMessages.DocumentNotFound);
						toolStripTextBoxFind.SelectAll();
						toolStripTextBoxFind.Focus();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void OnActivated()
		{
		}

		private void ItemGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
			else
			{
				dataGridItems.SaveLayout();
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

		private void Form_Load(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					comboBoxPaymentMethod.LoadData();
					comboBoxPaymentMethod.SelectedType = PaymentMethodTypes.Cash;
					SetupGrid();
					labelTaxGroup.Visible = CompanyPreferences.IsTax;
					comboBoxPayeeTaxGroup.Visible = CompanyPreferences.IsTax;
					checkBoxPriceIncludeTax.Visible = false;
					if (!CompanyPreferences.IsTax)
					{
						panelNonTax.Top -= 24;
						panelNonTax.SendToBack();
					}
					UltraFormattedLinkLabel ultraFormattedLinkLabel = labelCurrency;
					bool visible = comboBoxCurrency.Visible = CompanyPreferences.UseMultiCurrency;
					ultraFormattedLinkLabel.Visible = visible;
					mergeMatrixPrint = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.MergeMatrixItems.ToString(), false).ToString());
					comboBoxSysDoc.FilterByType(SysDocTypes.SalesReceipt);
					SetSecurity();
					if (!base.IsDisposed)
					{
						IsNewRecord = true;
						ClearForm();
					}
				}
				catch (Exception e2)
				{
					dataGridItems.LoadLayoutFailed = true;
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDocNumber))
			{
				textBoxVoucherNumber.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.GiveDiscount))
			{
				textBoxDiscountAmount.ReadOnly = true;
				textBoxDiscountPercent.ReadOnly = true;
				textBoxTotal.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityUser(GeneralSecurityRoles.ChangeTransactionRegister))
			{
				comboBoxRegister.ShowDefaultRegisterOnly = true;
			}
			else
			{
				comboBoxRegister.ShowDefaultRegisterOnly = false;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				allowEditTransaction = false;
			}
			else
			{
				allowEditTransaction = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransDiffLocation))
			{
				allowEditTransDiffLocation = false;
			}
			else
			{
				allowEditTransDiffLocation = true;
			}
			totalDiscountPercAllowed = Security.AllowedDiscount(GeneralSecurityRoles.GiveDiscount);
			priceDiscountPercAllowed = Security.AllowedDiscount(GeneralSecurityRoles.AllowPriceDiscount);
			comboBoxPayeeTaxGroup.ReadOnly = !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
			checkBoxPriceIncludeTax.Enabled = Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null)
			{
				comboBoxGridItem.IsLoadingData = false;
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				simpleButtonSave.Enabled = true;
			}
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextDocumentNumber(SystemDocID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord && Factory.SalesInvoiceSystem.InvoiceHasShippedQuantity(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.ErrorMessage("Some items in this invoice has been already delivered. You are not able to modify.");
			}
			else if (ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid) != DialogResult.No)
			{
				if (Void(isVoid: true))
				{
					IsVoid = true;
				}
				else
				{
					ErrorHelper.ErrorMessage("Unable to void the transaction.");
				}
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				return Factory.SalesInvoiceSystem.VoidSalesInvoice(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.SalesReceipt);
		}

		private void LoadCustomerBillingAddress()
		{
			try
			{
				if (!isDataLoading)
				{
					DataSet customerDocumentAddress = Factory.CustomerSystem.GetCustomerDocumentAddress(comboBoxCustomer.SelectedID, "BillToAddressID");
					DataRow dataRow = null;
					if (customerDocumentAddress != null && customerDocumentAddress.Tables.Count > 0 && customerDocumentAddress.Tables[0].Rows.Count > 0)
					{
						dataRow = customerDocumentAddress.Tables[0].Rows[0];
					}
					if (dataRow != null)
					{
						currentCustomerAddressID = dataRow["BillToAddressID"].ToString();
						textBoxBilltoAddress.Text = dataRow["AddressPrintFormat"].ToString();
					}
					else
					{
						textBoxBilltoAddress.Clear();
						currentCustomerAddressID = "";
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonNextBillTo_Click(object sender, EventArgs e)
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				return;
			}
			string nextID = DatabaseHelper.GetNextID("Customer_Address", "AddressID", currentCustomerAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (nextID != "")
			{
				currentCustomerAddressID = nextID;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer_Address", "AddressPrintFormat", "AddressID", nextID, "CustomerID", comboBoxCustomer.SelectedID);
				if (fieldValue != null)
				{
					textBoxBilltoAddress.Text = fieldValue.ToString();
				}
				else
				{
					textBoxBilltoAddress.Clear();
				}
			}
		}

		private void buttonPrevBillto_Click(object sender, EventArgs e)
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				return;
			}
			string previousID = DatabaseHelper.GetPreviousID("Customer_Address", "AddressID", currentCustomerAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (previousID != "")
			{
				currentCustomerAddressID = previousID;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer_Address", "AddressPrintFormat", "AddressID", previousID, "CustomerID", comboBoxCustomer.SelectedID);
				if (fieldValue != null)
				{
					textBoxBilltoAddress.Text = fieldValue.ToString();
				}
				else
				{
					textBoxBilltoAddress.Clear();
				}
			}
		}

		private void CalculateTotal()
		{
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				decimal result3 = default(decimal);
				decimal result4 = default(decimal);
				if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result3);
					num += result3;
					if (!row.Cells["Tax"].Value.IsNullOrEmpty())
					{
						num2 += decimal.Parse(row.Cells["Tax"].Value.ToString());
					}
					decimal.TryParse(row.Cells["Tax"].Value.ToString(), out result4);
					row.Cells["TaxTotal"].Value = result3 + result4;
				}
			}
			textBoxSubtotal.Text = num.ToString(Format.TotalAmountFormat);
			decimal.TryParse(textBoxDiscountPercent.Text, out result2);
			decimal.TryParse(textBoxDiscountAmount.Text, out result);
			if (isDiscountPercent && result2 != 0m)
			{
				result = Math.Round(num * result2 / 100m, Global.CurDecimalPoints);
				textBoxDiscountAmount.Text = result.ToString(Format.TotalAmountFormat);
			}
			else if (num > 0m)
			{
				result2 = Math.Round(result / num * 100m, Global.CurDecimalPoints);
				textBoxDiscountPercent.Text = result2.ToString();
			}
			else
			{
				textBoxDiscountPercent.Text = "0";
				textBoxDiscountAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			num4 = num - result;
			num3 = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
			num4 += num3;
			if (!checkBoxPriceIncludeTax.Checked)
			{
				num4 += num2;
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].Hidden = checkBoxPriceIncludeTax.Checked;
			textBoxTaxAmount.Text = num2.ToString(Format.TotalAmountFormat);
			textBoxTotal.Text = num4.ToString(Format.TotalAmountFormat);
			CalculateTotalTaxes();
		}

		private void CalculateAllRowsTaxes()
		{
			try
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					ItemTaxOptions itemTaxOptions = ItemTaxOptions.BasedOnCustomer;
					if (!row.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						itemTaxOptions = (ItemTaxOptions)byte.Parse(row.Cells["TaxOption"].Value.ToString());
					}
					if (itemTaxOptions == ItemTaxOptions.BasedOnCustomer)
					{
						row.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
					}
					decimal amount = decimal.Parse(row.Cells["Amount"].Value.ToString());
					decimal subtotal = decimal.Parse(textBoxSubtotal.Text);
					decimal tradeDiscount = decimal.Parse(textBoxDiscountAmount.Text);
					TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOptions, row.Cells["TaxGroupID"].Value.ToString());
					row.Cells["Tax"].Tag = tag;
					UIGlobal.CalculateRowTax(row, "Tax", amount, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CalculateTotalTaxes()
		{
			TaxTransactionData taxTransactionData = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID);
			DataTable taxDetailTable = taxTransactionData.TaxDetailTable;
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["Tax"].Tag != null)
				{
					foreach (DataRow row2 in (row.Cells["Tax"].Tag as TaxTransactionData).TaxDetailTable.Rows)
					{
						string text = row2["TaxItemID"].ToString();
						decimal result = default(decimal);
						decimal.TryParse(row2["TaxAmount"].ToString(), out result);
						DataRow[] array = taxDetailTable.Select("TaxItemID  = '" + text + "'");
						if (array.Count() > 0)
						{
							decimal result2 = default(decimal);
							decimal.TryParse(array[0]["TaxAmount"].ToString(), out result2);
							result2 += result;
							array[0]["TaxAmount"] = result2;
						}
						else
						{
							DataRow dataRow = taxDetailTable.NewRow();
							dataRow["TaxItemID"] = text;
							dataRow["TaxAmount"] = result;
							taxDetailTable.Rows.Add(dataRow);
						}
					}
				}
			}
			textBoxTaxAmount.Tag = taxTransactionData;
		}

		private void textBoxDiscountPercent_TextChanged(object sender, EventArgs e)
		{
			if (textBoxDiscountPercent.Focused)
			{
				isDiscountPercent = true;
				CalculateTotal();
			}
		}

		private void textBoxDiscountAmount_TextChanged(object sender, EventArgs e)
		{
			if (textBoxDiscountAmount.Focused)
			{
				isDiscountPercent = false;
				CalculateTotal();
			}
		}

		private void ValidateAllowedDiscount()
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal d = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			num3 = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
			num = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num4 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			num5 = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
			num2 = num + num4 - num3 + num5;
			num2 = decimal.Parse(textBoxDiscountAmount.Text, NumberStyles.Any);
			if (num2 < 0m)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "Total amount cannot be greater than the subtotal.", "Please enter a numeric value less or equal to the subtotal.");
				return;
			}
			_ = (num3 < 0m);
			if (num > 0m)
			{
				d = Math.Round(num2 / num * 100m, Global.CurDecimalPoints);
				textBoxDiscountPercent.Text = d.ToString();
			}
			else
			{
				textBoxDiscountPercent.Text = "0";
			}
			if (totalDiscountPercAllowed < d && !showednotdiscount && totalDiscountPercAllowed > 0m && !Global.IsUserAdmin)
			{
				showednotdiscount = true;
				ErrorHelper.WarningMessage("Higher Discount is not applicable.", "Please enter a proper discount.");
			}
		}

		private void textBoxTotal_TextChanged(object sender, EventArgs e)
		{
			if (textBoxTotal.Focused)
			{
				totalChanged = true;
			}
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			if (!(textBoxTotal.Text != "") || comboBoxPaymentMethod.SelectedType != PaymentMethodTypes.CreditCard)
			{
				return;
			}
			if (textboxPercent.Text != "")
			{
				num2 = decimal.Parse(textBoxTotal.Text);
				_ = decimal.Parse(textboxPercent.Text) / 100m * num2;
			}
			else if (textBoxExpAmount.Text != "")
			{
				num2 = decimal.Parse(textBoxTotal.Text);
				num = decimal.Parse(textBoxExpAmount.Text);
				if (num2 > 0m)
				{
					decimal num3 = num / num2 * 100m;
					textboxPercent.Text = num3.ToString(Format.PercentageFormat);
				}
			}
		}

		private void textBoxTotal_Validating(object sender, CancelEventArgs e)
		{
			if (!totalChanged)
			{
				return;
			}
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			decimal num6 = default(decimal);
			num4 = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
			num = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num5 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			num6 = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
			num2 = num - num4 + num6 + num5;
			if (num2 < 0m)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "Total amount cannot be greater than the subtotal.", "Please enter a numeric value less or equal to the subtotal.");
				e.Cancel = true;
				return;
			}
			_ = (num4 < 0m);
			textBoxDiscountAmount.Text = num2.ToString(Format.TotalAmountFormat);
			if (num > 0m)
			{
				num3 = Math.Round(num2 / num * 100m, Global.CurDecimalPoints);
				textBoxDiscountPercent.Text = num3.ToString();
			}
			else
			{
				textBoxDiscountPercent.Text = "0";
			}
		}

		private void textboxPercent_Validated(object sender, EventArgs e)
		{
			if (textBoxTotal.Text != "" && textboxPercent.Text != "")
			{
				Amount = decimal.Parse(textBoxTotal.Text);
				decimal num = decimal.Parse(textboxPercent.Text) / 100m * Amount;
				textBoxExpAmount.Text = num.ToString(Format.GridAmountFormat);
			}
		}

		private void textBoxExpAmount_Validated(object sender, EventArgs e)
		{
			if (textBoxTotal.Text != "" && textBoxExpAmount.Text != "")
			{
				Amount = decimal.Parse(textBoxTotal.Text);
				decimal d = decimal.Parse(textBoxExpAmount.Text);
				if (Amount > 0m)
				{
					decimal num = d / Amount * 100m;
					textboxPercent.Text = num.ToString(Format.GridAmountFormat);
				}
			}
		}

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				dataGridItems.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void availableQuantityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				FormActivator.ProductQuantityFormObj.LoadData(productID);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void itemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string id = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				new FormHelper().EditItem(id);
			}
		}

		private void itemPicToolStripMenuItem_Click(object sender, EventArgs e)
		{
			checked
			{
				if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
				{
					string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					productPhotoViewer.ShowImage(productID, dataGridItems.Left + 20, dataGridItems.Top + 20);
				}
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomerAddress(comboBoxCustomer.SelectedID, comboBoxShippingAddressID.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomerAddress(comboBoxCustomer.SelectedID, comboBoxBillingAddress.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomer(comboBoxCustomer.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxSalesperson.SelectedID);
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to duplicate this document?") == DialogResult.Yes)
			{
				string text = textBoxVoucherNumber.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				EnableButton();
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			duplicateToolStripMenuItem.Enabled = !IsNewRecord;
		}

		private void numberTextBox1_TextChanged(object sender, EventArgs e)
		{
			if (textBoxTaxAmount.Focused)
			{
				isTaxPercent = false;
				CalculateTotal();
			}
		}

		private void textBoxTaxPercent_TextChanged(object sender, EventArgs e)
		{
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet salesInvoiceToPrint = Factory.SalesInvoiceSystem.GetSalesInvoiceToPrint(selectedID, text, mergeMatrixPrint, showLotDetail);
					if (salesInvoiceToPrint == null || salesInvoiceToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(salesInvoiceToPrint, selectedID, "Cash Sale", SysDocTypes.SalesReceipt, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			if (!comboBoxSysDoc.Enabled && sysDocID != comboBoxSysDoc.SelectedID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.SelectedSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.BringFormToFront(FormActivator.SalesReceiptListFormObj);
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			DocumentInformationDialog documentInformationDialog = new DocumentInformationDialog();
			documentInformationDialog.VoucherID = textBoxVoucherNumber.Text;
			documentInformationDialog.SysDocID = comboBoxSysDoc.SelectedID;
			documentInformationDialog.ShowDialog(this);
		}

		private void salesStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string selectedID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				InventorySalesStatisticForm inventorySalesStatisticForm = new InventorySalesStatisticForm();
				inventorySalesStatisticForm.SelectedID = selectedID;
				inventorySalesStatisticForm.Show();
				inventorySalesStatisticForm.BringToFront();
			}
		}

		private void panelDetails_Paint(object sender, PaintEventArgs e)
		{
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityName = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonMultiPreview_Click(object sender, EventArgs e)
		{
			AttachementDetailsForm attachementDetailsForm = new AttachementDetailsForm();
			attachementDetailsForm.SysDocID = comboBoxSysDoc.SelectedID;
			attachementDetailsForm.VoucherID = textBoxVoucherNumber.Text;
			attachementDetailsForm.SysDocType = SysDocTypes.SalesReceipt;
			attachementDetailsForm.FormType = "Transaction";
			attachementDetailsForm.Show();
		}

		private void labelCurrency_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(comboBoxCurrency.SelectedID);
		}

		private void ultraFormattedLinkPaymentMethod_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPaymentMethod(comboBoxPaymentMethod.SelectedType.ToString());
		}

		private void ultraFormattedLinkLabelShipment_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditShippingMethod(comboBoxShippingMethod.SelectedID);
		}

		private void labelJob_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJob(comboBoxJob.SelectedID);
		}

		private void labelcostcategory_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCostCategory(comboBoxCostCategory.SelectedID);
		}

		private void createFromTransactionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet itemTransactionList = Factory.ItemTransactionSystem.GetItemTransactionList("", SysDocTypes.SalesReceipt);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = itemTransactionList;
			selectDocumentDialog.Text = "Select Transaction";
			selectDocumentDialog.IsMultiSelect = false;
			selectDocumentDialog.ValidateSelection += formSelectPO_ValidateSelection;
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			ItemTransactionData itemTransactionByID = Factory.ItemTransactionSystem.GetItemTransactionByID(text, text2);
			DataRow dataRow = itemTransactionByID.ItemTransactionTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			if (comboBoxCustomer.SelectedID == "")
			{
				comboBoxCustomer.SelectedID = dataRow["PartyID"].ToString();
			}
			dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
			comboBoxSysDoc.SelectedID = text;
			textBoxVoucherNumber.Text = text2;
			comboBoxPaymentMethod.SelectedIndex = 3;
			decimal result = default(decimal);
			decimal.TryParse(dataRow["Discount"].ToString(), out result);
			textBoxDiscountAmount.Text = result.ToString();
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			DataTable dataTable2 = dataTable.Copy();
			dataTable2 = new DataTable();
			if (SetTransasctionTable(dataTable2, itemTransactionByID.ItemTransactionDetailTable).Rows.Count > 0)
			{
				dataTable.Rows.Clear();
				_ = selectDocumentDialog.SelectedRows;
				foreach (DataRow row in dtTransaction.Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["Item Code"] = row["Item Code"];
					dataRow3["ISPORRow"] = true;
					if (row["Quantity"] != DBNull.Value)
					{
						dataRow3["Quantity"] = row["Quantity"];
					}
					else
					{
						dataRow3["Quantity"] = row["Quantity"];
					}
					dataRow3["Job"] = row["Job"];
					dataRow3["ListSysDocID"] = row["ListSysDocID"];
					dataRow3["ListVoucherID"] = row["ListVoucherID"];
					dataRow3["ListRowIndex"] = row["ListRowIndex"];
					dataRow3["Description"] = row["Description"];
					dataRow3["Price"] = row["UnitPrice"];
					decimal result2 = default(decimal);
					decimal num = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result2);
					decimal.TryParse(row["Ordered"].ToString(), out result3);
					dataRow3["Ordered"] = result3;
					dataRow3["Received"] = num;
					result2 -= num;
					dataRow3["Quantity"] = result2;
					if (result2 < 0m)
					{
						result2 = default(decimal);
					}
					dataRow3.EndEdit();
					dataTable.Rows.Add(dataRow3);
				}
				return;
			}
			dataTable.Rows.Clear();
			if (itemTransactionByID.Tables.Contains("Item_Transaction_Detail") && itemTransactionByID.ItemTransactionDetailTable.Rows.Count != 0)
			{
				_ = selectDocumentDialog.SelectedRows;
				foreach (DataRow row2 in itemTransactionByID.Tables["Item_Transaction_Detail"].Rows)
				{
					DataRow dataRow5 = dataTable.NewRow();
					dataRow5["Item Code"] = row2["ProductID"];
					if (row2["Quantity"] != DBNull.Value)
					{
						dataRow5["Quantity"] = row2["Quantity"];
					}
					dataRow5["Description"] = row2["Description"];
					dataRow5["ListSysDocID"] = row2["SysDocID"];
					dataRow5["ListVoucherID"] = row2["VoucherID"];
					dataRow5["ListRowIndex"] = row2["RowIndex"];
					dataRow5["Location"] = dataRow["LocationID"].ToString();
					dataRow5["Unit"] = row2["UnitID"];
					decimal productSalesPrice = Factory.ProductSystem.GetProductSalesPrice(row2["ProductID"].ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, row2["UnitID"].ToString());
					dataRow5["Price"] = productSalesPrice;
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					decimal result6 = default(decimal);
					decimal.TryParse(dataRow5["Quantity"].ToString(), out result4);
					decimal.TryParse(dataRow5["Price"].ToString(), out result5);
					decimal.TryParse(dataRow5["Discount"].ToString(), out result6);
					dataRow5["Amount"] = Math.Round(result4 * (result5 - result6), Global.CurDecimalPoints);
					dataRow5.EndEdit();
					dataTable.Rows.Add(dataRow5);
				}
				CalculateTotal();
			}
		}

		private void formSelectPO_ValidateSelection(object sender, EventArgs e)
		{
		}

		private void simpleButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Temporary_Transaction", "SKey", "SName", 26.ToString(), "SysDocID", comboBoxSysDoc.SelectedID);
			if (!(lastID == ""))
			{
				string key = lastID;
				if (!string.IsNullOrEmpty(lastID))
				{
					currentkey = lastID;
				}
				DataSet dataSet = Global.CompanySettings.LoadTransactionDraftForTemporary(key, SysDocTypes.SalesReceipt);
				currentData = (dataSet as SalesInvoiceData);
				IsTemporaryTransaction = true;
				comboBoxSysDoc.ReadOnly = true;
				TempAutoKeyID = Factory.SettingSystem.GetTemporaryAutoKeyID(key);
				FillData();
			}
		}

		private void GetUpdatedDocumentNo()
		{
			IsTempRecordexists = Factory.SystemDocumentSystem.ExistDocumentNumber("Temporary_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text);
			IsDocExists = Factory.SystemDocumentSystem.ExistDocumentNumber("Sales_Invoice", "VoucherID", SystemDocID, textBoxVoucherNumber.Text);
			if ((IsTempRecordexists || IsDocExists) && IsNewRecord && !IsTemporaryTransaction)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
		}

		private bool SaveDraft()
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
			try
			{
				GetUpdatedDocumentNo();
				if (!hideRegister && comboBoxRegister.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				if (!ValidateData())
				{
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				if (GetData())
				{
					string text = "";
					text = SystemDocID + textBoxVoucherNumber.Text + "-" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToLongTimeString() + "-" + Global.CurrentUser;
					return Global.CompanySettings.SaveTransactionDraftForDashBoard(currentData, text, SysDocTypes.SalesReceipt, SystemDocID, textBoxVoucherNumber.Text, TempAutoKeyID, isNewRecord);
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool LoadDraft()
		{
			try
			{
				DataSet dataSet = (Global.IsUserAdmin || !(Global.DefaultLocationID != "") || Global.DefaultLocationID == null) ? Factory.SettingSystem.GetSettingsListData("", 26.ToString()) : Factory.SettingSystem.GetSettingsListData(Global.CurrentUser, 26.ToString());
				DataTable dataTable = dataSet.Tables[0].DefaultView.ToTable(true, "SysDocID", "VoucherID", "CustomerID", "TransactionDate", "AutoKeyID");
				if (dataTable == null && dataTable.Rows.Count == 0)
				{
					return false;
				}
				DataSet dataSet2 = new DataSet();
				dataSet2.Tables.Add(dataTable);
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = dataSet2;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = currentkey = Factory.SettingSystem.GetTempTransactionByKey(selectDocumentDialog.SelectedRow.Cells["SysDocID"].Value.ToString() + selectDocumentDialog.SelectedRow.Cells["VoucherID"].Value.ToString());
					DataSet dataSet3 = Global.CompanySettings.LoadTransactionDraftForTemporary(key, SysDocTypes.SalesReceipt);
					comboBoxSysDoc.SelectedID = selectDocumentDialog.SelectedRow.Cells["SysDocID"].Value.ToString();
					TempAutoKeyID = int.Parse(selectDocumentDialog.SelectedRow.Cells["AutoKeyID"].Value.ToString());
					currentData = (dataSet3 as SalesInvoiceData);
					IsTemporaryTransaction = true;
					comboBoxSysDoc.ReadOnly = true;
					FillData();
					buttonSave.Enabled = true;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void loadDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CanClose())
			{
				LoadDraft();
			}
		}

		private void simpleButtonNext_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("Temporary_Transaction", "SKey", currentkey, "SName", 26.ToString(), "SysDocID", comboBoxSysDoc.SelectedID);
			if (!(nextID == "") && !(currentkey == ""))
			{
				string key = nextID;
				if (!string.IsNullOrEmpty(nextID))
				{
					currentkey = nextID;
				}
				DataSet dataSet = Global.CompanySettings.LoadTransactionDraftForTemporary(key, SysDocTypes.SalesReceipt);
				currentData = (dataSet as SalesInvoiceData);
				IsTemporaryTransaction = true;
				comboBoxSysDoc.ReadOnly = true;
				TempAutoKeyID = Factory.SettingSystem.GetTemporaryAutoKeyID(key);
				FillData();
			}
		}

		private void simpleButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Temporary_Transaction", "SKey", "SName", 26.ToString(), "SysDocID", comboBoxSysDoc.SelectedID);
			if (!(firstID == ""))
			{
				string key = firstID;
				if (!string.IsNullOrEmpty(firstID))
				{
					currentkey = firstID;
				}
				DataSet dataSet = Global.CompanySettings.LoadTransactionDraftForTemporary(key, SysDocTypes.SalesReceipt);
				currentData = (dataSet as SalesInvoiceData);
				IsTemporaryTransaction = true;
				comboBoxSysDoc.ReadOnly = true;
				TempAutoKeyID = Factory.SettingSystem.GetTemporaryAutoKeyID(key);
				FillData();
			}
		}

		private void simpleButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Temporary_Transaction", "SKey", currentkey, "SName", 26.ToString(), "SysDocID", comboBoxSysDoc.SelectedID);
			if (!(previousID == "") && !(currentkey == ""))
			{
				string key = previousID;
				if (!string.IsNullOrEmpty(previousID))
				{
					currentkey = previousID;
				}
				DataSet dataSet = Global.CompanySettings.LoadTransactionDraftForTemporary(key, SysDocTypes.SalesReceipt);
				currentData = (dataSet as SalesInvoiceData);
				IsTemporaryTransaction = true;
				comboBoxSysDoc.ReadOnly = true;
				TempAutoKeyID = Factory.SettingSystem.GetTemporaryAutoKeyID(key);
				FillData();
			}
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			if (SaveDraft())
			{
				ClearForm();
			}
		}

		private void simpleButtonFind_Click(object sender, EventArgs e)
		{
			LoadDraft();
			formManager.IsForcedDirty = false;
		}

		private void comboBoxCustomer_TabIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxCustomer_Leave(object sender, EventArgs e)
		{
		}

		private DataTable SetTransasctionTable(DataTable DtDetails, DataTable DtTransactionDetails)
		{
			List<translist> list = new List<translist>();
			decimal num = default(decimal);
			string text = "";
			string text2 = "";
			int num2 = 0;
			checked
			{
				for (int i = 0; i < DtTransactionDetails.Rows.Count; i++)
				{
					num = decimal.Parse(DtTransactionDetails.Rows[i]["Quantity"].ToString());
					text = DtTransactionDetails.Rows[i]["Item Code"].ToString();
					DtTransactionDetails.Rows[i]["SysDocID"].ToString();
					DtTransactionDetails.Rows[i]["VoucherID"].ToString();
					text2 = DtTransactionDetails.Rows[i]["Description"].ToString();
					num2 = int.Parse(DtTransactionDetails.Rows[i]["Rowindex"].ToString());
					list.Add(new translist(text, num, Istaken: false, text2, num2));
				}
				for (int j = 0; j < DtDetails.Rows.Count; j++)
				{
					text = DtDetails.Rows[j]["Item Code"].ToString();
					decimal num3 = default(decimal);
					string text3 = "";
					string text4 = "";
					text3 = DtTransactionDetails.Rows[1]["SysDocID"].ToString();
					text4 = DtTransactionDetails.Rows[1]["VoucherID"].ToString();
					foreach (translist item in list)
					{
						DataView defaultView = DtDetails.DefaultView;
						defaultView.RowFilter = "[Item Code]='" + item.ItemCode.ToString() + "'";
						DataSet dataSet = new DataSet();
						DataTable dataTable = defaultView.ToTable();
						dataSet.Tables.Add(dataTable);
						if (item.ItemCode == text && !item.Istaken)
						{
							decimal.Parse(DtDetails.Rows[j]["Ordered"].ToString());
							num = item.Trqty;
							num3 = num;
							DtDetails.Rows[j]["Quantity"] = num3;
							DtDetails.Rows[j]["ListSysDocID"] = text3;
							DtDetails.Rows[j]["ListVoucherID"] = text4;
							DtDetails.Rows[j]["ListRowIndex"] = item.Trindex;
							DtDetails.Rows[j]["RowDocType"] = ItemSourceTypes.None;
							DtDetails.AcceptChanges();
							item.Istaken = true;
						}
						else if (!item.ItemCode.Contains(text) && !item.Istaken && dataTable.Rows.Count == 0)
						{
							DataRow dataRow = DtDetails.NewRow();
							dataRow["Item Code"] = item.ItemCode;
							dataRow["RowDocType"] = ItemSourceTypes.None;
							dataRow["ListSysDocID"] = text3;
							dataRow["ListVoucherID"] = text4;
							dataRow["Description"] = item.ItemDescription;
							dataRow["ListRowIndex"] = item.Trindex;
							dataRow["Quantity"] = item.Trqty;
							dataRow.EndEdit();
							DtDetails.Rows.Add(dataRow);
							DtDetails.AcceptChanges();
							item.Istaken = true;
						}
					}
					DtDetails.AcceptChanges();
				}
				DtDetails.AcceptChanges();
				dtTransaction = DtDetails;
				return dtTransaction;
			}
		}

		private void allowUpdateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			List<string> selectedCodes = new List<string>();
			dataSet = Factory.UserSystem.GetUserComboList();
			bool flag = false;
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = dataSet;
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.SelectedCodes = selectedCodes;
			selectDocumentDialog.Text = "Select User";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				selectedCodes = selectDocumentDialog.SelectedCodes;
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					string text = selectedRow.Cells["Code"].Value.ToString();
					string name = selectedRow.Cells["Name"].Value.ToString();
					if (0 == 0)
					{
						new NameValue(name, text);
						flag = Factory.SalesInvoiceSystem.ModifyTransactions(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, text, isModify: true, "");
					}
				}
				if (flag)
				{
					ErrorHelper.WarningMessage("User can able to modify");
				}
			}
		}

		private void textBoxExpAmount_TextChanged(object sender, EventArgs e)
		{
		}

		private void labelExpAmount_Click(object sender, EventArgs e)
		{
		}

		private void comboBoxPaymentMethod_ValueChanged(object sender, EventArgs e)
		{
			if (comboBoxPaymentMethod.SelectedType == PaymentMethodTypes.CreditCard)
			{
				labelExpenseCode.ReadOnly = false;
				comboBoxExpenseCode.ReadOnly = false;
				comboBoxExpenseCode.ExpenseCodeType = ExpenseCodeTypes.BankFee;
				textboxPercent.ReadOnly = true;
				textBoxExpAmount.ReadOnly = false;
				panelExpense.Visible = true;
			}
			else
			{
				comboBoxExpenseCode.ReadOnly = true;
				textboxPercent.ReadOnly = true;
				textBoxExpAmount.ReadOnly = true;
				textboxPercent.Text = "";
				textBoxExpAmount.Text = "";
				comboBoxExpenseCode.Clear();
				panelExpense.Visible = false;
			}
		}

		private void checkedListBoxPI_DoubleClick(object sender, EventArgs e)
		{
			NameValue nameValue = checkedListBoxPI.SelectedItem as NameValue;
			if (nameValue != null)
			{
				new FormHelper().EditTransaction(nameValue.ID, nameValue.Name);
			}
		}

		private void comboBoxPayeeTaxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			CalculateAllRowsTaxes();
			CalculateTotal();
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxPayeeTaxGroup.SelectedID);
		}

		private void textBoxDiscountAmount_Leave(object sender, EventArgs e)
		{
			showednotdiscount = false;
			ValidateAllowedDiscount();
		}

		private void textBoxDiscountPercent_Leave(object sender, EventArgs e)
		{
			showednotdiscount = false;
			ValidateAllowedDiscount();
		}

		private void checkBoxPriceIncludeTax_CheckedChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				CalculateAllRowsTaxes();
				CalculateTotal();
			}
		}

		private void createFromSalesQuoteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet dataSet = new DataSet();
			if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.DefaultLocationSales) && !Global.isUserAdmin)
			{
				string defaultLocationID = Global.DefaultLocationID;
				dataSet = Factory.SalesQuoteSystem.GetOpenQuotesSummary(comboBoxSysDoc.SelectedID, comboBoxCustomer.SelectedID, defaultLocationID);
			}
			else
			{
				dataSet = Factory.SalesQuoteSystem.GetOpenQuotesSummary(comboBoxCustomer.SelectedID);
			}
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = dataSet;
			selectDocumentDialog.Text = "Select Sales Quote";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				ClearForm();
				string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				sourceDocType = ItemSourceTypes.None;
				SalesQuoteData salesQuoteByID = Factory.SalesQuoteSystem.GetSalesQuoteByID(text, text2);
				DataRow dataRow = salesQuoteByID.SalesQuoteTable.Rows[0];
				textBoxRef1.Text = dataRow["VoucherID"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				if (comboBoxCustomer.SelectedID == "")
				{
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
				}
				textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
				comboBoxCustomer.Enabled = false;
				sourceDocType = ItemSourceTypes.None;
				if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
				{
					comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
				}
				if (!string.IsNullOrEmpty(dataRow["CurrencyID"].ToString()))
				{
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
				}
				if (!string.IsNullOrEmpty(dataRow["ShippingAddressID"].ToString()))
				{
					comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
				}
				if (!string.IsNullOrEmpty(dataRow["SalespersonID"].ToString()))
				{
					comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
				}
				if (!string.IsNullOrEmpty(dataRow["JobID"].ToString()))
				{
					comboBoxJob.SelectedID = dataRow["JobID"].ToString();
				}
				if (!string.IsNullOrEmpty(dataRow["CostCategoryID"].ToString()))
				{
					comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
				}
				if (!string.IsNullOrEmpty(dataRow["PayeeTaxGroupID"].ToString()))
				{
					comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
				}
				DataSet dataSet2 = new DataSet();
				dataSet2.Merge(salesQuoteByID.SalesQuoteDetailTable, preserveChanges: true, MissingSchemaAction.Add);
				textBoxPONumber.Text = dataRow["PONumber"].ToString();
				textBoxDiscountAmount.Text = dataRow["Discount"].ToString();
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in dataSet2.Tables[0].Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["SOSysDocID"] = text;
					dataRow3["SOVoucherID"] = text2;
					dataRow3["SORowIndex"] = row["RowIndex"];
					dataRow3["Item Code"] = row["ProductID"].ToString();
					dataRow3["Quantity"] = row["Quantity"].ToString();
					dataRow3["ItemType"] = row["ItemType"].ToString();
					dataRow3["Description"] = row["Description"].ToString();
					dataRow3["Unit"] = row["UnitID"].ToString();
					dataRow3["Price"] = row["UnitPrice"].ToString();
					dataRow3["Amount"] = double.Parse(row["Quantity"].ToString()) * double.Parse(row["UnitPrice"].ToString());
					if (!row["TaxOption"].IsNullOrEmpty())
					{
						dataRow3["TaxOption"] = row["TaxOption"].ToString();
					}
					else
					{
						dataRow3["TaxOption"] = (byte)2;
					}
					dataRow3["TaxGroupID"] = row["TaxGroupID"];
					dataRow3.EndEdit();
					dataTable.Rows.Add(dataRow3);
				}
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					ItemTypes itemTypes = ItemTypes.Inventory;
					if (row2.Cells["ItemType"].Value.ToString() != "")
					{
						itemTypes = (ItemTypes)checked((byte)int.Parse(row2.Cells["ItemType"].Value.ToString()));
					}
					if (itemTypes == ItemTypes.Discount)
					{
						row2.Cells["Quantity"].Activation = Activation.Disabled;
					}
				}
				CalculateAllRowsTaxes();
				CalculateTotal();
				textBoxDiscountPercent.Modified = true;
				textBoxDiscountAmount_Validated(sender, e);
				ValidationwithColor();
			}
		}

		private void comboBoxSalesperson_SelectedIndexChanged(object sender, EventArgs e)
		{
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Salesperson", "ReportTo", "SalespersonID", comboBoxSalesperson.SelectedID);
			if (fieldValue != null)
			{
				labelReportTo.Text = fieldValue.ToString();
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void comboBoxVehicle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxVehicle.SelectedID == "")
			{
				textBoxVechicleName.ReadOnly = false;
			}
			else
			{
				textBoxVechicleName.ReadOnly = true;
			}
		}

		private void linkLabelVehicle_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVehicle(comboBoxVehicle.SelectedID);
		}

		private void linkLabelDriver_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditDriver(comboBoxDriver.SelectedID);
		}

		private void createFromSalesOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet openOrdersSummary = Factory.SalesOrderSystem.GetOpenOrdersSummary(comboBoxCustomer.SelectedID, isExport: false);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openOrdersSummary;
			selectDocumentDialog.Text = "Select Sales Order";
			selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["JobName"].Hidden = !useJobCosting;
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			refDocID = text;
			refVoucherID = text2;
			timeStampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("Sales_Order", refDocID, refVoucherID);
			refDateTime = DateTime.Parse(timeStampStatus.Tables[0].Rows[0]["TimeStamp"].ToString());
			SalesOrderData salesOrderByID = Factory.SalesOrderSystem.GetSalesOrderByID(text, text2);
			DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(salesOrderByID, SysDocTypes.SalesOrder, Global.CurrentUser, includeApproveUser: true);
			if (entityApprovalStatus.Tables[0].Rows.Count > 0)
			{
				bool.TryParse(entityApprovalStatus.Tables[0].Rows[0]["AllownextTransaction"].ToString(), out restrictTransaction);
				int num = int.Parse(entityApprovalStatus.Tables[0].Rows[0]["ApprovalStatus"].ToString());
				if (restrictTransaction && num != 10)
				{
					restrictTransaction = true;
				}
				else
				{
					restrictTransaction = false;
				}
				if (restrictTransaction)
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
				}
			}
			DataRow dataRow = salesOrderByID.SalesOrderTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			if (comboBoxCustomer.SelectedID == "")
			{
				comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
			}
			textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
			comboBoxCustomer.Enabled = false;
			sourceDocType = ItemSourceTypes.SalesOrder;
			if (!string.IsNullOrEmpty(dataRow["CurrencyID"].ToString()))
			{
				comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
			{
				comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingAddressID"].ToString()))
			{
				comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["SalespersonID"].ToString()))
			{
				comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["JobID"].ToString()))
			{
				comboBoxJob.SelectedID = dataRow["JobID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["CostCategoryID"].ToString()))
			{
				comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
			}
			textBoxPONumber.Text = dataRow["PONumber"].ToString();
			textBoxDiscountAmount.Text = decimal.Parse(dataRow["Discount"].ToString()).ToString(Format.TotalAmountFormat);
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if (!dataTable.Columns.Contains("Ordered"))
			{
				dataTable.Columns.Remove("Quantity");
				dataTable.Columns.Remove("Amount");
				dataTable.Columns.Remove("Price");
				dataTable.Columns.Add("Ordered", typeof(decimal));
				dataTable.Columns.Add("Shipped", typeof(decimal));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				if (!dataTable.Columns.Contains("SOSysDocID"))
				{
					dataTable.Columns.Add("SOSysDocID");
					dataTable.Columns.Add("SOVoucherID");
					dataTable.Columns.Add("SORowIndex", typeof(int));
				}
			}
			dataTable.Rows.Clear();
			if (!salesOrderByID.Tables.Contains("Sales_Order_Detail") || salesOrderByID.SalesOrderDetailTable.Rows.Count == 0)
			{
				return;
			}
			foreach (DataRow row in salesOrderByID.Tables["Sales_Order_Detail"].Rows)
			{
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["Item Code"] = row["ProductID"];
				dataRow3["SOSysDocID"] = text;
				dataRow3["SOVoucherID"] = text2;
				dataRow3["SORowIndex"] = row["RowIndex"];
				if (row["UnitQuantity"] != DBNull.Value)
				{
					dataRow3["Quantity"] = row["UnitQuantity"];
				}
				else
				{
					dataRow3["Quantity"] = row["Quantity"];
				}
				dataRow3["ItemType"] = row["ItemType"];
				dataRow3["RowSourceType"] = ItemSourceTypes.SalesOrder;
				dataRow3["Attribute1"] = row["Attribute1"];
				dataRow3["Attribute2"] = row["Attribute2"];
				dataRow3["Attribute3"] = row["Attribute3"];
				dataRow3["MatrixParentID"] = row["MatrixParentID"];
				dataRow3["Description"] = row["Description"];
				dataRow3["Remarks"] = row["Remarks"];
				dataRow3["TaxGroupID"] = row["TaxGroupID"];
				dataRow3["Location"] = Security.DefaultInventoryLocationID;
				dataRow3["Unit"] = row["UnitID"];
				if (row["SubunitPrice"] != DBNull.Value)
				{
					dataRow3["Price"] = decimal.Parse(row["SubunitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				else
				{
					dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				decimal num2 = default(decimal);
				decimal d = default(decimal);
				decimal num3 = default(decimal);
				if (dataRow3["Quantity"].ToString() != "")
				{
					num2 = decimal.Parse(dataRow3["Quantity"].ToString(), NumberStyles.Any);
				}
				if (dataRow3["Price"].ToString() != "")
				{
					d = decimal.Parse(dataRow3["Price"].ToString(), NumberStyles.Any);
				}
				if (row["QuantityShipped"].ToString() != "")
				{
					num3 = decimal.Parse(row["QuantityShipped"].ToString(), NumberStyles.Any);
				}
				if (row["TaxOption"] != DBNull.Value)
				{
					dataRow3["TaxOption"] = byte.Parse(row["TaxOption"].ToString());
				}
				else
				{
					dataRow3["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
				}
				if (row["TaxAmount"] != DBNull.Value)
				{
					dataRow3["Tax"] = decimal.Parse(row["TaxAmount"].ToString()).ToString(Format.UnitPriceFormat);
				}
				if (!row["TaxGroupID"].IsDBNullOrEmpty())
				{
					dataRow3["TaxGroupID"] = row["TaxGroupID"];
				}
				dataRow3["Ordered"] = num2;
				dataRow3["Shipped"] = num3;
				num2 -= num3;
				dataRow3["Quantity"] = num2;
				dataRow3["Amount"] = Math.Round(num2 * d, Global.CurDecimalPoints);
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			foreach (UltraGridRow row2 in dataGridItems.Rows)
			{
				ItemTypes itemTypes = ItemTypes.Inventory;
				if (row2.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(row2.Cells["ItemType"].Value.ToString()));
				}
				if (itemTypes == ItemTypes.Discount)
				{
					row2.Cells["Quantity"].Activation = Activation.Disabled;
				}
			}
			if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenInvoiceThenDN)
			{
				if (!CompanyPreferences.AllowLSaleAddNew)
				{
					dataGridItems.AllowAddNew = false;
				}
				else
				{
					dataGridItems.AllowAddNew = true;
				}
			}
			AdjustGridColumnSettings();
			CalculateAllRowsTaxes();
			CalculateTotal();
			textBoxDiscountPercent.Modified = true;
			textBoxDiscountAmount_Validated(sender, e);
		}

		private void linkLabelTax_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				TaxDistibutionDialog taxDistibutionDialog = new TaxDistibutionDialog();
				TaxTransactionData taxData = new TaxTransactionData();
				if (textBoxTaxAmount.Tag != null)
				{
					taxData = (textBoxTaxAmount.Tag as TaxTransactionData);
				}
				taxDistibutionDialog.TaxData = taxData;
				taxDistibutionDialog.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void deleteTempToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (currentkey == "")
			{
				ErrorHelper.WarningMessage("No Temporary transaction has selected!");
			}
			if (!string.IsNullOrEmpty(currentkey) && Factory.SettingSystem.DeleteSettingTemporary(currentkey, "", ""))
			{
				ClearForm();
				ErrorHelper.WarningMessage("Temporary transaction Deleted Succesfully!");
			}
		}

		private void textBoxRoundOff_Leave(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void createFromProformaInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet openOrdersSummary = Factory.SalesProformaSystem.GetOpenOrdersSummary(comboBoxCustomer.SelectedID, isExport: false);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openOrdersSummary;
			selectDocumentDialog.Text = "Select Sales Proforma Invoice";
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			refDocID = text;
			refVoucherID = text2;
			checkedListBoxPI.Items.Clear();
			NameValue item = new NameValue(text2, text);
			checkedListBoxPI.Items.Add(item);
			timestampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("SalesProforma_Invoice", refDocID, refVoucherID);
			refDateTime = DateTime.Parse(timestampStatus.Tables[0].Rows[0]["TimeStamp"].ToString());
			SalesProformaInvoiceData salesOrderByID = Factory.SalesProformaSystem.GetSalesOrderByID(text, text2);
			DataRow dataRow = salesOrderByID.SalesProformaTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
			textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
			comboBoxCustomer.Enabled = false;
			sourceDocType = ItemSourceTypes.SalesProformaInvoice;
			if (!string.IsNullOrEmpty(dataRow["CurrencyID"].ToString()))
			{
				comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
			{
				comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingAddressID"].ToString()))
			{
				comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["SalespersonID"].ToString()))
			{
				comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["JobID"].ToString()))
			{
				comboBoxJob.SelectedID = dataRow["JobID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["CostCategoryID"].ToString()))
			{
				comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
			}
			textBoxPONumber.Text = dataRow["PONumber"].ToString();
			textBoxDiscountAmount.Text = decimal.Parse(dataRow["Discount"].ToString()).ToString(Format.TotalAmountFormat);
			if (!string.IsNullOrEmpty(dataRow["PayeeTaxGroupID"].ToString()))
			{
				comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
			}
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if (!dataTable.Columns.Contains("Ordered"))
			{
				dataTable.Columns.Remove("Quantity");
				dataTable.Columns.Remove("Amount");
				dataTable.Columns.Remove("Price");
				dataTable.Columns.Add("Ordered", typeof(decimal));
				dataTable.Columns.Add("Shipped", typeof(decimal));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				if (!dataTable.Columns.Contains("SOSysDocID"))
				{
					dataTable.Columns.Add("SOSysDocID");
					dataTable.Columns.Add("SOVoucherID");
					dataTable.Columns.Add("SORowIndex", typeof(int));
				}
			}
			dataTable.Rows.Clear();
			if (!salesOrderByID.Tables.Contains("SalesProforma_Invoice_Detail") || salesOrderByID.SalesProformaDetailTable.Rows.Count == 0)
			{
				return;
			}
			foreach (DataRow row in salesOrderByID.Tables["SalesProforma_Invoice_Detail"].Rows)
			{
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["Item Code"] = row["ProductID"];
				dataRow3["SOSysDocID"] = text;
				dataRow3["SOVoucherID"] = text2;
				dataRow3["SORowIndex"] = row["RowIndex"];
				if (row["UnitQuantity"] != DBNull.Value)
				{
					dataRow3["Quantity"] = row["UnitQuantity"];
				}
				else
				{
					dataRow3["Quantity"] = row["Quantity"];
				}
				dataRow3["ItemType"] = row["ItemType"];
				dataRow3["RowSourceType"] = ItemSourceTypes.SalesProformaInvoice;
				dataRow3["Attribute1"] = row["Attribute1"];
				dataRow3["Attribute2"] = row["Attribute2"];
				dataRow3["Attribute3"] = row["Attribute3"];
				dataRow3["MatrixParentID"] = row["MatrixParentID"];
				dataRow3["Description"] = row["Description"];
				dataRow3["Location"] = Security.DefaultInventoryLocationID;
				dataRow3["Unit"] = row["UnitID"];
				if (row["SubunitPrice"] != DBNull.Value)
				{
					dataRow3["Price"] = decimal.Parse(row["SubunitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				else
				{
					dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				decimal num = default(decimal);
				decimal d = default(decimal);
				decimal num2 = default(decimal);
				if (dataRow3["Quantity"].ToString() != "")
				{
					num = decimal.Parse(dataRow3["Quantity"].ToString(), NumberStyles.Any);
				}
				if (dataRow3["Price"].ToString() != "")
				{
					d = decimal.Parse(dataRow3["Price"].ToString(), NumberStyles.Any);
				}
				if (row["QuantityShipped"].ToString() != "")
				{
					num2 = decimal.Parse(row["QuantityShipped"].ToString(), NumberStyles.Any);
				}
				if (!row["TaxOption"].IsNullOrEmpty())
				{
					dataRow3["TaxOption"] = row["TaxOption"].ToString();
				}
				else
				{
					dataRow3["TaxOption"] = (byte)2;
				}
				dataRow3["TaxGroupID"] = row["TaxGroupID"];
				dataRow3["Ordered"] = num;
				dataRow3["Shipped"] = num2;
				num -= num2;
				dataRow3["Quantity"] = num;
				dataRow3["Amount"] = Math.Round(num * d, Global.CurDecimalPoints);
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			foreach (UltraGridRow row2 in dataGridItems.Rows)
			{
				ItemTypes itemTypes = ItemTypes.Inventory;
				if (row2.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(row2.Cells["ItemType"].Value.ToString()));
				}
				if (itemTypes == ItemTypes.Discount)
				{
					row2.Cells["Quantity"].Activation = Activation.Disabled;
				}
			}
			if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenInvoiceThenDN)
			{
				if (!CompanyPreferences.AllowLSaleAddNew)
				{
					dataGridItems.AllowAddNew = false;
				}
				else
				{
					dataGridItems.AllowAddNew = true;
				}
			}
			AdjustGridColumnSettings();
			CalculateAllRowsTaxes();
			CalculateTotal();
			textBoxDiscountPercent.Modified = true;
			textBoxDiscountAmount_Validated(sender, e);
		}

		private void labelExpenseCode_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditExpense(comboBoxExpenseCode.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.SalesReceiptForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMultiPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromTransactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromSalesQuoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromSalesOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromProformaInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deleteTempToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			allowUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			linkLabelVehicle = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label6 = new System.Windows.Forms.Label();
			textBoxVechicleName = new System.Windows.Forms.TextBox();
			comboBoxVehicle = new Micromind.DataControls.VehicleComboBox();
			comboBoxDriver = new Micromind.DataControls.DriverComboBox();
			linkLabelDriver = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelTaxGroup = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPayeeTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			panelExpense = new Infragistics.Win.Misc.UltraPanel();
			labelExpAmount = new System.Windows.Forms.Label();
			textBoxExpAmount = new Micromind.UISupport.AmountTextBox();
			textboxPercent = new Micromind.UISupport.PercentTextBox();
			labelPercent = new System.Windows.Forms.Label();
			labelExpenseCode = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxExpenseCode = new Micromind.DataControls.ExpenseCodeComboBox();
			panelxml = new System.Windows.Forms.Panel();
			simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonFind = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonLast = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonNext = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonPrevious = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonFirst = new DevExpress.XtraEditors.SimpleButton();
			labelcostcategory = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelJob = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabelShipment = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxShipto = new Micromind.UISupport.MMTextBox();
			comboBoxBillingAddress = new Micromind.DataControls.CustomerAddressComboBox();
			comboBoxShippingAddressID = new Micromind.DataControls.CustomerAddressComboBox();
			textBoxBilltoAddress = new Micromind.UISupport.MMTextBox();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			label4 = new System.Windows.Forms.Label();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			textBoxRef2 = new System.Windows.Forms.TextBox();
			comboBoxPaymentMethod = new Micromind.DataControls.PaymentMethodTypesComboBox();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxRegister = new Micromind.DataControls.RegisterComboBox();
			comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
			comboBoxShippingMethod = new Micromind.DataControls.ShippingMethodsComboBox();
			comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label2 = new System.Windows.Forms.Label();
			textBoxPONumber = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			labelReportTo = new System.Windows.Forms.Label();
			labelVoided = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			textBoxDiscountPercent = new Micromind.UISupport.PercentTextBox();
			textBoxDiscountAmount = new Micromind.UISupport.AmountTextBox();
			panelNonTax = new System.Windows.Forms.Panel();
			linkLabelTax = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label10 = new System.Windows.Forms.Label();
			textBoxRoundOff = new Micromind.UISupport.AmountTextBox();
			textBoxTaxAmount = new Micromind.UISupport.AmountTextBox();
			label8 = new System.Windows.Forms.Label();
			textBoxTotal = new Micromind.UISupport.AmountTextBox();
			label11 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			textBoxSubtotal = new Micromind.UISupport.AmountTextBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraToolTipManager1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(components);
			labelSelectedDocs = new System.Windows.Forms.Label();
			checkedListBoxPI = new System.Windows.Forms.ListBox();
			checkBoxPriceIncludeTax = new System.Windows.Forms.CheckBox();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxStyle = new Micromind.DataControls.ProductStyleComboBox();
			comboBoxSpecification = new Micromind.DataControls.ProductSpecificationComboBox();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			formManager = new Micromind.DataControls.FormManager();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicle).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDriver).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).BeginInit();
			panelExpense.ClientArea.SuspendLayout();
			panelExpense.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxExpenseCode).BeginInit();
			panelxml.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBillingAddress).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingAddressID).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethod).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRegister).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			panel1.SuspendLayout();
			panelNonTax.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStyle).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSpecification).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[19]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator5,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator7,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonMultiPreview,
				toolStripSeparator4,
				toolStripButtonDistribution,
				toolStripButtonInformation,
				toolStripDropDownButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(916, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
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
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
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
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonMultiPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonMultiPreview.Image = Micromind.ClientUI.Properties.Resources.multi_doc;
			toolStripButtonMultiPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMultiPreview.Name = "toolStripButtonMultiPreview";
			toolStripButtonMultiPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonMultiPreview.Text = "MultiPreview";
			toolStripButtonMultiPreview.Click += new System.EventHandler(toolStripButtonMultiPreview_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8]
			{
				duplicateToolStripMenuItem,
				createFromTransactionToolStripMenuItem,
				createFromSalesQuoteToolStripMenuItem,
				createFromSalesOrderToolStripMenuItem,
				createFromProformaInvoiceToolStripMenuItem,
				deleteTempToolStripMenuItem,
				toolStripSeparator6,
				allowUpdateToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			duplicateToolStripMenuItem.Text = "Duplicate";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			createFromTransactionToolStripMenuItem.Name = "createFromTransactionToolStripMenuItem";
			createFromTransactionToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			createFromTransactionToolStripMenuItem.Text = "Create from Transaction..";
			createFromTransactionToolStripMenuItem.Click += new System.EventHandler(createFromTransactionToolStripMenuItem_Click);
			createFromSalesQuoteToolStripMenuItem.Name = "createFromSalesQuoteToolStripMenuItem";
			createFromSalesQuoteToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			createFromSalesQuoteToolStripMenuItem.Text = "Create from Sales Quote";
			createFromSalesQuoteToolStripMenuItem.Click += new System.EventHandler(createFromSalesQuoteToolStripMenuItem_Click);
			createFromSalesOrderToolStripMenuItem.Name = "createFromSalesOrderToolStripMenuItem";
			createFromSalesOrderToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			createFromSalesOrderToolStripMenuItem.Text = "Create from Sales Order";
			createFromSalesOrderToolStripMenuItem.Click += new System.EventHandler(createFromSalesOrderToolStripMenuItem_Click);
			createFromProformaInvoiceToolStripMenuItem.Name = "createFromProformaInvoiceToolStripMenuItem";
			createFromProformaInvoiceToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			createFromProformaInvoiceToolStripMenuItem.Text = "Create from Proforma Invoice";
			createFromProformaInvoiceToolStripMenuItem.Click += new System.EventHandler(createFromProformaInvoiceToolStripMenuItem_Click);
			deleteTempToolStripMenuItem.Name = "deleteTempToolStripMenuItem";
			deleteTempToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			deleteTempToolStripMenuItem.Text = "Delete Temp Transaction..";
			deleteTempToolStripMenuItem.Click += new System.EventHandler(deleteTempToolStripMenuItem_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(228, 6);
			allowUpdateToolStripMenuItem.Name = "allowUpdateToolStripMenuItem";
			allowUpdateToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
			allowUpdateToolStripMenuItem.Text = "Grant Edit Permission";
			allowUpdateToolStripMenuItem.Click += new System.EventHandler(allowUpdateToolStripMenuItem_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 606);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(916, 40);
			panelButtons.TabIndex = 5;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(216, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 24);
			buttonVoid.TabIndex = 2;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(318, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 3;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
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
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(916, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(806, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 4;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
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
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(769, 4);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(123, 20);
			dateTimePickerDate.TabIndex = 6;
			textBoxVoucherNumber.Location = new System.Drawing.Point(299, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(131, 20);
			textBoxVoucherNumber.TabIndex = 3;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(688, 30);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(769, 26);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(123, 20);
			textBoxRef1.TabIndex = 19;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(49, 498);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(282, 90);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 498);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(216, 5);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Doc Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(linkLabelVehicle);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(textBoxVechicleName);
			panelDetails.Controls.Add(comboBoxVehicle);
			panelDetails.Controls.Add(comboBoxDriver);
			panelDetails.Controls.Add(linkLabelDriver);
			panelDetails.Controls.Add(ultraFormattedLinkLabel8);
			panelDetails.Controls.Add(labelTaxGroup);
			panelDetails.Controls.Add(comboBoxPayeeTaxGroup);
			panelDetails.Controls.Add(panelExpense);
			panelDetails.Controls.Add(panelxml);
			panelDetails.Controls.Add(labelcostcategory);
			panelDetails.Controls.Add(labelJob);
			panelDetails.Controls.Add(ultraFormattedLinkLabelShipment);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(textBoxShipto);
			panelDetails.Controls.Add(comboBoxBillingAddress);
			panelDetails.Controls.Add(comboBoxShippingAddressID);
			panelDetails.Controls.Add(textBoxBilltoAddress);
			panelDetails.Controls.Add(comboBoxCostCategory);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(comboBoxJob);
			panelDetails.Controls.Add(textBoxRef2);
			panelDetails.Controls.Add(comboBoxPaymentMethod);
			panelDetails.Controls.Add(ultraFormattedLinkLabel7);
			panelDetails.Controls.Add(comboBoxRegister);
			panelDetails.Controls.Add(comboBoxCurrency);
			panelDetails.Controls.Add(comboBoxShippingMethod);
			panelDetails.Controls.Add(comboBoxSalesperson);
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxPONumber);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxCustomer);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxCustomerName);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(labelReportTo);
			panelDetails.Location = new System.Drawing.Point(0, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(901, 232);
			panelDetails.TabIndex = 0;
			panelDetails.Paint += new System.Windows.Forms.PaintEventHandler(panelDetails_Paint);
			appearance3.FontData.BoldAsString = "False";
			appearance3.FontData.Name = "Tahoma";
			linkLabelVehicle.Appearance = appearance3;
			linkLabelVehicle.AutoSize = true;
			linkLabelVehicle.Location = new System.Drawing.Point(510, 142);
			linkLabelVehicle.Name = "linkLabelVehicle";
			linkLabelVehicle.Size = new System.Drawing.Size(43, 15);
			linkLabelVehicle.TabIndex = 182;
			linkLabelVehicle.TabStop = true;
			linkLabelVehicle.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVehicle.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVehicle.Value = "Vehicle:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			linkLabelVehicle.VisitedLinkAppearance = appearance4;
			linkLabelVehicle.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVehicle_LinkClicked);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(688, 141);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(76, 13);
			label6.TabIndex = 181;
			label6.Text = "Vehicle Name:";
			textBoxVechicleName.Location = new System.Drawing.Point(769, 137);
			textBoxVechicleName.MaxLength = 20;
			textBoxVechicleName.Name = "textBoxVechicleName";
			textBoxVechicleName.ReadOnly = true;
			textBoxVechicleName.Size = new System.Drawing.Size(125, 20);
			textBoxVechicleName.TabIndex = 25;
			comboBoxVehicle.Assigned = false;
			comboBoxVehicle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVehicle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVehicle.CustomReportFieldName = "";
			comboBoxVehicle.CustomReportKey = "";
			comboBoxVehicle.CustomReportValueType = 1;
			comboBoxVehicle.DescriptionTextBox = textBoxVechicleName;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVehicle.DisplayLayout.Appearance = appearance5;
			comboBoxVehicle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVehicle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxVehicle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicle.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxVehicle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVehicle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVehicle.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVehicle.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxVehicle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVehicle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVehicle.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxVehicle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVehicle.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxVehicle.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxVehicle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVehicle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxVehicle.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxVehicle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVehicle.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxVehicle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVehicle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVehicle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVehicle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVehicle.Editable = true;
			comboBoxVehicle.FilterString = "";
			comboBoxVehicle.HasAllAccount = false;
			comboBoxVehicle.HasCustom = false;
			comboBoxVehicle.IsDataLoaded = false;
			comboBoxVehicle.Location = new System.Drawing.Point(559, 140);
			comboBoxVehicle.MaxDropDownItems = 12;
			comboBoxVehicle.Name = "comboBoxVehicle";
			comboBoxVehicle.ShowInactiveItems = false;
			comboBoxVehicle.ShowQuickAdd = true;
			comboBoxVehicle.Size = new System.Drawing.Size(126, 20);
			comboBoxVehicle.TabIndex = 24;
			comboBoxVehicle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVehicle.SelectedIndexChanged += new System.EventHandler(comboBoxVehicle_SelectedIndexChanged);
			comboBoxDriver.Assigned = false;
			comboBoxDriver.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDriver.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDriver.CustomReportFieldName = "";
			comboBoxDriver.CustomReportKey = "";
			comboBoxDriver.CustomReportValueType = 1;
			comboBoxDriver.DescriptionTextBox = null;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDriver.DisplayLayout.Appearance = appearance17;
			comboBoxDriver.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDriver.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDriver.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDriver.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxDriver.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDriver.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxDriver.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDriver.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDriver.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDriver.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxDriver.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDriver.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDriver.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDriver.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxDriver.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDriver.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDriver.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxDriver.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxDriver.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDriver.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxDriver.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxDriver.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDriver.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxDriver.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDriver.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDriver.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDriver.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDriver.Editable = true;
			comboBoxDriver.FilterString = "";
			comboBoxDriver.HasAllAccount = false;
			comboBoxDriver.HasCustom = false;
			comboBoxDriver.IsDataLoaded = false;
			comboBoxDriver.Location = new System.Drawing.Point(769, 160);
			comboBoxDriver.MaxDropDownItems = 12;
			comboBoxDriver.Name = "comboBoxDriver";
			comboBoxDriver.ShowInactiveItems = false;
			comboBoxDriver.ShowQuickAdd = true;
			comboBoxDriver.Size = new System.Drawing.Size(125, 20);
			comboBoxDriver.TabIndex = 26;
			comboBoxDriver.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance29.FontData.BoldAsString = "False";
			appearance29.FontData.Name = "Tahoma";
			linkLabelDriver.Appearance = appearance29;
			linkLabelDriver.AutoSize = true;
			linkLabelDriver.Location = new System.Drawing.Point(688, 161);
			linkLabelDriver.Name = "linkLabelDriver";
			linkLabelDriver.Size = new System.Drawing.Size(38, 15);
			linkLabelDriver.TabIndex = 180;
			linkLabelDriver.TabStop = true;
			linkLabelDriver.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelDriver.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelDriver.Value = "Driver:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			linkLabelDriver.VisitedLinkAppearance = appearance30;
			linkLabelDriver.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelDriver_LinkClicked);
			appearance31.FontData.BoldAsString = "False";
			appearance31.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel8.Appearance = appearance31;
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(12, 172);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(65, 15);
			ultraFormattedLinkLabel8.TabIndex = 175;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Pay.Method:";
			appearance32.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance32;
			appearance33.FontData.BoldAsString = "False";
			appearance33.FontData.Name = "Tahoma";
			labelTaxGroup.Appearance = appearance33;
			labelTaxGroup.AutoSize = true;
			labelTaxGroup.Location = new System.Drawing.Point(12, 148);
			labelTaxGroup.Name = "labelTaxGroup";
			labelTaxGroup.Size = new System.Drawing.Size(59, 15);
			labelTaxGroup.TabIndex = 174;
			labelTaxGroup.TabStop = true;
			labelTaxGroup.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelTaxGroup.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelTaxGroup.Value = "Tax Group:";
			appearance34.ForeColor = System.Drawing.Color.Blue;
			labelTaxGroup.VisitedLinkAppearance = appearance34;
			labelTaxGroup.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			comboBoxPayeeTaxGroup.Assigned = false;
			comboBoxPayeeTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPayeeTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayeeTaxGroup.CustomReportFieldName = "";
			comboBoxPayeeTaxGroup.CustomReportKey = "";
			comboBoxPayeeTaxGroup.CustomReportValueType = 1;
			comboBoxPayeeTaxGroup.DescriptionTextBox = null;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayeeTaxGroup.DisplayLayout.Appearance = appearance35;
			comboBoxPayeeTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayeeTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance36;
			appearance37.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance37;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance38.BackColor2 = System.Drawing.SystemColors.Control;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance38.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance38;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance39;
			appearance40.BackColor = System.Drawing.SystemColors.Highlight;
			appearance40.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance40;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance41;
			appearance42.BorderColor = System.Drawing.Color.Silver;
			appearance42.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellAppearance = appearance42;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance43.BackColor = System.Drawing.SystemColors.Control;
			appearance43.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance43.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance43;
			appearance44.TextHAlignAsString = "Left";
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance44;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowAppearance = appearance45;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance46;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayeeTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayeeTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayeeTaxGroup.Editable = true;
			comboBoxPayeeTaxGroup.FilterString = "";
			comboBoxPayeeTaxGroup.HasAllAccount = false;
			comboBoxPayeeTaxGroup.HasCustom = false;
			comboBoxPayeeTaxGroup.IsDataLoaded = false;
			comboBoxPayeeTaxGroup.Location = new System.Drawing.Point(99, 145);
			comboBoxPayeeTaxGroup.MaxDropDownItems = 12;
			comboBoxPayeeTaxGroup.Name = "comboBoxPayeeTaxGroup";
			comboBoxPayeeTaxGroup.ReadOnly = true;
			comboBoxPayeeTaxGroup.ShowInactiveItems = false;
			comboBoxPayeeTaxGroup.ShowQuickAdd = true;
			comboBoxPayeeTaxGroup.Size = new System.Drawing.Size(117, 20);
			comboBoxPayeeTaxGroup.TabIndex = 13;
			comboBoxPayeeTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPayeeTaxGroup.SelectedIndexChanged += new System.EventHandler(comboBoxPayeeTaxGroup_SelectedIndexChanged);
			panelExpense.ClientArea.Controls.Add(labelExpAmount);
			panelExpense.ClientArea.Controls.Add(textBoxExpAmount);
			panelExpense.ClientArea.Controls.Add(textboxPercent);
			panelExpense.ClientArea.Controls.Add(labelPercent);
			panelExpense.ClientArea.Controls.Add(labelExpenseCode);
			panelExpense.ClientArea.Controls.Add(comboBoxExpenseCode);
			panelExpense.Location = new System.Drawing.Point(436, 69);
			panelExpense.Name = "panelExpense";
			panelExpense.Size = new System.Drawing.Size(249, 68);
			panelExpense.TabIndex = 18;
			labelExpAmount.AutoSize = true;
			labelExpAmount.Location = new System.Drawing.Point(110, 43);
			labelExpAmount.Name = "labelExpAmount";
			labelExpAmount.Size = new System.Drawing.Size(67, 13);
			labelExpAmount.TabIndex = 4;
			labelExpAmount.Text = "Exp.Amount:";
			labelExpAmount.Click += new System.EventHandler(labelExpAmount_Click);
			textBoxExpAmount.AllowDecimal = true;
			textBoxExpAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxExpAmount.CustomReportFieldName = "";
			textBoxExpAmount.CustomReportKey = "";
			textBoxExpAmount.CustomReportValueType = 1;
			textBoxExpAmount.IsComboTextBox = false;
			textBoxExpAmount.IsModified = false;
			textBoxExpAmount.Location = new System.Drawing.Point(181, 39);
			textBoxExpAmount.MaxLength = 15;
			textBoxExpAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxExpAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxExpAmount.Name = "textBoxExpAmount";
			textBoxExpAmount.NullText = "0";
			textBoxExpAmount.ReadOnly = true;
			textBoxExpAmount.Size = new System.Drawing.Size(62, 20);
			textBoxExpAmount.TabIndex = 5;
			textBoxExpAmount.Text = "0.00";
			textBoxExpAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxExpAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxExpAmount.TextChanged += new System.EventHandler(textBoxExpAmount_TextChanged);
			textBoxExpAmount.Validated += new System.EventHandler(textBoxExpAmount_Validated);
			textboxPercent.BackColor = System.Drawing.Color.WhiteSmoke;
			textboxPercent.CustomReportFieldName = "";
			textboxPercent.CustomReportKey = "";
			textboxPercent.CustomReportValueType = 1;
			textboxPercent.IsComboTextBox = false;
			textboxPercent.IsModified = false;
			textboxPercent.Location = new System.Drawing.Point(53, 39);
			textboxPercent.Name = "textboxPercent";
			textboxPercent.ReadOnly = true;
			textboxPercent.Size = new System.Drawing.Size(56, 20);
			textboxPercent.TabIndex = 3;
			textboxPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textboxPercent.Validated += new System.EventHandler(textboxPercent_Validated);
			labelPercent.AutoSize = true;
			labelPercent.Location = new System.Drawing.Point(8, 42);
			labelPercent.Name = "labelPercent";
			labelPercent.Size = new System.Drawing.Size(47, 13);
			labelPercent.TabIndex = 2;
			labelPercent.Text = "Percent:";
			appearance47.FontData.BoldAsString = "False";
			appearance47.FontData.Name = "Tahoma";
			labelExpenseCode.Appearance = appearance47;
			labelExpenseCode.AutoSize = true;
			labelExpenseCode.Location = new System.Drawing.Point(8, 8);
			labelExpenseCode.Name = "labelExpenseCode";
			labelExpenseCode.Size = new System.Drawing.Size(77, 15);
			labelExpenseCode.TabIndex = 0;
			labelExpenseCode.TabStop = true;
			labelExpenseCode.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelExpenseCode.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelExpenseCode.Value = "Expense Code:";
			appearance48.ForeColor = System.Drawing.Color.Blue;
			labelExpenseCode.VisitedLinkAppearance = appearance48;
			labelExpenseCode.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelExpenseCode_LinkClicked);
			comboBoxExpenseCode.Assigned = false;
			comboBoxExpenseCode.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxExpenseCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxExpenseCode.CustomReportFieldName = "";
			comboBoxExpenseCode.CustomReportKey = "";
			comboBoxExpenseCode.CustomReportValueType = 1;
			comboBoxExpenseCode.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxExpenseCode.DisplayLayout.Appearance = appearance49;
			comboBoxExpenseCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxExpenseCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxExpenseCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxExpenseCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxExpenseCode.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxExpenseCode.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxExpenseCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxExpenseCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseCode.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxExpenseCode.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxExpenseCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxExpenseCode.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseCode.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxExpenseCode.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxExpenseCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxExpenseCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxExpenseCode.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxExpenseCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxExpenseCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxExpenseCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxExpenseCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxExpenseCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxExpenseCode.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxExpenseCode.Editable = true;
			comboBoxExpenseCode.FilterString = "";
			comboBoxExpenseCode.HasAllAccount = false;
			comboBoxExpenseCode.HasCustom = false;
			comboBoxExpenseCode.IsDataLoaded = false;
			comboBoxExpenseCode.Location = new System.Drawing.Point(89, 5);
			comboBoxExpenseCode.MaxDropDownItems = 12;
			comboBoxExpenseCode.Name = "comboBoxExpenseCode";
			comboBoxExpenseCode.ReadOnly = true;
			comboBoxExpenseCode.ShowInactiveItems = false;
			comboBoxExpenseCode.ShowQuickAdd = true;
			comboBoxExpenseCode.Size = new System.Drawing.Size(127, 20);
			comboBoxExpenseCode.TabIndex = 1;
			comboBoxExpenseCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			panelxml.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panelxml.Controls.Add(simpleButtonSave);
			panelxml.Controls.Add(simpleButtonFind);
			panelxml.Controls.Add(simpleButtonLast);
			panelxml.Controls.Add(simpleButtonNext);
			panelxml.Controls.Add(simpleButtonPrevious);
			panelxml.Controls.Add(simpleButtonFirst);
			panelxml.Location = new System.Drawing.Point(574, 194);
			panelxml.Name = "panelxml";
			panelxml.Size = new System.Drawing.Size(318, 30);
			panelxml.TabIndex = 172;
			simpleButtonSave.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonSave.Appearance.Options.UseFont = true;
			simpleButtonSave.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.save;
			simpleButtonSave.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonSave.Location = new System.Drawing.Point(270, 5);
			simpleButtonSave.Name = "simpleButtonSave";
			simpleButtonSave.Size = new System.Drawing.Size(39, 20);
			simpleButtonSave.TabIndex = 250;
			simpleButtonSave.Click += new System.EventHandler(simpleButton1_Click);
			simpleButtonFind.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonFind.Appearance.Options.UseFont = true;
			simpleButtonFind.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.find;
			simpleButtonFind.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonFind.Location = new System.Drawing.Point(108, 5);
			simpleButtonFind.Name = "simpleButtonFind";
			simpleButtonFind.Size = new System.Drawing.Size(60, 20);
			simpleButtonFind.TabIndex = 249;
			simpleButtonFind.Click += new System.EventHandler(simpleButtonFind_Click);
			simpleButtonLast.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonLast.Appearance.Options.UseFont = true;
			simpleButtonLast.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.last;
			simpleButtonLast.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonLast.Location = new System.Drawing.Point(220, 5);
			simpleButtonLast.Name = "simpleButtonLast";
			simpleButtonLast.Size = new System.Drawing.Size(50, 20);
			simpleButtonLast.TabIndex = 6;
			simpleButtonLast.Click += new System.EventHandler(simpleButtonLast_Click);
			simpleButtonNext.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonNext.Appearance.Options.UseFont = true;
			simpleButtonNext.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.next;
			simpleButtonNext.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonNext.Location = new System.Drawing.Point(169, 5);
			simpleButtonNext.Name = "simpleButtonNext";
			simpleButtonNext.Size = new System.Drawing.Size(50, 20);
			simpleButtonNext.TabIndex = 5;
			simpleButtonNext.Click += new System.EventHandler(simpleButtonNext_Click);
			simpleButtonPrevious.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonPrevious.Appearance.Options.UseFont = true;
			simpleButtonPrevious.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.prev;
			simpleButtonPrevious.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonPrevious.Location = new System.Drawing.Point(57, 5);
			simpleButtonPrevious.Name = "simpleButtonPrevious";
			simpleButtonPrevious.Size = new System.Drawing.Size(50, 20);
			simpleButtonPrevious.TabIndex = 4;
			simpleButtonPrevious.Click += new System.EventHandler(simpleButtonPrevious_Click);
			simpleButtonFirst.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonFirst.Appearance.Options.UseFont = true;
			simpleButtonFirst.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.first;
			simpleButtonFirst.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonFirst.Location = new System.Drawing.Point(6, 5);
			simpleButtonFirst.Name = "simpleButtonFirst";
			simpleButtonFirst.Size = new System.Drawing.Size(50, 20);
			simpleButtonFirst.TabIndex = 3;
			simpleButtonFirst.Click += new System.EventHandler(simpleButtonFirst_Click);
			appearance61.FontData.BoldAsString = "False";
			appearance61.FontData.Name = "Tahoma";
			labelcostcategory.Appearance = appearance61;
			labelcostcategory.AutoSize = true;
			labelcostcategory.Location = new System.Drawing.Point(389, 172);
			labelcostcategory.Name = "labelcostcategory";
			labelcostcategory.Size = new System.Drawing.Size(76, 15);
			labelcostcategory.TabIndex = 170;
			labelcostcategory.TabStop = true;
			labelcostcategory.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelcostcategory.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelcostcategory.Value = "Cost Category:";
			appearance62.ForeColor = System.Drawing.Color.Blue;
			labelcostcategory.VisitedLinkAppearance = appearance62;
			labelcostcategory.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelcostcategory_LinkClicked);
			appearance63.FontData.BoldAsString = "False";
			appearance63.FontData.Name = "Tahoma";
			labelJob.Appearance = appearance63;
			labelJob.AutoSize = true;
			labelJob.Location = new System.Drawing.Point(229, 172);
			labelJob.Name = "labelJob";
			labelJob.Size = new System.Drawing.Size(42, 15);
			labelJob.TabIndex = 169;
			labelJob.TabStop = true;
			labelJob.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelJob.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelJob.Value = "Project:";
			appearance64.ForeColor = System.Drawing.Color.Blue;
			labelJob.VisitedLinkAppearance = appearance64;
			labelJob.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelJob_LinkClicked);
			appearance65.FontData.BoldAsString = "False";
			appearance65.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelShipment.Appearance = appearance65;
			ultraFormattedLinkLabelShipment.AutoSize = true;
			ultraFormattedLinkLabelShipment.Location = new System.Drawing.Point(225, 145);
			ultraFormattedLinkLabelShipment.Name = "ultraFormattedLinkLabelShipment";
			ultraFormattedLinkLabelShipment.Size = new System.Drawing.Size(90, 15);
			ultraFormattedLinkLabelShipment.TabIndex = 168;
			ultraFormattedLinkLabelShipment.TabStop = true;
			ultraFormattedLinkLabelShipment.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelShipment.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelShipment.Value = "Shipping Method:";
			appearance66.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelShipment.VisitedLinkAppearance = appearance66;
			ultraFormattedLinkLabelShipment.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelShipment_LinkClicked);
			appearance67.FontData.BoldAsString = "False";
			appearance67.FontData.Name = "Tahoma";
			labelCurrency.Appearance = appearance67;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(688, 117);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 15);
			labelCurrency.TabIndex = 167;
			labelCurrency.TabStop = true;
			labelCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCurrency.Value = "Currency:";
			appearance68.ForeColor = System.Drawing.Color.Blue;
			labelCurrency.VisitedLinkAppearance = appearance68;
			labelCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelCurrency_LinkClicked);
			textBoxShipto.BackColor = System.Drawing.Color.White;
			textBoxShipto.CustomReportFieldName = "";
			textBoxShipto.CustomReportKey = "";
			textBoxShipto.CustomReportValueType = 1;
			textBoxShipto.IsComboTextBox = false;
			textBoxShipto.IsModified = false;
			textBoxShipto.Location = new System.Drawing.Point(230, 68);
			textBoxShipto.MaxLength = 255;
			textBoxShipto.Multiline = true;
			textBoxShipto.Name = "textBoxShipto";
			textBoxShipto.Size = new System.Drawing.Size(200, 72);
			textBoxShipto.TabIndex = 12;
			comboBoxBillingAddress.Assigned = false;
			comboBoxBillingAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBillingAddress.CustomReportFieldName = "";
			comboBoxBillingAddress.CustomReportKey = "";
			comboBoxBillingAddress.CustomReportValueType = 1;
			comboBoxBillingAddress.DescriptionTextBox = null;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBillingAddress.DisplayLayout.Appearance = appearance69;
			comboBoxBillingAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBillingAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance70.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance70.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.Appearance = appearance70;
			appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance71;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance72.BackColor2 = System.Drawing.SystemColors.Control;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance72;
			comboBoxBillingAddress.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBillingAddress.DisplayLayout.MaxRowScrollRegions = 1;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBillingAddress.DisplayLayout.Override.ActiveCellAppearance = appearance73;
			appearance74.BackColor = System.Drawing.SystemColors.Highlight;
			appearance74.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBillingAddress.DisplayLayout.Override.ActiveRowAppearance = appearance74;
			comboBoxBillingAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBillingAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.Override.CardAreaAppearance = appearance75;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			appearance76.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBillingAddress.DisplayLayout.Override.CellAppearance = appearance76;
			comboBoxBillingAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBillingAddress.DisplayLayout.Override.CellPadding = 0;
			appearance77.BackColor = System.Drawing.SystemColors.Control;
			appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance77.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.Override.GroupByRowAppearance = appearance77;
			appearance78.TextHAlignAsString = "Left";
			comboBoxBillingAddress.DisplayLayout.Override.HeaderAppearance = appearance78;
			comboBoxBillingAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBillingAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.Color.Silver;
			comboBoxBillingAddress.DisplayLayout.Override.RowAppearance = appearance79;
			comboBoxBillingAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBillingAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance80;
			comboBoxBillingAddress.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBillingAddress.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBillingAddress.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBillingAddress.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBillingAddress.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxBillingAddress.Editable = true;
			comboBoxBillingAddress.FilterString = "";
			comboBoxBillingAddress.HasAllAccount = false;
			comboBoxBillingAddress.HasCustom = false;
			comboBoxBillingAddress.IsDataLoaded = false;
			comboBoxBillingAddress.Location = new System.Drawing.Point(99, 45);
			comboBoxBillingAddress.MaxDropDownItems = 12;
			comboBoxBillingAddress.Name = "comboBoxBillingAddress";
			comboBoxBillingAddress.ShowInactiveItems = false;
			comboBoxBillingAddress.ShowQuickAdd = true;
			comboBoxBillingAddress.Size = new System.Drawing.Size(128, 20);
			comboBoxBillingAddress.TabIndex = 9;
			comboBoxBillingAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxShippingAddressID.Assigned = false;
			comboBoxShippingAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingAddressID.CustomReportFieldName = "";
			comboBoxShippingAddressID.CustomReportKey = "";
			comboBoxShippingAddressID.CustomReportValueType = 1;
			comboBoxShippingAddressID.DescriptionTextBox = null;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingAddressID.DisplayLayout.Appearance = appearance81;
			comboBoxShippingAddressID.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingAddressID.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.Appearance = appearance82;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.BandLabelAppearance = appearance83;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance84.BackColor2 = System.Drawing.SystemColors.Control;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.PromptAppearance = appearance84;
			comboBoxShippingAddressID.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingAddressID.DisplayLayout.MaxRowScrollRegions = 1;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingAddressID.DisplayLayout.Override.ActiveCellAppearance = appearance85;
			appearance86.BackColor = System.Drawing.SystemColors.Highlight;
			appearance86.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingAddressID.DisplayLayout.Override.ActiveRowAppearance = appearance86;
			comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.Override.CardAreaAppearance = appearance87;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			appearance88.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingAddressID.DisplayLayout.Override.CellAppearance = appearance88;
			comboBoxShippingAddressID.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingAddressID.DisplayLayout.Override.CellPadding = 0;
			appearance89.BackColor = System.Drawing.SystemColors.Control;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.Override.GroupByRowAppearance = appearance89;
			appearance90.TextHAlignAsString = "Left";
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderAppearance = appearance90;
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingAddressID.DisplayLayout.Override.RowAppearance = appearance91;
			comboBoxShippingAddressID.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingAddressID.DisplayLayout.Override.TemplateAddRowAppearance = appearance92;
			comboBoxShippingAddressID.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingAddressID.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingAddressID.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingAddressID.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingAddressID.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxShippingAddressID.Editable = true;
			comboBoxShippingAddressID.FilterString = "";
			comboBoxShippingAddressID.HasAllAccount = false;
			comboBoxShippingAddressID.HasCustom = false;
			comboBoxShippingAddressID.IsDataLoaded = false;
			comboBoxShippingAddressID.Location = new System.Drawing.Point(290, 46);
			comboBoxShippingAddressID.MaxDropDownItems = 12;
			comboBoxShippingAddressID.Name = "comboBoxShippingAddressID";
			comboBoxShippingAddressID.ShowInactiveItems = false;
			comboBoxShippingAddressID.ShowQuickAdd = true;
			comboBoxShippingAddressID.Size = new System.Drawing.Size(140, 20);
			comboBoxShippingAddressID.TabIndex = 10;
			comboBoxShippingAddressID.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxBilltoAddress.BackColor = System.Drawing.Color.White;
			textBoxBilltoAddress.CustomReportFieldName = "";
			textBoxBilltoAddress.CustomReportKey = "";
			textBoxBilltoAddress.CustomReportValueType = 1;
			textBoxBilltoAddress.IsComboTextBox = false;
			textBoxBilltoAddress.IsModified = false;
			textBoxBilltoAddress.Location = new System.Drawing.Point(12, 67);
			textBoxBilltoAddress.MaxLength = 255;
			textBoxBilltoAddress.Multiline = true;
			textBoxBilltoAddress.Name = "textBoxBilltoAddress";
			textBoxBilltoAddress.Size = new System.Drawing.Size(215, 72);
			textBoxBilltoAddress.TabIndex = 11;
			comboBoxCostCategory.Assigned = false;
			comboBoxCostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCategory.CustomReportFieldName = "";
			comboBoxCostCategory.CustomReportKey = "";
			comboBoxCostCategory.CustomReportValueType = 1;
			comboBoxCostCategory.DescriptionTextBox = null;
			comboBoxCostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCategory.Editable = true;
			comboBoxCostCategory.FilterString = "";
			comboBoxCostCategory.HasAllAccount = false;
			comboBoxCostCategory.HasCustom = false;
			comboBoxCostCategory.IsDataLoaded = false;
			comboBoxCostCategory.Location = new System.Drawing.Point(469, 169);
			comboBoxCostCategory.MaxDropDownItems = 12;
			comboBoxCostCategory.Name = "comboBoxCostCategory";
			comboBoxCostCategory.ShowInactiveItems = false;
			comboBoxCostCategory.ShowQuickAdd = true;
			comboBoxCostCategory.Size = new System.Drawing.Size(103, 20);
			comboBoxCostCategory.TabIndex = 17;
			comboBoxCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(688, 52);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(69, 13);
			label4.TabIndex = 146;
			label4.Text = "Reference 2:";
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJob.CustomReportFieldName = "";
			comboBoxJob.CustomReportKey = "";
			comboBoxJob.CustomReportValueType = 1;
			comboBoxJob.DescriptionTextBox = null;
			comboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJob.Editable = true;
			comboBoxJob.FilterString = "";
			comboBoxJob.HasAllAccount = false;
			comboBoxJob.HasCustom = false;
			comboBoxJob.IsDataLoaded = false;
			comboBoxJob.Location = new System.Drawing.Point(277, 169);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(104, 20);
			comboBoxJob.TabIndex = 16;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxRef2.Location = new System.Drawing.Point(769, 48);
			textBoxRef2.MaxLength = 20;
			textBoxRef2.Name = "textBoxRef2";
			textBoxRef2.Size = new System.Drawing.Size(123, 20);
			textBoxRef2.TabIndex = 20;
			comboBoxPaymentMethod.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			comboBoxPaymentMethod.HideARAccount = false;
			comboBoxPaymentMethod.HideCheque = true;
			comboBoxPaymentMethod.Location = new System.Drawing.Point(99, 169);
			comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
			comboBoxPaymentMethod.Size = new System.Drawing.Size(117, 21);
			comboBoxPaymentMethod.TabIndex = 15;
			comboBoxPaymentMethod.ValueChanged += new System.EventHandler(comboBoxPaymentMethod_ValueChanged);
			appearance93.FontData.BoldAsString = "True";
			appearance93.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance93;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(441, 5);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(55, 15);
			ultraFormattedLinkLabel7.TabIndex = 4;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Register:";
			appearance94.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance94;
			comboBoxRegister.Assigned = false;
			comboBoxRegister.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRegister.CustomReportFieldName = "";
			comboBoxRegister.CustomReportKey = "";
			comboBoxRegister.CustomReportValueType = 1;
			comboBoxRegister.DescriptionTextBox = null;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRegister.DisplayLayout.Appearance = appearance95;
			comboBoxRegister.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRegister.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance96.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance96.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.GroupByBox.Appearance = appearance96;
			appearance97.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRegister.DisplayLayout.GroupByBox.BandLabelAppearance = appearance97;
			comboBoxRegister.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance98.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance98.BackColor2 = System.Drawing.SystemColors.Control;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance98.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRegister.DisplayLayout.GroupByBox.PromptAppearance = appearance98;
			comboBoxRegister.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRegister.DisplayLayout.MaxRowScrollRegions = 1;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRegister.DisplayLayout.Override.ActiveCellAppearance = appearance99;
			appearance100.BackColor = System.Drawing.SystemColors.Highlight;
			appearance100.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRegister.DisplayLayout.Override.ActiveRowAppearance = appearance100;
			comboBoxRegister.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRegister.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.Override.CardAreaAppearance = appearance101;
			appearance102.BorderColor = System.Drawing.Color.Silver;
			appearance102.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRegister.DisplayLayout.Override.CellAppearance = appearance102;
			comboBoxRegister.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRegister.DisplayLayout.Override.CellPadding = 0;
			appearance103.BackColor = System.Drawing.SystemColors.Control;
			appearance103.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance103.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance103.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance103.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.Override.GroupByRowAppearance = appearance103;
			appearance104.TextHAlignAsString = "Left";
			comboBoxRegister.DisplayLayout.Override.HeaderAppearance = appearance104;
			comboBoxRegister.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRegister.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.Color.Silver;
			comboBoxRegister.DisplayLayout.Override.RowAppearance = appearance105;
			comboBoxRegister.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRegister.DisplayLayout.Override.TemplateAddRowAppearance = appearance106;
			comboBoxRegister.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRegister.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRegister.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRegister.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRegister.Editable = true;
			comboBoxRegister.FilterString = "";
			comboBoxRegister.HasAllAccount = false;
			comboBoxRegister.HasCustom = false;
			comboBoxRegister.IsDataLoaded = false;
			comboBoxRegister.Location = new System.Drawing.Point(502, 2);
			comboBoxRegister.MaxDropDownItems = 12;
			comboBoxRegister.Name = "comboBoxRegister";
			comboBoxRegister.ShowDefaultRegisterOnly = false;
			comboBoxRegister.ShowInactiveItems = false;
			comboBoxRegister.ShowQuickAdd = true;
			comboBoxRegister.Size = new System.Drawing.Size(150, 20);
			comboBoxRegister.TabIndex = 5;
			comboBoxRegister.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCurrency.Location = new System.Drawing.Point(769, 114);
			comboBoxCurrency.MaximumSize = new System.Drawing.Size(99999, 20);
			comboBoxCurrency.MinimumSize = new System.Drawing.Size(5, 20);
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.Rate = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			comboBoxCurrency.SelectedID = "";
			comboBoxCurrency.Size = new System.Drawing.Size(123, 20);
			comboBoxCurrency.TabIndex = 23;
			comboBoxShippingMethod.Assigned = false;
			comboBoxShippingMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingMethod.CustomReportFieldName = "";
			comboBoxShippingMethod.CustomReportKey = "";
			comboBoxShippingMethod.CustomReportValueType = 1;
			comboBoxShippingMethod.DescriptionTextBox = null;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingMethod.DisplayLayout.Appearance = appearance107;
			comboBoxShippingMethod.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingMethod.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance108.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance108.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.Appearance = appearance108;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BandLabelAppearance = appearance109;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance110.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance110.BackColor2 = System.Drawing.SystemColors.Control;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance110.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.PromptAppearance = appearance110;
			comboBoxShippingMethod.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingMethod.DisplayLayout.MaxRowScrollRegions = 1;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveCellAppearance = appearance111;
			appearance112.BackColor = System.Drawing.SystemColors.Highlight;
			appearance112.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveRowAppearance = appearance112;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.CardAreaAppearance = appearance113;
			appearance114.BorderColor = System.Drawing.Color.Silver;
			appearance114.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingMethod.DisplayLayout.Override.CellAppearance = appearance114;
			comboBoxShippingMethod.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingMethod.DisplayLayout.Override.CellPadding = 0;
			appearance115.BackColor = System.Drawing.SystemColors.Control;
			appearance115.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance115.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance115.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance115.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.GroupByRowAppearance = appearance115;
			appearance116.TextHAlignAsString = "Left";
			comboBoxShippingMethod.DisplayLayout.Override.HeaderAppearance = appearance116;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingMethod.DisplayLayout.Override.RowAppearance = appearance117;
			comboBoxShippingMethod.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingMethod.DisplayLayout.Override.TemplateAddRowAppearance = appearance118;
			comboBoxShippingMethod.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingMethod.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingMethod.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingMethod.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingMethod.Editable = true;
			comboBoxShippingMethod.FilterString = "";
			comboBoxShippingMethod.HasAllAccount = false;
			comboBoxShippingMethod.HasCustom = false;
			comboBoxShippingMethod.IsDataLoaded = false;
			comboBoxShippingMethod.Location = new System.Drawing.Point(318, 143);
			comboBoxShippingMethod.MaxDropDownItems = 12;
			comboBoxShippingMethod.Name = "comboBoxShippingMethod";
			comboBoxShippingMethod.ShowInactiveItems = false;
			comboBoxShippingMethod.ShowQuickAdd = true;
			comboBoxShippingMethod.Size = new System.Drawing.Size(117, 20);
			comboBoxShippingMethod.TabIndex = 14;
			comboBoxShippingMethod.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSalesperson.Assigned = false;
			comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesperson.CustomReportFieldName = "";
			comboBoxSalesperson.CustomReportKey = "";
			comboBoxSalesperson.CustomReportValueType = 1;
			comboBoxSalesperson.DescriptionTextBox = null;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesperson.DisplayLayout.Appearance = appearance119;
			comboBoxSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance120.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance120.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance120.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.GroupByBox.Appearance = appearance120;
			appearance121.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance121;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance122.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance122.BackColor2 = System.Drawing.SystemColors.Control;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance122.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance122;
			comboBoxSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			appearance123.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance123;
			appearance124.BackColor = System.Drawing.SystemColors.Highlight;
			appearance124.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance124;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance125;
			appearance126.BorderColor = System.Drawing.Color.Silver;
			appearance126.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesperson.DisplayLayout.Override.CellAppearance = appearance126;
			comboBoxSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance127.BackColor = System.Drawing.SystemColors.Control;
			appearance127.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance127.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance127.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance127.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance127;
			appearance128.TextHAlignAsString = "Left";
			comboBoxSalesperson.DisplayLayout.Override.HeaderAppearance = appearance128;
			comboBoxSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesperson.DisplayLayout.Override.RowAppearance = appearance129;
			comboBoxSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance130.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance130;
			comboBoxSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesperson.Editable = true;
			comboBoxSalesperson.FilterString = "";
			comboBoxSalesperson.HasAllAccount = false;
			comboBoxSalesperson.HasCustom = false;
			comboBoxSalesperson.IsDataLoaded = false;
			comboBoxSalesperson.Location = new System.Drawing.Point(769, 92);
			comboBoxSalesperson.MaxDropDownItems = 12;
			comboBoxSalesperson.Name = "comboBoxSalesperson";
			comboBoxSalesperson.ShowInactiveItems = false;
			comboBoxSalesperson.ShowQuickAdd = true;
			comboBoxSalesperson.Size = new System.Drawing.Size(123, 20);
			comboBoxSalesperson.TabIndex = 22;
			comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSalesperson.SelectedIndexChanged += new System.EventHandler(comboBoxSalesperson_SelectedIndexChanged);
			appearance131.FontData.BoldAsString = "False";
			appearance131.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance131;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(688, 95);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(66, 15);
			ultraFormattedLinkLabel6.TabIndex = 137;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Salesperson:";
			appearance132.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance132;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			appearance133.FontData.BoldAsString = "False";
			appearance133.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance133;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(229, 46);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel3.TabIndex = 137;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Ship To:";
			appearance134.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance134;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			appearance135.FontData.BoldAsString = "False";
			appearance135.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance135;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 44);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(38, 15);
			ultraFormattedLinkLabel1.TabIndex = 134;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Bill To:";
			appearance136.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance136;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(688, 74);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(79, 13);
			label2.TabIndex = 132;
			label2.Text = "Customer PO#:";
			textBoxPONumber.Location = new System.Drawing.Point(769, 70);
			textBoxPONumber.MaxLength = 15;
			textBoxPONumber.Name = "textBoxPONumber";
			textBoxPONumber.Size = new System.Drawing.Size(123, 20);
			textBoxPONumber.TabIndex = 21;
			appearance137.FontData.BoldAsString = "True";
			appearance137.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance137;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(12, 24);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(79, 15);
			ultraFormattedLinkLabel4.TabIndex = 6;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Customer ID:";
			appearance138.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance138;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = null;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance139;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance140.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance140.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance140.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance140.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance140;
			appearance141.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance141;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance142.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance142.BackColor2 = System.Drawing.SystemColors.Control;
			appearance142.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance142.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance142;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance143;
			appearance144.BackColor = System.Drawing.SystemColors.Highlight;
			appearance144.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance144;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance145;
			appearance146.BorderColor = System.Drawing.Color.Silver;
			appearance146.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance146;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance147.BackColor = System.Drawing.SystemColors.Control;
			appearance147.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance147.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance147.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance147.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance147;
			appearance148.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance148;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			appearance149.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance149;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance150.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance150;
			comboBoxCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomer.Editable = true;
			comboBoxCustomer.FilterString = "";
			comboBoxCustomer.FilterSysDocID = "";
			comboBoxCustomer.HasAll = false;
			comboBoxCustomer.HasCustom = false;
			comboBoxCustomer.IsDataLoaded = false;
			comboBoxCustomer.Location = new System.Drawing.Point(99, 23);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ShowConsignmentOnly = false;
			comboBoxCustomer.ShowInactive = false;
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = false;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(109, 20);
			comboBoxCustomer.TabIndex = 7;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCustomer.TabIndexChanged += new System.EventHandler(comboBoxCustomer_TabIndexChanged);
			comboBoxCustomer.Leave += new System.EventHandler(comboBoxCustomer_Leave);
			appearance151.FontData.BoldAsString = "True";
			appearance151.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance151;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance152.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance152;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance153.BackColor = System.Drawing.SystemColors.Window;
			appearance153.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance153;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance154.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance154.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance154.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance154.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance154;
			appearance155.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance155;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance156.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance156.BackColor2 = System.Drawing.SystemColors.Control;
			appearance156.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance156.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance156;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance157;
			appearance158.BackColor = System.Drawing.SystemColors.Highlight;
			appearance158.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance158;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance159.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance159;
			appearance160.BorderColor = System.Drawing.Color.Silver;
			appearance160.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance160;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance161.BackColor = System.Drawing.SystemColors.Control;
			appearance161.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance161.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance161.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance161.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance161;
			appearance162.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance162;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			appearance163.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance163;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance164.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance164;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(99, 1);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(109, 20);
			comboBoxSysDoc.TabIndex = 1;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(688, 8);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 11;
			mmLabel1.Text = "Date:";
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.Location = new System.Drawing.Point(210, 23);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(442, 20);
			textBoxCustomerName.TabIndex = 8;
			textBoxCustomerName.TabStop = false;
			labelReportTo.AutoSize = true;
			labelReportTo.Location = new System.Drawing.Point(791, 95);
			labelReportTo.Name = "labelReportTo";
			labelReportTo.Size = new System.Drawing.Size(52, 13);
			labelReportTo.TabIndex = 176;
			labelReportTo.Text = "ReportTo";
			labelReportTo.Visible = false;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 454);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(888, 38);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(textBoxDiscountPercent);
			panel1.Controls.Add(textBoxDiscountAmount);
			panel1.Controls.Add(panelNonTax);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(textBoxTotal);
			panel1.Controls.Add(label11);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(textBoxSubtotal);
			panel1.Location = new System.Drawing.Point(684, 494);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(220, 110);
			panel1.TabIndex = 4;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(4, 25);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(52, 13);
			label7.TabIndex = 152;
			label7.Text = "Discount:";
			textBoxDiscountPercent.CustomReportFieldName = "";
			textBoxDiscountPercent.CustomReportKey = "";
			textBoxDiscountPercent.CustomReportValueType = 1;
			textBoxDiscountPercent.IsComboTextBox = false;
			textBoxDiscountPercent.IsModified = false;
			textBoxDiscountPercent.Location = new System.Drawing.Point(80, 21);
			textBoxDiscountPercent.MaxLength = 4;
			textBoxDiscountPercent.Name = "textBoxDiscountPercent";
			textBoxDiscountPercent.Size = new System.Drawing.Size(34, 20);
			textBoxDiscountPercent.TabIndex = 1;
			textBoxDiscountPercent.Text = "0";
			textBoxDiscountPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDiscountPercent.TextChanged += new System.EventHandler(textBoxDiscountPercent_TextChanged);
			textBoxDiscountPercent.Leave += new System.EventHandler(textBoxDiscountPercent_Leave);
			textBoxDiscountAmount.AllowDecimal = true;
			textBoxDiscountAmount.CustomReportFieldName = "";
			textBoxDiscountAmount.CustomReportKey = "";
			textBoxDiscountAmount.CustomReportValueType = 1;
			textBoxDiscountAmount.IsComboTextBox = false;
			textBoxDiscountAmount.IsModified = false;
			textBoxDiscountAmount.Location = new System.Drawing.Point(131, 21);
			textBoxDiscountAmount.MaxLength = 15;
			textBoxDiscountAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDiscountAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDiscountAmount.Name = "textBoxDiscountAmount";
			textBoxDiscountAmount.NullText = "0";
			textBoxDiscountAmount.Size = new System.Drawing.Size(87, 20);
			textBoxDiscountAmount.TabIndex = 2;
			textBoxDiscountAmount.Text = "0.00";
			textBoxDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDiscountAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxDiscountAmount.TextChanged += new System.EventHandler(textBoxDiscountAmount_TextChanged);
			textBoxDiscountAmount.Leave += new System.EventHandler(textBoxDiscountAmount_Leave);
			panelNonTax.Controls.Add(linkLabelTax);
			panelNonTax.Controls.Add(label10);
			panelNonTax.Controls.Add(textBoxRoundOff);
			panelNonTax.Controls.Add(textBoxTaxAmount);
			panelNonTax.Location = new System.Drawing.Point(0, 45);
			panelNonTax.Name = "panelNonTax";
			panelNonTax.Size = new System.Drawing.Size(219, 42);
			panelNonTax.TabIndex = 149;
			appearance165.FontData.BoldAsString = "False";
			appearance165.FontData.Name = "Tahoma";
			linkLabelTax.Appearance = appearance165;
			linkLabelTax.AutoSize = true;
			linkLabelTax.Location = new System.Drawing.Point(4, 2);
			linkLabelTax.Name = "linkLabelTax";
			linkLabelTax.Size = new System.Drawing.Size(25, 15);
			linkLabelTax.TabIndex = 156;
			linkLabelTax.TabStop = true;
			linkLabelTax.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelTax.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelTax.Value = "Tax:";
			appearance166.ForeColor = System.Drawing.Color.Blue;
			linkLabelTax.VisitedLinkAppearance = appearance166;
			linkLabelTax.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelTax_LinkClicked);
			label10.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			label10.Location = new System.Drawing.Point(4, 21);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(69, 15);
			label10.TabIndex = 154;
			label10.Text = "Round Off:";
			label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxRoundOff.AllowDecimal = true;
			textBoxRoundOff.CustomReportFieldName = "";
			textBoxRoundOff.CustomReportKey = "";
			textBoxRoundOff.CustomReportValueType = 1;
			textBoxRoundOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxRoundOff.IsComboTextBox = false;
			textBoxRoundOff.IsModified = false;
			textBoxRoundOff.Location = new System.Drawing.Point(80, 21);
			textBoxRoundOff.MaxLength = 15;
			textBoxRoundOff.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxRoundOff.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxRoundOff.Name = "textBoxRoundOff";
			textBoxRoundOff.NullText = "0";
			textBoxRoundOff.Size = new System.Drawing.Size(138, 20);
			textBoxRoundOff.TabIndex = 1;
			textBoxRoundOff.Text = "0.00";
			textBoxRoundOff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxRoundOff.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxRoundOff.Leave += new System.EventHandler(textBoxRoundOff_Leave);
			textBoxTaxAmount.AllowDecimal = true;
			textBoxTaxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTaxAmount.CustomReportFieldName = "";
			textBoxTaxAmount.CustomReportKey = "";
			textBoxTaxAmount.CustomReportValueType = 1;
			textBoxTaxAmount.IsComboTextBox = false;
			textBoxTaxAmount.IsModified = false;
			textBoxTaxAmount.Location = new System.Drawing.Point(80, 0);
			textBoxTaxAmount.MaxLength = 15;
			textBoxTaxAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTaxAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTaxAmount.Name = "textBoxTaxAmount";
			textBoxTaxAmount.NullText = "0";
			textBoxTaxAmount.ReadOnly = true;
			textBoxTaxAmount.Size = new System.Drawing.Size(138, 20);
			textBoxTaxAmount.TabIndex = 0;
			textBoxTaxAmount.Text = "0.00";
			textBoxTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTaxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxTaxAmount.TextChanged += new System.EventHandler(numberTextBox1_TextChanged);
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(4, 90);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(40, 13);
			label8.TabIndex = 133;
			label8.Text = "Total:";
			textBoxTotal.AllowDecimal = true;
			textBoxTotal.CustomReportFieldName = "";
			textBoxTotal.CustomReportKey = "";
			textBoxTotal.CustomReportValueType = 1;
			textBoxTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			textBoxTotal.IsComboTextBox = false;
			textBoxTotal.IsModified = false;
			textBoxTotal.Location = new System.Drawing.Point(80, 86);
			textBoxTotal.MaxLength = 15;
			textBoxTotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotal.Name = "textBoxTotal";
			textBoxTotal.NullText = "0";
			textBoxTotal.Size = new System.Drawing.Size(138, 20);
			textBoxTotal.TabIndex = 3;
			textBoxTotal.Text = "0.00";
			textBoxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxTotal.TextChanged += new System.EventHandler(textBoxTotal_TextChanged);
			textBoxTotal.Validating += new System.ComponentModel.CancelEventHandler(textBoxTotal_Validating);
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(115, 24);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(15, 13);
			label11.TabIndex = 148;
			label11.Text = "%";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(4, 3);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(49, 13);
			label5.TabIndex = 133;
			label5.Text = "Subtotal:";
			textBoxSubtotal.AllowDecimal = true;
			textBoxSubtotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSubtotal.CustomReportFieldName = "";
			textBoxSubtotal.CustomReportKey = "";
			textBoxSubtotal.CustomReportValueType = 1;
			textBoxSubtotal.ForeColor = System.Drawing.Color.Black;
			textBoxSubtotal.IsComboTextBox = false;
			textBoxSubtotal.IsModified = false;
			textBoxSubtotal.Location = new System.Drawing.Point(80, -1);
			textBoxSubtotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSubtotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSubtotal.Name = "textBoxSubtotal";
			textBoxSubtotal.NullText = "0";
			textBoxSubtotal.ReadOnly = true;
			textBoxSubtotal.Size = new System.Drawing.Size(138, 20);
			textBoxSubtotal.TabIndex = 0;
			textBoxSubtotal.TabStop = false;
			textBoxSubtotal.Text = "0.00";
			textBoxSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSubtotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				availableQuantityToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem,
				toolStripSeparator3,
				removeRowToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(181, 120);
			availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
			availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			availableQuantityToolStripMenuItem.Text = "Available Quantity...";
			availableQuantityToolStripMenuItem.Click += new System.EventHandler(availableQuantityToolStripMenuItem_Click);
			salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
			salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			salesStatisticsToolStripMenuItem.Text = "Sales Statistics...";
			salesStatisticsToolStripMenuItem.Click += new System.EventHandler(salesStatisticsToolStripMenuItem_Click);
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemPicToolStripMenuItem.Text = "Item Photo...";
			itemPicToolStripMenuItem.Click += new System.EventHandler(itemPicToolStripMenuItem_Click);
			itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
			itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemDetailsToolStripMenuItem.Text = "Item Details...";
			itemDetailsToolStripMenuItem.Click += new System.EventHandler(itemDetailsToolStripMenuItem_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
			removeRowToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
			removeRowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			removeRowToolStripMenuItem.Text = "Remove Row";
			removeRowToolStripMenuItem.Click += new System.EventHandler(removeRowToolStripMenuItem_Click);
			ultraToolTipManager1.ContainingControl = this;
			labelSelectedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelSelectedDocs.AutoSize = true;
			labelSelectedDocs.Location = new System.Drawing.Point(331, 491);
			labelSelectedDocs.Name = "labelSelectedDocs";
			labelSelectedDocs.Size = new System.Drawing.Size(109, 13);
			labelSelectedDocs.TabIndex = 165;
			labelSelectedDocs.Text = "Selected Documents:";
			checkedListBoxPI.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxPI.FormattingEnabled = true;
			checkedListBoxPI.Location = new System.Drawing.Point(334, 506);
			checkedListBoxPI.Name = "checkedListBoxPI";
			checkedListBoxPI.Size = new System.Drawing.Size(106, 82);
			checkedListBoxPI.TabIndex = 164;
			checkedListBoxPI.DoubleClick += new System.EventHandler(checkedListBoxPI_DoubleClick);
			checkBoxPriceIncludeTax.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			checkBoxPriceIncludeTax.AutoSize = true;
			checkBoxPriceIncludeTax.Location = new System.Drawing.Point(539, 494);
			checkBoxPriceIncludeTax.Name = "checkBoxPriceIncludeTax";
			checkBoxPriceIncludeTax.Size = new System.Drawing.Size(123, 17);
			checkBoxPriceIncludeTax.TabIndex = 3;
			checkBoxPriceIncludeTax.Text = "Price inclusive of tax";
			checkBoxPriceIncludeTax.UseVisualStyleBackColor = true;
			checkBoxPriceIncludeTax.Visible = false;
			checkBoxPriceIncludeTax.CheckedChanged += new System.EventHandler(checkBoxPriceIncludeTax_CheckedChanged);
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			appearance167.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance167;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance168.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance168.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance168.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance168.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance168;
			appearance169.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance169;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance170.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance170.BackColor2 = System.Drawing.SystemColors.Control;
			appearance170.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance170.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance170;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance171.BackColor = System.Drawing.SystemColors.Window;
			appearance171.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance171;
			appearance172.BackColor = System.Drawing.SystemColors.Highlight;
			appearance172.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance172;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance173.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance173;
			appearance174.BorderColor = System.Drawing.Color.Silver;
			appearance174.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance174;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance175.BackColor = System.Drawing.SystemColors.Control;
			appearance175.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance175.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance175.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance175.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance175;
			appearance176.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance176;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance177.BackColor = System.Drawing.SystemColors.Window;
			appearance177.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance177;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance178.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance178;
			comboBoxGridItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridItem.Editable = true;
			comboBoxGridItem.FilterCustomerID = "";
			comboBoxGridItem.FilterString = "";
			comboBoxGridItem.FilterSysDocID = "";
			comboBoxGridItem.HasAllAccount = false;
			comboBoxGridItem.HasCustom = false;
			comboBoxGridItem.IsDataLoaded = false;
			comboBoxGridItem.Location = new System.Drawing.Point(502, 387);
			comboBoxGridItem.MaxDropDownItems = 12;
			comboBoxGridItem.Name = "comboBoxGridItem";
			comboBoxGridItem.Show3PLItems = true;
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowOnlyLotItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(100, 20);
			comboBoxGridItem.TabIndex = 163;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance179.BackColor = System.Drawing.SystemColors.Window;
			appearance179.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance179;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance180.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance180.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance180.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance180.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance180;
			appearance181.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance181;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance182.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance182.BackColor2 = System.Drawing.SystemColors.Control;
			appearance182.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance182.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance182;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance183.BackColor = System.Drawing.SystemColors.Window;
			appearance183.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance183;
			appearance184.BackColor = System.Drawing.SystemColors.Highlight;
			appearance184.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance184;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance185.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance185;
			appearance186.BorderColor = System.Drawing.Color.Silver;
			appearance186.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance186;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance187.BackColor = System.Drawing.SystemColors.Control;
			appearance187.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance187.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance187.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance187.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance187;
			appearance188.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance188;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance189.BackColor = System.Drawing.SystemColors.Window;
			appearance189.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance189;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance190.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance190;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 267);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(890, 221);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntry&Grid1";
			comboBoxStyle.Assigned = false;
			comboBoxStyle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxStyle.CustomReportFieldName = "";
			comboBoxStyle.CustomReportKey = "";
			comboBoxStyle.CustomReportValueType = 1;
			comboBoxStyle.DescriptionTextBox = null;
			appearance191.BackColor = System.Drawing.SystemColors.Window;
			appearance191.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxStyle.DisplayLayout.Appearance = appearance191;
			comboBoxStyle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxStyle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance192.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance192.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance192.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance192.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.GroupByBox.Appearance = appearance192;
			appearance193.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStyle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance193;
			comboBoxStyle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance194.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance194.BackColor2 = System.Drawing.SystemColors.Control;
			appearance194.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance194.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStyle.DisplayLayout.GroupByBox.PromptAppearance = appearance194;
			comboBoxStyle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxStyle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance195.BackColor = System.Drawing.SystemColors.Window;
			appearance195.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxStyle.DisplayLayout.Override.ActiveCellAppearance = appearance195;
			appearance196.BackColor = System.Drawing.SystemColors.Highlight;
			appearance196.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxStyle.DisplayLayout.Override.ActiveRowAppearance = appearance196;
			comboBoxStyle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxStyle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance197.BackColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.Override.CardAreaAppearance = appearance197;
			appearance198.BorderColor = System.Drawing.Color.Silver;
			appearance198.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxStyle.DisplayLayout.Override.CellAppearance = appearance198;
			comboBoxStyle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxStyle.DisplayLayout.Override.CellPadding = 0;
			appearance199.BackColor = System.Drawing.SystemColors.Control;
			appearance199.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance199.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance199.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance199.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.Override.GroupByRowAppearance = appearance199;
			appearance200.TextHAlignAsString = "Left";
			comboBoxStyle.DisplayLayout.Override.HeaderAppearance = appearance200;
			comboBoxStyle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxStyle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance201.BackColor = System.Drawing.SystemColors.Window;
			appearance201.BorderColor = System.Drawing.Color.Silver;
			comboBoxStyle.DisplayLayout.Override.RowAppearance = appearance201;
			comboBoxStyle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance202.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxStyle.DisplayLayout.Override.TemplateAddRowAppearance = appearance202;
			comboBoxStyle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxStyle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxStyle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxStyle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxStyle.Editable = true;
			comboBoxStyle.FilterString = "";
			comboBoxStyle.HasAllAccount = false;
			comboBoxStyle.HasCustom = false;
			comboBoxStyle.IsDataLoaded = false;
			comboBoxStyle.Location = new System.Drawing.Point(455, 267);
			comboBoxStyle.MaxDropDownItems = 12;
			comboBoxStyle.Name = "comboBoxStyle";
			comboBoxStyle.ShowInactiveItems = false;
			comboBoxStyle.Size = new System.Drawing.Size(154, 20);
			comboBoxStyle.TabIndex = 162;
			comboBoxStyle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSpecification.Assigned = false;
			comboBoxSpecification.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSpecification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSpecification.CustomReportFieldName = "";
			comboBoxSpecification.CustomReportKey = "";
			comboBoxSpecification.CustomReportValueType = 1;
			comboBoxSpecification.DescriptionTextBox = null;
			appearance203.BackColor = System.Drawing.SystemColors.Window;
			appearance203.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSpecification.DisplayLayout.Appearance = appearance203;
			comboBoxSpecification.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSpecification.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance204.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance204.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance204.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance204.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.GroupByBox.Appearance = appearance204;
			appearance205.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSpecification.DisplayLayout.GroupByBox.BandLabelAppearance = appearance205;
			comboBoxSpecification.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance206.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance206.BackColor2 = System.Drawing.SystemColors.Control;
			appearance206.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance206.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSpecification.DisplayLayout.GroupByBox.PromptAppearance = appearance206;
			comboBoxSpecification.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSpecification.DisplayLayout.MaxRowScrollRegions = 1;
			appearance207.BackColor = System.Drawing.SystemColors.Window;
			appearance207.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSpecification.DisplayLayout.Override.ActiveCellAppearance = appearance207;
			appearance208.BackColor = System.Drawing.SystemColors.Highlight;
			appearance208.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSpecification.DisplayLayout.Override.ActiveRowAppearance = appearance208;
			comboBoxSpecification.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSpecification.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance209.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.Override.CardAreaAppearance = appearance209;
			appearance210.BorderColor = System.Drawing.Color.Silver;
			appearance210.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSpecification.DisplayLayout.Override.CellAppearance = appearance210;
			comboBoxSpecification.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSpecification.DisplayLayout.Override.CellPadding = 0;
			appearance211.BackColor = System.Drawing.SystemColors.Control;
			appearance211.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance211.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance211.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance211.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.Override.GroupByRowAppearance = appearance211;
			appearance212.TextHAlignAsString = "Left";
			comboBoxSpecification.DisplayLayout.Override.HeaderAppearance = appearance212;
			comboBoxSpecification.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSpecification.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance213.BackColor = System.Drawing.SystemColors.Window;
			appearance213.BorderColor = System.Drawing.Color.Silver;
			comboBoxSpecification.DisplayLayout.Override.RowAppearance = appearance213;
			comboBoxSpecification.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance214.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSpecification.DisplayLayout.Override.TemplateAddRowAppearance = appearance214;
			comboBoxSpecification.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSpecification.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSpecification.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSpecification.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSpecification.Editable = true;
			comboBoxSpecification.FilterString = "";
			comboBoxSpecification.HasAllAccount = false;
			comboBoxSpecification.HasCustom = false;
			comboBoxSpecification.IsDataLoaded = false;
			comboBoxSpecification.Location = new System.Drawing.Point(308, 267);
			comboBoxSpecification.MaxDropDownItems = 12;
			comboBoxSpecification.Name = "comboBoxSpecification";
			comboBoxSpecification.ShowInactiveItems = false;
			comboBoxSpecification.Size = new System.Drawing.Size(100, 20);
			comboBoxSpecification.TabIndex = 161;
			comboBoxSpecification.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSpecification.Visible = false;
			comboBoxGridLocation.AlwaysInEditMode = true;
			comboBoxGridLocation.Assigned = false;
			comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridLocation.CustomReportFieldName = "";
			comboBoxGridLocation.CustomReportKey = "";
			comboBoxGridLocation.CustomReportValueType = 1;
			comboBoxGridLocation.DescriptionTextBox = null;
			appearance215.BackColor = System.Drawing.SystemColors.Window;
			appearance215.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridLocation.DisplayLayout.Appearance = appearance215;
			comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance216.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance216.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance216.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance216.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance216;
			appearance217.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance217;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance218.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance218.BackColor2 = System.Drawing.SystemColors.Control;
			appearance218.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance218.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance218;
			comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance219.BackColor = System.Drawing.SystemColors.Window;
			appearance219.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance219;
			appearance220.BackColor = System.Drawing.SystemColors.Highlight;
			appearance220.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance220;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance221.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance221;
			appearance222.BorderColor = System.Drawing.Color.Silver;
			appearance222.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance222;
			comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
			appearance223.BackColor = System.Drawing.SystemColors.Control;
			appearance223.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance223.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance223.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance223.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance223;
			appearance224.TextHAlignAsString = "Left";
			comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance224;
			comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance225.BackColor = System.Drawing.SystemColors.Window;
			appearance225.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance225;
			comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance226.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance226;
			comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridLocation.Editable = true;
			comboBoxGridLocation.FilterString = "";
			comboBoxGridLocation.HasAllAccount = false;
			comboBoxGridLocation.HasCustom = false;
			comboBoxGridLocation.IsDataLoaded = false;
			comboBoxGridLocation.Location = new System.Drawing.Point(649, 182);
			comboBoxGridLocation.MaxDropDownItems = 12;
			comboBoxGridLocation.Name = "comboBoxGridLocation";
			comboBoxGridLocation.ShowAll = false;
			comboBoxGridLocation.ShowConsignIn = false;
			comboBoxGridLocation.ShowConsignOut = false;
			comboBoxGridLocation.ShowDefaultLocationOnly = false;
			comboBoxGridLocation.ShowInactiveItems = false;
			comboBoxGridLocation.ShowNormalLocations = true;
			comboBoxGridLocation.ShowPOSOnly = false;
			comboBoxGridLocation.ShowQuickAdd = true;
			comboBoxGridLocation.ShowWarehouseOnly = false;
			comboBoxGridLocation.Size = new System.Drawing.Size(114, 20);
			comboBoxGridLocation.TabIndex = 122;
			comboBoxGridLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridLocation.Visible = false;
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(48, 251);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 120;
			productPhotoViewer.Visible = false;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(916, 646);
			base.Controls.Add(checkBoxPriceIncludeTax);
			base.Controls.Add(labelSelectedDocs);
			base.Controls.Add(checkedListBoxPI);
			base.Controls.Add(comboBoxGridItem);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxStyle);
			base.Controls.Add(comboBoxSpecification);
			base.Controls.Add(panelDetails);
			base.Controls.Add(comboBoxGridLocation);
			base.Controls.Add(panel1);
			base.Controls.Add(productPhotoViewer);
			base.Controls.Add(labelVoided);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "SalesReceiptForm";
			Text = "Sales Receipt";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicle).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDriver).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).EndInit();
			panelExpense.ClientArea.ResumeLayout(false);
			panelExpense.ClientArea.PerformLayout();
			panelExpense.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxExpenseCode).EndInit();
			panelxml.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxBillingAddress).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingAddressID).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethod).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRegister).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panelNonTax.ResumeLayout(false);
			panelNonTax.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStyle).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSpecification).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
