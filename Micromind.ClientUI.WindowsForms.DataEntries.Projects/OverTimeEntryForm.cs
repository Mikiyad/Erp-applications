using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinMaskedEdit;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Projects
{
	public class OverTimeEntryForm : Form, IForm, IWorkFlowForm
	{
		private bool supressExpenseMessage;

		private int totalWorkingHours = CompanyPreferences.TotalWorkingDayHours;

		private int offday = CompanyPreferences.OffDay;

		private OverTimeEntryData currentData;

		private const string TABLENAME_CONST = "OverTimeEntry";

		private const string TABLENAMEDETAIL_CONST = "OverTimeEntry_Detail";

		private const string IDFIELD_CONST = "VoucherID";

		private const string EMPLOYEEIDFIELD_CONST = "EmployeeID";

		private const string YEARFIELD_CONST = "Year";

		private const string MONTHFIELD_CONST = "Month";

		private const string WORKDATE_CONST = "WorkDate";

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

		private DateTimePicker dateTimePickerDate;

		private TextBox textBoxVoucherNumber;

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

		private JobComboBox comboBoxJob;

		private EmployeeComboBox comboBoxEmployee;

		private TextBox textBoxEmployeeName;

		private UltraFormattedLinkLabel employeeLinkLabel;

		private ComboBox comboBoxYear;

		private MonthComboBox comboBoxMonth;

		private MMLabel mmLabel4;

		private PayrollItemComboBox comboBoxPayrolItem;

		private OverTimeComboBox comboBoxOverTime;

		private Panel panelApproval;

		private CheckBox checkBoxApprove;

		private EmployeeComboBox comboBoxGridEmployee;

		private CheckBox checkBoxDatewise;

		private Panel panelDate;

		private Panel panelEmployee;

		private Panel panelApprovalDetail;

		private Label label2;

		private DateTimePicker dateTimeApprovalDate;

		private Label label1;

		private TextBox textBoxApprovedBy;

		private TextBox textBoxCreatedBy;

		private Label label5;

		private DateTimePicker dateTimeCreatedDate;

		private Label label4;

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

		private DateTimePicker dateTimePickerFromTime;

		private DateTimePicker dateTimePickerToTime;

		public ScreenAreas ScreenArea => ScreenAreas.Project;

		public int ScreenID => 4002;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		private string VoucherID => textBoxVoucherNumber.Text.Trim();

		private DateTime Nextschedule
		{
			get;
			set;
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
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = true);
					sysDocComboBox.Enabled = enabled;
					panelApproval.Visible = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton2.Enabled = enabled;
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = false);
					sysDocComboBox2.Enabled = enabled;
					panelApproval.Visible = true;
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

		public OverTimeEntryForm()
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
				comboBoxMonth.LoadData();
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
			dataGridItems.BeforeEnterEditMode += dataGridItems_BeforeEnterEditMode;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxEmployee.SelectedIndexChanged += comboBoxEmployee_SelectedIndexChanged;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			base.FormClosing += ItemGroupDetailsForm_FormClosing;
			dateTimePickerFromTime.Leave += dateTimePickerFromTime_Leave;
			dateTimePickerToTime.Leave += dateTimePickerToTime_Leave;
			dateTimePickerFromTime.ValueChanged += dateTimePickerFromTime_ValueChanged;
			dateTimePickerToTime.ValueChanged += dateTimePickerToTime_ValueChanged;
			dataGridItems.BeforeRowActivate += dataGridItems_BeforeRowActivate;
			checkBoxApprove.CheckedChanged += checkBoxApprove_CheckedChanged;
			comboBoxMonth.SelectedIndexChanged += comboBoxMonth_SelectedIndexChanged;
		}

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				row.Cells["EmpNo"].Value = comboBoxEmployee.SelectedID;
				row.Cells["EmpName"].Value = comboBoxEmployee.SelectedName;
			}
		}

		private void dateTimePickerFromTime_Leave(object sender, EventArgs e)
		{
			dateTimePickerFromTime.Visible = false;
		}

		private void dateTimePickerFromTime_ValueChanged(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				dataGridItems.ActiveRow.Cells["From"].Value = dateTimePickerFromTime.Value.ToShortTimeString();
			}
		}

		private void dateTimePickerToTime_ValueChanged(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				dataGridItems.ActiveRow.Cells["To"].Value = dateTimePickerToTime.Value.ToShortTimeString();
			}
		}

		private void dateTimePickerToTime_Leave(object sender, EventArgs e)
		{
			dateTimePickerToTime.Visible = false;
		}

		private void dataGridItems_BeforeEnterEditMode(object sender, CancelEventArgs e)
		{
			UltraGridCell activeCell = dataGridItems.ActiveCell;
			if (activeCell == null)
			{
				return;
			}
			CellUIElement cellUIElement = (CellUIElement)activeCell.GetUIElement(dataGridItems.ActiveRowScrollRegion, dataGridItems.ActiveColScrollRegion);
			if (cellUIElement == null)
			{
				return;
			}
			checked
			{
				int x = cellUIElement.RectInsideBorders.Location.X + dataGridItems.Location.X;
				int y = cellUIElement.RectInsideBorders.Location.Y + dataGridItems.Location.Y;
				int width = cellUIElement.RectInsideBorders.Width;
				int height = cellUIElement.RectInsideBorders.Height;
				if (activeCell.Column.Key == "From")
				{
					dateTimePickerFromTime.SetBounds(x, y, width, height);
					dateTimePickerFromTime.Visible = true;
					dateTimePickerFromTime.Focus();
					dateTimePickerFromTime.BringToFront();
					if (e != null)
					{
						e.Cancel = true;
					}
				}
				else if (activeCell.Column.Key == "To")
				{
					dateTimePickerToTime.SetBounds(x, y, width, height);
					dateTimePickerToTime.Visible = true;
					dateTimePickerToTime.Focus();
					dateTimePickerToTime.BringToFront();
					if (e != null)
					{
						e.Cancel = true;
					}
				}
			}
		}

		private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
		{
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
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				row.Cells["Date"].Value = dateTimePickerDate.Value.ToShortDateString();
			}
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
			try
			{
				if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow != null)
				{
					string key = e.Cell.Column.Key;
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["OT Hours"].Value.ToString(), out result);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["OT Rate"].Value.ToString(), out result2);
					decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
					if (key == "Date" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Date" && !string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["Date"].Value.ToString()) && !string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["EmpName"].Value.ToString()))
					{
						TimeScheduleCalculations();
					}
					if (key == "EmpNo" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "EmpNo")
					{
						dataGridItems.ActiveRow.Cells["EmpName"].Value = comboBoxGridEmployee.SelectedName;
						if (!string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["Date"].Value.ToString()) && !string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["EmpName"].Value.ToString()))
						{
							TimeScheduleCalculations();
						}
					}
					if (key == "OT Type" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "OT Type")
					{
						dataGridItems.ActiveRow.Cells["OT Rate"].Value = comboBoxOverTime.SelectedRate;
						decimal.TryParse(dataGridItems.ActiveRow.Cells["OT Rate"].Value.ToString(), out result2);
						result3 = Math.Round(result * result2, Global.CurDecimalPoints);
						dataGridItems.ActiveRow.Cells["Amount"].Value = result3.ToString(Format.GridAmountFormat);
					}
					if (key == "OT Hours" || key == "OT Rate")
					{
						result3 = Math.Round(result * result2, Global.CurDecimalPoints);
						dataGridItems.ActiveRow.Cells["Amount"].Value = result3.ToString(Format.GridAmountFormat);
					}
					if ((key == "From" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "From") || (key == "To" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "To"))
					{
						if (!string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["Date"].Value.ToString()) && !string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["EmpName"].Value.ToString()))
						{
							TimeScheduleCalculations();
						}
						else
						{
							ErrorHelper.InformationMessage("Pleas Select Valid Employee And Date");
						}
					}
					if (key == "Hours" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Hours")
					{
						result = decimal.Parse(dataGridItems.ActiveRow.Cells["Hours"].Value.ToString());
						if (result - (decimal)totalWorkingHours <= 0m)
						{
							dataGridItems.ActiveRow.Cells["OT Hours"].Value = 0;
						}
						else
						{
							dataGridItems.ActiveRow.Cells["OT Hours"].Value = result - (decimal)totalWorkingHours;
						}
					}
					if (key == "Amount" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Amount")
					{
						if (result == 0m)
						{
							result = 1m;
							dataGridItems.ActiveRow.Cells["OT Hours"].Value = result;
						}
						result2 = Math.Round(result3 / result, 4);
						dataGridItems.ActiveRow.Cells["OT Rate"].Value = Math.Abs(result2);
					}
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
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (IsNewRecord && !CompanyPreferences.UseOTBasedon)
			{
				if (string.IsNullOrWhiteSpace(comboBoxEmployee.SelectedID))
				{
					ErrorHelper.InformationMessage("Please Select Employee First");
					comboBoxEmployee.Focus();
					return;
				}
				if (dataGridItems.ActiveRow != null)
				{
					dataGridItems.ActiveRow.Cells["EmpNo"].Value = comboBoxEmployee.SelectedID;
					dataGridItems.ActiveRow.Cells["EmpName"].Value = comboBoxEmployee.SelectedName;
				}
				dateTimePickerToTime.Visible = false;
				dateTimePickerFromTime.Visible = false;
			}
			buttonFillDetails.Enabled = true;
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
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow == null)
			{
				return;
			}
			if (!checkBoxDatewise.Checked)
			{
				activeRow.Cells["EmpNo"].Value = comboBoxEmployee.Value;
			}
			if (string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["Date"].Value.ToString()))
			{
				ErrorHelper.InformationMessage("Pleas Select Date ");
				activeRow.Cells["Date"].Activate();
				return;
			}
			if (string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["EmpNo"].Value.ToString()))
			{
				ErrorHelper.InformationMessage("Pleas Select Employee ");
				activeRow.Cells["EmpNo"].Activate();
				return;
			}
			if (string.IsNullOrEmpty(activeRow.Cells["OT Type"].Value.ToString()))
			{
				ErrorHelper.InformationMessage("Please select an Overtime Type.");
				e.Cancel = true;
				activeRow.Cells["OT Type"].Activate();
				return;
			}
			if (!string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["From"].Value.ToString()) && string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["To"].Value.ToString()))
			{
				ErrorHelper.InformationMessage("Please Enter To Time");
				e.Cancel = true;
				activeRow.Cells["To"].Activate();
				return;
			}
			if (string.IsNullOrEmpty(activeRow.Cells["OT Rate"].Value.ToString()))
			{
				ErrorHelper.InformationMessage("Please select an Overtime Rate.");
				e.Cancel = true;
				activeRow.Cells["OT Rate"].Activate();
				return;
			}
			if (activeRow.Cells["OT Hours"].Value.ToString() == "")
			{
				activeRow.Cells["OT Hours"].Value = 0;
			}
			if (activeRow.Cells["Date"].Value.ToString() == "")
			{
				activeRow.Cells["Date"].Value = DateTime.Now;
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && e.Cell.Column.Key == "To")
			{
				DateTime dateTime = default(DateTime);
				DateTime result = dateTime = DateTime.Now;
				DateTime.TryParse(activeRow.Cells["From"].Value.ToString(), out result);
				DateTime.TryParse(e.NewValue.ToString(), out dateTime);
				if (dateTime < result)
				{
					ErrorHelper.InformationMessage("To-Time can not be less than From-Time");
					e.Cancel = true;
				}
			}
		}

		private bool IsValidTimeSchedule(UltraGridRow row)
		{
			DateTime toTime = default(DateTime);
			DateTime fromTime = toTime = DateTime.Now;
			DateTime.TryParse(row.Cells["From"].Value.ToString(), out fromTime);
			DateTime.TryParse(row.Cells["to"].Value.ToString(), out toTime);
			string date = Convert.ToDateTime(row.Cells["date"].Value).ToShortDateString();
			string empid = row.Cells["EmpNo"].Value.ToString();
			IEnumerable<UltraGridRow> source = from x in dataGridItems.Rows
				where Convert.ToDateTime(x.Cells["date"].Value).ToShortDateString() == date && x.Cells["EmpNo"].Value.ToString() == empid
				where x.Index != row.Index
				select x;
			bool flag = source.Any((UltraGridRow x) => string.IsNullOrWhiteSpace(x.Cells["From"].Value.ToString()) || string.IsNullOrWhiteSpace(x.Cells["to"].Value.ToString()));
			if (flag)
			{
				return !flag;
			}
			bool num = source.Any((UltraGridRow x) => DateTime.Parse(DateTime.Parse(x.Cells["from"].Value.ToString()).ToShortDateString()) < fromTime && DateTime.Parse(DateTime.Parse(x.Cells["to"].Value.ToString()).ToShortDateString()) > toTime);
			if (num)
			{
				Nextschedule = DateTime.Parse(source.Max((UltraGridRow x) => x.Cells["to"].Value.ToString()));
			}
			return num;
		}

		private bool IsValidTime(UltraGridRow row)
		{
			bool result = false;
			DateTime dateTime = default(DateTime);
			DateTime result2 = dateTime = DateTime.Now;
			DateTime.TryParse(row.Cells["From"].Value.ToString(), out result2);
			DateTime.TryParse(row.Cells["to"].Value.ToString(), out dateTime);
			if (dateTime < result2)
			{
				result = true;
			}
			return result;
		}

		private void TimeScheduleCalculations()
		{
			string date = Convert.ToDateTime(dataGridItems.ActiveRow.Cells["date"].Value).ToShortDateString();
			string empid = dataGridItems.ActiveRow.Cells["EmpNo"].Value.ToString();
			if (dataGridItems.Rows.Where((UltraGridRow x) => Convert.ToDateTime(x.Cells["date"].Value).ToShortDateString() == date && x.Cells["EmpNo"].Value.ToString() == empid).Any((UltraGridRow x) => string.IsNullOrWhiteSpace(x.Cells["From"].Value.ToString()) || string.IsNullOrWhiteSpace(x.Cells["to"].Value.ToString())))
			{
				return;
			}
			if (IsValidTime(dataGridItems.ActiveRow))
			{
				dataGridItems.ActiveRow.Cells["to"].Value = DateTime.Parse(dataGridItems.ActiveRow.Cells["from"].Value.ToString()).AddMinutes(1.0);
			}
			if (IsValidTimeSchedule(dataGridItems.ActiveRow))
			{
				ErrorHelper.ErrorMessage("Enterd TimeSchedule Already Exsist , Pleas  Enter Valid Time Schedule");
				_ = dataGridItems.ActiveRow.Cells["from"].Value;
				dataGridItems.ActiveRow.Cells["to"].Value = Nextschedule.AddHours(1.0);
				dataGridItems.ActiveRow.Cells["from"].Value = Nextschedule;
			}
			decimal d = default(decimal);
			decimal num = default(decimal);
			DateTime dateTime = default(DateTime);
			DateTime d2 = dateTime = dateTimePickerDate.Value;
			IEnumerable<UltraGridRow> enumerable = dataGridItems.Rows.Where((UltraGridRow x) => Convert.ToDateTime(x.Cells["date"].Value).ToShortDateString() == date && x.Cells["EmpNo"].Value.ToString() == empid);
			foreach (UltraGridRow item in enumerable)
			{
				if (dataGridItems.Rows[item.Index].Cells["From"].Value != null && dataGridItems.Rows[item.Index].Cells["From"].Value.ToString() != string.Empty)
				{
					d2 = DateTime.Parse(dataGridItems.Rows[item.Index].Cells["From"].Value.ToString());
				}
				if (dataGridItems.Rows[item.Index].Cells["To"].Value != null && dataGridItems.Rows[item.Index].Cells["To"].Value.ToString() != string.Empty)
				{
					dateTime = DateTime.Parse(dataGridItems.Rows[item.Index].Cells["To"].Value.ToString());
				}
				TimeSpan timeSpan = dateTime - d2;
				d += decimal.Parse(timeSpan.ToString("hh\\:mm").Replace(':', '.').ToString());
				d += num;
				if (d - (decimal)totalWorkingHours <= 0m)
				{
					dataGridItems.Rows[item.Index].Cells["OT Hours"].Value = 0;
					dataGridItems.Rows[item.Index].Cells["Hours"].Value = timeSpan.ToString("hh\\:mm").Replace(':', '.').ToString();
				}
				else
				{
					num = d - (decimal)totalWorkingHours - num;
					if ((decimal)timeSpan.Hours - num < 0m)
					{
						dataGridItems.Rows[item.Index].Cells["Hours"].Value = 0;
						dataGridItems.Rows[item.Index].Cells["OT Hours"].Value = timeSpan.ToString("hh\\:mm").Replace(':', '.').ToString();
					}
					else
					{
						dataGridItems.Rows[item.Index].Cells["Hours"].Value = decimal.Parse(timeSpan.ToString("hh\\:mm").Replace(':', '.').ToString()) - num;
						dataGridItems.Rows[item.Index].Cells["OT Hours"].Value = num;
					}
				}
			}
			List<int> indexes = enumerable.Select((UltraGridRow x) => x.Index).ToList();
			UltraGridRow ultraGridRow = dataGridItems.Rows.Where((UltraGridRow x) => !indexes.Contains(x.Index)).FirstOrDefault();
			while (ultraGridRow != null)
			{
				date = Convert.ToDateTime(dataGridItems.Rows[ultraGridRow.Index].Cells["date"].Value).ToShortDateString();
				empid = dataGridItems.Rows[ultraGridRow.Index].Cells["EmpNo"].Value.ToString();
				d = default(decimal);
				num = default(decimal);
				enumerable = dataGridItems.Rows.Where((UltraGridRow x) => Convert.ToDateTime(x.Cells["date"].Value).ToShortDateString() == date && x.Cells["EmpNo"].Value.ToString() == empid);
				if (enumerable.Any((UltraGridRow x) => string.IsNullOrWhiteSpace(x.Cells["From"].Value.ToString()) || string.IsNullOrWhiteSpace(x.Cells["to"].Value.ToString())))
				{
					indexes.AddRange(enumerable.Select((UltraGridRow x) => x.Index).ToList());
					ultraGridRow = dataGridItems.Rows.Where((UltraGridRow x) => !indexes.Contains(x.Index)).FirstOrDefault();
				}
				else
				{
					foreach (UltraGridRow item2 in enumerable)
					{
						if (dataGridItems.Rows[item2.Index].Cells["From"].Value != null && dataGridItems.Rows[item2.Index].Cells["From"].Value.ToString() != string.Empty)
						{
							d2 = DateTime.Parse(dataGridItems.Rows[item2.Index].Cells["From"].Value.ToString());
						}
						if (dataGridItems.Rows[item2.Index].Cells["To"].Value != null && dataGridItems.Rows[item2.Index].Cells["To"].Value.ToString() != string.Empty)
						{
							dateTime = DateTime.Parse(dataGridItems.Rows[item2.Index].Cells["To"].Value.ToString());
						}
						TimeSpan timeSpan2 = dateTime - d2;
						d += decimal.Parse(timeSpan2.ToString("hh\\:mm").Replace(':', '.').ToString());
						d += num;
						if (d - (decimal)totalWorkingHours <= 0m)
						{
							dataGridItems.Rows[item2.Index].Cells["OT Hours"].Value = 0;
							dataGridItems.Rows[item2.Index].Cells["Hours"].Value = timeSpan2.ToString("hh\\:mm").Replace(':', '.').ToString();
						}
						else
						{
							num = d - (decimal)totalWorkingHours - num;
							if ((decimal)timeSpan2.Hours - num < 0m)
							{
								dataGridItems.Rows[item2.Index].Cells["Hours"].Value = 0;
								dataGridItems.Rows[item2.Index].Cells["OT Hours"].Value = timeSpan2.ToString("hh\\:mm").Replace(':', '.').ToString();
							}
							else
							{
								dataGridItems.Rows[item2.Index].Cells["Hours"].Value = decimal.Parse(timeSpan2.ToString("hh\\:mm").Replace(':', '.').ToString()) - num;
								dataGridItems.Rows[item2.Index].Cells["OT Hours"].Value = num;
							}
						}
					}
					indexes.AddRange(enumerable.Select((UltraGridRow x) => x.Index).ToList());
					ultraGridRow = dataGridItems.Rows.Where((UltraGridRow x) => !indexes.Contains(x.Index)).FirstOrDefault();
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
					currentData = new OverTimeEntryData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.OverTimeEntryTable.Rows[0] : currentData.OverTimeEntryTable.NewRow();
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
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Month"] = int.Parse(comboBoxMonth.SelectedID.ToString());
				dataRow["Year"] = int.Parse(comboBoxYear.SelectedItem.ToString());
				dataRow["Note"] = textBoxNote.Text.Trim();
				dataRow["DateCreated"] = DateTime.Now;
				dataRow["CreatedBy"] = Global.CurrentUser;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.OverTimeEntryTable.Rows.Add(dataRow);
				}
				currentData.OverTimeEntryDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.OverTimeEntryDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow2["PayrollPeriod"] = new DateTime(int.Parse(comboBoxYear.SelectedItem.ToString()), comboBoxMonth.SelectedID, 1);
					if (!checkBoxDatewise.Checked)
					{
						if (row.Cells["Date"].Value.ToString() != "")
						{
							dataRow2["WorkDate"] = DateTime.Parse(row.Cells["Date"].Value.ToString());
						}
						else
						{
							dataRow2["WorkDate"] = DBNull.Value;
						}
						dataRow2["EmployeeID"] = comboBoxEmployee.SelectedID;
						dataRow2["EmployeeName"] = comboBoxEmployee.SelectedName;
					}
					else
					{
						dataRow2["WorkDate"] = dateTimePickerDate.Value;
						dataRow2["EmployeeID"] = row.Cells["EmpNo"].Value.ToString();
						dataRow2["EmployeeName"] = row.Cells["EmpName"].Value.ToString();
					}
					if (row.Cells["Job"].Value != null && row.Cells["Job"].Value.ToString() != "")
					{
						dataRow2["JobID"] = row.Cells["Job"].Value.ToString();
					}
					else
					{
						dataRow2["JobID"] = DBNull.Value;
					}
					if (row.Cells["CostCategoryID"].Value != null && row.Cells["CostCategoryID"].Value.ToString() != "")
					{
						dataRow2["CostCategoryID"] = row.Cells["CostCategoryID"].Value.ToString();
					}
					else
					{
						dataRow2["CostCategoryID"] = DBNull.Value;
					}
					if (row.Cells["Location"].Value != null && row.Cells["Location"].Value.ToString() != "")
					{
						dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
					}
					else
					{
						dataRow2["LocationID"] = DBNull.Value;
					}
					if (row.Cells["From"].Value.ToString() != "")
					{
						dataRow2["FromTime"] = DateTime.Parse(row.Cells["From"].Value.ToString()).ToShortTimeString();
					}
					else
					{
						dataRow2["FromTime"] = DBNull.Value;
					}
					if (row.Cells["To"].Value.ToString() != "")
					{
						dataRow2["ToTime"] = DateTime.Parse(row.Cells["To"].Value.ToString()).ToShortTimeString();
					}
					else
					{
						dataRow2["ToTime"] = DBNull.Value;
					}
					if (row.Cells["Hours"].Value.ToString() != "")
					{
						dataRow2["Hours"] = decimal.Parse(row.Cells["Hours"].Value.ToString());
					}
					else
					{
						dataRow2["Hours"] = 0;
					}
					if (row.Cells["OT Hours"].Value.ToString() != "")
					{
						dataRow2["OTHours"] = decimal.Parse(row.Cells["OT Hours"].Value.ToString());
					}
					else
					{
						dataRow2["OTHours"] = 0;
					}
					dataRow2["OTType"] = row.Cells["OT Type"].Value.ToString();
					if (row.Cells["OT Rate"].Value.ToString() != "")
					{
						dataRow2["OTRate"] = decimal.Parse(row.Cells["OT Rate"].Value.ToString());
					}
					else
					{
						dataRow2["OTRate"] = DBNull.Value;
					}
					if (row.Cells["Amount"].Value.ToString() != "")
					{
						dataRow2["Amount"] = decimal.Parse(row.Cells["Amount"].Value.ToString());
					}
					else
					{
						dataRow2["Amount"] = DBNull.Value;
					}
					if (row.Cells["LeaveDays"].Value.ToString() != "")
					{
						dataRow2["LeaveDays"] = int.Parse(row.Cells["LeaveDays"].Value.ToString());
					}
					else
					{
						dataRow2["LeaveDays"] = 0;
					}
					dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.OverTimeEntryDetailTable.Rows.Add(dataRow2);
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
					DataRow dataRow = currentData.Tables["OverTimeEntry"].Rows[0];
					DataRow dataRow2 = currentData.Tables["OverTimeEntry_Detail"].Rows[0];
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					if (dataRow["Month"] != DBNull.Value)
					{
						comboBoxMonth.SelectedID = int.Parse(dataRow["Month"].ToString());
					}
					else
					{
						comboBoxMonth.Clear();
					}
					if (dataRow["Year"] != DBNull.Value)
					{
						comboBoxYear.SelectedItem = dataRow["Year"].ToString();
					}
					else
					{
						comboBoxYear.SelectedItem = null;
					}
					if (dataRow["ApprovalDate"] != null && dataRow["ApprovalDate"].ToString() != string.Empty && dataRow["ApprovedBy"] != null && dataRow["ApprovedBy"].ToString() != string.Empty)
					{
						checkBoxApprove.Checked = true;
					}
					else
					{
						checkBoxApprove.Checked = false;
					}
					comboBoxEmployee.SelectedID = dataRow2["EmployeeID"].ToString();
					dateTimeCreatedDate.Value = DateTime.Parse(dataRow["DateCreated"].ToString());
					if (dataRow["ApprovalDate"] != DBNull.Value)
					{
						dateTimeApprovalDate.Value = DateTime.Parse(dataRow["ApprovalDate"].ToString());
					}
					textBoxApprovedBy.Text = dataRow["ApprovedBy"].ToString();
					textBoxCreatedBy.Text = dataRow["CreatedBy"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("OverTimeEntry_Detail") && currentData.OverTimeEntryDetailTable.Rows.Count != 0)
					{
						dataGridItems.BeginUpdate();
						foreach (DataRow row in currentData.Tables["OverTimeEntry_Detail"].Rows)
						{
							DataRow dataRow4 = dataTable.NewRow();
							if (row["WorkDate"] != DBNull.Value)
							{
								dataRow4["Date"] = DateTime.Parse(row["WorkDate"].ToString());
							}
							else
							{
								dataRow4["Date"] = DBNull.Value;
							}
							if (row["JobID"] != DBNull.Value)
							{
								dataRow4["Job"] = row["JobID"];
							}
							if (row["JobID"] != DBNull.Value)
							{
								dataRow4["CostCategoryID"] = row["CostCategoryID"];
							}
							if (row["LocationID"] != DBNull.Value)
							{
								dataRow4["Location"] = row["LocationID"];
							}
							if (row["FromTime"] != DBNull.Value)
							{
								dataRow4["From"] = DateTime.Parse(row["FromTime"].ToString());
							}
							else
							{
								dataRow4["From"] = DBNull.Value;
							}
							if (row["ToTime"] != DBNull.Value)
							{
								dataRow4["To"] = DateTime.Parse(row["ToTime"].ToString());
							}
							else
							{
								dataRow4["To"] = DBNull.Value;
							}
							if (row["WorkDate"] != DBNull.Value)
							{
								dateTimePickerDate.Value = DateTime.Parse(row["WorkDate"].ToString());
							}
							dataRow4["EmpNo"] = row["EmployeeID"];
							dataRow4["EmpName"] = row["EmployeeName"];
							dataRow4["OT Type"] = row["OTType"].ToString();
							if (row["Hours"] != DBNull.Value)
							{
								dataRow4["Hours"] = row["Hours"].ToString();
							}
							else
							{
								dataRow4["Hours"] = DBNull.Value;
							}
							if (row["OTHours"] != DBNull.Value)
							{
								dataRow4["OT Hours"] = row["OTHours"].ToString();
							}
							else
							{
								dataRow4["OT Hours"] = DBNull.Value;
							}
							if (row["OTRate"] != DBNull.Value)
							{
								dataRow4["OT Rate"] = decimal.Parse(row["OTRate"].ToString());
							}
							else
							{
								dataRow4["OT Rate"] = DBNull.Value;
							}
							if (row["Amount"] != DBNull.Value)
							{
								dataRow4["Amount"] = decimal.Parse(row["Amount"].ToString());
							}
							else
							{
								dataRow4["Amount"] = DBNull.Value;
							}
							if (row["LeaveDays"] != DBNull.Value)
							{
								dataRow4["LeaveDays"] = int.Parse(row["LeaveDays"].ToString());
							}
							else
							{
								dataRow4["LeaveDays"] = DBNull.Value;
							}
							dataRow4["Remarks"] = row["Remarks"];
							dataRow4.EndEdit();
							dataTable.Rows.Add(dataRow4);
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
				dataTable.Columns.Add("Date", typeof(DateTime));
				dataTable.Columns.Add("EmpNo");
				dataTable.Columns.Add("EmpName");
				dataTable.Columns.Add("Remarks");
				dataTable.Columns.Add("Job");
				dataTable.Columns.Add("CostCategoryID");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("From", typeof(DateTime));
				dataTable.Columns.Add("To", typeof(DateTime));
				dataTable.Columns.Add("Hours", typeof(decimal));
				dataTable.Columns.Add("LeaveDays", typeof(decimal));
				dataTable.Columns.Add("OT Hours", typeof(decimal));
				dataTable.Columns.Add("OT Type");
				dataTable.Columns.Add("OT Rate", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["LeaveDays"].Hidden = true;
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].ValueList = comboBoxGridEmployee;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"].Header.Caption = "Employee No";
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 3000;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].ValueList = comboBoxLocation;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Header.Caption = "Location";
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Type"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Type"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Type"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Type"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Type"].ValueList = comboBoxOverTime;
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Hours"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Hours"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Hours"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].Header.Caption = "Employee Name";
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Rate"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Rate"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Rate"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Rate"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Rate"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Rate"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"];
				bool hidden = dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].Hidden = true;
				ultraGridColumn.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ValueList = comboBoxJob;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].MaxLength = 30;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].ValueList = comboBoxCostCategory;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].Header.Caption = "Cost Category";
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Hidden = !useJobCosting;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategoryID"].Hidden = !useJobCosting;
				if (useJobCosting)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].Width = checked(30 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["OT Rate"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Amount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
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
					currentData = Factory.OverTimeEntrySystem.GetOverTimeEntryByID(SystemDocID, voucherID);
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
				if (!(employeeID.Trim() == "") && month != 0)
				{
					currentData = Factory.OverTimeEntrySystem.GetOverTimeEntry(employeeID, year, month);
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
				bool flag = Factory.OverTimeEntrySystem.CreateOverTimeEntry(currentData, !isNewRecord);
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
			formManager.ShowApprovalPanel(approvalTaskID, "OverTimeEntry", "VoucherID");
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
			if (comboBoxMonth.SelectedIndex == 0)
			{
				ErrorHelper.InformationMessage("Please select period month!");
				return false;
			}
			if (checkBoxDatewise.Checked)
			{
				for (int i = 0; i < dataGridItems.Rows.Count; i = checked(i + 1))
				{
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
					}
					else if (dataGridItems.Rows[i].Cells["EmpNo"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select an employee for all the rows.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
				}
			}
			else if (comboBoxEmployee.SelectedID == string.Empty)
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one item row.");
				return false;
			}
			if (dataGridItems.Rows.Any((UltraGridRow x) => string.IsNullOrEmpty(x.Cells["Ot Type"].Value.ToString())))
			{
				ErrorHelper.InformationMessage("Please Select Valid OT Type For All Records");
				return false;
			}
			if (dataGridItems.Rows.Any((UltraGridRow x) => string.IsNullOrEmpty(x.Cells["OT Rate"].Value.ToString())))
			{
				ErrorHelper.InformationMessage("Please Select Valid OT Type For All Records");
				return false;
			}
			if (dataGridItems.Rows.Any((UltraGridRow x) => string.IsNullOrEmpty(x.Cells["EmpName"].Value.ToString())))
			{
				ErrorHelper.InformationMessage("Please Select Valid Employee For All Records");
				return false;
			}
			if (IsNewRecord)
			{
				foreach (var item in dataGridItems.Rows.Select((UltraGridRow row) => new
				{
					EmpNo = row.Cells["EmpNo"].Value,
					Date = row.Cells["Date"].Value
				}).Distinct())
				{
					if (Factory.OverTimeEntrySystem.IsValidEntry(item.EmpNo.ToString(), DateTime.Parse(item.Date.ToString())))
					{
						ErrorHelper.InformationMessage($"OverTime already exist for the employee  {item.EmpNo}  on  {DateTime.Parse(item.Date.ToString()).ToShortDateString()}");
						return false;
					}
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
				textBoxEmployeeName.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				comboBoxMonth.SelectedID = 0;
				comboBoxYear.SelectedIndex = checked(DateTime.Today.Year - 2000);
				comboBoxEmployee.Clear();
				int month = DateTime.Today.Month;
				comboBoxMonth.SelectedID = month;
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
				return Factory.OverTimeEntrySystem.DeleteOverTimeEntry(textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("OverTimeEntry", "VoucherID", textBoxVoucherNumber.Text);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("OverTimeEntry", "VoucherID", textBoxVoucherNumber.Text);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("OverTimeEntry", "VoucherID");
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("OverTimeEntry", "VoucherID");
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("OverTimeEntry", "VoucherID", VoucherID, toolStripTextBoxFind.Text);
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
				dateTimePickerDate.Value = DateTime.Now;
				checkBoxDatewise.Visible = false;
				buttonFillDetails.Visible = !CompanyPreferences.UseOTBasedon;
				checkBoxDatewise.Checked = CompanyPreferences.UseOTBasedon;
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					comboBoxSysDoc.FilterByType(SysDocTypes.OverTimeEntry);
					dataGridItems.DisplayLayout.Bands[0].Columns["from"].Format = "H:mm";
					dataGridItems.DisplayLayout.Bands[0].Columns["from"].MaskDisplayMode = MaskMode.IncludeBoth;
					dataGridItems.DisplayLayout.Bands[0].Columns["to"].Format = "H:mm";
					dataGridItems.DisplayLayout.Bands[0].Columns["to"].MaskDisplayMode = MaskMode.IncludeBoth;
					dataGridItems.DisplayLayout.Bands[0].Columns["From"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.TimeWithSpin;
					dataGridItems.DisplayLayout.Bands[0].Columns["To"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.TimeWithSpin;
					dateTimePickerFromTime.Value = DateTime.Now;
					dateTimePickerToTime.Value = DateTime.Now;
					dateTimePickerFromTime.CustomFormat = "H:mm";
					dateTimePickerToTime.CustomFormat = "H:mm";
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
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null && string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["Date"].Value.ToString()))
			{
				dataGridItems.ActiveRow.Cells["Date"].Value = dateTimePickerDate.Value.ToShortDateString();
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
					currentData = (dataSet as OverTimeEntryData);
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
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet overTimeEntryToPrint = Factory.OverTimeEntrySystem.GetOverTimeEntryToPrint(selectedID, text);
					if (overTimeEntryToPrint == null || overTimeEntryToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(overTimeEntryToPrint, selectedID, "Overtime Entry", SysDocTypes.JobTimesheet, isPrint, showPrintDialog);
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
			Panel panel = panelDate;
			bool visible = dataGridItems.DisplayLayout.Bands[0].Columns["Date"].Hidden = checkBoxDatewise.Checked;
			panel.Visible = visible;
			Panel panel2 = panelEmployee;
			UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"];
			bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].Hidden = !checkBoxDatewise.Checked;
			visible = (ultraGridColumn.Hidden = flag2);
			panel2.Visible = visible;
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
			FormActivator.SelectedSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.BringFormToFront(FormActivator.OverTimeEntrytListFormObj);
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.OverTimeEntry);
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
		}

		private void toolStripButtonExcelImport_Click(object sender, EventArgs e)
		{
			try
			{
				ImportFromExcel(autoFill: true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public DataSet ImportFromExcel(bool autoFill)
		{
			try
			{
				GridImportFromExcelForm gridImportFromExcelForm = new GridImportFromExcelForm();
				if (!CompanyPreferences.UseOTBasedon)
				{
					UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"];
					bool hidden = dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].Hidden = false;
					ultraGridColumn.Hidden = hidden;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Date"].Hidden = false;
				}
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
						if (!importData.Tables[0].Columns.Contains("EmpName"))
						{
							importData.Tables[0].Columns.Add("EmpName");
						}
						if (!importData.Tables[0].Columns.Contains("Date"))
						{
							importData.Tables[0].Columns.Add("Date");
						}
						if (importData.Tables[0].Columns.Contains("OT Type") && !importData.Tables[0].Columns.Contains("OT Rate"))
						{
							importData.Tables[0].Columns.Add("OT Rate");
						}
						comboBoxGridEmployee.LoadData();
						comboBoxOverTime.LoadData();
						comboBoxCostCategory.LoadData();
						comboBoxJob.LoadData();
						comboBoxLocation.LoadData();
						foreach (DataRow row2 in dataTable.Rows)
						{
							comboBoxGridEmployee.SelectedID = row2["EmpNo"].ToString();
							row2["EmpName"] = comboBoxGridEmployee.SelectedName;
							comboBoxOverTime.SelectedID = row2["OT Type"].ToString();
							string.IsNullOrWhiteSpace(comboBoxGridEmployee.SelectedName);
							if (importData.Tables[0].Columns.Contains("CostCategoryID"))
							{
								if (!string.IsNullOrWhiteSpace(row2["CostCategoryID"].ToString()))
								{
									comboBoxCostCategory.SelectedID = row2["CostCategoryID"].ToString();
								}
								if (string.IsNullOrWhiteSpace(comboBoxCostCategory.SelectedName))
								{
									row2["CostCategoryID"] = "";
								}
							}
							if (importData.Tables[0].Columns.Contains("Job"))
							{
								if (!string.IsNullOrWhiteSpace(row2["Job"].ToString()))
								{
									comboBoxJob.SelectedID = row2["Job"].ToString();
								}
								if (string.IsNullOrWhiteSpace(comboBoxJob.SelectedName))
								{
									row2["Job"] = "";
								}
							}
							if (importData.Tables[0].Columns.Contains("Location"))
							{
								if (!string.IsNullOrWhiteSpace(row2["Location"].ToString()))
								{
									comboBoxLocation.SelectedID = row2["Location"].ToString();
								}
								if (string.IsNullOrWhiteSpace(comboBoxLocation.SelectedName))
								{
									row2["Location"] = "";
								}
							}
							if (string.IsNullOrWhiteSpace(comboBoxOverTime.SelectedName))
							{
								row2["OT Type"] = "";
							}
							else
							{
								decimal result = default(decimal);
								decimal result2 = default(decimal);
								decimal.TryParse(comboBoxOverTime.SelectedRate, out result);
								decimal.TryParse(row2["Hours"].ToString(), out result2);
								row2["Amount"] = Math.Round(result2 * result, Global.CurDecimalPoints).ToString(Format.GridAmountFormat);
								row2["OT Rate"] = comboBoxOverTime.SelectedRate;
							}
						}
						if (!CompanyPreferences.UseOTBasedon)
						{
							comboBoxEmployee.LoadData();
							string text = (from x in dataTable.AsEnumerable()
								select x.Field<string>("EmpNo")).FirstOrDefault();
							comboBoxEmployee.SelectedID = ((text == null) ? comboBoxEmployee.SelectedID : text);
						}
						else
						{
							DateTime value = (from x in dataTable.AsEnumerable()
								select x.Field<DateTime>("Date")).FirstOrDefault();
							dateTimePickerDate.Value = value;
						}
					}
					if (!CompanyPreferences.UseOTBasedon)
					{
						UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["EmpNo"];
						bool hidden = dataGridItems.DisplayLayout.Bands[0].Columns["EmpName"].Hidden = true;
						ultraGridColumn2.Hidden = hidden;
					}
					else
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["Date"].Hidden = true;
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

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Tab || keyData == Keys.Return)
			{
				if (base.ActiveControl == dateTimePickerFromTime)
				{
					if (dataGridItems.ActiveRow != null)
					{
						dataGridItems.ActiveCell = dataGridItems.ActiveRow.Cells["To"];
						dataGridItems.ActiveRow.Cells["To"].Activate();
						dateTimePickerToTime.Focus();
						dataGridItems.PerformAction(UltraGridAction.EnterEditMode, shift: false, control: false);
						return true;
					}
				}
				else if (base.ActiveControl == dateTimePickerToTime && dataGridItems.ActiveRow != null)
				{
					dataGridItems.ActiveCell = dataGridItems.ActiveRow.Cells["Hours"];
					dataGridItems.ActiveRow.Cells["Hours"].Activate();
					dataGridItems.PerformAction(UltraGridAction.EnterEditMode, shift: false, control: false);
					return true;
				}
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void OverTimeEntryForm_ResizeEnd(object sender, EventArgs e)
		{
		}

		private void OverTimeEntryForm_SizeChanged(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					if (!dateTimePickerFromTime.Visible || dataGridItems.ActiveRow == null)
					{
						goto IL_0108;
					}
					UltraGridCell ultraGridCell = dataGridItems.ActiveRow.Cells["From"];
					if (ultraGridCell != null)
					{
						CellUIElement cellUIElement = (CellUIElement)ultraGridCell.GetUIElement(dataGridItems.ActiveRowScrollRegion, dataGridItems.ActiveColScrollRegion);
						if (cellUIElement != null)
						{
							int x = cellUIElement.RectInsideBorders.Location.X + dataGridItems.Location.X;
							int y = cellUIElement.RectInsideBorders.Location.Y + dataGridItems.Location.Y;
							int width = cellUIElement.RectInsideBorders.Width;
							int height = cellUIElement.RectInsideBorders.Height;
							dateTimePickerFromTime.SetBounds(x, y, width, height);
							dateTimePickerFromTime.Visible = true;
							goto IL_0108;
						}
					}
					goto end_IL_0000;
					IL_0108:
					if (dateTimePickerToTime.Visible && dataGridItems.ActiveRow != null)
					{
						UltraGridCell ultraGridCell2 = dataGridItems.ActiveRow.Cells["To"];
						if (ultraGridCell2 != null)
						{
							CellUIElement cellUIElement2 = (CellUIElement)ultraGridCell2.GetUIElement(dataGridItems.ActiveRowScrollRegion, dataGridItems.ActiveColScrollRegion);
							if (cellUIElement2 != null)
							{
								int x2 = cellUIElement2.RectInsideBorders.Location.X + dataGridItems.Location.X;
								int y2 = cellUIElement2.RectInsideBorders.Location.Y + dataGridItems.Location.Y;
								int width2 = cellUIElement2.RectInsideBorders.Width;
								int height2 = cellUIElement2.RectInsideBorders.Height;
								dateTimePickerToTime.SetBounds(x2, y2, width2, height2);
							}
						}
					}
					end_IL_0000:;
				}
				catch
				{
				}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Projects.OverTimeEntryForm));
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
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			panelDetails = new System.Windows.Forms.Panel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			buttonFillDetails = new System.Windows.Forms.Button();
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkBoxDatewise = new System.Windows.Forms.CheckBox();
			comboBoxYear = new System.Windows.Forms.ComboBox();
			comboBoxMonth = new Micromind.DataControls.MonthComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			panelEmployee = new System.Windows.Forms.Panel();
			textBoxEmployeeName = new System.Windows.Forms.TextBox();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			employeeLinkLabel = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDate = new System.Windows.Forms.Panel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
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
			dateTimePickerFromTime = new System.Windows.Forms.DateTimePicker();
			dateTimePickerToTime = new System.Windows.Forms.DateTimePicker();
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
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			panelEmployee.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			panelDate.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			panelApproval.SuspendLayout();
			panelApprovalDetail.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrolItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOverTime).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)jobComboBox1).BeginInit();
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
			toolStrip1.Size = new System.Drawing.Size(887, 31);
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
			copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			copyToolStripMenuItem.Text = "Copy";
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
			panelButtons.Size = new System.Drawing.Size(887, 40);
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
			linePanelDown.Size = new System.Drawing.Size(887, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(777, 8);
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
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(87, 2);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(139, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(315, 2);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 20);
			textBoxVoucherNumber.TabIndex = 1;
			textBoxNote.Location = new System.Drawing.Point(97, 50);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(536, 20);
			textBoxNote.TabIndex = 8;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(10, 53);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(buttonFillDetails);
			panelDetails.Controls.Add(linkLabelVoucherNumber);
			panelDetails.Controls.Add(checkBoxDatewise);
			panelDetails.Controls.Add(comboBoxYear);
			panelDetails.Controls.Add(comboBoxMonth);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(panelEmployee);
			panelDetails.Controls.Add(panelDate);
			panelDetails.Location = new System.Drawing.Point(0, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(785, 83);
			panelDetails.TabIndex = 0;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(10, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
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
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(98, 1);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(114, 20);
			comboBoxSysDoc.TabIndex = 136;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonFillDetails.Enabled = false;
			buttonFillDetails.Location = new System.Drawing.Point(656, 48);
			buttonFillDetails.Name = "buttonFillDetails";
			buttonFillDetails.Size = new System.Drawing.Size(99, 23);
			buttonFillDetails.TabIndex = 135;
			buttonFillDetails.Text = "Fill Details";
			buttonFillDetails.UseVisualStyleBackColor = true;
			buttonFillDetails.Click += new System.EventHandler(buttonFillDetails_Click);
			appearance15.FontData.BoldAsString = "True";
			appearance15.FontData.Name = "Tahoma";
			linkLabelVoucherNumber.Appearance = appearance15;
			linkLabelVoucherNumber.AutoSize = true;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(229, 5);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(77, 15);
			linkLabelVoucherNumber.TabIndex = 134;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Doc Number:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			linkLabelVoucherNumber.VisitedLinkAppearance = appearance16;
			linkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVoucherNumber_LinkClicked);
			checkBoxDatewise.AutoSize = true;
			checkBoxDatewise.Location = new System.Drawing.Point(708, 5);
			checkBoxDatewise.Name = "checkBoxDatewise";
			checkBoxDatewise.Size = new System.Drawing.Size(70, 17);
			checkBoxDatewise.TabIndex = 132;
			checkBoxDatewise.Text = "Datewise";
			checkBoxDatewise.UseVisualStyleBackColor = true;
			checkBoxDatewise.CheckedChanged += new System.EventHandler(checkBoxDatewise_CheckedChanged);
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
			comboBoxYear.Location = new System.Drawing.Point(622, 2);
			comboBoxYear.Name = "comboBoxYear";
			comboBoxYear.Size = new System.Drawing.Size(77, 21);
			comboBoxYear.TabIndex = 7;
			comboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMonth.FormattingEnabled = true;
			comboBoxMonth.IsMonthNumbers = false;
			comboBoxMonth.Location = new System.Drawing.Point(511, 2);
			comboBoxMonth.Name = "comboBoxMonth";
			comboBoxMonth.Size = new System.Drawing.Size(107, 21);
			comboBoxMonth.TabIndex = 6;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(458, 6);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(47, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Period:";
			panelEmployee.Controls.Add(textBoxEmployeeName);
			panelEmployee.Controls.Add(comboBoxEmployee);
			panelEmployee.Controls.Add(employeeLinkLabel);
			panelEmployee.Location = new System.Drawing.Point(10, 23);
			panelEmployee.Name = "panelEmployee";
			panelEmployee.Size = new System.Drawing.Size(630, 25);
			panelEmployee.TabIndex = 133;
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
			employeeLinkLabel.Location = new System.Drawing.Point(3, 6);
			employeeLinkLabel.Name = "employeeLinkLabel";
			employeeLinkLabel.Size = new System.Drawing.Size(62, 15);
			employeeLinkLabel.TabIndex = 129;
			employeeLinkLabel.TabStop = true;
			employeeLinkLabel.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			employeeLinkLabel.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			employeeLinkLabel.Value = "Employee:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			employeeLinkLabel.VisitedLinkAppearance = appearance30;
			employeeLinkLabel.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(employeeLinkLabel_LinkClicked);
			panelDate.Controls.Add(mmLabel4);
			panelDate.Controls.Add(dateTimePickerDate);
			panelDate.Location = new System.Drawing.Point(10, 24);
			panelDate.Name = "panelDate";
			panelDate.Size = new System.Drawing.Size(274, 24);
			panelDate.TabIndex = 131;
			panelDate.Visible = false;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(0, 5);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(38, 13);
			mmLabel4.TabIndex = 130;
			mmLabel4.Text = "Date:";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(12, 375);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(853, 62);
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
			panelApproval.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panelApproval.Controls.Add(panelApprovalDetail);
			panelApproval.Controls.Add(checkBoxApprove);
			panelApproval.Location = new System.Drawing.Point(12, 433);
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
			textBoxCreatedBy.TabIndex = 131;
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
			dateTimeCreatedDate.TabIndex = 129;
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
			textBoxApprovedBy.TabIndex = 127;
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
			dateTimeApprovalDate.TabIndex = 22;
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
			checkBoxApprove.TabIndex = 0;
			checkBoxApprove.Text = "Approve";
			checkBoxApprove.UseVisualStyleBackColor = true;
			dateTimePickerFromTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
			dateTimePickerFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerFromTime.Location = new System.Drawing.Point(531, 174);
			dateTimePickerFromTime.Name = "dateTimePickerFromTime";
			dateTimePickerFromTime.ShowUpDown = true;
			dateTimePickerFromTime.Size = new System.Drawing.Size(131, 20);
			dateTimePickerFromTime.TabIndex = 131;
			dateTimePickerFromTime.Visible = false;
			dateTimePickerToTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
			dateTimePickerToTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerToTime.Location = new System.Drawing.Point(690, 174);
			dateTimePickerToTime.Name = "dateTimePickerToTime";
			dateTimePickerToTime.ShowUpDown = true;
			dateTimePickerToTime.Size = new System.Drawing.Size(131, 20);
			dateTimePickerToTime.TabIndex = 132;
			dateTimePickerToTime.Visible = false;
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
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance31;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance38;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance40;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance41;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(11, 120);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(854, 318);
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
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayrolItem.DisplayLayout.Appearance = appearance43;
			comboBoxPayrolItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayrolItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance44.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance44.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.Appearance = appearance44;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance45;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance46.BackColor2 = System.Drawing.SystemColors.Control;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrolItem.DisplayLayout.GroupByBox.PromptAppearance = appearance46;
			comboBoxPayrolItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayrolItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayrolItem.DisplayLayout.Override.ActiveCellAppearance = appearance47;
			appearance48.BackColor = System.Drawing.SystemColors.Highlight;
			appearance48.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayrolItem.DisplayLayout.Override.ActiveRowAppearance = appearance48;
			comboBoxPayrolItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayrolItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayrolItem.DisplayLayout.Override.CardAreaAppearance = appearance49;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayrolItem.DisplayLayout.Override.CellAppearance = appearance50;
			comboBoxPayrolItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayrolItem.DisplayLayout.Override.CellPadding = 0;
			appearance51.BackColor = System.Drawing.SystemColors.Control;
			appearance51.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance51.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance51.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrolItem.DisplayLayout.Override.GroupByRowAppearance = appearance51;
			appearance52.TextHAlignAsString = "Left";
			comboBoxPayrolItem.DisplayLayout.Override.HeaderAppearance = appearance52;
			comboBoxPayrolItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayrolItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayrolItem.DisplayLayout.Override.RowAppearance = appearance53;
			comboBoxPayrolItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayrolItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance54;
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
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxOverTime.DisplayLayout.Appearance = appearance55;
			comboBoxOverTime.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxOverTime.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance56.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOverTime.DisplayLayout.GroupByBox.Appearance = appearance56;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOverTime.DisplayLayout.GroupByBox.BandLabelAppearance = appearance57;
			comboBoxOverTime.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance58.BackColor2 = System.Drawing.SystemColors.Control;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOverTime.DisplayLayout.GroupByBox.PromptAppearance = appearance58;
			comboBoxOverTime.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxOverTime.DisplayLayout.MaxRowScrollRegions = 1;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxOverTime.DisplayLayout.Override.ActiveCellAppearance = appearance59;
			appearance60.BackColor = System.Drawing.SystemColors.Highlight;
			appearance60.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxOverTime.DisplayLayout.Override.ActiveRowAppearance = appearance60;
			comboBoxOverTime.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxOverTime.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			comboBoxOverTime.DisplayLayout.Override.CardAreaAppearance = appearance61;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			appearance62.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxOverTime.DisplayLayout.Override.CellAppearance = appearance62;
			comboBoxOverTime.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxOverTime.DisplayLayout.Override.CellPadding = 0;
			appearance63.BackColor = System.Drawing.SystemColors.Control;
			appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance63.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance63.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOverTime.DisplayLayout.Override.GroupByRowAppearance = appearance63;
			appearance64.TextHAlignAsString = "Left";
			comboBoxOverTime.DisplayLayout.Override.HeaderAppearance = appearance64;
			comboBoxOverTime.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxOverTime.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			comboBoxOverTime.DisplayLayout.Override.RowAppearance = appearance65;
			comboBoxOverTime.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxOverTime.DisplayLayout.Override.TemplateAddRowAppearance = appearance66;
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
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridEmployee.DisplayLayout.Appearance = appearance67;
			comboBoxGridEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance68.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance68.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.Appearance = appearance68;
			appearance69.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance69;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance70.BackColor2 = System.Drawing.SystemColors.Control;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance70.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance70;
			comboBoxGridEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance71;
			appearance72.BackColor = System.Drawing.SystemColors.Highlight;
			appearance72.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance72;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.CardAreaAppearance = appearance73;
			appearance74.BorderColor = System.Drawing.Color.Silver;
			appearance74.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridEmployee.DisplayLayout.Override.CellAppearance = appearance74;
			comboBoxGridEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance75.BackColor = System.Drawing.SystemColors.Control;
			appearance75.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance75.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance75.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance75;
			appearance76.TextHAlignAsString = "Left";
			comboBoxGridEmployee.DisplayLayout.Override.HeaderAppearance = appearance76;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridEmployee.DisplayLayout.Override.RowAppearance = appearance77;
			comboBoxGridEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance78;
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
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance79;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance80.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance80.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance80.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance80;
			appearance81.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance81;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance82.BackColor2 = System.Drawing.SystemColors.Control;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance82.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance82;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance83;
			appearance84.BackColor = System.Drawing.SystemColors.Highlight;
			appearance84.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance84;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance85;
			appearance86.BorderColor = System.Drawing.Color.Silver;
			appearance86.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance86;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance87.BackColor = System.Drawing.SystemColors.Control;
			appearance87.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance87.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance87.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance87.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance87;
			appearance88.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance88;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance89;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance90;
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
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			jobComboBox1.DisplayLayout.Appearance = appearance91;
			jobComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			jobComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance92.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance92.BorderColor = System.Drawing.SystemColors.Window;
			jobComboBox1.DisplayLayout.GroupByBox.Appearance = appearance92;
			appearance93.ForeColor = System.Drawing.SystemColors.GrayText;
			jobComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance93;
			jobComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance94.BackColor2 = System.Drawing.SystemColors.Control;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance94.ForeColor = System.Drawing.SystemColors.GrayText;
			jobComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance94;
			jobComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			jobComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.ForeColor = System.Drawing.SystemColors.ControlText;
			jobComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance95;
			appearance96.BackColor = System.Drawing.SystemColors.Highlight;
			appearance96.ForeColor = System.Drawing.SystemColors.HighlightText;
			jobComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance96;
			jobComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			jobComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			jobComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance97;
			appearance98.BorderColor = System.Drawing.Color.Silver;
			appearance98.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			jobComboBox1.DisplayLayout.Override.CellAppearance = appearance98;
			jobComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			jobComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance99.BackColor = System.Drawing.SystemColors.Control;
			appearance99.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance99.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance99.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance99.BorderColor = System.Drawing.SystemColors.Window;
			jobComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance99;
			appearance100.TextHAlignAsString = "Left";
			jobComboBox1.DisplayLayout.Override.HeaderAppearance = appearance100;
			jobComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			jobComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			jobComboBox1.DisplayLayout.Override.RowAppearance = appearance101;
			jobComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ControlLight;
			jobComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance102;
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
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(887, 528);
			base.Controls.Add(dateTimePickerToTime);
			base.Controls.Add(dateTimePickerFromTime);
			base.Controls.Add(comboBoxCostCategory);
			base.Controls.Add(panelApproval);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxJob);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxPayrolItem);
			base.Controls.Add(comboBoxOverTime);
			base.Controls.Add(comboBoxGridEmployee);
			base.Controls.Add(comboBoxLocation);
			base.Controls.Add(jobComboBox1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(895, 555);
			base.Name = "OverTimeEntryForm";
			Text = "Attendance Entry";
			base.ResizeEnd += new System.EventHandler(OverTimeEntryForm_ResizeEnd);
			base.SizeChanged += new System.EventHandler(OverTimeEntryForm_SizeChanged);
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
			panelDate.ResumeLayout(false);
			panelDate.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			panelApproval.ResumeLayout(false);
			panelApproval.PerformLayout();
			panelApprovalDetail.ResumeLayout(false);
			panelApprovalDetail.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrolItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOverTime).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)jobComboBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
