using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ProductClassSelector : UserControl
	{
		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private Label label3;

		private RadioButton radioButtonSingle;

		private RadioButton radioButtonClass;

		private ItemClassComboBox comboBoxFromClass;

		private ItemClassComboBox comboBoxToClass;

		private ItemClassComboBox comboBoxSingleProductClass;

		public string FromProductClass
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleProductClass.SelectedID;
				}
				if (radioButtonClass.Checked)
				{
					return comboBoxFromClass.SelectedID;
				}
				return "";
			}
		}

		public string ToProductClass
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleProductClass.SelectedID;
				}
				if (radioButtonClass.Checked)
				{
					return comboBoxToClass.SelectedID;
				}
				return "";
			}
		}

		public ProductClassSelector()
		{
			InitializeComponent();
		}

		private void ProductClassSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleProductClass.Enabled = radioButtonSingle.Checked;
			ItemClassComboBox itemClassComboBox = comboBoxFromClass;
			bool enabled = comboBoxToClass.Enabled = radioButtonClass.Checked;
			itemClassComboBox.Enabled = enabled;
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
			label3 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			radioButtonClass = new System.Windows.Forms.RadioButton();
			comboBoxSingleProductClass = new Micromind.DataControls.ItemClassComboBox();
			comboBoxToClass = new Micromind.DataControls.ItemClassComboBox();
			comboBoxFromClass = new Micromind.DataControls.ItemClassComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleProductClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).BeginInit();
			SuspendLayout();
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(270, 29);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(64, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Class";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(124, 30);
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
			radioButtonClass.AutoSize = true;
			radioButtonClass.Location = new System.Drawing.Point(6, 28);
			radioButtonClass.Name = "radioButtonClass";
			radioButtonClass.Size = new System.Drawing.Size(76, 17);
			radioButtonClass.TabIndex = 7;
			radioButtonClass.Text = "Item Class:";
			radioButtonClass.UseVisualStyleBackColor = true;
			comboBoxSingleProductClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleProductClass.CustomReportFieldName = "";
			comboBoxSingleProductClass.CustomReportKey = "";
			comboBoxSingleProductClass.CustomReportValueType = 1;
			comboBoxSingleProductClass.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleProductClass.DisplayLayout.Appearance = appearance;
			comboBoxSingleProductClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleProductClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProductClass.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleProductClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSingleProductClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleProductClass.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSingleProductClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleProductClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleProductClass.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleProductClass.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSingleProductClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleProductClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProductClass.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleProductClass.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSingleProductClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleProductClass.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProductClass.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSingleProductClass.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSingleProductClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleProductClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleProductClass.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSingleProductClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleProductClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSingleProductClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleProductClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleProductClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleProductClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleProductClass.Editable = true;
			comboBoxSingleProductClass.Enabled = false;
			comboBoxSingleProductClass.FilterString = "";
			comboBoxSingleProductClass.HasAllAccount = false;
			comboBoxSingleProductClass.HasCustom = false;
			comboBoxSingleProductClass.IsDataLoaded = false;
			comboBoxSingleProductClass.Location = new System.Drawing.Point(163, 0);
			comboBoxSingleProductClass.MaxDropDownItems = 12;
			comboBoxSingleProductClass.Name = "comboBoxSingleProductClass";
			comboBoxSingleProductClass.ShowInactiveItems = false;
			comboBoxSingleProductClass.ShowQuickAdd = true;
			comboBoxSingleProductClass.Size = new System.Drawing.Size(234, 20);
			comboBoxSingleProductClass.TabIndex = 18;
			comboBoxSingleProductClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToClass.CustomReportFieldName = "";
			comboBoxToClass.CustomReportKey = "";
			comboBoxToClass.CustomReportValueType = 1;
			comboBoxToClass.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToClass.DisplayLayout.Appearance = appearance13;
			comboBoxToClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxToClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxToClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToClass.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToClass.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxToClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToClass.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxToClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToClass.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxToClass.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxToClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxToClass.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxToClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxToClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToClass.Editable = true;
			comboBoxToClass.Enabled = false;
			comboBoxToClass.FilterString = "";
			comboBoxToClass.HasAllAccount = false;
			comboBoxToClass.HasCustom = false;
			comboBoxToClass.IsDataLoaded = false;
			comboBoxToClass.Location = new System.Drawing.Point(294, 24);
			comboBoxToClass.MaxDropDownItems = 12;
			comboBoxToClass.Name = "comboBoxToClass";
			comboBoxToClass.ShowInactiveItems = false;
			comboBoxToClass.ShowQuickAdd = true;
			comboBoxToClass.Size = new System.Drawing.Size(103, 20);
			comboBoxToClass.TabIndex = 17;
			comboBoxToClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromClass.CustomReportFieldName = "";
			comboBoxFromClass.CustomReportKey = "";
			comboBoxFromClass.CustomReportValueType = 1;
			comboBoxFromClass.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromClass.DisplayLayout.Appearance = appearance25;
			comboBoxFromClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxFromClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxFromClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromClass.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromClass.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromClass.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxFromClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromClass.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxFromClass.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxFromClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromClass.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxFromClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxFromClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromClass.Editable = true;
			comboBoxFromClass.Enabled = false;
			comboBoxFromClass.FilterString = "";
			comboBoxFromClass.HasAllAccount = false;
			comboBoxFromClass.HasCustom = false;
			comboBoxFromClass.IsDataLoaded = false;
			comboBoxFromClass.Location = new System.Drawing.Point(162, 24);
			comboBoxFromClass.MaxDropDownItems = 12;
			comboBoxFromClass.Name = "comboBoxFromClass";
			comboBoxFromClass.ShowInactiveItems = false;
			comboBoxFromClass.ShowQuickAdd = true;
			comboBoxFromClass.Size = new System.Drawing.Size(103, 20);
			comboBoxFromClass.TabIndex = 16;
			comboBoxFromClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxSingleProductClass);
			base.Controls.Add(comboBoxToClass);
			base.Controls.Add(comboBoxFromClass);
			base.Controls.Add(radioButtonClass);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "ProductClassSelector";
			base.Size = new System.Drawing.Size(414, 54);
			base.Load += new System.EventHandler(ProductClassSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxSingleProductClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
