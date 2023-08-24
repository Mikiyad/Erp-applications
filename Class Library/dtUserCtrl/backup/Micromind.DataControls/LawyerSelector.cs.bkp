using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class LawyerSelector : UserControl, ICustomReportControl
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

		private LawyerComboBox comboBoxSingleLawyer;

		private LawyerComboBox comboBoxToLawyer;

		private LawyerComboBox comboBoxFromLawyer;

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

		public string FromLawyer
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleLawyer.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromLawyer.SelectedID;
				}
				return "";
			}
		}

		public string ToLawyer
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleLawyer.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToLawyer.SelectedID;
				}
				return "";
			}
		}

		public LawyerSelector()
		{
			InitializeComponent();
		}

		private void VehicleSelector_Load(object sender, EventArgs e)
		{
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return "ANY(SELECT VehicleID FROM Vehicle)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxSingleLawyer.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return "ANY(SELECT VehicleID FROM Vehicle WHERE VehicleID Between '" + comboBoxFromLawyer.SelectedID + "' AND '" + comboBoxToLawyer.SelectedID + "')";
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
				return crFieldName = "'" + comboBoxSingleLawyer.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromLawyer.SelectedID + "' AND '" + comboBoxToLawyer.SelectedID + "')";
			}
			return "''=''";
		}

		private void EnableDisableControls()
		{
			comboBoxSingleLawyer.Enabled = radioButtonSingle.Checked;
			LawyerComboBox lawyerComboBox = comboBoxFromLawyer;
			bool enabled = comboBoxToLawyer.Enabled = radioButtonRange.Checked;
			lawyerComboBox.Enabled = enabled;
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
			comboBoxSingleLawyer = new Micromind.DataControls.LawyerComboBox();
			comboBoxToLawyer = new Micromind.DataControls.LawyerComboBox();
			comboBoxFromLawyer = new Micromind.DataControls.LawyerComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleLawyer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToLawyer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLawyer).BeginInit();
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
			radioButtonAll.Size = new System.Drawing.Size(78, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Lawyers";
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
			comboBoxSingleLawyer.Assigned = false;
			comboBoxSingleLawyer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSingleLawyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleLawyer.CustomReportFieldName = "";
			comboBoxSingleLawyer.CustomReportKey = "";
			comboBoxSingleLawyer.CustomReportValueType = 1;
			comboBoxSingleLawyer.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleLawyer.DisplayLayout.Appearance = appearance;
			comboBoxSingleLawyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleLawyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleLawyer.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleLawyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSingleLawyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleLawyer.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSingleLawyer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleLawyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleLawyer.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleLawyer.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSingleLawyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleLawyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleLawyer.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleLawyer.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSingleLawyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleLawyer.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleLawyer.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSingleLawyer.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSingleLawyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleLawyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleLawyer.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSingleLawyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleLawyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSingleLawyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleLawyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleLawyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleLawyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleLawyer.Editable = true;
			comboBoxSingleLawyer.Enabled = false;
			comboBoxSingleLawyer.FilterString = "";
			comboBoxSingleLawyer.HasAllAccount = false;
			comboBoxSingleLawyer.HasCustom = false;
			comboBoxSingleLawyer.IsDataLoaded = false;
			comboBoxSingleLawyer.Location = new System.Drawing.Point(186, 2);
			comboBoxSingleLawyer.MaxDropDownItems = 12;
			comboBoxSingleLawyer.Name = "comboBoxSingleLawyer";
			comboBoxSingleLawyer.ShowInactiveItems = false;
			comboBoxSingleLawyer.ShowQuickAdd = true;
			comboBoxSingleLawyer.Size = new System.Drawing.Size(241, 20);
			comboBoxSingleLawyer.TabIndex = 12;
			comboBoxSingleLawyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToLawyer.Assigned = false;
			comboBoxToLawyer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToLawyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToLawyer.CustomReportFieldName = "";
			comboBoxToLawyer.CustomReportKey = "";
			comboBoxToLawyer.CustomReportValueType = 1;
			comboBoxToLawyer.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToLawyer.DisplayLayout.Appearance = appearance13;
			comboBoxToLawyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToLawyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLawyer.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLawyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxToLawyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLawyer.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxToLawyer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToLawyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToLawyer.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToLawyer.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxToLawyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToLawyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToLawyer.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToLawyer.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxToLawyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToLawyer.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLawyer.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxToLawyer.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxToLawyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToLawyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxToLawyer.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxToLawyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToLawyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxToLawyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToLawyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToLawyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToLawyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToLawyer.Editable = true;
			comboBoxToLawyer.Enabled = false;
			comboBoxToLawyer.FilterString = "";
			comboBoxToLawyer.HasAllAccount = false;
			comboBoxToLawyer.HasCustom = false;
			comboBoxToLawyer.IsDataLoaded = false;
			comboBoxToLawyer.Location = new System.Drawing.Point(315, 25);
			comboBoxToLawyer.MaxDropDownItems = 12;
			comboBoxToLawyer.Name = "comboBoxToLawyer";
			comboBoxToLawyer.ShowInactiveItems = false;
			comboBoxToLawyer.ShowQuickAdd = true;
			comboBoxToLawyer.Size = new System.Drawing.Size(112, 20);
			comboBoxToLawyer.TabIndex = 14;
			comboBoxToLawyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromLawyer.Assigned = false;
			comboBoxFromLawyer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromLawyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromLawyer.CustomReportFieldName = "";
			comboBoxFromLawyer.CustomReportKey = "";
			comboBoxFromLawyer.CustomReportValueType = 1;
			comboBoxFromLawyer.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromLawyer.DisplayLayout.Appearance = appearance25;
			comboBoxFromLawyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromLawyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLawyer.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLawyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxFromLawyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLawyer.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxFromLawyer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromLawyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromLawyer.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromLawyer.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxFromLawyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromLawyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromLawyer.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromLawyer.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxFromLawyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromLawyer.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLawyer.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxFromLawyer.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxFromLawyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromLawyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromLawyer.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxFromLawyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromLawyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxFromLawyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromLawyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromLawyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromLawyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromLawyer.Editable = true;
			comboBoxFromLawyer.Enabled = false;
			comboBoxFromLawyer.FilterString = "";
			comboBoxFromLawyer.HasAllAccount = false;
			comboBoxFromLawyer.HasCustom = false;
			comboBoxFromLawyer.IsDataLoaded = false;
			comboBoxFromLawyer.Location = new System.Drawing.Point(186, 25);
			comboBoxFromLawyer.MaxDropDownItems = 12;
			comboBoxFromLawyer.Name = "comboBoxFromLawyer";
			comboBoxFromLawyer.ShowInactiveItems = false;
			comboBoxFromLawyer.ShowQuickAdd = true;
			comboBoxFromLawyer.Size = new System.Drawing.Size(112, 20);
			comboBoxFromLawyer.TabIndex = 15;
			comboBoxFromLawyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxFromLawyer);
			base.Controls.Add(comboBoxToLawyer);
			base.Controls.Add(comboBoxSingleLawyer);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "LawyerSelector";
			base.Size = new System.Drawing.Size(430, 48);
			base.Load += new System.EventHandler(VehicleSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxSingleLawyer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToLawyer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLawyer).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
