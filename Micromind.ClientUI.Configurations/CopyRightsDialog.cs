using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class CopyRightsDialog : Form
	{
		private IContainer components;

		private UserGroupComboBox comboBoxUserGroup;

		private UserComboBox comboBoxUser;

		private MMTextBox textBoxGroupName;

		private XPButton buttonCancel;

		private XPButton buttonOK;

		private MMTextBox textBoxUserName;

		private RadioButton radioButtonUser;

		private RadioButton radioButtonGroup;

		public bool IsUser => radioButtonUser.Checked;

		public string SelectedID
		{
			get
			{
				if (IsUser)
				{
					return comboBoxUser.SelectedID;
				}
				return comboBoxUserGroup.SelectedID;
			}
		}

		public CopyRightsDialog()
		{
			InitializeComponent();
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxUser.Enabled = radioButtonUser.Checked;
			comboBoxUserGroup.Enabled = radioButtonGroup.Checked;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.None;
			if (radioButtonUser.Checked && comboBoxUser.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a user.");
			}
			else if (radioButtonGroup.Checked && comboBoxUserGroup.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Please select a user group.");
			}
			else
			{
				base.DialogResult = DialogResult.OK;
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
			comboBoxUserGroup = new Micromind.DataControls.UserGroupComboBox();
			textBoxGroupName = new Micromind.UISupport.MMTextBox();
			comboBoxUser = new Micromind.DataControls.UserComboBox();
			textBoxUserName = new Micromind.UISupport.MMTextBox();
			buttonCancel = new Micromind.UISupport.XPButton();
			buttonOK = new Micromind.UISupport.XPButton();
			radioButtonUser = new System.Windows.Forms.RadioButton();
			radioButtonGroup = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)comboBoxUserGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUser).BeginInit();
			SuspendLayout();
			comboBoxUserGroup.AlwaysInEditMode = true;
			comboBoxUserGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUserGroup.CustomReportFieldName = "";
			comboBoxUserGroup.CustomReportKey = "";
			comboBoxUserGroup.CustomReportValueType = 1;
			comboBoxUserGroup.DescriptionTextBox = textBoxGroupName;
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
			comboBoxUserGroup.Enabled = false;
			comboBoxUserGroup.FilterString = "";
			comboBoxUserGroup.HasAllAccount = false;
			comboBoxUserGroup.HasCustom = false;
			comboBoxUserGroup.IsDataLoaded = false;
			comboBoxUserGroup.Location = new System.Drawing.Point(94, 64);
			comboBoxUserGroup.MaxDropDownItems = 12;
			comboBoxUserGroup.Name = "comboBoxUserGroup";
			comboBoxUserGroup.ShowInactiveItems = false;
			comboBoxUserGroup.ShowQuickAdd = true;
			comboBoxUserGroup.Size = new System.Drawing.Size(179, 20);
			comboBoxUserGroup.TabIndex = 4;
			comboBoxUserGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxGroupName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGroupName.CustomReportFieldName = "";
			textBoxGroupName.CustomReportKey = "";
			textBoxGroupName.CustomReportValueType = 1;
			textBoxGroupName.ForeColor = System.Drawing.Color.Black;
			textBoxGroupName.IsComboTextBox = false;
			textBoxGroupName.Location = new System.Drawing.Point(94, 87);
			textBoxGroupName.MaxLength = 15;
			textBoxGroupName.Name = "textBoxGroupName";
			textBoxGroupName.ReadOnly = true;
			textBoxGroupName.Size = new System.Drawing.Size(290, 20);
			textBoxGroupName.TabIndex = 5;
			comboBoxUser.AlwaysInEditMode = true;
			comboBoxUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUser.CustomReportFieldName = "";
			comboBoxUser.CustomReportKey = "";
			comboBoxUser.CustomReportValueType = 1;
			comboBoxUser.DescriptionTextBox = textBoxUserName;
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
			comboBoxUser.Location = new System.Drawing.Point(94, 9);
			comboBoxUser.MaxDropDownItems = 12;
			comboBoxUser.Name = "comboBoxUser";
			comboBoxUser.ShowInactiveItems = false;
			comboBoxUser.ShowQuickAdd = true;
			comboBoxUser.Size = new System.Drawing.Size(179, 20);
			comboBoxUser.TabIndex = 1;
			comboBoxUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxUserName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUserName.CustomReportFieldName = "";
			textBoxUserName.CustomReportKey = "";
			textBoxUserName.CustomReportValueType = 1;
			textBoxUserName.ForeColor = System.Drawing.Color.Black;
			textBoxUserName.IsComboTextBox = false;
			textBoxUserName.Location = new System.Drawing.Point(94, 31);
			textBoxUserName.MaxLength = 15;
			textBoxUserName.Name = "textBoxUserName";
			textBoxUserName.ReadOnly = true;
			textBoxUserName.Size = new System.Drawing.Size(290, 20);
			textBoxUserName.TabIndex = 2;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(296, 131);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 7;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(194, 131);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 6;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			radioButtonUser.AutoSize = true;
			radioButtonUser.Checked = true;
			radioButtonUser.Location = new System.Drawing.Point(8, 9);
			radioButtonUser.Name = "radioButtonUser";
			radioButtonUser.Size = new System.Drawing.Size(50, 17);
			radioButtonUser.TabIndex = 0;
			radioButtonUser.TabStop = true;
			radioButtonUser.Text = "User:";
			radioButtonUser.UseVisualStyleBackColor = true;
			radioButtonUser.CheckedChanged += new System.EventHandler(radioButton2_CheckedChanged);
			radioButtonGroup.AutoSize = true;
			radioButtonGroup.Location = new System.Drawing.Point(8, 66);
			radioButtonGroup.Name = "radioButtonGroup";
			radioButtonGroup.Size = new System.Drawing.Size(82, 17);
			radioButtonGroup.TabIndex = 3;
			radioButtonGroup.Text = "User Group:";
			radioButtonGroup.UseVisualStyleBackColor = true;
			radioButtonGroup.CheckedChanged += new System.EventHandler(radioButton2_CheckedChanged);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(401, 162);
			base.Controls.Add(radioButtonGroup);
			base.Controls.Add(radioButtonUser);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(comboBoxUserGroup);
			base.Controls.Add(comboBoxUser);
			base.Controls.Add(textBoxUserName);
			base.Controls.Add(textBoxGroupName);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CopyRightsDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Copy Access Rights";
			((System.ComponentModel.ISupportInitialize)comboBoxUserGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUser).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
