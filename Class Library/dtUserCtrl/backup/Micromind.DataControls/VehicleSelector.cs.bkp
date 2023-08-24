using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class VehicleSelector : UserControl, ICustomReportControl
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

		private VehicleComboBox comboBoxSingleVehicle;

		private VehicleComboBox comboBoxToVehicle;

		private VehicleComboBox comboBoxFromVehicle;

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

		public string FromVehicle
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleVehicle.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromVehicle.SelectedID;
				}
				return "";
			}
		}

		public string ToVehicle
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleVehicle.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToVehicle.SelectedID;
				}
				return "";
			}
		}

		public VehicleSelector()
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
					return "'" + comboBoxSingleVehicle.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return "ANY(SELECT VehicleID FROM Vehicle WHERE VehicleID Between '" + comboBoxFromVehicle.SelectedID + "' AND '" + comboBoxToVehicle.SelectedID + "')";
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
				return crFieldName = "'" + comboBoxSingleVehicle.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromVehicle.SelectedID + "' AND '" + comboBoxToVehicle.SelectedID + "')";
			}
			return "''=''";
		}

		private void EnableDisableControls()
		{
			comboBoxSingleVehicle.Enabled = radioButtonSingle.Checked;
			VehicleComboBox vehicleComboBox = comboBoxFromVehicle;
			bool enabled = comboBoxToVehicle.Enabled = radioButtonRange.Checked;
			vehicleComboBox.Enabled = enabled;
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
			comboBoxSingleVehicle = new Micromind.DataControls.VehicleComboBox();
			comboBoxToVehicle = new Micromind.DataControls.VehicleComboBox();
			comboBoxFromVehicle = new Micromind.DataControls.VehicleComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleVehicle).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToVehicle).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromVehicle).BeginInit();
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
			radioButtonAll.Size = new System.Drawing.Size(79, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Vehicles";
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
			comboBoxSingleVehicle.Assigned = false;
			comboBoxSingleVehicle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSingleVehicle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleVehicle.CustomReportFieldName = "";
			comboBoxSingleVehicle.CustomReportKey = "";
			comboBoxSingleVehicle.CustomReportValueType = 1;
			comboBoxSingleVehicle.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleVehicle.DisplayLayout.Appearance = appearance;
			comboBoxSingleVehicle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleVehicle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleVehicle.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleVehicle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSingleVehicle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleVehicle.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSingleVehicle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleVehicle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleVehicle.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleVehicle.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSingleVehicle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleVehicle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleVehicle.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleVehicle.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSingleVehicle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleVehicle.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleVehicle.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSingleVehicle.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSingleVehicle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleVehicle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleVehicle.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSingleVehicle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleVehicle.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSingleVehicle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleVehicle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleVehicle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleVehicle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleVehicle.Editable = true;
			comboBoxSingleVehicle.FilterString = "";
			comboBoxSingleVehicle.HasAllAccount = false;
			comboBoxSingleVehicle.HasCustom = false;
			comboBoxSingleVehicle.IsDataLoaded = false;
			comboBoxSingleVehicle.Location = new System.Drawing.Point(186, 2);
			comboBoxSingleVehicle.MaxDropDownItems = 12;
			comboBoxSingleVehicle.Name = "comboBoxSingleVehicle";
			comboBoxSingleVehicle.ShowInactiveItems = false;
			comboBoxSingleVehicle.ShowQuickAdd = true;
			comboBoxSingleVehicle.Size = new System.Drawing.Size(241, 20);
			comboBoxSingleVehicle.TabIndex = 12;
			comboBoxSingleVehicle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToVehicle.Assigned = false;
			comboBoxToVehicle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToVehicle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToVehicle.CustomReportFieldName = "";
			comboBoxToVehicle.CustomReportKey = "";
			comboBoxToVehicle.CustomReportValueType = 1;
			comboBoxToVehicle.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToVehicle.DisplayLayout.Appearance = appearance13;
			comboBoxToVehicle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToVehicle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToVehicle.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToVehicle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxToVehicle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToVehicle.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxToVehicle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToVehicle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToVehicle.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToVehicle.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxToVehicle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToVehicle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToVehicle.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToVehicle.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxToVehicle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToVehicle.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToVehicle.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxToVehicle.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxToVehicle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToVehicle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxToVehicle.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxToVehicle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToVehicle.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxToVehicle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToVehicle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToVehicle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToVehicle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToVehicle.Editable = true;
			comboBoxToVehicle.FilterString = "";
			comboBoxToVehicle.HasAllAccount = false;
			comboBoxToVehicle.HasCustom = false;
			comboBoxToVehicle.IsDataLoaded = false;
			comboBoxToVehicle.Location = new System.Drawing.Point(315, 25);
			comboBoxToVehicle.MaxDropDownItems = 12;
			comboBoxToVehicle.Name = "comboBoxToVehicle";
			comboBoxToVehicle.ShowInactiveItems = false;
			comboBoxToVehicle.ShowQuickAdd = true;
			comboBoxToVehicle.Size = new System.Drawing.Size(112, 20);
			comboBoxToVehicle.TabIndex = 14;
			comboBoxToVehicle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromVehicle.Assigned = false;
			comboBoxFromVehicle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromVehicle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromVehicle.CustomReportFieldName = "";
			comboBoxFromVehicle.CustomReportKey = "";
			comboBoxFromVehicle.CustomReportValueType = 1;
			comboBoxFromVehicle.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromVehicle.DisplayLayout.Appearance = appearance25;
			comboBoxFromVehicle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromVehicle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromVehicle.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromVehicle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxFromVehicle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromVehicle.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxFromVehicle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromVehicle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromVehicle.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromVehicle.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxFromVehicle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromVehicle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromVehicle.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromVehicle.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxFromVehicle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromVehicle.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromVehicle.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxFromVehicle.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxFromVehicle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromVehicle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromVehicle.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxFromVehicle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromVehicle.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxFromVehicle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromVehicle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromVehicle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromVehicle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromVehicle.Editable = true;
			comboBoxFromVehicle.FilterString = "";
			comboBoxFromVehicle.HasAllAccount = false;
			comboBoxFromVehicle.HasCustom = false;
			comboBoxFromVehicle.IsDataLoaded = false;
			comboBoxFromVehicle.Location = new System.Drawing.Point(186, 25);
			comboBoxFromVehicle.MaxDropDownItems = 12;
			comboBoxFromVehicle.Name = "comboBoxFromVehicle";
			comboBoxFromVehicle.ShowInactiveItems = false;
			comboBoxFromVehicle.ShowQuickAdd = true;
			comboBoxFromVehicle.Size = new System.Drawing.Size(112, 20);
			comboBoxFromVehicle.TabIndex = 15;
			comboBoxFromVehicle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxFromVehicle);
			base.Controls.Add(comboBoxToVehicle);
			base.Controls.Add(comboBoxSingleVehicle);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "VehicleSelector";
			base.Size = new System.Drawing.Size(430, 48);
			base.Load += new System.EventHandler(VehicleSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxSingleVehicle).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToVehicle).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromVehicle).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
