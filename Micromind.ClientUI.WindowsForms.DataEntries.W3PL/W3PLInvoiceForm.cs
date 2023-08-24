using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
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
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.W3PL
{
	public class W3PLInvoiceForm : Form, IForm
	{
		private bool trackDetailedSales;

		private DataTable deliveryNoteTable;

		private bool allowEdit = true;

		private DataTable grnTable;

		private W3PLInvoiceData currentData;

		private const string TABLENAME_CONST = "W3PL_Invoice";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentVendorAddressID = "";

		private string consignSysDocID = "";

		private string consignVoucherID = "";

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

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

		private ProductUnitComboBox comboBoxGridProductUnit;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TextBox textBoxVendorName;

		private Label labelCurrency;

		private NumberTextBox textBoxSubtotal;

		private Panel panel1;

		private Label label5;

		private NumberTextBox textBoxExpenses;

		private Label label8;

		private Label label7;

		private NumberTextBox textBoxTotal;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

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

		private XPButton buttonSelectConsignment;

		private NumberTextBox textBoxTotalExpense;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private customersFlatComboBox comboBoxCustomer;

		private ListBox checkedListBoxGRN;

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
				bool enabled;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
					customersFlatComboBox customersFlatComboBox = comboBoxCustomer;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					bool flag3 = textBoxVoucherNumber.Enabled = true;
					enabled = (sysDocComboBox.Enabled = flag3);
					customersFlatComboBox.Enabled = enabled;
					buttonSelectConsignment.Enabled = true;
					dateTimePickerDate.Enabled = true;
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
					enabled = (sysDocComboBox2.Enabled = flag3);
					customersFlatComboBox2.Enabled = enabled;
					buttonSelectConsignment.Enabled = false;
					dateTimePickerDate.Enabled = false;
				}
				toolStripButtonDistribution.Enabled = !value;
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
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

		public W3PLInvoiceForm()
		{
			InitializeComponent();
			AddEvents();
			trackDetailedSales = CompanyPreferences.TrackConsignInDetailedSales;
			comboBoxCustomer.ShowConsignmentOnly = true;
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
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			comboBoxCurrency.SelectedIndexChanged += comboBoxCurrency_SelectedIndexChanged;
			dataGridExpense.AfterCellUpdate += dataGridExpense_AfterCellUpdate;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			textBoxExpenses.Validating += textBoxDiscountAmount_Validating;
			textBoxExpenses.Validated += textBoxDiscountAmount_Validated;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
			LoadItemsForInvoicing();
		}

		private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
		{
			HideShowFCColumns();
			CalculateTotal();
		}

		private void HideShowFCColumns()
		{
			if (comboBoxCurrency.IsBaseCurrency)
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["AmountFC"].Hidden = true;
				if (trackDetailedSales)
				{
					dataGridItems.DisplayLayout.Bands[1].Columns["AmountFC"].Hidden = true;
				}
			}
			else
			{
				dataGridExpense.DisplayLayout.Bands[0].Columns["AmountFC"].Header.Caption = "Amount (" + comboBoxCurrency.SelectedID + ")";
			}
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "BalanceQuantity")
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
				if (result2 < 0m)
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
				if (activeRow != null && activeRow.Cells["ProductID"].Value != null && !(activeRow.Cells["ProductID"].Value.ToString() == "") && activeRow != null && activeRow.DataChanged && !IsNewRecord)
				{
					_ = comboBoxSysDoc.SelectedID;
					_ = textBoxVoucherNumber.Text;
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
			if (dataGridItems.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "ProductID")
			{
				contextMenuStrip1.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isDataLoading && comboBoxCustomer.SelectedID != "")
			{
				string text = Factory.DatabaseSystem.GetFieldValue("Customer", "CurrencyID", "CustomerID", comboBoxCustomer.SelectedID).ToString();
				if (text != "")
				{
					comboBoxCurrency.SelectedID = text;
					comboBoxCurrency.GetLastRate();
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
				if (e.Cell.Column.Key == "ProductID")
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
					dataGridItems.ActiveRow.Cells["BalanceQuantity"].Value = 0;
					dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
					dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
					if (comboBoxGridItem.SelectedItemType == ItemTypes.Discount)
					{
						dataGridItems.ActiveRow.Cells["BalanceQuantity"].Value = -1;
						dataGridItems.ActiveRow.Cells["BalanceQuantity"].Activation = Activation.Disabled;
					}
					else
					{
						dataGridItems.ActiveRow.Cells["BalanceQuantity"].Activation = Activation.AllowEdit;
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
			decimal.TryParse(dataGridItems.ActiveRow.Cells["BalanceQuantity"].Value.ToString(), out result);
			decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result2);
			decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
			if (key == "BalanceQuantity" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "BalanceQuantity")
			{
				result3 = Math.Round(result * result2, Global.CurDecimalPoints);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
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
					dataGridItems.ActiveRow.Cells["BalanceQuantity"].Value = result;
				}
				result2 = Math.Round(result3 / result, 4);
				if (result3 < 0m)
				{
					result = -1m * Math.Abs(result);
					dataGridItems.ActiveRow.Cells["BalanceQuantity"].Value = result;
				}
				dataGridItems.ActiveRow.Cells["Price"].Value = Math.Abs(result2);
			}
			else if (key == "ProductID")
			{
				if (dataGridItems.ActiveRow.Cells["BalanceQuantity"].Value == null || dataGridItems.ActiveRow.Cells["BalanceQuantity"].Value.ToString() == "")
				{
					dataGridItems.ActiveRow.Cells["BalanceQuantity"].Value = result;
				}
				result3 = Math.Round(result * result2, Global.CurDecimalPoints);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
			}
			if (key == "Amount" || key == "BalanceQuantity")
			{
				CalculateTotal();
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
			if (dataGridItems.ActiveRow != null)
			{
				UltraGridRow activeRow = dataGridItems.ActiveRow;
				if (activeRow != null && activeRow.DataChanged && activeRow.Band.Index == 1 && dataGridItems.ActiveCell.Column.Key == "BalanceQuantity" && !IsNewRecord)
				{
					_ = comboBoxSysDoc.SelectedID;
					_ = textBoxVoucherNumber.Text;
				}
				if (dataGridItems.ActiveCell.Column.Key.ToString() == "Price")
				{
					decimal result = default(decimal);
					decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result);
					result = Math.Round(result, 5);
					dataGridItems.ActiveCell.Value = result;
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "BalanceQuantity")
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
			_ = dataGridItems.ActiveRow;
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Band.Index == 1 && e.Cell.Column.Key == "BalanceQuantity")
			{
				decimal result = default(decimal);
				if (e.NewValue.ToString() == "")
				{
					e.Cell.Value = 0;
				}
				if (!decimal.TryParse(e.NewValue.ToString(), out result) || result < 0m)
				{
					ErrorHelper.InformationMessage("Invalid quantity. Please enter a non negative numeric value.");
					e.Cancel = true;
				}
			}
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "ProductID")
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
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "BalanceQuantity")
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
					currentData = new W3PLInvoiceData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.W3PLInvoiceTable.Rows[0] : currentData.W3PLInvoiceTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["IsCash"] = false;
				if (comboBoxTerm.SelectedID != "")
				{
					dataRow["TermID"] = comboBoxTerm.SelectedID;
				}
				else
				{
					dataRow["TermID"] = DBNull.Value;
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
				if (comboBoxCurrency.IsBaseCurrency)
				{
					dataRow["Total"] = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
				}
				else
				{
					dataRow["TotalFC"] = decimal.Parse(textBoxTotal.Text, NumberStyles.Any);
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.W3PLInvoiceTable.Rows.Add(dataRow);
				}
				currentData.W3PLInvoiceDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					foreach (UltraGridRow row2 in row.ChildBands[0].Rows)
					{
						DataRow dataRow2 = currentData.W3PLInvoiceDetailTable.NewRow();
						dataRow2.BeginEdit();
						if (row2.Cells["StoreQuantity"].Value != null && !(row2.Cells["StoreQuantity"].Value.ToString() == "") && !(decimal.Parse(row2.Cells["StoreQuantity"].Value.ToString()) == 0m))
						{
							dataRow2["ProductID"] = row.Cells["ProductID"].Value.ToString();
							dataRow2["Quantity"] = row2.Cells["StoreQuantity"].Value.ToString();
							dataRow2["DLQuantity"] = row2.Cells["DeliveredQuantity"].Value.ToString();
							dataRow2["UnitWeight"] = row2.Cells["UnitWeight"].Value.ToString();
							dataRow2["TotalWeight"] = row2.Cells["TotalWeight"].Value.ToString();
							dataRow2["SourceSysDocID"] = row.Cells["GRNSysDocID"].Value.ToString();
							dataRow2["SourceVoucherID"] = row.Cells["GRNVoucherID"].Value.ToString();
							dataRow2["SourceRowIndex"] = row.Cells["GRNRowIndex"].Value.ToString();
							dataRow2["StartDate"] = row2.Cells["StartDate"].Value.ToString();
							dataRow2["EndDate"] = row2.Cells["EndDate"].Value.ToString();
							dataRow2["UnitPrice"] = row2.Cells["Price"].Value.ToString();
							dataRow2["Amount"] = row2.Cells["Amount"].Value.ToString();
							dataRow2["Description"] = row.Cells["ItemDescription"].Value.ToString();
							dataRow2["RowIndex"] = row2.Index;
							dataRow2.EndEdit();
							currentData.W3PLInvoiceDetailTable.Rows.Add(dataRow2);
							row.Cells["ProductID"].Value.ToString();
							if (row2.Cells["StoreQuantity"].Tag != null)
							{
								foreach (DataRow row3 in (row2.Cells["StoreQuantity"].Tag as DataTable).Rows)
								{
									DataRow dataRow4 = currentData.Tables["Product_Lot_Issue_Detail"].NewRow();
									dataRow4["ProductID"] = row3["ProductID"];
									dataRow4["LocationID"] = row3["LocationID"];
									dataRow4["LotNumber"] = row3["LotNumber"];
									dataRow4["Reference"] = row3["Reference"];
									dataRow4["SourceLotNumber"] = row3["SourceLotNumber"];
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
				if (deliveryNoteTable != null)
				{
					currentData.Tables.Add(deliveryNoteTable);
				}
				currentData.ExpenseTable.Rows.Clear();
				foreach (UltraGridRow row4 in dataGridExpense.Rows)
				{
					DataRow dataRow5 = currentData.ExpenseTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["ExpenseID"] = row4.Cells["Expense"].Value.ToString();
					dataRow5["Description"] = row4.Cells["Description"].Value.ToString();
					dataRow5["Reference"] = row4.Cells["Ref"].Value.ToString();
					if (row4.Cells["SourceRowIndex"].Value != null && row4.Cells["SourceRowIndex"].Value.ToString() != "")
					{
						dataRow5["SourceRowIndex"] = row4.Cells["SourceRowIndex"].Value.ToString();
						dataRow5["SourceSysDocID"] = row4.Cells["SourceSysDocID"].Value.ToString();
						dataRow5["SourceVoucherID"] = row4.Cells["SourceVoucherID"].Value.ToString();
					}
					dataRow5["RowIndex"] = row4.Index;
					dataRow5["Amount"] = row4.Cells["Amount"].Value.ToString();
					dataRow5["AmountFC"] = row4.Cells["AmountFC"].Value.ToString();
					dataRow5["CurrencyID"] = comboBoxCurrency.SelectedID;
					dataRow5["CurrencyRate"] = comboBoxCurrency.Rate;
					dataRow5.EndEdit();
					currentData.ExpenseTable.Rows.Add(dataRow5);
				}
				_ = dataGridItems.DataSource;
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
				DataSet dataSet = new DataSet();
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("GRNSysDocID");
				dataTable.Columns.Add("GRNVoucherID");
				dataTable.Columns.Add("ProductID");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("ItemDescription");
				dataTable.Columns.Add("GRNRowIndex");
				dataSet.Tables.Add(dataTable);
				DataTable dataTable2 = new DataTable("GRN_Detail");
				dataTable2.Columns.Add("ProductID");
				dataTable2.Columns.Add("GRNSysDocID");
				dataTable2.Columns.Add("GRNVoucherID");
				dataTable2.Columns.Add("GRNRowIndex", typeof(int));
				dataTable2.Columns.Add("StartDate", typeof(DateTime));
				dataTable2.Columns.Add("EndDate", typeof(DateTime));
				dataTable2.Columns.Add("StoreQuantity", typeof(decimal));
				dataTable2.Columns.Add("DeliveredQuantity", typeof(decimal));
				dataTable2.Columns.Add("BalanceQuantity", typeof(decimal));
				dataTable2.Columns.Add("UnitWeight", typeof(decimal));
				dataTable2.Columns.Add("TotalWeight", typeof(decimal));
				dataTable2.Columns.Add("Price", typeof(decimal));
				dataTable2.Columns.Add("Amount", typeof(decimal));
				dataSet.Tables.Add(dataTable2);
				dataSet.Relations.Add("REL", new DataColumn[3]
				{
					dataTable.Columns["ProductID"],
					dataTable.Columns["GRNSysDocID"],
					dataTable.Columns["GRNVoucherID"]
				}, new DataColumn[3]
				{
					dataTable2.Columns["ProductID"],
					dataTable2.Columns["GRNSysDocID"],
					dataTable2.Columns["GRNVoucherID"]
				}, createConstraints: true);
				dataGridItems.DataSource = dataSet;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["GRNSysDocID"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["GRNVoucherID"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["ProductID"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["ItemDescription"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["GRNRowIndex"].Hidden = true;
				bool flag4 = ultraGridColumn4.Hidden = flag2;
				bool flag6 = ultraGridColumn3.Hidden = flag4;
				bool hidden = ultraGridColumn2.Hidden = flag6;
				ultraGridColumn.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Indentation = 0;
				dataGridItems.DisplayLayout.Bands[1].Indentation = 0;
				dataGridItems.DisplayLayout.InterBandSpacing = 0;
				dataGridItems.DisplayLayout.Bands[0].Override.ExpansionIndicator = ShowExpansionIndicator.Never;
				dataGridItems.DisplayLayout.Bands[0].ColHeadersVisible = false;
				dataGridItems.DisplayLayout.Bands[0].Override.CellAppearance.FontData.Bold = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[1].Override.HeaderPlacement = HeaderPlacement.FixedOnTop;
				dataGridItems.ShowDeleteMenu = false;
				dataGridItems.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
				dataGridItems.DisplayLayout.Bands[0].Columns["ProductID"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[1].Columns["ProductID"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[1].Columns["GRNSysDocID"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[1].Columns["GRNVoucherID"];
				flag4 = (dataGridItems.DisplayLayout.Bands[1].Columns["GRNRowIndex"].Hidden = true);
				flag6 = (ultraGridColumn7.Hidden = flag4);
				hidden = (ultraGridColumn6.Hidden = flag6);
				ultraGridColumn5.Hidden = hidden;
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[1].Columns["ProductID"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[1].Columns["GRNSysDocID"];
				ExcludeFromColumnChooser excludeFromColumnChooser2 = dataGridItems.DisplayLayout.Bands[1].Columns["GRNVoucherID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				ExcludeFromColumnChooser excludeFromColumnChooser5 = ultraGridColumn8.ExcludeFromColumnChooser = (ultraGridColumn9.ExcludeFromColumnChooser = excludeFromColumnChooser2);
				dataGridItems.DisplayLayout.Bands[1].Columns["StartDate"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["StartDate"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["StartDate"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["StartDate"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[1].Columns["EndDate"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["EndDate"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["EndDate"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["EndDate"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[1].Columns["UnitWeight"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["UnitWeight"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[1].Columns["UnitWeight"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["UnitWeight"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["UnitWeight"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["UnitWeight"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["UnitWeight"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["UnitWeight"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[1].Columns["TotalWeight"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["TotalWeight"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[1].Columns["TotalWeight"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["TotalWeight"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["TotalWeight"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["TotalWeight"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["TotalWeight"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["TotalWeight"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[1].Columns["TotalWeight"].Header.Caption = "Total Weight";
				dataGridItems.DisplayLayout.Bands[1].Columns["BalanceQuantity"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["BalanceQuantity"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[1].Columns["BalanceQuantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["BalanceQuantity"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["BalanceQuantity"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["BalanceQuantity"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["BalanceQuantity"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["BalanceQuantity"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[1].Columns["BalanceQuantity"].Header.Caption = "Balance Qty";
				dataGridItems.DisplayLayout.Bands[1].Columns["DeliveredQuantity"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["DeliveredQuantity"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[1].Columns["DeliveredQuantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["DeliveredQuantity"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["DeliveredQuantity"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["DeliveredQuantity"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["DeliveredQuantity"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["DeliveredQuantity"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[1].Columns["DeliveredQuantity"].Header.Caption = "Delivered Qty";
				dataGridItems.DisplayLayout.Bands[1].Columns["StoreQuantity"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["StoreQuantity"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[1].Columns["StoreQuantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["StoreQuantity"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["StoreQuantity"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["StoreQuantity"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["StoreQuantity"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["StoreQuantity"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[1].Columns["StoreQuantity"].Header.Caption = "Store Qty";
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["Price"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].TabStop = false;
				SetupExpenseGrid();
				AdjustGridColumnSettings();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupExpenseGrid()
		{
			dataGridExpense.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Expense");
			dataTable.Columns.Add("SourceRowIndex", typeof(int));
			dataTable.Columns.Add("SourceSysDocID");
			dataTable.Columns.Add("SourceVoucherID");
			dataTable.Columns.Add("Description");
			dataTable.Columns.Add("Ref");
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataTable.Columns.Add("AmountFC", typeof(decimal));
			dataGridExpense.DataSource = dataTable;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].CharacterCasing = CharacterCasing.Upper;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].MaxLength = 64;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].ValueList = comboBoxGridExpenseCode;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].MaxLength = 20;
			dataGridExpense.DisplayLayout.Bands[0].Columns["SourceSysDocID"].Header.Caption = "Doc ID";
			dataGridExpense.DisplayLayout.Bands[0].Columns["SourceVoucherID"].Header.Caption = "Doc No";
			UltraGridColumn ultraGridColumn = dataGridExpense.DisplayLayout.Bands[0].Columns["SourceVoucherID"];
			UltraGridColumn ultraGridColumn2 = dataGridExpense.DisplayLayout.Bands[0].Columns["SourceRowIndex"];
			bool flag2 = dataGridExpense.DisplayLayout.Bands[0].Columns["SourceSysDocID"].Hidden = true;
			bool hidden = ultraGridColumn2.Hidden = flag2;
			ultraGridColumn.Hidden = hidden;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
			dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountFC"].MinValue = 0;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountFC"].MaxValue = new decimal(999999999999L);
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountFC"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountFC"].Format = Format.GridAmountFormat;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountFC"].CellActivation = Activation.NoEdit;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountFC"].CellAppearance.BackColor = Color.WhiteSmoke;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountFC"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountFC"].TabStop = false;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountFC"].Header.Caption = "Amount(" + comboBoxCurrency.SelectedID + ")";
			Activation activation3 = dataGridExpense.DisplayLayout.Bands[0].Columns["SourceVoucherID"].CellActivation = (dataGridExpense.DisplayLayout.Bands[0].Columns["SourceSysDocID"].CellActivation = Activation.NoEdit);
			Color color2 = dataGridExpense.DisplayLayout.Bands[0].Columns["SourceVoucherID"].CellAppearance.BackColor = (dataGridExpense.DisplayLayout.Bands[0].Columns["SourceSysDocID"].CellAppearance.BackColor = Color.WhiteSmoke);
			color2 = (dataGridExpense.DisplayLayout.Bands[0].Columns["SourceVoucherID"].CellAppearance.BackColorDisabled = (dataGridExpense.DisplayLayout.Bands[0].Columns["SourceSysDocID"].CellAppearance.BackColorDisabled = Color.WhiteSmoke));
			if (dataGridExpense.DisplayLayout.Bands[0].Summaries.Count == 0)
			{
				dataGridExpense.DisplayLayout.Bands[0].Summaries.Add("amount", SummaryType.Sum, dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridExpense.DisplayLayout.Bands[0].Summaries["amount"].Appearance.BackColor = Color.White;
				dataGridExpense.DisplayLayout.Bands[0].Summaries["amount"].Appearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Summaries["amount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridExpense.DisplayLayout.Bands[0].Summaries["amount"].DisplayFormat = "{0:n}";
				dataGridExpense.DisplayLayout.Bands[0].Summaries.Add("amountFC", SummaryType.Sum, dataGridExpense.DisplayLayout.Bands[0].Columns["AmountFC"], SummaryPosition.UseSummaryPositionColumn);
				dataGridExpense.DisplayLayout.Bands[0].Summaries["amountFC"].Appearance.BackColor = Color.White;
				dataGridExpense.DisplayLayout.Bands[0].Summaries["amountFC"].Appearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Summaries["amountFC"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridExpense.DisplayLayout.Bands[0].Summaries["amountFC"].DisplayFormat = "{0:n}";
			}
			dataGridExpense.DisplayLayout.Bands[0].Columns["Expense"].Width = checked(7 * dataGridExpense.Width) / 100;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].Width = checked(5 * dataGridExpense.Width) / 100;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(7 * dataGridExpense.Width) / 100;
		}

		private void AdjustGridColumnSettings()
		{
			Color backColor = Color.FromArgb(240, 240, 240);
			dataGridItems.DisplayLayout.Bands[0].Columns["ProductID"].CellAppearance.BackColor = backColor;
			dataGridItems.DisplayLayout.Bands[0].Columns["ProductID"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColor = backColor;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].Format = Format.GridAmountFormat;
			dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].MinValue = new decimal(-999999999999L);
			dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridItems.DisplayLayout.Bands[0].Columns["ProductID"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["ProductID"].Header.Appearance.Cursor = Cursors.Hand;
			_ = dataGridItems.DisplayLayout.Bands[0].Summaries.Count;
			if (dataGridItems.DisplayLayout.Bands[1].Summaries.Count == 0)
			{
				dataGridItems.DisplayLayout.Bands[1].Summaries.Add("Amount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[1].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[1].Summaries["Amount"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[1].Summaries["Amount"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Summaries["Amount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[1].Summaries["Amount"].DisplayFormat = "{0:n}";
			}
		}

		private void CalculateExpensePerItem()
		{
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal.TryParse(textBoxTotalExpense.Text, out result);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!(row.Cells["ProductID"].Value.ToString() == ""))
				{
					decimal result2 = default(decimal);
					decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result2);
					num += result2;
				}
			}
			decimal d = default(decimal);
			if (num > 0m)
			{
				d = result / num;
			}
			decimal d2 = default(decimal);
			int num2 = 0;
			checked
			{
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					_ = row2;
					num2++;
				}
				int num3 = 1;
				foreach (UltraGridRow row3 in dataGridItems.Rows)
				{
					decimal result3 = default(decimal);
					decimal.TryParse(row3.Cells["Amount"].Value.ToString(), out result3);
					decimal result4 = default(decimal);
					decimal.TryParse(row3.Cells["StoreQuantity"].Value.ToString(), out result4);
					decimal num4 = default(decimal);
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
					num3++;
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
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.W3PLInvoiceSystem.GetInvoiceByID(SystemDocID, voucherID);
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
					DataRow dataRow = currentData.Tables["W3PL_Invoice"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					consignSysDocID = dataRow["ConsignSysDocID"].ToString();
					consignVoucherID = dataRow["ConsignVoucherID"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					if (dataRow["CurrencyRate"] != DBNull.Value)
					{
						comboBoxCurrency.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
					}
					else
					{
						comboBoxCurrency.Rate = 1m;
					}
					if (dataRow["CurrencyID"] != DBNull.Value)
					{
						_ = (dataRow["CurrencyID"].ToString() != Global.BaseCurrencyID);
					}
					comboBoxTerm.SelectedID = dataRow["TermID"].ToString();
					if (dataRow["TotalFC"] != DBNull.Value)
					{
						textBoxTotal.Text = decimal.Parse(dataRow["TotalFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTotal.Text = decimal.Parse(dataRow["Total"].ToString()).ToString(Format.TotalAmountFormat);
					}
					List<string> list = new List<string>();
					foreach (DataRow row in currentData.W3PLInvoiceDetailTable.Rows)
					{
						string item = row["SourceSysDocID"].ToString() + row["SourceVoucherID"].ToString() + row["ProductID"].ToString();
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
					DataSet obj = dataGridItems.DataSource as DataSet;
					DataTable dataTable = obj.Tables[0];
					DataTable dataTable2 = obj.Tables[1];
					dataTable2.Rows.Clear();
					dataTable.Rows.Clear();
					foreach (string item2 in list)
					{
						DataRow[] array = currentData.W3PLInvoiceDetailTable.Select("SourceSysDocID + SourceVoucherID + ProductID = '" + item2 + "'");
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["GRNSysDocID"] = array[0]["SourceSysDocID"];
						dataRow3["GRNVoucherID"] = array[0]["SourceVoucherID"];
						dataRow3["GRNRowIndex"] = array[0]["SourceRowIndex"];
						dataRow3["ProductID"] = array[0]["ProductID"];
						dataRow3["ItemDescription"] = array[0]["Description"];
						dataRow3["Description"] = "GRN No: " + array[0]["SourceSysDocID"].ToString() + "-" + array[0]["SourceVoucherID"].ToString() + "   -  Item: " + array[0]["Description"].ToString();
						dataTable.Rows.Add(dataRow3);
						for (int i = 0; i < array.Length; i = checked(i + 1))
						{
							DataRow dataRow4 = dataTable2.NewRow();
							dataRow4["ProductID"] = array[i]["ProductID"];
							dataRow4["StartDate"] = array[i]["StartDate"];
							dataRow4["EndDate"] = array[i]["EndDate"];
							dataRow4["GRNSysDocID"] = array[i]["SourceSysDocID"];
							dataRow4["GRNVoucherID"] = array[i]["SourceVoucherID"];
							dataRow4["GRNRowIndex"] = array[i]["SourceRowIndex"];
							dataRow4["StoreQuantity"] = array[i]["Quantity"];
							dataRow4["DeliveredQuantity"] = array[i]["DLQuantity"];
							decimal d = decimal.Parse(array[i]["Quantity"].ToString());
							decimal d2 = decimal.Parse(array[i]["DLQuantity"].ToString());
							dataRow4["BalanceQuantity"] = d - d2;
							dataRow4["UnitWeight"] = array[i]["UnitWeight"];
							dataRow4["TotalWeight"] = array[i]["TotalWeight"];
							dataRow4["Price"] = array[i]["UnitPrice"];
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal num = default(decimal);
							decimal.TryParse(dataRow4["TotalWeight"].ToString(), out result);
							decimal.TryParse(dataRow4["Price"].ToString(), out result2);
							num = Math.Round(Math.Round(result2 * result), Global.CurDecimalPoints);
							dataRow4["Price"] = array[i]["UnitPrice"];
							dataRow4["Amount"] = num;
							dataTable2.Rows.Add(dataRow4);
						}
					}
					dataTable.AcceptChanges();
					dataTable = (dataGridExpense.DataSource as DataTable);
					dataTable.Clear();
					foreach (DataRow row2 in currentData.Tables["W3PL_Expense"].Rows)
					{
						DataRow dataRow6 = dataTable.NewRow();
						dataRow6["Expense"] = row2["ExpenseID"];
						dataRow6["Description"] = row2["Description"];
						dataRow6["Ref"] = row2["Reference"];
						dataRow6["SourceRowIndex"] = row2["SourceRowIndex"];
						dataRow6["SourceSysDocID"] = row2["SourceSysDocID"];
						dataRow6["SourceVoucherID"] = row2["SourceVoucherID"];
						if (row2["CurrencyID"].ToString() != "")
						{
							_ = (row2["CurrencyID"].ToString() != Global.BaseCurrencyID);
						}
						dataRow6["Amount"] = decimal.Parse(row2["Amount"].ToString()).ToString(Format.TotalAmountFormat);
						dataRow6.EndEdit();
						dataTable.Rows.Add(dataRow6);
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
					dataGridItems.Rows.ExpandAll(recursive: true);
					CalculateTotal();
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
				bool flag2 = Factory.W3PLInvoiceSystem.CreateInvoice(currentData, !isNewRecord);
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
				if (ex.Number == 1037)
				{
					ErrorHelper.WarningMessage("Unable to modify or delete the transaction. Some of the items in this transaction are in use.");
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("W3PL_Invoice", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to (SecurityRoleID:116).");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
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
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions not more than", num.ToString(), " days.");
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
				if ((!decimal.TryParse(textBoxTotal.Text, out result) || result < 0m) && ErrorHelper.QuestionMessageYesNo("Total settlement amount is negative. Are you sure you want to save the settlement with negative total?") == DialogResult.No)
				{
					return false;
				}
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
					}
					else if (dataGridItems.Rows[i].Cells["ProductID"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select an item.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one row of item.");
					return false;
				}
				if ((dataGridItems.DataSource as DataSet).Tables["GRN_Detail"].Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one invoice row.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("W3PL_Invoice", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
					textBoxVoucherNumber.Focus();
					return false;
				}
				if (CompanyPreferences.OverCLAction != 1 && !IsNewRecord)
				{
					_ = comboBoxSysDoc.SelectedID;
					_ = textBoxVoucherNumber.Text;
				}
				return true;
			}
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["StoreQuantity"].Value != null && row.Cells["StoreQuantity"].Value.ToString() != "")
				{
					result += decimal.Parse(row.Cells["StoreQuantity"].Value.ToString());
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
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				checkedListBoxGRN.Items.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxCustomer.Clear();
				comboBoxTerm.Clear();
				textBoxSubtotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				string text3 = textBoxExpenses.Text = (textBoxTotalExpense.Text = 0.ToString(Format.TotalAmountFormat));
				comboBoxCustomer.Enabled = true;
				consignSysDocID = "";
				consignVoucherID = "";
				(dataGridItems.DataSource as DataSet).Tables[1].Rows.Clear();
				(dataGridExpense.DataSource as DataTable).Rows.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				deliveryNoteTable = null;
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
				return Factory.W3PLInvoiceSystem.DeleteInvoice(SystemDocID, textBoxVoucherNumber.Text);
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1037)
				{
					ErrorHelper.WarningMessage("Unable to modify or delete the transaction. Some of the items in this transaction are in use.");
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
				return false;
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
			string nextID = DatabaseHelper.GetNextID("W3PL_Invoice", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("W3PL_Invoice", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("W3PL_Invoice", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("W3PL_Invoice", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("W3PL_Invoice", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				Label label = labelCurrency;
				bool visible = comboBoxCurrency.Visible = CompanyPreferences.UseMultiCurrency;
				label.Visible = visible;
				comboBoxSysDoc.FilterByType(SysDocTypes.W3PLInvoice);
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
				return Factory.W3PLInvoiceSystem.VoidInvoice(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1037)
				{
					ErrorHelper.WarningMessage("Unable to modify or delete the transaction. Some of the items in this transaction are in use.");
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.W3PLInvoice);
		}

		private void CalculateTotal()
		{
			decimal d = default(decimal);
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal d2 = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				num2 = default(decimal);
				num3 = default(decimal);
				if (row.HasChild())
				{
					foreach (UltraGridRow row2 in row.ChildBands[0].Rows)
					{
						if (row2.Cells["Amount"].Value != null && !(row2.Cells["Amount"].Value.ToString() == ""))
						{
							decimal num4 = decimal.Parse(row2.Cells["BalanceQuantity"].Value.ToString());
							decimal.TryParse(row2.Cells["Amount"].Value.ToString(), out result);
							num2 += num4;
							num3 += result;
							d += result;
						}
					}
				}
			}
			textBoxSubtotal.Text = d.ToString(Format.TotalAmountFormat);
			foreach (UltraGridRow row3 in dataGridExpense.Rows)
			{
				if (row3.Cells["Amount"].Value != null && !(row3.Cells["Amount"].Value.ToString() == ""))
				{
					decimal.TryParse(row3.Cells["Amount"].Value.ToString(), out result);
					decimal num5 = Math.Round(comboBoxCurrency.GetFCEquivalant(result), Global.CurDecimalPoints);
					row3.Cells["AmountFC"].Value = num5;
					d2 += num5;
				}
			}
			string text3 = textBoxTotalExpense.Text = (textBoxExpenses.Text = d2.ToString(Format.TotalAmountFormat));
			num = d + d2;
			textBoxTotal.Text = num.ToString(Format.TotalAmountFormat);
		}

		private void CalculateFC()
		{
			decimal d = default(decimal);
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal d2 = default(decimal);
			_ = comboBoxCurrency.SelectedID;
			if (trackDetailedSales)
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					foreach (UltraGridRow row2 in row.ChildBands[0].Rows)
					{
						if (row2.Cells["Amount"].Value != null && !(row2.Cells["Amount"].Value.ToString() == ""))
						{
							decimal.TryParse(row2.Cells["Amount"].Value.ToString(), out result);
							row2.Cells["AmountFC"].Value = Math.Round(comboBoxCurrency.GetFCEquivalant(result), Global.CurDecimalPoints);
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
						row3.Cells["AmountFC"].Value = Math.Round(comboBoxCurrency.GetFCEquivalant(result), Global.CurDecimalPoints);
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
					d2 += result;
				}
			}
			string text3 = textBoxTotalExpense.Text = (textBoxExpenses.Text = d2.ToString(Format.TotalAmountFormat));
			num = d - d2;
			textBoxTotal.Text = num.ToString(Format.TotalAmountFormat);
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
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["ProductID"].Value != null && dataGridItems.ActiveRow.Cells["ProductID"].Value.ToString() != "")
			{
				string productID = dataGridItems.ActiveRow.Cells["ProductID"].Value.ToString();
				FormActivator.ProductQuantityFormObj.LoadData(productID);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void itemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["ProductID"].Value != null && dataGridItems.ActiveRow.Cells["ProductID"].Value.ToString() != "")
			{
				string id = dataGridItems.ActiveRow.Cells["ProductID"].Value.ToString();
				new FormHelper().EditItem(id);
			}
		}

		private void itemPicToolStripMenuItem_Click(object sender, EventArgs e)
		{
			checked
			{
				if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["ProductID"].Value != null && dataGridItems.ActiveRow.Cells["ProductID"].Value.ToString() != "")
				{
					string productID = dataGridItems.ActiveRow.Cells["ProductID"].Value.ToString();
					productPhotoViewer.ShowImage(productID, dataGridItems.Left + 20, dataGridItems.Top + 20);
				}
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (!(currentVendorAddressID == ""))
			{
				new FormHelper().EditVendorAddress(comboBoxCustomer.SelectedID, currentVendorAddressID);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(comboBoxCustomer.SelectedID);
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
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
					DataSet invoiceToPrint = Factory.W3PLInvoiceSystem.GetInvoiceToPrint(selectedID, text);
					if (invoiceToPrint == null || invoiceToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(invoiceToPrint, selectedID, "3PL Rental Invoice", SysDocTypes.W3PLInvoice, isPrint, showPrintDialog);
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
						DataSet uninvoicedGRNs = Factory.W3PLGRNSystem.GetUninvoicedGRNs(comboBoxCustomer.SelectedID);
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
							checkedListBoxGRN.Items.Clear();
							List<UltraGridRow> selectedRows = selectDocumentDialog.SelectedRows;
							foreach (UltraGridRow item2 in selectedRows)
							{
								string id = item2.Cells["Doc ID"].Value.ToString();
								string text = item2.Cells["Number"].Value.ToString();
								NameValue item = new NameValue(text, id);
								checkedListBoxGRN.Items.Add(item);
								textBoxNote.Text += text;
								if (num < selectedRows.Count - 1)
								{
									textBoxNote.Text += ",";
								}
								num++;
							}
							LoadItemsForInvoicing();
						}
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void LoadItemsForInvoicing()
		{
			DataSet obj = dataGridItems.DataSource as DataSet;
			obj.Tables[1].Rows.Clear();
			obj.Tables[0].Rows.Clear();
			foreach (object item2 in checkedListBoxGRN.Items)
			{
				NameValue obj2 = item2 as NameValue;
				string iD = obj2.ID;
				string name = obj2.Name;
				grnTable.Rows.Add(iD, name);
				DataSet gRNsItemsToInvoice = Factory.W3PLGRNSystem.GetGRNsItemsToInvoice(iD, name, dateTimePickerDate.Value);
				if (gRNsItemsToInvoice != null && gRNsItemsToInvoice.Tables.Count != 0)
				{
					List<string> list = new List<string>();
					foreach (DataRow row in gRNsItemsToInvoice.Tables["Items"].Rows)
					{
						string item = row["GRNSysDocID"].ToString() + row["GRNVoucherID"].ToString() + row["ProductID"].ToString();
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
					DataRow dataRow2 = gRNsItemsToInvoice.Tables["GRN"].Rows[0];
					if (comboBoxCustomer.SelectedID == "")
					{
						comboBoxCustomer.SelectedID = dataRow2["CustomerID"].ToString();
					}
					comboBoxCustomer.Enabled = false;
					DataSet obj3 = dataGridItems.DataSource as DataSet;
					DataTable dataTable = obj3.Tables[0];
					DataTable dataTable2 = obj3.Tables[1];
					foreach (string item3 in list)
					{
						DataRow[] array = gRNsItemsToInvoice.Tables["Items"].Select("GRNSysDocID + GRNVoucherID + ProductID = '" + item3 + "'");
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["GRNSysDocID"] = array[0]["GRNSysDocID"];
						dataRow3["GRNVoucherID"] = array[0]["GRNVoucherID"];
						dataRow3["GRNRowIndex"] = array[0]["GRNRowIndex"];
						dataRow3["ProductID"] = array[0]["ProductID"];
						dataRow3["Description"] = "GRN No: " + array[0]["GRNSysDocID"].ToString() + "-" + array[0]["GRNVoucherID"].ToString() + "   -  Item: " + array[0]["ProductID"].ToString() + " | " + array[0]["Description"].ToString();
						dataRow3["ItemDescription"] = array[0]["Description"];
						dataTable.Rows.Add(dataRow3);
						for (int i = 0; i < array.Length; i = checked(i + 1))
						{
							DataRow dataRow4 = dataTable2.NewRow();
							dataRow4["ProductID"] = array[i]["ProductID"];
							dataRow4["StartDate"] = array[i]["StartDate"];
							dataRow4["EndDate"] = array[i]["EndDate"];
							dataRow4["GRNSysDocID"] = array[i]["GRNSysDocID"];
							dataRow4["GRNVoucherID"] = array[i]["GRNVoucherID"];
							dataRow4["GRNRowIndex"] = array[i]["GRNRowIndex"];
							dataRow4["StoreQuantity"] = array[i]["StoreQuantity"];
							dataRow4["DeliveredQuantity"] = array[i]["DeliveredQuantity"];
							dataRow4["BalanceQuantity"] = array[i]["BalanceQuantity"];
							dataRow4["UnitWeight"] = array[i]["UnitWeight"];
							dataRow4["TotalWeight"] = array[i]["TotalWeight"];
							dataRow4["Price"] = array[i]["RentPrice"];
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal num = default(decimal);
							decimal.TryParse(dataRow4["TotalWeight"].ToString(), out result);
							decimal.TryParse(dataRow4["Price"].ToString(), out result2);
							num = Math.Round(Math.Round(result2 * result), Global.CurDecimalPoints);
							dataRow4["Price"] = array[i]["RentPrice"];
							dataRow4["Amount"] = num;
							dataTable2.Rows.Add(dataRow4);
						}
					}
					CalculateTotal();
					dataGridItems.DropDownMenu.Enabled = false;
					foreach (UltraGridRow row2 in dataGridItems.Rows)
					{
						row2.Cells["ProductID"].Activation = Activation.Disabled;
						row2.Cells["ProductID"].Appearance.BackColorDisabled = Color.WhiteSmoke;
						row2.Cells["ProductID"].Appearance.ForeColorDisabled = Color.Black;
						row2.Cells["Description"].Activation = Activation.Disabled;
						row2.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
						row2.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
					}
				}
			}
			dataGridItems.Rows.ExpandAll(recursive: true);
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
			SelectDocumentDialog selectDocumentDialog = sender as SelectDocumentDialog;
			List<UltraGridRow> selectedRows = selectDocumentDialog.SelectedRows;
			if (selectedRows != null && selectedRows.Count != 0)
			{
				foreach (UltraGridRow item in selectedRows)
				{
					if (item.Cells["Customer"].Value.ToString() != selectedRows[0].Cells["Customer"].Value.ToString())
					{
						ErrorHelper.ErrorMessage("Only GRNs from same customer can be selected together.");
						selectDocumentDialog.CanClose = false;
						break;
					}
				}
			}
		}

		private void W3PLInvoiceForm_Load(object sender, EventArgs e)
		{
		}

		private void mmLabel2_Click(object sender, EventArgs e)
		{
		}

		private void textBoxConsignment_TextChanged(object sender, EventArgs e)
		{
		}

		private void buttonBillings_Click(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					if (comboBoxCustomer.SelectedID == "")
					{
						ErrorHelper.InformationMessage("Please select a vendor first.");
					}
					else
					{
						SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
						if (selectDocumentDialog.ShowDialog() == DialogResult.OK)
						{
							for (int i = 0; i < dataGridExpense.Rows.Count; i++)
							{
								UltraGridRow ultraGridRow = dataGridExpense.Rows[i];
								if (!string.IsNullOrEmpty(ultraGridRow.Cells["SourceVoucherID"].Value.ToString()))
								{
									ultraGridRow.Delete(displayPrompt: false);
									i--;
								}
							}
							if (selectDocumentDialog.SelectedRows.Count > 0)
							{
								UltraGridColumn ultraGridColumn = dataGridExpense.DisplayLayout.Bands[0].Columns["SourceSysDocID"];
								bool hidden = dataGridExpense.DisplayLayout.Bands[0].Columns["SourceVoucherID"].Hidden = false;
								ultraGridColumn.Hidden = hidden;
							}
							foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
							{
								DataTable obj = dataGridExpense.DataSource as DataTable;
								DataRow dataRow = obj.NewRow();
								dataRow["Expense"] = selectedRow.Cells["ConsignExpenseID"].Value.ToString();
								dataRow["SourceSysDocID"] = selectedRow.Cells["SysDocID"].Value.ToString();
								dataRow["SourceVoucherID"] = selectedRow.Cells["VoucherID"].Value.ToString();
								dataRow["SourceRowIndex"] = selectedRow.Cells["RowIndex"].Value.ToString();
								dataRow["Description"] = selectedRow.Cells["Description"].Value.ToString();
								dataRow["Amount"] = selectedRow.Cells["Amount"].Value.ToString();
								obj.Rows.Add(dataRow);
							}
							foreach (UltraGridRow row in dataGridExpense.Rows)
							{
								if (row.Cells["SourceSysDocID"].Value != null && row.Cells["SourceSysDocID"].Value.ToString() != "")
								{
									row.Activation = Activation.NoEdit;
									row.Appearance.BackColor = Color.WhiteSmoke;
								}
							}
						}
						CalculateTotal();
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void buttonLoadSales_Click(object sender, EventArgs e)
		{
		}

		private void textBoxCommissionAmount_TextChanged(object sender, EventArgs e)
		{
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTransaction(TransactionListType.ConsignIn, consignSysDocID, consignVoucherID);
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind.PerformClick();
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.W3PLInvoiceListFormObj);
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.W3PL.W3PLInvoiceForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxGridProductUnit = new Micromind.DataControls.ProductUnitComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxTotalExpense = new Micromind.UISupport.NumberTextBox();
			dataGridExpense = new Micromind.DataControls.DataEntryGrid();
			comboBoxGridExpenseCode = new Micromind.DataControls.ExpenseCodeComboBox();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
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
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			textBoxVendorName = new System.Windows.Forms.TextBox();
			buttonSelectConsignment = new Micromind.UISupport.XPButton();
			label9 = new System.Windows.Forms.Label();
			comboBoxTerm = new Micromind.DataControls.PaymentTermComboBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
			labelCurrency = new System.Windows.Forms.Label();
			labelVoided = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			label8 = new System.Windows.Forms.Label();
			textBoxTotal = new Micromind.UISupport.NumberTextBox();
			label7 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			textBoxExpenses = new Micromind.UISupport.NumberTextBox();
			textBoxSubtotal = new Micromind.UISupport.NumberTextBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			formManager = new Micromind.DataControls.FormManager();
			checkedListBoxGRN = new System.Windows.Forms.ListBox();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTerm).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			panel1.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(productPhotoViewer);
			tabPageGeneral.Controls.Add(comboBoxGridItem);
			tabPageGeneral.Controls.Add(comboBoxGridLocation);
			tabPageGeneral.Controls.Add(comboBoxGridProductUnit);
			tabPageGeneral.Controls.Add(dataGridItems);
			tabPageGeneral.Location = new System.Drawing.Point(1, 20);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(764, 294);
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
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
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
			comboBoxGridItem.ShowConsignmentItems = false;
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowOnly3PLItems = true;
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
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridLocation.DisplayLayout.Appearance = appearance13;
			comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
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
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridProductUnit.DisplayLayout.Appearance = appearance25;
			comboBoxGridProductUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridProductUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxGridProductUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridProductUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridProductUnit.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxGridProductUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridProductUnit.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridProductUnit.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxGridProductUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridProductUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxGridProductUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridProductUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridProductUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridProductUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridProductUnit.Editable = true;
			comboBoxGridProductUnit.FilterString = "";
			comboBoxGridProductUnit.IsDataLoaded = false;
			comboBoxGridProductUnit.Location = new System.Drawing.Point(665, 46);
			comboBoxGridProductUnit.MaxDropDownItems = 12;
			comboBoxGridProductUnit.Name = "comboBoxGridProductUnit";
			comboBoxGridProductUnit.Size = new System.Drawing.Size(101, 20);
			comboBoxGridProductUnit.TabIndex = 119;
			comboBoxGridProductUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridProductUnit.Visible = false;
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
			dataGridItems.Location = new System.Drawing.Point(4, 3);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(757, 288);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			tabPageDetails.Controls.Add(textBoxTotalExpense);
			tabPageDetails.Controls.Add(dataGridExpense);
			tabPageDetails.Controls.Add(comboBoxGridExpenseCode);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(764, 294);
			textBoxTotalExpense.AllowDecimal = true;
			textBoxTotalExpense.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalExpense.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalExpense.CustomReportFieldName = "";
			textBoxTotalExpense.CustomReportKey = "";
			textBoxTotalExpense.CustomReportValueType = 1;
			textBoxTotalExpense.ForeColor = System.Drawing.Color.Black;
			textBoxTotalExpense.IsComboTextBox = false;
			textBoxTotalExpense.IsModified = false;
			textBoxTotalExpense.Location = new System.Drawing.Point(663, 269);
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
			textBoxTotalExpense.Visible = false;
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
			dataGridExpense.Location = new System.Drawing.Point(3, 4);
			dataGridExpense.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridExpense.Name = "dataGridExpense";
			dataGridExpense.ShowClearMenu = true;
			dataGridExpense.ShowDeleteMenu = true;
			dataGridExpense.ShowInsertMenu = true;
			dataGridExpense.ShowMoveRowsMenu = true;
			dataGridExpense.Size = new System.Drawing.Size(756, 287);
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
			comboBoxGridExpenseCode.ExpenseCodeType = Micromind.Common.Data.ExpenseCodeTypes.W3PLCharges;
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
			comboBoxGridExpenseCode.Visible = false;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator3,
				toolStripButtonOpenList,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator4,
				toolStripButtonDistribution
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(789, 31);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 548);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(789, 40);
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
			linePanelDown.Size = new System.Drawing.Size(789, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(679, 8);
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
			textBoxVoucherNumber.Size = new System.Drawing.Size(137, 20);
			textBoxVoucherNumber.TabIndex = 2;
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
			textBoxNote.Location = new System.Drawing.Point(49, 456);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(327, 69);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 456);
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
			ultraFormattedLinkLabel2.TabIndex = 1;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance74.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance74;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(comboBoxCustomer);
			panelDetails.Controls.Add(buttonSelectConsignment);
			panelDetails.Controls.Add(label9);
			panelDetails.Controls.Add(comboBoxTerm);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxVendorName);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(comboBoxCurrency);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Location = new System.Drawing.Point(0, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(749, 98);
			panelDetails.TabIndex = 0;
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = textBoxVendorName;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance75;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance76.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance76;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance77;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance78.BackColor2 = System.Drawing.SystemColors.Control;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance78;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance79;
			appearance80.BackColor = System.Drawing.SystemColors.Highlight;
			appearance80.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance80;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance81;
			appearance82.BorderColor = System.Drawing.Color.Silver;
			appearance82.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance82;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance83.BackColor = System.Drawing.SystemColors.Control;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance83;
			appearance84.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance84;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance85;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance86;
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
			comboBoxCustomer.TabIndex = 3;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxVendorName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVendorName.Location = new System.Drawing.Point(210, 23);
			textBoxVendorName.MaxLength = 64;
			textBoxVendorName.Name = "textBoxVendorName";
			textBoxVendorName.ReadOnly = true;
			textBoxVendorName.Size = new System.Drawing.Size(227, 20);
			textBoxVendorName.TabIndex = 4;
			textBoxVendorName.TabStop = false;
			buttonSelectConsignment.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectConsignment.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectConsignment.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectConsignment.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectConsignment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectConsignment.Location = new System.Drawing.Point(438, 21);
			buttonSelectConsignment.Name = "buttonSelectConsignment";
			buttonSelectConsignment.Size = new System.Drawing.Size(25, 22);
			buttonSelectConsignment.TabIndex = 6;
			buttonSelectConsignment.Text = "...";
			buttonSelectConsignment.UseVisualStyleBackColor = false;
			buttonSelectConsignment.Click += new System.EventHandler(buttonSelectConsignment_Click);
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(469, 47);
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
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTerm.DisplayLayout.Appearance = appearance87;
			comboBoxTerm.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTerm.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance88.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance88.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance88.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.GroupByBox.Appearance = appearance88;
			appearance89.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTerm.DisplayLayout.GroupByBox.BandLabelAppearance = appearance89;
			comboBoxTerm.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance90.BackColor2 = System.Drawing.SystemColors.Control;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance90.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTerm.DisplayLayout.GroupByBox.PromptAppearance = appearance90;
			comboBoxTerm.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTerm.DisplayLayout.MaxRowScrollRegions = 1;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTerm.DisplayLayout.Override.ActiveCellAppearance = appearance91;
			appearance92.BackColor = System.Drawing.SystemColors.Highlight;
			appearance92.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTerm.DisplayLayout.Override.ActiveRowAppearance = appearance92;
			comboBoxTerm.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTerm.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.Override.CardAreaAppearance = appearance93;
			appearance94.BorderColor = System.Drawing.Color.Silver;
			appearance94.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTerm.DisplayLayout.Override.CellAppearance = appearance94;
			comboBoxTerm.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTerm.DisplayLayout.Override.CellPadding = 0;
			appearance95.BackColor = System.Drawing.SystemColors.Control;
			appearance95.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance95.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance95.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTerm.DisplayLayout.Override.GroupByRowAppearance = appearance95;
			appearance96.TextHAlignAsString = "Left";
			comboBoxTerm.DisplayLayout.Override.HeaderAppearance = appearance96;
			comboBoxTerm.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTerm.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.Color.Silver;
			comboBoxTerm.DisplayLayout.Override.RowAppearance = appearance97;
			comboBoxTerm.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTerm.DisplayLayout.Override.TemplateAddRowAppearance = appearance98;
			comboBoxTerm.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTerm.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTerm.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTerm.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTerm.Editable = true;
			comboBoxTerm.FilterString = "";
			comboBoxTerm.HasAllAccount = false;
			comboBoxTerm.HasCustom = false;
			comboBoxTerm.IsDataLoaded = false;
			comboBoxTerm.Location = new System.Drawing.Point(551, 45);
			comboBoxTerm.MaxDropDownItems = 12;
			comboBoxTerm.Name = "comboBoxTerm";
			comboBoxTerm.ShowInactiveItems = false;
			comboBoxTerm.ShowQuickAdd = true;
			comboBoxTerm.Size = new System.Drawing.Size(153, 20);
			comboBoxTerm.TabIndex = 10;
			comboBoxTerm.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance99.FontData.BoldAsString = "True";
			appearance99.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance99;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(12, 24);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(62, 15);
			ultraFormattedLinkLabel4.TabIndex = 129;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Customer:";
			appearance100.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance100;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance101.FontData.BoldAsString = "True";
			appearance101.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance101;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 2;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance102.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance102;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance103;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance104.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance104.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance104;
			appearance105.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance105;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance106.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance106.BackColor2 = System.Drawing.SystemColors.Control;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance106.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance106;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance107;
			appearance108.BackColor = System.Drawing.SystemColors.Highlight;
			appearance108.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance108;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance109;
			appearance110.BorderColor = System.Drawing.Color.Silver;
			appearance110.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance110;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance111.BackColor = System.Drawing.SystemColors.Control;
			appearance111.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance111.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance111.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance111.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance111;
			appearance112.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance112;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance113;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance114.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance114;
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
			comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCurrency.Location = new System.Drawing.Point(551, 67);
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
			comboBoxCurrency.TabIndex = 11;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(469, 70);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 13);
			labelCurrency.TabIndex = 132;
			labelCurrency.Text = "Currency:";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(11, 387);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(465, 59);
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
			panel1.Location = new System.Drawing.Point(549, 450);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(231, 78);
			panel1.TabIndex = 3;
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(4, 48);
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
			textBoxTotal.Location = new System.Drawing.Point(88, 45);
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
			textBoxTotal.TabIndex = 4;
			textBoxTotal.Text = "0.00";
			textBoxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotal.TextChanged += new System.EventHandler(textBoxTotal_TextChanged);
			textBoxTotal.Validating += new System.ComponentModel.CancelEventHandler(textBoxTotal_Validating);
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(4, 26);
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
			textBoxExpenses.Location = new System.Drawing.Point(88, 23);
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
			textBoxExpenses.TabIndex = 3;
			textBoxExpenses.Text = "0.00";
			textBoxExpenses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				availableQuantityToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
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
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Location = new System.Drawing.Point(11, 135);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(766, 315);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControl1.TabIndex = 123;
			appearance115.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance115;
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
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(764, 294);
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
			checkedListBoxGRN.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxGRN.FormattingEnabled = true;
			checkedListBoxGRN.Location = new System.Drawing.Point(382, 469);
			checkedListBoxGRN.Name = "checkedListBoxGRN";
			checkedListBoxGRN.Size = new System.Drawing.Size(125, 56);
			checkedListBoxGRN.TabIndex = 126;
			checkedListBoxGRN.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(789, 588);
			base.Controls.Add(checkedListBoxGRN);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "W3PLInvoiceForm";
			Text = "3PL Rental Invoice";
			base.Load += new System.EventHandler(W3PLInvoiceForm_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTerm).EndInit();
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
