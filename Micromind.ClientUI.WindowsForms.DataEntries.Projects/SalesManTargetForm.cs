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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Projects
{
	public class SalesManTargetForm : Form, IForm, IWorkFlowForm
	{
		private bool supressExpenseMessage;

		private int totalWorkingHours = CompanyPreferences.TotalWorkingDayHours;

		private int offday = CompanyPreferences.OffDay;

		private SalesManTargetData currentData;

		private DataSet currentDataset;

		private const string TABLENAME_CONST = "Sales_Man_Target";

		private const string TABLENAMEDETAIL_CONST = "Sales_Man_Target_Detail";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool byVoucherId = true;

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

		private DateTimePicker dateTimePickerFromDate;

		private TextBox textBoxVoucherNumber;

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

		private JobComboBox comboBoxJob;

		private EmployeeComboBox comboBoxEmployee;

		private TextBox textBoxEmployeeName;

		private UltraFormattedLinkLabel employeeLinkLabel;

		private ComboBox comboBoxYear;

		private MonthComboBox comboBoxMonth;

		private MMLabel mmLabel4;

		private PayrollItemComboBox comboBoxPayrolItem;

		private OverTimeComboBox comboBoxOverTime;

		private EmployeeComboBox comboBoxGridEmployee;

		private Panel panelEmployee;

		private UltraFormattedLinkLabel linkLabelVoucherNumber;

		private Button buttonFillDetails;

		private WorkLocationComboBox comboBoxLocation;

		private JobComboBox jobComboBox1;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem copyToolStripMenuItem;

		private MMLabel mmLabel1;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonExcelImport;

		private CostCategoryComboBox comboBoxCostCategory;

		private DriverComboBox driverComboBox1;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerToDate;

		private SalespersonGroupComboBox salespersonGroupComboBox1;

		private SalespersonComboBox salespersonComboBox1;

		private ItemClassComboBox itemClassComboBox1;

		private ProductComboBox productComboBox1;

		private Label label1;

		private RadioButton radioButtonCommission;

		private RadioButton radioButtonAmount;

		private MMLabel mmLabel3;

		private Label label2;

		private AdjustmentTypeComboBox adjustmentTypeComboBox1;

		private ProductCategoryComboBox productCategoryComboBox;

		private Label label3;

		public ScreenAreas ScreenArea => ScreenAreas.Project;

		public int ScreenID => 4002;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		private string VoucherID => textBoxVoucherNumber.Text.Trim();

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
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = true);
					sysDocComboBox.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = false);
					sysDocComboBox2.Enabled = enabled;
				}
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
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

		public SalesManTargetForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
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
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dateTimePickerFromDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			base.FormClosing += ItemGroupDetailsForm_FormClosing;
			dataGridItems.BeforeRowActivate += dataGridItems_BeforeRowActivate;
			comboBoxMonth.SelectedIndexChanged += comboBoxMonth_SelectedIndexChanged;
			dataGridItems.KeyPress += dataGridItems_KeyDown;
		}

		private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void checkBoxApprove_CheckedChanged(object sender, EventArgs e)
		{
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

		private void dataGridItems_KeyDown(object sender, KeyPressEventArgs e)
		{
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow != null)
				{
					_ = e.Cell.Column.Key;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				_ = IsNewRecord;
				buttonFillDetails.Enabled = true;
			}
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
			_ = dataGridItems.ActiveRow;
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			_ = dataGridItems.ActiveRow;
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			_ = dataGridItems.ActiveRow;
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
					currentData = new SalesManTargetData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.SalesManTargetTable.Rows[0] : currentData.SalesManTargetTable.NewRow();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["FromDate"] = dateTimePickerFromDate.Value;
				dataRow["ToDate"] = dateTimePickerToDate.Value;
				dataRow["Month"] = int.Parse(comboBoxMonth.SelectedItem.ToString());
				if (radioButtonAmount.Checked)
				{
					dataRow["TargetType"] = "A";
				}
				else
				{
					dataRow["TargetType"] = "Q";
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.SalesManTargetTable.Rows.Add(dataRow);
				}
				currentData.SalesManTargetDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.SalesManTargetDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
					if (row.Cells["Sales Man Group ID"].Value != null && row.Cells["Sales Man Group ID"].Value.ToString() != "")
					{
						dataRow2["SalesManGroupID"] = row.Cells["Sales Man Group ID"].Value.ToString();
					}
					else
					{
						dataRow2["SalesManGroupID"] = DBNull.Value;
					}
					if (row.Cells["Sales Man ID"].Value != null && row.Cells["Sales Man ID"].Value.ToString() != "")
					{
						dataRow2["SalesManID"] = row.Cells["Sales Man ID"].Value.ToString();
					}
					else
					{
						dataRow2["SalesManID"] = DBNull.Value;
					}
					if (row.Cells["Product Class ID"].Value != null && row.Cells["Product Class ID"].Value.ToString() != "")
					{
						dataRow2["ProductClassID"] = row.Cells["Product Class ID"].Value.ToString();
					}
					else
					{
						dataRow2["ProductClassID"] = DBNull.Value;
					}
					if (row.Cells["Product Category ID"].Value.ToString() != "")
					{
						dataRow2["ProductCategoryID"] = row.Cells["Product Category ID"].Value.ToString();
					}
					else
					{
						dataRow2["ProductCategoryID"] = DBNull.Value;
					}
					if (row.Cells["Product ID"].Value.ToString() != "")
					{
						dataRow2["ProductID"] = row.Cells["Product ID"].Value.ToString();
					}
					else
					{
						dataRow2["ProductID"] = DBNull.Value;
					}
					if (row.Cells["Amount"].Value.ToString() != "")
					{
						dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
					}
					else
					{
						dataRow2["Amount"] = DBNull.Value;
					}
					if (row.Cells["Commission %"].Value.ToString() != "")
					{
						dataRow2["CommissionPercent"] = row.Cells["Commission %"].Value.ToString();
					}
					else
					{
						dataRow2["CommissionPercent"] = DBNull.Value;
					}
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.SalesManTargetDetailTable.Rows.Add(dataRow2);
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
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Sales_Man_Target"].Rows[0];
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					if (dataRow["FromDate"] != DBNull.Value)
					{
						dateTimePickerFromDate.Value = DateTime.Parse(dataRow["FromDate"].ToString());
					}
					if (dataRow["ToDate"] != DBNull.Value)
					{
						dateTimePickerToDate.Value = DateTime.Parse(dataRow["ToDate"].ToString());
					}
					if (dataRow["Month"] != DBNull.Value)
					{
						comboBoxMonth.SelectedID = checked(Convert.ToInt16(dataRow["Month"]) - 1);
					}
					else
					{
						comboBoxMonth.Clear();
					}
					string a = dataRow["TargetType"].ToString().TrimEnd();
					if (a == "A")
					{
						radioButtonAmount.Checked = true;
					}
					else if (a == "Q")
					{
						radioButtonCommission.Checked = true;
					}
					else
					{
						radioButtonAmount.Checked = true;
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Sales_Man_Target_Detail") && currentData.SalesManTargetDetailTable.Rows.Count != 0)
					{
						dataGridItems.BeginUpdate();
						foreach (DataRow row in currentData.Tables["Sales_Man_Target_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							if (row["SalesManGroupID"] != DBNull.Value)
							{
								dataRow3["Sales Man Group ID"] = row["SalesManGroupID"].ToString();
							}
							else
							{
								dataRow3["Sales Man Group ID"] = DBNull.Value;
							}
							if (row["SalesManID"] != DBNull.Value)
							{
								dataRow3["Sales Man ID"] = row["SalesManID"];
							}
							if (row["ProductClassID"] != DBNull.Value)
							{
								dataRow3["Product Class ID"] = row["ProductClassID"];
							}
							if (row["ProductCategoryID"] != DBNull.Value)
							{
								dataRow3["Product Category ID"] = row["ProductCategoryID"];
							}
							if (row["ProductID"] != DBNull.Value)
							{
								dataRow3["Product ID"] = row["ProductID"].ToString();
							}
							else
							{
								dataRow3["Product ID"] = DBNull.Value;
							}
							if (row["Amount"] != DBNull.Value)
							{
								dataRow3["Amount"] = row["Amount"].ToString();
							}
							else
							{
								dataRow3["Amount"] = DBNull.Value;
							}
							if (row["CommissionPercent"] != DBNull.Value)
							{
								dataRow3["Commission %"] = row["CommissionPercent"];
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

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Sales Man Group ID");
				dataTable.Columns.Add("Sales Man ID");
				dataTable.Columns.Add("Product Class ID");
				dataTable.Columns.Add("Product ID");
				dataTable.Columns.Add("Product Category ID");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("Commission %", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man Group ID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man Group ID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man Group ID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man Group ID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man Group ID"].ValueList = salespersonGroupComboBox1;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man Group ID"].Header.Caption = "Sales Man Group ID";
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man ID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man ID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man ID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man ID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man ID"].ValueList = salespersonComboBox1;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sales Man ID"].Header.Caption = "Sales Man ID";
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Class ID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Class ID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Class ID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Class ID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Class ID"].ValueList = itemClassComboBox1;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Class ID"].Header.Caption = "Product Class ID";
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Category ID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Category ID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Category ID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Category ID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Category ID"].ValueList = productCategoryComboBox;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product Category ID"].Header.Caption = "Product Category ID";
				dataGridItems.DisplayLayout.Bands[0].Columns["Product ID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product ID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product ID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product ID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product ID"].ValueList = productComboBox1;
				dataGridItems.DisplayLayout.Bands[0].Columns["Product ID"].Header.Caption = "Product ID";
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = new decimal(-999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Commission %"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commission %"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commission %"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Commission %"].MinValue = new decimal(-999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Commission %"].MaxValue = new decimal(999999999999L);
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

		private void LoadData(bool _byVoucherId)
		{
			byVoucherId = _byVoucherId;
			if (byVoucherId)
			{
				LoadData(textBoxVoucherNumber.Text.Trim());
			}
			else
			{
				LoadData(comboBoxEmployee.SelectedID, int.Parse(comboBoxYear.SelectedItem.ToString()), comboBoxMonth.SelectedID);
			}
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == ""))
				{
					currentData = Factory.SalesManTargetSystem.GetSalesManTargetByID(SystemDocID, voucherID);
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

		public void LoadData(string employeeID, int year, int month)
		{
			try
			{
				if (!(employeeID.Trim() == "") && month != 0 && currentData != null)
				{
					FillData();
					IsNewRecord = false;
					formManager.ResetDirty();
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
				bool flag = Factory.SalesManTargetSystem.CreateSalesManTarget(currentData, !isNewRecord);
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
			formManager.ShowApprovalPanel(approvalTaskID, "Sales_Man_Target", "VoucherID");
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

		private bool ValidateData()
		{
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
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
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxEmployeeName.Clear();
				comboBoxMonth.Clear();
				dateTimePickerFromDate.Value = DateTime.Now;
				dateTimePickerToDate.Value = DateTime.Now;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				IsVoid = false;
				SetApprovalStatus();
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
				return Factory.SalesManTargetSystem.DeleteSalesManTarget(textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Sales_Man_Target", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Sales_Man_Target", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Sales_Man_Target", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Sales_Man_Target", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Sales_Man_Target", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				SetupGrid();
				dataGridItems.ApplyFormat();
				dateTimePickerFromDate.Value = DateTime.Now;
				comboBoxSysDoc.FilterByType(SysDocTypes.SalesTarget);
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
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Product Category" && dataGridItems.ActiveRow.Cells["Product ID"].Value.ToString() != "")
			{
				dataGridItems.ActiveRow.Cells["Product ID"].Value.ToString();
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
			if (Void(!IsVoid))
			{
				IsVoid = !IsVoid;
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				if (isVoid)
				{
					DialogResult dialogResult = ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid);
				}
				else
				{
					DialogResult dialogResult = ErrorHelper.QuestionMessageYesNo(UIMessages.WantToUnvoid);
				}
				_ = 7;
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.SalesTarget);
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
			return false;
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
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet salesManTargetToPrint = Factory.SalesManTargetSystem.GetSalesManTargetToPrint(selectedID, text);
					if (salesManTargetToPrint == null || salesManTargetToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(salesManTargetToPrint, selectedID, "Sales Man Target", SysDocTypes.SalesTarget, isPrint, showPrintDialog);
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
			return "Job Expense Issue";
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
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void linkLabelVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
		}

		private void buttonFillDetails_Click(object sender, EventArgs e)
		{
			dataGridItems.DisplayLayout.Bands[0].AddNew().Cells[0].Value = DateTime.Now;
			UltraGridRow ultraGridRow = dataGridItems.Rows[0];
			if (string.IsNullOrEmpty(ultraGridRow.Cells["OT Type"].Value.ToString()))
			{
				ErrorHelper.InformationMessage("Please select an Overtime Type.");
				return;
			}
			if (string.IsNullOrEmpty(ultraGridRow.Cells["OT Rate"].Value.ToString()))
			{
				ErrorHelper.InformationMessage("Please select an Overtime Rate.");
				return;
			}
			if (ultraGridRow.Cells["Hours"].Value.ToString() == "")
			{
				ultraGridRow.Cells["Hours"].Value = 0;
			}
			if (ultraGridRow.Cells["OT Hours"].Value.ToString() == "")
			{
				ultraGridRow.Cells["OT Hours"].Value = 0;
			}
			if (ultraGridRow.Cells["Date"].Value.ToString() == "")
			{
				ultraGridRow.Cells["Date"].Value = DateTime.Now;
			}
			int num = int.Parse(comboBoxMonth.SelectedID.ToString());
			int num2 = int.Parse(comboBoxYear.SelectedItem.ToString());
			if (num == -1)
			{
				ErrorHelper.InformationMessage("Please select month.");
				return;
			}
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			string text8 = "";
			string text9 = "";
			text6 = ultraGridRow.Cells["Hours"].Value.ToString();
			text = ultraGridRow.Cells["OT Hours"].Value.ToString();
			text2 = ultraGridRow.Cells["OT Rate"].Value.ToString();
			text3 = ultraGridRow.Cells["OT Type"].Value.ToString();
			text4 = ultraGridRow.Cells["Location"].Value.ToString();
			text5 = ultraGridRow.Cells["Job"].Value.ToString();
			text7 = ultraGridRow.Cells["CostCategoryID"].Value.ToString();
			text8 = ((ultraGridRow.Cells["From"].Value.ToString() == null || !(ultraGridRow.Cells["From"].Value.ToString() != string.Empty)) ? "" : ultraGridRow.Cells["From"].Value.ToString());
			text9 = ((ultraGridRow.Cells["To"].Value.ToString() == null || !(ultraGridRow.Cells["To"].Value.ToString() != string.Empty)) ? "" : ultraGridRow.Cells["To"].Value.ToString());
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (num == -1 || num2 == -1)
			{
				return;
			}
			int num3 = DateTime.DaysInMonth(num2, num);
			for (int i = 1; i <= num3; i = checked(i + 1))
			{
				DataRow dataRow = dataTable.NewRow();
				DateTime dateTime = new DateTime(num2, num, i);
				dateTime.ToString("dddd");
				dataRow["Date"] = dateTime;
				dataRow["Hours"] = text6;
				dataRow["OT Hours"] = text;
				dataRow["OT Type"] = text3;
				dataRow["OT Rate"] = text2;
				dataRow["Location"] = text4;
				dataRow["Job"] = text5;
				dataRow["CostCategoryID"] = text7;
				if (text8 != "")
				{
					dataRow["From"] = text8;
				}
				if (text9 != "")
				{
					dataRow["To"] = text9;
				}
				dataRow.EndEdit();
				dataTable.Rows.Add(dataRow);
			}
			dataTable.AcceptChanges();
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.SalesManTargetListFormObj);
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

		private void ultraFormattedLinkLabel5_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.SalesTarget);
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
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

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void toolStripButtonExcelImport_Click(object sender, EventArgs e)
		{
			ImportFromExcel(autoFill: true);
		}

		public DataSet ImportFromExcel(bool autoFill)
		{
			try
			{
				GridImportFromExcelForm gridImportFromExcelForm = new GridImportFromExcelForm();
				gridImportFromExcelForm.Grid = dataGridItems;
				if (gridImportFromExcelForm.ShowDialog() == DialogResult.OK)
				{
					if (autoFill)
					{
						DataSet importData = gridImportFromExcelForm.ImportData;
						DataTable dataTable = dataGridItems.DataSource as DataTable;
						if (dataTable != null)
						{
							dataTable.Rows.Clear();
							foreach (DataRow row in importData.Tables[0].Rows)
							{
								DataRow dataRow2 = dataTable.NewRow();
								foreach (DataColumn column in importData.Tables[0].Columns)
								{
									dataRow2[column.ColumnName] = row[column.ColumnName];
								}
								dataTable.Rows.Add(dataRow2);
							}
						}
					}
					return gridImportFromExcelForm.ImportData;
				}
				return null;
			}
			catch (Exception ex)
			{
				ErrorHelper.WarningMessage("An error occured during import.", "The error message is:", ex.Message);
				return null;
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Projects.SalesManTargetForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonExcelImport = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonApproval = new System.Windows.Forms.ToolStripButton();
			toolStripLabelApproval = new System.Windows.Forms.ToolStripLabel();
			toolStripSeparatorApproval = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			dateTimePickerFromDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			panelDetails = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			label2 = new System.Windows.Forms.Label();
			radioButtonCommission = new System.Windows.Forms.RadioButton();
			radioButtonAmount = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimePickerToDate = new System.Windows.Forms.DateTimePicker();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxMonth = new Micromind.DataControls.MonthComboBox();
			buttonFillDetails = new System.Windows.Forms.Button();
			comboBoxYear = new System.Windows.Forms.ComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			panelEmployee = new System.Windows.Forms.Panel();
			textBoxEmployeeName = new System.Windows.Forms.TextBox();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			employeeLinkLabel = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			driverComboBox1 = new Micromind.DataControls.DriverComboBox();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			comboBoxPayrolItem = new Micromind.DataControls.PayrollItemComboBox();
			comboBoxOverTime = new Micromind.DataControls.OverTimeComboBox();
			comboBoxGridEmployee = new Micromind.DataControls.EmployeeComboBox();
			comboBoxLocation = new Micromind.DataControls.WorkLocationComboBox();
			jobComboBox1 = new Micromind.DataControls.JobComboBox();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			salespersonComboBox1 = new Micromind.DataControls.SalespersonComboBox();
			salespersonGroupComboBox1 = new Micromind.DataControls.SalespersonGroupComboBox();
			itemClassComboBox1 = new Micromind.DataControls.ItemClassComboBox();
			productComboBox1 = new Micromind.DataControls.ProductComboBox();
			adjustmentTypeComboBox1 = new Micromind.DataControls.AdjustmentTypeComboBox();
			productCategoryComboBox = new Micromind.DataControls.ProductCategoryComboBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			panelEmployee.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)driverComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrolItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOverTime).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)jobComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)salespersonComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)salespersonGroupComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)itemClassComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)productComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)adjustmentTypeComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)productCategoryComboBox).BeginInit();
			SuspendLayout();
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
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonExcelImport,
				toolStripSeparator3,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval,
				toolStripButtonInformation,
				toolStripDropDownButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(879, 31);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				copyToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			copyToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			copyToolStripMenuItem.Text = "Duplicate";
			copyToolStripMenuItem.Click += new System.EventHandler(copyToolStripMenuItem_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 488);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(879, 40);
			panelButtons.TabIndex = 2;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(42, 24);
			buttonVoid.TabIndex = 12;
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
			buttonDelete.TabIndex = 11;
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
			buttonNew.TabIndex = 10;
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
			linePanelDown.Size = new System.Drawing.Size(879, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(769, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 13;
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
			buttonSave.TabIndex = 9;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraCalcManager1.ContainingControl = this;
			dateTimePickerFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerFromDate.Location = new System.Drawing.Point(335, 28);
			dateTimePickerFromDate.Name = "dateTimePickerFromDate";
			dateTimePickerFromDate.Size = new System.Drawing.Size(94, 20);
			dateTimePickerFromDate.TabIndex = 3;
			textBoxVoucherNumber.Location = new System.Drawing.Point(335, 4);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 20);
			textBoxVoucherNumber.TabIndex = 1;
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(mmLabel3);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(radioButtonCommission);
			panelDetails.Controls.Add(radioButtonAmount);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(dateTimePickerToDate);
			panelDetails.Controls.Add(mmLabel4);
			panelDetails.Controls.Add(dateTimePickerFromDate);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(linkLabelVoucherNumber);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(comboBoxMonth);
			panelDetails.Location = new System.Drawing.Point(0, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(785, 53);
			panelDetails.TabIndex = 0;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(609, 7);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(37, 13);
			label3.TabIndex = 146;
			label3.Text = "Month";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(230, 31);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(47, 13);
			mmLabel3.TabIndex = 145;
			mmLabel3.Text = "Period:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(10, 32);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(41, 13);
			label2.TabIndex = 144;
			label2.Text = "Target:";
			radioButtonCommission.AutoSize = true;
			radioButtonCommission.Location = new System.Drawing.Point(140, 30);
			radioButtonCommission.Name = "radioButtonCommission";
			radioButtonCommission.Size = new System.Drawing.Size(64, 17);
			radioButtonCommission.TabIndex = 143;
			radioButtonCommission.Text = "Quantity";
			radioButtonCommission.UseVisualStyleBackColor = true;
			radioButtonAmount.AutoSize = true;
			radioButtonAmount.Checked = true;
			radioButtonAmount.Location = new System.Drawing.Point(65, 30);
			radioButtonAmount.Name = "radioButtonAmount";
			radioButtonAmount.Size = new System.Drawing.Size(61, 17);
			radioButtonAmount.TabIndex = 142;
			radioButtonAmount.TabStop = true;
			radioButtonAmount.Text = "Amount";
			radioButtonAmount.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(491, 7);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(37, 13);
			label1.TabIndex = 141;
			label1.Text = "Every:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(491, 32);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(26, 13);
			mmLabel2.TabIndex = 140;
			mmLabel2.Text = "To:";
			dateTimePickerToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerToDate.Location = new System.Drawing.Point(531, 29);
			dateTimePickerToDate.Name = "dateTimePickerToDate";
			dateTimePickerToDate.Size = new System.Drawing.Size(94, 20);
			dateTimePickerToDate.TabIndex = 4;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(282, 32);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(38, 13);
			mmLabel4.TabIndex = 130;
			mmLabel4.Text = "From:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(10, 7);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(44, 15);
			ultraFormattedLinkLabel5.TabIndex = 137;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked_1);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CalcManager = ultraCalcManager1;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance3;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(66, 4);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(139, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance15.FontData.BoldAsString = "True";
			appearance15.FontData.Name = "Tahoma";
			linkLabelVoucherNumber.Appearance = appearance15;
			linkLabelVoucherNumber.AutoSize = true;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(230, 7);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(100, 15);
			linkLabelVoucherNumber.TabIndex = 134;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Voucher Number:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			linkLabelVoucherNumber.VisitedLinkAppearance = appearance16;
			linkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVoucherNumber_LinkClicked);
			comboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMonth.FormattingEnabled = true;
			comboBoxMonth.IsMonthNumbers = true;
			comboBoxMonth.Items.AddRange(new object[6]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6"
			});
			comboBoxMonth.Location = new System.Drawing.Point(531, 4);
			comboBoxMonth.Name = "comboBoxMonth";
			comboBoxMonth.Size = new System.Drawing.Size(77, 21);
			comboBoxMonth.TabIndex = 2;
			buttonFillDetails.Enabled = false;
			buttonFillDetails.Location = new System.Drawing.Point(833, 64);
			buttonFillDetails.Name = "buttonFillDetails";
			buttonFillDetails.Size = new System.Drawing.Size(68, 23);
			buttonFillDetails.TabIndex = 135;
			buttonFillDetails.Text = "Fill Details";
			buttonFillDetails.UseVisualStyleBackColor = true;
			buttonFillDetails.Visible = false;
			buttonFillDetails.Click += new System.EventHandler(buttonFillDetails_Click);
			comboBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxYear.FormattingEnabled = true;
			comboBoxYear.Items.AddRange(new object[51]
			{
				"2000",
				"2001",
				"2002",
				"2003",
				"2004",
				"2005",
				"2006",
				"2007",
				"2008",
				"2009",
				"2010",
				"2011",
				"2012",
				"2013",
				"2014",
				"2015",
				"2016",
				"2017",
				"2018",
				"2019",
				"2020",
				"2021",
				"2022",
				"2023",
				"2024",
				"2025",
				"2026",
				"2027",
				"2028",
				"2029",
				"2030",
				"2031",
				"2032",
				"2033",
				"2034",
				"2035",
				"2036",
				"2037",
				"2038",
				"2039",
				"2040",
				"2041",
				"2042",
				"2043",
				"2044",
				"2045",
				"2046",
				"2047",
				"2048",
				"2049",
				"2050"
			});
			comboBoxYear.Location = new System.Drawing.Point(860, 37);
			comboBoxYear.Name = "comboBoxYear";
			comboBoxYear.Size = new System.Drawing.Size(10, 21);
			comboBoxYear.TabIndex = 7;
			comboBoxYear.Visible = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(791, 40);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(47, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Period:";
			mmLabel1.Visible = false;
			panelEmployee.Controls.Add(textBoxEmployeeName);
			panelEmployee.Controls.Add(comboBoxEmployee);
			panelEmployee.Location = new System.Drawing.Point(1013, 57);
			panelEmployee.Name = "panelEmployee";
			panelEmployee.Size = new System.Drawing.Size(86, 25);
			panelEmployee.TabIndex = 133;
			panelEmployee.Visible = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(233, 3);
			textBoxEmployeeName.MaxLength = 250;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(390, 20);
			textBoxEmployeeName.TabIndex = 126;
			textBoxEmployeeName.TabStop = false;
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CalcManager = ultraCalcManager1;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = textBoxEmployeeName;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance17;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(87, 3);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(139, 20);
			comboBoxEmployee.TabIndex = 5;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance29.FontData.BoldAsString = "True";
			appearance29.FontData.Name = "Tahoma";
			employeeLinkLabel.Appearance = appearance29;
			employeeLinkLabel.AutoSize = true;
			employeeLinkLabel.Location = new System.Drawing.Point(922, 66);
			employeeLinkLabel.Name = "employeeLinkLabel";
			employeeLinkLabel.Size = new System.Drawing.Size(62, 15);
			employeeLinkLabel.TabIndex = 129;
			employeeLinkLabel.TabStop = true;
			employeeLinkLabel.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			employeeLinkLabel.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			employeeLinkLabel.Value = "Employee:";
			employeeLinkLabel.Visible = false;
			appearance30.ForeColor = System.Drawing.Color.Blue;
			employeeLinkLabel.VisitedLinkAppearance = appearance30;
			employeeLinkLabel.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(employeeLinkLabel_LinkClicked);
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(12, 375);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(845, 62);
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
			driverComboBox1.Assigned = false;
			driverComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			driverComboBox1.CalcManager = ultraCalcManager1;
			driverComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			driverComboBox1.CustomReportFieldName = "";
			driverComboBox1.CustomReportKey = "";
			driverComboBox1.CustomReportValueType = 1;
			driverComboBox1.DescriptionTextBox = null;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			driverComboBox1.DisplayLayout.Appearance = appearance31;
			driverComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			driverComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			driverComboBox1.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			driverComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			driverComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			driverComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			driverComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			driverComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			driverComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			driverComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			driverComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			driverComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			driverComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			driverComboBox1.DisplayLayout.Override.CellAppearance = appearance38;
			driverComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			driverComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			driverComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			driverComboBox1.DisplayLayout.Override.HeaderAppearance = appearance40;
			driverComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			driverComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			driverComboBox1.DisplayLayout.Override.RowAppearance = appearance41;
			driverComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			driverComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			driverComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			driverComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			driverComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			driverComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			driverComboBox1.Editable = true;
			driverComboBox1.FilterString = "";
			driverComboBox1.HasAllAccount = false;
			driverComboBox1.HasCustom = false;
			driverComboBox1.IsDataLoaded = false;
			driverComboBox1.Location = new System.Drawing.Point(751, 247);
			driverComboBox1.MaxDropDownItems = 12;
			driverComboBox1.Name = "driverComboBox1";
			driverComboBox1.ShowInactiveItems = false;
			driverComboBox1.ShowQuickAdd = true;
			driverComboBox1.Size = new System.Drawing.Size(100, 20);
			driverComboBox1.TabIndex = 136;
			driverComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			driverComboBox1.Visible = false;
			comboBoxCostCategory.Assigned = false;
			comboBoxCostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCostCategory.CalcManager = ultraCalcManager1;
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
			comboBoxCostCategory.Location = new System.Drawing.Point(389, 248);
			comboBoxCostCategory.MaxDropDownItems = 12;
			comboBoxCostCategory.Name = "comboBoxCostCategory";
			comboBoxCostCategory.ShowInactiveItems = false;
			comboBoxCostCategory.ShowQuickAdd = true;
			comboBoxCostCategory.Size = new System.Drawing.Size(100, 20);
			comboBoxCostCategory.TabIndex = 130;
			comboBoxCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCostCategory.Visible = false;
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
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance43;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance44.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance44.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance44;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance45;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance46.BackColor2 = System.Drawing.SystemColors.Control;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance46;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance47;
			appearance48.BackColor = System.Drawing.SystemColors.Highlight;
			appearance48.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance48;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance49;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance50;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance51.BackColor = System.Drawing.SystemColors.Control;
			appearance51.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance51.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance51.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance51;
			appearance52.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance52;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance53;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance54;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(11, 89);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(846, 393);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CalcManager = ultraCalcManager1;
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
			comboBoxJob.Location = new System.Drawing.Point(508, 174);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(100, 20);
			comboBoxJob.TabIndex = 124;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJob.Visible = false;
			comboBoxPayrolItem.Assigned = false;
			comboBoxPayrolItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPayrolItem.CalcManager = ultraCalcManager1;
			comboBoxPayrolItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayrolItem.CustomReportFieldName = "";
			comboBoxPayrolItem.CustomReportKey = "";
			comboBoxPayrolItem.CustomReportValueType = 1;
			comboBoxPayrolItem.DescriptionTextBox = null;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayrolItem.DisplayLayout.Appearance = appearance55;
			comboBoxPayrolItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayrolItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance56.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.Appearance = appearance56;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance57;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance58.BackColor2 = System.Drawing.SystemColors.Control;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.PromptAppearance = appearance58;
			comboBoxPayrolItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayrolItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayrolItem.DisplayLayout.Override.ActiveCellAppearance = appearance59;
			appearance60.BackColor = System.Drawing.SystemColors.Highlight;
			appearance60.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayrolItem.DisplayLayout.Override.ActiveRowAppearance = appearance60;
			comboBoxPayrolItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayrolItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayrolItem.DisplayLayout.Override.CardAreaAppearance = appearance61;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			appearance62.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayrolItem.DisplayLayout.Override.CellAppearance = appearance62;
			comboBoxPayrolItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayrolItem.DisplayLayout.Override.CellPadding = 0;
			appearance63.BackColor = System.Drawing.SystemColors.Control;
			appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance63.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance63.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrolItem.DisplayLayout.Override.GroupByRowAppearance = appearance63;
			appearance64.TextHAlignAsString = "Left";
			comboBoxPayrolItem.DisplayLayout.Override.HeaderAppearance = appearance64;
			comboBoxPayrolItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayrolItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayrolItem.DisplayLayout.Override.RowAppearance = appearance65;
			comboBoxPayrolItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayrolItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance66;
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
			comboBoxPayrolItem.Location = new System.Drawing.Point(589, 247);
			comboBoxPayrolItem.MaxDropDownItems = 12;
			comboBoxPayrolItem.Name = "comboBoxPayrolItem";
			comboBoxPayrolItem.ShowInactiveItems = false;
			comboBoxPayrolItem.ShowQuickAdd = true;
			comboBoxPayrolItem.Size = new System.Drawing.Size(142, 20);
			comboBoxPayrolItem.TabIndex = 126;
			comboBoxPayrolItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPayrolItem.Visible = false;
			comboBoxOverTime.Assigned = false;
			comboBoxOverTime.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxOverTime.CalcManager = ultraCalcManager1;
			comboBoxOverTime.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxOverTime.CustomReportFieldName = "";
			comboBoxOverTime.CustomReportKey = "";
			comboBoxOverTime.CustomReportValueType = 1;
			comboBoxOverTime.DescriptionTextBox = null;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxOverTime.DisplayLayout.Appearance = appearance67;
			comboBoxOverTime.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxOverTime.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance68.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance68.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOverTime.DisplayLayout.GroupByBox.Appearance = appearance68;
			appearance69.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOverTime.DisplayLayout.GroupByBox.BandLabelAppearance = appearance69;
			comboBoxOverTime.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance70.BackColor2 = System.Drawing.SystemColors.Control;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance70.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOverTime.DisplayLayout.GroupByBox.PromptAppearance = appearance70;
			comboBoxOverTime.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxOverTime.DisplayLayout.MaxRowScrollRegions = 1;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxOverTime.DisplayLayout.Override.ActiveCellAppearance = appearance71;
			appearance72.BackColor = System.Drawing.SystemColors.Highlight;
			appearance72.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxOverTime.DisplayLayout.Override.ActiveRowAppearance = appearance72;
			comboBoxOverTime.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxOverTime.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			comboBoxOverTime.DisplayLayout.Override.CardAreaAppearance = appearance73;
			appearance74.BorderColor = System.Drawing.Color.Silver;
			appearance74.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxOverTime.DisplayLayout.Override.CellAppearance = appearance74;
			comboBoxOverTime.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxOverTime.DisplayLayout.Override.CellPadding = 0;
			appearance75.BackColor = System.Drawing.SystemColors.Control;
			appearance75.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance75.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance75.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOverTime.DisplayLayout.Override.GroupByRowAppearance = appearance75;
			appearance76.TextHAlignAsString = "Left";
			comboBoxOverTime.DisplayLayout.Override.HeaderAppearance = appearance76;
			comboBoxOverTime.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxOverTime.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.BorderColor = System.Drawing.Color.Silver;
			comboBoxOverTime.DisplayLayout.Override.RowAppearance = appearance77;
			comboBoxOverTime.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxOverTime.DisplayLayout.Override.TemplateAddRowAppearance = appearance78;
			comboBoxOverTime.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxOverTime.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxOverTime.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxOverTime.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxOverTime.Editable = true;
			comboBoxOverTime.FilterString = "";
			comboBoxOverTime.HasAllAccount = false;
			comboBoxOverTime.HasCustom = false;
			comboBoxOverTime.IsDataLoaded = false;
			comboBoxOverTime.Location = new System.Drawing.Point(640, 221);
			comboBoxOverTime.MaxDropDownItems = 12;
			comboBoxOverTime.Name = "comboBoxOverTime";
			comboBoxOverTime.ShowInactiveItems = false;
			comboBoxOverTime.ShowQuickAdd = true;
			comboBoxOverTime.Size = new System.Drawing.Size(91, 20);
			comboBoxOverTime.TabIndex = 127;
			comboBoxOverTime.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxOverTime.Visible = false;
			comboBoxGridEmployee.Assigned = false;
			comboBoxGridEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridEmployee.CalcManager = ultraCalcManager1;
			comboBoxGridEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridEmployee.CustomReportFieldName = "";
			comboBoxGridEmployee.CustomReportKey = "";
			comboBoxGridEmployee.CustomReportValueType = 1;
			comboBoxGridEmployee.DescriptionTextBox = null;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridEmployee.DisplayLayout.Appearance = appearance79;
			comboBoxGridEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance80.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance80.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance80.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.Appearance = appearance80;
			appearance81.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance81;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance82.BackColor2 = System.Drawing.SystemColors.Control;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance82.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance82;
			comboBoxGridEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance83;
			appearance84.BackColor = System.Drawing.SystemColors.Highlight;
			appearance84.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance84;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.CardAreaAppearance = appearance85;
			appearance86.BorderColor = System.Drawing.Color.Silver;
			appearance86.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridEmployee.DisplayLayout.Override.CellAppearance = appearance86;
			comboBoxGridEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance87.BackColor = System.Drawing.SystemColors.Control;
			appearance87.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance87.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance87.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance87.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance87;
			appearance88.TextHAlignAsString = "Left";
			comboBoxGridEmployee.DisplayLayout.Override.HeaderAppearance = appearance88;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridEmployee.DisplayLayout.Override.RowAppearance = appearance89;
			comboBoxGridEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance90;
			comboBoxGridEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridEmployee.Editable = true;
			comboBoxGridEmployee.FilterString = "";
			comboBoxGridEmployee.HasAllAccount = false;
			comboBoxGridEmployee.HasCustom = false;
			comboBoxGridEmployee.IsDataLoaded = false;
			comboBoxGridEmployee.Location = new System.Drawing.Point(592, 273);
			comboBoxGridEmployee.MaxDropDownItems = 12;
			comboBoxGridEmployee.Name = "comboBoxGridEmployee";
			comboBoxGridEmployee.ShowInactiveItems = false;
			comboBoxGridEmployee.ShowQuickAdd = true;
			comboBoxGridEmployee.ShowTerminatedEmployees = true;
			comboBoxGridEmployee.Size = new System.Drawing.Size(139, 20);
			comboBoxGridEmployee.TabIndex = 129;
			comboBoxGridEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridEmployee.Visible = false;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLocation.CalcManager = ultraCalcManager1;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance91;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance92.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance92.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance92;
			appearance93.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance93;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance94.BackColor2 = System.Drawing.SystemColors.Control;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance94.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance94;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance95;
			appearance96.BackColor = System.Drawing.SystemColors.Highlight;
			appearance96.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance96;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance97;
			appearance98.BorderColor = System.Drawing.Color.Silver;
			appearance98.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance98;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance99.BackColor = System.Drawing.SystemColors.Control;
			appearance99.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance99.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance99.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance99.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance99;
			appearance100.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance100;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance101;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance102;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(550, 375);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowAll = false;
			comboBoxLocation.ShowConsignIn = false;
			comboBoxLocation.ShowConsignOut = false;
			comboBoxLocation.ShowInactiveItems = false;
			comboBoxLocation.ShowNormalLocations = true;
			comboBoxLocation.ShowPOSOnly = false;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.ShowWarehouseOnly = false;
			comboBoxLocation.Size = new System.Drawing.Size(151, 20);
			comboBoxLocation.TabIndex = 15;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			jobComboBox1.Assigned = false;
			jobComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			jobComboBox1.CalcManager = ultraCalcManager1;
			jobComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			jobComboBox1.CustomReportFieldName = "";
			jobComboBox1.CustomReportKey = "";
			jobComboBox1.CustomReportValueType = 1;
			jobComboBox1.DescriptionTextBox = null;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			jobComboBox1.DisplayLayout.Appearance = appearance103;
			jobComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			jobComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance104.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance104.BorderColor = System.Drawing.SystemColors.Window;
			jobComboBox1.DisplayLayout.GroupByBox.Appearance = appearance104;
			appearance105.ForeColor = System.Drawing.SystemColors.GrayText;
			jobComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance105;
			jobComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance106.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance106.BackColor2 = System.Drawing.SystemColors.Control;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance106.ForeColor = System.Drawing.SystemColors.GrayText;
			jobComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance106;
			jobComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			jobComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.ForeColor = System.Drawing.SystemColors.ControlText;
			jobComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance107;
			appearance108.BackColor = System.Drawing.SystemColors.Highlight;
			appearance108.ForeColor = System.Drawing.SystemColors.HighlightText;
			jobComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance108;
			jobComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			jobComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			jobComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance109;
			appearance110.BorderColor = System.Drawing.Color.Silver;
			appearance110.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			jobComboBox1.DisplayLayout.Override.CellAppearance = appearance110;
			jobComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			jobComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance111.BackColor = System.Drawing.SystemColors.Control;
			appearance111.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance111.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance111.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance111.BorderColor = System.Drawing.SystemColors.Window;
			jobComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance111;
			appearance112.TextHAlignAsString = "Left";
			jobComboBox1.DisplayLayout.Override.HeaderAppearance = appearance112;
			jobComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			jobComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.BorderColor = System.Drawing.Color.Silver;
			jobComboBox1.DisplayLayout.Override.RowAppearance = appearance113;
			jobComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance114.BackColor = System.Drawing.SystemColors.ControlLight;
			jobComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance114;
			jobComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			jobComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			jobComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			jobComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			jobComboBox1.Editable = true;
			jobComboBox1.FilterString = "";
			jobComboBox1.HasAllAccount = false;
			jobComboBox1.HasCustom = false;
			jobComboBox1.IsDataLoaded = false;
			jobComboBox1.Location = new System.Drawing.Point(392, 375);
			jobComboBox1.MaxDropDownItems = 12;
			jobComboBox1.Name = "jobComboBox1";
			jobComboBox1.ShowInactiveItems = false;
			jobComboBox1.ShowQuickAdd = true;
			jobComboBox1.Size = new System.Drawing.Size(130, 20);
			jobComboBox1.TabIndex = 16;
			jobComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGridPrintDocument1.DocumentName = "Inventory Adjustment - List";
			ultraGridPrintDocument1.Grid = dataGridItems;
			salespersonComboBox1.Assigned = false;
			salespersonComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			salespersonComboBox1.CalcManager = ultraCalcManager1;
			salespersonComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			salespersonComboBox1.CustomReportFieldName = "";
			salespersonComboBox1.CustomReportKey = "";
			salespersonComboBox1.CustomReportValueType = 1;
			salespersonComboBox1.DescriptionTextBox = null;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			salespersonComboBox1.DisplayLayout.Appearance = appearance115;
			salespersonComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			salespersonComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance116.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance116.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance116.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance116.BorderColor = System.Drawing.SystemColors.Window;
			salespersonComboBox1.DisplayLayout.GroupByBox.Appearance = appearance116;
			appearance117.ForeColor = System.Drawing.SystemColors.GrayText;
			salespersonComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance117;
			salespersonComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance118.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance118.BackColor2 = System.Drawing.SystemColors.Control;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance118.ForeColor = System.Drawing.SystemColors.GrayText;
			salespersonComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance118;
			salespersonComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			salespersonComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.ForeColor = System.Drawing.SystemColors.ControlText;
			salespersonComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance119;
			appearance120.BackColor = System.Drawing.SystemColors.Highlight;
			appearance120.ForeColor = System.Drawing.SystemColors.HighlightText;
			salespersonComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance120;
			salespersonComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			salespersonComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			salespersonComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance121;
			appearance122.BorderColor = System.Drawing.Color.Silver;
			appearance122.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			salespersonComboBox1.DisplayLayout.Override.CellAppearance = appearance122;
			salespersonComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			salespersonComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance123.BackColor = System.Drawing.SystemColors.Control;
			appearance123.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance123.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance123.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance123.BorderColor = System.Drawing.SystemColors.Window;
			salespersonComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance123;
			appearance124.TextHAlignAsString = "Left";
			salespersonComboBox1.DisplayLayout.Override.HeaderAppearance = appearance124;
			salespersonComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			salespersonComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.BorderColor = System.Drawing.Color.Silver;
			salespersonComboBox1.DisplayLayout.Override.RowAppearance = appearance125;
			salespersonComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance126.BackColor = System.Drawing.SystemColors.ControlLight;
			salespersonComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance126;
			salespersonComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			salespersonComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			salespersonComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			salespersonComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			salespersonComboBox1.Editable = true;
			salespersonComboBox1.FilterString = "";
			salespersonComboBox1.HasAllAccount = false;
			salespersonComboBox1.HasCustom = false;
			salespersonComboBox1.IsDataLoaded = false;
			salespersonComboBox1.Location = new System.Drawing.Point(388, 330);
			salespersonComboBox1.MaxDropDownItems = 12;
			salespersonComboBox1.Name = "salespersonComboBox1";
			salespersonComboBox1.ShowInactiveItems = false;
			salespersonComboBox1.ShowQuickAdd = true;
			salespersonComboBox1.Size = new System.Drawing.Size(100, 20);
			salespersonComboBox1.TabIndex = 137;
			salespersonComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			salespersonComboBox1.Visible = false;
			salespersonGroupComboBox1.Assigned = false;
			salespersonGroupComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			salespersonGroupComboBox1.CalcManager = ultraCalcManager1;
			salespersonGroupComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			salespersonGroupComboBox1.CustomReportFieldName = "";
			salespersonGroupComboBox1.CustomReportKey = "";
			salespersonGroupComboBox1.CustomReportValueType = 1;
			salespersonGroupComboBox1.DescriptionTextBox = null;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			salespersonGroupComboBox1.DisplayLayout.Appearance = appearance127;
			salespersonGroupComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			salespersonGroupComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance128.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance128.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance128.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance128.BorderColor = System.Drawing.SystemColors.Window;
			salespersonGroupComboBox1.DisplayLayout.GroupByBox.Appearance = appearance128;
			appearance129.ForeColor = System.Drawing.SystemColors.GrayText;
			salespersonGroupComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance129;
			salespersonGroupComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance130.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance130.BackColor2 = System.Drawing.SystemColors.Control;
			appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance130.ForeColor = System.Drawing.SystemColors.GrayText;
			salespersonGroupComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance130;
			salespersonGroupComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			salespersonGroupComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.ForeColor = System.Drawing.SystemColors.ControlText;
			salespersonGroupComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance131;
			appearance132.BackColor = System.Drawing.SystemColors.Highlight;
			appearance132.ForeColor = System.Drawing.SystemColors.HighlightText;
			salespersonGroupComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance132;
			salespersonGroupComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			salespersonGroupComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			salespersonGroupComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance133;
			appearance134.BorderColor = System.Drawing.Color.Silver;
			appearance134.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			salespersonGroupComboBox1.DisplayLayout.Override.CellAppearance = appearance134;
			salespersonGroupComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			salespersonGroupComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance135.BackColor = System.Drawing.SystemColors.Control;
			appearance135.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance135.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance135.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance135.BorderColor = System.Drawing.SystemColors.Window;
			salespersonGroupComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance135;
			appearance136.TextHAlignAsString = "Left";
			salespersonGroupComboBox1.DisplayLayout.Override.HeaderAppearance = appearance136;
			salespersonGroupComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			salespersonGroupComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance137.BackColor = System.Drawing.SystemColors.Window;
			appearance137.BorderColor = System.Drawing.Color.Silver;
			salespersonGroupComboBox1.DisplayLayout.Override.RowAppearance = appearance137;
			salespersonGroupComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance138.BackColor = System.Drawing.SystemColors.ControlLight;
			salespersonGroupComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance138;
			salespersonGroupComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			salespersonGroupComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			salespersonGroupComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			salespersonGroupComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			salespersonGroupComboBox1.Editable = true;
			salespersonGroupComboBox1.FilterString = "";
			salespersonGroupComboBox1.HasAllAccount = false;
			salespersonGroupComboBox1.HasCustom = false;
			salespersonGroupComboBox1.IsDataLoaded = false;
			salespersonGroupComboBox1.Location = new System.Drawing.Point(602, 329);
			salespersonGroupComboBox1.MaxDropDownItems = 12;
			salespersonGroupComboBox1.Name = "salespersonGroupComboBox1";
			salespersonGroupComboBox1.ShowInactiveItems = false;
			salespersonGroupComboBox1.ShowQuickAdd = true;
			salespersonGroupComboBox1.Size = new System.Drawing.Size(100, 20);
			salespersonGroupComboBox1.TabIndex = 138;
			salespersonGroupComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			salespersonGroupComboBox1.Visible = false;
			itemClassComboBox1.Assigned = false;
			itemClassComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			itemClassComboBox1.CalcManager = ultraCalcManager1;
			itemClassComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			itemClassComboBox1.CustomReportFieldName = "";
			itemClassComboBox1.CustomReportKey = "";
			itemClassComboBox1.CustomReportValueType = 1;
			itemClassComboBox1.DescriptionTextBox = null;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			itemClassComboBox1.DisplayLayout.Appearance = appearance139;
			itemClassComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			itemClassComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance140.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance140.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance140.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance140.BorderColor = System.Drawing.SystemColors.Window;
			itemClassComboBox1.DisplayLayout.GroupByBox.Appearance = appearance140;
			appearance141.ForeColor = System.Drawing.SystemColors.GrayText;
			itemClassComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance141;
			itemClassComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance142.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance142.BackColor2 = System.Drawing.SystemColors.Control;
			appearance142.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance142.ForeColor = System.Drawing.SystemColors.GrayText;
			itemClassComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance142;
			itemClassComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			itemClassComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.ForeColor = System.Drawing.SystemColors.ControlText;
			itemClassComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance143;
			appearance144.BackColor = System.Drawing.SystemColors.Highlight;
			appearance144.ForeColor = System.Drawing.SystemColors.HighlightText;
			itemClassComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance144;
			itemClassComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			itemClassComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			itemClassComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance145;
			appearance146.BorderColor = System.Drawing.Color.Silver;
			appearance146.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			itemClassComboBox1.DisplayLayout.Override.CellAppearance = appearance146;
			itemClassComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			itemClassComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance147.BackColor = System.Drawing.SystemColors.Control;
			appearance147.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance147.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance147.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance147.BorderColor = System.Drawing.SystemColors.Window;
			itemClassComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance147;
			appearance148.TextHAlignAsString = "Left";
			itemClassComboBox1.DisplayLayout.Override.HeaderAppearance = appearance148;
			itemClassComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			itemClassComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			appearance149.BorderColor = System.Drawing.Color.Silver;
			itemClassComboBox1.DisplayLayout.Override.RowAppearance = appearance149;
			itemClassComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance150.BackColor = System.Drawing.SystemColors.ControlLight;
			itemClassComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance150;
			itemClassComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			itemClassComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			itemClassComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			itemClassComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			itemClassComboBox1.Editable = true;
			itemClassComboBox1.FilterString = "";
			itemClassComboBox1.HasAllAccount = false;
			itemClassComboBox1.HasCustom = false;
			itemClassComboBox1.IsDataLoaded = false;
			itemClassComboBox1.Location = new System.Drawing.Point(519, 201);
			itemClassComboBox1.MaxDropDownItems = 12;
			itemClassComboBox1.Name = "itemClassComboBox1";
			itemClassComboBox1.ShowInactiveItems = false;
			itemClassComboBox1.ShowQuickAdd = true;
			itemClassComboBox1.Size = new System.Drawing.Size(100, 20);
			itemClassComboBox1.TabIndex = 139;
			itemClassComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			itemClassComboBox1.Visible = false;
			productComboBox1.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			productComboBox1.Assigned = false;
			productComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			productComboBox1.CalcManager = ultraCalcManager1;
			productComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			productComboBox1.CustomReportFieldName = "";
			productComboBox1.CustomReportKey = "";
			productComboBox1.CustomReportValueType = 1;
			productComboBox1.DescriptionTextBox = null;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			appearance151.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			productComboBox1.DisplayLayout.Appearance = appearance151;
			productComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			productComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance152.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance152.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance152.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance152.BorderColor = System.Drawing.SystemColors.Window;
			productComboBox1.DisplayLayout.GroupByBox.Appearance = appearance152;
			appearance153.ForeColor = System.Drawing.SystemColors.GrayText;
			productComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance153;
			productComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance154.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance154.BackColor2 = System.Drawing.SystemColors.Control;
			appearance154.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance154.ForeColor = System.Drawing.SystemColors.GrayText;
			productComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance154;
			productComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			productComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance155.BackColor = System.Drawing.SystemColors.Window;
			appearance155.ForeColor = System.Drawing.SystemColors.ControlText;
			productComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance155;
			appearance156.BackColor = System.Drawing.SystemColors.Highlight;
			appearance156.ForeColor = System.Drawing.SystemColors.HighlightText;
			productComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance156;
			productComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			productComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			productComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance157;
			appearance158.BorderColor = System.Drawing.Color.Silver;
			appearance158.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			productComboBox1.DisplayLayout.Override.CellAppearance = appearance158;
			productComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			productComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance159.BackColor = System.Drawing.SystemColors.Control;
			appearance159.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance159.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance159.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance159.BorderColor = System.Drawing.SystemColors.Window;
			productComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance159;
			appearance160.TextHAlignAsString = "Left";
			productComboBox1.DisplayLayout.Override.HeaderAppearance = appearance160;
			productComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			productComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			appearance161.BorderColor = System.Drawing.Color.Silver;
			productComboBox1.DisplayLayout.Override.RowAppearance = appearance161;
			productComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance162.BackColor = System.Drawing.SystemColors.ControlLight;
			productComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance162;
			productComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			productComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			productComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			productComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			productComboBox1.Editable = true;
			productComboBox1.FilterCustomerID = "";
			productComboBox1.FilterString = "";
			productComboBox1.FilterSysDocID = "";
			productComboBox1.HasAllAccount = false;
			productComboBox1.HasCustom = false;
			productComboBox1.IsDataLoaded = false;
			productComboBox1.Location = new System.Drawing.Point(269, 173);
			productComboBox1.MaxDropDownItems = 12;
			productComboBox1.Name = "productComboBox1";
			productComboBox1.Show3PLItems = true;
			productComboBox1.ShowInactiveItems = false;
			productComboBox1.ShowQuickAdd = true;
			productComboBox1.Size = new System.Drawing.Size(100, 20);
			productComboBox1.TabIndex = 140;
			productComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			productComboBox1.Visible = false;
			adjustmentTypeComboBox1.Assigned = false;
			adjustmentTypeComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			adjustmentTypeComboBox1.CalcManager = ultraCalcManager1;
			adjustmentTypeComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			adjustmentTypeComboBox1.CustomReportFieldName = "";
			adjustmentTypeComboBox1.CustomReportKey = "";
			adjustmentTypeComboBox1.CustomReportValueType = 1;
			adjustmentTypeComboBox1.DescriptionTextBox = null;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			appearance163.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			adjustmentTypeComboBox1.DisplayLayout.Appearance = appearance163;
			adjustmentTypeComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			adjustmentTypeComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance164.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance164.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance164.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance164.BorderColor = System.Drawing.SystemColors.Window;
			adjustmentTypeComboBox1.DisplayLayout.GroupByBox.Appearance = appearance164;
			appearance165.ForeColor = System.Drawing.SystemColors.GrayText;
			adjustmentTypeComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance165;
			adjustmentTypeComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance166.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance166.BackColor2 = System.Drawing.SystemColors.Control;
			appearance166.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance166.ForeColor = System.Drawing.SystemColors.GrayText;
			adjustmentTypeComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance166;
			adjustmentTypeComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			adjustmentTypeComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			appearance167.ForeColor = System.Drawing.SystemColors.ControlText;
			adjustmentTypeComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance167;
			appearance168.BackColor = System.Drawing.SystemColors.Highlight;
			appearance168.ForeColor = System.Drawing.SystemColors.HighlightText;
			adjustmentTypeComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance168;
			adjustmentTypeComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			adjustmentTypeComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance169.BackColor = System.Drawing.SystemColors.Window;
			adjustmentTypeComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance169;
			appearance170.BorderColor = System.Drawing.Color.Silver;
			appearance170.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			adjustmentTypeComboBox1.DisplayLayout.Override.CellAppearance = appearance170;
			adjustmentTypeComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			adjustmentTypeComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance171.BackColor = System.Drawing.SystemColors.Control;
			appearance171.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance171.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance171.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance171.BorderColor = System.Drawing.SystemColors.Window;
			adjustmentTypeComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance171;
			appearance172.TextHAlignAsString = "Left";
			adjustmentTypeComboBox1.DisplayLayout.Override.HeaderAppearance = appearance172;
			adjustmentTypeComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			adjustmentTypeComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance173.BackColor = System.Drawing.SystemColors.Window;
			appearance173.BorderColor = System.Drawing.Color.Silver;
			adjustmentTypeComboBox1.DisplayLayout.Override.RowAppearance = appearance173;
			adjustmentTypeComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance174.BackColor = System.Drawing.SystemColors.ControlLight;
			adjustmentTypeComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance174;
			adjustmentTypeComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			adjustmentTypeComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			adjustmentTypeComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			adjustmentTypeComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			adjustmentTypeComboBox1.Editable = true;
			adjustmentTypeComboBox1.FilterString = "";
			adjustmentTypeComboBox1.HasAllAccount = false;
			adjustmentTypeComboBox1.HasCustom = false;
			adjustmentTypeComboBox1.IsDataLoaded = false;
			adjustmentTypeComboBox1.Location = new System.Drawing.Point(607, 154);
			adjustmentTypeComboBox1.MaxDropDownItems = 12;
			adjustmentTypeComboBox1.Name = "adjustmentTypeComboBox1";
			adjustmentTypeComboBox1.ShowInactiveItems = false;
			adjustmentTypeComboBox1.ShowNonSaleItems = false;
			adjustmentTypeComboBox1.ShowQuickAdd = true;
			adjustmentTypeComboBox1.Size = new System.Drawing.Size(100, 20);
			adjustmentTypeComboBox1.TabIndex = 141;
			adjustmentTypeComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			adjustmentTypeComboBox1.Visible = false;
			productCategoryComboBox.Assigned = false;
			productCategoryComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			productCategoryComboBox.CalcManager = ultraCalcManager1;
			productCategoryComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			productCategoryComboBox.CustomReportFieldName = "";
			productCategoryComboBox.CustomReportKey = "";
			productCategoryComboBox.CustomReportValueType = 1;
			productCategoryComboBox.DescriptionTextBox = null;
			appearance175.BackColor = System.Drawing.SystemColors.Window;
			appearance175.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			productCategoryComboBox.DisplayLayout.Appearance = appearance175;
			productCategoryComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			productCategoryComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance176.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance176.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance176.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance176.BorderColor = System.Drawing.SystemColors.Window;
			productCategoryComboBox.DisplayLayout.GroupByBox.Appearance = appearance176;
			appearance177.ForeColor = System.Drawing.SystemColors.GrayText;
			productCategoryComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance177;
			productCategoryComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance178.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance178.BackColor2 = System.Drawing.SystemColors.Control;
			appearance178.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance178.ForeColor = System.Drawing.SystemColors.GrayText;
			productCategoryComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance178;
			productCategoryComboBox.DisplayLayout.MaxColScrollRegions = 1;
			productCategoryComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance179.BackColor = System.Drawing.SystemColors.Window;
			appearance179.ForeColor = System.Drawing.SystemColors.ControlText;
			productCategoryComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance179;
			appearance180.BackColor = System.Drawing.SystemColors.Highlight;
			appearance180.ForeColor = System.Drawing.SystemColors.HighlightText;
			productCategoryComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance180;
			productCategoryComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			productCategoryComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance181.BackColor = System.Drawing.SystemColors.Window;
			productCategoryComboBox.DisplayLayout.Override.CardAreaAppearance = appearance181;
			appearance182.BorderColor = System.Drawing.Color.Silver;
			appearance182.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			productCategoryComboBox.DisplayLayout.Override.CellAppearance = appearance182;
			productCategoryComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			productCategoryComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance183.BackColor = System.Drawing.SystemColors.Control;
			appearance183.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance183.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance183.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance183.BorderColor = System.Drawing.SystemColors.Window;
			productCategoryComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance183;
			appearance184.TextHAlignAsString = "Left";
			productCategoryComboBox.DisplayLayout.Override.HeaderAppearance = appearance184;
			productCategoryComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			productCategoryComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance185.BackColor = System.Drawing.SystemColors.Window;
			appearance185.BorderColor = System.Drawing.Color.Silver;
			productCategoryComboBox.DisplayLayout.Override.RowAppearance = appearance185;
			productCategoryComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance186.BackColor = System.Drawing.SystemColors.ControlLight;
			productCategoryComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance186;
			productCategoryComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			productCategoryComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			productCategoryComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			productCategoryComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			productCategoryComboBox.Editable = true;
			productCategoryComboBox.FilterString = "";
			productCategoryComboBox.HasAllAccount = false;
			productCategoryComboBox.HasCustom = false;
			productCategoryComboBox.IsDataLoaded = false;
			productCategoryComboBox.Location = new System.Drawing.Point(412, 139);
			productCategoryComboBox.MaxDropDownItems = 12;
			productCategoryComboBox.Name = "productCategoryComboBox";
			productCategoryComboBox.ShowInactiveItems = false;
			productCategoryComboBox.Size = new System.Drawing.Size(100, 20);
			productCategoryComboBox.TabIndex = 142;
			productCategoryComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			productCategoryComboBox.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(879, 528);
			base.Controls.Add(productCategoryComboBox);
			base.Controls.Add(adjustmentTypeComboBox1);
			base.Controls.Add(productComboBox1);
			base.Controls.Add(itemClassComboBox1);
			base.Controls.Add(salespersonGroupComboBox1);
			base.Controls.Add(salespersonComboBox1);
			base.Controls.Add(employeeLinkLabel);
			base.Controls.Add(driverComboBox1);
			base.Controls.Add(comboBoxCostCategory);
			base.Controls.Add(buttonFillDetails);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(comboBoxYear);
			base.Controls.Add(panelEmployee);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxJob);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxPayrolItem);
			base.Controls.Add(comboBoxOverTime);
			base.Controls.Add(comboBoxGridEmployee);
			base.Controls.Add(comboBoxLocation);
			base.Controls.Add(jobComboBox1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(895, 555);
			base.Name = "SalesManTargetForm";
			Text = "Sales Man Target";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			panelEmployee.ResumeLayout(false);
			panelEmployee.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)driverComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrolItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOverTime).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)jobComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)salespersonComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)salespersonGroupComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)itemClassComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)productComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)adjustmentTypeComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)productCategoryComboBox).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
