using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class EmployeeLoanForm : Form, IForm
	{
		private bool canEdit = true;

		private EmployeeLoanData currentData;

		private const string TABLENAME_CONST = "Employee_Loan";

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

		private MMTextBox textBoxFromLocation;

		private MMTextBox textBoxFromDivision;

		private MMLabel mmLabel4;

		private MMTextBox textBoxFromDepartment;

		private FormManager formManager;

		private EmployeeComboBox comboBoxEmployee;

		private MMTextBox textBoxEmployeeName;

		private MMLabel mmLabel3;

		private MMLabel mmLabel5;

		private MMTextBox textBoxReason;

		private MMLabel mmLabel1;

		private MMLabel mmLabel9;

		private MMTextBox textBoxReference;

		private MMLabel mmLabel10;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel12;

		private MMSDateTimePicker dateTimePickerDate;

		private MMLabel mmLabel6;

		private MMTextBox textBoxDesignation;

		private MMLabel mmLabel7;

		private EmployeeLoanTypeComboBox comboBoxLoanType;

		private AmountTextBox textBoxLoanAmount;

		private MMLabel mmLabel8;

		private NumericUpDown numericUpDownInstallmentNum;

		private MMLabel mmLabel11;

		private MMLabel mmLabel13;

		private AmountTextBox textBoxInstallmentAmount;

		private MMLabel mmLabel14;

		private MMSDateTimePicker dateTimePickerDedStart;

		private AmountTextBox textBoxBalance;

		private MMLabel labelBalance;

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
				bool enabled;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = true);
					sysDocComboBox.Enabled = enabled;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = false);
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
				MMLabel mMLabel = labelBalance;
				enabled = (textBoxBalance.Visible = !isNewRecord);
				mMLabel.Visible = enabled;
				comboBoxEmployee.ReadOnly = !isNewRecord;
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

		public EmployeeLoanForm()
		{
			InitializeComponent();
			AddEvents();
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
					int value = int.Parse(numericUpDownInstallmentNum.Value.ToString());
					textBoxInstallmentAmount.Text = (result / (decimal)value).ToString(Format.TotalAmountFormat);
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
					currentData = new EmployeeLoanData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EmployeeLoanTable.Rows[0] : currentData.EmployeeLoanTable.NewRow();
				dataRow.BeginEdit();
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["DedStartDate"] = dateTimePickerDedStart.Value;
				dataRow["Amount"] = textBoxLoanAmount.Text;
				dataRow["InstallmentAmount"] = textBoxInstallmentAmount.Text;
				if (comboBoxLoanType.SelectedID != "")
				{
					dataRow["LoanType"] = comboBoxLoanType.SelectedID;
				}
				else
				{
					dataRow["LoanType"] = DBNull.Value;
				}
				dataRow["Reason"] = textBoxReason.Text;
				dataRow["Reference"] = textBoxReference.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EmployeeLoanTable.Rows.Add(dataRow);
				}
				currentData.EmployeeLoanDetailTable.Rows.Clear();
				dataRow = currentData.EmployeeLoanDetailTable.NewRow();
				dataRow["LoanSysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["LoanVoucherID"] = textBoxVoucherNumber.Text;
				dataRow["PaymentSysDocID"] = DBNull.Value;
				dataRow["PaymentVoucherID"] = DBNull.Value;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["EmployeeID"] = comboBoxEmployee.SelectedID;
				dataRow["Description"] = textBoxReason.Text;
				dataRow["Debit"] = textBoxLoanAmount.Text;
				dataRow["Reference"] = textBoxReference.Text;
				dataRow.EndEdit();
				currentData.EmployeeLoanDetailTable.Rows.Add(dataRow);
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
					currentData = Factory.EmployeeLoanSystem.GetEmployeeLoanByID(SystemDocID, voucherID);
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
				DataRow dataRow = currentData.Tables["Employee_Loan"].Rows[0];
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				num = decimal.Parse(dataRow["InstallmentAmount"].ToString());
				num2 = decimal.Parse(dataRow["Amount"].ToString());
				num3 = decimal.Parse(dataRow["Balance"].ToString());
				if (num2 != num3)
				{
					textBoxLoanAmount.ReadOnly = true;
				}
				else
				{
					textBoxLoanAmount.ReadOnly = false;
				}
				textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
				comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
				comboBoxEmployee.LoadData();
				comboBoxEmployee.SelectedID = dataRow["EmployeeID"].ToString();
				textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
				comboBoxLoanType.SelectedID = dataRow["LoanType"].ToString();
				textBoxInstallmentAmount.Text = decimal.Parse(dataRow["InstallmentAmount"].ToString()).ToString(Format.TotalAmountFormat);
				textBoxBalance.Text = decimal.Parse(dataRow["Balance"].ToString()).ToString(Format.TotalAmountFormat);
				textBoxLoanAmount.Text = decimal.Parse(dataRow["Amount"].ToString()).ToString(Format.TotalAmountFormat);
				dateTimePickerDedStart.Value = DateTime.Parse(dataRow["DedStartDate"].ToString());
				textBoxNote.Text = dataRow["Note"].ToString();
				textBoxReason.Text = dataRow["Reason"].ToString();
				textBoxReference.Text = dataRow["Reference"].ToString();
				dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
				comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
				canEdit = bool.Parse(dataRow["CanEdit"].ToString());
				if (!canEdit)
				{
					AmountTextBox amountTextBox = textBoxLoanAmount;
					bool readOnly = comboBoxEmployee.ReadOnly = false;
					amountTextBox.ReadOnly = readOnly;
				}
				else
				{
					AmountTextBox amountTextBox2 = textBoxLoanAmount;
					bool readOnly = comboBoxEmployee.ReadOnly = true;
					amountTextBox2.ReadOnly = readOnly;
				}
				if (num != 0m)
				{
					numericUpDownInstallmentNum.Value = Convert.ToInt32(num2 / num);
				}
				else
				{
					numericUpDownInstallmentNum.Value = 1m;
				}
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
				num3 = default(decimal);
				foreach (DataRow row in currentData.EmployeeLoanDetailTable.Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					if (row["Debit"] != DBNull.Value)
					{
						dataRow3["SysDocID"] = row["LoanSysDocID"];
						dataRow3["VoucherID"] = row["LoanVoucherID"];
					}
					else
					{
						dataRow3["SysDocID"] = row["PaymentSysDocID"];
						dataRow3["VoucherID"] = row["PaymentVoucherID"];
					}
					dataRow3["Date"] = row["TransactionDate"];
					dataRow3["Description"] = row["TransactionDate"];
					dataRow3["Reference"] = row["Reference"];
					dataRow3["Debit"] = row["Debit"];
					dataRow3["Credit"] = row["Credit"];
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["Debit"].ToString(), out result);
					decimal.TryParse(row["Credit"].ToString(), out result2);
					num3 = num3 + result - result2;
					dataRow3["Balance"] = num3;
					dataRow3.EndEdit();
					dataTable.Rows.Add(dataRow3);
				}
				isLoading = false;
			}
		}

		private void SetupGrid()
		{
			try
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("SysDocID");
				dataTable.Columns.Add("VoucherID");
				dataTable.Columns.Add("Date", typeof(DateTime));
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Reference");
				dataTable.Columns.Add("Debit", typeof(decimal));
				dataTable.Columns.Add("Credit", typeof(decimal));
				dataTable.Columns.Add("Balance", typeof(decimal));
				dataGridList.DataSource = dataTable;
				dataGridList.DisplayLayout.Bands[0].Columns["SysDocID"].Header.Caption = "Doc ID";
				dataGridList.DisplayLayout.Bands[0].Columns["VoucherID"].Header.Caption = "Number";
				UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["Debit"];
				UltraGridColumn ultraGridColumn2 = dataGridList.DisplayLayout.Bands[0].Columns["Credit"];
				string text2 = dataGridList.DisplayLayout.Bands[0].Columns["Balance"].Format = "#,##0.00";
				string text5 = ultraGridColumn.Format = (ultraGridColumn2.Format = text2);
				AppearanceBase cellAppearance = dataGridList.DisplayLayout.Bands[0].Columns["Debit"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridList.DisplayLayout.Bands[0].Columns["Credit"].CellAppearance;
				HAlign hAlign2 = dataGridList.DisplayLayout.Bands[0].Columns["Balance"].CellAppearance.TextHAlign = HAlign.Right;
				HAlign hAlign5 = cellAppearance.TextHAlign = (cellAppearance2.TextHAlign = hAlign2);
				dataGridList.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.Select;
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.EmployeeLoanSystem.CreateEmployeeLoan(currentData, isUpdate: false);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeLoan, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.EmployeeLoanSystem.CreateEmployeeLoan(currentData, isUpdate: true);
				}
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
			if (comboBoxEmployee.SelectedID == "" || comboBoxLoanType.SelectedID == "" || comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Employee_Loan", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
			comboBoxEmployee.Clear();
			textBoxEmployeeName.Clear();
			textBoxFromDepartment.Clear();
			textBoxFromDivision.Clear();
			textBoxFromLocation.Clear();
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
			textBoxDesignation.Clear();
			textBoxInstallmentAmount.Text = 0.ToString(Format.TotalAmountFormat);
			textBoxLoanAmount.Text = 0.ToString(Format.TotalAmountFormat);
			numericUpDownInstallmentNum.Value = 1m;
			comboBoxLoanType.Clear();
			dateTimePickerDedStart.Value = DateTime.Now;
			textBoxNote.Clear();
			textBoxReason.Clear();
			dateTimePickerDate.Value = DateTime.Today;
			textBoxReference.Clear();
			IsVoid = false;
			canEdit = true;
			textBoxLoanAmount.ReadOnly = false;
			textBoxBalance.Clear();
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
			comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
			(dataGridList.DataSource as DataTable).Rows.Clear();
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
				return Factory.EmployeeLoanSystem.DeleteLoan(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Employee_Loan", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Employee_Loan", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Employee_Loan", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Employee_Loan", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Employee_Loan", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					comboBoxSysDoc.FilterByType(SysDocTypes.EmployeeLoan);
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
				textBoxEmployeeName.Text = comboBoxEmployee.SelectedName;
				if (comboBoxEmployee.SelectedID == "")
				{
					textBoxFromLocation.Text = "";
					textBoxFromDivision.Text = "";
					textBoxFromDepartment.Text = "";
					textBoxDesignation.Text = "";
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
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.EmployeeLoan);
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
			FormActivator.BringFormToFront(FormActivator.EmployeeLoanListFormObj);
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
						PrintHelper.PrintDocument(employeeLoanToPrint, selectedID, "Employee Loan", SysDocTypes.EmployeeLoan, isPrint, showPrintDialog);
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
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeLoanForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelDetails = new System.Windows.Forms.Panel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			numericUpDownInstallmentNum = new System.Windows.Forms.NumericUpDown();
			labelBalance = new Micromind.UISupport.MMLabel();
			textBoxInstallmentAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxBalance = new Micromind.UISupport.AmountTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxLoanAmount = new Micromind.UISupport.AmountTextBox();
			textBoxReason = new Micromind.UISupport.MMTextBox();
			comboBoxLoanType = new Micromind.DataControls.EmployeeLoanTypeComboBox();
			textBoxReference = new Micromind.UISupport.MMTextBox();
			dateTimePickerDedStart = new Micromind.UISupport.MMSDateTimePicker();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxFromDepartment = new Micromind.UISupport.MMTextBox();
			textBoxFromDivision = new Micromind.UISupport.MMTextBox();
			textBoxEmployeeName = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			textBoxDesignation = new Micromind.UISupport.MMTextBox();
			textBoxFromLocation = new Micromind.UISupport.MMTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridList = new Micromind.UISupport.DataGridList();
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
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownInstallmentNum).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLoanType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
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
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(ultraGroupBox1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(comboBoxEmployee);
			panelDetails.Controls.Add(formManager);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(textBoxFromDepartment);
			panelDetails.Controls.Add(textBoxFromDivision);
			panelDetails.Controls.Add(textBoxEmployeeName);
			panelDetails.Controls.Add(mmLabel10);
			panelDetails.Controls.Add(textBoxDesignation);
			panelDetails.Controls.Add(textBoxFromLocation);
			panelDetails.Controls.Add(mmLabel5);
			panelDetails.Controls.Add(mmLabel3);
			panelDetails.Controls.Add(mmLabel7);
			panelDetails.Controls.Add(mmLabel4);
			panelDetails.Controls.Add(mmLabel12);
			panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			panelDetails.Location = new System.Drawing.Point(0, 0);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(705, 389);
			panelDetails.TabIndex = 124;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(10, 28);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(62, 15);
			ultraFormattedLinkLabel6.TabIndex = 130;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Employee:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(mmLabel6);
			ultraGroupBox1.Controls.Add(mmLabel8);
			ultraGroupBox1.Controls.Add(mmLabel14);
			ultraGroupBox1.Controls.Add(mmLabel11);
			ultraGroupBox1.Controls.Add(mmLabel13);
			ultraGroupBox1.Controls.Add(numericUpDownInstallmentNum);
			ultraGroupBox1.Controls.Add(labelBalance);
			ultraGroupBox1.Controls.Add(textBoxInstallmentAmount);
			ultraGroupBox1.Controls.Add(mmLabel1);
			ultraGroupBox1.Controls.Add(textBoxBalance);
			ultraGroupBox1.Controls.Add(mmLabel9);
			ultraGroupBox1.Controls.Add(textBoxLoanAmount);
			ultraGroupBox1.Controls.Add(textBoxReason);
			ultraGroupBox1.Controls.Add(comboBoxLoanType);
			ultraGroupBox1.Controls.Add(textBoxReference);
			ultraGroupBox1.Controls.Add(dateTimePickerDedStart);
			ultraGroupBox1.Location = new System.Drawing.Point(4, 79);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(705, 146);
			ultraGroupBox1.TabIndex = 9;
			ultraGroupBox1.Text = "Loan Details";
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = true;
			mmLabel6.Location = new System.Drawing.Point(4, 25);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(71, 13);
			mmLabel6.TabIndex = 9;
			mmLabel6.Text = "Loan Type:";
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = true;
			mmLabel8.Location = new System.Drawing.Point(4, 47);
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
			mmLabel14.Location = new System.Drawing.Point(4, 93);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(100, 13);
			mmLabel14.TabIndex = 0;
			mmLabel14.Text = "Deduction Start:";
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = true;
			mmLabel11.Location = new System.Drawing.Point(4, 68);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(78, 13);
			mmLabel11.TabIndex = 0;
			mmLabel11.Text = "Installments:";
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = true;
			mmLabel13.Location = new System.Drawing.Point(186, 70);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(118, 13);
			mmLabel13.TabIndex = 0;
			mmLabel13.Text = "Installment Amount:";
			numericUpDownInstallmentNum.Location = new System.Drawing.Point(109, 66);
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
			numericUpDownInstallmentNum.TabIndex = 4;
			numericUpDownInstallmentNum.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			labelBalance.AutoSize = true;
			labelBalance.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelBalance.IsFieldHeader = false;
			labelBalance.IsRequired = false;
			labelBalance.Location = new System.Drawing.Point(251, 47);
			labelBalance.Name = "labelBalance";
			labelBalance.PenWidth = 1f;
			labelBalance.ShowBorder = false;
			labelBalance.Size = new System.Drawing.Size(49, 13);
			labelBalance.TabIndex = 9;
			labelBalance.Text = "Balance:";
			labelBalance.Visible = false;
			textBoxInstallmentAmount.AllowDecimal = true;
			textBoxInstallmentAmount.CustomReportFieldName = "";
			textBoxInstallmentAmount.CustomReportKey = "";
			textBoxInstallmentAmount.CustomReportValueType = 1;
			textBoxInstallmentAmount.IsComboTextBox = false;
			textBoxInstallmentAmount.IsModified = false;
			textBoxInstallmentAmount.Location = new System.Drawing.Point(325, 66);
			textBoxInstallmentAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxInstallmentAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxInstallmentAmount.Name = "textBoxInstallmentAmount";
			textBoxInstallmentAmount.NullText = "0";
			textBoxInstallmentAmount.Size = new System.Drawing.Size(142, 20);
			textBoxInstallmentAmount.TabIndex = 5;
			textBoxInstallmentAmount.Text = "0.00";
			textBoxInstallmentAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxInstallmentAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(4, 114);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(63, 13);
			mmLabel1.TabIndex = 9;
			mmLabel1.Text = "Description:";
			textBoxBalance.AllowDecimal = true;
			textBoxBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBalance.CustomReportFieldName = "";
			textBoxBalance.CustomReportKey = "";
			textBoxBalance.CustomReportValueType = 1;
			textBoxBalance.ForeColor = System.Drawing.Color.Black;
			textBoxBalance.IsComboTextBox = false;
			textBoxBalance.IsModified = false;
			textBoxBalance.Location = new System.Drawing.Point(325, 44);
			textBoxBalance.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxBalance.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxBalance.Name = "textBoxBalance";
			textBoxBalance.NullText = "0";
			textBoxBalance.ReadOnly = true;
			textBoxBalance.Size = new System.Drawing.Size(142, 20);
			textBoxBalance.TabIndex = 3;
			textBoxBalance.TabStop = false;
			textBoxBalance.Text = "0.00";
			textBoxBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBalance.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxBalance.Visible = false;
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(251, 26);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(60, 13);
			mmLabel9.TabIndex = 9;
			mmLabel9.Text = "Reference:";
			textBoxLoanAmount.AllowDecimal = true;
			textBoxLoanAmount.CustomReportFieldName = "";
			textBoxLoanAmount.CustomReportKey = "";
			textBoxLoanAmount.CustomReportValueType = 1;
			textBoxLoanAmount.IsComboTextBox = false;
			textBoxLoanAmount.IsModified = false;
			textBoxLoanAmount.Location = new System.Drawing.Point(109, 44);
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
			textBoxLoanAmount.TabIndex = 2;
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
			textBoxReason.Location = new System.Drawing.Point(109, 112);
			textBoxReason.MaxLength = 255;
			textBoxReason.Name = "textBoxReason";
			textBoxReason.Size = new System.Drawing.Size(575, 20);
			textBoxReason.TabIndex = 7;
			comboBoxLoanType.Assigned = false;
			comboBoxLoanType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLoanType.CustomReportFieldName = "";
			comboBoxLoanType.CustomReportKey = "";
			comboBoxLoanType.CustomReportValueType = 1;
			comboBoxLoanType.DescriptionTextBox = null;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLoanType.DisplayLayout.Appearance = appearance3;
			comboBoxLoanType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLoanType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLoanType.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLoanType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxLoanType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLoanType.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxLoanType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLoanType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLoanType.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLoanType.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxLoanType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLoanType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLoanType.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLoanType.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxLoanType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLoanType.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLoanType.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxLoanType.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxLoanType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLoanType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxLoanType.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxLoanType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLoanType.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			comboBoxLoanType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLoanType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLoanType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLoanType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLoanType.Editable = true;
			comboBoxLoanType.FilterString = "";
			comboBoxLoanType.HasAllAccount = false;
			comboBoxLoanType.HasCustom = false;
			comboBoxLoanType.IsDataLoaded = false;
			comboBoxLoanType.Location = new System.Drawing.Point(109, 22);
			comboBoxLoanType.MaxDropDownItems = 12;
			comboBoxLoanType.Name = "comboBoxLoanType";
			comboBoxLoanType.ShowInactiveItems = false;
			comboBoxLoanType.ShowQuickAdd = true;
			comboBoxLoanType.Size = new System.Drawing.Size(139, 20);
			comboBoxLoanType.TabIndex = 0;
			comboBoxLoanType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxReference.BackColor = System.Drawing.Color.White;
			textBoxReference.CustomReportFieldName = "";
			textBoxReference.CustomReportKey = "";
			textBoxReference.CustomReportValueType = 1;
			textBoxReference.IsComboTextBox = false;
			textBoxReference.IsModified = false;
			textBoxReference.Location = new System.Drawing.Point(326, 22);
			textBoxReference.MaxLength = 15;
			textBoxReference.Name = "textBoxReference";
			textBoxReference.Size = new System.Drawing.Size(142, 20);
			textBoxReference.TabIndex = 1;
			dateTimePickerDedStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDedStart.Location = new System.Drawing.Point(109, 89);
			dateTimePickerDedStart.Name = "dateTimePickerDedStart";
			dateTimePickerDedStart.Size = new System.Drawing.Size(139, 20);
			dateTimePickerDedStart.TabIndex = 6;
			dateTimePickerDedStart.Value = new System.DateTime(2014, 6, 11, 16, 24, 17, 769);
			appearance15.FontData.BoldAsString = "True";
			appearance15.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance15;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(10, 7);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 122;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance16;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance17;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(112, 4);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(139, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance29.FontData.BoldAsString = "True";
			appearance29.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance29;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(261, 7);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(53, 15);
			ultraFormattedLinkLabel2.TabIndex = 121;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Number:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance30;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			textBoxVoucherNumber.Location = new System.Drawing.Point(328, 4);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(142, 20);
			textBoxVoucherNumber.TabIndex = 1;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(566, 4);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(121, 20);
			dateTimePickerDate.TabIndex = 2;
			dateTimePickerDate.Value = new System.DateTime(2014, 6, 11, 16, 24, 17, 770);
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = null;
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
			comboBoxEmployee.Location = new System.Drawing.Point(112, 26);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(216, 20);
			comboBoxEmployee.TabIndex = 3;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxEmployee.SelectedIndexChanged += new System.EventHandler(comboBoxEmployee_SelectedIndexChanged);
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
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(113, 226);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(574, 151);
			textBoxNote.TabIndex = 10;
			textBoxFromDepartment.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDepartment.CustomReportFieldName = "";
			textBoxFromDepartment.CustomReportKey = "";
			textBoxFromDepartment.CustomReportValueType = 1;
			textBoxFromDepartment.IsComboTextBox = false;
			textBoxFromDepartment.IsModified = false;
			textBoxFromDepartment.Location = new System.Drawing.Point(587, 48);
			textBoxFromDepartment.MaxLength = 255;
			textBoxFromDepartment.Name = "textBoxFromDepartment";
			textBoxFromDepartment.ReadOnly = true;
			textBoxFromDepartment.Size = new System.Drawing.Size(100, 20);
			textBoxFromDepartment.TabIndex = 8;
			textBoxFromDepartment.TabStop = false;
			textBoxFromDivision.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromDivision.CustomReportFieldName = "";
			textBoxFromDivision.CustomReportKey = "";
			textBoxFromDivision.CustomReportValueType = 1;
			textBoxFromDivision.IsComboTextBox = false;
			textBoxFromDivision.IsModified = false;
			textBoxFromDivision.Location = new System.Drawing.Point(422, 48);
			textBoxFromDivision.MaxLength = 64;
			textBoxFromDivision.Name = "textBoxFromDivision";
			textBoxFromDivision.ReadOnly = true;
			textBoxFromDivision.Size = new System.Drawing.Size(100, 20);
			textBoxFromDivision.TabIndex = 7;
			textBoxFromDivision.TabStop = false;
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxEmployeeName.CustomReportFieldName = "";
			textBoxEmployeeName.CustomReportKey = "";
			textBoxEmployeeName.CustomReportValueType = 1;
			textBoxEmployeeName.IsComboTextBox = false;
			textBoxEmployeeName.IsModified = false;
			textBoxEmployeeName.Location = new System.Drawing.Point(329, 26);
			textBoxEmployeeName.MaxLength = 15;
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(358, 20);
			textBoxEmployeeName.TabIndex = 4;
			textBoxEmployeeName.TabStop = false;
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(8, 226);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(33, 13);
			mmLabel10.TabIndex = 9;
			mmLabel10.Text = "Note:";
			textBoxDesignation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDesignation.CustomReportFieldName = "";
			textBoxDesignation.CustomReportKey = "";
			textBoxDesignation.CustomReportValueType = 1;
			textBoxDesignation.IsComboTextBox = false;
			textBoxDesignation.IsModified = false;
			textBoxDesignation.Location = new System.Drawing.Point(112, 48);
			textBoxDesignation.MaxLength = 15;
			textBoxDesignation.Name = "textBoxDesignation";
			textBoxDesignation.ReadOnly = true;
			textBoxDesignation.Size = new System.Drawing.Size(100, 20);
			textBoxDesignation.TabIndex = 5;
			textBoxDesignation.TabStop = false;
			textBoxFromLocation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFromLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFromLocation.CustomReportFieldName = "";
			textBoxFromLocation.CustomReportKey = "";
			textBoxFromLocation.CustomReportValueType = 1;
			textBoxFromLocation.IsComboTextBox = false;
			textBoxFromLocation.IsModified = false;
			textBoxFromLocation.Location = new System.Drawing.Point(268, 48);
			textBoxFromLocation.MaxLength = 15;
			textBoxFromLocation.Name = "textBoxFromLocation";
			textBoxFromLocation.ReadOnly = true;
			textBoxFromLocation.Size = new System.Drawing.Size(100, 20);
			textBoxFromLocation.TabIndex = 6;
			textBoxFromLocation.TabStop = false;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(525, 51);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(65, 13);
			mmLabel5.TabIndex = 9;
			mmLabel5.Text = "Department:";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(371, 51);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(47, 13);
			mmLabel3.TabIndex = 9;
			mmLabel3.Text = "Division:";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(7, 51);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(66, 13);
			mmLabel7.TabIndex = 9;
			mmLabel7.Text = "Designation:";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(216, 51);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(51, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Location:";
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = true;
			mmLabel12.Location = new System.Drawing.Point(475, 7);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(70, 13);
			mmLabel12.TabIndex = 0;
			mmLabel12.Text = "Loan Date:";
			tabPageDetails.Controls.Add(dataGridList);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(705, 389);
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance43;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance44.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance44.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance44;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance45;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance46.BackColor2 = System.Drawing.SystemColors.Control;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance46;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance47;
			appearance48.BackColor = System.Drawing.SystemColors.Highlight;
			appearance48.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance48;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance49;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance50;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance51.BackColor = System.Drawing.SystemColors.Control;
			appearance51.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance51.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance51.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance51;
			appearance52.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance52;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance53;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance54;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(4, 9);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(696, 382);
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
			panelButtons.TabIndex = 0;
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
			linePanelDown.TabIndex = 14;
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
			appearance55.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance55;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Details";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(705, 389);
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
			base.Name = "EmployeeLoanForm";
			Text = "Employee Loan Entry";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownInstallmentNum).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLoanType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
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
