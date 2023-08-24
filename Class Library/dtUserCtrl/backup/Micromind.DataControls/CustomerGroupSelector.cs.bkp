using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CustomerGroupSelector : UserControl
	{
		private IContainer components;

		private CustomerGroupComboBox comboBoxFromCustomerGroup;

		private CustomerGroupComboBox comboBoxToCustomerGroup;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private Label label3;

		private RadioButton radioButtonSingle;

		private CustomerGroupComboBox comboBoxSingleCustomerGroup;

		public string FromCustomerGroup
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleCustomerGroup.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromCustomerGroup.SelectedID;
				}
				return "";
			}
		}

		public string ToCustomerGroup
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleCustomerGroup.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToCustomerGroup.SelectedID;
				}
				return "";
			}
		}

		public CustomerGroupSelector()
		{
			InitializeComponent();
		}

		private void CustomerGroupSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleCustomerGroup.Enabled = radioButtonSingle.Checked;
			CustomerGroupComboBox customerGroupComboBox = comboBoxFromCustomerGroup;
			bool enabled = comboBoxToCustomerGroup.Enabled = radioButtonRange.Checked;
			customerGroupComboBox.Enabled = enabled;
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
			comboBoxFromCustomerGroup = new Micromind.DataControls.CustomerGroupComboBox();
			comboBoxToCustomerGroup = new Micromind.DataControls.CustomerGroupComboBox();
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			label3 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxSingleCustomerGroup = new Micromind.DataControls.CustomerGroupComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCustomerGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCustomerGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleCustomerGroup).BeginInit();
			SuspendLayout();
			comboBoxFromCustomerGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromCustomerGroup.DisplayLayout.Appearance = appearance;
			comboBoxFromCustomerGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromCustomerGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCustomerGroup.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCustomerGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxFromCustomerGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCustomerGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxFromCustomerGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromCustomerGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromCustomerGroup.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromCustomerGroup.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxFromCustomerGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromCustomerGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromCustomerGroup.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromCustomerGroup.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxFromCustomerGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromCustomerGroup.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCustomerGroup.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxFromCustomerGroup.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxFromCustomerGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromCustomerGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromCustomerGroup.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxFromCustomerGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromCustomerGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxFromCustomerGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromCustomerGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromCustomerGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromCustomerGroup.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			comboBoxFromCustomerGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromCustomerGroup.Editable = true;
			comboBoxFromCustomerGroup.Enabled = false;
			comboBoxFromCustomerGroup.HasAllAccount = false;
			comboBoxFromCustomerGroup.HasCustom = false;
			comboBoxFromCustomerGroup.Location = new System.Drawing.Point(187, 25);
			comboBoxFromCustomerGroup.Name = "comboBoxFromCustomerGroup";
			comboBoxFromCustomerGroup.ShowInactiveItems = false;
			comboBoxFromCustomerGroup.ShowQuickAdd = true;
			comboBoxFromCustomerGroup.Size = new System.Drawing.Size(103, 20);
			comboBoxFromCustomerGroup.TabIndex = 4;
			comboBoxFromCustomerGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToCustomerGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToCustomerGroup.DisplayLayout.Appearance = appearance13;
			comboBoxToCustomerGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToCustomerGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCustomerGroup.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCustomerGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxToCustomerGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCustomerGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxToCustomerGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToCustomerGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToCustomerGroup.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToCustomerGroup.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxToCustomerGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToCustomerGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToCustomerGroup.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToCustomerGroup.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxToCustomerGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToCustomerGroup.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCustomerGroup.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxToCustomerGroup.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxToCustomerGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToCustomerGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxToCustomerGroup.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxToCustomerGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToCustomerGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxToCustomerGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToCustomerGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToCustomerGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToCustomerGroup.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			comboBoxToCustomerGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToCustomerGroup.Editable = true;
			comboBoxToCustomerGroup.Enabled = false;
			comboBoxToCustomerGroup.HasAllAccount = false;
			comboBoxToCustomerGroup.HasCustom = false;
			comboBoxToCustomerGroup.Location = new System.Drawing.Point(320, 25);
			comboBoxToCustomerGroup.Name = "comboBoxToCustomerGroup";
			comboBoxToCustomerGroup.ShowInactiveItems = false;
			comboBoxToCustomerGroup.ShowQuickAdd = true;
			comboBoxToCustomerGroup.Size = new System.Drawing.Size(103, 20);
			comboBoxToCustomerGroup.TabIndex = 5;
			comboBoxToCustomerGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			comboBoxSingleCustomerGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleCustomerGroup.DisplayLayout.Appearance = appearance25;
			comboBoxSingleCustomerGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleCustomerGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleCustomerGroup.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleCustomerGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxSingleCustomerGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleCustomerGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxSingleCustomerGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleCustomerGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxSingleCustomerGroup.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleCustomerGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxSingleCustomerGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleCustomerGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleCustomerGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleCustomerGroup.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			comboBoxSingleCustomerGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleCustomerGroup.Editable = true;
			comboBoxSingleCustomerGroup.Enabled = false;
			comboBoxSingleCustomerGroup.HasAllAccount = false;
			comboBoxSingleCustomerGroup.HasCustom = false;
			comboBoxSingleCustomerGroup.Location = new System.Drawing.Point(187, 3);
			comboBoxSingleCustomerGroup.Name = "comboBoxSingleCustomerGroup";
			comboBoxSingleCustomerGroup.ShowInactiveItems = false;
			comboBoxSingleCustomerGroup.ShowQuickAdd = true;
			comboBoxSingleCustomerGroup.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleCustomerGroup.TabIndex = 2;
			comboBoxSingleCustomerGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Controls.Add(comboBoxToCustomerGroup);
			base.Controls.Add(comboBoxSingleCustomerGroup);
			base.Controls.Add(comboBoxFromCustomerGroup);
			base.Name = "CustomerGroupSelector";
			base.Size = new System.Drawing.Size(433, 49);
			base.Load += new System.EventHandler(CustomerGroupSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxFromCustomerGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCustomerGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleCustomerGroup).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
