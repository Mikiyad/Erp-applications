using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Employees;
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
	public class EmployeeAppraisalForm : Form, IForm
	{
		private bool supressExpenseMessage;

		private EmployeeAppraisalData currentData;

		private const string TABLENAME_CONST = "Employee_Appraisal";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private decimal appriasalPointFrom = CompanyPreferences.AppraisalRangeFrom;

		private decimal appriasalPointTo = CompanyPreferences.AppraisalRangeTo;

		private decimal remarkvalidationPoint = CompanyPreferences.RemarkValidation;

		private string EmployeeID = "";

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

		private UltraFormattedLinkLabel linkLabelVoucherNumber;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private TextBox textBoxEmployee;

		private DataEntryGrid dataGridItems;

		private ToolStripButton toolStripButtonOpenList;

		private UltraFormattedLinkLabel LabelEmployee;

		private TextBox textBoxPosition;

		private PositionComboBox comboBoxPosition;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private SysDocComboBox comboBoxSysDoc;

		private XPButton buttonActivityHistory;

		private EmployeeFilterComboBox comboBoxEmployee;

		public ScreenAreas ScreenArea => ScreenAreas.Project;

		public int ScreenID => 4002;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
				}
				SysDocComboBox sysDocComboBox = comboBoxSysDoc;
				enabled = (textBoxVoucherNumber.Enabled = isNewRecord);
				sysDocComboBox.Enabled = enabled;
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

		public EmployeeAppraisalForm()
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
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			base.FormClosing += ItemGroupDetailsForm_FormClosing;
			dataGridItems.BeforeRowActivate += dataGridItems_BeforeRowActivate;
			EmployeeReportToID();
		}

		public void EmployeeReportToID()
		{
			comboBoxEmployee.LoadData();
		}

		private void dataGridItems_BeforeRowActivate(object sender, RowEventArgs e)
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
			try
			{
				if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow != null && dataGridItems.ActiveRow != null)
				{
					if (e.Cell.Column.Key == "Rating")
					{
						decimal result = default(decimal);
						decimal result2 = default(decimal);
						decimal num = default(decimal);
						decimal.TryParse(dataGridItems.ActiveCell.Row.Cells["Rating"].Value.ToString(), out result);
						decimal.TryParse(dataGridItems.ActiveCell.Row.Cells["Weightage"].Value.ToString(), out result2);
						num = result * result2 / 100m;
						dataGridItems.ActiveCell.Row.Cells["Points"].Value = num;
					}
					if (e.Cell.Column.Key == "Rating")
					{
						decimal result3 = default(decimal);
						decimal.TryParse(dataGridItems.ActiveCell.Row.Cells["Rating"].Value.ToString(), out result3);
						if (result3 < appriasalPointFrom || result3 > appriasalPointTo)
						{
							ErrorHelper.InformationMessage("Rating Should range between " + appriasalPointFrom + " and " + appriasalPointTo);
						}
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
			if (dataGridItems.ActiveRow == null || !(dataGridItems.ActiveCell.Column.Key.ToString() == "Amount"))
			{
				return;
			}
			checked
			{
				for (int i = dataGridItems.ActiveCell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
				{
					if (dataGridItems.Rows[i].Cells["Amount"].Value.ToString() != "")
					{
						dataGridItems.Rows[i].Cells["Amount"].Value = dataGridItems.ActiveCell.Value;
					}
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
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeAppraisalData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeAppraisalTable.Rows[0] : currentData.EmployeeAppraisalTable.NewRow();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["Note"] = textBoxNote.Text.Trim();
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["PositionID"] = comboBoxPosition.SelectedID;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.EmployeeAppraisalTable.Rows.Add(dataRow);
				}
				currentData.EmployeeAppraisalDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.EmployeeAppraisalDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow2["KPIParameter"] = row.Cells["KPI Parameter"].Value.ToString();
					dataRow2["Weightage"] = row.Cells["Weightage"].Value.ToString();
					if (row.Cells["Scale"].Value.ToString() != "")
					{
						dataRow2["Scale"] = row.Cells["Scale"].Value.ToString();
					}
					else
					{
						dataRow2["Scale"] = DBNull.Value;
					}
					if (row.Cells["Target"].Value.ToString() != "")
					{
						dataRow2["Target"] = decimal.Parse(row.Cells["Target"].Value.ToString());
					}
					else
					{
						dataRow2["Target"] = 0;
					}
					if (row.Cells["Actual"].Value.ToString() != "")
					{
						dataRow2["Actual"] = decimal.Parse(row.Cells["Actual"].Value.ToString());
					}
					else
					{
						dataRow2["Actual"] = 0;
					}
					if (row.Cells["ACH"].Value.ToString() != "")
					{
						dataRow2["ACH"] = decimal.Parse(row.Cells["ACH"].Value.ToString());
					}
					else
					{
						dataRow2["ACH"] = 0;
					}
					if (row.Cells["Rating"].Value.ToString() != "")
					{
						dataRow2["Rating"] = decimal.Parse(row.Cells["Rating"].Value.ToString());
					}
					else
					{
						dataRow2["Rating"] = 0;
					}
					if (row.Cells["Points"].Value.ToString() != "")
					{
						dataRow2["Points"] = decimal.Parse(row.Cells["Points"].Value.ToString());
					}
					else
					{
						dataRow2["Points"] = 0;
					}
					dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
					dataRow2["RowIndex"] = row.Cells["Sl No."].Value.ToString();
					dataRow2.EndEdit();
					currentData.EmployeeAppraisalDetailTable.Rows.Add(dataRow2);
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
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables["Employee_Appraisal"].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Employee_Appraisal"].Rows[0];
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					dateTimePickerDate.Text = dataRow["TransactionDate"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
					comboBoxPosition.SelectedID = dataRow["PositionID"].ToString();
					if (currentData.Tables.Contains("Employee_Appraisal_Detail") && currentData.EmployeeAppraisalDetailTable.Rows.Count != 0)
					{
						DataTable dataTable = dataGridItems.DataSource as DataTable;
						dataTable.Rows.Clear();
						dataGridItems.BeginUpdate();
						foreach (DataRow row in currentData.Tables["Employee_Appraisal_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["KPI Parameter"] = row["KPIParameter"].ToString();
							dataRow3["Weightage"] = row["Weightage"].ToString();
							dataRow3["Scale"] = row["Scale"].ToString();
							dataRow3["Target"] = row["Target"].ToString();
							dataRow3["Remarks"] = row["Remarks"].ToString();
							if (row["Points"] != DBNull.Value)
							{
								dataRow3["Points"] = decimal.Parse(row["Points"].ToString());
							}
							else
							{
								dataRow3["Points"] = DBNull.Value;
							}
							if (row["Actual"] != DBNull.Value)
							{
								dataRow3["Actual"] = decimal.Parse(row["Actual"].ToString());
							}
							else
							{
								dataRow3["Actual"] = DBNull.Value;
							}
							if (row["ACH"] != DBNull.Value)
							{
								dataRow3["ACH"] = decimal.Parse(row["ACH"].ToString());
							}
							else
							{
								dataRow3["ACH"] = DBNull.Value;
							}
							if (row["Rating"] != DBNull.Value)
							{
								dataRow3["Rating"] = decimal.Parse(row["Rating"].ToString());
							}
							else
							{
								dataRow3["Rating"] = DBNull.Value;
							}
							dataRow3["Sl No."] = row["RowIndex"].ToString();
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
				dataGridItems.DisplayLayout.Bands[0].Summaries.Clear();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Sl No.");
				dataTable.Columns.Add("KPI Parameter");
				dataTable.Columns.Add("Scale");
				dataTable.Columns.Add("Target");
				dataTable.Columns.Add("Actual");
				dataTable.Columns.Add("ACH");
				dataTable.Columns.Add("Rating");
				dataTable.Columns.Add("Weightage", typeof(decimal));
				dataTable.Columns.Add("Points", typeof(decimal));
				dataTable.Columns.Add("Remarks");
				dataGridItems.DataSource = dataTable;
				dataTable = new DataTable("EmployeeAppraisalDetails");
				dataGridItems.DisplayLayout.Bands[0].Columns["Sl No."].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sl No."].Width = checked(5 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["KPI Parameter"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Weightage"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Scale"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Target"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Points"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Weightage"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Points"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["KPI Parameter"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["KPI Parameter"].VertScrollBar = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Width = checked(50 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["KPI Parameter"].Width = checked(50 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Scale"].Width = checked(30 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Target"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Actual"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["ACH"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Rating"].Width = checked(20 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Weightage"].Header.Caption = "Weightage(%)";
				dataGridItems.DisplayLayout.Bands[0].Columns["ACH"].Header.Caption = "Actual(%)";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalWeightage", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Weightage"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalWeightage"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalWeightage"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalWeightage"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalWeightage"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalPoints", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Points"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalPoints"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalPoints"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalPoints"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalPoints"].DisplayFormat = "{0:n}";
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
					currentData = Factory.EmployeeAppraisalSystem.GetPositionByID(SystemDocID, voucherID);
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
				bool flag = (!isNewRecord) ? Factory.EmployeeAppraisalSystem.UpdatePosition(currentData) : Factory.EmployeeAppraisalSystem.CreatePosition(currentData);
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
			DateTime result = new DateTime(2014, 1, 1);
			DateTime result2 = new DateTime(2014, 1, 1);
			DateTime d = new DateTime(1, 1, 1);
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Position", "AppraisalFromDate", "PositionID", comboBoxPosition.SelectedID);
			object fieldValue2 = Factory.DatabaseSystem.GetFieldValue("Position", "AppraisalToDate", "PositionID", comboBoxPosition.SelectedID);
			DateTime.TryParse(fieldValue.ToString(), out result);
			DateTime.TryParse(fieldValue2.ToString(), out result2);
			if (result != d && result2 != d && (dateTimePickerDate.Value < result || dateTimePickerDate.Value > result2))
			{
				ErrorHelper.InformationMessage("Date Should be in range between   " + result.ToShortDateString() + " and " + result2.ToShortDateString());
				return false;
			}
			for (int i = 0; i < dataGridItems.Rows.Count; i = checked(i + 1))
			{
				UltraGridRow ultraGridRow = dataGridItems.Rows[i];
				decimal result3 = default(decimal);
				decimal.TryParse(ultraGridRow.Cells["Rating"].Value.ToString(), out result3);
				if (result3 > remarkvalidationPoint && ultraGridRow.Cells["Remarks"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please mention the remarks for Top points ");
					dataGridItems.Rows[i].Activate();
					return false;
				}
				if (result3 < appriasalPointFrom || result3 > appriasalPointTo)
				{
					ErrorHelper.InformationMessage("Rating Should range between " + appriasalPointFrom + " and " + appriasalPointTo);
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
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxEmployee.Clear();
				comboBoxPosition.Clear();
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
				return Factory.EmployeeAppraisalSystem.DeletePosition(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID);
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
			string nextID = DatabaseHelper.GetNextID("Employee_Appraisal", "VoucherID", textBoxVoucherNumber.Text);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Employee_Appraisal", "VoucherID", textBoxVoucherNumber.Text);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Employee_Appraisal", "VoucherID");
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Employee_Appraisal", "VoucherID");
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Employee_Appraisal", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				comboBoxSysDoc.FilterByType(SysDocTypes.EmployeeAppraisal);
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
				return Factory.SystemDocumentSystem.GetNextDocumentNumber("Employee_Appraisal", "VoucherID");
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
					Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.JobTimesheet);
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
					_ = textBoxVoucherNumber.Text;
					DataSet employeeAppraisalToPrint = Factory.EmployeeAppraisalSystem.GetEmployeeAppraisalToPrint(SystemDocID, textBoxVoucherNumber.Text);
					if (employeeAppraisalToPrint == null || employeeAppraisalToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(employeeAppraisalToPrint, SystemDocID, "Employee Appraisal", SysDocTypes.None, isPrint, showPrintDialog);
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
			return "Salary Addition";
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

		private void textBoxVoucherNumber_TextChanged(object sender, EventArgs e)
		{
		}

		private void checkBoxMultiPeriod_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void checkBoxMultiPeriod_CheckStateChanged(object sender, EventArgs e)
		{
		}

		private void buttonFillDetails_Click(object sender, EventArgs e)
		{
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

		private void toolStripSeparator2_Click(object sender, EventArgs e)
		{
		}

		public void EmployeeKPIList(string EmployeeID)
		{
			DataSet employeeKPIList = Factory.EmployeeAppraisalSystem.GetEmployeeKPIList(EmployeeID);
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			dataTable.Rows.Clear();
			foreach (DataRow row in employeeKPIList.Tables["Position_Details"].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["Sl No."] = row["RowIndex"];
				dataRow2["KPI Parameter"] = row["KPIParameter"];
				dataRow2["Weightage"] = row["Weightage"];
				dataRow2["Scale"] = row["Scale"];
				dataRow2["Target"] = row["Target"];
				comboBoxPosition.SelectedID = row["PositionID"].ToString();
				dataRow2.EndEdit();
				dataTable.Rows.Add(dataRow2);
			}
			dataTable.AcceptChanges();
		}

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			EmployeeKPIList(comboBoxEmployee.SelectedID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeAppraisalListFormObj);
		}

		private void LabelEmployee_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPosition(comboBoxPosition.SelectedID);
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

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.EmployeeAppraisal);
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "Employee_Appraisal", "VoucherID");
		}

		private void buttonActivityHistory_Click(object sender, EventArgs e)
		{
			EmployeeAppraisalHistoryListForm employeeAppraisalHistoryListForm = new EmployeeAppraisalHistoryListForm();
			employeeAppraisalHistoryListForm.EmployeeID = comboBoxEmployee.SelectedID;
			employeeAppraisalHistoryListForm.ShowDialog();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Projects.EmployeeAppraisalForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
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
			comboBoxEmployee = new Micromind.DataControls.EmployeeFilterComboBox();
			textBoxEmployee = new System.Windows.Forms.TextBox();
			buttonActivityHistory = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			textBoxPosition = new System.Windows.Forms.TextBox();
			comboBoxPosition = new Micromind.DataControls.PositionComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			LabelEmployee = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			LabelPeriod = new Micromind.UISupport.MMLabel();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPosition).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(877, 31);
			toolStrip1.TabIndex = 3;
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
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(34, 28);
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
			toolStripSeparator2.Click += new System.EventHandler(toolStripSeparator2_Click);
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
			linePanelDown.TabIndex = 0;
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
			dateTimePickerDate.Location = new System.Drawing.Point(484, 3);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(139, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(306, 3);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(125, 20);
			textBoxVoucherNumber.TabIndex = 1;
			textBoxVoucherNumber.TextChanged += new System.EventHandler(textBoxVoucherNumber_TextChanged);
			textBoxNote.Location = new System.Drawing.Point(97, 76);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(580, 28);
			textBoxNote.TabIndex = 7;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(15, 83);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			panelDetails.Controls.Add(comboBoxEmployee);
			panelDetails.Controls.Add(buttonActivityHistory);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(textBoxPosition);
			panelDetails.Controls.Add(comboBoxPosition);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(LabelEmployee);
			panelDetails.Controls.Add(textBoxEmployee);
			panelDetails.Controls.Add(linkLabelVoucherNumber);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(LabelPeriod);
			panelDetails.Location = new System.Drawing.Point(0, 33);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(855, 111);
			panelDetails.TabIndex = 0;
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CalcManager = ultraCalcManager1;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = textBoxEmployee;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(97, 30);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = false;
			comboBoxEmployee.Size = new System.Drawing.Size(139, 20);
			comboBoxEmployee.TabIndex = 3;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployee.SelectedIndexChanged += new System.EventHandler(comboBoxEmployee_SelectedIndexChanged);
			textBoxEmployee.Location = new System.Drawing.Point(239, 30);
			textBoxEmployee.MaxLength = 15;
			textBoxEmployee.Name = "textBoxEmployee";
			textBoxEmployee.ReadOnly = true;
			textBoxEmployee.Size = new System.Drawing.Size(189, 20);
			textBoxEmployee.TabIndex = 4;
			buttonActivityHistory.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonActivityHistory.BackColor = System.Drawing.Color.Silver;
			buttonActivityHistory.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonActivityHistory.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonActivityHistory.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonActivityHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonActivityHistory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonActivityHistory.Location = new System.Drawing.Point(743, 47);
			buttonActivityHistory.Name = "buttonActivityHistory";
			buttonActivityHistory.Size = new System.Drawing.Size(96, 24);
			buttonActivityHistory.TabIndex = 8;
			buttonActivityHistory.Text = "Activity History";
			buttonActivityHistory.UseVisualStyleBackColor = false;
			buttonActivityHistory.Click += new System.EventHandler(buttonActivityHistory_Click);
			ultraFormattedLinkLabel1.AutoSize = true;
			appearance13.FontData.BoldAsString = "True";
			ultraFormattedLinkLabel1.LinkAppearance = appearance13;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 5);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(42, 14);
			ultraFormattedLinkLabel1.TabIndex = 166;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Doc ID:";
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CalcManager = ultraCalcManager1;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance14;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance15.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance15.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance15.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance15;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance16;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance17.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance17.BackColor2 = System.Drawing.SystemColors.Control;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance17;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance18;
			appearance19.BackColor = System.Drawing.SystemColors.Highlight;
			appearance19.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance19;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance20;
			appearance21.BorderColor = System.Drawing.Color.Silver;
			appearance21.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance21;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance22.BackColor = System.Drawing.SystemColors.Control;
			appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance22.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance22.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance22;
			appearance23.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance23;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance24;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance25.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance25;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(98, 4);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(109, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxPosition.Location = new System.Drawing.Point(239, 52);
			textBoxPosition.MaxLength = 15;
			textBoxPosition.Name = "textBoxPosition";
			textBoxPosition.ReadOnly = true;
			textBoxPosition.Size = new System.Drawing.Size(189, 20);
			textBoxPosition.TabIndex = 6;
			comboBoxPosition.Assigned = false;
			comboBoxPosition.CalcManager = ultraCalcManager1;
			comboBoxPosition.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPosition.CustomReportFieldName = "";
			comboBoxPosition.CustomReportKey = "";
			comboBoxPosition.CustomReportValueType = 1;
			comboBoxPosition.DescriptionTextBox = textBoxPosition;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPosition.DisplayLayout.Appearance = appearance26;
			comboBoxPosition.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPosition.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance27.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance27.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance27.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPosition.DisplayLayout.GroupByBox.Appearance = appearance27;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPosition.DisplayLayout.GroupByBox.BandLabelAppearance = appearance28;
			comboBoxPosition.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance29.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance29.BackColor2 = System.Drawing.SystemColors.Control;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPosition.DisplayLayout.GroupByBox.PromptAppearance = appearance29;
			comboBoxPosition.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPosition.DisplayLayout.MaxRowScrollRegions = 1;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			appearance30.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPosition.DisplayLayout.Override.ActiveCellAppearance = appearance30;
			appearance31.BackColor = System.Drawing.SystemColors.Highlight;
			appearance31.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPosition.DisplayLayout.Override.ActiveRowAppearance = appearance31;
			comboBoxPosition.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPosition.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPosition.DisplayLayout.Override.CardAreaAppearance = appearance32;
			appearance33.BorderColor = System.Drawing.Color.Silver;
			appearance33.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPosition.DisplayLayout.Override.CellAppearance = appearance33;
			comboBoxPosition.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPosition.DisplayLayout.Override.CellPadding = 0;
			appearance34.BackColor = System.Drawing.SystemColors.Control;
			appearance34.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance34.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPosition.DisplayLayout.Override.GroupByRowAppearance = appearance34;
			appearance35.TextHAlignAsString = "Left";
			comboBoxPosition.DisplayLayout.Override.HeaderAppearance = appearance35;
			comboBoxPosition.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPosition.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			comboBoxPosition.DisplayLayout.Override.RowAppearance = appearance36;
			comboBoxPosition.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance37.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPosition.DisplayLayout.Override.TemplateAddRowAppearance = appearance37;
			comboBoxPosition.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPosition.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPosition.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPosition.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPosition.Editable = true;
			comboBoxPosition.FilterString = "";
			comboBoxPosition.HasAllAccount = false;
			comboBoxPosition.HasCustom = false;
			comboBoxPosition.IsDataLoaded = false;
			comboBoxPosition.Location = new System.Drawing.Point(97, 53);
			comboBoxPosition.MaxDropDownItems = 12;
			comboBoxPosition.Name = "comboBoxPosition";
			comboBoxPosition.ShowInactiveItems = false;
			comboBoxPosition.ShowQuickAdd = true;
			comboBoxPosition.Size = new System.Drawing.Size(139, 20);
			comboBoxPosition.TabIndex = 5;
			comboBoxPosition.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(15, 57);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(47, 14);
			ultraFormattedLinkLabel5.TabIndex = 164;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Position:";
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked_1);
			LabelEmployee.AutoSize = true;
			LabelEmployee.Location = new System.Drawing.Point(12, 32);
			LabelEmployee.Name = "LabelEmployee";
			LabelEmployee.Size = new System.Drawing.Size(56, 14);
			LabelEmployee.TabIndex = 162;
			LabelEmployee.TabStop = true;
			LabelEmployee.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			LabelEmployee.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			LabelEmployee.Value = "Employee:";
			LabelEmployee.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(LabelEmployee_LinkClicked);
			linkLabelVoucherNumber.AutoSize = true;
			appearance38.FontData.BoldAsString = "True";
			linkLabelVoucherNumber.LinkAppearance = appearance38;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(223, 6);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(71, 14);
			linkLabelVoucherNumber.TabIndex = 135;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Doc Number:";
			linkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVoucherNumber_LinkClicked);
			LabelPeriod.AutoSize = true;
			LabelPeriod.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			LabelPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			LabelPeriod.IsFieldHeader = false;
			LabelPeriod.IsRequired = true;
			LabelPeriod.Location = new System.Drawing.Point(438, 6);
			LabelPeriod.Name = "LabelPeriod";
			LabelPeriod.PenWidth = 1f;
			LabelPeriod.ShowBorder = false;
			LabelPeriod.Size = new System.Drawing.Size(42, 13);
			LabelPeriod.TabIndex = 2;
			LabelPeriod.Text = "Date :";
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(12, 398);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(843, 83);
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
			formManager.TabIndex = 5;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.CalcManager = ultraCalcManager1;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance39;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance40;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance41;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance42.BackColor2 = System.Drawing.SystemColors.Control;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance42;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance43;
			appearance44.BackColor = System.Drawing.SystemColors.Highlight;
			appearance44.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance44;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance45;
			appearance46.BorderColor = System.Drawing.Color.Silver;
			appearance46.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance46;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance47.BackColor = System.Drawing.SystemColors.Control;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance47;
			appearance48.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance48;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance49;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance50;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(11, 148);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(846, 363);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			ultraGridPrintDocument1.DocumentName = "Inventory Adjustment - List";
			ultraGridPrintDocument1.Grid = dataGridItems;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(877, 568);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(885, 595);
			base.Name = "EmployeeAppraisalForm";
			Text = "Employee Appraisal";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPosition).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
