using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class SalespersonSelector : UserControl
	{
		private IContainer components;

		private SalespersonComboBox comboBoxFromSalesperson;

		private SalespersonComboBox comboBoxToSalesperson;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private Label label3;

		private RadioButton radioButtonSingle;

		private SalespersonComboBox comboBoxSingleSalesperson;

		private RadioButton radioButtonDivision;

		private Label label1;

		private Label label2;

		private RadioButton radioButtonArea;

		private Label label4;

		private Label label5;

		private AreaComboBox comboBoxToArea;

		private AreaComboBox comboBoxFromArea;

		private RadioButton radioButtonCountry;

		private Label label6;

		private Label label7;

		private CountryComboBox comboBoxToCountry;

		private CountryComboBox comboBoxFromCountry;

		private RadioButton radioButtonGroup;

		private Label label8;

		private Label label9;

		private SalespersonGroupComboBox comboBoxToGroup;

		private SalespersonGroupComboBox comboBoxFromGroup;

		private CompanyDivisionComboBox comboBoxToDivision;

		private CompanyDivisionComboBox comboBoxFromDivision;

		public string FromSalesperson
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleSalesperson.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromSalesperson.SelectedID;
				}
				return "";
			}
		}

		public string FromSalespersonName
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleSalesperson.SelectedName;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxFromSalesperson.SelectedName;
				}
				return "";
			}
		}

		public string ToSalesperson
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleSalesperson.SelectedID;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToSalesperson.SelectedID;
				}
				return "";
			}
		}

		public string ToSalespersonName
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxSingleSalesperson.SelectedName;
				}
				if (radioButtonRange.Checked)
				{
					return comboBoxToSalesperson.SelectedName;
				}
				return "";
			}
		}

		public string FromDivision
		{
			get
			{
				if (radioButtonDivision.Checked)
				{
					return comboBoxFromDivision.SelectedID;
				}
				return "";
			}
		}

		public string FromDivisionName
		{
			get
			{
				if (radioButtonDivision.Checked)
				{
					return comboBoxFromDivision.SelectedName;
				}
				return "";
			}
		}

		public string ToDivision
		{
			get
			{
				if (radioButtonDivision.Checked)
				{
					return comboBoxToDivision.SelectedID;
				}
				return "";
			}
		}

		public string ToDivisionName
		{
			get
			{
				if (radioButtonDivision.Checked)
				{
					return comboBoxToDivision.SelectedName;
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

		public string FromGroupName
		{
			get
			{
				if (radioButtonGroup.Checked)
				{
					return comboBoxFromGroup.SelectedName;
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

		public string ToGroupName
		{
			get
			{
				if (radioButtonGroup.Checked)
				{
					return comboBoxToGroup.SelectedName;
				}
				return "";
			}
		}

		public string FromArea
		{
			get
			{
				if (radioButtonArea.Checked)
				{
					return comboBoxFromArea.SelectedID;
				}
				return "";
			}
		}

		public string FromAreaName
		{
			get
			{
				if (radioButtonArea.Checked)
				{
					return comboBoxFromArea.SelectedName;
				}
				return "";
			}
		}

		public string ToArea
		{
			get
			{
				if (radioButtonArea.Checked)
				{
					return comboBoxToArea.SelectedID;
				}
				return "";
			}
		}

		public string ToAreaName
		{
			get
			{
				if (radioButtonArea.Checked)
				{
					return comboBoxToArea.SelectedName;
				}
				return "";
			}
		}

		public string FromCountry
		{
			get
			{
				if (radioButtonCountry.Checked)
				{
					return comboBoxFromCountry.SelectedID;
				}
				return "";
			}
		}

		public string FromCountryName
		{
			get
			{
				if (radioButtonCountry.Checked)
				{
					return comboBoxFromCountry.SelectedName;
				}
				return "";
			}
		}

		public string ToCountry
		{
			get
			{
				if (radioButtonCountry.Checked)
				{
					return comboBoxToCountry.SelectedID;
				}
				return "";
			}
		}

		public string ToCountryName
		{
			get
			{
				if (radioButtonCountry.Checked)
				{
					return comboBoxToCountry.SelectedName;
				}
				return "";
			}
		}

		public SalespersonSelector()
		{
			InitializeComponent();
		}

		private void SalespersonSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleSalesperson.Enabled = radioButtonSingle.Checked;
			SalespersonComboBox salespersonComboBox = comboBoxFromSalesperson;
			bool enabled = comboBoxToSalesperson.Enabled = radioButtonRange.Checked;
			salespersonComboBox.Enabled = enabled;
			SalespersonGroupComboBox salespersonGroupComboBox = comboBoxFromGroup;
			enabled = (comboBoxToGroup.Enabled = radioButtonGroup.Checked);
			salespersonGroupComboBox.Enabled = enabled;
			CompanyDivisionComboBox companyDivisionComboBox = comboBoxFromDivision;
			enabled = (comboBoxToDivision.Enabled = radioButtonDivision.Checked);
			companyDivisionComboBox.Enabled = enabled;
			AreaComboBox areaComboBox = comboBoxFromArea;
			enabled = (comboBoxToArea.Enabled = radioButtonArea.Checked);
			areaComboBox.Enabled = enabled;
			CountryComboBox countryComboBox = comboBoxFromCountry;
			enabled = (comboBoxToCountry.Enabled = radioButtonCountry.Checked);
			countryComboBox.Enabled = enabled;
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
			Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
			comboBoxFromSalesperson = new Micromind.DataControls.SalespersonComboBox();
			comboBoxToSalesperson = new Micromind.DataControls.SalespersonComboBox();
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			label3 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxSingleSalesperson = new Micromind.DataControls.SalespersonComboBox();
			radioButtonDivision = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			radioButtonArea = new System.Windows.Forms.RadioButton();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			comboBoxToArea = new Micromind.DataControls.AreaComboBox();
			comboBoxFromArea = new Micromind.DataControls.AreaComboBox();
			radioButtonCountry = new System.Windows.Forms.RadioButton();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			comboBoxToCountry = new Micromind.DataControls.CountryComboBox();
			comboBoxFromCountry = new Micromind.DataControls.CountryComboBox();
			radioButtonGroup = new System.Windows.Forms.RadioButton();
			label8 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			comboBoxToGroup = new Micromind.DataControls.SalespersonGroupComboBox();
			comboBoxFromGroup = new Micromind.DataControls.SalespersonGroupComboBox();
			comboBoxToDivision = new Micromind.DataControls.CompanyDivisionComboBox();
			comboBoxFromDivision = new Micromind.DataControls.CompanyDivisionComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxFromSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromArea).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCountry).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCountry).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToDivision).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromDivision).BeginInit();
			SuspendLayout();
			comboBoxFromSalesperson.Assigned = false;
			comboBoxFromSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromSalesperson.CustomReportFieldName = "";
			comboBoxFromSalesperson.CustomReportKey = "";
			comboBoxFromSalesperson.CustomReportValueType = 1;
			comboBoxFromSalesperson.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromSalesperson.DisplayLayout.Appearance = appearance;
			comboBoxFromSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromSalesperson.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxFromSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxFromSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxFromSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromSalesperson.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxFromSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxFromSalesperson.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxFromSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromSalesperson.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxFromSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxFromSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromSalesperson.Editable = true;
			comboBoxFromSalesperson.Enabled = false;
			comboBoxFromSalesperson.FilterString = "";
			comboBoxFromSalesperson.HasAllAccount = false;
			comboBoxFromSalesperson.HasCustom = false;
			comboBoxFromSalesperson.IsDataLoaded = false;
			comboBoxFromSalesperson.Location = new System.Drawing.Point(187, 25);
			comboBoxFromSalesperson.MaxDropDownItems = 12;
			comboBoxFromSalesperson.Name = "comboBoxFromSalesperson";
			comboBoxFromSalesperson.ShowInactiveItems = false;
			comboBoxFromSalesperson.ShowQuickAdd = true;
			comboBoxFromSalesperson.Size = new System.Drawing.Size(103, 20);
			comboBoxFromSalesperson.TabIndex = 4;
			comboBoxFromSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToSalesperson.Assigned = false;
			comboBoxToSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToSalesperson.CustomReportFieldName = "";
			comboBoxToSalesperson.CustomReportKey = "";
			comboBoxToSalesperson.CustomReportValueType = 1;
			comboBoxToSalesperson.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToSalesperson.DisplayLayout.Appearance = appearance13;
			comboBoxToSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToSalesperson.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxToSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxToSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxToSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToSalesperson.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxToSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxToSalesperson.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxToSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxToSalesperson.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxToSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxToSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToSalesperson.Editable = true;
			comboBoxToSalesperson.Enabled = false;
			comboBoxToSalesperson.FilterString = "";
			comboBoxToSalesperson.HasAllAccount = false;
			comboBoxToSalesperson.HasCustom = false;
			comboBoxToSalesperson.IsDataLoaded = false;
			comboBoxToSalesperson.Location = new System.Drawing.Point(320, 25);
			comboBoxToSalesperson.MaxDropDownItems = 12;
			comboBoxToSalesperson.Name = "comboBoxToSalesperson";
			comboBoxToSalesperson.ShowInactiveItems = false;
			comboBoxToSalesperson.ShowQuickAdd = true;
			comboBoxToSalesperson.Size = new System.Drawing.Size(103, 20);
			comboBoxToSalesperson.TabIndex = 5;
			comboBoxToSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(291, 28);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(102, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Salespersons";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 26);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(60, 17);
			radioButtonRange.TabIndex = 3;
			radioButtonRange.Text = "Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(135, 28);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 6;
			label3.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(119, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxSingleSalesperson.Assigned = false;
			comboBoxSingleSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleSalesperson.CustomReportFieldName = "";
			comboBoxSingleSalesperson.CustomReportKey = "";
			comboBoxSingleSalesperson.CustomReportValueType = 1;
			comboBoxSingleSalesperson.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSingleSalesperson.DisplayLayout.Appearance = appearance25;
			comboBoxSingleSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSingleSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleSalesperson.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxSingleSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSingleSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxSingleSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSingleSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSingleSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSingleSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxSingleSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSingleSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSingleSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSingleSalesperson.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxSingleSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSingleSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSingleSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxSingleSalesperson.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxSingleSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSingleSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxSingleSalesperson.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxSingleSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSingleSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxSingleSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSingleSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSingleSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSingleSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleSalesperson.Editable = true;
			comboBoxSingleSalesperson.Enabled = false;
			comboBoxSingleSalesperson.FilterString = "";
			comboBoxSingleSalesperson.HasAllAccount = false;
			comboBoxSingleSalesperson.HasCustom = false;
			comboBoxSingleSalesperson.IsDataLoaded = false;
			comboBoxSingleSalesperson.Location = new System.Drawing.Point(187, 3);
			comboBoxSingleSalesperson.MaxDropDownItems = 12;
			comboBoxSingleSalesperson.Name = "comboBoxSingleSalesperson";
			comboBoxSingleSalesperson.ShowInactiveItems = false;
			comboBoxSingleSalesperson.ShowQuickAdd = true;
			comboBoxSingleSalesperson.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleSalesperson.TabIndex = 2;
			comboBoxSingleSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			radioButtonDivision.AutoSize = true;
			radioButtonDivision.Location = new System.Drawing.Point(6, 48);
			radioButtonDivision.Name = "radioButtonDivision";
			radioButtonDivision.Size = new System.Drawing.Size(65, 17);
			radioButtonDivision.TabIndex = 7;
			radioButtonDivision.Text = "Division:";
			radioButtonDivision.UseVisualStyleBackColor = true;
			radioButtonDivision.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(135, 50);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(33, 13);
			label1.TabIndex = 10;
			label1.Text = "From:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(291, 50);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 11;
			label2.Text = "To:";
			radioButtonArea.AutoSize = true;
			radioButtonArea.Location = new System.Drawing.Point(6, 92);
			radioButtonArea.Name = "radioButtonArea";
			radioButtonArea.Size = new System.Drawing.Size(50, 17);
			radioButtonArea.TabIndex = 12;
			radioButtonArea.Text = "Area:";
			radioButtonArea.UseVisualStyleBackColor = true;
			radioButtonArea.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(135, 94);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(33, 13);
			label4.TabIndex = 15;
			label4.Text = "From:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(291, 94);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(23, 13);
			label5.TabIndex = 16;
			label5.Text = "To:";
			comboBoxToArea.Assigned = false;
			comboBoxToArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToArea.CustomReportFieldName = "";
			comboBoxToArea.CustomReportKey = "";
			comboBoxToArea.CustomReportValueType = 1;
			comboBoxToArea.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToArea.DisplayLayout.Appearance = appearance37;
			comboBoxToArea.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToArea.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToArea.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToArea.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxToArea.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToArea.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxToArea.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToArea.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToArea.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToArea.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxToArea.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToArea.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToArea.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToArea.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxToArea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToArea.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToArea.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxToArea.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxToArea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToArea.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxToArea.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxToArea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToArea.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxToArea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToArea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToArea.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToArea.Editable = true;
			comboBoxToArea.Enabled = false;
			comboBoxToArea.FilterString = "";
			comboBoxToArea.HasAllAccount = false;
			comboBoxToArea.HasCustom = false;
			comboBoxToArea.IsDataLoaded = false;
			comboBoxToArea.Location = new System.Drawing.Point(320, 91);
			comboBoxToArea.MaxDropDownItems = 12;
			comboBoxToArea.Name = "comboBoxToArea";
			comboBoxToArea.ShowInactiveItems = false;
			comboBoxToArea.ShowQuickAdd = true;
			comboBoxToArea.Size = new System.Drawing.Size(103, 20);
			comboBoxToArea.TabIndex = 14;
			comboBoxToArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromArea.Assigned = false;
			comboBoxFromArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromArea.CustomReportFieldName = "";
			comboBoxFromArea.CustomReportKey = "";
			comboBoxFromArea.CustomReportValueType = 1;
			comboBoxFromArea.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromArea.DisplayLayout.Appearance = appearance49;
			comboBoxFromArea.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromArea.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromArea.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromArea.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxFromArea.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromArea.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxFromArea.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromArea.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromArea.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromArea.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxFromArea.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromArea.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromArea.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromArea.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxFromArea.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromArea.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromArea.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxFromArea.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxFromArea.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromArea.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromArea.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxFromArea.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromArea.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxFromArea.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromArea.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromArea.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromArea.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromArea.Editable = true;
			comboBoxFromArea.Enabled = false;
			comboBoxFromArea.FilterString = "";
			comboBoxFromArea.HasAllAccount = false;
			comboBoxFromArea.HasCustom = false;
			comboBoxFromArea.IsDataLoaded = false;
			comboBoxFromArea.Location = new System.Drawing.Point(187, 91);
			comboBoxFromArea.MaxDropDownItems = 12;
			comboBoxFromArea.Name = "comboBoxFromArea";
			comboBoxFromArea.ShowInactiveItems = false;
			comboBoxFromArea.ShowQuickAdd = true;
			comboBoxFromArea.Size = new System.Drawing.Size(103, 20);
			comboBoxFromArea.TabIndex = 13;
			comboBoxFromArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			radioButtonCountry.AutoSize = true;
			radioButtonCountry.Location = new System.Drawing.Point(6, 114);
			radioButtonCountry.Name = "radioButtonCountry";
			radioButtonCountry.Size = new System.Drawing.Size(64, 17);
			radioButtonCountry.TabIndex = 17;
			radioButtonCountry.Text = "Country:";
			radioButtonCountry.UseVisualStyleBackColor = true;
			radioButtonCountry.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(135, 116);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(33, 13);
			label6.TabIndex = 20;
			label6.Text = "From:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(291, 116);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(23, 13);
			label7.TabIndex = 21;
			label7.Text = "To:";
			comboBoxToCountry.Assigned = false;
			comboBoxToCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToCountry.CustomReportFieldName = "";
			comboBoxToCountry.CustomReportKey = "";
			comboBoxToCountry.CustomReportValueType = 1;
			comboBoxToCountry.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToCountry.DisplayLayout.Appearance = appearance61;
			comboBoxToCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCountry.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxToCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxToCountry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToCountry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToCountry.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToCountry.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxToCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToCountry.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToCountry.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxToCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToCountry.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToCountry.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxToCountry.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxToCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxToCountry.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxToCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxToCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToCountry.Editable = true;
			comboBoxToCountry.Enabled = false;
			comboBoxToCountry.FilterString = "";
			comboBoxToCountry.HasAllAccount = false;
			comboBoxToCountry.HasCustom = false;
			comboBoxToCountry.IsDataLoaded = false;
			comboBoxToCountry.Location = new System.Drawing.Point(320, 113);
			comboBoxToCountry.MaxDropDownItems = 12;
			comboBoxToCountry.Name = "comboBoxToCountry";
			comboBoxToCountry.ShowInactiveItems = false;
			comboBoxToCountry.ShowQuickAdd = true;
			comboBoxToCountry.Size = new System.Drawing.Size(103, 20);
			comboBoxToCountry.TabIndex = 19;
			comboBoxToCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromCountry.Assigned = false;
			comboBoxFromCountry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromCountry.CustomReportFieldName = "";
			comboBoxFromCountry.CustomReportKey = "";
			comboBoxFromCountry.CustomReportValueType = 1;
			comboBoxFromCountry.DescriptionTextBox = null;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromCountry.DisplayLayout.Appearance = appearance73;
			comboBoxFromCountry.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromCountry.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCountry.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCountry.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxFromCountry.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromCountry.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxFromCountry.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromCountry.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromCountry.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromCountry.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxFromCountry.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromCountry.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromCountry.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromCountry.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxFromCountry.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromCountry.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromCountry.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxFromCountry.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxFromCountry.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromCountry.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromCountry.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxFromCountry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromCountry.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxFromCountry.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromCountry.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromCountry.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromCountry.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromCountry.Editable = true;
			comboBoxFromCountry.Enabled = false;
			comboBoxFromCountry.FilterString = "";
			comboBoxFromCountry.HasAllAccount = false;
			comboBoxFromCountry.HasCustom = false;
			comboBoxFromCountry.IsDataLoaded = false;
			comboBoxFromCountry.Location = new System.Drawing.Point(187, 113);
			comboBoxFromCountry.MaxDropDownItems = 12;
			comboBoxFromCountry.Name = "comboBoxFromCountry";
			comboBoxFromCountry.ShowInactiveItems = false;
			comboBoxFromCountry.ShowQuickAdd = true;
			comboBoxFromCountry.Size = new System.Drawing.Size(103, 20);
			comboBoxFromCountry.TabIndex = 18;
			comboBoxFromCountry.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			radioButtonGroup.AutoSize = true;
			radioButtonGroup.Location = new System.Drawing.Point(6, 70);
			radioButtonGroup.Name = "radioButtonGroup";
			radioButtonGroup.Size = new System.Drawing.Size(57, 17);
			radioButtonGroup.TabIndex = 22;
			radioButtonGroup.Text = "Group:";
			radioButtonGroup.UseVisualStyleBackColor = true;
			radioButtonGroup.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(135, 72);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(33, 13);
			label8.TabIndex = 25;
			label8.Text = "From:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(291, 72);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(23, 13);
			label9.TabIndex = 26;
			label9.Text = "To:";
			comboBoxToGroup.Assigned = false;
			comboBoxToGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToGroup.CustomReportFieldName = "";
			comboBoxToGroup.CustomReportKey = "";
			comboBoxToGroup.CustomReportValueType = 1;
			comboBoxToGroup.DescriptionTextBox = null;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToGroup.DisplayLayout.Appearance = appearance85;
			comboBoxToGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance86.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.GroupByBox.Appearance = appearance86;
			appearance87.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance87;
			comboBoxToGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance88.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance88.BackColor2 = System.Drawing.SystemColors.Control;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance88;
			comboBoxToGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToGroup.DisplayLayout.Override.ActiveCellAppearance = appearance89;
			appearance90.BackColor = System.Drawing.SystemColors.Highlight;
			appearance90.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToGroup.DisplayLayout.Override.ActiveRowAppearance = appearance90;
			comboBoxToGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.Override.CardAreaAppearance = appearance91;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			appearance92.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToGroup.DisplayLayout.Override.CellAppearance = appearance92;
			comboBoxToGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToGroup.DisplayLayout.Override.CellPadding = 0;
			appearance93.BackColor = System.Drawing.SystemColors.Control;
			appearance93.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance93.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance93.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToGroup.DisplayLayout.Override.GroupByRowAppearance = appearance93;
			appearance94.TextHAlignAsString = "Left";
			comboBoxToGroup.DisplayLayout.Override.HeaderAppearance = appearance94;
			comboBoxToGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.Color.Silver;
			comboBoxToGroup.DisplayLayout.Override.RowAppearance = appearance95;
			comboBoxToGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance96;
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
			comboBoxToGroup.Location = new System.Drawing.Point(320, 69);
			comboBoxToGroup.MaxDropDownItems = 12;
			comboBoxToGroup.Name = "comboBoxToGroup";
			comboBoxToGroup.ShowInactiveItems = false;
			comboBoxToGroup.ShowQuickAdd = true;
			comboBoxToGroup.Size = new System.Drawing.Size(103, 20);
			comboBoxToGroup.TabIndex = 24;
			comboBoxToGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromGroup.Assigned = false;
			comboBoxFromGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromGroup.CustomReportFieldName = "";
			comboBoxFromGroup.CustomReportKey = "";
			comboBoxFromGroup.CustomReportValueType = 1;
			comboBoxFromGroup.DescriptionTextBox = null;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromGroup.DisplayLayout.Appearance = appearance97;
			comboBoxFromGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance98.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance98.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.GroupByBox.Appearance = appearance98;
			appearance99.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance99;
			comboBoxFromGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance100.BackColor2 = System.Drawing.SystemColors.Control;
			appearance100.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance100.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance100;
			comboBoxFromGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromGroup.DisplayLayout.Override.ActiveCellAppearance = appearance101;
			appearance102.BackColor = System.Drawing.SystemColors.Highlight;
			appearance102.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromGroup.DisplayLayout.Override.ActiveRowAppearance = appearance102;
			comboBoxFromGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.Override.CardAreaAppearance = appearance103;
			appearance104.BorderColor = System.Drawing.Color.Silver;
			appearance104.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromGroup.DisplayLayout.Override.CellAppearance = appearance104;
			comboBoxFromGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromGroup.DisplayLayout.Override.CellPadding = 0;
			appearance105.BackColor = System.Drawing.SystemColors.Control;
			appearance105.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance105.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance105.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance105.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromGroup.DisplayLayout.Override.GroupByRowAppearance = appearance105;
			appearance106.TextHAlignAsString = "Left";
			comboBoxFromGroup.DisplayLayout.Override.HeaderAppearance = appearance106;
			comboBoxFromGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromGroup.DisplayLayout.Override.RowAppearance = appearance107;
			comboBoxFromGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance108;
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
			comboBoxFromGroup.Location = new System.Drawing.Point(187, 69);
			comboBoxFromGroup.MaxDropDownItems = 12;
			comboBoxFromGroup.Name = "comboBoxFromGroup";
			comboBoxFromGroup.ShowInactiveItems = false;
			comboBoxFromGroup.ShowQuickAdd = true;
			comboBoxFromGroup.Size = new System.Drawing.Size(103, 20);
			comboBoxFromGroup.TabIndex = 23;
			comboBoxFromGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToDivision.Assigned = false;
			comboBoxToDivision.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToDivision.CustomReportFieldName = "";
			comboBoxToDivision.CustomReportKey = "";
			comboBoxToDivision.CustomReportValueType = 1;
			comboBoxToDivision.DescriptionTextBox = null;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToDivision.DisplayLayout.Appearance = appearance109;
			comboBoxToDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance110.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance110.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance110.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToDivision.DisplayLayout.GroupByBox.Appearance = appearance110;
			appearance111.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance111;
			comboBoxToDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance112.BackColor2 = System.Drawing.SystemColors.Control;
			appearance112.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance112.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance112;
			comboBoxToDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToDivision.DisplayLayout.Override.ActiveCellAppearance = appearance113;
			appearance114.BackColor = System.Drawing.SystemColors.Highlight;
			appearance114.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToDivision.DisplayLayout.Override.ActiveRowAppearance = appearance114;
			comboBoxToDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToDivision.DisplayLayout.Override.CardAreaAppearance = appearance115;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			appearance116.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToDivision.DisplayLayout.Override.CellAppearance = appearance116;
			comboBoxToDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToDivision.DisplayLayout.Override.CellPadding = 0;
			appearance117.BackColor = System.Drawing.SystemColors.Control;
			appearance117.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance117.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance117.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance117.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToDivision.DisplayLayout.Override.GroupByRowAppearance = appearance117;
			appearance118.TextHAlignAsString = "Left";
			comboBoxToDivision.DisplayLayout.Override.HeaderAppearance = appearance118;
			comboBoxToDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			appearance119.BorderColor = System.Drawing.Color.Silver;
			comboBoxToDivision.DisplayLayout.Override.RowAppearance = appearance119;
			comboBoxToDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance120;
			comboBoxToDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToDivision.Editable = true;
			comboBoxToDivision.Enabled = false;
			comboBoxToDivision.FilterString = "";
			comboBoxToDivision.HasAllAccount = false;
			comboBoxToDivision.HasCustom = false;
			comboBoxToDivision.IsDataLoaded = false;
			comboBoxToDivision.Location = new System.Drawing.Point(320, 47);
			comboBoxToDivision.MaxDropDownItems = 12;
			comboBoxToDivision.Name = "comboBoxToDivision";
			comboBoxToDivision.ShowInactiveItems = false;
			comboBoxToDivision.ShowQuickAdd = true;
			comboBoxToDivision.Size = new System.Drawing.Size(103, 20);
			comboBoxToDivision.TabIndex = 27;
			comboBoxToDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromDivision.Assigned = false;
			comboBoxFromDivision.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromDivision.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromDivision.CustomReportFieldName = "";
			comboBoxFromDivision.CustomReportKey = "";
			comboBoxFromDivision.CustomReportValueType = 1;
			comboBoxFromDivision.DescriptionTextBox = null;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromDivision.DisplayLayout.Appearance = appearance121;
			comboBoxFromDivision.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromDivision.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance122.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance122.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance122.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromDivision.DisplayLayout.GroupByBox.Appearance = appearance122;
			appearance123.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromDivision.DisplayLayout.GroupByBox.BandLabelAppearance = appearance123;
			comboBoxFromDivision.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance124.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance124.BackColor2 = System.Drawing.SystemColors.Control;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance124.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromDivision.DisplayLayout.GroupByBox.PromptAppearance = appearance124;
			comboBoxFromDivision.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromDivision.DisplayLayout.MaxRowScrollRegions = 1;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromDivision.DisplayLayout.Override.ActiveCellAppearance = appearance125;
			appearance126.BackColor = System.Drawing.SystemColors.Highlight;
			appearance126.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromDivision.DisplayLayout.Override.ActiveRowAppearance = appearance126;
			comboBoxFromDivision.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromDivision.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromDivision.DisplayLayout.Override.CardAreaAppearance = appearance127;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			appearance128.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromDivision.DisplayLayout.Override.CellAppearance = appearance128;
			comboBoxFromDivision.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromDivision.DisplayLayout.Override.CellPadding = 0;
			appearance129.BackColor = System.Drawing.SystemColors.Control;
			appearance129.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance129.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance129.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance129.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromDivision.DisplayLayout.Override.GroupByRowAppearance = appearance129;
			appearance130.TextHAlignAsString = "Left";
			comboBoxFromDivision.DisplayLayout.Override.HeaderAppearance = appearance130;
			comboBoxFromDivision.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromDivision.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromDivision.DisplayLayout.Override.RowAppearance = appearance131;
			comboBoxFromDivision.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromDivision.DisplayLayout.Override.TemplateAddRowAppearance = appearance132;
			comboBoxFromDivision.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromDivision.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromDivision.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromDivision.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromDivision.Editable = true;
			comboBoxFromDivision.Enabled = false;
			comboBoxFromDivision.FilterString = "";
			comboBoxFromDivision.HasAllAccount = false;
			comboBoxFromDivision.HasCustom = false;
			comboBoxFromDivision.IsDataLoaded = false;
			comboBoxFromDivision.Location = new System.Drawing.Point(187, 47);
			comboBoxFromDivision.MaxDropDownItems = 12;
			comboBoxFromDivision.Name = "comboBoxFromDivision";
			comboBoxFromDivision.ShowInactiveItems = false;
			comboBoxFromDivision.ShowQuickAdd = true;
			comboBoxFromDivision.Size = new System.Drawing.Size(103, 20);
			comboBoxFromDivision.TabIndex = 28;
			comboBoxFromDivision.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxFromDivision);
			base.Controls.Add(comboBoxToDivision);
			base.Controls.Add(radioButtonGroup);
			base.Controls.Add(label8);
			base.Controls.Add(label9);
			base.Controls.Add(comboBoxToGroup);
			base.Controls.Add(comboBoxFromGroup);
			base.Controls.Add(radioButtonCountry);
			base.Controls.Add(label6);
			base.Controls.Add(label7);
			base.Controls.Add(comboBoxToCountry);
			base.Controls.Add(comboBoxFromCountry);
			base.Controls.Add(radioButtonArea);
			base.Controls.Add(label4);
			base.Controls.Add(label5);
			base.Controls.Add(comboBoxToArea);
			base.Controls.Add(comboBoxFromArea);
			base.Controls.Add(radioButtonDivision);
			base.Controls.Add(label1);
			base.Controls.Add(label2);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Controls.Add(comboBoxToSalesperson);
			base.Controls.Add(comboBoxSingleSalesperson);
			base.Controls.Add(comboBoxFromSalesperson);
			base.Name = "SalespersonSelector";
			base.Size = new System.Drawing.Size(433, 141);
			base.Load += new System.EventHandler(SalespersonSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxFromSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromArea).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToCountry).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromCountry).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToDivision).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromDivision).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
