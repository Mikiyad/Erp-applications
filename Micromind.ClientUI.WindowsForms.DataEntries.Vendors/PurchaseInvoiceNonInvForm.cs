using DevExpress.Utils;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
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
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Vendors
{
	public class PurchaseInvoiceNonInvForm : Form, IForm
	{
		private PayeeTaxOptions taxOption = PayeeTaxOptions.NonTaxable;

		private bool isTaxIncluded;

		private ItemSourceTypes sourceDocType;

		private bool allowNegativeQty = true;

		private bool allowPriceChange = true;

		private DataTable grnTable;

		private bool allowEdit = true;

		private PurchaseInvoiceNIData currentData;

		private const string TABLENAME_CONST = "Purchase_Invoice_NonInv";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentVendorAddressID = "";

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool showItemdetail = CompanyPreferences.ShowItemdetail;

		private bool useProperty = CompanyPreferences.UseProperty;

		private bool purchaseLandingCostCalculationMethod = true;

		private bool userViewStaus;

		private decimal TaxPercent = CompanyPreferences.TaxPercent;

		private bool LoadItemFeatures = CompanyPreferences.LoadItemFeatures;

		private string attributeID1Title = CompanyPreferences.AttributeID1Title;

		private string attributeID2Title = CompanyPreferences.AttributeID2Title;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private bool isDiscountPercent;

		private bool totalChanged;

		private bool restrictTransaction;

		private bool isTaxPercent;

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

		private vendorsFlatComboBox comboBoxVendor;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TextBox textBoxVendorName;

		private AmountTextBox textBoxSubtotal;

		private Panel panel1;

		private Label label5;

		private Label label8;

		private AmountTextBox textBoxTotal;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem purchaseStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private LocationComboBox comboBoxGridLocation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem createFromPurchaseOrderToolStripMenuItem;

		private Label label11;

		private ShippingMethodsComboBox comboBoxShippingMethod;

		private PaymentTermComboBox comboBoxTerm;

		private BuyerComboBox comboBoxBuyer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private CurrencySelector comboBoxCurrency;

		private ToolStripMenuItem createFromItemReceiptToolStripMenuItem;

		private Panel panelNonTax;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerDueDate;

		private XPButton buttonSelectDocument;

		private Label labelSelectedDocs;

		private ListBox checkedListBoxPO;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageItems;

		private UltraTabPageControl tabPageExpense;

		private DataEntryGrid dataGridExpense;

		private ExpenseCodeComboBox comboBoxGridExpenseCode;

		private CurrencyComboBox comboBoxGridCurrency;

		private AmountTextBox textBoxTotalExpense;

		private Label label2;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator6;

		private JobComboBox comboBoxgridJob;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonDistribution;

		private Label label4;

		private TextBox textBoxRef2;

		private ToolStripButton toolStripButtonInformation;

		private ToolTipController toolTipController1;

		private TextBox textBoxBOLNo;

		private Label label13;

		private CostCategoryComboBox comboBoxCostCategory;

		private JobComboBox comboBoxjob;

		private UltraFormattedLinkLabel labelCurrency;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel labelJob;

		private UltraFormattedLinkLabel labelcostcategory;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private Label label7;

		private PercentTextBox textBoxDiscountPercent;

		private AmountTextBox textBoxDiscountAmount;

		private AmountTextBox textBoxTaxAmount;

		private UltraFormattedLinkLabel labelTaxGroup;

		private TaxGroupComboBox comboBoxPayeeTaxGroup;

		private UltraFormattedLinkLabel linkLabelTax;

		private CheckBox checkBoxPriceIncludeTax;

		private AnalysisComboBox comboBoxAnalysis;

		private Label label6;

		private TextBox textBoxSupplierInvoiceNo;

		private ToolStripButton toolStripButtonExcelImport;

		private PropertyUnitComboBox comboBoxPropertyUnit;

		private PropertyComboBox comboBoxProperty;

		private MMLabel labelTaxOption;

		private ComboBox comboBoxTaxOption;

		public ScreenAreas ScreenArea => ScreenAreas.Purchases;

		public int ScreenID => 3008;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

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
				bool enabled;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
					vendorsFlatComboBox vendorsFlatComboBox = comboBoxVendor;
					enabled = (comboBoxSysDoc.Enabled = true);
					vendorsFlatComboBox.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = false;
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
						comboBoxVendor.Enabled = false;
					}
					else
					{
						comboBoxVendor.Enabled = true;
					}
					comboBoxSysDoc.Enabled = false;
					textBoxVoucherNumber.ReadOnly = true;
				}
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
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
				dataGridExpense.Enabled = !value;
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

		public PurchaseInvoiceNonInvForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			allowNegativeQty = CompanyPreferences.AllowPurchaseInvoiceNegativeQty;
			allowPriceChange = CompanyPreferences.AllowPurchaseInvoiceChangePrice;
			purchaseLandingCostCalculationMethod = CompanyPreferences.PurchaseLandingCostCalculationMethod;
			comboBoxGridLocation.ShowWarehouseOnly = true;
			labelJob.Visible = (labelcostcategory.Visible = (comboBoxjob.Visible = (comboBoxCostCategory.Visible = useJobCosting)));
		}

		private void AddEvents()
		{
			base.Load += Form_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
			dataGridItems.DoubleClickCell += dataGridItems_DoubleClickCell;
			dataGridExpense.BeforeCellUpdate += dataGridExpense_BeforeCellUpdate;
			dataGridExpense.AfterCellUpdate += dataGridExpense_AfterCellUpdate;
			dataGridExpense.BeforeRowDeactivate += dataGridExpense_BeforeRowDeactivate;
			dataGridExpense.BeforeCellDeactivate += dataGridExpense_BeforeCellDeactivate;
			dataGridExpense.AfterRowsDeleted += dataGridExpense_AfterRowsDeleted;
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxVendor.SelectedIndexChanged += comboBoxVendor_SelectedIndexChanged;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
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
			comboBoxPayeeTaxGroup.SelectedIndexChanged += comboBoxPayeeTaxGroup_SelectedIndexChanged;
		}

		private void dataGridItems_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity")
			{
				AllocateQuantityToLot(e.Cell);
			}
		}

		private void dataGridItems_DoubleClick(object sender, EventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			AllocateQuantityToLot(activeRow.Cells["Quantity"]);
		}

		private void dataGridExpense_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity")
			{
				AllocateQuantityToLot(e.Cell);
			}
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			string text = "";
			if (dataGridItems.Rows.Count > 0 && dataGridItems.ActiveRow.Cells["Item code"].Value != null)
			{
				text = activeRow.Cells["Item code"].Value.ToString();
			}
			if (text != "" && showItemdetail && userViewStaus)
			{
				DisplayItemDetails(text);
			}
		}

		private bool AllocateQuantityToLot(UltraGridCell cell)
		{
			try
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
					ReceiveLotSelectionForm receiveLotSelectionForm = new ReceiveLotSelectionForm();
					receiveLotSelectionForm.ProductID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					receiveLotSelectionForm.ProductDescription = dataGridItems.ActiveRow.Cells["Description"].Value.ToString();
					receiveLotSelectionForm.LocationID = dataGridItems.ActiveRow.Cells["Location"].Value.ToString();
					receiveLotSelectionForm.RowQuantity = float.Parse(cell.Text);
					if (cell.Tag != null)
					{
						receiveLotSelectionForm.ProductLotTable = (DataTable)cell.Tag;
					}
					if (receiveLotSelectionForm.ShowDialog() != DialogResult.OK)
					{
						return false;
					}
					cell.Tag = receiveLotSelectionForm.ProductLotTable;
					cell.Appearance.FontData.Underline = DefaultableBoolean.True;
					cell.Appearance.ForeColor = Color.Black;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
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
				if (dataGridItems.ActiveCell.Text.Trim() == "")
				{
					dataGridItems.ActiveCell.Value = 0;
				}
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

		private void dataGridExpense_AfterRowsDeleted(object sender, EventArgs e)
		{
			if (!purchaseLandingCostCalculationMethod)
			{
				CalculateLandingCostByWeight();
			}
			else
			{
				CalculateLandingCost();
			}
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

		private void dataGridExpense_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridExpense.ActiveRow != null)
				{
					if (e.Cell.Column.Key == "Expense Code")
					{
						dataGridExpense.ActiveRow.Cells["Description"].Value = comboBoxGridExpenseCode.SelectedName;
						if (dataGridExpense.ActiveRow.Cells["Currency"].Value.ToString() == "")
						{
							dataGridExpense.ActiveRow.Cells["Currency"].Value = Global.BaseCurrencyID;
						}
						ItemTaxOptions itemTaxOptions = comboBoxGridExpenseCode.TaxOption;
						dataGridExpense.ActiveRow.Cells["TaxOption"].Value = itemTaxOptions;
						switch (itemTaxOptions)
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
					}
					else if (e.Cell.Column.Key == "TaxGroupID")
					{
						ItemTaxOptions itemTaxOption = ItemTaxOptions.NonTaxable;
						if (!dataGridExpense.ActiveRow.Cells["TaxOption"].Value.IsNullOrEmpty())
						{
							itemTaxOption = (ItemTaxOptions)byte.Parse(dataGridExpense.ActiveRow.Cells["TaxOption"].Value.ToString());
						}
						TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxVendor.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOption, comboBoxGridExpenseCode.TaxGroupID);
						dataGridExpense.ActiveRow.Cells["Tax"].Tag = tag;
					}
					if (e.Cell.Column.Key == "Currency")
					{
						dataGridExpense.ActiveRow.Cells["Rate"].Value = comboBoxGridCurrency.SelectedRate.ToString();
						dataGridExpense.ActiveRow.Cells["RateType"].Value = comboBoxGridCurrency.SelectedRateType;
					}
					if (e.Cell.Column.Key == "Amount" || e.Cell.Column.Key == "Rate" || e.Cell.Column.Key == "Currency")
					{
						decimal result = default(decimal);
						decimal.TryParse(e.Cell.Value.ToString(), out result);
						decimal subtotal = decimal.Parse(textBoxTotalExpense.Text);
						UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result, subtotal, default(decimal), checkBoxPriceIncludeTax.Checked);
						SetRowLCAmount(dataGridExpense.ActiveRow);
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetRowLCAmount(UltraGridRow row)
		{
			decimal result = default(decimal);
			decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result);
			decimal num = 1m;
			if (row.Cells["Rate"].Value.ToString() != "")
			{
				num = decimal.Parse(row.Cells["Rate"].Value.ToString());
			}
			string a = row.Cells["RateType"].Value.ToString();
			if (row.Cells["Currency"].Value.ToString() == "" || row.Cells["Currency"].Value.ToString() == Global.BaseCurrencyID)
			{
				num = 1m;
			}
			if (row.Cells["Currency"].Value.ToString() != "" && row.Cells["Currency"].Value.ToString() != Global.BaseCurrencyID && num > 0m)
			{
				if (a == "D")
				{
					result /= num;
				}
				else if (a == "M")
				{
					result *= num;
				}
			}
			row.Cells["AmountLC"].Value = result.ToString(Format.TotalAmountFormat);
			CalculateTotalExpense();
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
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
			if (LoadItemFeatures && e.KeyCode == Keys.F3)
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
				if (comboBoxVendor.SelectedID != "" && comboBoxVendor.SelectedID != null)
				{
					comboSearchDialogNew.SelectedVendor = comboBoxVendor.SelectedID;
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

		private void comboBoxVendor_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxVendorName.Text = comboBoxVendor.SelectedName;
			try
			{
				if (!isDataLoading && comboBoxVendor.SelectedID != "")
				{
					string defaultCurrencyID = comboBoxVendor.DefaultCurrencyID;
					comboBoxCurrency.SelectedID = defaultCurrencyID;
					comboBoxCurrency.GetLastRate();
					comboBoxTerm.SelectedID = comboBoxVendor.DefaultPaymentTerm;
					comboBoxShippingMethod.SelectedID = comboBoxVendor.DefaultShippingMethod;
					comboBoxBuyer.SelectedID = comboBoxVendor.DefaultBuyer;
					if (comboBoxPayeeTaxGroup.SelectedID != "")
					{
						comboBoxTaxOption.SelectedIndex = 0;
					}
					if (CompanyPreferences.IsTax)
					{
						comboBoxPayeeTaxGroup.Clear();
						taxOption = comboBoxVendor.TaxOption;
						string text = taxOption.ToString();
						comboBoxTaxOption.Text = text;
						if (taxOption == PayeeTaxOptions.Taxable || taxOption == PayeeTaxOptions.ReverseCharge)
						{
							comboBoxPayeeTaxGroup.SelectedID = comboBoxVendor.DefaultTaxGroupID;
						}
						if (taxOption == PayeeTaxOptions.ReverseCharge)
						{
							checkBoxPriceIncludeTax.Checked = false;
							checkBoxPriceIncludeTax.Enabled = false;
						}
						else
						{
							checkBoxPriceIncludeTax.Enabled = true;
						}
					}
					else
					{
						comboBoxPayeeTaxGroup.Clear();
					}
					SetDueDate();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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
			if (!isDataLoading)
			{
				if (isNewRecord)
				{
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
				comboBoxVendor.FilterSysDocID = comboBoxSysDoc.SelectedID;
				comboBoxGridItem.FilterSysDocID = comboBoxSysDoc.SelectedID;
				CheckBox checkBox = checkBoxPriceIncludeTax;
				bool visible = checkBoxPriceIncludeTax.Checked = comboBoxSysDoc.IsPriceIncludeTax;
				checkBox.Visible = visible;
			}
		}

		private void comboBoxPayeeTaxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			CalculateAllRowsTaxes();
			CalculateTotal();
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
					ItemTypes itemTypes = ItemTypes.None;
					if (comboBoxGridItem.SelectedRow != null && !(comboBoxGridItem.SelectedID == "") && comboBoxGridItem.SelectedRow != null && comboBoxGridItem.SelectedRow.Cells["ItemType"].Value != null)
					{
						itemTypes = (ItemTypes)checked((byte)int.Parse(comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString()));
					}
					if (itemTypes == ItemTypes.Matrix)
					{
						MatrixSelectionForm matrixSelectionForm = new MatrixSelectionForm();
						matrixSelectionForm.LoadMatrixData(comboBoxGridItem.SelectedID, comboBoxGridItem.SelectedName);
						string text = "";
						if (!allowNegativeQty)
						{
							matrixSelectionForm.AllowNegativeQuantity = false;
						}
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
								ultraGridRow.Cells["Price"].Value = row["LastCost"].ToString();
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
					dataGridItems.ActiveRow.Cells["Attribute1"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute1"].Value.ToString();
					dataGridItems.ActiveRow.Cells["Attribute2"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute2"].Value.ToString();
					dataGridItems.ActiveRow.Cells["Attribute3"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute3"].Value.ToString();
					dataGridItems.ActiveRow.Cells["MatrixParentID"].Value = comboBoxGridItem.SelectedRow.Cells["MatrixParentID"].Value.ToString();
					dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
					dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
					dataGridItems.ActiveRow.Cells["Quantity"].Tag = null;
					dataGridItems.ActiveRow.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
					if (comboBoxGridItem.SelectedID != "")
					{
						decimal productPurchasePrice = Factory.ProductSystem.GetProductPurchasePrice(comboBoxGridItem.SelectedID);
						dataGridItems.ActiveRow.Cells["Price"].Value = productPurchasePrice;
						dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
						dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
						ItemTaxOptions itemTaxOptions = comboBoxGridItem.TaxOption;
						dataGridItems.ActiveRow.Cells["TaxOption"].Value = itemTaxOptions;
						switch (itemTaxOptions)
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
						if ((dataGridItems.ActiveRow.Cells["Location"].Value == null || dataGridItems.ActiveRow.Cells["Location"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
						{
							dataGridItems.ActiveRow.Cells["Location"].Value = dataGridItems.Rows[checked(dataGridItems.ActiveRow.Index - 1)].Cells["Location"].Value;
						}
						if (comboBoxGridItem.SelectedRow != null)
						{
							dataGridItems.ActiveRow.Cells["ItemType"].Value = comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString();
						}
						else
						{
							dataGridItems.ActiveRow.Cells["ItemType"].Value = null;
						}
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
						if (comboBoxGridItem.SelectedRow != null)
						{
							dataGridItems.ActiveRow.Cells["Weight"].Value = ((!string.IsNullOrEmpty(comboBoxGridItem.SelectedRow.Cells["Weight"].Value.ToString())) ? decimal.Parse(comboBoxGridItem.SelectedRow.Cells["Weight"].Value.ToString()) : 0m);
						}
						else
						{
							dataGridItems.ActiveRow.Cells["Weight"].Value = 0;
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
					}
				}
				else if (e.Cell.Column.Key == "TaxGroupID")
				{
					ItemTaxOptions itemTaxOption = ItemTaxOptions.BasedOnCustomer;
					if (!dataGridItems.ActiveRow.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						itemTaxOption = (ItemTaxOptions)byte.Parse(dataGridItems.ActiveRow.Cells["TaxOption"].Value.ToString());
					}
					TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxVendor.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOption, comboBoxGridItem.TaxGroupID);
					dataGridItems.ActiveRow.Cells["Tax"].Tag = tag;
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
						ItemTypes itemTypes2 = ItemTypes.None;
						if (key == "ItemType" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "ItemType")
						{
							itemTypes2 = (ItemTypes)checked((byte)int.Parse(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString()));
						}
						if (itemTypes2 == ItemTypes.Discount && result5 > 0m)
						{
							result5 = -1m * Math.Abs(result5);
							dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
						}
						result4 = Math.Round(result5 / result3, 5);
						if (itemTypes2 != ItemTypes.Discount && result5 < 0m)
						{
							result3 = Math.Abs(result3);
							dataGridItems.ActiveRow.Cells["Quantity"].Value = result3;
							result5 = Math.Abs(result5);
							dataGridItems.ActiveRow.Cells["Amount"].Value = result5;
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
					if (key == "Amount")
					{
						decimal result6 = default(decimal);
						decimal.TryParse(e.Cell.Value.ToString(), out result6);
						decimal subtotal = decimal.Parse(textBoxSubtotal.Text);
						decimal tradeDiscount = decimal.Parse(textBoxDiscountAmount.Text);
						UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result6, subtotal, tradeDiscount);
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

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotal();
			CalculateAllRowsTaxes();
			CalculateTotal();
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
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Job")
				{
					for (int i = dataGridItems.ActiveCell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
					{
						if (dataGridItems.Rows[i].Cells["Job"].Value.ToString() == "")
						{
							dataGridItems.Rows[i].Cells["Job"].Value = dataGridItems.ActiveCell.Value;
						}
					}
				}
				else
				{
					if (!(dataGridItems.ActiveCell.Column.Key.ToString() == "Location"))
					{
						return;
					}
					for (int j = dataGridItems.ActiveCell.Row.Index + 1; j < dataGridItems.Rows.Count; j++)
					{
						if (dataGridItems.Rows[j].Cells["Location"].Value.ToString() == "")
						{
							dataGridItems.Rows[j].Cells["Location"].Value = dataGridItems.ActiveCell.Value;
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
			if (e.Cell.Column.Key == "Amount")
			{
				decimal d = default(decimal);
				if (e.NewValue != null && e.NewValue.ToString() != "")
				{
					d = decimal.Parse(e.NewValue.ToString());
				}
				e.Cell.Row.Cells["ItemType"].Value.ToString();
				ItemTypes itemTypes = ItemTypes.Inventory;
				if (e.Cell.Row.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(e.Cell.Row.Cells["ItemType"].Value.ToString(), NumberStyles.Any));
				}
				if (itemTypes != ItemTypes.Discount && d < 0m)
				{
					ErrorHelper.InformationMessage("Negative amount is not allowed for this type of item. Please enter a positive amount.");
					e.Cancel = true;
					return;
				}
			}
			if (CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.POThenInvoice && e.Cell.Column.Key == "Quantity" && sourceDocType == ItemSourceTypes.PurchaseOrder && !CompanyPreferences.AllowLPurchaseQtyMoreThanPO && e.Cell.Row.Cells["ISPORRow"].Value.ToString() != "")
			{
				decimal d2 = decimal.Parse(e.NewValue.ToString());
				decimal result = default(decimal);
				decimal.TryParse(e.Cell.Row.Cells["Received"].Value.ToString(), out result);
				decimal result2 = default(decimal);
				decimal.TryParse(e.Cell.Row.Cells["Ordered"].Value.ToString(), out result2);
				if (d2 > result2 - result)
				{
					ErrorHelper.InformationMessage("Invoice quantity cannot be greater than ordered quantity.");
					e.Cancel = true;
					return;
				}
			}
			if (e.Cell != null && e.Cell.Column.Key == "Quantity" && dataGridItems.ActiveCell.Column.Key == "Quantity" && !AllocateQuantityToLot(e.Cell))
			{
				e.Cancel = true;
			}
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
				ErrorHelper.InformationMessage("Invalid quantity. Please enter a non-negative numeric value.");
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
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PurchaseInvoiceNIData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PurchaseInvoiceTable.Rows[0] : currentData.PurchaseInvoiceTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["DueDate"] = dateTimePickerDueDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["IsCash"] = false;
				dataRow["PurchaseFlow"] = CompanyPreferences.LocalPurchaseFlow;
				dataRow["BOLNo"] = textBoxBOLNo.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Reference2"] = textBoxRef2.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["VendorID"] = comboBoxVendor.SelectedID;
				dataRow["SourceDocType"] = sourceDocType;
				dataRow["SupplierInvoiceNumber"] = textBoxSupplierInvoiceNo.Text;
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
				if (comboBoxBuyer.SelectedID != "")
				{
					dataRow["BuyerID"] = comboBoxBuyer.SelectedID;
				}
				else
				{
					dataRow["BuyerID"] = DBNull.Value;
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
					dataRow["TaxOption"] = (byte)taxOption;
					dataRow["PriceIncludeTax"] = checkBoxPriceIncludeTax.Checked;
				}
				else
				{
					dataRow["TaxOption"] = PayeeTaxOptions.NonTaxable;
					dataRow["PriceIncludeTax"] = false;
				}
				dataRow["Discount"] = textBoxDiscountAmount.Text;
				dataRow["TaxAmount"] = textBoxTaxAmount.Text;
				dataRow["Total"] = textBoxTotal.Text;
				if (comboBoxjob.SelectedID != "")
				{
					dataRow["JobID"] = comboBoxjob.SelectedID;
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
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.PurchaseInvoiceTable.Rows.Add(dataRow);
				}
				foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
				{
					if (!currentData.PurchaseInvoiceDetailTable.Columns.Contains(column.Key))
					{
						currentData.PurchaseInvoiceDetailTable.Columns.Add(column.Key, column.DataType);
					}
				}
				currentData.PurchaseInvoiceDetailTable.Rows.Clear();
				currentData.Tables["Product_Lot_Receiving_Detail"].Rows.Clear();
				checked
				{
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						DataRow dataRow2 = currentData.PurchaseInvoiceDetailTable.NewRow();
						dataRow2.BeginEdit();
						dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
						dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
						if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
						{
							dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
						}
						if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("POSysDocID"))
						{
							if (row.Cells["POSysDocID"].Value.ToString() != "")
							{
								dataRow2["OrderSysDocID"] = row.Cells["POSysDocID"].Value.ToString();
							}
							if (row.Cells["POVoucherID"].Value.ToString() != "")
							{
								dataRow2["OrderVoucherID"] = row.Cells["POVoucherID"].Value.ToString();
							}
							if (row.Cells["PORowIndex"].Value.ToString() != "")
							{
								dataRow2["OrderRowIndex"] = row.Cells["PORowIndex"].Value.ToString();
							}
							if (row.Cells.Exists("RowDocType") && row.Cells["RowDocType"].Value.ToString() != "")
							{
								dataRow2["IsPORRow"] = true;
							}
						}
						if (row.Cells["Job"].Value != null && row.Cells["Job"].Value.ToString() != "")
						{
							dataRow2["JobID"] = row.Cells["Job"].Value.ToString();
						}
						else
						{
							dataRow2["JobID"] = DBNull.Value;
						}
						if (row.Cells["AttributeID1"].Value != null && row.Cells["AttributeID1"].Value.ToString() != "")
						{
							dataRow2["AttributeID1"] = row.Cells["AttributeID1"].Value.ToString();
						}
						else
						{
							dataRow2["AttributeID1"] = DBNull.Value;
						}
						if (row.Cells["AttributeID2"].Value != null && row.Cells["AttributeID2"].Value.ToString() != "")
						{
							dataRow2["AttributeID2"] = row.Cells["AttributeID2"].Value.ToString();
						}
						else
						{
							dataRow2["AttributeID2"] = DBNull.Value;
						}
						dataRow2["Attribute1"] = row.Cells["Attribute1"].Value.ToString();
						dataRow2["Attribute2"] = row.Cells["Attribute2"].Value.ToString();
						dataRow2["Attribute3"] = row.Cells["Attribute3"].Value.ToString();
						dataRow2["MatrixParentID"] = row.Cells["MatrixParentID"].Value.ToString();
						dataRow2["RowSource"] = (byte)sourceDocType;
						dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
						if (row.Cells["LCost"].Value != null && row.Cells["LCost"].Value.ToString() != "")
						{
							dataRow2["LCost"] = row.Cells["LCost"].Value.ToString();
							dataRow2["LCostAmount"] = row.Cells["LCostAmount"].Value.ToString();
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
						dataRow2["UnitPrice"] = row.Cells["Price"].Value.ToString();
						dataRow2["Description"] = row.Cells["Description"].Value.ToString();
						dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
						dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
						dataRow2["RowIndex"] = row.Index;
						if (row.Cells["ISPORRow"].Value != null && row.Cells["ISPORRow"].Value.ToString() != "")
						{
							dataRow2["IsPORRow"] = row.Cells["ISPORRow"].Value.ToString();
						}
						if (row.Cells["RowDocType"].Value != null && row.Cells["RowDocType"].Value.ToString() != "")
						{
							dataRow2["RowSource"] = row.Cells["RowDocType"].Value.ToString();
						}
						dataRow2["AnalysisID"] = row.Cells["Analysis"].Value.ToString();
						dataRow2.EndEdit();
						currentData.PurchaseInvoiceDetailTable.Rows.Add(dataRow2);
						if (row.Cells["Quantity"].Tag != null)
						{
							foreach (DataRow row2 in (row.Cells["Quantity"].Tag as DataTable).Rows)
							{
								DataRow dataRow4 = currentData.Tables["Product_Lot_Receiving_Detail"].NewRow();
								dataRow4["ProductID"] = row2["ProductID"];
								dataRow4["LocationID"] = row2["LocationID"];
								dataRow4["LotNumber"] = row2["LotNumber"];
								dataRow4["BinID"] = row2["BinID"];
								dataRow4["ProductionDate"] = row2["ProductionDate"];
								dataRow4["ExpiryDate"] = row2["ExpiryDate"];
								dataRow4["LotQty"] = row2["LotQty"];
								dataRow4["SysDocID"] = comboBoxSysDoc.SelectedID;
								dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
								dataRow4["RowIndex"] = row.Index;
								currentData.Tables["Product_Lot_Receiving_Detail"].Rows.Add(dataRow4);
							}
						}
					}
					currentData.InvoiceExpenseTable.Rows.Clear();
					foreach (UltraGridRow row3 in dataGridExpense.Rows)
					{
						DataRow dataRow5 = currentData.InvoiceExpenseTable.NewRow();
						dataRow5.BeginEdit();
						dataRow5["ExpenseID"] = row3.Cells["Expense Code"].Value.ToString();
						dataRow5["Description"] = row3.Cells["Description"].Value.ToString();
						dataRow5["Reference"] = row3.Cells["Ref"].Value.ToString();
						dataRow5["RowIndex"] = row3.Index;
						if (row3.Cells["Amount"].Value != null && row3.Cells["Amount"].Value.ToString() != "")
						{
							dataRow5["Amount"] = decimal.Parse(row3.Cells["Amount"].Value.ToString());
						}
						else
						{
							dataRow5["Amount"] = 0;
						}
						dataRow5["CurrencyID"] = row3.Cells["Currency"].Value.ToString();
						dataRow5["CurrencyRate"] = row3.Cells["Rate"].Value.ToString();
						dataRow5["RateType"] = row3.Cells["RateType"].Value.ToString();
						dataRow5.EndEdit();
						currentData.InvoiceExpenseTable.Rows.Add(dataRow5);
					}
					currentData.InvoiceGRNTable.Rows.Clear();
					if (sourceDocType == ItemSourceTypes.GRN)
					{
						foreach (object item in checkedListBoxPO.Items)
						{
							NameValue nameValue = item as NameValue;
							DataRow dataRow6 = currentData.InvoiceGRNTable.NewRow();
							dataRow6["GRNSysDocID"] = nameValue.ID;
							dataRow6["GRNNumber"] = nameValue.Name;
							dataRow6.EndEdit();
							currentData.InvoiceGRNTable.Rows.Add(dataRow6);
						}
					}
					if (grnTable != null)
					{
						if (grnTable.DataSet != null)
						{
							grnTable.DataSet.Tables.Remove(grnTable);
						}
						currentData.Tables.Add(grnTable);
					}
					currentData.Tables["Tax_Detail"].Rows.Clear();
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					int num = 0;
					foreach (UltraGridRow row4 in dataGridItems.Rows)
					{
						if (row4.Cells["Tax"].Tag != null)
						{
							TaxHelper.CreateTaxRows(currentData, row4.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Items, selectedID, text, num, comboBoxCurrency.SelectedID, comboBoxCurrency.Rate);
						}
						num++;
					}
					num = 0;
					foreach (UltraGridRow row5 in dataGridExpense.Rows)
					{
						if (row5.Cells["Tax"].Tag != null)
						{
							TaxHelper.CreateTaxRows(currentData, row5.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Expenses, selectedID, text, num, comboBoxCurrency.SelectedID, comboBoxCurrency.Rate);
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
				dataTable.Columns.Add("POSysDocID");
				dataTable.Columns.Add("POVoucherID");
				dataTable.Columns.Add("PORowIndex", typeof(int));
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("TaxGroupID");
				dataTable.Columns.Add("TaxOption", typeof(byte));
				dataTable.Columns.Add("Tax", typeof(decimal));
				dataTable.Columns.Add("Attribute1");
				dataTable.Columns.Add("Attribute2");
				dataTable.Columns.Add("Attribute3");
				dataTable.Columns.Add("Remarks");
				dataTable.Columns.Add("MatrixParentID");
				dataTable.Columns.Add("Job");
				dataTable.Columns.Add("AttributeID1");
				dataTable.Columns.Add("AttributeID2");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("Analysis");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("LCost", typeof(decimal));
				dataTable.Columns.Add("LCostAmount", typeof(decimal));
				dataTable.Columns.Add("IsTrackLot");
				dataTable.Columns.Add("IsTrackSerial");
				dataTable.Columns.Add("ItemType");
				dataTable.Columns.Add("ISPORRow", typeof(bool));
				dataTable.Columns.Add("RowDocType", typeof(byte));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("Weight", typeof(decimal));
				dataTable.Columns.Add("TaxTotal", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].Hidden = true;
				bool flag4 = ultraGridColumn5.Hidden = flag2;
				bool flag6 = ultraGridColumn4.Hidden = flag4;
				bool flag8 = ultraGridColumn3.Hidden = flag6;
				bool hidden = ultraGridColumn2.Hidden = flag8;
				ultraGridColumn.Hidden = hidden;
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Hidden = true);
				ultraGridColumn6.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Hidden = true;
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Hidden = true);
				ultraGridColumn7.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Hidden = !useJobCosting;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Width = checked(50 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(10 * dataGridItems.Width) / 100;
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn10 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				Activation activation2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].CellActivation = Activation.Disabled;
				Activation activation4 = ultraGridColumn10.CellActivation = activation2;
				Activation activation7 = ultraGridColumn8.CellActivation = (ultraGridColumn9.CellActivation = activation4);
				dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["POSysDocID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["POVoucherID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["PORowIndex"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["ISPORRow"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowDocType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Weight"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				UltraGridColumn ultraGridColumn11 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
				UltraGridColumn ultraGridColumn12 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"];
				flag8 = (dataGridItems.DisplayLayout.Bands[0].Columns["Weight"].Hidden = true);
				hidden = (ultraGridColumn12.Hidden = flag8);
				ultraGridColumn11.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ValueList = comboBoxgridJob;
				UltraGridColumn ultraGridColumn13 = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Hidden = !useProperty);
				ultraGridColumn13.Hidden = hidden;
				if (!useProperty)
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
				}
				else
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False);
				}
				if (useJobCosting)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				}
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance;
				AppearanceBase cellAppearance4 = dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance;
				Color color = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				Color color3 = cellAppearance4.BackColorDisabled = color;
				Color color5 = cellAppearance3.BackColorDisabled = color3;
				Color color8 = cellAppearance.BackColorDisabled = (cellAppearance2.BackColorDisabled = color5);
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCost"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCost"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["LCost"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCost"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Analysis"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Analysis"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Analysis"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Analysis"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Analysis"].ValueList = comboBoxAnalysis;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].Header.Caption = attributeID1Title;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].Width = 75;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ValueList = comboBoxProperty;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Header.Caption = attributeID2Title;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Width = 75;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].ValueList = comboBoxPropertyUnit;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowDocType"].Hidden = true;
				if (CompanyPreferences.ShowLandingCostCalculation)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["LCost"].Hidden = false;
					dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"].Hidden = false;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["LCost"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"].Hidden = true;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["LCost"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCost"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCost"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"].CellAppearance.ForeColorDisabled = Color.Black;
				AdjustGridColumnSettings();
				if (!CompanyPreferences.AllowLandingCostForLocalPurchase)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["LCost"].Hidden = true;
					tabPageExpense.Tab.Visible = false;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].Header.Caption = CompanyPreferences.Attribute1Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].Header.Caption = CompanyPreferences.Attribute2Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].Header.Caption = CompanyPreferences.Attribute3Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].Header.Caption = "Tax";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].Header.Caption = "Tax Group";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].Header.Caption = "Net Amount";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxTotal"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 3000;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
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
				dataTable.Columns.Add("Currency");
				dataTable.Columns.Add("RateType");
				dataTable.Columns.Add("Rate", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("AmountLC", typeof(decimal));
				dataGridExpense.DataSource = dataTable;
				dataGridExpense.DisplayLayout.Bands[0].Columns["RateType"].Hidden = true;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].MaxLength = 64;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].ValueList = comboBoxGridExpenseCode;
				dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].Header.Caption = "Amount (" + Global.BaseCurrencyID + ")";
				dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].CellActivation = Activation.Disabled;
				dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].Format = Format.GridAmountFormat;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Currency"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Currency"].ValueList = comboBoxGridCurrency;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 64;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].MaxLength = 20;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Rate"].MinValue = 0;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Rate"].MaxValue = new decimal(999999999999L);
				dataGridExpense.DisplayLayout.Bands[0].Columns["Rate"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Rate"].Format = "#,##0.#####";
				dataGridExpense.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
				dataGridExpense.DisplayLayout.Bands[0].Columns["TaxGroupID"].Hidden = true;
				Color color2 = dataGridExpense.DisplayLayout.Bands[0].Columns["TaxOption"].CellAppearance.BackColorDisabled = (dataGridExpense.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColorDisabled = Color.WhiteSmoke);
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].TabStop = false;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellActivation = Activation.NoEdit;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].Width = 100;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].Width = 100;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Width = 70;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Currency"].Width = 70;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Rate"].Width = 70;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].Width = 250;
			}
			catch (Exception e)
			{
				dataGridExpense.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void CalculateLandingCost()
		{
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal.TryParse(textBoxTotalExpense.Text, out result);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!(row.Cells["Item Code"].Value.ToString() == ""))
				{
					ItemTypes itemTypes = ItemTypes.Inventory;
					if (row.Cells["ItemType"].Value.ToString() != "")
					{
						itemTypes = (ItemTypes)checked((byte)int.Parse(row.Cells["ItemType"].Value.ToString()));
					}
					if (itemTypes == ItemTypes.Inventory)
					{
						decimal result2 = default(decimal);
						decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result2);
						num += result2;
					}
				}
			}
			decimal d = default(decimal);
			if (num > 0m)
			{
				d = result / num;
			}
			decimal d2 = default(decimal);
			int num2 = 0;
			foreach (UltraGridRow row2 in dataGridItems.Rows)
			{
				ItemTypes itemTypes2 = ItemTypes.Inventory;
				if (row2.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes2 = (ItemTypes)checked((byte)int.Parse(row2.Cells["ItemType"].Value.ToString()));
				}
				if (itemTypes2 == ItemTypes.Inventory)
				{
					num2 = checked(num2 + 1);
				}
			}
			int num3 = 1;
			foreach (UltraGridRow row3 in dataGridItems.Rows)
			{
				decimal d3 = default(decimal);
				decimal result3 = default(decimal);
				if (row3.Cells["Amount"].Value.ToString() != "")
				{
					d3 = decimal.Parse(row3.Cells["Amount"].Value.ToString());
				}
				if (row3.Cells["Quantity"].Value.ToString() != "")
				{
					decimal.TryParse(row3.Cells["Quantity"].Value.ToString(), out result3);
				}
				decimal num4 = default(decimal);
				ItemTypes itemTypes3 = ItemTypes.Inventory;
				if (row3.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes3 = (ItemTypes)checked((byte)int.Parse(row3.Cells["ItemType"].Value.ToString()));
				}
				if (itemTypes3 == ItemTypes.Inventory)
				{
					num4 = ((num3 != num2) ? (d3 * d) : (result - d2));
					num4 = Math.Round(num4, Global.CurDecimalPoints);
					d2 += num4;
					row3.Cells["LCostAmount"].Value = Math.Round(num4, Global.CurDecimalPoints);
					if (result3 > 0m)
					{
						num4 /= result3;
					}
					else
					{
						num4 = default(decimal);
					}
					row3.Cells["LCost"].Value = Math.Round(num4, 5);
					num3 = checked(num3 + 1);
				}
			}
		}

		private void CalculateLandingCostByWeight()
		{
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal.TryParse(textBoxTotalExpense.Text, out result);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!(row.Cells["Item Code"].Value.ToString() == ""))
				{
					ItemTypes itemTypes = ItemTypes.Inventory;
					if (row.Cells["ItemType"].Value.ToString() != "")
					{
						itemTypes = (ItemTypes)checked((byte)int.Parse(row.Cells["ItemType"].Value.ToString()));
					}
					if (itemTypes == ItemTypes.Inventory)
					{
						decimal result2 = default(decimal);
						decimal result3 = default(decimal);
						decimal.TryParse(row.Cells["Weight"].Value.ToString(), out result2);
						decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out result3);
						num += result2 * result3;
					}
				}
			}
			decimal d = default(decimal);
			if (num > 0m)
			{
				d = result / num;
			}
			decimal d2 = default(decimal);
			int num2 = 0;
			foreach (UltraGridRow row2 in dataGridItems.Rows)
			{
				ItemTypes itemTypes2 = ItemTypes.Inventory;
				if (row2.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes2 = (ItemTypes)checked((byte)int.Parse(row2.Cells["ItemType"].Value.ToString()));
				}
				if (itemTypes2 == ItemTypes.Inventory)
				{
					num2 = checked(num2 + 1);
				}
			}
			int num3 = 1;
			foreach (UltraGridRow row3 in dataGridItems.Rows)
			{
				decimal num4 = default(decimal);
				decimal d3 = default(decimal);
				decimal result4 = default(decimal);
				if (row3.Cells["Weight"].Value.ToString() != "")
				{
					d3 = decimal.Parse(row3.Cells["Weight"].Value.ToString());
				}
				if (row3.Cells["Quantity"].Value.ToString() != "")
				{
					decimal.TryParse(row3.Cells["Quantity"].Value.ToString(), out result4);
				}
				num4 = result4 * d3;
				decimal num5 = default(decimal);
				ItemTypes itemTypes3 = ItemTypes.Inventory;
				if (row3.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes3 = (ItemTypes)checked((byte)int.Parse(row3.Cells["ItemType"].Value.ToString()));
				}
				if (itemTypes3 == ItemTypes.Inventory)
				{
					num5 = ((num3 != num2) ? (num4 * d) : (result - d2));
					num5 = Math.Round(num5, Global.CurDecimalPoints);
					d2 += num5;
					row3.Cells["LCostAmount"].Value = Math.Round(num5, Global.CurDecimalPoints);
					if (result4 > 0m)
					{
						num5 /= result4;
					}
					else
					{
						num5 = default(decimal);
					}
					row3.Cells["LCost"].Value = Math.Round(num5, 5);
					num3 = checked(num3 + 1);
				}
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
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].CellAppearance.ForeColorDisabled = Color.Black;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					row.Cells["Item Code"].Activation = Activation.Disabled;
					row.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Description"].Activation = Activation.Disabled;
					row.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Unit"].Activation = Activation.Disabled;
					row.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
				}
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColorDisabled = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.ForeColorDisabled = Color.Black;
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
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("POSysDocID"))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["POSysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["POVoucherID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["PORowIndex"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowDocType"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ISPORRow"].Hidden = true;
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "#,0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 0;
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
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
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
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Received"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["Received"]);
			}
			if (!dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("LCost"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("LCost", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["LCostAmount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["LCost"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["LCost"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["LCost"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["LCost"].DisplayFormat = "{0:n}";
			}
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Ordered"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Ordered", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Received", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Received"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Received"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Received"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Received"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Received"].DisplayFormat = "{0:n}";
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeInventoryLocation))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.Disabled;
				comboBoxGridLocation.ReadOnly = true;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			comboBoxTaxOption.SelectedIndex = -1;
			comboBoxVendor.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.PurchaseInvoiceNISystem.GetPurchaseInvoiceByID(SystemDocID, voucherID);
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
					DataRow dataRow = currentData.Tables["Purchase_Invoice_NonInv"].Rows[0];
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					if (dataRow["DueDate"] != DBNull.Value)
					{
						dateTimePickerDueDate.Value = DateTime.Parse(dataRow["DueDate"].ToString());
					}
					else
					{
						dateTimePickerDueDate.Value = dateTimePickerDate.Value;
					}
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxRef2.Text = dataRow["Reference2"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
					if (dataRow["BOLNo"] != DBNull.Value)
					{
						textBoxBOLNo.Text = dataRow["BOLNo"].ToString();
					}
					else
					{
						textBoxBOLNo.Text = "";
					}
					if (dataRow["CurrencyRate"] != DBNull.Value)
					{
						comboBoxCurrency.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
					}
					else
					{
						comboBoxCurrency.Rate = 1m;
					}
					textBoxSupplierInvoiceNo.Text = dataRow["SupplierInvoiceNumber"].ToString();
					if (dataRow["TaxOption"] != DBNull.Value)
					{
						taxOption = (PayeeTaxOptions)checked((byte)int.Parse(dataRow["TaxOption"].ToString()));
					}
					else
					{
						taxOption = PayeeTaxOptions.NonTaxable;
					}
					if (!string.IsNullOrEmpty(dataRow["TaxOption"].ToString()))
					{
						comboBoxTaxOption.SelectedIndex = checked(int.Parse(dataRow["TaxOption"].ToString()) - 1);
					}
					else
					{
						comboBoxTaxOption.SelectedIndex = -1;
					}
					if (dataRow["PriceIncludeTax"] != DBNull.Value && CompanyPreferences.IsTax)
					{
						checkBoxPriceIncludeTax.Checked = bool.Parse(dataRow["PriceIncludeTax"].ToString());
					}
					else
					{
						checkBoxPriceIncludeTax.Checked = false;
					}
					bool flag = false;
					if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != Global.BaseCurrencyID)
					{
						flag = true;
					}
					if (dataRow["SourceDocType"] != DBNull.Value)
					{
						sourceDocType = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
					}
					else
					{
						sourceDocType = ItemSourceTypes.None;
					}
					comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
					comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
					comboBoxBuyer.SelectedID = dataRow["BuyerID"].ToString();
					comboBoxjob.SelectedID = dataRow["JobID"].ToString();
					comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
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
					else
					{
						textBoxTaxAmount.Text = decimal.Parse(dataRow["TaxAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (dataRow["TotalFC"] != DBNull.Value)
					{
						textBoxTotal.Text = decimal.Parse(dataRow["TotalFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTotal.Text = decimal.Parse(dataRow["Total"].ToString()).ToString(Format.TotalAmountFormat);
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Purchase_Invoice_NonInv_Detail") && currentData.PurchaseInvoiceDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Purchase_Invoice_NonInv_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							bool result = false;
							if (row["IsPORRow"] != DBNull.Value && bool.TryParse(row["IsPORRow"].ToString(), out result))
							{
								dataRow3["IsPORRow"] = row["IsPORRow"];
							}
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
							dataRow3["Weight"] = row["Weight"];
							dataRow3["ItemType"] = row["ItemType"];
							dataRow3["Description"] = row["Description"];
							dataRow3["Location"] = row["LocationID"];
							dataRow3["Unit"] = row["UnitID"];
							dataRow3["Remarks"] = row["Remarks"];
							dataRow3["TaxOption"] = row["TaxOption"];
							dataRow3["Analysis"] = row["AnalysisID"];
							if (row["JobID"] != DBNull.Value)
							{
								dataRow3["Job"] = row["JobID"];
							}
							if (row["AttributeID1"] != DBNull.Value)
							{
								dataRow3["AttributeID1"] = row["AttributeID1"];
							}
							if (row["AttributeID2"] != DBNull.Value)
							{
								dataRow3["AttributeID2"] = row["AttributeID2"];
							}
							if (flag)
							{
								dataRow3["Price"] = decimal.Parse(row["UnitPriceFC"].ToString()).ToString(Format.UnitPriceFormat);
							}
							else
							{
								dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
							}
							decimal result2 = default(decimal);
							decimal result3 = default(decimal);
							decimal.TryParse(dataRow3["Quantity"].ToString(), out result2);
							decimal.TryParse(dataRow3["Price"].ToString(), out result3);
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
							if (flag)
							{
								dataRow3["Amount"] = Math.Round(decimal.Parse(row["AmountFC"].ToString()), Global.CurDecimalPoints);
							}
							else
							{
								dataRow3["Amount"] = Math.Round(decimal.Parse(row["Amount"].ToString()), Global.CurDecimalPoints);
							}
							dataRow3["POSysDocID"] = row["OrderSysDocID"];
							dataRow3["POVoucherID"] = row["OrderVoucherID"];
							dataRow3["PORowIndex"] = row["OrderRowIndex"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						dataTable = (dataGridExpense.DataSource as DataTable);
						dataTable.Clear();
						textBoxTotalExpense.Text = 0.ToString(Format.TotalAmountFormat);
						foreach (DataRow row2 in currentData.Tables["Purchase_Invoice_NonInv_Expense"].Rows)
						{
							DataRow dataRow5 = dataTable.NewRow();
							bool flag2 = false;
							dataRow5["Expense Code"] = row2["ExpenseID"];
							dataRow5["Description"] = row2["Description"];
							dataRow5["Currency"] = row2["CurrencyID"];
							dataRow5["Ref"] = row2["Reference"];
							dataRow5["Rate"] = row2["CurrencyRate"];
							dataRow5["RateType"] = row2["RateType"];
							if (row2["CurrencyID"].ToString() != "" && row2["CurrencyID"].ToString() != Global.BaseCurrencyID)
							{
								flag2 = true;
							}
							dataRow5["Rate"] = row2["CurrencyRate"];
							if (row2["TaxOption"] != DBNull.Value)
							{
								dataRow5["TaxOption"] = byte.Parse(row2["TaxOption"].ToString());
							}
							else
							{
								dataRow5["TaxOption"] = ItemTaxOptions.NonTaxable;
							}
							if (row2["TaxAmount"] != DBNull.Value)
							{
								dataRow5["Tax"] = decimal.Parse(row2["TaxAmount"].ToString()).ToString(Format.UnitPriceFormat);
							}
							if (!row2["TaxGroupID"].IsDBNullOrEmpty())
							{
								dataRow5["TaxGroupID"] = row2["TaxGroupID"];
							}
							if (flag2)
							{
								dataRow5["Amount"] = decimal.Parse(row2["AmountFC"].ToString()).ToString(Format.TotalAmountFormat);
							}
							else
							{
								dataRow5["Amount"] = decimal.Parse(row2["Amount"].ToString()).ToString(Format.TotalAmountFormat);
							}
							dataRow5.EndEdit();
							dataTable.Rows.Add(dataRow5);
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
						foreach (UltraGridRow row3 in dataGridItems.Rows)
						{
							if (row3.Cells["ISPORRow"].Value != null && row3.Cells["ISPORRow"].Value.ToString() != "" && bool.Parse(row3.Cells["ISPORRow"].Value.ToString()))
							{
								row3.Cells["Item Code"].Activation = Activation.Disabled;
								row3.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row3.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
								row3.Cells["Description"].Activation = Activation.Disabled;
								row3.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row3.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
								row3.Cells["Location"].Activation = Activation.Disabled;
								row3.Cells["Location"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row3.Cells["Location"].Appearance.ForeColorDisabled = Color.Black;
								row3.Cells["Unit"].Activation = Activation.Disabled;
								row3.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row3.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
								row3.Cells["Quantity"].Activation = Activation.Disabled;
								row3.Cells["Quantity"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row3.Cells["Quantity"].Appearance.ForeColorDisabled = Color.Black;
							}
							row3.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(row3.Cells["Item Code"].Value.ToString());
							if (row3.Cells["IsTrackLot"].Value != DBNull.Value && bool.Parse(row3.Cells["IsTrackLot"].Value.ToString()))
							{
								DataRow[] array = currentData.Tables["Product_Lot_Receiving_Detail"].Select("ProductID = '" + row3.Cells["Item Code"].Value.ToString() + "' AND RowIndex = " + row3.Index);
								if (array.Length != 0)
								{
									DataSet dataSet = new DataSet();
									dataSet.Merge(array);
									DataTable tag = dataSet.Tables[0];
									row3.Cells["Quantity"].Tag = tag;
									row3.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
								}
							}
							string productID = row3.Cells["Item Code"].Value.ToString();
							row3.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
							DataRow[] array2 = currentData.TaxDetailsTable.Select("RowIndex = " + row3.Index + " AND TaxLevel = " + (byte)2);
							if (array2.Length != 0)
							{
								TaxTransactionData taxTransactionData = new TaxTransactionData();
								taxTransactionData.Merge(array2);
								row3.Cells["Tax"].Tag = taxTransactionData;
							}
							if (checked((byte)int.Parse(row3.Cells["ItemType"].Value.ToString())) == 4)
							{
								row3.Cells["Quantity"].Activation = Activation.Disabled;
							}
						}
						foreach (UltraGridRow row4 in dataGridExpense.Rows)
						{
							DataRow[] array3 = currentData.TaxDetailsTable.Select("RowIndex = " + row4.Index + " AND TaxLevel = " + (byte)3);
							if (array3.Length != 0)
							{
								TaxTransactionData taxTransactionData2 = new TaxTransactionData();
								taxTransactionData2.Merge(array3);
								row4.Cells["Tax"].Tag = taxTransactionData2;
							}
						}
						foreach (UltraGridRow row5 in dataGridExpense.Rows)
						{
							SetRowLCAmount(row5);
						}
						checkedListBoxPO.Items.Clear();
						if (currentData.Tables.Contains("Purchase_Order_NonInv_Detail"))
						{
							foreach (DataRow row6 in currentData.Tables["Purchase_Order_NonInv_Detail"].Rows)
							{
								NameValue nameValue = new NameValue(row6["VoucherID"].ToString(), row6["SysDocID"].ToString());
								if (!checkedListBoxPO.Items.Contains(nameValue))
								{
									checkedListBoxPO.Items.Add(nameValue);
								}
							}
						}
						if (checkedListBoxPO.Items.Count > 0)
						{
							checkedListBoxPO.Visible = true;
							checkedListBoxPO.Visible = true;
							labelSelectedDocs.Visible = true;
						}
						else
						{
							checkedListBoxPO.Visible = false;
							checkedListBoxPO.Visible = false;
							labelSelectedDocs.Visible = false;
						}
						DataRow[] array4 = currentData.TaxDetailsTable.Select("RowIndex = -1 AND TaxLevel = " + (byte)1);
						if (array4.Length != 0)
						{
							TaxTransactionData taxTransactionData3 = new TaxTransactionData();
							taxTransactionData3.Merge(array4);
							textBoxTaxAmount.Tag = taxTransactionData3;
						}
						CalculateTotal();
						CalculateTotalExpense();
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
				bool flag2 = Factory.PurchaseInvoiceNISystem.CreatePurchaseInvoice(currentData, !isNewRecord);
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
			formManager.ShowApprovalPanel(approvalTaskID, "Purchase_Invoice_NonInv", "VoucherID");
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
			if (!isNewRecord && Factory.ProductSystem.DocumentHasUsedLots(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("This document cannot be modified because some items are refered by other transactions.");
				return false;
			}
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Purchase_Invoice_NonInv", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to (SecurityRoleID:116).");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			if (RestrictTransaction)
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
				return false;
			}
			string text = Factory.SystemDocumentSystem.GetSystemDocumentByID(comboBoxSysDoc.SelectedID).Tables[0].Rows[0]["IsSupplierInvoiceNoMandatory"].ToString();
			bool flag = false;
			if (text != "" && text != null)
			{
				flag = bool.Parse(text);
			}
			if (flag && textBoxSupplierInvoiceNo.Text == "")
			{
				ErrorHelper.InformationMessage(" Vendor Reference #: is Mandatory");
				return false;
			}
			if (!IsNewRecord)
			{
				DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(currentData, SysDocTypes.PurchaseInvoiceNI, Global.CurrentUser, includeApproveUser: false);
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
			bool flag2 = false;
			checked
			{
				if (timeSpan.Days <= num + 1)
				{
					flag2 = true;
				}
				else if (Global.isUserAdmin)
				{
					flag2 = true;
				}
				else if (num == 0)
				{
					flag2 = true;
				}
				if (isNewRecord && dateTimePickerDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
					return false;
				}
				if (!flag2)
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions not more than " + num + " days.");
					return false;
				}
				if (isNewRecord && dateTimePickerDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
					return false;
				}
				if (comboBoxVendor.SelectedID.Trim() != "" && textBoxSupplierInvoiceNo.Text.Trim() != "" && Factory.PurchaseInvoiceNISystem.IsAlreadyExisting(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, comboBoxVendor.SelectedID, textBoxSupplierInvoiceNo.Text.Trim()))
				{
					ErrorHelper.InformationMessage("Vendor and Supplier Doc no already exist. Please enter another number.");
					textBoxSupplierInvoiceNo.Focus();
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxVendor.SelectedID == "")
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
				int num2 = 0;
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
					}
					UltraGridRow ultraGridRow = dataGridItems.Rows[i];
					decimal d = decimal.Parse(ultraGridRow.Cells["Quantity"].Value.ToString());
					decimal d2 = decimal.Parse(ultraGridRow.Cells["Price"].Value.ToString());
					decimal num3 = decimal.Parse(ultraGridRow.Cells["Amount"].Value.ToString());
					if (Math.Abs(Math.Round(d * d2, Global.CurDecimalPoints) - num3) > 0.1m)
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
				foreach (UltraGridRow row in dataGridExpense.Rows)
				{
					if (row.Cells["Expense Code"].Value == null || !(row.Cells["Expense Code"].Value.ToString() != string.Empty))
					{
						ErrorHelper.InformationMessage("Please select an expense code for a row");
						return false;
					}
				}
				decimal d3 = default(decimal);
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					if (row2.Cells["LCostAmount"] != null && !(row2.Cells["LCostAmount"].Value.ToString() == ""))
					{
						d3 += decimal.Parse(row2.Cells["LCostAmount"].Value.ToString());
					}
				}
				if (decimal.Parse(textBoxTotalExpense.Text) != d3)
				{
					ErrorHelper.WarningMessage("Total landing cost is not must equal to total allocated Landing Cost. Some of landing costs are not allocated to items.\nPlease allocate all landing cost amount.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Purchase_Invoice_NonInv", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				textBoxRef2.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				textBoxVendorName.Clear();
				comboBoxVendor.Clear();
				comboBoxBuyer.Clear();
				comboBoxShippingMethod.Clear();
				comboBoxTerm.Clear();
				textBoxTotalExpense.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxSubtotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscountAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscountPercent.Text = "0";
				isDiscountPercent = false;
				isDiscountPercent = false;
				comboBoxVendor.Enabled = true;
				comboBoxPayeeTaxGroup.Clear();
				checkedListBoxPO.Items.Clear();
				textBoxBOLNo.Clear();
				comboBoxjob.Clear();
				comboBoxCostCategory.Clear();
				textBoxSupplierInvoiceNo.Clear();
				comboBoxTaxOption.SelectedIndex = -1;
				DataTable dataTable = dataGridExpense.DataSource as DataTable;
				dataTable.Rows.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				dataTable = (dataGridItems.DataSource as DataTable);
				if (dataTable.Columns.Contains("Ordered"))
				{
					dataTable.Columns.Remove("Ordered");
					dataTable.Columns.Remove("Received");
				}
				dataGridItems.DropDownMenu.Enabled = true;
				dataTable.Rows.Clear();
				grnTable = null;
				sourceDocType = ItemSourceTypes.None;
				IsVoid = false;
				formManager.ResetDirty();
				comboBoxVendor.Focus();
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
				if (Factory.ProductSystem.DocumentHasUsedLots(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("This transaction cannot be deleted because some items are refered by other transactions.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.PurchaseInvoiceNISystem.DeletePurchaseInvoice(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Purchase_Invoice_NonInv", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Purchase_Invoice_NonInv", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Purchase_Invoice_NonInv", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Purchase_Invoice_NonInv", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Purchase_Invoice_NonInv", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
			checked
			{
				try
				{
					SetupGrid();
					dataGridExpense.SetupUI();
					SetupExpenseGrid();
					checkBoxPriceIncludeTax.Visible = false;
					if (!CompanyPreferences.IsTax)
					{
						panelNonTax.Top -= 22;
					}
					UltraFormattedLinkLabel ultraFormattedLinkLabel = labelCurrency;
					bool visible = comboBoxCurrency.Visible = CompanyPreferences.UseMultiCurrency;
					ultraFormattedLinkLabel.Visible = visible;
					labelTaxGroup.Visible = CompanyPreferences.IsTax;
					comboBoxPayeeTaxGroup.Visible = CompanyPreferences.IsTax;
					comboBoxTaxOption.Visible = CompanyPreferences.IsTax;
					comboBoxSysDoc.FilterByType(SysDocTypes.PurchaseInvoiceNI);
					SetSecurity();
					ultraTabControl1.Tabs[1].Visible = false;
					if (!base.IsDisposed)
					{
						IsNewRecord = true;
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
			userViewStaus = Security.IsAllowedSecurityRole(GeneralSecurityRoles.ViewItemdetails);
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransDiffLocation))
			{
				AllowEditTransDiffLocation = false;
			}
			else
			{
				AllowEditTransDiffLocation = true;
			}
			comboBoxPayeeTaxGroup.ReadOnly = !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
			checkBoxPriceIncludeTax.Enabled = Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
		}

		private void SetupForm()
		{
			checked
			{
				switch (0)
				{
				case 2:
				case 3:
					dataGridItems.AllowAddNew = false;
					createFromPurchaseOrderToolStripMenuItem.Visible = false;
					duplicateToolStripMenuItem.Enabled = false;
					buttonSelectDocument.Visible = true;
					textBoxVendorName.Width = textBoxVendorName.Width - buttonSelectDocument.Width - 3;
					textBoxNote.Width = 306;
					checkedListBoxPO.Visible = true;
					labelSelectedDocs.Visible = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.Disabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.Disabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.Disabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.Disabled;
					break;
				case 1:
					if (CompanyPreferences.AllowLocalGRNWithoutPO)
					{
						dataGridItems.AllowAddNew = true;
					}
					else
					{
						dataGridItems.AllowAddNew = false;
					}
					dataGridItems.ShowDeleteMenu = false;
					createFromItemReceiptToolStripMenuItem.Visible = false;
					buttonSelectDocument.Visible = true;
					duplicateToolStripMenuItem.Enabled = false;
					textBoxVendorName.Width = textBoxVendorName.Width - buttonSelectDocument.Width - 3;
					checkedListBoxPO.Visible = false;
					labelSelectedDocs.Visible = false;
					if (!CompanyPreferences.AllowLocalGRNAddNewRow && !CompanyPreferences.AllowLocalGRNWithoutPO)
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
						dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.Disabled;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
						dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.Disabled;
					}
					break;
				case 0:
					buttonSelectDocument.Visible = false;
					checkedListBoxPO.Visible = false;
					labelSelectedDocs.Visible = false;
					break;
				}
			}
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow == null || dataGridItems.ActiveCell == null)
			{
				return;
			}
			if (dataGridItems.ActiveCell.Column.Key == "Analysis")
			{
				string text = "jfk23lf";
				if (dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
				{
					string text2 = "";
					text2 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					text = Factory.DatabaseSystem.GetFieldValue("Product", "COGSAccount", "ProductID", text2).ToString();
					comboBoxAnalysis.FilterByAccount(text);
				}
			}
			comboBoxGridItem.IsLoadingData = false;
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
				return Factory.PurchaseInvoiceNISystem.VoidPurchaseInvoice(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.PurchaseInvoiceNI);
		}

		private void CalculateTotalExpense()
		{
			decimal num = default(decimal);
			foreach (UltraGridRow row in dataGridExpense.Rows)
			{
				decimal result = default(decimal);
				if (row.Cells["AmountLC"].Value != null && !(row.Cells["AmountLC"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["AmountLC"].Value.ToString(), out result);
					num += result;
				}
			}
			textBoxTotalExpense.Text = num.ToString(Format.TotalAmountFormat);
			if (!purchaseLandingCostCalculationMethod)
			{
				CalculateLandingCostByWeight();
			}
			else
			{
				CalculateLandingCost();
			}
		}

		private void CalculateTotal()
		{
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal result3 = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				decimal result4 = default(decimal);
				if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result4);
					num += result4;
					if (!row.Cells["Tax"].Value.IsNullOrEmpty())
					{
						decimal.TryParse(row.Cells["Tax"].Value.ToString(), out result3);
						row.Cells["TaxTotal"].Value = result4 + result3;
					}
					if (!row.Cells["Tax"].Value.IsNullOrEmpty())
					{
						num2 += decimal.Parse(row.Cells["Tax"].Value.ToString());
					}
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
			num3 = num - result;
			textBoxTaxAmount.Text = num2.ToString(Format.TotalAmountFormat);
			if (taxOption != PayeeTaxOptions.ReverseCharge && !checkBoxPriceIncludeTax.Checked)
			{
				num3 += num2;
			}
			textBoxTotal.Text = num3.ToString(Format.TotalAmountFormat);
			if (!purchaseLandingCostCalculationMethod)
			{
				CalculateLandingCostByWeight();
			}
			else
			{
				CalculateLandingCost();
			}
			CalculateTotalTaxes();
		}

		private void CalculateAllRowsTaxes()
		{
			try
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					ItemTaxOptions itemTaxOptions = ItemTaxOptions.BasedOnCustomer;
					if (!row.Cells["TaxOption"].IsNullOrEmpty())
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
					TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxVendor.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOptions, row.Cells["TaxGroupID"].Value.ToString());
					row.Cells["Tax"].Tag = tag;
					UIGlobal.CalculateRowTax(row, "Tax", amount, subtotal, tradeDiscount);
				}
				foreach (UltraGridRow row2 in dataGridExpense.Rows)
				{
					ItemTaxOptions itemTaxOptions2 = ItemTaxOptions.NonTaxable;
					if (!row2.Cells["TaxOption"].IsNullOrEmpty())
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
					TaxTransactionData tag2 = TaxHelper.CreateTaxDetailData(comboBoxVendor.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOptions2, row2.Cells["TaxGroupID"].Value.ToString());
					row2.Cells["Tax"].Tag = tag2;
					UIGlobal.CalculateRowTax(row2, "Tax", amount2, subtotal2, tradeDiscount2);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CalculateTotalTaxes()
		{
			TaxTransactionData taxTransactionData = TaxHelper.CreateTaxDetailData(comboBoxVendor.TaxOption, comboBoxPayeeTaxGroup.SelectedID);
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
			num4 = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
			num = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			num5 = decimal.Parse(textBoxTaxAmount.Text, NumberStyles.Any);
			num2 = num + num5 - num4;
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
				num3 = Math.Round(num2 / (num + num5) * 100m, 2);
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

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (!(currentVendorAddressID == ""))
			{
				new FormHelper().EditVendorAddress(comboBoxVendor.SelectedID, currentVendorAddressID);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(comboBoxVendor.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBuyer(comboBoxBuyer.SelectedID);
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
					row.Cells["RowDocType"].Value = DBNull.Value;
				}
				grnTable = null;
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			if (dataGridItems.AllowAddNew)
			{
				duplicateToolStripMenuItem.Enabled = !IsNewRecord;
			}
			else
			{
				duplicateToolStripMenuItem.Enabled = false;
			}
		}

		private void createFromPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet openOrdersSummary = Factory.PurchaseOrderNISystem.GetOpenOrdersSummary(comboBoxVendor.SelectedID, isImport: false);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openOrdersSummary;
			selectDocumentDialog.Text = "Select Purchase Order";
			selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["JobName"].Hidden = !useJobCosting;
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			PurchaseOrderNIData purchaseOrderByID = Factory.PurchaseOrderNISystem.GetPurchaseOrderByID(text, text2);
			DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(purchaseOrderByID, SysDocTypes.PurchaseOrderNI, Global.CurrentUser, includeApproveUser: true);
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
			DataRow dataRow = purchaseOrderByID.PurchaseOrderTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			textBoxSupplierInvoiceNo.Text = dataRow["VendorReferenceNo"].ToString();
			if (comboBoxVendor.SelectedID == "")
			{
				comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
			}
			comboBoxVendor.Enabled = false;
			if (!string.IsNullOrEmpty(dataRow["CurrencyID"].ToString()))
			{
				comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
			{
				comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["TermID"].ToString()))
			{
				comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["BuyerID"].ToString()))
			{
				comboBoxBuyer.SelectedID = dataRow["BuyerID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["JobID"].ToString()))
			{
				comboBoxjob.SelectedID = dataRow["JobID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["CostCategoryID"].ToString()))
			{
				comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
			}
			textBoxBOLNo.Text = dataRow["BOLNo"].ToString();
			textBoxDiscountAmount.Text = decimal.Parse(dataRow["Discount"].ToString()).ToString(Format.TotalAmountFormat);
			comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
			sourceDocType = ItemSourceTypes.PurchaseOrder;
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if (!dataTable.Columns.Contains("Ordered"))
			{
				dataTable.Columns.Remove("Quantity");
				dataTable.Columns.Remove("Amount");
				dataTable.Columns.Remove("Price");
				dataTable.Columns.Add("Ordered", typeof(decimal));
				dataTable.Columns.Add("Received", typeof(decimal));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				if (!dataTable.Columns.Contains("POSysDocID"))
				{
					dataTable.Columns.Add("POSysDocID");
					dataTable.Columns.Add("POVoucherID");
					dataTable.Columns.Add("PORowIndex", typeof(int));
				}
			}
			dataTable.Rows.Clear();
			if (!purchaseOrderByID.Tables.Contains("Purchase_Order_NonInv_Detail") || purchaseOrderByID.PurchaseOrderDetailTable.Rows.Count == 0)
			{
				return;
			}
			foreach (DataRow row in purchaseOrderByID.Tables["Purchase_Order_NonInv_Detail"].Rows)
			{
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["Item Code"] = row["ProductID"];
				dataRow3["POSysDocID"] = text;
				dataRow3["POVoucherID"] = text2;
				dataRow3["PORowIndex"] = row["RowIndex"];
				if (row["UnitQuantity"] != DBNull.Value)
				{
					dataRow3["Quantity"] = row["UnitQuantity"];
				}
				else
				{
					dataRow3["Quantity"] = row["Quantity"];
				}
				dataRow3["IsTrackLot"] = row["IsTrackLot"];
				dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
				dataRow3["RowDocType"] = ItemSourceTypes.PurchaseOrder;
				dataRow3["ISPORRow"] = true;
				dataRow3["Attribute1"] = row["Attribute1"];
				dataRow3["Attribute2"] = row["Attribute2"];
				dataRow3["Attribute3"] = row["Attribute3"];
				dataRow3["MatrixParentID"] = row["MatrixParentID"];
				dataRow3["Description"] = row["Description"];
				dataRow3["TaxGroupID"] = row["TaxGroupID"];
				if (row["TaxOption"] != DBNull.Value)
				{
					dataRow3["TaxOption"] = byte.Parse(row["TaxOption"].ToString());
				}
				else
				{
					dataRow3["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
				}
				dataRow3["Job"] = row["JobID"];
				dataRow3["Unit"] = row["UnitID"];
				if (row["SubunitPrice"] != DBNull.Value)
				{
					dataRow3["Price"] = decimal.Parse(row["SubunitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				else
				{
					dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal.TryParse(dataRow3["Quantity"].ToString(), out result);
				decimal.TryParse(dataRow3["Price"].ToString(), out result2);
				decimal.TryParse(row["QuantityReceived"].ToString(), out result3);
				dataRow3["Ordered"] = result;
				dataRow3["Received"] = result3;
				result -= result3;
				dataRow3["Quantity"] = result;
				if (result < 0m)
				{
					result = default(decimal);
				}
				dataRow3["Amount"] = Math.Round(result * result2, Global.CurDecimalPoints);
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			foreach (UltraGridRow row2 in dataGridItems.Rows)
			{
				if (!allowPriceChange)
				{
					row2.Cells["Price"].Activation = Activation.Disabled;
				}
			}
			foreach (UltraGridRow row3 in dataGridItems.Rows)
			{
				bool result4 = false;
				bool result5 = false;
				if (row3.Cells["IsTrackLot"].Value != null)
				{
					bool.TryParse(row3.Cells["IsTrackLot"].Value.ToString(), out result4);
				}
				if (row3.Cells["IsTrackSerial"].Value != null)
				{
					bool.TryParse(row3.Cells["IsTrackSerial"].Value.ToString(), out result5);
				}
				if (result4)
				{
					row3.Cells["Quantity"].Appearance.ForeColor = Color.DarkRed;
					row3.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
				}
			}
			if (CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.POThenGRNThenInvoice || CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.POThenInvoice)
			{
				if (!CompanyPreferences.AllowLocalGRNAddNewRow)
				{
					dataGridItems.AllowAddNew = false;
				}
				else
				{
					dataGridItems.AllowAddNew = true;
				}
			}
			AdjustGridColumnSettings();
			dataGridItems.DropDownMenu.Enabled = false;
			CalculateAllRowsTaxes();
			CalculateTotal();
			textBoxDiscountPercent.Modified = true;
			textBoxDiscountAmount_Validated(sender, e);
		}

		private void numberTextBox1_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBoxTaxPercent_TextChanged(object sender, EventArgs e)
		{
		}

		private void createFromItemReceiptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					if (!IsNewRecord)
					{
						ErrorHelper.InformationMessage("Please start a new transaction first.");
					}
					else
					{
						DataSet uninvoicedGRNs = Factory.PurchaseReceiptSystem.GetUninvoicedGRNs(comboBoxVendor.SelectedID, isImport: false);
						SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
						selectDocumentDialog.DataSource = uninvoicedGRNs;
						selectDocumentDialog.Text = "Select GRNs";
						selectDocumentDialog.IsMultiSelect = true;
						selectDocumentDialog.ValidateSelection += form_ValidateSelection;
						if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
						{
							ClearForm();
							int num = 0;
							textBoxNote.Text = "GRNs:\r\n";
							grnTable = new DataTable("GRN");
							grnTable.Columns.Add("SysDocID");
							grnTable.Columns.Add("VoucherID");
							List<UltraGridRow> selectedRows = selectDocumentDialog.SelectedRows;
							foreach (UltraGridRow item2 in selectedRows)
							{
								string text = item2.Cells["Doc ID"].Value.ToString();
								string text2 = item2.Cells["Number"].Value.ToString();
								NameValue item = new NameValue(text2, text);
								checkedListBoxPO.Items.Add(item);
								grnTable.Rows.Add(text, text2);
								textBoxNote.Text += text2;
								if (num < selectedRows.Count - 1)
								{
									textBoxNote.Text += ",";
								}
								num++;
								PurchaseReceiptData purchaseReceiptByID = Factory.PurchaseReceiptSystem.GetPurchaseReceiptByID(text, text2);
								DataRow dataRow = purchaseReceiptByID.PurchaseReceiptTable.Rows[0];
								textBoxRef1.Text = dataRow["VoucherID"].ToString();
								textBoxNote.Text = dataRow["Note"].ToString();
								if (comboBoxVendor.SelectedID == "")
								{
									comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
								}
								comboBoxVendor.Enabled = false;
								if (!string.IsNullOrEmpty(dataRow["CurrencyID"].ToString()))
								{
									comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
								{
									comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow["TermID"].ToString()))
								{
									comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow["BuyerID"].ToString()))
								{
									comboBoxBuyer.SelectedID = dataRow["BuyerID"].ToString();
								}
								DataTable dataTable = dataGridItems.DataSource as DataTable;
								if (!dataTable.Columns.Contains("POSysDocID"))
								{
									dataTable.Columns.Add("POSysDocID");
									dataTable.Columns.Add("POVoucherID");
									dataTable.Columns.Add("PORowIndex", typeof(int));
								}
								if (!purchaseReceiptByID.Tables.Contains("Purchase_Receipt_Detail") || purchaseReceiptByID.PurchaseReceiptDetailTable.Rows.Count == 0)
								{
									break;
								}
								foreach (DataRow row in purchaseReceiptByID.Tables["Purchase_Receipt_Detail"].Rows)
								{
									decimal num2 = default(decimal);
									decimal result = default(decimal);
									DataRow dataRow3 = dataTable.NewRow();
									dataRow3["Item Code"] = row["ProductID"];
									dataRow3["POSysDocID"] = text;
									dataRow3["POVoucherID"] = text2;
									dataRow3["PORowIndex"] = row["RowIndex"];
									dataRow3["ItemType"] = row["ItemType"];
									dataRow3["RowDocType"] = ItemSourceTypes.GRN;
									dataRow3["IsPORRow"] = true;
									decimal num3 = default(decimal);
									num2 = ((row["UnitQuantity"] == DBNull.Value) ? decimal.Parse(row["Quantity"].ToString()) : decimal.Parse(row["UnitQuantity"].ToString()));
									if (row["QuantityReturned"].ToString() != "")
									{
										num3 = decimal.Parse(row["QuantityReturned"].ToString(), NumberStyles.Any);
									}
									num2 -= num3;
									dataRow3["Quantity"] = num2;
									dataRow3["TaxGroupID"] = row["TaxGroupID"];
									if (row["TaxOption"] != DBNull.Value)
									{
										dataRow3["TaxOption"] = byte.Parse(row["TaxOption"].ToString());
									}
									else
									{
										dataRow3["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
									}
									dataRow3["Description"] = row["Description"];
									dataRow3["Job"] = row["JobID"];
									dataRow3["Location"] = row["LocationID"];
									dataRow3["Unit"] = row["UnitID"];
									if (row["UnitPrice"] != DBNull.Value && decimal.Parse(row["UnitPrice"].ToString()) != 0m)
									{
										dataRow3["Price"] = row["UnitPrice"];
									}
									else
									{
										dataRow3["Price"] = Factory.ProductSystem.GetProductPurchasePrice(row["ProductID"].ToString());
									}
									decimal.TryParse(dataRow3["Quantity"].ToString(), out num2);
									decimal.TryParse(dataRow3["Price"].ToString(), out result);
									dataRow3["Price"] = row["UnitPrice"];
									if (num2 < 0m)
									{
										num2 = default(decimal);
									}
									dataRow3["Amount"] = Math.Round(num2 * result, Global.CurDecimalPoints);
									dataRow3.EndEdit();
									dataTable.Rows.Add(dataRow3);
								}
								if (CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.DirectInvoice)
								{
									AdjustGridColumnSettings();
								}
								CalculateAllRowsTaxes();
								CalculateTotal();
								textBoxDiscountPercent.Modified = true;
								textBoxDiscountAmount_Validated(sender, e);
								sourceDocType = ItemSourceTypes.GRN;
								dataGridItems.DropDownMenu.Enabled = false;
								foreach (UltraGridRow row2 in dataGridItems.Rows)
								{
									row2.Cells["Item Code"].Activation = Activation.Disabled;
									row2.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row2.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
									row2.Cells["Description"].Activation = Activation.Disabled;
									row2.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row2.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
									row2.Cells["Location"].Activation = Activation.Disabled;
									row2.Cells["Location"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row2.Cells["Location"].Appearance.ForeColorDisabled = Color.Black;
									row2.Cells["Unit"].Activation = Activation.Disabled;
									row2.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row2.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
									row2.Cells["Quantity"].Activation = Activation.Disabled;
									row2.Cells["Quantity"].Appearance.BackColorDisabled = Color.WhiteSmoke;
									row2.Cells["Quantity"].Appearance.ForeColorDisabled = Color.Black;
								}
							}
						}
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
			SelectDocumentDialog selectDocumentDialog = sender as SelectDocumentDialog;
			List<UltraGridRow> selectedRows = selectDocumentDialog.SelectedRows;
			if (selectedRows != null && selectedRows.Count != 0)
			{
				foreach (UltraGridRow item in selectedRows)
				{
					if (item.Cells["Vendor"].Value.ToString() != selectedRows[0].Cells["Vendor"].Value.ToString())
					{
						ErrorHelper.ErrorMessage("Only GRNs from same vendor can be selected together.");
						selectDocumentDialog.CanClose = false;
						break;
					}
				}
			}
		}

		private void createFromPOShipmentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet openShipmentsSummary = Factory.POShipmentSystem.GetOpenShipmentsSummary(comboBoxVendor.SelectedID);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openShipmentsSummary;
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.Text = "Select PO Shipments";
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			POShipmentData pOShipmentByID = Factory.POShipmentSystem.GetPOShipmentByID(text, text2);
			DataRow dataRow = pOShipmentByID.POShipmentTable.Rows[0];
			textBoxNote.Text = dataRow["Note"].ToString();
			if (comboBoxVendor.SelectedID == "")
			{
				comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
			}
			comboBoxVendor.Enabled = false;
			comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if (!dataTable.Columns.Contains("Ordered"))
			{
				dataTable.Columns.Remove("Quantity");
				dataTable.Columns.Remove("Amount");
				dataTable.Columns.Remove("Price");
				dataTable.Columns.Add("Ordered", typeof(decimal));
				dataTable.Columns.Add("Received", typeof(decimal));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				if (!dataTable.Columns.Contains("POSysDocID"))
				{
					dataTable.Columns.Add("POSysDocID");
					dataTable.Columns.Add("POVoucherID");
					dataTable.Columns.Add("PORowIndex", typeof(int));
				}
			}
			dataTable.Rows.Clear();
			if (pOShipmentByID.Tables.Contains("PO_Shipment_Detail") && pOShipmentByID.POShipmentDetailTable.Rows.Count != 0)
			{
				foreach (DataRow row in pOShipmentByID.Tables["PO_Shipment_Detail"].Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["Item Code"] = row["ProductID"];
					dataRow3["POSysDocID"] = text;
					dataRow3["POVoucherID"] = text2;
					dataRow3["PORowIndex"] = row["RowIndex"];
					if (row["UnitQuantity"] != DBNull.Value)
					{
						dataRow3["Quantity"] = row["UnitQuantity"];
					}
					else
					{
						dataRow3["Quantity"] = row["Quantity"];
					}
					dataRow3["Description"] = row["Description"];
					dataRow3["Unit"] = row["UnitID"];
					if (row["UnitPrice"] != DBNull.Value)
					{
						dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
					}
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(dataRow3["Quantity"].ToString(), out result);
					decimal.TryParse(dataRow3["Price"].ToString(), out result2);
					decimal.TryParse(row["QuantityReceived"].ToString(), out result3);
					dataRow3["Ordered"] = result;
					dataRow3["Received"] = result3;
					result -= result3;
					dataRow3["Quantity"] = result;
					if (result < 0m)
					{
						result = default(decimal);
					}
					dataRow3["Amount"] = Math.Round(result * result2, Global.CurDecimalPoints);
					dataRow3.EndEdit();
					dataTable.Rows.Add(dataRow3);
				}
				sourceDocType = ItemSourceTypes.PackingList;
				if (CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.DirectInvoice)
				{
					AdjustGridColumnSettings();
				}
				CalculateTotal();
				dataGridItems.DropDownMenu.Enabled = false;
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
					DataSet purchaseInvoiceToPrint = Factory.PurchaseInvoiceNISystem.GetPurchaseInvoiceToPrint(selectedID, text);
					if (purchaseInvoiceToPrint == null || purchaseInvoiceToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(purchaseInvoiceToPrint, selectedID, "Purchase Invoice NI", SysDocTypes.PurchaseInvoiceNI, isPrint, showPrintDialog);
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
			switch (CompanyPreferences.LocalPurchaseFlow)
			{
			case PurchaseFlows.GRNThenInvoice:
			case PurchaseFlows.POThenGRNThenInvoice:
				createFromItemReceiptToolStripMenuItem_Click(sender, e);
				break;
			case PurchaseFlows.POThenInvoice:
				createFromPurchaseOrderToolStripMenuItem_Click(sender, e);
				break;
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.PurchaseInvoiceNIListFormObj);
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

		private void checkedListBoxGRN_DoubleClick(object sender, EventArgs e)
		{
			if (checkedListBoxPO.SelectedItem.ToString() != "")
			{
				NameValue nameValue = checkedListBoxPO.SelectedItem as NameValue;
				new FormHelper().EditTransaction(TransactionListType.PurchaseOrderNI, nameValue.ID, nameValue.Name);
			}
		}

		private void purchaseStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				InventoryPurchasesStatisticForm inventoryPurchasesStatisticForm = new InventoryPurchasesStatisticForm();
				inventoryPurchasesStatisticForm.SelectedID = selectedID;
				inventoryPurchasesStatisticForm.Show();
				inventoryPurchasesStatisticForm.BringToFront();
			}
		}

		private string GetSelectedID()
		{
			string result = "";
			if (dataGridItems.ActiveRow == null)
			{
				return "";
			}
			dataGridItems.ActiveRow.GetType();
			if (dataGridItems.ActiveRow != null)
			{
				if (dataGridItems.ActiveRow.GetType() != typeof(UltraGridRow))
				{
					return "";
				}
				result = dataGridItems.ActiveRow.Cells["Item Code"].Text.ToString();
			}
			return result;
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void PurchaseInvoiceForm_Click(object sender, EventArgs e)
		{
		}

		private void dataGridItem_MouseClick(object sender, MouseEventArgs e)
		{
		}

		private void DisplayItemDetails(string SelectedID)
		{
			DataSet dataSet = null;
			dataSet = Factory.VendorSystem.GetInventoryPurchaseItemDetail(comboBoxVendor.SelectedID, SelectedID);
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
			string title = ToCSV(dataTable);
			toolTipController1.ShowHint("Last Purchase Details", title, ToolTipLocation.BottomRight);
			toolTipController1.InitialDelay = 15000;
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

		private void labelTaxGroup_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxPayeeTaxGroup.SelectedID);
		}

		private void checkBoxPriceIncludeTax_CheckedChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				CalculateAllRowsTaxes();
				CalculateTotal();
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
			CalculateTotal();
		}

		private void comboBoxTaxOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxTaxOption.Text == PayeeTaxOptions.NonTaxable.ToString())
			{
				comboBoxPayeeTaxGroup.ReadOnly = true;
				comboBoxPayeeTaxGroup.Clear();
				comboBoxPayeeTaxGroup.SelectedID = null;
				comboBoxPayeeTaxGroup_SelectedIndexChanged(sender, e);
			}
			else
			{
				comboBoxPayeeTaxGroup.ReadOnly = !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
			}
			if (comboBoxTaxOption.Text != PayeeTaxOptions.NonTaxable.ToString())
			{
				taxOption = (PayeeTaxOptions)checked((byte)(comboBoxTaxOption.SelectedIndex + 1));
			}
			else
			{
				taxOption = PayeeTaxOptions.NonTaxable;
			}
		}

		private void ultraFormattedLinkLabel6_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBuyer(comboBoxBuyer.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Vendors.PurchaseInvoiceNonInvForm));
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			tabPageItems = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxAnalysis = new Micromind.DataControls.AnalysisComboBox();
			labelVoided = new System.Windows.Forms.Label();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			comboBoxgridJob = new Micromind.DataControls.JobComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			tabPageExpense = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxTotalExpense = new Micromind.UISupport.AmountTextBox();
			label2 = new System.Windows.Forms.Label();
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
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonExcelImport = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			createFromPurchaseOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromItemReceiptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			labelTaxOption = new Micromind.UISupport.MMLabel();
			comboBoxTaxOption = new System.Windows.Forms.ComboBox();
			label6 = new System.Windows.Forms.Label();
			textBoxSupplierInvoiceNo = new System.Windows.Forms.TextBox();
			labelTaxGroup = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPayeeTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			labelcostcategory = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelJob = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			textBoxBOLNo = new System.Windows.Forms.TextBox();
			comboBoxjob = new Micromind.DataControls.JobComboBox();
			label13 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			textBoxRef2 = new System.Windows.Forms.TextBox();
			buttonSelectDocument = new Micromind.UISupport.XPButton();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimePickerDueDate = new System.Windows.Forms.DateTimePicker();
			comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
			comboBoxShippingMethod = new Micromind.DataControls.ShippingMethodsComboBox();
			comboBoxTerm = new Micromind.DataControls.PaymentTermComboBox();
			comboBoxBuyer = new Micromind.DataControls.BuyerComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxVendorName = new System.Windows.Forms.TextBox();
			panel1 = new System.Windows.Forms.Panel();
			label7 = new System.Windows.Forms.Label();
			textBoxDiscountPercent = new Micromind.UISupport.PercentTextBox();
			textBoxDiscountAmount = new Micromind.UISupport.AmountTextBox();
			panelNonTax = new System.Windows.Forms.Panel();
			linkLabelTax = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxTaxAmount = new Micromind.UISupport.AmountTextBox();
			label8 = new System.Windows.Forms.Label();
			textBoxTotal = new Micromind.UISupport.AmountTextBox();
			label11 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			textBoxSubtotal = new Micromind.UISupport.AmountTextBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			purchaseStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			labelSelectedDocs = new System.Windows.Forms.Label();
			checkedListBoxPO = new System.Windows.Forms.ListBox();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			toolTipController1 = new DevExpress.Utils.ToolTipController(components);
			checkBoxPriceIncludeTax = new System.Windows.Forms.CheckBox();
			comboBoxPropertyUnit = new Micromind.DataControls.PropertyUnitComboBox();
			comboBoxProperty = new Micromind.DataControls.PropertyComboBox();
			formManager = new Micromind.DataControls.FormManager();
			tabPageItems.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxgridJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			tabPageExpense.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxjob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTerm).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBuyer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			panel1.SuspendLayout();
			panelNonTax.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).BeginInit();
			SuspendLayout();
			tabPageItems.Controls.Add(comboBoxAnalysis);
			tabPageItems.Controls.Add(labelVoided);
			tabPageItems.Controls.Add(comboBoxGridItem);
			tabPageItems.Controls.Add(comboBoxGridLocation);
			tabPageItems.Controls.Add(productPhotoViewer);
			tabPageItems.Controls.Add(comboBoxgridJob);
			tabPageItems.Controls.Add(dataGridItems);
			tabPageItems.Location = new System.Drawing.Point(1, 23);
			tabPageItems.Name = "tabPageItems";
			tabPageItems.Size = new System.Drawing.Size(879, 231);
			comboBoxAnalysis.Assigned = false;
			comboBoxAnalysis.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAnalysis.CustomReportFieldName = "";
			comboBoxAnalysis.CustomReportKey = "";
			comboBoxAnalysis.CustomReportValueType = 1;
			comboBoxAnalysis.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAnalysis.DisplayLayout.Appearance = appearance;
			comboBoxAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAnalysis.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxAnalysis.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxAnalysis.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAnalysis.Editable = true;
			comboBoxAnalysis.FilterString = "";
			comboBoxAnalysis.HasAllAccount = false;
			comboBoxAnalysis.HasCustom = false;
			comboBoxAnalysis.IsDataLoaded = false;
			comboBoxAnalysis.Location = new System.Drawing.Point(712, 94);
			comboBoxAnalysis.MaxDropDownItems = 12;
			comboBoxAnalysis.Name = "comboBoxAnalysis";
			comboBoxAnalysis.ShowInactiveItems = false;
			comboBoxAnalysis.ShowQuickAdd = true;
			comboBoxAnalysis.Size = new System.Drawing.Size(100, 20);
			comboBoxAnalysis.TabIndex = 128;
			comboBoxAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAnalysis.Visible = false;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(5, 169);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(871, 59);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[2]
			{
				Micromind.Common.Data.ItemTypes.NonInventory,
				Micromind.Common.Data.ItemTypes.Service
			};
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
			comboBoxGridItem.Location = new System.Drawing.Point(566, 36);
			comboBoxGridItem.MaxDropDownItems = 12;
			comboBoxGridItem.Name = "comboBoxGridItem";
			comboBoxGridItem.Show3PLItems = true;
			comboBoxGridItem.ShowConsignmentItems = false;
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
			comboBoxGridLocation.Location = new System.Drawing.Point(540, 51);
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
			productPhotoViewer.Location = new System.Drawing.Point(39, 120);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 120;
			productPhotoViewer.Visible = false;
			comboBoxgridJob.Assigned = false;
			comboBoxgridJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxgridJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxgridJob.CustomReportFieldName = "";
			comboBoxgridJob.CustomReportKey = "";
			comboBoxgridJob.CustomReportValueType = 1;
			comboBoxgridJob.DescriptionTextBox = null;
			comboBoxgridJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxgridJob.Editable = true;
			comboBoxgridJob.FilterString = "";
			comboBoxgridJob.HasAllAccount = false;
			comboBoxgridJob.HasCustom = false;
			comboBoxgridJob.IsDataLoaded = false;
			comboBoxgridJob.Location = new System.Drawing.Point(641, 51);
			comboBoxgridJob.MaxDropDownItems = 12;
			comboBoxgridJob.Name = "comboBoxgridJob";
			comboBoxgridJob.ShowInactiveItems = false;
			comboBoxgridJob.ShowQuickAdd = true;
			comboBoxgridJob.Size = new System.Drawing.Size(100, 20);
			comboBoxgridJob.TabIndex = 127;
			comboBoxgridJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxgridJob.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance37;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance44;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance46;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance47;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(3, 3);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(873, 225);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			tabPageExpense.Controls.Add(textBoxTotalExpense);
			tabPageExpense.Controls.Add(label2);
			tabPageExpense.Controls.Add(dataGridExpense);
			tabPageExpense.Controls.Add(comboBoxGridExpenseCode);
			tabPageExpense.Controls.Add(comboBoxGridCurrency);
			tabPageExpense.Location = new System.Drawing.Point(-10000, -10000);
			tabPageExpense.Name = "tabPageExpense";
			tabPageExpense.Size = new System.Drawing.Size(879, 231);
			textBoxTotalExpense.AllowDecimal = true;
			textBoxTotalExpense.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalExpense.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalExpense.CustomReportFieldName = "";
			textBoxTotalExpense.CustomReportKey = "";
			textBoxTotalExpense.CustomReportValueType = 1;
			textBoxTotalExpense.ForeColor = System.Drawing.Color.Black;
			textBoxTotalExpense.IsComboTextBox = false;
			textBoxTotalExpense.IsModified = false;
			textBoxTotalExpense.Location = new System.Drawing.Point(590, 228);
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
			textBoxTotalExpense.Size = new System.Drawing.Size(138, 20);
			textBoxTotalExpense.TabIndex = 0;
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
			label2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			label2.Location = new System.Drawing.Point(7, 228);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(585, 18);
			label2.TabIndex = 134;
			label2.Text = "Total Expenses:";
			label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			dataGridExpense.AllowAddNew = false;
			dataGridExpense.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridExpense.DisplayLayout.Appearance = appearance49;
			dataGridExpense.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridExpense.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			dataGridExpense.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			dataGridExpense.DisplayLayout.MaxColScrollRegions = 1;
			dataGridExpense.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridExpense.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridExpense.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			dataGridExpense.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridExpense.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridExpense.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridExpense.DisplayLayout.Override.CellAppearance = appearance56;
			dataGridExpense.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridExpense.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			dataGridExpense.DisplayLayout.Override.HeaderAppearance = appearance58;
			dataGridExpense.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridExpense.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			dataGridExpense.DisplayLayout.Override.RowAppearance = appearance59;
			dataGridExpense.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridExpense.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			dataGridExpense.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridExpense.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridExpense.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridExpense.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridExpense.ExitEditModeOnLeave = false;
			dataGridExpense.IncludeLotItems = false;
			dataGridExpense.LoadLayoutFailed = false;
			dataGridExpense.Location = new System.Drawing.Point(1, 2);
			dataGridExpense.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridExpense.Name = "dataGridExpense";
			dataGridExpense.ShowClearMenu = true;
			dataGridExpense.ShowDeleteMenu = true;
			dataGridExpense.ShowInsertMenu = true;
			dataGridExpense.ShowMoveRowsMenu = true;
			dataGridExpense.Size = new System.Drawing.Size(725, 223);
			dataGridExpense.TabIndex = 2;
			dataGridExpense.Text = "dataEntryGrid1";
			comboBoxGridExpenseCode.AlwaysInEditMode = true;
			comboBoxGridExpenseCode.Assigned = false;
			comboBoxGridExpenseCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridExpenseCode.CustomReportFieldName = "";
			comboBoxGridExpenseCode.CustomReportKey = "";
			comboBoxGridExpenseCode.CustomReportValueType = 1;
			comboBoxGridExpenseCode.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridExpenseCode.DisplayLayout.Appearance = appearance61;
			comboBoxGridExpenseCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridExpenseCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxGridExpenseCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridExpenseCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridExpenseCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
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
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCurrency.DisplayLayout.Appearance = appearance73;
			comboBoxGridCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxGridCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCurrency.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxGridCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxGridCurrency.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCurrency.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxGridCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
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
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[22]
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
				toolStripSeparator7,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonDistribution,
				toolStripButtonExcelImport,
				toolStripButtonInformation,
				toolStripDropDownButton1,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(900, 31);
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
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonExcelImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonExcelImport.Image = Micromind.ClientUI.Properties.Resources.excelimport;
			toolStripButtonExcelImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonExcelImport.Name = "toolStripButtonExcelImport";
			toolStripButtonExcelImport.Size = new System.Drawing.Size(28, 28);
			toolStripButtonExcelImport.Text = "Import from Excel";
			toolStripButtonExcelImport.Visible = false;
			toolStripButtonExcelImport.Click += new System.EventHandler(toolStripButtonExcelImport_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				duplicateToolStripMenuItem,
				toolStripSeparator4,
				createFromPurchaseOrderToolStripMenuItem,
				createFromItemReceiptToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
			duplicateToolStripMenuItem.Text = "Copy";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(259, 6);
			createFromPurchaseOrderToolStripMenuItem.Name = "createFromPurchaseOrderToolStripMenuItem";
			createFromPurchaseOrderToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
			createFromPurchaseOrderToolStripMenuItem.Text = "Create from Purchase Order...";
			createFromPurchaseOrderToolStripMenuItem.Click += new System.EventHandler(createFromPurchaseOrderToolStripMenuItem_Click);
			createFromItemReceiptToolStripMenuItem.Name = "createFromItemReceiptToolStripMenuItem";
			createFromItemReceiptToolStripMenuItem.Size = new System.Drawing.Size(262, 22);
			createFromItemReceiptToolStripMenuItem.Text = "Create from Goods Received Note...";
			createFromItemReceiptToolStripMenuItem.Visible = false;
			createFromItemReceiptToolStripMenuItem.Click += new System.EventHandler(createFromItemReceiptToolStripMenuItem_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 520);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(900, 40);
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
			linePanelDown.Size = new System.Drawing.Size(900, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(790, 8);
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
			dateTimePickerDate.Location = new System.Drawing.Point(675, 1);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(336, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(138, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(593, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(69, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference 1:";
			textBoxRef1.Location = new System.Drawing.Point(675, 23);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(138, 20);
			textBoxRef1.TabIndex = 6;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(49, 434);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(310, 78);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 434);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance85.FontData.BoldAsString = "True";
			appearance85.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance85;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(224, 4);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 0;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance86.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance86;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(labelTaxOption);
			panelDetails.Controls.Add(comboBoxTaxOption);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(textBoxSupplierInvoiceNo);
			panelDetails.Controls.Add(labelTaxGroup);
			panelDetails.Controls.Add(comboBoxPayeeTaxGroup);
			panelDetails.Controls.Add(labelcostcategory);
			panelDetails.Controls.Add(labelJob);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(comboBoxCostCategory);
			panelDetails.Controls.Add(textBoxBOLNo);
			panelDetails.Controls.Add(comboBoxjob);
			panelDetails.Controls.Add(label13);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(textBoxRef2);
			panelDetails.Controls.Add(buttonSelectDocument);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(dateTimePickerDueDate);
			panelDetails.Controls.Add(comboBoxCurrency);
			panelDetails.Controls.Add(comboBoxShippingMethod);
			panelDetails.Controls.Add(comboBoxTerm);
			panelDetails.Controls.Add(comboBoxBuyer);
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxVendor);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxVendorName);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(883, 133);
			panelDetails.TabIndex = 0;
			labelTaxOption.AutoSize = true;
			labelTaxOption.BackColor = System.Drawing.Color.Transparent;
			labelTaxOption.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelTaxOption.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			labelTaxOption.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTaxOption.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelTaxOption.IsFieldHeader = false;
			labelTaxOption.IsRequired = false;
			labelTaxOption.Location = new System.Drawing.Point(257, 71);
			labelTaxOption.Name = "labelTaxOption";
			labelTaxOption.PenWidth = 1f;
			labelTaxOption.ShowBorder = false;
			labelTaxOption.Size = new System.Drawing.Size(64, 13);
			labelTaxOption.TabIndex = 196;
			labelTaxOption.Text = "Tax Option:";
			comboBoxTaxOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTaxOption.ForeColor = System.Drawing.SystemColors.WindowText;
			comboBoxTaxOption.FormattingEnabled = true;
			comboBoxTaxOption.Items.AddRange(new object[3]
			{
				"Taxable",
				"NonTaxable",
				"ReverseChargeMethod"
			});
			comboBoxTaxOption.Location = new System.Drawing.Point(350, 67);
			comboBoxTaxOption.Name = "comboBoxTaxOption";
			comboBoxTaxOption.Size = new System.Drawing.Size(155, 21);
			comboBoxTaxOption.TabIndex = 11;
			comboBoxTaxOption.SelectedIndexChanged += new System.EventHandler(comboBoxTaxOption_SelectedIndexChanged);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(593, 92);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(74, 13);
			label6.TabIndex = 194;
			label6.Text = "Vendor Ref #:";
			textBoxSupplierInvoiceNo.Location = new System.Drawing.Point(675, 89);
			textBoxSupplierInvoiceNo.MaxLength = 20;
			textBoxSupplierInvoiceNo.Name = "textBoxSupplierInvoiceNo";
			textBoxSupplierInvoiceNo.Size = new System.Drawing.Size(138, 20);
			textBoxSupplierInvoiceNo.TabIndex = 15;
			appearance87.FontData.BoldAsString = "False";
			appearance87.FontData.Name = "Tahoma";
			labelTaxGroup.Appearance = appearance87;
			labelTaxGroup.AutoSize = true;
			labelTaxGroup.Location = new System.Drawing.Point(257, 91);
			labelTaxGroup.Name = "labelTaxGroup";
			labelTaxGroup.Size = new System.Drawing.Size(59, 15);
			labelTaxGroup.TabIndex = 192;
			labelTaxGroup.TabStop = true;
			labelTaxGroup.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelTaxGroup.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelTaxGroup.Value = "Tax Group:";
			appearance88.ForeColor = System.Drawing.Color.Blue;
			labelTaxGroup.VisitedLinkAppearance = appearance88;
			labelTaxGroup.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelTaxGroup_LinkClicked);
			comboBoxPayeeTaxGroup.Assigned = false;
			comboBoxPayeeTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPayeeTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayeeTaxGroup.CustomReportFieldName = "";
			comboBoxPayeeTaxGroup.CustomReportKey = "";
			comboBoxPayeeTaxGroup.CustomReportValueType = 1;
			comboBoxPayeeTaxGroup.DescriptionTextBox = null;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayeeTaxGroup.DisplayLayout.Appearance = appearance89;
			comboBoxPayeeTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayeeTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance90.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance90;
			appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance91;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance92.BackColor2 = System.Drawing.SystemColors.Control;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance92.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance92;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance93;
			appearance94.BackColor = System.Drawing.SystemColors.Highlight;
			appearance94.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance94;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance95;
			appearance96.BorderColor = System.Drawing.Color.Silver;
			appearance96.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellAppearance = appearance96;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance97.BackColor = System.Drawing.SystemColors.Control;
			appearance97.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance97.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance97.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance97;
			appearance98.TextHAlignAsString = "Left";
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance98;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowAppearance = appearance99;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance100;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayeeTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayeeTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayeeTaxGroup.Editable = true;
			comboBoxPayeeTaxGroup.FilterString = "";
			comboBoxPayeeTaxGroup.HasAllAccount = false;
			comboBoxPayeeTaxGroup.HasCustom = false;
			comboBoxPayeeTaxGroup.IsDataLoaded = false;
			comboBoxPayeeTaxGroup.Location = new System.Drawing.Point(350, 89);
			comboBoxPayeeTaxGroup.MaxDropDownItems = 12;
			comboBoxPayeeTaxGroup.Name = "comboBoxPayeeTaxGroup";
			comboBoxPayeeTaxGroup.ReadOnly = true;
			comboBoxPayeeTaxGroup.ShowInactiveItems = false;
			comboBoxPayeeTaxGroup.ShowQuickAdd = true;
			comboBoxPayeeTaxGroup.Size = new System.Drawing.Size(155, 20);
			comboBoxPayeeTaxGroup.TabIndex = 14;
			comboBoxPayeeTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance101.FontData.BoldAsString = "False";
			appearance101.FontData.Name = "Tahoma";
			labelcostcategory.Appearance = appearance101;
			labelcostcategory.AutoSize = true;
			labelcostcategory.Location = new System.Drawing.Point(593, 112);
			labelcostcategory.Name = "labelcostcategory";
			labelcostcategory.Size = new System.Drawing.Size(76, 15);
			labelcostcategory.TabIndex = 190;
			labelcostcategory.TabStop = true;
			labelcostcategory.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelcostcategory.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelcostcategory.Value = "Cost Category:";
			appearance102.ForeColor = System.Drawing.Color.Blue;
			labelcostcategory.VisitedLinkAppearance = appearance102;
			appearance103.FontData.BoldAsString = "False";
			appearance103.FontData.Name = "Tahoma";
			labelJob.Appearance = appearance103;
			labelJob.AutoSize = true;
			labelJob.Location = new System.Drawing.Point(257, 113);
			labelJob.Name = "labelJob";
			labelJob.Size = new System.Drawing.Size(42, 15);
			labelJob.TabIndex = 189;
			labelJob.TabStop = true;
			labelJob.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelJob.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelJob.Value = "Project:";
			appearance104.ForeColor = System.Drawing.Color.Blue;
			labelJob.VisitedLinkAppearance = appearance104;
			appearance105.FontData.BoldAsString = "False";
			appearance105.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance105;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(12, 69);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(90, 15);
			ultraFormattedLinkLabel3.TabIndex = 188;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Shipping Method:";
			appearance106.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance106;
			appearance107.FontData.BoldAsString = "False";
			appearance107.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance107;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 91);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(79, 15);
			ultraFormattedLinkLabel1.TabIndex = 187;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Payment Term:";
			appearance108.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance108;
			appearance109.FontData.BoldAsString = "False";
			appearance109.FontData.Name = "Tahoma";
			labelCurrency.Appearance = appearance109;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(593, 69);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 15);
			labelCurrency.TabIndex = 186;
			labelCurrency.TabStop = true;
			labelCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCurrency.Value = "Currency:";
			appearance110.ForeColor = System.Drawing.Color.Blue;
			labelCurrency.VisitedLinkAppearance = appearance110;
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
			comboBoxCostCategory.Location = new System.Drawing.Point(675, 111);
			comboBoxCostCategory.MaxDropDownItems = 12;
			comboBoxCostCategory.Name = "comboBoxCostCategory";
			comboBoxCostCategory.ShowInactiveItems = false;
			comboBoxCostCategory.ShowQuickAdd = true;
			comboBoxCostCategory.Size = new System.Drawing.Size(138, 20);
			comboBoxCostCategory.TabIndex = 18;
			comboBoxCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxBOLNo.BackColor = System.Drawing.Color.White;
			textBoxBOLNo.Location = new System.Drawing.Point(109, 109);
			textBoxBOLNo.Name = "textBoxBOLNo";
			textBoxBOLNo.ReadOnly = true;
			textBoxBOLNo.Size = new System.Drawing.Size(140, 20);
			textBoxBOLNo.TabIndex = 16;
			textBoxBOLNo.TabStop = false;
			comboBoxjob.Assigned = false;
			comboBoxjob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxjob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxjob.CustomReportFieldName = "";
			comboBoxjob.CustomReportKey = "";
			comboBoxjob.CustomReportValueType = 1;
			comboBoxjob.DescriptionTextBox = null;
			comboBoxjob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxjob.Editable = true;
			comboBoxjob.FilterString = "";
			comboBoxjob.HasAllAccount = false;
			comboBoxjob.HasCustom = false;
			comboBoxjob.IsDataLoaded = false;
			comboBoxjob.Location = new System.Drawing.Point(350, 111);
			comboBoxjob.MaxDropDownItems = 12;
			comboBoxjob.Name = "comboBoxjob";
			comboBoxjob.ShowInactiveItems = false;
			comboBoxjob.ShowQuickAdd = true;
			comboBoxjob.Size = new System.Drawing.Size(155, 20);
			comboBoxjob.TabIndex = 17;
			comboBoxjob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(12, 113);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(48, 13);
			label13.TabIndex = 185;
			label13.Text = "BOL No:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(593, 49);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(69, 13);
			label4.TabIndex = 151;
			label4.Text = "Reference 2:";
			textBoxRef2.Location = new System.Drawing.Point(675, 45);
			textBoxRef2.MaxLength = 20;
			textBoxRef2.Name = "textBoxRef2";
			textBoxRef2.Size = new System.Drawing.Size(138, 20);
			textBoxRef2.TabIndex = 9;
			buttonSelectDocument.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocument.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocument.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocument.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocument.Location = new System.Drawing.Point(444, 22);
			buttonSelectDocument.Name = "buttonSelectDocument";
			buttonSelectDocument.Size = new System.Drawing.Size(29, 22);
			buttonSelectDocument.TabIndex = 5;
			buttonSelectDocument.Text = "...";
			buttonSelectDocument.UseVisualStyleBackColor = false;
			buttonSelectDocument.Click += new System.EventHandler(buttonSelectDocument_Click);
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(257, 50);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(65, 13);
			mmLabel2.TabIndex = 149;
			mmLabel2.Text = "Due Date:";
			dateTimePickerDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDueDate.Location = new System.Drawing.Point(350, 46);
			dateTimePickerDueDate.Name = "dateTimePickerDueDate";
			dateTimePickerDueDate.Size = new System.Drawing.Size(124, 20);
			dateTimePickerDueDate.TabIndex = 8;
			comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCurrency.Location = new System.Drawing.Point(675, 67);
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
			comboBoxCurrency.Size = new System.Drawing.Size(138, 20);
			comboBoxCurrency.TabIndex = 12;
			comboBoxShippingMethod.AlwaysInEditMode = true;
			comboBoxShippingMethod.Assigned = false;
			comboBoxShippingMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingMethod.CustomReportFieldName = "";
			comboBoxShippingMethod.CustomReportKey = "";
			comboBoxShippingMethod.CustomReportValueType = 1;
			comboBoxShippingMethod.DescriptionTextBox = null;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingMethod.DisplayLayout.Appearance = appearance111;
			comboBoxShippingMethod.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingMethod.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance112.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance112.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance112.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.Appearance = appearance112;
			appearance113.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BandLabelAppearance = appearance113;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance114.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance114.BackColor2 = System.Drawing.SystemColors.Control;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance114.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.PromptAppearance = appearance114;
			comboBoxShippingMethod.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingMethod.DisplayLayout.MaxRowScrollRegions = 1;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveCellAppearance = appearance115;
			appearance116.BackColor = System.Drawing.SystemColors.Highlight;
			appearance116.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveRowAppearance = appearance116;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.CardAreaAppearance = appearance117;
			appearance118.BorderColor = System.Drawing.Color.Silver;
			appearance118.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingMethod.DisplayLayout.Override.CellAppearance = appearance118;
			comboBoxShippingMethod.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingMethod.DisplayLayout.Override.CellPadding = 0;
			appearance119.BackColor = System.Drawing.SystemColors.Control;
			appearance119.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance119.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance119.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance119.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.GroupByRowAppearance = appearance119;
			appearance120.TextHAlignAsString = "Left";
			comboBoxShippingMethod.DisplayLayout.Override.HeaderAppearance = appearance120;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingMethod.DisplayLayout.Override.RowAppearance = appearance121;
			comboBoxShippingMethod.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance122.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingMethod.DisplayLayout.Override.TemplateAddRowAppearance = appearance122;
			comboBoxShippingMethod.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingMethod.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingMethod.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingMethod.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingMethod.Editable = true;
			comboBoxShippingMethod.FilterString = "";
			comboBoxShippingMethod.HasAllAccount = false;
			comboBoxShippingMethod.HasCustom = false;
			comboBoxShippingMethod.IsDataLoaded = false;
			comboBoxShippingMethod.Location = new System.Drawing.Point(109, 66);
			comboBoxShippingMethod.MaxDropDownItems = 12;
			comboBoxShippingMethod.Name = "comboBoxShippingMethod";
			comboBoxShippingMethod.ShowInactiveItems = false;
			comboBoxShippingMethod.ShowQuickAdd = true;
			comboBoxShippingMethod.Size = new System.Drawing.Size(140, 20);
			comboBoxShippingMethod.TabIndex = 10;
			comboBoxShippingMethod.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxTerm.AlwaysInEditMode = true;
			comboBoxTerm.Assigned = false;
			comboBoxTerm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTerm.CustomReportFieldName = "";
			comboBoxTerm.CustomReportKey = "";
			comboBoxTerm.CustomReportValueType = 1;
			comboBoxTerm.DescriptionTextBox = null;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			appearance123.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTerm.DisplayLayout.Appearance = appearance123;
			comboBoxTerm.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTerm.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance124.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance124.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance124.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.GroupByBox.Appearance = appearance124;
			appearance125.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTerm.DisplayLayout.GroupByBox.BandLabelAppearance = appearance125;
			comboBoxTerm.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance126.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance126.BackColor2 = System.Drawing.SystemColors.Control;
			appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance126.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTerm.DisplayLayout.GroupByBox.PromptAppearance = appearance126;
			comboBoxTerm.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTerm.DisplayLayout.MaxRowScrollRegions = 1;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTerm.DisplayLayout.Override.ActiveCellAppearance = appearance127;
			appearance128.BackColor = System.Drawing.SystemColors.Highlight;
			appearance128.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTerm.DisplayLayout.Override.ActiveRowAppearance = appearance128;
			comboBoxTerm.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTerm.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.Override.CardAreaAppearance = appearance129;
			appearance130.BorderColor = System.Drawing.Color.Silver;
			appearance130.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTerm.DisplayLayout.Override.CellAppearance = appearance130;
			comboBoxTerm.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTerm.DisplayLayout.Override.CellPadding = 0;
			appearance131.BackColor = System.Drawing.SystemColors.Control;
			appearance131.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance131.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance131.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.Override.GroupByRowAppearance = appearance131;
			appearance132.TextHAlignAsString = "Left";
			comboBoxTerm.DisplayLayout.Override.HeaderAppearance = appearance132;
			comboBoxTerm.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTerm.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.BorderColor = System.Drawing.Color.Silver;
			comboBoxTerm.DisplayLayout.Override.RowAppearance = appearance133;
			comboBoxTerm.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance134.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTerm.DisplayLayout.Override.TemplateAddRowAppearance = appearance134;
			comboBoxTerm.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTerm.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTerm.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTerm.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTerm.Editable = true;
			comboBoxTerm.FilterString = "";
			comboBoxTerm.HasAllAccount = false;
			comboBoxTerm.HasCustom = false;
			comboBoxTerm.IsDataLoaded = false;
			comboBoxTerm.Location = new System.Drawing.Point(109, 87);
			comboBoxTerm.MaxDropDownItems = 12;
			comboBoxTerm.Name = "comboBoxTerm";
			comboBoxTerm.ShowInactiveItems = false;
			comboBoxTerm.ShowQuickAdd = true;
			comboBoxTerm.Size = new System.Drawing.Size(140, 20);
			comboBoxTerm.TabIndex = 13;
			comboBoxTerm.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBuyer.AlwaysInEditMode = true;
			comboBoxBuyer.Assigned = false;
			comboBoxBuyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBuyer.CustomReportFieldName = "";
			comboBoxBuyer.CustomReportKey = "";
			comboBoxBuyer.CustomReportValueType = 1;
			comboBoxBuyer.DescriptionTextBox = null;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			appearance135.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBuyer.DisplayLayout.Appearance = appearance135;
			comboBoxBuyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBuyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance136.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance136.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance136.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance136.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBuyer.DisplayLayout.GroupByBox.Appearance = appearance136;
			appearance137.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBuyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance137;
			comboBoxBuyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance138.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance138.BackColor2 = System.Drawing.SystemColors.Control;
			appearance138.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance138.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBuyer.DisplayLayout.GroupByBox.PromptAppearance = appearance138;
			comboBoxBuyer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBuyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBuyer.DisplayLayout.Override.ActiveCellAppearance = appearance139;
			appearance140.BackColor = System.Drawing.SystemColors.Highlight;
			appearance140.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBuyer.DisplayLayout.Override.ActiveRowAppearance = appearance140;
			comboBoxBuyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBuyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance141.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBuyer.DisplayLayout.Override.CardAreaAppearance = appearance141;
			appearance142.BorderColor = System.Drawing.Color.Silver;
			appearance142.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBuyer.DisplayLayout.Override.CellAppearance = appearance142;
			comboBoxBuyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBuyer.DisplayLayout.Override.CellPadding = 0;
			appearance143.BackColor = System.Drawing.SystemColors.Control;
			appearance143.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance143.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance143.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance143.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBuyer.DisplayLayout.Override.GroupByRowAppearance = appearance143;
			appearance144.TextHAlignAsString = "Left";
			comboBoxBuyer.DisplayLayout.Override.HeaderAppearance = appearance144;
			comboBoxBuyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBuyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.BorderColor = System.Drawing.Color.Silver;
			comboBoxBuyer.DisplayLayout.Override.RowAppearance = appearance145;
			comboBoxBuyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance146.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBuyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance146;
			comboBoxBuyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBuyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBuyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBuyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBuyer.Editable = true;
			comboBoxBuyer.FilterString = "";
			comboBoxBuyer.HasAllAccount = false;
			comboBoxBuyer.HasCustom = false;
			comboBoxBuyer.IsDataLoaded = false;
			comboBoxBuyer.Location = new System.Drawing.Point(109, 45);
			comboBoxBuyer.MaxDropDownItems = 12;
			comboBoxBuyer.Name = "comboBoxBuyer";
			comboBoxBuyer.ShowInactiveItems = false;
			comboBoxBuyer.ShowQuickAdd = true;
			comboBoxBuyer.Size = new System.Drawing.Size(140, 20);
			comboBoxBuyer.TabIndex = 7;
			comboBoxBuyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance147.FontData.BoldAsString = "False";
			appearance147.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance147;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(12, 47);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(36, 15);
			ultraFormattedLinkLabel6.TabIndex = 146;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Buyer:";
			appearance148.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance148;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked_1);
			appearance149.FontData.BoldAsString = "True";
			appearance149.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance149;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(12, 24);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(64, 15);
			ultraFormattedLinkLabel4.TabIndex = 129;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Vendor ID:";
			appearance150.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance150;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = null;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			appearance151.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance151;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance152.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance152.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance152.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance152.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance152;
			appearance153.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance153;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance154.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance154.BackColor2 = System.Drawing.SystemColors.Control;
			appearance154.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance154.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance154;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance155.BackColor = System.Drawing.SystemColors.Window;
			appearance155.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance155;
			appearance156.BackColor = System.Drawing.SystemColors.Highlight;
			appearance156.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance156;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance157;
			appearance158.BorderColor = System.Drawing.Color.Silver;
			appearance158.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance158;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance159.BackColor = System.Drawing.SystemColors.Control;
			appearance159.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance159.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance159.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance159.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance159;
			appearance160.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance160;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			appearance161.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance161;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance162.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance162;
			comboBoxVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVendor.Editable = true;
			comboBoxVendor.FilterString = "";
			comboBoxVendor.FilterSysDocID = "";
			comboBoxVendor.HasAll = false;
			comboBoxVendor.HasCustom = false;
			comboBoxVendor.IsDataLoaded = false;
			comboBoxVendor.Location = new System.Drawing.Point(109, 23);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(109, 20);
			comboBoxVendor.TabIndex = 3;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance163.FontData.BoldAsString = "True";
			appearance163.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance163;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 2;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance164.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance164;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance165.BackColor = System.Drawing.SystemColors.Window;
			appearance165.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance165;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance166.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance166.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance166.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance166.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance166;
			appearance167.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance167;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance168.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance168.BackColor2 = System.Drawing.SystemColors.Control;
			appearance168.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance168.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance168;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance169.BackColor = System.Drawing.SystemColors.Window;
			appearance169.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance169;
			appearance170.BackColor = System.Drawing.SystemColors.Highlight;
			appearance170.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance170;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance171.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance171;
			appearance172.BorderColor = System.Drawing.Color.Silver;
			appearance172.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance172;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance173.BackColor = System.Drawing.SystemColors.Control;
			appearance173.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance173.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance173.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance173.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance173;
			appearance174.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance174;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance175.BackColor = System.Drawing.SystemColors.Window;
			appearance175.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance175;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance176.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance176;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(109, 1);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(109, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(593, 3);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			textBoxVendorName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVendorName.Location = new System.Drawing.Point(220, 23);
			textBoxVendorName.MaxLength = 64;
			textBoxVendorName.Name = "textBoxVendorName";
			textBoxVendorName.ReadOnly = true;
			textBoxVendorName.Size = new System.Drawing.Size(252, 20);
			textBoxVendorName.TabIndex = 4;
			textBoxVendorName.TabStop = false;
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(textBoxDiscountPercent);
			panel1.Controls.Add(textBoxDiscountAmount);
			panel1.Controls.Add(panelNonTax);
			panel1.Controls.Add(label11);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(textBoxSubtotal);
			panel1.Location = new System.Drawing.Point(668, 428);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(221, 90);
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
			panelNonTax.Controls.Add(linkLabelTax);
			panelNonTax.Controls.Add(textBoxTaxAmount);
			panelNonTax.Controls.Add(label8);
			panelNonTax.Controls.Add(textBoxTotal);
			panelNonTax.Location = new System.Drawing.Point(0, 43);
			panelNonTax.Name = "panelNonTax";
			panelNonTax.Size = new System.Drawing.Size(219, 43);
			panelNonTax.TabIndex = 149;
			appearance177.FontData.BoldAsString = "False";
			appearance177.FontData.Name = "Tahoma";
			linkLabelTax.Appearance = appearance177;
			linkLabelTax.AutoSize = true;
			linkLabelTax.Location = new System.Drawing.Point(7, 4);
			linkLabelTax.Name = "linkLabelTax";
			linkLabelTax.Size = new System.Drawing.Size(25, 15);
			linkLabelTax.TabIndex = 153;
			linkLabelTax.TabStop = true;
			linkLabelTax.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelTax.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelTax.Value = "Tax:";
			appearance178.ForeColor = System.Drawing.Color.Blue;
			linkLabelTax.VisitedLinkAppearance = appearance178;
			linkLabelTax.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelTax_LinkClicked);
			textBoxTaxAmount.AllowDecimal = true;
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
			label8.Location = new System.Drawing.Point(4, 26);
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
			textBoxTotal.Location = new System.Drawing.Point(80, 22);
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
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				availableQuantityToolStripMenuItem,
				purchaseStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
			availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
			availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			availableQuantityToolStripMenuItem.Text = "Available Quantity...";
			availableQuantityToolStripMenuItem.Click += new System.EventHandler(availableQuantityToolStripMenuItem_Click);
			purchaseStatisticsToolStripMenuItem.Name = "purchaseStatisticsToolStripMenuItem";
			purchaseStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			purchaseStatisticsToolStripMenuItem.Text = "Purchase Statistics...";
			purchaseStatisticsToolStripMenuItem.Click += new System.EventHandler(purchaseStatisticsToolStripMenuItem_Click);
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemPicToolStripMenuItem.Text = "Item Photo...";
			itemPicToolStripMenuItem.Click += new System.EventHandler(itemPicToolStripMenuItem_Click);
			itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
			itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemDetailsToolStripMenuItem.Text = "Item Details...";
			itemDetailsToolStripMenuItem.Click += new System.EventHandler(itemDetailsToolStripMenuItem_Click);
			labelSelectedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelSelectedDocs.AutoSize = true;
			labelSelectedDocs.Location = new System.Drawing.Point(362, 432);
			labelSelectedDocs.Name = "labelSelectedDocs";
			labelSelectedDocs.Size = new System.Drawing.Size(109, 13);
			labelSelectedDocs.TabIndex = 124;
			labelSelectedDocs.Text = "Selected Documents:";
			labelSelectedDocs.Visible = false;
			checkedListBoxPO.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxPO.FormattingEnabled = true;
			checkedListBoxPO.Location = new System.Drawing.Point(365, 445);
			checkedListBoxPO.Name = "checkedListBoxPO";
			checkedListBoxPO.Size = new System.Drawing.Size(155, 69);
			checkedListBoxPO.TabIndex = 125;
			checkedListBoxPO.DoubleClick += new System.EventHandler(checkedListBoxGRN_DoubleClick);
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageItems);
			ultraTabControl1.Controls.Add(tabPageExpense);
			ultraTabControl1.Location = new System.Drawing.Point(6, 171);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(883, 257);
			ultraTabControl1.TabIndex = 1;
			ultraTab.TabPage = tabPageItems;
			ultraTab.Text = "Items";
			ultraTab2.TabPage = tabPageExpense;
			ultraTab2.Text = "Expenses";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(879, 231);
			checkBoxPriceIncludeTax.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			checkBoxPriceIncludeTax.AutoSize = true;
			checkBoxPriceIncludeTax.Location = new System.Drawing.Point(539, 434);
			checkBoxPriceIncludeTax.Name = "checkBoxPriceIncludeTax";
			checkBoxPriceIncludeTax.Size = new System.Drawing.Size(123, 17);
			checkBoxPriceIncludeTax.TabIndex = 3;
			checkBoxPriceIncludeTax.Text = "Price inclusive of tax";
			checkBoxPriceIncludeTax.UseVisualStyleBackColor = true;
			checkBoxPriceIncludeTax.Visible = false;
			checkBoxPriceIncludeTax.CheckedChanged += new System.EventHandler(checkBoxPriceIncludeTax_CheckedChanged);
			comboBoxPropertyUnit.Assigned = false;
			comboBoxPropertyUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPropertyUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPropertyUnit.CustomReportFieldName = "";
			comboBoxPropertyUnit.CustomReportKey = "";
			comboBoxPropertyUnit.CustomReportValueType = 1;
			comboBoxPropertyUnit.DescriptionTextBox = null;
			appearance179.BackColor = System.Drawing.SystemColors.Window;
			appearance179.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPropertyUnit.DisplayLayout.Appearance = appearance179;
			comboBoxPropertyUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPropertyUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance180.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance180.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance180.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance180.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.Appearance = appearance180;
			appearance181.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance181;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance182.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance182.BackColor2 = System.Drawing.SystemColors.Control;
			appearance182.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance182.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance182;
			comboBoxPropertyUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPropertyUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance183.BackColor = System.Drawing.SystemColors.Window;
			appearance183.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPropertyUnit.DisplayLayout.Override.ActiveCellAppearance = appearance183;
			appearance184.BackColor = System.Drawing.SystemColors.Highlight;
			appearance184.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPropertyUnit.DisplayLayout.Override.ActiveRowAppearance = appearance184;
			comboBoxPropertyUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPropertyUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance185.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.Override.CardAreaAppearance = appearance185;
			appearance186.BorderColor = System.Drawing.Color.Silver;
			appearance186.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPropertyUnit.DisplayLayout.Override.CellAppearance = appearance186;
			comboBoxPropertyUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPropertyUnit.DisplayLayout.Override.CellPadding = 0;
			appearance187.BackColor = System.Drawing.SystemColors.Control;
			appearance187.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance187.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance187.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance187.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.Override.GroupByRowAppearance = appearance187;
			appearance188.TextHAlignAsString = "Left";
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderAppearance = appearance188;
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance189.BackColor = System.Drawing.SystemColors.Window;
			appearance189.BorderColor = System.Drawing.Color.Silver;
			comboBoxPropertyUnit.DisplayLayout.Override.RowAppearance = appearance189;
			comboBoxPropertyUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance190.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPropertyUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance190;
			comboBoxPropertyUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPropertyUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPropertyUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPropertyUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPropertyUnit.Editable = true;
			comboBoxPropertyUnit.FilterString = "";
			comboBoxPropertyUnit.HasAllAccount = false;
			comboBoxPropertyUnit.HasCustom = false;
			comboBoxPropertyUnit.IsDataLoaded = false;
			comboBoxPropertyUnit.Location = new System.Drawing.Point(423, 291);
			comboBoxPropertyUnit.MaxDropDownItems = 12;
			comboBoxPropertyUnit.Name = "comboBoxPropertyUnit";
			comboBoxPropertyUnit.ShowActiveOnly = false;
			comboBoxPropertyUnit.ShowInactiveItems = false;
			comboBoxPropertyUnit.ShowQuickAdd = true;
			comboBoxPropertyUnit.Size = new System.Drawing.Size(100, 20);
			comboBoxPropertyUnit.TabIndex = 128;
			comboBoxPropertyUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPropertyUnit.Visible = false;
			comboBoxProperty.Assigned = false;
			comboBoxProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProperty.CustomReportFieldName = "";
			comboBoxProperty.CustomReportKey = "";
			comboBoxProperty.CustomReportValueType = 1;
			comboBoxProperty.DescriptionTextBox = null;
			appearance191.BackColor = System.Drawing.SystemColors.Window;
			appearance191.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProperty.DisplayLayout.Appearance = appearance191;
			comboBoxProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance192.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance192.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance192.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance192.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.GroupByBox.Appearance = appearance192;
			appearance193.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance193;
			comboBoxProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance194.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance194.BackColor2 = System.Drawing.SystemColors.Control;
			appearance194.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance194.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance194;
			comboBoxProperty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance195.BackColor = System.Drawing.SystemColors.Window;
			appearance195.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProperty.DisplayLayout.Override.ActiveCellAppearance = appearance195;
			appearance196.BackColor = System.Drawing.SystemColors.Highlight;
			appearance196.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProperty.DisplayLayout.Override.ActiveRowAppearance = appearance196;
			comboBoxProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance197.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.CardAreaAppearance = appearance197;
			appearance198.BorderColor = System.Drawing.Color.Silver;
			appearance198.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProperty.DisplayLayout.Override.CellAppearance = appearance198;
			comboBoxProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProperty.DisplayLayout.Override.CellPadding = 0;
			appearance199.BackColor = System.Drawing.SystemColors.Control;
			appearance199.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance199.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance199.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance199.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.GroupByRowAppearance = appearance199;
			appearance200.TextHAlignAsString = "Left";
			comboBoxProperty.DisplayLayout.Override.HeaderAppearance = appearance200;
			comboBoxProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance201.BackColor = System.Drawing.SystemColors.Window;
			appearance201.BorderColor = System.Drawing.Color.Silver;
			comboBoxProperty.DisplayLayout.Override.RowAppearance = appearance201;
			comboBoxProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance202.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance202;
			comboBoxProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProperty.Editable = true;
			comboBoxProperty.FilterString = "";
			comboBoxProperty.HasAllAccount = false;
			comboBoxProperty.HasCustom = false;
			comboBoxProperty.IsDataLoaded = false;
			comboBoxProperty.Location = new System.Drawing.Point(378, 250);
			comboBoxProperty.MaxDropDownItems = 12;
			comboBoxProperty.Name = "comboBoxProperty";
			comboBoxProperty.ShowInactiveItems = false;
			comboBoxProperty.ShowQuickAdd = true;
			comboBoxProperty.Size = new System.Drawing.Size(100, 20);
			comboBoxProperty.TabIndex = 127;
			comboBoxProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxProperty.Visible = false;
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
			base.ClientSize = new System.Drawing.Size(900, 560);
			base.Controls.Add(comboBoxPropertyUnit);
			base.Controls.Add(comboBoxProperty);
			base.Controls.Add(checkBoxPriceIncludeTax);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(labelSelectedDocs);
			base.Controls.Add(panel1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(label3);
			base.Controls.Add(checkedListBoxPO);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "PurchaseInvoiceNonInvForm";
			Text = "Purchase Invoice(Non Inventory)";
			base.Click += new System.EventHandler(PurchaseInvoiceForm_Click);
			tabPageItems.ResumeLayout(false);
			tabPageItems.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxgridJob).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
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
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxjob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTerm).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBuyer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panelNonTax.ResumeLayout(false);
			panelNonTax.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
