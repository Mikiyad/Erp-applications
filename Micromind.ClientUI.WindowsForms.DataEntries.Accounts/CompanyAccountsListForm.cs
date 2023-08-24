using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTree;
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
	public class CompanyAccountsListForm : Form, IDataForm, IDataList, IExternalReport
	{
		private UltraGrid gridInUse;

		private bool isFirstTimeActivating = true;

		private Hashtable listViewKeys = new Hashtable();

		private bool isReadOnly;

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet companyAccountData;

		private bool showInactiveItems;

		private XPButton buttonDone;

		private XPButton buttonNew;

		private XPButton buttonDelete;

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

		private TreeGridList treeGridList;

		private DataGridTreeList dataGridTreeList;

		private ToolStripButton toolStripButtonTreeView;

		private ContextMenuStrip contextMenuStripNew;

		private ToolStripMenuItem newGroupToolStripMenuItem;

		private ToolStripMenuItem newAccountToolStripMenuItem;

		public ContextMenuStrip contextMenuStripTree;

		private ToolStripMenuItem toolStripMenuItemOpen;

		private ToolStripMenuItem toolStripMenuItemNew;

		private ToolStripMenuItem toolStripMenuItemDelete;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem toolStripMenuItemAutoFit;

		private ToolStripMenuItem toolStripMenuItemColumnChooser;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem toolStripMenuItemExpandAll;

		private ToolStripMenuItem toolStripMenuItemCollapseAll;

		private ToolStripMenuItem newAccountToolStripMenuItem1;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripButton toolStripButtonFitText;

		private ToolStripButton toolStripButtonClearFilter;

		private ToolStrip toolStrip1;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1016;

		public ScreenTypes ScreenType => ScreenTypes.List;

		private bool ShowInactiveItems
		{
			get
			{
				return showInactiveItems;
			}
			set
			{
				showInactiveItems = value;
			}
		}

		public CompanyAccountsListForm()
		{
			InitializeComponent();
			base.Activated += CompanyAccountsListForm_Activated;
			dataGridList.DoubleClickRow += dataGridList_DoubleClickRow;
			dataGridList.NewMenuClicked += dataGridList_NewMenuClicked;
			dataGridList.OpenMenuClicked += dataGridList_OpenMenuClicked;
			dataGridList.DeleteMenuClicked += dataGridList_DeleteMenuClicked;
			treeGridList.InitializeDataNode += ultraTreeItems_InitializeDataNode;
			dataGridTreeList.InitializeRow += dataGridList_InitializeRow;
			dataGridTreeList.ContextMenuStrip = contextMenuStripTree;
			toolStripButtonTreeView.Checked = bool.Parse(Global.CompanySettings.GetSetting("AccountListTree", false).ToString());
			dataGridTreeList.DoubleClick += dataGridTreeList_DoubleClick;
			toolStripButtonTreeView.CheckedChanged += toolStripButtonTreeView_CheckedChanged;
			dataGridTreeList.AfterRowActivate += dataGridTreeList_AfterRowActivate;
			base.FormClosing += CompanyAccountsListForm_FormClosing;
			gridInUse = dataGridList;
		}

		private void CompanyAccountsListForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			dataGridList.SaveLayout();
			dataGridTreeList.SaveLayout();
		}

		private void toolStripButtonTreeView_CheckedChanged(object sender, EventArgs e)
		{
			toolStripButtonAllowGrouping.Enabled = !toolStripButtonTreeView.Checked;
		}

		private void dataGridTreeList_AfterRowActivate(object sender, EventArgs e)
		{
			if (!toolStripButtonTreeView.Checked)
			{
				return;
			}
			if (dataGridTreeList.ActiveRow.ChildBands.Count > 0)
			{
				if (dataGridTreeList.ActiveRow.ChildBands[0].Rows.Count > 0)
				{
					XPButton xPButton = buttonDelete;
					bool enabled = toolStripMenuItemDelete.Enabled = false;
					xPButton.Enabled = enabled;
				}
				else
				{
					XPButton xPButton2 = buttonDelete;
					bool enabled = toolStripMenuItemDelete.Enabled = true;
					xPButton2.Enabled = enabled;
				}
			}
			else
			{
				XPButton xPButton3 = buttonDelete;
				bool enabled = toolStripMenuItemDelete.Enabled = true;
				xPButton3.Enabled = enabled;
			}
		}

		private void dataGridTreeList_DoubleClick(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void dataGridList_InitializeRow(object sender, InitializeRowEventArgs e)
		{
			if (e.Row.Band.Index == 0 && e.Row.Cells["ParentID"].Value.ToString() != string.Empty)
			{
				e.Row.Hidden = true;
			}
		}

		private void ultraTreeItems_InitializeDataNode(object sender, InitializeDataNodeEventArgs e)
		{
			if (e.Node.Level == 0 && e.Node.Cells["ParentID"].Value.ToString() != string.Empty)
			{
				e.Node.Visible = false;
			}
		}

		private void dataGridList_DeleteMenuClicked(object sender, EventArgs e)
		{
			Delete();
		}

		private void dataGridList_OpenMenuClicked(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void dataGridList_NewMenuClicked(object sender, EventArgs e)
		{
			New(isGroup: false);
		}

		private void dataGridList_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			OpenForEdit();
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
			if (disposing && companyAccountData != null)
			{
				companyAccountData.Dispose();
				companyAccountData = null;
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.CompanyAccountsListForm));
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
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonDone = new Micromind.UISupport.XPButton();
			buttonOpen = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			contextMenuStripNew = new System.Windows.Forms.ContextMenuStrip(components);
			newGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			newAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			toolStripButtonFitText = new System.Windows.Forms.ToolStripButton();
			toolStripButtonTreeView = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonColumnChooser = new System.Windows.Forms.ToolStripButton();
			toolStripButtonClearFilter = new System.Windows.Forms.ToolStripButton();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			treeGridList = new Micromind.UISupport.TreeGridList(components);
			dataGridTreeList = new Micromind.UISupport.DataGridTreeList(components);
			contextMenuStripTree = new System.Windows.Forms.ContextMenuStrip(components);
			toolStripMenuItemNew = new System.Windows.Forms.ToolStripMenuItem();
			newAccountToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemAutoFit = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemColumnChooser = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemExpandAll = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItemCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons.SuspendLayout();
			contextMenuStripNew.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			((System.ComponentModel.ISupportInitialize)treeGridList).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridTreeList).BeginInit();
			contextMenuStripTree.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Controls.Add(buttonOpen);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 608);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(1067, 40);
			panelButtons.TabIndex = 1;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(1067, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 22);
			buttonDelete.TabIndex = 3;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(959, 8);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 22);
			buttonDone.TabIndex = 5;
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
			buttonOpen.Size = new System.Drawing.Size(96, 22);
			buttonOpen.TabIndex = 1;
			buttonOpen.Text = "&Open";
			buttonOpen.UseVisualStyleBackColor = false;
			buttonOpen.Click += new System.EventHandler(buttonGotoItem_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ContextMenuStrip = contextMenuStripNew;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 22);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			contextMenuStripNew.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				newGroupToolStripMenuItem,
				newAccountToolStripMenuItem
			});
			contextMenuStripNew.Name = "contextMenuStripNew";
			contextMenuStripNew.Size = new System.Drawing.Size(150, 48);
			newGroupToolStripMenuItem.Name = "newGroupToolStripMenuItem";
			newGroupToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			newGroupToolStripMenuItem.Text = "New Group...";
			newGroupToolStripMenuItem.Click += new System.EventHandler(newGroupToolStripMenuItem_Click);
			newAccountToolStripMenuItem.Name = "newAccountToolStripMenuItem";
			newAccountToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			newAccountToolStripMenuItem.Text = "New Account...";
			newAccountToolStripMenuItem.Click += new System.EventHandler(newAccountToolStripMenuItem_Click);
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
				toolStripButtonFitText,
				toolStripButtonTreeView,
				toolStripSeparator3,
				toolStripButtonColumnChooser,
				toolStripButtonClearFilter
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(1067, 31);
			toolStrip1.TabIndex = 289;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonRefresh.Image = Micromind.ClientUI.Properties.Resources.Refresh;
			toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonRefresh.Name = "toolStripButtonRefresh";
			toolStripButtonRefresh.Size = new System.Drawing.Size(73, 28);
			toolStripButtonRefresh.Text = "Refresh";
			toolStripButtonRefresh.Click += new System.EventHandler(toolStripButtonRefresh_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				microsoftExcelToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(76, 28);
			toolStripDropDownButton1.Text = "Export";
			microsoftExcelToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Excel;
			microsoftExcelToolStripMenuItem.Name = "microsoftExcelToolStripMenuItem";
			microsoftExcelToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			microsoftExcelToolStripMenuItem.Text = "Microsoft Excel";
			microsoftExcelToolStripMenuItem.Click += new System.EventHandler(microsoftExcelToolStripMenuItem_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonShowInactive.CheckOnClick = true;
			toolStripButtonShowInactive.Image = Micromind.ClientUI.Properties.Resources.ShowInactive;
			toolStripButtonShowInactive.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonShowInactive.Name = "toolStripButtonShowInactive";
			toolStripButtonShowInactive.Size = new System.Drawing.Size(103, 28);
			toolStripButtonShowInactive.Text = "Show Inactive";
			toolStripButtonShowInactive.Click += new System.EventHandler(toolStripButtonShowInactive_Click);
			toolStripButtonAllowGrouping.CheckOnClick = true;
			toolStripButtonAllowGrouping.Image = Micromind.ClientUI.Properties.Resources.Groupby;
			toolStripButtonAllowGrouping.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAllowGrouping.Name = "toolStripButtonAllowGrouping";
			toolStripButtonAllowGrouping.Size = new System.Drawing.Size(106, 28);
			toolStripButtonAllowGrouping.Text = "Allow Grouping";
			toolStripButtonAllowGrouping.Click += new System.EventHandler(toolStripButtonAllowGrouping_Click);
			toolStripButtonAutoFit.Image = Micromind.ClientUI.Properties.Resources.autofit;
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(96, 28);
			toolStripButtonAutoFit.Text = "Fit to Screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripButtonFitText.Image = Micromind.ClientUI.Properties.Resources.colbestsize;
			toolStripButtonFitText.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFitText.Name = "toolStripButtonFitText";
			toolStripButtonFitText.Size = new System.Drawing.Size(85, 28);
			toolStripButtonFitText.Text = "Fit to Text";
			toolStripButtonFitText.Click += new System.EventHandler(toolStripButtonFitText_Click);
			toolStripButtonTreeView.Checked = true;
			toolStripButtonTreeView.CheckOnClick = true;
			toolStripButtonTreeView.CheckState = System.Windows.Forms.CheckState.Checked;
			toolStripButtonTreeView.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonTreeView.Image");
			toolStripButtonTreeView.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonTreeView.Name = "toolStripButtonTreeView";
			toolStripButtonTreeView.Size = new System.Drawing.Size(82, 28);
			toolStripButtonTreeView.Text = "Tree View";
			toolStripButtonTreeView.Click += new System.EventHandler(toolStripButtonTreeView_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonColumnChooser.Image = Micromind.ClientUI.Properties.Resources.ColumnChooser;
			toolStripButtonColumnChooser.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonColumnChooser.Name = "toolStripButtonColumnChooser";
			toolStripButtonColumnChooser.Size = new System.Drawing.Size(75, 28);
			toolStripButtonColumnChooser.Text = "Columns";
			toolStripButtonColumnChooser.Click += new System.EventHandler(toolStripButtonColumnChooser_Click);
			toolStripButtonClearFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonClearFilter.Image = Micromind.ClientUI.Properties.Resources.clearfilter;
			toolStripButtonClearFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonClearFilter.Name = "toolStripButtonClearFilter";
			toolStripButtonClearFilter.Size = new System.Drawing.Size(28, 28);
			toolStripButtonClearFilter.Text = "Clear All Filters";
			toolStripButtonClearFilter.Click += new System.EventHandler(toolStripButtonClearFilter_Click);
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
			dataGridList.Location = new System.Drawing.Point(12, 37);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(1043, 560);
			dataGridList.TabIndex = 290;
			dataGridList.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridList;
			treeGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			treeGridList.Location = new System.Drawing.Point(12, 37);
			treeGridList.Name = "treeGridList";
			treeGridList.ShowDeleteMenu = false;
			treeGridList.ShowMinusInRed = true;
			treeGridList.ShowNewMenu = false;
			treeGridList.Size = new System.Drawing.Size(804, 560);
			treeGridList.TabIndex = 291;
			treeGridList.Visible = false;
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
			dataGridTreeList.Location = new System.Drawing.Point(12, 37);
			dataGridTreeList.Name = "dataGridTreeList";
			dataGridTreeList.ShowDeleteMenu = false;
			dataGridTreeList.ShowMinusInRed = true;
			dataGridTreeList.ShowNewMenu = false;
			dataGridTreeList.Size = new System.Drawing.Size(1043, 560);
			dataGridTreeList.TabIndex = 293;
			dataGridTreeList.Text = "dataGridTreeList1";
			dataGridTreeList.Visible = false;
			contextMenuStripTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
			{
				toolStripMenuItemNew,
				newAccountToolStripMenuItem1,
				toolStripMenuItemOpen,
				toolStripSeparator6,
				toolStripMenuItemDelete,
				toolStripSeparator4,
				toolStripMenuItemAutoFit,
				toolStripMenuItemColumnChooser,
				toolStripSeparator5,
				toolStripMenuItemExpandAll,
				toolStripMenuItemCollapseAll
			});
			contextMenuStripTree.Name = "contextMenuStrip1";
			contextMenuStripTree.Size = new System.Drawing.Size(165, 198);
			toolStripMenuItemNew.Name = "toolStripMenuItemNew";
			toolStripMenuItemNew.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemNew.Text = "New Group...";
			toolStripMenuItemNew.Click += new System.EventHandler(toolStripMenuItemNew_Click);
			newAccountToolStripMenuItem1.Name = "newAccountToolStripMenuItem1";
			newAccountToolStripMenuItem1.Size = new System.Drawing.Size(164, 22);
			newAccountToolStripMenuItem1.Text = "New Account...";
			newAccountToolStripMenuItem1.Click += new System.EventHandler(newAccountToolStripMenuItem1_Click);
			toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
			toolStripMenuItemOpen.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemOpen.Text = "Open...";
			toolStripMenuItemOpen.Click += new System.EventHandler(toolStripMenuItemOpen_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(161, 6);
			toolStripMenuItemDelete.Image = Micromind.ClientUI.Properties.Resources.Delete;
			toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			toolStripMenuItemDelete.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemDelete.Text = "Delete";
			toolStripMenuItemDelete.Click += new System.EventHandler(toolStripMenuItemDelete_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(161, 6);
			toolStripMenuItemAutoFit.Name = "toolStripMenuItemAutoFit";
			toolStripMenuItemAutoFit.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemAutoFit.Text = "Fit to Screen";
			toolStripMenuItemColumnChooser.Name = "toolStripMenuItemColumnChooser";
			toolStripMenuItemColumnChooser.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemColumnChooser.Text = "Column Chooser...";
			toolStripMenuItemColumnChooser.Click += new System.EventHandler(toolStripMenuItemColumnChooser_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(161, 6);
			toolStripMenuItemExpandAll.Image = Micromind.ClientUI.Properties.Resources._001_03;
			toolStripMenuItemExpandAll.Name = "toolStripMenuItemExpandAll";
			toolStripMenuItemExpandAll.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemExpandAll.Text = "Expand All";
			toolStripMenuItemExpandAll.Click += new System.EventHandler(toolStripMenuItemExpandAll_Click);
			toolStripMenuItemCollapseAll.Image = Micromind.ClientUI.Properties.Resources._001_04;
			toolStripMenuItemCollapseAll.Name = "toolStripMenuItemCollapseAll";
			toolStripMenuItemCollapseAll.Size = new System.Drawing.Size(164, 22);
			toolStripMenuItemCollapseAll.Text = "Collapse All";
			toolStripMenuItemCollapseAll.Click += new System.EventHandler(toolStripMenuItemCollapseAll_Click);
			base.AcceptButton = buttonOpen;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(1067, 648);
			base.Controls.Add(dataGridTreeList);
			base.Controls.Add(treeGridList);
			base.Controls.Add(dataGridList);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "CompanyAccountsListForm";
			Text = "Account List";
			base.Load += new System.EventHandler(CompanyAccountsListForm_Load);
			panelButtons.ResumeLayout(false);
			contextMenuStripNew.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			((System.ComponentModel.ISupportInitialize)treeGridList).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridTreeList).EndInit();
			contextMenuStripTree.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
		}

		private void LoadData()
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					PublicFunctions.StartWaiting(this);
					if (toolStripButtonTreeView.Checked)
					{
						dataGridTreeList.ApplyUIDesign();
						DataSet accountsList = Factory.CompanyAccountSystem.GetAccountsList(toolStripButtonShowInactive.Checked, isHierarchy: true);
						dataGridTreeList.DataSource = accountsList.Tables["Account_Group"];
						dataGridTreeList.ApplyUIDesign();
						foreach (UltraGridRow item in dataGridTreeList.Rows.GetRowEnumerator(GridRowType.DataRow, null, null))
						{
							if (bool.Parse(item.Cells["IsGroup"].Value.ToString()))
							{
								item.Appearance.FontData.Bold = DefaultableBoolean.True;
							}
						}
						foreach (UltraGridBand band in dataGridTreeList.DisplayLayout.Bands)
						{
							band.Columns["I"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
							band.Columns["I"].Hidden = true;
							band.Columns["IsGroup"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
							band.Columns["IsGroup"].Hidden = true;
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
						companyAccountData = Factory.CompanyAccountSystem.GetAccountsList(toolStripButtonShowInactive.Checked, isHierarchy: false);
						dataGridList.DataSource = companyAccountData;
						dataGridList.ApplyFormat();
						dataGridList.DisplayLayout.Bands[0].Columns["I"].CellDisplayStyle = CellDisplayStyle.FormattedText;
						dataGridList.DisplayLayout.Bands[0].Columns["I"].Width = 18;
						dataGridList.DisplayLayout.Bands[0].Columns["I"].MaxWidth = 18;
						dataGridList.DisplayLayout.Bands[0].Columns["I"].MinWidth = 18;
						dataGridList.DisplayLayout.Bands[0].Columns["I"].LockedWidth = true;
						dataGridList.DisplayLayout.Bands[0].Columns["I"].AllowRowFiltering = DefaultableBoolean.False;
						dataGridList.SetInactiveColumn("I");
						dataGridList.AddColumnRowCount(dataGridList.DisplayLayout.Bands[0].Columns["Account Code"]);
						dataGridList.Visible = true;
						dataGridTreeList.Visible = false;
						gridInUse = dataGridList;
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

		private void HideShowColumns()
		{
			checked
			{
				if (dataGridList.DisplayLayout.Bands.Count != 0 && !toolStripButtonTreeView.Checked)
				{
					int count = dataGridList.DisplayLayout.Bands[0].Columns.Count;
					dataGridList.DisplayLayout.Bands[0].Columns[count - 1].Hidden = true;
					dataGridList.DisplayLayout.Bands[0].Columns[count - 2].Hidden = true;
					dataGridList.DisplayLayout.Bands[0].Columns[count - 3].Hidden = true;
					dataGridList.DisplayLayout.Bands[0].Columns[count - 4].Hidden = true;
				}
			}
		}

		private void buttonDone_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OpenForEdit()
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				if (IsSelectedRowGroup())
				{
					FormActivator.BringFormToFront(FormActivator.AccountGroupDetailsFormObj);
					FormActivator.AccountGroupDetailsFormObj.LoadData(selectedID);
				}
				else
				{
					FormActivator.BringFormToFront(FormActivator.CompanyAccountDetailsFormObj);
					FormActivator.CompanyAccountDetailsFormObj.LoadData(selectedID);
				}
			}
		}

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			buttonNew.ContextMenuStrip.Show(buttonNew, 0, checked(-1 * buttonNew.ContextMenuStrip.Height));
		}

		private void New(bool isGroup)
		{
			if (isGroup)
			{
				FormActivator.BringFormToFront(FormActivator.AccountGroupDetailsFormObj);
			}
			else
			{
				FormActivator.BringFormToFront(FormActivator.CompanyAccountDetailsFormObj);
			}
		}

		private void buttonGotoItem_Click(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void CompanyAccountsListForm_Load(object sender, EventArgs e)
		{
			try
			{
				Init();
				LoadData();
				dataGridList.ApplyUIDesign();
				dataGridList.LoadLayout();
				HideShowColumns();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
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

		private bool IsSelectedRowGroup()
		{
			if (!toolStripButtonTreeView.Checked)
			{
				return false;
			}
			if (gridInUse.ActiveRow == null)
			{
				return false;
			}
			if (gridInUse.ActiveRow.GetType() != typeof(UltraGridRow))
			{
				return false;
			}
			return bool.Parse(gridInUse.ActiveRow.Cells["IsGroup"].Text.ToString());
		}

		private string GetSelectedID()
		{
			if (gridInUse.ActiveRow == null)
			{
				return "";
			}
			if (gridInUse.ActiveRow.GetType() != typeof(UltraGridRow))
			{
				return "";
			}
			return gridInUse.ActiveRow.Cells["Account Code"].Text.ToString();
		}

		private string GetSelectedParentID()
		{
			if (!toolStripButtonTreeView.Checked)
			{
				return "";
			}
			if (gridInUse.ActiveRow == null)
			{
				return "";
			}
			if (gridInUse.ActiveRow.GetType() != typeof(UltraGridRow))
			{
				return "";
			}
			return gridInUse.ActiveRow.Cells["ParentID"].Text.ToString();
		}

		private UltraGridRow GetSelectedItem()
		{
			if (gridInUse.ActiveRow != null)
			{
				return gridInUse.ActiveRow;
			}
			return null;
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			Delete();
		}

		private void Delete()
		{
			if (!isReadOnly)
			{
				string selectedID = GetSelectedID();
				bool flag = IsSelectedRowGroup();
				if (selectedID != "")
				{
					try
					{
						if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this record?") == DialogResult.Yes)
						{
							PublicFunctions.StartWaiting(this);
							if (flag)
							{
								if (Factory.AccountGroupsSystem.DeleteAccountGroup(selectedID))
								{
									try
									{
										GetSelectedItem().Delete(displayPrompt: false);
									}
									catch
									{
									}
								}
							}
							else if (Factory.CompanyAccountSystem.DeleteAccount(selectedID))
							{
								try
								{
									GetSelectedItem().Delete(displayPrompt: false);
								}
								catch
								{
								}
							}
						}
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
					finally
					{
						PublicFunctions.EndWaiting(this);
					}
				}
			}
		}

		private bool SetSecurity()
		{
			bool result = true;
			switch (Security.GetUserAccessRight(GetScreenArea()))
			{
			case AccessRigths.NoAccess:
				buttonDelete.Enabled = false;
				menuItemDelete.Enabled = false;
				menuItemInactive.Visible = false;
				Global.ChangeApplicationStatusMessage(Text + ": " + SR.GetString("00162"));
				result = false;
				isReadOnly = true;
				break;
			case AccessRigths.ReadOnlyAccess:
				buttonDelete.Enabled = false;
				menuItemDelete.Enabled = false;
				menuItemInactive.Visible = false;
				Global.ChangeApplicationStatusMessage(Text + ": " + SR.GetString("00324"));
				isReadOnly = true;
				break;
			default:
				buttonDelete.Visible = true;
				menuItemDelete.Visible = true;
				isReadOnly = false;
				break;
			}
			return result;
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
				ultraGridPrintDocument1.Grid = gridInUse;
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
			new DataExportHelper().ExportToExcel(gridInUse);
		}

		private void toolStripButtonAllowGrouping_Click(object sender, EventArgs e)
		{
			dataGridList.DisplayLayout.GroupByBox.Hidden = !toolStripButtonAllowGrouping.Checked;
		}

		private void toolStripButtonColumnChooser_Click(object sender, EventArgs e)
		{
			gridInUse.ShowColumnChooser();
		}

		private void toolStripButtonAutoFit_Click(object sender, EventArgs e)
		{
			dataGridList.AutoFitColumns();
		}

		private void toolStripButtonShowInactive_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void toolStripButtonTreeView_Click(object sender, EventArgs e)
		{
			Global.CompanySettings.SaveSetting("AccountListTree", toolStripButtonTreeView.Checked);
			LoadData();
		}

		private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			Delete();
		}

		private void toolStripMenuItemExpandAll_Click(object sender, EventArgs e)
		{
			dataGridTreeList.Rows.ExpandAll(recursive: true);
		}

		private void toolStripMenuItemCollapseAll_Click(object sender, EventArgs e)
		{
			dataGridTreeList.Rows.CollapseAll(recursive: true);
		}

		private void toolStripMenuItemNew_Click(object sender, EventArgs e)
		{
			NewGroup();
		}

		private void newGroupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewGroup();
		}

		private void NewAccount()
		{
			FormActivator.BringFormToFront(FormActivator.CompanyAccountDetailsFormObj);
			if (toolStripButtonTreeView.Checked)
			{
				if (IsSelectedRowGroup())
				{
					FormActivator.CompanyAccountDetailsFormObj.GroupID = GetSelectedID();
				}
				else
				{
					FormActivator.CompanyAccountDetailsFormObj.GroupID = GetSelectedParentID();
				}
			}
		}

		private void NewGroup()
		{
			FormActivator.BringFormToFront(FormActivator.AccountGroupDetailsFormObj);
			if (toolStripButtonTreeView.Checked)
			{
				if (IsSelectedRowGroup())
				{
					FormActivator.AccountGroupDetailsFormObj.GroupID = GetSelectedID();
				}
				else
				{
					FormActivator.AccountGroupDetailsFormObj.GroupID = GetSelectedParentID();
				}
			}
		}

		private void newAccountToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewAccount();
		}

		private void newAccountToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			NewAccount();
		}

		private void toolStripMenuItemColumnChooser_Click(object sender, EventArgs e)
		{
			gridInUse.ShowColumnChooser();
		}

		private void toolStripButtonFitText_Click(object sender, EventArgs e)
		{
			dataGridList.AutoSizeColumns();
		}

		private void toolStripButtonClearFilter_Click(object sender, EventArgs e)
		{
			if (dataGridList.DisplayLayout != null && dataGridList.DisplayLayout.Bands.Count > 0)
			{
				dataGridList.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
			}
		}
	}
}
