using DevExpress.XtraEditors.Controls;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Legal
{
	public class LegalActivityDetailsForm : Form, IForm
	{
		private LegalActivityData currentData;

		private const string TABLENAME_CONST = "Legal_Activity";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool EnableLegalAnalysis = CompanyPreferences.EnableLegalAnalysisCode;

		private DataSet companyInformation;

		private ScreenAccessRight screenRight;

		private DataSet followUpData;

		private string parentSysDocID = "";

		private string parentVoucherID = "";

		private IContainer components;

		private ToolStrip toolStrip1;

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

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonInformation;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl ultraTabPageControl1;

		private MMTextBox textBoxCaseParty;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private SysDocComboBox comboBoxSysDoc;

		private DateTimePicker dateTimePickerDate;

		private MMLabel labelCloseDate;

		private MMTextBox textBoxNote;

		private MMTextBox textBoxClientName;

		private FormManager formManager;

		private MMTextBox textBoxActionName;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel4;

		private MMLabel labelName;

		private UltraTabPageControl ultraTabPageControl2;

		private Button button1;

		private GadgetDateRangeComboBox comboBoxFollowupPeriod;

		private DataGridList dataGridListFollowup;

		private Button buttonAddActivity;

		private MMLabel mmLabel25;

		private CasePartyComboBox comboBoxCaseParty;

		private MMTextBox textBoxLawyer;

		private LawyerComboBox ComboBoxLawyer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ToolStripButton toolStripButtonComments;

		private UltraFormattedLinkLabel labelStatus;

		private GenericListComboBox comboBoxLegalStatus;

		private UltraTabPageControl ultraTabPageControl3;

		private DataGridList dataGridList;

		private MMLabel mmLabel1;

		private AnalysisComboBox comboBoxAnalysis;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripSeparator toolStripSeparator4;

		private MMTextBox textBoxCaseType;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private GenericListComboBox comboBoxCaseType;

		private MMTextBox textBoxParentCode;

		private SysDocComboBox comboBoxParentsysDoc;

		private MMTextBox textboxFileNo;

		private MMLabel mmLabel2;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem createFromExistingActivityToolStripMenuItem;

		private CaseClientComboBox comboBoxDefendant;

		private XPButton xpButton2;

		private GroupBox groupBox1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel9;

		private MMTextBox textBoxContactName;

		private ContactsComboBox comboBoxContact;

		private UserComboBox comboBoxOwner;

		private DateTimePicker dateTimePickerActDate;

		private MMLabel mmLabel3;

		private MMTextBox textBoxNote1;

		private MMLabel mmLabel6;

		private MMLabel mmLabel7;

		private MMTextBox textboxActivityName;

		private CheckBox checkBoxChangeStatus;

		private CaseClientComboBox comboBoxPlantiff;

		private MMTextBox mmTextBox1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private MMLabel mmLabel5;

		private DateTimePicker dateTimePickerActTime;

		public ScreenAreas ScreenArea => ScreenAreas.Legal;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

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
				comboBoxSysDoc.Enabled = value;
				toolStripButtonAttach.Enabled = !value;
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

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		public string ParentSysDocID
		{
			get
			{
				return parentSysDocID;
			}
			set
			{
				parentSysDocID = value;
			}
		}

		public string ParentVoucherID
		{
			get
			{
				return parentVoucherID;
			}
			set
			{
				parentVoucherID = value;
			}
		}

		public LegalActivityDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			companyInformation = Factory.CompanyInformationSystem.GetCompanyInformation();
			comboBoxAnalysis.FilterByAccount(companyInformation.Tables[0].Rows[0]["LegalAnalysisGroup"].ToString());
		}

		private void AddEvents()
		{
			base.Load += LegalActivityDetailsForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dataGridListFollowup.DoubleClick += dataGridListFollowup_DoubleClick;
		}

		private void dataGridListFollowup_DoubleClick(object sender, EventArgs e)
		{
			int num = checked(dataGridListFollowup.Rows.Count - 1);
			num = dataGridListFollowup.ActiveRow.Index;
			string followupId = dataGridListFollowup.Rows[num].Cells["FollowupID"].Value.ToString();
			dataGridListFollowup.Rows[num].Cells["SourceVoucherID"].Value.ToString();
			dataGridListFollowup.Rows[num].Cells["SourceSysDocID"].Value.ToString();
			FormActivator.BringFormToFront(FormActivator.FollowupDetailsFormObj);
			FormActivator.FollowupDetailsFormObj.SourceSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.FollowupDetailsFormObj.SourceVoucherID = textBoxCode.Text;
			FormActivator.FollowupDetailsFormObj.CRMType = CRMRelatedTypes.LegalActivity;
			FormActivator.FollowupDetailsFormObj.FollowupId = followupId;
			FormActivator.FollowupDetailsFormObj.EditDocument(followupId);
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxCode.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxCode, textBoxCode.Text);
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new LegalActivityData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.LegalActivityTable.Rows[0] : currentData.LegalActivityTable.NewRow();
				dataRow.BeginEdit();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxCode.Text.Trim();
				dataRow["ActivityName"] = textboxActivityName.Text.Trim();
				dataRow["ActionName"] = textBoxActionName.Text.Trim();
				dataRow["FileNo"] = textboxFileNo.Text.Trim();
				dataRow["VoucherID"] = textBoxCode.Text.Trim();
				dataRow["ParentSysDocID"] = comboBoxParentsysDoc.SelectedID;
				dataRow["ParentVoucherID"] = textBoxParentCode.Text.Trim();
				dataRow["ParentSysDocID"] = ParentSysDocID;
				dataRow["ParentVoucherID"] = ParentVoucherID;
				if (comboBoxDefendant.SelectedID != "")
				{
					dataRow["CaseClient1"] = comboBoxDefendant.SelectedID;
				}
				else
				{
					dataRow["CaseClient1"] = DBNull.Value;
				}
				if (comboBoxPlantiff.SelectedID != "")
				{
					dataRow["CaseClient2"] = comboBoxPlantiff.SelectedID;
				}
				else
				{
					dataRow["CaseClient2"] = DBNull.Value;
				}
				if (comboBoxCaseParty.SelectedID != "")
				{
					dataRow["CasePartyID"] = comboBoxCaseParty.SelectedID;
				}
				else
				{
					dataRow["CasePartyID"] = DBNull.Value;
				}
				if (ComboBoxLawyer.SelectedID != "")
				{
					dataRow["LawyerID"] = ComboBoxLawyer.SelectedID;
				}
				else
				{
					dataRow["LawyerID"] = DBNull.Value;
				}
				if (comboBoxLegalStatus.SelectedID != "")
				{
					dataRow["StatusID"] = comboBoxLegalStatus.SelectedID;
				}
				else
				{
					dataRow["StatusID"] = DBNull.Value;
				}
				if (comboBoxCaseType.SelectedID != "")
				{
					dataRow["CaseTypeID"] = comboBoxCaseType.SelectedID;
				}
				else
				{
					dataRow["CaseTypeID"] = DBNull.Value;
				}
				dataRow["ActivityDateTime"] = dateTimePickerDate.Value.ToShortDateString();
				dataRow["Note"] = textBoxNote1.Text;
				if (comboBoxOwner.SelectedID != "")
				{
					dataRow["OwnerID"] = comboBoxOwner.SelectedID;
				}
				else
				{
					dataRow["OwnerID"] = DBNull.Value;
				}
				if (comboBoxContact.SelectedID != "")
				{
					dataRow["ContactID"] = comboBoxContact.SelectedID;
				}
				else
				{
					dataRow["ContactID"] = DBNull.Value;
				}
				dataRow["ActDateTime"] = dateTimePickerActDate.Value.ToShortDateString() + " " + dateTimePickerActTime.Value.ToShortTimeString();
				dataRow["Note"] = textBoxNote1.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.LegalActivityTable.Rows.Add(dataRow);
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

		public void AddNewActivity(CRMRelatedTypes type, string relatedID, string parentSysDocID, string parentVoucherID)
		{
			ParentSysDocID = parentSysDocID;
			this.parentVoucherID = parentVoucherID;
			DataRow dataRow = Factory.LegalActionSystem.GetLegalActionByID(parentSysDocID, parentVoucherID, comboBoxAnalysis.SelectedID).LegalActionTable.Rows[0];
			textBoxParentCode.Text = dataRow["VoucherID"].ToString();
			comboBoxParentsysDoc.SelectedID = dataRow["VoucherID"].ToString();
			comboBoxDefendant.SelectedID = dataRow["Caseclient1"].ToString();
			comboBoxPlantiff.SelectedID = dataRow["Caseclient2"].ToString();
			comboBoxCaseParty.SelectedID = dataRow["CasePartyID"].ToString();
			ComboBoxLawyer.SelectedID = dataRow["LawyerID"].ToString();
			comboBoxCaseType.SelectedID = dataRow["CaseTypeID"].ToString();
			textboxFileNo.Text = dataRow["FileNo"].ToString();
			if (dataRow["ParentSysDocID"].ToString() == "" || dataRow["ParentSysDocID"].ToString() == string.Empty)
			{
				comboBoxParentsysDoc.SelectedID = dataRow["SysDocID"].ToString();
				textBoxParentCode.Text = dataRow["VoucherID"].ToString();
				ParentSysDocID = comboBoxParentsysDoc.SelectedID;
				ParentVoucherID = textBoxParentCode.Text;
			}
			else
			{
				comboBoxParentsysDoc.SelectedID = dataRow["ParentSysDocID"].ToString();
				textBoxParentCode.Text = dataRow["ParentVoucherID"].ToString();
				ParentSysDocID = comboBoxParentsysDoc.SelectedID;
				ParentVoucherID = textBoxParentCode.Text;
			}
			comboBoxAnalysis.SelectedID = dataRow["AnalysisID"].ToString();
			textBoxActionName.Text = dataRow["ActionName"].ToString();
			textBoxNote1.Text = dataRow["Note"].ToString();
			if (ParentSysDocID != "" && ParentVoucherID != "")
			{
				xpButton2.Visible = false;
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.LegalActivitySystem.GetLegalActivityByID(SystemDocID, id.Trim(), comboBoxAnalysis.SelectedID);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables["Legal_Activity"].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
						textBoxCode.Focus();
					}
					else
					{
						IsNewRecord = false;
						FillData();
						LoadFollowUp();
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
				comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
				textBoxCode.Text = dataRow["VoucherID"].ToString();
				textBoxActionName.Text = dataRow["ActionName"].ToString();
				textboxActivityName.Text = dataRow["ActivityName"].ToString();
				comboBoxParentsysDoc.SelectedID = dataRow["ParentSysDocID"].ToString();
				textBoxParentCode.Text = dataRow["ParentVoucherID"].ToString();
				textboxFileNo.Text = dataRow["FileNo"].ToString();
				ParentSysDocID = dataRow["ParentSysDocID"].ToString();
				parentVoucherID = dataRow["ParentVoucherID"].ToString();
				comboBoxDefendant.SelectedID = dataRow["CaseClient1"].ToString();
				comboBoxPlantiff.SelectedID = dataRow["CaseClient2"].ToString();
				ComboBoxLawyer.SelectedID = dataRow["LawyerID"].ToString();
				comboBoxCaseParty.SelectedID = dataRow["CasePartyID"].ToString();
				comboBoxLegalStatus.SelectedID = dataRow["StatusID"].ToString();
				comboBoxCaseType.SelectedID = dataRow["CaseTypeID"].ToString();
				DateTime dateTime = DateTime.Parse(dataRow["ActivityDateTime"].ToString());
				dateTimePickerDate.Value = dateTime.Date;
				comboBoxOwner.SelectedID = dataRow["OwnerID"].ToString();
				comboBoxContact.SelectedID = dataRow["ContactID"].ToString();
				DateTime dateTime2 = DateTime.Parse(dataRow["ActDateTime"].ToString());
				dateTimePickerActDate.Value = dateTime2.Date;
				dateTimePickerActTime.Value = DateTime.Parse(dateTime2.ToString("hh:mm:ss"));
				textBoxNote1.Text = dataRow["Note"].ToString();
				if (dataRow["AnalysisID"] != DBNull.Value)
				{
					comboBoxAnalysis.SelectedID = dataRow["AnalysisID"].ToString();
				}
				else
				{
					comboBoxAnalysis.Clear();
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
				bool flag = (!isNewRecord) ? Factory.LegalActivitySystem.UpdateLegalActivity(currentData) : Factory.LegalActivitySystem.CreateLegalActivity(currentData);
				FormActivator.LegalActionDetailsFormObj.LoadActivities();
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
			if (comboBoxSysDoc.SelectedID == "" || textBoxCode.Text.Trim().Length == 0 || textBoxActionName.Text.Trim().Length == 0 || textboxActivityName.Text.Trim().Length == 0 || textboxFileNo.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Legal_Activity", "VoucherID", textBoxCode.Text.Trim(), "SysDocID", SystemDocID))
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
			textBoxCode.Text = GetNextVoucherNumber();
			textBoxActionName.Clear();
			textboxActivityName.Clear();
			comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
			textBoxClientName.Clear();
			dateTimePickerDate.Value = DateTime.Now;
			comboBoxDefendant.Clear();
			ComboBoxLawyer.Clear();
			comboBoxCaseParty.Clear();
			textBoxNote1.Clear();
			comboBoxLegalStatus.Clear();
			comboBoxParentsysDoc.Clear();
			textBoxParentCode.Clear();
			textboxFileNo.Clear();
			comboBoxCaseType.Clear();
			comboBoxPlantiff.Clear();
			comboBoxOwner.LoadData();
			comboBoxOwner.SelectedID = Global.CurrentUser.ToUpper();
			comboBoxContact.Clear();
			dateTimePickerActDate.Value = DateTime.Now;
			dateTimePickerActTime.Value = DateTime.Now;
			ParentSysDocID = "";
			parentVoucherID = "";
			formManager.ResetDirty();
			textBoxCode.Focus();
			comboBoxAnalysis.Clear();
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
				if (ErrorHelper.QuestionMessageYesNo("Are you sure! you want to delete the record?") == DialogResult.No)
				{
					return false;
				}
				Factory.AnalysisSystem.DeleteAnalysis(comboBoxAnalysis.SelectedID);
				bool num = Factory.LegalActivitySystem.DeleteLegalActivity(SystemDocID, textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Activity, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("Legal_Activity", "VoucherID", textBoxCode.Text, "SysDocID", SystemDocID));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Legal_Activity", "VoucherID", textBoxCode.Text, "SysDocID", SystemDocID));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Legal_Activity", "VoucherID", "SysDocID", SystemDocID));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Legal_Activity", "VoucherID", "SysDocID", SystemDocID));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Legal_Activity", "VoucherID", toolStripTextBoxFind.Text.Trim(), "SysDocID", SystemDocID))
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

		private void LegalActivityDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				dataGridListFollowup.ApplyUIDesign();
				SetupFollowGrid();
				dataGridList.ApplyUIDesign();
				SetupGrid();
				comboBoxFollowupPeriod.LoadData();
				comboBoxFollowupPeriod.SelectedIndex = 13;
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					comboBoxSysDoc.FilterByType(SysDocTypes.LegalActivity);
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataGridList.LoadLayoutFailed = true;
				dataGridListFollowup.LoadLayoutFailed = true;
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
			FormActivator.BringFormToFront(FormActivator.LegalActivityListObj);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxCode.Text.Trim();
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

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.LegalActivity);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxCode.Text = GetNextVoucherNumber();
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

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				comboBoxFollowupPeriod.SelectedIndex = 13;
				LoadFollowUp();
				DateTime dateTime = DateTime.Now;
				string text = "";
				int result = 0;
				if (dataGridListFollowup.Rows.Count > 0)
				{
					int index = checked(dataGridListFollowup.Rows.Count - 1);
					dateTime = DateTime.Parse(dataGridListFollowup.Rows[index].Cells["NextFollowupDate"].Value.ToString());
					dataGridListFollowup.Rows[index].Cells["NextFollow_upBy"].Value.ToString().TrimEnd();
					text = dataGridListFollowup.Rows[index].Cells["NextFollowupByID"].Value.ToString();
					int.TryParse(dataGridListFollowup.Rows[index].Cells["StatusID"].Value.ToString(), out result);
				}
				FormActivator.BringFormToFront(FormActivator.FollowupDetailsFormObj);
				FormActivator.FollowupDetailsFormObj.SourceSysDocID = comboBoxSysDoc.SelectedID;
				FormActivator.FollowupDetailsFormObj.SourceVoucherID = textBoxCode.Text;
				FormActivator.FollowupDetailsFormObj.CRMType = CRMRelatedTypes.LegalActivity;
				FormActivator.FollowupDetailsFormObj.ThisfollowupBy = text.TrimEnd();
				FormActivator.FollowupDetailsFormObj.ThisfollowupDate = dateTime;
				FormActivator.FollowupDetailsFormObj.ThisfollowupTime = dateTime;
				FormActivator.FollowupDetailsFormObj.Status = result;
				FormActivator.FollowupDetailsFormObj.AddFollowUp();
				FormActivator.FollowupDetailsFormObj.LoadFollowUp();
			}
		}

		private void SetupFollowGrid()
		{
			try
			{
				dataGridListFollowup.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("FollowupID");
				dataTable.Columns.Add("SourceSysDocID");
				dataTable.Columns.Add("SourceVoucherID");
				dataTable.Columns.Add("FollowupDate", typeof(DateTime));
				dataTable.Columns.Add("Follow_upBy");
				dataTable.Columns.Add("NextFollowupDate", typeof(DateTime));
				dataTable.Columns.Add("NextFollow_upBy");
				dataTable.Columns.Add("NextFollowupByID");
				dataTable.Columns.Add("Status");
				dataTable.Columns.Add("StatusID");
				dataGridListFollowup.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridListFollowup.DisplayLayout.Bands[0].Columns["StatusID"];
				UltraGridColumn ultraGridColumn2 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["NextFollowupByID"];
				UltraGridColumn ultraGridColumn3 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["FollowupID"];
				UltraGridColumn ultraGridColumn4 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["SourceSysDocID"];
				bool flag2 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["SourceVoucherID"].Hidden = true;
				bool flag4 = ultraGridColumn4.Hidden = flag2;
				bool flag6 = ultraGridColumn3.Hidden = flag4;
				bool hidden = ultraGridColumn2.Hidden = flag6;
				ultraGridColumn.Hidden = hidden;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void LoadFollowUp()
		{
			try
			{
				if (!isNewRecord)
				{
					followUpData = Factory.FollowupSystem.GetFollowupListByActivityID(CRMRelatedTypes.Followup, textBoxCode.Text, textBoxCode.Text, comboBoxSysDoc.SelectedID, comboBoxFollowupPeriod.FromDate, comboBoxFollowupPeriod.ToDate);
					DataTable dataTable = dataGridListFollowup.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in followUpData.Tables[0].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["FollowupID"] = row["FollowupID"];
						dataRow2["SourceSysDocID"] = row["SourceSysDocID"];
						dataRow2["SourceVoucherID"] = row["SourceVoucherID"];
						dataRow2["FollowupDate"] = row["ThisFollowupDate"];
						dataRow2["Follow_upBy"] = row["Follow_upBy"];
						dataRow2["NextFollowupDate"] = row["NextFollowupDate"];
						dataRow2["NextFollow_upBy"] = row["NextFollow_upBy"];
						dataRow2["Status"] = row["Status"];
						dataRow2["StatusID"] = row["ThisFollowupStatusID"];
						dataRow2["NextFollowupByID"] = row["NextFollowupByID"];
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
					dataTable.AcceptChanges();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void comboBoxFollowupPeriod_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadFollowUp();
		}

		private void ultraFormattedLinkLabel5_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.LegalActivity);
		}

		private void ultraFormattedLinkLabel3_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCaseClient(comboBoxDefendant.SelectedID);
		}

		private void ultraTabPageControl1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCaseClient(comboBoxPlantiff.SelectedID.ToString());
		}

		private void formManager_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void toolStripButtonComments_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					EntityCommentsForm entityCommentsForm = new EntityCommentsForm();
					entityCommentsForm.EntityID = comboBoxSysDoc.SelectedID;
					entityCommentsForm.EntityName = textBoxCode.Text;
					entityCommentsForm.EntityType = EntityTypesEnum.LegalActivitys;
					entityCommentsForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel6_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.ActionStatus, comboBoxLegalStatus.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked_2(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCaseParty(comboBoxCaseParty.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLawyer(ComboBoxLawyer.SelectedID);
		}

		private void SetupGrid()
		{
			try
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("SysDocID");
				dataTable.Columns.Add("VoucherID");
				dataTable.Columns.Add("Date", typeof(DateTime));
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Reference");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridList.DataSource = dataTable;
				dataGridList.DisplayLayout.Bands[0].Columns["SysDocID"].Header.Caption = "Doc ID";
				dataGridList.DisplayLayout.Bands[0].Columns["VoucherID"].Header.Caption = "Number";
				dataGridList.DisplayLayout.Bands[0].Columns["Amount"].Format = "#,##0.00";
				dataGridList.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridList.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.Select;
			}
			catch (Exception e)
			{
				dataGridList.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				string selectedID = comboBoxSysDoc.SelectedID;
				string text = textBoxCode.Text;
				DataSet leaglActivityToPrint = Factory.LegalActivitySystem.GetLeaglActivityToPrint(selectedID, text);
				if (leaglActivityToPrint == null || leaglActivityToPrint.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(leaglActivityToPrint, selectedID, "Legal Activity", SysDocTypes.LegalActivity, isPrint, showPrintDialog);
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

		private void createFromExistingActivityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet legalActionList = Factory.LegalActionSystem.GetLegalActionList(DateTime.MinValue, DateTime.MaxValue);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = legalActionList;
			selectDocumentDialog.Text = "Select Legal Action";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				ClearForm();
				string sysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				string voucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				DataRow dataRow = Factory.LegalActionSystem.GetLegalActionByID(sysDocID, voucherID, comboBoxAnalysis.SelectedID).LegalActionTable.Rows[0];
				textBoxParentCode.Text = dataRow["VoucherID"].ToString();
				comboBoxParentsysDoc.SelectedID = dataRow["VoucherID"].ToString();
				comboBoxDefendant.SelectedID = dataRow["Caseclient1"].ToString();
				comboBoxPlantiff.SelectedID = dataRow["Caseclient2"].ToString();
				comboBoxCaseParty.SelectedID = dataRow["CasePartyID"].ToString();
				ComboBoxLawyer.SelectedID = dataRow["LawyerID"].ToString();
				comboBoxCaseType.SelectedID = dataRow["CaseTypeID"].ToString();
				textboxFileNo.Text = dataRow["FileNo"].ToString();
				if (dataRow["ParentSysDocID"].ToString() == "" || dataRow["ParentSysDocID"].ToString() == string.Empty)
				{
					comboBoxParentsysDoc.SelectedID = dataRow["SysDocID"].ToString();
					textBoxParentCode.Text = dataRow["VoucherID"].ToString();
				}
				else
				{
					comboBoxParentsysDoc.SelectedID = dataRow["ParentSysDocID"].ToString();
					textBoxParentCode.Text = dataRow["ParentVoucherID"].ToString();
				}
				ParentSysDocID = comboBoxParentsysDoc.SelectedID;
				ParentVoucherID = textBoxParentCode.Text;
				comboBoxAnalysis.SelectedID = dataRow["AnalysisID"].ToString();
				textBoxActionName.Text = dataRow["ActionName"].ToString();
				textBoxNote1.Text = dataRow["Note"].ToString();
			}
		}

		private void ultraFormattedLinkLabel7_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.CaseType, comboBoxCaseType.SelectedID);
		}

		private void ultraTabControl1_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditContact(comboBoxContact.SelectedID);
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditUser(comboBoxOwner.SelectedID);
		}

		private void checkBoxChangeStatus_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxChangeStatus.Checked)
			{
				comboBoxLegalStatus.Visible = true;
				labelStatus.Visible = true;
			}
			else
			{
				comboBoxLegalStatus.Visible = false;
				labelStatus.Visible = false;
			}
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet legalActionList = Factory.LegalActionSystem.GetLegalActionList(DateTime.MinValue, DateTime.MaxValue);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = legalActionList;
			selectDocumentDialog.Text = "Select Legal Action";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				ClearForm();
				string sysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				string voucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				DataRow dataRow = Factory.LegalActionSystem.GetLegalActionByID(sysDocID, voucherID, comboBoxAnalysis.SelectedID).LegalActionTable.Rows[0];
				textBoxParentCode.Text = dataRow["VoucherID"].ToString();
				comboBoxParentsysDoc.SelectedID = dataRow["VoucherID"].ToString();
				comboBoxDefendant.SelectedID = dataRow["Caseclient1"].ToString();
				comboBoxPlantiff.SelectedID = dataRow["Caseclient2"].ToString();
				comboBoxCaseParty.SelectedID = dataRow["CasePartyID"].ToString();
				ComboBoxLawyer.SelectedID = dataRow["LawyerID"].ToString();
				comboBoxCaseType.SelectedID = dataRow["CaseTypeID"].ToString();
				textboxFileNo.Text = dataRow["FileNo"].ToString();
				if (dataRow["ParentSysDocID"].ToString() == "" || dataRow["ParentSysDocID"].ToString() == string.Empty)
				{
					comboBoxParentsysDoc.SelectedID = dataRow["SysDocID"].ToString();
					textBoxParentCode.Text = dataRow["VoucherID"].ToString();
					ParentSysDocID = comboBoxParentsysDoc.SelectedID;
					ParentVoucherID = textBoxParentCode.Text;
				}
				else
				{
					comboBoxParentsysDoc.SelectedID = dataRow["ParentSysDocID"].ToString();
					textBoxParentCode.Text = dataRow["ParentVoucherID"].ToString();
					ParentSysDocID = comboBoxParentsysDoc.SelectedID;
					ParentVoucherID = textBoxParentCode.Text;
				}
				comboBoxAnalysis.SelectedID = dataRow["AnalysisID"].ToString();
				textBoxActionName.Text = dataRow["ActionName"].ToString();
				textBoxNote1.Text = dataRow["Note"].ToString();
				comboBoxLegalStatus.SelectedID = dataRow["StatusID"].ToString();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Legal.LegalActivityDetailsForm));
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance180 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance181 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance182 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance183 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance184 = new Infragistics.Win.Appearance();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			dateTimePickerActTime = new System.Windows.Forms.DateTimePicker();
			comboBoxPlantiff = new Micromind.DataControls.CaseClientComboBox();
			mmTextBox1 = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			checkBoxChangeStatus = new System.Windows.Forms.CheckBox();
			textboxActivityName = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel9 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxContactName = new Micromind.UISupport.MMTextBox();
			comboBoxContact = new Micromind.DataControls.ContactsComboBox();
			comboBoxOwner = new Micromind.DataControls.UserComboBox();
			dateTimePickerActDate = new System.Windows.Forms.DateTimePicker();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxNote1 = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			xpButton2 = new Micromind.UISupport.XPButton();
			comboBoxDefendant = new Micromind.DataControls.CaseClientComboBox();
			textBoxClientName = new Micromind.UISupport.MMTextBox();
			textboxFileNo = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxParentCode = new Micromind.UISupport.MMTextBox();
			comboBoxParentsysDoc = new Micromind.DataControls.SysDocComboBox();
			labelStatus = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxLegalStatus = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			labelCloseDate = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxActionName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			labelName = new Micromind.UISupport.MMLabel();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			button1 = new System.Windows.Forms.Button();
			comboBoxFollowupPeriod = new Micromind.DataControls.GadgetDateRangeComboBox(components);
			buttonAddActivity = new System.Windows.Forms.Button();
			dataGridListFollowup = new Micromind.UISupport.DataGridList(components);
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			comboBoxAnalysis = new Micromind.DataControls.AnalysisComboBox();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			groupBox1 = new System.Windows.Forms.GroupBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxCaseParty = new Micromind.UISupport.MMTextBox();
			comboBoxCaseParty = new Micromind.DataControls.CasePartyComboBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxCaseType = new Micromind.UISupport.MMTextBox();
			ComboBoxLawyer = new Micromind.DataControls.LawyerComboBox();
			textBoxLawyer = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCaseType = new Micromind.DataControls.GenericListComboBox();
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
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonComments = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			createFromExistingActivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPlantiff).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxContact).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOwner).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefendant).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxParentsysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLegalStatus).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupPeriod.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridListFollowup).BeginInit();
			ultraTabPageControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCaseParty).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxLawyer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCaseType).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			ultraTabPageControl1.Controls.Add(mmLabel5);
			ultraTabPageControl1.Controls.Add(dateTimePickerActTime);
			ultraTabPageControl1.Controls.Add(comboBoxPlantiff);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel6);
			ultraTabPageControl1.Controls.Add(mmTextBox1);
			ultraTabPageControl1.Controls.Add(checkBoxChangeStatus);
			ultraTabPageControl1.Controls.Add(textboxActivityName);
			ultraTabPageControl1.Controls.Add(mmLabel7);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel8);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel9);
			ultraTabPageControl1.Controls.Add(textBoxContactName);
			ultraTabPageControl1.Controls.Add(comboBoxContact);
			ultraTabPageControl1.Controls.Add(comboBoxOwner);
			ultraTabPageControl1.Controls.Add(dateTimePickerActDate);
			ultraTabPageControl1.Controls.Add(mmLabel3);
			ultraTabPageControl1.Controls.Add(textBoxNote1);
			ultraTabPageControl1.Controls.Add(mmLabel6);
			ultraTabPageControl1.Controls.Add(xpButton2);
			ultraTabPageControl1.Controls.Add(comboBoxDefendant);
			ultraTabPageControl1.Controls.Add(textboxFileNo);
			ultraTabPageControl1.Controls.Add(mmLabel2);
			ultraTabPageControl1.Controls.Add(textBoxParentCode);
			ultraTabPageControl1.Controls.Add(comboBoxParentsysDoc);
			ultraTabPageControl1.Controls.Add(labelStatus);
			ultraTabPageControl1.Controls.Add(comboBoxLegalStatus);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel1);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel5);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel2);
			ultraTabPageControl1.Controls.Add(comboBoxSysDoc);
			ultraTabPageControl1.Controls.Add(dateTimePickerDate);
			ultraTabPageControl1.Controls.Add(labelCloseDate);
			ultraTabPageControl1.Controls.Add(textBoxClientName);
			ultraTabPageControl1.Controls.Add(formManager);
			ultraTabPageControl1.Controls.Add(textBoxActionName);
			ultraTabPageControl1.Controls.Add(textBoxCode);
			ultraTabPageControl1.Controls.Add(labelName);
			ultraTabPageControl1.Location = new System.Drawing.Point(2, 21);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(691, 348);
			ultraTabPageControl1.Paint += new System.Windows.Forms.PaintEventHandler(ultraTabPageControl1_Paint);
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(462, 167);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(33, 13);
			mmLabel5.TabIndex = 191;
			mmLabel5.Text = "Time:";
			dateTimePickerActTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerActTime.Location = new System.Drawing.Point(501, 162);
			dateTimePickerActTime.Name = "dateTimePickerActTime";
			dateTimePickerActTime.Size = new System.Drawing.Size(129, 20);
			dateTimePickerActTime.TabIndex = 190;
			comboBoxPlantiff.Assigned = false;
			comboBoxPlantiff.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPlantiff.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPlantiff.CustomReportFieldName = "";
			comboBoxPlantiff.CustomReportKey = "";
			comboBoxPlantiff.CustomReportValueType = 1;
			comboBoxPlantiff.DescriptionTextBox = mmTextBox1;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPlantiff.DisplayLayout.Appearance = appearance;
			comboBoxPlantiff.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPlantiff.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPlantiff.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPlantiff.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxPlantiff.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPlantiff.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxPlantiff.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPlantiff.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPlantiff.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPlantiff.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxPlantiff.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPlantiff.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPlantiff.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPlantiff.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxPlantiff.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPlantiff.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPlantiff.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxPlantiff.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxPlantiff.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPlantiff.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxPlantiff.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxPlantiff.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPlantiff.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxPlantiff.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPlantiff.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPlantiff.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPlantiff.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPlantiff.Editable = true;
			comboBoxPlantiff.FilterString = "";
			comboBoxPlantiff.FilterSysDocID = "";
			comboBoxPlantiff.HasAll = false;
			comboBoxPlantiff.HasCustom = false;
			comboBoxPlantiff.IsDataLoaded = false;
			comboBoxPlantiff.Location = new System.Drawing.Point(102, 119);
			comboBoxPlantiff.MaxDropDownItems = 12;
			comboBoxPlantiff.Name = "comboBoxPlantiff";
			comboBoxPlantiff.ReadOnly = true;
			comboBoxPlantiff.ShowDefendant = false;
			comboBoxPlantiff.ShowInactive = false;
			comboBoxPlantiff.ShowPlantiff = false;
			comboBoxPlantiff.ShowPROCustomersOnly = false;
			comboBoxPlantiff.ShowQuickAdd = true;
			comboBoxPlantiff.Size = new System.Drawing.Size(168, 20);
			comboBoxPlantiff.TabIndex = 9;
			comboBoxPlantiff.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBox1.CustomReportFieldName = "";
			mmTextBox1.CustomReportKey = "";
			mmTextBox1.CustomReportValueType = 1;
			mmTextBox1.IsComboTextBox = false;
			mmTextBox1.IsModified = false;
			mmTextBox1.Location = new System.Drawing.Point(273, 119);
			mmTextBox1.MaxLength = 64;
			mmTextBox1.Name = "mmTextBox1";
			mmTextBox1.ReadOnly = true;
			mmTextBox1.Size = new System.Drawing.Size(391, 20);
			mmTextBox1.TabIndex = 188;
			mmTextBox1.TabStop = false;
			appearance13.BackColor = System.Drawing.Color.WhiteSmoke;
			appearance13.BackColor2 = System.Drawing.Color.White;
			appearance13.FontData.BoldAsString = "True";
			appearance13.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance13;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(8, 122);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(48, 15);
			ultraFormattedLinkLabel6.TabIndex = 189;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Plantiff:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			checkBoxChangeStatus.BackColor = System.Drawing.Color.Transparent;
			checkBoxChangeStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxChangeStatus.Location = new System.Drawing.Point(407, 53);
			checkBoxChangeStatus.Name = "checkBoxChangeStatus";
			checkBoxChangeStatus.Size = new System.Drawing.Size(108, 17);
			checkBoxChangeStatus.TabIndex = 186;
			checkBoxChangeStatus.Text = "Change Status";
			checkBoxChangeStatus.UseVisualStyleBackColor = false;
			checkBoxChangeStatus.CheckedChanged += new System.EventHandler(checkBoxChangeStatus_CheckedChanged);
			textboxActivityName.BackColor = System.Drawing.Color.White;
			textboxActivityName.CustomReportFieldName = "";
			textboxActivityName.CustomReportKey = "";
			textboxActivityName.CustomReportValueType = 1;
			textboxActivityName.IsComboTextBox = false;
			textboxActivityName.IsModified = false;
			textboxActivityName.Location = new System.Drawing.Point(102, 31);
			textboxActivityName.MaxLength = 64;
			textboxActivityName.Name = "textboxActivityName";
			textboxActivityName.Size = new System.Drawing.Size(259, 20);
			textboxActivityName.TabIndex = 3;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = true;
			mmLabel7.Location = new System.Drawing.Point(8, 35);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(89, 13);
			mmLabel7.TabIndex = 185;
			mmLabel7.Text = "Activity Name:";
			appearance15.BackColor = System.Drawing.Color.WhiteSmoke;
			appearance15.BackColor2 = System.Drawing.Color.White;
			appearance15.FontData.BoldAsString = "False";
			appearance15.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel8.Appearance = appearance15;
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(8, 166);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(59, 15);
			ultraFormattedLinkLabel8.TabIndex = 184;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Activity By:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance16;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			appearance17.BackColor = System.Drawing.Color.WhiteSmoke;
			appearance17.BackColor2 = System.Drawing.Color.White;
			appearance17.FontData.BoldAsString = "False";
			appearance17.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel9.Appearance = appearance17;
			ultraFormattedLinkLabel9.AutoSize = true;
			ultraFormattedLinkLabel9.Location = new System.Drawing.Point(8, 144);
			ultraFormattedLinkLabel9.Name = "ultraFormattedLinkLabel9";
			ultraFormattedLinkLabel9.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel9.TabIndex = 183;
			ultraFormattedLinkLabel9.TabStop = true;
			ultraFormattedLinkLabel9.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel9.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel9.Value = "Contact:";
			appearance18.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel9.VisitedLinkAppearance = appearance18;
			ultraFormattedLinkLabel9.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel9_LinkClicked);
			textBoxContactName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxContactName.CustomReportFieldName = "";
			textBoxContactName.CustomReportKey = "";
			textBoxContactName.CustomReportValueType = 1;
			textBoxContactName.IsComboTextBox = false;
			textBoxContactName.IsModified = false;
			textBoxContactName.Location = new System.Drawing.Point(273, 141);
			textBoxContactName.MaxLength = 64;
			textBoxContactName.Name = "textBoxContactName";
			textBoxContactName.ReadOnly = true;
			textBoxContactName.Size = new System.Drawing.Size(358, 20);
			textBoxContactName.TabIndex = 175;
			textBoxContactName.TabStop = false;
			comboBoxContact.Assigned = false;
			comboBoxContact.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxContact.CustomReportFieldName = "";
			comboBoxContact.CustomReportKey = "";
			comboBoxContact.CustomReportValueType = 1;
			comboBoxContact.DescriptionTextBox = textBoxContactName;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxContact.DisplayLayout.Appearance = appearance19;
			comboBoxContact.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxContact.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance20.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance20.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance20.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxContact.DisplayLayout.GroupByBox.Appearance = appearance20;
			appearance21.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxContact.DisplayLayout.GroupByBox.BandLabelAppearance = appearance21;
			comboBoxContact.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance22.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance22.BackColor2 = System.Drawing.SystemColors.Control;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance22.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxContact.DisplayLayout.GroupByBox.PromptAppearance = appearance22;
			comboBoxContact.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxContact.DisplayLayout.MaxRowScrollRegions = 1;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxContact.DisplayLayout.Override.ActiveCellAppearance = appearance23;
			appearance24.BackColor = System.Drawing.SystemColors.Highlight;
			appearance24.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxContact.DisplayLayout.Override.ActiveRowAppearance = appearance24;
			comboBoxContact.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxContact.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			comboBoxContact.DisplayLayout.Override.CardAreaAppearance = appearance25;
			appearance26.BorderColor = System.Drawing.Color.Silver;
			appearance26.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxContact.DisplayLayout.Override.CellAppearance = appearance26;
			comboBoxContact.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxContact.DisplayLayout.Override.CellPadding = 0;
			appearance27.BackColor = System.Drawing.SystemColors.Control;
			appearance27.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance27.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance27.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxContact.DisplayLayout.Override.GroupByRowAppearance = appearance27;
			appearance28.TextHAlignAsString = "Left";
			comboBoxContact.DisplayLayout.Override.HeaderAppearance = appearance28;
			comboBoxContact.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxContact.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.Color.Silver;
			comboBoxContact.DisplayLayout.Override.RowAppearance = appearance29;
			comboBoxContact.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxContact.DisplayLayout.Override.TemplateAddRowAppearance = appearance30;
			comboBoxContact.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxContact.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxContact.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxContact.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxContact.Editable = true;
			comboBoxContact.FilterString = "";
			comboBoxContact.HasAllAccount = false;
			comboBoxContact.HasCustom = false;
			comboBoxContact.IsDataLoaded = false;
			comboBoxContact.Location = new System.Drawing.Point(102, 141);
			comboBoxContact.MaxDropDownItems = 12;
			comboBoxContact.Name = "comboBoxContact";
			comboBoxContact.ShowInactiveItems = false;
			comboBoxContact.ShowQuickAdd = true;
			comboBoxContact.Size = new System.Drawing.Size(168, 20);
			comboBoxContact.TabIndex = 10;
			comboBoxContact.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxOwner.Assigned = false;
			comboBoxOwner.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxOwner.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxOwner.CustomReportFieldName = "";
			comboBoxOwner.CustomReportKey = "";
			comboBoxOwner.CustomReportValueType = 1;
			comboBoxOwner.DescriptionTextBox = null;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxOwner.DisplayLayout.Appearance = appearance31;
			comboBoxOwner.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxOwner.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOwner.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOwner.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			comboBoxOwner.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOwner.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			comboBoxOwner.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxOwner.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxOwner.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxOwner.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			comboBoxOwner.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxOwner.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			comboBoxOwner.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxOwner.DisplayLayout.Override.CellAppearance = appearance38;
			comboBoxOwner.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxOwner.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOwner.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			comboBoxOwner.DisplayLayout.Override.HeaderAppearance = appearance40;
			comboBoxOwner.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxOwner.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			comboBoxOwner.DisplayLayout.Override.RowAppearance = appearance41;
			comboBoxOwner.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxOwner.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			comboBoxOwner.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxOwner.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxOwner.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxOwner.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxOwner.Editable = true;
			comboBoxOwner.FilterString = "";
			comboBoxOwner.HasAllAccount = false;
			comboBoxOwner.HasCustom = false;
			comboBoxOwner.IsDataLoaded = false;
			comboBoxOwner.Location = new System.Drawing.Point(102, 163);
			comboBoxOwner.MaxDropDownItems = 12;
			comboBoxOwner.Name = "comboBoxOwner";
			comboBoxOwner.ShowInactiveItems = false;
			comboBoxOwner.ShowQuickAdd = true;
			comboBoxOwner.Size = new System.Drawing.Size(168, 20);
			comboBoxOwner.TabIndex = 11;
			comboBoxOwner.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimePickerActDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerActDate.Location = new System.Drawing.Point(331, 163);
			dateTimePickerActDate.Name = "dateTimePickerActDate";
			dateTimePickerActDate.Size = new System.Drawing.Size(129, 20);
			dateTimePickerActDate.TabIndex = 12;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(296, 165);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(33, 13);
			mmLabel3.TabIndex = 181;
			mmLabel3.Text = "Date:";
			textBoxNote1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote1.BackColor = System.Drawing.Color.White;
			textBoxNote1.CustomReportFieldName = "";
			textBoxNote1.CustomReportKey = "";
			textBoxNote1.CustomReportValueType = 1;
			textBoxNote1.IsComboTextBox = false;
			textBoxNote1.IsModified = false;
			textBoxNote1.Location = new System.Drawing.Point(102, 185);
			textBoxNote1.MaxLength = 4000;
			textBoxNote1.Multiline = true;
			textBoxNote1.Name = "textBoxNote1";
			textBoxNote1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote1.Size = new System.Drawing.Size(562, 149);
			textBoxNote1.TabIndex = 13;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(8, 187);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(33, 13);
			mmLabel6.TabIndex = 177;
			mmLabel6.Text = "Note:";
			xpButton2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton2.BackColor = System.Drawing.Color.DarkGray;
			xpButton2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton2.Location = new System.Drawing.Point(361, 53);
			xpButton2.Name = "xpButton2";
			xpButton2.Size = new System.Drawing.Size(25, 20);
			xpButton2.TabIndex = 5;
			xpButton2.Text = "..";
			xpButton2.UseVisualStyleBackColor = false;
			xpButton2.Click += new System.EventHandler(xpButton2_Click);
			comboBoxDefendant.Assigned = false;
			comboBoxDefendant.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDefendant.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDefendant.CustomReportFieldName = "";
			comboBoxDefendant.CustomReportKey = "";
			comboBoxDefendant.CustomReportValueType = 1;
			comboBoxDefendant.DescriptionTextBox = textBoxClientName;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDefendant.DisplayLayout.Appearance = appearance43;
			comboBoxDefendant.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDefendant.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance44.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance44.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefendant.DisplayLayout.GroupByBox.Appearance = appearance44;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefendant.DisplayLayout.GroupByBox.BandLabelAppearance = appearance45;
			comboBoxDefendant.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance46.BackColor2 = System.Drawing.SystemColors.Control;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefendant.DisplayLayout.GroupByBox.PromptAppearance = appearance46;
			comboBoxDefendant.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDefendant.DisplayLayout.MaxRowScrollRegions = 1;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDefendant.DisplayLayout.Override.ActiveCellAppearance = appearance47;
			appearance48.BackColor = System.Drawing.SystemColors.Highlight;
			appearance48.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDefendant.DisplayLayout.Override.ActiveRowAppearance = appearance48;
			comboBoxDefendant.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDefendant.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDefendant.DisplayLayout.Override.CardAreaAppearance = appearance49;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDefendant.DisplayLayout.Override.CellAppearance = appearance50;
			comboBoxDefendant.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDefendant.DisplayLayout.Override.CellPadding = 0;
			appearance51.BackColor = System.Drawing.SystemColors.Control;
			appearance51.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance51.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance51.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefendant.DisplayLayout.Override.GroupByRowAppearance = appearance51;
			appearance52.TextHAlignAsString = "Left";
			comboBoxDefendant.DisplayLayout.Override.HeaderAppearance = appearance52;
			comboBoxDefendant.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDefendant.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			comboBoxDefendant.DisplayLayout.Override.RowAppearance = appearance53;
			comboBoxDefendant.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDefendant.DisplayLayout.Override.TemplateAddRowAppearance = appearance54;
			comboBoxDefendant.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDefendant.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDefendant.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDefendant.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDefendant.Editable = true;
			comboBoxDefendant.FilterString = "";
			comboBoxDefendant.FilterSysDocID = "";
			comboBoxDefendant.HasAll = false;
			comboBoxDefendant.HasCustom = false;
			comboBoxDefendant.IsDataLoaded = false;
			comboBoxDefendant.Location = new System.Drawing.Point(102, 97);
			comboBoxDefendant.MaxDropDownItems = 12;
			comboBoxDefendant.Name = "comboBoxDefendant";
			comboBoxDefendant.ReadOnly = true;
			comboBoxDefendant.ShowDefendant = false;
			comboBoxDefendant.ShowInactive = false;
			comboBoxDefendant.ShowPlantiff = false;
			comboBoxDefendant.ShowPROCustomersOnly = false;
			comboBoxDefendant.ShowQuickAdd = true;
			comboBoxDefendant.Size = new System.Drawing.Size(168, 20);
			comboBoxDefendant.TabIndex = 8;
			comboBoxDefendant.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxClientName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxClientName.CustomReportFieldName = "";
			textBoxClientName.CustomReportKey = "";
			textBoxClientName.CustomReportValueType = 1;
			textBoxClientName.IsComboTextBox = false;
			textBoxClientName.IsModified = false;
			textBoxClientName.Location = new System.Drawing.Point(273, 98);
			textBoxClientName.MaxLength = 64;
			textBoxClientName.Name = "textBoxClientName";
			textBoxClientName.ReadOnly = true;
			textBoxClientName.Size = new System.Drawing.Size(391, 20);
			textBoxClientName.TabIndex = 138;
			textBoxClientName.TabStop = false;
			textboxFileNo.BackColor = System.Drawing.Color.WhiteSmoke;
			textboxFileNo.CustomReportFieldName = "";
			textboxFileNo.CustomReportKey = "";
			textboxFileNo.CustomReportValueType = 1;
			textboxFileNo.IsComboTextBox = false;
			textboxFileNo.IsModified = false;
			textboxFileNo.Location = new System.Drawing.Point(102, 75);
			textboxFileNo.MaxLength = 64;
			textboxFileNo.Name = "textboxFileNo";
			textboxFileNo.ReadOnly = true;
			textboxFileNo.Size = new System.Drawing.Size(259, 20);
			textboxFileNo.TabIndex = 7;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(8, 79);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(51, 13);
			mmLabel2.TabIndex = 172;
			mmLabel2.Text = "File No:";
			textBoxParentCode.BackColor = System.Drawing.Color.White;
			textBoxParentCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxParentCode.CustomReportFieldName = "";
			textBoxParentCode.CustomReportKey = "";
			textBoxParentCode.CustomReportValueType = 1;
			textBoxParentCode.IsComboTextBox = false;
			textBoxParentCode.IsModified = false;
			textBoxParentCode.Location = new System.Drawing.Point(534, 74);
			textBoxParentCode.MaxLength = 15;
			textBoxParentCode.Name = "textBoxParentCode";
			textBoxParentCode.Size = new System.Drawing.Size(129, 20);
			textBoxParentCode.TabIndex = 170;
			textBoxParentCode.Visible = false;
			comboBoxParentsysDoc.Assigned = false;
			comboBoxParentsysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxParentsysDoc.CustomReportFieldName = "";
			comboBoxParentsysDoc.CustomReportKey = "";
			comboBoxParentsysDoc.CustomReportValueType = 1;
			comboBoxParentsysDoc.DescriptionTextBox = null;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxParentsysDoc.DisplayLayout.Appearance = appearance55;
			comboBoxParentsysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxParentsysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance56.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxParentsysDoc.DisplayLayout.GroupByBox.Appearance = appearance56;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxParentsysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance57;
			comboBoxParentsysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance58.BackColor2 = System.Drawing.SystemColors.Control;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxParentsysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance58;
			comboBoxParentsysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxParentsysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxParentsysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance59;
			appearance60.BackColor = System.Drawing.SystemColors.Highlight;
			appearance60.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxParentsysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance60;
			comboBoxParentsysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxParentsysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			comboBoxParentsysDoc.DisplayLayout.Override.CardAreaAppearance = appearance61;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			appearance62.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxParentsysDoc.DisplayLayout.Override.CellAppearance = appearance62;
			comboBoxParentsysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxParentsysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance63.BackColor = System.Drawing.SystemColors.Control;
			appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance63.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance63.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxParentsysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance63;
			appearance64.TextHAlignAsString = "Left";
			comboBoxParentsysDoc.DisplayLayout.Override.HeaderAppearance = appearance64;
			comboBoxParentsysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxParentsysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			comboBoxParentsysDoc.DisplayLayout.Override.RowAppearance = appearance65;
			comboBoxParentsysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxParentsysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance66;
			comboBoxParentsysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxParentsysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxParentsysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxParentsysDoc.DivisionID = "";
			comboBoxParentsysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxParentsysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxParentsysDoc.Editable = true;
			comboBoxParentsysDoc.ExcludeFromSecurity = false;
			comboBoxParentsysDoc.FilterString = "";
			comboBoxParentsysDoc.HasAllAccount = false;
			comboBoxParentsysDoc.HasCustom = false;
			comboBoxParentsysDoc.IsDataLoaded = false;
			comboBoxParentsysDoc.Location = new System.Drawing.Point(416, 74);
			comboBoxParentsysDoc.MaxDropDownItems = 12;
			comboBoxParentsysDoc.Name = "comboBoxParentsysDoc";
			comboBoxParentsysDoc.ShowAll = false;
			comboBoxParentsysDoc.ShowInactiveItems = false;
			comboBoxParentsysDoc.ShowQuickAdd = true;
			comboBoxParentsysDoc.Size = new System.Drawing.Size(116, 20);
			comboBoxParentsysDoc.TabIndex = 169;
			comboBoxParentsysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxParentsysDoc.Visible = false;
			appearance67.FontData.BoldAsString = "True";
			appearance67.FontData.Name = "Tahoma";
			labelStatus.Appearance = appearance67;
			labelStatus.AutoSize = true;
			labelStatus.Location = new System.Drawing.Point(516, 55);
			labelStatus.Name = "labelStatus";
			labelStatus.Size = new System.Drawing.Size(44, 15);
			labelStatus.TabIndex = 165;
			labelStatus.TabStop = true;
			labelStatus.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelStatus.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelStatus.Value = "Status:";
			labelStatus.Visible = false;
			appearance68.ForeColor = System.Drawing.Color.Blue;
			labelStatus.VisitedLinkAppearance = appearance68;
			labelStatus.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked_1);
			comboBoxLegalStatus.Assigned = false;
			comboBoxLegalStatus.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLegalStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLegalStatus.CustomReportFieldName = "";
			comboBoxLegalStatus.CustomReportKey = "";
			comboBoxLegalStatus.CustomReportValueType = 1;
			comboBoxLegalStatus.DescriptionTextBox = null;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLegalStatus.DisplayLayout.Appearance = appearance69;
			comboBoxLegalStatus.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLegalStatus.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance70.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance70.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLegalStatus.DisplayLayout.GroupByBox.Appearance = appearance70;
			appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLegalStatus.DisplayLayout.GroupByBox.BandLabelAppearance = appearance71;
			comboBoxLegalStatus.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance72.BackColor2 = System.Drawing.SystemColors.Control;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLegalStatus.DisplayLayout.GroupByBox.PromptAppearance = appearance72;
			comboBoxLegalStatus.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLegalStatus.DisplayLayout.MaxRowScrollRegions = 1;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLegalStatus.DisplayLayout.Override.ActiveCellAppearance = appearance73;
			appearance74.BackColor = System.Drawing.SystemColors.Highlight;
			appearance74.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLegalStatus.DisplayLayout.Override.ActiveRowAppearance = appearance74;
			comboBoxLegalStatus.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLegalStatus.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLegalStatus.DisplayLayout.Override.CardAreaAppearance = appearance75;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			appearance76.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLegalStatus.DisplayLayout.Override.CellAppearance = appearance76;
			comboBoxLegalStatus.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLegalStatus.DisplayLayout.Override.CellPadding = 0;
			appearance77.BackColor = System.Drawing.SystemColors.Control;
			appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance77.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLegalStatus.DisplayLayout.Override.GroupByRowAppearance = appearance77;
			appearance78.TextHAlignAsString = "Left";
			comboBoxLegalStatus.DisplayLayout.Override.HeaderAppearance = appearance78;
			comboBoxLegalStatus.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLegalStatus.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.Color.Silver;
			comboBoxLegalStatus.DisplayLayout.Override.RowAppearance = appearance79;
			comboBoxLegalStatus.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLegalStatus.DisplayLayout.Override.TemplateAddRowAppearance = appearance80;
			comboBoxLegalStatus.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLegalStatus.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLegalStatus.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLegalStatus.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLegalStatus.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxLegalStatus.Editable = true;
			comboBoxLegalStatus.FilterString = "";
			comboBoxLegalStatus.GenericListType = Micromind.Common.Data.GenericListTypes.ActionStatus;
			comboBoxLegalStatus.HasAllAccount = false;
			comboBoxLegalStatus.HasCustom = false;
			comboBoxLegalStatus.IsDataLoaded = false;
			comboBoxLegalStatus.IsSingleColumn = false;
			comboBoxLegalStatus.Location = new System.Drawing.Point(563, 53);
			comboBoxLegalStatus.MaxDropDownItems = 12;
			comboBoxLegalStatus.Name = "comboBoxLegalStatus";
			comboBoxLegalStatus.ShowInactiveItems = false;
			comboBoxLegalStatus.ShowQuickAdd = true;
			comboBoxLegalStatus.Size = new System.Drawing.Size(100, 20);
			comboBoxLegalStatus.TabIndex = 6;
			comboBoxLegalStatus.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLegalStatus.Visible = false;
			appearance81.BackColor = System.Drawing.Color.WhiteSmoke;
			appearance81.FontData.BoldAsString = "True";
			appearance81.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance81;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(8, 100);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(66, 15);
			ultraFormattedLinkLabel1.TabIndex = 151;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Defendant:";
			appearance82.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance82;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance83.BackColor = System.Drawing.Color.WhiteSmoke;
			appearance83.FontData.BoldAsString = "True";
			appearance83.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance83;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(8, 11);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 150;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance84.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance84;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked_1);
			appearance85.FontData.BoldAsString = "True";
			appearance85.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance85;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(224, 12);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabel2.TabIndex = 149;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Doc Number:";
			appearance86.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance86;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance87;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance88.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance88.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance88.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance88;
			appearance89.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance89;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance90.BackColor2 = System.Drawing.SystemColors.Control;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance90.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance90;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance91;
			appearance92.BackColor = System.Drawing.SystemColors.Highlight;
			appearance92.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance92;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance93;
			appearance94.BorderColor = System.Drawing.Color.Silver;
			appearance94.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance94;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance95.BackColor = System.Drawing.SystemColors.Control;
			appearance95.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance95.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance95.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance95;
			appearance96.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance96;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance97;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance98;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(102, 9);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(116, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(488, 9);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(85, 20);
			dateTimePickerDate.TabIndex = 2;
			labelCloseDate.AutoSize = true;
			labelCloseDate.BackColor = System.Drawing.Color.Transparent;
			labelCloseDate.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCloseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelCloseDate.IsFieldHeader = false;
			labelCloseDate.IsRequired = false;
			labelCloseDate.Location = new System.Drawing.Point(449, 9);
			labelCloseDate.Name = "labelCloseDate";
			labelCloseDate.PenWidth = 1f;
			labelCloseDate.ShowBorder = false;
			labelCloseDate.Size = new System.Drawing.Size(33, 13);
			labelCloseDate.TabIndex = 148;
			labelCloseDate.Text = "Date:";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 146;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			formManager.Click += new System.EventHandler(formManager_Click);
			textBoxActionName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxActionName.CustomReportFieldName = "";
			textBoxActionName.CustomReportKey = "";
			textBoxActionName.CustomReportValueType = 1;
			textBoxActionName.IsComboTextBox = false;
			textBoxActionName.IsModified = false;
			textBoxActionName.Location = new System.Drawing.Point(102, 53);
			textBoxActionName.MaxLength = 64;
			textBoxActionName.Name = "textBoxActionName";
			textBoxActionName.ReadOnly = true;
			textBoxActionName.Size = new System.Drawing.Size(259, 20);
			textBoxActionName.TabIndex = 4;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(316, 9);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(129, 20);
			textBoxCode.TabIndex = 1;
			labelName.AutoSize = true;
			labelName.BackColor = System.Drawing.Color.Transparent;
			labelName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelName.IsFieldHeader = false;
			labelName.IsRequired = true;
			labelName.Location = new System.Drawing.Point(8, 57);
			labelName.Name = "labelName";
			labelName.PenWidth = 1f;
			labelName.ShowBorder = false;
			labelName.Size = new System.Drawing.Size(83, 13);
			labelName.TabIndex = 135;
			labelName.Text = "Action Name:";
			ultraTabPageControl2.Controls.Add(mmLabel25);
			ultraTabPageControl2.Controls.Add(button1);
			ultraTabPageControl2.Controls.Add(comboBoxFollowupPeriod);
			ultraTabPageControl2.Controls.Add(buttonAddActivity);
			ultraTabPageControl2.Controls.Add(dataGridListFollowup);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(691, 348);
			mmLabel25.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(480, 6);
			mmLabel25.Name = "mmLabel25";
			mmLabel25.PenWidth = 1f;
			mmLabel25.ShowBorder = false;
			mmLabel25.Size = new System.Drawing.Size(41, 13);
			mmLabel25.TabIndex = 366;
			mmLabel25.Text = "Period:";
			button1.Image = Micromind.ClientUI.Properties.Resources.add;
			button1.Location = new System.Drawing.Point(2, 1);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(23, 22);
			button1.TabIndex = 365;
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			comboBoxFollowupPeriod.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			comboBoxFollowupPeriod.Location = new System.Drawing.Point(527, 3);
			comboBoxFollowupPeriod.Name = "comboBoxFollowupPeriod";
			comboBoxFollowupPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboBoxFollowupPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxFollowupPeriod.Size = new System.Drawing.Size(152, 20);
			comboBoxFollowupPeriod.TabIndex = 364;
			comboBoxFollowupPeriod.SelectedIndexChanged += new System.EventHandler(comboBoxFollowupPeriod_SelectedIndexChanged);
			buttonAddActivity.Image = Micromind.ClientUI.Properties.Resources.add;
			buttonAddActivity.Location = new System.Drawing.Point(0, -60);
			buttonAddActivity.Name = "buttonAddActivity";
			buttonAddActivity.Size = new System.Drawing.Size(23, 22);
			buttonAddActivity.TabIndex = 362;
			buttonAddActivity.UseVisualStyleBackColor = true;
			dataGridListFollowup.AllowUnfittedView = false;
			dataGridListFollowup.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridListFollowup.DisplayLayout.Appearance = appearance99;
			dataGridListFollowup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridListFollowup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance100.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance100.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance100.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance100.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.GroupByBox.Appearance = appearance100;
			appearance101.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance101;
			dataGridListFollowup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance102.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance102.BackColor2 = System.Drawing.SystemColors.Control;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance102.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.PromptAppearance = appearance102;
			dataGridListFollowup.DisplayLayout.MaxColScrollRegions = 1;
			dataGridListFollowup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridListFollowup.DisplayLayout.Override.ActiveCellAppearance = appearance103;
			appearance104.BackColor = System.Drawing.SystemColors.Highlight;
			appearance104.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridListFollowup.DisplayLayout.Override.ActiveRowAppearance = appearance104;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.CardAreaAppearance = appearance105;
			appearance106.BorderColor = System.Drawing.Color.Silver;
			appearance106.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridListFollowup.DisplayLayout.Override.CellAppearance = appearance106;
			dataGridListFollowup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridListFollowup.DisplayLayout.Override.CellPadding = 0;
			appearance107.BackColor = System.Drawing.SystemColors.Control;
			appearance107.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance107.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance107.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance107.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.GroupByRowAppearance = appearance107;
			appearance108.TextHAlignAsString = "Left";
			dataGridListFollowup.DisplayLayout.Override.HeaderAppearance = appearance108;
			dataGridListFollowup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridListFollowup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.BorderColor = System.Drawing.Color.Silver;
			dataGridListFollowup.DisplayLayout.Override.RowAppearance = appearance109;
			dataGridListFollowup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance110.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridListFollowup.DisplayLayout.Override.TemplateAddRowAppearance = appearance110;
			dataGridListFollowup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridListFollowup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridListFollowup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridListFollowup.LoadLayoutFailed = false;
			dataGridListFollowup.Location = new System.Drawing.Point(2, 27);
			dataGridListFollowup.Name = "dataGridListFollowup";
			dataGridListFollowup.ShowDeleteMenu = false;
			dataGridListFollowup.ShowMinusInRed = true;
			dataGridListFollowup.ShowNewMenu = false;
			dataGridListFollowup.Size = new System.Drawing.Size(677, 292);
			dataGridListFollowup.TabIndex = 363;
			dataGridListFollowup.Text = "dataGridList1";
			ultraTabPageControl3.Controls.Add(mmLabel1);
			ultraTabPageControl3.Controls.Add(comboBoxAnalysis);
			ultraTabPageControl3.Controls.Add(dataGridList);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(691, 348);
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(3, 8);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(76, 13);
			mmLabel1.TabIndex = 170;
			mmLabel1.Text = "Analysis Code:";
			mmLabel1.Visible = false;
			comboBoxAnalysis.Assigned = false;
			comboBoxAnalysis.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAnalysis.CustomReportFieldName = "";
			comboBoxAnalysis.CustomReportKey = "";
			comboBoxAnalysis.CustomReportValueType = 1;
			comboBoxAnalysis.DescriptionTextBox = null;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAnalysis.DisplayLayout.Appearance = appearance111;
			comboBoxAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance112.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance112.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance112.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.GroupByBox.Appearance = appearance112;
			appearance113.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance113;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance114.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance114.BackColor2 = System.Drawing.SystemColors.Control;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance114.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance114;
			comboBoxAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance115;
			appearance116.BackColor = System.Drawing.SystemColors.Highlight;
			appearance116.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance116;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance117;
			appearance118.BorderColor = System.Drawing.Color.Silver;
			appearance118.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAnalysis.DisplayLayout.Override.CellAppearance = appearance118;
			comboBoxAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance119.BackColor = System.Drawing.SystemColors.Control;
			appearance119.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance119.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance119.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance119.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance119;
			appearance120.TextHAlignAsString = "Left";
			comboBoxAnalysis.DisplayLayout.Override.HeaderAppearance = appearance120;
			comboBoxAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.BorderColor = System.Drawing.Color.Silver;
			comboBoxAnalysis.DisplayLayout.Override.RowAppearance = appearance121;
			comboBoxAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance122.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance122;
			comboBoxAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAnalysis.Editable = true;
			comboBoxAnalysis.FilterString = "";
			comboBoxAnalysis.HasAllAccount = false;
			comboBoxAnalysis.HasCustom = false;
			comboBoxAnalysis.IsDataLoaded = false;
			comboBoxAnalysis.Location = new System.Drawing.Point(79, 3);
			comboBoxAnalysis.MaxDropDownItems = 12;
			comboBoxAnalysis.Name = "comboBoxAnalysis";
			comboBoxAnalysis.ReadOnly = true;
			comboBoxAnalysis.ShowInactiveItems = false;
			comboBoxAnalysis.ShowQuickAdd = true;
			comboBoxAnalysis.Size = new System.Drawing.Size(168, 20);
			comboBoxAnalysis.TabIndex = 169;
			comboBoxAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAnalysis.Visible = false;
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			appearance123.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance123;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance124.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance124.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance124.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance124;
			appearance125.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance125;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance126.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance126.BackColor2 = System.Drawing.SystemColors.Control;
			appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance126.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance126;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance127;
			appearance128.BackColor = System.Drawing.SystemColors.Highlight;
			appearance128.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance128;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance129;
			appearance130.BorderColor = System.Drawing.Color.Silver;
			appearance130.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance130;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance131.BackColor = System.Drawing.SystemColors.Control;
			appearance131.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance131.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance131.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance131;
			appearance132.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance132;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance133;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance134.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance134;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(-2, 26);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(690, 319);
			dataGridList.TabIndex = 2;
			dataGridList.Text = "dataGridList1";
			groupBox1.Controls.Add(ultraFormattedLinkLabel3);
			groupBox1.Controls.Add(mmLabel4);
			groupBox1.Controls.Add(textBoxNote);
			groupBox1.Controls.Add(textBoxCaseParty);
			groupBox1.Controls.Add(comboBoxCaseParty);
			groupBox1.Controls.Add(ultraFormattedLinkLabel4);
			groupBox1.Controls.Add(textBoxCaseType);
			groupBox1.Controls.Add(ComboBoxLawyer);
			groupBox1.Controls.Add(ultraFormattedLinkLabel7);
			groupBox1.Controls.Add(textBoxLawyer);
			groupBox1.Controls.Add(comboBoxCaseType);
			groupBox1.Location = new System.Drawing.Point(342, 5);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(244, 28);
			groupBox1.TabIndex = 174;
			groupBox1.TabStop = false;
			groupBox1.Text = "groupBox1";
			groupBox1.Visible = false;
			appearance135.FontData.BoldAsString = "False";
			appearance135.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance135;
			ultraFormattedLinkLabel3.AutoSize = true;
			appearance136.BackColor = System.Drawing.Color.White;
			appearance136.BackColor2 = System.Drawing.Color.White;
			ultraFormattedLinkLabel3.LinkAppearance = appearance136;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(6, 19);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(75, 15);
			ultraFormattedLinkLabel3.TabIndex = 163;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Judicial Entity:";
			appearance137.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance137;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked_2);
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(4, 87);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(63, 13);
			mmLabel4.TabIndex = 142;
			mmLabel4.Text = "Description:";
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(100, 84);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(112, 0);
			textBoxNote.TabIndex = 10;
			textBoxCaseParty.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCaseParty.CustomReportFieldName = "";
			textBoxCaseParty.CustomReportKey = "";
			textBoxCaseParty.CustomReportValueType = 1;
			textBoxCaseParty.IsComboTextBox = false;
			textBoxCaseParty.IsModified = false;
			textBoxCaseParty.Location = new System.Drawing.Point(162, 16);
			textBoxCaseParty.MaxLength = 64;
			textBoxCaseParty.Name = "textBoxCaseParty";
			textBoxCaseParty.ReadOnly = true;
			textBoxCaseParty.Size = new System.Drawing.Size(77, 20);
			textBoxCaseParty.TabIndex = 140;
			textBoxCaseParty.TabStop = false;
			comboBoxCaseParty.Assigned = false;
			comboBoxCaseParty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCaseParty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCaseParty.CustomReportFieldName = "";
			comboBoxCaseParty.CustomReportKey = "";
			comboBoxCaseParty.CustomReportValueType = 1;
			comboBoxCaseParty.DescriptionTextBox = textBoxCaseParty;
			appearance138.BackColor = System.Drawing.SystemColors.Window;
			appearance138.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCaseParty.DisplayLayout.Appearance = appearance138;
			comboBoxCaseParty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCaseParty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance139.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance139.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance139.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance139.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCaseParty.DisplayLayout.GroupByBox.Appearance = appearance139;
			appearance140.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCaseParty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance140;
			comboBoxCaseParty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance141.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance141.BackColor2 = System.Drawing.SystemColors.Control;
			appearance141.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance141.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCaseParty.DisplayLayout.GroupByBox.PromptAppearance = appearance141;
			comboBoxCaseParty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCaseParty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance142.BackColor = System.Drawing.SystemColors.Window;
			appearance142.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCaseParty.DisplayLayout.Override.ActiveCellAppearance = appearance142;
			appearance143.BackColor = System.Drawing.SystemColors.Highlight;
			appearance143.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCaseParty.DisplayLayout.Override.ActiveRowAppearance = appearance143;
			comboBoxCaseParty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCaseParty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance144.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCaseParty.DisplayLayout.Override.CardAreaAppearance = appearance144;
			appearance145.BorderColor = System.Drawing.Color.Silver;
			appearance145.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCaseParty.DisplayLayout.Override.CellAppearance = appearance145;
			comboBoxCaseParty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCaseParty.DisplayLayout.Override.CellPadding = 0;
			appearance146.BackColor = System.Drawing.SystemColors.Control;
			appearance146.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance146.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance146.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCaseParty.DisplayLayout.Override.GroupByRowAppearance = appearance146;
			appearance147.TextHAlignAsString = "Left";
			comboBoxCaseParty.DisplayLayout.Override.HeaderAppearance = appearance147;
			comboBoxCaseParty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCaseParty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance148.BackColor = System.Drawing.SystemColors.Window;
			appearance148.BorderColor = System.Drawing.Color.Silver;
			comboBoxCaseParty.DisplayLayout.Override.RowAppearance = appearance148;
			comboBoxCaseParty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance149.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCaseParty.DisplayLayout.Override.TemplateAddRowAppearance = appearance149;
			comboBoxCaseParty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCaseParty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCaseParty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCaseParty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCaseParty.Editable = true;
			comboBoxCaseParty.FilterString = "";
			comboBoxCaseParty.HasAllAccount = false;
			comboBoxCaseParty.HasCustom = false;
			comboBoxCaseParty.IsDataLoaded = false;
			comboBoxCaseParty.Location = new System.Drawing.Point(100, 16);
			comboBoxCaseParty.MaxDropDownItems = 12;
			comboBoxCaseParty.Name = "comboBoxCaseParty";
			comboBoxCaseParty.ShowInactiveItems = false;
			comboBoxCaseParty.ShowQuickAdd = true;
			comboBoxCaseParty.Size = new System.Drawing.Size(56, 20);
			comboBoxCaseParty.TabIndex = 7;
			comboBoxCaseParty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance150.FontData.BoldAsString = "False";
			appearance150.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance150;
			ultraFormattedLinkLabel4.AutoSize = true;
			appearance151.BackColor = System.Drawing.Color.White;
			appearance151.BackColor2 = System.Drawing.Color.White;
			ultraFormattedLinkLabel4.LinkAppearance = appearance151;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(6, 41);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(43, 15);
			ultraFormattedLinkLabel4.TabIndex = 160;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Lawyer:";
			appearance152.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance152;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked_1);
			textBoxCaseType.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCaseType.CustomReportFieldName = "";
			textBoxCaseType.CustomReportKey = "";
			textBoxCaseType.CustomReportValueType = 1;
			textBoxCaseType.IsComboTextBox = false;
			textBoxCaseType.IsModified = false;
			textBoxCaseType.Location = new System.Drawing.Point(162, 61);
			textBoxCaseType.MaxLength = 64;
			textBoxCaseType.Name = "textBoxCaseType";
			textBoxCaseType.ReadOnly = true;
			textBoxCaseType.Size = new System.Drawing.Size(77, 20);
			textBoxCaseType.TabIndex = 168;
			textBoxCaseType.TabStop = false;
			ComboBoxLawyer.Assigned = false;
			ComboBoxLawyer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxLawyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxLawyer.CustomReportFieldName = "";
			ComboBoxLawyer.CustomReportKey = "";
			ComboBoxLawyer.CustomReportValueType = 1;
			ComboBoxLawyer.DescriptionTextBox = textBoxLawyer;
			appearance153.BackColor = System.Drawing.SystemColors.Window;
			appearance153.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxLawyer.DisplayLayout.Appearance = appearance153;
			ComboBoxLawyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxLawyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance154.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance154.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance154.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance154.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxLawyer.DisplayLayout.GroupByBox.Appearance = appearance154;
			appearance155.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxLawyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance155;
			ComboBoxLawyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance156.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance156.BackColor2 = System.Drawing.SystemColors.Control;
			appearance156.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance156.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxLawyer.DisplayLayout.GroupByBox.PromptAppearance = appearance156;
			ComboBoxLawyer.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxLawyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxLawyer.DisplayLayout.Override.ActiveCellAppearance = appearance157;
			appearance158.BackColor = System.Drawing.SystemColors.Highlight;
			appearance158.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxLawyer.DisplayLayout.Override.ActiveRowAppearance = appearance158;
			ComboBoxLawyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxLawyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance159.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxLawyer.DisplayLayout.Override.CardAreaAppearance = appearance159;
			appearance160.BorderColor = System.Drawing.Color.Silver;
			appearance160.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxLawyer.DisplayLayout.Override.CellAppearance = appearance160;
			ComboBoxLawyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxLawyer.DisplayLayout.Override.CellPadding = 0;
			appearance161.BackColor = System.Drawing.SystemColors.Control;
			appearance161.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance161.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance161.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance161.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxLawyer.DisplayLayout.Override.GroupByRowAppearance = appearance161;
			appearance162.TextHAlignAsString = "Left";
			ComboBoxLawyer.DisplayLayout.Override.HeaderAppearance = appearance162;
			ComboBoxLawyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxLawyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			appearance163.BorderColor = System.Drawing.Color.Silver;
			ComboBoxLawyer.DisplayLayout.Override.RowAppearance = appearance163;
			ComboBoxLawyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance164.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxLawyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance164;
			ComboBoxLawyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxLawyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxLawyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxLawyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxLawyer.Editable = true;
			ComboBoxLawyer.FilterString = "";
			ComboBoxLawyer.HasAllAccount = false;
			ComboBoxLawyer.HasCustom = false;
			ComboBoxLawyer.IsDataLoaded = false;
			ComboBoxLawyer.Location = new System.Drawing.Point(100, 39);
			ComboBoxLawyer.MaxDropDownItems = 12;
			ComboBoxLawyer.Name = "ComboBoxLawyer";
			ComboBoxLawyer.ShowInactiveItems = false;
			ComboBoxLawyer.ShowQuickAdd = true;
			ComboBoxLawyer.Size = new System.Drawing.Size(56, 20);
			ComboBoxLawyer.TabIndex = 8;
			ComboBoxLawyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxLawyer.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLawyer.CustomReportFieldName = "";
			textBoxLawyer.CustomReportKey = "";
			textBoxLawyer.CustomReportValueType = 1;
			textBoxLawyer.IsComboTextBox = false;
			textBoxLawyer.IsModified = false;
			textBoxLawyer.Location = new System.Drawing.Point(162, 39);
			textBoxLawyer.MaxLength = 64;
			textBoxLawyer.Name = "textBoxLawyer";
			textBoxLawyer.ReadOnly = true;
			textBoxLawyer.Size = new System.Drawing.Size(77, 20);
			textBoxLawyer.TabIndex = 162;
			textBoxLawyer.TabStop = false;
			appearance165.FontData.BoldAsString = "False";
			appearance165.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance165;
			ultraFormattedLinkLabel7.AutoSize = true;
			appearance166.BackColor = System.Drawing.Color.White;
			appearance166.BackColor2 = System.Drawing.Color.White;
			ultraFormattedLinkLabel7.LinkAppearance = appearance166;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(5, 63);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(59, 15);
			ultraFormattedLinkLabel7.TabIndex = 167;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Case Type:";
			appearance167.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance167;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked_1);
			comboBoxCaseType.Assigned = false;
			comboBoxCaseType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCaseType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCaseType.CustomReportFieldName = "";
			comboBoxCaseType.CustomReportKey = "";
			comboBoxCaseType.CustomReportValueType = 1;
			comboBoxCaseType.DescriptionTextBox = textBoxCaseType;
			appearance168.BackColor = System.Drawing.SystemColors.Window;
			appearance168.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCaseType.DisplayLayout.Appearance = appearance168;
			comboBoxCaseType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCaseType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance169.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance169.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance169.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance169.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCaseType.DisplayLayout.GroupByBox.Appearance = appearance169;
			appearance170.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCaseType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance170;
			comboBoxCaseType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance171.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance171.BackColor2 = System.Drawing.SystemColors.Control;
			appearance171.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance171.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCaseType.DisplayLayout.GroupByBox.PromptAppearance = appearance171;
			comboBoxCaseType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCaseType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance172.BackColor = System.Drawing.SystemColors.Window;
			appearance172.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCaseType.DisplayLayout.Override.ActiveCellAppearance = appearance172;
			appearance173.BackColor = System.Drawing.SystemColors.Highlight;
			appearance173.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCaseType.DisplayLayout.Override.ActiveRowAppearance = appearance173;
			comboBoxCaseType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCaseType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance174.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCaseType.DisplayLayout.Override.CardAreaAppearance = appearance174;
			appearance175.BorderColor = System.Drawing.Color.Silver;
			appearance175.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCaseType.DisplayLayout.Override.CellAppearance = appearance175;
			comboBoxCaseType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCaseType.DisplayLayout.Override.CellPadding = 0;
			appearance176.BackColor = System.Drawing.SystemColors.Control;
			appearance176.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance176.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance176.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance176.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCaseType.DisplayLayout.Override.GroupByRowAppearance = appearance176;
			appearance177.TextHAlignAsString = "Left";
			comboBoxCaseType.DisplayLayout.Override.HeaderAppearance = appearance177;
			comboBoxCaseType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCaseType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance178.BackColor = System.Drawing.SystemColors.Window;
			appearance178.BorderColor = System.Drawing.Color.Silver;
			comboBoxCaseType.DisplayLayout.Override.RowAppearance = appearance178;
			comboBoxCaseType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance179.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCaseType.DisplayLayout.Override.TemplateAddRowAppearance = appearance179;
			comboBoxCaseType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCaseType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCaseType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCaseType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCaseType.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxCaseType.Editable = true;
			comboBoxCaseType.FilterString = "";
			comboBoxCaseType.GenericListType = Micromind.Common.Data.GenericListTypes.CaseType;
			comboBoxCaseType.HasAllAccount = false;
			comboBoxCaseType.HasCustom = false;
			comboBoxCaseType.IsDataLoaded = false;
			comboBoxCaseType.IsSingleColumn = false;
			comboBoxCaseType.Location = new System.Drawing.Point(100, 61);
			comboBoxCaseType.MaxDropDownItems = 12;
			comboBoxCaseType.Name = "comboBoxCaseType";
			comboBoxCaseType.ShowInactiveItems = false;
			comboBoxCaseType.ShowQuickAdd = true;
			comboBoxCaseType.Size = new System.Drawing.Size(56, 20);
			comboBoxCaseType.TabIndex = 9;
			comboBoxCaseType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[18]
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
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonComments,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripDropDownButton1,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(708, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonComments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonComments.Image = Micromind.ClientUI.Properties.Resources.comment;
			toolStripButtonComments.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonComments.Name = "toolStripButtonComments";
			toolStripButtonComments.Size = new System.Drawing.Size(28, 28);
			toolStripButtonComments.Text = "Comments...";
			toolStripButtonComments.Click += new System.EventHandler(toolStripButtonComments_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				createFromExistingActivityToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.Visible = false;
			createFromExistingActivityToolStripMenuItem.Name = "createFromExistingActivityToolStripMenuItem";
			createFromExistingActivityToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			createFromExistingActivityToolStripMenuItem.Text = "Create from Legal Actions";
			createFromExistingActivityToolStripMenuItem.Click += new System.EventHandler(createFromExistingActivityToolStripMenuItem_Click);
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
			panelButtons.Controls.Add(groupBox1);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 416);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(708, 40);
			panelButtons.TabIndex = 13;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(708, 1);
			linePanelDown.TabIndex = 1;
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
			xpButton1.Location = new System.Drawing.Point(598, 8);
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
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
			ultraTabControl1.Location = new System.Drawing.Point(6, 34);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(695, 371);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 131;
			appearance180.Cursor = System.Windows.Forms.Cursors.Hand;
			ultraTab.ActiveAppearance = appearance180;
			appearance181.BorderColor3DBase = System.Drawing.Color.Transparent;
			appearance181.Cursor = System.Windows.Forms.Cursors.Arrow;
			ultraTab.Appearance = appearance181;
			ultraTab.TabPage = ultraTabPageControl1;
			ultraTab.Text = "Activity";
			appearance182.Cursor = System.Windows.Forms.Cursors.Hand;
			ultraTab2.ActiveAppearance = appearance182;
			appearance183.BorderColor3DBase = System.Drawing.Color.Transparent;
			appearance183.Cursor = System.Windows.Forms.Cursors.Arrow;
			ultraTab2.Appearance = appearance183;
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "Follow Up";
			appearance184.BorderColor3DBase = System.Drawing.Color.Transparent;
			ultraTab3.Appearance = appearance184;
			ultraTab3.TabPage = ultraTabPageControl3;
			ultraTab3.Text = "Expenses";
			ultraTab3.Visible = false;
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3]
			{
				ultraTab,
				ultraTab2,
				ultraTab3
			});
			ultraTabControl1.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			ultraTabControl1.Click += new System.EventHandler(ultraTabControl1_Click);
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(691, 348);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(708, 456);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "LegalActivityDetailsForm";
			Text = "Legal Activity Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPlantiff).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxContact).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOwner).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefendant).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxParentsysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLegalStatus).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupPeriod.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridListFollowup).EndInit();
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCaseParty).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxLawyer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCaseType).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
