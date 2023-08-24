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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class TaxEntryForm : Form, IForm
	{
		private TaxData currentData;

		private const string TABLENAME_CONST = "Tax";

		private const string IDFIELD_CONST = "TaxCode";

		private bool isNewRecord = true;

		private bool isDataLoading;

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

		private MMTextBox textBoxDescription;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMTextBox textBoxSalesTaxAccountName;

		private AllAccountsComboBox comboBoxSalesTaxAccount;

		private AmountTextBox textBoxRate;

		private MMLabel mmLabel5;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private MMLabel mmLabel2;

		private MMTextBox textBoxTaxID;

		private MMLabel mmLabel7;

		private ComboBox comboBoxTaxType;

		private MMLabel mmLabel3;

		private MMTextBox textBoxRemarks;

		private MMLabel mmLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private MMTextBox textBoxPurchaseTaxAccountName;

		private AllAccountsComboBox comboBoxPurchaseTaxAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private MMTextBox textBoxTaxReverseChargeAccount;

		private AllAccountsComboBox comboBoxTaxReverseChargeAccount;

		private TaxMethodComboBox comboBoxMethod;

		private MMLabel mmLabel8;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6011;

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

		public TaxEntryForm()
		{
			InitializeComponent();
			comboBoxMethod.LoadData();
			comboBoxMethod.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxMethod.SelectedMethod = TaxCalculationMethods.PercentageOfSale;
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += ExpenseCodeDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new TaxData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.TaxTable.Rows[0] : currentData.TaxTable.NewRow();
				dataRow.BeginEdit();
				dataRow["TaxCode"] = textBoxCode.Text.Trim();
				dataRow["TaxName"] = textBoxName.Text.Trim();
				dataRow["Description"] = textBoxDescription.Text;
				dataRow["Remarks"] = textBoxRemarks.Text;
				dataRow["TaxType"] = checked(comboBoxTaxType.SelectedIndex + 1);
				dataRow["CalculationMethod"] = comboBoxMethod.SelectedID;
				dataRow["TaxID"] = textBoxTaxID.Text.Trim();
				dataRow["TaxRate"] = textBoxRate.Text;
				dataRow["SalesTaxAccountID"] = comboBoxSalesTaxAccount.SelectedID;
				dataRow["PurchaseTaxAccountID"] = comboBoxPurchaseTaxAccount.SelectedID;
				dataRow["TaxReverseChargeAccountID"] = comboBoxTaxReverseChargeAccount.SelectedID;
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.TaxTable.Rows.Add(dataRow);
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
			if (SaveData())
			{
				FormActivator.ProductClassDetailsFormObj.LoadTaxData();
			}
			textBoxCode.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.TaxSystem.GetTaxByID(id.Trim());
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
			try
			{
				isDataLoading = true;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["TaxCode"].ToString();
					textBoxName.Text = dataRow["TaxName"].ToString();
					textBoxDescription.Text = dataRow["Description"].ToString();
					textBoxRemarks.Text = dataRow["Remarks"].ToString();
					comboBoxSalesTaxAccount.SelectedID = dataRow["SalesTaxAccountID"].ToString();
					comboBoxPurchaseTaxAccount.SelectedID = dataRow["PurchaseTaxAccountID"].ToString();
					comboBoxTaxReverseChargeAccount.SelectedID = dataRow["TaxReverseChargeAccountID"].ToString();
					textBoxTaxID.Text = dataRow["TaxID"].ToString();
					if (!string.IsNullOrEmpty(dataRow["TaxType"].ToString()))
					{
						comboBoxTaxType.SelectedIndex = checked(int.Parse(dataRow["TaxType"].ToString()) - 1);
					}
					else
					{
						comboBoxTaxType.SelectedIndex = -1;
					}
					textBoxRate.Text = dataRow["TaxRate"].ToString();
					checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
					if (!dataRow["CalculationMethod"].IsDBNullOrEmpty())
					{
						comboBoxMethod.SelectedID = int.Parse(dataRow["CalculationMethod"].ToString());
					}
					else
					{
						comboBoxMethod.SelectedID = 1;
					}
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.TaxSystem.CreateTax(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Tax, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.TaxSystem.UpdateTax(currentData);
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
			if (comboBoxTaxType.SelectedIndex == 0 && comboBoxSalesTaxAccount.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select Sales account.");
				return false;
			}
			if (comboBoxTaxType.SelectedIndex == 1 && comboBoxPurchaseTaxAccount.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select Purchase account.");
				return false;
			}
			if (comboBoxTaxType.SelectedIndex == 2 && (comboBoxPurchaseTaxAccount.SelectedID == "" || comboBoxSalesTaxAccount.SelectedID == ""))
			{
				ErrorHelper.InformationMessage("Please select account.");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Tax", "TaxCode", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Tax", "TaxCode");
			textBoxName.Clear();
			textBoxDescription.Clear();
			comboBoxSalesTaxAccount.Clear();
			textBoxSalesTaxAccountName.Clear();
			comboBoxPurchaseTaxAccount.Clear();
			textBoxPurchaseTaxAccountName.Clear();
			comboBoxTaxReverseChargeAccount.Clear();
			comboBoxMethod.SelectedMethod = TaxCalculationMethods.PercentageOfSale;
			textBoxRate.Clear();
			textBoxRemarks.Clear();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
			textBoxTaxID.Clear();
			comboBoxTaxType.SelectedIndex = 0;
		}

		private void ExpenseCodeGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ExpenseCodeGroupDetailsForm_Validated(object sender, EventArgs e)
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
				bool num = Factory.TaxSystem.DeleteTax(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Tax, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("Tax", "TaxCode", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Tax", "TaxCode", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Tax", "TaxCode"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Tax", "TaxCode"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Tax", "TaxCode", toolStripTextBoxFind.Text.Trim()))
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

		private void ExpenseCodeDetailsForm_Load(object sender, EventArgs e)
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
			new FormHelper().ShowList(DataComboType.Tax);
		}

		private void mmLabel1_Click(object sender, EventArgs e)
		{
		}

		private void comboBoxAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxSalesTaxAccountName.Text = comboBoxSalesTaxAccount.SelectedName;
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void mmLabel4_Click(object sender, EventArgs e)
		{
		}

		private void textBoxDescription_TextChanged(object sender, EventArgs e)
		{
		}

		private void mmTextBox1_TextChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxSalesTaxAccount.SelectedID);
		}

		private void radioButtonPercent_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonFixed_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxTaxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxTaxType.SelectedIndex == 0)
			{
				comboBoxPurchaseTaxAccount.Clear();
				comboBoxTaxReverseChargeAccount.Clear();
				comboBoxSalesTaxAccount.ReadOnly = false;
				comboBoxPurchaseTaxAccount.ReadOnly = true;
				comboBoxPurchaseTaxAccount.TabStop = false;
				comboBoxSalesTaxAccount.TabStop = true;
				comboBoxTaxReverseChargeAccount.ReadOnly = true;
				comboBoxTaxReverseChargeAccount.TabStop = false;
			}
			else if (comboBoxTaxType.SelectedIndex == 1)
			{
				comboBoxSalesTaxAccount.Clear();
				comboBoxPurchaseTaxAccount.ReadOnly = false;
				comboBoxSalesTaxAccount.ReadOnly = true;
				comboBoxPurchaseTaxAccount.TabStop = true;
				comboBoxSalesTaxAccount.TabStop = false;
				comboBoxTaxReverseChargeAccount.ReadOnly = false;
				comboBoxTaxReverseChargeAccount.TabStop = true;
			}
			else if (comboBoxTaxType.SelectedIndex == 2)
			{
				comboBoxSalesTaxAccount.ReadOnly = false;
				comboBoxPurchaseTaxAccount.ReadOnly = false;
				comboBoxPurchaseTaxAccount.TabStop = true;
				comboBoxSalesTaxAccount.TabStop = true;
				comboBoxTaxReverseChargeAccount.ReadOnly = false;
				comboBoxTaxReverseChargeAccount.TabStop = true;
				if (!isDataLoading)
				{
					comboBoxPurchaseTaxAccount.Clear();
					comboBoxSalesTaxAccount.Clear();
				}
			}
			else
			{
				comboBoxSalesTaxAccount.ReadOnly = true;
				comboBoxPurchaseTaxAccount.ReadOnly = true;
				comboBoxPurchaseTaxAccount.Clear();
				comboBoxSalesTaxAccount.Clear();
				comboBoxTaxReverseChargeAccount.Clear();
				comboBoxPurchaseTaxAccount.TabStop = false;
				comboBoxPurchaseTaxAccount.TabStop = false;
				comboBoxTaxReverseChargeAccount.ReadOnly = true;
				comboBoxTaxReverseChargeAccount.TabStop = false;
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxPurchaseTaxAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxTaxReverseChargeAccount.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.TaxEntryForm));
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
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxTaxType = new System.Windows.Forms.ComboBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxTaxReverseChargeAccount = new Micromind.UISupport.MMTextBox();
			comboBoxTaxReverseChargeAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxPurchaseTaxAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxPurchaseTaxAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			textBoxTaxID = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxRate = new Micromind.UISupport.AmountTextBox();
			textBoxSalesTaxAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxSalesTaxAccount = new Micromind.DataControls.AllAccountsComboBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxDescription = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxMethod = new Micromind.DataControls.TaxMethodComboBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxReverseChargeAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPurchaseTaxAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesTaxAccount).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
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
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(774, 31);
			toolStrip1.TabIndex = 19;
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
			panelButtons.Location = new System.Drawing.Point(0, 425);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(774, 40);
			panelButtons.TabIndex = 18;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(774, 1);
			linePanelDown.TabIndex = 0;
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
			xpButton1.Location = new System.Drawing.Point(664, 8);
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
			checkBoxInactive.Location = new System.Drawing.Point(345, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 2;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(8, 225);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(109, 15);
			ultraFormattedLinkLabel2.TabIndex = 21;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Sales Tax Account:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			comboBoxTaxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTaxType.FormattingEnabled = true;
			comboBoxTaxType.Items.AddRange(new object[3]
			{
				"Sales",
				"Purchase",
				"Sales & Purchase"
			});
			comboBoxTaxType.Location = new System.Drawing.Point(180, 182);
			comboBoxTaxType.Name = "comboBoxTaxType";
			comboBoxTaxType.Size = new System.Drawing.Size(191, 21);
			comboBoxTaxType.TabIndex = 8;
			comboBoxTaxType.SelectedIndexChanged += new System.EventHandler(comboBoxTaxType_SelectedIndexChanged);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance3;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(8, 247);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(130, 15);
			ultraFormattedLinkLabel1.TabIndex = 27;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Purchase Tax Account:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance5;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(8, 270);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(166, 15);
			ultraFormattedLinkLabel3.TabIndex = 30;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Tax Reverse Charge Account:";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance6;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			textBoxTaxReverseChargeAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTaxReverseChargeAccount.CustomReportFieldName = "";
			textBoxTaxReverseChargeAccount.CustomReportKey = "";
			textBoxTaxReverseChargeAccount.CustomReportValueType = 1;
			textBoxTaxReverseChargeAccount.IsComboTextBox = false;
			textBoxTaxReverseChargeAccount.IsModified = false;
			textBoxTaxReverseChargeAccount.Location = new System.Drawing.Point(338, 267);
			textBoxTaxReverseChargeAccount.MaxLength = 30;
			textBoxTaxReverseChargeAccount.Name = "textBoxTaxReverseChargeAccount";
			textBoxTaxReverseChargeAccount.ReadOnly = true;
			textBoxTaxReverseChargeAccount.Size = new System.Drawing.Size(212, 20);
			textBoxTaxReverseChargeAccount.TabIndex = 29;
			textBoxTaxReverseChargeAccount.TabStop = false;
			comboBoxTaxReverseChargeAccount.Assigned = false;
			comboBoxTaxReverseChargeAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxReverseChargeAccount.CustomReportFieldName = "";
			comboBoxTaxReverseChargeAccount.CustomReportKey = "";
			comboBoxTaxReverseChargeAccount.CustomReportValueType = 1;
			comboBoxTaxReverseChargeAccount.DescriptionTextBox = textBoxTaxReverseChargeAccount;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Appearance = appearance7;
			comboBoxTaxReverseChargeAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxReverseChargeAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance8.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance8.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance8.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxReverseChargeAccount.DisplayLayout.GroupByBox.Appearance = appearance8;
			appearance9.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxReverseChargeAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance9;
			comboBoxTaxReverseChargeAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance10.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance10.BackColor2 = System.Drawing.SystemColors.Control;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxReverseChargeAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance10;
			comboBoxTaxReverseChargeAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxReverseChargeAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.ActiveCellAppearance = appearance11;
			appearance12.BackColor = System.Drawing.SystemColors.Highlight;
			appearance12.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.ActiveRowAppearance = appearance12;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.CardAreaAppearance = appearance13;
			appearance14.BorderColor = System.Drawing.Color.Silver;
			appearance14.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.CellAppearance = appearance14;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.CellPadding = 0;
			appearance15.BackColor = System.Drawing.SystemColors.Control;
			appearance15.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance15.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance15.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.GroupByRowAppearance = appearance15;
			appearance16.TextHAlignAsString = "Left";
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.HeaderAppearance = appearance16;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.RowAppearance = appearance17;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxReverseChargeAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance18;
			comboBoxTaxReverseChargeAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaxReverseChargeAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaxReverseChargeAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaxReverseChargeAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaxReverseChargeAccount.Editable = true;
			comboBoxTaxReverseChargeAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxTaxReverseChargeAccount.FilterString = "";
			comboBoxTaxReverseChargeAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxTaxReverseChargeAccount.FilterSysDocID = "";
			comboBoxTaxReverseChargeAccount.HasAllAccount = false;
			comboBoxTaxReverseChargeAccount.HasCustom = false;
			comboBoxTaxReverseChargeAccount.IsDataLoaded = false;
			comboBoxTaxReverseChargeAccount.Location = new System.Drawing.Point(180, 267);
			comboBoxTaxReverseChargeAccount.MaxDropDownItems = 12;
			comboBoxTaxReverseChargeAccount.MaxLength = 64;
			comboBoxTaxReverseChargeAccount.Name = "comboBoxTaxReverseChargeAccount";
			comboBoxTaxReverseChargeAccount.ReadOnly = true;
			comboBoxTaxReverseChargeAccount.ShowInactiveItems = false;
			comboBoxTaxReverseChargeAccount.ShowQuickAdd = true;
			comboBoxTaxReverseChargeAccount.Size = new System.Drawing.Size(158, 20);
			comboBoxTaxReverseChargeAccount.TabIndex = 16;
			comboBoxTaxReverseChargeAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxPurchaseTaxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPurchaseTaxAccountName.CustomReportFieldName = "";
			textBoxPurchaseTaxAccountName.CustomReportKey = "";
			textBoxPurchaseTaxAccountName.CustomReportValueType = 1;
			textBoxPurchaseTaxAccountName.IsComboTextBox = false;
			textBoxPurchaseTaxAccountName.IsModified = false;
			textBoxPurchaseTaxAccountName.Location = new System.Drawing.Point(338, 244);
			textBoxPurchaseTaxAccountName.MaxLength = 30;
			textBoxPurchaseTaxAccountName.Name = "textBoxPurchaseTaxAccountName";
			textBoxPurchaseTaxAccountName.ReadOnly = true;
			textBoxPurchaseTaxAccountName.Size = new System.Drawing.Size(212, 20);
			textBoxPurchaseTaxAccountName.TabIndex = 15;
			textBoxPurchaseTaxAccountName.TabStop = false;
			comboBoxPurchaseTaxAccount.Assigned = false;
			comboBoxPurchaseTaxAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPurchaseTaxAccount.CustomReportFieldName = "";
			comboBoxPurchaseTaxAccount.CustomReportKey = "";
			comboBoxPurchaseTaxAccount.CustomReportValueType = 1;
			comboBoxPurchaseTaxAccount.DescriptionTextBox = textBoxPurchaseTaxAccountName;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPurchaseTaxAccount.DisplayLayout.Appearance = appearance19;
			comboBoxPurchaseTaxAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPurchaseTaxAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance20.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance20.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance20.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPurchaseTaxAccount.DisplayLayout.GroupByBox.Appearance = appearance20;
			appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPurchaseTaxAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance21;
			comboBoxPurchaseTaxAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance22.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance22.BackColor2 = System.Drawing.SystemColors.Control;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance22.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPurchaseTaxAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance22;
			comboBoxPurchaseTaxAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPurchaseTaxAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.ActiveCellAppearance = appearance23;
			appearance24.BackColor = System.Drawing.SystemColors.Highlight;
			appearance24.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.ActiveRowAppearance = appearance24;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.CardAreaAppearance = appearance25;
			appearance26.BorderColor = System.Drawing.Color.Silver;
			appearance26.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.CellAppearance = appearance26;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.CellPadding = 0;
			appearance27.BackColor = System.Drawing.SystemColors.Control;
			appearance27.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance27.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance27.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.GroupByRowAppearance = appearance27;
			appearance28.TextHAlignAsString = "Left";
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.HeaderAppearance = appearance28;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.Color.Silver;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.RowAppearance = appearance29;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPurchaseTaxAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance30;
			comboBoxPurchaseTaxAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPurchaseTaxAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPurchaseTaxAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPurchaseTaxAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPurchaseTaxAccount.Editable = true;
			comboBoxPurchaseTaxAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxPurchaseTaxAccount.FilterString = "";
			comboBoxPurchaseTaxAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxPurchaseTaxAccount.FilterSysDocID = "";
			comboBoxPurchaseTaxAccount.HasAllAccount = false;
			comboBoxPurchaseTaxAccount.HasCustom = false;
			comboBoxPurchaseTaxAccount.IsDataLoaded = false;
			comboBoxPurchaseTaxAccount.Location = new System.Drawing.Point(180, 244);
			comboBoxPurchaseTaxAccount.MaxDropDownItems = 12;
			comboBoxPurchaseTaxAccount.MaxLength = 64;
			comboBoxPurchaseTaxAccount.Name = "comboBoxPurchaseTaxAccount";
			comboBoxPurchaseTaxAccount.ReadOnly = true;
			comboBoxPurchaseTaxAccount.ShowInactiveItems = false;
			comboBoxPurchaseTaxAccount.ShowQuickAdd = true;
			comboBoxPurchaseTaxAccount.Size = new System.Drawing.Size(158, 20);
			comboBoxPurchaseTaxAccount.TabIndex = 14;
			comboBoxPurchaseTaxAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxRemarks.BackColor = System.Drawing.Color.White;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.IsModified = false;
			textBoxRemarks.Location = new System.Drawing.Point(180, 291);
			textBoxRemarks.MaxLength = 2000;
			textBoxRemarks.Multiline = true;
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.Size = new System.Drawing.Size(370, 75);
			textBoxRemarks.TabIndex = 17;
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(8, 291);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(52, 13);
			mmLabel6.TabIndex = 24;
			mmLabel6.Text = "Remarks:";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(8, 104);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(42, 13);
			mmLabel3.TabIndex = 23;
			mmLabel3.Text = "Tax ID:";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(8, 186);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(74, 13);
			mmLabel7.TabIndex = 21;
			mmLabel7.Text = "Applicable on:";
			textBoxTaxID.BackColor = System.Drawing.Color.White;
			textBoxTaxID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxTaxID.CustomReportFieldName = "";
			textBoxTaxID.CustomReportKey = "";
			textBoxTaxID.CustomReportValueType = 1;
			textBoxTaxID.IsComboTextBox = false;
			textBoxTaxID.IsModified = false;
			textBoxTaxID.Location = new System.Drawing.Point(180, 102);
			textBoxTaxID.MaxLength = 15;
			textBoxTaxID.Name = "textBoxTaxID";
			textBoxTaxID.Size = new System.Drawing.Size(191, 20);
			textBoxTaxID.TabIndex = 7;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(553, 131);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(15, 13);
			mmLabel2.TabIndex = 18;
			mmLabel2.Text = "%";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(412, 130);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(65, 13);
			mmLabel5.TabIndex = 17;
			mmLabel5.Text = "Percentage:";
			textBoxRate.AllowDecimal = true;
			textBoxRate.CustomReportFieldName = "";
			textBoxRate.CustomReportKey = "";
			textBoxRate.CustomReportValueType = 1;
			textBoxRate.IsComboTextBox = false;
			textBoxRate.IsModified = false;
			textBoxRate.Location = new System.Drawing.Point(483, 128);
			textBoxRate.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxRate.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxRate.Name = "textBoxRate";
			textBoxRate.NullText = "0";
			textBoxRate.Size = new System.Drawing.Size(67, 20);
			textBoxRate.TabIndex = 9;
			textBoxRate.Text = "0.00";
			textBoxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxRate.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxSalesTaxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSalesTaxAccountName.CustomReportFieldName = "";
			textBoxSalesTaxAccountName.CustomReportKey = "";
			textBoxSalesTaxAccountName.CustomReportValueType = 1;
			textBoxSalesTaxAccountName.IsComboTextBox = false;
			textBoxSalesTaxAccountName.IsModified = false;
			textBoxSalesTaxAccountName.Location = new System.Drawing.Point(338, 222);
			textBoxSalesTaxAccountName.MaxLength = 30;
			textBoxSalesTaxAccountName.Name = "textBoxSalesTaxAccountName";
			textBoxSalesTaxAccountName.ReadOnly = true;
			textBoxSalesTaxAccountName.Size = new System.Drawing.Size(212, 20);
			textBoxSalesTaxAccountName.TabIndex = 13;
			textBoxSalesTaxAccountName.TabStop = false;
			comboBoxSalesTaxAccount.Assigned = false;
			comboBoxSalesTaxAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesTaxAccount.CustomReportFieldName = "";
			comboBoxSalesTaxAccount.CustomReportKey = "";
			comboBoxSalesTaxAccount.CustomReportValueType = 1;
			comboBoxSalesTaxAccount.DescriptionTextBox = textBoxSalesTaxAccountName;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesTaxAccount.DisplayLayout.Appearance = appearance31;
			comboBoxSalesTaxAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesTaxAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTaxAccount.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesTaxAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			comboBoxSalesTaxAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesTaxAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			comboBoxSalesTaxAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesTaxAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesTaxAccount.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesTaxAccount.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			comboBoxSalesTaxAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesTaxAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTaxAccount.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesTaxAccount.DisplayLayout.Override.CellAppearance = appearance38;
			comboBoxSalesTaxAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesTaxAccount.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTaxAccount.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			comboBoxSalesTaxAccount.DisplayLayout.Override.HeaderAppearance = appearance40;
			comboBoxSalesTaxAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesTaxAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesTaxAccount.DisplayLayout.Override.RowAppearance = appearance41;
			comboBoxSalesTaxAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesTaxAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			comboBoxSalesTaxAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesTaxAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesTaxAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesTaxAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesTaxAccount.Editable = true;
			comboBoxSalesTaxAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxSalesTaxAccount.FilterString = "";
			comboBoxSalesTaxAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxSalesTaxAccount.FilterSysDocID = "";
			comboBoxSalesTaxAccount.HasAllAccount = false;
			comboBoxSalesTaxAccount.HasCustom = false;
			comboBoxSalesTaxAccount.IsDataLoaded = false;
			comboBoxSalesTaxAccount.Location = new System.Drawing.Point(180, 222);
			comboBoxSalesTaxAccount.MaxDropDownItems = 12;
			comboBoxSalesTaxAccount.MaxLength = 64;
			comboBoxSalesTaxAccount.Name = "comboBoxSalesTaxAccount";
			comboBoxSalesTaxAccount.ReadOnly = true;
			comboBoxSalesTaxAccount.ShowInactiveItems = false;
			comboBoxSalesTaxAccount.ShowQuickAdd = true;
			comboBoxSalesTaxAccount.Size = new System.Drawing.Size(158, 20);
			comboBoxSalesTaxAccount.TabIndex = 12;
			comboBoxSalesTaxAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSalesTaxAccount.SelectedIndexChanged += new System.EventHandler(comboBoxAccount_SelectedIndexChanged);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 20;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxDescription.BackColor = System.Drawing.Color.White;
			textBoxDescription.CustomReportFieldName = "";
			textBoxDescription.CustomReportKey = "";
			textBoxDescription.CustomReportValueType = 1;
			textBoxDescription.IsComboTextBox = false;
			textBoxDescription.IsModified = false;
			textBoxDescription.Location = new System.Drawing.Point(180, 80);
			textBoxDescription.MaxLength = 64;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.Size = new System.Drawing.Size(287, 20);
			textBoxDescription.TabIndex = 6;
			textBoxDescription.TextChanged += new System.EventHandler(textBoxDescription_TextChanged);
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(180, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(287, 20);
			textBoxName.TabIndex = 4;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(180, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(158, 20);
			textBoxCode.TabIndex = 1;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(8, 81);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(63, 13);
			mmLabel4.TabIndex = 5;
			mmLabel4.Text = "Description:";
			mmLabel4.Click += new System.EventHandler(mmLabel4_Click);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 58);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(68, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Tax Name:";
			mmLabel1.Click += new System.EventHandler(mmLabel1_Click);
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(65, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Tax Code:";
			comboBoxMethod.FormattingEnabled = true;
			comboBoxMethod.Location = new System.Drawing.Point(180, 127);
			comboBoxMethod.Name = "comboBoxMethod";
			comboBoxMethod.SelectedMethod = Micromind.Common.Data.TaxCalculationMethods.None;
			comboBoxMethod.Size = new System.Drawing.Size(191, 21);
			comboBoxMethod.TabIndex = 31;
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(9, 130);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(55, 13);
			mmLabel8.TabIndex = 32;
			mmLabel8.Text = "Based on:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(774, 465);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(comboBoxMethod);
			base.Controls.Add(ultraFormattedLinkLabel3);
			base.Controls.Add(textBoxTaxReverseChargeAccount);
			base.Controls.Add(comboBoxTaxReverseChargeAccount);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(textBoxPurchaseTaxAccountName);
			base.Controls.Add(comboBoxPurchaseTaxAccount);
			base.Controls.Add(textBoxRemarks);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(comboBoxTaxType);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(textBoxTaxID);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(textBoxRate);
			base.Controls.Add(textBoxSalesTaxAccountName);
			base.Controls.Add(comboBoxSalesTaxAccount);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxDescription);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "TaxEntryForm";
			Text = "Tax Item";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(ExpenseCodeDetailsForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxTaxReverseChargeAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPurchaseTaxAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesTaxAccount).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
