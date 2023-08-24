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
	public class CampaignDetailsForm : Form, IForm
	{
		private CampaignData currentData;

		private const string TABLENAME_CONST = "Campaign";

		private const string IDFIELD_CONST = "CampaignID";

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

		private DateTimePicker dateTimeStartDate;

		private MMLabel labelCloseDate;

		private AmountTextBox textBoxBudgCost;

		private MMLabel mmLabel2;

		private MMLabel mmLabel1;

		private PercentTextBox textBoxExpResponse;

		private DateTimePicker dateTimeEndDate;

		private MMLabel mmLabel3;

		private MMLabel mmLabel5;

		private NumberTextBox textBoxNumSent;

		private MMLabel mmLabel7;

		private AmountTextBox textBoxActCost;

		private MMLabel mmLabel8;

		private AmountTextBox textBoxExpRevenue;

		private CampaignTypeComboBox comboBoxCampaignType;

		private CampaignStatusComboBox comboBoxCampaignStatus;

		private CheckBox checkBoxIsInactive;

		private ToolStripButton toolStripButtonInformation;

		private MMLabel mmLabel6;

		private MMLabel labelType;

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

		public CampaignDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += CampaignDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CampaignData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CampaignTable.Rows[0] : currentData.CampaignTable.NewRow();
				dataRow.BeginEdit();
				dataRow["CampaignID"] = textBoxCode.Text.Trim();
				dataRow["CampaignName"] = textBoxName.Text.Trim();
				dataRow["Type"] = comboBoxCampaignType.SelectedID;
				dataRow["Status"] = comboBoxCampaignStatus.SelectedID;
				dataRow["StartDate"] = dateTimeStartDate.Value;
				dataRow["EndDate"] = dateTimeStartDate.Value;
				if (textBoxNumSent.Text != "")
				{
					dataRow["NumberSent"] = int.Parse(textBoxNumSent.Text);
				}
				else
				{
					dataRow["NumberSent"] = DBNull.Value;
				}
				if (textBoxExpResponse.Text != "")
				{
					dataRow["ExpectedResponse"] = byte.Parse(textBoxExpResponse.Text);
				}
				else
				{
					dataRow["ExpectedResponse"] = DBNull.Value;
				}
				if (textBoxBudgCost.Text != "")
				{
					dataRow["BudgetedCost"] = decimal.Parse(textBoxBudgCost.Text);
				}
				else
				{
					dataRow["BudgetedCost"] = DBNull.Value;
				}
				if (textBoxActCost.Text != "")
				{
					dataRow["ActualCost"] = decimal.Parse(textBoxActCost.Text);
				}
				else
				{
					dataRow["ActualCost"] = DBNull.Value;
				}
				if (textBoxExpRevenue.Text != "")
				{
					dataRow["ExpectedRevenue"] = decimal.Parse(textBoxExpRevenue.Text);
				}
				else
				{
					dataRow["ExpectedRevenue"] = DBNull.Value;
				}
				dataRow["IsInactive"] = checkBoxIsInactive.Checked;
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CampaignTable.Rows.Add(dataRow);
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
					currentData = Factory.CampaignSystem.GetCampaignByID(id.Trim());
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
				textBoxCode.Text = dataRow["CampaignID"].ToString();
				textBoxName.Text = dataRow["CampaignName"].ToString();
				comboBoxCampaignType.SelectedID = int.Parse(dataRow["Type"].ToString());
				comboBoxCampaignStatus.SelectedID = int.Parse(dataRow["Status"].ToString());
				dateTimeStartDate.Value = DateTime.Parse(dataRow["StartDate"].ToString());
				dateTimeEndDate.Value = DateTime.Parse(dataRow["EndDate"].ToString());
				textBoxNumSent.Text = dataRow["NumberSent"].ToString();
				textBoxExpResponse.Text = dataRow["ExpectedResponse"].ToString();
				textBoxBudgCost.Text = dataRow["BudgetedCost"].ToString();
				textBoxActCost.Text = dataRow["ActualCost"].ToString();
				textBoxExpRevenue.Text = dataRow["ExpectedRevenue"].ToString();
				checkBoxIsInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
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
					flag = Factory.CampaignSystem.CreateCampaign(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Campaign, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.CampaignSystem.UpdateCampaign(currentData);
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
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Campaign", "CampaignID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Campaign", "CampaignID");
			textBoxName.Clear();
			comboBoxCampaignType.LoadData();
			comboBoxCampaignType.SelectedIndex = 0;
			comboBoxCampaignStatus.LoadData();
			comboBoxCampaignStatus.SelectedIndex = 0;
			DateTime dateTime2 = dateTimeStartDate.Value = (dateTimeEndDate.Value = DateTime.Now);
			textBoxNumSent.Clear();
			textBoxExpResponse.Clear();
			textBoxBudgCost.Clear();
			textBoxActCost.Clear();
			textBoxExpRevenue.Clear();
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
				bool num = Factory.CampaignSystem.DeleteCampaign(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Campaign, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("Campaign", "CampaignID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Campaign", "CampaignID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Campaign", "CampaignID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Campaign", "CampaignID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Campaign", "CampaignID", toolStripTextBoxFind.Text.Trim()))
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

		private void CampaignDetailsForm_Load(object sender, EventArgs e)
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
			new FormHelper().ShowList(DataComboType.Campaign);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.CampaignDetailsForm));
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
			dateTimeStartDate = new System.Windows.Forms.DateTimePicker();
			dateTimeEndDate = new System.Windows.Forms.DateTimePicker();
			checkBoxIsInactive = new System.Windows.Forms.CheckBox();
			comboBoxCampaignStatus = new Micromind.DataControls.CampaignStatusComboBox();
			comboBoxCampaignType = new Micromind.DataControls.CampaignTypeComboBox();
			textBoxExpRevenue = new Micromind.UISupport.AmountTextBox();
			mmLabel8 = new Micromind.UISupport.MMLabel();
			textBoxActCost = new Micromind.UISupport.AmountTextBox();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			textBoxNumSent = new Micromind.UISupport.NumberTextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxBudgCost = new Micromind.UISupport.AmountTextBox();
			textBoxExpResponse = new Micromind.UISupport.PercentTextBox();
			labelCloseDate = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			labelName = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			labelType = new Micromind.UISupport.MMLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
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
			toolStrip1.Size = new System.Drawing.Size(565, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 341);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(565, 40);
			panelButtons.TabIndex = 50;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(565, 1);
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
			buttonDelete.TabIndex = 15;
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
			xpButton1.Location = new System.Drawing.Point(455, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 16;
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
			buttonNew.TabIndex = 14;
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
			buttonSave.TabIndex = 13;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			dateTimeStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeStartDate.Location = new System.Drawing.Point(127, 106);
			dateTimeStartDate.Name = "dateTimeStartDate";
			dateTimeStartDate.Size = new System.Drawing.Size(185, 20);
			dateTimeStartDate.TabIndex = 5;
			dateTimeEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeEndDate.Location = new System.Drawing.Point(388, 106);
			dateTimeEndDate.Name = "dateTimeEndDate";
			dateTimeEndDate.Size = new System.Drawing.Size(164, 20);
			dateTimeEndDate.TabIndex = 6;
			checkBoxIsInactive.BackColor = System.Drawing.Color.Transparent;
			checkBoxIsInactive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxIsInactive.Location = new System.Drawing.Point(327, 36);
			checkBoxIsInactive.Name = "checkBoxIsInactive";
			checkBoxIsInactive.Size = new System.Drawing.Size(65, 16);
			checkBoxIsInactive.TabIndex = 1;
			checkBoxIsInactive.Text = "Inactive";
			checkBoxIsInactive.UseVisualStyleBackColor = false;
			comboBoxCampaignStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxCampaignStatus.FormattingEnabled = true;
			comboBoxCampaignStatus.Location = new System.Drawing.Point(388, 81);
			comboBoxCampaignStatus.Name = "comboBoxCampaignStatus";
			comboBoxCampaignStatus.Size = new System.Drawing.Size(164, 21);
			comboBoxCampaignStatus.TabIndex = 4;
			comboBoxCampaignType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxCampaignType.FormattingEnabled = true;
			comboBoxCampaignType.Location = new System.Drawing.Point(127, 81);
			comboBoxCampaignType.Name = "comboBoxCampaignType";
			comboBoxCampaignType.Size = new System.Drawing.Size(185, 21);
			comboBoxCampaignType.TabIndex = 3;
			textBoxExpRevenue.CustomReportFieldName = "";
			textBoxExpRevenue.CustomReportKey = "";
			textBoxExpRevenue.CustomReportValueType = 1;
			textBoxExpRevenue.IsComboTextBox = false;
			textBoxExpRevenue.Location = new System.Drawing.Point(127, 180);
			textBoxExpRevenue.Name = "textBoxExpRevenue";
			textBoxExpRevenue.Size = new System.Drawing.Size(185, 20);
			textBoxExpRevenue.TabIndex = 11;
			textBoxExpRevenue.Text = "0.00";
			textBoxExpRevenue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxExpRevenue.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel8.AutoSize = true;
			mmLabel8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel8.IsFieldHeader = false;
			mmLabel8.IsRequired = false;
			mmLabel8.Location = new System.Drawing.Point(15, 183);
			mmLabel8.Name = "mmLabel8";
			mmLabel8.PenWidth = 1f;
			mmLabel8.ShowBorder = false;
			mmLabel8.Size = new System.Drawing.Size(102, 13);
			mmLabel8.TabIndex = 46;
			mmLabel8.Text = "Expected Revenue:";
			textBoxActCost.CustomReportFieldName = "";
			textBoxActCost.CustomReportKey = "";
			textBoxActCost.CustomReportValueType = 1;
			textBoxActCost.IsComboTextBox = false;
			textBoxActCost.Location = new System.Drawing.Point(397, 155);
			textBoxActCost.Name = "textBoxActCost";
			textBoxActCost.Size = new System.Drawing.Size(155, 20);
			textBoxActCost.TabIndex = 10;
			textBoxActCost.Text = "0.00";
			textBoxActCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxActCost.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(327, 158);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(64, 13);
			mmLabel7.TabIndex = 44;
			mmLabel7.Text = "Actual Cost:";
			textBoxNumSent.AllowDecimal = true;
			textBoxNumSent.CustomReportFieldName = "";
			textBoxNumSent.CustomReportKey = "";
			textBoxNumSent.CustomReportValueType = 1;
			textBoxNumSent.IsComboTextBox = false;
			textBoxNumSent.Location = new System.Drawing.Point(127, 130);
			textBoxNumSent.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxNumSent.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxNumSent.Name = "textBoxNumSent";
			textBoxNumSent.NullText = "0";
			textBoxNumSent.Size = new System.Drawing.Size(185, 20);
			textBoxNumSent.TabIndex = 7;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(15, 133);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(57, 13);
			mmLabel5.TabIndex = 42;
			mmLabel5.Text = "Num Sent:";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(327, 110);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(55, 13);
			mmLabel3.TabIndex = 41;
			mmLabel3.Text = "End Date:";
			textBoxBudgCost.CustomReportFieldName = "";
			textBoxBudgCost.CustomReportKey = "";
			textBoxBudgCost.CustomReportValueType = 1;
			textBoxBudgCost.IsComboTextBox = false;
			textBoxBudgCost.Location = new System.Drawing.Point(127, 155);
			textBoxBudgCost.Name = "textBoxBudgCost";
			textBoxBudgCost.Size = new System.Drawing.Size(185, 20);
			textBoxBudgCost.TabIndex = 9;
			textBoxBudgCost.Text = "0.00";
			textBoxBudgCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBudgCost.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxExpResponse.BackColor = System.Drawing.Color.White;
			textBoxExpResponse.CustomReportFieldName = "";
			textBoxExpResponse.CustomReportKey = "";
			textBoxExpResponse.CustomReportValueType = 1;
			textBoxExpResponse.IsComboTextBox = false;
			textBoxExpResponse.Location = new System.Drawing.Point(456, 130);
			textBoxExpResponse.Name = "textBoxExpResponse";
			textBoxExpResponse.Size = new System.Drawing.Size(96, 20);
			textBoxExpResponse.TabIndex = 8;
			textBoxExpResponse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelCloseDate.AutoSize = true;
			labelCloseDate.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCloseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelCloseDate.IsFieldHeader = false;
			labelCloseDate.IsRequired = false;
			labelCloseDate.Location = new System.Drawing.Point(14, 110);
			labelCloseDate.Name = "labelCloseDate";
			labelCloseDate.PenWidth = 1f;
			labelCloseDate.ShowBorder = false;
			labelCloseDate.Size = new System.Drawing.Size(58, 13);
			labelCloseDate.TabIndex = 31;
			labelCloseDate.Text = "Start Date:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(15, 158);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(80, 13);
			mmLabel2.TabIndex = 28;
			mmLabel2.Text = "Budgeted Cost:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(327, 133);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(123, 13);
			mmLabel1.TabIndex = 26;
			mmLabel1.Text = "Expected Response (%):";
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
			textBoxNote.Location = new System.Drawing.Point(127, 205);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(425, 106);
			textBoxNote.TabIndex = 12;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(127, 57);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(425, 20);
			textBoxName.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(127, 33);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(185, 20);
			textBoxCode.TabIndex = 0;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(15, 208);
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
			labelName.Location = new System.Drawing.Point(13, 60);
			labelName.Name = "labelName";
			labelName.PenWidth = 1f;
			labelName.ShowBorder = false;
			labelName.Size = new System.Drawing.Size(102, 13);
			labelName.TabIndex = 3;
			labelName.Text = "Campaign Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(12, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(99, 13);
			labelCode.TabIndex = 1;
			labelCode.Text = "Campaign Code:";
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(327, 84);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(47, 13);
			mmLabel6.TabIndex = 39;
			mmLabel6.Text = "Status:";
			labelType.AutoSize = true;
			labelType.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelType.IsFieldHeader = false;
			labelType.IsRequired = false;
			labelType.Location = new System.Drawing.Point(12, 84);
			labelType.Name = "labelType";
			labelType.PenWidth = 1f;
			labelType.ShowBorder = false;
			labelType.Size = new System.Drawing.Size(39, 13);
			labelType.TabIndex = 24;
			labelType.Text = "Type:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(565, 381);
			base.Controls.Add(checkBoxIsInactive);
			base.Controls.Add(comboBoxCampaignStatus);
			base.Controls.Add(comboBoxCampaignType);
			base.Controls.Add(textBoxExpRevenue);
			base.Controls.Add(mmLabel8);
			base.Controls.Add(textBoxActCost);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(textBoxNumSent);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(dateTimeEndDate);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(textBoxBudgCost);
			base.Controls.Add(textBoxExpResponse);
			base.Controls.Add(dateTimeStartDate);
			base.Controls.Add(labelCloseDate);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelType);
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
			base.Name = "CampaignDetailsForm";
			Text = "Campaign Details";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(CampaignDetailsForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
