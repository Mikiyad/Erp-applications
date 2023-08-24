using DevExpress.Utils;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;
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
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental
{
	public class PropertyServiceInvoice : Form, IForm
	{
		private bool allowNegativeQty = true;

		private DataTable deliveryNoteTable;

		private bool mergeMatrixPrint;

		private string clUserID = "";

		private bool allowEdit = true;

		private bool allowFOC;

		private bool allowMultiTemplate;

		private System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();

		private SalesInvoiceNIData currentData;

		private const string TABLENAME_CONST = "Sales_Invoice_NonInv";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentBillingAddressID = "";

		private ItemSourceTypes sourceDocType;

		private DataSet priceListData;

		private bool loadItemDescFromPriceList = CompanyPreferences.LoadItemDescFromPriceList;

		private bool useInlineDiscount = CompanyPreferences.UseInlineDiscount;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private decimal TaxPercent = CompanyPreferences.TaxPercent;

		private bool isWeightInvoice;

		private bool useDNotePrice;

		private bool showLotDetail = CompanyPreferences.ShowLotdetailinPrintout;

		private bool showItemdetail = CompanyPreferences.ShowItemdetail;

		private bool setlastSalesprice = CompanyPreferences.SetlastSalesprice;

		private bool userViewStaus;

		private string refDocID = "";

		private string refVoucherID = "";

		private decimal discountPercAllowed;

		private DateTime refDateTime = DateTime.MinValue;

		private DateTime lastUpdateDateTime = DateTime.MinValue;

		private DataSet timeStampStatus;

		private bool allowZeroprice = CompanyPreferences.Allowzeropriceinsale;

		private bool loadItemFeatures = CompanyPreferences.LoadItemFeatures;

		private bool activatePartsDetails = CompanyPreferences.ActivatePartsDetails;

		private bool canAccessCost;

		private bool IsTaxbasedonProfit;

		private decimal priceDiscountPercAllowed;

		private decimal TotalDiscountPercAllowed;

		private string baseFilter = "";

		private string currentCustomerID;

		private bool supressInventoryMessage;

		private bool isDataLoading;

		private bool restrictTransaction;

		private bool showedNotDiscount;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private bool isDiscountPercent;

		private bool totalChanged;

		private IContainer components;

		private ToolStrip toolStrip1;

		private System.Windows.Forms.Panel panelButtons;

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

		private System.Windows.Forms.TextBox textBoxVoucherNumber;

		private System.Windows.Forms.Label label1;

		private MMLabel mmLabel1;

		private System.Windows.Forms.TextBox textBoxRef1;

		private System.Windows.Forms.TextBox textBoxNote;

		private System.Windows.Forms.Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private System.Windows.Forms.Panel panelDetails;

		private System.Windows.Forms.Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private ProductComboBox comboBoxGridItem;

		private customersFlatComboBox comboBoxCustomer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private System.Windows.Forms.TextBox textBoxCustomerName;

		private MMTextBox textBoxBilltoAddress;

		private System.Windows.Forms.Label label2;

		private System.Windows.Forms.TextBox textBoxPONumber;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private CustomerAddressComboBox comboBoxShippingAddressID;

		private SalespersonComboBox comboBoxSalesperson;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private PaymentTermComboBox comboBoxTerm;

		private ShippingMethodsComboBox comboBoxShippingMethod;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private LocationComboBox comboBoxGridLocation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem createFromSalesOrderToolStripMenuItem;

		private CurrencySelector comboBoxCurrency;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerDueDate;

		private XPButton buttonSelectDocument;

		private System.Windows.Forms.Label labelSelectedDocs;

		private System.Windows.Forms.ListBox checkedListBoxDN;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem saveAsDraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private JobComboBox comboBoxJob1;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonDistribution;

		private System.Windows.Forms.Label label4;

		private System.Windows.Forms.TextBox textBoxRef2;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageItems;

		private UltraTabPageControl tabPageExpense;

		private DataEntryGrid dataGridExpense;

		private ExpenseCodeComboBox comboBoxGridExpenseCode;

		private CurrencyComboBox comboBoxGridCurrency;

		private ToolStripButton toolStripButtonBalance;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonInformation;

		private CostCategoryComboBox comboBoxCostCategory;

		private JobComboBox comboBoxJob;

		private ToolTipController toolTipController1;

		private CostCategoryComboBox ComboBoxitemcostCategory;

		private JobComboBox ComboBoxitemJob;

		private MMTextBox textBoxShipto;

		private CustomerAddressComboBox comboBoxBillingAddress;

		private Timer timer1;

		private ToolStripButton toolStripButtonMultiPreview;

		private UltraFormattedLinkLabel ultraFormattedLinkCurrency;

		private UltraFormattedLinkLabel ultraFormattedLinkCostCategory;

		private UltraFormattedLinkLabel ultraFormattedLinkProject;

		private UltraFormattedLinkLabel ultraFormattedLinkShipping;

		private UltraFormattedLinkLabel ultraFormattedLinkPaymentTerm;

		private UltraToolTipManager ultraToolTipManager1;

		private ToolStripButton toolStripButtonInfo;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private ProductSpecificationComboBox comboBoxSpecification;

		private ProductStyleComboBox comboBoxStyle;

		private GroupBox groupBoxTax;

		private TableLayoutPanel tableLayoutPanel1;

		private UltraFormattedLinkLabel labelTaxGroup;

		private TaxGroupComboBox comboBoxPayeeTaxGroup;

		private ToolStripMenuItem grantEditPermissionToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator8;

		private System.Windows.Forms.CheckBox checkBoxPriceIncludeTax;

		private System.Windows.Forms.Panel panel1;

		private System.Windows.Forms.Label label7;

		private PercentTextBox textBoxDiscountPercent;

		private AmountTextBox textBoxDiscountAmount;

		private System.Windows.Forms.Label label11;

		private System.Windows.Forms.Label label5;

		private AmountTextBox textBoxSubtotal;

		private System.Windows.Forms.Panel panelNonTax;

		private System.Windows.Forms.Panel panelTotal;

		private System.Windows.Forms.Label label9;

		private AmountTextBox textBoxRoundOff;

		private AmountTextBox textBoxTotal;

		private System.Windows.Forms.Label label8;

		private System.Windows.Forms.Label label14;

		private AmountTextBox textBoxTotalExpense;

		private UltraFormattedLinkLabel linkLabelTax;

		private AmountTextBox textBoxTaxAmount;

		private System.Windows.Forms.Label labelReportTo;

		private ToolStripButton toolStripButtonPaymentAllocation;

		private ToolStripSeparator toolStripSeparator9;

		private ToolStripButton toolStripButtonExcelImport;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelUnit;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelProperty;

		private PropertyComboBox comboBoxProperty;

		private PropertyUnitComboBox ComboBoxpropertyUnit;

		private PropertyAgentComboBox comboBoxPropertyAgent;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelPropertyAgent;

		private PropertyIncomeCodeComboBox comboBoxGridIncomeCode;

		private System.Windows.Forms.TextBox textBoxUnitName;

		private System.Windows.Forms.TextBox textBoxPropertyName;

		private System.Windows.Forms.Panel panel2;

		private ToolStripMenuItem createFromRentalRegistrationToolStripMenuItem;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2010;

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
				bool flag3;
				bool enabled;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
					customersFlatComboBox customersFlatComboBox = comboBoxCustomer;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					flag3 = (textBoxVoucherNumber.Enabled = true);
					enabled = (sysDocComboBox.Enabled = flag3);
					customersFlatComboBox.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					if (!IsVoid)
					{
						buttonVoid.Enabled = true;
					}
					if (sourceDocType != 0)
					{
						comboBoxCustomer.Enabled = false;
					}
					else
					{
						comboBoxCustomer.Enabled = true;
					}
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = false);
					sysDocComboBox2.Enabled = enabled;
				}
				ToolStripButton toolStripButton = toolStripButtonPrint;
				ToolStripButton toolStripButton2 = toolStripButtonPreview;
				flag3 = (toolStripButtonMultiPreview.Enabled = !isNewRecord);
				enabled = (toolStripButton2.Enabled = flag3);
				toolStripButton.Enabled = enabled;
				toolStripButtonAttach.Enabled = !value;
				toolStripButtonDistribution.Enabled = !value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
					grantEditPermissionToolStripMenuItem.Visible = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
				SetApprovalStatus();
			}
		}

		private bool RestrictTransaction
		{
			get
			{
				return restrictTransaction;
			}
			set
			{
				restrictTransaction = value;
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
				dataGridExpense.Enabled = !value;
				panelDetails.Enabled = !value;
				dataGridItems.Enabled = !value;
				panel1.Enabled = !value;
				textBoxNote.Enabled = !value;
				buttonSave.Enabled = !value;
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

		public PropertyServiceInvoice()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			allowNegativeQty = CompanyPreferences.AllowSalesInvoiceNegativeQty;
			comboBoxGridExpenseCode.ExpenseCodeType = ExpenseCodeTypes.InvoiceCharge;
			useDNotePrice = CompanyPreferences.CheckCreditLimitInDeliveryNote;
			ultraFormattedLinkProject.Visible = (ultraFormattedLinkCostCategory.Visible = (comboBoxJob.Visible = (comboBoxCostCategory.Visible = useJobCosting)));
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
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
			productPhotoViewer.EnlargeRequested += productPhotoViewer_EnlargeRequested;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			textBoxDiscountAmount.Validating += textBoxDiscountAmount_Validating;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxDiscountAmount.Validated += textBoxDiscountAmount_Validated;
			textBoxDiscountPercent.Validated += textBoxDiscountPercent_Validated;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			comboBoxTerm.SelectedIndexChanged += comboBoxTerm_SelectedIndexChanged;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			dataGridItems.KeyPress += dataGridItems_KeyDown;
			comboBoxPayeeTaxGroup.SelectedIndexChanged += comboBoxPayeeTaxGroup_SelectedIndexChanged;
			comboBoxBillingAddress.SelectedIndexChanged += comboBoxBillingAddress_SelectedIndexChanged;
			comboBoxShippingAddressID.SelectedIndexChanged += comboBoxShippingAddressID_SelectedIndexChanged;
			ComboBoxpropertyUnit.SelectedIndexChanged += comboBoxPropertyUnit_SelectedIndexChanged;
		}

		private void comboBoxPropertyUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ComboBoxpropertyUnit.SelectedID != "")
			{
				string selectedID = ComboBoxpropertyUnit.SelectedID;
				DateTime value = dateTimePickerDate.Value;
				DataSet dataSet = new DataSet();
				dataSet = Factory.PropertyServiceSystem.GetTenantByUnit(selectedID, value);
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					comboBoxCustomer.SelectedID = dataSet.Tables[0].Rows[0]["CustomerID"].ToString();
				}
			}
			else
			{
				comboBoxCustomer.SelectedID = "";
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

		private void dataGridExpense_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGridExpense_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private void dataGridExpense_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridExpense.ActiveCell.Column.Key.ToString() == "Amount")
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridExpense.ActiveCell.Value.ToString(), out result);
				result = Math.Round(result, Global.CurDecimalPoints);
				dataGridExpense.ActiveCell.Value = result;
			}
		}

		private void dataGridExpense_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridExpense.ActiveRow;
			if (activeRow != null)
			{
				if (activeRow.Cells["Amount"].Value.ToString() == "")
				{
					activeRow.Cells["Amount"].Value = 0;
				}
				if (activeRow.Cells["Expense Code"].Value == null || activeRow.Cells["Expense Code"].Value.ToString() == string.Empty)
				{
					ErrorHelper.InformationMessage("Please select an expense code for the row");
					e.Cancel = true;
					activeRow.Cells["Expense Code"].Activate();
				}
			}
		}

		private void ApplyCustomerSettings()
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				isWeightInvoice = false;
			}
			else
			{
				object selectedCellValue = comboBoxCustomer.GetSelectedCellValue("IsWeightInvoice");
				if (selectedCellValue != null && selectedCellValue != DBNull.Value && selectedCellValue != "")
				{
					isWeightInvoice = Convert.ToBoolean(selectedCellValue.ToString());
				}
				else
				{
					isWeightInvoice = false;
				}
			}
			if (isWeightInvoice)
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Hidden = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
			}
		}

		private void dataGridExpense_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridExpense.ActiveRow != null)
				{
					if (e.Cell.Column.Key == "Expense Code")
					{
						dataGridExpense.ActiveRow.Cells["Description"].Value = comboBoxGridExpenseCode.SelectedName;
						ItemTaxOptions taxOption = comboBoxGridExpenseCode.TaxOption;
						dataGridExpense.ActiveRow.Cells["TaxOption"].Value = taxOption;
						switch (taxOption)
						{
						case ItemTaxOptions.BasedOnCustomer:
							dataGridExpense.ActiveRow.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
							break;
						case ItemTaxOptions.Taxable:
							dataGridExpense.ActiveRow.Cells["TaxGroupID"].Value = comboBoxGridExpenseCode.TaxGroupID;
							break;
						case ItemTaxOptions.NonTaxable:
							dataGridExpense.ActiveRow.Cells["TaxGroupID"].Value = DBNull.Value;
							break;
						}
						dataGridExpense.ActiveRow.Cells["Amount"].Value = comboBoxGridExpenseCode.ExpenseRate;
					}
					else if (e.Cell.Column.Key == "TaxGroupID")
					{
						ItemTaxOptions itemTaxOption = ItemTaxOptions.NonTaxable;
						if (!e.Cell.Row.Cells["TaxOption"].Value.IsNullOrEmpty())
						{
							itemTaxOption = (ItemTaxOptions)byte.Parse(e.Cell.Row.Cells["TaxOption"].Value.ToString());
						}
						TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOption, comboBoxGridExpenseCode.TaxGroupID);
						e.Cell.Row.Cells["Tax"].Tag = tag;
					}
					if (e.Cell.Column.Key == "Amount")
					{
						decimal result = default(decimal);
						decimal.TryParse(e.Cell.Value.ToString(), out result);
						decimal subtotal = decimal.Parse(textBoxTotalExpense.Text);
						decimal tradeDiscount = default(decimal);
						decimal result2 = default(decimal);
						decimal result3 = default(decimal);
						decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result3);
						decimal.TryParse(e.Cell.Row.Cells["Cost"].Value.ToString(), out result2);
						result2 = result3 * result2;
						UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result2, result, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
						CalculateTotal();
					}
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
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			string text = "";
			if (dataGridItems.Rows.Count > 0 && dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item code"].Value != null)
			{
				text = activeRow.Cells["Item code"].Value.ToString();
			}
			if (text != "" && showItemdetail && userViewStaus)
			{
				DisplayItemDetails(text);
			}
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (allowNegativeQty)
			{
				return;
			}
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

		private void comboBoxTerm_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				SetDueDate();
			}
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				SetDueDate();
			}
		}

		private void textBoxDiscountPercent_Validated(object sender, EventArgs e)
		{
			if (textBoxDiscountPercent.Modified)
			{
				CalculateTotal();
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

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
			if (e.KeyCode == Keys.G && e.Alt)
			{
				dataGridItems.Focus();
				comboBoxGridItem.Focus();
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
			if (e.KeyCode == Keys.F6 && dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				ProductQuantityForm productQuantityForm = new ProductQuantityForm();
				productQuantityForm.LoadData(productID);
				productQuantityForm.ShowDialog();
			}
		}

		private void comboBoxPayeeTaxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			CalculateAllRowsTaxes();
			CalculateTotal();
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
			if (ultraGridColumn.Key == "Income Code")
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
							dataGridItems.ActiveRow.Cells["Price"].Value = num2;
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
				result3 = Math.Round(result * result2, Global.CurDecimalPoints);
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

		private void dataGridExpense_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridExpense.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Tax" && dataGridExpense.ActiveRow != null)
			{
				TaxTransactionData taxData = new TaxTransactionData();
				if (dataGridExpense.ActiveRow.Cells["Tax"].Tag != null)
				{
					taxData = (dataGridExpense.ActiveRow.Cells["Tax"].Tag as TaxTransactionData);
				}
				TaxDistibutionDialog taxDistibutionDialog = new TaxDistibutionDialog();
				taxDistibutionDialog.TaxData = taxData;
				taxDistibutionDialog.ShowDialog();
			}
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				bool result = false;
				bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Customer", "IsHold", "CustomerID", comboBoxCustomer.SelectedID).ToString(), out result);
				if (result && IsNewRecord)
				{
					ErrorHelper.WarningMessage("This customer is on hold status and does not allow transaction.");
				}
				else
				{
					textBoxCustomerName.Text = comboBoxCustomer.SelectedName;
					CurrentCustomerID = comboBoxCustomer.SelectedID;
					comboBoxBillingAddress.Clear();
					comboBoxShippingAddressID.Clear();
					LoadCustomerPriceList();
					if (!isDataLoading && comboBoxCustomer.SelectedID != "")
					{
						comboBoxCurrency.SelectedID = comboBoxCustomer.DefaultCurrencyID;
						comboBoxCurrency.GetLastRate();
						comboBoxTerm.SelectedID = comboBoxCustomer.DefaultPaymentTerm;
						comboBoxSalesperson.SelectedID = comboBoxCustomer.DefaultSalesPersonID;
						if (comboBoxSalesperson.SelectedID == "")
						{
							comboBoxSalesperson.SelectedID = Security.DefaultSalespersonID;
						}
						comboBoxShippingMethod.SelectedID = comboBoxCustomer.DefaultShippingMethod;
						string defaultShipToAddress = comboBoxCustomer.DefaultShipToAddress;
						comboBoxShippingAddressID.IsDataLoaded = true;
						comboBoxShippingAddressID.Filter(comboBoxCustomer.SelectedID);
						if (useJobCosting)
						{
							comboBoxJob.Clear();
							comboBoxJob.Filter(comboBoxCustomer.SelectedID.Trim());
						}
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
						if (CompanyPreferences.IsTax)
						{
							comboBoxPayeeTaxGroup.Clear();
							if (comboBoxCustomer.TaxOption == PayeeTaxOptions.Taxable)
							{
								comboBoxPayeeTaxGroup.SelectedID = comboBoxCustomer.DefaultTaxGroupID;
							}
						}
						else
						{
							comboBoxPayeeTaxGroup.Clear();
						}
						SetDueDate();
					}
					object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer", "DeliveryInstructions", "CustomerID", comboBoxCustomer.SelectedID);
					UltraToolTipInfo ultraToolTipInfo = new UltraToolTipInfo();
					ultraToolTipInfo.ToolTipText = "";
					if (fieldValue != null)
					{
						ultraToolTipInfo.ToolTipText = fieldValue.ToString();
					}
					if (!fieldValue.IsNullOrEmpty())
					{
						toolStripButtonInfo.Visible = true;
						ultraToolTipManager1.SetUltraToolTip(textBoxCustomerName, ultraToolTipInfo);
					}
					else
					{
						ultraToolTipInfo.ToolTipText = "";
						ultraToolTipManager1.SetUltraToolTip(textBoxCustomerName, ultraToolTipInfo);
						toolStripButtonInfo.Visible = false;
					}
					ApplyCustomerSettings();
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

		private void SetDueDate()
		{
			dateTimePickerDueDate.Value = Global.CalculateDueDate(dateTimePickerDate.Value, comboBoxTerm.SelectedID);
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (!isDataLoading)
				{
					if (isNewRecord)
					{
						textBoxVoucherNumber.Text = GetNextVoucherNumber();
					}
					formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
					object fieldValue = Factory.DatabaseSystem.GetFieldValue("System_Document", "AllowFOC", "SysDocID", comboBoxSysDoc.SelectedID);
					if (fieldValue != null && fieldValue.ToString() != "")
					{
						allowFOC = bool.Parse(fieldValue.ToString());
					}
					if (allowFOC)
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Hidden = false;
						dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Hidden = false;
						dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].Hidden = false;
					}
					else
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Hidden = true;
						dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Hidden = true;
						dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].Hidden = true;
					}
					comboBoxCustomer.FilterSysDocID = comboBoxSysDoc.SelectedID;
					comboBoxGridItem.FilterSysDocID = comboBoxSysDoc.SelectedID;
					allowMultiTemplate = false;
					object fieldValue2 = Factory.DatabaseSystem.GetFieldValue("System_Document", "AllowMultiTemplate", "SysDocID", comboBoxSysDoc.SelectedID);
					if (fieldValue2 != null && fieldValue2.ToString() != "")
					{
						allowMultiTemplate = bool.Parse(fieldValue2.ToString());
					}
					if (allowMultiTemplate)
					{
						toolStripButtonMultiPreview.Visible = true;
					}
					else
					{
						toolStripButtonMultiPreview.Visible = false;
					}
					System.Windows.Forms.CheckBox checkBox = checkBoxPriceIncludeTax;
					bool visible = checkBoxPriceIncludeTax.Checked = comboBoxSysDoc.IsPriceIncludeTax;
					checkBox.Visible = visible;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridItems.ActiveRow == null)
				{
					return;
				}
				if (e.Cell.Column.Key == "Income Code")
				{
					dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridIncomeCode.SelectedName;
					ItemTaxOptions taxOption = comboBoxGridIncomeCode.TaxOption;
					dataGridItems.ActiveRow.Cells["TaxOption"].Value = taxOption;
					switch (taxOption)
					{
					case ItemTaxOptions.BasedOnCustomer:
						dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
						break;
					case ItemTaxOptions.Taxable:
						dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = comboBoxGridIncomeCode.TaxGroupID;
						break;
					case ItemTaxOptions.NonTaxable:
						dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = DBNull.Value;
						break;
					}
				}
				else if (e.Cell.Column.Key == "TaxGroupID")
				{
					ItemTaxOptions itemTaxOption = ItemTaxOptions.BasedOnCustomer;
					if (!e.Cell.Row.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						itemTaxOption = (ItemTaxOptions)byte.Parse(e.Cell.Row.Cells["TaxOption"].Value.ToString());
					}
					TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOption, comboBoxGridItem.TaxGroupID);
					e.Cell.Row.Cells["Tax"].Tag = tag;
				}
				else if (e.Cell.Column.Key == "Location" && e.Cell.Row.Cells["Quantity"].Tag != null)
				{
					e.Cell.Row.Cells["Quantity"].Tag = null;
					e.Cell.Row.Cells["Quantity"].Value = 0;
					e.Cell.Row.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
				}
				if (e.Cell.Column.Key == "Unit" && dataGridItems.ActiveCell != null && (priceListData == null || (dataGridItems.ActiveRow.Cells["Unit"].Value == null && e.Cell.Value.ToString() == "")))
				{
					return;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			try
			{
				string key;
				decimal result2;
				if (dataGridItems.ActiveRow != null)
				{
					key = e.Cell.Column.Key;
					decimal result = default(decimal);
					result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					decimal result6 = default(decimal);
					decimal result7 = default(decimal);
					decimal result8 = default(decimal);
					decimal result9 = default(decimal);
					decimal result10 = default(decimal);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].OriginalValue.ToString(), out result9);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].OriginalValue.ToString(), out result10);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result2);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Discount"].Value.ToString(), out result4);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["FOCQuantity"].Value.ToString(), out result5);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["WeightQuantity"].Value.ToString(), out result6);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["WeightPrice"].Value.ToString(), out result7);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Tax"].Value.ToString(), out result8);
					dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
					dataGridItems.ActiveRow.Cells["Unit"].OriginalValue.ToString();
					if ((key == "Quantity" || key == "FOCQuantity") && dataGridItems.ActiveCell != null && (dataGridItems.ActiveCell.Column.Key == "Quantity" || dataGridItems.ActiveCell.Column.Key == "FOCQuantity"))
					{
						if (!isWeightInvoice)
						{
							result3 = (useInlineDiscount ? Math.Round((result - result5) * (result2 - result4), Global.CurDecimalPoints) : Math.Round((result - result5) * result2, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
							dataGridItems.ActiveRow.Cells["InvQuantity"].Value = result - result5;
						}
						goto IL_0df4;
					}
					if (key == "WeightQuantity" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "WeightQuantity")
					{
						if (isWeightInvoice)
						{
							result3 = Math.Round(result6 * result7, Global.CurDecimalPoints);
							dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
							dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(result3 / result, 5);
						}
						goto IL_0df4;
					}
					if (key == "Price" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Price")
					{
						if (!isWeightInvoice)
						{
							result3 = (useInlineDiscount ? Math.Round((result - result5) * (result2 - result4), Global.CurDecimalPoints) : Math.Round((result - result5) * result2, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
						}
						goto IL_0df4;
					}
					if (key == "Unit" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Unit")
					{
						result3 = (useInlineDiscount ? Math.Round(result * result2 - result4, Global.CurDecimalPoints) : Math.Round(result * result2, Global.CurDecimalPoints));
						dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
						goto IL_0df4;
					}
					if (key == "Location" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Location")
					{
						result3 = (useInlineDiscount ? Math.Round(result * result2 - result4, Global.CurDecimalPoints) : Math.Round(result * result2, Global.CurDecimalPoints));
						dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
						goto IL_0df4;
					}
					if (key == "WeightPrice" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "WeightPrice")
					{
						if (isWeightInvoice)
						{
							result3 = Math.Round(result6 * result7, 2);
							dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
							dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(result3 / result, 5);
						}
						goto IL_0df4;
					}
					if (key == "Discount" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Discount")
					{
						result3 = Math.Round(result2 - result4, Global.CurDecimalPoints);
						dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
						goto IL_0df4;
					}
					if (!(key == "Amount") || dataGridItems.ActiveCell == null || !(dataGridItems.ActiveCell.Column.Key == "Amount"))
					{
						if (key == "Income Code")
						{
							if (dataGridItems.ActiveRow.Cells["Quantity"].Value == null || dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString() == "")
							{
								dataGridItems.ActiveRow.Cells["Quantity"].Value = result;
							}
							result3 = (useInlineDiscount ? Math.Round((result - result5) * (result2 - result4), Global.CurDecimalPoints) : Math.Round((result - result5) * result2, Global.CurDecimalPoints));
							dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
						}
						goto IL_0df4;
					}
					if (result == 0m)
					{
						result = 1m;
						dataGridItems.ActiveRow.Cells["Quantity"].Value = result;
					}
					if (isWeightInvoice && result6 == 0m)
					{
						result6 = 1m;
						dataGridItems.ActiveRow.Cells["WeightQuantity"].Value = result6;
					}
					ItemTypes itemTypes = ItemTypes.Inventory;
					if (dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString() != "")
					{
						itemTypes = (ItemTypes)checked((byte)int.Parse(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString()));
					}
					if ((itemTypes == ItemTypes.Discount && result3 > 0m) || (result < 0m && result3 > 0m))
					{
						result3 = -1m * Math.Abs(result3);
						dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
					}
					result2 = Math.Round((result3 + result4) / result, 4);
					if (isWeightInvoice)
					{
						result7 = Math.Round(result3 / result6, 5);
					}
					if (!(result3 < 0m))
					{
						goto IL_0cd0;
					}
					if (!isWeightInvoice)
					{
						result = -1m * Math.Abs(result);
						dataGridItems.ActiveRow.Cells["Quantity"].Value = result;
						goto IL_0cd0;
					}
					ErrorHelper.WarningMessage("Negative amount is not allowed when invoicing on weight.");
					e.Cell.Value = 0;
				}
				goto end_IL_0310;
				IL_0df4:
				if (key == "Amount")
				{
					decimal result11 = default(decimal);
					decimal.TryParse(e.Cell.Value.ToString(), out result11);
					decimal subtotal = decimal.Parse(textBoxSubtotal.Text);
					decimal tradeDiscount = decimal.Parse(textBoxDiscountAmount.Text);
					decimal result12 = default(decimal);
					decimal result13 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result13);
					decimal.TryParse(e.Cell.Row.Cells["Cost"].Value.ToString(), out result12);
					result12 = result13 * result12;
					UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result12, result11, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
					CalculateTotal();
				}
				if (key == "Cost")
				{
					decimal result14 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Amount"].Value.ToString(), out result14);
					decimal subtotal2 = decimal.Parse(textBoxSubtotal.Text);
					decimal tradeDiscount2 = decimal.Parse(textBoxDiscountAmount.Text);
					decimal result15 = default(decimal);
					decimal result16 = default(decimal);
					decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result16);
					decimal.TryParse(e.Cell.Row.Cells["Cost"].Value.ToString(), out result15);
					result15 = result16 * result15;
					UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result15, result14, subtotal2, tradeDiscount2, checkBoxPriceIncludeTax.Checked);
					CalculateTotal();
				}
				checked
				{
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
					goto end_IL_0310;
				}
				IL_0cd0:
				dataGridItems.ActiveRow.Cells["Price"].Value = Math.Abs(result2);
				goto IL_0df4;
				end_IL_0310:;
			}
			catch (Exception e3)
			{
				dataGridItems.ActiveRow.Cells["Price"].Value = 0;
				dataGridItems.ActiveRow.Cells["Amount"].Value = 0;
				ErrorHelper.ProcessError(e3);
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

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			comboBoxGridIncomeCode.IsLoadingData = true;
		}

		private void comboBoxGridItem_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				if (dataGridItems.ActiveCell.Column.Key == "Income Code" && dataGridItems.ActiveRow.Cells["Income Code"].Value.ToString() == "")
				{
					dataGridItems.ActiveRow.Cells["Description"].Value = "";
				}
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
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow == null)
			{
				return;
			}
			if (activeRow.Cells["Income Code"].Value.ToString() == "" && activeRow.Cells["Quantity"].Value.ToString() != "")
			{
				ErrorHelper.InformationMessage("Please select an item.");
				e.Cancel = true;
				activeRow.Cells["Income Code"].Activate();
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
			if (dataGridItems.ActiveCell != null)
			{
				if (e.Cell.DataChanged && e.Cell.Column.Key == "Quantity" && dataGridItems.ActiveCell.Column.Key == "Quantity" && !AllocateQuantityToLot(e.Cell))
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
						return;
					}
				}
			}
			if (activeRow != null && e.Cell.Column.Key == "FOCQuantity")
			{
				decimal result3 = default(decimal);
				decimal result4 = default(decimal);
				decimal.TryParse(e.Cell.Row.Cells["quantity"].Value.ToString(), out result3);
				decimal.TryParse(e.NewValue.ToString(), out result4);
				if (result4 > result3)
				{
					ErrorHelper.InformationMessage("FOC quantity cannot be greater than invoiced quantity.");
					e.Cancel = true;
				}
			}
		}

		private void SetQuantity(UltraGridRow row)
		{
			ItemTypes itemTypes = ItemTypes.Inventory;
			if (row.Cells["ItemType"].Value.ToString() != "")
			{
				itemTypes = (ItemTypes)checked((byte)int.Parse(row.Cells["ItemType"].Value.ToString()));
			}
			if ((CompanyPreferences.NegativeQuantityAction == 1 && itemTypes != ItemTypes.ConsignmentItem) || supressInventoryMessage)
			{
				return;
			}
			string sysDocID = "";
			string voucherID = "";
			if (!IsNewRecord)
			{
				sysDocID = comboBoxSysDoc.SelectedID;
				voucherID = textBoxVoucherNumber.Text;
			}
			if (Factory.ProductSystem.IsSufficientQuantityOnhand(row.Cells["Item Code"].Value.ToString(), row.Cells["Unit"].Value.ToString(), row.Cells["Location"].Value.ToString(), "Sales_Invoice_Detail", sysDocID, voucherID, decimal.Parse(row.Cells["Quantity"].Value.ToString())))
			{
				return;
			}
			if (itemTypes == ItemTypes.ConsignmentItem)
			{
				ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
				row.Cells["Quantity"].Value = 0;
			}
			else if (CompanyPreferences.NegativeQuantityAction == 2)
			{
				if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You do not have sufficient quantity in this location. Do you want to continue?") != DialogResult.Yes)
				{
					row.Cells["Quantity"].Value = 0;
				}
			}
			else if (CompanyPreferences.NegativeQuantityAction == 3)
			{
				ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
				row.Cells["Quantity"].Value = 0;
			}
		}

		private bool AllocateQuantityToLot(UltraGridCell cell)
		{
			bool result = false;
			bool result2 = false;
			if (dataGridItems.ActiveCell == null)
			{
				return true;
			}
			if (dataGridItems.ActiveRow.Cells["IsTrackLot"].Value != null)
			{
				bool.TryParse(dataGridItems.ActiveRow.Cells["IsTrackLot"].Value.ToString(), out result);
			}
			if (dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value != null)
			{
				bool.TryParse(dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value.ToString(), out result2);
			}
			ItemTypes itemTypes = ItemTypes.Inventory;
			if (dataGridItems.ActiveRow.Cells["ItemType"].Value != null && !string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString()))
			{
				itemTypes = (ItemTypes)checked((byte)int.Parse(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString()));
			}
			if (result || itemTypes == ItemTypes.ConsignmentItem)
			{
				if (dataGridItems.ActiveRow.Cells["Location"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select a location first.");
					return false;
				}
				IssueLotSelectionForm issueLotSelectionForm = new IssueLotSelectionForm();
				issueLotSelectionForm.ProductID = cell.Row.Cells["Item Code"].Value.ToString();
				issueLotSelectionForm.ProductDescription = cell.Row.Cells["Description"].Value.ToString();
				issueLotSelectionForm.LocationID = cell.Row.Cells["Location"].Value.ToString();
				issueLotSelectionForm.RowQuantity = float.Parse(cell.Value.ToString());
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
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Income Code")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridIncomeCode.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridIncomeCode.QuickAddItem();
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
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity" || dataGridItems.ActiveCell.Column.Key.ToString() == "FOCQuantity")
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
					currentData = new SalesInvoiceNIData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.SalesInvoiceTable.Rows[0] : currentData.SalesInvoiceTable.NewRow();
				if (clUserID != "")
				{
					dataRow["CLUserID"] = clUserID;
				}
				else
				{
					dataRow["CLUserID"] = DBNull.Value;
				}
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["DueDate"] = dateTimePickerDueDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["SalesFlow"] = CompanyPreferences.LocalSalesFlow;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Reference2"] = textBoxRef2.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["CustomerAddress"] = textBoxBilltoAddress.Text;
				dataRow["ShipToAddress"] = textBoxShipto.Text;
				dataRow["IsCash"] = false;
				dataRow["IsWeightInvoice"] = isWeightInvoice;
				dataRow["SourceDocType"] = sourceDocType;
				if (comboBoxShippingAddressID.SelectedID != "")
				{
					dataRow["ShippingAddressID"] = comboBoxShippingAddressID.SelectedID;
				}
				else
				{
					dataRow["ShippingAddressID"] = DBNull.Value;
				}
				if (comboBoxBillingAddress.SelectedID != "")
				{
					dataRow["BillingAddressID"] = comboBoxBillingAddress.SelectedID;
				}
				else
				{
					dataRow["BillingAddressID"] = DBNull.Value;
				}
				if (comboBoxTerm.SelectedID != "")
				{
					dataRow["TermID"] = comboBoxTerm.SelectedID;
				}
				else
				{
					dataRow["TermID"] = DBNull.Value;
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
				if (comboBoxShippingMethod.SelectedID != "")
				{
					dataRow["ShippingMethodID"] = comboBoxShippingMethod.SelectedID;
				}
				else
				{
					dataRow["ShippingMethodID"] = DBNull.Value;
				}
				if (comboBoxSalesperson.SelectedID != "")
				{
					dataRow["SalespersonID"] = comboBoxSalesperson.SelectedID;
				}
				else
				{
					dataRow["SalespersonID"] = DBNull.Value;
				}
				dataRow["ReportTo"] = labelReportTo.Text;
				dataRow["CurrentUser"] = Global.CurrentUser.ToString();
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
				if (comboBoxProperty.SelectedID != "")
				{
					dataRow["PropertyID"] = comboBoxProperty.SelectedID;
				}
				else
				{
					dataRow["PropertyID"] = DBNull.Value;
				}
				if (ComboBoxpropertyUnit.SelectedID != "")
				{
					dataRow["PropertyUnitID"] = ComboBoxpropertyUnit.SelectedID;
				}
				else
				{
					dataRow["PropertyUnitID"] = DBNull.Value;
				}
				if (comboBoxPropertyAgent.SelectedID != "")
				{
					dataRow["AgentID"] = comboBoxPropertyAgent.SelectedID;
				}
				else
				{
					dataRow["AgentID"] = DBNull.Value;
				}
				dataRow["InvoiceType"] = (byte)2;
				dataRow["PONumber"] = textBoxPONumber.Text;
				dataRow["Discount"] = textBoxDiscountAmount.Text;
				dataRow["TaxAmount"] = textBoxTaxAmount.Text;
				decimal num = default(decimal);
				num = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
				dataRow["RoundOff"] = num;
				dataRow["Total"] = textBoxTotal.Text;
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
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.SalesInvoiceDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["ProductID"] = row.Cells["Income Code"].Value.ToString();
					dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
					if (!dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Hidden && row.Cells["Quantity"].Value != null && row.Cells["Quantity"].Value.ToString() != "")
					{
						dataRow2["FOCQuantity"] = ((!string.IsNullOrEmpty(row.Cells["FOCQuantity"].Value.ToString())) ? float.Parse(row.Cells["FOCQuantity"].Value.ToString()) : 0f);
					}
					if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
					{
						dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
					}
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
					if (isWeightInvoice)
					{
						dataRow2["WeightQuantity"] = row.Cells["WeightQuantity"].Value.ToString();
						dataRow2["WeightPrice"] = row.Cells["WeightPrice"].Value.ToString();
					}
					dataRow2["SpecificationID"] = row.Cells["SpecificationID"].Value.ToString();
					dataRow2["StyleID"] = row.Cells["Style"].Value.ToString();
					dataRow2["Attribute1"] = row.Cells["Attribute1"].Value.ToString();
					dataRow2["Attribute2"] = row.Cells["Attribute2"].Value.ToString();
					dataRow2["Attribute3"] = row.Cells["Attribute3"].Value.ToString();
					dataRow2["MatrixParentID"] = row.Cells["MatrixParentID"].Value.ToString();
					dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
					if (row.Cells["ItemType"].Value != null && row.Cells["ItemType"].Value.ToString() != "")
					{
						dataRow2["ItemType"] = row.Cells["ItemType"].Value.ToString();
					}
					else
					{
						dataRow2["ItemType"] = DBNull.Value;
					}
					dataRow2["UnitPrice"] = row.Cells["Price"].Value.ToString();
					if (row.Cells["Cost"].Value != null && row.Cells["Cost"].Value.ToString() != "")
					{
						dataRow2["Cost"] = row.Cells["Cost"].Value.ToString();
					}
					else
					{
						dataRow2["Cost"] = DBNull.Value;
					}
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
					dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
					if (row.Cells["RowSourceType"].Value != null && row.Cells["RowSourceType"].Value.ToString() != "")
					{
						dataRow2["RowSource"] = row.Cells["RowSourceType"].Value.ToString();
					}
					dataRow2["RowIndex"] = row.Index;
					if (row.Cells["Job"].Value != null && row.Cells["Job"].Value.ToString() != "")
					{
						dataRow2["JobID"] = row.Cells["Job"].Value.ToString();
					}
					else
					{
						dataRow2["JobID"] = DBNull.Value;
					}
					if (row.Cells["CostCategory"].Value != null && row.Cells["CostCategory"].Value.ToString() != "")
					{
						dataRow2["CostCategoryID"] = row.Cells["CostCategory"].Value.ToString();
					}
					else
					{
						dataRow2["CostCategoryID"] = DBNull.Value;
					}
					dataRow2.EndEdit();
					currentData.SalesInvoiceDetailTable.Rows.Add(dataRow2);
				}
				currentData.Tables["Tax_Detail"].Rows.Clear();
				string selectedID = comboBoxSysDoc.SelectedID;
				string text = textBoxVoucherNumber.Text;
				int num2 = 0;
				checked
				{
					foreach (UltraGridRow row2 in dataGridItems.Rows)
					{
						if (row2.Cells["Tax"].Tag != null)
						{
							TaxHelper.CreateTaxRows(currentData, row2.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Items, selectedID, text, num2, comboBoxCurrency.SelectedID, comboBoxCurrency.Rate);
						}
						num2++;
					}
					num2 = 0;
					foreach (UltraGridRow row3 in dataGridExpense.Rows)
					{
						if (row3.Cells["Tax"].Tag != null)
						{
							TaxHelper.CreateTaxRows(currentData, row3.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Expenses, selectedID, text, num2, comboBoxCurrency.SelectedID, comboBoxCurrency.Rate);
						}
						num2++;
					}
					if (textBoxTaxAmount.Tag != null)
					{
						TaxHelper.CreateTaxRows(currentData, textBoxTaxAmount.Tag as TaxTransactionData, TaxDetailLevel.Transaction, selectedID, text, -1, comboBoxCurrency.SelectedID, comboBoxCurrency.Rate);
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
				dataTable.Columns.Add("Job");
				dataTable.Columns.Add("CostCategory");
				dataTable.Columns.Add("Income Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Remarks");
				dataTable.Columns.Add("SOSysDocID");
				dataTable.Columns.Add("SOVoucherID");
				dataTable.Columns.Add("SORowIndex", typeof(int));
				dataTable.Columns.Add("Attribute1");
				dataTable.Columns.Add("Attribute2");
				dataTable.Columns.Add("Attribute3");
				dataTable.Columns.Add("Brand");
				dataTable.Columns.Add("MatrixParentID");
				dataTable.Columns.Add("IsTrackLot");
				dataTable.Columns.Add("IsTrackSerial");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("SpecificationID");
				dataTable.Columns.Add("Style");
				dataTable.Columns.Add("ItemType");
				dataTable.Columns.Add("RowSourceType", typeof(int));
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("TaxGroupID");
				dataTable.Columns.Add("TaxOption", typeof(byte));
				dataTable.Columns.Add("Tax", typeof(decimal));
				dataTable.Columns.Add("IsDNRow");
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("FOCQuantity", typeof(decimal));
				dataTable.Columns.Add("InvQuantity", typeof(decimal));
				dataTable.Columns.Add("ConsignmentNo");
				dataTable.Columns.Add("WeightQuantity", typeof(decimal));
				dataTable.Columns.Add("WeightPrice", typeof(decimal));
				dataTable.Columns.Add("HiddenAverageCost", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Cost", typeof(decimal));
				dataTable.Columns.Add("Discount", typeof(decimal));
				dataTable.Columns.Add("DefaultDescription");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("TaxTotal", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["Unit"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["Location"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["Job"];
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["Brand"];
				UltraGridColumn ultraGridColumn10 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"];
				UltraGridColumn ultraGridColumn11 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"];
				UltraGridColumn ultraGridColumn12 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"];
				UltraGridColumn ultraGridColumn13 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"];
				UltraGridColumn ultraGridColumn14 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Hidden = true;
				bool flag4 = ultraGridColumn14.Hidden = flag2;
				bool flag6 = ultraGridColumn13.Hidden = flag4;
				bool flag8 = ultraGridColumn12.Hidden = flag6;
				bool flag10 = ultraGridColumn11.Hidden = flag8;
				bool flag12 = ultraGridColumn10.Hidden = flag10;
				bool flag14 = ultraGridColumn9.Hidden = flag12;
				bool flag16 = ultraGridColumn8.Hidden = flag14;
				bool flag18 = ultraGridColumn7.Hidden = flag16;
				bool flag20 = ultraGridColumn6.Hidden = flag18;
				bool flag22 = ultraGridColumn5.Hidden = flag20;
				bool flag24 = ultraGridColumn4.Hidden = flag22;
				bool flag26 = ultraGridColumn3.Hidden = flag24;
				bool hidden = ultraGridColumn2.Hidden = flag26;
				ultraGridColumn.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				UltraGridColumn ultraGridColumn15 = dataGridItems.DisplayLayout.Bands[0].Columns["IsDNRow"];
				UltraGridColumn ultraGridColumn16 = dataGridItems.DisplayLayout.Bands[0].Columns["RowSourceType"];
				UltraGridColumn ultraGridColumn17 = dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"];
				UltraGridColumn ultraGridColumn18 = dataGridItems.DisplayLayout.Bands[0].Columns["RowSourceType"];
				UltraGridColumn ultraGridColumn19 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
				flag20 = (dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true);
				flag22 = (ultraGridColumn19.Hidden = flag20);
				flag24 = (ultraGridColumn18.Hidden = flag22);
				flag26 = (ultraGridColumn17.Hidden = flag24);
				hidden = (ultraGridColumn16.Hidden = flag26);
				ultraGridColumn15.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Hidden = !canAccessCost;
				UltraGridColumn ultraGridColumn20 = dataGridItems.DisplayLayout.Bands[0].Columns["HiddenAverageCost"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].Hidden = true);
				ultraGridColumn20.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].Hidden = true;
				UltraGridColumn ultraGridColumn21 = dataGridItems.DisplayLayout.Bands[0].Columns["Brand"];
				UltraGridColumn ultraGridColumn22 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn23 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn24 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				Activation activation2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].CellActivation = Activation.NoEdit;
				Activation activation4 = ultraGridColumn24.CellActivation = activation2;
				Activation activation6 = ultraGridColumn23.CellActivation = activation4;
				Activation activation9 = ultraGridColumn21.CellActivation = (ultraGridColumn22.CellActivation = activation6);
				dataGridItems.DisplayLayout.Bands[0].Columns["Brand"].CellAppearance.BackColor = Color.WhiteSmoke;
				UltraGridColumn ultraGridColumn25 = dataGridItems.DisplayLayout.Bands[0].Columns["Brand"];
				UltraGridColumn ultraGridColumn26 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn27 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn28 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				flag22 = (dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].TabStop = false);
				flag24 = (ultraGridColumn28.TabStop = flag22);
				flag26 = (ultraGridColumn27.TabStop = flag24);
				hidden = (ultraGridColumn26.TabStop = flag26);
				ultraGridColumn25.TabStop = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOSysDocID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOVoucherID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SORowIndex"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsDNRow"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowSourceType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["HiddenAverageCost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Brand"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].DefaultCellValue = "1";
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance;
				AppearanceBase cellAppearance4 = dataGridItems.DisplayLayout.Bands[0].Columns["Brand"].CellAppearance;
				AppearanceBase cellAppearance5 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].CellAppearance;
				AppearanceBase cellAppearance6 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].CellAppearance;
				Color color = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				Color color3 = cellAppearance6.BackColorDisabled = color;
				Color color5 = cellAppearance5.BackColorDisabled = color3;
				Color color7 = cellAppearance4.BackColorDisabled = color5;
				Color color9 = cellAppearance3.BackColorDisabled = color7;
				Color color12 = cellAppearance.BackColorDisabled = (cellAppearance2.BackColorDisabled = color9);
				AppearanceBase cellAppearance7 = dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].CellAppearance;
				AppearanceBase cellAppearance8 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].CellAppearance;
				AppearanceBase cellAppearance9 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].CellAppearance;
				AppearanceBase cellAppearance10 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].CellAppearance;
				color5 = (dataGridItems.DisplayLayout.Bands[0].Columns["Brand"].CellAppearance.ForeColor = Color.Black);
				color7 = (cellAppearance10.ForeColor = color5);
				color9 = (cellAppearance9.ForeColor = color7);
				color12 = (cellAppearance7.ForeColor = (cellAppearance8.ForeColor = color9));
				dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
				dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].AutoSuggestFilterMode = AutoSuggestFilterMode.Contains;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].Hidden = !useInlineDiscount;
				if (useInlineDiscount)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				}
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
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].Header.Caption = "Default Description";
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].Header.Caption = CompanyPreferences.Attribute1Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].Header.Caption = CompanyPreferences.Attribute2Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].Header.Caption = CompanyPreferences.Attribute3Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].Header.Caption = "Weight";
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Caption = "Tax";
				dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].Header.Caption = "Price";
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].Header.Caption = CompanyPreferences.SpecificationID;
				if (false)
				{
					dataGridItems.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
					dataGridItems.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
					dataGridItems.DisplayLayout.Override.RowFilterAction = RowFilterAction.AppearancesOnly;
					dataGridItems.DisplayLayout.Override.FilteredInRowAppearance.BackColor = Color.Blue;
					dataGridItems.DisplayLayout.Override.FilteredInCellAppearance.ForeColor = Color.Yellow;
					dataGridItems.DisplayLayout.Override.FilteredOutRowAppearance.BackColor = SystemColors.ControlLight;
					dataGridItems.DisplayLayout.Override.FilteredOutCellAppearance.ForeColor = SystemColors.ControlDarkDark;
				}
				else
				{
					dataGridItems.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.False;
				}
				if (allowFOC)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Hidden = false;
					dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Hidden = false;
					dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].Hidden = false;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].Hidden = true;
				}
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDescription))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
				}
				UltraGridColumn ultraGridColumn29 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].Hidden = true);
				ultraGridColumn29.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Header.Caption = "FOC";
				dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Header.Caption = "Inv Qty";
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].Header.Caption = "Consign #";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].Header.Caption = "Tax Group";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].Header.Caption = "Net Amount";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.TextHAlign = HAlign.Right;
				UltraGridColumn ultraGridColumn30 = dataGridItems.DisplayLayout.Bands[0].Columns["Price"];
				UltraGridColumn ultraGridColumn31 = dataGridItems.DisplayLayout.Bands[0].Columns["Discount"];
				UltraGridColumn ultraGridColumn32 = dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"];
				UltraGridColumn ultraGridColumn33 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"];
				UltraGridColumn ultraGridColumn34 = dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"];
				UltraGridColumn ultraGridColumn35 = dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"];
				UltraGridColumn ultraGridColumn36 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"];
				int num2 = dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MinWidth = 50;
				int num4 = ultraGridColumn36.MinWidth = num2;
				int num6 = ultraGridColumn35.MinWidth = num4;
				int num8 = ultraGridColumn34.MinWidth = num6;
				int num10 = ultraGridColumn33.MinWidth = num8;
				int num12 = ultraGridColumn32.MinWidth = num10;
				int num15 = ultraGridColumn30.MinWidth = (ultraGridColumn31.MinWidth = num12);
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"].MinWidth = 60;
				num15 = (dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].MinWidth = (dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MinWidth = 40));
				UltraGridColumn ultraGridColumn37 = dataGridItems.DisplayLayout.Bands[0].Columns["Price"];
				UltraGridColumn ultraGridColumn38 = dataGridItems.DisplayLayout.Bands[0].Columns["Discount"];
				UltraGridColumn ultraGridColumn39 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"];
				UltraGridColumn ultraGridColumn40 = dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"];
				UltraGridColumn ultraGridColumn41 = dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"];
				UltraGridColumn ultraGridColumn42 = dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"];
				UltraGridColumn ultraGridColumn43 = dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"];
				UltraGridColumn ultraGridColumn44 = dataGridItems.DisplayLayout.Bands[0].Columns["ConsignmentNo"];
				UltraGridColumn ultraGridColumn45 = dataGridItems.DisplayLayout.Bands[0].Columns["Unit"];
				int num19 = dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MaxWidth = 100;
				int num21 = ultraGridColumn45.MaxWidth = num19;
				num2 = (ultraGridColumn44.MaxWidth = num21);
				num4 = (ultraGridColumn43.MaxWidth = num2);
				num6 = (ultraGridColumn42.MaxWidth = num4);
				num8 = (ultraGridColumn41.MaxWidth = num6);
				num10 = (ultraGridColumn40.MaxWidth = num8);
				num12 = (ultraGridColumn39.MaxWidth = num10);
				num15 = (ultraGridColumn37.MaxWidth = (ultraGridColumn38.MaxWidth = num12));
				dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].MaxWidth = 200;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].Hidden = false;
				AdjustGridColumnSettings();
				dataGridItems.SetupUI();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupExpenseGrid()
		{
			try
			{
				dataGridExpense.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Expense Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Ref");
				dataTable.Columns.Add("TaxGroupID");
				dataTable.Columns.Add("TaxOption", typeof(byte));
				dataTable.Columns.Add("Tax", typeof(decimal));
				dataTable.Columns.Add("Cost", typeof(decimal));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridExpense.DataSource = dataTable;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].Hidden = !CompanyPreferences.IsTax;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].MaxLength = 64;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].ValueList = comboBoxGridExpenseCode;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 64;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].MaxLength = 20;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 1;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.AllowEdit;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.BackColorDisabled = Color.White;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Quantity"].Format = "0.####";
				dataGridExpense.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
				dataGridExpense.DisplayLayout.Bands[0].Columns["TaxGroupID"].Hidden = true;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Cost"].Hidden = true;
				Color color2 = dataGridExpense.DisplayLayout.Bands[0].Columns["TaxOption"].CellAppearance.BackColorDisabled = (dataGridExpense.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColorDisabled = Color.WhiteSmoke);
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].TabStop = false;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellActivation = Activation.NoEdit;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].Width = 100;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].Width = 100;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Width = 70;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].Width = 250;
			}
			catch (Exception e)
			{
				dataGridExpense.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void AdjustGridColumnSettings()
		{
			dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].MaxLength = 64;
			dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].ValueList = comboBoxGridIncomeCode;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].ValueList = comboBoxGridLocation;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MaxLength = 15;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].DefaultCellValue = Security.DefaultInventoryLocationID;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
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
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					row.Cells["Description"].Activation = Activation.Disabled;
					row.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Unit"].Activation = Activation.Disabled;
					row.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
				}
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].CellAppearance.BackColorDisabled = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellAppearance.BackColorDisabled = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.BackColorDisabled = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = true;
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SOSysDocID"))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["SOSysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOVoucherID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SORowIndex"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsDNRow"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = true;
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].CellActivation = Activation.NoEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].CellAppearance.BackColor = Color.WhiteSmoke;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"].TabStop = false;
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
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].MinValue = -99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightQuantity"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].Format = "#,0.00##";
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["WeightPrice"].MaxValue = new decimal(999999999999L);
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
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDescription))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Income Code"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.Cursor = Cursors.Hand;
			if (!dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("TotalQty"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("FOC", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["FOCQuantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["FOC"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["FOC"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["FOC"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["FOC"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("InvQuantity", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["InvQuantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["InvQuantity"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["InvQuantity"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["InvQuantity"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["InvQuantity"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("RowCount", SummaryType.Count, dataGridItems.DisplayLayout.Bands[0].Columns["Description"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["RowCount"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["RowCount"].Appearance.TextHAlign = HAlign.Left;
				dataGridItems.DisplayLayout.Bands[0].Summaries["RowCount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["RowCount"].DisplayFormat = "{0:#,##0} Rows";
			}
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Ordered") && !dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Ordered"))
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
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].DisplayFormat = "Count:{0:n}";
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeInventoryLocation))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.Disabled;
				comboBoxGridLocation.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangePrice))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.NoEdit;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.AllowEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.AllowEdit;
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Job"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Job"].MaxLength = 64;
			dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ValueList = ComboBoxitemJob;
			dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].MaxLength = 64;
			dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ValueList = ComboBoxitemcostCategory;
			UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"];
			bool hidden = dataGridItems.DisplayLayout.Bands[0].Columns["Style"].Hidden = true;
			ultraGridColumn.Hidden = hidden;
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
					currentData = Factory.SalesInvoiceNISystem.GetServiceInvoiceByID(SystemDocID, voucherID);
					if (currentData != null)
					{
						IsNewRecord = false;
						FillData();
						if (!isDataLoading)
						{
							comboBoxShippingAddressID.Filter(comboBoxCustomer.SelectedID);
							comboBoxBillingAddress.Filter(comboBoxCustomer.SelectedID);
						}
						if (useJobCosting)
						{
							comboBoxJob.Filter(comboBoxCustomer.SelectedID);
						}
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
					bool flag = false;
					DataRow dataRow = currentData.Tables["Sales_Invoice_NonInv"].Rows[0];
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					dateTimePickerDueDate.Value = DateTime.Parse(dataRow["DueDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxRef2.Text = dataRow["Reference2"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
					comboBoxJob.SelectedID = dataRow["JobID"].ToString();
					comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
					if (dataRow["CurrencyRate"] != DBNull.Value)
					{
						comboBoxCurrency.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
					}
					else
					{
						comboBoxCurrency.Rate = 1m;
					}
					if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != Global.BaseCurrencyID)
					{
						flag = true;
					}
					comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
					comboBoxBillingAddress.SelectedID = dataRow["BillingAddressID"].ToString();
					textBoxShipto.Text = dataRow["ShipToAddress"].ToString();
					textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
					comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
					comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
					comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
					labelReportTo.Text = dataRow["ReportTo"].ToString();
					if (dataRow["SourceDocType"] != DBNull.Value)
					{
						sourceDocType = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
					}
					else
					{
						sourceDocType = ItemSourceTypes.None;
					}
					if (sourceDocType != 0)
					{
						comboBoxCustomer.Enabled = false;
					}
					else
					{
						comboBoxCustomer.Enabled = true;
					}
					textBoxPONumber.Text = dataRow["PONumber"].ToString();
					if (dataRow["PriceIncludeTax"] != DBNull.Value && CompanyPreferences.IsTax)
					{
						checkBoxPriceIncludeTax.Checked = bool.Parse(dataRow["PriceIncludeTax"].ToString());
					}
					else
					{
						checkBoxPriceIncludeTax.Checked = false;
					}
					if (dataRow["DiscountFC"] != DBNull.Value)
					{
						textBoxDiscountAmount.Text = decimal.Parse(dataRow["DiscountFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxDiscountAmount.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (textBoxDiscountAmount.Text == 0.ToString(Format.TotalAmountFormat))
					{
						textBoxDiscountPercent.Text = "0";
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
					if (dataRow["PropertyID"] != DBNull.Value)
					{
						comboBoxProperty.SelectedID = dataRow["PropertyID"].ToString();
					}
					else
					{
						comboBoxProperty.SelectedID = "";
					}
					if (dataRow["PropertyUnitID"] != DBNull.Value)
					{
						ComboBoxpropertyUnit.SelectedID = dataRow["PropertyUnitID"].ToString();
					}
					else
					{
						ComboBoxpropertyUnit.SelectedID = "";
					}
					if (dataRow["AgentID"] != DBNull.Value)
					{
						comboBoxPropertyAgent.SelectedID = dataRow["AgentID"].ToString();
					}
					else
					{
						comboBoxPropertyAgent.SelectedID = "";
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Sales_Invoice_NonInv_Detail") && currentData.SalesInvoiceDetailTable.Rows.Count != 0)
					{
						CustomerData customerByID = Factory.CustomerSystem.GetCustomerByID(comboBoxCustomer.SelectedID);
						if (customerByID != null && customerByID.Tables.Count != 0 && customerByID.Tables[0].Rows.Count != 0)
						{
							ultraToolTipManager1.Enabled = true;
							UltraToolTipInfo ultraToolTipInfo = new UltraToolTipInfo();
							ultraToolTipInfo.ToolTipText = "";
							string text = customerByID.CustomerTable.Rows[0]["DeliveryInstructions"].ToString();
							if (text != null && text != "")
							{
								ultraToolTipInfo.ToolTipText = text;
								ultraToolTipManager1.SetUltraToolTip(textBoxCustomerName, ultraToolTipInfo);
							}
							else
							{
								ultraToolTipManager1.Enabled = false;
							}
							foreach (DataRow row in currentData.Tables["Sales_Invoice_NonInv_Detail"].Rows)
							{
								DataRow dataRow3 = dataTable.NewRow();
								bool result = false;
								if (row["IsDNRow"] != DBNull.Value && bool.TryParse(row["IsDNRow"].ToString(), out result))
								{
									dataRow3["IsDNRow"] = row["IsDNRow"];
								}
								dataRow3["RowSourceType"] = row["RowSource"];
								dataRow3["Income Code"] = row["ProductID"];
								if (row["UnitQuantity"] != DBNull.Value)
								{
									dataRow3["Quantity"] = row["UnitQuantity"];
								}
								else
								{
									dataRow3["Quantity"] = row["Quantity"];
								}
								if (row["Quantity"] != DBNull.Value)
								{
									dataRow3["FOCQuantity"] = row["FOCQuantity"];
								}
								dataRow3["Description"] = row["Description"];
								dataRow3["Remarks"] = row["Remarks"];
								dataRow3["Location"] = row["LocationID"];
								dataRow3["Unit"] = row["UnitID"];
								dataRow3["TaxOption"] = row["TaxOption"];
								dataRow3["SOSysDocID"] = row["OrderSysDocID"];
								dataRow3["SOVoucherID"] = row["OrderVoucherID"];
								dataRow3["SORowIndex"] = row["OrderRowIndex"];
								dataRow3["Job"] = row["JobID"];
								dataRow3["CostCategory"] = row["CostCategoryID"];
								dataRow3["WeightQuantity"] = row["WeightQuantity"];
								dataRow3["WeightPrice"] = row["WeightPrice"];
								if (flag)
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
								if (row["Cost"] != DBNull.Value)
								{
									dataRow3["Cost"] = decimal.Parse(row["Cost"].ToString()).ToString(Format.UnitPriceFormat);
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
								decimal result2 = default(decimal);
								decimal result3 = default(decimal);
								decimal result4 = default(decimal);
								decimal result5 = default(decimal);
								decimal result6 = default(decimal);
								decimal result7 = default(decimal);
								decimal.TryParse(dataRow3["Quantity"].ToString(), out result2);
								decimal.TryParse(dataRow3["FOCQuantity"].ToString(), out result5);
								decimal.TryParse(dataRow3["Price"].ToString(), out result3);
								decimal.TryParse(dataRow3["Discount"].ToString(), out result4);
								decimal.TryParse(dataRow3["Tax"].ToString(), out result6);
								dataRow3["InvQuantity"] = result2 - result5;
								if (flag)
								{
									dataRow3["Amount"] = Math.Round(decimal.Parse(row["AmountFC"].ToString()), Global.CurDecimalPoints);
								}
								else
								{
									dataRow3["Amount"] = Math.Round(decimal.Parse(row["Amount"].ToString()), Global.CurDecimalPoints);
								}
								decimal.TryParse(dataRow3["InvQuantity"].ToString(), out result7);
								dataRow3["SpecificationID"] = row["SpecificationID"];
								dataRow3["Style"] = row["StyleID"];
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
								DataRow[] array = currentData.TaxDetailsTable.Select("RowIndex = " + row2.Index + " AND TaxLevel = " + (byte)2);
								if (array.Length != 0)
								{
									TaxTransactionData taxTransactionData = new TaxTransactionData();
									taxTransactionData.Merge(array);
									row2.Cells["Tax"].Tag = taxTransactionData;
								}
							}
							DataRow[] array2 = currentData.TaxDetailsTable.Select("RowIndex = -1 AND TaxLevel = " + (byte)1);
							if (array2.Length != 0)
							{
								TaxTransactionData taxTransactionData2 = new TaxTransactionData();
								taxTransactionData2.Merge(array2);
								textBoxTaxAmount.Tag = taxTransactionData2;
							}
							isDiscountPercent = false;
							CalculateTotal();
							EnableButton();
						}
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

		private void createFromRentalRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet list = Factory.PropertyRentSystem.GetList();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = list;
			selectDocumentDialog.Text = "Select Rental Registration";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				ClearForm();
				string sysDocID = selectDocumentDialog.SelectedRow.Cells["SysDocID"].Value.ToString();
				string voucherID = selectDocumentDialog.SelectedRow.Cells["VoucherID"].Value.ToString();
				PropertyRentData propertyRentByID = Factory.PropertyRentSystem.GetPropertyRentByID(sysDocID, voucherID);
				DataRow dataRow = propertyRentByID.PropertyRentTable.Rows[0];
				textBoxRef2.Text = dataRow["VoucherID"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				if (comboBoxCustomer.SelectedID == "")
				{
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
				}
				comboBoxCustomer.Enabled = false;
				if (dataRow["PropertyID"] != DBNull.Value)
				{
					comboBoxProperty.SelectedID = dataRow["PropertyID"].ToString();
				}
				else
				{
					comboBoxProperty.SelectedID = "";
				}
				if (dataRow["UnitID"] != DBNull.Value)
				{
					ComboBoxpropertyUnit.SelectedID = dataRow["UnitID"].ToString();
				}
				else
				{
					ComboBoxpropertyUnit.SelectedID = "";
				}
				sourceDocType = ItemSourceTypes.None;
				if (!string.IsNullOrEmpty(dataRow["CurrencyID"].ToString()))
				{
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
				}
				if (!string.IsNullOrEmpty(dataRow["PropertyAgentID"].ToString()))
				{
					comboBoxPropertyAgent.SelectedID = dataRow["PropertyAgentID"].ToString();
				}
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (!propertyRentByID.Tables.Contains("Property_Rent_Detail") || propertyRentByID.PropertyRentDetailTable.Rows.Count == 0)
				{
					return;
				}
				foreach (DataRow row in propertyRentByID.PropertyRentDetailTable.Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["Income Code"] = row["IncomeID"];
					dataRow3["Quantity"] = 1;
					dataRow3["Description"] = row["Description"];
					if (row["Discount"] != DBNull.Value)
					{
						dataRow3["Discount"] = decimal.Parse(row["Discount"].ToString()).ToString(Format.UnitPriceFormat);
					}
					dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
					dataRow3["Amount"] = decimal.Parse(row["Amount"].ToString()).ToString(Format.UnitPriceFormat);
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
					dataRow3.EndEdit();
					dataRow3.EndEdit();
					dataTable.Rows.Add(dataRow3);
				}
			}
			textBoxDiscountAmount_Validated(sender, e);
		}

		private void EnableButton()
		{
			bool flag = Factory.SalesInvoiceNISystem.ModifyTransactions(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, Global.CurrentUser.ToUpper(), isModify: false, "");
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
			dataGridItems.PerformAction(UltraGridAction.ExitEditMode);
			if (!allowEdit)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "You cannot edit this transfer transaction because it is already accepted or rejected.", "Document is in use.");
				return false;
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
				bool flag2 = Factory.SalesInvoiceNISystem.CreateSalesInvoice(currentData, !isNewRecord);
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
				if (ex.Number == 1051)
				{
					ErrorHelper.WarningMessage(ex.Message);
					return false;
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

		public void SetApprovalStatus()
		{
			if (IsNewRecord)
			{
				ToolStripButton toolStripButton = toolStripButtonApproval;
				ToolStripLabel toolStripLabel = toolStripLabelApproval;
				bool flag2 = toolStripSeparatorApproval.Visible = false;
				bool visible = toolStripLabel.Visible = flag2;
				toolStripButton.Visible = visible;
			}
			else
			{
				if (currentData == null || currentData.Tables[0].Rows.Count <= 0)
				{
					return;
				}
				DataRow dataRow = currentData.Tables[0].Rows[0];
				bool flag2;
				bool visible;
				if (!dataRow.Table.Columns.Contains("ApprovalStatus") || dataRow["ApprovalStatus"].IsDBNullOrEmpty())
				{
					ToolStripButton toolStripButton2 = toolStripButtonApproval;
					ToolStripLabel toolStripLabel2 = toolStripLabelApproval;
					flag2 = (toolStripSeparatorApproval.Visible = false);
					visible = (toolStripLabel2.Visible = flag2);
					toolStripButton2.Visible = visible;
					return;
				}
				switch (int.Parse(dataRow["ApprovalStatus"].ToString()))
				{
				case 3:
					toolStripButtonApproval.Text = "Rejected";
					toolStripButtonApproval.ForeColor = Color.Red;
					break;
				case 10:
					toolStripButtonApproval.Text = "Approved";
					toolStripButtonApproval.ForeColor = Color.ForestGreen;
					break;
				default:
					toolStripButtonApproval.Text = "Pending";
					toolStripButtonApproval.ForeColor = Color.Orange;
					break;
				}
				ToolStripButton toolStripButton3 = toolStripButtonApproval;
				ToolStripLabel toolStripLabel3 = toolStripLabelApproval;
				flag2 = (toolStripSeparatorApproval.Visible = true);
				visible = (toolStripLabel3.Visible = flag2);
				toolStripButton3.Visible = visible;
			}
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "Sales_Invoice_NonInv", "VoucherID");
		}

		private void ValidateAllowedDiscount()
		{
			decimal d = default(decimal);
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			num = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
			decimal d2 = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num2 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			num3 = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
			_ = d2 + num2 - num + num3;
			if (decimal.Parse(textBoxDiscountAmount.Text, NumberStyles.Any) < 0m)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "Total amount cannot be greater than the subtotal.", "Please enter a numeric value less or equal to the subtotal.");
				return;
			}
			_ = (num < 0m);
			if (TotalDiscountPercAllowed < d && !showedNotDiscount && TotalDiscountPercAllowed > 0m && !Global.IsUserAdmin)
			{
				showedNotDiscount = true;
				ErrorHelper.WarningMessage("Higher Discount is not applicable.", "Please enter a proper discount.");
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
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Sales_Invoice_NonInv", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (sourceDocType == ItemSourceTypes.SalesOrder)
			{
				timeStampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("Sales_Order", refDocID, refVoucherID);
			}
			if (timeStampStatus != null && timeStampStatus.Tables.Count > 0 && timeStampStatus.Tables[0].Rows.Count > 0 && IsNewRecord)
			{
				lastUpdateDateTime = DateTime.Parse(timeStampStatus.Tables[0].Rows[0]["updatetime"].ToString());
			}
			_ = (lastUpdateDateTime > refDateTime);
			if (RestrictTransaction)
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
				return false;
			}
			if (!IsNewRecord)
			{
				DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(currentData, SysDocTypes.PropertyServiceInvoice, Global.CurrentUser, includeApproveUser: false);
				if (entityApprovalStatus.Tables[0].Rows.Count > 0 && !bool.Parse(entityApprovalStatus.Tables[0].Rows[0]["ModifyTransaction"].ToString()))
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
					return false;
				}
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
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxCustomer.SelectedID == "" || comboBoxProperty.SelectedID == "" || ComboBoxpropertyUnit.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				if (!IsNewRecord && Factory.SalesInvoiceNISystem.InvoiceHasShippedQuantity(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this invoice has been already delivered. You are not able to modify.");
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
				if (result2)
				{
					ErrorHelper.WarningMessage("This customer is on hold status and does not allow transaction.");
					return false;
				}
				showedNotDiscount = false;
				ValidateAllowedDiscount();
				if (showedNotDiscount)
				{
					ErrorHelper.WarningMessage("Higher Discount is not applicable.", "Please enter a proper discount.");
					return false;
				}
				int num2 = 0;
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					UltraGridRow ultraGridRow = dataGridItems.Rows[i];
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
					}
					else
					{
						if (dataGridItems.Rows[i].Cells["Income Code"].Value.ToString() == "")
						{
							ErrorHelper.InformationMessage("Please select an item.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
						if (dataGridItems.Rows[i].Cells["Price"].Value == null || dataGridItems.Rows[i].Cells["Price"].Value.ToString() == "")
						{
							ErrorHelper.InformationMessage("Please enter price for all the items.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
						if (allowZeroprice && (dataGridItems.Rows[i].Cells["Amount"].Value.ToString() == "0" || dataGridItems.Rows[i].Cells["Price"].Value.ToString() == "0" || dataGridItems.Rows[i].Cells["Amount"].Value.ToString() == "0.00" || dataGridItems.Rows[i].Cells["Price"].Value.ToString() == "0.00"))
						{
							ErrorHelper.InformationMessage("Do not allowed to save with zero price.");
							dataGridItems.Rows[i].Activate();
							return false;
						}
					}
					decimal d = decimal.Parse(ultraGridRow.Cells["Quantity"].Value.ToString());
					decimal d2 = decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString());
					decimal num3 = decimal.Parse(ultraGridRow.Cells["Amount"].Value.ToString());
					decimal d3 = default(decimal);
					decimal result3 = default(decimal);
					if (ultraGridRow.Cells["Discount"].Value != DBNull.Value)
					{
						decimal.TryParse(ultraGridRow.Cells["Discount"].Value.ToString(), out result3);
					}
					decimal d4 = default(decimal);
					if (ultraGridRow.Cells["FOCQuantity"].Value != null && ultraGridRow.Cells["FOCQuantity"].Value.ToString() != "")
					{
						d4 = decimal.Parse(ultraGridRow.Cells["FOCQuantity"].Value.ToString());
					}
					if (Math.Abs(Math.Round((d - d4) * (d2 - result3 + d3), Global.CurDecimalPoints) - num3) > 0.1m)
					{
						ErrorHelper.WarningMessage("Amount does not match the quantity and price for row no:" + ultraGridRow.Index + 1 + "\nItem Code:" + ultraGridRow.Cells["Item Code"].Value.ToString());
						return false;
					}
					if (num3 == 0m)
					{
						num2++;
					}
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one row of item.");
					return false;
				}
				if (num2 != 0)
				{
					switch (ErrorHelper.QuestionMessageYesNo(num2 + " No. of items with Zero price, Do you wants to continue?"))
					{
					case DialogResult.Yes:
						return true;
					case DialogResult.No:
						return false;
					}
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Sales_Invoice_NonInv", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
					textBoxVoucherNumber.Focus();
					return false;
				}
				if (CompanyPreferences.OverCLAction != 1)
				{
					string sysDocID = "";
					string voucherID = "";
					if (!IsNewRecord)
					{
						sysDocID = comboBoxSysDoc.SelectedID;
						voucherID = textBoxVoucherNumber.Text;
					}
					if (Factory.CustomerSystem.IsOverCreditLimit(comboBoxCustomer.SelectedID, sysDocID, voucherID, decimal.Parse(textBoxTotal.Text), checkOpenDN: false, dateTimePickerDate.Value))
					{
						if (CompanyPreferences.OverCLAction == 2)
						{
							if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "This transaction exceeds the customer's credit limit.", "Do you want to continue?") == DialogResult.No)
							{
								return false;
							}
						}
						else
						{
							if (CompanyPreferences.OverCLAction == 3)
							{
								ErrorHelper.WarningMessage("This transaction exceeds the customer's credit limit.", "You are not allowed to sell over the customer's credit limit.");
								return false;
							}
							if (CompanyPreferences.OverCLAction == 4)
							{
								ErrorHelper.WarningMessage("This transaction exceeds the customer's credit limit.", "The transaction should be approved to proceed.");
								CLPasswordForm cLPasswordForm = new CLPasswordForm();
								cLPasswordForm.CustomerID = comboBoxCustomer.SelectedID;
								cLPasswordForm.CustomerName = comboBoxCustomer.SelectedName;
								cLPasswordForm.InvoiceAmount = decimal.Parse(textBoxTotal.Text);
								if (cLPasswordForm.ShowDialog() == DialogResult.Cancel)
								{
									return false;
								}
								clUserID = cLPasswordForm.ApproverID;
							}
						}
					}
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

		private void ClearForm()
		{
			try
			{
				allowEdit = true;
				textBoxNote.Clear();
				textBoxRef1.Clear();
				textBoxRef2.Clear();
				clUserID = "";
				dateTimePickerDate.Value = DateTime.Now;
				textBoxShipto.Clear();
				comboBoxBillingAddress.Clear();
				comboBoxPropertyAgent.Clear();
				ComboBoxpropertyUnit.Clear();
				comboBoxProperty.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				comboBoxGridIncomeCode.Clear();
				textBoxCustomerName.Clear();
				comboBoxCustomer.Clear();
				textBoxTotalExpense.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxPONumber.Clear();
				comboBoxSalesperson.Clear();
				comboBoxShippingAddressID.Clear();
				comboBoxShippingMethod.Clear();
				comboBoxJob.Clear();
				comboBoxJob.Filter("");
				comboBoxCostCategory.Clear();
				comboBoxTerm.Clear();
				textBoxSubtotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxBilltoAddress.Clear();
				comboBoxPayeeTaxGroup.Clear();
				comboBoxCustomer.Enabled = true;
				priceListData = null;
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscountAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscountPercent.Text = "0";
				isDiscountPercent = false;
				textBoxRoundOff.Text = 0.ToString(Format.TotalAmountFormat);
				labelReportTo.Text = "";
				textBoxPropertyName.Clear();
				textBoxUnitName.Clear();
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				if (dataTable.Columns.Contains("Ordered"))
				{
					dataTable.Columns.Remove("Ordered");
					dataTable.Columns.Remove("Shipped");
				}
				dataTable.Rows.Clear();
				IsNewRecord = true;
				dataTable = (dataGridExpense.DataSource as DataTable);
				dataTable.Rows.Clear();
				ApplyCustomerSettings();
				checkedListBoxDN.Items.Clear();
				sourceDocType = ItemSourceTypes.None;
				if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenInvoiceThenDN)
				{
					if (!CompanyPreferences.AllowLSellWithoutSO)
					{
						dataGridItems.AllowAddNew = false;
					}
					else
					{
						dataGridItems.AllowAddNew = true;
					}
				}
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				deliveryNoteTable = null;
				IsVoid = false;
				formManager.ResetDirty();
				comboBoxCustomer.Enabled = true;
				comboBoxProperty.Focus();
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
				if (!IsNewRecord && Factory.SalesInvoiceNISystem.InvoiceHasShippedQuantity(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this invoice has been already delivered. You are not able to delete.");
					return false;
				}
				if (!IsNewRecord && !Factory.SalesInvoiceNISystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Some items in this transaction has been already returned. You are not able to modify.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.SalesInvoiceNISystem.DeleteSalesInvoice(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Sales_Invoice_NonInv", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Sales_Invoice_NonInv", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Sales_Invoice_NonInv", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Sales_Invoice_NonInv", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Sales_Invoice_NonInv", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
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
			try
			{
				dataGridExpense.SetupUI();
				SetupExpenseGrid();
				SetupGrid();
				if (!CompanyPreferences.IsTax)
				{
					panelNonTax.Visible = false;
					panelTotal.Location = new Point(761, 410);
				}
				else
				{
					panelNonTax.Visible = true;
					panelTotal.Location = new Point(761, 434);
				}
				groupBoxTax.Visible = false;
				labelTaxGroup.Visible = CompanyPreferences.IsTax;
				comboBoxPayeeTaxGroup.Visible = CompanyPreferences.IsTax;
				checkBoxPriceIncludeTax.Visible = false;
				UltraFormattedLinkLabel ultraFormattedLinkLabel = linkLabelTax;
				bool visible = textBoxTaxAmount.Visible = CompanyPreferences.IsTax;
				ultraFormattedLinkLabel.Visible = visible;
				UltraFormattedLinkLabel ultraFormattedLinkLabel2 = ultraFormattedLinkCurrency;
				visible = (comboBoxCurrency.Visible = CompanyPreferences.UseMultiCurrency);
				ultraFormattedLinkLabel2.Visible = visible;
				mergeMatrixPrint = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.MergeMatrixItems.ToString(), false).ToString());
				comboBoxSysDoc.FilterByType(SysDocTypes.PropertyServiceInvoice);
				object obj = null;
				obj = Factory.DatabaseSystem.GetFieldValue("Tax", "CalculationMethod", "CalculationMethod", 3);
				if (obj != null)
				{
					if (int.Parse(obj.ToString()) == 3)
					{
						IsTaxbasedonProfit = true;
					}
					else
					{
						IsTaxbasedonProfit = false;
					}
				}
				SetSecurity();
				IsNewRecord = true;
				if (!base.IsDisposed)
				{
					ClearForm();
					SetupForm();
				}
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
				dataGridExpense.LoadLayoutFailed = true;
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
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDocNumber))
			{
				textBoxVoucherNumber.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDueDate))
			{
				comboBoxTerm.Enabled = false;
				dateTimePickerDueDate.Enabled = false;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.GiveDiscount))
			{
				textBoxDiscountAmount.ReadOnly = true;
				textBoxDiscountPercent.ReadOnly = true;
				textBoxTotal.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
			}
			userViewStaus = Security.IsAllowedSecurityRole(GeneralSecurityRoles.ViewItemdetails);
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowChangeBillAddress))
			{
				comboBoxBillingAddress.ReadOnly = true;
				textBoxBilltoAddress.ReadOnly = true;
			}
			else
			{
				comboBoxBillingAddress.ReadOnly = false;
				textBoxBilltoAddress.ReadOnly = false;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransDiffLocation))
			{
				AllowEditTransDiffLocation = false;
			}
			else
			{
				AllowEditTransDiffLocation = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost) && IsTaxbasedonProfit)
			{
				canAccessCost = true;
				dataGridItems.DataSource = null;
				SetupGrid();
			}
			TotalDiscountPercAllowed = Security.AllowedDiscount(GeneralSecurityRoles.GiveDiscount);
			priceDiscountPercAllowed = Security.AllowedDiscount(GeneralSecurityRoles.AllowPriceDiscount);
			comboBoxPayeeTaxGroup.ReadOnly = !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
			checkBoxPriceIncludeTax.Enabled = Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
		}

		private void SetupForm()
		{
			if (CompanyPreferences.LocalSalesFlow == SalesFlows.DirectInvoice)
			{
				buttonSelectDocument.Visible = false;
				checkedListBoxDN.Visible = false;
				labelSelectedDocs.Visible = false;
			}
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null)
			{
				comboBoxGridIncomeCode.IsLoadingData = false;
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
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

		private void CalculateTotalExpense()
		{
			decimal num = default(decimal);
			foreach (UltraGridRow row in dataGridExpense.Rows)
			{
				decimal result = default(decimal);
				if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result);
					num += result;
				}
			}
			textBoxTotalExpense.Text = num.ToString(Format.TotalAmountFormat);
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid) != DialogResult.No)
			{
				if (!IsNewRecord && Factory.SalesInvoiceNISystem.InvoiceHasShippedQuantity(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Some items in this invoice has been already delivered. You are not able to modify.");
				}
				else if (Void(!isVoid))
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
				return Factory.SalesInvoiceNISystem.VoidSalesInvoice(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.PropertyServiceInvoice);
		}

		private void LoadCustomerDocAddress()
		{
		}

		private void buttonNextBillTo_Click(object sender, EventArgs e)
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				return;
			}
			string nextID = DatabaseHelper.GetNextID("Customer_Address", "AddressID", currentBillingAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (nextID != "")
			{
				currentBillingAddressID = nextID;
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
			string previousID = DatabaseHelper.GetPreviousID("Customer_Address", "AddressID", currentBillingAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (previousID != "")
			{
				currentBillingAddressID = previousID;
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
			decimal result3 = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				decimal result4 = default(decimal);
				decimal result5 = default(decimal);
				if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result4);
					num += result4;
					if (!row.Cells["Tax"].Value.IsNullOrEmpty())
					{
						result3 += decimal.Parse(row.Cells["Tax"].Value.ToString());
					}
					decimal.TryParse(row.Cells["Tax"].Value.ToString(), out result5);
					row.Cells["TaxTotal"].Value = result4 + result5;
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
				isDiscountPercent = false;
			}
			else
			{
				textBoxDiscountPercent.Text = "0";
				textBoxDiscountAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			num2 = num - result;
			CalculateTotalExpense();
			foreach (UltraGridRow row2 in dataGridExpense.Rows)
			{
				if (!row2.Cells["Tax"].Value.IsNullOrEmpty())
				{
					result3 += decimal.Parse(row2.Cells["Tax"].Value.ToString());
				}
			}
			textBoxTaxAmount.Text = result3.ToString(Format.TotalAmountFormat);
			decimal result6 = default(decimal);
			decimal.TryParse(textBoxTotalExpense.Text, out result6);
			decimal.TryParse(textBoxTaxAmount.Text, out result3);
			num3 = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
			num2 = num2 + result6 + num3;
			if (!checkBoxPriceIncludeTax.Checked)
			{
				num2 += result3;
			}
			textBoxTotal.Text = num2.ToString(Format.TotalAmountFormat);
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
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out result2);
					decimal.TryParse(row.Cells["Cost"].Value.ToString(), out result);
					result = result2 * result;
					TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOptions, row.Cells["TaxGroupID"].Value.ToString());
					row.Cells["Tax"].Tag = tag;
					UIGlobal.CalculateRowTax(row, "Tax", result, amount, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
				}
				foreach (UltraGridRow row2 in dataGridExpense.Rows)
				{
					ItemTaxOptions itemTaxOptions2 = ItemTaxOptions.NonTaxable;
					if (!row2.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						itemTaxOptions2 = (ItemTaxOptions)byte.Parse(row2.Cells["TaxOption"].Value.ToString());
					}
					if (itemTaxOptions2 == ItemTaxOptions.BasedOnCustomer)
					{
						row2.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
					}
					decimal amount2 = decimal.Parse(row2.Cells["Amount"].Value.ToString());
					decimal subtotal2 = decimal.Parse(textBoxTotalExpense.Text);
					decimal tradeDiscount2 = default(decimal);
					TaxTransactionData tag2 = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOptions2, row2.Cells["TaxGroupID"].Value.ToString());
					row2.Cells["Tax"].Tag = tag2;
					UIGlobal.CalculateRowTax(row2, "Tax", amount2, subtotal2, tradeDiscount2, checkBoxPriceIncludeTax.Checked);
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
			foreach (UltraGridRow row3 in dataGridExpense.Rows)
			{
				if (row3.Cells["Tax"].Tag != null)
				{
					foreach (DataRow row4 in (row3.Cells["Tax"].Tag as TaxTransactionData).TaxDetailTable.Rows)
					{
						string text2 = row4["TaxItemID"].ToString();
						decimal result3 = default(decimal);
						decimal.TryParse(row4["TaxAmount"].ToString(), out result3);
						DataRow[] array2 = taxDetailTable.Select("TaxItemID  = '" + text2 + "'");
						if (array2.Count() > 0)
						{
							decimal result4 = default(decimal);
							decimal.TryParse(array2[0]["TaxAmount"].ToString(), out result4);
							result4 += result3;
							array2[0]["TaxAmount"] = result4;
						}
						else
						{
							DataRow dataRow2 = taxDetailTable.NewRow();
							dataRow2["TaxItemID"] = text2;
							dataRow2["TaxAmount"] = result3;
							taxDetailTable.Rows.Add(dataRow2);
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
			}
			decimal result = default(decimal);
			decimal.TryParse(textBoxDiscountPercent.Text, out result);
			if (result > 0m && !isDiscountPercent)
			{
				isDiscountPercent = true;
			}
		}

		private void textBoxDiscountAmount_TextChanged(object sender, EventArgs e)
		{
			if (textBoxDiscountAmount.Focused)
			{
				isDiscountPercent = false;
			}
		}

		private void textBoxTotal_TextChanged(object sender, EventArgs e)
		{
			if (textBoxTotal.Focused)
			{
				totalChanged = true;
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
			decimal num7 = default(decimal);
			num4 = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
			num = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num5 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			num6 = decimal.Parse(textBoxTotalExpense.Text, NumberStyles.Any);
			num7 = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
			num2 = num + num5 + num6 - num4 + num7;
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
				num3 = Math.Round(num2 / (num + num5) * 100m, Global.CurDecimalPoints);
				textBoxDiscountPercent.Text = num3.ToString();
			}
			else
			{
				textBoxDiscountPercent.Text = "0";
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
			new FormHelper().EditTenant(comboBoxCustomer.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxSalesperson.SelectedID);
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to copy this document?") == DialogResult.Yes)
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
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					foreach (UltraGridCell cell in row.Cells)
					{
						if (cell.Activation == Activation.Disabled)
						{
							cell.Activation = Activation.AllowEdit;
						}
					}
					row.Cells["IsDNRow"].Value = DBNull.Value;
					string text2 = row.Cells["SOSysDocID"].Value.ToString();
					string text3 = row.Cells["SOVoucherID"].Value.ToString();
					refDocID = text2;
					refVoucherID = text3;
					timeStampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("Sales_Order", refDocID, refVoucherID);
					if (timeStampStatus != null && timeStampStatus.Tables.Count > 0 && timeStampStatus.Tables[0].Rows.Count > 0)
					{
						refDateTime = DateTime.Parse(timeStampStatus.Tables[0].Rows[0]["TimeStamp"].ToString());
					}
				}
				deliveryNoteTable = null;
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			duplicateToolStripMenuItem.Enabled = !IsNewRecord;
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
			if (!string.IsNullOrEmpty(dataRow["TermID"].ToString()))
			{
				comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
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
			if (salesOrderByID.Tables.Contains("Sales_Order_Detail") && salesOrderByID.SalesOrderDetailTable.Rows.Count != 0)
			{
				foreach (DataRow row in salesOrderByID.Tables["Sales_Order_Detail"].Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["Item Code"] = row["ProductID"];
					dataRow3["ItemType"] = row["ItemType"];
					ItemTypes itemTypes = ItemTypes.NonInventory;
					if (row["ItemType"].ToString() != "")
					{
						itemTypes = (ItemTypes)checked((byte)int.Parse(row["ItemType"].ToString()));
					}
					if (itemTypes == ItemTypes.Service)
					{
						dataRow3["SOSysDocID"] = text;
						dataRow3["SOVoucherID"] = text2;
						dataRow3["SORowIndex"] = row["RowIndex"];
						if (row["JobID"] != DBNull.Value)
						{
							dataRow3["Job"] = row["JobID"];
						}
						else
						{
							dataRow3["Job"] = row["JobID"];
						}
						if (row["CostCategoryID"] != DBNull.Value)
						{
							dataRow3["CostCategory"] = row["CostCategoryID"];
						}
						else
						{
							dataRow3["CostCategory"] = row["CostCategoryID"];
						}
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
						if (row["Cost"] != DBNull.Value)
						{
							dataRow3["Cost"] = row["Cost"];
						}
						else
						{
							dataRow3["Cost"] = DBNull.Value;
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
				}
				AdjustGridColumnSettings();
				CalculateAllRowsTaxes();
				CalculateTotal();
				textBoxDiscountPercent.Modified = true;
				textBoxDiscountAmount_Validated(sender, e);
			}
		}

		private void numberTextBox1_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBoxTaxPercent_TextChanged(object sender, EventArgs e)
		{
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
			SelectDocumentDialog selectDocumentDialog = sender as SelectDocumentDialog;
			string text = "";
			foreach (UltraGridRow row in selectDocumentDialog.Grid.Rows)
			{
				if (row.Cells["C"].Value != null && row.Cells["C"].Value.ToString() != "" && bool.Parse(row.Cells["C"].Value.ToString()))
				{
					string text2 = row.Cells["Customer"].Value.ToString();
					if (text != "" && text2 != text)
					{
						ErrorHelper.WarningMessage("Cannot select documents from different customers.");
						selectDocumentDialog.CanClose = false;
						break;
					}
					text = text2;
				}
			}
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
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet salesInvoiceToPrint = Factory.SalesInvoiceNISystem.GetSalesInvoiceToPrint(selectedID, text, mergeMatrixPrint, showLotDetail, NonInventoryInvoiceType.PropertySalesInvoice);
					if (salesInvoiceToPrint == null || salesInvoiceToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						string printTemplateName = comboBoxSysDoc.GetPrintTemplateName();
						if (!string.IsNullOrEmpty(printTemplateName))
						{
							PrintHelper.PrintDocument(salesInvoiceToPrint, selectedID, printTemplateName, SysDocTypes.PropertyServiceInvoice, isPrint, showPrintDialog);
						}
						else
						{
							PrintHelper.PrintDocument(salesInvoiceToPrint, selectedID, "Property Service Invoice", SysDocTypes.PropertyServiceInvoice, isPrint, showPrintDialog);
						}
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

		private void buttonSelectDocument_Click(object sender, EventArgs e)
		{
			_ = CompanyPreferences.LocalSalesFlow;
			createFromSalesOrderToolStripMenuItem_Click(sender, e);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.SelectedSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.BringFormToFront(FormActivator.PropertyServiceInvoiceListFormObj);
		}

		private void saveAsDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveDraft();
		}

		private bool SaveDraft()
		{
			try
			{
				if (GetData())
				{
					EnterNameDialog enterNameDialog = new EnterNameDialog();
					if (enterNameDialog.ShowDialog() == DialogResult.OK)
					{
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.PropertyServiceInvoice);
					}
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 260.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.PropertyServiceInvoice);
					currentData = (dataSet as SalesInvoiceNIData);
					FillData();
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

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void textBoxTotalExpense_TextChanged(object sender, EventArgs e)
		{
		}

		private void toolStripButtonBalance_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void checkedListBoxDN_DoubleClick(object sender, EventArgs e)
		{
			NameValue nameValue = checkedListBoxDN.SelectedItem as NameValue;
			if (nameValue != null)
			{
				new FormHelper().EditTransaction(nameValue.ID, nameValue.Name);
			}
		}

		private void DisplayItemDetails(string SelectedID)
		{
			DataSet dataSet = null;
			dataSet = Factory.CustomerSystem.GetInventorySalesItemDetail(comboBoxCustomer.SelectedID, SelectedID);
			DataTable dataTable = new DataSet().Tables.Add("Table");
			dataTable.Columns.Add("Date", typeof(string));
			dataTable.Columns.Add("Unit");
			dataTable.Columns.Add("Quantity", typeof(double));
			dataTable.Columns.Add("Price", typeof(double));
			dataTable.Columns.Add("Amount", typeof(double));
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataView dataView = new DataView(dataSet.Tables[0]);
				new DataTable();
				foreach (DataRow row in dataView.ToTable().Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["Date"] = row["Date"];
					dataRow2["Quantity"] = row["Quantity"];
					dataRow2["Price"] = row["UnitPrice"];
					dataRow2["Amount"] = (double.Parse(row["UnitPrice"].ToString()) * double.Parse(row["Quantity"].ToString())).ToString();
					dataRow2["Unit"] = row["UnitID"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
			}
			toolTipController1.HideHint();
			string text = "";
			text = ToCSV(dataTable);
			toolTipController1.ShowHint("Last Sale Details", text, ToolTipLocation.BottomRight);
			toolTipController1.InitialDelay = 15000;
		}

		protected string ExportDatatableToHtml(DataTable dt)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			string stringValue = registryHelper.GetStringValue(registryHelper.CurrentWindowsUserKey, "Skin", "");
			stringValue = "LightBlue";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<html >");
			stringBuilder.Append("<head>");
			stringBuilder.Append("</head>");
			stringBuilder.Append("<body>");
			stringBuilder.Append("<table border='1px' cellpadding='1' cellspacing='1' bgcolor='" + stringValue + "' style='font-family:Garamond; font-size:smaller'>");
			stringBuilder.Append("<tr >");
			foreach (DataColumn column in dt.Columns)
			{
				stringBuilder.Append("<td >");
				stringBuilder.Append(column.ColumnName);
				stringBuilder.Append("</td>");
			}
			stringBuilder.Append("</tr>");
			foreach (DataRow row in dt.Rows)
			{
				stringBuilder.Append("<tr >");
				foreach (DataColumn column2 in dt.Columns)
				{
					stringBuilder.Append("<td >");
					stringBuilder.Append(row[column2.ColumnName].ToString());
					stringBuilder.Append("</td>");
				}
				stringBuilder.Append("</tr>");
			}
			stringBuilder.Append("</table>");
			stringBuilder.Append("</body>");
			stringBuilder.Append("</html>");
			return stringBuilder.ToString();
		}

		public string ToCSV(DataTable table)
		{
			StringBuilder stringBuilder = new StringBuilder();
			checked
			{
				for (int i = 0; i < table.Columns.Count; i++)
				{
					stringBuilder.Append("\t" + table.Columns[i].ColumnName);
					stringBuilder.Append((i == table.Columns.Count - 1) ? "\n" : "\t\t\t");
				}
				foreach (DataRow row in table.Rows)
				{
					for (int j = 0; j < table.Columns.Count; j++)
					{
						stringBuilder.Append(row[j].ToString());
						stringBuilder.Append((j == table.Columns.Count - 1) ? "" : "\t\t");
					}
					stringBuilder.Append("\n");
				}
				return stringBuilder.ToString();
			}
		}

		public static Table DataTableToHTMLTable(DataTable dt, bool includeHeaders)
		{
			Table table = new Table();
			TableRow tableRow = null;
			TableCell tableCell = null;
			int count = dt.Rows.Count;
			int count2 = dt.Columns.Count;
			checked
			{
				if (includeHeaders)
				{
					TableHeaderRow tableHeaderRow = new TableHeaderRow();
					TableHeaderCell tableHeaderCell = null;
					for (int i = 0; i < count2; i++)
					{
						tableHeaderCell = new TableHeaderCell();
						tableHeaderCell.Text = dt.Columns[i].ColumnName.ToString();
						tableHeaderRow.Cells.Add(tableHeaderCell);
					}
					table.Rows.Add(tableHeaderRow);
				}
				for (int j = 0; j < count; j++)
				{
					tableRow = new TableRow();
					for (int k = 0; k < count2; k++)
					{
						tableCell = new TableCell();
						tableCell.Text = dt.Rows[j][k].ToString();
						tableRow.Cells.Add(tableCell);
					}
					table.Rows.Add(tableRow);
				}
				return table;
			}
		}

		private void toolStripSeparator7_Click(object sender, EventArgs e)
		{
		}

		private void buttonPrevShipto_Click(object sender, EventArgs e)
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				return;
			}
			string previousID = DatabaseHelper.GetPreviousID("Customer_Address", "AddressID", currentBillingAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (previousID != "")
			{
				currentBillingAddressID = previousID;
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

		private void toolStripButtonMultiPreview_Click(object sender, EventArgs e)
		{
			AttachementDetailsForm attachementDetailsForm = new AttachementDetailsForm();
			attachementDetailsForm.SysDocID = comboBoxSysDoc.SelectedID;
			attachementDetailsForm.VoucherID = textBoxVoucherNumber.Text;
			attachementDetailsForm.SysDocType = SysDocTypes.PropertyServiceInvoice;
			attachementDetailsForm.FormType = "Transaction";
			attachementDetailsForm.Show();
		}

		private void ultraFormattedLinkShipping_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditShippingMethod(comboBoxShippingMethod.SelectedID);
		}

		private void ultraFormattedLinkProject_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJob(comboBoxJob.SelectedID);
		}

		private void ultraFormattedLinkCostCategory_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCostCategory(comboBoxCostCategory.SelectedID);
		}

		private void ultraFormattedLinkCurrency_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(comboBoxCurrency.SelectedID);
		}

		private void pictureBoxDeliveryInr_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonInfo_Click(object sender, EventArgs e)
		{
		}

		private void toolStrip1_MouseEnter(object sender, EventArgs e)
		{
		}

		private void toolStripButtonInfo_MouseEnter(object sender, EventArgs e)
		{
			toolTip.OwnerDraw = true;
			toolTip.Draw += tooltip_Draw;
			toolTip.Popup += tooltip_Popup;
			toolTip.InitialDelay = 500;
			toolTip.IsBalloon = true;
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer", "DeliveryInstructions", "CustomerID", comboBoxCustomer.SelectedID);
			if (fieldValue.ToString() != "" || fieldValue.ToString() != string.Empty)
			{
				toolStrip1.PointToClient(Cursor.Position);
				toolStripButtonInfo.Visible = true;
				toolTip.Show(fieldValue.ToString(), toolStrip1);
				toolTip.ShowAlways = true;
			}
			else
			{
				toolTip.Show("", toolStrip1.Parent);
				toolStripButtonInfo.Visible = false;
			}
		}

		private void toolStripButtonInfo_MouseLeave(object sender, EventArgs e)
		{
			toolTip.Show("", toolStrip1);
		}

		private void ultraFormattedLinkPaymentTerm_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPaymentTerm(comboBoxTerm.SelectedID);
		}

		private void tooltip_Draw(object sender, DrawToolTipEventArgs e)
		{
			Rectangle bounds = e.Bounds;
			bounds.Offset(200, 400);
			toolTip.BackColor = Color.Yellow;
			DrawToolTipEventArgs drawToolTipEventArgs = new DrawToolTipEventArgs(e.Graphics, e.AssociatedWindow, e.AssociatedControl, bounds, e.ToolTipText, Color.Yellow, Color.Yellow, new Font("Courier New", 10f, FontStyle.Bold));
			drawToolTipEventArgs.DrawBackground();
			drawToolTipEventArgs.DrawBorder();
			drawToolTipEventArgs.DrawText(TextFormatFlags.TextBoxControl);
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
			NameValue item = new NameValue(text2, text);
			checkedListBoxDN.Items.Add(item);
			timeStampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("SalesProforma_Invoice", refDocID, refVoucherID);
			refDateTime = DateTime.Parse(timeStampStatus.Tables[0].Rows[0]["TimeStamp"].ToString());
			SalesProformaInvoiceData salesOrderByID = Factory.SalesProformaSystem.GetSalesOrderByID(text, text2);
			DataRow dataRow = salesOrderByID.SalesProformaTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			if (comboBoxCustomer.SelectedID == "")
			{
				comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
			}
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
			if (!string.IsNullOrEmpty(dataRow["TermID"].ToString()))
			{
				comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
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
				if (row["JobID"] != DBNull.Value)
				{
					dataRow3["Job"] = row["JobID"];
				}
				else
				{
					dataRow3["Job"] = row["JobID"];
				}
				if (row["CostCategoryID"] != DBNull.Value)
				{
					dataRow3["CostCategory"] = row["CostCategoryID"];
				}
				else
				{
					dataRow3["CostCategory"] = row["CostCategoryID"];
				}
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
			CalculateTotal();
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxPayeeTaxGroup.SelectedID);
		}

		private void textBoxRoundOff_Leave(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxDiscountAmount_Leave(object sender, EventArgs e)
		{
			showedNotDiscount = false;
			ValidateAllowedDiscount();
		}

		private void textBoxDiscountPercent_Leave(object sender, EventArgs e)
		{
			showedNotDiscount = false;
			ValidateAllowedDiscount();
		}

		private void grantEditPermissionToolStripMenuItem_Click(object sender, EventArgs e)
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
						flag = Factory.SalesInvoiceNISystem.ModifyTransactions(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, text, isModify: true, "");
					}
				}
				if (flag)
				{
					ErrorHelper.WarningMessage("User can able to modify");
				}
			}
		}

		private void checkBoxPriceIncludeTax_CheckedChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				CalculateAllRowsTaxes();
				CalculateTotal();
			}
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

		private void comboBoxSalesperson_SelectedIndexChanged(object sender, EventArgs e)
		{
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Salesperson", "ReportTo", "SalespersonID", comboBoxSalesperson.SelectedID);
			if (fieldValue != null)
			{
				labelReportTo.Text = fieldValue.ToString();
			}
		}

		private void toolStripButtonPaymentAllocation_Click(object sender, EventArgs e)
		{
			DataSet dataSet = null;
			decimal num = default(decimal);
			decimal result = default(decimal);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				PaymentAllocationDialog paymentAllocationDialog = new PaymentAllocationDialog();
				paymentAllocationDialog.EntityType = EntityTypesEnum.Customers;
				paymentAllocationDialog.DataSource = dataSet;
				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					num += decimal.Parse(row["PaymentAmount"].ToString());
					decimal.TryParse(row["Total"].ToString(), out result);
				}
				decimal balance = result - num;
				paymentAllocationDialog.InvoiceAmount = result;
				paymentAllocationDialog.Paid = num;
				paymentAllocationDialog.Balance = balance;
				if (paymentAllocationDialog.ShowDialog(this) == DialogResult.OK)
				{
					paymentAllocationDialog.Close();
				}
			}
			else
			{
				ErrorHelper.InformationMessage("No allocation for this invoice");
			}
		}

		private void toolStripButtonExcelImport_Click(object sender, EventArgs e)
		{
			bool result = false;
			bool.TryParse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ShowSlNo.ToString(), false).ToString(), out result);
			if (result)
			{
				DataSet dataSet = dataGridItems.ImportFromExcel(autoFill: true);
				string text = "";
				string text2 = "";
				decimal num = default(decimal);
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["Item Code"].Value.ToString() != "")
					{
						text = row.Cells["Item Code"].Value.ToString();
						if (Factory.DatabaseSystem.ExistFieldValue("Product", "ProductID", text))
						{
							string value = Factory.DatabaseSystem.GetFieldValue("Product", "Description", "ProductID", text).ToString() ?? null;
							row.Cells["Description"].Value = value;
							string value2 = Factory.DatabaseSystem.GetFieldValue("Product", "UnitID", "ProductID", text).ToString() ?? null;
							row.Cells["Unit"].Value = value2;
							text2 = row.Cells["LotNumber"].Value.ToString();
							num = decimal.Parse(row.Cells["Quantity"].Value.ToString());
							decimal d = decimal.Parse(row.Cells["Price"].Value.ToString());
							row.Cells["Amount"].Value = (num * d).ToString(Format.TotalAmountFormat);
							if (row.Cells["Amount"].Value.ToString() != "")
							{
								CalculateTotal();
							}
							ItemTaxOptions itemTaxOptions = (ItemTaxOptions)byte.Parse(Factory.DatabaseSystem.GetFieldValue("Product", "TaxOption", "ProductID", text).ToString() ?? null);
							row.Cells["TaxOption"].Value = itemTaxOptions;
							switch (itemTaxOptions)
							{
							case ItemTaxOptions.BasedOnCustomer:
								row.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
								break;
							case ItemTaxOptions.Taxable:
								row.Cells["TaxGroupID"].Value = (Factory.DatabaseSystem.GetFieldValue("Product", "TaxGroupID", "ProductID", text).ToString() ?? null);
								break;
							case ItemTaxOptions.NonTaxable:
								row.Cells["TaxGroupID"].Value = DBNull.Value;
								break;
							}
						}
					}
					DataRow[] array = dataSet.Tables[0].Select("[Item Code]='" + text + "'    AND LotNumber='" + text2 + "'");
					DataTable dataTable = new DataTable();
					dataTable = dataSet.Tables[0].Clone();
					for (int i = 0; i < array.Length; i = checked(i + 1))
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["Item Code"] = array[i]["Item Code"];
						dataRow["LotNumber"] = array[i]["LotNumber"];
						dataRow["BinID"] = array[i]["BinID"];
						if (!dataTable.Columns.Contains("ProductID"))
						{
							dataTable.Columns.Add("ProductID");
						}
						dataRow["ProductID"] = array[i]["Item Code"];
						if (!dataTable.Columns.Contains("LocationID"))
						{
							dataTable.Columns.Add("LocationID");
							dataRow["LocationID"] = array[i]["Location"];
						}
						if (!dataTable.Columns.Contains("SourceLotNumber"))
						{
							dataTable.Columns.Add("SourceLotNumber");
						}
						if (!dataTable.Columns.Contains("SoldQty"))
						{
							dataTable.Columns.Add("SoldQty");
							dataRow["SoldQty"] = num;
						}
						dataRow["Cost"] = array[i]["Cost"];
						dataTable.Rows.Add(dataRow);
					}
					row.Cells["Quantity"].Tag = dataTable;
					row.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceLotNumber"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SoldQty"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Reference2"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].Hidden = true;
			}
			else
			{
				dataGridItems.ImportFromExcel(autoFill: true);
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					if (row2.Cells["Item Code"].Value.ToString() != "")
					{
						string text3 = row2.Cells["Item Code"].Value.ToString();
						if (Factory.DatabaseSystem.ExistFieldValue("Product", "ProductID", text3))
						{
							string value3 = Factory.DatabaseSystem.GetFieldValue("Product", "Description", "ProductID", text3).ToString() ?? null;
							row2.Cells["Description"].Value = value3;
							string value4 = Factory.DatabaseSystem.GetFieldValue("Product", "UnitID", "ProductID", text3).ToString() ?? null;
							row2.Cells["Unit"].Value = value4;
							decimal d2 = decimal.Parse(row2.Cells["Quantity"].Value.ToString());
							decimal d3 = decimal.Parse(row2.Cells["Price"].Value.ToString());
							ItemTaxOptions itemTaxOptions2 = (ItemTaxOptions)byte.Parse(Factory.DatabaseSystem.GetFieldValue("Product", "TaxOption", "ProductID", text3).ToString() ?? null);
							row2.Cells["TaxOption"].Value = itemTaxOptions2;
							switch (itemTaxOptions2)
							{
							case ItemTaxOptions.BasedOnCustomer:
								row2.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
								break;
							case ItemTaxOptions.Taxable:
								row2.Cells["TaxGroupID"].Value = (Factory.DatabaseSystem.GetFieldValue("Product", "TaxGroupID", "ProductID", text3).ToString() ?? null);
								break;
							case ItemTaxOptions.NonTaxable:
								row2.Cells["TaxGroupID"].Value = DBNull.Value;
								break;
							}
							row2.Cells["Amount"].Value = (d2 * d3).ToString(Format.TotalAmountFormat);
							if (row2.Cells["Amount"].Value.ToString() != "")
							{
								CalculateTotal();
							}
						}
					}
				}
			}
			CalculateAllRowsTaxes();
			CalculateTotal();
			textBoxDiscountPercent.Modified = true;
			textBoxDiscountAmount_Validated(sender, e);
		}

		private void ultraFormattedLinkLabelProperty_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProperty(comboBoxProperty.SelectedID);
		}

		private void ultraFormattedLinkLabelUnit_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPropertyUnit(ComboBoxpropertyUnit.SelectedID);
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void toolStripButtonInfo_MouseHover(object sender, EventArgs e)
		{
			toolTip.OwnerDraw = true;
			toolTip.Draw += tooltip_Draw;
			toolTip.Popup += tooltip_Popup;
			toolTip.InitialDelay = 500;
			toolTip.IsBalloon = true;
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer", "DeliveryInstructions", "CustomerID", comboBoxCustomer.SelectedID);
			if (fieldValue.ToString() != "" || fieldValue.ToString() != string.Empty)
			{
				toolStrip1.Parent.PointToClient(Cursor.Position);
				toolStripButtonInfo.Visible = true;
				toolTip.Show(fieldValue.ToString(), toolStrip1);
			}
			else
			{
				toolTip.Show("", toolStrip1.Parent);
				toolStripButtonInfo.Visible = false;
			}
		}

		private void tooltip_Popup(object sender, PopupEventArgs e)
		{
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
								bool flag3 = Factory.SalesInvoiceNISystem.IsBelowMinPrice(ultraGridRow.Cells["Item Code"].Value.ToString(), ultraGridRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString()), comboBoxSysDoc.LocationID);
								if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
								{
									flag3 = Factory.SalesInvoiceNISystem.IsBelowMinAllowedPrice(ultraGridRow.Cells["Item Code"].Value.ToString(), ultraGridRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString()));
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
							if (CompanyPreferences.PricelessCostAction != 1 && Factory.SalesInvoiceNISystem.IsBelowAverageCost(ultraGridRow.Cells["Item Code"].Value.ToString(), ultraGridRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString())))
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental.PropertyServiceInvoice));
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			tabPageItems = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			groupBoxTax = new System.Windows.Forms.GroupBox();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			ComboBoxitemcostCategory = new Micromind.DataControls.CostCategoryComboBox();
			ComboBoxitemJob = new Micromind.DataControls.JobComboBox();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxJob1 = new Micromind.DataControls.JobComboBox();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			comboBoxSpecification = new Micromind.DataControls.ProductSpecificationComboBox();
			comboBoxStyle = new Micromind.DataControls.ProductStyleComboBox();
			labelVoided = new System.Windows.Forms.Label();
			tabPageExpense = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridExpense = new Micromind.DataControls.DataEntryGrid();
			comboBoxGridExpenseCode = new Micromind.DataControls.ExpenseCodeComboBox();
			comboBoxGridCurrency = new Micromind.DataControls.CurrencyComboBox();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMultiPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonExcelImport = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripButtonBalance = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPaymentAllocation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveAsDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			createFromRentalRegistrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromSalesOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			grantEditPermissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonInfo = new System.Windows.Forms.ToolStripButton();
			toolStripButtonApproval = new System.Windows.Forms.ToolStripButton();
			toolStripLabelApproval = new System.Windows.Forms.ToolStripLabel();
			toolStripSeparatorApproval = new System.Windows.Forms.ToolStripSeparator();
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
			textBoxUnitName = new System.Windows.Forms.TextBox();
			textBoxPropertyName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabelUnit = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPropertyAgent = new Micromind.DataControls.PropertyAgentComboBox();
			ultraFormattedLinkLabelProperty = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabelPropertyAgent = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxProperty = new Micromind.DataControls.PropertyComboBox();
			ComboBoxpropertyUnit = new Micromind.DataControls.PropertyUnitComboBox();
			labelTaxGroup = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPayeeTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			ultraFormattedLinkPaymentTerm = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label4 = new System.Windows.Forms.Label();
			textBoxRef2 = new System.Windows.Forms.TextBox();
			comboBoxTerm = new Micromind.DataControls.PaymentTermComboBox();
			comboBoxBillingAddress = new Micromind.DataControls.CustomerAddressComboBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label2 = new System.Windows.Forms.Label();
			textBoxPONumber = new System.Windows.Forms.TextBox();
			textBoxBilltoAddress = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			dateTimePickerDueDate = new System.Windows.Forms.DateTimePicker();
			comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
			labelReportTo = new System.Windows.Forms.Label();
			buttonSelectDocument = new Micromind.UISupport.XPButton();
			ultraFormattedLinkProject = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkShipping = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkCostCategory = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxShipto = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			labelSelectedDocs = new System.Windows.Forms.Label();
			checkedListBoxDN = new System.Windows.Forms.ListBox();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			toolTipController1 = new DevExpress.Utils.ToolTipController(components);
			timer1 = new System.Windows.Forms.Timer(components);
			ultraToolTipManager1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(components);
			checkBoxPriceIncludeTax = new System.Windows.Forms.CheckBox();
			label5 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label14 = new System.Windows.Forms.Label();
			textBoxTotalExpense = new Micromind.UISupport.AmountTextBox();
			textBoxDiscountPercent = new Micromind.UISupport.PercentTextBox();
			textBoxDiscountAmount = new Micromind.UISupport.AmountTextBox();
			panelNonTax = new System.Windows.Forms.Panel();
			linkLabelTax = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxTaxAmount = new Micromind.UISupport.AmountTextBox();
			textBoxSubtotal = new Micromind.UISupport.AmountTextBox();
			panelTotal = new System.Windows.Forms.Panel();
			label9 = new System.Windows.Forms.Label();
			textBoxRoundOff = new Micromind.UISupport.AmountTextBox();
			textBoxTotal = new Micromind.UISupport.AmountTextBox();
			label8 = new System.Windows.Forms.Label();
			comboBoxGridIncomeCode = new Micromind.DataControls.PropertyIncomeCodeComboBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
			comboBoxShippingAddressID = new Micromind.DataControls.CustomerAddressComboBox();
			comboBoxShippingMethod = new Micromind.DataControls.ShippingMethodsComboBox();
			panel2 = new System.Windows.Forms.Panel();
			tabPageItems.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			groupBoxTax.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemcostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSpecification).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStyle).BeginInit();
			tabPageExpense.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyAgent).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxpropertyUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTerm).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBillingAddress).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panel1.SuspendLayout();
			panelNonTax.SuspendLayout();
			panelTotal.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridIncomeCode).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingAddressID).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).BeginInit();
			panel2.SuspendLayout();
			SuspendLayout();
			tabPageItems.Controls.Add(dataGridItems);
			tabPageItems.Controls.Add(groupBoxTax);
			tabPageItems.Controls.Add(ComboBoxitemcostCategory);
			tabPageItems.Controls.Add(ComboBoxitemJob);
			tabPageItems.Controls.Add(comboBoxGridItem);
			tabPageItems.Controls.Add(comboBoxGridLocation);
			tabPageItems.Controls.Add(comboBoxJob1);
			tabPageItems.Controls.Add(productPhotoViewer);
			tabPageItems.Controls.Add(comboBoxSpecification);
			tabPageItems.Controls.Add(comboBoxStyle);
			tabPageItems.Controls.Add(labelVoided);
			tabPageItems.Location = new System.Drawing.Point(23, 1);
			tabPageItems.Name = "tabPageItems";
			tabPageItems.Size = new System.Drawing.Size(955, 135);
			dataGridItems.AllowAddNew = false;
			dataGridItems.AllowCustomizeHeaders = true;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(5, 5);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(944, 134);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			groupBoxTax.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			groupBoxTax.Controls.Add(tableLayoutPanel1);
			groupBoxTax.Location = new System.Drawing.Point(630, -12);
			groupBoxTax.Name = "groupBoxTax";
			groupBoxTax.Size = new System.Drawing.Size(201, 92);
			groupBoxTax.TabIndex = 128;
			groupBoxTax.TabStop = false;
			groupBoxTax.Text = "Tax :";
			groupBoxTax.Visible = false;
			tableLayoutPanel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			tableLayoutPanel1.AutoSize = true;
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.Location = new System.Drawing.Point(5, 12);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76f));
			tableLayoutPanel1.Size = new System.Drawing.Size(193, 76);
			tableLayoutPanel1.TabIndex = 132;
			ComboBoxitemcostCategory.Assigned = false;
			ComboBoxitemcostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxitemcostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxitemcostCategory.CustomReportFieldName = "";
			ComboBoxitemcostCategory.CustomReportKey = "";
			ComboBoxitemcostCategory.CustomReportValueType = 1;
			ComboBoxitemcostCategory.DescriptionTextBox = null;
			ComboBoxitemcostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxitemcostCategory.Editable = true;
			ComboBoxitemcostCategory.FilterString = "";
			ComboBoxitemcostCategory.HasAllAccount = false;
			ComboBoxitemcostCategory.HasCustom = false;
			ComboBoxitemcostCategory.IsDataLoaded = false;
			ComboBoxitemcostCategory.Location = new System.Drawing.Point(404, 99);
			ComboBoxitemcostCategory.MaxDropDownItems = 12;
			ComboBoxitemcostCategory.Name = "ComboBoxitemcostCategory";
			ComboBoxitemcostCategory.ShowInactiveItems = false;
			ComboBoxitemcostCategory.ShowQuickAdd = true;
			ComboBoxitemcostCategory.Size = new System.Drawing.Size(127, 20);
			ComboBoxitemcostCategory.TabIndex = 158;
			ComboBoxitemcostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxitemcostCategory.Visible = false;
			ComboBoxitemJob.Assigned = false;
			ComboBoxitemJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxitemJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxitemJob.CustomReportFieldName = "";
			ComboBoxitemJob.CustomReportKey = "";
			ComboBoxitemJob.CustomReportValueType = 1;
			ComboBoxitemJob.DescriptionTextBox = null;
			ComboBoxitemJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxitemJob.Editable = true;
			ComboBoxitemJob.FilterString = "";
			ComboBoxitemJob.HasAllAccount = false;
			ComboBoxitemJob.HasCustom = false;
			ComboBoxitemJob.IsDataLoaded = false;
			ComboBoxitemJob.Location = new System.Drawing.Point(231, 99);
			ComboBoxitemJob.MaxDropDownItems = 12;
			ComboBoxitemJob.Name = "ComboBoxitemJob";
			ComboBoxitemJob.ShowInactiveItems = false;
			ComboBoxitemJob.ShowQuickAdd = true;
			ComboBoxitemJob.Size = new System.Drawing.Size(100, 20);
			ComboBoxitemJob.TabIndex = 157;
			ComboBoxitemJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxitemJob.Visible = false;
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.AlwaysInEditMode = true;
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance13;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
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
			comboBoxGridItem.Location = new System.Drawing.Point(497, 19);
			comboBoxGridItem.MaxDropDownItems = 12;
			comboBoxGridItem.Name = "comboBoxGridItem";
			comboBoxGridItem.Show3PLItems = true;
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(74, 20);
			comboBoxGridItem.TabIndex = 118;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
			comboBoxGridLocation.AlwaysInEditMode = true;
			comboBoxGridLocation.Assigned = false;
			comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridLocation.CustomReportFieldName = "";
			comboBoxGridLocation.CustomReportKey = "";
			comboBoxGridLocation.CustomReportValueType = 1;
			comboBoxGridLocation.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridLocation.DisplayLayout.Appearance = appearance25;
			comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridLocation.Editable = true;
			comboBoxGridLocation.FilterString = "";
			comboBoxGridLocation.HasAllAccount = false;
			comboBoxGridLocation.HasCustom = false;
			comboBoxGridLocation.IsDataLoaded = false;
			comboBoxGridLocation.Location = new System.Drawing.Point(471, 34);
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
			comboBoxJob1.Assigned = false;
			comboBoxJob1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJob1.CustomReportFieldName = "";
			comboBoxJob1.CustomReportKey = "";
			comboBoxJob1.CustomReportValueType = 1;
			comboBoxJob1.DescriptionTextBox = null;
			comboBoxJob1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJob1.Editable = true;
			comboBoxJob1.FilterString = "";
			comboBoxJob1.HasAllAccount = false;
			comboBoxJob1.HasCustom = false;
			comboBoxJob1.IsDataLoaded = false;
			comboBoxJob1.Location = new System.Drawing.Point(528, 52);
			comboBoxJob1.MaxDropDownItems = 12;
			comboBoxJob1.Name = "comboBoxJob1";
			comboBoxJob1.ShowInactiveItems = false;
			comboBoxJob1.ShowQuickAdd = true;
			comboBoxJob1.Size = new System.Drawing.Size(100, 20);
			comboBoxJob1.TabIndex = 128;
			comboBoxJob1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJob1.Visible = false;
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(8, 37);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 120;
			productPhotoViewer.Visible = false;
			comboBoxSpecification.Assigned = false;
			comboBoxSpecification.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSpecification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSpecification.CustomReportFieldName = "";
			comboBoxSpecification.CustomReportKey = "";
			comboBoxSpecification.CustomReportValueType = 1;
			comboBoxSpecification.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSpecification.DisplayLayout.Appearance = appearance37;
			comboBoxSpecification.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSpecification.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSpecification.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxSpecification.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSpecification.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxSpecification.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSpecification.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSpecification.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSpecification.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxSpecification.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSpecification.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSpecification.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxSpecification.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSpecification.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxSpecification.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxSpecification.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSpecification.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxSpecification.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxSpecification.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSpecification.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxSpecification.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSpecification.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSpecification.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSpecification.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSpecification.Editable = true;
			comboBoxSpecification.FilterString = "";
			comboBoxSpecification.HasAllAccount = false;
			comboBoxSpecification.HasCustom = false;
			comboBoxSpecification.IsDataLoaded = false;
			comboBoxSpecification.Location = new System.Drawing.Point(478, 130);
			comboBoxSpecification.MaxDropDownItems = 12;
			comboBoxSpecification.Name = "comboBoxSpecification";
			comboBoxSpecification.ShowInactiveItems = false;
			comboBoxSpecification.Size = new System.Drawing.Size(100, 20);
			comboBoxSpecification.TabIndex = 160;
			comboBoxSpecification.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSpecification.Visible = false;
			comboBoxStyle.Assigned = false;
			comboBoxStyle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxStyle.CustomReportFieldName = "";
			comboBoxStyle.CustomReportKey = "";
			comboBoxStyle.CustomReportValueType = 1;
			comboBoxStyle.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxStyle.DisplayLayout.Appearance = appearance49;
			comboBoxStyle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxStyle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStyle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxStyle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStyle.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxStyle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxStyle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxStyle.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxStyle.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxStyle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxStyle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxStyle.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxStyle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxStyle.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxStyle.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxStyle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxStyle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxStyle.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxStyle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxStyle.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxStyle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxStyle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxStyle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxStyle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxStyle.Editable = true;
			comboBoxStyle.FilterString = "";
			comboBoxStyle.HasAllAccount = false;
			comboBoxStyle.HasCustom = false;
			comboBoxStyle.IsDataLoaded = false;
			comboBoxStyle.Location = new System.Drawing.Point(284, 103);
			comboBoxStyle.MaxDropDownItems = 12;
			comboBoxStyle.Name = "comboBoxStyle";
			comboBoxStyle.ShowInactiveItems = false;
			comboBoxStyle.Size = new System.Drawing.Size(154, 20);
			comboBoxStyle.TabIndex = 159;
			comboBoxStyle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxStyle.Visible = false;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(-93, 80);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(1114, 59);
			labelVoided.TabIndex = 0;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			tabPageExpense.Controls.Add(dataGridExpense);
			tabPageExpense.Controls.Add(comboBoxGridExpenseCode);
			tabPageExpense.Controls.Add(comboBoxGridCurrency);
			tabPageExpense.Location = new System.Drawing.Point(-10000, -10000);
			tabPageExpense.Name = "tabPageExpense";
			tabPageExpense.Size = new System.Drawing.Size(955, 135);
			dataGridExpense.AllowAddNew = false;
			dataGridExpense.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridExpense.DisplayLayout.Appearance = appearance61;
			dataGridExpense.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridExpense.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			dataGridExpense.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			dataGridExpense.DisplayLayout.MaxColScrollRegions = 1;
			dataGridExpense.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridExpense.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridExpense.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			dataGridExpense.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridExpense.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridExpense.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridExpense.DisplayLayout.Override.CellAppearance = appearance68;
			dataGridExpense.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridExpense.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			dataGridExpense.DisplayLayout.Override.HeaderAppearance = appearance70;
			dataGridExpense.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridExpense.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			dataGridExpense.DisplayLayout.Override.RowAppearance = appearance71;
			dataGridExpense.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridExpense.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			dataGridExpense.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridExpense.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridExpense.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridExpense.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridExpense.ExitEditModeOnLeave = false;
			dataGridExpense.IncludeLotItems = false;
			dataGridExpense.LoadLayoutFailed = false;
			dataGridExpense.Location = new System.Drawing.Point(3, 2);
			dataGridExpense.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridExpense.Name = "dataGridExpense";
			dataGridExpense.ShowClearMenu = true;
			dataGridExpense.ShowDeleteMenu = true;
			dataGridExpense.ShowInsertMenu = true;
			dataGridExpense.ShowMoveRowsMenu = true;
			dataGridExpense.Size = new System.Drawing.Size(949, 268);
			dataGridExpense.TabIndex = 2;
			dataGridExpense.Text = "dataEntryGrid1";
			comboBoxGridExpenseCode.AlwaysInEditMode = true;
			comboBoxGridExpenseCode.Assigned = false;
			comboBoxGridExpenseCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridExpenseCode.CustomReportFieldName = "";
			comboBoxGridExpenseCode.CustomReportKey = "";
			comboBoxGridExpenseCode.CustomReportValueType = 1;
			comboBoxGridExpenseCode.DescriptionTextBox = null;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridExpenseCode.DisplayLayout.Appearance = appearance73;
			comboBoxGridExpenseCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridExpenseCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxGridExpenseCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridExpenseCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridExpenseCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxGridExpenseCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridExpenseCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridExpenseCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridExpenseCode.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridExpenseCode.Editable = true;
			comboBoxGridExpenseCode.FilterString = "";
			comboBoxGridExpenseCode.HasAllAccount = false;
			comboBoxGridExpenseCode.HasCustom = false;
			comboBoxGridExpenseCode.IsDataLoaded = false;
			comboBoxGridExpenseCode.Location = new System.Drawing.Point(557, 40);
			comboBoxGridExpenseCode.MaxDropDownItems = 12;
			comboBoxGridExpenseCode.Name = "comboBoxGridExpenseCode";
			comboBoxGridExpenseCode.ShowInactiveItems = false;
			comboBoxGridExpenseCode.ShowQuickAdd = true;
			comboBoxGridExpenseCode.Size = new System.Drawing.Size(124, 20);
			comboBoxGridExpenseCode.TabIndex = 121;
			comboBoxGridExpenseCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridExpenseCode.Visible = false;
			comboBoxGridCurrency.AlwaysInEditMode = true;
			comboBoxGridCurrency.Assigned = false;
			comboBoxGridCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCurrency.CustomReportFieldName = "";
			comboBoxGridCurrency.CustomReportKey = "";
			comboBoxGridCurrency.CustomReportValueType = 1;
			comboBoxGridCurrency.DescriptionTextBox = null;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCurrency.DisplayLayout.Appearance = appearance85;
			comboBoxGridCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance86.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.Appearance = appearance86;
			appearance87.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance87;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance88.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance88.BackColor2 = System.Drawing.SystemColors.Control;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance88;
			comboBoxGridCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance89;
			appearance90.BackColor = System.Drawing.SystemColors.Highlight;
			appearance90.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance90;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.CardAreaAppearance = appearance91;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			appearance92.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCurrency.DisplayLayout.Override.CellAppearance = appearance92;
			comboBoxGridCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance93.BackColor = System.Drawing.SystemColors.Control;
			appearance93.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance93.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance93.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance93;
			appearance94.TextHAlignAsString = "Left";
			comboBoxGridCurrency.DisplayLayout.Override.HeaderAppearance = appearance94;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCurrency.DisplayLayout.Override.RowAppearance = appearance95;
			comboBoxGridCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance96;
			comboBoxGridCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridCurrency.Editable = true;
			comboBoxGridCurrency.FilterString = "";
			comboBoxGridCurrency.HasAllAccount = false;
			comboBoxGridCurrency.HasCustom = false;
			comboBoxGridCurrency.IsDataLoaded = false;
			comboBoxGridCurrency.Location = new System.Drawing.Point(350, 104);
			comboBoxGridCurrency.MaxDropDownItems = 12;
			comboBoxGridCurrency.Name = "comboBoxGridCurrency";
			comboBoxGridCurrency.ShowInactiveItems = false;
			comboBoxGridCurrency.ShowQuickAdd = true;
			comboBoxGridCurrency.Size = new System.Drawing.Size(95, 20);
			comboBoxGridCurrency.TabIndex = 122;
			comboBoxGridCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCurrency.Visible = false;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[27]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator6,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator7,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonMultiPreview,
				toolStripSeparator5,
				toolStripButtonDistribution,
				toolStripSeparator9,
				toolStripButtonExcelImport,
				toolStripButtonInformation,
				toolStripButtonBalance,
				toolStripButtonPaymentAllocation,
				toolStripDropDownButton1,
				toolStripButtonInfo,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(993, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.MouseEnter += new System.EventHandler(toolStrip1_MouseEnter);
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
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator7.Click += new System.EventHandler(toolStripSeparator7_Click);
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripSeparator9.Name = "toolStripSeparator9";
			toolStripSeparator9.Size = new System.Drawing.Size(6, 31);
			toolStripButtonExcelImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonExcelImport.Image = Micromind.ClientUI.Properties.Resources.excelimport;
			toolStripButtonExcelImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonExcelImport.Name = "toolStripButtonExcelImport";
			toolStripButtonExcelImport.Size = new System.Drawing.Size(28, 28);
			toolStripButtonExcelImport.Text = "Import from Excel";
			toolStripButtonExcelImport.Click += new System.EventHandler(toolStripButtonExcelImport_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripButtonBalance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonBalance.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonBalance.Image");
			toolStripButtonBalance.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonBalance.Name = "toolStripButtonBalance";
			toolStripButtonBalance.Size = new System.Drawing.Size(28, 28);
			toolStripButtonBalance.Text = "Check Customer Balance";
			toolStripButtonBalance.Visible = false;
			toolStripButtonBalance.Click += new System.EventHandler(toolStripButtonBalance_Click);
			toolStripButtonPaymentAllocation.Image = Micromind.ClientUI.Properties.Resources.allocate;
			toolStripButtonPaymentAllocation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPaymentAllocation.Name = "toolStripButtonPaymentAllocation";
			toolStripButtonPaymentAllocation.Size = new System.Drawing.Size(82, 28);
			toolStripButtonPaymentAllocation.Text = "Payment";
			toolStripButtonPaymentAllocation.Visible = false;
			toolStripButtonPaymentAllocation.Click += new System.EventHandler(toolStripButtonPaymentAllocation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8]
			{
				duplicateToolStripMenuItem,
				saveAsDraftToolStripMenuItem,
				loadDraftToolStripMenuItem,
				toolStripSeparator4,
				createFromRentalRegistrationToolStripMenuItem,
				createFromSalesOrderToolStripMenuItem,
				toolStripSeparator8,
				grantEditPermissionToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			duplicateToolStripMenuItem.Text = "Copy";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			saveAsDraftToolStripMenuItem.Name = "saveAsDraftToolStripMenuItem";
			saveAsDraftToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			saveAsDraftToolStripMenuItem.Text = "Save as Draft";
			saveAsDraftToolStripMenuItem.Click += new System.EventHandler(saveAsDraftToolStripMenuItem_Click);
			loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
			loadDraftToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			loadDraftToolStripMenuItem.Text = "Load Draft...";
			loadDraftToolStripMenuItem.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(236, 6);
			toolStripSeparator4.Visible = false;
			createFromRentalRegistrationToolStripMenuItem.Name = "createFromRentalRegistrationToolStripMenuItem";
			createFromRentalRegistrationToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			createFromRentalRegistrationToolStripMenuItem.Text = "Create from Rental Registration";
			createFromRentalRegistrationToolStripMenuItem.Click += new System.EventHandler(createFromRentalRegistrationToolStripMenuItem_Click);
			createFromSalesOrderToolStripMenuItem.Name = "createFromSalesOrderToolStripMenuItem";
			createFromSalesOrderToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			createFromSalesOrderToolStripMenuItem.Text = "Create from Sales Order...";
			createFromSalesOrderToolStripMenuItem.Visible = false;
			createFromSalesOrderToolStripMenuItem.Click += new System.EventHandler(createFromSalesOrderToolStripMenuItem_Click);
			toolStripSeparator8.Name = "toolStripSeparator8";
			toolStripSeparator8.Size = new System.Drawing.Size(236, 6);
			toolStripSeparator8.Visible = false;
			grantEditPermissionToolStripMenuItem.Name = "grantEditPermissionToolStripMenuItem";
			grantEditPermissionToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
			grantEditPermissionToolStripMenuItem.Text = "Grant Edit Permission";
			grantEditPermissionToolStripMenuItem.Visible = false;
			grantEditPermissionToolStripMenuItem.Click += new System.EventHandler(grantEditPermissionToolStripMenuItem_Click);
			toolStripButtonInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInfo.Image = Micromind.ClientUI.Properties.Resources.Alert;
			toolStripButtonInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInfo.Name = "toolStripButtonInfo";
			toolStripButtonInfo.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInfo.Visible = false;
			toolStripButtonInfo.Click += new System.EventHandler(toolStripButtonInfo_Click);
			toolStripButtonInfo.MouseLeave += new System.EventHandler(toolStripButtonInfo_MouseLeave);
			toolStripButtonInfo.MouseHover += new System.EventHandler(toolStripButtonInfo_MouseHover);
			toolStripButtonApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonApproval.AutoSize = false;
			toolStripButtonApproval.BackColor = System.Drawing.Color.Transparent;
			toolStripButtonApproval.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			toolStripButtonApproval.ForeColor = System.Drawing.Color.Green;
			toolStripButtonApproval.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			toolStripButtonApproval.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonApproval.Name = "toolStripButtonApproval";
			toolStripButtonApproval.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			toolStripButtonApproval.Size = new System.Drawing.Size(70, 22);
			toolStripButtonApproval.Text = "Pending";
			toolStripLabelApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripLabelApproval.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			toolStripLabelApproval.Name = "toolStripLabelApproval";
			toolStripLabelApproval.Size = new System.Drawing.Size(45, 28);
			toolStripLabelApproval.Text = "Status:";
			toolStripSeparatorApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripSeparatorApproval.Name = "toolStripSeparatorApproval";
			toolStripSeparatorApproval.Size = new System.Drawing.Size(6, 31);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 482);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(993, 40);
			panelButtons.TabIndex = 7;
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
			linePanelDown.Size = new System.Drawing.Size(993, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(883, 8);
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
			dateTimePickerDate.Location = new System.Drawing.Point(732, 8);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(142, 20);
			dateTimePickerDate.TabIndex = 11;
			textBoxVoucherNumber.Location = new System.Drawing.Point(326, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(181, 20);
			textBoxVoucherNumber.TabIndex = 2;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(643, 57);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(89, 13);
			label1.TabIndex = 13;
			label1.Text = "Register Number:";
			textBoxRef1.Location = new System.Drawing.Point(732, 53);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(142, 20);
			textBoxRef1.TabIndex = 13;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(52, 357);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(306, 117);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 361);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 1;
			label3.Text = "Note:";
			appearance97.FontData.BoldAsString = "True";
			appearance97.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance97;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(214, 4);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 1;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance98.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance98;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(textBoxUnitName);
			panelDetails.Controls.Add(textBoxPropertyName);
			panelDetails.Controls.Add(ultraFormattedLinkLabelUnit);
			panelDetails.Controls.Add(comboBoxPropertyAgent);
			panelDetails.Controls.Add(ultraFormattedLinkLabelProperty);
			panelDetails.Controls.Add(ultraFormattedLinkLabelPropertyAgent);
			panelDetails.Controls.Add(comboBoxProperty);
			panelDetails.Controls.Add(ComboBoxpropertyUnit);
			panelDetails.Controls.Add(labelTaxGroup);
			panelDetails.Controls.Add(comboBoxPayeeTaxGroup);
			panelDetails.Controls.Add(ultraFormattedLinkPaymentTerm);
			panelDetails.Controls.Add(ultraFormattedLinkCurrency);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(textBoxRef2);
			panelDetails.Controls.Add(comboBoxTerm);
			panelDetails.Controls.Add(comboBoxBillingAddress);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxPONumber);
			panelDetails.Controls.Add(textBoxBilltoAddress);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxCustomer);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxCustomerName);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDueDate);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(comboBoxCurrency);
			panelDetails.Controls.Add(labelReportTo);
			panelDetails.Location = new System.Drawing.Point(0, 33);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(891, 174);
			panelDetails.TabIndex = 0;
			textBoxUnitName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUnitName.Location = new System.Drawing.Point(210, 44);
			textBoxUnitName.MaxLength = 64;
			textBoxUnitName.Name = "textBoxUnitName";
			textBoxUnitName.ReadOnly = true;
			textBoxUnitName.Size = new System.Drawing.Size(297, 20);
			textBoxUnitName.TabIndex = 212;
			textBoxUnitName.TabStop = false;
			textBoxPropertyName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPropertyName.Location = new System.Drawing.Point(210, 22);
			textBoxPropertyName.MaxLength = 64;
			textBoxPropertyName.Name = "textBoxPropertyName";
			textBoxPropertyName.ReadOnly = true;
			textBoxPropertyName.Size = new System.Drawing.Size(297, 20);
			textBoxPropertyName.TabIndex = 211;
			textBoxPropertyName.TabStop = false;
			appearance99.FontData.BoldAsString = "True";
			appearance99.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelUnit.Appearance = appearance99;
			ultraFormattedLinkLabelUnit.AutoSize = true;
			ultraFormattedLinkLabelUnit.Location = new System.Drawing.Point(12, 49);
			ultraFormattedLinkLabelUnit.Name = "ultraFormattedLinkLabelUnit";
			ultraFormattedLinkLabelUnit.Size = new System.Drawing.Size(31, 15);
			ultraFormattedLinkLabelUnit.TabIndex = 210;
			ultraFormattedLinkLabelUnit.TabStop = true;
			ultraFormattedLinkLabelUnit.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelUnit.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelUnit.Value = "Unit:";
			appearance100.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelUnit.VisitedLinkAppearance = appearance100;
			ultraFormattedLinkLabelUnit.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelUnit_LinkClicked);
			comboBoxPropertyAgent.Assigned = false;
			comboBoxPropertyAgent.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPropertyAgent.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPropertyAgent.CustomReportFieldName = "";
			comboBoxPropertyAgent.CustomReportKey = "";
			comboBoxPropertyAgent.CustomReportValueType = 1;
			comboBoxPropertyAgent.DescriptionTextBox = null;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPropertyAgent.DisplayLayout.Appearance = appearance101;
			comboBoxPropertyAgent.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPropertyAgent.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance102.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.Appearance = appearance102;
			appearance103.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.BandLabelAppearance = appearance103;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance104.BackColor2 = System.Drawing.SystemColors.Control;
			appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance104.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.PromptAppearance = appearance104;
			comboBoxPropertyAgent.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPropertyAgent.DisplayLayout.MaxRowScrollRegions = 1;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPropertyAgent.DisplayLayout.Override.ActiveCellAppearance = appearance105;
			appearance106.BackColor = System.Drawing.SystemColors.Highlight;
			appearance106.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPropertyAgent.DisplayLayout.Override.ActiveRowAppearance = appearance106;
			comboBoxPropertyAgent.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPropertyAgent.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyAgent.DisplayLayout.Override.CardAreaAppearance = appearance107;
			appearance108.BorderColor = System.Drawing.Color.Silver;
			appearance108.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPropertyAgent.DisplayLayout.Override.CellAppearance = appearance108;
			comboBoxPropertyAgent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPropertyAgent.DisplayLayout.Override.CellPadding = 0;
			appearance109.BackColor = System.Drawing.SystemColors.Control;
			appearance109.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance109.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance109.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyAgent.DisplayLayout.Override.GroupByRowAppearance = appearance109;
			appearance110.TextHAlignAsString = "Left";
			comboBoxPropertyAgent.DisplayLayout.Override.HeaderAppearance = appearance110;
			comboBoxPropertyAgent.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPropertyAgent.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.BorderColor = System.Drawing.Color.Silver;
			comboBoxPropertyAgent.DisplayLayout.Override.RowAppearance = appearance111;
			comboBoxPropertyAgent.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPropertyAgent.DisplayLayout.Override.TemplateAddRowAppearance = appearance112;
			comboBoxPropertyAgent.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPropertyAgent.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPropertyAgent.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPropertyAgent.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPropertyAgent.Editable = true;
			comboBoxPropertyAgent.FilterString = "";
			comboBoxPropertyAgent.HasAllAccount = false;
			comboBoxPropertyAgent.HasCustom = false;
			comboBoxPropertyAgent.IsDataLoaded = false;
			comboBoxPropertyAgent.Location = new System.Drawing.Point(509, 96);
			comboBoxPropertyAgent.MaxDropDownItems = 12;
			comboBoxPropertyAgent.Name = "comboBoxPropertyAgent";
			comboBoxPropertyAgent.ShowInactiveItems = false;
			comboBoxPropertyAgent.ShowQuickAdd = true;
			comboBoxPropertyAgent.Size = new System.Drawing.Size(128, 20);
			comboBoxPropertyAgent.TabIndex = 9;
			comboBoxPropertyAgent.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance113.FontData.BoldAsString = "True";
			appearance113.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelProperty.Appearance = appearance113;
			ultraFormattedLinkLabelProperty.AutoSize = true;
			ultraFormattedLinkLabelProperty.Location = new System.Drawing.Point(12, 26);
			ultraFormattedLinkLabelProperty.Name = "ultraFormattedLinkLabelProperty";
			ultraFormattedLinkLabelProperty.Size = new System.Drawing.Size(56, 15);
			ultraFormattedLinkLabelProperty.TabIndex = 209;
			ultraFormattedLinkLabelProperty.TabStop = true;
			ultraFormattedLinkLabelProperty.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelProperty.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelProperty.Value = "Property:";
			appearance114.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelProperty.VisitedLinkAppearance = appearance114;
			ultraFormattedLinkLabelProperty.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelProperty_LinkClicked);
			appearance115.FontData.BoldAsString = "False";
			appearance115.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelPropertyAgent.Appearance = appearance115;
			ultraFormattedLinkLabelPropertyAgent.AutoSize = true;
			ultraFormattedLinkLabelPropertyAgent.Location = new System.Drawing.Point(430, 98);
			ultraFormattedLinkLabelPropertyAgent.Name = "ultraFormattedLinkLabelPropertyAgent";
			ultraFormattedLinkLabelPropertyAgent.Size = new System.Drawing.Size(37, 15);
			ultraFormattedLinkLabelPropertyAgent.TabIndex = 168;
			ultraFormattedLinkLabelPropertyAgent.TabStop = true;
			ultraFormattedLinkLabelPropertyAgent.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelPropertyAgent.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelPropertyAgent.Value = "Agent:";
			appearance116.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelPropertyAgent.VisitedLinkAppearance = appearance116;
			comboBoxProperty.Assigned = false;
			comboBoxProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProperty.CustomReportFieldName = "";
			comboBoxProperty.CustomReportKey = "";
			comboBoxProperty.CustomReportValueType = 1;
			comboBoxProperty.DescriptionTextBox = textBoxPropertyName;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProperty.DisplayLayout.Appearance = appearance117;
			comboBoxProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance118.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.GroupByBox.Appearance = appearance118;
			appearance119.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance119;
			comboBoxProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance120.BackColor2 = System.Drawing.SystemColors.Control;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance120;
			comboBoxProperty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProperty.DisplayLayout.Override.ActiveCellAppearance = appearance121;
			appearance122.BackColor = System.Drawing.SystemColors.Highlight;
			appearance122.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProperty.DisplayLayout.Override.ActiveRowAppearance = appearance122;
			comboBoxProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.CardAreaAppearance = appearance123;
			appearance124.BorderColor = System.Drawing.Color.Silver;
			appearance124.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProperty.DisplayLayout.Override.CellAppearance = appearance124;
			comboBoxProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProperty.DisplayLayout.Override.CellPadding = 0;
			appearance125.BackColor = System.Drawing.SystemColors.Control;
			appearance125.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance125.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance125.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.GroupByRowAppearance = appearance125;
			appearance126.TextHAlignAsString = "Left";
			comboBoxProperty.DisplayLayout.Override.HeaderAppearance = appearance126;
			comboBoxProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.BorderColor = System.Drawing.Color.Silver;
			comboBoxProperty.DisplayLayout.Override.RowAppearance = appearance127;
			comboBoxProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance128.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance128;
			comboBoxProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProperty.Editable = true;
			comboBoxProperty.FilterString = "";
			comboBoxProperty.HasAllAccount = false;
			comboBoxProperty.HasCustom = false;
			comboBoxProperty.IsDataLoaded = false;
			comboBoxProperty.Location = new System.Drawing.Point(99, 23);
			comboBoxProperty.MaxDropDownItems = 12;
			comboBoxProperty.Name = "comboBoxProperty";
			comboBoxProperty.ShowInactiveItems = false;
			comboBoxProperty.ShowQuickAdd = true;
			comboBoxProperty.Size = new System.Drawing.Size(109, 20);
			comboBoxProperty.TabIndex = 3;
			comboBoxProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxpropertyUnit.Assigned = false;
			ComboBoxpropertyUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxpropertyUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxpropertyUnit.CustomReportFieldName = "";
			ComboBoxpropertyUnit.CustomReportKey = "";
			ComboBoxpropertyUnit.CustomReportValueType = 1;
			ComboBoxpropertyUnit.DescriptionTextBox = textBoxUnitName;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxpropertyUnit.DisplayLayout.Appearance = appearance129;
			ComboBoxpropertyUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxpropertyUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance130.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance130.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance130.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.Appearance = appearance130;
			appearance131.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance131;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance132.BackColor2 = System.Drawing.SystemColors.Control;
			appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxpropertyUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance132;
			ComboBoxpropertyUnit.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxpropertyUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxpropertyUnit.DisplayLayout.Override.ActiveCellAppearance = appearance133;
			appearance134.BackColor = System.Drawing.SystemColors.Highlight;
			appearance134.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxpropertyUnit.DisplayLayout.Override.ActiveRowAppearance = appearance134;
			ComboBoxpropertyUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxpropertyUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxpropertyUnit.DisplayLayout.Override.CardAreaAppearance = appearance135;
			appearance136.BorderColor = System.Drawing.Color.Silver;
			appearance136.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxpropertyUnit.DisplayLayout.Override.CellAppearance = appearance136;
			ComboBoxpropertyUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxpropertyUnit.DisplayLayout.Override.CellPadding = 0;
			appearance137.BackColor = System.Drawing.SystemColors.Control;
			appearance137.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance137.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance137.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance137.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxpropertyUnit.DisplayLayout.Override.GroupByRowAppearance = appearance137;
			appearance138.TextHAlignAsString = "Left";
			ComboBoxpropertyUnit.DisplayLayout.Override.HeaderAppearance = appearance138;
			ComboBoxpropertyUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxpropertyUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.BorderColor = System.Drawing.Color.Silver;
			ComboBoxpropertyUnit.DisplayLayout.Override.RowAppearance = appearance139;
			ComboBoxpropertyUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance140.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxpropertyUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance140;
			ComboBoxpropertyUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxpropertyUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxpropertyUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxpropertyUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxpropertyUnit.Editable = true;
			ComboBoxpropertyUnit.FilterString = "";
			ComboBoxpropertyUnit.HasAllAccount = false;
			ComboBoxpropertyUnit.HasCustom = false;
			ComboBoxpropertyUnit.IsDataLoaded = false;
			ComboBoxpropertyUnit.Location = new System.Drawing.Point(99, 45);
			ComboBoxpropertyUnit.MaxDropDownItems = 12;
			ComboBoxpropertyUnit.Name = "ComboBoxpropertyUnit";
			ComboBoxpropertyUnit.ShowActiveOnly = false;
			ComboBoxpropertyUnit.ShowInactiveItems = false;
			ComboBoxpropertyUnit.ShowQuickAdd = true;
			ComboBoxpropertyUnit.Size = new System.Drawing.Size(109, 20);
			ComboBoxpropertyUnit.TabIndex = 4;
			ComboBoxpropertyUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance141.FontData.BoldAsString = "False";
			appearance141.FontData.Name = "Tahoma";
			labelTaxGroup.Appearance = appearance141;
			labelTaxGroup.AutoSize = true;
			labelTaxGroup.Location = new System.Drawing.Point(430, 121);
			labelTaxGroup.Name = "labelTaxGroup";
			labelTaxGroup.Size = new System.Drawing.Size(59, 15);
			labelTaxGroup.TabIndex = 148;
			labelTaxGroup.TabStop = true;
			labelTaxGroup.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelTaxGroup.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelTaxGroup.Value = "Tax Group:";
			appearance142.ForeColor = System.Drawing.Color.Blue;
			labelTaxGroup.VisitedLinkAppearance = appearance142;
			labelTaxGroup.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			comboBoxPayeeTaxGroup.Assigned = false;
			comboBoxPayeeTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPayeeTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayeeTaxGroup.CustomReportFieldName = "";
			comboBoxPayeeTaxGroup.CustomReportKey = "";
			comboBoxPayeeTaxGroup.CustomReportValueType = 1;
			comboBoxPayeeTaxGroup.DescriptionTextBox = null;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayeeTaxGroup.DisplayLayout.Appearance = appearance143;
			comboBoxPayeeTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayeeTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance144.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance144.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance144.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance144;
			appearance145.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance145;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance146.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance146.BackColor2 = System.Drawing.SystemColors.Control;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance146.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance146;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance147.BackColor = System.Drawing.SystemColors.Window;
			appearance147.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance147;
			appearance148.BackColor = System.Drawing.SystemColors.Highlight;
			appearance148.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance148;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance149;
			appearance150.BorderColor = System.Drawing.Color.Silver;
			appearance150.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellAppearance = appearance150;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance151.BackColor = System.Drawing.SystemColors.Control;
			appearance151.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance151.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance151.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance151.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance151;
			appearance152.TextHAlignAsString = "Left";
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance152;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance153.BackColor = System.Drawing.SystemColors.Window;
			appearance153.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowAppearance = appearance153;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance154.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance154;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayeeTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayeeTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayeeTaxGroup.Editable = true;
			comboBoxPayeeTaxGroup.FilterString = "";
			comboBoxPayeeTaxGroup.HasAllAccount = false;
			comboBoxPayeeTaxGroup.HasCustom = false;
			comboBoxPayeeTaxGroup.IsDataLoaded = false;
			comboBoxPayeeTaxGroup.Location = new System.Drawing.Point(509, 118);
			comboBoxPayeeTaxGroup.MaxDropDownItems = 12;
			comboBoxPayeeTaxGroup.Name = "comboBoxPayeeTaxGroup";
			comboBoxPayeeTaxGroup.ReadOnly = true;
			comboBoxPayeeTaxGroup.ShowInactiveItems = false;
			comboBoxPayeeTaxGroup.ShowQuickAdd = true;
			comboBoxPayeeTaxGroup.Size = new System.Drawing.Size(128, 20);
			comboBoxPayeeTaxGroup.TabIndex = 10;
			comboBoxPayeeTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance155.FontData.BoldAsString = "False";
			appearance155.FontData.Name = "Tahoma";
			ultraFormattedLinkPaymentTerm.Appearance = appearance155;
			ultraFormattedLinkPaymentTerm.AutoSize = true;
			ultraFormattedLinkPaymentTerm.Location = new System.Drawing.Point(430, 77);
			ultraFormattedLinkPaymentTerm.Name = "ultraFormattedLinkPaymentTerm";
			ultraFormattedLinkPaymentTerm.Size = new System.Drawing.Size(79, 15);
			ultraFormattedLinkPaymentTerm.TabIndex = 146;
			ultraFormattedLinkPaymentTerm.TabStop = true;
			ultraFormattedLinkPaymentTerm.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkPaymentTerm.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkPaymentTerm.Value = "Payment Term:";
			appearance156.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkPaymentTerm.VisitedLinkAppearance = appearance156;
			ultraFormattedLinkPaymentTerm.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkPaymentTerm_LinkClicked);
			appearance157.FontData.BoldAsString = "False";
			appearance157.FontData.Name = "Tahoma";
			ultraFormattedLinkCurrency.Appearance = appearance157;
			ultraFormattedLinkCurrency.AutoSize = true;
			ultraFormattedLinkCurrency.Location = new System.Drawing.Point(643, 123);
			ultraFormattedLinkCurrency.Name = "ultraFormattedLinkCurrency";
			ultraFormattedLinkCurrency.Size = new System.Drawing.Size(52, 15);
			ultraFormattedLinkCurrency.TabIndex = 142;
			ultraFormattedLinkCurrency.TabStop = true;
			ultraFormattedLinkCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkCurrency.Value = "Currency:";
			appearance158.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkCurrency.VisitedLinkAppearance = appearance158;
			ultraFormattedLinkCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkCurrency_LinkClicked);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(643, 79);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(60, 13);
			label4.TabIndex = 15;
			label4.Text = "Reference:";
			textBoxRef2.Location = new System.Drawing.Point(732, 75);
			textBoxRef2.MaxLength = 20;
			textBoxRef2.Name = "textBoxRef2";
			textBoxRef2.Size = new System.Drawing.Size(142, 20);
			textBoxRef2.TabIndex = 14;
			comboBoxTerm.Assigned = false;
			comboBoxTerm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTerm.CustomReportFieldName = "";
			comboBoxTerm.CustomReportKey = "";
			comboBoxTerm.CustomReportValueType = 1;
			comboBoxTerm.DescriptionTextBox = null;
			appearance159.BackColor = System.Drawing.SystemColors.Window;
			appearance159.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTerm.DisplayLayout.Appearance = appearance159;
			comboBoxTerm.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTerm.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance160.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance160.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance160.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance160.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.GroupByBox.Appearance = appearance160;
			appearance161.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTerm.DisplayLayout.GroupByBox.BandLabelAppearance = appearance161;
			comboBoxTerm.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance162.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance162.BackColor2 = System.Drawing.SystemColors.Control;
			appearance162.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance162.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTerm.DisplayLayout.GroupByBox.PromptAppearance = appearance162;
			comboBoxTerm.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTerm.DisplayLayout.MaxRowScrollRegions = 1;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			appearance163.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTerm.DisplayLayout.Override.ActiveCellAppearance = appearance163;
			appearance164.BackColor = System.Drawing.SystemColors.Highlight;
			appearance164.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTerm.DisplayLayout.Override.ActiveRowAppearance = appearance164;
			comboBoxTerm.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTerm.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance165.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.Override.CardAreaAppearance = appearance165;
			appearance166.BorderColor = System.Drawing.Color.Silver;
			appearance166.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTerm.DisplayLayout.Override.CellAppearance = appearance166;
			comboBoxTerm.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTerm.DisplayLayout.Override.CellPadding = 0;
			appearance167.BackColor = System.Drawing.SystemColors.Control;
			appearance167.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance167.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance167.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance167.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.Override.GroupByRowAppearance = appearance167;
			appearance168.TextHAlignAsString = "Left";
			comboBoxTerm.DisplayLayout.Override.HeaderAppearance = appearance168;
			comboBoxTerm.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTerm.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance169.BackColor = System.Drawing.SystemColors.Window;
			appearance169.BorderColor = System.Drawing.Color.Silver;
			comboBoxTerm.DisplayLayout.Override.RowAppearance = appearance169;
			comboBoxTerm.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance170.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTerm.DisplayLayout.Override.TemplateAddRowAppearance = appearance170;
			comboBoxTerm.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTerm.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTerm.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTerm.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTerm.Editable = true;
			comboBoxTerm.FilterString = "";
			comboBoxTerm.HasAllAccount = false;
			comboBoxTerm.HasCustom = false;
			comboBoxTerm.IsDataLoaded = false;
			comboBoxTerm.Location = new System.Drawing.Point(509, 74);
			comboBoxTerm.MaxDropDownItems = 12;
			comboBoxTerm.Name = "comboBoxTerm";
			comboBoxTerm.ShowInactiveItems = false;
			comboBoxTerm.ShowQuickAdd = true;
			comboBoxTerm.Size = new System.Drawing.Size(128, 20);
			comboBoxTerm.TabIndex = 8;
			comboBoxTerm.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBillingAddress.Assigned = false;
			comboBoxBillingAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBillingAddress.CustomReportFieldName = "";
			comboBoxBillingAddress.CustomReportKey = "";
			comboBoxBillingAddress.CustomReportValueType = 1;
			comboBoxBillingAddress.DescriptionTextBox = null;
			appearance171.BackColor = System.Drawing.SystemColors.Window;
			appearance171.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBillingAddress.DisplayLayout.Appearance = appearance171;
			comboBoxBillingAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBillingAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance172.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance172.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance172.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance172.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.Appearance = appearance172;
			appearance173.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance173;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance174.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance174.BackColor2 = System.Drawing.SystemColors.Control;
			appearance174.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance174.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance174;
			comboBoxBillingAddress.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBillingAddress.DisplayLayout.MaxRowScrollRegions = 1;
			appearance175.BackColor = System.Drawing.SystemColors.Window;
			appearance175.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBillingAddress.DisplayLayout.Override.ActiveCellAppearance = appearance175;
			appearance176.BackColor = System.Drawing.SystemColors.Highlight;
			appearance176.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBillingAddress.DisplayLayout.Override.ActiveRowAppearance = appearance176;
			comboBoxBillingAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBillingAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance177.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.Override.CardAreaAppearance = appearance177;
			appearance178.BorderColor = System.Drawing.Color.Silver;
			appearance178.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBillingAddress.DisplayLayout.Override.CellAppearance = appearance178;
			comboBoxBillingAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBillingAddress.DisplayLayout.Override.CellPadding = 0;
			appearance179.BackColor = System.Drawing.SystemColors.Control;
			appearance179.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance179.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance179.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance179.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.Override.GroupByRowAppearance = appearance179;
			appearance180.TextHAlignAsString = "Left";
			comboBoxBillingAddress.DisplayLayout.Override.HeaderAppearance = appearance180;
			comboBoxBillingAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBillingAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance181.BackColor = System.Drawing.SystemColors.Window;
			appearance181.BorderColor = System.Drawing.Color.Silver;
			comboBoxBillingAddress.DisplayLayout.Override.RowAppearance = appearance181;
			comboBoxBillingAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance182.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBillingAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance182;
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
			comboBoxBillingAddress.Location = new System.Drawing.Point(99, 89);
			comboBoxBillingAddress.MaxDropDownItems = 12;
			comboBoxBillingAddress.Name = "comboBoxBillingAddress";
			comboBoxBillingAddress.ShowInactiveItems = false;
			comboBoxBillingAddress.ShowQuickAdd = true;
			comboBoxBillingAddress.Size = new System.Drawing.Size(109, 20);
			comboBoxBillingAddress.TabIndex = 6;
			comboBoxBillingAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance183.FontData.BoldAsString = "False";
			appearance183.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance183;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 95);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(38, 15);
			ultraFormattedLinkLabel1.TabIndex = 134;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Bill To:";
			appearance184.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance184;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(643, 101);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(79, 13);
			label2.TabIndex = 17;
			label2.Text = "Customer PO#:";
			textBoxPONumber.Location = new System.Drawing.Point(732, 97);
			textBoxPONumber.MaxLength = 30;
			textBoxPONumber.Name = "textBoxPONumber";
			textBoxPONumber.Size = new System.Drawing.Size(142, 20);
			textBoxPONumber.TabIndex = 15;
			textBoxBilltoAddress.BackColor = System.Drawing.Color.White;
			textBoxBilltoAddress.CustomReportFieldName = "";
			textBoxBilltoAddress.CustomReportKey = "";
			textBoxBilltoAddress.CustomReportValueType = 1;
			textBoxBilltoAddress.IsComboTextBox = false;
			textBoxBilltoAddress.IsModified = false;
			textBoxBilltoAddress.Location = new System.Drawing.Point(99, 111);
			textBoxBilltoAddress.MaxLength = 255;
			textBoxBilltoAddress.Multiline = true;
			textBoxBilltoAddress.Name = "textBoxBilltoAddress";
			textBoxBilltoAddress.Size = new System.Drawing.Size(326, 58);
			textBoxBilltoAddress.TabIndex = 7;
			appearance185.FontData.BoldAsString = "True";
			appearance185.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance185;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(12, 71);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(47, 15);
			ultraFormattedLinkLabel4.TabIndex = 3;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Tenant:";
			appearance186.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance186;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = null;
			appearance187.BackColor = System.Drawing.SystemColors.Window;
			appearance187.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance187;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance188.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance188.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance188.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance188.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance188;
			appearance189.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance189;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance190.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance190.BackColor2 = System.Drawing.SystemColors.Control;
			appearance190.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance190.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance190;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance191.BackColor = System.Drawing.SystemColors.Window;
			appearance191.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance191;
			appearance192.BackColor = System.Drawing.SystemColors.Highlight;
			appearance192.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance192;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance193.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance193;
			appearance194.BorderColor = System.Drawing.Color.Silver;
			appearance194.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance194;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance195.BackColor = System.Drawing.SystemColors.Control;
			appearance195.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance195.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance195.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance195.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance195;
			appearance196.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance196;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance197.BackColor = System.Drawing.SystemColors.Window;
			appearance197.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance197;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance198.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance198;
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
			comboBoxCustomer.Location = new System.Drawing.Point(99, 67);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ShowConsignmentOnly = false;
			comboBoxCustomer.ShowInactive = false;
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = false;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(109, 20);
			comboBoxCustomer.TabIndex = 5;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance199.FontData.BoldAsString = "True";
			appearance199.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance199;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance200.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance200;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance201.BackColor = System.Drawing.SystemColors.Window;
			appearance201.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance201;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance202.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance202.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance202.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance202.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance202;
			appearance203.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance203;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance204.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance204.BackColor2 = System.Drawing.SystemColors.Control;
			appearance204.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance204.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance204;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance205.BackColor = System.Drawing.SystemColors.Window;
			appearance205.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance205;
			appearance206.BackColor = System.Drawing.SystemColors.Highlight;
			appearance206.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance206;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance207.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance207;
			appearance208.BorderColor = System.Drawing.Color.Silver;
			appearance208.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance208;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance209.BackColor = System.Drawing.SystemColors.Control;
			appearance209.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance209.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance209.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance209.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance209;
			appearance210.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance210;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance211.BackColor = System.Drawing.SystemColors.Window;
			appearance211.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance211;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance212.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance212;
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
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(643, 34);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(65, 13);
			mmLabel2.TabIndex = 7;
			mmLabel2.Text = "Due Date:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(643, 12);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 11;
			mmLabel1.Text = "Date:";
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.Location = new System.Drawing.Point(210, 66);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(215, 20);
			textBoxCustomerName.TabIndex = 26;
			textBoxCustomerName.TabStop = false;
			dateTimePickerDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDueDate.Location = new System.Drawing.Point(732, 30);
			dateTimePickerDueDate.Name = "dateTimePickerDueDate";
			dateTimePickerDueDate.Size = new System.Drawing.Size(142, 20);
			dateTimePickerDueDate.TabIndex = 12;
			comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCurrency.Location = new System.Drawing.Point(732, 119);
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
			comboBoxCurrency.Size = new System.Drawing.Size(128, 20);
			comboBoxCurrency.TabIndex = 16;
			labelReportTo.AutoSize = true;
			labelReportTo.Location = new System.Drawing.Point(544, 98);
			labelReportTo.Name = "labelReportTo";
			labelReportTo.Size = new System.Drawing.Size(52, 13);
			labelReportTo.TabIndex = 149;
			labelReportTo.Text = "ReportTo";
			labelReportTo.Visible = false;
			buttonSelectDocument.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocument.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocument.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocument.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocument.Location = new System.Drawing.Point(33, 3);
			buttonSelectDocument.Name = "buttonSelectDocument";
			buttonSelectDocument.Size = new System.Drawing.Size(29, 22);
			buttonSelectDocument.TabIndex = 141;
			buttonSelectDocument.Text = "...";
			buttonSelectDocument.UseVisualStyleBackColor = false;
			buttonSelectDocument.Visible = false;
			buttonSelectDocument.Click += new System.EventHandler(buttonSelectDocument_Click);
			appearance213.FontData.BoldAsString = "False";
			appearance213.FontData.Name = "Tahoma";
			ultraFormattedLinkProject.Appearance = appearance213;
			ultraFormattedLinkProject.AutoSize = true;
			ultraFormattedLinkProject.Location = new System.Drawing.Point(26, 108);
			ultraFormattedLinkProject.Name = "ultraFormattedLinkProject";
			ultraFormattedLinkProject.Size = new System.Drawing.Size(42, 15);
			ultraFormattedLinkProject.TabIndex = 145;
			ultraFormattedLinkProject.TabStop = true;
			ultraFormattedLinkProject.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkProject.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkProject.Value = "Project:";
			ultraFormattedLinkProject.Visible = false;
			appearance214.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkProject.VisitedLinkAppearance = appearance214;
			ultraFormattedLinkProject.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkProject_LinkClicked);
			appearance215.FontData.BoldAsString = "False";
			appearance215.FontData.Name = "Tahoma";
			ultraFormattedLinkShipping.Appearance = appearance215;
			ultraFormattedLinkShipping.AutoSize = true;
			ultraFormattedLinkShipping.Location = new System.Drawing.Point(9, 95);
			ultraFormattedLinkShipping.Name = "ultraFormattedLinkShipping";
			ultraFormattedLinkShipping.Size = new System.Drawing.Size(90, 15);
			ultraFormattedLinkShipping.TabIndex = 36;
			ultraFormattedLinkShipping.TabStop = true;
			ultraFormattedLinkShipping.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkShipping.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkShipping.Value = "Shipping Method:";
			ultraFormattedLinkShipping.Visible = false;
			appearance216.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkShipping.VisitedLinkAppearance = appearance216;
			ultraFormattedLinkShipping.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkShipping_LinkClicked);
			appearance217.FontData.BoldAsString = "False";
			appearance217.FontData.Name = "Tahoma";
			ultraFormattedLinkCostCategory.Appearance = appearance217;
			ultraFormattedLinkCostCategory.AutoSize = true;
			ultraFormattedLinkCostCategory.Location = new System.Drawing.Point(4, 117);
			ultraFormattedLinkCostCategory.Name = "ultraFormattedLinkCostCategory";
			ultraFormattedLinkCostCategory.Size = new System.Drawing.Size(76, 15);
			ultraFormattedLinkCostCategory.TabIndex = 143;
			ultraFormattedLinkCostCategory.TabStop = true;
			ultraFormattedLinkCostCategory.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkCostCategory.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkCostCategory.Value = "Cost Category:";
			ultraFormattedLinkCostCategory.Visible = false;
			appearance218.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkCostCategory.VisitedLinkAppearance = appearance218;
			ultraFormattedLinkCostCategory.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkCostCategory_LinkClicked);
			textBoxShipto.BackColor = System.Drawing.Color.White;
			textBoxShipto.CustomReportFieldName = "";
			textBoxShipto.CustomReportKey = "";
			textBoxShipto.CustomReportValueType = 1;
			textBoxShipto.IsComboTextBox = false;
			textBoxShipto.IsModified = false;
			textBoxShipto.Location = new System.Drawing.Point(26, 103);
			textBoxShipto.MaxLength = 255;
			textBoxShipto.Multiline = true;
			textBoxShipto.Name = "textBoxShipto";
			textBoxShipto.Size = new System.Drawing.Size(74, 59);
			textBoxShipto.TabIndex = 16;
			textBoxShipto.Visible = false;
			appearance219.FontData.BoldAsString = "False";
			appearance219.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance219;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(26, 138);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel3.TabIndex = 14;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Ship To:";
			ultraFormattedLinkLabel3.Visible = false;
			appearance220.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance220;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			appearance221.FontData.BoldAsString = "False";
			appearance221.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance221;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(9, 28);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(66, 15);
			ultraFormattedLinkLabel6.TabIndex = 19;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Salesperson:";
			ultraFormattedLinkLabel6.Visible = false;
			appearance222.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance222;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
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
			labelSelectedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelSelectedDocs.AutoSize = true;
			labelSelectedDocs.Location = new System.Drawing.Point(362, 356);
			labelSelectedDocs.Name = "labelSelectedDocs";
			labelSelectedDocs.Size = new System.Drawing.Size(109, 13);
			labelSelectedDocs.TabIndex = 126;
			labelSelectedDocs.Text = "Selected Documents:";
			labelSelectedDocs.Visible = false;
			checkedListBoxDN.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxDN.FormattingEnabled = true;
			checkedListBoxDN.Location = new System.Drawing.Point(365, 377);
			checkedListBoxDN.Name = "checkedListBoxDN";
			checkedListBoxDN.Size = new System.Drawing.Size(152, 82);
			checkedListBoxDN.TabIndex = 3;
			checkedListBoxDN.Visible = false;
			checkedListBoxDN.DoubleClick += new System.EventHandler(checkedListBoxDN_DoubleClick);
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageItems);
			ultraTabControl1.Controls.Add(tabPageExpense);
			ultraTabControl1.Location = new System.Drawing.Point(0, 207);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(981, 139);
			ultraTabControl1.TabIndex = 1;
			ultraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.LeftTop;
			ultraTab.TabPage = tabPageItems;
			ultraTab.Text = "Items";
			ultraTab2.TabPage = tabPageExpense;
			ultraTab2.Text = "Charges";
			ultraTab2.Visible = false;
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(955, 135);
			ultraToolTipManager1.ContainingControl = this;
			checkBoxPriceIncludeTax.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			checkBoxPriceIncludeTax.AutoSize = true;
			checkBoxPriceIncludeTax.Location = new System.Drawing.Point(603, 353);
			checkBoxPriceIncludeTax.Name = "checkBoxPriceIncludeTax";
			checkBoxPriceIncludeTax.Size = new System.Drawing.Size(123, 17);
			checkBoxPriceIncludeTax.TabIndex = 4;
			checkBoxPriceIncludeTax.Text = "Price inclusive of tax";
			checkBoxPriceIncludeTax.UseVisualStyleBackColor = true;
			checkBoxPriceIncludeTax.Visible = false;
			checkBoxPriceIncludeTax.CheckedChanged += new System.EventHandler(checkBoxPriceIncludeTax_CheckedChanged);
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(4, 3);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(49, 13);
			label5.TabIndex = 133;
			label5.Text = "Subtotal:";
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(115, 24);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(15, 13);
			label11.TabIndex = 148;
			label11.Text = "%";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(4, 23);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(52, 13);
			label7.TabIndex = 164;
			label7.Text = "Discount:";
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(label14);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(textBoxTotalExpense);
			panel1.Controls.Add(textBoxDiscountPercent);
			panel1.Controls.Add(textBoxDiscountAmount);
			panel1.Controls.Add(panelNonTax);
			panel1.Controls.Add(label11);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(textBoxSubtotal);
			panel1.Location = new System.Drawing.Point(761, 346);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(220, 134);
			panel1.TabIndex = 5;
			label14.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			label14.Location = new System.Drawing.Point(4, 44);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(69, 15);
			label14.TabIndex = 162;
			label14.Text = "Charges:";
			label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxTotalExpense.AllowDecimal = true;
			textBoxTotalExpense.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalExpense.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalExpense.CustomReportFieldName = "";
			textBoxTotalExpense.CustomReportKey = "";
			textBoxTotalExpense.CustomReportValueType = 1;
			textBoxTotalExpense.ForeColor = System.Drawing.Color.Black;
			textBoxTotalExpense.IsComboTextBox = false;
			textBoxTotalExpense.IsModified = false;
			textBoxTotalExpense.Location = new System.Drawing.Point(81, 42);
			textBoxTotalExpense.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalExpense.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalExpense.Name = "textBoxTotalExpense";
			textBoxTotalExpense.NullText = "0";
			textBoxTotalExpense.ReadOnly = true;
			textBoxTotalExpense.Size = new System.Drawing.Size(137, 20);
			textBoxTotalExpense.TabIndex = 3;
			textBoxTotalExpense.TabStop = false;
			textBoxTotalExpense.Text = "0.00";
			textBoxTotalExpense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalExpense.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxDiscountPercent.CustomReportFieldName = "";
			textBoxDiscountPercent.CustomReportKey = "";
			textBoxDiscountPercent.CustomReportValueType = 1;
			textBoxDiscountPercent.IsComboTextBox = false;
			textBoxDiscountPercent.IsModified = false;
			textBoxDiscountPercent.Location = new System.Drawing.Point(81, 21);
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
			panelNonTax.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panelNonTax.Controls.Add(linkLabelTax);
			panelNonTax.Controls.Add(textBoxTaxAmount);
			panelNonTax.Location = new System.Drawing.Point(0, 63);
			panelNonTax.Name = "panelNonTax";
			panelNonTax.Size = new System.Drawing.Size(218, 25);
			panelNonTax.TabIndex = 4;
			appearance223.FontData.BoldAsString = "False";
			appearance223.FontData.Name = "Tahoma";
			linkLabelTax.Appearance = appearance223;
			linkLabelTax.AutoSize = true;
			linkLabelTax.Location = new System.Drawing.Point(4, 5);
			linkLabelTax.Name = "linkLabelTax";
			linkLabelTax.Size = new System.Drawing.Size(25, 15);
			linkLabelTax.TabIndex = 164;
			linkLabelTax.TabStop = true;
			linkLabelTax.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelTax.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelTax.Value = "Tax:";
			appearance224.ForeColor = System.Drawing.Color.Blue;
			linkLabelTax.VisitedLinkAppearance = appearance224;
			textBoxTaxAmount.AllowDecimal = true;
			textBoxTaxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTaxAmount.CustomReportFieldName = "";
			textBoxTaxAmount.CustomReportKey = "";
			textBoxTaxAmount.CustomReportValueType = 1;
			textBoxTaxAmount.IsComboTextBox = false;
			textBoxTaxAmount.IsModified = false;
			textBoxTaxAmount.Location = new System.Drawing.Point(81, 2);
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
			textBoxTaxAmount.Size = new System.Drawing.Size(137, 20);
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
			textBoxSubtotal.AllowDecimal = true;
			textBoxSubtotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSubtotal.CustomReportFieldName = "";
			textBoxSubtotal.CustomReportKey = "";
			textBoxSubtotal.CustomReportValueType = 1;
			textBoxSubtotal.ForeColor = System.Drawing.Color.Black;
			textBoxSubtotal.IsComboTextBox = false;
			textBoxSubtotal.IsModified = false;
			textBoxSubtotal.Location = new System.Drawing.Point(81, -1);
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
			textBoxSubtotal.Size = new System.Drawing.Size(137, 20);
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
			panelTotal.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panelTotal.Controls.Add(label9);
			panelTotal.Controls.Add(textBoxRoundOff);
			panelTotal.Controls.Add(textBoxTotal);
			panelTotal.Controls.Add(label8);
			panelTotal.Location = new System.Drawing.Point(761, 433);
			panelTotal.Name = "panelTotal";
			panelTotal.Size = new System.Drawing.Size(220, 47);
			panelTotal.TabIndex = 6;
			label9.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label9.Location = new System.Drawing.Point(4, 7);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(72, 15);
			label9.TabIndex = 174;
			label9.Text = "Round Off:";
			label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxRoundOff.AllowDecimal = true;
			textBoxRoundOff.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxRoundOff.CustomReportFieldName = "";
			textBoxRoundOff.CustomReportKey = "";
			textBoxRoundOff.CustomReportValueType = 1;
			textBoxRoundOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxRoundOff.IsComboTextBox = false;
			textBoxRoundOff.IsModified = false;
			textBoxRoundOff.Location = new System.Drawing.Point(81, 2);
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
			textBoxRoundOff.Size = new System.Drawing.Size(137, 20);
			textBoxRoundOff.TabIndex = 0;
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
			textBoxTotal.AllowDecimal = true;
			textBoxTotal.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotal.CustomReportFieldName = "";
			textBoxTotal.CustomReportKey = "";
			textBoxTotal.CustomReportValueType = 1;
			textBoxTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			textBoxTotal.IsComboTextBox = false;
			textBoxTotal.IsModified = false;
			textBoxTotal.Location = new System.Drawing.Point(81, 23);
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
			textBoxTotal.Size = new System.Drawing.Size(137, 20);
			textBoxTotal.TabIndex = 1;
			textBoxTotal.Text = "0.00";
			textBoxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label8.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(4, 28);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(40, 13);
			label8.TabIndex = 172;
			label8.Text = "Total:";
			comboBoxGridIncomeCode.Assigned = false;
			comboBoxGridIncomeCode.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridIncomeCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridIncomeCode.CustomReportFieldName = "";
			comboBoxGridIncomeCode.CustomReportKey = "";
			comboBoxGridIncomeCode.CustomReportValueType = 1;
			comboBoxGridIncomeCode.DescriptionTextBox = null;
			appearance225.BackColor = System.Drawing.SystemColors.Window;
			appearance225.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridIncomeCode.DisplayLayout.Appearance = appearance225;
			comboBoxGridIncomeCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridIncomeCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance226.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance226.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance226.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance226.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.Appearance = appearance226;
			appearance227.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance227;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance228.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance228.BackColor2 = System.Drawing.SystemColors.Control;
			appearance228.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance228.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.PromptAppearance = appearance228;
			comboBoxGridIncomeCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridIncomeCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance229.BackColor = System.Drawing.SystemColors.Window;
			appearance229.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridIncomeCode.DisplayLayout.Override.ActiveCellAppearance = appearance229;
			appearance230.BackColor = System.Drawing.SystemColors.Highlight;
			appearance230.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridIncomeCode.DisplayLayout.Override.ActiveRowAppearance = appearance230;
			comboBoxGridIncomeCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridIncomeCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance231.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridIncomeCode.DisplayLayout.Override.CardAreaAppearance = appearance231;
			appearance232.BorderColor = System.Drawing.Color.Silver;
			appearance232.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridIncomeCode.DisplayLayout.Override.CellAppearance = appearance232;
			comboBoxGridIncomeCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridIncomeCode.DisplayLayout.Override.CellPadding = 0;
			appearance233.BackColor = System.Drawing.SystemColors.Control;
			appearance233.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance233.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance233.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance233.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridIncomeCode.DisplayLayout.Override.GroupByRowAppearance = appearance233;
			appearance234.TextHAlignAsString = "Left";
			comboBoxGridIncomeCode.DisplayLayout.Override.HeaderAppearance = appearance234;
			comboBoxGridIncomeCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridIncomeCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance235.BackColor = System.Drawing.SystemColors.Window;
			appearance235.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridIncomeCode.DisplayLayout.Override.RowAppearance = appearance235;
			comboBoxGridIncomeCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance236.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridIncomeCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance236;
			comboBoxGridIncomeCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridIncomeCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridIncomeCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridIncomeCode.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridIncomeCode.Editable = true;
			comboBoxGridIncomeCode.FilterString = "";
			comboBoxGridIncomeCode.HasAllAccount = false;
			comboBoxGridIncomeCode.HasCustom = false;
			comboBoxGridIncomeCode.IsDataLoaded = false;
			comboBoxGridIncomeCode.Location = new System.Drawing.Point(4, 81);
			comboBoxGridIncomeCode.MaxDropDownItems = 12;
			comboBoxGridIncomeCode.Name = "comboBoxGridIncomeCode";
			comboBoxGridIncomeCode.ShowInactiveItems = false;
			comboBoxGridIncomeCode.ShowQuickAdd = true;
			comboBoxGridIncomeCode.Size = new System.Drawing.Size(100, 20);
			comboBoxGridIncomeCode.TabIndex = 137;
			comboBoxGridIncomeCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridIncomeCode.Visible = false;
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
			comboBoxCostCategory.Location = new System.Drawing.Point(42, 144);
			comboBoxCostCategory.MaxDropDownItems = 12;
			comboBoxCostCategory.Name = "comboBoxCostCategory";
			comboBoxCostCategory.ShowInactiveItems = false;
			comboBoxCostCategory.ShowQuickAdd = true;
			comboBoxCostCategory.Size = new System.Drawing.Size(42, 20);
			comboBoxCostCategory.TabIndex = 19;
			comboBoxCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCostCategory.Visible = false;
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
			comboBoxJob.Location = new System.Drawing.Point(33, 145);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(78, 20);
			comboBoxJob.TabIndex = 18;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJob.Visible = false;
			comboBoxSalesperson.Assigned = false;
			comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesperson.CustomReportFieldName = "";
			comboBoxSalesperson.CustomReportKey = "";
			comboBoxSalesperson.CustomReportValueType = 1;
			comboBoxSalesperson.DescriptionTextBox = null;
			appearance237.BackColor = System.Drawing.SystemColors.Window;
			appearance237.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesperson.DisplayLayout.Appearance = appearance237;
			comboBoxSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance238.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance238.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance238.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance238.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.GroupByBox.Appearance = appearance238;
			appearance239.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance239;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance240.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance240.BackColor2 = System.Drawing.SystemColors.Control;
			appearance240.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance240.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance240;
			comboBoxSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance241.BackColor = System.Drawing.SystemColors.Window;
			appearance241.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance241;
			appearance242.BackColor = System.Drawing.SystemColors.Highlight;
			appearance242.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance242;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance243.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance243;
			appearance244.BorderColor = System.Drawing.Color.Silver;
			appearance244.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesperson.DisplayLayout.Override.CellAppearance = appearance244;
			comboBoxSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance245.BackColor = System.Drawing.SystemColors.Control;
			appearance245.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance245.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance245.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance245.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance245;
			appearance246.TextHAlignAsString = "Left";
			comboBoxSalesperson.DisplayLayout.Override.HeaderAppearance = appearance246;
			comboBoxSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance247.BackColor = System.Drawing.SystemColors.Window;
			appearance247.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesperson.DisplayLayout.Override.RowAppearance = appearance247;
			comboBoxSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance248.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance248;
			comboBoxSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesperson.Editable = true;
			comboBoxSalesperson.FilterString = "";
			comboBoxSalesperson.HasAllAccount = false;
			comboBoxSalesperson.HasCustom = false;
			comboBoxSalesperson.IsDataLoaded = false;
			comboBoxSalesperson.Location = new System.Drawing.Point(4, 49);
			comboBoxSalesperson.MaxDropDownItems = 12;
			comboBoxSalesperson.Name = "comboBoxSalesperson";
			comboBoxSalesperson.ShowInactiveItems = false;
			comboBoxSalesperson.ShowQuickAdd = true;
			comboBoxSalesperson.Size = new System.Drawing.Size(96, 20);
			comboBoxSalesperson.TabIndex = 20;
			comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSalesperson.Visible = false;
			comboBoxSalesperson.SelectedIndexChanged += new System.EventHandler(comboBoxSalesperson_SelectedIndexChanged);
			comboBoxShippingAddressID.Assigned = false;
			comboBoxShippingAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingAddressID.CustomReportFieldName = "";
			comboBoxShippingAddressID.CustomReportKey = "";
			comboBoxShippingAddressID.CustomReportValueType = 1;
			comboBoxShippingAddressID.DescriptionTextBox = null;
			appearance249.BackColor = System.Drawing.SystemColors.Window;
			appearance249.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingAddressID.DisplayLayout.Appearance = appearance249;
			comboBoxShippingAddressID.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingAddressID.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance250.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance250.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance250.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance250.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.Appearance = appearance250;
			appearance251.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.BandLabelAppearance = appearance251;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance252.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance252.BackColor2 = System.Drawing.SystemColors.Control;
			appearance252.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance252.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.PromptAppearance = appearance252;
			comboBoxShippingAddressID.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingAddressID.DisplayLayout.MaxRowScrollRegions = 1;
			appearance253.BackColor = System.Drawing.SystemColors.Window;
			appearance253.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingAddressID.DisplayLayout.Override.ActiveCellAppearance = appearance253;
			appearance254.BackColor = System.Drawing.SystemColors.Highlight;
			appearance254.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingAddressID.DisplayLayout.Override.ActiveRowAppearance = appearance254;
			comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance255.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.Override.CardAreaAppearance = appearance255;
			appearance256.BorderColor = System.Drawing.Color.Silver;
			appearance256.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingAddressID.DisplayLayout.Override.CellAppearance = appearance256;
			comboBoxShippingAddressID.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingAddressID.DisplayLayout.Override.CellPadding = 0;
			appearance257.BackColor = System.Drawing.SystemColors.Control;
			appearance257.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance257.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance257.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance257.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.Override.GroupByRowAppearance = appearance257;
			appearance258.TextHAlignAsString = "Left";
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderAppearance = appearance258;
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance259.BackColor = System.Drawing.SystemColors.Window;
			appearance259.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingAddressID.DisplayLayout.Override.RowAppearance = appearance259;
			comboBoxShippingAddressID.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance260.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingAddressID.DisplayLayout.Override.TemplateAddRowAppearance = appearance260;
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
			comboBoxShippingAddressID.Location = new System.Drawing.Point(58, 134);
			comboBoxShippingAddressID.MaxDropDownItems = 12;
			comboBoxShippingAddressID.Name = "comboBoxShippingAddressID";
			comboBoxShippingAddressID.ShowInactiveItems = false;
			comboBoxShippingAddressID.ShowQuickAdd = true;
			comboBoxShippingAddressID.Size = new System.Drawing.Size(46, 20);
			comboBoxShippingAddressID.TabIndex = 6;
			comboBoxShippingAddressID.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxShippingAddressID.Visible = false;
			comboBoxShippingMethod.Assigned = false;
			comboBoxShippingMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingMethod.CustomReportFieldName = "";
			comboBoxShippingMethod.CustomReportKey = "";
			comboBoxShippingMethod.CustomReportValueType = 1;
			comboBoxShippingMethod.DescriptionTextBox = null;
			appearance261.BackColor = System.Drawing.SystemColors.Window;
			appearance261.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingMethod.DisplayLayout.Appearance = appearance261;
			comboBoxShippingMethod.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingMethod.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance262.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance262.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance262.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance262.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.Appearance = appearance262;
			appearance263.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BandLabelAppearance = appearance263;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance264.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance264.BackColor2 = System.Drawing.SystemColors.Control;
			appearance264.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance264.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.PromptAppearance = appearance264;
			comboBoxShippingMethod.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingMethod.DisplayLayout.MaxRowScrollRegions = 1;
			appearance265.BackColor = System.Drawing.SystemColors.Window;
			appearance265.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveCellAppearance = appearance265;
			appearance266.BackColor = System.Drawing.SystemColors.Highlight;
			appearance266.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveRowAppearance = appearance266;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance267.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.CardAreaAppearance = appearance267;
			appearance268.BorderColor = System.Drawing.Color.Silver;
			appearance268.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingMethod.DisplayLayout.Override.CellAppearance = appearance268;
			comboBoxShippingMethod.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingMethod.DisplayLayout.Override.CellPadding = 0;
			appearance269.BackColor = System.Drawing.SystemColors.Control;
			appearance269.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance269.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance269.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance269.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.GroupByRowAppearance = appearance269;
			appearance270.TextHAlignAsString = "Left";
			comboBoxShippingMethod.DisplayLayout.Override.HeaderAppearance = appearance270;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance271.BackColor = System.Drawing.SystemColors.Window;
			appearance271.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingMethod.DisplayLayout.Override.RowAppearance = appearance271;
			comboBoxShippingMethod.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance272.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingMethod.DisplayLayout.Override.TemplateAddRowAppearance = appearance272;
			comboBoxShippingMethod.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingMethod.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingMethod.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingMethod.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingMethod.Editable = true;
			comboBoxShippingMethod.FilterString = "";
			comboBoxShippingMethod.HasAllAccount = false;
			comboBoxShippingMethod.HasCustom = false;
			comboBoxShippingMethod.IsDataLoaded = false;
			comboBoxShippingMethod.Location = new System.Drawing.Point(9, 120);
			comboBoxShippingMethod.MaxDropDownItems = 12;
			comboBoxShippingMethod.Name = "comboBoxShippingMethod";
			comboBoxShippingMethod.ShowInactiveItems = false;
			comboBoxShippingMethod.ShowQuickAdd = true;
			comboBoxShippingMethod.Size = new System.Drawing.Size(53, 20);
			comboBoxShippingMethod.TabIndex = 17;
			comboBoxShippingMethod.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxShippingMethod.Visible = false;
			panel2.Controls.Add(buttonSelectDocument);
			panel2.Controls.Add(comboBoxGridIncomeCode);
			panel2.Controls.Add(comboBoxShippingMethod);
			panel2.Controls.Add(comboBoxShippingAddressID);
			panel2.Controls.Add(ultraFormattedLinkLabel3);
			panel2.Controls.Add(comboBoxSalesperson);
			panel2.Controls.Add(textBoxShipto);
			panel2.Controls.Add(comboBoxJob);
			panel2.Controls.Add(ultraFormattedLinkLabel6);
			panel2.Controls.Add(comboBoxCostCategory);
			panel2.Controls.Add(ultraFormattedLinkCostCategory);
			panel2.Controls.Add(ultraFormattedLinkShipping);
			panel2.Controls.Add(ultraFormattedLinkProject);
			panel2.Location = new System.Drawing.Point(949, 151);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(29, 48);
			panel2.TabIndex = 146;
			panel2.Visible = false;
			AllowDrop = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(993, 522);
			base.Controls.Add(panel2);
			base.Controls.Add(panelTotal);
			base.Controls.Add(checkBoxPriceIncludeTax);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Controls.Add(labelSelectedDocs);
			base.Controls.Add(checkedListBoxDN);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(902, 521);
			base.Name = "PropertyServiceInvoice";
			Text = "Property Service Invoice";
			toolTipController1.SetToolTipIconType(this, DevExpress.Utils.ToolTipIconType.Information);
			tabPageItems.ResumeLayout(false);
			tabPageItems.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			groupBoxTax.ResumeLayout(false);
			groupBoxTax.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemcostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSpecification).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStyle).EndInit();
			tabPageExpense.ResumeLayout(false);
			tabPageExpense.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyAgent).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxpropertyUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTerm).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBillingAddress).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panelNonTax.ResumeLayout(false);
			panelNonTax.PerformLayout();
			panelTotal.ResumeLayout(false);
			panelTotal.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridIncomeCode).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingAddressID).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).EndInit();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
