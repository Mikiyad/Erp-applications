using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class VendorGroupSelector : UserControl
	{
		private IContainer components;

		private VendorGroupComboBox comboBoxFromVendorGroup;

		private VendorGroupComboBox comboBoxToVendorGroup;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private Label label3;

		private RadioButton radioButtonSingle;

		private VendorGroupComboBox comboBoxSingleVendorGroup;

		public string FromVendorGroup
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleVendorGroup.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromVendorGroup.SelectedID;
				}
				return "";
			}
		}

		public string ToVendorGroup
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleVendorGroup.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToVendorGroup.SelectedID;
				}
				return "";
			}
		}

		public VendorGroupSelector()
		{
			InitializeComponent();
		}

		private void VendorGroupSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleVendorGroup.Enabled = radioButtonSingle.Checked;
			VendorGroupComboBox vendorGroupComboBox = comboBoxFromVendorGroup;
			bool enabled = comboBoxToVendorGroup.Enabled = radioButtonRange.Checked;
			vendorGroupComboBox.Enabled = enabled;
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
			comboBoxFromVendorGroup = new Micromind.DataControls.VendorGroupComboBox();
			comboBoxToVendorGroup = new Micromind.DataControls.VendorGroupComboBox();
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			label3 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxSingleVendorGroup = new Micromind.DataControls.VendorGroupComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxFromVendorGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToVendorGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleVendorGroup).BeginInit();
			SuspendLayout();
			comboBoxFromVendorGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromVendorGroup.DisplayLayout.Appearance = appearance;
			comboBoxFromVendorGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromVendorGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromVendorGroup.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromVendorGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxFromVendorGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromVendorGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxFromVendorGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromVendorGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromVendorGroup.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromVendorGroup.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxFromVendorGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromVendorGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromVendorGroup.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromVendorGroup.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxFromVendorGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromVendorGroup.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromVendorGroup.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxFromVendorGroup.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxFromVendorGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromVendorGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromVendorGroup.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxFromVendorGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromVendorGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxFromVendorGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromVendorGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromVendorGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromVendorGroup.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			comboBoxFromVendorGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromVendorGroup.Editable = true;
			comboBoxFromVendorGroup.Enabled = false;
			comboBoxFromVendorGroup.HasAllAccount = false;
			comboBoxFromVendorGroup.HasCustom = false;
			comboBoxFromVendorGroup.Location = new System.Drawing.Point(187, 25);
			comboBoxFromVendorGroup.Name = "comboBoxFromVendorGroup";
			comboBoxFromVendorGroup.ShowInactiveItems = false;
			comboBoxFromVendorGroup.ShowQuickAdd = true;
			comboBoxFromVendorGroup.Size = new System.Drawing.Size(103, 20);
			comboBoxFromVendorGroup.TabIndex = 4;
			comboBoxFromVendorGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToVendorGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToVendorGroup.DisplayLayout.Appearance = appearance13;
			comboBoxToVendorGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToVendorGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToVendorGroup.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToVendorGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxToVendorGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToVendorGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxToVendorGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToVendorGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToVendorGroup.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToVendorGroup.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxToVendorGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToVendorGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToVendorGroup.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToVendorGroup.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxToVendorGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToVendorGroup.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToVendorGroup.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxToVendorGroup.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxToVendorGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToVendorGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxToVendorGroup.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxToVendorGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToVendorGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxToVendorGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToVendorGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToVendorGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToVendorGroup.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			comboBoxToVendorGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToVendorGroup.Editable = true;
			comboBoxToVendorGroup.Enabled = false;
			comboBoxToVendorGroup.HasAllAccount = false;
			comboBoxToVendorGroup.HasCustom = false;
			comboBoxToVendorGroup.Location = new System.Drawing.Point(320, 25);
			comboBoxToVendorGroup.Name = "comboBoxToVendorGroup";
			comboBoxToVendorGroup.ShowInactiveItems = false;
			comboBoxToVendorGroup.ShowQuickAdd = true;
			comboBoxToVendorGroup.Size = new System.Drawing.Size(103, 20);
			comboBoxToVendorGroup.TabIndex = 5;
			comboBoxToVendorGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(291, 28);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(73, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Groups";
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
			comboBoxSingleVendorGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleVendorGroup.DisplayLayout.Appearance = appearance25;
			comboBoxSingleVendorGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleVendorGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleVendorGroup.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleVendorGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxSingleVendorGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleVendorGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxSingleVendorGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleVendorGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleVendorGroup.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleVendorGroup.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxSingleVendorGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleVendorGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleVendorGroup.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleVendorGroup.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxSingleVendorGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleVendorGroup.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleVendorGroup.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxSingleVendorGroup.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxSingleVendorGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleVendorGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleVendorGroup.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxSingleVendorGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleVendorGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxSingleVendorGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleVendorGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleVendorGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleVendorGroup.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			comboBoxSingleVendorGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleVendorGroup.Editable = true;
			comboBoxSingleVendorGroup.Enabled = false;
			comboBoxSingleVendorGroup.HasAllAccount = false;
			comboBoxSingleVendorGroup.HasCustom = false;
			comboBoxSingleVendorGroup.Location = new System.Drawing.Point(187, 3);
			comboBoxSingleVendorGroup.Name = "comboBoxSingleVendorGroup";
			comboBoxSingleVendorGroup.ShowInactiveItems = false;
			comboBoxSingleVendorGroup.ShowQuickAdd = true;
			comboBoxSingleVendorGroup.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleVendorGroup.TabIndex = 2;
			comboBoxSingleVendorGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Controls.Add(comboBoxToVendorGroup);
			base.Controls.Add(comboBoxSingleVendorGroup);
			base.Controls.Add(comboBoxFromVendorGroup);
			base.Name = "VendorGroupSelector";
			base.Size = new System.Drawing.Size(433, 49);
			base.Load += new System.EventHandler(VendorGroupSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxFromVendorGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToVendorGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleVendorGroup).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
