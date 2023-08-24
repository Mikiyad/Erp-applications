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
using Micromind.Common.Libraries;
using Micromind.DataControls;
using Micromind.DataControls.OtherControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class PaymentRequestForm : Form, IForm, IWorkFlowForm
	{
		private bool allowEdit = true;

		private PaymentRequestData currentData;

		private const string TABLENAME_CONST = "Payment_Request";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private decimal TotBalance;

		private decimal TotDue;

		private int totPC;

		private int totQC;

		private decimal balance;

		private decimal dueBalance;

		private decimal purchaseclaimamt;

		private decimal qualityclaimamt;

		private decimal AvailableBal;

		private string currentCustomerID;

		private bool isDataLoading;

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

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private CurrencySelector comboBoxCurrency;

		private Label labelCurrency;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem saveDraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private ComboBox comboBoxType;

		private Label label4;

		private TextBox textBoxPayeeName;

		private MMLabel mmLabel2;

		private AmountTextBox textBoxAmount;

		private ComboBox comboBoxReason;

		private Label label2;

		private MMLabel labelVendorBalance;

		private AmountTextBox textBoxTotalBalance;

		private MMLabel mmLabel4;

		private AmountTextBox textBoxTotalDue;

		private BankFacilityComboBox comboBoxFacility;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private ChequebookComboBox comboBoxChequebook;

		private TextBox textBoxPayFromDesc;

		private BankAccountsComboBox comboBoxBankAccount;

		private Label label5;

		private AmountTextBox textBoxPOAmount;

		private Label label6;

		private Label label7;

		private AmountTextBox textBoxPOPaid;

		private Label label8;

		private AmountTextBox textBoxPOBalance;

		private TextBox textBoxPOTerm;

		private Label label9;

		private Label label10;

		private TextBox textBoxTerm;

		private vendorsFlatComboBox comboBoxVendor;

		private Panel panelPO;

		private AmountTextBox textBoxAmountLC;

		private Label labelAmountLC;

		private MMLabel mmLabel5;

		private AmountTextBox textBoxBankBalance;

		private PaymentTermComboBox comboBoxVendorTerm;

		private PaymentTermComboBox comboBoxPOTerm;

		private TextBox textBoxPOSysDocID;

		private XPButton buttonSelectPO;

		private TextBox textBoxPOVoucherID;

		private Label labelVoided;

		private ComboBox comboBoxStatus;

		private Label label11;

		private DocStatusLabel docStatusLabel;

		private ToolStripButton toolStripButtonPrintTemplate;

		private AmountTextBox textBoxPaymentRequested;

		private ToolStripButton toolStripButtonInformation;

		private Label label13;

		private Label labelAmountReqLC;

		private Label labelAmountReqFC;

		private AmountTextBox textBoxPaymentRequestedFC;

		private MMLabel mmLabel3;

		private MMLabel mmLabel6;

		private BALinkLabel linklabelQC;

		private BALinkLabel linklabelPC;

		private Label textBoxTotalPurchaseClaim;

		private Label textBoxTotalQualityClaim;

		private RegisterComboBox comboBoxRegister;

		private Panel panelTR;

		private Label label18;

		private Label label17;

		private Label label16;

		private Label label15;

		private Label label14;

		private Label labelInvnos;

		private TextBox textBoxAuthorizedby;

		private TextBox textBoxInvoiceNo;

		private NumberTextBox textBoxNoofPLs;

		private NumberTextBox textBoxNoofBOLs;

		private NumberTextBox textBoxNoofInvoices;

		private TextBox textBoxNoofGoods;

		private TextBox dateTimePickerETA;

		private XPButton buttonSelectInvoice;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2011;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		public string CurrentCustomerID
		{
			get
			{
				return currentCustomerID;
			}
			set
			{
				currentCustomerID = value;
			}
		}

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
					CurrencySelector currencySelector = comboBoxCurrency;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					bool flag3 = textBoxVoucherNumber.Enabled = true;
					enabled = (sysDocComboBox.Enabled = flag3);
					currencySelector.Enabled = enabled;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					if (!IsVoid)
					{
						buttonVoid.Enabled = true;
					}
					buttonSelectInvoice.Enabled = false;
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					enabled = (textBoxVoucherNumber.Enabled = false);
					sysDocComboBox2.Enabled = enabled;
				}
				toolStripButtonAttach.Enabled = !value;
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
				else if (!isVoid)
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
				buttonSave.Enabled = !value;
				textBoxNote.Enabled = !value;
				if (value)
				{
					labelVoided.Visible = true;
					buttonVoid.Enabled = false;
					return;
				}
				labelVoided.Visible = false;
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

		public PaymentRequestForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxFacility.FilterFacilityType = BankFacilityTypes.TR;
			comboBoxType.SelectedIndex = 0;
			comboBoxType.SelectedIndexChanged += comboBoxType_SelectedIndexChanged;
			comboBoxReason.SelectedIndex = 1;
			comboBoxReason.SelectedIndexChanged += comboBoxReason_SelectedIndexChanged;
			panelPO.Visible = false;
			comboBoxStatus.SelectedIndex = 0;
			comboBoxChequebook.SelectedIndexChanged += comboBoxBank_SelectedIndexChanged;
			comboBoxBankAccount.SelectedIndexChanged += comboBoxBank_SelectedIndexChanged;
			comboBoxFacility.SelectedIndexChanged += comboBoxBank_SelectedIndexChanged;
			comboBoxRegister.SelectedIndexChanged += comboBoxBank_SelectedIndexChanged;
			comboBoxVendor.SelectedIndexChanged += comboBoxVendor_SelectedIndexChanged;
			textBoxAmount.TextChanged += textBoxAmount_TextChanged;
			comboBoxCurrency.SelectedIndexChanged += comboBoxCurrency_SelectedIndexChanged;
			comboBoxType_SelectedIndexChanged(null, null);
			labelAmountLC.Text = "Amount (" + Global.BaseCurrencyID + "):";
			labelAmountReqLC.Text = "Payment Requested(" + Global.BaseCurrencyID + "):";
		}

		private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
		{
			CalculateBaseAmount();
		}

		private void textBoxAmount_TextChanged(object sender, EventArgs e)
		{
			CalculateBaseAmount();
		}

		private void CalculateBaseAmount()
		{
			decimal result = default(decimal);
			decimal.TryParse(textBoxAmount.Text, out result);
			if (comboBoxCurrency.IsBaseCurrency || comboBoxCurrency.SelectedID == "")
			{
				textBoxAmountLC.Text = result.ToString(Format.TotalAmountFormat);
			}
			else
			{
				textBoxAmountLC.Text = comboBoxCurrency.GetBaseEquivalant(result).ToString(Format.TotalAmountFormat);
			}
		}

		private void comboBoxVendor_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				textBoxPOVoucherID.Clear();
				textBoxPOSysDocID.Clear();
				string text = "";
				string text2 = "";
				string selectedID = comboBoxVendor.SelectedID;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Vendor", "CurrencyID", "VendorID", selectedID);
				object fieldValue2 = Factory.DatabaseSystem.GetFieldValue("Vendor", "PaymentTermID", "VendorID", selectedID);
				DataSet vendorDueBalanceSummary = Factory.VendorSystem.GetVendorDueBalanceSummary(selectedID, fieldValue.ToString(), dateTimePickerDate.Value);
				if (vendorDueBalanceSummary != null && vendorDueBalanceSummary.Tables[0].Rows.Count > 0)
				{
					balance = decimal.Parse(vendorDueBalanceSummary.Tables[0].Rows[0]["Balance"].ToString());
					dueBalance = decimal.Parse(vendorDueBalanceSummary.Tables[0].Rows[0]["BalanceDue"].ToString());
					purchaseclaimamt = decimal.Parse(vendorDueBalanceSummary.Tables[0].Rows[0]["Purchaseclaimamt"].ToString());
					qualityclaimamt = decimal.Parse(vendorDueBalanceSummary.Tables[0].Rows[0]["Qualityclaimamt"].ToString());
					text = vendorDueBalanceSummary.Tables[0].Rows[0]["PCCount"].ToString();
					text2 = vendorDueBalanceSummary.Tables[0].Rows[0]["QCCount"].ToString();
				}
				TotBalance = balance;
				TotDue = dueBalance;
				if (isNewRecord)
				{
					textBoxTotalBalance.Text = balance.ToString(Format.TotalAmountFormat);
				}
				textBoxTotalDue.Text = dueBalance.ToString(Format.TotalAmountFormat);
				textBoxTotalQualityClaim.Text = qualityclaimamt.ToString(Format.TotalAmountFormat);
				textBoxTotalPurchaseClaim.Text = purchaseclaimamt.ToString(Format.TotalAmountFormat);
				totPC = int.Parse(text);
				totQC = int.Parse(text2);
				linklabelQC.Text = "(" + text2 + ")";
				linklabelPC.Text = "(" + text + ")";
				labelVendorBalance.Text = "Balance: (" + fieldValue + ")";
				comboBoxVendorTerm.SelectedID = fieldValue2.ToString();
				labelAmountReqFC.Text = "(" + fieldValue + "):";
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				if (vendorDueBalanceSummary != null && vendorDueBalanceSummary.Tables[1].Rows.Count > 0)
				{
					num = decimal.Parse(vendorDueBalanceSummary.Tables[1].Rows[0]["PaymentRequested"].ToString());
					num2 = decimal.Parse(vendorDueBalanceSummary.Tables[1].Rows[0]["PaymentRequestedFC"].ToString());
					textBoxPaymentRequested.Text = num.ToString(Format.TotalAmountFormat);
					textBoxPaymentRequestedFC.Text = num2.ToString(Format.TotalAmountFormat);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxBank_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				decimal availableBal = default(decimal);
				if (comboBoxType.SelectedIndex == 0)
				{
					availableBal = Factory.ChequebookSystem.GetChequebookBalance(comboBoxChequebook.SelectedID, includeOD: true);
				}
				else if (comboBoxType.SelectedIndex == 1)
				{
					availableBal = Factory.CompanyAccountSystem.GetAccountBalance(comboBoxBankAccount.SelectedID, includeOD: true);
				}
				else if (comboBoxType.SelectedIndex == 2)
				{
					availableBal = Factory.BankFacilitySystem.GetBankFacilityAvailableLimit(comboBoxFacility.SelectedID);
				}
				else if (comboBoxType.SelectedIndex == 3)
				{
					availableBal = Factory.RegisterSystem.GetRegisterBalance(comboBoxRegister.SelectedID, "CashAccountID");
				}
				AvailableBal = availableBal;
				textBoxBankBalance.Text = availableBal.ToString(Format.TotalAmountFormat);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxReason_SelectedIndexChanged(object sender, EventArgs e)
		{
			panelPO.Visible = (comboBoxReason.SelectedIndex == 0);
		}

		private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			comboBoxChequebook.Visible = (comboBoxType.SelectedIndex == 0);
			comboBoxBankAccount.Visible = (comboBoxType.SelectedIndex == 1);
			comboBoxFacility.Visible = (comboBoxType.SelectedIndex == 2);
			comboBoxRegister.Visible = (comboBoxType.SelectedIndex == 3);
			comboBoxChequebook.Clear();
			comboBoxBankAccount.Clear();
			comboBoxFacility.Clear();
			comboBoxRegister.Clear();
			if (comboBoxType.SelectedIndex == 2)
			{
				panelTR.Enabled = true;
				textBoxInvoiceNo.Enabled = true;
				textBoxInvoiceNo.MaxLength = 1000;
			}
			else
			{
				panelTR.Enabled = false;
				textBoxInvoiceNo.Enabled = false;
				textBoxInvoiceNo.MaxLength = 1000;
			}
		}

		private void AddEvents()
		{
			base.Load += Form_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += PaymentRequestForm_KeyDown;
		}

		private void PaymentRequestForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
		{
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

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private bool GetData()
		{
			checked
			{
				try
				{
					if (currentData == null || isNewRecord)
					{
						currentData = new PaymentRequestData();
					}
					DataRow dataRow = (!isNewRecord) ? currentData.PaymentRequestTable.Rows[0] : currentData.PaymentRequestTable.NewRow();
					dataRow["TransactionDate"] = dateTimePickerDate.Value;
					dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow["VoucherID"] = textBoxVoucherNumber.Text;
					if (textBoxPOVoucherID.Text != "")
					{
						dataRow["POSysDocID"] = textBoxPOSysDocID.Text;
						dataRow["POVoucherID"] = textBoxPOVoucherID.Text;
					}
					else
					{
						dataRow["POSysDocID"] = DBNull.Value;
						dataRow["POVoucherID"] = DBNull.Value;
					}
					dataRow["PLSysDocID"] = DBNull.Value;
					dataRow["PLVoucherID"] = DBNull.Value;
					dataRow["TransactionDate"] = dateTimePickerDate.Value;
					dataRow["Reason"] = comboBoxReason.SelectedIndex + 1;
					dataRow["TypeID"] = comboBoxType.SelectedIndex + 1;
					dataRow["Status"] = comboBoxStatus.SelectedIndex + 1;
					dataRow["PayeeType"] = "V";
					dataRow["PayeeID"] = comboBoxVendor.SelectedID;
					if (comboBoxType.SelectedIndex == 0)
					{
						dataRow["PayFromID"] = comboBoxChequebook.SelectedID;
					}
					else if (comboBoxType.SelectedIndex == 1)
					{
						dataRow["PayFromID"] = comboBoxBankAccount.SelectedID;
					}
					else if (comboBoxType.SelectedIndex == 2)
					{
						dataRow["PayFromID"] = comboBoxFacility.SelectedID;
					}
					else if (comboBoxType.SelectedIndex == 3)
					{
						dataRow["PayFromID"] = comboBoxRegister.SelectedID;
					}
					dataRow["Reference"] = textBoxRef1.Text;
					dataRow["Note"] = textBoxNote.Text;
					dataRow["AmountFC"] = textBoxAmount.Text;
					dataRow["Amount"] = textBoxAmountLC.Text;
					dataRow["AvailableBal"] = AvailableBal;
					dataRow["InvoiceNos"] = textBoxInvoiceNo.Text;
					dataRow["Authorizedby"] = textBoxAuthorizedby.Text;
					dataRow["NoofInvoices"] = textBoxNoofInvoices.Text;
					dataRow["NoofPL"] = textBoxNoofPLs.Text;
					dataRow["NoofBOL"] = textBoxNoofBOLs.Text;
					dataRow["NoofGoods"] = textBoxNoofGoods.Text;
					decimal num = default(decimal);
					decimal num2 = default(decimal);
					num = balance;
					num2 = textBoxAmount.Value;
					if (num > 0m)
					{
						dataRow["CurrentBal"] = num - num2;
					}
					else
					{
						dataRow["CurrentBal"] = num + num2;
					}
					if (textBoxPaymentRequested.Text != "")
					{
						dataRow["PaymentRequested"] = textBoxPaymentRequested.Text;
					}
					else
					{
						dataRow["PaymentRequested"] = 0;
					}
					if (textBoxPaymentRequestedFC.Text != "")
					{
						dataRow["PaymentRequestedFC"] = textBoxPaymentRequestedFC.Text;
					}
					else
					{
						dataRow["PaymentRequestedFC"] = 0;
					}
					if (comboBoxCurrency.SelectedID != "")
					{
						dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
					}
					else
					{
						dataRow["CurrencyID"] = DBNull.Value;
					}
					dataRow.EndEdit();
					if (IsNewRecord)
					{
						currentData.PaymentRequestTable.Rows.Add(dataRow);
					}
					return true;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return false;
				}
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
					currentData = Factory.PaymentRequestSystem.GetPaymentRequestByID(SystemDocID, voucherID);
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
			checked
			{
				try
				{
					isDataLoading = true;
					if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables["Payment_Request"].Rows.Count != 0)
					{
						DataRow dataRow = currentData.Tables["Payment_Request"].Rows[0];
						dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
						textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
						textBoxRef1.Text = dataRow["Reference"].ToString();
						textBoxNote.Text = dataRow["Note"].ToString();
						comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
						comboBoxStatus.SelectedIndex = int.Parse(dataRow["Status"].ToString()) - 1;
						int num = int.Parse(dataRow["TypeID"].ToString()) - 1;
						comboBoxType.SelectedIndex = num;
						switch (num)
						{
						case 0:
							comboBoxChequebook.SelectedID = dataRow["PayFromID"].ToString();
							break;
						case 1:
							comboBoxBankAccount.SelectedID = dataRow["PayFromID"].ToString();
							break;
						case 2:
							comboBoxFacility.SelectedID = dataRow["PayFromID"].ToString();
							break;
						case 3:
							comboBoxRegister.SelectedID = dataRow["PayFromID"].ToString();
							break;
						}
						comboBoxVendor.SelectedID = dataRow["PayeeID"].ToString();
						if (!Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["AmountFC"]))
						{
							textBoxAmount.Text = decimal.Parse(dataRow["AmountFC"].ToString()).ToString(Format.TotalAmountFormat);
						}
						else
						{
							textBoxAmount.Text = decimal.Parse(dataRow["Amount"].ToString()).ToString(Format.TotalAmountFormat);
						}
						comboBoxReason.SelectedIndex = int.Parse(dataRow["Reason"].ToString()) - 1;
						textBoxPOSysDocID.Text = dataRow["POSysDocID"].ToString();
						textBoxPOVoucherID.Text = dataRow["POVoucherID"].ToString();
						textBoxTotalBalance.Text = dataRow["CurrentBal"].ToString();
						textBoxInvoiceNo.Text = dataRow["InvoiceNos"].ToString();
						textBoxAuthorizedby.Text = dataRow["Authorizedby"].ToString();
						textBoxNoofInvoices.Text = dataRow["NoofInvoices"].ToString();
						textBoxNoofPLs.Text = dataRow["NoofPL"].ToString();
						textBoxNoofBOLs.Text = dataRow["NoofBOL"].ToString();
						textBoxNoofGoods.Text = dataRow["NoofGoods"].ToString();
						if (dataRow["IsVoid"] != DBNull.Value)
						{
							IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
						}
						else
						{
							IsVoid = false;
						}
						FillPOData();
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
			if (!allowEdit)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "You cannot edit this transfer transaction because it is already accepted or rejected.", "Document is in use.");
				return false;
			}
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			bool flag = false;
			try
			{
				bool flag2 = Factory.PaymentRequestSystem.CreatePaymentRequest(currentData, !isNewRecord);
				if (flag2)
				{
					flag = true;
				}
				if (!flag2)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					bool result = false;
					bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("PrintAfterSave").ToString(), out result);
					if (result)
					{
						Print(isPrint: true, showPrintDialog: true, saveChanges: false);
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
				return flag2;
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
			finally
			{
				if (flag)
				{
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
				if (!dataRow.Table.Columns.Contains("ApprovalStatus") || Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["ApprovalStatus"]))
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
			formManager.ShowApprovalPanel(approvalTaskID, "Payment_Request", "VoucherID");
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Payment_Request", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
			if (!IsNewRecord && Factory.PaymentRequestSystem.OrderHasShippedQuantity(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.ErrorMessage("Some items in this order has been already shipped. You are not able to modify.");
				return false;
			}
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (panelPO.Visible && textBoxPOVoucherID.Text == "")
			{
				ErrorHelper.InformationMessage("Please select a Purchase Order.");
				return false;
			}
			if (comboBoxVendor.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a payee.");
				return false;
			}
			if ((comboBoxType.SelectedIndex == 0 && comboBoxChequebook.SelectedID == "") || (comboBoxType.SelectedIndex == 1 && comboBoxBankAccount.SelectedID == "") || (comboBoxType.SelectedIndex == 2 && comboBoxFacility.SelectedID == ""))
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Payment_Request", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				allowEdit = true;
				textBoxNote.Clear();
				textBoxRef1.Clear();
				comboBoxVendor.Clear();
				comboBoxChequebook.Clear();
				comboBoxFacility.Clear();
				comboBoxBankAccount.Clear();
				comboBoxRegister.Clear();
				textBoxTotalDue.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxAmountLC.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxBankBalance.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxPOBalance.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotalBalance.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxPOAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxPOSysDocID.Clear();
				textBoxPOVoucherID.Clear();
				comboBoxStatus.SelectedIndex = 0;
				textBoxPOTerm.Clear();
				textBoxTerm.Clear();
				textBoxPaymentRequested.Clear();
				textBoxPaymentRequestedFC.Clear();
				textBoxTotalPurchaseClaim.Text = "0.0";
				textBoxTotalQualityClaim.Text = "0.0";
				textBoxPOPaid.Text = "0.0";
				textBoxInvoiceNo.Clear();
				textBoxAuthorizedby.Clear();
				textBoxNoofInvoices.Text = "0";
				textBoxNoofBOLs.Text = "0";
				textBoxNoofGoods.Clear();
				textBoxNoofPLs.Text = "0";
				linklabelQC.Text = "";
				linklabelPC.Text = "";
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				SetApprovalStatus();
				IsVoid = false;
				formManager.ResetDirty();
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
				if (!allowEdit)
				{
					ErrorHelper.InformationMessage(Application.ProductName, "You cannot delete this transfer transaction because it is already accepted or rejected.", "Document is in use.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.PaymentRequestSystem.DeletePaymentRequest(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Payment_Request", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Payment_Request", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Payment_Request", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Payment_Request", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Payment_Request", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
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

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				Label label = labelCurrency;
				bool visible = comboBoxCurrency.Visible = CompanyPreferences.UseMultiCurrency;
				label.Visible = visible;
				comboBoxSysDoc.FilterByType(SysDocTypes.PaymentRequest);
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
				return Factory.PaymentRequestSystem.VoidPaymentRequest(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.PaymentRequest);
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to duplicate this document?") == DialogResult.Yes)
			{
				string text = textBoxVoucherNumber.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			duplicateToolStripMenuItem.Enabled = !IsNewRecord;
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
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet paymentRequestToPrint = Factory.PaymentRequestSystem.GetPaymentRequestToPrint(selectedID, text);
					if (paymentRequestToPrint == null || paymentRequestToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						foreach (DataRow row in paymentRequestToPrint.Tables[0].Rows)
						{
							row["AvailableBal"] = AvailableBal;
							row["PayeeBalance"] = TotBalance;
							row["PayeeTotalDue"] = TotDue;
							row["PurchaseClaim"] = textBoxTotalPurchaseClaim.Text;
							row["QualityClaim"] = textBoxTotalQualityClaim.Text;
							row["PCCount"] = totPC;
							row["QCCount"] = totQC;
						}
						PrintHelper.PrintDocument(paymentRequestToPrint, selectedID, "Payment Request", SysDocTypes.PaymentRequest, isPrint, showPrintDialog);
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
			if (!comboBoxSysDoc.Enabled && sysDocID != comboBoxSysDoc.SelectedID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID);
		}

		private void transferToPaymentRequestToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.PaymentRequestListFormObj);
		}

		private void saveDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (GetData())
				{
					EnterNameDialog enterNameDialog = new EnterNameDialog();
					if (enterNameDialog.ShowDialog() == DialogResult.OK)
					{
						Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.PaymentRequest);
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private bool LoadDraft()
		{
			try
			{
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 100.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.PaymentRequest);
					currentData = (dataSet as PaymentRequestData);
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
			LoadDraft();
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

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			DocumentInformationDialog documentInformationDialog = new DocumentInformationDialog();
			documentInformationDialog.VoucherID = textBoxVoucherNumber.Text;
			documentInformationDialog.SysDocID = comboBoxSysDoc.SelectedID;
			documentInformationDialog.ShowDialog(this);
		}

		private void buttonSelectPO_Click(object sender, EventArgs e)
		{
			try
			{
				if (comboBoxVendor.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Please select a vendor.");
				}
				else
				{
					SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
					selectDocumentDialog.Text = "Select Purchase Order";
					selectDocumentDialog.DataSource = Factory.PurchaseOrderSystem.GetPOListForPayment(comboBoxVendor.SelectedID);
					if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
					{
						textBoxPOSysDocID.Text = selectDocumentDialog.SelectedRow.Cells["SysDocID"].Value.ToString();
						textBoxPOVoucherID.Text = selectDocumentDialog.SelectedRow.Cells["VoucherID"].Value.ToString();
						FillPOData();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void FillPOData()
		{
			try
			{
				if (!(textBoxPOVoucherID.Text == ""))
				{
					DataSet pOPaymentSummary = Factory.PurchaseOrderSystem.GetPOPaymentSummary(textBoxPOSysDocID.Text, textBoxPOVoucherID.Text, SystemDocID, textBoxVoucherNumber.Text);
					if (pOPaymentSummary != null && pOPaymentSummary.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = pOPaymentSummary.Tables[0].Rows[0];
						decimal d = decimal.Parse(dataRow["Total"].ToString());
						decimal d2 = decimal.Parse(dataRow["PaidAmount"].ToString());
						textBoxPOAmount.Text = d.ToString(Format.TotalAmountFormat);
						textBoxPOPaid.Text = d2.ToString(Format.TotalAmountFormat);
						textBoxPOBalance.Text = (d - d2).ToString(Format.TotalAmountFormat);
						comboBoxPOTerm.SelectedID = dataRow["TermID"].ToString();
						if (dataRow["ETA"].ToString() != "" && dataRow["ETA"].ToString() != string.Empty)
						{
							DateTime dateTime = DateTime.Parse(dataRow["ETA"].ToString());
							dateTimePickerETA.Text = dateTime.ToShortDateString();
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			FormHelper formHelper = new FormHelper();
			if (comboBoxChequebook.Visible)
			{
				formHelper.EditChequebook(comboBoxChequebook.SelectedID);
			}
			else if (comboBoxBankAccount.Visible)
			{
				formHelper.EditAccount(comboBoxBankAccount.SelectedID);
			}
			else
			{
				formHelper.EditBankFacility(comboBoxFacility.SelectedID);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(comboBoxVendor.SelectedID);
		}

		private void comboBoxType_SelectedValueChanged(object sender, EventArgs e)
		{
			if (comboBoxType.Text.Contains("TR"))
			{
				toolStripButtonPrintTemplate.Enabled = true;
			}
			else
			{
				toolStripButtonPrintTemplate.Enabled = false;
			}
		}

		private void SelectInvoicesToPay(string vendorID, string vendorName, string currencyID)
		{
			try
			{
				if (vendorID != "")
				{
					PaymentAdviceDetailsForm paymentAdviceDetailsForm = new PaymentAdviceDetailsForm();
					paymentAdviceDetailsForm.IsARPayment = false;
					paymentAdviceDetailsForm.SetData(vendorID, vendorName, currencyID, comboBoxCurrency.Rate);
					if (paymentAdviceDetailsForm.ShowDialog() == DialogResult.OK)
					{
						textBoxAmount.Tag = paymentAdviceDetailsForm.PaymentData;
						decimal d = default(decimal);
						foreach (DataRow row in paymentAdviceDetailsForm.PaymentData.Tables["AP_Payment_Advice"].Rows)
						{
							d += decimal.Parse(row["PaymentAmount"].ToString());
						}
						textBoxAmount.Text = d.ToString(Format.TotalAmountFormat);
						if (d > 0m)
						{
							textBoxAmount.ReadOnly = true;
						}
					}
				}
				else
				{
					ErrorHelper.InformationMessage("Please select a vendor.");
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonSelectInvoice_Click(object sender, EventArgs e)
		{
			string text = "";
			text = comboBoxVendor.SelectedID;
			SelectInvoicesToPay(text, comboBoxVendor.SelectedName, comboBoxCurrency.SelectedID);
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void toolStripButtonPrintTemplate_Click(object sender, EventArgs e)
		{
			PrintTRApplicationTemplate(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void PrintTRApplicationTemplate(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet paymentRequestToPrintTR = Factory.PaymentRequestSystem.GetPaymentRequestToPrintTR(selectedID, text);
					if (paymentRequestToPrintTR == null || paymentRequestToPrintTR.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						DataRow dataRow = paymentRequestToPrintTR.Tables[0].Rows[0];
						dataRow["AmountInWords"] = NumToWord.GetNumInWords(decimal.Parse(textBoxAmount.Text));
						dataRow.EndEdit();
						PrintHelper.PrintDocument(paymentRequestToPrintTR, selectedID, paymentRequestToPrintTR.Tables[0].Rows[0]["TemplateName"].ToString(), SysDocTypes.PaymentRequest, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonInformation_Click_1(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void baLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DataSet openQualityClaims = Factory.QualityClaimSystem.GetOpenQualityClaims(comboBoxVendor.SelectedID);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openQualityClaims;
			selectDocumentDialog.Text = "Select Quality Claim";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				string sysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				string text = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				FormActivator.BringFormToFront(FormActivator.QualityClaimFormObj);
				if (text != "")
				{
					FormActivator.QualityClaimFormObj.EditDocument(sysDocID, text);
				}
			}
		}

		private void baLinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DataSet openPurchaseClaims = Factory.PurchaseClaimSystem.GetOpenPurchaseClaims(comboBoxVendor.SelectedID);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openPurchaseClaims;
			selectDocumentDialog.Text = "Select Purchase Claim";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				string sysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				string text = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				FormActivator.BringFormToFront(FormActivator.PurchaseClaimFormObj);
				if (text != "")
				{
					FormActivator.PurchaseClaimFormObj.EditDocument(sysDocID, text);
				}
			}
		}

		private void buttonSelectPL_Click(object sender, EventArgs e)
		{
			try
			{
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.Text = "Select BOL";
				selectDocumentDialog.DataSource = Factory.POShipmentSystem.GetBOLListForPayment(POMultipleBOL: false);
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					textBoxPOSysDocID.Text = selectDocumentDialog.SelectedRow.Cells["SysDocID"].Value.ToString();
					textBoxPOVoucherID.Text = selectDocumentDialog.SelectedRow.Cells["VoucherID"].Value.ToString();
					FillPOData();
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.PaymentRequestForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			saveDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonApproval = new System.Windows.Forms.ToolStripButton();
			toolStripLabelApproval = new System.Windows.Forms.ToolStripLabel();
			toolStripSeparatorApproval = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrintTemplate = new System.Windows.Forms.ToolStripButton();
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
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelCurrency = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			comboBoxType = new System.Windows.Forms.ComboBox();
			label4 = new System.Windows.Forms.Label();
			textBoxPayeeName = new System.Windows.Forms.TextBox();
			comboBoxReason = new System.Windows.Forms.ComboBox();
			label2 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxPayFromDesc = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			textBoxPOTerm = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			textBoxTerm = new System.Windows.Forms.TextBox();
			panelPO = new System.Windows.Forms.Panel();
			dateTimePickerETA = new System.Windows.Forms.TextBox();
			label13 = new System.Windows.Forms.Label();
			textBoxPOSysDocID = new System.Windows.Forms.TextBox();
			buttonSelectPO = new Micromind.UISupport.XPButton();
			textBoxPOVoucherID = new System.Windows.Forms.TextBox();
			textBoxPOBalance = new Micromind.UISupport.AmountTextBox();
			textBoxPOPaid = new Micromind.UISupport.AmountTextBox();
			textBoxPOAmount = new Micromind.UISupport.AmountTextBox();
			comboBoxPOTerm = new Micromind.DataControls.PaymentTermComboBox();
			labelAmountLC = new System.Windows.Forms.Label();
			labelVoided = new System.Windows.Forms.Label();
			comboBoxStatus = new System.Windows.Forms.ComboBox();
			label11 = new System.Windows.Forms.Label();
			labelAmountReqLC = new System.Windows.Forms.Label();
			labelAmountReqFC = new System.Windows.Forms.Label();
			textBoxTotalPurchaseClaim = new System.Windows.Forms.Label();
			textBoxTotalQualityClaim = new System.Windows.Forms.Label();
			comboBoxRegister = new Micromind.DataControls.RegisterComboBox();
			linklabelPC = new Micromind.UISupport.BALinkLabel();
			linklabelQC = new Micromind.UISupport.BALinkLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxPaymentRequestedFC = new Micromind.UISupport.AmountTextBox();
			textBoxPaymentRequested = new Micromind.UISupport.AmountTextBox();
			docStatusLabel = new Micromind.DataControls.OtherControls.DocStatusLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxBankBalance = new Micromind.UISupport.AmountTextBox();
			textBoxAmountLC = new Micromind.UISupport.AmountTextBox();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxTotalDue = new Micromind.UISupport.AmountTextBox();
			labelVendorBalance = new Micromind.UISupport.MMLabel();
			textBoxTotalBalance = new Micromind.UISupport.AmountTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
			formManager = new Micromind.DataControls.FormManager();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			comboBoxVendorTerm = new Micromind.DataControls.PaymentTermComboBox();
			comboBoxBankAccount = new Micromind.DataControls.BankAccountsComboBox();
			comboBoxChequebook = new Micromind.DataControls.ChequebookComboBox();
			comboBoxFacility = new Micromind.DataControls.BankFacilityComboBox();
			panelTR = new System.Windows.Forms.Panel();
			textBoxNoofGoods = new System.Windows.Forms.TextBox();
			textBoxNoofPLs = new Micromind.UISupport.NumberTextBox();
			textBoxNoofBOLs = new Micromind.UISupport.NumberTextBox();
			label18 = new System.Windows.Forms.Label();
			label17 = new System.Windows.Forms.Label();
			label16 = new System.Windows.Forms.Label();
			textBoxNoofInvoices = new Micromind.UISupport.NumberTextBox();
			label15 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			textBoxAuthorizedby = new System.Windows.Forms.TextBox();
			labelInvnos = new System.Windows.Forms.Label();
			textBoxInvoiceNo = new System.Windows.Forms.TextBox();
			buttonSelectInvoice = new Micromind.UISupport.XPButton();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			panelPO.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPOTerm).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRegister).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendorTerm).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBankAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxChequebook).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFacility).BeginInit();
			panelTR.SuspendLayout();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[21]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator5,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator7,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator4,
				toolStripDropDownButton1,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval,
				toolStripButtonPrintTemplate,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(646, 31);
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
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				duplicateToolStripMenuItem,
				toolStripSeparator6,
				saveDraftToolStripMenuItem,
				loadDraftToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			duplicateToolStripMenuItem.Text = "Duplicate";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(138, 6);
			saveDraftToolStripMenuItem.Name = "saveDraftToolStripMenuItem";
			saveDraftToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			saveDraftToolStripMenuItem.Text = "Save as Draft";
			saveDraftToolStripMenuItem.Click += new System.EventHandler(saveDraftToolStripMenuItem_Click);
			loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
			loadDraftToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			loadDraftToolStripMenuItem.Text = "Load Draft...";
			loadDraftToolStripMenuItem.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
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
			toolStripSeparatorApproval.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrintTemplate.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonPrintTemplate.Image");
			toolStripButtonPrintTemplate.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrintTemplate.Name = "toolStripButtonPrintTemplate";
			toolStripButtonPrintTemplate.Size = new System.Drawing.Size(77, 28);
			toolStripButtonPrintTemplate.Text = "Print TR";
			toolStripButtonPrintTemplate.Click += new System.EventHandler(toolStripButtonPrintTemplate_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click_1);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 460);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(646, 40);
			panelButtons.TabIndex = 21;
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
			linePanelDown.Size = new System.Drawing.Size(646, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(536, 8);
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
			dateTimePickerDate.Location = new System.Drawing.Point(501, 32);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(120, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(334, 32);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(98, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(437, 59);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(501, 56);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(121, 20);
			textBoxRef1.TabIndex = 4;
			textBoxNote.Location = new System.Drawing.Point(106, 323);
			textBoxNote.MaxLength = 1000;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(515, 20);
			textBoxNote.TabIndex = 20;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 327);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(222, 35);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 1;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(334, 201);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 13);
			labelCurrency.TabIndex = 142;
			labelCurrency.Text = "Currency:";
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance3;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(9, 131);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel4.TabIndex = 129;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Pay To:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance5;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(9, 34);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 2;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance6;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
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
			contextMenuStrip1.Size = new System.Drawing.Size(181, 120);
			availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
			availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			availableQuantityToolStripMenuItem.Text = "Available Quantity...";
			salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
			salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			salesStatisticsToolStripMenuItem.Text = "Sales Statistics...";
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemPicToolStripMenuItem.Text = "Item Photo...";
			itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
			itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemDetailsToolStripMenuItem.Text = "Item Details...";
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
			removeRowToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
			removeRowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			removeRowToolStripMenuItem.Text = "Remove Row";
			comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxType.FormattingEnabled = true;
			comboBoxType.Items.AddRange(new object[4]
			{
				"Cheque Payment",
				"Bank Payment (TT)",
				"Trust Receipt (TR)",
				"Cash Payment"
			});
			comboBoxType.Location = new System.Drawing.Point(107, 56);
			comboBoxType.Name = "comboBoxType";
			comboBoxType.Size = new System.Drawing.Size(152, 21);
			comboBoxType.TabIndex = 3;
			comboBoxType.SelectedValueChanged += new System.EventHandler(comboBoxType_SelectedValueChanged);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(8, 60);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(34, 13);
			label4.TabIndex = 145;
			label4.Text = "Type:";
			textBoxPayeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPayeeName.Location = new System.Drawing.Point(287, 129);
			textBoxPayeeName.Name = "textBoxPayeeName";
			textBoxPayeeName.ReadOnly = true;
			textBoxPayeeName.Size = new System.Drawing.Size(335, 20);
			textBoxPayeeName.TabIndex = 12;
			textBoxPayeeName.TabStop = false;
			comboBoxReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxReason.FormattingEnabled = true;
			comboBoxReason.Items.AddRange(new object[2]
			{
				"Advance",
				"Outstanding Balance"
			});
			comboBoxReason.Location = new System.Drawing.Point(107, 244);
			comboBoxReason.Name = "comboBoxReason";
			comboBoxReason.Size = new System.Drawing.Size(128, 21);
			comboBoxReason.TabIndex = 18;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(9, 248);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(49, 13);
			label2.TabIndex = 152;
			label2.Text = "Purpose:";
			appearance7.FontData.BoldAsString = "True";
			appearance7.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance7;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(9, 83);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(60, 15);
			ultraFormattedLinkLabel1.TabIndex = 129;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Pay From:";
			appearance8.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance8;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			textBoxPayFromDesc.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPayFromDesc.Location = new System.Drawing.Point(287, 80);
			textBoxPayFromDesc.Name = "textBoxPayFromDesc";
			textBoxPayFromDesc.ReadOnly = true;
			textBoxPayFromDesc.Size = new System.Drawing.Size(335, 20);
			textBoxPayFromDesc.TabIndex = 6;
			textBoxPayFromDesc.TabStop = false;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(9, 6);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(84, 13);
			label5.TabIndex = 162;
			label5.Text = "Purchase Order:";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(9, 29);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(64, 13);
			label6.TabIndex = 164;
			label6.Text = "PO Amount:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(244, 29);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(31, 13);
			label7.TabIndex = 166;
			label7.Text = "Paid:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(383, 29);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(49, 13);
			label8.TabIndex = 168;
			label8.Text = "Balance:";
			textBoxPOTerm.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPOTerm.Location = new System.Drawing.Point(324, 3);
			textBoxPOTerm.Name = "textBoxPOTerm";
			textBoxPOTerm.ReadOnly = true;
			textBoxPOTerm.Size = new System.Drawing.Size(138, 20);
			textBoxPOTerm.TabIndex = 1;
			textBoxPOTerm.TabStop = false;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(272, 6);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(52, 13);
			label9.TabIndex = 170;
			label9.Text = "PO Term:";
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(452, 156);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(34, 13);
			label10.TabIndex = 172;
			label10.Text = "Term:";
			textBoxTerm.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTerm.Location = new System.Drawing.Point(492, 153);
			textBoxTerm.Name = "textBoxTerm";
			textBoxTerm.ReadOnly = true;
			textBoxTerm.Size = new System.Drawing.Size(129, 20);
			textBoxTerm.TabIndex = 171;
			textBoxTerm.TabStop = false;
			panelPO.Controls.Add(dateTimePickerETA);
			panelPO.Controls.Add(label13);
			panelPO.Controls.Add(textBoxPOSysDocID);
			panelPO.Controls.Add(buttonSelectPO);
			panelPO.Controls.Add(textBoxPOVoucherID);
			panelPO.Controls.Add(label9);
			panelPO.Controls.Add(textBoxPOTerm);
			panelPO.Controls.Add(label8);
			panelPO.Controls.Add(textBoxPOBalance);
			panelPO.Controls.Add(label7);
			panelPO.Controls.Add(textBoxPOPaid);
			panelPO.Controls.Add(label6);
			panelPO.Controls.Add(textBoxPOAmount);
			panelPO.Controls.Add(label5);
			panelPO.Controls.Add(comboBoxPOTerm);
			panelPO.Location = new System.Drawing.Point(0, 269);
			panelPO.Name = "panelPO";
			panelPO.Size = new System.Drawing.Size(621, 50);
			panelPO.TabIndex = 19;
			dateTimePickerETA.BackColor = System.Drawing.Color.WhiteSmoke;
			dateTimePickerETA.Location = new System.Drawing.Point(500, 3);
			dateTimePickerETA.Name = "dateTimePickerETA";
			dateTimePickerETA.ReadOnly = true;
			dateTimePickerETA.Size = new System.Drawing.Size(118, 20);
			dateTimePickerETA.TabIndex = 188;
			dateTimePickerETA.TabStop = false;
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(468, 7);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(31, 13);
			label13.TabIndex = 187;
			label13.Text = "ETA:";
			textBoxPOSysDocID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPOSysDocID.Location = new System.Drawing.Point(107, 4);
			textBoxPOSysDocID.Name = "textBoxPOSysDocID";
			textBoxPOSysDocID.ReadOnly = true;
			textBoxPOSysDocID.Size = new System.Drawing.Size(32, 20);
			textBoxPOSysDocID.TabIndex = 182;
			textBoxPOSysDocID.TabStop = false;
			buttonSelectPO.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectPO.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectPO.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectPO.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectPO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectPO.Location = new System.Drawing.Point(238, 3);
			buttonSelectPO.Name = "buttonSelectPO";
			buttonSelectPO.Size = new System.Drawing.Size(25, 24);
			buttonSelectPO.TabIndex = 180;
			buttonSelectPO.Text = "...";
			buttonSelectPO.UseVisualStyleBackColor = false;
			buttonSelectPO.Click += new System.EventHandler(buttonSelectPO_Click);
			textBoxPOVoucherID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPOVoucherID.Location = new System.Drawing.Point(141, 4);
			textBoxPOVoucherID.Name = "textBoxPOVoucherID";
			textBoxPOVoucherID.ReadOnly = true;
			textBoxPOVoucherID.Size = new System.Drawing.Size(95, 20);
			textBoxPOVoucherID.TabIndex = 181;
			textBoxPOVoucherID.TabStop = false;
			textBoxPOBalance.AllowDecimal = true;
			textBoxPOBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPOBalance.CustomReportFieldName = "";
			textBoxPOBalance.CustomReportKey = "";
			textBoxPOBalance.CustomReportValueType = 1;
			textBoxPOBalance.IsComboTextBox = false;
			textBoxPOBalance.IsModified = false;
			textBoxPOBalance.Location = new System.Drawing.Point(438, 26);
			textBoxPOBalance.MaxLength = 15;
			textBoxPOBalance.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxPOBalance.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxPOBalance.Name = "textBoxPOBalance";
			textBoxPOBalance.NullText = "0";
			textBoxPOBalance.ReadOnly = true;
			textBoxPOBalance.Size = new System.Drawing.Size(93, 20);
			textBoxPOBalance.TabIndex = 4;
			textBoxPOBalance.Text = "0.00";
			textBoxPOBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxPOBalance.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxPOPaid.AllowDecimal = true;
			textBoxPOPaid.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPOPaid.CustomReportFieldName = "";
			textBoxPOPaid.CustomReportKey = "";
			textBoxPOPaid.CustomReportValueType = 1;
			textBoxPOPaid.IsComboTextBox = false;
			textBoxPOPaid.IsModified = false;
			textBoxPOPaid.Location = new System.Drawing.Point(279, 26);
			textBoxPOPaid.MaxLength = 15;
			textBoxPOPaid.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxPOPaid.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxPOPaid.Name = "textBoxPOPaid";
			textBoxPOPaid.NullText = "0";
			textBoxPOPaid.ReadOnly = true;
			textBoxPOPaid.Size = new System.Drawing.Size(93, 20);
			textBoxPOPaid.TabIndex = 3;
			textBoxPOPaid.Text = "0.00";
			textBoxPOPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxPOPaid.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxPOAmount.AllowDecimal = true;
			textBoxPOAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPOAmount.CustomReportFieldName = "";
			textBoxPOAmount.CustomReportKey = "";
			textBoxPOAmount.CustomReportValueType = 1;
			textBoxPOAmount.IsComboTextBox = false;
			textBoxPOAmount.IsModified = false;
			textBoxPOAmount.Location = new System.Drawing.Point(106, 26);
			textBoxPOAmount.MaxLength = 15;
			textBoxPOAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxPOAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxPOAmount.Name = "textBoxPOAmount";
			textBoxPOAmount.NullText = "0";
			textBoxPOAmount.ReadOnly = true;
			textBoxPOAmount.Size = new System.Drawing.Size(129, 20);
			textBoxPOAmount.TabIndex = 2;
			textBoxPOAmount.Text = "0.00";
			textBoxPOAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxPOAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			comboBoxPOTerm.Assigned = false;
			comboBoxPOTerm.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPOTerm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPOTerm.CustomReportFieldName = "";
			comboBoxPOTerm.CustomReportKey = "";
			comboBoxPOTerm.CustomReportValueType = 1;
			comboBoxPOTerm.DescriptionTextBox = textBoxPOTerm;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPOTerm.DisplayLayout.Appearance = appearance9;
			comboBoxPOTerm.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPOTerm.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance10.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPOTerm.DisplayLayout.GroupByBox.Appearance = appearance10;
			appearance11.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPOTerm.DisplayLayout.GroupByBox.BandLabelAppearance = appearance11;
			comboBoxPOTerm.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance12.BackColor2 = System.Drawing.SystemColors.Control;
			appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance12.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPOTerm.DisplayLayout.GroupByBox.PromptAppearance = appearance12;
			comboBoxPOTerm.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPOTerm.DisplayLayout.MaxRowScrollRegions = 1;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPOTerm.DisplayLayout.Override.ActiveCellAppearance = appearance13;
			appearance14.BackColor = System.Drawing.SystemColors.Highlight;
			appearance14.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPOTerm.DisplayLayout.Override.ActiveRowAppearance = appearance14;
			comboBoxPOTerm.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPOTerm.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPOTerm.DisplayLayout.Override.CardAreaAppearance = appearance15;
			appearance16.BorderColor = System.Drawing.Color.Silver;
			appearance16.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPOTerm.DisplayLayout.Override.CellAppearance = appearance16;
			comboBoxPOTerm.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPOTerm.DisplayLayout.Override.CellPadding = 0;
			appearance17.BackColor = System.Drawing.SystemColors.Control;
			appearance17.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance17.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance17.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPOTerm.DisplayLayout.Override.GroupByRowAppearance = appearance17;
			appearance18.TextHAlignAsString = "Left";
			comboBoxPOTerm.DisplayLayout.Override.HeaderAppearance = appearance18;
			comboBoxPOTerm.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPOTerm.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.BorderColor = System.Drawing.Color.Silver;
			comboBoxPOTerm.DisplayLayout.Override.RowAppearance = appearance19;
			comboBoxPOTerm.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPOTerm.DisplayLayout.Override.TemplateAddRowAppearance = appearance20;
			comboBoxPOTerm.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPOTerm.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPOTerm.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPOTerm.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPOTerm.Editable = true;
			comboBoxPOTerm.FilterString = "";
			comboBoxPOTerm.HasAllAccount = false;
			comboBoxPOTerm.HasCustom = false;
			comboBoxPOTerm.IsDataLoaded = false;
			comboBoxPOTerm.Location = new System.Drawing.Point(351, 3);
			comboBoxPOTerm.MaxDropDownItems = 12;
			comboBoxPOTerm.Name = "comboBoxPOTerm";
			comboBoxPOTerm.ShowInactiveItems = false;
			comboBoxPOTerm.ShowQuickAdd = true;
			comboBoxPOTerm.Size = new System.Drawing.Size(100, 20);
			comboBoxPOTerm.TabIndex = 180;
			comboBoxPOTerm.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPOTerm.Visible = false;
			labelAmountLC.AutoSize = true;
			labelAmountLC.Location = new System.Drawing.Point(8, 224);
			labelAmountLC.Name = "labelAmountLC";
			labelAmountLC.Size = new System.Drawing.Size(62, 13);
			labelAmountLC.TabIndex = 176;
			labelAmountLC.Text = "Amount LC:";
			labelVoided.BackColor = System.Drawing.Color.Transparent;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(364, 240);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(133, 23);
			labelVoided.TabIndex = 180;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Items.AddRange(new object[3]
			{
				"Open",
				"Paid",
				"Cancelled"
			});
			comboBoxStatus.Location = new System.Drawing.Point(322, 56);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(110, 21);
			comboBoxStatus.TabIndex = 181;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(265, 59);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(40, 13);
			label11.TabIndex = 182;
			label11.Text = "Status:";
			labelAmountReqLC.AutoSize = true;
			labelAmountReqLC.Location = new System.Drawing.Point(247, 108);
			labelAmountReqLC.Name = "labelAmountReqLC";
			labelAmountReqLC.Size = new System.Drawing.Size(109, 13);
			labelAmountReqLC.TabIndex = 186;
			labelAmountReqLC.Text = "Payment Requested :";
			labelAmountReqFC.AutoSize = true;
			labelAmountReqFC.Location = new System.Drawing.Point(483, 109);
			labelAmountReqFC.Name = "labelAmountReqFC";
			labelAmountReqFC.Size = new System.Drawing.Size(23, 13);
			labelAmountReqFC.TabIndex = 187;
			labelAmountReqFC.Text = "FC:";
			textBoxTotalPurchaseClaim.AutoSize = true;
			textBoxTotalPurchaseClaim.Location = new System.Drawing.Point(321, 177);
			textBoxTotalPurchaseClaim.Name = "textBoxTotalPurchaseClaim";
			textBoxTotalPurchaseClaim.Size = new System.Drawing.Size(22, 13);
			textBoxTotalPurchaseClaim.TabIndex = 198;
			textBoxTotalPurchaseClaim.Text = "0.0";
			textBoxTotalPurchaseClaim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			textBoxTotalQualityClaim.AutoSize = true;
			textBoxTotalQualityClaim.Location = new System.Drawing.Point(105, 176);
			textBoxTotalQualityClaim.Name = "textBoxTotalQualityClaim";
			textBoxTotalQualityClaim.Size = new System.Drawing.Size(22, 13);
			textBoxTotalQualityClaim.TabIndex = 199;
			textBoxTotalQualityClaim.Text = "0.0";
			textBoxTotalQualityClaim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			comboBoxRegister.AlwaysInEditMode = true;
			comboBoxRegister.Assigned = false;
			comboBoxRegister.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRegister.CustomReportFieldName = "";
			comboBoxRegister.CustomReportKey = "";
			comboBoxRegister.CustomReportValueType = 1;
			comboBoxRegister.DescriptionTextBox = textBoxPayFromDesc;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRegister.DisplayLayout.Appearance = appearance21;
			comboBoxRegister.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRegister.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance22.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance22.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.GroupByBox.Appearance = appearance22;
			appearance23.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRegister.DisplayLayout.GroupByBox.BandLabelAppearance = appearance23;
			comboBoxRegister.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance24.BackColor2 = System.Drawing.SystemColors.Control;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRegister.DisplayLayout.GroupByBox.PromptAppearance = appearance24;
			comboBoxRegister.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRegister.DisplayLayout.MaxRowScrollRegions = 1;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRegister.DisplayLayout.Override.ActiveCellAppearance = appearance25;
			appearance26.BackColor = System.Drawing.SystemColors.Highlight;
			appearance26.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRegister.DisplayLayout.Override.ActiveRowAppearance = appearance26;
			comboBoxRegister.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRegister.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.Override.CardAreaAppearance = appearance27;
			appearance28.BorderColor = System.Drawing.Color.Silver;
			appearance28.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRegister.DisplayLayout.Override.CellAppearance = appearance28;
			comboBoxRegister.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRegister.DisplayLayout.Override.CellPadding = 0;
			appearance29.BackColor = System.Drawing.SystemColors.Control;
			appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance29.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance29.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.Override.GroupByRowAppearance = appearance29;
			appearance30.TextHAlignAsString = "Left";
			comboBoxRegister.DisplayLayout.Override.HeaderAppearance = appearance30;
			comboBoxRegister.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRegister.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.Color.Silver;
			comboBoxRegister.DisplayLayout.Override.RowAppearance = appearance31;
			comboBoxRegister.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRegister.DisplayLayout.Override.TemplateAddRowAppearance = appearance32;
			comboBoxRegister.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRegister.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRegister.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRegister.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRegister.Editable = true;
			comboBoxRegister.FilterString = "";
			comboBoxRegister.HasAllAccount = false;
			comboBoxRegister.HasCustom = false;
			comboBoxRegister.IsDataLoaded = false;
			comboBoxRegister.Location = new System.Drawing.Point(107, 80);
			comboBoxRegister.MaxDropDownItems = 12;
			comboBoxRegister.Name = "comboBoxRegister";
			comboBoxRegister.ShowDefaultRegisterOnly = false;
			comboBoxRegister.ShowInactiveItems = false;
			comboBoxRegister.ShowQuickAdd = true;
			comboBoxRegister.Size = new System.Drawing.Size(176, 20);
			comboBoxRegister.TabIndex = 200;
			comboBoxRegister.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linklabelPC.AutoSize = true;
			linklabelPC.AvailableInEdition = true;
			linklabelPC.Description = "";
			linklabelPC.LinkArea = new System.Windows.Forms.LinkArea(0, 53);
			linklabelPC.Location = new System.Drawing.Point(386, 177);
			linklabelPC.Name = "linklabelPC";
			linklabelPC.OriginalText = "";
			linklabelPC.Size = new System.Drawing.Size(19, 17);
			linklabelPC.TabIndex = 197;
			linklabelPC.TabStop = true;
			linklabelPC.Text = "bl2";
			linklabelPC.ToBeAligned = true;
			linklabelPC.UseCompatibleTextRendering = true;
			linklabelPC.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(baLinkLabel2_LinkClicked);
			linklabelQC.AutoSize = true;
			linklabelQC.AvailableInEdition = true;
			linklabelQC.Description = "";
			linklabelQC.LinkArea = new System.Windows.Forms.LinkArea(0, 53);
			linklabelQC.Location = new System.Drawing.Point(177, 177);
			linklabelQC.Name = "linklabelQC";
			linklabelQC.OriginalText = "";
			linklabelQC.Size = new System.Drawing.Size(19, 17);
			linklabelQC.TabIndex = 196;
			linklabelQC.TabStop = true;
			linklabelQC.Text = "bl1";
			linklabelQC.ToBeAligned = true;
			linklabelQC.UseCompatibleTextRendering = true;
			linklabelQC.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(baLinkLabel1_LinkClicked);
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(11, 176);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(70, 13);
			mmLabel6.TabIndex = 192;
			mmLabel6.Text = "Quality Claim:";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(229, 176);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(83, 13);
			mmLabel3.TabIndex = 191;
			mmLabel3.Text = "Purchase Claim:";
			textBoxPaymentRequestedFC.AllowDecimal = true;
			textBoxPaymentRequestedFC.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPaymentRequestedFC.CustomReportFieldName = "";
			textBoxPaymentRequestedFC.CustomReportKey = "";
			textBoxPaymentRequestedFC.CustomReportValueType = 1;
			textBoxPaymentRequestedFC.IsComboTextBox = false;
			textBoxPaymentRequestedFC.IsModified = false;
			textBoxPaymentRequestedFC.Location = new System.Drawing.Point(531, 104);
			textBoxPaymentRequestedFC.MaxLength = 15;
			textBoxPaymentRequestedFC.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxPaymentRequestedFC.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxPaymentRequestedFC.Name = "textBoxPaymentRequestedFC";
			textBoxPaymentRequestedFC.NullText = "0";
			textBoxPaymentRequestedFC.ReadOnly = true;
			textBoxPaymentRequestedFC.Size = new System.Drawing.Size(89, 20);
			textBoxPaymentRequestedFC.TabIndex = 188;
			textBoxPaymentRequestedFC.Text = "0.00";
			textBoxPaymentRequestedFC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxPaymentRequestedFC.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxPaymentRequested.AllowDecimal = true;
			textBoxPaymentRequested.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPaymentRequested.CustomReportFieldName = "";
			textBoxPaymentRequested.CustomReportKey = "";
			textBoxPaymentRequested.CustomReportValueType = 1;
			textBoxPaymentRequested.IsComboTextBox = false;
			textBoxPaymentRequested.IsModified = false;
			textBoxPaymentRequested.Location = new System.Drawing.Point(387, 105);
			textBoxPaymentRequested.MaxLength = 15;
			textBoxPaymentRequested.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxPaymentRequested.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxPaymentRequested.Name = "textBoxPaymentRequested";
			textBoxPaymentRequested.NullText = "0";
			textBoxPaymentRequested.ReadOnly = true;
			textBoxPaymentRequested.Size = new System.Drawing.Size(89, 20);
			textBoxPaymentRequested.TabIndex = 184;
			textBoxPaymentRequested.Text = "0.00";
			textBoxPaymentRequested.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxPaymentRequested.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			docStatusLabel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			docStatusLabel.BackColor = System.Drawing.Color.Transparent;
			docStatusLabel.DocumentNumber = "";
			docStatusLabel.LinkEnabled = true;
			docStatusLabel.Location = new System.Drawing.Point(505, 390);
			docStatusLabel.Name = "docStatusLabel";
			docStatusLabel.ShowDocNumber = true;
			docStatusLabel.Size = new System.Drawing.Size(131, 56);
			docStatusLabel.StatusColor = Micromind.DataControls.OtherControls.DocStatusLabel.StatusColors.Red;
			docStatusLabel.TabIndex = 183;
			docStatusLabel.Visible = false;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(8, 105);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(95, 13);
			mmLabel5.TabIndex = 178;
			mmLabel5.Text = "Available Balance:";
			textBoxBankBalance.AllowDecimal = true;
			textBoxBankBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankBalance.CustomReportFieldName = "";
			textBoxBankBalance.CustomReportKey = "";
			textBoxBankBalance.CustomReportValueType = 1;
			textBoxBankBalance.IsComboTextBox = false;
			textBoxBankBalance.IsModified = false;
			textBoxBankBalance.Location = new System.Drawing.Point(107, 104);
			textBoxBankBalance.MaxLength = 15;
			textBoxBankBalance.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxBankBalance.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxBankBalance.Name = "textBoxBankBalance";
			textBoxBankBalance.NullText = "0";
			textBoxBankBalance.ReadOnly = true;
			textBoxBankBalance.Size = new System.Drawing.Size(129, 20);
			textBoxBankBalance.TabIndex = 7;
			textBoxBankBalance.Text = "0.00";
			textBoxBankBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBankBalance.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxAmountLC.AllowDecimal = true;
			textBoxAmountLC.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAmountLC.CustomReportFieldName = "";
			textBoxAmountLC.CustomReportKey = "";
			textBoxAmountLC.CustomReportValueType = 1;
			textBoxAmountLC.IsComboTextBox = false;
			textBoxAmountLC.IsModified = false;
			textBoxAmountLC.Location = new System.Drawing.Point(106, 220);
			textBoxAmountLC.MaxLength = 15;
			textBoxAmountLC.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAmountLC.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAmountLC.Name = "textBoxAmountLC";
			textBoxAmountLC.NullText = "0";
			textBoxAmountLC.ReadOnly = true;
			textBoxAmountLC.Size = new System.Drawing.Size(129, 20);
			textBoxAmountLC.TabIndex = 17;
			textBoxAmountLC.Text = "0.00";
			textBoxAmountLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmountLC.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			comboBoxVendor.Assigned = false;
			comboBoxVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = textBoxPayeeName;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance33;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance34.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance34.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance34.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance34;
			appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance35;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance36.BackColor2 = System.Drawing.SystemColors.Control;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance36.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance36;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance37;
			appearance38.BackColor = System.Drawing.SystemColors.Highlight;
			appearance38.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance38;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance39;
			appearance40.BorderColor = System.Drawing.Color.Silver;
			appearance40.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance40;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance41.BackColor = System.Drawing.SystemColors.Control;
			appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance41.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance41.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance41;
			appearance42.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance42;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance43;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance44;
			comboBoxVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVendor.Editable = true;
			comboBoxVendor.FilterString = "";
			comboBoxVendor.FilterSysDocID = "";
			comboBoxVendor.HasAll = false;
			comboBoxVendor.HasCustom = false;
			comboBoxVendor.IsDataLoaded = false;
			comboBoxVendor.Location = new System.Drawing.Point(106, 129);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(177, 20);
			comboBoxVendor.TabIndex = 8;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(241, 155);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(67, 13);
			mmLabel4.TabIndex = 156;
			mmLabel4.Text = "Current Due:";
			textBoxTotalDue.AllowDecimal = true;
			textBoxTotalDue.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalDue.CustomReportFieldName = "";
			textBoxTotalDue.CustomReportKey = "";
			textBoxTotalDue.CustomReportValueType = 1;
			textBoxTotalDue.IsComboTextBox = false;
			textBoxTotalDue.IsModified = false;
			textBoxTotalDue.Location = new System.Drawing.Point(322, 153);
			textBoxTotalDue.MaxLength = 15;
			textBoxTotalDue.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalDue.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalDue.Name = "textBoxTotalDue";
			textBoxTotalDue.NullText = "0";
			textBoxTotalDue.ReadOnly = true;
			textBoxTotalDue.Size = new System.Drawing.Size(129, 20);
			textBoxTotalDue.TabIndex = 14;
			textBoxTotalDue.Text = "0.00";
			textBoxTotalDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalDue.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			labelVendorBalance.AutoSize = true;
			labelVendorBalance.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelVendorBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelVendorBalance.IsFieldHeader = false;
			labelVendorBalance.IsRequired = false;
			labelVendorBalance.Location = new System.Drawing.Point(9, 154);
			labelVendorBalance.Name = "labelVendorBalance";
			labelVendorBalance.PenWidth = 1f;
			labelVendorBalance.ShowBorder = false;
			labelVendorBalance.Size = new System.Drawing.Size(86, 13);
			labelVendorBalance.TabIndex = 154;
			labelVendorBalance.Text = "Current Balance:";
			textBoxTotalBalance.AllowDecimal = true;
			textBoxTotalBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalBalance.CustomReportFieldName = "";
			textBoxTotalBalance.CustomReportKey = "";
			textBoxTotalBalance.CustomReportValueType = 1;
			textBoxTotalBalance.IsComboTextBox = false;
			textBoxTotalBalance.IsModified = false;
			textBoxTotalBalance.Location = new System.Drawing.Point(107, 151);
			textBoxTotalBalance.MaxLength = 15;
			textBoxTotalBalance.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalBalance.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalBalance.Name = "textBoxTotalBalance";
			textBoxTotalBalance.NullText = "0";
			textBoxTotalBalance.ReadOnly = true;
			textBoxTotalBalance.Size = new System.Drawing.Size(129, 20);
			textBoxTotalBalance.TabIndex = 13;
			textBoxTotalBalance.Text = "0.00";
			textBoxTotalBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalBalance.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(9, 197);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(53, 13);
			mmLabel2.TabIndex = 150;
			mmLabel2.Text = "Amount:";
			textBoxAmount.AllowDecimal = true;
			textBoxAmount.BackColor = System.Drawing.Color.White;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.IsModified = false;
			textBoxAmount.Location = new System.Drawing.Point(107, 197);
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
			textBoxAmount.Size = new System.Drawing.Size(128, 20);
			textBoxAmount.TabIndex = 15;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCurrency.Location = new System.Drawing.Point(413, 198);
			comboBoxCurrency.MaximumSize = new System.Drawing.Size(99999, 20);
			comboBoxCurrency.MinimumSize = new System.Drawing.Size(5, 20);
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.Rate = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			comboBoxCurrency.SelectedID = "";
			comboBoxCurrency.Size = new System.Drawing.Size(129, 20);
			comboBoxCurrency.TabIndex = 16;
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
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(437, 34);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance45;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance52;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance54;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance55;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(107, 32);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(109, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVendorTerm.Assigned = false;
			comboBoxVendorTerm.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVendorTerm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendorTerm.CustomReportFieldName = "";
			comboBoxVendorTerm.CustomReportKey = "";
			comboBoxVendorTerm.CustomReportValueType = 1;
			comboBoxVendorTerm.DescriptionTextBox = textBoxTerm;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendorTerm.DisplayLayout.Appearance = appearance57;
			comboBoxVendorTerm.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendorTerm.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance58.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance58.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance58.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendorTerm.DisplayLayout.GroupByBox.Appearance = appearance58;
			appearance59.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendorTerm.DisplayLayout.GroupByBox.BandLabelAppearance = appearance59;
			comboBoxVendorTerm.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance60.BackColor2 = System.Drawing.SystemColors.Control;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendorTerm.DisplayLayout.GroupByBox.PromptAppearance = appearance60;
			comboBoxVendorTerm.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendorTerm.DisplayLayout.MaxRowScrollRegions = 1;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendorTerm.DisplayLayout.Override.ActiveCellAppearance = appearance61;
			appearance62.BackColor = System.Drawing.SystemColors.Highlight;
			appearance62.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendorTerm.DisplayLayout.Override.ActiveRowAppearance = appearance62;
			comboBoxVendorTerm.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendorTerm.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendorTerm.DisplayLayout.Override.CardAreaAppearance = appearance63;
			appearance64.BorderColor = System.Drawing.Color.Silver;
			appearance64.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendorTerm.DisplayLayout.Override.CellAppearance = appearance64;
			comboBoxVendorTerm.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendorTerm.DisplayLayout.Override.CellPadding = 0;
			appearance65.BackColor = System.Drawing.SystemColors.Control;
			appearance65.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance65.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance65.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendorTerm.DisplayLayout.Override.GroupByRowAppearance = appearance65;
			appearance66.TextHAlignAsString = "Left";
			comboBoxVendorTerm.DisplayLayout.Override.HeaderAppearance = appearance66;
			comboBoxVendorTerm.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendorTerm.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendorTerm.DisplayLayout.Override.RowAppearance = appearance67;
			comboBoxVendorTerm.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendorTerm.DisplayLayout.Override.TemplateAddRowAppearance = appearance68;
			comboBoxVendorTerm.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVendorTerm.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVendorTerm.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVendorTerm.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVendorTerm.Editable = true;
			comboBoxVendorTerm.FilterString = "";
			comboBoxVendorTerm.HasAllAccount = false;
			comboBoxVendorTerm.HasCustom = false;
			comboBoxVendorTerm.IsDataLoaded = false;
			comboBoxVendorTerm.Location = new System.Drawing.Point(492, 153);
			comboBoxVendorTerm.MaxDropDownItems = 12;
			comboBoxVendorTerm.Name = "comboBoxVendorTerm";
			comboBoxVendorTerm.ShowInactiveItems = false;
			comboBoxVendorTerm.ShowQuickAdd = true;
			comboBoxVendorTerm.Size = new System.Drawing.Size(100, 20);
			comboBoxVendorTerm.TabIndex = 179;
			comboBoxVendorTerm.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVendorTerm.Visible = false;
			comboBoxBankAccount.Assigned = false;
			comboBoxBankAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBankAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBankAccount.CustomReportFieldName = "";
			comboBoxBankAccount.CustomReportKey = "";
			comboBoxBankAccount.CustomReportValueType = 1;
			comboBoxBankAccount.DescriptionTextBox = textBoxPayFromDesc;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBankAccount.DisplayLayout.Appearance = appearance69;
			comboBoxBankAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBankAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance70.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance70.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBankAccount.DisplayLayout.GroupByBox.Appearance = appearance70;
			appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBankAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance71;
			comboBoxBankAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance72.BackColor2 = System.Drawing.SystemColors.Control;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBankAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance72;
			comboBoxBankAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBankAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBankAccount.DisplayLayout.Override.ActiveCellAppearance = appearance73;
			appearance74.BackColor = System.Drawing.SystemColors.Highlight;
			appearance74.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBankAccount.DisplayLayout.Override.ActiveRowAppearance = appearance74;
			comboBoxBankAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBankAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBankAccount.DisplayLayout.Override.CardAreaAppearance = appearance75;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			appearance76.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBankAccount.DisplayLayout.Override.CellAppearance = appearance76;
			comboBoxBankAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBankAccount.DisplayLayout.Override.CellPadding = 0;
			appearance77.BackColor = System.Drawing.SystemColors.Control;
			appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance77.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBankAccount.DisplayLayout.Override.GroupByRowAppearance = appearance77;
			appearance78.TextHAlignAsString = "Left";
			comboBoxBankAccount.DisplayLayout.Override.HeaderAppearance = appearance78;
			comboBoxBankAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBankAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.Color.Silver;
			comboBoxBankAccount.DisplayLayout.Override.RowAppearance = appearance79;
			comboBoxBankAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBankAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance80;
			comboBoxBankAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBankAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBankAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBankAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBankAccount.Editable = true;
			comboBoxBankAccount.FilterString = "";
			comboBoxBankAccount.HasAllAccount = false;
			comboBoxBankAccount.HasCustom = false;
			comboBoxBankAccount.IsDataLoaded = false;
			comboBoxBankAccount.Location = new System.Drawing.Point(107, 80);
			comboBoxBankAccount.MaxDropDownItems = 12;
			comboBoxBankAccount.Name = "comboBoxBankAccount";
			comboBoxBankAccount.ShowInactiveItems = false;
			comboBoxBankAccount.ShowQuickAdd = true;
			comboBoxBankAccount.Size = new System.Drawing.Size(176, 20);
			comboBoxBankAccount.TabIndex = 5;
			comboBoxBankAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxChequebook.Assigned = false;
			comboBoxChequebook.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxChequebook.CustomReportFieldName = "";
			comboBoxChequebook.CustomReportKey = "";
			comboBoxChequebook.CustomReportValueType = 1;
			comboBoxChequebook.DescriptionTextBox = textBoxPayFromDesc;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxChequebook.DisplayLayout.Appearance = appearance81;
			comboBoxChequebook.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxChequebook.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.GroupByBox.Appearance = appearance82;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxChequebook.DisplayLayout.GroupByBox.BandLabelAppearance = appearance83;
			comboBoxChequebook.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance84.BackColor2 = System.Drawing.SystemColors.Control;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxChequebook.DisplayLayout.GroupByBox.PromptAppearance = appearance84;
			comboBoxChequebook.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxChequebook.DisplayLayout.MaxRowScrollRegions = 1;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxChequebook.DisplayLayout.Override.ActiveCellAppearance = appearance85;
			appearance86.BackColor = System.Drawing.SystemColors.Highlight;
			appearance86.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxChequebook.DisplayLayout.Override.ActiveRowAppearance = appearance86;
			comboBoxChequebook.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxChequebook.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.Override.CardAreaAppearance = appearance87;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			appearance88.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxChequebook.DisplayLayout.Override.CellAppearance = appearance88;
			comboBoxChequebook.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxChequebook.DisplayLayout.Override.CellPadding = 0;
			appearance89.BackColor = System.Drawing.SystemColors.Control;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.Override.GroupByRowAppearance = appearance89;
			appearance90.TextHAlignAsString = "Left";
			comboBoxChequebook.DisplayLayout.Override.HeaderAppearance = appearance90;
			comboBoxChequebook.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxChequebook.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.Color.Silver;
			comboBoxChequebook.DisplayLayout.Override.RowAppearance = appearance91;
			comboBoxChequebook.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxChequebook.DisplayLayout.Override.TemplateAddRowAppearance = appearance92;
			comboBoxChequebook.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxChequebook.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxChequebook.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxChequebook.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxChequebook.Editable = true;
			comboBoxChequebook.FilterString = "";
			comboBoxChequebook.HasAllAccount = false;
			comboBoxChequebook.HasCustom = false;
			comboBoxChequebook.IsDataLoaded = false;
			comboBoxChequebook.Location = new System.Drawing.Point(107, 80);
			comboBoxChequebook.MaxDropDownItems = 12;
			comboBoxChequebook.Name = "comboBoxChequebook";
			comboBoxChequebook.ShowInactiveItems = false;
			comboBoxChequebook.ShowQuickAdd = true;
			comboBoxChequebook.Size = new System.Drawing.Size(176, 20);
			comboBoxChequebook.TabIndex = 5;
			comboBoxChequebook.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFacility.Assigned = false;
			comboBoxFacility.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFacility.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFacility.CustomReportFieldName = "";
			comboBoxFacility.CustomReportKey = "";
			comboBoxFacility.CustomReportValueType = 1;
			comboBoxFacility.DescriptionTextBox = textBoxPayFromDesc;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFacility.DisplayLayout.Appearance = appearance93;
			comboBoxFacility.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFacility.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFacility.DisplayLayout.GroupByBox.Appearance = appearance94;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFacility.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
			comboBoxFacility.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance96.BackColor2 = System.Drawing.SystemColors.Control;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFacility.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
			comboBoxFacility.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFacility.DisplayLayout.MaxRowScrollRegions = 1;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFacility.DisplayLayout.Override.ActiveCellAppearance = appearance97;
			appearance98.BackColor = System.Drawing.SystemColors.Highlight;
			appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFacility.DisplayLayout.Override.ActiveRowAppearance = appearance98;
			comboBoxFacility.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFacility.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFacility.DisplayLayout.Override.CardAreaAppearance = appearance99;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFacility.DisplayLayout.Override.CellAppearance = appearance100;
			comboBoxFacility.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFacility.DisplayLayout.Override.CellPadding = 0;
			appearance101.BackColor = System.Drawing.SystemColors.Control;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFacility.DisplayLayout.Override.GroupByRowAppearance = appearance101;
			appearance102.TextHAlignAsString = "Left";
			comboBoxFacility.DisplayLayout.Override.HeaderAppearance = appearance102;
			comboBoxFacility.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFacility.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.Color.Silver;
			comboBoxFacility.DisplayLayout.Override.RowAppearance = appearance103;
			comboBoxFacility.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFacility.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
			comboBoxFacility.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFacility.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFacility.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFacility.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFacility.Editable = true;
			comboBoxFacility.FilterFacilityType = Micromind.Common.Data.BankFacilityTypes.None;
			comboBoxFacility.FilterString = "";
			comboBoxFacility.HasAllAccount = false;
			comboBoxFacility.HasCustom = false;
			comboBoxFacility.IsDataLoaded = false;
			comboBoxFacility.Location = new System.Drawing.Point(107, 80);
			comboBoxFacility.MaxDropDownItems = 12;
			comboBoxFacility.Name = "comboBoxFacility";
			comboBoxFacility.ShowInactiveItems = false;
			comboBoxFacility.ShowQuickAdd = true;
			comboBoxFacility.Size = new System.Drawing.Size(176, 20);
			comboBoxFacility.TabIndex = 5;
			comboBoxFacility.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			panelTR.Controls.Add(textBoxNoofGoods);
			panelTR.Controls.Add(textBoxNoofPLs);
			panelTR.Controls.Add(textBoxNoofBOLs);
			panelTR.Controls.Add(label18);
			panelTR.Controls.Add(label17);
			panelTR.Controls.Add(label16);
			panelTR.Controls.Add(textBoxNoofInvoices);
			panelTR.Controls.Add(label15);
			panelTR.Controls.Add(label14);
			panelTR.Controls.Add(textBoxAuthorizedby);
			panelTR.Enabled = false;
			panelTR.Location = new System.Drawing.Point(108, 369);
			panelTR.Name = "panelTR";
			panelTR.Size = new System.Drawing.Size(369, 75);
			panelTR.TabIndex = 22;
			textBoxNoofGoods.Location = new System.Drawing.Point(79, 27);
			textBoxNoofGoods.MaxLength = 30;
			textBoxNoofGoods.Name = "textBoxNoofGoods";
			textBoxNoofGoods.Size = new System.Drawing.Size(119, 20);
			textBoxNoofGoods.TabIndex = 219;
			textBoxNoofPLs.AllowDecimal = true;
			textBoxNoofPLs.CustomReportFieldName = "";
			textBoxNoofPLs.CustomReportKey = "";
			textBoxNoofPLs.CustomReportValueType = 1;
			textBoxNoofPLs.IsComboTextBox = false;
			textBoxNoofPLs.IsModified = false;
			textBoxNoofPLs.Location = new System.Drawing.Point(79, 50);
			textBoxNoofPLs.MaxLength = 7;
			textBoxNoofPLs.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxNoofPLs.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxNoofPLs.Name = "textBoxNoofPLs";
			textBoxNoofPLs.NullText = "0";
			textBoxNoofPLs.Size = new System.Drawing.Size(62, 20);
			textBoxNoofPLs.TabIndex = 218;
			textBoxNoofPLs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxNoofBOLs.AllowDecimal = true;
			textBoxNoofBOLs.CustomReportFieldName = "";
			textBoxNoofBOLs.CustomReportKey = "";
			textBoxNoofBOLs.CustomReportValueType = 1;
			textBoxNoofBOLs.IsComboTextBox = false;
			textBoxNoofBOLs.IsModified = false;
			textBoxNoofBOLs.Location = new System.Drawing.Point(306, 7);
			textBoxNoofBOLs.MaxLength = 7;
			textBoxNoofBOLs.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxNoofBOLs.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxNoofBOLs.Name = "textBoxNoofBOLs";
			textBoxNoofBOLs.NullText = "0";
			textBoxNoofBOLs.Size = new System.Drawing.Size(55, 20);
			textBoxNoofBOLs.TabIndex = 214;
			textBoxNoofBOLs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label18.AutoSize = true;
			label18.Location = new System.Drawing.Point(4, 31);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(41, 13);
			label18.TabIndex = 212;
			label18.Text = "Goods:";
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(5, 55);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(55, 13);
			label17.TabIndex = 211;
			label17.Text = "No: of PL:";
			label16.AutoSize = true;
			label16.Location = new System.Drawing.Point(237, 9);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(63, 13);
			label16.TabIndex = 210;
			label16.Text = "No: of BOL:";
			textBoxNoofInvoices.AllowDecimal = true;
			textBoxNoofInvoices.CustomReportFieldName = "";
			textBoxNoofInvoices.CustomReportKey = "";
			textBoxNoofInvoices.CustomReportValueType = 1;
			textBoxNoofInvoices.IsComboTextBox = false;
			textBoxNoofInvoices.IsModified = false;
			textBoxNoofInvoices.Location = new System.Drawing.Point(306, 29);
			textBoxNoofInvoices.MaxLength = 7;
			textBoxNoofInvoices.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxNoofInvoices.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxNoofInvoices.Name = "textBoxNoofInvoices";
			textBoxNoofInvoices.NullText = "0";
			textBoxNoofInvoices.Size = new System.Drawing.Size(56, 20);
			textBoxNoofInvoices.TabIndex = 213;
			textBoxNoofInvoices.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(4, 9);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(74, 13);
			label15.TabIndex = 209;
			label15.Text = "Authorized by:";
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(237, 32);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(62, 13);
			label14.TabIndex = 208;
			label14.Text = "No: of Invs:";
			textBoxAuthorizedby.Location = new System.Drawing.Point(79, 5);
			textBoxAuthorizedby.MaxLength = 30;
			textBoxAuthorizedby.Name = "textBoxAuthorizedby";
			textBoxAuthorizedby.Size = new System.Drawing.Size(142, 20);
			textBoxAuthorizedby.TabIndex = 203;
			labelInvnos.AutoSize = true;
			labelInvnos.Location = new System.Drawing.Point(9, 349);
			labelInvnos.Name = "labelInvnos";
			labelInvnos.Size = new System.Drawing.Size(67, 13);
			labelInvnos.TabIndex = 202;
			labelInvnos.Text = "Invoice Nos:";
			textBoxInvoiceNo.Location = new System.Drawing.Point(107, 346);
			textBoxInvoiceNo.MaxLength = 15;
			textBoxInvoiceNo.Name = "textBoxInvoiceNo";
			textBoxInvoiceNo.Size = new System.Drawing.Size(513, 20);
			textBoxInvoiceNo.TabIndex = 21;
			buttonSelectInvoice.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectInvoice.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectInvoice.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectInvoice.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectInvoice.Location = new System.Drawing.Point(238, 196);
			buttonSelectInvoice.Name = "buttonSelectInvoice";
			buttonSelectInvoice.Size = new System.Drawing.Size(27, 21);
			buttonSelectInvoice.TabIndex = 203;
			buttonSelectInvoice.Text = "...";
			buttonSelectInvoice.UseVisualStyleBackColor = false;
			buttonSelectInvoice.Click += new System.EventHandler(buttonSelectInvoice_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(646, 500);
			base.Controls.Add(buttonSelectInvoice);
			base.Controls.Add(textBoxInvoiceNo);
			base.Controls.Add(panelTR);
			base.Controls.Add(comboBoxRegister);
			base.Controls.Add(textBoxTotalQualityClaim);
			base.Controls.Add(textBoxTotalPurchaseClaim);
			base.Controls.Add(linklabelPC);
			base.Controls.Add(linklabelQC);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(textBoxPaymentRequestedFC);
			base.Controls.Add(labelInvnos);
			base.Controls.Add(labelAmountReqFC);
			base.Controls.Add(labelAmountReqLC);
			base.Controls.Add(textBoxPaymentRequested);
			base.Controls.Add(docStatusLabel);
			base.Controls.Add(label11);
			base.Controls.Add(comboBoxStatus);
			base.Controls.Add(labelVoided);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(textBoxBankBalance);
			base.Controls.Add(labelAmountLC);
			base.Controls.Add(textBoxAmountLC);
			base.Controls.Add(panelPO);
			base.Controls.Add(comboBoxVendor);
			base.Controls.Add(label10);
			base.Controls.Add(textBoxPayFromDesc);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(textBoxTotalDue);
			base.Controls.Add(labelVendorBalance);
			base.Controls.Add(textBoxTotalBalance);
			base.Controls.Add(label2);
			base.Controls.Add(comboBoxReason);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(textBoxAmount);
			base.Controls.Add(textBoxPayeeName);
			base.Controls.Add(label4);
			base.Controls.Add(comboBoxType);
			base.Controls.Add(comboBoxCurrency);
			base.Controls.Add(formManager);
			base.Controls.Add(labelCurrency);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Controls.Add(ultraFormattedLinkLabel5);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(textBoxVoucherNumber);
			base.Controls.Add(textBoxRef1);
			base.Controls.Add(label1);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(comboBoxSysDoc);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(textBoxTerm);
			base.Controls.Add(comboBoxVendorTerm);
			base.Controls.Add(comboBoxBankAccount);
			base.Controls.Add(comboBoxChequebook);
			base.Controls.Add(comboBoxFacility);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "PaymentRequestForm";
			Text = "Payment Request";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			contextMenuStrip1.ResumeLayout(false);
			panelPO.ResumeLayout(false);
			panelPO.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPOTerm).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRegister).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendorTerm).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBankAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxChequebook).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFacility).EndInit();
			panelTR.ResumeLayout(false);
			panelTR.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
