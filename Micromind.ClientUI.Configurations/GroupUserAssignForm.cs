using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
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
	public class GroupUserAssignForm : Form, IForm
	{
		private DataSet currentData;

		private const string TABLENAME_CONST = "Users";

		private const string IDFIELD_CONST = "UserID";

		private bool isNewRecord = true;

		private string userID = "";

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private MMLabel labelCode;

		private MMTextBox textBoxUserName;

		private FormManager formManager;

		private Label label1;

		private Label label2;

		private CheckedListBox listBoxGroups;

		private UserGroupComboBox comboBoxGroup;

		public ScreenAreas ScreenArea => ScreenAreas.Company;

		public int ScreenID => 8002;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		public string UserID
		{
			set
			{
				userID = value;
			}
		}

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
			}
		}

		public GroupUserAssignForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += GroupUserAssignForm_Load;
			comboBoxGroup.SelectedIndexChanged += comboBoxGroup_SelectedIndexChanged;
		}

		private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxUserName.Text = comboBoxGroup.SelectedName;
			LoadData(comboBoxGroup.SelectedID);
		}

		private bool GetData()
		{
			try
			{
				UserGroupData userGroupData = new UserGroupData();
				foreach (object checkedItem in listBoxGroups.CheckedItems)
				{
					NameValue nameValue = checkedItem as NameValue;
					DataRow dataRow = userGroupData.UserGroupDetailTable.NewRow();
					dataRow["UserID"] = nameValue.ID;
					dataRow["GroupID"] = comboBoxGroup.SelectedID;
					dataRow.EndEdit();
					userGroupData.UserGroupDetailTable.Rows.Add(dataRow);
				}
				currentData = userGroupData;
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
			if (SaveData())
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.None;
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!(id.Trim() == "") && CanClose())
				{
					currentData = Factory.UserGroupSystem.GetUsersByGroup(id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
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
				listBoxGroups.Items.Clear();
				foreach (DataRow row in currentData.Tables[0].Rows)
				{
					NameValue item = new NameValue(row["UserName"].ToString(), row["UserID"].ToString());
					bool result = false;
					bool.TryParse(row["IsMember"].ToString(), out result);
					listBoxGroups.Items.Add(item, result);
				}
			}
		}

		private bool SaveData()
		{
			switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
			{
			case DialogResult.No:
				return true;
			case DialogResult.Cancel:
				return false;
			default:
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
					bool flag = true;
					flag = Factory.UserGroupSystem.AssignUsersToGroup(comboBoxGroup.SelectedID, (UserGroupData)currentData);
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
		}

		private bool ValidateData()
		{
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
			listBoxGroups.Items.Clear();
		}

		private void UserGroupGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void UserGroupGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
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

		private void GroupUserAssignForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxGroup.LoadData();
				IsNewRecord = true;
				ClearForm();
				comboBoxGroup.SelectedID = userID;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.UserGroup);
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
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			labelCode = new Micromind.UISupport.MMLabel();
			textBoxUserName = new Micromind.UISupport.MMTextBox();
			formManager = new Micromind.DataControls.FormManager();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			listBoxGroups = new System.Windows.Forms.CheckedListBox();
			comboBoxGroup = new Micromind.DataControls.UserGroupComboBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGroup).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 312);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(496, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(496, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(386, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
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
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 9);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(62, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Group ID:";
			textBoxUserName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUserName.CustomReportFieldName = "";
			textBoxUserName.CustomReportKey = "";
			textBoxUserName.CustomReportValueType = 1;
			textBoxUserName.ForeColor = System.Drawing.Color.Black;
			textBoxUserName.IsComboTextBox = false;
			textBoxUserName.Location = new System.Drawing.Point(90, 31);
			textBoxUserName.MaxLength = 15;
			textBoxUserName.Name = "textBoxUserName";
			textBoxUserName.ReadOnly = true;
			textBoxUserName.Size = new System.Drawing.Size(290, 20);
			textBoxUserName.TabIndex = 1;
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
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(8, 34);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(63, 13);
			label1.TabIndex = 18;
			label1.Text = "User Name:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 72);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(213, 13);
			label2.TabIndex = 20;
			label2.Text = "Select user groups that this user belongs to:";
			listBoxGroups.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			listBoxGroups.FormattingEnabled = true;
			listBoxGroups.Location = new System.Drawing.Point(11, 91);
			listBoxGroups.Name = "listBoxGroups";
			listBoxGroups.Size = new System.Drawing.Size(470, 214);
			listBoxGroups.TabIndex = 2;
			comboBoxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGroup.CustomReportFieldName = "";
			comboBoxGroup.CustomReportKey = "";
			comboBoxGroup.CustomReportValueType = 1;
			comboBoxGroup.DescriptionTextBox = textBoxUserName;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGroup.DisplayLayout.Appearance = appearance;
			comboBoxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGroup.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxGroup.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxGroup.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGroup.Editable = true;
			comboBoxGroup.FilterString = "";
			comboBoxGroup.HasAllAccount = false;
			comboBoxGroup.HasCustom = false;
			comboBoxGroup.IsDataLoaded = false;
			comboBoxGroup.Location = new System.Drawing.Point(90, 7);
			comboBoxGroup.MaxDropDownItems = 12;
			comboBoxGroup.Name = "comboBoxGroup";
			comboBoxGroup.ShowInactiveItems = false;
			comboBoxGroup.ShowQuickAdd = true;
			comboBoxGroup.Size = new System.Drawing.Size(194, 20);
			comboBoxGroup.TabIndex = 21;
			comboBoxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(496, 352);
			base.Controls.Add(comboBoxGroup);
			base.Controls.Add(listBoxGroups);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxUserName);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "GroupUserAssignForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			Text = "Assign User Groups";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxGroup).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
