using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class FixedAssetSelector : UserControl, ICustomReportControl
	{
		private bool showGroupAndClass = true;

		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private RadioButton radioButtonClass;

		private RadioButton radioButtonGroup;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private RadioButton radioButtonSingle;

		private RadioButton radioButtonLocation;

		private Label label6;

		private Label label7;

		private FixedAssetsComboBox comboBoxSingleFixedAsset;

		private FixedAssetClassComboBox comboBoxFromClass;

		private FixedAssetClassComboBox comboBoxToClass;

		private FixedAssetsComboBox comboBoxFromAsset;

		private FixedAssetsComboBox comboBoxToAsset;

		private FixedAssetGroupComboBox comboBoxFromGroup;

		private FixedAssetGroupComboBox comboBoxToGroup;

		private FixedAssetLocationComboBox comboBoxFromLocation;

		private FixedAssetLocationComboBox comboBoxToLocation;

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
					RadioButton radioButton = radioButtonClass;
					RadioButton radioButton2 = radioButtonGroup;
					FixedAssetClassComboBox fixedAssetClassComboBox = comboBoxFromClass;
					FixedAssetClassComboBox fixedAssetClassComboBox2 = comboBoxToClass;
					FixedAssetGroupComboBox fixedAssetGroupComboBox = comboBoxFromGroup;
					bool flag2 = comboBoxToGroup.Visible = true;
					bool flag4 = fixedAssetGroupComboBox.Visible = flag2;
					bool flag6 = fixedAssetClassComboBox2.Visible = flag4;
					bool flag8 = fixedAssetClassComboBox.Visible = flag6;
					bool visible = radioButton2.Visible = flag8;
					radioButton.Visible = visible;
					base.Height = 99;
				}
				else
				{
					RadioButton radioButton3 = radioButtonClass;
					RadioButton radioButton4 = radioButtonGroup;
					FixedAssetClassComboBox fixedAssetClassComboBox3 = comboBoxFromClass;
					FixedAssetClassComboBox fixedAssetClassComboBox4 = comboBoxToClass;
					FixedAssetGroupComboBox fixedAssetGroupComboBox2 = comboBoxFromGroup;
					bool flag2 = comboBoxToGroup.Visible = false;
					bool flag4 = fixedAssetGroupComboBox2.Visible = flag2;
					bool flag6 = fixedAssetClassComboBox4.Visible = flag4;
					bool flag8 = fixedAssetClassComboBox3.Visible = flag6;
					bool visible = radioButton4.Visible = flag8;
					radioButton3.Visible = visible;
					base.Height = 48;
				}
			}
		}

		public bool IsSingleFixedAsset => radioButtonSingle.Checked;

		public string FromFixedAsset
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleFixedAsset.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromAsset.SelectedID;
				}
				return "";
			}
		}

		public string ToFixedAsset
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleFixedAsset.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToAsset.SelectedID;
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
					return comboBoxFromClass.SelectedID;
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
					return comboBoxToClass.SelectedID;
				}
				return "";
			}
		}

		public string FromGroup
		{
			get
			{
				if (radioButtonGroup.Checked)
				{
					return comboBoxFromGroup.SelectedID;
				}
				return "";
			}
		}

		public string ToGroup
		{
			get
			{
				if (radioButtonGroup.Checked)
				{
					return comboBoxToGroup.SelectedID;
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

		public FixedAssetSelector()
		{
			InitializeComponent();
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return "ANY(SELECT FixedAssetID FROM FixedAsset)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxSingleFixedAsset.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return "ANY(SELECT CUSTOMERID FROM FixedAsset WHERE FixedAssetID Between '" + comboBoxFromAsset.SelectedID + "' AND '" + comboBoxToAsset.SelectedID + "')";
				}
				if (radioButtonClass.Checked)
				{
					return "ANY(SELECT CUSTOMERID FROM FixedAsset WHERE FixedAssetClassID Between '" + comboBoxFromClass.SelectedID + "' AND '" + comboBoxToClass.SelectedID + "')";
				}
				if (radioButtonGroup.Checked)
				{
					return "ANY(SELECT CUSTOMERID FROM FixedAsset WHERE FixedAssetGroupID Between '" + comboBoxFromGroup.SelectedID + "' AND '" + comboBoxToGroup.SelectedID + "')";
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
				return crFieldName + " = '" + comboBoxSingleFixedAsset.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromAsset.SelectedID + "' AND '" + comboBoxToAsset.SelectedID + "')";
			}
			if (radioButtonClass.Checked)
			{
				return crFieldName + " = ANY(SELECT CUSTOMERID FROM FixedAsset WHERE FixedAssetClassID Between '" + comboBoxFromClass.SelectedID + "' AND '" + comboBoxToClass.SelectedID + "')";
			}
			if (radioButtonGroup.Checked)
			{
				return crFieldName + " = ANY(SELECT CUSTOMERID FROM FixedAsset WHERE FixedAssetGroupID Between '" + comboBoxFromGroup.SelectedID + "' AND '" + comboBoxToGroup.SelectedID + "')";
			}
			return "''=''";
		}

		private void FixedAssetSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleFixedAsset.Enabled = radioButtonSingle.Checked;
			FixedAssetsComboBox fixedAssetsComboBox = comboBoxFromAsset;
			bool enabled = comboBoxToAsset.Enabled = radioButtonRange.Checked;
			fixedAssetsComboBox.Enabled = enabled;
			FixedAssetGroupComboBox fixedAssetGroupComboBox = comboBoxFromGroup;
			enabled = (comboBoxToGroup.Enabled = radioButtonGroup.Checked);
			fixedAssetGroupComboBox.Enabled = enabled;
			FixedAssetClassComboBox fixedAssetClassComboBox = comboBoxFromClass;
			enabled = (comboBoxToClass.Enabled = radioButtonClass.Checked);
			fixedAssetClassComboBox.Enabled = enabled;
			FixedAssetLocationComboBox fixedAssetLocationComboBox = comboBoxFromLocation;
			enabled = (comboBoxToLocation.Enabled = radioButtonLocation.Checked);
			fixedAssetLocationComboBox.Enabled = enabled;
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
			Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			radioButtonClass = new System.Windows.Forms.RadioButton();
			radioButtonGroup = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			radioButtonLocation = new System.Windows.Forms.RadioButton();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			comboBoxToLocation = new Micromind.DataControls.FixedAssetLocationComboBox();
			comboBoxFromLocation = new Micromind.DataControls.FixedAssetLocationComboBox();
			comboBoxToGroup = new Micromind.DataControls.FixedAssetGroupComboBox();
			comboBoxFromGroup = new Micromind.DataControls.FixedAssetGroupComboBox();
			comboBoxToAsset = new Micromind.DataControls.FixedAssetsComboBox();
			comboBoxFromAsset = new Micromind.DataControls.FixedAssetsComboBox();
			comboBoxToClass = new Micromind.DataControls.FixedAssetClassComboBox();
			comboBoxFromClass = new Micromind.DataControls.FixedAssetClassComboBox();
			comboBoxSingleFixedAsset = new Micromind.DataControls.FixedAssetsComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToAsset).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromAsset).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleFixedAsset).BeginInit();
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
			radioButtonAll.Size = new System.Drawing.Size(65, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Asset";
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
			radioButtonClass.AutoSize = true;
			radioButtonClass.Location = new System.Drawing.Point(6, 51);
			radioButtonClass.Name = "radioButtonClass";
			radioButtonClass.Size = new System.Drawing.Size(82, 17);
			radioButtonClass.TabIndex = 6;
			radioButtonClass.Text = "Asset Class:";
			radioButtonClass.UseVisualStyleBackColor = true;
			radioButtonClass.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonGroup.AutoSize = true;
			radioButtonGroup.Location = new System.Drawing.Point(6, 74);
			radioButtonGroup.Name = "radioButtonGroup";
			radioButtonGroup.Size = new System.Drawing.Size(86, 17);
			radioButtonGroup.TabIndex = 9;
			radioButtonGroup.Text = "Asset Group:";
			radioButtonGroup.UseVisualStyleBackColor = true;
			radioButtonGroup.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
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
			radioButtonLocation.AutoSize = true;
			radioButtonLocation.Location = new System.Drawing.Point(7, 97);
			radioButtonLocation.Name = "radioButtonLocation";
			radioButtonLocation.Size = new System.Drawing.Size(98, 17);
			radioButtonLocation.TabIndex = 12;
			radioButtonLocation.Text = "Asset Location:";
			radioButtonLocation.UseVisualStyleBackColor = true;
			radioButtonLocation.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(139, 97);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(33, 13);
			label6.TabIndex = 13;
			label6.Text = "From:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(295, 97);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(23, 13);
			label7.TabIndex = 14;
			label7.Text = "To:";
			comboBoxToLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToLocation.CustomReportFieldName = "";
			comboBoxToLocation.CustomReportKey = "";
			comboBoxToLocation.CustomReportValueType = 1;
			comboBoxToLocation.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToLocation.DisplayLayout.Appearance = appearance;
			comboBoxToLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxToLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxToLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToLocation.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToLocation.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToLocation.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxToLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToLocation.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToLocation.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxToLocation.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxToLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxToLocation.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxToLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxToLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToLocation.Editable = true;
			comboBoxToLocation.FilterString = "";
			comboBoxToLocation.HasAllAccount = false;
			comboBoxToLocation.HasCustom = false;
			comboBoxToLocation.IsDataLoaded = false;
			comboBoxToLocation.Location = new System.Drawing.Point(318, 93);
			comboBoxToLocation.MaxDropDownItems = 12;
			comboBoxToLocation.Name = "comboBoxToLocation";
			comboBoxToLocation.ShowInactiveItems = true;
			comboBoxToLocation.ShowQuickAdd = true;
			comboBoxToLocation.Size = new System.Drawing.Size(105, 20);
			comboBoxToLocation.TabIndex = 23;
			comboBoxToLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromLocation.CustomReportFieldName = "";
			comboBoxFromLocation.CustomReportKey = "";
			comboBoxFromLocation.CustomReportValueType = 1;
			comboBoxFromLocation.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromLocation.DisplayLayout.Appearance = appearance13;
			comboBoxFromLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromLocation.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromLocation.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromLocation.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromLocation.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromLocation.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromLocation.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromLocation.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromLocation.Editable = true;
			comboBoxFromLocation.FilterString = "";
			comboBoxFromLocation.HasAllAccount = false;
			comboBoxFromLocation.HasCustom = false;
			comboBoxFromLocation.IsDataLoaded = false;
			comboBoxFromLocation.Location = new System.Drawing.Point(182, 92);
			comboBoxFromLocation.MaxDropDownItems = 12;
			comboBoxFromLocation.Name = "comboBoxFromLocation";
			comboBoxFromLocation.ShowInactiveItems = true;
			comboBoxFromLocation.ShowQuickAdd = true;
			comboBoxFromLocation.Size = new System.Drawing.Size(110, 20);
			comboBoxFromLocation.TabIndex = 22;
			comboBoxFromLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToGroup.CustomReportFieldName = "";
			comboBoxToGroup.CustomReportKey = "";
			comboBoxToGroup.CustomReportValueType = 1;
			comboBoxToGroup.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToGroup.DisplayLayout.Appearance = appearance25;
			comboBoxToGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxToGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxToGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToGroup.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToGroup.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxToGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToGroup.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxToGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToGroup.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxToGroup.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxToGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxToGroup.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxToGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxToGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToGroup.Editable = true;
			comboBoxToGroup.FilterString = "";
			comboBoxToGroup.HasAllAccount = false;
			comboBoxToGroup.HasCustom = false;
			comboBoxToGroup.IsDataLoaded = false;
			comboBoxToGroup.Location = new System.Drawing.Point(318, 70);
			comboBoxToGroup.MaxDropDownItems = 12;
			comboBoxToGroup.Name = "comboBoxToGroup";
			comboBoxToGroup.ShowInactiveItems = true;
			comboBoxToGroup.ShowQuickAdd = true;
			comboBoxToGroup.Size = new System.Drawing.Size(105, 20);
			comboBoxToGroup.TabIndex = 21;
			comboBoxToGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromGroup.CustomReportFieldName = "";
			comboBoxFromGroup.CustomReportKey = "";
			comboBoxFromGroup.CustomReportValueType = 1;
			comboBoxFromGroup.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromGroup.DisplayLayout.Appearance = appearance37;
			comboBoxFromGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxFromGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxFromGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromGroup.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromGroup.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxFromGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromGroup.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxFromGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromGroup.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxFromGroup.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxFromGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromGroup.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxFromGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxFromGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromGroup.Editable = true;
			comboBoxFromGroup.FilterString = "";
			comboBoxFromGroup.HasAllAccount = false;
			comboBoxFromGroup.HasCustom = false;
			comboBoxFromGroup.IsDataLoaded = false;
			comboBoxFromGroup.Location = new System.Drawing.Point(182, 70);
			comboBoxFromGroup.MaxDropDownItems = 12;
			comboBoxFromGroup.Name = "comboBoxFromGroup";
			comboBoxFromGroup.ShowInactiveItems = true;
			comboBoxFromGroup.ShowQuickAdd = true;
			comboBoxFromGroup.Size = new System.Drawing.Size(110, 20);
			comboBoxFromGroup.TabIndex = 20;
			comboBoxFromGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToAsset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToAsset.CustomReportFieldName = "";
			comboBoxToAsset.CustomReportKey = "";
			comboBoxToAsset.CustomReportValueType = 1;
			comboBoxToAsset.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToAsset.DisplayLayout.Appearance = appearance49;
			comboBoxToAsset.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToAsset.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToAsset.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToAsset.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxToAsset.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToAsset.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxToAsset.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToAsset.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToAsset.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToAsset.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxToAsset.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToAsset.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToAsset.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToAsset.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxToAsset.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToAsset.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToAsset.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxToAsset.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxToAsset.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToAsset.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxToAsset.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxToAsset.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToAsset.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxToAsset.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToAsset.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToAsset.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToAsset.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToAsset.Editable = true;
			comboBoxToAsset.FilterString = "";
			comboBoxToAsset.HasAllAccount = false;
			comboBoxToAsset.HasCustom = false;
			comboBoxToAsset.IsDataLoaded = false;
			comboBoxToAsset.Location = new System.Drawing.Point(320, 23);
			comboBoxToAsset.MaxDropDownItems = 12;
			comboBoxToAsset.Name = "comboBoxToAsset";
			comboBoxToAsset.ShowInactiveItems = false;
			comboBoxToAsset.ShowQuickAdd = true;
			comboBoxToAsset.Size = new System.Drawing.Size(103, 20);
			comboBoxToAsset.TabIndex = 19;
			comboBoxToAsset.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromAsset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromAsset.CustomReportFieldName = "";
			comboBoxFromAsset.CustomReportKey = "";
			comboBoxFromAsset.CustomReportValueType = 1;
			comboBoxFromAsset.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromAsset.DisplayLayout.Appearance = appearance61;
			comboBoxFromAsset.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromAsset.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromAsset.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromAsset.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxFromAsset.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromAsset.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxFromAsset.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromAsset.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromAsset.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromAsset.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxFromAsset.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromAsset.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromAsset.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromAsset.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxFromAsset.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromAsset.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromAsset.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxFromAsset.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxFromAsset.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromAsset.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromAsset.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxFromAsset.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromAsset.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxFromAsset.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromAsset.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromAsset.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromAsset.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromAsset.Editable = true;
			comboBoxFromAsset.FilterString = "";
			comboBoxFromAsset.HasAllAccount = false;
			comboBoxFromAsset.HasCustom = false;
			comboBoxFromAsset.IsDataLoaded = false;
			comboBoxFromAsset.Location = new System.Drawing.Point(182, 23);
			comboBoxFromAsset.MaxDropDownItems = 12;
			comboBoxFromAsset.Name = "comboBoxFromAsset";
			comboBoxFromAsset.ShowInactiveItems = false;
			comboBoxFromAsset.ShowQuickAdd = true;
			comboBoxFromAsset.Size = new System.Drawing.Size(107, 20);
			comboBoxFromAsset.TabIndex = 18;
			comboBoxFromAsset.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToClass.CustomReportFieldName = "";
			comboBoxToClass.CustomReportKey = "";
			comboBoxToClass.CustomReportValueType = 1;
			comboBoxToClass.DescriptionTextBox = null;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToClass.DisplayLayout.Appearance = appearance73;
			comboBoxToClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxToClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxToClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToClass.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToClass.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxToClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToClass.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxToClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToClass.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxToClass.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxToClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxToClass.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxToClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxToClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToClass.Editable = true;
			comboBoxToClass.FilterString = "";
			comboBoxToClass.HasAllAccount = false;
			comboBoxToClass.HasCustom = false;
			comboBoxToClass.IsDataLoaded = false;
			comboBoxToClass.Location = new System.Drawing.Point(320, 46);
			comboBoxToClass.MaxDropDownItems = 12;
			comboBoxToClass.Name = "comboBoxToClass";
			comboBoxToClass.ShowInactiveItems = true;
			comboBoxToClass.ShowQuickAdd = true;
			comboBoxToClass.Size = new System.Drawing.Size(103, 20);
			comboBoxToClass.TabIndex = 17;
			comboBoxToClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromClass.CustomReportFieldName = "";
			comboBoxFromClass.CustomReportKey = "";
			comboBoxFromClass.CustomReportValueType = 1;
			comboBoxFromClass.DescriptionTextBox = null;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromClass.DisplayLayout.Appearance = appearance85;
			comboBoxFromClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance86.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.GroupByBox.Appearance = appearance86;
			appearance87.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance87;
			comboBoxFromClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance88.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance88.BackColor2 = System.Drawing.SystemColors.Control;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.PromptAppearance = appearance88;
			comboBoxFromClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromClass.DisplayLayout.Override.ActiveCellAppearance = appearance89;
			appearance90.BackColor = System.Drawing.SystemColors.Highlight;
			appearance90.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromClass.DisplayLayout.Override.ActiveRowAppearance = appearance90;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.CardAreaAppearance = appearance91;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			appearance92.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromClass.DisplayLayout.Override.CellAppearance = appearance92;
			comboBoxFromClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromClass.DisplayLayout.Override.CellPadding = 0;
			appearance93.BackColor = System.Drawing.SystemColors.Control;
			appearance93.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance93.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance93.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.GroupByRowAppearance = appearance93;
			appearance94.TextHAlignAsString = "Left";
			comboBoxFromClass.DisplayLayout.Override.HeaderAppearance = appearance94;
			comboBoxFromClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromClass.DisplayLayout.Override.RowAppearance = appearance95;
			comboBoxFromClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance96;
			comboBoxFromClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromClass.Editable = true;
			comboBoxFromClass.FilterString = "";
			comboBoxFromClass.HasAllAccount = false;
			comboBoxFromClass.HasCustom = false;
			comboBoxFromClass.IsDataLoaded = false;
			comboBoxFromClass.Location = new System.Drawing.Point(182, 46);
			comboBoxFromClass.MaxDropDownItems = 12;
			comboBoxFromClass.Name = "comboBoxFromClass";
			comboBoxFromClass.ShowInactiveItems = true;
			comboBoxFromClass.ShowQuickAdd = true;
			comboBoxFromClass.Size = new System.Drawing.Size(110, 20);
			comboBoxFromClass.TabIndex = 16;
			comboBoxFromClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSingleFixedAsset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleFixedAsset.CustomReportFieldName = "";
			comboBoxSingleFixedAsset.CustomReportKey = "";
			comboBoxSingleFixedAsset.CustomReportValueType = 1;
			comboBoxSingleFixedAsset.DescriptionTextBox = null;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleFixedAsset.DisplayLayout.Appearance = appearance97;
			comboBoxSingleFixedAsset.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleFixedAsset.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance98.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance98.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleFixedAsset.DisplayLayout.GroupByBox.Appearance = appearance98;
			appearance99.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleFixedAsset.DisplayLayout.GroupByBox.BandLabelAppearance = appearance99;
			comboBoxSingleFixedAsset.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance100.BackColor2 = System.Drawing.SystemColors.Control;
			appearance100.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance100.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleFixedAsset.DisplayLayout.GroupByBox.PromptAppearance = appearance100;
			comboBoxSingleFixedAsset.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleFixedAsset.DisplayLayout.MaxRowScrollRegions = 1;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleFixedAsset.DisplayLayout.Override.ActiveCellAppearance = appearance101;
			appearance102.BackColor = System.Drawing.SystemColors.Highlight;
			appearance102.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleFixedAsset.DisplayLayout.Override.ActiveRowAppearance = appearance102;
			comboBoxSingleFixedAsset.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleFixedAsset.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleFixedAsset.DisplayLayout.Override.CardAreaAppearance = appearance103;
			appearance104.BorderColor = System.Drawing.Color.Silver;
			appearance104.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleFixedAsset.DisplayLayout.Override.CellAppearance = appearance104;
			comboBoxSingleFixedAsset.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleFixedAsset.DisplayLayout.Override.CellPadding = 0;
			appearance105.BackColor = System.Drawing.SystemColors.Control;
			appearance105.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance105.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance105.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance105.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleFixedAsset.DisplayLayout.Override.GroupByRowAppearance = appearance105;
			appearance106.TextHAlignAsString = "Left";
			comboBoxSingleFixedAsset.DisplayLayout.Override.HeaderAppearance = appearance106;
			comboBoxSingleFixedAsset.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleFixedAsset.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleFixedAsset.DisplayLayout.Override.RowAppearance = appearance107;
			comboBoxSingleFixedAsset.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleFixedAsset.DisplayLayout.Override.TemplateAddRowAppearance = appearance108;
			comboBoxSingleFixedAsset.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleFixedAsset.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleFixedAsset.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleFixedAsset.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleFixedAsset.Editable = true;
			comboBoxSingleFixedAsset.FilterString = "";
			comboBoxSingleFixedAsset.HasAllAccount = false;
			comboBoxSingleFixedAsset.HasCustom = false;
			comboBoxSingleFixedAsset.IsDataLoaded = false;
			comboBoxSingleFixedAsset.Location = new System.Drawing.Point(182, 1);
			comboBoxSingleFixedAsset.MaxDropDownItems = 12;
			comboBoxSingleFixedAsset.Name = "comboBoxSingleFixedAsset";
			comboBoxSingleFixedAsset.ShowInactiveItems = false;
			comboBoxSingleFixedAsset.ShowQuickAdd = true;
			comboBoxSingleFixedAsset.Size = new System.Drawing.Size(241, 20);
			comboBoxSingleFixedAsset.TabIndex = 15;
			comboBoxSingleFixedAsset.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToLocation);
			base.Controls.Add(comboBoxFromLocation);
			base.Controls.Add(comboBoxToGroup);
			base.Controls.Add(comboBoxFromGroup);
			base.Controls.Add(comboBoxToAsset);
			base.Controls.Add(comboBoxFromAsset);
			base.Controls.Add(comboBoxToClass);
			base.Controls.Add(comboBoxFromClass);
			base.Controls.Add(comboBoxSingleFixedAsset);
			base.Controls.Add(label7);
			base.Controls.Add(label6);
			base.Controls.Add(radioButtonLocation);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(label5);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonGroup);
			base.Controls.Add(radioButtonClass);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(label4);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(labelTo);
			base.Name = "FixedAssetSelector";
			base.Size = new System.Drawing.Size(428, 118);
			base.Load += new System.EventHandler(FixedAssetSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxToLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToAsset).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromAsset).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleFixedAsset).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
