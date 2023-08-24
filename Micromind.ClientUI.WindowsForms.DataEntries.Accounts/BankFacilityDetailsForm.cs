using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
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
	public class BankFacilityDetailsForm : Form, IForm
	{
		private BankFacilityData currentData;

		private const string TABLENAME_CONST = "Bank_Facility";

		private const string IDFIELD_CONST = "FacilityID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

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

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private UltraGroupBox ultraGroupBox2;

		private MMTextBox textBoxCurrentAccount;

		private UltraFormattedLinkLabel linkLabelCurrentAccount;

		private AllAccountsComboBox comboBoxCurrentAccount;

		private MMTextBox textBoxARName;

		private UltraFormattedLinkLabel linkLabelARAccount;

		private AllAccountsComboBox comboBoxPayableAccount;

		private UltraFormattedLinkLabel linkLabelFacilityGroup;

		private MMLabel mmLabel5;

		private MMSDateTimePicker dateTimeStartDate;

		private MMLabel mmLabel6;

		private MMLabel mmLabel10;

		private MMSDateTimePicker dateTimeEndDate;

		private MMLabel mmLabel7;

		private BankFacilityGroupComboBox comboBoxBankFacilityGroup;

		private BankFacilityTypesComboBox comboBoxBankFacilityType;

		private MMTextBox textBoxFacilityGroup;

		private BankFacilityStatusComboBox comboBoxStatus;

		private AmountTextBox textBoxAmountLimit;

		private MMLabel mmLabel2;

		private Panel panleEndDate;

		private MMTextBox textBoxBankInterestName;

		private MMTextBox textBoxBankChargeName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private AllAccountsComboBox comboBoxBankInterestAccount;

		private AllAccountsComboBox comboBoxBankChargeAccount;

		private MMTextBox textBoxTemplateName;

		private MMLabel mmLabel3;

		private MMLabel mmLabel8;

		private XPButton buttonSelectTemplatePath;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private MMLabel mmLabel13;

		private MMTextBox textBoxAlias;

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

		public BankFacilityDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += BankFacilityDetailsForm_Load;
			comboBoxStatus.SelectedIndexChanged += comboBoxStatus_SelectedIndexChanged;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new BankFacilityData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.BankFacilityTable.Rows[0] : currentData.BankFacilityTable.NewRow();
				dataRow.BeginEdit();
				dataRow["FacilityID"] = textBoxCode.Text.Trim();
				dataRow["FacilityName"] = textBoxName.Text.Trim();
				dataRow["PrintTemplateName"] = textBoxTemplateName.Text.Trim();
				if (dateTimeStartDate.Checked)
				{
					dataRow["StartDate"] = dateTimeStartDate.Value;
				}
				else
				{
					dataRow["StartDate"] = DBNull.Value;
				}
				if (dateTimeEndDate.Checked && comboBoxStatus.SelectedID == 2)
				{
					dataRow["EndDate"] = dateTimeEndDate.Value;
				}
				else
				{
					dataRow["EndDate"] = DBNull.Value;
				}
				if (comboBoxStatus.SelectedID != -1)
				{
					dataRow["Status"] = comboBoxStatus.SelectedID;
				}
				else
				{
					dataRow["Status"] = DBNull.Value;
				}
				if (comboBoxBankFacilityGroup.SelectedID != "")
				{
					dataRow["GroupID"] = comboBoxBankFacilityGroup.SelectedID;
				}
				else
				{
					dataRow["GroupID"] = DBNull.Value;
				}
				if (comboBoxBankFacilityType.SelectedID != -1)
				{
					dataRow["FacilityType"] = comboBoxBankFacilityType.SelectedID;
				}
				else
				{
					dataRow["FacilityType"] = DBNull.Value;
				}
				if (comboBoxPayableAccount.SelectedID != "")
				{
					dataRow["PayableAccountID"] = comboBoxPayableAccount.SelectedID;
				}
				else
				{
					dataRow["PayableAccountID"] = DBNull.Value;
				}
				if (comboBoxCurrentAccount.SelectedID != "")
				{
					dataRow["CurrentAccountID"] = comboBoxCurrentAccount.SelectedID;
				}
				else
				{
					dataRow["CurrentAccountID"] = DBNull.Value;
				}
				dataRow["Alias"] = textBoxAlias.Text;
				dataRow["Note"] = textBoxNote.Text;
				if (comboBoxBankChargeAccount.SelectedID != "")
				{
					dataRow["BankChargeAccountID"] = comboBoxBankChargeAccount.SelectedID;
				}
				else
				{
					dataRow["BankChargeAccountID"] = DBNull.Value;
				}
				if (comboBoxBankInterestAccount.SelectedID != "")
				{
					dataRow["BankInterestAccountID"] = comboBoxBankInterestAccount.SelectedID;
				}
				else
				{
					dataRow["BankInterestAccountID"] = DBNull.Value;
				}
				dataRow["Note"] = textBoxNote.Text;
				if (textBoxAmountLimit.Text.Trim() != "")
				{
					dataRow["LimitAmount"] = decimal.Parse(textBoxAmountLimit.Text.Trim());
				}
				else
				{
					dataRow["LimitAmount"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.BankFacilityTable.Rows.Add(dataRow);
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
					currentData = Factory.BankFacilitySystem.GetBankFacilityByID(id.Trim());
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
						if (comboBoxStatus.SelectedID == 2)
						{
							panleEndDate.Visible = true;
						}
						else
						{
							panleEndDate.Visible = false;
						}
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
				textBoxCode.Text = dataRow["FacilityID"].ToString();
				textBoxName.Text = dataRow["FacilityName"].ToString();
				textBoxAmountLimit.Text = dataRow["LimitAmount"].ToString();
				textBoxTemplateName.Text = dataRow["PrintTemplateName"].ToString();
				if (dataRow["StartDate"] != DBNull.Value)
				{
					dateTimeStartDate.Value = DateTime.Parse(dataRow["StartDate"].ToString());
					dateTimeStartDate.Checked = true;
				}
				else
				{
					dateTimeStartDate.IsNull = true;
					dateTimeStartDate.Checked = false;
				}
				if (dataRow["EndDate"] != DBNull.Value)
				{
					dateTimeEndDate.Value = DateTime.Parse(dataRow["EndDate"].ToString());
					dateTimeEndDate.Checked = true;
				}
				else
				{
					dateTimeEndDate.IsNull = true;
					dateTimeEndDate.Checked = false;
				}
				if (dataRow["Status"] != DBNull.Value)
				{
					comboBoxStatus.SelectedID = int.Parse(dataRow["Status"].ToString());
				}
				else
				{
					comboBoxStatus.Clear();
				}
				if (dataRow["GroupID"] != DBNull.Value)
				{
					comboBoxBankFacilityGroup.SelectedID = dataRow["GroupID"].ToString();
				}
				else
				{
					comboBoxBankFacilityGroup.Clear();
				}
				if (dataRow["FacilityType"] != DBNull.Value)
				{
					comboBoxBankFacilityType.SelectedID = int.Parse(dataRow["FacilityType"].ToString());
				}
				else
				{
					comboBoxBankFacilityType.Clear();
				}
				if (dataRow["PayableAccountID"] != DBNull.Value)
				{
					comboBoxPayableAccount.SelectedID = dataRow["PayableAccountID"].ToString();
				}
				else
				{
					comboBoxPayableAccount.Clear();
				}
				if (dataRow["CurrentAccountID"] != DBNull.Value)
				{
					comboBoxCurrentAccount.SelectedID = dataRow["CurrentAccountID"].ToString();
				}
				else
				{
					comboBoxCurrentAccount.Clear();
				}
				if (dataRow["BankChargeAccountID"] != DBNull.Value)
				{
					comboBoxBankChargeAccount.SelectedID = dataRow["BankChargeAccountID"].ToString();
				}
				else
				{
					comboBoxBankChargeAccount.Clear();
				}
				if (dataRow["BankInterestAccountID"] != DBNull.Value)
				{
					comboBoxBankInterestAccount.SelectedID = dataRow["BankInterestAccountID"].ToString();
				}
				else
				{
					comboBoxBankInterestAccount.Clear();
				}
				textBoxNote.Text = dataRow["Note"].ToString();
				textBoxAlias.Text = dataRow["Alias"].ToString();
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
					flag = Factory.BankFacilitySystem.CreateBankFacility(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.BankFacility, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.BankFacilitySystem.UpdateBankFacility(currentData);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Bank_Facility", "FacilityID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0 || comboBoxBankFacilityGroup.SelectedID == "" || comboBoxBankFacilityType.SelectedID == -1)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Bank_Facility", "FacilityID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Bank_Facility", "FacilityID");
			textBoxName.Clear();
			textBoxNote.Clear();
			textBoxFacilityGroup.Clear();
			textBoxAmountLimit.Text = 0.ToString(Format.TotalAmountFormat);
			dateTimeStartDate.Clear();
			dateTimeEndDate.Clear();
			comboBoxStatus.LoadData();
			comboBoxStatus.SelectedID = 0;
			comboBoxBankFacilityType.LoadData();
			comboBoxBankFacilityGroup.Clear();
			comboBoxPayableAccount.Clear();
			comboBoxCurrentAccount.Clear();
			comboBoxBankChargeAccount.Clear();
			comboBoxBankInterestAccount.Clear();
			textBoxTemplateName.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
			textBoxAlias.Clear();
		}

		private void BankFacilityGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void BankFacilityGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.BankFacilitySystem.DeleteBankFacility(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Bank_Facility", "FacilityID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Bank_Facility", "FacilityID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Bank_Facility", "FacilityID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Bank_Facility", "FacilityID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Bank_Facility", "FacilityID", toolStripTextBoxFind.Text.Trim()))
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

		private void BankFacilityDetailsForm_Load(object sender, EventArgs e)
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
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditCard))
			{
				AllowEditCard = false;
			}
			else
			{
				AllowEditCard = true;
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.BankFacility);
		}

		private void linkLabelARAccount_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxPayableAccount.SelectedID);
		}

		private void linkLabelCurrentAccount_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxCurrentAccount.SelectedID);
		}

		private void linkLabelFacilityGroup_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBankFacilityGroup(comboBoxBankFacilityGroup.SelectedID);
		}

		private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxStatus.SelectedID == 2)
			{
				panleEndDate.Visible = true;
			}
			else
			{
				panleEndDate.Visible = false;
			}
		}

		private void buttonSelectTemplatePath_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			string printTemplatePath = PrintHelper.PrintTemplatePath;
			openFileDialog.InitialDirectory = printTemplatePath + "\\Documents";
			openFileDialog.DefaultExt = "*.repx";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxTemplateName.Text = openFileDialog.SafeFileName.Replace(".repx", "");
			}
		}

		private void comboBoxBankFacilityType_SelectedValueChanged(object sender, EventArgs e)
		{
			if (comboBoxBankFacilityType.Text == "TR")
			{
				textBoxTemplateName.Enabled = true;
				buttonSelectTemplatePath.Enabled = true;
			}
			else
			{
				textBoxTemplateName.Enabled = false;
				buttonSelectTemplatePath.Enabled = false;
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text;
					docManagementForm.EntityName = textBoxName.Text;
					docManagementForm.EntityType = EntityTypesEnum.Acccounts;
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
			components = new System.ComponentModel.Container();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.BankFacilityDetailsForm));
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
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			textBoxBankInterestName = new Micromind.UISupport.MMTextBox();
			textBoxBankChargeName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxBankInterestAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxBankChargeAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxCurrentAccount = new Micromind.UISupport.MMTextBox();
			linkLabelCurrentAccount = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCurrentAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxARName = new Micromind.UISupport.MMTextBox();
			linkLabelARAccount = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPayableAccount = new Micromind.DataControls.AllAccountsComboBox();
			linkLabelFacilityGroup = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxStatus = new Micromind.DataControls.BankFacilityStatusComboBox();
			textBoxFacilityGroup = new Micromind.UISupport.MMTextBox();
			comboBoxBankFacilityType = new Micromind.DataControls.BankFacilityTypesComboBox();
			comboBoxBankFacilityGroup = new Micromind.DataControls.BankFacilityGroupComboBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			dateTimeEndDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel7 = new Micromind.UISupport.MMLabel();
			dateTimeStartDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			textBoxAmountLimit = new Micromind.UISupport.AmountTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			panleEndDate = new System.Windows.Forms.Panel();
			textBoxTemplateName = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			buttonSelectTemplatePath = new Micromind.UISupport.XPButton();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			textBoxAlias = new Micromind.UISupport.MMTextBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBankInterestAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBankChargeAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrentAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayableAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBankFacilityGroup).BeginInit();
			panleEndDate.SuspendLayout();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
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
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(632, 31);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 400);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(632, 40);
			panelButtons.TabIndex = 13;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(632, 1);
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
			buttonDelete.TabIndex = 16;
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
			xpButton1.Location = new System.Drawing.Point(522, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 17;
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
			buttonNew.TabIndex = 15;
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
			buttonSave.TabIndex = 14;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox2.Controls.Add(textBoxBankInterestName);
			ultraGroupBox2.Controls.Add(textBoxBankChargeName);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel2);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel1);
			ultraGroupBox2.Controls.Add(comboBoxBankInterestAccount);
			ultraGroupBox2.Controls.Add(comboBoxBankChargeAccount);
			ultraGroupBox2.Controls.Add(textBoxCurrentAccount);
			ultraGroupBox2.Controls.Add(linkLabelCurrentAccount);
			ultraGroupBox2.Controls.Add(comboBoxCurrentAccount);
			ultraGroupBox2.Controls.Add(textBoxARName);
			ultraGroupBox2.Controls.Add(linkLabelARAccount);
			ultraGroupBox2.Controls.Add(comboBoxPayableAccount);
			ultraGroupBox2.Location = new System.Drawing.Point(9, 240);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(621, 138);
			ultraGroupBox2.TabIndex = 12;
			ultraGroupBox2.Text = "Accounts";
			textBoxBankInterestName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankInterestName.CustomReportFieldName = "";
			textBoxBankInterestName.CustomReportKey = "";
			textBoxBankInterestName.CustomReportValueType = 1;
			textBoxBankInterestName.IsComboTextBox = false;
			textBoxBankInterestName.IsModified = false;
			textBoxBankInterestName.Location = new System.Drawing.Point(285, 91);
			textBoxBankInterestName.MaxLength = 30;
			textBoxBankInterestName.Name = "textBoxBankInterestName";
			textBoxBankInterestName.ReadOnly = true;
			textBoxBankInterestName.Size = new System.Drawing.Size(324, 21);
			textBoxBankInterestName.TabIndex = 7;
			textBoxBankInterestName.TabStop = false;
			textBoxBankChargeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankChargeName.CustomReportFieldName = "";
			textBoxBankChargeName.CustomReportKey = "";
			textBoxBankChargeName.CustomReportValueType = 1;
			textBoxBankChargeName.IsComboTextBox = false;
			textBoxBankChargeName.IsModified = false;
			textBoxBankChargeName.Location = new System.Drawing.Point(285, 67);
			textBoxBankChargeName.MaxLength = 30;
			textBoxBankChargeName.Name = "textBoxBankChargeName";
			textBoxBankChargeName.ReadOnly = true;
			textBoxBankChargeName.Size = new System.Drawing.Size(324, 21);
			textBoxBankChargeName.TabIndex = 5;
			textBoxBankChargeName.TabStop = false;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(0, 92);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(74, 15);
			ultraFormattedLinkLabel2.TabIndex = 14;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Bank Interest:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(0, 68);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabel1.TabIndex = 14;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Bank Charges:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxBankInterestAccount.Assigned = false;
			comboBoxBankInterestAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBankInterestAccount.CustomReportFieldName = "";
			comboBoxBankInterestAccount.CustomReportKey = "";
			comboBoxBankInterestAccount.CustomReportValueType = 1;
			comboBoxBankInterestAccount.DescriptionTextBox = textBoxBankInterestName;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBankInterestAccount.DisplayLayout.Appearance = appearance3;
			comboBoxBankInterestAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBankInterestAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBankInterestAccount.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBankInterestAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxBankInterestAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBankInterestAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxBankInterestAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBankInterestAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBankInterestAccount.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBankInterestAccount.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxBankInterestAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBankInterestAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBankInterestAccount.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBankInterestAccount.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxBankInterestAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBankInterestAccount.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBankInterestAccount.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxBankInterestAccount.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxBankInterestAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBankInterestAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxBankInterestAccount.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxBankInterestAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBankInterestAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			comboBoxBankInterestAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBankInterestAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBankInterestAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBankInterestAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBankInterestAccount.Editable = true;
			comboBoxBankInterestAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxBankInterestAccount.FilterString = "";
			comboBoxBankInterestAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxBankInterestAccount.FilterSysDocID = "";
			comboBoxBankInterestAccount.HasAllAccount = false;
			comboBoxBankInterestAccount.HasCustom = false;
			comboBoxBankInterestAccount.IsDataLoaded = false;
			comboBoxBankInterestAccount.Location = new System.Drawing.Point(99, 91);
			comboBoxBankInterestAccount.MaxDropDownItems = 12;
			comboBoxBankInterestAccount.MaxLength = 64;
			comboBoxBankInterestAccount.Name = "comboBoxBankInterestAccount";
			comboBoxBankInterestAccount.ShowInactiveItems = false;
			comboBoxBankInterestAccount.ShowQuickAdd = true;
			comboBoxBankInterestAccount.Size = new System.Drawing.Size(180, 21);
			comboBoxBankInterestAccount.TabIndex = 6;
			comboBoxBankInterestAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBankChargeAccount.Assigned = false;
			comboBoxBankChargeAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBankChargeAccount.CustomReportFieldName = "";
			comboBoxBankChargeAccount.CustomReportKey = "";
			comboBoxBankChargeAccount.CustomReportValueType = 1;
			comboBoxBankChargeAccount.DescriptionTextBox = textBoxBankChargeName;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBankChargeAccount.DisplayLayout.Appearance = appearance15;
			comboBoxBankChargeAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBankChargeAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBankChargeAccount.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBankChargeAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxBankChargeAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBankChargeAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxBankChargeAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBankChargeAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBankChargeAccount.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBankChargeAccount.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxBankChargeAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBankChargeAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBankChargeAccount.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBankChargeAccount.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxBankChargeAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBankChargeAccount.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBankChargeAccount.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxBankChargeAccount.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxBankChargeAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBankChargeAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxBankChargeAccount.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxBankChargeAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBankChargeAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			comboBoxBankChargeAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBankChargeAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBankChargeAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBankChargeAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBankChargeAccount.Editable = true;
			comboBoxBankChargeAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxBankChargeAccount.FilterString = "";
			comboBoxBankChargeAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxBankChargeAccount.FilterSysDocID = "";
			comboBoxBankChargeAccount.HasAllAccount = false;
			comboBoxBankChargeAccount.HasCustom = false;
			comboBoxBankChargeAccount.IsDataLoaded = false;
			comboBoxBankChargeAccount.Location = new System.Drawing.Point(99, 67);
			comboBoxBankChargeAccount.MaxDropDownItems = 12;
			comboBoxBankChargeAccount.MaxLength = 64;
			comboBoxBankChargeAccount.Name = "comboBoxBankChargeAccount";
			comboBoxBankChargeAccount.ShowInactiveItems = false;
			comboBoxBankChargeAccount.ShowQuickAdd = true;
			comboBoxBankChargeAccount.Size = new System.Drawing.Size(180, 21);
			comboBoxBankChargeAccount.TabIndex = 4;
			comboBoxBankChargeAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxCurrentAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCurrentAccount.CustomReportFieldName = "";
			textBoxCurrentAccount.CustomReportKey = "";
			textBoxCurrentAccount.CustomReportValueType = 1;
			textBoxCurrentAccount.IsComboTextBox = false;
			textBoxCurrentAccount.IsModified = false;
			textBoxCurrentAccount.Location = new System.Drawing.Point(285, 43);
			textBoxCurrentAccount.MaxLength = 30;
			textBoxCurrentAccount.Name = "textBoxCurrentAccount";
			textBoxCurrentAccount.ReadOnly = true;
			textBoxCurrentAccount.Size = new System.Drawing.Size(324, 21);
			textBoxCurrentAccount.TabIndex = 3;
			textBoxCurrentAccount.TabStop = false;
			linkLabelCurrentAccount.AutoSize = true;
			linkLabelCurrentAccount.Location = new System.Drawing.Point(0, 44);
			linkLabelCurrentAccount.Name = "linkLabelCurrentAccount";
			linkLabelCurrentAccount.Size = new System.Drawing.Size(87, 15);
			linkLabelCurrentAccount.TabIndex = 7;
			linkLabelCurrentAccount.TabStop = true;
			linkLabelCurrentAccount.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCurrentAccount.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCurrentAccount.Value = "Current Account:";
			appearance27.ForeColor = System.Drawing.Color.Blue;
			linkLabelCurrentAccount.VisitedLinkAppearance = appearance27;
			linkLabelCurrentAccount.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCurrentAccount_LinkClicked);
			comboBoxCurrentAccount.Assigned = false;
			comboBoxCurrentAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCurrentAccount.CustomReportFieldName = "";
			comboBoxCurrentAccount.CustomReportKey = "";
			comboBoxCurrentAccount.CustomReportValueType = 1;
			comboBoxCurrentAccount.DescriptionTextBox = textBoxCurrentAccount;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			appearance28.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCurrentAccount.DisplayLayout.Appearance = appearance28;
			comboBoxCurrentAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCurrentAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance29.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance29.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrentAccount.DisplayLayout.GroupByBox.Appearance = appearance29;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrentAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance30;
			comboBoxCurrentAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance31.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance31.BackColor2 = System.Drawing.SystemColors.Control;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCurrentAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance31;
			comboBoxCurrentAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCurrentAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCurrentAccount.DisplayLayout.Override.ActiveCellAppearance = appearance32;
			appearance33.BackColor = System.Drawing.SystemColors.Highlight;
			appearance33.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCurrentAccount.DisplayLayout.Override.ActiveRowAppearance = appearance33;
			comboBoxCurrentAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCurrentAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCurrentAccount.DisplayLayout.Override.CardAreaAppearance = appearance34;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			appearance35.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCurrentAccount.DisplayLayout.Override.CellAppearance = appearance35;
			comboBoxCurrentAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCurrentAccount.DisplayLayout.Override.CellPadding = 0;
			appearance36.BackColor = System.Drawing.SystemColors.Control;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCurrentAccount.DisplayLayout.Override.GroupByRowAppearance = appearance36;
			appearance37.TextHAlignAsString = "Left";
			comboBoxCurrentAccount.DisplayLayout.Override.HeaderAppearance = appearance37;
			comboBoxCurrentAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCurrentAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			comboBoxCurrentAccount.DisplayLayout.Override.RowAppearance = appearance38;
			comboBoxCurrentAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance39.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCurrentAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance39;
			comboBoxCurrentAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCurrentAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCurrentAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCurrentAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCurrentAccount.Editable = true;
			comboBoxCurrentAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxCurrentAccount.FilterString = "";
			comboBoxCurrentAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxCurrentAccount.FilterSysDocID = "";
			comboBoxCurrentAccount.HasAllAccount = false;
			comboBoxCurrentAccount.HasCustom = false;
			comboBoxCurrentAccount.IsDataLoaded = false;
			comboBoxCurrentAccount.Location = new System.Drawing.Point(99, 43);
			comboBoxCurrentAccount.MaxDropDownItems = 12;
			comboBoxCurrentAccount.MaxLength = 64;
			comboBoxCurrentAccount.Name = "comboBoxCurrentAccount";
			comboBoxCurrentAccount.ShowInactiveItems = false;
			comboBoxCurrentAccount.ShowQuickAdd = true;
			comboBoxCurrentAccount.Size = new System.Drawing.Size(180, 21);
			comboBoxCurrentAccount.TabIndex = 2;
			comboBoxCurrentAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxARName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxARName.CustomReportFieldName = "";
			textBoxARName.CustomReportKey = "";
			textBoxARName.CustomReportValueType = 1;
			textBoxARName.IsComboTextBox = false;
			textBoxARName.IsModified = false;
			textBoxARName.Location = new System.Drawing.Point(285, 20);
			textBoxARName.MaxLength = 30;
			textBoxARName.Name = "textBoxARName";
			textBoxARName.ReadOnly = true;
			textBoxARName.Size = new System.Drawing.Size(324, 21);
			textBoxARName.TabIndex = 1;
			textBoxARName.TabStop = false;
			linkLabelARAccount.AutoSize = true;
			linkLabelARAccount.Location = new System.Drawing.Point(0, 23);
			linkLabelARAccount.Name = "linkLabelARAccount";
			linkLabelARAccount.Size = new System.Drawing.Size(90, 15);
			linkLabelARAccount.TabIndex = 0;
			linkLabelARAccount.TabStop = true;
			linkLabelARAccount.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelARAccount.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelARAccount.Value = "Payable Account:";
			appearance40.ForeColor = System.Drawing.Color.Blue;
			linkLabelARAccount.VisitedLinkAppearance = appearance40;
			linkLabelARAccount.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelARAccount_LinkClicked);
			comboBoxPayableAccount.Assigned = false;
			comboBoxPayableAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayableAccount.CustomReportFieldName = "";
			comboBoxPayableAccount.CustomReportKey = "";
			comboBoxPayableAccount.CustomReportValueType = 1;
			comboBoxPayableAccount.DescriptionTextBox = textBoxARName;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayableAccount.DisplayLayout.Appearance = appearance41;
			comboBoxPayableAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayableAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayableAccount.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayableAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxPayableAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayableAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxPayableAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayableAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayableAccount.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayableAccount.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxPayableAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayableAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayableAccount.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayableAccount.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxPayableAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayableAccount.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayableAccount.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxPayableAccount.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxPayableAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayableAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayableAccount.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxPayableAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayableAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxPayableAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayableAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayableAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayableAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayableAccount.Editable = true;
			comboBoxPayableAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxPayableAccount.FilterString = "";
			comboBoxPayableAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxPayableAccount.FilterSysDocID = "";
			comboBoxPayableAccount.HasAllAccount = false;
			comboBoxPayableAccount.HasCustom = false;
			comboBoxPayableAccount.IsDataLoaded = false;
			comboBoxPayableAccount.Location = new System.Drawing.Point(99, 20);
			comboBoxPayableAccount.MaxDropDownItems = 12;
			comboBoxPayableAccount.MaxLength = 64;
			comboBoxPayableAccount.Name = "comboBoxPayableAccount";
			comboBoxPayableAccount.ShowInactiveItems = false;
			comboBoxPayableAccount.ShowQuickAdd = true;
			comboBoxPayableAccount.Size = new System.Drawing.Size(180, 21);
			comboBoxPayableAccount.TabIndex = 0;
			comboBoxPayableAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelFacilityGroup.AutoSize = true;
			linkLabelFacilityGroup.Location = new System.Drawing.Point(8, 114);
			linkLabelFacilityGroup.Name = "linkLabelFacilityGroup";
			linkLabelFacilityGroup.Size = new System.Drawing.Size(75, 15);
			linkLabelFacilityGroup.TabIndex = 55;
			linkLabelFacilityGroup.TabStop = true;
			linkLabelFacilityGroup.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelFacilityGroup.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelFacilityGroup.Value = "Facility Group:";
			appearance53.ForeColor = System.Drawing.Color.Blue;
			linkLabelFacilityGroup.VisitedLinkAppearance = appearance53;
			linkLabelFacilityGroup.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelFacilityGroup_LinkClicked);
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Location = new System.Drawing.Point(336, 35);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(95, 21);
			comboBoxStatus.TabIndex = 1;
			textBoxFacilityGroup.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFacilityGroup.CustomReportFieldName = "";
			textBoxFacilityGroup.CustomReportKey = "";
			textBoxFacilityGroup.CustomReportValueType = 1;
			textBoxFacilityGroup.IsComboTextBox = false;
			textBoxFacilityGroup.IsModified = false;
			textBoxFacilityGroup.Location = new System.Drawing.Point(254, 111);
			textBoxFacilityGroup.MaxLength = 30;
			textBoxFacilityGroup.Name = "textBoxFacilityGroup";
			textBoxFacilityGroup.ReadOnly = true;
			textBoxFacilityGroup.Size = new System.Drawing.Size(364, 21);
			textBoxFacilityGroup.TabIndex = 6;
			textBoxFacilityGroup.TabStop = false;
			comboBoxBankFacilityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxBankFacilityType.FormattingEnabled = true;
			comboBoxBankFacilityType.Location = new System.Drawing.Point(108, 136);
			comboBoxBankFacilityType.MaxLength = 2;
			comboBoxBankFacilityType.Name = "comboBoxBankFacilityType";
			comboBoxBankFacilityType.Size = new System.Drawing.Size(144, 21);
			comboBoxBankFacilityType.TabIndex = 7;
			comboBoxBankFacilityType.SelectedValueChanged += new System.EventHandler(comboBoxBankFacilityType_SelectedValueChanged);
			comboBoxBankFacilityGroup.Assigned = false;
			comboBoxBankFacilityGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBankFacilityGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBankFacilityGroup.CustomReportFieldName = "";
			comboBoxBankFacilityGroup.CustomReportKey = "";
			comboBoxBankFacilityGroup.CustomReportValueType = 1;
			comboBoxBankFacilityGroup.DescriptionTextBox = textBoxFacilityGroup;
			appearance54.BackColor = System.Drawing.SystemColors.Window;
			appearance54.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBankFacilityGroup.DisplayLayout.Appearance = appearance54;
			comboBoxBankFacilityGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBankFacilityGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance55.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance55.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance55.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBankFacilityGroup.DisplayLayout.GroupByBox.Appearance = appearance55;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBankFacilityGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance56;
			comboBoxBankFacilityGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance57.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance57.BackColor2 = System.Drawing.SystemColors.Control;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBankFacilityGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance57;
			comboBoxBankFacilityGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBankFacilityGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			appearance58.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBankFacilityGroup.DisplayLayout.Override.ActiveCellAppearance = appearance58;
			appearance59.BackColor = System.Drawing.SystemColors.Highlight;
			appearance59.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBankFacilityGroup.DisplayLayout.Override.ActiveRowAppearance = appearance59;
			comboBoxBankFacilityGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBankFacilityGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance60.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBankFacilityGroup.DisplayLayout.Override.CardAreaAppearance = appearance60;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			appearance61.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBankFacilityGroup.DisplayLayout.Override.CellAppearance = appearance61;
			comboBoxBankFacilityGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBankFacilityGroup.DisplayLayout.Override.CellPadding = 0;
			appearance62.BackColor = System.Drawing.SystemColors.Control;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBankFacilityGroup.DisplayLayout.Override.GroupByRowAppearance = appearance62;
			appearance63.TextHAlignAsString = "Left";
			comboBoxBankFacilityGroup.DisplayLayout.Override.HeaderAppearance = appearance63;
			comboBoxBankFacilityGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBankFacilityGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			appearance64.BorderColor = System.Drawing.Color.Silver;
			comboBoxBankFacilityGroup.DisplayLayout.Override.RowAppearance = appearance64;
			comboBoxBankFacilityGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance65.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBankFacilityGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance65;
			comboBoxBankFacilityGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBankFacilityGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBankFacilityGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBankFacilityGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBankFacilityGroup.Editable = true;
			comboBoxBankFacilityGroup.FilterString = "";
			comboBoxBankFacilityGroup.HasAllAccount = false;
			comboBoxBankFacilityGroup.HasCustom = false;
			comboBoxBankFacilityGroup.IsDataLoaded = false;
			comboBoxBankFacilityGroup.Location = new System.Drawing.Point(108, 111);
			comboBoxBankFacilityGroup.MaxDropDownItems = 12;
			comboBoxBankFacilityGroup.MaxLength = 15;
			comboBoxBankFacilityGroup.Name = "comboBoxBankFacilityGroup";
			comboBoxBankFacilityGroup.ShowInactiveItems = false;
			comboBoxBankFacilityGroup.ShowQuickAdd = true;
			comboBoxBankFacilityGroup.Size = new System.Drawing.Size(144, 21);
			comboBoxBankFacilityGroup.TabIndex = 5;
			comboBoxBankFacilityGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.Transparent;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel10.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(287, 38);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(42, 13);
			mmLabel10.TabIndex = 61;
			mmLabel10.Text = "Status:";
			dateTimeEndDate.Checked = false;
			dateTimeEndDate.CustomFormat = " ";
			dateTimeEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeEndDate.Location = new System.Drawing.Point(57, 7);
			dateTimeEndDate.Name = "dateTimeEndDate";
			dateTimeEndDate.ShowCheckBox = true;
			dateTimeEndDate.Size = new System.Drawing.Size(127, 21);
			dateTimeEndDate.TabIndex = 2;
			dateTimeEndDate.Value = new System.DateTime(0L);
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(3, 10);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(55, 13);
			mmLabel7.TabIndex = 60;
			mmLabel7.Text = "End Date:";
			dateTimeStartDate.Checked = false;
			dateTimeStartDate.CustomFormat = " ";
			dateTimeStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeStartDate.Location = new System.Drawing.Point(323, 136);
			dateTimeStartDate.Name = "dateTimeStartDate";
			dateTimeStartDate.ShowCheckBox = true;
			dateTimeStartDate.Size = new System.Drawing.Size(144, 21);
			dateTimeStartDate.TabIndex = 8;
			dateTimeStartDate.Value = new System.DateTime(0L);
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(258, 140);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(61, 13);
			mmLabel6.TabIndex = 58;
			mmLabel6.Text = "Start Date:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = true;
			mmLabel5.Location = new System.Drawing.Point(8, 140);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(83, 13);
			mmLabel5.TabIndex = 56;
			mmLabel5.Text = "Facility Type:";
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
			textBoxNote.Location = new System.Drawing.Point(108, 211);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(510, 21);
			textBoxNote.TabIndex = 11;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(108, 61);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(510, 21);
			textBoxName.TabIndex = 3;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(108, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(173, 21);
			textBoxCode.TabIndex = 0;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(8, 215);
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
			mmLabel1.Location = new System.Drawing.Point(8, 65);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(87, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Facility Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 40);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(84, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Facility Code:";
			textBoxAmountLimit.AllowDecimal = true;
			textBoxAmountLimit.CustomReportFieldName = "";
			textBoxAmountLimit.CustomReportKey = "";
			textBoxAmountLimit.CustomReportValueType = 1;
			textBoxAmountLimit.IsComboTextBox = false;
			textBoxAmountLimit.IsModified = false;
			textBoxAmountLimit.Location = new System.Drawing.Point(108, 161);
			textBoxAmountLimit.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAmountLimit.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAmountLimit.Name = "textBoxAmountLimit";
			textBoxAmountLimit.NullText = "0";
			textBoxAmountLimit.Size = new System.Drawing.Size(144, 21);
			textBoxAmountLimit.TabIndex = 9;
			textBoxAmountLimit.Text = "0.00";
			textBoxAmountLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmountLimit.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(8, 165);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(70, 13);
			mmLabel2.TabIndex = 1001;
			mmLabel2.Text = "Amount Limit:";
			panleEndDate.Controls.Add(dateTimeEndDate);
			panleEndDate.Controls.Add(mmLabel7);
			panleEndDate.Location = new System.Drawing.Point(437, 28);
			panleEndDate.Name = "panleEndDate";
			panleEndDate.Size = new System.Drawing.Size(185, 29);
			panleEndDate.TabIndex = 2;
			panleEndDate.Visible = false;
			textBoxTemplateName.BackColor = System.Drawing.Color.White;
			textBoxTemplateName.CustomReportFieldName = "";
			textBoxTemplateName.CustomReportKey = "";
			textBoxTemplateName.CustomReportValueType = 1;
			textBoxTemplateName.IsComboTextBox = false;
			textBoxTemplateName.IsModified = false;
			textBoxTemplateName.Location = new System.Drawing.Point(108, 186);
			textBoxTemplateName.MaxLength = 64;
			textBoxTemplateName.Name = "textBoxTemplateName";
			textBoxTemplateName.Size = new System.Drawing.Size(217, 21);
			textBoxTemplateName.TabIndex = 10;
			textBoxTemplateName.TabStop = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(325, 187);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(30, 13);
			mmLabel3.TabIndex = 1003;
			mmLabel3.Text = ".repx";
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(4, 190);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(85, 13);
			mmLabel8.TabIndex = 1005;
			mmLabel8.Text = "Template Name:";
			buttonSelectTemplatePath.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectTemplatePath.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectTemplatePath.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectTemplatePath.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectTemplatePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectTemplatePath.Location = new System.Drawing.Point(359, 186);
			buttonSelectTemplatePath.Name = "buttonSelectTemplatePath";
			buttonSelectTemplatePath.Size = new System.Drawing.Size(25, 21);
			buttonSelectTemplatePath.TabIndex = 1004;
			buttonSelectTemplatePath.Text = "...";
			buttonSelectTemplatePath.UseVisualStyleBackColor = false;
			buttonSelectTemplatePath.Click += new System.EventHandler(buttonSelectTemplatePath_Click);
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(8, 90);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(32, 13);
			mmLabel13.TabIndex = 1007;
			mmLabel13.Text = "Alias:";
			textBoxAlias.BackColor = System.Drawing.Color.White;
			textBoxAlias.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAlias.CustomReportFieldName = "";
			textBoxAlias.CustomReportKey = "";
			textBoxAlias.CustomReportValueType = 1;
			textBoxAlias.IsComboTextBox = false;
			textBoxAlias.IsModified = false;
			textBoxAlias.Location = new System.Drawing.Point(108, 86);
			textBoxAlias.MaxLength = 64;
			textBoxAlias.Name = "textBoxAlias";
			textBoxAlias.Size = new System.Drawing.Size(510, 21);
			textBoxAlias.TabIndex = 4;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(632, 440);
			base.Controls.Add(mmLabel13);
			base.Controls.Add(textBoxAlias);
			base.Controls.Add(textBoxTemplateName);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(buttonSelectTemplatePath);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(panleEndDate);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(textBoxAmountLimit);
			base.Controls.Add(comboBoxStatus);
			base.Controls.Add(textBoxFacilityGroup);
			base.Controls.Add(comboBoxBankFacilityType);
			base.Controls.Add(comboBoxBankFacilityGroup);
			base.Controls.Add(dateTimeStartDate);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(linkLabelFacilityGroup);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "BankFacilityDetailsForm";
			Text = "Bank Facility";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBankInterestAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBankChargeAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCurrentAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayableAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBankFacilityGroup).EndInit();
			panleEndDate.ResumeLayout(false);
			panleEndDate.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
