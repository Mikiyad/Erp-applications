using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ServiceSelector : UserControl, ICustomReportControl
	{
		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private Label label3;

		private RadioButton radioButtonSingle;

		private ServiceItemComboBox comboBoxSingleServiceItem;

		private ServiceItemComboBox comboBoxFromServiceItem;

		private ServiceItemComboBox comboBoxToServiceItem;

		public string CustomReportFieldName
		{
			get
			{
				return crFieldName;
			}
			set
			{
				crFieldName = value;
			}
		}

		public string CustomReportKey
		{
			get
			{
				return crKey;
			}
			set
			{
				crKey = value;
			}
		}

		public byte CustomReportValueType
		{
			get
			{
				return crValueType;
			}
			set
			{
				crValueType = value;
			}
		}

		public string FromServiceItem
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleServiceItem.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromServiceItem.SelectedID;
				}
				return "";
			}
		}

		public string ToServiceItem
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleServiceItem.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToServiceItem.SelectedID;
				}
				return "";
			}
		}

		public ServiceSelector()
		{
			InitializeComponent();
		}

		private void ServiceSelector_Load(object sender, EventArgs e)
		{
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return "ANY(SELECT ServiceItemID FROM Service_Item)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxSingleServiceItem.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return "ANY(SELECT ServiceItemID FROM Service_Item WHERE ServiceItemID Between '" + comboBoxFromServiceItem.SelectedID + "' AND '" + comboBoxToServiceItem.SelectedID + "')";
				}
				return "1=1";
			}
			if (crFieldName == "")
			{
				return "''=''";
			}
			if (radioButtonAll.Checked)
			{
				return "''=''";
			}
			if (radioButtonSingle.Checked)
			{
				return crFieldName = "'" + comboBoxSingleServiceItem.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromServiceItem.SelectedID + "' AND '" + comboBoxToServiceItem.SelectedID + "')";
			}
			return "''=''";
		}

		private void EnableDisableControls()
		{
			comboBoxSingleServiceItem.Enabled = radioButtonSingle.Checked;
			ServiceItemComboBox serviceItemComboBox = comboBoxFromServiceItem;
			bool enabled = comboBoxToServiceItem.Enabled = radioButtonRange.Checked;
			serviceItemComboBox.Enabled = enabled;
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
			comboBoxSingleServiceItem = new Micromind.DataControls.ServiceItemComboBox();
			comboBoxFromServiceItem = new Micromind.DataControls.ServiceItemComboBox();
			comboBoxToServiceItem = new Micromind.DataControls.ServiceItemComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleServiceItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromServiceItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToServiceItem).BeginInit();
			SuspendLayout();
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(298, 29);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 3);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(103, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Service Items";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 27);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(60, 17);
			radioButtonRange.TabIndex = 9;
			radioButtonRange.Text = "Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(122, 29);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 10;
			label3.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(123, 3);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxSingleServiceItem.Assigned = false;
			comboBoxSingleServiceItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSingleServiceItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleServiceItem.CustomReportFieldName = "";
			comboBoxSingleServiceItem.CustomReportKey = "";
			comboBoxSingleServiceItem.CustomReportValueType = 1;
			comboBoxSingleServiceItem.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleServiceItem.DisplayLayout.Appearance = appearance;
			comboBoxSingleServiceItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleServiceItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleServiceItem.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleServiceItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSingleServiceItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleServiceItem.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSingleServiceItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleServiceItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleServiceItem.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleServiceItem.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSingleServiceItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleServiceItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleServiceItem.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleServiceItem.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSingleServiceItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleServiceItem.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleServiceItem.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSingleServiceItem.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSingleServiceItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleServiceItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleServiceItem.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSingleServiceItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleServiceItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSingleServiceItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleServiceItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleServiceItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleServiceItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleServiceItem.Editable = true;
			comboBoxSingleServiceItem.FilterString = "";
			comboBoxSingleServiceItem.HasAllAccount = false;
			comboBoxSingleServiceItem.HasCustom = false;
			comboBoxSingleServiceItem.IsDataLoaded = false;
			comboBoxSingleServiceItem.Location = new System.Drawing.Point(186, 0);
			comboBoxSingleServiceItem.MaxDropDownItems = 12;
			comboBoxSingleServiceItem.Name = "comboBoxSingleServiceItem";
			comboBoxSingleServiceItem.ShowInactiveItems = false;
			comboBoxSingleServiceItem.ShowQuickAdd = true;
			comboBoxSingleServiceItem.Size = new System.Drawing.Size(241, 20);
			comboBoxSingleServiceItem.TabIndex = 16;
			comboBoxSingleServiceItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromServiceItem.Assigned = false;
			comboBoxFromServiceItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromServiceItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromServiceItem.CustomReportFieldName = "";
			comboBoxFromServiceItem.CustomReportKey = "";
			comboBoxFromServiceItem.CustomReportValueType = 1;
			comboBoxFromServiceItem.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromServiceItem.DisplayLayout.Appearance = appearance13;
			comboBoxFromServiceItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromServiceItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromServiceItem.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromServiceItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromServiceItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromServiceItem.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromServiceItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromServiceItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromServiceItem.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromServiceItem.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromServiceItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromServiceItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromServiceItem.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromServiceItem.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromServiceItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromServiceItem.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromServiceItem.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromServiceItem.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromServiceItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromServiceItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromServiceItem.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromServiceItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromServiceItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromServiceItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromServiceItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromServiceItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromServiceItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromServiceItem.Editable = true;
			comboBoxFromServiceItem.FilterString = "";
			comboBoxFromServiceItem.HasAllAccount = false;
			comboBoxFromServiceItem.HasCustom = false;
			comboBoxFromServiceItem.IsDataLoaded = false;
			comboBoxFromServiceItem.Location = new System.Drawing.Point(186, 24);
			comboBoxFromServiceItem.MaxDropDownItems = 12;
			comboBoxFromServiceItem.Name = "comboBoxFromServiceItem";
			comboBoxFromServiceItem.ShowInactiveItems = false;
			comboBoxFromServiceItem.ShowQuickAdd = true;
			comboBoxFromServiceItem.Size = new System.Drawing.Size(106, 20);
			comboBoxFromServiceItem.TabIndex = 17;
			comboBoxFromServiceItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToServiceItem.Assigned = false;
			comboBoxToServiceItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToServiceItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToServiceItem.CustomReportFieldName = "";
			comboBoxToServiceItem.CustomReportKey = "";
			comboBoxToServiceItem.CustomReportValueType = 1;
			comboBoxToServiceItem.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToServiceItem.DisplayLayout.Appearance = appearance25;
			comboBoxToServiceItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToServiceItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToServiceItem.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToServiceItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToServiceItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToServiceItem.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToServiceItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToServiceItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToServiceItem.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToServiceItem.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToServiceItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToServiceItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToServiceItem.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToServiceItem.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToServiceItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToServiceItem.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToServiceItem.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToServiceItem.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToServiceItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToServiceItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToServiceItem.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToServiceItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToServiceItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToServiceItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToServiceItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToServiceItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToServiceItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToServiceItem.Editable = true;
			comboBoxToServiceItem.FilterString = "";
			comboBoxToServiceItem.HasAllAccount = false;
			comboBoxToServiceItem.HasCustom = false;
			comboBoxToServiceItem.IsDataLoaded = false;
			comboBoxToServiceItem.Location = new System.Drawing.Point(327, 24);
			comboBoxToServiceItem.MaxDropDownItems = 12;
			comboBoxToServiceItem.Name = "comboBoxToServiceItem";
			comboBoxToServiceItem.ShowInactiveItems = false;
			comboBoxToServiceItem.ShowQuickAdd = true;
			comboBoxToServiceItem.Size = new System.Drawing.Size(100, 20);
			comboBoxToServiceItem.TabIndex = 18;
			comboBoxToServiceItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToServiceItem);
			base.Controls.Add(comboBoxFromServiceItem);
			base.Controls.Add(comboBoxSingleServiceItem);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "ServiceSelector";
			base.Size = new System.Drawing.Size(430, 48);
			base.Load += new System.EventHandler(ServiceSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxSingleServiceItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromServiceItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToServiceItem).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
