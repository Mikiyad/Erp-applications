using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class WorkLocationSelector : UserControl, ICustomReportControl
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

		private WorkLocationComboBox comboBoxSingleWorkLocation;

		private WorkLocationComboBox comboBoxFromWorkLocation;

		private WorkLocationComboBox comboBoxToWorkLocation;

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

		public string FromLocation
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleWorkLocation.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromWorkLocation.SelectedID;
				}
				return "";
			}
		}

		public string ToLocation
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleWorkLocation.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToWorkLocation.SelectedID;
				}
				return "";
			}
		}

		public WorkLocationSelector()
		{
			InitializeComponent();
		}

		private void WorkLocationSelector_Load(object sender, EventArgs e)
		{
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return "ANY(SELECT WorkLocationID FROM WorkLocation)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxSingleWorkLocation.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return "ANY(SELECT WorkLocationID FROM WorkLocation WHERE WorkLocationID Between '" + comboBoxFromWorkLocation.SelectedID + "' AND '" + comboBoxToWorkLocation.SelectedID + "')";
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
				return crFieldName + " = '" + comboBoxSingleWorkLocation.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromWorkLocation.SelectedID + "' AND '" + comboBoxToWorkLocation.SelectedID + "')";
			}
			return "''=''";
		}

		private void EnableDisableControls()
		{
			comboBoxSingleWorkLocation.Enabled = radioButtonSingle.Checked;
			WorkLocationComboBox workLocationComboBox = comboBoxFromWorkLocation;
			bool enabled = comboBoxToWorkLocation.Enabled = radioButtonRange.Checked;
			workLocationComboBox.Enabled = enabled;
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
			comboBoxSingleWorkLocation = new Micromind.DataControls.WorkLocationComboBox();
			comboBoxFromWorkLocation = new Micromind.DataControls.WorkLocationComboBox();
			comboBoxToWorkLocation = new Micromind.DataControls.WorkLocationComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleWorkLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromWorkLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToWorkLocation).BeginInit();
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
			radioButtonAll.Size = new System.Drawing.Size(85, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Locations";
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
			comboBoxSingleWorkLocation.Assigned = false;
			comboBoxSingleWorkLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSingleWorkLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleWorkLocation.CustomReportFieldName = "";
			comboBoxSingleWorkLocation.CustomReportKey = "";
			comboBoxSingleWorkLocation.CustomReportValueType = 1;
			comboBoxSingleWorkLocation.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleWorkLocation.DisplayLayout.Appearance = appearance;
			comboBoxSingleWorkLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleWorkLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleWorkLocation.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleWorkLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSingleWorkLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleWorkLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSingleWorkLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleWorkLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleWorkLocation.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleWorkLocation.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSingleWorkLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleWorkLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleWorkLocation.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleWorkLocation.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSingleWorkLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleWorkLocation.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleWorkLocation.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSingleWorkLocation.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSingleWorkLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleWorkLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleWorkLocation.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSingleWorkLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleWorkLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSingleWorkLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleWorkLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleWorkLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleWorkLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleWorkLocation.Editable = true;
			comboBoxSingleWorkLocation.Enabled = false;
			comboBoxSingleWorkLocation.FilterString = "";
			comboBoxSingleWorkLocation.HasAllAccount = false;
			comboBoxSingleWorkLocation.HasCustom = false;
			comboBoxSingleWorkLocation.IsDataLoaded = false;
			comboBoxSingleWorkLocation.Location = new System.Drawing.Point(159, 3);
			comboBoxSingleWorkLocation.MaxDropDownItems = 12;
			comboBoxSingleWorkLocation.Name = "comboBoxSingleWorkLocation";
			comboBoxSingleWorkLocation.ShowAll = false;
			comboBoxSingleWorkLocation.ShowConsignIn = false;
			comboBoxSingleWorkLocation.ShowConsignOut = false;
			comboBoxSingleWorkLocation.ShowInactiveItems = false;
			comboBoxSingleWorkLocation.ShowNormalLocations = true;
			comboBoxSingleWorkLocation.ShowPOSOnly = false;
			comboBoxSingleWorkLocation.ShowQuickAdd = true;
			comboBoxSingleWorkLocation.ShowWarehouseOnly = false;
			comboBoxSingleWorkLocation.Size = new System.Drawing.Size(238, 20);
			comboBoxSingleWorkLocation.TabIndex = 7;
			comboBoxSingleWorkLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromWorkLocation.Assigned = false;
			comboBoxFromWorkLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromWorkLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromWorkLocation.CustomReportFieldName = "";
			comboBoxFromWorkLocation.CustomReportKey = "";
			comboBoxFromWorkLocation.CustomReportValueType = 1;
			comboBoxFromWorkLocation.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromWorkLocation.DisplayLayout.Appearance = appearance13;
			comboBoxFromWorkLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromWorkLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromWorkLocation.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromWorkLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromWorkLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromWorkLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromWorkLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromWorkLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromWorkLocation.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromWorkLocation.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromWorkLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromWorkLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromWorkLocation.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromWorkLocation.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromWorkLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromWorkLocation.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromWorkLocation.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromWorkLocation.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromWorkLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromWorkLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromWorkLocation.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromWorkLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromWorkLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromWorkLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromWorkLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromWorkLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromWorkLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromWorkLocation.Editable = true;
			comboBoxFromWorkLocation.Enabled = false;
			comboBoxFromWorkLocation.FilterString = "";
			comboBoxFromWorkLocation.HasAllAccount = false;
			comboBoxFromWorkLocation.HasCustom = false;
			comboBoxFromWorkLocation.IsDataLoaded = false;
			comboBoxFromWorkLocation.Location = new System.Drawing.Point(159, 26);
			comboBoxFromWorkLocation.MaxDropDownItems = 12;
			comboBoxFromWorkLocation.Name = "comboBoxFromWorkLocation";
			comboBoxFromWorkLocation.ShowAll = false;
			comboBoxFromWorkLocation.ShowConsignIn = false;
			comboBoxFromWorkLocation.ShowConsignOut = false;
			comboBoxFromWorkLocation.ShowInactiveItems = false;
			comboBoxFromWorkLocation.ShowNormalLocations = true;
			comboBoxFromWorkLocation.ShowPOSOnly = false;
			comboBoxFromWorkLocation.ShowQuickAdd = true;
			comboBoxFromWorkLocation.ShowWarehouseOnly = false;
			comboBoxFromWorkLocation.Size = new System.Drawing.Size(100, 20);
			comboBoxFromWorkLocation.TabIndex = 8;
			comboBoxFromWorkLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToWorkLocation.Assigned = false;
			comboBoxToWorkLocation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToWorkLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToWorkLocation.CustomReportFieldName = "";
			comboBoxToWorkLocation.CustomReportKey = "";
			comboBoxToWorkLocation.CustomReportValueType = 1;
			comboBoxToWorkLocation.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToWorkLocation.DisplayLayout.Appearance = appearance25;
			comboBoxToWorkLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToWorkLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToWorkLocation.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToWorkLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToWorkLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToWorkLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToWorkLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToWorkLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToWorkLocation.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToWorkLocation.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToWorkLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToWorkLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToWorkLocation.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToWorkLocation.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToWorkLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToWorkLocation.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToWorkLocation.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToWorkLocation.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToWorkLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToWorkLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToWorkLocation.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToWorkLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToWorkLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToWorkLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToWorkLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToWorkLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToWorkLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToWorkLocation.Editable = true;
			comboBoxToWorkLocation.Enabled = false;
			comboBoxToWorkLocation.FilterString = "";
			comboBoxToWorkLocation.HasAllAccount = false;
			comboBoxToWorkLocation.HasCustom = false;
			comboBoxToWorkLocation.IsDataLoaded = false;
			comboBoxToWorkLocation.Location = new System.Drawing.Point(297, 26);
			comboBoxToWorkLocation.MaxDropDownItems = 12;
			comboBoxToWorkLocation.Name = "comboBoxToWorkLocation";
			comboBoxToWorkLocation.ShowAll = false;
			comboBoxToWorkLocation.ShowConsignIn = false;
			comboBoxToWorkLocation.ShowConsignOut = false;
			comboBoxToWorkLocation.ShowInactiveItems = false;
			comboBoxToWorkLocation.ShowNormalLocations = true;
			comboBoxToWorkLocation.ShowPOSOnly = false;
			comboBoxToWorkLocation.ShowQuickAdd = true;
			comboBoxToWorkLocation.ShowWarehouseOnly = false;
			comboBoxToWorkLocation.Size = new System.Drawing.Size(100, 20);
			comboBoxToWorkLocation.TabIndex = 9;
			comboBoxToWorkLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToWorkLocation);
			base.Controls.Add(comboBoxFromWorkLocation);
			base.Controls.Add(comboBoxSingleWorkLocation);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "WorkLocationSelector";
			base.Size = new System.Drawing.Size(414, 54);
			base.Load += new System.EventHandler(WorkLocationSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxSingleWorkLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromWorkLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToWorkLocation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
