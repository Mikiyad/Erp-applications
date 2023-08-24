using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
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
	public class POSCashRegisterDetailsForm : Form, IForm
	{
		private POSCashRegisterData currentData;

		private const string TABLENAME_CONST = "POS_CashRegister";

		private const string IDFIELD_CONST = "CashRegisterID";

		private bool isNewRecord = true;

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

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private MMLabel mmLabel4;

		private MMTextBox textBoxNote;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private LocationComboBox locationComboBox1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private MMTextBox textBoxStoreName;

		private XPButton buttonPaymentMethod;

		private customersFlatComboBox comboBoxDefaultCustomer;

		private MMTextBox textBoxCustomerName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private AllAccountsComboBox comboBoxDiscountGiven;

		private MMTextBox textBoxDiscountGivenAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private SysDocComboBox comboBoxSysDocReceipt;

		private MMTextBox textBoxDocID;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private MMTextBox textBoxExpenseDocName;

		private SysDocComboBox comboBoxExpenseDocID;

		private XPButton xpButton2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private AllAccountsComboBox comboBoxPettyCashAccount;

		private MMTextBox textBoxPettyCashName;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1027;

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
					buttonDelete.Enabled = false;
					textBoxCode.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
				}
				XPButton xPButton = buttonPaymentMethod;
				bool enabled = xpButton2.Enabled = !value;
				xPButton.Enabled = enabled;
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

		public POSCashRegisterDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxSysDocReceipt.FilterByType(SysDocTypes.SalesPOS);
			comboBoxExpenseDocID.FilterByType(SysDocTypes.CashExpense);
		}

		private void AddEvents()
		{
			base.Load += POSCashRegisterDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new POSCashRegisterData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.POSCashRegisterTable.Rows[0] : currentData.POSCashRegisterTable.NewRow();
				dataRow.BeginEdit();
				dataRow["CashRegisterID"] = textBoxCode.Text.Trim();
				dataRow["CashRegisterName"] = textBoxName.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["LocationID"] = locationComboBox1.SelectedID;
				if (comboBoxDefaultCustomer.SelectedID == "")
				{
					dataRow["DefaultCustomerID"] = DBNull.Value;
				}
				else
				{
					dataRow["DefaultCustomerID"] = comboBoxDefaultCustomer.SelectedID;
				}
				if (comboBoxExpenseDocID.SelectedID == "")
				{
					dataRow["ExpenseDocID"] = DBNull.Value;
				}
				else
				{
					dataRow["ExpenseDocID"] = comboBoxExpenseDocID.SelectedID;
				}
				dataRow["ReceiptDocID"] = comboBoxSysDocReceipt.SelectedID;
				if (comboBoxDiscountGiven.SelectedID == "")
				{
					dataRow["DiscountAccountID"] = DBNull.Value;
				}
				else
				{
					dataRow["DiscountAccountID"] = comboBoxDiscountGiven.SelectedID;
				}
				if (comboBoxPettyCashAccount.SelectedID == "")
				{
					dataRow["PettyCashAccountID"] = DBNull.Value;
				}
				else
				{
					dataRow["PettyCashAccountID"] = comboBoxPettyCashAccount.SelectedID;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.POSCashRegisterTable.Rows.Add(dataRow);
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
					currentData = Factory.POSCashRegisterSystem.GetPOSCashRegisterByID(id.Trim());
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
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables[0].Rows[0];
				textBoxCode.Text = dataRow["CashRegisterID"].ToString();
				textBoxName.Text = dataRow["CashRegisterName"].ToString();
				comboBoxDiscountGiven.SelectedID = dataRow["DiscountAccountID"].ToString();
				comboBoxPettyCashAccount.SelectedID = dataRow["PettyCashAccountID"].ToString();
				comboBoxSysDocReceipt.SelectedID = dataRow["ReceiptDocID"].ToString();
				comboBoxExpenseDocID.SelectedID = dataRow["ExpenseDocID"].ToString();
				locationComboBox1.SelectedID = dataRow["LocationID"].ToString();
				comboBoxDefaultCustomer.SelectedID = dataRow["DefaultCustomerID"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
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
					flag = Factory.POSCashRegisterSystem.CreatePOSCashRegister(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.POSCashRegister, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.POSCashRegisterSystem.UpdatePOSCashRegister(currentData);
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
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (locationComboBox1.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a POS Store for this Cash Register.");
				return false;
			}
			if (comboBoxSysDocReceipt.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a System Document for this Cash Register.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("POS_CashRegister", "CashRegisterID", textBoxCode.Text.Trim()))
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
			comboBoxPettyCashAccount.Clear();
			textBoxCode.Clear();
			textBoxName.Clear();
			textBoxNote.Clear();
			locationComboBox1.Clear();
			comboBoxDefaultCustomer.Clear();
			comboBoxDiscountGiven.Clear();
			comboBoxSysDocReceipt.Clear();
			comboBoxExpenseDocID.Clear();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void POSCashRegisterGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void POSCashRegisterGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.POSCashRegisterSystem.DeletePOSCashRegister(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("POS_CashRegister", "CashRegisterID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("POS_CashRegister", "CashRegisterID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("POS_CashRegister", "CashRegisterID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("POS_CashRegister", "CashRegisterID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("POS_CashRegister", "CashRegisterID", toolStripTextBoxFind.Text.Trim()))
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

		private void POSCashRegisterDetailsForm_Load(object sender, EventArgs e)
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
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.POSCashRegister);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPOSLocation(locationComboBox1.SelectedID);
		}

		private void buttonPaymentMethod_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.POSCashRegisterPaymentMethodsFormObj);
			FormActivator.POSCashRegisterPaymentMethodsFormObj.SelectedRegister = textBoxCode.Text;
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomer(comboBoxDefaultCustomer.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxDiscountGiven.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDocReceipt.SelectedID, SysDocTypes.SalesPOS);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxExpenseDocID.SelectedID, SysDocTypes.CashExpense);
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.POSCashRegisterExpenseAccountsFormObj);
			FormActivator.POSCashRegisterExpenseAccountsFormObj.SelectedRegister = textBoxCode.Text;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.POSCashRegisterDetailsForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxStoreName = new Micromind.UISupport.MMTextBox();
			locationComboBox1 = new Micromind.DataControls.LocationComboBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			buttonPaymentMethod = new Micromind.UISupport.XPButton();
			comboBoxDefaultCustomer = new Micromind.DataControls.customersFlatComboBox();
			textBoxCustomerName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxDiscountGiven = new Micromind.DataControls.AllAccountsComboBox();
			textBoxDiscountGivenAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDocReceipt = new Micromind.DataControls.SysDocComboBox();
			textBoxDocID = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxExpenseDocName = new Micromind.UISupport.MMTextBox();
			comboBoxExpenseDocID = new Micromind.DataControls.SysDocComboBox();
			xpButton2 = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPettyCashAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxPettyCashName = new Micromind.UISupport.MMTextBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)locationComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountGiven).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDocReceipt).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxExpenseDocID).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPettyCashAccount).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
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
				toolStripSeparator2
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(557, 31);
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
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 327);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(557, 40);
			panelButtons.TabIndex = 18;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(557, 1);
			linePanelDown.TabIndex = 14;
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
			buttonDelete.TabIndex = 2;
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
			xpButton1.Location = new System.Drawing.Point(447, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
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
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(322, 37);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			appearance.FontData.BoldAsString = "True";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(9, 82);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(62, 14);
			ultraFormattedLinkLabel2.TabIndex = 19;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "POS Store:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			textBoxStoreName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxStoreName.CustomReportFieldName = "";
			textBoxStoreName.CustomReportKey = "";
			textBoxStoreName.CustomReportValueType = 1;
			textBoxStoreName.IsComboTextBox = false;
			textBoxStoreName.IsModified = false;
			textBoxStoreName.Location = new System.Drawing.Point(319, 80);
			textBoxStoreName.MaxLength = 255;
			textBoxStoreName.Name = "textBoxStoreName";
			textBoxStoreName.ReadOnly = true;
			textBoxStoreName.Size = new System.Drawing.Size(220, 20);
			textBoxStoreName.TabIndex = 4;
			textBoxStoreName.TabStop = false;
			locationComboBox1.AlwaysInEditMode = true;
			locationComboBox1.Assigned = false;
			locationComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			locationComboBox1.CustomReportFieldName = "";
			locationComboBox1.CustomReportKey = "";
			locationComboBox1.CustomReportValueType = 1;
			locationComboBox1.DescriptionTextBox = textBoxStoreName;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			locationComboBox1.DisplayLayout.Appearance = appearance3;
			locationComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			locationComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			locationComboBox1.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			locationComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			locationComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			locationComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			locationComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			locationComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			locationComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			locationComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			locationComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			locationComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			locationComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			locationComboBox1.DisplayLayout.Override.CellAppearance = appearance10;
			locationComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			locationComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			locationComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			locationComboBox1.DisplayLayout.Override.HeaderAppearance = appearance12;
			locationComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			locationComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			locationComboBox1.DisplayLayout.Override.RowAppearance = appearance13;
			locationComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			locationComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			locationComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			locationComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			locationComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			locationComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			locationComboBox1.Editable = true;
			locationComboBox1.FilterString = "";
			locationComboBox1.HasAllAccount = false;
			locationComboBox1.HasCustom = false;
			locationComboBox1.IsDataLoaded = false;
			locationComboBox1.Location = new System.Drawing.Point(137, 80);
			locationComboBox1.MaxDropDownItems = 12;
			locationComboBox1.Name = "locationComboBox1";
			locationComboBox1.ShowAll = false;
			locationComboBox1.ShowConsignIn = false;
			locationComboBox1.ShowConsignOut = false;
			locationComboBox1.ShowDefaultLocationOnly = false;
			locationComboBox1.ShowInactiveItems = false;
			locationComboBox1.ShowNormalLocations = false;
			locationComboBox1.ShowPOSOnly = true;
			locationComboBox1.ShowQuickAdd = true;
			locationComboBox1.ShowWarehouseOnly = false;
			locationComboBox1.Size = new System.Drawing.Size(179, 20);
			locationComboBox1.TabIndex = 3;
			locationComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(137, 238);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(402, 20);
			textBoxNote.TabIndex = 15;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(137, 57);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(324, 20);
			textBoxName.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(137, 35);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(179, 20);
			textBoxCode.TabIndex = 0;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(8, 240);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Note:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 59);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(126, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Cash Register Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(123, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Cash Register Code:";
			buttonPaymentMethod.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPaymentMethod.BackColor = System.Drawing.Color.DarkGray;
			buttonPaymentMethod.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPaymentMethod.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPaymentMethod.Enabled = false;
			buttonPaymentMethod.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonPaymentMethod.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPaymentMethod.Location = new System.Drawing.Point(137, 264);
			buttonPaymentMethod.Name = "buttonPaymentMethod";
			buttonPaymentMethod.Size = new System.Drawing.Size(130, 24);
			buttonPaymentMethod.TabIndex = 16;
			buttonPaymentMethod.Text = "Payment Methods...";
			buttonPaymentMethod.UseVisualStyleBackColor = false;
			buttonPaymentMethod.Click += new System.EventHandler(buttonPaymentMethod_Click);
			comboBoxDefaultCustomer.AlwaysInEditMode = true;
			comboBoxDefaultCustomer.Assigned = false;
			comboBoxDefaultCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDefaultCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDefaultCustomer.CustomReportFieldName = "";
			comboBoxDefaultCustomer.CustomReportKey = "";
			comboBoxDefaultCustomer.CustomReportValueType = 1;
			comboBoxDefaultCustomer.DescriptionTextBox = textBoxCustomerName;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDefaultCustomer.DisplayLayout.Appearance = appearance15;
			comboBoxDefaultCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDefaultCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultCustomer.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxDefaultCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefaultCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxDefaultCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDefaultCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDefaultCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDefaultCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxDefaultCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDefaultCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultCustomer.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDefaultCustomer.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxDefaultCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDefaultCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefaultCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxDefaultCustomer.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxDefaultCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDefaultCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxDefaultCustomer.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxDefaultCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDefaultCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			comboBoxDefaultCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDefaultCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDefaultCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDefaultCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDefaultCustomer.Editable = true;
			comboBoxDefaultCustomer.FilterString = "";
			comboBoxDefaultCustomer.FilterSysDocID = "";
			comboBoxDefaultCustomer.HasAll = false;
			comboBoxDefaultCustomer.HasCustom = false;
			comboBoxDefaultCustomer.IsDataLoaded = false;
			comboBoxDefaultCustomer.Location = new System.Drawing.Point(137, 128);
			comboBoxDefaultCustomer.MaxDropDownItems = 12;
			comboBoxDefaultCustomer.Name = "comboBoxDefaultCustomer";
			comboBoxDefaultCustomer.ShowConsignmentOnly = false;
			comboBoxDefaultCustomer.ShowInactive = false;
			comboBoxDefaultCustomer.ShowLPOCustomersOnly = false;
			comboBoxDefaultCustomer.ShowPROCustomersOnly = false;
			comboBoxDefaultCustomer.ShowQuickAdd = true;
			comboBoxDefaultCustomer.Size = new System.Drawing.Size(179, 20);
			comboBoxDefaultCustomer.TabIndex = 7;
			comboBoxDefaultCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.CustomReportFieldName = "";
			textBoxCustomerName.CustomReportKey = "";
			textBoxCustomerName.CustomReportValueType = 1;
			textBoxCustomerName.IsComboTextBox = false;
			textBoxCustomerName.IsModified = false;
			textBoxCustomerName.Location = new System.Drawing.Point(319, 128);
			textBoxCustomerName.MaxLength = 255;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(220, 20);
			textBoxCustomerName.TabIndex = 8;
			textBoxCustomerName.TabStop = false;
			appearance27.FontData.BoldAsString = "False";
			ultraFormattedLinkLabel1.Appearance = appearance27;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(9, 130);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(93, 14);
			ultraFormattedLinkLabel1.TabIndex = 22;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Default Customer:";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance28;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance29.FontData.BoldAsString = "False";
			appearance29.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance29;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(9, 153);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(92, 15);
			ultraFormattedLinkLabel5.TabIndex = 131;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Discount Account:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance30;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxDiscountGiven.AlwaysInEditMode = true;
			comboBoxDiscountGiven.Assigned = false;
			comboBoxDiscountGiven.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDiscountGiven.CustomReportFieldName = "";
			comboBoxDiscountGiven.CustomReportKey = "";
			comboBoxDiscountGiven.CustomReportValueType = 1;
			comboBoxDiscountGiven.DescriptionTextBox = textBoxDiscountGivenAccountName;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDiscountGiven.DisplayLayout.Appearance = appearance31;
			comboBoxDiscountGiven.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDiscountGiven.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			comboBoxDiscountGiven.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDiscountGiven.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDiscountGiven.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDiscountGiven.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			comboBoxDiscountGiven.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDiscountGiven.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDiscountGiven.DisplayLayout.Override.CellAppearance = appearance38;
			comboBoxDiscountGiven.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDiscountGiven.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderAppearance = appearance40;
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			comboBoxDiscountGiven.DisplayLayout.Override.RowAppearance = appearance41;
			comboBoxDiscountGiven.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDiscountGiven.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			comboBoxDiscountGiven.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDiscountGiven.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDiscountGiven.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDiscountGiven.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDiscountGiven.Editable = true;
			comboBoxDiscountGiven.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxDiscountGiven.FilterString = "";
			comboBoxDiscountGiven.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxDiscountGiven.FilterSysDocID = "";
			comboBoxDiscountGiven.HasAllAccount = false;
			comboBoxDiscountGiven.HasCustom = false;
			comboBoxDiscountGiven.IsDataLoaded = false;
			comboBoxDiscountGiven.Location = new System.Drawing.Point(137, 151);
			comboBoxDiscountGiven.MaxDropDownItems = 12;
			comboBoxDiscountGiven.Name = "comboBoxDiscountGiven";
			comboBoxDiscountGiven.ShowInactiveItems = false;
			comboBoxDiscountGiven.ShowQuickAdd = true;
			comboBoxDiscountGiven.Size = new System.Drawing.Size(179, 20);
			comboBoxDiscountGiven.TabIndex = 9;
			comboBoxDiscountGiven.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxDiscountGivenAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDiscountGivenAccountName.CustomReportFieldName = "";
			textBoxDiscountGivenAccountName.CustomReportKey = "";
			textBoxDiscountGivenAccountName.CustomReportValueType = 1;
			textBoxDiscountGivenAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxDiscountGivenAccountName.IsComboTextBox = false;
			textBoxDiscountGivenAccountName.IsModified = false;
			textBoxDiscountGivenAccountName.Location = new System.Drawing.Point(319, 151);
			textBoxDiscountGivenAccountName.MaxLength = 255;
			textBoxDiscountGivenAccountName.Name = "textBoxDiscountGivenAccountName";
			textBoxDiscountGivenAccountName.ReadOnly = true;
			textBoxDiscountGivenAccountName.Size = new System.Drawing.Size(220, 20);
			textBoxDiscountGivenAccountName.TabIndex = 10;
			textBoxDiscountGivenAccountName.TabStop = false;
			appearance43.FontData.BoldAsString = "True";
			appearance43.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance43;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(11, 105);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(82, 15);
			ultraFormattedLinkLabel3.TabIndex = 133;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Document ID:";
			appearance44.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance44;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			comboBoxSysDocReceipt.AlwaysInEditMode = true;
			comboBoxSysDocReceipt.Assigned = false;
			comboBoxSysDocReceipt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDocReceipt.CustomReportFieldName = "";
			comboBoxSysDocReceipt.CustomReportKey = "";
			comboBoxSysDocReceipt.CustomReportValueType = 1;
			comboBoxSysDocReceipt.DescriptionTextBox = textBoxDocID;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDocReceipt.DisplayLayout.Appearance = appearance45;
			comboBoxSysDocReceipt.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDocReceipt.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDocReceipt.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDocReceipt.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			comboBoxSysDocReceipt.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDocReceipt.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			comboBoxSysDocReceipt.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDocReceipt.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDocReceipt.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDocReceipt.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			comboBoxSysDocReceipt.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDocReceipt.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDocReceipt.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDocReceipt.DisplayLayout.Override.CellAppearance = appearance52;
			comboBoxSysDocReceipt.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDocReceipt.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDocReceipt.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			comboBoxSysDocReceipt.DisplayLayout.Override.HeaderAppearance = appearance54;
			comboBoxSysDocReceipt.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDocReceipt.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDocReceipt.DisplayLayout.Override.RowAppearance = appearance55;
			comboBoxSysDocReceipt.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDocReceipt.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
			comboBoxSysDocReceipt.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDocReceipt.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDocReceipt.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDocReceipt.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDocReceipt.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDocReceipt.Editable = true;
			comboBoxSysDocReceipt.ExcludeFromSecurity = false;
			comboBoxSysDocReceipt.FilterString = "";
			comboBoxSysDocReceipt.HasAllAccount = false;
			comboBoxSysDocReceipt.HasCustom = false;
			comboBoxSysDocReceipt.IsDataLoaded = false;
			comboBoxSysDocReceipt.Location = new System.Drawing.Point(137, 104);
			comboBoxSysDocReceipt.MaxDropDownItems = 12;
			comboBoxSysDocReceipt.Name = "comboBoxSysDocReceipt";
			comboBoxSysDocReceipt.ShowAll = false;
			comboBoxSysDocReceipt.ShowInactiveItems = false;
			comboBoxSysDocReceipt.ShowQuickAdd = true;
			comboBoxSysDocReceipt.Size = new System.Drawing.Size(179, 20);
			comboBoxSysDocReceipt.TabIndex = 5;
			comboBoxSysDocReceipt.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxDocID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDocID.CustomReportFieldName = "";
			textBoxDocID.CustomReportKey = "";
			textBoxDocID.CustomReportValueType = 1;
			textBoxDocID.IsComboTextBox = false;
			textBoxDocID.IsModified = false;
			textBoxDocID.Location = new System.Drawing.Point(319, 104);
			textBoxDocID.MaxLength = 255;
			textBoxDocID.Name = "textBoxDocID";
			textBoxDocID.ReadOnly = true;
			textBoxDocID.Size = new System.Drawing.Size(220, 20);
			textBoxDocID.TabIndex = 6;
			textBoxDocID.TabStop = false;
			appearance57.FontData.BoldAsString = "False";
			appearance57.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance57;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(9, 198);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(82, 15);
			ultraFormattedLinkLabel4.TabIndex = 134;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Expense DocID:";
			appearance58.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance58;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			textBoxExpenseDocName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxExpenseDocName.CustomReportFieldName = "";
			textBoxExpenseDocName.CustomReportKey = "";
			textBoxExpenseDocName.CustomReportValueType = 1;
			textBoxExpenseDocName.IsComboTextBox = false;
			textBoxExpenseDocName.IsModified = false;
			textBoxExpenseDocName.Location = new System.Drawing.Point(319, 197);
			textBoxExpenseDocName.MaxLength = 255;
			textBoxExpenseDocName.Name = "textBoxExpenseDocName";
			textBoxExpenseDocName.ReadOnly = true;
			textBoxExpenseDocName.Size = new System.Drawing.Size(220, 20);
			textBoxExpenseDocName.TabIndex = 14;
			textBoxExpenseDocName.TabStop = false;
			comboBoxExpenseDocID.AlwaysInEditMode = true;
			comboBoxExpenseDocID.Assigned = false;
			comboBoxExpenseDocID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxExpenseDocID.CustomReportFieldName = "";
			comboBoxExpenseDocID.CustomReportKey = "";
			comboBoxExpenseDocID.CustomReportValueType = 1;
			comboBoxExpenseDocID.DescriptionTextBox = textBoxExpenseDocName;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxExpenseDocID.DisplayLayout.Appearance = appearance59;
			comboBoxExpenseDocID.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxExpenseDocID.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseDocID.DisplayLayout.GroupByBox.Appearance = appearance60;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExpenseDocID.DisplayLayout.GroupByBox.BandLabelAppearance = appearance61;
			comboBoxExpenseDocID.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance62.BackColor2 = System.Drawing.SystemColors.Control;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxExpenseDocID.DisplayLayout.GroupByBox.PromptAppearance = appearance62;
			comboBoxExpenseDocID.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxExpenseDocID.DisplayLayout.MaxRowScrollRegions = 1;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxExpenseDocID.DisplayLayout.Override.ActiveCellAppearance = appearance63;
			appearance64.BackColor = System.Drawing.SystemColors.Highlight;
			appearance64.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxExpenseDocID.DisplayLayout.Override.ActiveRowAppearance = appearance64;
			comboBoxExpenseDocID.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxExpenseDocID.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseDocID.DisplayLayout.Override.CardAreaAppearance = appearance65;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			appearance66.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxExpenseDocID.DisplayLayout.Override.CellAppearance = appearance66;
			comboBoxExpenseDocID.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxExpenseDocID.DisplayLayout.Override.CellPadding = 0;
			appearance67.BackColor = System.Drawing.SystemColors.Control;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxExpenseDocID.DisplayLayout.Override.GroupByRowAppearance = appearance67;
			appearance68.TextHAlignAsString = "Left";
			comboBoxExpenseDocID.DisplayLayout.Override.HeaderAppearance = appearance68;
			comboBoxExpenseDocID.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxExpenseDocID.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.Color.Silver;
			comboBoxExpenseDocID.DisplayLayout.Override.RowAppearance = appearance69;
			comboBoxExpenseDocID.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxExpenseDocID.DisplayLayout.Override.TemplateAddRowAppearance = appearance70;
			comboBoxExpenseDocID.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxExpenseDocID.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxExpenseDocID.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxExpenseDocID.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxExpenseDocID.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxExpenseDocID.Editable = true;
			comboBoxExpenseDocID.ExcludeFromSecurity = false;
			comboBoxExpenseDocID.FilterString = "";
			comboBoxExpenseDocID.HasAllAccount = false;
			comboBoxExpenseDocID.HasCustom = false;
			comboBoxExpenseDocID.IsDataLoaded = false;
			comboBoxExpenseDocID.Location = new System.Drawing.Point(137, 197);
			comboBoxExpenseDocID.MaxDropDownItems = 12;
			comboBoxExpenseDocID.Name = "comboBoxExpenseDocID";
			comboBoxExpenseDocID.ShowAll = false;
			comboBoxExpenseDocID.ShowInactiveItems = false;
			comboBoxExpenseDocID.ShowQuickAdd = true;
			comboBoxExpenseDocID.Size = new System.Drawing.Size(179, 20);
			comboBoxExpenseDocID.TabIndex = 13;
			comboBoxExpenseDocID.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			xpButton2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton2.BackColor = System.Drawing.Color.DarkGray;
			xpButton2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton2.Enabled = false;
			xpButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			xpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton2.Location = new System.Drawing.Point(297, 264);
			xpButton2.Name = "xpButton2";
			xpButton2.Size = new System.Drawing.Size(130, 24);
			xpButton2.TabIndex = 17;
			xpButton2.Text = "Expense Accounts...";
			xpButton2.UseVisualStyleBackColor = false;
			xpButton2.Click += new System.EventHandler(xpButton2_Click);
			appearance71.FontData.BoldAsString = "False";
			appearance71.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance71;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(9, 176);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(102, 15);
			ultraFormattedLinkLabel6.TabIndex = 140;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Petty Cash Account:";
			appearance72.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance72;
			comboBoxPettyCashAccount.AlwaysInEditMode = true;
			comboBoxPettyCashAccount.Assigned = false;
			comboBoxPettyCashAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPettyCashAccount.CustomReportFieldName = "";
			comboBoxPettyCashAccount.CustomReportKey = "";
			comboBoxPettyCashAccount.CustomReportValueType = 1;
			comboBoxPettyCashAccount.DescriptionTextBox = textBoxPettyCashName;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPettyCashAccount.DisplayLayout.Appearance = appearance73;
			comboBoxPettyCashAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPettyCashAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPettyCashAccount.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPettyCashAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxPettyCashAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPettyCashAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxPettyCashAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPettyCashAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPettyCashAccount.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPettyCashAccount.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxPettyCashAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPettyCashAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPettyCashAccount.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPettyCashAccount.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxPettyCashAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPettyCashAccount.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPettyCashAccount.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxPettyCashAccount.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxPettyCashAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPettyCashAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxPettyCashAccount.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxPettyCashAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPettyCashAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxPettyCashAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPettyCashAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPettyCashAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPettyCashAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPettyCashAccount.Editable = true;
			comboBoxPettyCashAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxPettyCashAccount.FilterString = "";
			comboBoxPettyCashAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxPettyCashAccount.FilterSysDocID = "";
			comboBoxPettyCashAccount.HasAllAccount = false;
			comboBoxPettyCashAccount.HasCustom = false;
			comboBoxPettyCashAccount.IsDataLoaded = false;
			comboBoxPettyCashAccount.Location = new System.Drawing.Point(137, 174);
			comboBoxPettyCashAccount.MaxDropDownItems = 12;
			comboBoxPettyCashAccount.Name = "comboBoxPettyCashAccount";
			comboBoxPettyCashAccount.ShowInactiveItems = false;
			comboBoxPettyCashAccount.ShowQuickAdd = true;
			comboBoxPettyCashAccount.Size = new System.Drawing.Size(179, 20);
			comboBoxPettyCashAccount.TabIndex = 11;
			comboBoxPettyCashAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxPettyCashName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPettyCashName.CustomReportFieldName = "";
			textBoxPettyCashName.CustomReportKey = "";
			textBoxPettyCashName.CustomReportValueType = 1;
			textBoxPettyCashName.ForeColor = System.Drawing.Color.Black;
			textBoxPettyCashName.IsComboTextBox = false;
			textBoxPettyCashName.IsModified = false;
			textBoxPettyCashName.Location = new System.Drawing.Point(319, 174);
			textBoxPettyCashName.MaxLength = 255;
			textBoxPettyCashName.Name = "textBoxPettyCashName";
			textBoxPettyCashName.ReadOnly = true;
			textBoxPettyCashName.Size = new System.Drawing.Size(220, 20);
			textBoxPettyCashName.TabIndex = 12;
			textBoxPettyCashName.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(557, 367);
			base.Controls.Add(ultraFormattedLinkLabel6);
			base.Controls.Add(comboBoxPettyCashAccount);
			base.Controls.Add(textBoxPettyCashName);
			base.Controls.Add(xpButton2);
			base.Controls.Add(textBoxExpenseDocName);
			base.Controls.Add(comboBoxExpenseDocID);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(textBoxDocID);
			base.Controls.Add(ultraFormattedLinkLabel3);
			base.Controls.Add(comboBoxSysDocReceipt);
			base.Controls.Add(ultraFormattedLinkLabel5);
			base.Controls.Add(comboBoxDiscountGiven);
			base.Controls.Add(textBoxDiscountGivenAccountName);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(textBoxCustomerName);
			base.Controls.Add(comboBoxDefaultCustomer);
			base.Controls.Add(buttonPaymentMethod);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(textBoxStoreName);
			base.Controls.Add(locationComboBox1);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "POSCashRegisterDetailsForm";
			Text = "POS Cash Register";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)locationComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefaultCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountGiven).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDocReceipt).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxExpenseDocID).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPettyCashAccount).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
