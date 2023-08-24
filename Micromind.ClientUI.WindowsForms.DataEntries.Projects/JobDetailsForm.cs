using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Projects
{
	public class JobDetailsForm : Form, IForm
	{
		private JobData currentData;

		private const string TABLENAME_CONST = "Job";

		private const string IDFIELD_CONST = "JobID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

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

		private ToolStripButton toolStripButtonFind;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMLabel labelStartDate;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimeEndDate;

		private DateTimePicker dateTimeStartDate;

		private customersFlatComboBox comboBoxCutomers;

		private MMLabel labelJobStatus;

		private JobStatusComboBox comboBoxStatus;

		private MMTextBox textBoxCustomerName;

		private ToolStripButton toolStripButtonAttach;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private MMLabel mmLabel3;

		private MMTextBox textBoxReference;

		private UltraFormattedLinkLabel ultraLinkProjectMgr;

		private UltraFormattedLinkLabel ultraLinkSalesPerson;

		private SalespersonComboBox comboBoxSalesperson;

		private UltraTabPageControl tabPageSummary;

		private UltraTabPageControl tabPageUserDefined;

		private Panel panel1;

		private UltraFormattedLinkLabel ultraLinkCurrency;

		private CurrencyComboBox comboBoxCurrency;

		private MMLabel mmLabel5;

		private MMTextBox textBoxPONumber;

		private MMLabel mmLabel9;

		private MMLabel mmLabel8;

		private MMLabel mmLabel7;

		private MMLabel mmLabel6;

		private UltraTabPageControl tabPageNote;

		private MMTextBox textBoxNote;

		private AmountTextBox textBoxProjectAmount;

		private MMLabel mmLabel4;

		private Panel panel2;

		private XPButton buttonBudget;

		private XPButton buttonFees;

		private UltraTabPageControl tabPageAccounts;

		private MMTextBox textBoxIncomeAccountName;

		private AllAccountsComboBox comboBoxIncomeAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private MMTextBox textBoxWIPAccountName;

		private AllAccountsComboBox comboBoxWIPAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private EmployeeComboBox comboBoxManager;

		private UltraFormattedLinkLabel ultraLinkJobType;

		private JobTypeComboBox comboBoxJobType;

		private UltraGroupBox ultraGroupBox1;

		private MMLabel mmLabel10;

		private AmountTextBox textBoxProjectBudget;

		private UDFEntryControl udfEntryGrid;

		private MMTextBox textBoxTimesheetAccountName;

		private AllAccountsComboBox comboBoxTimesheetAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMTextBox textBoxRetentionAccountName;

		private AllAccountsComboBox comboBoxRetentionAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private MMLabel mmLabel12;

		private MMLabel mmLabel11;

		private AmountTextBox textBoxRetention;

		private MMLabel mmLabel13;

		private AmountTextBox textBoxAdvanceAmount;

		private UltraGroupBox ultraGroupBox4;

		private MMLabel mmLabel17;

		private AmountTextBox textBoxSumAdvanceBalance;

		private MMLabel mmLabel19;

		private AmountTextBox textBoxSumAdvanceReceived;

		private MMLabel mmLabel22;

		private AmountTextBox textBoxSumAdvanceApplied;

		private AmountTextBox textBoxSumAdvance;

		private MMLabel mmLabel21;

		private UltraGroupBox ultraGroupBox3;

		private MMLabel mmLabel20;

		private AmountTextBox textBoxSumRetentionBalance;

		private MMLabel mmLabel23;

		private MMLabel mmLabel15;

		private MMLabel mmLabel16;

		private AmountTextBox textBoxSumRetentionAmount;

		private MMLabel mmLabel18;

		private AmountTextBox textBoxSumRetentionBilled;

		private AmountTextBox textBoxSumRetentionPercent;

		private UltraGroupBox ultraGroupBox2;

		private AmountTextBox textBoxSumProfit;

		private MMLabel mmLabel14;

		private AmountTextBox textBoxSumBilled;

		private AmountTextBox textBoxSumCost;

		private AmountTextBox textBoxSumWIP;

		private AmountTextBox textBoxSumExpense;

		private MMTextBox textBoxAdvanceName;

		private AllAccountsComboBox comboBoxAdvanceAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private XPButton buttonEquipment;

		private MMTextBox textBoxProjectCostAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel16;

		private AllAccountsComboBox comboBoxProjectCostAccount;

		private XPButton buttonEstimation;

		private LocationComboBox comboBoxLocation;

		private Label labelSiteAddress;

		private TextBox textBoxSiteAddress;

		private NumberTextBox textBoxSumRetentionDays;

		private Label label5;

		private ToolStripButton toolStripButtonInformation;

		private PercentTextBox textBoxCompletedPercentage;

		private MMLabel mmLabel25;

		private MMLabel mmLabel24;

		private XPButton buttonSchedule;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel labelSiteLocation;

		private CheckBox checkBoxIsInactive;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonPreview;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraGroupBox ultraGroupBox5;

		private MMTextBox textBoxVehicleName;

		private VehicleComboBox comboBoxVehicle;

		private MMTextBox textBoxRegistrationNo;

		private MMLabel mmLabel32;

		private MMTextBox textBoxModel;

		private MMLabel mmLabel33;

		private MMTextBox textBoxColor;

		private MMLabel mmLabel34;

		private MMLabel mmLabel35;

		private NumberTextBox textBoxOdometer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private UltraGroupBox ultraGroupBox6;

		private PercentTextBox textBoxOverHeadVariance;

		private MMLabel mmLabel31;

		private PercentTextBox textBoxLaborVariance;

		private MMLabel mmLabel30;

		private PercentTextBox textBoxMisVariance;

		private MMLabel mmLabel29;

		private AmountTextBox textBoxOverHeadAmount;

		private MMLabel mmLabel26;

		private MMLabel mmLabel27;

		private AmountTextBox textBoxLaborAmount;

		private MMLabel mmLabel28;

		private AmountTextBox textBoxMiscellaneousAmount;

		private MMLabel mmLabel37;

		private MMLabel mmLabel36;

		private XPButton buttonManHrEstimation;

		private AmountTextBox textBoxMaterial;

		private MMLabel mmLabel41;

		private AmountTextBox textBoxProfit;

		private MMLabel mmLabel40;

		private MMLabel mmLabel39;

		private AmountTextBox textBoxTotal;

		private Line line1;

		private MMSDateTimePicker dateTimePickerRetDate;

		private MMLabel mmLabel38;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

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
					XPButton xPButton = buttonDelete;
					MMTextBox mMTextBox = textBoxCode;
					XPButton xPButton2 = buttonFees;
					XPButton xPButton3 = buttonBudget;
					XPButton xPButton4 = buttonEquipment;
					XPButton xPButton5 = buttonEstimation;
					XPButton xPButton6 = buttonSchedule;
					bool flag2 = buttonManHrEstimation.Enabled = false;
					bool flag4 = xPButton6.Enabled = flag2;
					bool flag6 = xPButton5.Enabled = flag4;
					bool flag8 = xPButton4.Enabled = flag6;
					bool flag10 = xPButton3.Enabled = flag8;
					bool flag12 = xPButton2.Enabled = flag10;
					bool enabled = mMTextBox.ReadOnly = flag12;
					xPButton.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton7 = buttonDelete;
					MMTextBox mMTextBox2 = textBoxCode;
					XPButton xPButton8 = buttonFees;
					XPButton xPButton9 = buttonBudget;
					XPButton xPButton10 = buttonEquipment;
					XPButton xPButton11 = buttonEstimation;
					XPButton xPButton12 = buttonSchedule;
					bool flag2 = buttonManHrEstimation.Enabled = true;
					bool flag4 = xPButton12.Enabled = flag2;
					bool flag6 = xPButton11.Enabled = flag4;
					bool flag8 = xPButton10.Enabled = flag6;
					bool flag10 = xPButton9.Enabled = flag8;
					bool flag12 = xPButton8.Enabled = flag10;
					bool enabled = mMTextBox2.ReadOnly = flag12;
					xPButton7.Enabled = enabled;
				}
				toolStripButtonAttach.Enabled = !value;
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

		public JobDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxWIPAccount.FilterSubType = AccountSubTypes.WIP;
			if (CompanyPreferences.UseProjectPhase)
			{
				buttonFees.Text = "Phases && Fees";
			}
			if (CompanyPreferences.ActivateAutoservice)
			{
				ultraTabControl1.Tabs[1].Visible = true;
				ultraTabControl1.SelectedTab = ultraTabControl1.Tabs[0];
			}
		}

		private void AddEvents()
		{
			base.Load += JobDetailsForm_Load;
			udfEntryGrid.SetupUDF += udfEntryGrid_SetupUDF;
		}

		private void udfEntryGrid_SetupUDF(object sender, EventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new JobData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.JobTable.Rows[0] : currentData.JobTable.NewRow();
				dataRow.BeginEdit();
				dataRow["JobID"] = textBoxCode.Text.Trim();
				dataRow["JobName"] = textBoxName.Text.Trim();
				dataRow["Inactive"] = checkBoxIsInactive.Checked;
				if (comboBoxStatus.SelectedID != -1)
				{
					dataRow["Status"] = comboBoxStatus.SelectedID;
				}
				else
				{
					dataRow["Status"] = DBNull.Value;
				}
				if (comboBoxCutomers.SelectedID != "")
				{
					dataRow["CustomerID"] = comboBoxCutomers.SelectedID;
				}
				else
				{
					dataRow["CustomerID"] = DBNull.Value;
				}
				if (comboBoxSalesperson.SelectedID != "")
				{
					dataRow["SalesPersonID"] = comboBoxSalesperson.SelectedID;
				}
				else
				{
					dataRow["SalesPersonID"] = DBNull.Value;
				}
				if (comboBoxManager.SelectedID != "")
				{
					dataRow["ProjectManagerID"] = comboBoxManager.SelectedID;
				}
				else
				{
					dataRow["ProjectManagerID"] = DBNull.Value;
				}
				if (comboBoxWIPAccount.SelectedID != "")
				{
					dataRow["WIPAccountID"] = comboBoxWIPAccount.SelectedID;
				}
				else
				{
					dataRow["WIPAccountID"] = DBNull.Value;
				}
				if (comboBoxIncomeAccount.SelectedID != "")
				{
					dataRow["IncomeAccountID"] = comboBoxIncomeAccount.SelectedID;
				}
				else
				{
					dataRow["IncomeAccountID"] = DBNull.Value;
				}
				if (comboBoxTimesheetAccount.SelectedID != "")
				{
					dataRow["TimesheetContraAccountID"] = comboBoxTimesheetAccount.SelectedID;
				}
				else
				{
					dataRow["TimesheetContraAccountID"] = DBNull.Value;
				}
				if (comboBoxRetentionAccount.SelectedID != "")
				{
					dataRow["RetentionAccountID"] = comboBoxRetentionAccount.SelectedID;
				}
				else
				{
					dataRow["RetentionAccountID"] = DBNull.Value;
				}
				if (comboBoxAdvanceAccount.SelectedID != "")
				{
					dataRow["AdvanceAccountID"] = comboBoxAdvanceAccount.SelectedID;
				}
				else
				{
					dataRow["AdvanceAccountID"] = DBNull.Value;
				}
				if (comboBoxProjectCostAccount.SelectedID != "")
				{
					dataRow["CostAccountID"] = comboBoxProjectCostAccount.SelectedID;
				}
				else
				{
					dataRow["CostAccountID"] = DBNull.Value;
				}
				if (textBoxSumRetentionAmount.Text != "")
				{
					dataRow["RetentionAmount"] = textBoxSumRetentionAmount.Text;
				}
				else
				{
					dataRow["RetentionAmount"] = "0.0";
				}
				if (textBoxSumRetentionPercent.Text != "")
				{
					dataRow["RetentionPercent"] = textBoxSumRetentionPercent.Text;
				}
				else
				{
					dataRow["RetentionPercent"] = "0";
				}
				if (textBoxSumRetentionDays.Text != "")
				{
					dataRow["RetentionDays"] = textBoxSumRetentionDays.Text;
				}
				else
				{
					dataRow["RetentionDays"] = "0.0";
				}
				if (dateTimePickerRetDate.Checked)
				{
					dataRow["RetentionDate"] = dateTimePickerRetDate.Value;
				}
				else
				{
					dataRow["RetentionDays"] = DBNull.Value;
				}
				if (comboBoxCurrency.SelectedID != "")
				{
					dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
				}
				if (comboBoxJobType.SelectedID != "")
				{
					dataRow["JobTypeID"] = comboBoxJobType.SelectedID;
				}
				else
				{
					dataRow["JobTypeID"] = DBNull.Value;
				}
				if (textBoxCompletedPercentage.Text != "")
				{
					dataRow["CompletedPercent"] = textBoxCompletedPercentage.Text;
				}
				else
				{
					dataRow["CompletedPercent"] = "0";
				}
				dataRow["Reference"] = textBoxReference.Text.Trim();
				dataRow["PONumber"] = textBoxPONumber.Text.Trim();
				dataRow["ProjectAmount"] = textBoxProjectAmount.Text;
				dataRow["StartDate"] = dateTimeStartDate.Value;
				dataRow["EndDate"] = dateTimeEndDate.Value;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["SiteLocationID"] = comboBoxLocation.SelectedID;
				dataRow["SiteLocationAddress"] = textBoxSiteAddress.Text.Trim();
				if (!string.IsNullOrEmpty(textBoxMiscellaneousAmount.Text))
				{
					dataRow["MiscellaneousAmount"] = textBoxMiscellaneousAmount.Text;
				}
				else
				{
					dataRow["MiscellaneousAmount"] = "0";
				}
				if (!string.IsNullOrEmpty(textBoxMisVariance.Text))
				{
					dataRow["MiscellaneousVariance"] = textBoxMisVariance.Text;
				}
				else
				{
					dataRow["MiscellaneousVariance"] = "0";
				}
				if (!string.IsNullOrEmpty(textBoxLaborAmount.Text))
				{
					dataRow["LaborAmount"] = textBoxLaborAmount.Text;
				}
				else
				{
					dataRow["LaborAmount"] = "0";
				}
				if (!string.IsNullOrEmpty(textBoxLaborVariance.Text))
				{
					dataRow["LaborVariance"] = textBoxLaborVariance.Text;
				}
				else
				{
					dataRow["LaborVariance"] = "0";
				}
				if (!string.IsNullOrEmpty(textBoxOverHeadAmount.Text))
				{
					dataRow["OverHeadAmount"] = textBoxOverHeadAmount.Text;
				}
				else
				{
					dataRow["OverHeadAmount"] = "0";
				}
				if (!string.IsNullOrEmpty(textBoxOverHeadVariance.Text))
				{
					dataRow["OverHeadVariance"] = textBoxOverHeadVariance.Text;
				}
				else
				{
					dataRow["OverHeadVariance"] = "0";
				}
				if (!string.IsNullOrEmpty(textBoxProfit.Text))
				{
					dataRow["Profit"] = textBoxProfit.Text;
				}
				else
				{
					dataRow["Profit"] = "0";
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.JobTable.Rows.Add(dataRow);
				}
				DataRow dataRow2 = (!isNewRecord) ? ((currentData.JobVehicleDetailTable.Rows.Count > 0) ? currentData.JobVehicleDetailTable.Rows[0] : currentData.JobVehicleDetailTable.NewRow()) : currentData.JobVehicleDetailTable.NewRow();
				dataRow.BeginEdit();
				dataRow2["JobID"] = textBoxCode.Text;
				dataRow2["VehicleID"] = comboBoxVehicle.SelectedID;
				dataRow2["RegistrationNumber"] = textBoxRegistrationNo.Text;
				dataRow2["Color"] = textBoxColor.Text;
				dataRow2["Model"] = textBoxModel.Text;
				if (textBoxOdometer.Text != "")
				{
					dataRow2["Odometer"] = textBoxOdometer.Text;
				}
				else
				{
					dataRow2["Odometer"] = 0;
				}
				dataRow2.EndEdit();
				if (isNewRecord || dataRow2.RowState == DataRowState.Detached)
				{
					currentData.JobVehicleDetailTable.Rows.Add(dataRow2);
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

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.JobSystem.GetJobByID(id);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
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
				ClearForm();
			}
		}

		private void FillData()
		{
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.Tables[0].Rows[0];
			textBoxCode.Text = dataRow["JobID"].ToString();
			textBoxName.Text = dataRow["JobName"].ToString();
			if (dataRow["Inactive"] != DBNull.Value && dataRow["Inactive"].ToString() != "")
			{
				checkBoxIsInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
			}
			else
			{
				checkBoxIsInactive.Checked = false;
			}
			dateTimeStartDate.Value = DateTime.Parse(dataRow["StartDate"].ToString());
			dateTimeEndDate.Value = DateTime.Parse(dataRow["EndDate"].ToString());
			textBoxNote.Text = dataRow["Note"].ToString();
			if (dataRow["CustomerID"] != DBNull.Value)
			{
				comboBoxCutomers.SelectedID = dataRow["CustomerID"].ToString();
			}
			else
			{
				comboBoxCutomers.Clear();
			}
			if (dataRow["Status"] != DBNull.Value)
			{
				comboBoxStatus.SelectedID = int.Parse(dataRow["Status"].ToString());
			}
			else
			{
				comboBoxStatus.Clear();
			}
			if (dataRow["WIPAccountID"] != DBNull.Value)
			{
				comboBoxWIPAccount.SelectedID = dataRow["WIPAccountID"].ToString();
			}
			else
			{
				comboBoxWIPAccount.Clear();
			}
			if (dataRow["IncomeAccountID"] != DBNull.Value)
			{
				comboBoxIncomeAccount.SelectedID = dataRow["IncomeAccountID"].ToString();
			}
			else
			{
				comboBoxIncomeAccount.Clear();
			}
			if (dataRow["TimesheetContraAccountID"] != DBNull.Value)
			{
				comboBoxTimesheetAccount.SelectedID = dataRow["TimesheetContraAccountID"].ToString();
			}
			else
			{
				comboBoxTimesheetAccount.Clear();
			}
			if (dataRow["RetentionAccountID"] != DBNull.Value)
			{
				comboBoxRetentionAccount.SelectedID = dataRow["RetentionAccountID"].ToString();
			}
			else
			{
				comboBoxRetentionAccount.Clear();
			}
			if (dataRow["AdvanceAccountID"] != DBNull.Value)
			{
				comboBoxAdvanceAccount.SelectedID = dataRow["AdvanceAccountID"].ToString();
			}
			else
			{
				comboBoxAdvanceAccount.Clear();
			}
			if (dataRow["CostAccountID"] != DBNull.Value)
			{
				comboBoxProjectCostAccount.SelectedID = dataRow["CostAccountID"].ToString();
			}
			else
			{
				comboBoxProjectCostAccount.Clear();
			}
			if (dataRow["SalesPersonID"] != DBNull.Value)
			{
				comboBoxSalesperson.SelectedID = dataRow["SalesPersonID"].ToString();
			}
			else
			{
				comboBoxSalesperson.Clear();
			}
			if (dataRow["JobTypeID"] != DBNull.Value)
			{
				comboBoxJobType.SelectedID = dataRow["JobTypeID"].ToString();
			}
			else
			{
				comboBoxJobType.Clear();
				buttonSchedule.Visible = false;
			}
			if (dataRow["CurrencyID"] != DBNull.Value)
			{
				comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
			}
			else
			{
				comboBoxCurrency.Clear();
			}
			if (dataRow["ProjectManagerID"] != DBNull.Value)
			{
				comboBoxManager.SelectedID = dataRow["ProjectManagerID"].ToString();
			}
			else
			{
				comboBoxManager.Clear();
			}
			textBoxReference.Text = dataRow["Reference"].ToString();
			textBoxPONumber.Text = dataRow["PONumber"].ToString();
			if (dataRow["ProjectAmount"] != DBNull.Value)
			{
				textBoxProjectAmount.Text = dataRow["ProjectAmount"].ToString();
			}
			else
			{
				textBoxProjectAmount.Clear();
			}
			if (dataRow["AdvanceAmount"] != DBNull.Value)
			{
				textBoxAdvanceAmount.Text = dataRow["AdvanceAmount"].ToString();
			}
			else
			{
				textBoxAdvanceAmount.Clear();
			}
			if (dataRow["RetentionPercent"] != DBNull.Value)
			{
				textBoxRetention.Text = dataRow["RetentionPercent"].ToString();
			}
			else
			{
				textBoxRetention.Clear();
			}
			if (dataRow["CompletedPercent"] != DBNull.Value)
			{
				textBoxCompletedPercentage.Text = dataRow["CompletedPercent"].ToString();
			}
			else
			{
				textBoxCompletedPercentage.Clear();
			}
			if (dataRow["ProjectBudget"] != DBNull.Value)
			{
				textBoxProjectBudget.Text = dataRow["ProjectBudget"].ToString();
			}
			else
			{
				textBoxProjectBudget.Clear();
			}
			if (dataRow["SiteLocationID"] != DBNull.Value)
			{
				comboBoxLocation.SelectedID = dataRow["SiteLocationID"].ToString();
			}
			else
			{
				comboBoxLocation.Clear();
			}
			if (dataRow["SiteLocationAddress"] != DBNull.Value)
			{
				textBoxSiteAddress.Text = dataRow["SiteLocationAddress"].ToString();
			}
			else
			{
				textBoxSiteAddress.Clear();
			}
			DataRow dataRow2 = currentData.Tables["Job_Summary"].Rows[0];
			decimal d = default(decimal);
			decimal d2 = default(decimal);
			decimal num = default(decimal);
			decimal d3 = default(decimal);
			if (dataRow2["Expense"] != DBNull.Value)
			{
				d = decimal.Parse(dataRow2["Expense"].ToString());
			}
			if (dataRow2["WIP"] != DBNull.Value)
			{
				d2 = decimal.Parse(dataRow2["WIP"].ToString());
			}
			num = d2 + d;
			if (dataRow2["Billed"] != DBNull.Value)
			{
				d3 = decimal.Parse(dataRow2["Billed"].ToString());
			}
			textBoxSumExpense.Text = d.ToString(Format.TotalAmountFormat);
			textBoxSumWIP.Text = d2.ToString(Format.TotalAmountFormat);
			textBoxSumCost.Text = num.ToString(Format.TotalAmountFormat);
			textBoxSumBilled.Text = d3.ToString(Format.TotalAmountFormat);
			textBoxSumProfit.Text = (d3 - num).ToString(Format.TotalAmountFormat);
			decimal d4 = default(decimal);
			decimal d5 = default(decimal);
			if (dataRow["RetentionPercent"] != DBNull.Value)
			{
				textBoxSumRetentionPercent.Text = dataRow["RetentionPercent"].ToString();
			}
			else
			{
				textBoxSumRetentionPercent.Clear();
			}
			if (dataRow["RetentionAmount"] != DBNull.Value)
			{
				textBoxSumRetentionAmount.Text = dataRow["RetentionAmount"].ToString();
				d4 = decimal.Parse(dataRow["RetentionAmount"].ToString());
			}
			else
			{
				textBoxSumRetentionAmount.Text = "0";
			}
			if (dataRow["RetentionPaid"] != DBNull.Value)
			{
				textBoxSumRetentionBilled.Text = dataRow["RetentionPaid"].ToString();
				d5 = decimal.Parse(dataRow["RetentionPaid"].ToString());
			}
			else
			{
				textBoxSumRetentionBilled.Text = "0";
			}
			textBoxSumRetentionBalance.Text = (d4 - d5).ToString();
			if (dataRow["RetentionDays"] != DBNull.Value)
			{
				textBoxSumRetentionDays.Text = Convert.ToString(Convert.ToInt64(Convert.ToDouble(dataRow["RetentionDays"].ToString())));
			}
			else
			{
				textBoxSumRetentionDays.Text = "0";
			}
			if (dataRow["RetentionDate"] != DBNull.Value)
			{
				dateTimePickerRetDate.Value = DateTime.Parse(dataRow["RetentionDate"].ToString());
				dateTimePickerRetDate.Checked = true;
			}
			else
			{
				dateTimePickerRetDate.Clear();
			}
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal result3 = default(decimal);
			decimal result4 = default(decimal);
			decimal num2 = default(decimal);
			if (dataRow["MiscellaneousAmount"] != DBNull.Value)
			{
				textBoxMiscellaneousAmount.Text = dataRow["MiscellaneousAmount"].ToString();
				decimal.TryParse(dataRow["MiscellaneousAmount"].ToString(), out result);
			}
			else
			{
				textBoxMiscellaneousAmount.Text = "0";
			}
			if (dataRow["MiscellaneousVariance"] != DBNull.Value)
			{
				decimal result5 = default(decimal);
				decimal.TryParse(dataRow["MiscellaneousVariance"].ToString(), out result5);
				textBoxMisVariance.Text = result5.ToString(Format.PercentageFormat);
			}
			else
			{
				textBoxMisVariance.Text = 0.ToString(Format.PercentageFormat);
			}
			if (dataRow["LaborAmount"] != DBNull.Value)
			{
				textBoxLaborAmount.Text = dataRow["LaborAmount"].ToString();
				decimal.TryParse(dataRow["LaborAmount"].ToString(), out result2);
			}
			else
			{
				textBoxLaborAmount.Text = "0";
			}
			if (dataRow["LaborVariance"] != DBNull.Value)
			{
				decimal result6 = default(decimal);
				decimal.TryParse(dataRow["LaborVariance"].ToString(), out result6);
				textBoxLaborVariance.Text = result6.ToString(Format.PercentageFormat);
			}
			else
			{
				textBoxLaborVariance.Text = 0.ToString(Format.PercentageFormat);
			}
			if (dataRow["OverHeadAmount"] != DBNull.Value)
			{
				textBoxOverHeadAmount.Text = dataRow["OverHeadAmount"].ToString();
				decimal.TryParse(dataRow["OverHeadAmount"].ToString(), out result3);
			}
			else
			{
				textBoxOverHeadAmount.Text = "0";
			}
			if (dataRow["OverHeadVariance"] != DBNull.Value)
			{
				decimal result7 = default(decimal);
				decimal.TryParse(dataRow["OverHeadVariance"].ToString(), out result7);
				textBoxOverHeadVariance.Text = result7.ToString(Format.PercentageFormat);
			}
			else
			{
				textBoxOverHeadVariance.Text = 0.ToString(Format.PercentageFormat);
			}
			decimal num3 = default(decimal);
			num3 = Factory.JobMaterialEstimateSystem.GetJobMaterialEstimationQty(textBoxCode.Text);
			textBoxMaterial.Text = num3.ToString();
			if (dataRow["Profit"] != DBNull.Value)
			{
				textBoxProfit.Text = dataRow["Profit"].ToString();
				decimal.TryParse(dataRow["Profit"].ToString(), out result4);
			}
			else
			{
				textBoxProfit.Text = "0";
			}
			num2 = result + result2 + result3 + num3 + result4;
			textBoxTotal.Text = num2.ToString(Format.GridAmountFormat);
			decimal d6 = default(decimal);
			decimal d7 = default(decimal);
			if (dataRow["AdvanceAmount"] != DBNull.Value)
			{
				textBoxSumAdvance.Text = dataRow["AdvanceAmount"].ToString();
			}
			else
			{
				textBoxSumAdvance.Text = "0";
			}
			if (dataRow["AdvanceBilled"] != DBNull.Value)
			{
				textBoxSumAdvanceReceived.Text = dataRow["AdvanceBilled"].ToString();
				d6 = decimal.Parse(dataRow["AdvanceBilled"].ToString());
			}
			else
			{
				textBoxSumAdvanceReceived.Text = "0";
			}
			if (dataRow["AdvanceApplied"] != DBNull.Value)
			{
				textBoxSumAdvanceApplied.Text = dataRow["AdvanceApplied"].ToString();
				d7 = decimal.Parse(dataRow["AdvanceApplied"].ToString());
			}
			else
			{
				textBoxSumAdvanceApplied.Text = "0";
			}
			textBoxSumAdvanceBalance.Text = (d6 - d7).ToString();
			if (currentData.Tables.Contains("Job_Vehicle_Detail") && currentData.JobVehicleDetailTable.Rows.Count > 0)
			{
				DataRow dataRow3 = currentData.Tables["Job_Vehicle_Detail"].Rows[0];
				if (dataRow3["VehicleID"] != DBNull.Value && dataRow3["VehicleID"].ToString() != "")
				{
					comboBoxVehicle.SelectedID = dataRow3["VehicleID"].ToString();
				}
				else
				{
					comboBoxVehicle.Clear();
				}
				textBoxColor.Text = dataRow3["Color"].ToString();
				textBoxModel.Text = dataRow3["Model"].ToString();
				textBoxRegistrationNo.Text = dataRow3["RegistrationNumber"].ToString();
				if (dataRow3["Odometer"] != DBNull.Value)
				{
					textBoxOdometer.Text = dataRow3["Odometer"].ToString();
				}
				else
				{
					textBoxOdometer.Text = 0.ToString();
				}
			}
			if (currentData.Tables["UDF"].Rows.Count > 0)
			{
				_ = currentData.Tables["UDF"].Rows[0];
				foreach (DataColumn column in currentData.Tables["UDF"].Columns)
				{
					_ = (column.ColumnName == "EntityID");
				}
			}
			else
			{
				udfEntryGrid.ClearData();
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.JobSystem.CreateJob(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Job, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.JobSystem.UpdateJob(currentData);
				}
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Job", "JobID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0 || comboBoxCutomers.SelectedID == "" || comboBoxStatus.SelectedID == -1)
			{
				ErrorHelper.InformationMessage("Please enter required fields in bold.");
				return false;
			}
			if (dateTimeEndDate.Value < dateTimeStartDate.Value)
			{
				ErrorHelper.InformationMessage("End Date cannot be less than Start Date");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Job", "JobID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxCode.Focus();
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

		private void ClearForm()
		{
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Job", "JobID");
			textBoxName.Clear();
			DateTime dateTime2 = dateTimeStartDate.Value = (dateTimeEndDate.Value = DateTime.Now);
			comboBoxStatus.LoadData();
			comboBoxStatus.SelectedIndex = 0;
			comboBoxCutomers.Clear();
			textBoxRetention.Text = 0.ToString(Format.TotalAmountFormat);
			comboBoxSalesperson.Clear();
			comboBoxManager.Clear();
			comboBoxJobType.Clear();
			comboBoxCurrency.Clear();
			comboBoxIncomeAccount.Clear();
			comboBoxWIPAccount.Clear();
			comboBoxRetentionAccount.Clear();
			comboBoxProjectCostAccount.Clear();
			checkBoxIsInactive.Checked = false;
			textBoxReference.Clear();
			textBoxPONumber.Clear();
			textBoxProjectAmount.Clear();
			textBoxProjectBudget.Clear();
			textBoxSumAdvance.Clear();
			textBoxSumAdvanceApplied.Clear();
			textBoxSumAdvanceBalance.Clear();
			textBoxSumAdvanceReceived.Clear();
			textBoxSumBilled.Clear();
			textBoxSumCost.Clear();
			textBoxSumExpense.Clear();
			textBoxSumProfit.Clear();
			textBoxSumRetentionAmount.Clear();
			textBoxSumRetentionBalance.Clear();
			textBoxSumRetentionBilled.Clear();
			textBoxSumRetentionPercent.Clear();
			textBoxSumRetentionDays.Clear();
			textBoxSumWIP.Clear();
			comboBoxAdvanceAccount.Clear();
			comboBoxTimesheetAccount.Clear();
			textBoxAdvanceAmount.Clear();
			textBoxNote.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
			comboBoxLocation.Clear();
			textBoxSiteAddress.Clear();
			textBoxCompletedPercentage.Clear();
			buttonSchedule.Visible = false;
			textBoxColor.Clear();
			textBoxModel.Clear();
			textBoxOdometer.Clear();
			comboBoxVehicle.Clear();
			textBoxRegistrationNo.Clear();
			textBoxMiscellaneousAmount.Clear();
			textBoxMisVariance.Clear();
			textBoxLaborAmount.Clear();
			textBoxLaborVariance.Clear();
			textBoxOverHeadAmount.Clear();
			textBoxOverHeadVariance.Clear();
			dateTimePickerRetDate.Clear();
			textBoxProfit.Clear();
			textBoxMaterial.Clear();
			textBoxTotal.Clear();
			udfEntryGrid.ClearData();
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
				if (ErrorHelper.QuestionMessageYesNo("Are you sure! you want to delete the record?") == DialogResult.No)
				{
					return false;
				}
				bool num = Factory.JobSystem.DeleteJob(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Job, needRefresh: true);
				}
				return num;
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
			LoadData(DatabaseHelper.GetNextID("Job", "JobID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Job", "JobID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Job", "JobID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Job", "JobID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Job", "JobID", toolStripTextBoxFind.Text.Trim()))
				{
					LoadData(toolStripTextBoxFind.Text.Trim());
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

		private void JobDetailsForm_Load(object sender, EventArgs e)
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
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Job);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = textBoxName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Jobs;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void labelStartDate_Click(object sender, EventArgs e)
		{
		}

		private void buttonFees_Click(object sender, EventArgs e)
		{
			JobFeeDetailForm jobFeeDetailForm = new JobFeeDetailForm();
			jobFeeDetailForm.EntityID = comboBoxCutomers.SelectedID;
			jobFeeDetailForm.EntityName = textBoxCustomerName.Text;
			jobFeeDetailForm.ProjectID = textBoxCode.Text;
			jobFeeDetailForm.ProjectName = textBoxName.Text;
			if (jobFeeDetailForm.ShowDialog(this) == DialogResult.Yes)
			{
				textBoxProjectAmount.Text = jobFeeDetailForm.TotalAmount.ToString();
				string text3 = textBoxRetention.Text = (textBoxSumRetentionPercent.Text = jobFeeDetailForm.RetentionPercent.ToString(Format.TotalAmountFormat));
				text3 = (textBoxSumAdvance.Text = (textBoxAdvanceAmount.Text = jobFeeDetailForm.AdvanceAmount.ToString(Format.TotalAmountFormat)));
				textBoxSumRetentionDays.Text = jobFeeDetailForm.RetentionDays.ToString();
				dateTimePickerRetDate.Value = jobFeeDetailForm.RetentionDate;
				dateTimePickerRetDate.Checked = jobFeeDetailForm.isRetDateChecked;
				decimal d = decimal.Parse(textBoxProjectAmount.Text);
				decimal d2 = decimal.Parse(textBoxSumRetentionPercent.Text);
				decimal num = d * d2 / 100m;
				textBoxSumRetentionAmount.Text = num.ToString();
			}
		}

		private void ultraLinkSalesPerson_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxSalesperson.SelectedID);
		}

		private void ultraLinkProjectMgr_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxManager.SelectedID);
		}

		private void ultraLinkCurrency_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(comboBoxCurrency.SelectedID);
		}

		private void ultraLinkJobType_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJobType(comboBoxJobType.SelectedID);
		}

		private void buttonBudget_Click(object sender, EventArgs e)
		{
			JobBudgetDetailForm jobBudgetDetailForm = new JobBudgetDetailForm();
			jobBudgetDetailForm.EntityID = comboBoxCutomers.SelectedID;
			jobBudgetDetailForm.EntityName = textBoxCustomerName.Text;
			jobBudgetDetailForm.ProjectID = textBoxCode.Text;
			jobBudgetDetailForm.ProjectName = textBoxName.Text;
			if (jobBudgetDetailForm.ShowDialog(this) == DialogResult.Yes)
			{
				textBoxProjectBudget.Text = jobBudgetDetailForm.TotalBudget.ToString();
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxTimesheetAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxRetentionAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxIncomeAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxWIPAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAdvanceAccount.SelectedID);
		}

		private void buttonEquipment_Click(object sender, EventArgs e)
		{
			JobEquipmentDetailForm jobEquipmentDetailForm = new JobEquipmentDetailForm();
			jobEquipmentDetailForm.EntityID = comboBoxCutomers.SelectedID;
			jobEquipmentDetailForm.EntityName = textBoxCustomerName.Text;
			jobEquipmentDetailForm.ProjectID = textBoxCode.Text;
			jobEquipmentDetailForm.ProjectName = textBoxName.Text;
			jobEquipmentDetailForm.ShowDialog(this);
			_ = 6;
		}

		private void buttonEstimation_Click(object sender, EventArgs e)
		{
			JobMaterialEstimateForm jobMaterialEstimateForm = new JobMaterialEstimateForm();
			jobMaterialEstimateForm.ProjectID = textBoxCode.Text;
			jobMaterialEstimateForm.ProjectName = textBoxName.Text;
			jobMaterialEstimateForm.ShowDialog();
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				DataSet jobReport = Factory.JobSystem.GetJobReport(textBoxCode.Text, textBoxCode.Text, "", "", "", "", "", "", showInactive: true);
				if (jobReport == null || jobReport.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(jobReport, "", "Job Details", SysDocTypes.None, isPrint, showPrintDialog);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void comboBoxJobType_SelectedIndexChanged(object sender, EventArgs e)
		{
			buttonSchedule.Visible = false;
			bool result = false;
			if (comboBoxJobType.SelectedID != "")
			{
				bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Job_Type", "AMCEnabled", "JobTypeID", comboBoxJobType.SelectedID).ToString(), out result);
				buttonSchedule.Visible = result;
			}
		}

		private void buttonSchedule_Click(object sender, EventArgs e)
		{
			JobMaintenanceScheduleForm jobMaintenanceScheduleForm = new JobMaintenanceScheduleForm();
			jobMaintenanceScheduleForm.ProjectID = textBoxCode.Text;
			jobMaintenanceScheduleForm.ProjectName = textBoxName.Text;
			jobMaintenanceScheduleForm.StartDate = dateTimeStartDate.Value;
			jobMaintenanceScheduleForm.EndDate = dateTimeEndDate.Value;
			jobMaintenanceScheduleForm.ShowDialog();
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomer(comboBoxCutomers.SelectedID);
		}

		private void labelSiteLocation_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxLocation.SelectedID);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void comboBoxVehicle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxVehicle.SelectedID != "")
			{
				DataRow dataRow = Factory.VehicleSystem.GetVehicleByID(comboBoxVehicle.SelectedID).Tables[0].Rows[0];
				textBoxRegistrationNo.Text = dataRow["RegistrationNumber"].ToString();
				textBoxModel.Text = dataRow["Model"].ToString();
				textBoxColor.Text = dataRow["Color"].ToString();
			}
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVehicle(comboBoxVehicle.SelectedID);
		}

		private void buttonManHrEstimation_Click(object sender, EventArgs e)
		{
			JobManHrsBudgetingForm jobManHrsBudgetingForm = new JobManHrsBudgetingForm();
			jobManHrsBudgetingForm.ProjectID = textBoxCode.Text;
			jobManHrsBudgetingForm.ProjectName = textBoxName.Text;
			jobManHrsBudgetingForm.ShowDialog();
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Projects.JobDetailsForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			labelSiteLocation = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxCompletedPercentage = new Micromind.UISupport.PercentTextBox();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			mmLabel24 = new Micromind.UISupport.MMLabel();
			textBoxSiteAddress = new System.Windows.Forms.TextBox();
			labelSiteAddress = new System.Windows.Forms.Label();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxAdvanceAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textBoxRetention = new Micromind.UISupport.AmountTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxProjectBudget = new Micromind.UISupport.AmountTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxProjectAmount = new Micromind.UISupport.AmountTextBox();
			ultraLinkJobType = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxJobType = new Micromind.DataControls.JobTypeComboBox();
			comboBoxManager = new Micromind.DataControls.EmployeeComboBox();
			panel2 = new System.Windows.Forms.Panel();
			buttonManHrEstimation = new Micromind.UISupport.XPButton();
			buttonSchedule = new Micromind.UISupport.XPButton();
			buttonEstimation = new Micromind.UISupport.XPButton();
			buttonEquipment = new Micromind.UISupport.XPButton();
			buttonBudget = new Micromind.UISupport.XPButton();
			buttonFees = new Micromind.UISupport.XPButton();
			ultraLinkCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCurrency = new Micromind.DataControls.CurrencyComboBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxPONumber = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxReference = new Micromind.UISupport.MMTextBox();
			ultraLinkProjectMgr = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraLinkSalesPerson = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
			labelCode = new Micromind.UISupport.MMLabel();
			textBoxCustomerName = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			comboBoxStatus = new Micromind.DataControls.JobStatusComboBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			labelJobStatus = new Micromind.UISupport.MMLabel();
			textBoxName = new Micromind.UISupport.MMTextBox();
			comboBoxCutomers = new Micromind.DataControls.customersFlatComboBox();
			dateTimeStartDate = new System.Windows.Forms.DateTimePicker();
			labelStartDate = new Micromind.UISupport.MMLabel();
			dateTimeEndDate = new System.Windows.Forms.DateTimePicker();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox5 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxOdometer = new Micromind.UISupport.NumberTextBox();
			mmLabel35 = new Micromind.UISupport.MMLabel();
			textBoxColor = new Micromind.UISupport.MMTextBox();
			mmLabel34 = new Micromind.UISupport.MMLabel();
			textBoxModel = new Micromind.UISupport.MMTextBox();
			mmLabel33 = new Micromind.UISupport.MMLabel();
			textBoxRegistrationNo = new Micromind.UISupport.MMTextBox();
			mmLabel32 = new Micromind.UISupport.MMLabel();
			textBoxVehicleName = new Micromind.UISupport.MMTextBox();
			comboBoxVehicle = new Micromind.DataControls.VehicleComboBox();
			tabPageAccounts = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxProjectCostAccount = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel16 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxProjectCostAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxAdvanceName = new Micromind.UISupport.MMTextBox();
			comboBoxAdvanceAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxRetentionAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxRetentionAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxTimesheetAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxTimesheetAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxIncomeAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxIncomeAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxWIPAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxWIPAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			tabPageSummary = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox6 = new Infragistics.Win.Misc.UltraGroupBox();
			line1 = new Micromind.UISupport.Line();
			mmLabel39 = new Micromind.UISupport.MMLabel();
			textBoxTotal = new Micromind.UISupport.AmountTextBox();
			textBoxMaterial = new Micromind.UISupport.AmountTextBox();
			mmLabel41 = new Micromind.UISupport.MMLabel();
			textBoxProfit = new Micromind.UISupport.AmountTextBox();
			mmLabel40 = new Micromind.UISupport.MMLabel();
			mmLabel37 = new Micromind.UISupport.MMLabel();
			mmLabel36 = new Micromind.UISupport.MMLabel();
			textBoxOverHeadVariance = new Micromind.UISupport.PercentTextBox();
			mmLabel31 = new Micromind.UISupport.MMLabel();
			textBoxLaborVariance = new Micromind.UISupport.PercentTextBox();
			mmLabel30 = new Micromind.UISupport.MMLabel();
			textBoxMisVariance = new Micromind.UISupport.PercentTextBox();
			mmLabel29 = new Micromind.UISupport.MMLabel();
			textBoxOverHeadAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel26 = new Micromind.UISupport.MMLabel();
			mmLabel27 = new Micromind.UISupport.MMLabel();
			textBoxLaborAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel28 = new Micromind.UISupport.MMLabel();
			textBoxMiscellaneousAmount = new Micromind.UISupport.AmountTextBox();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel17 = new Micromind.UISupport.MMLabel();
			textBoxSumAdvanceBalance = new Micromind.UISupport.AmountTextBox();
			mmLabel19 = new Micromind.UISupport.MMLabel();
			textBoxSumAdvanceReceived = new Micromind.UISupport.AmountTextBox();
			mmLabel22 = new Micromind.UISupport.MMLabel();
			textBoxSumAdvanceApplied = new Micromind.UISupport.AmountTextBox();
			textBoxSumAdvance = new Micromind.UISupport.AmountTextBox();
			mmLabel21 = new Micromind.UISupport.MMLabel();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			dateTimePickerRetDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel38 = new Micromind.UISupport.MMLabel();
			label5 = new System.Windows.Forms.Label();
			textBoxSumRetentionDays = new Micromind.UISupport.NumberTextBox();
			mmLabel20 = new Micromind.UISupport.MMLabel();
			textBoxSumRetentionBalance = new Micromind.UISupport.AmountTextBox();
			mmLabel23 = new Micromind.UISupport.MMLabel();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			textBoxSumRetentionAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel18 = new Micromind.UISupport.MMLabel();
			textBoxSumRetentionBilled = new Micromind.UISupport.AmountTextBox();
			textBoxSumRetentionPercent = new Micromind.UISupport.AmountTextBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			textBoxSumProfit = new Micromind.UISupport.AmountTextBox();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			textBoxSumBilled = new Micromind.UISupport.AmountTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			textBoxSumCost = new Micromind.UISupport.AmountTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxSumWIP = new Micromind.UISupport.AmountTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxSumExpense = new Micromind.UISupport.AmountTextBox();
			tabPageNote = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			tabPageUserDefined = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			udfEntryGrid = new Micromind.DataControls.UDFEntryControl();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			panel1 = new System.Windows.Forms.Panel();
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxJobType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManager).BeginInit();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCutomers).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).BeginInit();
			ultraGroupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicle).BeginInit();
			tabPageAccounts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectCostAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAdvanceAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRetentionAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTimesheetAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxIncomeAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxWIPAccount).BeginInit();
			tabPageSummary.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox6).BeginInit();
			ultraGroupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			tabPageNote.SuspendLayout();
			tabPageUserDefined.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(checkBoxIsInactive);
			tabPageGeneral.Controls.Add(labelSiteLocation);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel5);
			tabPageGeneral.Controls.Add(textBoxCompletedPercentage);
			tabPageGeneral.Controls.Add(mmLabel25);
			tabPageGeneral.Controls.Add(mmLabel24);
			tabPageGeneral.Controls.Add(textBoxSiteAddress);
			tabPageGeneral.Controls.Add(labelSiteAddress);
			tabPageGeneral.Controls.Add(comboBoxLocation);
			tabPageGeneral.Controls.Add(ultraGroupBox1);
			tabPageGeneral.Controls.Add(ultraLinkJobType);
			tabPageGeneral.Controls.Add(comboBoxJobType);
			tabPageGeneral.Controls.Add(comboBoxManager);
			tabPageGeneral.Controls.Add(panel2);
			tabPageGeneral.Controls.Add(ultraLinkCurrency);
			tabPageGeneral.Controls.Add(comboBoxCurrency);
			tabPageGeneral.Controls.Add(mmLabel5);
			tabPageGeneral.Controls.Add(textBoxPONumber);
			tabPageGeneral.Controls.Add(mmLabel3);
			tabPageGeneral.Controls.Add(textBoxReference);
			tabPageGeneral.Controls.Add(ultraLinkProjectMgr);
			tabPageGeneral.Controls.Add(ultraLinkSalesPerson);
			tabPageGeneral.Controls.Add(comboBoxSalesperson);
			tabPageGeneral.Controls.Add(labelCode);
			tabPageGeneral.Controls.Add(textBoxCustomerName);
			tabPageGeneral.Controls.Add(mmLabel1);
			tabPageGeneral.Controls.Add(comboBoxStatus);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(labelJobStatus);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(comboBoxCutomers);
			tabPageGeneral.Controls.Add(dateTimeStartDate);
			tabPageGeneral.Controls.Add(labelStartDate);
			tabPageGeneral.Controls.Add(dateTimeEndDate);
			tabPageGeneral.Controls.Add(mmLabel2);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(711, 559);
			checkBoxIsInactive.BackColor = System.Drawing.Color.Transparent;
			checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxIsInactive.Location = new System.Drawing.Point(334, 15);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(87, 16);
			checkBoxIsInactive.TabIndex = 176;
			checkBoxIsInactive.Text = "Inactive";
			checkBoxIsInactive.UseVisualStyleBackColor = false;
			labelSiteLocation.AutoSize = true;
			labelSiteLocation.Location = new System.Drawing.Point(15, 176);
			labelSiteLocation.Name = "labelSiteLocation";
			labelSiteLocation.Size = new System.Drawing.Size(71, 14);
			labelSiteLocation.TabIndex = 175;
			labelSiteLocation.TabStop = true;
			labelSiteLocation.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelSiteLocation.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelSiteLocation.Value = "Site Location:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			labelSiteLocation.VisitedLinkAppearance = appearance;
			labelSiteLocation.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelSiteLocation_LinkClicked);
			appearance2.FontData.BoldAsString = "True";
			appearance2.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance2;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(14, 64);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(79, 15);
			ultraFormattedLinkLabel5.TabIndex = 174;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Customer ID:";
			appearance3.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance3;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			textBoxCompletedPercentage.CustomReportFieldName = "";
			textBoxCompletedPercentage.CustomReportKey = "";
			textBoxCompletedPercentage.CustomReportValueType = 1;
			textBoxCompletedPercentage.IsComboTextBox = false;
			textBoxCompletedPercentage.IsModified = false;
			textBoxCompletedPercentage.Location = new System.Drawing.Point(593, 37);
			textBoxCompletedPercentage.Name = "textBoxCompletedPercentage";
			textBoxCompletedPercentage.Size = new System.Drawing.Size(59, 20);
			textBoxCompletedPercentage.TabIndex = 173;
			textBoxCompletedPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel25.AutoSize = true;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(657, 41);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(15, 13);
			mmLabel25.TabIndex = 37;
			mmLabel25.Text = "%";
			mmLabel24.AutoSize = true;
			mmLabel24.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel24.IsFieldHeader = false;
			mmLabel24.IsRequired = false;
			mmLabel24.Location = new System.Drawing.Point(529, 41);
			mmLabel24.Name = "mmLabel24";
			mmLabel24.PenWidth = 1f;
			mmLabel24.ShowBorder = false;
			mmLabel24.Size = new System.Drawing.Size(63, 13);
			mmLabel24.TabIndex = 37;
			mmLabel24.Text = "Completed :";
			textBoxSiteAddress.Location = new System.Drawing.Point(346, 171);
			textBoxSiteAddress.MaxLength = 255;
			textBoxSiteAddress.Multiline = true;
			textBoxSiteAddress.Name = "textBoxSiteAddress";
			textBoxSiteAddress.Size = new System.Drawing.Size(351, 20);
			textBoxSiteAddress.TabIndex = 13;
			labelSiteAddress.AutoSize = true;
			labelSiteAddress.Location = new System.Drawing.Point(271, 174);
			labelSiteAddress.Name = "labelSiteAddress";
			labelSiteAddress.Size = new System.Drawing.Size(69, 13);
			labelSiteAddress.TabIndex = 172;
			labelSiteAddress.Text = "Site Address:";
			comboBoxLocation.AlwaysInEditMode = true;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance4.BackColor = System.Drawing.SystemColors.Window;
			appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance4;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance5.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance5.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance5.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance5;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance6;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance7.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance7.BackColor2 = System.Drawing.SystemColors.Control;
			appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance7;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			appearance8.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance8;
			appearance9.BackColor = System.Drawing.SystemColors.Highlight;
			appearance9.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance9;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance10.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance10;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			appearance11.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance11;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance12.BackColor = System.Drawing.SystemColors.Control;
			appearance12.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance12.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance12.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance12;
			appearance13.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance13;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance14;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance15.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance15;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(108, 170);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowAll = false;
			comboBoxLocation.ShowConsignIn = false;
			comboBoxLocation.ShowConsignOut = false;
			comboBoxLocation.ShowDefaultLocationOnly = false;
			comboBoxLocation.ShowInactiveItems = false;
			comboBoxLocation.ShowNormalLocations = true;
			comboBoxLocation.ShowPOSOnly = false;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.ShowWarehouseOnly = false;
			comboBoxLocation.Size = new System.Drawing.Size(138, 20);
			comboBoxLocation.TabIndex = 12;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox1.Controls.Add(mmLabel12);
			ultraGroupBox1.Controls.Add(mmLabel13);
			ultraGroupBox1.Controls.Add(textBoxAdvanceAmount);
			ultraGroupBox1.Controls.Add(mmLabel11);
			ultraGroupBox1.Controls.Add(textBoxRetention);
			ultraGroupBox1.Controls.Add(mmLabel10);
			ultraGroupBox1.Controls.Add(textBoxProjectBudget);
			ultraGroupBox1.Controls.Add(mmLabel4);
			ultraGroupBox1.Controls.Add(textBoxProjectAmount);
			ultraGroupBox1.Location = new System.Drawing.Point(11, 210);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(686, 64);
			ultraGroupBox1.TabIndex = 39;
			ultraGroupBox1.Text = "Summary";
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(428, 30);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(15, 13);
			mmLabel12.TabIndex = 36;
			mmLabel12.Text = "%";
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(453, 30);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(53, 13);
			mmLabel13.TabIndex = 36;
			mmLabel13.Text = "Advance:";
			textBoxAdvanceAmount.AllowDecimal = true;
			textBoxAdvanceAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAdvanceAmount.CustomReportFieldName = "";
			textBoxAdvanceAmount.CustomReportKey = "";
			textBoxAdvanceAmount.CustomReportValueType = 1;
			textBoxAdvanceAmount.IsComboTextBox = false;
			textBoxAdvanceAmount.IsModified = false;
			textBoxAdvanceAmount.Location = new System.Drawing.Point(512, 27);
			textBoxAdvanceAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAdvanceAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAdvanceAmount.Name = "textBoxAdvanceAmount";
			textBoxAdvanceAmount.NullText = "0";
			textBoxAdvanceAmount.ReadOnly = true;
			textBoxAdvanceAmount.Size = new System.Drawing.Size(86, 20);
			textBoxAdvanceAmount.TabIndex = 2;
			textBoxAdvanceAmount.Text = "0.00";
			textBoxAdvanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAdvanceAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(280, 30);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(56, 13);
			mmLabel11.TabIndex = 36;
			mmLabel11.Text = "Retention:";
			textBoxRetention.AllowDecimal = true;
			textBoxRetention.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRetention.CustomReportFieldName = "";
			textBoxRetention.CustomReportKey = "";
			textBoxRetention.CustomReportValueType = 1;
			textBoxRetention.IsComboTextBox = false;
			textBoxRetention.IsModified = false;
			textBoxRetention.Location = new System.Drawing.Point(339, 27);
			textBoxRetention.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxRetention.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxRetention.Name = "textBoxRetention";
			textBoxRetention.NullText = "0";
			textBoxRetention.ReadOnly = true;
			textBoxRetention.Size = new System.Drawing.Size(86, 20);
			textBoxRetention.TabIndex = 2;
			textBoxRetention.Text = "0.00";
			textBoxRetention.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxRetention.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(138, 30);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(44, 13);
			mmLabel10.TabIndex = 34;
			mmLabel10.Text = "Budget:";
			textBoxProjectBudget.AllowDecimal = true;
			textBoxProjectBudget.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectBudget.CustomReportFieldName = "";
			textBoxProjectBudget.CustomReportKey = "";
			textBoxProjectBudget.CustomReportValueType = 1;
			textBoxProjectBudget.IsComboTextBox = false;
			textBoxProjectBudget.IsModified = false;
			textBoxProjectBudget.Location = new System.Drawing.Point(188, 27);
			textBoxProjectBudget.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxProjectBudget.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxProjectBudget.Name = "textBoxProjectBudget";
			textBoxProjectBudget.NullText = "0";
			textBoxProjectBudget.ReadOnly = true;
			textBoxProjectBudget.Size = new System.Drawing.Size(86, 20);
			textBoxProjectBudget.TabIndex = 1;
			textBoxProjectBudget.Text = "0.00";
			textBoxProjectBudget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxProjectBudget.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(8, 29);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 19;
			mmLabel4.Text = "Fees:";
			mmLabel4.Click += new System.EventHandler(labelStartDate_Click);
			textBoxProjectAmount.AllowDecimal = true;
			textBoxProjectAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectAmount.CustomReportFieldName = "";
			textBoxProjectAmount.CustomReportKey = "";
			textBoxProjectAmount.CustomReportValueType = 1;
			textBoxProjectAmount.IsComboTextBox = false;
			textBoxProjectAmount.IsModified = false;
			textBoxProjectAmount.Location = new System.Drawing.Point(47, 27);
			textBoxProjectAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxProjectAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxProjectAmount.Name = "textBoxProjectAmount";
			textBoxProjectAmount.NullText = "0";
			textBoxProjectAmount.ReadOnly = true;
			textBoxProjectAmount.Size = new System.Drawing.Size(86, 20);
			textBoxProjectAmount.TabIndex = 0;
			textBoxProjectAmount.Text = "0.00";
			textBoxProjectAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxProjectAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			ultraLinkJobType.AutoSize = true;
			ultraLinkJobType.Location = new System.Drawing.Point(274, 121);
			ultraLinkJobType.Name = "ultraLinkJobType";
			ultraLinkJobType.Size = new System.Drawing.Size(32, 14);
			ultraLinkJobType.TabIndex = 38;
			ultraLinkJobType.TabStop = true;
			ultraLinkJobType.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraLinkJobType.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraLinkJobType.Value = "Type:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			ultraLinkJobType.VisitedLinkAppearance = appearance16;
			ultraLinkJobType.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraLinkJobType_LinkClicked);
			comboBoxJobType.Assigned = false;
			comboBoxJobType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJobType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJobType.CustomReportFieldName = "";
			comboBoxJobType.CustomReportKey = "";
			comboBoxJobType.CustomReportValueType = 1;
			comboBoxJobType.DescriptionTextBox = null;
			comboBoxJobType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJobType.Editable = true;
			comboBoxJobType.FilterString = "";
			comboBoxJobType.HasAllAccount = false;
			comboBoxJobType.HasCustom = false;
			comboBoxJobType.IsDataLoaded = false;
			comboBoxJobType.Location = new System.Drawing.Point(345, 118);
			comboBoxJobType.MaxDropDownItems = 12;
			comboBoxJobType.Name = "comboBoxJobType";
			comboBoxJobType.ShowInactiveItems = false;
			comboBoxJobType.ShowQuickAdd = true;
			comboBoxJobType.Size = new System.Drawing.Size(137, 20);
			comboBoxJobType.TabIndex = 8;
			comboBoxJobType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJobType.SelectedIndexChanged += new System.EventHandler(comboBoxJobType_SelectedIndexChanged);
			comboBoxManager.Assigned = false;
			comboBoxManager.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxManager.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxManager.CustomReportFieldName = "";
			comboBoxManager.CustomReportKey = "";
			comboBoxManager.CustomReportValueType = 1;
			comboBoxManager.DescriptionTextBox = null;
			comboBoxManager.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxManager.Editable = true;
			comboBoxManager.FilterString = "";
			comboBoxManager.HasAllAccount = false;
			comboBoxManager.HasCustom = false;
			comboBoxManager.IsDataLoaded = false;
			comboBoxManager.Location = new System.Drawing.Point(108, 118);
			comboBoxManager.MaxDropDownItems = 12;
			comboBoxManager.Name = "comboBoxManager";
			comboBoxManager.ShowInactiveItems = false;
			comboBoxManager.ShowQuickAdd = true;
			comboBoxManager.ShowTerminatedEmployees = true;
			comboBoxManager.Size = new System.Drawing.Size(156, 20);
			comboBoxManager.TabIndex = 7;
			comboBoxManager.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			panel2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			panel2.Controls.Add(buttonManHrEstimation);
			panel2.Controls.Add(buttonSchedule);
			panel2.Controls.Add(buttonEstimation);
			panel2.Controls.Add(buttonEquipment);
			panel2.Controls.Add(buttonBudget);
			panel2.Controls.Add(buttonFees);
			panel2.Location = new System.Drawing.Point(0, 423);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(699, 59);
			panel2.TabIndex = 34;
			buttonManHrEstimation.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonManHrEstimation.BackColor = System.Drawing.Color.Silver;
			buttonManHrEstimation.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonManHrEstimation.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonManHrEstimation.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonManHrEstimation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonManHrEstimation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonManHrEstimation.Location = new System.Drawing.Point(442, 28);
			buttonManHrEstimation.Name = "buttonManHrEstimation";
			buttonManHrEstimation.Size = new System.Drawing.Size(121, 24);
			buttonManHrEstimation.TabIndex = 17;
			buttonManHrEstimation.Text = "Man Hr Estimation";
			buttonManHrEstimation.UseVisualStyleBackColor = false;
			buttonManHrEstimation.Click += new System.EventHandler(buttonManHrEstimation_Click);
			buttonSchedule.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSchedule.BackColor = System.Drawing.Color.Silver;
			buttonSchedule.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSchedule.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSchedule.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSchedule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSchedule.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSchedule.Location = new System.Drawing.Point(569, 28);
			buttonSchedule.Name = "buttonSchedule";
			buttonSchedule.Size = new System.Drawing.Size(127, 24);
			buttonSchedule.TabIndex = 16;
			buttonSchedule.Text = "Maintenance Schedule";
			buttonSchedule.UseVisualStyleBackColor = false;
			buttonSchedule.Visible = false;
			buttonSchedule.Click += new System.EventHandler(buttonSchedule_Click);
			buttonEstimation.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonEstimation.BackColor = System.Drawing.Color.Silver;
			buttonEstimation.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonEstimation.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonEstimation.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonEstimation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonEstimation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonEstimation.Location = new System.Drawing.Point(317, 28);
			buttonEstimation.Name = "buttonEstimation";
			buttonEstimation.Size = new System.Drawing.Size(119, 24);
			buttonEstimation.TabIndex = 15;
			buttonEstimation.Text = "Material Estimation";
			buttonEstimation.UseVisualStyleBackColor = false;
			buttonEstimation.Click += new System.EventHandler(buttonEstimation_Click);
			buttonEquipment.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonEquipment.BackColor = System.Drawing.Color.Silver;
			buttonEquipment.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonEquipment.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonEquipment.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonEquipment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonEquipment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonEquipment.Location = new System.Drawing.Point(215, 28);
			buttonEquipment.Name = "buttonEquipment";
			buttonEquipment.Size = new System.Drawing.Size(96, 24);
			buttonEquipment.TabIndex = 14;
			buttonEquipment.Text = "Equipment";
			buttonEquipment.UseVisualStyleBackColor = false;
			buttonEquipment.Click += new System.EventHandler(buttonEquipment_Click);
			buttonBudget.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonBudget.BackColor = System.Drawing.Color.Silver;
			buttonBudget.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonBudget.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonBudget.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonBudget.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonBudget.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonBudget.Location = new System.Drawing.Point(115, 28);
			buttonBudget.Name = "buttonBudget";
			buttonBudget.Size = new System.Drawing.Size(96, 24);
			buttonBudget.TabIndex = 13;
			buttonBudget.Text = "Budget";
			buttonBudget.UseVisualStyleBackColor = false;
			buttonBudget.Click += new System.EventHandler(buttonBudget_Click);
			buttonFees.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonFees.BackColor = System.Drawing.Color.Silver;
			buttonFees.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonFees.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonFees.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonFees.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonFees.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonFees.Location = new System.Drawing.Point(14, 28);
			buttonFees.Name = "buttonFees";
			buttonFees.Size = new System.Drawing.Size(96, 24);
			buttonFees.TabIndex = 12;
			buttonFees.Text = "Fees";
			buttonFees.UseVisualStyleBackColor = false;
			buttonFees.Click += new System.EventHandler(buttonFees_Click);
			ultraLinkCurrency.AutoSize = true;
			ultraLinkCurrency.Location = new System.Drawing.Point(485, 121);
			ultraLinkCurrency.Name = "ultraLinkCurrency";
			ultraLinkCurrency.Size = new System.Drawing.Size(52, 14);
			ultraLinkCurrency.TabIndex = 32;
			ultraLinkCurrency.TabStop = true;
			ultraLinkCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraLinkCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraLinkCurrency.Value = "Currency:";
			appearance17.ForeColor = System.Drawing.Color.Blue;
			ultraLinkCurrency.VisitedLinkAppearance = appearance17;
			ultraLinkCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraLinkCurrency_LinkClicked);
			comboBoxCurrency.Assigned = false;
			comboBoxCurrency.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCurrency.CustomReportFieldName = "";
			comboBoxCurrency.CustomReportKey = "";
			comboBoxCurrency.CustomReportValueType = 1;
			comboBoxCurrency.DescriptionTextBox = null;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCurrency.DisplayLayout.Appearance = appearance18;
			comboBoxCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance19.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance19.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance19.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.GroupByBox.Appearance = appearance19;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance20;
			comboBoxCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance21.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance21.BackColor2 = System.Drawing.SystemColors.Control;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance21;
			comboBoxCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance22.BackColor = System.Drawing.SystemColors.Window;
			appearance22.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance22;
			appearance23.BackColor = System.Drawing.SystemColors.Highlight;
			appearance23.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance23;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.CardAreaAppearance = appearance24;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			appearance25.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCurrency.DisplayLayout.Override.CellAppearance = appearance25;
			comboBoxCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance26.BackColor = System.Drawing.SystemColors.Control;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance26;
			appearance27.TextHAlignAsString = "Left";
			comboBoxCurrency.DisplayLayout.Override.HeaderAppearance = appearance27;
			comboBoxCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			appearance28.BorderColor = System.Drawing.Color.Silver;
			comboBoxCurrency.DisplayLayout.Override.RowAppearance = appearance28;
			comboBoxCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance29.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance29;
			comboBoxCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCurrency.Editable = true;
			comboBoxCurrency.FilterString = "";
			comboBoxCurrency.HasAllAccount = false;
			comboBoxCurrency.HasCustom = false;
			comboBoxCurrency.IsDataLoaded = false;
			comboBoxCurrency.Location = new System.Drawing.Point(552, 118);
			comboBoxCurrency.MaxDropDownItems = 12;
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.ShowInactiveItems = false;
			comboBoxCurrency.ShowQuickAdd = true;
			comboBoxCurrency.Size = new System.Drawing.Size(125, 20);
			comboBoxCurrency.TabIndex = 9;
			comboBoxCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(484, 99);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(65, 13);
			mmLabel5.TabIndex = 29;
			mmLabel5.Text = "PO Number:";
			textBoxPONumber.BackColor = System.Drawing.Color.White;
			textBoxPONumber.CustomReportFieldName = "";
			textBoxPONumber.CustomReportKey = "";
			textBoxPONumber.CustomReportValueType = 1;
			textBoxPONumber.IsComboTextBox = false;
			textBoxPONumber.IsModified = false;
			textBoxPONumber.Location = new System.Drawing.Point(552, 96);
			textBoxPONumber.MaxLength = 30;
			textBoxPONumber.Name = "textBoxPONumber";
			textBoxPONumber.Size = new System.Drawing.Size(125, 20);
			textBoxPONumber.TabIndex = 6;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(271, 99);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(61, 13);
			mmLabel3.TabIndex = 26;
			mmLabel3.Text = "Reference:";
			textBoxReference.BackColor = System.Drawing.Color.White;
			textBoxReference.CustomReportFieldName = "";
			textBoxReference.CustomReportKey = "";
			textBoxReference.CustomReportValueType = 1;
			textBoxReference.IsComboTextBox = false;
			textBoxReference.IsModified = false;
			textBoxReference.Location = new System.Drawing.Point(345, 96);
			textBoxReference.MaxLength = 30;
			textBoxReference.Name = "textBoxReference";
			textBoxReference.Size = new System.Drawing.Size(137, 20);
			textBoxReference.TabIndex = 5;
			ultraLinkProjectMgr.AutoSize = true;
			ultraLinkProjectMgr.Location = new System.Drawing.Point(11, 120);
			ultraLinkProjectMgr.Name = "ultraLinkProjectMgr";
			ultraLinkProjectMgr.Size = new System.Drawing.Size(64, 14);
			ultraLinkProjectMgr.TabIndex = 23;
			ultraLinkProjectMgr.TabStop = true;
			ultraLinkProjectMgr.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraLinkProjectMgr.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraLinkProjectMgr.Value = "Project Mgr:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			ultraLinkProjectMgr.VisitedLinkAppearance = appearance30;
			ultraLinkProjectMgr.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraLinkProjectMgr_LinkClicked);
			ultraLinkSalesPerson.AutoSize = true;
			ultraLinkSalesPerson.Location = new System.Drawing.Point(12, 98);
			ultraLinkSalesPerson.Name = "ultraLinkSalesPerson";
			ultraLinkSalesPerson.Size = new System.Drawing.Size(69, 14);
			ultraLinkSalesPerson.TabIndex = 23;
			ultraLinkSalesPerson.TabStop = true;
			ultraLinkSalesPerson.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraLinkSalesPerson.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraLinkSalesPerson.Value = "Salesperson:";
			appearance31.ForeColor = System.Drawing.Color.Blue;
			ultraLinkSalesPerson.VisitedLinkAppearance = appearance31;
			ultraLinkSalesPerson.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraLinkSalesPerson_LinkClicked);
			comboBoxSalesperson.Assigned = false;
			comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesperson.CustomReportFieldName = "";
			comboBoxSalesperson.CustomReportKey = "";
			comboBoxSalesperson.CustomReportValueType = 1;
			comboBoxSalesperson.DescriptionTextBox = null;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesperson.DisplayLayout.Appearance = appearance32;
			comboBoxSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance33.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.GroupByBox.Appearance = appearance33;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance34;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance35.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance35.BackColor2 = System.Drawing.SystemColors.Control;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance35;
			comboBoxSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			appearance36.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance36;
			appearance37.BackColor = System.Drawing.SystemColors.Highlight;
			appearance37.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance37;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance38;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			appearance39.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesperson.DisplayLayout.Override.CellAppearance = appearance39;
			comboBoxSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance40.BackColor = System.Drawing.SystemColors.Control;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance40;
			appearance41.TextHAlignAsString = "Left";
			comboBoxSalesperson.DisplayLayout.Override.HeaderAppearance = appearance41;
			comboBoxSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance42.BackColor = System.Drawing.SystemColors.Window;
			appearance42.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesperson.DisplayLayout.Override.RowAppearance = appearance42;
			comboBoxSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance43.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance43;
			comboBoxSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesperson.Editable = true;
			comboBoxSalesperson.FilterString = "";
			comboBoxSalesperson.HasAllAccount = false;
			comboBoxSalesperson.HasCustom = false;
			comboBoxSalesperson.IsDataLoaded = false;
			comboBoxSalesperson.Location = new System.Drawing.Point(108, 96);
			comboBoxSalesperson.MaxDropDownItems = 12;
			comboBoxSalesperson.MaxLength = 64;
			comboBoxSalesperson.Name = "comboBoxSalesperson";
			comboBoxSalesperson.ShowInactiveItems = false;
			comboBoxSalesperson.ShowQuickAdd = true;
			comboBoxSalesperson.Size = new System.Drawing.Size(156, 20);
			comboBoxSalesperson.TabIndex = 4;
			comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(12, 16);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(64, 13);
			labelCode.TabIndex = 1;
			labelCode.Text = "Job Code:";
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.CustomReportFieldName = "";
			textBoxCustomerName.CustomReportKey = "";
			textBoxCustomerName.CustomReportValueType = 1;
			textBoxCustomerName.IsComboTextBox = false;
			textBoxCustomerName.IsModified = false;
			textBoxCustomerName.Location = new System.Drawing.Point(234, 61);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(315, 20);
			textBoxCustomerName.TabIndex = 4;
			textBoxCustomerName.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(12, 41);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(67, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Job Name:";
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Location = new System.Drawing.Point(380, 36);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(118, 21);
			comboBoxStatus.TabIndex = 1;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(108, 13);
			textBoxCode.MaxLength = 20;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(217, 20);
			textBoxCode.TabIndex = 0;
			labelJobStatus.AutoSize = true;
			labelJobStatus.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelJobStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelJobStatus.IsFieldHeader = false;
			labelJobStatus.IsRequired = false;
			labelJobStatus.Location = new System.Drawing.Point(331, 40);
			labelJobStatus.Name = "labelJobStatus";
			labelJobStatus.PenWidth = 1f;
			labelJobStatus.ShowBorder = false;
			labelJobStatus.Size = new System.Drawing.Size(47, 13);
			labelJobStatus.TabIndex = 22;
			labelJobStatus.Text = "Status:";
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(108, 37);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(217, 20);
			textBoxName.TabIndex = 2;
			comboBoxCutomers.Assigned = false;
			comboBoxCutomers.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCutomers.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCutomers.CustomReportFieldName = "";
			comboBoxCutomers.CustomReportKey = "";
			comboBoxCutomers.CustomReportValueType = 1;
			comboBoxCutomers.DescriptionTextBox = textBoxCustomerName;
			comboBoxCutomers.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCutomers.Editable = true;
			comboBoxCutomers.FilterString = "";
			comboBoxCutomers.FilterSysDocID = "";
			comboBoxCutomers.HasAll = false;
			comboBoxCutomers.HasCustom = false;
			comboBoxCutomers.IsDataLoaded = false;
			comboBoxCutomers.Location = new System.Drawing.Point(108, 61);
			comboBoxCutomers.MaxDropDownItems = 12;
			comboBoxCutomers.Name = "comboBoxCutomers";
			comboBoxCutomers.ShowConsignmentOnly = false;
			comboBoxCutomers.ShowInactive = false;
			comboBoxCutomers.ShowLPOCustomersOnly = false;
			comboBoxCutomers.ShowPROCustomersOnly = false;
			comboBoxCutomers.ShowQuickAdd = true;
			comboBoxCutomers.Size = new System.Drawing.Size(125, 20);
			comboBoxCutomers.TabIndex = 3;
			comboBoxCutomers.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimeStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeStartDate.Location = new System.Drawing.Point(108, 144);
			dateTimeStartDate.Name = "dateTimeStartDate";
			dateTimeStartDate.Size = new System.Drawing.Size(107, 20);
			dateTimeStartDate.TabIndex = 10;
			labelStartDate.AutoSize = true;
			labelStartDate.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelStartDate.IsFieldHeader = false;
			labelStartDate.IsRequired = false;
			labelStartDate.Location = new System.Drawing.Point(12, 147);
			labelStartDate.Name = "labelStartDate";
			labelStartDate.PenWidth = 1f;
			labelStartDate.ShowBorder = false;
			labelStartDate.Size = new System.Drawing.Size(58, 13);
			labelStartDate.TabIndex = 19;
			labelStartDate.Text = "Start Date:";
			labelStartDate.Click += new System.EventHandler(labelStartDate_Click);
			dateTimeEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeEndDate.Location = new System.Drawing.Point(278, 145);
			dateTimeEndDate.Name = "dateTimeEndDate";
			dateTimeEndDate.Size = new System.Drawing.Size(107, 20);
			dateTimeEndDate.TabIndex = 11;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(221, 148);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(55, 13);
			mmLabel2.TabIndex = 20;
			mmLabel2.Text = "End Date:";
			ultraTabPageControl1.Controls.Add(ultraGroupBox5);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(711, 559);
			ultraGroupBox5.Controls.Add(ultraFormattedLinkLabel7);
			ultraGroupBox5.Controls.Add(textBoxOdometer);
			ultraGroupBox5.Controls.Add(mmLabel35);
			ultraGroupBox5.Controls.Add(textBoxColor);
			ultraGroupBox5.Controls.Add(mmLabel34);
			ultraGroupBox5.Controls.Add(textBoxModel);
			ultraGroupBox5.Controls.Add(mmLabel33);
			ultraGroupBox5.Controls.Add(textBoxRegistrationNo);
			ultraGroupBox5.Controls.Add(mmLabel32);
			ultraGroupBox5.Controls.Add(textBoxVehicleName);
			ultraGroupBox5.Controls.Add(comboBoxVehicle);
			ultraGroupBox5.Location = new System.Drawing.Point(13, 11);
			ultraGroupBox5.Name = "ultraGroupBox5";
			ultraGroupBox5.Size = new System.Drawing.Size(686, 161);
			ultraGroupBox5.TabIndex = 41;
			ultraGroupBox5.Text = "Vehicle Details";
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(13, 36);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(44, 14);
			ultraFormattedLinkLabel7.TabIndex = 46;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Vehicle:";
			appearance44.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance44;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			textBoxOdometer.AllowDecimal = false;
			textBoxOdometer.BackColor = System.Drawing.Color.White;
			textBoxOdometer.CustomReportFieldName = "";
			textBoxOdometer.CustomReportKey = "";
			textBoxOdometer.CustomReportValueType = 1;
			textBoxOdometer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxOdometer.IsComboTextBox = false;
			textBoxOdometer.IsModified = false;
			textBoxOdometer.Location = new System.Drawing.Point(113, 124);
			textBoxOdometer.MaxLength = 15;
			textBoxOdometer.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxOdometer.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxOdometer.Name = "textBoxOdometer";
			textBoxOdometer.NullText = "0";
			textBoxOdometer.Size = new System.Drawing.Size(167, 20);
			textBoxOdometer.TabIndex = 4;
			textBoxOdometer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel35.AutoSize = true;
			mmLabel35.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel35.IsFieldHeader = false;
			mmLabel35.IsRequired = false;
			mmLabel35.Location = new System.Drawing.Point(13, 128);
			mmLabel35.Name = "mmLabel35";
			mmLabel35.PenWidth = 1f;
			mmLabel35.ShowBorder = false;
			mmLabel35.Size = new System.Drawing.Size(56, 13);
			mmLabel35.TabIndex = 45;
			mmLabel35.Text = "Odometer:";
			textBoxColor.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxColor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxColor.CustomReportFieldName = "";
			textBoxColor.CustomReportKey = "";
			textBoxColor.CustomReportValueType = 1;
			textBoxColor.IsComboTextBox = false;
			textBoxColor.IsModified = false;
			textBoxColor.Location = new System.Drawing.Point(113, 101);
			textBoxColor.MaxLength = 15;
			textBoxColor.Name = "textBoxColor";
			textBoxColor.ReadOnly = true;
			textBoxColor.Size = new System.Drawing.Size(168, 20);
			textBoxColor.TabIndex = 3;
			textBoxColor.TabStop = false;
			mmLabel34.AutoSize = true;
			mmLabel34.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel34.IsFieldHeader = false;
			mmLabel34.IsRequired = false;
			mmLabel34.Location = new System.Drawing.Point(13, 105);
			mmLabel34.Name = "mmLabel34";
			mmLabel34.PenWidth = 1f;
			mmLabel34.ShowBorder = false;
			mmLabel34.Size = new System.Drawing.Size(34, 13);
			mmLabel34.TabIndex = 43;
			mmLabel34.Text = "Color:";
			textBoxModel.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxModel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxModel.CustomReportFieldName = "";
			textBoxModel.CustomReportKey = "";
			textBoxModel.CustomReportValueType = 1;
			textBoxModel.IsComboTextBox = false;
			textBoxModel.IsModified = false;
			textBoxModel.Location = new System.Drawing.Point(113, 79);
			textBoxModel.MaxLength = 15;
			textBoxModel.Name = "textBoxModel";
			textBoxModel.ReadOnly = true;
			textBoxModel.Size = new System.Drawing.Size(168, 20);
			textBoxModel.TabIndex = 2;
			textBoxModel.TabStop = false;
			mmLabel33.AutoSize = true;
			mmLabel33.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel33.IsFieldHeader = false;
			mmLabel33.IsRequired = false;
			mmLabel33.Location = new System.Drawing.Point(13, 83);
			mmLabel33.Name = "mmLabel33";
			mmLabel33.PenWidth = 1f;
			mmLabel33.ShowBorder = false;
			mmLabel33.Size = new System.Drawing.Size(39, 13);
			mmLabel33.TabIndex = 41;
			mmLabel33.Text = "Model:";
			textBoxRegistrationNo.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRegistrationNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxRegistrationNo.CustomReportFieldName = "";
			textBoxRegistrationNo.CustomReportKey = "";
			textBoxRegistrationNo.CustomReportValueType = 1;
			textBoxRegistrationNo.IsComboTextBox = false;
			textBoxRegistrationNo.IsModified = false;
			textBoxRegistrationNo.Location = new System.Drawing.Point(113, 56);
			textBoxRegistrationNo.MaxLength = 15;
			textBoxRegistrationNo.Name = "textBoxRegistrationNo";
			textBoxRegistrationNo.ReadOnly = true;
			textBoxRegistrationNo.Size = new System.Drawing.Size(168, 20);
			textBoxRegistrationNo.TabIndex = 1;
			textBoxRegistrationNo.TabStop = false;
			mmLabel32.AutoSize = true;
			mmLabel32.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel32.IsFieldHeader = false;
			mmLabel32.IsRequired = false;
			mmLabel32.Location = new System.Drawing.Point(13, 60);
			mmLabel32.Name = "mmLabel32";
			mmLabel32.PenWidth = 1f;
			mmLabel32.ShowBorder = false;
			mmLabel32.Size = new System.Drawing.Size(83, 13);
			mmLabel32.TabIndex = 39;
			mmLabel32.Text = "Registration No:";
			textBoxVehicleName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVehicleName.CustomReportFieldName = "";
			textBoxVehicleName.CustomReportKey = "";
			textBoxVehicleName.CustomReportValueType = 1;
			textBoxVehicleName.IsComboTextBox = false;
			textBoxVehicleName.IsModified = false;
			textBoxVehicleName.Location = new System.Drawing.Point(283, 33);
			textBoxVehicleName.MaxLength = 64;
			textBoxVehicleName.Name = "textBoxVehicleName";
			textBoxVehicleName.ReadOnly = true;
			textBoxVehicleName.Size = new System.Drawing.Size(315, 20);
			textBoxVehicleName.TabIndex = 37;
			textBoxVehicleName.TabStop = false;
			comboBoxVehicle.Assigned = false;
			comboBoxVehicle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVehicle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVehicle.CustomReportFieldName = "";
			comboBoxVehicle.CustomReportKey = "";
			comboBoxVehicle.CustomReportValueType = 1;
			comboBoxVehicle.DescriptionTextBox = textBoxVehicleName;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVehicle.DisplayLayout.Appearance = appearance45;
			comboBoxVehicle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVehicle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			comboBoxVehicle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicle.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			comboBoxVehicle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVehicle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVehicle.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVehicle.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			comboBoxVehicle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVehicle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVehicle.DisplayLayout.Override.CellAppearance = appearance52;
			comboBoxVehicle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVehicle.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			comboBoxVehicle.DisplayLayout.Override.HeaderAppearance = appearance54;
			comboBoxVehicle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVehicle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			comboBoxVehicle.DisplayLayout.Override.RowAppearance = appearance55;
			comboBoxVehicle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVehicle.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
			comboBoxVehicle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVehicle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVehicle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVehicle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVehicle.Editable = true;
			comboBoxVehicle.FilterString = "";
			comboBoxVehicle.HasAllAccount = false;
			comboBoxVehicle.HasCustom = false;
			comboBoxVehicle.IsDataLoaded = false;
			comboBoxVehicle.Location = new System.Drawing.Point(113, 33);
			comboBoxVehicle.MaxDropDownItems = 12;
			comboBoxVehicle.Name = "comboBoxVehicle";
			comboBoxVehicle.ShowInactiveItems = false;
			comboBoxVehicle.ShowQuickAdd = true;
			comboBoxVehicle.Size = new System.Drawing.Size(168, 20);
			comboBoxVehicle.TabIndex = 0;
			comboBoxVehicle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVehicle.SelectedIndexChanged += new System.EventHandler(comboBoxVehicle_SelectedIndexChanged);
			tabPageAccounts.Controls.Add(textBoxProjectCostAccount);
			tabPageAccounts.Controls.Add(ultraFormattedLinkLabel16);
			tabPageAccounts.Controls.Add(comboBoxProjectCostAccount);
			tabPageAccounts.Controls.Add(textBoxAdvanceName);
			tabPageAccounts.Controls.Add(comboBoxAdvanceAccount);
			tabPageAccounts.Controls.Add(ultraFormattedLinkLabel4);
			tabPageAccounts.Controls.Add(textBoxRetentionAccountName);
			tabPageAccounts.Controls.Add(comboBoxRetentionAccount);
			tabPageAccounts.Controls.Add(ultraFormattedLinkLabel2);
			tabPageAccounts.Controls.Add(textBoxTimesheetAccountName);
			tabPageAccounts.Controls.Add(comboBoxTimesheetAccount);
			tabPageAccounts.Controls.Add(ultraFormattedLinkLabel1);
			tabPageAccounts.Controls.Add(textBoxIncomeAccountName);
			tabPageAccounts.Controls.Add(comboBoxIncomeAccount);
			tabPageAccounts.Controls.Add(ultraFormattedLinkLabel6);
			tabPageAccounts.Controls.Add(textBoxWIPAccountName);
			tabPageAccounts.Controls.Add(comboBoxWIPAccount);
			tabPageAccounts.Controls.Add(ultraFormattedLinkLabel3);
			tabPageAccounts.Location = new System.Drawing.Point(-10000, -10000);
			tabPageAccounts.Name = "tabPageAccounts";
			tabPageAccounts.Size = new System.Drawing.Size(711, 559);
			textBoxProjectCostAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectCostAccount.CustomReportFieldName = "";
			textBoxProjectCostAccount.CustomReportKey = "";
			textBoxProjectCostAccount.CustomReportValueType = 1;
			textBoxProjectCostAccount.ForeColor = System.Drawing.Color.Black;
			textBoxProjectCostAccount.IsComboTextBox = false;
			textBoxProjectCostAccount.IsModified = false;
			textBoxProjectCostAccount.Location = new System.Drawing.Point(300, 59);
			textBoxProjectCostAccount.MaxLength = 255;
			textBoxProjectCostAccount.Name = "textBoxProjectCostAccount";
			textBoxProjectCostAccount.ReadOnly = true;
			textBoxProjectCostAccount.Size = new System.Drawing.Size(315, 20);
			textBoxProjectCostAccount.TabIndex = 7;
			textBoxProjectCostAccount.TabStop = false;
			appearance57.FontData.BoldAsString = "False";
			appearance57.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel16.Appearance = appearance57;
			ultraFormattedLinkLabel16.AutoSize = true;
			ultraFormattedLinkLabel16.Location = new System.Drawing.Point(11, 58);
			ultraFormattedLinkLabel16.Name = "ultraFormattedLinkLabel16";
			ultraFormattedLinkLabel16.Size = new System.Drawing.Size(80, 15);
			ultraFormattedLinkLabel16.TabIndex = 5;
			ultraFormattedLinkLabel16.TabStop = true;
			ultraFormattedLinkLabel16.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel16.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel16.Value = "Cost of Project:";
			appearance58.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel16.VisitedLinkAppearance = appearance58;
			comboBoxProjectCostAccount.AlwaysInEditMode = true;
			comboBoxProjectCostAccount.Assigned = false;
			comboBoxProjectCostAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProjectCostAccount.CustomReportFieldName = "";
			comboBoxProjectCostAccount.CustomReportKey = "";
			comboBoxProjectCostAccount.CustomReportValueType = 1;
			comboBoxProjectCostAccount.DescriptionTextBox = textBoxProjectCostAccount;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProjectCostAccount.DisplayLayout.Appearance = appearance59;
			comboBoxProjectCostAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProjectCostAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.Appearance = appearance60;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance61;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance62.BackColor2 = System.Drawing.SystemColors.Control;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance62;
			comboBoxProjectCostAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProjectCostAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProjectCostAccount.DisplayLayout.Override.ActiveCellAppearance = appearance63;
			appearance64.BackColor = System.Drawing.SystemColors.Highlight;
			appearance64.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProjectCostAccount.DisplayLayout.Override.ActiveRowAppearance = appearance64;
			comboBoxProjectCostAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProjectCostAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProjectCostAccount.DisplayLayout.Override.CardAreaAppearance = appearance65;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			appearance66.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProjectCostAccount.DisplayLayout.Override.CellAppearance = appearance66;
			comboBoxProjectCostAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProjectCostAccount.DisplayLayout.Override.CellPadding = 0;
			appearance67.BackColor = System.Drawing.SystemColors.Control;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectCostAccount.DisplayLayout.Override.GroupByRowAppearance = appearance67;
			appearance68.TextHAlignAsString = "Left";
			comboBoxProjectCostAccount.DisplayLayout.Override.HeaderAppearance = appearance68;
			comboBoxProjectCostAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProjectCostAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.Color.Silver;
			comboBoxProjectCostAccount.DisplayLayout.Override.RowAppearance = appearance69;
			comboBoxProjectCostAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProjectCostAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance70;
			comboBoxProjectCostAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProjectCostAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProjectCostAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProjectCostAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProjectCostAccount.Editable = true;
			comboBoxProjectCostAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxProjectCostAccount.FilterString = "";
			comboBoxProjectCostAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxProjectCostAccount.FilterSysDocID = "";
			comboBoxProjectCostAccount.HasAllAccount = false;
			comboBoxProjectCostAccount.HasCustom = false;
			comboBoxProjectCostAccount.IsDataLoaded = false;
			comboBoxProjectCostAccount.Location = new System.Drawing.Point(109, 59);
			comboBoxProjectCostAccount.MaxDropDownItems = 12;
			comboBoxProjectCostAccount.Name = "comboBoxProjectCostAccount";
			comboBoxProjectCostAccount.ShowInactiveItems = false;
			comboBoxProjectCostAccount.ShowQuickAdd = true;
			comboBoxProjectCostAccount.Size = new System.Drawing.Size(188, 20);
			comboBoxProjectCostAccount.TabIndex = 6;
			comboBoxProjectCostAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxAdvanceName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAdvanceName.CustomReportFieldName = "";
			textBoxAdvanceName.CustomReportKey = "";
			textBoxAdvanceName.CustomReportValueType = 1;
			textBoxAdvanceName.IsComboTextBox = false;
			textBoxAdvanceName.IsModified = false;
			textBoxAdvanceName.Location = new System.Drawing.Point(300, 127);
			textBoxAdvanceName.MaxLength = 64;
			textBoxAdvanceName.Name = "textBoxAdvanceName";
			textBoxAdvanceName.ReadOnly = true;
			textBoxAdvanceName.Size = new System.Drawing.Size(315, 20);
			textBoxAdvanceName.TabIndex = 16;
			textBoxAdvanceName.TabStop = false;
			comboBoxAdvanceAccount.Assigned = false;
			comboBoxAdvanceAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAdvanceAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAdvanceAccount.CustomReportFieldName = "";
			comboBoxAdvanceAccount.CustomReportKey = "";
			comboBoxAdvanceAccount.CustomReportValueType = 1;
			comboBoxAdvanceAccount.DescriptionTextBox = textBoxAdvanceName;
			comboBoxAdvanceAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAdvanceAccount.Editable = true;
			comboBoxAdvanceAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAdvanceAccount.FilterString = "";
			comboBoxAdvanceAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAdvanceAccount.FilterSysDocID = "";
			comboBoxAdvanceAccount.HasAllAccount = false;
			comboBoxAdvanceAccount.HasCustom = false;
			comboBoxAdvanceAccount.IsDataLoaded = false;
			comboBoxAdvanceAccount.Location = new System.Drawing.Point(109, 127);
			comboBoxAdvanceAccount.MaxDropDownItems = 12;
			comboBoxAdvanceAccount.Name = "comboBoxAdvanceAccount";
			comboBoxAdvanceAccount.ShowInactiveItems = false;
			comboBoxAdvanceAccount.ShowQuickAdd = true;
			comboBoxAdvanceAccount.Size = new System.Drawing.Size(188, 20);
			comboBoxAdvanceAccount.TabIndex = 15;
			comboBoxAdvanceAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(11, 128);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(96, 18);
			ultraFormattedLinkLabel4.TabIndex = 14;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Advance:";
			appearance71.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance71;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			textBoxRetentionAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRetentionAccountName.CustomReportFieldName = "";
			textBoxRetentionAccountName.CustomReportKey = "";
			textBoxRetentionAccountName.CustomReportValueType = 1;
			textBoxRetentionAccountName.IsComboTextBox = false;
			textBoxRetentionAccountName.IsModified = false;
			textBoxRetentionAccountName.Location = new System.Drawing.Point(300, 105);
			textBoxRetentionAccountName.MaxLength = 64;
			textBoxRetentionAccountName.Name = "textBoxRetentionAccountName";
			textBoxRetentionAccountName.ReadOnly = true;
			textBoxRetentionAccountName.Size = new System.Drawing.Size(315, 20);
			textBoxRetentionAccountName.TabIndex = 13;
			textBoxRetentionAccountName.TabStop = false;
			comboBoxRetentionAccount.Assigned = false;
			comboBoxRetentionAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxRetentionAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRetentionAccount.CustomReportFieldName = "";
			comboBoxRetentionAccount.CustomReportKey = "";
			comboBoxRetentionAccount.CustomReportValueType = 1;
			comboBoxRetentionAccount.DescriptionTextBox = textBoxRetentionAccountName;
			comboBoxRetentionAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRetentionAccount.Editable = true;
			comboBoxRetentionAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxRetentionAccount.FilterString = "";
			comboBoxRetentionAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxRetentionAccount.FilterSysDocID = "";
			comboBoxRetentionAccount.HasAllAccount = false;
			comboBoxRetentionAccount.HasCustom = false;
			comboBoxRetentionAccount.IsDataLoaded = false;
			comboBoxRetentionAccount.Location = new System.Drawing.Point(109, 105);
			comboBoxRetentionAccount.MaxDropDownItems = 12;
			comboBoxRetentionAccount.Name = "comboBoxRetentionAccount";
			comboBoxRetentionAccount.ShowInactiveItems = false;
			comboBoxRetentionAccount.ShowQuickAdd = true;
			comboBoxRetentionAccount.Size = new System.Drawing.Size(188, 20);
			comboBoxRetentionAccount.TabIndex = 12;
			comboBoxRetentionAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(11, 106);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(96, 18);
			ultraFormattedLinkLabel2.TabIndex = 11;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Retention:";
			appearance72.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance72;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			textBoxTimesheetAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTimesheetAccountName.CustomReportFieldName = "";
			textBoxTimesheetAccountName.CustomReportKey = "";
			textBoxTimesheetAccountName.CustomReportValueType = 1;
			textBoxTimesheetAccountName.IsComboTextBox = false;
			textBoxTimesheetAccountName.IsModified = false;
			textBoxTimesheetAccountName.Location = new System.Drawing.Point(300, 82);
			textBoxTimesheetAccountName.MaxLength = 64;
			textBoxTimesheetAccountName.Name = "textBoxTimesheetAccountName";
			textBoxTimesheetAccountName.ReadOnly = true;
			textBoxTimesheetAccountName.Size = new System.Drawing.Size(315, 20);
			textBoxTimesheetAccountName.TabIndex = 10;
			textBoxTimesheetAccountName.TabStop = false;
			comboBoxTimesheetAccount.Assigned = false;
			comboBoxTimesheetAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTimesheetAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTimesheetAccount.CustomReportFieldName = "";
			comboBoxTimesheetAccount.CustomReportKey = "";
			comboBoxTimesheetAccount.CustomReportValueType = 1;
			comboBoxTimesheetAccount.DescriptionTextBox = textBoxTimesheetAccountName;
			comboBoxTimesheetAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTimesheetAccount.Editable = true;
			comboBoxTimesheetAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxTimesheetAccount.FilterString = "";
			comboBoxTimesheetAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxTimesheetAccount.FilterSysDocID = "";
			comboBoxTimesheetAccount.HasAllAccount = false;
			comboBoxTimesheetAccount.HasCustom = false;
			comboBoxTimesheetAccount.IsDataLoaded = false;
			comboBoxTimesheetAccount.Location = new System.Drawing.Point(109, 82);
			comboBoxTimesheetAccount.MaxDropDownItems = 12;
			comboBoxTimesheetAccount.Name = "comboBoxTimesheetAccount";
			comboBoxTimesheetAccount.ShowInactiveItems = false;
			comboBoxTimesheetAccount.ShowQuickAdd = true;
			comboBoxTimesheetAccount.Size = new System.Drawing.Size(188, 20);
			comboBoxTimesheetAccount.TabIndex = 9;
			comboBoxTimesheetAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(11, 83);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(96, 18);
			ultraFormattedLinkLabel1.TabIndex = 8;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Timesheet Contra:";
			appearance73.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance73;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			textBoxIncomeAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxIncomeAccountName.CustomReportFieldName = "";
			textBoxIncomeAccountName.CustomReportKey = "";
			textBoxIncomeAccountName.CustomReportValueType = 1;
			textBoxIncomeAccountName.IsComboTextBox = false;
			textBoxIncomeAccountName.IsModified = false;
			textBoxIncomeAccountName.Location = new System.Drawing.Point(300, 36);
			textBoxIncomeAccountName.MaxLength = 64;
			textBoxIncomeAccountName.Name = "textBoxIncomeAccountName";
			textBoxIncomeAccountName.ReadOnly = true;
			textBoxIncomeAccountName.Size = new System.Drawing.Size(315, 20);
			textBoxIncomeAccountName.TabIndex = 4;
			textBoxIncomeAccountName.TabStop = false;
			comboBoxIncomeAccount.Assigned = false;
			comboBoxIncomeAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxIncomeAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxIncomeAccount.CustomReportFieldName = "";
			comboBoxIncomeAccount.CustomReportKey = "";
			comboBoxIncomeAccount.CustomReportValueType = 1;
			comboBoxIncomeAccount.DescriptionTextBox = textBoxIncomeAccountName;
			comboBoxIncomeAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxIncomeAccount.Editable = true;
			comboBoxIncomeAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxIncomeAccount.FilterString = "";
			comboBoxIncomeAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxIncomeAccount.FilterSysDocID = "";
			comboBoxIncomeAccount.HasAllAccount = false;
			comboBoxIncomeAccount.HasCustom = false;
			comboBoxIncomeAccount.IsDataLoaded = false;
			comboBoxIncomeAccount.Location = new System.Drawing.Point(109, 36);
			comboBoxIncomeAccount.MaxDropDownItems = 12;
			comboBoxIncomeAccount.Name = "comboBoxIncomeAccount";
			comboBoxIncomeAccount.ShowInactiveItems = false;
			comboBoxIncomeAccount.ShowQuickAdd = true;
			comboBoxIncomeAccount.Size = new System.Drawing.Size(188, 20);
			comboBoxIncomeAccount.TabIndex = 3;
			comboBoxIncomeAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(11, 38);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(96, 18);
			ultraFormattedLinkLabel6.TabIndex = 2;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Income:";
			appearance74.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance74;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			textBoxWIPAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxWIPAccountName.CustomReportFieldName = "";
			textBoxWIPAccountName.CustomReportKey = "";
			textBoxWIPAccountName.CustomReportValueType = 1;
			textBoxWIPAccountName.IsComboTextBox = false;
			textBoxWIPAccountName.IsModified = false;
			textBoxWIPAccountName.Location = new System.Drawing.Point(300, 13);
			textBoxWIPAccountName.MaxLength = 64;
			textBoxWIPAccountName.Name = "textBoxWIPAccountName";
			textBoxWIPAccountName.ReadOnly = true;
			textBoxWIPAccountName.Size = new System.Drawing.Size(315, 20);
			textBoxWIPAccountName.TabIndex = 1;
			textBoxWIPAccountName.TabStop = false;
			comboBoxWIPAccount.Assigned = false;
			comboBoxWIPAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxWIPAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxWIPAccount.CustomReportFieldName = "";
			comboBoxWIPAccount.CustomReportKey = "";
			comboBoxWIPAccount.CustomReportValueType = 1;
			comboBoxWIPAccount.DescriptionTextBox = textBoxWIPAccountName;
			comboBoxWIPAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxWIPAccount.Editable = true;
			comboBoxWIPAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxWIPAccount.FilterString = "";
			comboBoxWIPAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.WIP;
			comboBoxWIPAccount.FilterSysDocID = "";
			comboBoxWIPAccount.HasAllAccount = false;
			comboBoxWIPAccount.HasCustom = false;
			comboBoxWIPAccount.IsDataLoaded = false;
			comboBoxWIPAccount.Location = new System.Drawing.Point(109, 13);
			comboBoxWIPAccount.MaxDropDownItems = 12;
			comboBoxWIPAccount.Name = "comboBoxWIPAccount";
			comboBoxWIPAccount.ShowInactiveItems = false;
			comboBoxWIPAccount.ShowQuickAdd = true;
			comboBoxWIPAccount.Size = new System.Drawing.Size(188, 20);
			comboBoxWIPAccount.TabIndex = 0;
			comboBoxWIPAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(11, 15);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(96, 17);
			ultraFormattedLinkLabel3.TabIndex = 24;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Work in Progress:";
			appearance75.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance75;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			tabPageSummary.Controls.Add(ultraGroupBox6);
			tabPageSummary.Controls.Add(ultraGroupBox4);
			tabPageSummary.Controls.Add(ultraGroupBox3);
			tabPageSummary.Controls.Add(ultraGroupBox2);
			tabPageSummary.Location = new System.Drawing.Point(-10000, -10000);
			tabPageSummary.Name = "tabPageSummary";
			tabPageSummary.Size = new System.Drawing.Size(711, 559);
			ultraGroupBox6.Controls.Add(line1);
			ultraGroupBox6.Controls.Add(mmLabel39);
			ultraGroupBox6.Controls.Add(textBoxTotal);
			ultraGroupBox6.Controls.Add(textBoxMaterial);
			ultraGroupBox6.Controls.Add(mmLabel41);
			ultraGroupBox6.Controls.Add(textBoxProfit);
			ultraGroupBox6.Controls.Add(mmLabel40);
			ultraGroupBox6.Controls.Add(mmLabel37);
			ultraGroupBox6.Controls.Add(mmLabel36);
			ultraGroupBox6.Controls.Add(textBoxOverHeadVariance);
			ultraGroupBox6.Controls.Add(mmLabel31);
			ultraGroupBox6.Controls.Add(textBoxLaborVariance);
			ultraGroupBox6.Controls.Add(mmLabel30);
			ultraGroupBox6.Controls.Add(textBoxMisVariance);
			ultraGroupBox6.Controls.Add(mmLabel29);
			ultraGroupBox6.Controls.Add(textBoxOverHeadAmount);
			ultraGroupBox6.Controls.Add(mmLabel26);
			ultraGroupBox6.Controls.Add(mmLabel27);
			ultraGroupBox6.Controls.Add(textBoxLaborAmount);
			ultraGroupBox6.Controls.Add(mmLabel28);
			ultraGroupBox6.Controls.Add(textBoxMiscellaneousAmount);
			ultraGroupBox6.Location = new System.Drawing.Point(10, 330);
			ultraGroupBox6.Name = "ultraGroupBox6";
			ultraGroupBox6.Size = new System.Drawing.Size(686, 185);
			ultraGroupBox6.TabIndex = 3;
			ultraGroupBox6.Text = "Amount/Variance";
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(3, 150);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(291, 1);
			line1.TabIndex = 190;
			line1.TabStop = false;
			mmLabel39.AutoSize = true;
			mmLabel39.BackColor = System.Drawing.Color.Transparent;
			mmLabel39.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel39.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel39.IsFieldHeader = false;
			mmLabel39.IsRequired = false;
			mmLabel39.Location = new System.Drawing.Point(10, 162);
			mmLabel39.Name = "mmLabel39";
			mmLabel39.PenWidth = 1f;
			mmLabel39.ShowBorder = false;
			mmLabel39.Size = new System.Drawing.Size(39, 13);
			mmLabel39.TabIndex = 189;
			mmLabel39.Text = "Total:";
			textBoxTotal.AllowDecimal = true;
			textBoxTotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotal.CustomReportFieldName = "";
			textBoxTotal.CustomReportKey = "";
			textBoxTotal.CustomReportValueType = 1;
			textBoxTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			textBoxTotal.IsComboTextBox = false;
			textBoxTotal.IsModified = false;
			textBoxTotal.Location = new System.Drawing.Point(114, 159);
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
			textBoxTotal.Size = new System.Drawing.Size(110, 20);
			textBoxTotal.TabIndex = 188;
			textBoxTotal.Text = "0.00";
			textBoxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxMaterial.AllowDecimal = true;
			textBoxMaterial.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxMaterial.CustomReportFieldName = "";
			textBoxMaterial.CustomReportKey = "";
			textBoxMaterial.CustomReportValueType = 1;
			textBoxMaterial.IsComboTextBox = false;
			textBoxMaterial.IsModified = false;
			textBoxMaterial.Location = new System.Drawing.Point(114, 122);
			textBoxMaterial.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxMaterial.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxMaterial.Name = "textBoxMaterial";
			textBoxMaterial.NullText = "0";
			textBoxMaterial.ReadOnly = true;
			textBoxMaterial.Size = new System.Drawing.Size(110, 20);
			textBoxMaterial.TabIndex = 7;
			textBoxMaterial.Text = "0.00";
			textBoxMaterial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxMaterial.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel41.AutoSize = true;
			mmLabel41.BackColor = System.Drawing.Color.Transparent;
			mmLabel41.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel41.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel41.IsFieldHeader = false;
			mmLabel41.IsRequired = false;
			mmLabel41.Location = new System.Drawing.Point(8, 128);
			mmLabel41.Name = "mmLabel41";
			mmLabel41.PenWidth = 1f;
			mmLabel41.ShowBorder = false;
			mmLabel41.Size = new System.Drawing.Size(49, 13);
			mmLabel41.TabIndex = 187;
			mmLabel41.Text = "Material:";
			textBoxProfit.AllowDecimal = true;
			textBoxProfit.BackColor = System.Drawing.Color.White;
			textBoxProfit.CustomReportFieldName = "";
			textBoxProfit.CustomReportKey = "";
			textBoxProfit.CustomReportValueType = 1;
			textBoxProfit.IsComboTextBox = false;
			textBoxProfit.IsModified = false;
			textBoxProfit.Location = new System.Drawing.Point(114, 100);
			textBoxProfit.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxProfit.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxProfit.Name = "textBoxProfit";
			textBoxProfit.NullText = "0";
			textBoxProfit.Size = new System.Drawing.Size(110, 20);
			textBoxProfit.TabIndex = 6;
			textBoxProfit.Text = "0.00";
			textBoxProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxProfit.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel40.AutoSize = true;
			mmLabel40.BackColor = System.Drawing.Color.Transparent;
			mmLabel40.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel40.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel40.IsFieldHeader = false;
			mmLabel40.IsRequired = false;
			mmLabel40.Location = new System.Drawing.Point(8, 106);
			mmLabel40.Name = "mmLabel40";
			mmLabel40.PenWidth = 1f;
			mmLabel40.ShowBorder = false;
			mmLabel40.Size = new System.Drawing.Size(37, 13);
			mmLabel40.TabIndex = 184;
			mmLabel40.Text = "Profit:";
			mmLabel37.AutoSize = true;
			mmLabel37.BackColor = System.Drawing.Color.Transparent;
			mmLabel37.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel37.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel37.IsFieldHeader = false;
			mmLabel37.IsRequired = false;
			mmLabel37.Location = new System.Drawing.Point(230, 16);
			mmLabel37.Name = "mmLabel37";
			mmLabel37.PenWidth = 1f;
			mmLabel37.ShowBorder = false;
			mmLabel37.Size = new System.Drawing.Size(62, 13);
			mmLabel37.TabIndex = 181;
			mmLabel37.Text = "Variance %";
			mmLabel36.AutoSize = true;
			mmLabel36.BackColor = System.Drawing.Color.Transparent;
			mmLabel36.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel36.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel36.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel36.IsFieldHeader = false;
			mmLabel36.IsRequired = false;
			mmLabel36.Location = new System.Drawing.Point(131, 16);
			mmLabel36.Name = "mmLabel36";
			mmLabel36.PenWidth = 1f;
			mmLabel36.ShowBorder = false;
			mmLabel36.Size = new System.Drawing.Size(44, 13);
			mmLabel36.TabIndex = 180;
			mmLabel36.Text = "Amount";
			textBoxOverHeadVariance.CustomReportFieldName = "";
			textBoxOverHeadVariance.CustomReportKey = "";
			textBoxOverHeadVariance.CustomReportValueType = 1;
			textBoxOverHeadVariance.IsComboTextBox = false;
			textBoxOverHeadVariance.IsModified = false;
			textBoxOverHeadVariance.Location = new System.Drawing.Point(230, 80);
			textBoxOverHeadVariance.Name = "textBoxOverHeadVariance";
			textBoxOverHeadVariance.Size = new System.Drawing.Size(48, 20);
			textBoxOverHeadVariance.TabIndex = 179;
			textBoxOverHeadVariance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel31.AutoSize = true;
			mmLabel31.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel31.IsFieldHeader = false;
			mmLabel31.IsRequired = false;
			mmLabel31.Location = new System.Drawing.Point(284, 84);
			mmLabel31.Name = "mmLabel31";
			mmLabel31.PenWidth = 1f;
			mmLabel31.ShowBorder = false;
			mmLabel31.Size = new System.Drawing.Size(15, 13);
			mmLabel31.TabIndex = 5;
			mmLabel31.Text = "%";
			textBoxLaborVariance.CustomReportFieldName = "";
			textBoxLaborVariance.CustomReportKey = "";
			textBoxLaborVariance.CustomReportValueType = 1;
			textBoxLaborVariance.IsComboTextBox = false;
			textBoxLaborVariance.IsModified = false;
			textBoxLaborVariance.Location = new System.Drawing.Point(230, 57);
			textBoxLaborVariance.Name = "textBoxLaborVariance";
			textBoxLaborVariance.Size = new System.Drawing.Size(48, 20);
			textBoxLaborVariance.TabIndex = 177;
			textBoxLaborVariance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel30.AutoSize = true;
			mmLabel30.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel30.IsFieldHeader = false;
			mmLabel30.IsRequired = false;
			mmLabel30.Location = new System.Drawing.Point(284, 61);
			mmLabel30.Name = "mmLabel30";
			mmLabel30.PenWidth = 1f;
			mmLabel30.ShowBorder = false;
			mmLabel30.Size = new System.Drawing.Size(15, 13);
			mmLabel30.TabIndex = 3;
			mmLabel30.Text = "%";
			textBoxMisVariance.CustomReportFieldName = "";
			textBoxMisVariance.CustomReportKey = "";
			textBoxMisVariance.CustomReportValueType = 1;
			textBoxMisVariance.IsComboTextBox = false;
			textBoxMisVariance.IsModified = false;
			textBoxMisVariance.Location = new System.Drawing.Point(230, 34);
			textBoxMisVariance.Name = "textBoxMisVariance";
			textBoxMisVariance.Size = new System.Drawing.Size(48, 20);
			textBoxMisVariance.TabIndex = 175;
			textBoxMisVariance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel29.AutoSize = true;
			mmLabel29.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel29.IsFieldHeader = false;
			mmLabel29.IsRequired = false;
			mmLabel29.Location = new System.Drawing.Point(284, 38);
			mmLabel29.Name = "mmLabel29";
			mmLabel29.PenWidth = 1f;
			mmLabel29.ShowBorder = false;
			mmLabel29.Size = new System.Drawing.Size(15, 13);
			mmLabel29.TabIndex = 1;
			mmLabel29.Text = "%";
			textBoxOverHeadAmount.AllowDecimal = true;
			textBoxOverHeadAmount.BackColor = System.Drawing.Color.White;
			textBoxOverHeadAmount.CustomReportFieldName = "";
			textBoxOverHeadAmount.CustomReportKey = "";
			textBoxOverHeadAmount.CustomReportValueType = 1;
			textBoxOverHeadAmount.IsComboTextBox = false;
			textBoxOverHeadAmount.IsModified = false;
			textBoxOverHeadAmount.Location = new System.Drawing.Point(114, 78);
			textBoxOverHeadAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxOverHeadAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxOverHeadAmount.Name = "textBoxOverHeadAmount";
			textBoxOverHeadAmount.NullText = "0";
			textBoxOverHeadAmount.Size = new System.Drawing.Size(110, 20);
			textBoxOverHeadAmount.TabIndex = 4;
			textBoxOverHeadAmount.Text = "0.00";
			textBoxOverHeadAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxOverHeadAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel26.AutoSize = true;
			mmLabel26.BackColor = System.Drawing.Color.Transparent;
			mmLabel26.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel26.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel26.IsFieldHeader = false;
			mmLabel26.IsRequired = false;
			mmLabel26.Location = new System.Drawing.Point(8, 83);
			mmLabel26.Name = "mmLabel26";
			mmLabel26.PenWidth = 1f;
			mmLabel26.ShowBorder = false;
			mmLabel26.Size = new System.Drawing.Size(60, 13);
			mmLabel26.TabIndex = 32;
			mmLabel26.Text = "OverHead:";
			mmLabel27.AutoSize = true;
			mmLabel27.BackColor = System.Drawing.Color.Transparent;
			mmLabel27.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel27.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel27.IsFieldHeader = false;
			mmLabel27.IsRequired = false;
			mmLabel27.Location = new System.Drawing.Point(8, 37);
			mmLabel27.Name = "mmLabel27";
			mmLabel27.PenWidth = 1f;
			mmLabel27.ShowBorder = false;
			mmLabel27.Size = new System.Drawing.Size(76, 13);
			mmLabel27.TabIndex = 27;
			mmLabel27.Text = "Miscellaneous:";
			textBoxLaborAmount.AllowDecimal = true;
			textBoxLaborAmount.BackColor = System.Drawing.Color.White;
			textBoxLaborAmount.CustomReportFieldName = "";
			textBoxLaborAmount.CustomReportKey = "";
			textBoxLaborAmount.CustomReportValueType = 1;
			textBoxLaborAmount.IsComboTextBox = false;
			textBoxLaborAmount.IsModified = false;
			textBoxLaborAmount.Location = new System.Drawing.Point(114, 56);
			textBoxLaborAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxLaborAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxLaborAmount.Name = "textBoxLaborAmount";
			textBoxLaborAmount.NullText = "0";
			textBoxLaborAmount.Size = new System.Drawing.Size(110, 20);
			textBoxLaborAmount.TabIndex = 2;
			textBoxLaborAmount.Text = "0.00";
			textBoxLaborAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLaborAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel28.AutoSize = true;
			mmLabel28.BackColor = System.Drawing.Color.Transparent;
			mmLabel28.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel28.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel28.IsFieldHeader = false;
			mmLabel28.IsRequired = false;
			mmLabel28.Location = new System.Drawing.Point(8, 60);
			mmLabel28.Name = "mmLabel28";
			mmLabel28.PenWidth = 1f;
			mmLabel28.ShowBorder = false;
			mmLabel28.Size = new System.Drawing.Size(38, 13);
			mmLabel28.TabIndex = 29;
			mmLabel28.Text = "Labor:";
			textBoxMiscellaneousAmount.AllowDecimal = true;
			textBoxMiscellaneousAmount.BackColor = System.Drawing.Color.White;
			textBoxMiscellaneousAmount.CustomReportFieldName = "";
			textBoxMiscellaneousAmount.CustomReportKey = "";
			textBoxMiscellaneousAmount.CustomReportValueType = 1;
			textBoxMiscellaneousAmount.IsComboTextBox = false;
			textBoxMiscellaneousAmount.IsModified = false;
			textBoxMiscellaneousAmount.Location = new System.Drawing.Point(114, 34);
			textBoxMiscellaneousAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxMiscellaneousAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxMiscellaneousAmount.Name = "textBoxMiscellaneousAmount";
			textBoxMiscellaneousAmount.NullText = "0";
			textBoxMiscellaneousAmount.Size = new System.Drawing.Size(110, 20);
			textBoxMiscellaneousAmount.TabIndex = 0;
			textBoxMiscellaneousAmount.Text = "0.00";
			textBoxMiscellaneousAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxMiscellaneousAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			ultraGroupBox4.Controls.Add(mmLabel17);
			ultraGroupBox4.Controls.Add(textBoxSumAdvanceBalance);
			ultraGroupBox4.Controls.Add(mmLabel19);
			ultraGroupBox4.Controls.Add(textBoxSumAdvanceReceived);
			ultraGroupBox4.Controls.Add(mmLabel22);
			ultraGroupBox4.Controls.Add(textBoxSumAdvanceApplied);
			ultraGroupBox4.Controls.Add(textBoxSumAdvance);
			ultraGroupBox4.Controls.Add(mmLabel21);
			ultraGroupBox4.Location = new System.Drawing.Point(10, 246);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(686, 79);
			ultraGroupBox4.TabIndex = 2;
			ultraGroupBox4.Text = "Advance Summary";
			mmLabel17.AutoSize = true;
			mmLabel17.BackColor = System.Drawing.Color.Transparent;
			mmLabel17.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel17.IsFieldHeader = false;
			mmLabel17.IsRequired = false;
			mmLabel17.Location = new System.Drawing.Point(442, 48);
			mmLabel17.Name = "mmLabel17";
			mmLabel17.PenWidth = 1f;
			mmLabel17.ShowBorder = false;
			mmLabel17.Size = new System.Drawing.Size(93, 13);
			mmLabel17.TabIndex = 32;
			mmLabel17.Text = "Advance Balance:";
			textBoxSumAdvanceBalance.AllowDecimal = true;
			textBoxSumAdvanceBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumAdvanceBalance.CustomReportFieldName = "";
			textBoxSumAdvanceBalance.CustomReportKey = "";
			textBoxSumAdvanceBalance.CustomReportValueType = 1;
			textBoxSumAdvanceBalance.IsComboTextBox = false;
			textBoxSumAdvanceBalance.IsModified = false;
			textBoxSumAdvanceBalance.Location = new System.Drawing.Point(546, 45);
			textBoxSumAdvanceBalance.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumAdvanceBalance.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumAdvanceBalance.Name = "textBoxSumAdvanceBalance";
			textBoxSumAdvanceBalance.NullText = "0";
			textBoxSumAdvanceBalance.ReadOnly = true;
			textBoxSumAdvanceBalance.Size = new System.Drawing.Size(118, 20);
			textBoxSumAdvanceBalance.TabIndex = 3;
			textBoxSumAdvanceBalance.Text = "0.00";
			textBoxSumAdvanceBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumAdvanceBalance.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel19.AutoSize = true;
			mmLabel19.BackColor = System.Drawing.Color.Transparent;
			mmLabel19.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel19.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel19.IsFieldHeader = false;
			mmLabel19.IsRequired = false;
			mmLabel19.Location = new System.Drawing.Point(8, 25);
			mmLabel19.Name = "mmLabel19";
			mmLabel19.PenWidth = 1f;
			mmLabel19.ShowBorder = false;
			mmLabel19.Size = new System.Drawing.Size(79, 13);
			mmLabel19.TabIndex = 27;
			mmLabel19.Text = "Proj. Advance:";
			textBoxSumAdvanceReceived.AllowDecimal = true;
			textBoxSumAdvanceReceived.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumAdvanceReceived.CustomReportFieldName = "";
			textBoxSumAdvanceReceived.CustomReportKey = "";
			textBoxSumAdvanceReceived.CustomReportValueType = 1;
			textBoxSumAdvanceReceived.IsComboTextBox = false;
			textBoxSumAdvanceReceived.IsModified = false;
			textBoxSumAdvanceReceived.Location = new System.Drawing.Point(114, 45);
			textBoxSumAdvanceReceived.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumAdvanceReceived.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumAdvanceReceived.Name = "textBoxSumAdvanceReceived";
			textBoxSumAdvanceReceived.NullText = "0";
			textBoxSumAdvanceReceived.ReadOnly = true;
			textBoxSumAdvanceReceived.Size = new System.Drawing.Size(110, 20);
			textBoxSumAdvanceReceived.TabIndex = 1;
			textBoxSumAdvanceReceived.Text = "0.00";
			textBoxSumAdvanceReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumAdvanceReceived.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel22.AutoSize = true;
			mmLabel22.BackColor = System.Drawing.Color.Transparent;
			mmLabel22.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel22.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel22.IsFieldHeader = false;
			mmLabel22.IsRequired = false;
			mmLabel22.Location = new System.Drawing.Point(8, 48);
			mmLabel22.Name = "mmLabel22";
			mmLabel22.PenWidth = 1f;
			mmLabel22.ShowBorder = false;
			mmLabel22.Size = new System.Drawing.Size(100, 13);
			mmLabel22.TabIndex = 29;
			mmLabel22.Text = "Advance Received:";
			textBoxSumAdvanceApplied.AllowDecimal = true;
			textBoxSumAdvanceApplied.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumAdvanceApplied.CustomReportFieldName = "";
			textBoxSumAdvanceApplied.CustomReportKey = "";
			textBoxSumAdvanceApplied.CustomReportValueType = 1;
			textBoxSumAdvanceApplied.IsComboTextBox = false;
			textBoxSumAdvanceApplied.IsModified = false;
			textBoxSumAdvanceApplied.Location = new System.Drawing.Point(320, 45);
			textBoxSumAdvanceApplied.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumAdvanceApplied.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumAdvanceApplied.Name = "textBoxSumAdvanceApplied";
			textBoxSumAdvanceApplied.NullText = "0";
			textBoxSumAdvanceApplied.ReadOnly = true;
			textBoxSumAdvanceApplied.Size = new System.Drawing.Size(118, 20);
			textBoxSumAdvanceApplied.TabIndex = 2;
			textBoxSumAdvanceApplied.Text = "0.00";
			textBoxSumAdvanceApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumAdvanceApplied.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxSumAdvance.AllowDecimal = true;
			textBoxSumAdvance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumAdvance.CustomReportFieldName = "";
			textBoxSumAdvance.CustomReportKey = "";
			textBoxSumAdvance.CustomReportValueType = 1;
			textBoxSumAdvance.IsComboTextBox = false;
			textBoxSumAdvance.IsModified = false;
			textBoxSumAdvance.Location = new System.Drawing.Point(114, 22);
			textBoxSumAdvance.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumAdvance.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumAdvance.Name = "textBoxSumAdvance";
			textBoxSumAdvance.NullText = "0";
			textBoxSumAdvance.ReadOnly = true;
			textBoxSumAdvance.Size = new System.Drawing.Size(110, 20);
			textBoxSumAdvance.TabIndex = 0;
			textBoxSumAdvance.Text = "0.00";
			textBoxSumAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumAdvance.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel21.AutoSize = true;
			mmLabel21.BackColor = System.Drawing.Color.Transparent;
			mmLabel21.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel21.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel21.IsFieldHeader = false;
			mmLabel21.IsRequired = false;
			mmLabel21.Location = new System.Drawing.Point(227, 48);
			mmLabel21.Name = "mmLabel21";
			mmLabel21.PenWidth = 1f;
			mmLabel21.ShowBorder = false;
			mmLabel21.Size = new System.Drawing.Size(91, 13);
			mmLabel21.TabIndex = 27;
			mmLabel21.Text = "Advance Applied:";
			ultraGroupBox3.Controls.Add(dateTimePickerRetDate);
			ultraGroupBox3.Controls.Add(mmLabel38);
			ultraGroupBox3.Controls.Add(label5);
			ultraGroupBox3.Controls.Add(textBoxSumRetentionDays);
			ultraGroupBox3.Controls.Add(mmLabel20);
			ultraGroupBox3.Controls.Add(textBoxSumRetentionBalance);
			ultraGroupBox3.Controls.Add(mmLabel23);
			ultraGroupBox3.Controls.Add(mmLabel15);
			ultraGroupBox3.Controls.Add(mmLabel16);
			ultraGroupBox3.Controls.Add(textBoxSumRetentionAmount);
			ultraGroupBox3.Controls.Add(mmLabel18);
			ultraGroupBox3.Controls.Add(textBoxSumRetentionBilled);
			ultraGroupBox3.Controls.Add(textBoxSumRetentionPercent);
			ultraGroupBox3.Location = new System.Drawing.Point(13, 160);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(686, 79);
			ultraGroupBox3.TabIndex = 1;
			ultraGroupBox3.Text = "Retention Summary";
			dateTimePickerRetDate.Checked = false;
			dateTimePickerRetDate.CustomFormat = " ";
			dateTimePickerRetDate.Enabled = false;
			dateTimePickerRetDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerRetDate.Location = new System.Drawing.Point(546, 20);
			dateTimePickerRetDate.Name = "dateTimePickerRetDate";
			dateTimePickerRetDate.ShowCheckBox = true;
			dateTimePickerRetDate.Size = new System.Drawing.Size(118, 20);
			dateTimePickerRetDate.TabIndex = 2;
			dateTimePickerRetDate.Value = new System.DateTime(0L);
			mmLabel38.AutoSize = true;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(446, 24);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(82, 13);
			mmLabel38.TabIndex = 159;
			mmLabel38.Text = "Retention Date:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(259, 24);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(85, 13);
			label5.TabIndex = 158;
			label5.Text = "Retension Days:";
			textBoxSumRetentionDays.AllowDecimal = false;
			textBoxSumRetentionDays.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumRetentionDays.CustomReportFieldName = "";
			textBoxSumRetentionDays.CustomReportKey = "";
			textBoxSumRetentionDays.CustomReportValueType = 1;
			textBoxSumRetentionDays.ForeColor = System.Drawing.Color.Black;
			textBoxSumRetentionDays.IsComboTextBox = false;
			textBoxSumRetentionDays.IsModified = false;
			textBoxSumRetentionDays.Location = new System.Drawing.Point(345, 20);
			textBoxSumRetentionDays.MaxValue = new decimal(new int[4]
			{
				365,
				0,
				0,
				0
			});
			textBoxSumRetentionDays.MinValue = new decimal(new int[4]);
			textBoxSumRetentionDays.Name = "textBoxSumRetentionDays";
			textBoxSumRetentionDays.NullText = "0";
			textBoxSumRetentionDays.ReadOnly = true;
			textBoxSumRetentionDays.Size = new System.Drawing.Size(90, 20);
			textBoxSumRetentionDays.TabIndex = 1;
			textBoxSumRetentionDays.Text = "0";
			mmLabel20.AutoSize = true;
			mmLabel20.BackColor = System.Drawing.Color.Transparent;
			mmLabel20.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel20.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel20.IsFieldHeader = false;
			mmLabel20.IsRequired = false;
			mmLabel20.Location = new System.Drawing.Point(446, 48);
			mmLabel20.Name = "mmLabel20";
			mmLabel20.PenWidth = 1f;
			mmLabel20.ShowBorder = false;
			mmLabel20.Size = new System.Drawing.Size(98, 13);
			mmLabel20.TabIndex = 32;
			mmLabel20.Text = "Retention Balance:";
			textBoxSumRetentionBalance.AllowDecimal = true;
			textBoxSumRetentionBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumRetentionBalance.CustomReportFieldName = "";
			textBoxSumRetentionBalance.CustomReportKey = "";
			textBoxSumRetentionBalance.CustomReportValueType = 1;
			textBoxSumRetentionBalance.IsComboTextBox = false;
			textBoxSumRetentionBalance.IsModified = false;
			textBoxSumRetentionBalance.Location = new System.Drawing.Point(546, 45);
			textBoxSumRetentionBalance.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumRetentionBalance.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumRetentionBalance.Name = "textBoxSumRetentionBalance";
			textBoxSumRetentionBalance.NullText = "0";
			textBoxSumRetentionBalance.ReadOnly = true;
			textBoxSumRetentionBalance.Size = new System.Drawing.Size(118, 20);
			textBoxSumRetentionBalance.TabIndex = 5;
			textBoxSumRetentionBalance.Text = "0.00";
			textBoxSumRetentionBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumRetentionBalance.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel23.AutoSize = true;
			mmLabel23.BackColor = System.Drawing.Color.Transparent;
			mmLabel23.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel23.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel23.IsFieldHeader = false;
			mmLabel23.IsRequired = false;
			mmLabel23.Location = new System.Drawing.Point(227, 25);
			mmLabel23.Name = "mmLabel23";
			mmLabel23.PenWidth = 1f;
			mmLabel23.ShowBorder = false;
			mmLabel23.Size = new System.Drawing.Size(18, 13);
			mmLabel23.TabIndex = 27;
			mmLabel23.Text = "%";
			mmLabel15.AutoSize = true;
			mmLabel15.BackColor = System.Drawing.Color.Transparent;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(8, 24);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(84, 13);
			mmLabel15.TabIndex = 27;
			mmLabel15.Text = "Proj. Retention:";
			mmLabel16.AutoSize = true;
			mmLabel16.BackColor = System.Drawing.Color.Transparent;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(227, 48);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(85, 13);
			mmLabel16.TabIndex = 27;
			mmLabel16.Text = "Retention Billed:";
			textBoxSumRetentionAmount.AllowDecimal = true;
			textBoxSumRetentionAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumRetentionAmount.CustomReportFieldName = "";
			textBoxSumRetentionAmount.CustomReportKey = "";
			textBoxSumRetentionAmount.CustomReportValueType = 1;
			textBoxSumRetentionAmount.IsComboTextBox = false;
			textBoxSumRetentionAmount.IsModified = false;
			textBoxSumRetentionAmount.Location = new System.Drawing.Point(114, 45);
			textBoxSumRetentionAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumRetentionAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumRetentionAmount.Name = "textBoxSumRetentionAmount";
			textBoxSumRetentionAmount.NullText = "0";
			textBoxSumRetentionAmount.ReadOnly = true;
			textBoxSumRetentionAmount.Size = new System.Drawing.Size(110, 20);
			textBoxSumRetentionAmount.TabIndex = 3;
			textBoxSumRetentionAmount.Text = "0.00";
			textBoxSumRetentionAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumRetentionAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel18.AutoSize = true;
			mmLabel18.BackColor = System.Drawing.Color.Transparent;
			mmLabel18.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel18.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel18.IsFieldHeader = false;
			mmLabel18.IsRequired = false;
			mmLabel18.Location = new System.Drawing.Point(8, 48);
			mmLabel18.Name = "mmLabel18";
			mmLabel18.PenWidth = 1f;
			mmLabel18.ShowBorder = false;
			mmLabel18.Size = new System.Drawing.Size(98, 13);
			mmLabel18.TabIndex = 29;
			mmLabel18.Text = "Retention Amount:";
			textBoxSumRetentionBilled.AllowDecimal = true;
			textBoxSumRetentionBilled.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumRetentionBilled.CustomReportFieldName = "";
			textBoxSumRetentionBilled.CustomReportKey = "";
			textBoxSumRetentionBilled.CustomReportValueType = 1;
			textBoxSumRetentionBilled.IsComboTextBox = false;
			textBoxSumRetentionBilled.IsModified = false;
			textBoxSumRetentionBilled.Location = new System.Drawing.Point(318, 45);
			textBoxSumRetentionBilled.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumRetentionBilled.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumRetentionBilled.Name = "textBoxSumRetentionBilled";
			textBoxSumRetentionBilled.NullText = "0";
			textBoxSumRetentionBilled.ReadOnly = true;
			textBoxSumRetentionBilled.Size = new System.Drawing.Size(118, 20);
			textBoxSumRetentionBilled.TabIndex = 4;
			textBoxSumRetentionBilled.Text = "0.00";
			textBoxSumRetentionBilled.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumRetentionBilled.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxSumRetentionPercent.AllowDecimal = true;
			textBoxSumRetentionPercent.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumRetentionPercent.CustomReportFieldName = "";
			textBoxSumRetentionPercent.CustomReportKey = "";
			textBoxSumRetentionPercent.CustomReportValueType = 1;
			textBoxSumRetentionPercent.IsComboTextBox = false;
			textBoxSumRetentionPercent.IsModified = false;
			textBoxSumRetentionPercent.Location = new System.Drawing.Point(114, 20);
			textBoxSumRetentionPercent.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumRetentionPercent.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumRetentionPercent.Name = "textBoxSumRetentionPercent";
			textBoxSumRetentionPercent.NullText = "0";
			textBoxSumRetentionPercent.ReadOnly = true;
			textBoxSumRetentionPercent.Size = new System.Drawing.Size(110, 20);
			textBoxSumRetentionPercent.TabIndex = 0;
			textBoxSumRetentionPercent.Text = "0.00";
			textBoxSumRetentionPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumRetentionPercent.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			ultraGroupBox2.Controls.Add(mmLabel6);
			ultraGroupBox2.Controls.Add(textBoxSumProfit);
			ultraGroupBox2.Controls.Add(mmLabel14);
			ultraGroupBox2.Controls.Add(textBoxSumBilled);
			ultraGroupBox2.Controls.Add(mmLabel7);
			ultraGroupBox2.Controls.Add(textBoxSumCost);
			ultraGroupBox2.Controls.Add(mmLabel8);
			ultraGroupBox2.Controls.Add(textBoxSumWIP);
			ultraGroupBox2.Controls.Add(mmLabel9);
			ultraGroupBox2.Controls.Add(textBoxSumExpense);
			ultraGroupBox2.Location = new System.Drawing.Point(10, 17);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(686, 137);
			ultraGroupBox2.TabIndex = 0;
			ultraGroupBox2.Text = "Project Summary";
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(9, 25);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(57, 13);
			mmLabel6.TabIndex = 27;
			mmLabel6.Text = "Expenses:";
			textBoxSumProfit.AllowDecimal = true;
			textBoxSumProfit.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumProfit.CustomReportFieldName = "";
			textBoxSumProfit.CustomReportKey = "";
			textBoxSumProfit.CustomReportValueType = 1;
			textBoxSumProfit.IsComboTextBox = false;
			textBoxSumProfit.IsModified = false;
			textBoxSumProfit.Location = new System.Drawing.Point(89, 103);
			textBoxSumProfit.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumProfit.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumProfit.Name = "textBoxSumProfit";
			textBoxSumProfit.NullText = "0";
			textBoxSumProfit.ReadOnly = true;
			textBoxSumProfit.Size = new System.Drawing.Size(110, 20);
			textBoxSumProfit.TabIndex = 3;
			textBoxSumProfit.Text = "0.00";
			textBoxSumProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumProfit.Value = new decimal(new int[4]
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
			mmLabel14.Location = new System.Drawing.Point(205, 25);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(94, 13);
			mmLabel14.TabIndex = 27;
			mmLabel14.Text = "Work In Progress:";
			textBoxSumBilled.AllowDecimal = true;
			textBoxSumBilled.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumBilled.CustomReportFieldName = "";
			textBoxSumBilled.CustomReportKey = "";
			textBoxSumBilled.CustomReportValueType = 1;
			textBoxSumBilled.IsComboTextBox = false;
			textBoxSumBilled.IsModified = false;
			textBoxSumBilled.Location = new System.Drawing.Point(89, 80);
			textBoxSumBilled.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumBilled.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumBilled.Name = "textBoxSumBilled";
			textBoxSumBilled.NullText = "0";
			textBoxSumBilled.ReadOnly = true;
			textBoxSumBilled.Size = new System.Drawing.Size(110, 20);
			textBoxSumBilled.TabIndex = 2;
			textBoxSumBilled.Text = "0.00";
			textBoxSumBilled.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumBilled.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(9, 83);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(74, 13);
			mmLabel7.TabIndex = 27;
			mmLabel7.Text = "Billed to Date:";
			textBoxSumCost.AllowDecimal = true;
			textBoxSumCost.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumCost.CustomReportFieldName = "";
			textBoxSumCost.CustomReportKey = "";
			textBoxSumCost.CustomReportValueType = 1;
			textBoxSumCost.IsComboTextBox = false;
			textBoxSumCost.IsModified = false;
			textBoxSumCost.Location = new System.Drawing.Point(89, 46);
			textBoxSumCost.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumCost.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumCost.Name = "textBoxSumCost";
			textBoxSumCost.NullText = "0";
			textBoxSumCost.ReadOnly = true;
			textBoxSumCost.Size = new System.Drawing.Size(110, 20);
			textBoxSumCost.TabIndex = 1;
			textBoxSumCost.Text = "0.00";
			textBoxSumCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumCost.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(9, 48);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(72, 13);
			mmLabel8.TabIndex = 29;
			mmLabel8.Text = "Cost to Date:";
			textBoxSumWIP.AllowDecimal = true;
			textBoxSumWIP.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumWIP.CustomReportFieldName = "";
			textBoxSumWIP.CustomReportKey = "";
			textBoxSumWIP.CustomReportValueType = 1;
			textBoxSumWIP.IsComboTextBox = false;
			textBoxSumWIP.IsModified = false;
			textBoxSumWIP.Location = new System.Drawing.Point(302, 22);
			textBoxSumWIP.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumWIP.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumWIP.Name = "textBoxSumWIP";
			textBoxSumWIP.NullText = "0";
			textBoxSumWIP.ReadOnly = true;
			textBoxSumWIP.Size = new System.Drawing.Size(118, 20);
			textBoxSumWIP.TabIndex = 4;
			textBoxSumWIP.Text = "0.00";
			textBoxSumWIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumWIP.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(9, 107);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(37, 13);
			mmLabel9.TabIndex = 29;
			mmLabel9.Text = "Profit:";
			textBoxSumExpense.AllowDecimal = true;
			textBoxSumExpense.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSumExpense.CustomReportFieldName = "";
			textBoxSumExpense.CustomReportKey = "";
			textBoxSumExpense.CustomReportValueType = 1;
			textBoxSumExpense.IsComboTextBox = false;
			textBoxSumExpense.IsModified = false;
			textBoxSumExpense.Location = new System.Drawing.Point(89, 22);
			textBoxSumExpense.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSumExpense.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSumExpense.Name = "textBoxSumExpense";
			textBoxSumExpense.NullText = "0";
			textBoxSumExpense.ReadOnly = true;
			textBoxSumExpense.Size = new System.Drawing.Size(110, 20);
			textBoxSumExpense.TabIndex = 0;
			textBoxSumExpense.Text = "0.00";
			textBoxSumExpense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSumExpense.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			tabPageNote.Controls.Add(textBoxNote);
			tabPageNote.Location = new System.Drawing.Point(-10000, -10000);
			tabPageNote.Name = "tabPageNote";
			tabPageNote.Size = new System.Drawing.Size(711, 559);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(5, 5);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(698, 426);
			textBoxNote.TabIndex = 45;
			tabPageUserDefined.Controls.Add(udfEntryGrid);
			tabPageUserDefined.Location = new System.Drawing.Point(-10000, -10000);
			tabPageUserDefined.Name = "tabPageUserDefined";
			tabPageUserDefined.Size = new System.Drawing.Size(711, 559);
			udfEntryGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			udfEntryGrid.Location = new System.Drawing.Point(-2, 0);
			udfEntryGrid.Name = "udfEntryGrid";
			udfEntryGrid.Size = new System.Drawing.Size(715, 477);
			udfEntryGrid.TabIndex = 2;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripButton1,
				toolStripButtonPreview,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(715, 31);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
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
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(28, 28);
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
			panelButtons.Location = new System.Drawing.Point(0, 587);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(715, 40);
			panelButtons.TabIndex = 8;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(715, 1);
			linePanelDown.TabIndex = 3;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 16;
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
			xpButton1.Location = new System.Drawing.Point(605, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 17;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 15;
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
			buttonSave.TabIndex = 14;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageSummary);
			ultraTabControl1.Controls.Add(tabPageUserDefined);
			ultraTabControl1.Controls.Add(tabPageNote);
			ultraTabControl1.Controls.Add(tabPageAccounts);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 45);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(715, 582);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 23;
			appearance76.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance76;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = ultraTabPageControl1;
			ultraTab2.Text = "&Vehicle";
			ultraTab2.Visible = false;
			ultraTab3.TabPage = tabPageAccounts;
			ultraTab3.Text = "&Accounts";
			ultraTab4.TabPage = tabPageSummary;
			ultraTab4.Text = "&Summary";
			ultraTab5.TabPage = tabPageNote;
			ultraTab5.Text = "Note";
			ultraTab6.TabPage = tabPageUserDefined;
			ultraTab6.Text = "&User Defined";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[6]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(711, 559);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 31);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(715, 14);
			panel1.TabIndex = 24;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 45);
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
			base.ClientSize = new System.Drawing.Size(715, 627);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "JobDetailsForm";
			Text = "Job/Project Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxJobType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManager).EndInit();
			panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCutomers).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox5).EndInit();
			ultraGroupBox5.ResumeLayout(false);
			ultraGroupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicle).EndInit();
			tabPageAccounts.ResumeLayout(false);
			tabPageAccounts.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectCostAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAdvanceAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRetentionAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTimesheetAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxIncomeAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxWIPAccount).EndInit();
			tabPageSummary.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox6).EndInit();
			ultraGroupBox6.ResumeLayout(false);
			ultraGroupBox6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			ultraGroupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			ultraGroupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			tabPageNote.ResumeLayout(false);
			tabPageNote.PerformLayout();
			tabPageUserDefined.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
