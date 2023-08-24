using DevExpress.Utils;
using DevExpress.XtraEditors;
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Legal
{
	public class LegalActionDetailsForm : Form, IForm
	{
		private LegalActionData currentData;

		private DataSet activityData;

		private const string TABLENAME_CONST = "Legal_Actions";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool EnableLegalAnalysis = CompanyPreferences.EnableLegalAnalysisCode;

		private DataSet companyInformation;

		private string parentRefNumber = "";

		private string sourceRefNumber = "";

		private bool isDefendant;

		private bool isPlantiff;

		private List<Tuple<string, string, string>> docs = new List<Tuple<string, string, string>>();

		private List<Tuple<string, string>> idList = new List<Tuple<string, string>>();

		private ScreenAccessRight screenRight;

		private DataSet followUpData;

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

		private DateTimePicker dateTimePickerDate;

		private MMLabel labelCloseDate;

		private MMTextBox textBoxClientName;

		private FormManager formManager;

		private MMTextBox textBoxName;

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

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

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

		private MMTextBox textboxFileNo;

		private MMLabel mmLabel2;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem createFromExistingActivityToolStripMenuItem;

		private CaseClientComboBox comboBoxDefendant;

		private UltraTabPageControl ultraTabPageControl4;

		private GadgetDateRangeComboBox comboBoxActivityPeriod;

		private DataGridList dataGridActivities;

		private Button button2;

		private LegalActionStatusComboBox comboBoxLegalActionStatus;

		private CaseClientComboBox comboBoxPlantiff;

		private MMTextBox mmTextBox1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private GroupBox groupBoxCaseParty;

		private MMTextBox textBoxNote;

		private MMLabel mmLabel9;

		private MMTextBox textBoxSourceFileNO;

		private MMLabel mmLabel7;

		private MMTextBox textBoxParentFileNo;

		private SysDocComboBox comboBoxSysDoc;

		private MMLabel mmLabel8;

		private MMLabel mmLabel6;

		private XPButton buttonSelectDocSource;

		private XPButton buttonSelectDocParent;

		private MMLabel mmLabel5;

		private MMLabel mmLabel3;

		private MMTextBox textBoxParentsysDoc;

		private MMTextBox textBoxParentVoucherID;

		private MMTextBox textBoxSourceSysDoc;

		private MMTextBox textBoxSourceVoucherID;

		private HyperlinkLabelControl hyperlinkLabelControlHistory;

		private ListBox listBoxCaseParty;

		private Button buttonPlantiffAddMore;

		private Button buttonDefendantAddMore;

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

		public LegalActionDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			companyInformation = Factory.CompanyInformationSystem.GetCompanyInformation();
			comboBoxAnalysis.FilterByAccount(companyInformation.Tables[0].Rows[0]["LegalAnalysisGroup"].ToString());
		}

		private void AddEvents()
		{
			base.Load += LegalActionDetailsForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dataGridActivities.DoubleClick += dataGridActivities_DoubleClick;
			comboBoxActivityPeriod.LoadData();
		}

		private void dataGridActivities_DoubleClick(object sender, EventArgs e)
		{
			int num = checked(dataGridActivities.Rows.Count - 1);
			num = dataGridActivities.ActiveRow.Index;
			string voucherID = dataGridActivities.Rows[num].Cells["VoucherID"].Value.ToString();
			string sysDocID = dataGridActivities.Rows[num].Cells["SysDocID"].Value.ToString();
			FormActivator.BringFormToFront(FormActivator.LegalActivityDetailsFormObj);
			FormActivator.LegalActivityDetailsFormObj.EditDocument(sysDocID, voucherID);
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
					currentData = new LegalActionData();
				}
				string text = "";
				string text2 = "";
				string text3 = "";
				AnalysisData analysisData = new AnalysisData();
				if (!EnableLegalAnalysis)
				{
					ErrorHelper.WarningMessage("Set AnalysisPrefix on Company settings");
					return false;
				}
				if (isNewRecord && EnableLegalAnalysis)
				{
					companyInformation = Factory.CompanyInformationSystem.GetCompanyInformation();
					text = companyInformation.Tables[0].Rows[0]["LegalAnalysisGroup"].ToString();
					text2 = companyInformation.Tables[0].Rows[0]["LegalAnalysisPrefix"].ToString();
					if (string.IsNullOrEmpty(text2) || string.IsNullOrWhiteSpace(text2))
					{
						ErrorHelper.WarningMessage("Set AnalysisPrefix on Company settings");
						return false;
					}
					text3 = text2 + "/" + comboBoxSysDoc.SelectedID.Trim() + "/" + textBoxCode.Text;
					DataRow dataRow = analysisData.AnalysisTable.NewRow();
					dataRow["AnalysisID"] = text3;
					dataRow["AnalysisName"] = text3;
					dataRow["GroupID"] = text;
					dataRow.EndEdit();
					analysisData.AnalysisTable.Rows.Add(dataRow);
				}
				DataRow dataRow2 = (!isNewRecord) ? currentData.LegalActionTable.Rows[0] : currentData.LegalActionTable.NewRow();
				dataRow2.BeginEdit();
				dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow2["VoucherID"] = textBoxCode.Text.Trim();
				dataRow2["ActionName"] = textBoxName.Text.Trim();
				dataRow2["FileNo"] = textboxFileNo.Text.Trim();
				dataRow2["VoucherID"] = textBoxCode.Text.Trim();
				dataRow2["ParentSysDocID"] = textBoxParentsysDoc.Text;
				dataRow2["ParentVoucherID"] = textBoxParentVoucherID.Text.Trim();
				dataRow2["SourceSysDocID"] = textBoxSourceSysDoc.Text;
				dataRow2["SourceVoucherID"] = textBoxSourceVoucherID.Text.Trim();
				if (comboBoxCaseParty.SelectedID != "")
				{
					dataRow2["CasePartyID"] = comboBoxCaseParty.SelectedID;
				}
				else
				{
					dataRow2["CasePartyID"] = DBNull.Value;
				}
				if (ComboBoxLawyer.SelectedID != "")
				{
					dataRow2["LawyerID"] = ComboBoxLawyer.SelectedID;
				}
				else
				{
					dataRow2["LawyerID"] = DBNull.Value;
				}
				if (!string.IsNullOrEmpty(comboBoxLegalActionStatus.SelectedID))
				{
					dataRow2["StatusID"] = comboBoxLegalActionStatus.SelectedID;
				}
				else
				{
					dataRow2["StatusID"] = DBNull.Value;
				}
				if (comboBoxCaseType.SelectedID != "")
				{
					dataRow2["CaseTypeID"] = comboBoxCaseType.SelectedID;
				}
				else
				{
					dataRow2["CaseTypeID"] = DBNull.Value;
				}
				if (comboBoxAnalysis.SelectedID != "")
				{
					dataRow2["AnalysisID"] = comboBoxAnalysis.SelectedID;
				}
				else if (text3 != "")
				{
					dataRow2["AnalysisID"] = text3;
				}
				else
				{
					dataRow2["AnalysisID"] = DBNull.Value;
				}
				if (comboBoxDefendant.SelectedID != "")
				{
					dataRow2["Caseclient1"] = comboBoxDefendant.SelectedID;
				}
				else
				{
					dataRow2["Caseclient1"] = DBNull.Value;
				}
				if (comboBoxPlantiff.SelectedID != "")
				{
					dataRow2["Caseclient2"] = comboBoxPlantiff.SelectedID;
				}
				else
				{
					dataRow2["Caseclient2"] = DBNull.Value;
				}
				dataRow2["ActionDateTime"] = dateTimePickerDate.Value.ToShortDateString();
				dataRow2["Note"] = textBoxNote.Text;
				if (isNewRecord)
				{
					currentData.LegalActionTable.Rows.Add(dataRow2);
				}
				dataRow2.EndEdit();
				currentData.LegalCaseClientListTable.Rows.Clear();
				int num = 0;
				if (docs.Count > 0)
				{
					foreach (Tuple<string, string, string> doc in docs)
					{
						DataRow dataRow3 = currentData.LegalCaseClientListTable.NewRow();
						dataRow3.BeginEdit();
						dataRow3["SysDocID"] = comboBoxSysDoc.SelectedID;
						dataRow3["VoucherID"] = textBoxCode.Text;
						dataRow3["Caseclient"] = doc.Item1;
						dataRow3["ClientType"] = doc.Item3;
						dataRow3["RowIndex"] = num;
						num = checked(num + 1);
						if (isNewRecord)
						{
							currentData.LegalCaseClientListTable.Rows.Add(dataRow3);
						}
					}
				}
				if (analysisData != null)
				{
					currentData.Merge(analysisData);
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

		public void AddNewActivity(CRMRelatedTypes type, string relatedID)
		{
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.LegalActionSystem.GetLegalActionByID(SystemDocID, id.Trim(), comboBoxAnalysis.SelectedID);
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables["Legal_Actions"].Rows.Count == 0)
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
						LoadActivities();
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
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.Tables[0].Rows[0];
			comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
			textBoxCode.Text = dataRow["VoucherID"].ToString();
			textBoxName.Text = dataRow["ActionName"].ToString();
			textBoxParentsysDoc.Text = dataRow["ParentSysDocID"].ToString();
			textBoxParentVoucherID.Text = dataRow["ParentVoucherID"].ToString();
			textBoxSourceSysDoc.Text = dataRow["SourceSysDocID"].ToString();
			textBoxSourceVoucherID.Text = dataRow["SourceVoucherID"].ToString();
			textboxFileNo.Text = dataRow["FileNo"].ToString();
			comboBoxDefendant.SelectedID = dataRow["Caseclient1"].ToString();
			comboBoxPlantiff.SelectedID = dataRow["Caseclient2"].ToString();
			ComboBoxLawyer.SelectedID = dataRow["LawyerID"].ToString();
			comboBoxCaseParty.SelectedID = dataRow["CasePartyID"].ToString();
			if (!string.IsNullOrEmpty(dataRow["StatusID"].ToString()))
			{
				comboBoxLegalActionStatus.SelectedID = dataRow["StatusID"].ToString();
			}
			comboBoxCaseType.SelectedID = dataRow["CaseTypeID"].ToString();
			DateTime dateTime = DateTime.Parse(dataRow["ActionDateTime"].ToString());
			dateTimePickerDate.Value = dateTime.Date;
			textBoxNote.Text = dataRow["Note"].ToString();
			if (dataRow["AnalysisID"] != DBNull.Value)
			{
				comboBoxAnalysis.SelectedID = dataRow["AnalysisID"].ToString();
			}
			else
			{
				comboBoxAnalysis.Clear();
			}
			(dataGridList.DataSource as DataTable).Rows.Clear();
			if (currentData.Tables.Contains("Legal_Actions_Client_List") && currentData.LegalCaseClientListTable.Rows.Count != 0)
			{
				foreach (DataRow row in currentData.LegalCaseClientListTable.Rows)
				{
					string text = row["Caseclient"].ToString();
					string text2 = row["ClientType"].ToString();
					string text3 = "";
					object obj2 = Factory.DatabaseSystem.GetFieldValue("Case_Client", "CaseClientName", "CaseClientID", text) ?? null;
					if (!string.IsNullOrEmpty(obj2.ToString()))
					{
						text3 = obj2.ToString();
					}
					listBoxCaseParty.Items.Add(text2 + ":- " + text + "-" + text3);
				}
				if (listBoxCaseParty.Items.Count > 0)
				{
					groupBoxCaseParty.Visible = true;
					listBoxCaseParty.Visible = true;
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
					flag = Factory.LegalActionSystem.CreateLegalAction(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Activity, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.LegalActionSystem.UpdateLegalAction(currentData);
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
			if (comboBoxSysDoc.SelectedID == "" || textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0 || textboxFileNo.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Legal_Actions", "VoucherID", textBoxCode.Text.Trim(), "SysDocID", SystemDocID))
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
			textBoxName.Clear();
			comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
			textBoxClientName.Clear();
			dateTimePickerDate.Value = DateTime.Now;
			comboBoxDefendant.Clear();
			ComboBoxLawyer.Clear();
			textBoxNote.Clear();
			comboBoxLegalActionStatus.Clear();
			comboBoxCaseParty.Clear();
			textBoxParentsysDoc.Clear();
			textBoxSourceSysDoc.Clear();
			textBoxParentVoucherID.Clear();
			textBoxSourceVoucherID.Clear();
			textboxFileNo.Clear();
			comboBoxCaseType.Clear();
			(dataGridActivities.DataSource as DataTable).Rows.Clear();
			(dataGridList.DataSource as DataTable).Rows.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
			comboBoxAnalysis.Clear();
			comboBoxPlantiff.Clear();
			comboBoxDefendant.Clear();
			groupBoxCaseParty.Visible = false;
			listBoxCaseParty.Items.Clear();
			textBoxParentFileNo.Clear();
			textBoxSourceFileNO.Clear();
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
			LoadData(DatabaseHelper.GetNextID("Legal_Actions", "VoucherID", textBoxCode.Text, "SysDocID", SystemDocID));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Legal_Actions", "VoucherID", textBoxCode.Text, "SysDocID", SystemDocID));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Legal_Actions", "VoucherID", "SysDocID", SystemDocID));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Legal_Actions", "VoucherID", "SysDocID", SystemDocID));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Legal_Actions", "VoucherID", toolStripTextBoxFind.Text.Trim(), "SysDocID", SystemDocID))
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

		private void LegalActionDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				dataGridListFollowup.ApplyUIDesign();
				SetupFollowGrid();
				dataGridList.ApplyUIDesign();
				SetupGrid();
				dataGridActivities.ApplyUIDesign();
				SetupActivityGrid();
				comboBoxFollowupPeriod.LoadData();
				comboBoxFollowupPeriod.SelectedIndex = 13;
				comboBoxDefendant.ShowDefendant = true;
				comboBoxPlantiff.ShowPlantiff = true;
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					comboBoxSysDoc.FilterByType(SysDocTypes.LegalAction);
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataGridActivities.LoadLayoutFailed = true;
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
			FormActivator.SelectedSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.BringFormToFront(FormActivator.LegalActionListObj);
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
				FormHelper.ShowDocumentInfo(textBoxCode.Text, SystemDocID, this);
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.LegalAction);
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.LegalAction);
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
			new FormHelper().EditLegalActionStatus(comboBoxLegalActionStatus.SelectedID);
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
					PrintHelper.PrintDocument(leaglActivityToPrint, selectedID, "Legal Activity", SysDocTypes.LegalAction, isPrint, showPrintDialog);
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
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string sysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string voucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			LegalActionData legalActionByID = Factory.LegalActionSystem.GetLegalActionByID(sysDocID, voucherID, comboBoxAnalysis.SelectedID);
			DataRow dataRow = legalActionByID.LegalActionTable.Rows[0];
			textBoxSourceSysDoc.Text = dataRow["VoucherID"].ToString();
			textBoxParentsysDoc.Text = dataRow["VoucherID"].ToString();
			comboBoxDefendant.SelectedID = dataRow["Caseclient1"].ToString();
			comboBoxPlantiff.SelectedID = dataRow["Caseclient2"].ToString();
			comboBoxCaseParty.SelectedID = dataRow["CasePartyID"].ToString();
			ComboBoxLawyer.SelectedID = dataRow["LawyerID"].ToString();
			comboBoxCaseType.SelectedID = dataRow["CaseTypeID"].ToString();
			textboxFileNo.Text = dataRow["FileNo"].ToString();
			if (dataRow["ParentSysDocID"].ToString() == "" || dataRow["ParentSysDocID"].ToString() == string.Empty)
			{
				parentRefNumber = "";
				textBoxParentsysDoc.Text = dataRow["SysDocID"].ToString();
				textBoxParentVoucherID.Text = dataRow["VoucherID"].ToString();
			}
			else
			{
				textBoxParentsysDoc.Text = dataRow["ParentSysDocID"].ToString();
				textBoxParentVoucherID.Text = dataRow["ParentVoucherID"].ToString();
				parentRefNumber = textBoxParentsysDoc.Text + "-" + textBoxSourceSysDoc.Text;
			}
			if (dataRow["SourceSysDocID"].ToString() == "" || dataRow["SourceVoucherID"].ToString() == string.Empty)
			{
				textBoxSourceSysDoc.Text = dataRow["SysDocID"].ToString();
				textBoxSourceVoucherID.Text = dataRow["VoucherID"].ToString();
				sourceRefNumber = textBoxParentVoucherID.Text + "-" + textBoxSourceVoucherID.Text;
			}
			else
			{
				textBoxSourceSysDoc.Text = dataRow["SourceSysDocID"].ToString();
				textBoxSourceVoucherID.Text = dataRow["SourceVoucherID"].ToString();
				sourceRefNumber = textBoxParentVoucherID.Text + "-" + textBoxSourceVoucherID.Text;
			}
			comboBoxAnalysis.SelectedID = dataRow["AnalysisID"].ToString();
			textBoxName.Text = dataRow["ActionName"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			if (legalActionByID.Tables.Contains("Legal_Actions_Client_List") && legalActionByID.Tables["Legal_Actions_Client_List"].Rows.Count > 0)
			{
				foreach (DataRow row in legalActionByID.LegalCaseClientListTable.Rows)
				{
					string text = row["Caseclient"].ToString();
					string text2 = row["ClientType"].ToString();
					string text3 = "";
					object obj2 = Factory.DatabaseSystem.GetFieldValue("Case_Client", "CaseClientName", "CaseClientID", text) ?? null;
					if (!string.IsNullOrEmpty(obj2.ToString()))
					{
						text3 = obj2.ToString();
					}
					listBoxCaseParty.Items.Add(text2 + " :- " + text + "-" + text3);
					docs.Add(new Tuple<string, string, string>(text, text3, text2));
				}
				if (listBoxCaseParty.Items.Count > 0)
				{
					groupBoxCaseParty.Visible = true;
					listBoxCaseParty.Visible = true;
				}
			}
		}

		private void ultraFormattedLinkLabel7_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditGenericList(GenericListTypes.CaseType, comboBoxCaseType.SelectedID);
		}

		private void ultraTabControl1_Click(object sender, EventArgs e)
		{
			if (ultraTabControl1.SelectedTab == ultraTabControl1.Tabs[3] && !isNewRecord)
			{
				LegalActionData legalActionExpenseByID = Factory.LegalActionSystem.GetLegalActionExpenseByID(SystemDocID, textBoxCode.Text, comboBoxAnalysis.SelectedID);
				if (legalActionExpenseByID != null && legalActionExpenseByID.Tables.Count != 0 && legalActionExpenseByID.Tables["Legal_Expense_Details"].Rows.Count != 0)
				{
					DataTable dataTable = dataGridList.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in legalActionExpenseByID.Tables["Legal_Expense_Details"].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						if (row["Amount"] != DBNull.Value)
						{
							dataRow2["SysDocID"] = row["SysDocID"];
							dataRow2["VoucherID"] = row["VoucherID"];
							dataRow2["Date"] = row["JournalDate"];
							dataRow2["Reference"] = row["Reference"];
							dataRow2["Amount"] = row["Amount"];
							dataRow2.EndEdit();
							dataTable.Rows.Add(dataRow2);
						}
					}
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormActivator.BringFormToFront(FormActivator.LegalActivityDetailsFormObj);
				FormActivator.LegalActivityDetailsFormObj.AddNewActivity(CRMRelatedTypes.LegalActivity, textBoxCode.Text, comboBoxSysDoc.Text, textBoxCode.Text);
			}
		}

		private void SetupActivityGrid()
		{
			try
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("SysDocID");
				dataTable.Columns.Add("VoucherID");
				dataTable.Columns.Add("ActivityName");
				dataTable.Columns.Add("ActivityDateTime");
				dataTable.Columns.Add("FileNo");
				dataTable.Columns.Add("Defendant");
				dataTable.Columns.Add("Plantiff");
				dataTable.Columns.Add("Activity By");
				dataTable.Columns.Add("ContactID");
				dataTable.Columns.Add("Done Date");
				dataTable.Columns.Add("Time");
				dataTable.Columns.Add("StatusID");
				dataGridActivities.DataSource = dataTable;
				dataGridActivities.DisplayLayout.Bands[0].Columns["Time"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Time;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void LoadActivities()
		{
			try
			{
				if (!isNewRecord)
				{
					activityData = Factory.LegalActivitySystem.GetLegalActivityListByLeadID(CRMRelatedTypes.LegalActivity, comboBoxSysDoc.SelectedID, textBoxCode.Text, comboBoxActivityPeriod.FromDate, comboBoxActivityPeriod.ToDate);
					DataTable dataTable = dataGridActivities.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in activityData.Tables["Legal_Activity"].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["SysDocID"] = row["Doc ID"];
						dataRow2["VoucherID"] = row["Number"];
						dataRow2["FileNo"] = row["FileNo"];
						dataRow2["ActivityName"] = row["Activity Name"];
						dataRow2["Defendant"] = row["Defendant"];
						dataRow2["Plantiff"] = row["Plantiff"];
						dataRow2["StatusID"] = row["Status"];
						dataRow2["ActivityDateTime"] = row["Date"];
						dataRow2["Activity By"] = row["Activity By"];
						dataRow2["ContactID"] = row["ContactID"];
						dataRow2["Done Date"] = row["ActDateTime"];
						dataRow2["Time"] = row["ActDateTime"];
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

		private void comboBoxActivityPeriod_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadActivities();
		}

		private void comboBoxDefendant_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxPlantiff_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCaseClient(comboBoxPlantiff.SelectedID.ToString());
		}

		private void buttonDefendantAddMore_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			new List<string>();
			dataSet = Factory.CaseClientSystem.GetCaseClientList(isDefendant: true, plantiff: false);
			SelectDialog selectDialog = new SelectDialog();
			selectDialog.DataSource = dataSet;
			selectDialog.IsMultiSelect = true;
			selectDialog.Text = "Select Case Clients";
			DialogResult num = selectDialog.ShowDialog(this);
			listBoxCaseParty.Items.Clear();
			if (num == DialogResult.OK)
			{
				foreach (UltraGridRow selectedRow in selectDialog.SelectedRows)
				{
					string item = selectedRow.Cells["Code"].Value.ToString();
					string item2 = selectedRow.Cells["Name"].Value.ToString();
					docs.Add(new Tuple<string, string, string>(item, item2, "D"));
				}
				foreach (Tuple<string, string, string> doc in docs)
				{
					listBoxCaseParty.Items.Add(doc.Item3 + " :- " + doc.Item1 + "-" + doc.Item2);
				}
				if (listBoxCaseParty.Items.Count > 0)
				{
					groupBoxCaseParty.Visible = true;
				}
			}
		}

		private void buttonPlantiffAddMore_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			new List<string>();
			dataSet = Factory.CaseClientSystem.GetCaseClientList(isDefendant: false, plantiff: true);
			SelectDialog selectDialog = new SelectDialog();
			selectDialog.DataSource = dataSet;
			selectDialog.IsMultiSelect = true;
			selectDialog.Text = "Select Case Clients";
			DialogResult num = selectDialog.ShowDialog(this);
			listBoxCaseParty.Items.Clear();
			if (num == DialogResult.OK)
			{
				foreach (UltraGridRow selectedRow in selectDialog.SelectedRows)
				{
					string item = selectedRow.Cells["Code"].Value.ToString();
					string item2 = selectedRow.Cells["Name"].Value.ToString();
					docs.Add(new Tuple<string, string, string>(item, item2, "P"));
				}
				foreach (Tuple<string, string, string> doc in docs)
				{
					listBoxCaseParty.Items.Add(doc.Item3 + " :- " + doc.Item1 + "-" + doc.Item2);
				}
				if (listBoxCaseParty.Items.Count > 0)
				{
					groupBoxCaseParty.Visible = true;
				}
			}
		}

		private void buttonSelectDoc_Click(object sender, EventArgs e)
		{
			DataSet legalDocIDList = Factory.LegalActionSystem.GetLegalDocIDList(comboBoxSysDoc.SelectedID);
			try
			{
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.AllowDateFilter = true;
				string text = "";
				string text2 = "";
				selectDocumentDialog.Text = "Select Value";
				selectDocumentDialog.IsMultiSelect = false;
				selectDocumentDialog.AllowDateFilter = true;
				selectDocumentDialog.DataSource = legalDocIDList;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
					{
						if (selectedRow.Cells.Exists("Doc ID"))
						{
							text2 = selectedRow.Cells["Doc ID"].Text.ToString();
						}
						if (selectedRow.Cells.Exists("Doc Number"))
						{
							text = selectedRow.Cells["Doc Number"].Text.ToString();
						}
						else if (selectedRow.Cells.Exists("VoucherID"))
						{
							text = selectedRow.Cells["VoucherID"].Text.ToString();
						}
						else if (selectedRow.Cells.Exists("Number"))
						{
							text = selectedRow.Cells["Number"].Text.ToString();
						}
						else if (selectedRow.Cells.Exists("Batch Number"))
						{
							text = selectedRow.Cells["Batch Number"].Text.ToString();
						}
						if (selectedRow.Cells.Exists("FileNo"))
						{
							selectedRow.Cells["FileNo"].Text.ToString();
						}
					}
				}
				textBoxParentsysDoc.Text = text2;
				textBoxParentVoucherID.Text = text;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonSelectDocSource_Click(object sender, EventArgs e)
		{
			DataSet legalDocIDList = Factory.LegalActionSystem.GetLegalDocIDList(comboBoxSysDoc.SelectedID);
			try
			{
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.AllowDateFilter = true;
				string text = "";
				string text2 = "";
				selectDocumentDialog.Text = "Select Value";
				selectDocumentDialog.IsMultiSelect = false;
				selectDocumentDialog.AllowDateFilter = true;
				selectDocumentDialog.DataSource = legalDocIDList;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
					{
						if (selectedRow.Cells.Exists("Doc ID"))
						{
							text2 = selectedRow.Cells["Doc ID"].Text.ToString();
						}
						if (selectedRow.Cells.Exists("Doc Number"))
						{
							text = selectedRow.Cells["Doc Number"].Text.ToString();
						}
						else if (selectedRow.Cells.Exists("VoucherID"))
						{
							text = selectedRow.Cells["VoucherID"].Text.ToString();
						}
						else if (selectedRow.Cells.Exists("Number"))
						{
							text = selectedRow.Cells["Number"].Text.ToString();
						}
						else if (selectedRow.Cells.Exists("Batch Number"))
						{
							text = selectedRow.Cells["Batch Number"].Text.ToString();
						}
					}
				}
				textBoxSourceSysDoc.Text = text2;
				textBoxSourceVoucherID.Text = text;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxSourceVoucherID_Validated(object sender, EventArgs e)
		{
		}

		private void textBoxParentVoucherID_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(textBoxParentVoucherID.Text))
			{
				string text = "";
				object obj = Factory.DatabaseSystem.GetFieldValue("Legal_Actions", "FileNo", "VoucherID", textBoxParentVoucherID.Text) ?? null;
				if (obj != null)
				{
					text = obj.ToString();
					textBoxParentFileNo.Text = text;
				}
			}
		}

		private void textBoxSourceVoucherID_TextChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(textBoxSourceVoucherID.Text))
			{
				string text = "";
				object obj = Factory.DatabaseSystem.GetFieldValue("Legal_Actions", "FileNo", "VoucherID", textBoxSourceVoucherID.Text) ?? null;
				if (obj != null)
				{
					text = obj.ToString();
					textBoxSourceFileNO.Text = text;
				}
			}
		}

		private void hyperlinkLabelControlHistory_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
		{
			if (!IsNewRecord)
			{
				idList.Add(new Tuple<string, string>(comboBoxSysDoc.SelectedID, textBoxCode.Text));
				if (!string.IsNullOrEmpty(textBoxParentsysDoc.Text) && !string.IsNullOrEmpty(textBoxParentVoucherID.Text))
				{
					idList.Add(new Tuple<string, string>(textBoxParentsysDoc.Text, textBoxParentVoucherID.Text));
				}
				if (!string.IsNullOrEmpty(textBoxSourceSysDoc.Text) && !string.IsNullOrEmpty(textBoxSourceVoucherID.Text))
				{
					idList.Add(new Tuple<string, string>(textBoxSourceSysDoc.Text, textBoxSourceVoucherID.Text));
				}
				DataSet legalActionHistory = Factory.LegalActionSystem.GetLegalActionHistory(idList);
				if (legalActionHistory != null && legalActionHistory.Tables.Count > 0 && legalActionHistory.Tables[0].Rows.Count > 0)
				{
					SelectDialog selectDialog = new SelectDialog();
					selectDialog.DataSource = legalActionHistory;
					selectDialog.IsMultiSelect = false;
					selectDialog.Text = "Case History";
					selectDialog.ShowDialog(this);
				}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Legal.LegalActionDetailsForm));
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance157 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonPlantiffAddMore = new System.Windows.Forms.Button();
			buttonDefendantAddMore = new System.Windows.Forms.Button();
			hyperlinkLabelControlHistory = new DevExpress.XtraEditors.HyperlinkLabelControl();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			buttonSelectDocSource = new Micromind.UISupport.XPButton();
			buttonSelectDocParent = new Micromind.UISupport.XPButton();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxParentsysDoc = new Micromind.UISupport.MMTextBox();
			textBoxParentVoucherID = new Micromind.UISupport.MMTextBox();
			textBoxSourceSysDoc = new Micromind.UISupport.MMTextBox();
			textBoxSourceVoucherID = new Micromind.UISupport.MMTextBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			textBoxSourceFileNO = new Micromind.UISupport.MMTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			textBoxParentFileNo = new Micromind.UISupport.MMTextBox();
			groupBoxCaseParty = new System.Windows.Forms.GroupBox();
			listBoxCaseParty = new System.Windows.Forms.ListBox();
			comboBoxPlantiff = new Micromind.DataControls.CaseClientComboBox();
			mmTextBox1 = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxLegalActionStatus = new Micromind.DataControls.LegalActionStatusComboBox();
			comboBoxDefendant = new Micromind.DataControls.CaseClientComboBox();
			textBoxClientName = new Micromind.UISupport.MMTextBox();
			textboxFileNo = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxCaseType = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCaseType = new Micromind.DataControls.GenericListComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxLawyer = new Micromind.UISupport.MMTextBox();
			ComboBoxLawyer = new Micromind.DataControls.LawyerComboBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCaseParty = new Micromind.DataControls.CasePartyComboBox();
			textBoxCaseParty = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			labelCloseDate = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			labelName = new Micromind.UISupport.MMLabel();
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			button2 = new System.Windows.Forms.Button();
			comboBoxActivityPeriod = new Micromind.DataControls.GadgetDateRangeComboBox(components);
			dataGridActivities = new Micromind.UISupport.DataGridList(components);
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
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			createFromExistingActivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			groupBoxCaseParty.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPlantiff).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLegalActionStatus).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefendant).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCaseType).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxLawyer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCaseParty).BeginInit();
			ultraTabPageControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxActivityPeriod.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridActivities).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupPeriod.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridListFollowup).BeginInit();
			ultraTabPageControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			ultraTabPageControl1.Controls.Add(buttonPlantiffAddMore);
			ultraTabPageControl1.Controls.Add(buttonDefendantAddMore);
			ultraTabPageControl1.Controls.Add(hyperlinkLabelControlHistory);
			ultraTabPageControl1.Controls.Add(mmLabel8);
			ultraTabPageControl1.Controls.Add(mmLabel6);
			ultraTabPageControl1.Controls.Add(buttonSelectDocSource);
			ultraTabPageControl1.Controls.Add(buttonSelectDocParent);
			ultraTabPageControl1.Controls.Add(mmLabel5);
			ultraTabPageControl1.Controls.Add(mmLabel3);
			ultraTabPageControl1.Controls.Add(textBoxParentsysDoc);
			ultraTabPageControl1.Controls.Add(textBoxParentVoucherID);
			ultraTabPageControl1.Controls.Add(textBoxSourceSysDoc);
			ultraTabPageControl1.Controls.Add(textBoxSourceVoucherID);
			ultraTabPageControl1.Controls.Add(comboBoxSysDoc);
			ultraTabPageControl1.Controls.Add(textBoxNote);
			ultraTabPageControl1.Controls.Add(mmLabel9);
			ultraTabPageControl1.Controls.Add(textBoxSourceFileNO);
			ultraTabPageControl1.Controls.Add(mmLabel7);
			ultraTabPageControl1.Controls.Add(textBoxParentFileNo);
			ultraTabPageControl1.Controls.Add(groupBoxCaseParty);
			ultraTabPageControl1.Controls.Add(comboBoxPlantiff);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel8);
			ultraTabPageControl1.Controls.Add(mmTextBox1);
			ultraTabPageControl1.Controls.Add(comboBoxLegalActionStatus);
			ultraTabPageControl1.Controls.Add(comboBoxDefendant);
			ultraTabPageControl1.Controls.Add(textboxFileNo);
			ultraTabPageControl1.Controls.Add(mmLabel2);
			ultraTabPageControl1.Controls.Add(textBoxCaseType);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel7);
			ultraTabPageControl1.Controls.Add(comboBoxCaseType);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel6);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel3);
			ultraTabPageControl1.Controls.Add(textBoxLawyer);
			ultraTabPageControl1.Controls.Add(ComboBoxLawyer);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel4);
			ultraTabPageControl1.Controls.Add(comboBoxCaseParty);
			ultraTabPageControl1.Controls.Add(textBoxCaseParty);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel1);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel5);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel2);
			ultraTabPageControl1.Controls.Add(dateTimePickerDate);
			ultraTabPageControl1.Controls.Add(labelCloseDate);
			ultraTabPageControl1.Controls.Add(textBoxClientName);
			ultraTabPageControl1.Controls.Add(formManager);
			ultraTabPageControl1.Controls.Add(textBoxName);
			ultraTabPageControl1.Controls.Add(textBoxCode);
			ultraTabPageControl1.Controls.Add(mmLabel4);
			ultraTabPageControl1.Controls.Add(labelName);
			ultraTabPageControl1.Location = new System.Drawing.Point(2, 21);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(891, 387);
			ultraTabPageControl1.Paint += new System.Windows.Forms.PaintEventHandler(ultraTabPageControl1_Paint);
			buttonPlantiffAddMore.Image = Micromind.ClientUI.Properties.Resources.add;
			buttonPlantiffAddMore.Location = new System.Drawing.Point(465, 97);
			buttonPlantiffAddMore.Name = "buttonPlantiffAddMore";
			buttonPlantiffAddMore.Size = new System.Drawing.Size(23, 22);
			buttonPlantiffAddMore.TabIndex = 370;
			buttonPlantiffAddMore.UseVisualStyleBackColor = true;
			buttonPlantiffAddMore.Click += new System.EventHandler(buttonPlantiffAddMore_Click);
			buttonDefendantAddMore.Image = Micromind.ClientUI.Properties.Resources.add;
			buttonDefendantAddMore.Location = new System.Drawing.Point(465, 75);
			buttonDefendantAddMore.Name = "buttonDefendantAddMore";
			buttonDefendantAddMore.Size = new System.Drawing.Size(23, 22);
			buttonDefendantAddMore.TabIndex = 369;
			buttonDefendantAddMore.UseVisualStyleBackColor = true;
			buttonDefendantAddMore.Click += new System.EventHandler(buttonDefendantAddMore_Click);
			hyperlinkLabelControlHistory.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25f);
			hyperlinkLabelControlHistory.Appearance.LinkColor = System.Drawing.Color.Blue;
			hyperlinkLabelControlHistory.Appearance.Options.UseFont = true;
			hyperlinkLabelControlHistory.Appearance.Options.UseLinkColor = true;
			hyperlinkLabelControlHistory.Appearance.Options.UseVisitedColor = true;
			hyperlinkLabelControlHistory.Appearance.VisitedColor = System.Drawing.Color.FromArgb(128, 128, 255);
			hyperlinkLabelControlHistory.Cursor = System.Windows.Forms.Cursors.Hand;
			hyperlinkLabelControlHistory.Location = new System.Drawing.Point(824, 233);
			hyperlinkLabelControlHistory.Name = "hyperlinkLabelControlHistory";
			hyperlinkLabelControlHistory.Size = new System.Drawing.Size(37, 14);
			hyperlinkLabelControlHistory.TabIndex = 225;
			hyperlinkLabelControlHistory.Text = "History";
			hyperlinkLabelControlHistory.HyperlinkClick += new DevExpress.Utils.HyperlinkClickEventHandler(hyperlinkLabelControlHistory_HyperlinkClick);
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.Transparent;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(283, 222);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(58, 13);
			mmLabel8.TabIndex = 224;
			mmLabel8.Text = "SysDocID:";
			mmLabel8.Visible = false;
			mmLabel6.AutoSize = true;
			mmLabel6.BackColor = System.Drawing.Color.Transparent;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(283, 197);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(47, 13);
			mmLabel6.TabIndex = 223;
			mmLabel6.Text = "SysDoc:";
			mmLabel6.Visible = false;
			buttonSelectDocSource.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocSource.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocSource.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocSource.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocSource.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSelectDocSource.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocSource.Location = new System.Drawing.Point(242, 217);
			buttonSelectDocSource.Name = "buttonSelectDocSource";
			buttonSelectDocSource.Size = new System.Drawing.Size(34, 24);
			buttonSelectDocSource.TabIndex = 216;
			buttonSelectDocSource.Text = "...";
			buttonSelectDocSource.UseVisualStyleBackColor = false;
			buttonSelectDocSource.Click += new System.EventHandler(buttonSelectDocSource_Click);
			buttonSelectDocParent.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocParent.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocParent.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocParent.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocParent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSelectDocParent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocParent.Location = new System.Drawing.Point(242, 191);
			buttonSelectDocParent.Name = "buttonSelectDocParent";
			buttonSelectDocParent.Size = new System.Drawing.Size(34, 24);
			buttonSelectDocParent.TabIndex = 215;
			buttonSelectDocParent.Text = "...";
			buttonSelectDocParent.UseVisualStyleBackColor = false;
			buttonSelectDocParent.Click += new System.EventHandler(buttonSelectDoc_Click);
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(454, 222);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(61, 13);
			mmLabel5.TabIndex = 222;
			mmLabel5.Text = "VoucherID:";
			mmLabel5.Visible = false;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(454, 198);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(61, 13);
			mmLabel3.TabIndex = 221;
			mmLabel3.Text = "VoucherID:";
			mmLabel3.Visible = false;
			textBoxParentsysDoc.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxParentsysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxParentsysDoc.CustomReportFieldName = "";
			textBoxParentsysDoc.CustomReportKey = "";
			textBoxParentsysDoc.CustomReportValueType = 1;
			textBoxParentsysDoc.IsComboTextBox = false;
			textBoxParentsysDoc.IsModified = false;
			textBoxParentsysDoc.Location = new System.Drawing.Point(347, 193);
			textBoxParentsysDoc.MaxLength = 15;
			textBoxParentsysDoc.Name = "textBoxParentsysDoc";
			textBoxParentsysDoc.ReadOnly = true;
			textBoxParentsysDoc.Size = new System.Drawing.Size(106, 20);
			textBoxParentsysDoc.TabIndex = 219;
			textBoxParentsysDoc.Visible = false;
			textBoxParentVoucherID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxParentVoucherID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxParentVoucherID.CustomReportFieldName = "";
			textBoxParentVoucherID.CustomReportKey = "";
			textBoxParentVoucherID.CustomReportValueType = 1;
			textBoxParentVoucherID.IsComboTextBox = false;
			textBoxParentVoucherID.IsModified = false;
			textBoxParentVoucherID.Location = new System.Drawing.Point(520, 194);
			textBoxParentVoucherID.MaxLength = 15;
			textBoxParentVoucherID.Name = "textBoxParentVoucherID";
			textBoxParentVoucherID.ReadOnly = true;
			textBoxParentVoucherID.Size = new System.Drawing.Size(128, 20);
			textBoxParentVoucherID.TabIndex = 220;
			textBoxParentVoucherID.Visible = false;
			textBoxParentVoucherID.TextChanged += new System.EventHandler(textBoxParentVoucherID_TextChanged);
			textBoxSourceSysDoc.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSourceSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSourceSysDoc.CustomReportFieldName = "";
			textBoxSourceSysDoc.CustomReportKey = "";
			textBoxSourceSysDoc.CustomReportValueType = 1;
			textBoxSourceSysDoc.IsComboTextBox = false;
			textBoxSourceSysDoc.IsModified = false;
			textBoxSourceSysDoc.Location = new System.Drawing.Point(347, 216);
			textBoxSourceSysDoc.MaxLength = 15;
			textBoxSourceSysDoc.Name = "textBoxSourceSysDoc";
			textBoxSourceSysDoc.ReadOnly = true;
			textBoxSourceSysDoc.Size = new System.Drawing.Size(106, 20);
			textBoxSourceSysDoc.TabIndex = 217;
			textBoxSourceSysDoc.Visible = false;
			textBoxSourceVoucherID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSourceVoucherID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSourceVoucherID.CustomReportFieldName = "";
			textBoxSourceVoucherID.CustomReportKey = "";
			textBoxSourceVoucherID.CustomReportValueType = 1;
			textBoxSourceVoucherID.IsComboTextBox = false;
			textBoxSourceVoucherID.IsModified = false;
			textBoxSourceVoucherID.Location = new System.Drawing.Point(520, 219);
			textBoxSourceVoucherID.MaxLength = 15;
			textBoxSourceVoucherID.Name = "textBoxSourceVoucherID";
			textBoxSourceVoucherID.ReadOnly = true;
			textBoxSourceVoucherID.Size = new System.Drawing.Size(128, 20);
			textBoxSourceVoucherID.TabIndex = 218;
			textBoxSourceVoucherID.Visible = false;
			textBoxSourceVoucherID.TextChanged += new System.EventHandler(textBoxSourceVoucherID_TextChanged);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(102, 7);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(109, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(102, 253);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(774, 119);
			textBoxNote.TabIndex = 15;
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.Transparent;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(2, 222);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(77, 13);
			mmLabel9.TabIndex = 204;
			mmLabel9.Text = "Parent File No:";
			textBoxSourceFileNO.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSourceFileNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxSourceFileNO.CustomReportFieldName = "";
			textBoxSourceFileNO.CustomReportKey = "";
			textBoxSourceFileNO.CustomReportValueType = 1;
			textBoxSourceFileNO.IsComboTextBox = false;
			textBoxSourceFileNO.IsModified = false;
			textBoxSourceFileNO.Location = new System.Drawing.Point(102, 219);
			textBoxSourceFileNO.MaxLength = 15;
			textBoxSourceFileNO.Name = "textBoxSourceFileNO";
			textBoxSourceFileNO.ReadOnly = true;
			textBoxSourceFileNO.Size = new System.Drawing.Size(140, 20);
			textBoxSourceFileNO.TabIndex = 203;
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.Transparent;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(2, 196);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(87, 13);
			mmLabel7.TabIndex = 200;
			mmLabel7.Text = "Previous File No:";
			textBoxParentFileNo.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxParentFileNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxParentFileNo.CustomReportFieldName = "";
			textBoxParentFileNo.CustomReportKey = "";
			textBoxParentFileNo.CustomReportValueType = 1;
			textBoxParentFileNo.IsComboTextBox = false;
			textBoxParentFileNo.IsModified = false;
			textBoxParentFileNo.Location = new System.Drawing.Point(102, 193);
			textBoxParentFileNo.MaxLength = 15;
			textBoxParentFileNo.Name = "textBoxParentFileNo";
			textBoxParentFileNo.ReadOnly = true;
			textBoxParentFileNo.Size = new System.Drawing.Size(140, 20);
			textBoxParentFileNo.TabIndex = 199;
			groupBoxCaseParty.Controls.Add(listBoxCaseParty);
			groupBoxCaseParty.Location = new System.Drawing.Point(687, 32);
			groupBoxCaseParty.Name = "groupBoxCaseParty";
			groupBoxCaseParty.Size = new System.Drawing.Size(189, 174);
			groupBoxCaseParty.TabIndex = 185;
			groupBoxCaseParty.TabStop = false;
			groupBoxCaseParty.Text = "Case Party";
			groupBoxCaseParty.Visible = false;
			listBoxCaseParty.FormattingEnabled = true;
			listBoxCaseParty.Location = new System.Drawing.Point(6, 19);
			listBoxCaseParty.Name = "listBoxCaseParty";
			listBoxCaseParty.Size = new System.Drawing.Size(177, 147);
			listBoxCaseParty.TabIndex = 0;
			comboBoxPlantiff.Assigned = false;
			comboBoxPlantiff.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPlantiff.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPlantiff.CustomReportFieldName = "";
			comboBoxPlantiff.CustomReportKey = "";
			comboBoxPlantiff.CustomReportValueType = 1;
			comboBoxPlantiff.DescriptionTextBox = mmTextBox1;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPlantiff.DisplayLayout.Appearance = appearance13;
			comboBoxPlantiff.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPlantiff.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPlantiff.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPlantiff.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxPlantiff.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPlantiff.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxPlantiff.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPlantiff.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPlantiff.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPlantiff.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxPlantiff.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPlantiff.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPlantiff.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPlantiff.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxPlantiff.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPlantiff.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPlantiff.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxPlantiff.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxPlantiff.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPlantiff.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxPlantiff.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxPlantiff.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPlantiff.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
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
			comboBoxPlantiff.Location = new System.Drawing.Point(102, 99);
			comboBoxPlantiff.MaxDropDownItems = 12;
			comboBoxPlantiff.Name = "comboBoxPlantiff";
			comboBoxPlantiff.ShowDefendant = false;
			comboBoxPlantiff.ShowInactive = false;
			comboBoxPlantiff.ShowPlantiff = false;
			comboBoxPlantiff.ShowPROCustomersOnly = false;
			comboBoxPlantiff.ShowQuickAdd = true;
			comboBoxPlantiff.Size = new System.Drawing.Size(168, 20);
			comboBoxPlantiff.TabIndex = 8;
			comboBoxPlantiff.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPlantiff.SelectedIndexChanged += new System.EventHandler(comboBoxPlantiff_SelectedIndexChanged);
			mmTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBox1.CustomReportFieldName = "";
			mmTextBox1.CustomReportKey = "";
			mmTextBox1.CustomReportValueType = 1;
			mmTextBox1.IsComboTextBox = false;
			mmTextBox1.IsModified = false;
			mmTextBox1.Location = new System.Drawing.Point(273, 99);
			mmTextBox1.MaxLength = 64;
			mmTextBox1.Name = "mmTextBox1";
			mmTextBox1.ReadOnly = true;
			mmTextBox1.Size = new System.Drawing.Size(191, 20);
			mmTextBox1.TabIndex = 181;
			mmTextBox1.TabStop = false;
			appearance25.FontData.BoldAsString = "True";
			appearance25.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel8.Appearance = appearance25;
			ultraFormattedLinkLabel8.AutoSize = true;
			appearance26.BackColor = System.Drawing.Color.Transparent;
			appearance26.BackColor2 = System.Drawing.Color.White;
			ultraFormattedLinkLabel8.LinkAppearance = appearance26;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(2, 102);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(48, 15);
			ultraFormattedLinkLabel8.TabIndex = 182;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Plantiff:";
			appearance27.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance27;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			comboBoxLegalActionStatus.Assigned = false;
			comboBoxLegalActionStatus.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLegalActionStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLegalActionStatus.CustomReportFieldName = "";
			comboBoxLegalActionStatus.CustomReportKey = "";
			comboBoxLegalActionStatus.CustomReportValueType = 1;
			comboBoxLegalActionStatus.DescriptionTextBox = null;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			appearance28.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLegalActionStatus.DisplayLayout.Appearance = appearance28;
			comboBoxLegalActionStatus.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLegalActionStatus.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance29.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance29.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLegalActionStatus.DisplayLayout.GroupByBox.Appearance = appearance29;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLegalActionStatus.DisplayLayout.GroupByBox.BandLabelAppearance = appearance30;
			comboBoxLegalActionStatus.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance31.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance31.BackColor2 = System.Drawing.SystemColors.Control;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLegalActionStatus.DisplayLayout.GroupByBox.PromptAppearance = appearance31;
			comboBoxLegalActionStatus.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLegalActionStatus.DisplayLayout.MaxRowScrollRegions = 1;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLegalActionStatus.DisplayLayout.Override.ActiveCellAppearance = appearance32;
			appearance33.BackColor = System.Drawing.SystemColors.Highlight;
			appearance33.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLegalActionStatus.DisplayLayout.Override.ActiveRowAppearance = appearance33;
			comboBoxLegalActionStatus.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLegalActionStatus.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLegalActionStatus.DisplayLayout.Override.CardAreaAppearance = appearance34;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			appearance35.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLegalActionStatus.DisplayLayout.Override.CellAppearance = appearance35;
			comboBoxLegalActionStatus.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLegalActionStatus.DisplayLayout.Override.CellPadding = 0;
			appearance36.BackColor = System.Drawing.SystemColors.Control;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLegalActionStatus.DisplayLayout.Override.GroupByRowAppearance = appearance36;
			appearance37.TextHAlignAsString = "Left";
			comboBoxLegalActionStatus.DisplayLayout.Override.HeaderAppearance = appearance37;
			comboBoxLegalActionStatus.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLegalActionStatus.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			comboBoxLegalActionStatus.DisplayLayout.Override.RowAppearance = appearance38;
			comboBoxLegalActionStatus.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance39.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLegalActionStatus.DisplayLayout.Override.TemplateAddRowAppearance = appearance39;
			comboBoxLegalActionStatus.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLegalActionStatus.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLegalActionStatus.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLegalActionStatus.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLegalActionStatus.Editable = true;
			comboBoxLegalActionStatus.FilterString = "";
			comboBoxLegalActionStatus.HasAllAccount = false;
			comboBoxLegalActionStatus.HasCustom = false;
			comboBoxLegalActionStatus.IsDataLoaded = false;
			comboBoxLegalActionStatus.Location = new System.Drawing.Point(565, 30);
			comboBoxLegalActionStatus.MaxDropDownItems = 12;
			comboBoxLegalActionStatus.Name = "comboBoxLegalActionStatus";
			comboBoxLegalActionStatus.ShowInactiveItems = false;
			comboBoxLegalActionStatus.ShowQuickAdd = true;
			comboBoxLegalActionStatus.Size = new System.Drawing.Size(100, 20);
			comboBoxLegalActionStatus.TabIndex = 4;
			comboBoxLegalActionStatus.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDefendant.Assigned = false;
			comboBoxDefendant.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDefendant.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDefendant.CustomReportFieldName = "";
			comboBoxDefendant.CustomReportKey = "";
			comboBoxDefendant.CustomReportValueType = 1;
			comboBoxDefendant.DescriptionTextBox = textBoxClientName;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			appearance40.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDefendant.DisplayLayout.Appearance = appearance40;
			comboBoxDefendant.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDefendant.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance41.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance41.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefendant.DisplayLayout.GroupByBox.Appearance = appearance41;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefendant.DisplayLayout.GroupByBox.BandLabelAppearance = appearance42;
			comboBoxDefendant.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance43.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance43.BackColor2 = System.Drawing.SystemColors.Control;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDefendant.DisplayLayout.GroupByBox.PromptAppearance = appearance43;
			comboBoxDefendant.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDefendant.DisplayLayout.MaxRowScrollRegions = 1;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDefendant.DisplayLayout.Override.ActiveCellAppearance = appearance44;
			appearance45.BackColor = System.Drawing.SystemColors.Highlight;
			appearance45.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDefendant.DisplayLayout.Override.ActiveRowAppearance = appearance45;
			comboBoxDefendant.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDefendant.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDefendant.DisplayLayout.Override.CardAreaAppearance = appearance46;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			appearance47.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDefendant.DisplayLayout.Override.CellAppearance = appearance47;
			comboBoxDefendant.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDefendant.DisplayLayout.Override.CellPadding = 0;
			appearance48.BackColor = System.Drawing.SystemColors.Control;
			appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance48.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDefendant.DisplayLayout.Override.GroupByRowAppearance = appearance48;
			appearance49.TextHAlignAsString = "Left";
			comboBoxDefendant.DisplayLayout.Override.HeaderAppearance = appearance49;
			comboBoxDefendant.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDefendant.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			comboBoxDefendant.DisplayLayout.Override.RowAppearance = appearance50;
			comboBoxDefendant.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance51.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDefendant.DisplayLayout.Override.TemplateAddRowAppearance = appearance51;
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
			comboBoxDefendant.Location = new System.Drawing.Point(102, 76);
			comboBoxDefendant.MaxDropDownItems = 12;
			comboBoxDefendant.Name = "comboBoxDefendant";
			comboBoxDefendant.ShowDefendant = false;
			comboBoxDefendant.ShowInactive = false;
			comboBoxDefendant.ShowPlantiff = false;
			comboBoxDefendant.ShowPROCustomersOnly = false;
			comboBoxDefendant.ShowQuickAdd = true;
			comboBoxDefendant.Size = new System.Drawing.Size(168, 20);
			comboBoxDefendant.TabIndex = 6;
			comboBoxDefendant.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDefendant.SelectedIndexChanged += new System.EventHandler(comboBoxDefendant_SelectedIndexChanged);
			textBoxClientName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxClientName.CustomReportFieldName = "";
			textBoxClientName.CustomReportKey = "";
			textBoxClientName.CustomReportValueType = 1;
			textBoxClientName.IsComboTextBox = false;
			textBoxClientName.IsModified = false;
			textBoxClientName.Location = new System.Drawing.Point(273, 76);
			textBoxClientName.MaxLength = 64;
			textBoxClientName.Name = "textBoxClientName";
			textBoxClientName.ReadOnly = true;
			textBoxClientName.Size = new System.Drawing.Size(191, 20);
			textBoxClientName.TabIndex = 138;
			textBoxClientName.TabStop = false;
			textboxFileNo.BackColor = System.Drawing.Color.White;
			textboxFileNo.CustomReportFieldName = "";
			textboxFileNo.CustomReportKey = "";
			textboxFileNo.CustomReportValueType = 1;
			textboxFileNo.IsComboTextBox = false;
			textboxFileNo.IsModified = false;
			textboxFileNo.Location = new System.Drawing.Point(102, 53);
			textboxFileNo.MaxLength = 64;
			textboxFileNo.Name = "textboxFileNo";
			textboxFileNo.Size = new System.Drawing.Size(288, 20);
			textboxFileNo.TabIndex = 5;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(2, 57);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(51, 13);
			mmLabel2.TabIndex = 172;
			mmLabel2.Text = "File No:";
			textBoxCaseType.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCaseType.CustomReportFieldName = "";
			textBoxCaseType.CustomReportKey = "";
			textBoxCaseType.CustomReportValueType = 1;
			textBoxCaseType.IsComboTextBox = false;
			textBoxCaseType.IsModified = false;
			textBoxCaseType.Location = new System.Drawing.Point(273, 166);
			textBoxCaseType.MaxLength = 64;
			textBoxCaseType.Name = "textBoxCaseType";
			textBoxCaseType.ReadOnly = true;
			textBoxCaseType.Size = new System.Drawing.Size(391, 20);
			textBoxCaseType.TabIndex = 168;
			textBoxCaseType.TabStop = false;
			appearance52.FontData.BoldAsString = "False";
			appearance52.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance52;
			ultraFormattedLinkLabel7.AutoSize = true;
			appearance53.BackColor = System.Drawing.Color.Transparent;
			appearance53.BackColor2 = System.Drawing.Color.White;
			ultraFormattedLinkLabel7.LinkAppearance = appearance53;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(2, 171);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(59, 15);
			ultraFormattedLinkLabel7.TabIndex = 167;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Case Type:";
			appearance54.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance54;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked_1);
			comboBoxCaseType.Assigned = false;
			comboBoxCaseType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCaseType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCaseType.CustomReportFieldName = "";
			comboBoxCaseType.CustomReportKey = "";
			comboBoxCaseType.CustomReportValueType = 1;
			comboBoxCaseType.DescriptionTextBox = textBoxCaseType;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCaseType.DisplayLayout.Appearance = appearance55;
			comboBoxCaseType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCaseType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance56.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance56.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCaseType.DisplayLayout.GroupByBox.Appearance = appearance56;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCaseType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance57;
			comboBoxCaseType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance58.BackColor2 = System.Drawing.SystemColors.Control;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance58.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCaseType.DisplayLayout.GroupByBox.PromptAppearance = appearance58;
			comboBoxCaseType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCaseType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCaseType.DisplayLayout.Override.ActiveCellAppearance = appearance59;
			appearance60.BackColor = System.Drawing.SystemColors.Highlight;
			appearance60.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCaseType.DisplayLayout.Override.ActiveRowAppearance = appearance60;
			comboBoxCaseType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCaseType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCaseType.DisplayLayout.Override.CardAreaAppearance = appearance61;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			appearance62.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCaseType.DisplayLayout.Override.CellAppearance = appearance62;
			comboBoxCaseType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCaseType.DisplayLayout.Override.CellPadding = 0;
			appearance63.BackColor = System.Drawing.SystemColors.Control;
			appearance63.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance63.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance63.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCaseType.DisplayLayout.Override.GroupByRowAppearance = appearance63;
			appearance64.TextHAlignAsString = "Left";
			comboBoxCaseType.DisplayLayout.Override.HeaderAppearance = appearance64;
			comboBoxCaseType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCaseType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			comboBoxCaseType.DisplayLayout.Override.RowAppearance = appearance65;
			comboBoxCaseType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCaseType.DisplayLayout.Override.TemplateAddRowAppearance = appearance66;
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
			comboBoxCaseType.Location = new System.Drawing.Point(102, 168);
			comboBoxCaseType.MaxDropDownItems = 12;
			comboBoxCaseType.Name = "comboBoxCaseType";
			comboBoxCaseType.ShowInactiveItems = false;
			comboBoxCaseType.ShowQuickAdd = true;
			comboBoxCaseType.Size = new System.Drawing.Size(168, 20);
			comboBoxCaseType.TabIndex = 12;
			comboBoxCaseType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance67.FontData.BoldAsString = "True";
			appearance67.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance67;
			ultraFormattedLinkLabel6.AutoSize = true;
			appearance68.BackColor = System.Drawing.Color.Transparent;
			appearance68.BackColor2 = System.Drawing.Color.White;
			ultraFormattedLinkLabel6.LinkAppearance = appearance68;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(516, 35);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(44, 15);
			ultraFormattedLinkLabel6.TabIndex = 165;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Status:";
			appearance69.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance69;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked_1);
			appearance70.FontData.BoldAsString = "False";
			appearance70.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance70;
			ultraFormattedLinkLabel3.AutoSize = true;
			appearance71.BackColor = System.Drawing.Color.Transparent;
			appearance71.BackColor2 = System.Drawing.Color.White;
			ultraFormattedLinkLabel3.LinkAppearance = appearance71;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(2, 125);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(75, 15);
			ultraFormattedLinkLabel3.TabIndex = 163;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Judicial Entity:";
			appearance72.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance72;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked_2);
			textBoxLawyer.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLawyer.CustomReportFieldName = "";
			textBoxLawyer.CustomReportKey = "";
			textBoxLawyer.CustomReportValueType = 1;
			textBoxLawyer.IsComboTextBox = false;
			textBoxLawyer.IsModified = false;
			textBoxLawyer.Location = new System.Drawing.Point(273, 143);
			textBoxLawyer.MaxLength = 64;
			textBoxLawyer.Name = "textBoxLawyer";
			textBoxLawyer.ReadOnly = true;
			textBoxLawyer.Size = new System.Drawing.Size(391, 20);
			textBoxLawyer.TabIndex = 162;
			textBoxLawyer.TabStop = false;
			ComboBoxLawyer.Assigned = false;
			ComboBoxLawyer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxLawyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxLawyer.CustomReportFieldName = "";
			ComboBoxLawyer.CustomReportKey = "";
			ComboBoxLawyer.CustomReportValueType = 1;
			ComboBoxLawyer.DescriptionTextBox = textBoxLawyer;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxLawyer.DisplayLayout.Appearance = appearance73;
			ComboBoxLawyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxLawyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxLawyer.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxLawyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			ComboBoxLawyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxLawyer.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			ComboBoxLawyer.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxLawyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxLawyer.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxLawyer.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			ComboBoxLawyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxLawyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxLawyer.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxLawyer.DisplayLayout.Override.CellAppearance = appearance80;
			ComboBoxLawyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxLawyer.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxLawyer.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			ComboBoxLawyer.DisplayLayout.Override.HeaderAppearance = appearance82;
			ComboBoxLawyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxLawyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			ComboBoxLawyer.DisplayLayout.Override.RowAppearance = appearance83;
			ComboBoxLawyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxLawyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			ComboBoxLawyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxLawyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxLawyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxLawyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxLawyer.Editable = true;
			ComboBoxLawyer.FilterString = "";
			ComboBoxLawyer.HasAllAccount = false;
			ComboBoxLawyer.HasCustom = false;
			ComboBoxLawyer.IsDataLoaded = false;
			ComboBoxLawyer.Location = new System.Drawing.Point(102, 145);
			ComboBoxLawyer.MaxDropDownItems = 12;
			ComboBoxLawyer.Name = "ComboBoxLawyer";
			ComboBoxLawyer.ShowInactiveItems = false;
			ComboBoxLawyer.ShowQuickAdd = true;
			ComboBoxLawyer.Size = new System.Drawing.Size(168, 20);
			ComboBoxLawyer.TabIndex = 11;
			ComboBoxLawyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance85.FontData.BoldAsString = "False";
			appearance85.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance85;
			ultraFormattedLinkLabel4.AutoSize = true;
			appearance86.BackColor = System.Drawing.Color.Transparent;
			appearance86.BackColor2 = System.Drawing.Color.White;
			ultraFormattedLinkLabel4.LinkAppearance = appearance86;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(2, 148);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(43, 15);
			ultraFormattedLinkLabel4.TabIndex = 160;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Lawyer:";
			appearance87.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance87;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked_1);
			comboBoxCaseParty.Assigned = false;
			comboBoxCaseParty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCaseParty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCaseParty.CustomReportFieldName = "";
			comboBoxCaseParty.CustomReportKey = "";
			comboBoxCaseParty.CustomReportValueType = 1;
			comboBoxCaseParty.DescriptionTextBox = textBoxCaseParty;
			appearance88.BackColor = System.Drawing.SystemColors.Window;
			appearance88.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCaseParty.DisplayLayout.Appearance = appearance88;
			comboBoxCaseParty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCaseParty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance89.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCaseParty.DisplayLayout.GroupByBox.Appearance = appearance89;
			appearance90.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCaseParty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance90;
			comboBoxCaseParty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance91.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance91.BackColor2 = System.Drawing.SystemColors.Control;
			appearance91.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCaseParty.DisplayLayout.GroupByBox.PromptAppearance = appearance91;
			comboBoxCaseParty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCaseParty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			appearance92.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCaseParty.DisplayLayout.Override.ActiveCellAppearance = appearance92;
			appearance93.BackColor = System.Drawing.SystemColors.Highlight;
			appearance93.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCaseParty.DisplayLayout.Override.ActiveRowAppearance = appearance93;
			comboBoxCaseParty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCaseParty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance94.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCaseParty.DisplayLayout.Override.CardAreaAppearance = appearance94;
			appearance95.BorderColor = System.Drawing.Color.Silver;
			appearance95.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCaseParty.DisplayLayout.Override.CellAppearance = appearance95;
			comboBoxCaseParty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCaseParty.DisplayLayout.Override.CellPadding = 0;
			appearance96.BackColor = System.Drawing.SystemColors.Control;
			appearance96.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance96.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCaseParty.DisplayLayout.Override.GroupByRowAppearance = appearance96;
			appearance97.TextHAlignAsString = "Left";
			comboBoxCaseParty.DisplayLayout.Override.HeaderAppearance = appearance97;
			comboBoxCaseParty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCaseParty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.BorderColor = System.Drawing.Color.Silver;
			comboBoxCaseParty.DisplayLayout.Override.RowAppearance = appearance98;
			comboBoxCaseParty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance99.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCaseParty.DisplayLayout.Override.TemplateAddRowAppearance = appearance99;
			comboBoxCaseParty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCaseParty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCaseParty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCaseParty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCaseParty.Editable = true;
			comboBoxCaseParty.FilterString = "";
			comboBoxCaseParty.HasAllAccount = false;
			comboBoxCaseParty.HasCustom = false;
			comboBoxCaseParty.IsDataLoaded = false;
			comboBoxCaseParty.Location = new System.Drawing.Point(102, 122);
			comboBoxCaseParty.MaxDropDownItems = 12;
			comboBoxCaseParty.Name = "comboBoxCaseParty";
			comboBoxCaseParty.ShowInactiveItems = false;
			comboBoxCaseParty.ShowQuickAdd = true;
			comboBoxCaseParty.Size = new System.Drawing.Size(168, 20);
			comboBoxCaseParty.TabIndex = 10;
			comboBoxCaseParty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxCaseParty.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCaseParty.CustomReportFieldName = "";
			textBoxCaseParty.CustomReportKey = "";
			textBoxCaseParty.CustomReportValueType = 1;
			textBoxCaseParty.IsComboTextBox = false;
			textBoxCaseParty.IsModified = false;
			textBoxCaseParty.Location = new System.Drawing.Point(273, 122);
			textBoxCaseParty.MaxLength = 64;
			textBoxCaseParty.Name = "textBoxCaseParty";
			textBoxCaseParty.ReadOnly = true;
			textBoxCaseParty.Size = new System.Drawing.Size(191, 20);
			textBoxCaseParty.TabIndex = 140;
			textBoxCaseParty.TabStop = false;
			appearance100.FontData.BoldAsString = "True";
			appearance100.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance100;
			ultraFormattedLinkLabel1.AutoSize = true;
			appearance101.BackColor = System.Drawing.Color.Transparent;
			appearance101.BackColor2 = System.Drawing.Color.White;
			ultraFormattedLinkLabel1.LinkAppearance = appearance101;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(2, 79);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(66, 15);
			ultraFormattedLinkLabel1.TabIndex = 151;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Defendant:";
			appearance102.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance102;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance103.FontData.BoldAsString = "True";
			appearance103.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance103;
			ultraFormattedLinkLabel5.AutoSize = true;
			appearance104.BackColor = System.Drawing.Color.Transparent;
			appearance104.BackColor2 = System.Drawing.Color.White;
			ultraFormattedLinkLabel5.LinkAppearance = appearance104;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(2, 10);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 150;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance105.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance105;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked_1);
			appearance106.FontData.BoldAsString = "True";
			appearance106.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance106;
			ultraFormattedLinkLabel2.AutoSize = true;
			appearance107.BackColor = System.Drawing.Color.Transparent;
			appearance107.BackColor2 = System.Drawing.Color.White;
			ultraFormattedLinkLabel2.LinkAppearance = appearance107;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(224, 10);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabel2.TabIndex = 149;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Doc Number:";
			appearance108.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance108;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(488, 7);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(85, 20);
			dateTimePickerDate.TabIndex = 2;
			labelCloseDate.AutoSize = true;
			labelCloseDate.BackColor = System.Drawing.Color.Transparent;
			labelCloseDate.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCloseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelCloseDate.IsFieldHeader = false;
			labelCloseDate.IsRequired = false;
			labelCloseDate.Location = new System.Drawing.Point(449, 11);
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
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(102, 30);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(380, 20);
			textBoxName.TabIndex = 3;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(316, 7);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(129, 20);
			textBoxCode.TabIndex = 1;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(2, 256);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(63, 13);
			mmLabel4.TabIndex = 142;
			mmLabel4.Text = "Description:";
			labelName.AutoSize = true;
			labelName.BackColor = System.Drawing.Color.Transparent;
			labelName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelName.IsFieldHeader = false;
			labelName.IsRequired = true;
			labelName.Location = new System.Drawing.Point(2, 34);
			labelName.Name = "labelName";
			labelName.PenWidth = 1f;
			labelName.ShowBorder = false;
			labelName.Size = new System.Drawing.Size(43, 13);
			labelName.TabIndex = 135;
			labelName.Text = "Name:";
			ultraTabPageControl4.Controls.Add(button2);
			ultraTabPageControl4.Controls.Add(comboBoxActivityPeriod);
			ultraTabPageControl4.Controls.Add(dataGridActivities);
			ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(891, 387);
			button2.Image = Micromind.ClientUI.Properties.Resources.add;
			button2.Location = new System.Drawing.Point(4, 1);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(23, 22);
			button2.TabIndex = 368;
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(button2_Click);
			comboBoxActivityPeriod.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			comboBoxActivityPeriod.Location = new System.Drawing.Point(733, 3);
			comboBoxActivityPeriod.Name = "comboBoxActivityPeriod";
			comboBoxActivityPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboBoxActivityPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxActivityPeriod.Size = new System.Drawing.Size(152, 20);
			comboBoxActivityPeriod.TabIndex = 361;
			comboBoxActivityPeriod.SelectedIndexChanged += new System.EventHandler(comboBoxActivityPeriod_SelectedIndexChanged);
			dataGridActivities.AllowUnfittedView = false;
			dataGridActivities.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridActivities.DisplayLayout.Appearance = appearance109;
			dataGridActivities.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridActivities.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance110.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance110.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance110.BorderColor = System.Drawing.SystemColors.Window;
			dataGridActivities.DisplayLayout.GroupByBox.Appearance = appearance110;
			appearance111.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridActivities.DisplayLayout.GroupByBox.BandLabelAppearance = appearance111;
			dataGridActivities.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance112.BackColor2 = System.Drawing.SystemColors.Control;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance112.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridActivities.DisplayLayout.GroupByBox.PromptAppearance = appearance112;
			dataGridActivities.DisplayLayout.MaxColScrollRegions = 1;
			dataGridActivities.DisplayLayout.MaxRowScrollRegions = 1;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridActivities.DisplayLayout.Override.ActiveCellAppearance = appearance113;
			appearance114.BackColor = System.Drawing.SystemColors.Highlight;
			appearance114.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridActivities.DisplayLayout.Override.ActiveRowAppearance = appearance114;
			dataGridActivities.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridActivities.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			dataGridActivities.DisplayLayout.Override.CardAreaAppearance = appearance115;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			appearance116.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridActivities.DisplayLayout.Override.CellAppearance = appearance116;
			dataGridActivities.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridActivities.DisplayLayout.Override.CellPadding = 0;
			appearance117.BackColor = System.Drawing.SystemColors.Control;
			appearance117.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance117.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance117.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance117.BorderColor = System.Drawing.SystemColors.Window;
			dataGridActivities.DisplayLayout.Override.GroupByRowAppearance = appearance117;
			appearance118.TextHAlignAsString = "Left";
			dataGridActivities.DisplayLayout.Override.HeaderAppearance = appearance118;
			dataGridActivities.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridActivities.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.BorderColor = System.Drawing.Color.Silver;
			dataGridActivities.DisplayLayout.Override.RowAppearance = appearance119;
			dataGridActivities.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridActivities.DisplayLayout.Override.TemplateAddRowAppearance = appearance120;
			dataGridActivities.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridActivities.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridActivities.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridActivities.LoadLayoutFailed = false;
			dataGridActivities.Location = new System.Drawing.Point(3, 23);
			dataGridActivities.Name = "dataGridActivities";
			dataGridActivities.ShowDeleteMenu = false;
			dataGridActivities.ShowMinusInRed = true;
			dataGridActivities.ShowNewMenu = false;
			dataGridActivities.Size = new System.Drawing.Size(885, 360);
			dataGridActivities.TabIndex = 360;
			dataGridActivities.Text = "dataGridList1";
			ultraTabPageControl2.Controls.Add(mmLabel25);
			ultraTabPageControl2.Controls.Add(button1);
			ultraTabPageControl2.Controls.Add(comboBoxFollowupPeriod);
			ultraTabPageControl2.Controls.Add(buttonAddActivity);
			ultraTabPageControl2.Controls.Add(dataGridListFollowup);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(891, 387);
			mmLabel25.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(686, 6);
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
			comboBoxFollowupPeriod.Location = new System.Drawing.Point(733, 3);
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
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridListFollowup.DisplayLayout.Appearance = appearance121;
			dataGridListFollowup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridListFollowup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance122.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance122.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance122.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.GroupByBox.Appearance = appearance122;
			appearance123.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance123;
			dataGridListFollowup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance124.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance124.BackColor2 = System.Drawing.SystemColors.Control;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance124.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.PromptAppearance = appearance124;
			dataGridListFollowup.DisplayLayout.MaxColScrollRegions = 1;
			dataGridListFollowup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridListFollowup.DisplayLayout.Override.ActiveCellAppearance = appearance125;
			appearance126.BackColor = System.Drawing.SystemColors.Highlight;
			appearance126.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridListFollowup.DisplayLayout.Override.ActiveRowAppearance = appearance126;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.CardAreaAppearance = appearance127;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			appearance128.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridListFollowup.DisplayLayout.Override.CellAppearance = appearance128;
			dataGridListFollowup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridListFollowup.DisplayLayout.Override.CellPadding = 0;
			appearance129.BackColor = System.Drawing.SystemColors.Control;
			appearance129.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance129.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance129.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance129.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.GroupByRowAppearance = appearance129;
			appearance130.TextHAlignAsString = "Left";
			dataGridListFollowup.DisplayLayout.Override.HeaderAppearance = appearance130;
			dataGridListFollowup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridListFollowup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.BorderColor = System.Drawing.Color.Silver;
			dataGridListFollowup.DisplayLayout.Override.RowAppearance = appearance131;
			dataGridListFollowup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridListFollowup.DisplayLayout.Override.TemplateAddRowAppearance = appearance132;
			dataGridListFollowup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridListFollowup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridListFollowup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridListFollowup.LoadLayoutFailed = false;
			dataGridListFollowup.Location = new System.Drawing.Point(2, 27);
			dataGridListFollowup.Name = "dataGridListFollowup";
			dataGridListFollowup.ShowDeleteMenu = false;
			dataGridListFollowup.ShowMinusInRed = true;
			dataGridListFollowup.ShowNewMenu = false;
			dataGridListFollowup.Size = new System.Drawing.Size(888, 337);
			dataGridListFollowup.TabIndex = 363;
			dataGridListFollowup.Text = "dataGridList1";
			ultraTabPageControl3.Controls.Add(mmLabel1);
			ultraTabPageControl3.Controls.Add(comboBoxAnalysis);
			ultraTabPageControl3.Controls.Add(dataGridList);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(891, 387);
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
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAnalysis.DisplayLayout.Appearance = appearance133;
			comboBoxAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance134.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance134.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance134.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance134.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.GroupByBox.Appearance = appearance134;
			appearance135.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance135;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance136.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance136.BackColor2 = System.Drawing.SystemColors.Control;
			appearance136.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance136.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance136;
			comboBoxAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance137.BackColor = System.Drawing.SystemColors.Window;
			appearance137.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance137;
			appearance138.BackColor = System.Drawing.SystemColors.Highlight;
			appearance138.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance138;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance139;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			appearance140.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAnalysis.DisplayLayout.Override.CellAppearance = appearance140;
			comboBoxAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance141.BackColor = System.Drawing.SystemColors.Control;
			appearance141.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance141.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance141.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance141.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance141;
			appearance142.TextHAlignAsString = "Left";
			comboBoxAnalysis.DisplayLayout.Override.HeaderAppearance = appearance142;
			comboBoxAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.BorderColor = System.Drawing.Color.Silver;
			comboBoxAnalysis.DisplayLayout.Override.RowAppearance = appearance143;
			comboBoxAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance144.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance144;
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
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance145;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance146.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance146.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance146.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance146;
			appearance147.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance147;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance148.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance148.BackColor2 = System.Drawing.SystemColors.Control;
			appearance148.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance148.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance148;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			appearance149.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance149;
			appearance150.BackColor = System.Drawing.SystemColors.Highlight;
			appearance150.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance150;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance151;
			appearance152.BorderColor = System.Drawing.Color.Silver;
			appearance152.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance152;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance153.BackColor = System.Drawing.SystemColors.Control;
			appearance153.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance153.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance153.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance153.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance153;
			appearance154.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance154;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance155.BackColor = System.Drawing.SystemColors.Window;
			appearance155.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance155;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance156.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance156;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(3, 26);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(885, 343);
			dataGridList.TabIndex = 2;
			dataGridList.Text = "dataGridList1";
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
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButtonInformation,
				toolStripDropDownButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(895, 31);
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
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
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
			createFromExistingActivityToolStripMenuItem.Name = "createFromExistingActivityToolStripMenuItem";
			createFromExistingActivityToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			createFromExistingActivityToolStripMenuItem.Text = "Create from Existing Action";
			createFromExistingActivityToolStripMenuItem.Click += new System.EventHandler(createFromExistingActivityToolStripMenuItem_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 441);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(895, 40);
			panelButtons.TabIndex = 13;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(895, 1);
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
			xpButton1.Location = new System.Drawing.Point(785, 8);
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
			ultraTabControl1.Controls.Add(ultraTabPageControl4);
			ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			ultraTabControl1.Location = new System.Drawing.Point(0, 31);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(895, 410);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 131;
			ultraTab.TabPage = ultraTabPageControl1;
			ultraTab.Text = "Action";
			appearance157.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab2.ClientAreaAppearance = appearance157;
			ultraTab2.TabPage = ultraTabPageControl4;
			ultraTab2.Text = "Activities";
			ultraTab3.TabPage = ultraTabPageControl2;
			ultraTab3.Text = "Follow Up";
			ultraTab3.Visible = false;
			ultraTab4.TabPage = ultraTabPageControl3;
			ultraTab4.Text = "Expenses";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4
			});
			ultraTabControl1.Click += new System.EventHandler(ultraTabControl1_Click);
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(891, 387);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(895, 481);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "LegalActionDetailsForm";
			Text = "Legal Action ";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(LegalActionDetailsForm_Load);
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			groupBoxCaseParty.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxPlantiff).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLegalActionStatus).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDefendant).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCaseType).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxLawyer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCaseParty).EndInit();
			ultraTabPageControl4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxActivityPeriod.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridActivities).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupPeriod.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridListFollowup).EndInit();
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
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
