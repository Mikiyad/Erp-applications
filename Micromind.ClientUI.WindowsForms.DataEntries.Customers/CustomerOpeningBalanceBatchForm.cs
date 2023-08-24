using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerOpeningBalanceBatchForm : Form, IForm
	{
		private OpeningBalanceBatchData currentData;

		private const string TABLENAME_CONST = "Opening_Balance_Batch";

		private const string IDFIELD_CONST = "BatchID";

		private bool isNewRecord = true;

		private int docNumberTryCount;

		private ScreenAccessRight screenRight;

		private bool isVoid;

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

		private UltraGroupBox ultraGroupBox1;

		private UltraLabel labelBalance;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private LocationComboBox comboBoxLocation;

		private Label label4;

		private UltraLabel ultraLabel1;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator3;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private customersFlatComboBox comboBoxCustomer;

		private MMTextBox textBoxAccountName;

		private AllAccountsComboBox comboBoxAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private CurrencyComboBox comboBoxCurrency;

		private ToolStripButton toolStripButtonGroup;

		private CurrencySelector currencySelector1;

		private Label labelCurrency;

		private ToolStripButton toolStripButtonExcelImport;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator5;

		private CompanyDivisionComboBox comboBoxDivision;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4002;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

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
					textBoxVoucherNumber.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = true;
				}
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
					labelVoided.Visible = value;
					if (value)
					{
						buttonVoid.Text = UIMessages.Unvoid;
					}
					else
					{
						buttonVoid.Text = UIMessages.Void;
					}
				}
			}
		}

		public CustomerOpeningBalanceBatchForm()
		{
			InitializeComponent();
			AddEvents();
			checked
			{
				int num;
				for (num = 0; num < contextMenuStrip1.Items.Count; num++)
				{
					dataGridItems.DropDownMenu.Items.Add(contextMenuStrip1.Items[num]);
					num--;
				}
			}
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
			comboBoxLocation.SelectedIndexChanged += comboBoxLocation_SelectedIndexChanged;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dataGridItems.BeforeCellDeactivate += dataGridItems_BeforeCellDeactivate;
			toolStripButtonGroup.CheckedChanged += toolStripButton2_Click;
			base.FormClosing += Form_FormClosing;
		}

		private void dataGridItems_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridCell activeCell = dataGridItems.ActiveCell;
			if (activeCell == null)
			{
				return;
			}
			if (activeCell.Column.Key == "InvoiceAmount")
			{
				if (activeCell.Value == null || activeCell.Value.ToString() == "")
				{
					activeCell.Value = 0.ToString(Format.TotalAmountFormat);
				}
			}
			else if (activeCell.Column.Key == "BalanceAmount" && (activeCell.Value == null || activeCell.Value.ToString() == ""))
			{
				activeCell.Value = 0.ToString(Format.TotalAmountFormat);
			}
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void FillProductQuantityAndCost()
		{
		}

		private void comboBoxGridProductUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxCustomer_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
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

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			int index = dataGridItems.ActiveRow.Index;
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (!(e.Cell.Column.Key == "CustomerID"))
			{
				return;
			}
			activeRow.Cells["CustomerName"].Value = comboBoxCustomer.SelectedName;
			checked
			{
				if (index > 0)
				{
					if (activeRow.Cells["TransactionDate"].Value == null || activeRow.Cells["TransactionDate"].Value.ToString() == "")
					{
						activeRow.Cells["TransactionDate"].Value = dataGridItems.Rows[index - 1].Cells["TransactionDate"].Value;
					}
					if (activeRow.Cells["DueDate"].Value == null || activeRow.Cells["DueDate"].Value.ToString() == "")
					{
						activeRow.Cells["DueDate"].Value = dataGridItems.Rows[index - 1].Cells["DueDate"].Value;
					}
					if (dataGridItems.Rows[activeRow.Index - 1].Cells["Reference"].Value != null && dataGridItems.Rows[activeRow.Index - 1].Cells["Reference"].Value.ToString() != "")
					{
						activeRow.Cells["Reference"].Value = CommonLib.GetNextDocumentNumber(dataGridItems.Rows[activeRow.Index - 1].Cells["Reference"].Value.ToString());
					}
				}
				else
				{
					if (activeRow.Cells["TransactionDate"].Value == null || activeRow.Cells["TransactionDate"].Value.ToString() == "")
					{
						activeRow.Cells["TransactionDate"].Value = dateTimePickerDate.Value;
					}
					if (activeRow.Cells["DueDate"].Value == null || activeRow.Cells["DueDate"].Value.ToString() == "")
					{
						activeRow.Cells["DueDate"].Value = dateTimePickerDate.Value;
					}
				}
				if (activeRow.Cells["InvoiceAmount"].Value == null || activeRow.Cells["InvoiceAmount"].Value.ToString() == "")
				{
					activeRow.Cells["InvoiceAmount"].Value = 0.ToString(Format.TotalAmountFormat);
				}
				if (activeRow.Cells["BalanceAmount"].Value == null || activeRow.Cells["BalanceAmount"].Value.ToString() == "")
				{
					activeRow.Cells["BalanceAmount"].Value = 0.ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
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
			comboBoxCustomer.Clear();
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && !activeRow.IsGroupByRow)
			{
				if (activeRow.Cells["CustomerID"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select an item.");
					e.Cancel = true;
					activeRow.Cells["CustomerID"].Activate();
				}
				else if (activeRow.Cells["TransactionDate"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please enter the transaction date.");
					e.Cancel = true;
					activeRow.Cells["TransactionDate"].Activate();
				}
				else
				{
					labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "CustomerID")
			{
				e.RaiseErrorEvent = false;
				comboBoxCustomer.Text = dataGridItems.ActiveCell.Text;
				comboBoxCustomer.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "InvoiceAmount" || dataGridItems.ActiveCell.Column.Key.ToString() == "BalanceAmount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid amount. Please enter a numeric value.");
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new OpeningBalanceBatchData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.OpeningBalanceBatchTable.Rows[0] : currentData.OpeningBalanceBatchTable.NewRow();
				if (dateTimePickerDate.Value > DateTime.Today)
				{
					dataRow["BatchDate"] = dateTimePickerDate.Value;
				}
				else
				{
					DateTime value = dateTimePickerDate.Value;
					dataRow["BatchDate"] = new DateTime(value.Year, value.Month, value.Day, 11, 59, 59);
				}
				dataRow["BatchID"] = textBoxVoucherNumber.Text;
				dataRow["LocationID"] = comboBoxLocation.SelectedID;
				dataRow["AccountID"] = comboBoxAccount.SelectedID;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Description"] = textBoxNote.Text;
				dataRow["SysDocID"] = "SYS_010";
				dataRow["CurrencyID"] = currencySelector1.SelectedID;
				dataRow["CurrencyRate"] = currencySelector1.Rate;
				dataRow["BatchType"] = (byte)1;
				if (!string.IsNullOrEmpty(comboBoxDivision.SelectedID))
				{
					dataRow["DivisionID"] = comboBoxDivision.SelectedID;
				}
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.OpeningBalanceBatchTable.Rows.Add(dataRow);
				}
				currentData.OpeningBalanceBatchDetailsTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.OpeningBalanceBatchDetailsTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["BatchID"] = textBoxVoucherNumber.Text;
					dataRow2["SysDocID"] = "SYS_010";
					dataRow2["AccountID"] = row.Cells["CustomerID"].Value.ToString();
					dataRow2["TransactionDate"] = row.Cells["TransactionDate"].Value.ToString();
					dataRow2["DueDate"] = DateTime.Parse(row.Cells["DueDate"].Value.ToString());
					dataRow2["Reference"] = row.Cells["Reference"].Value.ToString();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					if (row.Cells["InvoiceAmount"].Value.ToString() != "")
					{
						dataRow2["InvoiceAmount"] = decimal.Parse(row.Cells["InvoiceAmount"].Value.ToString());
					}
					if (row.Cells["BalanceAmount"].Value.ToString() != "")
					{
						dataRow2["BalanceAmount"] = decimal.Parse(row.Cells["BalanceAmount"].Value.ToString());
					}
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.OpeningBalanceBatchDetailsTable.Rows.Add(dataRow2);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			LoadData(voucherID);
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("CustomerID");
				dataTable.Columns.Add("CustomerName");
				dataTable.Columns.Add("Reference");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("TransactionDate", typeof(DateTime));
				dataTable.Columns.Add("DueDate", typeof(DateTime));
				dataTable.Columns.Add("InvoiceAmount", typeof(decimal));
				dataTable.Columns.Add("BalanceAmount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["CustomerID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["CustomerID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["CustomerID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["CustomerID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["CustomerID"].ValueList = comboBoxCustomer;
				dataGridItems.DisplayLayout.Bands[0].Columns["CustomerID"].Header.Caption = "Customer Code";
				dataGridItems.DisplayLayout.Bands[0].Columns["CustomerName"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["CustomerName"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["CustomerName"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["CustomerName"].Header.Caption = "Customer Name";
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["InvoiceAmount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["InvoiceAmount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["InvoiceAmount"].Header.Caption = "Inv Amt";
				dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].Header.Caption = "Doc Number";
				dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["TransactionDate"].Header.Caption = "Invoice Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["DueDate"].Header.Caption = "Due Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["BalanceAmount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["BalanceAmount"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["BalanceAmount"].Header.Caption = "Balance Amt";
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("InvoiceTotal", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["InvoiceAmount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["InvoiceTotal"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("BalanceTotal", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["BalanceAmount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["BalanceTotal"].Appearance.TextHAlign = HAlign.Right;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGridItems.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.OpeningBalanceBatchSystem.GetOpeningBalanceBatchByID("SYS_010", voucherID);
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
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					toolStripButtonGroup.Checked = false;
					DataRow dataRow = currentData.Tables["Opening_Balance_Batch"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["BatchDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["BatchID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
					textBoxNote.Text = dataRow["Description"].ToString();
					currencySelector1.SelectedID = dataRow["CurrencyID"].ToString();
					if (dataRow["CurrencyRate"] != DBNull.Value)
					{
						currencySelector1.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
					}
					else
					{
						currencySelector1.Rate = 1m;
					}
					comboBoxAccount.SelectedID = dataRow["AccountID"].ToString();
					if (dataRow["DivisionID"] != DBNull.Value)
					{
						comboBoxDivision.SelectedID = dataRow["DivisionID"].ToString();
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Opening_Balance_Batch_Detail") && currentData.OpeningBalanceBatchDetailsTable.Rows.Count != 0)
					{
						dataGridItems.BeginUpdate();
						foreach (DataRow row in currentData.Tables["Opening_Balance_Batch_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["CustomerID"] = row["AccountID"];
							dataRow3["CustomerName"] = row["CustomerName"];
							dataRow3["Description"] = row["Description"];
							dataRow3["Reference"] = row["Reference"];
							dataRow3["TransactionDate"] = row["TransactionDate"];
							dataRow3["DueDate"] = row["DueDate"];
							dataRow3["InvoiceAmount"] = row["InvoiceAmount"];
							dataRow3["BalanceAmount"] = row["BalanceAmount"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						dataGridItems.EndUpdate();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private bool SaveData()
		{
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
		{
			toolStripButtonGroup.Checked = false;
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
				bool flag = Factory.OpeningBalanceBatchSystem.CreateOpeningBalanceBatch(currentData, !isNewRecord);
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
				if (ex.Number == 1055)
				{
					ErrorHelper.WarningMessage(ex.Message);
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
				docNumberTryCount = 0;
			}
		}

		private bool ValidateData()
		{
			if (isNewRecord && dateTimePickerDate.Value < DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
				return false;
			}
			if (isNewRecord && dateTimePickerDate.Value > DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxAccount.SelectedID == "" || comboBoxLocation.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			for (int i = 0; i < dataGridItems.Rows.Count; i = checked(i + 1))
			{
				if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
				{
					dataGridItems.Rows[i].Delete(displayPrompt: false);
				}
				else if (dataGridItems.Rows[i].Cells["CustomerID"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select an item.");
					dataGridItems.Rows[i].Activate();
					return false;
				}
			}
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one adjustment row.");
				return false;
			}
			formManager.IsFieldDirty(textBoxVoucherNumber);
			return true;
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				_ = row;
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
				textBoxNote.Clear();
				textBoxRef1.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxCustomer.Clear();
				comboBoxLocation.Clear();
				comboBoxAccount.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxDivision.Clear();
				toolStripButtonGroup.Checked = false;
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

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this record?") == DialogResult.Yes && Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private bool Delete()
		{
			try
			{
				return Factory.OpeningBalanceBatchSystem.DeleteOpeningBalanceBatch("SYS_010", textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Opening_Balance_Batch", "BatchID", textBoxVoucherNumber.Text, "SysDocID", "SYS_010");
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Opening_Balance_Batch", "BatchID", textBoxVoucherNumber.Text, "SysDocID", "SYS_010");
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Opening_Balance_Batch", "BatchID", "SysDocID", "SYS_010");
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Opening_Balance_Batch", "BatchID", "SysDocID", "SYS_010");
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Opening_Balance_Batch", "BatchID", "", toolStripTextBoxFind.Text);
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

		private void JournalLeavesForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				SetupGrid();
				dateTimePickerDate.Value = DateTime.Now;
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

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDocNumber))
			{
				textBoxVoucherNumber.ReadOnly = true;
			}
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null && !dataGridItems.ActiveRow.IsGroupByRow && dataGridItems.ActiveCell.Column.Key == "CustomerID" && dataGridItems.ActiveCell.Row.Index > 0 && (dataGridItems.ActiveCell.Value == null || dataGridItems.ActiveCell.Value.ToString() == ""))
			{
				string text2 = (string)(dataGridItems.ActiveCell.Value = (comboBoxCustomer.SelectedID = dataGridItems.Rows[checked(dataGridItems.ActiveCell.Row.Index - 1)].Cells["CustomerID"].Value.ToString()));
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.OpeningBalanceBatchSystem.GetNextBatchNumber("SYS_010");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
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
					string sysDocID = "SYS_010";
					string text = textBoxVoucherNumber.Text;
					DataSet openingBalanceBatchToPrint = Factory.OpeningBalanceBatchSystem.GetOpeningBalanceBatchToPrint(sysDocID, text);
					if (openingBalanceBatchToPrint == null || openingBalanceBatchToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(openingBalanceBatchToPrint, sysDocID, "Opening Balance Batch", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void printListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PrintList();
		}

		private string GetDocumentTitle()
		{
			return "Inventory Adjustment - Products List";
		}

		private void PrintList()
		{
			try
			{
				PrintHelper.PreviewDocument(ultraGridPrintDocument1, Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAccount.SelectedID);
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			if (toolStripButtonGroup.Checked)
			{
				dataGridItems.DisplayLayout.Bands[0].SortedColumns.Add("CustomerID", descending: true, groupBy: true);
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].SortedColumns.Remove("CustomerID");
			}
		}

		private void toolStripButtonExcelImport_Click(object sender, EventArgs e)
		{
			dataGridItems.ImportFromExcel(autoFill: true);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerOpeningBalanceBatchListFormObj);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCompanyDivision(comboBoxDivision.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.CustomerOpeningBalanceBatchForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonExcelImport = new System.Windows.Forms.ToolStripButton();
			toolStripButtonGroup = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			labelBalance = new Infragistics.Win.Misc.UltraLabel();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			panelDetails = new System.Windows.Forms.Panel();
			currencySelector1 = new Micromind.DataControls.CurrencySelector();
			labelCurrency = new System.Windows.Forms.Label();
			textBoxAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label4 = new System.Windows.Forms.Label();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxCurrency = new Micromind.DataControls.CurrencyComboBox();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxDivision = new Micromind.DataControls.CompanyDivisionComboBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator4,
				toolStripButtonOpenList,
				toolStripSeparator5,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButtonExcelImport,
				toolStripButtonGroup
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(740, 31);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonExcelImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonExcelImport.Image = Micromind.ClientUI.Properties.Resources.excelimport;
			toolStripButtonExcelImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonExcelImport.Name = "toolStripButtonExcelImport";
			toolStripButtonExcelImport.Size = new System.Drawing.Size(28, 28);
			toolStripButtonExcelImport.Text = "Import from Excel";
			toolStripButtonExcelImport.Click += new System.EventHandler(toolStripButtonExcelImport_Click);
			toolStripButtonGroup.CheckOnClick = true;
			toolStripButtonGroup.Image = Micromind.ClientUI.Properties.Resources.Groupby;
			toolStripButtonGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonGroup.Name = "toolStripButtonGroup";
			toolStripButtonGroup.Size = new System.Drawing.Size(139, 28);
			toolStripButtonGroup.Text = "Group By Customer";
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 450);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(740, 40);
			panelButtons.TabIndex = 10;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(42, 24);
			buttonVoid.TabIndex = 14;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Visible = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 13;
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
			buttonNew.TabIndex = 12;
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
			linePanelDown.Size = new System.Drawing.Size(740, 1);
			linePanelDown.TabIndex = 0;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(630, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 15;
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
			buttonSave.TabIndex = 11;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraCalcManager1.ContainingControl = this;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(347, 4);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(142, 21);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(123, 3);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 21);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(494, 8);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(61, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(557, 5);
			textBoxRef1.MaxLength = 30;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(92, 21);
			textBoxRef1.TabIndex = 3;
			textBoxNote.Location = new System.Drawing.Point(123, 72);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(526, 21);
			textBoxNote.TabIndex = 9;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(15, 74);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(34, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(15, 6);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(87, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Batch Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance3.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance3.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance3;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
			ultraGroupBox1.Controls.Add(labelBalance);
			ultraGroupBox1.Controls.Add(ultraLabel1);
			ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			ultraGroupBox1.Location = new System.Drawing.Point(11, 418);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(716, 21);
			ultraGroupBox1.TabIndex = 17;
			ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
			labelBalance.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			appearance4.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance4.FontData.BoldAsString = "True";
			appearance4.FontData.Name = "Tahoma";
			appearance4.ForeColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance4.TextHAlignAsString = "Right";
			appearance4.TextVAlignAsString = "Middle";
			labelBalance.Appearance = appearance4;
			labelBalance.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			labelBalance.Location = new System.Drawing.Point(573, 3);
			labelBalance.Name = "labelBalance";
			labelBalance.Size = new System.Drawing.Size(141, 16);
			labelBalance.TabIndex = 113;
			labelBalance.Text = "0.00";
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance5.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Tahoma";
			appearance5.ForeColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance5.TextHAlignAsString = "Right";
			appearance5.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance5;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(2, 3);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(571, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Balance:";
			panelDetails.Controls.Add(comboBoxDivision);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(currencySelector1);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(textBoxAccountName);
			panelDetails.Controls.Add(comboBoxAccount);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(comboBoxLocation);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 33);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(655, 104);
			panelDetails.TabIndex = 0;
			currencySelector1.BackColor = System.Drawing.Color.WhiteSmoke;
			currencySelector1.Location = new System.Drawing.Point(494, 27);
			currencySelector1.MaximumSize = new System.Drawing.Size(99999, 20);
			currencySelector1.MinimumSize = new System.Drawing.Size(5, 20);
			currencySelector1.Name = "currencySelector1";
			currencySelector1.Rate = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			currencySelector1.SelectedID = "";
			currencySelector1.Size = new System.Drawing.Size(155, 20);
			currencySelector1.TabIndex = 6;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(434, 30);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(55, 13);
			labelCurrency.TabIndex = 134;
			labelCurrency.Text = "Currency:";
			textBoxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccountName.CustomReportFieldName = "";
			textBoxAccountName.CustomReportKey = "";
			textBoxAccountName.CustomReportValueType = 1;
			textBoxAccountName.IsComboTextBox = false;
			textBoxAccountName.IsModified = false;
			textBoxAccountName.Location = new System.Drawing.Point(314, 49);
			textBoxAccountName.MaxLength = 64;
			textBoxAccountName.Name = "textBoxAccountName";
			textBoxAccountName.ReadOnly = true;
			textBoxAccountName.Size = new System.Drawing.Size(335, 21);
			textBoxAccountName.TabIndex = 8;
			textBoxAccountName.TabStop = false;
			comboBoxAccount.Assigned = false;
			comboBoxAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAccount.CalcManager = ultraCalcManager1;
			comboBoxAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccount.CustomReportFieldName = "";
			comboBoxAccount.CustomReportKey = "";
			comboBoxAccount.CustomReportValueType = 1;
			comboBoxAccount.DescriptionTextBox = textBoxAccountName;
			comboBoxAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAccount.Editable = true;
			comboBoxAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAccount.FilterString = "";
			comboBoxAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAccount.FilterSysDocID = "";
			comboBoxAccount.HasAllAccount = false;
			comboBoxAccount.HasCustom = false;
			comboBoxAccount.IsDataLoaded = false;
			comboBoxAccount.Location = new System.Drawing.Point(123, 49);
			comboBoxAccount.MaxDropDownItems = 12;
			comboBoxAccount.Name = "comboBoxAccount";
			comboBoxAccount.ShowInactiveItems = false;
			comboBoxAccount.ShowQuickAdd = true;
			comboBoxAccount.Size = new System.Drawing.Size(188, 21);
			comboBoxAccount.TabIndex = 7;
			comboBoxAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance6.FontData.BoldAsString = "True";
			ultraFormattedLinkLabel3.Appearance = appearance6;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(15, 50);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(93, 15);
			ultraFormattedLinkLabel3.TabIndex = 125;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "OP.Bal Account:";
			appearance7.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance7;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(15, 29);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(60, 13);
			label4.TabIndex = 122;
			label4.Text = "Location:";
			comboBoxLocation.AlwaysInEditMode = true;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CalcManager = ultraCalcManager1;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			appearance8.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance8;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance9.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance9;
			appearance10.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance10;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance11.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance11.BackColor2 = System.Drawing.SystemColors.Control;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance11;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance12;
			appearance13.BackColor = System.Drawing.SystemColors.Highlight;
			appearance13.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance13;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance14;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			appearance15.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance15;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance16.BackColor = System.Drawing.SystemColors.Control;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance16;
			appearance17.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance17;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance18;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance19.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance19;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(123, 27);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowAll = false;
			comboBoxLocation.ShowConsignIn = false;
			comboBoxLocation.ShowConsignOut = false;
			comboBoxLocation.ShowDefaultLocationOnly = false;
			comboBoxLocation.ShowInactiveItems = false;
			comboBoxLocation.ShowNormalLocations = true;
			comboBoxLocation.ShowPOSOnly = false;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.ShowWarehouseOnly = false;
			comboBoxLocation.Size = new System.Drawing.Size(139, 21);
			comboBoxLocation.TabIndex = 4;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(266, 6);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(75, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Batch Date:";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 339);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(712, 81);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				printListToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(130, 26);
			printListToolStripMenuItem.Name = "printListToolStripMenuItem";
			printListToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			printListToolStripMenuItem.Text = "Print List...";
			printListToolStripMenuItem.Click += new System.EventHandler(printListToolStripMenuItem_Click);
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
			dataGridItems.CalcManager = ultraCalcManager1;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			appearance20.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance20;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance21.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance21;
			appearance22.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance22;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance23.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance23.BackColor2 = System.Drawing.SystemColors.Control;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance23;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			appearance24.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance24;
			appearance25.BackColor = System.Drawing.SystemColors.Highlight;
			appearance25.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance25;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance26;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			appearance27.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance27;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance28.BackColor = System.Drawing.SystemColors.Control;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance28;
			appearance29.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance29;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			appearance30.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance30;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance31.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance31;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 141);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(714, 279);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			comboBoxCurrency.Assigned = false;
			comboBoxCurrency.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCurrency.CalcManager = ultraCalcManager1;
			comboBoxCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCurrency.CustomReportFieldName = "";
			comboBoxCurrency.CustomReportKey = "";
			comboBoxCurrency.CustomReportValueType = 1;
			comboBoxCurrency.DescriptionTextBox = null;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCurrency.DisplayLayout.Appearance = appearance32;
			comboBoxCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance33.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.GroupByBox.Appearance = appearance33;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance34;
			comboBoxCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance35.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance35.BackColor2 = System.Drawing.SystemColors.Control;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance35;
			comboBoxCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			appearance36.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance36;
			appearance37.BackColor = System.Drawing.SystemColors.Highlight;
			appearance37.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance37;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.CardAreaAppearance = appearance38;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			appearance39.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCurrency.DisplayLayout.Override.CellAppearance = appearance39;
			comboBoxCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance40.BackColor = System.Drawing.SystemColors.Control;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance40;
			appearance41.TextHAlignAsString = "Left";
			comboBoxCurrency.DisplayLayout.Override.HeaderAppearance = appearance41;
			comboBoxCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance42.BackColor = System.Drawing.SystemColors.Window;
			appearance42.BorderColor = System.Drawing.Color.Silver;
			comboBoxCurrency.DisplayLayout.Override.RowAppearance = appearance42;
			comboBoxCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance43.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance43;
			comboBoxCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCurrency.Editable = true;
			comboBoxCurrency.FilterString = "";
			comboBoxCurrency.HasAllAccount = false;
			comboBoxCurrency.HasCustom = false;
			comboBoxCurrency.IsDataLoaded = false;
			comboBoxCurrency.Location = new System.Drawing.Point(663, 244);
			comboBoxCurrency.MaxDropDownItems = 12;
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.ShowInactiveItems = false;
			comboBoxCurrency.ShowQuickAdd = true;
			comboBoxCurrency.Size = new System.Drawing.Size(77, 21);
			comboBoxCurrency.TabIndex = 122;
			comboBoxCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCurrency.Visible = false;
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCustomer.CalcManager = ultraCalcManager1;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = null;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance44;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance45.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance45;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance46;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance47.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance47.BackColor2 = System.Drawing.SystemColors.Control;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance47;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance48.BackColor = System.Drawing.SystemColors.Window;
			appearance48.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance48;
			appearance49.BackColor = System.Drawing.SystemColors.Highlight;
			appearance49.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance49;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance50;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			appearance51.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance51;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance52.BackColor = System.Drawing.SystemColors.Control;
			appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance52.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance52;
			appearance53.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance53;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance54.BackColor = System.Drawing.SystemColors.Window;
			appearance54.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance54;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance55.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance55;
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
			comboBoxCustomer.Location = new System.Drawing.Point(668, 217);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ShowConsignmentOnly = false;
			comboBoxCustomer.ShowInactive = false;
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = false;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(72, 21);
			comboBoxCustomer.TabIndex = 121;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCustomer.Visible = false;
			ultraGridPrintDocument1.DocumentName = "Inventory Adjustment - List";
			ultraGridPrintDocument1.Grid = dataGridItems;
			appearance56.FontData.BoldAsString = "True";
			ultraFormattedLinkLabel1.Appearance = appearance56;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(266, 30);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(51, 15);
			ultraFormattedLinkLabel1.TabIndex = 135;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Division:";
			appearance57.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance57;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxDivision.Assigned = false;
			comboBoxDivision.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDivision.CalcManager = ultraCalcManager1;
			comboBoxDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDivision.CustomReportFieldName = "";
			comboBoxDivision.CustomReportKey = "";
			comboBoxDivision.CustomReportValueType = 1;
			comboBoxDivision.DescriptionTextBox = null;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			appearance58.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDivision.DisplayLayout.Appearance = appearance58;
			comboBoxDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance59.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.GroupByBox.Appearance = appearance59;
			appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance60;
			comboBoxDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance61.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance61.BackColor2 = System.Drawing.SystemColors.Control;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance61;
			comboBoxDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance62.BackColor = System.Drawing.SystemColors.Window;
			appearance62.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDivision.DisplayLayout.Override.ActiveCellAppearance = appearance62;
			appearance63.BackColor = System.Drawing.SystemColors.Highlight;
			appearance63.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDivision.DisplayLayout.Override.ActiveRowAppearance = appearance63;
			comboBoxDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.CardAreaAppearance = appearance64;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			appearance65.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDivision.DisplayLayout.Override.CellAppearance = appearance65;
			comboBoxDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDivision.DisplayLayout.Override.CellPadding = 0;
			appearance66.BackColor = System.Drawing.SystemColors.Control;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDivision.DisplayLayout.Override.GroupByRowAppearance = appearance66;
			appearance67.TextHAlignAsString = "Left";
			comboBoxDivision.DisplayLayout.Override.HeaderAppearance = appearance67;
			comboBoxDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance68.BackColor = System.Drawing.SystemColors.Window;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			comboBoxDivision.DisplayLayout.Override.RowAppearance = appearance68;
			comboBoxDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance69.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance69;
			comboBoxDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDivision.Editable = true;
			comboBoxDivision.FilterString = "";
			comboBoxDivision.HasAllAccount = false;
			comboBoxDivision.HasCustom = false;
			comboBoxDivision.IsDataLoaded = false;
			comboBoxDivision.Location = new System.Drawing.Point(319, 26);
			comboBoxDivision.MaxDropDownItems = 12;
			comboBoxDivision.Name = "comboBoxDivision";
			comboBoxDivision.ShowInactiveItems = false;
			comboBoxDivision.ShowQuickAdd = true;
			comboBoxDivision.Size = new System.Drawing.Size(112, 21);
			comboBoxDivision.TabIndex = 5;
			comboBoxDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(740, 490);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxCurrency);
			base.Controls.Add(comboBoxCustomer);
			Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "CustomerOpeningBalanceBatchForm";
			Text = "Customer Opening Balance Entry";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDivision).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
