using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Customers;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class CashReceiptForm : Form, IForm
	{
		private TransactionData currentData;

		private const string TABLENAME_CONST = "GL_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool isCostCenterMandatory = CompanyPreferences.IsCostCenterMandatory;

		private bool showAllocationForm = CompanyPreferences.ShowAllocationForm;

		private bool setcustomerID;

		private bool setregisterID;

		private string unit = string.Empty;

		private string property = string.Empty;

		private string _entityType = string.Empty;

		private decimal _amount;

		private string _entityID = string.Empty;

		private string _reference = string.Empty;

		private string _description = string.Empty;

		private string _note = string.Empty;

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

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private Label label3;

		private CurrencySelector currencySelector;

		private UltraFormattedLinkLabel labelCurrency;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private PayeeSelector payeeSelector1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private CostCenterComboBox comboBoxCostCenter;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private TextBox textBoxPayeeName;

		private flatDatePicker dateTimePickerDate;

		private RegisterComboBox comboBoxRegister;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private AmountTextBox textBoxAmount;

		private MMLabel mmLabel2;

		private Label label2;

		private PaymentMethodTypesComboBox comboBoxPaymentMethod;

		private TextBox textBoxDescription;

		private Label label4;

		private Panel panelDetails;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private TextBox textBoxBalance;

		private Label label5;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private Panel panelJobCC;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private JobComboBox comboBoxJob;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private CostCategoryComboBox comboBoxCostCategory;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private ExpenseCodeComboBox comboBoxExpenseCode;

		private Label labelPercent;

		private Label labelExpAmount;

		private AmountTextBox textBoxExpAmount;

		private AnalysisComboBox comboBoxAnalysis;

		private PercentTextBox textboxPercent;

		private UltraFormattedLinkLabel labelExpenseCode;

		private UltraFormattedLinkLabel labelAnalysis;

		private UltraPanel panelProperty;

		private UltraFormattedLinkLabel labelAttributeID1;

		private UltraFormattedLinkLabel labelAttributeID2;

		private PropertyComboBox comboBoxProperty;

		private PropertyUnitComboBox comboBoxPropertyUnit;

		private XPButton buttonSelectInvoice;

		private ToolStripButton toolStripButtonSignature;

		private ToolStripSeparator toolStripSeparatorSign;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1007;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		public string EntityType
		{
			private get
			{
				return _entityType;
			}
			set
			{
				_entityType = value;
			}
		}

		public decimal Amount
		{
			private get
			{
				return _amount;
			}
			set
			{
				_amount = value;
			}
		}

		public string EntityID
		{
			private get
			{
				return _entityID;
			}
			set
			{
				_entityID = value;
			}
		}

		public string SourceSysDocID
		{
			get;
			set;
		}

		public string SourceVoucherID
		{
			get;
			set;
		}

		public string Unit
		{
			private get
			{
				return unit;
			}
			set
			{
				unit = value;
			}
		}

		public string Property
		{
			private get
			{
				return property;
			}
			set
			{
				property = value;
			}
		}

		public string Reference
		{
			get
			{
				return _reference;
			}
			set
			{
				_reference = value;
			}
		}

		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}

		public string Note
		{
			get
			{
				return _note;
			}
			set
			{
				_note = value;
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
					textBoxVoucherNumber.Enabled = true;
					comboBoxSysDoc.Enabled = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.Enabled = false;
					comboBoxSysDoc.Enabled = false;
				}
				toolStripButtonDistribution.Enabled = !value;
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
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

		public CashReceiptForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxPaymentMethod.HideARAccount = true;
			labelAttributeID1.Value = CompanyPreferences.AttributeID1Title + ":";
			labelAttributeID2.Value = CompanyPreferences.AttributeID2Title + ":";
			if (useJobCosting)
			{
				panelJobCC.Visible = true;
			}
		}

		private void AddEvents()
		{
			base.Load += TransactionLeavesForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxCostCenter.SelectedIndexChanged += comboBoxCostCenter_SelectedIndexChanged;
			payeeSelector1.SelectedItemChanged += payeeSelector1_SelectedItemChanged;
			base.KeyDown += Form_KeyDown;
		}

		private void Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void payeeSelector1_SelectedItemChanged(object sender, EventArgs e)
		{
			textBoxPayeeName.Text = payeeSelector1.SelectedName;
			if (payeeSelector1.SelectedType == "A")
			{
				comboBoxAnalysis.FilterByAccount(payeeSelector1.SelectedID);
				labelAnalysis.Visible = true;
				comboBoxAnalysis.Visible = true;
			}
			else
			{
				comboBoxAnalysis.Clear();
				comboBoxAnalysis.FilterByAccount("$#$$LDF");
				labelAnalysis.Visible = false;
				comboBoxAnalysis.Visible = false;
			}
			if (payeeSelector1.SelectedID != "")
			{
				if (payeeSelector1.SelectedType == "C")
				{
					DataSet customerSnapBalance = Factory.CustomerSystem.GetCustomerSnapBalance(payeeSelector1.SelectedID);
					decimal d = decimal.Parse(customerSnapBalance.Tables[0].Rows[0]["Balance"].ToString());
					decimal d2 = decimal.Parse(customerSnapBalance.Tables[0].Rows[0]["PDCAmount"].ToString());
					textBoxBalance.Text = (d - d2).ToString(Format.TotalAmountFormat);
					if (useJobCosting)
					{
						comboBoxJob.Clear();
						comboBoxJob.Filter(payeeSelector1.SelectedID.Trim());
					}
					buttonSelectInvoice.Visible = true;
					if (PublicFunctions.GetCustomerSignatureThumbnailImage(payeeSelector1.SelectedID) != null)
					{
						ToolStripButton toolStripButton = toolStripButtonSignature;
						bool visible = toolStripSeparatorSign.Visible = true;
						toolStripButton.Visible = visible;
					}
					else
					{
						ToolStripButton toolStripButton2 = toolStripButtonSignature;
						bool visible = toolStripSeparatorSign.Visible = false;
						toolStripButton2.Visible = visible;
					}
				}
				else if (payeeSelector1.SelectedType == "V")
				{
					DataSet vendorBalanceAmount = Factory.VendorSystem.GetVendorBalanceAmount(payeeSelector1.SelectedID);
					decimal d3 = decimal.Parse(vendorBalanceAmount.Tables[0].Rows[0]["Balance"].ToString());
					decimal d4 = decimal.Parse(vendorBalanceAmount.Tables[0].Rows[0]["PDCAmount"].ToString());
					textBoxBalance.Text = (d3 - d4).ToString(Format.TotalAmountFormat);
					if (useJobCosting)
					{
						comboBoxJob.Clear();
						comboBoxJob.Filter("");
					}
					buttonSelectInvoice.Visible = false;
				}
				else
				{
					textBoxBalance.Text = "";
					if (useJobCosting)
					{
						comboBoxJob.Clear();
						comboBoxJob.Filter("");
					}
					buttonSelectInvoice.Visible = false;
				}
				if (payeeSelector1.SelectedType == "V")
				{
					bool result = false;
					bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Vendor", "IsHoldForPayment", "VendorID", payeeSelector1.SelectedID).ToString(), out result);
					if (result && IsNewRecord)
					{
						ErrorHelper.WarningMessage("This vendor is on hold status and does not allow transaction.");
						return;
					}
				}
			}
			else
			{
				textBoxBalance.Text = "";
				if (useJobCosting)
				{
					comboBoxJob.Clear();
					comboBoxJob.Filter("");
				}
			}
			if (textBoxAmount.Tag != null)
			{
				textBoxAmount.Tag = null;
				textBoxAmount.Clear();
			}
		}

		private void comboBoxCostCenter_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			string idFieldValue = Factory.DatabaseSystem.GetFieldValue("System_Document", "LocationID", "SysDocID", comboBoxSysDoc.SelectedID).ToString();
			string text = "";
			string text2 = "";
			string text3 = "";
			setcustomerID = false;
			setregisterID = false;
			text = Factory.DatabaseSystem.GetFieldValue("Location", "DefaultCustomerID", "LocationID", idFieldValue).ToString();
			text2 = Factory.DatabaseSystem.GetFieldValue("Location", "DefaultRegisterID", "LocationID", idFieldValue).ToString();
			if (text != "")
			{
				text3 = Factory.DatabaseSystem.GetFieldValue("Customer", "CustomerName", "CustomerID", text).ToString();
			}
			if (text != "")
			{
				payeeSelector1.SelectedID = text;
				payeeSelector1.Text = text3;
			}
			if (text2 != "")
			{
				setregisterID = true;
				comboBoxRegister.SelectedID = text2;
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
		}

		private bool GetData()
		{
			checked
			{
				try
				{
					if (currentData == null || isNewRecord)
					{
						currentData = new TransactionData();
					}
					DataRow dataRow = (!isNewRecord) ? currentData.TransactionTable.Rows[0] : currentData.TransactionTable.NewRow();
					dataRow["TransactionID"] = 0;
					dataRow["SysDocType"] = (byte)3;
					dataRow["Description"] = textBoxNote.Text;
					dataRow["CostCenterID"] = comboBoxCostCenter.SelectedID;
					dataRow["TransactionDate"] = dateTimePickerDate.Value;
					dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow["Reference"] = textBoxRef1.Text;
					dataRow["RegisterID"] = comboBoxRegister.SelectedID;
					dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
					dataRow["CompanyID"] = Global.CompanyID;
					string text = (string)(dataRow["PayeeType"] = payeeSelector1.SelectedType);
					dataRow["PayeeID"] = payeeSelector1.SelectedID;
					if (currencySelector.SelectedID != "" && currencySelector.SelectedID != Global.BaseCurrencyID)
					{
						dataRow["CurrencyID"] = currencySelector.SelectedID;
						dataRow["CurrencyRate"] = currencySelector.Rate;
						dataRow["AmountFC"] = textBoxAmount.Text;
					}
					else
					{
						dataRow["CurrencyID"] = DBNull.Value;
						dataRow["CurrencyRate"] = 1;
						dataRow["Amount"] = textBoxAmount.Text;
					}
					if (comboBoxPaymentMethod.SelectedType == PaymentMethodTypes.CreditCard && comboBoxExpenseCode.SelectedID != "")
					{
						dataRow["ExpAmount"] = textBoxExpAmount.Text;
						dataRow["ExpPercent"] = textboxPercent.Text;
						dataRow["ExpCode"] = comboBoxExpenseCode.SelectedID;
					}
					else
					{
						dataRow["ExpAmount"] = DBNull.Value;
						dataRow["ExpPercent"] = DBNull.Value;
						dataRow["ExpCode"] = "";
					}
					dataRow.EndEdit();
					if (IsNewRecord)
					{
						currentData.TransactionTable.Rows.Add(dataRow);
					}
					currentData.TransactionDetailsTable.Rows.Clear();
					DataRow dataRow2 = currentData.TransactionDetailsTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["PaymentMethodType"] = (byte)comboBoxPaymentMethod.SelectedType;
					dataRow2["Description"] = textBoxDescription.Text;
					if (currencySelector.SelectedID == "" || currencySelector.IsBaseCurrency)
					{
						dataRow2["Amount"] = textBoxAmount.Text;
						dataRow2["AmountFC"] = DBNull.Value;
					}
					else
					{
						dataRow2["AmountFC"] = textBoxAmount.Text;
					}
					if (comboBoxExpenseCode.SelectedID != "")
					{
						dataRow2["ExpAmount"] = textBoxExpAmount.Text;
						dataRow2["ExpCode"] = comboBoxExpenseCode.SelectedID;
					}
					else
					{
						dataRow2["ExpAmount"] = DBNull.Value;
						dataRow2["ExpCode"] = comboBoxExpenseCode.SelectedID;
					}
					if (comboBoxJob.SelectedID != null && comboBoxJob.SelectedID.ToString() != "")
					{
						dataRow2["JobID"] = comboBoxJob.SelectedID;
					}
					else
					{
						dataRow2["JobID"] = DBNull.Value;
					}
					if (comboBoxCostCategory.SelectedID != null && comboBoxCostCategory.SelectedID.ToString() != "")
					{
						dataRow2["CostCategoryID"] = comboBoxCostCategory.SelectedID.ToString();
					}
					else
					{
						dataRow2["CostCategoryID"] = DBNull.Value;
					}
					if (comboBoxProperty.SelectedID != null && comboBoxProperty.SelectedID.ToString() != "")
					{
						dataRow2["AttributeID1"] = comboBoxProperty.SelectedID;
					}
					else
					{
						dataRow2["AttributeID1"] = DBNull.Value;
					}
					if (comboBoxPropertyUnit.SelectedID != null && comboBoxPropertyUnit.SelectedID.ToString() != "")
					{
						dataRow2["AttributeID2"] = comboBoxPropertyUnit.SelectedID.ToString();
					}
					else
					{
						dataRow2["AttributeID2"] = DBNull.Value;
					}
					if (comboBoxAnalysis.SelectedID != null && comboBoxAnalysis.SelectedID.ToString() != "")
					{
						dataRow2["AnalysisID"] = comboBoxAnalysis.SelectedID.ToString();
					}
					else
					{
						dataRow2["AnalysisID"] = DBNull.Value;
					}
					dataRow2["RowIndex"] = 0;
					dataRow2.EndEdit();
					currentData.TransactionDetailsTable.Rows.Add(dataRow2);
					if (comboBoxPaymentMethod.SelectedType == PaymentMethodTypes.CreditCard && comboBoxExpenseCode.SelectedID != "")
					{
						DataRow dataRow3 = currentData.TransactionDetailsTable.NewRow();
						dataRow3.BeginEdit();
						dataRow3["ExpAmount"] = textBoxExpAmount.Text;
						dataRow3["ExpPercent"] = textboxPercent.Text;
						dataRow3["ExpCode"] = comboBoxExpenseCode.SelectedID;
						dataRow3["PaymentMethodType"] = (byte)comboBoxPaymentMethod.SelectedType;
						dataRow3["Description"] = textBoxDescription.Text;
						if (currencySelector.SelectedID == "" || currencySelector.IsBaseCurrency)
						{
							dataRow3["Amount"] = textBoxAmount.Text;
							dataRow3["AmountFC"] = DBNull.Value;
						}
						else
						{
							dataRow3["AmountFC"] = textBoxAmount.Text;
						}
						if (comboBoxJob.SelectedID != null && comboBoxJob.SelectedID.ToString() != "")
						{
							dataRow3["JobID"] = comboBoxJob.SelectedID;
						}
						else
						{
							dataRow3["JobID"] = DBNull.Value;
						}
						if (comboBoxCostCategory.SelectedID != null && comboBoxCostCategory.SelectedID.ToString() != "")
						{
							dataRow3["CostCategoryID"] = comboBoxCostCategory.SelectedID.ToString();
						}
						else
						{
							dataRow3["CostCategoryID"] = DBNull.Value;
						}
						if (comboBoxAnalysis.SelectedID != null && comboBoxAnalysis.SelectedID.ToString() != "")
						{
							dataRow3["AnalysisID"] = comboBoxAnalysis.SelectedID.ToString();
						}
						else
						{
							dataRow3["AnalysisID"] = DBNull.Value;
						}
						dataRow3["RowIndex"] = 1;
						dataRow3.EndEdit();
						currentData.TransactionDetailsTable.Rows.Add(dataRow3);
					}
					if (SourceSysDocID != null && SourceVoucherID != null)
					{
						currentData.GeneralPaymentDetailsTable.Rows.Clear();
						DataRow dataRow4 = currentData.GeneralPaymentDetailsTable.NewRow();
						dataRow4.BeginEdit();
						dataRow4["SourceSysDocID"] = SourceSysDocID;
						dataRow4["SourceVoucherID"] = SourceVoucherID;
						dataRow4["SysDocID"] = comboBoxSysDoc.SelectedID;
						dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
						dataRow4["Description"] = textBoxDescription.Text;
						dataRow4["Amount"] = textBoxAmount.Text;
						dataRow4["PayeeID"] = payeeSelector1.SelectedID;
						dataRow4["PayeeType"] = payeeSelector1.SelectedType;
						dataRow4["TransactionDate"] = dateTimePickerDate.Value;
						dataRow4["Reference"] = textBoxRef1.Text;
						dataRow4.EndEdit();
						currentData.GeneralPaymentDetailsTable.Rows.Add(dataRow4);
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
						if (payeeSelector1.SelectedType == "C")
						{
							comboBoxJob.Filter(payeeSelector1.SelectedID);
						}
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
					comboBoxRegister.SelectedID = dataRow["RegisterID"].ToString();
					comboBoxCostCenter.SelectedID = dataRow["CostCenterID"].ToString();
					textBoxNote.Text = dataRow["Description"].ToString();
					payeeSelector1.SelectedType = dataRow["PayeeType"].ToString();
					payeeSelector1.SelectedID = dataRow["PayeeID"].ToString();
					comboBoxAnalysis.SelectedID = dataRow["AnalysisID"].ToString();
					if (dataRow["ExpCode"].ToString() != "")
					{
						comboBoxExpenseCode.SelectedID = dataRow["ExpCode"].ToString();
						textBoxExpAmount.Text = dataRow["ExpAmount"].ToString();
						decimal result = default(decimal);
						decimal.TryParse(dataRow["ExpPercent"].ToString(), out result);
						textboxPercent.Text = result.ToString(Format.PercentageFormat);
					}
					if (dataRow["CurrencyID"] != DBNull.Value)
					{
						currencySelector.SelectedID = dataRow["CurrencyID"].ToString();
						currencySelector.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
					}
					else
					{
						currencySelector.SelectedID = "";
						currencySelector.Rate = 1m;
					}
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
						if (dataRow2["AmountFC"] != DBNull.Value)
						{
							textBoxAmount.Text = Math.Round(decimal.Parse(dataRow2["AmountFC"].ToString()), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
						}
						else if (dataRow2["Amount"] != DBNull.Value)
						{
							textBoxAmount.Text = Math.Round(decimal.Parse(dataRow2["Amount"].ToString()), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
						}
						else
						{
							textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
						}
						comboBoxPaymentMethod.SelectedType = (PaymentMethodTypes)int.Parse(dataRow2["PaymentMethodType"].ToString());
						if (comboBoxPaymentMethod.SelectedType == PaymentMethodTypes.CreditCard)
						{
							comboBoxExpenseCode.SelectedID = dataRow2["ExpCode"].ToString();
							textBoxExpAmount.Text = dataRow2["ExpAmount"].ToString();
						}
						if (dataRow2["JobID"] != DBNull.Value)
						{
							comboBoxJob.SelectedID = dataRow2["JobID"].ToString();
						}
						else
						{
							comboBoxJob.Clear();
						}
						if (dataRow2["CostCategoryID"] != DBNull.Value)
						{
							comboBoxCostCategory.SelectedID = dataRow2["CostCategoryID"].ToString();
						}
						else
						{
							comboBoxCostCategory.Clear();
						}
						if (dataRow2["AttributeID1"] != DBNull.Value)
						{
							comboBoxProperty.SelectedID = dataRow2["AttributeID1"].ToString();
						}
						else
						{
							comboBoxProperty.Clear();
						}
						if (dataRow2["AttributeID2"] != DBNull.Value)
						{
							comboBoxPropertyUnit.SelectedID = dataRow2["AttributeID2"].ToString();
						}
						else
						{
							comboBoxPropertyUnit.Clear();
						}
						if (dataRow2["AnalysisID"] != DBNull.Value)
						{
							comboBoxAnalysis.SelectedID = dataRow2["AnalysisID"].ToString();
						}
						else
						{
							comboBoxAnalysis.Clear();
						}
						textBoxDescription.Text = dataRow2["Description"].ToString();
						if (currentData.Tables["General_Payment_Detail"].Rows.Count > 0)
						{
							DataRow dataRow3 = currentData.Tables["General_Payment_Detail"].Rows[0];
							SourceSysDocID = dataRow3["SourceSysDocID"].ToString();
							SourceVoucherID = dataRow3["SourceVoucherID"].ToString();
						}
						buttonSelectInvoice.Enabled = false;
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

		private bool SaveDataOld(bool clearAfter)
		{
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
				bool flag2 = (!isNewRecord) ? Factory.TransactionSystem.UpdateTransaction(currentData) : Factory.TransactionSystem.CreateTransaction(currentData);
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
					if (showAllocationForm && payeeSelector1.SelectedType == "C")
					{
						CustomerPaymentAllocationForm customerPaymentAllocationForm = new CustomerPaymentAllocationForm();
						customerPaymentAllocationForm.SetData(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, payeeSelector1.SelectedID, -1, isPDC: false);
						customerPaymentAllocationForm.ShowDialog();
					}
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
			bool flag = false;
			try
			{
				ARJournalData aRJournalData = new ARJournalData();
				bool flag2;
				if (isNewRecord)
				{
					if (showAllocationForm && textBoxAmount.Tag != null)
					{
						string a = Global.BaseCurrencyID;
						aRJournalData = (textBoxAmount.Tag as ARJournalData);
						foreach (DataRow row in aRJournalData.Tables["AR_Payment_Allocation"].Rows)
						{
							row["PaymentSysDocID"] = comboBoxSysDoc.SelectedID;
							row["PaymentVoucherID"] = textBoxVoucherNumber.Text;
							if (!row["CurrencyID"].IsDBNullOrEmpty())
							{
								a = row["CurrencyID"].ToString();
							}
						}
						if (a != currencySelector.SelectedID)
						{
							ErrorHelper.WarningMessage("Currency should not change after selecting invoices.");
							return false;
						}
						currentData.Merge(aRJournalData);
					}
					flag2 = Factory.TransactionSystem.CreateTransaction(currentData);
				}
				else
				{
					flag2 = Factory.TransactionSystem.UpdateTransaction(currentData);
				}
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
					if (showAllocationForm && textBoxAmount.Tag == null && payeeSelector1.SelectedType == "C")
					{
						CustomerPaymentAllocationForm customerPaymentAllocationForm = new CustomerPaymentAllocationForm();
						customerPaymentAllocationForm.SetData(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, payeeSelector1.SelectedID, -1, isPDC: false);
						customerPaymentAllocationForm.ShowDialog();
					}
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
			formManager.ShowApprovalPanel(approvalTaskID, "GL_Transaction", "VoucherID");
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
			if (!IsNewRecord)
			{
				DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(currentData, SysDocTypes.CashReceipt, Global.CurrentUser, includeApproveUser: false);
				if (entityApprovalStatus.Tables[0].Rows.Count > 0 && !bool.Parse(entityApprovalStatus.Tables[0].Rows[0]["ModifyTransaction"].ToString()))
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
					return false;
				}
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
			if (comboBoxRegister.SelectedID == "" || comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "" || payeeSelector1.SelectedID == "")
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
			if (isCostCenterMandatory && comboBoxCostCenter.SelectedID == string.Empty)
			{
				ErrorHelper.InformationMessage("Please select Cost Category");
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("GL_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
				textBoxVoucherNumber.Focus();
				return false;
			}
			if (payeeSelector1.SelectedType == "V" && payeeSelector1.SelectedID != "")
			{
				bool result2 = false;
				bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Vendor", "IsHoldForPayment", "VendorID", payeeSelector1.SelectedID).ToString(), out result2);
				if (result2 && IsNewRecord)
				{
					ErrorHelper.WarningMessage("This vendor is on hold status and does not allow transaction.");
					return false;
				}
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
				textBoxNote.Clear();
				textBoxRef1.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxAnalysis.Clear();
				if (!setcustomerID)
				{
					payeeSelector1.Clear();
					textBoxPayeeName.Clear();
				}
				textBoxDescription.Clear();
				payeeSelector1.Enabled = true;
				comboBoxPaymentMethod.SelectedType = PaymentMethodTypes.Cash;
				textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxAmount.ReadOnly = false;
				textBoxBalance.Clear();
				payeeSelector1.SelectedType = "C";
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				comboBoxJob.Clear();
				comboBoxCostCategory.Clear();
				comboBoxExpenseCode.Clear();
				textBoxExpAmount.Clear();
				textboxPercent.Clear();
				comboBoxPropertyUnit.Clear();
				comboBoxProperty.Clear();
				textBoxAmount.Tag = null;
				textBoxAmount.ReadOnly = false;
				buttonSelectInvoice.Enabled = true;
				IsVoid = false;
				if (useJobCosting)
				{
					comboBoxJob.Filter("");
				}
				formManager.ResetDirty();
				dateTimePickerDate.Focus();
				ToolStripButton toolStripButton = toolStripButtonSignature;
				bool visible = toolStripSeparatorSign.Visible = false;
				toolStripButton.Visible = visible;
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
				UltraFormattedLinkLabel ultraFormattedLinkLabel = labelCurrency;
				bool visible = currencySelector.Visible = CompanyPreferences.UseMultiCurrency;
				ultraFormattedLinkLabel.Visible = visible;
				panelProperty.Visible = CompanyPreferences.UseProperty;
				comboBoxPaymentMethod.LoadData();
				SetSecurity();
				if (!base.IsDisposed)
				{
					dateTimePickerDate.Value = DateTime.Now;
					IsNewRecord = true;
					ClearForm();
					LoadDataIfExist();
					comboBoxSysDoc.FilterByType(SysDocTypes.CashReceipt);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadDataIfExist()
		{
			if (EntityType == "" && EntityID == "")
			{
				payeeSelector1.SelectedType = "C";
				return;
			}
			payeeSelector1.SelectedType = EntityType;
			payeeSelector1.SelectedID = EntityID;
			textBoxAmount.Text = Amount.ToString();
			textBoxRef1.Text = Reference.ToString();
			textBoxDescription.Text = Description.ToString();
			textBoxNote.Text = Note.ToString();
			comboBoxPropertyUnit.SelectedID = Unit;
			comboBoxProperty.SelectedID = Property;
			if (Note != "" && Note != string.Empty)
			{
				payeeSelector1.Enabled = false;
				comboBoxRegister.SelectedIndex = 2;
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
			if (!Security.IsAllowedSecurityUser(GeneralSecurityRoles.ChangeTransactionRegister))
			{
				comboBoxRegister.ShowDefaultRegisterOnly = true;
			}
			else
			{
				comboBoxRegister.ShowDefaultRegisterOnly = false;
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

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(currencySelector.SelectedID);
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

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			string selectedType = payeeSelector1.SelectedType;
			if (!(selectedType == ""))
			{
				FormHelper formHelper = new FormHelper();
				if (selectedType == "A")
				{
					formHelper.EditAccount(payeeSelector1.SelectedID);
				}
				else if (selectedType == "C")
				{
					formHelper.EditCustomer(payeeSelector1.SelectedID);
				}
				else if (selectedType == "V")
				{
					formHelper.EditVendor(payeeSelector1.SelectedID);
				}
				else if (selectedType == "E")
				{
					formHelper.EditEmployee(payeeSelector1.SelectedID);
				}
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCostCenter(comboBoxCostCenter.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.CashReceipt);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditRegister(comboBoxRegister.SelectedID);
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
						PrintHelper.PrintDocument(transactionToPrint, systemDocID, "Cash Receipt", SysDocTypes.CashReceipt, isPrint, showPrintDialog);
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ReceiptVoucherListFormObj);
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJob(comboBoxJob.SelectedID);
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCostCategory(comboBoxCostCategory.SelectedID);
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

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to copy this document?") == DialogResult.Yes)
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

		private void comboBoxPaymentMethod_ValueChanged(object sender, EventArgs e)
		{
			if (comboBoxPaymentMethod.SelectedType == PaymentMethodTypes.CreditCard)
			{
				labelExpenseCode.ReadOnly = false;
				comboBoxExpenseCode.ReadOnly = false;
				comboBoxExpenseCode.ExpenseCodeType = ExpenseCodeTypes.BankFee;
				textboxPercent.ReadOnly = false;
				textBoxExpAmount.ReadOnly = false;
			}
			else
			{
				comboBoxExpenseCode.ReadOnly = true;
				textboxPercent.ReadOnly = true;
				textBoxExpAmount.ReadOnly = true;
				textboxPercent.Text = "";
				textBoxExpAmount.Text = "";
				comboBoxExpenseCode.Clear();
			}
		}

		private void textBoxAmount_TextChanged(object sender, EventArgs e)
		{
			decimal num = default(decimal);
			if (textBoxAmount.Text != "" && comboBoxPaymentMethod.SelectedType == PaymentMethodTypes.CreditCard)
			{
				if (textboxPercent.Text != "")
				{
					num = decimal.Parse(textBoxAmount.Text);
					decimal num2 = decimal.Parse(textboxPercent.Text) / 100m * num;
					textBoxExpAmount.Text = num2.ToString();
				}
				else if (textBoxExpAmount.Text != "")
				{
					num = decimal.Parse(textBoxAmount.Text);
					decimal num3 = decimal.Parse(textBoxExpAmount.Text) / num * 100m;
					textboxPercent.Text = num3.ToString();
				}
			}
		}

		private void textboxPercent_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBoxExpAmount_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBoxExpAmount_TabIndexChanged(object sender, EventArgs e)
		{
			if (textBoxAmount.Text != "" && textBoxExpAmount.Text != "")
			{
				Amount = decimal.Parse(textBoxAmount.Text);
				decimal num = decimal.Parse(textBoxExpAmount.Text) / Amount * 100m;
				textboxPercent.Text = num.ToString();
			}
		}

		private void textboxPercent_TabIndexChanged(object sender, EventArgs e)
		{
		}

		private void textboxPercent_Validated(object sender, EventArgs e)
		{
			if (textBoxAmount.Text != "" && textboxPercent.Text != "")
			{
				Amount = decimal.Parse(textBoxAmount.Text);
				decimal num = decimal.Parse(textboxPercent.Text) / 100m * Amount;
				textBoxExpAmount.Text = num.ToString();
			}
		}

		private void textBoxExpAmount_Validated(object sender, EventArgs e)
		{
			if (textBoxAmount.Text != "" && textBoxExpAmount.Text != "")
			{
				Amount = decimal.Parse(textBoxAmount.Text);
				decimal d = decimal.Parse(textBoxExpAmount.Text);
				if (Amount != 0m)
				{
					decimal num = d / Amount * 100m;
					textboxPercent.Text = num.ToString();
				}
			}
		}

		private void labelAttributeID1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProperty(comboBoxProperty.SelectedID);
		}

		private void labelAttributeID2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPropertyUnit(comboBoxPropertyUnit.SelectedID);
		}

		private void buttonSelectInvoice_Click(object sender, EventArgs e)
		{
			CustomerPaymentAllocationForm customerPaymentAllocationForm = new CustomerPaymentAllocationForm();
			customerPaymentAllocationForm.IsCashOrCard = true;
			customerPaymentAllocationForm.SetData(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, payeeSelector1.SelectedID, -1, isPDC: false);
			if (customerPaymentAllocationForm.ShowDialog() == DialogResult.OK)
			{
				textBoxAmount.Tag = customerPaymentAllocationForm.PaymentData;
				decimal num = default(decimal);
				foreach (DataRow row in customerPaymentAllocationForm.PaymentData.Tables["AR_Payment_Allocation"].Rows)
				{
					if (!string.IsNullOrEmpty(row["PaymentAmount"].ToString()))
					{
						num += decimal.Parse(row["PaymentAmount"].ToString());
					}
				}
				textBoxAmount.Text = num.ToString(Format.TotalAmountFormat);
				textBoxAmount.ReadOnly = true;
			}
			else
			{
				textBoxAmount.ReadOnly = false;
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void toolStripButtonSignature_Click(object sender, EventArgs e)
		{
			if (payeeSelector1.SelectedType == "C" && payeeSelector1.SelectedID != "")
			{
				Form obj = new Form
				{
					Size = new Size(200, 150),
					StartPosition = FormStartPosition.CenterParent
				};
				bool maximizeBox = obj.MinimizeBox = false;
				obj.MaximizeBox = maximizeBox;
				obj.FormBorderStyle = FormBorderStyle.FixedSingle;
				PictureBox pictureBox = new PictureBox();
				obj.Controls.Add(pictureBox);
				pictureBox.Dock = DockStyle.Fill;
				pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
				pictureBox.Image = PublicFunctions.GetCustomerSignatureThumbnailImage(payeeSelector1.SelectedID);
				obj.ShowDialog();
			}
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditExpense(comboBoxExpenseCode.SelectedID);
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAnalysis(comboBoxAnalysis.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.CashReceiptForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonSignature = new System.Windows.Forms.ToolStripButton();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripButtonApproval = new System.Windows.Forms.ToolStripButton();
			toolStripLabelApproval = new System.Windows.Forms.ToolStripLabel();
			toolStripSeparatorApproval = new System.Windows.Forms.ToolStripSeparator();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			labelCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxPayeeName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelVoided = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			textBoxDescription = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			panelDetails = new System.Windows.Forms.Panel();
			buttonSelectInvoice = new Micromind.UISupport.XPButton();
			panelProperty = new Infragistics.Win.Misc.UltraPanel();
			comboBoxPropertyUnit = new Micromind.DataControls.PropertyUnitComboBox();
			labelAttributeID1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelAttributeID2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxProperty = new Micromind.DataControls.PropertyComboBox();
			labelAnalysis = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelExpenseCode = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textboxPercent = new Micromind.UISupport.PercentTextBox();
			comboBoxAnalysis = new Micromind.DataControls.AnalysisComboBox();
			labelExpAmount = new System.Windows.Forms.Label();
			textBoxExpAmount = new Micromind.UISupport.AmountTextBox();
			labelPercent = new System.Windows.Forms.Label();
			comboBoxExpenseCode = new Micromind.DataControls.ExpenseCodeComboBox();
			panelJobCC = new System.Windows.Forms.Panel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			label5 = new System.Windows.Forms.Label();
			comboBoxPaymentMethod = new Micromind.DataControls.PaymentMethodTypesComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			comboBoxRegister = new Micromind.DataControls.RegisterComboBox();
			dateTimePickerDate = new Micromind.UISupport.flatDatePicker();
			textBoxBalance = new System.Windows.Forms.TextBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			comboBoxCostCenter = new Micromind.DataControls.CostCenterComboBox();
			payeeSelector1 = new Micromind.DataControls.PayeeSelector();
			currencySelector = new Micromind.DataControls.CurrencySelector();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			toolStripSeparatorSign = new System.Windows.Forms.ToolStripSeparator();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			panelProperty.ClientArea.SuspendLayout();
			panelProperty.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxExpenseCode).BeginInit();
			panelJobCC.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethod).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRegister).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[23]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator5,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator4,
				toolStripButtonSignature,
				toolStripSeparatorSign,
				toolStripButtonDistribution,
				toolStripDropDownButton1,
				toolStripButtonInformation,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(664, 31);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonSignature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonSignature.Image = Micromind.ClientUI.Properties.Resources.signatureicon;
			toolStripButtonSignature.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonSignature.Name = "toolStripButtonSignature";
			toolStripButtonSignature.Size = new System.Drawing.Size(28, 28);
			toolStripButtonSignature.Text = "Customer Signature";
			toolStripButtonSignature.Visible = false;
			toolStripButtonSignature.Click += new System.EventHandler(toolStripButtonSignature_Click);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				duplicateToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 19);
			toolStripDropDownButton1.Text = "Actions";
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			duplicateToolStripMenuItem.Text = "Copy";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
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
			toolStripSeparatorApproval.Size = new System.Drawing.Size(6, 25);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 296);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(664, 40);
			panelButtons.TabIndex = 1;
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
			linePanelDown.Size = new System.Drawing.Size(664, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(554, 8);
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
			textBoxVoucherNumber.Location = new System.Drawing.Point(315, 7);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(122, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 121);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(98, 117);
			textBoxRef1.MaxLength = 15;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(295, 20);
			textBoxRef1.TabIndex = 15;
			textBoxNote.Location = new System.Drawing.Point(98, 161);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(525, 20);
			textBoxNote.TabIndex = 17;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(3, 167);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "False";
			labelCurrency.Appearance = appearance;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(388, 32);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 14);
			labelCurrency.TabIndex = 4;
			labelCurrency.TabStop = true;
			labelCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCurrency.Value = "Currency:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			labelCurrency.VisitedLinkAppearance = appearance2;
			labelCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance3;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(212, 11);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 110;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance5;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(443, 9);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(55, 15);
			ultraFormattedLinkLabel6.TabIndex = 121;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Register:";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance6;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			textBoxPayeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPayeeName.Location = new System.Drawing.Point(427, 51);
			textBoxPayeeName.Name = "textBoxPayeeName";
			textBoxPayeeName.ReadOnly = true;
			textBoxPayeeName.Size = new System.Drawing.Size(210, 20);
			textBoxPayeeName.TabIndex = 7;
			textBoxPayeeName.TabStop = false;
			appearance7.FontData.BoldAsString = "True";
			appearance7.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance7;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(3, 10);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 116;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance8.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance8;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			appearance9.FontData.BoldAsString = "False";
			appearance9.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance9;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(3, 31);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(65, 15);
			ultraFormattedLinkLabel4.TabIndex = 114;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Cost Center:";
			appearance10.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance10;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance11.FontData.BoldAsString = "True";
			appearance11.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance11;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(3, 53);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(90, 15);
			ultraFormattedLinkLabel3.TabIndex = 112;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Received From:";
			appearance12.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance12;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			labelVoided.BackColor = System.Drawing.Color.Transparent;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(510, 194);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(91, 22);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(240, 76);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(90, 13);
			label2.TabIndex = 124;
			label2.Text = "Payment Method:";
			textBoxDescription.Location = new System.Drawing.Point(98, 139);
			textBoxDescription.MaxLength = 255;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.Size = new System.Drawing.Size(525, 20);
			textBoxDescription.TabIndex = 16;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(3, 145);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(74, 13);
			label4.TabIndex = 127;
			label4.Text = "Received For:";
			panelDetails.Controls.Add(buttonSelectInvoice);
			panelDetails.Controls.Add(panelProperty);
			panelDetails.Controls.Add(labelAnalysis);
			panelDetails.Controls.Add(labelExpenseCode);
			panelDetails.Controls.Add(textboxPercent);
			panelDetails.Controls.Add(comboBoxAnalysis);
			panelDetails.Controls.Add(labelExpAmount);
			panelDetails.Controls.Add(textBoxExpAmount);
			panelDetails.Controls.Add(labelPercent);
			panelDetails.Controls.Add(comboBoxExpenseCode);
			panelDetails.Controls.Add(panelJobCC);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(textBoxDescription);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(comboBoxPaymentMethod);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(textBoxAmount);
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(comboBoxRegister);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(textBoxBalance);
			panelDetails.Controls.Add(textBoxPayeeName);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxCostCenter);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(payeeSelector1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(currencySelector);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(labelVoided);
			panelDetails.Location = new System.Drawing.Point(10, 34);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(648, 256);
			panelDetails.TabIndex = 0;
			buttonSelectInvoice.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectInvoice.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectInvoice.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectInvoice.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectInvoice.Location = new System.Drawing.Point(209, 72);
			buttonSelectInvoice.Name = "buttonSelectInvoice";
			buttonSelectInvoice.Size = new System.Drawing.Size(27, 21);
			buttonSelectInvoice.TabIndex = 164;
			buttonSelectInvoice.Text = "...";
			buttonSelectInvoice.UseVisualStyleBackColor = false;
			buttonSelectInvoice.Visible = false;
			buttonSelectInvoice.Click += new System.EventHandler(buttonSelectInvoice_Click);
			panelProperty.ClientArea.Controls.Add(comboBoxPropertyUnit);
			panelProperty.ClientArea.Controls.Add(labelAttributeID1);
			panelProperty.ClientArea.Controls.Add(labelAttributeID2);
			panelProperty.ClientArea.Controls.Add(comboBoxProperty);
			panelProperty.Location = new System.Drawing.Point(3, 209);
			panelProperty.Name = "panelProperty";
			panelProperty.Size = new System.Drawing.Size(471, 34);
			panelProperty.TabIndex = 147;
			comboBoxPropertyUnit.Assigned = false;
			comboBoxPropertyUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPropertyUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPropertyUnit.CustomReportFieldName = "";
			comboBoxPropertyUnit.CustomReportKey = "";
			comboBoxPropertyUnit.CustomReportValueType = 1;
			comboBoxPropertyUnit.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPropertyUnit.DisplayLayout.Appearance = appearance13;
			comboBoxPropertyUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPropertyUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxPropertyUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPropertyUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPropertyUnit.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPropertyUnit.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxPropertyUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPropertyUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPropertyUnit.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxPropertyUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPropertyUnit.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxPropertyUnit.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxPropertyUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPropertyUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxPropertyUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPropertyUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPropertyUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPropertyUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPropertyUnit.Editable = true;
			comboBoxPropertyUnit.FilterString = "";
			comboBoxPropertyUnit.HasAllAccount = false;
			comboBoxPropertyUnit.HasCustom = false;
			comboBoxPropertyUnit.IsDataLoaded = false;
			comboBoxPropertyUnit.Location = new System.Drawing.Point(316, 4);
			comboBoxPropertyUnit.MaxDropDownItems = 12;
			comboBoxPropertyUnit.Name = "comboBoxPropertyUnit";
			comboBoxPropertyUnit.ShowActiveOnly = false;
			comboBoxPropertyUnit.ShowInactiveItems = false;
			comboBoxPropertyUnit.ShowQuickAdd = true;
			comboBoxPropertyUnit.Size = new System.Drawing.Size(133, 20);
			comboBoxPropertyUnit.TabIndex = 149;
			comboBoxPropertyUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance25.ForeColor = System.Drawing.Color.Blue;
			labelAttributeID1.ActiveLinkAppearance = appearance25;
			appearance26.FontData.BoldAsString = "False";
			appearance26.FontData.Name = "Tahoma";
			labelAttributeID1.Appearance = appearance26;
			labelAttributeID1.AutoSize = true;
			labelAttributeID1.Location = new System.Drawing.Point(2, 6);
			labelAttributeID1.Name = "labelAttributeID1";
			labelAttributeID1.Size = new System.Drawing.Size(49, 15);
			labelAttributeID1.TabIndex = 148;
			labelAttributeID1.TabStop = true;
			labelAttributeID1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelAttributeID1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelAttributeID1.Value = "Property:";
			appearance27.ForeColor = System.Drawing.Color.Blue;
			labelAttributeID1.VisitedLinkAppearance = appearance27;
			labelAttributeID1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelAttributeID1_LinkClicked);
			appearance28.FontData.BoldAsString = "False";
			appearance28.FontData.Name = "Tahoma";
			labelAttributeID2.Appearance = appearance28;
			labelAttributeID2.AutoSize = true;
			labelAttributeID2.Location = new System.Drawing.Point(234, 7);
			labelAttributeID2.Name = "labelAttributeID2";
			labelAttributeID2.Size = new System.Drawing.Size(72, 15);
			labelAttributeID2.TabIndex = 147;
			labelAttributeID2.TabStop = true;
			labelAttributeID2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelAttributeID2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelAttributeID2.Value = "Property Unit:";
			appearance29.ForeColor = System.Drawing.Color.Blue;
			labelAttributeID2.VisitedLinkAppearance = appearance29;
			labelAttributeID2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelAttributeID2_LinkClicked);
			comboBoxProperty.Assigned = false;
			comboBoxProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProperty.CustomReportFieldName = "";
			comboBoxProperty.CustomReportKey = "";
			comboBoxProperty.CustomReportValueType = 1;
			comboBoxProperty.DescriptionTextBox = null;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			appearance30.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProperty.DisplayLayout.Appearance = appearance30;
			comboBoxProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance31.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance31.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance31.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.GroupByBox.Appearance = appearance31;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance32;
			comboBoxProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance33.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance33.BackColor2 = System.Drawing.SystemColors.Control;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance33;
			comboBoxProperty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			appearance34.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProperty.DisplayLayout.Override.ActiveCellAppearance = appearance34;
			appearance35.BackColor = System.Drawing.SystemColors.Highlight;
			appearance35.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProperty.DisplayLayout.Override.ActiveRowAppearance = appearance35;
			comboBoxProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.CardAreaAppearance = appearance36;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			appearance37.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProperty.DisplayLayout.Override.CellAppearance = appearance37;
			comboBoxProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProperty.DisplayLayout.Override.CellPadding = 0;
			appearance38.BackColor = System.Drawing.SystemColors.Control;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.GroupByRowAppearance = appearance38;
			appearance39.TextHAlignAsString = "Left";
			comboBoxProperty.DisplayLayout.Override.HeaderAppearance = appearance39;
			comboBoxProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			appearance40.BorderColor = System.Drawing.Color.Silver;
			comboBoxProperty.DisplayLayout.Override.RowAppearance = appearance40;
			comboBoxProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance41.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance41;
			comboBoxProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProperty.Editable = true;
			comboBoxProperty.FilterString = "";
			comboBoxProperty.HasAllAccount = false;
			comboBoxProperty.HasCustom = false;
			comboBoxProperty.IsDataLoaded = false;
			comboBoxProperty.Location = new System.Drawing.Point(95, 3);
			comboBoxProperty.MaxDropDownItems = 12;
			comboBoxProperty.Name = "comboBoxProperty";
			comboBoxProperty.ShowInactiveItems = false;
			comboBoxProperty.ShowQuickAdd = true;
			comboBoxProperty.Size = new System.Drawing.Size(133, 20);
			comboBoxProperty.TabIndex = 145;
			comboBoxProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance42.FontData.BoldAsString = "False";
			appearance42.FontData.Name = "Tahoma";
			labelAnalysis.Appearance = appearance42;
			labelAnalysis.AutoSize = true;
			labelAnalysis.Location = new System.Drawing.Point(459, 99);
			labelAnalysis.Name = "labelAnalysis";
			labelAnalysis.Size = new System.Drawing.Size(47, 15);
			labelAnalysis.TabIndex = 144;
			labelAnalysis.TabStop = true;
			labelAnalysis.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelAnalysis.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelAnalysis.Value = "Analysis:";
			appearance43.ForeColor = System.Drawing.Color.Blue;
			labelAnalysis.VisitedLinkAppearance = appearance43;
			labelAnalysis.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel9_LinkClicked);
			appearance44.FontData.BoldAsString = "False";
			appearance44.FontData.Name = "Tahoma";
			labelExpenseCode.Appearance = appearance44;
			labelExpenseCode.AutoSize = true;
			labelExpenseCode.Location = new System.Drawing.Point(3, 98);
			labelExpenseCode.Name = "labelExpenseCode";
			labelExpenseCode.Size = new System.Drawing.Size(77, 15);
			labelExpenseCode.TabIndex = 143;
			labelExpenseCode.TabStop = true;
			labelExpenseCode.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelExpenseCode.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelExpenseCode.Value = "Expense Code:";
			appearance45.ForeColor = System.Drawing.Color.Blue;
			labelExpenseCode.VisitedLinkAppearance = appearance45;
			labelExpenseCode.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			textboxPercent.BackColor = System.Drawing.Color.WhiteSmoke;
			textboxPercent.CustomReportFieldName = "";
			textboxPercent.CustomReportKey = "";
			textboxPercent.CustomReportValueType = 1;
			textboxPercent.IsComboTextBox = false;
			textboxPercent.IsModified = false;
			textboxPercent.Location = new System.Drawing.Point(263, 96);
			textboxPercent.Name = "textboxPercent";
			textboxPercent.ReadOnly = true;
			textboxPercent.Size = new System.Drawing.Size(56, 20);
			textboxPercent.TabIndex = 12;
			textboxPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textboxPercent.TabIndexChanged += new System.EventHandler(textboxPercent_TabIndexChanged);
			textboxPercent.TextChanged += new System.EventHandler(textboxPercent_TextChanged);
			textboxPercent.Validated += new System.EventHandler(textboxPercent_Validated);
			comboBoxAnalysis.Assigned = false;
			comboBoxAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAnalysis.CustomReportFieldName = "";
			comboBoxAnalysis.CustomReportKey = "";
			comboBoxAnalysis.CustomReportValueType = 1;
			comboBoxAnalysis.DescriptionTextBox = null;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			appearance46.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAnalysis.DisplayLayout.Appearance = appearance46;
			comboBoxAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance47.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.GroupByBox.Appearance = appearance47;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance48;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance49.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance49.BackColor2 = System.Drawing.SystemColors.Control;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance49;
			comboBoxAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			appearance50.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance50;
			appearance51.BackColor = System.Drawing.SystemColors.Highlight;
			appearance51.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance51;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance52.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance52;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			appearance53.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAnalysis.DisplayLayout.Override.CellAppearance = appearance53;
			comboBoxAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance54.BackColor = System.Drawing.SystemColors.Control;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance54;
			appearance55.TextHAlignAsString = "Left";
			comboBoxAnalysis.DisplayLayout.Override.HeaderAppearance = appearance55;
			comboBoxAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance56.BackColor = System.Drawing.SystemColors.Window;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			comboBoxAnalysis.DisplayLayout.Override.RowAppearance = appearance56;
			comboBoxAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance57.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance57;
			comboBoxAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAnalysis.Editable = true;
			comboBoxAnalysis.FilterString = "";
			comboBoxAnalysis.HasAllAccount = false;
			comboBoxAnalysis.HasCustom = false;
			comboBoxAnalysis.IsDataLoaded = false;
			comboBoxAnalysis.Location = new System.Drawing.Point(511, 96);
			comboBoxAnalysis.MaxDropDownItems = 12;
			comboBoxAnalysis.Name = "comboBoxAnalysis";
			comboBoxAnalysis.ShowInactiveItems = false;
			comboBoxAnalysis.ShowQuickAdd = true;
			comboBoxAnalysis.Size = new System.Drawing.Size(126, 20);
			comboBoxAnalysis.TabIndex = 14;
			comboBoxAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAnalysis.Visible = false;
			labelExpAmount.AutoSize = true;
			labelExpAmount.Location = new System.Drawing.Point(320, 100);
			labelExpAmount.Name = "labelExpAmount";
			labelExpAmount.Size = new System.Drawing.Size(67, 13);
			labelExpAmount.TabIndex = 140;
			labelExpAmount.Text = "Exp.Amount:";
			textBoxExpAmount.AllowDecimal = true;
			textBoxExpAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxExpAmount.CustomReportFieldName = "";
			textBoxExpAmount.CustomReportKey = "";
			textBoxExpAmount.CustomReportValueType = 1;
			textBoxExpAmount.IsComboTextBox = false;
			textBoxExpAmount.IsModified = false;
			textBoxExpAmount.Location = new System.Drawing.Point(391, 96);
			textBoxExpAmount.MaxLength = 15;
			textBoxExpAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxExpAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxExpAmount.Name = "textBoxExpAmount";
			textBoxExpAmount.NullText = "0";
			textBoxExpAmount.ReadOnly = true;
			textBoxExpAmount.Size = new System.Drawing.Size(62, 20);
			textBoxExpAmount.TabIndex = 13;
			textBoxExpAmount.Text = "0.00";
			textBoxExpAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxExpAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxExpAmount.TabIndexChanged += new System.EventHandler(textBoxExpAmount_TabIndexChanged);
			textBoxExpAmount.TextChanged += new System.EventHandler(textBoxExpAmount_TextChanged);
			textBoxExpAmount.Validated += new System.EventHandler(textBoxExpAmount_Validated);
			labelPercent.AutoSize = true;
			labelPercent.Location = new System.Drawing.Point(216, 99);
			labelPercent.Name = "labelPercent";
			labelPercent.Size = new System.Drawing.Size(47, 13);
			labelPercent.TabIndex = 137;
			labelPercent.Text = "Percent:";
			comboBoxExpenseCode.Assigned = false;
			comboBoxExpenseCode.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxExpenseCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxExpenseCode.CustomReportFieldName = "";
			comboBoxExpenseCode.CustomReportKey = "";
			comboBoxExpenseCode.CustomReportValueType = 1;
			comboBoxExpenseCode.DescriptionTextBox = null;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			appearance58.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxExpenseCode.DisplayLayout.Appearance = appearance58;
			comboBoxExpenseCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxExpenseCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance59.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.Appearance = appearance59;
			appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance60;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance61.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance61.BackColor2 = System.Drawing.SystemColors.Control;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExpenseCode.DisplayLayout.GroupByBox.PromptAppearance = appearance61;
			comboBoxExpenseCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxExpenseCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance62.BackColor = System.Drawing.SystemColors.Window;
			appearance62.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxExpenseCode.DisplayLayout.Override.ActiveCellAppearance = appearance62;
			appearance63.BackColor = System.Drawing.SystemColors.Highlight;
			appearance63.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxExpenseCode.DisplayLayout.Override.ActiveRowAppearance = appearance63;
			comboBoxExpenseCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxExpenseCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseCode.DisplayLayout.Override.CardAreaAppearance = appearance64;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			appearance65.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxExpenseCode.DisplayLayout.Override.CellAppearance = appearance65;
			comboBoxExpenseCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxExpenseCode.DisplayLayout.Override.CellPadding = 0;
			appearance66.BackColor = System.Drawing.SystemColors.Control;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseCode.DisplayLayout.Override.GroupByRowAppearance = appearance66;
			appearance67.TextHAlignAsString = "Left";
			comboBoxExpenseCode.DisplayLayout.Override.HeaderAppearance = appearance67;
			comboBoxExpenseCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxExpenseCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance68.BackColor = System.Drawing.SystemColors.Window;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			comboBoxExpenseCode.DisplayLayout.Override.RowAppearance = appearance68;
			comboBoxExpenseCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance69.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxExpenseCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance69;
			comboBoxExpenseCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxExpenseCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxExpenseCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxExpenseCode.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxExpenseCode.Editable = true;
			comboBoxExpenseCode.FilterString = "";
			comboBoxExpenseCode.HasAllAccount = false;
			comboBoxExpenseCode.HasCustom = false;
			comboBoxExpenseCode.IsDataLoaded = false;
			comboBoxExpenseCode.Location = new System.Drawing.Point(98, 95);
			comboBoxExpenseCode.MaxDropDownItems = 12;
			comboBoxExpenseCode.Name = "comboBoxExpenseCode";
			comboBoxExpenseCode.ReadOnly = true;
			comboBoxExpenseCode.ShowInactiveItems = false;
			comboBoxExpenseCode.ShowQuickAdd = true;
			comboBoxExpenseCode.Size = new System.Drawing.Size(111, 20);
			comboBoxExpenseCode.TabIndex = 11;
			comboBoxExpenseCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			panelJobCC.Controls.Add(ultraFormattedLinkLabel7);
			panelJobCC.Controls.Add(comboBoxJob);
			panelJobCC.Controls.Add(ultraFormattedLinkLabel1);
			panelJobCC.Controls.Add(comboBoxCostCategory);
			panelJobCC.Location = new System.Drawing.Point(3, 185);
			panelJobCC.Name = "panelJobCC";
			panelJobCC.Size = new System.Drawing.Size(471, 28);
			panelJobCC.TabIndex = 132;
			panelJobCC.Visible = false;
			appearance70.FontData.BoldAsString = "False";
			appearance70.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance70;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(234, 4);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(76, 15);
			ultraFormattedLinkLabel7.TabIndex = 132;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Cost Category:";
			appearance71.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance71;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJob.CustomReportFieldName = "";
			comboBoxJob.CustomReportKey = "";
			comboBoxJob.CustomReportValueType = 1;
			comboBoxJob.DescriptionTextBox = null;
			comboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJob.Editable = true;
			comboBoxJob.FilterString = "";
			comboBoxJob.HasAllAccount = false;
			comboBoxJob.HasCustom = false;
			comboBoxJob.IsDataLoaded = false;
			comboBoxJob.Location = new System.Drawing.Point(95, 2);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(133, 20);
			comboBoxJob.TabIndex = 1;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance72.FontData.BoldAsString = "False";
			appearance72.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance72;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(3, 4);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(42, 15);
			ultraFormattedLinkLabel1.TabIndex = 0;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Project:";
			appearance73.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance73;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked_1);
			comboBoxCostCategory.Assigned = false;
			comboBoxCostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCategory.CustomReportFieldName = "";
			comboBoxCostCategory.CustomReportKey = "";
			comboBoxCostCategory.CustomReportValueType = 1;
			comboBoxCostCategory.DescriptionTextBox = null;
			appearance74.BackColor = System.Drawing.SystemColors.Window;
			appearance74.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCostCategory.DisplayLayout.Appearance = appearance74;
			comboBoxCostCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCostCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance75.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance75.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance75.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.GroupByBox.Appearance = appearance75;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance76;
			comboBoxCostCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance77.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance77.BackColor2 = System.Drawing.SystemColors.Control;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance77;
			comboBoxCostCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCostCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance78.BackColor = System.Drawing.SystemColors.Window;
			appearance78.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCostCategory.DisplayLayout.Override.ActiveCellAppearance = appearance78;
			appearance79.BackColor = System.Drawing.SystemColors.Highlight;
			appearance79.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCostCategory.DisplayLayout.Override.ActiveRowAppearance = appearance79;
			comboBoxCostCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCostCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance80.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.Override.CardAreaAppearance = appearance80;
			appearance81.BorderColor = System.Drawing.Color.Silver;
			appearance81.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCostCategory.DisplayLayout.Override.CellAppearance = appearance81;
			comboBoxCostCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCostCategory.DisplayLayout.Override.CellPadding = 0;
			appearance82.BackColor = System.Drawing.SystemColors.Control;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.Override.GroupByRowAppearance = appearance82;
			appearance83.TextHAlignAsString = "Left";
			comboBoxCostCategory.DisplayLayout.Override.HeaderAppearance = appearance83;
			comboBoxCostCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCostCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance84.BackColor = System.Drawing.SystemColors.Window;
			appearance84.BorderColor = System.Drawing.Color.Silver;
			comboBoxCostCategory.DisplayLayout.Override.RowAppearance = appearance84;
			comboBoxCostCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance85.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCostCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance85;
			comboBoxCostCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCostCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCostCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCategory.Editable = true;
			comboBoxCostCategory.FilterString = "";
			comboBoxCostCategory.HasAllAccount = false;
			comboBoxCostCategory.HasCustom = false;
			comboBoxCostCategory.IsDataLoaded = false;
			comboBoxCostCategory.Location = new System.Drawing.Point(316, 2);
			comboBoxCostCategory.MaxDropDownItems = 12;
			comboBoxCostCategory.Name = "comboBoxCostCategory";
			comboBoxCostCategory.ShowInactiveItems = false;
			comboBoxCostCategory.ShowQuickAdd = true;
			comboBoxCostCategory.Size = new System.Drawing.Size(133, 20);
			comboBoxCostCategory.TabIndex = 2;
			comboBoxCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(430, 76);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(49, 13);
			label5.TabIndex = 128;
			label5.Text = "Balance:";
			comboBoxPaymentMethod.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			comboBoxPaymentMethod.HideARAccount = false;
			comboBoxPaymentMethod.HideCheque = true;
			comboBoxPaymentMethod.Location = new System.Drawing.Point(334, 72);
			comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
			comboBoxPaymentMethod.Size = new System.Drawing.Size(95, 21);
			comboBoxPaymentMethod.TabIndex = 9;
			comboBoxPaymentMethod.ValueChanged += new System.EventHandler(comboBoxPaymentMethod_ValueChanged);
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(3, 76);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(53, 13);
			mmLabel2.TabIndex = 123;
			mmLabel2.Text = "Amount:";
			textBoxAmount.AllowDecimal = true;
			textBoxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.IsModified = false;
			textBoxAmount.Location = new System.Drawing.Point(98, 73);
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
			textBoxAmount.Size = new System.Drawing.Size(111, 20);
			textBoxAmount.TabIndex = 8;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxAmount.TextChanged += new System.EventHandler(textBoxAmount_TextChanged);
			comboBoxRegister.Assigned = false;
			comboBoxRegister.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRegister.CustomReportFieldName = "";
			comboBoxRegister.CustomReportKey = "";
			comboBoxRegister.CustomReportValueType = 1;
			comboBoxRegister.DescriptionTextBox = null;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			appearance86.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRegister.DisplayLayout.Appearance = appearance86;
			comboBoxRegister.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRegister.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance87.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance87.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance87.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance87.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.GroupByBox.Appearance = appearance87;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRegister.DisplayLayout.GroupByBox.BandLabelAppearance = appearance88;
			comboBoxRegister.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance89.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance89.BackColor2 = System.Drawing.SystemColors.Control;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRegister.DisplayLayout.GroupByBox.PromptAppearance = appearance89;
			comboBoxRegister.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRegister.DisplayLayout.MaxRowScrollRegions = 1;
			appearance90.BackColor = System.Drawing.SystemColors.Window;
			appearance90.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRegister.DisplayLayout.Override.ActiveCellAppearance = appearance90;
			appearance91.BackColor = System.Drawing.SystemColors.Highlight;
			appearance91.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRegister.DisplayLayout.Override.ActiveRowAppearance = appearance91;
			comboBoxRegister.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRegister.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.Override.CardAreaAppearance = appearance92;
			appearance93.BorderColor = System.Drawing.Color.Silver;
			appearance93.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRegister.DisplayLayout.Override.CellAppearance = appearance93;
			comboBoxRegister.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRegister.DisplayLayout.Override.CellPadding = 0;
			appearance94.BackColor = System.Drawing.SystemColors.Control;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.Override.GroupByRowAppearance = appearance94;
			appearance95.TextHAlignAsString = "Left";
			comboBoxRegister.DisplayLayout.Override.HeaderAppearance = appearance95;
			comboBoxRegister.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRegister.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance96.BackColor = System.Drawing.SystemColors.Window;
			appearance96.BorderColor = System.Drawing.Color.Silver;
			comboBoxRegister.DisplayLayout.Override.RowAppearance = appearance96;
			comboBoxRegister.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance97.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRegister.DisplayLayout.Override.TemplateAddRowAppearance = appearance97;
			comboBoxRegister.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRegister.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRegister.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRegister.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRegister.Editable = true;
			comboBoxRegister.FilterString = "";
			comboBoxRegister.HasAllAccount = false;
			comboBoxRegister.HasCustom = false;
			comboBoxRegister.IsDataLoaded = false;
			comboBoxRegister.Location = new System.Drawing.Point(514, 7);
			comboBoxRegister.MaxDropDownItems = 12;
			comboBoxRegister.Name = "comboBoxRegister";
			comboBoxRegister.ShowDefaultRegisterOnly = false;
			comboBoxRegister.ShowInactiveItems = false;
			comboBoxRegister.ShowQuickAdd = true;
			comboBoxRegister.Size = new System.Drawing.Size(123, 20);
			comboBoxRegister.TabIndex = 2;
			comboBoxRegister.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(256, 29);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(126, 20);
			dateTimePickerDate.TabIndex = 4;
			dateTimePickerDate.Value = new System.DateTime(2008, 5, 15, 11, 57, 20, 362);
			textBoxBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBalance.Location = new System.Drawing.Point(483, 73);
			textBoxBalance.Name = "textBoxBalance";
			textBoxBalance.ReadOnly = true;
			textBoxBalance.Size = new System.Drawing.Size(154, 20);
			textBoxBalance.TabIndex = 10;
			textBoxBalance.TabStop = false;
			textBoxBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance98;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance99.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance99.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance99.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance99.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance99;
			appearance100.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance100;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance101.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance101.BackColor2 = System.Drawing.SystemColors.Control;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance101;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance102.BackColor = System.Drawing.SystemColors.Window;
			appearance102.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance102;
			appearance103.BackColor = System.Drawing.SystemColors.Highlight;
			appearance103.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance103;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance104.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance104;
			appearance105.BorderColor = System.Drawing.Color.Silver;
			appearance105.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance105;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance106.BackColor = System.Drawing.SystemColors.Control;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance106;
			appearance107.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance107;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance108.BackColor = System.Drawing.SystemColors.Window;
			appearance108.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance108;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance109.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance109;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(98, 7);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(111, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCostCenter.Assigned = false;
			comboBoxCostCenter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCenter.CustomReportFieldName = "";
			comboBoxCostCenter.CustomReportKey = "";
			comboBoxCostCenter.CustomReportValueType = 1;
			comboBoxCostCenter.DescriptionTextBox = null;
			appearance110.BackColor = System.Drawing.SystemColors.Window;
			appearance110.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCostCenter.DisplayLayout.Appearance = appearance110;
			comboBoxCostCenter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCostCenter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance111.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance111.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance111.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance111.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.GroupByBox.Appearance = appearance111;
			appearance112.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance112;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance113.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance113.BackColor2 = System.Drawing.SystemColors.Control;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance113.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.PromptAppearance = appearance113;
			comboBoxCostCenter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCostCenter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance114.BackColor = System.Drawing.SystemColors.Window;
			appearance114.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveCellAppearance = appearance114;
			appearance115.BackColor = System.Drawing.SystemColors.Highlight;
			appearance115.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveRowAppearance = appearance115;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance116.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.CardAreaAppearance = appearance116;
			appearance117.BorderColor = System.Drawing.Color.Silver;
			appearance117.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCostCenter.DisplayLayout.Override.CellAppearance = appearance117;
			comboBoxCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCostCenter.DisplayLayout.Override.CellPadding = 0;
			appearance118.BackColor = System.Drawing.SystemColors.Control;
			appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance118.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance118.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.GroupByRowAppearance = appearance118;
			appearance119.TextHAlignAsString = "Left";
			comboBoxCostCenter.DisplayLayout.Override.HeaderAppearance = appearance119;
			comboBoxCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCostCenter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance120.BackColor = System.Drawing.SystemColors.Window;
			appearance120.BorderColor = System.Drawing.Color.Silver;
			comboBoxCostCenter.DisplayLayout.Override.RowAppearance = appearance120;
			comboBoxCostCenter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance121.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCostCenter.DisplayLayout.Override.TemplateAddRowAppearance = appearance121;
			comboBoxCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCostCenter.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCostCenter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCenter.Editable = true;
			comboBoxCostCenter.FilterString = "";
			comboBoxCostCenter.HasAllAccount = false;
			comboBoxCostCenter.HasCustom = false;
			comboBoxCostCenter.IsDataLoaded = false;
			comboBoxCostCenter.Location = new System.Drawing.Point(98, 29);
			comboBoxCostCenter.MaxDropDownItems = 12;
			comboBoxCostCenter.Name = "comboBoxCostCenter";
			comboBoxCostCenter.ShowInactiveItems = false;
			comboBoxCostCenter.ShowQuickAdd = true;
			comboBoxCostCenter.Size = new System.Drawing.Size(111, 20);
			comboBoxCostCenter.TabIndex = 3;
			comboBoxCostCenter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			payeeSelector1.BackColor = System.Drawing.Color.Transparent;
			payeeSelector1.Location = new System.Drawing.Point(98, 51);
			payeeSelector1.MaximumSize = new System.Drawing.Size(2000, 20);
			payeeSelector1.MinimumSize = new System.Drawing.Size(0, 20);
			payeeSelector1.Name = "payeeSelector1";
			payeeSelector1.SelectedID = "";
			payeeSelector1.SelectedType = "";
			payeeSelector1.Size = new System.Drawing.Size(323, 20);
			payeeSelector1.TabIndex = 6;
			currencySelector.BackColor = System.Drawing.Color.WhiteSmoke;
			currencySelector.Location = new System.Drawing.Point(443, 29);
			currencySelector.MaximumSize = new System.Drawing.Size(99999, 20);
			currencySelector.MinimumSize = new System.Drawing.Size(5, 20);
			currencySelector.Name = "currencySelector";
			currencySelector.Rate = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			currencySelector.SelectedID = "";
			currencySelector.Size = new System.Drawing.Size(194, 20);
			currencySelector.TabIndex = 5;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(212, 30);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
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
			toolStripSeparatorSign.Name = "toolStripSeparatorSign";
			toolStripSeparatorSign.Size = new System.Drawing.Size(6, 31);
			toolStripSeparatorSign.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(664, 336);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 166);
			base.Name = "CashReceiptForm";
			Text = "Cash/Card Receipt Voucher";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			panelProperty.ClientArea.ResumeLayout(false);
			panelProperty.ClientArea.PerformLayout();
			panelProperty.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxExpenseCode).EndInit();
			panelJobCC.ResumeLayout(false);
			panelJobCC.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethod).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRegister).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
