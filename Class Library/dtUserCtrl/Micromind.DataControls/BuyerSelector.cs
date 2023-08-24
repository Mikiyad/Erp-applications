using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class BuyerSelector : UserControl
	{
		private IContainer components;

		private BuyerComboBox comboBoxFromBuyer;

		private BuyerComboBox comboBoxToBuyer;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private Label label3;

		private RadioButton radioButtonSingle;

		private BuyerComboBox comboBoxSingleBuyer;

		public string FromBuyer
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleBuyer.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromBuyer.SelectedID;
				}
				return "";
			}
		}

		public string ToBuyer
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleBuyer.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToBuyer.SelectedID;
				}
				return "";
			}
		}

		public BuyerSelector()
		{
			InitializeComponent();
		}

		private void BuyerSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleBuyer.Enabled = radioButtonSingle.Checked;
			BuyerComboBox buyerComboBox = comboBoxFromBuyer;
			bool enabled = comboBoxToBuyer.Enabled = radioButtonRange.Checked;
			buyerComboBox.Enabled = enabled;
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
			comboBoxFromBuyer = new Micromind.DataControls.BuyerComboBox();
			comboBoxToBuyer = new Micromind.DataControls.BuyerComboBox();
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			label3 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxSingleBuyer = new Micromind.DataControls.BuyerComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxFromBuyer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToBuyer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleBuyer).BeginInit();
			SuspendLayout();
			comboBoxFromBuyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromBuyer.DisplayLayout.Appearance = appearance;
			comboBoxFromBuyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromBuyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromBuyer.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromBuyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxFromBuyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromBuyer.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxFromBuyer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromBuyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromBuyer.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromBuyer.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxFromBuyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromBuyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromBuyer.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromBuyer.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxFromBuyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromBuyer.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromBuyer.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxFromBuyer.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxFromBuyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromBuyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromBuyer.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxFromBuyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromBuyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxFromBuyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromBuyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromBuyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromBuyer.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			comboBoxFromBuyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromBuyer.Editable = true;
			comboBoxFromBuyer.Enabled = false;
			comboBoxFromBuyer.HasCustom = false;
			comboBoxFromBuyer.Location = new System.Drawing.Point(187, 25);
			comboBoxFromBuyer.Name = "comboBoxFromBuyer";
			comboBoxFromBuyer.ShowQuickAdd = true;
			comboBoxFromBuyer.Size = new System.Drawing.Size(103, 20);
			comboBoxFromBuyer.TabIndex = 4;
			comboBoxFromBuyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToBuyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToBuyer.DisplayLayout.Appearance = appearance13;
			comboBoxToBuyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToBuyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToBuyer.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToBuyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxToBuyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToBuyer.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxToBuyer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToBuyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToBuyer.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToBuyer.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxToBuyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToBuyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToBuyer.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToBuyer.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxToBuyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToBuyer.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToBuyer.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxToBuyer.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxToBuyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToBuyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxToBuyer.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxToBuyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToBuyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxToBuyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToBuyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToBuyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToBuyer.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			comboBoxToBuyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToBuyer.Editable = true;
			comboBoxToBuyer.Enabled = false;
			comboBoxToBuyer.Location = new System.Drawing.Point(320, 25);
			comboBoxToBuyer.Name = "comboBoxToBuyer";
			comboBoxToBuyer.ShowQuickAdd = true;
			comboBoxToBuyer.Size = new System.Drawing.Size(103, 20);
			comboBoxToBuyer.TabIndex = 5;
			comboBoxToBuyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			radioButtonAll.Size = new System.Drawing.Size(102, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Buyers";
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
			comboBoxSingleBuyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleBuyer.DisplayLayout.Appearance = appearance25;
			comboBoxSingleBuyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleBuyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleBuyer.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleBuyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxSingleBuyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleBuyer.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxSingleBuyer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleBuyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleBuyer.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleBuyer.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxSingleBuyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleBuyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleBuyer.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleBuyer.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxSingleBuyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleBuyer.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleBuyer.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxSingleBuyer.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxSingleBuyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleBuyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleBuyer.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxSingleBuyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleBuyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxSingleBuyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleBuyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleBuyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleBuyer.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			comboBoxSingleBuyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleBuyer.Editable = true;
			comboBoxSingleBuyer.Enabled = false;
			comboBoxSingleBuyer.Location = new System.Drawing.Point(187, 3);
			comboBoxSingleBuyer.Name = "comboBoxSingleBuyer";
			comboBoxSingleBuyer.ShowQuickAdd = true;
			comboBoxSingleBuyer.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleBuyer.TabIndex = 2;
			comboBoxSingleBuyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Controls.Add(comboBoxToBuyer);
			base.Controls.Add(comboBoxSingleBuyer);
			base.Controls.Add(comboBoxFromBuyer);
			base.Name = "BuyerSelector";
			base.Size = new System.Drawing.Size(433, 49);
			base.Load += new System.EventHandler(BuyerSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxFromBuyer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToBuyer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleBuyer).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
