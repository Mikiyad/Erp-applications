using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class PropertySelector : UserControl, ICustomReportControl
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

		private PropertyComboBox comboBoxSingleProperty;

		private PropertyComboBox comboBoxFromProperty;

		private PropertyComboBox comboBoxToProperty;

		private RadioButton radioButtonClass;

		private Label label4;

		private Label label6;

		private PropertyClassComboBox comboBoxFromPropertyClass;

		private PropertyClassComboBox comboBoxToPropertyClass;

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
					PropertyComboBox propertyComboBox = comboBoxFromProperty;
					bool flag2 = comboBoxToProperty.Visible = true;
					bool visible = propertyComboBox.Visible = flag2;
					radioButton.Visible = visible;
					base.Height = 99;
				}
				else
				{
					RadioButton radioButton2 = radioButtonRange;
					PropertyComboBox propertyComboBox2 = comboBoxFromProperty;
					bool flag2 = comboBoxToProperty.Visible = false;
					bool visible = propertyComboBox2.Visible = flag2;
					radioButton2.Visible = visible;
					base.Height = 48;
				}
			}
		}

		public bool IsSingleCustomer => radioButtonSingle.Checked;

		public string FromProperty
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleProperty.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromProperty.SelectedID;
				}
				return "";
			}
		}

		public string ToProperty
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleProperty.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToProperty.SelectedID;
				}
				return "";
			}
		}

		public string FromPropertyName
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleProperty.SelectedName;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromProperty.SelectedName;
				}
				return "";
			}
		}

		public string ToPropertyName
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleProperty.SelectedName;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToProperty.SelectedName;
				}
				return "";
			}
		}

		public string FromClass
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxFromPropertyClass.SelectedID;
				}
				return "";
			}
		}

		public string ToClass
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxToPropertyClass.SelectedID;
				}
				return "";
			}
		}

		public string FromClassName
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxFromPropertyClass.SelectedName;
				}
				return "";
			}
		}

		public string ToClassName
		{
			get
			{
				if (radioButtonClass.Checked)
				{
					return comboBoxToPropertyClass.SelectedName;
				}
				return "";
			}
		}

		public string FromUnit => "";

		public string ToUnit => "";

		public PropertySelector()
		{
			InitializeComponent();
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return "ANY(SELECT PropertyID FROM Property)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxSingleProperty.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return "ANY(SELECT PropertyID FROM Property WHERE PropertyID Between '" + comboBoxFromProperty.SelectedID + "' AND '" + comboBoxToProperty.SelectedID + "')";
				}
				if (radioButtonClass.Checked)
				{
					return "ANY(SELECT PropertyID FROM Property WHERE PropertyClassID Between '" + comboBoxFromPropertyClass.SelectedID + "' AND '" + comboBoxToPropertyClass.SelectedID + "')";
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
				return crFieldName + " = '" + comboBoxSingleProperty.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromProperty.SelectedID + "' AND '" + comboBoxToProperty.SelectedID + "')";
			}
			if (radioButtonClass.Checked)
			{
				return crFieldName + " = ANY(SELECT PropertyID FROM Customer WHERE CustomerClassID Between '" + comboBoxFromPropertyClass.SelectedID + "' AND '" + comboBoxToPropertyClass.SelectedID + "')";
			}
			return "''=''";
		}

		private void PropertySelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleProperty.Enabled = radioButtonSingle.Checked;
			PropertyComboBox propertyComboBox = comboBoxFromProperty;
			bool enabled = comboBoxToProperty.Enabled = radioButtonRange.Checked;
			propertyComboBox.Enabled = enabled;
			PropertyClassComboBox propertyClassComboBox = comboBoxFromPropertyClass;
			enabled = (comboBoxToPropertyClass.Enabled = radioButtonClass.Checked);
			propertyClassComboBox.Enabled = enabled;
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
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			label3 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			radioButtonClass = new System.Windows.Forms.RadioButton();
			label4 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			comboBoxToPropertyClass = new Micromind.DataControls.PropertyClassComboBox();
			comboBoxFromPropertyClass = new Micromind.DataControls.PropertyClassComboBox();
			comboBoxToProperty = new Micromind.DataControls.PropertyComboBox();
			comboBoxFromProperty = new Micromind.DataControls.PropertyComboBox();
			comboBoxSingleProperty = new Micromind.DataControls.PropertyComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxToPropertyClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromPropertyClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToProperty).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromProperty).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleProperty).BeginInit();
			SuspendLayout();
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(295, 29);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(86, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Properties";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 29);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(63, 17);
			radioButtonRange.TabIndex = 6;
			radioButtonRange.Text = "Range :";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(139, 29);
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
			radioButtonClass.AutoSize = true;
			radioButtonClass.Location = new System.Drawing.Point(7, 51);
			radioButtonClass.Name = "radioButtonClass";
			radioButtonClass.Size = new System.Drawing.Size(95, 17);
			radioButtonClass.TabIndex = 18;
			radioButtonClass.Text = "Property Class:";
			radioButtonClass.UseVisualStyleBackColor = true;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(140, 51);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 19;
			label4.Text = "From:";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(296, 53);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(23, 13);
			label6.TabIndex = 21;
			label6.Text = "To:";
			comboBoxToPropertyClass.Assigned = false;
			comboBoxToPropertyClass.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToPropertyClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToPropertyClass.CustomReportFieldName = "";
			comboBoxToPropertyClass.CustomReportKey = "";
			comboBoxToPropertyClass.CustomReportValueType = 1;
			comboBoxToPropertyClass.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToPropertyClass.DisplayLayout.Appearance = appearance;
			comboBoxToPropertyClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToPropertyClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToPropertyClass.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToPropertyClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxToPropertyClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToPropertyClass.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxToPropertyClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToPropertyClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToPropertyClass.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToPropertyClass.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxToPropertyClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToPropertyClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToPropertyClass.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToPropertyClass.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxToPropertyClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToPropertyClass.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToPropertyClass.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxToPropertyClass.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxToPropertyClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToPropertyClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxToPropertyClass.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxToPropertyClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToPropertyClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxToPropertyClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToPropertyClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToPropertyClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToPropertyClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToPropertyClass.Editable = true;
			comboBoxToPropertyClass.Enabled = false;
			comboBoxToPropertyClass.FilterString = "";
			comboBoxToPropertyClass.HasAllAccount = false;
			comboBoxToPropertyClass.HasCustom = false;
			comboBoxToPropertyClass.IsDataLoaded = false;
			comboBoxToPropertyClass.Location = new System.Drawing.Point(319, 48);
			comboBoxToPropertyClass.MaxDropDownItems = 12;
			comboBoxToPropertyClass.Name = "comboBoxToPropertyClass";
			comboBoxToPropertyClass.ShowInactiveItems = false;
			comboBoxToPropertyClass.ShowQuickAdd = true;
			comboBoxToPropertyClass.Size = new System.Drawing.Size(106, 20);
			comboBoxToPropertyClass.TabIndex = 31;
			comboBoxToPropertyClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromPropertyClass.Assigned = false;
			comboBoxFromPropertyClass.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromPropertyClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromPropertyClass.CustomReportFieldName = "";
			comboBoxFromPropertyClass.CustomReportKey = "";
			comboBoxFromPropertyClass.CustomReportValueType = 1;
			comboBoxFromPropertyClass.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromPropertyClass.DisplayLayout.Appearance = appearance13;
			comboBoxFromPropertyClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromPropertyClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromPropertyClass.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromPropertyClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromPropertyClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromPropertyClass.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromPropertyClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromPropertyClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromPropertyClass.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromPropertyClass.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromPropertyClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromPropertyClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromPropertyClass.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromPropertyClass.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromPropertyClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromPropertyClass.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromPropertyClass.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromPropertyClass.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromPropertyClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromPropertyClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromPropertyClass.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromPropertyClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromPropertyClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromPropertyClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromPropertyClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromPropertyClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromPropertyClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromPropertyClass.Editable = true;
			comboBoxFromPropertyClass.Enabled = false;
			comboBoxFromPropertyClass.FilterString = "";
			comboBoxFromPropertyClass.HasAllAccount = false;
			comboBoxFromPropertyClass.HasCustom = false;
			comboBoxFromPropertyClass.IsDataLoaded = false;
			comboBoxFromPropertyClass.Location = new System.Drawing.Point(182, 47);
			comboBoxFromPropertyClass.MaxDropDownItems = 12;
			comboBoxFromPropertyClass.Name = "comboBoxFromPropertyClass";
			comboBoxFromPropertyClass.ShowInactiveItems = false;
			comboBoxFromPropertyClass.ShowQuickAdd = true;
			comboBoxFromPropertyClass.Size = new System.Drawing.Size(112, 20);
			comboBoxFromPropertyClass.TabIndex = 30;
			comboBoxFromPropertyClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToProperty.Assigned = false;
			comboBoxToProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToProperty.CustomReportFieldName = "";
			comboBoxToProperty.CustomReportKey = "";
			comboBoxToProperty.CustomReportValueType = 1;
			comboBoxToProperty.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToProperty.DisplayLayout.Appearance = appearance25;
			comboBoxToProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToProperty.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToProperty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToProperty.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToProperty.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToProperty.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToProperty.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToProperty.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToProperty.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToProperty.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToProperty.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToProperty.Editable = true;
			comboBoxToProperty.Enabled = false;
			comboBoxToProperty.FilterString = "";
			comboBoxToProperty.HasAllAccount = false;
			comboBoxToProperty.HasCustom = false;
			comboBoxToProperty.IsDataLoaded = false;
			comboBoxToProperty.Location = new System.Drawing.Point(319, 25);
			comboBoxToProperty.MaxDropDownItems = 12;
			comboBoxToProperty.Name = "comboBoxToProperty";
			comboBoxToProperty.ShowInactiveItems = false;
			comboBoxToProperty.ShowQuickAdd = true;
			comboBoxToProperty.Size = new System.Drawing.Size(106, 20);
			comboBoxToProperty.TabIndex = 14;
			comboBoxToProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromProperty.Assigned = false;
			comboBoxFromProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromProperty.CustomReportFieldName = "";
			comboBoxFromProperty.CustomReportKey = "";
			comboBoxFromProperty.CustomReportValueType = 1;
			comboBoxFromProperty.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromProperty.DisplayLayout.Appearance = appearance37;
			comboBoxFromProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromProperty.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxFromProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxFromProperty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromProperty.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromProperty.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxFromProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromProperty.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromProperty.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxFromProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromProperty.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromProperty.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxFromProperty.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxFromProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromProperty.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxFromProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxFromProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromProperty.Editable = true;
			comboBoxFromProperty.Enabled = false;
			comboBoxFromProperty.FilterString = "";
			comboBoxFromProperty.HasAllAccount = false;
			comboBoxFromProperty.HasCustom = false;
			comboBoxFromProperty.IsDataLoaded = false;
			comboBoxFromProperty.Location = new System.Drawing.Point(182, 25);
			comboBoxFromProperty.MaxDropDownItems = 12;
			comboBoxFromProperty.Name = "comboBoxFromProperty";
			comboBoxFromProperty.ShowInactiveItems = false;
			comboBoxFromProperty.ShowQuickAdd = true;
			comboBoxFromProperty.Size = new System.Drawing.Size(112, 20);
			comboBoxFromProperty.TabIndex = 13;
			comboBoxFromProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSingleProperty.Assigned = false;
			comboBoxSingleProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSingleProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleProperty.CustomReportFieldName = "";
			comboBoxSingleProperty.CustomReportKey = "";
			comboBoxSingleProperty.CustomReportValueType = 1;
			comboBoxSingleProperty.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleProperty.DisplayLayout.Appearance = appearance49;
			comboBoxSingleProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProperty.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxSingleProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxSingleProperty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleProperty.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleProperty.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxSingleProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProperty.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleProperty.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxSingleProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleProperty.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleProperty.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxSingleProperty.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxSingleProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleProperty.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxSingleProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxSingleProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleProperty.Editable = true;
			comboBoxSingleProperty.Enabled = false;
			comboBoxSingleProperty.FilterString = "";
			comboBoxSingleProperty.HasAllAccount = false;
			comboBoxSingleProperty.HasCustom = false;
			comboBoxSingleProperty.IsDataLoaded = false;
			comboBoxSingleProperty.Location = new System.Drawing.Point(182, 3);
			comboBoxSingleProperty.MaxDropDownItems = 12;
			comboBoxSingleProperty.Name = "comboBoxSingleProperty";
			comboBoxSingleProperty.ShowInactiveItems = false;
			comboBoxSingleProperty.ShowQuickAdd = true;
			comboBoxSingleProperty.Size = new System.Drawing.Size(243, 20);
			comboBoxSingleProperty.TabIndex = 12;
			comboBoxSingleProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToPropertyClass);
			base.Controls.Add(comboBoxFromPropertyClass);
			base.Controls.Add(radioButtonClass);
			base.Controls.Add(label4);
			base.Controls.Add(label6);
			base.Controls.Add(comboBoxToProperty);
			base.Controls.Add(comboBoxFromProperty);
			base.Controls.Add(comboBoxSingleProperty);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "PropertySelector";
			base.Size = new System.Drawing.Size(436, 80);
			base.Load += new System.EventHandler(PropertySelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxToPropertyClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromPropertyClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToProperty).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromProperty).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleProperty).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
