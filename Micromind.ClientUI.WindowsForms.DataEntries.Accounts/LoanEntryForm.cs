using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class LoanEntryForm : Form, IForm
	{
		private bool canEdit = true;

		private LoanEntryData currentData;

		private const string TABLENAME_CONST = "Loan_Entry";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool isLoading;

		private bool isVoid;

		private ScreenAccessRight screenRight;

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

		private FormManager formManager;

		private MMTextBox textBoxLoanAccountName;

		private MMTextBox textBoxReason;

		private MMLabel mmLabel1;

		private MMLabel mmLabel10;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel12;

		private MMSDateTimePicker dateTimePickerDate;

		private AmountTextBox textBoxLoanAmount;

		private MMLabel mmLabel8;

		private NumericUpDown numericUpDownInstallmentNum;

		private MMLabel mmLabel11;

		private MMLabel mmLabel14;

		private MMSDateTimePicker dateTimePickerDedStart;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private TextBox textBoxVoucherNumber;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator3;

		private XPButton buttonVoid;

		private Label labelVoided;

		private Panel panelDetails;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private UltraGroupBox ultraGroupBox1;

		private DataGridList dataGridList;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private MMLabel mmLabel3;

		private MMLabel mmLabel2;

		private MMTextBox textBoxLoanRepayAccountName;

		private AmountTextBox textBoxInterestRate;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMTextBox textBoxInterestAccount;

		private XPButton buttonCalculate;

		private AllAccountsComboBox comboBoxLoanRepayAccount;

		private AllAccountsComboBox comboBoxInterestAccount;

		private AllAccountsComboBox comboBoxLoanAccount;

		private ComboBox comboBoxloanTenure;

		private XPButton buttonEMICaculate;

		private MMLabel mmLabel4;

		private AmountTextBox textBoxMonthlyEMI;

		private Panel panel1;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5035;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

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
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					bool enabled = textBoxVoucherNumber.Enabled = true;
					sysDocComboBox.Enabled = enabled;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					bool enabled = textBoxVoucherNumber.Enabled = false;
					sysDocComboBox2.Enabled = enabled;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
					if (IsVoid)
					{
						buttonVoid.Enabled = false;
					}
					textBoxVoucherNumber.Enabled = false;
					if (!canEdit)
					{
						XPButton xPButton3 = buttonDelete;
						enabled = (buttonVoid.Enabled = false);
						xPButton3.Enabled = enabled;
					}
					else
					{
						if (!IsVoid)
						{
							buttonVoid.Enabled = true;
						}
						buttonDelete.Enabled = true;
					}
				}
				comboBoxLoanAccount.ReadOnly = !isNewRecord;
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
					buttonSave.Enabled = !value;
					labelVoided.Visible = value;
					if (value)
					{
						buttonVoid.Enabled = false;
					}
					else
					{
						buttonVoid.Text = UIMessages.Void;
					}
				}
			}
		}

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		public LoanEntryForm()
		{
			InitializeComponent();
			dataGridList.InitializeRow += dataGridList_InitializeRow;
			AddEvents();
			comboBoxloanTenure.SelectedIndex = 1;
		}

		private void AddEvents()
		{
			base.Load += EmployeeLoanForm_Load;
			textBoxLoanAmount.TextChanged += textBoxLoanAmount_TextChanged;
			numericUpDownInstallmentNum.ValueChanged += numericUpDownInstallmentNum_ValueChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
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

		private void numericUpDownInstallmentNum_ValueChanged(object sender, EventArgs e)
		{
			CalculateInstallmentAmount();
		}

		private void textBoxLoanAmount_TextChanged(object sender, EventArgs e)
		{
			CalculateInstallmentAmount();
		}

		private void CalculateInstallmentAmount()
		{
			try
			{
				if (!isLoading)
				{
					decimal result = default(decimal);
					decimal.TryParse(textBoxLoanAmount.Text, out result);
					int.Parse(numericUpDownInstallmentNum.Value.ToString());
				}
			}
			catch (Exception)
			{
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new LoanEntryData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.LoanEntryTable.Rows[0] : currentData.LoanEntryTable.NewRow();
				dataRow.BeginEdit();
				dataRow["LoanAccountID"] = comboBoxLoanAccount.SelectedID;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["LoanRepaymentAccountID"] = comboBoxLoanRepayAccount.SelectedID;
				dataRow["InterestAccountID"] = comboBoxInterestAccount.SelectedID;
				dataRow["LoanTermType"] = comboBoxloanTenure.SelectedIndex;
				dataRow["LoanDate"] = dateTimePickerDate.Value;
				dataRow["DedStartDate"] = dateTimePickerDedStart.Value;
				dataRow["LoanAmount"] = textBoxLoanAmount.Text;
				dataRow["InterestRate"] = textBoxInterestRate.Text;
				dataRow["InstallmentNumber"] = numericUpDownInstallmentNum.Text;
				dataRow["Description"] = textBoxReason.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["MonthlyEMI"] = textBoxMonthlyEMI.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.LoanEntryTable.Rows.Add(dataRow);
				}
				currentData.LoanEntryDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridList.Rows)
				{
					DataRow dataRow2 = currentData.LoanEntryDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["LoanSysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow2["LoanVoucherID"] = textBoxVoucherNumber.Text;
					if (row.Cells["Installment"].Value != null && row.Cells["Installment"].Value.ToString() != "")
					{
						dataRow2["Installment"] = row.Cells["Installment"].Value.ToString();
					}
					else
					{
						dataRow2["Installment"] = 0;
					}
					if (row.Cells["TransactionDate"].Value != null && row.Cells["TransactionDate"].Value.ToString() != "")
					{
						dataRow2["TransactionDate"] = row.Cells["TransactionDate"].Value.ToString();
					}
					if (row.Cells["Installment Amount"].Value != null && row.Cells["Installment Amount"].Value.ToString() != "")
					{
						dataRow2["InstallmentAmount"] = row.Cells["Installment Amount"].Value.ToString();
					}
					else
					{
						dataRow2["InstallmentAmount"] = 0;
					}
					if (row.Cells["Principle"].Value != null && row.Cells["Principle"].Value.ToString() != "")
					{
						dataRow2["Principle"] = row.Cells["Principle"].Value.ToString();
					}
					else
					{
						dataRow2["Principle"] = 0;
					}
					if (row.Cells["Interest"].Value != null && row.Cells["Interest"].Value.ToString() != "")
					{
						dataRow2["Interest"] = row.Cells["Interest"].Value.ToString();
					}
					else
					{
						dataRow2["Interest"] = 0;
					}
					if (row.Cells["OutStanding Payment"].Value != null && row.Cells["OutStanding Payment"].Value.ToString() != "")
					{
						dataRow2["OutStandingPayment"] = row.Cells["OutStanding Payment"].Value.ToString();
					}
					else
					{
						dataRow2["OutStandingPayment"] = 0;
					}
					dataRow2.EndEdit();
					currentData.LoanEntryDetailTable.Rows.Add(dataRow2);
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
			textBoxVoucherNumber.Focus();
		}

		public void LoadData(string sysDocID, string voucherID)
		{
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID);
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && CanClose())
				{
					currentData = Factory.LoanEntrySystem.GetLoanEntryByID(SystemDocID, voucherID);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxVoucherNumber.Focus();
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
				isLoading = true;
				DataRow dataRow = currentData.Tables["Loan_Entry"].Rows[0];
				textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
				comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
				comboBoxLoanAccount.LoadData();
				comboBoxLoanAccount.SelectedID = dataRow["LoanAccountID"].ToString();
				comboBoxLoanRepayAccount.SelectedID = dataRow["LoanRepaymentAccountID"].ToString();
				comboBoxInterestAccount.SelectedID = dataRow["InterestAccountID"].ToString();
				if (dataRow["MonthlyEMI"] != DBNull.Value)
				{
					textBoxMonthlyEMI.Text = decimal.Parse(dataRow["MonthlyEMI"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					textBoxMonthlyEMI.Text = decimal.Parse("0").ToString(Format.TotalAmountFormat);
				}
				textBoxLoanAccountName.Text = comboBoxLoanAccount.SelectedName;
				textBoxInterestAccount.Text = comboBoxInterestAccount.SelectedName;
				textBoxLoanRepayAccountName.Text = comboBoxLoanRepayAccount.SelectedName;
				if (dataRow["LoanTermType"] != DBNull.Value)
				{
					comboBoxloanTenure.SelectedIndex = int.Parse(dataRow["LoanTermType"].ToString());
				}
				else
				{
					comboBoxloanTenure.SelectedIndex = -1;
				}
				if (dataRow["LoanAmount"] != DBNull.Value)
				{
					textBoxLoanAmount.Text = decimal.Parse(dataRow["LoanAmount"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					textBoxLoanAmount.Text = 0.ToString(Format.TotalAmountFormat);
				}
				dateTimePickerDedStart.Value = DateTime.Parse(dataRow["DedStartDate"].ToString());
				textBoxNote.Text = dataRow["Note"].ToString();
				textBoxReason.Text = dataRow["Description"].ToString();
				dateTimePickerDate.Value = DateTime.Parse(dataRow["LoanDate"].ToString());
				textBoxInterestRate.Text = dataRow["InterestRate"].ToString();
				numericUpDownInstallmentNum.Text = dataRow["InstallmentNumber"].ToString();
				if (dataRow["IsVoid"] != DBNull.Value)
				{
					IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
				}
				else
				{
					IsVoid = false;
				}
				DataTable dataTable = dataGridList.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (currentData.Tables.Contains("Loan_Entry_Detail") && currentData.LoanEntryDetailTable.Rows.Count != 0)
				{
					foreach (DataRow row in currentData.LoanEntryDetailTable.Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["LoanSysDocID"] = row["LoanSysDocID"];
						dataRow3["LoanVoucherID"] = row["LoanVoucherID"];
						if (row["Installment"] != DBNull.Value)
						{
							dataRow3["Installment"] = row["Installment"];
						}
						if (row["TransactionDate"] != DBNull.Value)
						{
							dataRow3["TransactionDate"] = row["TransactionDate"];
						}
						if (row["InstallmentAmount"] != DBNull.Value)
						{
							dataRow3["Installment Amount"] = row["InstallmentAmount"];
						}
						if (row["Principle"] != DBNull.Value)
						{
							dataRow3["Principle"] = row["Principle"];
						}
						if (row["Interest"] != DBNull.Value)
						{
							dataRow3["Interest"] = row["Interest"];
						}
						if (row["OutStandingPayment"] != DBNull.Value)
						{
							dataRow3["OutStanding Payment"] = row["OutStandingPayment"];
						}
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					foreach (UltraGridColumn column in dataGridList.DisplayLayout.Bands[0].Columns)
					{
						column.CellActivation = Activation.AllowEdit;
					}
					isLoading = false;
				}
			}
		}

		private void SetupGrid()
		{
			try
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("LoanSysDocID");
				dataTable.Columns.Add("LoanVoucherID");
				dataTable.Columns.Add("Installment");
				dataTable.Columns.Add("TransactionDate", typeof(DateTime));
				dataTable.Columns.Add("Installment Amount", typeof(decimal));
				dataTable.Columns.Add("Principle", typeof(decimal));
				dataTable.Columns.Add("Interest", typeof(decimal));
				dataTable.Columns.Add("Outstanding Payment", typeof(decimal));
				dataGridList.DataSource = dataTable;
				dataGridList.DisplayLayout.Bands[0].Columns["Installment"].CellActivation = Activation.AllowEdit;
				dataGridList.DisplayLayout.Bands[0].Columns["TransactionDate"].CellActivation = Activation.AllowEdit;
				dataGridList.DisplayLayout.Bands[0].Columns["Installment Amount"].CellActivation = Activation.AllowEdit;
				dataGridList.DisplayLayout.Bands[0].Columns["Principle"].CellActivation = Activation.AllowEdit;
				dataGridList.DisplayLayout.Bands[0].Columns["Interest"].CellActivation = Activation.AllowEdit;
				dataGridList.DisplayLayout.Bands[0].Columns["Outstanding Payment"].CellActivation = Activation.AllowEdit;
				UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["LoanSysDocID"];
				bool hidden = dataGridList.DisplayLayout.Bands[0].Columns["LoanVoucherID"].Hidden = true;
				ultraGridColumn.Hidden = hidden;
				UltraGridColumn ultraGridColumn2 = dataGridList.DisplayLayout.Bands[0].Columns["Installment Amount"];
				UltraGridColumn ultraGridColumn3 = dataGridList.DisplayLayout.Bands[0].Columns["Principle"];
				UltraGridColumn ultraGridColumn4 = dataGridList.DisplayLayout.Bands[0].Columns["Interest"];
				string text2 = dataGridList.DisplayLayout.Bands[0].Columns["Outstanding Payment"].Format = "#,##0.00";
				string text4 = ultraGridColumn4.Format = text2;
				string text7 = ultraGridColumn2.Format = (ultraGridColumn3.Format = text4);
				AppearanceBase cellAppearance = dataGridList.DisplayLayout.Bands[0].Columns["Installment Amount"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridList.DisplayLayout.Bands[0].Columns["Principle"].CellAppearance;
				AppearanceBase cellAppearance3 = dataGridList.DisplayLayout.Bands[0].Columns["Interest"].CellAppearance;
				HAlign hAlign2 = dataGridList.DisplayLayout.Bands[0].Columns["Outstanding Payment"].CellAppearance.TextHAlign = HAlign.Right;
				HAlign hAlign4 = cellAppearance3.TextHAlign = hAlign2;
				HAlign hAlign7 = cellAppearance.TextHAlign = (cellAppearance2.TextHAlign = hAlign4);
				dataGridList.DisplayLayout.Bands[0].Columns.Add("Pay", "");
				dataGridList.DisplayLayout.Bands[0].Columns["Pay"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
				dataGridList.Rows.Band.Columns["Pay"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
				dataGridList.DisplayLayout.Bands[0].Columns["Pay"].MaxWidth = 50;
				dataGridList.DisplayLayout.Bands[0].Columns["Pay"].CellAppearance.TextHAlign = HAlign.Center;
			}
			catch (Exception e)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
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
					flag = Factory.LoanEntrySystem.CreateLoanEntry(currentData, isUpdate: false);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeLoan, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.LoanEntrySystem.CreateLoanEntry(currentData, isUpdate: true);
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
			if (isNewRecord && dateTimePickerDate.Value < DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
				return false;
			}
			if (isNewRecord && dateTimePickerDate.Value > DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Loan_Entry", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
				textBoxVoucherNumber.Focus();
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
			textBoxVoucherNumber.Clear();
			comboBoxLoanAccount.Clear();
			textBoxLoanAccountName.Clear();
			textBoxLoanRepayAccountName.Clear();
			textBoxInterestAccount.Clear();
			textBoxLoanAmount.ReadOnly = false;
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
			comboBoxloanTenure.SelectedIndex = 1;
			comboBoxLoanRepayAccount.Clear();
			comboBoxInterestAccount.Clear();
			textBoxMonthlyEMI.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxLoanAmount.Text = 0.ToString(Format.TotalAmountFormat);
			numericUpDownInstallmentNum.Value = 1m;
			dateTimePickerDedStart.Value = DateTime.Now;
			textBoxNote.Clear();
			textBoxReason.Clear();
			dateTimePickerDate.Value = DateTime.Today;
			textBoxInterestRate.Clear();
			IsVoid = false;
			canEdit = true;
			textBoxLoanAmount.ReadOnly = false;
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
			comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
			(dataGridList.DataSource as DataTable).Rows.Clear();
			formManager.ResetDirty();
			comboBoxLoanAccount.Focus();
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
				return Factory.LoanEntrySystem.DeleteLoanEntry(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Loan_Entry", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Loan_Entry", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Loan_Entry", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Loan_Entry", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Loan_Entry", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void EmployeeLoanForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridList.ApplyUIDesign();
				SetupGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.LoanEntry);
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

		private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				textBoxLoanAccountName.Text = comboBoxLoanAccount.SelectedName;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.LoanEntry);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
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
				return Factory.EmployeeLoanSystem.VoidLoan(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.LoanEntryListFormObj);
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

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
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
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet employeeLoanToPrint = Factory.EmployeeLoanSystem.GetEmployeeLoanToPrint(selectedID, text);
					if (employeeLoanToPrint == null || employeeLoanToPrint.Tables[0].Rows.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(employeeLoanToPrint, selectedID, "Loan Entry", SysDocTypes.LoanEntry, isPrint, showPrintDialog);
					}
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
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxLoanAccount.SelectedID);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = comboBoxSysDoc.SelectedID + textBoxVoucherNumber.Text.Trim();
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

		private void buttonCalculate_Click(object sender, EventArgs e)
		{
			try
			{
				GetLoanCalculation();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void GetLoanCalculation()
		{
			DataTable dataTable = dataGridList.DataSource as DataTable;
			dataTable.Rows.Clear();
			double num = 0.0;
			double num2 = 0.0;
			double result = 0.0;
			double result2 = 0.0;
			int num3 = 0;
			DateTime dateTime = dateTimePickerDate.Value;
			int num4 = int.Parse(numericUpDownInstallmentNum.Text);
			int selectedIndex = comboBoxloanTenure.SelectedIndex;
			double.TryParse(textBoxLoanAmount.Text, out result);
			double.TryParse(textBoxInterestRate.Text, out result2);
			result2 = result2 / 12.0 / 100.0;
			checked
			{
				if (selectedIndex > 0)
				{
					num4 *= 12;
				}
				double num5 = Math.Round(result * result2 * Math.Pow(1.0 + result2, num4) / (Math.Pow(1.0 + result2, num4) - 1.0));
				if (result > 0.0)
				{
					for (int i = 1; i <= num4; i++)
					{
						DataRow dataRow = dataTable.NewRow();
						dateTime = dateTime.AddMonths(1);
						num2 = result * result2;
						num2 = Math.Round(num2, 0, MidpointRounding.AwayFromZero);
						num3 = (int)Math.Floor(num5 - num2);
						num = result - (double)num3;
						dataRow["Installment"] = i;
						dataRow["TransactionDate"] = dateTime.ToString();
						dataRow["Installment Amount"] = num5;
						dataRow["Principle"] = num3;
						dataRow["Interest"] = num2;
						dataRow["OutStanding Payment"] = num;
						double.TryParse(dataRow["OutStanding Payment"].ToString(), out result);
						dataRow.EndEdit();
						dataTable.Rows.Add(dataRow);
					}
				}
				dataTable.AcceptChanges();
				formManager.IsForcedDirty = true;
			}
		}

		private void comboBoxInterestAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				textBoxInterestAccount.Text = comboBoxInterestAccount.SelectedName;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxLoanRepayAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				textBoxLoanRepayAccountName.Text = comboBoxLoanRepayAccount.SelectedName;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxLoanAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				textBoxLoanAccountName.Text = comboBoxLoanAccount.SelectedName;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridList_InitializeRow(object sender, InitializeRowEventArgs e)
		{
			if (!e.ReInitialize)
			{
				e.Row.Cells["Pay"].Value = "Pay";
			}
		}

		private void buttonEMICaculate_Click(object sender, EventArgs e)
		{
			try
			{
				double result = 0.0;
				double result2 = 0.0;
				int num = int.Parse(numericUpDownInstallmentNum.Text);
				int selectedIndex = comboBoxloanTenure.SelectedIndex;
				double.TryParse(textBoxLoanAmount.Text, out result);
				double.TryParse(textBoxInterestRate.Text, out result2);
				result2 = result2 / 12.0 / 100.0;
				if (selectedIndex > 0)
				{
					num = checked(num * 12);
				}
				double num2 = Math.Round(result * result2 * Math.Pow(1.0 + result2, num) / (Math.Pow(1.0 + result2, num) - 1.0));
				textBoxMonthlyEMI.Text = num2.ToString(Format.TotalAmountFormat);
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.LoanEntryForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelDetails = new System.Windows.Forms.Panel();
			panel1 = new System.Windows.Forms.Panel();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel12 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			buttonEMICaculate = new Micromind.UISupport.XPButton();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxMonthlyEMI = new Micromind.UISupport.AmountTextBox();
			comboBoxloanTenure = new System.Windows.Forms.ComboBox();
			comboBoxLoanRepayAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxInterestAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxLoanAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxLoanRepayAccountName = new Micromind.UISupport.MMTextBox();
			textBoxInterestRate = new Micromind.UISupport.AmountTextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			textBoxInterestAccount = new Micromind.UISupport.MMTextBox();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			numericUpDownInstallmentNum = new System.Windows.Forms.NumericUpDown();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxLoanAmount = new Micromind.UISupport.AmountTextBox();
			textBoxReason = new Micromind.UISupport.MMTextBox();
			dateTimePickerDedStart = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxLoanAccountName = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonCalculate = new Micromind.UISupport.XPButton();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			labelVoided = new System.Windows.Forms.Label();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			tabPageGeneral.SuspendLayout();
			panelDetails.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLoanRepayAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInterestAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLoanAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownInstallmentNum).BeginInit();
			tabPageDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(panelDetails);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(705, 389);
			panelDetails.Controls.Add(panel1);
			panelDetails.Controls.Add(ultraGroupBox1);
			panelDetails.Controls.Add(formManager);
			panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			panelDetails.Location = new System.Drawing.Point(0, 0);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(705, 389);
			panelDetails.TabIndex = 0;
			panel1.Controls.Add(dateTimePickerDate);
			panel1.Controls.Add(mmLabel12);
			panel1.Controls.Add(ultraFormattedLinkLabel5);
			panel1.Controls.Add(textBoxVoucherNumber);
			panel1.Controls.Add(comboBoxSysDoc);
			panel1.Controls.Add(ultraFormattedLinkLabel2);
			panel1.Location = new System.Drawing.Point(4, 1);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(689, 32);
			panel1.TabIndex = 123;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(563, 6);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(117, 20);
			dateTimePickerDate.TabIndex = 2;
			dateTimePickerDate.Value = new System.DateTime(2014, 6, 11, 16, 24, 17, 770);
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = true;
			mmLabel12.Location = new System.Drawing.Point(481, 9);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(70, 13);
			mmLabel12.TabIndex = 0;
			mmLabel12.Text = "Loan Date:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(7, 9);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 122;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			textBoxVoucherNumber.Location = new System.Drawing.Point(325, 6);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(142, 20);
			textBoxVoucherNumber.TabIndex = 1;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(109, 6);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(139, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance15.FontData.BoldAsString = "True";
			appearance15.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance15;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(258, 9);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(53, 15);
			ultraFormattedLinkLabel2.TabIndex = 121;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Number:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance16;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(mmLabel10);
			ultraGroupBox1.Controls.Add(buttonEMICaculate);
			ultraGroupBox1.Controls.Add(textBoxNote);
			ultraGroupBox1.Controls.Add(mmLabel4);
			ultraGroupBox1.Controls.Add(textBoxMonthlyEMI);
			ultraGroupBox1.Controls.Add(comboBoxloanTenure);
			ultraGroupBox1.Controls.Add(comboBoxLoanRepayAccount);
			ultraGroupBox1.Controls.Add(comboBoxInterestAccount);
			ultraGroupBox1.Controls.Add(comboBoxLoanAccount);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel3);
			ultraGroupBox1.Controls.Add(mmLabel3);
			ultraGroupBox1.Controls.Add(mmLabel2);
			ultraGroupBox1.Controls.Add(textBoxLoanRepayAccountName);
			ultraGroupBox1.Controls.Add(textBoxInterestRate);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel1);
			ultraGroupBox1.Controls.Add(mmLabel8);
			ultraGroupBox1.Controls.Add(mmLabel14);
			ultraGroupBox1.Controls.Add(textBoxInterestAccount);
			ultraGroupBox1.Controls.Add(mmLabel11);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel6);
			ultraGroupBox1.Controls.Add(numericUpDownInstallmentNum);
			ultraGroupBox1.Controls.Add(mmLabel1);
			ultraGroupBox1.Controls.Add(textBoxLoanAmount);
			ultraGroupBox1.Controls.Add(textBoxReason);
			ultraGroupBox1.Controls.Add(dateTimePickerDedStart);
			ultraGroupBox1.Controls.Add(textBoxLoanAccountName);
			ultraGroupBox1.Location = new System.Drawing.Point(4, 32);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(705, 353);
			ultraGroupBox1.TabIndex = 124;
			ultraGroupBox1.Text = "Loan Details";
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(8, 182);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(33, 13);
			mmLabel10.TabIndex = 9;
			mmLabel10.Text = "Note:";
			buttonEMICaculate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonEMICaculate.BackColor = System.Drawing.Color.DarkGray;
			buttonEMICaculate.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonEMICaculate.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonEMICaculate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonEMICaculate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonEMICaculate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonEMICaculate.Location = new System.Drawing.Point(504, 107);
			buttonEMICaculate.Name = "buttonEMICaculate";
			buttonEMICaculate.Size = new System.Drawing.Size(78, 24);
			buttonEMICaculate.TabIndex = 140;
			buttonEMICaculate.Text = "Calculate";
			buttonEMICaculate.UseVisualStyleBackColor = false;
			buttonEMICaculate.Click += new System.EventHandler(buttonEMICaculate_Click);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(158, 175);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(535, 174);
			textBoxNote.TabIndex = 13;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(304, 112);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(81, 13);
			mmLabel4.TabIndex = 139;
			mmLabel4.Text = "Monthly EMI:";
			textBoxMonthlyEMI.AllowDecimal = true;
			textBoxMonthlyEMI.CustomReportFieldName = "";
			textBoxMonthlyEMI.CustomReportKey = "";
			textBoxMonthlyEMI.CustomReportValueType = 1;
			textBoxMonthlyEMI.IsComboTextBox = false;
			textBoxMonthlyEMI.IsModified = false;
			textBoxMonthlyEMI.Location = new System.Drawing.Point(390, 109);
			textBoxMonthlyEMI.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxMonthlyEMI.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxMonthlyEMI.Name = "textBoxMonthlyEMI";
			textBoxMonthlyEMI.NullText = "0";
			textBoxMonthlyEMI.Size = new System.Drawing.Size(108, 20);
			textBoxMonthlyEMI.TabIndex = 10;
			textBoxMonthlyEMI.Text = "0.00";
			textBoxMonthlyEMI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxMonthlyEMI.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			comboBoxloanTenure.FormattingEnabled = true;
			comboBoxloanTenure.Items.AddRange(new object[2]
			{
				"Month",
				"Year"
			});
			comboBoxloanTenure.Location = new System.Drawing.Point(234, 108);
			comboBoxloanTenure.Name = "comboBoxloanTenure";
			comboBoxloanTenure.Size = new System.Drawing.Size(63, 21);
			comboBoxloanTenure.TabIndex = 9;
			comboBoxLoanRepayAccount.Assigned = false;
			comboBoxLoanRepayAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLoanRepayAccount.CustomReportFieldName = "";
			comboBoxLoanRepayAccount.CustomReportKey = "";
			comboBoxLoanRepayAccount.CustomReportValueType = 1;
			comboBoxLoanRepayAccount.DescriptionTextBox = null;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLoanRepayAccount.DisplayLayout.Appearance = appearance17;
			comboBoxLoanRepayAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLoanRepayAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLoanRepayAccount.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLoanRepayAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxLoanRepayAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLoanRepayAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxLoanRepayAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLoanRepayAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLoanRepayAccount.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLoanRepayAccount.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxLoanRepayAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLoanRepayAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLoanRepayAccount.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLoanRepayAccount.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxLoanRepayAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLoanRepayAccount.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLoanRepayAccount.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxLoanRepayAccount.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxLoanRepayAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLoanRepayAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxLoanRepayAccount.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxLoanRepayAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLoanRepayAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxLoanRepayAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLoanRepayAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLoanRepayAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLoanRepayAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLoanRepayAccount.Editable = true;
			comboBoxLoanRepayAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxLoanRepayAccount.FilterString = "";
			comboBoxLoanRepayAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxLoanRepayAccount.FilterSysDocID = "";
			comboBoxLoanRepayAccount.HasAllAccount = false;
			comboBoxLoanRepayAccount.HasCustom = false;
			comboBoxLoanRepayAccount.IsDataLoaded = false;
			comboBoxLoanRepayAccount.Location = new System.Drawing.Point(158, 63);
			comboBoxLoanRepayAccount.MaxDropDownItems = 12;
			comboBoxLoanRepayAccount.Name = "comboBoxLoanRepayAccount";
			comboBoxLoanRepayAccount.ShowInactiveItems = false;
			comboBoxLoanRepayAccount.ShowQuickAdd = true;
			comboBoxLoanRepayAccount.Size = new System.Drawing.Size(211, 20);
			comboBoxLoanRepayAccount.TabIndex = 4;
			comboBoxLoanRepayAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLoanRepayAccount.SelectedIndexChanged += new System.EventHandler(comboBoxLoanRepayAccount_SelectedIndexChanged);
			comboBoxInterestAccount.Assigned = false;
			comboBoxInterestAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxInterestAccount.CustomReportFieldName = "";
			comboBoxInterestAccount.CustomReportKey = "";
			comboBoxInterestAccount.CustomReportValueType = 1;
			comboBoxInterestAccount.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxInterestAccount.DisplayLayout.Appearance = appearance29;
			comboBoxInterestAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxInterestAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInterestAccount.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInterestAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxInterestAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInterestAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxInterestAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxInterestAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxInterestAccount.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxInterestAccount.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxInterestAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxInterestAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxInterestAccount.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxInterestAccount.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxInterestAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxInterestAccount.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInterestAccount.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxInterestAccount.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxInterestAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxInterestAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxInterestAccount.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxInterestAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxInterestAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxInterestAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxInterestAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxInterestAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxInterestAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxInterestAccount.Editable = true;
			comboBoxInterestAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxInterestAccount.FilterString = "";
			comboBoxInterestAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxInterestAccount.FilterSysDocID = "";
			comboBoxInterestAccount.HasAllAccount = false;
			comboBoxInterestAccount.HasCustom = false;
			comboBoxInterestAccount.IsDataLoaded = false;
			comboBoxInterestAccount.Location = new System.Drawing.Point(158, 41);
			comboBoxInterestAccount.MaxDropDownItems = 12;
			comboBoxInterestAccount.Name = "comboBoxInterestAccount";
			comboBoxInterestAccount.ShowInactiveItems = false;
			comboBoxInterestAccount.ShowQuickAdd = true;
			comboBoxInterestAccount.Size = new System.Drawing.Size(211, 20);
			comboBoxInterestAccount.TabIndex = 2;
			comboBoxInterestAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxInterestAccount.SelectedIndexChanged += new System.EventHandler(comboBoxInterestAccount_SelectedIndexChanged);
			comboBoxLoanAccount.Assigned = false;
			comboBoxLoanAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLoanAccount.CustomReportFieldName = "";
			comboBoxLoanAccount.CustomReportKey = "";
			comboBoxLoanAccount.CustomReportValueType = 1;
			comboBoxLoanAccount.DescriptionTextBox = null;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLoanAccount.DisplayLayout.Appearance = appearance41;
			comboBoxLoanAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLoanAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLoanAccount.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLoanAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxLoanAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLoanAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxLoanAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLoanAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLoanAccount.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLoanAccount.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxLoanAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLoanAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLoanAccount.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLoanAccount.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxLoanAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLoanAccount.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLoanAccount.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxLoanAccount.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxLoanAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLoanAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxLoanAccount.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxLoanAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLoanAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxLoanAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLoanAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLoanAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLoanAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLoanAccount.Editable = true;
			comboBoxLoanAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxLoanAccount.FilterString = "";
			comboBoxLoanAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxLoanAccount.FilterSysDocID = "";
			comboBoxLoanAccount.HasAllAccount = false;
			comboBoxLoanAccount.HasCustom = false;
			comboBoxLoanAccount.IsDataLoaded = false;
			comboBoxLoanAccount.Location = new System.Drawing.Point(158, 19);
			comboBoxLoanAccount.MaxDropDownItems = 12;
			comboBoxLoanAccount.Name = "comboBoxLoanAccount";
			comboBoxLoanAccount.ShowInactiveItems = false;
			comboBoxLoanAccount.ShowQuickAdd = true;
			comboBoxLoanAccount.Size = new System.Drawing.Size(211, 20);
			comboBoxLoanAccount.TabIndex = 0;
			comboBoxLoanAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLoanAccount.SelectedIndexChanged += new System.EventHandler(comboBoxLoanAccount_SelectedIndexChanged);
			appearance53.FontData.BoldAsString = "True";
			appearance53.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance53;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(7, 66);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(149, 15);
			ultraFormattedLinkLabel3.TabIndex = 136;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Loan Repayment Account:";
			appearance54.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance54;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(452, 90);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(16, 13);
			mmLabel3.TabIndex = 12;
			mmLabel3.Text = "%";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(304, 89);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(85, 13);
			mmLabel2.TabIndex = 10;
			mmLabel2.Text = "Interest Rate:";
			textBoxLoanRepayAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLoanRepayAccountName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLoanRepayAccountName.CustomReportFieldName = "";
			textBoxLoanRepayAccountName.CustomReportKey = "";
			textBoxLoanRepayAccountName.CustomReportValueType = 1;
			textBoxLoanRepayAccountName.IsComboTextBox = false;
			textBoxLoanRepayAccountName.IsModified = false;
			textBoxLoanRepayAccountName.Location = new System.Drawing.Point(375, 63);
			textBoxLoanRepayAccountName.MaxLength = 15;
			textBoxLoanRepayAccountName.Name = "textBoxLoanRepayAccountName";
			textBoxLoanRepayAccountName.ReadOnly = true;
			textBoxLoanRepayAccountName.Size = new System.Drawing.Size(318, 20);
			textBoxLoanRepayAccountName.TabIndex = 5;
			textBoxLoanRepayAccountName.TabStop = false;
			textBoxInterestRate.AllowDecimal = true;
			textBoxInterestRate.CustomReportFieldName = "";
			textBoxInterestRate.CustomReportKey = "";
			textBoxInterestRate.CustomReportValueType = 1;
			textBoxInterestRate.IsComboTextBox = false;
			textBoxInterestRate.IsModified = false;
			textBoxInterestRate.Location = new System.Drawing.Point(390, 87);
			textBoxInterestRate.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxInterestRate.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxInterestRate.Name = "textBoxInterestRate";
			textBoxInterestRate.NullText = "0";
			textBoxInterestRate.Size = new System.Drawing.Size(59, 20);
			textBoxInterestRate.TabIndex = 7;
			textBoxInterestRate.Text = "0.00";
			textBoxInterestRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInterestRate.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			appearance55.FontData.BoldAsString = "True";
			appearance55.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance55;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(6, 43);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel1.TabIndex = 133;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Interest Account:";
			appearance56.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance56;
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = true;
			mmLabel8.Location = new System.Drawing.Point(6, 89);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(85, 13);
			mmLabel8.TabIndex = 0;
			mmLabel8.Text = "Loan Amount:";
			mmLabel14.AutoSize = true;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = true;
			mmLabel14.Location = new System.Drawing.Point(6, 131);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(131, 13);
			mmLabel14.TabIndex = 0;
			mmLabel14.Text = "Deduction Start Date:";
			textBoxInterestAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxInterestAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxInterestAccount.CustomReportFieldName = "";
			textBoxInterestAccount.CustomReportKey = "";
			textBoxInterestAccount.CustomReportValueType = 1;
			textBoxInterestAccount.IsComboTextBox = false;
			textBoxInterestAccount.IsModified = false;
			textBoxInterestAccount.Location = new System.Drawing.Point(375, 41);
			textBoxInterestAccount.MaxLength = 15;
			textBoxInterestAccount.Name = "textBoxInterestAccount";
			textBoxInterestAccount.ReadOnly = true;
			textBoxInterestAccount.Size = new System.Drawing.Size(318, 20);
			textBoxInterestAccount.TabIndex = 3;
			textBoxInterestAccount.TabStop = false;
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = true;
			mmLabel11.Location = new System.Drawing.Point(6, 110);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(78, 13);
			mmLabel11.TabIndex = 0;
			mmLabel11.Text = "Installments:";
			appearance57.FontData.BoldAsString = "True";
			appearance57.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance57;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(7, 21);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(83, 15);
			ultraFormattedLinkLabel6.TabIndex = 130;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Loan Account:";
			appearance58.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance58;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			numericUpDownInstallmentNum.Location = new System.Drawing.Point(158, 108);
			numericUpDownInstallmentNum.Maximum = new decimal(new int[4]
			{
				99,
				0,
				0,
				0
			});
			numericUpDownInstallmentNum.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			numericUpDownInstallmentNum.Name = "numericUpDownInstallmentNum";
			numericUpDownInstallmentNum.Size = new System.Drawing.Size(71, 20);
			numericUpDownInstallmentNum.TabIndex = 8;
			numericUpDownInstallmentNum.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(7, 157);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(63, 13);
			mmLabel1.TabIndex = 9;
			mmLabel1.Text = "Description:";
			textBoxLoanAmount.AllowDecimal = true;
			textBoxLoanAmount.CustomReportFieldName = "";
			textBoxLoanAmount.CustomReportKey = "";
			textBoxLoanAmount.CustomReportValueType = 1;
			textBoxLoanAmount.IsComboTextBox = false;
			textBoxLoanAmount.IsModified = false;
			textBoxLoanAmount.Location = new System.Drawing.Point(158, 86);
			textBoxLoanAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxLoanAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxLoanAmount.Name = "textBoxLoanAmount";
			textBoxLoanAmount.NullText = "0";
			textBoxLoanAmount.Size = new System.Drawing.Size(139, 20);
			textBoxLoanAmount.TabIndex = 6;
			textBoxLoanAmount.Text = "0.00";
			textBoxLoanAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLoanAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxReason.BackColor = System.Drawing.Color.White;
			textBoxReason.CustomReportFieldName = "";
			textBoxReason.CustomReportKey = "";
			textBoxReason.CustomReportValueType = 1;
			textBoxReason.IsComboTextBox = false;
			textBoxReason.IsModified = false;
			textBoxReason.Location = new System.Drawing.Point(158, 153);
			textBoxReason.MaxLength = 255;
			textBoxReason.Name = "textBoxReason";
			textBoxReason.Size = new System.Drawing.Size(535, 20);
			textBoxReason.TabIndex = 12;
			dateTimePickerDedStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDedStart.Location = new System.Drawing.Point(158, 130);
			dateTimePickerDedStart.Name = "dateTimePickerDedStart";
			dateTimePickerDedStart.Size = new System.Drawing.Size(139, 20);
			dateTimePickerDedStart.TabIndex = 11;
			dateTimePickerDedStart.Value = new System.DateTime(2014, 6, 11, 16, 24, 17, 769);
			textBoxLoanAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLoanAccountName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxLoanAccountName.CustomReportFieldName = "";
			textBoxLoanAccountName.CustomReportKey = "";
			textBoxLoanAccountName.CustomReportValueType = 1;
			textBoxLoanAccountName.IsComboTextBox = false;
			textBoxLoanAccountName.IsModified = false;
			textBoxLoanAccountName.Location = new System.Drawing.Point(375, 19);
			textBoxLoanAccountName.MaxLength = 15;
			textBoxLoanAccountName.Name = "textBoxLoanAccountName";
			textBoxLoanAccountName.ReadOnly = true;
			textBoxLoanAccountName.Size = new System.Drawing.Size(318, 20);
			textBoxLoanAccountName.TabIndex = 1;
			textBoxLoanAccountName.TabStop = false;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			tabPageDetails.Controls.Add(buttonCalculate);
			tabPageDetails.Controls.Add(dataGridList);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(705, 389);
			buttonCalculate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCalculate.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonCalculate.BackColor = System.Drawing.Color.DarkGray;
			buttonCalculate.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCalculate.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCalculate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCalculate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			buttonCalculate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCalculate.Location = new System.Drawing.Point(604, 3);
			buttonCalculate.Name = "buttonCalculate";
			buttonCalculate.Size = new System.Drawing.Size(96, 24);
			buttonCalculate.TabIndex = 4;
			buttonCalculate.Text = "&Calculate";
			buttonCalculate.UseVisualStyleBackColor = false;
			buttonCalculate.Click += new System.EventHandler(buttonCalculate_Click);
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance59;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance60;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance61;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance62.BackColor2 = System.Drawing.SystemColors.Control;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance62;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance63;
			appearance64.BackColor = System.Drawing.SystemColors.Highlight;
			appearance64.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance64;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance65;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			appearance66.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance66;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance67.BackColor = System.Drawing.SystemColors.Control;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance67;
			appearance68.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance68;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance69;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance70;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(4, 33);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(696, 358);
			dataGridList.TabIndex = 1;
			dataGridList.Text = "dataGridList1";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButton1,
				toolStripButtonPreview,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator3,
				toolStripButtonDistribution,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(709, 31);
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
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
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
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 443);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(709, 40);
			panelButtons.TabIndex = 125;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(210, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 24);
			buttonVoid.TabIndex = 2;
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
			linePanelDown.Size = new System.Drawing.Size(709, 1);
			linePanelDown.TabIndex = 3;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(309, 8);
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
			xpButton1.Location = new System.Drawing.Point(599, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 4;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(111, 8);
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
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.WhiteSmoke;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(4, 414);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(705, 26);
			labelVoided.TabIndex = 123;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 31);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(709, 412);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 125;
			appearance71.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance71;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Installments";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(705, 389);
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(709, 483);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(603, 381);
			base.Name = "LoanEntryForm";
			Text = "Loan Entry ";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLoanRepayAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInterestAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLoanAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownInstallmentNum).EndInit();
			tabPageDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
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
