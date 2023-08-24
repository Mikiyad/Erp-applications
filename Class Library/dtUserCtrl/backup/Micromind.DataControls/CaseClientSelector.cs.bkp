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
	public class CaseClientSelector : UserControl, ICustomReportControl
	{
		private bool showGroupAndClass = true;

		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private List<string> CustomerList = new List<string>();

		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonDefendant;

		private Label label3;

		private RadioButton radioButtonSingle;

		private CaseClientComboBox comboBoxCaseClientSingle;

		private CaseClientComboBox comboBoxFromDefendantCaseClient;

		private CaseClientComboBox comboBoxToDefendantCaseClient;

		private CaseClientComboBox comboBoxToPlantiffCaseClient;

		private CaseClientComboBox comboBoxFromPlantiffCaseClient;

		private RadioButton radioButtonPlantiff;

		private Label label1;

		private Label label2;

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
		public bool IsSingleCustomer => radioButtonSingle.Checked;

		public string FromCustomer
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxCaseClientSingle.SelectedID;
				}
				if (radioButtonDefendant.Checked)
				{
					return comboBoxFromDefendantCaseClient.SelectedID;
				}
				return "";
			}
		}

		public string ToCustomer
		{
			get
			{
				if (radioButtonSingle.Checked)
				{
					return comboBoxCaseClientSingle.SelectedID;
				}
				if (radioButtonDefendant.Checked)
				{
					return comboBoxToDefendantCaseClient.SelectedID;
				}
				return "";
			}
		}

		public CaseClientSelector()
		{
			InitializeComponent();
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				if (radioButtonAll.Checked)
				{
					return "ANY(SELECT CaseClientID FROM CaseClient)";
				}
				if (radioButtonSingle.Checked)
				{
					return "'" + comboBoxCaseClientSingle.SelectedID + "'";
				}
				if (radioButtonDefendant.Checked)
				{
					return "ANY(SELECT CaseClientID FROM CaseClient WHERE CaseClient Between '" + comboBoxFromDefendantCaseClient.SelectedID + "' AND '" + comboBoxToDefendantCaseClient.SelectedID + "')";
				}
				if (radioButtonPlantiff.Checked)
				{
					return "ANY(SELECT CaseClientID FROM CaseClient WHERE CaseClient Between '" + comboBoxFromDefendantCaseClient.SelectedID + "' AND '" + comboBoxToDefendantCaseClient.SelectedID + "')";
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
				return crFieldName + " = '" + comboBoxCaseClientSingle.SelectedID + "'";
			}
			if (radioButtonDefendant.Checked)
			{
				return crFieldName + " Between '" + comboBoxFromDefendantCaseClient.SelectedID + "' AND '" + comboBoxToDefendantCaseClient.SelectedID + "')";
			}
			return "''=''";
		}

		private void CustomerSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxCaseClientSingle.Enabled = radioButtonSingle.Checked;
			CaseClientComboBox caseClientComboBox = comboBoxFromDefendantCaseClient;
			bool enabled = comboBoxToDefendantCaseClient.Enabled = radioButtonDefendant.Checked;
			caseClientComboBox.Enabled = enabled;
		}

		private void radioButtons_CheckedChanged(object sender, EventArgs e)
		{
			EnableDisableControls();
		}

		private void buttonMultiple_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			List<string> selectedDocuments = new List<string>();
			dataSet = Factory.CaseClientSystem.GetCustomerList();
			SelectTransactionDialog selectTransactionDialog = new SelectTransactionDialog();
			selectTransactionDialog.DataSource = dataSet;
			selectTransactionDialog.IsMultiSelect = true;
			selectTransactionDialog.SelectedDocuments = selectedDocuments;
			selectTransactionDialog.Text = "Select CaseClients";
			if (selectTransactionDialog.ShowDialog(this) == DialogResult.OK)
			{
				selectedDocuments = selectTransactionDialog.SelectedDocuments;
				foreach (UltraGridRow selectedRow in selectTransactionDialog.SelectedRows)
				{
					string item = selectedRow.Cells["Code"].Value.ToString();
					selectedRow.Cells["Name"].Value.ToString();
					CustomerList.Add(item);
				}
			}
		}

		private void radioButtonMultipleCustomer_CheckedChanged(object sender, EventArgs e)
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
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonDefendant = new System.Windows.Forms.RadioButton();
			label3 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxToDefendantCaseClient = new Micromind.DataControls.CaseClientComboBox();
			comboBoxFromDefendantCaseClient = new Micromind.DataControls.CaseClientComboBox();
			comboBoxCaseClientSingle = new Micromind.DataControls.CaseClientComboBox();
			comboBoxToPlantiffCaseClient = new Micromind.DataControls.CaseClientComboBox();
			comboBoxFromPlantiffCaseClient = new Micromind.DataControls.CaseClientComboBox();
			radioButtonPlantiff = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)comboBoxToDefendantCaseClient).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromDefendantCaseClient).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCaseClientSingle).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToPlantiffCaseClient).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromPlantiffCaseClient).BeginInit();
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
			radioButtonAll.Size = new System.Drawing.Size(97, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Case Clients";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonDefendant.AutoSize = true;
			radioButtonDefendant.Location = new System.Drawing.Point(6, 28);
			radioButtonDefendant.Name = "radioButtonDefendant";
			radioButtonDefendant.Size = new System.Drawing.Size(78, 17);
			radioButtonDefendant.TabIndex = 3;
			radioButtonDefendant.Text = "Defendant:";
			radioButtonDefendant.UseVisualStyleBackColor = true;
			radioButtonDefendant.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(139, 30);
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
			comboBoxToDefendantCaseClient.Assigned = false;
			comboBoxToDefendantCaseClient.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToDefendantCaseClient.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToDefendantCaseClient.CustomReportFieldName = "";
			comboBoxToDefendantCaseClient.CustomReportKey = "";
			comboBoxToDefendantCaseClient.CustomReportValueType = 1;
			comboBoxToDefendantCaseClient.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToDefendantCaseClient.DisplayLayout.Appearance = appearance;
			comboBoxToDefendantCaseClient.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToDefendantCaseClient.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToDefendantCaseClient.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToDefendantCaseClient.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxToDefendantCaseClient.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToDefendantCaseClient.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxToDefendantCaseClient.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToDefendantCaseClient.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxToDefendantCaseClient.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToDefendantCaseClient.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxToDefendantCaseClient.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToDefendantCaseClient.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToDefendantCaseClient.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToDefendantCaseClient.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToDefendantCaseClient.Editable = true;
			comboBoxToDefendantCaseClient.FilterString = "";
			comboBoxToDefendantCaseClient.FilterSysDocID = "";
			comboBoxToDefendantCaseClient.HasAll = false;
			comboBoxToDefendantCaseClient.HasCustom = false;
			comboBoxToDefendantCaseClient.IsDataLoaded = false;
			comboBoxToDefendantCaseClient.Location = new System.Drawing.Point(330, 25);
			comboBoxToDefendantCaseClient.MaxDropDownItems = 12;
			comboBoxToDefendantCaseClient.Name = "comboBoxToDefendantCaseClient";
			comboBoxToDefendantCaseClient.ShowDefendant = true;
			comboBoxToDefendantCaseClient.ShowInactive = false;
			comboBoxToDefendantCaseClient.ShowPlantiff = false;
			comboBoxToDefendantCaseClient.ShowPROCustomersOnly = false;
			comboBoxToDefendantCaseClient.ShowQuickAdd = true;
			comboBoxToDefendantCaseClient.Size = new System.Drawing.Size(93, 20);
			comboBoxToDefendantCaseClient.TabIndex = 64;
			comboBoxToDefendantCaseClient.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromDefendantCaseClient.Assigned = false;
			comboBoxFromDefendantCaseClient.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromDefendantCaseClient.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromDefendantCaseClient.CustomReportFieldName = "";
			comboBoxFromDefendantCaseClient.CustomReportKey = "";
			comboBoxFromDefendantCaseClient.CustomReportValueType = 1;
			comboBoxFromDefendantCaseClient.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromDefendantCaseClient.DisplayLayout.Appearance = appearance13;
			comboBoxFromDefendantCaseClient.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromDefendantCaseClient.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromDefendantCaseClient.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromDefendantCaseClient.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxFromDefendantCaseClient.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromDefendantCaseClient.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxFromDefendantCaseClient.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromDefendantCaseClient.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromDefendantCaseClient.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxFromDefendantCaseClient.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromDefendantCaseClient.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromDefendantCaseClient.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromDefendantCaseClient.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromDefendantCaseClient.Editable = true;
			comboBoxFromDefendantCaseClient.FilterString = "";
			comboBoxFromDefendantCaseClient.FilterSysDocID = "";
			comboBoxFromDefendantCaseClient.HasAll = false;
			comboBoxFromDefendantCaseClient.HasCustom = false;
			comboBoxFromDefendantCaseClient.IsDataLoaded = false;
			comboBoxFromDefendantCaseClient.Location = new System.Drawing.Point(182, 25);
			comboBoxFromDefendantCaseClient.MaxDropDownItems = 12;
			comboBoxFromDefendantCaseClient.Name = "comboBoxFromDefendantCaseClient";
			comboBoxFromDefendantCaseClient.ShowDefendant = true;
			comboBoxFromDefendantCaseClient.ShowInactive = false;
			comboBoxFromDefendantCaseClient.ShowPlantiff = false;
			comboBoxFromDefendantCaseClient.ShowPROCustomersOnly = false;
			comboBoxFromDefendantCaseClient.ShowQuickAdd = true;
			comboBoxFromDefendantCaseClient.Size = new System.Drawing.Size(93, 20);
			comboBoxFromDefendantCaseClient.TabIndex = 63;
			comboBoxFromDefendantCaseClient.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCaseClientSingle.Assigned = false;
			comboBoxCaseClientSingle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCaseClientSingle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCaseClientSingle.CustomReportFieldName = "";
			comboBoxCaseClientSingle.CustomReportKey = "";
			comboBoxCaseClientSingle.CustomReportValueType = 1;
			comboBoxCaseClientSingle.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCaseClientSingle.DisplayLayout.Appearance = appearance25;
			comboBoxCaseClientSingle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCaseClientSingle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCaseClientSingle.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCaseClientSingle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxCaseClientSingle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCaseClientSingle.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxCaseClientSingle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCaseClientSingle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCaseClientSingle.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCaseClientSingle.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxCaseClientSingle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCaseClientSingle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCaseClientSingle.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCaseClientSingle.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxCaseClientSingle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCaseClientSingle.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCaseClientSingle.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxCaseClientSingle.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxCaseClientSingle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCaseClientSingle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxCaseClientSingle.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxCaseClientSingle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCaseClientSingle.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxCaseClientSingle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCaseClientSingle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCaseClientSingle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCaseClientSingle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCaseClientSingle.Editable = true;
			comboBoxCaseClientSingle.Enabled = false;
			comboBoxCaseClientSingle.FilterString = "";
			comboBoxCaseClientSingle.FilterSysDocID = "";
			comboBoxCaseClientSingle.HasAll = false;
			comboBoxCaseClientSingle.HasCustom = false;
			comboBoxCaseClientSingle.IsDataLoaded = false;
			comboBoxCaseClientSingle.Location = new System.Drawing.Point(182, 3);
			comboBoxCaseClientSingle.MaxDropDownItems = 12;
			comboBoxCaseClientSingle.Name = "comboBoxCaseClientSingle";
			comboBoxCaseClientSingle.ShowDefendant = false;
			comboBoxCaseClientSingle.ShowInactive = false;
			comboBoxCaseClientSingle.ShowPlantiff = false;
			comboBoxCaseClientSingle.ShowPROCustomersOnly = false;
			comboBoxCaseClientSingle.ShowQuickAdd = true;
			comboBoxCaseClientSingle.Size = new System.Drawing.Size(241, 20);
			comboBoxCaseClientSingle.TabIndex = 62;
			comboBoxCaseClientSingle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxToPlantiffCaseClient.Assigned = false;
			comboBoxToPlantiffCaseClient.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxToPlantiffCaseClient.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxToPlantiffCaseClient.CustomReportFieldName = "";
			comboBoxToPlantiffCaseClient.CustomReportKey = "";
			comboBoxToPlantiffCaseClient.CustomReportValueType = 1;
			comboBoxToPlantiffCaseClient.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxToPlantiffCaseClient.DisplayLayout.Appearance = appearance37;
			comboBoxToPlantiffCaseClient.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxToPlantiffCaseClient.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToPlantiffCaseClient.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToPlantiffCaseClient.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxToPlantiffCaseClient.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxToPlantiffCaseClient.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxToPlantiffCaseClient.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxToPlantiffCaseClient.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxToPlantiffCaseClient.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxToPlantiffCaseClient.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxToPlantiffCaseClient.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxToPlantiffCaseClient.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxToPlantiffCaseClient.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxToPlantiffCaseClient.Editable = true;
			comboBoxToPlantiffCaseClient.FilterString = "";
			comboBoxToPlantiffCaseClient.FilterSysDocID = "";
			comboBoxToPlantiffCaseClient.HasAll = false;
			comboBoxToPlantiffCaseClient.HasCustom = false;
			comboBoxToPlantiffCaseClient.IsDataLoaded = false;
			comboBoxToPlantiffCaseClient.Location = new System.Drawing.Point(330, 48);
			comboBoxToPlantiffCaseClient.MaxDropDownItems = 12;
			comboBoxToPlantiffCaseClient.Name = "comboBoxToPlantiffCaseClient";
			comboBoxToPlantiffCaseClient.ShowDefendant = false;
			comboBoxToPlantiffCaseClient.ShowInactive = false;
			comboBoxToPlantiffCaseClient.ShowPlantiff = true;
			comboBoxToPlantiffCaseClient.ShowPROCustomersOnly = false;
			comboBoxToPlantiffCaseClient.ShowQuickAdd = true;
			comboBoxToPlantiffCaseClient.Size = new System.Drawing.Size(93, 20);
			comboBoxToPlantiffCaseClient.TabIndex = 74;
			comboBoxToPlantiffCaseClient.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFromPlantiffCaseClient.Assigned = false;
			comboBoxFromPlantiffCaseClient.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFromPlantiffCaseClient.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFromPlantiffCaseClient.CustomReportFieldName = "";
			comboBoxFromPlantiffCaseClient.CustomReportKey = "";
			comboBoxFromPlantiffCaseClient.CustomReportValueType = 1;
			comboBoxFromPlantiffCaseClient.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Appearance = appearance49;
			comboBoxFromPlantiffCaseClient.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFromPlantiffCaseClient.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromPlantiffCaseClient.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromPlantiffCaseClient.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxFromPlantiffCaseClient.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFromPlantiffCaseClient.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxFromPlantiffCaseClient.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFromPlantiffCaseClient.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFromPlantiffCaseClient.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxFromPlantiffCaseClient.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFromPlantiffCaseClient.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFromPlantiffCaseClient.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFromPlantiffCaseClient.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFromPlantiffCaseClient.Editable = true;
			comboBoxFromPlantiffCaseClient.FilterString = "";
			comboBoxFromPlantiffCaseClient.FilterSysDocID = "";
			comboBoxFromPlantiffCaseClient.HasAll = false;
			comboBoxFromPlantiffCaseClient.HasCustom = false;
			comboBoxFromPlantiffCaseClient.IsDataLoaded = false;
			comboBoxFromPlantiffCaseClient.Location = new System.Drawing.Point(182, 48);
			comboBoxFromPlantiffCaseClient.MaxDropDownItems = 12;
			comboBoxFromPlantiffCaseClient.Name = "comboBoxFromPlantiffCaseClient";
			comboBoxFromPlantiffCaseClient.ShowDefendant = false;
			comboBoxFromPlantiffCaseClient.ShowInactive = false;
			comboBoxFromPlantiffCaseClient.ShowPlantiff = true;
			comboBoxFromPlantiffCaseClient.ShowPROCustomersOnly = false;
			comboBoxFromPlantiffCaseClient.ShowQuickAdd = true;
			comboBoxFromPlantiffCaseClient.Size = new System.Drawing.Size(93, 20);
			comboBoxFromPlantiffCaseClient.TabIndex = 73;
			comboBoxFromPlantiffCaseClient.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			radioButtonPlantiff.AutoSize = true;
			radioButtonPlantiff.Location = new System.Drawing.Point(6, 51);
			radioButtonPlantiff.Name = "radioButtonPlantiff";
			radioButtonPlantiff.Size = new System.Drawing.Size(60, 17);
			radioButtonPlantiff.TabIndex = 70;
			radioButtonPlantiff.Text = "Plantiff:";
			radioButtonPlantiff.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(139, 53);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(33, 13);
			label1.TabIndex = 71;
			label1.Text = "From:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(295, 53);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 72;
			label2.Text = "To:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxToPlantiffCaseClient);
			base.Controls.Add(comboBoxFromPlantiffCaseClient);
			base.Controls.Add(radioButtonPlantiff);
			base.Controls.Add(label1);
			base.Controls.Add(label2);
			base.Controls.Add(comboBoxToDefendantCaseClient);
			base.Controls.Add(comboBoxFromDefendantCaseClient);
			base.Controls.Add(comboBoxCaseClientSingle);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonDefendant);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "CaseClientSelector";
			base.Size = new System.Drawing.Size(428, 82);
			base.Load += new System.EventHandler(CustomerSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxToDefendantCaseClient).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromDefendantCaseClient).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCaseClientSingle).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxToPlantiffCaseClient).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFromPlantiffCaseClient).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
