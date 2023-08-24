using DevExpress.XtraEditors.Controls;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.CRM
{
	public class FollowupDetailsForm : Form, IForm
	{
		private CRMFollowupData currentData;

		private const string TABLENAME_CONST = "Lead_Followup_Details";

		private const string IDFIELD_CONST = "FollowupID";

		private const string IDFIELD2_CONST = "SourceVoucherID";

		private const string IDFIELD3_CONST = "SourceSysDocID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private string sourceSysDocID = "";

		private string sourceVoucherID = "";

		private string followupId = "";

		private CRMRelatedTypes crmType;

		private DateTime thisfollowupdate;

		private DateTime thisfollowuptime;

		private string thisfollowupBy;

		private int status;

		private DataSet followUpData;

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

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private MMLabel mmLabel3;

		private ToolStripButton toolStripButtonInformation;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl ultraTabPageControl2;

		private MMLabel mmLabel25;

		private Button button1;

		private GadgetDateRangeComboBox comboBoxFollowupPeriod;

		private Button buttonAddActivity;

		private DataGridList dataGridListFollowup;

		private UltraTabPageControl ultraTabPageControl1;

		private MMLabel mmLabel10;

		private MMLabel mmLabel9;

		private MMLabel mmLabel8;

		private MMLabel labelName;

		private MMLabel mmLabel7;

		private MMTextBox textBoxNextFollowBy;

		private MMTextBox textBoxFollowBy;

		private DateTimePicker dateTimePickernextFollowupTime;

		private DateTimePicker dateTimePickernextFollowupDate;

		private DateTimePicker dateTimePickerthisFollowupTime;

		private DateTimePicker dateTimePickerthisFollowupDate;

		private MMTextBox textBoxRemark;

		private MMTextBox textBoxFollowupNo;

		private MMLabel mmLabel4;

		private MMTextBox textBoxLeadName;

		private leadsFlatComboBox comboBoxLeads;

		private UserComboBox comboBoxNextFollowupBy;

		private UserComboBox comboBoxThisFollowupBy;

		private LeadStatusComboBox comboBoxFollowupStatus;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private LawyerComboBox ComboBoxNextlawyer;

		private LawyerComboBox ComboBoxThisLawyer;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6001;

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
					textBoxFollowupNo.ReadOnly = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxFollowupNo.ReadOnly = true;
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

		public string FollowupId
		{
			get
			{
				return followupId;
			}
			set
			{
				followupId = value;
			}
		}

		public string SourceVoucherID
		{
			get
			{
				return sourceVoucherID;
			}
			set
			{
				sourceVoucherID = value;
			}
		}

		public string SourceSysDocID
		{
			get
			{
				return sourceSysDocID;
			}
			set
			{
				sourceSysDocID = value;
			}
		}

		public CRMRelatedTypes CRMType
		{
			get
			{
				return crmType;
			}
			set
			{
				crmType = value;
			}
		}

		public DateTime ThisfollowupDate
		{
			get
			{
				return thisfollowupdate;
			}
			set
			{
				thisfollowupdate = value;
			}
		}

		public DateTime ThisfollowupTime
		{
			get
			{
				return thisfollowuptime;
			}
			set
			{
				thisfollowuptime = value;
			}
		}

		public string ThisfollowupBy
		{
			get
			{
				return thisfollowupBy;
			}
			set
			{
				thisfollowupBy = value;
			}
		}

		public int Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
			}
		}

		public FollowupDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += ActivityDetailsForm_Load;
			dataGridListFollowup.DoubleClick += dataGridListFollowup_DoubleClick;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CRMFollowupData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CRMFollowupTable.Rows[0] : currentData.CRMFollowupTable.NewRow();
				dataRow.BeginEdit();
				dataRow["FollowupID"] = textBoxFollowupNo.Text.Trim();
				dataRow["ThisFollowupDate"] = dateTimePickerthisFollowupDate.Value.ToShortDateString() + " " + dateTimePickerthisFollowupTime.Value.ToShortTimeString();
				dataRow["NextFollowupDate"] = dateTimePickernextFollowupDate.Value.ToShortDateString() + " " + dateTimePickernextFollowupTime.Value.ToShortTimeString();
				if (CRMType == CRMRelatedTypes.LegalActivity)
				{
					dataRow["ThisFollowupByID"] = ComboBoxThisLawyer.SelectedID;
					dataRow["NextFollowupByID"] = ComboBoxNextlawyer.SelectedID;
				}
				else
				{
					dataRow["ThisFollowupByID"] = comboBoxThisFollowupBy.SelectedID;
					dataRow["NextFollowupByID"] = comboBoxNextFollowupBy.SelectedID;
				}
				dataRow["ThisFollowupStatusID"] = comboBoxFollowupStatus.SelectedID;
				dataRow["Remark"] = textBoxRemark.Text;
				dataRow["SourceSysDocID"] = sourceSysDocID;
				dataRow["SourceVoucherID"] = sourceVoucherID;
				dataRow["CRMType"] = (int)crmType;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CRMFollowupTable.Rows.Add(dataRow);
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
			textBoxFollowupNo.Focus();
			LoadFollowUp();
			if (CRMType == CRMRelatedTypes.Lead)
			{
				FormActivator.BringFormToFront(FormActivator.LeadDetailsFormObj);
				FormActivator.LeadDetailsFormObj.LoadFollowUp();
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.FollowupSystem.GetFollowupByID(id.Trim(), SourceSysDocID, SourceVoucherID);
					SetupFollowGrid();
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxFollowupNo.Text = id;
						textBoxFollowupNo.Focus();
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
				textBoxFollowupNo.Text = dataRow["FollowupID"].ToString();
				comboBoxLeads.SelectedID = dataRow["LeadID"].ToString();
				DateTime dateTime = DateTime.Parse(dataRow["ThisFollowupDate"].ToString());
				dateTimePickerthisFollowupDate.Value = dateTime.Date;
				dateTimePickerthisFollowupTime.Value = DateTime.Parse(dateTime.ToString("hh:mm:ss"));
				DateTime dateTime2 = DateTime.Parse(dataRow["NextFollowupDate"].ToString());
				dateTimePickernextFollowupDate.Value = dateTime2.Date;
				dateTimePickernextFollowupTime.Value = DateTime.Parse(dateTime2.ToString("hh:mm:ss"));
				if (CRMType == CRMRelatedTypes.LegalActivity)
				{
					ComboBoxThisLawyer.SelectedID = dataRow["ThisFollowupByID"].ToString();
					ComboBoxNextlawyer.SelectedID = dataRow["NextFollowupByID"].ToString();
				}
				else
				{
					comboBoxThisFollowupBy.SelectedID = dataRow["ThisFollowupByID"].ToString();
					comboBoxNextFollowupBy.SelectedID = dataRow["NextFollowupByID"].ToString();
				}
				SourceSysDocID = dataRow["SourceSysDocID"].ToString();
				SourceVoucherID = dataRow["SourceVoucherID"].ToString();
				textBoxRemark.Text = dataRow["Remark"].ToString();
				if (dataRow["ThisFollowupStatusID"] != DBNull.Value)
				{
					comboBoxFollowupStatus.SelectedID = dataRow["ThisFollowupStatusID"].ToString();
				}
				else
				{
					comboBoxFollowupStatus.SelectedID = "";
				}
				textBoxFollowBy.Text = comboBoxThisFollowupBy.SelectedName;
				textBoxNextFollowBy.Text = comboBoxNextFollowupBy.SelectedName;
			}
		}

		public void EditDocument(string FollowupId)
		{
			LoadData(this.FollowupId);
			comboBoxFollowupPeriod.SelectedIndex = 13;
		}

		public void AddFollowUp()
		{
			IsNewRecord = true;
			ClearForm();
			if (IsNewRecord && ThisfollowupBy != null)
			{
				SetupFollowGrid();
				ComboBoxThisLawyer.SelectedID = ThisfollowupBy.TrimStart();
				if (CRMType == CRMRelatedTypes.LegalActivity)
				{
					textBoxFollowBy.Text = ComboBoxThisLawyer.SelectedName;
				}
				else
				{
					textBoxFollowBy.Text = comboBoxThisFollowupBy.SelectedName;
				}
				dateTimePickerthisFollowupDate.Value = ThisfollowupDate.Date;
				dateTimePickerthisFollowupTime.Value = DateTime.Parse(ThisfollowupTime.ToString("hh:mm:ss"));
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
					flag = Factory.FollowupSystem.CreateFollowup(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Followup, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.FollowupSystem.UpdateFollowup(currentData);
				}
				FormActivator.LegalActivityDetailsFormObj.LoadFollowUp();
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
			if (CRMRelatedTypes.LegalActivity != CRMType && (textBoxFollowupNo.Text.Trim().Length == 0 || comboBoxThisFollowupBy.SelectedID == "" || comboBoxNextFollowupBy.SelectedID == ""))
			{
				ErrorHelper.InformationMessage("Please enter required fields in bold.");
				return false;
			}
			if (CRMRelatedTypes.LegalActivity == CRMType && (textBoxFollowupNo.Text.Trim().Length == 0 || ComboBoxThisLawyer.SelectedID == "" || ComboBoxNextlawyer.SelectedID == ""))
			{
				ErrorHelper.InformationMessage("Please enter required fields in bold.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Lead_Followup_Details", "FollowupID", textBoxFollowupNo.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxFollowupNo.Focus();
				return false;
			}
			if (dateTimePickernextFollowupDate.Value < dateTimePickerthisFollowupDate.Value)
			{
				ErrorHelper.InformationMessage("Next Follow up date should not be back date.");
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
			if (isNewRecord)
			{
				ActiveNewRecord();
			}
		}

		private void ClearForm()
		{
			textBoxFollowupNo.Text = GetNextFollowupNumber();
			textBoxLeadName.Clear();
			comboBoxThisFollowupBy.Clear();
			comboBoxFollowupStatus.Clear();
			comboBoxNextFollowupBy.Clear();
			ComboBoxThisLawyer.Clear();
			ComboBoxNextlawyer.Clear();
			dateTimePickerthisFollowupDate.Value = DateTime.Now;
			dateTimePickerthisFollowupTime.Value = DateTime.Now;
			dateTimePickernextFollowupDate.Value = DateTime.Now;
			dateTimePickernextFollowupTime.Value = DateTime.Now;
			textBoxRemark.Clear();
			if (CRMType == CRMRelatedTypes.LegalActivity)
			{
				comboBoxThisFollowupBy.Visible = false;
				comboBoxNextFollowupBy.Visible = false;
				ComboBoxNextlawyer.Visible = true;
				ComboBoxThisLawyer.Visible = true;
			}
			else if (CRMType == CRMRelatedTypes.InsuranceClaim)
			{
				comboBoxThisFollowupBy.Visible = true;
				comboBoxNextFollowupBy.Visible = true;
				ComboBoxNextlawyer.Visible = false;
				ComboBoxThisLawyer.Visible = false;
			}
			else
			{
				comboBoxThisFollowupBy.Visible = true;
				comboBoxNextFollowupBy.Visible = true;
				ComboBoxNextlawyer.Visible = false;
				ComboBoxThisLawyer.Visible = false;
			}
			formManager.ResetDirty();
			dateTimePickerthisFollowupDate.Focus();
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
				bool num = Factory.FollowupSystem.DeleteFollowup(textBoxFollowupNo.Text);
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
			LoadData(DatabaseHelper.GetNextID("Lead_Followup_Details", "FollowupID", textBoxFollowupNo.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Lead_Followup_Details", "FollowupID", textBoxFollowupNo.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Lead_Followup_Details", "FollowupID", "SourceVoucherID", SourceVoucherID, "SourceSysDocID", SourceSysDocID));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Lead_Followup_Details", "FollowupID", "SourceVoucherID", SourceVoucherID, "SourceSysDocID", SourceSysDocID));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Lead_Followup_Details", "FollowupID", toolStripTextBoxFind.Text.Trim(), "SourceVoucherID", SourceVoucherID))
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

		private void ActivityDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					dataGridListFollowup.ApplyUIDesign();
					SetupFollowGrid();
					comboBoxFollowupPeriod.LoadData();
					comboBoxThisFollowupBy.LoadData();
					comboBoxFollowupPeriod.SelectedIndex = 13;
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

		private void FollowupDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					comboBoxFollowupStatus.LoadData();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private string GetNextFollowupNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextDocumentNumber("Lead_Followup_Details", "FollowupID");
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
		}

		private void dataGridListFollowup_DoubleClick(object sender, EventArgs e)
		{
			if (dataGridListFollowup.Rows.Count != 0)
			{
				int num = checked(dataGridListFollowup.Rows.Count - 1);
				num = dataGridListFollowup.ActiveRow.Index;
				string text = dataGridListFollowup.Rows[num].Cells["FollowupID"].Value.ToString();
				string text2 = dataGridListFollowup.Rows[num].Cells["SourceVoucherID"].Value.ToString();
				string text3 = dataGridListFollowup.Rows[num].Cells["SourceSysDocID"].Value.ToString();
				CRMRelatedTypes cRMType = (CRMRelatedTypes)int.Parse(dataGridListFollowup.Rows[num].Cells["CRMType"].Value.ToString());
				SourceSysDocID = text3;
				sourceVoucherID = text2;
				CRMType = cRMType;
				FollowupId = text;
				FormActivator.FollowupDetailsFormObj.EditDocument(text);
				ultraTabControl1.SelectedTab = ultraTabControl1.Tabs[0];
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
				dataTable.Columns.Add("FollowupBy");
				dataTable.Columns.Add("Follow_upBy");
				dataTable.Columns.Add("NextFollowupDate", typeof(DateTime));
				dataTable.Columns.Add("NextFollowupBy");
				dataTable.Columns.Add("NextFollow_upBy");
				dataTable.Columns.Add("NextFollowupByID");
				dataTable.Columns.Add("Status");
				dataTable.Columns.Add("StatusID");
				dataTable.Columns.Add("CRMType");
				dataGridListFollowup.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridListFollowup.DisplayLayout.Bands[0].Columns["StatusID"];
				UltraGridColumn ultraGridColumn2 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["NextFollowupByID"];
				UltraGridColumn ultraGridColumn3 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["FollowupID"];
				UltraGridColumn ultraGridColumn4 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["SourceSysDocID"];
				UltraGridColumn ultraGridColumn5 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["CRMType"];
				bool flag2 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["SourceVoucherID"].Hidden = true;
				bool flag4 = ultraGridColumn5.Hidden = flag2;
				bool flag6 = ultraGridColumn4.Hidden = flag4;
				bool flag8 = ultraGridColumn3.Hidden = flag6;
				bool hidden = ultraGridColumn2.Hidden = flag8;
				ultraGridColumn.Hidden = hidden;
				if (CRMRelatedTypes.LegalActivity == CRMType)
				{
					UltraGridColumn ultraGridColumn6 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["FollowupBy"];
					hidden = (dataGridListFollowup.DisplayLayout.Bands[0].Columns["NextFollowupBy"].Hidden = true);
					ultraGridColumn6.Hidden = hidden;
					UltraGridColumn ultraGridColumn7 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["Follow_upBy"];
					hidden = (dataGridListFollowup.DisplayLayout.Bands[0].Columns["NextFollow_upBy"].Hidden = false);
					ultraGridColumn7.Hidden = hidden;
				}
				else
				{
					UltraGridColumn ultraGridColumn8 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["Follow_upBy"];
					hidden = (dataGridListFollowup.DisplayLayout.Bands[0].Columns["NextFollow_upBy"].Hidden = true);
					ultraGridColumn8.Hidden = hidden;
					UltraGridColumn ultraGridColumn9 = dataGridListFollowup.DisplayLayout.Bands[0].Columns["FollowupBy"];
					hidden = (dataGridListFollowup.DisplayLayout.Bands[0].Columns["NextFollowupBy"].Hidden = false);
					ultraGridColumn9.Hidden = hidden;
				}
				if (CRMType == CRMRelatedTypes.LegalActivity)
				{
					comboBoxThisFollowupBy.Visible = false;
					comboBoxNextFollowupBy.Visible = false;
					ComboBoxNextlawyer.Visible = true;
					ComboBoxThisLawyer.Visible = true;
				}
				else if (CRMType == CRMRelatedTypes.InsuranceClaim)
				{
					comboBoxThisFollowupBy.Visible = true;
					comboBoxNextFollowupBy.Visible = true;
					ComboBoxNextlawyer.Visible = false;
					ComboBoxThisLawyer.Visible = false;
				}
				else
				{
					comboBoxThisFollowupBy.Visible = true;
					comboBoxNextFollowupBy.Visible = true;
					ComboBoxNextlawyer.Visible = false;
					ComboBoxThisLawyer.Visible = false;
				}
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
				followUpData = Factory.FollowupSystem.GetFollowupListByActivityID(CRMRelatedTypes.Followup, "", SourceVoucherID, SourceSysDocID, comboBoxFollowupPeriod.FromDate, comboBoxFollowupPeriod.ToDate);
				DataTable dataTable = dataGridListFollowup.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in followUpData.Tables[0].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["FollowupID"] = row["FollowupID"];
					dataRow2["SourceSysDocID"] = row["SourceSysDocID"];
					dataRow2["SourceVoucherID"] = row["SourceVoucherID"];
					dataRow2["FollowupDate"] = row["ThisFollowupDate"];
					dataRow2["FollowupBy"] = row["FollowupBy"];
					dataRow2["NextFollowupDate"] = row["NextFollowupDate"];
					dataRow2["NextFollowupBy"] = row["NextFollowupBy"];
					dataRow2["Status"] = row["Status"];
					dataRow2["StatusID"] = row["ThisFollowupStatusID"];
					dataRow2["NextFollowupByID"] = row["NextFollowupByID"];
					dataRow2["Follow_upBy"] = row["Follow_upBy"];
					dataRow2["NextFollow_upBy"] = row["NextFollow_upBy"];
					dataRow2["CRMType"] = row["CRMType"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
				comboBoxFollowupPeriod.SelectedIndex = 13;
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

		public void ActiveNewRecord()
		{
			comboBoxFollowupPeriod.SelectedIndex = 13;
			LoadFollowUp();
			DateTime dateTime = DateTime.Now;
			string text = "";
			int result = 0;
			CRMRelatedTypes cRMType = CRMRelatedTypes.None;
			if (dataGridListFollowup.Rows.Count > 0)
			{
				int index = checked(dataGridListFollowup.Rows.Count - 1);
				dateTime = DateTime.Parse(dataGridListFollowup.Rows[index].Cells["NextFollowupDate"].Value.ToString());
				dataGridListFollowup.Rows[index].Cells["NextFollowupBy"].Value.ToString();
				text = dataGridListFollowup.Rows[index].Cells["NextFollowupByID"].Value.ToString();
				text = dataGridListFollowup.Rows[index].Cells["NextFollowupByID"].Value.ToString();
				int.TryParse(dataGridListFollowup.Rows[index].Cells["StatusID"].Value.ToString(), out result);
				cRMType = (CRMRelatedTypes)int.Parse(dataGridListFollowup.Rows[index].Cells["CRMType"].Value.ToString());
			}
			FormActivator.BringFormToFront(FormActivator.FollowupDetailsFormObj);
			FormActivator.FollowupDetailsFormObj.SourceSysDocID = SourceSysDocID;
			FormActivator.FollowupDetailsFormObj.SourceVoucherID = SourceVoucherID;
			FormActivator.FollowupDetailsFormObj.CRMType = cRMType;
			FormActivator.FollowupDetailsFormObj.ThisfollowupBy = text;
			FormActivator.FollowupDetailsFormObj.ThisfollowupDate = dateTime;
			FormActivator.FollowupDetailsFormObj.ThisfollowupTime = dateTime;
			FormActivator.FollowupDetailsFormObj.Status = result;
			AddFollowUp();
			if (CRMType == CRMRelatedTypes.CustomerCollection)
			{
				ultraFormattedLinkLabel2.Visible = false;
				comboBoxFollowupStatus.Visible = false;
			}
			else
			{
				ultraFormattedLinkLabel2.Visible = true;
				comboBoxFollowupStatus.Visible = true;
			}
		}

		private void ultraTabControl1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLeadStatus(comboBoxFollowupStatus.SelectedID);
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.CRM.FollowupDetailsForm));
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ComboBoxNextlawyer = new Micromind.DataControls.LawyerComboBox();
			textBoxNextFollowBy = new Micromind.UISupport.MMTextBox();
			ComboBoxThisLawyer = new Micromind.DataControls.LawyerComboBox();
			textBoxFollowBy = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxFollowupStatus = new Micromind.DataControls.LeadStatusComboBox();
			comboBoxNextFollowupBy = new Micromind.DataControls.UserComboBox();
			comboBoxThisFollowupBy = new Micromind.DataControls.UserComboBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			labelName = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			dateTimePickernextFollowupTime = new System.Windows.Forms.DateTimePicker();
			dateTimePickernextFollowupDate = new System.Windows.Forms.DateTimePicker();
			dateTimePickerthisFollowupTime = new System.Windows.Forms.DateTimePicker();
			dateTimePickerthisFollowupDate = new System.Windows.Forms.DateTimePicker();
			textBoxRemark = new Micromind.UISupport.MMTextBox();
			textBoxFollowupNo = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxLeadName = new Micromind.UISupport.MMTextBox();
			comboBoxLeads = new Micromind.DataControls.leadsFlatComboBox();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel25 = new Micromind.UISupport.MMLabel();
			button1 = new System.Windows.Forms.Button();
			comboBoxFollowupPeriod = new Micromind.DataControls.GadgetDateRangeComboBox(components);
			buttonAddActivity = new System.Windows.Forms.Button();
			dataGridListFollowup = new Micromind.UISupport.DataGridList(components);
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
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
			mmLabel3 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxNextlawyer).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxThisLawyer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupStatus).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNextFollowupBy).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxThisFollowupBy).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeads).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupPeriod.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridListFollowup).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			ultraTabPageControl1.Controls.Add(ComboBoxNextlawyer);
			ultraTabPageControl1.Controls.Add(ComboBoxThisLawyer);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel2);
			ultraTabPageControl1.Controls.Add(comboBoxFollowupStatus);
			ultraTabPageControl1.Controls.Add(comboBoxNextFollowupBy);
			ultraTabPageControl1.Controls.Add(comboBoxThisFollowupBy);
			ultraTabPageControl1.Controls.Add(mmLabel10);
			ultraTabPageControl1.Controls.Add(mmLabel9);
			ultraTabPageControl1.Controls.Add(mmLabel8);
			ultraTabPageControl1.Controls.Add(labelName);
			ultraTabPageControl1.Controls.Add(mmLabel7);
			ultraTabPageControl1.Controls.Add(textBoxNextFollowBy);
			ultraTabPageControl1.Controls.Add(textBoxFollowBy);
			ultraTabPageControl1.Controls.Add(dateTimePickernextFollowupTime);
			ultraTabPageControl1.Controls.Add(dateTimePickernextFollowupDate);
			ultraTabPageControl1.Controls.Add(dateTimePickerthisFollowupTime);
			ultraTabPageControl1.Controls.Add(dateTimePickerthisFollowupDate);
			ultraTabPageControl1.Controls.Add(textBoxRemark);
			ultraTabPageControl1.Controls.Add(textBoxFollowupNo);
			ultraTabPageControl1.Controls.Add(mmLabel4);
			ultraTabPageControl1.Controls.Add(textBoxLeadName);
			ultraTabPageControl1.Controls.Add(comboBoxLeads);
			ultraTabPageControl1.Location = new System.Drawing.Point(1, 23);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(592, 221);
			ComboBoxNextlawyer.Assigned = false;
			ComboBoxNextlawyer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxNextlawyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxNextlawyer.CustomReportFieldName = "";
			ComboBoxNextlawyer.CustomReportKey = "";
			ComboBoxNextlawyer.CustomReportValueType = 1;
			ComboBoxNextlawyer.DescriptionTextBox = textBoxNextFollowBy;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxNextlawyer.DisplayLayout.Appearance = appearance;
			ComboBoxNextlawyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxNextlawyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxNextlawyer.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxNextlawyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			ComboBoxNextlawyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxNextlawyer.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			ComboBoxNextlawyer.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxNextlawyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxNextlawyer.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxNextlawyer.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			ComboBoxNextlawyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxNextlawyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxNextlawyer.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxNextlawyer.DisplayLayout.Override.CellAppearance = appearance8;
			ComboBoxNextlawyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxNextlawyer.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxNextlawyer.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			ComboBoxNextlawyer.DisplayLayout.Override.HeaderAppearance = appearance10;
			ComboBoxNextlawyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxNextlawyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			ComboBoxNextlawyer.DisplayLayout.Override.RowAppearance = appearance11;
			ComboBoxNextlawyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxNextlawyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			ComboBoxNextlawyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxNextlawyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxNextlawyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxNextlawyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxNextlawyer.Editable = true;
			ComboBoxNextlawyer.FilterString = "";
			ComboBoxNextlawyer.HasAllAccount = false;
			ComboBoxNextlawyer.HasCustom = false;
			ComboBoxNextlawyer.IsDataLoaded = false;
			ComboBoxNextlawyer.Location = new System.Drawing.Point(129, 105);
			ComboBoxNextlawyer.MaxDropDownItems = 12;
			ComboBoxNextlawyer.Name = "ComboBoxNextlawyer";
			ComboBoxNextlawyer.ShowInactiveItems = false;
			ComboBoxNextlawyer.ShowQuickAdd = true;
			ComboBoxNextlawyer.Size = new System.Drawing.Size(64, 20);
			ComboBoxNextlawyer.TabIndex = 163;
			ComboBoxNextlawyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxNextFollowBy.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxNextFollowBy.CustomReportFieldName = "";
			textBoxNextFollowBy.CustomReportKey = "";
			textBoxNextFollowBy.CustomReportValueType = 1;
			textBoxNextFollowBy.IsComboTextBox = false;
			textBoxNextFollowBy.IsModified = false;
			textBoxNextFollowBy.Location = new System.Drawing.Point(200, 106);
			textBoxNextFollowBy.MaxLength = 64;
			textBoxNextFollowBy.Name = "textBoxNextFollowBy";
			textBoxNextFollowBy.ReadOnly = true;
			textBoxNextFollowBy.Size = new System.Drawing.Size(193, 20);
			textBoxNextFollowBy.TabIndex = 151;
			textBoxNextFollowBy.TabStop = false;
			ComboBoxThisLawyer.Assigned = false;
			ComboBoxThisLawyer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxThisLawyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxThisLawyer.CustomReportFieldName = "";
			ComboBoxThisLawyer.CustomReportKey = "";
			ComboBoxThisLawyer.CustomReportValueType = 1;
			ComboBoxThisLawyer.DescriptionTextBox = textBoxFollowBy;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxThisLawyer.DisplayLayout.Appearance = appearance13;
			ComboBoxThisLawyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxThisLawyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxThisLawyer.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxThisLawyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			ComboBoxThisLawyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxThisLawyer.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			ComboBoxThisLawyer.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxThisLawyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxThisLawyer.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxThisLawyer.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			ComboBoxThisLawyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxThisLawyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxThisLawyer.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxThisLawyer.DisplayLayout.Override.CellAppearance = appearance20;
			ComboBoxThisLawyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxThisLawyer.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxThisLawyer.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			ComboBoxThisLawyer.DisplayLayout.Override.HeaderAppearance = appearance22;
			ComboBoxThisLawyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxThisLawyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			ComboBoxThisLawyer.DisplayLayout.Override.RowAppearance = appearance23;
			ComboBoxThisLawyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxThisLawyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			ComboBoxThisLawyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxThisLawyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxThisLawyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxThisLawyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxThisLawyer.Editable = true;
			ComboBoxThisLawyer.FilterString = "";
			ComboBoxThisLawyer.HasAllAccount = false;
			ComboBoxThisLawyer.HasCustom = false;
			ComboBoxThisLawyer.IsDataLoaded = false;
			ComboBoxThisLawyer.Location = new System.Drawing.Point(129, 57);
			ComboBoxThisLawyer.MaxDropDownItems = 12;
			ComboBoxThisLawyer.Name = "ComboBoxThisLawyer";
			ComboBoxThisLawyer.ShowInactiveItems = false;
			ComboBoxThisLawyer.ShowQuickAdd = true;
			ComboBoxThisLawyer.Size = new System.Drawing.Size(64, 20);
			ComboBoxThisLawyer.TabIndex = 162;
			ComboBoxThisLawyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxFollowBy.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFollowBy.CustomReportFieldName = "";
			textBoxFollowBy.CustomReportKey = "";
			textBoxFollowBy.CustomReportValueType = 1;
			textBoxFollowBy.IsComboTextBox = false;
			textBoxFollowBy.IsModified = false;
			textBoxFollowBy.Location = new System.Drawing.Point(199, 56);
			textBoxFollowBy.MaxLength = 64;
			textBoxFollowBy.Name = "textBoxFollowBy";
			textBoxFollowBy.ReadOnly = true;
			textBoxFollowBy.Size = new System.Drawing.Size(193, 20);
			textBoxFollowBy.TabIndex = 150;
			textBoxFollowBy.TabStop = false;
			appearance25.BackColor = System.Drawing.Color.White;
			appearance25.BackColor2 = System.Drawing.Color.White;
			appearance25.FontData.BoldAsString = "True";
			appearance25.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance25;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(407, 60);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(44, 15);
			ultraFormattedLinkLabel2.TabIndex = 161;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Status:";
			appearance26.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance26;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			comboBoxFollowupStatus.Assigned = false;
			comboBoxFollowupStatus.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFollowupStatus.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFollowupStatus.CustomReportFieldName = "";
			comboBoxFollowupStatus.CustomReportKey = "";
			comboBoxFollowupStatus.CustomReportValueType = 1;
			comboBoxFollowupStatus.DescriptionTextBox = null;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFollowupStatus.DisplayLayout.Appearance = appearance27;
			comboBoxFollowupStatus.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFollowupStatus.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance28.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance28.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFollowupStatus.DisplayLayout.GroupByBox.Appearance = appearance28;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFollowupStatus.DisplayLayout.GroupByBox.BandLabelAppearance = appearance29;
			comboBoxFollowupStatus.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance30.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance30.BackColor2 = System.Drawing.SystemColors.Control;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFollowupStatus.DisplayLayout.GroupByBox.PromptAppearance = appearance30;
			comboBoxFollowupStatus.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFollowupStatus.DisplayLayout.MaxRowScrollRegions = 1;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFollowupStatus.DisplayLayout.Override.ActiveCellAppearance = appearance31;
			appearance32.BackColor = System.Drawing.SystemColors.Highlight;
			appearance32.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFollowupStatus.DisplayLayout.Override.ActiveRowAppearance = appearance32;
			comboBoxFollowupStatus.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFollowupStatus.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFollowupStatus.DisplayLayout.Override.CardAreaAppearance = appearance33;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			appearance34.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFollowupStatus.DisplayLayout.Override.CellAppearance = appearance34;
			comboBoxFollowupStatus.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFollowupStatus.DisplayLayout.Override.CellPadding = 0;
			appearance35.BackColor = System.Drawing.SystemColors.Control;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFollowupStatus.DisplayLayout.Override.GroupByRowAppearance = appearance35;
			appearance36.TextHAlignAsString = "Left";
			comboBoxFollowupStatus.DisplayLayout.Override.HeaderAppearance = appearance36;
			comboBoxFollowupStatus.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFollowupStatus.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.Color.Silver;
			comboBoxFollowupStatus.DisplayLayout.Override.RowAppearance = appearance37;
			comboBoxFollowupStatus.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFollowupStatus.DisplayLayout.Override.TemplateAddRowAppearance = appearance38;
			comboBoxFollowupStatus.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFollowupStatus.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFollowupStatus.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFollowupStatus.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFollowupStatus.Editable = true;
			comboBoxFollowupStatus.FilterString = "";
			comboBoxFollowupStatus.HasAllAccount = false;
			comboBoxFollowupStatus.HasCustom = false;
			comboBoxFollowupStatus.IsDataLoaded = false;
			comboBoxFollowupStatus.Location = new System.Drawing.Point(452, 57);
			comboBoxFollowupStatus.MaxDropDownItems = 12;
			comboBoxFollowupStatus.Name = "comboBoxFollowupStatus";
			comboBoxFollowupStatus.ShowInactiveItems = false;
			comboBoxFollowupStatus.ShowQuickAdd = true;
			comboBoxFollowupStatus.Size = new System.Drawing.Size(109, 20);
			comboBoxFollowupStatus.TabIndex = 143;
			comboBoxFollowupStatus.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxNextFollowupBy.Assigned = false;
			comboBoxNextFollowupBy.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxNextFollowupBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxNextFollowupBy.CustomReportFieldName = "";
			comboBoxNextFollowupBy.CustomReportKey = "";
			comboBoxNextFollowupBy.CustomReportValueType = 1;
			comboBoxNextFollowupBy.DescriptionTextBox = textBoxNextFollowBy;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxNextFollowupBy.DisplayLayout.Appearance = appearance39;
			comboBoxNextFollowupBy.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxNextFollowupBy.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance40.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance40.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNextFollowupBy.DisplayLayout.GroupByBox.Appearance = appearance40;
			appearance41.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNextFollowupBy.DisplayLayout.GroupByBox.BandLabelAppearance = appearance41;
			comboBoxNextFollowupBy.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance42.BackColor2 = System.Drawing.SystemColors.Control;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxNextFollowupBy.DisplayLayout.GroupByBox.PromptAppearance = appearance42;
			comboBoxNextFollowupBy.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxNextFollowupBy.DisplayLayout.MaxRowScrollRegions = 1;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxNextFollowupBy.DisplayLayout.Override.ActiveCellAppearance = appearance43;
			appearance44.BackColor = System.Drawing.SystemColors.Highlight;
			appearance44.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxNextFollowupBy.DisplayLayout.Override.ActiveRowAppearance = appearance44;
			comboBoxNextFollowupBy.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxNextFollowupBy.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			comboBoxNextFollowupBy.DisplayLayout.Override.CardAreaAppearance = appearance45;
			appearance46.BorderColor = System.Drawing.Color.Silver;
			appearance46.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxNextFollowupBy.DisplayLayout.Override.CellAppearance = appearance46;
			comboBoxNextFollowupBy.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxNextFollowupBy.DisplayLayout.Override.CellPadding = 0;
			appearance47.BackColor = System.Drawing.SystemColors.Control;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxNextFollowupBy.DisplayLayout.Override.GroupByRowAppearance = appearance47;
			appearance48.TextHAlignAsString = "Left";
			comboBoxNextFollowupBy.DisplayLayout.Override.HeaderAppearance = appearance48;
			comboBoxNextFollowupBy.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxNextFollowupBy.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.Color.Silver;
			comboBoxNextFollowupBy.DisplayLayout.Override.RowAppearance = appearance49;
			comboBoxNextFollowupBy.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxNextFollowupBy.DisplayLayout.Override.TemplateAddRowAppearance = appearance50;
			comboBoxNextFollowupBy.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxNextFollowupBy.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxNextFollowupBy.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxNextFollowupBy.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxNextFollowupBy.Editable = true;
			comboBoxNextFollowupBy.FilterString = "";
			comboBoxNextFollowupBy.HasAllAccount = false;
			comboBoxNextFollowupBy.HasCustom = false;
			comboBoxNextFollowupBy.IsDataLoaded = false;
			comboBoxNextFollowupBy.Location = new System.Drawing.Point(129, 106);
			comboBoxNextFollowupBy.MaxDropDownItems = 12;
			comboBoxNextFollowupBy.Name = "comboBoxNextFollowupBy";
			comboBoxNextFollowupBy.ShowInactiveItems = false;
			comboBoxNextFollowupBy.ShowQuickAdd = true;
			comboBoxNextFollowupBy.Size = new System.Drawing.Size(65, 20);
			comboBoxNextFollowupBy.TabIndex = 8;
			comboBoxNextFollowupBy.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxThisFollowupBy.Assigned = false;
			comboBoxThisFollowupBy.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxThisFollowupBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxThisFollowupBy.CustomReportFieldName = "";
			comboBoxThisFollowupBy.CustomReportKey = "";
			comboBoxThisFollowupBy.CustomReportValueType = 1;
			comboBoxThisFollowupBy.DescriptionTextBox = textBoxFollowBy;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxThisFollowupBy.DisplayLayout.Appearance = appearance51;
			comboBoxThisFollowupBy.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxThisFollowupBy.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance52.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxThisFollowupBy.DisplayLayout.GroupByBox.Appearance = appearance52;
			appearance53.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxThisFollowupBy.DisplayLayout.GroupByBox.BandLabelAppearance = appearance53;
			comboBoxThisFollowupBy.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance54.BackColor2 = System.Drawing.SystemColors.Control;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxThisFollowupBy.DisplayLayout.GroupByBox.PromptAppearance = appearance54;
			comboBoxThisFollowupBy.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxThisFollowupBy.DisplayLayout.MaxRowScrollRegions = 1;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxThisFollowupBy.DisplayLayout.Override.ActiveCellAppearance = appearance55;
			appearance56.BackColor = System.Drawing.SystemColors.Highlight;
			appearance56.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxThisFollowupBy.DisplayLayout.Override.ActiveRowAppearance = appearance56;
			comboBoxThisFollowupBy.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxThisFollowupBy.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			comboBoxThisFollowupBy.DisplayLayout.Override.CardAreaAppearance = appearance57;
			appearance58.BorderColor = System.Drawing.Color.Silver;
			appearance58.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxThisFollowupBy.DisplayLayout.Override.CellAppearance = appearance58;
			comboBoxThisFollowupBy.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxThisFollowupBy.DisplayLayout.Override.CellPadding = 0;
			appearance59.BackColor = System.Drawing.SystemColors.Control;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxThisFollowupBy.DisplayLayout.Override.GroupByRowAppearance = appearance59;
			appearance60.TextHAlignAsString = "Left";
			comboBoxThisFollowupBy.DisplayLayout.Override.HeaderAppearance = appearance60;
			comboBoxThisFollowupBy.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxThisFollowupBy.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			comboBoxThisFollowupBy.DisplayLayout.Override.RowAppearance = appearance61;
			comboBoxThisFollowupBy.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxThisFollowupBy.DisplayLayout.Override.TemplateAddRowAppearance = appearance62;
			comboBoxThisFollowupBy.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxThisFollowupBy.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxThisFollowupBy.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxThisFollowupBy.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxThisFollowupBy.Editable = true;
			comboBoxThisFollowupBy.FilterString = "";
			comboBoxThisFollowupBy.HasAllAccount = false;
			comboBoxThisFollowupBy.HasCustom = false;
			comboBoxThisFollowupBy.IsDataLoaded = false;
			comboBoxThisFollowupBy.Location = new System.Drawing.Point(129, 57);
			comboBoxThisFollowupBy.MaxDropDownItems = 12;
			comboBoxThisFollowupBy.Name = "comboBoxThisFollowupBy";
			comboBoxThisFollowupBy.ShowInactiveItems = false;
			comboBoxThisFollowupBy.ShowQuickAdd = true;
			comboBoxThisFollowupBy.Size = new System.Drawing.Size(65, 20);
			comboBoxThisFollowupBy.TabIndex = 4;
			comboBoxThisFollowupBy.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel10.AutoSize = true;
			mmLabel10.BackColor = System.Drawing.Color.White;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = true;
			mmLabel10.Location = new System.Drawing.Point(5, 112);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(115, 13);
			mmLabel10.TabIndex = 159;
			mmLabel10.Text = "Next Follow Up By:";
			mmLabel9.AutoSize = true;
			mmLabel9.BackColor = System.Drawing.Color.White;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = true;
			mmLabel9.Location = new System.Drawing.Point(5, 86);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(115, 13);
			mmLabel9.TabIndex = 158;
			mmLabel9.Text = "Next Follow Up on:";
			mmLabel8.AutoSize = true;
			mmLabel8.BackColor = System.Drawing.Color.White;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = true;
			mmLabel8.Location = new System.Drawing.Point(5, 61);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(89, 13);
			mmLabel8.TabIndex = 157;
			mmLabel8.Text = "Follow Up By :";
			labelName.AutoSize = true;
			labelName.BackColor = System.Drawing.Color.White;
			labelName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelName.IsFieldHeader = false;
			labelName.IsRequired = true;
			labelName.Location = new System.Drawing.Point(5, 36);
			labelName.Name = "labelName";
			labelName.PenWidth = 1f;
			labelName.ShowBorder = false;
			labelName.Size = new System.Drawing.Size(113, 13);
			labelName.TabIndex = 156;
			labelName.Text = "This Follow Up on:";
			mmLabel7.AutoSize = true;
			mmLabel7.BackColor = System.Drawing.Color.White;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(5, 11);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(72, 13);
			mmLabel7.TabIndex = 155;
			mmLabel7.Text = "Follow up No:";
			dateTimePickernextFollowupTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickernextFollowupTime.Location = new System.Drawing.Point(264, 81);
			dateTimePickernextFollowupTime.Name = "dateTimePickernextFollowupTime";
			dateTimePickernextFollowupTime.Size = new System.Drawing.Size(129, 20);
			dateTimePickernextFollowupTime.TabIndex = 6;
			dateTimePickernextFollowupDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickernextFollowupDate.Location = new System.Drawing.Point(129, 82);
			dateTimePickernextFollowupDate.Name = "dateTimePickernextFollowupDate";
			dateTimePickernextFollowupDate.Size = new System.Drawing.Size(129, 20);
			dateTimePickernextFollowupDate.TabIndex = 5;
			dateTimePickerthisFollowupTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerthisFollowupTime.Location = new System.Drawing.Point(264, 32);
			dateTimePickerthisFollowupTime.Name = "dateTimePickerthisFollowupTime";
			dateTimePickerthisFollowupTime.Size = new System.Drawing.Size(129, 20);
			dateTimePickerthisFollowupTime.TabIndex = 143;
			dateTimePickerthisFollowupDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerthisFollowupDate.Location = new System.Drawing.Point(129, 32);
			dateTimePickerthisFollowupDate.Name = "dateTimePickerthisFollowupDate";
			dateTimePickerthisFollowupDate.Size = new System.Drawing.Size(129, 20);
			dateTimePickerthisFollowupDate.TabIndex = 142;
			textBoxRemark.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxRemark.BackColor = System.Drawing.Color.White;
			textBoxRemark.CustomReportFieldName = "";
			textBoxRemark.CustomReportKey = "";
			textBoxRemark.CustomReportValueType = 1;
			textBoxRemark.IsComboTextBox = false;
			textBoxRemark.IsModified = false;
			textBoxRemark.Location = new System.Drawing.Point(129, 130);
			textBoxRemark.MaxLength = 255;
			textBoxRemark.Multiline = true;
			textBoxRemark.Name = "textBoxRemark";
			textBoxRemark.Size = new System.Drawing.Size(455, 89);
			textBoxRemark.TabIndex = 9;
			textBoxFollowupNo.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFollowupNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxFollowupNo.CustomReportFieldName = "";
			textBoxFollowupNo.CustomReportKey = "";
			textBoxFollowupNo.CustomReportValueType = 1;
			textBoxFollowupNo.IsComboTextBox = false;
			textBoxFollowupNo.IsModified = false;
			textBoxFollowupNo.Location = new System.Drawing.Point(129, 8);
			textBoxFollowupNo.MaxLength = 15;
			textBoxFollowupNo.Name = "textBoxFollowupNo";
			textBoxFollowupNo.ReadOnly = true;
			textBoxFollowupNo.Size = new System.Drawing.Size(185, 20);
			textBoxFollowupNo.TabIndex = 141;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.White;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(7, 136);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(55, 13);
			mmLabel4.TabIndex = 146;
			mmLabel4.Text = "Remarks :";
			textBoxLeadName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLeadName.CustomReportFieldName = "";
			textBoxLeadName.CustomReportKey = "";
			textBoxLeadName.CustomReportValueType = 1;
			textBoxLeadName.IsComboTextBox = false;
			textBoxLeadName.IsModified = false;
			textBoxLeadName.Location = new System.Drawing.Point(267, 159);
			textBoxLeadName.MaxLength = 64;
			textBoxLeadName.Name = "textBoxLeadName";
			textBoxLeadName.ReadOnly = true;
			textBoxLeadName.Size = new System.Drawing.Size(290, 20);
			textBoxLeadName.TabIndex = 154;
			textBoxLeadName.TabStop = false;
			textBoxLeadName.Visible = false;
			comboBoxLeads.Assigned = false;
			comboBoxLeads.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLeads.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLeads.CustomReportFieldName = "";
			comboBoxLeads.CustomReportKey = "";
			comboBoxLeads.CustomReportValueType = 1;
			comboBoxLeads.DescriptionTextBox = textBoxLeadName;
			comboBoxLeads.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLeads.Editable = true;
			comboBoxLeads.FilterString = "";
			comboBoxLeads.HasAll = false;
			comboBoxLeads.HasCustom = false;
			comboBoxLeads.IsDataLoaded = false;
			comboBoxLeads.Location = new System.Drawing.Point(185, 152);
			comboBoxLeads.MaxDropDownItems = 12;
			comboBoxLeads.Name = "comboBoxLeads";
			comboBoxLeads.ShowInactiveItems = false;
			comboBoxLeads.ShowQuickAdd = true;
			comboBoxLeads.Size = new System.Drawing.Size(129, 20);
			comboBoxLeads.TabIndex = 153;
			comboBoxLeads.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLeads.Visible = false;
			ultraTabPageControl2.Controls.Add(mmLabel25);
			ultraTabPageControl2.Controls.Add(button1);
			ultraTabPageControl2.Controls.Add(comboBoxFollowupPeriod);
			ultraTabPageControl2.Controls.Add(buttonAddActivity);
			ultraTabPageControl2.Controls.Add(dataGridListFollowup);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(592, 221);
			mmLabel25.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(394, 5);
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
			button1.Visible = false;
			comboBoxFollowupPeriod.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			comboBoxFollowupPeriod.Location = new System.Drawing.Point(438, 1);
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
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridListFollowup.DisplayLayout.Appearance = appearance63;
			dataGridListFollowup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridListFollowup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.GroupByBox.Appearance = appearance64;
			appearance65.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance65;
			dataGridListFollowup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance66.BackColor2 = System.Drawing.SystemColors.Control;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.PromptAppearance = appearance66;
			dataGridListFollowup.DisplayLayout.MaxColScrollRegions = 1;
			dataGridListFollowup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridListFollowup.DisplayLayout.Override.ActiveCellAppearance = appearance67;
			appearance68.BackColor = System.Drawing.SystemColors.Highlight;
			appearance68.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridListFollowup.DisplayLayout.Override.ActiveRowAppearance = appearance68;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.CardAreaAppearance = appearance69;
			appearance70.BorderColor = System.Drawing.Color.Silver;
			appearance70.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridListFollowup.DisplayLayout.Override.CellAppearance = appearance70;
			dataGridListFollowup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridListFollowup.DisplayLayout.Override.CellPadding = 0;
			appearance71.BackColor = System.Drawing.SystemColors.Control;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.GroupByRowAppearance = appearance71;
			appearance72.TextHAlignAsString = "Left";
			dataGridListFollowup.DisplayLayout.Override.HeaderAppearance = appearance72;
			dataGridListFollowup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridListFollowup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			dataGridListFollowup.DisplayLayout.Override.RowAppearance = appearance73;
			dataGridListFollowup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridListFollowup.DisplayLayout.Override.TemplateAddRowAppearance = appearance74;
			dataGridListFollowup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridListFollowup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridListFollowup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridListFollowup.LoadLayoutFailed = false;
			dataGridListFollowup.Location = new System.Drawing.Point(-2, 23);
			dataGridListFollowup.Name = "dataGridListFollowup";
			dataGridListFollowup.ShowDeleteMenu = false;
			dataGridListFollowup.ShowMinusInRed = true;
			dataGridListFollowup.ShowNewMenu = false;
			dataGridListFollowup.Size = new System.Drawing.Size(592, 194);
			dataGridListFollowup.TabIndex = 363;
			dataGridListFollowup.Text = "dataGridList1";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[10]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(567, 25);
			toolStrip1.TabIndex = 10;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.Visible = false;
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 22);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 22);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 22);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 22);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripTextBoxFind.Enabled = false;
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 25);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 22);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 22);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 265);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(600, 40);
			panelButtons.TabIndex = 8;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(600, 1);
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
			buttonDelete.TabIndex = 7;
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
			xpButton1.Location = new System.Drawing.Point(490, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 8;
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
			buttonNew.TabIndex = 6;
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
			buttonSave.TabIndex = 5;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(9, 122);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(0, 13);
			mmLabel3.TabIndex = 38;
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
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Location = new System.Drawing.Point(4, 16);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(596, 247);
			ultraTabControl1.TabIndex = 142;
			ultraTab.TabPage = ultraTabPageControl1;
			ultraTab.Text = "Follow Up";
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "History";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabControl1.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(ultraTabControl1_SelectedTabChanged);
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(592, 221);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(600, 305);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "FollowupDetailsForm";
			Text = "Follow Up ";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(FollowupDetailsForm_Load);
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxNextlawyer).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxThisLawyer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupStatus).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxNextFollowupBy).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxThisFollowupBy).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeads).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupPeriod.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridListFollowup).EndInit();
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
