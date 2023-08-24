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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class OpportunityDetailsForm : Form, IForm
	{
		private OpportunityData currentData;

		private const string TABLENAME_CONST = "Opportunity";

		private const string IDFIELD_CONST = "OpportunityID";

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

		private MMLabel labelName;

		private MMTextBox textBoxName;

		private MMLabel mmLabel4;

		private MMTextBox textBoxNote;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonAttach;

		private MMLabel labelJobStatus;

		private OpportunityStageComboBox comboBoxStage;

		private MMLabel mmLabel1;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimeCloseDate;

		private MMLabel labelCloseDate;

		private MMLabel mmLabel3;

		private PercentTextBox textBoxProbability;

		private AmountTextBox textBoxAmount;

		private MMTextBox textBoxCampaignName;

		private CampaignComboBox comboBoxCampaign;

		private ToolStripButton toolStripButtonInformation;

		private CRMRelatedSelector crmRelatedSelector1;

		private MMTextBox textBoxLeadName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private SalespersonComboBox comboBoxLeadOwner;

		private MMTextBox textBoxLeadOwnerName;

		private MMLabel mmLabel6;

		private DateTimePicker dateTimePickerDueDate;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

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

		public OpportunityDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += OpportunityDetailsForm_Load;
			comboBoxStage.SelectedIndexChanged += comboBoxStage_SelectedIndexChanged;
			crmRelatedSelector1.SelectedItemChanged += crmRelatedSelector1_SelectedItemChanged;
		}

		private void crmRelatedSelector1_SelectedItemChanged(object sender, EventArgs e)
		{
			textBoxLeadName.Text = crmRelatedSelector1.SelectedName;
		}

		private void comboBoxStage_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxStage.SelectedID == 1)
			{
				textBoxProbability.Text = "10";
			}
			else if (comboBoxStage.SelectedID == 2)
			{
				textBoxProbability.Text = "10";
			}
			else if (comboBoxStage.SelectedID == 3)
			{
				textBoxProbability.Text = "20";
			}
			else if (comboBoxStage.SelectedID == 4)
			{
				textBoxProbability.Text = "50";
			}
			else if (comboBoxStage.SelectedID == 5)
			{
				textBoxProbability.Text = "60";
			}
			else if (comboBoxStage.SelectedID == 6)
			{
				textBoxProbability.Text = "70";
			}
			else if (comboBoxStage.SelectedID == 7)
			{
				textBoxProbability.Text = "80";
			}
			else if (comboBoxStage.SelectedID == 8)
			{
				textBoxProbability.Text = "90";
			}
			else if (comboBoxStage.SelectedID == 9)
			{
				textBoxProbability.Text = "100";
			}
			else
			{
				textBoxProbability.Text = "0";
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new OpportunityData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.OpportunityTable.Rows[0] : currentData.OpportunityTable.NewRow();
				dataRow.BeginEdit();
				dataRow["OpportunityID"] = textBoxCode.Text.Trim();
				dataRow["OpportunityName"] = textBoxName.Text.Trim();
				dataRow["Status"] = comboBoxStage.SelectedID;
				dataRow["ClosingDate"] = dateTimeCloseDate.Value;
				dataRow["DueDate"] = dateTimePickerDueDate.Value;
				dataRow["Probability"] = textBoxProbability.Text;
				dataRow["Amount"] = textBoxAmount.Text;
				dataRow["RelatedType"] = (int)crmRelatedSelector1.SelectedType;
				dataRow["RelatedID"] = crmRelatedSelector1.SelectedID;
				dataRow["Note"] = textBoxNote.Text;
				if (comboBoxLeadOwner.SelectedID != "")
				{
					dataRow["OwnerID"] = comboBoxLeadOwner.SelectedID;
				}
				else
				{
					dataRow["OwnerID"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.OpportunityTable.Rows.Add(dataRow);
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
					currentData = Factory.OpportunitySystem.GetOpportunityByID(id.Trim());
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
				textBoxCode.Text = dataRow["OpportunityID"].ToString();
				textBoxName.Text = dataRow["OpportunityName"].ToString();
				comboBoxStage.SelectedID = int.Parse(dataRow["Status"].ToString());
				if (!dataRow["ClosingDate"].IsDBNullOrEmpty())
				{
					dateTimeCloseDate.Value = DateTime.Parse(dataRow["ClosingDate"].ToString());
				}
				if (!dataRow["DueDate"].IsDBNullOrEmpty())
				{
					dateTimePickerDueDate.Value = DateTime.Parse(dataRow["DueDate"].ToString());
				}
				textBoxProbability.Text = dataRow["Probability"].ToString();
				textBoxAmount.Text = dataRow["Amount"].ToString();
				if (!dataRow["RelatedType"].IsDBNullOrEmpty())
				{
					crmRelatedSelector1.SelectedType = (CRMRelatedTypes)int.Parse(dataRow["RelatedType"].ToString());
				}
				else
				{
					crmRelatedSelector1.SelectedType = CRMRelatedTypes.Lead;
				}
				crmRelatedSelector1.SelectedID = dataRow["RelatedID"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				comboBoxLeadOwner.SelectedID = dataRow["OwnerID"].ToString();
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
					flag = Factory.OpportunitySystem.CreateOpportunity(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Opportunity, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.OpportunitySystem.UpdateOpportunity(currentData);
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
				ErrorHelper.InformationMessage("Please enter required fields in bold.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Opportunity", "OpportunityID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Opportunity", "OpportunityID");
			textBoxName.Clear();
			comboBoxStage.LoadData();
			comboBoxStage.SelectedIndex = 1;
			dateTimeCloseDate.Value = DateTime.Now;
			dateTimePickerDueDate.Value = DateTime.Now;
			textBoxAmount.Clear();
			comboBoxLeadOwner.Clear();
			textBoxLeadName.Clear();
			crmRelatedSelector1.Clear();
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
				bool num = Factory.OpportunitySystem.DeleteOpportunity(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Opportunity, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("Opportunity", "OpportunityID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Opportunity", "OpportunityID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Opportunity", "OpportunityID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Opportunity", "OpportunityID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Opportunity", "OpportunityID", toolStripTextBoxFind.Text.Trim()))
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

		private void OpportunityDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					crmRelatedSelector1.LoadData();
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
			new FormHelper().ShowList(DataComboType.Opportunity);
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
					docManagementForm.EntityType = EntityTypesEnum.Opportunities;
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

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxLeadOwner.Text);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCampaign(comboBoxCampaign.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.OpportunityDetailsForm));
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
			dateTimeCloseDate = new System.Windows.Forms.DateTimePicker();
			textBoxCampaignName = new Micromind.UISupport.MMTextBox();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			textBoxProbability = new Micromind.UISupport.PercentTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			labelCloseDate = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			comboBoxStage = new Micromind.DataControls.OpportunityStageComboBox();
			labelJobStatus = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			labelName = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxCampaign = new Micromind.DataControls.CampaignComboBox();
			crmRelatedSelector1 = new Micromind.DataControls.CRMRelatedSelector();
			textBoxLeadName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxLeadOwner = new Micromind.DataControls.SalespersonComboBox();
			textBoxLeadOwnerName = new Micromind.UISupport.MMTextBox();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			dateTimePickerDueDate = new System.Windows.Forms.DateTimePicker();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCampaign).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadOwner).BeginInit();
			SuspendLayout();
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
			toolStrip1.Size = new System.Drawing.Size(674, 31);
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
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(86, 28);
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
			panelButtons.Location = new System.Drawing.Point(0, 404);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(674, 40);
			panelButtons.TabIndex = 14;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(674, 1);
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
			buttonDelete.TabIndex = 10;
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
			xpButton1.Location = new System.Drawing.Point(564, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 11;
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
			buttonNew.TabIndex = 9;
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
			buttonSave.TabIndex = 8;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			dateTimeCloseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeCloseDate.Location = new System.Drawing.Point(317, 107);
			dateTimeCloseDate.Name = "dateTimeCloseDate";
			dateTimeCloseDate.Size = new System.Drawing.Size(113, 20);
			dateTimeCloseDate.TabIndex = 5;
			textBoxCampaignName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCampaignName.CustomReportFieldName = "";
			textBoxCampaignName.CustomReportKey = "";
			textBoxCampaignName.CustomReportValueType = 1;
			textBoxCampaignName.IsComboTextBox = false;
			textBoxCampaignName.Location = new System.Drawing.Point(262, 155);
			textBoxCampaignName.MaxLength = 64;
			textBoxCampaignName.Name = "textBoxCampaignName";
			textBoxCampaignName.ReadOnly = true;
			textBoxCampaignName.Size = new System.Drawing.Size(352, 20);
			textBoxCampaignName.TabIndex = 10;
			textBoxCampaignName.TabStop = false;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.Location = new System.Drawing.Point(500, 106);
			textBoxAmount.Name = "textBoxAmount";
			textBoxAmount.Size = new System.Drawing.Size(114, 20);
			textBoxAmount.TabIndex = 6;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxProbability.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProbability.CustomReportFieldName = "";
			textBoxProbability.CustomReportKey = "";
			textBoxProbability.CustomReportValueType = 1;
			textBoxProbability.IsComboTextBox = false;
			textBoxProbability.Location = new System.Drawing.Point(429, 82);
			textBoxProbability.Name = "textBoxProbability";
			textBoxProbability.ReadOnly = true;
			textBoxProbability.Size = new System.Drawing.Size(185, 20);
			textBoxProbability.TabIndex = 3;
			textBoxProbability.TabStop = false;
			textBoxProbability.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(13, 134);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(59, 13);
			mmLabel3.TabIndex = 32;
			mmLabel3.Text = "Related to:";
			labelCloseDate.AutoSize = true;
			labelCloseDate.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCloseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelCloseDate.IsFieldHeader = false;
			labelCloseDate.IsRequired = false;
			labelCloseDate.Location = new System.Drawing.Point(248, 110);
			labelCloseDate.Name = "labelCloseDate";
			labelCloseDate.PenWidth = 1f;
			labelCloseDate.ShowBorder = false;
			labelCloseDate.Size = new System.Drawing.Size(62, 13);
			labelCloseDate.TabIndex = 3;
			labelCloseDate.Text = "Close Date:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(448, 109);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(46, 13);
			mmLabel2.TabIndex = 28;
			mmLabel2.Text = "Amount:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(348, 85);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(75, 13);
			mmLabel1.TabIndex = 26;
			mmLabel1.Text = "Probability (%):";
			comboBoxStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStage.FormattingEnabled = true;
			comboBoxStage.Location = new System.Drawing.Point(127, 82);
			comboBoxStage.Name = "comboBoxStage";
			comboBoxStage.Size = new System.Drawing.Size(185, 21);
			comboBoxStage.TabIndex = 2;
			labelJobStatus.AutoSize = true;
			labelJobStatus.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelJobStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelJobStatus.IsFieldHeader = false;
			labelJobStatus.IsRequired = false;
			labelJobStatus.Location = new System.Drawing.Point(13, 85);
			labelJobStatus.Name = "labelJobStatus";
			labelJobStatus.PenWidth = 1f;
			labelJobStatus.ShowBorder = false;
			labelJobStatus.Size = new System.Drawing.Size(44, 13);
			labelJobStatus.TabIndex = 24;
			labelJobStatus.Text = "Stage:";
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
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(127, 208);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(533, 189);
			textBoxNote.TabIndex = 13;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(127, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(487, 20);
			textBoxName.TabIndex = 1;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(127, 34);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(185, 20);
			textBoxCode.TabIndex = 0;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(14, 208);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Note:";
			labelName.AutoSize = true;
			labelName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelName.IsFieldHeader = false;
			labelName.IsRequired = true;
			labelName.Location = new System.Drawing.Point(13, 61);
			labelName.Name = "labelName";
			labelName.PenWidth = 1f;
			labelName.ShowBorder = false;
			labelName.Size = new System.Drawing.Size(112, 13);
			labelName.TabIndex = 3;
			labelName.Text = "Opportunity Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(13, 37);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(109, 13);
			labelCode.TabIndex = 1;
			labelCode.Text = "Opportunity Code:";
			comboBoxCampaign.Assigned = false;
			comboBoxCampaign.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCampaign.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCampaign.CustomReportFieldName = "";
			comboBoxCampaign.CustomReportKey = "";
			comboBoxCampaign.CustomReportValueType = 1;
			comboBoxCampaign.DescriptionTextBox = textBoxCampaignName;
			comboBoxCampaign.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCampaign.Editable = true;
			comboBoxCampaign.FilterString = "";
			comboBoxCampaign.HasAllAccount = false;
			comboBoxCampaign.HasCustom = false;
			comboBoxCampaign.IsDataLoaded = false;
			comboBoxCampaign.Location = new System.Drawing.Point(127, 155);
			comboBoxCampaign.MaxDropDownItems = 12;
			comboBoxCampaign.Name = "comboBoxCampaign";
			comboBoxCampaign.ShowInactiveItems = false;
			comboBoxCampaign.ShowQuickAdd = true;
			comboBoxCampaign.Size = new System.Drawing.Size(129, 20);
			comboBoxCampaign.TabIndex = 9;
			comboBoxCampaign.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			crmRelatedSelector1.BackColor = System.Drawing.Color.Transparent;
			crmRelatedSelector1.IsFlag = 0;
			crmRelatedSelector1.Location = new System.Drawing.Point(127, 133);
			crmRelatedSelector1.MaximumSize = new System.Drawing.Size(2000, 20);
			crmRelatedSelector1.MinimumSize = new System.Drawing.Size(0, 20);
			crmRelatedSelector1.Name = "crmRelatedSelector1";
			crmRelatedSelector1.SelectedID = "";
			crmRelatedSelector1.SelectedType = Micromind.Common.Data.CRMRelatedTypes.Lead;
			crmRelatedSelector1.Size = new System.Drawing.Size(185, 20);
			crmRelatedSelector1.TabIndex = 7;
			textBoxLeadName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLeadName.CustomReportFieldName = "";
			textBoxLeadName.CustomReportKey = "";
			textBoxLeadName.CustomReportValueType = 1;
			textBoxLeadName.IsComboTextBox = false;
			textBoxLeadName.Location = new System.Drawing.Point(318, 133);
			textBoxLeadName.MaxLength = 64;
			textBoxLeadName.Name = "textBoxLeadName";
			textBoxLeadName.ReadOnly = true;
			textBoxLeadName.Size = new System.Drawing.Size(296, 20);
			textBoxLeadName.TabIndex = 8;
			textBoxLeadName.TabStop = false;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(12, 181);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(95, 14);
			ultraFormattedLinkLabel3.TabIndex = 68;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Opportunity Owner:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			comboBoxLeadOwner.Assigned = false;
			comboBoxLeadOwner.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLeadOwner.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLeadOwner.CustomReportFieldName = "";
			comboBoxLeadOwner.CustomReportKey = "";
			comboBoxLeadOwner.CustomReportValueType = 1;
			comboBoxLeadOwner.DescriptionTextBox = textBoxLeadOwnerName;
			appearance2.BackColor = System.Drawing.SystemColors.Window;
			appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLeadOwner.DisplayLayout.Appearance = appearance2;
			comboBoxLeadOwner.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLeadOwner.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance3.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeadOwner.DisplayLayout.GroupByBox.Appearance = appearance3;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeadOwner.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
			comboBoxLeadOwner.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance5.BackColor2 = System.Drawing.SystemColors.Control;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeadOwner.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
			comboBoxLeadOwner.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLeadOwner.DisplayLayout.MaxRowScrollRegions = 1;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLeadOwner.DisplayLayout.Override.ActiveCellAppearance = appearance6;
			appearance7.BackColor = System.Drawing.SystemColors.Highlight;
			appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLeadOwner.DisplayLayout.Override.ActiveRowAppearance = appearance7;
			comboBoxLeadOwner.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLeadOwner.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLeadOwner.DisplayLayout.Override.CardAreaAppearance = appearance8;
			appearance9.BorderColor = System.Drawing.Color.Silver;
			appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLeadOwner.DisplayLayout.Override.CellAppearance = appearance9;
			comboBoxLeadOwner.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLeadOwner.DisplayLayout.Override.CellPadding = 0;
			appearance10.BackColor = System.Drawing.SystemColors.Control;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeadOwner.DisplayLayout.Override.GroupByRowAppearance = appearance10;
			appearance11.TextHAlignAsString = "Left";
			comboBoxLeadOwner.DisplayLayout.Override.HeaderAppearance = appearance11;
			comboBoxLeadOwner.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLeadOwner.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			comboBoxLeadOwner.DisplayLayout.Override.RowAppearance = appearance12;
			comboBoxLeadOwner.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLeadOwner.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
			comboBoxLeadOwner.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLeadOwner.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLeadOwner.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLeadOwner.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLeadOwner.Editable = true;
			comboBoxLeadOwner.FilterString = "";
			comboBoxLeadOwner.HasAllAccount = false;
			comboBoxLeadOwner.HasCustom = false;
			comboBoxLeadOwner.IsDataLoaded = false;
			comboBoxLeadOwner.Location = new System.Drawing.Point(127, 179);
			comboBoxLeadOwner.MaxDropDownItems = 12;
			comboBoxLeadOwner.Name = "comboBoxLeadOwner";
			comboBoxLeadOwner.ShowInactiveItems = false;
			comboBoxLeadOwner.ShowQuickAdd = true;
			comboBoxLeadOwner.Size = new System.Drawing.Size(129, 20);
			comboBoxLeadOwner.TabIndex = 11;
			comboBoxLeadOwner.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxLeadOwnerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLeadOwnerName.CustomReportFieldName = "";
			textBoxLeadOwnerName.CustomReportKey = "";
			textBoxLeadOwnerName.CustomReportValueType = 1;
			textBoxLeadOwnerName.IsComboTextBox = false;
			textBoxLeadOwnerName.Location = new System.Drawing.Point(262, 179);
			textBoxLeadOwnerName.MaxLength = 64;
			textBoxLeadOwnerName.Name = "textBoxLeadOwnerName";
			textBoxLeadOwnerName.ReadOnly = true;
			textBoxLeadOwnerName.Size = new System.Drawing.Size(352, 20);
			textBoxLeadOwnerName.TabIndex = 12;
			textBoxLeadOwnerName.TabStop = false;
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(14, 108);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(56, 13);
			mmLabel6.TabIndex = 3;
			mmLabel6.Text = "Due Date:";
			dateTimePickerDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDueDate.Location = new System.Drawing.Point(127, 107);
			dateTimePickerDueDate.Name = "dateTimePickerDueDate";
			dateTimePickerDueDate.Size = new System.Drawing.Size(115, 20);
			dateTimePickerDueDate.TabIndex = 4;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 155);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(54, 14);
			ultraFormattedLinkLabel1.TabIndex = 69;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Campaign:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(674, 444);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(ultraFormattedLinkLabel3);
			base.Controls.Add(comboBoxLeadOwner);
			base.Controls.Add(textBoxLeadOwnerName);
			base.Controls.Add(crmRelatedSelector1);
			base.Controls.Add(textBoxLeadName);
			base.Controls.Add(comboBoxCampaign);
			base.Controls.Add(textBoxCampaignName);
			base.Controls.Add(textBoxAmount);
			base.Controls.Add(textBoxProbability);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(dateTimePickerDueDate);
			base.Controls.Add(dateTimeCloseDate);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(labelCloseDate);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(comboBoxStage);
			base.Controls.Add(labelJobStatus);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(labelName);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "OpportunityDetailsForm";
			Text = "Opportunity Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxCampaign).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadOwner).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
