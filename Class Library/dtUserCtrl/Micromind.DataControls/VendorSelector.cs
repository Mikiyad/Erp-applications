using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class VendorSelector : UserControl, ICustomReportControl
	{
		private bool showGroupAndClass = true;

		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private List<string> Vendorlist = new List<string>();

		private IContainer components;

		private vendorsFlatComboBox comboBoxFromVendor;

		private vendorsFlatComboBox comboBoxToVendor;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private RadioButton radioButtonClass;

		private RadioButton radioButtonGroup;

		private VendorClassComboBox comboBoxFromClass;

		private VendorClassComboBox comboBoxToClass;

		private Label label1;

		private VendorGroupComboBox comboBoxFromGroup;

		private VendorGroupComboBox comboBoxToGroup;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private RadioButton radioButtonSingle;

		private vendorsFlatComboBox comboBoxSingleVendor;

		private Button buttonMultiple;

		private RadioButton radioButtonMultipleVendor;

		private TextBox textBoxMultipleVendors;

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
					VendorClassComboBox vendorClassComboBox = comboBoxFromClass;
					VendorClassComboBox vendorClassComboBox2 = comboBoxToClass;
					VendorGroupComboBox vendorGroupComboBox = comboBoxFromGroup;
					bool flag2 = comboBoxToGroup.Visible = true;
					bool flag4 = vendorGroupComboBox.Visible = flag2;
					bool flag6 = vendorClassComboBox2.Visible = flag4;
					bool flag8 = vendorClassComboBox.Visible = flag6;
					bool visible = radioButton2.Visible = flag8;
					radioButton.Visible = visible;
					base.Height = 99;
				}
				else
				{
					RadioButton radioButton3 = radioButtonClass;
					RadioButton radioButton4 = radioButtonGroup;
					VendorClassComboBox vendorClassComboBox3 = comboBoxFromClass;
					VendorClassComboBox vendorClassComboBox4 = comboBoxToClass;
					VendorGroupComboBox vendorGroupComboBox2 = comboBoxFromGroup;
					bool flag2 = comboBoxToGroup.Visible = false;
					bool flag4 = vendorGroupComboBox2.Visible = flag2;
					bool flag6 = vendorClassComboBox4.Visible = flag4;
					bool flag8 = vendorClassComboBox3.Visible = flag6;
					bool visible = radioButton4.Visible = flag8;
					radioButton3.Visible = visible;
					base.Height = 48;
				}
			}
		}

		public string FromVendor
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleVendor.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromVendor.SelectedID;
				}
				return "";
			}
		}

		public string ToVendor
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleVendor.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToVendor.SelectedID;
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

		public string MultipleVendors
		{
			get
			{
				if (radioButtonMultipleVendor.Checked)
				{
					return "'" + string.Join("','", Vendorlist) + "'";
				}
				return "";
			}
		}

		public VendorSelector()
		{
			InitializeComponent();
		}

		private void VendorSelector_Load(object sender, EventArgs e)
		{
			buttonMultiple.Enabled = false;
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return " ANY(SELECT VendorID FROM Vendor)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxSingleVendor.SelectedID + "'";
				}
				if (radioButtonRange.Checked)
				{
					return " ANY(SELECT VendorID FROM Vendor WHERE VendorID Between '" + comboBoxFromVendor.SelectedID + "' AND '" + comboBoxToVendor.SelectedID + "')";
				}
				if (radioButtonClass.Checked)
				{
					return " ANY(SELECT VendorID FROM Vendor WHERE VendorClassID Between '" + comboBoxFromClass.SelectedID + "' AND '" + comboBoxToClass.SelectedID + "')";
				}
				if (radioButtonGroup.Checked)
				{
					return " ANY(SELECT VendorID FROM Vendor WHERE VendorGroupID Between '" + comboBoxFromGroup.SelectedID + "' AND '" + comboBoxToGroup.SelectedID + "')";
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
				return crFieldName + " = '" + comboBoxSingleVendor.SelectedID + "'";
			}
			if (radioButtonRange.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromVendor.SelectedID + "' AND '" + comboBoxToVendor.SelectedID + "')";
			}
			if (radioButtonClass.Checked)
			{
				return crFieldName + " = ANY(SELECT VendorID FROM Vendor WHERE VendorClassID Between '" + comboBoxFromClass.SelectedID + "' AND '" + comboBoxToClass.SelectedID + "')";
			}
			if (radioButtonGroup.Checked)
			{
				return crFieldName + " = ANY(SELECT VendorID FROM Vendor WHERE VendorGroupID Between '" + comboBoxFromGroup.SelectedID + "' AND '" + comboBoxToGroup.SelectedID + "')";
			}
			return "''=''";
		}

		private void EnableDisableControls()
		{
			comboBoxSingleVendor.Enabled = radioButtonSingle.Checked;
			vendorsFlatComboBox vendorsFlatComboBox = comboBoxFromVendor;
			bool enabled = comboBoxToVendor.Enabled = radioButtonRange.Checked;
			vendorsFlatComboBox.Enabled = enabled;
			VendorGroupComboBox vendorGroupComboBox = comboBoxFromGroup;
			enabled = (comboBoxToGroup.Enabled = radioButtonGroup.Checked);
			vendorGroupComboBox.Enabled = enabled;
			VendorClassComboBox vendorClassComboBox = comboBoxFromClass;
			enabled = (comboBoxToClass.Enabled = radioButtonClass.Checked);
			vendorClassComboBox.Enabled = enabled;
			buttonMultiple.Enabled = radioButtonMultipleVendor.Checked;
		}

		private void radioButtons_CheckedChanged(object sender, EventArgs e)
		{
			EnableDisableControls();
		}

		private void buttonMultiple_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			List<string> selectedDocuments = new List<string>();
			dataSet = Factory.VendorSystem.GetVendorSelectionList();
			SelectTransactionDialog selectTransactionDialog = new SelectTransactionDialog();
			selectTransactionDialog.DataSource = dataSet;
			selectTransactionDialog.IsMultiSelect = true;
			selectTransactionDialog.SelectedDocuments = selectedDocuments;
			selectTransactionDialog.Text = "Select Vendors";
			if (selectTransactionDialog.ShowDialog(this) == DialogResult.OK)
			{
				selectedDocuments = selectTransactionDialog.SelectedDocuments;
				foreach (UltraGridRow selectedRow in selectTransactionDialog.SelectedRows)
				{
					string item = selectedRow.Cells["Code"].Value.ToString();
					selectedRow.Cells["Name"].Value.ToString();
					Vendorlist.Add(item);
				}
				textBoxMultipleVendors.Text = string.Join(",", Vendorlist);
			}
		}

		private void radioButtonMultipleVendor_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonMultipleVendor.Checked)
			{
				buttonMultiple.Enabled = true;
				textBoxMultipleVendors.Clear();
				Vendorlist.Clear();
			}
			else
			{
				buttonMultiple.Enabled = false;
				textBoxMultipleVendors.Clear();
				Vendorlist.Clear();
			}
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
			comboBoxFromVendor = new Micromind.DataControls.vendorsFlatComboBox();
			comboBoxToVendor = new Micromind.DataControls.vendorsFlatComboBox();
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			radioButtonClass = new System.Windows.Forms.RadioButton();
			radioButtonGroup = new System.Windows.Forms.RadioButton();
			comboBoxFromClass = new Micromind.DataControls.VendorClassComboBox();
			comboBoxToClass = new Micromind.DataControls.VendorClassComboBox();
			label1 = new System.Windows.Forms.Label();
			comboBoxFromGroup = new Micromind.DataControls.VendorGroupComboBox();
			comboBoxToGroup = new Micromind.DataControls.VendorGroupComboBox();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxSingleVendor = new Micromind.DataControls.vendorsFlatComboBox();
			buttonMultiple = new System.Windows.Forms.Button();
			radioButtonMultipleVendor = new System.Windows.Forms.RadioButton();
			textBoxMultipleVendors = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)comboBoxFromVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleVendor).BeginInit();
			SuspendLayout();
			comboBoxFromVendor.Assigned = false;
			comboBoxFromVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromVendor.CustomReportFieldName = "";
			comboBoxFromVendor.CustomReportKey = "";
			comboBoxFromVendor.CustomReportValueType = 1;
			comboBoxFromVendor.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromVendor.DisplayLayout.Appearance = appearance;
			comboBoxFromVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromVendor.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxFromVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxFromVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromVendor.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromVendor.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxFromVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromVendor.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromVendor.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxFromVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromVendor.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromVendor.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxFromVendor.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxFromVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromVendor.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxFromVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxFromVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromVendor.Editable = true;
			comboBoxFromVendor.Enabled = false;
			comboBoxFromVendor.FilterString = "";
			comboBoxFromVendor.FilterSysDocID = "";
			comboBoxFromVendor.HasAll = false;
			comboBoxFromVendor.HasCustom = false;
			comboBoxFromVendor.IsDataLoaded = false;
			comboBoxFromVendor.Location = new System.Drawing.Point(182, 48);
			comboBoxFromVendor.MaxDropDownItems = 12;
			comboBoxFromVendor.Name = "comboBoxFromVendor";
			comboBoxFromVendor.ShowConsignmentOnly = false;
			comboBoxFromVendor.ShowQuickAdd = true;
			comboBoxFromVendor.Size = new System.Drawing.Size(103, 20);
			comboBoxFromVendor.TabIndex = 4;
			comboBoxFromVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToVendor.Assigned = false;
			comboBoxToVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToVendor.CustomReportFieldName = "";
			comboBoxToVendor.CustomReportKey = "";
			comboBoxToVendor.CustomReportValueType = 1;
			comboBoxToVendor.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToVendor.DisplayLayout.Appearance = appearance13;
			comboBoxToVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToVendor.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxToVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxToVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToVendor.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToVendor.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxToVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToVendor.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToVendor.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxToVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToVendor.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToVendor.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxToVendor.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxToVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxToVendor.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxToVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxToVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToVendor.Editable = true;
			comboBoxToVendor.Enabled = false;
			comboBoxToVendor.FilterString = "";
			comboBoxToVendor.FilterSysDocID = "";
			comboBoxToVendor.HasAll = false;
			comboBoxToVendor.HasCustom = false;
			comboBoxToVendor.IsDataLoaded = false;
			comboBoxToVendor.Location = new System.Drawing.Point(320, 48);
			comboBoxToVendor.MaxDropDownItems = 12;
			comboBoxToVendor.Name = "comboBoxToVendor";
			comboBoxToVendor.ShowConsignmentOnly = false;
			comboBoxToVendor.ShowQuickAdd = true;
			comboBoxToVendor.Size = new System.Drawing.Size(103, 20);
			comboBoxToVendor.TabIndex = 5;
			comboBoxToVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(291, 51);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(78, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.Text = "All Vendors";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 51);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(60, 17);
			radioButtonRange.TabIndex = 3;
			radioButtonRange.Text = "Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonClass.AutoSize = true;
			radioButtonClass.Location = new System.Drawing.Point(6, 74);
			radioButtonClass.Name = "radioButtonClass";
			radioButtonClass.Size = new System.Drawing.Size(90, 17);
			radioButtonClass.TabIndex = 6;
			radioButtonClass.Text = "Vendor Class:";
			radioButtonClass.UseVisualStyleBackColor = true;
			radioButtonClass.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonGroup.AutoSize = true;
			radioButtonGroup.Location = new System.Drawing.Point(6, 97);
			radioButtonGroup.Name = "radioButtonGroup";
			radioButtonGroup.Size = new System.Drawing.Size(94, 17);
			radioButtonGroup.TabIndex = 9;
			radioButtonGroup.Text = "Vendor Group:";
			radioButtonGroup.UseVisualStyleBackColor = true;
			radioButtonGroup.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxFromClass.Assigned = false;
			comboBoxFromClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromClass.CustomReportFieldName = "";
			comboBoxFromClass.CustomReportKey = "";
			comboBoxFromClass.CustomReportValueType = 1;
			comboBoxFromClass.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromClass.DisplayLayout.Appearance = appearance25;
			comboBoxFromClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxFromClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromClass.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxFromClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromClass.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromClass.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromClass.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxFromClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromClass.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromClass.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxFromClass.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxFromClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromClass.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxFromClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxFromClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromClass.Editable = true;
			comboBoxFromClass.Enabled = false;
			comboBoxFromClass.FilterString = "";
			comboBoxFromClass.HasAllAccount = false;
			comboBoxFromClass.HasCustom = false;
			comboBoxFromClass.IsDataLoaded = false;
			comboBoxFromClass.Location = new System.Drawing.Point(182, 71);
			comboBoxFromClass.MaxDropDownItems = 12;
			comboBoxFromClass.Name = "comboBoxFromClass";
			comboBoxFromClass.ShowInactiveItems = false;
			comboBoxFromClass.ShowQuickAdd = true;
			comboBoxFromClass.Size = new System.Drawing.Size(103, 20);
			comboBoxFromClass.TabIndex = 7;
			comboBoxFromClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToClass.Assigned = false;
			comboBoxToClass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToClass.CustomReportFieldName = "";
			comboBoxToClass.CustomReportKey = "";
			comboBoxToClass.CustomReportValueType = 1;
			comboBoxToClass.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToClass.DisplayLayout.Appearance = appearance37;
			comboBoxToClass.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToClass.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxToClass.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToClass.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxToClass.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToClass.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToClass.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToClass.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxToClass.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToClass.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToClass.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxToClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToClass.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToClass.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxToClass.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxToClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToClass.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxToClass.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxToClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToClass.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxToClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToClass.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToClass.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToClass.Editable = true;
			comboBoxToClass.Enabled = false;
			comboBoxToClass.FilterString = "";
			comboBoxToClass.HasAllAccount = false;
			comboBoxToClass.HasCustom = false;
			comboBoxToClass.IsDataLoaded = false;
			comboBoxToClass.Location = new System.Drawing.Point(320, 71);
			comboBoxToClass.MaxDropDownItems = 12;
			comboBoxToClass.Name = "comboBoxToClass";
			comboBoxToClass.ShowInactiveItems = false;
			comboBoxToClass.ShowQuickAdd = true;
			comboBoxToClass.Size = new System.Drawing.Size(103, 20);
			comboBoxToClass.TabIndex = 8;
			comboBoxToClass.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(291, 97);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(23, 13);
			label1.TabIndex = 11;
			label1.Text = "To:";
			comboBoxFromGroup.Assigned = false;
			comboBoxFromGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromGroup.CustomReportFieldName = "";
			comboBoxFromGroup.CustomReportKey = "";
			comboBoxFromGroup.CustomReportValueType = 1;
			comboBoxFromGroup.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromGroup.DisplayLayout.Appearance = appearance49;
			comboBoxFromGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxFromGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxFromGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromGroup.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromGroup.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxFromGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromGroup.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxFromGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromGroup.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxFromGroup.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxFromGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromGroup.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxFromGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxFromGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromGroup.Editable = true;
			comboBoxFromGroup.Enabled = false;
			comboBoxFromGroup.FilterString = "";
			comboBoxFromGroup.HasAllAccount = false;
			comboBoxFromGroup.HasCustom = false;
			comboBoxFromGroup.IsDataLoaded = false;
			comboBoxFromGroup.Location = new System.Drawing.Point(182, 94);
			comboBoxFromGroup.MaxDropDownItems = 12;
			comboBoxFromGroup.Name = "comboBoxFromGroup";
			comboBoxFromGroup.ShowInactiveItems = false;
			comboBoxFromGroup.ShowQuickAdd = true;
			comboBoxFromGroup.Size = new System.Drawing.Size(103, 20);
			comboBoxFromGroup.TabIndex = 10;
			comboBoxFromGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToGroup.Assigned = false;
			comboBoxToGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToGroup.CustomReportFieldName = "";
			comboBoxToGroup.CustomReportKey = "";
			comboBoxToGroup.CustomReportValueType = 1;
			comboBoxToGroup.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToGroup.DisplayLayout.Appearance = appearance61;
			comboBoxToGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxToGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxToGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToGroup.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToGroup.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxToGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToGroup.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxToGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToGroup.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxToGroup.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxToGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxToGroup.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxToGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxToGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToGroup.Editable = true;
			comboBoxToGroup.Enabled = false;
			comboBoxToGroup.FilterString = "";
			comboBoxToGroup.HasAllAccount = false;
			comboBoxToGroup.HasCustom = false;
			comboBoxToGroup.IsDataLoaded = false;
			comboBoxToGroup.Location = new System.Drawing.Point(320, 94);
			comboBoxToGroup.MaxDropDownItems = 12;
			comboBoxToGroup.Name = "comboBoxToGroup";
			comboBoxToGroup.ShowInactiveItems = false;
			comboBoxToGroup.ShowQuickAdd = true;
			comboBoxToGroup.Size = new System.Drawing.Size(103, 20);
			comboBoxToGroup.TabIndex = 11;
			comboBoxToGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(291, 74);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 6;
			label2.Text = "To:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(140, 51);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 6;
			label3.Text = "From:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(140, 75);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 6;
			label4.Text = "From:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(140, 97);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(33, 13);
			label5.TabIndex = 11;
			label5.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Checked = true;
			radioButtonSingle.Location = new System.Drawing.Point(123, 6);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.TabStop = true;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxSingleVendor.Assigned = false;
			comboBoxSingleVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleVendor.CustomReportFieldName = "";
			comboBoxSingleVendor.CustomReportKey = "";
			comboBoxSingleVendor.CustomReportValueType = 1;
			comboBoxSingleVendor.DescriptionTextBox = null;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleVendor.DisplayLayout.Appearance = appearance73;
			comboBoxSingleVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleVendor.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxSingleVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxSingleVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleVendor.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleVendor.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxSingleVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleVendor.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleVendor.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxSingleVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleVendor.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleVendor.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxSingleVendor.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxSingleVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleVendor.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxSingleVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxSingleVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleVendor.Editable = true;
			comboBoxSingleVendor.FilterString = "";
			comboBoxSingleVendor.FilterSysDocID = "";
			comboBoxSingleVendor.HasAll = false;
			comboBoxSingleVendor.HasCustom = false;
			comboBoxSingleVendor.IsDataLoaded = false;
			comboBoxSingleVendor.Location = new System.Drawing.Point(182, 3);
			comboBoxSingleVendor.MaxDropDownItems = 12;
			comboBoxSingleVendor.Name = "comboBoxSingleVendor";
			comboBoxSingleVendor.ShowConsignmentOnly = false;
			comboBoxSingleVendor.ShowQuickAdd = true;
			comboBoxSingleVendor.Size = new System.Drawing.Size(241, 20);
			comboBoxSingleVendor.TabIndex = 2;
			comboBoxSingleVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonMultiple.Location = new System.Drawing.Point(398, 24);
			buttonMultiple.Name = "buttonMultiple";
			buttonMultiple.Size = new System.Drawing.Size(26, 23);
			buttonMultiple.TabIndex = 58;
			buttonMultiple.Text = "...";
			buttonMultiple.UseVisualStyleBackColor = true;
			buttonMultiple.Click += new System.EventHandler(buttonMultiple_Click);
			radioButtonMultipleVendor.AutoSize = true;
			radioButtonMultipleVendor.Location = new System.Drawing.Point(6, 28);
			radioButtonMultipleVendor.Name = "radioButtonMultipleVendor";
			radioButtonMultipleVendor.Size = new System.Drawing.Size(103, 17);
			radioButtonMultipleVendor.TabIndex = 57;
			radioButtonMultipleVendor.Text = "Multiple Vendors";
			radioButtonMultipleVendor.UseVisualStyleBackColor = true;
			radioButtonMultipleVendor.CheckedChanged += new System.EventHandler(radioButtonMultipleVendor_CheckedChanged);
			textBoxMultipleVendors.Location = new System.Drawing.Point(182, 25);
			textBoxMultipleVendors.Name = "textBoxMultipleVendors";
			textBoxMultipleVendors.ReadOnly = true;
			textBoxMultipleVendors.Size = new System.Drawing.Size(214, 20);
			textBoxMultipleVendors.TabIndex = 56;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(buttonMultiple);
			base.Controls.Add(radioButtonMultipleVendor);
			base.Controls.Add(textBoxMultipleVendors);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(comboBoxToGroup);
			base.Controls.Add(comboBoxFromGroup);
			base.Controls.Add(label5);
			base.Controls.Add(label1);
			base.Controls.Add(comboBoxToClass);
			base.Controls.Add(comboBoxFromClass);
			base.Controls.Add(radioButtonGroup);
			base.Controls.Add(radioButtonClass);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(label4);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(labelTo);
			base.Controls.Add(comboBoxToVendor);
			base.Controls.Add(comboBoxSingleVendor);
			base.Controls.Add(comboBoxFromVendor);
			base.Name = "VendorSelector";
			base.Size = new System.Drawing.Size(432, 128);
			base.Load += new System.EventHandler(VendorSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxFromVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToClass).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleVendor).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
