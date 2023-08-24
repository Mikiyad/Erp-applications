using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class HorseSelector : UserControl, ICustomReportControl
	{
		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private RadioButton radioButtonDepartment;

		private RadioButton radioButtonLocation;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private RadioButton radioButtonSingle;

		private LocationComboBox comboBoxFromLocation;

		private LocationComboBox comboBoxToLocation;

		private HorseComboBox comboBoxSingleHorse;

		private HorseComboBox comboBoxFromHorse;

		private HorseComboBox comboBoxToHorse;

		private RiderComboBox comboBoxFromRider;

		private RiderComboBox comboBoxToRider;

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

		public string FromHorse
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleHorse.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromHorse.SelectedID;
				}
				return "";
			}
		}

		public string ToHorse
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleHorse.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToHorse.SelectedID;
				}
				return "";
			}
		}

		public string FromLocation
		{
			get
			{
				if (radioButtonLocation.Checked)
				{
					return comboBoxFromLocation.SelectedID;
				}
				return "";
			}
		}

		public string ToLocation
		{
			get
			{
				if (radioButtonLocation.Checked)
				{
					return comboBoxToLocation.SelectedID;
				}
				return "";
			}
		}

		public string FromTrainer
		{
			get
			{
				if (radioButtonDepartment.Checked)
				{
					return comboBoxFromRider.SelectedID;
				}
				return "";
			}
		}

		public string ToTrainer
		{
			get
			{
				if (radioButtonDepartment.Checked)
				{
					return comboBoxToRider.SelectedID;
				}
				return "";
			}
		}

		public HorseSelector()
		{
			InitializeComponent();
		}

		private void HorseSelector_Load(object sender, EventArgs e)
		{
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return "ANY(SELECT HorseCode FROM Horse_Summary)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxSingleHorse.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return "ANY(SELECT HorseCode FROM Horse_Summary WHERE HorseID Between '" + comboBoxFromHorse.SelectedID + "' AND '" + comboBoxToHorse.SelectedID + "')";
				}
				if (radioButtonDepartment.Checked)
				{
					return "ANY(SELECT HorseCode FROM Horse_Summary WHERE RiderID Between '" + comboBoxFromRider.SelectedID + "' AND '" + comboBoxToRider.SelectedID + "')";
				}
				if (radioButtonLocation.Checked)
				{
					return "ANY(SELECT HorseCode FROM Horse_Summary WHERE LocationID Between '" + comboBoxFromLocation.SelectedID + "' AND '" + comboBoxToLocation.SelectedID + "')";
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
				return crFieldName + " = '" + comboBoxSingleHorse.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromHorse.SelectedID + "' AND '" + comboBoxToHorse.SelectedID + "')";
			}
			if (radioButtonDepartment.Checked)
			{
				return crFieldName + " = ANY(SELECT HorseCode FROM Horse_Summary WHERE RiderID Between '" + comboBoxFromRider.SelectedID + "' AND '" + comboBoxToRider.SelectedID + "')";
			}
			if (radioButtonLocation.Checked)
			{
				return crFieldName + " = ANY(SELECT HorseCode FROM Horse_Summary WHERE LocationID Between '" + comboBoxFromLocation.SelectedID + "' AND '" + comboBoxToLocation.SelectedID + "')";
			}
			return "''=''";
		}

		private void EnableDisableControls()
		{
			comboBoxSingleHorse.Enabled = radioButtonSingle.Checked;
			HorseComboBox horseComboBox = comboBoxFromHorse;
			bool enabled = comboBoxToHorse.Enabled = radioButtonRange.Checked;
			horseComboBox.Enabled = enabled;
			RiderComboBox riderComboBox = comboBoxFromRider;
			enabled = (comboBoxToRider.Enabled = radioButtonDepartment.Checked);
			riderComboBox.Enabled = enabled;
			LocationComboBox locationComboBox = comboBoxFromLocation;
			enabled = (comboBoxToLocation.Enabled = radioButtonLocation.Checked);
			locationComboBox.Enabled = enabled;
		}

		private void radioButtons_CheckedChanged(object sender, EventArgs e)
		{
			EnableDisableControls();
		}

		private void horseComboBox1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
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
			radioButtonDepartment = new System.Windows.Forms.RadioButton();
			radioButtonLocation = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxToRider = new Micromind.DataControls.RiderComboBox();
			comboBoxFromRider = new Micromind.DataControls.RiderComboBox();
			comboBoxToHorse = new Micromind.DataControls.HorseComboBox();
			comboBoxFromHorse = new Micromind.DataControls.HorseComboBox();
			comboBoxSingleHorse = new Micromind.DataControls.HorseComboBox();
			comboBoxToLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxFromLocation = new Micromind.DataControls.LocationComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxToRider).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromRider).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToHorse).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromHorse).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleHorse).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLocation).BeginInit();
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
			radioButtonAll.Size = new System.Drawing.Size(72, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Horses";
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
			radioButtonDepartment.AutoSize = true;
			radioButtonDepartment.Location = new System.Drawing.Point(6, 50);
			radioButtonDepartment.Name = "radioButtonDepartment";
			radioButtonDepartment.Size = new System.Drawing.Size(61, 17);
			radioButtonDepartment.TabIndex = 7;
			radioButtonDepartment.Text = "Trainer:";
			radioButtonDepartment.UseVisualStyleBackColor = true;
			radioButtonDepartment.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonLocation.AutoSize = true;
			radioButtonLocation.Location = new System.Drawing.Point(6, 74);
			radioButtonLocation.Name = "radioButtonLocation";
			radioButtonLocation.Size = new System.Drawing.Size(69, 17);
			radioButtonLocation.TabIndex = 10;
			radioButtonLocation.Text = "Location:";
			radioButtonLocation.UseVisualStyleBackColor = true;
			radioButtonLocation.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(265, 75);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(23, 13);
			label1.TabIndex = 11;
			label1.Text = "To:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(265, 52);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 6;
			label2.Text = "To:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(116, 29);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 4;
			label3.Text = "From:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(116, 52);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 6;
			label4.Text = "From:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(116, 75);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(33, 13);
			label5.TabIndex = 11;
			label5.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(100, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxToRider.Assigned = false;
			comboBoxToRider.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToRider.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToRider.CustomReportFieldName = "";
			comboBoxToRider.CustomReportKey = "";
			comboBoxToRider.CustomReportValueType = 1;
			comboBoxToRider.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToRider.DisplayLayout.Appearance = appearance;
			comboBoxToRider.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToRider.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToRider.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToRider.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxToRider.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToRider.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxToRider.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToRider.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToRider.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToRider.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxToRider.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToRider.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToRider.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToRider.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxToRider.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToRider.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToRider.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxToRider.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxToRider.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToRider.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxToRider.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxToRider.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToRider.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxToRider.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToRider.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToRider.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToRider.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToRider.Editable = true;
			comboBoxToRider.FilterString = "";
			comboBoxToRider.HasAllAccount = false;
			comboBoxToRider.HasCustom = false;
			comboBoxToRider.IsDataLoaded = false;
			comboBoxToRider.Location = new System.Drawing.Point(294, 48);
			comboBoxToRider.MaxDropDownItems = 12;
			comboBoxToRider.Name = "comboBoxToRider";
			comboBoxToRider.ShowInactiveItems = false;
			comboBoxToRider.ShowQuickAdd = true;
			comboBoxToRider.Size = new System.Drawing.Size(103, 20);
			comboBoxToRider.TabIndex = 17;
			comboBoxToRider.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromRider.Assigned = false;
			comboBoxFromRider.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromRider.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromRider.CustomReportFieldName = "";
			comboBoxFromRider.CustomReportKey = "";
			comboBoxFromRider.CustomReportValueType = 1;
			comboBoxFromRider.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromRider.DisplayLayout.Appearance = appearance13;
			comboBoxFromRider.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromRider.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromRider.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromRider.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromRider.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromRider.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromRider.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromRider.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromRider.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromRider.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromRider.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromRider.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromRider.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromRider.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromRider.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromRider.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromRider.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromRider.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromRider.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromRider.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromRider.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromRider.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromRider.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromRider.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromRider.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromRider.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromRider.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromRider.Editable = true;
			comboBoxFromRider.FilterString = "";
			comboBoxFromRider.HasAllAccount = false;
			comboBoxFromRider.HasCustom = false;
			comboBoxFromRider.IsDataLoaded = false;
			comboBoxFromRider.Location = new System.Drawing.Point(161, 48);
			comboBoxFromRider.MaxDropDownItems = 12;
			comboBoxFromRider.Name = "comboBoxFromRider";
			comboBoxFromRider.ShowInactiveItems = false;
			comboBoxFromRider.ShowQuickAdd = true;
			comboBoxFromRider.Size = new System.Drawing.Size(103, 20);
			comboBoxFromRider.TabIndex = 16;
			comboBoxFromRider.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToHorse.Assigned = false;
			comboBoxToHorse.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToHorse.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToHorse.CustomReportFieldName = "";
			comboBoxToHorse.CustomReportKey = "";
			comboBoxToHorse.CustomReportValueType = 1;
			comboBoxToHorse.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToHorse.DisplayLayout.Appearance = appearance25;
			comboBoxToHorse.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToHorse.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToHorse.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToHorse.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToHorse.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToHorse.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToHorse.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToHorse.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToHorse.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToHorse.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToHorse.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToHorse.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToHorse.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToHorse.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToHorse.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToHorse.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToHorse.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToHorse.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToHorse.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToHorse.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToHorse.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToHorse.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToHorse.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToHorse.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToHorse.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToHorse.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToHorse.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToHorse.Editable = true;
			comboBoxToHorse.FilterString = "";
			comboBoxToHorse.HasAllAccount = false;
			comboBoxToHorse.HasCustom = false;
			comboBoxToHorse.IsDataLoaded = false;
			comboBoxToHorse.Location = new System.Drawing.Point(294, 25);
			comboBoxToHorse.MaxDropDownItems = 12;
			comboBoxToHorse.Name = "comboBoxToHorse";
			comboBoxToHorse.ShowInactiveItems = false;
			comboBoxToHorse.ShowQuickAdd = true;
			comboBoxToHorse.Size = new System.Drawing.Size(103, 20);
			comboBoxToHorse.TabIndex = 15;
			comboBoxToHorse.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromHorse.Assigned = false;
			comboBoxFromHorse.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromHorse.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromHorse.CustomReportFieldName = "";
			comboBoxFromHorse.CustomReportKey = "";
			comboBoxFromHorse.CustomReportValueType = 1;
			comboBoxFromHorse.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromHorse.DisplayLayout.Appearance = appearance37;
			comboBoxFromHorse.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromHorse.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromHorse.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromHorse.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxFromHorse.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromHorse.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxFromHorse.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromHorse.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromHorse.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromHorse.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxFromHorse.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromHorse.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromHorse.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromHorse.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxFromHorse.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromHorse.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromHorse.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxFromHorse.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxFromHorse.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromHorse.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromHorse.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxFromHorse.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromHorse.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxFromHorse.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromHorse.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromHorse.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromHorse.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromHorse.Editable = true;
			comboBoxFromHorse.FilterString = "";
			comboBoxFromHorse.HasAllAccount = false;
			comboBoxFromHorse.HasCustom = false;
			comboBoxFromHorse.IsDataLoaded = false;
			comboBoxFromHorse.Location = new System.Drawing.Point(161, 25);
			comboBoxFromHorse.MaxDropDownItems = 12;
			comboBoxFromHorse.Name = "comboBoxFromHorse";
			comboBoxFromHorse.ShowInactiveItems = false;
			comboBoxFromHorse.ShowQuickAdd = true;
			comboBoxFromHorse.Size = new System.Drawing.Size(102, 20);
			comboBoxFromHorse.TabIndex = 14;
			comboBoxFromHorse.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSingleHorse.Assigned = false;
			comboBoxSingleHorse.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSingleHorse.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleHorse.CustomReportFieldName = "";
			comboBoxSingleHorse.CustomReportKey = "";
			comboBoxSingleHorse.CustomReportValueType = 1;
			comboBoxSingleHorse.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleHorse.DisplayLayout.Appearance = appearance49;
			comboBoxSingleHorse.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleHorse.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleHorse.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleHorse.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxSingleHorse.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleHorse.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxSingleHorse.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleHorse.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleHorse.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleHorse.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxSingleHorse.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleHorse.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleHorse.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleHorse.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxSingleHorse.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleHorse.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleHorse.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxSingleHorse.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxSingleHorse.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleHorse.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleHorse.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxSingleHorse.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleHorse.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxSingleHorse.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleHorse.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleHorse.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleHorse.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleHorse.Editable = true;
			comboBoxSingleHorse.FilterString = "";
			comboBoxSingleHorse.HasAllAccount = false;
			comboBoxSingleHorse.HasCustom = false;
			comboBoxSingleHorse.IsDataLoaded = false;
			comboBoxSingleHorse.Location = new System.Drawing.Point(161, 2);
			comboBoxSingleHorse.MaxDropDownItems = 12;
			comboBoxSingleHorse.Name = "comboBoxSingleHorse";
			comboBoxSingleHorse.ShowInactiveItems = false;
			comboBoxSingleHorse.ShowQuickAdd = true;
			comboBoxSingleHorse.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleHorse.TabIndex = 13;
			comboBoxSingleHorse.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSingleHorse.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(horseComboBox1_InitializeLayout);
			comboBoxToLocation.Assigned = false;
			comboBoxToLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToLocation.CustomReportFieldName = "";
			comboBoxToLocation.CustomReportKey = "";
			comboBoxToLocation.CustomReportValueType = 1;
			comboBoxToLocation.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToLocation.DisplayLayout.Appearance = appearance61;
			comboBoxToLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxToLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxToLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToLocation.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToLocation.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToLocation.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxToLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToLocation.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxToLocation.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxToLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxToLocation.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxToLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxToLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToLocation.Editable = true;
			comboBoxToLocation.Enabled = false;
			comboBoxToLocation.FilterString = "";
			comboBoxToLocation.HasAllAccount = false;
			comboBoxToLocation.HasCustom = false;
			comboBoxToLocation.IsDataLoaded = false;
			comboBoxToLocation.Location = new System.Drawing.Point(294, 71);
			comboBoxToLocation.MaxDropDownItems = 12;
			comboBoxToLocation.Name = "comboBoxToLocation";
			comboBoxToLocation.ShowAll = false;
			comboBoxToLocation.ShowConsignIn = false;
			comboBoxToLocation.ShowConsignOut = false;
			comboBoxToLocation.ShowDefaultLocationOnly = false;
			comboBoxToLocation.ShowInactiveItems = false;
			comboBoxToLocation.ShowNormalLocations = true;
			comboBoxToLocation.ShowPOSOnly = false;
			comboBoxToLocation.ShowQuickAdd = true;
			comboBoxToLocation.ShowWarehouseOnly = false;
			comboBoxToLocation.Size = new System.Drawing.Size(103, 20);
			comboBoxToLocation.TabIndex = 12;
			comboBoxToLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromLocation.Assigned = false;
			comboBoxFromLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromLocation.CustomReportFieldName = "";
			comboBoxFromLocation.CustomReportKey = "";
			comboBoxFromLocation.CustomReportValueType = 1;
			comboBoxFromLocation.DescriptionTextBox = null;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromLocation.DisplayLayout.Appearance = appearance73;
			comboBoxFromLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxFromLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxFromLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromLocation.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromLocation.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxFromLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromLocation.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxFromLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromLocation.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxFromLocation.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxFromLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromLocation.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxFromLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxFromLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromLocation.Editable = true;
			comboBoxFromLocation.Enabled = false;
			comboBoxFromLocation.FilterString = "";
			comboBoxFromLocation.HasAllAccount = false;
			comboBoxFromLocation.HasCustom = false;
			comboBoxFromLocation.IsDataLoaded = false;
			comboBoxFromLocation.Location = new System.Drawing.Point(161, 71);
			comboBoxFromLocation.MaxDropDownItems = 12;
			comboBoxFromLocation.Name = "comboBoxFromLocation";
			comboBoxFromLocation.ShowAll = false;
			comboBoxFromLocation.ShowConsignIn = false;
			comboBoxFromLocation.ShowConsignOut = false;
			comboBoxFromLocation.ShowDefaultLocationOnly = false;
			comboBoxFromLocation.ShowInactiveItems = false;
			comboBoxFromLocation.ShowNormalLocations = true;
			comboBoxFromLocation.ShowPOSOnly = false;
			comboBoxFromLocation.ShowQuickAdd = true;
			comboBoxFromLocation.ShowWarehouseOnly = false;
			comboBoxFromLocation.Size = new System.Drawing.Size(103, 20);
			comboBoxFromLocation.TabIndex = 11;
			comboBoxFromLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToRider);
			base.Controls.Add(comboBoxFromRider);
			base.Controls.Add(comboBoxToHorse);
			base.Controls.Add(comboBoxFromHorse);
			base.Controls.Add(comboBoxSingleHorse);
			base.Controls.Add(comboBoxToLocation);
			base.Controls.Add(comboBoxFromLocation);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(label5);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonLocation);
			base.Controls.Add(radioButtonDepartment);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(label4);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(labelTo);
			base.Name = "HorseSelector";
			base.Size = new System.Drawing.Size(414, 99);
			base.Load += new System.EventHandler(HorseSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxToRider).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromRider).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToHorse).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromHorse).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleHorse).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLocation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
