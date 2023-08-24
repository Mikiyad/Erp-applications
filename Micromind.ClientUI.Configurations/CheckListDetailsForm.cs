using DevExpress.XtraScheduler;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class CheckListDetailsForm : Form, IForm
	{
		private CheckListData currentData;

		private const string TABLENAME_CONST = "CheckList";

		private const string IDFIELD_CONST = "CheckListID";

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

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private UserComboBox comboBoxUser;

		private UserGroupComboBox comboBoxUserGroup;

		private ComboBox comboBoxAssigneeType;

		private MMLabel mmLabel2;

		private ComboBox comboBoxInterval;

		private MMLabel mmLabel3;

		private SchedulerStorage schedulerStorage1;

		private NumericUpDown numericUpDownDeadLine;

		private Label label1;

		private Label label2;

		private DateTimePicker dateTimePickerStartDate;

		private Label label3;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5021;

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

		public CheckListDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += CheckListDetailsForm_Load;
			comboBoxAssigneeType.SelectedIndexChanged += comboBoxAssigneeType_SelectedIndexChanged;
		}

		private void comboBoxAssigneeType_SelectedIndexChanged(object sender, EventArgs e)
		{
			comboBoxUser.Visible = (comboBoxAssigneeType.SelectedIndex == 0);
			comboBoxUserGroup.Visible = (comboBoxAssigneeType.SelectedIndex == 1);
		}

		private void comboBoxObjectType_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private bool GetData()
		{
			checked
			{
				try
				{
					if (currentData == null || isNewRecord)
					{
						currentData = new CheckListData();
					}
					DataRow dataRow = (!isNewRecord) ? currentData.CheckListTable.Rows[0] : currentData.CheckListTable.NewRow();
					dataRow.BeginEdit();
					dataRow["CheckListID"] = textBoxCode.Text.Trim();
					dataRow["CheckListName"] = textBoxName.Text.Trim();
					dataRow["CheckListType"] = (byte)1;
					dataRow["StartDate"] = new DateTime(dateTimePickerStartDate.Value.Year, dateTimePickerStartDate.Value.Month, dateTimePickerStartDate.Value.Day);
					dataRow["Interval"] = comboBoxInterval.SelectedIndex + 1;
					dataRow["DeadlineDays"] = numericUpDownDeadLine.Value;
					int num = comboBoxAssigneeType.SelectedIndex + 1;
					dataRow["ApproverType"] = num;
					if (num == 1)
					{
						dataRow["ApproverID"] = comboBoxUser.SelectedID;
					}
					else
					{
						dataRow["ApproverID"] = comboBoxUserGroup.SelectedID;
					}
					dataRow["IsInactive"] = checkBoxInactive.Checked;
					dataRow.EndEdit();
					if (isNewRecord)
					{
						currentData.CheckListTable.Rows.Add(dataRow);
					}
					return true;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return false;
				}
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
					currentData = Factory.CheckListSystem.GetCheckListByID(CheckListTypes.CheckList, id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
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
			checked
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["CheckListID"].ToString();
					textBoxName.Text = dataRow["CheckListName"].ToString();
					if (dataRow["StartDate"] != DBNull.Value)
					{
						dateTimePickerStartDate.Value = DateTime.Parse(dataRow["StartDate"].ToString());
					}
					else
					{
						dateTimePickerStartDate.Value = DateTime.Today;
					}
					if (!dataRow["Interval"].IsDBNullOrEmpty())
					{
						comboBoxInterval.SelectedIndex = int.Parse(dataRow["Interval"].ToString()) - 1;
					}
					else
					{
						comboBoxInterval.SelectedIndex = 1;
					}
					if (!dataRow["DeadlineDays"].IsDBNullOrEmpty())
					{
						numericUpDownDeadLine.Value = int.Parse(dataRow["DeadlineDays"].ToString());
					}
					else
					{
						numericUpDownDeadLine.Value = 1m;
					}
					int num = 0;
					if (!dataRow["ApproverType"].IsDBNullOrEmpty())
					{
						num = int.Parse(dataRow["ApproverType"].ToString()) - 1;
					}
					comboBoxAssigneeType.SelectedIndex = num;
					if (num == 0)
					{
						comboBoxUser.SelectedID = dataRow["ApproverID"].ToString();
					}
					else
					{
						comboBoxUserGroup.SelectedID = dataRow["ApproverID"].ToString();
					}
					if (dataRow["IsInactive"] != DBNull.Value)
					{
						checkBoxInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					}
					else
					{
						checkBoxInactive.Checked = false;
					}
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
				bool flag = (!isNewRecord) ? Factory.CheckListSystem.UpdateCheckList(currentData) : Factory.CheckListSystem.CreateCheckList(currentData);
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
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("CheckList", "CheckListID", textBoxCode.Text.Trim(), "CheckListType", 1))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("CheckList", "CheckListID", "CheckListType", 1.ToString());
			textBoxName.Clear();
			comboBoxInterval.SelectedIndex = 1;
			comboBoxUser.Clear();
			comboBoxUserGroup.Clear();
			checkBoxInactive.Checked = false;
			dateTimePickerStartDate.Value = DateTime.Today;
			numericUpDownDeadLine.Value = 1m;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void CheckListGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void CheckListGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.CheckListSystem.DeleteCheckList(CheckListTypes.CheckList, textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("CheckList", "CheckListID", textBoxCode.Text, "CheckListType", ((byte)1).ToString()));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("CheckList", "CheckListID", textBoxCode.Text, "CheckListType", ((byte)1).ToString()));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("CheckList", "CheckListID", "CheckListType", ((byte)1).ToString()));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("CheckList", "CheckListID", "CheckListType", ((byte)1).ToString()));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("CheckList", "CheckListID", toolStripTextBoxFind.Text.Trim(), "CheckListType", (byte)1))
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

		private void CheckListDetailsForm_Load(object sender, EventArgs e)
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
			new FormHelper().ShowList(DataComboType.CheckList);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.CheckListDetailsForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			comboBoxAssigneeType = new System.Windows.Forms.ComboBox();
			comboBoxInterval = new System.Windows.Forms.ComboBox();
			schedulerStorage1 = new DevExpress.XtraScheduler.SchedulerStorage(components);
			numericUpDownDeadLine = new System.Windows.Forms.NumericUpDown();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxUserGroup = new Micromind.DataControls.UserGroupComboBox();
			comboBoxUser = new Micromind.DataControls.UserComboBox();
			dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
			label3 = new System.Windows.Forms.Label();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)schedulerStorage1).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownDeadLine).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUserGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUser).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
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
				toolStripSeparator2
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(696, 31);
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
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 411);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(696, 40);
			panelButtons.TabIndex = 10;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(696, 1);
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
			buttonDelete.TabIndex = 6;
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
			xpButton1.Location = new System.Drawing.Point(586, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 7;
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
			buttonNew.TabIndex = 5;
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
			buttonSave.TabIndex = 4;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(307, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			comboBoxAssigneeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxAssigneeType.FormattingEnabled = true;
			comboBoxAssigneeType.Items.AddRange(new object[2]
			{
				"User",
				"User Role"
			});
			comboBoxAssigneeType.Location = new System.Drawing.Point(129, 103);
			comboBoxAssigneeType.Name = "comboBoxAssigneeType";
			comboBoxAssigneeType.Size = new System.Drawing.Size(131, 21);
			comboBoxAssigneeType.TabIndex = 3;
			comboBoxInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxInterval.FormattingEnabled = true;
			comboBoxInterval.Items.AddRange(new object[6]
			{
				"Daily",
				"Weekly",
				"Biweekly",
				"Monthly",
				"Quarterly",
				"Annually"
			});
			comboBoxInterval.Location = new System.Drawing.Point(129, 162);
			comboBoxInterval.Name = "comboBoxInterval";
			comboBoxInterval.Size = new System.Drawing.Size(131, 21);
			comboBoxInterval.TabIndex = 6;
			numericUpDownDeadLine.Location = new System.Drawing.Point(129, 210);
			numericUpDownDeadLine.Name = "numericUpDownDeadLine";
			numericUpDownDeadLine.Size = new System.Drawing.Size(60, 20);
			numericUpDownDeadLine.TabIndex = 8;
			numericUpDownDeadLine.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 212);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(52, 13);
			label1.TabIndex = 30;
			label1.Text = "Deadline:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(193, 212);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(31, 13);
			label2.TabIndex = 9;
			label2.Text = "Days";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(12, 165);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(54, 13);
			mmLabel3.TabIndex = 28;
			mmLabel3.Text = "Interval:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(12, 107);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(81, 13);
			mmLabel2.TabIndex = 26;
			mmLabel2.Text = "Assigned To:";
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
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(129, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(318, 20);
			textBoxName.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(129, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(172, 20);
			textBoxCode.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(12, 59);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(103, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "CheckList Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(12, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(100, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "CheckList Code:";
			comboBoxUserGroup.Assigned = false;
			comboBoxUserGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxUserGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUserGroup.CustomReportFieldName = "";
			comboBoxUserGroup.CustomReportKey = "";
			comboBoxUserGroup.CustomReportValueType = 1;
			comboBoxUserGroup.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxUserGroup.DisplayLayout.Appearance = appearance;
			comboBoxUserGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxUserGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUserGroup.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUserGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxUserGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUserGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxUserGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxUserGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxUserGroup.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxUserGroup.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxUserGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxUserGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxUserGroup.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxUserGroup.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxUserGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxUserGroup.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUserGroup.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxUserGroup.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxUserGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxUserGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxUserGroup.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxUserGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxUserGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxUserGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxUserGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxUserGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxUserGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxUserGroup.Editable = true;
			comboBoxUserGroup.FilterString = "";
			comboBoxUserGroup.HasAllAccount = false;
			comboBoxUserGroup.HasCustom = false;
			comboBoxUserGroup.IsDataLoaded = false;
			comboBoxUserGroup.Location = new System.Drawing.Point(129, 127);
			comboBoxUserGroup.MaxDropDownItems = 12;
			comboBoxUserGroup.Name = "comboBoxUserGroup";
			comboBoxUserGroup.ShowInactiveItems = false;
			comboBoxUserGroup.ShowQuickAdd = true;
			comboBoxUserGroup.Size = new System.Drawing.Size(172, 20);
			comboBoxUserGroup.TabIndex = 5;
			comboBoxUserGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxUserGroup.Visible = false;
			comboBoxUser.Assigned = false;
			comboBoxUser.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUser.CustomReportFieldName = "";
			comboBoxUser.CustomReportKey = "";
			comboBoxUser.CustomReportValueType = 1;
			comboBoxUser.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxUser.DisplayLayout.Appearance = appearance13;
			comboBoxUser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxUser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUser.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUser.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxUser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUser.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxUser.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxUser.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxUser.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxUser.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxUser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxUser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxUser.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxUser.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxUser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxUser.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUser.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxUser.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxUser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxUser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxUser.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxUser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxUser.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxUser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxUser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxUser.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxUser.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxUser.Editable = true;
			comboBoxUser.FilterString = "";
			comboBoxUser.HasAllAccount = false;
			comboBoxUser.HasCustom = false;
			comboBoxUser.IsDataLoaded = false;
			comboBoxUser.Location = new System.Drawing.Point(129, 127);
			comboBoxUser.MaxDropDownItems = 12;
			comboBoxUser.Name = "comboBoxUser";
			comboBoxUser.ShowInactiveItems = false;
			comboBoxUser.ShowQuickAdd = true;
			comboBoxUser.Size = new System.Drawing.Size(172, 20);
			comboBoxUser.TabIndex = 4;
			comboBoxUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxUser.Visible = false;
			dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerStartDate.Location = new System.Drawing.Point(129, 186);
			dateTimePickerStartDate.Name = "dateTimePickerStartDate";
			dateTimePickerStartDate.Size = new System.Drawing.Size(131, 20);
			dateTimePickerStartDate.TabIndex = 7;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 189);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(58, 13);
			label3.TabIndex = 30;
			label3.Text = "Start Date:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(696, 451);
			base.Controls.Add(dateTimePickerStartDate);
			base.Controls.Add(label2);
			base.Controls.Add(label3);
			base.Controls.Add(label1);
			base.Controls.Add(numericUpDownDeadLine);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(comboBoxInterval);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxUser);
			base.Controls.Add(comboBoxAssigneeType);
			base.Controls.Add(comboBoxUserGroup);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "CheckListDetailsForm";
			Text = "CheckList";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)schedulerStorage1).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownDeadLine).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUserGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUser).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
