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
	public class EmployeeResumptionForm : Form, IForm
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

		private const string TABLENAME_CONST = "Employee_Resumption";

		private const string IDFIELD_CONST = "ActivityID";

		private bool isNewRecord = true;

		private DateTime endDate;

		private string activityID = "";

		private decimal minusLeaves;

		private bool isVisible;

		private double actualleavedays;

		private DateTime originalenddate;

		private ScreenAccessRight screenRight;

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

		private MMLabel mmLabel2;

		private MMTextBox textBoxEmployeeName;

		private MMLabel mmLabel3;

		private MMLabel mmLabel5;

		private MMLabel mmLabel9;

		private MMTextBox textBoxReference;

		private MMLabel mmLabel10;

		private MMTextBox textBoxNote;

		private MMTextBox textBoxCode;

		private MMLabel label1TransactionID;

		private MMTextBox textBoxDesignation;

		private MMLabel mmLabel7;

		private MMLabel labelTerminated;

		private MMLabel mmLabel13;

		private MMLabel mmLabel8;

		private MMSDateTimePicker dateTimePickerTransactionDate;

		private MMTextBox textBoxLeaveID;

		private XPButton buttonSelectEmployee;

		private MMTextBox textBoxReqDays;

		private MMLabel mmLabel1;

		private MMLabel mmLabel6;

		private MMTextBox textBoxEmployeeID;

		private MMLabel mmLabel11;

		private MMTextBox textBoxStartDate;

		private MMTextBox textBoxEndDate;

		private MMTextBox textBoxActualDaysonResume;

		private MMLabel mmLabel12;

		private MMTextBox textBoxDiffDays;

		private MMLabel mmLabel14;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonInformation;

		private Panel panel1;

		private GroupBox groupBox22;

		private RadioButton radioButtonCreateNewLeave;

		private RadioButton radioButtonExtendLeaveDate;

		private MMLabel mmLabel15;

		private RadioButton radioButtonAsItIs;

		private GroupBox groupBoxLeaveRequest;

		private Label labelLeaveAvaildays;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private NumberTextBox textBoxDays;

		private LeaveTypeComboBox comboBoxLeaveType;

		private MMSDateTimePicker mmsDateTimePicker1;

		private MMSDateTimePicker dateTimePickerEndDate;

		private MMSDateTimePicker dateTimePickerStartDate;

		private MMTextBox mmTextBox1;

		private MMTextBox mmTextBox2;

		private MMTextBox textBoxReason;

		private MMLabel mmLabel16;

		private MMLabel mmLabel17;

		private MMLabel mmLabel18;

		private MMLabel mmLabel19;

		private MMLabel mmLabel20;

		private MMLabel mmLabel21;

		private MMLabel mmLabel22;

		private MMLabel mmLabel23;

		private MMTextBox textBoxActualTaken;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonPrint;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5038;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

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

		public bool IsVisible
		{
			set
			{
				bool flag2 = buttonDelete.Visible = value;
				isVisible = flag2;
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
			}
		}

		private DateTime OriginalEndDate
		{
			get
			{
				return originalenddate;
			}
			set
			{
				originalenddate = value;
			}
		}

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
					textBoxCode.Clear();
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					labelTerminated.Visible = false;
				}
				panel1.Visible = isNewRecord;
				buttonSelectEmployee.Enabled = isNewRecord;
				MMLabel mMLabel = label1TransactionID;
				bool visible = textBoxCode.Visible = !isNewRecord;
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

		public EmployeeResumptionForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EmployeeResumptionForm_Load;
			dateTimePickerTransactionDate.ValueChanged += dateTimePickerTransactionDate_ValueChanged;
			dateTimePickerStartDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dateTimePickerEndDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dateTimePickerTransactionDate.ValueChanged += dateTimePickerDate_ValueChanged;
			comboBoxLeaveType.SelectedIndexChanged += comboBoxLeaveType_SelectedIndexChanged;
			comboBoxLeaveType.SelectedIndexChanged += dateTimePickerDate_ValueChanged;
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

		private void comboBoxLeaveType_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				labelLeaveAvaildays.Text = "";
				if (comboBoxLeaveType.SelectedID != "" && textBoxEmployeeID.Text != "")
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
			if (TypeID != "" && TypeID != string.Empty && textBoxEmployeeID.Text != "")
			{
				dataSet = Factory.EmployeeLeaveDetailSystem.GetEmployeeLeaveAvailability(textBoxEmployeeID.Text, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value, TypeID);
			}
			checked
			{
				if (textBoxEmployeeID.Text == "" || TypeID == "" || TypeID == string.Empty)
				{
					labelLeaveAvaildays.Text = "";
				}
				else if (dataSet.Tables[0].Rows.Count == 0 && textBoxEmployeeID.Text != "")
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
					DataView defaultView2 = dataSet.Tables[0].DefaultView;
					defaultView2.RowFilter = "LeaveTypeID='" + TypeID.Trim() + "'";
					DataSet dataSet3 = new DataSet();
					DataTable dataTable4 = defaultView2.ToTable();
					dataSet3.Tables.Add(dataTable4);
					object obj3 = dataTable4.Compute("Sum(LeavesRemaining)", "LeavesRemaining <> 0");
					labelLeaveAvaildays.Text = "";
					labelLeaveAvaildays.Text = obj3.ToString();
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

		private void dateTimePickerTransactionDate_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (textBoxLeaveID.Text != "")
				{
					textBoxActualDaysonResume.Text = dateTimePickerTransactionDate.Value.Subtract(DateTime.Parse(textBoxStartDate.Text)).Days.ToString();
					textBoxDiffDays.Text = checked(int.Parse(textBoxActualDaysonResume.Text) - int.Parse(textBoxReqDays.Text)).ToString();
					if (radioButtonCreateNewLeave.Checked)
					{
						dateTimePickerStartDate.Value = DateTime.Parse(endDate.AddDays(1.0).ToString());
						DateTime.Parse(textBoxStartDate.Text);
						TimeSpan value = dateTimePickerTransactionDate.Value - endDate.AddDays(1.0);
						int.Parse(value.Days.ToString());
						dateTimePickerEndDate.Value = endDate.Add(value);
					}
				}
			}
			catch
			{
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeActivityData(EmployeeActivityTypes.Resumption);
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeActivityTable.Rows[0] : currentData.EmployeeActivityTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EmployeeID"] = textBoxEmployeeID.Text;
				dataRow["ActivityType"] = (byte)12;
				dataRow["TransactionDate"] = dateTimePickerTransactionDate.Value;
				dataRow["Reference"] = textBoxReference.Text;
				if (radioButtonExtendLeaveDate.Checked)
				{
					dataRow["Note"] = textBoxNote.Text + " Leave Extended";
				}
				else if (radioButtonCreateNewLeave.Checked)
				{
					dataRow["Note"] = textBoxNote.Text + " New Leave Created";
				}
				else
				{
					dataRow["Note"] = textBoxNote.Text;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeActivityTable.Rows.Add(dataRow);
				}
				dataRow = ((!isNewRecord) ? currentData.EmployeeResumptionTable.Rows[0] : currentData.EmployeeResumptionTable.NewRow());
				dataRow["LeaveID"] = textBoxLeaveID.Text;
				if (radioButtonExtendLeaveDate.Checked)
				{
					dataRow["EndDate"] = DateTime.Parse(textBoxEndDate.Text);
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeResumptionTable.Rows.Add(dataRow);
				}
				EmployeeActivityData employeeActivityData = null;
				if (radioButtonCreateNewLeave.Checked)
				{
					employeeActivityData = new EmployeeActivityData(EmployeeActivityTypes.Leave);
					dataRow = ((!isNewRecord) ? employeeActivityData.EmployeeLeaveRequestTable.Rows[0] : employeeActivityData.EmployeeLeaveRequestTable.NewRow());
					dataRow["StartDate"] = dateTimePickerStartDate.Value;
					dataRow["EndDate"] = dateTimePickerEndDate.Value;
					dataRow["ActualLeaveDays"] = ActualLeaveDays;
					dataRow["IsApproved"] = "True";
					dataRow["ApprovedBy"] = Global.CurrentUser;
					dataRow["ApproveDate"] = DateTime.Now;
					dataRow["ResumptionDate"] = dateTimePickerTransactionDate.Value;
					if (!(comboBoxLeaveType.SelectedID != ""))
					{
						ErrorHelper.ErrorMessage("Please select the leave type.");
						return false;
					}
					dataRow["LeaveTypeID"] = comboBoxLeaveType.SelectedID;
					dataRow.EndEdit();
					if (isNewRecord)
					{
						employeeActivityData.EmployeeLeaveRequestTable.Rows.Add(dataRow);
					}
				}
				if (employeeActivityData != null && employeeActivityData.Tables.Count > 0 && employeeActivityData.Tables["Employee_Leave_request"].Rows.Count > 0)
				{
					currentData.Merge(employeeActivityData);
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
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables["Employee_Activity"].Rows[0];
				textBoxCode.Text = dataRow["ActivityID"].ToString();
				textBoxEmployeeID.Text = dataRow["EmployeeID"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				textBoxReference.Text = dataRow["Reference"].ToString();
				DateTime value = DateTime.Parse(dataRow["TransactionDate"].ToString());
				dataRow = currentData.Tables["Employee_Resumption"].Rows[0];
				textBoxLeaveID.Text = dataRow["LeaveID"].ToString();
				DataSet employeeLeaveByID = Factory.EmployeeActivitySystem.GetEmployeeLeaveByID(textBoxLeaveID.Text);
				if (employeeLeaveByID != null && employeeLeaveByID.Tables.Count > 0 && employeeLeaveByID.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow2 = employeeLeaveByID.Tables[0].Rows[0];
					textBoxFromLocation.Text = dataRow2["LocationName"].ToString();
					textBoxFromDivision.Text = dataRow2["DivisionName"].ToString();
					textBoxFromDepartment.Text = dataRow2["DepartmentName"].ToString();
					textBoxDesignation.Text = dataRow2["PositionName"].ToString();
					textBoxEmployeeName.Text = dataRow2["EmployeeName"].ToString();
					textBoxEmployeeID.Text = dataRow2["EmployeeID"].ToString();
					textBoxStartDate.Text = DateTime.Parse(dataRow2["StartDate"].ToString()).ToShortDateString();
					textBoxEndDate.Text = DateTime.Parse(dataRow2["EndDate"].ToString()).ToShortDateString();
				}
				DateTime dateTime = DateTime.Parse(textBoxStartDate.Text);
				TimeSpan timeSpan = DateTime.Parse(textBoxEndDate.Text).Add(TimeSpan.FromDays(1.0)) - dateTime;
				textBoxReqDays.Text = timeSpan.Days.ToString();
				textBoxActualDaysonResume.Text = dateTimePickerTransactionDate.Value.Subtract(dateTime).Days.ToString();
				textBoxDiffDays.Text = checked(int.Parse(textBoxActualDaysonResume.Text) - int.Parse(textBoxReqDays.Text)).ToString();
				dateTimePickerTransactionDate.Value = value;
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
				bool flag = (!isNewRecord) ? Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Resumption, isUpdate: true) : Factory.EmployeeActivitySystem.CreateEmployeeActivity(currentData, EmployeeActivityTypes.Resumption, isUpdate: false);
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
			if (radioButtonExtendLeaveDate.Checked || radioButtonCreateNewLeave.Checked)
			{
				DateTime d = DateTime.Parse(textBoxEndDate.Text);
				_ = (dateTimePickerTransactionDate.Value - d).TotalDays;
				_ = 1.0;
			}
			if (textBoxLeaveID.Text == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (Factory.EmployeeActivitySystem.AllowDelete(textBoxEmployeeID.Text, DateTime.Parse(textBoxStartDate.Text)).Tables["LeaveDetails"].Rows.Count > 0)
			{
				ErrorHelper.InformationMessage(textBoxEmployeeID.Text + " already have  previous leave to resume.Pls select that First!!");
				return false;
			}
			if ((radioButtonCreateNewLeave.Checked || radioButtonExtendLeaveDate.Checked) && !LeaveAvailability(comboBoxLeaveType.SelectedID))
			{
				return false;
			}
			return true;
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData()))
				{
					DataSet employeeResumptionDataToPrint = Factory.EmployeeActivitySystem.GetEmployeeResumptionDataToPrint(Convert.ToInt32(textBoxCode.Text));
					DataSet employeeLeaveAvailability = Factory.EmployeeLeaveDetailSystem.GetEmployeeLeaveAvailability(textBoxEmployeeID.Text, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value, "");
					if (employeeLeaveAvailability.Tables.Contains("LeavesTaken"))
					{
						employeeLeaveAvailability.Tables.Remove("LeavesTaken");
					}
					if (employeeLeaveAvailability.Tables.Contains("ActualLeave"))
					{
						employeeLeaveAvailability.Tables.Remove("ActualLeave");
					}
					employeeResumptionDataToPrint.Merge(employeeLeaveAvailability);
					employeeResumptionDataToPrint.Relations.Add("Employee_LeaveDetails", new DataColumn[1]
					{
						employeeResumptionDataToPrint.Tables["Employee_Activity"].Columns["EmployeeID"]
					}, new DataColumn[1]
					{
						employeeResumptionDataToPrint.Tables["LeaveAvailability"].Columns["EmployeeID"]
					}, createConstraints: false);
					if (employeeResumptionDataToPrint == null || employeeResumptionDataToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(employeeResumptionDataToPrint, "", "Duty Resumption", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private bool LeaveAvailability(string LeaveType)
		{
			DataSet dataSet = new DataSet();
			List<Leavelist> list = new List<Leavelist>();
			if (radioButtonCreateNewLeave.Checked)
			{
				dataSet = Factory.EmployeeLeaveDetailSystem.GetEmployeeLeaveAvailability(textBoxEmployeeID.Text, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value, LeaveType);
			}
			else if (radioButtonExtendLeaveDate.Checked)
			{
				if (LeaveType == "")
				{
					LeaveType = Factory.DatabaseSystem.GetFieldValue("Employee_Leave_Request", "LeaveTypeID", "ActivityID", textBoxLeaveID.Text).ToString();
				}
				dataSet = Factory.EmployeeLeaveDetailSystem.GetEmployeeLeaveAvailability(textBoxEmployeeID.Text, originalenddate.AddDays(1.0), dateTimePickerTransactionDate.Value.AddDays(-1.0), LeaveType);
			}
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
							DataRow[] array = dataSet.Tables[0].Select("LeaveTypeID ='" + comboBoxLeaveType.SelectedID + "'");
							object fieldValue = Factory.DatabaseSystem.GetFieldValue("Employee_Leave_Request", "LeaveTypeID", "ActivityID", textBoxLeaveID.Text);
							if (radioButtonExtendLeaveDate.Checked)
							{
								array = dataSet.Tables[0].Select("LeaveTypeID ='" + fieldValue.ToString() + "'");
							}
							if (array.Length == 0)
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
			textBoxCode.Clear();
			textBoxLeaveID.Clear();
			textBoxEmployeeName.Clear();
			textBoxFromDepartment.Clear();
			textBoxFromDivision.Clear();
			textBoxFromLocation.Clear();
			textBoxDesignation.Clear();
			textBoxNote.Clear();
			radioButtonExtendLeaveDate.Checked = false;
			radioButtonCreateNewLeave.Checked = false;
			textBoxStartDate.Clear();
			textBoxEndDate.Clear();
			textBoxEmployeeID.Clear();
			IsVisible = true;
			textBoxDiffDays.Clear();
			textBoxActualDaysonResume.Clear();
			dateTimePickerTransactionDate.Value = DateTime.Today;
			textBoxReference.Clear();
			textBoxReqDays.Clear();
			activityID = "";
			formManager.ResetDirty();
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
				return Factory.EmployeeActivitySystem.DeleteActivity(textBoxCode.Text, EmployeeActivityTypes.Resumption);
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
			int result = -1;
			int.TryParse(DatabaseHelper.GetNextID("Employee_Resumption", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetPreviousID("Employee_Resumption", "ActivityID", textBoxCode.Text), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetLastID("Employee_Resumption", "ActivityID"), out result);
			if (result > 0)
			{
				LoadData(result);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			int result = -1;
			int.TryParse(DatabaseHelper.GetFirstID("Employee_Resumption", "ActivityID"), out result);
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

		private void EmployeeResumptionForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					panel1.Visible = isNewRecord;
					panel1.Width = 443;
					panel1.Height = 102;
					base.Width = 632;
					base.Height = 478;
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

		private void LoadLeaveData(string leaveID)
		{
			try
			{
				if (leaveID == "")
				{
					textBoxFromLocation.Text = "";
					textBoxFromDivision.Text = "";
					textBoxFromDepartment.Text = "";
					textBoxDesignation.Text = "";
					labelTerminated.Visible = false;
				}
				else
				{
					DataSet employeeLeaveByID = Factory.EmployeeActivitySystem.GetEmployeeLeaveByID(leaveID);
					if (employeeLeaveByID != null && employeeLeaveByID.Tables.Count > 0 && employeeLeaveByID.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = employeeLeaveByID.Tables[0].Rows[0];
						textBoxFromLocation.Text = dataRow["LocationName"].ToString();
						textBoxFromDivision.Text = dataRow["DivisionName"].ToString();
						textBoxFromDepartment.Text = dataRow["DepartmentName"].ToString();
						textBoxDesignation.Text = dataRow["PositionName"].ToString();
						textBoxEmployeeName.Text = dataRow["EmployeeName"].ToString();
						textBoxEmployeeID.Text = dataRow["EmployeeID"].ToString();
						textBoxStartDate.Text = DateTime.Parse(dataRow["StartDate"].ToString()).ToShortDateString();
						textBoxEndDate.Text = DateTime.Parse(dataRow["EndDate"].ToString()).ToShortDateString();
						originalenddate = DateTime.Parse(dataRow["EndDate"].ToString());
						DateTime dateTime = DateTime.Parse(textBoxStartDate.Text);
						endDate = DateTime.Parse(textBoxEndDate.Text);
						dateTimePickerTransactionDate.Value = endDate.AddDays(1.0);
						TimeSpan timeSpan = endDate.Add(TimeSpan.FromDays(1.0)) - dateTime;
						TimeSpan timeSpan2 = endDate.Add(TimeSpan.FromDays(0.0)) - dateTime;
						textBoxReqDays.Text = timeSpan.Days.ToString();
						textBoxActualDaysonResume.Text = dateTimePickerTransactionDate.Value.Subtract(dateTime).Days.ToString();
						textBoxDiffDays.Text = timeSpan2.Days.ToString();
						textBoxActualTaken.Text = dataRow["ActualLeaveDays"].ToString();
						if (textBoxActualTaken.Text == " 0")
						{
							textBoxActualTaken.Text = timeSpan.ToString();
						}
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
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonSelectEmployee_Click(object sender, EventArgs e)
		{
			DataSet employeesLeavesToResume = Factory.EmployeeActivitySystem.GetEmployeesLeavesToResume();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = employeesLeavesToResume;
			selectDocumentDialog.Name = "Employee Leave";
			selectDocumentDialog.Text = "Select Employee";
			selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["EmployeeName"].Header.Caption = "Employee Name";
			selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["LeaveID"].Hidden = true;
			if (selectDocumentDialog.ShowDialog() == DialogResult.OK)
			{
				activityID = selectDocumentDialog.SelectedRow.Cells["LeaveID"].Value.ToString();
				textBoxLeaveID.Text = activityID;
				if (!(activityID == ""))
				{
					LoadLeaveData(activityID);
					textBoxEmployeeName.Text = selectDocumentDialog.SelectedRow.Cells["EmployeeName"].Value.ToString();
				}
			}
		}

		private void mmLabel14_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.EmployeeLeaveResumption);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
		}

		private void radioButtonExtendLeaveDate_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonExtendLeaveDate.Checked && textBoxLeaveID.Text != "")
			{
				textBoxEndDate.Text = dateTimePickerTransactionDate.Value.AddDays(-1.0).ToShortDateString();
			}
			else
			{
				textBoxEndDate.Text = endDate.ToShortDateString();
			}
		}

		private void radioButtonCreateNewLeave_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonCreateNewLeave.Checked && textBoxLeaveID.Text != "")
			{
				groupBoxLeaveRequest.Visible = true;
				panel1.Width = 548;
				panel1.Height = 289;
				base.Width = 687;
				base.Height = 680;
			}
			else
			{
				groupBoxLeaveRequest.Visible = false;
				panel1.Width = 480;
				panel1.Height = 102;
				base.Width = 632;
				base.Height = 478;
			}
		}

		private void radioButtonCreateNewLeave_Click(object sender, EventArgs e)
		{
			if (radioButtonCreateNewLeave.Checked && textBoxLeaveID.Text != "")
			{
				groupBoxLeaveRequest.Visible = true;
				dateTimePickerStartDate.Value = DateTime.Parse(endDate.AddDays(1.0).ToString());
				DateTime.Parse(textBoxStartDate.Text);
				TimeSpan value = dateTimePickerTransactionDate.Value - endDate.AddDays(1.0);
				int.Parse(value.Days.ToString());
				dateTimePickerEndDate.Value = endDate.Add(value);
				dateTimePickerStartDate.Enabled = false;
			}
			else
			{
				groupBoxLeaveRequest.Visible = false;
				dateTimePickerStartDate.Value = DateTime.Now;
				dateTimePickerStartDate.Enabled = true;
			}
		}

		private void radioButtonAsItIs_CheckedChanged(object sender, EventArgs e)
		{
			if (activityID != "" && activityID != null && radioButtonAsItIs.Checked)
			{
				LoadLeaveData(activityID);
			}
		}

		private void textBoxEmployeeID_TextChanged(object sender, EventArgs e)
		{
			DataSet employeeLeaveTypeComboList = Factory.LeaveTypeSystem.GetEmployeeLeaveTypeComboList(textBoxEmployeeID.Text);
			if (employeeLeaveTypeComboList.Tables[0].Rows.Count > 0)
			{
				comboBoxLeaveType.LoadData(isReferesh: false);
				comboBoxLeaveType.DataSource = employeeLeaveTypeComboList;
				comboBoxLeaveType.DataBind();
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (textBoxEmployeeID.Text != "")
			{
				string text = textBoxEmployeeID.Text;
				FormActivator.BringFormToFront(FormActivator.LeaveAvailabilityFormObj);
				FormActivator.LeaveAvailabilityFormObj.LoadData(text, textBoxEmployeeName.Text, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value);
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: false);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeResumptionForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxFromLocation = new Micromind.UISupport.MMTextBox();
			textBoxFromDivision = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxFromDepartment = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxReference = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			label1TransactionID = new Micromind.UISupport.MMLabel();
			textBoxDesignation = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			labelTerminated = new Micromind.UISupport.MMLabel();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			dateTimePickerTransactionDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxLeaveID = new Micromind.UISupport.MMTextBox();
			buttonSelectEmployee = new Micromind.UISupport.XPButton();
			textBoxReqDays = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxEmployeeID = new Micromind.UISupport.MMTextBox();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxStartDate = new Micromind.UISupport.MMTextBox();
			textBoxEndDate = new Micromind.UISupport.MMTextBox();
			textBoxActualDaysonResume = new Micromind.UISupport.MMTextBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			textBoxDiffDays = new Micromind.UISupport.MMTextBox();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			panel1 = new System.Windows.Forms.Panel();
			groupBoxLeaveRequest = new System.Windows.Forms.GroupBox();
			labelLeaveAvaildays = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxDays = new Micromind.UISupport.NumberTextBox();
			comboBoxLeaveType = new Micromind.DataControls.LeaveTypeComboBox();
			mmsDateTimePicker1 = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimePickerEndDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimePickerStartDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmTextBox1 = new Micromind.UISupport.MMTextBox();
			mmTextBox2 = new Micromind.UISupport.MMTextBox();
			textBoxReason = new Micromind.UISupport.MMTextBox();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			groupBox22 = new System.Windows.Forms.GroupBox();
			radioButtonAsItIs = new System.Windows.Forms.RadioButton();
			radioButtonCreateNewLeave = new System.Windows.Forms.RadioButton();
			radioButtonExtendLeaveDate = new System.Windows.Forms.RadioButton();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			textBoxActualTaken = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panel1.SuspendLayout();
			groupBoxLeaveRequest.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLeaveType).BeginInit();
			groupBox22.SuspendLayout();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(659, 31);
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 557);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(659, 40);
			panelButtons.TabIndex = 19;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(659, 1);
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
			xpButton1.Location = new System.Drawing.Point(549, 8);
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
			textBoxFromLocation.Location = new System.Drawing.Point(329, 101);
			textBoxFromLocation.MaxLength = 15;
			textBoxFromLocation.Name = "textBoxFromLocation";
			textBoxFromLocation.ReadOnly = true;
			textBoxFromLocation.Size = new System.Drawing.Size(141, 20);
			textBoxFromLocation.TabIndex = 6;
			textBoxFromLocation.TabStop = false;
			textBoxFromDivision.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDivision.CustomReportFieldName = "";
			textBoxFromDivision.CustomReportKey = "";
			textBoxFromDivision.CustomReportValueType = 1;
			textBoxFromDivision.IsComboTextBox = false;
			textBoxFromDivision.IsModified = false;
			textBoxFromDivision.Location = new System.Drawing.Point(112, 123);
			textBoxFromDivision.MaxLength = 64;
			textBoxFromDivision.Name = "textBoxFromDivision";
			textBoxFromDivision.ReadOnly = true;
			textBoxFromDivision.Size = new System.Drawing.Size(140, 20);
			textBoxFromDivision.TabIndex = 7;
			textBoxFromDivision.TabStop = false;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(258, 102);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(51, 13);
			mmLabel4.TabIndex = 20;
			mmLabel4.Text = "Location:";
			textBoxFromDepartment.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDepartment.CustomReportFieldName = "";
			textBoxFromDepartment.CustomReportKey = "";
			textBoxFromDepartment.CustomReportValueType = 1;
			textBoxFromDepartment.IsComboTextBox = false;
			textBoxFromDepartment.IsModified = false;
			textBoxFromDepartment.Location = new System.Drawing.Point(329, 123);
			textBoxFromDepartment.MaxLength = 255;
			textBoxFromDepartment.Name = "textBoxFromDepartment";
			textBoxFromDepartment.ReadOnly = true;
			textBoxFromDepartment.Size = new System.Drawing.Size(141, 20);
			textBoxFromDepartment.TabIndex = 8;
			textBoxFromDepartment.TabStop = false;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(11, 35);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(46, 13);
			mmLabel2.TabIndex = 21;
			mmLabel2.Text = "Leave:";
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.IsModified = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(112, 79);
			textBoxEmployeeName.MaxLength = 15;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(358, 20);
			textBoxEmployeeName.TabIndex = 4;
			textBoxEmployeeName.TabStop = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(11, 127);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(47, 13);
			mmLabel3.TabIndex = 24;
			mmLabel3.Text = "Division:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(258, 126);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(65, 13);
			mmLabel5.TabIndex = 19;
			mmLabel5.Text = "Department:";
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(260, 205);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(60, 13);
			mmLabel9.TabIndex = 18;
			mmLabel9.Text = "Reference:";
			textBoxReference.BackColor = System.Drawing.Color.White;
			textBoxReference.CustomReportFieldName = "";
			textBoxReference.CustomReportKey = "";
			textBoxReference.CustomReportValueType = 1;
			textBoxReference.IsComboTextBox = false;
			textBoxReference.IsModified = false;
			textBoxReference.Location = new System.Drawing.Point(329, 202);
			textBoxReference.MaxLength = 15;
			textBoxReference.Name = "textBoxReference";
			textBoxReference.Size = new System.Drawing.Size(141, 20);
			textBoxReference.TabIndex = 15;
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(11, 227);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(30, 13);
			mmLabel10.TabIndex = 28;
			mmLabel10.Text = "Note";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(112, 226);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(358, 52);
			textBoxNote.TabIndex = 16;
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(383, 33);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(87, 20);
			textBoxCode.TabIndex = 2;
			textBoxCode.TabStop = false;
			textBoxCode.Visible = false;
			label1TransactionID.AutoSize = true;
			label1TransactionID.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1TransactionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			label1TransactionID.IsFieldHeader = false;
			label1TransactionID.IsRequired = false;
			label1TransactionID.Location = new System.Drawing.Point(297, 35);
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
			textBoxDesignation.Location = new System.Drawing.Point(112, 101);
			textBoxDesignation.MaxLength = 15;
			textBoxDesignation.Name = "textBoxDesignation";
			textBoxDesignation.ReadOnly = true;
			textBoxDesignation.Size = new System.Drawing.Size(140, 20);
			textBoxDesignation.TabIndex = 5;
			textBoxDesignation.TabStop = false;
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(11, 104);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(66, 13);
			mmLabel7.TabIndex = 23;
			mmLabel7.Text = "Designation:";
			labelTerminated.AutoSize = true;
			labelTerminated.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelTerminated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelTerminated.ForeColor = System.Drawing.Color.Red;
			labelTerminated.IsFieldHeader = false;
			labelTerminated.IsRequired = false;
			labelTerminated.Location = new System.Drawing.Point(297, 36);
			labelTerminated.Name = "labelTerminated";
			labelTerminated.PenWidth = 1f;
			labelTerminated.ShowBorder = false;
			labelTerminated.Size = new System.Drawing.Size(152, 13);
			labelTerminated.TabIndex = 1;
			labelTerminated.Text = "Employee is already terminated";
			labelTerminated.Visible = false;
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(11, 173);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(77, 13);
			mmLabel13.TabIndex = 26;
			mmLabel13.Text = "Request Days:";
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = true;
			mmLabel8.Location = new System.Drawing.Point(11, 205);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(87, 13);
			mmLabel8.TabIndex = 27;
			mmLabel8.Text = "Resume Date:";
			dateTimePickerTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerTransactionDate.Location = new System.Drawing.Point(112, 202);
			dateTimePickerTransactionDate.Name = "dateTimePickerTransactionDate";
			dateTimePickerTransactionDate.Size = new System.Drawing.Size(140, 20);
			dateTimePickerTransactionDate.TabIndex = 14;
			dateTimePickerTransactionDate.Value = new System.DateTime(2015, 7, 4, 11, 50, 40, 864);
			dateTimePickerTransactionDate.ValueChanged += new System.EventHandler(dateTimePickerTransactionDate_ValueChanged);
			textBoxLeaveID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLeaveID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLeaveID.CustomReportFieldName = "";
			textBoxLeaveID.CustomReportKey = "";
			textBoxLeaveID.CustomReportValueType = 1;
			textBoxLeaveID.IsComboTextBox = false;
			textBoxLeaveID.IsModified = false;
			textBoxLeaveID.Location = new System.Drawing.Point(112, 33);
			textBoxLeaveID.MaxLength = 15;
			textBoxLeaveID.Name = "textBoxLeaveID";
			textBoxLeaveID.ReadOnly = true;
			textBoxLeaveID.Size = new System.Drawing.Size(140, 20);
			textBoxLeaveID.TabIndex = 0;
			textBoxLeaveID.TabStop = false;
			buttonSelectEmployee.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectEmployee.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectEmployee.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectEmployee.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSelectEmployee.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectEmployee.Location = new System.Drawing.Point(253, 33);
			buttonSelectEmployee.Name = "buttonSelectEmployee";
			buttonSelectEmployee.Size = new System.Drawing.Size(25, 20);
			buttonSelectEmployee.TabIndex = 1;
			buttonSelectEmployee.Text = "...";
			buttonSelectEmployee.UseVisualStyleBackColor = false;
			buttonSelectEmployee.Click += new System.EventHandler(buttonSelectEmployee_Click);
			textBoxReqDays.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxReqDays.CustomReportFieldName = "";
			textBoxReqDays.CustomReportKey = "";
			textBoxReqDays.CustomReportValueType = 1;
			textBoxReqDays.IsComboTextBox = false;
			textBoxReqDays.IsModified = false;
			textBoxReqDays.Location = new System.Drawing.Point(112, 170);
			textBoxReqDays.MaxLength = 64;
			textBoxReqDays.Name = "textBoxReqDays";
			textBoxReqDays.ReadOnly = true;
			textBoxReqDays.Size = new System.Drawing.Size(62, 20);
			textBoxReqDays.TabIndex = 11;
			textBoxReqDays.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(258, 149);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(55, 13);
			mmLabel1.TabIndex = 18;
			mmLabel1.Text = "End Date:";
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(12, 148);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(58, 13);
			mmLabel6.TabIndex = 25;
			mmLabel6.Text = "Start Date:";
			textBoxEmployeeID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeID.CustomReportFieldName = "";
			textBoxEmployeeID.CustomReportKey = "";
			textBoxEmployeeID.CustomReportValueType = 1;
			textBoxEmployeeID.IsComboTextBox = false;
			textBoxEmployeeID.IsModified = false;
			textBoxEmployeeID.Location = new System.Drawing.Point(112, 55);
			textBoxEmployeeID.MaxLength = 15;
			textBoxEmployeeID.Name = "textBoxEmployeeID";
			textBoxEmployeeID.ReadOnly = true;
			textBoxEmployeeID.Size = new System.Drawing.Size(140, 20);
			textBoxEmployeeID.TabIndex = 3;
			textBoxEmployeeID.TabStop = false;
			textBoxEmployeeID.TextChanged += new System.EventHandler(textBoxEmployeeID_TextChanged);
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(9, 58);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(56, 13);
			mmLabel11.TabIndex = 22;
			mmLabel11.Text = "Employee:";
			textBoxStartDate.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxStartDate.CustomReportFieldName = "";
			textBoxStartDate.CustomReportKey = "";
			textBoxStartDate.CustomReportValueType = 1;
			textBoxStartDate.IsComboTextBox = false;
			textBoxStartDate.IsModified = false;
			textBoxStartDate.Location = new System.Drawing.Point(112, 146);
			textBoxStartDate.MaxLength = 64;
			textBoxStartDate.Name = "textBoxStartDate";
			textBoxStartDate.ReadOnly = true;
			textBoxStartDate.Size = new System.Drawing.Size(140, 20);
			textBoxStartDate.TabIndex = 9;
			textBoxStartDate.TabStop = false;
			textBoxEndDate.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEndDate.CustomReportFieldName = "";
			textBoxEndDate.CustomReportKey = "";
			textBoxEndDate.CustomReportValueType = 1;
			textBoxEndDate.IsComboTextBox = false;
			textBoxEndDate.IsModified = false;
			textBoxEndDate.Location = new System.Drawing.Point(329, 146);
			textBoxEndDate.MaxLength = 64;
			textBoxEndDate.Name = "textBoxEndDate";
			textBoxEndDate.ReadOnly = true;
			textBoxEndDate.Size = new System.Drawing.Size(140, 20);
			textBoxEndDate.TabIndex = 10;
			textBoxEndDate.TabStop = false;
			textBoxActualDaysonResume.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxActualDaysonResume.CustomReportFieldName = "";
			textBoxActualDaysonResume.CustomReportKey = "";
			textBoxActualDaysonResume.CustomReportValueType = 1;
			textBoxActualDaysonResume.IsComboTextBox = false;
			textBoxActualDaysonResume.IsModified = false;
			textBoxActualDaysonResume.Location = new System.Drawing.Point(408, 170);
			textBoxActualDaysonResume.MaxLength = 64;
			textBoxActualDaysonResume.Name = "textBoxActualDaysonResume";
			textBoxActualDaysonResume.ReadOnly = true;
			textBoxActualDaysonResume.Size = new System.Drawing.Size(62, 20);
			textBoxActualDaysonResume.TabIndex = 12;
			textBoxActualDaysonResume.TabStop = false;
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(351, 170);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(52, 26);
			mmLabel12.TabIndex = 145;
			mmLabel12.Text = "Days on\r\n Resume:";
			textBoxDiffDays.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDiffDays.CustomReportFieldName = "";
			textBoxDiffDays.CustomReportKey = "";
			textBoxDiffDays.CustomReportValueType = 1;
			textBoxDiffDays.IsComboTextBox = false;
			textBoxDiffDays.IsModified = false;
			textBoxDiffDays.Location = new System.Drawing.Point(585, 258);
			textBoxDiffDays.MaxLength = 64;
			textBoxDiffDays.Name = "textBoxDiffDays";
			textBoxDiffDays.ReadOnly = true;
			textBoxDiffDays.Size = new System.Drawing.Size(65, 20);
			textBoxDiffDays.TabIndex = 13;
			textBoxDiffDays.TabStop = false;
			textBoxDiffDays.Visible = false;
			mmLabel14.AutoSize = true;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(526, 261);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(53, 13);
			mmLabel14.TabIndex = 147;
			mmLabel14.Text = "Diff Days:";
			mmLabel14.Visible = false;
			mmLabel14.Click += new System.EventHandler(mmLabel14_Click);
			panel1.Controls.Add(groupBoxLeaveRequest);
			panel1.Controls.Add(groupBox22);
			panel1.Location = new System.Drawing.Point(108, 285);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(548, 267);
			panel1.TabIndex = 17;
			panel1.TabStop = true;
			groupBoxLeaveRequest.Controls.Add(labelLeaveAvaildays);
			groupBoxLeaveRequest.Controls.Add(ultraFormattedLinkLabel1);
			groupBoxLeaveRequest.Controls.Add(ultraFormattedLinkLabel3);
			groupBoxLeaveRequest.Controls.Add(textBoxDays);
			groupBoxLeaveRequest.Controls.Add(comboBoxLeaveType);
			groupBoxLeaveRequest.Controls.Add(mmsDateTimePicker1);
			groupBoxLeaveRequest.Controls.Add(dateTimePickerEndDate);
			groupBoxLeaveRequest.Controls.Add(dateTimePickerStartDate);
			groupBoxLeaveRequest.Controls.Add(mmTextBox1);
			groupBoxLeaveRequest.Controls.Add(mmTextBox2);
			groupBoxLeaveRequest.Controls.Add(textBoxReason);
			groupBoxLeaveRequest.Controls.Add(mmLabel16);
			groupBoxLeaveRequest.Controls.Add(mmLabel17);
			groupBoxLeaveRequest.Controls.Add(mmLabel18);
			groupBoxLeaveRequest.Controls.Add(mmLabel19);
			groupBoxLeaveRequest.Controls.Add(mmLabel20);
			groupBoxLeaveRequest.Controls.Add(mmLabel21);
			groupBoxLeaveRequest.Controls.Add(mmLabel22);
			groupBoxLeaveRequest.Location = new System.Drawing.Point(6, 78);
			groupBoxLeaveRequest.Name = "groupBoxLeaveRequest";
			groupBoxLeaveRequest.Size = new System.Drawing.Size(536, 184);
			groupBoxLeaveRequest.TabIndex = 1;
			groupBoxLeaveRequest.TabStop = false;
			groupBoxLeaveRequest.Text = "Leave Request";
			groupBoxLeaveRequest.Visible = false;
			labelLeaveAvaildays.AutoSize = true;
			labelLeaveAvaildays.Location = new System.Drawing.Point(109, 164);
			labelLeaveAvaildays.Name = "labelLeaveAvaildays";
			labelLeaveAvaildays.Size = new System.Drawing.Size(15, 13);
			labelLeaveAvaildays.TabIndex = 148;
			labelLeaveAvaildays.Text = "D";
			appearance.FontData.BoldAsString = "False";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 16);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(67, 15);
			ultraFormattedLinkLabel1.TabIndex = 157;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Leave Type :";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance2;
			appearance3.FontData.BoldAsString = "False";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance3;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(10, 163);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(87, 15);
			ultraFormattedLinkLabel3.TabIndex = 149;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Available Leave :";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			textBoxDays.AllowDecimal = false;
			textBoxDays.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDays.CustomReportFieldName = "";
			textBoxDays.CustomReportKey = "";
			textBoxDays.CustomReportValueType = 1;
			textBoxDays.ForeColor = System.Drawing.Color.Black;
			textBoxDays.IsComboTextBox = false;
			textBoxDays.IsModified = false;
			textBoxDays.Location = new System.Drawing.Point(432, 38);
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
			textBoxDays.TabIndex = 5;
			textBoxDays.Text = "0";
			textBoxDays.Visible = false;
			comboBoxLeaveType.Assigned = false;
			comboBoxLeaveType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLeaveType.CustomReportFieldName = "";
			comboBoxLeaveType.CustomReportKey = "";
			comboBoxLeaveType.CustomReportValueType = 1;
			comboBoxLeaveType.DescriptionTextBox = null;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLeaveType.DisplayLayout.Appearance = appearance5;
			comboBoxLeaveType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLeaveType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeaveType.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeaveType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxLeaveType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeaveType.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxLeaveType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLeaveType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLeaveType.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLeaveType.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxLeaveType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLeaveType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLeaveType.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLeaveType.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxLeaveType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLeaveType.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeaveType.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxLeaveType.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxLeaveType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLeaveType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxLeaveType.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxLeaveType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLeaveType.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxLeaveType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLeaveType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLeaveType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLeaveType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLeaveType.Editable = true;
			comboBoxLeaveType.FilterString = "";
			comboBoxLeaveType.HasAllAccount = false;
			comboBoxLeaveType.HasCustom = false;
			comboBoxLeaveType.IsDataLoaded = false;
			comboBoxLeaveType.Location = new System.Drawing.Point(111, 16);
			comboBoxLeaveType.MaxDropDownItems = 12;
			comboBoxLeaveType.Name = "comboBoxLeaveType";
			comboBoxLeaveType.ShowInactiveItems = false;
			comboBoxLeaveType.ShowQuickAdd = true;
			comboBoxLeaveType.Size = new System.Drawing.Size(98, 20);
			comboBoxLeaveType.TabIndex = 0;
			comboBoxLeaveType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmsDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			mmsDateTimePicker1.Location = new System.Drawing.Point(282, 16);
			mmsDateTimePicker1.Name = "mmsDateTimePicker1";
			mmsDateTimePicker1.Size = new System.Drawing.Size(104, 20);
			mmsDateTimePicker1.TabIndex = 1;
			mmsDateTimePicker1.Value = new System.DateTime(2015, 1, 15, 10, 1, 17, 341);
			dateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerEndDate.Location = new System.Drawing.Point(282, 38);
			dateTimePickerEndDate.Name = "dateTimePickerEndDate";
			dateTimePickerEndDate.Size = new System.Drawing.Size(104, 20);
			dateTimePickerEndDate.TabIndex = 4;
			dateTimePickerEndDate.Value = new System.DateTime(2015, 1, 15, 10, 1, 17, 343);
			dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerStartDate.Location = new System.Drawing.Point(111, 38);
			dateTimePickerStartDate.Name = "dateTimePickerStartDate";
			dateTimePickerStartDate.Size = new System.Drawing.Size(98, 20);
			dateTimePickerStartDate.TabIndex = 3;
			dateTimePickerStartDate.Value = new System.DateTime(2015, 1, 15, 10, 1, 17, 344);
			mmTextBox1.BackColor = System.Drawing.Color.White;
			mmTextBox1.CustomReportFieldName = "";
			mmTextBox1.CustomReportKey = "";
			mmTextBox1.CustomReportValueType = 1;
			mmTextBox1.IsComboTextBox = false;
			mmTextBox1.IsModified = false;
			mmTextBox1.Location = new System.Drawing.Point(432, 16);
			mmTextBox1.MaxLength = 15;
			mmTextBox1.Name = "mmTextBox1";
			mmTextBox1.Size = new System.Drawing.Size(90, 20);
			mmTextBox1.TabIndex = 2;
			mmTextBox2.BackColor = System.Drawing.Color.White;
			mmTextBox2.CustomReportFieldName = "";
			mmTextBox2.CustomReportKey = "";
			mmTextBox2.CustomReportValueType = 1;
			mmTextBox2.IsComboTextBox = false;
			mmTextBox2.IsModified = false;
			mmTextBox2.Location = new System.Drawing.Point(111, 82);
			mmTextBox2.MaxLength = 1000;
			mmTextBox2.Multiline = true;
			mmTextBox2.Name = "mmTextBox2";
			mmTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			mmTextBox2.Size = new System.Drawing.Size(411, 77);
			mmTextBox2.TabIndex = 7;
			textBoxReason.BackColor = System.Drawing.Color.White;
			textBoxReason.CustomReportFieldName = "";
			textBoxReason.CustomReportKey = "";
			textBoxReason.CustomReportValueType = 1;
			textBoxReason.IsComboTextBox = false;
			textBoxReason.IsModified = false;
			textBoxReason.Location = new System.Drawing.Point(111, 60);
			textBoxReason.MaxLength = 255;
			textBoxReason.Name = "textBoxReason";
			textBoxReason.Size = new System.Drawing.Size(411, 20);
			textBoxReason.TabIndex = 6;
			mmLabel16.AutoSize = true;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(392, 19);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(27, 13);
			mmLabel16.TabIndex = 151;
			mmLabel16.Text = "Ref:";
			mmLabel17.AutoSize = true;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(12, 83);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(30, 13);
			mmLabel17.TabIndex = 156;
			mmLabel17.Text = "Note";
			mmLabel18.AutoSize = true;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(12, 63);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(47, 13);
			mmLabel18.TabIndex = 155;
			mmLabel18.Text = "Reason:";
			mmLabel19.AutoSize = true;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(392, 41);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(34, 13);
			mmLabel19.TabIndex = 152;
			mmLabel19.Text = "Days:";
			mmLabel19.Visible = false;
			mmLabel20.AutoSize = true;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = true;
			mmLabel20.Location = new System.Drawing.Point(212, 20);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(68, 13);
			mmLabel20.TabIndex = 142;
			mmLabel20.Text = "App. Date:";
			mmLabel21.AutoSize = true;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = true;
			mmLabel21.Location = new System.Drawing.Point(212, 42);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(64, 13);
			mmLabel21.TabIndex = 143;
			mmLabel21.Text = "End Date:";
			mmLabel22.AutoSize = true;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = true;
			mmLabel22.Location = new System.Drawing.Point(12, 42);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(69, 13);
			mmLabel22.TabIndex = 154;
			mmLabel22.Text = "Start Date:";
			groupBox22.Controls.Add(radioButtonAsItIs);
			groupBox22.Controls.Add(radioButtonCreateNewLeave);
			groupBox22.Controls.Add(radioButtonExtendLeaveDate);
			groupBox22.Controls.Add(mmLabel15);
			groupBox22.Location = new System.Drawing.Point(4, 3);
			groupBox22.Name = "groupBox22";
			groupBox22.Size = new System.Drawing.Size(428, 67);
			groupBox22.TabIndex = 0;
			groupBox22.TabStop = false;
			radioButtonAsItIs.AutoSize = true;
			radioButtonAsItIs.Checked = true;
			radioButtonAsItIs.Location = new System.Drawing.Point(35, 42);
			radioButtonAsItIs.Name = "radioButtonAsItIs";
			radioButtonAsItIs.Size = new System.Drawing.Size(75, 17);
			radioButtonAsItIs.TabIndex = 4;
			radioButtonAsItIs.TabStop = true;
			radioButtonAsItIs.Text = "As Original";
			radioButtonAsItIs.UseVisualStyleBackColor = true;
			radioButtonAsItIs.CheckedChanged += new System.EventHandler(radioButtonAsItIs_CheckedChanged);
			radioButtonCreateNewLeave.AutoSize = true;
			radioButtonCreateNewLeave.Location = new System.Drawing.Point(274, 42);
			radioButtonCreateNewLeave.Name = "radioButtonCreateNewLeave";
			radioButtonCreateNewLeave.Size = new System.Drawing.Size(114, 17);
			radioButtonCreateNewLeave.TabIndex = 2;
			radioButtonCreateNewLeave.Text = "Create New Leave";
			radioButtonCreateNewLeave.UseVisualStyleBackColor = true;
			radioButtonCreateNewLeave.CheckedChanged += new System.EventHandler(radioButtonCreateNewLeave_CheckedChanged);
			radioButtonCreateNewLeave.Click += new System.EventHandler(radioButtonCreateNewLeave_Click);
			radioButtonExtendLeaveDate.AutoSize = true;
			radioButtonExtendLeaveDate.Location = new System.Drawing.Point(127, 42);
			radioButtonExtendLeaveDate.Name = "radioButtonExtendLeaveDate";
			radioButtonExtendLeaveDate.Size = new System.Drawing.Size(137, 17);
			radioButtonExtendLeaveDate.TabIndex = 1;
			radioButtonExtendLeaveDate.Text = "Extend Leave End date";
			radioButtonExtendLeaveDate.UseVisualStyleBackColor = true;
			radioButtonExtendLeaveDate.CheckedChanged += new System.EventHandler(radioButtonExtendLeaveDate_CheckedChanged);
			mmLabel15.AutoSize = true;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(8, 14);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(363, 13);
			mmLabel15.TabIndex = 0;
			mmLabel15.Text = "Resume Date is not as Per Approved leave Date, Please choose an action:";
			mmLabel23.AutoSize = true;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(194, 175);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(67, 13);
			mmLabel23.TabIndex = 148;
			mmLabel23.Text = "Actual Days:";
			textBoxActualTaken.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxActualTaken.CustomReportFieldName = "";
			textBoxActualTaken.CustomReportKey = "";
			textBoxActualTaken.CustomReportValueType = 1;
			textBoxActualTaken.IsComboTextBox = false;
			textBoxActualTaken.IsModified = false;
			textBoxActualTaken.Location = new System.Drawing.Point(261, 170);
			textBoxActualTaken.MaxLength = 64;
			textBoxActualTaken.Name = "textBoxActualTaken";
			textBoxActualTaken.ReadOnly = true;
			textBoxActualTaken.Size = new System.Drawing.Size(65, 20);
			textBoxActualTaken.TabIndex = 149;
			textBoxActualTaken.TabStop = false;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 0;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(659, 597);
			base.Controls.Add(textBoxActualTaken);
			base.Controls.Add(mmLabel23);
			base.Controls.Add(panel1);
			base.Controls.Add(textBoxDiffDays);
			base.Controls.Add(mmLabel14);
			base.Controls.Add(textBoxActualDaysonResume);
			base.Controls.Add(mmLabel12);
			base.Controls.Add(textBoxEndDate);
			base.Controls.Add(textBoxStartDate);
			base.Controls.Add(buttonSelectEmployee);
			base.Controls.Add(dateTimePickerTransactionDate);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxReference);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxFromDepartment);
			base.Controls.Add(textBoxReqDays);
			base.Controls.Add(textBoxFromDivision);
			base.Controls.Add(labelTerminated);
			base.Controls.Add(label1TransactionID);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(textBoxEmployeeName);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(textBoxEmployeeID);
			base.Controls.Add(textBoxLeaveID);
			base.Controls.Add(textBoxDesignation);
			base.Controls.Add(textBoxFromLocation);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(mmLabel13);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel11);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(603, 381);
			base.Name = "EmployeeResumptionForm";
			Text = "Employee Duty Resumption Entry";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(EmployeeResumptionForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panel1.ResumeLayout(false);
			groupBoxLeaveRequest.ResumeLayout(false);
			groupBoxLeaveRequest.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLeaveType).EndInit();
			groupBox22.ResumeLayout(false);
			groupBox22.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
