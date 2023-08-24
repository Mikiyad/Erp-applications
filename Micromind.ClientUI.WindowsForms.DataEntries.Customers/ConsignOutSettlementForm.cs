using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class ConsignOutSettlementForm : Form, IForm
	{
		private bool trackDetailedSales;

		private DataTable deliveryNoteTable;

		private bool allowEdit = true;

		private ConsignOutSettlementData currentData;

		private const string TABLENAME_CONST = "ConsignOut_Settlement";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentCustomerAddressID = "";

		private string consignSysDocID = "";

		private string consignVoucherID = "";

		private bool allowMultiTemplate;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool isVoid;

		private bool totalChanged;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

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

		private ProductUnitComboBox comboBoxGridProductUnit;

		private customersFlatComboBox comboBoxCustomer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TextBox textBoxCustomerName;

		private MMTextBox textBoxBilltoAddress;

		private Label label2;

		private TextBox textBoxSettleNumber;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraButton buttonPrevBillto;

		private UltraButton buttonNextBillTo;

		private AmountTextBox textBoxSubtotal;

		private Panel panel1;

		private Label label5;

		private AmountTextBox textBoxExpenses;

		private Label label8;

		private Label label7;

		private AmountTextBox textBoxTotal;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private Label label9;

		private PaymentTermComboBox comboBoxTerm;

		private LocationComboBox comboBoxGridLocation;

		private CurrencySelector comboBoxCurrency;

		private ToolStripButton toolStripButtonPreview;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private DataEntryGrid dataGridExpense;

		private ExpenseCodeComboBox comboBoxGridExpenseCode;

		private TextBox textBoxConsignment;

		private XPButton buttonSelectConsignment;

		private AmountTextBox textBoxTotalExpense;

		private Label label4;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonDistribution;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem saveDraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator8;

		private UltraFormattedLinkLabel ultraFormattedLinkCurrency;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripButton toolStripButtonMultiPreview;

		private SalespersonComboBox comboBoxSalesperson;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerDueDate;

		private AmountTextBox textBoxExpenseDiff;

		private Label labelExpDiff;

		private ToolStripButton toolStripButtonPaymentAllocation;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2010;

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
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					bool enabled = buttonVoid.Enabled = false;
					xPButton.Enabled = enabled;
					customersFlatComboBox customersFlatComboBox = comboBoxCustomer;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					bool flag3 = textBoxVoucherNumber.Enabled = true;
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
					customersFlatComboBox customersFlatComboBox2 = comboBoxCustomer;
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					bool flag3 = textBoxVoucherNumber.Enabled = false;
					bool enabled = sysDocComboBox2.Enabled = flag3;
					customersFlatComboBox2.Enabled = enabled;
				}
				toolStripButtonDistribution.Enabled = !value;
				toolStripButtonMultiPreview.Visible = !value;
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

		public ConsignOutSettlementForm()
		{
			InitializeComponent();
			AddEvents();
			trackDetailedSales = CompanyPreferences.TrackConsignOutDetailedSales;
			comboBoxCustomer.ShowConsignmentOnly = true;
			dataGridExpense.AllowCustomizeHeaders = true;
			dataGridItems.AllowCustomizeHeaders = true;
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
			dataGridExpense.AfterRowsDeleted += dataGridExpense_AfterRowsDeleted;
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			productPhotoViewer.EnlargeRequested += productPhotoViewer_EnlargeRequested;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			textBoxExpenses.Validating += textBoxDiscountAmount_Validating;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			textBoxExpenses.Validated += textBoxDiscountAmount_Validated;
			dataGridExpense.AfterCellUpdate += dataGridExpense_AfterCellUpdate;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			comboBoxTerm.SelectedIndexChanged += comboBoxTerm_SelectedIndexChanged;
			dataGridExpense.CellChange += dataGridExpense_CellChange;
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity")
			{
				AllocateQuantityToLot(e.Cell);
			}
		}

		private void dataGridExpense_CellChange(object sender, CellEventArgs e)
		{
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
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount" && dataGridItems.ActiveCell.Band.Index == 0)
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
			CalculateTotal();
		}

		private void dataGridExpense_AfterCellUpdate(object sender, CellEventArgs e)
		{
			string key = e.Cell.Column.Key;
			if (key == "Expense")
			{
				dataGridExpense.ActiveRow.Cells["Description"].Value = comboBoxGridExpenseCode.SelectedName;
				dataGridExpense.ActiveRow.Cells["Deductable"].Value = DefaultableBoolean.True;
			}
			if (e.Cell.Column.Key == "Deductable")
			{
				CalculateTotal();
			}
			if (key == "Amount")
			{
				CalculateTotal();
			}
		}

		private void textBoxDiscountPercent_Validated(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxDiscountAmount_Validated(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			try
			{
				UltraGridRow activeRow = dataGridItems.ActiveRow;
				if (activeRow != null && activeRow.Cells["Item Code"].Value != null && !(activeRow.Cells["Item Code"].Value.ToString() == "") && activeRow != null && activeRow.DataChanged)
				{
					if (!IsNewRecord)
					{
						_ = comboBoxSysDoc.SelectedID;
						_ = textBoxVoucherNumber.Text;
					}
					if (trackDetailedSales)
					{
						if (activeRow.ParentRow != null)
						{
							decimal result = default(decimal);
							decimal.TryParse(activeRow.ParentRow.Cells["Balance Qty"].Value.ToString(), out result);
							decimal d = default(decimal);
							foreach (UltraGridRow row in activeRow.ParentRow.ChildBands[0].Rows)
							{
								decimal result2 = default(decimal);
								decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out result2);
								d += result2;
							}
							if (d > result)
							{
								ErrorHelper.WarningMessage("Quantity invoiced cannot be more than balance quantity.");
								e.Cancel = true;
								dataGridItems.EnterEditMode(dataGridItems.ActiveCell);
							}
						}
					}
					else
					{
						decimal d2 = default(decimal);
						decimal.Parse(activeRow.Cells["Balance Qty"].Value.ToString());
						decimal result3 = default(decimal);
						decimal.TryParse(activeRow.Cells["Quantity"].Value.ToString(), out result3);
						if (result3 > d2)
						{
							ErrorHelper.WarningMessage("Quantity invoiced cannot be more than balance quantity.");
							e.Cancel = true;
							dataGridItems.EnterEditMode(dataGridItems.ActiveCell);
						}
					}
				}
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
			decimal d = default(decimal);
			num = decimal.Parse(textBoxExpenses.Text, NumberStyles.Any);
			num2 = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			if (!(num == 0m))
			{
				if (num > num2 + d)
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
			if (dataGridItems.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip1.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxCustomerName.Text = comboBoxCustomer.SelectedName;
			LoadCustomerBillingAddress();
			if (!isDataLoading && comboBoxCustomer.SelectedID != "")
			{
				string text = Factory.DatabaseSystem.GetFieldValue("Customer", "CurrencyID", "CustomerID", comboBoxCustomer.SelectedID).ToString();
				if (text != "")
				{
					comboBoxCurrency.SelectedID = text;
					comboBoxCurrency.GetLastRate();
					SetDueDate();
				}
				string text2 = Factory.DatabaseSystem.GetFieldValue("Customer", "PaymentTermID", "CustomerID", comboBoxCustomer.SelectedID).ToString();
				if (text2 != "")
				{
					comboBoxTerm.SelectedID = text2;
				}
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
					dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
					decimal productSalesPrice = Factory.ProductSystem.GetProductSalesPrice(comboBoxGridItem.SelectedID, comboBoxCustomer.SelectedID);
					dataGridItems.ActiveRow.Cells["Price"].Value = productSalesPrice;
					dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
					dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
					dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
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
						dataGridItems.ActiveRow.Cells["Location"].Value = dataGridItems.Rows[checked(dataGridItems.ActiveRow.Index - 1)].Cells["Location"].Value;
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
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			string key = e.Cell.Column.Key;
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal result3 = default(decimal);
			decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
			decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result2);
			decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
			if (key == "Quantity" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Quantity")
			{
				result3 = Math.Round(result * result2, Global.CurDecimalPoints);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
				if (dataGridItems.ActiveRow.Cells["Price"].Value == null || string.IsNullOrEmpty(dataGridItems.ActiveRow.Cells["Price"].Value.ToString()))
				{
					dataGridItems.ActiveRow.Cells["Price"].Value = 0;
				}
			}
			else if (key == "Price" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Price")
			{
				result3 = Math.Round(result * result2, Global.CurDecimalPoints);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
			}
			else if (key == "Amount" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Amount")
			{
				if (result == 0m)
				{
					result = 1m;
					dataGridItems.ActiveRow.Cells["Quantity"].Value = result;
				}
				ItemTypes itemTypes = ItemTypes.Inventory;
				if (dataGridItems.ActiveRow.HasChild())
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString()));
				}
				if ((itemTypes == ItemTypes.Discount && result3 > 0m) || (result < 0m && result3 > 0m))
				{
					result3 = -1m * Math.Abs(result3);
					dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
				}
				result2 = Math.Round(result3 / result, 4);
				if (result3 < 0m)
				{
					result = -1m * Math.Abs(result);
					dataGridItems.ActiveRow.Cells["Quantity"].Value = result;
				}
				dataGridItems.ActiveRow.Cells["Price"].Value = Math.Abs(result2);
			}
			else if (key == "Item Code")
			{
				if (dataGridItems.ActiveRow.Cells["Quantity"].Value == null || dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString() == "")
				{
					dataGridItems.ActiveRow.Cells["Quantity"].Value = result;
				}
				result3 = Math.Round(result * result2, Global.CurDecimalPoints);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
			}
			if (key == "Amount")
			{
				CalculateTotal();
			}
			if (trackDetailedSales && (key == "Quantity" || key == "Price" || key == "Amount"))
			{
				CalculateParentRowSummary(e.Cell.Row.ParentRow);
			}
			if (key == "ExpenseAmount")
			{
				CalculateExpenseValidation();
			}
		}

		private void CalculateExpenseValidation()
		{
			try
			{
				textBoxExpenseDiff.Visible = false;
				labelExpDiff.Visible = false;
				textBoxExpenseDiff.Text = 0.ToString(Format.TotalAmountFormat);
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal num = default(decimal);
				decimal.TryParse(textBoxExpenses.Text, out result);
				decimal.TryParse(textBoxExpenseDiff.Text, out result2);
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					decimal result3 = default(decimal);
					decimal.TryParse(row.Cells["ExpenseAmount"].Value.ToString(), out result3);
					result2 += result3;
					decimal num2 = default(decimal);
					decimal result4 = default(decimal);
					num2 = decimal.Parse(row.Cells["ExpenseAmount"].Value.ToString());
					num2 = Math.Round(num2, Global.CurDecimalPoints);
					decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out result4);
					if (result4 > 0m)
					{
						num2 /= result4;
					}
					else
					{
						num2 = default(decimal);
					}
					row.Cells["UnitExpense"].Value = Math.Round(num2, 5);
				}
				num = result - result2;
				if (num != 0m)
				{
					textBoxExpenseDiff.Text = num.ToString();
					textBoxExpenseDiff.Visible = true;
					labelExpDiff.Visible = true;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CalculateParentRowSummary(UltraGridRow parentRow)
		{
			if (parentRow != null)
			{
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				foreach (UltraGridRow row in parentRow.ChildBands[0].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out result);
					decimal.TryParse(row.Cells["Price"].Value.ToString(), out result2);
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result3);
					num += result;
					num2 += result3;
				}
				parentRow.Cells["Quantity"].Value = num;
				parentRow.Cells["Amount"].Value = num2;
				if (num != 0m)
				{
					parentRow.Cells["Price"].Value = Math.Round(num2 / num, 5);
				}
				else
				{
					parentRow.Cells["Price"].Value = 0;
				}
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
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
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && activeRow.DataChanged && dataGridItems.ActiveCell.Column.Key == "Quantity")
			{
				if (!IsNewRecord)
				{
					_ = comboBoxSysDoc.SelectedID;
					_ = textBoxVoucherNumber.Text;
				}
				if (trackDetailedSales)
				{
					if (activeRow.ParentRow == null)
					{
						return;
					}
					decimal result = default(decimal);
					decimal.TryParse(activeRow.ParentRow.Cells["Balance Qty"].Value.ToString(), out result);
					decimal d = default(decimal);
					foreach (UltraGridRow row in activeRow.ParentRow.ChildBands[0].Rows)
					{
						decimal result2 = default(decimal);
						decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out result2);
						d += result2;
					}
					if (d > result)
					{
						ErrorHelper.WarningMessage("Quantity invoiced cannot be more than balance quantity.");
						e.Cancel = true;
						dataGridItems.EnterEditMode(dataGridItems.ActiveCell);
						return;
					}
				}
				else
				{
					decimal result3 = default(decimal);
					decimal.TryParse(activeRow.Cells["Balance Qty"].Value.ToString(), out result3);
					decimal result4 = default(decimal);
					decimal.TryParse(activeRow.Cells["Quantity"].Value.ToString(), out result4);
					if (result4 > result3)
					{
						ErrorHelper.WarningMessage("Quantity invoiced cannot be more than balance quantity.");
						e.Cancel = true;
						dataGridItems.EnterEditMode(dataGridItems.ActiveCell);
						return;
					}
				}
			}
			if (dataGridItems.ActiveCell.Column.Key == "Item Code" && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() == "")
			{
				dataGridItems.ActiveRow.Cells["Description"].Value = "";
			}
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Price")
			{
				decimal result5 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result5);
				result5 = Math.Round(result5, 5);
				dataGridItems.ActiveCell.Value = result5;
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				decimal result6 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result6);
				result6 = Math.Round(result6, 4);
				dataGridItems.ActiveCell.Value = result6;
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
			{
				decimal result7 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result7);
				result7 = Math.Round(result7, Global.CurDecimalPoints);
				dataGridItems.ActiveCell.Value = result7;
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow == null)
			{
				return;
			}
			if (!trackDetailedSales && activeRow.Cells["Item Code"].Value.ToString() == "" && activeRow.Cells["Quantity"].Value.ToString() != "")
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
			if (e.Cell.Column.Key == "Quantity")
			{
				decimal result = default(decimal);
				if (!decimal.TryParse(e.NewValue.ToString(), out result) || result < 0m)
				{
					ErrorHelper.InformationMessage("Invalid quantity. Please enter a non negative numeric value.");
					e.Cancel = true;
					return;
				}
			}
			if (e.Cell.Column.Key == "Quantity" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Quantity" && !AllocateQuantityToLot(e.Cell))
			{
				e.Cancel = true;
			}
		}

		private bool AllocateQuantityToLot(UltraGridCell cell)
		{
			bool result = false;
			bool result2 = false;
			if (trackDetailedSales)
			{
				if (dataGridItems.ActiveRow.ParentRow == null)
				{
					return false;
				}
				if (dataGridItems.ActiveRow.ParentRow.Cells["IsTrackLot"].Value != null)
				{
					bool.TryParse(dataGridItems.ActiveRow.ParentRow.Cells["IsTrackLot"].Value.ToString(), out result);
				}
				if (dataGridItems.ActiveRow.ParentRow.Cells["IsTrackSerial"].Value != null)
				{
					bool.TryParse(dataGridItems.ActiveRow.ParentRow.Cells["IsTrackSerial"].Value.ToString(), out result2);
				}
				if (result)
				{
					if (dataGridItems.ActiveRow.ParentRow.Cells["Location"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select a location first.");
						return false;
					}
					if (dataGridItems.ActiveCell.Text == "")
					{
						return false;
					}
					IssueLotSelectionForm issueLotSelectionForm = new IssueLotSelectionForm();
					issueLotSelectionForm.ProductID = dataGridItems.ActiveRow.ParentRow.Cells["Item Code"].Value.ToString();
					issueLotSelectionForm.ProductDescription = dataGridItems.ActiveRow.ParentRow.Cells["Description"].Value.ToString();
					issueLotSelectionForm.LocationID = dataGridItems.ActiveRow.ParentRow.Cells["Location"].Value.ToString();
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
			}
			else
			{
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
					IssueLotSelectionForm issueLotSelectionForm2 = new IssueLotSelectionForm();
					issueLotSelectionForm2.ProductID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					issueLotSelectionForm2.ProductDescription = dataGridItems.ActiveRow.Cells["Description"].Value.ToString();
					issueLotSelectionForm2.LocationID = dataGridItems.ActiveRow.Cells["Location"].Value.ToString();
					issueLotSelectionForm2.RowQuantity = float.Parse(dataGridItems.ActiveCell.Text);
					if (!isNewRecord)
					{
						issueLotSelectionForm2.SysDocID = comboBoxSysDoc.SelectedID;
						issueLotSelectionForm2.VoucherID = textBoxVoucherNumber.Text;
					}
					if (cell.Tag != null)
					{
						issueLotSelectionForm2.ProductLotTable = (DataTable)cell.Tag;
					}
					if (issueLotSelectionForm2.ShowDialog() != DialogResult.OK)
					{
						return false;
					}
					cell.Tag = issueLotSelectionForm2.ProductLotTable;
					cell.Appearance.FontData.Underline = DefaultableBoolean.True;
				}
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
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ConsignOutSettlementData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ConsignOutSettlementTable.Rows[0] : currentData.ConsignOutSettlementTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["ConsignSysDocID"] = consignSysDocID;
				dataRow["ConsignVoucherID"] = consignVoucherID;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["CustomerAddress"] = textBoxBilltoAddress.Text;
				dataRow["IsCash"] = false;
				dataRow["DueDate"] = dateTimePickerDueDate.Value;
				if (comboBoxTerm.SelectedID != "")
				{
					dataRow["TermID"] = comboBoxTerm.SelectedID;
				}
				else
				{
					dataRow["TermID"] = DBNull.Value;
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
				dataRow["PONumber"] = textBoxSettleNumber.Text;
				dataRow["Total"] = textBoxTotal.Text;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.ConsignOutSettlementTable.Rows.Add(dataRow);
				}
				currentData.ConsignOutSettlementDetailTable.Rows.Clear();
				currentData.Tables["Product_Lot_Issue_Detail"].Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (trackDetailedSales)
					{
						foreach (UltraGridRow row2 in row.ChildBands[0].Rows)
						{
							DataRow dataRow2 = currentData.ConsignOutSettlementDetailTable.NewRow();
							dataRow2.BeginEdit();
							if (row2.Cells["Quantity"].Value != null && !(row2.Cells["Quantity"].Value.ToString() == "") && !(decimal.Parse(row2.Cells["Quantity"].Value.ToString()) == 0m))
							{
								dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
								dataRow2["Quantity"] = row2.Cells["Quantity"].Value.ToString();
								if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
								{
									dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
								}
								if (row.Cells["UnitExpense"].Value != null && row.Cells["UnitExpense"].Value.ToString() != "")
								{
									dataRow2["UnitExpense"] = row.Cells["UnitExpense"].Value.ToString();
									dataRow2["ExpenseAmount"] = row.Cells["ExpenseAmount"].Value.ToString();
								}
								dataRow2["ConsignSysDocID"] = consignSysDocID;
								dataRow2["ConsignVoucherID"] = consignVoucherID;
								dataRow2["ConsignRowIndex"] = row.Cells["ConsignRowIndex"].Value.ToString();
								dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
								dataRow2["UnitPrice"] = row2.Cells["Price"].Value.ToString();
								dataRow2["Amount"] = row2.Cells["Amount"].Value.ToString();
								if (row2.Cells["Description"].Value.ToString() == "")
								{
									dataRow2["Description"] = row.Cells["Description"].Value.ToString();
								}
								else
								{
									dataRow2["Description"] = row2.Cells["Description"].Value.ToString();
								}
								dataRow2["RowIndex"] = row2.Index;
								dataRow2.EndEdit();
								currentData.ConsignOutSettlementDetailTable.Rows.Add(dataRow2);
								string b = row.Cells["Location"].Value.ToString();
								string text = row.Cells["Item Code"].Value.ToString();
								if (row2.Cells["Quantity"].Tag != null)
								{
									foreach (DataRow row3 in (row2.Cells["Quantity"].Tag as DataTable).Rows)
									{
										if (row3["LocationID"].ToString() != b || row3["ProductID"].ToString() != text)
										{
											ErrorHelper.WarningMessage("Location or Product code mismatch with selected lots for item code: '" + text + "'.\nPlease reallocate the lots for this item.");
											return false;
										}
										DataRow dataRow4 = currentData.Tables["Product_Lot_Issue_Detail"].NewRow();
										dataRow4["ProductID"] = row3["ProductID"];
										dataRow4["LocationID"] = row3["LocationID"];
										dataRow4["Reference"] = row3["Reference"];
										dataRow4["SourceLotNumber"] = row3["SourceLotNumber"];
										dataRow4["LotNumber"] = row3["LotNumber"];
										dataRow4["SoldQty"] = row3["SoldQty"];
										dataRow4["SysDocID"] = comboBoxSysDoc.SelectedID;
										dataRow4["Cost"] = row3["Cost"];
										dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
										dataRow4["UnitPrice"] = row.Cells["Price"].Value.ToString();
										dataRow4["RowIndex"] = row.Index;
										currentData.Tables["Product_Lot_Issue_Detail"].Rows.Add(dataRow4);
									}
								}
							}
						}
					}
					else
					{
						DataRow dataRow5 = currentData.ConsignOutSettlementDetailTable.NewRow();
						dataRow5.BeginEdit();
						if (row.Cells["Quantity"].Value != null && !(row.Cells["Quantity"].Value.ToString() == "") && !(decimal.Parse(row.Cells["Quantity"].Value.ToString()) == 0m))
						{
							dataRow5["ProductID"] = row.Cells["Item Code"].Value.ToString();
							dataRow5["Quantity"] = row.Cells["Quantity"].Value.ToString();
							if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
							{
								dataRow5["UnitID"] = row.Cells["Unit"].Value.ToString();
							}
							if (row.Cells["UnitExpense"].Value != null && row.Cells["UnitExpense"].Value.ToString() != "")
							{
								dataRow5["UnitExpense"] = row.Cells["UnitExpense"].Value.ToString();
								dataRow5["ExpenseAmount"] = row.Cells["ExpenseAmount"].Value.ToString();
							}
							dataRow5["ConsignSysDocID"] = consignSysDocID;
							dataRow5["ConsignVoucherID"] = consignVoucherID;
							dataRow5["ConsignRowIndex"] = row.Cells["ConsignRowIndex"].Value.ToString();
							dataRow5["LocationID"] = row.Cells["Location"].Value.ToString();
							dataRow5["UnitPrice"] = row.Cells["Price"].Value.ToString();
							dataRow5["Amount"] = row.Cells["Amount"].Value.ToString();
							dataRow5["Description"] = row.Cells["Description"].Value.ToString();
							dataRow5["RowIndex"] = row.Index;
							dataRow5.EndEdit();
							currentData.ConsignOutSettlementDetailTable.Rows.Add(dataRow5);
							string b2 = row.Cells["Location"].Value.ToString();
							string text2 = row.Cells["Item Code"].Value.ToString();
							if (row.Cells["Quantity"].Tag != null)
							{
								foreach (DataRow row4 in (row.Cells["Quantity"].Tag as DataTable).Rows)
								{
									if (row4["LocationID"].ToString() != b2 || row4["ProductID"].ToString() != text2)
									{
										ErrorHelper.WarningMessage("Location or Product code mismatch with selected lots for item code: '" + text2 + "'.\nPlease reallocate the lots for this item.");
										return false;
									}
									DataRow dataRow7 = currentData.Tables["Product_Lot_Issue_Detail"].NewRow();
									dataRow7["ProductID"] = row4["ProductID"];
									dataRow7["LocationID"] = row4["LocationID"];
									dataRow7["LotNumber"] = row4["LotNumber"];
									dataRow7["Reference"] = row4["Reference"];
									dataRow7["SourceLotNumber"] = row4["SourceLotNumber"];
									dataRow7["SoldQty"] = row4["SoldQty"];
									dataRow7["SysDocID"] = comboBoxSysDoc.SelectedID;
									dataRow7["Cost"] = row4["Cost"];
									dataRow7["VoucherID"] = textBoxVoucherNumber.Text;
									dataRow7["UnitPrice"] = row.Cells["Price"].Value.ToString();
									dataRow7["RowIndex"] = row.Index;
									currentData.Tables["Product_Lot_Issue_Detail"].Rows.Add(dataRow7);
								}
							}
						}
					}
				}
				if (deliveryNoteTable != null)
				{
					currentData.Tables.Add(deliveryNoteTable);
				}
				currentData.ExpenseTable.Rows.Clear();
				foreach (UltraGridRow row5 in dataGridExpense.Rows)
				{
					DataRow dataRow8 = currentData.ExpenseTable.NewRow();
					dataRow8.BeginEdit();
					dataRow8["ExpenseID"] = row5.Cells["Expense"].Value.ToString();
					dataRow8["Description"] = row5.Cells["Description"].Value.ToString();
					dataRow8["Reference"] = row5.Cells["Ref"].Value.ToString();
					dataRow8["RowIndex"] = row5.Index;
					dataRow8["Amount"] = row5.Cells["Amount"].Value.ToString();
					dataRow8["CurrencyID"] = comboBoxCurrency.SelectedID;
					dataRow8["CurrencyRate"] = comboBoxCurrency.Rate;
					dataRow8["RateType"] = comboBoxCurrency.RateType;
					dataRow8["IsDeduct"] = bool.Parse(row5.Cells["Deductable"].Value.ToString());
					dataRow8.EndEdit();
					currentData.ExpenseTable.Rows.Add(dataRow8);
				}
				if (currentData.ConsignOutSettlementDetailTable.Rows.Count == 0)
				{
					ErrorHelper.WarningMessage("You must have at least one row to settle.");
					return false;
				}
				return true;
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
				DataSet dataSet = new DataSet();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("ConsignRowIndex", typeof(int));
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("ConsignLocationID");
				dataTable.Columns.Add("ItemType");
				dataTable.Columns.Add("IsTrackLot");
				dataTable.Columns.Add("IsTrackSerial");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("IsDNRow");
				dataTable.Columns.Add("UnitExpense", typeof(decimal));
				dataTable.Columns.Add("ExpenseAmount", typeof(decimal));
				dataTable.Columns.Add("Consign QTY", typeof(decimal));
				dataTable.Columns.Add("Balance QTY", typeof(decimal));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				if (trackDetailedSales)
				{
					DataTable dataTable2 = new DataTable();
					dataTable2.Columns.Add("ConsignRowIndex", typeof(int));
					dataTable2.Columns.Add("Item Code");
					dataTable2.Columns.Add("Description");
					dataTable2.Columns.Add("Quantity", typeof(decimal));
					dataTable2.Columns.Add("Price", typeof(decimal));
					dataTable2.Columns.Add("Amount", typeof(decimal));
					dataSet.Tables.Add(dataTable);
					dataSet.Tables.Add(dataTable2);
					dataSet.Relations.Add("REL", dataTable.Columns["ConsignRowIndex"], dataTable2.Columns["ConsignRowIndex"], createConstraints: true);
					dataGridItems.DataSource = dataSet;
				}
				else
				{
					dataGridItems.DataSource = dataTable;
				}
				dataGridItems.ShowDeleteMenu = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["UnitExpense"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ExpenseAmount"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsDNRow"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignRowIndex"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignLocationID"].Hidden = true;
				AdjustGridColumnSettings();
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 0.0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Consign QTY"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Balance Qty"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Balance Qty"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Consign Qty"].CellActivation = Activation.NoEdit;
				bool hidden;
				if (trackDetailedSales)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.NoEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.NoEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.NoEdit;
					AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance;
					AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellAppearance;
					Color color = dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.BackColor = Color.WhiteSmoke;
					Color color4 = cellAppearance.BackColor = (cellAppearance2.BackColor = color);
					dataGridItems.DisplayLayout.Bands[1].Override.AllowAddNew = AllowAddNew.Yes;
					UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[1].Columns["Item Code"];
					hidden = (dataGridItems.DisplayLayout.Bands[1].Columns["ConsignRowIndex"].Hidden = true);
					ultraGridColumn.Hidden = hidden;
					dataGridItems.DisplayLayout.Bands[1].Columns["ConsignRowIndex"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
					dataGridItems.DisplayLayout.Bands[1].Indentation = 20;
					dataGridItems.DisplayLayout.Bands[1].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.AllowEdit;
				}
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].Hidden = true);
				ultraGridColumn2.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
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
				dataTable.Columns.Add("Expense");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Ref");
				dataTable.Columns.Add("Deductable", typeof(bool));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridExpense.DataSource = dataTable;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].Hidden = true;
				dataGridExpense.LoadLayout();
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].CharacterCasing = CharacterCasing.Upper;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].MaxLength = 64;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].ValueList = comboBoxGridExpenseCode;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].DefaultCellValue = DefaultableBoolean.True;
				if (dataGridExpense.DisplayLayout.Bands[0].Columns.Exists("Deductable"))
				{
					dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].CellActivation = Activation.AllowEdit;
					dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].CellClickAction = CellClickAction.Edit;
					dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
				}
				dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].MaxLength = 20;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.AllowEdit;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.BackColor = Color.White;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].Width = checked(7 * dataGridExpense.Width) / 100;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].Width = checked(5 * dataGridExpense.Width) / 100;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(7 * dataGridExpense.Width) / 100;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Deductable"].Width = checked(2 * dataGridExpense.Width) / 100;
				dataGridExpense.SetupUI();
			}
			catch (Exception e)
			{
				dataGridExpense.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void AdjustGridColumnSettings()
		{
			Color backColor = Color.FromArgb(240, 240, 240);
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColor = backColor;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColor = backColor;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColor = backColor;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Consign QTY"].CellAppearance.BackColor = backColor;
			dataGridItems.DisplayLayout.Bands[0].Columns["Consign QTY"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Balance Qty"].CellAppearance.BackColor = backColor;
			dataGridItems.DisplayLayout.Bands[0].Columns["Balance Qty"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = true;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = -99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["Consign QTY"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Consign QTY"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Consign QTY"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["Consign QTY"].MinValue = -99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["Consign QTY"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["Balance Qty"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Balance Qty"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Balance Qty"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["Balance Qty"].MinValue = -99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["Balance Qty"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Format = "#,0.00##";
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxValue = new decimal(999999999999L);
			if (trackDetailedSales)
			{
				dataGridItems.DisplayLayout.Bands[1].Columns["Quantity"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[1].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Quantity"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[1].Columns["Quantity"].MinValue = -99999999m;
				dataGridItems.DisplayLayout.Bands[1].Columns["Quantity"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].MinValue = new decimal(-999999999999L);
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].MaxValue = new decimal(999999999999L);
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["ExpenseAmount"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["ExpenseAmount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["ExpenseAmount"].Format = Format.GridAmountFormat;
			dataGridItems.DisplayLayout.Bands[0].Columns["ExpenseAmount"].MinValue = new decimal(-999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["ExpenseAmount"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["UnitExpense"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["UnitExpense"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["UnitExpense"].Format = Format.GridAmountFormat;
			dataGridItems.DisplayLayout.Bands[0].Columns["UnitExpense"].MinValue = new decimal(-999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["UnitExpense"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = new decimal(-999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("TotalQty"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"]);
			}
			dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
			if (trackDetailedSales && !dataGridItems.DisplayLayout.Bands[1].Summaries.Exists("Qty1"))
			{
				dataGridItems.DisplayLayout.Bands[1].Summaries.Add("Qty1", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[1].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[1].Summaries.Add("Amount1", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[1].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[1].Summaries["Qty1"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[1].Summaries["Qty1"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Summaries["Qty1"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[1].Summaries["Qty1"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[1].Summaries["Amount1"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[1].Summaries["Amount1"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Summaries["Amount1"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[1].Summaries["Amount1"].DisplayFormat = "{0:n}";
			}
		}

		private void CalculateExpensePerItem()
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
				decimal result3 = default(decimal);
				decimal.TryParse(row3.Cells["Amount"].Value.ToString(), out result3);
				decimal result4 = default(decimal);
				decimal.TryParse(row3.Cells["Quantity"].Value.ToString(), out result4);
				decimal num4 = default(decimal);
				ItemTypes itemTypes3 = ItemTypes.Inventory;
				if (row3.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes3 = (ItemTypes)checked((byte)int.Parse(row3.Cells["ItemType"].Value.ToString()));
				}
				if (itemTypes3 == ItemTypes.Inventory)
				{
					num4 = ((num3 != num2) ? (result3 * d) : (result - d2));
					num4 = Math.Round(num4, Global.CurDecimalPoints);
					d2 += num4;
					row3.Cells["ExpenseAmount"].Value = Math.Round(num4, Global.CurDecimalPoints);
					if (result4 > 0m)
					{
						num4 /= result4;
					}
					else
					{
						num4 = default(decimal);
					}
					row3.Cells["UnitExpense"].Value = Math.Round(num4, 5);
					num3 = checked(num3 + 1);
				}
			}
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
				labelExpDiff.Visible = false;
				textBoxExpenseDiff.Visible = false;
				textBoxExpenseDiff.Text = 0.ToString(Format.TotalAmountFormat);
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.ConsignOutSettlementSystem.GetSettlementByID(SystemDocID, voucherID);
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
					bool flag = false;
					DataRow dataRow = currentData.Tables["ConsignOut_Settlement"].Rows[0];
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					textBoxConsignment.Text = dataRow["ConsignVoucherID"].ToString();
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					dateTimePickerDueDate.Value = DateTime.Parse(dataRow["DueDate"].ToString());
					if (dataRow["CurrencyRate"] != DBNull.Value)
					{
						comboBoxCurrency.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
					}
					else
					{
						comboBoxCurrency.Rate = 1m;
					}
					comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
					if (dataRow["CurrencyID"] != DBNull.Value && dataRow["CurrencyID"].ToString() != Global.BaseCurrencyID)
					{
						flag = true;
					}
					comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
					textBoxSettleNumber.Text = dataRow["PONumber"].ToString();
					consignSysDocID = dataRow["ConsignSysDocID"].ToString();
					consignVoucherID = dataRow["ConsignVoucherID"].ToString();
					if (dataRow["TotalFC"] != DBNull.Value)
					{
						textBoxTotal.Text = decimal.Parse(dataRow["TotalFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTotal.Text = decimal.Parse(dataRow["Total"].ToString()).ToString(Format.TotalAmountFormat);
					}
					DataTable dataTable = null;
					DataTable dataTable2 = null;
					if (trackDetailedSales)
					{
						DataSet obj = dataGridItems.DataSource as DataSet;
						dataTable = obj.Tables[0];
						dataTable2 = obj.Tables[1];
						dataTable2.Rows.Clear();
						dataTable.Rows.Clear();
					}
					else
					{
						dataTable = (dataGridItems.DataSource as DataTable);
						dataTable.Rows.Clear();
					}
					if (currentData.Tables.Contains("ConsignOut_Settlement_Detail") && currentData.ConsignOutSettlementDetailTable.Rows.Count != 0)
					{
						decimal num = default(decimal);
						decimal num2 = default(decimal);
						foreach (DataRow row in currentData.Tables["ConsignOut_Settlement_Detail"].Rows)
						{
							string text = row["ProductID"].ToString();
							int num3 = int.Parse(row["ConsignRowIndex"].ToString());
							if (trackDetailedSales)
							{
								DataRow dataRow3;
								if (dataGridItems.ExistCellValue("ConsignRowIndex", num3.ToString()) < 0)
								{
									dataRow3 = dataTable.NewRow();
									dataRow3["Item Code"] = row["ProductID"];
									dataRow3["Quantity"] = 0;
									if (row["DeliveredQty"] != DBNull.Value)
									{
										dataRow3["Consign QTY"] = row["DeliveredQty"];
									}
									dataRow3["Balance Qty"] = row["BalanceQty"];
									dataRow3["ConsignRowIndex"] = row["ConsignRowIndex"];
									dataRow3["Description"] = row["Description"];
									dataRow3["Location"] = row["LocationID"];
									dataRow3["Unit"] = row["UnitID"];
									dataRow3["ItemType"] = row["ItemType"];
									dataRow3["IsTrackLot"] = row["IsTrackLot"];
									dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
									dataRow3["Price"] = 0;
									dataRow3["Amount"] = 0;
									dataRow3["UnitExpense"] = row["UnitExpense"];
									dataRow3["ExpenseAmount"] = row["ExpenseAmount"];
									dataRow3.EndEdit();
									dataTable.Rows.Add(dataRow3);
								}
								else
								{
									text = text;
								}
								dataRow3 = dataTable2.NewRow();
								dataRow3["Item Code"] = row["ProductID"];
								dataRow3["ConsignRowIndex"] = row["ConsignRowIndex"];
								dataRow3["Description"] = row["Description"];
								if (row["UnitQuantity"] != DBNull.Value)
								{
									dataRow3["Quantity"] = row["UnitQuantity"];
								}
								else
								{
									dataRow3["Quantity"] = row["Quantity"];
								}
								if (flag)
								{
									dataRow3["Price"] = decimal.Parse(row["UnitPriceFC"].ToString()).ToString(Format.UnitPriceFormat);
								}
								else
								{
									dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
								}
								num = default(decimal);
								num2 = default(decimal);
								decimal.TryParse(dataRow3["Quantity"].ToString(), out num);
								decimal.TryParse(dataRow3["Price"].ToString(), out num2);
								if (flag)
								{
									dataRow3["Amount"] = Math.Round(decimal.Parse(row["AmountFC"].ToString()), Global.CurDecimalPoints);
								}
								else
								{
									dataRow3["Amount"] = Math.Round(decimal.Parse(row["Amount"].ToString()), Global.CurDecimalPoints);
								}
								dataRow3.EndEdit();
								dataTable2.Rows.Add(dataRow3);
							}
							else
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
								if (row["DeliveredQty"] != DBNull.Value)
								{
									dataRow3["Consign QTY"] = row["DeliveredQty"];
								}
								dataRow3["Balance Qty"] = row["BalanceQty"];
								dataRow3["ConsignRowIndex"] = row["ConsignRowIndex"];
								dataRow3["Description"] = row["Description"];
								dataRow3["Location"] = row["LocationID"];
								dataRow3["Unit"] = row["UnitID"];
								dataRow3["ItemType"] = row["ItemType"];
								dataRow3["IsTrackLot"] = row["IsTrackLot"];
								dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
								if (flag)
								{
									dataRow3["Price"] = decimal.Parse(row["UnitPriceFC"].ToString()).ToString(Format.UnitPriceFormat);
								}
								else
								{
									dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
								}
								num = default(decimal);
								num2 = default(decimal);
								decimal.TryParse(dataRow3["Quantity"].ToString(), out num);
								decimal.TryParse(dataRow3["Price"].ToString(), out num2);
								if (flag)
								{
									dataRow3["Amount"] = Math.Round(decimal.Parse(row["AmountFC"].ToString()), Global.CurDecimalPoints);
								}
								else
								{
									dataRow3["Amount"] = Math.Round(decimal.Parse(row["Amount"].ToString()), Global.CurDecimalPoints);
								}
								dataRow3.EndEdit();
								dataTable.Rows.Add(dataRow3);
							}
						}
						dataTable.AcceptChanges();
						dataTable2.AcceptChanges();
						dataTable = (dataGridExpense.DataSource as DataTable);
						dataTable.Clear();
						foreach (DataRow row2 in currentData.Tables["ConsignOut_Expense"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							bool flag2 = false;
							dataRow3["Expense"] = row2["ExpenseID"];
							dataRow3["Description"] = row2["Description"];
							dataRow3["Ref"] = row2["Reference"];
							if (row2["CurrencyID"].ToString() != "" && row2["CurrencyID"].ToString() != Global.BaseCurrencyID)
							{
								flag2 = true;
							}
							if (flag2)
							{
								dataRow3["Amount"] = decimal.Parse(row2["AmountFC"].ToString()).ToString(Format.TotalAmountFormat);
							}
							else
							{
								dataRow3["Amount"] = decimal.Parse(row2["Amount"].ToString()).ToString(Format.TotalAmountFormat);
							}
							bool result = true;
							bool.TryParse(row2["Deductable"].ToString(), out result);
							dataRow3["Deductable"] = result;
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
						foreach (UltraGridRow row3 in dataGridItems.Rows)
						{
							if (row3.Cells["IsDNRow"].Value != null && row3.Cells["IsDNRow"].Value.ToString() != "" && bool.Parse(row3.Cells["IsDNRow"].Value.ToString()))
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
							string productID = row3.Cells["Item Code"].Value.ToString();
							row3.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
							if (checked((byte)int.Parse(row3.Cells["ItemType"].Value.ToString())) == 4)
							{
								row3.Cells["Quantity"].Activation = Activation.Disabled;
							}
							if (trackDetailedSales)
							{
								foreach (UltraGridRow row4 in row3.ChildBands[0].Rows)
								{
									if (row3.Cells["IsTrackLot"].Value != DBNull.Value && bool.Parse(row3.Cells["IsTrackLot"].Value.ToString()))
									{
										DataRow[] array = currentData.Tables["Product_Lot_Issue_Detail"].Select("ProductID = '" + row3.Cells["Item Code"].Value.ToString() + "' AND RowIndex = " + row4.Index);
										if (array.Length != 0)
										{
											DataSet dataSet = new DataSet();
											dataSet.Merge(array);
											DataTable tag = dataSet.Tables[0];
											row4.Cells["Quantity"].Tag = tag;
											row4.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
										}
									}
								}
							}
							else if (row3.Cells["IsTrackLot"].Value != DBNull.Value && bool.Parse(row3.Cells["IsTrackLot"].Value.ToString()))
							{
								DataRow[] array2 = currentData.Tables["Product_Lot_Issue_Detail"].Select("ProductID = '" + row3.Cells["Item Code"].Value.ToString() + "' AND RowIndex = " + row3.Index);
								if (array2.Length != 0)
								{
									DataSet dataSet2 = new DataSet();
									dataSet2.Merge(array2);
									DataTable tag2 = dataSet2.Tables[0];
									row3.Cells["Quantity"].Tag = tag2;
									row3.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
								}
							}
						}
						foreach (UltraGridRow row5 in dataGridItems.Rows)
						{
							CalculateParentRowSummary(row5);
						}
						CalculateTotal();
					}
				}
			}
			catch (Exception)
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
			try
			{
				bool flag = Factory.ConsignOutSettlementSystem.CreateSettlement(currentData, !isNewRecord);
				if (!flag)
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
				return flag;
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("ConsignOut_Settlement", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
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
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxCustomer.SelectedID == "")
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
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
						continue;
					}
					UltraGridRow ultraGridRow = dataGridItems.Rows[i];
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
					if (ultraGridRow.HasChild())
					{
						foreach (UltraGridRow row in ultraGridRow.ChildBands[0].Rows)
						{
							if (row.Cells["Quantity"].Value.ToString() != null && row.Cells["Quantity"].Value.ToString() != "" && (row.Cells["Price"].Value.ToString() == "" || row.Cells["Amount"].Value.ToString() == ""))
							{
								ErrorHelper.InformationMessage("Some rows does not have information. Please enter information for all rows.");
								row.Activate();
								return false;
							}
						}
					}
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one row of item.");
					return false;
				}
				if (comboBoxCustomer.IsCustomerOnHold)
				{
					ErrorHelper.WarningMessage("This customer is on hold status and does not allow transaction.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("ConsignOut_Settlement", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
								if (new ApprovalPasswordForm
								{
									CorrectPassword = CompanyPreferences.OverCLPassword
								}.ShowDialog() == DialogResult.Cancel)
								{
									return false;
								}
							}
						}
					}
				}
				if (double.Parse(textBoxExpenseDiff.Text, NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands) != 0.0)
				{
					ErrorHelper.WarningMessage("Please validate the expenses!");
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
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				textBoxCustomerName.Clear();
				comboBoxCustomer.Clear();
				textBoxSettleNumber.Clear();
				comboBoxTerm.Clear();
				textBoxSubtotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxBilltoAddress.Clear();
				textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				string text3 = textBoxExpenses.Text = (textBoxTotalExpense.Text = 0.ToString(Format.TotalAmountFormat));
				comboBoxCustomer.Enabled = true;
				comboBoxSalesperson.Clear();
				labelExpDiff.Visible = false;
				textBoxExpenseDiff.Visible = false;
				textBoxConsignment.Clear();
				textBoxExpenseDiff.Text = 0.ToString(Format.TotalAmountFormat);
				if (trackDetailedSales)
				{
					DataSet obj = dataGridItems.DataSource as DataSet;
					obj.Tables[1].Rows.Clear();
					obj.Tables[0].Rows.Clear();
				}
				else
				{
					(dataGridItems.DataSource as DataTable).Rows.Clear();
				}
				(dataGridExpense.DataSource as DataTable).Rows.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				deliveryNoteTable = null;
				AdjustGridColumnSettings();
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
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.ConsignOutSettlementSystem.DeleteSettlement(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("ConsignOut_Settlement", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("ConsignOut_Settlement", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("ConsignOut_Settlement", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("ConsignOut_Settlement", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("ConsignOut_Settlement", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				return;
			}
			dataGridItems.SaveLayout();
			dataGridExpense.SaveLayout();
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
				dataGridItems.SetupUI();
				dataGridExpense.SetupUI();
				SetupGrid();
				SetupExpenseGrid();
				UltraFormattedLinkLabel ultraFormattedLinkLabel = ultraFormattedLinkCurrency;
				bool visible = comboBoxCurrency.Visible = CompanyPreferences.UseMultiCurrency;
				ultraFormattedLinkLabel.Visible = visible;
				comboBoxSysDoc.FilterByType(SysDocTypes.ConsignOutSettlement);
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
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
			}
		}

		private void SetupForm()
		{
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
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
				return Factory.ConsignOutSettlementSystem.VoidSettlement(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.ConsignOutSettlement);
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

		private void SetDueDate()
		{
			dateTimePickerDueDate.Value = Global.CalculateDueDate(dateTimePickerDate.Value, comboBoxTerm.SelectedID);
		}

		private void CalculateTotal()
		{
			decimal d = default(decimal);
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal d2 = default(decimal);
			if (trackDetailedSales)
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					foreach (UltraGridRow row2 in row.ChildBands[0].Rows)
					{
						if (row2.Cells["Amount"].Value != null && !(row2.Cells["Amount"].Value.ToString() == ""))
						{
							decimal.TryParse(row2.Cells["Amount"].Value.ToString(), out result);
							d += result;
						}
					}
				}
			}
			else
			{
				foreach (UltraGridRow row3 in dataGridItems.Rows)
				{
					if (row3.Cells["Amount"].Value != null && !(row3.Cells["Amount"].Value.ToString() == ""))
					{
						decimal.TryParse(row3.Cells["Amount"].Value.ToString(), out result);
						d += result;
					}
				}
			}
			textBoxSubtotal.Text = d.ToString(Format.TotalAmountFormat);
			foreach (UltraGridRow row4 in dataGridExpense.Rows)
			{
				if (row4.Cells["Amount"].Value != null && !(row4.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row4.Cells["Amount"].Value.ToString(), out result);
					if (Convert.ToBoolean(row4.Cells["Deductable"].Value.ToString()))
					{
						d2 += result;
					}
				}
			}
			string text3 = textBoxTotalExpense.Text = (textBoxExpenses.Text = d2.ToString(Format.TotalAmountFormat));
			num = d - d2;
			textBoxTotal.Text = num.ToString(Format.TotalAmountFormat);
			if (!isDataLoading)
			{
				CalculateExpensePerItem();
				CalculateExpenseValidation();
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
			if (!(currentCustomerAddressID == ""))
			{
				new FormHelper().EditCustomerAddress(comboBoxCustomer.SelectedID, currentCustomerAddressID);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomer(comboBoxCustomer.SelectedID);
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
					DataSet settlementToPrint = Factory.ConsignOutSettlementSystem.GetSettlementToPrint(selectedID, text);
					if (settlementToPrint == null || settlementToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(settlementToPrint, selectedID, "Consignment Out Settlement", SysDocTypes.ConsignOutSettlement, isPrint, showPrintDialog);
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

		private void buttonSelectConsignment_Click(object sender, EventArgs e)
		{
			DataSet openConsignments = Factory.ConsignOutSystem.GetOpenConsignments(comboBoxCustomer.SelectedID);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.IsMultiSelect = false;
			selectDocumentDialog.DataSource = openConsignments;
			selectDocumentDialog.Text = "Select Consignment for Settlement";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				ClearForm();
				bool flag = false;
				int num = 0;
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					string sysDocID = selectedRow.Cells["Doc ID"].Value.ToString();
					string text = selectedRow.Cells["Number"].Value.ToString();
					textBoxConsignment.Text = text;
					consignSysDocID = sysDocID;
					consignVoucherID = text;
					num = checked(num + 1);
					ConsignOutData consignOutByID = Factory.ConsignOutSystem.GetConsignOutByID(sysDocID, text);
					if (!flag)
					{
						DataRow dataRow = consignOutByID.ConsignOutTable.Rows[0];
						textBoxNote.Text = dataRow["Note"].ToString();
						if (comboBoxCustomer.SelectedID == "")
						{
							comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
						}
						textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
						flag = true;
					}
					DataTable dataTable = null;
					DataTable dataTable2 = null;
					if (trackDetailedSales)
					{
						DataSet obj = dataGridItems.DataSource as DataSet;
						dataTable = obj.Tables[0];
						dataTable2 = obj.Tables[1];
						dataTable2.Rows.Clear();
						dataTable.Rows.Clear();
					}
					else
					{
						dataTable = (dataGridItems.DataSource as DataTable);
						dataTable.Rows.Clear();
					}
					if (!consignOutByID.Tables.Contains("Consign_Out_Detail") || consignOutByID.ConsignOutDetailTable.Rows.Count == 0)
					{
						break;
					}
					foreach (DataRow row in consignOutByID.Tables["Consign_Out_Detail"].Rows)
					{
						row["ProductID"].ToString();
						int num2 = int.Parse(row["RowIndex"].ToString());
						if (trackDetailedSales)
						{
							DataRow dataRow3;
							if (dataGridItems.ExistCellValue("ConsignRowIndex", num2.ToString()) < 0)
							{
								dataRow3 = dataTable.NewRow();
								dataRow3["Item Code"] = row["ProductID"];
								dataRow3["Balance Qty"] = row["QuantityBalance"];
								dataRow3["ConsignRowIndex"] = row["RowIndex"];
								dataRow3["Description"] = row["Description"];
								dataRow3["Location"] = row["ConsignLocationID"];
								dataRow3["Unit"] = row["UnitID"];
								dataRow3["ItemType"] = row["ItemType"];
								if (row["UnitQuantity"] != DBNull.Value)
								{
									dataRow3["Consign QTY"] = row["UnitQuantity"];
								}
								else
								{
									dataRow3["Consign QTY"] = row["Quantity"];
								}
								dataRow3["IsTrackLot"] = row["IsTrackLot"];
								dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
								dataRow3.EndEdit();
								dataTable.Rows.Add(dataRow3);
							}
							dataRow3 = dataTable2.NewRow();
							dataRow3["Item Code"] = row["ProductID"];
							dataRow3.EndEdit();
							dataTable2.Rows.Add(dataRow3);
						}
						else
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Item Code"] = row["ProductID"];
							if (row["UnitQuantity"] != DBNull.Value)
							{
								dataRow3["Consign QTY"] = row["UnitQuantity"];
							}
							else
							{
								dataRow3["Consign QTY"] = row["Quantity"];
							}
							dataRow3["Balance Qty"] = row["QuantityBalance"];
							dataRow3["ConsignRowIndex"] = row["RowIndex"];
							dataRow3["IsTrackLot"] = row["IsTrackLot"];
							dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
							dataRow3["ItemType"] = row["ItemType"];
							dataRow3["Description"] = row["Description"];
							dataRow3["Location"] = row["ConsignLocationID"];
							dataRow3["Unit"] = row["UnitID"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
					}
					comboBoxCustomer.Enabled = false;
					CalculateTotal();
				}
			}
		}

		private void ConsignOutSettlementForm_Load(object sender, EventArgs e)
		{
		}

		private void mmLabel2_Click(object sender, EventArgs e)
		{
		}

		private void textBoxConsignment_TextChanged(object sender, EventArgs e)
		{
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTransaction(TransactionListType.ConsignOut, consignSysDocID, consignVoucherID);
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
						Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.ConsignOutSettlement);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 48.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.SalesQuote);
					currentData = (dataSet as ConsignOutSettlementData);
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

		private void ultraFormattedLinkCurrency_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(comboBoxCurrency.SelectedID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ConsignmentOutSettlementListFormObj);
		}

		private void toolStripButtonMultiPreview_Click(object sender, EventArgs e)
		{
			AttachementDetailsForm attachementDetailsForm = new AttachementDetailsForm();
			attachementDetailsForm.SysDocID = comboBoxSysDoc.SelectedID;
			attachementDetailsForm.VoucherID = textBoxVoucherNumber.Text;
			attachementDetailsForm.SysDocType = SysDocTypes.ConsignOutSettlement;
			attachementDetailsForm.FormType = "Transaction";
			attachementDetailsForm.Show();
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxSalesperson.SelectedID);
		}

		private void toolStripButtonPaymentAllocation_Click(object sender, EventArgs e)
		{
			DataSet paymentAllocationDetails = Factory.SalesInvoiceSystem.GetPaymentAllocationDetails(SystemDocID, textBoxVoucherNumber.Text);
			decimal num = default(decimal);
			decimal result = default(decimal);
			if (paymentAllocationDetails != null && paymentAllocationDetails.Tables.Count > 0 && paymentAllocationDetails.Tables[0].Rows.Count > 0)
			{
				PaymentAllocationDialog paymentAllocationDialog = new PaymentAllocationDialog();
				paymentAllocationDialog.EntityType = EntityTypesEnum.Customers;
				paymentAllocationDialog.DataSource = paymentAllocationDetails;
				foreach (DataRow row in paymentAllocationDetails.Tables[0].Rows)
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

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind.PerformClick();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.ConsignOutSettlementForm));
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxGridProductUnit = new Micromind.DataControls.ProductUnitComboBox();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxTotalExpense = new Micromind.UISupport.AmountTextBox();
			label4 = new System.Windows.Forms.Label();
			dataGridExpense = new Micromind.DataControls.DataEntryGrid();
			comboBoxGridExpenseCode = new Micromind.DataControls.ExpenseCodeComboBox();
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
			toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMultiPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPaymentAllocation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			saveDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
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
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimePickerDueDate = new System.Windows.Forms.DateTimePicker();
			comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			buttonSelectConsignment = new Micromind.UISupport.XPButton();
			label9 = new System.Windows.Forms.Label();
			comboBoxTerm = new Micromind.DataControls.PaymentTermComboBox();
			buttonPrevBillto = new Infragistics.Win.Misc.UltraButton();
			buttonNextBillTo = new Infragistics.Win.Misc.UltraButton();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label2 = new System.Windows.Forms.Label();
			textBoxSettleNumber = new System.Windows.Forms.TextBox();
			textBoxBilltoAddress = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxConsignment = new System.Windows.Forms.TextBox();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
			labelVoided = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			textBoxTotal = new Micromind.UISupport.AmountTextBox();
			label7 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			textBoxExpenses = new Micromind.UISupport.AmountTextBox();
			textBoxSubtotal = new Micromind.UISupport.AmountTextBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			textBoxExpenseDiff = new Micromind.UISupport.AmountTextBox();
			formManager = new Micromind.DataControls.FormManager();
			labelExpDiff = new System.Windows.Forms.Label();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTerm).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			panel1.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(dataGridItems);
			tabPageGeneral.Controls.Add(productPhotoViewer);
			tabPageGeneral.Controls.Add(comboBoxGridItem);
			tabPageGeneral.Controls.Add(comboBoxGridLocation);
			tabPageGeneral.Controls.Add(comboBoxGridProductUnit);
			tabPageGeneral.Location = new System.Drawing.Point(1, 20);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(776, 233);
			dataGridItems.AllowAddNew = false;
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
			dataGridItems.Location = new System.Drawing.Point(4, 13);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(769, 217);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(69, 66);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 120;
			productPhotoViewer.Visible = false;
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
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
			comboBoxGridItem.Location = new System.Drawing.Point(656, 37);
			comboBoxGridItem.MaxDropDownItems = 12;
			comboBoxGridItem.Name = "comboBoxGridItem";
			comboBoxGridItem.Show3PLItems = true;
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(74, 20);
			comboBoxGridItem.TabIndex = 118;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
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
			comboBoxGridLocation.Location = new System.Drawing.Point(630, 37);
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
			comboBoxGridProductUnit.Assigned = false;
			comboBoxGridProductUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridProductUnit.CustomReportFieldName = "";
			comboBoxGridProductUnit.CustomReportKey = "";
			comboBoxGridProductUnit.CustomReportValueType = 1;
			comboBoxGridProductUnit.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridProductUnit.DisplayLayout.Appearance = appearance37;
			comboBoxGridProductUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridProductUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxGridProductUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridProductUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridProductUnit.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxGridProductUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridProductUnit.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridProductUnit.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxGridProductUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridProductUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxGridProductUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridProductUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridProductUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridProductUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridProductUnit.Editable = true;
			comboBoxGridProductUnit.FilterString = "";
			comboBoxGridProductUnit.IsDataLoaded = false;
			comboBoxGridProductUnit.Location = new System.Drawing.Point(648, 23);
			comboBoxGridProductUnit.MaxDropDownItems = 12;
			comboBoxGridProductUnit.Name = "comboBoxGridProductUnit";
			comboBoxGridProductUnit.Size = new System.Drawing.Size(101, 20);
			comboBoxGridProductUnit.TabIndex = 119;
			comboBoxGridProductUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridProductUnit.Visible = false;
			tabPageDetails.Controls.Add(textBoxTotalExpense);
			tabPageDetails.Controls.Add(label4);
			tabPageDetails.Controls.Add(dataGridExpense);
			tabPageDetails.Controls.Add(comboBoxGridExpenseCode);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(776, 233);
			textBoxTotalExpense.AllowDecimal = true;
			textBoxTotalExpense.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalExpense.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalExpense.CustomReportFieldName = "";
			textBoxTotalExpense.CustomReportKey = "";
			textBoxTotalExpense.CustomReportValueType = 1;
			textBoxTotalExpense.ForeColor = System.Drawing.Color.Black;
			textBoxTotalExpense.IsComboTextBox = false;
			textBoxTotalExpense.IsModified = false;
			textBoxTotalExpense.Location = new System.Drawing.Point(635, 206);
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
			textBoxTotalExpense.TabIndex = 135;
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
			label4.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			label4.Location = new System.Drawing.Point(3, 206);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(629, 18);
			label4.TabIndex = 136;
			label4.Text = "Total Expenses:";
			label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			dataGridExpense.AllowAddNew = true;
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
			dataGridExpense.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
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
			dataGridExpense.Location = new System.Drawing.Point(3, 14);
			dataGridExpense.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridExpense.Name = "dataGridExpense";
			dataGridExpense.ShowClearMenu = true;
			dataGridExpense.ShowDeleteMenu = true;
			dataGridExpense.ShowInsertMenu = true;
			dataGridExpense.ShowMoveRowsMenu = true;
			dataGridExpense.Size = new System.Drawing.Size(770, 191);
			dataGridExpense.TabIndex = 118;
			dataGridExpense.Text = "dataEntryGrid1";
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
			comboBoxGridExpenseCode.ExpenseCodeType = Micromind.Common.Data.ExpenseCodeTypes.ConsignmentOut;
			comboBoxGridExpenseCode.FilterString = "";
			comboBoxGridExpenseCode.HasAllAccount = false;
			comboBoxGridExpenseCode.HasCustom = false;
			comboBoxGridExpenseCode.IsDataLoaded = false;
			comboBoxGridExpenseCode.Location = new System.Drawing.Point(568, 45);
			comboBoxGridExpenseCode.MaxDropDownItems = 12;
			comboBoxGridExpenseCode.Name = "comboBoxGridExpenseCode";
			comboBoxGridExpenseCode.ShowInactiveItems = false;
			comboBoxGridExpenseCode.ShowQuickAdd = true;
			comboBoxGridExpenseCode.Size = new System.Drawing.Size(124, 20);
			comboBoxGridExpenseCode.TabIndex = 120;
			comboBoxGridExpenseCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[21]
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
				toolStripSeparator8,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonMultiPreview,
				toolStripSeparator4,
				toolStripButtonDistribution,
				toolStripButtonPaymentAllocation,
				toolStripDropDownButton1,
				toolStripButtonInformation,
				toolStripSeparator5
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(803, 31);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonPaymentAllocation.Image = Micromind.ClientUI.Properties.Resources.allocate;
			toolStripButtonPaymentAllocation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPaymentAllocation.Name = "toolStripButtonPaymentAllocation";
			toolStripButtonPaymentAllocation.Size = new System.Drawing.Size(82, 28);
			toolStripButtonPaymentAllocation.Text = "Payment";
			toolStripButtonPaymentAllocation.Click += new System.EventHandler(toolStripButtonPaymentAllocation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				duplicateToolStripMenuItem,
				toolStripSeparator7,
				saveDraftToolStripMenuItem,
				loadDraftToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			duplicateToolStripMenuItem.Text = "Duplicate";
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(138, 6);
			saveDraftToolStripMenuItem.Name = "saveDraftToolStripMenuItem";
			saveDraftToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			saveDraftToolStripMenuItem.Text = "Save as Draft";
			saveDraftToolStripMenuItem.Click += new System.EventHandler(saveDraftToolStripMenuItem_Click);
			loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
			loadDraftToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			loadDraftToolStripMenuItem.Text = "Load Draft...";
			loadDraftToolStripMenuItem.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 556);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(803, 40);
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
			linePanelDown.Size = new System.Drawing.Size(803, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(693, 8);
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
			dateTimePickerDate.Location = new System.Drawing.Point(551, 1);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDate.TabIndex = 7;
			textBoxVoucherNumber.Location = new System.Drawing.Point(326, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(109, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(469, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(551, 23);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(153, 20);
			textBoxRef1.TabIndex = 8;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(49, 470);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(381, 78);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 470);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance73.FontData.BoldAsString = "True";
			appearance73.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance73;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(214, 4);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 0;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance74.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance74;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(dateTimePickerDueDate);
			panelDetails.Controls.Add(comboBoxSalesperson);
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(ultraFormattedLinkCurrency);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(buttonSelectConsignment);
			panelDetails.Controls.Add(label9);
			panelDetails.Controls.Add(comboBoxTerm);
			panelDetails.Controls.Add(buttonPrevBillto);
			panelDetails.Controls.Add(buttonNextBillTo);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxSettleNumber);
			panelDetails.Controls.Add(textBoxBilltoAddress);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxCustomer);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxConsignment);
			panelDetails.Controls.Add(textBoxCustomerName);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(comboBoxCurrency);
			panelDetails.Location = new System.Drawing.Point(0, 35);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(749, 163);
			panelDetails.TabIndex = 0;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(469, 135);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(65, 13);
			mmLabel2.TabIndex = 147;
			mmLabel2.Text = "Due Date:";
			dateTimePickerDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDueDate.Location = new System.Drawing.Point(551, 131);
			dateTimePickerDueDate.Name = "dateTimePickerDueDate";
			dateTimePickerDueDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDueDate.TabIndex = 14;
			comboBoxSalesperson.Assigned = false;
			comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesperson.CustomReportFieldName = "";
			comboBoxSalesperson.CustomReportKey = "";
			comboBoxSalesperson.CustomReportValueType = 1;
			comboBoxSalesperson.DescriptionTextBox = null;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesperson.DisplayLayout.Appearance = appearance75;
			comboBoxSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance76.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.GroupByBox.Appearance = appearance76;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance77;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance78.BackColor2 = System.Drawing.SystemColors.Control;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance78;
			comboBoxSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance79;
			appearance80.BackColor = System.Drawing.SystemColors.Highlight;
			appearance80.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance80;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance81;
			appearance82.BorderColor = System.Drawing.Color.Silver;
			appearance82.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesperson.DisplayLayout.Override.CellAppearance = appearance82;
			comboBoxSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance83.BackColor = System.Drawing.SystemColors.Control;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance83;
			appearance84.TextHAlignAsString = "Left";
			comboBoxSalesperson.DisplayLayout.Override.HeaderAppearance = appearance84;
			comboBoxSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesperson.DisplayLayout.Override.RowAppearance = appearance85;
			comboBoxSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance86;
			comboBoxSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesperson.Editable = true;
			comboBoxSalesperson.FilterString = "";
			comboBoxSalesperson.HasAllAccount = false;
			comboBoxSalesperson.HasCustom = false;
			comboBoxSalesperson.IsDataLoaded = false;
			comboBoxSalesperson.Location = new System.Drawing.Point(551, 109);
			comboBoxSalesperson.MaxDropDownItems = 12;
			comboBoxSalesperson.Name = "comboBoxSalesperson";
			comboBoxSalesperson.ShowInactiveItems = false;
			comboBoxSalesperson.ShowQuickAdd = true;
			comboBoxSalesperson.Size = new System.Drawing.Size(153, 20);
			comboBoxSalesperson.TabIndex = 13;
			comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance87.FontData.BoldAsString = "False";
			appearance87.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance87;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(469, 112);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(66, 15);
			ultraFormattedLinkLabel6.TabIndex = 146;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Salesperson:";
			appearance88.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance88;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			appearance89.FontData.BoldAsString = "True";
			appearance89.FontData.Name = "Tahoma";
			ultraFormattedLinkCurrency.Appearance = appearance89;
			ultraFormattedLinkCurrency.AutoSize = true;
			ultraFormattedLinkCurrency.Location = new System.Drawing.Point(469, 87);
			ultraFormattedLinkCurrency.Name = "ultraFormattedLinkCurrency";
			ultraFormattedLinkCurrency.Size = new System.Drawing.Size(58, 15);
			ultraFormattedLinkCurrency.TabIndex = 144;
			ultraFormattedLinkCurrency.TabStop = true;
			ultraFormattedLinkCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkCurrency.Value = "Currency:";
			appearance90.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkCurrency.VisitedLinkAppearance = appearance90;
			ultraFormattedLinkCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkCurrency_LinkClicked);
			appearance91.FontData.BoldAsString = "True";
			appearance91.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance91;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(10, 48);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(81, 15);
			ultraFormattedLinkLabel3.TabIndex = 143;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Consignment:";
			appearance92.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance92;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			buttonSelectConsignment.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectConsignment.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectConsignment.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectConsignment.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectConsignment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectConsignment.Location = new System.Drawing.Point(315, 44);
			buttonSelectConsignment.Name = "buttonSelectConsignment";
			buttonSelectConsignment.Size = new System.Drawing.Size(25, 24);
			buttonSelectConsignment.TabIndex = 141;
			buttonSelectConsignment.Text = "...";
			buttonSelectConsignment.UseVisualStyleBackColor = false;
			buttonSelectConsignment.Click += new System.EventHandler(buttonSelectConsignment_Click);
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(469, 66);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(34, 13);
			label9.TabIndex = 140;
			label9.Text = "Term:";
			comboBoxTerm.Assigned = false;
			comboBoxTerm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTerm.CustomReportFieldName = "";
			comboBoxTerm.CustomReportKey = "";
			comboBoxTerm.CustomReportValueType = 1;
			comboBoxTerm.DescriptionTextBox = null;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTerm.DisplayLayout.Appearance = appearance93;
			comboBoxTerm.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTerm.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.GroupByBox.Appearance = appearance94;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTerm.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
			comboBoxTerm.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance96.BackColor2 = System.Drawing.SystemColors.Control;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTerm.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
			comboBoxTerm.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTerm.DisplayLayout.MaxRowScrollRegions = 1;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTerm.DisplayLayout.Override.ActiveCellAppearance = appearance97;
			appearance98.BackColor = System.Drawing.SystemColors.Highlight;
			appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTerm.DisplayLayout.Override.ActiveRowAppearance = appearance98;
			comboBoxTerm.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTerm.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.Override.CardAreaAppearance = appearance99;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTerm.DisplayLayout.Override.CellAppearance = appearance100;
			comboBoxTerm.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTerm.DisplayLayout.Override.CellPadding = 0;
			appearance101.BackColor = System.Drawing.SystemColors.Control;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.Override.GroupByRowAppearance = appearance101;
			appearance102.TextHAlignAsString = "Left";
			comboBoxTerm.DisplayLayout.Override.HeaderAppearance = appearance102;
			comboBoxTerm.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTerm.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.Color.Silver;
			comboBoxTerm.DisplayLayout.Override.RowAppearance = appearance103;
			comboBoxTerm.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTerm.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
			comboBoxTerm.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTerm.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTerm.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTerm.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTerm.Editable = true;
			comboBoxTerm.FilterString = "";
			comboBoxTerm.HasAllAccount = false;
			comboBoxTerm.HasCustom = false;
			comboBoxTerm.IsDataLoaded = false;
			comboBoxTerm.Location = new System.Drawing.Point(551, 65);
			comboBoxTerm.MaxDropDownItems = 12;
			comboBoxTerm.Name = "comboBoxTerm";
			comboBoxTerm.ShowInactiveItems = false;
			comboBoxTerm.ShowQuickAdd = true;
			comboBoxTerm.Size = new System.Drawing.Size(153, 20);
			comboBoxTerm.TabIndex = 11;
			comboBoxTerm.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance105.ImageBackground = Micromind.ClientUI.Properties.Resources.prev;
			appearance105.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
			buttonPrevBillto.Appearance = appearance105;
			buttonPrevBillto.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton;
			buttonPrevBillto.Location = new System.Drawing.Point(285, 68);
			buttonPrevBillto.Name = "buttonPrevBillto";
			buttonPrevBillto.ShowFocusRect = false;
			buttonPrevBillto.ShowOutline = false;
			buttonPrevBillto.Size = new System.Drawing.Size(14, 14);
			buttonPrevBillto.TabIndex = 135;
			buttonPrevBillto.TabStop = false;
			buttonPrevBillto.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			buttonPrevBillto.Click += new System.EventHandler(buttonPrevBillto_Click);
			appearance106.ImageBackground = Micromind.ClientUI.Properties.Resources.next;
			appearance106.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
			buttonNextBillTo.Appearance = appearance106;
			buttonNextBillTo.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2003ToolbarButton;
			buttonNextBillTo.Location = new System.Drawing.Point(299, 68);
			buttonNextBillTo.Name = "buttonNextBillTo";
			buttonNextBillTo.ShowFocusRect = false;
			buttonNextBillTo.ShowOutline = false;
			buttonNextBillTo.Size = new System.Drawing.Size(14, 14);
			buttonNextBillTo.TabIndex = 135;
			buttonNextBillTo.TabStop = false;
			buttonNextBillTo.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			buttonNextBillTo.Click += new System.EventHandler(buttonNextBillTo_Click);
			appearance107.FontData.BoldAsString = "False";
			appearance107.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance107;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 66);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabel1.TabIndex = 134;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Bill to Address:";
			appearance108.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance108;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(469, 47);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(70, 13);
			label2.TabIndex = 132;
			label2.Text = "Settlement #:";
			textBoxSettleNumber.Location = new System.Drawing.Point(551, 44);
			textBoxSettleNumber.MaxLength = 15;
			textBoxSettleNumber.Name = "textBoxSettleNumber";
			textBoxSettleNumber.Size = new System.Drawing.Size(153, 20);
			textBoxSettleNumber.TabIndex = 9;
			textBoxBilltoAddress.BackColor = System.Drawing.Color.White;
			textBoxBilltoAddress.CustomReportFieldName = "";
			textBoxBilltoAddress.CustomReportKey = "";
			textBoxBilltoAddress.CustomReportValueType = 1;
			textBoxBilltoAddress.IsComboTextBox = false;
			textBoxBilltoAddress.IsModified = false;
			textBoxBilltoAddress.Location = new System.Drawing.Point(99, 67);
			textBoxBilltoAddress.MaxLength = 255;
			textBoxBilltoAddress.Multiline = true;
			textBoxBilltoAddress.Name = "textBoxBilltoAddress";
			textBoxBilltoAddress.Size = new System.Drawing.Size(215, 72);
			textBoxBilltoAddress.TabIndex = 4;
			appearance109.FontData.BoldAsString = "True";
			appearance109.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance109;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(12, 24);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(79, 15);
			ultraFormattedLinkLabel4.TabIndex = 129;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Customer ID:";
			appearance110.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance110;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = null;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance111;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance112.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance112.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance112.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance112;
			appearance113.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance113;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance114.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance114.BackColor2 = System.Drawing.SystemColors.Control;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance114.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance114;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance115;
			appearance116.BackColor = System.Drawing.SystemColors.Highlight;
			appearance116.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance116;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance117;
			appearance118.BorderColor = System.Drawing.Color.Silver;
			appearance118.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance118;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance119.BackColor = System.Drawing.SystemColors.Control;
			appearance119.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance119.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance119.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance119.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance119;
			appearance120.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance120;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance121;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance122.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance122;
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
			comboBoxCustomer.TabIndex = 2;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance123.FontData.BoldAsString = "True";
			appearance123.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance123;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 2;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance124.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance124;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance125;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance126.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance126.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance126.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance126;
			appearance127.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance127;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance128.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance128.BackColor2 = System.Drawing.SystemColors.Control;
			appearance128.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance128.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance128;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance129;
			appearance130.BackColor = System.Drawing.SystemColors.Highlight;
			appearance130.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance130;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance131;
			appearance132.BorderColor = System.Drawing.Color.Silver;
			appearance132.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance132;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance133.BackColor = System.Drawing.SystemColors.Control;
			appearance133.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance133.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance133.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance133.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance133;
			appearance134.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance134;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			appearance135.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance135;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance136.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance136;
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
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(469, 3);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			textBoxConsignment.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxConsignment.Location = new System.Drawing.Point(99, 45);
			textBoxConsignment.MaxLength = 64;
			textBoxConsignment.Name = "textBoxConsignment";
			textBoxConsignment.ReadOnly = true;
			textBoxConsignment.Size = new System.Drawing.Size(215, 20);
			textBoxConsignment.TabIndex = 3;
			textBoxConsignment.TabStop = false;
			textBoxConsignment.TextChanged += new System.EventHandler(textBoxConsignment_TextChanged);
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.Location = new System.Drawing.Point(210, 23);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(225, 20);
			textBoxCustomerName.TabIndex = 3;
			textBoxCustomerName.TabStop = false;
			comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCurrency.Location = new System.Drawing.Point(551, 87);
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
			comboBoxCurrency.Size = new System.Drawing.Size(153, 20);
			comboBoxCurrency.TabIndex = 12;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(11, 395);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(775, 59);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(textBoxTotal);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(textBoxExpenses);
			panel1.Controls.Add(textBoxSubtotal);
			panel1.Location = new System.Drawing.Point(559, 458);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(231, 90);
			panel1.TabIndex = 3;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(3, 47);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(40, 13);
			label8.TabIndex = 133;
			label8.Text = "Total:";
			textBoxTotal.AllowDecimal = true;
			textBoxTotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotal.CustomReportFieldName = "";
			textBoxTotal.CustomReportKey = "";
			textBoxTotal.CustomReportValueType = 1;
			textBoxTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			textBoxTotal.IsComboTextBox = false;
			textBoxTotal.IsModified = false;
			textBoxTotal.Location = new System.Drawing.Point(87, 44);
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
			textBoxTotal.ReadOnly = true;
			textBoxTotal.Size = new System.Drawing.Size(138, 20);
			textBoxTotal.TabIndex = 5;
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
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(4, 25);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(56, 13);
			label7.TabIndex = 133;
			label7.Text = "Expenses:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(4, 4);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(49, 13);
			label5.TabIndex = 133;
			label5.Text = "Subtotal:";
			textBoxExpenses.AllowDecimal = true;
			textBoxExpenses.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxExpenses.CustomReportFieldName = "";
			textBoxExpenses.CustomReportKey = "";
			textBoxExpenses.CustomReportValueType = 1;
			textBoxExpenses.IsComboTextBox = false;
			textBoxExpenses.IsModified = false;
			textBoxExpenses.Location = new System.Drawing.Point(88, 22);
			textBoxExpenses.MaxLength = 15;
			textBoxExpenses.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxExpenses.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxExpenses.Name = "textBoxExpenses";
			textBoxExpenses.NullText = "0";
			textBoxExpenses.ReadOnly = true;
			textBoxExpenses.Size = new System.Drawing.Size(138, 20);
			textBoxExpenses.TabIndex = 4;
			textBoxExpenses.Text = "0.00";
			textBoxExpenses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxExpenses.Value = new decimal(new int[4]
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
			textBoxSubtotal.Location = new System.Drawing.Point(88, 0);
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
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Location = new System.Drawing.Point(11, 204);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(778, 254);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControl1.TabIndex = 123;
			appearance137.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance137;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&Items";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Expenses";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(776, 233);
			textBoxExpenseDiff.AllowDecimal = true;
			textBoxExpenseDiff.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxExpenseDiff.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxExpenseDiff.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxExpenseDiff.CustomReportFieldName = "";
			textBoxExpenseDiff.CustomReportKey = "";
			textBoxExpenseDiff.CustomReportValueType = 1;
			textBoxExpenseDiff.IsComboTextBox = false;
			textBoxExpenseDiff.IsModified = false;
			textBoxExpenseDiff.Location = new System.Drawing.Point(464, 462);
			textBoxExpenseDiff.MaxLength = 15;
			textBoxExpenseDiff.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxExpenseDiff.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxExpenseDiff.Name = "textBoxExpenseDiff";
			textBoxExpenseDiff.NullText = "0";
			textBoxExpenseDiff.ReadOnly = true;
			textBoxExpenseDiff.Size = new System.Drawing.Size(71, 13);
			textBoxExpenseDiff.TabIndex = 124;
			textBoxExpenseDiff.Text = "0.00";
			textBoxExpenseDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxExpenseDiff.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxExpenseDiff.Visible = false;
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
			labelExpDiff.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			labelExpDiff.AutoSize = true;
			labelExpDiff.Location = new System.Drawing.Point(437, 462);
			labelExpDiff.Name = "labelExpDiff";
			labelExpDiff.Size = new System.Drawing.Size(70, 13);
			labelExpDiff.TabIndex = 134;
			labelExpDiff.Text = "Expense Diff:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(803, 596);
			base.Controls.Add(labelExpDiff);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxExpenseDiff);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "ConsignOutSettlementForm";
			Text = "Consignment Out Settlement";
			base.Load += new System.EventHandler(ConsignOutSettlementForm_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTerm).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
