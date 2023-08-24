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
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class InventoryAccountsForm : Form, IForm
	{
		private ItemTypes itemType = ItemTypes.Inventory;

		private LocationData currentData;

		private const string TABLENAME_CONST = "Location";

		private const string IDFIELD_CONST = "LocationID";

		private bool isNewRecord = true;

		private bool isDirty;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonSave;

		private FormManager formManager;

		private MMTextBox textBoxCOGSAccount;

		private MMTextBox textBoxAssetAccount;

		private MMTextBox textBoxIncomeAccount;

		private UltraFormattedLinkLabel linkLabelCOGSAccount;

		private UltraFormattedLinkLabel linkLabelAssetAccount;

		private AllAccountsComboBox comboBoxAssetAccount;

		private AllAccountsComboBox comboBoxCOGS;

		private UltraFormattedLinkLabel linkLabelIncomeAccount;

		private AllAccountsComboBox comboBoxIncomeAccount;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6008;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public ItemTypes ItemType
		{
			get
			{
				return itemType;
			}
			set
			{
				itemType = value;
				switch (value)
				{
				case ItemTypes.Inventory:
					linkLabelAssetAccount.Visible = true;
					comboBoxAssetAccount.Visible = true;
					textBoxAssetAccount.Visible = true;
					linkLabelCOGSAccount.Value = "COGS Account:";
					linkLabelIncomeAccount.Value = "Income Account:";
					break;
				case ItemTypes.NonInventory:
				case ItemTypes.Service:
				case ItemTypes.Discount:
					linkLabelAssetAccount.Visible = false;
					comboBoxAssetAccount.Visible = false;
					textBoxAssetAccount.Visible = false;
					if (value == ItemTypes.Discount)
					{
						linkLabelCOGSAccount.Value = "Discount Received:";
						linkLabelIncomeAccount.Value = "Discount Given:";
					}
					else
					{
						linkLabelCOGSAccount.Value = "Purchase Account:";
						linkLabelIncomeAccount.Value = "Sales Account:";
					}
					break;
				case ItemTypes.ProjectFee:
					linkLabelAssetAccount.Visible = false;
					comboBoxAssetAccount.Visible = false;
					textBoxAssetAccount.Visible = false;
					comboBoxCOGS.Visible = false;
					linkLabelCOGSAccount.Visible = false;
					textBoxCOGSAccount.Visible = false;
					linkLabelIncomeAccount.Value = "Sales:";
					break;
				}
			}
		}

		public string IncomeAccount
		{
			get
			{
				return comboBoxIncomeAccount.SelectedID;
			}
			set
			{
				comboBoxIncomeAccount.SelectedID = value;
			}
		}

		public string COGSAccount
		{
			get
			{
				return comboBoxCOGS.SelectedID;
			}
			set
			{
				comboBoxCOGS.SelectedID = value;
			}
		}

		public string AssetAccount
		{
			get
			{
				return comboBoxAssetAccount.SelectedID;
			}
			set
			{
				comboBoxAssetAccount.SelectedID = value;
			}
		}

		public bool IsDirty => isDirty;

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

		public InventoryAccountsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += InventoryAccountsForm_Load;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		public void LoadData(LocationData data)
		{
			try
			{
				currentData = data;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					formManager.ResetDirty();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private bool SaveData()
		{
			try
			{
				isDirty = formManager.GetDirtyStatus();
				base.DialogResult = DialogResult.OK;
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
			formManager.ResetDirty();
		}

		private void LocationGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void LocationGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
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

		private void InventoryAccountsForm_Load(object sender, EventArgs e)
		{
			try
			{
				if (!base.IsDisposed)
				{
					formManager.ResetDirty();
				}
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

		private void linkLabelIncomeAccount_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxIncomeAccount.SelectedID);
		}

		private void linkLabelCOGSAccount_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxCOGS.SelectedID);
		}

		private void linkLabelAssetAccount_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAssetAccount.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.InventoryAccountsForm));
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonSave = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			textBoxCOGSAccount = new Micromind.UISupport.MMTextBox();
			textBoxAssetAccount = new Micromind.UISupport.MMTextBox();
			textBoxIncomeAccount = new Micromind.UISupport.MMTextBox();
			linkLabelCOGSAccount = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			linkLabelAssetAccount = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxAssetAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxCOGS = new Micromind.DataControls.AllAccountsComboBox();
			linkLabelIncomeAccount = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxIncomeAccount = new Micromind.DataControls.AllAccountsComboBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAssetAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGS).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxIncomeAccount).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 111);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(559, 33);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(559, 1);
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
			buttonSave.Location = new System.Drawing.Point(452, 5);
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
			textBoxCOGSAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCOGSAccount.CustomReportFieldName = "";
			textBoxCOGSAccount.CustomReportKey = "";
			textBoxCOGSAccount.CustomReportValueType = 1;
			textBoxCOGSAccount.IsComboTextBox = false;
			textBoxCOGSAccount.Location = new System.Drawing.Point(246, 34);
			textBoxCOGSAccount.MaxLength = 255;
			textBoxCOGSAccount.Name = "textBoxCOGSAccount";
			textBoxCOGSAccount.ReadOnly = true;
			textBoxCOGSAccount.Size = new System.Drawing.Size(297, 21);
			textBoxCOGSAccount.TabIndex = 6;
			textBoxCOGSAccount.TabStop = false;
			textBoxAssetAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAssetAccount.CustomReportFieldName = "";
			textBoxAssetAccount.CustomReportKey = "";
			textBoxAssetAccount.CustomReportValueType = 1;
			textBoxAssetAccount.IsComboTextBox = false;
			textBoxAssetAccount.Location = new System.Drawing.Point(246, 57);
			textBoxAssetAccount.MaxLength = 255;
			textBoxAssetAccount.Name = "textBoxAssetAccount";
			textBoxAssetAccount.ReadOnly = true;
			textBoxAssetAccount.Size = new System.Drawing.Size(297, 21);
			textBoxAssetAccount.TabIndex = 6;
			textBoxAssetAccount.TabStop = false;
			textBoxIncomeAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxIncomeAccount.CustomReportFieldName = "";
			textBoxIncomeAccount.CustomReportKey = "";
			textBoxIncomeAccount.CustomReportValueType = 1;
			textBoxIncomeAccount.IsComboTextBox = false;
			textBoxIncomeAccount.Location = new System.Drawing.Point(246, 12);
			textBoxIncomeAccount.MaxLength = 255;
			textBoxIncomeAccount.Name = "textBoxIncomeAccount";
			textBoxIncomeAccount.ReadOnly = true;
			textBoxIncomeAccount.Size = new System.Drawing.Size(297, 21);
			textBoxIncomeAccount.TabIndex = 6;
			textBoxIncomeAccount.TabStop = false;
			linkLabelCOGSAccount.AutoSize = true;
			linkLabelCOGSAccount.Location = new System.Drawing.Point(5, 36);
			linkLabelCOGSAccount.Name = "linkLabelCOGSAccount";
			linkLabelCOGSAccount.Size = new System.Drawing.Size(80, 15);
			linkLabelCOGSAccount.TabIndex = 4;
			linkLabelCOGSAccount.TabStop = true;
			linkLabelCOGSAccount.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelCOGSAccount.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelCOGSAccount.Value = "COGS Account:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			linkLabelCOGSAccount.VisitedLinkAppearance = appearance;
			linkLabelCOGSAccount.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelCOGSAccount_LinkClicked);
			linkLabelAssetAccount.AutoSize = true;
			linkLabelAssetAccount.Location = new System.Drawing.Point(5, 58);
			linkLabelAssetAccount.Name = "linkLabelAssetAccount";
			linkLabelAssetAccount.Size = new System.Drawing.Size(76, 15);
			linkLabelAssetAccount.TabIndex = 2;
			linkLabelAssetAccount.TabStop = true;
			linkLabelAssetAccount.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelAssetAccount.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelAssetAccount.Value = "Asset Account:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			linkLabelAssetAccount.VisitedLinkAppearance = appearance2;
			linkLabelAssetAccount.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelAssetAccount_LinkClicked);
			comboBoxAssetAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAssetAccount.CustomReportFieldName = "";
			comboBoxAssetAccount.CustomReportKey = "";
			comboBoxAssetAccount.CustomReportValueType = 1;
			comboBoxAssetAccount.DescriptionTextBox = textBoxAssetAccount;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAssetAccount.DisplayLayout.Appearance = appearance3;
			comboBoxAssetAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAssetAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAssetAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxAssetAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAssetAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAssetAccount.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAssetAccount.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxAssetAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAssetAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAssetAccount.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAssetAccount.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxAssetAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAssetAccount.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAssetAccount.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxAssetAccount.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxAssetAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAssetAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxAssetAccount.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxAssetAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAssetAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			comboBoxAssetAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAssetAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAssetAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAssetAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAssetAccount.Editable = true;
			comboBoxAssetAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAssetAccount.FilterString = "";
			comboBoxAssetAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAssetAccount.FilterSysDocID = "";
			comboBoxAssetAccount.HasAllAccount = false;
			comboBoxAssetAccount.HasCustom = false;
			comboBoxAssetAccount.IsDataLoaded = false;
			comboBoxAssetAccount.Location = new System.Drawing.Point(106, 57);
			comboBoxAssetAccount.MaxDropDownItems = 12;
			comboBoxAssetAccount.MaxLength = 64;
			comboBoxAssetAccount.Name = "comboBoxAssetAccount";
			comboBoxAssetAccount.ShowInactiveItems = false;
			comboBoxAssetAccount.ShowQuickAdd = true;
			comboBoxAssetAccount.Size = new System.Drawing.Size(136, 21);
			comboBoxAssetAccount.TabIndex = 2;
			comboBoxAssetAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCOGS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCOGS.CustomReportFieldName = "";
			comboBoxCOGS.CustomReportKey = "";
			comboBoxCOGS.CustomReportValueType = 1;
			comboBoxCOGS.DescriptionTextBox = textBoxCOGSAccount;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCOGS.DisplayLayout.Appearance = appearance15;
			comboBoxCOGS.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCOGS.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGS.DisplayLayout.GroupByBox.Appearance = appearance16;
			appearance17.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGS.DisplayLayout.GroupByBox.BandLabelAppearance = appearance17;
			comboBoxCOGS.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance18.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance18.BackColor2 = System.Drawing.SystemColors.Control;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGS.DisplayLayout.GroupByBox.PromptAppearance = appearance18;
			comboBoxCOGS.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCOGS.DisplayLayout.MaxRowScrollRegions = 1;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCOGS.DisplayLayout.Override.ActiveCellAppearance = appearance19;
			appearance20.BackColor = System.Drawing.SystemColors.Highlight;
			appearance20.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCOGS.DisplayLayout.Override.ActiveRowAppearance = appearance20;
			comboBoxCOGS.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCOGS.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCOGS.DisplayLayout.Override.CardAreaAppearance = appearance21;
			appearance22.BorderColor = System.Drawing.Color.Silver;
			appearance22.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCOGS.DisplayLayout.Override.CellAppearance = appearance22;
			comboBoxCOGS.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCOGS.DisplayLayout.Override.CellPadding = 0;
			appearance23.BackColor = System.Drawing.SystemColors.Control;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGS.DisplayLayout.Override.GroupByRowAppearance = appearance23;
			appearance24.TextHAlignAsString = "Left";
			comboBoxCOGS.DisplayLayout.Override.HeaderAppearance = appearance24;
			comboBoxCOGS.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCOGS.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.Color.Silver;
			comboBoxCOGS.DisplayLayout.Override.RowAppearance = appearance25;
			comboBoxCOGS.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCOGS.DisplayLayout.Override.TemplateAddRowAppearance = appearance26;
			comboBoxCOGS.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCOGS.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCOGS.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCOGS.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCOGS.Editable = true;
			comboBoxCOGS.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxCOGS.FilterString = "";
			comboBoxCOGS.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxCOGS.FilterSysDocID = "";
			comboBoxCOGS.HasAllAccount = false;
			comboBoxCOGS.HasCustom = false;
			comboBoxCOGS.IsDataLoaded = false;
			comboBoxCOGS.Location = new System.Drawing.Point(106, 34);
			comboBoxCOGS.MaxDropDownItems = 12;
			comboBoxCOGS.MaxLength = 64;
			comboBoxCOGS.Name = "comboBoxCOGS";
			comboBoxCOGS.ShowInactiveItems = false;
			comboBoxCOGS.ShowQuickAdd = true;
			comboBoxCOGS.Size = new System.Drawing.Size(136, 21);
			comboBoxCOGS.TabIndex = 1;
			comboBoxCOGS.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			linkLabelIncomeAccount.AutoSize = true;
			linkLabelIncomeAccount.Location = new System.Drawing.Point(5, 14);
			linkLabelIncomeAccount.Name = "linkLabelIncomeAccount";
			linkLabelIncomeAccount.Size = new System.Drawing.Size(85, 15);
			linkLabelIncomeAccount.TabIndex = 0;
			linkLabelIncomeAccount.TabStop = true;
			linkLabelIncomeAccount.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelIncomeAccount.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelIncomeAccount.Value = "Income Account:";
			appearance27.ForeColor = System.Drawing.Color.Blue;
			linkLabelIncomeAccount.VisitedLinkAppearance = appearance27;
			linkLabelIncomeAccount.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelIncomeAccount_LinkClicked);
			comboBoxIncomeAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxIncomeAccount.CustomReportFieldName = "";
			comboBoxIncomeAccount.CustomReportKey = "";
			comboBoxIncomeAccount.CustomReportValueType = 1;
			comboBoxIncomeAccount.DescriptionTextBox = textBoxIncomeAccount;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			appearance28.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxIncomeAccount.DisplayLayout.Appearance = appearance28;
			comboBoxIncomeAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxIncomeAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance29.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance29.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxIncomeAccount.DisplayLayout.GroupByBox.Appearance = appearance29;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxIncomeAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance30;
			comboBoxIncomeAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance31.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance31.BackColor2 = System.Drawing.SystemColors.Control;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxIncomeAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance31;
			comboBoxIncomeAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxIncomeAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxIncomeAccount.DisplayLayout.Override.ActiveCellAppearance = appearance32;
			appearance33.BackColor = System.Drawing.SystemColors.Highlight;
			appearance33.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxIncomeAccount.DisplayLayout.Override.ActiveRowAppearance = appearance33;
			comboBoxIncomeAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxIncomeAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			comboBoxIncomeAccount.DisplayLayout.Override.CardAreaAppearance = appearance34;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			appearance35.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxIncomeAccount.DisplayLayout.Override.CellAppearance = appearance35;
			comboBoxIncomeAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxIncomeAccount.DisplayLayout.Override.CellPadding = 0;
			appearance36.BackColor = System.Drawing.SystemColors.Control;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxIncomeAccount.DisplayLayout.Override.GroupByRowAppearance = appearance36;
			appearance37.TextHAlignAsString = "Left";
			comboBoxIncomeAccount.DisplayLayout.Override.HeaderAppearance = appearance37;
			comboBoxIncomeAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxIncomeAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			comboBoxIncomeAccount.DisplayLayout.Override.RowAppearance = appearance38;
			comboBoxIncomeAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance39.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxIncomeAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance39;
			comboBoxIncomeAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxIncomeAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxIncomeAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxIncomeAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxIncomeAccount.Editable = true;
			comboBoxIncomeAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxIncomeAccount.FilterString = "";
			comboBoxIncomeAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxIncomeAccount.FilterSysDocID = "";
			comboBoxIncomeAccount.HasAllAccount = false;
			comboBoxIncomeAccount.HasCustom = false;
			comboBoxIncomeAccount.IsDataLoaded = false;
			comboBoxIncomeAccount.Location = new System.Drawing.Point(106, 12);
			comboBoxIncomeAccount.MaxDropDownItems = 12;
			comboBoxIncomeAccount.MaxLength = 64;
			comboBoxIncomeAccount.Name = "comboBoxIncomeAccount";
			comboBoxIncomeAccount.ShowInactiveItems = false;
			comboBoxIncomeAccount.ShowQuickAdd = true;
			comboBoxIncomeAccount.Size = new System.Drawing.Size(136, 21);
			comboBoxIncomeAccount.TabIndex = 0;
			comboBoxIncomeAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(559, 144);
			base.Controls.Add(textBoxCOGSAccount);
			base.Controls.Add(textBoxAssetAccount);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxIncomeAccount);
			base.Controls.Add(linkLabelCOGSAccount);
			base.Controls.Add(panelButtons);
			base.Controls.Add(linkLabelAssetAccount);
			base.Controls.Add(comboBoxIncomeAccount);
			base.Controls.Add(comboBoxAssetAccount);
			base.Controls.Add(linkLabelIncomeAccount);
			base.Controls.Add(comboBoxCOGS);
			Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(500, 160);
			base.Name = "InventoryAccountsForm";
			Text = "Inventory Accounts";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxAssetAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGS).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxIncomeAccount).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
