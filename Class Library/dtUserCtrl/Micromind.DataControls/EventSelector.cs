using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class EventSelector : UserControl
	{
		private IContainer components;

		private leadsFlatComboBox comboBoxFromLead;

		private leadsFlatComboBox comboBoxToLead;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private RadioButton radioButtonUser;

		private Label label2;

		private Label label3;

		private Label label4;

		private RadioButton radioButtonSingle;

		private leadsFlatComboBox comboBoxSingleLead;

		private UserComboBox comboBoxFromUser;

		private UserComboBox comboBoxToUser;

		public string FromLead
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleLead.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromLead.SelectedID;
				}
				return "";
			}
		}

		public string ToLead
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleLead.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToLead.SelectedID;
				}
				return "";
			}
		}

		public string FromUser
		{
			get
			{
				if (radioButtonUser.Checked)
				{
					return comboBoxFromUser.SelectedID;
				}
				return "";
			}
		}

		public string ToUser
		{
			get
			{
				if (radioButtonUser.Checked)
				{
					return comboBoxToUser.SelectedID;
				}
				return "";
			}
		}

		public EventSelector()
		{
			InitializeComponent();
		}

		private void EventSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleLead.Enabled = radioButtonSingle.Checked;
			leadsFlatComboBox leadsFlatComboBox = comboBoxFromLead;
			bool enabled = comboBoxToLead.Enabled = radioButtonRange.Checked;
			leadsFlatComboBox.Enabled = enabled;
			UserComboBox userComboBox = comboBoxFromUser;
			enabled = (comboBoxToUser.Enabled = radioButtonUser.Checked);
			userComboBox.Enabled = enabled;
		}

		private void radioButtons_CheckedChanged(object sender, EventArgs e)
		{
			EnableDisableControls();
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
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			radioButtonUser = new System.Windows.Forms.RadioButton();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxToLead = new Micromind.DataControls.leadsFlatComboBox();
			comboBoxSingleLead = new Micromind.DataControls.leadsFlatComboBox();
			comboBoxFromLead = new Micromind.DataControls.leadsFlatComboBox();
			comboBoxFromUser = new Micromind.DataControls.UserComboBox();
			comboBoxToUser = new Micromind.DataControls.UserComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxToLead).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleLead).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLead).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToUser).BeginInit();
			SuspendLayout();
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(265, 29);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(68, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Leads";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 28);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(87, 17);
			radioButtonRange.TabIndex = 3;
			radioButtonRange.Text = "Lead Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonUser.AutoSize = true;
			radioButtonUser.Location = new System.Drawing.Point(6, 50);
			radioButtonUser.Name = "radioButtonUser";
			radioButtonUser.Size = new System.Drawing.Size(87, 17);
			radioButtonUser.TabIndex = 7;
			radioButtonUser.Text = "Assigned To:";
			radioButtonUser.UseVisualStyleBackColor = true;
			radioButtonUser.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(265, 52);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 6;
			label2.Text = "To:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(116, 29);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 4;
			label3.Text = "From:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(116, 52);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 6;
			label4.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(100, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxToLead.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToLead.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToLead.DisplayLayout.Appearance = appearance;
			comboBoxToLead.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToLead.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLead.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLead.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxToLead.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLead.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxToLead.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToLead.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToLead.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToLead.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxToLead.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToLead.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToLead.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToLead.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxToLead.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToLead.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLead.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxToLead.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxToLead.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToLead.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxToLead.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxToLead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToLead.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxToLead.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToLead.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToLead.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToLead.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToLead.Editable = true;
			comboBoxToLead.Enabled = false;
			comboBoxToLead.FilterString = "";
			comboBoxToLead.HasAll = false;
			comboBoxToLead.HasCustom = false;
			comboBoxToLead.Location = new System.Drawing.Point(294, 26);
			comboBoxToLead.MaxDropDownItems = 12;
			comboBoxToLead.Name = "comboBoxToLead";
			comboBoxToLead.ShowInactiveItems = false;
			comboBoxToLead.ShowQuickAdd = true;
			comboBoxToLead.Size = new System.Drawing.Size(103, 20);
			comboBoxToLead.TabIndex = 6;
			comboBoxToLead.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSingleLead.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleLead.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleLead.DisplayLayout.Appearance = appearance13;
			comboBoxSingleLead.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleLead.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleLead.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleLead.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxSingleLead.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleLead.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxSingleLead.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleLead.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleLead.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleLead.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxSingleLead.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleLead.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleLead.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleLead.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxSingleLead.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleLead.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleLead.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxSingleLead.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxSingleLead.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleLead.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleLead.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxSingleLead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleLead.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxSingleLead.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleLead.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleLead.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleLead.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleLead.Editable = true;
			comboBoxSingleLead.Enabled = false;
			comboBoxSingleLead.FilterString = "";
			comboBoxSingleLead.HasAll = false;
			comboBoxSingleLead.HasCustom = false;
			comboBoxSingleLead.Location = new System.Drawing.Point(161, 3);
			comboBoxSingleLead.MaxDropDownItems = 12;
			comboBoxSingleLead.Name = "comboBoxSingleLead";
			comboBoxSingleLead.ShowInactiveItems = false;
			comboBoxSingleLead.ShowQuickAdd = true;
			comboBoxSingleLead.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleLead.TabIndex = 2;
			comboBoxSingleLead.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromLead.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromLead.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromLead.DisplayLayout.Appearance = appearance25;
			comboBoxFromLead.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromLead.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLead.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLead.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxFromLead.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLead.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxFromLead.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromLead.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromLead.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromLead.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxFromLead.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromLead.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromLead.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromLead.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxFromLead.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromLead.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLead.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxFromLead.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxFromLead.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromLead.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromLead.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxFromLead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromLead.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxFromLead.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromLead.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromLead.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromLead.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromLead.Editable = true;
			comboBoxFromLead.Enabled = false;
			comboBoxFromLead.FilterString = "";
			comboBoxFromLead.HasAll = false;
			comboBoxFromLead.HasCustom = false;
			comboBoxFromLead.Location = new System.Drawing.Point(161, 26);
			comboBoxFromLead.MaxDropDownItems = 12;
			comboBoxFromLead.Name = "comboBoxFromLead";
			comboBoxFromLead.ShowInactiveItems = false;
			comboBoxFromLead.ShowQuickAdd = true;
			comboBoxFromLead.Size = new System.Drawing.Size(103, 20);
			comboBoxFromLead.TabIndex = 5;
			comboBoxFromLead.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromUser.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromUser.DescriptionTextBox = null;
			comboBoxFromUser.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromUser.Editable = true;
			comboBoxFromUser.Enabled = false;
			comboBoxFromUser.FilterString = "";
			comboBoxFromUser.HasAllAccount = false;
			comboBoxFromUser.HasCustom = false;
			comboBoxFromUser.Location = new System.Drawing.Point(161, 49);
			comboBoxFromUser.MaxDropDownItems = 12;
			comboBoxFromUser.Name = "comboBoxFromUser";
			comboBoxFromUser.ShowInactiveItems = false;
			comboBoxFromUser.ShowQuickAdd = true;
			comboBoxFromUser.Size = new System.Drawing.Size(103, 20);
			comboBoxFromUser.TabIndex = 13;
			comboBoxFromUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToUser.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToUser.DescriptionTextBox = null;
			comboBoxToUser.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToUser.Editable = true;
			comboBoxToUser.Enabled = false;
			comboBoxToUser.FilterString = "";
			comboBoxToUser.HasAllAccount = false;
			comboBoxToUser.HasCustom = false;
			comboBoxToUser.Location = new System.Drawing.Point(294, 49);
			comboBoxToUser.MaxDropDownItems = 12;
			comboBoxToUser.Name = "comboBoxToUser";
			comboBoxToUser.ShowInactiveItems = false;
			comboBoxToUser.ShowQuickAdd = true;
			comboBoxToUser.Size = new System.Drawing.Size(103, 20);
			comboBoxToUser.TabIndex = 14;
			comboBoxToUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToUser);
			base.Controls.Add(comboBoxFromUser);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonUser);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(label4);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(labelTo);
			base.Controls.Add(comboBoxToLead);
			base.Controls.Add(comboBoxSingleLead);
			base.Controls.Add(comboBoxFromLead);
			base.Name = "EventSelector";
			base.Size = new System.Drawing.Size(414, 77);
			base.Load += new System.EventHandler(EventSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxToLead).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleLead).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLead).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromUser).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToUser).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
