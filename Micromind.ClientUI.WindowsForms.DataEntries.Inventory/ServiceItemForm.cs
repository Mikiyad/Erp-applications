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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ServiceItemForm : Form, IForm
	{
		private ServiceItemData currentData;

		private const string TABLENAME_CONST = "Service_Item";

		private const string IDFIELD_CONST = "ServiceItemID";

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

		private MMTextBox textBoxDescription;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMLabel mmLabel5;

		private ToolStripButton toolStripButtonInformation;

		private MMLabel mmLabel6;

		private MMTextBox textboxNote;

		private GenericListComboBox comboVehicleType;

		private ComboBox comboServiceType;

		private MMLabel mmLabel2;

		private MMLabel mmLabel3;

		private MMTextBox textBoxVehicleType;

		private MMLabel mmLabel8;

		private MMLabel mmLabel9;

		private MMLabel mmLabel4;

		private MMLabel mmLabel10;

		private NumberTextBox textBoxRepeatDays;

		private NumberTextBox textboxkm1;

		private NumberTextBox textBoxreminderdays;

		private NumberTextBox textboxkm2;

		private MMLabel mmLabel11;

		private MMLabel mmLabel7;

		private MMTextBox textBoxAPName;

		private AllAccountsComboBox comboBoxAPAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraGroupBox ultraGroupBox7;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel22;

		private TaxGroupComboBox comboBoxTaxGroup;

		private MMLabel mmLabel1;

		private ComboBox comboBoxTaxOption;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6011;

		public ScreenTypes ScreenType => ScreenTypes.Other;

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

		public ServiceItemForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += ServiceItemForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ServiceItemData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ServiceItemTable.Rows[0] : currentData.ServiceItemTable.NewRow();
				dataRow.BeginEdit();
				dataRow["ServiceItemID"] = textBoxCode.Text.Trim();
				dataRow["Description"] = textBoxDescription.Text;
				dataRow["ServiceType"] = checked(comboServiceType.SelectedIndex + 1);
				if (textBoxRepeatDays.Text.Trim().Length != 0 || textBoxRepeatDays.Text.Trim().Length != 0)
				{
					dataRow["RepeatCountDays"] = textBoxRepeatDays.Text;
				}
				else
				{
					dataRow["RepeatCountDays"] = 0;
				}
				if (textboxkm1.Text.Trim().Length != 0 || textboxkm1.Text.Trim().Length != 0)
				{
					dataRow["RepeatCountKM"] = textboxkm1.Text.Replace(",", "");
				}
				else
				{
					dataRow["RepeatCountKM"] = 0;
				}
				if (textBoxreminderdays.Text.Trim().Length != 0 || textBoxreminderdays.Text.Trim().Length != 0)
				{
					dataRow["ReminderDays"] = textBoxreminderdays.Text;
				}
				else
				{
					dataRow["ReminderDays"] = 0;
				}
				if (textboxkm2.Text.Trim().Length != 0 || textboxkm2.Text.Trim().Length != 0)
				{
					dataRow["ReminderKM"] = textboxkm2.Text;
				}
				else
				{
					dataRow["ReminderKM"] = 0;
				}
				if (comboBoxAPAccount.SelectedID != "" && comboBoxAPAccount.SelectedID != null)
				{
					dataRow["APAccountID"] = comboBoxAPAccount.SelectedID;
				}
				else
				{
					dataRow["APAccountID"] = DBNull.Value;
				}
				if (comboVehicleType.SelectedID != "" && comboVehicleType.SelectedID != null)
				{
					dataRow["VehicleType"] = comboVehicleType.SelectedID;
				}
				else
				{
					dataRow["VehicleType"] = DBNull.Value;
				}
				dataRow["Note"] = textboxNote.Text;
				dataRow["Inactive"] = checkBoxInactive.Checked;
				dataRow["TaxOption"] = comboBoxTaxOption.SelectedIndex;
				if (comboBoxTaxOption.SelectedIndex == 1)
				{
					dataRow["TaxGroupID"] = comboBoxTaxGroup.SelectedID;
				}
				else
				{
					dataRow["TaxGroupID"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.ServiceItemTable.Rows.Add(dataRow);
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
					currentData = Factory.ServiceTypeSystem.GetServiceItemByID(id.Trim());
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
				textBoxCode.Text = dataRow["ServiceItemID"].ToString();
				textBoxDescription.Text = dataRow["Description"].ToString();
				textBoxRepeatDays.Text = dataRow["RepeatCountDays"].ToString();
				textboxkm1.Text = dataRow["RepeatCountKM"].ToString();
				comboServiceType.SelectedIndex = checked(int.Parse(dataRow["ServiceType"].ToString()) - 1);
				comboVehicleType.SelectedID = dataRow["VehicleType"].ToString();
				textBoxreminderdays.Text = dataRow["ReminderDays"].ToString();
				textboxkm2.Text = dataRow["ReminderKM"].ToString();
				checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
				comboBoxAPAccount.SelectedID = dataRow["APAccountID"].ToString();
				textboxNote.Text = dataRow["Note"].ToString();
				textBoxVehicleType.Text = dataRow["VehicleType"].ToString();
				if (!string.IsNullOrEmpty(dataRow["TaxOption"].ToString()))
				{
					comboBoxTaxOption.SelectedIndex = int.Parse(dataRow["TaxOption"].ToString());
				}
				else
				{
					comboBoxTaxOption.SelectedIndex = 0;
				}
				if (!string.IsNullOrEmpty(dataRow["TaxGroupID"].ToString()))
				{
					comboBoxTaxGroup.SelectedID = dataRow["TaxGroupID"].ToString();
				}
				else
				{
					comboBoxTaxGroup.Clear();
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
					flag = Factory.ServiceTypeSystem.CreateServiceItem(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.ServiceItem, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.ServiceTypeSystem.UpdateServiceItem(currentData);
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
			if (comboServiceType.SelectedIndex < 0 || comboServiceType.SelectedIndex.ToString() == null)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (comboVehicleType.SelectedID == "" || comboVehicleType.SelectedID == null)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (comboBoxAPAccount.SelectedID == "" || comboVehicleType.SelectedID == null)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxDescription.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Service_Item", "ServiceItemID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Service_Item", "ServiceItemID");
			textBoxDescription.Clear();
			textBoxRepeatDays.Clear();
			textboxkm1.Clear();
			textboxkm2.Clear();
			comboVehicleType.Clear();
			comboBoxAPAccount.Clear();
			comboServiceType.SelectedIndex = -1;
			textBoxVehicleType.Clear();
			textboxNote.Clear();
			textBoxreminderdays.Clear();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
			comboBoxTaxOption.SelectedIndex = -1;
			comboBoxTaxGroup.Clear();
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
				bool num = Factory.ServiceTypeSystem.DeleteServiceItem(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.ServiceItem, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("Service_Item", "ServiceItemID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Service_Item", "ServiceItemID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Service_Item", "ServiceItemID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Service_Item", "ServiceItemID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Service_Item", "ServiceItemID", toolStripTextBoxFind.Text.Trim()))
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

		private void ServiceItemForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				textBoxRepeatDays.Text = "";
				textboxkm1.Text = "";
				textBoxreminderdays.Text = "";
				textboxkm2.Text = "";
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
			new FormHelper().ShowList(DataComboType.ServiceItem);
		}

		private void comboVehicleType_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxVehicleType.Text = comboVehicleType.SelectedName;
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void linkLabelARAccount_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAPAccount.Text);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAPAccount.SelectedID);
		}

		private void comboBoxTaxOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxTaxOption.SelectedIndex == 1)
			{
				comboBoxTaxGroup.ReadOnly = false;
				return;
			}
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.Clear();
		}

		private void ultraFormattedLinkLabel22_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxTaxGroup.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ServiceItemForm));
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
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			comboServiceType = new System.Windows.Forms.ComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox7 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel22 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxTaxOption = new System.Windows.Forms.ComboBox();
			comboBoxTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxAPName = new Micromind.UISupport.MMTextBox();
			comboBoxAPAccount = new Micromind.DataControls.AllAccountsComboBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			textboxkm2 = new Micromind.UISupport.NumberTextBox();
			textBoxreminderdays = new Micromind.UISupport.NumberTextBox();
			textboxkm1 = new Micromind.UISupport.NumberTextBox();
			textBoxRepeatDays = new Micromind.UISupport.NumberTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboVehicleType = new Micromind.DataControls.GenericListComboBox();
			textBoxVehicleType = new Micromind.UISupport.MMTextBox();
			textboxNote = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxDescription = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			labelCode = new Micromind.UISupport.MMLabel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).BeginInit();
			ultraGroupBox7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAPAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboVehicleType).BeginInit();
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
			toolStrip1.Size = new System.Drawing.Size(543, 31);
			toolStrip1.TabIndex = 14;
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
			panelButtons.Location = new System.Drawing.Point(0, 368);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(543, 40);
			panelButtons.TabIndex = 13;
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(293, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			comboServiceType.FormattingEnabled = true;
			comboServiceType.Items.AddRange(new object[5]
			{
				"None",
				"Repair",
				"Maintenance",
				"Inspection",
				"Renewals"
			});
			comboServiceType.Location = new System.Drawing.Point(113, 82);
			comboServiceType.Name = "comboServiceType";
			comboServiceType.Size = new System.Drawing.Size(117, 21);
			comboServiceType.TabIndex = 3;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(8, 178);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(100, 15);
			ultraFormattedLinkLabel5.TabIndex = 153;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Account Payable:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			ultraGroupBox7.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox7.Controls.Add(ultraFormattedLinkLabel22);
			ultraGroupBox7.Controls.Add(comboBoxTaxGroup);
			ultraGroupBox7.Controls.Add(mmLabel1);
			ultraGroupBox7.Controls.Add(comboBoxTaxOption);
			ultraGroupBox7.Location = new System.Drawing.Point(11, 283);
			ultraGroupBox7.Name = "ultraGroupBox7";
			ultraGroupBox7.Size = new System.Drawing.Size(516, 79);
			ultraGroupBox7.TabIndex = 154;
			ultraGroupBox7.Text = "Tax Details";
			ultraFormattedLinkLabel22.AutoSize = true;
			appearance3.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel22.LinkAppearance = appearance3;
			ultraFormattedLinkLabel22.Location = new System.Drawing.Point(8, 48);
			ultraFormattedLinkLabel22.Name = "ultraFormattedLinkLabel22";
			ultraFormattedLinkLabel22.Size = new System.Drawing.Size(59, 14);
			ultraFormattedLinkLabel22.TabIndex = 73;
			ultraFormattedLinkLabel22.TabStop = true;
			ultraFormattedLinkLabel22.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel22.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel22.Value = "Tax Group:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel22.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel22.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel22_LinkClicked);
			comboBoxTaxOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxTaxOption.ForeColor = System.Drawing.SystemColors.WindowText;
			comboBoxTaxOption.FormattingEnabled = true;
			comboBoxTaxOption.Items.AddRange(new object[3]
			{
				"Based on Customer",
				"Taxable",
				"Non Taxable"
			});
			comboBoxTaxOption.Location = new System.Drawing.Point(100, 22);
			comboBoxTaxOption.Name = "comboBoxTaxOption";
			comboBoxTaxOption.Size = new System.Drawing.Size(156, 21);
			comboBoxTaxOption.TabIndex = 0;
			comboBoxTaxOption.SelectedIndexChanged += new System.EventHandler(comboBoxTaxOption_SelectedIndexChanged);
			comboBoxTaxGroup.Assigned = false;
			comboBoxTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaxGroup.CustomReportFieldName = "";
			comboBoxTaxGroup.CustomReportKey = "";
			comboBoxTaxGroup.CustomReportValueType = 1;
			comboBoxTaxGroup.DescriptionTextBox = null;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaxGroup.DisplayLayout.Appearance = appearance5;
			comboBoxTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaxGroup.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaxGroup.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaxGroup.Editable = true;
			comboBoxTaxGroup.FilterString = "";
			comboBoxTaxGroup.HasAllAccount = false;
			comboBoxTaxGroup.HasCustom = false;
			comboBoxTaxGroup.IsDataLoaded = false;
			comboBoxTaxGroup.Location = new System.Drawing.Point(100, 46);
			comboBoxTaxGroup.MaxDropDownItems = 12;
			comboBoxTaxGroup.Name = "comboBoxTaxGroup";
			comboBoxTaxGroup.ReadOnly = true;
			comboBoxTaxGroup.ShowInactiveItems = false;
			comboBoxTaxGroup.ShowQuickAdd = true;
			comboBoxTaxGroup.Size = new System.Drawing.Size(156, 20);
			comboBoxTaxGroup.TabIndex = 1;
			comboBoxTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(8, 26);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(64, 13);
			mmLabel1.TabIndex = 70;
			mmLabel1.Text = "Tax Option:";
			textBoxAPName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAPName.CustomReportFieldName = "";
			textBoxAPName.CustomReportKey = "";
			textBoxAPName.CustomReportValueType = 1;
			textBoxAPName.IsComboTextBox = true;
			textBoxAPName.IsModified = false;
			textBoxAPName.Location = new System.Drawing.Point(306, 176);
			textBoxAPName.MaxLength = 30;
			textBoxAPName.Name = "textBoxAPName";
			textBoxAPName.ReadOnly = true;
			textBoxAPName.Size = new System.Drawing.Size(221, 20);
			textBoxAPName.TabIndex = 11;
			textBoxAPName.TabStop = false;
			comboBoxAPAccount.Assigned = false;
			comboBoxAPAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAPAccount.CustomReportFieldName = "";
			comboBoxAPAccount.CustomReportKey = "";
			comboBoxAPAccount.CustomReportValueType = 1;
			comboBoxAPAccount.DescriptionTextBox = textBoxAPName;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAPAccount.DisplayLayout.Appearance = appearance17;
			comboBoxAPAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAPAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAPAccount.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAPAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxAPAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAPAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxAPAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAPAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAPAccount.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAPAccount.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxAPAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAPAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAPAccount.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAPAccount.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxAPAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAPAccount.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAPAccount.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxAPAccount.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxAPAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAPAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxAPAccount.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxAPAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAPAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
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
			comboBoxAPAccount.Location = new System.Drawing.Point(113, 175);
			comboBoxAPAccount.MaxDropDownItems = 12;
			comboBoxAPAccount.MaxLength = 64;
			comboBoxAPAccount.Name = "comboBoxAPAccount";
			comboBoxAPAccount.ShowInactiveItems = false;
			comboBoxAPAccount.ShowQuickAdd = true;
			comboBoxAPAccount.Size = new System.Drawing.Size(190, 20);
			comboBoxAPAccount.TabIndex = 10;
			comboBoxAPAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = true;
			mmLabel7.Location = new System.Drawing.Point(8, 156);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(85, 13);
			mmLabel7.TabIndex = 27;
			mmLabel7.Text = "Vehicle Type:";
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = true;
			mmLabel11.Location = new System.Drawing.Point(8, 86);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(86, 13);
			mmLabel11.TabIndex = 24;
			mmLabel11.Text = "Service Type:";
			textboxkm2.AllowDecimal = false;
			textboxkm2.CustomReportFieldName = "";
			textboxkm2.CustomReportKey = "";
			textboxkm2.CustomReportValueType = 1;
			textboxkm2.IsComboTextBox = false;
			textboxkm2.IsModified = false;
			textboxkm2.Location = new System.Drawing.Point(236, 129);
			textboxkm2.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textboxkm2.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textboxkm2.Name = "textboxkm2";
			textboxkm2.NullText = "0";
			textboxkm2.Size = new System.Drawing.Size(67, 20);
			textboxkm2.TabIndex = 7;
			textboxkm2.Text = "0";
			textBoxreminderdays.AllowDecimal = false;
			textBoxreminderdays.CustomReportFieldName = "";
			textBoxreminderdays.CustomReportKey = "";
			textBoxreminderdays.CustomReportValueType = 1;
			textBoxreminderdays.IsComboTextBox = false;
			textBoxreminderdays.IsModified = false;
			textBoxreminderdays.Location = new System.Drawing.Point(113, 129);
			textBoxreminderdays.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxreminderdays.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxreminderdays.Name = "textBoxreminderdays";
			textBoxreminderdays.NullText = "0";
			textBoxreminderdays.Size = new System.Drawing.Size(63, 20);
			textBoxreminderdays.TabIndex = 6;
			textBoxreminderdays.Text = "0";
			textboxkm1.AllowDecimal = false;
			textboxkm1.CustomReportFieldName = "";
			textboxkm1.CustomReportKey = "";
			textboxkm1.CustomReportValueType = 1;
			textboxkm1.IsComboTextBox = false;
			textboxkm1.IsModified = false;
			textboxkm1.Location = new System.Drawing.Point(236, 106);
			textboxkm1.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textboxkm1.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textboxkm1.Name = "textboxkm1";
			textboxkm1.NullText = "0";
			textboxkm1.Size = new System.Drawing.Size(67, 20);
			textboxkm1.TabIndex = 5;
			textboxkm1.Text = "0";
			textBoxRepeatDays.AllowDecimal = false;
			textBoxRepeatDays.CustomReportFieldName = "";
			textBoxRepeatDays.CustomReportKey = "";
			textBoxRepeatDays.CustomReportValueType = 1;
			textBoxRepeatDays.IsComboTextBox = false;
			textBoxRepeatDays.IsModified = false;
			textBoxRepeatDays.Location = new System.Drawing.Point(113, 106);
			textBoxRepeatDays.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxRepeatDays.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxRepeatDays.Name = "textBoxRepeatDays";
			textBoxRepeatDays.NullText = "0";
			textBoxRepeatDays.Size = new System.Drawing.Size(63, 20);
			textBoxRepeatDays.TabIndex = 4;
			textBoxRepeatDays.Text = "0";
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(180, 129);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(31, 13);
			mmLabel10.TabIndex = 21;
			mmLabel10.Text = "Days";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(306, 109);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(28, 13);
			mmLabel4.TabIndex = 17;
			mmLabel4.Text = "KMs";
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(180, 108);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(31, 13);
			mmLabel9.TabIndex = 17;
			mmLabel9.Text = "Days";
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(307, 129);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(28, 13);
			mmLabel8.TabIndex = 16;
			mmLabel8.Text = "KMs";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(8, 63);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(75, 13);
			mmLabel3.TabIndex = 23;
			mmLabel3.Text = "Description:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(8, 110);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(90, 13);
			mmLabel2.TabIndex = 25;
			mmLabel2.Text = "Repeat on Every:";
			comboVehicleType.Assigned = false;
			comboVehicleType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboVehicleType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboVehicleType.CustomReportFieldName = "";
			comboVehicleType.CustomReportKey = "";
			comboVehicleType.CustomReportValueType = 1;
			comboVehicleType.DescriptionTextBox = textBoxVehicleType;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboVehicleType.DisplayLayout.Appearance = appearance29;
			comboVehicleType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboVehicleType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboVehicleType.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboVehicleType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboVehicleType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboVehicleType.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboVehicleType.DisplayLayout.MaxColScrollRegions = 1;
			comboVehicleType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboVehicleType.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboVehicleType.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboVehicleType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboVehicleType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboVehicleType.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboVehicleType.DisplayLayout.Override.CellAppearance = appearance36;
			comboVehicleType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboVehicleType.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboVehicleType.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboVehicleType.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboVehicleType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboVehicleType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboVehicleType.DisplayLayout.Override.RowAppearance = appearance39;
			comboVehicleType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboVehicleType.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboVehicleType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboVehicleType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboVehicleType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboVehicleType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboVehicleType.Editable = true;
			comboVehicleType.FilterString = "";
			comboVehicleType.GenericListType = Micromind.Common.Data.GenericListTypes.VehicleType;
			comboVehicleType.HasAllAccount = false;
			comboVehicleType.HasCustom = false;
			comboVehicleType.IsDataLoaded = false;
			comboVehicleType.IsSingleColumn = false;
			comboVehicleType.Location = new System.Drawing.Point(113, 152);
			comboVehicleType.MaxDropDownItems = 12;
			comboVehicleType.Name = "comboVehicleType";
			comboVehicleType.ShowInactiveItems = false;
			comboVehicleType.ShowQuickAdd = true;
			comboVehicleType.Size = new System.Drawing.Size(134, 20);
			comboVehicleType.TabIndex = 8;
			comboVehicleType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxVehicleType.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVehicleType.CustomReportFieldName = "";
			textBoxVehicleType.CustomReportKey = "";
			textBoxVehicleType.CustomReportValueType = 1;
			textBoxVehicleType.IsComboTextBox = false;
			textBoxVehicleType.IsModified = false;
			textBoxVehicleType.Location = new System.Drawing.Point(247, 152);
			textBoxVehicleType.MaxLength = 30;
			textBoxVehicleType.Name = "textBoxVehicleType";
			textBoxVehicleType.ReadOnly = true;
			textBoxVehicleType.Size = new System.Drawing.Size(280, 20);
			textBoxVehicleType.TabIndex = 9;
			textBoxVehicleType.TabStop = false;
			textboxNote.BackColor = System.Drawing.Color.White;
			textboxNote.CustomReportFieldName = "";
			textboxNote.CustomReportKey = "";
			textboxNote.CustomReportValueType = 1;
			textboxNote.IsComboTextBox = false;
			textboxNote.IsModified = false;
			textboxNote.Location = new System.Drawing.Point(113, 198);
			textboxNote.MaxLength = 64;
			textboxNote.Multiline = true;
			textboxNote.Name = "textboxNote";
			textboxNote.Size = new System.Drawing.Size(414, 78);
			textboxNote.TabIndex = 12;
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(8, 201);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(33, 13);
			mmLabel6.TabIndex = 19;
			mmLabel6.Text = "Note:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(8, 133);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(89, 13);
			mmLabel5.TabIndex = 26;
			mmLabel5.Text = "Reminder Before:";
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
			textBoxDescription.BackColor = System.Drawing.Color.White;
			textBoxDescription.CustomReportFieldName = "";
			textBoxDescription.CustomReportKey = "";
			textBoxDescription.CustomReportValueType = 1;
			textBoxDescription.IsComboTextBox = false;
			textBoxDescription.IsModified = false;
			textBoxDescription.Location = new System.Drawing.Point(113, 59);
			textBoxDescription.MaxLength = 64;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.Size = new System.Drawing.Size(320, 20);
			textBoxDescription.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(113, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(175, 20);
			textBoxCode.TabIndex = 0;
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 40);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(40, 13);
			labelCode.TabIndex = 22;
			labelCode.Text = "Code:";
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(543, 1);
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
			xpButton1.Location = new System.Drawing.Point(435, 8);
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
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 2;
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
			buttonSave.TabIndex = 1;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(543, 408);
			base.Controls.Add(ultraGroupBox7);
			base.Controls.Add(ultraFormattedLinkLabel5);
			base.Controls.Add(textBoxAPName);
			base.Controls.Add(comboBoxAPAccount);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(mmLabel11);
			base.Controls.Add(textboxkm2);
			base.Controls.Add(textBoxreminderdays);
			base.Controls.Add(textboxkm1);
			base.Controls.Add(textBoxRepeatDays);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(comboServiceType);
			base.Controls.Add(comboVehicleType);
			base.Controls.Add(textboxNote);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(textBoxVehicleType);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxDescription);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ServiceItemForm";
			Text = "Service Item";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(ServiceItemForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox7).EndInit();
			ultraGroupBox7.ResumeLayout(false);
			ultraGroupBox7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAPAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboVehicleType).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
