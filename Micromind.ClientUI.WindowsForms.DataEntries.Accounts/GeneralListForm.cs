using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class GeneralListForm : Form, IDataForm, IDataList, IExternalReport
	{
		private bool isFirstTimeActivating = true;

		private DataComboType listType = DataComboType.AccountGroup;

		private UltraGrid gridInUse;

		private Hashtable listViewKeys = new Hashtable();

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet listData;

		private XPButton buttonDone;

		private XPButton buttonNew;

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

		private XPButton buttonOpen;

		private DataGridList dataGridList;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonRefresh;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem microsoftExcelToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton toolStripButtonShowInactive;

		private ToolStripButton toolStripButtonAllowGrouping;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonColumnChooser;

		private ToolStripButton toolStripButtonAutoFit;

		private XPButton buttonDelete;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonMerge;

		private ToolStripButton toolStripButtonTreeView;

		private DataGridTreeList dataGridTreeList;

		private ToolStrip toolStrip1;

		private ScreenAccessRight screenRight;

		public DataComboType ListType
		{
			get
			{
				return listType;
			}
			set
			{
				listType = value;
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

		public GeneralListForm()
		{
			InitializeComponent();
			base.Activated += CompanyAccountsListForm_Activated;
			dataGridList.DoubleClickRow += dataGridList_DoubleClickRow;
			dataGridList.NewMenuClicked += dataGridList_NewMenuClicked;
			dataGridList.OpenMenuClicked += dataGridList_OpenMenuClicked;
			dataGridList.DeleteMenuClicked += dataGridList_DeleteMenuClicked;
			dataGridList.AllowUnfittedView = true;
			base.FormClosing += GeneralListForm_FormClosing;
			dataGridTreeList.DoubleClick += dataGridTreeList_DoubleClick;
		}

		private void dataGridTreeList_DoubleClick(object sender, EventArgs e)
		{
			EditSelectedTreeRow();
		}

		private void GeneralListForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				UserPreferences.SaveCurrentUserSetting(base.Name + "MergeCell", toolStripButtonMerge.Checked);
				Global.GlobalSettings.SaveFormProperties(this);
				dataGridList.SaveLayout();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridList_DeleteMenuClicked(object sender, EventArgs e)
		{
			Delete();
		}

		private void dataGridList_OpenMenuClicked(object sender, EventArgs e)
		{
			EditSelectedRow();
		}

		private void dataGridList_NewMenuClicked(object sender, EventArgs e)
		{
			OpenForEdit("");
		}

		private void dataGridList_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			EditSelectedRow();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.GeneralListForm));
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
			buttonDelete = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDone = new Micromind.UISupport.XPButton();
			buttonOpen = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
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
			toolStripButtonShowInactive = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAllowGrouping = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAutoFit = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonColumnChooser = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMerge = new System.Windows.Forms.ToolStripButton();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			toolStripButtonTreeView = new System.Windows.Forms.ToolStripButton();
			dataGridTreeList = new Micromind.UISupport.DataGridTreeList(components);
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridTreeList).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Controls.Add(buttonOpen);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 468);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(974, 39);
			panelButtons.TabIndex = 1;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "Delete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Visible = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(974, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(866, 8);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(95, 24);
			buttonDone.TabIndex = 3;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			buttonOpen.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpen.BackColor = System.Drawing.Color.DarkGray;
			buttonOpen.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpen.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpen.Location = new System.Drawing.Point(12, 8);
			buttonOpen.Name = "buttonOpen";
			buttonOpen.Size = new System.Drawing.Size(96, 24);
			buttonOpen.TabIndex = 0;
			buttonOpen.Text = "&Open";
			buttonOpen.UseVisualStyleBackColor = false;
			buttonOpen.Click += new System.EventHandler(buttonGotoItem_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
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
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[13]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonShowInactive,
				toolStripButtonAllowGrouping,
				toolStripButtonAutoFit,
				toolStripButton1,
				toolStripSeparator3,
				toolStripButtonColumnChooser,
				toolStripButtonMerge,
				toolStripButtonTreeView
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(974, 31);
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
			toolStripButtonShowInactive.CheckOnClick = true;
			toolStripButtonShowInactive.Image = Micromind.ClientUI.Properties.Resources.ShowInactive;
			toolStripButtonShowInactive.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowInactive.Name = "toolStripButtonShowInactive";
			toolStripButtonShowInactive.Size = new System.Drawing.Size(108, 28);
			toolStripButtonShowInactive.Text = "Show Inactive";
			toolStripButtonShowInactive.Click += new System.EventHandler(toolStripButtonShowInactive_Click);
			toolStripButtonAllowGrouping.CheckOnClick = true;
			toolStripButtonAllowGrouping.Image = Micromind.ClientUI.Properties.Resources.Groupby;
			toolStripButtonAllowGrouping.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAllowGrouping.Name = "toolStripButtonAllowGrouping";
			toolStripButtonAllowGrouping.Size = new System.Drawing.Size(118, 28);
			toolStripButtonAllowGrouping.Text = "Allow Grouping";
			toolStripButtonAllowGrouping.Click += new System.EventHandler(toolStripButtonAllowGrouping_Click);
			toolStripButtonAutoFit.Image = Micromind.ClientUI.Properties.Resources.column;
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(99, 28);
			toolStripButtonAutoFit.Text = "Fit Columns";
			toolStripButtonAutoFit.ToolTipText = "Fit columns to fit in screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.resize;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(113, 28);
			toolStripButton1.Text = "Resize Column";
			toolStripButton1.ToolTipText = "Resize column to show contents";
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
			dataGridList.Location = new System.Drawing.Point(12, 42);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(949, 407);
			dataGridList.TabIndex = 0;
			dataGridList.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridList;
			toolStripButtonTreeView.Checked = true;
			toolStripButtonTreeView.CheckOnClick = true;
			toolStripButtonTreeView.CheckState = System.Windows.Forms.CheckState.Checked;
			toolStripButtonTreeView.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonTreeView.Image");
			toolStripButtonTreeView.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonTreeView.Name = "toolStripButtonTreeView";
			toolStripButtonTreeView.Size = new System.Drawing.Size(85, 28);
			toolStripButtonTreeView.Text = "Tree View";
			toolStripButtonTreeView.Visible = false;
			toolStripButtonTreeView.Click += new System.EventHandler(toolStripButtonTreeView_Click);
			dataGridTreeList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridTreeList.DisplayLayout.Appearance = appearance13;
			dataGridTreeList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridTreeList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridTreeList.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridTreeList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridTreeList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridTreeList.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridTreeList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridTreeList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridTreeList.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridTreeList.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridTreeList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridTreeList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridTreeList.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridTreeList.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridTreeList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridTreeList.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridTreeList.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridTreeList.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridTreeList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridTreeList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridTreeList.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridTreeList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridTreeList.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridTreeList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridTreeList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridTreeList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridTreeList.Location = new System.Drawing.Point(12, 42);
			dataGridTreeList.Name = "dataGridTreeList";
			dataGridTreeList.ShowDeleteMenu = false;
			dataGridTreeList.ShowMinusInRed = true;
			dataGridTreeList.ShowNewMenu = false;
			dataGridTreeList.Size = new System.Drawing.Size(949, 407);
			dataGridTreeList.TabIndex = 294;
			dataGridTreeList.Text = "dataGridTreeList1";
			dataGridTreeList.Visible = false;
			base.AcceptButton = buttonOpen;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(974, 507);
			base.Controls.Add(dataGridTreeList);
			base.Controls.Add(dataGridList);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "GeneralListForm";
			Text = "List";
			base.Load += new System.EventHandler(CompanyAccountsListForm_Load);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridTreeList).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			toolStripButtonMerge.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "MergeCell", defaultValue: false);
			Global.GlobalSettings.LoadFormProperties(this);
		}

		private void SetupList()
		{
			try
			{
				if (dataGridList.DisplayLayout != null && dataGridList.DisplayLayout.Bands.Count > 0)
				{
					if (listType == DataComboType.TenancyContract)
					{
						if (!dataGridList.DisplayLayout.ValueLists.Exists("Status"))
						{
							ValueList valueList = dataGridList.DisplayLayout.ValueLists.Add("Status");
							valueList.ValueListItems.Add(1, "Active");
							valueList.ValueListItems.Add(2, "Cancelled");
							valueList.ValueListItems.Add(3, "Expired");
							dataGridList.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList;
						}
					}
					else if (listType == DataComboType.TradeLicense)
					{
						if (!dataGridList.DisplayLayout.ValueLists.Exists("Status"))
						{
							ValueList valueList2 = dataGridList.DisplayLayout.ValueLists.Add("Status");
							valueList2.ValueListItems.Add(1, "Active");
							valueList2.ValueListItems.Add(2, "Cancelled");
							valueList2.ValueListItems.Add(3, "Expired");
							dataGridList.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList2;
						}
					}
					else if (listType == DataComboType.Visa)
					{
						if (!dataGridList.DisplayLayout.ValueLists.Exists("Type"))
						{
							ValueList valueList3 = dataGridList.DisplayLayout.ValueLists.Add("Type");
							valueList3.ValueListItems.Add(1, "N/A");
							valueList3.ValueListItems.Add(2, "Visit");
							valueList3.ValueListItems.Add(3, "Transit");
							valueList3.ValueListItems.Add(4, "Employment");
							valueList3.ValueListItems.Add(5, "Other");
							dataGridList.DisplayLayout.Bands[0].Columns["Type"].ValueList = valueList3;
							valueList3 = dataGridList.DisplayLayout.ValueLists.Add("Status");
							valueList3.ValueListItems.Add(1, "Active");
							valueList3.ValueListItems.Add(2, "Cancelled");
							valueList3.ValueListItems.Add(3, "Expired");
							valueList3.ValueListItems.Add(4, "Closed");
							dataGridList.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList3;
						}
					}
					else if (listType == DataComboType.Chequebook && !dataGridList.DisplayLayout.ValueLists.Exists("Status"))
					{
						ValueList valueList4 = dataGridList.DisplayLayout.ValueLists.Add("Status");
						valueList4.ValueListItems.Add(1, "Active");
						valueList4.ValueListItems.Add(2, "Hold");
						valueList4.ValueListItems.Add(3, "Closed");
						dataGridList.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList4;
					}
				}
				dataGridList.AddColumnRowCount(dataGridList.DisplayLayout.Bands[0].Columns[0]);
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
					GenericListTypes result = GenericListTypes.All;
					if (listType == DataComboType.AccountGroup)
					{
						listData = Factory.AccountGroupsSystem.GetAccountGroupsList();
						goto IL_15e6;
					}
					if (listType == DataComboType.PayrollItem)
					{
						listData = Factory.PayrollItemSystem.GetPayrollItemList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Analysis)
					{
						listData = Factory.AnalysisSystem.GetAnalysisList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ServiceItem)
					{
						listData = Factory.ServiceTypeSystem.GetServiceItemList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EmployeeProvisionType)
					{
						listData = Factory.ProvisionTypeSystem.GetProvisionTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.AnalysisGroup)
					{
						listData = Factory.AnalysisGroupsSystem.GetAnalysisGroupsList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Area)
					{
						listData = Factory.AreaSystem.GetAreaList();
						goto IL_15e6;
					}
					if (listType == DataComboType.VendorAddress)
					{
						listData = Factory.VendorAddressSystem.GetVendorAddressList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Bank)
					{
						listData = Factory.BankSystem.GetBankList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Driver)
					{
						listData = Factory.DriverSystem.GetDriverList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Benefit)
					{
						listData = Factory.BenefitSystem.GetBenefitList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EOSRule)
					{
						listData = Factory.EOSRuleSystem.GetEOSRuleList();
						goto IL_15e6;
					}
					if (listType == DataComboType.FiscalYear)
					{
						listData = Factory.FiscalYearSystem.GetFiscalYearList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Buyer)
					{
						listData = Factory.BuyerSystem.GetBuyerList();
						goto IL_15e6;
					}
					if (listType == DataComboType.FixedAsset)
					{
						listData = Factory.FixedAssetSystem.GetAssetList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CompanyDocType)
					{
						listData = Factory.CompanyDocTypeSystem.GetCompanyDocTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CompanyDocument)
					{
						listData = Factory.CompanyDocumentSystem.GetCompanyDocumentList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Contact)
					{
						listData = Factory.ContactSystem.GetContactList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Country)
					{
						listData = Factory.CountrySystem.GetCountryList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CustomerCategory)
					{
						listData = Factory.EntityCategorySystem.GetEntityCategoryList(EntityTypesEnum.Customers);
						toolStripButtonTreeView.Checked = true;
						GetTreeview();
						goto IL_15e6;
					}
					if (listType == DataComboType.ContactsCategory)
					{
						listData = Factory.EntityCategorySystem.GetEntityCategoryList(EntityTypesEnum.Contacts);
						goto IL_15e6;
					}
					if (listType == DataComboType.LeadCategory)
					{
						listData = Factory.EntityCategorySystem.GetEntityCategoryList(EntityTypesEnum.Leads);
						goto IL_15e6;
					}
					if (listType == DataComboType.VendorCategory)
					{
						listData = Factory.EntityCategorySystem.GetEntityCategoryList(EntityTypesEnum.Vendors);
						goto IL_15e6;
					}
					if (listType == DataComboType.EntityCategory)
					{
						listData = Factory.EntityCategorySystem.GetEntityCategoryList(EntityTypesEnum.Customers);
						goto IL_15e6;
					}
					if (listType == DataComboType.LeadStatus)
					{
						listData = Factory.LeadStatusSystem.GetLeadStatusList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Currency)
					{
						listData = Factory.CurrencySystem.GetCurrencyList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CustomerClass)
					{
						listData = Factory.CustomerClassSystem.GetCustomerClassList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CustomerAddress)
					{
						listData = Factory.CustomerAddressSystem.GetCustomerAddressList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ServiceProvider)
					{
						listData = Factory.VendorSystem.GetServiceProviderList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CustomerGroup)
					{
						listData = Factory.CustomerGroupSystem.GetCustomerGroupList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Deduction)
					{
						listData = Factory.PayrollItemSystem.GetPayrollDeductionList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Degree)
					{
						listData = Factory.DegreeSystem.GetDegreeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Department)
					{
						listData = Factory.DepartmentSystem.GetDepartmentList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Destination)
					{
						listData = Factory.DestinationSystem.GetDestinationList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EmployeeDocument)
					{
						listData = Factory.EmployeeDocumentSystem.GetEmployeeDocumentList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Division)
					{
						listData = Factory.DivisionSystem.GetDivisionList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CompanyDivision)
					{
						listData = Factory.CompanyDivisionSystem.GetDivisionList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EmployeeDocType)
					{
						listData = Factory.EmployeeDocTypeSystem.GetEmployeeDocTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.VehicleDocType)
					{
						listData = Factory.VehicleDocTypeSystem.GetVehicleDocTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EmployeeGroup)
					{
						listData = Factory.EmployeeGroupSystem.GetEmployeeGroupList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Grade)
					{
						listData = Factory.GradeSystem.GetGradeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.LeaveType)
					{
						listData = Factory.LeaveTypeSystem.GetLeaveTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.JobType)
					{
						listData = Factory.JobTypeSystem.GetJobTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CostCategory)
					{
						listData = Factory.CostCategorySystem.GetCostCategoryList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CheckList)
					{
						listData = Factory.CheckListSystem.GetCheckList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Location)
					{
						listData = Factory.LocationSystem.GetLocationList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Unit)
					{
						listData = Factory.UnitSystem.GetUnitList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Nationality)
					{
						listData = Factory.NationalitySystem.GetNationalityList();
						goto IL_15e6;
					}
					if (listType == DataComboType.PaymentMethod)
					{
						listData = Factory.PaymentMethodSystem.GetPaymentMethodsList();
						goto IL_15e6;
					}
					if (listType == DataComboType.PaymentTerm)
					{
						listData = Factory.TermSystem.GetPaymentTermsList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Position)
					{
						listData = Factory.PositionSystem.GetPositionList();
						goto IL_15e6;
					}
					if (listType == DataComboType.PriceLevel)
					{
						listData = Factory.PriceLevelSystem.GetPriceLevelList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ProductBrand)
					{
						listData = Factory.ProductBrandSystem.GetProductBrandList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ProductCategory)
					{
						listData = Factory.ProductCategorySystem.GetProductCategoryList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ProductClass)
					{
						listData = Factory.ProductClassSystem.GetProductClassList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ProductManufacturer)
					{
						listData = Factory.ProductManufacturerSystem.GetProductManufacturerList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ProductStyle)
					{
						listData = Factory.ProductStyleSystem.GetProductStyleList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ProductSpecification)
					{
						listData = Factory.ProductSpecificationSystem.GetProductSpecificationList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Religion)
					{
						listData = Factory.ReligionSystem.GetReligionList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Salesperson)
					{
						listData = Factory.SalespersonSystem.GetSalespersonList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ShippingMethod)
					{
						listData = Factory.ShippingMethodSystem.GetShippingMethodsList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Skill)
					{
						listData = Factory.SkillSystem.GetSkillList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Sponsor)
					{
						listData = Factory.SponsorSystem.GetSponsorList();
						goto IL_15e6;
					}
					if (listType == DataComboType.TenancyContract)
					{
						listData = Factory.TenancyContractSystem.GetTenancyContractList();
						goto IL_15e6;
					}
					if (listType == DataComboType.TradeLicense)
					{
						listData = Factory.TradeLicenseSystem.GetTradeLicenseList();
						goto IL_15e6;
					}
					if (listType == DataComboType.VendorClass)
					{
						listData = Factory.VendorClassSystem.GetVendorClassList();
						goto IL_15e6;
					}
					if (listType == DataComboType.VendorGroup)
					{
						listData = Factory.VendorGroupSystem.GetVendorGroupList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Visa)
					{
						listData = Factory.VisaSystem.GetVisaList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CostCenter)
					{
						listData = Factory.CostCenterSystem.GetCostCenterList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ReturnedChequeReason)
					{
						listData = Factory.ReturnedChequeReasonSystem.GetReturnedChequeReasonList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Chequebook)
					{
						listData = Factory.ChequebookSystem.GetChequebookList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Register)
					{
						listData = Factory.RegisterSystem.GetRegisterList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Port)
					{
						listData = Factory.PortSystem.GetPortList();
						goto IL_15e6;
					}
					if (listType == DataComboType.DisciplineActionType)
					{
						listData = Factory.DisciplineActionTypeSystem.GetActionTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EmployeeActivityType)
					{
						listData = Factory.EmployeeActivityTypeSystem.GetActivityTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Register)
					{
						listData = Factory.RegisterSystem.GetRegisterList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Employee)
					{
						listData = Factory.EmployeeSystem.GetEmployeeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Job)
					{
						listData = Factory.JobSystem.GetJobList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Opportunity)
					{
						listData = Factory.OpportunitySystem.GetOpportunityList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Competitor)
					{
						listData = Factory.CompetitorSystem.GetCompetitorList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Campaign)
					{
						listData = Factory.CampaignSystem.GetCampaignList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Event)
					{
						listData = Factory.EventSystem.GetEventList();
						goto IL_15e6;
					}
					if (listType == DataComboType.BankFacilityGroup)
					{
						listData = Factory.BankFacilityGroupSystem.GetBankFacilityGroupList();
						goto IL_15e6;
					}
					if (listType == DataComboType.BankFacility)
					{
						listData = Factory.BankFacilitySystem.GetBankFacilityList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ArrivalReportTemplate)
					{
						listData = Factory.ArrivalReportTemplateSystem.GetArrivalReportTemplateList();
						goto IL_15e6;
					}
					if (listType == DataComboType.JobFee)
					{
						listData = Factory.JobSystem.GetJobFeeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Vehicle)
					{
						listData = Factory.VehicleSystem.GetVehicleList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CustomerRelation)
					{
						listData = Factory.CustomerSystem.GetCustomerRelationshipList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CustomerVendorLink)
					{
						listData = Factory.PartySystem.GetPartyList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Equipment)
					{
						listData = Factory.EquipmentSystem.GetEquipmentList();
						goto IL_15e6;
					}
					if (listType == DataComboType.PriceList)
					{
						listData = Factory.PriceListSystem.GetPriceListAll();
						goto IL_15e6;
					}
					if (listType == DataComboType.UserGroup)
					{
						listData = Factory.UserGroupSystem.GetUserGroupList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Candidate)
					{
						listData = Factory.CandidateSystem.GetCandidateList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Appointment)
					{
						listData = Factory.CandidateSystem.GetAppointmentList();
						goto IL_15e6;
					}
					if (listType == DataComboType.WorkLocation)
					{
						listData = Factory.WorkLocationSystem.GetWorkLocationList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ExpenseCode)
					{
						listData = Factory.ExpenseCodeSystem.GetExpenseCodeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Transporter)
					{
						listData = Factory.TransporterSystem.GetTransporterList();
						goto IL_15e6;
					}
					if (listType == DataComboType.User)
					{
						listData = Factory.UserSystem.GetUserList();
						goto IL_15e6;
					}
					if (listType == DataComboType.INCO)
					{
						listData = Factory.INCOSystem.GetINCOList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Collateral)
					{
						listData = Factory.CollateralSystem.GetCollateralList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CustomReport)
					{
						listData = Factory.CustomReportSystem.GetCustomReportComboList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CustomGadget)
					{
						listData = Factory.CustomGadgetSystem.GetCustomGadgetComboList();
						goto IL_15e6;
					}
					if (listType == DataComboType.JobTask)
					{
						listData = Factory.JobTaskSystem.GetJobTaskList();
						goto IL_15e6;
					}
					if (listType == DataComboType.JobTaskGroup)
					{
						listData = Factory.JobTaskGroupSystem.GetJobTaskGroupList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Followup)
					{
						goto IL_15e6;
					}
					if (listType == DataComboType.CustomerCategory)
					{
						listData = Factory.EntityCategorySystem.GetEntityCategoryList(EntityTypesEnum.Customers);
						goto IL_15e6;
					}
					if (listType == DataComboType.LeadCategory)
					{
						listData = Factory.EntityCategorySystem.GetEntityCategoryList(EntityTypesEnum.Leads);
						goto IL_15e6;
					}
					if (listType == DataComboType.QualityTask)
					{
						listData = Factory.QualityTaskSystem.GetQualityTaskList(includeClosedTasks: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.ArrivalReportTemplate)
					{
						listData = Factory.ArrivalReportTemplateSystem.GetArrivalReportTemplateList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Surveyor)
					{
						listData = Factory.SurveyorSystem.GetSurveyorList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EmployeeLoanType)
					{
						listData = Factory.EmployeeLoanTypeSystem.GetEmployeeLoanTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EmployeeLeave)
					{
						listData = Factory.EmployeeActivitySystem.GetEmployeeLeaveDetailList();
						goto IL_15e6;
					}
					if (listType == DataComboType.PassportControl)
					{
						listData = Factory.EmployeeActivitySystem.GetEmployeePassportControlList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EmployeeLeaveResumption)
					{
						listData = Factory.EmployeeActivitySystem.GetEmployeeLeaveResumptionDetailList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EmployeeType)
					{
						listData = Factory.EmployeeTypeSystem.GetEmployeeTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.AdjustmentType)
					{
						listData = Factory.AdjustmentTypeSystem.GetAdjustmentTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.PropertyClass)
					{
						listData = Factory.PropertyClassSystem.GetPropertyClassList();
						goto IL_15e6;
					}
					if (listType == DataComboType.PropertyAgent)
					{
						listData = Factory.PropertyAgentSystem.GetPropertyAgentList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Property)
					{
						listData = Factory.PropertySystem.GetPropertyList(showInactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.PropertyUnit)
					{
						listData = Factory.PropertyUnitSystem.GetPropertyUnitList(showInactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.PropertyVirtualUnit)
					{
						listData = Factory.PropertyVirtualUnitSystem.GetPropertyVirtualUnitList(showInactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.PropertyCategory)
					{
						listData = Factory.PropertyCategorySystem.GetPropertyCategoryList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Tenant)
					{
						listData = Factory.CustomerSystem.GetTenantList(inactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.PropertyIncomeCode)
					{
						listData = Factory.PropertyIncomeCodeSystem.GetPropertyIncomeCodeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ContainerSize)
					{
						listData = Factory.ContainerSizeSystem.GetContainerSizeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.FixedAssetClass)
					{
						listData = Factory.FixedAssetClassSystem.GetAssetClassList();
						goto IL_15e6;
					}
					if (listType == DataComboType.FixedAssetLocation)
					{
						listData = Factory.FixedAssetLocationSystem.GetAssetLocation();
						goto IL_15e6;
					}
					if (listType == DataComboType.FixedAssetGroup)
					{
						listData = Factory.FixedAssetGroupSystem.GetAssetGroup();
						goto IL_15e6;
					}
					if (listType == DataComboType.InventoryTransferType)
					{
						listData = Factory.InventoryTransferTypeSystem.GetInventoryTransferType();
						goto IL_15e6;
					}
					if (listType == DataComboType.JobBOM)
					{
						listData = Factory.JobBOMSystem.GetJobBOMList(isInactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.ClientAsset)
					{
						listData = Factory.ClientAssetSystem.GetClientAssetList(isactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.Package)
					{
						listData = Factory.BOMSystem.GetPackageList(isInactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.Bin)
					{
						listData = Factory.BinSystem.GetBinList(isactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.Route)
					{
						listData = Factory.RouteSystem.GetRouteList(isactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.RouteGroup)
					{
						listData = Factory.RouteGroupSystem.GetRouteGroupList(isInactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.Approval)
					{
						listData = Factory.ApprovalSystem.GetApprovalList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Verification)
					{
						listData = Factory.ApprovalSystem.GetVerificationList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ReleaseType)
					{
						listData = Factory.ReleaseTypeSystem.GetReleaseTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ServiceActivity)
					{
						listData = Factory.ServiceActivitySystem.GetServiceActivityList();
						goto IL_15e6;
					}
					if (listType == DataComboType.InsuranceProvider)
					{
						listData = Factory.InsuranceProviderSystem.GetInsuranceProviderList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CRMCustomerActivity)
					{
						listData = Factory.EmployeeActivitySystem.GetActivityList();
						goto IL_15e6;
					}
					if (listType == DataComboType.RiderSummary)
					{
						listData = Factory.RiderSummarySystem.GetRiderSummaryList(isactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.HorseSummary)
					{
						listData = Factory.HorseSummarySystem.GetHorseSummaryList(isactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.HorseType)
					{
						listData = Factory.HorseTypeSystem.GetHorseTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.HorseSex)
					{
						listData = Factory.HorseSexSystem.GetHorseSexList();
						goto IL_15e6;
					}
					if (listType == DataComboType.HolidayCalendar)
					{
						listData = Factory.HolidayCalendarSystem.GetList();
						goto IL_15e6;
					}
					if (listType == DataComboType.OverTime)
					{
						listData = Factory.OverTimeSystem.GetOverTimeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EquipmentCategory)
					{
						listData = Factory.EquipmentCategorySystem.GetEquipmentCategoryList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EquipmentType)
					{
						listData = Factory.EquipmentTypeSystem.GetEquipmentTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EAEquipment)
					{
						listData = Factory.EAEquipmentSystem.GetEquipmentList();
						goto IL_15e6;
					}
					if (listType == DataComboType.RequisitionType)
					{
						listData = Factory.RequisitionTypeSystem.GetRequisitionTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Lawyer)
					{
						listData = Factory.LawyerSystem.GetLawyerList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CaseParty)
					{
						listData = Factory.CasePartySystem.GetCasePartyList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Tax)
					{
						listData = Factory.TaxSystem.GetTaxList();
						goto IL_15e6;
					}
					if (listType == DataComboType.TaxGroup)
					{
						listData = Factory.TaxGroupSystem.GetTaxGroupList();
						goto IL_15e6;
					}
					if (listType == DataComboType.SysDoc)
					{
						listData = Factory.SystemDocumentSystem.GetSystemDocumentList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ProductMake)
					{
						listData = Factory.ProductMakeSystem.GetProductMakeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ProductType)
					{
						listData = Factory.ProductTypeSystem.GetProductTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.ProductModel)
					{
						listData = Factory.ProductModelSystem.GetProductModelList();
						goto IL_15e6;
					}
					if (listType == DataComboType.CaseClient)
					{
						listData = Factory.CaseClientSystem.GetCustomerList();
						goto IL_15e6;
					}
					if (listType == DataComboType.TransactionDoc)
					{
						listData = Factory.SalespersonGroupSystem.GetSalespersonGroupList();
						goto IL_15e6;
					}
					if (listType == DataComboType.TaskSteps)
					{
						listData = Factory.TaskStepsSystem.GetTaskStepsList();
						goto IL_15e6;
					}
					if (listType == DataComboType.TaskType)
					{
						listData = Factory.TaskTypeSystem.GetTaskTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.LegalActionStatus)
					{
						listData = Factory.LegalActionStatusSystem.GetLegalActionStatusList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Rack)
					{
						listData = Factory.RackSystem.GetRackList(isactive: true);
						goto IL_15e6;
					}
					if (listType == DataComboType.PrintTemplateMap)
					{
						listData = Factory.PrintTemplateMapSystem.GetPrintTemplateMapList();
						goto IL_15e6;
					}
					if (listType == DataComboType.EmployeeAbsconding)
					{
						listData = Factory.EmployeeActivitySystem.GetEmployeeAbscondingEntryList();
						goto IL_15e6;
					}
					if (listType == DataComboType.Patient)
					{
						listData = Factory.PatientSystem.GetCustomerList();
						goto IL_15e6;
					}
					if (listType == DataComboType.PatientDocType)
					{
						listData = Factory.PatientDocTypeSystem.GetPatientDocTypeList();
						goto IL_15e6;
					}
					if (listType == DataComboType.DataSync)
					{
						listData = Factory.DataSyncSystem.GetSyncList();
						goto IL_15e6;
					}
					if (Enum.TryParse(listType.ToString(), out result))
					{
						listData = Factory.GenericListSystem.GetGenericListList(result);
						goto IL_15e6;
					}
					ErrorHelper.ErrorMessage("General List is not implemented for type: " + listType.ToString());
					goto end_IL_0009;
					IL_15e6:
					dataGridList.DataSource = listData;
					end_IL_0009:;
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

		private void OpenForEdit(string id)
		{
			FormHelper formHelper = new FormHelper();
			if (listType == DataComboType.VendorAddress)
			{
				string addressID = "";
				if (dataGridList.ActiveRow != null)
				{
					if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						addressID = "";
					}
					addressID = dataGridList.ActiveRow.Cells[1].Text.ToString();
				}
				formHelper.EditVendorAddress(id, addressID);
			}
			else if (listType == DataComboType.CustomerAddress)
			{
				string addressID2 = "";
				if (dataGridList.ActiveRow != null)
				{
					if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						addressID2 = "";
					}
					addressID2 = dataGridList.ActiveRow.Cells[1].Text.ToString();
				}
				formHelper.EditCustomerAddress(id, addressID2);
			}
			else
			{
				formHelper.OpenCardForEdit(listType, id);
			}
		}

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			EditSelectedRow();
		}

		private void EditSelectedRow()
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				OpenForEdit(selectedID);
			}
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			OpenForEdit("");
		}

		private void buttonGotoItem_Click(object sender, EventArgs e)
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				OpenForEdit(selectedID);
			}
		}

		private void CompanyAccountsListForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					if (listType == DataComboType.CustomerCategory)
					{
						toolStripButtonTreeView.Visible = true;
						toolStripButtonTreeView.Checked = false;
					}
					Init();
					LoadData();
					HideShowColumns();
					dataGridList.LoadLayout();
					dataGridList.ApplyUIDesign();
					dataGridList.ApplyFormat();
					dataGridList.FormatAllNumericColumns(null);
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
		}

		public void OnActivated()
		{
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
				if (dataGridList.ActiveRow != null)
				{
					if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						return "";
					}
					result = dataGridList.ActiveRow.Cells[0].Text.ToString();
				}
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private string GetSelectedIDTreeView()
		{
			try
			{
				string result = "";
				if (dataGridTreeList.ActiveRow != null)
				{
					if (dataGridTreeList.ActiveRow.GetType() != typeof(UltraGridRow))
					{
						return "";
					}
					result = dataGridTreeList.ActiveRow.Cells[0].Text.ToString();
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

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			Delete();
		}

		private void Delete()
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == "") && ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "Are you sure to delete this item?") != DialogResult.No)
			{
				try
				{
					GenericListTypes result = GenericListTypes.All;
					bool flag = false;
					if (listType == DataComboType.AccountGroup)
					{
						flag = Factory.AccountGroupsSystem.DeleteAccountGroup(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.PayrollItem)
					{
						flag = Factory.PayrollItemSystem.DeletePayrollItem(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Analysis)
					{
						flag = Factory.AnalysisSystem.DeleteAnalysis(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.LeadStatus)
					{
						flag = Factory.LeadStatusSystem.DeleteLeadStatus(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.AnalysisGroup)
					{
						flag = Factory.AnalysisGroupsSystem.DeleteAnalysisGroup(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Area)
					{
						flag = Factory.AreaSystem.DeleteArea(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.VendorAddress)
					{
						string addressID = "";
						if (dataGridList.ActiveRow != null)
						{
							if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
							{
								addressID = "";
							}
							addressID = dataGridList.ActiveRow.Cells[1].Text.ToString();
						}
						flag = Factory.VendorAddressSystem.DeleteVendorAddress(addressID, selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Bank)
					{
						flag = Factory.BankSystem.DeleteBank(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Driver)
					{
						flag = Factory.DriverSystem.DeleteDriver(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Benefit)
					{
						flag = Factory.BenefitSystem.DeleteBenefit(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.FiscalYear)
					{
						flag = Factory.FiscalYearSystem.DeleteFiscalYear(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.User)
					{
						flag = Factory.UserSystem.DeleteUser(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.UserGroup)
					{
						flag = Factory.UserGroupSystem.DeleteUserGroup(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Buyer)
					{
						flag = Factory.BuyerSystem.DeleteBuyer(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.FixedAsset)
					{
						flag = Factory.FixedAssetSystem.DeleteAsset(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.CompanyDocType)
					{
						flag = Factory.CompanyDocTypeSystem.DeleteCompanyDocType(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.ReturnedChequeReason)
					{
						flag = Factory.ReturnedChequeReasonSystem.DeleteReturnedChequeReason(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.CompanyDocument)
					{
						flag = Factory.CompanyDocumentSystem.DeleteCompanyDocument(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Contact)
					{
						flag = Factory.ContactSystem.DeleteContact(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Country)
					{
						flag = Factory.CountrySystem.DeleteCountry(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.CustomerCategory)
					{
						flag = Factory.EntityCategorySystem.DeleteEntityCategory(selectedID, EntityTypesEnum.Customers);
						goto IL_0a2f;
					}
					if (listType == DataComboType.CustomerAddress)
					{
						string customerID = "";
						if (dataGridList.ActiveRow != null)
						{
							if (dataGridList.ActiveRow.GetType() != typeof(UltraGridRow))
							{
								customerID = "";
							}
							customerID = dataGridList.ActiveRow.Cells[1].Text.ToString();
						}
						flag = Factory.CustomerAddressSystem.DeleteCustomerAddress(selectedID, customerID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Currency)
					{
						flag = Factory.CurrencySystem.DeleteCurrency(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.CustomerClass)
					{
						flag = Factory.CustomerClassSystem.DeleteCustomerClass(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.CustomerGroup)
					{
						flag = Factory.CustomerGroupSystem.DeleteCustomerGroup(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Deduction)
					{
						flag = Factory.DeductionSystem.DeleteDeduction(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Degree)
					{
						flag = Factory.DegreeSystem.DeleteDegree(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Department)
					{
						flag = Factory.DepartmentSystem.DeleteDepartment(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Destination)
					{
						flag = Factory.DestinationSystem.DeleteDestination(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.EmployeeDocument)
					{
						flag = Factory.EmployeeDocumentSystem.DeleteEmployeeDocument(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Division)
					{
						flag = Factory.DivisionSystem.DeleteDivision(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.EmployeeDocType)
					{
						flag = Factory.EmployeeDocTypeSystem.DeleteEmployeeDocType(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.EmployeeGroup)
					{
						flag = Factory.EmployeeGroupSystem.DeleteEmployeeGroup(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Grade)
					{
						flag = Factory.GradeSystem.DeleteGrade(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.LeaveType)
					{
						flag = Factory.LeaveTypeSystem.DeleteLeaveType(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.JobType)
					{
						flag = Factory.JobTypeSystem.DeleteJobType(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Location)
					{
						flag = Factory.LocationSystem.DeleteLocation(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Chequebook)
					{
						flag = Factory.ChequebookSystem.DeleteChequebook(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Nationality)
					{
						flag = Factory.NationalitySystem.DeleteNationality(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.PaymentMethod)
					{
						flag = Factory.PaymentMethodSystem.DeletePaymentMethod(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.PaymentTerm)
					{
						flag = Factory.TermSystem.DeleteTerm(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Position)
					{
						flag = Factory.PositionSystem.DeletePosition(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.PriceLevel)
					{
						flag = Factory.PriceLevelSystem.DeletePriceLevel(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.ProductBrand)
					{
						flag = Factory.ProductBrandSystem.DeleteProductBrand(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.ProductCategory)
					{
						flag = Factory.ProductCategorySystem.DeleteProductCategory(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.ProductClass)
					{
						flag = Factory.ProductClassSystem.DeleteProductClass(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.ProductManufacturer)
					{
						flag = Factory.ProductManufacturerSystem.DeleteProductManufacturer(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.ProductStyle)
					{
						flag = Factory.ProductStyleSystem.DeleteProductStyle(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Religion)
					{
						flag = Factory.ReligionSystem.DeleteReligion(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Salesperson)
					{
						flag = Factory.SalespersonSystem.DeleteSalesperson(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.ShippingMethod)
					{
						flag = Factory.ShippingMethodSystem.DeleteShippingMethod(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Skill)
					{
						flag = Factory.SkillSystem.DeleteSkill(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Sponsor)
					{
						flag = Factory.SponsorSystem.DeleteSponsor(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.TenancyContract)
					{
						flag = Factory.TenancyContractSystem.DeleteTenancyContract(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.TradeLicense)
					{
						flag = Factory.TradeLicenseSystem.DeleteTradeLicense(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Unit)
					{
						flag = Factory.UnitSystem.DeleteUnit(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.VendorClass)
					{
						flag = Factory.VendorClassSystem.DeleteVendorClass(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.VendorGroup)
					{
						flag = Factory.VendorGroupSystem.DeleteVendorGroup(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Visa)
					{
						flag = Factory.VisaSystem.DeleteVisa(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.CostCenter)
					{
						flag = Factory.CostCenterSystem.DeleteCostCenter(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Register)
					{
						flag = Factory.RegisterSystem.DeleteRegister(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Employee)
					{
						flag = Factory.EmployeeSystem.DeleteEmployee(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Job)
					{
						flag = Factory.JobSystem.DeleteJob(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Opportunity)
					{
						flag = Factory.OpportunitySystem.DeleteOpportunity(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Competitor)
					{
						flag = Factory.CompetitorSystem.DeleteCompetitor(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Campaign)
					{
						flag = Factory.CampaignSystem.DeleteCampaign(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Event)
					{
						flag = Factory.EventSystem.DeleteEvent(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.BankFacility)
					{
						flag = Factory.BankFacilitySystem.DeleteBankFacility(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.BankFacilityGroup)
					{
						flag = Factory.BankFacilityGroupSystem.DeleteBankFacilityGroup(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.ArrivalReportTemplate)
					{
						flag = Factory.ArrivalReportTemplateSystem.DeleteArrivalReportTemplate(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.ArrivalReportTemplate)
					{
						flag = Factory.JobSystem.DeleteJobFee(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Vehicle)
					{
						flag = Factory.VehicleSystem.DeleteVehicle(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.CustomerRelation)
					{
						flag = Factory.CustomerSystem.DeleteCustomerChildren(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.CustomerVendorLink)
					{
						flag = Factory.PartySystem.DeleteParty(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Equipment)
					{
						flag = Factory.EquipmentSystem.DeleteEquipment(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.PriceList)
					{
						flag = Factory.PriceListSystem.DeletePriceList(selectedID, string.Empty);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Candidate)
					{
						flag = Factory.CandidateSystem.DeleteCandidate(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.WorkLocation)
					{
						flag = Factory.WorkLocationSystem.DeleteWorkLocation(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.ExpenseCode)
					{
						flag = Factory.ExpenseCodeSystem.DeleteExpenseCode(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Transporter)
					{
						flag = Factory.TransporterSystem.DeleteTransporter(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.INCO)
					{
						flag = Factory.INCOSystem.DeleteINCO(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.Collateral)
					{
						flag = Factory.CollateralSystem.DeleteCollateral(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.CustomReport)
					{
						flag = Factory.CustomReportSystem.DeleteCustomReport(selectedID);
						goto IL_0a2f;
					}
					if (listType == DataComboType.CustomGadget)
					{
						flag = Factory.CustomGadgetSystem.DeleteCustomGadget(selectedID);
						goto IL_0a2f;
					}
					if (Enum.TryParse(listType.ToString(), out result))
					{
						flag = Factory.GenericListSystem.DeleteGenericList(selectedID, result);
						goto IL_0a2f;
					}
					ErrorHelper.ErrorMessage("General List Delete is not implemented for type: " + listType.ToString());
					goto end_IL_002e;
					IL_0a2f:
					if (flag)
					{
						try
						{
							GetSelectedItem().Delete(displayPrompt: false);
						}
						catch
						{
						}
					}
					else
					{
						ErrorHelper.ErrorMessage("Cannot delete this item. The item may be in use.");
					}
					end_IL_002e:;
				}
				catch (SqlException ex)
				{
					if (ex.Number == 547)
					{
						ErrorHelper.ErrorMessage("Cannot delete this item because it is in use or referenced by other records.");
					}
					else
					{
						ErrorHelper.ProcessError(ex);
					}
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
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

		private void toolStripButtonTreeView_Click(object sender, EventArgs e)
		{
			GetTreeview();
		}

		private void EditSelectedTreeRow()
		{
			string selectedIDTreeView = GetSelectedIDTreeView();
			if (!(selectedIDTreeView == ""))
			{
				OpenForEdit(selectedIDTreeView);
			}
		}

		private void GetTreeview()
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					PublicFunctions.StartWaiting(this);
					if (toolStripButtonTreeView.Checked)
					{
						dataGridTreeList.ApplyUIDesign();
						DataSet entityCategoryTree = Factory.EntityCategorySystem.GetEntityCategoryTree(toolStripButtonShowInactive.Checked, isHierarchy: true);
						dataGridTreeList.DataSource = entityCategoryTree.Tables["ParentCustomerCategory"];
						dataGridTreeList.ApplyUIDesign();
						foreach (UltraGridRow item in dataGridTreeList.Rows.GetRowEnumerator(GridRowType.DataRow, null, null))
						{
							if (bool.Parse(item.Cells["IsParent"].Value.ToString()))
							{
								item.Appearance.FontData.Bold = DefaultableBoolean.True;
							}
						}
						foreach (UltraGridBand band in dataGridTreeList.DisplayLayout.Bands)
						{
							band.Columns["IsParent"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
							band.Columns["IsParent"].Hidden = true;
							band.Columns["ParentID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
							band.Columns["ParentID"].Hidden = true;
						}
						dataGridTreeList.Rows.ExpandAll(recursive: true);
						dataGridTreeList.Visible = true;
						dataGridList.Visible = false;
						gridInUse = dataGridTreeList;
					}
					else
					{
						dataGridList.Visible = true;
						dataGridTreeList.Visible = false;
						gridInUse = dataGridList;
						LoadData();
					}
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
				}
				finally
				{
					PublicFunctions.EndWaiting(this);
				}
			}
		}
	}
}
