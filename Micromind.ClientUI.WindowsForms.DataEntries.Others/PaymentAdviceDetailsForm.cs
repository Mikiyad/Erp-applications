using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class PaymentAdviceDetailsForm : Form, IForm
	{
		private bool allowEdit = true;

		private DataSet invoiceData;

		private DataSet paymentData;

		private DataSet currentData;

		private bool isViewOnly;

		private bool isARPayment = true;

		private const string TABLENAME_CONST = "ARJournal";

		private const string IDFIELD_CONST = "";

		private bool isNewRecord = true;

		private bool isAutoFilling;

		private string sysDocID = "";

		private string voucherID = "";

		private string payeeID = "";

		private string payeeName = "";

		private string currencyID = "";

		private bool isPDC;

		private decimal currencyRate = 1m;

		private int paymentJournalID = -1;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool isVoid;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private FormManager formManager;

		private DataEntryGrid dataGridItems;

		private Panel panelDetails;

		private TextBox textBoxCustomerName;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private Label labelCurrency;

		private Label labelCustomerName;

		private Label labelCustomerCode;

		private Label label3;

		private TextBox textBoxCustomerCode;

		private TextBox textBoxOriginalAmount;

		private TextBox textBoxCurrency;

		private XPButton buttonClear;

		private Label label1;

		private TextBox textBoxRate;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonDueDate;

		private DateTimePicker dateTimePickerDueDate;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2005;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		public bool IsARPayment
		{
			get
			{
				return isARPayment;
			}
			set
			{
				isARPayment = value;
				if (!value)
				{
					labelCustomerCode.Text = "Vendor Code:";
					labelCustomerName.Text = "Vendor Name:";
				}
			}
		}

		public bool IsViewOnly
		{
			get
			{
				return isViewOnly;
			}
			set
			{
				isViewOnly = value;
			}
		}

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

		public DataSet PaymentData
		{
			get
			{
				return currentData;
			}
			set
			{
				currentData = value;
			}
		}

		public DataSet InvoiceData
		{
			get
			{
				return invoiceData;
			}
			set
			{
				invoiceData = value;
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
				if (!screenRight.New || !screenRight.Edit)
				{
					buttonSave.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
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
				if (isVoid != value)
				{
					isVoid = value;
					panelDetails.Enabled = !value;
					dataGridItems.Enabled = !value;
					buttonSave.Enabled = !value;
				}
			}
		}

		public PaymentAdviceDetailsForm()
		{
			InitializeComponent();
			AddEvents();
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
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			base.FormClosing += Form_FormClosing;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			base.KeyDown += SalesOrderForm_KeyDown;
			dateTimePickerDueDate.ValueChanged += DateTimePickerDueDate_ValueChanged;
		}

		private void DateTimePickerDueDate_ValueChanged(object sender, EventArgs e)
		{
			FillData();
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (e.CancellingEditOperation)
			{
				return;
			}
			UltraGridCell activeCell = dataGridItems.ActiveCell;
			if (activeCell.Column.Key == "Payment")
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal.TryParse(activeCell.Row.Cells["Payment"].Text.ToString(), out result2);
				decimal.TryParse(activeCell.Row.Cells["Payment"].Value.ToString(), out result);
				decimal.TryParse(activeCell.Row.Cells["Amount Due"].Value.ToString(), out result3);
				result3 = Math.Round(result3, Global.CurDecimalPoints);
				if (result2 > result3)
				{
					ErrorHelper.InformationMessage("Total allocated amount cannot be greater than the amount due.");
					e.Cancel = true;
				}
			}
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void dataGridItems_GotFocus(object sender, EventArgs e)
		{
			if (!dataGridItems.Focused)
			{
				dataGridItems.DisplayLayout.Bands[0].AddNew();
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (isAutoFilling)
				{
					return;
				}
				if (e.Cell.Column.Key == "Payment")
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal num = default(decimal);
					decimal.TryParse(e.Cell.Value.ToString(), out result2);
					decimal.TryParse(e.Cell.OriginalValue.ToString(), out result);
					decimal.TryParse(e.Cell.Row.Cells["Amount Due"].Value.ToString(), out result3);
					num = result3 - result2;
					e.Cell.Row.Cells["Balance"].Value = num.ToString(Format.TotalAmountFormat);
					CalculateTotalPayment();
				}
				if (e.Cell.Row.Cells["Payment"].Value.ToString() == "")
				{
					e.Cell.Row.Cells["Balance"].Value = DBNull.Value;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			_ = dataGridItems.ActiveRow;
		}

		private void CalculateTotalPayment()
		{
			try
			{
				decimal num = default(decimal);
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (!row.Cells["Payment"].Value.IsNullOrEmpty())
					{
						num += decimal.Parse(row.Cells["Payment"].Value.ToString());
					}
				}
				textBoxOriginalAmount.Text = num.ToString(Format.TotalAmountFormat);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
			if (e.Cell.Activated && e.Cell.Column.Key == "P")
			{
				if (bool.Parse(e.Cell.Text.ToString()))
				{
					isAutoFilling = true;
					UltraGridRow activeRow = dataGridItems.ActiveRow;
					decimal num = default(decimal);
					decimal result = default(decimal);
					decimal.TryParse(activeRow.Cells["Amount Due"].Value.ToString(), out result);
					num = (result = Math.Round(result, Global.CurDecimalPoints));
					activeRow.Cells["Payment"].Value = result.ToString(Format.TotalAmountFormat);
					decimal num2 = result - num;
					activeRow.Cells["Balance"].Value = num2.ToString(Format.TotalAmountFormat);
					isAutoFilling = false;
				}
				else
				{
					isAutoFilling = true;
					UltraGridRow activeRow2 = dataGridItems.ActiveRow;
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(activeRow2.Cells["Payment"].Text.ToString(), out result2);
					decimal.TryParse(activeRow2.Cells["Amount Due"].Value.ToString(), out result3);
					result2 = Math.Round(result2, Global.CurDecimalPoints);
					result3 = Math.Round(result3, Global.CurDecimalPoints);
					activeRow2.Cells["Payment"].Value = DBNull.Value;
					activeRow2.Cells["Balance"].Value = DBNull.Value;
					isAutoFilling = false;
				}
				CalculateTotalPayment();
			}
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
		}

		private void comboBoxGridItem_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				if (dataGridItems.ActiveCell.Column.Key == "Item Code" && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() == "")
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
			_ = dataGridItems.ActiveRow;
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Cost")
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
				currentData = new APJournalData();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["Payment"].Value != null && !(row.Cells["Payment"].Value.ToString() == "") && !(decimal.Parse(row.Cells["Payment"].Value.ToString()) == 0m))
					{
						DataRow dataRow = null;
						dataRow = currentData.Tables["AP_Payment_Advice"].NewRow();
						dataRow["InvoiceSysDocID"] = row.Cells["SysDocID"].Value.ToString();
						dataRow["InvoiceVoucherID"] = row.Cells["Invoice Num"].Value.ToString();
						dataRow["CurrencyID"] = textBoxCurrency.Text;
						dataRow["OriginalAmount"] = row.Cells["Orig Amount"].Value.ToString();
						dataRow["AmountDue"] = row.Cells["Amount Due"].Value.ToString();
						dataRow["VendorID"] = textBoxCustomerCode.Text;
						dataRow["APJournalID"] = row.Cells["JournalID"].Value.ToString();
						dataRow["APDate"] = row.Cells["Invoice Date"].Value.ToString();
						dataRow["APDueDate"] = row.Cells["Due Date"].Value.ToString();
						if (textBoxRate.Text != "")
						{
							dataRow["CurrencyRate"] = textBoxRate.Text;
						}
						else
						{
							dataRow["CurrencyRate"] = 1;
						}
						if (row.Cells["Payment"].Value != null && row.Cells["Payment"].Value.ToString() != "")
						{
							dataRow["PaymentAmount"] = row.Cells["Payment"].Value.ToString();
						}
						dataRow.EndEdit();
						currentData.Tables["AP_Payment_Advice"].Rows.Add(dataRow);
					}
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
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("P", typeof(bool)).DefaultValue = false;
				dataTable.Columns.Add("JournalID");
				dataTable.Columns.Add("SysDocID");
				dataTable.Columns.Add("Invoice Date", typeof(DateTime));
				dataTable.Columns.Add("Due Date", typeof(DateTime));
				dataTable.Columns.Add("Invoice Num");
				dataTable.Columns.Add("Ref");
				dataTable.Columns.Add("Cur");
				dataTable.Columns.Add("Rate");
				dataTable.Columns.Add("Orig Amount", typeof(decimal));
				dataTable.Columns.Add("Amount Due", typeof(decimal));
				dataTable.Columns.Add("Payment", typeof(decimal));
				dataTable.Columns.Add("Balance", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				if (isViewOnly)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["P"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["JournalID"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["Payment"].CellAppearance.BackColor = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["Payment"].CellClickAction = CellClickAction.RowSelect;
					dataGridItems.DisplayLayout.Bands[0].Columns["Payment"].CellActivation = Activation.NoEdit;
				}
				dataGridItems.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
				dataGridItems.ShowDeleteMenu = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].MaxWidth = 18;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].MinWidth = 18;
				dataGridItems.DisplayLayout.Bands[0].Columns["P"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["JournalID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].Header.Caption = "Doc ID";
				dataGridItems.DisplayLayout.Bands[0].Columns["Cur"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cur"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cur"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cur"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cur"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns["Rate"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Rate"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Rate"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Rate"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Rate"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Rate"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Rate"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Date"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Date"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Date"].Format = "d";
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Date"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Date"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Date"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns["Due Date"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Due Date"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Due Date"].Format = "d";
				dataGridItems.DisplayLayout.Bands[0].Columns["Due Date"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Due Date"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Due Date"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Num"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Num"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Num"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Num"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Num"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ref"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ref"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ref"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ref"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ref"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns["Orig Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Orig Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Orig Amount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Orig Amount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Orig Amount"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Orig Amount"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Orig Amount"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Orig Amount"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns["Payment"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Payment"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Payment"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Payment"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Balance"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalOrigAmount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Orig Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalOrigAmount"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalOrigAmount"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalOrigAmount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalOrigAmount"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalAmountDue", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalAmountDue"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalAmountDue"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalAmountDue"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalAmountDue"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalAllocated", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Payment"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalAllocated"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalAllocated"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalAllocated"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalAllocated"].DisplayFormat = "{0:n}";
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (!SaveData())
			{
				base.DialogResult = DialogResult.None;
			}
		}

		public void SetData(string vendorID, string vendorName, string currencyID, decimal currencyRate)
		{
			payeeID = vendorID;
			payeeName = vendorName;
			this.currencyID = currencyID;
			this.currencyRate = currencyRate;
		}

		public void LoadData(string sysDocID, string voucherID, string customerVendorID, bool isPDC)
		{
			try
			{
				if (!(payeeID.Trim() == "") && CanClose())
				{
					invoiceData = Factory.APJournalSystem.GetUnallocatedInvoices(payeeID);
					FillData();
					IsNewRecord = false;
					formManager.ResetDirty();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			try
			{
				isDataLoading = true;
				textBoxRate.TextAlign = HorizontalAlignment.Right;
				textBoxCustomerCode.Text = payeeID;
				textBoxCustomerName.Text = payeeName;
				textBoxCurrency.Text = currencyID;
				textBoxRate.Text = currencyRate.ToString();
				if (isViewOnly)
				{
					if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
					{
						goto IL_00dc;
					}
				}
				else if (invoiceData != null && invoiceData.Tables.Count != 0 && invoiceData.Tables[0].Rows.Count != 0)
				{
					goto IL_00dc;
				}
				goto end_IL_0000;
				IL_00dc:
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				DataTable dataTable2 = (!isViewOnly) ? invoiceData.Tables["AP_Payment_Allocation"] : currentData.Tables["AP_Payment_Advice"];
				string text = textBoxCurrency.Text;
				decimal d = decimal.Parse(textBoxRate.Text);
				string baseCurrencyID = Global.BaseCurrencyID;
				DateTime t = dateTimePickerDueDate.Value.EndOfDay();
				foreach (DataRow row in dataTable2.Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					string a = row["CurrencyID"].ToString();
					decimal.Parse(row["CurrencyRate"].ToString());
					if (!isViewOnly)
					{
						dataRow2["SysDocID"] = row["SysDocID"];
						dataRow2["JournalID"] = row["JournalID"];
						dataRow2["Invoice Num"] = row["VoucherID"];
						dataRow2["Ref"] = row["Reference"];
					}
					else
					{
						dataRow2["SysDocID"] = row["InvoiceSysDocID"];
						dataRow2["Invoice Num"] = row["InvoiceVoucherID"];
						dataRow2["Payment"] = row["PaymentAmount"];
					}
					if (!row["APDate"].IsDBNullOrEmpty())
					{
						dataRow2["Invoice Date"] = DateTime.Parse(row["APDate"].ToString()).ToShortDateString();
					}
					if (!row["APDueDate"].IsDBNullOrEmpty())
					{
						dataRow2["Due Date"] = DateTime.Parse(row["APDueDate"].ToString()).ToShortDateString();
					}
					dataRow2["Orig Amount"] = row["OriginalAmount"];
					DateTime t2 = DateTime.MinValue;
					if (!row["APDueDate"].IsDBNullOrEmpty())
					{
						t2 = DateTime.Parse(row["APDueDate"].ToString());
					}
					if (!radioButtonDueDate.Checked || !(t2 > t))
					{
						decimal d2 = default(decimal);
						decimal d3 = default(decimal);
						if (!row["AmountDue"].IsDBNullOrEmpty())
						{
							d2 = decimal.Parse(row["AmountDue"].ToString());
						}
						if (!row["Prepayment"].IsDBNullOrEmpty())
						{
							d3 = decimal.Parse(row["Prepayment"].ToString());
						}
						if (text == baseCurrencyID)
						{
							if (a != text)
							{
								d2 = decimal.Parse(row["AmountDueBase"].ToString());
							}
						}
						else if (a == baseCurrencyID)
						{
							d2 = ((!(Factory.CurrencySystem.GetCurrencyRateType(a) == "M")) ? Math.Round(d2 * d, Global.CurDecimalPoints, MidpointRounding.AwayFromZero) : Math.Round(d2 / d, Global.CurDecimalPoints, MidpointRounding.AwayFromZero));
						}
						dataRow2["Amount Due"] = d2 - d3;
						dataRow2["Cur"] = row["CurrencyID"];
						if (row["CurrencyRate"] != DBNull.Value)
						{
							dataRow2["Rate"] = decimal.Parse(row["CurrencyRate"].ToString()).ToString("##0.####");
						}
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
				}
				dataTable.AcceptChanges();
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"].Header.Caption = "Unpaid (" + textBoxCurrency.Text + ")";
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					string a2 = row2.Cells["Cur"].Value.ToString();
					if ((!(textBoxCurrency.Text != baseCurrencyID) || !(a2 != baseCurrencyID) || !(a2 != textBoxCurrency.Text)) && textBoxCurrency.Text == baseCurrencyID)
					{
						_ = (a2 != baseCurrencyID);
					}
				}
				CalculateTotalPayment();
				CalculateTotal();
				end_IL_0000:;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				isDataLoading = false;
			}
		}

		private bool SaveData()
		{
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
				bool flag = true;
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					base.DialogResult = DialogResult.OK;
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
			return true;
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
				textBoxCustomerName.Clear();
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
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

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void OnActivated()
		{
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult != DialogResult.OK && 0m > 0m && dataGridItems.Rows.Count > 0 && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure you want to continue without allocating the amounts?") == DialogResult.No)
			{
				base.DialogResult = DialogResult.None;
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
				SetupGrid();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					if (!isViewOnly)
					{
						LoadData(sysDocID, voucherID, payeeID, isPDC);
					}
					else
					{
						FillData();
						XPButton xPButton = buttonSave;
						XPButton xPButton2 = buttonClear;
						RadioButton radioButton = radioButtonAll;
						RadioButton radioButton2 = radioButtonDueDate;
						bool flag2 = dateTimePickerDueDate.Visible = false;
						bool flag4 = radioButton2.Visible = flag2;
						bool flag6 = radioButton.Visible = flag4;
						bool visible = xPButton2.Visible = flag6;
						xPButton.Visible = visible;
					}
				}
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
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

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				_ = dataGridItems.ActiveCell;
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			string text = voucherID;
			if (text != null)
			{
				new FormHelper().EditTransaction(sysDocID, text);
			}
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
		}

		private void CalculateTotal()
		{
		}

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				dataGridItems.ActiveRow.Delete(displayPrompt: false);
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
				if (!IsDirty || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string text = "";
					string text2 = "";
					DataSet salesQuoteToPrint = Factory.SalesQuoteSystem.GetSalesQuoteToPrint(text, text2);
					if (salesQuoteToPrint == null || salesQuoteToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(salesQuoteToPrint, text, "Sales Quote", SysDocTypes.SalesQuote, isPrint, showPrintDialog);
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

		private void radioButtonDueDate_CheckedChanged(object sender, EventArgs e)
		{
			dateTimePickerDueDate.Enabled = radioButtonDueDate.Checked;
			FillData();
		}

		private void buttonAutoAllocate_Click(object sender, EventArgs e)
		{
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
			isAutoFilling = true;
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				row.Cells["Payment"].Value = DBNull.Value;
				row.Cells["Balance"].Value = DBNull.Value;
				row.Cells["P"].Value = false;
			}
			isAutoFilling = false;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.PaymentAdviceDetailsForm));
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			panelDetails = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			textBoxRate = new System.Windows.Forms.TextBox();
			labelCustomerName = new System.Windows.Forms.Label();
			labelCustomerCode = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			labelCurrency = new System.Windows.Forms.Label();
			textBoxCustomerCode = new System.Windows.Forms.TextBox();
			textBoxOriginalAmount = new System.Windows.Forms.TextBox();
			textBoxCurrency = new System.Windows.Forms.TextBox();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			buttonClear = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonDueDate = new System.Windows.Forms.RadioButton();
			dateTimePickerDueDate = new System.Windows.Forms.DateTimePicker();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 518);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(822, 40);
			panelButtons.TabIndex = 4;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(822, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(712, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
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
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRate);
			panelDetails.Controls.Add(labelCustomerName);
			panelDetails.Controls.Add(labelCustomerCode);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(textBoxCustomerCode);
			panelDetails.Controls.Add(textBoxOriginalAmount);
			panelDetails.Controls.Add(textBoxCurrency);
			panelDetails.Controls.Add(textBoxCustomerName);
			panelDetails.Location = new System.Drawing.Point(0, 1);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(662, 79);
			panelDetails.TabIndex = 0;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(416, 27);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(33, 13);
			label1.TabIndex = 146;
			label1.Text = "Rate:";
			textBoxRate.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRate.Location = new System.Drawing.Point(490, 26);
			textBoxRate.MaxLength = 64;
			textBoxRate.Name = "textBoxRate";
			textBoxRate.ReadOnly = true;
			textBoxRate.Size = new System.Drawing.Size(142, 20);
			textBoxRate.TabIndex = 145;
			textBoxRate.TabStop = false;
			labelCustomerName.AutoSize = true;
			labelCustomerName.Location = new System.Drawing.Point(9, 30);
			labelCustomerName.Name = "labelCustomerName";
			labelCustomerName.Size = new System.Drawing.Size(75, 13);
			labelCustomerName.TabIndex = 144;
			labelCustomerName.Text = "Vendor Name:";
			labelCustomerCode.AutoSize = true;
			labelCustomerCode.Location = new System.Drawing.Point(9, 8);
			labelCustomerCode.Name = "labelCustomerCode";
			labelCustomerCode.Size = new System.Drawing.Size(72, 13);
			labelCustomerCode.TabIndex = 144;
			labelCustomerCode.Text = "Vendor Code:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(416, 51);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(34, 13);
			label3.TabIndex = 144;
			label3.Text = "Total:";
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(416, 6);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 13);
			labelCurrency.TabIndex = 144;
			labelCurrency.Text = "Currency:";
			textBoxCustomerCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerCode.Location = new System.Drawing.Point(97, 5);
			textBoxCustomerCode.MaxLength = 64;
			textBoxCustomerCode.Name = "textBoxCustomerCode";
			textBoxCustomerCode.ReadOnly = true;
			textBoxCustomerCode.Size = new System.Drawing.Size(295, 20);
			textBoxCustomerCode.TabIndex = 3;
			textBoxCustomerCode.TabStop = false;
			textBoxOriginalAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxOriginalAmount.Location = new System.Drawing.Point(490, 48);
			textBoxOriginalAmount.MaxLength = 64;
			textBoxOriginalAmount.Name = "textBoxOriginalAmount";
			textBoxOriginalAmount.ReadOnly = true;
			textBoxOriginalAmount.Size = new System.Drawing.Size(142, 20);
			textBoxOriginalAmount.TabIndex = 3;
			textBoxOriginalAmount.TabStop = false;
			textBoxOriginalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCurrency.Location = new System.Drawing.Point(490, 4);
			textBoxCurrency.MaxLength = 64;
			textBoxCurrency.Name = "textBoxCurrency";
			textBoxCurrency.ReadOnly = true;
			textBoxCurrency.Size = new System.Drawing.Size(142, 20);
			textBoxCurrency.TabIndex = 3;
			textBoxCurrency.TabStop = false;
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.Location = new System.Drawing.Point(97, 27);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(295, 20);
			textBoxCustomerName.TabIndex = 3;
			textBoxCustomerName.TabStop = false;
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
			salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
			salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			salesStatisticsToolStripMenuItem.Text = "Sales Statistics...";
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemPicToolStripMenuItem.Text = "Item Photo...";
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
			buttonClear.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClear.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonClear.BackColor = System.Drawing.Color.DarkGray;
			buttonClear.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClear.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClear.Location = new System.Drawing.Point(708, 86);
			buttonClear.Name = "buttonClear";
			buttonClear.Size = new System.Drawing.Size(96, 24);
			buttonClear.TabIndex = 2;
			buttonClear.Text = "Clear";
			buttonClear.UseVisualStyleBackColor = false;
			buttonClear.Click += new System.EventHandler(buttonClear_Click);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
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
			dataGridItems.Location = new System.Drawing.Point(12, 113);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(796, 399);
			dataGridItems.TabIndex = 3;
			dataGridItems.Text = "dataEntryGrid1";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(14, 89);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(78, 17);
			radioButtonAll.TabIndex = 17;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All invoices";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonDueDate.AutoSize = true;
			radioButtonDueDate.Location = new System.Drawing.Point(105, 91);
			radioButtonDueDate.Name = "radioButtonDueDate";
			radioButtonDueDate.Size = new System.Drawing.Size(59, 17);
			radioButtonDueDate.TabIndex = 18;
			radioButtonDueDate.Text = "Due by";
			radioButtonDueDate.UseVisualStyleBackColor = true;
			radioButtonDueDate.CheckedChanged += new System.EventHandler(radioButtonDueDate_CheckedChanged);
			dateTimePickerDueDate.Enabled = false;
			dateTimePickerDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDueDate.Location = new System.Drawing.Point(174, 90);
			dateTimePickerDueDate.Name = "dateTimePickerDueDate";
			dateTimePickerDueDate.Size = new System.Drawing.Size(121, 20);
			dateTimePickerDueDate.TabIndex = 19;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(822, 558);
			base.Controls.Add(dateTimePickerDueDate);
			base.Controls.Add(radioButtonDueDate);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(buttonClear);
			base.Controls.Add(formManager);
			base.Controls.Add(panelDetails);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dataGridItems);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "PaymentAdviceDetailsForm";
			Text = "Payment Advice";
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
