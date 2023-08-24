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

namespace Micromind.ClientUI.WindowsForms.DataEntries.CRM
{
	public class ActivityDetailsForm : Form, IForm
	{
		private CRMActivityData currentData;

		private const string TABLENAME_CONST = "Activity";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

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

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonInformation;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private MMTextBox textBoxContactName;

		private ContactsComboBox comboBoxContact;

		private MMLabel mmLabel5;

		private UserComboBox comboBoxOwner;

		private GenericListComboBox comboBoxReason;

		private CRMRelatedSelector crmRelatedSelector1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private SysDocComboBox comboBoxSysDoc;

		private DateTimePicker dateTimePickerTime;

		private DateTimePicker dateTimePickerDate;

		private MMLabel labelCloseDate;

		private MMTextBox textBoxNote;

		private MMTextBox textBoxLeadName;

		private ActivityTypesComboBox comboBoxActivityType;

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

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private MMLabel mmLabel2;

		private MMLabel mmLabel1;

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

		public int CRMSELECTOR
		{
			get
			{
				return 2;
			}
			set
			{
				crmRelatedSelector1.IsFlag = 1;
				crmRelatedSelector1.SelectedType = (CRMRelatedTypes)value;
			}
		}

		public ActivityDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += ActivityDetailsForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			crmRelatedSelector1.SelectedItemChanged += crmRelatedSelector1_SelectedItemChanged;
			dataGridListFollowup.DoubleClick += dataGridListFollowup_DoubleClick;
		}

		private void crmRelatedSelector1_SelectedItemChanged(object sender, EventArgs e)
		{
			textBoxLeadName.Text = crmRelatedSelector1.SelectedName;
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
			FormActivator.FollowupDetailsFormObj.CRMType = CRMRelatedTypes.Activity;
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
					currentData = new CRMActivityData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CRMActivityTable.Rows[0] : currentData.CRMActivityTable.NewRow();
				dataRow.BeginEdit();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["ReasonID"] = comboBoxReason.SelectedID;
				dataRow["VoucherID"] = textBoxCode.Text.Trim();
				dataRow["ActivityName"] = textBoxName.Text.Trim();
				dataRow["VoucherID"] = textBoxCode.Text.Trim();
				dataRow["ActivityType"] = comboBoxActivityType.SelectedID;
				if (crmRelatedSelector1.IsFlag == 0)
				{
					dataRow["RelatedType"] = (int)crmRelatedSelector1.SelectedType;
				}
				else
				{
					dataRow["RelatedType"] = 2;
				}
				dataRow["RelatedID"] = crmRelatedSelector1.SelectedID;
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
				dataRow["ActivityDateTime"] = dateTimePickerDate.Value.ToShortDateString() + " " + dateTimePickerTime.Value.ToShortTimeString();
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CRMActivityTable.Rows.Add(dataRow);
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
			crmRelatedSelector1.SelectedType = type;
			crmRelatedSelector1.SelectedID = relatedID;
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					if (crmRelatedSelector1.SelectedType == CRMRelatedTypes.Customer)
					{
						currentData = Factory.ActivitySystem.GetCustomerActivityByID(SystemDocID, id.Trim());
					}
					currentData = Factory.ActivitySystem.GetActivityByID(SystemDocID, id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables["Activity"].Rows.Count == 0)
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
				textBoxName.Text = dataRow["ActivityName"].ToString();
				comboBoxActivityType.SelectedID = int.Parse(dataRow["ActivityType"].ToString());
				comboBoxReason.SelectedID = dataRow["ReasonID"].ToString();
				if (!dataRow["RelatedType"].IsDBNullOrEmpty())
				{
					crmRelatedSelector1.SelectedType = (CRMRelatedTypes)int.Parse(dataRow["RelatedType"].ToString());
				}
				else
				{
					crmRelatedSelector1.SelectedType = CRMRelatedTypes.Lead;
				}
				crmRelatedSelector1.SelectedID = dataRow["RelatedID"].ToString();
				comboBoxOwner.SelectedID = dataRow["OwnerID"].ToString();
				comboBoxContact.SelectedID = dataRow["ContactID"].ToString();
				DateTime dateTime = DateTime.Parse(dataRow["ActivityDateTime"].ToString());
				dateTimePickerDate.Value = dateTime.Date;
				dateTimePickerTime.Value = DateTime.Parse(dateTime.ToString("hh:mm:ss"));
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
					flag = Factory.ActivitySystem.CreateActivity(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Activity, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.ActivitySystem.UpdateActivity(currentData);
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
			if (comboBoxSysDoc.SelectedID == "" || textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0 || crmRelatedSelector1.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Activity", "VoucherID", textBoxCode.Text.Trim(), "SysDocID", SystemDocID))
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
			comboBoxActivityType.LoadData();
			comboBoxActivityType.SelectedIndex = 0;
			comboBoxOwner.LoadData();
			comboBoxOwner.SelectedID = Global.CurrentUser.ToUpper();
			comboBoxContact.Clear();
			comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
			comboBoxReason.Clear();
			textBoxLeadName.Clear();
			dateTimePickerDate.Value = DateTime.Now;
			dateTimePickerTime.Value = DateTime.Now;
			textBoxNote.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
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
				bool num = Factory.ActivitySystem.DeleteActivity(SystemDocID, textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Activity", "VoucherID", textBoxCode.Text, "SysDocID", SystemDocID));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Activity", "VoucherID", textBoxCode.Text, "SysDocID", SystemDocID));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Activity", "VoucherID", "SysDocID", SystemDocID));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Activity", "VoucherID", "SysDocID", SystemDocID));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Activity", "VoucherID", toolStripTextBoxFind.Text.Trim(), "SysDocID", SystemDocID))
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
				crmRelatedSelector1.LoadData();
				if (crmRelatedSelector1.IsFlag == 1)
				{
					crmRelatedSelector1.SelectedType = CRMRelatedTypes.Customer;
					crmRelatedSelector1.AllowDrop = false;
					crmRelatedSelector1.CustomSelection();
				}
				else
				{
					crmRelatedSelector1.Clear();
				}
				SetSecurity();
				dataGridListFollowup.ApplyUIDesign();
				SetupFollowGrid();
				comboBoxFollowupPeriod.LoadData();
				comboBoxFollowupPeriod.SelectedIndex = 13;
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					comboBoxSysDoc.FilterByType(SysDocTypes.CRMActivity);
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
			if (crmRelatedSelector1.IsFlag == 1)
			{
				FormActivator.BringFormToFront(FormActivator.CRMCustomerActivityListObj);
			}
			else
			{
				FormActivator.BringFormToFront(FormActivator.CRMActivityListObj);
			}
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
					docManagementForm.EntityType = EntityTypesEnum.Activities;
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.CRMActivity);
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
			new FormHelper().EditContact(comboBoxContact.SelectedID);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				comboBoxFollowupPeriod.SelectedIndex = 13;
				LoadFollowUp();
				DateTime dateTime = DateTime.Now;
				string thisfollowupBy = "";
				int result = 0;
				if (dataGridListFollowup.Rows.Count > 0)
				{
					int index = checked(dataGridListFollowup.Rows.Count - 1);
					dateTime = DateTime.Parse(dataGridListFollowup.Rows[index].Cells["NextFollowupDate"].Value.ToString());
					dataGridListFollowup.Rows[index].Cells["NextFollowupBy"].Value.ToString();
					thisfollowupBy = dataGridListFollowup.Rows[index].Cells["NextFollowupByID"].Value.ToString();
					thisfollowupBy = dataGridListFollowup.Rows[index].Cells["NextFollowupByID"].Value.ToString();
					int.TryParse(dataGridListFollowup.Rows[index].Cells["StatusID"].Value.ToString(), out result);
				}
				FormActivator.BringFormToFront(FormActivator.FollowupDetailsFormObj);
				FormActivator.FollowupDetailsFormObj.SourceSysDocID = comboBoxSysDoc.SelectedID;
				FormActivator.FollowupDetailsFormObj.SourceVoucherID = textBoxCode.Text;
				FormActivator.FollowupDetailsFormObj.CRMType = CRMRelatedTypes.Activity;
				FormActivator.FollowupDetailsFormObj.ThisfollowupBy = thisfollowupBy;
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
				dataTable.Columns.Add("FollowupBy");
				dataTable.Columns.Add("NextFollowupDate", typeof(DateTime));
				dataTable.Columns.Add("NextFollowupBy");
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

		private void LoadFollowUp()
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
						dataRow2["FollowupBy"] = row["FollowupBy"];
						dataRow2["NextFollowupDate"] = row["NextFollowupDate"];
						dataRow2["NextFollowupBy"] = row["NextFollowupBy"];
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.CRMActivity);
		}

		private void ultraFormattedLinkLabel3_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditContact(comboBoxContact.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			FormHelper formHelper = new FormHelper();
			if (crmRelatedSelector1.SelectedID != "")
			{
				if (crmRelatedSelector1.SelectedType == CRMRelatedTypes.Customer)
				{
					formHelper.EditCustomer(crmRelatedSelector1.SelectedID);
				}
				if (crmRelatedSelector1.SelectedType == CRMRelatedTypes.Lead)
				{
					formHelper.EditLead(crmRelatedSelector1.SelectedID);
				}
				if (crmRelatedSelector1.SelectedType == CRMRelatedTypes.Opportunity)
				{
					formHelper.EditOpportunity(crmRelatedSelector1.SelectedID);
				}
			}
		}

		private void ultraTabPageControl1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper();
		}

		private void formManager_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditUser(comboBoxOwner.SelectedID);
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.CRM.ActivityDetailsForm));
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxContactName = new Micromind.UISupport.MMTextBox();
			comboBoxContact = new Micromind.DataControls.ContactsComboBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			comboBoxOwner = new Micromind.DataControls.UserComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxReason = new Micromind.DataControls.GenericListComboBox();
			crmRelatedSelector1 = new Micromind.DataControls.CRMRelatedSelector();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			labelCloseDate = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxLeadName = new Micromind.UISupport.MMTextBox();
			comboBoxActivityType = new Micromind.DataControls.ActivityTypesComboBox();
			formManager = new Micromind.DataControls.FormManager();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			labelName = new Micromind.UISupport.MMLabel();
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
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
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
			((System.ComponentModel.ISupportInitialize)comboBoxContact).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOwner).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReason).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFollowupPeriod.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridListFollowup).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			ultraTabPageControl1.Controls.Add(mmLabel1);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel4);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel3);
			ultraTabPageControl1.Controls.Add(textBoxContactName);
			ultraTabPageControl1.Controls.Add(comboBoxContact);
			ultraTabPageControl1.Controls.Add(mmLabel5);
			ultraTabPageControl1.Controls.Add(comboBoxOwner);
			ultraTabPageControl1.Controls.Add(mmLabel2);
			ultraTabPageControl1.Controls.Add(comboBoxReason);
			ultraTabPageControl1.Controls.Add(crmRelatedSelector1);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel1);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel5);
			ultraTabPageControl1.Controls.Add(ultraFormattedLinkLabel2);
			ultraTabPageControl1.Controls.Add(comboBoxSysDoc);
			ultraTabPageControl1.Controls.Add(dateTimePickerTime);
			ultraTabPageControl1.Controls.Add(dateTimePickerDate);
			ultraTabPageControl1.Controls.Add(labelCloseDate);
			ultraTabPageControl1.Controls.Add(textBoxNote);
			ultraTabPageControl1.Controls.Add(textBoxLeadName);
			ultraTabPageControl1.Controls.Add(comboBoxActivityType);
			ultraTabPageControl1.Controls.Add(formManager);
			ultraTabPageControl1.Controls.Add(textBoxName);
			ultraTabPageControl1.Controls.Add(textBoxCode);
			ultraTabPageControl1.Controls.Add(mmLabel4);
			ultraTabPageControl1.Controls.Add(labelName);
			ultraTabPageControl1.Location = new System.Drawing.Point(1, 23);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(675, 309);
			ultraTabPageControl1.Paint += new System.Windows.Forms.PaintEventHandler(ultraTabPageControl1_Paint);
			mmLabel1.AutoSize = true;
			mmLabel1.BackColor = System.Drawing.Color.Transparent;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(7, 61);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(85, 13);
			mmLabel1.TabIndex = 158;
			mmLabel1.Text = "Activity Type:";
			appearance.BackColor = System.Drawing.Color.White;
			appearance.BackColor2 = System.Drawing.Color.White;
			appearance.FontData.BoldAsString = "False";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(7, 136);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(59, 15);
			ultraFormattedLinkLabel4.TabIndex = 156;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Activity By:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance3.BackColor = System.Drawing.Color.White;
			appearance3.BackColor2 = System.Drawing.Color.White;
			appearance3.FontData.BoldAsString = "False";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance3;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(7, 113);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel3.TabIndex = 155;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Contact:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked_1);
			textBoxContactName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxContactName.CustomReportFieldName = "";
			textBoxContactName.CustomReportKey = "";
			textBoxContactName.CustomReportValueType = 1;
			textBoxContactName.IsComboTextBox = false;
			textBoxContactName.IsModified = false;
			textBoxContactName.Location = new System.Drawing.Point(310, 110);
			textBoxContactName.MaxLength = 64;
			textBoxContactName.Name = "textBoxContactName";
			textBoxContactName.ReadOnly = true;
			textBoxContactName.Size = new System.Drawing.Size(358, 20);
			textBoxContactName.TabIndex = 140;
			textBoxContactName.TabStop = false;
			comboBoxContact.Assigned = false;
			comboBoxContact.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxContact.CustomReportFieldName = "";
			comboBoxContact.CustomReportKey = "";
			comboBoxContact.CustomReportValueType = 1;
			comboBoxContact.DescriptionTextBox = textBoxContactName;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxContact.DisplayLayout.Appearance = appearance5;
			comboBoxContact.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxContact.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxContact.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxContact.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxContact.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxContact.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxContact.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxContact.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxContact.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxContact.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxContact.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxContact.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxContact.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxContact.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxContact.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxContact.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxContact.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxContact.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxContact.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxContact.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxContact.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxContact.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxContact.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxContact.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxContact.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxContact.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxContact.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxContact.Editable = true;
			comboBoxContact.FilterString = "";
			comboBoxContact.HasAllAccount = false;
			comboBoxContact.HasCustom = false;
			comboBoxContact.IsDataLoaded = false;
			comboBoxContact.Location = new System.Drawing.Point(121, 110);
			comboBoxContact.MaxDropDownItems = 12;
			comboBoxContact.Name = "comboBoxContact";
			comboBoxContact.ShowInactiveItems = false;
			comboBoxContact.ShowQuickAdd = true;
			comboBoxContact.Size = new System.Drawing.Size(185, 20);
			comboBoxContact.TabIndex = 139;
			comboBoxContact.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel5.AutoSize = true;
			mmLabel5.BackColor = System.Drawing.Color.Transparent;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(443, 136);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(33, 13);
			mmLabel5.TabIndex = 154;
			mmLabel5.Text = "Time:";
			comboBoxOwner.Assigned = false;
			comboBoxOwner.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxOwner.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxOwner.CustomReportFieldName = "";
			comboBoxOwner.CustomReportKey = "";
			comboBoxOwner.CustomReportValueType = 1;
			comboBoxOwner.DescriptionTextBox = null;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxOwner.DisplayLayout.Appearance = appearance17;
			comboBoxOwner.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxOwner.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOwner.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOwner.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxOwner.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOwner.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxOwner.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxOwner.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxOwner.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxOwner.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxOwner.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxOwner.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxOwner.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxOwner.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxOwner.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxOwner.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOwner.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxOwner.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxOwner.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxOwner.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxOwner.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxOwner.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxOwner.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxOwner.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxOwner.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxOwner.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxOwner.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxOwner.Editable = true;
			comboBoxOwner.FilterString = "";
			comboBoxOwner.HasAllAccount = false;
			comboBoxOwner.HasCustom = false;
			comboBoxOwner.IsDataLoaded = false;
			comboBoxOwner.Location = new System.Drawing.Point(121, 133);
			comboBoxOwner.MaxDropDownItems = 12;
			comboBoxOwner.Name = "comboBoxOwner";
			comboBoxOwner.ShowInactiveItems = false;
			comboBoxOwner.ShowQuickAdd = true;
			comboBoxOwner.Size = new System.Drawing.Size(146, 20);
			comboBoxOwner.TabIndex = 141;
			comboBoxOwner.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel2.AutoSize = true;
			mmLabel2.BackColor = System.Drawing.Color.Transparent;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(276, 61);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(59, 13);
			mmLabel2.TabIndex = 152;
			mmLabel2.Text = "Regarding:";
			comboBoxReason.Assigned = false;
			comboBoxReason.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxReason.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxReason.CustomReportFieldName = "";
			comboBoxReason.CustomReportKey = "";
			comboBoxReason.CustomReportValueType = 1;
			comboBoxReason.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxReason.DisplayLayout.Appearance = appearance29;
			comboBoxReason.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxReason.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReason.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxReason.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxReason.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxReason.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxReason.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxReason.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxReason.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxReason.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxReason.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxReason.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxReason.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxReason.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxReason.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxReason.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxReason.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxReason.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxReason.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxReason.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxReason.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxReason.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxReason.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxReason.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxReason.DisplayMember = "Name";
			comboBoxReason.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxReason.Editable = true;
			comboBoxReason.FilterString = "";
			comboBoxReason.GenericListType = Micromind.Common.Data.GenericListTypes.CRMActivityReason;
			comboBoxReason.HasAllAccount = false;
			comboBoxReason.HasCustom = false;
			comboBoxReason.IsDataLoaded = false;
			comboBoxReason.IsSingleColumn = true;
			comboBoxReason.Location = new System.Drawing.Point(341, 60);
			comboBoxReason.MaxDropDownItems = 12;
			comboBoxReason.Name = "comboBoxReason";
			comboBoxReason.ShowInactiveItems = false;
			comboBoxReason.ShowQuickAdd = false;
			comboBoxReason.Size = new System.Drawing.Size(205, 20);
			comboBoxReason.TabIndex = 136;
			comboBoxReason.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			crmRelatedSelector1.BackColor = System.Drawing.Color.Transparent;
			crmRelatedSelector1.IsFlag = 0;
			crmRelatedSelector1.Location = new System.Drawing.Point(121, 85);
			crmRelatedSelector1.MaximumSize = new System.Drawing.Size(2000, 20);
			crmRelatedSelector1.MinimumSize = new System.Drawing.Size(0, 20);
			crmRelatedSelector1.Name = "crmRelatedSelector1";
			crmRelatedSelector1.SelectedID = "";
			crmRelatedSelector1.SelectedType = Micromind.Common.Data.CRMRelatedTypes.Lead;
			crmRelatedSelector1.Size = new System.Drawing.Size(264, 20);
			crmRelatedSelector1.TabIndex = 137;
			appearance41.BackColor = System.Drawing.Color.White;
			appearance41.BackColor2 = System.Drawing.Color.White;
			appearance41.FontData.BoldAsString = "True";
			appearance41.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance41;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(7, 87);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(68, 15);
			ultraFormattedLinkLabel1.TabIndex = 151;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Related To:";
			appearance42.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance42;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance43.BackColor = System.Drawing.Color.White;
			appearance43.BackColor2 = System.Drawing.Color.White;
			appearance43.FontData.BoldAsString = "True";
			appearance43.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance43;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(7, 10);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 150;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance44.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance44;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked_1);
			appearance45.BackColor = System.Drawing.Color.White;
			appearance45.BackColor2 = System.Drawing.Color.White;
			appearance45.FontData.BoldAsString = "True";
			appearance45.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance45;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(243, 12);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabel2.TabIndex = 149;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Doc Number:";
			appearance46.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance46;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance47;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance48.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance48;
			appearance49.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance49;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance50.BackColor2 = System.Drawing.SystemColors.Control;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance50.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance50;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance51;
			appearance52.BackColor = System.Drawing.SystemColors.Highlight;
			appearance52.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance52;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance53;
			appearance54.BorderColor = System.Drawing.Color.Silver;
			appearance54.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance54;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance55.BackColor = System.Drawing.SystemColors.Control;
			appearance55.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance55.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance55.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance55;
			appearance56.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance56;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance57;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance58;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(121, 9);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(116, 20);
			comboBoxSysDoc.TabIndex = 131;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerTime.Location = new System.Drawing.Point(482, 133);
			dateTimePickerTime.Name = "dateTimePickerTime";
			dateTimePickerTime.Size = new System.Drawing.Size(129, 20);
			dateTimePickerTime.TabIndex = 144;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(308, 133);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(129, 20);
			dateTimePickerDate.TabIndex = 143;
			labelCloseDate.AutoSize = true;
			labelCloseDate.BackColor = System.Drawing.Color.Transparent;
			labelCloseDate.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCloseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelCloseDate.IsFieldHeader = false;
			labelCloseDate.IsRequired = false;
			labelCloseDate.Location = new System.Drawing.Point(273, 136);
			labelCloseDate.Name = "labelCloseDate";
			labelCloseDate.PenWidth = 1f;
			labelCloseDate.ShowBorder = false;
			labelCloseDate.Size = new System.Drawing.Size(33, 13);
			labelCloseDate.TabIndex = 148;
			labelCloseDate.Text = "Date:";
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(121, 159);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(551, 147);
			textBoxNote.TabIndex = 145;
			textBoxLeadName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLeadName.CustomReportFieldName = "";
			textBoxLeadName.CustomReportKey = "";
			textBoxLeadName.CustomReportValueType = 1;
			textBoxLeadName.IsComboTextBox = false;
			textBoxLeadName.IsModified = false;
			textBoxLeadName.Location = new System.Drawing.Point(391, 85);
			textBoxLeadName.MaxLength = 64;
			textBoxLeadName.Name = "textBoxLeadName";
			textBoxLeadName.ReadOnly = true;
			textBoxLeadName.Size = new System.Drawing.Size(277, 20);
			textBoxLeadName.TabIndex = 138;
			textBoxLeadName.TabStop = false;
			comboBoxActivityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxActivityType.FormattingEnabled = true;
			comboBoxActivityType.Location = new System.Drawing.Point(121, 58);
			comboBoxActivityType.Name = "comboBoxActivityType";
			comboBoxActivityType.Size = new System.Drawing.Size(129, 21);
			comboBoxActivityType.TabIndex = 134;
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
			textBoxName.Location = new System.Drawing.Point(121, 33);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(425, 20);
			textBoxName.TabIndex = 133;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(335, 9);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(129, 20);
			textBoxCode.TabIndex = 132;
			mmLabel4.AutoSize = true;
			mmLabel4.BackColor = System.Drawing.Color.Transparent;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(7, 172);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 142;
			mmLabel4.Text = "Note:";
			labelName.AutoSize = true;
			labelName.BackColor = System.Drawing.Color.Transparent;
			labelName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelName.IsFieldHeader = false;
			labelName.IsRequired = true;
			labelName.Location = new System.Drawing.Point(7, 35);
			labelName.Name = "labelName";
			labelName.PenWidth = 1f;
			labelName.ShowBorder = false;
			labelName.Size = new System.Drawing.Size(89, 13);
			labelName.TabIndex = 135;
			labelName.Text = "Activity Name:";
			ultraTabPageControl2.Controls.Add(mmLabel25);
			ultraTabPageControl2.Controls.Add(button1);
			ultraTabPageControl2.Controls.Add(comboBoxFollowupPeriod);
			ultraTabPageControl2.Controls.Add(buttonAddActivity);
			ultraTabPageControl2.Controls.Add(dataGridListFollowup);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(675, 309);
			mmLabel25.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			mmLabel25.AutoSize = true;
			mmLabel25.BackColor = System.Drawing.Color.Transparent;
			mmLabel25.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel25.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			mmLabel25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel25.IsFieldHeader = false;
			mmLabel25.IsRequired = false;
			mmLabel25.Location = new System.Drawing.Point(475, 6);
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
			comboBoxFollowupPeriod.Location = new System.Drawing.Point(522, 3);
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
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridListFollowup.DisplayLayout.Appearance = appearance59;
			dataGridListFollowup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridListFollowup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.GroupByBox.Appearance = appearance60;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance61;
			dataGridListFollowup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance62.BackColor2 = System.Drawing.SystemColors.Control;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListFollowup.DisplayLayout.GroupByBox.PromptAppearance = appearance62;
			dataGridListFollowup.DisplayLayout.MaxColScrollRegions = 1;
			dataGridListFollowup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridListFollowup.DisplayLayout.Override.ActiveCellAppearance = appearance63;
			appearance64.BackColor = System.Drawing.SystemColors.Highlight;
			appearance64.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridListFollowup.DisplayLayout.Override.ActiveRowAppearance = appearance64;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridListFollowup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.CardAreaAppearance = appearance65;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			appearance66.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridListFollowup.DisplayLayout.Override.CellAppearance = appearance66;
			dataGridListFollowup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridListFollowup.DisplayLayout.Override.CellPadding = 0;
			appearance67.BackColor = System.Drawing.SystemColors.Control;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListFollowup.DisplayLayout.Override.GroupByRowAppearance = appearance67;
			appearance68.TextHAlignAsString = "Left";
			dataGridListFollowup.DisplayLayout.Override.HeaderAppearance = appearance68;
			dataGridListFollowup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridListFollowup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.Color.Silver;
			dataGridListFollowup.DisplayLayout.Override.RowAppearance = appearance69;
			dataGridListFollowup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridListFollowup.DisplayLayout.Override.TemplateAddRowAppearance = appearance70;
			dataGridListFollowup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridListFollowup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridListFollowup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridListFollowup.LoadLayoutFailed = false;
			dataGridListFollowup.Location = new System.Drawing.Point(2, 27);
			dataGridListFollowup.Name = "dataGridListFollowup";
			dataGridListFollowup.ShowDeleteMenu = false;
			dataGridListFollowup.ShowMinusInRed = true;
			dataGridListFollowup.ShowNewMenu = false;
			dataGridListFollowup.Size = new System.Drawing.Size(672, 289);
			dataGridListFollowup.TabIndex = 363;
			dataGridListFollowup.Text = "dataGridList1";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[13]
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
				toolStripButtonAttach,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(691, 31);
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
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 376);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(691, 40);
			panelButtons.TabIndex = 13;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(691, 1);
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
			xpButton1.Location = new System.Drawing.Point(581, 8);
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
			ultraTabControl1.Location = new System.Drawing.Point(6, 35);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(679, 335);
			ultraTabControl1.TabIndex = 131;
			ultraTab.TabPage = ultraTabPageControl1;
			ultraTab.Text = "Activity";
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "FollowUp";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(675, 309);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(691, 416);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ActivityDetailsForm";
			Text = "Activity Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxContact).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOwner).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxReason).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
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
