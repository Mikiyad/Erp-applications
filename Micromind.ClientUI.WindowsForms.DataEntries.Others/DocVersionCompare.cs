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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class DocVersionCompare : Form, IForm
	{
		private bool isFirstTimeActivating = true;

		private DataSet newData;

		private DataSet oldData;

		private Hashtable listViewKeys = new Hashtable();

		private bool isReadOnly;

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet productData;

		private bool showInactiveItems;

		private XPButton buttonDone;

		private Panel panelButtons;

		private Line linePanelDown;

		private IContainer components;

		private DataGridList dataGridNew;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonRefresh;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem toolStripMenuItemLocationWiseOnhand;

		private TreeView treeViewList;

		private SplitContainer splitContainer1;

		private ContextMenuStrip contextMenuStripTreeView;

		private ToolStripMenuItem newGroupToolStripMenuItem;

		private ImageList imageList1;

		private ToolStripMenuItem newReportToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem editToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem deleteToolStripMenuItem;

		private System.Windows.Forms.ToolTip toolTip1;

		private Label labelViewListName;

		private ToolStripMenuItem favoriteToolStripMenuItem;

		private Label label1;

		private ToolStripMenuItem moveUpToolStripMenuItem;

		private ToolStripMenuItem moveDownToolStripMenuItem;

		private SplitContainer splitContainer2;

		private DataGridList dataGridOld;

		private Panel panel1;

		private Panel panel2;

		private Label labelNewDataCaption;

		private Label labelOldDataCaption;

		private Label labelEntityName;

		private ToolStrip toolStrip1;

		private bool isSubReport;

		private ScreenAccessRight screenRight;

		public DataSet NewData
		{
			get
			{
				return newData;
			}
			set
			{
				newData = value;
			}
		}

		public DataSet OldData
		{
			get
			{
				return oldData;
			}
			set
			{
				oldData = value;
			}
		}

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4011;

		public ScreenTypes ScreenType => ScreenTypes.List;

		public bool IsSubReport
		{
			get
			{
				return isSubReport;
			}
			set
			{
				isSubReport = value;
				if (value)
				{
					treeViewList.Visible = false;
					splitContainer1.SplitterDistance = 0;
					splitContainer1.Panel1MinSize = 0;
					splitContainer1.Left = 0;
					splitContainer1.Panel1.Hide();
					splitContainer1.IsSplitterFixed = true;
				}
			}
		}

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

		public DocVersionCompare()
		{
			InitializeComponent();
			base.Activated += ProductListForm_Activated;
			treeViewList.ContextMenuStrip = contextMenuStripTreeView;
			treeViewList.NodeMouseClick += treeView1_NodeMouseClick;
			treeViewList.Click += treeView1_Click;
			treeViewList.AfterSelect += treeView1_AfterSelect;
			treeViewList.MouseDown += TreeViewList_MouseUp;
			treeViewList.BeforeSelect += treeView1_BeforeSelect;
			base.FormClosing += VendorListForm_FormClosing;
			dataGridNew.AllowUnfittedView = true;
			treeViewList.LabelEdit = false;
			contextMenuStripTreeView.Opening += ContextMenuStripTreeView_Opening;
			treeViewList.ItemHeight = 18;
		}

		private void TreeViewList_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Point pt = new Point(e.X, e.Y);
				TreeNode nodeAt = treeViewList.GetNodeAt(pt);
				if (nodeAt != null)
				{
					treeViewList.SelectedNode = nodeAt;
				}
			}
		}

		private void ContextMenuStripTreeView_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.ImageIndex == 2)
				{
					string name = treeViewList.SelectedNode.Name;
					if (name == null || name == "")
					{
						favoriteToolStripMenuItem.Enabled = false;
					}
					else
					{
						favoriteToolStripMenuItem.Enabled = true;
						if (Factory.UserFavoriteSystem.IsUserFavorite(Global.CurrentUser, 1, name))
						{
							favoriteToolStripMenuItem.Checked = true;
						}
						else
						{
							favoriteToolStripMenuItem.Checked = false;
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.ImageIndex == 2)
			{
				dataGridNew.SaveLayout(treeViewList.SelectedNode.Name, saveFullSetting: true);
			}
		}

		private void VendorListForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				LoadReport();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadReport()
		{
			try
			{
				TreeNode selectedNode = treeViewList.SelectedNode;
				if (selectedNode != null && selectedNode.ImageIndex == 2)
				{
					labelViewListName.Text = "";
					if (treeViewList.SelectedNode != null && selectedNode.ImageIndex == 2)
					{
						labelViewListName.Text = treeViewList.SelectedNode.Text;
						LoadChanges(selectedNode.Name);
						try
						{
							dataGridNew.LoadLayout(selectedNode.Name);
							dataGridNew.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
						}
						catch
						{
						}
						dataGridNew.ApplyFormat();
						dataGridNew.FormatAllNumericColumns(null);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadChanges(string tableName)
		{
		}

		private void treeView1_Click(object sender, EventArgs e)
		{
		}

		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node != null)
			{
				CompareTable(e.Node.Text);
			}
		}

		private void toolStripMenuItemLocationWiseOnhand_Click(object sender, EventArgs e)
		{
		}

		private void dataGridList_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
		{
			_ = Cursor.Position;
			if (e.Cell.Column.Key == "P" && e.Cell.Value.ToString() == "1")
			{
				PublicFunctions.GetProductThumbnailImage(e.Cell.Row.Cells["Item Code"].Value.ToString(), isProductParentID: false);
			}
			else
			{
				OpenForEdit();
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
			New();
		}

		private void ProductListForm_Activated(object sender, EventArgs e)
		{
			if (isFirstTimeActivating)
			{
				isFirstTimeActivating = false;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && productData != null)
			{
				productData.Dispose();
				productData = null;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.DocVersionCompare));
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
			buttonDone = new Micromind.UISupport.XPButton();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			dataGridNew = new Micromind.UISupport.DataGridList(components);
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemLocationWiseOnhand = new System.Windows.Forms.ToolStripMenuItem();
			treeViewList = new System.Windows.Forms.TreeView();
			contextMenuStripTreeView = new System.Windows.Forms.ContextMenuStrip(components);
			newGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			newReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			favoriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			imageList1 = new System.Windows.Forms.ImageList(components);
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			label1 = new System.Windows.Forms.Label();
			splitContainer2 = new System.Windows.Forms.SplitContainer();
			dataGridOld = new Micromind.UISupport.DataGridList(components);
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			labelViewListName = new System.Windows.Forms.Label();
			panel1 = new System.Windows.Forms.Panel();
			panel2 = new System.Windows.Forms.Panel();
			labelNewDataCaption = new System.Windows.Forms.Label();
			labelOldDataCaption = new System.Windows.Forms.Label();
			labelEntityName = new System.Windows.Forms.Label();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridNew).BeginInit();
			contextMenuStripTreeView.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
			splitContainer2.Panel1.SuspendLayout();
			splitContainer2.Panel2.SuspendLayout();
			splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridOld).BeginInit();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 581);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(993, 40);
			panelButtons.TabIndex = 1;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(993, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(885, 8);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 24);
			buttonDone.TabIndex = 5;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(993, 31);
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
			dataGridNew.AllowUnfittedView = false;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridNew.DisplayLayout.Appearance = appearance;
			dataGridNew.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridNew.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridNew.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridNew.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridNew.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridNew.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridNew.DisplayLayout.MaxColScrollRegions = 1;
			dataGridNew.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridNew.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridNew.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridNew.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridNew.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridNew.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridNew.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridNew.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridNew.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridNew.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridNew.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridNew.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridNew.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridNew.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridNew.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridNew.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridNew.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridNew.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridNew.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridNew.Dock = System.Windows.Forms.DockStyle.Fill;
			dataGridNew.LoadLayoutFailed = false;
			dataGridNew.Location = new System.Drawing.Point(0, 25);
			dataGridNew.Name = "dataGridNew";
			dataGridNew.ShowDeleteMenu = false;
			dataGridNew.ShowMinusInRed = true;
			dataGridNew.ShowNewMenu = false;
			dataGridNew.Size = new System.Drawing.Size(356, 489);
			dataGridNew.TabIndex = 290;
			dataGridNew.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridNew;
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(200, 6);
			toolStripMenuItemLocationWiseOnhand.Name = "toolStripMenuItemLocationWiseOnhand";
			toolStripMenuItemLocationWiseOnhand.Size = new System.Drawing.Size(203, 22);
			toolStripMenuItemLocationWiseOnhand.Text = "Location Wise Onhand...";
			treeViewList.AllowDrop = true;
			treeViewList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			treeViewList.ContextMenuStrip = contextMenuStripTreeView;
			treeViewList.FullRowSelect = true;
			treeViewList.HideSelection = false;
			treeViewList.ImageIndex = 0;
			treeViewList.ImageList = imageList1;
			treeViewList.Location = new System.Drawing.Point(0, 26);
			treeViewList.Name = "treeViewList";
			treeViewList.SelectedImageIndex = 1;
			treeViewList.Size = new System.Drawing.Size(230, 484);
			treeViewList.StateImageList = imageList1;
			treeViewList.TabIndex = 291;
			contextMenuStripTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[9]
			{
				newGroupToolStripMenuItem,
				newReportToolStripMenuItem,
				toolStripSeparator5,
				editToolStripMenuItem,
				favoriteToolStripMenuItem,
				toolStripSeparator6,
				deleteToolStripMenuItem,
				moveUpToolStripMenuItem,
				moveDownToolStripMenuItem
			});
			contextMenuStripTreeView.Name = "contextMenuStripTreeView";
			contextMenuStripTreeView.Size = new System.Drawing.Size(146, 170);
			newGroupToolStripMenuItem.Name = "newGroupToolStripMenuItem";
			newGroupToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			newGroupToolStripMenuItem.Text = "New Group...";
			newReportToolStripMenuItem.Name = "newReportToolStripMenuItem";
			newReportToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			newReportToolStripMenuItem.Text = "New Report...";
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(142, 6);
			editToolStripMenuItem.Name = "editToolStripMenuItem";
			editToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			editToolStripMenuItem.Text = "Edit...";
			favoriteToolStripMenuItem.CheckOnClick = true;
			favoriteToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.star_16x16;
			favoriteToolStripMenuItem.Name = "favoriteToolStripMenuItem";
			favoriteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			favoriteToolStripMenuItem.Text = "Favorite";
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(142, 6);
			deleteToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			deleteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			deleteToolStripMenuItem.Text = "Delete";
			moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
			moveUpToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			moveUpToolStripMenuItem.Text = "Move Up";
			moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
			moveDownToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			moveDownToolStripMenuItem.Text = "Move Down";
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "folder_closed.png");
			imageList1.Images.SetKeyName(1, "folder_open.png");
			imageList1.Images.SetKeyName(2, "repport16x16.png");
			splitContainer1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			splitContainer1.Location = new System.Drawing.Point(12, 61);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(label1);
			splitContainer1.Panel1.Controls.Add(treeViewList);
			splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(splitContainer1_Panel1_Paint);
			splitContainer1.Panel2.Controls.Add(splitContainer2);
			splitContainer1.Size = new System.Drawing.Size(969, 514);
			splitContainer1.SplitterDistance = 233;
			splitContainer1.TabIndex = 292;
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 10);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(42, 13);
			label1.TabIndex = 293;
			label1.Text = "Tables:";
			splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer2.Location = new System.Drawing.Point(0, 0);
			splitContainer2.Name = "splitContainer2";
			splitContainer2.Panel1.Controls.Add(dataGridNew);
			splitContainer2.Panel1.Controls.Add(panel1);
			splitContainer2.Panel2.Controls.Add(dataGridOld);
			splitContainer2.Panel2.Controls.Add(panel2);
			splitContainer2.Size = new System.Drawing.Size(732, 514);
			splitContainer2.SplitterDistance = 356;
			splitContainer2.TabIndex = 291;
			dataGridOld.AllowUnfittedView = false;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridOld.DisplayLayout.Appearance = appearance13;
			dataGridOld.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridOld.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridOld.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridOld.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridOld.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridOld.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridOld.DisplayLayout.MaxColScrollRegions = 1;
			dataGridOld.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridOld.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridOld.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridOld.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridOld.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridOld.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridOld.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridOld.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridOld.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridOld.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridOld.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridOld.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridOld.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridOld.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridOld.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridOld.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridOld.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridOld.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridOld.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridOld.Dock = System.Windows.Forms.DockStyle.Fill;
			dataGridOld.LoadLayoutFailed = false;
			dataGridOld.Location = new System.Drawing.Point(0, 25);
			dataGridOld.Name = "dataGridOld";
			dataGridOld.ShowDeleteMenu = false;
			dataGridOld.ShowMinusInRed = true;
			dataGridOld.ShowNewMenu = false;
			dataGridOld.Size = new System.Drawing.Size(372, 489);
			dataGridOld.TabIndex = 291;
			dataGridOld.Text = "dataGridList1";
			labelViewListName.AutoSize = true;
			labelViewListName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelViewListName.Location = new System.Drawing.Point(526, 30);
			labelViewListName.Name = "labelViewListName";
			labelViewListName.Size = new System.Drawing.Size(0, 13);
			labelViewListName.TabIndex = 295;
			panel1.Controls.Add(labelNewDataCaption);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(356, 25);
			panel1.TabIndex = 291;
			panel2.Controls.Add(labelOldDataCaption);
			panel2.Dock = System.Windows.Forms.DockStyle.Top;
			panel2.Location = new System.Drawing.Point(0, 0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(372, 25);
			panel2.TabIndex = 292;
			labelNewDataCaption.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelNewDataCaption.AutoSize = true;
			labelNewDataCaption.Location = new System.Drawing.Point(3, 9);
			labelNewDataCaption.Name = "labelNewDataCaption";
			labelNewDataCaption.Size = new System.Drawing.Size(0, 13);
			labelNewDataCaption.TabIndex = 294;
			labelOldDataCaption.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelOldDataCaption.AutoSize = true;
			labelOldDataCaption.Location = new System.Drawing.Point(3, 9);
			labelOldDataCaption.Name = "labelOldDataCaption";
			labelOldDataCaption.Size = new System.Drawing.Size(0, 13);
			labelOldDataCaption.TabIndex = 294;
			labelEntityName.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelEntityName.AutoSize = true;
			labelEntityName.Location = new System.Drawing.Point(12, 40);
			labelEntityName.Name = "labelEntityName";
			labelEntityName.Size = new System.Drawing.Size(0, 13);
			labelEntityName.TabIndex = 296;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(993, 621);
			base.Controls.Add(labelEntityName);
			base.Controls.Add(labelViewListName);
			base.Controls.Add(splitContainer1);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "DocVersionCompare";
			Text = "History";
			base.Load += new System.EventHandler(ProductListForm_Load);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridNew).EndInit();
			contextMenuStripTreeView.ResumeLayout(false);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel1.PerformLayout();
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			splitContainer2.Panel1.ResumeLayout(false);
			splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
			splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridOld).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
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
				dataGridNew.ApplyUIDesign();
				dataGridOld.ApplyUIDesign();
				try
				{
					PublicFunctions.StartWaiting(this);
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
		}

		private void SetCardView(bool val)
		{
			if (val)
			{
				dataGridNew.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				dataGridNew.DisplayLayout.UseFixedHeaders = true;
				dataGridNew.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.SingleSummary;
				dataGridNew.DisplayLayout.Override.ActiveRowAppearance.BackColor = Color.White;
				dataGridNew.DisplayLayout.Override.ActiveRowAppearance.ForeColor = Color.Black;
				dataGridNew.DisplayLayout.Bands[0].CardView = true;
				dataGridNew.DisplayLayout.Override.CellClickAction = CellClickAction.CellSelect;
				dataGridNew.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.False;
				dataGridNew.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.False;
				dataGridNew.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.Select;
				dataGridNew.DisplayLayout.Override.AllowColMoving = AllowColMoving.NotAllowed;
				dataGridOld.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				dataGridOld.DisplayLayout.UseFixedHeaders = true;
				dataGridOld.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.SingleSummary;
				dataGridOld.DisplayLayout.Override.ActiveRowAppearance.BackColor = Color.White;
				dataGridOld.DisplayLayout.Override.ActiveRowAppearance.ForeColor = Color.Black;
				dataGridOld.DisplayLayout.Bands[0].CardView = true;
				dataGridOld.DisplayLayout.Override.CellClickAction = CellClickAction.CellSelect;
				dataGridOld.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.False;
				dataGridOld.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.False;
				dataGridOld.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.Select;
				dataGridOld.DisplayLayout.Override.AllowColMoving = AllowColMoving.NotAllowed;
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
				FormActivator.ProductDetailsFormObj.LoadData(selectedID);
				FormActivator.BringFormToFront(FormActivator.ProductDetailsFormObj);
			}
		}

		private void listViewItems_DoubleClick(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			New();
		}

		private void New()
		{
			FormActivator.BringFormToFront(FormActivator.ProductDetailsFormObj);
		}

		private void buttonGotoItem_Click(object sender, EventArgs e)
		{
			OpenForEdit();
		}

		private void ProductListForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					Init();
					LoadData();
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
			else if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditSmartList))
			{
				treeViewList.LabelEdit = false;
				ToolStripMenuItem toolStripMenuItem = newGroupToolStripMenuItem;
				ToolStripMenuItem toolStripMenuItem2 = newReportToolStripMenuItem;
				ToolStripMenuItem toolStripMenuItem3 = editToolStripMenuItem;
				ToolStripMenuItem toolStripMenuItem4 = moveUpToolStripMenuItem;
				ToolStripMenuItem toolStripMenuItem5 = moveDownToolStripMenuItem;
				bool flag2 = deleteToolStripMenuItem.Enabled = false;
				bool flag4 = toolStripMenuItem5.Enabled = flag2;
				bool flag6 = toolStripMenuItem4.Enabled = flag4;
				bool flag8 = toolStripMenuItem3.Enabled = flag6;
				bool enabled = toolStripMenuItem2.Enabled = flag8;
				toolStripMenuItem.Enabled = enabled;
			}
		}

		public void OnActivated()
		{
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.Products;
		}

		public static int GetScreenID()
		{
			return 7004;
		}

		private string GetSelectedID()
		{
			string result = "";
			if (dataGridNew.ActiveRow == null)
			{
				return "";
			}
			dataGridNew.ActiveRow.GetType();
			if (dataGridNew.ActiveRow != null)
			{
				if (dataGridNew.ActiveRow.GetType() != typeof(UltraGridRow))
				{
					return "";
				}
				result = dataGridNew.ActiveRow.Cells["Item Code"].Text.ToString();
			}
			return result;
		}

		private UltraGridRow GetSelectedItem()
		{
			if (dataGridNew.ActiveRow != null)
			{
				return dataGridNew.ActiveRow;
			}
			return null;
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			Delete();
		}

		private void Delete()
		{
			if (!isReadOnly && GetSelectedID() != "")
			{
				try
				{
					ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this record?");
					_ = 6;
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

		public void RefreshData()
		{
		}

		private string GetDocumentTitle()
		{
			return "Item List";
		}

		private void Print()
		{
			try
			{
				string text = Text;
				if (labelViewListName.Text != "")
				{
					text = labelViewListName.Text;
				}
				PrintHelper.PreviewDocument(ultraGridPrintDocument1, text);
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
			new DataExportHelper().ExportToExcel(dataGridNew, labelViewListName.Text);
		}

		private void toolStripButtonColumnChooser_Click(object sender, EventArgs e)
		{
			dataGridNew.ShowColumnChooser();
		}

		private void toolStripButtonAutoFit_Click(object sender, EventArgs e)
		{
			dataGridNew.AutoFitColumns();
		}

		private void toolStripButtonShowInactive_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void menuItem5_Click(object sender, EventArgs e)
		{
		}

		private void LoadCategories()
		{
			string key = "";
			int num = -1;
			if (treeViewList.SelectedNode != null)
			{
				key = treeViewList.SelectedNode.Name;
				num = treeViewList.SelectedNode.ImageIndex;
			}
			treeViewList.Nodes.Clear();
			DataSet categoryList = Factory.SmartListSystem.GetCategoryList();
			DataSet smartList = Factory.SmartListSystem.GetSmartList();
			if (categoryList != null && categoryList.Tables.Count > 0)
			{
				DataRow[] array = categoryList.Tables[0].Select("ParentID IS NULL");
				foreach (DataRow dataRow in array)
				{
					AddTreeNode(null, dataRow["CategoryID"].ToString(), dataRow["CategoryName"].ToString(), categoryList, smartList);
				}
			}
			TreeNode[] array2 = treeViewList.Nodes.Find(key, searchAllChildren: true);
			int num2 = 0;
			while (true)
			{
				if (num2 < array2.Length)
				{
					if (array2[num2].ImageIndex == num)
					{
						break;
					}
					num2 = checked(num2 + 1);
					continue;
				}
				return;
			}
			array2[num2].EnsureVisible();
			treeViewList.SelectedNode = array2[num2];
		}

		private int RemoveEmptyGroups(TreeNode parentNode)
		{
			int num = 0;
			TreeNodeCollection nodes = treeViewList.Nodes;
			if (parentNode != null)
			{
				nodes = parentNode.Nodes;
			}
			checked
			{
				for (int i = 0; i < nodes.Count; i++)
				{
					TreeNode treeNode = nodes[i];
					if (treeNode.Tag.ToString() == "1")
					{
						if (treeNode.GetNodeCount(includeSubTrees: false) == 0)
						{
							treeNode.Remove();
							num++;
						}
						else
						{
							num += RemoveEmptyGroups(treeNode);
						}
					}
				}
				return num;
			}
		}

		private void AddTreeNode(TreeNode parent, string categoryID, string categoryName, DataSet data, DataSet reportsData)
		{
			if (parent == null)
			{
				parent = treeViewList.Nodes.Add(categoryID, categoryName, 0, 0);
				parent.Tag = 1;
			}
			else
			{
				parent = parent.Nodes.Add(categoryID, categoryName, 0, 0);
				parent.Tag = 1;
			}
			DataRow[] array = data.Tables[0].Select("ParentID='" + categoryID + "'");
			foreach (DataRow dataRow in array)
			{
				AddTreeNode(parent, dataRow["CategoryID"].ToString(), dataRow["CategoryName"].ToString(), data, reportsData);
			}
			DataRow[] array2 = reportsData.Tables[0].Select("CategoryID='" + categoryID + "'");
			for (int j = 0; j < array2.Length; j = checked(j + 1))
			{
				if (Security.GetCustomReportAccessRight(CustomReportTypes.SmartList, array2[j]["SmartListID"].ToString()).Visible)
				{
					parent.Nodes.Add(array2[j]["SmartListID"].ToString(), array2[j]["SmartListName"].ToString(), 2, 2).Tag = 2;
				}
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			LoadReport();
		}

		private void toolStripButtonFitText_Click(object sender, EventArgs e)
		{
			dataGridNew.AutoSizeColumns();
		}

		private void toolStripButtonClearFilter_Click(object sender, EventArgs e)
		{
			if (dataGridNew.DisplayLayout != null)
			{
				_ = dataGridNew.DisplayLayout.Bands.Count;
				_ = 0;
			}
		}

		private void toolStripButtonExpand_Click(object sender, EventArgs e)
		{
			if (dataGridNew.Rows.Count > 0)
			{
				dataGridNew.Rows.ExpandAll(recursive: true);
			}
		}

		private void toolStripButtonCollapse_Click(object sender, EventArgs e)
		{
			if (dataGridNew.Rows.Count > 0)
			{
				dataGridNew.Rows.CollapseAll(recursive: true);
			}
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBoxSearch_KeyUp(object sender, KeyEventArgs e)
		{
		}

		private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
		{
		}

		public void CompareVersions(DataSet newData, DataSet oldData)
		{
			this.newData = newData;
			this.oldData = oldData;
			AddTables();
		}

		private void CompareTable(string tableName)
		{
			if (tableName == "Company_Option")
			{
				DataView dataView = new DataView(OldData.Tables["Company_Option"]);
				DataView dataView2 = new DataView(NewData.Tables["Company_Option"]);
				DataTable dataTable = dataView.ToTable(true, "OptionID", "OptionValue").Copy();
				DataTable dataTable2 = dataView2.ToTable(true, "OptionID", "OptionValue");
				dataTable.Columns.Add("Option");
				dataTable.Columns.Add("ChangedTo");
				DataTable dataTable3 = dataTable.Clone();
				dataTable3.Columns["Option"].SetOrdinal(1);
				foreach (DataRow row in dataTable.Rows)
				{
					string text = row["OptionID"].ToString();
					string a = row["OptionValue"].ToString();
					DataRow[] array = dataTable2.Select("OptionID='" + text + "'");
					if (array.Length != 0)
					{
						DataRow[] array2 = array;
						foreach (DataRow dataRow2 in array2)
						{
							if (a != dataRow2["OptionValue"].ToString())
							{
								row["Option"] = (CompanyOptionsEnum)Enum.Parse(typeof(CompanyOptionsEnum), text);
								row["ChangedTo"] = dataRow2["OptionValue"].ToString();
								dataTable3.ImportRow(row);
							}
						}
					}
				}
				dataGridNew.DataSource = dataTable3;
				dataGridNew.DisplayLayout.Bands[0].Columns["OptionValue"].Header.Caption = "ChangedFrom";
				dataGridOld.Visible = false;
				panel2.Visible = false;
			}
			else
			{
				dataGridNew.DataSource = NewData.Tables[tableName];
				dataGridOld.DataSource = OldData.Tables[tableName];
			}
			if (dataGridNew.Rows.Count == 0 && dataGridOld.Rows.Count == 0)
			{
				dataGridNew.DataSource = null;
				dataGridOld.DataSource = null;
			}
			else if (dataGridNew.Rows.Count == 1 && dataGridOld.Rows.Count <= 1)
			{
				SetCardView(val: true);
				foreach (UltraGridColumn column in dataGridNew.DisplayLayout.Bands[0].Columns)
				{
					if (!dataGridOld.DisplayLayout.Bands[0].Columns.Exists(column.Key))
					{
						dataGridNew.DisplayLayout.Bands[0].Columns[column.Key].Header.Appearance.FontData.Bold = DefaultableBoolean.True;
					}
					else if (dataGridNew.Rows[0].Cells[column.Key].Value.ToString() == dataGridOld.Rows[0].Cells[column.Key].Value.ToString())
					{
						dataGridNew.DisplayLayout.Bands[0].Columns[column.Key].Hidden = true;
						dataGridOld.DisplayLayout.Bands[0].Columns[column.Key].Hidden = true;
					}
				}
			}
		}

		private void AddTables()
		{
			treeViewList.Nodes.Clear();
			foreach (DataTable table in newData.Tables)
			{
				TreeNode treeNode = treeViewList.Nodes.Add(table.TableName);
				if (!oldData.Tables.Contains(table.TableName))
				{
					treeNode.ForeColor = Color.Green;
				}
			}
		}
	}
}
