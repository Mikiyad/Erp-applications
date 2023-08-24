using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class EquipmentSelector : UserControl, ICustomReportControl
	{
		private bool showGroupAndClass = true;

		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private RadioButton radioButtonType;

		private RadioButton radioButtonCategory;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private RadioButton radioButtonSingle;

		private EAEquipmentComboBox comboBoxSingleEquipment;

		private EquipmentTypeComboBox comboBoxToType;

		private EquipmentTypeComboBox comboBoxFromType;

		private EquipmentCategoryComboBox comboBoxFromCategory;

		private EquipmentCategoryComboBox comboBoxToCategory;

		private EAEquipmentComboBox comboBoxFromEquipment;

		private EAEquipmentComboBox comboBoxToEquipment;

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
					RadioButton radioButton = radioButtonType;
					RadioButton radioButton2 = radioButtonCategory;
					EquipmentTypeComboBox equipmentTypeComboBox = comboBoxFromType;
					EquipmentTypeComboBox equipmentTypeComboBox2 = comboBoxToType;
					EquipmentCategoryComboBox equipmentCategoryComboBox = comboBoxFromCategory;
					bool flag2 = comboBoxToCategory.Visible = true;
					bool flag4 = equipmentCategoryComboBox.Visible = flag2;
					bool flag6 = equipmentTypeComboBox2.Visible = flag4;
					bool flag8 = equipmentTypeComboBox.Visible = flag6;
					bool visible = radioButton2.Visible = flag8;
					radioButton.Visible = visible;
					base.Height = 99;
				}
				else
				{
					RadioButton radioButton3 = radioButtonType;
					RadioButton radioButton4 = radioButtonCategory;
					EquipmentTypeComboBox equipmentTypeComboBox3 = comboBoxFromType;
					EquipmentTypeComboBox equipmentTypeComboBox4 = comboBoxToType;
					EquipmentCategoryComboBox equipmentCategoryComboBox2 = comboBoxFromCategory;
					bool flag2 = comboBoxToCategory.Visible = false;
					bool flag4 = equipmentCategoryComboBox2.Visible = flag2;
					bool flag6 = equipmentTypeComboBox4.Visible = flag4;
					bool flag8 = equipmentTypeComboBox3.Visible = flag6;
					bool visible = radioButton4.Visible = flag8;
					radioButton3.Visible = visible;
					base.Height = 48;
				}
			}
		}

		public bool IsSingleEquipment => radioButtonSingle.Checked;

		public string FromEquipment
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleEquipment.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromEquipment.SelectedID;
				}
				return "";
			}
		}

		public string ToEquipment
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleEquipment.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToEquipment.SelectedID;
				}
				return "";
			}
		}

		public string FromType
		{
			get
			{
				if (radioButtonType.Checked)
				{
					return comboBoxFromType.SelectedID;
				}
				return "";
			}
		}

		public string ToType
		{
			get
			{
				if (radioButtonType.Checked)
				{
					return comboBoxToType.SelectedID;
				}
				return "";
			}
		}

		public string FromCategory
		{
			get
			{
				if (radioButtonCategory.Checked)
				{
					return comboBoxFromCategory.SelectedID;
				}
				return "";
			}
		}

		public string ToCategory
		{
			get
			{
				if (radioButtonCategory.Checked)
				{
					return comboBoxToCategory.SelectedID;
				}
				return "";
			}
		}

		public EquipmentSelector()
		{
			InitializeComponent();
		}

		public string GetParameterValue()
		{
			if (radioButtonAll.Checked)
			{
				return "ANY(SELECT EquipmentID FROM Equipment)";
			}
			if (radioButtonSingle.Checked)
			{
				return "'" + comboBoxSingleEquipment.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return "ANY(SELECT EquipmentID FROM Equipment WHERE EquipmentID Between '" + comboBoxFromEquipment.SelectedID + "' AND '" + comboBoxToEquipment.SelectedID + "')";
			}
			if (radioButtonType.Checked)
			{
				return "ANY(SELECT EquipmentID FROM Equipment WHERE EquipmentTypeID Between '" + comboBoxFromType.SelectedID + "' AND '" + comboBoxToType.SelectedID + "')";
			}
			if (radioButtonCategory.Checked)
			{
				return "ANY(SELECT EquipmentID FROM Equipment WHERE EquipmentCategoryID Between '" + comboBoxFromCategory.SelectedID + "' AND '" + comboBoxToCategory.SelectedID + "')";
			}
			return "1=1";
		}

		private void radioButtons_CheckedChanged(object sender, EventArgs e)
		{
			EnableDisableControls();
		}

		private void EquipmentSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleEquipment.Enabled = radioButtonSingle.Checked;
			EAEquipmentComboBox eAEquipmentComboBox = comboBoxFromEquipment;
			bool enabled = comboBoxToEquipment.Enabled = radioButtonRange.Checked;
			eAEquipmentComboBox.Enabled = enabled;
			EquipmentTypeComboBox equipmentTypeComboBox = comboBoxFromType;
			enabled = (comboBoxToType.Enabled = radioButtonType.Checked);
			equipmentTypeComboBox.Enabled = enabled;
			EquipmentCategoryComboBox equipmentCategoryComboBox = comboBoxFromCategory;
			enabled = (comboBoxToCategory.Enabled = radioButtonCategory.Checked);
			equipmentCategoryComboBox.Enabled = enabled;
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
			Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			radioButtonType = new System.Windows.Forms.RadioButton();
			radioButtonCategory = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxSingleEquipment = new Micromind.DataControls.EAEquipmentComboBox();
			comboBoxToType = new Micromind.DataControls.EquipmentTypeComboBox();
			comboBoxFromType = new Micromind.DataControls.EquipmentTypeComboBox();
			comboBoxFromCategory = new Micromind.DataControls.EquipmentCategoryComboBox();
			comboBoxToCategory = new Micromind.DataControls.EquipmentCategoryComboBox();
			comboBoxFromEquipment = new Micromind.DataControls.EAEquipmentComboBox();
			comboBoxToEquipment = new Micromind.DataControls.EAEquipmentComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleEquipment).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromEquipment).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToEquipment).BeginInit();
			SuspendLayout();
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(295, 30);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(94, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Equipments";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(7, 28);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(60, 17);
			radioButtonRange.TabIndex = 3;
			radioButtonRange.Text = "Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonType.AutoSize = true;
			radioButtonType.Location = new System.Drawing.Point(6, 51);
			radioButtonType.Name = "radioButtonType";
			radioButtonType.Size = new System.Drawing.Size(74, 17);
			radioButtonType.TabIndex = 6;
			radioButtonType.Text = "Eqp.Type:";
			radioButtonType.UseVisualStyleBackColor = true;
			radioButtonType.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonCategory.AutoSize = true;
			radioButtonCategory.Location = new System.Drawing.Point(6, 74);
			radioButtonCategory.Name = "radioButtonCategory";
			radioButtonCategory.Size = new System.Drawing.Size(92, 17);
			radioButtonCategory.TabIndex = 9;
			radioButtonCategory.Text = "Eqp Category:";
			radioButtonCategory.UseVisualStyleBackColor = true;
			radioButtonCategory.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(295, 75);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(23, 13);
			label1.TabIndex = 11;
			label1.Text = "To:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(295, 52);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 6;
			label2.Text = "To:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(139, 30);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 6;
			label3.Text = "From:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(139, 53);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 6;
			label4.Text = "From:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(139, 76);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(33, 13);
			label5.TabIndex = 11;
			label5.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(123, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxSingleEquipment.Assigned = false;
			comboBoxSingleEquipment.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSingleEquipment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleEquipment.CustomReportFieldName = "";
			comboBoxSingleEquipment.CustomReportKey = "";
			comboBoxSingleEquipment.CustomReportValueType = 1;
			comboBoxSingleEquipment.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleEquipment.DisplayLayout.Appearance = appearance;
			comboBoxSingleEquipment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleEquipment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleEquipment.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleEquipment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxSingleEquipment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleEquipment.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxSingleEquipment.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleEquipment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleEquipment.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleEquipment.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxSingleEquipment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleEquipment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleEquipment.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleEquipment.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxSingleEquipment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleEquipment.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleEquipment.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxSingleEquipment.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxSingleEquipment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleEquipment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleEquipment.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxSingleEquipment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleEquipment.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxSingleEquipment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleEquipment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleEquipment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleEquipment.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleEquipment.Editable = true;
			comboBoxSingleEquipment.Enabled = false;
			comboBoxSingleEquipment.FilterString = "";
			comboBoxSingleEquipment.HasAllAccount = false;
			comboBoxSingleEquipment.HasCustom = false;
			comboBoxSingleEquipment.IsDataLoaded = false;
			comboBoxSingleEquipment.Location = new System.Drawing.Point(182, 3);
			comboBoxSingleEquipment.MaxDropDownItems = 12;
			comboBoxSingleEquipment.Name = "comboBoxSingleEquipment";
			comboBoxSingleEquipment.ShowInactiveItems = false;
			comboBoxSingleEquipment.ShowQuickAdd = true;
			comboBoxSingleEquipment.Size = new System.Drawing.Size(241, 20);
			comboBoxSingleEquipment.TabIndex = 12;
			comboBoxSingleEquipment.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToType.Assigned = false;
			comboBoxToType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToType.CustomReportFieldName = "";
			comboBoxToType.CustomReportKey = "";
			comboBoxToType.CustomReportValueType = 1;
			comboBoxToType.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToType.DisplayLayout.Appearance = appearance13;
			comboBoxToType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToType.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxToType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToType.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxToType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToType.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToType.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxToType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToType.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToType.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxToType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToType.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToType.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxToType.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxToType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxToType.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxToType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToType.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxToType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToType.Editable = true;
			comboBoxToType.Enabled = false;
			comboBoxToType.FilterString = "";
			comboBoxToType.HasAllAccount = false;
			comboBoxToType.HasCustom = false;
			comboBoxToType.IsDataLoaded = false;
			comboBoxToType.Location = new System.Drawing.Point(320, 48);
			comboBoxToType.MaxDropDownItems = 12;
			comboBoxToType.Name = "comboBoxToType";
			comboBoxToType.ShowInactiveItems = false;
			comboBoxToType.ShowQuickAdd = true;
			comboBoxToType.Size = new System.Drawing.Size(103, 20);
			comboBoxToType.TabIndex = 15;
			comboBoxToType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromType.Assigned = false;
			comboBoxFromType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromType.CustomReportFieldName = "";
			comboBoxFromType.CustomReportKey = "";
			comboBoxFromType.CustomReportValueType = 1;
			comboBoxFromType.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromType.DisplayLayout.Appearance = appearance25;
			comboBoxFromType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromType.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxFromType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromType.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxFromType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromType.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromType.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxFromType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromType.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromType.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxFromType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromType.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromType.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxFromType.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxFromType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromType.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxFromType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromType.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxFromType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromType.Editable = true;
			comboBoxFromType.Enabled = false;
			comboBoxFromType.FilterString = "";
			comboBoxFromType.HasAllAccount = false;
			comboBoxFromType.HasCustom = false;
			comboBoxFromType.IsDataLoaded = false;
			comboBoxFromType.Location = new System.Drawing.Point(182, 48);
			comboBoxFromType.MaxDropDownItems = 12;
			comboBoxFromType.Name = "comboBoxFromType";
			comboBoxFromType.ShowInactiveItems = false;
			comboBoxFromType.ShowQuickAdd = true;
			comboBoxFromType.Size = new System.Drawing.Size(112, 20);
			comboBoxFromType.TabIndex = 16;
			comboBoxFromType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromCategory.Assigned = false;
			comboBoxFromCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromCategory.CustomReportFieldName = "";
			comboBoxFromCategory.CustomReportKey = "";
			comboBoxFromCategory.CustomReportValueType = 1;
			comboBoxFromCategory.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromCategory.DisplayLayout.Appearance = appearance37;
			comboBoxFromCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCategory.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxFromCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxFromCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromCategory.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromCategory.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxFromCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromCategory.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromCategory.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxFromCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromCategory.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCategory.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxFromCategory.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxFromCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromCategory.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxFromCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxFromCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromCategory.Editable = true;
			comboBoxFromCategory.Enabled = false;
			comboBoxFromCategory.FilterString = "";
			comboBoxFromCategory.HasAllAccount = false;
			comboBoxFromCategory.HasCustom = false;
			comboBoxFromCategory.IsDataLoaded = false;
			comboBoxFromCategory.Location = new System.Drawing.Point(182, 71);
			comboBoxFromCategory.MaxDropDownItems = 12;
			comboBoxFromCategory.Name = "comboBoxFromCategory";
			comboBoxFromCategory.ShowAll = false;
			comboBoxFromCategory.ShowConsignIn = false;
			comboBoxFromCategory.ShowConsignOut = false;
			comboBoxFromCategory.ShowInactiveItems = false;
			comboBoxFromCategory.ShowNormalLocations = true;
			comboBoxFromCategory.ShowPOSOnly = false;
			comboBoxFromCategory.ShowQuickAdd = true;
			comboBoxFromCategory.ShowWarehouseOnly = false;
			comboBoxFromCategory.Size = new System.Drawing.Size(112, 20);
			comboBoxFromCategory.TabIndex = 17;
			comboBoxFromCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToCategory.Assigned = false;
			comboBoxToCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToCategory.CustomReportFieldName = "";
			comboBoxToCategory.CustomReportKey = "";
			comboBoxToCategory.CustomReportValueType = 1;
			comboBoxToCategory.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToCategory.DisplayLayout.Appearance = appearance49;
			comboBoxToCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCategory.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxToCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxToCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToCategory.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToCategory.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxToCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToCategory.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToCategory.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxToCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToCategory.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCategory.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxToCategory.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxToCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxToCategory.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxToCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxToCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToCategory.Editable = true;
			comboBoxToCategory.Enabled = false;
			comboBoxToCategory.FilterString = "";
			comboBoxToCategory.HasAllAccount = false;
			comboBoxToCategory.HasCustom = false;
			comboBoxToCategory.IsDataLoaded = false;
			comboBoxToCategory.Location = new System.Drawing.Point(320, 71);
			comboBoxToCategory.MaxDropDownItems = 12;
			comboBoxToCategory.Name = "comboBoxToCategory";
			comboBoxToCategory.ShowAll = false;
			comboBoxToCategory.ShowConsignIn = false;
			comboBoxToCategory.ShowConsignOut = false;
			comboBoxToCategory.ShowInactiveItems = false;
			comboBoxToCategory.ShowNormalLocations = true;
			comboBoxToCategory.ShowPOSOnly = false;
			comboBoxToCategory.ShowQuickAdd = true;
			comboBoxToCategory.ShowWarehouseOnly = false;
			comboBoxToCategory.Size = new System.Drawing.Size(103, 20);
			comboBoxToCategory.TabIndex = 18;
			comboBoxToCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromEquipment.Assigned = false;
			comboBoxFromEquipment.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromEquipment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromEquipment.CustomReportFieldName = "";
			comboBoxFromEquipment.CustomReportKey = "";
			comboBoxFromEquipment.CustomReportValueType = 1;
			comboBoxFromEquipment.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromEquipment.DisplayLayout.Appearance = appearance61;
			comboBoxFromEquipment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromEquipment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromEquipment.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromEquipment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxFromEquipment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromEquipment.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxFromEquipment.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromEquipment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromEquipment.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromEquipment.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxFromEquipment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromEquipment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromEquipment.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromEquipment.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxFromEquipment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromEquipment.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromEquipment.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxFromEquipment.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxFromEquipment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromEquipment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromEquipment.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxFromEquipment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromEquipment.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxFromEquipment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromEquipment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromEquipment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromEquipment.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromEquipment.Editable = true;
			comboBoxFromEquipment.Enabled = false;
			comboBoxFromEquipment.FilterString = "";
			comboBoxFromEquipment.HasAllAccount = false;
			comboBoxFromEquipment.HasCustom = false;
			comboBoxFromEquipment.IsDataLoaded = false;
			comboBoxFromEquipment.Location = new System.Drawing.Point(182, 25);
			comboBoxFromEquipment.MaxDropDownItems = 12;
			comboBoxFromEquipment.Name = "comboBoxFromEquipment";
			comboBoxFromEquipment.ShowInactiveItems = false;
			comboBoxFromEquipment.ShowQuickAdd = true;
			comboBoxFromEquipment.Size = new System.Drawing.Size(112, 20);
			comboBoxFromEquipment.TabIndex = 19;
			comboBoxFromEquipment.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToEquipment.Assigned = false;
			comboBoxToEquipment.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToEquipment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToEquipment.CustomReportFieldName = "";
			comboBoxToEquipment.CustomReportKey = "";
			comboBoxToEquipment.CustomReportValueType = 1;
			comboBoxToEquipment.DescriptionTextBox = null;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToEquipment.DisplayLayout.Appearance = appearance73;
			comboBoxToEquipment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToEquipment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToEquipment.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToEquipment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxToEquipment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToEquipment.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxToEquipment.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToEquipment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToEquipment.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToEquipment.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxToEquipment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToEquipment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToEquipment.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToEquipment.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxToEquipment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToEquipment.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToEquipment.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxToEquipment.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxToEquipment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToEquipment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxToEquipment.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxToEquipment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToEquipment.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxToEquipment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToEquipment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToEquipment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToEquipment.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToEquipment.Editable = true;
			comboBoxToEquipment.Enabled = false;
			comboBoxToEquipment.FilterString = "";
			comboBoxToEquipment.HasAllAccount = false;
			comboBoxToEquipment.HasCustom = false;
			comboBoxToEquipment.IsDataLoaded = false;
			comboBoxToEquipment.Location = new System.Drawing.Point(320, 25);
			comboBoxToEquipment.MaxDropDownItems = 12;
			comboBoxToEquipment.Name = "comboBoxToEquipment";
			comboBoxToEquipment.ShowInactiveItems = false;
			comboBoxToEquipment.ShowQuickAdd = true;
			comboBoxToEquipment.Size = new System.Drawing.Size(103, 20);
			comboBoxToEquipment.TabIndex = 20;
			comboBoxToEquipment.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToEquipment);
			base.Controls.Add(comboBoxFromEquipment);
			base.Controls.Add(comboBoxToCategory);
			base.Controls.Add(comboBoxFromCategory);
			base.Controls.Add(comboBoxFromType);
			base.Controls.Add(comboBoxToType);
			base.Controls.Add(comboBoxSingleEquipment);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(label5);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonCategory);
			base.Controls.Add(radioButtonType);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(label4);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(labelTo);
			base.Name = "EquipmentSelector";
			base.Size = new System.Drawing.Size(428, 96);
			base.Load += new System.EventHandler(EquipmentSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxSingleEquipment).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromEquipment).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToEquipment).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
