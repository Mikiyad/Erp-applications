using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
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
	public class POSLocationDetailsForm : Form, IForm
	{
		private LocationData currentData;

		private const string TABLENAME_CONST = "Location";

		private const string IDFIELD_CONST = "LocationID";

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

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private AllAccountsComboBox comboBoxARAccount;

		private MMTextBox textBoxARName;

		private UltraGroupBox ultraGroupBox2;

		private MMTextBox textBoxCardReceivedName;

		private MMTextBox textBoxCashAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private AllAccountsComboBox comboBoxCashAccount;

		private AllAccountsComboBox comboBoxCardReceived;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6008;

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

		public POSLocationDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += POSLocationDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new LocationData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.LocationTable.Rows[0] : currentData.LocationTable.NewRow();
				dataRow.BeginEdit();
				dataRow["LocationID"] = textBoxCode.Text.Trim();
				dataRow["LocationName"] = textBoxName.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow["IsWarehouse"] = true;
				dataRow["IsPOSLocation"] = true;
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
				if (comboBoxCashAccount.SelectedID != "")
				{
					dataRow["POSCashAccountID"] = comboBoxCashAccount.SelectedID;
				}
				else
				{
					dataRow["POSCashAccountID"] = DBNull.Value;
				}
				if (comboBoxCardReceived.SelectedID != "")
				{
					dataRow["POSCardAccountID"] = comboBoxCardReceived.SelectedID;
				}
				else
				{
					dataRow["POSCardAccountID"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.LocationTable.Rows.Add(dataRow);
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
					currentData = Factory.LocationSystem.GetLocationByID(id.Trim());
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
				textBoxCode.Text = dataRow["LocationID"].ToString();
				textBoxName.Text = dataRow["LocationName"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
				comboBoxInventoryAccount.SelectedID = dataRow["InventoryAccountID"].ToString();
				comboBoxSalesAccount.SelectedID = dataRow["SalesAccountID"].ToString();
				comboBoxCOGSAccount.SelectedID = dataRow["COGSAccountID"].ToString();
				comboBoxDiscountGiven.SelectedID = dataRow["DiscountGivenAccountID"].ToString();
				comboBoxSalesTax.SelectedID = dataRow["SalesTaxAccountID"].ToString();
				comboBoxARAccount.SelectedID = dataRow["ARAccountID"].ToString();
				comboBoxCashAccount.SelectedID = dataRow["POSCashAccountID"].ToString();
				comboBoxCardReceived.SelectedID = dataRow["POSCardAccountID"].ToString();
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
					flag = Factory.LocationSystem.CreateLocation(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Location, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.LocationSystem.UpdateLocation(currentData);
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
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Location", "LocationID", textBoxCode.Text.Trim()))
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
			textBoxCode.Clear();
			textBoxName.Clear();
			textBoxNote.Clear();
			comboBoxSalesTax.Clear();
			comboBoxSalesAccount.Clear();
			comboBoxInventoryAccount.Clear();
			comboBoxCOGSAccount.Clear();
			comboBoxDiscountGiven.Clear();
			comboBoxARAccount.Clear();
			textBoxDiscountGivenAccountName.Clear();
			textBoxGOGSAccountName.Clear();
			textBoxInventoryAccountName.Clear();
			textBoxSalesAccountName.Clear();
			textBoxSalesTaxAccountName.Clear();
			comboBoxCashAccount.Clear();
			comboBoxCardReceived.Clear();
			textBoxARName.Clear();
			checkBoxInactive.Checked = false;
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

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Location", "LocationID", textBoxCode.Text, "IsPOSLocation", "True"));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Location", "LocationID", textBoxCode.Text, "IsPOSLocation", "True"));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Location", "LocationID", "IsPOSLocation", "True"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Location", "LocationID", "IsPOSLocation", "True"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Location", "LocationID", toolStripTextBoxFind.Text.Trim()))
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

		private void POSLocationDetailsForm_Load(object sender, EventArgs e)
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

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxARAccount.SelectedID);
		}

		private void comboBoxARAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxARName.Text = comboBoxARAccount.SelectedName;
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxCashAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxCardReceived.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.POSLocationDetailsForm));
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
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxARAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxARName = new Micromind.UISupport.MMTextBox();
			comboBoxDiscountGiven = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxSalesTax = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxCOGSAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxSalesAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxInventoryAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxDiscountGivenAccountName = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxSalesTaxAccountName = new Micromind.UISupport.MMTextBox();
			textBoxGOGSAccountName = new Micromind.UISupport.MMTextBox();
			textBoxSalesAccountName = new Micromind.UISupport.MMTextBox();
			textBoxInventoryAccountName = new Micromind.UISupport.MMTextBox();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxCardReceivedName = new Micromind.UISupport.MMTextBox();
			textBoxCashAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCashAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxCardReceived = new Micromind.DataControls.AllAccountsComboBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountGiven).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesTax).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGSAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInventoryAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCashAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCardReceived).BeginInit();
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
			toolStrip1.Size = new System.Drawing.Size(575, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 28);
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
			toolStripButtonFind.Size = new System.Drawing.Size(55, 28);
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
			panelButtons.Location = new System.Drawing.Point(0, 316);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(575, 40);
			panelButtons.TabIndex = 5;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(575, 1);
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
			xpButton1.Location = new System.Drawing.Point(465, 8);
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
			checkBoxInactive.Location = new System.Drawing.Point(293, 33);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			appearance.FontData.BoldAsString = "False";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(8, 19);
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
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(8, 41);
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
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(8, 63);
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
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(8, 85);
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
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(8, 107);
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
			ultraFormattedLinkLabel7.Appearance = appearance11;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(8, 128);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(68, 15);
			ultraFormattedLinkLabel7.TabIndex = 131;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "A/R Account:";
			appearance12.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance12;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			comboBoxARAccount.Assigned = false;
			comboBoxARAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxARAccount.CustomReportFieldName = "";
			comboBoxARAccount.CustomReportKey = "";
			comboBoxARAccount.CustomReportValueType = 1;
			comboBoxARAccount.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxARAccount.DisplayLayout.Appearance = appearance13;
			comboBoxARAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxARAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxARAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxARAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxARAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxARAccount.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxARAccount.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxARAccount.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxARAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxARAccount.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxARAccount.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxARAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxARAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxARAccount.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxARAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxARAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
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
			comboBoxARAccount.Location = new System.Drawing.Point(118, 126);
			comboBoxARAccount.MaxDropDownItems = 12;
			comboBoxARAccount.Name = "comboBoxARAccount";
			comboBoxARAccount.ShowInactiveItems = false;
			comboBoxARAccount.ShowQuickAdd = true;
			comboBoxARAccount.Size = new System.Drawing.Size(132, 20);
			comboBoxARAccount.TabIndex = 5;
			comboBoxARAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxARAccount.SelectedIndexChanged += new System.EventHandler(comboBoxARAccount_SelectedIndexChanged);
			textBoxARName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxARName.CustomReportFieldName = "";
			textBoxARName.CustomReportKey = "";
			textBoxARName.CustomReportValueType = 1;
			textBoxARName.ForeColor = System.Drawing.Color.Black;
			textBoxARName.IsComboTextBox = false;
			textBoxARName.Location = new System.Drawing.Point(252, 126);
			textBoxARName.MaxLength = 255;
			textBoxARName.Name = "textBoxARName";
			textBoxARName.ReadOnly = true;
			textBoxARName.Size = new System.Drawing.Size(303, 20);
			textBoxARName.TabIndex = 17;
			textBoxARName.TabStop = false;
			comboBoxDiscountGiven.Assigned = false;
			comboBoxDiscountGiven.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDiscountGiven.CustomReportFieldName = "";
			comboBoxDiscountGiven.CustomReportKey = "";
			comboBoxDiscountGiven.CustomReportValueType = 1;
			comboBoxDiscountGiven.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDiscountGiven.DisplayLayout.Appearance = appearance25;
			comboBoxDiscountGiven.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDiscountGiven.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxDiscountGiven.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDiscountGiven.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDiscountGiven.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDiscountGiven.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxDiscountGiven.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDiscountGiven.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDiscountGiven.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxDiscountGiven.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDiscountGiven.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxDiscountGiven.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxDiscountGiven.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDiscountGiven.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
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
			comboBoxDiscountGiven.Location = new System.Drawing.Point(118, 105);
			comboBoxDiscountGiven.MaxDropDownItems = 12;
			comboBoxDiscountGiven.Name = "comboBoxDiscountGiven";
			comboBoxDiscountGiven.ShowInactiveItems = false;
			comboBoxDiscountGiven.ShowQuickAdd = true;
			comboBoxDiscountGiven.Size = new System.Drawing.Size(132, 20);
			comboBoxDiscountGiven.TabIndex = 4;
			comboBoxDiscountGiven.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDiscountGiven.SelectedIndexChanged += new System.EventHandler(comboBoxDiscountGiven_SelectedIndexChanged);
			comboBoxSalesTax.Assigned = false;
			comboBoxSalesTax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesTax.CustomReportFieldName = "";
			comboBoxSalesTax.CustomReportKey = "";
			comboBoxSalesTax.CustomReportValueType = 1;
			comboBoxSalesTax.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesTax.DisplayLayout.Appearance = appearance37;
			comboBoxSalesTax.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesTax.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesTax.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxSalesTax.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesTax.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxSalesTax.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesTax.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesTax.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesTax.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxSalesTax.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesTax.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesTax.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxSalesTax.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesTax.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxSalesTax.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxSalesTax.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesTax.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesTax.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxSalesTax.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesTax.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
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
			comboBoxSalesTax.Location = new System.Drawing.Point(118, 83);
			comboBoxSalesTax.MaxDropDownItems = 12;
			comboBoxSalesTax.Name = "comboBoxSalesTax";
			comboBoxSalesTax.ShowInactiveItems = false;
			comboBoxSalesTax.ShowQuickAdd = true;
			comboBoxSalesTax.Size = new System.Drawing.Size(132, 20);
			comboBoxSalesTax.TabIndex = 3;
			comboBoxSalesTax.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSalesTax.SelectedIndexChanged += new System.EventHandler(comboBoxSalesTax_SelectedIndexChanged);
			comboBoxCOGSAccount.Assigned = false;
			comboBoxCOGSAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCOGSAccount.CustomReportFieldName = "";
			comboBoxCOGSAccount.CustomReportKey = "";
			comboBoxCOGSAccount.CustomReportValueType = 1;
			comboBoxCOGSAccount.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCOGSAccount.DisplayLayout.Appearance = appearance49;
			comboBoxCOGSAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCOGSAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxCOGSAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCOGSAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCOGSAccount.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCOGSAccount.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxCOGSAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCOGSAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCOGSAccount.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxCOGSAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCOGSAccount.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxCOGSAccount.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxCOGSAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCOGSAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
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
			comboBoxCOGSAccount.Location = new System.Drawing.Point(118, 61);
			comboBoxCOGSAccount.MaxDropDownItems = 12;
			comboBoxCOGSAccount.Name = "comboBoxCOGSAccount";
			comboBoxCOGSAccount.ShowInactiveItems = false;
			comboBoxCOGSAccount.ShowQuickAdd = true;
			comboBoxCOGSAccount.Size = new System.Drawing.Size(132, 20);
			comboBoxCOGSAccount.TabIndex = 2;
			comboBoxCOGSAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCOGSAccount.SelectedIndexChanged += new System.EventHandler(comboBoxCOGSAccount_SelectedIndexChanged);
			comboBoxSalesAccount.Assigned = false;
			comboBoxSalesAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesAccount.CustomReportFieldName = "";
			comboBoxSalesAccount.CustomReportKey = "";
			comboBoxSalesAccount.CustomReportValueType = 1;
			comboBoxSalesAccount.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesAccount.DisplayLayout.Appearance = appearance61;
			comboBoxSalesAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxSalesAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesAccount.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesAccount.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxSalesAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesAccount.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxSalesAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesAccount.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxSalesAccount.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxSalesAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesAccount.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxSalesAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
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
			comboBoxSalesAccount.Location = new System.Drawing.Point(118, 39);
			comboBoxSalesAccount.MaxDropDownItems = 12;
			comboBoxSalesAccount.Name = "comboBoxSalesAccount";
			comboBoxSalesAccount.ShowInactiveItems = false;
			comboBoxSalesAccount.ShowQuickAdd = true;
			comboBoxSalesAccount.Size = new System.Drawing.Size(132, 20);
			comboBoxSalesAccount.TabIndex = 1;
			comboBoxSalesAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSalesAccount.SelectedIndexChanged += new System.EventHandler(comboBoxSalesAccount_SelectedIndexChanged);
			comboBoxInventoryAccount.Assigned = false;
			comboBoxInventoryAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxInventoryAccount.CustomReportFieldName = "";
			comboBoxInventoryAccount.CustomReportKey = "";
			comboBoxInventoryAccount.CustomReportValueType = 1;
			comboBoxInventoryAccount.DescriptionTextBox = null;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxInventoryAccount.DisplayLayout.Appearance = appearance73;
			comboBoxInventoryAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxInventoryAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxInventoryAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxInventoryAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxInventoryAccount.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxInventoryAccount.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxInventoryAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxInventoryAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryAccount.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxInventoryAccount.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxInventoryAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxInventoryAccount.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryAccount.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxInventoryAccount.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxInventoryAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxInventoryAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxInventoryAccount.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxInventoryAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxInventoryAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
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
			comboBoxInventoryAccount.Location = new System.Drawing.Point(118, 17);
			comboBoxInventoryAccount.MaxDropDownItems = 12;
			comboBoxInventoryAccount.Name = "comboBoxInventoryAccount";
			comboBoxInventoryAccount.ShowInactiveItems = false;
			comboBoxInventoryAccount.ShowQuickAdd = true;
			comboBoxInventoryAccount.Size = new System.Drawing.Size(132, 20);
			comboBoxInventoryAccount.TabIndex = 0;
			comboBoxInventoryAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxInventoryAccount.SelectedIndexChanged += new System.EventHandler(comboBoxInventoryAccount_SelectedIndexChanged);
			textBoxDiscountGivenAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDiscountGivenAccountName.CustomReportFieldName = "";
			textBoxDiscountGivenAccountName.CustomReportKey = "";
			textBoxDiscountGivenAccountName.CustomReportValueType = 1;
			textBoxDiscountGivenAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxDiscountGivenAccountName.IsComboTextBox = false;
			textBoxDiscountGivenAccountName.Location = new System.Drawing.Point(252, 105);
			textBoxDiscountGivenAccountName.MaxLength = 255;
			textBoxDiscountGivenAccountName.Name = "textBoxDiscountGivenAccountName";
			textBoxDiscountGivenAccountName.ReadOnly = true;
			textBoxDiscountGivenAccountName.Size = new System.Drawing.Size(303, 20);
			textBoxDiscountGivenAccountName.TabIndex = 13;
			textBoxDiscountGivenAccountName.TabStop = false;
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
			textBoxSalesTaxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSalesTaxAccountName.CustomReportFieldName = "";
			textBoxSalesTaxAccountName.CustomReportKey = "";
			textBoxSalesTaxAccountName.CustomReportValueType = 1;
			textBoxSalesTaxAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxSalesTaxAccountName.IsComboTextBox = false;
			textBoxSalesTaxAccountName.Location = new System.Drawing.Point(252, 83);
			textBoxSalesTaxAccountName.MaxLength = 255;
			textBoxSalesTaxAccountName.Name = "textBoxSalesTaxAccountName";
			textBoxSalesTaxAccountName.ReadOnly = true;
			textBoxSalesTaxAccountName.Size = new System.Drawing.Size(303, 20);
			textBoxSalesTaxAccountName.TabIndex = 11;
			textBoxSalesTaxAccountName.TabStop = false;
			textBoxGOGSAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGOGSAccountName.CustomReportFieldName = "";
			textBoxGOGSAccountName.CustomReportKey = "";
			textBoxGOGSAccountName.CustomReportValueType = 1;
			textBoxGOGSAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxGOGSAccountName.IsComboTextBox = false;
			textBoxGOGSAccountName.Location = new System.Drawing.Point(252, 61);
			textBoxGOGSAccountName.MaxLength = 255;
			textBoxGOGSAccountName.Name = "textBoxGOGSAccountName";
			textBoxGOGSAccountName.ReadOnly = true;
			textBoxGOGSAccountName.Size = new System.Drawing.Size(303, 20);
			textBoxGOGSAccountName.TabIndex = 9;
			textBoxGOGSAccountName.TabStop = false;
			textBoxSalesAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSalesAccountName.CustomReportFieldName = "";
			textBoxSalesAccountName.CustomReportKey = "";
			textBoxSalesAccountName.CustomReportValueType = 1;
			textBoxSalesAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxSalesAccountName.IsComboTextBox = false;
			textBoxSalesAccountName.Location = new System.Drawing.Point(252, 39);
			textBoxSalesAccountName.MaxLength = 255;
			textBoxSalesAccountName.Name = "textBoxSalesAccountName";
			textBoxSalesAccountName.ReadOnly = true;
			textBoxSalesAccountName.Size = new System.Drawing.Size(303, 20);
			textBoxSalesAccountName.TabIndex = 7;
			textBoxSalesAccountName.TabStop = false;
			textBoxInventoryAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxInventoryAccountName.CustomReportFieldName = "";
			textBoxInventoryAccountName.CustomReportKey = "";
			textBoxInventoryAccountName.CustomReportValueType = 1;
			textBoxInventoryAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxInventoryAccountName.IsComboTextBox = false;
			textBoxInventoryAccountName.Location = new System.Drawing.Point(252, 17);
			textBoxInventoryAccountName.MaxLength = 255;
			textBoxInventoryAccountName.Name = "textBoxInventoryAccountName";
			textBoxInventoryAccountName.ReadOnly = true;
			textBoxInventoryAccountName.Size = new System.Drawing.Size(303, 20);
			textBoxInventoryAccountName.TabIndex = 5;
			textBoxInventoryAccountName.TabStop = false;
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(123, 75);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(338, 20);
			textBoxNote.TabIndex = 3;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(123, 53);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(338, 20);
			textBoxName.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(123, 31);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(164, 20);
			textBoxCode.TabIndex = 0;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(8, 76);
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
			mmLabel1.Location = new System.Drawing.Point(8, 53);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(77, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Store Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 31);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(74, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Store Code:";
			ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox2.Controls.Add(textBoxCardReceivedName);
			ultraGroupBox2.Controls.Add(textBoxCashAccountName);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel6);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel8);
			ultraGroupBox2.Controls.Add(comboBoxCashAccount);
			ultraGroupBox2.Controls.Add(comboBoxCardReceived);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel1);
			ultraGroupBox2.Controls.Add(textBoxInventoryAccountName);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel7);
			ultraGroupBox2.Controls.Add(textBoxSalesAccountName);
			ultraGroupBox2.Controls.Add(comboBoxARAccount);
			ultraGroupBox2.Controls.Add(textBoxGOGSAccountName);
			ultraGroupBox2.Controls.Add(textBoxARName);
			ultraGroupBox2.Controls.Add(textBoxSalesTaxAccountName);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel5);
			ultraGroupBox2.Controls.Add(textBoxDiscountGivenAccountName);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel4);
			ultraGroupBox2.Controls.Add(comboBoxInventoryAccount);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel3);
			ultraGroupBox2.Controls.Add(comboBoxSalesAccount);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel2);
			ultraGroupBox2.Controls.Add(comboBoxCOGSAccount);
			ultraGroupBox2.Controls.Add(comboBoxDiscountGiven);
			ultraGroupBox2.Controls.Add(comboBoxSalesTax);
			ultraGroupBox2.Location = new System.Drawing.Point(5, 110);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(592, 197);
			ultraGroupBox2.TabIndex = 4;
			ultraGroupBox2.Text = "Accounts";
			textBoxCardReceivedName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCardReceivedName.CustomReportFieldName = "";
			textBoxCardReceivedName.CustomReportKey = "";
			textBoxCardReceivedName.CustomReportValueType = 1;
			textBoxCardReceivedName.IsComboTextBox = false;
			textBoxCardReceivedName.Location = new System.Drawing.Point(252, 170);
			textBoxCardReceivedName.MaxLength = 255;
			textBoxCardReceivedName.Name = "textBoxCardReceivedName";
			textBoxCardReceivedName.ReadOnly = true;
			textBoxCardReceivedName.Size = new System.Drawing.Size(303, 20);
			textBoxCardReceivedName.TabIndex = 136;
			textBoxCardReceivedName.TabStop = false;
			textBoxCashAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCashAccountName.CustomReportFieldName = "";
			textBoxCashAccountName.CustomReportKey = "";
			textBoxCashAccountName.CustomReportValueType = 1;
			textBoxCashAccountName.IsComboTextBox = false;
			textBoxCashAccountName.Location = new System.Drawing.Point(252, 148);
			textBoxCashAccountName.MaxLength = 255;
			textBoxCashAccountName.Name = "textBoxCashAccountName";
			textBoxCashAccountName.ReadOnly = true;
			textBoxCashAccountName.Size = new System.Drawing.Size(303, 20);
			textBoxCashAccountName.TabIndex = 137;
			textBoxCashAccountName.TabStop = false;
			appearance85.FontData.BoldAsString = "False";
			ultraFormattedLinkLabel6.Appearance = appearance85;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(7, 149);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(31, 14);
			ultraFormattedLinkLabel6.TabIndex = 134;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Cash:";
			appearance86.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance86;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			appearance87.FontData.BoldAsString = "False";
			ultraFormattedLinkLabel8.Appearance = appearance87;
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(7, 171);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(29, 14);
			ultraFormattedLinkLabel8.TabIndex = 135;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Card:";
			appearance88.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance88;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			comboBoxCashAccount.Assigned = false;
			comboBoxCashAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCashAccount.CustomReportFieldName = "";
			comboBoxCashAccount.CustomReportKey = "";
			comboBoxCashAccount.CustomReportValueType = 1;
			comboBoxCashAccount.DescriptionTextBox = textBoxCashAccountName;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCashAccount.DisplayLayout.Appearance = appearance89;
			comboBoxCashAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCashAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance90.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCashAccount.DisplayLayout.GroupByBox.Appearance = appearance90;
			appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCashAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance91;
			comboBoxCashAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance92.BackColor2 = System.Drawing.SystemColors.Control;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance92.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCashAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance92;
			comboBoxCashAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCashAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCashAccount.DisplayLayout.Override.ActiveCellAppearance = appearance93;
			appearance94.BackColor = System.Drawing.SystemColors.Highlight;
			appearance94.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCashAccount.DisplayLayout.Override.ActiveRowAppearance = appearance94;
			comboBoxCashAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCashAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCashAccount.DisplayLayout.Override.CardAreaAppearance = appearance95;
			appearance96.BorderColor = System.Drawing.Color.Silver;
			appearance96.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCashAccount.DisplayLayout.Override.CellAppearance = appearance96;
			comboBoxCashAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCashAccount.DisplayLayout.Override.CellPadding = 0;
			appearance97.BackColor = System.Drawing.SystemColors.Control;
			appearance97.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance97.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance97.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCashAccount.DisplayLayout.Override.GroupByRowAppearance = appearance97;
			appearance98.TextHAlignAsString = "Left";
			comboBoxCashAccount.DisplayLayout.Override.HeaderAppearance = appearance98;
			comboBoxCashAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCashAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.BorderColor = System.Drawing.Color.Silver;
			comboBoxCashAccount.DisplayLayout.Override.RowAppearance = appearance99;
			comboBoxCashAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCashAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance100;
			comboBoxCashAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCashAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCashAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCashAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCashAccount.Editable = true;
			comboBoxCashAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxCashAccount.FilterString = "";
			comboBoxCashAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxCashAccount.FilterSysDocID = "";
			comboBoxCashAccount.HasAllAccount = false;
			comboBoxCashAccount.HasCustom = false;
			comboBoxCashAccount.IsDataLoaded = false;
			comboBoxCashAccount.Location = new System.Drawing.Point(118, 148);
			comboBoxCashAccount.MaxDropDownItems = 12;
			comboBoxCashAccount.Name = "comboBoxCashAccount";
			comboBoxCashAccount.ShowInactiveItems = false;
			comboBoxCashAccount.ShowQuickAdd = true;
			comboBoxCashAccount.Size = new System.Drawing.Size(132, 20);
			comboBoxCashAccount.TabIndex = 6;
			comboBoxCashAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCardReceived.Assigned = false;
			comboBoxCardReceived.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCardReceived.CustomReportFieldName = "";
			comboBoxCardReceived.CustomReportKey = "";
			comboBoxCardReceived.CustomReportValueType = 1;
			comboBoxCardReceived.DescriptionTextBox = textBoxCardReceivedName;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCardReceived.DisplayLayout.Appearance = appearance101;
			comboBoxCardReceived.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCardReceived.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance102.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCardReceived.DisplayLayout.GroupByBox.Appearance = appearance102;
			appearance103.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCardReceived.DisplayLayout.GroupByBox.BandLabelAppearance = appearance103;
			comboBoxCardReceived.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance104.BackColor2 = System.Drawing.SystemColors.Control;
			appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance104.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCardReceived.DisplayLayout.GroupByBox.PromptAppearance = appearance104;
			comboBoxCardReceived.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCardReceived.DisplayLayout.MaxRowScrollRegions = 1;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCardReceived.DisplayLayout.Override.ActiveCellAppearance = appearance105;
			appearance106.BackColor = System.Drawing.SystemColors.Highlight;
			appearance106.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCardReceived.DisplayLayout.Override.ActiveRowAppearance = appearance106;
			comboBoxCardReceived.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCardReceived.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCardReceived.DisplayLayout.Override.CardAreaAppearance = appearance107;
			appearance108.BorderColor = System.Drawing.Color.Silver;
			appearance108.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCardReceived.DisplayLayout.Override.CellAppearance = appearance108;
			comboBoxCardReceived.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCardReceived.DisplayLayout.Override.CellPadding = 0;
			appearance109.BackColor = System.Drawing.SystemColors.Control;
			appearance109.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance109.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance109.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCardReceived.DisplayLayout.Override.GroupByRowAppearance = appearance109;
			appearance110.TextHAlignAsString = "Left";
			comboBoxCardReceived.DisplayLayout.Override.HeaderAppearance = appearance110;
			comboBoxCardReceived.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCardReceived.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.BorderColor = System.Drawing.Color.Silver;
			comboBoxCardReceived.DisplayLayout.Override.RowAppearance = appearance111;
			comboBoxCardReceived.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCardReceived.DisplayLayout.Override.TemplateAddRowAppearance = appearance112;
			comboBoxCardReceived.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCardReceived.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCardReceived.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCardReceived.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCardReceived.Editable = true;
			comboBoxCardReceived.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxCardReceived.FilterString = "";
			comboBoxCardReceived.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxCardReceived.FilterSysDocID = "";
			comboBoxCardReceived.HasAllAccount = false;
			comboBoxCardReceived.HasCustom = false;
			comboBoxCardReceived.IsDataLoaded = false;
			comboBoxCardReceived.Location = new System.Drawing.Point(118, 169);
			comboBoxCardReceived.MaxDropDownItems = 12;
			comboBoxCardReceived.Name = "comboBoxCardReceived";
			comboBoxCardReceived.ShowInactiveItems = false;
			comboBoxCardReceived.ShowQuickAdd = true;
			comboBoxCardReceived.Size = new System.Drawing.Size(132, 20);
			comboBoxCardReceived.TabIndex = 7;
			comboBoxCardReceived.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(575, 356);
			base.Controls.Add(ultraGroupBox2);
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
			base.Name = "POSLocationDetailsForm";
			Text = "POS Store";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountGiven).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesTax).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGSAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInventoryAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCashAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCardReceived).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
