using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ItemBrandSelector : UserControl
	{
		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private Label label3;

		private RadioButton radioButtonSingle;

		private ProductBrandComboBox comboBoxSingleProductBrand;

		private ProductBrandComboBox comboBoxFromProductBrand;

		private ProductBrandComboBox comboBoxToProductBrand;

		public string FromProductBrand
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleProductBrand.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromProductBrand.SelectedID;
				}
				return "";
			}
		}

		public string ToProductBrand
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleProductBrand.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToProductBrand.SelectedID;
				}
				return "";
			}
		}

		public ItemBrandSelector()
		{
			InitializeComponent();
		}

		private void ProductCategorySelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleProductBrand.Enabled = radioButtonSingle.Checked;
			ProductBrandComboBox productBrandComboBox = comboBoxFromProductBrand;
			bool enabled = comboBoxToProductBrand.Enabled = radioButtonRange.Checked;
			productBrandComboBox.Enabled = enabled;
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
			comboBoxSingleProductBrand = new Micromind.DataControls.ProductBrandComboBox();
			comboBoxFromProductBrand = new Micromind.DataControls.ProductBrandComboBox();
			comboBoxToProductBrand = new Micromind.DataControls.ProductBrandComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleProductBrand).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromProductBrand).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToProductBrand).BeginInit();
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
			radioButtonAll.Size = new System.Drawing.Size(72, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Brands";
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
			comboBoxSingleProductBrand.Assigned = false;
			comboBoxSingleProductBrand.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSingleProductBrand.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleProductBrand.CustomReportFieldName = "";
			comboBoxSingleProductBrand.CustomReportKey = "";
			comboBoxSingleProductBrand.CustomReportValueType = 1;
			comboBoxSingleProductBrand.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleProductBrand.DisplayLayout.Appearance = appearance;
			comboBoxSingleProductBrand.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleProductBrand.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProductBrand.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleProductBrand.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSingleProductBrand.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleProductBrand.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSingleProductBrand.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleProductBrand.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleProductBrand.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleProductBrand.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSingleProductBrand.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleProductBrand.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProductBrand.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleProductBrand.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSingleProductBrand.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleProductBrand.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProductBrand.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSingleProductBrand.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSingleProductBrand.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleProductBrand.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleProductBrand.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSingleProductBrand.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleProductBrand.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSingleProductBrand.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleProductBrand.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleProductBrand.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleProductBrand.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleProductBrand.Editable = true;
			comboBoxSingleProductBrand.Enabled = false;
			comboBoxSingleProductBrand.FilterString = "";
			comboBoxSingleProductBrand.HasAllAccount = false;
			comboBoxSingleProductBrand.HasCustom = false;
			comboBoxSingleProductBrand.IsDataLoaded = false;
			comboBoxSingleProductBrand.Location = new System.Drawing.Point(161, 3);
			comboBoxSingleProductBrand.MaxDropDownItems = 12;
			comboBoxSingleProductBrand.Name = "comboBoxSingleProductBrand";
			comboBoxSingleProductBrand.ShowInactiveItems = false;
			comboBoxSingleProductBrand.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleProductBrand.TabIndex = 7;
			comboBoxSingleProductBrand.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromProductBrand.Assigned = false;
			comboBoxFromProductBrand.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromProductBrand.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromProductBrand.CustomReportFieldName = "";
			comboBoxFromProductBrand.CustomReportKey = "";
			comboBoxFromProductBrand.CustomReportValueType = 1;
			comboBoxFromProductBrand.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromProductBrand.DisplayLayout.Appearance = appearance13;
			comboBoxFromProductBrand.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromProductBrand.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromProductBrand.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromProductBrand.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromProductBrand.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromProductBrand.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromProductBrand.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromProductBrand.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromProductBrand.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromProductBrand.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromProductBrand.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromProductBrand.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromProductBrand.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromProductBrand.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromProductBrand.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromProductBrand.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromProductBrand.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromProductBrand.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromProductBrand.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromProductBrand.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromProductBrand.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromProductBrand.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromProductBrand.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromProductBrand.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromProductBrand.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromProductBrand.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromProductBrand.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromProductBrand.Editable = true;
			comboBoxFromProductBrand.Enabled = false;
			comboBoxFromProductBrand.FilterString = "";
			comboBoxFromProductBrand.HasAllAccount = false;
			comboBoxFromProductBrand.HasCustom = false;
			comboBoxFromProductBrand.IsDataLoaded = false;
			comboBoxFromProductBrand.Location = new System.Drawing.Point(161, 26);
			comboBoxFromProductBrand.MaxDropDownItems = 12;
			comboBoxFromProductBrand.Name = "comboBoxFromProductBrand";
			comboBoxFromProductBrand.ShowInactiveItems = false;
			comboBoxFromProductBrand.Size = new System.Drawing.Size(100, 20);
			comboBoxFromProductBrand.TabIndex = 8;
			comboBoxFromProductBrand.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToProductBrand.Assigned = false;
			comboBoxToProductBrand.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToProductBrand.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToProductBrand.CustomReportFieldName = "";
			comboBoxToProductBrand.CustomReportKey = "";
			comboBoxToProductBrand.CustomReportValueType = 1;
			comboBoxToProductBrand.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToProductBrand.DisplayLayout.Appearance = appearance25;
			comboBoxToProductBrand.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToProductBrand.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToProductBrand.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToProductBrand.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToProductBrand.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToProductBrand.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToProductBrand.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToProductBrand.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToProductBrand.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToProductBrand.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToProductBrand.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToProductBrand.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToProductBrand.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToProductBrand.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToProductBrand.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToProductBrand.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToProductBrand.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToProductBrand.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToProductBrand.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToProductBrand.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToProductBrand.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToProductBrand.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToProductBrand.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToProductBrand.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToProductBrand.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToProductBrand.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToProductBrand.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToProductBrand.Editable = true;
			comboBoxToProductBrand.Enabled = false;
			comboBoxToProductBrand.FilterString = "";
			comboBoxToProductBrand.HasAllAccount = false;
			comboBoxToProductBrand.HasCustom = false;
			comboBoxToProductBrand.IsDataLoaded = false;
			comboBoxToProductBrand.Location = new System.Drawing.Point(297, 26);
			comboBoxToProductBrand.MaxDropDownItems = 12;
			comboBoxToProductBrand.Name = "comboBoxToProductBrand";
			comboBoxToProductBrand.ShowInactiveItems = false;
			comboBoxToProductBrand.Size = new System.Drawing.Size(100, 20);
			comboBoxToProductBrand.TabIndex = 9;
			comboBoxToProductBrand.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToProductBrand);
			base.Controls.Add(comboBoxFromProductBrand);
			base.Controls.Add(comboBoxSingleProductBrand);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "ItemBrandSelector";
			base.Size = new System.Drawing.Size(414, 54);
			base.Load += new System.EventHandler(ProductCategorySelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxSingleProductBrand).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromProductBrand).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToProductBrand).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
