using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class InventoryLedgerForm : Form, IDataForm, IDataList, IExternalReport
	{
		private bool isFirstTimeActivating = true;

		private bool canAccessCost;

		private bool excludeInventoryTransfer;

		public string parameter1 = "";

		private Hashtable listViewKeys = new Hashtable();

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet listData;

		private XPButton buttonDone;

		private Panel panelButtons;

		private ContextMenu contextMenuPopup;

		private MenuItem menuItemGotoAccount;

		private MenuItem menuItem2;

		private MenuItem menuItemNew;

		private MenuItem menuItemDelete;

		private MenuItem menuItem1;

		private MenuItem menuItemMarkAs;

		private MenuItem menuItemInactive;

		private ContextMenu contextMenu2;

		private MenuItem menuItemFlat;

		private MenuItem menuItemHierarchical;

		private MenuItem menuItem3;

		private MenuItem menuItemShowInactiveItems;

		private Line linePanelDown;

		private IContainer components;

		private DataGridList dataGridList;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonRefresh;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem microsoftExcelToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonAllowGrouping;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonColumnChooser;

		private ToolStripButton toolStripButtonAutoFit;

		private DateControl dateControl1;

		private PictureBox pictureBoxVoid;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonMerge;

		private TextBox textBoxCode;

		private Label label2;

		private RadioButton radioButtonCode;

		private RadioButton radioButtonItem;

		private ProductComboBox comboBoxItem;

		private TextBox textBoxDescription;

		private CheckBox checkBoxShowCost;

		private CheckBox checkBoxExcludeTransfer;

		private ToolStrip toolStrip1;

		private ScreenAccessRight screenRight;

		public string SelectedID
		{
			get
			{
				if (radioButtonItem.Checked)
				{
					return comboBoxItem.SelectedID;
				}
				return textBoxCode.Text;
			}
			set
			{
				if (radioButtonItem.Checked)
				{
					comboBoxItem.SelectedID = value;
				}
				else
				{
					textBoxCode.Text = value;
				}
			}
		}

		private bool ShowCost
		{
			get
			{
				if (!checkBoxShowCost.Checked || !canAccessCost)
				{
					return false;
				}
				return true;
			}
		}

		private bool ShowInactiveItems
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		public InventoryLedgerForm()
		{
			InitializeComponent();
			base.Activated += CompanyAccountsListForm_Activated;
			dataGridList.DoubleClickRow += dataGridList_DoubleClickRow;
			dataGridList.NewMenuClicked += dataGridList_NewMenuClicked;
			dataGridList.OpenMenuClicked += dataGridList_OpenMenuClicked;
			dataGridList.DeleteMenuClicked += dataGridList_DeleteMenuClicked;
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowNewMenu = false;
			base.FormClosing += InventoryLedgerForm_FormClosing;
			dataGridList.AllowUnfittedView = true;
		}

		private void InventoryLedgerForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveUserSettings();
		}

		private void SaveUserSettings()
		{
			try
			{
				UserPreferences.SaveCurrentUserSetting(base.Name + "MergeCell", toolStripButtonMerge.Checked);
				UserPreferences.SaveCurrentUserSetting(base.Name + "Period", (int)dateControl1.SelectedPeriod);
				dataGridList.SaveLayout();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadUserSettings()
		{
			try
			{
				toolStripButtonMerge.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "MergeCell", defaultValue: false);
				dateControl1.SelectedPeriod = (DatePeriods)UserPreferences.GetCurrentUserSetting(base.Name + "Period", 2);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void dataGridList_DeleteMenuClicked(object sender, EventArgs e)
		{
		}

		private void dataGridList_OpenMenuClicked(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void dataGridList_NewMenuClicked(object sender, EventArgs e)
		{
		}

		private void dataGridList_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			string text = "";
			string text2 = "";
			if (dataGridList.ActiveRow.Cells.Exists("Doc Number"))
			{
				text = dataGridList.ActiveRow.Cells["Doc Number"].Text.ToString();
			}
			else if (dataGridList.ActiveRow.Cells.Exists("VoucherID"))
			{
				text = dataGridList.ActiveRow.Cells["VoucherID"].Text.ToString();
			}
			if (dataGridList.ActiveRow.Cells.Exists("Doc ID"))
			{
				text2 = dataGridList.ActiveRow.Cells["Doc ID"].Text.ToString();
			}
			else if (dataGridList.ActiveRow.Cells.Exists("SysDocID"))
			{
				text2 = dataGridList.ActiveRow.Cells["SysDocID"].Text.ToString();
			}
			string voucherID = text;
			string sysDocID = text2;
			new FormHelper().EditTransaction(sysDocID, voucherID);
		}

		private void CompanyAccountsListForm_Activated(object sender, EventArgs e)
		{
			if (isFirstTimeActivating)
			{
				isFirstTimeActivating = false;
				dataGridList.AutoFitColumns();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && listData != null)
			{
				listData.Dispose();
				listData = null;
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.InventoryLedgerForm));
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
			panelButtons = new System.Windows.Forms.Panel();
			checkBoxShowCost = new System.Windows.Forms.CheckBox();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDone = new Micromind.UISupport.XPButton();
			contextMenuPopup = new System.Windows.Forms.ContextMenu();
			menuItemGotoAccount = new System.Windows.Forms.MenuItem();
			menuItem2 = new System.Windows.Forms.MenuItem();
			menuItemNew = new System.Windows.Forms.MenuItem();
			menuItemDelete = new System.Windows.Forms.MenuItem();
			menuItem1 = new System.Windows.Forms.MenuItem();
			menuItemMarkAs = new System.Windows.Forms.MenuItem();
			menuItemInactive = new System.Windows.Forms.MenuItem();
			contextMenu2 = new System.Windows.Forms.ContextMenu();
			menuItemFlat = new System.Windows.Forms.MenuItem();
			menuItemHierarchical = new System.Windows.Forms.MenuItem();
			menuItem3 = new System.Windows.Forms.MenuItem();
			menuItemShowInactiveItems = new System.Windows.Forms.MenuItem();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			microsoftExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAllowGrouping = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAutoFit = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonColumnChooser = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMerge = new System.Windows.Forms.ToolStripButton();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			dateControl1 = new Micromind.DataControls.DateControl();
			pictureBoxVoid = new System.Windows.Forms.PictureBox();
			textBoxCode = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			radioButtonCode = new System.Windows.Forms.RadioButton();
			radioButtonItem = new System.Windows.Forms.RadioButton();
			comboBoxItem = new Micromind.DataControls.ProductComboBox();
			textBoxDescription = new System.Windows.Forms.TextBox();
			checkBoxExcludeTransfer = new System.Windows.Forms.CheckBox();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxItem).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(checkBoxExcludeTransfer);
			panelButtons.Controls.Add(checkBoxShowCost);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 506);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(828, 48);
			panelButtons.TabIndex = 4;
			checkBoxShowCost.AutoSize = true;
			checkBoxShowCost.Location = new System.Drawing.Point(14, 17);
			checkBoxShowCost.Name = "checkBoxShowCost";
			checkBoxShowCost.Size = new System.Drawing.Size(130, 17);
			checkBoxShowCost.TabIndex = 299;
			checkBoxShowCost.Text = "Show cost information";
			checkBoxShowCost.UseVisualStyleBackColor = true;
			checkBoxShowCost.CheckedChanged += new System.EventHandler(checkBoxShowCost_CheckedChanged);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(828, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(720, 16);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 24);
			buttonDone.TabIndex = 3;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			contextMenuPopup.MenuItems.AddRange(new System.Windows.Forms.MenuItem[6]
			{
				menuItemGotoAccount,
				menuItem2,
				menuItemNew,
				menuItemDelete,
				menuItem1,
				menuItemMarkAs
			});
			menuItemGotoAccount.Index = 0;
			menuItemGotoAccount.Text = "&Goto Account";
			menuItem2.Index = 1;
			menuItem2.Text = "-";
			menuItemNew.Index = 2;
			menuItemNew.Text = "&New Account";
			menuItemDelete.Index = 3;
			menuItemDelete.Text = "&Delete Account";
			menuItem1.Index = 4;
			menuItem1.Text = "-";
			menuItemMarkAs.Index = 5;
			menuItemMarkAs.MenuItems.AddRange(new System.Windows.Forms.MenuItem[1]
			{
				menuItemInactive
			});
			menuItemMarkAs.Text = "Mark As";
			menuItemInactive.Index = 0;
			menuItemInactive.Text = "Inactive";
			contextMenu2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[4]
			{
				menuItemFlat,
				menuItemHierarchical,
				menuItem3,
				menuItemShowInactiveItems
			});
			menuItemFlat.Index = 0;
			menuItemFlat.Text = "Flat View";
			menuItemHierarchical.Index = 1;
			menuItemHierarchical.Text = "Hierarchical View";
			menuItem3.Index = 2;
			menuItem3.Text = "-";
			menuItemShowInactiveItems.Index = 3;
			menuItemShowInactiveItems.Text = "Show Inactive Items";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonAllowGrouping,
				toolStripButtonAutoFit,
				toolStripButton1,
				toolStripSeparator3,
				toolStripButtonColumnChooser,
				toolStripButtonMerge
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(828, 31);
			toolStrip1.TabIndex = 289;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonRefresh.Image = Micromind.ClientUI.Properties.Resources.Refresh;
			toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonRefresh.Name = "toolStripButtonRefresh";
			toolStripButtonRefresh.Size = new System.Drawing.Size(74, 28);
			toolStripButtonRefresh.Text = "Refresh";
			toolStripButtonRefresh.Click += new System.EventHandler(toolStripButtonRefresh_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				microsoftExcelToolStripMenuItem
			});
			toolStripDropDownButton1.Image = Micromind.ClientUI.Properties.Resources.Export;
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(77, 28);
			toolStripDropDownButton1.Text = "Export";
			microsoftExcelToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Excel;
			microsoftExcelToolStripMenuItem.Name = "microsoftExcelToolStripMenuItem";
			microsoftExcelToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			microsoftExcelToolStripMenuItem.Text = "Microsoft Excel";
			microsoftExcelToolStripMenuItem.Click += new System.EventHandler(microsoftExcelToolStripMenuItem_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAllowGrouping.CheckOnClick = true;
			toolStripButtonAllowGrouping.Image = Micromind.ClientUI.Properties.Resources.Groupby;
			toolStripButtonAllowGrouping.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAllowGrouping.Name = "toolStripButtonAllowGrouping";
			toolStripButtonAllowGrouping.Size = new System.Drawing.Size(118, 28);
			toolStripButtonAllowGrouping.Text = "Allow Grouping";
			toolStripButtonAllowGrouping.Click += new System.EventHandler(toolStripButtonAllowGrouping_Click);
			toolStripButtonAutoFit.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonAutoFit.Image");
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(99, 28);
			toolStripButtonAutoFit.Text = "Fit Columns";
			toolStripButtonAutoFit.ToolTipText = "Fit Columns to Screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(118, 28);
			toolStripButton1.Text = "Resize Columns";
			toolStripButton1.ToolTipText = "Resize columns to length of text";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonColumnChooser.Image = Micromind.ClientUI.Properties.Resources.ColumnChooser;
			toolStripButtonColumnChooser.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonColumnChooser.Name = "toolStripButtonColumnChooser";
			toolStripButtonColumnChooser.Size = new System.Drawing.Size(83, 28);
			toolStripButtonColumnChooser.Text = "Columns";
			toolStripButtonColumnChooser.Click += new System.EventHandler(toolStripButtonColumnChooser_Click);
			toolStripButtonMerge.CheckOnClick = true;
			toolStripButtonMerge.Image = Micromind.ClientUI.Properties.Resources.merge;
			toolStripButtonMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMerge.Name = "toolStripButtonMerge";
			toolStripButtonMerge.Size = new System.Drawing.Size(97, 28);
			toolStripButtonMerge.Text = "Merge Cells";
			toolStripButtonMerge.Click += new System.EventHandler(toolStripButtonMerge_Click);
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(12, 126);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(804, 373);
			dataGridList.TabIndex = 3;
			dataGridList.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridList;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 12, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(374, 32);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(318, 88);
			dateControl1.TabIndex = 0;
			dateControl1.ToDate = new System.DateTime(2017, 12, 25, 23, 59, 59, 59);
			pictureBoxVoid.Image = Micromind.ClientUI.Properties.Resources.voidicon;
			pictureBoxVoid.Location = new System.Drawing.Point(802, 28);
			pictureBoxVoid.Name = "pictureBoxVoid";
			pictureBoxVoid.Size = new System.Drawing.Size(26, 20);
			pictureBoxVoid.TabIndex = 291;
			pictureBoxVoid.TabStop = false;
			pictureBoxVoid.Visible = false;
			textBoxCode.Enabled = false;
			textBoxCode.Location = new System.Drawing.Point(73, 85);
			textBoxCode.MaxLength = 64;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(268, 20);
			textBoxCode.TabIndex = 292;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(73, 107);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(239, 13);
			label2.TabIndex = 294;
			label2.Text = "Use wild character '%' to select range of products";
			radioButtonCode.AutoSize = true;
			radioButtonCode.Location = new System.Drawing.Point(14, 86);
			radioButtonCode.Name = "radioButtonCode";
			radioButtonCode.Size = new System.Drawing.Size(53, 17);
			radioButtonCode.TabIndex = 295;
			radioButtonCode.Text = "Code:";
			radioButtonCode.UseVisualStyleBackColor = true;
			radioButtonCode.CheckedChanged += new System.EventHandler(radioButtonItem_CheckedChanged);
			radioButtonItem.AutoSize = true;
			radioButtonItem.Checked = true;
			radioButtonItem.Location = new System.Drawing.Point(12, 35);
			radioButtonItem.Name = "radioButtonItem";
			radioButtonItem.Size = new System.Drawing.Size(48, 17);
			radioButtonItem.TabIndex = 295;
			radioButtonItem.TabStop = true;
			radioButtonItem.Text = "Item:";
			radioButtonItem.UseVisualStyleBackColor = true;
			radioButtonItem.CheckedChanged += new System.EventHandler(radioButtonItem_CheckedChanged);
			comboBoxItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxItem.Assigned = false;
			comboBoxItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxItem.CustomReportFieldName = "";
			comboBoxItem.CustomReportKey = "";
			comboBoxItem.CustomReportValueType = 1;
			comboBoxItem.DescriptionTextBox = textBoxDescription;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxItem.DisplayLayout.Appearance = appearance13;
			comboBoxItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxItem.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxItem.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxItem.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxItem.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxItem.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxItem.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxItem.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxItem.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxItem.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxItem.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxItem.Editable = true;
			comboBoxItem.FilterString = "";
			comboBoxItem.FilterSysDocID = "";
			comboBoxItem.HasAllAccount = false;
			comboBoxItem.HasCustom = false;
			comboBoxItem.IsDataLoaded = false;
			comboBoxItem.Location = new System.Drawing.Point(73, 35);
			comboBoxItem.MaxDropDownItems = 12;
			comboBoxItem.Name = "comboBoxItem";
			comboBoxItem.Show3PLItems = true;
			comboBoxItem.ShowInactiveItems = false;
			comboBoxItem.ShowQuickAdd = true;
			comboBoxItem.Size = new System.Drawing.Size(268, 20);
			comboBoxItem.TabIndex = 296;
			comboBoxItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxDescription.Location = new System.Drawing.Point(73, 57);
			textBoxDescription.MaxLength = 255;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.ReadOnly = true;
			textBoxDescription.Size = new System.Drawing.Size(268, 20);
			textBoxDescription.TabIndex = 292;
			checkBoxExcludeTransfer.AutoSize = true;
			checkBoxExcludeTransfer.Location = new System.Drawing.Point(162, 19);
			checkBoxExcludeTransfer.Name = "checkBoxExcludeTransfer";
			checkBoxExcludeTransfer.Size = new System.Drawing.Size(106, 17);
			checkBoxExcludeTransfer.TabIndex = 300;
			checkBoxExcludeTransfer.Text = "Exclude Transfer";
			checkBoxExcludeTransfer.UseVisualStyleBackColor = true;
			checkBoxExcludeTransfer.CheckedChanged += new System.EventHandler(checkBoxExcludeTransfer_CheckedChanged);
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(828, 554);
			base.Controls.Add(comboBoxItem);
			base.Controls.Add(radioButtonItem);
			base.Controls.Add(radioButtonCode);
			base.Controls.Add(label2);
			base.Controls.Add(textBoxDescription);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(pictureBoxVoid);
			base.Controls.Add(dateControl1);
			base.Controls.Add(dataGridList);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "InventoryLedgerForm";
			Text = "Inventory Ledger";
			base.Load += new System.EventHandler(CompanyAccountsListForm_Load);
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxItem).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			LoadUserSettings();
		}

		private void SetupList()
		{
			try
			{
				dataGridList.ApplyFormat();
				HideShowColumns();
				dataGridList.LoadLayout();
				dataGridList.ApplyUIDesign();
				if (!dataGridList.DisplayLayout.Bands[0].Columns.Exists("Cost"))
				{
					dataGridList.DisplayLayout.Bands[0].Columns.Add("Cost", "Cost").DataType = typeof(decimal);
				}
				dataGridList.DisplayLayout.Bands[0].Columns["Cost"].Hidden = true;
				dataGridList.DisplayLayout.Bands[0].Columns["Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridList.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				dataGridList.ApplyFormat();
				dataGridList.ApplyQuantityColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Qty In"], addSummary: true);
				dataGridList.ApplyQuantityColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Qty Out"], addSummary: true);
				dataGridList.ApplyNumericColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Price"], addSummary: false);
				dataGridList.ApplyNumericColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Asset Value"], addSummary: false);
				dataGridList.ApplyNumericColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Avg Cost"], addSummary: false);
				dataGridList.ApplyQuantityColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Balance"], addSummary: true);
				if (dataGridList.DisplayLayout.Bands.Count > 0 && dataGridList.DisplayLayout.Bands[0].Summaries.Count > 0 && dataGridList.DisplayLayout.Bands[0].Summaries.Exists("count"))
				{
					dataGridList.DisplayLayout.Bands[0].Summaries["count"].SummaryPosition = SummaryPosition.UseSummaryPositionColumn;
					dataGridList.DisplayLayout.Bands[0].Summaries["count"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
					dataGridList.DisplayLayout.Bands[0].Summaries["count"].SummaryPositionColumn = dataGridList.DisplayLayout.Bands[0].Columns[1];
				}
				dataGridList.DisplayLayout.Override.AllowColSizing = AllowColSizing.Free;
				if (dataGridList.DisplayLayout != null && dataGridList.DisplayLayout.Bands.Count > 0)
				{
					ValueList sysDocTypesValueList = UILib.GetSysDocTypesValueList();
					dataGridList.DisplayLayout.Bands[0].Columns["Type"].ValueList = sysDocTypesValueList;
				}
				ShowHideCostInformation();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadData()
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					PublicFunctions.StartWaiting(this);
					listData = Factory.ProductSystem.GetInventoryLedgerList(SelectedID, dateControl1.FromDate, dateControl1.ToDate, excludeInventoryTransfer);
					dataGridList.DataSource = listData;
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
				finally
				{
					PublicFunctions.EndWaiting(this);
				}
			}
		}

		private void HideShowColumns()
		{
			if (dataGridList.DataSource != null)
			{
				_ = dataGridList.DisplayLayout.Bands.Count;
			}
		}

		private void buttonDone_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OpenForEdit()
		{
		}

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
		}

		private void buttonGotoItem_Click(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void CompanyAccountsListForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					Init();
					LoadData();
					SetupList();
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
				if (base.IsHandleCreated)
				{
					Close();
				}
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
			}
			if (canAccessCost)
			{
				checkBoxShowCost.Visible = true;
				return;
			}
			checkBoxShowCost.Checked = false;
			checkBoxShowCost.Visible = false;
		}

		public void OnActivated()
		{
		}

		private void CloseForm()
		{
			Close();
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Accounts;
		}

		public static int GetScreenID()
		{
			return 7004;
		}

		private string GetSelectedID()
		{
			try
			{
				string result = "";
				if (dataGridList.ActiveRow != null && dataGridList.ActiveRow.Band.Index == 0)
				{
					if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						return "";
					}
					if (dataGridList.ActiveRow.Cells.Exists("Doc Number"))
					{
						result = dataGridList.ActiveRow.Cells["Doc Number"].Text.ToString();
					}
					else if (dataGridList.ActiveRow.Cells.Exists("VoucherID"))
					{
						result = dataGridList.ActiveRow.Cells["VoucherID"].Text.ToString();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private string GetSelectedSysDocID()
		{
			try
			{
				string result = "";
				if (dataGridList.ActiveRow.Band.Index == 1)
				{
					return "";
				}
				if (dataGridList.ActiveRow != null)
				{
					if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						return "";
					}
					if (dataGridList.ActiveRow.Cells.Exists("Doc ID"))
					{
						result = dataGridList.ActiveRow.Cells["Doc ID"].Text.ToString();
					}
					else if (dataGridList.ActiveRow.Cells.Exists("SysDocID"))
					{
						result = dataGridList.ActiveRow.Cells["SysDocID"].Text.ToString();
					}
				}
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private UltraGridRow GetSelectedItem()
		{
			if (dataGridList.ActiveRow != null)
			{
				return dataGridList.ActiveRow;
			}
			return null;
		}

		public void RefreshData()
		{
		}

		private string GetDocumentTitle()
		{
			return "Account List";
		}

		private void Print()
		{
			try
			{
				PrintHelper.PreviewDocument(ultraGridPrintDocument1, Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public bool SaveReport(ExternalReportTypes reportType)
		{
			return true;
		}

		public void ClearView()
		{
		}

		private void toolStripButtonRefresh_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print();
		}

		private void microsoftExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new DataExportHelper().ExportToExcel(dataGridList);
		}

		private void toolStripButtonAllowGrouping_Click(object sender, EventArgs e)
		{
			dataGridList.DisplayLayout.GroupByBox.Hidden = !toolStripButtonAllowGrouping.Checked;
		}

		private void toolStripButtonColumnChooser_Click(object sender, EventArgs e)
		{
			dataGridList.ShowColumnChooser();
		}

		private void toolStripButtonAutoFit_Click(object sender, EventArgs e)
		{
			dataGridList.AutoFitColumns();
		}

		private void toolStripButtonShowInactive_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			dataGridList.AutoSizeColumns();
		}

		private void toolStripButtonMerge_Click(object sender, EventArgs e)
		{
			if (toolStripButtonMerge.Checked)
			{
				dataGridList.DisplayLayout.Override.MergedCellStyle = MergedCellStyle.OnlyWhenSorted;
			}
			else
			{
				dataGridList.DisplayLayout.Override.MergedCellStyle = MergedCellStyle.Never;
			}
		}

		private void radioButtonItem_CheckedChanged(object sender, EventArgs e)
		{
			textBoxCode.Enabled = radioButtonCode.Checked;
			ProductComboBox productComboBox = comboBoxItem;
			bool enabled = textBoxDescription.Enabled = radioButtonItem.Checked;
			productComboBox.Enabled = enabled;
		}

		private void checkBoxShowCost_CheckedChanged(object sender, EventArgs e)
		{
			ShowHideCostInformation();
		}

		private void ShowHideCostInformation()
		{
			if (ShowCost)
			{
				dataGridList.DisplayLayout.Bands[0].Columns["Avg Cost"].Hidden = false;
				dataGridList.DisplayLayout.Bands[0].Columns["Avg Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridList.DisplayLayout.Bands[0].Columns["Asset Value"].Hidden = false;
				dataGridList.DisplayLayout.Bands[0].Columns["Asset Value"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			else
			{
				UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["Avg Cost"];
				bool hidden = dataGridList.DisplayLayout.Bands[0].Columns["Asset Value"].Hidden = true;
				ultraGridColumn.Hidden = hidden;
				ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridList.DisplayLayout.Bands[0].Columns["Avg Cost"].ExcludeFromColumnChooser = (dataGridList.DisplayLayout.Bands[0].Columns["Asset Value"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
			}
			foreach (UltraGridRow row in dataGridList.Rows)
			{
				decimal result = default(decimal);
				decimal.TryParse(row.Cells["Asset Value"].Value.ToString(), out result);
				if (result > 0m)
				{
					if (ShowCost)
					{
						if (!row.Cells["Cost"].Value.IsNullOrEmpty())
						{
							row.Cells["Price"].Value = row.Cells["Cost"].Value;
						}
					}
					else
					{
						row.Cells["Cost"].Value = row.Cells["Price"].Value;
						row.Cells["Price"].Value = DBNull.Value;
					}
				}
			}
		}

		private void checkBoxExcludeTransfer_CheckedChanged(object sender, EventArgs e)
		{
			excludeInventoryTransfer = checkBoxExcludeTransfer.Checked;
		}
	}
}
