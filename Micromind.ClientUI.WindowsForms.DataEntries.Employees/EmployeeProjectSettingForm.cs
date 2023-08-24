using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class EmployeeProjectSettingForm : Form, IForm
	{
		private LocationData currentData;

		private const string TABLENAME_CONST = "Location";

		private const string IDFIELD_CONST = "LocationID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonSave;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private FormManager formManager;

		private AllAccountsComboBox comboBoxInventoryAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private AllAccountsComboBox comboBoxSalesAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private AllAccountsComboBox comboBoxCOGSAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private MMTextBox textBoxInventoryAccountName;

		private MMTextBox textBoxSalesAccountName;

		private MMTextBox textBoxGOGSAccountName;

		private MMTextBox textBoxSalesTaxAccountName;

		private AllAccountsComboBox comboBoxSalesTax;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private MMTextBox textBoxDiscountGivenAccountName;

		private AllAccountsComboBox comboBoxDiscountGiven;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private MMTextBox textBoxDiscountReceivedAccountName;

		private AllAccountsComboBox comboBoxDiscountReceived;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private AllAccountsComboBox comboBoxARAccount;

		private MMTextBox textBoxARName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private AllAccountsComboBox comboBoxAPAccount;

		private MMTextBox textBoxAP;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel9;

		private AllAccountsComboBox comboBoxGainLossAccount;

		private MMTextBox textBoxGainLoss;

		private UltraGroupBox ultraGroupBox1;

		private UltraGroupBox ultraGroupBox2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel16;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel17;

		private AllAccountsComboBox comboBoxProjectCostAccount;

		private AllAccountsComboBox comboBoxProjectIncomeAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel18;

		private AllAccountsComboBox comboBoxProjectWIPAccount;

		private MMTextBox textBoxProjectCostAccount;

		private MMTextBox textBoxProjectIncomeAccount;

		private MMTextBox textBoxProjectWIPAccount;

		private UltraGroupBox ultraGroupBox3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel12;

		private AllAccountsComboBox comboBoxManuWIPAccount;

		private MMTextBox textBoxManuWIPAccount;

		private Panel panel1;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6008;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public string ItemCode
		{
			get
			{
				return textBoxCode.Text;
			}
			set
			{
				textBoxCode.Text = value;
			}
		}

		public string ItemName
		{
			get
			{
				return textBoxName.Text;
			}
			set
			{
				textBoxName.Text = value;
			}
		}

		public bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public EmployeeProjectSettingForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EmployeeProjectSettingForm_Load;
			comboBoxGainLossAccount.SelectedIndexChanged += comboBoxGainLossAccount_SelectedIndexChanged;
		}

		private void comboBoxGainLossAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxGainLoss.Text = comboBoxGainLossAccount.SelectedName;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null)
				{
					currentData = new LocationData();
				}
				DataRow dataRow = currentData.LocationTable.Rows[0];
				dataRow.BeginEdit();
				if (comboBoxInventoryAccount.SelectedID != "")
				{
					dataRow["InventoryAccountID"] = comboBoxInventoryAccount.SelectedID;
				}
				else
				{
					dataRow["InventoryAccountID"] = DBNull.Value;
				}
				if (comboBoxSalesAccount.SelectedID != "")
				{
					dataRow["SalesAccountID"] = comboBoxSalesAccount.SelectedID;
				}
				else
				{
					dataRow["SalesAccountID"] = DBNull.Value;
				}
				if (comboBoxCOGSAccount.SelectedID != "")
				{
					dataRow["COGSAccountID"] = comboBoxCOGSAccount.SelectedID;
				}
				else
				{
					dataRow["COGSAccountID"] = DBNull.Value;
				}
				if (comboBoxDiscountGiven.SelectedID != "")
				{
					dataRow["DiscountGivenAccountID"] = comboBoxDiscountGiven.SelectedID;
				}
				else
				{
					dataRow["DiscountGivenAccountID"] = DBNull.Value;
				}
				if (comboBoxDiscountReceived.SelectedID != "")
				{
					dataRow["DiscountReceivedAccountID"] = comboBoxDiscountReceived.SelectedID;
				}
				else
				{
					dataRow["DiscountReceivedAccountID"] = DBNull.Value;
				}
				if (comboBoxSalesTax.SelectedID != "")
				{
					dataRow["SalesTaxAccountID"] = comboBoxSalesTax.SelectedID;
				}
				else
				{
					dataRow["SalesTaxAccountID"] = DBNull.Value;
				}
				if (comboBoxARAccount.SelectedID != "")
				{
					dataRow["ARAccountID"] = comboBoxARAccount.SelectedID;
				}
				else
				{
					dataRow["ARAccountID"] = DBNull.Value;
				}
				if (comboBoxAPAccount.SelectedID != "")
				{
					dataRow["APAccountID"] = comboBoxAPAccount.SelectedID;
				}
				else
				{
					dataRow["APAccountID"] = DBNull.Value;
				}
				if (comboBoxGainLossAccount.SelectedID != "")
				{
					dataRow["ExchangeGainLossAccountID"] = comboBoxGainLossAccount.SelectedID;
				}
				else
				{
					dataRow["ExchangeGainLossAccountID"] = DBNull.Value;
				}
				dataRow.EndEdit();
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

		public void LoadData(LocationData data)
		{
			try
			{
				currentData = data;
				if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
				{
					textBoxCode.Focus();
				}
				else
				{
					FillData();
					formManager.ResetDirty();
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
				comboBoxInventoryAccount.SelectedID = dataRow["InventoryAccountID"].ToString();
				comboBoxSalesAccount.SelectedID = dataRow["SalesAccountID"].ToString();
				comboBoxCOGSAccount.SelectedID = dataRow["COGSAccountID"].ToString();
				comboBoxDiscountGiven.SelectedID = dataRow["DiscountGivenAccountID"].ToString();
				comboBoxDiscountReceived.SelectedID = dataRow["DiscountReceivedAccountID"].ToString();
				comboBoxSalesTax.SelectedID = dataRow["SalesTaxAccountID"].ToString();
				comboBoxARAccount.SelectedID = dataRow["ARAccountID"].ToString();
				comboBoxAPAccount.SelectedID = dataRow["APAccountID"].ToString();
				comboBoxGainLossAccount.SelectedID = dataRow["ExchangeGainLossAccountID"].ToString();
			}
		}

		private bool SaveData()
		{
			if (!GetData())
			{
				return false;
			}
			try
			{
				Close();
				return true;
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
			textBoxCode.Clear();
			textBoxName.Clear();
			comboBoxSalesTax.Clear();
			comboBoxSalesAccount.Clear();
			comboBoxInventoryAccount.Clear();
			comboBoxCOGSAccount.Clear();
			comboBoxDiscountReceived.Clear();
			comboBoxDiscountGiven.Clear();
			comboBoxARAccount.Clear();
			comboBoxAPAccount.Clear();
			textBoxDiscountGivenAccountName.Clear();
			textBoxDiscountReceivedAccountName.Clear();
			textBoxGOGSAccountName.Clear();
			textBoxInventoryAccountName.Clear();
			textBoxSalesAccountName.Clear();
			textBoxSalesTaxAccountName.Clear();
			textBoxGainLoss.Clear();
			comboBoxGainLossAccount.Clear();
			textBoxARName.Clear();
			textBoxAP.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void LocationGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void LocationGroupDetailsForm_Validated(object sender, EventArgs e)
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
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DocumentNumberInUse) == DialogResult.No)
				{
					return false;
				}
				return Factory.LocationSystem.DeleteLocation(textBoxCode.Text);
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
			return true;
		}

		private void EmployeeProjectSettingForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				_ = base.IsDisposed;
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
			new FormHelper().ShowList(DataComboType.Location);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxInventoryAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxSalesAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxCOGSAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxSalesTax.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxDiscountGiven.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxDiscountReceived.SelectedID);
		}

		private void comboBoxInventoryAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxInventoryAccountName.Text = comboBoxInventoryAccount.SelectedName;
		}

		private void comboBoxSalesAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxSalesAccountName.Text = comboBoxSalesAccount.SelectedName;
		}

		private void comboBoxCOGSAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxGOGSAccountName.Text = comboBoxCOGSAccount.SelectedName;
		}

		private void comboBoxSalesTax_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxSalesTaxAccountName.Text = comboBoxSalesTax.SelectedName;
		}

		private void comboBoxDiscountGiven_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxDiscountGivenAccountName.Text = comboBoxDiscountGiven.SelectedName;
		}

		private void comboBoxDiscountReceived_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxDiscountReceivedAccountName.Text = comboBoxDiscountReceived.SelectedName;
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxARAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAPAccount.SelectedID);
		}

		private void comboBoxARAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxARName.Text = comboBoxARAccount.SelectedName;
		}

		private void comboBoxAPAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxAP.Text = comboBoxAPAccount.SelectedName;
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxGainLossAccount.SelectedID);
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
			Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance148 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance149 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance150 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance151 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance152 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance153 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance154 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance155 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance156 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance157 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance158 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance159 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance160 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance161 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance162 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance163 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance164 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance165 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance166 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance167 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance168 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance169 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance170 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance171 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance172 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance173 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance174 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance175 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance176 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance177 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance178 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance179 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance180 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance181 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance182 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeProjectSettingForm));
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel9 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			comboBoxGainLossAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxGainLoss = new Micromind.UISupport.MMTextBox();
			comboBoxAPAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxAP = new Micromind.UISupport.MMTextBox();
			comboBoxARAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxARName = new Micromind.UISupport.MMTextBox();
			comboBoxDiscountReceived = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxDiscountGiven = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxSalesTax = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxCOGSAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxSalesAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxInventoryAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxDiscountReceivedAccountName = new Micromind.UISupport.MMTextBox();
			textBoxDiscountGivenAccountName = new Micromind.UISupport.MMTextBox();
			textBoxSalesTaxAccountName = new Micromind.UISupport.MMTextBox();
			textBoxGOGSAccountName = new Micromind.UISupport.MMTextBox();
			textBoxSalesAccountName = new Micromind.UISupport.MMTextBox();
			textBoxInventoryAccountName = new Micromind.UISupport.MMTextBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel16 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel17 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxProjectCostAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxProjectCostAccount = new Micromind.UISupport.MMTextBox();
			comboBoxProjectIncomeAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxProjectIncomeAccount = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel18 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxProjectWIPAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxProjectWIPAccount = new Micromind.UISupport.MMTextBox();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel12 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxManuWIPAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxManuWIPAccount = new Micromind.UISupport.MMTextBox();
			panel1 = new System.Windows.Forms.Panel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGainLossAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAPAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountReceived).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountGiven).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesTax).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGSAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInventoryAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectCostAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectIncomeAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectWIPAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxManuWIPAccount).BeginInit();
			panel1.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 503);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(624, 40);
			panelButtons.TabIndex = 15;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(624, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(517, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&OK";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			appearance.FontData.BoldAsString = "False";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(9, 19);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(84, 15);
			ultraFormattedLinkLabel1.TabIndex = 128;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Inventory Asset:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance3.FontData.BoldAsString = "False";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance3;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(9, 41);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(33, 15);
			ultraFormattedLinkLabel2.TabIndex = 128;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Sales:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			appearance5.FontData.BoldAsString = "False";
			appearance5.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance5;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(9, 63);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(36, 15);
			ultraFormattedLinkLabel3.TabIndex = 128;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "COGS:";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance6;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			appearance7.FontData.BoldAsString = "False";
			appearance7.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance7;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(9, 85);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(54, 15);
			ultraFormattedLinkLabel4.TabIndex = 128;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Sales Tax:";
			appearance8.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance8;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance9.FontData.BoldAsString = "False";
			appearance9.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance9;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(9, 107);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(79, 15);
			ultraFormattedLinkLabel5.TabIndex = 128;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Discount Given:";
			appearance10.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance10;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			appearance11.FontData.BoldAsString = "False";
			appearance11.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance11;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(9, 129);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(96, 15);
			ultraFormattedLinkLabel6.TabIndex = 128;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Discount Received:";
			appearance12.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance12;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			appearance13.FontData.BoldAsString = "False";
			appearance13.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance13;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(9, 152);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(68, 15);
			ultraFormattedLinkLabel7.TabIndex = 131;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "A/R Account:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			appearance15.FontData.BoldAsString = "False";
			appearance15.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel8.Appearance = appearance15;
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(9, 175);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(63, 15);
			ultraFormattedLinkLabel8.TabIndex = 134;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "A/P Account";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance16;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			appearance17.FontData.BoldAsString = "False";
			appearance17.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel9.Appearance = appearance17;
			ultraFormattedLinkLabel9.AutoSize = true;
			ultraFormattedLinkLabel9.Location = new System.Drawing.Point(9, 199);
			ultraFormattedLinkLabel9.Name = "ultraFormattedLinkLabel9";
			ultraFormattedLinkLabel9.Size = new System.Drawing.Size(104, 15);
			ultraFormattedLinkLabel9.TabIndex = 137;
			ultraFormattedLinkLabel9.TabStop = true;
			ultraFormattedLinkLabel9.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel9.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel9.Value = "Exchange Gain/Loss:";
			appearance18.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel9.VisitedLinkAppearance = appearance18;
			ultraFormattedLinkLabel9.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel9_LinkClicked);
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel9);
			ultraGroupBox1.Controls.Add(comboBoxGainLossAccount);
			ultraGroupBox1.Controls.Add(textBoxGainLoss);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel8);
			ultraGroupBox1.Controls.Add(comboBoxAPAccount);
			ultraGroupBox1.Controls.Add(textBoxAP);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel7);
			ultraGroupBox1.Controls.Add(comboBoxARAccount);
			ultraGroupBox1.Controls.Add(textBoxARName);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel6);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel5);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel4);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel3);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel2);
			ultraGroupBox1.Controls.Add(comboBoxDiscountReceived);
			ultraGroupBox1.Controls.Add(comboBoxDiscountGiven);
			ultraGroupBox1.Controls.Add(comboBoxSalesTax);
			ultraGroupBox1.Controls.Add(comboBoxCOGSAccount);
			ultraGroupBox1.Controls.Add(comboBoxSalesAccount);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel1);
			ultraGroupBox1.Controls.Add(comboBoxInventoryAccount);
			ultraGroupBox1.Controls.Add(textBoxDiscountReceivedAccountName);
			ultraGroupBox1.Controls.Add(textBoxDiscountGivenAccountName);
			ultraGroupBox1.Controls.Add(textBoxSalesTaxAccountName);
			ultraGroupBox1.Controls.Add(textBoxGOGSAccountName);
			ultraGroupBox1.Controls.Add(textBoxSalesAccountName);
			ultraGroupBox1.Controls.Add(textBoxInventoryAccountName);
			ultraGroupBox1.Location = new System.Drawing.Point(5, 10);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(570, 230);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "General Accounts";
			comboBoxGainLossAccount.AlwaysInEditMode = true;
			comboBoxGainLossAccount.Assigned = false;
			comboBoxGainLossAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGainLossAccount.CustomReportFieldName = "";
			comboBoxGainLossAccount.CustomReportKey = "";
			comboBoxGainLossAccount.CustomReportValueType = 1;
			comboBoxGainLossAccount.DescriptionTextBox = null;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGainLossAccount.DisplayLayout.Appearance = appearance19;
			comboBoxGainLossAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGainLossAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance20.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance20.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance20.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGainLossAccount.DisplayLayout.GroupByBox.Appearance = appearance20;
			appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGainLossAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance21;
			comboBoxGainLossAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance22.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance22.BackColor2 = System.Drawing.SystemColors.Control;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance22.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGainLossAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance22;
			comboBoxGainLossAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGainLossAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGainLossAccount.DisplayLayout.Override.ActiveCellAppearance = appearance23;
			appearance24.BackColor = System.Drawing.SystemColors.Highlight;
			appearance24.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGainLossAccount.DisplayLayout.Override.ActiveRowAppearance = appearance24;
			comboBoxGainLossAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGainLossAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGainLossAccount.DisplayLayout.Override.CardAreaAppearance = appearance25;
			appearance26.BorderColor = System.Drawing.Color.Silver;
			appearance26.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGainLossAccount.DisplayLayout.Override.CellAppearance = appearance26;
			comboBoxGainLossAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGainLossAccount.DisplayLayout.Override.CellPadding = 0;
			appearance27.BackColor = System.Drawing.SystemColors.Control;
			appearance27.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance27.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance27.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGainLossAccount.DisplayLayout.Override.GroupByRowAppearance = appearance27;
			appearance28.TextHAlignAsString = "Left";
			comboBoxGainLossAccount.DisplayLayout.Override.HeaderAppearance = appearance28;
			comboBoxGainLossAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGainLossAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.Color.Silver;
			comboBoxGainLossAccount.DisplayLayout.Override.RowAppearance = appearance29;
			comboBoxGainLossAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGainLossAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance30;
			comboBoxGainLossAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGainLossAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGainLossAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGainLossAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGainLossAccount.Editable = true;
			comboBoxGainLossAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxGainLossAccount.FilterString = "";
			comboBoxGainLossAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxGainLossAccount.FilterSysDocID = "";
			comboBoxGainLossAccount.HasAllAccount = false;
			comboBoxGainLossAccount.HasCustom = false;
			comboBoxGainLossAccount.IsDataLoaded = false;
			comboBoxGainLossAccount.Location = new System.Drawing.Point(123, 197);
			comboBoxGainLossAccount.MaxDropDownItems = 12;
			comboBoxGainLossAccount.Name = "comboBoxGainLossAccount";
			comboBoxGainLossAccount.ShowInactiveItems = false;
			comboBoxGainLossAccount.ShowQuickAdd = true;
			comboBoxGainLossAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxGainLossAccount.TabIndex = 16;
			comboBoxGainLossAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxGainLoss.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGainLoss.CustomReportFieldName = "";
			textBoxGainLoss.CustomReportKey = "";
			textBoxGainLoss.CustomReportValueType = 1;
			textBoxGainLoss.ForeColor = System.Drawing.Color.Black;
			textBoxGainLoss.IsComboTextBox = false;
			textBoxGainLoss.Location = new System.Drawing.Point(242, 197);
			textBoxGainLoss.MaxLength = 255;
			textBoxGainLoss.Name = "textBoxGainLoss";
			textBoxGainLoss.ReadOnly = true;
			textBoxGainLoss.Size = new System.Drawing.Size(303, 21);
			textBoxGainLoss.TabIndex = 17;
			textBoxGainLoss.TabStop = false;
			comboBoxAPAccount.AlwaysInEditMode = true;
			comboBoxAPAccount.Assigned = false;
			comboBoxAPAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAPAccount.CustomReportFieldName = "";
			comboBoxAPAccount.CustomReportKey = "";
			comboBoxAPAccount.CustomReportValueType = 1;
			comboBoxAPAccount.DescriptionTextBox = null;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAPAccount.DisplayLayout.Appearance = appearance31;
			comboBoxAPAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAPAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAPAccount.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAPAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			comboBoxAPAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAPAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			comboBoxAPAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAPAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAPAccount.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAPAccount.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			comboBoxAPAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAPAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAPAccount.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAPAccount.DisplayLayout.Override.CellAppearance = appearance38;
			comboBoxAPAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAPAccount.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAPAccount.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			comboBoxAPAccount.DisplayLayout.Override.HeaderAppearance = appearance40;
			comboBoxAPAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAPAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			comboBoxAPAccount.DisplayLayout.Override.RowAppearance = appearance41;
			comboBoxAPAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAPAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			comboBoxAPAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAPAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAPAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAPAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAPAccount.Editable = true;
			comboBoxAPAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAPAccount.FilterString = "";
			comboBoxAPAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAPAccount.FilterSysDocID = "";
			comboBoxAPAccount.HasAllAccount = false;
			comboBoxAPAccount.HasCustom = false;
			comboBoxAPAccount.IsDataLoaded = false;
			comboBoxAPAccount.Location = new System.Drawing.Point(123, 173);
			comboBoxAPAccount.MaxDropDownItems = 12;
			comboBoxAPAccount.Name = "comboBoxAPAccount";
			comboBoxAPAccount.ShowInactiveItems = false;
			comboBoxAPAccount.ShowQuickAdd = true;
			comboBoxAPAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxAPAccount.TabIndex = 14;
			comboBoxAPAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAPAccount.SelectedIndexChanged += new System.EventHandler(comboBoxAPAccount_SelectedIndexChanged);
			textBoxAP.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAP.CustomReportFieldName = "";
			textBoxAP.CustomReportKey = "";
			textBoxAP.CustomReportValueType = 1;
			textBoxAP.ForeColor = System.Drawing.Color.Black;
			textBoxAP.IsComboTextBox = false;
			textBoxAP.Location = new System.Drawing.Point(242, 173);
			textBoxAP.MaxLength = 255;
			textBoxAP.Name = "textBoxAP";
			textBoxAP.ReadOnly = true;
			textBoxAP.Size = new System.Drawing.Size(303, 21);
			textBoxAP.TabIndex = 15;
			textBoxAP.TabStop = false;
			comboBoxARAccount.AlwaysInEditMode = true;
			comboBoxARAccount.Assigned = false;
			comboBoxARAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxARAccount.CustomReportFieldName = "";
			comboBoxARAccount.CustomReportKey = "";
			comboBoxARAccount.CustomReportValueType = 1;
			comboBoxARAccount.DescriptionTextBox = null;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxARAccount.DisplayLayout.Appearance = appearance43;
			comboBoxARAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxARAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance44.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance44.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.GroupByBox.Appearance = appearance44;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance45;
			comboBoxARAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance46.BackColor2 = System.Drawing.SystemColors.Control;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance46;
			comboBoxARAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxARAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxARAccount.DisplayLayout.Override.ActiveCellAppearance = appearance47;
			appearance48.BackColor = System.Drawing.SystemColors.Highlight;
			appearance48.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxARAccount.DisplayLayout.Override.ActiveRowAppearance = appearance48;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.CardAreaAppearance = appearance49;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxARAccount.DisplayLayout.Override.CellAppearance = appearance50;
			comboBoxARAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxARAccount.DisplayLayout.Override.CellPadding = 0;
			appearance51.BackColor = System.Drawing.SystemColors.Control;
			appearance51.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance51.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance51.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.GroupByRowAppearance = appearance51;
			appearance52.TextHAlignAsString = "Left";
			comboBoxARAccount.DisplayLayout.Override.HeaderAppearance = appearance52;
			comboBoxARAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxARAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			comboBoxARAccount.DisplayLayout.Override.RowAppearance = appearance53;
			comboBoxARAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxARAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance54;
			comboBoxARAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxARAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxARAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxARAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxARAccount.Editable = true;
			comboBoxARAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxARAccount.FilterString = "";
			comboBoxARAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxARAccount.FilterSysDocID = "";
			comboBoxARAccount.HasAllAccount = false;
			comboBoxARAccount.HasCustom = false;
			comboBoxARAccount.IsDataLoaded = false;
			comboBoxARAccount.Location = new System.Drawing.Point(123, 150);
			comboBoxARAccount.MaxDropDownItems = 12;
			comboBoxARAccount.Name = "comboBoxARAccount";
			comboBoxARAccount.ShowInactiveItems = false;
			comboBoxARAccount.ShowQuickAdd = true;
			comboBoxARAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxARAccount.TabIndex = 12;
			comboBoxARAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxARAccount.SelectedIndexChanged += new System.EventHandler(comboBoxARAccount_SelectedIndexChanged);
			textBoxARName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxARName.CustomReportFieldName = "";
			textBoxARName.CustomReportKey = "";
			textBoxARName.CustomReportValueType = 1;
			textBoxARName.ForeColor = System.Drawing.Color.Black;
			textBoxARName.IsComboTextBox = false;
			textBoxARName.Location = new System.Drawing.Point(242, 150);
			textBoxARName.MaxLength = 255;
			textBoxARName.Name = "textBoxARName";
			textBoxARName.ReadOnly = true;
			textBoxARName.Size = new System.Drawing.Size(303, 21);
			textBoxARName.TabIndex = 13;
			textBoxARName.TabStop = false;
			comboBoxDiscountReceived.AlwaysInEditMode = true;
			comboBoxDiscountReceived.Assigned = false;
			comboBoxDiscountReceived.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDiscountReceived.CustomReportFieldName = "";
			comboBoxDiscountReceived.CustomReportKey = "";
			comboBoxDiscountReceived.CustomReportValueType = 1;
			comboBoxDiscountReceived.DescriptionTextBox = null;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDiscountReceived.DisplayLayout.Appearance = appearance55;
			comboBoxDiscountReceived.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDiscountReceived.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance56.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountReceived.DisplayLayout.GroupByBox.Appearance = appearance56;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountReceived.DisplayLayout.GroupByBox.BandLabelAppearance = appearance57;
			comboBoxDiscountReceived.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance58.BackColor2 = System.Drawing.SystemColors.Control;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountReceived.DisplayLayout.GroupByBox.PromptAppearance = appearance58;
			comboBoxDiscountReceived.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDiscountReceived.DisplayLayout.MaxRowScrollRegions = 1;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDiscountReceived.DisplayLayout.Override.ActiveCellAppearance = appearance59;
			appearance60.BackColor = System.Drawing.SystemColors.Highlight;
			appearance60.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDiscountReceived.DisplayLayout.Override.ActiveRowAppearance = appearance60;
			comboBoxDiscountReceived.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDiscountReceived.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountReceived.DisplayLayout.Override.CardAreaAppearance = appearance61;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			appearance62.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDiscountReceived.DisplayLayout.Override.CellAppearance = appearance62;
			comboBoxDiscountReceived.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDiscountReceived.DisplayLayout.Override.CellPadding = 0;
			appearance63.BackColor = System.Drawing.SystemColors.Control;
			appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance63.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance63.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountReceived.DisplayLayout.Override.GroupByRowAppearance = appearance63;
			appearance64.TextHAlignAsString = "Left";
			comboBoxDiscountReceived.DisplayLayout.Override.HeaderAppearance = appearance64;
			comboBoxDiscountReceived.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDiscountReceived.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			comboBoxDiscountReceived.DisplayLayout.Override.RowAppearance = appearance65;
			comboBoxDiscountReceived.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDiscountReceived.DisplayLayout.Override.TemplateAddRowAppearance = appearance66;
			comboBoxDiscountReceived.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDiscountReceived.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDiscountReceived.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDiscountReceived.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDiscountReceived.Editable = true;
			comboBoxDiscountReceived.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxDiscountReceived.FilterString = "";
			comboBoxDiscountReceived.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxDiscountReceived.FilterSysDocID = "";
			comboBoxDiscountReceived.HasAllAccount = false;
			comboBoxDiscountReceived.HasCustom = false;
			comboBoxDiscountReceived.IsDataLoaded = false;
			comboBoxDiscountReceived.Location = new System.Drawing.Point(123, 127);
			comboBoxDiscountReceived.MaxDropDownItems = 12;
			comboBoxDiscountReceived.Name = "comboBoxDiscountReceived";
			comboBoxDiscountReceived.ShowInactiveItems = false;
			comboBoxDiscountReceived.ShowQuickAdd = true;
			comboBoxDiscountReceived.Size = new System.Drawing.Size(117, 21);
			comboBoxDiscountReceived.TabIndex = 10;
			comboBoxDiscountReceived.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDiscountReceived.SelectedIndexChanged += new System.EventHandler(comboBoxDiscountReceived_SelectedIndexChanged);
			comboBoxDiscountGiven.AlwaysInEditMode = true;
			comboBoxDiscountGiven.Assigned = false;
			comboBoxDiscountGiven.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDiscountGiven.CustomReportFieldName = "";
			comboBoxDiscountGiven.CustomReportKey = "";
			comboBoxDiscountGiven.CustomReportValueType = 1;
			comboBoxDiscountGiven.DescriptionTextBox = null;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDiscountGiven.DisplayLayout.Appearance = appearance67;
			comboBoxDiscountGiven.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDiscountGiven.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance68.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance68.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.Appearance = appearance68;
			appearance69.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.BandLabelAppearance = appearance69;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance70.BackColor2 = System.Drawing.SystemColors.Control;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance70.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.PromptAppearance = appearance70;
			comboBoxDiscountGiven.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDiscountGiven.DisplayLayout.MaxRowScrollRegions = 1;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDiscountGiven.DisplayLayout.Override.ActiveCellAppearance = appearance71;
			appearance72.BackColor = System.Drawing.SystemColors.Highlight;
			appearance72.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDiscountGiven.DisplayLayout.Override.ActiveRowAppearance = appearance72;
			comboBoxDiscountGiven.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDiscountGiven.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.Override.CardAreaAppearance = appearance73;
			appearance74.BorderColor = System.Drawing.Color.Silver;
			appearance74.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDiscountGiven.DisplayLayout.Override.CellAppearance = appearance74;
			comboBoxDiscountGiven.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDiscountGiven.DisplayLayout.Override.CellPadding = 0;
			appearance75.BackColor = System.Drawing.SystemColors.Control;
			appearance75.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance75.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance75.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.Override.GroupByRowAppearance = appearance75;
			appearance76.TextHAlignAsString = "Left";
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderAppearance = appearance76;
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.BorderColor = System.Drawing.Color.Silver;
			comboBoxDiscountGiven.DisplayLayout.Override.RowAppearance = appearance77;
			comboBoxDiscountGiven.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDiscountGiven.DisplayLayout.Override.TemplateAddRowAppearance = appearance78;
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
			comboBoxDiscountGiven.Location = new System.Drawing.Point(123, 105);
			comboBoxDiscountGiven.MaxDropDownItems = 12;
			comboBoxDiscountGiven.Name = "comboBoxDiscountGiven";
			comboBoxDiscountGiven.ShowInactiveItems = false;
			comboBoxDiscountGiven.ShowQuickAdd = true;
			comboBoxDiscountGiven.Size = new System.Drawing.Size(117, 21);
			comboBoxDiscountGiven.TabIndex = 8;
			comboBoxDiscountGiven.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDiscountGiven.SelectedIndexChanged += new System.EventHandler(comboBoxDiscountGiven_SelectedIndexChanged);
			comboBoxSalesTax.AlwaysInEditMode = true;
			comboBoxSalesTax.Assigned = false;
			comboBoxSalesTax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesTax.CustomReportFieldName = "";
			comboBoxSalesTax.CustomReportKey = "";
			comboBoxSalesTax.CustomReportValueType = 1;
			comboBoxSalesTax.DescriptionTextBox = null;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesTax.DisplayLayout.Appearance = appearance79;
			comboBoxSalesTax.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesTax.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance80.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance80.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance80.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.GroupByBox.Appearance = appearance80;
			appearance81.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesTax.DisplayLayout.GroupByBox.BandLabelAppearance = appearance81;
			comboBoxSalesTax.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance82.BackColor2 = System.Drawing.SystemColors.Control;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance82.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesTax.DisplayLayout.GroupByBox.PromptAppearance = appearance82;
			comboBoxSalesTax.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesTax.DisplayLayout.MaxRowScrollRegions = 1;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesTax.DisplayLayout.Override.ActiveCellAppearance = appearance83;
			appearance84.BackColor = System.Drawing.SystemColors.Highlight;
			appearance84.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesTax.DisplayLayout.Override.ActiveRowAppearance = appearance84;
			comboBoxSalesTax.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesTax.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.Override.CardAreaAppearance = appearance85;
			appearance86.BorderColor = System.Drawing.Color.Silver;
			appearance86.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesTax.DisplayLayout.Override.CellAppearance = appearance86;
			comboBoxSalesTax.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesTax.DisplayLayout.Override.CellPadding = 0;
			appearance87.BackColor = System.Drawing.SystemColors.Control;
			appearance87.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance87.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance87.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance87.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.Override.GroupByRowAppearance = appearance87;
			appearance88.TextHAlignAsString = "Left";
			comboBoxSalesTax.DisplayLayout.Override.HeaderAppearance = appearance88;
			comboBoxSalesTax.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesTax.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesTax.DisplayLayout.Override.RowAppearance = appearance89;
			comboBoxSalesTax.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesTax.DisplayLayout.Override.TemplateAddRowAppearance = appearance90;
			comboBoxSalesTax.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesTax.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesTax.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesTax.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesTax.Editable = true;
			comboBoxSalesTax.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxSalesTax.FilterString = "";
			comboBoxSalesTax.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxSalesTax.FilterSysDocID = "";
			comboBoxSalesTax.HasAllAccount = false;
			comboBoxSalesTax.HasCustom = false;
			comboBoxSalesTax.IsDataLoaded = false;
			comboBoxSalesTax.Location = new System.Drawing.Point(123, 83);
			comboBoxSalesTax.MaxDropDownItems = 12;
			comboBoxSalesTax.Name = "comboBoxSalesTax";
			comboBoxSalesTax.ShowInactiveItems = false;
			comboBoxSalesTax.ShowQuickAdd = true;
			comboBoxSalesTax.Size = new System.Drawing.Size(117, 21);
			comboBoxSalesTax.TabIndex = 6;
			comboBoxSalesTax.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSalesTax.SelectedIndexChanged += new System.EventHandler(comboBoxSalesTax_SelectedIndexChanged);
			comboBoxCOGSAccount.AlwaysInEditMode = true;
			comboBoxCOGSAccount.Assigned = false;
			comboBoxCOGSAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCOGSAccount.CustomReportFieldName = "";
			comboBoxCOGSAccount.CustomReportKey = "";
			comboBoxCOGSAccount.CustomReportValueType = 1;
			comboBoxCOGSAccount.DescriptionTextBox = null;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCOGSAccount.DisplayLayout.Appearance = appearance91;
			comboBoxCOGSAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCOGSAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance92.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance92.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.Appearance = appearance92;
			appearance93.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance93;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance94.BackColor2 = System.Drawing.SystemColors.Control;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance94.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance94;
			comboBoxCOGSAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCOGSAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCOGSAccount.DisplayLayout.Override.ActiveCellAppearance = appearance95;
			appearance96.BackColor = System.Drawing.SystemColors.Highlight;
			appearance96.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCOGSAccount.DisplayLayout.Override.ActiveRowAppearance = appearance96;
			comboBoxCOGSAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCOGSAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.Override.CardAreaAppearance = appearance97;
			appearance98.BorderColor = System.Drawing.Color.Silver;
			appearance98.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCOGSAccount.DisplayLayout.Override.CellAppearance = appearance98;
			comboBoxCOGSAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCOGSAccount.DisplayLayout.Override.CellPadding = 0;
			appearance99.BackColor = System.Drawing.SystemColors.Control;
			appearance99.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance99.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance99.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance99.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.Override.GroupByRowAppearance = appearance99;
			appearance100.TextHAlignAsString = "Left";
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderAppearance = appearance100;
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			comboBoxCOGSAccount.DisplayLayout.Override.RowAppearance = appearance101;
			comboBoxCOGSAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCOGSAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance102;
			comboBoxCOGSAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCOGSAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCOGSAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCOGSAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCOGSAccount.Editable = true;
			comboBoxCOGSAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxCOGSAccount.FilterString = "";
			comboBoxCOGSAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxCOGSAccount.FilterSysDocID = "";
			comboBoxCOGSAccount.HasAllAccount = false;
			comboBoxCOGSAccount.HasCustom = false;
			comboBoxCOGSAccount.IsDataLoaded = false;
			comboBoxCOGSAccount.Location = new System.Drawing.Point(123, 61);
			comboBoxCOGSAccount.MaxDropDownItems = 12;
			comboBoxCOGSAccount.Name = "comboBoxCOGSAccount";
			comboBoxCOGSAccount.ShowInactiveItems = false;
			comboBoxCOGSAccount.ShowQuickAdd = true;
			comboBoxCOGSAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxCOGSAccount.TabIndex = 4;
			comboBoxCOGSAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCOGSAccount.SelectedIndexChanged += new System.EventHandler(comboBoxCOGSAccount_SelectedIndexChanged);
			comboBoxSalesAccount.AlwaysInEditMode = true;
			comboBoxSalesAccount.Assigned = false;
			comboBoxSalesAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesAccount.CustomReportFieldName = "";
			comboBoxSalesAccount.CustomReportKey = "";
			comboBoxSalesAccount.CustomReportValueType = 1;
			comboBoxSalesAccount.DescriptionTextBox = null;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesAccount.DisplayLayout.Appearance = appearance103;
			comboBoxSalesAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance104.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance104.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.Appearance = appearance104;
			appearance105.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance105;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance106.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance106.BackColor2 = System.Drawing.SystemColors.Control;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance106.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance106;
			comboBoxSalesAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesAccount.DisplayLayout.Override.ActiveCellAppearance = appearance107;
			appearance108.BackColor = System.Drawing.SystemColors.Highlight;
			appearance108.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesAccount.DisplayLayout.Override.ActiveRowAppearance = appearance108;
			comboBoxSalesAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.Override.CardAreaAppearance = appearance109;
			appearance110.BorderColor = System.Drawing.Color.Silver;
			appearance110.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesAccount.DisplayLayout.Override.CellAppearance = appearance110;
			comboBoxSalesAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesAccount.DisplayLayout.Override.CellPadding = 0;
			appearance111.BackColor = System.Drawing.SystemColors.Control;
			appearance111.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance111.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance111.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance111.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.Override.GroupByRowAppearance = appearance111;
			appearance112.TextHAlignAsString = "Left";
			comboBoxSalesAccount.DisplayLayout.Override.HeaderAppearance = appearance112;
			comboBoxSalesAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesAccount.DisplayLayout.Override.RowAppearance = appearance113;
			comboBoxSalesAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance114.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance114;
			comboBoxSalesAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesAccount.Editable = true;
			comboBoxSalesAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxSalesAccount.FilterString = "";
			comboBoxSalesAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxSalesAccount.FilterSysDocID = "";
			comboBoxSalesAccount.HasAllAccount = false;
			comboBoxSalesAccount.HasCustom = false;
			comboBoxSalesAccount.IsDataLoaded = false;
			comboBoxSalesAccount.Location = new System.Drawing.Point(123, 39);
			comboBoxSalesAccount.MaxDropDownItems = 12;
			comboBoxSalesAccount.Name = "comboBoxSalesAccount";
			comboBoxSalesAccount.ShowInactiveItems = false;
			comboBoxSalesAccount.ShowQuickAdd = true;
			comboBoxSalesAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxSalesAccount.TabIndex = 2;
			comboBoxSalesAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSalesAccount.SelectedIndexChanged += new System.EventHandler(comboBoxSalesAccount_SelectedIndexChanged);
			comboBoxInventoryAccount.AlwaysInEditMode = true;
			comboBoxInventoryAccount.Assigned = false;
			comboBoxInventoryAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxInventoryAccount.CustomReportFieldName = "";
			comboBoxInventoryAccount.CustomReportKey = "";
			comboBoxInventoryAccount.CustomReportValueType = 1;
			comboBoxInventoryAccount.DescriptionTextBox = null;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxInventoryAccount.DisplayLayout.Appearance = appearance115;
			comboBoxInventoryAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxInventoryAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance116.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance116.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance116.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance116.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.Appearance = appearance116;
			appearance117.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance117;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance118.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance118.BackColor2 = System.Drawing.SystemColors.Control;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance118.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance118;
			comboBoxInventoryAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxInventoryAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxInventoryAccount.DisplayLayout.Override.ActiveCellAppearance = appearance119;
			appearance120.BackColor = System.Drawing.SystemColors.Highlight;
			appearance120.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxInventoryAccount.DisplayLayout.Override.ActiveRowAppearance = appearance120;
			comboBoxInventoryAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxInventoryAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryAccount.DisplayLayout.Override.CardAreaAppearance = appearance121;
			appearance122.BorderColor = System.Drawing.Color.Silver;
			appearance122.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxInventoryAccount.DisplayLayout.Override.CellAppearance = appearance122;
			comboBoxInventoryAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxInventoryAccount.DisplayLayout.Override.CellPadding = 0;
			appearance123.BackColor = System.Drawing.SystemColors.Control;
			appearance123.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance123.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance123.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance123.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryAccount.DisplayLayout.Override.GroupByRowAppearance = appearance123;
			appearance124.TextHAlignAsString = "Left";
			comboBoxInventoryAccount.DisplayLayout.Override.HeaderAppearance = appearance124;
			comboBoxInventoryAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxInventoryAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.BorderColor = System.Drawing.Color.Silver;
			comboBoxInventoryAccount.DisplayLayout.Override.RowAppearance = appearance125;
			comboBoxInventoryAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance126.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxInventoryAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance126;
			comboBoxInventoryAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxInventoryAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxInventoryAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxInventoryAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxInventoryAccount.Editable = true;
			comboBoxInventoryAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxInventoryAccount.FilterString = "";
			comboBoxInventoryAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxInventoryAccount.FilterSysDocID = "";
			comboBoxInventoryAccount.HasAllAccount = false;
			comboBoxInventoryAccount.HasCustom = false;
			comboBoxInventoryAccount.IsDataLoaded = false;
			comboBoxInventoryAccount.Location = new System.Drawing.Point(123, 17);
			comboBoxInventoryAccount.MaxDropDownItems = 12;
			comboBoxInventoryAccount.Name = "comboBoxInventoryAccount";
			comboBoxInventoryAccount.ShowInactiveItems = false;
			comboBoxInventoryAccount.ShowQuickAdd = true;
			comboBoxInventoryAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxInventoryAccount.TabIndex = 0;
			comboBoxInventoryAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxInventoryAccount.SelectedIndexChanged += new System.EventHandler(comboBoxInventoryAccount_SelectedIndexChanged);
			textBoxDiscountReceivedAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDiscountReceivedAccountName.CustomReportFieldName = "";
			textBoxDiscountReceivedAccountName.CustomReportKey = "";
			textBoxDiscountReceivedAccountName.CustomReportValueType = 1;
			textBoxDiscountReceivedAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxDiscountReceivedAccountName.IsComboTextBox = false;
			textBoxDiscountReceivedAccountName.Location = new System.Drawing.Point(242, 127);
			textBoxDiscountReceivedAccountName.MaxLength = 255;
			textBoxDiscountReceivedAccountName.Name = "textBoxDiscountReceivedAccountName";
			textBoxDiscountReceivedAccountName.ReadOnly = true;
			textBoxDiscountReceivedAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxDiscountReceivedAccountName.TabIndex = 11;
			textBoxDiscountReceivedAccountName.TabStop = false;
			textBoxDiscountGivenAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDiscountGivenAccountName.CustomReportFieldName = "";
			textBoxDiscountGivenAccountName.CustomReportKey = "";
			textBoxDiscountGivenAccountName.CustomReportValueType = 1;
			textBoxDiscountGivenAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxDiscountGivenAccountName.IsComboTextBox = false;
			textBoxDiscountGivenAccountName.Location = new System.Drawing.Point(242, 105);
			textBoxDiscountGivenAccountName.MaxLength = 255;
			textBoxDiscountGivenAccountName.Name = "textBoxDiscountGivenAccountName";
			textBoxDiscountGivenAccountName.ReadOnly = true;
			textBoxDiscountGivenAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxDiscountGivenAccountName.TabIndex = 9;
			textBoxDiscountGivenAccountName.TabStop = false;
			textBoxSalesTaxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSalesTaxAccountName.CustomReportFieldName = "";
			textBoxSalesTaxAccountName.CustomReportKey = "";
			textBoxSalesTaxAccountName.CustomReportValueType = 1;
			textBoxSalesTaxAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxSalesTaxAccountName.IsComboTextBox = false;
			textBoxSalesTaxAccountName.Location = new System.Drawing.Point(242, 83);
			textBoxSalesTaxAccountName.MaxLength = 255;
			textBoxSalesTaxAccountName.Name = "textBoxSalesTaxAccountName";
			textBoxSalesTaxAccountName.ReadOnly = true;
			textBoxSalesTaxAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxSalesTaxAccountName.TabIndex = 7;
			textBoxSalesTaxAccountName.TabStop = false;
			textBoxGOGSAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGOGSAccountName.CustomReportFieldName = "";
			textBoxGOGSAccountName.CustomReportKey = "";
			textBoxGOGSAccountName.CustomReportValueType = 1;
			textBoxGOGSAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxGOGSAccountName.IsComboTextBox = false;
			textBoxGOGSAccountName.Location = new System.Drawing.Point(242, 61);
			textBoxGOGSAccountName.MaxLength = 255;
			textBoxGOGSAccountName.Name = "textBoxGOGSAccountName";
			textBoxGOGSAccountName.ReadOnly = true;
			textBoxGOGSAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxGOGSAccountName.TabIndex = 5;
			textBoxGOGSAccountName.TabStop = false;
			textBoxSalesAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSalesAccountName.CustomReportFieldName = "";
			textBoxSalesAccountName.CustomReportKey = "";
			textBoxSalesAccountName.CustomReportValueType = 1;
			textBoxSalesAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxSalesAccountName.IsComboTextBox = false;
			textBoxSalesAccountName.Location = new System.Drawing.Point(242, 39);
			textBoxSalesAccountName.MaxLength = 255;
			textBoxSalesAccountName.Name = "textBoxSalesAccountName";
			textBoxSalesAccountName.ReadOnly = true;
			textBoxSalesAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxSalesAccountName.TabIndex = 3;
			textBoxSalesAccountName.TabStop = false;
			textBoxInventoryAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxInventoryAccountName.CustomReportFieldName = "";
			textBoxInventoryAccountName.CustomReportKey = "";
			textBoxInventoryAccountName.CustomReportValueType = 1;
			textBoxInventoryAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxInventoryAccountName.IsComboTextBox = false;
			textBoxInventoryAccountName.Location = new System.Drawing.Point(242, 17);
			textBoxInventoryAccountName.MaxLength = 255;
			textBoxInventoryAccountName.Name = "textBoxInventoryAccountName";
			textBoxInventoryAccountName.ReadOnly = true;
			textBoxInventoryAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxInventoryAccountName.TabIndex = 1;
			textBoxInventoryAccountName.TabStop = false;
			ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel16);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel17);
			ultraGroupBox2.Controls.Add(comboBoxProjectCostAccount);
			ultraGroupBox2.Controls.Add(comboBoxProjectIncomeAccount);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel18);
			ultraGroupBox2.Controls.Add(comboBoxProjectWIPAccount);
			ultraGroupBox2.Controls.Add(textBoxProjectCostAccount);
			ultraGroupBox2.Controls.Add(textBoxProjectIncomeAccount);
			ultraGroupBox2.Controls.Add(textBoxProjectWIPAccount);
			ultraGroupBox2.Location = new System.Drawing.Point(5, 237);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(570, 92);
			ultraGroupBox2.TabIndex = 1;
			ultraGroupBox2.Text = "Projects Accounts";
			appearance127.FontData.BoldAsString = "False";
			appearance127.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel16.Appearance = appearance127;
			ultraFormattedLinkLabel16.AutoSize = true;
			ultraFormattedLinkLabel16.Location = new System.Drawing.Point(9, 68);
			ultraFormattedLinkLabel16.Name = "ultraFormattedLinkLabel16";
			ultraFormattedLinkLabel16.Size = new System.Drawing.Size(80, 15);
			ultraFormattedLinkLabel16.TabIndex = 128;
			ultraFormattedLinkLabel16.TabStop = true;
			ultraFormattedLinkLabel16.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel16.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel16.Value = "Cost of Project:";
			appearance128.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel16.VisitedLinkAppearance = appearance128;
			appearance129.FontData.BoldAsString = "False";
			appearance129.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel17.Appearance = appearance129;
			ultraFormattedLinkLabel17.AutoSize = true;
			ultraFormattedLinkLabel17.Location = new System.Drawing.Point(9, 46);
			ultraFormattedLinkLabel17.Name = "ultraFormattedLinkLabel17";
			ultraFormattedLinkLabel17.Size = new System.Drawing.Size(80, 15);
			ultraFormattedLinkLabel17.TabIndex = 128;
			ultraFormattedLinkLabel17.TabStop = true;
			ultraFormattedLinkLabel17.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel17.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel17.Value = "Project Income:";
			appearance130.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel17.VisitedLinkAppearance = appearance130;
			comboBoxProjectCostAccount.AlwaysInEditMode = true;
			comboBoxProjectCostAccount.Assigned = false;
			comboBoxProjectCostAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProjectCostAccount.CustomReportFieldName = "";
			comboBoxProjectCostAccount.CustomReportKey = "";
			comboBoxProjectCostAccount.CustomReportValueType = 1;
			comboBoxProjectCostAccount.DescriptionTextBox = textBoxProjectCostAccount;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProjectCostAccount.DisplayLayout.Appearance = appearance131;
			comboBoxProjectCostAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProjectCostAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance132.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance132.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance132.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.Appearance = appearance132;
			appearance133.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance133;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance134.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance134.BackColor2 = System.Drawing.SystemColors.Control;
			appearance134.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance134.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance134;
			comboBoxProjectCostAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProjectCostAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			appearance135.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProjectCostAccount.DisplayLayout.Override.ActiveCellAppearance = appearance135;
			appearance136.BackColor = System.Drawing.SystemColors.Highlight;
			appearance136.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProjectCostAccount.DisplayLayout.Override.ActiveRowAppearance = appearance136;
			comboBoxProjectCostAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProjectCostAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance137.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProjectCostAccount.DisplayLayout.Override.CardAreaAppearance = appearance137;
			appearance138.BorderColor = System.Drawing.Color.Silver;
			appearance138.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProjectCostAccount.DisplayLayout.Override.CellAppearance = appearance138;
			comboBoxProjectCostAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProjectCostAccount.DisplayLayout.Override.CellPadding = 0;
			appearance139.BackColor = System.Drawing.SystemColors.Control;
			appearance139.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance139.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance139.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance139.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectCostAccount.DisplayLayout.Override.GroupByRowAppearance = appearance139;
			appearance140.TextHAlignAsString = "Left";
			comboBoxProjectCostAccount.DisplayLayout.Override.HeaderAppearance = appearance140;
			comboBoxProjectCostAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProjectCostAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance141.BackColor = System.Drawing.SystemColors.Window;
			appearance141.BorderColor = System.Drawing.Color.Silver;
			comboBoxProjectCostAccount.DisplayLayout.Override.RowAppearance = appearance141;
			comboBoxProjectCostAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance142.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProjectCostAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance142;
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
			comboBoxProjectCostAccount.Location = new System.Drawing.Point(123, 66);
			comboBoxProjectCostAccount.MaxDropDownItems = 12;
			comboBoxProjectCostAccount.Name = "comboBoxProjectCostAccount";
			comboBoxProjectCostAccount.ShowInactiveItems = false;
			comboBoxProjectCostAccount.ShowQuickAdd = true;
			comboBoxProjectCostAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxProjectCostAccount.TabIndex = 4;
			comboBoxProjectCostAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxProjectCostAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectCostAccount.CustomReportFieldName = "";
			textBoxProjectCostAccount.CustomReportKey = "";
			textBoxProjectCostAccount.CustomReportValueType = 1;
			textBoxProjectCostAccount.ForeColor = System.Drawing.Color.Black;
			textBoxProjectCostAccount.IsComboTextBox = false;
			textBoxProjectCostAccount.Location = new System.Drawing.Point(242, 66);
			textBoxProjectCostAccount.MaxLength = 255;
			textBoxProjectCostAccount.Name = "textBoxProjectCostAccount";
			textBoxProjectCostAccount.ReadOnly = true;
			textBoxProjectCostAccount.Size = new System.Drawing.Size(303, 21);
			textBoxProjectCostAccount.TabIndex = 5;
			textBoxProjectCostAccount.TabStop = false;
			comboBoxProjectIncomeAccount.AlwaysInEditMode = true;
			comboBoxProjectIncomeAccount.Assigned = false;
			comboBoxProjectIncomeAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProjectIncomeAccount.CustomReportFieldName = "";
			comboBoxProjectIncomeAccount.CustomReportKey = "";
			comboBoxProjectIncomeAccount.CustomReportValueType = 1;
			comboBoxProjectIncomeAccount.DescriptionTextBox = textBoxProjectIncomeAccount;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProjectIncomeAccount.DisplayLayout.Appearance = appearance143;
			comboBoxProjectIncomeAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProjectIncomeAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance144.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance144.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance144.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectIncomeAccount.DisplayLayout.GroupByBox.Appearance = appearance144;
			appearance145.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectIncomeAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance145;
			comboBoxProjectIncomeAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance146.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance146.BackColor2 = System.Drawing.SystemColors.Control;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance146.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectIncomeAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance146;
			comboBoxProjectIncomeAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProjectIncomeAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance147.BackColor = System.Drawing.SystemColors.Window;
			appearance147.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.ActiveCellAppearance = appearance147;
			appearance148.BackColor = System.Drawing.SystemColors.Highlight;
			appearance148.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.ActiveRowAppearance = appearance148;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.CardAreaAppearance = appearance149;
			appearance150.BorderColor = System.Drawing.Color.Silver;
			appearance150.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.CellAppearance = appearance150;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.CellPadding = 0;
			appearance151.BackColor = System.Drawing.SystemColors.Control;
			appearance151.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance151.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance151.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance151.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.GroupByRowAppearance = appearance151;
			appearance152.TextHAlignAsString = "Left";
			comboBoxProjectIncomeAccount.DisplayLayout.Override.HeaderAppearance = appearance152;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance153.BackColor = System.Drawing.SystemColors.Window;
			appearance153.BorderColor = System.Drawing.Color.Silver;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.RowAppearance = appearance153;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance154.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance154;
			comboBoxProjectIncomeAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProjectIncomeAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProjectIncomeAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProjectIncomeAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProjectIncomeAccount.Editable = true;
			comboBoxProjectIncomeAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxProjectIncomeAccount.FilterString = "";
			comboBoxProjectIncomeAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxProjectIncomeAccount.FilterSysDocID = "";
			comboBoxProjectIncomeAccount.HasAllAccount = false;
			comboBoxProjectIncomeAccount.HasCustom = false;
			comboBoxProjectIncomeAccount.IsDataLoaded = false;
			comboBoxProjectIncomeAccount.Location = new System.Drawing.Point(123, 44);
			comboBoxProjectIncomeAccount.MaxDropDownItems = 12;
			comboBoxProjectIncomeAccount.Name = "comboBoxProjectIncomeAccount";
			comboBoxProjectIncomeAccount.ShowInactiveItems = false;
			comboBoxProjectIncomeAccount.ShowQuickAdd = true;
			comboBoxProjectIncomeAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxProjectIncomeAccount.TabIndex = 2;
			comboBoxProjectIncomeAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxProjectIncomeAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectIncomeAccount.CustomReportFieldName = "";
			textBoxProjectIncomeAccount.CustomReportKey = "";
			textBoxProjectIncomeAccount.CustomReportValueType = 1;
			textBoxProjectIncomeAccount.ForeColor = System.Drawing.Color.Black;
			textBoxProjectIncomeAccount.IsComboTextBox = false;
			textBoxProjectIncomeAccount.Location = new System.Drawing.Point(242, 44);
			textBoxProjectIncomeAccount.MaxLength = 255;
			textBoxProjectIncomeAccount.Name = "textBoxProjectIncomeAccount";
			textBoxProjectIncomeAccount.ReadOnly = true;
			textBoxProjectIncomeAccount.Size = new System.Drawing.Size(303, 21);
			textBoxProjectIncomeAccount.TabIndex = 3;
			textBoxProjectIncomeAccount.TabStop = false;
			appearance155.FontData.BoldAsString = "False";
			appearance155.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel18.Appearance = appearance155;
			ultraFormattedLinkLabel18.AutoSize = true;
			ultraFormattedLinkLabel18.Location = new System.Drawing.Point(9, 24);
			ultraFormattedLinkLabel18.Name = "ultraFormattedLinkLabel18";
			ultraFormattedLinkLabel18.Size = new System.Drawing.Size(91, 15);
			ultraFormattedLinkLabel18.TabIndex = 128;
			ultraFormattedLinkLabel18.TabStop = true;
			ultraFormattedLinkLabel18.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel18.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel18.Value = "Work In Progress:";
			appearance156.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel18.VisitedLinkAppearance = appearance156;
			comboBoxProjectWIPAccount.AlwaysInEditMode = true;
			comboBoxProjectWIPAccount.Assigned = false;
			comboBoxProjectWIPAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProjectWIPAccount.CustomReportFieldName = "";
			comboBoxProjectWIPAccount.CustomReportKey = "";
			comboBoxProjectWIPAccount.CustomReportValueType = 1;
			comboBoxProjectWIPAccount.DescriptionTextBox = textBoxProjectWIPAccount;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProjectWIPAccount.DisplayLayout.Appearance = appearance157;
			comboBoxProjectWIPAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProjectWIPAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance158.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance158.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance158.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance158.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectWIPAccount.DisplayLayout.GroupByBox.Appearance = appearance158;
			appearance159.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectWIPAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance159;
			comboBoxProjectWIPAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance160.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance160.BackColor2 = System.Drawing.SystemColors.Control;
			appearance160.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance160.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectWIPAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance160;
			comboBoxProjectWIPAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProjectWIPAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			appearance161.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProjectWIPAccount.DisplayLayout.Override.ActiveCellAppearance = appearance161;
			appearance162.BackColor = System.Drawing.SystemColors.Highlight;
			appearance162.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProjectWIPAccount.DisplayLayout.Override.ActiveRowAppearance = appearance162;
			comboBoxProjectWIPAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProjectWIPAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProjectWIPAccount.DisplayLayout.Override.CardAreaAppearance = appearance163;
			appearance164.BorderColor = System.Drawing.Color.Silver;
			appearance164.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProjectWIPAccount.DisplayLayout.Override.CellAppearance = appearance164;
			comboBoxProjectWIPAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProjectWIPAccount.DisplayLayout.Override.CellPadding = 0;
			appearance165.BackColor = System.Drawing.SystemColors.Control;
			appearance165.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance165.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance165.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance165.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectWIPAccount.DisplayLayout.Override.GroupByRowAppearance = appearance165;
			appearance166.TextHAlignAsString = "Left";
			comboBoxProjectWIPAccount.DisplayLayout.Override.HeaderAppearance = appearance166;
			comboBoxProjectWIPAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProjectWIPAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			appearance167.BorderColor = System.Drawing.Color.Silver;
			comboBoxProjectWIPAccount.DisplayLayout.Override.RowAppearance = appearance167;
			comboBoxProjectWIPAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance168.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProjectWIPAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance168;
			comboBoxProjectWIPAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProjectWIPAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProjectWIPAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProjectWIPAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProjectWIPAccount.Editable = true;
			comboBoxProjectWIPAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxProjectWIPAccount.FilterString = "";
			comboBoxProjectWIPAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxProjectWIPAccount.FilterSysDocID = "";
			comboBoxProjectWIPAccount.HasAllAccount = false;
			comboBoxProjectWIPAccount.HasCustom = false;
			comboBoxProjectWIPAccount.IsDataLoaded = false;
			comboBoxProjectWIPAccount.Location = new System.Drawing.Point(123, 22);
			comboBoxProjectWIPAccount.MaxDropDownItems = 12;
			comboBoxProjectWIPAccount.Name = "comboBoxProjectWIPAccount";
			comboBoxProjectWIPAccount.ShowInactiveItems = false;
			comboBoxProjectWIPAccount.ShowQuickAdd = true;
			comboBoxProjectWIPAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxProjectWIPAccount.TabIndex = 0;
			comboBoxProjectWIPAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxProjectWIPAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectWIPAccount.CustomReportFieldName = "";
			textBoxProjectWIPAccount.CustomReportKey = "";
			textBoxProjectWIPAccount.CustomReportValueType = 1;
			textBoxProjectWIPAccount.ForeColor = System.Drawing.Color.Black;
			textBoxProjectWIPAccount.IsComboTextBox = false;
			textBoxProjectWIPAccount.Location = new System.Drawing.Point(242, 22);
			textBoxProjectWIPAccount.MaxLength = 255;
			textBoxProjectWIPAccount.Name = "textBoxProjectWIPAccount";
			textBoxProjectWIPAccount.ReadOnly = true;
			textBoxProjectWIPAccount.Size = new System.Drawing.Size(303, 21);
			textBoxProjectWIPAccount.TabIndex = 1;
			textBoxProjectWIPAccount.TabStop = false;
			ultraGroupBox3.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox3.Controls.Add(ultraFormattedLinkLabel12);
			ultraGroupBox3.Controls.Add(comboBoxManuWIPAccount);
			ultraGroupBox3.Controls.Add(textBoxManuWIPAccount);
			ultraGroupBox3.Location = new System.Drawing.Point(5, 335);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(570, 92);
			ultraGroupBox3.TabIndex = 2;
			ultraGroupBox3.Text = "Manufacturing Accounts";
			appearance169.FontData.BoldAsString = "False";
			appearance169.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel12.Appearance = appearance169;
			ultraFormattedLinkLabel12.AutoSize = true;
			ultraFormattedLinkLabel12.Location = new System.Drawing.Point(9, 24);
			ultraFormattedLinkLabel12.Name = "ultraFormattedLinkLabel12";
			ultraFormattedLinkLabel12.Size = new System.Drawing.Size(91, 15);
			ultraFormattedLinkLabel12.TabIndex = 128;
			ultraFormattedLinkLabel12.TabStop = true;
			ultraFormattedLinkLabel12.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel12.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel12.Value = "Work In Progress:";
			appearance170.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel12.VisitedLinkAppearance = appearance170;
			comboBoxManuWIPAccount.AlwaysInEditMode = true;
			comboBoxManuWIPAccount.Assigned = false;
			comboBoxManuWIPAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxManuWIPAccount.CustomReportFieldName = "";
			comboBoxManuWIPAccount.CustomReportKey = "";
			comboBoxManuWIPAccount.CustomReportValueType = 1;
			comboBoxManuWIPAccount.DescriptionTextBox = textBoxManuWIPAccount;
			appearance171.BackColor = System.Drawing.SystemColors.Window;
			appearance171.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxManuWIPAccount.DisplayLayout.Appearance = appearance171;
			comboBoxManuWIPAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxManuWIPAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance172.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance172.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance172.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance172.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManuWIPAccount.DisplayLayout.GroupByBox.Appearance = appearance172;
			appearance173.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManuWIPAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance173;
			comboBoxManuWIPAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance174.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance174.BackColor2 = System.Drawing.SystemColors.Control;
			appearance174.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance174.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManuWIPAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance174;
			comboBoxManuWIPAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxManuWIPAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance175.BackColor = System.Drawing.SystemColors.Window;
			appearance175.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxManuWIPAccount.DisplayLayout.Override.ActiveCellAppearance = appearance175;
			appearance176.BackColor = System.Drawing.SystemColors.Highlight;
			appearance176.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxManuWIPAccount.DisplayLayout.Override.ActiveRowAppearance = appearance176;
			comboBoxManuWIPAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxManuWIPAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance177.BackColor = System.Drawing.SystemColors.Window;
			comboBoxManuWIPAccount.DisplayLayout.Override.CardAreaAppearance = appearance177;
			appearance178.BorderColor = System.Drawing.Color.Silver;
			appearance178.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxManuWIPAccount.DisplayLayout.Override.CellAppearance = appearance178;
			comboBoxManuWIPAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxManuWIPAccount.DisplayLayout.Override.CellPadding = 0;
			appearance179.BackColor = System.Drawing.SystemColors.Control;
			appearance179.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance179.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance179.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance179.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManuWIPAccount.DisplayLayout.Override.GroupByRowAppearance = appearance179;
			appearance180.TextHAlignAsString = "Left";
			comboBoxManuWIPAccount.DisplayLayout.Override.HeaderAppearance = appearance180;
			comboBoxManuWIPAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxManuWIPAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance181.BackColor = System.Drawing.SystemColors.Window;
			appearance181.BorderColor = System.Drawing.Color.Silver;
			comboBoxManuWIPAccount.DisplayLayout.Override.RowAppearance = appearance181;
			comboBoxManuWIPAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance182.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxManuWIPAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance182;
			comboBoxManuWIPAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxManuWIPAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxManuWIPAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxManuWIPAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxManuWIPAccount.Editable = true;
			comboBoxManuWIPAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxManuWIPAccount.FilterString = "";
			comboBoxManuWIPAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxManuWIPAccount.FilterSysDocID = "";
			comboBoxManuWIPAccount.HasAllAccount = false;
			comboBoxManuWIPAccount.HasCustom = false;
			comboBoxManuWIPAccount.IsDataLoaded = false;
			comboBoxManuWIPAccount.Location = new System.Drawing.Point(123, 22);
			comboBoxManuWIPAccount.MaxDropDownItems = 12;
			comboBoxManuWIPAccount.Name = "comboBoxManuWIPAccount";
			comboBoxManuWIPAccount.ShowInactiveItems = false;
			comboBoxManuWIPAccount.ShowQuickAdd = true;
			comboBoxManuWIPAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxManuWIPAccount.TabIndex = 0;
			comboBoxManuWIPAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxManuWIPAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxManuWIPAccount.CustomReportFieldName = "";
			textBoxManuWIPAccount.CustomReportKey = "";
			textBoxManuWIPAccount.CustomReportValueType = 1;
			textBoxManuWIPAccount.ForeColor = System.Drawing.Color.Black;
			textBoxManuWIPAccount.IsComboTextBox = false;
			textBoxManuWIPAccount.Location = new System.Drawing.Point(242, 22);
			textBoxManuWIPAccount.MaxLength = 255;
			textBoxManuWIPAccount.Name = "textBoxManuWIPAccount";
			textBoxManuWIPAccount.ReadOnly = true;
			textBoxManuWIPAccount.Size = new System.Drawing.Size(303, 21);
			textBoxManuWIPAccount.TabIndex = 1;
			textBoxManuWIPAccount.TabStop = false;
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panel1.AutoScroll = true;
			panel1.Controls.Add(ultraGroupBox3);
			panel1.Controls.Add(ultraGroupBox2);
			panel1.Controls.Add(ultraGroupBox1);
			panel1.Location = new System.Drawing.Point(0, 54);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(621, 445);
			panel1.TabIndex = 2;
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
			textBoxName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(123, 30);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.ReadOnly = true;
			textBoxName.Size = new System.Drawing.Size(309, 21);
			textBoxName.TabIndex = 1;
			textBoxName.TabStop = false;
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(123, 8);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(139, 21);
			textBoxCode.TabIndex = 0;
			textBoxCode.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 30);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(96, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Location Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 8);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(93, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Location Code:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(624, 543);
			base.Controls.Add(panel1);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(620, 160);
			base.Name = "EmployeeProjectSettingForm";
			Text = "Location Accounts";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGainLossAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAPAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountReceived).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountGiven).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesTax).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGSAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInventoryAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectCostAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectIncomeAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectWIPAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			ultraGroupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxManuWIPAccount).EndInit();
			panel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
