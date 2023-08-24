using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CRMRelatedSelector : UserControl
	{
		private int isFlag;

		private IContainer components;

		private leadsFlatComboBox comboBoxLead;

		private OpportunityComboBox comboBoxOpportunity;

		private customersFlatComboBox comboBoxCustomers;

		private CRMRelatedTypesComboBox comboBoxRelatedType;

		public string SelectedName
		{
			get
			{
				if (comboBoxRelatedType.SelectedType == CRMRelatedTypes.Customer)
				{
					return comboBoxCustomers.SelectedName;
				}
				if (comboBoxRelatedType.SelectedType == CRMRelatedTypes.Opportunity)
				{
					return comboBoxOpportunity.SelectedName;
				}
				return comboBoxLead.SelectedName;
			}
		}

		public string SelectedID
		{
			get
			{
				if (comboBoxRelatedType.SelectedType == CRMRelatedTypes.Customer)
				{
					return comboBoxCustomers.SelectedID;
				}
				if (comboBoxRelatedType.SelectedType == CRMRelatedTypes.Opportunity)
				{
					return comboBoxOpportunity.SelectedID;
				}
				if (comboBoxRelatedType.SelectedType == CRMRelatedTypes.Lead)
				{
					return comboBoxLead.SelectedID;
				}
				return "";
			}
			set
			{
				if (SelectedType == CRMRelatedTypes.Customer)
				{
					comboBoxCustomers.SelectedID = value;
				}
				else if (SelectedType == CRMRelatedTypes.Opportunity)
				{
					comboBoxOpportunity.SelectedID = value;
				}
				else
				{
					comboBoxLead.SelectedID = value;
				}
			}
		}

		public int IsFlag
		{
			get
			{
				return isFlag;
			}
			set
			{
				isFlag = value;
			}
		}

		public CRMRelatedTypes SelectedType
		{
			get
			{
				return (CRMRelatedTypes)comboBoxRelatedType.SelectedID;
			}
			set
			{
				comboBoxRelatedType.SelectedID = (int)value;
			}
		}

		public event EventHandler SelectedItemChanged;

		public CRMRelatedSelector()
		{
			InitializeComponent();
			comboBoxRelatedType.ValueChanged += payeeTypeComboBox1_SelectedIndexChanged;
			comboBoxRelatedType.Resize += payeeTypeComboBox1_Resize;
			comboBoxRelatedType.DropDownStyle = DropDownStyle.DropDownList;
			comboBoxLead.SelectedIndexChanged += payee_SelectedIndexChanged;
			comboBoxCustomers.SelectedIndexChanged += payee_SelectedIndexChanged;
			comboBoxOpportunity.SelectedIndexChanged += payee_SelectedIndexChanged;
		}

		private void payee_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.SelectedItemChanged != null)
			{
				this.SelectedItemChanged(this, null);
			}
		}

		private void payeeTypeComboBox1_Resize(object sender, EventArgs e)
		{
		}

		private void payeeTypeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			customersFlatComboBox customersFlatComboBox = comboBoxCustomers;
			leadsFlatComboBox leadsFlatComboBox = comboBoxLead;
			bool flag2 = comboBoxOpportunity.Visible = false;
			bool visible = leadsFlatComboBox.Visible = flag2;
			customersFlatComboBox.Visible = visible;
			if (comboBoxRelatedType.SelectedType == CRMRelatedTypes.Opportunity)
			{
				comboBoxOpportunity.Visible = true;
			}
			else if (comboBoxRelatedType.SelectedType == CRMRelatedTypes.Customer)
			{
				comboBoxCustomers.Visible = true;
			}
			else
			{
				comboBoxLead.Visible = true;
			}
			if (this.SelectedItemChanged != null)
			{
				this.SelectedItemChanged(this, null);
			}
		}

		public void Clear()
		{
			comboBoxCustomers.Clear();
			comboBoxLead.Clear();
			comboBoxOpportunity.Clear();
			comboBoxRelatedType.SelectedType = CRMRelatedTypes.Lead;
		}

		public void CustomSelection()
		{
			comboBoxRelatedType.Enabled = false;
		}

		public void LoadData()
		{
			comboBoxRelatedType.LoadData();
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
			comboBoxRelatedType = new Micromind.DataControls.CRMRelatedTypesComboBox();
			comboBoxCustomers = new Micromind.DataControls.customersFlatComboBox();
			comboBoxOpportunity = new Micromind.DataControls.OpportunityComboBox();
			comboBoxLead = new Micromind.DataControls.leadsFlatComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxRelatedType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomers).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOpportunity).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLead).BeginInit();
			SuspendLayout();
			appearance.ForeColor = System.Drawing.Color.White;
			comboBoxRelatedType.Appearance = appearance;
			comboBoxRelatedType.DropDownListWidth = 100;
			comboBoxRelatedType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			comboBoxRelatedType.Location = new System.Drawing.Point(0, 0);
			comboBoxRelatedType.Name = "comboBoxRelatedType";
			comboBoxRelatedType.SelectedID = 1;
			comboBoxRelatedType.SelectedType = Micromind.Common.Data.CRMRelatedTypes.Lead;
			comboBoxRelatedType.Size = new System.Drawing.Size(39, 21);
			comboBoxRelatedType.TabIndex = 6;
			comboBoxCustomers.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			comboBoxCustomers.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCustomers.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomers.CustomReportFieldName = "";
			comboBoxCustomers.CustomReportKey = "";
			comboBoxCustomers.CustomReportValueType = 1;
			comboBoxCustomers.DescriptionTextBox = null;
			appearance2.BackColor = System.Drawing.SystemColors.Window;
			appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomers.DisplayLayout.Appearance = appearance2;
			comboBoxCustomers.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomers.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance3.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomers.DisplayLayout.GroupByBox.Appearance = appearance3;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomers.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
			comboBoxCustomers.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance5.BackColor2 = System.Drawing.SystemColors.Control;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomers.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
			comboBoxCustomers.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomers.DisplayLayout.MaxRowScrollRegions = 1;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomers.DisplayLayout.Override.ActiveCellAppearance = appearance6;
			appearance7.BackColor = System.Drawing.SystemColors.Highlight;
			appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomers.DisplayLayout.Override.ActiveRowAppearance = appearance7;
			comboBoxCustomers.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomers.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomers.DisplayLayout.Override.CardAreaAppearance = appearance8;
			appearance9.BorderColor = System.Drawing.Color.Silver;
			appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomers.DisplayLayout.Override.CellAppearance = appearance9;
			comboBoxCustomers.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomers.DisplayLayout.Override.CellPadding = 0;
			appearance10.BackColor = System.Drawing.SystemColors.Control;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomers.DisplayLayout.Override.GroupByRowAppearance = appearance10;
			appearance11.TextHAlignAsString = "Left";
			comboBoxCustomers.DisplayLayout.Override.HeaderAppearance = appearance11;
			comboBoxCustomers.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomers.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomers.DisplayLayout.Override.RowAppearance = appearance12;
			comboBoxCustomers.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomers.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
			comboBoxCustomers.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomers.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomers.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomers.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomers.Editable = true;
			comboBoxCustomers.FilterString = "";
			comboBoxCustomers.FilterSysDocID = "";
			comboBoxCustomers.HasAll = false;
			comboBoxCustomers.HasCustom = false;
			comboBoxCustomers.IsDataLoaded = false;
			comboBoxCustomers.Location = new System.Drawing.Point(40, 0);
			comboBoxCustomers.MaxDropDownItems = 12;
			comboBoxCustomers.Name = "comboBoxCustomers";
			comboBoxCustomers.ShowConsignmentOnly = false;
			comboBoxCustomers.ShowLPOCustomersOnly = false;
			comboBoxCustomers.ShowPROCustomersOnly = false;
			comboBoxCustomers.ShowQuickAdd = true;
			comboBoxCustomers.Size = new System.Drawing.Size(156, 20);
			comboBoxCustomers.TabIndex = 5;
			comboBoxCustomers.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCustomers.Visible = false;
			comboBoxOpportunity.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			comboBoxOpportunity.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxOpportunity.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxOpportunity.CustomReportFieldName = "";
			comboBoxOpportunity.CustomReportKey = "";
			comboBoxOpportunity.CustomReportValueType = 1;
			comboBoxOpportunity.DescriptionTextBox = null;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxOpportunity.DisplayLayout.Appearance = appearance14;
			comboBoxOpportunity.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxOpportunity.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance15.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance15.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance15.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOpportunity.DisplayLayout.GroupByBox.Appearance = appearance15;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOpportunity.DisplayLayout.GroupByBox.BandLabelAppearance = appearance16;
			comboBoxOpportunity.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance17.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance17.BackColor2 = System.Drawing.SystemColors.Control;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxOpportunity.DisplayLayout.GroupByBox.PromptAppearance = appearance17;
			comboBoxOpportunity.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxOpportunity.DisplayLayout.MaxRowScrollRegions = 1;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxOpportunity.DisplayLayout.Override.ActiveCellAppearance = appearance18;
			appearance19.BackColor = System.Drawing.SystemColors.Highlight;
			appearance19.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxOpportunity.DisplayLayout.Override.ActiveRowAppearance = appearance19;
			comboBoxOpportunity.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxOpportunity.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			comboBoxOpportunity.DisplayLayout.Override.CardAreaAppearance = appearance20;
			appearance21.BorderColor = System.Drawing.Color.Silver;
			appearance21.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxOpportunity.DisplayLayout.Override.CellAppearance = appearance21;
			comboBoxOpportunity.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxOpportunity.DisplayLayout.Override.CellPadding = 0;
			appearance22.BackColor = System.Drawing.SystemColors.Control;
			appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance22.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance22.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxOpportunity.DisplayLayout.Override.GroupByRowAppearance = appearance22;
			appearance23.TextHAlignAsString = "Left";
			comboBoxOpportunity.DisplayLayout.Override.HeaderAppearance = appearance23;
			comboBoxOpportunity.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxOpportunity.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			comboBoxOpportunity.DisplayLayout.Override.RowAppearance = appearance24;
			comboBoxOpportunity.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance25.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxOpportunity.DisplayLayout.Override.TemplateAddRowAppearance = appearance25;
			comboBoxOpportunity.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxOpportunity.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxOpportunity.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxOpportunity.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxOpportunity.Editable = true;
			comboBoxOpportunity.FilterString = "";
			comboBoxOpportunity.HasAllAccount = false;
			comboBoxOpportunity.HasCustom = false;
			comboBoxOpportunity.IsDataLoaded = false;
			comboBoxOpportunity.Location = new System.Drawing.Point(40, 0);
			comboBoxOpportunity.MaxDropDownItems = 12;
			comboBoxOpportunity.Name = "comboBoxOpportunity";
			comboBoxOpportunity.ShowInactiveItems = false;
			comboBoxOpportunity.ShowQuickAdd = true;
			comboBoxOpportunity.Size = new System.Drawing.Size(156, 20);
			comboBoxOpportunity.TabIndex = 4;
			comboBoxOpportunity.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxOpportunity.Visible = false;
			comboBoxLead.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			comboBoxLead.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLead.CustomReportFieldName = "";
			comboBoxLead.CustomReportKey = "";
			comboBoxLead.CustomReportValueType = 1;
			comboBoxLead.DescriptionTextBox = null;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLead.DisplayLayout.Appearance = appearance26;
			comboBoxLead.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLead.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance27.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance27.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance27.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLead.DisplayLayout.GroupByBox.Appearance = appearance27;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLead.DisplayLayout.GroupByBox.BandLabelAppearance = appearance28;
			comboBoxLead.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance29.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance29.BackColor2 = System.Drawing.SystemColors.Control;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLead.DisplayLayout.GroupByBox.PromptAppearance = appearance29;
			comboBoxLead.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLead.DisplayLayout.MaxRowScrollRegions = 1;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			appearance30.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLead.DisplayLayout.Override.ActiveCellAppearance = appearance30;
			appearance31.BackColor = System.Drawing.SystemColors.Highlight;
			appearance31.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLead.DisplayLayout.Override.ActiveRowAppearance = appearance31;
			comboBoxLead.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLead.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLead.DisplayLayout.Override.CardAreaAppearance = appearance32;
			appearance33.BorderColor = System.Drawing.Color.Silver;
			appearance33.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLead.DisplayLayout.Override.CellAppearance = appearance33;
			comboBoxLead.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLead.DisplayLayout.Override.CellPadding = 0;
			appearance34.BackColor = System.Drawing.SystemColors.Control;
			appearance34.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance34.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLead.DisplayLayout.Override.GroupByRowAppearance = appearance34;
			appearance35.TextHAlignAsString = "Left";
			comboBoxLead.DisplayLayout.Override.HeaderAppearance = appearance35;
			comboBoxLead.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLead.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			comboBoxLead.DisplayLayout.Override.RowAppearance = appearance36;
			comboBoxLead.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance37.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLead.DisplayLayout.Override.TemplateAddRowAppearance = appearance37;
			comboBoxLead.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLead.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLead.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLead.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLead.Editable = true;
			comboBoxLead.FilterString = "";
			comboBoxLead.HasAll = false;
			comboBoxLead.HasCustom = false;
			comboBoxLead.IsDataLoaded = false;
			comboBoxLead.Location = new System.Drawing.Point(40, 0);
			comboBoxLead.MaxDropDownItems = 12;
			comboBoxLead.Name = "comboBoxLead";
			comboBoxLead.ShowInactiveItems = false;
			comboBoxLead.ShowQuickAdd = true;
			comboBoxLead.Size = new System.Drawing.Size(156, 20);
			comboBoxLead.TabIndex = 3;
			comboBoxLead.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLead.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxRelatedType);
			base.Controls.Add(comboBoxCustomers);
			base.Controls.Add(comboBoxOpportunity);
			base.Controls.Add(comboBoxLead);
			MaximumSize = new System.Drawing.Size(2000, 20);
			MinimumSize = new System.Drawing.Size(0, 20);
			base.Name = "CRMRelatedSelector";
			base.Size = new System.Drawing.Size(199, 20);
			((System.ComponentModel.ISupportInitialize)comboBoxRelatedType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomers).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxOpportunity).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLead).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
