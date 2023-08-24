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
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerPaymentAllocationForm : Form, IForm
	{
		private bool allowEdit = true;

		private DataSet invoiceData;

		private DataSet prepaymentSourceInvoiceData;

		private DataSet paymentData;

		private DataSet currentData;

		private bool isARPayment = true;

		private bool isCashCard;

		private const string TABLENAME_CONST = "ARJournal";

		private const string IDFIELD_CONST = "";

		private bool isNewRecord = true;

		private decimal DiscountWriteOffPercent = CompanyPreferences.DiscountWriteOffPerc;

		private bool isAutoFilling;

		private string sysDocID = "";

		private string voucherID = "";

		private string payeeID = "";

		private bool isPDC;

		private DateTime voucherDate = DateTime.Now;

		private string Ref1 = "";

		private string Ref2 = "";

		private bool writeoffposting;

		private int paymentJournalID = -1;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool isVoid;

		private bool isDiscountPercent;

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

		private Label label4;

		private Label label9;

		private Label label13;

		private TextBox textBoxUnallocated;

		private TextBox textBoxVoucherType;

		private TextBox textBoxVoucherID;

		private TextBox textBoxDocID;

		private XPButton buttonAutoAllocate;

		private XPButton buttonClear;

		private Label label1;

		private TextBox textBoxRate;

		private DateTimePicker dateTimePickerDate;

		private Label label2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private Label label5;

		private TextBox textBoxRef2;

		private Label label6;

		private TextBox textBoxRef1;

		private CheckBox checkBoxrigtoffUnallocateAmount;

		private XPButton buttonProceedtoSave;

		private CheckBox checkBoxPrepayOnly;

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

		public bool IsCashOrCard
		{
			get
			{
				return isCashCard;
			}
			set
			{
				isCashCard = value;
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
				return paymentData;
			}
			set
			{
				paymentData = value;
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

		public CustomerPaymentAllocationForm()
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
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (e.CancellingEditOperation)
			{
				return;
			}
			UltraGridCell activeCell = dataGridItems.ActiveCell;
			if (activeCell.Column.Key == "Payment" || activeCell.Column.Key == "Discount")
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal result4 = default(decimal);
				decimal result5 = default(decimal);
				decimal d = decimal.Parse(textBoxUnallocated.Text, NumberStyles.Any);
				decimal.TryParse(activeCell.Row.Cells["Payment"].Text.ToString(), out result2);
				decimal.TryParse(activeCell.Row.Cells["Payment"].Value.ToString(), out result);
				decimal.TryParse(activeCell.Row.Cells["Discount"].Text.ToString(), out result4);
				decimal.TryParse(activeCell.Row.Cells["Discount"].Value.ToString(), out result3);
				decimal.TryParse(activeCell.Row.Cells["Amount Due"].Value.ToString(), out result5);
				decimal d2 = Math.Round(d + result, Global.CurDecimalPoints);
				result5 = Math.Round(result5, Global.CurDecimalPoints);
				if (d2 < result2)
				{
					ErrorHelper.InformationMessage("Total allocated amount cannot exceed available amount.");
					e.Cancel = true;
				}
				else if (result2 + result4 > result5)
				{
					ErrorHelper.InformationMessage("Total allocated amount cannot be greater than the amount due.");
					e.Cancel = true;
				}
			}
		}

		private decimal HandleMinus(string text)
		{
			decimal num = default(decimal);
			if (text.Contains("("))
			{
				string[] array = text.Split('(');
				string s = array[checked(array.Length - 1)].Replace(")", "");
				return 0m - decimal.Parse(s);
			}
			return decimal.Parse(text);
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
					decimal result4 = default(decimal);
					decimal num = default(decimal);
					decimal d = decimal.Parse(textBoxUnallocated.Text);
					decimal.TryParse(e.Cell.Value.ToString(), out result2);
					decimal.TryParse(e.Cell.OriginalValue.ToString(), out result);
					decimal.TryParse(e.Cell.Row.Cells["Discount"].Value.ToString(), out result3);
					decimal.TryParse(e.Cell.Row.Cells["Amount Due"].Value.ToString(), out result4);
					d = d + result - result2;
					textBoxUnallocated.Text = d.ToString(Format.TotalAmountFormat);
					num = result4 - result2 - result3;
					e.Cell.Row.Cells["Balance"].Value = num.ToString(Format.TotalAmountFormat);
				}
				else if (e.Cell.Column.Key == "Discount")
				{
					decimal result5 = default(decimal);
					decimal result6 = default(decimal);
					decimal result7 = default(decimal);
					decimal num2 = default(decimal);
					decimal result8 = default(decimal);
					decimal.TryParse(e.Cell.Value.ToString(), out result5);
					decimal.TryParse(e.Cell.Row.Cells["Payment"].Value.ToString(), out result6);
					decimal.TryParse(e.Cell.Row.Cells["Amount Due"].Value.ToString(), out result7);
					decimal.TryParse(e.Cell.Row.Cells["Balance"].Value.ToString(), out result8);
					decimal d2 = decimal.Parse(textBoxUnallocated.Text);
					num2 = result7 - result6 - result5;
					e.Cell.Row.Cells["Balance"].Value = num2.ToString(Format.TotalAmountFormat);
					d2 = d2 + result7 - result6 - result5 - result8;
					textBoxUnallocated.Text = d2.ToString(Format.TotalAmountFormat);
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

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			if (!e.Cell.Activated)
			{
				return;
			}
			if (IsCashOrCard)
			{
				if (e.Cell.Column.Key == "P")
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
				}
			}
			else
			{
				if (!(e.Cell.Column.Key == "P"))
				{
					return;
				}
				if (bool.Parse(e.Cell.Text.ToString()))
				{
					decimal num3 = decimal.Parse(textBoxUnallocated.Text);
					isAutoFilling = true;
					UltraGridRow activeRow3 = dataGridItems.ActiveRow;
					if (!(num3 <= 0m))
					{
						decimal num4 = default(decimal);
						decimal result4 = default(decimal);
						decimal result5 = default(decimal);
						decimal.TryParse(activeRow3.Cells["Discount"].Value.ToString(), out result4);
						decimal.TryParse(activeRow3.Cells["Amount Due"].Value.ToString(), out result5);
						result4 = Math.Round(result4, Global.CurDecimalPoints);
						result5 = Math.Round(result5, Global.CurDecimalPoints);
						num4 = ((!(num3 >= result5 - result4)) ? num3 : (result5 - result4));
						num3 -= num4;
						num3 = Math.Round(num3, Global.CurDecimalPoints);
						activeRow3.Cells["Payment"].Value = num4.ToString(Format.TotalAmountFormat);
						textBoxUnallocated.Text = num3.ToString(Format.TotalAmountFormat);
						decimal num5 = result5 - num4 - result4;
						activeRow3.Cells["Balance"].Value = num5.ToString(Format.TotalAmountFormat);
						isAutoFilling = false;
					}
				}
				else
				{
					decimal num6 = decimal.Parse(textBoxUnallocated.Text);
					isAutoFilling = true;
					UltraGridRow activeRow4 = dataGridItems.ActiveRow;
					decimal result6 = default(decimal);
					decimal result7 = default(decimal);
					decimal result8 = default(decimal);
					decimal.TryParse(activeRow4.Cells["Discount"].Value.ToString(), out result7);
					decimal.TryParse(activeRow4.Cells["Payment"].Text.ToString(), out result6);
					decimal.TryParse(activeRow4.Cells["Amount Due"].Value.ToString(), out result8);
					result6 = Math.Round(result6, Global.CurDecimalPoints);
					result8 = Math.Round(result8, Global.CurDecimalPoints);
					num6 += result6;
					activeRow4.Cells["Payment"].Value = DBNull.Value;
					activeRow4.Cells["Balance"].Value = DBNull.Value;
					activeRow4.Cells["Discount"].Value = DBNull.Value;
					isAutoFilling = false;
					textBoxUnallocated.Text = num6.ToString(Format.TotalAmountFormat);
				}
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
				if (isARPayment)
				{
					currentData = new ARJournalData();
				}
				else
				{
					currentData = new APJournalData();
				}
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["Payment"].Value != null && !(row.Cells["Payment"].Value.ToString() == "") && !(decimal.Parse(row.Cells["Payment"].Value.ToString()) == 0m))
					{
						DataRow dataRow = null;
						dataRow = ((!isARPayment) ? currentData.Tables["AP_Payment_Allocation"].NewRow() : currentData.Tables["AR_Payment_Allocation"].NewRow());
						dataRow["InvoiceSysDocID"] = row.Cells["SysDocID"].Value.ToString();
						dataRow["InvoiceVoucherID"] = row.Cells["Invoice Num"].Value.ToString();
						dataRow["PaymentSysDocID"] = textBoxDocID.Text;
						dataRow["PaymentVoucherID"] = textBoxVoucherID.Text;
						dataRow["CurrencyID"] = textBoxCurrency.Text;
						if (isARPayment)
						{
							dataRow["CustomerID"] = textBoxCustomerCode.Text;
							dataRow["ARJournalID"] = row.Cells["JournalID"].Value.ToString();
							dataRow["PaymentARID"] = paymentJournalID;
						}
						else
						{
							dataRow["VendorID"] = textBoxCustomerCode.Text;
							dataRow["APJournalID"] = row.Cells["JournalID"].Value.ToString();
							dataRow["PaymentAPID"] = paymentJournalID;
						}
						dataRow["CurrencyRate"] = textBoxRate.Text;
						dataRow["AllocationDate"] = dateTimePickerDate.Value;
						if (row.Cells["Payment"].Value != null && row.Cells["Payment"].Value.ToString() != "")
						{
							dataRow["PaymentAmount"] = row.Cells["Payment"].Value.ToString();
						}
						if (row.Cells["Discount"].Value != null && row.Cells["Discount"].Value.ToString() != "")
						{
							dataRow["DiscountAmount"] = row.Cells["Discount"].Value.ToString();
						}
						if (checkBoxrigtoffUnallocateAmount.Checked)
						{
							decimal num = default(decimal);
							num = decimal.Parse(textBoxUnallocated.Text, NumberStyles.Any);
							dataRow["UnAllocatedAmount"] = num;
						}
						else
						{
							dataRow["UnAllocatedAmount"] = DBNull.Value;
						}
						dataRow.EndEdit();
						if (isARPayment)
						{
							currentData.Tables["AR_Payment_Allocation"].Rows.Add(dataRow);
						}
						else
						{
							currentData.Tables["AP_Payment_Allocation"].Rows.Add(dataRow);
						}
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
				dataTable.Columns.Add("Invoice Date");
				dataTable.Columns.Add("Invoice Num");
				dataTable.Columns.Add("Ref");
				dataTable.Columns.Add("Cur");
				dataTable.Columns.Add("Rate");
				dataTable.Columns.Add("Orig Amount", typeof(decimal));
				dataTable.Columns.Add("Amount Due", typeof(decimal));
				dataTable.Columns.Add("Payment", typeof(decimal));
				dataTable.Columns.Add("Discount", typeof(decimal));
				dataTable.Columns.Add("Balance", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
				dataGridItems.ShowDeleteMenu = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["JournalID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].Header.Caption = "Invoice ID";
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
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Num"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Num"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Num"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Num"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoice Num"].CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CellClickAction = CellClickAction.RowSelect;
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
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Discount"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
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
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalDiscount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Discount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalDiscount"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalDiscount"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalDiscount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalDiscount"].DisplayFormat = "{0:n}";
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			decimal result = default(decimal);
			decimal.TryParse(textBoxUnallocated.Text, out result);
			if ((!(result > 0m) || checkBoxrigtoffUnallocateAmount.Checked || ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "There are still unallocated amounts.", "Do you want to continue without allocating full amounts?") != DialogResult.No) && (!checkBoxrigtoffUnallocateAmount.Checked || ValidateDiscountWriteOff()) && !SaveData())
			{
				base.DialogResult = DialogResult.None;
			}
		}

		private bool ValidateDiscountWriteOff()
		{
			decimal result = default(decimal);
			decimal discountWriteOffPercent = DiscountWriteOffPercent;
			decimal.TryParse(textBoxUnallocated.Text, out result);
			decimal result2 = default(decimal);
			decimal.TryParse(textBoxOriginalAmount.Text, out result2);
			decimal d = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["Discount"].Value != null && row.Cells["Discount"].Value.ToString() != "")
				{
					d += decimal.Parse(row.Cells["Discount"].Value.ToString());
				}
			}
			if ((result + d) / result2 * 100m > discountWriteOffPercent)
			{
				ErrorHelper.WarningMessage("Can't Write-off/Discount than allowed percentage");
				return false;
			}
			return true;
		}

		public void SetData(string sysDocID, string voucherID, string payeeID, int journalID, bool isPDC)
		{
			this.isPDC = isPDC;
			this.sysDocID = sysDocID;
			this.voucherID = voucherID;
			this.payeeID = payeeID;
			paymentJournalID = journalID;
		}

		public void SetData(string ref1, string ref2)
		{
			string text2 = Ref1 = (textBoxRef1.Text = ref1);
			text2 = (Ref2 = (textBoxRef2.Text = ref2));
		}

		public void SetData(DateTime voucherDatepar)
		{
			dateTimePickerDate.Value = voucherDatepar;
		}

		public void SetData(string sysDocID, string voucherID, string payeeID, int journalID, bool isAR, bool isPDC)
		{
			SetData(sysDocID, voucherID, payeeID, journalID, isPDC);
			isARPayment = isAR;
		}

		public void LoadData(string sysDocID, string voucherID, string customerVendorID, bool isPDC)
		{
			try
			{
				if ((!(voucherID.Trim() == "") || IsCashOrCard) && (writeoffposting || CanClose()))
				{
					bool result = false;
					if (customerVendorID != "" && isARPayment)
					{
						bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Customer", "IsParentPosting", "CustomerID", customerVendorID).ToString(), out result);
					}
					if (result)
					{
						customerVendorID = Factory.DatabaseSystem.GetFieldValue("Customer", "ParentCustomerID", "CustomerID", customerVendorID).ToString();
					}
					if (IsCashOrCard)
					{
						paymentData = Factory.ARJournalSystem.GetARPaymentToAllocate(customerVendorID, paymentJournalID, isPDC);
					}
					else if (isARPayment)
					{
						paymentData = Factory.ARJournalSystem.GetARPaymentToAllocate(sysDocID, voucherID, customerVendorID, paymentJournalID, isPDC);
					}
					else
					{
						paymentData = Factory.APJournalSystem.GetAPPaymentToAllocate(sysDocID, voucherID, customerVendorID, paymentJournalID);
					}
					if (paymentData == null || paymentData.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot find the document to allocate.");
					}
					else
					{
						string text = paymentData.Tables[0].Rows[0]["PayeeID"].ToString();
						string a = paymentData.Tables[0].Rows[0]["PayeeType"].ToString();
						if (sysDocID == "SYS_017")
						{
							checkBoxPrepayOnly.Checked = true;
							checkBoxPrepayOnly.Visible = true;
						}
						else
						{
							CheckBox checkBox = checkBoxPrepayOnly;
							bool @checked = checkBoxPrepayOnly.Visible = false;
							checkBox.Checked = @checked;
						}
						if (a == "C")
						{
							invoiceData = Factory.ARJournalSystem.GetUnallocatedInvoices(text);
						}
						else
						{
							invoiceData = Factory.APJournalSystem.GetUnallocatedInvoices(text);
						}
						if (checkBoxPrepayOnly.Visible)
						{
							prepaymentSourceInvoiceData = Factory.PurchasePrepaymentInvoiceSystem.GetInvoicesToAllocate(this.sysDocID, this.voucherID);
						}
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
			try
			{
				isDataLoading = true;
				if (paymentData != null && paymentData.Tables.Count != 0 && paymentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = paymentData.Tables[0].Rows[0];
					textBoxCurrency.Text = dataRow["CurrencyID"].ToString();
					textBoxRate.TextAlign = HorizontalAlignment.Right;
					if (dataRow["CurrencyRate"] != DBNull.Value)
					{
						textBoxRate.Text = decimal.Parse(dataRow["CurrencyRate"].ToString()).ToString("##0.#####");
					}
					else
					{
						textBoxRate.Text = "1";
					}
					if (isARPayment)
					{
						paymentJournalID = int.Parse(dataRow["ARID"].ToString());
					}
					else
					{
						paymentJournalID = int.Parse(dataRow["APID"].ToString());
					}
					if (paymentJournalID < 0)
					{
						throw new Exception("Payment Journal ID is not available.");
					}
					textBoxCustomerCode.Text = dataRow["PayeeID"].ToString();
					textBoxCustomerName.Text = dataRow["PayeeName"].ToString();
					textBoxDocID.Text = dataRow["SysDocID"].ToString();
					textBoxVoucherID.Text = dataRow["VoucherID"].ToString();
					if (dataRow["AmountFC"] != DBNull.Value && dataRow["AmountFC"].ToString() != "")
					{
						textBoxOriginalAmount.Text = decimal.Parse(dataRow["AmountFC"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else if (dataRow["Amount"] != DBNull.Value && dataRow["Amount"].ToString() != "")
					{
						textBoxOriginalAmount.Text = decimal.Parse(dataRow["Amount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					if (dataRow["AmountFC"] != DBNull.Value)
					{
						textBoxUnallocated.Text = decimal.Parse(dataRow["UnAllocatedAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxUnallocated.Text = decimal.Parse(dataRow["UnAllocatedAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					int sysDocType = -1;
					if (dataRow["SysDocType"] != DBNull.Value)
					{
						int.Parse(dataRow["SysDocType"].ToString());
					}
					else if (sysDocID == "SYS_010")
					{
						sysDocType = 1002;
					}
					textBoxVoucherType.Text = PublicFunctions.GetSysDocTypeString(sysDocType);
					if (invoiceData != null && invoiceData.Tables.Count != 0 && invoiceData.Tables[0].Rows.Count != 0)
					{
						DataTable dataTable = dataGridItems.DataSource as DataTable;
						DataTable dataTable2 = (!isARPayment) ? invoiceData.Tables["AP_Payment_Allocation"] : invoiceData.Tables["AR_Payment_Allocation"];
						dataTable.Rows.Clear();
						string text = textBoxCurrency.Text;
						decimal d = decimal.Parse(textBoxRate.Text);
						string baseCurrencyID = Global.BaseCurrencyID;
						foreach (DataRow row in dataTable2.Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							string text2 = row["CurrencyID"].ToString();
							decimal.Parse(row["CurrencyRate"].ToString());
							if (checkBoxPrepayOnly.Checked)
							{
								string text3 = row["SysDocID"].ToString();
								string text4 = row["VoucherID"].ToString();
								if (prepaymentSourceInvoiceData.Tables[0].Rows.Count == 0 || prepaymentSourceInvoiceData.Tables[0].Select("SysDocID = '" + text3 + "' AND VoucherID = '" + text4 + "'").Length == 0)
								{
									continue;
								}
							}
							dataRow3["SysDocID"] = row["SysDocID"];
							dataRow3["JournalID"] = row["JournalID"];
							if (isARPayment)
							{
								dataRow3["Invoice Date"] = DateTime.Parse(row["ARDate"].ToString()).ToShortDateString();
							}
							else
							{
								dataRow3["Invoice Date"] = DateTime.Parse(row["APDate"].ToString()).ToShortDateString();
							}
							dataRow3["Invoice Num"] = row["VoucherID"];
							dataRow3["Ref"] = row["Reference"];
							dataRow3["Orig Amount"] = row["OriginalAmount"];
							decimal num = decimal.Parse(row["AmountDue"].ToString());
							if (text == baseCurrencyID)
							{
								if (text2 != text)
								{
									num = decimal.Parse(row["AmountDueBase"].ToString());
								}
							}
							else if (text2 == baseCurrencyID)
							{
								num = ((!(Factory.CurrencySystem.GetCurrencyRateType(text2) == "M")) ? Math.Round(num * d, 4) : Math.Round(num / d, 4));
							}
							dataRow3["Amount Due"] = num;
							dataRow3["Cur"] = row["CurrencyID"];
							if (row["CurrencyRate"] != DBNull.Value)
							{
								dataRow3["Rate"] = decimal.Parse(row["CurrencyRate"].ToString()).ToString("##0.####");
							}
							if (row["Discount"] != DBNull.Value)
							{
								dataRow3["Discount"] = row["Discount"];
							}
							else
							{
								dataRow3["Discount"] = 0;
							}
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						dataGridItems.DisplayLayout.Bands[0].Columns["Amount Due"].Header.Caption = "Amount Due (" + textBoxCurrency.Text + ")";
						foreach (UltraGridRow row2 in dataGridItems.Rows)
						{
							string a = row2.Cells["Cur"].Value.ToString();
							if ((!(textBoxCurrency.Text != baseCurrencyID) || !(a != baseCurrencyID) || !(a != textBoxCurrency.Text)) && textBoxCurrency.Text == baseCurrencyID)
							{
								_ = (a != baseCurrencyID);
							}
						}
						AutoAllocate();
						CalculateTotal();
					}
				}
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
				if (isARPayment)
				{
					if (((ARJournalData)currentData).PaymentAllocationTable.Rows.Count == 0)
					{
						return true;
					}
					flag = Factory.ARJournalSystem.CreatePaymentAllocation((ARJournalData)currentData);
				}
				else
				{
					if (((APJournalData)currentData).PaymentAllocationTable.Rows.Count == 0)
					{
						return true;
					}
					flag = Factory.APJournalSystem.CreatePaymentAllocation((APJournalData)currentData);
				}
				if (checkBoxrigtoffUnallocateAmount.Checked && flag)
				{
					ClearForm();
					textBoxOriginalAmount.Clear();
					textBoxUnallocated.Clear();
					checkBoxrigtoffUnallocateAmount.Checked = false;
					writeoffposting = true;
					LoadData(sysDocID, voucherID, payeeID, isPDC);
					if (!GetData())
					{
						return false;
					}
					if (isARPayment)
					{
						if (((ARJournalData)currentData).PaymentAllocationTable.Rows.Count == 0)
						{
							return true;
						}
						flag = Factory.ARJournalSystem.CreatePaymentAllocation((ARJournalData)currentData);
					}
					else
					{
						if (((APJournalData)currentData).PaymentAllocationTable.Rows.Count == 0)
						{
							return true;
						}
						flag = Factory.APJournalSystem.CreatePaymentAllocation((APJournalData)currentData);
					}
				}
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
				isDiscountPercent = false;
				isDiscountPercent = false;
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				IsVoid = false;
				writeoffposting = false;
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
			if (base.DialogResult != DialogResult.OK)
			{
				decimal result = default(decimal);
				decimal.TryParse(textBoxUnallocated.Text, out result);
				if (result > 0m && dataGridItems.Rows.Count > 0 && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure you want to continue without allocating the amounts?") == DialogResult.No)
				{
					base.DialogResult = DialogResult.None;
					e.Cancel = true;
				}
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
				dataGridItems.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.SortSingle;
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					LoadData(sysDocID, voucherID, payeeID, isPDC);
					if (IsCashOrCard)
					{
						buttonSave.Enabled = false;
						buttonProceedtoSave.Visible = true;
						string text3 = textBoxDocID.Text = (textBoxVoucherID.Text = "");
					}
					else
					{
						buttonSave.Enabled = true;
						buttonProceedtoSave.Visible = false;
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

		private void buttonAutoAllocate_Click(object sender, EventArgs e)
		{
			AutoAllocate();
		}

		private void buttonProceedtoSave_Click(object sender, EventArgs e)
		{
			if (!AllocationProcess())
			{
				base.DialogResult = DialogResult.None;
			}
		}

		private bool AllocationProcess()
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
				bool flag = true;
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					ARJournalData aRJournalData = (ARJournalData)currentData;
					foreach (DataRow row in aRJournalData.Tables["AR_Payment_Allocation"].Rows)
					{
						string text = row["InvoiceSysDocID"].ToString();
						string text2 = row["InvoiceVoucherID"].ToString();
						DataRow[] array = paymentData.Tables[0].Select("[SysDocID]='" + text + "'    AND VoucherID='" + text2 + "'");
						for (int i = 0; i < array.Length; i = checked(i + 1))
						{
							row["PaymentARID"] = array[i]["ARID"];
						}
					}
					PaymentData = aRJournalData;
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

		private void checkBoxPrepayOnly_CheckedChanged(object sender, EventArgs e)
		{
			FillData();
		}

		private void AutoAllocate()
		{
			textBoxUnallocated.Text = decimal.Parse(paymentData.Tables[0].Rows[0]["UnAllocatedAmount"].ToString()).ToString(Format.TotalAmountFormat);
			decimal num = decimal.Parse(textBoxUnallocated.Text, NumberStyles.Any);
			isAutoFilling = true;
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["P"].Activation != Activation.Disabled)
				{
					if (num <= 0m)
					{
						row.Cells["Payment"].Value = DBNull.Value;
						row.Cells["Discount"].Value = DBNull.Value;
						row.Cells["Balance"].Value = DBNull.Value;
						row.Cells["P"].Value = false;
					}
					else
					{
						decimal num2 = default(decimal);
						decimal result = default(decimal);
						decimal result2 = default(decimal);
						decimal.TryParse(row.Cells["Discount"].Value.ToString(), out result);
						decimal.TryParse(row.Cells["Amount Due"].Value.ToString(), out result2);
						num2 = ((!(num >= result2 - result)) ? num : (result2 - result));
						num -= num2;
						row.Cells["P"].Value = true;
						if (num2 > 0m)
						{
							row.Cells["Payment"].Value = Math.Round(num2, Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
						}
						else
						{
							row.Cells["Payment"].Value = decimal.Parse(Math.Round(num2, Global.CurDecimalPoints).ToString(Format.TotalAmountFormat), NumberStyles.Any);
						}
						row.Cells["Balance"].Value = Math.Round(result2 - num2 - result, Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
					}
				}
			}
			isAutoFilling = false;
			textBoxUnallocated.Text = Math.Round(num, 2).ToString(Format.TotalAmountFormat);
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
			textBoxUnallocated.Text = Math.Round(decimal.Parse(paymentData.Tables[0].Rows[0]["UnAllocatedAmount"].ToString()), 2).ToString(Format.TotalAmountFormat);
			isAutoFilling = true;
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				row.Cells["Payment"].Value = DBNull.Value;
				row.Cells["Discount"].Value = DBNull.Value;
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
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.CustomerPaymentAllocationForm));
			panelButtons = new System.Windows.Forms.Panel();
			buttonProceedtoSave = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			panelDetails = new System.Windows.Forms.Panel();
			checkBoxrigtoffUnallocateAmount = new System.Windows.Forms.CheckBox();
			label5 = new System.Windows.Forms.Label();
			textBoxRef2 = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			label1 = new System.Windows.Forms.Label();
			textBoxRate = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			labelCustomerName = new System.Windows.Forms.Label();
			labelCustomerCode = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			labelCurrency = new System.Windows.Forms.Label();
			textBoxCustomerCode = new System.Windows.Forms.TextBox();
			textBoxUnallocated = new System.Windows.Forms.TextBox();
			textBoxOriginalAmount = new System.Windows.Forms.TextBox();
			textBoxCurrency = new System.Windows.Forms.TextBox();
			textBoxVoucherType = new System.Windows.Forms.TextBox();
			textBoxVoucherID = new System.Windows.Forms.TextBox();
			textBoxDocID = new System.Windows.Forms.TextBox();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			buttonClear = new Micromind.UISupport.XPButton();
			buttonAutoAllocate = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			checkBoxPrepayOnly = new System.Windows.Forms.CheckBox();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonProceedtoSave);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 488);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(690, 40);
			panelButtons.TabIndex = 4;
			buttonProceedtoSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonProceedtoSave.BackColor = System.Drawing.Color.Silver;
			buttonProceedtoSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonProceedtoSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonProceedtoSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonProceedtoSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonProceedtoSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonProceedtoSave.Location = new System.Drawing.Point(114, 8);
			buttonProceedtoSave.Name = "buttonProceedtoSave";
			buttonProceedtoSave.Size = new System.Drawing.Size(96, 24);
			buttonProceedtoSave.TabIndex = 15;
			buttonProceedtoSave.Text = "Proceed to Save";
			buttonProceedtoSave.UseVisualStyleBackColor = false;
			buttonProceedtoSave.Click += new System.EventHandler(buttonProceedtoSave_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(690, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(580, 8);
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
			panelDetails.Controls.Add(checkBoxrigtoffUnallocateAmount);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(textBoxRef2);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRate);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(label9);
			panelDetails.Controls.Add(labelCustomerName);
			panelDetails.Controls.Add(labelCustomerCode);
			panelDetails.Controls.Add(label13);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(textBoxCustomerCode);
			panelDetails.Controls.Add(textBoxUnallocated);
			panelDetails.Controls.Add(textBoxOriginalAmount);
			panelDetails.Controls.Add(textBoxCurrency);
			panelDetails.Controls.Add(textBoxVoucherType);
			panelDetails.Controls.Add(textBoxVoucherID);
			panelDetails.Controls.Add(textBoxDocID);
			panelDetails.Controls.Add(textBoxCustomerName);
			panelDetails.Location = new System.Drawing.Point(0, 1);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(662, 140);
			panelDetails.TabIndex = 0;
			checkBoxrigtoffUnallocateAmount.AutoSize = true;
			checkBoxrigtoffUnallocateAmount.Location = new System.Drawing.Point(490, 95);
			checkBoxrigtoffUnallocateAmount.Name = "checkBoxrigtoffUnallocateAmount";
			checkBoxrigtoffUnallocateAmount.Size = new System.Drawing.Size(126, 17);
			checkBoxrigtoffUnallocateAmount.TabIndex = 154;
			checkBoxrigtoffUnallocateAmount.Text = "Write-off Unallocated";
			checkBoxrigtoffUnallocateAmount.UseVisualStyleBackColor = true;
			checkBoxrigtoffUnallocateAmount.Visible = false;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(195, 93);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(69, 13);
			label5.TabIndex = 153;
			label5.Text = "Reference 2:";
			textBoxRef2.Location = new System.Drawing.Point(285, 93);
			textBoxRef2.MaxLength = 20;
			textBoxRef2.Name = "textBoxRef2";
			textBoxRef2.ReadOnly = true;
			textBoxRef2.Size = new System.Drawing.Size(107, 20);
			textBoxRef2.TabIndex = 151;
			textBoxRef2.TabStop = false;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(9, 93);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(69, 13);
			label6.TabIndex = 152;
			label6.Text = "Reference 1:";
			textBoxRef1.Location = new System.Drawing.Point(97, 93);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.ReadOnly = true;
			textBoxRef1.Size = new System.Drawing.Size(92, 20);
			textBoxRef1.TabIndex = 150;
			textBoxRef1.TabStop = false;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(195, 53);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 147;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(285, 71);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDate.TabIndex = 0;
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
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(9, 74);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(77, 13);
			label4.TabIndex = 144;
			label4.Text = "Voucher Type:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(195, 75);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(82, 13);
			label2.TabIndex = 144;
			label2.Text = "Allocation Date:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(9, 52);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(44, 13);
			label9.TabIndex = 144;
			label9.Text = "Doc ID:";
			labelCustomerName.AutoSize = true;
			labelCustomerName.Location = new System.Drawing.Point(9, 30);
			labelCustomerName.Name = "labelCustomerName";
			labelCustomerName.Size = new System.Drawing.Size(85, 13);
			labelCustomerName.TabIndex = 144;
			labelCustomerName.Text = "Customer Name:";
			labelCustomerCode.AutoSize = true;
			labelCustomerCode.Location = new System.Drawing.Point(9, 8);
			labelCustomerCode.Name = "labelCustomerCode";
			labelCustomerCode.Size = new System.Drawing.Size(82, 13);
			labelCustomerCode.TabIndex = 144;
			labelCustomerCode.Text = "Customer Code:";
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(416, 73);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(67, 13);
			label13.TabIndex = 144;
			label13.Text = "Unallocated:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(416, 51);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(68, 13);
			label3.TabIndex = 144;
			label3.Text = "Orig Amount:";
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
			textBoxUnallocated.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUnallocated.Location = new System.Drawing.Point(490, 70);
			textBoxUnallocated.MaxLength = 64;
			textBoxUnallocated.Name = "textBoxUnallocated";
			textBoxUnallocated.ReadOnly = true;
			textBoxUnallocated.Size = new System.Drawing.Size(142, 20);
			textBoxUnallocated.TabIndex = 3;
			textBoxUnallocated.TabStop = false;
			textBoxUnallocated.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
			textBoxVoucherType.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVoucherType.Location = new System.Drawing.Point(97, 71);
			textBoxVoucherType.MaxLength = 64;
			textBoxVoucherType.Name = "textBoxVoucherType";
			textBoxVoucherType.ReadOnly = true;
			textBoxVoucherType.Size = new System.Drawing.Size(92, 20);
			textBoxVoucherType.TabIndex = 3;
			textBoxVoucherType.TabStop = false;
			textBoxVoucherID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVoucherID.Location = new System.Drawing.Point(302, 49);
			textBoxVoucherID.MaxLength = 64;
			textBoxVoucherID.Name = "textBoxVoucherID";
			textBoxVoucherID.ReadOnly = true;
			textBoxVoucherID.Size = new System.Drawing.Size(90, 20);
			textBoxVoucherID.TabIndex = 3;
			textBoxVoucherID.TabStop = false;
			textBoxDocID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDocID.Location = new System.Drawing.Point(97, 49);
			textBoxDocID.MaxLength = 64;
			textBoxDocID.Name = "textBoxDocID";
			textBoxDocID.ReadOnly = true;
			textBoxDocID.Size = new System.Drawing.Size(92, 20);
			textBoxDocID.TabIndex = 3;
			textBoxDocID.TabStop = false;
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
			availableQuantityToolStripMenuItem.Click += new System.EventHandler(availableQuantityToolStripMenuItem_Click);
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
			buttonClear.Location = new System.Drawing.Point(581, 147);
			buttonClear.Name = "buttonClear";
			buttonClear.Size = new System.Drawing.Size(96, 24);
			buttonClear.TabIndex = 2;
			buttonClear.Text = "Clear";
			buttonClear.UseVisualStyleBackColor = false;
			buttonClear.Click += new System.EventHandler(buttonClear_Click);
			buttonAutoAllocate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAutoAllocate.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonAutoAllocate.BackColor = System.Drawing.Color.DarkGray;
			buttonAutoAllocate.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAutoAllocate.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAutoAllocate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonAutoAllocate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAutoAllocate.Location = new System.Drawing.Point(479, 147);
			buttonAutoAllocate.Name = "buttonAutoAllocate";
			buttonAutoAllocate.Size = new System.Drawing.Size(96, 24);
			buttonAutoAllocate.TabIndex = 1;
			buttonAutoAllocate.Text = "Auto Allocate";
			buttonAutoAllocate.UseVisualStyleBackColor = false;
			buttonAutoAllocate.Click += new System.EventHandler(buttonAutoAllocate_Click);
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
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance3;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance10;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance12;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance13;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 176);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(664, 306);
			dataGridItems.TabIndex = 3;
			dataGridItems.Text = "dataEntryGrid1";
			checkBoxPrepayOnly.AutoSize = true;
			checkBoxPrepayOnly.Location = new System.Drawing.Point(12, 154);
			checkBoxPrepayOnly.Name = "checkBoxPrepayOnly";
			checkBoxPrepayOnly.Size = new System.Drawing.Size(247, 17);
			checkBoxPrepayOnly.TabIndex = 155;
			checkBoxPrepayOnly.Text = "Show related invoices for this pre-payment only";
			checkBoxPrepayOnly.UseVisualStyleBackColor = true;
			checkBoxPrepayOnly.CheckedChanged += new System.EventHandler(checkBoxPrepayOnly_CheckedChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(690, 528);
			base.Controls.Add(checkBoxPrepayOnly);
			base.Controls.Add(buttonClear);
			base.Controls.Add(formManager);
			base.Controls.Add(panelDetails);
			base.Controls.Add(buttonAutoAllocate);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dataGridItems);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "CustomerPaymentAllocationForm";
			Text = "Payment Allocation";
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
