using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class JobTaskDetailsForm : Form, IForm
	{
		private EntityCommentsForm form = new EntityCommentsForm();

		private JobTaskData currentData;

		private const string TABLENAME_CONST = "Job_Task";

		private const string IDFIELD_CONST = "TaskID";

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

		private MMTextBox textBoxDescription;

		private MMLabel mmLabel4;

		private MMTextBox textBoxNote;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private JobComboBox comboBoxJob;

		private JobFeeComboBox comboBoxFee;

		private MMLabel labelFee;

		private MMLabel mmLabel5;

		private MMLabel mmLabel6;

		private MMLabel mmLabel7;

		private MMLabel mmLabel8;

		private EmployeeComboBox comboBoxAssignedTo;

		private NumericUpDown numericUpDownFeePercent;

		private NumericUpDown numericUpDownCompletePercent;

		private MMTextBox textBoxJobName;

		private MMTextBox textBoxFeeName;

		private MMLabel labelFeePercent;

		private MMLabel mmLabel10;

		private MMLabel mmLabel11;

		private MMLabel mmLabel12;

		private NumericUpDown numericUpDownTotalHours;

		private MMLabel mmLabel14;

		private MMLabel mmLabel13;

		private MMTextBox textBoxAssignedToName;

		private MMTextBox textBoxCompletedDescription;

		private MMLabel mmLabel15;

		private MMSDateTimePicker dateTimeStartDate;

		private MMSDateTimePicker dateTimeEndDate;

		private MMSDateTimePicker dateTimeActualStartDate;

		private MMSDateTimePicker dateTimeActualEndDate;

		private ComboBox comboBoxStatus;

		private MMLabel mmLabel16;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel ultraLinkVoucherNumber;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButton2;

		private ToolStripButton toolStripButton3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private JobTaskGroupComboBox jobTaskGroupComboBox;

		private MMTextBox jobTaskGroupTextBox;

		private Panel panelCommentBox;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5003;

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
					panelCommentBox.Enabled = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
					panelCommentBox.Enabled = true;
				}
				AddCommentForm();
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

		public JobTaskDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxStatus.SelectedIndex = 0;
			if (CompanyPreferences.UseProjectPhase)
			{
				labelFee.Text = "Phase:";
				labelFeePercent.Text = "Phase Percent:";
			}
		}

		private void AddEvents()
		{
			base.Load += JobTaskDetailsForm_Load;
			comboBoxJob.SelectedIndexChanged += comboBoxJob_SelectedIndexChanged;
		}

		private void comboBoxJob_SelectedIndexChanged(object sender, EventArgs e)
		{
			comboBoxFee.FilteredJobID = comboBoxJob.SelectedID;
			comboBoxFee.LoadData();
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new JobTaskData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.JobTaskTable.Rows[0] : currentData.JobTaskTable.NewRow();
				dataRow.BeginEdit();
				dataRow["TaskID"] = textBoxCode.Text.Trim();
				dataRow["Description"] = textBoxDescription.Text.Trim();
				dataRow["Status"] = checked(comboBoxStatus.SelectedIndex + 1);
				if (dateTimeStartDate.Checked)
				{
					dataRow["StartDate"] = dateTimeStartDate.Value;
				}
				else
				{
					dataRow["StartDate"] = DBNull.Value;
				}
				if (dateTimeEndDate.Checked)
				{
					dataRow["EndDate"] = dateTimeEndDate.Value;
				}
				else
				{
					dataRow["EndDate"] = DBNull.Value;
				}
				if (dateTimeActualStartDate.Checked)
				{
					dataRow["ActualStartDate"] = dateTimeActualStartDate.Value;
				}
				else
				{
					dataRow["ActualStartDate"] = DBNull.Value;
				}
				if (dateTimeActualEndDate.Checked)
				{
					dataRow["ActualEndDate"] = dateTimeActualEndDate.Value;
				}
				else
				{
					dataRow["ActualEndDate"] = DBNull.Value;
				}
				if (comboBoxAssignedTo.SelectedID != "")
				{
					dataRow["AssignedToID"] = comboBoxAssignedTo.SelectedID;
				}
				else
				{
					dataRow["AssignedToID"] = DBNull.Value;
				}
				if (jobTaskGroupComboBox.SelectedID != "")
				{
					dataRow["TaskGroupID"] = jobTaskGroupComboBox.SelectedID;
				}
				else
				{
					dataRow["TaskGroupID"] = DBNull.Value;
				}
				dataRow["CompletedDescription"] = textBoxCompletedDescription.Text;
				dataRow["CompletedPercentage"] = numericUpDownCompletePercent.Value;
				dataRow["FeePercentage"] = numericUpDownFeePercent.Value;
				dataRow["TotalHours"] = numericUpDownTotalHours.Value;
				dataRow["JobID"] = comboBoxJob.SelectedID;
				dataRow["FeeID"] = comboBoxFee.SelectedID;
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.JobTaskTable.Rows.Add(dataRow);
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
					currentData = Factory.JobTaskSystem.GetJobTaskByID(id.Trim());
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
				textBoxCode.Text = dataRow["TaskID"].ToString();
				textBoxDescription.Text = dataRow["Description"].ToString();
				textBoxCompletedDescription.Text = dataRow["CompletedDescription"].ToString();
				comboBoxFee.SelectedID = dataRow["FeeID"].ToString();
				comboBoxJob.SelectedID = dataRow["JobID"].ToString();
				if (dataRow["Status"] != DBNull.Value)
				{
					comboBoxStatus.SelectedIndex = checked(int.Parse(dataRow["Status"].ToString()) - 1);
				}
				else
				{
					comboBoxStatus.SelectedIndex = 0;
				}
				if (dataRow["StartDate"] != DBNull.Value)
				{
					dateTimeStartDate.Checked = true;
					dateTimeStartDate.Value = DateTime.Parse(dataRow["StartDate"].ToString());
				}
				else
				{
					dateTimeStartDate.Checked = false;
				}
				if (dataRow["EndDate"] != DBNull.Value)
				{
					dateTimeEndDate.Checked = true;
					dateTimeEndDate.Value = DateTime.Parse(dataRow["EndDate"].ToString());
				}
				else
				{
					dateTimeEndDate.Checked = false;
				}
				if (dataRow["ActualStartDate"] != DBNull.Value)
				{
					dateTimeActualStartDate.Checked = true;
					dateTimeActualStartDate.Value = DateTime.Parse(dataRow["ActualStartDate"].ToString());
				}
				else
				{
					dateTimeActualStartDate.Checked = false;
				}
				if (dataRow["ActualEndDate"] != DBNull.Value)
				{
					dateTimeActualEndDate.Checked = true;
					dateTimeActualEndDate.Value = DateTime.Parse(dataRow["ActualEndDate"].ToString());
				}
				else
				{
					dateTimeActualEndDate.Checked = false;
				}
				if (dataRow["FeePercentage"] != DBNull.Value)
				{
					numericUpDownFeePercent.Value = decimal.Parse(dataRow["FeePercentage"].ToString());
				}
				if (dataRow["CompletedPercentage"] != DBNull.Value)
				{
					numericUpDownCompletePercent.Value = decimal.Parse(dataRow["CompletedPercentage"].ToString());
				}
				if (dataRow["TotalHours"] != DBNull.Value)
				{
					numericUpDownTotalHours.Value = decimal.Parse(dataRow["TotalHours"].ToString());
				}
				if (dataRow["AssignedToID"] != DBNull.Value)
				{
					comboBoxAssignedTo.SelectedID = dataRow["AssignedToID"].ToString();
				}
				else
				{
					comboBoxAssignedTo.Clear();
				}
				if (dataRow["TaskGroupID"] != DBNull.Value)
				{
					jobTaskGroupComboBox.SelectedID = dataRow["TaskGroupID"].ToString();
				}
				else
				{
					jobTaskGroupComboBox.SelectedID = "";
				}
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
					flag = Factory.JobTaskSystem.CreateJobTask(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.JobTask, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.JobTaskSystem.UpdateJobTask(currentData);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditCard && Global.CurrentUser != Factory.SystemDocumentSystem.GetCardUserID("Job_Task", "TaskID", textBoxCode.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:115).");
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxDescription.Text.Trim().Length == 0 || jobTaskGroupComboBox.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Job_Task", "TaskID", textBoxCode.Text.Trim()))
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
			textBoxDescription.Clear();
			textBoxNote.Clear();
			textBoxCompletedDescription.Clear();
			numericUpDownCompletePercent.Value = 0m;
			numericUpDownFeePercent.Value = 0m;
			comboBoxAssignedTo.Clear();
			comboBoxFee.Clear();
			comboBoxJob.Clear();
			dateTimeActualEndDate.Checked = false;
			dateTimeActualStartDate.Checked = false;
			dateTimeEndDate.Checked = false;
			dateTimeStartDate.Checked = false;
			numericUpDownTotalHours.Value = 0m;
			comboBoxStatus.SelectedIndex = 0;
			formManager.ResetDirty();
			textBoxCode.Focus();
			jobTaskGroupComboBox.Clear();
			AddCommentForm();
		}

		private void JobTaskGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void JobTaskGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.JobTaskSystem.DeleteJobTask(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Job_Task", "TaskID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Job_Task", "TaskID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Job_Task", "TaskID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Job_Task", "TaskID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Job_Task", "TaskID", toolStripTextBoxFind.Text.Trim()))
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

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(textBoxCode.Text == "") && (!IsDirty || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData())))
				{
					DataSet jobTaskList = Factory.JobTaskSystem.GetJobTaskList();
					if (jobTaskList == null || jobTaskList.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(jobTaskList, "", "Job Task", SysDocTypes.None, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void JobTaskDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				AddCommentForm();
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
			new FormHelper().ShowList(DataComboType.JobTask);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJobTaskGroup(jobTaskGroupComboBox.SelectedID);
		}

		private void ultraLinkVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJob(comboBoxJob.SelectedID);
		}

		private void AddCommentForm()
		{
			form = new EntityCommentsForm();
			if (!IsNewRecord)
			{
				form.EntityID = textBoxCode.Text;
				form.EntityName = textBoxDescription.Text;
			}
			panelCommentBox.Controls.Clear();
			form.TopLevel = false;
			form.AutoScroll = true;
			form.TopMost = true;
			form.FormBorderStyle = FormBorderStyle.None;
			form.Dock = DockStyle.Fill;
			form.EntityType = EntityTypesEnum.JobTask;
			panelCommentBox.Controls.Add(form);
			panelCommentBox.BorderStyle = BorderStyle.FixedSingle;
			form.Show();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.JobTaskDetailsForm));
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
			toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			numericUpDownFeePercent = new System.Windows.Forms.NumericUpDown();
			numericUpDownCompletePercent = new System.Windows.Forms.NumericUpDown();
			numericUpDownTotalHours = new System.Windows.Forms.NumericUpDown();
			comboBoxStatus = new System.Windows.Forms.ComboBox();
			ultraLinkVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelCommentBox = new System.Windows.Forms.Panel();
			jobTaskGroupTextBox = new Micromind.UISupport.MMTextBox();
			jobTaskGroupComboBox = new Micromind.DataControls.JobTaskGroupComboBox();
			mmLabel16 = new Micromind.UISupport.MMLabel();
			dateTimeActualEndDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeEndDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeActualStartDate = new Micromind.UISupport.MMSDateTimePicker(components);
			dateTimeStartDate = new Micromind.UISupport.MMSDateTimePicker(components);
			textBoxCompletedDescription = new Micromind.UISupport.MMTextBox();
			mmLabel15 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			mmLabel13 = new Micromind.UISupport.MMLabel();
			mmLabel14 = new Micromind.UISupport.MMLabel();
			comboBoxAssignedTo = new Micromind.DataControls.EmployeeComboBox();
			textBoxAssignedToName = new Micromind.UISupport.MMTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel12 = new Micromind.UISupport.MMLabel();
			mmLabel11 = new Micromind.UISupport.MMLabel();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			labelFeePercent = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			labelFee = new Micromind.UISupport.MMLabel();
			comboBoxFee = new Micromind.DataControls.JobFeeComboBox();
			textBoxFeeName = new Micromind.UISupport.MMTextBox();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			textBoxJobName = new Micromind.UISupport.MMTextBox();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxDescription = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownFeePercent).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownCompletePercent).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownTotalHours).BeginInit();
			((System.ComponentModel.ISupportInitialize)jobTaskGroupComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAssignedTo).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
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
				toolStripButton3,
				toolStripButton2,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(555, 31);
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
			toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton3.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton3.Name = "toolStripButton3";
			toolStripButton3.Size = new System.Drawing.Size(28, 28);
			toolStripButton3.Text = "&Print";
			toolStripButton3.ToolTipText = "Print (Ctrl+P)";
			toolStripButton3.Click += new System.EventHandler(toolStripButton3_Click);
			toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton2.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton2.Name = "toolStripButton2";
			toolStripButton2.Size = new System.Drawing.Size(28, 28);
			toolStripButton2.Text = "Preview";
			toolStripButton2.ToolTipText = "Preview";
			toolStripButton2.Click += new System.EventHandler(toolStripButton2_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 572);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(555, 40);
			panelButtons.TabIndex = 15;
			numericUpDownFeePercent.Location = new System.Drawing.Point(99, 124);
			numericUpDownFeePercent.Name = "numericUpDownFeePercent";
			numericUpDownFeePercent.Size = new System.Drawing.Size(114, 20);
			numericUpDownFeePercent.TabIndex = 5;
			numericUpDownFeePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			numericUpDownCompletePercent.Location = new System.Drawing.Point(375, 124);
			numericUpDownCompletePercent.Name = "numericUpDownCompletePercent";
			numericUpDownCompletePercent.Size = new System.Drawing.Size(105, 20);
			numericUpDownCompletePercent.TabIndex = 6;
			numericUpDownCompletePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			numericUpDownTotalHours.Location = new System.Drawing.Point(99, 190);
			numericUpDownTotalHours.Name = "numericUpDownTotalHours";
			numericUpDownTotalHours.Size = new System.Drawing.Size(128, 20);
			numericUpDownTotalHours.TabIndex = 11;
			numericUpDownTotalHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStatus.FormattingEnabled = true;
			comboBoxStatus.Items.AddRange(new object[3]
			{
				"Active",
				"Hold",
				"Completed"
			});
			comboBoxStatus.Location = new System.Drawing.Point(350, 35);
			comboBoxStatus.Name = "comboBoxStatus";
			comboBoxStatus.Size = new System.Drawing.Size(121, 21);
			comboBoxStatus.TabIndex = 1;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraLinkVoucherNumber.Appearance = appearance;
			ultraLinkVoucherNumber.AutoSize = true;
			ultraLinkVoucherNumber.Location = new System.Drawing.Point(9, 103);
			ultraLinkVoucherNumber.Name = "ultraLinkVoucherNumber";
			ultraLinkVoucherNumber.Size = new System.Drawing.Size(48, 15);
			ultraLinkVoucherNumber.TabIndex = 162;
			ultraLinkVoucherNumber.TabStop = true;
			ultraLinkVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraLinkVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraLinkVoucherNumber.Value = "Project:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraLinkVoucherNumber.VisitedLinkAppearance = appearance2;
			ultraLinkVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraLinkVoucherNumber_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance3;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(9, 82);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(71, 15);
			ultraFormattedLinkLabel1.TabIndex = 163;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Task Group:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			panelCommentBox.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panelCommentBox.Enabled = false;
			panelCommentBox.Location = new System.Drawing.Point(99, 310);
			panelCommentBox.Name = "panelCommentBox";
			panelCommentBox.Size = new System.Drawing.Size(448, 256);
			panelCommentBox.TabIndex = 166;
			jobTaskGroupTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
			jobTaskGroupTextBox.CustomReportFieldName = "";
			jobTaskGroupTextBox.CustomReportKey = "";
			jobTaskGroupTextBox.CustomReportValueType = 1;
			jobTaskGroupTextBox.IsComboTextBox = false;
			jobTaskGroupTextBox.IsModified = false;
			jobTaskGroupTextBox.Location = new System.Drawing.Point(230, 80);
			jobTaskGroupTextBox.MaxLength = 64;
			jobTaskGroupTextBox.Name = "jobTaskGroupTextBox";
			jobTaskGroupTextBox.ReadOnly = true;
			jobTaskGroupTextBox.Size = new System.Drawing.Size(318, 20);
			jobTaskGroupTextBox.TabIndex = 165;
			jobTaskGroupTextBox.TabStop = false;
			jobTaskGroupComboBox.Assigned = false;
			jobTaskGroupComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			jobTaskGroupComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			jobTaskGroupComboBox.CustomReportFieldName = "";
			jobTaskGroupComboBox.CustomReportKey = "";
			jobTaskGroupComboBox.CustomReportValueType = 1;
			jobTaskGroupComboBox.DescriptionTextBox = jobTaskGroupTextBox;
			jobTaskGroupComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			jobTaskGroupComboBox.Editable = true;
			jobTaskGroupComboBox.FilterString = "";
			jobTaskGroupComboBox.HasAllAccount = false;
			jobTaskGroupComboBox.HasCustom = false;
			jobTaskGroupComboBox.IsDataLoaded = false;
			jobTaskGroupComboBox.Location = new System.Drawing.Point(99, 80);
			jobTaskGroupComboBox.MaxDropDownItems = 12;
			jobTaskGroupComboBox.Name = "jobTaskGroupComboBox";
			jobTaskGroupComboBox.ShowInactiveItems = false;
			jobTaskGroupComboBox.ShowQuickAdd = true;
			jobTaskGroupComboBox.Size = new System.Drawing.Size(128, 20);
			jobTaskGroupComboBox.TabIndex = 3;
			jobTaskGroupComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel16.AutoSize = true;
			mmLabel16.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel16.IsFieldHeader = false;
			mmLabel16.IsRequired = false;
			mmLabel16.Location = new System.Drawing.Point(304, 39);
			mmLabel16.Name = "mmLabel16";
			mmLabel16.PenWidth = 1f;
			mmLabel16.ShowBorder = false;
			mmLabel16.Size = new System.Drawing.Size(40, 13);
			mmLabel16.TabIndex = 30;
			mmLabel16.Text = "Status:";
			dateTimeActualEndDate.Checked = false;
			dateTimeActualEndDate.CustomFormat = " ";
			dateTimeActualEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeActualEndDate.Location = new System.Drawing.Point(374, 169);
			dateTimeActualEndDate.Name = "dateTimeActualEndDate";
			dateTimeActualEndDate.ShowCheckBox = true;
			dateTimeActualEndDate.Size = new System.Drawing.Size(125, 20);
			dateTimeActualEndDate.TabIndex = 10;
			dateTimeActualEndDate.Value = new System.DateTime(0L);
			dateTimeEndDate.Checked = false;
			dateTimeEndDate.CustomFormat = " ";
			dateTimeEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeEndDate.Location = new System.Drawing.Point(374, 147);
			dateTimeEndDate.Name = "dateTimeEndDate";
			dateTimeEndDate.ShowCheckBox = true;
			dateTimeEndDate.Size = new System.Drawing.Size(125, 20);
			dateTimeEndDate.TabIndex = 8;
			dateTimeEndDate.Value = new System.DateTime(0L);
			dateTimeActualStartDate.Checked = false;
			dateTimeActualStartDate.CustomFormat = " ";
			dateTimeActualStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeActualStartDate.Location = new System.Drawing.Point(99, 168);
			dateTimeActualStartDate.Name = "dateTimeActualStartDate";
			dateTimeActualStartDate.ShowCheckBox = true;
			dateTimeActualStartDate.Size = new System.Drawing.Size(128, 20);
			dateTimeActualStartDate.TabIndex = 9;
			dateTimeActualStartDate.Value = new System.DateTime(0L);
			dateTimeStartDate.Checked = false;
			dateTimeStartDate.CustomFormat = " ";
			dateTimeStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimeStartDate.Location = new System.Drawing.Point(99, 146);
			dateTimeStartDate.Name = "dateTimeStartDate";
			dateTimeStartDate.ShowCheckBox = true;
			dateTimeStartDate.Size = new System.Drawing.Size(128, 20);
			dateTimeStartDate.TabIndex = 7;
			dateTimeStartDate.Value = new System.DateTime(0L);
			textBoxCompletedDescription.BackColor = System.Drawing.Color.White;
			textBoxCompletedDescription.CustomReportFieldName = "";
			textBoxCompletedDescription.CustomReportKey = "";
			textBoxCompletedDescription.CustomReportValueType = 1;
			textBoxCompletedDescription.IsComboTextBox = false;
			textBoxCompletedDescription.IsModified = false;
			textBoxCompletedDescription.Location = new System.Drawing.Point(99, 235);
			textBoxCompletedDescription.MaxLength = 255;
			textBoxCompletedDescription.Name = "textBoxCompletedDescription";
			textBoxCompletedDescription.Size = new System.Drawing.Size(449, 20);
			textBoxCompletedDescription.TabIndex = 13;
			mmLabel15.AutoSize = true;
			mmLabel15.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel15.Font = new System.Drawing.Font("Tahoma", 8.25f);
			mmLabel15.IsFieldHeader = false;
			mmLabel15.IsRequired = false;
			mmLabel15.Location = new System.Drawing.Point(9, 238);
			mmLabel15.Name = "mmLabel15";
			mmLabel15.PenWidth = 1f;
			mmLabel15.ShowBorder = false;
			mmLabel15.Size = new System.Drawing.Size(68, 13);
			mmLabel15.TabIndex = 28;
			mmLabel15.Text = "Comp. Desc:";
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
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(555, 1);
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
			xpButton1.Location = new System.Drawing.Point(445, 8);
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
			mmLabel13.AutoSize = true;
			mmLabel13.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel13.IsFieldHeader = false;
			mmLabel13.IsRequired = false;
			mmLabel13.Location = new System.Drawing.Point(9, 215);
			mmLabel13.Name = "mmLabel13";
			mmLabel13.PenWidth = 1f;
			mmLabel13.ShowBorder = false;
			mmLabel13.Size = new System.Drawing.Size(69, 13);
			mmLabel13.TabIndex = 26;
			mmLabel13.Text = "Assigned To:";
			mmLabel14.AutoSize = true;
			mmLabel14.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel14.IsFieldHeader = false;
			mmLabel14.IsRequired = false;
			mmLabel14.Location = new System.Drawing.Point(9, 192);
			mmLabel14.Name = "mmLabel14";
			mmLabel14.PenWidth = 1f;
			mmLabel14.ShowBorder = false;
			mmLabel14.Size = new System.Drawing.Size(65, 13);
			mmLabel14.TabIndex = 26;
			mmLabel14.Text = "Total Hours:";
			comboBoxAssignedTo.Assigned = false;
			comboBoxAssignedTo.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAssignedTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAssignedTo.CustomReportFieldName = "";
			comboBoxAssignedTo.CustomReportKey = "";
			comboBoxAssignedTo.CustomReportValueType = 1;
			comboBoxAssignedTo.DescriptionTextBox = textBoxAssignedToName;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAssignedTo.DisplayLayout.Appearance = appearance5;
			comboBoxAssignedTo.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAssignedTo.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAssignedTo.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAssignedTo.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxAssignedTo.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAssignedTo.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxAssignedTo.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAssignedTo.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAssignedTo.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAssignedTo.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxAssignedTo.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAssignedTo.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAssignedTo.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAssignedTo.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxAssignedTo.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAssignedTo.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAssignedTo.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxAssignedTo.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxAssignedTo.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAssignedTo.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxAssignedTo.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxAssignedTo.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAssignedTo.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxAssignedTo.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAssignedTo.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAssignedTo.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAssignedTo.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAssignedTo.Editable = true;
			comboBoxAssignedTo.FilterString = "";
			comboBoxAssignedTo.HasAllAccount = false;
			comboBoxAssignedTo.HasCustom = false;
			comboBoxAssignedTo.IsDataLoaded = false;
			comboBoxAssignedTo.Location = new System.Drawing.Point(99, 213);
			comboBoxAssignedTo.MaxDropDownItems = 12;
			comboBoxAssignedTo.Name = "comboBoxAssignedTo";
			comboBoxAssignedTo.ShowInactiveItems = false;
			comboBoxAssignedTo.ShowQuickAdd = true;
			comboBoxAssignedTo.ShowTerminatedEmployees = true;
			comboBoxAssignedTo.Size = new System.Drawing.Size(128, 20);
			comboBoxAssignedTo.TabIndex = 12;
			comboBoxAssignedTo.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxAssignedToName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAssignedToName.CustomReportFieldName = "";
			textBoxAssignedToName.CustomReportKey = "";
			textBoxAssignedToName.CustomReportValueType = 1;
			textBoxAssignedToName.IsComboTextBox = false;
			textBoxAssignedToName.IsModified = false;
			textBoxAssignedToName.Location = new System.Drawing.Point(230, 213);
			textBoxAssignedToName.MaxLength = 64;
			textBoxAssignedToName.Name = "textBoxAssignedToName";
			textBoxAssignedToName.ReadOnly = true;
			textBoxAssignedToName.Size = new System.Drawing.Size(318, 20);
			textBoxAssignedToName.TabIndex = 23;
			textBoxAssignedToName.TabStop = false;
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(244, 171);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(77, 13);
			mmLabel8.TabIndex = 22;
			mmLabel8.Text = "Act. End Date:";
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(244, 150);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(55, 13);
			mmLabel6.TabIndex = 22;
			mmLabel6.Text = "End Date:";
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(9, 171);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(80, 13);
			mmLabel7.TabIndex = 22;
			mmLabel7.Text = "Act. Start Date:";
			mmLabel12.AutoSize = true;
			mmLabel12.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel12.IsFieldHeader = false;
			mmLabel12.IsRequired = false;
			mmLabel12.Location = new System.Drawing.Point(217, 127);
			mmLabel12.Name = "mmLabel12";
			mmLabel12.PenWidth = 1f;
			mmLabel12.ShowBorder = false;
			mmLabel12.Size = new System.Drawing.Size(15, 13);
			mmLabel12.TabIndex = 22;
			mmLabel12.Text = "%";
			mmLabel11.AutoSize = true;
			mmLabel11.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel11.IsFieldHeader = false;
			mmLabel11.IsRequired = false;
			mmLabel11.Location = new System.Drawing.Point(484, 127);
			mmLabel11.Name = "mmLabel11";
			mmLabel11.PenWidth = 1f;
			mmLabel11.ShowBorder = false;
			mmLabel11.Size = new System.Drawing.Size(15, 13);
			mmLabel11.TabIndex = 22;
			mmLabel11.Text = "%";
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(243, 126);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(118, 13);
			mmLabel10.TabIndex = 7;
			mmLabel10.Text = "Percentage Completed:";
			labelFeePercent.AutoSize = true;
			labelFeePercent.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelFeePercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelFeePercent.IsFieldHeader = false;
			labelFeePercent.IsRequired = false;
			labelFeePercent.Location = new System.Drawing.Point(9, 126);
			labelFeePercent.Name = "labelFeePercent";
			labelFeePercent.PenWidth = 1f;
			labelFeePercent.ShowBorder = false;
			labelFeePercent.Size = new System.Drawing.Size(68, 13);
			labelFeePercent.TabIndex = 22;
			labelFeePercent.Text = "Fee Percent:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(9, 149);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(58, 13);
			mmLabel5.TabIndex = 22;
			mmLabel5.Text = "Start Date:";
			labelFee.AutoSize = true;
			labelFee.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelFee.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelFee.IsFieldHeader = false;
			labelFee.IsRequired = true;
			labelFee.Location = new System.Drawing.Point(639, 120);
			labelFee.Name = "labelFee";
			labelFee.PenWidth = 1f;
			labelFee.ShowBorder = false;
			labelFee.Size = new System.Drawing.Size(30, 13);
			labelFee.TabIndex = 20;
			labelFee.Text = "Fee:";
			labelFee.Visible = false;
			comboBoxFee.Assigned = false;
			comboBoxFee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFee.CustomReportFieldName = "";
			comboBoxFee.CustomReportKey = "";
			comboBoxFee.CustomReportValueType = 1;
			comboBoxFee.DescriptionTextBox = textBoxFeeName;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFee.DisplayLayout.Appearance = appearance17;
			comboBoxFee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFee.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxFee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFee.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxFee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFee.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFee.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxFee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFee.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFee.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxFee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFee.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFee.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxFee.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxFee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxFee.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxFee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFee.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxFee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFee.Editable = true;
			comboBoxFee.FilteredJobID = null;
			comboBoxFee.FilterString = "";
			comboBoxFee.HasAllAccount = false;
			comboBoxFee.HasCustom = false;
			comboBoxFee.IsDataLoaded = false;
			comboBoxFee.Location = new System.Drawing.Point(624, 84);
			comboBoxFee.MaxDropDownItems = 12;
			comboBoxFee.Name = "comboBoxFee";
			comboBoxFee.ShowInactiveItems = false;
			comboBoxFee.ShowQuickAdd = true;
			comboBoxFee.Size = new System.Drawing.Size(128, 20);
			comboBoxFee.TabIndex = 23;
			comboBoxFee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFee.Visible = false;
			textBoxFeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFeeName.CustomReportFieldName = "";
			textBoxFeeName.CustomReportKey = "";
			textBoxFeeName.CustomReportValueType = 1;
			textBoxFeeName.IsComboTextBox = false;
			textBoxFeeName.IsModified = false;
			textBoxFeeName.Location = new System.Drawing.Point(642, 103);
			textBoxFeeName.MaxLength = 64;
			textBoxFeeName.Name = "textBoxFeeName";
			textBoxFeeName.ReadOnly = true;
			textBoxFeeName.Size = new System.Drawing.Size(110, 20);
			textBoxFeeName.TabIndex = 26;
			textBoxFeeName.TabStop = false;
			textBoxFeeName.Visible = false;
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJob.CustomReportFieldName = "";
			comboBoxJob.CustomReportKey = "";
			comboBoxJob.CustomReportValueType = 1;
			comboBoxJob.DescriptionTextBox = textBoxJobName;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxJob.DisplayLayout.Appearance = appearance29;
			comboBoxJob.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxJob.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxJob.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxJob.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxJob.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxJob.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxJob.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxJob.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxJob.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxJob.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxJob.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxJob.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxJob.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxJob.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxJob.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxJob.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxJob.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxJob.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxJob.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxJob.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxJob.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxJob.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxJob.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxJob.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxJob.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxJob.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJob.Editable = true;
			comboBoxJob.FilterString = "";
			comboBoxJob.HasAllAccount = false;
			comboBoxJob.HasCustom = false;
			comboBoxJob.IsDataLoaded = false;
			comboBoxJob.Location = new System.Drawing.Point(99, 102);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(128, 20);
			comboBoxJob.TabIndex = 4;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxJobName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxJobName.CustomReportFieldName = "";
			textBoxJobName.CustomReportKey = "";
			textBoxJobName.CustomReportValueType = 1;
			textBoxJobName.IsComboTextBox = false;
			textBoxJobName.IsModified = false;
			textBoxJobName.Location = new System.Drawing.Point(230, 102);
			textBoxJobName.MaxLength = 64;
			textBoxJobName.Name = "textBoxJobName";
			textBoxJobName.ReadOnly = true;
			textBoxJobName.Size = new System.Drawing.Size(318, 20);
			textBoxJobName.TabIndex = 3;
			textBoxJobName.TabStop = false;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(99, 257);
			textBoxNote.MaxLength = 5000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(448, 50);
			textBoxNote.TabIndex = 14;
			textBoxDescription.BackColor = System.Drawing.Color.White;
			textBoxDescription.CustomReportFieldName = "";
			textBoxDescription.CustomReportKey = "";
			textBoxDescription.CustomReportValueType = 1;
			textBoxDescription.IsComboTextBox = false;
			textBoxDescription.IsModified = false;
			textBoxDescription.Location = new System.Drawing.Point(99, 58);
			textBoxDescription.MaxLength = 255;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.Size = new System.Drawing.Size(449, 20);
			textBoxDescription.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(99, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(197, 20);
			textBoxCode.TabIndex = 0;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(9, 264);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Note:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(9, 58);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(74, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Description:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(9, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(68, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Task Code:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(555, 612);
			base.Controls.Add(panelCommentBox);
			base.Controls.Add(jobTaskGroupTextBox);
			base.Controls.Add(jobTaskGroupComboBox);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(ultraLinkVoucherNumber);
			base.Controls.Add(mmLabel16);
			base.Controls.Add(comboBoxStatus);
			base.Controls.Add(dateTimeActualEndDate);
			base.Controls.Add(dateTimeEndDate);
			base.Controls.Add(dateTimeActualStartDate);
			base.Controls.Add(dateTimeStartDate);
			base.Controls.Add(textBoxCompletedDescription);
			base.Controls.Add(mmLabel15);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(numericUpDownTotalHours);
			base.Controls.Add(mmLabel13);
			base.Controls.Add(mmLabel14);
			base.Controls.Add(numericUpDownCompletePercent);
			base.Controls.Add(numericUpDownFeePercent);
			base.Controls.Add(comboBoxAssignedTo);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(mmLabel12);
			base.Controls.Add(mmLabel11);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(labelFeePercent);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(labelFee);
			base.Controls.Add(comboBoxFee);
			base.Controls.Add(comboBoxJob);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxFeeName);
			base.Controls.Add(textBoxAssignedToName);
			base.Controls.Add(textBoxJobName);
			base.Controls.Add(textBoxDescription);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "JobTaskDetailsForm";
			Text = "Project Task";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)numericUpDownFeePercent).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownCompletePercent).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownTotalHours).EndInit();
			((System.ComponentModel.ISupportInitialize)jobTaskGroupComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAssignedTo).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
