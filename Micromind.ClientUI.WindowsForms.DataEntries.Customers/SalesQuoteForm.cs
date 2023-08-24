using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class SalesQuoteForm : Form, IForm, IWorkFlowForm
	{
		private bool allowEdit = true;

		private SalesQuoteData currentData;

		private const string TABLENAME_CONST = "Sales_Quote";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentCustomerAddressID = "";

		private ItemSourceTypes sourceDocType;

		private DataSet priceListData;

		private bool allowMultiTemplate;

		private bool canAccessCost;

		private bool IsTaxbasedonProfit;

		private bool loadItemDescFromPriceList = CompanyPreferences.LoadItemDescFromPriceList;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool createJobwithSO = CompanyPreferences.CreateProjectwithSO;

		private bool LoadItemFeatures = CompanyPreferences.LoadItemFeatures;

		private decimal TaxPercent = CompanyPreferences.TaxPercent;

		private bool setlastSalesprice = CompanyPreferences.SetlastSalesprice;

		private bool ActivatePartsDetails = CompanyPreferences.ActivatePartsDetails;

		private decimal priceDiscountPercAllowed;

		private decimal TotalDiscountPercAllowed;

		private decimal roundoffamount;

		private bool IsDiscPercentage;

		private bool priceValidationInSQ;

		private int slNo = 1;

		private bool showCost = CompanyPreferences.ShowItemCostInCombo;

		private string currentCustomerID;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool isVoid;

		private bool isDiscountPercent;

		private bool showednotdiscount;

		private bool totalChanged;

		private bool isTaxPercent;

		private bool isloadrev;

		private bool isRevise;

		private string RefDocID;

		private string RefVoucherID;

		private DataSet TimestampStatus;

		private DateTime RefDateTime;

		private bool restrictTransaction;

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

		private ProductComboBox comboBoxGridItem;

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

		private PaymentTermComboBox comboBoxTerm;

		private ShippingMethodsComboBox comboBoxShippingMethod;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private Label label11;

		private Panel panelNonTax;

		private CurrencySelector comboBoxCurrency;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator5;

		private Label label4;

		private ComboBox comboBoxStatus;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerExpiry;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private JobComboBox comboBoxJob;

		private CostCategoryComboBox comboBoxCostCategory;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem saveDraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private ToolStripMenuItem itemSortToolStripMenuItem;

		private CustomerLeadsComboBox comboBoxCustomer;

		private MMTextBox textBoxShipto;

		private CustomerAddressComboBox comboBoxBillingAddress;

		private CustomerAddressComboBox comboBoxShippingAddressID;

		private MMTextBox textBoxBilltoAddress;

		private NonDirtyPanel panelReviseDate;

		private ComboBox comboBoxRevisionNo;

		private Label label13;

		private MMTextBox textBoxLastReviseDate;

		private Label label14;

		private ToolStripMenuItem ReviseToolStripMenuItem;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator8;

		private UltraFormattedLinkLabel labelCurrency;

		private UltraFormattedLinkLabel labelcostcategory;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private UltraFormattedLinkLabel ultraFormattedLinkPayment;

		private UltraFormattedLinkLabel labelJob;

		private ToolStripMenuItem createFromSalesEnquiryToolStripMenuItem;

		private Label label7;

		private PercentTextBox textBoxDiscountPercent;

		private AmountTextBox textBoxDiscountAmount;

		private AmountTextBox textBoxTaxAmount;

		private ToolStripButton toolStripButtonMultiPreview;

		private UltraFormattedLinkLabel labelTaxGroup;

		private TaxGroupComboBox comboBoxPayeeTaxGroup;

		private UltraFormattedLinkLabel linkLabelTax;

		private CheckBox checkBoxPriceIncludeTax;

		private Label label9;

		private AmountTextBox textBoxRoundOff;

		private ToolStripButton toolStripButtonExcelImport;

		private Label labelSelectedDocs;

		private ListBox checkedListBoxDN;

		private Label labelProcessedDocs;

		private ListBox checkedListBoxProcessedDocs;

		private MMLabel mmLabel3;

		private MMTextBox textBoxRemarks2;

		private MMLabel mmLabel39;

		private MMTextBox textBoxRemarks1;

		private ToolStripMenuItem toolStripMenuRelationshipMap;

		private MMTextBox mmTextBoxRevNo;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2012;

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
					CustomerLeadsComboBox customerLeadsComboBox = comboBoxCustomer;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					bool flag3 = textBoxVoucherNumber.Enabled = true;
					flag5 = (sysDocComboBox.Enabled = flag3);
					enabled = (customerLeadsComboBox.Enabled = flag5);
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
				if (createJobwithSO)
				{
					comboBoxJob.Enabled = !value;
				}
				ToolStripButton toolStripButton = toolStripButtonAttach;
				enabled = (toolStripButtonMultiPreview.Visible = !value);
				toolStripButton.Enabled = enabled;
				ToolStripButton toolStripButton2 = toolStripButtonPrint;
				ToolStripButton toolStripButton3 = toolStripButtonPreview;
				flag5 = (toolStripButtonMultiPreview.Enabled = !isNewRecord);
				enabled = (toolStripButton3.Enabled = flag5);
				toolStripButton2.Enabled = enabled;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
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

		public bool IsRevised
		{
			get
			{
				return isRevise;
			}
			set
			{
				isRevise = value;
				panelReviseDate.Visible = isRevise;
				panelReviseDate.Enabled = isRevise;
				comboBoxRevisionNo.Visible = isRevise;
				mmTextBoxRevNo.Visible = !isRevise;
				Text = (isRevise ? "Revise SalesQuote" : "SalesQuote");
				if (isRevise)
				{
					LoadData(textBoxVoucherNumber.Text, IsRevised);
				}
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

		public SalesQuoteForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			labelJob.Visible = (labelcostcategory.Visible = (comboBoxJob.Visible = (comboBoxCostCategory.Visible = useJobCosting)));
		}

		private void AddEvents()
		{
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			base.Load += Form_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
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
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxDiscountAmount.Validated += textBoxDiscountAmount_Validated;
			comboBoxBillingAddress.SelectedIndexChanged += comboBoxBillingAddress_SelectedIndexChanged;
			comboBoxShippingAddressID.SelectedIndexChanged += comboBoxShippingAddressID_SelectedIndexChanged;
			textBoxDiscountPercent.Validated += textBoxDiscountPercent_Validated;
			textBoxDiscountAmount.Validated += textBoxDiscountAmount_Validated;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			comboBoxTerm.SelectedIndexChanged += comboBoxTerm_SelectedIndexChanged;
			comboBoxPayeeTaxGroup.SelectedIndexChanged += comboBoxPayeeTaxGroup_SelectedIndexChanged;
			dataGridItems.KeyPress += dataGridItems_KeyDown;
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

		private void comboBoxTerm_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxPayeeTaxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			CalculateAllRowsTaxes();
			CalculateTotal();
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
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
						dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(num2, Global.CurDecimalPoints);
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

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
			if (LoadItemFeatures && e.KeyCode == Keys.F3 && !ActivatePartsDetails)
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
					comboBoxGridItem.SelectedID = dataGridItems.ActiveCell.Text;
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

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxCustomerName.Text = comboBoxCustomer.SelectedName;
			CurrentCustomerID = comboBoxCustomer.SelectedID;
			comboBoxBillingAddress.Clear();
			comboBoxShippingAddressID.Clear();
			LoadCustomerPriceList();
			try
			{
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
				CheckBox checkBox = checkBoxPriceIncludeTax;
				bool visible = checkBoxPriceIncludeTax.Checked = comboBoxSysDoc.IsPriceIncludeTax;
				checkBox.Visible = visible;
				priceValidationInSQ = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PriceValidationInSQ, 22, comboBoxSysDoc.SelectedID, defaultValue: false);
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
					if (comboBoxGridItem.SelectedRow != null && !(comboBoxGridItem.SelectedID == "") && comboBoxGridItem.SelectedRow != null && comboBoxGridItem.SelectedRow.Cells["ItemType"].Value != null)
					{
						itemTypes = (ItemTypes)checked((byte)int.Parse(comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString()));
					}
					if (itemTypes == ItemTypes.Matrix)
					{
						MatrixSelectionForm matrixSelectionForm = new MatrixSelectionForm();
						matrixSelectionForm.LoadMatrixData(comboBoxGridItem.SelectedID, comboBoxGridItem.SelectedName);
						matrixSelectionForm.AllowNegativeQuantity = false;
						dataGridItems.ActiveRow.Delete(displayPrompt: false);
						if (matrixSelectionForm.ShowDialog(this) == DialogResult.OK)
						{
							foreach (DataRow row in matrixSelectionForm.SelectedItems.Tables[0].Rows)
							{
								UltraGridRow ultraGridRow = dataGridItems.DisplayLayout.Bands[0].AddNew();
								ultraGridRow.Cells["Item Code"].Value = row["ProductID"].ToString();
								ultraGridRow.Cells["Description"].Value = row["Description"].ToString();
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
						return;
					}
					object obj2 = dataGridItems.ActiveRow.Cells["Description"].Value = (dataGridItems.ActiveRow.Cells["DefaultDescription"].Value = comboBoxGridItem.SelectedName);
					dataGridItems.ActiveRow.Cells["Attribute1"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute1"].Value.ToString();
					dataGridItems.ActiveRow.Cells["Attribute2"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute2"].Value.ToString();
					dataGridItems.ActiveRow.Cells["Attribute3"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute3"].Value.ToString();
					dataGridItems.ActiveRow.Cells["MatrixParentID"].Value = comboBoxGridItem.SelectedRow.Cells["MatrixParentID"].Value.ToString();
					if (comboBoxGridItem.SelectedID != "")
					{
						if (comboBoxGridItem.SelectedRow != null)
						{
							dataGridItems.ActiveRow.Cells["ItemType"].Value = comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString();
						}
						else
						{
							dataGridItems.ActiveRow.Cells["ItemType"].Value = null;
						}
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
						if (comboBoxGridItem.SelectedItemType == ItemTypes.Discount)
						{
							dataGridItems.ActiveRow.Cells["Quantity"].Value = -1;
							dataGridItems.ActiveRow.Cells["Quantity"].Activation = Activation.Disabled;
						}
						else
						{
							dataGridItems.ActiveRow.Cells["Quantity"].Activation = Activation.AllowEdit;
						}
						if (setlastSalesprice)
						{
							decimal lastSaleTransationByCustomerID = Factory.ProductSystem.GetLastSaleTransationByCustomerID(comboBoxGridItem.SelectedID, comboBoxCustomer.SelectedID);
							dataGridItems.ActiveRow.Cells["Price"].Value = lastSaleTransationByCustomerID;
						}
						else
						{
							dataGridItems.ActiveRow.Cells["Cost"].Value = comboBoxGridItem.SelectedRow.Cells["StandardCost"].Value.ToString();
						}
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
				if (e.Cell.Column.Key == "Unit" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Unit")
				{
					if (priceListData == null || (dataGridItems.ActiveRow.Cells["Unit"].Value == null && e.Cell.Value.ToString() == ""))
					{
						return;
					}
					SetPriceAndDescription();
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
					ItemTypes itemTypes2 = ItemTypes.None;
					string key = e.Cell.Column.Key;
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result3);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result4);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result5);
					if (key == "Quantity" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Quantity")
					{
						result5 = Math.Round(result3 * result4, Global.CurDecimalPoints);
						dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
					}
					else if (key == "Price" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Price")
					{
						result5 = Math.Round(result3 * result4, Global.CurDecimalPoints);
						dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
					}
					else if (key == "Amount" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Amount")
					{
						if (result3 == 0m)
						{
							result3 = 1m;
							dataGridItems.ActiveRow.Cells["Quantity"].Value = result3;
						}
						if (comboBoxGridItem.SelectedRow != null && comboBoxGridItem.SelectedRow.Cells["ItemType"].Value != null)
						{
							itemTypes2 = (ItemTypes)checked((byte)int.Parse(comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString()));
						}
						if ((itemTypes2 == ItemTypes.Discount && result5 > 0m) || (result3 < 0m && result5 > 0m))
						{
							result5 = -1m * Math.Abs(result5);
							dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
						}
						result4 = Math.Round(result5 / result3, 4);
						if (result5 < 0m)
						{
							result3 = -1m * Math.Abs(result3);
							dataGridItems.ActiveRow.Cells["Quantity"].Value = result3;
						}
						dataGridItems.ActiveRow.Cells["Price"].Value = Math.Abs(result4);
					}
					else if (key == "Item Code")
					{
						if (dataGridItems.ActiveRow.Cells["Quantity"].Value == null || dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString() == "")
						{
							dataGridItems.ActiveRow.Cells["Quantity"].Value = result3;
						}
						result5 = Math.Round(result3 * result4, Global.CurDecimalPoints);
						dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
					}
					else if (e.Cell.Column.Key == "Unit" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Unit")
					{
						result5 = Math.Round(result3 * result4, Global.CurDecimalPoints);
						dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
					}
					if (key == "Amount")
					{
						decimal result6 = default(decimal);
						decimal result7 = default(decimal);
						decimal result8 = default(decimal);
						decimal.TryParse(e.Cell.Value.ToString(), out result8);
						decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result7);
						decimal.TryParse(e.Cell.Row.Cells["Cost"].Value.ToString(), out result6);
						decimal subtotal = decimal.Parse(textBoxSubtotal.Text);
						decimal tradeDiscount = decimal.Parse(textBoxDiscountAmount.Text);
						result6 = result7 * result6;
						UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result6, result8, subtotal, tradeDiscount, checkBoxPriceIncludeTax.Checked);
						CalculateTotal();
					}
					if (key == "Cost")
					{
						decimal result9 = default(decimal);
						decimal.TryParse(e.Cell.Row.Cells["Amount"].Value.ToString(), out result9);
						decimal subtotal2 = decimal.Parse(textBoxSubtotal.Text);
						decimal tradeDiscount2 = decimal.Parse(textBoxDiscountAmount.Text);
						decimal result10 = default(decimal);
						decimal result11 = default(decimal);
						decimal.TryParse(e.Cell.Row.Cells["Quantity"].Value.ToString(), out result11);
						decimal.TryParse(e.Cell.Row.Cells["Cost"].Value.ToString(), out result10);
						result10 = result11 * result10;
						UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result10, result9, subtotal2, tradeDiscount2, checkBoxPriceIncludeTax.Checked);
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
			else if (comboBoxCustomer.EntityType == "Customer")
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
				decimal d = decimal.Parse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString());
				if (a == "M")
				{
					d /= num;
				}
				else
				{
					d *= num;
				}
				d = Math.Round(d, Global.CurDecimalPoints);
				dataGridItems.ActiveRow.Cells["Price"].Value = d;
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
			checked
			{
				if (dataGridItems.ActiveRow != null && (dataGridItems.ActiveRow.Cells["RefSlNo"].Value == null || dataGridItems.ActiveRow.Cells["RefSlNo"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
				{
					int.TryParse(dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["RefSlNo"].Text.ToString(), out slNo);
					dataGridItems.ActiveRow.Cells["RefSlNo"].Value = slNo + 1;
				}
			}
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			comboBoxGridItem.IsLoadingData = true;
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			try
			{
				UltraGridRow activeRow = dataGridItems.ActiveRow;
				bool flag;
				decimal result2;
				decimal result3;
				decimal num2;
				if (activeRow != null && activeRow.Cells["Item Code"].Value != null && !(activeRow.Cells["Item Code"].Value.ToString() == ""))
				{
					flag = false;
					decimal result = default(decimal);
					result2 = default(decimal);
					result3 = default(decimal);
					if (activeRow == null || !activeRow.DataChanged)
					{
						goto IL_0445;
					}
					decimal.TryParse(activeRow.Cells["Price"].Value.ToString(), out result2);
					decimal.TryParse(activeRow.Cells["Cost"].Value.ToString(), out result3);
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
						goto IL_0412;
					}
					flag = true;
					if (!(priceDiscountPercAllowed > 0m))
					{
						goto IL_0412;
					}
					ErrorHelper.WarningMessage("More discount not allowed for the  product-" + activeRow.Cells["Item Code"].Value.ToString() + ".");
					e.Row.Cells["Price"].Value = 0;
					e.Row.Cells["Amount"].Value = 0;
					if (!e.Row.IsAddRow)
					{
						e.Cancel = true;
					}
				}
				goto end_IL_0000;
				IL_0766:
				if (CompanyPreferences.PricelessCostAction != 1 && Factory.SalesInvoiceSystem.IsBelowAverageCost(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(activeRow.Cells["Price"].Value.ToString())))
				{
					if (CompanyPreferences.PricelessCostAction == 2)
					{
						if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the cost. Do you want to continue?") != DialogResult.Yes && !e.Row.IsAddRow)
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
					else if (CompanyPreferences.PricelessCostAction == 4)
					{
						if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the cost. You need to enter password to continue. \nDo you want to continue?") == DialogResult.Yes)
						{
							if (new ApprovalPasswordForm
							{
								CorrectPassword = CompanyPreferences.PricelessCostPassword
							}.ShowDialog() != DialogResult.OK)
							{
								activeRow.Cells["price"].Value = 0;
								activeRow.Cells["Amount"].Value = 0;
								activeRow.Cells["TaxTotal"].Value = 0;
								if (!e.Row.IsAddRow)
								{
									e.Cancel = true;
								}
							}
						}
						else if (!e.Row.IsAddRow)
						{
							e.Cancel = true;
						}
					}
				}
				goto end_IL_0000;
				IL_0445:
				if (priceValidationInSQ)
				{
					if (activeRow == null || !activeRow.DataChanged)
					{
						goto IL_0766;
					}
					if (result2 < result3)
					{
						ErrorHelper.WarningMessage("Price less than cost for the Product:  " + activeRow.Cells["Item Code"].Value.ToString() + ".");
						if (!e.Row.IsAddRow)
						{
							activeRow.CancelUpdate();
							e.Cancel = true;
						}
					}
					else
					{
						if (CompanyPreferences.MinPriceSaleAction == 1)
						{
							goto IL_0766;
						}
						bool flag2 = Factory.SalesInvoiceSystem.IsBelowMinPrice(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(activeRow.Cells["Price"].Value.ToString()), comboBoxSysDoc.LocationID);
						if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
						{
							flag2 = Factory.SalesInvoiceSystem.IsBelowMinAllowedPrice(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), comboBoxCurrency.SelectedID, comboBoxCurrency.Rate, decimal.Parse(activeRow.Cells["Price"].Value.ToString()));
						}
						if (!flag2)
						{
							goto IL_0766;
						}
						if (CompanyPreferences.MinPriceSaleAction == 2)
						{
							if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the minimum price. Do you want to continue?") == DialogResult.Yes)
							{
								goto IL_0766;
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
								goto IL_0766;
							}
							if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the minimum price. You need to enter password to continue. \nDo you want to continue?") == DialogResult.Yes)
							{
								if (new ApprovalPasswordForm
								{
									CorrectPassword = CompanyPreferences.MinPriceSalePassword
								}.ShowDialog() == DialogResult.OK)
								{
									goto IL_0766;
								}
								activeRow.Cells["price"].Value = 0;
								activeRow.Cells["Amount"].Value = 0;
								activeRow.Cells["TaxTotal"].Value = 0;
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
					}
				}
				goto end_IL_0000;
				IL_0412:
				if (Global.IsUserAdmin || priceDiscountPercAllowed == 0m)
				{
					flag = false;
				}
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowPriceDiscount) && result2 < num2)
				{
					flag = true;
				}
				goto IL_0445;
				end_IL_0000:;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
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
			if (activeRow != null)
			{
				if (activeRow.Cells["Item Code"].Value.ToString() == "" && activeRow.Cells["Quantity"].Value.ToString() != "")
				{
					ErrorHelper.InformationMessage("Please select an item.");
					e.Cancel = true;
					activeRow.Cells["Item Code"].Activate();
				}
				else if (activeRow.Cells["Quantity"].Value.ToString() == "")
				{
					activeRow.Cells["Quantity"].Value = 0;
				}
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
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
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new SalesQuoteData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.SalesQuoteTable.Rows[0] : currentData.SalesQuoteTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["SalesFlow"] = CompanyPreferences.LocalSalesFlow;
				if (dateTimePickerExpiry.Checked)
				{
					dataRow["ExpiryDate"] = dateTimePickerExpiry.Value;
				}
				else
				{
					dataRow["ExpiryDate"] = DBNull.Value;
				}
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["CustomerAddress"] = textBoxBilltoAddress.Text;
				dataRow["ShipToAddress"] = textBoxShipto.Text;
				if (comboBoxCustomer.EntityType == "Customer")
				{
					dataRow["EntityType"] = 1;
				}
				else
				{
					dataRow["EntityType"] = 2;
				}
				if (comboBoxShippingAddressID.SelectedID != "")
				{
					dataRow["ShippingAddressID"] = comboBoxShippingAddressID.SelectedID;
				}
				else
				{
					dataRow["ShippingAddressID"] = DBNull.Value;
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
				if (comboBoxPayeeTaxGroup.SelectedID != "")
				{
					dataRow["PayeeTaxGroupID"] = comboBoxPayeeTaxGroup.SelectedID;
				}
				else
				{
					dataRow["PayeeTaxGroupID"] = DBNull.Value;
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
				checked
				{
					dataRow["Status"] = comboBoxStatus.SelectedIndex + 1;
					if (comboBoxCurrency.SelectedID != "")
					{
						dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
					}
					else
					{
						dataRow["CurrencyID"] = DBNull.Value;
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
					dataRow["PONumber"] = textBoxPONumber.Text;
					dataRow["Discount"] = textBoxDiscountAmount.Text;
					dataRow["TaxAmount"] = textBoxTaxAmount.Text;
					dataRow["Total"] = textBoxTotal.Text;
					roundoffamount = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
					dataRow["RoundOff"] = roundoffamount;
					dataRow["LastRevisedDate"] = dateTimePickerDate.Value;
					if (textBoxLastReviseDate.Text != "")
					{
						dataRow["PreviousRevisedDate"] = DateTime.Parse(textBoxLastReviseDate.Text);
					}
					else
					{
						dataRow["PreviousRevisedDate"] = DBNull.Value;
					}
					dataRow["Remarks1"] = textBoxRemarks1.Text;
					dataRow["Remarks2"] = textBoxRemarks2.Text;
					dataRow.EndEdit();
					if (IsNewRecord)
					{
						currentData.SalesQuoteTable.Rows.Add(dataRow);
					}
					foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
					{
						if (!currentData.SalesQuoteDetailTable.Columns.Contains(column.Key))
						{
							currentData.SalesQuoteDetailTable.Columns.Add(column.Key, column.DataType);
						}
					}
					currentData.SalesQuoteDetailTable.Rows.Clear();
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						DataRow dataRow2 = currentData.SalesQuoteDetailTable.NewRow();
						dataRow2.BeginEdit();
						dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
						dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
						dataRow2["JobID"] = row.Cells["Job"].Value.ToString();
						dataRow2["CostCategoryID"] = row.Cells["CostCategory"].Value.ToString();
						if (row.Cells["ItemType"].Value != null && row.Cells["ItemType"].Value.ToString() != "")
						{
							dataRow2["ItemType"] = row.Cells["ItemType"].Value.ToString();
						}
						if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
						{
							dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
						}
						if (row.Cells["Cost"].Value != null && row.Cells["Cost"].Value.ToString() != "")
						{
							dataRow2["Cost"] = row.Cells["Cost"].Value.ToString();
						}
						else
						{
							dataRow2["Cost"] = DBNull.Value;
						}
						dataRow2["UnitPrice"] = row.Cells["Price"].Value.ToString();
						dataRow2["Description"] = row.Cells["Description"].Value.ToString();
						dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
						dataRow2["SourceSysDocID"] = RefDocID;
						dataRow2["SourceVoucherID"] = RefVoucherID;
						dataRow2["Attribute1"] = row.Cells["Attribute1"].Value.ToString();
						dataRow2["Attribute2"] = row.Cells["Attribute2"].Value.ToString();
						dataRow2["Attribute3"] = row.Cells["Attribute3"].Value.ToString();
						dataRow2["MatrixParentID"] = row.Cells["MatrixParentID"].Value.ToString();
						if (!string.IsNullOrEmpty(row.Cells["RefSlNo"].Value.ToString()))
						{
							dataRow2["RefSlNo"] = row.Cells["RefSlNo"].Value.ToString();
						}
						if (!string.IsNullOrEmpty(row.Cells["RefText1"].Value.ToString()))
						{
							dataRow2["RefText1"] = row.Cells["RefText1"].Value.ToString();
						}
						if (!string.IsNullOrEmpty(row.Cells["RefText2"].Value.ToString()))
						{
							dataRow2["RefText2"] = row.Cells["RefText2"].Value.ToString();
						}
						if (!string.IsNullOrEmpty(row.Cells["RefNum1"].Value.ToString()))
						{
							dataRow2["RefNum1"] = row.Cells["RefNum1"].Value.ToString();
						}
						if (!string.IsNullOrEmpty(row.Cells["RefNum2"].Value.ToString()))
						{
							dataRow2["RefNum2"] = row.Cells["RefNum2"].Value.ToString();
						}
						if (!string.IsNullOrEmpty(row.Cells["RefDate1"].Value.ToString()))
						{
							dataRow2["RefDate1"] = row.Cells["RefDate1"].Value.ToString();
						}
						if (!string.IsNullOrEmpty(row.Cells["RefDate2"].Value.ToString()))
						{
							dataRow2["RefDate2"] = row.Cells["RefDate2"].Value.ToString();
						}
						if (row.Cells["Tax"].Value != null && row.Cells["Tax"].Value.ToString() != "")
						{
							dataRow2["TaxAmount"] = row.Cells["Tax"].Value.ToString();
						}
						else
						{
							dataRow2["TaxAmount"] = DBNull.Value;
						}
						if (!row.Cells["TaxOption"].Value.IsNullOrEmpty())
						{
							dataRow2["TaxOption"] = row.Cells["TaxOption"].Value.ToString();
						}
						else
						{
							dataRow2["TaxOption"] = (byte)2;
						}
						if (row.Cells["TaxGroupID"].Value != null && row.Cells["TaxGroupID"].Value.ToString() != "")
						{
							dataRow2["TaxGroupID"] = row.Cells["TaxGroupID"].Value.ToString();
						}
						else
						{
							dataRow2["TaxGroupID"] = DBNull.Value;
						}
						dataRow2["RowIndex"] = row.Index;
						dataRow2.EndEdit();
						currentData.SalesQuoteDetailTable.Rows.Add(dataRow2);
					}
					currentData.Tables["Tax_Detail"].Rows.Clear();
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					int num = 0;
					foreach (UltraGridRow row2 in dataGridItems.Rows)
					{
						if (row2.Cells["Tax"].Tag != null)
						{
							TaxHelper.CreateTaxRows(currentData, row2.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Items, selectedID, text, num, comboBoxCurrency.SelectedID, comboBoxCurrency.Rate);
						}
						num++;
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
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("Job");
				dataTable.Columns.Add("CostCategory");
				dataTable.Columns.Add("SOSysDocID");
				dataTable.Columns.Add("SOVoucherID");
				dataTable.Columns.Add("SORowIndex", typeof(int));
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Remarks");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("TaxGroupID");
				dataTable.Columns.Add("TaxOption", typeof(byte));
				dataTable.Columns.Add("Tax", typeof(decimal));
				dataTable.Columns.Add("Attribute1");
				dataTable.Columns.Add("Attribute2");
				dataTable.Columns.Add("Attribute3");
				dataTable.Columns.Add("RefSlNo", typeof(int));
				dataTable.Columns.Add("RefText1");
				dataTable.Columns.Add("RefText2");
				dataTable.Columns.Add("RefNum1", typeof(decimal));
				dataTable.Columns.Add("RefNum2", typeof(decimal));
				dataTable.Columns.Add("RefDate1", typeof(DateTime));
				dataTable.Columns.Add("RefDate2", typeof(DateTime));
				dataTable.Columns.Add("MatrixParentID");
				dataTable.Columns.Add("DefaultDescription");
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("ItemType");
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Cost", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
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
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Hidden = true;
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"];
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"];
				UltraGridColumn ultraGridColumn10 = dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"];
				bool flag8 = dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = true;
				bool flag10 = ultraGridColumn10.Hidden = flag8;
				bool flag12 = ultraGridColumn9.Hidden = flag10;
				flag2 = (ultraGridColumn8.Hidden = flag12);
				flag4 = (ultraGridColumn7.Hidden = flag2);
				hidden = (ultraGridColumn6.Hidden = flag4);
				ultraGridColumn5.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Hidden = !useJobCosting;
				if (createJobwithSO && useJobCosting)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Hidden = false;
				}
				else if (!createJobwithSO && useJobCosting)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Hidden = false;
				}
				else if (!useJobCosting)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Hidden = true;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				if (string.IsNullOrEmpty(CompanyPreferences.RefDate1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefDate2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefSlNo))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefText1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefText2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefNum1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefNum2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Hidden = false;
				}
				dataGridItems.LoadLayout();
				UltraGridColumn ultraGridColumn11 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn12 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn13 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				Activation activation2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].CellActivation = Activation.Disabled;
				Activation activation4 = ultraGridColumn13.CellActivation = activation2;
				Activation activation7 = ultraGridColumn11.CellActivation = (ultraGridColumn12.CellActivation = activation4);
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Hidden = !canAccessCost;
				dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOSysDocID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SOVoucherID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SORowIndex"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				ExcludeAdditionalColumns();
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance;
				AppearanceBase cellAppearance4 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].CellAppearance;
				AppearanceBase cellAppearance5 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance;
				Color color = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				Color color3 = cellAppearance5.BackColorDisabled = color;
				Color color5 = cellAppearance4.BackColorDisabled = color3;
				Color color7 = cellAppearance3.BackColorDisabled = color5;
				Color color10 = cellAppearance.BackColorDisabled = (cellAppearance2.BackColorDisabled = color7);
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDescription))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = comboBoxGridItem;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
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
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].Header.Caption = "Default Description";
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].CellMultiLine = DefaultableBoolean.True;
				if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SOSysDocID"))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["SOSysDocID"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["SOVoucherID"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["SORowIndex"].Hidden = true;
				}
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangePrice))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.NoEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.NoEdit;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ValueList = comboBoxJob;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].AutoSuggestFilterMode = AutoSuggestFilterMode.Contains;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ValueList = comboBoxCostCategory;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].Header.Caption = "Tax Group";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].Header.Caption = "Net Amount";
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 3000;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].Header.Caption = CompanyPreferences.Attribute1Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].Header.Caption = CompanyPreferences.Attribute2Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].Header.Caption = CompanyPreferences.Attribute3Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Header.Caption = CompanyPreferences.RefSlNo;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Header.Caption = CompanyPreferences.RefText1;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Header.Caption = CompanyPreferences.RefText2;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Header.Caption = CompanyPreferences.RefNum1;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Header.Caption = CompanyPreferences.RefNum2;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Header.Caption = CompanyPreferences.RefDate1;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Header.Caption = CompanyPreferences.RefDate2;
				if (string.IsNullOrEmpty(CompanyPreferences.RefDate1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Hidden = true;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefDate2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = true;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefSlNo))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Hidden = true;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefText1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Hidden = true;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefText2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Hidden = true;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefNum1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Hidden = true;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefNum2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Hidden = true;
				}
				AdjustGridColumnSettings();
				dataGridItems.SetupUI();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void ExcludeAdditionalColumns()
		{
			if (string.IsNullOrEmpty(CompanyPreferences.RefDate1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefDate2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefText1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefText2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefSlNo))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefNum1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefNum2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
		}

		private void AdjustGridColumnSettings()
		{
			dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.Cursor = Cursors.Hand;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			comboBoxCustomer.Focus();
		}

		public void LoadData(string voucherID, bool isRevised)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					int num = 0;
					if (IsRevised && comboBoxRevisionNo.Text != "")
					{
						num = int.Parse(comboBoxRevisionNo.Text.ToString());
						currentData = Factory.SalesQuoteSystem.GetSalesQuoteByRevID(SystemDocID, voucherID, num);
					}
					else if (!IsRevised)
					{
						currentData = Factory.SalesQuoteSystem.GetSalesQuoteByID(SystemDocID, voucherID);
					}
					if (currentData != null)
					{
						FillData();
						LoadCustomerPriceList();
						IsNewRecord = false;
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
					DataRow dataRow = currentData.Tables["Sales_Quote"].Rows[0];
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
					comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
					comboBoxBillingAddress.SelectedID = dataRow["BillingAddressID"].ToString();
					textBoxShipto.Text = dataRow["ShipToAddress"].ToString();
					textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
					comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
					comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
					comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
					if (dataRow["PriceIncludeTax"] != DBNull.Value && CompanyPreferences.IsTax)
					{
						checkBoxPriceIncludeTax.Checked = bool.Parse(dataRow["PriceIncludeTax"].ToString());
					}
					else
					{
						checkBoxPriceIncludeTax.Checked = false;
					}
					comboBoxJob.SelectedID = dataRow["JobID"].ToString();
					comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
					textBoxPONumber.Text = dataRow["PONumber"].ToString();
					textBoxDiscountAmount.Text = decimal.Parse(dataRow["Discount"].ToString()).ToString(Format.TotalAmountFormat);
					if (dataRow["TaxAmount"] != DBNull.Value)
					{
						textBoxTaxAmount.Text = decimal.Parse(dataRow["TaxAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTaxAmount.Text = decimal.Parse("0").ToString(Format.TotalAmountFormat);
					}
					if (dataRow["Discount"] != DBNull.Value)
					{
						textBoxDiscountAmount.Text = decimal.Parse(dataRow["Discount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxDiscountAmount.Text = decimal.Parse("0").ToString(Format.TotalAmountFormat);
					}
					textBoxTotal.Text = decimal.Parse(dataRow["Total"].ToString()).ToString(Format.TotalAmountFormat);
					if (dataRow["ExpiryDate"] != DBNull.Value)
					{
						dateTimePickerExpiry.Value = DateTime.Parse(dataRow["ExpiryDate"].ToString());
					}
					else
					{
						dateTimePickerExpiry.Value = DateTime.Now;
						dateTimePickerExpiry.Checked = false;
					}
					if (!dataRow["Status"].IsDBNullOrEmpty())
					{
						comboBoxStatus.SelectedIndex = checked(int.Parse(dataRow["Status"].ToString()) - 1);
					}
					else
					{
						comboBoxStatus.SelectedIndex = 0;
					}
					if (dataRow["RoundOff"] != DBNull.Value)
					{
						textBoxRoundOff.Text = decimal.Parse(dataRow["RoundOff"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (dataRow["LastRevisedDate"] != DBNull.Value)
					{
						textBoxLastReviseDate.Text = DateTime.Parse(dataRow["LastRevisedDate"].ToString()).ToShortDateString();
					}
					else
					{
						textBoxLastReviseDate.Clear();
					}
					if (currentData.Tables["Sales_Quote"].Columns.Contains("Remarks1"))
					{
						if (!string.IsNullOrEmpty(dataRow["Remarks1"].ToString()))
						{
							textBoxRemarks1.Text = dataRow["Remarks1"].ToString();
						}
						else
						{
							textBoxRemarks1.Text = "";
						}
					}
					if (currentData.Tables["Sales_Quote"].Columns.Contains("Remarks2"))
					{
						if (!string.IsNullOrEmpty(dataRow["Remarks2"].ToString()))
						{
							textBoxRemarks2.Text = dataRow["Remarks2"].ToString();
						}
						else
						{
							textBoxRemarks2.Text = "";
						}
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Sales_Quote_Detail") && currentData.SalesQuoteDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Sales_Quote_Detail"].Rows)
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
							dataRow3["RefSlNo"] = row["RefSlNo"];
							dataRow3["RefText1"] = row["RefText1"];
							dataRow3["RefText2"] = row["RefText2"];
							dataRow3["RefNum1"] = row["RefNum1"];
							dataRow3["RefNum2"] = row["RefNum2"];
							dataRow3["RefDate1"] = row["RefDate1"];
							dataRow3["RefDate2"] = row["RefDate2"];
							dataRow3["MatrixParentID"] = row["MatrixParentID"];
							dataRow3["Job"] = row["JobID"];
							dataRow3["CostCategory"] = row["CostCategoryID"];
							dataRow3["Description"] = row["Description"];
							dataRow3["Remarks"] = row["Remarks"];
							dataRow3["Unit"] = row["UnitID"];
							if (!string.IsNullOrEmpty(row["ItemType"].ToString()))
							{
								dataRow3["ItemType"] = row["ItemType"];
							}
							else
							{
								object obj = Factory.DatabaseSystem.GetFieldValue("Product", "ItemType", "ProductID", dataRow3["Item Code"].ToString()).ToString();
								ItemTypes itemTypes = ItemTypes.Inventory;
								if (obj != null)
								{
									itemTypes = (ItemTypes)checked((byte)int.Parse(obj.ToString()));
								}
								dataRow3["ItemType"] = itemTypes;
							}
							dataRow3["TaxOption"] = row["TaxOption"];
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
								dataRow3["Cost"] = decimal.Parse(row["Cost"].ToString()).ToString(Format.UnitPriceFormat);
							}
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal.TryParse(dataRow3["Quantity"].ToString(), out result);
							decimal.TryParse(dataRow3["Price"].ToString(), out result2);
							dataRow3["Amount"] = Math.Round(result * result2, Global.CurDecimalPoints);
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
						checkedListBoxDN.Items.Clear();
						checkedListBoxProcessedDocs.Items.Clear();
						if (currentData.Tables.Contains("Sales_Enquiry_Detail") && currentData.Tables["Sales_Enquiry_Detail"].Rows.Count > 0)
						{
							sourceDocType = ItemSourceTypes.SalesQuote;
							labelSelectedDocs.Visible = true;
							checkedListBoxDN.Visible = true;
							foreach (DataRow row2 in currentData.Tables["Sales_Enquiry_Detail"].Rows)
							{
								NameValue item = new NameValue(row2["VoucherID"].ToString(), row2["SysDocID"].ToString());
								checkedListBoxDN.Items.Add(item);
							}
						}
						if (currentData.Tables.Contains("Sales_Order_Detail") && currentData.Tables["Sales_Order_Detail"].Rows.Count > 0)
						{
							sourceDocType = ItemSourceTypes.SalesOrder;
							labelProcessedDocs.Visible = true;
							checkedListBoxProcessedDocs.Visible = true;
							foreach (DataRow row3 in currentData.Tables["Sales_Order_Detail"].Rows)
							{
								NameValue item2 = new NameValue(row3["VoucherID"].ToString(), row3["SysDocID"].ToString());
								checkedListBoxProcessedDocs.Items.Add(item2);
							}
						}
						if (currentData.Tables.Contains("Delivery_Note_Detail") && currentData.Tables["Delivery_Note_Detail"].Rows.Count > 0)
						{
							sourceDocType = ItemSourceTypes.DeliveryNote;
							labelProcessedDocs.Visible = true;
							checkedListBoxProcessedDocs.Visible = true;
							foreach (DataRow row4 in currentData.Tables["Delivery_Note_Detail"].Rows)
							{
								NameValue item3 = new NameValue(row4["VoucherID"].ToString(), row4["SysDocID"].ToString());
								checkedListBoxProcessedDocs.Items.Add(item3);
							}
						}
						if (currentData.Tables.Contains("Revision") && currentData.Tables["Revision"].Rows.Count > 0 && !IsRevised)
						{
							panelReviseDate.Visible = true;
							panelReviseDate.Enabled = false;
							mmTextBoxRevNo.Text = currentData.Tables["Revision"].Rows[0][0].ToString();
							mmTextBoxRevNo.Visible = true;
							mmTextBoxRevNo.ReadOnly = true;
							if (currentData.Tables["Sales_Quote"].Rows[0]["LastRevisedDate"] != DBNull.Value)
							{
								textBoxLastReviseDate.Text = DateTime.Parse(currentData.Tables["Sales_Quote"].Rows[0]["LastRevisedDate"].ToString()).ToShortDateString();
							}
						}
						checked
						{
							foreach (UltraGridRow row5 in dataGridItems.Rows)
							{
								if (row5.Cells.Contains("ItemType"))
								{
									if ((byte)int.Parse(row5.Cells["ItemType"].Value.ToString()) == 4)
									{
										row5.Cells["Quantity"].Activation = Activation.Disabled;
									}
								}
								else
								{
									object obj2 = Factory.DatabaseSystem.GetFieldValue("Product", "ItemType", "ProductID", row5.Cells["Item Code"].Value.ToString()).ToString();
									if (obj2 != null)
									{
										_ = (byte)int.Parse(obj2.ToString());
									}
								}
								string productID = row5.Cells["Item Code"].Value.ToString();
								row5.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
								DataRow[] array = currentData.TaxDetailsTable.Select("RowIndex = " + row5.Index + " AND TaxLevel = " + (byte)2);
								if (array.Length != 0)
								{
									TaxTransactionData taxTransactionData = new TaxTransactionData();
									taxTransactionData.Merge(array);
									row5.Cells["Tax"].Tag = taxTransactionData;
								}
							}
							DataRow[] array2 = currentData.TaxDetailsTable.Select("RowIndex = -1 AND TaxLevel = " + (byte)1);
							if (array2.Length != 0)
							{
								TaxTransactionData taxTransactionData2 = new TaxTransactionData();
								taxTransactionData2.Merge(array2);
								textBoxTaxAmount.Tag = taxTransactionData2;
							}
							CalculateTotal();
							if (comboBoxStatus.SelectedIndex == 3)
							{
								allowEdit = false;
							}
							else
							{
								allowEdit = true;
							}
							if (checkedListBoxDN.Items.Count == 0)
							{
								labelSelectedDocs.Visible = false;
								checkedListBoxDN.Visible = false;
							}
							if (checkedListBoxProcessedDocs.Items.Count == 0)
							{
								labelProcessedDocs.Visible = false;
								checkedListBoxProcessedDocs.Visible = false;
							}
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
				bool flag2 = Factory.SalesQuoteSystem.CreateSalesQuote(currentData, !isNewRecord, IsRevised);
				RefVoucherID = null;
				RefDocID = null;
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
			formManager.ShowApprovalPanel(approvalTaskID, "Sales_Quote", "VoucherID");
		}

		private bool ValidateData()
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
			if (comboBoxStatus.SelectedIndex == 3)
			{
				allowEdit = false;
			}
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Sales_Quote", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (RestrictTransaction)
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
				return false;
			}
			if (!IsNewRecord)
			{
				DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(currentData, SysDocTypes.SalesQuote, Global.CurrentUser, includeApproveUser: false);
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
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxCustomer.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				showednotdiscount = false;
				ValidateAllowedDiscount();
				if (showednotdiscount)
				{
					ErrorHelper.WarningMessage("Higher Discount is not applicable.", "Please enter a proper discount.");
					return false;
				}
				decimal result = -1m;
				if (!decimal.TryParse(textBoxTotal.Text, out result) || result < 0m)
				{
					ErrorHelper.InformationMessage("Cannot save an invoice with negative total.");
					return false;
				}
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
					}
					else if (dataGridItems.Rows[i].Cells["Item Code"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select an item.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
					UltraGridRow ultraGridRow = dataGridItems.Rows[i];
					decimal d = decimal.Parse(ultraGridRow.Cells["Quantity"].Value.ToString());
					decimal d2 = decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString());
					decimal d3 = decimal.Parse(ultraGridRow.Cells["Amount"].Value.ToString());
					if (Math.Abs(Math.Round(d * (d2 + default(decimal)), Global.CurDecimalPoints) - d3) > 0.1m)
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
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Sales_Quote", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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

		private void ClearForm()
		{
			try
			{
				allowEdit = true;
				textBoxNote.Clear();
				textBoxRef1.Clear();
				textBoxShipto.Clear();
				comboBoxBillingAddress.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				dateTimePickerExpiry.Value = DateTime.Now;
				dateTimePickerExpiry.Checked = false;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				textBoxCustomerName.Clear();
				comboBoxCustomer.Clear();
				textBoxPONumber.Clear();
				comboBoxSalesperson.Clear();
				comboBoxShippingAddressID.Clear();
				comboBoxShippingMethod.Clear();
				comboBoxJob.Clear();
				if (useJobCosting)
				{
					comboBoxJob.Filter("");
				}
				comboBoxCostCategory.Clear();
				comboBoxTerm.Clear();
				comboBoxPayeeTaxGroup.Clear();
				comboBoxRevisionNo.DataSource = null;
				textBoxSubtotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxBilltoAddress.Clear();
				checkedListBoxDN.Items.Clear();
				labelSelectedDocs.Visible = false;
				checkedListBoxDN.Visible = false;
				checkedListBoxProcessedDocs.Items.Clear();
				labelProcessedDocs.Visible = false;
				checkedListBoxProcessedDocs.Visible = false;
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscountAmount.Text = "0.00";
				textBoxDiscountPercent.Text = "0";
				isDiscountPercent = false;
				isDiscountPercent = false;
				textBoxRoundOff.Text = 0.ToString(Format.TotalAmountFormat);
				comboBoxStatus.SelectedIndex = 1;
				textBoxLastReviseDate.Clear();
				ReviseToolStripMenuItem.Checked = false;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				comboBoxSalesperson.SelectedID = Security.DefaultSalespersonID;
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				SetApprovalStatus();
				IsVoid = false;
				IsRevised = false;
				formManager.ResetDirty();
				comboBoxCustomer.Focus();
				textBoxRemarks1.Clear();
				textBoxRemarks2.Clear();
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
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.SalesQuoteSystem.DeleteSalesQuote(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Sales_Quote", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID, IsRevised);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Sales_Quote", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID, IsRevised);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Sales_Quote", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID, IsRevised);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Sales_Quote", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID, IsRevised);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Sales_Quote", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
					if (text != "")
					{
						LoadData(text, IsRevised);
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
			checked
			{
				try
				{
					SetupGrid();
					if (!CompanyPreferences.IsTax)
					{
						panelNonTax.Top -= 22;
					}
					UltraFormattedLinkLabel ultraFormattedLinkLabel = labelCurrency;
					bool visible = comboBoxCurrency.Visible = CompanyPreferences.UseMultiCurrency;
					ultraFormattedLinkLabel.Visible = visible;
					checkBoxPriceIncludeTax.Visible = false;
					labelTaxGroup.Visible = CompanyPreferences.IsTax;
					comboBoxPayeeTaxGroup.Visible = CompanyPreferences.IsTax;
					comboBoxSysDoc.FilterByType(SysDocTypes.SalesQuote);
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
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
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
			comboBoxPayeeTaxGroup.ReadOnly = !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
			checkBoxPriceIncludeTax.Enabled = Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
			TotalDiscountPercAllowed = Security.AllowedDiscount(GeneralSecurityRoles.GiveDiscount);
			priceDiscountPercAllowed = Security.AllowedDiscount(GeneralSecurityRoles.AllowPriceDiscount);
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
			if (Void(isVoid: true))
			{
				IsVoid = true;
			}
			else
			{
				ErrorHelper.ErrorMessage("Unable to void the transaction.");
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				DialogResult dialogResult = (!isVoid) ? ErrorHelper.QuestionMessageYesNo(UIMessages.WantToUnvoid) : ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid);
				if (dialogResult == DialogResult.No)
				{
					return false;
				}
				if (!allowEdit)
				{
					ErrorHelper.InformationMessage(Application.ProductName, "You cannot edit this transfer transaction because it is already accepted or rejected.", "Document is in use.");
					return false;
				}
				return Factory.SalesQuoteSystem.VoidSalesQuote(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.SalesQuote);
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
			decimal result3 = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal result4 = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				decimal result5 = default(decimal);
				if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result5);
					num += result5;
					if (!row.Cells["Tax"].Value.IsNullOrEmpty())
					{
						decimal.TryParse(row.Cells["Tax"].Value.ToString(), out result4);
						row.Cells["TaxTotal"].Value = result5 + result4;
					}
					if (!row.Cells["Tax"].Value.IsNullOrEmpty())
					{
						result3 += decimal.Parse(row.Cells["Tax"].Value.ToString());
					}
				}
			}
			textBoxSubtotal.Text = num.ToString(Format.TotalAmountFormat);
			textBoxTaxAmount.Text = result3.ToString(Format.TotalAmountFormat);
			decimal.TryParse(textBoxDiscountPercent.Text, out result2);
			decimal.TryParse(textBoxDiscountAmount.Text, out result);
			decimal.TryParse(textBoxTaxAmount.Text, out result3);
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
			num3 = num - result;
			if (isTaxPercent && num2 != 0m)
			{
				result3 = Math.Round(num3 * num2 / 100m, Global.CurDecimalPoints);
			}
			else if (num > 0m && textBoxTaxAmount.Text != "")
			{
				_ = (textBoxTaxAmount.Text != "0.00");
			}
			roundoffamount = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
			num3 += roundoffamount;
			if (!checkBoxPriceIncludeTax.Checked)
			{
				num3 += result3;
			}
			textBoxTotal.Text = num3.ToString(Format.TotalAmountFormat);
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
			}
		}

		private void textBoxDiscountAmount_TextChanged(object sender, EventArgs e)
		{
			if (textBoxDiscountAmount.Focused)
			{
				isDiscountPercent = false;
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
				if (!IsDiscPercentage)
				{
					d = Math.Round(num2 / num * 100m, Global.CurDecimalPoints);
					textBoxDiscountPercent.Text = d.ToString();
				}
				else if (textBoxDiscountPercent.Text != "")
				{
					decimal d2 = decimal.Parse(textBoxDiscountPercent.Text, NumberStyles.Any);
					textBoxDiscountAmount.Text = (num * d2 / 100m).ToString();
					IsDiscPercentage = false;
				}
			}
			else
			{
				textBoxDiscountPercent.Text = "0";
			}
			if (TotalDiscountPercAllowed < d && !showednotdiscount && TotalDiscountPercAllowed > 0m && !Global.IsUserAdmin)
			{
				showednotdiscount = true;
				ErrorHelper.WarningMessage("Higher Discount is not applicable.", "Please enter a proper discount.");
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
			num4 = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
			num = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num5 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			roundoffamount = decimal.Parse(textBoxRoundOff.Text, NumberStyles.Any);
			num2 = num + num5 - num4;
			num2 = decimal.Parse(textBoxDiscountAmount.Text, NumberStyles.Any);
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
			if (!(currentCustomerAddressID == ""))
			{
				new FormHelper().EditCustomerAddress(comboBoxCustomer.SelectedID, currentCustomerAddressID);
			}
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
					LoadData(text, IsRevised);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				allowEdit = true;
				comboBoxStatus.SelectedIndex = 1;
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			duplicateToolStripMenuItem.Enabled = !IsNewRecord;
			ReviseToolStripMenuItem.Enabled = !IsNewRecord;
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
					DataSet dataSet = null;
					string text2 = "";
					if (!IsRevised)
					{
						dataSet = Factory.SalesQuoteSystem.GetSalesQuoteToPrint(selectedID, text);
						text2 = "Sales Quote";
					}
					else
					{
						dataSet = Factory.SalesQuoteSystem.GetSalesQuoteRevToPrint(selectedID, text, int.Parse(comboBoxRevisionNo.SelectedValue.ToString()));
						text2 = "Sales Quote Revised";
					}
					if (dataSet == null || dataSet.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						string printTemplateName = comboBoxSysDoc.GetPrintTemplateName();
						if (!string.IsNullOrEmpty(printTemplateName))
						{
							PrintHelper.PrintDocument(dataSet, selectedID, printTemplateName, SysDocTypes.SalesQuote, isPrint, showPrintDialog);
						}
						else
						{
							PrintHelper.PrintDocument(dataSet, selectedID, text2, SysDocTypes.SalesQuote, isPrint, showPrintDialog);
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
			LoadData(voucherID, IsRevised);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.SelectedSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.BringFormToFront(FormActivator.SalesQuoteListFormObj);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void saveDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (GetData())
				{
					EnterNameDialog enterNameDialog = new EnterNameDialog();
					enterNameDialog.EnteredName = SystemDocID + "-" + Global.CurrentUser + "-" + DateTime.Now;
					if (enterNameDialog.ShowDialog() == DialogResult.OK)
					{
						Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.SalesQuote);
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private bool LoadDraft()
		{
			try
			{
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 22.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.SalesQuote);
					currentData = (dataSet as SalesQuoteData);
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
			LoadDraft();
		}

		private void itemSortToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UltraGridBand ultraGridBand = dataGridItems.DisplayLayout.Bands[0];
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].SortIndicator = SortIndicator.Ascending;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].SortIndicator = SortIndicator.Descending;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
			ultraGridBand.SortedColumns.Clear();
		}

		private void LoadRevsionCombo()
		{
			DataSet dataSet = new DataSet();
			dataSet = Factory.SalesQuoteSystem.GetLoadRevisionCombo(SystemDocID, textBoxVoucherNumber.Text);
			if (!isloadrev)
			{
				isloadrev = true;
				comboBoxRevisionNo.DataSource = dataSet.Tables[0];
				comboBoxRevisionNo.DisplayMember = "RevisionNo";
				comboBoxRevisionNo.ValueMember = "RevisionNoInt";
				isloadrev = false;
			}
		}

		private void ReviseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem dropDownItem in ReviseToolStripMenuItem.DropDownItems)
			{
				dropDownItem.Checked = false;
			}
			if (!IsRevised)
			{
				if (ErrorHelper.QuestionMessageYesNo("Are you sure to revise this document?") != DialogResult.Yes)
				{
					return;
				}
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (IsRevised)
			{
				IsRevised = false;
				((ToolStripMenuItem)sender).Checked = false;
				return;
			}
			IsRevised = true;
			IsRevised = true;
			if (IsRevised)
			{
				LoadRevsionCombo();
			}
		}

		private void comboBoxRevisionNo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isloadrev && comboBoxRevisionNo.SelectedValue != null)
			{
				LoadData(textBoxVoucherNumber.Text, IsRevised);
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

		private void ultraFormattedLinkCostCategory_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCostCategory(comboBoxCostCategory.SelectedID);
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditShippingMethod(comboBoxShippingMethod.SelectedID);
		}

		private void ultraFormattedLinkCurrency_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(comboBoxCurrency.SelectedID);
		}

		private void labelJob_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJob(comboBoxJob.SelectedID);
		}

		private void ultraFormattedLinkPayment_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPaymentTerm(comboBoxTerm.SelectedID);
		}

		private void createFromSalesEnquiryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet openOrdersSummary = Factory.SalesEnquirySystem.GetOpenOrdersSummary(comboBoxCustomer.SelectedID, isExport: false);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openOrdersSummary;
			selectDocumentDialog.Text = "Select Sales Enquiry";
			selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["Job"].Hidden = !useJobCosting;
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			RefDocID = text;
			RefVoucherID = text2;
			TimestampStatus = Factory.SystemDocumentSystem.CheckStimeStampStatus("Sales_Enquiry", RefDocID, RefVoucherID);
			RefDateTime = DateTime.Parse(TimestampStatus.Tables[0].Rows[0]["TimeStamp"].ToString());
			SalesEnquiryData salesOrderByID = Factory.SalesEnquirySystem.GetSalesOrderByID(text, text2);
			DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(salesOrderByID, SysDocTypes.SalesEnquiry, Global.CurrentUser, includeApproveUser: true);
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
			DataRow dataRow = salesOrderByID.SalesEnquiryTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			if (comboBoxCustomer.SelectedID == "")
			{
				comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
			}
			textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
			comboBoxCustomer.Enabled = false;
			sourceDocType = ItemSourceTypes.SalesOrder;
			if (!string.IsNullOrEmpty(dataRow["TermID"].ToString()))
			{
				comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
			}
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
			if (salesOrderByID.Tables.Contains("Sales_Enquiry_Detail") && salesOrderByID.SalesEnquiryDetailTable.Rows.Count != 0)
			{
				foreach (DataRow row in salesOrderByID.Tables["Sales_Enquiry_Detail"].Rows)
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
					dataRow3["Attribute1"] = row["Attribute1"];
					dataRow3["Attribute2"] = row["Attribute2"];
					dataRow3["Attribute3"] = row["Attribute3"];
					dataRow3["MatrixParentID"] = row["MatrixParentID"];
					dataRow3["Description"] = row["Description"];
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
					num2 -= num3;
					dataRow3["Quantity"] = num2;
					dataRow3["Amount"] = Math.Round(num2 * d, Global.CurDecimalPoints);
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
			}
		}

		private void toolStripButtonMultiPreview_Click(object sender, EventArgs e)
		{
			AttachementDetailsForm attachementDetailsForm = new AttachementDetailsForm();
			attachementDetailsForm.SysDocID = comboBoxSysDoc.SelectedID;
			attachementDetailsForm.VoucherID = textBoxVoucherNumber.Text;
			attachementDetailsForm.SysDocType = SysDocTypes.SalesQuote;
			attachementDetailsForm.FormType = "Transaction";
			attachementDetailsForm.Show();
		}

		private void labelTaxGroup_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxPayeeTaxGroup.SelectedID);
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

		private void textBoxDiscountAmount_Leave(object sender, EventArgs e)
		{
			showednotdiscount = false;
			ValidateAllowedDiscount();
		}

		private void textBoxDiscountPercent_Leave(object sender, EventArgs e)
		{
			showednotdiscount = false;
			IsDiscPercentage = true;
			ValidateAllowedDiscount();
		}

		private void textBoxRoundOff_Leave(object sender, EventArgs e)
		{
			CalculateTotal();
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

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void toolStripButtonExcelImport_Click(object sender, EventArgs e)
		{
			dataGridItems.ImportFromExcel(autoFill: true);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["Item Code"].Value.ToString() != "")
				{
					string text = row.Cells["Item Code"].Value.ToString();
					if (Factory.DatabaseSystem.ExistFieldValue("Product", "ProductID", text))
					{
						string value = Factory.DatabaseSystem.GetFieldValue("Product", "Description", "ProductID", text).ToString() ?? null;
						row.Cells["Description"].Value = value;
						string value2 = Factory.DatabaseSystem.GetFieldValue("Product", "UnitID", "ProductID", text).ToString() ?? null;
						row.Cells["Unit"].Value = value2;
						decimal d = decimal.Parse(row.Cells["Quantity"].Value.ToString());
						decimal d2 = decimal.Parse(row.Cells["Price"].Value.ToString());
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
						row.Cells["Amount"].Value = (d * d2).ToString(Format.TotalAmountFormat);
						if (row.Cells["Amount"].Value.ToString() != "")
						{
							CalculateTotal();
						}
						if (dataGridItems.DisplayLayout.Bands[0].Columns.Contains("LotNumber"))
						{
							dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].Hidden = true;
							dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Hidden = true;
							dataGridItems.DisplayLayout.Bands[0].Columns["SourceLotNumber"].Hidden = true;
							dataGridItems.DisplayLayout.Bands[0].Columns["SoldQty"].Hidden = true;
							dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Hidden = true;
							dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].Hidden = true;
							dataGridItems.DisplayLayout.Bands[0].Columns["Reference2"].Hidden = true;
							dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].Hidden = true;
							dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].Hidden = true;
							dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Hidden = true;
						}
					}
				}
			}
			numberTextBox1_TextChanged(sender, e);
		}

		private void checkedListBoxDN_DoubleClick(object sender, EventArgs e)
		{
			NameValue nameValue = checkedListBoxDN.SelectedItem as NameValue;
			if (nameValue != null)
			{
				new FormHelper().EditTransaction(TransactionListType.SalesEnquiry, nameValue.ID, nameValue.Name);
			}
		}

		private void checkedListBoxProcessedDocs_DoubleClick(object sender, EventArgs e)
		{
			if (checkedListBoxProcessedDocs.SelectedItem.ToString() != "")
			{
				NameValue nameValue = checkedListBoxProcessedDocs.SelectedItem as NameValue;
				FormHelper formHelper = new FormHelper();
				if (sourceDocType == ItemSourceTypes.DeliveryNote)
				{
					formHelper.EditTransaction(nameValue.ID, nameValue.Name);
				}
				else if (sourceDocType == ItemSourceTypes.SalesOrder)
				{
					formHelper.EditTransaction(nameValue.ID, nameValue.Name);
				}
			}
		}

		private void toolStripMenuRelationshipMap_Click(object sender, EventArgs e)
		{
			RelationshipMapForm relationshipMapForm = new RelationshipMapForm();
			relationshipMapForm.sysDocID = comboBoxSysDoc.SelectedID;
			relationshipMapForm.voucherID = textBoxVoucherNumber.Text;
			relationshipMapForm.Show();
		}

		private void checkBoxPriceIncludeTax_CheckedChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				CalculateAllRowsTaxes();
				CalculateTotal();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.SalesQuoteForm));
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
			toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMultiPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonExcelImport = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ReviseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuRelationshipMap = new System.Windows.Forms.ToolStripMenuItem();
			createFromSalesEnquiryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxRemarks2 = new Micromind.UISupport.MMTextBox();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			textBoxRemarks1 = new Micromind.UISupport.MMTextBox();
			labelTaxGroup = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelJob = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPayeeTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			ultraFormattedLinkPayment = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelcostcategory = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelReviseDate = new Micromind.UISupport.NonDirtyPanel(components);
			comboBoxRevisionNo = new System.Windows.Forms.ComboBox();
			label13 = new System.Windows.Forms.Label();
			textBoxLastReviseDate = new Micromind.UISupport.MMTextBox();
			label14 = new System.Windows.Forms.Label();
			textBoxShipto = new Micromind.UISupport.MMTextBox();
			comboBoxBillingAddress = new Micromind.DataControls.CustomerAddressComboBox();
			comboBoxShippingAddressID = new Micromind.DataControls.CustomerAddressComboBox();
			textBoxBilltoAddress = new Micromind.UISupport.MMTextBox();
			comboBoxCustomer = new Micromind.DataControls.CustomerLeadsComboBox();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimePickerExpiry = new System.Windows.Forms.DateTimePicker();
			label4 = new System.Windows.Forms.Label();
			comboBoxStatus = new System.Windows.Forms.ComboBox();
			comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
			comboBoxShippingMethod = new Micromind.DataControls.ShippingMethodsComboBox();
			comboBoxTerm = new Micromind.DataControls.PaymentTermComboBox();
			comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label2 = new System.Windows.Forms.Label();
			textBoxPONumber = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			labelVoided = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			textBoxDiscountPercent = new Micromind.UISupport.PercentTextBox();
			textBoxDiscountAmount = new Micromind.UISupport.AmountTextBox();
			panelNonTax = new System.Windows.Forms.Panel();
			label9 = new System.Windows.Forms.Label();
			textBoxRoundOff = new Micromind.UISupport.AmountTextBox();
			linkLabelTax = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
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
			itemSortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			checkBoxPriceIncludeTax = new System.Windows.Forms.CheckBox();
			labelSelectedDocs = new System.Windows.Forms.Label();
			checkedListBoxDN = new System.Windows.Forms.ListBox();
			labelProcessedDocs = new System.Windows.Forms.Label();
			checkedListBoxProcessedDocs = new System.Windows.Forms.ListBox();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			mmTextBoxRevNo = new Micromind.UISupport.MMTextBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).BeginInit();
			panelReviseDate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBillingAddress).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingAddressID).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTerm).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			panel1.SuspendLayout();
			panelNonTax.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[23]
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
				toolStripSeparator8,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonMultiPreview,
				toolStripSeparator6,
				toolStripButtonExcelImport,
				toolStripButtonInformation,
				toolStripSeparator4,
				toolStripDropDownButton1,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(932, 31);
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
			toolStripSeparator8.Name = "toolStripSeparator8";
			toolStripSeparator8.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7]
			{
				duplicateToolStripMenuItem,
				ReviseToolStripMenuItem,
				saveDraftToolStripMenuItem,
				loadDraftToolStripMenuItem,
				toolStripSeparator7,
				toolStripMenuRelationshipMap,
				createFromSalesEnquiryToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			duplicateToolStripMenuItem.Text = "Duplicate";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			ReviseToolStripMenuItem.Name = "ReviseToolStripMenuItem";
			ReviseToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			ReviseToolStripMenuItem.Text = "Revise";
			ReviseToolStripMenuItem.Click += new System.EventHandler(ReviseToolStripMenuItem_Click);
			saveDraftToolStripMenuItem.Name = "saveDraftToolStripMenuItem";
			saveDraftToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			saveDraftToolStripMenuItem.Text = "Save as Draft";
			saveDraftToolStripMenuItem.Click += new System.EventHandler(saveDraftToolStripMenuItem_Click);
			loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
			loadDraftToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			loadDraftToolStripMenuItem.Text = "Load Draft...";
			loadDraftToolStripMenuItem.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(208, 6);
			toolStripMenuRelationshipMap.Name = "toolStripMenuRelationshipMap";
			toolStripMenuRelationshipMap.Size = new System.Drawing.Size(211, 22);
			toolStripMenuRelationshipMap.Text = "RelationshipMap";
			toolStripMenuRelationshipMap.Click += new System.EventHandler(toolStripMenuRelationshipMap_Click);
			createFromSalesEnquiryToolStripMenuItem.Name = "createFromSalesEnquiryToolStripMenuItem";
			createFromSalesEnquiryToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			createFromSalesEnquiryToolStripMenuItem.Text = "Create From Sales Enquiry";
			createFromSalesEnquiryToolStripMenuItem.Click += new System.EventHandler(createFromSalesEnquiryToolStripMenuItem_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 572);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(932, 40);
			panelButtons.TabIndex = 4;
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
			linePanelDown.Size = new System.Drawing.Size(932, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(822, 8);
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
			dateTimePickerDate.Location = new System.Drawing.Point(769, 3);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDate.TabIndex = 15;
			textBoxVoucherNumber.Location = new System.Drawing.Point(326, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(177, 20);
			textBoxVoucherNumber.TabIndex = 3;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(686, 28);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(769, 24);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(133, 20);
			textBoxRef1.TabIndex = 16;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(48, 479);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(311, 79);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 478);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(214, 4);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(mmLabel3);
			panelDetails.Controls.Add(textBoxRemarks2);
			panelDetails.Controls.Add(mmLabel39);
			panelDetails.Controls.Add(textBoxRemarks1);
			panelDetails.Controls.Add(labelTaxGroup);
			panelDetails.Controls.Add(labelJob);
			panelDetails.Controls.Add(comboBoxPayeeTaxGroup);
			panelDetails.Controls.Add(ultraFormattedLinkPayment);
			panelDetails.Controls.Add(ultraFormattedLinkLabel7);
			panelDetails.Controls.Add(labelcostcategory);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(panelReviseDate);
			panelDetails.Controls.Add(textBoxShipto);
			panelDetails.Controls.Add(comboBoxBillingAddress);
			panelDetails.Controls.Add(comboBoxShippingAddressID);
			panelDetails.Controls.Add(textBoxBilltoAddress);
			panelDetails.Controls.Add(comboBoxCustomer);
			panelDetails.Controls.Add(comboBoxCostCategory);
			panelDetails.Controls.Add(comboBoxJob);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(dateTimePickerExpiry);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(comboBoxStatus);
			panelDetails.Controls.Add(comboBoxCurrency);
			panelDetails.Controls.Add(comboBoxShippingMethod);
			panelDetails.Controls.Add(comboBoxTerm);
			panelDetails.Controls.Add(comboBoxSalesperson);
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxPONumber);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxCustomerName);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 33);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(917, 212);
			panelDetails.TabIndex = 0;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(424, 172);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(58, 13);
			mmLabel3.TabIndex = 211;
			mmLabel3.Text = "Remarks2:";
			textBoxRemarks2.BackColor = System.Drawing.Color.White;
			textBoxRemarks2.CustomReportFieldName = "";
			textBoxRemarks2.CustomReportKey = "";
			textBoxRemarks2.CustomReportValueType = 1;
			textBoxRemarks2.IsComboTextBox = false;
			textBoxRemarks2.IsModified = false;
			textBoxRemarks2.Location = new System.Drawing.Point(493, 172);
			textBoxRemarks2.MaxLength = 255;
			textBoxRemarks2.Multiline = true;
			textBoxRemarks2.Name = "textBoxRemarks2";
			textBoxRemarks2.Size = new System.Drawing.Size(315, 30);
			textBoxRemarks2.TabIndex = 24;
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(10, 172);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(58, 13);
			mmLabel39.TabIndex = 210;
			mmLabel39.Text = "Remarks1:";
			textBoxRemarks1.BackColor = System.Drawing.Color.White;
			textBoxRemarks1.CustomReportFieldName = "";
			textBoxRemarks1.CustomReportKey = "";
			textBoxRemarks1.CustomReportValueType = 1;
			textBoxRemarks1.IsComboTextBox = false;
			textBoxRemarks1.IsModified = false;
			textBoxRemarks1.Location = new System.Drawing.Point(101, 172);
			textBoxRemarks1.MaxLength = 255;
			textBoxRemarks1.Multiline = true;
			textBoxRemarks1.Name = "textBoxRemarks1";
			textBoxRemarks1.Size = new System.Drawing.Size(315, 30);
			textBoxRemarks1.TabIndex = 23;
			appearance3.FontData.BoldAsString = "False";
			appearance3.FontData.Name = "Tahoma";
			labelTaxGroup.Appearance = appearance3;
			labelTaxGroup.AutoSize = true;
			labelTaxGroup.Location = new System.Drawing.Point(451, 116);
			labelTaxGroup.Name = "labelTaxGroup";
			labelTaxGroup.Size = new System.Drawing.Size(59, 15);
			labelTaxGroup.TabIndex = 162;
			labelTaxGroup.TabStop = true;
			labelTaxGroup.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelTaxGroup.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelTaxGroup.Value = "Tax Group:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			labelTaxGroup.VisitedLinkAppearance = appearance4;
			labelTaxGroup.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelTaxGroup_LinkClicked);
			appearance5.FontData.BoldAsString = "False";
			appearance5.FontData.Name = "Tahoma";
			labelJob.Appearance = appearance5;
			labelJob.AutoSize = true;
			labelJob.Location = new System.Drawing.Point(234, 144);
			labelJob.Name = "labelJob";
			labelJob.Size = new System.Drawing.Size(42, 15);
			labelJob.TabIndex = 167;
			labelJob.TabStop = true;
			labelJob.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelJob.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelJob.Value = "Project:";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			labelJob.VisitedLinkAppearance = appearance6;
			labelJob.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelJob_LinkClicked);
			comboBoxPayeeTaxGroup.Assigned = false;
			comboBoxPayeeTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPayeeTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayeeTaxGroup.CustomReportFieldName = "";
			comboBoxPayeeTaxGroup.CustomReportKey = "";
			comboBoxPayeeTaxGroup.CustomReportValueType = 1;
			comboBoxPayeeTaxGroup.DescriptionTextBox = null;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayeeTaxGroup.DisplayLayout.Appearance = appearance7;
			comboBoxPayeeTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayeeTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance8.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance8.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance8.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance8;
			appearance9.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance9;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance10.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance10.BackColor2 = System.Drawing.SystemColors.Control;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance10;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance11;
			appearance12.BackColor = System.Drawing.SystemColors.Highlight;
			appearance12.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance12;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance13;
			appearance14.BorderColor = System.Drawing.Color.Silver;
			appearance14.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellAppearance = appearance14;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance15.BackColor = System.Drawing.SystemColors.Control;
			appearance15.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance15.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance15.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance15;
			appearance16.TextHAlignAsString = "Left";
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance16;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowAppearance = appearance17;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance18;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayeeTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayeeTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayeeTaxGroup.Editable = true;
			comboBoxPayeeTaxGroup.FilterString = "";
			comboBoxPayeeTaxGroup.HasAllAccount = false;
			comboBoxPayeeTaxGroup.HasCustom = false;
			comboBoxPayeeTaxGroup.IsDataLoaded = false;
			comboBoxPayeeTaxGroup.Location = new System.Drawing.Point(532, 113);
			comboBoxPayeeTaxGroup.MaxDropDownItems = 12;
			comboBoxPayeeTaxGroup.Name = "comboBoxPayeeTaxGroup";
			comboBoxPayeeTaxGroup.ReadOnly = true;
			comboBoxPayeeTaxGroup.ShowInactiveItems = false;
			comboBoxPayeeTaxGroup.ShowQuickAdd = true;
			comboBoxPayeeTaxGroup.Size = new System.Drawing.Size(118, 20);
			comboBoxPayeeTaxGroup.TabIndex = 14;
			comboBoxPayeeTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance19.FontData.BoldAsString = "False";
			appearance19.FontData.Name = "Tahoma";
			ultraFormattedLinkPayment.Appearance = appearance19;
			ultraFormattedLinkPayment.AutoSize = true;
			ultraFormattedLinkPayment.Location = new System.Drawing.Point(451, 49);
			ultraFormattedLinkPayment.Name = "ultraFormattedLinkPayment";
			ultraFormattedLinkPayment.Size = new System.Drawing.Size(79, 15);
			ultraFormattedLinkPayment.TabIndex = 166;
			ultraFormattedLinkPayment.TabStop = true;
			ultraFormattedLinkPayment.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkPayment.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkPayment.Value = "Payment Term:";
			appearance20.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkPayment.VisitedLinkAppearance = appearance20;
			ultraFormattedLinkPayment.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkPayment_LinkClicked);
			appearance21.FontData.BoldAsString = "False";
			appearance21.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance21;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(10, 143);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(90, 15);
			ultraFormattedLinkLabel7.TabIndex = 165;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Shipping Method:";
			appearance22.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance22;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			appearance23.FontData.BoldAsString = "False";
			appearance23.FontData.Name = "Tahoma";
			labelcostcategory.Appearance = appearance23;
			labelcostcategory.AutoSize = true;
			labelcostcategory.Location = new System.Drawing.Point(424, 144);
			labelcostcategory.Name = "labelcostcategory";
			labelcostcategory.Size = new System.Drawing.Size(76, 15);
			labelcostcategory.TabIndex = 138;
			labelcostcategory.TabStop = true;
			labelcostcategory.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelcostcategory.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelcostcategory.Value = "Cost Category:";
			appearance24.ForeColor = System.Drawing.Color.Blue;
			labelcostcategory.VisitedLinkAppearance = appearance24;
			labelcostcategory.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkCostCategory_LinkClicked);
			appearance25.FontData.BoldAsString = "False";
			appearance25.FontData.Name = "Tahoma";
			labelCurrency.Appearance = appearance25;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(689, 90);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 15);
			labelCurrency.TabIndex = 164;
			labelCurrency.TabStop = true;
			labelCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCurrency.Value = "Currency:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			labelCurrency.VisitedLinkAppearance = appearance26;
			labelCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkCurrency_LinkClicked);
			panelReviseDate.Controls.Add(mmTextBoxRevNo);
			panelReviseDate.Controls.Add(comboBoxRevisionNo);
			panelReviseDate.Controls.Add(label13);
			panelReviseDate.Controls.Add(textBoxLastReviseDate);
			panelReviseDate.Controls.Add(label14);
			panelReviseDate.Location = new System.Drawing.Point(652, 115);
			panelReviseDate.Name = "panelReviseDate";
			panelReviseDate.Size = new System.Drawing.Size(262, 50);
			panelReviseDate.TabIndex = 163;
			panelReviseDate.Visible = false;
			comboBoxRevisionNo.FormattingEnabled = true;
			comboBoxRevisionNo.Location = new System.Drawing.Point(117, 25);
			comboBoxRevisionNo.Name = "comboBoxRevisionNo";
			comboBoxRevisionNo.Size = new System.Drawing.Size(87, 21);
			comboBoxRevisionNo.TabIndex = 1;
			comboBoxRevisionNo.SelectedIndexChanged += new System.EventHandler(comboBoxRevisionNo_SelectedIndexChanged);
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(14, 28);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(66, 13);
			label13.TabIndex = 66;
			label13.Text = "Revised No:";
			textBoxLastReviseDate.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLastReviseDate.CustomReportFieldName = "";
			textBoxLastReviseDate.CustomReportKey = "";
			textBoxLastReviseDate.CustomReportValueType = 1;
			textBoxLastReviseDate.ForeColor = System.Drawing.Color.Black;
			textBoxLastReviseDate.IsComboTextBox = false;
			textBoxLastReviseDate.IsModified = false;
			textBoxLastReviseDate.Location = new System.Drawing.Point(117, 3);
			textBoxLastReviseDate.MaxLength = 255;
			textBoxLastReviseDate.Name = "textBoxLastReviseDate";
			textBoxLastReviseDate.ReadOnly = true;
			textBoxLastReviseDate.Size = new System.Drawing.Size(133, 20);
			textBoxLastReviseDate.TabIndex = 0;
			textBoxLastReviseDate.TabStop = false;
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(12, 6);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(98, 13);
			label14.TabIndex = 65;
			label14.Text = "Last Revised Date:";
			textBoxShipto.BackColor = System.Drawing.Color.White;
			textBoxShipto.CustomReportFieldName = "";
			textBoxShipto.CustomReportKey = "";
			textBoxShipto.CustomReportValueType = 1;
			textBoxShipto.IsComboTextBox = false;
			textBoxShipto.IsModified = false;
			textBoxShipto.Location = new System.Drawing.Point(230, 66);
			textBoxShipto.MaxLength = 255;
			textBoxShipto.Multiline = true;
			textBoxShipto.Name = "textBoxShipto";
			textBoxShipto.Size = new System.Drawing.Size(215, 72);
			textBoxShipto.TabIndex = 10;
			comboBoxBillingAddress.AlwaysInEditMode = true;
			comboBoxBillingAddress.Assigned = false;
			comboBoxBillingAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBillingAddress.CustomReportFieldName = "";
			comboBoxBillingAddress.CustomReportKey = "";
			comboBoxBillingAddress.CustomReportValueType = 1;
			comboBoxBillingAddress.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBillingAddress.DisplayLayout.Appearance = appearance27;
			comboBoxBillingAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBillingAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxBillingAddress.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBillingAddress.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBillingAddress.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBillingAddress.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxBillingAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBillingAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBillingAddress.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxBillingAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBillingAddress.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxBillingAddress.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxBillingAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBillingAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxBillingAddress.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxBillingAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBillingAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
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
			comboBoxBillingAddress.TabIndex = 7;
			comboBoxBillingAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxShippingAddressID.AlwaysInEditMode = true;
			comboBoxShippingAddressID.Assigned = false;
			comboBoxShippingAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingAddressID.CustomReportFieldName = "";
			comboBoxShippingAddressID.CustomReportKey = "";
			comboBoxShippingAddressID.CustomReportValueType = 1;
			comboBoxShippingAddressID.DescriptionTextBox = null;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingAddressID.DisplayLayout.Appearance = appearance39;
			comboBoxShippingAddressID.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingAddressID.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.Appearance = appearance40;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.BandLabelAppearance = appearance41;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance42.BackColor2 = System.Drawing.SystemColors.Control;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.PromptAppearance = appearance42;
			comboBoxShippingAddressID.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingAddressID.DisplayLayout.MaxRowScrollRegions = 1;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingAddressID.DisplayLayout.Override.ActiveCellAppearance = appearance43;
			appearance44.BackColor = System.Drawing.SystemColors.Highlight;
			appearance44.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingAddressID.DisplayLayout.Override.ActiveRowAppearance = appearance44;
			comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.Override.CardAreaAppearance = appearance45;
			appearance46.BorderColor = System.Drawing.Color.Silver;
			appearance46.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingAddressID.DisplayLayout.Override.CellAppearance = appearance46;
			comboBoxShippingAddressID.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingAddressID.DisplayLayout.Override.CellPadding = 0;
			appearance47.BackColor = System.Drawing.SystemColors.Control;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.Override.GroupByRowAppearance = appearance47;
			appearance48.TextHAlignAsString = "Left";
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderAppearance = appearance48;
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingAddressID.DisplayLayout.Override.RowAppearance = appearance49;
			comboBoxShippingAddressID.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingAddressID.DisplayLayout.Override.TemplateAddRowAppearance = appearance50;
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
			comboBoxShippingAddressID.Location = new System.Drawing.Point(305, 46);
			comboBoxShippingAddressID.MaxDropDownItems = 12;
			comboBoxShippingAddressID.Name = "comboBoxShippingAddressID";
			comboBoxShippingAddressID.ShowInactiveItems = false;
			comboBoxShippingAddressID.ShowQuickAdd = true;
			comboBoxShippingAddressID.Size = new System.Drawing.Size(140, 20);
			comboBoxShippingAddressID.TabIndex = 8;
			comboBoxShippingAddressID.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxBilltoAddress.BackColor = System.Drawing.Color.White;
			textBoxBilltoAddress.CustomReportFieldName = "";
			textBoxBilltoAddress.CustomReportKey = "";
			textBoxBilltoAddress.CustomReportValueType = 1;
			textBoxBilltoAddress.IsComboTextBox = false;
			textBoxBilltoAddress.IsModified = false;
			textBoxBilltoAddress.Location = new System.Drawing.Point(12, 65);
			textBoxBilltoAddress.MaxLength = 255;
			textBoxBilltoAddress.Multiline = true;
			textBoxBilltoAddress.Name = "textBoxBilltoAddress";
			textBoxBilltoAddress.Size = new System.Drawing.Size(215, 72);
			textBoxBilltoAddress.TabIndex = 9;
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = null;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance51;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance52.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance52;
			appearance53.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance53;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance54.BackColor2 = System.Drawing.SystemColors.Control;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance54;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance55;
			appearance56.BackColor = System.Drawing.SystemColors.Highlight;
			appearance56.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance56;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance57;
			appearance58.BorderColor = System.Drawing.Color.Silver;
			appearance58.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance58;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance59.BackColor = System.Drawing.SystemColors.Control;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance59;
			appearance60.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance60;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance61;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance62;
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
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = false;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(109, 20);
			comboBoxCustomer.TabIndex = 5;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			comboBoxCostCategory.Location = new System.Drawing.Point(504, 140);
			comboBoxCostCategory.MaxDropDownItems = 12;
			comboBoxCostCategory.Name = "comboBoxCostCategory";
			comboBoxCostCategory.ShowInactiveItems = false;
			comboBoxCostCategory.ShowQuickAdd = true;
			comboBoxCostCategory.Size = new System.Drawing.Size(127, 20);
			comboBoxCostCategory.TabIndex = 22;
			comboBoxCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			comboBoxJob.Location = new System.Drawing.Point(281, 140);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(135, 20);
			comboBoxJob.TabIndex = 21;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(451, 72);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(38, 13);
			mmLabel2.TabIndex = 147;
			mmLabel2.Text = "Expiry:";
			dateTimePickerExpiry.Checked = false;
			dateTimePickerExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerExpiry.Location = new System.Drawing.Point(532, 68);
			dateTimePickerExpiry.Name = "dateTimePickerExpiry";
			dateTimePickerExpiry.ShowCheckBox = true;
			dateTimePickerExpiry.Size = new System.Drawing.Size(118, 20);
			dateTimePickerExpiry.TabIndex = 12;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(451, 94);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(40, 13);
			label4.TabIndex = 146;
			label4.Text = "Status:";
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Items.AddRange(new object[4]
			{
				"Draft",
				"Active",
				"Expired",
				"Closed"
			});
			comboBoxStatus.Location = new System.Drawing.Point(532, 90);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(118, 21);
			comboBoxStatus.TabIndex = 13;
			comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCurrency.Location = new System.Drawing.Point(769, 87);
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
			comboBoxCurrency.Size = new System.Drawing.Size(133, 20);
			comboBoxCurrency.TabIndex = 19;
			comboBoxShippingMethod.Assigned = false;
			comboBoxShippingMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingMethod.CustomReportFieldName = "";
			comboBoxShippingMethod.CustomReportKey = "";
			comboBoxShippingMethod.CustomReportValueType = 1;
			comboBoxShippingMethod.DescriptionTextBox = null;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingMethod.DisplayLayout.Appearance = appearance63;
			comboBoxShippingMethod.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingMethod.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.Appearance = appearance64;
			appearance65.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BandLabelAppearance = appearance65;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance66.BackColor2 = System.Drawing.SystemColors.Control;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.PromptAppearance = appearance66;
			comboBoxShippingMethod.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingMethod.DisplayLayout.MaxRowScrollRegions = 1;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveCellAppearance = appearance67;
			appearance68.BackColor = System.Drawing.SystemColors.Highlight;
			appearance68.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveRowAppearance = appearance68;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.CardAreaAppearance = appearance69;
			appearance70.BorderColor = System.Drawing.Color.Silver;
			appearance70.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingMethod.DisplayLayout.Override.CellAppearance = appearance70;
			comboBoxShippingMethod.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingMethod.DisplayLayout.Override.CellPadding = 0;
			appearance71.BackColor = System.Drawing.SystemColors.Control;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.GroupByRowAppearance = appearance71;
			appearance72.TextHAlignAsString = "Left";
			comboBoxShippingMethod.DisplayLayout.Override.HeaderAppearance = appearance72;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingMethod.DisplayLayout.Override.RowAppearance = appearance73;
			comboBoxShippingMethod.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingMethod.DisplayLayout.Override.TemplateAddRowAppearance = appearance74;
			comboBoxShippingMethod.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingMethod.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingMethod.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingMethod.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingMethod.Editable = true;
			comboBoxShippingMethod.FilterString = "";
			comboBoxShippingMethod.HasAllAccount = false;
			comboBoxShippingMethod.HasCustom = false;
			comboBoxShippingMethod.IsDataLoaded = false;
			comboBoxShippingMethod.Location = new System.Drawing.Point(101, 140);
			comboBoxShippingMethod.MaxDropDownItems = 12;
			comboBoxShippingMethod.Name = "comboBoxShippingMethod";
			comboBoxShippingMethod.ShowInactiveItems = false;
			comboBoxShippingMethod.ShowQuickAdd = true;
			comboBoxShippingMethod.Size = new System.Drawing.Size(126, 20);
			comboBoxShippingMethod.TabIndex = 20;
			comboBoxShippingMethod.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxTerm.Assigned = false;
			comboBoxTerm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTerm.CustomReportFieldName = "";
			comboBoxTerm.CustomReportKey = "";
			comboBoxTerm.CustomReportValueType = 1;
			comboBoxTerm.DescriptionTextBox = null;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTerm.DisplayLayout.Appearance = appearance75;
			comboBoxTerm.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTerm.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance76.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.GroupByBox.Appearance = appearance76;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTerm.DisplayLayout.GroupByBox.BandLabelAppearance = appearance77;
			comboBoxTerm.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance78.BackColor2 = System.Drawing.SystemColors.Control;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTerm.DisplayLayout.GroupByBox.PromptAppearance = appearance78;
			comboBoxTerm.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTerm.DisplayLayout.MaxRowScrollRegions = 1;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTerm.DisplayLayout.Override.ActiveCellAppearance = appearance79;
			appearance80.BackColor = System.Drawing.SystemColors.Highlight;
			appearance80.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTerm.DisplayLayout.Override.ActiveRowAppearance = appearance80;
			comboBoxTerm.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTerm.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.Override.CardAreaAppearance = appearance81;
			appearance82.BorderColor = System.Drawing.Color.Silver;
			appearance82.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTerm.DisplayLayout.Override.CellAppearance = appearance82;
			comboBoxTerm.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTerm.DisplayLayout.Override.CellPadding = 0;
			appearance83.BackColor = System.Drawing.SystemColors.Control;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.Override.GroupByRowAppearance = appearance83;
			appearance84.TextHAlignAsString = "Left";
			comboBoxTerm.DisplayLayout.Override.HeaderAppearance = appearance84;
			comboBoxTerm.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTerm.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.Color.Silver;
			comboBoxTerm.DisplayLayout.Override.RowAppearance = appearance85;
			comboBoxTerm.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTerm.DisplayLayout.Override.TemplateAddRowAppearance = appearance86;
			comboBoxTerm.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTerm.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTerm.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTerm.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTerm.Editable = true;
			comboBoxTerm.FilterString = "";
			comboBoxTerm.HasAllAccount = false;
			comboBoxTerm.HasCustom = false;
			comboBoxTerm.IsDataLoaded = false;
			comboBoxTerm.Location = new System.Drawing.Point(532, 46);
			comboBoxTerm.MaxDropDownItems = 12;
			comboBoxTerm.Name = "comboBoxTerm";
			comboBoxTerm.ShowInactiveItems = false;
			comboBoxTerm.ShowQuickAdd = true;
			comboBoxTerm.Size = new System.Drawing.Size(118, 20);
			comboBoxTerm.TabIndex = 11;
			comboBoxTerm.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSalesperson.Assigned = false;
			comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesperson.CustomReportFieldName = "";
			comboBoxSalesperson.CustomReportKey = "";
			comboBoxSalesperson.CustomReportValueType = 1;
			comboBoxSalesperson.DescriptionTextBox = null;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesperson.DisplayLayout.Appearance = appearance87;
			comboBoxSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance88.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance88.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance88.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.GroupByBox.Appearance = appearance88;
			appearance89.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance89;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance90.BackColor2 = System.Drawing.SystemColors.Control;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance90.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance90;
			comboBoxSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance91;
			appearance92.BackColor = System.Drawing.SystemColors.Highlight;
			appearance92.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance92;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance93;
			appearance94.BorderColor = System.Drawing.Color.Silver;
			appearance94.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesperson.DisplayLayout.Override.CellAppearance = appearance94;
			comboBoxSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance95.BackColor = System.Drawing.SystemColors.Control;
			appearance95.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance95.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance95.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance95;
			appearance96.TextHAlignAsString = "Left";
			comboBoxSalesperson.DisplayLayout.Override.HeaderAppearance = appearance96;
			comboBoxSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesperson.DisplayLayout.Override.RowAppearance = appearance97;
			comboBoxSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance98;
			comboBoxSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesperson.Editable = true;
			comboBoxSalesperson.FilterString = "";
			comboBoxSalesperson.HasAllAccount = false;
			comboBoxSalesperson.HasCustom = false;
			comboBoxSalesperson.IsDataLoaded = false;
			comboBoxSalesperson.Location = new System.Drawing.Point(769, 66);
			comboBoxSalesperson.MaxDropDownItems = 12;
			comboBoxSalesperson.Name = "comboBoxSalesperson";
			comboBoxSalesperson.ShowInactiveItems = false;
			comboBoxSalesperson.ShowQuickAdd = true;
			comboBoxSalesperson.Size = new System.Drawing.Size(133, 20);
			comboBoxSalesperson.TabIndex = 18;
			comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance99.FontData.BoldAsString = "False";
			appearance99.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance99;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(686, 69);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(66, 15);
			ultraFormattedLinkLabel6.TabIndex = 137;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Salesperson:";
			appearance100.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance100;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			appearance101.FontData.BoldAsString = "False";
			appearance101.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance101;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(230, 48);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel3.TabIndex = 16;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Ship To:";
			appearance102.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance102;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			appearance103.FontData.BoldAsString = "False";
			appearance103.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance103;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 44);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(38, 15);
			ultraFormattedLinkLabel1.TabIndex = 8;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Bill To:";
			appearance104.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance104;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(686, 49);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(79, 13);
			label2.TabIndex = 132;
			label2.Text = "Customer PO#:";
			textBoxPONumber.Location = new System.Drawing.Point(769, 45);
			textBoxPONumber.MaxLength = 30;
			textBoxPONumber.Name = "textBoxPONumber";
			textBoxPONumber.Size = new System.Drawing.Size(133, 20);
			textBoxPONumber.TabIndex = 17;
			appearance105.FontData.BoldAsString = "True";
			appearance105.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance105;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(12, 24);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(79, 15);
			ultraFormattedLinkLabel4.TabIndex = 4;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Customer ID:";
			appearance106.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance106;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance107.FontData.BoldAsString = "True";
			appearance107.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance107;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance108.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance108;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance109;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance110.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance110.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance110.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance110;
			appearance111.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance111;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance112.BackColor2 = System.Drawing.SystemColors.Control;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance112.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance112;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance113;
			appearance114.BackColor = System.Drawing.SystemColors.Highlight;
			appearance114.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance114;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance115;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			appearance116.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance116;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance117.BackColor = System.Drawing.SystemColors.Control;
			appearance117.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance117.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance117.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance117.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance117;
			appearance118.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance118;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance119;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance120;
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
			mmLabel1.Location = new System.Drawing.Point(686, 7);
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
			textBoxCustomerName.Size = new System.Drawing.Size(293, 20);
			textBoxCustomerName.TabIndex = 6;
			textBoxCustomerName.TabStop = false;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 402);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(904, 49);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(textBoxDiscountPercent);
			panel1.Controls.Add(textBoxDiscountAmount);
			panel1.Controls.Add(panelNonTax);
			panel1.Controls.Add(label11);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(textBoxSubtotal);
			panel1.Location = new System.Drawing.Point(700, 454);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(220, 112);
			panel1.TabIndex = 3;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(4, 25);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(52, 13);
			label7.TabIndex = 142;
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
			panelNonTax.Controls.Add(label9);
			panelNonTax.Controls.Add(textBoxRoundOff);
			panelNonTax.Controls.Add(linkLabelTax);
			panelNonTax.Controls.Add(textBoxTaxAmount);
			panelNonTax.Controls.Add(label8);
			panelNonTax.Controls.Add(textBoxTotal);
			panelNonTax.Location = new System.Drawing.Point(0, 43);
			panelNonTax.Name = "panelNonTax";
			panelNonTax.Size = new System.Drawing.Size(219, 69);
			panelNonTax.TabIndex = 139;
			label9.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			label9.Location = new System.Drawing.Point(4, 27);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(69, 15);
			label9.TabIndex = 155;
			label9.Text = "Round Off:";
			label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			textBoxRoundOff.AllowDecimal = true;
			textBoxRoundOff.CustomReportFieldName = "";
			textBoxRoundOff.CustomReportKey = "";
			textBoxRoundOff.CustomReportValueType = 1;
			textBoxRoundOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxRoundOff.IsComboTextBox = false;
			textBoxRoundOff.IsModified = false;
			textBoxRoundOff.Location = new System.Drawing.Point(80, 22);
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
			textBoxRoundOff.TabIndex = 154;
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
			appearance121.FontData.BoldAsString = "False";
			appearance121.FontData.Name = "Tahoma";
			linkLabelTax.Appearance = appearance121;
			linkLabelTax.AutoSize = true;
			linkLabelTax.Location = new System.Drawing.Point(4, 4);
			linkLabelTax.Name = "linkLabelTax";
			linkLabelTax.Size = new System.Drawing.Size(25, 15);
			linkLabelTax.TabIndex = 153;
			linkLabelTax.TabStop = true;
			linkLabelTax.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelTax.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelTax.Value = "Tax:";
			appearance122.ForeColor = System.Drawing.Color.Blue;
			linkLabelTax.VisitedLinkAppearance = appearance122;
			linkLabelTax.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelTax_LinkClicked);
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
			textBoxTaxAmount.TabIndex = 1;
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
			label8.Location = new System.Drawing.Point(4, 48);
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
			textBoxTotal.Location = new System.Drawing.Point(80, 44);
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
			textBoxTotal.TabIndex = 2;
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
			label11.TabIndex = 138;
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
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7]
			{
				availableQuantityToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem,
				itemSortToolStripMenuItem,
				toolStripSeparator3,
				removeRowToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(181, 142);
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
			itemSortToolStripMenuItem.Name = "itemSortToolStripMenuItem";
			itemSortToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemSortToolStripMenuItem.Text = "Item Sort..";
			itemSortToolStripMenuItem.Click += new System.EventHandler(itemSortToolStripMenuItem_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
			removeRowToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
			removeRowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			removeRowToolStripMenuItem.Text = "Remove Row";
			removeRowToolStripMenuItem.Click += new System.EventHandler(removeRowToolStripMenuItem_Click);
			checkBoxPriceIncludeTax.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			checkBoxPriceIncludeTax.AutoSize = true;
			checkBoxPriceIncludeTax.Location = new System.Drawing.Point(556, 462);
			checkBoxPriceIncludeTax.Name = "checkBoxPriceIncludeTax";
			checkBoxPriceIncludeTax.Size = new System.Drawing.Size(123, 17);
			checkBoxPriceIncludeTax.TabIndex = 121;
			checkBoxPriceIncludeTax.Text = "Price inclusive of tax";
			checkBoxPriceIncludeTax.UseVisualStyleBackColor = true;
			checkBoxPriceIncludeTax.CheckedChanged += new System.EventHandler(checkBoxPriceIncludeTax_CheckedChanged);
			labelSelectedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelSelectedDocs.AutoSize = true;
			labelSelectedDocs.Location = new System.Drawing.Point(365, 482);
			labelSelectedDocs.Name = "labelSelectedDocs";
			labelSelectedDocs.Size = new System.Drawing.Size(109, 13);
			labelSelectedDocs.TabIndex = 167;
			labelSelectedDocs.Text = "Selected Documents:";
			labelSelectedDocs.Visible = false;
			checkedListBoxDN.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxDN.FormattingEnabled = true;
			checkedListBoxDN.Location = new System.Drawing.Point(368, 498);
			checkedListBoxDN.Name = "checkedListBoxDN";
			checkedListBoxDN.Size = new System.Drawing.Size(152, 56);
			checkedListBoxDN.TabIndex = 166;
			checkedListBoxDN.Visible = false;
			checkedListBoxDN.DoubleClick += new System.EventHandler(checkedListBoxDN_DoubleClick);
			labelProcessedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelProcessedDocs.AutoSize = true;
			labelProcessedDocs.Location = new System.Drawing.Point(525, 482);
			labelProcessedDocs.Name = "labelProcessedDocs";
			labelProcessedDocs.Size = new System.Drawing.Size(117, 13);
			labelProcessedDocs.TabIndex = 168;
			labelProcessedDocs.Text = "Processed Documents:";
			labelProcessedDocs.Visible = false;
			checkedListBoxProcessedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxProcessedDocs.FormattingEnabled = true;
			checkedListBoxProcessedDocs.Location = new System.Drawing.Point(525, 498);
			checkedListBoxProcessedDocs.Name = "checkedListBoxProcessedDocs";
			checkedListBoxProcessedDocs.Size = new System.Drawing.Size(152, 56);
			checkedListBoxProcessedDocs.TabIndex = 169;
			checkedListBoxProcessedDocs.Visible = false;
			checkedListBoxProcessedDocs.DoubleClick += new System.EventHandler(checkedListBoxProcessedDocs_DoubleClick);
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
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			appearance123.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance123;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance124.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance124.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance124.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance124;
			appearance125.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance125;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance126.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance126.BackColor2 = System.Drawing.SystemColors.Control;
			appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance126.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance126;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance127;
			appearance128.BackColor = System.Drawing.SystemColors.Highlight;
			appearance128.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance128;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance129;
			appearance130.BorderColor = System.Drawing.Color.Silver;
			appearance130.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance130;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance131.BackColor = System.Drawing.SystemColors.Control;
			appearance131.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance131.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance131.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance131;
			appearance132.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance132;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance133;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance134.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance134;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 241);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(906, 213);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			appearance135.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance135;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance136.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance136.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance136.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance136.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance136;
			appearance137.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance137;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance138.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance138.BackColor2 = System.Drawing.SystemColors.Control;
			appearance138.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance138.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance138;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance139;
			appearance140.BackColor = System.Drawing.SystemColors.Highlight;
			appearance140.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance140;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance141.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance141;
			appearance142.BorderColor = System.Drawing.Color.Silver;
			appearance142.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance142;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance143.BackColor = System.Drawing.SystemColors.Control;
			appearance143.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance143.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance143.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance143.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance143;
			appearance144.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance144;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance145;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance146.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance146;
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
			comboBoxGridItem.Location = new System.Drawing.Point(675, 167);
			comboBoxGridItem.MaxDropDownItems = 12;
			comboBoxGridItem.Name = "comboBoxGridItem";
			comboBoxGridItem.Show3PLItems = true;
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowOnlyLotItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(74, 20);
			comboBoxGridItem.TabIndex = 118;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
			mmTextBoxRevNo.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBoxRevNo.CustomReportFieldName = "";
			mmTextBoxRevNo.CustomReportKey = "";
			mmTextBoxRevNo.CustomReportValueType = 1;
			mmTextBoxRevNo.ForeColor = System.Drawing.Color.Black;
			mmTextBoxRevNo.IsComboTextBox = false;
			mmTextBoxRevNo.IsModified = false;
			mmTextBoxRevNo.Location = new System.Drawing.Point(117, 25);
			mmTextBoxRevNo.MaxLength = 255;
			mmTextBoxRevNo.Name = "mmTextBoxRevNo";
			mmTextBoxRevNo.ReadOnly = true;
			mmTextBoxRevNo.Size = new System.Drawing.Size(133, 20);
			mmTextBoxRevNo.TabIndex = 67;
			mmTextBoxRevNo.TabStop = false;
			mmTextBoxRevNo.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(932, 612);
			base.Controls.Add(labelProcessedDocs);
			base.Controls.Add(checkedListBoxProcessedDocs);
			base.Controls.Add(labelSelectedDocs);
			base.Controls.Add(checkedListBoxDN);
			base.Controls.Add(checkBoxPriceIncludeTax);
			base.Controls.Add(panel1);
			base.Controls.Add(productPhotoViewer);
			base.Controls.Add(panelDetails);
			base.Controls.Add(labelVoided);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxGridItem);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "SalesQuoteForm";
			Text = "Sales Quote";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).EndInit();
			panelReviseDate.ResumeLayout(false);
			panelReviseDate.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBillingAddress).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingAddressID).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTerm).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panelNonTax.ResumeLayout(false);
			panelNonTax.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
