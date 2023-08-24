using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class SysDocAccountsForm : Form, IForm
	{
		private SystemDocumentData currentData;

		private const string TABLENAME_CONST = "System_Document";

		private const string IDFIELD_CONST = "SysDocID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonSave;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private FormManager formManager;

		private AllAccountsComboBox comboBoxDiscountGiven;

		private MMTextBox textBoxDiscountGivenAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private AllAccountsComboBox comboBoxSalesTax;

		private MMTextBox textBoxSalesTaxAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private AllAccountsComboBox comboBoxCOGSAccount;

		private MMTextBox textBoxGOGSAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private AllAccountsComboBox comboBoxSalesAccount;

		private MMTextBox textBoxSalesAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6008;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public string ItemCode
		{
			get
			{
				return textBoxCode.Text;
			}
			set
			{
				textBoxCode.Text = value;
			}
		}

		public string ItemName
		{
			get
			{
				return textBoxName.Text;
			}
			set
			{
				textBoxName.Text = value;
			}
		}

		public bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public SysDocAccountsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += SysDocAccountsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null)
				{
					currentData = new SystemDocumentData();
				}
				DataRow dataRow = currentData.SystemDocumentTable.Rows[0];
				dataRow.BeginEdit();
				if (comboBoxSalesAccount.SelectedID != "")
				{
					dataRow["SalesAccountID"] = comboBoxSalesAccount.SelectedID;
				}
				else
				{
					dataRow["SalesAccountID"] = DBNull.Value;
				}
				if (comboBoxCOGSAccount.SelectedID != "")
				{
					dataRow["COGSAccountID"] = comboBoxCOGSAccount.SelectedID;
				}
				else
				{
					dataRow["COGSAccountID"] = DBNull.Value;
				}
				if (comboBoxDiscountGiven.SelectedID != "")
				{
					dataRow["DiscountGivenAccountID"] = comboBoxDiscountGiven.SelectedID;
				}
				else
				{
					dataRow["DiscountGivenAccountID"] = DBNull.Value;
				}
				if (comboBoxSalesTax.SelectedID != "")
				{
					dataRow["SalesTaxAccountID"] = comboBoxSalesTax.SelectedID;
				}
				else
				{
					dataRow["SalesTaxAccountID"] = DBNull.Value;
				}
				dataRow.EndEdit();
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxCode.Focus();
		}

		public void LoadData(SystemDocumentData data)
		{
			try
			{
				currentData = data;
				if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
				{
					textBoxCode.Focus();
				}
				else
				{
					FillData();
					formManager.ResetDirty();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables[0].Rows[0];
				comboBoxSalesAccount.SelectedID = dataRow["SalesAccountID"].ToString();
				comboBoxCOGSAccount.SelectedID = dataRow["COGSAccountID"].ToString();
				comboBoxDiscountGiven.SelectedID = dataRow["DiscountGivenAccountID"].ToString();
				comboBoxSalesTax.SelectedID = dataRow["SalesTaxAccountID"].ToString();
			}
		}

		private bool SaveData()
		{
			if (!GetData())
			{
				return false;
			}
			try
			{
				Close();
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (!screenRight.New && isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionNew);
				return false;
			}
			if (!screenRight.Edit && !isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			return true;
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				ClearForm();
			}
			else if (SaveData())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private void ClearForm()
		{
			textBoxCode.Clear();
			textBoxName.Clear();
			comboBoxSalesTax.Clear();
			comboBoxSalesAccount.Clear();
			comboBoxCOGSAccount.Clear();
			comboBoxDiscountGiven.Clear();
			textBoxDiscountGivenAccountName.Clear();
			textBoxGOGSAccountName.Clear();
			textBoxSalesAccountName.Clear();
			textBoxSalesTaxAccountName.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void LocationGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void LocationGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DocumentNumberInUse) == DialogResult.No)
				{
					return false;
				}
				return Factory.LocationSystem.DeleteLocation(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
		{
			return true;
		}

		private void SysDocAccountsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				_ = base.IsDisposed;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Location);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxSalesAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxCOGSAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxSalesTax.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxDiscountGiven.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.SysDocAccountsForm));
			comboBoxDiscountGiven = new Micromind.DataControls.AllAccountsComboBox();
			textBoxDiscountGivenAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSalesTax = new Micromind.DataControls.AllAccountsComboBox();
			textBoxSalesTaxAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCOGSAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxGOGSAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSalesAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxSalesAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountGiven).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesTax).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGSAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesAccount).BeginInit();
			panelButtons.SuspendLayout();
			SuspendLayout();
			comboBoxDiscountGiven.AlwaysInEditMode = true;
			comboBoxDiscountGiven.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDiscountGiven.CustomReportFieldName = "";
			comboBoxDiscountGiven.CustomReportKey = "";
			comboBoxDiscountGiven.CustomReportValueType = 1;
			comboBoxDiscountGiven.DescriptionTextBox = textBoxDiscountGivenAccountName;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDiscountGiven.DisplayLayout.Appearance = appearance;
			comboBoxDiscountGiven.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDiscountGiven.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxDiscountGiven.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDiscountGiven.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDiscountGiven.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDiscountGiven.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxDiscountGiven.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDiscountGiven.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDiscountGiven.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxDiscountGiven.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDiscountGiven.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxDiscountGiven.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxDiscountGiven.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDiscountGiven.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxDiscountGiven.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDiscountGiven.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDiscountGiven.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDiscountGiven.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDiscountGiven.Editable = true;
			comboBoxDiscountGiven.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxDiscountGiven.FilterString = "";
			comboBoxDiscountGiven.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxDiscountGiven.FilterSysDocID = "";
			comboBoxDiscountGiven.HasAllAccount = false;
			comboBoxDiscountGiven.HasCustom = false;
			comboBoxDiscountGiven.IsDataLoaded = false;
			comboBoxDiscountGiven.Location = new System.Drawing.Point(123, 148);
			comboBoxDiscountGiven.MaxDropDownItems = 12;
			comboBoxDiscountGiven.Name = "comboBoxDiscountGiven";
			comboBoxDiscountGiven.ShowInactiveItems = false;
			comboBoxDiscountGiven.ShowQuickAdd = true;
			comboBoxDiscountGiven.Size = new System.Drawing.Size(117, 21);
			comboBoxDiscountGiven.TabIndex = 8;
			comboBoxDiscountGiven.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxDiscountGivenAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDiscountGivenAccountName.CustomReportFieldName = "";
			textBoxDiscountGivenAccountName.CustomReportKey = "";
			textBoxDiscountGivenAccountName.CustomReportValueType = 1;
			textBoxDiscountGivenAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxDiscountGivenAccountName.IsComboTextBox = false;
			textBoxDiscountGivenAccountName.Location = new System.Drawing.Point(242, 148);
			textBoxDiscountGivenAccountName.MaxLength = 255;
			textBoxDiscountGivenAccountName.Name = "textBoxDiscountGivenAccountName";
			textBoxDiscountGivenAccountName.ReadOnly = true;
			textBoxDiscountGivenAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxDiscountGivenAccountName.TabIndex = 9;
			textBoxDiscountGivenAccountName.TabStop = false;
			appearance13.FontData.BoldAsString = "False";
			appearance13.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance13;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(11, 79);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(34, 15);
			ultraFormattedLinkLabel2.TabIndex = 157;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Sales:";
			appearance14.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance14;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			comboBoxSalesTax.AlwaysInEditMode = true;
			comboBoxSalesTax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesTax.CustomReportFieldName = "";
			comboBoxSalesTax.CustomReportKey = "";
			comboBoxSalesTax.CustomReportValueType = 1;
			comboBoxSalesTax.DescriptionTextBox = textBoxSalesTaxAccountName;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesTax.DisplayLayout.Appearance = appearance15;
			comboBoxSalesTax.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesTax.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesTax.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxSalesTax.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesTax.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxSalesTax.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesTax.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesTax.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesTax.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxSalesTax.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesTax.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesTax.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxSalesTax.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesTax.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxSalesTax.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxSalesTax.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesTax.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesTax.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxSalesTax.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesTax.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			comboBoxSalesTax.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesTax.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesTax.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesTax.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesTax.Editable = true;
			comboBoxSalesTax.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxSalesTax.FilterString = "";
			comboBoxSalesTax.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxSalesTax.FilterSysDocID = "";
			comboBoxSalesTax.HasAllAccount = false;
			comboBoxSalesTax.HasCustom = false;
			comboBoxSalesTax.IsDataLoaded = false;
			comboBoxSalesTax.Location = new System.Drawing.Point(123, 125);
			comboBoxSalesTax.MaxDropDownItems = 12;
			comboBoxSalesTax.Name = "comboBoxSalesTax";
			comboBoxSalesTax.ShowInactiveItems = false;
			comboBoxSalesTax.ShowQuickAdd = true;
			comboBoxSalesTax.Size = new System.Drawing.Size(117, 21);
			comboBoxSalesTax.TabIndex = 6;
			comboBoxSalesTax.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxSalesTaxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSalesTaxAccountName.CustomReportFieldName = "";
			textBoxSalesTaxAccountName.CustomReportKey = "";
			textBoxSalesTaxAccountName.CustomReportValueType = 1;
			textBoxSalesTaxAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxSalesTaxAccountName.IsComboTextBox = false;
			textBoxSalesTaxAccountName.Location = new System.Drawing.Point(242, 125);
			textBoxSalesTaxAccountName.MaxLength = 255;
			textBoxSalesTaxAccountName.Name = "textBoxSalesTaxAccountName";
			textBoxSalesTaxAccountName.ReadOnly = true;
			textBoxSalesTaxAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxSalesTaxAccountName.TabIndex = 7;
			textBoxSalesTaxAccountName.TabStop = false;
			appearance27.FontData.BoldAsString = "False";
			appearance27.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance27;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(11, 102);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(36, 15);
			ultraFormattedLinkLabel3.TabIndex = 161;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "COGS:";
			appearance28.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance28;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			comboBoxCOGSAccount.AlwaysInEditMode = true;
			comboBoxCOGSAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCOGSAccount.CustomReportFieldName = "";
			comboBoxCOGSAccount.CustomReportKey = "";
			comboBoxCOGSAccount.CustomReportValueType = 1;
			comboBoxCOGSAccount.DescriptionTextBox = textBoxGOGSAccountName;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCOGSAccount.DisplayLayout.Appearance = appearance29;
			comboBoxCOGSAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCOGSAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxCOGSAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCOGSAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCOGSAccount.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCOGSAccount.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxCOGSAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCOGSAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCOGSAccount.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxCOGSAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCOGSAccount.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxCOGSAccount.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxCOGSAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCOGSAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxCOGSAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCOGSAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCOGSAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCOGSAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCOGSAccount.Editable = true;
			comboBoxCOGSAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxCOGSAccount.FilterString = "";
			comboBoxCOGSAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxCOGSAccount.FilterSysDocID = "";
			comboBoxCOGSAccount.HasAllAccount = false;
			comboBoxCOGSAccount.HasCustom = false;
			comboBoxCOGSAccount.IsDataLoaded = false;
			comboBoxCOGSAccount.Location = new System.Drawing.Point(123, 102);
			comboBoxCOGSAccount.MaxDropDownItems = 12;
			comboBoxCOGSAccount.Name = "comboBoxCOGSAccount";
			comboBoxCOGSAccount.ShowInactiveItems = false;
			comboBoxCOGSAccount.ShowQuickAdd = true;
			comboBoxCOGSAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxCOGSAccount.TabIndex = 4;
			comboBoxCOGSAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxGOGSAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGOGSAccountName.CustomReportFieldName = "";
			textBoxGOGSAccountName.CustomReportKey = "";
			textBoxGOGSAccountName.CustomReportValueType = 1;
			textBoxGOGSAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxGOGSAccountName.IsComboTextBox = false;
			textBoxGOGSAccountName.Location = new System.Drawing.Point(242, 102);
			textBoxGOGSAccountName.MaxLength = 255;
			textBoxGOGSAccountName.Name = "textBoxGOGSAccountName";
			textBoxGOGSAccountName.ReadOnly = true;
			textBoxGOGSAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxGOGSAccountName.TabIndex = 5;
			textBoxGOGSAccountName.TabStop = false;
			appearance41.FontData.BoldAsString = "False";
			appearance41.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance41;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(11, 125);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(55, 15);
			ultraFormattedLinkLabel4.TabIndex = 158;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Sales Tax:";
			appearance42.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance42;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxSalesAccount.AlwaysInEditMode = true;
			comboBoxSalesAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesAccount.CustomReportFieldName = "";
			comboBoxSalesAccount.CustomReportKey = "";
			comboBoxSalesAccount.CustomReportValueType = 1;
			comboBoxSalesAccount.DescriptionTextBox = textBoxSalesAccountName;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesAccount.DisplayLayout.Appearance = appearance43;
			comboBoxSalesAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance44.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance44.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.Appearance = appearance44;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance45;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance46.BackColor2 = System.Drawing.SystemColors.Control;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance46;
			comboBoxSalesAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesAccount.DisplayLayout.Override.ActiveCellAppearance = appearance47;
			appearance48.BackColor = System.Drawing.SystemColors.Highlight;
			appearance48.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesAccount.DisplayLayout.Override.ActiveRowAppearance = appearance48;
			comboBoxSalesAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.Override.CardAreaAppearance = appearance49;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesAccount.DisplayLayout.Override.CellAppearance = appearance50;
			comboBoxSalesAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesAccount.DisplayLayout.Override.CellPadding = 0;
			appearance51.BackColor = System.Drawing.SystemColors.Control;
			appearance51.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance51.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance51.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.Override.GroupByRowAppearance = appearance51;
			appearance52.TextHAlignAsString = "Left";
			comboBoxSalesAccount.DisplayLayout.Override.HeaderAppearance = appearance52;
			comboBoxSalesAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesAccount.DisplayLayout.Override.RowAppearance = appearance53;
			comboBoxSalesAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance54;
			comboBoxSalesAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesAccount.Editable = true;
			comboBoxSalesAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxSalesAccount.FilterString = "";
			comboBoxSalesAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxSalesAccount.FilterSysDocID = "";
			comboBoxSalesAccount.HasAllAccount = false;
			comboBoxSalesAccount.HasCustom = false;
			comboBoxSalesAccount.IsDataLoaded = false;
			comboBoxSalesAccount.Location = new System.Drawing.Point(123, 79);
			comboBoxSalesAccount.MaxDropDownItems = 12;
			comboBoxSalesAccount.Name = "comboBoxSalesAccount";
			comboBoxSalesAccount.ShowInactiveItems = false;
			comboBoxSalesAccount.ShowQuickAdd = true;
			comboBoxSalesAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxSalesAccount.TabIndex = 2;
			comboBoxSalesAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxSalesAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSalesAccountName.CustomReportFieldName = "";
			textBoxSalesAccountName.CustomReportKey = "";
			textBoxSalesAccountName.CustomReportValueType = 1;
			textBoxSalesAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxSalesAccountName.IsComboTextBox = false;
			textBoxSalesAccountName.Location = new System.Drawing.Point(242, 79);
			textBoxSalesAccountName.MaxLength = 255;
			textBoxSalesAccountName.Name = "textBoxSalesAccountName";
			textBoxSalesAccountName.ReadOnly = true;
			textBoxSalesAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxSalesAccountName.TabIndex = 3;
			textBoxSalesAccountName.TabStop = false;
			appearance55.FontData.BoldAsString = "False";
			appearance55.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance55;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(11, 148);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(81, 15);
			ultraFormattedLinkLabel5.TabIndex = 159;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Discount Given:";
			appearance56.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance56;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 209);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(604, 40);
			panelButtons.TabIndex = 10;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(604, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(497, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&OK";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(123, 30);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.ReadOnly = true;
			textBoxName.Size = new System.Drawing.Size(422, 21);
			textBoxName.TabIndex = 1;
			textBoxName.TabStop = false;
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(123, 8);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(139, 21);
			textBoxCode.TabIndex = 0;
			textBoxCode.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 30);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(70, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Doc Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 8);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(51, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Doc ID:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(604, 249);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(comboBoxSalesAccount);
			base.Controls.Add(textBoxDiscountGivenAccountName);
			base.Controls.Add(textBoxSalesTaxAccountName);
			base.Controls.Add(comboBoxDiscountGiven);
			base.Controls.Add(textBoxGOGSAccountName);
			base.Controls.Add(ultraFormattedLinkLabel2);
			base.Controls.Add(ultraFormattedLinkLabel5);
			base.Controls.Add(textBoxSalesAccountName);
			base.Controls.Add(comboBoxSalesTax);
			base.Controls.Add(ultraFormattedLinkLabel4);
			base.Controls.Add(ultraFormattedLinkLabel3);
			base.Controls.Add(comboBoxCOGSAccount);
			Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(620, 160);
			base.Name = "SysDocAccountsForm";
			Text = "System Documents Accounts";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountGiven).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesTax).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGSAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesAccount).EndInit();
			panelButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
