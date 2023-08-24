using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class PropertyAgentSelector : UserControl
	{
		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private Label label3;

		private RadioButton radioButtonSingle;

		private PropertyAgentComboBox comboBoxSinglePropertyAgent;

		private AccountSelector accountSelector1;

		private PropertyAgentComboBox comboBoxFromPropertyAgent;

		private PropertyAgentComboBox comboBoxToPropertyAgent;

		public string FromAgent
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSinglePropertyAgent.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromPropertyAgent.SelectedID;
				}
				return "";
			}
		}

		public string ToAgent
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSinglePropertyAgent.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToPropertyAgent.SelectedID;
				}
				return "";
			}
		}

		public PropertyAgentSelector()
		{
			InitializeComponent();
		}

		private void SalespersonSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSinglePropertyAgent.Enabled = radioButtonSingle.Checked;
			PropertyAgentComboBox propertyAgentComboBox = comboBoxFromPropertyAgent;
			bool enabled = comboBoxToPropertyAgent.Enabled = radioButtonRange.Checked;
			propertyAgentComboBox.Enabled = enabled;
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
			label3 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxSinglePropertyAgent = new Micromind.DataControls.PropertyAgentComboBox();
			accountSelector1 = new Micromind.DataControls.AccountSelector();
			comboBoxFromPropertyAgent = new Micromind.DataControls.PropertyAgentComboBox();
			comboBoxToPropertyAgent = new Micromind.DataControls.PropertyAgentComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxSinglePropertyAgent).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromPropertyAgent).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToPropertyAgent).BeginInit();
			SuspendLayout();
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(289, 29);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(72, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Agents";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 26);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(60, 17);
			radioButtonRange.TabIndex = 3;
			radioButtonRange.Text = "Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(135, 28);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 6;
			label3.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(119, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxSinglePropertyAgent.Assigned = false;
			comboBoxSinglePropertyAgent.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSinglePropertyAgent.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSinglePropertyAgent.CustomReportFieldName = "";
			comboBoxSinglePropertyAgent.CustomReportKey = "";
			comboBoxSinglePropertyAgent.CustomReportValueType = 1;
			comboBoxSinglePropertyAgent.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSinglePropertyAgent.DisplayLayout.Appearance = appearance;
			comboBoxSinglePropertyAgent.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSinglePropertyAgent.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSinglePropertyAgent.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSinglePropertyAgent.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSinglePropertyAgent.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSinglePropertyAgent.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSinglePropertyAgent.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSinglePropertyAgent.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSinglePropertyAgent.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSinglePropertyAgent.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSinglePropertyAgent.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSinglePropertyAgent.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSinglePropertyAgent.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSinglePropertyAgent.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSinglePropertyAgent.Editable = true;
			comboBoxSinglePropertyAgent.FilterString = "";
			comboBoxSinglePropertyAgent.HasAllAccount = false;
			comboBoxSinglePropertyAgent.HasCustom = false;
			comboBoxSinglePropertyAgent.IsDataLoaded = false;
			comboBoxSinglePropertyAgent.Location = new System.Drawing.Point(172, 4);
			comboBoxSinglePropertyAgent.MaxDropDownItems = 12;
			comboBoxSinglePropertyAgent.Name = "comboBoxSinglePropertyAgent";
			comboBoxSinglePropertyAgent.ShowInactiveItems = false;
			comboBoxSinglePropertyAgent.ShowQuickAdd = true;
			comboBoxSinglePropertyAgent.Size = new System.Drawing.Size(258, 20);
			comboBoxSinglePropertyAgent.TabIndex = 7;
			comboBoxSinglePropertyAgent.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			accountSelector1.BackColor = System.Drawing.Color.Transparent;
			accountSelector1.CustomReportFieldName = "";
			accountSelector1.CustomReportKey = "";
			accountSelector1.CustomReportValueType = 1;
			accountSelector1.Location = new System.Drawing.Point(310, 5);
			accountSelector1.Name = "accountSelector1";
			accountSelector1.Size = new System.Drawing.Size(8, 8);
			accountSelector1.TabIndex = 8;
			comboBoxFromPropertyAgent.Assigned = false;
			comboBoxFromPropertyAgent.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromPropertyAgent.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromPropertyAgent.CustomReportFieldName = "";
			comboBoxFromPropertyAgent.CustomReportKey = "";
			comboBoxFromPropertyAgent.CustomReportValueType = 1;
			comboBoxFromPropertyAgent.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromPropertyAgent.DisplayLayout.Appearance = appearance13;
			comboBoxFromPropertyAgent.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromPropertyAgent.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromPropertyAgent.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromPropertyAgent.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromPropertyAgent.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromPropertyAgent.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromPropertyAgent.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromPropertyAgent.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromPropertyAgent.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromPropertyAgent.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromPropertyAgent.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromPropertyAgent.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromPropertyAgent.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromPropertyAgent.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromPropertyAgent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromPropertyAgent.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromPropertyAgent.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromPropertyAgent.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromPropertyAgent.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromPropertyAgent.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromPropertyAgent.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromPropertyAgent.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromPropertyAgent.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromPropertyAgent.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromPropertyAgent.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromPropertyAgent.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromPropertyAgent.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromPropertyAgent.Editable = true;
			comboBoxFromPropertyAgent.FilterString = "";
			comboBoxFromPropertyAgent.HasAllAccount = false;
			comboBoxFromPropertyAgent.HasCustom = false;
			comboBoxFromPropertyAgent.IsDataLoaded = false;
			comboBoxFromPropertyAgent.Location = new System.Drawing.Point(172, 26);
			comboBoxFromPropertyAgent.MaxDropDownItems = 12;
			comboBoxFromPropertyAgent.Name = "comboBoxFromPropertyAgent";
			comboBoxFromPropertyAgent.ShowInactiveItems = false;
			comboBoxFromPropertyAgent.ShowQuickAdd = true;
			comboBoxFromPropertyAgent.Size = new System.Drawing.Size(113, 20);
			comboBoxFromPropertyAgent.TabIndex = 9;
			comboBoxFromPropertyAgent.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToPropertyAgent.Assigned = false;
			comboBoxToPropertyAgent.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToPropertyAgent.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToPropertyAgent.CustomReportFieldName = "";
			comboBoxToPropertyAgent.CustomReportKey = "";
			comboBoxToPropertyAgent.CustomReportValueType = 1;
			comboBoxToPropertyAgent.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToPropertyAgent.DisplayLayout.Appearance = appearance25;
			comboBoxToPropertyAgent.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToPropertyAgent.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToPropertyAgent.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToPropertyAgent.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToPropertyAgent.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToPropertyAgent.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToPropertyAgent.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToPropertyAgent.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToPropertyAgent.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToPropertyAgent.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToPropertyAgent.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToPropertyAgent.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToPropertyAgent.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToPropertyAgent.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToPropertyAgent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToPropertyAgent.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToPropertyAgent.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToPropertyAgent.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToPropertyAgent.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToPropertyAgent.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToPropertyAgent.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToPropertyAgent.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToPropertyAgent.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToPropertyAgent.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToPropertyAgent.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToPropertyAgent.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToPropertyAgent.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToPropertyAgent.Editable = true;
			comboBoxToPropertyAgent.FilterString = "";
			comboBoxToPropertyAgent.HasAllAccount = false;
			comboBoxToPropertyAgent.HasCustom = false;
			comboBoxToPropertyAgent.IsDataLoaded = false;
			comboBoxToPropertyAgent.Location = new System.Drawing.Point(310, 26);
			comboBoxToPropertyAgent.MaxDropDownItems = 12;
			comboBoxToPropertyAgent.Name = "comboBoxToPropertyAgent";
			comboBoxToPropertyAgent.ShowInactiveItems = false;
			comboBoxToPropertyAgent.ShowQuickAdd = true;
			comboBoxToPropertyAgent.Size = new System.Drawing.Size(120, 20);
			comboBoxToPropertyAgent.TabIndex = 10;
			comboBoxToPropertyAgent.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToPropertyAgent);
			base.Controls.Add(comboBoxFromPropertyAgent);
			base.Controls.Add(accountSelector1);
			base.Controls.Add(comboBoxSinglePropertyAgent);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "PropertyAgentSelector";
			base.Size = new System.Drawing.Size(433, 49);
			base.Load += new System.EventHandler(SalespersonSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxSinglePropertyAgent).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromPropertyAgent).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToPropertyAgent).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
