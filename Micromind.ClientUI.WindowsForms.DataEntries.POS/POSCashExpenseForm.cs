using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class POSCashExpenseForm : Form, IForm
	{
		private TransactionData currentData;

		public int ShiftID = -1;

		public int BatchID = -1;

		private const string TABLENAME_CONST = "GL_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool trackConsignInExpense = CompanyPreferences.TrackConsignInExpense;

		private bool isCostCenterMandatory = CompanyPreferences.IsCostCenterMandatory;

		private bool showAllocationForm = CompanyPreferences.ShowAllocationForm;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private IContainer components;

		private ToolStrip toolStrip1;

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

		private DateTimePicker dateTimePickerDate;

		private TextBox textBoxVoucherNumber;

		private MMLabel mmLabel1;

		private TextBox textBoxNote;

		private Label label3;

		private AllAccountsComboBox comboBoxGridAccount;

		private AnalysisComboBox comboBoxGridanalysis;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private CostCenterComboBox comboBoxGridCostCenter;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private MMLabel mmLabel2;

		private AmountTextBox textBoxAmount;

		private EmployeeComboBox comboBoxEmployee;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripButton toolStripButtonAttach;

		private AmountTextBox textBoxTaxAmount;

		private TaxGroupComboBox comboBoxTaxGroup;

		private Label labelTaxGroup;

		private TextBox textBoxAccount;

		private POSExpenseComboBox posExpenseComboBox1;

		private TextBox textBoxDocID;

		private Label label1;

		private Label label2;

		private Label label4;

		private TextBox textBoxRegister;

		private Label label5;

		private Label label6;

		private AmountTextBox textBoxTotal;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1005;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => textBoxDocID.Text;

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
					XPButton xPButton = buttonDelete;
					bool enabled = buttonVoid.Enabled = false;
					xPButton.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton2.Enabled = enabled;
				}
				toolStripButtonDistribution.Enabled = !value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
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
					if (value)
					{
						buttonVoid.Text = UIMessages.Unvoid;
					}
					else
					{
						buttonVoid.Text = UIMessages.Void;
					}
				}
			}
		}

		public POSCashExpenseForm()
		{
			InitializeComponent();
			AddEvents();
			ShiftID = UIGlobal.GetActiveShiftNumber();
			BatchID = UIGlobal.GetActiveBatchNumber();
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			base.KeyDown += Form_KeyDown;
			posExpenseComboBox1.SelectedIndexChanged += PosExpenseComboBox1_SelectedIndexChanged;
			comboBoxTaxGroup.SelectedIndexChanged += ComboBoxTaxGroup_SelectedIndexChanged;
		}

		private void ComboBoxTaxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			ItemTaxOptions itemTaxOptions = ItemTaxOptions.NonTaxable;
			if (comboBoxTaxGroup.SelectedID != "")
			{
				itemTaxOptions = ItemTaxOptions.Taxable;
			}
			if (itemTaxOptions != ItemTaxOptions.NonTaxable)
			{
				TaxTransactionData tag = TaxHelper.CreateTaxDetailData(PayeeTaxOptions.Taxable, comboBoxTaxGroup.SelectedID);
				textBoxTaxAmount.Tag = tag;
			}
			else
			{
				textBoxTaxAmount.Tag = null;
			}
			CalculateTax();
		}

		private void PosExpenseComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxAccount.Text = posExpenseComboBox1.SelectedID;
		}

		private void Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new TransactionData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.TransactionTable.Rows[0] : currentData.TransactionTable.NewRow();
				dataRow["TransactionID"] = 0;
				dataRow["SysDocType"] = (byte)4;
				dataRow["Description"] = textBoxNote.Text;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = textBoxDocID.Text;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["RegisterID"] = textBoxRegister.Text;
				dataRow["IsPOS"] = true;
				dataRow["POSShiftID"] = ShiftID;
				dataRow["POSBatchID"] = BatchID;
				dataRow["PayeeType"] = "A";
				dataRow["PayeeID"] = textBoxAccount.Text;
				dataRow["CurrencyID"] = DBNull.Value;
				dataRow["CurrencyRate"] = 1;
				dataRow["Amount"] = textBoxAmount.Text;
				if (comboBoxTaxGroup.SelectedID != "")
				{
					dataRow["TaxGroupID"] = comboBoxTaxGroup.SelectedID;
					dataRow["TaxAmount"] = textBoxTaxAmount.Text;
				}
				else
				{
					dataRow["TaxGroupID"] = DBNull.Value;
					dataRow["TaxAmount"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.TransactionTable.Rows.Add(dataRow);
				}
				currentData.TransactionDetailsTable.Rows.Clear();
				DataRow dataRow2 = currentData.TransactionDetailsTable.NewRow();
				dataRow2.BeginEdit();
				dataRow2["Description"] = textBoxNote.Text;
				dataRow2["PaymentMethodType"] = (byte)1;
				dataRow2["Amount"] = textBoxAmount.Text;
				dataRow2["AmountFC"] = DBNull.Value;
				dataRow2["RowIndex"] = 0;
				dataRow2.EndEdit();
				currentData.TransactionDetailsTable.Rows.Add(dataRow2);
				currentData.Tables["Tax_Detail"].Rows.Clear();
				string text = textBoxDocID.Text;
				string text2 = textBoxVoucherNumber.Text;
				if (textBoxTaxAmount.Tag != null)
				{
					TaxHelper.CreateTaxRows(currentData, textBoxTaxAmount.Tag as TaxTransactionData, TaxDetailLevel.Transaction, text, text2, -1, Global.BaseCurrencyID, 1m);
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
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.TransactionSystem.GetTransactionByID(SystemDocID, voucherID);
					if (currentData != null)
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
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["GL_Transaction"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRegister.Text = dataRow["RegisterID"].ToString();
					textBoxNote.Text = dataRow["Description"].ToString();
					string text3 = textBoxAccount.Text = (posExpenseComboBox1.SelectedID = dataRow["PayeeID"].ToString());
					if (dataRow["TaxAmount"] != DBNull.Value)
					{
						textBoxTaxAmount.Text = Math.Round(decimal.Parse(dataRow["Amount"].ToString()), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
					}
					comboBoxTaxGroup.SelectedID = dataRow["TaxGroupID"].ToString();
					if (dataRow["IsVoid"] != DBNull.Value)
					{
						IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
					}
					else
					{
						IsVoid = false;
					}
					if (currentData.Tables.Contains("Transaction_Details") && currentData.TransactionDetailsTable.Rows.Count != 0)
					{
						DataRow dataRow2 = currentData.Tables["Transaction_Details"].Rows[0];
						if (dataRow2["Amount"] != DBNull.Value)
						{
							textBoxAmount.Text = Math.Round(decimal.Parse(dataRow2["Amount"].ToString()), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
						}
						else
						{
							textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
						}
						textBoxNote.Text = dataRow2["Description"].ToString();
						DataRow[] array = currentData.TaxDetailsTable.Select("RowIndex = -1 AND TaxLevel = " + (byte)1);
						if (array.Length != 0)
						{
							TaxTransactionData taxTransactionData = new TaxTransactionData();
							taxTransactionData.Merge(array);
							textBoxTaxAmount.Tag = taxTransactionData;
						}
						CalculateTax();
					}
				}
			}
			catch
			{
				throw;
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
				bool flag = (!isNewRecord) ? Factory.TransactionSystem.UpdateTransaction(currentData) : Factory.TransactionSystem.CreateTransaction(currentData);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					bool flag2 = false;
					if (false)
					{
						if (flag2)
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
						LocalSettings.GlobalSettings.SaveSetting(base.Name + "TaxID", comboBoxTaxGroup.SelectedID);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("GL_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(textBoxDocID.Text, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
			if (isNewRecord && dateTimePickerDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
				return false;
			}
			if (isNewRecord && dateTimePickerDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			if (textBoxRegister.Text == "" || textBoxDocID.Text == "" || textBoxVoucherNumber.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			decimal result = default(decimal);
			decimal.TryParse(textBoxAmount.Text, out result);
			if (result <= 0m)
			{
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("GL_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
			try
			{
				textBoxNote.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxEmployee.Clear();
				comboBoxTaxGroup.SelectedID = LocalSettings.GlobalSettings.GetSetting(base.Name + "TaxID", "").ToString();
				textBoxTaxAmount.Clear();
				textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				CalculateTax();
				IsVoid = false;
				formManager.ResetDirty();
				dateTimePickerDate.Focus();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
				return Factory.TransactionSystem.DeleteTransaction(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("GL_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID, "IsPOS", true);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("GL_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID, "IsPOS", true);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("GL_Transaction", "VoucherID", "SysDocID", SystemDocID, "IsPOS", true);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("GL_Transaction", "VoucherID", "SysDocID", SystemDocID, "IsPOS", true);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("GL_Transaction", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void JournalLeavesForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxTaxGroup.ReadOnly = !CompanyPreferences.IsTax;
				SetSecurity();
				textBoxRegister.Text = Global.CurrentCashRegisterID;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("POS_CashRegister", "ExpenseDocID", "CashRegisterID", textBoxRegister.Text);
				if (!fieldValue.IsNullOrEmpty())
				{
					textBoxDocID.Text = fieldValue.ToString();
				}
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				posExpenseComboBox1.CashRegisterID = textBoxRegister.Text;
				posExpenseComboBox1.LoadData();
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
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDocNumber))
			{
				textBoxVoucherNumber.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransDiffLocation))
			{
				AllowEditTransDiffLocation = false;
			}
			else
			{
				AllowEditTransDiffLocation = true;
			}
			comboBoxTaxGroup.ReadOnly = false;
			comboBoxTaxGroup.ReadOnly = !CompanyPreferences.IsTax;
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
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
				return Factory.TransactionSystem.VoidTransaction(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(textBoxDocID.Text, SysDocTypes.CashPayment);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEmployee(comboBoxEmployee.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditRegister(textBoxRegister.Text);
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string systemDocID = SystemDocID;
					string text = textBoxVoucherNumber.Text;
					DataSet transactionToPrint = Factory.TransactionSystem.GetTransactionToPrint(systemDocID, text);
					if (transactionToPrint == null || transactionToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(transactionToPrint, systemDocID, "Cash Expense", SysDocTypes.CashPayment, isPrint, showPrintDialog);
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

		public void EditDocument(string sysDocID, string voucherID)
		{
			textBoxDocID.Text = sysDocID;
			LoadData(voucherID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.PaymentVoucherListObj);
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = textBoxDocID.Text;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, textBoxDocID.Text, this);
			}
		}

		private void textBoxAmount_Validated(object sender, EventArgs e)
		{
			CalculateTax();
		}

		private void CalculateTax()
		{
			decimal result = default(decimal);
			decimal.TryParse(textBoxAmount.Text, out result);
			UIGlobal.CalculateFieldTax(textBoxTaxAmount, result, priceIncludeTax: false);
			decimal result2 = default(decimal);
			decimal.TryParse(textBoxTaxAmount.Text, out result2);
			textBoxTotal.Text = (result + result2).ToString(Format.TotalAmountFormat);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntitySysDocID = textBoxDocID.Text;
					docManagementForm.EntityName = textBoxDocID.Text;
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.POS.POSCashExpenseForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			panelDetails = new System.Windows.Forms.Panel();
			label6 = new System.Windows.Forms.Label();
			textBoxTotal = new Micromind.UISupport.AmountTextBox();
			label5 = new System.Windows.Forms.Label();
			posExpenseComboBox1 = new Micromind.DataControls.POSExpenseComboBox();
			textBoxAccount = new System.Windows.Forms.TextBox();
			textBoxTaxAmount = new Micromind.UISupport.AmountTextBox();
			comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			labelTaxGroup = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			comboBoxGridCostCenter = new Micromind.DataControls.CostCenterComboBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxGridanalysis = new Micromind.DataControls.AnalysisComboBox();
			comboBoxGridAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxDocID = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			textBoxRegister = new System.Windows.Forms.TextBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)posExpenseComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCostCenter).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridanalysis).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridAccount).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator3,
				toolStripButtonOpenList,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator6,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator4,
				toolStripButtonDistribution,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(645, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
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
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(28, 28);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 326);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(645, 55);
			panelButtons.TabIndex = 4;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(216, 3);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 40);
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
			buttonDelete.Location = new System.Drawing.Point(318, 3);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 40);
			buttonDelete.TabIndex = 3;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 3);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 40);
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
			linePanelDown.Size = new System.Drawing.Size(645, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(535, 3);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 40);
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
			buttonSave.Location = new System.Drawing.Point(12, 3);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 40);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			dateTimePickerDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(474, 13);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(148, 29);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(281, 46);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.ReadOnly = true;
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 20);
			textBoxVoucherNumber.TabIndex = 1;
			textBoxNote.Location = new System.Drawing.Point(77, 206);
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(545, 20);
			textBoxNote.TabIndex = 7;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(10, 209);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(textBoxTotal);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(posExpenseComboBox1);
			panelDetails.Controls.Add(textBoxAccount);
			panelDetails.Controls.Add(textBoxTaxAmount);
			panelDetails.Controls.Add(comboBoxTaxGroup);
			panelDetails.Controls.Add(labelTaxGroup);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(comboBoxEmployee);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(textBoxAmount);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 72);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(634, 248);
			panelDetails.TabIndex = 3;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(9, 179);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(34, 13);
			label6.TabIndex = 174;
			label6.Text = "Total:";
			textBoxTotal.AllowDecimal = true;
			textBoxTotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotal.CustomReportFieldName = "";
			textBoxTotal.CustomReportKey = "";
			textBoxTotal.CustomReportValueType = 1;
			textBoxTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxTotal.IsComboTextBox = false;
			textBoxTotal.IsModified = false;
			textBoxTotal.Location = new System.Drawing.Point(77, 171);
			textBoxTotal.MaxLength = 15;
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
			textBoxTotal.Size = new System.Drawing.Size(261, 29);
			textBoxTotal.TabIndex = 6;
			textBoxTotal.Text = "0.00";
			textBoxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(11, 21);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(51, 13);
			label5.TabIndex = 172;
			label5.Text = "Paid to:";
			posExpenseComboBox1.Assigned = false;
			posExpenseComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			posExpenseComboBox1.CashRegisterID = null;
			posExpenseComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			posExpenseComboBox1.CustomReportFieldName = "";
			posExpenseComboBox1.CustomReportKey = "";
			posExpenseComboBox1.CustomReportValueType = 1;
			posExpenseComboBox1.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			posExpenseComboBox1.DisplayLayout.Appearance = appearance;
			posExpenseComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			posExpenseComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			posExpenseComboBox1.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			posExpenseComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			posExpenseComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			posExpenseComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			posExpenseComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			posExpenseComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			posExpenseComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			posExpenseComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			posExpenseComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			posExpenseComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			posExpenseComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			posExpenseComboBox1.DisplayLayout.Override.CellAppearance = appearance8;
			posExpenseComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			posExpenseComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			posExpenseComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			posExpenseComboBox1.DisplayLayout.Override.HeaderAppearance = appearance10;
			posExpenseComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			posExpenseComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			posExpenseComboBox1.DisplayLayout.Override.RowAppearance = appearance11;
			posExpenseComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			posExpenseComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			posExpenseComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			posExpenseComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			posExpenseComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			posExpenseComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			posExpenseComboBox1.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			posExpenseComboBox1.Editable = true;
			posExpenseComboBox1.FilterString = "";
			posExpenseComboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			posExpenseComboBox1.HasAllAccount = false;
			posExpenseComboBox1.HasCustom = false;
			posExpenseComboBox1.IsDataLoaded = false;
			posExpenseComboBox1.Location = new System.Drawing.Point(77, 13);
			posExpenseComboBox1.MaxDropDownItems = 12;
			posExpenseComboBox1.Name = "posExpenseComboBox1";
			posExpenseComboBox1.ShowInactiveItems = false;
			posExpenseComboBox1.ShowQuickAdd = true;
			posExpenseComboBox1.Size = new System.Drawing.Size(261, 30);
			posExpenseComboBox1.TabIndex = 0;
			posExpenseComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccount.Location = new System.Drawing.Point(501, 109);
			textBoxAccount.Name = "textBoxAccount";
			textBoxAccount.ReadOnly = true;
			textBoxAccount.Size = new System.Drawing.Size(130, 20);
			textBoxAccount.TabIndex = 170;
			textBoxAccount.TabStop = false;
			textBoxAccount.Text = "AccountID(Hidden)";
			textBoxAccount.Visible = false;
			textBoxTaxAmount.AllowDecimal = true;
			textBoxTaxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTaxAmount.CustomReportFieldName = "";
			textBoxTaxAmount.CustomReportKey = "";
			textBoxTaxAmount.CustomReportValueType = 1;
			textBoxTaxAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxTaxAmount.IsComboTextBox = false;
			textBoxTaxAmount.IsModified = false;
			textBoxTaxAmount.Location = new System.Drawing.Point(77, 124);
			textBoxTaxAmount.MaxLength = 15;
			textBoxTaxAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTaxAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTaxAmount.Name = "textBoxTaxAmount";
			textBoxTaxAmount.NullText = "0";
			textBoxTaxAmount.ReadOnly = true;
			textBoxTaxAmount.Size = new System.Drawing.Size(261, 29);
			textBoxTaxAmount.TabIndex = 5;
			textBoxTaxAmount.Text = "0.00";
			textBoxTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTaxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			comboBoxTaxGroup.Assigned = false;
			comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxGroup.CustomReportFieldName = "";
			comboBoxTaxGroup.CustomReportKey = "";
			comboBoxTaxGroup.CustomReportValueType = 1;
			comboBoxTaxGroup.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxGroup.DisplayLayout.Appearance = appearance13;
			comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaxGroup.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxTaxGroup.Editable = true;
			comboBoxTaxGroup.FilterString = "";
			comboBoxTaxGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			comboBoxTaxGroup.HasAllAccount = false;
			comboBoxTaxGroup.HasCustom = false;
			comboBoxTaxGroup.IsDataLoaded = false;
			comboBoxTaxGroup.Location = new System.Drawing.Point(77, 90);
			comboBoxTaxGroup.MaxDropDownItems = 12;
			comboBoxTaxGroup.Name = "comboBoxTaxGroup";
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.ShowInactiveItems = false;
			comboBoxTaxGroup.ShowQuickAdd = true;
			comboBoxTaxGroup.Size = new System.Drawing.Size(261, 30);
			comboBoxTaxGroup.TabIndex = 4;
			comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			labelTaxGroup.AutoSize = true;
			labelTaxGroup.Location = new System.Drawing.Point(8, 98);
			labelTaxGroup.Name = "labelTaxGroup";
			labelTaxGroup.Size = new System.Drawing.Size(60, 13);
			labelTaxGroup.TabIndex = 169;
			labelTaxGroup.Text = "Tax Group:";
			appearance25.FontData.BoldAsString = "False";
			ultraFormattedLinkLabel3.Appearance = appearance25;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(397, 51);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(56, 14);
			ultraFormattedLinkLabel3.TabIndex = 133;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Employee:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			comboBoxEmployee.Assigned = false;
			comboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployee.CustomReportFieldName = "";
			comboBoxEmployee.CustomReportKey = "";
			comboBoxEmployee.CustomReportValueType = 1;
			comboBoxEmployee.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployee.DisplayLayout.Appearance = appearance27;
			comboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployee.Editable = true;
			comboBoxEmployee.FilterString = "";
			comboBoxEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			comboBoxEmployee.HasAllAccount = false;
			comboBoxEmployee.HasCustom = false;
			comboBoxEmployee.IsDataLoaded = false;
			comboBoxEmployee.Location = new System.Drawing.Point(474, 46);
			comboBoxEmployee.MaxDropDownItems = 12;
			comboBoxEmployee.Name = "comboBoxEmployee";
			comboBoxEmployee.ShowInactiveItems = false;
			comboBoxEmployee.ShowQuickAdd = true;
			comboBoxEmployee.ShowTerminatedEmployees = true;
			comboBoxEmployee.Size = new System.Drawing.Size(148, 30);
			comboBoxEmployee.TabIndex = 3;
			comboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(10, 53);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(53, 13);
			mmLabel2.TabIndex = 130;
			mmLabel2.Text = "Amount:";
			textBoxAmount.AllowDecimal = true;
			textBoxAmount.BackColor = System.Drawing.Color.White;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.IsModified = false;
			textBoxAmount.Location = new System.Drawing.Point(77, 47);
			textBoxAmount.MaxLength = 15;
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
			textBoxAmount.Size = new System.Drawing.Size(261, 29);
			textBoxAmount.TabIndex = 1;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxAmount.Validated += new System.EventHandler(textBoxAmount_Validated);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(395, 21);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			comboBoxGridCostCenter.Assigned = false;
			comboBoxGridCostCenter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCostCenter.CustomReportFieldName = "";
			comboBoxGridCostCenter.CustomReportKey = "";
			comboBoxGridCostCenter.CustomReportValueType = 1;
			comboBoxGridCostCenter.DescriptionTextBox = null;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCostCenter.DisplayLayout.Appearance = appearance39;
			comboBoxGridCostCenter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCostCenter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.Appearance = appearance40;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance41;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance42.BackColor2 = System.Drawing.SystemColors.Control;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.PromptAppearance = appearance42;
			comboBoxGridCostCenter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCostCenter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCostCenter.DisplayLayout.Override.ActiveCellAppearance = appearance43;
			appearance44.BackColor = System.Drawing.SystemColors.Highlight;
			appearance44.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCostCenter.DisplayLayout.Override.ActiveRowAppearance = appearance44;
			comboBoxGridCostCenter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCostCenter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCostCenter.DisplayLayout.Override.CardAreaAppearance = appearance45;
			appearance46.BorderColor = System.Drawing.Color.Silver;
			appearance46.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCostCenter.DisplayLayout.Override.CellAppearance = appearance46;
			comboBoxGridCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCostCenter.DisplayLayout.Override.CellPadding = 0;
			appearance47.BackColor = System.Drawing.SystemColors.Control;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCostCenter.DisplayLayout.Override.GroupByRowAppearance = appearance47;
			appearance48.TextHAlignAsString = "Left";
			comboBoxGridCostCenter.DisplayLayout.Override.HeaderAppearance = appearance48;
			comboBoxGridCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCostCenter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCostCenter.DisplayLayout.Override.RowAppearance = appearance49;
			comboBoxGridCostCenter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCostCenter.DisplayLayout.Override.TemplateAddRowAppearance = appearance50;
			comboBoxGridCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridCostCenter.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridCostCenter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridCostCenter.Editable = true;
			comboBoxGridCostCenter.FilterString = "";
			comboBoxGridCostCenter.HasAllAccount = false;
			comboBoxGridCostCenter.HasCustom = false;
			comboBoxGridCostCenter.IsDataLoaded = false;
			comboBoxGridCostCenter.Location = new System.Drawing.Point(661, 31);
			comboBoxGridCostCenter.MaxDropDownItems = 12;
			comboBoxGridCostCenter.Name = "comboBoxGridCostCenter";
			comboBoxGridCostCenter.ShowInactiveItems = false;
			comboBoxGridCostCenter.ShowQuickAdd = true;
			comboBoxGridCostCenter.Size = new System.Drawing.Size(43, 20);
			comboBoxGridCostCenter.TabIndex = 115;
			comboBoxGridCostCenter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCostCenter.Visible = false;
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
			comboBoxGridanalysis.Assigned = false;
			comboBoxGridanalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridanalysis.CustomReportFieldName = "";
			comboBoxGridanalysis.CustomReportKey = "";
			comboBoxGridanalysis.CustomReportValueType = 1;
			comboBoxGridanalysis.DescriptionTextBox = null;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridanalysis.DisplayLayout.Appearance = appearance51;
			comboBoxGridanalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridanalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance52.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.Appearance = appearance52;
			appearance53.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance53;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance54.BackColor2 = System.Drawing.SystemColors.Control;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance54;
			comboBoxGridanalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridanalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridanalysis.DisplayLayout.Override.ActiveCellAppearance = appearance55;
			appearance56.BackColor = System.Drawing.SystemColors.Highlight;
			appearance56.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridanalysis.DisplayLayout.Override.ActiveRowAppearance = appearance56;
			comboBoxGridanalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridanalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridanalysis.DisplayLayout.Override.CardAreaAppearance = appearance57;
			appearance58.BorderColor = System.Drawing.Color.Silver;
			appearance58.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridanalysis.DisplayLayout.Override.CellAppearance = appearance58;
			comboBoxGridanalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridanalysis.DisplayLayout.Override.CellPadding = 0;
			appearance59.BackColor = System.Drawing.SystemColors.Control;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridanalysis.DisplayLayout.Override.GroupByRowAppearance = appearance59;
			appearance60.TextHAlignAsString = "Left";
			comboBoxGridanalysis.DisplayLayout.Override.HeaderAppearance = appearance60;
			comboBoxGridanalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridanalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridanalysis.DisplayLayout.Override.RowAppearance = appearance61;
			comboBoxGridanalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridanalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance62;
			comboBoxGridanalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridanalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridanalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridanalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridanalysis.Editable = true;
			comboBoxGridanalysis.FilterString = "";
			comboBoxGridanalysis.HasAllAccount = false;
			comboBoxGridanalysis.HasCustom = false;
			comboBoxGridanalysis.IsDataLoaded = false;
			comboBoxGridanalysis.Location = new System.Drawing.Point(661, 31);
			comboBoxGridanalysis.MaxDropDownItems = 12;
			comboBoxGridanalysis.Name = "comboBoxGridanalysis";
			comboBoxGridanalysis.ShowInactiveItems = false;
			comboBoxGridanalysis.ShowQuickAdd = true;
			comboBoxGridanalysis.Size = new System.Drawing.Size(81, 20);
			comboBoxGridanalysis.TabIndex = 109;
			comboBoxGridanalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridanalysis.Visible = false;
			comboBoxGridAccount.Assigned = false;
			comboBoxGridAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridAccount.CustomReportFieldName = "";
			comboBoxGridAccount.CustomReportKey = "";
			comboBoxGridAccount.CustomReportValueType = 1;
			comboBoxGridAccount.DescriptionTextBox = null;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridAccount.DisplayLayout.Appearance = appearance63;
			comboBoxGridAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.GroupByBox.Appearance = appearance64;
			appearance65.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance65;
			comboBoxGridAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance66.BackColor2 = System.Drawing.SystemColors.Control;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance66;
			comboBoxGridAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridAccount.DisplayLayout.Override.ActiveCellAppearance = appearance67;
			appearance68.BackColor = System.Drawing.SystemColors.Highlight;
			appearance68.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridAccount.DisplayLayout.Override.ActiveRowAppearance = appearance68;
			comboBoxGridAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.Override.CardAreaAppearance = appearance69;
			appearance70.BorderColor = System.Drawing.Color.Silver;
			appearance70.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridAccount.DisplayLayout.Override.CellAppearance = appearance70;
			comboBoxGridAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridAccount.DisplayLayout.Override.CellPadding = 0;
			appearance71.BackColor = System.Drawing.SystemColors.Control;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.Override.GroupByRowAppearance = appearance71;
			appearance72.TextHAlignAsString = "Left";
			comboBoxGridAccount.DisplayLayout.Override.HeaderAppearance = appearance72;
			comboBoxGridAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridAccount.DisplayLayout.Override.RowAppearance = appearance73;
			comboBoxGridAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance74;
			comboBoxGridAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridAccount.Editable = true;
			comboBoxGridAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxGridAccount.FilterString = "";
			comboBoxGridAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxGridAccount.FilterSysDocID = "";
			comboBoxGridAccount.HasAllAccount = false;
			comboBoxGridAccount.HasCustom = false;
			comboBoxGridAccount.IsDataLoaded = false;
			comboBoxGridAccount.Location = new System.Drawing.Point(661, 31);
			comboBoxGridAccount.MaxDropDownItems = 12;
			comboBoxGridAccount.Name = "comboBoxGridAccount";
			comboBoxGridAccount.ShowInactiveItems = false;
			comboBoxGridAccount.ShowQuickAdd = true;
			comboBoxGridAccount.Size = new System.Drawing.Size(106, 20);
			comboBoxGridAccount.TabIndex = 108;
			comboBoxGridAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridAccount.Visible = false;
			textBoxDocID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDocID.Location = new System.Drawing.Point(77, 46);
			textBoxDocID.Name = "textBoxDocID";
			textBoxDocID.ReadOnly = true;
			textBoxDocID.Size = new System.Drawing.Size(139, 20);
			textBoxDocID.TabIndex = 0;
			textBoxDocID.TabStop = false;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(9, 49);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(41, 13);
			label1.TabIndex = 170;
			label1.Text = "DocID:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(222, 49);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(47, 13);
			label2.TabIndex = 170;
			label2.Text = "Number:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(426, 49);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(49, 13);
			label4.TabIndex = 172;
			label4.Text = "Register:";
			textBoxRegister.Location = new System.Drawing.Point(485, 46);
			textBoxRegister.Name = "textBoxRegister";
			textBoxRegister.ReadOnly = true;
			textBoxRegister.Size = new System.Drawing.Size(139, 20);
			textBoxRegister.TabIndex = 2;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(645, 381);
			base.Controls.Add(label4);
			base.Controls.Add(textBoxRegister);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxDocID);
			base.Controls.Add(panelDetails);
			base.Controls.Add(comboBoxGridCostCenter);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxGridanalysis);
			base.Controls.Add(comboBoxGridAccount);
			base.Controls.Add(textBoxVoucherNumber);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "POSCashExpenseForm";
			Text = "Petty Cash Expense";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)posExpenseComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCostCenter).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridanalysis).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridAccount).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
