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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class PrintTemplateMappingForm : Form, IForm
	{
		private PrintTemplateMapData currentData;

		private const string TABLENAME_CONST = "Print_Template_Map";

		private const string IDFIELD_CONST = "MapID";

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

		private MMTextBox textBoxMapID;

		private MMLabel labelName;

		private MMTextBox textBoxTemplateName;

		private ToolStripTextBox toolStripTextBoxFind;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private MMLabel labelCloseDate;

		private MMLabel mmLabel5;

		private MMLabel mmLabel9;

		private ComboBox comboBoxScreenType;

		private MMLabel mmLabel1;

		private ComboBox comboBoxScreenArea;

		private ComboBox comboBoxScreenID;

		private MMLabel mmLabel2;

		private XPButton buttonSelectTemplatePath;

		private MMTextBox textBoxFileName;

		private MMTextBox textBoxScreenName;

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
					textBoxMapID.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxMapID.ReadOnly = true;
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

		public PrintTemplateMappingForm()
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
					currentData = new PrintTemplateMapData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PrintTemplateMapTable.Rows[0] : currentData.PrintTemplateMapTable.NewRow();
				dataRow.BeginEdit();
				dataRow["MapID"] = textBoxMapID.Text.Trim();
				dataRow["TemplateName"] = textBoxTemplateName.Text.Trim();
				dataRow["ScreenType"] = comboBoxScreenType.SelectedIndex;
				dataRow["ScreenID"] = comboBoxScreenID.Text;
				dataRow["FileName"] = textBoxFileName.Text;
				dataRow["ScreenArea"] = comboBoxScreenArea.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.PrintTemplateMapTable.Rows.Add(dataRow);
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
			textBoxMapID.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.PrintTemplateMapSystem.GetPrintTemplateMapByID(id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxMapID.Text = id;
						textBoxMapID.Focus();
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
				textBoxMapID.Text = dataRow["MapID"].ToString();
				textBoxTemplateName.Text = dataRow["TemplateName"].ToString();
				LoadScreenTypes();
				comboBoxScreenType.SelectedIndex = int.Parse(dataRow["ScreenType"].ToString());
				comboBoxScreenArea.Text = dataRow["ScreenArea"].ToString();
				LoadScreens(comboBoxScreenType.Text.ToString(), "");
				comboBoxScreenID.Text = dataRow["ScreenID"].ToString();
				textBoxFileName.Text = dataRow["FileName"].ToString();
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
					flag = Factory.PrintTemplateMapSystem.CreatePrintTemplateMap(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Campaign, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.PrintTemplateMapSystem.UpdatePrintTemplateMap(currentData);
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
			if (textBoxMapID.Text.Trim().Length == 0 || textBoxTemplateName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields in bold.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Print_Template_Map", "MapID", textBoxMapID.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxMapID.Focus();
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
			textBoxMapID.Text = PublicFunctions.GetNextCardNumber("Print_Template_Map", "MapID");
			textBoxTemplateName.Clear();
			textBoxFileName.Clear();
			LoadScreenTypes();
			comboBoxScreenType.SelectedIndex = 4;
			comboBoxScreenID.Items.Clear();
			textBoxScreenName.Clear();
			formManager.ResetDirty();
			textBoxMapID.Focus();
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
				bool num = Factory.PrintTemplateMapSystem.DeletePrintTemplateMap(textBoxMapID.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.PrintTemplateMap, needRefresh: true);
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
			LoadData(DatabaseHelper.GetNextID("Print_Template_Map", "MapID", textBoxMapID.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Print_Template_Map", "MapID", textBoxMapID.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Print_Template_Map", "MapID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Print_Template_Map", "MapID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Print_Template_Map", "MapID", toolStripTextBoxFind.Text.Trim()))
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

		private void LoadScreenTypes()
		{
			comboBoxScreenType.Items.Clear();
			ScreenTypes[] array = (ScreenTypes[])Enum.GetValues(typeof(ScreenTypes));
			for (int i = 0; i < array.Length; i++)
			{
				ScreenTypes screenTypes = array[i];
				comboBoxScreenType.Items.Add(screenTypes.ToString());
			}
		}

		private void LoadSubScreens(string typeID)
		{
			comboBoxScreenArea.Items.Clear();
			if (typeID != null)
			{
				foreach (DataRow row in new DataView(new FormHelper().GetSecurityFormList().Tables[2], "[ScreenType]='" + typeID + "'", "ScreenType", DataViewRowState.CurrentRows).ToTable(true, "ScreenSubArea").Copy().Rows)
				{
					comboBoxScreenArea.Items.Add(row["ScreenSubArea"].ToString());
				}
			}
		}

		private void LoadScreens(string typeID, string screenArea)
		{
			comboBoxScreenID.Items.Clear();
			if (typeID != "" && screenArea != "")
			{
				foreach (DataRow row in new DataView(new FormHelper().GetSecurityFormList().Tables[2], "ScreenType='" + typeID + "' AND ScreenSubArea='" + screenArea + "'", "ScreenID", DataViewRowState.CurrentRows).ToTable(true, "ScreenID").Copy().Rows)
				{
					comboBoxScreenID.Items.Add(row["ScreenID"].ToString());
				}
			}
			else if (typeID != "")
			{
				foreach (DataRow row2 in new DataView(new FormHelper().GetSecurityFormList().Tables[2], "ScreenType='" + typeID + "'", "ScreenID", DataViewRowState.CurrentRows).ToTable(true, "ScreenID").Copy().Rows)
				{
					comboBoxScreenID.Items.Add(row2["ScreenID"].ToString());
				}
			}
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
				LoadScreenTypes();
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
			new FormHelper().ShowList(DataComboType.PrintTemplateMap);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxMapID.Text;
					docManagementForm.EntityName = textBoxTemplateName.Text;
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
				FormHelper.ShowDocumentInfo(textBoxMapID.Text, "", this);
			}
		}

		private void comboBoxScreenArea_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(comboBoxScreenType.SelectedItem.ToString()) && !string.IsNullOrEmpty(comboBoxScreenArea.SelectedItem.ToString()))
			{
				LoadScreens(comboBoxScreenType.Text.ToString(), comboBoxScreenArea.Text.ToString());
			}
		}

		private void comboBoxScreenType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(comboBoxScreenType.SelectedItem.ToString()))
			{
				LoadSubScreens(comboBoxScreenType.Text.ToString());
			}
		}

		private void buttonSelectTemplatePath_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			string printTemplatePath = PrintHelper.PrintTemplatePath;
			openFileDialog.InitialDirectory = printTemplatePath + "\\Reports";
			openFileDialog.DefaultExt = "*.repx";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxFileName.Text = openFileDialog.SafeFileName.Replace(".repx", "");
			}
		}

		private void comboBoxScreenID_TextChanged(object sender, EventArgs e)
		{
			textBoxScreenName.Text = comboBoxScreenID.Text;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.PrintTemplateMappingForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			comboBoxScreenType = new System.Windows.Forms.ComboBox();
			comboBoxScreenArea = new System.Windows.Forms.ComboBox();
			comboBoxScreenID = new System.Windows.Forms.ComboBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			buttonSelectTemplatePath = new Micromind.UISupport.XPButton();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel9 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			labelCloseDate = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxTemplateName = new Micromind.UISupport.MMTextBox();
			textBoxMapID = new Micromind.UISupport.MMTextBox();
			labelName = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			textBoxFileName = new Micromind.UISupport.MMTextBox();
			textBoxScreenName = new Micromind.UISupport.MMTextBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			SuspendLayout();
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
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(641, 31);
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
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 190);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(641, 40);
			panelButtons.TabIndex = 50;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(641, 1);
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
			xpButton1.Location = new System.Drawing.Point(531, 8);
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
			comboBoxScreenType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxScreenType.FormattingEnabled = true;
			comboBoxScreenType.Location = new System.Drawing.Point(127, 81);
			comboBoxScreenType.Name = "comboBoxScreenType";
			comboBoxScreenType.Size = new System.Drawing.Size(138, 21);
			comboBoxScreenType.TabIndex = 3;
			comboBoxScreenType.SelectedIndexChanged += new System.EventHandler(comboBoxScreenType_SelectedIndexChanged);
			comboBoxScreenArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxScreenArea.FormattingEnabled = true;
			comboBoxScreenArea.Location = new System.Drawing.Point(348, 82);
			comboBoxScreenArea.Name = "comboBoxScreenArea";
			comboBoxScreenArea.Size = new System.Drawing.Size(185, 21);
			comboBoxScreenArea.TabIndex = 4;
			comboBoxScreenArea.SelectedIndexChanged += new System.EventHandler(comboBoxScreenArea_SelectedIndexChanged);
			comboBoxScreenID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxScreenID.FormattingEnabled = true;
			comboBoxScreenID.Location = new System.Drawing.Point(127, 107);
			comboBoxScreenID.Name = "comboBoxScreenID";
			comboBoxScreenID.Size = new System.Drawing.Size(185, 21);
			comboBoxScreenID.TabIndex = 5;
			comboBoxScreenID.TextChanged += new System.EventHandler(comboBoxScreenID_TextChanged);
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(433, 136);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(30, 13);
			mmLabel2.TabIndex = 123;
			mmLabel2.Text = ".repx";
			buttonSelectTemplatePath.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectTemplatePath.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectTemplatePath.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectTemplatePath.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectTemplatePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectTemplatePath.Location = new System.Drawing.Point(465, 132);
			buttonSelectTemplatePath.Name = "buttonSelectTemplatePath";
			buttonSelectTemplatePath.Size = new System.Drawing.Size(25, 21);
			buttonSelectTemplatePath.TabIndex = 7;
			buttonSelectTemplatePath.Text = "...";
			buttonSelectTemplatePath.UseVisualStyleBackColor = false;
			buttonSelectTemplatePath.Click += new System.EventHandler(buttonSelectTemplatePath_Click);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(273, 86);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(69, 13);
			mmLabel1.TabIndex = 52;
			mmLabel1.Text = "Screen Area:";
			mmLabel9.AutoSize = true;
			mmLabel9.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel9.IsFieldHeader = false;
			mmLabel9.IsRequired = false;
			mmLabel9.Location = new System.Drawing.Point(12, 85);
			mmLabel9.Name = "mmLabel9";
			mmLabel9.PenWidth = 1f;
			mmLabel9.ShowBorder = false;
			mmLabel9.Size = new System.Drawing.Size(71, 13);
			mmLabel9.TabIndex = 51;
			mmLabel9.Text = "Screen Type:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(12, 136);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(57, 13);
			mmLabel5.TabIndex = 42;
			mmLabel5.Text = "File Name:";
			labelCloseDate.AutoSize = true;
			labelCloseDate.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCloseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelCloseDate.IsFieldHeader = false;
			labelCloseDate.IsRequired = false;
			labelCloseDate.Location = new System.Drawing.Point(12, 111);
			labelCloseDate.Name = "labelCloseDate";
			labelCloseDate.PenWidth = 1f;
			labelCloseDate.ShowBorder = false;
			labelCloseDate.Size = new System.Drawing.Size(58, 13);
			labelCloseDate.TabIndex = 31;
			labelCloseDate.Text = "Screen ID:";
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
			textBoxTemplateName.BackColor = System.Drawing.Color.White;
			textBoxTemplateName.CustomReportFieldName = "";
			textBoxTemplateName.CustomReportKey = "";
			textBoxTemplateName.CustomReportValueType = 1;
			textBoxTemplateName.IsComboTextBox = false;
			textBoxTemplateName.IsModified = false;
			textBoxTemplateName.Location = new System.Drawing.Point(127, 57);
			textBoxTemplateName.MaxLength = 64;
			textBoxTemplateName.Name = "textBoxTemplateName";
			textBoxTemplateName.Size = new System.Drawing.Size(185, 20);
			textBoxTemplateName.TabIndex = 2;
			textBoxMapID.BackColor = System.Drawing.Color.White;
			textBoxMapID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxMapID.CustomReportFieldName = "";
			textBoxMapID.CustomReportKey = "";
			textBoxMapID.CustomReportValueType = 1;
			textBoxMapID.IsComboTextBox = false;
			textBoxMapID.IsModified = false;
			textBoxMapID.Location = new System.Drawing.Point(127, 33);
			textBoxMapID.MaxLength = 15;
			textBoxMapID.Name = "textBoxMapID";
			textBoxMapID.Size = new System.Drawing.Size(138, 20);
			textBoxMapID.TabIndex = 0;
			labelName.AutoSize = true;
			labelName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelName.IsFieldHeader = false;
			labelName.IsRequired = true;
			labelName.Location = new System.Drawing.Point(12, 61);
			labelName.Name = "labelName";
			labelName.PenWidth = 1f;
			labelName.ShowBorder = false;
			labelName.Size = new System.Drawing.Size(99, 13);
			labelName.TabIndex = 3;
			labelName.Text = "Template Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(12, 37);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(52, 13);
			labelCode.TabIndex = 1;
			labelCode.Text = "Map ID:";
			textBoxFileName.BackColor = System.Drawing.Color.White;
			textBoxFileName.CustomReportFieldName = "";
			textBoxFileName.CustomReportKey = "";
			textBoxFileName.CustomReportValueType = 1;
			textBoxFileName.IsComboTextBox = false;
			textBoxFileName.IsModified = false;
			textBoxFileName.Location = new System.Drawing.Point(127, 132);
			textBoxFileName.MaxLength = 64;
			textBoxFileName.Name = "textBoxFileName";
			textBoxFileName.Size = new System.Drawing.Size(309, 20);
			textBoxFileName.TabIndex = 6;
			textBoxScreenName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxScreenName.CustomReportFieldName = "";
			textBoxScreenName.CustomReportKey = "";
			textBoxScreenName.CustomReportValueType = 1;
			textBoxScreenName.IsComboTextBox = false;
			textBoxScreenName.IsModified = false;
			textBoxScreenName.Location = new System.Drawing.Point(318, 107);
			textBoxScreenName.MaxLength = 64;
			textBoxScreenName.Name = "textBoxScreenName";
			textBoxScreenName.ReadOnly = true;
			textBoxScreenName.Size = new System.Drawing.Size(297, 20);
			textBoxScreenName.TabIndex = 127;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(641, 230);
			base.Controls.Add(textBoxScreenName);
			base.Controls.Add(textBoxFileName);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(buttonSelectTemplatePath);
			base.Controls.Add(comboBoxScreenID);
			base.Controls.Add(comboBoxScreenArea);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(comboBoxScreenType);
			base.Controls.Add(mmLabel9);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(labelCloseDate);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxTemplateName);
			base.Controls.Add(textBoxMapID);
			base.Controls.Add(labelName);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "PrintTemplateMappingForm";
			Text = "Print Template Mapping";
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
