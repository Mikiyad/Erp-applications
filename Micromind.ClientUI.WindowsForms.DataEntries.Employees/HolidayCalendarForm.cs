using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class HolidayCalendarForm : Form, IForm
	{
		private HolidayCalendarData currentData;

		private const string TABLENAME_CONST = "Holiday_Calendar";

		private const string IDFIELD_CONST = "CalendarID";

		private bool isNewRecord = true;

		private bool isRevise;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private List<string> ListItems = new List<string>();

		private List<string> ListDays = new List<string>();

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

		private MMTextBox textBoxCalendarCode;

		private NonDirtyPanel nonDirtyPanel1;

		private DataEntryGrid dataGridHolidays;

		private PayrollItemComboBox comboBoxPayrollItem;

		private UltraTabControl ultraTabControlDays;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private TextBox textBoxRemarks;

		private Label label8;

		private UltraTabPageControl ultraTabPageControl1;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private DataEntryGrid dataGridOffDays;

		private MMLabel labelCalendarName;

		private CheckedListBox checkedListBoxOffDays;

		private MMTextBox textBoxCalendarName;

		private GroupBox groupBox1;

		private Label label1;

		private XPButton buttonClearHolidays;

		private XPButton buttonClearOffDays;

		private XPButton buttonLoadOffDays;

		private Label label3;

		private Label label2;

		private DateTimePicker dateTimePickerFromDate;

		private DateTimePicker dateTimePickerToDate;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5017;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		private bool IsDirty => formManager.GetDirtyStatus();

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
					buttonDelete.Enabled = false;
					textBoxCalendarCode.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCalendarCode.ReadOnly = true;
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

		public HolidayCalendarForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += HolidayCalendarForm_Load;
			dataGridHolidays.CellDataError += dataGrid_CellDataError;
			dataGridHolidays.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridHolidays.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridHolidays.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			comboBoxPayrollItem.SelectedIndexChanged += comboBoxPayrollItem_SelectedIndexChanged;
			dataGridHolidays.AfterCellUpdate += dataGridPayrollItem_AfterCellUpdate;
			dataGridHolidays.HeaderClicked += dataGridPayrollItem_HeaderClicked;
		}

		private void comboBoxBenefit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxDeduction_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGridPayrollItem_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (ultraGridColumn != null && ultraGridColumn.Key == "PayrollItem")
			{
				string id = "";
				if (dataGridHolidays.ActiveRow != null)
				{
					id = dataGridHolidays.ActiveRow.Cells["PayrollItem"].Text;
				}
				new FormHelper().EditPayrollItem(id);
			}
		}

		private void dataGridPayrollItem_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridHolidays.ActiveRow != null)
				{
					DateTime result = new DateTime(2014, 1, 1);
					DateTime result2 = new DateTime(2014, 1, 1);
					DateTime d = new DateTime(1, 1, 1);
					DateTime.TryParse(dataGridHolidays.ActiveRow.Cells["From"].Value.ToString(), out result);
					if (e.Cell.Column.Key == "From")
					{
						dataGridHolidays.ActiveRow.Cells["To"].Value = result;
						DateTime.TryParse(dataGridHolidays.ActiveRow.Cells["To"].Value.ToString(), out result2);
						if (result2 != d)
						{
							dataGridHolidays.ActiveRow.Cells["Day"].Value = (result2 - result).TotalDays + 1.0;
						}
					}
					else
					{
						_ = (e.Cell.Column.Key == "To");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxPayrollItem_SelectedIndexChanged(object sender, EventArgs e)
		{
			_ = dataGridHolidays.ActiveRow;
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
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
					currentData = new HolidayCalendarData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.HolidayCalendarTable.Rows[0] : currentData.HolidayCalendarTable.NewRow();
				dataRow.BeginEdit();
				dataRow["CalendarID"] = textBoxCalendarCode.Text;
				dataRow["CalendarName"] = textBoxCalendarName.Text;
				dataRow["Remarks"] = textBoxRemarks.Text;
				string text2 = (string)(dataRow["OffDays"] = string.Join(",", ListItems.ToArray()));
				dataRow["OffDateFrom"] = dateTimePickerFromDate.Value;
				dataRow["OffDateTo"] = dateTimePickerToDate.Value;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.HolidayCalendarTable.Rows.Add(dataRow);
				}
				currentData.HolidayCalendarDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridHolidays.Rows)
				{
					dataRow = currentData.Tables["Holiday_Calendar_Detail"].NewRow();
					dataRow.BeginEdit();
					dataRow["FromDate"] = DateTime.Parse(row.Cells["From"].Value.ToString());
					dataRow["ToDate"] = DateTime.Parse(row.Cells["To"].Value.ToString());
					dataRow["Days"] = row.Cells["Day"].Value.ToString();
					dataRow["HolidayType"] = "H";
					dataRow["Remarks"] = row.Cells["Remarks"].Value.ToString();
					dataRow["RowIndex"] = row.Index;
					dataRow.EndEdit();
					currentData.Tables["Holiday_Calendar_Detail"].Rows.Add(dataRow);
				}
				foreach (UltraGridRow row2 in dataGridOffDays.Rows)
				{
					dataRow = currentData.Tables["Holiday_Calendar_Detail"].NewRow();
					dataRow.BeginEdit();
					dataRow["FromDate"] = DateTime.Parse(row2.Cells["From"].Value.ToString());
					dataRow["ToDate"] = DateTime.Parse(row2.Cells["To"].Value.ToString());
					dataRow["Days"] = row2.Cells["Day"].Value.ToString();
					dataRow["HolidayType"] = "O";
					dataRow["Remarks"] = row2.Cells["Remarks"].Value.ToString();
					dataRow["RowIndex"] = row2.Index;
					dataRow.EndEdit();
					currentData.Tables["Holiday_Calendar_Detail"].Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void SetupHolidaysGrid()
		{
			dataGridHolidays.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable("PayrollItem");
			dataTable.Columns.Add("From", typeof(DateTime));
			dataTable.Columns.Add("To", typeof(DateTime));
			dataTable.Columns.Add("Day of Week", typeof(string));
			dataTable.Columns.Add("Day", typeof(int));
			dataTable.Columns.Add("Type", typeof(string));
			dataTable.Columns.Add("Remarks", typeof(string));
			dataGridHolidays.DataSource = dataTable;
			dataGridHolidays.DisplayLayout.Bands[0].Columns["From"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridHolidays.DisplayLayout.Bands[0].Columns["To"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridHolidays.DisplayLayout.Bands[0].Columns["To"].CellActivation = Activation.NoEdit;
			dataGridHolidays.DisplayLayout.Bands[0].Columns["Day"].CellActivation = Activation.NoEdit;
			dataGridHolidays.DisplayLayout.Bands[0].Columns["Day of Week"].CellActivation = Activation.NoEdit;
			dataGridHolidays.DisplayLayout.Bands[0].Columns["Day of Week"].Hidden = true;
			dataGridHolidays.DisplayLayout.Bands[0].Columns["Type"].Hidden = true;
			dataGridHolidays.DisplayLayout.Bands[0].Columns["From"].Width = checked(20 * dataGridHolidays.Width) / 100;
			dataGridHolidays.DisplayLayout.Bands[0].Columns["To"].Width = checked(20 * dataGridHolidays.Width) / 100;
			dataGridHolidays.DisplayLayout.Bands[0].Columns["Remarks"].Width = checked(60 * dataGridHolidays.Width) / 100;
		}

		private void SetupOffDaysGrid()
		{
			dataGridOffDays.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable("PayrollItem");
			dataTable.Columns.Add("From", typeof(DateTime));
			dataTable.Columns.Add("To", typeof(DateTime));
			dataTable.Columns.Add("Day of Week", typeof(string));
			dataTable.Columns.Add("Day", typeof(int));
			dataTable.Columns.Add("Type", typeof(string));
			dataTable.Columns.Add("Remarks", typeof(string));
			dataGridOffDays.DataSource = dataTable;
			dataGridOffDays.DisplayLayout.Bands[0].Columns["From"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridOffDays.DisplayLayout.Bands[0].Columns["To"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridOffDays.DisplayLayout.Bands[0].Columns["Day"].CellActivation = Activation.NoEdit;
			dataGridOffDays.DisplayLayout.Bands[0].Columns["From"].CellActivation = Activation.NoEdit;
			dataGridOffDays.DisplayLayout.Bands[0].Columns["To"].CellActivation = Activation.NoEdit;
			dataGridOffDays.DisplayLayout.Bands[0].Columns["Day of Week"].CellActivation = Activation.NoEdit;
			dataGridOffDays.DisplayLayout.Bands[0].Columns["Type"].Hidden = true;
			dataGridOffDays.DisplayLayout.Bands[0].Columns["From"].Width = checked(20 * dataGridOffDays.Width) / 100;
			dataGridOffDays.DisplayLayout.Bands[0].Columns["To"].Width = checked(20 * dataGridOffDays.Width) / 100;
			dataGridOffDays.DisplayLayout.Bands[0].Columns["Day of Week"].Width = checked(30 * dataGridOffDays.Width) / 100;
			dataGridOffDays.DisplayLayout.Bands[0].Columns["Remarks"].Width = checked(60 * dataGridOffDays.Width) / 100;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGridHolidays.Focus();
		}

		public void LoadData(string calendarID)
		{
			try
			{
				if (!(calendarID.Trim() == "") && CanClose())
				{
					currentData = Factory.HolidayCalendarSystem.GetHolidayCalendarByID(calendarID);
					if (currentData != null)
					{
						ClearForm();
						IsNewRecord = false;
						FillData();
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
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0 && currentData.Tables.Contains("Holiday_Calendar") && currentData.Tables["Holiday_Calendar"].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxCalendarCode.Text = dataRow["CalendarID"].ToString();
					textBoxCalendarName.Text = dataRow["CalendarName"].ToString();
					textBoxRemarks.Text = dataRow["Remarks"].ToString();
					string[] array = dataRow["OffDays"].ToString().Split(',');
					for (int i = 0; i < array.Length; i = checked(i + 1))
					{
						array[i] = array[i].Trim();
						if (array[i].Trim() != "" && array[i].Trim() != string.Empty)
						{
							int index = int.Parse(array[i].ToString());
							checkedListBoxOffDays.SetItemChecked(index, value: true);
							ListItems.Add(array[i].ToString());
						}
					}
					dateTimePickerFromDate.Value = DateTime.Parse(dataRow["OffDateFrom"].ToString());
					dateTimePickerToDate.Value = DateTime.Parse(dataRow["OffDateTo"].ToString());
					DataTable dataTable = dataGridHolidays.DataSource as DataTable;
					dataTable.Rows.Clear();
					DataTable dataTable2 = dataGridOffDays.DataSource as DataTable;
					dataTable2.Rows.Clear();
					if (currentData.Tables.Contains("Holiday_Calendar_Detail") && currentData.HolidayCalendarDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Holiday_Calendar_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							if (row["HolidayType"].ToString().Trim() == "H")
							{
								dataRow3["From"] = row["FromDate"];
								dataRow3["To"] = row["ToDate"];
								dataRow3["Day"] = row["Days"];
								dataRow3["Remarks"] = row["Remarks"].ToString();
								dataRow3.EndEdit();
								dataTable.Rows.Add(dataRow3);
							}
						}
						dataTable.AcceptChanges();
						foreach (DataRow row2 in currentData.Tables["Holiday_Calendar_Detail"].Rows)
						{
							DataRow dataRow5 = dataTable2.NewRow();
							if (row2["HolidayType"].ToString().Trim() == "O")
							{
								dataRow5["From"] = row2["FromDate"];
								dataRow5["To"] = row2["ToDate"];
								dataRow5["Day of Week"] = DateTime.Parse(row2["ToDate"].ToString()).DayOfWeek;
								dataRow5["Day"] = row2["Days"];
								dataRow5["Remarks"] = row2["Remarks"].ToString();
								dataRow5.EndEdit();
								dataTable2.Rows.Add(dataRow5);
							}
						}
						dataTable2.AcceptChanges();
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
				bool flag = Factory.HolidayCalendarSystem.CreateHolidayCalendar(currentData, !isNewRecord);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					IsNewRecord = true;
					ClearForm();
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
			if (!screenRight.Edit || !screenRight.New)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
				return false;
			}
			return true;
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
		}

		private void ClearForm()
		{
			(dataGridHolidays.DataSource as DataTable).Rows.Clear();
			(dataGridOffDays.DataSource as DataTable).Rows.Clear();
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("FiscalYear", "StartDate", "FiscalYearID", DateTime.Now.Year, "Status", "1");
			object fieldValue2 = Factory.DatabaseSystem.GetFieldValue("FiscalYear", "EndDate", "FiscalYearID", DateTime.Now.Year, "Status", "1");
			dateTimePickerFromDate.Text = fieldValue.ToString();
			dateTimePickerToDate.Text = fieldValue2.ToString();
			textBoxCalendarCode.Clear();
			textBoxCalendarName.Clear();
			textBoxRemarks.Clear();
			ListItems.Clear();
			ListDays.Clear();
			UltraTabsCollection tabs = ultraTabControlDays.Tabs;
			ultraTabControlDays.SelectedTab = tabs[0];
			foreach (int checkedIndex in checkedListBoxOffDays.CheckedIndices)
			{
				checkedListBoxOffDays.SetItemCheckState(checkedIndex, CheckState.Unchecked);
			}
			formManager.ResetDirty();
		}

		private void EmployeeSkillGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void EmployeeSkillGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.HolidayCalendarSystem.DeleteHolidayCalendar(textBoxCalendarCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Holiday_Calendar", "CalendarID", textBoxCalendarCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Holiday_Calendar", "CalendarID", textBoxCalendarCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Holiday_Calendar", "CalendarID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Holiday_Calendar", "CalendarID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Holiday_Calendar", "CalendarID", toolStripTextBoxFind.Text.Trim()))
				{
					toolStripTextBoxFind.Text.Trim();
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
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

		private void HolidayCalendarForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridHolidays.SetupUI();
				SetupHolidaysGrid();
				dataGridOffDays.SetupUI();
				SetupOffDaysGrid();
				SetSecurity();
				if (!base.IsDisposed)
				{
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataGridHolidays.LoadLayoutFailed = true;
				dataGridOffDays.LoadLayoutFailed = true;
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

		private void comboBoxEmployees_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void textBoxBasic_TextChanged(object sender, EventArgs e)
		{
			ShowTotalSalary();
		}

		private void ShowTotalSalary()
		{
			decimal num = default(decimal);
			foreach (UltraGridRow row in dataGridHolidays.Rows)
			{
				decimal result = default(decimal);
				decimal.TryParse(row.Cells["Amount"].Text, out result);
				num += result;
			}
		}

		private void linkLabelCountry_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCalendarCode.Text, "", this);
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.HolidayCalendar);
		}

		private void dateTimePickerFromDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void dateTimePickerToDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void buttonLoadOffDays_Click(object sender, EventArgs e)
		{
			ListItems.Clear();
			ListDays.Clear();
			foreach (object checkedItem in checkedListBoxOffDays.CheckedItems)
			{
				int num = checkedListBoxOffDays.Items.IndexOf(checkedItem);
				ListDays.Add(checkedItem.ToString());
				if (!ListItems.Contains(num.ToString()))
				{
					ListItems.Add(num.ToString());
				}
			}
			DataTable dataTable = dataGridOffDays.DataSource as DataTable;
			dataTable.Rows.Clear();
			foreach (DateTime item in EachDay(dateTimePickerFromDate.Value, dateTimePickerToDate.Value))
			{
				if (ListDays.Contains(item.DayOfWeek.ToString()))
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["From"] = item;
					dataRow["To"] = item;
					dataRow["Day"] = 1;
					dataRow["Day of Week"] = item.DayOfWeek.ToString();
					dataRow.EndEdit();
					dataTable.Rows.Add(dataRow);
				}
				dataTable.AcceptChanges();
			}
		}

		public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
		{
			DateTime day = from.Date;
			while (day.Date <= thru.Date)
			{
				yield return day;
				day = day.AddDays(1.0);
			}
		}

		private void buttonClearOffDays_Click(object sender, EventArgs e)
		{
			(dataGridOffDays.DataSource as DataTable).Rows.Clear();
		}

		private void buttonClearHolidays_Click(object sender, EventArgs e)
		{
			(dataGridHolidays.DataSource as DataTable).Rows.Clear();
		}

		private void buttonNew_Click_1(object sender, EventArgs e)
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

		private void HolidayCalendarForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private void textBoxCalendarName_TextChanged(object sender, EventArgs e)
		{
			formManager.IsForcedDirty = true;
		}

		private void textBoxRemarks_TextChanged(object sender, EventArgs e)
		{
			formManager.IsForcedDirty = true;
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData()))
				{
					string text = textBoxCalendarCode.Text;
					DataSet holidayCalendarToPrint = Factory.HolidayCalendarSystem.GetHolidayCalendarToPrint(text);
					if (holidayCalendarToPrint == null || holidayCalendarToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(holidayCalendarToPrint, "", "Holiday Calendar", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.HolidayCalendarForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonClearHolidays = new Micromind.UISupport.XPButton();
			dataGridHolidays = new Micromind.DataControls.DataEntryGrid();
			comboBoxPayrollItem = new Micromind.DataControls.PayrollItemComboBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonClearOffDays = new Micromind.UISupport.XPButton();
			checkedListBoxOffDays = new System.Windows.Forms.CheckedListBox();
			dataGridOffDays = new Micromind.DataControls.DataEntryGrid();
			groupBox1 = new System.Windows.Forms.GroupBox();
			dateTimePickerFromDate = new System.Windows.Forms.DateTimePicker();
			dateTimePickerToDate = new System.Windows.Forms.DateTimePicker();
			buttonLoadOffDays = new Micromind.UISupport.XPButton();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControlDays = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			nonDirtyPanel1 = new Micromind.UISupport.NonDirtyPanel(components);
			textBoxCalendarName = new Micromind.UISupport.MMTextBox();
			labelCalendarName = new Micromind.UISupport.MMLabel();
			label8 = new System.Windows.Forms.Label();
			textBoxRemarks = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxCalendarCode = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridHolidays).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItem).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridOffDays).BeginInit();
			groupBox1.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControlDays).BeginInit();
			ultraTabControlDays.SuspendLayout();
			nonDirtyPanel1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(buttonClearHolidays);
			tabPageGeneral.Controls.Add(dataGridHolidays);
			tabPageGeneral.Controls.Add(comboBoxPayrollItem);
			tabPageGeneral.Location = new System.Drawing.Point(1, 20);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(710, 390);
			buttonClearHolidays.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClearHolidays.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClearHolidays.BackColor = System.Drawing.Color.DarkGray;
			buttonClearHolidays.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClearHolidays.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClearHolidays.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClearHolidays.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClearHolidays.Location = new System.Drawing.Point(12, 357);
			buttonClearHolidays.Name = "buttonClearHolidays";
			buttonClearHolidays.Size = new System.Drawing.Size(96, 24);
			buttonClearHolidays.TabIndex = 20;
			buttonClearHolidays.Text = "&Clear";
			buttonClearHolidays.UseVisualStyleBackColor = false;
			buttonClearHolidays.Click += new System.EventHandler(buttonClearHolidays_Click);
			dataGridHolidays.AllowAddNew = false;
			dataGridHolidays.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridHolidays.DisplayLayout.Appearance = appearance;
			dataGridHolidays.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridHolidays.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridHolidays.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridHolidays.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridHolidays.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridHolidays.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridHolidays.DisplayLayout.MaxColScrollRegions = 1;
			dataGridHolidays.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridHolidays.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridHolidays.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridHolidays.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridHolidays.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridHolidays.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridHolidays.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridHolidays.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridHolidays.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridHolidays.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridHolidays.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridHolidays.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridHolidays.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridHolidays.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridHolidays.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridHolidays.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridHolidays.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridHolidays.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridHolidays.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridHolidays.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridHolidays.Location = new System.Drawing.Point(12, 17);
			dataGridHolidays.Name = "dataGridHolidays";
			dataGridHolidays.ShowClearMenu = true;
			dataGridHolidays.ShowDeleteMenu = true;
			dataGridHolidays.ShowInsertMenu = true;
			dataGridHolidays.ShowMoveRowsMenu = true;
			dataGridHolidays.Size = new System.Drawing.Size(687, 334);
			dataGridHolidays.TabIndex = 0;
			comboBoxPayrollItem.Assigned = false;
			comboBoxPayrollItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayrollItem.CustomReportFieldName = "";
			comboBoxPayrollItem.CustomReportKey = "";
			comboBoxPayrollItem.CustomReportValueType = 1;
			comboBoxPayrollItem.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayrollItem.DisplayLayout.Appearance = appearance13;
			comboBoxPayrollItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayrollItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItem.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxPayrollItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayrollItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayrollItem.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayrollItem.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxPayrollItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayrollItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItem.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayrollItem.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxPayrollItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayrollItem.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItem.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxPayrollItem.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxPayrollItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayrollItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayrollItem.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxPayrollItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayrollItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxPayrollItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayrollItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayrollItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayrollItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayrollItem.Editable = true;
			comboBoxPayrollItem.FilterString = "";
			comboBoxPayrollItem.HasAllAccount = false;
			comboBoxPayrollItem.HasCustom = false;
			comboBoxPayrollItem.IsDataLoaded = false;
			comboBoxPayrollItem.IsDeduction = false;
			comboBoxPayrollItem.Location = new System.Drawing.Point(510, 70);
			comboBoxPayrollItem.MaxDropDownItems = 12;
			comboBoxPayrollItem.Name = "comboBoxPayrollItem";
			comboBoxPayrollItem.ShowInactiveItems = false;
			comboBoxPayrollItem.ShowQuickAdd = true;
			comboBoxPayrollItem.Size = new System.Drawing.Size(81, 21);
			comboBoxPayrollItem.TabIndex = 19;
			comboBoxPayrollItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPayrollItem.Visible = false;
			ultraTabPageControl1.Controls.Add(buttonClearOffDays);
			ultraTabPageControl1.Controls.Add(checkedListBoxOffDays);
			ultraTabPageControl1.Controls.Add(dataGridOffDays);
			ultraTabPageControl1.Controls.Add(groupBox1);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(710, 390);
			buttonClearOffDays.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClearOffDays.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClearOffDays.BackColor = System.Drawing.Color.DarkGray;
			buttonClearOffDays.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClearOffDays.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClearOffDays.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClearOffDays.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClearOffDays.Location = new System.Drawing.Point(13, 364);
			buttonClearOffDays.Name = "buttonClearOffDays";
			buttonClearOffDays.Size = new System.Drawing.Size(96, 24);
			buttonClearOffDays.TabIndex = 21;
			buttonClearOffDays.Text = "&Clear";
			buttonClearOffDays.UseVisualStyleBackColor = false;
			buttonClearOffDays.Click += new System.EventHandler(buttonClearOffDays_Click);
			checkedListBoxOffDays.FormattingEnabled = true;
			checkedListBoxOffDays.HorizontalScrollbar = true;
			checkedListBoxOffDays.Items.AddRange(new object[7]
			{
				"Friday",
				"Saturday",
				"Sunday",
				"Monday",
				"Tuesday",
				"Wednesday",
				"Thursday"
			});
			checkedListBoxOffDays.Location = new System.Drawing.Point(79, 17);
			checkedListBoxOffDays.MultiColumn = true;
			checkedListBoxOffDays.Name = "checkedListBoxOffDays";
			checkedListBoxOffDays.Size = new System.Drawing.Size(502, 36);
			checkedListBoxOffDays.TabIndex = 0;
			dataGridOffDays.AllowAddNew = false;
			dataGridOffDays.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridOffDays.DisplayLayout.Appearance = appearance25;
			dataGridOffDays.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridOffDays.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridOffDays.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridOffDays.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridOffDays.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridOffDays.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridOffDays.DisplayLayout.MaxColScrollRegions = 1;
			dataGridOffDays.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridOffDays.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridOffDays.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridOffDays.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridOffDays.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridOffDays.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridOffDays.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridOffDays.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridOffDays.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridOffDays.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridOffDays.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridOffDays.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridOffDays.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridOffDays.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridOffDays.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridOffDays.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridOffDays.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGridOffDays.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridOffDays.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridOffDays.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridOffDays.Location = new System.Drawing.Point(12, 101);
			dataGridOffDays.Name = "dataGridOffDays";
			dataGridOffDays.ShowClearMenu = true;
			dataGridOffDays.ShowDeleteMenu = true;
			dataGridOffDays.ShowInsertMenu = true;
			dataGridOffDays.ShowMoveRowsMenu = true;
			dataGridOffDays.Size = new System.Drawing.Size(687, 260);
			dataGridOffDays.TabIndex = 2;
			dataGridOffDays.Text = "dataGridOffDays";
			groupBox1.Controls.Add(dateTimePickerFromDate);
			groupBox1.Controls.Add(dateTimePickerToDate);
			groupBox1.Controls.Add(buttonLoadOffDays);
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(label1);
			groupBox1.Location = new System.Drawing.Point(12, 3);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(685, 92);
			groupBox1.TabIndex = 1;
			groupBox1.TabStop = false;
			groupBox1.Text = "Select and  Load Days";
			dateTimePickerFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerFromDate.Location = new System.Drawing.Point(67, 54);
			dateTimePickerFromDate.Name = "dateTimePickerFromDate";
			dateTimePickerFromDate.Size = new System.Drawing.Size(112, 21);
			dateTimePickerFromDate.TabIndex = 116;
			dateTimePickerToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerToDate.Location = new System.Drawing.Point(219, 54);
			dateTimePickerToDate.Name = "dateTimePickerToDate";
			dateTimePickerToDate.Size = new System.Drawing.Size(111, 21);
			dateTimePickerToDate.TabIndex = 0;
			buttonLoadOffDays.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonLoadOffDays.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonLoadOffDays.BackColor = System.Drawing.Color.DarkGray;
			buttonLoadOffDays.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonLoadOffDays.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonLoadOffDays.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonLoadOffDays.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonLoadOffDays.Location = new System.Drawing.Point(570, 54);
			buttonLoadOffDays.Name = "buttonLoadOffDays";
			buttonLoadOffDays.Size = new System.Drawing.Size(96, 24);
			buttonLoadOffDays.TabIndex = 15;
			buttonLoadOffDays.Text = "&Load";
			buttonLoadOffDays.UseVisualStyleBackColor = false;
			buttonLoadOffDays.Click += new System.EventHandler(buttonLoadOffDays_Click);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(187, 60);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(26, 13);
			label3.TabIndex = 112;
			label3.Text = "To :";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 60);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(38, 13);
			label2.TabIndex = 109;
			label2.Text = "From :";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(9, 25);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(51, 13);
			label1.TabIndex = 109;
			label1.Text = "Off day :";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator3,
				toolStripButtonOpenList,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripButton1,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(725, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
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
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(55, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(86, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(57, 28);
			toolStripButton1.Text = "&Print";
			toolStripButton1.ToolTipText = "Print (Ctrl+P)";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 569);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(725, 40);
			panelButtons.TabIndex = 11;
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(111, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 18;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click_1);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(212, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 17;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(725, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(615, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 2;
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
			ultraTabControlDays.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControlDays.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControlDays.Controls.Add(tabPageGeneral);
			ultraTabControlDays.Controls.Add(ultraTabPageControl1);
			ultraTabControlDays.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			ultraTabControlDays.Location = new System.Drawing.Point(7, 152);
			ultraTabControlDays.MinTabWidth = 80;
			ultraTabControlDays.Name = "ultraTabControlDays";
			ultraTabControlDays.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControlDays.Size = new System.Drawing.Size(712, 411);
			ultraTabControlDays.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControlDays.TabIndex = 10;
			appearance37.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance37;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&Holidays";
			ultraTab2.TabPage = ultraTabPageControl1;
			ultraTab2.Text = "&OffDays";
			ultraTabControlDays.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabControlDays.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Standard;
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(710, 390);
			nonDirtyPanel1.Controls.Add(textBoxCalendarName);
			nonDirtyPanel1.Controls.Add(labelCalendarName);
			nonDirtyPanel1.Controls.Add(label8);
			nonDirtyPanel1.Controls.Add(textBoxRemarks);
			nonDirtyPanel1.Controls.Add(ultraFormattedLinkLabel5);
			nonDirtyPanel1.Controls.Add(textBoxCalendarCode);
			nonDirtyPanel1.Location = new System.Drawing.Point(0, 33);
			nonDirtyPanel1.Name = "nonDirtyPanel1";
			nonDirtyPanel1.Size = new System.Drawing.Size(705, 113);
			nonDirtyPanel1.TabIndex = 0;
			textBoxCalendarName.BackColor = System.Drawing.Color.White;
			textBoxCalendarName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCalendarName.CustomReportFieldName = "";
			textBoxCalendarName.CustomReportKey = "";
			textBoxCalendarName.CustomReportValueType = 1;
			textBoxCalendarName.IsComboTextBox = false;
			textBoxCalendarName.IsRequired = true;
			textBoxCalendarName.Location = new System.Drawing.Point(135, 30);
			textBoxCalendarName.MaxLength = 64;
			textBoxCalendarName.Name = "textBoxCalendarName";
			textBoxCalendarName.Size = new System.Drawing.Size(396, 20);
			textBoxCalendarName.TabIndex = 1;
			textBoxCalendarName.TextChanged += new System.EventHandler(textBoxCalendarName_TextChanged);
			labelCalendarName.AutoSize = true;
			labelCalendarName.BackColor = System.Drawing.Color.Transparent;
			labelCalendarName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCalendarName.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCalendarName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelCalendarName.IsFieldHeader = false;
			labelCalendarName.IsRequired = false;
			labelCalendarName.Location = new System.Drawing.Point(15, 33);
			labelCalendarName.Name = "labelCalendarName";
			labelCalendarName.PenWidth = 1f;
			labelCalendarName.ShowBorder = false;
			labelCalendarName.Size = new System.Drawing.Size(98, 13);
			labelCalendarName.TabIndex = 107;
			labelCalendarName.Text = "Calendar Name :";
			labelCalendarName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(17, 57);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(52, 13);
			label8.TabIndex = 22;
			label8.Text = "Remarks:";
			textBoxRemarks.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
			textBoxRemarks.Location = new System.Drawing.Point(135, 53);
			textBoxRemarks.MaxLength = 255;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.Size = new System.Drawing.Size(517, 54);
			textBoxRemarks.TabIndex = 2;
			textBoxRemarks.TextChanged += new System.EventHandler(textBoxRemarks_TextChanged);
			appearance38.FontData.BoldAsString = "True";
			appearance38.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance38;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(18, 8);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(88, 15);
			ultraFormattedLinkLabel5.TabIndex = 4;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Calendar Code:";
			appearance39.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance39;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			textBoxCalendarCode.BackColor = System.Drawing.Color.White;
			textBoxCalendarCode.CustomReportFieldName = "";
			textBoxCalendarCode.CustomReportKey = "";
			textBoxCalendarCode.CustomReportValueType = 1;
			textBoxCalendarCode.ForeColor = System.Drawing.Color.Black;
			textBoxCalendarCode.IsComboTextBox = false;
			textBoxCalendarCode.Location = new System.Drawing.Point(135, 7);
			textBoxCalendarCode.MaxLength = 255;
			textBoxCalendarCode.Name = "textBoxCalendarCode";
			textBoxCalendarCode.Size = new System.Drawing.Size(152, 20);
			textBoxCalendarCode.TabIndex = 0;
			textBoxCalendarCode.TabStop = false;
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
			base.ClientSize = new System.Drawing.Size(725, 609);
			base.Controls.Add(ultraTabControlDays);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(nonDirtyPanel1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "HolidayCalendarForm";
			Text = "Holiday Calendar";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(HolidayCalendarForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridHolidays).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItem).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridOffDays).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControlDays).EndInit();
			ultraTabControlDays.ResumeLayout(false);
			nonDirtyPanel1.ResumeLayout(false);
			nonDirtyPanel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
