using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ProductCategorySelector : UserControl
	{
		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private Label label3;

		private RadioButton radioButtonSingle;

		private ProductCategoryComboBox comboBoxFromProductCategory;

		private ProductCategoryComboBox comboBoxToProductCategory;

		private ProductCategoryComboBox comboBoxSingleProductCategory;

		public string FromProductCategory
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleProductCategory.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromProductCategory.SelectedID;
				}
				return "";
			}
		}

		public string ToProductCategory
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleProductCategory.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToProductCategory.SelectedID;
				}
				return "";
			}
		}

		public ProductCategorySelector()
		{
			InitializeComponent();
		}

		private void ProductCategorySelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleProductCategory.Enabled = radioButtonSingle.Checked;
			ProductCategoryComboBox productCategoryComboBox = comboBoxFromProductCategory;
			bool enabled = comboBoxToProductCategory.Enabled = radioButtonRange.Checked;
			productCategoryComboBox.Enabled = enabled;
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
			comboBoxToProductCategory = new Micromind.DataControls.ProductCategoryComboBox();
			comboBoxFromProductCategory = new Micromind.DataControls.ProductCategoryComboBox();
			comboBoxSingleProductCategory = new Micromind.DataControls.ProductCategoryComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxToProductCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromProductCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleProductCategory).BeginInit();
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
			radioButtonAll.Size = new System.Drawing.Size(89, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Categories";
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
			label3.TabIndex = 4;
			label3.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(100, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxToProductCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToProductCategory.CustomReportFieldName = "";
			comboBoxToProductCategory.CustomReportKey = "";
			comboBoxToProductCategory.CustomReportValueType = 1;
			comboBoxToProductCategory.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToProductCategory.DisplayLayout.Appearance = appearance;
			comboBoxToProductCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToProductCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToProductCategory.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToProductCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxToProductCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToProductCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxToProductCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToProductCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToProductCategory.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToProductCategory.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxToProductCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToProductCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToProductCategory.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToProductCategory.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxToProductCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToProductCategory.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToProductCategory.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxToProductCategory.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxToProductCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToProductCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxToProductCategory.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxToProductCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToProductCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxToProductCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToProductCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToProductCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToProductCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToProductCategory.Editable = true;
			comboBoxToProductCategory.Enabled = false;
			comboBoxToProductCategory.FilterString = "";
			comboBoxToProductCategory.HasAllAccount = false;
			comboBoxToProductCategory.HasCustom = false;
			comboBoxToProductCategory.IsDataLoaded = false;
			comboBoxToProductCategory.Location = new System.Drawing.Point(292, 26);
			comboBoxToProductCategory.MaxDropDownItems = 12;
			comboBoxToProductCategory.Name = "comboBoxToProductCategory";
			comboBoxToProductCategory.ShowInactiveItems = false;
			comboBoxToProductCategory.Size = new System.Drawing.Size(105, 20);
			comboBoxToProductCategory.TabIndex = 6;
			comboBoxToProductCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromProductCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromProductCategory.CustomReportFieldName = "";
			comboBoxFromProductCategory.CustomReportKey = "";
			comboBoxFromProductCategory.CustomReportValueType = 1;
			comboBoxFromProductCategory.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromProductCategory.DisplayLayout.Appearance = appearance13;
			comboBoxFromProductCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromProductCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromProductCategory.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromProductCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromProductCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromProductCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromProductCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromProductCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromProductCategory.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromProductCategory.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromProductCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromProductCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromProductCategory.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromProductCategory.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromProductCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromProductCategory.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromProductCategory.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromProductCategory.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromProductCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromProductCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromProductCategory.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromProductCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromProductCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromProductCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromProductCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromProductCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromProductCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromProductCategory.Editable = true;
			comboBoxFromProductCategory.Enabled = false;
			comboBoxFromProductCategory.FilterString = "";
			comboBoxFromProductCategory.HasAllAccount = false;
			comboBoxFromProductCategory.HasCustom = false;
			comboBoxFromProductCategory.IsDataLoaded = false;
			comboBoxFromProductCategory.Location = new System.Drawing.Point(161, 26);
			comboBoxFromProductCategory.MaxDropDownItems = 12;
			comboBoxFromProductCategory.Name = "comboBoxFromProductCategory";
			comboBoxFromProductCategory.ShowInactiveItems = false;
			comboBoxFromProductCategory.Size = new System.Drawing.Size(105, 20);
			comboBoxFromProductCategory.TabIndex = 5;
			comboBoxFromProductCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSingleProductCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleProductCategory.CustomReportFieldName = "";
			comboBoxSingleProductCategory.CustomReportKey = "";
			comboBoxSingleProductCategory.CustomReportValueType = 1;
			comboBoxSingleProductCategory.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleProductCategory.DisplayLayout.Appearance = appearance25;
			comboBoxSingleProductCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleProductCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProductCategory.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleProductCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxSingleProductCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleProductCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxSingleProductCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleProductCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleProductCategory.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleProductCategory.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxSingleProductCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleProductCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProductCategory.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleProductCategory.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxSingleProductCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleProductCategory.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProductCategory.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxSingleProductCategory.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxSingleProductCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleProductCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleProductCategory.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxSingleProductCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleProductCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxSingleProductCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleProductCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleProductCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleProductCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleProductCategory.Editable = true;
			comboBoxSingleProductCategory.Enabled = false;
			comboBoxSingleProductCategory.FilterString = "";
			comboBoxSingleProductCategory.HasAllAccount = false;
			comboBoxSingleProductCategory.HasCustom = false;
			comboBoxSingleProductCategory.IsDataLoaded = false;
			comboBoxSingleProductCategory.Location = new System.Drawing.Point(161, 4);
			comboBoxSingleProductCategory.MaxDropDownItems = 12;
			comboBoxSingleProductCategory.Name = "comboBoxSingleProductCategory";
			comboBoxSingleProductCategory.ShowInactiveItems = false;
			comboBoxSingleProductCategory.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleProductCategory.TabIndex = 2;
			comboBoxSingleProductCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToProductCategory);
			base.Controls.Add(comboBoxFromProductCategory);
			base.Controls.Add(comboBoxSingleProductCategory);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "ProductCategorySelector";
			base.Size = new System.Drawing.Size(414, 54);
			base.Load += new System.EventHandler(ProductCategorySelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxToProductCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromProductCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleProductCategory).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
