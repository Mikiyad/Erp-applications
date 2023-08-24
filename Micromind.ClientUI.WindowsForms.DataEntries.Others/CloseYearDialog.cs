using DevExpress.XtraWizard;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class CloseYearDialog : Form
	{
		private GLData closingTransactionData = new GLData();

		private DataSet lastClosingData;

		private IContainer components;

		private WizardControl wizardControl1;

		private WelcomeWizardPage welcomeWizardPage1;

		private WizardPage wizardPageIncomeExpense;

		private SplitContainer splitContainer1;

		private DataGridList dataGridIncome;

		private Label label5;

		private DataGridList dataGridExpense;

		private Label label6;

		private CompletionWizardPage completionWizardPage1;

		private WizardPage wizardPageSelectYear;

		private TextBox textBoxLastRemark;

		private Label label3;

		private TextBox textBoxLastDate;

		private Label label1;

		private Label label7;

		private TextBox textBoxPeriodTo;

		private TextBox textBoxPeriodFrom;

		private TextBox textBoxFiscalYearName;

		private FiscalYearComboBox comboBoxFiscalYear;

		private Label label2;

		private Label label4;

		private Panel panel1;

		private TextBox textBoxNetProfit;

		private Label label10;

		private TextBox textBoxTotalExpense;

		private TextBox textBoxTotalIncome;

		private Label label8;

		private Label label9;

		private WizardPage wizardPageClosingTransaction;

		private Panel panelDetails;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private Label label11;

		private TextBox textBoxDescription;

		private Label label13;

		private TextBox textBoxRef1;

		private TextBox textBoxVoucherNumber;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private TextBox textBoxREAccName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private AllAccountsComboBox comboBoxAccountRetainedEarning;

		private WizardPage wizardPageValidation;

		private DataGridList dataGridListTest;

		private TextBox textBoxTestResult;

		private Button buttonValidate;

		private ImageList imageList1;

		public CloseYearDialog()
		{
			InitializeComponent();
			base.Load += ClosePeriodDialog_Load;
			base.StartPosition = FormStartPosition.CenterParent;
			comboBoxFiscalYear.SelectedIndexChanged += comboBoxFiscalYear_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxAccountRetainedEarning.ShowQuickAdd = false;
			comboBoxAccountRetainedEarning.DescriptionTextBox = textBoxREAccName;
			textBoxREAccName.TabStop = false;
			dataGridListTest.AfterRowActivate += dataGridListTest_AfterRowActivate;
			SetupTestGrid();
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
		}

		private void SetupTestGrid()
		{
			dataGridListTest.ApplyUIDesign();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ID", typeof(byte));
			dataTable.Columns.Add("Status", typeof(byte));
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("Description");
			dataTable.Columns.Add("Result");
			dataTable.Columns.Add("ResultDesc");
			dataTable.Rows.Add(1, 0, "Pending Delivery Notes", "Invoice all pending delivery notes.", "", "");
			dataTable.Rows.Add(2, 0, "Pending Goods Receipt Notes (GRN)", "Invoice all pending GRNs.", "", "");
			dataTable.Rows.Add(3, 0, "Pending Inventory Transfer", "Accept all inventory transfers which are pending.", "", "");
			dataTable.Rows.Add(4, 0, "Negative inventory", "All inventory items must have positive or zero quantity.", "", "");
			dataTable.Rows.Add(5, 0, "Inventory Costing", "All inventory items must be costed correctly.", "", "");
			dataTable.Rows.Add(6, 0, "Trial Balance", "Trial balance and all transactions must be in balance.", "", "");
			dataGridListTest.DataSource = dataTable;
			ValueList valueList = new ValueList();
			valueList.ValueListItems.Add(0, " ");
			valueList.ValueListItems.Add(1, " ").Appearance.Image = imageList1.Images[0];
			ValueListItem valueListItem = valueList.ValueListItems.Add(2, " ");
			valueListItem.Appearance.Image = imageList1.Images[1];
			valueListItem.DisplayText = " ";
			dataGridListTest.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList;
			dataGridListTest.DisplayLayout.Bands[0].Columns["Status"].AllowRowSummaries = AllowRowSummaries.False;
			dataGridListTest.DisplayLayout.Bands[0].Columns["Status"].MaxWidth = 20;
			dataGridListTest.DisplayLayout.Bands[0].Columns["Status"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
			dataGridListTest.DisplayLayout.Bands[0].Columns["ID"].Hidden = true;
			dataGridListTest.DisplayLayout.Bands[0].Columns["ResultDesc"].Header.Caption = "Remarks";
		}

		private void ValidateDataAccuracy()
		{
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextDocumentNumber(comboBoxSysDoc.SelectedID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void comboBoxFiscalYear_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxFiscalYear.SelectedID == "")
			{
				textBoxPeriodFrom.Clear();
				textBoxPeriodTo.Clear();
			}
			else
			{
				textBoxPeriodFrom.Text = DateTime.Parse(comboBoxFiscalYear.SelectedRow.Cells["StartDate"].Value.ToString()).ToShortDateString();
				textBoxPeriodTo.Text = DateTime.Parse(comboBoxFiscalYear.SelectedRow.Cells["EndDate"].Value.ToString()).ToShortDateString();
			}
		}

		private void ClosePeriodDialog_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxSysDoc.FilterByType(SysDocTypes.GJournal);
				LoadLastClosing();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadLastClosing()
		{
			try
			{
				lastClosingData = Factory.CompanyInformationSystem.GetLastClosingPeriod();
				if (lastClosingData != null && lastClosingData.Tables[0].Rows.Count > 0)
				{
					textBoxLastDate.Text = DateTime.Parse(lastClosingData.Tables["Period"].Rows[0]["CloseDate"].ToString()).ToShortDateString();
					textBoxLastRemark.Text = lastClosingData.Tables["Period"].Rows[0]["Remarks"].ToString();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (true)
				{
					ErrorHelper.InformationMessage("Successfully closed the requested period.");
					Close();
				}
			}
			catch (CompanyException e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			catch (Exception e3)
			{
				ErrorHelper.ProcessError(e3);
			}
		}

		private void ClosePeriodDialog_Activated(object sender, EventArgs e)
		{
		}

		private void buttonUnlock_Click(object sender, EventArgs e)
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("Unlocking period will allow users to post transactions in the period.\nAre you sure you want to unlock the selected period lock?") != DialogResult.No)
				{
					int num = -1;
					if (lastClosingData != null)
					{
						num = int.Parse(lastClosingData.Tables[0].Rows[0]["PeriodID"].ToString());
						if (Factory.CompanyInformationSystem.UnlockPeriod(num))
						{
							LoadLastClosing();
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void wizardControl1_SelectedPageChanging(object sender, WizardPageChangingEventArgs e)
		{
			if (e.PrevPage.Name == "wizardPageSelectYear")
			{
				if (comboBoxFiscalYear.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Please select a fiscal year to close.");
					comboBoxFiscalYear.Focus();
					e.Cancel = true;
					return;
				}
				switch (Factory.FiscalYearSystem.CanCloseFiscalYear(comboBoxFiscalYear.SelectedID))
				{
				case -1:
					ErrorHelper.InformationMessage("Cannot close the selected fiscal year becuause there is a previous fiscal year still open. You must close the previous year first.");
					e.Cancel = true;
					break;
				case -2:
					ErrorHelper.InformationMessage("Cannot close the selected fiscal year becuause there are transactions which are happend before this fiscal year. You must define a fiscal year to include all these transactions and close that year first.");
					e.Cancel = true;
					break;
				default:
					wizardPageValidation.AllowNext = false;
					SetupTestGrid();
					LoadExpenseIncome();
					break;
				}
			}
			else if (e.PrevPage.Name == "wizardPageClosingTransaction")
			{
				if (SaveData())
				{
					completionWizardPage1.AllowBack = false;
				}
				else
				{
					e.Cancel = true;
				}
			}
		}

		private void LoadExpenseIncome()
		{
			try
			{
				dataGridExpense.ApplyUIDesign();
				dataGridIncome.ApplyUIDesign();
				DataSet closingIncomeExpenseList = Factory.JournalSystem.GetClosingIncomeExpenseList(comboBoxFiscalYear.SelectedID);
				DataView dataView = new DataView(closingIncomeExpenseList.Tables[0], "TypeID=3", "AccountID", DataViewRowState.CurrentRows);
				DataView dataView2 = new DataView(closingIncomeExpenseList.Tables[0], "TypeID=4", "AccountID", DataViewRowState.CurrentRows);
				decimal d = default(decimal);
				decimal d2 = default(decimal);
				foreach (DataRowView item in dataView)
				{
					if (item["Debit"] != DBNull.Value)
					{
						d -= decimal.Parse(item["Debit"].ToString());
					}
					if (item["Credit"] != DBNull.Value)
					{
						d += decimal.Parse(item["Credit"].ToString());
					}
				}
				foreach (DataRowView item2 in dataView2)
				{
					if (item2["Debit"] != DBNull.Value)
					{
						d2 += decimal.Parse(item2["Debit"].ToString());
					}
					if (item2["Credit"] != DBNull.Value)
					{
						d2 -= decimal.Parse(item2["Credit"].ToString());
					}
				}
				textBoxTotalIncome.Text = d.ToString(Format.TotalAmountFormat);
				textBoxTotalExpense.Text = d2.ToString(Format.TotalAmountFormat);
				textBoxNetProfit.Text = (d - d2).ToString(Format.TotalAmountFormat);
				dataGridIncome.DataSource = dataView;
				dataGridExpense.DataSource = dataView2;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TypeID"].Hidden = true;
				dataGridExpense.DisplayLayout.Bands[0].Columns["TypeID"].Hidden = true;
				dataGridIncome.DisplayLayout.Bands[0].Columns["AccountID"].Header.Caption = "Account Code";
				dataGridExpense.DisplayLayout.Bands[0].Columns["AccountID"].Header.Caption = "Account Code";
				dataGridIncome.DisplayLayout.Bands[0].Columns["AccountName"].Header.Caption = "Account Name";
				dataGridExpense.DisplayLayout.Bands[0].Columns["AccountName"].Header.Caption = "Account Name";
				dataGridIncome.DisplayLayout.Bands[0].Columns["Debit"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Debit"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Credit"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Credit"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Debit"].Format = Format.GridAmountFormat;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Debit"].Format = Format.GridAmountFormat;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Credit"].Format = Format.GridAmountFormat;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Credit"].Format = Format.GridAmountFormat;
				if (dataGridIncome.DisplayLayout.Bands[0].Summaries.Count == 0)
				{
					dataGridIncome.DisplayLayout.Bands[0].Summaries.Add("Debit", SummaryType.Sum, dataGridIncome.DisplayLayout.Bands[0].Columns["Debit"], SummaryPosition.UseSummaryPositionColumn).Appearance.TextHAlign = HAlign.Right;
					dataGridIncome.DisplayLayout.Bands[0].Summaries.Add("Credit", SummaryType.Sum, dataGridIncome.DisplayLayout.Bands[0].Columns["Credit"], SummaryPosition.UseSummaryPositionColumn).Appearance.TextHAlign = HAlign.Right;
					dataGridExpense.DisplayLayout.Bands[0].Summaries.Add("Debit", SummaryType.Sum, dataGridExpense.DisplayLayout.Bands[0].Columns["Debit"], SummaryPosition.UseSummaryPositionColumn).Appearance.TextHAlign = HAlign.Right;
					dataGridExpense.DisplayLayout.Bands[0].Summaries.Add("Credit", SummaryType.Sum, dataGridExpense.DisplayLayout.Bands[0].Columns["Credit"], SummaryPosition.UseSummaryPositionColumn).Appearance.TextHAlign = HAlign.Right;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private bool ValidateData()
		{
			if (comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (comboBoxAccountRetainedEarning.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select the Retained Earning Account.");
				comboBoxAccountRetainedEarning.Focus();
				return false;
			}
			return true;
		}

		private bool GetData()
		{
			closingTransactionData = new GLData();
			DataRow dataRow = closingTransactionData.JournalTable.NewRow();
			dataRow["JournalID"] = 0;
			dataRow["JournalDate"] = DateTime.Now;
			dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
			dataRow["VoucherID"] = textBoxVoucherNumber.Text;
			dataRow["Reference"] = textBoxRef1.Text;
			dataRow["Note"] = textBoxDescription.Text;
			dataRow.EndEdit();
			closingTransactionData.JournalTable.Rows.Add(dataRow);
			return true;
		}

		private bool SaveData()
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
				bool flag = Factory.FiscalYearSystem.CloseFiscalYear(comboBoxFiscalYear.SelectedID, closingTransactionData, comboBoxAccountRetainedEarning.SelectedID);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
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

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.GJournal);
		}

		private void buttonValidate_Click(object sender, EventArgs e)
		{
			try
			{
				Application.UseWaitCursor = true;
				DateTime dateTime = DateTime.Parse(textBoxPeriodTo.Text);
				dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 999);
				bool flag = true;
				(dataGridListTest.DisplayLayout.Bands[0].Columns["Status"].ValueList as ValueList).ValueListItems[0].Appearance.Image = imageList1.Images[2];
				foreach (UltraGridRow row in dataGridListTest.Rows)
				{
					row.Cells["Status"].Value = 0;
				}
				foreach (UltraGridRow row2 in dataGridListTest.Rows)
				{
					int num = int.Parse(row2.Cells["ID"].Value.ToString());
					int num2 = 0;
					switch (num)
					{
					case 1:
						num2 = Factory.FiscalYearSystem.GetPendingDeliveryNotesCount(dateTime);
						if (num2 > 0)
						{
							row2.Cells["Status"].Value = 2;
							row2.Cells["Result"].Value = "Failed";
							row2.Cells["ResultDesc"].Value = "There are " + num2 + " delivery notes in the selected fiscal year which are not invoiced. You must invoice all pending delivery notes before you can close the year.";
							flag = false;
						}
						else
						{
							row2.Cells["Status"].Value = 1;
							row2.Cells["Result"].Value = "Passed";
							row2.Cells["ResultDesc"].Value = "Passed";
						}
						break;
					case 2:
						num2 = Factory.FiscalYearSystem.GetPendingGRNCount(dateTime);
						if (num2 > 0)
						{
							row2.Cells["Status"].Value = 2;
							row2.Cells["Result"].Value = "Failed";
							row2.Cells["ResultDesc"].Value = "There are " + num2 + " Goods Receipt Notes (GRN) in the selected fiscal year which are not invoiced. You must invoice all pending GRNs before you can close the year.";
							flag = false;
						}
						else
						{
							row2.Cells["Status"].Value = 1;
							row2.Cells["Result"].Value = "Passed";
							row2.Cells["ResultDesc"].Value = "Passed";
						}
						break;
					case 3:
						num2 = Factory.FiscalYearSystem.GetPendingTransferCount(dateTime);
						if (num2 > 0)
						{
							row2.Cells["Status"].Value = 2;
							row2.Cells["Result"].Value = "Failed";
							row2.Cells["ResultDesc"].Value = "There are " + num2 + " inventory transfers in the selected fiscal year which are not yet accepted or rejected. You must receive all pending transfers before you can close the year.";
							flag = false;
						}
						else
						{
							row2.Cells["Status"].Value = 1;
							row2.Cells["Result"].Value = "Passed";
							row2.Cells["ResultDesc"].Value = "Passed";
						}
						break;
					case 4:
						num2 = Factory.FiscalYearSystem.GetNegativeStockCount(dateTime);
						if (num2 > 0)
						{
							row2.Cells["Status"].Value = 2;
							row2.Cells["Result"].Value = "Failed";
							row2.Cells["ResultDesc"].Value = "There are " + num2 + " items with negative quantity. All inventory items must have positive or zero quantity before you can close the year.";
							flag = false;
						}
						else
						{
							row2.Cells["Status"].Value = 1;
							row2.Cells["Result"].Value = "Passed";
							row2.Cells["ResultDesc"].Value = "Passed";
						}
						break;
					case 5:
						num2 = Factory.FiscalYearSystem.GetUncostedItemsCount(dateTime);
						if (num2 > 0)
						{
							row2.Cells["Status"].Value = 2;
							row2.Cells["Result"].Value = "Failed";
							row2.Cells["ResultDesc"].Value = "There are " + num2 + " items which are not costed correctly. All inventory items must be costed before proceeding. Please run 'Update Inventory Costing' utility to perform the inventory costing.";
							flag = false;
						}
						else
						{
							row2.Cells["Status"].Value = 1;
							row2.Cells["Result"].Value = "Passed";
							row2.Cells["ResultDesc"].Value = "Passed";
						}
						break;
					case 6:
					{
						bool flag2 = Factory.FiscalYearSystem.IsTBCorrect(dateTime);
						num2 = Factory.FiscalYearSystem.GetUnbalancedJournalsCount(dateTime);
						if (!flag || num2 > 0)
						{
							row2.Cells["Status"].Value = 2;
							row2.Cells["Result"].Value = "Failed";
							if (!flag2)
							{
								row2.Cells["ResultDesc"].Value = "Trial balance is not in balance. Please check all transactions and fix the transaction which has not equal debit and credit amount.";
							}
							else
							{
								row2.Cells["ResultDesc"].Value = "There are " + num2 + " transactions which have unbalanced debit and credit amount. Please make sure all transactions are in balance.";
							}
							flag = false;
						}
						else
						{
							row2.Cells["Status"].Value = 1;
							row2.Cells["Result"].Value = "Passed";
							row2.Cells["ResultDesc"].Value = "Passed";
						}
						break;
					}
					}
				}
				if (flag)
				{
					wizardPageValidation.AllowNext = true;
				}
				else
				{
					wizardPageValidation.AllowNext = false;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				Application.UseWaitCursor = false;
			}
		}

		private void dataGridListTest_AfterRowActivate(object sender, EventArgs e)
		{
			if (dataGridListTest.ActiveRow != null && dataGridListTest.ActiveRow.IsDataRow)
			{
				textBoxTestResult.Text = dataGridListTest.ActiveRow.Cells["ResultDesc"].Value.ToString();
			}
			else
			{
				textBoxTestResult.Clear();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.CloseYearDialog));
			wizardControl1 = new DevExpress.XtraWizard.WizardControl();
			welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
			wizardPageIncomeExpense = new DevExpress.XtraWizard.WizardPage();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			dataGridIncome = new Micromind.UISupport.DataGridList(components);
			label5 = new System.Windows.Forms.Label();
			dataGridExpense = new Micromind.UISupport.DataGridList(components);
			label6 = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			textBoxNetProfit = new System.Windows.Forms.TextBox();
			label10 = new System.Windows.Forms.Label();
			textBoxTotalExpense = new System.Windows.Forms.TextBox();
			textBoxTotalIncome = new System.Windows.Forms.TextBox();
			label8 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
			wizardPageSelectYear = new DevExpress.XtraWizard.WizardPage();
			label7 = new System.Windows.Forms.Label();
			textBoxPeriodTo = new System.Windows.Forms.TextBox();
			textBoxPeriodFrom = new System.Windows.Forms.TextBox();
			textBoxFiscalYearName = new System.Windows.Forms.TextBox();
			comboBoxFiscalYear = new Micromind.DataControls.FiscalYearComboBox();
			label2 = new System.Windows.Forms.Label();
			textBoxLastRemark = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			textBoxLastDate = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			wizardPageClosingTransaction = new DevExpress.XtraWizard.WizardPage();
			panelDetails = new System.Windows.Forms.Panel();
			textBoxREAccName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxAccountRetainedEarning = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label11 = new System.Windows.Forms.Label();
			textBoxDescription = new System.Windows.Forms.TextBox();
			label13 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			wizardPageValidation = new DevExpress.XtraWizard.WizardPage();
			buttonValidate = new System.Windows.Forms.Button();
			textBoxTestResult = new System.Windows.Forms.TextBox();
			dataGridListTest = new Micromind.UISupport.DataGridList(components);
			imageList1 = new System.Windows.Forms.ImageList(components);
			((System.ComponentModel.ISupportInitialize)wizardControl1).BeginInit();
			wizardControl1.SuspendLayout();
			wizardPageIncomeExpense.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridIncome).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).BeginInit();
			panel1.SuspendLayout();
			wizardPageSelectYear.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFiscalYear).BeginInit();
			wizardPageClosingTransaction.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccountRetainedEarning).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			wizardPageValidation.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridListTest).BeginInit();
			SuspendLayout();
			wizardControl1.Controls.Add(welcomeWizardPage1);
			wizardControl1.Controls.Add(wizardPageIncomeExpense);
			wizardControl1.Controls.Add(completionWizardPage1);
			wizardControl1.Controls.Add(wizardPageSelectYear);
			wizardControl1.Controls.Add(wizardPageClosingTransaction);
			wizardControl1.Controls.Add(wizardPageValidation);
			wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			wizardControl1.Location = new System.Drawing.Point(0, 0);
			wizardControl1.Name = "wizardControl1";
			wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[6]
			{
				welcomeWizardPage1,
				wizardPageSelectYear,
				wizardPageValidation,
				wizardPageIncomeExpense,
				wizardPageClosingTransaction,
				completionWizardPage1
			});
			wizardControl1.Size = new System.Drawing.Size(831, 541);
			wizardControl1.Text = "Year End Closing";
			wizardControl1.SelectedPageChanging += new DevExpress.XtraWizard.WizardPageChangingEventHandler(wizardControl1_SelectedPageChanging);
			welcomeWizardPage1.IntroductionText = "This wizard helps you to close the fiscal year. Follow the steps in the wizard to close your Fiscal Year.";
			welcomeWizardPage1.Name = "welcomeWizardPage1";
			welcomeWizardPage1.Size = new System.Drawing.Size(614, 408);
			welcomeWizardPage1.Text = "Year End Closing Wizard";
			wizardPageIncomeExpense.Controls.Add(splitContainer1);
			wizardPageIncomeExpense.Controls.Add(panel1);
			wizardPageIncomeExpense.DescriptionText = "Verify the income and expense accounts. Following accounts will be closed with selected Retained Earning account.";
			wizardPageIncomeExpense.Name = "wizardPageIncomeExpense";
			wizardPageIncomeExpense.Size = new System.Drawing.Size(799, 396);
			wizardPageIncomeExpense.Text = "Income and Expense Accounts";
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(dataGridIncome);
			splitContainer1.Panel1.Controls.Add(label5);
			splitContainer1.Panel2.Controls.Add(dataGridExpense);
			splitContainer1.Panel2.Controls.Add(label6);
			splitContainer1.Size = new System.Drawing.Size(799, 360);
			splitContainer1.SplitterDistance = 403;
			splitContainer1.TabIndex = 303;
			dataGridIncome.AllowUnfittedView = false;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridIncome.DisplayLayout.Appearance = appearance;
			dataGridIncome.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridIncome.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridIncome.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridIncome.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridIncome.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridIncome.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridIncome.DisplayLayout.MaxColScrollRegions = 1;
			dataGridIncome.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridIncome.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridIncome.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridIncome.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridIncome.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridIncome.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridIncome.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridIncome.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridIncome.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridIncome.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridIncome.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridIncome.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridIncome.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridIncome.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridIncome.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridIncome.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridIncome.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridIncome.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridIncome.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridIncome.Dock = System.Windows.Forms.DockStyle.Fill;
			dataGridIncome.Location = new System.Drawing.Point(0, 19);
			dataGridIncome.Name = "dataGridIncome";
			dataGridIncome.ShowDeleteMenu = false;
			dataGridIncome.ShowMinusInRed = true;
			dataGridIncome.ShowNewMenu = false;
			dataGridIncome.Size = new System.Drawing.Size(403, 341);
			dataGridIncome.TabIndex = 291;
			dataGridIncome.Text = "dataGridList1";
			label5.Dock = System.Windows.Forms.DockStyle.Top;
			label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(0, 0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(403, 19);
			label5.TabIndex = 293;
			label5.Text = "Income";
			dataGridExpense.AllowUnfittedView = false;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridExpense.DisplayLayout.Appearance = appearance13;
			dataGridExpense.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridExpense.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridExpense.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridExpense.DisplayLayout.MaxColScrollRegions = 1;
			dataGridExpense.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridExpense.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridExpense.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridExpense.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridExpense.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridExpense.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridExpense.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridExpense.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridExpense.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridExpense.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridExpense.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridExpense.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridExpense.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridExpense.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridExpense.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridExpense.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridExpense.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridExpense.Dock = System.Windows.Forms.DockStyle.Fill;
			dataGridExpense.Location = new System.Drawing.Point(0, 19);
			dataGridExpense.Name = "dataGridExpense";
			dataGridExpense.ShowDeleteMenu = false;
			dataGridExpense.ShowMinusInRed = true;
			dataGridExpense.ShowNewMenu = false;
			dataGridExpense.Size = new System.Drawing.Size(392, 341);
			dataGridExpense.TabIndex = 292;
			dataGridExpense.Text = "dataGridList1";
			label6.Dock = System.Windows.Forms.DockStyle.Top;
			label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(0, 0);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(392, 19);
			label6.TabIndex = 294;
			label6.Text = "Expense";
			panel1.Controls.Add(textBoxNetProfit);
			panel1.Controls.Add(label10);
			panel1.Controls.Add(textBoxTotalExpense);
			panel1.Controls.Add(textBoxTotalIncome);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(label9);
			panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel1.Location = new System.Drawing.Point(0, 360);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(799, 36);
			panel1.TabIndex = 304;
			textBoxNetProfit.Location = new System.Drawing.Point(552, 9);
			textBoxNetProfit.Name = "textBoxNetProfit";
			textBoxNetProfit.ReadOnly = true;
			textBoxNetProfit.Size = new System.Drawing.Size(124, 20);
			textBoxNetProfit.TabIndex = 319;
			textBoxNetProfit.TabStop = false;
			textBoxNetProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(471, 12);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(81, 13);
			label10.TabIndex = 318;
			label10.Text = "Net Profit/Loss:";
			textBoxTotalExpense.Location = new System.Drawing.Point(327, 9);
			textBoxTotalExpense.Name = "textBoxTotalExpense";
			textBoxTotalExpense.ReadOnly = true;
			textBoxTotalExpense.Size = new System.Drawing.Size(124, 20);
			textBoxTotalExpense.TabIndex = 316;
			textBoxTotalExpense.TabStop = false;
			textBoxTotalExpense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalIncome.Location = new System.Drawing.Point(104, 9);
			textBoxTotalIncome.Name = "textBoxTotalIncome";
			textBoxTotalIncome.ReadOnly = true;
			textBoxTotalIncome.Size = new System.Drawing.Size(124, 20);
			textBoxTotalIncome.TabIndex = 317;
			textBoxTotalIncome.TabStop = false;
			textBoxTotalIncome.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(246, 12);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(78, 13);
			label8.TabIndex = 314;
			label8.Text = "Total Expense:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(26, 12);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(72, 13);
			label9.TabIndex = 315;
			label9.Text = "Total Income:";
			completionWizardPage1.Name = "completionWizardPage1";
			completionWizardPage1.Size = new System.Drawing.Size(614, 408);
			wizardPageSelectYear.Controls.Add(label7);
			wizardPageSelectYear.Controls.Add(textBoxPeriodTo);
			wizardPageSelectYear.Controls.Add(textBoxPeriodFrom);
			wizardPageSelectYear.Controls.Add(textBoxFiscalYearName);
			wizardPageSelectYear.Controls.Add(comboBoxFiscalYear);
			wizardPageSelectYear.Controls.Add(label2);
			wizardPageSelectYear.Controls.Add(textBoxLastRemark);
			wizardPageSelectYear.Controls.Add(label4);
			wizardPageSelectYear.Controls.Add(label3);
			wizardPageSelectYear.Controls.Add(textBoxLastDate);
			wizardPageSelectYear.Controls.Add(label1);
			wizardPageSelectYear.DescriptionText = "Select the  year you would like to close";
			wizardPageSelectYear.Name = "wizardPageSelectYear";
			wizardPageSelectYear.Size = new System.Drawing.Size(799, 396);
			wizardPageSelectYear.Text = "Select Closing Year";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(12, 136);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(62, 13);
			label7.TabIndex = 314;
			label7.Text = "Fiscal Year:";
			textBoxPeriodTo.Location = new System.Drawing.Point(270, 159);
			textBoxPeriodTo.Name = "textBoxPeriodTo";
			textBoxPeriodTo.ReadOnly = true;
			textBoxPeriodTo.Size = new System.Drawing.Size(124, 20);
			textBoxPeriodTo.TabIndex = 313;
			textBoxPeriodFrom.Location = new System.Drawing.Point(111, 160);
			textBoxPeriodFrom.Name = "textBoxPeriodFrom";
			textBoxPeriodFrom.ReadOnly = true;
			textBoxPeriodFrom.Size = new System.Drawing.Size(122, 20);
			textBoxPeriodFrom.TabIndex = 313;
			textBoxFiscalYearName.Location = new System.Drawing.Point(236, 134);
			textBoxFiscalYearName.MaxLength = 15;
			textBoxFiscalYearName.Name = "textBoxFiscalYearName";
			textBoxFiscalYearName.ReadOnly = true;
			textBoxFiscalYearName.Size = new System.Drawing.Size(354, 20);
			textBoxFiscalYearName.TabIndex = 312;
			comboBoxFiscalYear.Assigned = false;
			comboBoxFiscalYear.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFiscalYear.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFiscalYear.CustomReportFieldName = "";
			comboBoxFiscalYear.CustomReportKey = "";
			comboBoxFiscalYear.CustomReportValueType = 1;
			comboBoxFiscalYear.DescriptionTextBox = textBoxFiscalYearName;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFiscalYear.DisplayLayout.Appearance = appearance25;
			comboBoxFiscalYear.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFiscalYear.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFiscalYear.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFiscalYear.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxFiscalYear.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFiscalYear.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxFiscalYear.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFiscalYear.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFiscalYear.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFiscalYear.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxFiscalYear.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFiscalYear.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFiscalYear.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFiscalYear.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxFiscalYear.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFiscalYear.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFiscalYear.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxFiscalYear.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxFiscalYear.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFiscalYear.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxFiscalYear.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxFiscalYear.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFiscalYear.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxFiscalYear.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFiscalYear.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFiscalYear.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFiscalYear.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFiscalYear.Editable = true;
			comboBoxFiscalYear.FilterString = "";
			comboBoxFiscalYear.HasAllAccount = false;
			comboBoxFiscalYear.HasCustom = false;
			comboBoxFiscalYear.IsDataLoaded = false;
			comboBoxFiscalYear.Location = new System.Drawing.Point(111, 134);
			comboBoxFiscalYear.MaxDropDownItems = 12;
			comboBoxFiscalYear.Name = "comboBoxFiscalYear";
			comboBoxFiscalYear.ShowInactiveItems = false;
			comboBoxFiscalYear.ShowQuickAdd = true;
			comboBoxFiscalYear.Size = new System.Drawing.Size(122, 20);
			comboBoxFiscalYear.TabIndex = 311;
			comboBoxFiscalYear.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 106);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(223, 13);
			label2.TabIndex = 310;
			label2.Text = "Select the Fiscal Year you would like to close:";
			textBoxLastRemark.Location = new System.Drawing.Point(111, 37);
			textBoxLastRemark.MaxLength = 15;
			textBoxLastRemark.Name = "textBoxLastRemark";
			textBoxLastRemark.ReadOnly = true;
			textBoxLastRemark.Size = new System.Drawing.Size(479, 20);
			textBoxLastRemark.TabIndex = 309;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(241, 161);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(23, 13);
			label4.TabIndex = 307;
			label4.Text = "To:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 163);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 307;
			label3.Text = "From:";
			textBoxLastDate.Location = new System.Drawing.Point(111, 15);
			textBoxLastDate.Name = "textBoxLastDate";
			textBoxLastDate.ReadOnly = true;
			textBoxLastDate.Size = new System.Drawing.Size(152, 20);
			textBoxLastDate.TabIndex = 303;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 17);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(93, 13);
			label1.TabIndex = 305;
			label1.Text = "Last Closing Date:";
			wizardPageClosingTransaction.Controls.Add(panelDetails);
			wizardPageClosingTransaction.DescriptionText = "Enter the closing transaction details.";
			wizardPageClosingTransaction.Name = "wizardPageClosingTransaction";
			wizardPageClosingTransaction.Size = new System.Drawing.Size(799, 396);
			wizardPageClosingTransaction.Text = "Closing Entry Transaction";
			panelDetails.Controls.Add(textBoxREAccName);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(comboBoxAccountRetainedEarning);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(label11);
			panelDetails.Controls.Add(textBoxDescription);
			panelDetails.Controls.Add(label13);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Location = new System.Drawing.Point(3, 9);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(774, 131);
			panelDetails.TabIndex = 0;
			textBoxREAccName.Location = new System.Drawing.Point(345, 31);
			textBoxREAccName.MaxLength = 15;
			textBoxREAccName.Name = "textBoxREAccName";
			textBoxREAccName.ReadOnly = true;
			textBoxREAccName.Size = new System.Drawing.Size(358, 20);
			textBoxREAccName.TabIndex = 3;
			appearance37.FontData.BoldAsString = "True";
			appearance37.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance37;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(10, 32);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(126, 15);
			ultraFormattedLinkLabel1.TabIndex = 120;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Retained Earning Acc:";
			appearance38.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance38;
			comboBoxAccountRetainedEarning.Assigned = false;
			comboBoxAccountRetainedEarning.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAccountRetainedEarning.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccountRetainedEarning.CustomReportFieldName = "";
			comboBoxAccountRetainedEarning.CustomReportKey = "";
			comboBoxAccountRetainedEarning.CustomReportValueType = 1;
			comboBoxAccountRetainedEarning.DescriptionTextBox = null;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccountRetainedEarning.DisplayLayout.Appearance = appearance39;
			comboBoxAccountRetainedEarning.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccountRetainedEarning.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccountRetainedEarning.DisplayLayout.GroupByBox.Appearance = appearance40;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccountRetainedEarning.DisplayLayout.GroupByBox.BandLabelAppearance = appearance41;
			comboBoxAccountRetainedEarning.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance42.BackColor2 = System.Drawing.SystemColors.Control;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccountRetainedEarning.DisplayLayout.GroupByBox.PromptAppearance = appearance42;
			comboBoxAccountRetainedEarning.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccountRetainedEarning.DisplayLayout.MaxRowScrollRegions = 1;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.ActiveCellAppearance = appearance43;
			appearance44.BackColor = System.Drawing.SystemColors.Highlight;
			appearance44.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.ActiveRowAppearance = appearance44;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.CardAreaAppearance = appearance45;
			appearance46.BorderColor = System.Drawing.Color.Silver;
			appearance46.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.CellAppearance = appearance46;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.CellPadding = 0;
			appearance47.BackColor = System.Drawing.SystemColors.Control;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.GroupByRowAppearance = appearance47;
			appearance48.TextHAlignAsString = "Left";
			comboBoxAccountRetainedEarning.DisplayLayout.Override.HeaderAppearance = appearance48;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.RowAppearance = appearance49;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccountRetainedEarning.DisplayLayout.Override.TemplateAddRowAppearance = appearance50;
			comboBoxAccountRetainedEarning.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAccountRetainedEarning.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAccountRetainedEarning.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAccountRetainedEarning.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAccountRetainedEarning.Editable = true;
			comboBoxAccountRetainedEarning.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAccountRetainedEarning.FilterString = "";
			comboBoxAccountRetainedEarning.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAccountRetainedEarning.FilterSysDocID = "";
			comboBoxAccountRetainedEarning.HasAllAccount = false;
			comboBoxAccountRetainedEarning.HasCustom = false;
			comboBoxAccountRetainedEarning.IsDataLoaded = false;
			comboBoxAccountRetainedEarning.Location = new System.Drawing.Point(145, 31);
			comboBoxAccountRetainedEarning.MaxDropDownItems = 12;
			comboBoxAccountRetainedEarning.Name = "comboBoxAccountRetainedEarning";
			comboBoxAccountRetainedEarning.ShowInactiveItems = false;
			comboBoxAccountRetainedEarning.ShowQuickAdd = true;
			comboBoxAccountRetainedEarning.Size = new System.Drawing.Size(198, 20);
			comboBoxAccountRetainedEarning.TabIndex = 2;
			comboBoxAccountRetainedEarning.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance51.FontData.BoldAsString = "True";
			appearance51.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance51;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(10, 8);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 118;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance52.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance52;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance53;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(145, 7);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(140, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance65.FontData.BoldAsString = "True";
			appearance65.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance65;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(291, 10);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 110;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance66.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance66;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(7, 79);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(33, 13);
			label11.TabIndex = 20;
			label11.Text = "Note:";
			textBoxDescription.Location = new System.Drawing.Point(145, 77);
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.Size = new System.Drawing.Size(558, 20);
			textBoxDescription.TabIndex = 5;
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(7, 59);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(60, 13);
			label13.TabIndex = 20;
			label13.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(145, 54);
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.ReadOnly = true;
			textBoxRef1.Size = new System.Drawing.Size(140, 20);
			textBoxRef1.TabIndex = 4;
			textBoxRef1.Text = "SYS_YEND";
			textBoxVoucherNumber.Location = new System.Drawing.Point(397, 7);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 20);
			textBoxVoucherNumber.TabIndex = 1;
			wizardPageValidation.Controls.Add(buttonValidate);
			wizardPageValidation.Controls.Add(textBoxTestResult);
			wizardPageValidation.Controls.Add(dataGridListTest);
			wizardPageValidation.DescriptionText = "This section will validate the data for the closing period. All conditions must pass before proceeding to year closing.";
			wizardPageValidation.Name = "wizardPageValidation";
			wizardPageValidation.Size = new System.Drawing.Size(799, 396);
			wizardPageValidation.Text = "Data Validation";
			buttonValidate.Location = new System.Drawing.Point(14, 358);
			buttonValidate.Name = "buttonValidate";
			buttonValidate.Size = new System.Drawing.Size(124, 23);
			buttonValidate.TabIndex = 2;
			buttonValidate.Text = "Validate Data Accuracy";
			buttonValidate.UseVisualStyleBackColor = true;
			buttonValidate.Click += new System.EventHandler(buttonValidate_Click);
			textBoxTestResult.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxTestResult.Location = new System.Drawing.Point(14, 270);
			textBoxTestResult.Multiline = true;
			textBoxTestResult.Name = "textBoxTestResult";
			textBoxTestResult.ReadOnly = true;
			textBoxTestResult.Size = new System.Drawing.Size(770, 73);
			textBoxTestResult.TabIndex = 1;
			dataGridListTest.AllowUnfittedView = false;
			dataGridListTest.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridListTest.DisplayLayout.Appearance = appearance67;
			dataGridListTest.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridListTest.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance68.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance68.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListTest.DisplayLayout.GroupByBox.Appearance = appearance68;
			appearance69.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListTest.DisplayLayout.GroupByBox.BandLabelAppearance = appearance69;
			dataGridListTest.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance70.BackColor2 = System.Drawing.SystemColors.Control;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance70.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListTest.DisplayLayout.GroupByBox.PromptAppearance = appearance70;
			dataGridListTest.DisplayLayout.MaxColScrollRegions = 1;
			dataGridListTest.DisplayLayout.MaxRowScrollRegions = 1;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridListTest.DisplayLayout.Override.ActiveCellAppearance = appearance71;
			appearance72.BackColor = System.Drawing.SystemColors.Highlight;
			appearance72.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridListTest.DisplayLayout.Override.ActiveRowAppearance = appearance72;
			dataGridListTest.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridListTest.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			dataGridListTest.DisplayLayout.Override.CardAreaAppearance = appearance73;
			appearance74.BorderColor = System.Drawing.Color.Silver;
			appearance74.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridListTest.DisplayLayout.Override.CellAppearance = appearance74;
			dataGridListTest.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridListTest.DisplayLayout.Override.CellPadding = 0;
			appearance75.BackColor = System.Drawing.SystemColors.Control;
			appearance75.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance75.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance75.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListTest.DisplayLayout.Override.GroupByRowAppearance = appearance75;
			appearance76.TextHAlignAsString = "Left";
			dataGridListTest.DisplayLayout.Override.HeaderAppearance = appearance76;
			dataGridListTest.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridListTest.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.BorderColor = System.Drawing.Color.Silver;
			dataGridListTest.DisplayLayout.Override.RowAppearance = appearance77;
			dataGridListTest.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridListTest.DisplayLayout.Override.TemplateAddRowAppearance = appearance78;
			dataGridListTest.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridListTest.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridListTest.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridListTest.Location = new System.Drawing.Point(14, 19);
			dataGridListTest.Name = "dataGridListTest";
			dataGridListTest.ShowDeleteMenu = false;
			dataGridListTest.ShowMinusInRed = true;
			dataGridListTest.ShowNewMenu = false;
			dataGridListTest.Size = new System.Drawing.Size(770, 245);
			dataGridListTest.TabIndex = 0;
			dataGridListTest.Text = "dataGridList1";
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "pass");
			imageList1.Images.SetKeyName(1, "Fail");
			imageList1.Images.SetKeyName(2, "wait-1.png");
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(831, 541);
			base.Controls.Add(wizardControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MinimizeBox = false;
			base.Name = "CloseYearDialog";
			Text = "Year End Closing";
			base.Activated += new System.EventHandler(ClosePeriodDialog_Activated);
			((System.ComponentModel.ISupportInitialize)wizardControl1).EndInit();
			wizardControl1.ResumeLayout(false);
			wizardPageIncomeExpense.ResumeLayout(false);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridIncome).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			wizardPageSelectYear.ResumeLayout(false);
			wizardPageSelectYear.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFiscalYear).EndInit();
			wizardPageClosingTransaction.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAccountRetainedEarning).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			wizardPageValidation.ResumeLayout(false);
			wizardPageValidation.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridListTest).EndInit();
			ResumeLayout(false);
		}
	}
}
