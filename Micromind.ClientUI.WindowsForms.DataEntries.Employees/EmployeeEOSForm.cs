using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class EmployeeEOSForm : Form, IForm
	{
		private EmployeeEOSSettlementData currentData;

		private const string TABLENAME_CONST = "Employee_EOS";

		private const string IDFIELD_CONST = "VoucherID";

		private int currDecimalPoint = Global.CurDecimalPoints;

		private bool isNewRecord = true;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

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

		private DataEntryGrid dataGridItem;

		private PayrollItemComboBox comboBoxPayrollItem;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl tabPageUserDefined;

		private CurrencyComboBox comboBoxCurrency;

		private DataEntryGrid dataGridDeduction;

		private BenefitComboBox comboBoxBenefit;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private PayrollItemComboBox comboBoxDeduction;

		private XPButton buttonClear;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonAttach;

		private MMTextBox textBoxEmployeeName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private EmployeeComboBox comboBoxEmployees;

		private DestinationComboBox comboBoxDestination;

		private SysDocComboBox comboBoxSysDoc;

		private TextBox textBoxVoucherNumber;

		private DateTimePicker dateTimePickerDate;

		private MMLabel LabelPeriod;

		private EmployeeComboBox comboBoxEmployee;

		private Label label3;

		private TextBox textBoxNote;

		private TextBox textBoxBasic;

		private Label label1;

		private MMTextBox textBoxConfirmation;

		private MMTextBox textBoxJoining;

		private MMTextBox textBoxLabourID;

		private MMLabel mmLabel38;

		private MMLabel mmLabel10;

		private MMLabel mmLabel9;

		private MMTextBox textBoxDepartment;

		private MMLabel mmLabel8;

		private MMTextBox textBoxDivision;

		private MMLabel mmLabel7;

		private MMTextBox textBoxLocation;

		private MMLabel mmLabel6;

		private MMTextBox textBoxGender;

		private MMLabel mmLabel3;

		private MMLabel lblDescriptions;

		private MMTextBox textBoxDesignation;

		private DateTimePicker dateTimePickerLastWorking;

		private MMLabel mmLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private AmountTextBox textBoxTotal;

		private Label label2;

		private AmountTextBox textBoxTotalDeduction;

		private Label label7;

		private AmountTextBox textBoxTotalLoan;

		private Label label4;

		private DataGridList dataGridLoan;

		private XPButton xpButtonDelete;

		private ToolStripButton toolStripButtonDistribution;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private Button buttonRefresh;

		private RadioButton radioButtonResigned;

		private MMLabel mmLabel2;

		private RadioButton radioButtonTerminated;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5017;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

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
					xpButtonDelete.Enabled = false;
					EmployeeComboBox employeeComboBox = comboBoxEmployee;
					enabled = (comboBoxSysDoc.Enabled = true);
					employeeComboBox.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = false;
				}
				else
				{
					xpButtonDelete.Enabled = true;
					comboBoxEmployee.Enabled = false;
					comboBoxSysDoc.Enabled = false;
					textBoxVoucherNumber.ReadOnly = true;
				}
				ToolStripButton toolStripButton = toolStripButton1;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
				toolStripButtonAttach.Enabled = !value;
				toolStripButtonDistribution.Enabled = !value;
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
			}
		}

		private bool IsDirty => formManager.GetDirtyStatus();

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		public EmployeeEOSForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EmployeeEOSForm_Load;
			comboBoxEmployee.SelectedIndexChanged += comboBoxEmployee_SelectedIndexChanged;
			dataGridDeduction.AfterCellUpdate += dataGridDeduction_AfterCellUpdate;
			dataGridItem.AfterCellUpdate += dataGridItem_AfterCellUpdate;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
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

		private void dataGridDeduction_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Deduction")
			{
				dataGridDeduction.ActiveRow.Cells["Description"].Value = comboBoxDeduction.SelectedName;
			}
			if (e.Cell.Column.Key == "Amount")
			{
				decimal num = default(decimal);
				foreach (UltraGridRow row in dataGridDeduction.Rows)
				{
					num += decimal.Parse(row.Cells["Amount"].Value.ToString());
				}
				textBoxTotalDeduction.Text = num.ToString();
				UltraGridRow ultraGridRow = dataGridItem.Rows[5];
				ultraGridRow.Cells["CalculatedValue"].Value = textBoxTotalDeduction.Text;
				ultraGridRow.Cells["PaidValue"].Value = textBoxTotalDeduction.Text;
				RefreshGridTotal();
			}
		}

		private void dataGridItem_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "PaidValue")
			{
				RefreshGridTotal();
			}
			if (!(e.Cell.Column.Key == "BasedOn1"))
			{
				return;
			}
			DataTable dataTable = dataGridItem.DataSource as DataTable;
			if (e.Cell.Row.Index == 0)
			{
				bool flag = true;
				flag = (CompanyPreferences.Annual ? true : false);
				DataTable dataTable2 = Factory.EmployeeEOSSettlementSystem.GetEmployeeFinalSettlement(comboBoxEmployee.SelectedID, dateTimePickerLastWorking.Value, flag).Tables[1];
				if (dataTable2.Rows.Count > 0)
				{
					decimal result = default(decimal);
					decimal.TryParse(e.Cell.Value.ToString(), out result);
					decimal d = decimal.Parse(dataTable2.Rows[0]["DailySalary2"].ToString());
					decimal num = result * d;
					DataRow dataRow = dataTable.Rows[0];
					dataRow.BeginEdit();
					dataRow["PaidValue"] = num;
					dataRow.EndEdit();
				}
			}
			else if (e.Cell.Row.Index == 1)
			{
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(textBoxBasic.Text, out result2);
				num3 = result2 / 30m;
				decimal result3 = default(decimal);
				decimal.TryParse(e.Cell.Value.ToString(), out result3);
				DataSet employeeEOSRule = Factory.EmployeeEOSSettlementSystem.GetEmployeeEOSRule(comboBoxEmployee.SelectedID, radioButtonResigned.Checked);
				if (employeeEOSRule.Tables["EOSRule"].Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("EOS Settings in Employee Class has not Activated.");
					ClearGrid();
					return;
				}
				DataTable dataTable3 = employeeEOSRule.Tables["EOSRule"];
				try
				{
					if (result3 < (decimal)int.Parse(dataTable3.Rows[0]["YearFrom"].ToString()))
					{
						num2 = default(decimal);
					}
					else if (result3 >= (decimal)int.Parse(dataTable3.Rows[0]["YearFrom"].ToString()) && result3 < (decimal)int.Parse(dataTable3.Rows[0]["YearTo"].ToString()))
					{
						num2 = num3 * (decimal)int.Parse(dataTable3.Rows[0]["EOSDay"].ToString()) * result3;
					}
					else if (result3 >= (decimal)int.Parse(dataTable3.Rows[1]["YearFrom"].ToString()) && result3 < (decimal)int.Parse(dataTable3.Rows[1]["YearTo"].ToString()))
					{
						num2 = num3 * (decimal)int.Parse(dataTable3.Rows[1]["EOSDay"].ToString()) * result3;
					}
					else if (result3 >= (decimal)int.Parse(dataTable3.Rows[2]["YearFrom"].ToString()) && result3 < (decimal)int.Parse(dataTable3.Rows[2]["YearTo"].ToString()))
					{
						num2 = num3 * (decimal)int.Parse(dataTable3.Rows[2]["EOSDay"].ToString()) * result3;
					}
					else if (result3 >= (decimal)int.Parse(dataTable3.Rows[3]["YearFrom"].ToString()) && result3 < (decimal)int.Parse(dataTable3.Rows[3]["YearTo"].ToString()))
					{
						num2 = num3 * (decimal)int.Parse(dataTable3.Rows[3]["EOSDay"].ToString()) * result3;
					}
					DataRow dataRow2 = dataTable.Rows[1];
					dataRow2.BeginEdit();
					dataRow2["PaidValue"] = num2;
					dataRow2.EndEdit();
				}
				catch
				{
				}
			}
			RefreshGridTotal();
		}

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (comboBoxEmployee.SelectedID == "")
				{
					(dataGridItem.DataSource as DataTable).Rows.Clear();
					BindConstantData();
					(dataGridDeduction.DataSource as DataTable).Rows.Clear();
					dataGridLoan.DataSource = null;
					textBoxLocation.Clear();
					textBoxDivision.Clear();
					textBoxDepartment.Clear();
					textBoxDesignation.Clear();
					textBoxLabourID.Clear();
					textBoxGender.Clear();
					textBoxJoining.Clear();
					textBoxConfirmation.Clear();
					textBoxBasic.Text = 0.ToString(Format.TotalAmountFormat);
					textBoxNote.Clear();
					textBoxTotalDeduction.Text = 0.ToString(Format.TotalAmountFormat);
					textBoxTotalLoan.Text = 0.ToString(Format.TotalAmountFormat);
					textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				}
				DataSet employeeBriefInfo = Factory.EmployeeEOSSettlementSystem.GetEmployeeBriefInfo(comboBoxEmployee.SelectedID);
				if (employeeBriefInfo != null && employeeBriefInfo.Tables.Count > 0 && employeeBriefInfo.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = employeeBriefInfo.Tables[0].Rows[0];
					textBoxLocation.Text = dataRow["LocationID"].ToString();
					textBoxDivision.Text = dataRow["DivisionName"].ToString();
					textBoxDepartment.Text = dataRow["DepartmentName"].ToString();
					textBoxDesignation.Text = dataRow["PositionName"].ToString();
					textBoxLabourID.Text = dataRow["LabourID"].ToString();
					if (dataRow["Gender"] != DBNull.Value && dataRow["Gender"].ToString() != string.Empty)
					{
						if (dataRow["Gender"].ToString() == "M")
						{
							textBoxGender.Text = "MALE";
						}
						else
						{
							textBoxGender.Text = "FEMALE";
						}
					}
					if (dataRow["JoiningDate"] != DBNull.Value)
					{
						textBoxJoining.Text = DateTime.Parse(dataRow["JoiningDate"].ToString()).ToShortDateString();
					}
					else
					{
						textBoxJoining.Clear();
					}
					if (dataRow["ConfirmationDate"] != DBNull.Value)
					{
						textBoxConfirmation.Text = DateTime.Parse(dataRow["JoiningDate"].ToString()).ToShortDateString();
					}
					else
					{
						textBoxConfirmation.Clear();
					}
					textBoxBasic.Text = decimal.Parse(dataRow["BasicSalary"].ToString()).ToString(Format.TotalAmountFormat);
					CalculateTotal();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EmployeeEOSSettlementData();
				}
				DataRow dataRow;
				if (isNewRecord)
				{
					dataRow = currentData.EmployeeEOSTable.NewRow();
					dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				}
				else
				{
					dataRow = currentData.EmployeeEOSTable.Rows[0];
				}
				dataRow.BeginEdit();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["LastWorkingDate"] = dateTimePickerLastWorking.Value;
				dataRow["EmployeeBasic"] = textBoxBasic.Text;
				if (radioButtonResigned.Checked)
				{
					dataRow["IsResigned"] = true;
				}
				else
				{
					dataRow["IsResigned"] = false;
				}
				dataRow["Note"] = textBoxNote.Text;
				if (textBoxTotal.Text != "")
				{
					dataRow["NetTotal"] = textBoxTotal.Text;
				}
				else
				{
					dataRow["NetTotal"] = 0;
				}
				DataTable obj = dataGridItem.DataSource as DataTable;
				DataRow dataRow2 = obj.Rows[0];
				dataRow["CalculatedLeaveAmount"] = dataRow2["CalculatedValue"].ToString();
				dataRow["LeaveDescription"] = dataRow2["BasedOn1"].ToString();
				dataRow["PaidLeaveAmount"] = dataRow2["PaidValue"].ToString();
				dataRow2 = obj.Rows[1];
				dataRow["CalculatedGratuityAmount"] = dataRow2["CalculatedValue"].ToString();
				dataRow["GratuityDescription"] = dataRow2["BasedOn1"].ToString();
				dataRow["PaidGratuityAmount"] = dataRow2["PaidValue"].ToString();
				dataRow2 = obj.Rows[2];
				dataRow["CalculatedSalaryAmount"] = dataRow2["CalculatedValue"].ToString();
				dataRow["SalaryDescription"] = dataRow2["BasedOn1"].ToString();
				dataRow["PaidSalaryAmount"] = dataRow2["PaidValue"].ToString();
				dataRow2 = obj.Rows[3];
				dataRow["PaidTicketAmount"] = dataRow2["PaidValue"].ToString();
				dataRow2 = obj.Rows[4];
				dataRow["PaidLoanAmount"] = dataRow2["PaidValue"].ToString();
				dataRow2 = obj.Rows[5];
				dataRow["PaidDeductionAmount"] = dataRow2["PaidValue"].ToString();
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeEOSTable.Rows.Add(dataRow);
				}
				currentData.EmployeeEOSDeductionDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridDeduction.Rows)
				{
					DataRow dataRow3 = currentData.EmployeeEOSDeductionDetailTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow3["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow3["DeductionID"] = row.Cells["Deduction"].Value.ToString();
					dataRow3["Description"] = row.Cells["Description"].Value.ToString();
					dataRow3["Amount"] = row.Cells["Amount"].Value.ToString();
					dataRow3.EndEdit();
					currentData.EmployeeEOSDeductionDetailTable.Rows.Add(dataRow3);
				}
				if (currentData.Tables.Contains("Employee_Loan"))
				{
					currentData.Tables.Remove("Employee_Loan");
				}
				DataSet dataSet = dataGridLoan.DataSource as DataSet;
				if (dataSet != null && dataSet.Tables.Count > 0)
				{
					currentData.Tables.Add(dataSet.Tables[0].Copy());
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void SetupDetailsGrid()
		{
			dataGridItem.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable("EOSDetails");
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("BasedOn", typeof(string));
			dataTable.Columns.Add("CalculatedValue", typeof(decimal));
			dataTable.Columns.Add("BasedOn1", typeof(string));
			dataTable.Columns.Add("PaidValue", typeof(decimal));
			dataGridItem.DataSource = dataTable;
			dataGridItem.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridItem.DisplayLayout.Bands[0].Columns["CalculatedValue"].CellActivation = Activation.NoEdit;
			dataGridItem.DisplayLayout.Bands[0].Columns["BasedOn"].CellActivation = Activation.NoEdit;
			dataGridItem.DisplayLayout.Bands[0].Columns["CalculatedValue"].Header.Caption = "Calculated Value";
			dataGridItem.DisplayLayout.Bands[0].Columns["PaidValue"].Header.Caption = "Paid Value";
			dataGridItem.DisplayLayout.Bands[0].Columns["BasedOn"].Header.Caption = "Based On";
			dataGridItem.DisplayLayout.Bands[0].Columns["BasedOn1"].Header.Caption = "BasedOn (Manual)";
			dataGridItem.DisplayLayout.Bands[0].Columns["PaidValue"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItem.DisplayLayout.Bands[0].Columns["PaidValue"].Format = Format.GridAmountFormat;
			dataGridItem.DisplayLayout.Bands[0].Columns["CalculatedValue"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItem.DisplayLayout.Bands[0].Columns["CalculatedValue"].Format = Format.GridAmountFormat;
			dataGridItem.DisplayLayout.Bands[0].Columns["BasedOn1"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItem.DisplayLayout.Bands[0].Columns["BasedOn1"].Format = Format.GridAmountFormat;
			dataGridItem.DisplayLayout.Bands[0].Columns["Description"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridItem.DisplayLayout.Bands[0].Columns["CalculatedValue"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridItem.DisplayLayout.Bands[0].Columns["BasedOn"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridItem.DisplayLayout.Bands[0].Columns["PaidValue"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridItem.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
		}

		private void SetupDeductionGrid()
		{
			dataGridDeduction.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable("Deduction");
			dataTable.Columns.Add("Deduction", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataGridDeduction.DataSource = dataTable;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].CharacterCasing = CharacterCasing.Upper;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].ValueList = comboBoxDeduction;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].Width = checked(25 * dataGridDeduction.Width) / 100;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Description"].Width = checked(50 * dataGridDeduction.Width) / 100;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(15 * dataGridDeduction.Width) / 100;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Description"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGridItem.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.EmployeeEOSSettlementSystem.GetEmployeeEOSByID(SystemDocID, voucherID);
					if (currentData != null)
					{
						ClearForm();
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
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[1].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Employee_EOS"].Rows[0];
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					dateTimePickerLastWorking.Value = DateTime.Parse(dataRow["LastWorkingDate"].ToString());
					textBoxBasic.Text = decimal.Parse(dataRow["EmployeeBasic"].ToString()).ToString(Format.TotalAmountFormat);
					textBoxNote.Text = dataRow["Note"].ToString();
					textBoxTotal.Text = decimal.Parse(dataRow["NetTotal"].ToString()).ToString(Format.TotalAmountFormat);
					if (dataRow["IsResigned"] == DBNull.Value || bool.Parse(dataRow["IsResigned"].ToString()))
					{
						radioButtonResigned.Checked = true;
					}
					else
					{
						radioButtonTerminated.Checked = true;
					}
					CalculateTotal();
					UltraGridRow ultraGridRow = dataGridItem.Rows[0];
					ultraGridRow.Cells["CalculatedValue"].Value = dataRow["CalculatedLeaveAmount"].ToString();
					ultraGridRow.Cells["BasedOn1"].Value = dataRow["LeaveDescription"].ToString();
					ultraGridRow.Cells["PaidValue"].Value = dataRow["PaidLeaveAmount"].ToString();
					UltraGridRow ultraGridRow2 = dataGridItem.Rows[1];
					ultraGridRow2.Cells["CalculatedValue"].Value = dataRow["CalculatedGratuityAmount"].ToString();
					ultraGridRow2.Cells["BasedOn1"].Value = dataRow["GratuityDescription"].ToString();
					ultraGridRow2.Cells["PaidValue"].Value = dataRow["PaidGratuityAmount"].ToString();
					UltraGridRow ultraGridRow3 = dataGridItem.Rows[2];
					ultraGridRow3.Cells["CalculatedValue"].Value = dataRow["CalculatedSalaryAmount"].ToString();
					ultraGridRow3.Cells["BasedOn1"].Value = dataRow["SalaryDescription"].ToString();
					ultraGridRow3.Cells["PaidValue"].Value = dataRow["PaidSalaryAmount"].ToString();
					dataGridItem.Rows[3].Cells["PaidValue"].Value = dataRow["PaidTicketAmount"].ToString();
					dataGridItem.Rows[4].Cells["PaidValue"].Value = dataRow["PaidLoanAmount"].ToString();
					dataGridItem.Rows[5].Cells["PaidValue"].Value = dataRow["PaidDeductionAmount"].ToString();
					DataTable dataTable = dataGridDeduction.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.EmployeeEOSDeductionDetailTable.Rows.Count > 0)
					{
						foreach (DataRow row in currentData.Tables["Employee_EOS_Deduction_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3.BeginEdit();
							dataRow3["Deduction"] = row["DeductionID"];
							dataRow3["Description"] = row["Description"];
							dataRow3["Amount"] = row["Amount"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
					}
					DataSet employeePendingLoanList = Factory.EmployeeLoanSystem.GetEmployeePendingLoanList(comboBoxEmployee.SelectedID);
					dataGridLoan.DataSource = employeePendingLoanList;
					SetupListGrid();
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
				bool flag = false;
				flag = Factory.EmployeeEOSSettlementSystem.CreateEmployeeEOS(currentData, !IsNewRecord);
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
			if (comboBoxEmployee.SelectedID == "" || comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			return true;
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

		private void BindConstantData()
		{
			DataTable dataTable = dataGridItem.DataSource as DataTable;
			dataTable.Rows.Clear();
			dataTable.Rows.Add("Balance Leave Amount", "", 0, "", 0);
			dataTable.Rows.Add("Gratuity Amount", "", 0, "", 0);
			dataTable.Rows.Add("Salary Amount", "", 0, "", 0);
			dataTable.Rows.Add("Ticket Amount", "", 0, "", 0);
			dataTable.Rows.Add("Loan Amount Deduction", "", 0, "", 0);
			dataTable.Rows.Add("Other Deduction", "", 0, "", 0);
			dataGridItem.DataSource = dataTable;
			dataGridItem.Rows[4].Cells["PaidValue"].Appearance.ForeColor = Color.Red;
			dataGridItem.Rows[5].Cells["PaidValue"].Appearance.ForeColor = Color.Red;
			foreach (UltraGridRow row in dataGridItem.Rows)
			{
				if (row.Cells["Description"].Value.ToString() == "Loan Amount Deduction" || row.Cells["Description"].Value.ToString() == "Other Deduction")
				{
					Activation activation3 = row.Cells["BasedOn1"].Activation = (row.Cells["PaidValue"].Activation = Activation.NoEdit);
				}
			}
		}

		private void ClearForm()
		{
			(dataGridItem.DataSource as DataTable).Rows.Clear();
			BindConstantData();
			(dataGridDeduction.DataSource as DataTable).Rows.Clear();
			dataGridLoan.DataSource = null;
			textBoxLocation.Clear();
			textBoxDivision.Clear();
			textBoxDepartment.Clear();
			textBoxDesignation.Clear();
			textBoxLabourID.Clear();
			textBoxGender.Clear();
			textBoxJoining.Clear();
			textBoxConfirmation.Clear();
			textBoxBasic.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxNote.Clear();
			textBoxTotalDeduction.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxTotalLoan.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
			comboBoxEmployee.Clear();
			comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
			IsNewRecord = true;
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
			comboBoxCurrency.Clear();
			comboBoxDestination.Clear();
			radioButtonResigned.Checked = true;
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
				return Factory.EmployeeEOSSettlementSystem.DeleteEOS(SystemDocID, textBoxVoucherNumber.Text, comboBoxEmployee.SelectedID);
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
			string nextID = DatabaseHelper.GetNextID("Employee_EOS", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Employee_EOS", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Employee_EOS", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Employee_EOS", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Employee_EOS", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void EmployeeEOSForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void EmployeeEOSForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItem.SetupUI();
				dataGridDeduction.SetupUI();
				SetupListGrid();
				SetupDetailsGrid();
				SetupDeductionGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.EmployeeEOS);
				SetSecurity();
				if (!base.IsDisposed)
				{
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataGridLoan.LoadLayoutFailed = true;
				dataGridDeduction.LoadLayoutFailed = true;
				dataGridItem.LoadLayoutFailed = true;
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
			if (comboBoxEmployees.SelectedRow != null)
			{
				_ = (comboBoxEmployees.SelectedID != "");
			}
		}

		private void textBoxBasic_TextChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(comboBoxCurrency.SelectedID);
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			if (CanClose())
			{
				ClearForm();
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeEOSListFormObj);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				string selectedID = comboBoxSysDoc.SelectedID;
				string text = textBoxVoucherNumber.Text;
				DataSet employeeEOSToPrint = Factory.EmployeeEOSSettlementSystem.GetEmployeeEOSToPrint(selectedID, text);
				if (employeeEOSToPrint == null || employeeEOSToPrint.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(employeeEOSToPrint, selectedID, "EOS Settlement", SysDocTypes.EmployeeEOS, isPrint, showPrintDialog);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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

		private void SetupListGrid()
		{
			dataGridLoan.ApplyUIDesign();
			dataGridLoan.ApplyFormat();
		}

		private void CalculateTotal()
		{
			if (comboBoxEmployee.SelectedID == "")
			{
				return;
			}
			DataTable dataTable = dataGridItem.DataSource as DataTable;
			bool flag = true;
			flag = (CompanyPreferences.Annual ? true : false);
			DataSet employeeFinalSettlement = Factory.EmployeeEOSSettlementSystem.GetEmployeeFinalSettlement(comboBoxEmployee.SelectedID, dateTimePickerLastWorking.Value, flag);
			DataSet employeeEOSRule = Factory.EmployeeEOSSettlementSystem.GetEmployeeEOSRule(comboBoxEmployee.SelectedID, radioButtonResigned.Checked);
			if (employeeEOSRule.Tables["EOSRule"].Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("EOS Settings in Employee Class has not Activated.");
				ClearGrid();
				return;
			}
			DataTable dataTable2 = employeeFinalSettlement.Tables[1];
			if (dataTable2.Rows.Count > 0)
			{
				object obj = dataTable2.Compute("Sum(LeavesRemaining)", "IsAnnual='True'");
				decimal d = decimal.Parse(dataTable2.Rows[0]["DailySalary2"].ToString());
				decimal num = Convert.ToDecimal(obj) * d;
				DataRow dataRow = dataTable.Rows[0];
				dataRow.BeginEdit();
				dataRow["CalculatedValue"] = num;
				dataRow["BasedOn"] = obj.ToString() + " (Days)";
				dataRow["BasedOn1"] = obj.ToString();
				dataRow["PaidValue"] = num;
				dataRow.EndEdit();
			}
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal result = default(decimal);
			decimal.TryParse(textBoxBasic.Text, out result);
			num3 = result / 30m;
			decimal num4 = Math.Round(decimal.Parse(((dateTimePickerLastWorking.Value - DateTime.Parse(textBoxJoining.Text)).TotalDays / 365.0).ToString()), 2);
			DataTable dataTable3 = employeeEOSRule.Tables["EOSRule"];
			try
			{
				decimal d2 = default(decimal);
				decimal d3 = default(decimal);
				foreach (DataRow row in dataTable3.Rows)
				{
					if (num4 >= (decimal)int.Parse(row["YearFrom"].ToString()) && num4 < (decimal)int.Parse(row["YearTo"].ToString()))
					{
						d2 += (num4 - d3) * (decimal)int.Parse(row["EOSDay"].ToString());
					}
					else if (num4 >= (decimal)int.Parse(row["YearFrom"].ToString()) && num4 > (decimal)int.Parse(row["YearTo"].ToString()))
					{
						d3 = int.Parse(row["YearTo"].ToString());
						d2 += (decimal)checked(int.Parse(row["YearTo"].ToString()) * int.Parse(row["EOSDay"].ToString()));
					}
				}
				num2 = num3 * d2;
				DataRow dataRow3 = dataTable.Rows[1];
				dataRow3.BeginEdit();
				dataRow3["CalculatedValue"] = num2;
				dataRow3["BasedOn"] = Math.Round(num4, 2) + " (Service Period)";
				dataRow3["BasedOn1"] = Math.Round(num4, 2);
				dataRow3["PaidValue"] = num2;
				dataRow3.EndEdit();
			}
			catch
			{
				ErrorHelper.ErrorMessage("EOS not implement for this working year");
			}
			DateTime value = dateTimePickerLastWorking.Value;
			DataSet salaryEmployeeSheetDetails = Factory.SalarySheetSystem.GetSalaryEmployeeSheetDetails(value.Month.ToString(), value.Year.ToString(), comboBoxEmployee.SelectedID);
			if (salaryEmployeeSheetDetails.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow4 = dataTable.Rows[2];
				dataRow4.BeginEdit();
				dataRow4["CalculatedValue"] = salaryEmployeeSheetDetails.Tables[0].Rows[0]["NetSalary"].ToString();
				dataRow4["BasedOn"] = "";
				dataRow4["PaidValue"] = salaryEmployeeSheetDetails.Tables[0].Rows[0]["NetSalary"].ToString();
				dataRow4.EndEdit();
			}
			else
			{
				DataRow dataRow5 = dataTable.Rows[2];
				dataRow5.BeginEdit();
				dataRow5["CalculatedValue"] = 0.ToString(Format.TotalAmountFormat);
				dataRow5["BasedOn"] = "";
				dataRow5["PaidValue"] = 0.ToString(Format.TotalAmountFormat);
				dataRow5.EndEdit();
			}
			DataSet employeePendingLoanList = Factory.EmployeeLoanSystem.GetEmployeePendingLoanList(comboBoxEmployee.SelectedID);
			dataGridLoan.DataSource = employeePendingLoanList;
			SetupListGrid();
			if (employeePendingLoanList.Tables.Contains("Employee_Loan_Sum"))
			{
				textBoxTotalLoan.Text = decimal.Parse(employeePendingLoanList.Tables[1].Rows[0]["Balance"].ToString()).ToString(Format.TotalAmountFormat);
				DataRow dataRow6 = dataTable.Rows[4];
				dataRow6.BeginEdit();
				dataRow6["CalculatedValue"] = textBoxTotalLoan.Text;
				dataRow6["BasedOn"] = "";
				dataRow6["PaidValue"] = textBoxTotalLoan.Text;
				dataRow6.EndEdit();
			}
			RefreshGridTotal();
		}

		private void RefreshGridTotal()
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			decimal num6 = default(decimal);
			decimal d = decimal.Parse(dataGridItem.Rows[0].Cells["PaidValue"].Value.ToString());
			num3 = decimal.Parse(dataGridItem.Rows[1].Cells["PaidValue"].Value.ToString());
			num2 = decimal.Parse(dataGridItem.Rows[2].Cells["PaidValue"].Value.ToString());
			num6 = decimal.Parse(dataGridItem.Rows[3].Cells["PaidValue"].Value.ToString());
			num4 = decimal.Parse(dataGridItem.Rows[4].Cells["PaidValue"].Value.ToString());
			num5 = decimal.Parse(dataGridItem.Rows[5].Cells["PaidValue"].Value.ToString());
			num = d + num3 + num2 + num6 - num4 - num5;
			textBoxTotal.Text = num.ToString(Format.TotalAmountFormat);
		}

		private void xpButton2_Click_1(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.EmployeeEOS);
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
				return Factory.SystemDocumentSystem.GetNextDocumentNumber(SystemDocID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void xpButtonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.EmployeeEOS);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = SystemDocID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			if (comboBoxEmployee.SelectedID != "")
			{
				comboBoxEmployee_SelectedIndexChanged(sender, e);
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void radioButtonResigned_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonResigned.Checked)
			{
				CalculateTotal();
			}
		}

		private void radioButtonTerminated_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonTerminated.Checked)
			{
				CalculateTotal();
			}
		}

		private void ClearGrid()
		{
			(dataGridItem.DataSource as DataTable).Rows.Clear();
			BindConstantData();
			(dataGridDeduction.DataSource as DataTable).Rows.Clear();
			dataGridLoan.DataSource = null;
			textBoxTotal.Clear();
			textBoxTotalDeduction.Clear();
			textBoxTotalLoan.Clear();
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeEOSForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxTotal = new Micromind.UISupport.AmountTextBox();
			label2 = new System.Windows.Forms.Label();
			dataGridItem = new Micromind.DataControls.DataEntryGrid();
			comboBoxPayrollItem = new Micromind.DataControls.PayrollItemComboBox();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxTotalDeduction = new Micromind.UISupport.AmountTextBox();
			label7 = new System.Windows.Forms.Label();
			comboBoxDeduction = new Micromind.DataControls.PayrollItemComboBox();
			dataGridDeduction = new Micromind.DataControls.DataEntryGrid();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridLoan = new Micromind.UISupport.DataGridList(components);
			textBoxTotalLoan = new Micromind.UISupport.AmountTextBox();
			label4 = new System.Windows.Forms.Label();
			comboBoxBenefit = new Micromind.DataControls.BenefitComboBox();
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
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			xpButtonDelete = new Micromind.UISupport.XPButton();
			buttonClear = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			LabelPeriod = new Micromind.UISupport.MMLabel();
			label3 = new System.Windows.Forms.Label();
			textBoxNote = new System.Windows.Forms.TextBox();
			textBoxBasic = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxConfirmation = new Micromind.UISupport.MMTextBox();
			textBoxJoining = new Micromind.UISupport.MMTextBox();
			textBoxLabourID = new Micromind.UISupport.MMTextBox();
			mmLabel38 = new Micromind.UISupport.MMLabel();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxDepartment = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxDivision = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			textBoxLocation = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxGender = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			lblDescriptions = new Micromind.UISupport.MMLabel();
			textBoxDesignation = new Micromind.UISupport.MMTextBox();
			dateTimePickerLastWorking = new System.Windows.Forms.DateTimePicker();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			comboBoxCurrency = new Micromind.DataControls.CurrencyComboBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxEmployees = new Micromind.DataControls.EmployeeComboBox();
			comboBoxDestination = new Micromind.DataControls.DestinationComboBox();
			buttonRefresh = new System.Windows.Forms.Button();
			radioButtonResigned = new System.Windows.Forms.RadioButton();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			radioButtonTerminated = new System.Windows.Forms.RadioButton();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItem).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDeduction).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridDeduction).BeginInit();
			tabPageUserDefined.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridLoan).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBenefit).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployees).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDestination).BeginInit();
			SuspendLayout();
			tabPageGeneral.Controls.Add(textBoxTotal);
			tabPageGeneral.Controls.Add(label2);
			tabPageGeneral.Controls.Add(dataGridItem);
			tabPageGeneral.Controls.Add(comboBoxPayrollItem);
			tabPageGeneral.Location = new System.Drawing.Point(1, 20);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(710, 255);
			textBoxTotal.AllowDecimal = true;
			textBoxTotal.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotal.CustomReportFieldName = "";
			textBoxTotal.CustomReportKey = "";
			textBoxTotal.CustomReportValueType = 1;
			textBoxTotal.ForeColor = System.Drawing.Color.Black;
			textBoxTotal.IsComboTextBox = false;
			textBoxTotal.IsModified = false;
			textBoxTotal.Location = new System.Drawing.Point(583, 229);
			textBoxTotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotal.Name = "textBoxTotal";
			textBoxTotal.NullText = "0";
			textBoxTotal.ReadOnly = true;
			textBoxTotal.Size = new System.Drawing.Size(114, 21);
			textBoxTotal.TabIndex = 28;
			textBoxTotal.TabStop = false;
			textBoxTotal.Text = "0.00";
			textBoxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(505, 232);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(75, 13);
			label2.TabIndex = 29;
			label2.Text = "Total Amount:";
			dataGridItem.AllowAddNew = false;
			dataGridItem.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItem.DisplayLayout.Appearance = appearance;
			dataGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItem.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridItem.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridItem.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItem.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridItem.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridItem.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItem.IncludeLotItems = false;
			dataGridItem.LoadLayoutFailed = false;
			dataGridItem.Location = new System.Drawing.Point(12, 10);
			dataGridItem.Name = "dataGridItem";
			dataGridItem.ShowClearMenu = true;
			dataGridItem.ShowDeleteMenu = true;
			dataGridItem.ShowInsertMenu = true;
			dataGridItem.ShowMoveRowsMenu = true;
			dataGridItem.Size = new System.Drawing.Size(687, 219);
			dataGridItem.TabIndex = 17;
			dataGridItem.Text = "dataEntryGrid1";
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
			comboBoxPayrollItem.Location = new System.Drawing.Point(560, 3);
			comboBoxPayrollItem.MaxDropDownItems = 12;
			comboBoxPayrollItem.Name = "comboBoxPayrollItem";
			comboBoxPayrollItem.ShowInactiveItems = false;
			comboBoxPayrollItem.ShowQuickAdd = true;
			comboBoxPayrollItem.Size = new System.Drawing.Size(81, 21);
			comboBoxPayrollItem.TabIndex = 19;
			comboBoxPayrollItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPayrollItem.Visible = false;
			tabPageDetails.Controls.Add(textBoxTotalDeduction);
			tabPageDetails.Controls.Add(label7);
			tabPageDetails.Controls.Add(comboBoxDeduction);
			tabPageDetails.Controls.Add(dataGridDeduction);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(710, 255);
			textBoxTotalDeduction.AllowDecimal = true;
			textBoxTotalDeduction.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalDeduction.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalDeduction.CustomReportFieldName = "";
			textBoxTotalDeduction.CustomReportKey = "";
			textBoxTotalDeduction.CustomReportValueType = 1;
			textBoxTotalDeduction.ForeColor = System.Drawing.Color.Black;
			textBoxTotalDeduction.IsComboTextBox = false;
			textBoxTotalDeduction.IsModified = false;
			textBoxTotalDeduction.Location = new System.Drawing.Point(588, 232);
			textBoxTotalDeduction.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalDeduction.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalDeduction.Name = "textBoxTotalDeduction";
			textBoxTotalDeduction.NullText = "0";
			textBoxTotalDeduction.ReadOnly = true;
			textBoxTotalDeduction.Size = new System.Drawing.Size(114, 21);
			textBoxTotalDeduction.TabIndex = 29;
			textBoxTotalDeduction.TabStop = false;
			textBoxTotalDeduction.Text = "0.00";
			textBoxTotalDeduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalDeduction.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label7.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(501, 235);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(86, 13);
			label7.TabIndex = 30;
			label7.Text = "Total Deduction:";
			comboBoxDeduction.Assigned = false;
			comboBoxDeduction.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDeduction.CustomReportFieldName = "";
			comboBoxDeduction.CustomReportKey = "";
			comboBoxDeduction.CustomReportValueType = 1;
			comboBoxDeduction.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDeduction.DisplayLayout.Appearance = appearance25;
			comboBoxDeduction.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDeduction.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDeduction.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDeduction.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxDeduction.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDeduction.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxDeduction.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDeduction.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDeduction.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDeduction.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxDeduction.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDeduction.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDeduction.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDeduction.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxDeduction.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDeduction.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDeduction.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxDeduction.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxDeduction.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDeduction.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxDeduction.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxDeduction.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDeduction.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxDeduction.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDeduction.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDeduction.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDeduction.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDeduction.Editable = true;
			comboBoxDeduction.FilterString = "";
			comboBoxDeduction.HasAllAccount = false;
			comboBoxDeduction.HasCustom = false;
			comboBoxDeduction.IsDataLoaded = false;
			comboBoxDeduction.IsDeduction = true;
			comboBoxDeduction.Location = new System.Drawing.Point(440, 3);
			comboBoxDeduction.MaxDropDownItems = 12;
			comboBoxDeduction.Name = "comboBoxDeduction";
			comboBoxDeduction.ShowInactiveItems = false;
			comboBoxDeduction.ShowQuickAdd = true;
			comboBoxDeduction.Size = new System.Drawing.Size(81, 21);
			comboBoxDeduction.TabIndex = 28;
			comboBoxDeduction.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDeduction.Visible = false;
			dataGridDeduction.AllowAddNew = false;
			dataGridDeduction.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridDeduction.DisplayLayout.Appearance = appearance37;
			dataGridDeduction.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridDeduction.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDeduction.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDeduction.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			dataGridDeduction.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridDeduction.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			dataGridDeduction.DisplayLayout.MaxColScrollRegions = 1;
			dataGridDeduction.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridDeduction.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridDeduction.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			dataGridDeduction.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridDeduction.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridDeduction.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			dataGridDeduction.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridDeduction.DisplayLayout.Override.CellAppearance = appearance44;
			dataGridDeduction.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridDeduction.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			dataGridDeduction.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			dataGridDeduction.DisplayLayout.Override.HeaderAppearance = appearance46;
			dataGridDeduction.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridDeduction.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			dataGridDeduction.DisplayLayout.Override.RowAppearance = appearance47;
			dataGridDeduction.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridDeduction.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			dataGridDeduction.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridDeduction.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridDeduction.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridDeduction.IncludeLotItems = false;
			dataGridDeduction.LoadLayoutFailed = false;
			dataGridDeduction.Location = new System.Drawing.Point(12, 10);
			dataGridDeduction.Name = "dataGridDeduction";
			dataGridDeduction.ShowClearMenu = true;
			dataGridDeduction.ShowDeleteMenu = true;
			dataGridDeduction.ShowInsertMenu = true;
			dataGridDeduction.ShowMoveRowsMenu = true;
			dataGridDeduction.Size = new System.Drawing.Size(691, 221);
			dataGridDeduction.TabIndex = 18;
			dataGridDeduction.Text = "dataEntryGrid1";
			tabPageUserDefined.Controls.Add(dataGridLoan);
			tabPageUserDefined.Controls.Add(textBoxTotalLoan);
			tabPageUserDefined.Controls.Add(label4);
			tabPageUserDefined.Controls.Add(comboBoxBenefit);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(710, 255);
			dataGridLoan.AllowUnfittedView = false;
			dataGridLoan.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridLoan.DisplayLayout.Appearance = appearance49;
			dataGridLoan.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridLoan.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			dataGridLoan.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridLoan.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			dataGridLoan.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridLoan.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			dataGridLoan.DisplayLayout.MaxColScrollRegions = 1;
			dataGridLoan.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridLoan.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridLoan.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			dataGridLoan.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridLoan.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			dataGridLoan.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridLoan.DisplayLayout.Override.CellAppearance = appearance56;
			dataGridLoan.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridLoan.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			dataGridLoan.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			dataGridLoan.DisplayLayout.Override.HeaderAppearance = appearance58;
			dataGridLoan.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridLoan.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			dataGridLoan.DisplayLayout.Override.RowAppearance = appearance59;
			dataGridLoan.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridLoan.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			dataGridLoan.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridLoan.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridLoan.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridLoan.LoadLayoutFailed = false;
			dataGridLoan.Location = new System.Drawing.Point(12, 10);
			dataGridLoan.Name = "dataGridLoan";
			dataGridLoan.ShowDeleteMenu = false;
			dataGridLoan.ShowMinusInRed = true;
			dataGridLoan.ShowNewMenu = false;
			dataGridLoan.Size = new System.Drawing.Size(688, 221);
			dataGridLoan.TabIndex = 30;
			dataGridLoan.Text = "dataGridList1";
			textBoxTotalLoan.AllowDecimal = true;
			textBoxTotalLoan.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalLoan.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalLoan.CustomReportFieldName = "";
			textBoxTotalLoan.CustomReportKey = "";
			textBoxTotalLoan.CustomReportValueType = 1;
			textBoxTotalLoan.ForeColor = System.Drawing.Color.Black;
			textBoxTotalLoan.IsComboTextBox = false;
			textBoxTotalLoan.IsModified = false;
			textBoxTotalLoan.Location = new System.Drawing.Point(586, 231);
			textBoxTotalLoan.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalLoan.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalLoan.Name = "textBoxTotalLoan";
			textBoxTotalLoan.NullText = "0";
			textBoxTotalLoan.ReadOnly = true;
			textBoxTotalLoan.Size = new System.Drawing.Size(114, 21);
			textBoxTotalLoan.TabIndex = 28;
			textBoxTotalLoan.TabStop = false;
			textBoxTotalLoan.Text = "0.00";
			textBoxTotalLoan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalLoan.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label4.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(508, 234);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(75, 13);
			label4.TabIndex = 29;
			label4.Text = "Total Amount:";
			comboBoxBenefit.Assigned = false;
			comboBoxBenefit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBenefit.CustomReportFieldName = "";
			comboBoxBenefit.CustomReportKey = "";
			comboBoxBenefit.CustomReportValueType = 1;
			comboBoxBenefit.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBenefit.DisplayLayout.Appearance = appearance61;
			comboBoxBenefit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBenefit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBenefit.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBenefit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxBenefit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBenefit.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxBenefit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBenefit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBenefit.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBenefit.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxBenefit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBenefit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBenefit.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBenefit.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxBenefit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBenefit.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBenefit.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxBenefit.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxBenefit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBenefit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxBenefit.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxBenefit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBenefit.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxBenefit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBenefit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBenefit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBenefit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBenefit.Editable = true;
			comboBoxBenefit.FilterString = "";
			comboBoxBenefit.HasAllAccount = false;
			comboBoxBenefit.HasCustom = false;
			comboBoxBenefit.IsDataLoaded = false;
			comboBoxBenefit.IsNonFinancial = true;
			comboBoxBenefit.Location = new System.Drawing.Point(510, 6);
			comboBoxBenefit.MaxDropDownItems = 12;
			comboBoxBenefit.Name = "comboBoxBenefit";
			comboBoxBenefit.ShowInactiveItems = false;
			comboBoxBenefit.ShowQuickAdd = true;
			comboBoxBenefit.Size = new System.Drawing.Size(92, 21);
			comboBoxBenefit.TabIndex = 19;
			comboBoxBenefit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBenefit.Visible = false;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[18]
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
				toolStripSeparator5,
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButton1,
				toolStripButtonPreview,
				toolStripSeparator2,
				toolStripButtonDistribution,
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
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(60, 28);
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
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(xpButtonDelete);
			panelButtons.Controls.Add(buttonClear);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 588);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(725, 40);
			panelButtons.TabIndex = 8;
			xpButtonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButtonDelete.BackColor = System.Drawing.Color.Silver;
			xpButtonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButtonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButtonDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButtonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			xpButtonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButtonDelete.Location = new System.Drawing.Point(216, 8);
			xpButtonDelete.Name = "xpButtonDelete";
			xpButtonDelete.Size = new System.Drawing.Size(96, 24);
			xpButtonDelete.TabIndex = 15;
			xpButtonDelete.Text = "&Delete";
			xpButtonDelete.UseVisualStyleBackColor = false;
			xpButtonDelete.Click += new System.EventHandler(xpButtonDelete_Click);
			buttonClear.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClear.BackColor = System.Drawing.Color.Silver;
			buttonClear.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClear.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClear.Location = new System.Drawing.Point(114, 8);
			buttonClear.Name = "buttonClear";
			buttonClear.Size = new System.Drawing.Size(96, 24);
			buttonClear.TabIndex = 1;
			buttonClear.Text = "&Clear";
			buttonClear.UseVisualStyleBackColor = false;
			buttonClear.Click += new System.EventHandler(xpButton2_Click);
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
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			ultraTabControl1.Location = new System.Drawing.Point(8, 295);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(712, 276);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControl1.TabIndex = 7;
			appearance73.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance73;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&Details";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Deduction List";
			ultraTab3.TabPage = tabPageUserDefined;
			ultraTab3.Text = "Pending Loan Amount List";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3]
			{
				ultraTab,
				ultraTab2,
				ultraTab3
			});
			ultraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Standard;
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(710, 255);
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(624, 44);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(52, 14);
			ultraFormattedLinkLabel1.TabIndex = 58;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Currency:";
			ultraFormattedLinkLabel1.Visible = false;
			appearance74.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance74;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.ForeColor = System.Drawing.Color.Black;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.IsModified = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(269, 62);
			textBoxEmployeeName.MaxLength = 255;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(347, 20);
			textBoxEmployeeName.TabIndex = 3;
			textBoxEmployeeName.TabStop = false;
			appearance75.FontData.BoldAsString = "True";
			appearance75.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance75;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(9, 66);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(65, 15);
			ultraFormattedLinkLabel5.TabIndex = 4;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Employee :";
			appearance76.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance76;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			textBoxVoucherNumber.Location = new System.Drawing.Point(348, 40);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(136, 20);
			textBoxVoucherNumber.TabIndex = 1;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(537, 40);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(139, 20);
			dateTimePickerDate.TabIndex = 2;
			LabelPeriod.AutoSize = true;
			LabelPeriod.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			LabelPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			LabelPeriod.IsFieldHeader = false;
			LabelPeriod.IsRequired = true;
			LabelPeriod.Location = new System.Drawing.Point(491, 43);
			LabelPeriod.Name = "LabelPeriod";
			LabelPeriod.PenWidth = 1f;
			LabelPeriod.ShowBorder = false;
			LabelPeriod.Size = new System.Drawing.Size(42, 13);
			LabelPeriod.TabIndex = 170;
			LabelPeriod.Text = "Date :";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(8, 218);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(36, 13);
			label3.TabIndex = 175;
			label3.Text = "Note :";
			textBoxNote.Location = new System.Drawing.Point(125, 216);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(491, 63);
			textBoxNote.TabIndex = 6;
			textBoxBasic.Location = new System.Drawing.Point(125, 172);
			textBoxBasic.MaxLength = 15;
			textBoxBasic.Name = "textBoxBasic";
			textBoxBasic.ReadOnly = true;
			textBoxBasic.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			textBoxBasic.Size = new System.Drawing.Size(186, 20);
			textBoxBasic.TabIndex = 4;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(8, 175);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(85, 13);
			label1.TabIndex = 177;
			label1.Text = "Basic Salary :";
			textBoxConfirmation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxConfirmation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxConfirmation.CustomReportFieldName = "";
			textBoxConfirmation.CustomReportKey = "";
			textBoxConfirmation.CustomReportValueType = 1;
			textBoxConfirmation.IsComboTextBox = false;
			textBoxConfirmation.IsModified = false;
			textBoxConfirmation.IsRequired = true;
			textBoxConfirmation.Location = new System.Drawing.Point(428, 128);
			textBoxConfirmation.MaxLength = 64;
			textBoxConfirmation.Name = "textBoxConfirmation";
			textBoxConfirmation.ReadOnly = true;
			textBoxConfirmation.Size = new System.Drawing.Size(188, 20);
			textBoxConfirmation.TabIndex = 358;
			textBoxConfirmation.TabStop = false;
			textBoxJoining.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxJoining.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxJoining.CustomReportFieldName = "";
			textBoxJoining.CustomReportKey = "";
			textBoxJoining.CustomReportValueType = 1;
			textBoxJoining.IsComboTextBox = false;
			textBoxJoining.IsModified = false;
			textBoxJoining.IsRequired = true;
			textBoxJoining.Location = new System.Drawing.Point(428, 106);
			textBoxJoining.MaxLength = 64;
			textBoxJoining.Name = "textBoxJoining";
			textBoxJoining.ReadOnly = true;
			textBoxJoining.Size = new System.Drawing.Size(188, 20);
			textBoxJoining.TabIndex = 357;
			textBoxJoining.TabStop = false;
			textBoxLabourID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLabourID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLabourID.CustomReportFieldName = "";
			textBoxLabourID.CustomReportKey = "";
			textBoxLabourID.CustomReportValueType = 1;
			textBoxLabourID.IsComboTextBox = false;
			textBoxLabourID.IsModified = false;
			textBoxLabourID.IsRequired = true;
			textBoxLabourID.Location = new System.Drawing.Point(428, 150);
			textBoxLabourID.MaxLength = 64;
			textBoxLabourID.Name = "textBoxLabourID";
			textBoxLabourID.ReadOnly = true;
			textBoxLabourID.Size = new System.Drawing.Size(188, 20);
			textBoxLabourID.TabIndex = 355;
			textBoxLabourID.TabStop = false;
			mmLabel38.AutoSize = true;
			mmLabel38.BackColor = System.Drawing.Color.Transparent;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel38.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(333, 128);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(72, 13);
			mmLabel38.TabIndex = 366;
			mmLabel38.Text = "Confirmation:";
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(333, 151);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(58, 13);
			mmLabel10.TabIndex = 365;
			mmLabel10.Text = "Labour ID:";
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(333, 106);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(73, 13);
			mmLabel9.TabIndex = 364;
			mmLabel9.Text = "Joining Date :";
			textBoxDepartment.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDepartment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDepartment.CustomReportFieldName = "";
			textBoxDepartment.CustomReportKey = "";
			textBoxDepartment.CustomReportValueType = 1;
			textBoxDepartment.IsComboTextBox = false;
			textBoxDepartment.IsModified = false;
			textBoxDepartment.IsRequired = true;
			textBoxDepartment.Location = new System.Drawing.Point(125, 150);
			textBoxDepartment.MaxLength = 64;
			textBoxDepartment.Name = "textBoxDepartment";
			textBoxDepartment.ReadOnly = true;
			textBoxDepartment.Size = new System.Drawing.Size(186, 20);
			textBoxDepartment.TabIndex = 354;
			textBoxDepartment.TabStop = false;
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(9, 153);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(71, 13);
			mmLabel8.TabIndex = 363;
			mmLabel8.Text = "Department :";
			textBoxDivision.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDivision.CustomReportFieldName = "";
			textBoxDivision.CustomReportKey = "";
			textBoxDivision.CustomReportValueType = 1;
			textBoxDivision.IsComboTextBox = false;
			textBoxDivision.IsModified = false;
			textBoxDivision.IsRequired = true;
			textBoxDivision.Location = new System.Drawing.Point(125, 128);
			textBoxDivision.MaxLength = 64;
			textBoxDivision.Name = "textBoxDivision";
			textBoxDivision.ReadOnly = true;
			textBoxDivision.Size = new System.Drawing.Size(186, 20);
			textBoxDivision.TabIndex = 353;
			textBoxDivision.TabStop = false;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(9, 130);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(50, 13);
			mmLabel7.TabIndex = 362;
			mmLabel7.Text = "Division :";
			textBoxLocation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLocation.CustomReportFieldName = "";
			textBoxLocation.CustomReportKey = "";
			textBoxLocation.CustomReportValueType = 1;
			textBoxLocation.IsComboTextBox = false;
			textBoxLocation.IsModified = false;
			textBoxLocation.IsRequired = true;
			textBoxLocation.Location = new System.Drawing.Point(125, 106);
			textBoxLocation.MaxLength = 64;
			textBoxLocation.Name = "textBoxLocation";
			textBoxLocation.ReadOnly = true;
			textBoxLocation.Size = new System.Drawing.Size(186, 20);
			textBoxLocation.TabIndex = 352;
			textBoxLocation.TabStop = false;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(9, 108);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(54, 13);
			mmLabel6.TabIndex = 361;
			mmLabel6.Text = "Location :";
			textBoxGender.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			textBoxGender.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
			textBoxGender.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGender.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxGender.CustomReportFieldName = "";
			textBoxGender.CustomReportKey = "";
			textBoxGender.CustomReportValueType = 1;
			textBoxGender.IsComboTextBox = false;
			textBoxGender.IsModified = false;
			textBoxGender.Location = new System.Drawing.Point(428, 84);
			textBoxGender.MaxLength = 20;
			textBoxGender.Name = "textBoxGender";
			textBoxGender.ReadOnly = true;
			textBoxGender.Size = new System.Drawing.Size(188, 20);
			textBoxGender.TabIndex = 356;
			textBoxGender.TabStop = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(333, 86);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(49, 13);
			mmLabel3.TabIndex = 360;
			mmLabel3.Text = "Gender :";
			lblDescriptions.AutoSize = true;
			lblDescriptions.BackColor = System.Drawing.Color.Transparent;
			lblDescriptions.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDescriptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDescriptions.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			lblDescriptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDescriptions.IsFieldHeader = false;
			lblDescriptions.IsRequired = false;
			lblDescriptions.Location = new System.Drawing.Point(9, 87);
			lblDescriptions.Name = "lblDescriptions";
			lblDescriptions.PenWidth = 1f;
			lblDescriptions.ShowBorder = false;
			lblDescriptions.Size = new System.Drawing.Size(70, 13);
			lblDescriptions.TabIndex = 359;
			lblDescriptions.Text = "Designation :";
			textBoxDesignation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDesignation.CustomReportFieldName = "";
			textBoxDesignation.CustomReportKey = "";
			textBoxDesignation.CustomReportValueType = 1;
			textBoxDesignation.IsComboTextBox = false;
			textBoxDesignation.IsModified = false;
			textBoxDesignation.IsRequired = true;
			textBoxDesignation.Location = new System.Drawing.Point(125, 84);
			textBoxDesignation.MaxLength = 64;
			textBoxDesignation.Name = "textBoxDesignation";
			textBoxDesignation.ReadOnly = true;
			textBoxDesignation.Size = new System.Drawing.Size(186, 20);
			textBoxDesignation.TabIndex = 351;
			textBoxDesignation.TabStop = false;
			dateTimePickerLastWorking.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerLastWorking.Location = new System.Drawing.Point(125, 194);
			dateTimePickerLastWorking.Name = "dateTimePickerLastWorking";
			dateTimePickerLastWorking.Size = new System.Drawing.Size(186, 20);
			dateTimePickerLastWorking.TabIndex = 5;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(7, 196);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(121, 13);
			mmLabel1.TabIndex = 369;
			mmLabel1.Text = "Last Working Date :";
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(169, 196);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(52, 14);
			ultraFormattedLinkLabel3.TabIndex = 367;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Currency:";
			ultraFormattedLinkLabel3.Visible = false;
			appearance77.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance77;
			appearance78.FontData.BoldAsString = "True";
			appearance78.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance78;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(10, 44);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(48, 15);
			ultraFormattedLinkLabel2.TabIndex = 370;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Doc ID :";
			appearance79.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance79;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked_1);
			appearance80.FontData.BoldAsString = "True";
			appearance80.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance80;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(269, 43);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(76, 15);
			ultraFormattedLinkLabel4.TabIndex = 371;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Voucher No :";
			appearance81.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance81;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = textBoxEmployeeName;
			appearance82.BackColor = System.Drawing.SystemColors.Window;
			appearance82.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance82;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance83.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance83;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance84;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance85.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance85.BackColor2 = System.Drawing.SystemColors.Control;
			appearance85.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance85.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance85;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			appearance86.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance86;
			appearance87.BackColor = System.Drawing.SystemColors.Highlight;
			appearance87.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance87;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance88.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance88;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			appearance89.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance89;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance90.BackColor = System.Drawing.SystemColors.Control;
			appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance90.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance90.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance90;
			appearance91.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance91;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance92;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance93.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance93;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(125, 62);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(138, 20);
			comboBoxEmployee.TabIndex = 3;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance94.BackColor = System.Drawing.SystemColors.Window;
			appearance94.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance94;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance95.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance95.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance95.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance95;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance96;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance97.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance97.BackColor2 = System.Drawing.SystemColors.Control;
			appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance97.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance97;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance98;
			appearance99.BackColor = System.Drawing.SystemColors.Highlight;
			appearance99.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance99;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance100.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance100;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			appearance101.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance101;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance102.BackColor = System.Drawing.SystemColors.Control;
			appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance102.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance102.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance102;
			appearance103.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance103;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance104.BackColor = System.Drawing.SystemColors.Window;
			appearance104.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance104;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance105.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance105;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(125, 40);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(138, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCurrency.Assigned = false;
			comboBoxCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCurrency.CustomReportFieldName = "";
			comboBoxCurrency.CustomReportKey = "";
			comboBoxCurrency.CustomReportValueType = 1;
			comboBoxCurrency.DescriptionTextBox = null;
			appearance106.BackColor = System.Drawing.SystemColors.Window;
			appearance106.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCurrency.DisplayLayout.Appearance = appearance106;
			comboBoxCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance107.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance107.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance107.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance107.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.GroupByBox.Appearance = appearance107;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance108;
			comboBoxCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance109.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance109.BackColor2 = System.Drawing.SystemColors.Control;
			appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance109;
			comboBoxCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance110.BackColor = System.Drawing.SystemColors.Window;
			appearance110.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance110;
			appearance111.BackColor = System.Drawing.SystemColors.Highlight;
			appearance111.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance111;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance112.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.CardAreaAppearance = appearance112;
			appearance113.BorderColor = System.Drawing.Color.Silver;
			appearance113.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCurrency.DisplayLayout.Override.CellAppearance = appearance113;
			comboBoxCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance114.BackColor = System.Drawing.SystemColors.Control;
			appearance114.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance114.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance114.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance114;
			appearance115.TextHAlignAsString = "Left";
			comboBoxCurrency.DisplayLayout.Override.HeaderAppearance = appearance115;
			comboBoxCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance116.BackColor = System.Drawing.SystemColors.Window;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			comboBoxCurrency.DisplayLayout.Override.RowAppearance = appearance116;
			comboBoxCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance117.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance117;
			comboBoxCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCurrency.Editable = true;
			comboBoxCurrency.FilterString = "";
			comboBoxCurrency.HasAllAccount = false;
			comboBoxCurrency.HasCustom = false;
			comboBoxCurrency.IsDataLoaded = false;
			comboBoxCurrency.Location = new System.Drawing.Point(636, 162);
			comboBoxCurrency.MaxDropDownItems = 12;
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.ShowInactiveItems = false;
			comboBoxCurrency.ShowQuickAdd = true;
			comboBoxCurrency.Size = new System.Drawing.Size(114, 20);
			comboBoxCurrency.TabIndex = 4;
			comboBoxCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCurrency.Visible = false;
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
			comboBoxEmployees.Assigned = false;
			comboBoxEmployees.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployees.CustomReportFieldName = "";
			comboBoxEmployees.CustomReportKey = "";
			comboBoxEmployees.CustomReportValueType = 1;
			comboBoxEmployees.DescriptionTextBox = null;
			appearance118.BackColor = System.Drawing.SystemColors.Window;
			appearance118.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployees.DisplayLayout.Appearance = appearance118;
			comboBoxEmployees.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployees.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance119.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance119.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance119.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance119.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployees.DisplayLayout.GroupByBox.Appearance = appearance119;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployees.DisplayLayout.GroupByBox.BandLabelAppearance = appearance120;
			comboBoxEmployees.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance121.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance121.BackColor2 = System.Drawing.SystemColors.Control;
			appearance121.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance121.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployees.DisplayLayout.GroupByBox.PromptAppearance = appearance121;
			comboBoxEmployees.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployees.DisplayLayout.MaxRowScrollRegions = 1;
			appearance122.BackColor = System.Drawing.SystemColors.Window;
			appearance122.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployees.DisplayLayout.Override.ActiveCellAppearance = appearance122;
			appearance123.BackColor = System.Drawing.SystemColors.Highlight;
			appearance123.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployees.DisplayLayout.Override.ActiveRowAppearance = appearance123;
			comboBoxEmployees.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployees.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance124.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployees.DisplayLayout.Override.CardAreaAppearance = appearance124;
			appearance125.BorderColor = System.Drawing.Color.Silver;
			appearance125.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployees.DisplayLayout.Override.CellAppearance = appearance125;
			comboBoxEmployees.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployees.DisplayLayout.Override.CellPadding = 0;
			appearance126.BackColor = System.Drawing.SystemColors.Control;
			appearance126.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance126.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance126.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployees.DisplayLayout.Override.GroupByRowAppearance = appearance126;
			appearance127.TextHAlignAsString = "Left";
			comboBoxEmployees.DisplayLayout.Override.HeaderAppearance = appearance127;
			comboBoxEmployees.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployees.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance128.BackColor = System.Drawing.SystemColors.Window;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployees.DisplayLayout.Override.RowAppearance = appearance128;
			comboBoxEmployees.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance129.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployees.DisplayLayout.Override.TemplateAddRowAppearance = appearance129;
			comboBoxEmployees.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployees.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployees.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployees.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployees.Editable = true;
			comboBoxEmployees.FilterString = "";
			comboBoxEmployees.HasAllAccount = false;
			comboBoxEmployees.HasCustom = false;
			comboBoxEmployees.IsDataLoaded = false;
			comboBoxEmployees.Location = new System.Drawing.Point(135, 7);
			comboBoxEmployees.MaxDropDownItems = 12;
			comboBoxEmployees.Name = "comboBoxEmployees";
			comboBoxEmployees.ShowInactiveItems = false;
			comboBoxEmployees.ShowQuickAdd = true;
			comboBoxEmployees.ShowTerminatedEmployees = true;
			comboBoxEmployees.Size = new System.Drawing.Size(142, 20);
			comboBoxEmployees.TabIndex = 1;
			comboBoxEmployees.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployees.SelectedIndexChanged += new System.EventHandler(comboBoxEmployees_SelectedIndexChanged);
			comboBoxDestination.Assigned = false;
			comboBoxDestination.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDestination.CustomReportFieldName = "";
			comboBoxDestination.CustomReportKey = "";
			comboBoxDestination.CustomReportValueType = 1;
			comboBoxDestination.DescriptionTextBox = null;
			appearance130.BackColor = System.Drawing.SystemColors.Window;
			appearance130.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDestination.DisplayLayout.Appearance = appearance130;
			comboBoxDestination.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDestination.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance131.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance131.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance131.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDestination.DisplayLayout.GroupByBox.Appearance = appearance131;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDestination.DisplayLayout.GroupByBox.BandLabelAppearance = appearance132;
			comboBoxDestination.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance133.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance133.BackColor2 = System.Drawing.SystemColors.Control;
			appearance133.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance133.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDestination.DisplayLayout.GroupByBox.PromptAppearance = appearance133;
			comboBoxDestination.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDestination.DisplayLayout.MaxRowScrollRegions = 1;
			appearance134.BackColor = System.Drawing.SystemColors.Window;
			appearance134.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDestination.DisplayLayout.Override.ActiveCellAppearance = appearance134;
			appearance135.BackColor = System.Drawing.SystemColors.Highlight;
			appearance135.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDestination.DisplayLayout.Override.ActiveRowAppearance = appearance135;
			comboBoxDestination.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDestination.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance136.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDestination.DisplayLayout.Override.CardAreaAppearance = appearance136;
			appearance137.BorderColor = System.Drawing.Color.Silver;
			appearance137.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDestination.DisplayLayout.Override.CellAppearance = appearance137;
			comboBoxDestination.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDestination.DisplayLayout.Override.CellPadding = 0;
			appearance138.BackColor = System.Drawing.SystemColors.Control;
			appearance138.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance138.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance138.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance138.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDestination.DisplayLayout.Override.GroupByRowAppearance = appearance138;
			appearance139.TextHAlignAsString = "Left";
			comboBoxDestination.DisplayLayout.Override.HeaderAppearance = appearance139;
			comboBoxDestination.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDestination.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance140.BackColor = System.Drawing.SystemColors.Window;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			comboBoxDestination.DisplayLayout.Override.RowAppearance = appearance140;
			comboBoxDestination.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance141.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDestination.DisplayLayout.Override.TemplateAddRowAppearance = appearance141;
			comboBoxDestination.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDestination.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDestination.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDestination.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDestination.Editable = true;
			comboBoxDestination.FilterString = "";
			comboBoxDestination.HasAllAccount = false;
			comboBoxDestination.HasCustom = false;
			comboBoxDestination.IsDataLoaded = false;
			comboBoxDestination.Location = new System.Drawing.Point(127, 17);
			comboBoxDestination.MaxDropDownItems = 12;
			comboBoxDestination.Name = "comboBoxDestination";
			comboBoxDestination.ShowInactiveItems = false;
			comboBoxDestination.ShowQuickAdd = true;
			comboBoxDestination.Size = new System.Drawing.Size(114, 20);
			comboBoxDestination.TabIndex = 0;
			comboBoxDestination.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonRefresh.BackColor = System.Drawing.Color.Transparent;
			buttonRefresh.BackgroundImage = Micromind.ClientUI.Properties.Resources.Refresh;
			buttonRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			buttonRefresh.Location = new System.Drawing.Point(311, 194);
			buttonRefresh.Name = "buttonRefresh";
			buttonRefresh.Size = new System.Drawing.Size(22, 21);
			buttonRefresh.TabIndex = 372;
			buttonRefresh.UseVisualStyleBackColor = false;
			buttonRefresh.Click += new System.EventHandler(buttonRefresh_Click);
			radioButtonResigned.AutoSize = true;
			radioButtonResigned.Checked = true;
			radioButtonResigned.Location = new System.Drawing.Point(428, 175);
			radioButtonResigned.Name = "radioButtonResigned";
			radioButtonResigned.Size = new System.Drawing.Size(70, 17);
			radioButtonResigned.TabIndex = 373;
			radioButtonResigned.TabStop = true;
			radioButtonResigned.Text = "Resigned";
			radioButtonResigned.UseVisualStyleBackColor = true;
			radioButtonResigned.CheckedChanged += new System.EventHandler(radioButtonResigned_CheckedChanged);
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(333, 175);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(45, 13);
			mmLabel2.TabIndex = 374;
			mmLabel2.Text = "Status :";
			radioButtonTerminated.AutoSize = true;
			radioButtonTerminated.Location = new System.Drawing.Point(507, 175);
			radioButtonTerminated.Name = "radioButtonTerminated";
			radioButtonTerminated.Size = new System.Drawing.Size(78, 17);
			radioButtonTerminated.TabIndex = 375;
			radioButtonTerminated.Text = "Terminated";
			radioButtonTerminated.UseVisualStyleBackColor = true;
			radioButtonTerminated.CheckedChanged += new System.EventHandler(radioButtonTerminated_CheckedChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(725, 628);
			base.Controls.Add(radioButtonTerminated);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(radioButtonResigned);
			base.Controls.Add(buttonRefresh);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(dateTimePickerLastWorking);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(ultraFormattedLinkLabel3);
			base.Controls.Add(textBoxConfirmation);
			base.Controls.Add(textBoxJoining);
			base.Controls.Add(textBoxLabourID);
			base.Controls.Add(mmLabel38);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(textBoxDepartment);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(textBoxDivision);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(textBoxLocation);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(textBoxGender);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(lblDescriptions);
			base.Controls.Add(textBoxDesignation);
			base.Controls.Add(textBoxBasic);
			base.Controls.Add(label1);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(comboBoxEmployee);
			base.Controls.Add(comboBoxSysDoc);
			base.Controls.Add(textBoxVoucherNumber);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(LabelPeriod);
			base.Controls.Add(textBoxEmployeeName);
			base.Controls.Add(ultraFormattedLinkLabel5);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(comboBoxCurrency);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "EmployeeEOSForm";
			Text = "Employee EOS";
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItem).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDeduction).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridDeduction).EndInit();
			tabPageUserDefined.ResumeLayout(false);
			tabPageUserDefined.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridLoan).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBenefit).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployees).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDestination).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
