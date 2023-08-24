using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class IssuedChequeReturnForm : Form, IForm
	{
		private DataRow currentChequeRow;

		private TransactionData currentData;

		private const string TABLENAME_CONST = "GL_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string selectedChequeID = "";

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label label4;

		private ReturnedChequeReasonComboBox comboBoxReason;

		private AmountTextBox textBoxAmount;

		private flatDatePicker dateTimePickerDate;

		private TextBox textBoxPayeeName;

		private TextBox textBoxVoucherNumber;

		private TextBox textBoxRef1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private Label label1;

		private TextBox textBoxNote;

		private Label label3;

		private MMLabel mmLabel1;

		private Label labelVoided;

		private Label label7;

		private Label label5;

		private TextBox textBoxPayeeType;

		private TextBox textBoxPayeeCode;

		private TextBox textBoxReasonName;

		private Label label8;

		private TextBox textBoxChequeDate;

		private TextBox textBoxStatus;

		private Label label9;

		private MMLabel mmLabel2;

		private TextBox textBoxChequeNumber;

		private TextBox textBoxBankName;

		private UltraGroupBox ultraGroupBox1;

		private XPButton buttonSelectCheque;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraGroupBox ultraGroupBox2;

		private MMLabel mmLabel4;

		private XPButton xpButton2;

		private TextBox textBox2;

		private TextBox textBox3;

		private TextBox textBox4;

		private TextBox textBox5;

		private TextBox textBox6;

		private AmountTextBox amountTextBox1;

		private Label label11;

		private Label label12;

		private TextBox textBox7;

		private Label label13;

		private Label label14;

		private Label label15;

		private Label label2;

		private MMLabel mmLabel3;

		private FormManager formManager;

		private ToolStrip toolStrip2;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1022;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => comboBoxSysDoc.SelectedID;

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
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
					textBoxVoucherNumber.Enabled = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.Enabled = false;
				}
				toolStripButtonDistribution.Enabled = !value;
				comboBoxSysDoc.Enabled = value;
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
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
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
					buttonSave.Enabled = !value;
					panelDetails.Enabled = !value;
					labelVoided.Visible = value;
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

		public IssuedChequeReturnForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxSysDoc.FilterByType(SysDocTypes.IssuedChequeReturn);
		}

		private void AddEvents()
		{
			base.Load += TransactionLeavesForm_Load;
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
				dataRow["SysDocType"] = (byte)16;
				dataRow["SysDocID"] = SystemDocID;
				dataRow["ChequeID"] = selectedChequeID;
				dataRow["ReasonID"] = comboBoxReason.SelectedID;
				dataRow["Description"] = textBoxNote.Text;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.TransactionTable.Rows.Add(dataRow);
				}
				currentData.TransactionDetailsTable.Rows.Clear();
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

		public void LoadData(string journalID)
		{
			try
			{
				if (!(journalID.Trim() == "") && CanClose())
				{
					currentData = Factory.TransactionSystem.GetTransactionByID(SystemDocID, journalID);
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
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxNote.Text = dataRow["Description"].ToString();
					comboBoxReason.SelectedID = dataRow["ReasonID"].ToString();
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					if (dataRow["IsVoid"] != DBNull.Value)
					{
						IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
					}
					else
					{
						IsVoid = false;
					}
					if (dataRow["ChequeID"] != DBNull.Value)
					{
						int num = int.Parse(dataRow["ChequeID"].ToString());
						DataSet chequeByID = Factory.IssuedChequeSystem.GetChequeByID(num.ToString());
						DataRow dataRow2 = currentChequeRow = chequeByID.Tables[0].Rows[0];
						textBoxAmount.Text = decimal.Parse(dataRow2["Amount"].ToString()).ToString(Format.TotalAmountFormat);
						textBoxChequeNumber.Text = dataRow2["ChequeNumber"].ToString();
						textBoxPayeeType.Text = dataRow2["PayeeType"].ToString();
						textBoxPayeeType.Text = dataRow2["PayeeType"].ToString();
						textBoxPayeeCode.Text = dataRow2["PayeeID"].ToString();
						textBoxPayeeName.Text = dataRow2["PayeeName"].ToString();
						textBoxChequeDate.Text = DateTime.Parse(dataRow2["ChequeDate"].ToString()).ToShortDateString();
						IssuedChequeStatus issuedChequeStatus = (IssuedChequeStatus)byte.Parse(dataRow2["Status"].ToString());
						textBoxStatus.Text = issuedChequeStatus.ToString();
						selectedChequeID = num.ToString();
						textBoxBankName.Text = dataRow2["BankName"].ToString();
					}
					if (currentData.Tables.Contains("Transaction_Details") && currentData.TransactionDetailsTable.Rows.Count != 0)
					{
						DataRow dataRow3 = currentData.Tables["Transaction_Details"].Rows[0];
						if (dataRow3["AmountFC"] != DBNull.Value)
						{
							textBoxAmount.Text = Math.Round(decimal.Parse(dataRow3["AmountFC"].ToString()), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
						}
						else if (dataRow3["Amount"] != DBNull.Value)
						{
							textBoxAmount.Text = Math.Round(decimal.Parse(dataRow3["Amount"].ToString()), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
						}
						else
						{
							textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
						}
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
				bool flag = (!isNewRecord) ? Factory.TransactionSystem.ReturnIssuedCheque(currentData, isUpdate: true) : Factory.TransactionSystem.ReturnIssuedCheque(currentData, isUpdate: false);
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
				if (ex.Number == 1003)
				{
					ErrorHelper.ErrorMessage(UIMessages.ReturnedChequeError);
				}
				else
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
				}
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
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
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
			if (comboBoxSysDoc.SelectedID == "" || textBoxChequeNumber.Text == "" || textBoxVoucherNumber.Text == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			IssuedChequeStatus issuedChequeStatus = (IssuedChequeStatus)byte.Parse(currentChequeRow["Status"].ToString());
			if (isNewRecord && issuedChequeStatus == IssuedChequeStatus.Bounced)
			{
				ErrorHelper.WarningMessage("This cheque is already returned.");
				textBoxChequeNumber.Focus();
				return false;
			}
			if (isNewRecord && issuedChequeStatus != IssuedChequeStatus.Cleared)
			{
				ErrorHelper.WarningMessage("Only issued cheques that are in 'Cleared' status can be returned.");
				textBoxChequeNumber.Focus();
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

		private decimal GetTransactionBalance()
		{
			return 0m;
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			formManager.ShowApprovalPanel(approvalTaskID, "GL_Transaction", "VoucherID");
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
				textBoxRef1.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxPayeeName.Clear();
				textBoxPayeeType.Clear();
				textBoxPayeeCode.Clear();
				comboBoxReason.Clear();
				textBoxBankName.Clear();
				textBoxStatus.Clear();
				textBoxReasonName.Clear();
				textBoxChequeNumber.Clear();
				textBoxChequeDate.Clear();
				textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				selectedChequeID = "";
				IsNewRecord = true;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				IsVoid = false;
				formManager.ResetDirty();
				dateTimePickerDate.Focus();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void TransactionLeaveGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void TransactionLeaveGroupDetailsForm_Validated(object sender, EventArgs e)
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
			string nextID = DatabaseHelper.GetNextID("GL_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("GL_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("GL_Transaction", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("GL_Transaction", "VoucherID", "SysDocID", SystemDocID);
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

		private void TransactionLeavesForm_Load(object sender, EventArgs e)
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

		private void comboBoxBank_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxReason_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxReasonName.Text = comboBoxReason.SelectedName;
		}

		private void buttonSelectCheque_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "";
				SelectChequeDialog selectChequeDialog = new SelectChequeDialog();
				selectChequeDialog.DataSource = Factory.IssuedChequeSystem.GetIssuedChequeToReturnList();
				selectChequeDialog.Grid.DisplayLayout.Bands[0].Columns["ChequeID"].Hidden = true;
				selectChequeDialog.Grid.DisplayLayout.Bands[0].Columns["Cheque #"].Width = 75;
				selectChequeDialog.Grid.DisplayLayout.Bands[0].Columns["Bank"].Width = 121;
				selectChequeDialog.Grid.DisplayLayout.Bands[0].Columns["Payee"].Width = 193;
				selectChequeDialog.Grid.DisplayLayout.Bands[0].Columns["Amount"].Width = 91;
				if (selectChequeDialog.ShowDialog() == DialogResult.OK)
				{
					text = (selectedChequeID = selectChequeDialog.SelectedID);
					if (!(text == ""))
					{
						DataSet chequeByID = Factory.IssuedChequeSystem.GetChequeByID(text);
						DataRow dataRow = currentChequeRow = chequeByID.Tables[0].Rows[0];
						textBoxAmount.Text = decimal.Parse(dataRow["Amount"].ToString()).ToString(Format.TotalAmountFormat);
						textBoxChequeNumber.Text = dataRow["ChequeNumber"].ToString();
						textBoxPayeeType.Text = dataRow["PayeeType"].ToString();
						textBoxPayeeType.Text = dataRow["PayeeType"].ToString();
						textBoxPayeeCode.Text = dataRow["PayeeID"].ToString();
						textBoxPayeeName.Text = dataRow["PayeeName"].ToString();
						textBoxChequeDate.Text = DateTime.Parse(dataRow["ChequeDate"].ToString()).ToShortDateString();
						IssuedChequeStatus issuedChequeStatus = (IssuedChequeStatus)byte.Parse(dataRow["Status"].ToString());
						textBoxStatus.Text = issuedChequeStatus.ToString();
						textBoxBankName.Text = dataRow["BankName"].ToString();
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.IssuedChequeReturn);
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButton7_Click(object sender, EventArgs e)
		{
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

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData()))
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
						PrintHelper.PrintDocument(transactionToPrint, systemDocID, "Issued Cheque Return", SysDocTypes.IssuedChequeReturn, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.IssuedChequeBounceListFormObj);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditReturnedChequeReason(comboBoxReason.SelectedID);
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.IssuedChequeReturnForm));
			panelButtons = new System.Windows.Forms.Panel();
			panelDetails = new System.Windows.Forms.Panel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxReasonName = new System.Windows.Forms.TextBox();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label1 = new System.Windows.Forms.Label();
			textBoxNote = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			labelVoided = new System.Windows.Forms.Label();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxPayeeName = new System.Windows.Forms.TextBox();
			textBoxChequeNumber = new System.Windows.Forms.TextBox();
			textBoxPayeeCode = new System.Windows.Forms.TextBox();
			textBoxBankName = new System.Windows.Forms.TextBox();
			textBoxPayeeType = new System.Windows.Forms.TextBox();
			textBoxStatus = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			textBoxChequeDate = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			textBox2 = new System.Windows.Forms.TextBox();
			textBox3 = new System.Windows.Forms.TextBox();
			textBox4 = new System.Windows.Forms.TextBox();
			textBox5 = new System.Windows.Forms.TextBox();
			textBox6 = new System.Windows.Forms.TextBox();
			label11 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			textBox7 = new System.Windows.Forms.TextBox();
			label13 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			toolStrip2 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			formManager = new Micromind.DataControls.FormManager();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			comboBoxReason = new Micromind.DataControls.ReturnedChequeReasonComboBox();
			dateTimePickerDate = new Micromind.UISupport.flatDatePicker();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			xpButton2 = new Micromind.UISupport.XPButton();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			buttonSelectCheque = new Micromind.UISupport.XPButton();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			amountTextBox1 = new Micromind.UISupport.AmountTextBox();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			toolStrip2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReason).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 302);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(581, 40);
			panelButtons.TabIndex = 1;
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(comboBoxReason);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(textBoxReasonName);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(mmLabel3);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(labelVoided);
			panelDetails.Controls.Add(ultraGroupBox2);
			panelDetails.Location = new System.Drawing.Point(3, 35);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(574, 232);
			panelDetails.TabIndex = 0;
			appearance.FontData.BoldAsString = "False";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(11, 72);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(47, 15);
			ultraFormattedLinkLabel3.TabIndex = 145;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Reason :";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance3;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(11, 18);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel1.TabIndex = 144;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Doc ID:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance5;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 18);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 144;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance6;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			textBoxReasonName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxReasonName.Location = new System.Drawing.Point(243, 69);
			textBoxReasonName.Name = "textBoxReasonName";
			textBoxReasonName.ReadOnly = true;
			textBoxReasonName.Size = new System.Drawing.Size(319, 20);
			textBoxReasonName.TabIndex = 5;
			textBoxReasonName.TabStop = false;
			textBoxVoucherNumber.Location = new System.Drawing.Point(326, 14);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(130, 20);
			textBoxVoucherNumber.TabIndex = 1;
			textBoxRef1.Location = new System.Drawing.Point(325, 40);
			textBoxRef1.MaxLength = 15;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(130, 20);
			textBoxRef1.TabIndex = 3;
			appearance7.FontData.BoldAsString = "True";
			appearance7.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance7;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(243, 18);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabel2.TabIndex = 110;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Doc Number:";
			appearance8.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance8;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(247, 43);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxNote.Location = new System.Drawing.Point(87, 95);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(475, 20);
			textBoxNote.TabIndex = 6;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(11, 95);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(33, 13);
			label2.TabIndex = 20;
			label2.Text = "Note:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 95);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			labelVoided.BackColor = System.Drawing.Color.Transparent;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(1003, 91);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(91, 27);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox2.Controls.Add(mmLabel4);
			ultraGroupBox2.Controls.Add(xpButton2);
			ultraGroupBox2.Controls.Add(ultraGroupBox1);
			ultraGroupBox2.Controls.Add(textBox2);
			ultraGroupBox2.Controls.Add(textBox3);
			ultraGroupBox2.Controls.Add(textBox4);
			ultraGroupBox2.Controls.Add(textBox5);
			ultraGroupBox2.Controls.Add(textBox6);
			ultraGroupBox2.Controls.Add(amountTextBox1);
			ultraGroupBox2.Controls.Add(label11);
			ultraGroupBox2.Controls.Add(label12);
			ultraGroupBox2.Controls.Add(textBox7);
			ultraGroupBox2.Controls.Add(label13);
			ultraGroupBox2.Controls.Add(label14);
			ultraGroupBox2.Controls.Add(label15);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 120);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(559, 101);
			ultraGroupBox2.TabIndex = 7;
			ultraGroupBox2.Text = "Select the cheque to cancel";
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(mmLabel2);
			ultraGroupBox1.Controls.Add(buttonSelectCheque);
			ultraGroupBox1.Controls.Add(textBoxPayeeName);
			ultraGroupBox1.Controls.Add(textBoxChequeNumber);
			ultraGroupBox1.Controls.Add(textBoxPayeeCode);
			ultraGroupBox1.Controls.Add(textBoxBankName);
			ultraGroupBox1.Controls.Add(textBoxPayeeType);
			ultraGroupBox1.Controls.Add(textBoxStatus);
			ultraGroupBox1.Controls.Add(textBoxAmount);
			ultraGroupBox1.Controls.Add(label9);
			ultraGroupBox1.Controls.Add(label4);
			ultraGroupBox1.Controls.Add(textBoxChequeDate);
			ultraGroupBox1.Controls.Add(label5);
			ultraGroupBox1.Controls.Add(label8);
			ultraGroupBox1.Controls.Add(label7);
			ultraGroupBox1.Location = new System.Drawing.Point(1, 1);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(559, 101);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Select the cheque to cancel";
			textBoxPayeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPayeeName.Location = new System.Drawing.Point(241, 69);
			textBoxPayeeName.Name = "textBoxPayeeName";
			textBoxPayeeName.ReadOnly = true;
			textBoxPayeeName.Size = new System.Drawing.Size(308, 20);
			textBoxPayeeName.TabIndex = 7;
			textBoxPayeeName.TabStop = false;
			textBoxChequeNumber.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxChequeNumber.Location = new System.Drawing.Point(88, 25);
			textBoxChequeNumber.Name = "textBoxChequeNumber";
			textBoxChequeNumber.ReadOnly = true;
			textBoxChequeNumber.Size = new System.Drawing.Size(86, 20);
			textBoxChequeNumber.TabIndex = 0;
			textBoxChequeNumber.TabStop = false;
			textBoxPayeeCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPayeeCode.Location = new System.Drawing.Point(120, 69);
			textBoxPayeeCode.Name = "textBoxPayeeCode";
			textBoxPayeeCode.ReadOnly = true;
			textBoxPayeeCode.Size = new System.Drawing.Size(119, 20);
			textBoxPayeeCode.TabIndex = 6;
			textBoxPayeeCode.TabStop = false;
			textBoxBankName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankName.Location = new System.Drawing.Point(294, 25);
			textBoxBankName.Name = "textBoxBankName";
			textBoxBankName.ReadOnly = true;
			textBoxBankName.Size = new System.Drawing.Size(255, 20);
			textBoxBankName.TabIndex = 1;
			textBoxBankName.TabStop = false;
			textBoxPayeeType.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPayeeType.Location = new System.Drawing.Point(88, 69);
			textBoxPayeeType.Name = "textBoxPayeeType";
			textBoxPayeeType.ReadOnly = true;
			textBoxPayeeType.Size = new System.Drawing.Size(30, 20);
			textBoxPayeeType.TabIndex = 5;
			textBoxPayeeType.TabStop = false;
			textBoxStatus.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxStatus.Location = new System.Drawing.Point(452, 47);
			textBoxStatus.Name = "textBoxStatus";
			textBoxStatus.ReadOnly = true;
			textBoxStatus.Size = new System.Drawing.Size(97, 20);
			textBoxStatus.TabIndex = 4;
			textBoxStatus.TabStop = false;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(406, 50);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(40, 13);
			label9.TabIndex = 137;
			label9.Text = "Status:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(206, 29);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(35, 13);
			label4.TabIndex = 131;
			label4.Text = "Bank:";
			textBoxChequeDate.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxChequeDate.Location = new System.Drawing.Point(294, 47);
			textBoxChequeDate.Name = "textBoxChequeDate";
			textBoxChequeDate.ReadOnly = true;
			textBoxChequeDate.Size = new System.Drawing.Size(106, 20);
			textBoxChequeDate.TabIndex = 3;
			textBoxChequeDate.TabStop = false;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(1, 73);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(40, 13);
			label5.TabIndex = 133;
			label5.Text = "Payee:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(206, 50);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(73, 13);
			label8.TabIndex = 135;
			label8.Text = "Cheque Date:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(1, 50);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(46, 13);
			label7.TabIndex = 133;
			label7.Text = "Amount:";
			textBox2.BackColor = System.Drawing.Color.WhiteSmoke;
			textBox2.Location = new System.Drawing.Point(87, 25);
			textBox2.Name = "textBox2";
			textBox2.ReadOnly = true;
			textBox2.Size = new System.Drawing.Size(86, 20);
			textBox2.TabIndex = 140;
			textBox2.TabStop = false;
			textBox3.BackColor = System.Drawing.Color.WhiteSmoke;
			textBox3.Location = new System.Drawing.Point(120, 69);
			textBox3.Name = "textBox3";
			textBox3.ReadOnly = true;
			textBox3.Size = new System.Drawing.Size(119, 20);
			textBox3.TabIndex = 118;
			textBox3.TabStop = false;
			textBox4.BackColor = System.Drawing.Color.WhiteSmoke;
			textBox4.Location = new System.Drawing.Point(291, 19);
			textBox4.Name = "textBox4";
			textBox4.ReadOnly = true;
			textBox4.Size = new System.Drawing.Size(265, 20);
			textBox4.TabIndex = 139;
			textBox4.TabStop = false;
			textBox5.BackColor = System.Drawing.Color.WhiteSmoke;
			textBox5.Location = new System.Drawing.Point(88, 69);
			textBox5.Name = "textBox5";
			textBox5.ReadOnly = true;
			textBox5.Size = new System.Drawing.Size(30, 20);
			textBox5.TabIndex = 118;
			textBox5.TabStop = false;
			textBox6.BackColor = System.Drawing.Color.WhiteSmoke;
			textBox6.Location = new System.Drawing.Point(452, 47);
			textBox6.Name = "textBox6";
			textBox6.ReadOnly = true;
			textBox6.Size = new System.Drawing.Size(107, 20);
			textBox6.TabIndex = 138;
			textBox6.TabStop = false;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(406, 50);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(40, 13);
			label11.TabIndex = 137;
			label11.Text = "Status:";
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(206, 29);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(35, 13);
			label12.TabIndex = 131;
			label12.Text = "Bank:";
			textBox7.BackColor = System.Drawing.Color.WhiteSmoke;
			textBox7.Location = new System.Drawing.Point(294, 47);
			textBox7.Name = "textBox7";
			textBox7.ReadOnly = true;
			textBox7.Size = new System.Drawing.Size(106, 20);
			textBox7.TabIndex = 136;
			textBox7.TabStop = false;
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(1, 73);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(40, 13);
			label13.TabIndex = 133;
			label13.Text = "Payee:";
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(206, 50);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(73, 13);
			label14.TabIndex = 135;
			label14.Text = "Cheque Date:";
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(1, 50);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(46, 13);
			label15.TabIndex = 133;
			label15.Text = "Amount:";
			toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator3,
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator1,
				toolStripButtonDistribution,
				toolStripButtonInformation
			});
			toolStrip2.Location = new System.Drawing.Point(20, 0);
			toolStrip2.Name = "toolStrip2";
			toolStrip2.Size = new System.Drawing.Size(561, 31);
			toolStrip2.TabIndex = 18;
			toolStrip2.Text = "toolStrip2";
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
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
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
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(213, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 24);
			buttonVoid.TabIndex = 2;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Visible = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(315, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 3;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Visible = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(111, 8);
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
			linePanelDown.Size = new System.Drawing.Size(581, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(471, 8);
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
			buttonSave.Location = new System.Drawing.Point(9, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance9;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance10.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance10;
			appearance11.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance11;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance12.BackColor2 = System.Drawing.SystemColors.Control;
			appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance12.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance12;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance13;
			appearance14.BackColor = System.Drawing.SystemColors.Highlight;
			appearance14.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance14;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance15;
			appearance16.BorderColor = System.Drawing.Color.Silver;
			appearance16.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance16;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance17.BackColor = System.Drawing.SystemColors.Control;
			appearance17.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance17.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance17.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance17;
			appearance18.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance18;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance19;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance20;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(88, 13);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(151, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxReason.Assigned = false;
			comboBoxReason.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxReason.CustomReportFieldName = "";
			comboBoxReason.CustomReportKey = "";
			comboBoxReason.CustomReportValueType = 1;
			comboBoxReason.DescriptionTextBox = null;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxReason.DisplayLayout.Appearance = appearance21;
			comboBoxReason.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxReason.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance22.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance22.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.GroupByBox.Appearance = appearance22;
			appearance23.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReason.DisplayLayout.GroupByBox.BandLabelAppearance = appearance23;
			comboBoxReason.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance24.BackColor2 = System.Drawing.SystemColors.Control;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReason.DisplayLayout.GroupByBox.PromptAppearance = appearance24;
			comboBoxReason.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxReason.DisplayLayout.MaxRowScrollRegions = 1;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxReason.DisplayLayout.Override.ActiveCellAppearance = appearance25;
			appearance26.BackColor = System.Drawing.SystemColors.Highlight;
			appearance26.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxReason.DisplayLayout.Override.ActiveRowAppearance = appearance26;
			comboBoxReason.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxReason.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.Override.CardAreaAppearance = appearance27;
			appearance28.BorderColor = System.Drawing.Color.Silver;
			appearance28.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxReason.DisplayLayout.Override.CellAppearance = appearance28;
			comboBoxReason.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxReason.DisplayLayout.Override.CellPadding = 0;
			appearance29.BackColor = System.Drawing.SystemColors.Control;
			appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance29.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance29.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.Override.GroupByRowAppearance = appearance29;
			appearance30.TextHAlignAsString = "Left";
			comboBoxReason.DisplayLayout.Override.HeaderAppearance = appearance30;
			comboBoxReason.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxReason.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.Color.Silver;
			comboBoxReason.DisplayLayout.Override.RowAppearance = appearance31;
			comboBoxReason.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxReason.DisplayLayout.Override.TemplateAddRowAppearance = appearance32;
			comboBoxReason.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxReason.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxReason.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxReason.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxReason.Editable = true;
			comboBoxReason.FilterString = "";
			comboBoxReason.HasAllAccount = false;
			comboBoxReason.HasCustom = false;
			comboBoxReason.IsDataLoaded = false;
			comboBoxReason.Location = new System.Drawing.Point(87, 69);
			comboBoxReason.MaxDropDownItems = 12;
			comboBoxReason.Name = "comboBoxReason";
			comboBoxReason.ShowInactiveItems = false;
			comboBoxReason.ShowQuickAdd = true;
			comboBoxReason.Size = new System.Drawing.Size(151, 20);
			comboBoxReason.TabIndex = 4;
			comboBoxReason.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxReason.SelectedIndexChanged += new System.EventHandler(comboBoxReason_SelectedIndexChanged);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(87, 43);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(151, 20);
			dateTimePickerDate.TabIndex = 2;
			dateTimePickerDate.Value = new System.DateTime(2008, 5, 15, 12, 1, 4, 358);
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(11, 43);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(38, 13);
			mmLabel3.TabIndex = 2;
			mmLabel3.Text = "Date:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(15, 43);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(1, 28);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(80, 13);
			mmLabel4.TabIndex = 2;
			mmLabel4.Text = "Chq Number:";
			xpButton2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton2.BackColor = System.Drawing.Color.DarkGray;
			xpButton2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			xpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton2.Location = new System.Drawing.Point(175, 25);
			xpButton2.Name = "xpButton2";
			xpButton2.Size = new System.Drawing.Size(25, 20);
			xpButton2.TabIndex = 141;
			xpButton2.Text = "...";
			xpButton2.UseVisualStyleBackColor = false;
			xpButton2.Click += new System.EventHandler(buttonSelectCheque_Click);
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(1, 28);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(80, 13);
			mmLabel2.TabIndex = 2;
			mmLabel2.Text = "Chq Number:";
			buttonSelectCheque.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectCheque.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectCheque.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectCheque.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectCheque.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSelectCheque.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectCheque.Location = new System.Drawing.Point(175, 25);
			buttonSelectCheque.Name = "buttonSelectCheque";
			buttonSelectCheque.Size = new System.Drawing.Size(25, 20);
			buttonSelectCheque.TabIndex = 141;
			buttonSelectCheque.Text = "...";
			buttonSelectCheque.UseVisualStyleBackColor = false;
			buttonSelectCheque.Click += new System.EventHandler(buttonSelectCheque_Click);
			textBoxAmount.AllowDecimal = true;
			textBoxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.IsModified = false;
			textBoxAmount.Location = new System.Drawing.Point(88, 47);
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
			textBoxAmount.ReadOnly = true;
			textBoxAmount.Size = new System.Drawing.Size(112, 20);
			textBoxAmount.TabIndex = 2;
			textBoxAmount.TabStop = false;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			amountTextBox1.AllowDecimal = true;
			amountTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			amountTextBox1.CustomReportFieldName = "";
			amountTextBox1.CustomReportKey = "";
			amountTextBox1.CustomReportValueType = 1;
			amountTextBox1.IsComboTextBox = false;
			amountTextBox1.IsModified = false;
			amountTextBox1.Location = new System.Drawing.Point(88, 47);
			amountTextBox1.MaxLength = 15;
			amountTextBox1.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			amountTextBox1.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			amountTextBox1.Name = "amountTextBox1";
			amountTextBox1.NullText = "0";
			amountTextBox1.ReadOnly = true;
			amountTextBox1.Size = new System.Drawing.Size(112, 20);
			amountTextBox1.TabIndex = 7;
			amountTextBox1.TabStop = false;
			amountTextBox1.Text = "0.00";
			amountTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			amountTextBox1.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(581, 342);
			base.Controls.Add(toolStrip2);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(panelDetails);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(588, 238);
			base.Name = "IssuedChequeReturnForm";
			Text = "Issued Cheque Bounce";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			toolStrip2.ResumeLayout(false);
			toolStrip2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReason).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
