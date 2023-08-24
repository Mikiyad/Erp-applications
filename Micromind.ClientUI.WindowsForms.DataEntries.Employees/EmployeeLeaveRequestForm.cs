using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
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
	public class EmployeeLeaveRequestForm : Form, IForm
	{
		public class Leavelist
		{
			private DateTime start;

			private DateTime end;

			public DateTime Start
			{
				get
				{
					return start;
				}
				set
				{
					start = value;
				}
			}

			public DateTime End
			{
				get
				{
					return end;
				}
				set
				{
					end = value;
				}
			}

			public Leavelist(DateTime Dtstart, DateTime Dtend)
			{
				start = Dtstart;
				end = Dtend;
			}
		}

		private EmployeeActivityData currentData;

		private const string TABLENAME_CONST = "Employee_Leave_Request";

		private const string IDFIELD_CONST = "ActivityID";

		private const string DOCIDFIELD_CONST = "DocNumber";

		private bool isNewRecord = true;

		private string EmployeeID = "";

		private int resumeActivityID;

		private double actualleavedays;

		private bool isVoid;

		private ScreenAccessRight screenRight;

		private decimal minusLeaves;

		private bool selfRequest;

		private bool allintermediatesub;

		private bool allsubordiantes;

		private string filteredReportToID = "asdr323f@$@";

		private IContainer components;

		private ToolStrip toolStrip1;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private MMTextBox textBoxFromLocation;

		private MMTextBox textBoxFromDivision;

		private MMLabel mmLabel4;

		private MMTextBox textBoxFromDepartment;

		private FormManager formManager;

		private MMTextBox textBoxEmployeeName;

		private MMLabel mmLabel3;

		private MMLabel mmLabel5;

		private MMTextBox textBoxReason;

		private MMLabel mmLabel1;

		private MMLabel mmLabel9;

		private MMTextBox textBoxReference;

		private MMLabel mmLabel10;

		private MMTextBox textBoxNote;

		private MMTextBox textBoxCode;

		private MMLabel label1TransactionID;

		private MMTextBox textBoxDesignation;

		private MMLabel mmLabel7;

		private MMLabel labelTerminated;

		private LeaveTypeComboBox comboBoxLeaveType;

		private MMLabel mmLabel12;

		private MMSDateTimePicker dateTimePickerStartDate;

		private MMLabel mmLabel11;

		private MMSDateTimePicker dateTimePickerEndDate;

		private NumberTextBox textBoxDays;

		private MMLabel mmLabel13;

		private CheckBox checkBoxApproved;

		private CheckBox checkBoxRejected;

		private MMLabel mmLabel8;

		private MMSDateTimePicker dateTimePickerTransactionDate;

		private UltraFormattedLinkLabel linkLabelVoucherNumber;

		private TextBox textBoxVoucherNumber;

		private MMTextBox textBoxResume;

		private UltraFormattedLinkLabel labelResume;

		private ToolStripButton toolStripButtonOpenList;

		private XPButton buttonVoid;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private Label labelLeaveAvaildays;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButton5;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private EmployeeFilterComboBox comboBoxEmployee;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMLabel mmLabel2;

		private MMSDateTimePicker dateTimePickerTravellingDate;

		private MMLabel labelDisplayAdays;

		private MMLabel labelADays;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5034;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private bool IsDirty => formManager.GetDirtyStatus();

		public int ResumeActivityID
		{
			get
			{
				return resumeActivityID;
			}
			set
			{
				resumeActivityID = value;
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
				bool visible;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					buttonDelete.Enabled = false;
					textBoxCode.Clear();
					UltraFormattedLinkLabel ultraFormattedLinkLabel = labelResume;
					visible = (textBoxResume.Visible = false);
					ultraFormattedLinkLabel.Visible = visible;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					labelTerminated.Visible = false;
				}
				textBoxVoucherNumber.Enabled = isNewRecord;
				comboBoxEmployee.Enabled = isNewRecord;
				MMLabel mMLabel = label1TransactionID;
				visible = (textBoxCode.Visible = !isNewRecord);
				mMLabel.Visible = visible;
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

		public double ActualLeaveDays
		{
			get
			{
				return actualleavedays;
			}
			set
			{
				actualleavedays = value;
				labelADays.Text = value.ToString();
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
				if (isVoid == value)
				{
					return;
				}
				isVoid = value;
				buttonSave.Enabled = !value;
				if (value)
				{
					buttonVoid.Enabled = false;
					return;
				}
				buttonVoid.Text = UIMessages.Void;
				if (!IsNewRecord)
				{
					buttonVoid.Enabled = true;
				}
				else
				{
					buttonVoid.Enabled = false;
				}
			}
		}

		public decimal MinusLeaves
		{
			get
			{
				return minusLeaves;
			}
			set
			{
				minusLeaves = value;
			}
		}

		public EmployeeLeaveRequestForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EmployeeLeaveRequestForm_Load;
			dateTimePickerStartDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dateTimePickerEndDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dateTimePickerTransactionDate.ValueChanged += dateTimePickerDate_ValueChanged;
			comboBoxLeaveType.SelectedIndexChanged += comboBoxLeaveType_SelectedIndexChanged;
			comboBoxLeaveType.SelectedIndexChanged += dateTimePickerDate_ValueChanged;
			comboBoxEmployee.SelectedIndexChanged += dateTimePickerDate_ValueChanged;
			EmployeeReportToID();
		}

		public void EmployeeReportToID()
		{
			comboBoxEmployee.LoadData();
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
			DateTime valueFrom = dateTimePickerStartDate.ValueFrom;
			TimeSpan timeSpan = dateTimePickerEndDate.ValueTo.Add(TimeSpan.FromDays(1.0)) - valueFrom;
			double num = 0.0;
			if (timeSpan.Days > 0)
			{
				textBoxDays.Text = timeSpan.Days.ToString();
			}
			else
			{
				textBoxDays.Text = 0.ToString();
			}
			num = GetDeductionProportion(comboBoxLeaveType.SelectedID);
			if (num == 0.5)
			{
				textBoxDays.Text = num.ToString();
			}
			if (isNewRecord)
			{
				CheckAvailableLeaves(comboBoxLeaveType.SelectedID);
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeActivityData(EmployeeActivityTypes.Leave);
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeActivityTable.Rows[0] : currentData.EmployeeActivityTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["ActivityType"] = (byte)7;
				dataRow["TransactionDate"] = dateTimePickerTransactionDate.Value;
				dataRow["Reason"] = textBoxReason.Text;
				dataRow["Reference"] = textBoxReference.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeActivityTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.EmployeeLeaveRequestTable.Rows[0] : currentData.EmployeeLeaveRequestTable.NewRow());
				dataRow["DocNumber"] = textBoxVoucherNumber.Text;
				dataRow["StartDate"] = dateTimePickerStartDate.Value;
				dataRow["EndDate"] = dateTimePickerEndDate.Value;
				if (checkBoxApproved.Checked)
				{
					dataRow["IsApproved"] = true;
				}
				else if (checkBoxRejected.Checked)
				{
					dataRow["IsApproved"] = false;
				}
				else
				{
					dataRow["IsApproved"] = DBNull.Value;
				}
				dataRow["ActualLeaveDays"] = ActualLeaveDays;
				if (!(comboBoxLeaveType.SelectedID != ""))
				{
					ErrorHelper.ErrorMessage("Please select the leave type.");
					return false;
				}
				dataRow["LeaveTypeID"] = comboBoxLeaveType.SelectedID;
				if (dateTimePickerTravellingDate.Checked)
				{
					dataRow["TravellingDate"] = dateTimePickerTravellingDate.Value;
				}
				else
				{
					dataRow["TravellingDate"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeLeaveRequestTable.Rows.Add(dataRow);
				}
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
			textBoxCode.Focus();
		}

		public void LoadData(int id)
		{
			try
			{
				if (CanClose())
				{
					currentData = Factory.EmployeeActivitySystem.GetEmployeeActivityByID(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id.ToString();
						textBoxCode.Focus();
					}
					else
					{
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
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.Tables["Employee_Activity"].Rows[0];
			DataSet employeeFilterComboList = Factory.EmployeeSystem.GetEmployeeFilterComboList();
			employeeFilterComboList = EmpComboFillData(employeeFilterComboList);
			DataRow[] array = employeeFilterComboList.Tables[0].Select("Code = '" + dataRow["EmployeeID"].ToString() + "'");
			if (array.Length == 0)
			{
				ErrorHelper.InformationMessage("User is not authorised to view this record!");
			}
			else
			{
				textBoxCode.Text = dataRow["ActivityID"].ToString();
				comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
				textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
				textBoxNote.Text = dataRow["Note"].ToString();
				textBoxReason.Text = dataRow["Reason"].ToString();
				textBoxReference.Text = dataRow["Reference"].ToString();
				dateTimePickerTransactionDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
				dataRow = currentData.Tables["Employee_Leave_Request"].Rows[0];
				textBoxVoucherNumber.Text = dataRow["DocNumber"].ToString();
				comboBoxLeaveType.SelectedID = dataRow["LeaveTypeID"].ToString();
				dateTimePickerStartDate.Value = DateTime.Parse(dataRow["StartDate"].ToString());
				dateTimePickerEndDate.Value = DateTime.Parse(dataRow["EndDate"].ToString());
				if (dataRow["TravellingDate"] != DBNull.Value)
				{
					dateTimePickerTravellingDate.Value = DateTime.Parse(dataRow["TravellingDate"].ToString());
					dateTimePickerTravellingDate.Checked = true;
				}
				else
				{
					dateTimePickerTravellingDate.Clear();
				}
				if (!dataRow["ResumeDate"].IsDBNullOrEmpty())
				{
					textBoxResume.Text = DateTime.Parse(dataRow["ResumeDate"].ToString()).ToShortDateString();
				}
				bool flag = bool.Parse(dataRow["IsResumed"].ToString());
				ActualLeaveDays = double.Parse(dataRow["LeaveDays"].ToString());
				labelADays.Text = int.Parse(dataRow["ActualLeaveDays"].ToString()).ToString();
				int result = 0;
				int.TryParse(dataRow["ResumeActivityID"].ToString(), out result);
				ResumeActivityID = result;
				UltraFormattedLinkLabel ultraFormattedLinkLabel = labelResume;
				bool visible = textBoxResume.Visible = flag;
				ultraFormattedLinkLabel.Visible = visible;
				checkBoxApproved.Checked = false;
				checkBoxRejected.Checked = false;
				if (dataRow["IsApproved"] != DBNull.Value)
				{
					if (bool.Parse(dataRow["IsApproved"].ToString()))
					{
						checkBoxApproved.Checked = true;
						checkBoxRejected.Checked = false;
					}
					else
					{
						checkBoxRejected.Checked = true;
						checkBoxApproved.Checked = false;
					}
					if (dataRow["ApprovedBy"].ToString() == Global.CurrentUser.ToString())
					{
						checkBoxApproved.Enabled = true;
						checkBoxRejected.Enabled = true;
					}
				}
				if (array.Length != 0)
				{
					goto IL_03e1;
				}
			}
			ClearForm();
			goto IL_03e1;
			IL_03e1:
			Console.WriteLine("");
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
				bool flag = (!isNewRecord) ? Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Leave, isUpdate: true) : Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Leave, isUpdate: false);
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
			catch (CompanyException ex)
			{
				ErrorHelper.ErrorMessage(ex.Message);
				return false;
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
			bool flag = true;
			flag = Factory.EmployeeLeaveDetailSystem.IsLeaveExist(comboBoxEmployee.SelectedID, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value);
			if (IsNewRecord && !flag)
			{
				ErrorHelper.InformationMessage("Employee has already requested leaves between this period!");
				comboBoxEmployee.Focus();
				return false;
			}
			if (dateTimePickerStartDate.ValueFrom > dateTimePickerEndDate.ValueFrom)
			{
				ErrorHelper.InformationMessage("Start date cannot be greater than end date.");
				dateTimePickerEndDate.Focus();
				return false;
			}
			if (comboBoxEmployee.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (labelTerminated.Visible)
			{
				ErrorHelper.InformationMessage("This employee is already terminated.");
				return false;
			}
			if (!isNewRecord && textBoxResume.Visible && ResumeActivityID > 0)
			{
				ErrorHelper.WarningMessage("This leave is already resumed and cannot be modifed.");
				return false;
			}
			if (!LeaveAvailability_New(comboBoxLeaveType.SelectedID))
			{
				return false;
			}
			return true;
		}

		private bool LeaveAvailability(string LeaveType)
		{
			DataSet dataSet = new DataSet();
			List<Leavelist> list = new List<Leavelist>();
			dataSet = Factory.EmployeeLeaveDetailSystem.GetEmployeeLeaveAvailability(comboBoxEmployee.SelectedID, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value, LeaveType);
			if (dataSet.Tables.Count == 0)
			{
				labelLeaveAvaildays.Text = "Not Activated";
				return true;
			}
			if (dataSet.Tables[0].Rows.Count == 0)
			{
				labelLeaveAvaildays.Text = "Not Activated";
				return true;
			}
			checked
			{
				if (dataSet.Tables.Count > 0)
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
					decimal num = default(decimal);
					int num2 = 0;
					int result10 = 0;
					DateTime result11 = new DateTime(2014, 1, 1);
					DateTime result12 = new DateTime(2014, 1, 1);
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
							decimal num3 = default(decimal);
							decimal num4 = default(decimal);
							num3 = result + result2 + result3 + (decimal)result6;
							dataSet.Tables[0].Rows[i]["TotalLeaves"] = num3;
							num4 = result + result2 + result3 - (decimal)result4 + (decimal)result6 - (decimal)result5;
							dataSet.Tables[0].Rows[i]["LeavesRemaining"] = num4;
						}
						dataSet.Tables[0].Columns.Remove("1SET");
						dataSet.Tables[0].Columns.Remove("2SET");
						dataSet.Tables[0].Columns.Remove("3SET");
					}
					else if (dataSet.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "Calendar Year")
					{
						List<string> list2 = new List<string>();
						for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
						{
							int.TryParse(dataSet.Tables[0].Rows[j]["openingLeavesTaken"].ToString(), out result4);
							int.TryParse(dataSet.Tables[0].Rows[j]["TotalTaken"].ToString(), out result10);
							int.TryParse(dataSet.Tables[0].Rows[j]["LeaveDayswithType"].ToString(), out result6);
							DateTime.TryParse(dataSet.Tables[0].Rows[j]["FromDate"].ToString(), out result11);
							DateTime.TryParse(dataSet.Tables[0].Rows[j]["ToDate"].ToString(), out result12);
							string text = dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString();
							result9 = bool.Parse(dataSet.Tables[0].Rows[j]["IsAnnual"].ToString());
							DataView defaultView = dataSet.Tables[1].DefaultView;
							if (result9 && result11 != d && result12 != d)
							{
								defaultView.RowFilter = "FromDate='" + result11.ToString() + "' AND ToDate='" + result12.ToString() + "' AND LeaveTypeID='" + LeaveType.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
							}
							else if (result9 && result11 == d && result12 == d)
							{
								defaultView.RowFilter = "LeaveTypeID='" + LeaveType.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
							}
							else if (!result9)
							{
								defaultView.RowFilter = "LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
							}
							DataSet dataSet2 = new DataSet();
							DataTable dataTable = defaultView.ToTable();
							dataSet2.Tables.Add(dataTable);
							bool flag = false;
							for (int k = 0; k < list.Count; k++)
							{
								DateTime start = list[k].Start;
								DateTime end = list[k].End;
								if (result11 >= start && result12 <= end)
								{
									flag = true;
								}
							}
							object obj;
							object obj2;
							if (result11 != d && result12 != d && !flag)
							{
								DataTable dataTable2 = new DataTable();
								dataTable2 = dataTable.DefaultView.ToTable(true, "DaysTaken");
								new DataTable();
								DataTable dataTable3 = dataTable.DefaultView.ToTable(true, "ToLessTaken");
								obj = dataTable2.Compute("Sum(DaysTaken)", "DaysTaken <> 0");
								obj2 = dataTable3.Compute("Sum(ToLessTaken)", "ToLessTaken <> 0");
								list.Add(new Leavelist(result11, result12));
							}
							else
							{
								obj = DBNull.Value;
								obj2 = DBNull.Value;
							}
							if (obj.ToString() != "" && obj2.ToString() != "")
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = int.Parse(obj.ToString()) - int.Parse(obj2.ToString());
							}
							else if (obj != DBNull.Value)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = obj;
							}
							else if (obj == DBNull.Value && result9)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = 0;
							}
							int.TryParse(dataSet.Tables[0].Rows[j]["TotalTaken"].ToString(), out result10);
							int.TryParse(dataSet.Tables[0].Rows[j]["AnnualAllowedDays"].ToString(), out result7);
							if (result9)
							{
								result6 = 0;
							}
							dataSet.Tables[0].Rows[j]["TotalLeaves"] = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7;
							if (result9 && num2 != result10 && !flag)
							{
								num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result10 + MinusLeaves;
								num2 = result10;
							}
							else if (!result9)
							{
								num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result5;
							}
							else if ((result9 && num2 == result10) & flag)
							{
								num = (decimal)result7 + MinusLeaves;
								dataSet.Tables[0].Rows[j]["TotalTaken"] = DBNull.Value;
							}
							else if (result9 && num2 == result10 && !flag)
							{
								num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result10 + MinusLeaves;
							}
							if (num >= 0m && result9 && MinusLeaves != 0m)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = -1m * MinusLeaves;
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = num;
								MinusLeaves = 0m;
							}
							else if (num >= 0m && result9 && MinusLeaves == 0m)
							{
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = num;
							}
							else if (num < 0m && result9)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = result7;
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = 0;
								MinusLeaves = num;
							}
							else if (!result9)
							{
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = num;
							}
							if (result11.Date < dateTimePickerStartDate.ValueFrom && result12.Date < dateTimePickerStartDate.ValueFrom && result7 != 0)
							{
								dataSet.Tables[0].Rows[j].Delete();
								continue;
							}
							if (result7 == 0 && !list2.Contains(dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString()))
							{
								list2.Add(dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString());
							}
							else if (result7 == 0)
							{
								dataSet.Tables[0].Rows[j].Delete();
								continue;
							}
							int result13 = 0;
							int.TryParse(dataSet.Tables[0].Rows[j]["LeavesRemaining"].ToString(), out result13);
						}
						dataSet.Tables[0].Columns.Remove("LeaveDayswithType");
						dataSet.AcceptChanges();
					}
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						string text2 = row["LeaveTypeID"].ToString();
						double result14 = 0.0;
						double result15 = 0.0;
						bool result16 = false;
						if (dataSet.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "On Account" && text2 == comboBoxLeaveType.SelectedID)
						{
							double.TryParse(row["LeavesRemaining"].ToString(), out result14);
							double.TryParse(textBoxDays.Text, out result15);
							if (result14 < result15)
							{
								ErrorHelper.WarningMessage("User doesn't have enough Leaves");
								return false;
							}
						}
						if (dataSet.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "Calendar Year")
						{
							double result17 = 0.0;
							double.TryParse(row["LeavesRemaining"].ToString(), out result17);
							bool.TryParse(row["IsAnnual"].ToString(), out result16);
							double.TryParse(textBoxDays.Text, out result15);
							if (dataSet.Tables[0].Select("LeaveTypeID ='" + comboBoxLeaveType.SelectedID + "'").Length == 0)
							{
								ErrorHelper.WarningMessage("User doesn't have Leave");
								return false;
							}
							if (text2 == comboBoxLeaveType.SelectedID && result17 < result15 && !result16)
							{
								ErrorHelper.WarningMessage("User doesn't have enough Leaves");
								return false;
							}
							double.TryParse(dataSet.Tables[0].Compute("Sum(LeavesRemaining)", "LeaveTypeID ='" + text2 + "'").ToString(), out result14);
							if ((text2 == comboBoxLeaveType.SelectedID && result14 < result15) & result16)
							{
								ErrorHelper.WarningMessage("User doesn't have enough Leaves");
								return false;
							}
							if ((result14 >= result15 && text2 == comboBoxLeaveType.SelectedID) & result16)
							{
								DateTime result18 = new DateTime(2014, 1, 1);
								DateTime result19 = new DateTime(2014, 1, 1);
								DateTime.TryParse(row["FromDate"].ToString(), out result18);
								DateTime.TryParse(row["ToDate"].ToString(), out result19);
							}
						}
					}
				}
				return true;
			}
		}

		private bool LeaveAvailability_New(string TypeID)
		{
			DataSet dataSet = null;
			List<Leavelist> list = new List<Leavelist>();
			if (TypeID != "" && TypeID != string.Empty && comboBoxEmployee.SelectedID != "")
			{
				dataSet = Factory.EmployeeLeaveDetailSystem.GetEmployeeLeaveAvailability(comboBoxEmployee.SelectedID, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value, TypeID);
			}
			if (comboBoxEmployee.SelectedID == "" || TypeID == "" || TypeID == string.Empty)
			{
				labelLeaveAvaildays.Text = "";
				return false;
			}
			if ((dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0) && comboBoxEmployee.SelectedID != "")
			{
				ErrorHelper.WarningMessage("Leave Not Activated");
				labelLeaveAvaildays.Text = "Not Activated";
				return false;
			}
			checked
			{
				if (dataSet.Tables.Count > 0)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					int result4 = 0;
					int result5 = 0;
					int result6 = 0;
					int result7 = 0;
					bool flag = false;
					bool result8 = false;
					decimal num = default(decimal);
					int num2 = 0;
					int result9 = 0;
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
							bool.TryParse(dataSet.Tables[0].Rows[i]["IsAnnual"].ToString(), out result8);
							if (result != 0m || result2 != 0m || result3 != 0m || (!flag && result8))
							{
								result6 = 0;
							}
							decimal num3 = default(decimal);
							decimal num4 = default(decimal);
							num3 = result + result2 + result3 + (decimal)result6;
							num4 = result + result2 + result3 - (decimal)result4 + (decimal)result6 - (decimal)result5;
							dataSet.Tables[0].Rows[i]["TotalLeaves"] = num3;
							dataSet.Tables[0].Rows[i]["LeavesRemaining"] = num4;
						}
						dataSet.Tables[0].Columns.Remove("1SET");
						dataSet.Tables[0].Columns.Remove("2SET");
						dataSet.Tables[0].Columns.Remove("3SET");
					}
					else if (dataSet.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "Calendar Year")
					{
						List<string> list2 = new List<string>();
						for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
						{
							int.TryParse(dataSet.Tables[0].Rows[j]["openingLeavesTaken"].ToString(), out result4);
							int.TryParse(dataSet.Tables[0].Rows[j]["TotalTaken"].ToString(), out result5);
							int.TryParse(dataSet.Tables[0].Rows[j]["LeaveDayswithType"].ToString(), out result6);
							DateTime.TryParse(dataSet.Tables[0].Rows[j]["FromDate"].ToString(), out result10);
							DateTime.TryParse(dataSet.Tables[0].Rows[j]["ToDate"].ToString(), out result11);
							string text = dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString();
							result8 = bool.Parse(dataSet.Tables[0].Rows[j]["IsAnnual"].ToString());
							DataView defaultView = dataSet.Tables[1].DefaultView;
							if (dataSet.Tables[2].Rows.Count > 0)
							{
								ActualLeaveDays = double.Parse(dataSet.Tables[2].Rows[0]["ActualLeaveDays"].ToString());
							}
							else
							{
								DateTime valueFrom = dateTimePickerStartDate.ValueFrom;
								ActualLeaveDays = (dateTimePickerEndDate.ValueTo.Add(TimeSpan.FromSeconds(1.0)) - valueFrom).TotalDays;
							}
							if (result8 && result10 != d && result11 != d)
							{
								defaultView.RowFilter = "FromDate='" + result10.ToString() + "' AND ToDate='" + result11.ToString() + "' AND LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result8.ToString() + "'";
							}
							else if (result8 && result10 == d && result11 == d)
							{
								defaultView.RowFilter = "LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result8.ToString() + "'";
							}
							else if (!result8)
							{
								defaultView.RowFilter = "LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result8.ToString() + "'";
							}
							DataSet dataSet2 = new DataSet();
							DataTable dataTable = defaultView.ToTable();
							dataSet2.Tables.Add(dataTable);
							bool flag2 = false;
							for (int k = 0; k < list.Count; k++)
							{
								DateTime start = list[k].Start;
								DateTime end = list[k].End;
								if (result10 >= start && result11 <= end)
								{
									flag2 = true;
								}
							}
							object obj;
							object obj2;
							if (result10 != d && result11 != d && !flag2)
							{
								DataTable dataTable2 = new DataTable();
								dataTable2 = dataTable.DefaultView.ToTable(true, "DaysTaken");
								new DataTable();
								DataTable dataTable3 = dataTable.DefaultView.ToTable(true, "ToLessTaken");
								obj = dataTable2.Compute("Sum(DaysTaken)", "DaysTaken <> 0");
								obj2 = dataTable3.Compute("Sum(ToLessTaken)", "ToLessTaken <> 0");
								list.Add(new Leavelist(result10, result11));
							}
							else
							{
								obj = DBNull.Value;
								obj2 = DBNull.Value;
							}
							if (obj != DBNull.Value && obj2 != DBNull.Value)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = int.Parse(obj.ToString()) - int.Parse(obj2.ToString());
							}
							else if (obj != DBNull.Value)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = obj;
							}
							else if (obj == DBNull.Value && result8)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = 0;
							}
							int.TryParse(dataSet.Tables[0].Rows[j]["TotalTaken"].ToString(), out result9);
							int.TryParse(dataSet.Tables[0].Rows[j]["AnnualAllowedDays"].ToString(), out result7);
							if (result8)
							{
								result6 = 0;
							}
							dataSet.Tables[0].Rows[j]["TotalLeaves"] = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7;
							if (result8 && num2 != result9 && !flag2)
							{
								num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result9 + MinusLeaves;
								num2 = result9;
							}
							else if (!result8)
							{
								num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result5;
							}
							else if ((result8 && num2 != result9) & flag2)
							{
								num = (decimal)result7 + MinusLeaves;
								dataSet.Tables[0].Rows[j]["TotalTaken"] = DBNull.Value;
							}
							else if (result8 && num2 == result9 && !flag2)
							{
								num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result9 + MinusLeaves;
							}
							else if (result8 && num2 == 0 && result9 == 0)
							{
								num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result9 + MinusLeaves;
							}
							if (num >= 0m && result8 && MinusLeaves != 0m)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = -1m * MinusLeaves;
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = num;
								MinusLeaves = 0m;
							}
							else if (num >= 0m && result8 && MinusLeaves == 0m)
							{
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = num;
							}
							else if (num < 0m && result8)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = result7;
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = 0;
								MinusLeaves = num;
							}
							else if (!result8)
							{
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = num;
							}
							if (result10.Date < dateTimePickerStartDate.ValueFrom && result11.Date < dateTimePickerStartDate.ValueFrom && result7 != 0)
							{
								dataSet.Tables[0].Rows[j].Delete();
								continue;
							}
							if (result7 == 0 && !list2.Contains(dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString()))
							{
								list2.Add(dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString());
							}
							else if (result7 == 0)
							{
								dataSet.Tables[0].Rows[j].Delete();
								continue;
							}
							int result12 = 0;
							int.TryParse(dataSet.Tables[0].Rows[j]["LeavesRemaining"].ToString(), out result12);
						}
						dataSet.Tables[0].Columns.Remove("LeaveDayswithType");
						dataSet.Tables[0].Columns.Remove("ToLessTaken");
					}
					dataSet.AcceptChanges();
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						string text2 = row["LeaveTypeID"].ToString();
						double result13 = 0.0;
						double result14 = 0.0;
						bool result15 = false;
						if (dataSet.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "On Account" && text2 == comboBoxLeaveType.SelectedID)
						{
							double.TryParse(row["LeavesRemaining"].ToString(), out result13);
							double.TryParse(textBoxDays.Text, out result14);
							if (result13 < result14)
							{
								ErrorHelper.WarningMessage("User doesn't have enough Leaves");
								return false;
							}
						}
						if (dataSet.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "Calendar Year")
						{
							double result16 = 0.0;
							double.TryParse(row["LeavesRemaining"].ToString(), out result16);
							bool.TryParse(row["IsAnnual"].ToString(), out result15);
							double.TryParse(textBoxDays.Text, out result14);
							if (dataSet.Tables[0].Select("LeaveTypeID ='" + comboBoxLeaveType.SelectedID + "'").Length == 0)
							{
								ErrorHelper.WarningMessage("User doesn't have Leave");
								return false;
							}
							if (text2 == comboBoxLeaveType.SelectedID && result16 < result14 && !result15)
							{
								ErrorHelper.WarningMessage("User doesn't have enough Leaves");
								return false;
							}
							double.TryParse(dataSet.Tables[0].Compute("Sum(LeavesRemaining)", "LeaveTypeID ='" + text2 + "'").ToString(), out result13);
							if (ActualLeaveDays != 0.0)
							{
								result14 = ActualLeaveDays;
							}
							if ((text2 == comboBoxLeaveType.SelectedID && result13 < result14) & result15)
							{
								ErrorHelper.WarningMessage("User doesn't have enough Leaves");
								return false;
							}
							if ((result13 >= result14 && text2 == comboBoxLeaveType.SelectedID) & result15)
							{
								DateTime result17 = new DateTime(2014, 1, 1);
								DateTime result18 = new DateTime(2014, 1, 1);
								DateTime.TryParse(row["FromDate"].ToString(), out result17);
								DateTime.TryParse(row["ToDate"].ToString(), out result18);
							}
						}
					}
				}
				return true;
			}
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
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
			textBoxCode.Clear();
			comboBoxEmployee.Clear();
			textBoxEmployeeName.Clear();
			textBoxFromDepartment.Clear();
			textBoxFromDivision.Clear();
			textBoxFromLocation.Clear();
			textBoxDesignation.Clear();
			textBoxNote.Clear();
			textBoxReason.Clear();
			dateTimePickerTransactionDate.Value = DateTime.Today;
			textBoxReference.Clear();
			textBoxResume.Clear();
			labelLeaveAvaildays.Text = "";
			UltraFormattedLinkLabel ultraFormattedLinkLabel = labelResume;
			bool visible = textBoxResume.Visible = false;
			ultraFormattedLinkLabel.Visible = visible;
			comboBoxLeaveType.Clear();
			MMSDateTimePicker mMSDateTimePicker = dateTimePickerStartDate;
			MMSDateTimePicker mMSDateTimePicker2 = dateTimePickerEndDate;
			DateTime dateTime = dateTimePickerTransactionDate.Value = DateTime.Now;
			DateTime dateTime4 = mMSDateTimePicker.Value = (mMSDateTimePicker2.Value = dateTime);
			dateTimePickerTravellingDate.Checked = false;
			dateTimePickerTravellingDate.Value = DateTime.Now;
			textBoxDays.Clear();
			labelADays.Text = "";
			checkBoxApproved.Checked = false;
			checkBoxRejected.Checked = false;
			checkBoxApproved.Enabled = false;
			checkBoxRejected.Enabled = false;
			formManager.ResetDirty();
			comboBoxEmployee.Focus();
		}

		private void SponsorGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void SponsorGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) != DialogResult.No)
			{
				if (!IsNewRecord && checkBoxApproved.Checked)
				{
					ErrorHelper.ErrorMessage("Leave has already approved. You are not able to modify.");
				}
				else if (Delete())
				{
					ClearForm();
					IsNewRecord = true;
				}
				else
				{
					ErrorHelper.ErrorMessage("Unable to delete the transaction.");
				}
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetNextID("Employee_Leave_Request", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetPreviousID("Employee_Leave_Request", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetLastID("Employee_Leave_Request", "ActivityID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetFirstID("Employee_Leave_Request", "ActivityID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
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

		private void EmployeeLeaveRequestForm_Load(object sender, EventArgs e)
		{
			try
			{
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
		}

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				labelLeaveAvaildays.Text = "";
				textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
				if (comboBoxEmployee.SelectedID == "")
				{
					textBoxFromLocation.Text = "";
					textBoxFromDivision.Text = "";
					textBoxFromDepartment.Text = "";
					textBoxDesignation.Text = "";
					labelTerminated.Visible = false;
				}
				else
				{
					DataSet employeeBriefInfo = Factory.EmployeeSystem.GetEmployeeBriefInfo(comboBoxEmployee.SelectedID);
					if (employeeBriefInfo != null && employeeBriefInfo.Tables.Count > 0 && employeeBriefInfo.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = employeeBriefInfo.Tables[0].Rows[0];
						textBoxFromLocation.Text = dataRow["WorkLocationName"].ToString();
						textBoxFromDivision.Text = dataRow["DivisionName"].ToString();
						textBoxFromDepartment.Text = dataRow["DepartmentName"].ToString();
						textBoxDesignation.Text = dataRow["PositionName"].ToString();
						bool result = false;
						bool.TryParse(dataRow["IsTerminated"].ToString(), out result);
						if (result && IsNewRecord)
						{
							labelTerminated.Visible = true;
						}
						else
						{
							labelTerminated.Visible = false;
						}
					}
					comboBoxLeaveType.Clear();
					DataSet employeeLeaveTypeComboList = Factory.LeaveTypeSystem.GetEmployeeLeaveTypeComboList(comboBoxEmployee.SelectedID);
					if (employeeLeaveTypeComboList.Tables[0].Rows.Count > 0)
					{
						comboBoxLeaveType.LoadData(isReferesh: false);
						comboBoxLeaveType.DataSource = employeeLeaveTypeComboList;
						comboBoxLeaveType.DataBind();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void linkLabelVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextDocumentNumber("Employee_Leave_Request", "DocNumber");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void comboBoxLeaveType_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				labelLeaveAvaildays.Text = "";
				if (comboBoxLeaveType.SelectedID != "" && comboBoxEmployee.SelectedID != "")
				{
					textBoxDays.Text = GetDeductionProportion(comboBoxLeaveType.SelectedID).ToString("0.0");
					if (textBoxDays.Text == "0.0")
					{
						textBoxDays.Clear();
					}
					if (isNewRecord)
					{
						CheckAvailableLeaves(comboBoxLeaveType.SelectedID);
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void CheckAvailableLeaves(string TypeID)
		{
			DataSet dataSet = null;
			List<Leavelist> list = new List<Leavelist>();
			if (TypeID != "" && TypeID != string.Empty && comboBoxEmployee.SelectedID != "")
			{
				dataSet = Factory.EmployeeLeaveDetailSystem.GetEmployeeLeaveAvailability(comboBoxEmployee.SelectedID, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value, TypeID);
			}
			checked
			{
				if (comboBoxEmployee.SelectedID == "" || TypeID == "" || TypeID == string.Empty)
				{
					labelLeaveAvaildays.Text = "";
				}
				else if ((dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0) && comboBoxEmployee.SelectedID != "")
				{
					labelLeaveAvaildays.Text = "Not Activated";
				}
				else
				{
					if (dataSet.Tables.Count <= 0)
					{
						return;
					}
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					int result4 = 0;
					int result5 = 0;
					int result6 = 0;
					int result7 = 0;
					bool flag = false;
					bool result8 = false;
					decimal num = default(decimal);
					int num2 = 0;
					int result9 = 0;
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
							bool.TryParse(dataSet.Tables[0].Rows[i]["IsAnnual"].ToString(), out result8);
							if (result != 0m || result2 != 0m || result3 != 0m || (!flag && result8))
							{
								result6 = 0;
							}
							decimal num3 = default(decimal);
							decimal num4 = default(decimal);
							num3 = result + result2 + result3 + (decimal)result6;
							num4 = result + result2 + result3 - (decimal)result4 + (decimal)result6 - (decimal)result5;
							dataSet.Tables[0].Rows[i]["TotalLeaves"] = num3;
							dataSet.Tables[0].Rows[i]["LeavesRemaining"] = num4;
						}
						dataSet.Tables[0].Columns.Remove("1SET");
						dataSet.Tables[0].Columns.Remove("2SET");
						dataSet.Tables[0].Columns.Remove("3SET");
						DateTime valueFrom = dateTimePickerStartDate.ValueFrom;
						ActualLeaveDays = (dateTimePickerEndDate.ValueTo.Add(TimeSpan.FromSeconds(1.0)) - valueFrom).TotalDays;
					}
					else if (dataSet.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "Calendar Year")
					{
						List<string> list2 = new List<string>();
						for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
						{
							int.TryParse(dataSet.Tables[0].Rows[j]["openingLeavesTaken"].ToString(), out result4);
							int.TryParse(dataSet.Tables[0].Rows[j]["TotalTaken"].ToString(), out result5);
							int.TryParse(dataSet.Tables[0].Rows[j]["LeaveDayswithType"].ToString(), out result6);
							DateTime.TryParse(dataSet.Tables[0].Rows[j]["FromDate"].ToString(), out result10);
							DateTime.TryParse(dataSet.Tables[0].Rows[j]["ToDate"].ToString(), out result11);
							string text = dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString();
							result8 = bool.Parse(dataSet.Tables[0].Rows[j]["IsAnnual"].ToString());
							DataView defaultView = dataSet.Tables[1].DefaultView;
							if (text != comboBoxLeaveType.SelectedID)
							{
								continue;
							}
							bool result12 = false;
							bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Leave_Type", "ActivateHC", "LeaveTypeID", text).ToString(), out result12);
							ActualLeaveDays = 0.0;
							if (dataSet.Tables[2].Rows.Count > 0 && result12)
							{
								ActualLeaveDays = double.Parse(dataSet.Tables[2].Rows[0]["ActualLeaveDays"].ToString());
							}
							else
							{
								DateTime valueFrom2 = dateTimePickerStartDate.ValueFrom;
								ActualLeaveDays = (dateTimePickerEndDate.ValueTo.Add(TimeSpan.FromSeconds(1.0)) - valueFrom2).TotalDays;
							}
							if (result8 && result10 != d && result11 != d)
							{
								defaultView.RowFilter = "FromDate='" + result10.ToString() + "' AND ToDate='" + result11.ToString() + "' AND LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result8.ToString() + "'";
							}
							else if (result8 && result10 == d && result11 == d)
							{
								defaultView.RowFilter = "LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result8.ToString() + "'";
							}
							else if (!result8)
							{
								defaultView.RowFilter = "LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result8.ToString() + "'";
							}
							DataSet dataSet2 = new DataSet();
							DataTable dataTable = defaultView.ToTable();
							dataSet2.Tables.Add(dataTable);
							bool flag2 = false;
							for (int k = 0; k < list.Count; k++)
							{
								DateTime start = list[k].Start;
								DateTime end = list[k].End;
								if (result10 >= start && result11 <= end)
								{
									flag2 = true;
								}
							}
							object obj;
							object obj2;
							if (result10 != d && result11 != d && !flag2)
							{
								DataTable dataTable2 = new DataTable();
								dataTable2 = dataTable.DefaultView.ToTable(true, "DaysTaken");
								new DataTable();
								DataTable dataTable3 = dataTable.DefaultView.ToTable(true, "ToLessTaken");
								obj = dataTable2.Compute("Sum(DaysTaken)", "DaysTaken <> 0");
								obj2 = dataTable3.Compute("Sum(ToLessTaken)", "ToLessTaken <> 0");
								list.Add(new Leavelist(result10, result11));
							}
							else
							{
								obj = DBNull.Value;
								obj2 = DBNull.Value;
							}
							if ((obj != DBNull.Value && obj2 != DBNull.Value) & result12)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = int.Parse(obj.ToString()) - int.Parse(obj2.ToString());
							}
							else if (obj != DBNull.Value)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = obj;
							}
							else if (obj == DBNull.Value && result8)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = 0;
							}
							int.TryParse(dataSet.Tables[0].Rows[j]["TotalTaken"].ToString(), out result9);
							int.TryParse(dataSet.Tables[0].Rows[j]["AnnualAllowedDays"].ToString(), out result7);
							if (result8)
							{
								result6 = 0;
							}
							dataSet.Tables[0].Rows[j]["TotalLeaves"] = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7;
							if (result8 && num2 != result9 && !flag2)
							{
								num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result9 + MinusLeaves;
								num2 = result9;
							}
							else if (!result8)
							{
								num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result5;
							}
							else if ((result8 && num2 != result9) & flag2)
							{
								num = (decimal)result7 + MinusLeaves;
								dataSet.Tables[0].Rows[j]["TotalTaken"] = DBNull.Value;
							}
							else if (result8 && num2 == result9 && !flag2)
							{
								num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result9 + MinusLeaves;
							}
							else if (result8 && num2 == 0 && result9 == 0)
							{
								num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result9 + MinusLeaves;
							}
							if (num >= 0m && result8 && MinusLeaves != 0m)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = -1m * MinusLeaves;
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = num;
								MinusLeaves = 0m;
							}
							else if (num >= 0m && result8 && MinusLeaves == 0m)
							{
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = num;
							}
							else if (num < 0m && result8)
							{
								dataSet.Tables[0].Rows[j]["TotalTaken"] = result7;
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = 0;
								MinusLeaves = num;
							}
							else if (!result8)
							{
								dataSet.Tables[0].Rows[j]["LeavesRemaining"] = num;
							}
							if (result10.Date < dateTimePickerStartDate.ValueFrom && result11.Date < dateTimePickerStartDate.ValueFrom && result7 != 0)
							{
								dataSet.Tables[0].Rows[j].Delete();
								continue;
							}
							if (result7 == 0 && !list2.Contains(dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString()))
							{
								list2.Add(dataSet.Tables[0].Rows[j]["LeaveTypeID"].ToString());
							}
							else if (result7 == 0)
							{
								dataSet.Tables[0].Rows[j].Delete();
								continue;
							}
							int result13 = 0;
							int.TryParse(dataSet.Tables[0].Rows[j]["LeavesRemaining"].ToString(), out result13);
						}
						dataSet.Tables[0].Columns.Remove("LeaveDayswithType");
						dataSet.Tables[0].Columns.Remove("ToLessTaken");
						dataSet.AcceptChanges();
					}
					DataView defaultView2 = dataSet.Tables[0].DefaultView;
					defaultView2.RowFilter = "LeaveTypeID='" + TypeID.Trim() + "'";
					DataSet dataSet3 = new DataSet();
					DataTable dataTable4 = defaultView2.ToTable();
					dataSet3.Tables.Add(dataTable4);
					object obj3 = dataTable4.Compute("Sum(LeavesRemaining)", "LeavesRemaining <> 0");
					labelLeaveAvaildays.Text = "";
					if (IsNewRecord)
					{
						labelLeaveAvaildays.Text = obj3.ToString();
					}
				}
			}
		}

		private double GetDeductionProportion(string ID)
		{
			try
			{
				return Factory.LeaveTypeSystem.GetDeductionProportion(ID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return 0.0;
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeLeaveRequestListFormObj);
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid) != DialogResult.No)
			{
				if (!IsNewRecord && checkBoxApproved.Checked)
				{
					ErrorHelper.ErrorMessage("Leave has already approved. You are not able to modify.");
				}
				else if (Void(!isVoid))
				{
					IsVoid = true;
				}
				else
				{
					ErrorHelper.ErrorMessage("Unable to void the transaction.");
				}
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				return Factory.EmployeeLeaveDetailSystem.VoidEmployeeLeaveDetail(textBoxCode.Text, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool Delete()
		{
			try
			{
				return Factory.EmployeeLeaveDetailSystem.DeleteEmployeeLeaveDetail(textBoxCode.Text, textBoxVoucherNumber.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (comboBoxEmployee.SelectedID != null && comboBoxEmployee.SelectedID != "")
			{
				string selectedID = comboBoxEmployee.SelectedID;
				FormActivator.BringFormToFront(FormActivator.LeaveAvailabilityFormObj);
				FormActivator.LeaveAvailabilityFormObj.LoadData(selectedID, textBoxEmployeeName.Text, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value);
			}
		}

		private void LinkLabelEmployee_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void dateTimePickerEndDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				labelLeaveAvaildays.Text = "";
				if (comboBoxLeaveType.SelectedID != "" && comboBoxEmployee.SelectedID != "")
				{
					textBoxDays.Text = GetDeductionProportion(comboBoxLeaveType.SelectedID).ToString("0.0");
					if (textBoxDays.Text == "0.0")
					{
						textBoxDays.Clear();
					}
					CheckAvailableLeaves(comboBoxLeaveType.SelectedID);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				labelLeaveAvaildays.Text = "";
				if (comboBoxLeaveType.SelectedID != "" && comboBoxEmployee.SelectedID != "")
				{
					textBoxDays.Text = GetDeductionProportion(comboBoxLeaveType.SelectedID).ToString("0.0");
					if (textBoxDays.Text == "0.0")
					{
						textBoxDays.Clear();
					}
					CheckAvailableLeaves(comboBoxLeaveType.SelectedID);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntitySysDocID = "EMP_LRQ";
					docManagementForm.EntityName = "EMP_LRQ";
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: false);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData()))
				{
					DataSet employeeActivityToPrint = Factory.EmployeeActivitySystem.GetEmployeeActivityToPrint(Convert.ToInt32(textBoxCode.Text));
					DataSet employeeLeaveAvailability = Factory.EmployeeLeaveDetailSystem.GetEmployeeLeaveAvailability(comboBoxEmployee.SelectedID, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value, "");
					if (employeeLeaveAvailability.Tables.Contains("LeavesTaken"))
					{
						employeeLeaveAvailability.Tables.Remove("LeavesTaken");
					}
					if (employeeLeaveAvailability.Tables.Contains("ActualLeave"))
					{
						employeeLeaveAvailability.Tables.Remove("ActualLeave");
					}
					employeeActivityToPrint.Merge(employeeLeaveAvailability);
					DataSet employeeLeaveList = Factory.EmployeeActivitySystem.GetEmployeeLeaveList(comboBoxEmployee.SelectedID);
					employeeActivityToPrint.Merge(employeeLeaveList);
					employeeActivityToPrint.Relations.Add("Employee_LeaveDetails", new DataColumn[1]
					{
						employeeActivityToPrint.Tables["Employee_Activity"].Columns["EmployeeID"]
					}, new DataColumn[1]
					{
						employeeActivityToPrint.Tables["LeaveAvailability"].Columns["EmployeeID"]
					}, createConstraints: false);
					employeeActivityToPrint.Relations.Add("PreviousLeaveDetails", new DataColumn[1]
					{
						employeeActivityToPrint.Tables["Employee_Activity"].Columns["EmployeeID"]
					}, new DataColumn[1]
					{
						employeeActivityToPrint.Tables["Employee_Leave_Request"].Columns["EmployeeID"]
					}, createConstraints: false);
					if (employeeActivityToPrint == null || employeeActivityToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(employeeActivityToPrint, "", "Leave Request", SysDocTypes.None, isPrint, showPrintDialog);
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

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		public void EditDocument(string DocNumber, int ActivityID)
		{
			LoadData(ActivityID);
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Employee_Leave_Request", "ActivityID", toolStripTextBoxFind.Text.Trim()))
				{
					string text = toolStripTextBoxFind.Text.Trim();
					textBoxCode.Text = text;
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

		private void EmpComboSetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessAllSub))
			{
				allsubordiantes = false;
			}
			else
			{
				allsubordiantes = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessSelfRequest))
			{
				selfRequest = false;
			}
			else
			{
				selfRequest = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessIntermediate))
			{
				allintermediatesub = false;
			}
			else
			{
				allintermediatesub = true;
			}
		}

		private void toolStripButton5_Click(object sender, EventArgs e)
		{
		}

		private void labelResume_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (ResumeActivityID != 0)
			{
				FormActivator.BringFormToFront(FormActivator.EmployeeResumptionFormObj);
				FormActivator.EmployeeResumptionFormObj.IsVisible = true;
				FormActivator.EmployeeResumptionFormObj.LoadData(ResumeActivityID);
			}
		}

		private void textBoxReason_Leave(object sender, EventArgs e)
		{
			try
			{
				labelLeaveAvaildays.Text = "";
				if (comboBoxLeaveType.SelectedID != "" && comboBoxEmployee.SelectedID != "")
				{
					CheckAvailableLeaves(comboBoxLeaveType.SelectedID);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxNote_Leave(object sender, EventArgs e)
		{
			try
			{
				labelLeaveAvaildays.Text = "";
				if (comboBoxLeaveType.SelectedID != "" && comboBoxEmployee.SelectedID != "")
				{
					CheckAvailableLeaves(comboBoxLeaveType.SelectedID);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLeaveType(comboBoxLeaveType.SelectedID);
		}

		private void labelLeaveAvaildays_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		public DataSet EmpComboFillData(DataSet data)
		{
			EmpComboSetSecurity();
			_ = data.Tables[0];
			filteredReportToID = "ABC";
			if (filteredReportToID != "")
			{
				string text = Global.CurrentUserEmployeeID.ToString();
				text = Factory.DatabaseSystem.GetFieldValue("Users", "EmployeeID", "UserID", Global.CurrentUser).ToString();
				DataRow[] array = null;
				if (!allsubordiantes)
				{
					if (allintermediatesub)
					{
						array = data.Tables[0].Select("ReportToID = '" + text + "' OR Code = '" + text + "'");
					}
					else if (selfRequest)
					{
						array = data.Tables[0].Select("Code = '" + text + "'");
					}
				}
				DataSet dataSet = new DataSet();
				if (array != null)
				{
					if (array.Length != 0)
					{
						dataSet.Merge(array);
					}
					else
					{
						dataSet = data.Clone();
					}
					data = dataSet;
				}
			}
			else
			{
				data = data.Clone();
			}
			return data;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeLeaveRequestForm));
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
			toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxFromLocation = new Micromind.UISupport.MMTextBox();
			textBoxFromDivision = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxFromDepartment = new Micromind.UISupport.MMTextBox();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxReason = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxReference = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			label1TransactionID = new Micromind.UISupport.MMLabel();
			textBoxDesignation = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			labelTerminated = new Micromind.UISupport.MMLabel();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			dateTimePickerStartDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel11 = new Micromind.UISupport.MMLabel();
			dateTimePickerEndDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxDays = new Micromind.UISupport.NumberTextBox();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			checkBoxApproved = new System.Windows.Forms.CheckBox();
			checkBoxRejected = new System.Windows.Forms.CheckBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			dateTimePickerTransactionDate = new Micromind.UISupport.MMSDateTimePicker(components);
			linkLabelVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxResume = new Micromind.UISupport.MMTextBox();
			labelResume = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelLeaveAvaildays = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimePickerTravellingDate = new Micromind.UISupport.MMSDateTimePicker(components);
			labelDisplayAdays = new Micromind.UISupport.MMLabel();
			labelADays = new Micromind.UISupport.MMLabel();
			comboBoxEmployee = new Micromind.DataControls.EmployeeFilterComboBox();
			comboBoxLeaveType = new Micromind.DataControls.LeaveTypeComboBox();
			formManager = new Micromind.DataControls.FormManager();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeaveType).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
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
				toolStripButton5,
				toolStripButtonPreview,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(595, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
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
			toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton5.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton5.Name = "toolStripButton5";
			toolStripButton5.Size = new System.Drawing.Size(28, 28);
			toolStripButton5.Text = "&Print";
			toolStripButton5.ToolTipText = "Print (Ctrl+P)";
			toolStripButton5.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 382);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(595, 40);
			panelButtons.TabIndex = 15;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(216, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 24);
			buttonVoid.TabIndex = 15;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(595, 1);
			linePanelDown.TabIndex = 0;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(316, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
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
			xpButton1.Location = new System.Drawing.Point(485, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
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
			textBoxFromLocation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFromLocation.CustomReportFieldName = "";
			textBoxFromLocation.CustomReportKey = "";
			textBoxFromLocation.CustomReportValueType = 1;
			textBoxFromLocation.IsComboTextBox = false;
			textBoxFromLocation.IsModified = false;
			textBoxFromLocation.Location = new System.Drawing.Point(329, 100);
			textBoxFromLocation.MaxLength = 15;
			textBoxFromLocation.Name = "textBoxFromLocation";
			textBoxFromLocation.ReadOnly = true;
			textBoxFromLocation.Size = new System.Drawing.Size(141, 20);
			textBoxFromLocation.TabIndex = 1;
			textBoxFromLocation.TabStop = false;
			textBoxFromDivision.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDivision.CustomReportFieldName = "";
			textBoxFromDivision.CustomReportKey = "";
			textBoxFromDivision.CustomReportValueType = 1;
			textBoxFromDivision.IsComboTextBox = false;
			textBoxFromDivision.IsModified = false;
			textBoxFromDivision.Location = new System.Drawing.Point(112, 122);
			textBoxFromDivision.MaxLength = 64;
			textBoxFromDivision.Name = "textBoxFromDivision";
			textBoxFromDivision.ReadOnly = true;
			textBoxFromDivision.Size = new System.Drawing.Size(140, 20);
			textBoxFromDivision.TabIndex = 4;
			textBoxFromDivision.TabStop = false;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(258, 101);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(51, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Location:";
			textBoxFromDepartment.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDepartment.CustomReportFieldName = "";
			textBoxFromDepartment.CustomReportKey = "";
			textBoxFromDepartment.CustomReportValueType = 1;
			textBoxFromDepartment.IsComboTextBox = false;
			textBoxFromDepartment.IsModified = false;
			textBoxFromDepartment.Location = new System.Drawing.Point(329, 122);
			textBoxFromDepartment.MaxLength = 255;
			textBoxFromDepartment.Name = "textBoxFromDepartment";
			textBoxFromDepartment.ReadOnly = true;
			textBoxFromDepartment.Size = new System.Drawing.Size(141, 20);
			textBoxFromDepartment.TabIndex = 5;
			textBoxFromDepartment.TabStop = false;
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.IsModified = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(112, 78);
			textBoxEmployeeName.MaxLength = 15;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(358, 20);
			textBoxEmployeeName.TabIndex = 1;
			textBoxEmployeeName.TabStop = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(13, 126);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(47, 13);
			mmLabel3.TabIndex = 18;
			mmLabel3.Text = "Division:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(258, 125);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(65, 13);
			mmLabel5.TabIndex = 9;
			mmLabel5.Text = "Department:";
			textBoxReason.BackColor = System.Drawing.Color.White;
			textBoxReason.CustomReportFieldName = "";
			textBoxReason.CustomReportKey = "";
			textBoxReason.CustomReportValueType = 1;
			textBoxReason.IsComboTextBox = false;
			textBoxReason.IsModified = false;
			textBoxReason.Location = new System.Drawing.Point(112, 241);
			textBoxReason.MaxLength = 255;
			textBoxReason.Name = "textBoxReason";
			textBoxReason.Size = new System.Drawing.Size(411, 20);
			textBoxReason.TabIndex = 10;
			textBoxReason.Leave += new System.EventHandler(textBoxReason_Leave);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(13, 245);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(47, 13);
			mmLabel1.TabIndex = 20;
			mmLabel1.Text = "Reason:";
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(393, 177);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(27, 13);
			mmLabel9.TabIndex = 9;
			mmLabel9.Text = "Ref:";
			textBoxReference.BackColor = System.Drawing.Color.White;
			textBoxReference.CustomReportFieldName = "";
			textBoxReference.CustomReportKey = "";
			textBoxReference.CustomReportValueType = 1;
			textBoxReference.IsComboTextBox = false;
			textBoxReference.IsModified = false;
			textBoxReference.Location = new System.Drawing.Point(433, 174);
			textBoxReference.MaxLength = 15;
			textBoxReference.Name = "textBoxReference";
			textBoxReference.Size = new System.Drawing.Size(90, 20);
			textBoxReference.TabIndex = 5;
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(13, 264);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(30, 13);
			mmLabel10.TabIndex = 21;
			mmLabel10.Text = "Note";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(112, 263);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(469, 77);
			textBoxNote.TabIndex = 11;
			textBoxNote.Leave += new System.EventHandler(textBoxNote_Leave);
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(383, 56);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(87, 20);
			textBoxCode.TabIndex = 1;
			textBoxCode.TabStop = false;
			textBoxCode.Visible = false;
			label1TransactionID.AutoSize = true;
			label1TransactionID.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1TransactionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			label1TransactionID.IsFieldHeader = false;
			label1TransactionID.IsRequired = false;
			label1TransactionID.Location = new System.Drawing.Point(297, 58);
			label1TransactionID.Name = "label1TransactionID";
			label1TransactionID.PenWidth = 1f;
			label1TransactionID.ShowBorder = false;
			label1TransactionID.Size = new System.Drawing.Size(80, 13);
			label1TransactionID.TabIndex = 9;
			label1TransactionID.Text = "Transaction ID:";
			label1TransactionID.Visible = false;
			textBoxDesignation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDesignation.CustomReportFieldName = "";
			textBoxDesignation.CustomReportKey = "";
			textBoxDesignation.CustomReportValueType = 1;
			textBoxDesignation.IsComboTextBox = false;
			textBoxDesignation.IsModified = false;
			textBoxDesignation.Location = new System.Drawing.Point(112, 100);
			textBoxDesignation.MaxLength = 15;
			textBoxDesignation.Name = "textBoxDesignation";
			textBoxDesignation.ReadOnly = true;
			textBoxDesignation.Size = new System.Drawing.Size(140, 20);
			textBoxDesignation.TabIndex = 1;
			textBoxDesignation.TabStop = false;
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(13, 103);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(66, 13);
			mmLabel7.TabIndex = 19;
			mmLabel7.Text = "Designation:";
			labelTerminated.AutoSize = true;
			labelTerminated.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelTerminated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelTerminated.ForeColor = System.Drawing.Color.Red;
			labelTerminated.IsFieldHeader = false;
			labelTerminated.IsRequired = false;
			labelTerminated.Location = new System.Drawing.Point(297, 59);
			labelTerminated.Name = "labelTerminated";
			labelTerminated.PenWidth = 1f;
			labelTerminated.ShowBorder = false;
			labelTerminated.Size = new System.Drawing.Size(152, 13);
			labelTerminated.TabIndex = 9;
			labelTerminated.Text = "Employee is already terminated";
			labelTerminated.Visible = false;
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = true;
			mmLabel12.Location = new System.Drawing.Point(13, 200);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(69, 13);
			mmLabel12.TabIndex = 18;
			mmLabel12.Text = "Start Date:";
			dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerStartDate.Location = new System.Drawing.Point(112, 196);
			dateTimePickerStartDate.Name = "dateTimePickerStartDate";
			dateTimePickerStartDate.Size = new System.Drawing.Size(98, 20);
			dateTimePickerStartDate.TabIndex = 6;
			dateTimePickerStartDate.Value = new System.DateTime(2015, 1, 15, 10, 1, 17, 344);
			dateTimePickerStartDate.ValueChanged += new System.EventHandler(dateTimePickerStartDate_ValueChanged);
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = true;
			mmLabel11.Location = new System.Drawing.Point(213, 200);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(64, 13);
			mmLabel11.TabIndex = 0;
			mmLabel11.Text = "End Date:";
			dateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerEndDate.Location = new System.Drawing.Point(283, 196);
			dateTimePickerEndDate.Name = "dateTimePickerEndDate";
			dateTimePickerEndDate.Size = new System.Drawing.Size(104, 20);
			dateTimePickerEndDate.TabIndex = 7;
			dateTimePickerEndDate.Value = new System.DateTime(2015, 1, 15, 10, 1, 17, 343);
			dateTimePickerEndDate.ValueChanged += new System.EventHandler(dateTimePickerEndDate_ValueChanged);
			textBoxDays.AllowDecimal = false;
			textBoxDays.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDays.CustomReportFieldName = "";
			textBoxDays.CustomReportKey = "";
			textBoxDays.CustomReportValueType = 1;
			textBoxDays.ForeColor = System.Drawing.Color.Black;
			textBoxDays.IsComboTextBox = false;
			textBoxDays.IsModified = false;
			textBoxDays.Location = new System.Drawing.Point(433, 196);
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
			textBoxDays.Size = new System.Drawing.Size(90, 20);
			textBoxDays.TabIndex = 8;
			textBoxDays.Text = "0";
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(393, 199);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(34, 13);
			mmLabel13.TabIndex = 9;
			mmLabel13.Text = "Days:";
			checkBoxApproved.AutoSize = true;
			checkBoxApproved.Enabled = false;
			checkBoxApproved.Location = new System.Drawing.Point(114, 345);
			checkBoxApproved.Name = "checkBoxApproved";
			checkBoxApproved.Size = new System.Drawing.Size(72, 17);
			checkBoxApproved.TabIndex = 12;
			checkBoxApproved.Text = "Approved";
			checkBoxApproved.UseVisualStyleBackColor = true;
			checkBoxRejected.AutoSize = true;
			checkBoxRejected.Enabled = false;
			checkBoxRejected.Location = new System.Drawing.Point(218, 345);
			checkBoxRejected.Name = "checkBoxRejected";
			checkBoxRejected.Size = new System.Drawing.Size(69, 17);
			checkBoxRejected.TabIndex = 13;
			checkBoxRejected.Text = "Rejected";
			checkBoxRejected.UseVisualStyleBackColor = true;
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = true;
			mmLabel8.Location = new System.Drawing.Point(213, 178);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(68, 13);
			mmLabel8.TabIndex = 0;
			mmLabel8.Text = "App. Date:";
			dateTimePickerTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerTransactionDate.Location = new System.Drawing.Point(283, 174);
			dateTimePickerTransactionDate.Name = "dateTimePickerTransactionDate";
			dateTimePickerTransactionDate.Size = new System.Drawing.Size(104, 20);
			dateTimePickerTransactionDate.TabIndex = 4;
			dateTimePickerTransactionDate.Value = new System.DateTime(2015, 1, 15, 10, 1, 17, 341);
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			linkLabelVoucherNumber.Appearance = appearance;
			linkLabelVoucherNumber.AutoSize = true;
			linkLabelVoucherNumber.Location = new System.Drawing.Point(13, 37);
			linkLabelVoucherNumber.Name = "linkLabelVoucherNumber";
			linkLabelVoucherNumber.Size = new System.Drawing.Size(77, 15);
			linkLabelVoucherNumber.TabIndex = 137;
			linkLabelVoucherNumber.TabStop = true;
			linkLabelVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVoucherNumber.Value = "Doc Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			linkLabelVoucherNumber.VisitedLinkAppearance = appearance2;
			linkLabelVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVoucherNumber_LinkClicked);
			textBoxVoucherNumber.Location = new System.Drawing.Point(111, 34);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 20);
			textBoxVoucherNumber.TabIndex = 0;
			textBoxResume.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxResume.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxResume.CustomReportFieldName = "";
			textBoxResume.CustomReportKey = "";
			textBoxResume.CustomReportValueType = 1;
			textBoxResume.IsComboTextBox = false;
			textBoxResume.IsModified = false;
			textBoxResume.Location = new System.Drawing.Point(442, 343);
			textBoxResume.MaxLength = 15;
			textBoxResume.Name = "textBoxResume";
			textBoxResume.ReadOnly = true;
			textBoxResume.Size = new System.Drawing.Size(141, 20);
			textBoxResume.TabIndex = 14;
			textBoxResume.TabStop = false;
			appearance3.FontData.BoldAsString = "False";
			appearance3.FontData.Name = "Tahoma";
			labelResume.Appearance = appearance3;
			labelResume.AutoSize = true;
			labelResume.Location = new System.Drawing.Point(360, 346);
			labelResume.Name = "labelResume";
			labelResume.Size = new System.Drawing.Size(70, 15);
			labelResume.TabIndex = 17;
			labelResume.TabStop = true;
			labelResume.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelResume.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelResume.Value = "Resumed On:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			labelResume.VisitedLinkAppearance = appearance4;
			labelResume.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelResume_LinkClicked);
			appearance5.FontData.BoldAsString = "False";
			appearance5.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance5;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(13, 152);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(87, 15);
			ultraFormattedLinkLabel3.TabIndex = 21;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Available Leave :";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance6;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			labelLeaveAvaildays.AutoSize = true;
			labelLeaveAvaildays.Location = new System.Drawing.Point(110, 152);
			labelLeaveAvaildays.Name = "labelLeaveAvaildays";
			labelLeaveAvaildays.Size = new System.Drawing.Size(15, 13);
			labelLeaveAvaildays.TabIndex = 2;
			labelLeaveAvaildays.Text = "D";
			labelLeaveAvaildays.Click += new System.EventHandler(labelLeaveAvaildays_Click);
			appearance7.FontData.BoldAsString = "True";
			appearance7.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance7;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(13, 59);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(62, 15);
			ultraFormattedLinkLabel4.TabIndex = 140;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Employee:";
			appearance8.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance8;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance9.FontData.BoldAsString = "False";
			appearance9.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance9;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(13, 174);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(67, 15);
			ultraFormattedLinkLabel1.TabIndex = 141;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Leave Type :";
			appearance10.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance10;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(13, 222);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(82, 13);
			mmLabel2.TabIndex = 145;
			mmLabel2.Text = "Travelling Date:";
			dateTimePickerTravellingDate.Checked = false;
			dateTimePickerTravellingDate.CustomFormat = " ";
			dateTimePickerTravellingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerTravellingDate.Location = new System.Drawing.Point(111, 218);
			dateTimePickerTravellingDate.Name = "dateTimePickerTravellingDate";
			dateTimePickerTravellingDate.ShowCheckBox = true;
			dateTimePickerTravellingDate.Size = new System.Drawing.Size(124, 20);
			dateTimePickerTravellingDate.TabIndex = 9;
			dateTimePickerTravellingDate.Value = new System.DateTime(0L);
			labelDisplayAdays.AutoSize = true;
			labelDisplayAdays.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelDisplayAdays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelDisplayAdays.IsFieldHeader = false;
			labelDisplayAdays.IsRequired = false;
			labelDisplayAdays.Location = new System.Drawing.Point(360, 219);
			labelDisplayAdays.Name = "labelDisplayAdays";
			labelDisplayAdays.PenWidth = 1f;
			labelDisplayAdays.ShowBorder = false;
			labelDisplayAdays.Size = new System.Drawing.Size(67, 13);
			labelDisplayAdays.TabIndex = 146;
			labelDisplayAdays.Text = "Actual Days:";
			labelADays.AutoSize = true;
			labelADays.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelADays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelADays.IsFieldHeader = false;
			labelADays.IsRequired = false;
			labelADays.Location = new System.Drawing.Point(433, 219);
			labelADays.Name = "labelADays";
			labelADays.PenWidth = 1f;
			labelADays.ShowBorder = false;
			labelADays.Size = new System.Drawing.Size(13, 13);
			labelADays.TabIndex = 147;
			labelADays.Text = "0";
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = null;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance11;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance12.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance12.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance12;
			appearance13.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance13;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance14.BackColor2 = System.Drawing.SystemColors.Control;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance14.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance14;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance15;
			appearance16.BackColor = System.Drawing.SystemColors.Highlight;
			appearance16.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance16;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance17;
			appearance18.BorderColor = System.Drawing.Color.Silver;
			appearance18.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance18;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance19.BackColor = System.Drawing.SystemColors.Control;
			appearance19.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance19.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance19.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance19;
			appearance20.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance20;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance21;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance22.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance22;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(112, 56);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(170, 20);
			comboBoxEmployee.TabIndex = 1;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployee.SelectedIndexChanged += new System.EventHandler(comboBoxEmployee_SelectedIndexChanged);
			comboBoxLeaveType.Assigned = false;
			comboBoxLeaveType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLeaveType.CustomReportFieldName = "";
			comboBoxLeaveType.CustomReportKey = "";
			comboBoxLeaveType.CustomReportValueType = 1;
			comboBoxLeaveType.DescriptionTextBox = null;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLeaveType.DisplayLayout.Appearance = appearance23;
			comboBoxLeaveType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLeaveType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance24.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance24.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeaveType.DisplayLayout.GroupByBox.Appearance = appearance24;
			appearance25.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeaveType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance25;
			comboBoxLeaveType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance26.BackColor2 = System.Drawing.SystemColors.Control;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance26.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeaveType.DisplayLayout.GroupByBox.PromptAppearance = appearance26;
			comboBoxLeaveType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLeaveType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLeaveType.DisplayLayout.Override.ActiveCellAppearance = appearance27;
			appearance28.BackColor = System.Drawing.SystemColors.Highlight;
			appearance28.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLeaveType.DisplayLayout.Override.ActiveRowAppearance = appearance28;
			comboBoxLeaveType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLeaveType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLeaveType.DisplayLayout.Override.CardAreaAppearance = appearance29;
			appearance30.BorderColor = System.Drawing.Color.Silver;
			appearance30.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLeaveType.DisplayLayout.Override.CellAppearance = appearance30;
			comboBoxLeaveType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLeaveType.DisplayLayout.Override.CellPadding = 0;
			appearance31.BackColor = System.Drawing.SystemColors.Control;
			appearance31.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance31.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance31.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeaveType.DisplayLayout.Override.GroupByRowAppearance = appearance31;
			appearance32.TextHAlignAsString = "Left";
			comboBoxLeaveType.DisplayLayout.Override.HeaderAppearance = appearance32;
			comboBoxLeaveType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLeaveType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.BorderColor = System.Drawing.Color.Silver;
			comboBoxLeaveType.DisplayLayout.Override.RowAppearance = appearance33;
			comboBoxLeaveType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLeaveType.DisplayLayout.Override.TemplateAddRowAppearance = appearance34;
			comboBoxLeaveType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLeaveType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLeaveType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLeaveType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLeaveType.Editable = true;
			comboBoxLeaveType.FilterString = "";
			comboBoxLeaveType.HasAllAccount = false;
			comboBoxLeaveType.HasCustom = false;
			comboBoxLeaveType.IsDataLoaded = false;
			comboBoxLeaveType.Location = new System.Drawing.Point(112, 174);
			comboBoxLeaveType.MaxDropDownItems = 12;
			comboBoxLeaveType.Name = "comboBoxLeaveType";
			comboBoxLeaveType.ShowInactiveItems = false;
			comboBoxLeaveType.ShowQuickAdd = true;
			comboBoxLeaveType.Size = new System.Drawing.Size(98, 20);
			comboBoxLeaveType.TabIndex = 3;
			comboBoxLeaveType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 20;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(595, 422);
			base.Controls.Add(labelADays);
			base.Controls.Add(labelDisplayAdays);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(dateTimePickerTravellingDate);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(comboBoxEmployee);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(labelLeaveAvaildays);
			base.Controls.Add(ultraFormattedLinkLabel3);
			base.Controls.Add(labelResume);
			base.Controls.Add(textBoxResume);
			base.Controls.Add(linkLabelVoucherNumber);
			base.Controls.Add(textBoxVoucherNumber);
			base.Controls.Add(checkBoxRejected);
			base.Controls.Add(checkBoxApproved);
			base.Controls.Add(textBoxDays);
			base.Controls.Add(comboBoxLeaveType);
			base.Controls.Add(dateTimePickerTransactionDate);
			base.Controls.Add(dateTimePickerEndDate);
			base.Controls.Add(dateTimePickerStartDate);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxReference);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxReason);
			base.Controls.Add(textBoxFromDepartment);
			base.Controls.Add(textBoxFromDivision);
			base.Controls.Add(labelTerminated);
			base.Controls.Add(label1TransactionID);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(textBoxEmployeeName);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(textBoxDesignation);
			base.Controls.Add(textBoxFromLocation);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(mmLabel13);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel11);
			base.Controls.Add(mmLabel12);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(603, 381);
			base.Name = "EmployeeLeaveRequestForm";
			Text = "Employee Leave Request Entry";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeaveType).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
