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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class SalaryDeductionForm : Form, IForm
	{
		private bool supressExpenseMessage;

		private SalaryData currentData;

		private const string TABLENAME_CONST = "Salary_Deduction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private int DeductionPercent;

		private CalculateSalaryForm calcForm = new CalculateSalaryForm();

		private EmployeeSelector2 empSelectorObj;

		private Form empFormObj;

		private ScreenAccessRight screenRight;

		private bool isVoid;

		private bool IsChangedallowed = true;

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

		private MMLabel LabelPeriod;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator3;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private PayrollItemComboBox comboBoxPayrolItem;

		private Panel panelApproval;

		private CheckBox checkBoxApprove;

		private EmployeeComboBox comboBoxGridEmployee;

		private Panel panelApprovalDetail;

		private Label label2;

		private DateTimePicker dateTimeApprovalDate;

		private Label label1;

		private TextBox textBoxApprovedBy;

		private TextBox textBoxCreatedBy;

		private Label label5;

		private DateTimePicker dateTimeCreatedDate;

		private Label label4;

		private PayrollItemComboBox comboBoxDeduction;

		private UltraFormattedLinkLabel linkLabelVoucherNumber;

		private Button buttonFillDetails;

		private NumericUpDown Numericnoofpayments;

		private NumericUpDown NumericNoofmonths;

		private MMLabel LabelNoofpayment;

		private MMLabel LabelNoofmonth;

		private CheckBox checkBoxMultiPeriod;

		private EmployeeComboBox comboBoxEmployee;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private UltraFormattedLinkLabel LabelEmployee;

		private ToolStripButton toolStripButtonExcelImport;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem loadDeductionAmountToolStripMenuItem;

		private CheckBox checkBoxDeductionPercent;

		private MMLabel mmLabel1;

		private PercentTextBox textBoxDeductionPercent;

		private MMLabel labelDeductionPercent;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripMenuItem loadEmployeesToolStripMenuItem;

		public ScreenAreas ScreenArea => ScreenAreas.Project;

		public int ScreenID => 4002;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => string.Empty;

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
					checkBoxDeductionPercent.Visible = true;
					panelApproval.Visible = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = true;
					panelApproval.Visible = true;
					checkBoxDeductionPercent.Visible = false;
					labelDeductionPercent.Visible = false;
					textBoxDeductionPercent.Visible = false;
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

		public SalaryDeductionForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			comboBoxDeduction.IsDeduction = true;
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
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			base.FormClosing += ItemGroupDetailsForm_FormClosing;
			dataGridItems.BeforeRowActivate += dataGridItems_BeforeRowActivate;
			checkBoxApprove.CheckedChanged += checkBoxApprove_CheckedChanged;
		}

		private void checkBoxApprove_CheckedChanged(object sender, EventArgs e)
		{
			panelApprovalDetail.Visible = checkBoxApprove.Checked;
		}

		private void dataGridItems_BeforeRowActivate(object sender, RowEventArgs e)
		{
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGridExpenseCode_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			checked
			{
				try
				{
					if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow != null)
					{
						string key = e.Cell.Column.Key;
						decimal result = default(decimal);
						decimal result2 = default(decimal);
						decimal result3 = default(decimal);
						if (key == "EmpNo" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "EmpNo")
						{
							dataGridItems.ActiveRow.Cells["EmpName"].Value = comboBoxGridEmployee.SelectedName;
						}
						if (key == "Deduction Code")
						{
							for (int i = 1; i < dataGridItems.Rows.Count; i++)
							{
								_ = dataGridItems.Rows[i];
								if ((dataGridItems.Rows[i].Cells["Deduction Code"].Value == null || dataGridItems.Rows[i].Cells["Deduction Code"].Value.ToString() == "") && dataGridItems.Rows[i].Index > 0)
								{
									dataGridItems.Rows[i].Cells["Deduction Code"].Value = dataGridItems.Rows[i - 1].Cells["Deduction Code"].Value;
								}
							}
						}
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result3);
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Rate"].Value.ToString(), out result);
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result2);
						if (key == "Quantity" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Quantity")
						{
							result2 = Math.Round(result3 * result, Global.CurDecimalPoints);
							dataGridItems.ActiveRow.Cells["Amount"].Value = result2;
						}
						else if (key == "Rate" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Rate")
						{
							result2 = Math.Round(result3 * result, Global.CurDecimalPoints);
							dataGridItems.ActiveRow.Cells["Amount"].Value = result2;
						}
						else if (key == "Amount" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Amount")
						{
							if (result3 == 0m)
							{
								result3 = 1m;
								dataGridItems.ActiveRow.Cells["Quantity"].Value = result3;
							}
							result = Math.Round(result2 / result3, 5);
							dataGridItems.ActiveRow.Cells["Rate"].Value = Math.Abs(result);
						}
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void dataGridItems_KeyDown(object sender, KeyPressEventArgs e)
		{
			if (dataGridItems.ActiveCell == null)
			{
				return;
			}
			UltraGridColumn column = dataGridItems.ActiveCell.Column;
			if (dataGridItems.ActiveRow != null && column != null)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Rate"].Value.ToString(), out result2);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
				if (column.Key == "Amount" && result > 0m)
				{
					result2 = Math.Round(result3 / result, 5);
					dataGridItems.ActiveRow.Cells["Rate"].Value = result2;
					dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				}
				else
				{
					result3 = Math.Round(result * result2, 5);
					dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
					dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				}
			}
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridItems.ActiveRow != null && ultraGridColumn != null)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Rate"].Value.ToString(), out result2);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
				result3 = Math.Round(result * result2, Global.CurDecimalPoints);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
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
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
		}

		private void comboBoxGridExpenseCode_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				if (dataGridItems.ActiveCell.Column.Key.ToString() == "Rate")
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
			if (dataGridItems.ActiveRow != null && e.Cell.Column.Key == "To")
			{
				DateTime dateTime = default(DateTime);
				DateTime result = dateTime = DateTime.Now;
				DateTime.TryParse(dataGridItems.ActiveCell.Row.Cells["From"].Value.ToString(), out result);
				DateTime.TryParse(e.NewValue.ToString(), out dateTime);
				if (dateTime < result)
				{
					ErrorHelper.InformationMessage("To-Time can not be less than From-Time");
					e.Cancel = true;
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
				if (currentData == null || isNewRecord)
				{
					currentData = new SalaryData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.SalaryDeductionTable.Rows[0] : currentData.SalaryDeductionTable.NewRow();
				if (checkBoxApprove.Checked)
				{
					dataRow["ApprovalDate"] = DateTime.Now;
					dataRow["ApprovedBy"] = Global.CurrentUser;
				}
				else
				{
					dataRow["ApprovalDate"] = DBNull.Value;
					dataRow["ApprovedBy"] = DBNull.Value;
				}
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["Note"] = textBoxNote.Text.Trim();
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.SalaryDeductionTable.Rows.Add(dataRow);
				}
				currentData.SalaryDeductionDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.SalaryDeductionDetailTable.NewRow();
					dataRow2.BeginEdit();
					if (!checkBoxMultiPeriod.Checked)
					{
						dataRow2["EmployeeID"] = row.Cells["EmpNo"].Value.ToString();
						dataRow2["EmployeeName"] = row.Cells["EmpName"].Value.ToString();
					}
					else
					{
						dataRow2["EmployeeID"] = comboBoxEmployee.SelectedID;
						dataRow2["EmployeeName"] = comboBoxEmployee.SelectedName;
					}
					if (checkBoxMultiPeriod.Checked)
					{
						if (row.Cells["Payroll Period"].Value.ToString() != "")
						{
							dataRow2["PayrollPeriod"] = DateTime.Parse(row.Cells["Payroll Period"].Value.ToString());
						}
						else
						{
							dataRow2["PayrollPeriod"] = DBNull.Value;
						}
					}
					else
					{
						dataRow2["PayrollPeriod"] = dateTimePickerDate.Value;
					}
					dataRow2["DeductionCode"] = row.Cells["Deduction Code"].Value.ToString();
					dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
					if (row.Cells["Amount"].Value.ToString() != "")
					{
						dataRow2["Amount"] = decimal.Parse(row.Cells["Amount"].Value.ToString());
					}
					else
					{
						dataRow2["Amount"] = DBNull.Value;
					}
					dataRow2["RowIndex"] = row.Index;
					if (row.Cells["Quantity"].Value.ToString() != "")
					{
						dataRow2["Quantity"] = decimal.Parse(row.Cells["Quantity"].Value.ToString());
					}
					else
					{
						dataRow2["Quantity"] = 1;
					}
					if (row.Cells["Rate"].Value.ToString() != "")
					{
						dataRow2["Rate"] = decimal.Parse(row.Cells["Rate"].Value.ToString());
					}
					else
					{
						dataRow2["Rate"] = DBNull.Value;
					}
					dataRow2.EndEdit();
					currentData.SalaryDeductionDetailTable.Rows.Add(dataRow2);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables["Salary_Deduction"].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Salary_Deduction"].Rows[0];
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					checkBoxApprove.Checked = false;
					if (dataRow["ApprovalDate"] != null && dataRow["ApprovalDate"].ToString() != string.Empty && dataRow["ApprovedBy"] != null && dataRow["ApprovedBy"].ToString() != string.Empty)
					{
						checkBoxApprove.Checked = true;
					}
					else
					{
						checkBoxApprove.Checked = false;
					}
					if (dataRow["ApprovalDate"] != DBNull.Value)
					{
						dateTimeApprovalDate.Value = DateTime.Parse(dataRow["ApprovalDate"].ToString());
						dateTimeCreatedDate.Value = DateTime.Parse(dataRow["DateCreated"].ToString());
					}
					textBoxApprovedBy.Text = dataRow["ApprovedBy"].ToString();
					textBoxCreatedBy.Text = dataRow["CreatedBy"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					if (currentData.Tables.Contains("Salary_Deduction_Detail") && currentData.SalaryDeductionDetailTable.Rows.Count != 0)
					{
						if (currentData.SalaryDeductionDetailTable.DefaultView.ToTable(true, "EmployeeID").Rows.Count == 1 && currentData.SalaryDeductionDetailTable.Rows.Count > 1)
						{
							checkBoxMultiPeriod.Checked = true;
						}
						else
						{
							checkBoxMultiPeriod.Checked = false;
						}
						DataTable dataTable = dataGridItems.DataSource as DataTable;
						dataTable.Rows.Clear();
						dataGridItems.BeginUpdate();
						foreach (DataRow row in currentData.Tables["Salary_Deduction_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							if (!checkBoxMultiPeriod.Checked)
							{
								dataRow3["EmpNo"] = row["EmployeeID"].ToString();
								dataRow3["EmpName"] = row["EmployeeName"].ToString();
							}
							else
							{
								comboBoxEmployee.SelectedID = row["EmployeeID"].ToString();
							}
							if (checkBoxMultiPeriod.Checked)
							{
								if (row["PayrollPeriod"] != DBNull.Value)
								{
									dataRow3["Payroll Period"] = DateTime.Parse(row["PayrollPeriod"].ToString());
								}
								else
								{
									dataRow3["Payroll Period"] = DBNull.Value;
								}
							}
							dataRow3["Deduction Code"] = row["DeductionCode"].ToString();
							dataRow3["Remarks"] = row["Remarks"].ToString();
							dataRow3["Remarks"] = row["Remarks"].ToString();
							if (row["Amount"] != DBNull.Value)
							{
								dataRow3["Amount"] = decimal.Parse(row["Amount"].ToString());
							}
							else
							{
								dataRow3["Amount"] = DBNull.Value;
							}
							if (row["Quantity"] != DBNull.Value)
							{
								dataRow3["Quantity"] = row["Quantity"].ToString();
							}
							else
							{
								dataRow3["Quantity"] = 1;
							}
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal.TryParse(dataRow3["Quantity"].ToString(), out result);
							decimal.TryParse(dataRow3["Amount"].ToString(), out result2);
							if (row["Rate"] != DBNull.Value)
							{
								dataRow3["Rate"] = decimal.Parse(row["Rate"].ToString());
							}
							else
							{
								dataRow3["Rate"] = Math.Abs(Math.Round(result2 / result, 5));
							}
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

		private void SetupGrid(bool status)
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				dataGridItems.DisplayLayout.Bands[0].Summaries.Clear();
				DataTable dataTable = new DataTable();
				if (!status)
				{
					dataTable.Columns.Add("EmpNo");
					dataTable.Columns.Add("EmpName");
				}
				else
				{
					dataTable.Columns.Add("Payroll Period", typeof(DateTime));
				}
				dataTable.Columns.Add("Deduction Code");
				dataTable.Columns.Add("Remarks");
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Rate", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.LoadLayout();
				if (!status)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].CharacterCasing = CharacterCasing.Upper;
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].MaxLength = 15;
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].ValueList = comboBoxGridEmployee;
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].Header.Caption = "Employee No";
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].CellActivation = Activation.Disabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].CellAppearance.ForeColorDisabled = Color.Black;
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].MaxLength = 255;
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].Header.Caption = "Employee Name";
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Deduction Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Deduction Code"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Deduction Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Deduction Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Deduction Code"].ValueList = comboBoxDeduction;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Rate"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Rate"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["Rate"].MinValue = 0;
				if (!status)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].Width = checked(20 * dataGridItems.Width) / 100;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Amount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				if (!status)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
					dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Deduction Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["Rate"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
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

		public void EditDocument(string sysDocID, string voucherID)
		{
			LoadData(voucherID);
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.SalarySystem.GetSalaryDeductionByID(voucherID);
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

		private bool SaveData()
		{
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
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
				bool flag = Factory.SalarySystem.CreateSalaryDeduction(currentData, !isNewRecord);
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
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (textBoxVoucherNumber.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one item row.");
				return false;
			}
			for (int i = 0; i < dataGridItems.Rows.Count; i = checked(i + 1))
			{
				_ = dataGridItems.Rows[i];
				if (dataGridItems.Rows[i].Cells["Deduction Code"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select deduction code for all the entries.");
					dataGridItems.Rows[i].Activate();
					return false;
				}
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
				textBoxNote.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				dateTimePickerDate.Value = DateTime.Now;
				checkBoxDeductionPercent.Checked = false;
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
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.SalarySystem.DeleteSalaryDeduction(textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Salary_Deduction", "VoucherID", textBoxVoucherNumber.Text);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Salary_Deduction", "VoucherID", textBoxVoucherNumber.Text);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Salary_Deduction", "VoucherID");
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Salary_Deduction", "VoucherID");
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Salary_Deduction", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			duplicateToolStripMenuItem.Enabled = !IsNewRecord;
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

		private void JournalLeavesForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				SetupGrid(status: false);
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
			if (dataGridItems.ActiveRow != null)
			{
				_ = dataGridItems.ActiveCell;
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
				return Factory.SystemDocumentSystem.GetNextDocumentNumber("Salary_Deduction", "VoucherID");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
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
				return Factory.JournalSystem.VoidJournalVoucher(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.JobTimesheet);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 76.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.JobTimesheet);
					currentData = (dataSet as SalaryData);
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
					string text = textBoxVoucherNumber.Text;
					DataSet salaryDeductionToPrint = Factory.SalarySystem.GetSalaryDeductionToPrint(text);
					if (salaryDeductionToPrint == null || salaryDeductionToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(salaryDeductionToPrint, "", "Salary Deduction", SysDocTypes.None, isPrint, showPrintDialog);
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
			return "Salary Deduction";
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

		private void checkBoxDatewise_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void employeeLinkLabel_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void linkLabelVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
		}

		private void CalculateMultiperiod()
		{
			dataGridItems.DisplayLayout.Bands[0].AddNew().Cells[0].Value = dateTimePickerDate.Value;
			UltraGridRow ultraGridRow = dataGridItems.Rows[0];
			if (comboBoxEmployee.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select employee.");
				return;
			}
			if (NumericNoofmonths.Value == 0m)
			{
				ErrorHelper.InformationMessage("Please select no of months.");
				return;
			}
			if (Numericnoofpayments.Value == 0m)
			{
				ErrorHelper.InformationMessage("Please select no of payments.");
				return;
			}
			DateTime value = dateTimePickerDate.Value;
			int month = value.Month;
			int year = value.Year;
			if (month == -1)
			{
				ErrorHelper.InformationMessage("Please select month.");
				return;
			}
			DateTime today = DateTime.Today;
			string text = "";
			string text2 = "";
			int result = 0;
			today = DateTime.Parse(ultraGridRow.Cells["Payroll Period"].Value.ToString());
			text = ultraGridRow.Cells["Deduction Code"].Value.ToString();
			text2 = ultraGridRow.Cells["Remarks"].Value.ToString();
			result = (int.TryParse(ultraGridRow.Cells["Amount"].Value.ToString(), out result) ? int.Parse(ultraGridRow.Cells["Amount"].Value.ToString()) : 0);
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (month != -1 && year != -1)
			{
				DateTime.DaysInMonth(year, month);
				int months = (int)NumericNoofmonths.Value;
				for (int i = 1; (decimal)i <= Numericnoofpayments.Value; i = checked(i + 1))
				{
					DataRow dataRow = dataTable.NewRow();
					new DateTime(year, month, i).ToString("dddd");
					dataRow["Payroll Period"] = today;
					dataRow["Deduction Code"] = text;
					dataRow["Remarks"] = text2;
					dataRow["Amount"] = result;
					today = today.AddMonths(months);
					dataRow.EndEdit();
					dataTable.Rows.Add(dataRow);
				}
				dataTable.AcceptChanges();
			}
		}

		private void buttonFillDetails_Click(object sender, EventArgs e)
		{
			CalculateMultiperiod();
		}

		private void checkBoxMultiPeriod_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxMultiPeriod.Checked)
			{
				NumericUpDown numericNoofmonths = NumericNoofmonths;
				NumericUpDown numericnoofpayments = Numericnoofpayments;
				MMLabel labelNoofmonth = LabelNoofmonth;
				MMLabel labelNoofpayment = LabelNoofpayment;
				UltraFormattedLinkLabel labelEmployee = LabelEmployee;
				EmployeeComboBox employeeComboBox = comboBoxEmployee;
				bool flag2 = buttonFillDetails.Visible = true;
				bool flag4 = employeeComboBox.Visible = flag2;
				bool flag6 = labelEmployee.Visible = flag4;
				bool flag8 = labelNoofpayment.Visible = flag6;
				bool flag10 = labelNoofmonth.Visible = flag8;
				bool visible = numericnoofpayments.Visible = flag10;
				numericNoofmonths.Visible = visible;
			}
			else
			{
				NumericUpDown numericNoofmonths2 = NumericNoofmonths;
				NumericUpDown numericnoofpayments2 = Numericnoofpayments;
				MMLabel labelNoofmonth2 = LabelNoofmonth;
				MMLabel labelNoofpayment2 = LabelNoofpayment;
				UltraFormattedLinkLabel labelEmployee2 = LabelEmployee;
				EmployeeComboBox employeeComboBox2 = comboBoxEmployee;
				bool flag2 = buttonFillDetails.Visible = false;
				bool flag4 = employeeComboBox2.Visible = flag2;
				bool flag6 = labelEmployee2.Visible = flag4;
				bool flag8 = labelNoofpayment2.Visible = flag6;
				bool flag10 = labelNoofmonth2.Visible = flag8;
				bool visible = numericnoofpayments2.Visible = flag10;
				numericNoofmonths2.Visible = visible;
				MMLabel labelPeriod = LabelPeriod;
				visible = (dateTimePickerDate.Visible = true);
				labelPeriod.Visible = visible;
			}
			bool @checked = checkBoxMultiPeriod.Checked;
			SetupGrid(@checked);
		}

		private void checkBoxMultiPeriod_CheckStateChanged(object sender, EventArgs e)
		{
			if (checkBoxMultiPeriod.Checked)
			{
				NumericUpDown numericNoofmonths = NumericNoofmonths;
				NumericUpDown numericnoofpayments = Numericnoofpayments;
				MMLabel labelNoofmonth = LabelNoofmonth;
				MMLabel labelNoofpayment = LabelNoofpayment;
				UltraFormattedLinkLabel labelEmployee = LabelEmployee;
				EmployeeComboBox employeeComboBox = comboBoxEmployee;
				bool flag2 = buttonFillDetails.Visible = true;
				bool flag4 = employeeComboBox.Visible = flag2;
				bool flag6 = labelEmployee.Visible = flag4;
				bool flag8 = labelNoofpayment.Visible = flag6;
				bool flag10 = labelNoofmonth.Visible = flag8;
				bool visible = numericnoofpayments.Visible = flag10;
				numericNoofmonths.Visible = visible;
			}
			else
			{
				NumericUpDown numericNoofmonths2 = NumericNoofmonths;
				NumericUpDown numericnoofpayments2 = Numericnoofpayments;
				MMLabel labelNoofmonth2 = LabelNoofmonth;
				MMLabel labelNoofpayment2 = LabelNoofpayment;
				UltraFormattedLinkLabel labelEmployee2 = LabelEmployee;
				EmployeeComboBox employeeComboBox2 = comboBoxEmployee;
				bool flag2 = buttonFillDetails.Visible = false;
				bool flag4 = employeeComboBox2.Visible = flag2;
				bool flag6 = labelEmployee2.Visible = flag4;
				bool flag8 = labelNoofpayment2.Visible = flag6;
				bool flag10 = labelNoofmonth2.Visible = flag8;
				bool visible = numericnoofpayments2.Visible = flag10;
				numericNoofmonths2.Visible = visible;
				MMLabel labelPeriod = LabelPeriod;
				visible = (dateTimePickerDate.Visible = true);
				labelPeriod.Visible = visible;
			}
			bool @checked = checkBoxMultiPeriod.Checked;
			SetupGrid(@checked);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntitySysDocID = "SAL_DED";
					docManagementForm.EntityName = "SAL_DED";
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void labelEmployee_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void toolStripButtonExcelImport_Click(object sender, EventArgs e)
		{
			dataGridItems.ImportFromExcel(autoFill: true);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
		}

		private void FillGridData(DataSet data)
		{
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			dataGridItems.BeginUpdate();
			foreach (DataRow row in data.Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["EmpNo"] = row["EmployeeID"].ToString();
				dataRow2["EmpName"] = row["Employee Name"].ToString();
				dataRow2["Remarks"] = row["Remarks"].ToString();
				if (row["Amount"] != DBNull.Value)
				{
					dataRow2["Amount"] = decimal.Parse(row["Amount"].ToString());
				}
				else
				{
					dataRow2["Amount"] = DBNull.Value;
				}
				if (row["Quantity"] != DBNull.Value)
				{
					dataRow2["Quantity"] = decimal.Parse(row["Quantity"].ToString());
				}
				else
				{
					dataRow2["Quantity"] = DBNull.Value;
				}
				if (row["Rate"] != DBNull.Value)
				{
					dataRow2["Rate"] = decimal.Parse(row["Rate"].ToString());
				}
				else
				{
					dataRow2["Rate"] = DBNull.Value;
				}
				dataRow2.EndEdit();
				dataTable.Rows.Add(dataRow2);
			}
			dataTable.AcceptChanges();
			dataGridItems.EndUpdate();
			formManager.ResetDirty();
		}

		private void loadDeductionAmountToolStripMenuItem_Click(object sender, EventArgs e)
		{
			IsNewRecord = true;
			if (checkBoxDeductionPercent.Checked && textBoxDeductionPercent.Text == "")
			{
				ErrorHelper.WarningMessage("Please Enter Deduction Percent.");
				textBoxDeductionPercent.Focus();
				return;
			}
			if (textBoxDeductionPercent.Text != "")
			{
				DeductionPercent = int.Parse(textBoxDeductionPercent.Text);
			}
			if (calcForm.ShowDialog() == DialogResult.OK)
			{
				DataSet dataSet = Factory.SalarySystem.LoadDeductionAmount(calcForm.FromEmployee, calcForm.ToEmployee, calcForm.FromDepartment, calcForm.ToDepartment, calcForm.FromLocation, calcForm.ToLocation, calcForm.FromType, calcForm.ToType, calcForm.FromDivision, calcForm.ToDivision, calcForm.FromSponsor, calcForm.ToSponsor, calcForm.FromGroup, calcForm.ToGroup, calcForm.FromGrade, calcForm.ToGrade, calcForm.FromPosition, calcForm.ToPosition, calcForm.FromBank, calcForm.ToBank, calcForm.FromAccount, calcForm.ToAccount, dateTimePickerDate.Value, calcForm.MultipleEmployees, DeductionPercent);
				if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
				{
					IsChangedallowed = false;
					FillGridData(dataSet);
					formManager.IsForcedDirty = true;
				}
			}
		}

		private void checkBoxDeductionPercent_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxDeductionPercent.Checked)
			{
				textBoxDeductionPercent.Visible = true;
				labelDeductionPercent.Visible = true;
			}
			else
			{
				textBoxDeductionPercent.Clear();
				textBoxDeductionPercent.Visible = false;
				labelDeductionPercent.Visible = false;
			}
		}

		private void textBoxDeductionPercent_TextChanged(object sender, EventArgs e)
		{
			if (textBoxDeductionPercent.Text != "" && int.Parse(textBoxDeductionPercent.Text) > 100)
			{
				ErrorHelper.WarningMessage("", "Please Enter value less than 100");
				textBoxDeductionPercent.Clear();
			}
			if (textBoxDeductionPercent.Text != "")
			{
				DeductionPercent = int.Parse(textBoxDeductionPercent.Text);
			}
			else
			{
				DeductionPercent = 0;
			}
			if (dataGridItems.Rows.Count != 0)
			{
				DataSet dataSet = Factory.SalarySystem.LoadDeductionAmount(calcForm.FromEmployee, calcForm.ToEmployee, calcForm.FromDepartment, calcForm.ToDepartment, calcForm.FromLocation, calcForm.ToLocation, calcForm.FromType, calcForm.ToType, calcForm.FromDivision, calcForm.ToDivision, calcForm.FromSponsor, calcForm.ToSponsor, calcForm.FromGroup, calcForm.ToGroup, calcForm.FromGrade, calcForm.ToGrade, calcForm.FromPosition, calcForm.ToPosition, calcForm.FromBank, calcForm.ToBank, calcForm.FromAccount, calcForm.ToAccount, dateTimePickerDate.Value, calcForm.MultipleEmployees, DeductionPercent);
				if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
				{
					IsChangedallowed = false;
					FillGridData(dataSet);
					formManager.IsForcedDirty = true;
				}
			}
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
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.SalaryDeductionListFormObj);
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void ShowEmployeeSelector()
		{
			empSelectorObj = new EmployeeSelector2();
			empSelectorObj.BackColor = Color.Transparent;
			empSelectorObj.CustomReportFieldName = "";
			empSelectorObj.CustomReportKey = "";
			empSelectorObj.CustomReportValueType = 1;
			empSelectorObj.Location = new Point(6, 15);
			empSelectorObj.Name = "EmployeeSelector";
			empSelectorObj.Size = new Size(430, 350);
			empSelectorObj.TabIndex = 0;
			GroupBox groupBox = new GroupBox();
			groupBox.Controls.Add(empSelectorObj);
			groupBox.Location = new Point(2, 12);
			groupBox.Name = "groupBoxCC";
			groupBox.Size = new Size(453, 353);
			groupBox.TabIndex = 10;
			groupBox.TabStop = false;
			groupBox.Text = "Employees";
			Button button = new Button();
			button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			button.Location = new Point(249, 370);
			button.Name = "buttonOK";
			button.Size = new Size(102, 24);
			button.TabIndex = 1;
			button.Text = "&Display";
			button.UseVisualStyleBackColor = true;
			button.Click += buttonOK_Click;
			Button button2 = new Button();
			button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			button2.DialogResult = DialogResult.Cancel;
			button2.Location = new Point(353, 370);
			button2.Name = "buttonClose";
			button2.Size = new Size(102, 24);
			button2.TabIndex = 5;
			button2.Text = "&Close";
			button2.UseVisualStyleBackColor = true;
			empFormObj = new Form();
			empFormObj.Size = new Size(481, 440);
			empFormObj.Controls.Add(button);
			empFormObj.Controls.Add(button2);
			empFormObj.Controls.Add(groupBox);
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SalaryDeductionForm));
			empFormObj.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			empFormObj.Text = "Select Employee";
			empFormObj.ShowDialog();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet employeeList = Factory.EmployeeSystem.GetEmployeeList(empSelectorObj.FromEmployee, empSelectorObj.ToEmployee, empSelectorObj.FromDepartment, empSelectorObj.ToDepartment, empSelectorObj.FromLocation, empSelectorObj.ToLocation, empSelectorObj.FromType, empSelectorObj.ToType, empSelectorObj.FromDivision, empSelectorObj.ToDivision, empSelectorObj.FromSponsor, empSelectorObj.ToSponsor, empSelectorObj.FromGroup, empSelectorObj.ToGroup, empSelectorObj.FromGrade, empSelectorObj.ToGrade, empSelectorObj.FromPosition, empSelectorObj.ToPosition, empSelectorObj.FromBank, empSelectorObj.ToBank, empSelectorObj.FromAccount, empSelectorObj.ToAccount, showInactive: false, empSelectorObj.MultipleEmployees);
				FillDataEmployee(employeeList);
				empFormObj.Close();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
				ClearForm();
			}
		}

		private void FillDataEmployee(DataSet dt)
		{
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (dt.Tables[0].Rows.Count != 0)
			{
				dataGridItems.BeginUpdate();
				foreach (DataRow row in dt.Tables["Employee"].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					if (row["EmployeeID"] != DBNull.Value)
					{
						dataRow2["EmpNo"] = row["EmployeeID"];
					}
					if (row["EmployeeName"] != DBNull.Value)
					{
						dataRow2["EmpName"] = row["EmployeeName"];
					}
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
				dataGridItems.EndUpdate();
			}
		}

		private void loadEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowEmployeeSelector();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.SalaryDeductionForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonExcelImport = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			loadEmployeesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDeductionAmountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
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
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			panelDetails = new System.Windows.Forms.Panel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxDeductionPercent = new Micromind.UISupport.PercentTextBox();
			labelDeductionPercent = new Micromind.UISupport.MMLabel();
			checkBoxDeductionPercent = new System.Windows.Forms.CheckBox();
			LabelEmployee = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			buttonFillDetails = new System.Windows.Forms.Button();
			Numericnoofpayments = new System.Windows.Forms.NumericUpDown();
			NumericNoofmonths = new System.Windows.Forms.NumericUpDown();
			LabelNoofpayment = new Micromind.UISupport.MMLabel();
			LabelNoofmonth = new Micromind.UISupport.MMLabel();
			checkBoxMultiPeriod = new System.Windows.Forms.CheckBox();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			LabelPeriod = new Micromind.UISupport.MMLabel();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panelApproval = new System.Windows.Forms.Panel();
			panelApprovalDetail = new System.Windows.Forms.Panel();
			textBoxCreatedBy = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			dateTimeCreatedDate = new System.Windows.Forms.DateTimePicker();
			label4 = new System.Windows.Forms.Label();
			textBoxApprovedBy = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			dateTimeApprovalDate = new System.Windows.Forms.DateTimePicker();
			label1 = new System.Windows.Forms.Label();
			checkBoxApprove = new System.Windows.Forms.CheckBox();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxPayrolItem = new Micromind.DataControls.PayrollItemComboBox();
			comboBoxGridEmployee = new Micromind.DataControls.EmployeeComboBox();
			comboBoxDeduction = new Micromind.DataControls.PayrollItemComboBox();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)Numericnoofpayments).BeginInit();
			((System.ComponentModel.ISupportInitialize)NumericNoofmonths).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			contextMenuStrip1.SuspendLayout();
			panelApproval.SuspendLayout();
			panelApprovalDetail.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrolItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDeduction).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[19]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator7,
				toolStripButtonOpenList,
				toolStripSeparator8,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonExcelImport,
				toolStripSeparator3,
				toolStripDropDownButton1,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(877, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.ToolTipText = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.ToolTipText = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.ToolTipText = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last Record";
			toolStripButtonLast.ToolTipText = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator8.Name = "toolStripSeparator8";
			toolStripSeparator8.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonExcelImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonExcelImport.Image = Micromind.ClientUI.Properties.Resources.excelimport;
			toolStripButtonExcelImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonExcelImport.Name = "toolStripButtonExcelImport";
			toolStripButtonExcelImport.Size = new System.Drawing.Size(28, 28);
			toolStripButtonExcelImport.Text = "Import from Excel";
			toolStripButtonExcelImport.Click += new System.EventHandler(toolStripButtonExcelImport_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				duplicateToolStripMenuItem,
				toolStripSeparator6,
				loadEmployeesToolStripMenuItem,
				loadDeductionAmountToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			duplicateToolStripMenuItem.Text = "Duplicate";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(202, 6);
			loadEmployeesToolStripMenuItem.Name = "loadEmployeesToolStripMenuItem";
			loadEmployeesToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			loadEmployeesToolStripMenuItem.Text = "Load Employees";
			loadEmployeesToolStripMenuItem.Click += new System.EventHandler(loadEmployeesToolStripMenuItem_Click);
			loadDeductionAmountToolStripMenuItem.Name = "loadDeductionAmountToolStripMenuItem";
			loadDeductionAmountToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			loadDeductionAmountToolStripMenuItem.Text = "Load Deduction Amount";
			loadDeductionAmountToolStripMenuItem.Click += new System.EventHandler(loadDeductionAmountToolStripMenuItem_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 528);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(877, 40);
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
			buttonVoid.TabIndex = 13;
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
			buttonDelete.TabIndex = 12;
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
			buttonNew.TabIndex = 11;
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
			linePanelDown.Size = new System.Drawing.Size(877, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(767, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 14;
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
			buttonSave.TabIndex = 10;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraCalcManager1.ContainingControl = this;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(282, 4);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(139, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(97, 4);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(127, 20);
			textBoxVoucherNumber.TabIndex = 1;
			textBoxNote.Location = new System.Drawing.Point(97, 26);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(324, 48);
			textBoxNote.TabIndex = 3;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 30);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(textBoxDeductionPercent);
			panelDetails.Controls.Add(labelDeductionPercent);
			panelDetails.Controls.Add(checkBoxDeductionPercent);
			panelDetails.Controls.Add(LabelEmployee);
			panelDetails.Controls.Add(buttonFillDetails);
			panelDetails.Controls.Add(Numericnoofpayments);
			panelDetails.Controls.Add(NumericNoofmonths);
			panelDetails.Controls.Add(LabelNoofpayment);
			panelDetails.Controls.Add(LabelNoofmonth);
			panelDetails.Controls.Add(checkBoxMultiPeriod);
			panelDetails.Controls.Add(comboBoxEmployee);
			panelDetails.Controls.Add(linkLabelVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(LabelPeriod);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Location = new System.Drawing.Point(0, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(851, 77);
			panelDetails.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(828, 31);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(15, 13);
			mmLabel1.TabIndex = 172;
			mmLabel1.Text = "%";
			mmLabel1.Visible = false;
			textBoxDeductionPercent.CustomReportFieldName = "";
			textBoxDeductionPercent.CustomReportKey = "";
			textBoxDeductionPercent.CustomReportValueType = 1;
			textBoxDeductionPercent.IsComboTextBox = false;
			textBoxDeductionPercent.IsModified = false;
			textBoxDeductionPercent.Location = new System.Drawing.Point(767, 28);
			textBoxDeductionPercent.Name = "textBoxDeductionPercent";
			textBoxDeductionPercent.Size = new System.Drawing.Size(56, 20);
			textBoxDeductionPercent.TabIndex = 170;
			textBoxDeductionPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDeductionPercent.Visible = false;
			textBoxDeductionPercent.TextChanged += new System.EventHandler(textBoxDeductionPercent_TextChanged);
			labelDeductionPercent.AutoSize = true;
			labelDeductionPercent.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelDeductionPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelDeductionPercent.IsFieldHeader = false;
			labelDeductionPercent.IsRequired = false;
			labelDeductionPercent.Location = new System.Drawing.Point(704, 31);
			labelDeductionPercent.Name = "labelDeductionPercent";
			labelDeductionPercent.PenWidth = 1f;
			labelDeductionPercent.ShowBorder = false;
			labelDeductionPercent.Size = new System.Drawing.Size(59, 13);
			labelDeductionPercent.TabIndex = 171;
			labelDeductionPercent.Text = "Deduction:";
			labelDeductionPercent.Visible = false;
			checkBoxDeductionPercent.AutoSize = true;
			checkBoxDeductionPercent.Location = new System.Drawing.Point(694, 5);
			checkBoxDeductionPercent.Name = "checkBoxDeductionPercent";
			checkBoxDeductionPercent.Size = new System.Drawing.Size(115, 17);
			checkBoxDeductionPercent.TabIndex = 169;
			checkBoxDeductionPercent.Text = "Deduction Percent";
			checkBoxDeductionPercent.UseVisualStyleBackColor = true;
			checkBoxDeductionPercent.CheckedChanged += new System.EventHandler(checkBoxDeductionPercent_CheckedChanged);
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			LabelEmployee.Appearance = appearance;
			LabelEmployee.AutoSize = true;
			LabelEmployee.Location = new System.Drawing.Point(428, 9);
			LabelEmployee.Name = "LabelEmployee";
			LabelEmployee.Size = new System.Drawing.Size(62, 15);
			LabelEmployee.TabIndex = 168;
			LabelEmployee.TabStop = true;
			LabelEmployee.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			LabelEmployee.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			LabelEmployee.Value = "Employee:";
			LabelEmployee.Visible = false;
			appearance2.ForeColor = System.Drawing.Color.Blue;
			LabelEmployee.VisitedLinkAppearance = appearance2;
			LabelEmployee.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelEmployee_LinkClicked);
			buttonFillDetails.Location = new System.Drawing.Point(747, 52);
			buttonFillDetails.Name = "buttonFillDetails";
			buttonFillDetails.Size = new System.Drawing.Size(99, 23);
			buttonFillDetails.TabIndex = 167;
			buttonFillDetails.Text = "Fill Details";
			buttonFillDetails.UseVisualStyleBackColor = true;
			buttonFillDetails.Visible = false;
			buttonFillDetails.Click += new System.EventHandler(buttonFillDetails_Click);
			Numericnoofpayments.Location = new System.Drawing.Point(662, 29);
			Numericnoofpayments.Name = "Numericnoofpayments";
			Numericnoofpayments.Size = new System.Drawing.Size(41, 20);
			Numericnoofpayments.TabIndex = 165;
			Numericnoofpayments.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			Numericnoofpayments.Visible = false;
			NumericNoofmonths.Location = new System.Drawing.Point(515, 29);
			NumericNoofmonths.Name = "NumericNoofmonths";
			NumericNoofmonths.Size = new System.Drawing.Size(41, 20);
			NumericNoofmonths.TabIndex = 164;
			NumericNoofmonths.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			NumericNoofmonths.Visible = false;
			LabelNoofpayment.AutoSize = true;
			LabelNoofpayment.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			LabelNoofpayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			LabelNoofpayment.IsFieldHeader = false;
			LabelNoofpayment.IsRequired = true;
			LabelNoofpayment.Location = new System.Drawing.Point(562, 31);
			LabelNoofpayment.Name = "LabelNoofpayment";
			LabelNoofpayment.PenWidth = 1f;
			LabelNoofpayment.ShowBorder = false;
			LabelNoofpayment.Size = new System.Drawing.Size(94, 13);
			LabelNoofpayment.TabIndex = 163;
			LabelNoofpayment.Text = "No of Payment:";
			LabelNoofpayment.Visible = false;
			LabelNoofmonth.AutoSize = true;
			LabelNoofmonth.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			LabelNoofmonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			LabelNoofmonth.IsFieldHeader = false;
			LabelNoofmonth.IsRequired = true;
			LabelNoofmonth.Location = new System.Drawing.Point(428, 31);
			LabelNoofmonth.Name = "LabelNoofmonth";
			LabelNoofmonth.PenWidth = 1f;
			LabelNoofmonth.ShowBorder = false;
			LabelNoofmonth.Size = new System.Drawing.Size(81, 13);
			LabelNoofmonth.TabIndex = 162;
			LabelNoofmonth.Text = "No of Month:";
			LabelNoofmonth.Visible = false;
			checkBoxMultiPeriod.AutoSize = true;
			checkBoxMultiPeriod.Location = new System.Drawing.Point(621, 4);
			checkBoxMultiPeriod.Name = "checkBoxMultiPeriod";
			checkBoxMultiPeriod.Size = new System.Drawing.Size(77, 17);
			checkBoxMultiPeriod.TabIndex = 160;
			checkBoxMultiPeriod.Text = "Mulitperiod";
			checkBoxMultiPeriod.UseVisualStyleBackColor = true;
			checkBoxMultiPeriod.CheckedChanged += new System.EventHandler(checkBoxMultiPeriod_CheckedChanged);
			checkBoxMultiPeriod.CheckStateChanged += new System.EventHandler(checkBoxMultiPeriod_CheckStateChanged);
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CalcManager = ultraCalcManager1;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = null;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance3;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(499, 4);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(116, 20);
			comboBoxEmployee.TabIndex = 161;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployee.Visible = false;
			appearance15.FontData.BoldAsString = "True";
			appearance15.FontData.Name = "Tahoma";
			linkLabelVoucherNumber.Appearance = appearance15;
			linkLabelVoucherNumber.AutoSize = true;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(14, 7);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(77, 15);
			linkLabelVoucherNumber.TabIndex = 136;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Doc Number:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			linkLabelVoucherNumber.VisitedLinkAppearance = appearance16;
			linkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVoucherNumber_LinkClicked);
			LabelPeriod.AutoSize = true;
			LabelPeriod.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			LabelPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			LabelPeriod.IsFieldHeader = false;
			LabelPeriod.IsRequired = true;
			LabelPeriod.Location = new System.Drawing.Point(229, 7);
			LabelPeriod.Name = "LabelPeriod";
			LabelPeriod.PenWidth = 1f;
			LabelPeriod.ShowBorder = false;
			LabelPeriod.Size = new System.Drawing.Size(47, 13);
			LabelPeriod.TabIndex = 2;
			LabelPeriod.Text = "Period:";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(12, 398);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(834, 83);
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
			panelApproval.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			panelApproval.Controls.Add(panelApprovalDetail);
			panelApproval.Controls.Add(checkBoxApprove);
			panelApproval.Location = new System.Drawing.Point(11, 480);
			panelApproval.Name = "panelApproval";
			panelApproval.Size = new System.Drawing.Size(849, 31);
			panelApproval.TabIndex = 128;
			panelApproval.Visible = false;
			panelApprovalDetail.Controls.Add(textBoxCreatedBy);
			panelApprovalDetail.Controls.Add(label5);
			panelApprovalDetail.Controls.Add(dateTimeCreatedDate);
			panelApprovalDetail.Controls.Add(label4);
			panelApprovalDetail.Controls.Add(textBoxApprovedBy);
			panelApprovalDetail.Controls.Add(label2);
			panelApprovalDetail.Controls.Add(dateTimeApprovalDate);
			panelApprovalDetail.Controls.Add(label1);
			panelApprovalDetail.Location = new System.Drawing.Point(71, 4);
			panelApprovalDetail.Name = "panelApprovalDetail";
			panelApprovalDetail.Size = new System.Drawing.Size(775, 24);
			panelApprovalDetail.TabIndex = 132;
			panelApprovalDetail.Visible = false;
			textBoxCreatedBy.Location = new System.Drawing.Point(653, 2);
			textBoxCreatedBy.MaxLength = 250;
			textBoxCreatedBy.Name = "textBoxCreatedBy";
			textBoxCreatedBy.ReadOnly = true;
			textBoxCreatedBy.Size = new System.Drawing.Size(114, 20);
			textBoxCreatedBy.TabIndex = 9;
			textBoxCreatedBy.TabStop = false;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(586, 5);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(62, 13);
			label5.TabIndex = 130;
			label5.Text = "Created By:";
			dateTimeCreatedDate.Enabled = false;
			dateTimeCreatedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeCreatedDate.Location = new System.Drawing.Point(467, 2);
			dateTimeCreatedDate.Name = "dateTimeCreatedDate";
			dateTimeCreatedDate.Size = new System.Drawing.Size(112, 20);
			dateTimeCreatedDate.TabIndex = 8;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(390, 5);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(73, 13);
			label4.TabIndex = 128;
			label4.Text = "Created Date:";
			textBoxApprovedBy.Location = new System.Drawing.Point(279, 2);
			textBoxApprovedBy.MaxLength = 250;
			textBoxApprovedBy.Name = "textBoxApprovedBy";
			textBoxApprovedBy.ReadOnly = true;
			textBoxApprovedBy.Size = new System.Drawing.Size(104, 20);
			textBoxApprovedBy.TabIndex = 7;
			textBoxApprovedBy.TabStop = false;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(205, 5);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(71, 13);
			label2.TabIndex = 23;
			label2.Text = "Approved By:";
			dateTimeApprovalDate.Enabled = false;
			dateTimeApprovalDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeApprovalDate.Location = new System.Drawing.Point(86, 2);
			dateTimeApprovalDate.Name = "dateTimeApprovalDate";
			dateTimeApprovalDate.Size = new System.Drawing.Size(112, 20);
			dateTimeApprovalDate.TabIndex = 6;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 5);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(82, 13);
			label1.TabIndex = 21;
			label1.Text = "Approved Date:";
			checkBoxApprove.AutoSize = true;
			checkBoxApprove.Location = new System.Drawing.Point(7, 8);
			checkBoxApprove.Name = "checkBoxApprove";
			checkBoxApprove.Size = new System.Drawing.Size(66, 17);
			checkBoxApprove.TabIndex = 5;
			checkBoxApprove.Text = "Approve";
			checkBoxApprove.UseVisualStyleBackColor = true;
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
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance17;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance24;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance26;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance27;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(11, 114);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(840, 368);
			dataGridItems.TabIndex = 4;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			comboBoxPayrolItem.Assigned = false;
			comboBoxPayrolItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPayrolItem.CalcManager = ultraCalcManager1;
			comboBoxPayrolItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayrolItem.CustomReportFieldName = "";
			comboBoxPayrolItem.CustomReportKey = "";
			comboBoxPayrolItem.CustomReportValueType = 1;
			comboBoxPayrolItem.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayrolItem.DisplayLayout.Appearance = appearance29;
			comboBoxPayrolItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayrolItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxPayrolItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayrolItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayrolItem.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayrolItem.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxPayrolItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayrolItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayrolItem.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayrolItem.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxPayrolItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayrolItem.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrolItem.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxPayrolItem.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxPayrolItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayrolItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayrolItem.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxPayrolItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayrolItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxPayrolItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayrolItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayrolItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayrolItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayrolItem.Editable = true;
			comboBoxPayrolItem.FilterString = "";
			comboBoxPayrolItem.HasAllAccount = false;
			comboBoxPayrolItem.HasCustom = false;
			comboBoxPayrolItem.IsDataLoaded = false;
			comboBoxPayrolItem.IsDeduction = false;
			comboBoxPayrolItem.Location = new System.Drawing.Point(727, 222);
			comboBoxPayrolItem.MaxDropDownItems = 12;
			comboBoxPayrolItem.Name = "comboBoxPayrolItem";
			comboBoxPayrolItem.ShowInactiveItems = false;
			comboBoxPayrolItem.ShowQuickAdd = true;
			comboBoxPayrolItem.Size = new System.Drawing.Size(142, 20);
			comboBoxPayrolItem.TabIndex = 126;
			comboBoxPayrolItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPayrolItem.Visible = false;
			comboBoxGridEmployee.Assigned = false;
			comboBoxGridEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridEmployee.CalcManager = ultraCalcManager1;
			comboBoxGridEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridEmployee.CustomReportFieldName = "";
			comboBoxGridEmployee.CustomReportKey = "";
			comboBoxGridEmployee.CustomReportValueType = 1;
			comboBoxGridEmployee.DescriptionTextBox = null;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridEmployee.DisplayLayout.Appearance = appearance41;
			comboBoxGridEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxGridEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridEmployee.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxGridEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxGridEmployee.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridEmployee.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxGridEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxGridEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridEmployee.Editable = true;
			comboBoxGridEmployee.FilterString = "";
			comboBoxGridEmployee.HasAllAccount = false;
			comboBoxGridEmployee.HasCustom = false;
			comboBoxGridEmployee.IsDataLoaded = false;
			comboBoxGridEmployee.Location = new System.Drawing.Point(730, 248);
			comboBoxGridEmployee.MaxDropDownItems = 12;
			comboBoxGridEmployee.Name = "comboBoxGridEmployee";
			comboBoxGridEmployee.ShowInactiveItems = false;
			comboBoxGridEmployee.ShowQuickAdd = true;
			comboBoxGridEmployee.ShowTerminatedEmployees = true;
			comboBoxGridEmployee.Size = new System.Drawing.Size(139, 20);
			comboBoxGridEmployee.TabIndex = 129;
			comboBoxGridEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridEmployee.Visible = false;
			comboBoxDeduction.Assigned = false;
			comboBoxDeduction.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDeduction.CalcManager = ultraCalcManager1;
			comboBoxDeduction.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDeduction.CustomReportFieldName = "";
			comboBoxDeduction.CustomReportKey = "";
			comboBoxDeduction.CustomReportValueType = 1;
			comboBoxDeduction.DescriptionTextBox = null;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDeduction.DisplayLayout.Appearance = appearance53;
			comboBoxDeduction.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDeduction.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDeduction.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDeduction.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxDeduction.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDeduction.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxDeduction.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDeduction.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDeduction.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDeduction.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxDeduction.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDeduction.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDeduction.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDeduction.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxDeduction.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDeduction.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDeduction.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxDeduction.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxDeduction.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDeduction.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxDeduction.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxDeduction.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDeduction.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxDeduction.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDeduction.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDeduction.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDeduction.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDeduction.Editable = true;
			comboBoxDeduction.FilterString = "";
			comboBoxDeduction.HasAllAccount = false;
			comboBoxDeduction.HasCustom = false;
			comboBoxDeduction.IsDataLoaded = false;
			comboBoxDeduction.IsDeduction = false;
			comboBoxDeduction.Location = new System.Drawing.Point(769, 274);
			comboBoxDeduction.MaxDropDownItems = 12;
			comboBoxDeduction.Name = "comboBoxDeduction";
			comboBoxDeduction.ShowInactiveItems = false;
			comboBoxDeduction.ShowQuickAdd = true;
			comboBoxDeduction.Size = new System.Drawing.Size(100, 20);
			comboBoxDeduction.TabIndex = 131;
			comboBoxDeduction.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDeduction.Visible = false;
			ultraGridPrintDocument1.DocumentName = "Inventory Adjustment - List";
			ultraGridPrintDocument1.Grid = dataGridItems;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(877, 568);
			base.Controls.Add(panelApproval);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxPayrolItem);
			base.Controls.Add(comboBoxGridEmployee);
			base.Controls.Add(comboBoxDeduction);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(885, 595);
			base.Name = "SalaryDeductionForm";
			Text = "Salary Deduction";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)Numericnoofpayments).EndInit();
			((System.ComponentModel.ISupportInitialize)NumericNoofmonths).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			panelApproval.ResumeLayout(false);
			panelApproval.PerformLayout();
			panelApprovalDetail.ResumeLayout(false);
			panelApprovalDetail.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrolItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDeduction).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
