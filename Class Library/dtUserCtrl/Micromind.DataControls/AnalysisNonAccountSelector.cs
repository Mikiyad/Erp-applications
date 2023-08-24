using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class AnalysisNonAccountSelector : UserControl
	{
		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private Label label3;

		private RadioButton radioButtonSingle;

		private AnalysisNonAccountComboBox comboBoxSingleAnalysis;

		private AnalysisNonAccountComboBox comboBoxFromAnalysis;

		private AnalysisNonAccountComboBox comboBoxToAnalysis;

		public string FromAnalysis
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleAnalysis.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromAnalysis.SelectedID;
				}
				return "";
			}
		}

		public string ToAnalysis
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleAnalysis.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToAnalysis.SelectedID;
				}
				return "";
			}
		}

		public AnalysisNonAccountSelector()
		{
			InitializeComponent();
		}

		private void AnalysisSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleAnalysis.Enabled = radioButtonSingle.Checked;
			AnalysisNonAccountComboBox analysisNonAccountComboBox = comboBoxFromAnalysis;
			bool enabled = comboBoxToAnalysis.Enabled = radioButtonRange.Checked;
			analysisNonAccountComboBox.Enabled = enabled;
		}

		private void radioButtons_CheckedChanged(object sender, EventArgs e)
		{
			EnableDisableControls();
		}

		public void FilterByAccount(string accountID)
		{
			comboBoxFromAnalysis.FilterByAccount(accountID);
			comboBoxSingleAnalysis.FilterByAccount(accountID);
			comboBoxToAnalysis.FilterByAccount(accountID);
			comboBoxFromAnalysis.Clear();
			comboBoxSingleAnalysis.Clear();
			comboBoxToAnalysis.Clear();
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
			comboBoxToAnalysis = new Micromind.DataControls.AnalysisNonAccountComboBox();
			comboBoxFromAnalysis = new Micromind.DataControls.AnalysisNonAccountComboBox();
			comboBoxSingleAnalysis = new Micromind.DataControls.AnalysisNonAccountComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxToAnalysis).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromAnalysis).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleAnalysis).BeginInit();
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
			radioButtonAll.Size = new System.Drawing.Size(82, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Analysiss";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 28);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(60, 17);
			radioButtonRange.TabIndex = 3;
			radioButtonRange.Text = "Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(116, 29);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 6;
			label3.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(100, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxToAnalysis.Assigned = false;
			comboBoxToAnalysis.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToAnalysis.CustomReportFieldName = "";
			comboBoxToAnalysis.CustomReportKey = "";
			comboBoxToAnalysis.CustomReportValueType = 1;
			comboBoxToAnalysis.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToAnalysis.DisplayLayout.Appearance = appearance;
			comboBoxToAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToAnalysis.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxToAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxToAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxToAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToAnalysis.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxToAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxToAnalysis.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxToAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxToAnalysis.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxToAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxToAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToAnalysis.Editable = true;
			comboBoxToAnalysis.Enabled = false;
			comboBoxToAnalysis.FilterString = "";
			comboBoxToAnalysis.HasAllAccount = false;
			comboBoxToAnalysis.HasCustom = false;
			comboBoxToAnalysis.IsDataLoaded = false;
			comboBoxToAnalysis.Location = new System.Drawing.Point(297, 24);
			comboBoxToAnalysis.MaxDropDownItems = 12;
			comboBoxToAnalysis.Name = "comboBoxToAnalysis";
			comboBoxToAnalysis.ShowInactiveItems = false;
			comboBoxToAnalysis.ShowQuickAdd = true;
			comboBoxToAnalysis.Size = new System.Drawing.Size(100, 20);
			comboBoxToAnalysis.TabIndex = 9;
			comboBoxToAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromAnalysis.Assigned = false;
			comboBoxFromAnalysis.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromAnalysis.CustomReportFieldName = "";
			comboBoxFromAnalysis.CustomReportKey = "";
			comboBoxFromAnalysis.CustomReportValueType = 1;
			comboBoxFromAnalysis.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromAnalysis.DisplayLayout.Appearance = appearance13;
			comboBoxFromAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromAnalysis.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromAnalysis.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromAnalysis.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromAnalysis.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromAnalysis.Editable = true;
			comboBoxFromAnalysis.Enabled = false;
			comboBoxFromAnalysis.FilterString = "";
			comboBoxFromAnalysis.HasAllAccount = false;
			comboBoxFromAnalysis.HasCustom = false;
			comboBoxFromAnalysis.IsDataLoaded = false;
			comboBoxFromAnalysis.Location = new System.Drawing.Point(161, 25);
			comboBoxFromAnalysis.MaxDropDownItems = 12;
			comboBoxFromAnalysis.Name = "comboBoxFromAnalysis";
			comboBoxFromAnalysis.ShowInactiveItems = false;
			comboBoxFromAnalysis.ShowQuickAdd = true;
			comboBoxFromAnalysis.Size = new System.Drawing.Size(100, 20);
			comboBoxFromAnalysis.TabIndex = 8;
			comboBoxFromAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSingleAnalysis.Assigned = false;
			comboBoxSingleAnalysis.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSingleAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleAnalysis.CustomReportFieldName = "";
			comboBoxSingleAnalysis.CustomReportKey = "";
			comboBoxSingleAnalysis.CustomReportValueType = 1;
			comboBoxSingleAnalysis.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleAnalysis.DisplayLayout.Appearance = appearance25;
			comboBoxSingleAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleAnalysis.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxSingleAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxSingleAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxSingleAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleAnalysis.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxSingleAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxSingleAnalysis.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxSingleAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleAnalysis.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxSingleAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxSingleAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleAnalysis.Editable = true;
			comboBoxSingleAnalysis.Enabled = false;
			comboBoxSingleAnalysis.FilterString = "";
			comboBoxSingleAnalysis.HasAllAccount = false;
			comboBoxSingleAnalysis.HasCustom = false;
			comboBoxSingleAnalysis.IsDataLoaded = false;
			comboBoxSingleAnalysis.Location = new System.Drawing.Point(161, 0);
			comboBoxSingleAnalysis.MaxDropDownItems = 12;
			comboBoxSingleAnalysis.Name = "comboBoxSingleAnalysis";
			comboBoxSingleAnalysis.ShowInactiveItems = false;
			comboBoxSingleAnalysis.ShowQuickAdd = true;
			comboBoxSingleAnalysis.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleAnalysis.TabIndex = 7;
			comboBoxSingleAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToAnalysis);
			base.Controls.Add(comboBoxFromAnalysis);
			base.Controls.Add(comboBoxSingleAnalysis);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "AnalysisNonAccountSelector";
			base.Size = new System.Drawing.Size(414, 59);
			base.Load += new System.EventHandler(AnalysisSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxToAnalysis).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromAnalysis).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleAnalysis).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
