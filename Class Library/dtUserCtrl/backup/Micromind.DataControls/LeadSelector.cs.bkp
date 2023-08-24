using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class LeadSelector : UserControl
	{
		private IContainer components;

		private leadsFlatComboBox comboBoxFromLead;

		private leadsFlatComboBox comboBoxToLead;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonLocation;

		private Label label1;

		private Label label3;

		private Label label5;

		private RadioButton radioButtonSingle;

		private leadsFlatComboBox comboBoxSingleLead;

		private LocationComboBox comboBoxFromLocation;

		private LocationComboBox comboBoxToLocation;

		private RadioButton radioButtonRange;

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

		public string FromLocation
		{
			get
			{
				if (radioButtonLocation.Checked)
				{
					return comboBoxFromLocation.SelectedID;
				}
				return "";
			}
		}

		public string ToLocation
		{
			get
			{
				if (radioButtonLocation.Checked)
				{
					return comboBoxToLocation.SelectedID;
				}
				return "";
			}
		}

		public LeadSelector()
		{
			InitializeComponent();
		}

		private void LeadSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleLead.Enabled = radioButtonSingle.Checked;
			leadsFlatComboBox leadsFlatComboBox = comboBoxFromLead;
			bool enabled = comboBoxToLead.Enabled = radioButtonRange.Checked;
			leadsFlatComboBox.Enabled = enabled;
			LocationComboBox locationComboBox = comboBoxFromLocation;
			enabled = (comboBoxToLocation.Enabled = radioButtonLocation.Checked);
			locationComboBox.Enabled = enabled;
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
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonLocation = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxToLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxFromLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxToLead = new Micromind.DataControls.leadsFlatComboBox();
			comboBoxSingleLead = new Micromind.DataControls.leadsFlatComboBox();
			comboBoxFromLead = new Micromind.DataControls.leadsFlatComboBox();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToLead).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleLead).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLead).BeginInit();
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
			radioButtonLocation.AutoSize = true;
			radioButtonLocation.Location = new System.Drawing.Point(6, 51);
			radioButtonLocation.Name = "radioButtonLocation";
			radioButtonLocation.Size = new System.Drawing.Size(69, 17);
			radioButtonLocation.TabIndex = 10;
			radioButtonLocation.Text = "Location:";
			radioButtonLocation.UseVisualStyleBackColor = true;
			radioButtonLocation.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(265, 52);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(23, 13);
			label1.TabIndex = 11;
			label1.Text = "To:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(116, 29);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 4;
			label3.Text = "From:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(116, 52);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(33, 13);
			label5.TabIndex = 11;
			label5.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(100, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxToLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToLocation.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToLocation.DisplayLayout.Appearance = appearance;
			comboBoxToLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxToLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxToLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToLocation.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToLocation.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToLocation.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxToLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToLocation.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxToLocation.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxToLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxToLocation.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxToLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxToLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToLocation.Editable = true;
			comboBoxToLocation.Enabled = false;
			comboBoxToLocation.FilterString = "";
			comboBoxToLocation.HasAllAccount = false;
			comboBoxToLocation.HasCustom = false;
			comboBoxToLocation.Location = new System.Drawing.Point(294, 49);
			comboBoxToLocation.MaxDropDownItems = 12;
			comboBoxToLocation.Name = "comboBoxToLocation";
			comboBoxToLocation.ShowAll = false;
			comboBoxToLocation.ShowConsignIn = false;
			comboBoxToLocation.ShowConsignOut = false;
			comboBoxToLocation.ShowInactiveItems = false;
			comboBoxToLocation.ShowNormalLocations = true;
			comboBoxToLocation.ShowPOSOnly = false;
			comboBoxToLocation.ShowQuickAdd = true;
			comboBoxToLocation.ShowWarehouseOnly = false;
			comboBoxToLocation.Size = new System.Drawing.Size(103, 20);
			comboBoxToLocation.TabIndex = 12;
			comboBoxToLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromLocation.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromLocation.DisplayLayout.Appearance = appearance13;
			comboBoxFromLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromLocation.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromLocation.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromLocation.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromLocation.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromLocation.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromLocation.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromLocation.Editable = true;
			comboBoxFromLocation.Enabled = false;
			comboBoxFromLocation.FilterString = "";
			comboBoxFromLocation.HasAllAccount = false;
			comboBoxFromLocation.HasCustom = false;
			comboBoxFromLocation.Location = new System.Drawing.Point(161, 49);
			comboBoxFromLocation.MaxDropDownItems = 12;
			comboBoxFromLocation.Name = "comboBoxFromLocation";
			comboBoxFromLocation.ShowAll = false;
			comboBoxFromLocation.ShowConsignIn = false;
			comboBoxFromLocation.ShowConsignOut = false;
			comboBoxFromLocation.ShowInactiveItems = false;
			comboBoxFromLocation.ShowNormalLocations = true;
			comboBoxFromLocation.ShowPOSOnly = false;
			comboBoxFromLocation.ShowQuickAdd = true;
			comboBoxFromLocation.ShowWarehouseOnly = false;
			comboBoxFromLocation.Size = new System.Drawing.Size(103, 20);
			comboBoxFromLocation.TabIndex = 11;
			comboBoxFromLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToLead.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToLead.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToLead.DisplayLayout.Appearance = appearance25;
			comboBoxToLead.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToLead.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLead.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLead.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToLead.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLead.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToLead.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToLead.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToLead.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToLead.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToLead.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToLead.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToLead.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToLead.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToLead.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToLead.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLead.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToLead.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToLead.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToLead.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToLead.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToLead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToLead.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
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
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleLead.DisplayLayout.Appearance = appearance37;
			comboBoxSingleLead.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleLead.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleLead.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleLead.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxSingleLead.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleLead.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxSingleLead.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleLead.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleLead.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleLead.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxSingleLead.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleLead.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleLead.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleLead.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxSingleLead.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleLead.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleLead.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxSingleLead.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxSingleLead.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleLead.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleLead.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxSingleLead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleLead.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
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
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromLead.DisplayLayout.Appearance = appearance49;
			comboBoxFromLead.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromLead.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLead.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLead.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxFromLead.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLead.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxFromLead.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromLead.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromLead.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromLead.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxFromLead.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromLead.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromLead.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromLead.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxFromLead.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromLead.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLead.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxFromLead.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxFromLead.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromLead.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromLead.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxFromLead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromLead.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
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
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 28);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(60, 17);
			radioButtonRange.TabIndex = 3;
			radioButtonRange.Text = "Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToLocation);
			base.Controls.Add(comboBoxFromLocation);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(label5);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonLocation);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Controls.Add(comboBoxToLead);
			base.Controls.Add(comboBoxSingleLead);
			base.Controls.Add(comboBoxFromLead);
			base.Name = "LeadSelector";
			base.Size = new System.Drawing.Size(414, 75);
			base.Load += new System.EventHandler(LeadSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToLead).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleLead).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLead).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
