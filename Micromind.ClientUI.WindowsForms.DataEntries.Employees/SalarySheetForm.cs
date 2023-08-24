using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class SalarySheetForm : Form, IForm, IWorkFlowForm
	{
		private bool allowEdit = true;

		private bool isPosted;

		private SalarySheetData currentData;

		private const string TABLENAME_CONST = "SalarySheet";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool IsReCalculate;

		private bool isUpdatingCell;

		private bool IsOT;

		private bool IsDD;

		private int parentrowindx;

		private int nextgridindx;

		private bool isChanged;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool isVoid;

		private DataSet currentgridData;

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

		private FormManager formManager;

		private DataEntryGrid dataGridItems;

		private DateTimePicker dateTimePickerDate;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private MMLabel mmLabel3;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerEndDate;

		private DateTimePicker dateTimePickerStartDate;

		private MonthComboBox comboBoxMonth;

		private MMLabel mmLabel4;

		private Label label4;

		private TextBox textBoxNote;

		private MMLabel mmLabel5;

		private TextBox textBoxDescription;

		private EmployeeComboBox comboBoxGridEmployee;

		private PayrollItemComboBox comboBoxPayrollItemPayment;

		private XPButton buttonCalculate;

		private Label label3;

		private TextBox textBoxTotalDays;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private TextBox textBoxVoucherNumber;

		private EmployeeLoanComboBox comboBoxGridEmployeeLoan;

		private PayrollItemComboBox comboBoxPayrollItemDeduction;

		private ComboBox comboBoxYear;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem saveAsDraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton Preview;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonPrintDetails;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripButton toolStripButtonCreateSIF;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripMenuItem createFromOverTimeToolStripMenuItem;

		private UltraLabel ultraLabel1;

		private UltraLabel labelTotal;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5042;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private bool IsDirty
		{
			get
			{
				if (IsVoid || isPosted)
				{
					return false;
				}
				return formManager.GetDirtyStatus();
			}
		}

		private string SystemDocID => comboBoxSysDoc.SelectedID;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					if (!IsVoid)
					{
						buttonVoid.Enabled = true;
					}
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					bool enabled = textBoxVoucherNumber.Enabled = false;
					sysDocComboBox2.Enabled = enabled;
				}
				toolStripButtonDistribution.Enabled = !value;
				toolStripButtonCreateSIF.Enabled = !value;
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
				if (isVoid == value)
				{
					return;
				}
				isVoid = value;
				bool enabled = !value;
				if (isPosted)
				{
					enabled = false;
				}
				panelDetails.Enabled = enabled;
				dataGridItems.Enabled = enabled;
				buttonSave.Enabled = enabled;
				labelVoided.Visible = value;
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

		public SalarySheetForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			comboBoxPayrollItemDeduction.DropDownStyle = UltraComboStyle.DropDownList;
			comboBoxPayrollItemPayment.DropDownStyle = UltraComboStyle.DropDownList;
			comboBoxYear.SelectedIndex = checked(DateTime.Today.Year - 2000);
			comboBoxYear.DropDownHeight = 100;
			base.FormClosing += SalarySheetForm_FormClosing;
			Global.GlobalSettings.LoadFormProperties(this);
		}

		private void AddEvents()
		{
			base.Load += Form_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxGridEmployee.SelectedIndexChanged += comboBoxGridEmployee_SelectedIndexChanged;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			comboBoxMonth.SelectedIndexChanged += comboBoxMonth_SelectedIndexChanged;
			comboBoxYear.SelectedIndexChanged += comboBoxYear_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dateTimePickerStartDate.ValueChanged += dateTimePickerStartDate_ValueChanged;
			dateTimePickerEndDate.ValueChanged += dateTimePickerStartDate_ValueChanged;
			base.FormClosing += Form_FormClosing;
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

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				if (isNewRecord)
				{
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
			}
		}

		private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
		{
			TimeSpan timeSpan = default(TimeSpan);
			int num = checked((dateTimePickerEndDate.Value - dateTimePickerStartDate.Value).Days + 1);
			textBoxTotalDays.Text = num.ToString();
			_ = string.Empty;
		}

		private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (IsNewRecord && comboBoxMonth.SelectedID >= 1)
			{
				SetBeginEndDate();
			}
		}

		private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (IsNewRecord && comboBoxMonth.SelectedID >= 1)
			{
				SetBeginEndDate();
			}
		}

		private void SetBeginEndDate()
		{
			DateTime value = new DateTime(int.Parse(comboBoxYear.Text), comboBoxMonth.SelectedID, 1);
			dateTimePickerStartDate.Value = value;
			value = value.AddMonths(1).AddDays(-1.0);
			dateTimePickerEndDate.Value = value;
		}

		private void SetTransactionDate()
		{
			DateTime value = new DateTime(int.Parse(comboBoxYear.Text), comboBoxMonth.SelectedID, int.Parse(textBoxTotalDays.Text));
			dateTimePickerDate.Value = value;
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
		}

		private void dataGridItems_GotFocus(object sender, EventArgs e)
		{
			if (!dataGridItems.Focused)
			{
				dataGridItems.DisplayLayout.Bands[0].AddNew();
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (isUpdatingCell)
				{
					return;
				}
				isUpdatingCell = true;
				if (dataGridItems.ActiveRow == null)
				{
					isUpdatingCell = false;
					return;
				}
				if (e.Cell.Column.Key == "AnnualLeaves" || e.Cell.Column.Key == "Absent" || e.Cell.Column.Key == "OTHours")
				{
					ReCalculateSalary(allRows: false);
				}
				if (e.Cell.Column.Band.Index == 1 && dataGridItems.ActiveRow.Band.Index == 1)
				{
					int num = 1;
					if (dataGridItems.ActiveRow.Cells["PayType"].Value != null && dataGridItems.ActiveRow.Cells["PayType"].Value.ToString() != "")
					{
						num = int.Parse(dataGridItems.ActiveRow.Cells["PayType"].Value.ToString());
					}
					decimal result = default(decimal);
					if (e.Cell.Column.Key == "Amount")
					{
						decimal.TryParse(e.Cell.Value.ToString(), out result);
						if (num == 1 && result < 0m)
						{
							e.Cell.Value = Math.Abs(Math.Round(result, Global.CurDecimalPoints));
						}
						else if (num == 2 && result > 0m)
						{
							e.Cell.Value = -1m * Math.Abs(Math.Round(result, Global.CurDecimalPoints));
						}
						else if (num == 3 && result > 0m)
						{
							e.Cell.Value = -1m * Math.Abs(Math.Round(result, Global.CurDecimalPoints));
						}
						if (e.Cell.Value == null || e.Cell.Value.ToString() == "")
						{
							e.Cell.Value = 0.ToString(Format.TotalAmountFormat);
						}
						ReCalculateSalary(allRows: false);
					}
					else if (e.Cell.Column.Key == "PayType")
					{
						switch (num)
						{
						case 1:
							dataGridItems.DisplayLayout.Bands[1].Columns["PayrollItemID"].ValueList = comboBoxPayrollItemPayment;
							break;
						case 2:
							dataGridItems.DisplayLayout.Bands[1].Columns["PayrollItemID"].ValueList = comboBoxPayrollItemDeduction;
							break;
						case 3:
							dataGridItems.DisplayLayout.Bands[1].Columns["PayrollItemID"].ValueList = comboBoxGridEmployeeLoan;
							break;
						}
						dataGridItems.ActiveRow.Cells["PayrollItemID"].Value = null;
					}
					else if (e.Cell.Column.Key == "PayrollItemID")
					{
						switch (num)
						{
						case 1:
							if (comboBoxPayrollItemPayment.SelectedID != "")
							{
								dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxPayrollItemPayment.SelectedName;
								dataGridItems.ActiveRow.Cells["InDeduction"].Value = comboBoxPayrollItemPayment.SelectedRow.Cells["InDeduction"].Value;
								dataGridItems.ActiveRow.Cells["PayCodeType"].Value = comboBoxPayrollItemPayment.SelectedRow.Cells["PayCodeType"].Value;
							}
							break;
						case 2:
							if (comboBoxPayrollItemDeduction.SelectedID != "")
							{
								dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxPayrollItemDeduction.SelectedName;
								dataGridItems.ActiveRow.Cells["InDeduction"].Value = comboBoxPayrollItemDeduction.SelectedRow.Cells["InDeduction"].Value;
								dataGridItems.ActiveRow.Cells["PayCodeType"].Value = comboBoxPayrollItemDeduction.SelectedRow.Cells["PayCodeType"].Value;
							}
							break;
						default:
							if (comboBoxGridEmployeeLoan.SelectedID != "")
							{
								dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridEmployeeLoan.SelectedName;
								dataGridItems.ActiveRow.Cells["InDeduction"].Value = false;
								dataGridItems.ActiveRow.Cells["LoanSysDocID"].Value = comboBoxGridEmployeeLoan.SelectedSysDocID;
							}
							break;
						}
					}
					ReCalculateSalary(allRows: false);
				}
				isUpdatingCell = false;
			}
			catch (Exception e2)
			{
				isUpdatingCell = false;
				ErrorHelper.ProcessError(e2);
			}
			_ = dataGridItems.ActiveRow;
		}

		private void ReCalculateSalary(bool allRows)
		{
			if (allRows)
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					ReCalculateSalary(row);
				}
			}
			else if (dataGridItems.ActiveRow != null)
			{
				ReCalculateSalary(dataGridItems.ActiveRow);
			}
		}

		private void ReCalculateSalary(UltraGridRow parentRow)
		{
			try
			{
				if (parentRow == null)
				{
					return;
				}
				int num = int.Parse(textBoxTotalDays.Text);
				decimal num2 = default(decimal);
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal num3 = default(decimal);
				decimal num4 = default(decimal);
				decimal num5 = default(decimal);
				decimal num6 = default(decimal);
				decimal num7 = default(decimal);
				decimal result3 = default(decimal);
				DateTime result4 = new DateTime(1, 1, 1);
				DateTime result5 = new DateTime(1, 1, 1);
				DateTime d = new DateTime(1, 1, 1);
				int totalWorkingMonthHours = CompanyPreferences.TotalWorkingMonthHours;
				if (parentRow.Band.Index == 1)
				{
					parentRow = parentRow.ParentRow;
				}
				decimal d2 = Convert.ToInt32(parentRow.Cells["WorkDays"].Value);
				if (parentRow.Cells["absent"].Value != null && parentRow.Cells["absent"].Value.ToString() != "")
				{
					decimal.TryParse(parentRow.Cells["absent"].Value.ToString(), out result);
				}
				if (parentRow.Cells["AnnualLeaves"].Value != null && parentRow.Cells["AnnualLeaves"].Value.ToString() != "")
				{
					decimal.TryParse(parentRow.Cells["AnnualLeaves"].Value.ToString(), out result2);
				}
				num2 = d2 - result - result2;
				if (CompanyPreferences.ThirtyDays && num > 30)
				{
					num2 -= 1m;
				}
				parentRow.Cells["NetDays"].Value = num2;
				decimal.TryParse(parentRow.Cells["OTBase"].Value.ToString(), out result3);
				DateTime.TryParse(parentRow.Cells["JoiningDate"].Value.ToString(), out result4);
				DateTime.TryParse(parentRow.Cells["ResumedDate"].Value.ToString(), out result5);
				bool flag = false;
				if (result4 > dateTimePickerStartDate.Value && result4 < dateTimePickerEndDate.Value)
				{
					flag = true;
					d = result4;
				}
				else if (result5 > dateTimePickerStartDate.Value && result5 < dateTimePickerEndDate.Value)
				{
					flag = true;
					d = result5;
				}
				else
				{
					flag = false;
				}
				foreach (UltraGridRow row in parentRow.ChildBands[0].Rows)
				{
					parentrowindx = parentRow.Index;
					int result6;
					int result7;
					decimal result8;
					bool result9;
					decimal result10;
					decimal num8;
					checked
					{
						if (parentrowindx >= 0 && parentrowindx == nextgridindx)
						{
							nextgridindx++;
							IsOT = false;
							IsDD = false;
						}
						result6 = 1;
						int.TryParse(row.Cells["PayType"].Value.ToString(), out result6);
						result7 = 1;
						int.TryParse(row.Cells["PayCodeType"].Value.ToString(), out result7);
						result8 = default(decimal);
						decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result8);
						result9 = true;
						bool.TryParse(row.Cells["InDeduction"].Value.ToString(), out result9);
						result10 = default(decimal);
						num8 = default(decimal);
						decimal num9 = default(decimal);
						decimal num10 = default(decimal);
						TimeSpan zero = TimeSpan.Zero;
						double num11 = 0.0;
					}
					switch (result6)
					{
					case 1:
						switch (result7)
						{
						case 1:
							if (result9)
							{
								if ((decimal)num * num2 > 0m)
								{
									if (CompanyPreferences.BasedonNetDays)
									{
										if (CompanyPreferences.Annual && (decimal)num != num2)
										{
											num8 = result8 * 12m / 365m * num2;
										}
										else if (!CompanyPreferences.ThirtyDays || !((decimal)num != num2))
										{
											num8 = ((CompanyPreferences.DaysInMonth && (decimal)num != num2) ? (result8 / (decimal)num * num2) : (((!CompanyPreferences.Annual && !CompanyPreferences.DaysInMonth && !CompanyPreferences.ThirtyDays) || !((decimal)num == num2)) ? (result8 / (decimal)num * num2) : result8));
										}
										else
										{
											num8 = result8 / 30m * num2;
											if (num < 30)
											{
												decimal num9 = result8 / 30m;
												decimal num10 = (decimal)num - num2;
												num9 *= num10;
												num8 = result8 - num9;
											}
										}
									}
									else if (!CompanyPreferences.BasedonNetDays)
									{
										if (CompanyPreferences.Annual && (decimal)num != num2 && !flag)
										{
											decimal num9 = result8 * 12m / 365m;
											decimal num10 = (decimal)num - num2;
											num9 *= num10;
											num8 = result8 - num9;
										}
										else if ((CompanyPreferences.Annual && (decimal)num != num2) & flag)
										{
											decimal num9 = result8 * 12m / 365m;
											num2 = decimal.Parse(((dateTimePickerEndDate.Value - d).TotalDays + 1.0).ToString());
											decimal num10 = num2;
											num9 *= num10;
											num8 = num9;
										}
										else if (CompanyPreferences.ThirtyDays && (decimal)num != num2 && !flag)
										{
											decimal num9 = result8 / 30m;
											decimal num10 = (decimal)num - num2;
											num9 *= num10;
											num8 = result8 - num9;
										}
										else if ((CompanyPreferences.ThirtyDays && (decimal)num != num2) & flag)
										{
											decimal num9 = result8 / 30m;
											num2 = decimal.Parse(((dateTimePickerEndDate.Value - d).TotalDays + 1.0).ToString());
											decimal num10 = num2;
											num9 *= num10;
											num8 = num9;
										}
										else if (CompanyPreferences.DaysInMonth && (decimal)num != num2 && !flag)
										{
											decimal num9 = result8 / (decimal)num;
											decimal num10 = (decimal)num - num2;
											num9 *= num10;
											num8 = result8 - num9;
										}
										else if (!((CompanyPreferences.DaysInMonth && (decimal)num != num2) & flag))
										{
											num8 = (((!CompanyPreferences.Annual && !CompanyPreferences.DaysInMonth && !CompanyPreferences.ThirtyDays) || !((decimal)num == num2)) ? (result8 / (decimal)num * num2) : result8);
										}
										else
										{
											decimal num9 = result8 / (decimal)num;
											num2 = decimal.Parse(((dateTimePickerEndDate.Value - d).TotalDays + 1.0).ToString());
											decimal num10 = num2;
											num9 *= num10;
											num8 = num9;
										}
									}
								}
							}
							else
							{
								num8 = result8;
							}
							num3 += num8;
							row.Cells["PayableAmount"].Value = Math.Round(num8, Global.CurDecimalPoints);
							break;
						case 6:
						{
							decimal.TryParse(row.Cells["OTHiddenAmount"].Value.ToString(), out result10);
							decimal result11 = default(decimal);
							decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result11);
							if (CompanyPreferences.Annual)
							{
								num8 = ((!IsNewRecord) ? result10 : (result10 * (result3 * 12m / 365m / (decimal)(totalWorkingMonthHours / 30))));
								if (IsReCalculate)
								{
									num8 = result10 * (result3 * 12m / 365m / (decimal)(totalWorkingMonthHours / 30));
								}
							}
							else
							{
								num8 = ((!IsNewRecord) ? result10 : (result10 * (result3 / (decimal)totalWorkingMonthHours)));
								if (IsReCalculate)
								{
									num8 = result10 * (result3 / (decimal)totalWorkingMonthHours);
								}
								if (isChanged)
								{
									num8 = result10 * (result3 / (decimal)totalWorkingMonthHours);
								}
							}
							if (!IsOT)
							{
								num7 += num8;
								object obj3 = row.Cells["PayableAmount"].Value = (row.Cells["Amount"].Value = Math.Round(num8, Global.CurDecimalPoints));
								parentRow.Cells["OTAmount"].Value = Math.Round(num7, Global.CurDecimalPoints);
							}
							else
							{
								row.Cells["PayableAmount"].Value = row.Cells["Amount"].Value;
								parentRow.Cells["OTAmount"].Value = Math.Round(double.Parse(row.Cells["Amount"].Value.ToString()), Global.CurDecimalPoints);
							}
							break;
						}
						case 7:
							num8 = result8;
							num4 += num8;
							row.Cells["PayableAmount"].Value = Math.Round(num8, Global.CurDecimalPoints);
							break;
						case 2:
							if (result9)
							{
								if ((decimal)num * num2 > 0m)
								{
									if (CompanyPreferences.BasedonNetDays)
									{
										if (CompanyPreferences.Annual && (decimal)num != num2)
										{
											num8 = result8 * 12m / 365m * num2;
										}
										else if (CompanyPreferences.DaysInMonth && (decimal)num != num2)
										{
											num8 = result8 / (decimal)num * num2;
										}
										else if (!CompanyPreferences.ThirtyDays || !((decimal)num != num2))
										{
											num8 = (((!CompanyPreferences.Annual && !CompanyPreferences.DaysInMonth && !CompanyPreferences.ThirtyDays) || !((decimal)num == num2)) ? (result8 / (decimal)num * num2) : result8);
										}
										else
										{
											num8 = result8 / 30m * num2;
											if (num < 30)
											{
												decimal num9 = result8 / 30m;
												decimal num10 = (decimal)num - num2;
												num9 *= num10;
												num8 = result8 - num9;
											}
										}
									}
									else if (!CompanyPreferences.BasedonNetDays)
									{
										if (CompanyPreferences.Annual && (decimal)num != num2 && !flag)
										{
											decimal num9 = result8 * 12m / 365m;
											decimal num10 = (decimal)num - num2;
											num9 *= num10;
											num8 = result8 - num9;
										}
										else if ((CompanyPreferences.Annual && (decimal)num != num2) & flag)
										{
											decimal num9 = result8 * 12m / 365m;
											num2 = decimal.Parse(((dateTimePickerEndDate.Value - d).TotalDays + 1.0).ToString());
											decimal num10 = num2;
											num9 *= num10;
											num8 = num9;
										}
										else if (CompanyPreferences.ThirtyDays && (decimal)num != num2 && !flag)
										{
											decimal num9 = result8 / 30m;
											decimal num10 = (decimal)num - num2;
											num9 *= num10;
											num8 = result8 - num9;
										}
										else if ((CompanyPreferences.ThirtyDays && (decimal)num != num2) & flag)
										{
											decimal num9 = result8 / 30m;
											num2 = decimal.Parse(((dateTimePickerEndDate.Value - d).TotalDays + 1.0).ToString());
											decimal num10 = num2;
											num9 *= num10;
											num8 = num9;
										}
										else if (CompanyPreferences.DaysInMonth && (decimal)num != num2 && !flag)
										{
											decimal num9 = result8 / (decimal)num;
											decimal num10 = (decimal)num - num2;
											num9 *= num10;
											num8 = result8 - num9;
										}
										else if (!((CompanyPreferences.DaysInMonth && (decimal)num != num2) & flag))
										{
											num8 = (((!CompanyPreferences.Annual && !CompanyPreferences.DaysInMonth && !CompanyPreferences.ThirtyDays) || !((decimal)num == num2)) ? (result8 / (decimal)num * num2) : result8);
										}
										else
										{
											decimal num9 = result8 / (decimal)num;
											num2 = decimal.Parse(((dateTimePickerEndDate.Value - d).TotalDays + 1.0).ToString());
											decimal num10 = num2;
											num9 *= num10;
											num8 = num9;
										}
									}
								}
							}
							else
							{
								num8 = result8;
							}
							num8 = Math.Round(num8, Global.CurDecimalPoints);
							num4 += num8;
							row.Cells["PayableAmount"].Value = num8;
							break;
						default:
							ErrorHelper.WarningMessage("Unreccogonized payment code type:", result7.ToString());
							break;
						case 0:
							break;
						}
						break;
					case 2:
						num5 += result8;
						if (!IsDD)
						{
							row.Cells["Amount"].Value = Math.Round(result8, Global.CurDecimalPoints);
							row.Cells["PayableAmount"].Value = Math.Round(result8, Global.CurDecimalPoints);
						}
						break;
					case 3:
						num6 += result8;
						row.Cells["PayableAmount"].Value = Math.Round(result8, Global.CurDecimalPoints);
						break;
					}
				}
				parentRow.Cells["Basic"].Value = num3;
				parentRow.Cells["Allowance"].Value = num4;
				parentRow.Cells["Deduction"].Value = num5;
				parentRow.Cells["LoanDeduction"].Value = num6;
				decimal d3 = decimal.Parse(parentRow.Cells["OTAmount"].Value.ToString());
				parentRow.Cells["OTAmount"].Value = num7;
				if (d3 != 0m)
				{
					IsOT = true;
				}
				decimal num12 = Math.Round(num3, Global.CurDecimalPoints) + Math.Round(num4, Global.CurDecimalPoints) + Math.Round(num7, Global.CurDecimalPoints);
				decimal num13 = default(decimal);
				parentRow.Cells["GrossSalary"].Value = num12;
				num13 = num12 + num5 + num6;
				parentRow.Cells["NetSalary"].Value = num13;
				decimal num14 = default(decimal);
				num14 = Math.Round(num13, 0) - Math.Round(num13, Global.CurDecimalPoints);
				if (CompanyPreferences.Roundoffsalary)
				{
					if (Global.CurDecimalPoints == 2)
					{
						parentRow.Cells["NetSalary"].Value = Math.Round(num13, 0);
					}
					else if (Global.CurDecimalPoints == 3)
					{
						parentRow.Cells["NetSalary"].Value = num13;
						parentRow.Cells["NetSalary"].Value = Math.Round(num13 * 10m, 0) / 10m;
						num14 = Math.Round(num13 * 10m, 0) / 10m - Math.Round(num13, Global.CurDecimalPoints);
					}
				}
				else if (!CompanyPreferences.Roundoffsalary)
				{
					parentRow.Cells["NetSalary"].Value = Math.Round(num13, Global.CurDecimalPoints);
				}
				int num15 = 0;
				foreach (UltraGridRow row2 in parentRow.ChildBands[0].Rows)
				{
					decimal result12 = default(decimal);
					if (num15 == 0 && CompanyPreferences.Roundoffsalary)
					{
						decimal.TryParse(row2.Cells["PayableAmount"].Value.ToString(), out result12);
						if ((int)decimal.Truncate(result12) == 0)
						{
							continue;
						}
						result12 += num14;
						row2.Cells["PayableAmount"].Value = result12;
					}
					num15 = checked(num15 + 1);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			IsOT = false;
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			IsReCalculate = false;
			isChanged = true;
			ReCalculateSalary(allRows: true);
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow.Band.Index == 1)
			{
				int num = 1;
				if (dataGridItems.ActiveRow.Cells["PayType"].Value != null && dataGridItems.ActiveRow.Cells["PayType"].Value.ToString() != "")
				{
					num = int.Parse(dataGridItems.ActiveRow.Cells["PayType"].Value.ToString());
				}
				switch (num)
				{
				case 1:
					dataGridItems.DisplayLayout.Bands[1].Columns["PayrollItemID"].ValueList = comboBoxPayrollItemPayment;
					break;
				case 2:
					dataGridItems.DisplayLayout.Bands[1].Columns["PayrollItemID"].ValueList = comboBoxPayrollItemDeduction;
					break;
				case 3:
					dataGridItems.DisplayLayout.Bands[1].Columns["PayrollItemID"].ValueList = comboBoxGridEmployeeLoan;
					comboBoxGridEmployeeLoan.FilterID = dataGridItems.ActiveRow.ParentRow.Cells["EmployeeID"].Value.ToString();
					break;
				}
			}
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			comboBoxGridEmployee.IsLoadingData = true;
		}

		private void comboBoxGridEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell.Band.Index == 1 && dataGridItems.ActiveCell.Column.Key == "Amount" && (dataGridItems.ActiveCell.Value == null || dataGridItems.ActiveCell.Value.ToString() == ""))
			{
				dataGridItems.ActiveCell.Value = 0.ToString(Format.TotalAmountFormat);
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && dataGridItems.HasRowAnyValue(activeRow) && activeRow.Band.Index == 1)
			{
				if (activeRow.Cells["PayType"].Value.IsDBNullOrEmpty() && activeRow.Cells["PayrollItemID"].Value.IsNullOrEmpty() && activeRow.Cells["Amount"].Value.IsNullOrEmpty())
				{
					activeRow.Delete(displayPrompt: false);
				}
				else if (activeRow.Cells["PayType"].Value == null || activeRow.Cells["PayType"].Value.ToString() == "" || activeRow.Cells["PayrollItemID"].Value == null || activeRow.Cells["PayrollItemID"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select payment type and payroll item.");
					e.Cancel = true;
				}
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Band.Index != 0)
			{
				return;
			}
			if (e.Cell.Column.Key == "Absent")
			{
				decimal result = default(decimal);
				if (!decimal.TryParse(e.NewValue.ToString(), out result))
				{
					ErrorHelper.InformationMessage("Please enter a numeric value greater than zero.");
					e.Cancel = true;
				}
				if (result < 0m)
				{
					ErrorHelper.InformationMessage("Please enter a numeric value greater than zero.");
					e.Cancel = true;
				}
			}
			else if (e.Cell.Column.Key == "Amount" && (dataGridItems.ActiveRow.Cells["PayType"].Value == null || dataGridItems.ActiveRow.Cells["PayType"].Value.ToString() == "" || dataGridItems.ActiveRow.Cells["PayrollItemID"].Value == null || dataGridItems.ActiveRow.Cells["PayrollItemID"].Value.ToString() == ""))
			{
				ErrorHelper.InformationMessage("Please select the payment type and payroll item.");
				e.Cancel = true;
			}
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			UltraGridCell activeCell = dataGridItems.ActiveCell;
			if (activeCell.Band.Index == 0)
			{
				if (activeCell.Column.Key == "Absent")
				{
					ErrorHelper.InformationMessage("Please enter a numeric value equal or greater than zero.");
					e.RestoreOriginalValue = true;
					e.RaiseErrorEvent = false;
				}
			}
			else if (activeCell.Band.Index == 1 && activeCell.Column.Key == "Amount")
			{
				ErrorHelper.InformationMessage("Please enter a numeric value equal or greater than zero.");
				e.RestoreOriginalValue = true;
				e.RaiseErrorEvent = false;
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new SalarySheetData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.SalarySheetTable.Rows[0] : currentData.SalarySheetTable.NewRow();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["StartDate"] = dateTimePickerStartDate.Value;
				dataRow["EndDate"] = dateTimePickerEndDate.Value;
				dataRow["Month"] = comboBoxMonth.SelectedID;
				dataRow["Year"] = comboBoxYear.Text;
				dataRow["SheetName"] = textBoxDescription.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Reference"] = textBoxRef.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.SalarySheetTable.Rows.Add(dataRow);
				}
				currentData.SalarySheetDetailItemsTable.Rows.Clear();
				currentData.SalarySheetDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.SalarySheetDetailTable.NewRow();
					dataRow2.BeginEdit();
					string value = (string)(dataRow2["EmployeeID"] = row.Cells["EmployeeID"].Value.ToString());
					dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow2["WorkDays"] = row.Cells["WorkDays"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
					dataRow2["EmployeeName"] = row.Cells["EmployeeName"].Value.ToString();
					dataRow2["NetDays"] = row.Cells["NetDays"].Value.ToString();
					dataRow2["GrossSalary"] = row.Cells["GrossSalary"].Value.ToString();
					dataRow2["Absent"] = row.Cells["Absent"].Value.ToString();
					dataRow2["AnnualLeaves"] = row.Cells["AnnualLeaves"].Value.ToString();
					if (!string.IsNullOrEmpty(row.Cells["UnpaidAnnualLeaves"].Value.ToString()))
					{
						dataRow2["UnpaidAnnualLeaves"] = row.Cells["UnpaidAnnualLeaves"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["NormalLeaves"].Value.ToString()))
					{
						dataRow2["NormalLeaves"] = row.Cells["NormalLeaves"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["SickLeaves"].Value.ToString()))
					{
						dataRow2["NormalLeaves"] = row.Cells["SickLeaves"].Value.ToString();
					}
					dataRow2["Basic"] = row.Cells["Basic"].Value.ToString();
					dataRow2["Allowance"] = row.Cells["Allowance"].Value.ToString();
					dataRow2["OTHours"] = row.Cells["OTHours"].Value.ToString();
					dataRow2["OTBase"] = row.Cells["OTBase"].Value.ToString();
					dataRow2["OTAmount"] = row.Cells["OTAmount"].Value.ToString();
					dataRow2["Deduction"] = row.Cells["Deduction"].Value.ToString();
					dataRow2["LoanDeduction"] = row.Cells["LoanDeduction"].Value.ToString();
					dataRow2["NetSalary"] = row.Cells["NetSalary"].Value.ToString();
					if (row.Cells["OTIsFixed"].Value != null && row.Cells["OTIsFixed"].Value.ToString() != "")
					{
						dataRow2["OTIsFixed"] = row.Cells["OTIsFixed"].Value.ToString();
					}
					else
					{
						dataRow2["OTIsFixed"] = true;
					}
					if (row.Cells["OTFixedAmount"].Value != null && row.Cells["OTFixedAmount"].Value.ToString() != "")
					{
						dataRow2["OTFixedAmount"] = row.Cells["OTFixedAmount"].Value.ToString();
					}
					else
					{
						dataRow2["OTFixedAmount"] = 0;
					}
					dataRow2.EndEdit();
					currentData.SalarySheetDetailTable.Rows.Add(dataRow2);
					foreach (UltraGridRow row2 in row.ChildBands[0].Rows)
					{
						DataRow dataRow3 = currentData.SalarySheetDetailItemsTable.NewRow();
						dataRow3.BeginEdit();
						dataRow3["SysDocID"] = comboBoxSysDoc.SelectedID;
						dataRow3["VoucherID"] = textBoxVoucherNumber.Text;
						dataRow3["EmployeeID"] = value;
						dataRow3["RowIndex"] = row2.Index;
						dataRow3["PayType"] = row2.Cells["PayType"].Value.ToString();
						dataRow3["PayrollItemID"] = row2.Cells["PayrollItemID"].Value.ToString();
						if (row2.Cells["LoanSysDocID"].Value != null && row2.Cells["LoanSysDocID"].Value.ToString() != "")
						{
							dataRow3["LoanSysDocID"] = row2.Cells["LoanSysDocID"].Value.ToString();
						}
						dataRow3["Description"] = row2.Cells["Description"].Value.ToString();
						dataRow3["Amount"] = row2.Cells["Amount"].Value.ToString();
						dataRow3["PayableAmount"] = row2.Cells["PayableAmount"].Value.ToString();
						if (row2.Cells["InDeduction"].Value != null && row2.Cells["InDeduction"].Value.ToString() != "")
						{
							dataRow3["InDeduction"] = row2.Cells["InDeduction"].Value.ToString();
						}
						else
						{
							dataRow3["InDeduction"] = true;
						}
						if (row2.Cells["PayCodeType"].Value != null && row2.Cells["PayCodeType"].Value.ToString() != "")
						{
							dataRow3["PayCodeType"] = row2.Cells["PayCodeType"].Value.ToString();
						}
						else
						{
							dataRow3["PayCodeType"] = 1;
						}
						if (row2.Cells["IsFixed"].Value != null && row2.Cells["IsFixed"].Value.ToString() != "")
						{
							dataRow3["IsFixed"] = row2.Cells["IsFixed"].Value.ToString();
						}
						else
						{
							dataRow3["IsFixed"] = false;
						}
						dataRow3.EndEdit();
						currentData.SalarySheetDetailItemsTable.Rows.Add(dataRow3);
					}
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
				DataSet dataSet = new DataSet();
				dataGridItems.Clear();
				dataGridItems.DataSource = null;
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable("SalarySheet_Detail");
				dataTable.Columns.Add("Sl.No");
				dataTable.Columns.Add("ResumedDate", typeof(DateTime));
				dataTable.Columns.Add("EmployeeID");
				dataTable.Columns.Add("EmployeeName");
				dataTable.Columns.Add("JoiningDate", typeof(DateTime));
				dataTable.Columns.Add("Gender");
				dataTable.Columns.Add("Sponsor");
				dataTable.Columns.Add("Class");
				dataTable.Columns.Add("Group");
				dataTable.Columns.Add("Position");
				dataTable.Columns.Add("Division");
				dataTable.Columns.Add("LabourID");
				dataTable.Columns.Add("IBANNo");
				dataTable.Columns.Add("Bank");
				dataTable.Columns.Add("TransferType");
				dataTable.Columns.Add("Nationality");
				dataTable.Columns.Add("WorkDays", typeof(decimal));
				dataTable.Columns.Add("AnnualLeaves", typeof(decimal));
				dataTable.Columns.Add("UnpaidAnnualLeaves", typeof(decimal));
				dataTable.Columns.Add("NormalLeaves", typeof(decimal));
				dataTable.Columns.Add("SickLeaves", typeof(decimal));
				dataTable.Columns.Add("Absent", typeof(decimal));
				dataTable.Columns.Add("NetDays", typeof(decimal));
				dataTable.Columns.Add("Basic", typeof(decimal));
				dataTable.Columns.Add("Allowance", typeof(decimal));
				dataTable.Columns.Add("OTHours", typeof(decimal));
				dataTable.Columns.Add("OTRate", typeof(decimal));
				dataTable.Columns.Add("OTFactor", typeof(decimal));
				dataTable.Columns.Add("OTFixedAmount", typeof(decimal));
				dataTable.Columns.Add("OTIsFixed", typeof(bool));
				dataTable.Columns.Add("OTAmount", typeof(decimal));
				dataTable.Columns.Add("OTBase", typeof(decimal));
				dataTable.Columns.Add("GrossSalary", typeof(decimal));
				dataTable.Columns.Add("Deduction", typeof(decimal));
				dataTable.Columns.Add("LoanDeduction", typeof(decimal));
				dataTable.Columns.Add("NetSalary", typeof(decimal));
				dataTable.Columns.Add("LastMonthSalary", typeof(decimal));
				dataTable.Columns.Add("Remarks");
				dataSet.Tables.Add(dataTable);
				DataTable dataTable2 = new DataTable("SalarySheet_Detail_Item");
				dataTable2.Columns.Add("EmployeeID");
				dataTable2.Columns.Add("PayType", typeof(byte));
				dataTable2.Columns.Add("PayrollItemID");
				dataTable2.Columns.Add("LoanSysDocID");
				dataTable2.Columns.Add("Description");
				dataTable2.Columns.Add("IsFixed", typeof(bool));
				dataTable2.Columns.Add("InDeduction", typeof(bool));
				dataTable2.Columns.Add("PayCodeType", typeof(byte));
				dataTable2.Columns.Add("Amount", typeof(decimal));
				dataTable2.Columns.Add("PayableAmount", typeof(decimal));
				dataTable2.Columns.Add("OTHiddenAmount", typeof(decimal));
				dataSet.Tables.Add(dataTable2);
				dataSet.Relations.Add("REL", dataSet.Tables["SalarySheet_Detail"].Columns["EmployeeID"], dataSet.Tables["SalarySheet_Detail_Item"].Columns["EmployeeID"], createConstraints: true);
				dataGridItems.DataSource = dataSet;
				dataGridItems.DisplayLayout.Bands[1].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["OTFactor"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["OTFixedAmount"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["OTIsFixed"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["OTRate"];
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["OTBase"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["JoiningDate"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["ResumedDate"];
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["Class"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["Gender"];
				UltraGridColumn ultraGridColumn10 = dataGridItems.DisplayLayout.Bands[0].Columns["Sponsor"];
				UltraGridColumn ultraGridColumn11 = dataGridItems.DisplayLayout.Bands[0].Columns["Group"];
				UltraGridColumn ultraGridColumn12 = dataGridItems.DisplayLayout.Bands[0].Columns["Position"];
				UltraGridColumn ultraGridColumn13 = dataGridItems.DisplayLayout.Bands[0].Columns["Division"];
				UltraGridColumn ultraGridColumn14 = dataGridItems.DisplayLayout.Bands[0].Columns["LabourID"];
				UltraGridColumn ultraGridColumn15 = dataGridItems.DisplayLayout.Bands[0].Columns["IBANNo"];
				UltraGridColumn ultraGridColumn16 = dataGridItems.DisplayLayout.Bands[0].Columns["Bank"];
				UltraGridColumn ultraGridColumn17 = dataGridItems.DisplayLayout.Bands[0].Columns["UnpaidAnnualLeaves"];
				UltraGridColumn ultraGridColumn18 = dataGridItems.DisplayLayout.Bands[0].Columns["NormalLeaves"];
				UltraGridColumn ultraGridColumn19 = dataGridItems.DisplayLayout.Bands[0].Columns["SickLeaves"];
				UltraGridColumn ultraGridColumn20 = dataGridItems.DisplayLayout.Bands[0].Columns["TransferType"];
				UltraGridColumn ultraGridColumn21 = dataGridItems.DisplayLayout.Bands[0].Columns["IBANNo"];
				UltraGridColumn ultraGridColumn22 = dataGridItems.DisplayLayout.Bands[0].Columns["LastMonthSalary"];
				UltraGridColumn ultraGridColumn23 = dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["Nationality"].Hidden = true;
				bool flag4 = ultraGridColumn23.Hidden = flag2;
				bool flag6 = ultraGridColumn22.Hidden = flag4;
				bool flag8 = ultraGridColumn21.Hidden = flag6;
				bool flag10 = ultraGridColumn20.Hidden = flag8;
				bool flag12 = ultraGridColumn19.Hidden = flag10;
				bool flag14 = ultraGridColumn18.Hidden = flag12;
				bool flag16 = ultraGridColumn17.Hidden = flag14;
				bool flag18 = ultraGridColumn16.Hidden = flag16;
				bool flag20 = ultraGridColumn15.Hidden = flag18;
				bool flag22 = ultraGridColumn14.Hidden = flag20;
				bool flag24 = ultraGridColumn13.Hidden = flag22;
				bool flag26 = ultraGridColumn12.Hidden = flag24;
				bool flag28 = ultraGridColumn11.Hidden = flag26;
				bool flag30 = ultraGridColumn10.Hidden = flag28;
				bool flag32 = ultraGridColumn9.Hidden = flag30;
				bool flag34 = ultraGridColumn8.Hidden = flag32;
				bool flag36 = ultraGridColumn7.Hidden = flag34;
				bool flag38 = ultraGridColumn6.Hidden = flag36;
				bool flag40 = ultraGridColumn5.Hidden = flag38;
				bool flag42 = ultraGridColumn4.Hidden = flag40;
				bool flag44 = ultraGridColumn3.Hidden = flag42;
				bool hidden = ultraGridColumn2.Hidden = flag44;
				ultraGridColumn.Hidden = hidden;
				dataGridItems.LoadLayout();
				dataGridItems.ShowInsertMenu = false;
				UltraGridColumn ultraGridColumn24 = dataGridItems.DisplayLayout.Bands[0].Columns["Sl.No"];
				UltraGridColumn ultraGridColumn25 = dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"];
				UltraGridColumn ultraGridColumn26 = dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"];
				UltraGridColumn ultraGridColumn27 = dataGridItems.DisplayLayout.Bands[0].Columns["WorkDays"];
				UltraGridColumn ultraGridColumn28 = dataGridItems.DisplayLayout.Bands[0].Columns["NetDays"];
				UltraGridColumn ultraGridColumn29 = dataGridItems.DisplayLayout.Bands[0].Columns["Deduction"];
				UltraGridColumn ultraGridColumn30 = dataGridItems.DisplayLayout.Bands[0].Columns["LoanDeduction"];
				UltraGridColumn ultraGridColumn31 = dataGridItems.DisplayLayout.Bands[0].Columns["Basic"];
				UltraGridColumn ultraGridColumn32 = dataGridItems.DisplayLayout.Bands[0].Columns["Allowance"];
				UltraGridColumn ultraGridColumn33 = dataGridItems.DisplayLayout.Bands[0].Columns["OTHours"];
				UltraGridColumn ultraGridColumn34 = dataGridItems.DisplayLayout.Bands[0].Columns["OTAmount"];
				UltraGridColumn ultraGridColumn35 = dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"];
				UltraGridColumn ultraGridColumn36 = dataGridItems.DisplayLayout.Bands[0].Columns["Gender"];
				UltraGridColumn ultraGridColumn37 = dataGridItems.DisplayLayout.Bands[0].Columns["Sponsor"];
				UltraGridColumn ultraGridColumn38 = dataGridItems.DisplayLayout.Bands[0].Columns["Class"];
				UltraGridColumn ultraGridColumn39 = dataGridItems.DisplayLayout.Bands[0].Columns["Group"];
				UltraGridColumn ultraGridColumn40 = dataGridItems.DisplayLayout.Bands[0].Columns["Position"];
				UltraGridColumn ultraGridColumn41 = dataGridItems.DisplayLayout.Bands[0].Columns["Division"];
				UltraGridColumn ultraGridColumn42 = dataGridItems.DisplayLayout.Bands[0].Columns["LabourID"];
				UltraGridColumn ultraGridColumn43 = dataGridItems.DisplayLayout.Bands[0].Columns["IBANNo"];
				UltraGridColumn ultraGridColumn44 = dataGridItems.DisplayLayout.Bands[0].Columns["Bank"];
				UltraGridColumn ultraGridColumn45 = dataGridItems.DisplayLayout.Bands[0].Columns["TransferType"];
				UltraGridColumn ultraGridColumn46 = dataGridItems.DisplayLayout.Bands[0].Columns["Nationality"];
				UltraGridColumn ultraGridColumn47 = dataGridItems.DisplayLayout.Bands[0].Columns["LastMonthSalary"];
				Activation activation2 = dataGridItems.DisplayLayout.Bands[0].Columns["GrossSalary"].CellActivation = Activation.NoEdit;
				Activation activation4 = ultraGridColumn47.CellActivation = activation2;
				Activation activation6 = ultraGridColumn46.CellActivation = activation4;
				Activation activation8 = ultraGridColumn45.CellActivation = activation6;
				Activation activation10 = ultraGridColumn44.CellActivation = activation8;
				Activation activation12 = ultraGridColumn43.CellActivation = activation10;
				Activation activation14 = ultraGridColumn42.CellActivation = activation12;
				Activation activation16 = ultraGridColumn41.CellActivation = activation14;
				Activation activation18 = ultraGridColumn40.CellActivation = activation16;
				Activation activation20 = ultraGridColumn39.CellActivation = activation18;
				Activation activation22 = ultraGridColumn38.CellActivation = activation20;
				Activation activation24 = ultraGridColumn37.CellActivation = activation22;
				Activation activation26 = ultraGridColumn36.CellActivation = activation24;
				Activation activation28 = ultraGridColumn35.CellActivation = activation26;
				Activation activation30 = ultraGridColumn34.CellActivation = activation28;
				Activation activation32 = ultraGridColumn33.CellActivation = activation30;
				Activation activation34 = ultraGridColumn32.CellActivation = activation32;
				Activation activation36 = ultraGridColumn31.CellActivation = activation34;
				Activation activation38 = ultraGridColumn30.CellActivation = activation36;
				Activation activation40 = ultraGridColumn29.CellActivation = activation38;
				Activation activation42 = ultraGridColumn28.CellActivation = activation40;
				Activation activation44 = ultraGridColumn27.CellActivation = activation42;
				Activation activation46 = ultraGridColumn26.CellActivation = activation44;
				Activation activation49 = ultraGridColumn24.CellActivation = (ultraGridColumn25.CellActivation = activation46);
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGridItems.DisplayLayout.Bands[0].Columns["WorkDays"].CellAppearance;
				AppearanceBase cellAppearance4 = dataGridItems.DisplayLayout.Bands[0].Columns["Deduction"].CellAppearance;
				AppearanceBase cellAppearance5 = dataGridItems.DisplayLayout.Bands[0].Columns["LoanDeduction"].CellAppearance;
				AppearanceBase cellAppearance6 = dataGridItems.DisplayLayout.Bands[0].Columns["NetDays"].CellAppearance;
				AppearanceBase cellAppearance7 = dataGridItems.DisplayLayout.Bands[0].Columns["Basic"].CellAppearance;
				AppearanceBase cellAppearance8 = dataGridItems.DisplayLayout.Bands[0].Columns["OTAmount"].CellAppearance;
				AppearanceBase cellAppearance9 = dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"].CellAppearance;
				AppearanceBase cellAppearance10 = dataGridItems.DisplayLayout.Bands[0].Columns["Allowance"].CellAppearance;
				Color color = dataGridItems.DisplayLayout.Bands[0].Columns["GrossSalary"].CellAppearance.BackColor = Color.WhiteSmoke;
				Color color3 = cellAppearance10.BackColor = color;
				Color color5 = cellAppearance9.BackColor = color3;
				Color color7 = cellAppearance8.BackColor = color5;
				Color color9 = cellAppearance7.BackColor = color7;
				Color color11 = cellAppearance6.BackColor = color9;
				Color color13 = cellAppearance5.BackColor = color11;
				Color color15 = cellAppearance4.BackColor = color13;
				Color color17 = cellAppearance3.BackColor = color15;
				Color color20 = cellAppearance.BackColor = (cellAppearance2.BackColor = color17);
				AppearanceBase cellAppearance11 = dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].CellAppearance;
				AppearanceBase cellAppearance12 = dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"].CellAppearance;
				AppearanceBase cellAppearance13 = dataGridItems.DisplayLayout.Bands[0].Columns["WorkDays"].CellAppearance;
				AppearanceBase cellAppearance14 = dataGridItems.DisplayLayout.Bands[0].Columns["NetDays"].CellAppearance;
				AppearanceBase cellAppearance15 = dataGridItems.DisplayLayout.Bands[0].Columns["Deduction"].CellAppearance;
				AppearanceBase cellAppearance16 = dataGridItems.DisplayLayout.Bands[0].Columns["LoanDeduction"].CellAppearance;
				AppearanceBase cellAppearance17 = dataGridItems.DisplayLayout.Bands[0].Columns["Basic"].CellAppearance;
				AppearanceBase cellAppearance18 = dataGridItems.DisplayLayout.Bands[0].Columns["OTAmount"].CellAppearance;
				AppearanceBase cellAppearance19 = dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"].CellAppearance;
				AppearanceBase cellAppearance20 = dataGridItems.DisplayLayout.Bands[0].Columns["Allowance"].CellAppearance;
				color = (dataGridItems.DisplayLayout.Bands[0].Columns["GrossSalary"].CellAppearance.BackColorDisabled = Color.WhiteSmoke);
				color3 = (cellAppearance20.BackColorDisabled = color);
				color5 = (cellAppearance19.BackColorDisabled = color3);
				color7 = (cellAppearance18.BackColorDisabled = color5);
				color9 = (cellAppearance17.BackColorDisabled = color7);
				color11 = (cellAppearance16.BackColorDisabled = color9);
				color13 = (cellAppearance15.BackColorDisabled = color11);
				color15 = (cellAppearance14.BackColorDisabled = color13);
				color17 = (cellAppearance13.BackColorDisabled = color15);
				color20 = (cellAppearance11.BackColorDisabled = (cellAppearance12.BackColorDisabled = color17));
				AppearanceBase cellAppearance21 = dataGridItems.DisplayLayout.Bands[0].Columns["Gender"].CellAppearance;
				AppearanceBase cellAppearance22 = dataGridItems.DisplayLayout.Bands[0].Columns["Sponsor"].CellAppearance;
				AppearanceBase cellAppearance23 = dataGridItems.DisplayLayout.Bands[0].Columns["Class"].CellAppearance;
				AppearanceBase cellAppearance24 = dataGridItems.DisplayLayout.Bands[0].Columns["Group"].CellAppearance;
				AppearanceBase cellAppearance25 = dataGridItems.DisplayLayout.Bands[0].Columns["Position"].CellAppearance;
				AppearanceBase cellAppearance26 = dataGridItems.DisplayLayout.Bands[0].Columns["Division"].CellAppearance;
				AppearanceBase cellAppearance27 = dataGridItems.DisplayLayout.Bands[0].Columns["LabourID"].CellAppearance;
				AppearanceBase cellAppearance28 = dataGridItems.DisplayLayout.Bands[0].Columns["IBANNo"].CellAppearance;
				AppearanceBase cellAppearance29 = dataGridItems.DisplayLayout.Bands[0].Columns["Bank"].CellAppearance;
				AppearanceBase cellAppearance30 = dataGridItems.DisplayLayout.Bands[0].Columns["TransferType"].CellAppearance;
				color = (dataGridItems.DisplayLayout.Bands[0].Columns["Nationality"].CellAppearance.BackColorDisabled = Color.WhiteSmoke);
				color3 = (cellAppearance30.BackColorDisabled = color);
				color5 = (cellAppearance29.BackColorDisabled = color3);
				color7 = (cellAppearance28.BackColorDisabled = color5);
				color9 = (cellAppearance27.BackColorDisabled = color7);
				color11 = (cellAppearance26.BackColorDisabled = color9);
				color13 = (cellAppearance25.BackColorDisabled = color11);
				color15 = (cellAppearance24.BackColorDisabled = color13);
				color17 = (cellAppearance23.BackColorDisabled = color15);
				color20 = (cellAppearance21.BackColorDisabled = (cellAppearance22.BackColorDisabled = color17));
				UltraGridColumn ultraGridColumn48 = dataGridItems.DisplayLayout.Bands[0].Columns["Basic"];
				UltraGridColumn ultraGridColumn49 = dataGridItems.DisplayLayout.Bands[0].Columns["Allowance"];
				UltraGridColumn ultraGridColumn50 = dataGridItems.DisplayLayout.Bands[0].Columns["Deduction"];
				UltraGridColumn ultraGridColumn51 = dataGridItems.DisplayLayout.Bands[0].Columns["LoanDeduction"];
				UltraGridColumn ultraGridColumn52 = dataGridItems.DisplayLayout.Bands[0].Columns["OTAmount"];
				UltraGridColumn ultraGridColumn53 = dataGridItems.DisplayLayout.Bands[0].Columns["OTHours"];
				UltraGridColumn ultraGridColumn54 = dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"];
				UltraGridColumn ultraGridColumn55 = dataGridItems.DisplayLayout.Bands[0].Columns["LastMonthSalary"];
				string text = dataGridItems.DisplayLayout.Bands[0].Columns["GrossSalary"].Format = Format.GridAmountFormat;
				string text3 = ultraGridColumn55.Format = text;
				string text5 = ultraGridColumn54.Format = text3;
				string text7 = ultraGridColumn53.Format = text5;
				string text9 = ultraGridColumn52.Format = text7;
				string text11 = ultraGridColumn51.Format = text9;
				string text13 = ultraGridColumn50.Format = text11;
				string text16 = ultraGridColumn48.Format = (ultraGridColumn49.Format = text13);
				AppearanceBase cellAppearance31 = dataGridItems.DisplayLayout.Bands[0].Columns["WorkDays"].CellAppearance;
				AppearanceBase cellAppearance32 = dataGridItems.DisplayLayout.Bands[0].Columns["NetDays"].CellAppearance;
				AppearanceBase cellAppearance33 = dataGridItems.DisplayLayout.Bands[0].Columns["OTHours"].CellAppearance;
				AppearanceBase cellAppearance34 = dataGridItems.DisplayLayout.Bands[0].Columns["OTRate"].CellAppearance;
				AppearanceBase cellAppearance35 = dataGridItems.DisplayLayout.Bands[0].Columns["Deduction"].CellAppearance;
				AppearanceBase cellAppearance36 = dataGridItems.DisplayLayout.Bands[0].Columns["LoanDeduction"].CellAppearance;
				AppearanceBase cellAppearance37 = dataGridItems.DisplayLayout.Bands[0].Columns["Absent"].CellAppearance;
				AppearanceBase cellAppearance38 = dataGridItems.DisplayLayout.Bands[0].Columns["Basic"].CellAppearance;
				AppearanceBase cellAppearance39 = dataGridItems.DisplayLayout.Bands[0].Columns["OTAmount"].CellAppearance;
				AppearanceBase cellAppearance40 = dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"].CellAppearance;
				AppearanceBase cellAppearance41 = dataGridItems.DisplayLayout.Bands[0].Columns["Allowance"].CellAppearance;
				AppearanceBase cellAppearance42 = dataGridItems.DisplayLayout.Bands[0].Columns["LastMonthSalary"].CellAppearance;
				HAlign hAlign2 = dataGridItems.DisplayLayout.Bands[0].Columns["GrossSalary"].CellAppearance.TextHAlign = HAlign.Right;
				HAlign hAlign4 = cellAppearance42.TextHAlign = hAlign2;
				HAlign hAlign6 = cellAppearance41.TextHAlign = hAlign4;
				HAlign hAlign8 = cellAppearance40.TextHAlign = hAlign6;
				HAlign hAlign10 = cellAppearance39.TextHAlign = hAlign8;
				HAlign hAlign12 = cellAppearance38.TextHAlign = hAlign10;
				HAlign hAlign14 = cellAppearance37.TextHAlign = hAlign12;
				HAlign hAlign16 = cellAppearance36.TextHAlign = hAlign14;
				HAlign hAlign18 = cellAppearance35.TextHAlign = hAlign16;
				HAlign hAlign20 = cellAppearance34.TextHAlign = hAlign18;
				HAlign hAlign22 = cellAppearance33.TextHAlign = hAlign20;
				HAlign hAlign25 = cellAppearance31.TextHAlign = (cellAppearance32.TextHAlign = hAlign22);
				UltraGridColumn ultraGridColumn56 = dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"];
				UltraGridColumn ultraGridColumn57 = dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"];
				UltraGridColumn ultraGridColumn58 = dataGridItems.DisplayLayout.Bands[0].Columns["Deduction"];
				UltraGridColumn ultraGridColumn59 = dataGridItems.DisplayLayout.Bands[0].Columns["LoanDeduction"];
				UltraGridColumn ultraGridColumn60 = dataGridItems.DisplayLayout.Bands[0].Columns["WorkDays"];
				UltraGridColumn ultraGridColumn61 = dataGridItems.DisplayLayout.Bands[0].Columns["NetDays"];
				UltraGridColumn ultraGridColumn62 = dataGridItems.DisplayLayout.Bands[0].Columns["Basic"];
				UltraGridColumn ultraGridColumn63 = dataGridItems.DisplayLayout.Bands[0].Columns["OTAmount"];
				UltraGridColumn ultraGridColumn64 = dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"];
				UltraGridColumn ultraGridColumn65 = dataGridItems.DisplayLayout.Bands[0].Columns["Allowance"];
				flag28 = (dataGridItems.DisplayLayout.Bands[0].Columns["GrossSalary"].TabStop = false);
				flag30 = (ultraGridColumn65.TabStop = flag28);
				flag32 = (ultraGridColumn64.TabStop = flag30);
				flag34 = (ultraGridColumn63.TabStop = flag32);
				flag36 = (ultraGridColumn62.TabStop = flag34);
				flag38 = (ultraGridColumn61.TabStop = flag36);
				flag40 = (ultraGridColumn60.TabStop = flag38);
				flag42 = (ultraGridColumn59.TabStop = flag40);
				flag44 = (ultraGridColumn58.TabStop = flag42);
				hidden = (ultraGridColumn57.TabStop = flag44);
				ultraGridColumn56.TabStop = hidden;
				dataGridItems.DisplayLayout.Bands[1].Columns["OTHiddenAmount"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].Header.Caption = "Emp Code";
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"].Header.Caption = "Emp Name";
				dataGridItems.DisplayLayout.Bands[0].Columns["WorkDays"].Header.Caption = "Work Days";
				dataGridItems.DisplayLayout.Bands[0].Columns["OTRate"].Header.Caption = "OT Rate";
				dataGridItems.DisplayLayout.Bands[0].Columns["NetDays"].Header.Caption = "Net Days";
				dataGridItems.DisplayLayout.Bands[0].Columns["Deduction"].Header.Caption = "Deduction";
				dataGridItems.DisplayLayout.Bands[0].Columns["LoanDeduction"].Header.Caption = "Loan";
				dataGridItems.DisplayLayout.Bands[0].Columns["Basic"].Header.Caption = "Basic";
				dataGridItems.DisplayLayout.Bands[0].Columns["OTHours"].Header.Caption = "OT Hours";
				dataGridItems.DisplayLayout.Bands[0].Columns["Allowance"].Header.Caption = "Allowance";
				dataGridItems.DisplayLayout.Bands[0].Columns["OTAmount"].Header.Caption = "OT Amount";
				dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"].Header.Caption = "Net Salary";
				dataGridItems.DisplayLayout.Bands[0].Columns["GrossSalary"].Header.Caption = "Gross Salary";
				UltraGridColumn ultraGridColumn66 = dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"];
				UltraGridColumn ultraGridColumn67 = dataGridItems.DisplayLayout.Bands[0].Columns["OTRate"];
				UltraGridColumn ultraGridColumn68 = dataGridItems.DisplayLayout.Bands[0].Columns["OTBase"];
				UltraGridColumn ultraGridColumn69 = dataGridItems.DisplayLayout.Bands[0].Columns["OTFactor"];
				UltraGridColumn ultraGridColumn70 = dataGridItems.DisplayLayout.Bands[0].Columns["OTFixedAmount"];
				UltraGridColumn ultraGridColumn71 = dataGridItems.DisplayLayout.Bands[0].Columns["OTIsFixed"];
				UltraGridColumn ultraGridColumn72 = dataGridItems.DisplayLayout.Bands[0].Columns["ResumedDate"];
				UltraGridColumn ultraGridColumn73 = dataGridItems.DisplayLayout.Bands[0].Columns["AnnualLeaves"];
				UltraGridColumn ultraGridColumn74 = dataGridItems.DisplayLayout.Bands[0].Columns["Absent"];
				ExcludeFromColumnChooser excludeFromColumnChooser2 = dataGridItems.DisplayLayout.Bands[0].Columns["Sl.No"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				ExcludeFromColumnChooser excludeFromColumnChooser4 = ultraGridColumn74.ExcludeFromColumnChooser = excludeFromColumnChooser2;
				ExcludeFromColumnChooser excludeFromColumnChooser6 = ultraGridColumn73.ExcludeFromColumnChooser = excludeFromColumnChooser4;
				ExcludeFromColumnChooser excludeFromColumnChooser8 = ultraGridColumn72.ExcludeFromColumnChooser = excludeFromColumnChooser6;
				ExcludeFromColumnChooser excludeFromColumnChooser10 = ultraGridColumn71.ExcludeFromColumnChooser = excludeFromColumnChooser8;
				ExcludeFromColumnChooser excludeFromColumnChooser12 = ultraGridColumn70.ExcludeFromColumnChooser = excludeFromColumnChooser10;
				ExcludeFromColumnChooser excludeFromColumnChooser14 = ultraGridColumn69.ExcludeFromColumnChooser = excludeFromColumnChooser12;
				ExcludeFromColumnChooser excludeFromColumnChooser16 = ultraGridColumn68.ExcludeFromColumnChooser = excludeFromColumnChooser14;
				ExcludeFromColumnChooser excludeFromColumnChooser19 = ultraGridColumn66.ExcludeFromColumnChooser = (ultraGridColumn67.ExcludeFromColumnChooser = excludeFromColumnChooser16);
				dataGridItems.DisplayLayout.Bands[0].Columns["AnnualLeaves"].Header.Caption = "Paid Annual Leaves";
				dataGridItems.DisplayLayout.Bands[0].Columns["UnPaidAnnualLeaves"].Header.Caption = "Unpaid Annual Leaves";
				UltraGridColumn ultraGridColumn75 = dataGridItems.DisplayLayout.Bands[1].Columns["EmployeeID"];
				UltraGridColumn ultraGridColumn76 = dataGridItems.DisplayLayout.Bands[1].Columns["LoanSysDocID"];
				UltraGridColumn ultraGridColumn77 = dataGridItems.DisplayLayout.Bands[1].Columns["InDeduction"];
				UltraGridColumn ultraGridColumn78 = dataGridItems.DisplayLayout.Bands[1].Columns["IsFixed"];
				flag40 = (dataGridItems.DisplayLayout.Bands[1].Columns["PayCodeType"].Hidden = true);
				flag42 = (ultraGridColumn78.Hidden = flag40);
				flag44 = (ultraGridColumn77.Hidden = flag42);
				hidden = (ultraGridColumn76.Hidden = flag44);
				ultraGridColumn75.Hidden = hidden;
				UltraGridColumn ultraGridColumn79 = dataGridItems.DisplayLayout.Bands[1].Columns["InDeduction"];
				UltraGridColumn ultraGridColumn80 = dataGridItems.DisplayLayout.Bands[1].Columns["IsFixed"];
				excludeFromColumnChooser16 = (dataGridItems.DisplayLayout.Bands[1].Columns["PayCodeType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
				excludeFromColumnChooser19 = (ultraGridColumn79.ExcludeFromColumnChooser = (ultraGridColumn80.ExcludeFromColumnChooser = excludeFromColumnChooser16));
				dataGridItems.DisplayLayout.Bands[1].Columns["PayableAmount"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[1].Columns["PayableAmount"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[1].Columns["PayableAmount"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				text16 = (dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].Format = (dataGridItems.DisplayLayout.Bands[1].Columns["PayableAmount"].Format = Format.GridAmountFormat));
				dataGridItems.DisplayLayout.Bands[1].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["PayableAmount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[1].Columns["PayableAmount"].Header.Caption = "Payable AMT";
				dataGridItems.DisplayLayout.Bands[1].Columns["PayType"].Header.Caption = "Type";
				dataGridItems.DisplayLayout.Bands[1].Columns["PayrollItemID"].Header.Caption = "Payroll Item";
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(1, "Payment");
				valueList.ValueListItems.Add(2, "Deduction");
				valueList.ValueListItems.Add(3, "Loan Recovery");
				dataGridItems.DisplayLayout.Bands[1].Columns["PayType"].ValueList = valueList;
				dataGridItems.DisplayLayout.Bands[1].Columns["PayrollItemID"].ValueList = comboBoxPayrollItemPayment;
				dataGridItems.DisplayLayout.Bands[1].Columns["PayrollItemID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridItems.DisplayLayout.Bands[1].Columns["PayType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridItems.DisplayLayout.Bands[1].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Clear();
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Basic", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Basic"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Allowance", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Allowance"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Deduction", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Deduction"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("GrossSalary", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["GrossSalary"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("OTAmount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["OTAmount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("NetSalary", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["NetSalary"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Override.SummaryFooterAppearance.BackColor = Color.FromArgb(227, 239, 253);
				dataGridItems.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.SortSingle;
				dataGridItems.DisplayLayout.Bands[0].Override.SummaryValueAppearance.BackColor = Color.FromArgb(227, 239, 253);
				dataGridItems.DisplayLayout.Bands[0].Override.SummaryFooterAppearance.BorderColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Override.SummaryFooterSpacingBefore = 1;
				dataGridItems.DisplayLayout.Bands[0].Override.MinRowHeight = 18;
				dataGridItems.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.No;
				dataGridItems.DisplayLayout.Bands[1].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
				dataGridItems.DisplayLayout.Bands[0].Columns["Sl.No"].MaxWidth = 36;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 3000;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeID"].AllowRowFiltering = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["EmployeeName"].AllowRowFiltering = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].CellMultiLine = DefaultableBoolean.True;
				UltraGridColumn ultraGridColumn81 = dataGridItems.DisplayLayout.Bands[0].Columns["OTRate"];
				UltraGridColumn ultraGridColumn82 = dataGridItems.DisplayLayout.Bands[0].Columns["OTFactor"];
				flag44 = (dataGridItems.DisplayLayout.Bands[0].Columns["OTFixedAmount"].Hidden = true);
				hidden = (ultraGridColumn82.Hidden = flag44);
				ultraGridColumn81.Hidden = hidden;
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
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && CanClose())
				{
					currentData = Factory.SalarySheetSystem.GetSalarySheetByID(SystemDocID, voucherID);
					if (currentData != null)
					{
						IsNewRecord = false;
						IsReCalculate = false;
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
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["SalarySheet"].Rows[0];
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					dateTimePickerStartDate.Value = DateTime.Parse(dataRow["StartDate"].ToString());
					dateTimePickerEndDate.Value = DateTime.Parse(dataRow["EndDate"].ToString());
					textBoxDescription.Text = dataRow["SheetName"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					textBoxRef.Text = dataRow["Reference"].ToString();
					comboBoxMonth.SelectedID = int.Parse(dataRow["Month"].ToString());
					comboBoxYear.SelectedIndex = checked(int.Parse(dataRow["Year"].ToString()) - 2000);
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					isPosted = false;
					bool.TryParse(dataRow["IsPosted"].ToString(), out isPosted);
					bool flag = false;
					if (dataRow["IsVoid"] != DBNull.Value)
					{
						flag = bool.Parse(dataRow["IsVoid"].ToString());
					}
					IsVoid = flag;
					if (currentData.Tables.Contains("SalarySheet_Detail") && currentData.SalarySheetDetailTable.Rows.Count != 0)
					{
						FillGridData(currentData, IsReCalculate);
						IsReCalculate = true;
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				isDataLoading = false;
			}
		}

		private void FillGridData(DataSet data, bool isRecalculate)
		{
			string[] array = null;
			decimal result = default(decimal);
			DataSet dataSet = dataGridItems.DataSource as DataSet;
			if (!isRecalculate)
			{
				dataSet.Tables[1].Rows.Clear();
				dataSet.Tables[0].Rows.Clear();
			}
			else
			{
				isNewRecord = false;
				DataView defaultView = currentgridData.Tables["SalarySheet_Detail"].DefaultView;
				defaultView.RowFilter = "EmployeeID <>''";
				array = (from s in defaultView.ToTable().AsEnumerable()
					select s.Field<string>("EmployeeID")).ToArray();
			}
			DataTable dataTable = dataSet.Tables["SalarySheet_Detail"];
			DataTable dataTable2;
			checked
			{
				foreach (DataRow row in data.Tables["SalarySheet_Detail"].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["Sl.No"] = int.Parse(row["SlNo"].ToString()) + 1;
					dataRow2["EmployeeID"] = row["EmployeeID"];
					int num = -1;
					if (array != null)
					{
						num = Array.IndexOf(array, row["EmployeeID"]);
					}
					if (num == -1)
					{
						if (array != null && num == -1)
						{
							int num2 = 0;
							if (dataSet.Tables[0].Rows.Count != 1)
							{
								num2 = Convert.ToInt32(dataSet.Tables[0].Compute("max([Sl.No])", string.Empty));
								dataRow2["Sl.No"] = num2 + 1;
								row["SlNo"] = num2 + 1;
							}
							else
							{
								dataRow2["Sl.No"] = 1;
								row["SlNo"] = 1;
							}
						}
						dataRow2["JoiningDate"] = row["JoiningDate"];
						dataRow2["ResumedDate"] = row["ResumedDate"];
						dataRow2["Gender"] = row["Gender"];
						dataRow2["Sponsor"] = row["SponsorID"];
						dataRow2["Class"] = row["ContractType"];
						dataRow2["Group"] = row["GroupID"];
						dataRow2["Position"] = row["PositionName"];
						dataRow2["Division"] = row["DivisionID"];
						dataRow2["LabourID"] = row["LabourID"];
						dataRow2["IBANNo"] = row["IBAN"];
						dataRow2["Nationality"] = row["NationalityID"];
						dataRow2["Bank"] = row["BankID"];
						dataRow2["TransferType"] = row["TransferType"];
						decimal result2 = default(decimal);
						decimal.TryParse(row["WorkDays"].ToString(), out result2);
						dataRow2["EmployeeName"] = row["EmployeeName"];
						dataRow2["WorkDays"] = result2.ToString(Format.QuantityFormat);
						decimal result3 = default(decimal);
						decimal result4 = default(decimal);
						decimal result5 = default(decimal);
						decimal result6 = default(decimal);
						decimal result7 = default(decimal);
						decimal result8 = default(decimal);
						decimal result9 = default(decimal);
						decimal result10 = default(decimal);
						decimal.TryParse(row["Absent"].ToString(), out result3);
						decimal.TryParse(row["AnnualLeaves"].ToString(), out result5);
						decimal.TryParse(row["UnpaidAnnualLeaves"].ToString(), out result8);
						decimal.TryParse(row["SickLeaves"].ToString(), out result10);
						decimal.TryParse(row["NormalLeaves"].ToString(), out result9);
						if (row.Table.Columns.Contains("ToLessTakenAbs"))
						{
							decimal.TryParse(row["ToLessTakenAbs"].ToString(), out result4);
						}
						if (row.Table.Columns.Contains("ToLessTakenAnn"))
						{
							decimal.TryParse(row["ToLessTakenAnn"].ToString(), out result6);
						}
						if (row.Table.Columns.Contains("Remarks"))
						{
							dataRow2["Remarks"] = row["Remarks"];
						}
						int num3 = 0;
						num3 = Convert.ToInt32(data.Tables["SalarySheet_Detail_Item"].Compute("max(LeaveDays)", string.Empty));
						string filterExpression = "EmployeeID = '" + row["EmployeeID"] + "'";
						string sort = "LeaveDays ASC";
						DataRow[] array2 = data.Tables["SalarySheet_Detail_Item"].Select(filterExpression, sort);
						for (int i = 0; i < array2.Length; i = unchecked(i + 1))
						{
							int.TryParse(array2[i]["LeaveDays"].ToString(), out num3);
						}
						result3 = result3 - result4 + (decimal)num3;
						dataRow2["AnnualLeaves"] = (result5 - result6).ToString(Format.QuantityFormat);
						dataRow2["Absent"] = result3.ToString(Format.QuantityFormat);
						dataRow2["UnpaidAnnualLeaves"] = result8.ToString(Format.QuantityFormat);
						dataRow2["NormalLeaves"] = result9.ToString(Format.QuantityFormat);
						dataRow2["SickLeaves"] = result10.ToString(Format.QuantityFormat);
						decimal.TryParse(row["NetDays"].ToString(), out result7);
						int num4 = int.Parse(textBoxTotalDays.Text);
						if (CompanyPreferences.ThirtyDays && num4 > 30)
						{
							result7 -= 1m;
						}
						dataRow2["NetDays"] = result7.ToString(Format.QuantityFormat);
						dataRow2["Basic"] = row["Basic"];
						dataRow2["Allowance"] = row["Allowance"];
						dataRow2["Deduction"] = row["Deduction"];
						dataRow2["LoanDeduction"] = row["LoanDeduction"];
						dataRow2["GrossSalary"] = row["GrossSalary"];
						dataRow2["OTHours"] = row["OTHours"];
						dataRow2["OTBase"] = row["OTBase"];
						dataRow2["OTAmount"] = row["OTAmount"];
						dataRow2["NetSalary"] = row["NetSalary"];
						dataRow2["LastMonthSalary"] = row["LastSalary"];
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
				}
				dataTable.AcceptChanges();
				dataTable2 = dataSet.Tables["SalarySheet_Detail_Item"];
				if (!isRecalculate)
				{
					dataTable2.Rows.Clear();
				}
			}
			foreach (DataRow row2 in data.Tables["SalarySheet_Detail_Item"].Rows)
			{
				DataRow dataRow4 = dataTable2.NewRow();
				dataRow4["EmployeeID"] = row2["EmployeeID"];
				dataRow4["PayType"] = row2["PayType"];
				dataRow4["PayrollItemID"] = row2["PayrollItemID"];
				dataRow4["LoanSysDocID"] = row2["LoanSysDocID"];
				dataRow4["Description"] = row2["Description"];
				dataRow4["InDeduction"] = row2["InDeduction"];
				dataRow4["PayCodeType"] = row2["PayCodeType"];
				dataRow4["Amount"] = row2["Amount"];
				dataRow4["PayableAmount"] = row2["PayableAmount"];
				if (isNewRecord || isChanged)
				{
					dataRow4["OTHiddenAmount"] = row2["Amount"];
				}
				else
				{
					string filterExpression2 = "EmployeeID = '" + row2["EmployeeID"] + "'";
					DataRow[] array2 = data.Tables["SalarySheet_Detail"].Select(filterExpression2);
					for (int i = 0; i < array2.Length; i++)
					{
						decimal.TryParse(array2[i]["OTBase"].ToString(), out result);
					}
					int totalWorkingMonthHours = CompanyPreferences.TotalWorkingMonthHours;
					decimal result11 = default(decimal);
					decimal.TryParse(row2["Amount"].ToString(), out result11);
					decimal num5 = default(decimal);
					if (result > 0m && !isChanged)
					{
						num5 = result11 * (decimal)totalWorkingMonthHours / result;
					}
					dataRow4["OTHiddenAmount"] = num5;
				}
				if (isNewRecord)
				{
					dataRow4["IsFixed"] = true;
				}
				else if (!isNewRecord && !IsReCalculate)
				{
					dataRow4["IsFixed"] = row2["IsFixed"];
				}
				else if (IsReCalculate)
				{
					dataRow4["IsFixed"] = true;
				}
				if (row2["PayType"].ToString() == "3")
				{
					dataRow4["IsFixed"] = false;
				}
				if (row2["PayCodeType"].ToString() == "6" || row2["PayCodeType"].ToString() == "7")
				{
					dataRow4["IsFixed"] = false;
				}
				dataRow4.EndEdit();
				dataTable2.Rows.Add(dataRow4);
			}
			dataTable2.AcceptChanges();
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
				bool flag = Factory.SalarySheetSystem.CreateSalarySheet(currentData, !isNewRecord);
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
				ErrorHelper.ProcessError(ex);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
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
			formManager.ShowApprovalPanel(approvalTaskID, "SalarySheet", "VoucherID");
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
			if (textBoxDescription.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				textBoxDescription.Focus();
				return false;
			}
			if (comboBoxMonth.SelectedID < 0)
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				comboBoxMonth.Focus();
				return false;
			}
			if (dateTimePickerStartDate.Value > dateTimePickerEndDate.Value)
			{
				ErrorHelper.InformationMessage("Start date cannot be greater than end date.");
				dateTimePickerEndDate.Focus();
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("SalarySheet", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
				textBoxVoucherNumber.Focus();
				return false;
			}
			new ArrayList();
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				foreach (UltraGridRow row2 in row.ChildBands[0].Rows)
				{
					row2.Cells["PayType"].Value.ToString();
					if (row2.Cells["PayrollItemID"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select a payroll item.");
						row2.Activate();
						return false;
					}
				}
			}
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one payment row.");
				return false;
			}
			if (!IsNewRecord && !Factory.SalarySheetSystem.AllowDelete(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("Some items in this transaction has been already allocated. You are not able to modify.");
				return false;
			}
			foreach (UltraGridRow row3 in dataGridItems.Rows)
			{
				string str = row3.Cells["EmployeeID"].Value.ToString();
				if (row3.ChildBands[0].Rows.Count < 1)
				{
					ErrorHelper.InformationMessage("There should be at least one payment row for Employee " + str);
					return false;
				}
			}
			foreach (UltraGridRow row4 in dataGridItems.Rows)
			{
				if (Convert.ToDecimal(row4.Cells["NetDays"].Value.ToString()) < 0m)
				{
					string str2 = row4.Cells["EmployeeID"].Value.ToString();
					ErrorHelper.InformationMessage("Incorrect format in net days for Employee " + str2);
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
				decimal num = default(decimal);
				byte b = 1;
				if (row.Cells["Amount"].Value != null && row.Cells["Amount"].Value.ToString() != "")
				{
					num = decimal.Parse(row.Cells["Amount"].Value.ToString());
				}
				if (row.Cells["Pay Type"].Value != null && row.Cells["Pay Type"].Value.ToString() != "")
				{
					b = byte.Parse(row.Cells["Pay Type"].Value.ToString());
				}
				if (b == 2 || b == 3)
				{
					result -= num;
				}
				else
				{
					result += num;
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
			checked
			{
				try
				{
					allowEdit = true;
					textBoxRef.Clear();
					dateTimePickerDate.Value = DateTime.Now;
					dateTimePickerEndDate.Value = DateTime.Now;
					dateTimePickerStartDate.Value = DateTime.Now;
					textBoxDescription.Clear();
					textBoxNote.Clear();
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
					isPosted = false;
					IsReCalculate = false;
					comboBoxGridEmployee.Clear();
					labelTotal.Text = 0.ToString(Format.TotalAmountFormat);
					int num = DateTime.Today.Month;
					if (DateTime.Today.Day < 15)
					{
						num--;
						if (num == 0)
						{
							num = 12;
							comboBoxYear.SelectedIndex -= 1;
						}
						comboBoxMonth.SelectedID = num;
						SetTransactionDate();
					}
					comboBoxMonth.SelectedID = num;
					SetBeginEndDate();
					DataSet obj = dataGridItems.DataSource as DataSet;
					obj.Tables[1].Rows.Clear();
					obj.Tables[0].Rows.Clear();
					IsVoid = false;
					formManager.ResetDirty();
					SetApprovalStatus();
					IsOT = false;
					IsDD = false;
					parentrowindx = 0;
					nextgridindx = 0;
					isChanged = false;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
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
				if (!IsNewRecord && !Factory.SalarySheetSystem.AllowDelete(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Some items in this transaction has been already allocated. You are not able to modify.");
					return false;
				}
				bool flag = Factory.SalarySheetSystem.DeleteSalarySheet(SystemDocID, textBoxVoucherNumber.Text);
				if (!flag)
				{
					ErrorHelper.ErrorMessage("Cannot delete the transaction.");
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

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("SalarySheet", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("SalarySheet", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("SalarySheet", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("SalarySheet", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID);
			}
		}

		public void OnActivated()
		{
		}

		private void SalarySheetForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
			else
			{
				Global.GlobalSettings.SaveFormProperties(this);
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

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxMonth.LoadData();
				dataGridItems.SetupUI();
				comboBoxSysDoc.FilterByType(SysDocTypes.SalarySheet);
				SetSecurity();
				SetupGrid();
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
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null)
			{
				if (dataGridItems.ActiveCell.Band.Index == 1 && dataGridItems.ActiveCell.Column.Key == "PayrollItemID")
				{
					comboBoxGridEmployeeLoan.FilterID = dataGridItems.ActiveRow.ParentRow.Cells["EmployeeID"].Value.ToString();
					comboBoxGridEmployeeLoan.ApplyFilter();
				}
				comboBoxGridEmployee.IsLoadingData = false;
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			_ = isNewRecord;
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
			if (Void(isVoid: true))
			{
				IsVoid = true;
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
				bool flag = Factory.SalarySheetSystem.VoidSalarySheet(SystemDocID, textBoxVoucherNumber.Text, isVoid);
				if (!flag)
				{
					ErrorHelper.ErrorMessage("Cannot void the transaction.");
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

		private void buttonCalculate_Click(object sender, EventArgs e)
		{
			try
			{
				IsNewRecord = true;
				IsOT = false;
				IsDD = false;
				parentrowindx = 0;
				nextgridindx = 0;
				isChanged = true;
				CalculateSalary();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void CalculateSalary()
		{
			try
			{
				CalculateSalaryForm calculateSalaryForm = new CalculateSalaryForm();
				currentgridData = (DataSet)dataGridItems.DataSource;
				if (currentgridData.Tables[0].Rows.Count > 0)
				{
					IsReCalculate = true;
					calculateSalaryForm.IsRecalulate = true;
				}
				if (calculateSalaryForm.ShowDialog() == DialogResult.OK)
				{
					DataSet dataSet = null;
					dataSet = (IsReCalculate ? Factory.SalarySheetSystem.ReCalculateSalarySheet(calculateSalaryForm.FromEmployee, calculateSalaryForm.ToEmployee, calculateSalaryForm.FromDepartment, calculateSalaryForm.ToDepartment, calculateSalaryForm.FromLocation, calculateSalaryForm.ToLocation, calculateSalaryForm.FromType, calculateSalaryForm.ToType, calculateSalaryForm.FromDivision, calculateSalaryForm.ToDivision, calculateSalaryForm.FromSponsor, calculateSalaryForm.ToSponsor, calculateSalaryForm.FromGroup, calculateSalaryForm.ToGroup, calculateSalaryForm.FromGrade, calculateSalaryForm.ToGrade, calculateSalaryForm.FromPosition, calculateSalaryForm.ToPosition, calculateSalaryForm.FromBank, calculateSalaryForm.ToBank, calculateSalaryForm.FromAccount, calculateSalaryForm.ToAccount, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value, int.Parse(comboBoxYear.Text), comboBoxMonth.SelectedID, calculateSalaryForm.MultipleEmployees, currentgridData, comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text) : Factory.SalarySheetSystem.CalculateSalarySheet(calculateSalaryForm.FromEmployee, calculateSalaryForm.ToEmployee, calculateSalaryForm.FromDepartment, calculateSalaryForm.ToDepartment, calculateSalaryForm.FromLocation, calculateSalaryForm.ToLocation, calculateSalaryForm.FromType, calculateSalaryForm.ToType, calculateSalaryForm.FromDivision, calculateSalaryForm.ToDivision, calculateSalaryForm.FromSponsor, calculateSalaryForm.ToSponsor, calculateSalaryForm.FromGroup, calculateSalaryForm.ToGroup, calculateSalaryForm.FromGrade, calculateSalaryForm.ToGrade, calculateSalaryForm.FromPosition, calculateSalaryForm.ToPosition, calculateSalaryForm.FromBank, calculateSalaryForm.ToBank, calculateSalaryForm.FromAccount, calculateSalaryForm.ToAccount, dateTimePickerStartDate.Value, dateTimePickerEndDate.Value, int.Parse(comboBoxYear.Text), comboBoxMonth.SelectedID, calculateSalaryForm.MultipleEmployees));
					if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
					{
						FillGridData(dataSet, IsReCalculate);
						ReCalculateSalary(allRows: true);
						toolStripButtonCreateSIF.Enabled = false;
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.SalarySheet);
		}

		private void ultraFormattedLinkLabel2_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord && ErrorHelper.QuestionMessageYesNo("Are you sure to copy this document?") == DialogResult.Yes)
			{
				string text = textBoxVoucherNumber.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxVoucherNumber.Text = "";
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = "";
				}
			}
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.SalarySheet);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 43.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.SalarySheet);
					currentData = (dataSet as SalarySheetData);
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

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges, bool printDetails = false)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData()))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					if (selectedID == "" || text == "")
					{
						ErrorHelper.InformationMessage("Please select a document to print!");
					}
					else
					{
						DataSet salarySheetByID = Factory.SalarySheetSystem.GetSalarySheetByID(selectedID, text);
						if (salarySheetByID == null || salarySheetByID.Tables.Count == 0)
						{
							ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
						}
						else if (!printDetails)
						{
							PrintHelper.PrintDocument(salarySheetByID, selectedID, "Salary Sheet", SysDocTypes.JobInvoice, isPrint, showPrintDialog);
						}
						else
						{
							PrintHelper.PrintDocument(salarySheetByID, "", "Salary Sheet with Details", SysDocTypes.JobInvoice, isPrint, showPrintDialog);
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButton1_Click_1(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButtonPrintDetails_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true, printDetails: true);
		}

		private void toolStripButtonCreateSIF_Click(object sender, EventArgs e)
		{
			EmployeeSIFDialogForm employeeSIFDialogForm = new EmployeeSIFDialogForm();
			employeeSIFDialogForm.Period = comboBoxMonth.SelectedID.ToString().PadLeft(2, '0') + "-" + comboBoxYear.Text;
			employeeSIFDialogForm.VoucherID = textBoxVoucherNumber.Text;
			employeeSIFDialogForm.SysDocID = comboBoxSysDoc.SelectedID;
			employeeSIFDialogForm.SetData(currentData);
			employeeSIFDialogForm.ShowDialog(this);
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.SalarySheetListFormObj);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("SalarySheet", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void toolStripMenuItemReCalculate_Click(object sender, EventArgs e)
		{
		}

		private void dataGridItems_InitializeRow(object sender, InitializeRowEventArgs e)
		{
			_ = e.ReInitialize;
		}

		private void dataGridItems_BeforeColumnChooserDisplayed(object sender, BeforeColumnChooserDisplayedEventArgs e)
		{
			e.Dialog.Size = new Size(200, 600);
			e.Dialog.DisposeOnClose = DefaultableBoolean.True;
			e.Dialog.ColumnChooserControl.MultipleBandSupport = MultipleBandSupport.SingleBandOnly;
			e.Dialog.ColumnChooserControl.Style = ColumnChooserStyle.AllColumnsWithCheckBoxes;
			e.Dialog.ColumnChooserControl.SyncLookWithSourceGrid = false;
			e.Dialog.ColumnChooserControl.DisplayLayout.Appearance.BackColor = SystemColors.Window;
		}

		private void createFromOverTimeToolStripMenuItem_DoubleClick(object sender, EventArgs e)
		{
		}

		private void createFromOverTimeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet approvedList = Factory.OverTimeEntrySystem.GetApprovedList();
			DataSet dataSet = new DataSet();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = approvedList;
			selectDocumentDialog.Text = "Select Over Time Entry";
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.ValidateSelection += formSelectPO_ValidateSelection;
			DialogResult num = selectDocumentDialog.ShowDialog(this);
			string text = "";
			string text2 = "";
			if (num == DialogResult.OK)
			{
				ClearForm();
				DateTime dateTime = default(DateTime);
				DateTime endDate = default(DateTime);
				int num2 = 0;
				int num3 = 0;
				List<string> list = new List<string>();
				string employeeIDs = "";
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					text = selectedRow.Cells["SysDocID"].Value.ToString();
					text2 = selectedRow.Cells["VoucherID"].Value.ToString();
					OverTimeEntryData overTimeEntryByID = Factory.OverTimeEntrySystem.GetOverTimeEntryByID(text, text2);
					num2 = int.Parse(overTimeEntryByID.Tables[0].Rows[0]["Month"].ToString());
					num3 = int.Parse(overTimeEntryByID.Tables[0].Rows[0]["Year"].ToString());
					comboBoxMonth.SelectedID = num2;
					comboBoxYear.Text = num3.ToString();
					dateTime = new DateTime(num3, num2, 1);
					endDate = dateTime.AddMonths(1).AddDays(-1.0);
					foreach (DataRow row in overTimeEntryByID.Tables[1].Rows)
					{
						string item = row["EmployeeID"].ToString();
						row["EmployeeName"].ToString();
						list.Add(item);
					}
					employeeIDs = "'" + string.Join("','", list) + "'";
				}
				if (dateTime != DateTime.MinValue)
				{
					dataSet = Factory.SalarySheetSystem.CalculateSalarySheet("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", dateTime, endDate, num3, num2, employeeIDs);
				}
				if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
				{
					FillGridData(dataSet, IsReCalculate);
					ReCalculateSalary(allRows: true);
				}
				toolStripButtonCreateSIF.Enabled = false;
			}
		}

		private void formSelectPO_ValidateSelection(object sender, EventArgs e)
		{
			SelectDocumentDialog selectDocumentDialog = sender as SelectDocumentDialog;
			List<UltraGridRow> selectedRows = selectDocumentDialog.SelectedRows;
			if (selectedRows != null && selectedRows.Count != 0)
			{
				foreach (UltraGridRow item in selectedRows)
				{
					int num = int.Parse(item.Cells["Month"].Value.ToString());
					int num2 = int.Parse(item.Cells["Year"].Value.ToString());
					DateTime.Parse(selectedRows[0].Cells["ApprovalDate"].Value.ToString());
					int num3 = int.Parse(selectedRows[0].Cells["Month"].Value.ToString());
					int num4 = int.Parse(selectedRows[0].Cells["Year"].Value.ToString());
					if (num != num3 || num4 != num2)
					{
						ErrorHelper.ErrorMessage("Please select Valid Overtime");
						selectDocumentDialog.CanClose = false;
						break;
					}
				}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.SalarySheetForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			saveAsDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			createFromOverTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			Preview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrintDetails = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonCreateSIF = new System.Windows.Forms.ToolStripButton();
			toolStripButtonApproval = new System.Windows.Forms.ToolStripButton();
			toolStripLabelApproval = new System.Windows.Forms.ToolStripLabel();
			toolStripSeparatorApproval = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			label1 = new System.Windows.Forms.Label();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxRef = new System.Windows.Forms.TextBox();
			panelDetails = new System.Windows.Forms.Panel();
			comboBoxYear = new System.Windows.Forms.ComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			comboBoxMonth = new Micromind.DataControls.MonthComboBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			textBoxNote = new System.Windows.Forms.TextBox();
			textBoxDescription = new System.Windows.Forms.TextBox();
			textBoxTotalDays = new System.Windows.Forms.TextBox();
			dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
			dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			buttonCalculate = new Micromind.UISupport.XPButton();
			comboBoxPayrollItemDeduction = new Micromind.DataControls.PayrollItemComboBox();
			comboBoxGridEmployeeLoan = new Micromind.DataControls.EmployeeLoanComboBox();
			comboBoxPayrollItemPayment = new Micromind.DataControls.PayrollItemComboBox();
			comboBoxGridEmployee = new Micromind.DataControls.EmployeeComboBox();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			labelTotal = new Infragistics.Win.Misc.UltraLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItemDeduction).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployeeLoan).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItemPayment).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[23]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator7,
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator1,
				toolStripDropDownButton1,
				toolStripSeparator4,
				toolStripButton1,
				Preview,
				toolStripSeparator5,
				toolStripButtonPrintDetails,
				toolStripSeparator6,
				toolStripButtonDistribution,
				toolStripButtonCreateSIF,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(786, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonPrint.Image");
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
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
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				duplicateToolStripMenuItem,
				toolStripSeparator2,
				saveAsDraftToolStripMenuItem,
				loadDraftToolStripMenuItem,
				toolStripSeparator8,
				createFromOverTimeToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			duplicateToolStripMenuItem.Text = "Duplicate";
			duplicateToolStripMenuItem.Visible = false;
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
			saveAsDraftToolStripMenuItem.Name = "saveAsDraftToolStripMenuItem";
			saveAsDraftToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			saveAsDraftToolStripMenuItem.Text = "Save as Draft";
			saveAsDraftToolStripMenuItem.Click += new System.EventHandler(saveAsDraftToolStripMenuItem_Click);
			loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
			loadDraftToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			loadDraftToolStripMenuItem.Text = "Load Draft...";
			loadDraftToolStripMenuItem.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
			toolStripSeparator8.Name = "toolStripSeparator8";
			toolStripSeparator8.Size = new System.Drawing.Size(192, 6);
			createFromOverTimeToolStripMenuItem.Name = "createFromOverTimeToolStripMenuItem";
			createFromOverTimeToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			createFromOverTimeToolStripMenuItem.Text = "Create from Over Time";
			createFromOverTimeToolStripMenuItem.Click += new System.EventHandler(createFromOverTimeToolStripMenuItem_Click);
			createFromOverTimeToolStripMenuItem.DoubleClick += new System.EventHandler(createFromOverTimeToolStripMenuItem_DoubleClick);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(28, 28);
			toolStripButton1.Text = "Print";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click_1);
			Preview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			Preview.Image = Micromind.ClientUI.Properties.Resources.preview;
			Preview.ImageTransparentColor = System.Drawing.Color.Magenta;
			Preview.Name = "Preview";
			Preview.Size = new System.Drawing.Size(28, 28);
			Preview.Text = "Preview";
			Preview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrintDetails.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonPrintDetails.Image");
			toolStripButtonPrintDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrintDetails.Name = "toolStripButtonPrintDetails";
			toolStripButtonPrintDetails.Size = new System.Drawing.Size(124, 28);
			toolStripButtonPrintDetails.Text = "Print with Details";
			toolStripButtonPrintDetails.Click += new System.EventHandler(toolStripButtonPrintDetails_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonCreateSIF.Enabled = false;
			toolStripButtonCreateSIF.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonCreateSIF.Image");
			toolStripButtonCreateSIF.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonCreateSIF.Name = "toolStripButtonCreateSIF";
			toolStripButtonCreateSIF.Size = new System.Drawing.Size(87, 28);
			toolStripButtonCreateSIF.Text = "Create SIF";
			toolStripButtonCreateSIF.Click += new System.EventHandler(toolStripButtonCreateSIF_Click);
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
			toolStripLabelApproval.Size = new System.Drawing.Size(45, 15);
			toolStripLabelApproval.Text = "Status:";
			toolStripSeparatorApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripSeparatorApproval.Name = "toolStripSeparatorApproval";
			toolStripSeparatorApproval.Size = new System.Drawing.Size(6, 27);
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
			panelButtons.Location = new System.Drawing.Point(0, 574);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(786, 40);
			panelButtons.TabIndex = 3;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(216, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 24);
			buttonVoid.TabIndex = 2;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(318, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 3;
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
			buttonNew.TabIndex = 1;
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
			linePanelDown.Size = new System.Drawing.Size(786, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(676, 8);
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
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(505, 1);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(110, 20);
			dateTimePickerDate.TabIndex = 0;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(420, 25);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(420, 4);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(75, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Sheet Date:";
			textBoxRef.Location = new System.Drawing.Point(505, 23);
			textBoxRef.MaxLength = 15;
			textBoxRef.Name = "textBoxRef";
			textBoxRef.Size = new System.Drawing.Size(133, 20);
			textBoxRef.TabIndex = 1;
			panelDetails.Controls.Add(comboBoxYear);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(comboBoxMonth);
			panelDetails.Controls.Add(mmLabel3);
			panelDetails.Controls.Add(mmLabel5);
			panelDetails.Controls.Add(mmLabel4);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(textBoxDescription);
			panelDetails.Controls.Add(textBoxRef);
			panelDetails.Controls.Add(textBoxTotalDays);
			panelDetails.Controls.Add(dateTimePickerEndDate);
			panelDetails.Controls.Add(dateTimePickerStartDate);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(659, 121);
			panelDetails.TabIndex = 0;
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
			comboBoxYear.Location = new System.Drawing.Point(200, 45);
			comboBoxYear.Name = "comboBoxYear";
			comboBoxYear.Size = new System.Drawing.Size(77, 21);
			comboBoxYear.TabIndex = 25;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(13, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 24;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(89, 1);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(107, 20);
			comboBoxSysDoc.TabIndex = 22;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance15.FontData.BoldAsString = "True";
			appearance15.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance15;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(200, 4);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(88, 15);
			ultraFormattedLinkLabel2.TabIndex = 21;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Sheet Number:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance16;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked_1);
			textBoxVoucherNumber.Location = new System.Drawing.Point(295, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(119, 20);
			textBoxVoucherNumber.TabIndex = 23;
			comboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMonth.FormattingEnabled = true;
			comboBoxMonth.IsMonthNumbers = false;
			comboBoxMonth.Location = new System.Drawing.Point(89, 45);
			comboBoxMonth.Name = "comboBoxMonth";
			comboBoxMonth.Size = new System.Drawing.Size(107, 21);
			comboBoxMonth.TabIndex = 3;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(200, 71);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(64, 13);
			mmLabel3.TabIndex = 2;
			mmLabel3.Text = "End Date:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = true;
			mmLabel5.Location = new System.Drawing.Point(10, 27);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(75, 13);
			mmLabel5.TabIndex = 2;
			mmLabel5.Text = "Description:";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(10, 48);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(47, 13);
			mmLabel4.TabIndex = 2;
			mmLabel4.Text = "Period:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(10, 71);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(69, 13);
			mmLabel2.TabIndex = 2;
			mmLabel2.Text = "Start Date:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(420, 72);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(61, 13);
			label3.TabIndex = 20;
			label3.Text = "Total Days:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(10, 93);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 20;
			label4.Text = "Note:";
			textBoxNote.Location = new System.Drawing.Point(89, 90);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(549, 20);
			textBoxNote.TabIndex = 6;
			textBoxDescription.Location = new System.Drawing.Point(89, 23);
			textBoxDescription.MaxLength = 100;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.Size = new System.Drawing.Size(325, 20);
			textBoxDescription.TabIndex = 2;
			textBoxTotalDays.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalDays.ForeColor = System.Drawing.Color.Black;
			textBoxTotalDays.Location = new System.Drawing.Point(484, 68);
			textBoxTotalDays.MaxLength = 15;
			textBoxTotalDays.Name = "textBoxTotalDays";
			textBoxTotalDays.ReadOnly = true;
			textBoxTotalDays.Size = new System.Drawing.Size(62, 20);
			textBoxTotalDays.TabIndex = 1;
			textBoxTotalDays.TabStop = false;
			dateTimePickerEndDate.Enabled = false;
			dateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerEndDate.Location = new System.Drawing.Point(287, 68);
			dateTimePickerEndDate.Name = "dateTimePickerEndDate";
			dateTimePickerEndDate.Size = new System.Drawing.Size(127, 20);
			dateTimePickerEndDate.TabIndex = 5;
			dateTimePickerStartDate.Enabled = false;
			dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerStartDate.Location = new System.Drawing.Point(89, 68);
			dateTimePickerStartDate.Name = "dateTimePickerStartDate";
			dateTimePickerStartDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerStartDate.TabIndex = 4;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 487);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(757, 63);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				availableQuantityToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem,
				toolStripSeparator3,
				removeRowToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(185, 140);
			availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
			availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
			availableQuantityToolStripMenuItem.Text = "Available Quantity...";
			salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
			salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
			salesStatisticsToolStripMenuItem.Text = "Sales Statistics...";
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
			itemPicToolStripMenuItem.Text = "Item Photo...";
			itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
			itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
			itemDetailsToolStripMenuItem.Text = "Item Details...";
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
			removeRowToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
			removeRowToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
			removeRowToolStripMenuItem.Text = "Remove Row";
			buttonCalculate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCalculate.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonCalculate.BackColor = System.Drawing.Color.DarkGray;
			buttonCalculate.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCalculate.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCalculate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCalculate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCalculate.Location = new System.Drawing.Point(674, 154);
			buttonCalculate.Name = "buttonCalculate";
			buttonCalculate.Size = new System.Drawing.Size(96, 24);
			buttonCalculate.TabIndex = 1;
			buttonCalculate.Text = "Calculate";
			buttonCalculate.UseVisualStyleBackColor = false;
			buttonCalculate.Click += new System.EventHandler(buttonCalculate_Click);
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
			comboBoxPayrollItemDeduction.Location = new System.Drawing.Point(690, 49);
			comboBoxPayrollItemDeduction.MaxDropDownItems = 12;
			comboBoxPayrollItemDeduction.Name = "comboBoxPayrollItemDeduction";
			comboBoxPayrollItemDeduction.ShowInactiveItems = false;
			comboBoxPayrollItemDeduction.ShowQuickAdd = true;
			comboBoxPayrollItemDeduction.Size = new System.Drawing.Size(80, 20);
			comboBoxPayrollItemDeduction.TabIndex = 125;
			comboBoxPayrollItemDeduction.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPayrollItemDeduction.Visible = false;
			comboBoxGridEmployeeLoan.Assigned = false;
			comboBoxGridEmployeeLoan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridEmployeeLoan.CustomReportFieldName = "";
			comboBoxGridEmployeeLoan.CustomReportKey = "";
			comboBoxGridEmployeeLoan.CustomReportValueType = 1;
			comboBoxGridEmployeeLoan.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridEmployeeLoan.DisplayLayout.Appearance = appearance29;
			comboBoxGridEmployeeLoan.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridEmployeeLoan.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployeeLoan.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployeeLoan.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxGridEmployeeLoan.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployeeLoan.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxGridEmployeeLoan.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridEmployeeLoan.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxGridEmployeeLoan.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridEmployeeLoan.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxGridEmployeeLoan.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridEmployeeLoan.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridEmployeeLoan.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridEmployeeLoan.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridEmployeeLoan.Editable = true;
			comboBoxGridEmployeeLoan.FilterID = "";
			comboBoxGridEmployeeLoan.FilterString = "";
			comboBoxGridEmployeeLoan.HasAllAccount = false;
			comboBoxGridEmployeeLoan.HasCustom = false;
			comboBoxGridEmployeeLoan.IsDataLoaded = false;
			comboBoxGridEmployeeLoan.Location = new System.Drawing.Point(295, 156);
			comboBoxGridEmployeeLoan.MaxDropDownItems = 12;
			comboBoxGridEmployeeLoan.Name = "comboBoxGridEmployeeLoan";
			comboBoxGridEmployeeLoan.SelectedSysDocID = "";
			comboBoxGridEmployeeLoan.ShowInactiveItems = false;
			comboBoxGridEmployeeLoan.ShowQuickAdd = true;
			comboBoxGridEmployeeLoan.Size = new System.Drawing.Size(129, 20);
			comboBoxGridEmployeeLoan.TabIndex = 124;
			comboBoxGridEmployeeLoan.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridEmployeeLoan.Visible = false;
			comboBoxPayrollItemPayment.Assigned = false;
			comboBoxPayrollItemPayment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayrollItemPayment.CustomReportFieldName = "";
			comboBoxPayrollItemPayment.CustomReportKey = "";
			comboBoxPayrollItemPayment.CustomReportValueType = 1;
			comboBoxPayrollItemPayment.DescriptionTextBox = null;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayrollItemPayment.DisplayLayout.Appearance = appearance41;
			comboBoxPayrollItemPayment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayrollItemPayment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItemPayment.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItemPayment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxPayrollItemPayment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayrollItemPayment.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxPayrollItemPayment.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayrollItemPayment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayrollItemPayment.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayrollItemPayment.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxPayrollItemPayment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayrollItemPayment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItemPayment.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayrollItemPayment.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxPayrollItemPayment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayrollItemPayment.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayrollItemPayment.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxPayrollItemPayment.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxPayrollItemPayment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayrollItemPayment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayrollItemPayment.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxPayrollItemPayment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayrollItemPayment.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxPayrollItemPayment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayrollItemPayment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayrollItemPayment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayrollItemPayment.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayrollItemPayment.Editable = true;
			comboBoxPayrollItemPayment.FilterString = "";
			comboBoxPayrollItemPayment.HasAllAccount = false;
			comboBoxPayrollItemPayment.HasCustom = false;
			comboBoxPayrollItemPayment.IsDataLoaded = false;
			comboBoxPayrollItemPayment.IsDeduction = false;
			comboBoxPayrollItemPayment.Location = new System.Drawing.Point(690, 72);
			comboBoxPayrollItemPayment.MaxDropDownItems = 12;
			comboBoxPayrollItemPayment.Name = "comboBoxPayrollItemPayment";
			comboBoxPayrollItemPayment.ShowInactiveItems = false;
			comboBoxPayrollItemPayment.ShowQuickAdd = true;
			comboBoxPayrollItemPayment.Size = new System.Drawing.Size(80, 20);
			comboBoxPayrollItemPayment.TabIndex = 118;
			comboBoxPayrollItemPayment.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPayrollItemPayment.Visible = false;
			comboBoxGridEmployee.Assigned = false;
			comboBoxGridEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridEmployee.CustomReportFieldName = "";
			comboBoxGridEmployee.CustomReportKey = "";
			comboBoxGridEmployee.CustomReportValueType = 1;
			comboBoxGridEmployee.DescriptionTextBox = null;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridEmployee.DisplayLayout.Appearance = appearance53;
			comboBoxGridEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxGridEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridEmployee.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxGridEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxGridEmployee.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridEmployee.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxGridEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxGridEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridEmployee.Editable = true;
			comboBoxGridEmployee.FilterString = "";
			comboBoxGridEmployee.HasAllAccount = false;
			comboBoxGridEmployee.HasCustom = false;
			comboBoxGridEmployee.IsDataLoaded = false;
			comboBoxGridEmployee.Location = new System.Drawing.Point(690, 25);
			comboBoxGridEmployee.MaxDropDownItems = 12;
			comboBoxGridEmployee.Name = "comboBoxGridEmployee";
			comboBoxGridEmployee.ShowInactiveItems = false;
			comboBoxGridEmployee.ShowQuickAdd = true;
			comboBoxGridEmployee.ShowTerminatedEmployees = true;
			comboBoxGridEmployee.Size = new System.Drawing.Size(80, 20);
			comboBoxGridEmployee.TabIndex = 22;
			comboBoxGridEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridEmployee.Visible = false;
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
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance65;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance66;
			appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance67;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance68.BackColor2 = System.Drawing.SystemColors.Control;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance68;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance69;
			appearance70.BackColor = System.Drawing.SystemColors.Highlight;
			appearance70.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance70;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance71;
			appearance72.BorderColor = System.Drawing.Color.Silver;
			appearance72.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance72;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance73.BackColor = System.Drawing.SystemColors.Control;
			appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance73.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance73;
			appearance74.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance74;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance75;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance76;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 182);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(758, 368);
			dataGridItems.TabIndex = 2;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(dataGridItems_InitializeRow);
			dataGridItems.BeforeColumnChooserDisplayed += new Infragistics.Win.UltraWinGrid.BeforeColumnChooserDisplayedEventHandler(dataGridItems_BeforeColumnChooserDisplayed);
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance77.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance77.FontData.BoldAsString = "True";
			appearance77.FontData.Name = "Tahoma";
			appearance77.TextHAlignAsString = "Right";
			appearance77.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance77;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(12, 553);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(646, 16);
			ultraLabel1.TabIndex = 126;
			ultraLabel1.Text = "Total:";
			ultraLabel1.Visible = false;
			labelTotal.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			appearance78.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance78.FontData.BoldAsString = "True";
			appearance78.FontData.Name = "Tahoma";
			appearance78.TextHAlignAsString = "Right";
			appearance78.TextVAlignAsString = "Middle";
			labelTotal.Appearance = appearance78;
			labelTotal.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			labelTotal.Location = new System.Drawing.Point(659, 553);
			labelTotal.Name = "labelTotal";
			labelTotal.Size = new System.Drawing.Size(111, 16);
			labelTotal.TabIndex = 127;
			labelTotal.Text = "0";
			labelTotal.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(786, 614);
			base.Controls.Add(labelTotal);
			base.Controls.Add(ultraLabel1);
			base.Controls.Add(comboBoxPayrollItemDeduction);
			base.Controls.Add(comboBoxGridEmployeeLoan);
			base.Controls.Add(buttonCalculate);
			base.Controls.Add(comboBoxPayrollItemPayment);
			base.Controls.Add(comboBoxGridEmployee);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(dataGridItems);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(648, 396);
			base.Name = "SalarySheetForm";
			Text = "Salary Sheet";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItemDeduction).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployeeLoan).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItemPayment).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
