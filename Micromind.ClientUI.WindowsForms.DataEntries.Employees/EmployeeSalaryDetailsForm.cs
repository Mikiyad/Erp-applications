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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class EmployeeSalaryDetailsForm : Form, IForm
	{
		private DataSet currentData;

		private const string TABLENAME_CONST = "Employee";

		private const string IDFIELD_CONST = "EmployeeID";

		private bool isRevise;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool isLoadingNewEmployee;

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

		private EmployeeComboBox comboBoxEmployees;

		private MMTextBox textBoxEmployeeName;

		private NonDirtyPanel nonDirtyPanel1;

		private DataEntryGrid dataGridPayrollItem;

		private PayrollItemComboBox comboBoxPayrollItem;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl tabPageUserDefined;

		private EmployeePaymentMethodComboBox comboBoxPaymentMethod;

		private Label label4;

		private TextBox textBoxBankAccountNumber;

		private CurrencyComboBox comboBoxCurrency;

		private DataEntryGrid dataGridDeduction;

		private DataEntryGrid dataGridBenefit;

		private BenefitComboBox comboBoxBenefit;

		private AmountTextBox textBoxTotalSalary;

		private Label label7;

		private TextBox textBoxRemarks;

		private Label label8;

		private BankComboBox comboBoxBank;

		private UltraFormattedLinkLabel linkLabelCountry;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private DestinationComboBox comboBoxDestination;

		private NumberTextBox textBoxNumberOfTickets;

		private AmountTextBox textBoxTicketAmount;

		private Label label3;

		private Label label5;

		private NumberTextBox textBoxTicketPeriod;

		private Label label9;

		private Label label6;

		private TextBox textBoxTicketRemarks;

		private Label label10;

		private PayrollItemComboBox comboBoxDeduction;

		private EOSRuleComboBox comboBoxEOS;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private OverTimeComboBox comboBoxOvertime;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private Label labelReviewDate;

		private DateTimePicker dateTimePickerDate;

		private Label label11;

		private MMTextBox textBoxLastReviseDate;

		private NonDirtyPanel panelReviseDate;

		private XPButton buttonClear;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonAttach;

		private Label label1;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5017;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		public bool IsRevised
		{
			get
			{
				return isRevise;
			}
			set
			{
				isRevise = value;
				panelReviseDate.Visible = isRevise;
				Text = (isRevise ? "Revise Employee Salary Detail" : "Employee Salary Detail");
				UltraGridColumn ultraGridColumn = dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"];
				UltraGridColumn ultraGridColumn2 = dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"];
				bool flag2 = dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].Hidden = !isRevise;
				bool hidden = ultraGridColumn2.Hidden = flag2;
				ultraGridColumn.Hidden = hidden;
				UltraGridColumn ultraGridColumn3 = dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"];
				UltraGridColumn ultraGridColumn4 = dataGridBenefit.DisplayLayout.Bands[0].Columns["Amount"];
				Activation activation2 = dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = (isRevise ? Activation.NoEdit : Activation.AllowEdit);
				Activation activation5 = ultraGridColumn3.CellActivation = (ultraGridColumn4.CellActivation = activation2);
				DateTimePicker dateTimePicker = dateTimePickerDate;
				hidden = (labelReviewDate.Visible = value);
				dateTimePicker.Visible = hidden;
			}
		}

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return false;
			}
			set
			{
				if (!screenRight.New || !screenRight.Edit)
				{
					buttonSave.Enabled = false;
				}
			}
		}

		public EmployeeSalaryDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EmployeeSalaryDetailsForm_Load;
			dataGridPayrollItem.CellDataError += dataGrid_CellDataError;
			dataGridPayrollItem.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridPayrollItem.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridPayrollItem.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			comboBoxPayrollItem.SelectedIndexChanged += comboBoxPayrollItem_SelectedIndexChanged;
			dataGridPayrollItem.AfterCellUpdate += dataGridPayrollItem_AfterCellUpdate;
			dataGridPayrollItem.HeaderClicked += dataGridPayrollItem_HeaderClicked;
			dataGridDeduction.HeaderClicked += dataGridDeduction_HeaderClicked;
			comboBoxDeduction.SelectedIndexChanged += comboBoxDeduction_SelectedIndexChanged;
			dataGridDeduction.BeforeCellDeactivate += dataGridDeduction_BeforeCellDeactivate;
			dataGridDeduction.BeforeRowDeactivate += dataGridDeduction_BeforeRowDeactivate;
			dataGridDeduction.CellDataError += dataGridDeduction_CellDataError;
			dataGridDeduction.AfterCellUpdate += dataGridDeduction_AfterCellUpdate;
			dataGridBenefit.HeaderClicked += dataGridBenefit_HeaderClicked;
			comboBoxBenefit.SelectedIndexChanged += comboBoxBenefit_SelectedIndexChanged;
			dataGridBenefit.BeforeCellDeactivate += dataGridBenefit_BeforeCellDeactivate;
			dataGridBenefit.BeforeRowDeactivate += dataGridBenefit_BeforeRowDeactivate;
			dataGridBenefit.CellDataError += dataGridBenefit_CellDataError;
			dataGridBenefit.AfterCellUpdate += dataGridBenefit_AfterCellUpdate;
			comboBoxDestination.SelectedIndexChanged += comboBoxDestination_SelectedIndexChanged;
		}

		private void dataGridBenefit_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Non-Benefit")
			{
				dataGridBenefit.ActiveRow.Cells["Description"].Value = comboBoxBenefit.SelectedName;
			}
		}

		private void dataGridDeduction_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Deduction")
			{
				dataGridDeduction.ActiveRow.Cells["Description"].Value = comboBoxDeduction.SelectedName;
			}
		}

		private void dataGridBenefit_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridBenefit.ActiveCell.Column.Key.ToString() == "Non-Benefit")
			{
				e.RaiseErrorEvent = false;
				comboBoxBenefit.Text = dataGridBenefit.ActiveCell.Text;
				comboBoxBenefit.QuickAddItem();
			}
			else if (dataGridBenefit.ActiveCell.Column.Key.ToString() == "Amount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please enter a numeric value greater or equal to zero.");
			}
		}

		private void dataGridDeduction_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridDeduction.ActiveCell.Column.Key.ToString() == "Deduction")
			{
				e.RaiseErrorEvent = false;
				comboBoxDeduction.Text = dataGridDeduction.ActiveCell.Text;
				comboBoxDeduction.QuickAddItem();
			}
			else if (dataGridDeduction.ActiveCell.Column.Key.ToString() == "Amount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please enter a numeric value greater or equal to zero.");
			}
		}

		private void dataGridBenefit_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridBenefit.ActiveRow;
			if (activeRow != null && activeRow.Cells["Non-Benefit"].Value.ToString() == "")
			{
				ErrorHelper.InformationMessage("Please select an benefit.");
				e.Cancel = true;
				activeRow.Cells["Non-Benefit"].Activate();
			}
		}

		private void dataGridDeduction_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridDeduction.ActiveRow;
			if (activeRow != null && activeRow.Cells["Deduction"].Value.ToString() == "")
			{
				ErrorHelper.InformationMessage("Please select a deduction.");
				e.Cancel = true;
				activeRow.Cells["Deduction"].Activate();
			}
		}

		private void dataGridBenefit_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridBenefit.ActiveCell.Column.Key == "Non-Benefit")
			{
				foreach (UltraGridRow row in dataGridBenefit.Rows)
				{
					if (row.Index != dataGridBenefit.ActiveRow.Index && !(row.Cells["Non-Benefit"].Value.ToString() == "") && row.Cells["Non-Benefit"].Value.ToString() == dataGridBenefit.ActiveCell.Value.ToString())
					{
						ErrorHelper.InformationMessage("This benefit is already selected for this employee.");
						e.Cancel = true;
						return;
					}
				}
				if (dataGridBenefit.ActiveRow.Cells["Non-Benefit"].Value.ToString() == "")
				{
					dataGridBenefit.ActiveRow.Cells["Description"].Value = "";
				}
			}
			else if (dataGridBenefit.ActiveCell.Column.Key == "Amount")
			{
				if (dataGridBenefit.ActiveCell.Text == "")
				{
					dataGridBenefit.ActiveCell.Value = 0.ToString(Format.TotalAmountFormat);
				}
				else
				{
					dataGridBenefit.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridBenefit.ActiveCell.Text, NumberStyles.Any), 2).ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void dataGridDeduction_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridDeduction.ActiveCell.Column.Key == "Deduction")
			{
				foreach (UltraGridRow row in dataGridDeduction.Rows)
				{
					if (row.Index != dataGridDeduction.ActiveRow.Index && !(row.Cells["Deduction"].Value.ToString() == "") && row.Cells["Deduction"].Value.ToString() == dataGridDeduction.ActiveCell.Value.ToString())
					{
						ErrorHelper.InformationMessage("This deduction is already selected for this employee.");
						e.Cancel = true;
						return;
					}
				}
				if (dataGridDeduction.ActiveRow.Cells["Deduction"].Value.ToString() == "")
				{
					dataGridDeduction.ActiveRow.Cells["Description"].Value = "";
				}
			}
			else if (dataGridDeduction.ActiveCell.Column.Key == "Amount")
			{
				if (dataGridDeduction.ActiveCell.Text == "")
				{
					dataGridDeduction.ActiveCell.Value = 0.ToString(Format.TotalAmountFormat);
				}
				else
				{
					dataGridDeduction.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridDeduction.ActiveCell.Text, NumberStyles.Any), 2).ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void comboBoxBenefit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxDeduction_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGridBenefit_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (ultraGridColumn != null && ultraGridColumn.Key == "Non-Benefit")
			{
				string id = "";
				if (dataGridBenefit.ActiveRow != null)
				{
					id = dataGridBenefit.ActiveRow.Cells["Non-Benefit"].Text;
				}
				new FormHelper().EditBenefit(id);
			}
		}

		public void EditDocument(string employeeID)
		{
			if (!comboBoxEmployees.Enabled)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxEmployees.SelectedID = employeeID;
			LoadData(employeeID);
		}

		private void dataGridDeduction_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (ultraGridColumn != null && ultraGridColumn.Key == "Deduction")
			{
				string id = "";
				if (dataGridDeduction.ActiveRow != null)
				{
					id = dataGridDeduction.ActiveRow.Cells["Deduction"].Text;
				}
				new FormHelper().EditDeduction(id);
			}
		}

		private void dataGridPayrollItem_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (ultraGridColumn != null && ultraGridColumn.Key == "PayrollItem")
			{
				string id = "";
				if (dataGridPayrollItem.ActiveRow != null)
				{
					id = dataGridPayrollItem.ActiveRow.Cells["PayrollItem"].Text;
				}
				new FormHelper().EditPayrollItem(id);
			}
		}

		private void dataGridPayrollItem_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "Amount")
			{
				ShowTotalSalary();
			}
			else if (e.Cell.Column.Key == "PayrollItem")
			{
				dataGridPayrollItem.ActiveRow.Cells["Description"].Value = comboBoxPayrollItem.SelectedName;
			}
		}

		private void comboBoxPayrollItem_SelectedIndexChanged(object sender, EventArgs e)
		{
			_ = dataGridPayrollItem.ActiveRow;
		}

		private void comboBoxDestination_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxDestination.SelectedID != "")
			{
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Destination", "TicketFixedAmount", "DestinationID", comboBoxDestination.SelectedID);
				textBoxTicketAmount.Value = decimal.Parse(fieldValue.ToString());
			}
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridPayrollItem.ActiveCell.Column.Key == "PayrollItem")
			{
				foreach (UltraGridRow row in dataGridPayrollItem.Rows)
				{
					if (row.Index != dataGridPayrollItem.ActiveRow.Index && !(row.Cells["PayrollItem"].Value.ToString() == "") && row.Cells["PayrollItem"].Value.ToString() == dataGridPayrollItem.ActiveCell.Value.ToString())
					{
						ErrorHelper.InformationMessage("This payrollItem is already selected for this employee.");
						e.Cancel = true;
						break;
					}
				}
			}
			else if (dataGridPayrollItem.ActiveCell.Column.Key == "Amount")
			{
				if (dataGridPayrollItem.ActiveCell.Text == "")
				{
					dataGridPayrollItem.ActiveCell.Value = 0.ToString(Format.TotalAmountFormat);
				}
				else
				{
					dataGridPayrollItem.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridPayrollItem.ActiveCell.Text, NumberStyles.Any), 2).ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridPayrollItem.ActiveRow;
			if (activeRow != null && activeRow.Cells["PayrollItem"].Value.ToString() == "")
			{
				ErrorHelper.InformationMessage("Please select an payrollItem.");
				e.Cancel = true;
				activeRow.Cells["PayrollItem"].Activate();
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridPayrollItem.ActiveCell.Column.Key.ToString() == "PayrollItem")
			{
				e.RaiseErrorEvent = false;
				comboBoxPayrollItem.Text = dataGridPayrollItem.ActiveCell.Text;
				comboBoxPayrollItem.QuickAddItem();
			}
			else if (dataGridPayrollItem.ActiveCell.Column.Key.ToString() == "Amount")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please enter a numeric value greater or equal to zero.");
			}
		}

		private bool GetData()
		{
			try
			{
				currentData = EmployeeData.CreateEmployeeSalaryDetailTable();
				DataRow dataRow = currentData.Tables["Employee"].NewRow();
				dataRow.BeginEdit();
				string value = (string)(dataRow["EmployeeID"] = ((!isLoadingNewEmployee) ? comboBoxEmployees.SelectedID : comboBoxEmployees.OldValue));
				if (comboBoxBank.SelectedID != "")
				{
					dataRow["BankID"] = comboBoxBank.SelectedID;
				}
				else
				{
					dataRow["BankID"] = DBNull.Value;
				}
				dataRow["AccountNumber"] = textBoxBankAccountNumber.Text;
				if (comboBoxCurrency.SelectedID != "")
				{
					dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
				}
				if (comboBoxPaymentMethod.SelectedID > 0)
				{
					dataRow["PaymentMethodID"] = comboBoxPaymentMethod.SelectedID;
				}
				else
				{
					dataRow["PaymentMethodID"] = 1;
				}
				if (comboBoxEOS.SelectedID != "")
				{
					dataRow["EOSRuleID"] = comboBoxEOS.SelectedID;
				}
				else
				{
					dataRow["EOSRuleID"] = DBNull.Value;
				}
				if (comboBoxOvertime.SelectedID != "")
				{
					dataRow["OverTimeID"] = comboBoxOvertime.SelectedID;
				}
				else
				{
					dataRow["OverTimeID"] = DBNull.Value;
				}
				dataRow["SalaryRemarks"] = textBoxRemarks.Text;
				if (comboBoxDestination.SelectedID != "")
				{
					dataRow["DestinationID"] = comboBoxDestination.SelectedID;
				}
				else
				{
					dataRow["DestinationID"] = DBNull.Value;
				}
				dataRow["NumberOfTickets"] = textBoxNumberOfTickets.Text;
				dataRow["TicketPeriod"] = textBoxTicketPeriod.Text;
				dataRow["TicketAmount"] = textBoxTicketAmount.Text;
				dataRow["TicketRemarks"] = textBoxRemarks.Text;
				dataRow["LastRevisedSalaryDate"] = dateTimePickerDate.Value;
				if (IsRevised)
				{
					dataRow["PreviousRevisedSalaryDate"] = DateTime.Parse(textBoxLastReviseDate.Text);
				}
				dataRow.EndEdit();
				currentData.Tables["Employee"].Rows.Add(dataRow);
				currentData.Tables["Employee"].AcceptChanges();
				currentData.Tables["Employee"].Rows[0]["EmployeeID"] = value;
				foreach (UltraGridRow row in dataGridPayrollItem.Rows)
				{
					if (!(row.Cells["PayrollItem"].Value.ToString() == ""))
					{
						if (!currentData.Tables.Contains("Employee_PayrollItem_Detail"))
						{
							EmployeePayrollItemDetailData employeePayrollItemDetailData = new EmployeePayrollItemDetailData();
							currentData.Tables.Add(employeePayrollItemDetailData.Tables[0].Clone());
						}
						dataRow = currentData.Tables["Employee_PayrollItem_Detail"].NewRow();
						dataRow.BeginEdit();
						dataRow["PayType"] = 1;
						dataRow["EmployeeID"] = value;
						dataRow["PayrollItemID"] = row.Cells["PayrollItem"].Value.ToString();
						dataRow["Amount"] = ((!IsRevised) ? decimal.Parse(row.Cells["Amount"].Value.ToString()) : decimal.Parse(row.Cells["New Amount"].Value.ToString()));
						if (IsRevised)
						{
							dataRow["LastAmount"] = decimal.Parse(row.Cells["Amount"].Value.ToString());
						}
						dataRow["RowIndex"] = row.Index;
						dataRow.EndEdit();
						currentData.Tables["Employee_PayrollItem_Detail"].Rows.Add(dataRow);
					}
				}
				foreach (UltraGridRow row2 in dataGridDeduction.Rows)
				{
					if (!(row2.Cells["Deduction"].Value.ToString() == ""))
					{
						if (!currentData.Tables.Contains("Employee_PayrollItem_Detail"))
						{
							EmployeePayrollItemDetailData employeePayrollItemDetailData2 = new EmployeePayrollItemDetailData();
							currentData.Tables.Add(employeePayrollItemDetailData2.Tables[0].Clone());
						}
						dataRow = currentData.Tables["Employee_PayrollItem_Detail"].NewRow();
						dataRow.BeginEdit();
						dataRow["PayType"] = 2;
						dataRow["EmployeeID"] = value;
						dataRow["PayrollItemID"] = row2.Cells["Deduction"].Value.ToString();
						dataRow["Amount"] = ((!IsRevised) ? decimal.Parse(row2.Cells["Amount"].Value.ToString()) : decimal.Parse(row2.Cells["New Amount"].Value.ToString()));
						if (IsRevised)
						{
							dataRow["LastAmount"] = decimal.Parse(row2.Cells["Amount"].Value.ToString());
						}
						if (row2.Cells["Start Date"].Value.ToString() != "")
						{
							dataRow["StartDate"] = row2.Cells["Start Date"].Value.ToString();
						}
						else
						{
							dataRow["StartDate"] = DBNull.Value;
						}
						if (row2.Cells["End Date"].Value.ToString() != "")
						{
							dataRow["EndDate"] = row2.Cells["End Date"].Value.ToString();
						}
						else
						{
							dataRow["EndDate"] = DBNull.Value;
						}
						dataRow["RowIndex"] = row2.Index;
						dataRow.EndEdit();
						currentData.Tables["Employee_PayrollItem_Detail"].Rows.Add(dataRow);
					}
				}
				foreach (UltraGridRow row3 in dataGridBenefit.Rows)
				{
					if (!(row3.Cells["Non-Benefit"].Value.ToString() == ""))
					{
						if (!currentData.Tables.Contains("Employee_Benefit_Detail"))
						{
							EmployeeBenefitDetailData employeeBenefitDetailData = new EmployeeBenefitDetailData();
							currentData.Tables.Add(employeeBenefitDetailData.Tables[0].Clone());
						}
						dataRow = currentData.Tables["Employee_Benefit_Detail"].NewRow();
						dataRow.BeginEdit();
						dataRow["EmployeeID"] = value;
						dataRow["BenefitID"] = row3.Cells["Non-Benefit"].Value.ToString();
						if (string.IsNullOrEmpty(row3.Cells["Amount"].Value.ToString()))
						{
							row3.Cells["Amount"].Value = 0;
						}
						dataRow["Amount"] = ((!IsRevised) ? decimal.Parse(row3.Cells["Amount"].Value.ToString()) : decimal.Parse(row3.Cells["New Amount"].Value.ToString()));
						if (IsRevised)
						{
							dataRow["LastAmount"] = decimal.Parse(row3.Cells["Amount"].Value.ToString());
						}
						if (row3.Cells["Start Date"].Value.ToString() != "")
						{
							dataRow["StartDate"] = row3.Cells["Start Date"].Value.ToString();
						}
						else
						{
							dataRow["StartDate"] = DBNull.Value;
						}
						if (row3.Cells["End Date"].Value.ToString() != "")
						{
							dataRow["EndDate"] = row3.Cells["End Date"].Value.ToString();
						}
						else
						{
							dataRow["EndDate"] = DBNull.Value;
						}
						dataRow["Remarks"] = row3.Cells["Remarks"].Value.ToString();
						dataRow["RowIndex"] = row3.Index;
						dataRow.EndEdit();
						currentData.Tables["Employee_Benefit_Detail"].Rows.Add(dataRow);
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

		private void SetupPayrollItemGrid()
		{
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable("PayrollItem");
			dataTable.Columns.Add("PayrollItem", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataTable.Columns.Add("New Amount", typeof(decimal));
			dataGridPayrollItem.DataSource = dataTable;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].CharacterCasing = CharacterCasing.Upper;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].ValueList = comboBoxPayrollItem;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Width = checked(25 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Description"].Width = checked(50 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(15 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["Amount"].Format = "n";
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].Width = checked(20 * dataGridPayrollItem.Width) / 100;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].MinValue = 0;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].Format = Format.GridAmountFormat;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["New Amount"].Hidden = true;
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridPayrollItem.DisplayLayout.Bands[0].Columns["PayrollItem"].Header.Appearance.Cursor = Cursors.Hand;
		}

		private void SetupDeductionGrid()
		{
			dataGridDeduction.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable("Deduction");
			dataTable.Columns.Add("Deduction", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("Start Date", typeof(DateTime));
			dataTable.Columns.Add("End Date", typeof(DateTime));
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataTable.Columns.Add("New Amount", typeof(decimal));
			dataGridDeduction.DataSource = dataTable;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].CharacterCasing = CharacterCasing.Upper;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].ValueList = comboBoxDeduction;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Start Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["End Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			UltraGridColumn ultraGridColumn = dataGridDeduction.DisplayLayout.Bands[0].Columns["Start Date"];
			bool hidden = dataGridDeduction.DisplayLayout.Bands[0].Columns["End Date"].Hidden = true;
			ultraGridColumn.Hidden = hidden;
			ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridDeduction.DisplayLayout.Bands[0].Columns["Start Date"].ExcludeFromColumnChooser = (dataGridDeduction.DisplayLayout.Bands[0].Columns["End Date"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].Width = checked(25 * dataGridDeduction.Width) / 100;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Description"].Width = checked(50 * dataGridDeduction.Width) / 100;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(15 * dataGridDeduction.Width) / 100;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].Width = checked(20 * dataGridDeduction.Width) / 100;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].MinValue = 0;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].Format = Format.GridAmountFormat;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["New Amount"].Hidden = true;
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridDeduction.DisplayLayout.Bands[0].Columns["Deduction"].Header.Appearance.Cursor = Cursors.Hand;
		}

		private void SetupBenefitGrid()
		{
			dataGridBenefit.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable("Benefit");
			dataTable.Columns.Add("Non-Benefit", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("Start Date", typeof(DateTime));
			dataTable.Columns.Add("End Date", typeof(DateTime));
			dataTable.Columns.Add("Remarks", typeof(string));
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataTable.Columns.Add("New Amount", typeof(decimal));
			dataGridBenefit.DataSource = dataTable;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Non-Benefit"].CharacterCasing = CharacterCasing.Upper;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 255;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Non-Benefit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Non-Benefit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Non-Benefit"].ValueList = comboBoxBenefit;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Start Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["End Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Non-Benefit"].Width = checked(25 * dataGridBenefit.Width) / 100;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Description"].Width = checked(50 * dataGridBenefit.Width) / 100;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(15 * dataGridBenefit.Width) / 100;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].Width = checked(20 * dataGridBenefit.Width) / 100;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].MinValue = 0;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].Format = Format.GridAmountFormat;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["New Amount"].Hidden = true;
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Non-Benefit"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridBenefit.DisplayLayout.Bands[0].Columns["Non-Benefit"].Header.Appearance.Cursor = Cursors.Hand;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGridPayrollItem.Focus();
		}

		public void LoadData(string employeeID)
		{
			try
			{
				if (!(employeeID.Trim() == ""))
				{
					if (comboBoxEmployees.Text != employeeID)
					{
						comboBoxEmployees.SelectedID = employeeID;
					}
					if (CanClose())
					{
						currentData = Factory.EmployeeSystem.GetEmployeeSalaryDetailsByEmployeeID(employeeID);
						if (currentData != null)
						{
							FillData();
							IsNewRecord = false;
							formManager.ResetDirty();
						}
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
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0 && currentData.Tables.Contains("Employee") && currentData.Tables["Employee"].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					comboBoxEmployees.SelectedID = dataRow["EmployeeID"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					if (dataRow["PaymentMethodID"] != DBNull.Value)
					{
						comboBoxPaymentMethod.SelectedID = int.Parse(dataRow["PaymentMethodID"].ToString());
					}
					else
					{
						comboBoxPaymentMethod.SelectedID = -1;
					}
					comboBoxBank.SelectedID = dataRow["BankID"].ToString();
					textBoxRemarks.Text = dataRow["SalaryRemarks"].ToString();
					textBoxBankAccountNumber.Text = dataRow["IBAN"].ToString();
					comboBoxDestination.SelectedID = dataRow["DestinationID"].ToString();
					if (dataRow["NumberOfTickets"] != DBNull.Value)
					{
						textBoxNumberOfTickets.Text = dataRow["NumberOfTickets"].ToString();
					}
					else
					{
						textBoxNumberOfTickets.Text = "0";
					}
					if (dataRow["TicketPeriod"] != DBNull.Value)
					{
						textBoxTicketPeriod.Text = dataRow["TicketPeriod"].ToString();
					}
					else
					{
						textBoxTicketPeriod.Text = "0";
					}
					if (dataRow["TicketAmount"] != DBNull.Value)
					{
						textBoxTicketAmount.Text = decimal.Parse(dataRow["TicketAmount"].ToString(), NumberStyles.Any).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTicketAmount.Text = 0.ToString(Format.TotalAmountFormat);
					}
					textBoxRemarks.Text = dataRow["TicketRemarks"].ToString();
					if (dataRow["OverTimeID"] != DBNull.Value)
					{
						comboBoxOvertime.SelectedID = dataRow["OverTimeID"].ToString();
					}
					else
					{
						comboBoxOvertime.Clear();
					}
					if (dataRow["LastRevisedSalaryDate"] != DBNull.Value && dataRow["LastRevisedSalaryDate"].ToString() != "")
					{
						textBoxLastReviseDate.Text = DateTime.Parse(dataRow["LastRevisedSalaryDate"].ToString()).ToShortDateString();
						dateTimePickerDate.Value = DateTime.Parse(dataRow["LastRevisedSalaryDate"].ToString());
					}
					else
					{
						textBoxLastReviseDate.Clear();
					}
					DataTable dataTable = dataGridPayrollItem.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in currentData.Tables["Employee_PayrollItem_Detail"].Rows)
					{
						byte result = 0;
						byte.TryParse(row["PayType"].ToString(), out result);
						if (result == 1)
						{
							DataRow dataRow3 = dataTable.NewRow();
							foreach (DataColumn column in dataRow3.Table.Columns)
							{
								_ = column;
								dataRow3["PayrollItem"] = row["PayrollItemID"];
								dataRow3["Description"] = row["PayrollItemName"];
								if (row["Amount"] != DBNull.Value)
								{
									object obj3 = dataRow3["Amount"] = (dataRow3["New Amount"] = decimal.Round(decimal.Parse(row["Amount"].ToString()), 2));
								}
							}
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
					}
					dataTable.AcceptChanges();
					DataTable dataTable2 = dataGridDeduction.DataSource as DataTable;
					dataTable2.Rows.Clear();
					foreach (DataRow row2 in currentData.Tables["Employee_PayrollItem_Detail"].Rows)
					{
						byte result2 = 0;
						byte.TryParse(row2["PayType"].ToString(), out result2);
						if (result2 == 2)
						{
							DataRow dataRow5 = dataTable2.NewRow();
							foreach (DataColumn column2 in dataRow5.Table.Columns)
							{
								_ = column2;
								dataRow5["Deduction"] = row2["PayrollItemID"];
								dataRow5["Description"] = row2["PayrollItemName"];
								dataRow5["Start Date"] = row2["StartDate"];
								dataRow5["End Date"] = row2["EndDate"];
								if (row2["Amount"] != DBNull.Value)
								{
									object obj3 = dataRow5["Amount"] = (dataRow5["New Amount"] = decimal.Round(decimal.Parse(row2["Amount"].ToString()), 2));
								}
							}
							dataRow5.EndEdit();
							dataTable2.Rows.Add(dataRow5);
						}
					}
					dataTable2.AcceptChanges();
					DataTable dataTable3 = dataGridBenefit.DataSource as DataTable;
					dataTable3.Rows.Clear();
					foreach (DataRow row3 in currentData.Tables["Employee_Benefit_Detail"].Rows)
					{
						DataRow dataRow7 = dataTable3.NewRow();
						foreach (DataColumn column3 in dataRow7.Table.Columns)
						{
							_ = column3;
							dataRow7["Non-Benefit"] = row3["BenefitID"];
							dataRow7["Description"] = row3["BenefitName"];
							dataRow7["Start Date"] = row3["StartDate"];
							dataRow7["End Date"] = row3["EndDate"];
							dataRow7["Remarks"] = row3["Remarks"];
							if (row3["Amount"] != DBNull.Value)
							{
								object obj3 = dataRow7["Amount"] = (dataRow7["New Amount"] = decimal.Round(decimal.Parse(row3["Amount"].ToString()), 2));
							}
						}
						dataRow7.EndEdit();
						dataTable3.Rows.Add(dataRow7);
					}
					dataTable3.AcceptChanges();
					ShowTotalSalary();
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
				bool flag = Factory.EmployeeSystem.UpdateEmployeeSalaryDetails(currentData, IsRevised);
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
			if (comboBoxEmployees.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			foreach (UltraGridRow row in dataGridPayrollItem.Rows)
			{
				if (row.Cells["PayrollItem"].Value == null || row.Cells["PayrollItem"].Value.ToString() == "" || row.Cells["Amount"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please enter required payroll items information.");
					return false;
				}
			}
			foreach (UltraGridRow row2 in dataGridDeduction.Rows)
			{
				if (row2.Cells["Deduction"].Value == null || row2.Cells["Deduction"].Value.ToString() == "" || row2.Cells["Amount"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please enter required deduction items information.");
					return false;
				}
			}
			foreach (UltraGridRow row3 in dataGridBenefit.Rows)
			{
				if (row3.Cells["Non-Benefit"].Value == null || row3.Cells["Non-Benefit"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please enter required benefit items information.");
					return false;
				}
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

		private void ClearForm()
		{
			(dataGridPayrollItem.DataSource as DataTable).Rows.Clear();
			(dataGridDeduction.DataSource as DataTable).Rows.Clear();
			(dataGridBenefit.DataSource as DataTable).Rows.Clear();
			comboBoxCurrency.Clear();
			comboBoxPaymentMethod.SelectedID = -1;
			comboBoxBank.Clear();
			textBoxRemarks.Clear();
			textBoxBankAccountNumber.Clear();
			comboBoxDestination.Clear();
			textBoxTicketAmount.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxTicketPeriod.Text = "0";
			textBoxNumberOfTickets.Text = "0";
			textBoxRemarks.Clear();
			textBoxTotalSalary.Text = 0.ToString(Format.TotalAmountFormat);
			comboBoxOvertime.Clear();
			textBoxLastReviseDate.Clear();
			dateTimePickerDate.Value = DateTime.Now;
			if (!isLoadingNewEmployee)
			{
				comboBoxEmployees.Clear();
				textBoxEmployeeName.Clear();
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
				return Factory.EmployeeSkillSystem.DeleteEmployeeSkill(comboBoxEmployees.SelectedID);
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
			string nextID = DatabaseHelper.GetNextID("Employee", "EmployeeID", comboBoxEmployees.SelectedID);
			if (!(nextID == ""))
			{
				comboBoxEmployees.SelectedID = nextID;
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Employee", "EmployeeID", comboBoxEmployees.SelectedID);
			if (!(previousID == ""))
			{
				comboBoxEmployees.SelectedID = previousID;
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Employee", "EmployeeID");
			if (!(lastID == ""))
			{
				comboBoxEmployees.SelectedID = lastID;
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Employee", "EmployeeID");
			if (!(firstID == ""))
			{
				comboBoxEmployees.SelectedID = firstID;
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
				else if (Factory.DatabaseSystem.ExistFieldValue("Employee", "EmployeeID", toolStripTextBoxFind.Text.Trim()))
				{
					string selectedID = toolStripTextBoxFind.Text.Trim();
					comboBoxEmployees.SelectedID = selectedID;
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

		private void EmployeeSalaryDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxPaymentMethod.LoadData();
				dataGridPayrollItem.SetupUI();
				dataGridDeduction.SetupUI();
				dataGridBenefit.SetupUI();
				SetupPayrollItemGrid();
				SetupDeductionGrid();
				SetupBenefitGrid();
				SetSecurity();
				if (!base.IsDisposed)
				{
					ClearForm();
					dataGridPayrollItem.Enabled = false;
					dataGridBenefit.Enabled = false;
					dataGridDeduction.Enabled = false;
				}
			}
			catch (Exception e2)
			{
				dataGridBenefit.LoadLayoutFailed = true;
				dataGridDeduction.LoadLayoutFailed = true;
				dataGridPayrollItem.LoadLayoutFailed = true;
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
			if (comboBoxEmployees.SelectedRow != null && comboBoxEmployees.SelectedID != "")
			{
				dataGridPayrollItem.Enabled = true;
				dataGridBenefit.Enabled = true;
				dataGridDeduction.Enabled = true;
				textBoxEmployeeName.Text = comboBoxEmployees.SelectedRow.Cells["Name"].Text.ToString();
				if (comboBoxEmployees.Text == "")
				{
					isLoadingNewEmployee = false;
					return;
				}
				if (isLoadingNewEmployee)
				{
					return;
				}
				isLoadingNewEmployee = true;
				if (CanClose())
				{
					ClearForm();
					LoadData(comboBoxEmployees.SelectedID);
				}
				else
				{
					comboBoxEmployees.SelectedID = comboBoxEmployees.OldValue;
				}
			}
			else
			{
				dataGridPayrollItem.Enabled = false;
				dataGridBenefit.Enabled = false;
				dataGridDeduction.Enabled = false;
				textBoxEmployeeName.Clear();
				ClearForm();
			}
			isLoadingNewEmployee = false;
		}

		private void textBoxBasic_TextChanged(object sender, EventArgs e)
		{
			ShowTotalSalary();
		}

		private void ShowTotalSalary()
		{
			decimal num = default(decimal);
			foreach (UltraGridRow row in dataGridPayrollItem.Rows)
			{
				decimal result = default(decimal);
				decimal.TryParse(row.Cells["Amount"].Text, out result);
				num += result;
			}
			textBoxTotalSalary.Text = num.ToString(Format.TotalAmountFormat);
		}

		private void linkLabelCountry_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBank(comboBoxBank.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(comboBoxCurrency.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditDestination(comboBoxDestination.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEOSRule(comboBoxDestination.SelectedID);
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
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployees.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPaymentMethod(comboBoxPaymentMethod.SelectedID.ToString());
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.EmployeeSalaryListFormObj);
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
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData()))
				{
					string selectedID = comboBoxEmployees.SelectedID;
					DataSet employeeSalaryDetailsByEmployeeID = Factory.EmployeeSystem.GetEmployeeSalaryDetailsByEmployeeID(selectedID);
					if (employeeSalaryDetailsByEmployeeID == null || employeeSalaryDetailsByEmployeeID.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(employeeSalaryDetailsByEmployeeID, "", "Salary Details", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
		}

		public void SetDataFromCandidate(DataTable payrollItems, DataTable DedeuctionItems, DataTable Benefits)
		{
			dataGridPayrollItem.DataSource = payrollItems;
			dataGridDeduction.DataSource = DedeuctionItems;
			dataGridBenefit.DataSource = Benefits;
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeSalaryDetailsForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridPayrollItem = new Micromind.DataControls.DataEntryGrid();
			comboBoxPayrollItem = new Micromind.DataControls.PayrollItemComboBox();
			textBoxTotalSalary = new Micromind.UISupport.AmountTextBox();
			label7 = new System.Windows.Forms.Label();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxDeduction = new Micromind.DataControls.PayrollItemComboBox();
			dataGridDeduction = new Micromind.DataControls.DataEntryGrid();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxBenefit = new Micromind.DataControls.BenefitComboBox();
			dataGridBenefit = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxTicketRemarks = new System.Windows.Forms.TextBox();
			label10 = new System.Windows.Forms.Label();
			textBoxTicketPeriod = new Micromind.UISupport.NumberTextBox();
			textBoxNumberOfTickets = new Micromind.UISupport.NumberTextBox();
			textBoxTicketAmount = new Micromind.UISupport.AmountTextBox();
			label5 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxDestination = new Micromind.DataControls.DestinationComboBox();
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
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonClear = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			label4 = new System.Windows.Forms.Label();
			textBoxBankAccountNumber = new System.Windows.Forms.TextBox();
			textBoxRemarks = new System.Windows.Forms.TextBox();
			label8 = new System.Windows.Forms.Label();
			linkLabelCountry = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			nonDirtyPanel1 = new Micromind.UISupport.NonDirtyPanel(components);
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			comboBoxEmployees = new Micromind.DataControls.EmployeeComboBox();
			labelReviewDate = new System.Windows.Forms.Label();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			label11 = new System.Windows.Forms.Label();
			textBoxLastReviseDate = new Micromind.UISupport.MMTextBox();
			panelReviseDate = new Micromind.UISupport.NonDirtyPanel(components);
			comboBoxOvertime = new Micromind.DataControls.OverTimeComboBox();
			comboBoxEOS = new Micromind.DataControls.EOSRuleComboBox();
			comboBoxBank = new Micromind.DataControls.BankComboBox();
			comboBoxCurrency = new Micromind.DataControls.CurrencyComboBox();
			comboBoxPaymentMethod = new Micromind.DataControls.EmployeePaymentMethodComboBox();
			formManager = new Micromind.DataControls.FormManager();
			label1 = new System.Windows.Forms.Label();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridPayrollItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItem).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDeduction).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridDeduction).BeginInit();
			tabPageUserDefined.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBenefit).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridBenefit).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDestination).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			nonDirtyPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployees).BeginInit();
			panelReviseDate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxOvertime).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEOS).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).BeginInit();
			SuspendLayout();
			tabPageGeneral.Controls.Add(dataGridPayrollItem);
			tabPageGeneral.Controls.Add(comboBoxPayrollItem);
			tabPageGeneral.Controls.Add(textBoxTotalSalary);
			tabPageGeneral.Controls.Add(label7);
			tabPageGeneral.Location = new System.Drawing.Point(1, 20);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(710, 285);
			dataGridPayrollItem.AllowAddNew = false;
			dataGridPayrollItem.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPayrollItem.DisplayLayout.Appearance = appearance;
			dataGridPayrollItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPayrollItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayrollItem.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayrollItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridPayrollItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayrollItem.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridPayrollItem.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPayrollItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPayrollItem.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPayrollItem.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridPayrollItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridPayrollItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPayrollItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridPayrollItem.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPayrollItem.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridPayrollItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPayrollItem.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayrollItem.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridPayrollItem.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridPayrollItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPayrollItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridPayrollItem.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridPayrollItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPayrollItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridPayrollItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPayrollItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPayrollItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPayrollItem.IncludeLotItems = false;
			dataGridPayrollItem.LoadLayoutFailed = false;
			dataGridPayrollItem.Location = new System.Drawing.Point(12, 17);
			dataGridPayrollItem.Name = "dataGridPayrollItem";
			dataGridPayrollItem.ShowClearMenu = true;
			dataGridPayrollItem.ShowDeleteMenu = true;
			dataGridPayrollItem.ShowInsertMenu = true;
			dataGridPayrollItem.ShowMoveRowsMenu = true;
			dataGridPayrollItem.Size = new System.Drawing.Size(687, 244);
			dataGridPayrollItem.TabIndex = 17;
			dataGridPayrollItem.Text = "dataEntryGrid1";
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
			textBoxTotalSalary.AllowDecimal = true;
			textBoxTotalSalary.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalSalary.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalSalary.CustomReportFieldName = "";
			textBoxTotalSalary.CustomReportKey = "";
			textBoxTotalSalary.CustomReportValueType = 1;
			textBoxTotalSalary.ForeColor = System.Drawing.Color.Black;
			textBoxTotalSalary.IsComboTextBox = false;
			textBoxTotalSalary.IsModified = false;
			textBoxTotalSalary.Location = new System.Drawing.Point(585, 261);
			textBoxTotalSalary.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalSalary.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalSalary.Name = "textBoxTotalSalary";
			textBoxTotalSalary.NullText = "0";
			textBoxTotalSalary.ReadOnly = true;
			textBoxTotalSalary.Size = new System.Drawing.Size(114, 21);
			textBoxTotalSalary.TabIndex = 9;
			textBoxTotalSalary.TabStop = false;
			textBoxTotalSalary.Text = "0.00";
			textBoxTotalSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalSalary.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label7.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(507, 264);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(68, 13);
			label7.TabIndex = 27;
			label7.Text = "Total Salary:";
			tabPageDetails.Controls.Add(comboBoxDeduction);
			tabPageDetails.Controls.Add(dataGridDeduction);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(710, 285);
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
			dataGridDeduction.Location = new System.Drawing.Point(12, 17);
			dataGridDeduction.Name = "dataGridDeduction";
			dataGridDeduction.ShowClearMenu = true;
			dataGridDeduction.ShowDeleteMenu = true;
			dataGridDeduction.ShowInsertMenu = true;
			dataGridDeduction.ShowMoveRowsMenu = true;
			dataGridDeduction.Size = new System.Drawing.Size(691, 240);
			dataGridDeduction.TabIndex = 18;
			dataGridDeduction.Text = "dataEntryGrid1";
			tabPageUserDefined.Controls.Add(comboBoxBenefit);
			tabPageUserDefined.Controls.Add(dataGridBenefit);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(710, 285);
			comboBoxBenefit.Assigned = false;
			comboBoxBenefit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBenefit.CustomReportFieldName = "";
			comboBoxBenefit.CustomReportKey = "";
			comboBoxBenefit.CustomReportValueType = 1;
			comboBoxBenefit.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBenefit.DisplayLayout.Appearance = appearance49;
			comboBoxBenefit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBenefit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBenefit.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBenefit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxBenefit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBenefit.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxBenefit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBenefit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBenefit.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBenefit.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxBenefit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBenefit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBenefit.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBenefit.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxBenefit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBenefit.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBenefit.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxBenefit.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxBenefit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBenefit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxBenefit.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxBenefit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBenefit.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
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
			dataGridBenefit.AllowAddNew = false;
			dataGridBenefit.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridBenefit.DisplayLayout.Appearance = appearance61;
			dataGridBenefit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridBenefit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			dataGridBenefit.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridBenefit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			dataGridBenefit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridBenefit.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			dataGridBenefit.DisplayLayout.MaxColScrollRegions = 1;
			dataGridBenefit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridBenefit.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridBenefit.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			dataGridBenefit.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridBenefit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridBenefit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			dataGridBenefit.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridBenefit.DisplayLayout.Override.CellAppearance = appearance68;
			dataGridBenefit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridBenefit.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			dataGridBenefit.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			dataGridBenefit.DisplayLayout.Override.HeaderAppearance = appearance70;
			dataGridBenefit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridBenefit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			dataGridBenefit.DisplayLayout.Override.RowAppearance = appearance71;
			dataGridBenefit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridBenefit.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			dataGridBenefit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridBenefit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridBenefit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridBenefit.IncludeLotItems = false;
			dataGridBenefit.LoadLayoutFailed = false;
			dataGridBenefit.Location = new System.Drawing.Point(12, 17);
			dataGridBenefit.Name = "dataGridBenefit";
			dataGridBenefit.ShowClearMenu = true;
			dataGridBenefit.ShowDeleteMenu = true;
			dataGridBenefit.ShowInsertMenu = true;
			dataGridBenefit.ShowMoveRowsMenu = true;
			dataGridBenefit.Size = new System.Drawing.Size(689, 235);
			dataGridBenefit.TabIndex = 18;
			dataGridBenefit.Text = "dataEntryGrid1";
			ultraTabPageControl1.Controls.Add(textBoxTicketRemarks);
			ultraTabPageControl1.Controls.Add(label10);
			ultraTabPageControl1.Controls.Add(textBoxTicketPeriod);
			ultraTabPageControl1.Controls.Add(textBoxNumberOfTickets);
			ultraTabPageControl1.Controls.Add(textBoxTicketAmount);
			ultraTabPageControl1.Controls.Add(label5);
			ultraTabPageControl1.Controls.Add(label9);
			ultraTabPageControl1.Controls.Add(label6);
			ultraTabPageControl1.Controls.Add(label3);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel3);
			ultraTabPageControl1.Controls.Add(comboBoxDestination);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(710, 285);
			textBoxTicketRemarks.Location = new System.Drawing.Point(127, 109);
			textBoxTicketRemarks.MaxLength = 255;
			textBoxTicketRemarks.Name = "textBoxTicketRemarks";
			textBoxTicketRemarks.Size = new System.Drawing.Size(396, 21);
			textBoxTicketRemarks.TabIndex = 4;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(10, 112);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(52, 13);
			label10.TabIndex = 64;
			label10.Text = "Remarks:";
			textBoxTicketPeriod.AllowDecimal = false;
			textBoxTicketPeriod.CustomReportFieldName = "";
			textBoxTicketPeriod.CustomReportKey = "";
			textBoxTicketPeriod.CustomReportValueType = 1;
			textBoxTicketPeriod.IsComboTextBox = false;
			textBoxTicketPeriod.IsModified = false;
			textBoxTicketPeriod.Location = new System.Drawing.Point(127, 63);
			textBoxTicketPeriod.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTicketPeriod.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTicketPeriod.Name = "textBoxTicketPeriod";
			textBoxTicketPeriod.NullText = "0";
			textBoxTicketPeriod.Size = new System.Drawing.Size(114, 21);
			textBoxTicketPeriod.TabIndex = 2;
			textBoxTicketPeriod.Text = "0";
			textBoxTicketPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxNumberOfTickets.AllowDecimal = false;
			textBoxNumberOfTickets.CustomReportFieldName = "";
			textBoxNumberOfTickets.CustomReportKey = "";
			textBoxNumberOfTickets.CustomReportValueType = 1;
			textBoxNumberOfTickets.IsComboTextBox = false;
			textBoxNumberOfTickets.IsModified = false;
			textBoxNumberOfTickets.Location = new System.Drawing.Point(127, 40);
			textBoxNumberOfTickets.MaxLength = 2;
			textBoxNumberOfTickets.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxNumberOfTickets.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxNumberOfTickets.Name = "textBoxNumberOfTickets";
			textBoxNumberOfTickets.NullText = "0";
			textBoxNumberOfTickets.Size = new System.Drawing.Size(114, 21);
			textBoxNumberOfTickets.TabIndex = 1;
			textBoxNumberOfTickets.Text = "0";
			textBoxNumberOfTickets.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTicketAmount.AllowDecimal = true;
			textBoxTicketAmount.CustomReportFieldName = "";
			textBoxTicketAmount.CustomReportKey = "";
			textBoxTicketAmount.CustomReportValueType = 1;
			textBoxTicketAmount.IsComboTextBox = false;
			textBoxTicketAmount.IsModified = false;
			textBoxTicketAmount.Location = new System.Drawing.Point(127, 86);
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
			textBoxTicketAmount.Size = new System.Drawing.Size(114, 21);
			textBoxTicketAmount.TabIndex = 3;
			textBoxTicketAmount.Text = "0.00";
			textBoxTicketAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTicketAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(10, 89);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(48, 13);
			label5.TabIndex = 60;
			label5.Text = "Amount:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(247, 66);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(31, 13);
			label9.TabIndex = 60;
			label9.Text = "Days";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(10, 66);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(72, 13);
			label6.TabIndex = 60;
			label6.Text = "Ticket Period:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(10, 43);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(97, 13);
			label3.TabIndex = 60;
			label3.Text = "Number of Tickets:";
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(10, 20);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(63, 15);
			ultraFormattedLinkLabel3.TabIndex = 59;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Destination:";
			appearance73.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance73;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			comboBoxDestination.Assigned = false;
			comboBoxDestination.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDestination.CustomReportFieldName = "";
			comboBoxDestination.CustomReportKey = "";
			comboBoxDestination.CustomReportValueType = 1;
			comboBoxDestination.DescriptionTextBox = null;
			appearance74.BackColor = System.Drawing.SystemColors.Window;
			appearance74.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDestination.DisplayLayout.Appearance = appearance74;
			comboBoxDestination.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDestination.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance75.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance75.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance75.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDestination.DisplayLayout.GroupByBox.Appearance = appearance75;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDestination.DisplayLayout.GroupByBox.BandLabelAppearance = appearance76;
			comboBoxDestination.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance77.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance77.BackColor2 = System.Drawing.SystemColors.Control;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDestination.DisplayLayout.GroupByBox.PromptAppearance = appearance77;
			comboBoxDestination.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDestination.DisplayLayout.MaxRowScrollRegions = 1;
			appearance78.BackColor = System.Drawing.SystemColors.Window;
			appearance78.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDestination.DisplayLayout.Override.ActiveCellAppearance = appearance78;
			appearance79.BackColor = System.Drawing.SystemColors.Highlight;
			appearance79.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDestination.DisplayLayout.Override.ActiveRowAppearance = appearance79;
			comboBoxDestination.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDestination.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance80.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDestination.DisplayLayout.Override.CardAreaAppearance = appearance80;
			appearance81.BorderColor = System.Drawing.Color.Silver;
			appearance81.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDestination.DisplayLayout.Override.CellAppearance = appearance81;
			comboBoxDestination.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDestination.DisplayLayout.Override.CellPadding = 0;
			appearance82.BackColor = System.Drawing.SystemColors.Control;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDestination.DisplayLayout.Override.GroupByRowAppearance = appearance82;
			appearance83.TextHAlignAsString = "Left";
			comboBoxDestination.DisplayLayout.Override.HeaderAppearance = appearance83;
			comboBoxDestination.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDestination.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance84.BackColor = System.Drawing.SystemColors.Window;
			appearance84.BorderColor = System.Drawing.Color.Silver;
			comboBoxDestination.DisplayLayout.Override.RowAppearance = appearance84;
			comboBoxDestination.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance85.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDestination.DisplayLayout.Override.TemplateAddRowAppearance = appearance85;
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
			comboBoxDestination.Size = new System.Drawing.Size(114, 21);
			comboBoxDestination.TabIndex = 0;
			comboBoxDestination.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(buttonClear);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 513);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(725, 40);
			panelButtons.TabIndex = 11;
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
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			ultraTabControl1.Location = new System.Drawing.Point(9, 201);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(712, 306);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControl1.TabIndex = 10;
			appearance86.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance86;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&PayrollItems";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Deductions";
			ultraTab3.TabPage = tabPageUserDefined;
			ultraTab3.Text = "Other Non-Fin &Benefits";
			ultraTab4.TabPage = ultraTabPageControl1;
			ultraTab4.Text = "&Tickets";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4
			});
			ultraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Standard;
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(710, 285);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(284, 93);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(97, 13);
			label4.TabIndex = 22;
			label4.Text = "Bank A/C Number:";
			textBoxBankAccountNumber.Location = new System.Drawing.Point(387, 91);
			textBoxBankAccountNumber.MaxLength = 30;
			textBoxBankAccountNumber.Name = "textBoxBankAccountNumber";
			textBoxBankAccountNumber.ReadOnly = true;
			textBoxBankAccountNumber.Size = new System.Drawing.Size(144, 20);
			textBoxBankAccountNumber.TabIndex = 4;
			textBoxRemarks.Location = new System.Drawing.Point(135, 113);
			textBoxRemarks.MaxLength = 255;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.Size = new System.Drawing.Size(396, 44);
			textBoxRemarks.TabIndex = 9;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(14, 127);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(52, 13);
			label8.TabIndex = 22;
			label8.Text = "Remarks:";
			linkLabelCountry.AutoSize = true;
			linkLabelCountry.Location = new System.Drawing.Point(14, 91);
			linkLabelCountry.Name = "linkLabelCountry";
			linkLabelCountry.Size = new System.Drawing.Size(32, 14);
			linkLabelCountry.TabIndex = 57;
			linkLabelCountry.TabStop = true;
			linkLabelCountry.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCountry.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCountry.Value = "Bank:";
			appearance87.ForeColor = System.Drawing.Color.Blue;
			linkLabelCountry.VisitedLinkAppearance = appearance87;
			linkLabelCountry.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCountry_LinkClicked);
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(568, 43);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(52, 14);
			ultraFormattedLinkLabel1.TabIndex = 58;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Currency:";
			ultraFormattedLinkLabel1.Visible = false;
			appearance88.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance88;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(14, 113);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(104, 14);
			ultraFormattedLinkLabel2.TabIndex = 60;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "End of Service Rule:";
			ultraFormattedLinkLabel2.Visible = false;
			appearance89.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance89;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(287, 115);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(52, 14);
			ultraFormattedLinkLabel4.TabIndex = 62;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Overtime:";
			ultraFormattedLinkLabel4.Visible = false;
			appearance90.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance90;
			nonDirtyPanel1.Controls.Add(ultraFormattedLinkLabel5);
			nonDirtyPanel1.Controls.Add(textBoxEmployeeName);
			nonDirtyPanel1.Controls.Add(comboBoxEmployees);
			nonDirtyPanel1.Location = new System.Drawing.Point(0, 33);
			nonDirtyPanel1.Name = "nonDirtyPanel1";
			nonDirtyPanel1.Size = new System.Drawing.Size(536, 32);
			nonDirtyPanel1.TabIndex = 0;
			appearance91.FontData.BoldAsString = "True";
			appearance91.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance91;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(18, 7);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(93, 15);
			ultraFormattedLinkLabel5.TabIndex = 4;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Employee Code:";
			appearance92.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance92;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.ForeColor = System.Drawing.Color.Black;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.IsModified = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(281, 7);
			textBoxEmployeeName.MaxLength = 255;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(250, 20);
			textBoxEmployeeName.TabIndex = 3;
			textBoxEmployeeName.TabStop = false;
			comboBoxEmployees.Assigned = false;
			comboBoxEmployees.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployees.CustomReportFieldName = "";
			comboBoxEmployees.CustomReportKey = "";
			comboBoxEmployees.CustomReportValueType = 1;
			comboBoxEmployees.DescriptionTextBox = null;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployees.DisplayLayout.Appearance = appearance93;
			comboBoxEmployees.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployees.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployees.DisplayLayout.GroupByBox.Appearance = appearance94;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployees.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
			comboBoxEmployees.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance96.BackColor2 = System.Drawing.SystemColors.Control;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployees.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
			comboBoxEmployees.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployees.DisplayLayout.MaxRowScrollRegions = 1;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployees.DisplayLayout.Override.ActiveCellAppearance = appearance97;
			appearance98.BackColor = System.Drawing.SystemColors.Highlight;
			appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployees.DisplayLayout.Override.ActiveRowAppearance = appearance98;
			comboBoxEmployees.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployees.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployees.DisplayLayout.Override.CardAreaAppearance = appearance99;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployees.DisplayLayout.Override.CellAppearance = appearance100;
			comboBoxEmployees.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployees.DisplayLayout.Override.CellPadding = 0;
			appearance101.BackColor = System.Drawing.SystemColors.Control;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployees.DisplayLayout.Override.GroupByRowAppearance = appearance101;
			appearance102.TextHAlignAsString = "Left";
			comboBoxEmployees.DisplayLayout.Override.HeaderAppearance = appearance102;
			comboBoxEmployees.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployees.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployees.DisplayLayout.Override.RowAppearance = appearance103;
			comboBoxEmployees.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployees.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
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
			labelReviewDate.AutoSize = true;
			labelReviewDate.Location = new System.Drawing.Point(284, 70);
			labelReviewDate.Name = "labelReviewDate";
			labelReviewDate.Size = new System.Drawing.Size(72, 13);
			labelReviewDate.TabIndex = 63;
			labelReviewDate.Text = "Review Date:";
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(387, 68);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(144, 20);
			dateTimePickerDate.TabIndex = 2;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(6, 6);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(98, 13);
			label11.TabIndex = 65;
			label11.Text = "Last Revised Date:";
			textBoxLastReviseDate.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLastReviseDate.CustomReportFieldName = "";
			textBoxLastReviseDate.CustomReportKey = "";
			textBoxLastReviseDate.CustomReportValueType = 1;
			textBoxLastReviseDate.ForeColor = System.Drawing.Color.Black;
			textBoxLastReviseDate.IsComboTextBox = false;
			textBoxLastReviseDate.IsModified = false;
			textBoxLastReviseDate.Location = new System.Drawing.Point(126, 3);
			textBoxLastReviseDate.MaxLength = 255;
			textBoxLastReviseDate.Name = "textBoxLastReviseDate";
			textBoxLastReviseDate.ReadOnly = true;
			textBoxLastReviseDate.Size = new System.Drawing.Size(142, 20);
			textBoxLastReviseDate.TabIndex = 7;
			textBoxLastReviseDate.TabStop = false;
			panelReviseDate.Controls.Add(textBoxLastReviseDate);
			panelReviseDate.Controls.Add(label11);
			panelReviseDate.Location = new System.Drawing.Point(9, 161);
			panelReviseDate.Name = "panelReviseDate";
			panelReviseDate.Size = new System.Drawing.Size(278, 26);
			panelReviseDate.TabIndex = 66;
			panelReviseDate.Visible = false;
			comboBoxOvertime.Assigned = false;
			comboBoxOvertime.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxOvertime.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxOvertime.CustomReportFieldName = "";
			comboBoxOvertime.CustomReportKey = "";
			comboBoxOvertime.CustomReportValueType = 1;
			comboBoxOvertime.DescriptionTextBox = null;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxOvertime.DisplayLayout.Appearance = appearance105;
			comboBoxOvertime.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxOvertime.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOvertime.DisplayLayout.GroupByBox.Appearance = appearance106;
			appearance107.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOvertime.DisplayLayout.GroupByBox.BandLabelAppearance = appearance107;
			comboBoxOvertime.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance108.BackColor2 = System.Drawing.SystemColors.Control;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOvertime.DisplayLayout.GroupByBox.PromptAppearance = appearance108;
			comboBoxOvertime.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxOvertime.DisplayLayout.MaxRowScrollRegions = 1;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxOvertime.DisplayLayout.Override.ActiveCellAppearance = appearance109;
			appearance110.BackColor = System.Drawing.SystemColors.Highlight;
			appearance110.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxOvertime.DisplayLayout.Override.ActiveRowAppearance = appearance110;
			comboBoxOvertime.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxOvertime.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			comboBoxOvertime.DisplayLayout.Override.CardAreaAppearance = appearance111;
			appearance112.BorderColor = System.Drawing.Color.Silver;
			appearance112.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxOvertime.DisplayLayout.Override.CellAppearance = appearance112;
			comboBoxOvertime.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxOvertime.DisplayLayout.Override.CellPadding = 0;
			appearance113.BackColor = System.Drawing.SystemColors.Control;
			appearance113.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance113.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance113.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOvertime.DisplayLayout.Override.GroupByRowAppearance = appearance113;
			appearance114.TextHAlignAsString = "Left";
			comboBoxOvertime.DisplayLayout.Override.HeaderAppearance = appearance114;
			comboBoxOvertime.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxOvertime.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.BorderColor = System.Drawing.Color.Silver;
			comboBoxOvertime.DisplayLayout.Override.RowAppearance = appearance115;
			comboBoxOvertime.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance116.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxOvertime.DisplayLayout.Override.TemplateAddRowAppearance = appearance116;
			comboBoxOvertime.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxOvertime.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxOvertime.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxOvertime.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxOvertime.Editable = true;
			comboBoxOvertime.FilterString = "";
			comboBoxOvertime.HasAllAccount = false;
			comboBoxOvertime.HasCustom = false;
			comboBoxOvertime.IsDataLoaded = false;
			comboBoxOvertime.Location = new System.Drawing.Point(387, 113);
			comboBoxOvertime.MaxDropDownItems = 12;
			comboBoxOvertime.Name = "comboBoxOvertime";
			comboBoxOvertime.ShowInactiveItems = false;
			comboBoxOvertime.ShowQuickAdd = true;
			comboBoxOvertime.Size = new System.Drawing.Size(144, 20);
			comboBoxOvertime.TabIndex = 6;
			comboBoxOvertime.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxOvertime.Visible = false;
			comboBoxEOS.Assigned = false;
			comboBoxEOS.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxEOS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEOS.CustomReportFieldName = "";
			comboBoxEOS.CustomReportKey = "";
			comboBoxEOS.CustomReportValueType = 1;
			comboBoxEOS.DescriptionTextBox = null;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEOS.DisplayLayout.Appearance = appearance117;
			comboBoxEOS.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEOS.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance118.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEOS.DisplayLayout.GroupByBox.Appearance = appearance118;
			appearance119.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEOS.DisplayLayout.GroupByBox.BandLabelAppearance = appearance119;
			comboBoxEOS.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance120.BackColor2 = System.Drawing.SystemColors.Control;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEOS.DisplayLayout.GroupByBox.PromptAppearance = appearance120;
			comboBoxEOS.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEOS.DisplayLayout.MaxRowScrollRegions = 1;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEOS.DisplayLayout.Override.ActiveCellAppearance = appearance121;
			appearance122.BackColor = System.Drawing.SystemColors.Highlight;
			appearance122.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEOS.DisplayLayout.Override.ActiveRowAppearance = appearance122;
			comboBoxEOS.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEOS.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEOS.DisplayLayout.Override.CardAreaAppearance = appearance123;
			appearance124.BorderColor = System.Drawing.Color.Silver;
			appearance124.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEOS.DisplayLayout.Override.CellAppearance = appearance124;
			comboBoxEOS.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEOS.DisplayLayout.Override.CellPadding = 0;
			appearance125.BackColor = System.Drawing.SystemColors.Control;
			appearance125.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance125.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance125.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEOS.DisplayLayout.Override.GroupByRowAppearance = appearance125;
			appearance126.TextHAlignAsString = "Left";
			comboBoxEOS.DisplayLayout.Override.HeaderAppearance = appearance126;
			comboBoxEOS.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEOS.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.BorderColor = System.Drawing.Color.Silver;
			comboBoxEOS.DisplayLayout.Override.RowAppearance = appearance127;
			comboBoxEOS.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance128.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEOS.DisplayLayout.Override.TemplateAddRowAppearance = appearance128;
			comboBoxEOS.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEOS.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEOS.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEOS.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEOS.Editable = true;
			comboBoxEOS.FilterString = "";
			comboBoxEOS.HasAllAccount = false;
			comboBoxEOS.HasCustom = false;
			comboBoxEOS.IsDataLoaded = false;
			comboBoxEOS.Location = new System.Drawing.Point(137, 114);
			comboBoxEOS.MaxDropDownItems = 12;
			comboBoxEOS.Name = "comboBoxEOS";
			comboBoxEOS.ShowInactiveItems = false;
			comboBoxEOS.ShowQuickAdd = true;
			comboBoxEOS.Size = new System.Drawing.Size(142, 20);
			comboBoxEOS.TabIndex = 5;
			comboBoxEOS.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEOS.Visible = false;
			comboBoxBank.Assigned = false;
			comboBoxBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBank.CustomReportFieldName = "";
			comboBoxBank.CustomReportKey = "";
			comboBoxBank.CustomReportValueType = 1;
			comboBoxBank.DescriptionTextBox = null;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBank.DisplayLayout.Appearance = appearance129;
			comboBoxBank.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBank.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance130.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance130.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance130.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.GroupByBox.Appearance = appearance130;
			appearance131.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.BandLabelAppearance = appearance131;
			comboBoxBank.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance132.BackColor2 = System.Drawing.SystemColors.Control;
			appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.PromptAppearance = appearance132;
			comboBoxBank.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBank.DisplayLayout.MaxRowScrollRegions = 1;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBank.DisplayLayout.Override.ActiveCellAppearance = appearance133;
			appearance134.BackColor = System.Drawing.SystemColors.Highlight;
			appearance134.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBank.DisplayLayout.Override.ActiveRowAppearance = appearance134;
			comboBoxBank.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBank.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.CardAreaAppearance = appearance135;
			appearance136.BorderColor = System.Drawing.Color.Silver;
			appearance136.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBank.DisplayLayout.Override.CellAppearance = appearance136;
			comboBoxBank.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBank.DisplayLayout.Override.CellPadding = 0;
			appearance137.BackColor = System.Drawing.SystemColors.Control;
			appearance137.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance137.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance137.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance137.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.GroupByRowAppearance = appearance137;
			appearance138.TextHAlignAsString = "Left";
			comboBoxBank.DisplayLayout.Override.HeaderAppearance = appearance138;
			comboBoxBank.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBank.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.BorderColor = System.Drawing.Color.Silver;
			comboBoxBank.DisplayLayout.Override.RowAppearance = appearance139;
			comboBoxBank.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance140.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBank.DisplayLayout.Override.TemplateAddRowAppearance = appearance140;
			comboBoxBank.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBank.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBank.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBank.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBank.Editable = true;
			comboBoxBank.FilterString = "";
			comboBoxBank.HasAllAccount = false;
			comboBoxBank.HasCustom = false;
			comboBoxBank.IsDataLoaded = false;
			comboBoxBank.Location = new System.Drawing.Point(135, 90);
			comboBoxBank.MaxDropDownItems = 12;
			comboBoxBank.Name = "comboBoxBank";
			comboBoxBank.ReadOnly = true;
			comboBoxBank.ShowInactiveItems = false;
			comboBoxBank.ShowQuickAdd = true;
			comboBoxBank.Size = new System.Drawing.Size(142, 20);
			comboBoxBank.TabIndex = 3;
			comboBoxBank.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCurrency.Assigned = false;
			comboBoxCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCurrency.CustomReportFieldName = "";
			comboBoxCurrency.CustomReportKey = "";
			comboBoxCurrency.CustomReportValueType = 1;
			comboBoxCurrency.DescriptionTextBox = null;
			appearance141.BackColor = System.Drawing.SystemColors.Window;
			appearance141.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCurrency.DisplayLayout.Appearance = appearance141;
			comboBoxCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance142.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance142.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance142.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance142.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.GroupByBox.Appearance = appearance142;
			appearance143.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance143;
			comboBoxCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance144.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance144.BackColor2 = System.Drawing.SystemColors.Control;
			appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance144.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance144;
			comboBoxCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance145;
			appearance146.BackColor = System.Drawing.SystemColors.Highlight;
			appearance146.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance146;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance147.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.CardAreaAppearance = appearance147;
			appearance148.BorderColor = System.Drawing.Color.Silver;
			appearance148.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCurrency.DisplayLayout.Override.CellAppearance = appearance148;
			comboBoxCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance149.BackColor = System.Drawing.SystemColors.Control;
			appearance149.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance149.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance149.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance149.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance149;
			appearance150.TextHAlignAsString = "Left";
			comboBoxCurrency.DisplayLayout.Override.HeaderAppearance = appearance150;
			comboBoxCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			appearance151.BorderColor = System.Drawing.Color.Silver;
			comboBoxCurrency.DisplayLayout.Override.RowAppearance = appearance151;
			comboBoxCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance152.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance152;
			comboBoxCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCurrency.Editable = true;
			comboBoxCurrency.FilterString = "";
			comboBoxCurrency.HasAllAccount = false;
			comboBoxCurrency.HasCustom = false;
			comboBoxCurrency.IsDataLoaded = false;
			comboBoxCurrency.Location = new System.Drawing.Point(568, 63);
			comboBoxCurrency.MaxDropDownItems = 12;
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.ShowInactiveItems = false;
			comboBoxCurrency.ShowQuickAdd = true;
			comboBoxCurrency.Size = new System.Drawing.Size(114, 20);
			comboBoxCurrency.TabIndex = 4;
			comboBoxCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCurrency.Visible = false;
			comboBoxPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxPaymentMethod.FormattingEnabled = true;
			comboBoxPaymentMethod.Location = new System.Drawing.Point(135, 67);
			comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
			comboBoxPaymentMethod.SelectedID = 0;
			comboBoxPaymentMethod.Size = new System.Drawing.Size(142, 21);
			comboBoxPaymentMethod.TabIndex = 1;
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
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(14, 70);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(90, 13);
			label1.TabIndex = 68;
			label1.Text = "Payment Method:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(725, 553);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxRemarks);
			base.Controls.Add(panelReviseDate);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(labelReviewDate);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(comboBoxOvertime);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(comboBoxEOS);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(linkLabelCountry);
			base.Controls.Add(comboBoxBank);
			base.Controls.Add(comboBoxCurrency);
			base.Controls.Add(textBoxBankAccountNumber);
			base.Controls.Add(label8);
			base.Controls.Add(label4);
			base.Controls.Add(comboBoxPaymentMethod);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(nonDirtyPanel1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "EmployeeSalaryDetailsForm";
			Text = "Employee Salary Detail";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridPayrollItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayrollItem).EndInit();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDeduction).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridDeduction).EndInit();
			tabPageUserDefined.ResumeLayout(false);
			tabPageUserDefined.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBenefit).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridBenefit).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDestination).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			nonDirtyPanel1.ResumeLayout(false);
			nonDirtyPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployees).EndInit();
			panelReviseDate.ResumeLayout(false);
			panelReviseDate.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxOvertime).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEOS).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
