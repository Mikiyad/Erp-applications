using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class BankReconciliationForm : Form, IForm
	{
		private bool supressInventoryMessage;

		private GLData currentData;

		private const string TABLENAME_CONST = "Journal";

		private const string IDFIELD_CONST = "JournalID";

		private bool isNewRecord = true;

		private decimal bookValue;

		private decimal bankValue;

		private string seekID;

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

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private DataEntryGrid dataGridItems;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripButton toolStripButtonPreview;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private TextBox textBoxName;

		private BankAccountsComboBox comboBoxGridAccounts;

		private BankAccountsComboBox comboBoxCode;

		private MMLabel mmLabel1;

		private MMLabel mmLabel5;

		private XPButton btnReload;

		private CheckBox checBoxReconciled;

		private DateControl dateControl;

		private MMLabel mmLabel3;

		private MMLabel mmLabel2;

		private MMLabel mmLabel4;

		private AmountTextBox textBoxDifference;

		private AmountTextBox textBoxBankStatement;

		private AmountTextBox textBoxBookValue;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonFilter;

		private ToolStripButton toolStripButton1;

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

		private DateTime FromDate => dateControl.FromDate;

		private DateTime ToDate => dateControl.ToDate;

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
				}
				else
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton2 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton2.Enabled = enabled;
				}
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

		private string SeekID
		{
			get
			{
				return seekID;
			}
			set
			{
				seekID = value;
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

		public BankReconciliationForm()
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
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			base.FormClosing += ItemGroupDetailsForm_FormClosing;
			comboBoxCode.SelectedIndexChanged += comboBoxCode_SelectedIndexChanged;
		}

		private void checkBoxDate_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxCode_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadData(comboBoxCode.SelectedID);
			LoadAccountData(comboBoxCode.SelectedID);
		}

		public void LoadAccountData(string accountID)
		{
			try
			{
				if (!(accountID.Trim() == ""))
				{
					DataSet accountStatement = Factory.JournalSystem.GetAccountStatement(accountID, checBoxReconciled.Checked, FromDate, ToDate);
					if (accountStatement.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = accountStatement.Tables[0].Rows[0];
						if (!string.IsNullOrEmpty(dataRow["Value"].ToString()))
						{
							bookValue = Math.Round(decimal.Parse(dataRow["Value"].ToString()), Global.CurDecimalPoints);
						}
						else
						{
							bookValue = default(decimal);
						}
					}
					if (accountStatement.Tables[1].Rows.Count > 0)
					{
						DataRow dataRow = accountStatement.Tables[1].Rows[0];
						if (!string.IsNullOrEmpty(dataRow["Value"].ToString()))
						{
							bankValue = Math.Round(decimal.Parse(dataRow["Value"].ToString()), Global.CurDecimalPoints);
						}
						else
						{
							bankValue = default(decimal);
						}
					}
					CalculateBalance();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				ClearForm();
			}
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				decimal result = -1m;
				decimal.TryParse(dataGridItems.ActiveCell.Text.ToString(), out result);
				if (result < 0m)
				{
					ErrorHelper.InformationMessage("Negative quantity is not allowed.", "Please enter a number greater than or equal to zero.");
					e.Cancel = true;
				}
			}
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGridProductUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridItems.ActiveRow != null)
				{
					if (e.Cell.Column.Key == "ReconciliationDate" && e.Cell.IsActiveCell)
					{
						if (e.Cell.Value == null || string.IsNullOrEmpty(e.Cell.Value.ToString()))
						{
							e.Cell.Row.Cells["R"].Value = true;
						}
						else if (!bool.Parse(e.Cell.Row.Cells["R"].Value.ToString()))
						{
							e.Cell.Row.Cells["R"].Value = true;
						}
						CalculateBalance();
					}
					if (e.Cell.Column.Key == "R" || e.Cell.Column.Key == "ReconciliationDate")
					{
						CalculateBalance();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void CalculateBalance()
		{
			try
			{
				decimal d = bankValue;
				dataGridItems.DisplayLayout.Bands[0].Columns["R"].FilterClearButtonVisible = DefaultableBoolean.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["R"].FilterOperatorDefaultValue = FilterOperatorDefaultValue.Equals;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["R"].Text != null && bool.Parse(row.Cells["R"].Text))
					{
						DateTime result = DateTime.MaxValue;
						if (row.Cells["ReconciliationDate"].Value != null && row.Cells["ReconciliationDate"].Value.ToString() != "")
						{
							DateTime.TryParse(row.Cells["ReconciliationDate"].Value.ToString(), out result);
						}
						if (!(result > dateControl.ToDate))
						{
							decimal result2 = default(decimal);
							decimal result3 = default(decimal);
							if (row.Cells["Debit"].Value != null && row.Cells["Debit"].Value.ToString() != "")
							{
								decimal.TryParse(row.Cells["Debit"].Value.ToString(), out result2);
							}
							if (row.Cells["Credit"].Value != null && row.Cells["Credit"].Value.ToString() != "")
							{
								decimal.TryParse(row.Cells["Credit"].Value.ToString(), out result3);
							}
							d += result2 - result3;
						}
					}
				}
				decimal num = bookValue - d;
				textBoxBookValue.Text = bookValue.ToString(Format.TotalAmountFormat);
				textBoxBankStatement.Text = d.ToString(Format.TotalAmountFormat);
				textBoxDifference.Text = num.ToString(Format.TotalAmountFormat);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			if (e.Cell.Activated && e.Cell.Column.Key == "R" && e.Cell.IsActiveCell)
			{
				if (bool.Parse(e.Cell.Text.ToString()))
				{
					e.Cell.Row.Cells["ReconciliationDate"].Value = e.Cell.Row.Cells["TransactionDate"].Value;
				}
				else
				{
					e.Cell.Row.Cells["ReconciliationDate"].Value = DBNull.Value;
				}
				e.Cell.Row.Tag = 1;
				CalculateBalance();
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
			_ = dataGridItems.ActiveRow;
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (e.Cell.Column.Key == "ReconciliationDate")
			{
				if (e.Cell.Row.Cells["JournalDetailID"].Value == null || e.Cell.Row.Cells["JournalDetailID"].Value.ToString() == string.Empty)
				{
					e.Cancel = true;
					e.Cell.Activate();
					return;
				}
				if (e.Cell.Row.Cells["SysDocID"].Value == null || e.Cell.Row.Cells["SysDocID"].Value.ToString() == string.Empty)
				{
					e.Cancel = true;
					e.Cell.Activate();
					return;
				}
			}
			if (e.Cell.Column.Key == "R")
			{
				if (e.Cell.Row.Cells["JournalDetailID"].Value == null || e.Cell.Row.Cells["JournalDetailID"].Value.ToString() == string.Empty)
				{
					e.Cancel = true;
					e.Cell.Activate();
				}
				else if (e.Cell.Row.Cells["SysDocID"].Value == null || e.Cell.Row.Cells["SysDocID"].Value.ToString() == string.Empty)
				{
					e.Cancel = true;
					e.Cell.Activate();
				}
			}
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				currentData = new GLData();
				currentData.JournalDetailsTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (bool.Parse(row.Cells["R"].Value.ToString()) || checBoxReconciled.Checked)
					{
						bool flag = false;
						if (bool.Parse(row.Cells["R"].Value.ToString()))
						{
							flag = true;
						}
						if (row.Tag != null && !(row.Tag.ToString() != "1"))
						{
							DataRow dataRow = currentData.JournalDetailsTable.NewRow();
							dataRow.BeginEdit();
							dataRow["JournalDetailID"] = row.Cells["JournalDetailID"].Value.ToString();
							dataRow["JournalID"] = row.Cells["JournalID"].Value.ToString();
							dataRow["SysDocID"] = row.Cells["SysDocID"].Value.ToString();
							dataRow["VoucherID"] = row.Cells["VoucherID"].Value.ToString();
							dataRow["Description"] = row.Cells["Description"].Value.ToString();
							if (row.Cells["Debit"].Value != null && row.Cells["Debit"].Value.ToString() != "")
							{
								dataRow["Debit"] = row.Cells["Debit"].Value.ToString();
							}
							else
							{
								dataRow["Debit"] = DBNull.Value;
							}
							if (row.Cells["Credit"].Value != null && row.Cells["Credit"].Value.ToString() != "")
							{
								dataRow["Credit"] = row.Cells["Credit"].Value.ToString();
							}
							else
							{
								dataRow["Credit"] = DBNull.Value;
							}
							if (flag && row.Cells["ReconciliationDate"].Value.ToString() != "" && row.Cells["ReconciliationDate"].Value != null)
							{
								dataRow["ReconcileDate"] = DateTime.Parse(row.Cells["ReconciliationDate"].Value.ToString());
							}
							else
							{
								dataRow["ReconcileDate"] = DBNull.Value;
							}
							dataRow.EndEdit();
							currentData.JournalDetailsTable.Rows.Add(dataRow);
						}
					}
				}
				if (currentData.JournalDetailsTable.Rows.Count == 0)
				{
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
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("R", typeof(bool)).DefaultValue = false;
				dataTable.Columns.Add("JournalDetailID", typeof(long));
				dataTable.Columns.Add("JournalID", typeof(long));
				dataTable.Columns.Add("SysDocID");
				dataTable.Columns.Add("VoucherID");
				dataTable.Columns.Add("SysDocType", typeof(int));
				dataTable.Columns.Add("ChequeNumber");
				dataTable.Columns.Add("ChequeID", typeof(int));
				dataTable.Columns.Add("SendNumber");
				dataTable.Columns.Add("SendReference");
				dataTable.Columns.Add("SendDate");
				dataTable.Columns.Add("ChequeReceiptDate", typeof(DateTime));
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("TransactionDate", typeof(DateTime));
				dataTable.Columns.Add("ReconciliationDate", typeof(DateTime));
				dataTable.Columns.Add("ReconDateDefault", typeof(DateTime));
				dataTable.Columns.Add("Debit", typeof(decimal));
				dataTable.Columns.Add("Credit", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.AllowAddNew = false;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["ChequeReceiptDate"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["SendReference"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["SendNumber"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["SendDate"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].Hidden = true;
				bool flag4 = ultraGridColumn4.Hidden = flag2;
				bool flag6 = ultraGridColumn3.Hidden = flag4;
				bool hidden = ultraGridColumn2.Hidden = flag6;
				ultraGridColumn.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["R"].FilterClearButtonVisible = DefaultableBoolean.False;
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["R"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
				dataGridItems.DisplayLayout.Bands[0].Columns["R"].MaxWidth = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["R"].MinWidth = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["R"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["R"].CellAppearance.TextHAlign = HAlign.Center;
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["JournalDetailID"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["JournalID"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["ChequeID"];
				flag4 = (dataGridItems.DisplayLayout.Bands[0].Columns["ReconDateDefault"].Hidden = true);
				flag6 = (ultraGridColumn7.Hidden = flag4);
				hidden = (ultraGridColumn6.Hidden = flag6);
				ultraGridColumn5.Hidden = hidden;
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["JournalDetailID"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["JournalID"];
				UltraGridColumn ultraGridColumn10 = dataGridItems.DisplayLayout.Bands[0].Columns["ChequeID"];
				ExcludeFromColumnChooser excludeFromColumnChooser2 = dataGridItems.DisplayLayout.Bands[0].Columns["ReconDateDefault"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				ExcludeFromColumnChooser excludeFromColumnChooser4 = ultraGridColumn10.ExcludeFromColumnChooser = excludeFromColumnChooser2;
				ExcludeFromColumnChooser excludeFromColumnChooser7 = ultraGridColumn8.ExcludeFromColumnChooser = (ultraGridColumn9.ExcludeFromColumnChooser = excludeFromColumnChooser4);
				dataGridItems.DisplayLayout.Bands[0].Columns["ChequeReceiptDate"].Header.Caption = "Chq.Rct.Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["ChequeReceiptDate"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["ChequeReceiptDate"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["ChequeReceiptDate"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["ChequeReceiptDate"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["ChequeReceiptDate"].Width = 140;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendReference"].Header.Caption = "Send Ref";
				dataGridItems.DisplayLayout.Bands[0].Columns["SendReference"].Width = 80;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendReference"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendReference"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendReference"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendNumber"].Header.Caption = "Send Number";
				dataGridItems.DisplayLayout.Bands[0].Columns["SendNumber"].Width = 80;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendNumber"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendNumber"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendNumber"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].Header.Caption = "Doc ID";
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].Width = 80;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["VoucherID"].Header.Caption = "Voucher ID";
				dataGridItems.DisplayLayout.Bands[0].Columns["VoucherID"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["VoucherID"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["VoucherID"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["VoucherID"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["TransactionDate"].Header.Caption = "Transaction Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["TransactionDate"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["TransactionDate"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TransactionDate"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["TransactionDate"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["TransactionDate"].Width = 140;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendDate"].Header.Caption = "Send Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["SendDate"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendDate"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendDate"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendDate"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendDate"].Width = 140;
				dataGridItems.DisplayLayout.Bands[0].Columns["SendDate"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
				dataGridItems.DisplayLayout.Bands[0].Columns["ReconciliationDate"].Header.Caption = "Statement Date";
				dataGridItems.DisplayLayout.Bands[0].Columns["ReconciliationDate"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["ReconciliationDate"].Width = 150;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Debit"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Debit"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Debit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Debit"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Debit"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Debit"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocType"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocType"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocType"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocType"].Header.Caption = "Doc Type";
				dataGridItems.DisplayLayout.Bands[0].Columns["ChequeNumber"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["ChequeNumber"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["ChequeNumber"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["ChequeNumber"].Header.Caption = "Cheque#";
				dataGridItems.DisplayLayout.Bands[0].Columns["Credit"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Credit"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Credit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Credit"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Credit"].Width = 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Credit"].Format = Format.GridAmountFormat;
				ValueList sysDocTypesValueList = UILib.GetSysDocTypesValueList();
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocType"].ValueList = sysDocTypesValueList;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"].Width = checked(15 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["VoucherID"].Width = checked(15 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalDebit", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Debit"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalDebit"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalDebit"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalDebit"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalDebit"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalDebit"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalCredit", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Credit"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalCredit"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalCredit"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalCredit"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalCredit"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalCredit"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.False;
				dataGridItems.DisplayLayout.Bands[0].Override.FilterUIType = FilterUIType.FilterRow;
				dataGridItems.ShowMoveRowsMenu = false;
				dataGridItems.ShowDeleteMenu = false;
				dataGridItems.ShowInsertMenu = false;
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

		public void LoadData(string accountID)
		{
			try
			{
				if (!(accountID.Trim() == ""))
				{
					currentData = Factory.JournalSystem.GetBankReconciliationList(accountID, FromDate, ToDate, checBoxReconciled.Checked);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[1].Rows.Count == 0)
					{
						dataGridItems.Clear();
						IsNewRecord = true;
						comboBoxCode.Focus();
					}
					else
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
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[1].Rows.Count != 0 && currentData.Tables.Contains("Journal_Details") && currentData.JournalDetailsTable.Rows.Count != 0)
				{
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					dataGridItems.BeginUpdate();
					foreach (DataRow row in currentData.Tables["Journal_Details"].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["JournalDetailID"] = row["JournalDetailID"];
						dataRow2["JournalID"] = row["JournalID"];
						dataRow2["SysDocID"] = row["SysDocID"];
						dataRow2["VoucherID"] = row["VoucherID"];
						dataRow2["SysDocType"] = row["SysDocType"];
						dataRow2["ChequeNumber"] = row["CheckNumber"];
						dataRow2["ChequeID"] = row["CheckID"];
						dataRow2["SendNumber"] = row["SendNumber"];
						dataRow2["SendReference"] = row["SendReference"];
						dataRow2["SendDate"] = row["SendDate"];
						dataRow2["ChequeReceiptDate"] = row["ChequeReceiptDate"];
						dataRow2["Description"] = row["Description"];
						dataRow2["TransactionDate"] = row["JournalDate"];
						decimal result = default(decimal);
						decimal result2 = default(decimal);
						decimal.TryParse(row["Debit"].ToString(), out result);
						decimal.TryParse(row["Credit"].ToString(), out result2);
						dataRow2["Debit"] = result.ToString(Format.GridAmountFormat);
						dataRow2["Credit"] = result2.ToString(Format.GridAmountFormat);
						object obj3 = dataRow2["ReconciliationDate"] = (dataRow2["ReconDateDefault"] = row["ReconcileDate"]);
						dataRow2["R"] = ((row["ReconcileDate"].ToString() != "") ? true : false);
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
					dataTable.AcceptChanges();
					dataGridItems.EndUpdate();
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
			bool flag = false;
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
			bool flag2 = false;
			try
			{
				flag = Factory.JournalSystem.UpdateJournalBankReconciliation(currentData);
				if (flag)
				{
					flag2 = true;
				}
				if (flag)
				{
					if (!clearAfter)
					{
						formManager.ResetDirty();
						return flag;
					}
					ClearForm();
					IsNewRecord = true;
					return flag;
				}
				ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
			finally
			{
				if (flag2)
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
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one item row.");
				return false;
			}
			return true;
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.GetCellValue("Quantity") != null && row.GetCellValue("Quantity").ToString() != "")
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
				dateControl.SelectedPeriod = DatePeriods.AllDates;
				checBoxReconciled.Checked = false;
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				comboBoxCode.Clear();
				comboBoxCode.Focus();
				textBoxBankStatement.Clear();
				textBoxBookValue.Clear();
				textBoxDifference.Clear();
				bankValue = default(decimal);
				bookValue = default(decimal);
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
				ErrorHelper.InformationMessage("Journal entries cannot be deleted. Use General Journal Entry screen to delete the transactions.");
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
			SeekID = DatabaseHelper.GetNextID("Account", "AccountID", comboBoxCode.SelectedID, "SubType", 3.ToString());
			if (SeekID != "")
			{
				comboBoxCode.SelectedID = SeekID;
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			SeekID = DatabaseHelper.GetPreviousID("Account", "AccountID", comboBoxCode.SelectedID, "SubType", 3.ToString());
			if (SeekID != "")
			{
				comboBoxCode.SelectedID = SeekID;
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			SeekID = DatabaseHelper.GetLastID("Account", "AccountID");
			if (SeekID != "")
			{
				comboBoxCode.SelectedID = SeekID;
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			SeekID = DatabaseHelper.GetFirstID("Account", "AccountID");
			if (SeekID != "")
			{
				comboBoxCode.SelectedID = SeekID;
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Journal", "JournalID", "", toolStripTextBoxFind.Text);
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
			dataGridItems.SaveLayout();
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
				dataGridItems.ApplyFormat();
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
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
			if (Void(!IsVoid))
			{
				IsVoid = !IsVoid;
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
				return Factory.JournalSystem.VoidJournalVoucher("", "", isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.GJournal);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 1.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.GJournal);
					currentData = (dataSet as GLData);
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
					string selectedID = comboBoxCode.SelectedID;
					if (selectedID == "")
					{
						ErrorHelper.InformationMessage("Please select a document to print.");
					}
					else
					{
						DataSet bankReconciliationToPrint = Factory.JournalSystem.GetBankReconciliationToPrint(selectedID, FromDate, ToDate, checBoxReconciled.Checked);
						if (bankReconciliationToPrint == null || bankReconciliationToPrint.Tables.Count == 0)
						{
							ErrorHelper.ErrorMessage("Cannot print the document.", "document not found.");
						}
						else
						{
							PrintHelper.PrintDocument(bankReconciliationToPrint, "", "Bank Reconciliation", SysDocTypes.GJournal, isPrint, showPrintDialog);
						}
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
			return "Bank Reconciliation";
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

		private void btnReload_Click(object sender, EventArgs e)
		{
			LoadData(comboBoxCode.SelectedID);
			LoadAccountData(comboBoxCode.SelectedID);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (toolStripButtonFilter.Checked)
			{
				dataGridItems.DisplayLayout.Bands[0].Override.AllowRowFiltering = DefaultableBoolean.True;
				return;
			}
			dataGridItems.DisplayLayout.Bands[0].Override.AllowRowFiltering = DefaultableBoolean.False;
			dataGridItems.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
		}

		private void toolStripButton1_Click_1(object sender, EventArgs e)
		{
			dataGridItems.ShowColumnChooser();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.BankReconciliationForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonFilter = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			panelDetails = new System.Windows.Forms.Panel();
			textBoxDifference = new Micromind.UISupport.AmountTextBox();
			textBoxBankStatement = new Micromind.UISupport.AmountTextBox();
			textBoxBookValue = new Micromind.UISupport.AmountTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateControl = new Micromind.DataControls.DateControl();
			checBoxReconciled = new System.Windows.Forms.CheckBox();
			btnReload = new Micromind.UISupport.XPButton();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			comboBoxCode = new Micromind.DataControls.BankAccountsComboBox();
			textBoxName = new System.Windows.Forms.TextBox();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxGridAccounts = new Micromind.DataControls.BankAccountsComboBox();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCode).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridAccounts).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[13]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButtonFilter,
				toolStripButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(753, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Visible = false;
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Visible = false;
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Visible = false;
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last Record";
			toolStripButtonLast.Visible = false;
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator1.Visible = false;
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.Visible = false;
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Visible = false;
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator2.Visible = false;
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
			toolStripButtonFilter.CheckOnClick = true;
			toolStripButtonFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFilter.Image = Micromind.ClientUI.Properties.Resources.filter;
			toolStripButtonFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFilter.Name = "toolStripButtonFilter";
			toolStripButtonFilter.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFilter.Text = "Filter";
			toolStripButtonFilter.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.column;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(83, 28);
			toolStripButton1.Text = "Columns";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click_1);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 544);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(753, 40);
			panelButtons.TabIndex = 9;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(42, 24);
			buttonVoid.TabIndex = 9;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Visible = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 8;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Visible = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 7;
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
			linePanelDown.Size = new System.Drawing.Size(753, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(643, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 10;
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
			buttonSave.TabIndex = 6;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraCalcManager1.ContainingControl = this;
			panelDetails.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panelDetails.Controls.Add(textBoxDifference);
			panelDetails.Controls.Add(textBoxBankStatement);
			panelDetails.Controls.Add(textBoxBookValue);
			panelDetails.Controls.Add(mmLabel4);
			panelDetails.Controls.Add(mmLabel3);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(dateControl);
			panelDetails.Controls.Add(checBoxReconciled);
			panelDetails.Controls.Add(btnReload);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(mmLabel5);
			panelDetails.Controls.Add(comboBoxCode);
			panelDetails.Controls.Add(textBoxName);
			panelDetails.Location = new System.Drawing.Point(9, 30);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(729, 164);
			panelDetails.TabIndex = 0;
			textBoxDifference.AllowDecimal = true;
			textBoxDifference.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDifference.CustomReportFieldName = "";
			textBoxDifference.CustomReportKey = "";
			textBoxDifference.CustomReportValueType = 1;
			textBoxDifference.IsComboTextBox = false;
			textBoxDifference.IsModified = false;
			textBoxDifference.Location = new System.Drawing.Point(472, 118);
			textBoxDifference.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDifference.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDifference.Name = "textBoxDifference";
			textBoxDifference.NullText = "0";
			textBoxDifference.ReadOnly = true;
			textBoxDifference.Size = new System.Drawing.Size(114, 20);
			textBoxDifference.TabIndex = 6;
			textBoxDifference.TabStop = false;
			textBoxDifference.Text = "0.00";
			textBoxDifference.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDifference.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxBankStatement.AllowDecimal = true;
			textBoxBankStatement.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankStatement.CustomReportFieldName = "";
			textBoxBankStatement.CustomReportKey = "";
			textBoxBankStatement.CustomReportValueType = 1;
			textBoxBankStatement.IsComboTextBox = false;
			textBoxBankStatement.IsModified = false;
			textBoxBankStatement.Location = new System.Drawing.Point(472, 95);
			textBoxBankStatement.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxBankStatement.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxBankStatement.Name = "textBoxBankStatement";
			textBoxBankStatement.NullText = "0";
			textBoxBankStatement.ReadOnly = true;
			textBoxBankStatement.Size = new System.Drawing.Size(114, 20);
			textBoxBankStatement.TabIndex = 5;
			textBoxBankStatement.TabStop = false;
			textBoxBankStatement.Text = "0.00";
			textBoxBankStatement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBankStatement.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxBookValue.AllowDecimal = true;
			textBoxBookValue.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBookValue.CustomReportFieldName = "";
			textBoxBookValue.CustomReportKey = "";
			textBoxBookValue.CustomReportValueType = 1;
			textBoxBookValue.IsComboTextBox = false;
			textBoxBookValue.IsModified = false;
			textBoxBookValue.Location = new System.Drawing.Point(472, 74);
			textBoxBookValue.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxBookValue.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxBookValue.Name = "textBoxBookValue";
			textBoxBookValue.NullText = "0";
			textBoxBookValue.ReadOnly = true;
			textBoxBookValue.Size = new System.Drawing.Size(114, 20);
			textBoxBookValue.TabIndex = 4;
			textBoxBookValue.TabStop = false;
			textBoxBookValue.Text = "0.00";
			textBoxBookValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBookValue.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(376, 118);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(59, 13);
			mmLabel4.TabIndex = 167;
			mmLabel4.Text = "Difference:";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(376, 98);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(86, 13);
			mmLabel3.TabIndex = 163;
			mmLabel3.Text = "Bank Statement:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(376, 77);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(65, 13);
			mmLabel2.TabIndex = 162;
			mmLabel2.Text = "Book Value:";
			dateControl.CustomReportFieldName = "";
			dateControl.CustomReportKey = "";
			dateControl.CustomReportValueType = 1;
			dateControl.FromDate = new System.DateTime(2017, 12, 1, 0, 0, 0, 0);
			dateControl.Location = new System.Drawing.Point(16, 64);
			dateControl.Name = "dateControl";
			dateControl.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl.Size = new System.Drawing.Size(345, 80);
			dateControl.TabIndex = 3;
			dateControl.ToDate = new System.DateTime(2017, 12, 25, 23, 59, 59, 59);
			checBoxReconciled.AutoSize = true;
			checBoxReconciled.Location = new System.Drawing.Point(252, 18);
			checBoxReconciled.Name = "checBoxReconciled";
			checBoxReconciled.Size = new System.Drawing.Size(118, 17);
			checBoxReconciled.TabIndex = 1;
			checBoxReconciled.Text = "Include Reconciled";
			checBoxReconciled.UseVisualStyleBackColor = true;
			btnReload.AdjustImageLocation = new System.Drawing.Point(0, 0);
			btnReload.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			btnReload.BackColor = System.Drawing.Color.Silver;
			btnReload.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			btnReload.BtnStyle = Micromind.UISupport.XPStyle.Default;
			btnReload.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnReload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			btnReload.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			btnReload.Location = new System.Drawing.Point(624, 137);
			btnReload.Name = "btnReload";
			btnReload.Size = new System.Drawing.Size(102, 24);
			btnReload.TabIndex = 7;
			btnReload.Text = "&Reload";
			btnReload.UseVisualStyleBackColor = false;
			btnReload.Click += new System.EventHandler(btnReload_Click);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(14, 41);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(81, 13);
			mmLabel1.TabIndex = 160;
			mmLabel1.Text = "Account Name:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(14, 17);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(78, 13);
			mmLabel5.TabIndex = 159;
			mmLabel5.Text = "Account Code:";
			comboBoxCode.Assigned = false;
			comboBoxCode.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCode.CalcManager = ultraCalcManager1;
			comboBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCode.CustomReportFieldName = "";
			comboBoxCode.CustomReportKey = "";
			comboBoxCode.CustomReportValueType = 1;
			comboBoxCode.DescriptionTextBox = textBoxName;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCode.DisplayLayout.Appearance = appearance;
			comboBoxCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCode.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCode.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCode.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCode.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCode.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCode.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCode.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCode.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxCode.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxCode.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCode.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCode.Editable = true;
			comboBoxCode.FilterString = "";
			comboBoxCode.HasAllAccount = false;
			comboBoxCode.HasCustom = false;
			comboBoxCode.IsDataLoaded = false;
			comboBoxCode.Location = new System.Drawing.Point(111, 15);
			comboBoxCode.MaxDropDownItems = 12;
			comboBoxCode.Name = "comboBoxCode";
			comboBoxCode.ShowInactiveItems = false;
			comboBoxCode.ShowQuickAdd = true;
			comboBoxCode.Size = new System.Drawing.Size(129, 20);
			comboBoxCode.TabIndex = 0;
			comboBoxCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxName.Location = new System.Drawing.Point(111, 38);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.ReadOnly = true;
			textBoxName.Size = new System.Drawing.Size(407, 20);
			textBoxName.TabIndex = 2;
			textBoxName.TabStop = false;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 454);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(722, 81);
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
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance13;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(11, 200);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(728, 338);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			comboBoxGridAccounts.Assigned = false;
			comboBoxGridAccounts.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridAccounts.CalcManager = ultraCalcManager1;
			comboBoxGridAccounts.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridAccounts.CustomReportFieldName = "";
			comboBoxGridAccounts.CustomReportKey = "";
			comboBoxGridAccounts.CustomReportValueType = 1;
			comboBoxGridAccounts.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridAccounts.DisplayLayout.Appearance = appearance25;
			comboBoxGridAccounts.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridAccounts.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccounts.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridAccounts.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxGridAccounts.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridAccounts.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxGridAccounts.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridAccounts.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridAccounts.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridAccounts.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxGridAccounts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridAccounts.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccounts.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridAccounts.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxGridAccounts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridAccounts.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccounts.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxGridAccounts.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxGridAccounts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridAccounts.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridAccounts.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxGridAccounts.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridAccounts.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxGridAccounts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridAccounts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridAccounts.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridAccounts.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridAccounts.Editable = true;
			comboBoxGridAccounts.FilterString = "";
			comboBoxGridAccounts.HasAllAccount = false;
			comboBoxGridAccounts.HasCustom = false;
			comboBoxGridAccounts.IsDataLoaded = false;
			comboBoxGridAccounts.Location = new System.Drawing.Point(616, 183);
			comboBoxGridAccounts.MaxDropDownItems = 12;
			comboBoxGridAccounts.Name = "comboBoxGridAccounts";
			comboBoxGridAccounts.ShowInactiveItems = false;
			comboBoxGridAccounts.ShowQuickAdd = true;
			comboBoxGridAccounts.Size = new System.Drawing.Size(100, 20);
			comboBoxGridAccounts.TabIndex = 127;
			comboBoxGridAccounts.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridAccounts.Visible = false;
			ultraGridPrintDocument1.DocumentName = "Inventory Adjustment - List";
			ultraGridPrintDocument1.Grid = dataGridItems;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(753, 584);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxGridAccounts);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(760, 585);
			base.Name = "BankReconciliationForm";
			Text = "Bank Reconciliation";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCode).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridAccounts).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
