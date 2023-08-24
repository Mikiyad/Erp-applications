using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class PropertyUnitSelector : UserControl, ICustomReportControl
	{
		private bool showGroupAndClass = true;

		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private Label label3;

		private RadioButton radioButtonSingle;

		private PropertyUnitComboBox comboBoxSinglePropertyUnit;

		private PropertyUnitComboBox comboBoxFromUnit;

		private PropertyUnitComboBox comboBoxToUnit;

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

		[DefaultValue(true)]
		public bool ShowGroupAndClass
		{
			get
			{
				return showGroupAndClass;
			}
			set
			{
				showGroupAndClass = value;
				if (value)
				{
					RadioButton radioButton = radioButtonRange;
					PropertyUnitComboBox propertyUnitComboBox = comboBoxToUnit;
					PropertyUnitComboBox propertyUnitComboBox2 = comboBoxFromUnit;
					bool flag2 = comboBoxToUnit.Visible = true;
					bool flag4 = propertyUnitComboBox2.Visible = flag2;
					bool visible = propertyUnitComboBox.Visible = flag4;
					radioButton.Visible = visible;
					base.Height = 99;
				}
				else
				{
					RadioButton radioButton2 = radioButtonRange;
					PropertyUnitComboBox propertyUnitComboBox3 = comboBoxFromUnit;
					bool flag4 = comboBoxToUnit.Visible = false;
					bool visible = propertyUnitComboBox3.Visible = flag4;
					radioButton2.Visible = visible;
					base.Height = 48;
				}
			}
		}

		public bool IsSingleCustomer => radioButtonSingle.Checked;

		public string FromUnit
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSinglePropertyUnit.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromUnit.SelectedID;
				}
				return "";
			}
		}

		public string ToUnit
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSinglePropertyUnit.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToUnit.SelectedID;
				}
				return "";
			}
		}

		public string FromUnitName
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSinglePropertyUnit.SelectedName;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromUnit.SelectedName;
				}
				return "";
			}
		}

		public string ToUnitName
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSinglePropertyUnit.SelectedName;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToUnit.SelectedName;
				}
				return "";
			}
		}

		public PropertyUnitSelector()
		{
			InitializeComponent();
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return "ANY(SELECT PropertyUnitID FROM Property_Unit)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxSinglePropertyUnit.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return "ANY(SELECT PropertyUnitID FROM Property_Unit WHERE PropertyUnitID Between '" + comboBoxFromUnit.SelectedID + "' AND '" + comboBoxToUnit.SelectedID + "')";
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
				return crFieldName + " = '" + comboBoxSinglePropertyUnit.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " = ANY(SELECT PropertyUnitID FROM Property_Unit WHERE PropertyUnitID Between '" + comboBoxFromUnit.SelectedID + "' AND '" + comboBoxToUnit.SelectedID + "')";
			}
			return "''=''";
		}

		private void PropertyUnitSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSinglePropertyUnit.Enabled = radioButtonSingle.Checked;
			PropertyUnitComboBox propertyUnitComboBox = comboBoxFromUnit;
			bool enabled = comboBoxToUnit.Enabled = radioButtonRange.Checked;
			propertyUnitComboBox.Enabled = enabled;
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
			comboBoxSinglePropertyUnit = new Micromind.DataControls.PropertyUnitComboBox();
			comboBoxFromUnit = new Micromind.DataControls.PropertyUnitComboBox();
			comboBoxToUnit = new Micromind.DataControls.PropertyUnitComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxSinglePropertyUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToUnit).BeginInit();
			SuspendLayout();
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(294, 27);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(63, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Units";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 24);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(57, 17);
			radioButtonRange.TabIndex = 6;
			radioButtonRange.Text = "Range";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(139, 26);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 6;
			label3.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(123, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxSinglePropertyUnit.Assigned = false;
			comboBoxSinglePropertyUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSinglePropertyUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSinglePropertyUnit.CustomReportFieldName = "";
			comboBoxSinglePropertyUnit.CustomReportKey = "";
			comboBoxSinglePropertyUnit.CustomReportValueType = 1;
			comboBoxSinglePropertyUnit.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSinglePropertyUnit.DisplayLayout.Appearance = appearance;
			comboBoxSinglePropertyUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSinglePropertyUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSinglePropertyUnit.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSinglePropertyUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSinglePropertyUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSinglePropertyUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSinglePropertyUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSinglePropertyUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSinglePropertyUnit.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSinglePropertyUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSinglePropertyUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSinglePropertyUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSinglePropertyUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSinglePropertyUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSinglePropertyUnit.Editable = true;
			comboBoxSinglePropertyUnit.Enabled = false;
			comboBoxSinglePropertyUnit.FilterString = "";
			comboBoxSinglePropertyUnit.HasAllAccount = false;
			comboBoxSinglePropertyUnit.HasCustom = false;
			comboBoxSinglePropertyUnit.IsDataLoaded = false;
			comboBoxSinglePropertyUnit.Location = new System.Drawing.Point(182, 0);
			comboBoxSinglePropertyUnit.MaxDropDownItems = 12;
			comboBoxSinglePropertyUnit.Name = "comboBoxSinglePropertyUnit";
			comboBoxSinglePropertyUnit.ShowInactiveItems = false;
			comboBoxSinglePropertyUnit.ShowQuickAdd = true;
			comboBoxSinglePropertyUnit.Size = new System.Drawing.Size(243, 20);
			comboBoxSinglePropertyUnit.TabIndex = 17;
			comboBoxSinglePropertyUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromUnit.Assigned = false;
			comboBoxFromUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromUnit.CustomReportFieldName = "";
			comboBoxFromUnit.CustomReportKey = "";
			comboBoxFromUnit.CustomReportValueType = 1;
			comboBoxFromUnit.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromUnit.DisplayLayout.Appearance = appearance13;
			comboBoxFromUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromUnit.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromUnit.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromUnit.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromUnit.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromUnit.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromUnit.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromUnit.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromUnit.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromUnit.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromUnit.Editable = true;
			comboBoxFromUnit.Enabled = false;
			comboBoxFromUnit.FilterString = "";
			comboBoxFromUnit.HasAllAccount = false;
			comboBoxFromUnit.HasCustom = false;
			comboBoxFromUnit.IsDataLoaded = false;
			comboBoxFromUnit.Location = new System.Drawing.Point(182, 22);
			comboBoxFromUnit.MaxDropDownItems = 12;
			comboBoxFromUnit.Name = "comboBoxFromUnit";
			comboBoxFromUnit.ShowInactiveItems = false;
			comboBoxFromUnit.ShowQuickAdd = true;
			comboBoxFromUnit.Size = new System.Drawing.Size(112, 20);
			comboBoxFromUnit.TabIndex = 18;
			comboBoxFromUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToUnit.Assigned = false;
			comboBoxToUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToUnit.CustomReportFieldName = "";
			comboBoxToUnit.CustomReportKey = "";
			comboBoxToUnit.CustomReportValueType = 1;
			comboBoxToUnit.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToUnit.DisplayLayout.Appearance = appearance25;
			comboBoxToUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToUnit.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToUnit.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToUnit.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToUnit.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToUnit.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToUnit.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToUnit.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToUnit.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToUnit.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToUnit.Editable = true;
			comboBoxToUnit.Enabled = false;
			comboBoxToUnit.FilterString = "";
			comboBoxToUnit.HasAllAccount = false;
			comboBoxToUnit.HasCustom = false;
			comboBoxToUnit.IsDataLoaded = false;
			comboBoxToUnit.Location = new System.Drawing.Point(313, 23);
			comboBoxToUnit.MaxDropDownItems = 12;
			comboBoxToUnit.Name = "comboBoxToUnit";
			comboBoxToUnit.ShowInactiveItems = false;
			comboBoxToUnit.ShowQuickAdd = true;
			comboBoxToUnit.Size = new System.Drawing.Size(112, 20);
			comboBoxToUnit.TabIndex = 19;
			comboBoxToUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToUnit);
			base.Controls.Add(comboBoxFromUnit);
			base.Controls.Add(comboBoxSinglePropertyUnit);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "PropertyUnitSelector";
			base.Size = new System.Drawing.Size(428, 51);
			base.Load += new System.EventHandler(PropertyUnitSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxSinglePropertyUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToUnit).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
