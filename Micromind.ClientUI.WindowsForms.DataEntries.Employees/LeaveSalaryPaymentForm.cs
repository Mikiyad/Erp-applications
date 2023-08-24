using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
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
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class LeaveSalaryPaymentForm : Form, IForm
	{
		private EmployeeActivityData currentData;

		private const string TABLENAME_CONST = "Employee_Leave_Payment";

		private const string IDFIELD_CONST = "VoucherID";

		private const string LEAVEPAYIDFIELD_CONST = "LeavePaymentID";

		private bool isNewRecord = true;

		private UltraGridRow row;

		private decimal payamount;

		private decimal basicsal;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonPayCash;

		private XPButton buttonSave;

		private FormManager formManager;

		private MMTextBox textBoxEmployeeName;

		private MMLabel mmLabel12;

		private GroupBox groupBox1;

		private MMTextBox textBoxPaymentNo;

		private XPButton buttonPayCheque;

		private AmountTextBox textBoxAmount;

		private MMLabel mmLabel5;

		private MMLabel mmLabel4;

		private EmployeeComboBox comboBoxEmployee;

		private MMLabel mmLabel7;

		private MMTextBox textBoxNote;

		private DateTimePicker dateTimePickerFrom;

		private ToolStrip toolStrip2;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripButton toolStripButton5;

		private ToolStripButton toolStripButtonPreview;

		private XPButton buttonNew;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel9;

		private MMLabel mmLabel8;

		private DateTimePicker dateTimePickerTo;

		private MMTextBox textBoxLeaveAppNo;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private MMLabel mmLabel3;

		private AmountTextBox textBoxTicketAmount;

		private AmountTextBox textBoxNetTotal;

		private MMLabel mmLabel14;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonDistribution;

		private AmountTextBox textBoxSalary;

		private MMLabel mmLabel6;

		private MMLabel mmLabel10;

		private NumberTextBox textBoxEligibleDays;

		private NumberTextBox textBoxDays;

		private CheckBox checkBoxsalary;

		private CheckBox checkBoxonemonth;

		private MMLabel mmLabel13;

		private PayrollItemComboBox comboBoxPayrollItemDeduction;

		private AmountTextBox textBoxDeductions;

		private CheckBox checkBoxTotalTaken;

		private XPButton xpButtonSelect;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel linkLabelVoucherNumber;

		private TextBox textBoxVoucherNumber;

		private ToolStripButton toolStripButtonAttach;

		private SysDocComboBox comboBoxSysDoc;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerDate;

		private MMTextBox textBoxFromDepartment;

		private MMTextBox textBoxFromDivision;

		private MMTextBox textBoxDesignation;

		private MMTextBox textBoxFromLocation;

		private MMLabel mmLabel11;

		private MMLabel mmLabel15;

		private MMLabel mmLabel16;

		private MMLabel mmLabel17;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5033;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private decimal PayableAmount
		{
			get
			{
				return payamount;
			}
			set
			{
				payamount = value;
			}
		}

		private decimal BasicSal
		{
			get
			{
				return basicsal;
			}
			set
			{
				basicsal = value;
			}
		}

		private bool IsDirty => false;

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
					buttonDelete.Enabled = false;
					XPButton xPButton = buttonPayCash;
					enabled = (buttonPayCheque.Enabled = false);
					xPButton.Enabled = enabled;
					ToolStripButton toolStripButton = toolStripButtonPreview;
					ToolStripButton toolStripButton2 = toolStripButton5;
					bool flag3 = toolStripButtonAttach.Enabled = false;
					enabled = (toolStripButton2.Enabled = flag3);
					toolStripButton.Enabled = enabled;
					xpButtonSelect.Enabled = true;
					comboBoxEmployee.Enabled = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					XPButton xPButton2 = buttonPayCash;
					enabled = (buttonPayCheque.Enabled = true);
					xPButton2.Enabled = enabled;
					ToolStripButton toolStripButton3 = toolStripButtonPreview;
					ToolStripButton toolStripButton4 = toolStripButton5;
					bool flag3 = toolStripButtonAttach.Enabled = true;
					enabled = (toolStripButton4.Enabled = flag3);
					toolStripButton3.Enabled = enabled;
					xpButtonSelect.Enabled = false;
					comboBoxEmployee.Enabled = false;
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
				buttonSave.Enabled = value;
			}
		}

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		public LeaveSalaryPaymentForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EmployeeLeaveApprovalForm_Load;
			comboBoxEmployee.SelectedIndexChanged += comboBoxEmployee_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
		}

		private void LoadEmployeeDetails()
		{
			try
			{
				if (row != null)
				{
					decimal result = default(decimal);
					decimal d = default(decimal);
					decimal num = default(decimal);
					decimal num2 = default(decimal);
					decimal.TryParse(row.Cells["Basic"].Value.ToString(), out result);
					BasicSal = result;
					textBoxCode.Text = row.Cells["ActivityID"].Value.ToString();
					comboBoxEmployee.SelectedID = row.Cells["EmployeeID"].Value.ToString();
					textBoxLeaveAppNo.Text = row.Cells["Doc Number"].Value.ToString();
					if (row.Cells["Start Date"].Value != DBNull.Value)
					{
						dateTimePickerFrom.Value = DateTime.Parse(row.Cells["Start Date"].Value.ToString());
					}
					if (row.Cells["End Date"].Value != DBNull.Value)
					{
						dateTimePickerTo.Value = DateTime.Parse(row.Cells["End Date"].Value.ToString());
					}
					DateTime d2 = default(DateTime);
					if (row.Cells["JoiningDate"].Value != DBNull.Value)
					{
						d2 = DateTime.Parse(row.Cells["JoiningDate"].Value.ToString());
					}
					DateTime value = dateTimePickerFrom.Value;
					DateTime value2 = dateTimePickerTo.Value;
					decimal num3 = default(decimal);
					TimeSpan timeSpan = value.Add(TimeSpan.FromDays(1.0)) - d2;
					(timeSpan.Days / 30).ToString();
					num3 = decimal.Parse(row.Cells["PaidDays"].Value.ToString());
					TimeSpan timeSpan2 = value2.Add(TimeSpan.FromDays(1.0)) - value;
					_ = (decimal)(timeSpan.Days / 30) - (num3 + (decimal)timeSpan2.Days);
					textBoxDays.Text = timeSpan2.Days.ToString();
					decimal payableAmount = default(decimal);
					if (CompanyPreferences.Annual)
					{
						payableAmount = result * 12m / 365m;
					}
					else if (CompanyPreferences.ThirtyDays)
					{
						payableAmount = result / 30m;
					}
					else if (CompanyPreferences.DaysInMonth)
					{
						int value3 = DateTime.DaysInMonth(dateTimePickerFrom.Value.Year, dateTimePickerFrom.Value.Month);
						payableAmount = result / (decimal)value3;
					}
					if (checkBoxTotalTaken.Checked)
					{
						PayableAmount = payableAmount;
						if (PayableAmount > 0m)
						{
							d = PayableAmount * decimal.Parse(textBoxDays.Text);
						}
						textBoxAmount.Text = d.ToString();
					}
					else if (checkBoxonemonth.Checked)
					{
						PayableAmount = BasicSal;
						textBoxAmount.Text = PayableAmount.ToString();
					}
					else
					{
						PayableAmount = payableAmount;
						if (PayableAmount > 0m && !string.IsNullOrEmpty(textBoxEligibleDays.Text))
						{
							d = PayableAmount * decimal.Parse(textBoxEligibleDays.Text);
						}
						textBoxAmount.Text = d.ToString();
					}
					textBoxTicketAmount.Text = row.Cells["TicketAmount"].Value.ToString();
					num2 = textBoxTicketAmount.Value;
					num = d + num2;
					textBoxNetTotal.Value = num;
					CalculateTotal();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void dateDiff()
		{
			DateTime value = dateTimePickerFrom.Value;
			TimeSpan timeSpan = dateTimePickerTo.Value.Add(TimeSpan.FromDays(1.0)) - value;
			textBoxDays.Text = timeSpan.Days.ToString();
		}

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			DataSet employeeBriefInfo = Factory.EmployeeSystem.GetEmployeeBriefInfo(comboBoxEmployee.SelectedID);
			if (employeeBriefInfo != null && employeeBriefInfo.Tables.Count > 0 && employeeBriefInfo.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = employeeBriefInfo.Tables[0].Rows[0];
				textBoxFromLocation.Text = dataRow["WorkLocationName"].ToString();
				textBoxFromDivision.Text = dataRow["DivisionName"].ToString();
				textBoxFromDepartment.Text = dataRow["DepartmentName"].ToString();
				textBoxDesignation.Text = dataRow["PositionName"].ToString();
			}
			else
			{
				textBoxFromLocation.Clear();
				textBoxFromDivision.Clear();
				textBoxFromDepartment.Clear();
				textBoxDesignation.Clear();
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeActivityData(EmployeeActivityTypes.LeavePayment);
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeActivityTable.Rows[0] : currentData.EmployeeActivityTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["ActivityType"] = (byte)14;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeActivityTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.EmployeeLeavePaymentTable.Rows[0] : currentData.EmployeeLeavePaymentTable.NewRow());
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["LeavePaymentID"] = textBoxPaymentNo.Text;
				dataRow["LeaveApplicationNo"] = textBoxLeaveAppNo.Text;
				dataRow["StartDate"] = dateTimePickerFrom.Value;
				dataRow["EndDate"] = dateTimePickerTo.Value;
				dataRow["Amount"] = ((!string.IsNullOrEmpty(textBoxAmount.Text)) ? Convert.ToDecimal(textBoxAmount.Text) : 0m);
				dataRow["TicketAmount"] = ((!string.IsNullOrEmpty(textBoxTicketAmount.Text)) ? Convert.ToDecimal(textBoxTicketAmount.Text) : 0m);
				dataRow["Total"] = ((!string.IsNullOrEmpty(textBoxNetTotal.Text)) ? Convert.ToDecimal(textBoxNetTotal.Text) : 0m);
				dataRow["SalaryAmount"] = ((!string.IsNullOrEmpty(textBoxSalary.Text)) ? Convert.ToDecimal(textBoxSalary.Text) : 0m);
				dataRow["DeductionAmount"] = ((!string.IsNullOrEmpty(textBoxDeductions.Text)) ? Convert.ToDecimal(textBoxDeductions.Text) : 0m);
				dataRow["DeductionID"] = comboBoxPayrollItemDeduction.SelectedID;
				if (textBoxEligibleDays.Text == "" && checkBoxonemonth.Checked)
				{
					dataRow["EligibleDays"] = DBNull.Value;
				}
				else
				{
					dataRow["EligibleDays"] = ((!string.IsNullOrEmpty(textBoxEligibleDays.Text)) ? Convert.ToDecimal(textBoxEligibleDays.Text) : 0m);
				}
				dataRow["BasedOnLeaveTaken"] = checkBoxTotalTaken.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeLeavePaymentTable.Rows.Add(dataRow);
				}
				DataView defaultView = Factory.EmployeeActivitySystem.GetAllEmployeeApprovedLeaves(comboBoxEmployee.SelectedID).Tables[0].DefaultView;
				defaultView.RowFilter = "EmployeeID= '" + comboBoxEmployee.SelectedID + "'";
				DataSet dataSet = new DataSet();
				DataTable table = defaultView.ToTable();
				dataSet.Tables.Add(table);
				DataRow dataRow2 = dataSet.Tables[0].Rows[0];
				dataRow2["Basic"] = textBoxAmount.Text;
				dataRow2["TicketAmount"] = textBoxTicketAmount.Text;
				currentData.Merge(dataSet);
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			comboBoxEmployee.Focus();
		}

		public void LoadData(string voucherId)
		{
			try
			{
				if (!base.IsDisposed && !(voucherId.Trim() == "") && CanClose())
				{
					currentData = Factory.EmployeeActivitySystem.GetEmployeeLeavePaymentByID(SystemDocID, voucherId);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
					}
					else
					{
						FillData();
						IsNewRecord = false;
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
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.Tables["Employee_Activity"].Rows[0];
			textBoxCode.Text = dataRow["ActivityID"].ToString();
			comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
			textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
			textBoxNote.Text = dataRow["Note"].ToString();
			dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
			dataRow = currentData.Tables["Employee_Leave_Payment"].Rows[0];
			comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
			textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
			textBoxPaymentNo.Text = dataRow["LeavePaymentID"].ToString();
			textBoxLeaveAppNo.Text = dataRow["LeaveApplicationNo"].ToString();
			dateTimePickerFrom.Value = Convert.ToDateTime(dataRow["StartDate"]);
			dateTimePickerTo.Value = Convert.ToDateTime(dataRow["EndDate"]);
			if (dataRow["BasedOnLeaveTaken"] != DBNull.Value && bool.Parse(dataRow["BasedOnLeaveTaken"].ToString()))
			{
				checkBoxTotalTaken.Checked = bool.Parse(dataRow["BasedOnLeaveTaken"].ToString());
			}
			else
			{
				checkBoxTotalTaken.Checked = false;
				if (dataRow["EligibleDays"].ToString() == string.Empty || dataRow["EligibleDays"].ToString() == "")
				{
					checkBoxonemonth.Checked = true;
				}
				else
				{
					checkBoxonemonth.Checked = false;
					textBoxEligibleDays.Text = dataRow["EligibleDays"].ToString();
				}
			}
			textBoxAmount.Text = dataRow["Amount"].ToString();
			textBoxTicketAmount.Text = dataRow["TicketAmount"].ToString();
			textBoxNetTotal.Text = dataRow["Total"].ToString();
			textBoxDeductions.Text = dataRow["DeductionAmount"].ToString();
			comboBoxPayrollItemDeduction.SelectedID = dataRow["DeductionID"].ToString();
			textBoxSalary.Text = dataRow["SalaryAmount"].ToString();
			dateDiff();
		}

		private bool SaveData()
		{
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
				bool flag = (!isNewRecord) ? Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.LeavePayment, isUpdate: true) : Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.LeavePayment, isUpdate: false);
				if (!flag)
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
			if (textBoxPaymentNo.Text == string.Empty || textBoxLeaveAppNo.Text == string.Empty || comboBoxEmployee.SelectedID == string.Empty)
			{
				ErrorHelper.WarningMessage("Please enter required fields.");
				return false;
			}
			return true;
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxPaymentNo.Text = textBoxVoucherNumber.Text;
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
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

		private void ClearForm()
		{
			textBoxNote.Clear();
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
			textBoxPaymentNo.Text = textBoxVoucherNumber.Text;
			comboBoxEmployee.Clear();
			textBoxEmployeeName.Clear();
			textBoxFromLocation.Clear();
			textBoxFromDivision.Clear();
			textBoxFromDepartment.Clear();
			textBoxDesignation.Clear();
			DateTime dateTime2 = dateTimePickerFrom.Value = (dateTimePickerTo.Value = DateTime.Now);
			textBoxLeaveAppNo.Clear();
			textBoxAmount.Clear();
			textBoxNote.Clear();
			textBoxTicketAmount.Clear();
			textBoxDeductions.Clear();
			comboBoxPayrollItemDeduction.Clear();
			textBoxSalary.Clear();
			textBoxNetTotal.Clear();
			textBoxNote.Clear();
			textBoxDays.Clear();
			checkBoxsalary.Checked = false;
			checkBoxonemonth.Checked = false;
			checkBoxTotalTaken.Checked = false;
			textBoxEligibleDays.Clear();
			row = null;
			comboBoxEmployee.Clear();
			textBoxLeaveAppNo.Clear();
			textBoxFromLocation.Clear();
			textBoxFromDivision.Clear();
			textBoxFromDepartment.Clear();
			textBoxDesignation.Clear();
			formManager.ResetDirty();
		}

		private void LoadLeavesToApprove()
		{
		}

		private void SponsorGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void SponsorGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.EmployeeActivitySystem.DeleteActivity(textBoxCode.Text, EmployeeActivityTypes.LeavePayment);
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

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
		{
			return true;
		}

		private void EmployeeLeaveApprovalForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					comboBoxSysDoc.FilterByType(SysDocTypes.EmployeeLeavePayment);
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
		}

		private void LoadEmployeeDetails(string id)
		{
			try
			{
				if (!(id == ""))
				{
					DataSet employeeLeaveInfo = Factory.EmployeeSystem.GetEmployeeLeaveInfo(id);
					if (employeeLeaveInfo != null && employeeLeaveInfo.Tables.Count > 0 && employeeLeaveInfo.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = employeeLeaveInfo.Tables[0].Rows[0];
						int num = Convert.ToInt32(dataRow["Leaves Eligible"]);
						int num2 = Convert.ToInt32(dataRow["Leaves Encash"]);
						textBoxLeaveAppNo.Text = checked(num - num2).ToString();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private string GetNextNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextNumber("Employee_Leave_Payment", "LeavePaymentID");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void buttonPayCheque_Click(object sender, EventArgs e)
		{
			try
			{
				ChequePaymentForm chequePaymentForm = new ChequePaymentForm();
				chequePaymentForm.EntityID = comboBoxEmployee.SelectedID;
				chequePaymentForm.EntityType = "E";
				chequePaymentForm.Amount = ((textBoxNetTotal.Text != string.Empty) ? Convert.ToDecimal(textBoxNetTotal.Text) : 0m);
				chequePaymentForm.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonPayCash_Click(object sender, EventArgs e)
		{
			try
			{
				CashPaymentForm cashPaymentForm = new CashPaymentForm();
				cashPaymentForm.EntityID = comboBoxEmployee.SelectedID;
				cashPaymentForm.EntityType = "E";
				cashPaymentForm.Amount = ((textBoxNetTotal.Text != string.Empty) ? Convert.ToDecimal(textBoxNetTotal.Text) : 0m);
				cashPaymentForm.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void btnPost_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure you want to post this leave encashment?") != DialogResult.No)
			{
				SaveData();
			}
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("Employee_Leave_Payment", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Employee_Leave_Payment", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Employee_Leave_Payment", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Employee_Leave_Payment", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID);
			}
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Employee_Leave_Payment", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					DataSet employeeActivityToPrint = Factory.EmployeeActivitySystem.GetEmployeeActivityToPrint(Convert.ToInt32(textBoxCode.Text));
					if (employeeActivityToPrint == null || employeeActivityToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(employeeActivityToPrint, "", "Leave Salary Payment", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButton5_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: false);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void textBoxAmount_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxTicketAmount_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void CalculateTotal()
		{
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal result3 = default(decimal);
			decimal num = default(decimal);
			decimal.TryParse(textBoxAmount.Text, out result);
			decimal.TryParse(textBoxTicketAmount.Text, out result2);
			decimal.TryParse(textBoxSalary.Text, out result3);
			decimal.TryParse(textBoxDeductions.Text, out decimal result4);
			num = result + result2 + result3 - result4;
			textBoxNetTotal.Value = num;
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = SystemDocID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void checkBoxsalary_CheckedChanged(object sender, EventArgs e)
		{
			DateTime value = dateTimePickerFrom.Value;
			if (checkBoxsalary.Checked)
			{
				DataSet salaryEmployeeSheetDetails = Factory.SalarySheetSystem.GetSalaryEmployeeSheetDetails(value.Month.ToString(), value.Year.ToString(), comboBoxEmployee.SelectedID);
				if (salaryEmployeeSheetDetails.Tables[0].Rows.Count > 0)
				{
					textBoxSalary.Text = salaryEmployeeSheetDetails.Tables[0].Rows[0]["NetSalary"].ToString();
				}
				else
				{
					textBoxSalary.Clear();
				}
			}
			else
			{
				textBoxSalary.Clear();
			}
		}

		private void textBoxSalary_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxEligibleDays_TextChanged(object sender, EventArgs e)
		{
			int result = 0;
			decimal num = default(decimal);
			int.TryParse(textBoxEligibleDays.Text, out result);
			num = PayableAmount * (decimal)result;
			textBoxAmount.Text = num.ToString();
		}

		public void LoadLeaveData(string EmployeeID, string EmployeeName, DateTime AsonDate, DateTime ToDate)
		{
			checked
			{
				try
				{
					DataSet dataSet = new DataSet();
					if (CanClose())
					{
						comboBoxEmployee.Text = EmployeeID;
						textBoxEmployeeName.Text = EmployeeName;
						dataSet = Factory.EmployeeLeaveDetailSystem.GetEmployeeLeaveAvailability(EmployeeID, AsonDate, ToDate, "");
						if (dataSet.Tables.Count == 0)
						{
							ErrorHelper.InformationMessage("Leave Settings in Employee Class has not Activated.");
						}
						else if (dataSet.Tables[0].Rows.Count == 0)
						{
							ErrorHelper.InformationMessage("Leave Settings in Employee Class has not Activated.");
						}
						else if (dataSet.Tables.Count > 0)
						{
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal result3 = default(decimal);
							int result4 = 0;
							int result5 = 0;
							int result6 = 0;
							int result7 = 0;
							bool result8 = false;
							bool result9 = false;
							DateTime result10 = new DateTime(2014, 1, 1);
							DateTime result11 = new DateTime(2014, 1, 1);
							DateTime d = new DateTime(1, 1, 1);
							if (dataSet.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "On Account")
							{
								for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
								{
									decimal.TryParse(dataSet.Tables[0].Rows[i]["1SET"].ToString(), out result);
									decimal.TryParse(dataSet.Tables[0].Rows[i]["2SET"].ToString(), out result2);
									decimal.TryParse(dataSet.Tables[0].Rows[i]["3SET"].ToString(), out result3);
									int.TryParse(dataSet.Tables[0].Rows[i]["openingLeavesTaken"].ToString(), out result4);
									int.TryParse(dataSet.Tables[0].Rows[i]["TotalTaken"].ToString(), out result5);
									int.TryParse(dataSet.Tables[0].Rows[i]["LeaveDayswithType"].ToString(), out result6);
									bool.TryParse(dataSet.Tables[0].Rows[i]["AnnualEligible"].ToString(), out result8);
									bool.TryParse(dataSet.Tables[0].Rows[i]["IsAnnual"].ToString(), out result9);
									if (result != 0m || result2 != 0m || result3 != 0m || (!result8 && result9))
									{
										result6 = 0;
									}
									decimal num = default(decimal);
									decimal num2 = default(decimal);
									num = result + result2 + result3 - (decimal)result4 + (decimal)result6;
									dataSet.Tables[0].Rows[i]["TotalLeaves"] = num;
									num2 = result + result2 + result3 - (decimal)result4 + (decimal)result6 - (decimal)result5;
									dataSet.Tables[0].Rows[i]["LeavesRemaining"] = num2;
									if (result9)
									{
										textBoxEligibleDays.Text = num2.ToString();
									}
								}
								dataSet.Tables[0].Columns.Remove("1SET");
								dataSet.Tables[0].Columns.Remove("2SET");
								dataSet.Tables[0].Columns.Remove("3SET");
								dataSet.Tables[0].Columns.Remove("EmployeeID");
								dataSet.Tables[0].Columns.Remove("AnnualEligible");
							}
							else if (dataSet.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "Calendar Year")
							{
								List<string> list = new List<string>();
								for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
								{
									int.TryParse(dataSet.Tables[0].Rows[j]["openingLeavesTaken"].ToString(), out result4);
									int.TryParse(dataSet.Tables[0].Rows[j]["TotalTaken"].ToString(), out result5);
									int.TryParse(dataSet.Tables[0].Rows[j]["LeaveDayswithType"].ToString(), out result6);
									DateTime.TryParse(dataSet.Tables[0].Rows[j]["FromDate"].ToString(), out result10);
									DateTime.TryParse(dataSet.Tables[0].Rows[j]["ToDate"].ToString(), out result11);
									string text = dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString();
									result9 = bool.Parse(dataSet.Tables[0].Rows[j]["IsAnnual"].ToString());
									DataView defaultView = dataSet.Tables[1].DefaultView;
									if (result9 && result10 != d && result11 != d)
									{
										defaultView.RowFilter = "FromDate='" + result10.ToString() + "' AND ToDate='" + result11.ToString() + "' AND LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
									}
									else if (result9 && result10 == d && result11 == d)
									{
										defaultView.RowFilter = "LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
									}
									else if (!result9)
									{
										defaultView.RowFilter = "LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
									}
									DataSet dataSet2 = new DataSet();
									DataTable dataTable = defaultView.ToTable();
									dataSet2.Tables.Add(dataTable);
									object value = (!(result10 != d) || !(result11 != d)) ? DBNull.Value : dataTable.Compute("Sum(DaysTaken)", "DaysTaken <> 0");
									dataSet.Tables[0].Rows[j]["TotalTaken"] = value;
									int.TryParse(dataSet.Tables[0].Rows[j]["TotalTaken"].ToString(), out result5);
									int.TryParse(dataSet.Tables[0].Rows[j]["AnnualAllowedDays"].ToString(), out result7);
									if (result9)
									{
										result6 = 0;
									}
									dataSet.Tables[0].Rows[j]["TotalLeaves"] = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7;
									dataSet.Tables[0].Rows[j]["LeavesRemaining"] = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result5;
									textBoxEligibleDays.Text = (result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result5).ToString();
									if (result10.Date < AsonDate.Date && result11.Date < AsonDate.Date && result7 != 0)
									{
										dataSet.Tables[0].Rows[j].Delete();
									}
									else
									{
										if (result7 == 0 && !list.Contains(dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString()))
										{
											list.Add(dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString());
										}
										else if (result7 == 0)
										{
											dataSet.Tables[0].Rows[j].Delete();
											continue;
										}
										int result12 = 0;
										int.TryParse(dataSet.Tables[0].Rows[j]["LeavesRemaining"].ToString(), out result12);
									}
								}
								dataSet.Tables[0].Columns.Remove("LeaveDayswithType");
								dataSet.Tables[0].Columns.Remove("EmployeeID");
								dataSet.AcceptChanges();
							}
							if (dataSet.Tables[0].Rows.Count > 0)
							{
								textBoxEmployeeName.Text = dataSet.Tables[0].Rows[0]["Name"].ToString();
								dataSet.Tables[0].Columns.Remove("Name");
								dataSet.Tables[0].Columns.Remove("Basedon");
								dataSet.Tables[0].Columns.Remove("LeaveTypeID");
								dataSet.AcceptChanges();
							}
						}
					}
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		private void checkBoxonemonth_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxonemonth.Checked)
			{
				textBoxEligibleDays.Text = "";
				textBoxEligibleDays.Enabled = false;
				textBoxAmount.Text = BasicSal.ToString();
				LoadEmployeeDetails();
			}
			else if (comboBoxEmployee.SelectedID != "")
			{
				LoadLeaveData(comboBoxEmployee.SelectedID, textBoxEmployeeName.Text, dateTimePickerFrom.Value, dateTimePickerTo.Value);
				textBoxEligibleDays.Enabled = true;
				LoadEmployeeDetails();
			}
		}

		private void textBoxDeductions_TextChanged(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void checkBoxTotalTaken_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxTotalTaken.Checked)
			{
				checkBoxonemonth.Checked = false;
				textBoxEligibleDays.Clear();
				checkBoxonemonth.Enabled = false;
				textBoxEligibleDays.Enabled = false;
				LoadEmployeeDetails();
			}
			else
			{
				checkBoxonemonth.Checked = false;
				textBoxEligibleDays.Clear();
				checkBoxonemonth.Enabled = true;
				textBoxEligibleDays.Enabled = true;
				LoadEmployeeDetails();
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.EmployeeLeavePayment);
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			DataSet allEmployeeApprovedLeaves = Factory.EmployeeActivitySystem.GetAllEmployeeApprovedLeaves(comboBoxEmployee.SelectedID);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = allEmployeeApprovedLeaves;
			selectDocumentDialog.Text = "Select Approved Leaves";
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["TAccountID"].Hidden = true;
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["Doc Number"].Hidden = true;
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["ActivityID"].Hidden = true;
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["Basic"].Hidden = true;
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["IsVoid"].Hidden = true;
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["IsApproved"].Hidden = true;
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["IsClosed"].Hidden = true;
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["PayAccountID"].Hidden = true;
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["TicketAmount"].Hidden = true;
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["PayItem"].Hidden = true;
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["JoiningDate"].Hidden = true;
			selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["PaidDays"].Hidden = true;
			if (!selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns.Contains("C"))
			{
				selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellActivation = Activation.AllowEdit;
				selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
				selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellClickAction = CellClickAction.Edit;
				selectDocumentDialog.dataGridItems.DisplayLayout.Bands[0].Columns["C"].Hidden = true;
			}
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK && IsNewRecord)
			{
				row = selectDocumentDialog.dataGridItems.ActiveRow;
				LoadEmployeeDetails(comboBoxEmployee.SelectedID);
				checkBoxonemonth.Checked = false;
				checkBoxsalary.Checked = false;
				LoadEmployeeDetails();
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeLeavePaymentListFormObj);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.LeaveSalaryPaymentForm));
			panelButtons = new System.Windows.Forms.Panel();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			buttonPayCash = new Micromind.UISupport.XPButton();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			groupBox1 = new System.Windows.Forms.GroupBox();
			textBoxFromDepartment = new Micromind.UISupport.MMTextBox();
			textBoxFromDivision = new Micromind.UISupport.MMTextBox();
			textBoxDesignation = new Micromind.UISupport.MMTextBox();
			textBoxFromLocation = new Micromind.UISupport.MMTextBox();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			formManager = new Micromind.DataControls.FormManager();
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			xpButtonSelect = new Micromind.UISupport.XPButton();
			checkBoxTotalTaken = new System.Windows.Forms.CheckBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			comboBoxPayrollItemDeduction = new Micromind.DataControls.PayrollItemComboBox();
			textBoxDeductions = new Micromind.UISupport.AmountTextBox();
			checkBoxonemonth = new System.Windows.Forms.CheckBox();
			checkBoxsalary = new System.Windows.Forms.CheckBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxEligibleDays = new Micromind.UISupport.NumberTextBox();
			textBoxDays = new Micromind.UISupport.NumberTextBox();
			textBoxSalary = new Micromind.UISupport.AmountTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxNetTotal = new Micromind.UISupport.AmountTextBox();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxTicketAmount = new Micromind.UISupport.AmountTextBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxLeaveAppNo = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			buttonPayCheque = new Micromind.UISupport.XPButton();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			textBoxPaymentNo = new Micromind.UISupport.MMTextBox();
			toolStrip2 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItemDeduction).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			toolStrip2.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 432);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(791, 40);
			panelButtons.TabIndex = 2;
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 2;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click_1);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(791, 1);
			linePanelDown.TabIndex = 0;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 3;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(681, 8);
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
			buttonSave.TabIndex = 1;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			buttonPayCash.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPayCash.BackColor = System.Drawing.Color.DarkGray;
			buttonPayCash.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPayCash.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPayCash.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonPayCash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPayCash.Location = new System.Drawing.Point(271, 371);
			buttonPayCash.Name = "buttonPayCash";
			buttonPayCash.Size = new System.Drawing.Size(96, 24);
			buttonPayCash.TabIndex = 16;
			buttonPayCash.Text = "Pay Cash";
			buttonPayCash.UseVisualStyleBackColor = false;
			buttonPayCash.Click += new System.EventHandler(buttonPayCash_Click);
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.IsModified = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(253, 42);
			textBoxEmployeeName.MaxLength = 15;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(222, 20);
			textBoxEmployeeName.TabIndex = 3;
			textBoxEmployeeName.TabStop = false;
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(12, 174);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(46, 13);
			mmLabel12.TabIndex = 20;
			mmLabel12.Text = "Amount:";
			groupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			groupBox1.Controls.Add(textBoxFromDepartment);
			groupBox1.Controls.Add(textBoxFromDivision);
			groupBox1.Controls.Add(textBoxDesignation);
			groupBox1.Controls.Add(textBoxFromLocation);
			groupBox1.Controls.Add(mmLabel11);
			groupBox1.Controls.Add(mmLabel15);
			groupBox1.Controls.Add(mmLabel16);
			groupBox1.Controls.Add(mmLabel17);
			groupBox1.Controls.Add(mmLabel2);
			groupBox1.Controls.Add(dateTimePickerDate);
			groupBox1.Controls.Add(comboBoxSysDoc);
			groupBox1.Controls.Add(textBoxVoucherNumber);
			groupBox1.Controls.Add(ultraFormattedLinkLabel5);
			groupBox1.Controls.Add(formManager);
			groupBox1.Controls.Add(linkLabelVoucherNumber);
			groupBox1.Controls.Add(xpButtonSelect);
			groupBox1.Controls.Add(checkBoxTotalTaken);
			groupBox1.Controls.Add(mmLabel13);
			groupBox1.Controls.Add(comboBoxPayrollItemDeduction);
			groupBox1.Controls.Add(textBoxDeductions);
			groupBox1.Controls.Add(checkBoxonemonth);
			groupBox1.Controls.Add(checkBoxsalary);
			groupBox1.Controls.Add(mmLabel10);
			groupBox1.Controls.Add(textBoxEligibleDays);
			groupBox1.Controls.Add(textBoxDays);
			groupBox1.Controls.Add(textBoxSalary);
			groupBox1.Controls.Add(mmLabel6);
			groupBox1.Controls.Add(textBoxNetTotal);
			groupBox1.Controls.Add(mmLabel14);
			groupBox1.Controls.Add(mmLabel3);
			groupBox1.Controls.Add(textBoxTicketAmount);
			groupBox1.Controls.Add(ultraFormattedLinkLabel6);
			groupBox1.Controls.Add(textBoxLeaveAppNo);
			groupBox1.Controls.Add(mmLabel9);
			groupBox1.Controls.Add(mmLabel8);
			groupBox1.Controls.Add(dateTimePickerTo);
			groupBox1.Controls.Add(textBoxCode);
			groupBox1.Controls.Add(dateTimePickerFrom);
			groupBox1.Controls.Add(textBoxNote);
			groupBox1.Controls.Add(mmLabel7);
			groupBox1.Controls.Add(buttonPayCheque);
			groupBox1.Controls.Add(textBoxAmount);
			groupBox1.Controls.Add(buttonPayCash);
			groupBox1.Controls.Add(mmLabel5);
			groupBox1.Controls.Add(mmLabel4);
			groupBox1.Controls.Add(comboBoxEmployee);
			groupBox1.Controls.Add(textBoxPaymentNo);
			groupBox1.Controls.Add(textBoxEmployeeName);
			groupBox1.Controls.Add(mmLabel12);
			groupBox1.Location = new System.Drawing.Point(0, 20);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(788, 414);
			groupBox1.TabIndex = 1;
			groupBox1.TabStop = false;
			textBoxFromDepartment.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDepartment.CustomReportFieldName = "";
			textBoxFromDepartment.CustomReportKey = "";
			textBoxFromDepartment.CustomReportValueType = 1;
			textBoxFromDepartment.IsComboTextBox = false;
			textBoxFromDepartment.IsModified = false;
			textBoxFromDepartment.Location = new System.Drawing.Point(329, 86);
			textBoxFromDepartment.MaxLength = 255;
			textBoxFromDepartment.Name = "textBoxFromDepartment";
			textBoxFromDepartment.ReadOnly = true;
			textBoxFromDepartment.Size = new System.Drawing.Size(146, 20);
			textBoxFromDepartment.TabIndex = 413;
			textBoxFromDepartment.TabStop = false;
			textBoxFromDivision.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDivision.CustomReportFieldName = "";
			textBoxFromDivision.CustomReportKey = "";
			textBoxFromDivision.CustomReportValueType = 1;
			textBoxFromDivision.IsComboTextBox = false;
			textBoxFromDivision.IsModified = false;
			textBoxFromDivision.Location = new System.Drawing.Point(112, 86);
			textBoxFromDivision.MaxLength = 64;
			textBoxFromDivision.Name = "textBoxFromDivision";
			textBoxFromDivision.ReadOnly = true;
			textBoxFromDivision.Size = new System.Drawing.Size(140, 20);
			textBoxFromDivision.TabIndex = 412;
			textBoxFromDivision.TabStop = false;
			textBoxDesignation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDesignation.CustomReportFieldName = "";
			textBoxDesignation.CustomReportKey = "";
			textBoxDesignation.CustomReportValueType = 1;
			textBoxDesignation.IsComboTextBox = false;
			textBoxDesignation.IsModified = false;
			textBoxDesignation.Location = new System.Drawing.Point(112, 64);
			textBoxDesignation.MaxLength = 15;
			textBoxDesignation.Name = "textBoxDesignation";
			textBoxDesignation.ReadOnly = true;
			textBoxDesignation.Size = new System.Drawing.Size(140, 20);
			textBoxDesignation.TabIndex = 410;
			textBoxDesignation.TabStop = false;
			textBoxFromLocation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFromLocation.CustomReportFieldName = "";
			textBoxFromLocation.CustomReportKey = "";
			textBoxFromLocation.CustomReportValueType = 1;
			textBoxFromLocation.IsComboTextBox = false;
			textBoxFromLocation.IsModified = false;
			textBoxFromLocation.Location = new System.Drawing.Point(329, 64);
			textBoxFromLocation.MaxLength = 15;
			textBoxFromLocation.Name = "textBoxFromLocation";
			textBoxFromLocation.ReadOnly = true;
			textBoxFromLocation.Size = new System.Drawing.Size(146, 20);
			textBoxFromLocation.TabIndex = 411;
			textBoxFromLocation.TabStop = false;
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(258, 89);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(65, 13);
			mmLabel11.TabIndex = 414;
			mmLabel11.Text = "Department:";
			mmLabel15.AutoSize = true;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(13, 90);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(47, 13);
			mmLabel15.TabIndex = 416;
			mmLabel15.Text = "Division:";
			mmLabel16.AutoSize = true;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(13, 67);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(66, 13);
			mmLabel16.TabIndex = 417;
			mmLabel16.Text = "Designation:";
			mmLabel17.AutoSize = true;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(258, 65);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(51, 13);
			mmLabel17.TabIndex = 415;
			mmLabel17.Text = "Location:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(520, 22);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(38, 13);
			mmLabel2.TabIndex = 408;
			mmLabel2.Text = "Date:";
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(564, 20);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDate.TabIndex = 409;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(112, 20);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(140, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxVoucherNumber.Location = new System.Drawing.Point(367, 20);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(140, 20);
			textBoxVoucherNumber.TabIndex = 1;
			appearance13.FontData.BoldAsString = "True";
			appearance13.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance13;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(15, 23);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 407;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(2, 10);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 3;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			appearance15.FontData.BoldAsString = "True";
			appearance15.FontData.Name = "Tahoma";
			linkLabelVoucherNumber.Appearance = appearance15;
			linkLabelVoucherNumber.AutoSize = true;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(259, 23);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(101, 15);
			linkLabelVoucherNumber.TabIndex = 406;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Voucher Number:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			linkLabelVoucherNumber.VisitedLinkAppearance = appearance16;
			xpButtonSelect.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButtonSelect.BackColor = System.Drawing.Color.DarkGray;
			xpButtonSelect.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButtonSelect.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButtonSelect.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButtonSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButtonSelect.Location = new System.Drawing.Point(475, 40);
			xpButtonSelect.Name = "xpButtonSelect";
			xpButtonSelect.Size = new System.Drawing.Size(32, 24);
			xpButtonSelect.TabIndex = 37;
			xpButtonSelect.Text = "...";
			xpButtonSelect.UseVisualStyleBackColor = false;
			xpButtonSelect.Click += new System.EventHandler(xpButton2_Click);
			checkBoxTotalTaken.AutoSize = true;
			checkBoxTotalTaken.Location = new System.Drawing.Point(346, 153);
			checkBoxTotalTaken.Name = "checkBoxTotalTaken";
			checkBoxTotalTaken.Size = new System.Drawing.Size(84, 17);
			checkBoxTotalTaken.TabIndex = 7;
			checkBoxTotalTaken.Text = "Total Taken";
			checkBoxTotalTaken.UseVisualStyleBackColor = true;
			checkBoxTotalTaken.CheckedChanged += new System.EventHandler(checkBoxTotalTaken_CheckedChanged);
			mmLabel13.AutoSize = true;
			mmLabel13.BackColor = System.Drawing.Color.Transparent;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(12, 240);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(67, 13);
			mmLabel13.TabIndex = 402;
			mmLabel13.Text = "Deductions :";
			comboBoxPayrollItemDeduction.Assigned = false;
			comboBoxPayrollItemDeduction.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayrollItemDeduction.CustomReportFieldName = "";
			comboBoxPayrollItemDeduction.CustomReportKey = "";
			comboBoxPayrollItemDeduction.CustomReportValueType = 1;
			comboBoxPayrollItemDeduction.DescriptionTextBox = null;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayrollItemDeduction.DisplayLayout.Appearance = appearance17;
			comboBoxPayrollItemDeduction.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayrollItemDeduction.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItemDeduction.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItemDeduction.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxPayrollItemDeduction.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItemDeduction.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxPayrollItemDeduction.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayrollItemDeduction.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxPayrollItemDeduction.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayrollItemDeduction.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxPayrollItemDeduction.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayrollItemDeduction.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayrollItemDeduction.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayrollItemDeduction.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayrollItemDeduction.Editable = true;
			comboBoxPayrollItemDeduction.FilterString = "";
			comboBoxPayrollItemDeduction.HasAllAccount = false;
			comboBoxPayrollItemDeduction.HasCustom = false;
			comboBoxPayrollItemDeduction.IsDataLoaded = false;
			comboBoxPayrollItemDeduction.IsDeduction = true;
			comboBoxPayrollItemDeduction.Location = new System.Drawing.Point(112, 240);
			comboBoxPayrollItemDeduction.MaxDropDownItems = 12;
			comboBoxPayrollItemDeduction.Name = "comboBoxPayrollItemDeduction";
			comboBoxPayrollItemDeduction.ShowInactiveItems = false;
			comboBoxPayrollItemDeduction.ShowQuickAdd = true;
			comboBoxPayrollItemDeduction.Size = new System.Drawing.Size(128, 20);
			comboBoxPayrollItemDeduction.TabIndex = 12;
			comboBoxPayrollItemDeduction.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxDeductions.AllowDecimal = true;
			textBoxDeductions.CustomReportFieldName = "";
			textBoxDeductions.CustomReportKey = "";
			textBoxDeductions.CustomReportValueType = 1;
			textBoxDeductions.IsComboTextBox = false;
			textBoxDeductions.IsModified = false;
			textBoxDeductions.Location = new System.Drawing.Point(260, 240);
			textBoxDeductions.MaxLength = 15;
			textBoxDeductions.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxDeductions.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxDeductions.Name = "textBoxDeductions";
			textBoxDeductions.NullText = "0";
			textBoxDeductions.Size = new System.Drawing.Size(100, 20);
			textBoxDeductions.TabIndex = 13;
			textBoxDeductions.Text = "0.00";
			textBoxDeductions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDeductions.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxDeductions.TextChanged += new System.EventHandler(textBoxDeductions_TextChanged);
			checkBoxonemonth.AutoSize = true;
			checkBoxonemonth.Location = new System.Drawing.Point(264, 153);
			checkBoxonemonth.Name = "checkBoxonemonth";
			checkBoxonemonth.Size = new System.Drawing.Size(76, 17);
			checkBoxonemonth.TabIndex = 6;
			checkBoxonemonth.Text = "one month";
			checkBoxonemonth.UseVisualStyleBackColor = true;
			checkBoxonemonth.CheckedChanged += new System.EventHandler(checkBoxonemonth_CheckedChanged);
			checkBoxsalary.AutoSize = true;
			checkBoxsalary.Location = new System.Drawing.Point(264, 219);
			checkBoxsalary.Name = "checkBoxsalary";
			checkBoxsalary.Size = new System.Drawing.Size(108, 17);
			checkBoxsalary.TabIndex = 11;
			checkBoxsalary.Text = "This month salary";
			checkBoxsalary.UseVisualStyleBackColor = true;
			checkBoxsalary.CheckedChanged += new System.EventHandler(checkBoxsalary_CheckedChanged);
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(12, 152);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(70, 13);
			mmLabel10.TabIndex = 399;
			mmLabel10.Text = "Eligible Days:";
			textBoxEligibleDays.AllowDecimal = false;
			textBoxEligibleDays.BackColor = System.Drawing.Color.White;
			textBoxEligibleDays.CustomReportFieldName = "";
			textBoxEligibleDays.CustomReportKey = "";
			textBoxEligibleDays.CustomReportValueType = 1;
			textBoxEligibleDays.ForeColor = System.Drawing.Color.Black;
			textBoxEligibleDays.IsComboTextBox = false;
			textBoxEligibleDays.IsModified = false;
			textBoxEligibleDays.Location = new System.Drawing.Point(112, 152);
			textBoxEligibleDays.MaxValue = new decimal(new int[4]
			{
				365,
				0,
				0,
				0
			});
			textBoxEligibleDays.MinValue = new decimal(new int[4]);
			textBoxEligibleDays.Name = "textBoxEligibleDays";
			textBoxEligibleDays.NullText = "0";
			textBoxEligibleDays.Size = new System.Drawing.Size(127, 20);
			textBoxEligibleDays.TabIndex = 5;
			textBoxEligibleDays.Text = "0";
			textBoxEligibleDays.TextChanged += new System.EventHandler(textBoxEligibleDays_TextChanged);
			textBoxDays.AllowDecimal = false;
			textBoxDays.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDays.CustomReportFieldName = "";
			textBoxDays.CustomReportKey = "";
			textBoxDays.CustomReportValueType = 1;
			textBoxDays.ForeColor = System.Drawing.Color.Black;
			textBoxDays.IsComboTextBox = false;
			textBoxDays.IsModified = false;
			textBoxDays.Location = new System.Drawing.Point(421, 131);
			textBoxDays.MaxValue = new decimal(new int[4]
			{
				365,
				0,
				0,
				0
			});
			textBoxDays.MinValue = new decimal(new int[4]);
			textBoxDays.Name = "textBoxDays";
			textBoxDays.NullText = "0";
			textBoxDays.ReadOnly = true;
			textBoxDays.Size = new System.Drawing.Size(54, 20);
			textBoxDays.TabIndex = 397;
			textBoxDays.TabStop = false;
			textBoxDays.Text = "0";
			textBoxSalary.AllowDecimal = true;
			textBoxSalary.CustomReportFieldName = "";
			textBoxSalary.CustomReportKey = "";
			textBoxSalary.CustomReportValueType = 1;
			textBoxSalary.IsComboTextBox = false;
			textBoxSalary.IsModified = false;
			textBoxSalary.Location = new System.Drawing.Point(112, 218);
			textBoxSalary.MaxLength = 15;
			textBoxSalary.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSalary.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSalary.Name = "textBoxSalary";
			textBoxSalary.NullText = "0";
			textBoxSalary.Size = new System.Drawing.Size(127, 20);
			textBoxSalary.TabIndex = 10;
			textBoxSalary.Text = "0.00";
			textBoxSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSalary.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxSalary.TextChanged += new System.EventHandler(textBoxSalary_TextChanged);
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(12, 218);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(39, 13);
			mmLabel6.TabIndex = 395;
			mmLabel6.Text = "Salary:";
			textBoxNetTotal.AllowDecimal = true;
			textBoxNetTotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxNetTotal.CustomReportFieldName = "";
			textBoxNetTotal.CustomReportKey = "";
			textBoxNetTotal.CustomReportValueType = 1;
			textBoxNetTotal.IsComboTextBox = false;
			textBoxNetTotal.IsModified = false;
			textBoxNetTotal.Location = new System.Drawing.Point(112, 360);
			textBoxNetTotal.MaxLength = 15;
			textBoxNetTotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxNetTotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxNetTotal.Name = "textBoxNetTotal";
			textBoxNetTotal.NullText = "0";
			textBoxNetTotal.ReadOnly = true;
			textBoxNetTotal.Size = new System.Drawing.Size(114, 20);
			textBoxNetTotal.TabIndex = 15;
			textBoxNetTotal.TabStop = false;
			textBoxNetTotal.Text = "0.00";
			textBoxNetTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxNetTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel14.AutoSize = true;
			mmLabel14.BackColor = System.Drawing.Color.Transparent;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(12, 360);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(58, 13);
			mmLabel14.TabIndex = 394;
			mmLabel14.Text = "Net Total :";
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(12, 196);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(82, 13);
			mmLabel3.TabIndex = 392;
			mmLabel3.Text = "Ticket Amount :";
			textBoxTicketAmount.AllowDecimal = true;
			textBoxTicketAmount.CustomReportFieldName = "";
			textBoxTicketAmount.CustomReportKey = "";
			textBoxTicketAmount.CustomReportValueType = 1;
			textBoxTicketAmount.IsComboTextBox = false;
			textBoxTicketAmount.IsModified = false;
			textBoxTicketAmount.Location = new System.Drawing.Point(112, 196);
			textBoxTicketAmount.MaxLength = 15;
			textBoxTicketAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTicketAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTicketAmount.Name = "textBoxTicketAmount";
			textBoxTicketAmount.NullText = "0";
			textBoxTicketAmount.Size = new System.Drawing.Size(127, 20);
			textBoxTicketAmount.TabIndex = 9;
			textBoxTicketAmount.Text = "0.00";
			textBoxTicketAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTicketAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxTicketAmount.TextChanged += new System.EventHandler(textBoxTicketAmount_TextChanged);
			appearance29.FontData.BoldAsString = "True";
			appearance29.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance29;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(15, 43);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(81, 15);
			ultraFormattedLinkLabel6.TabIndex = 131;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Employee No:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance30;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			textBoxLeaveAppNo.BackColor = System.Drawing.Color.White;
			textBoxLeaveAppNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLeaveAppNo.CustomReportFieldName = "";
			textBoxLeaveAppNo.CustomReportKey = "";
			textBoxLeaveAppNo.CustomReportValueType = 1;
			textBoxLeaveAppNo.IsComboTextBox = false;
			textBoxLeaveAppNo.IsModified = false;
			textBoxLeaveAppNo.Location = new System.Drawing.Point(112, 108);
			textBoxLeaveAppNo.MaxLength = 15;
			textBoxLeaveAppNo.Name = "textBoxLeaveAppNo";
			textBoxLeaveAppNo.Size = new System.Drawing.Size(140, 20);
			textBoxLeaveAppNo.TabIndex = 4;
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(13, 131);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(33, 13);
			mmLabel9.TabIndex = 36;
			mmLabel9.Text = "From:";
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(259, 134);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(23, 13);
			mmLabel8.TabIndex = 35;
			mmLabel8.Text = "To:";
			dateTimePickerTo.Enabled = false;
			dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerTo.Location = new System.Drawing.Point(288, 131);
			dateTimePickerTo.Name = "dateTimePickerTo";
			dateTimePickerTo.Size = new System.Drawing.Size(127, 20);
			dateTimePickerTo.TabIndex = 38;
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(606, 70);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(87, 20);
			textBoxCode.TabIndex = 33;
			textBoxCode.TabStop = false;
			textBoxCode.Visible = false;
			dateTimePickerFrom.Enabled = false;
			dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerFrom.Location = new System.Drawing.Point(112, 130);
			dateTimePickerFrom.Name = "dateTimePickerFrom";
			dateTimePickerFrom.Size = new System.Drawing.Size(127, 20);
			dateTimePickerFrom.TabIndex = 37;
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(112, 262);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(559, 96);
			textBoxNote.TabIndex = 13;
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(12, 262);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(33, 13);
			mmLabel7.TabIndex = 32;
			mmLabel7.Text = "Note:";
			buttonPayCheque.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPayCheque.BackColor = System.Drawing.Color.DarkGray;
			buttonPayCheque.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPayCheque.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPayCheque.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonPayCheque.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPayCheque.Location = new System.Drawing.Point(379, 371);
			buttonPayCheque.Name = "buttonPayCheque";
			buttonPayCheque.Size = new System.Drawing.Size(96, 24);
			buttonPayCheque.TabIndex = 17;
			buttonPayCheque.Text = "Pay Cheque";
			buttonPayCheque.UseVisualStyleBackColor = false;
			buttonPayCheque.Click += new System.EventHandler(buttonPayCheque_Click);
			textBoxAmount.AllowDecimal = true;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.IsModified = false;
			textBoxAmount.Location = new System.Drawing.Point(112, 174);
			textBoxAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAmount.Name = "textBoxAmount";
			textBoxAmount.NullText = "0";
			textBoxAmount.Size = new System.Drawing.Size(127, 20);
			textBoxAmount.TabIndex = 8;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxAmount.TextChanged += new System.EventHandler(textBoxAmount_TextChanged);
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(12, 110);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(99, 13);
			mmLabel5.TabIndex = 23;
			mmLabel5.Text = "Leave Appl. No:";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(12, 130);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(30, 13);
			mmLabel4.TabIndex = 21;
			mmLabel4.Text = "Date";
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = textBoxEmployeeName;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance31;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance38;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance40;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance41;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(112, 42);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(140, 20);
			comboBoxEmployee.TabIndex = 2;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxPaymentNo.BackColor = System.Drawing.Color.White;
			textBoxPaymentNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxPaymentNo.CustomReportFieldName = "";
			textBoxPaymentNo.CustomReportKey = "";
			textBoxPaymentNo.CustomReportValueType = 1;
			textBoxPaymentNo.IsComboTextBox = false;
			textBoxPaymentNo.IsModified = false;
			textBoxPaymentNo.Location = new System.Drawing.Point(606, 46);
			textBoxPaymentNo.MaxLength = 15;
			textBoxPaymentNo.Name = "textBoxPaymentNo";
			textBoxPaymentNo.Size = new System.Drawing.Size(87, 20);
			textBoxPaymentNo.TabIndex = 0;
			textBoxPaymentNo.TabStop = false;
			textBoxPaymentNo.Visible = false;
			toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator2,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripButtonAttach,
				toolStripButton5,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonDistribution,
				toolStripButtonInformation
			});
			toolStrip2.Location = new System.Drawing.Point(0, 0);
			toolStrip2.MinimumSize = new System.Drawing.Size(981, 31);
			toolStrip2.Name = "toolStrip2";
			toolStrip2.Size = new System.Drawing.Size(981, 31);
			toolStrip2.TabIndex = 4;
			toolStrip2.Text = "toolStrip2";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator3.Visible = false;
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton5.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton5.Name = "toolStripButton5";
			toolStripButton5.Size = new System.Drawing.Size(28, 28);
			toolStripButton5.Text = "&Print";
			toolStripButton5.ToolTipText = "Print (Ctrl+P)";
			toolStripButton5.Click += new System.EventHandler(toolStripButton5_Click);
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
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(791, 472);
			base.Controls.Add(toolStrip2);
			base.Controls.Add(groupBox1);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(500, 491);
			base.Name = "LeaveSalaryPaymentForm";
			Text = "Leave Salary Payment Entry";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItemDeduction).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			toolStrip2.ResumeLayout(false);
			toolStrip2.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
