using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports
{
	public class ExternalReportForm : Form, IForm
	{
		private bool isFirstTimeActivating = true;

		private bool canAccessCost = true;

		private DataSet reportData;

		private DataSet smartListData;

		private ArrayList tempFileList = new ArrayList();

		private string tempFilePath = "";

		private bool needDelete;

		private string parm1;

		private string parm2;

		private string parm3;

		private string parm4;

		private Hashtable listViewKeys = new Hashtable();

		private bool isReadOnly;

		private DateTime dateTimeStamp = DateTime.MinValue;

		private DataSet productData;

		private bool showInactiveItems;

		private XPButton buttonDone;

		private Panel panelButtons;

		private Line linePanelDown;

		private IContainer components;

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

		private Button buttonAddGroup;

		private Button button2;

		private ToolTip toolTip1;

		private Button button3;

		private Panel panelEdit;

		private ToolStripButton toolStripButtonMerge;

		private ToolStripButton toolStripButtonReset;

		private ToolStripButton toolStripButtonFitText;

		private ToolStripButton toolStripButtonClearFilter;

		private Label labelViewListName;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonExpand;

		private ToolStripButton toolStripButtonCollapse;

		private XPButton buttonSave;

		private WebBrowser webBrowser;

		private Panel panel1;

		private Label labelNoPreview;

		private ToolStrip toolStrip1;

		private bool isSubReport;

		private ScreenAccessRight screenRight;

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
					panelEdit.Visible = false;
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

		public ExternalReportForm()
		{
			InitializeComponent();
			base.Activated += ProductListForm_Activated;
			treeViewList.NodeMouseClick += treeView1_NodeMouseClick;
			treeViewList.Click += treeView1_Click;
			treeViewList.AfterSelect += treeView1_AfterSelect;
			treeViewList.AfterLabelEdit += treeView1_AfterLabelEdit;
			treeViewList.BeforeSelect += treeView1_BeforeSelect;
			base.FormClosing += VendorListForm_FormClosing;
			treeViewList.LabelEdit = false;
			webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;
			treeViewList.ItemHeight = 18;
			labelViewListName.Text = "";
			labelNoPreview.Visible = false;
		}

		private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			if (treeViewList.SelectedNode != null)
			{
				_ = treeViewList.SelectedNode.ImageIndex;
				_ = 2;
			}
		}

		private void VendorListForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				UserPreferences.SaveCurrentUserSetting(base.Name + "MergeCell", toolStripButtonMerge.Checked);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			try
			{
				string s = "-1";
				if (treeViewList.SelectedNode.Parent != null)
				{
					s = treeViewList.SelectedNode.Parent.Name;
				}
				if (!string.IsNullOrEmpty(e.Label))
				{
					if (treeViewList.SelectedNode.ImageIndex == 0)
					{
						if (!Factory.SmartListSystem.UpdateCategory(e.Label, int.Parse(s), int.Parse(treeViewList.SelectedNode.Name)))
						{
							e.CancelEdit = true;
						}
					}
					else if (!Factory.SmartListSystem.RenameReport(e.Label, int.Parse(treeViewList.SelectedNode.Name)))
					{
						e.CancelEdit = true;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
				e.CancelEdit = true;
			}
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				labelNoPreview.Visible = false;
				webBrowser.Url = null;
				webBrowser.Navigate("");
				Application.DoEvents();
				_ = treeViewList.SelectedNode.Index;
				if (treeViewList.SelectedNode != null)
				{
					string text = "";
					if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.ImageIndex != 0)
					{
						text = treeViewList.SelectedNode.Text;
						labelViewListName.Text = treeViewList.SelectedNode.Text;
					}
					object obj = null;
					if (text != "")
					{
						obj = Factory.DatabaseSystem.GetFieldValue("EntityDocs", "EntityDocName", "EntityID", text);
					}
					string text2 = "";
					if (obj != null)
					{
						text2 = Path.GetExtension(obj.ToString()).ToLower();
					}
					if (text2 == "jpg" || text2 == "png" || text2 == "gif" || text2 == "jpeg" || text2 == "tif" || text2 == "bmp" || text2 == "pdf" || text2 == "xlsx" || text2 == "xls")
					{
						byte[] entityFile = Factory.EntityDocSystem.GetEntityFile(EntityTypesEnum.ExternalReports, text, "EXR", obj.ToString());
						if (entityFile.IsNullOrEmpty())
						{
							ErrorHelper.WarningMessage("File not found.");
						}
						else
						{
							MemoryStream memoryStream = new MemoryStream(entityFile);
							byte[] buffer = new byte[memoryStream.Length];
							memoryStream.Read(buffer, 0, checked((int)memoryStream.Length));
							bool flag = false;
							if (memoryStream != null)
							{
								DataSet entityDocByID = Factory.EntityDocSystem.GetEntityDocByID(EntityTypesEnum.ExternalReports, text, obj.ToString());
								flag = CheckFile(entityDocByID.Tables[0].Rows[0]["EntityDocPath"].ToString());
							}
							if (flag)
							{
								webBrowser.Visible = false;
							}
						}
					}
					else
					{
						text2 = text2.ToLower().Trim();
						switch (text2)
						{
						case ".xlsx":
						case ".xls":
						case ".pdf":
							labelNoPreview.Visible = true;
							break;
						}
						if (text2 != ".xlsx" && text2 != ".xls" && text2 != ".pdf")
						{
							string urlString = SaveToDisk();
							webBrowser.Visible = true;
							webBrowser.BringToFront();
							webBrowser.Navigate(urlString);
							tempFilePath = urlString;
						}
					}
				}
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
						LoadReportData(selectedNode.Name);
						OpenFile();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void LoadReportData(string reportName)
		{
			try
			{
				smartListData = Factory.SmartListSystem.GetSmartListByID(reportName);
				if (IsSubReport)
				{
					_ = new string[4]
					{
						"@Parm1",
						"@Parm2",
						"@Parm3",
						"@Parm4"
					};
					_ = new string[4]
					{
						parm1,
						parm2,
						parm3,
						parm4
					};
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void treeView1_Click(object sender, EventArgs e)
		{
		}

		private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
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

		private void dataGridList_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			if (!e.Row.IsDataRow || smartListData == null)
			{
				return;
			}
			FormHelper formHelper = new FormHelper();
			SmartListDrillDownActions smartListDrillDownActions = SmartListDrillDownActions.None;
			DataRow dataRow = smartListData.Tables[0].Rows[0];
			if (dataRow["DrillAction"] != DBNull.Value)
			{
				smartListDrillDownActions = (SmartListDrillDownActions)int.Parse(dataRow["DrillAction"].ToString());
			}
			switch (smartListDrillDownActions)
			{
			case SmartListDrillDownActions.OpenCard:
			{
				DataComboType cardType = (DataComboType)int.Parse(dataRow["DrillCardTypeID"].ToString());
				string text7 = dataRow["DrillCardIDField"].ToString();
				if (!e.Row.Band.Columns.Exists(text7))
				{
					ErrorHelper.WarningMessage("Column name: " + text7 + " does not exist.");
					break;
				}
				string id = e.Row.Cells[text7].Value.ToString();
				formHelper.OpenCardForEdit(cardType, id);
				break;
			}
			case SmartListDrillDownActions.OpenTransaction:
			{
				string text5 = dataRow["DrillTransactionVoucherIDField"].ToString();
				string text6 = dataRow["DrillTransactionSysDocIDField"].ToString();
				bool flag = false;
				if (!dataRow["IsPreview"].IsDBNullOrEmpty())
				{
					flag = bool.Parse(dataRow["IsPreview"].ToString());
				}
				if (!e.Row.Band.Columns.Exists(text5))
				{
					ErrorHelper.WarningMessage("Column name: " + text5 + " does not exist.");
					break;
				}
				if (!e.Row.Band.Columns.Exists(text6))
				{
					ErrorHelper.WarningMessage("Column name: " + text6 + " does not exist.");
					break;
				}
				string voucherID = e.Row.Cells[text5].Value.ToString();
				string sysDocID = e.Row.Cells[text6].Value.ToString();
				if (!flag)
				{
					formHelper.EditTransaction(sysDocID, voucherID);
					break;
				}
				DocumentViewForm documentViewForm = new DocumentViewForm();
				documentViewForm.Document = formHelper.GetTransactionPreviewDoc(sysDocID, voucherID);
				documentViewForm.ShowDialog();
				break;
			}
			case SmartListDrillDownActions.OpenSmartList:
			{
				dataRow["DrillSubReportID"].ToString();
				string text = dataRow["DrillParm1"].ToString();
				string text2 = dataRow["DrillParm2"].ToString();
				string text3 = dataRow["DrillParm3"].ToString();
				string text4 = dataRow["DrillParm4"].ToString();
				if (!text.IsNullOrEmpty() && !e.Row.Band.Columns.Exists(text))
				{
					ErrorHelper.WarningMessage("Column name: " + text + " does not exist.");
				}
				else if (!text2.IsNullOrEmpty() && !e.Row.Band.Columns.Exists(text2))
				{
					ErrorHelper.WarningMessage("Column name: " + text2 + " does not exist.");
				}
				else if (!text3.IsNullOrEmpty() && !e.Row.Band.Columns.Exists(text3))
				{
					ErrorHelper.WarningMessage("Column name: " + text3 + " does not exist.");
				}
				else if (!text4.IsNullOrEmpty() && !e.Row.Band.Columns.Exists(text4))
				{
					ErrorHelper.WarningMessage("Column name: " + text4 + " does not exist.");
				}
				else
				{
					new ExternalReportForm().Show();
				}
				break;
			}
			}
		}

		public void ShowDrillDownReport(string reportID, DateTime fromDate, DateTime toDate, string parm1, string parm2, string parm3, string parm4)
		{
			try
			{
				this.parm1 = parm1;
				this.parm2 = parm2;
				this.parm3 = parm3;
				this.parm4 = parm4;
				IsSubReport = true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.ExternalReportForm));
			panelButtons = new System.Windows.Forms.Panel();
			buttonSave = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDone = new Micromind.UISupport.XPButton();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			microsoftExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAllowGrouping = new System.Windows.Forms.ToolStripButton();
			toolStripButtonAutoFit = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFitText = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonExpand = new System.Windows.Forms.ToolStripButton();
			toolStripButtonCollapse = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonColumnChooser = new System.Windows.Forms.ToolStripButton();
			toolStripButtonClearFilter = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMerge = new System.Windows.Forms.ToolStripButton();
			toolStripButtonReset = new System.Windows.Forms.ToolStripButton();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemLocationWiseOnhand = new System.Windows.Forms.ToolStripMenuItem();
			treeViewList = new System.Windows.Forms.TreeView();
			contextMenuStripTreeView = new System.Windows.Forms.ContextMenuStrip();
			newGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			newReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			imageList1 = new System.Windows.Forms.ImageList();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			panel1 = new System.Windows.Forms.Panel();
			webBrowser = new System.Windows.Forms.WebBrowser();
			labelViewListName = new System.Windows.Forms.Label();
			toolTip1 = new System.Windows.Forms.ToolTip();
			buttonAddGroup = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
			button3 = new System.Windows.Forms.Button();
			panelEdit = new System.Windows.Forms.Panel();
			labelNoPreview = new System.Windows.Forms.Label();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			contextMenuStripTreeView.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			panel1.SuspendLayout();
			panelEdit.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 532);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(980, 40);
			panelButtons.TabIndex = 1;
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.DarkGray;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(618, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 15;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Visible = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(980, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(872, 8);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 24);
			buttonDone.TabIndex = 5;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
			{
				toolStripButtonRefresh,
				toolStripSeparator1,
				toolStripButtonPrint,
				toolStripDropDownButton1,
				toolStripSeparator2,
				toolStripButtonAllowGrouping,
				toolStripButtonAutoFit,
				toolStripButtonFitText,
				toolStripSeparator7,
				toolStripButtonExpand,
				toolStripButtonCollapse,
				toolStripSeparator3,
				toolStripButtonColumnChooser,
				toolStripButtonClearFilter,
				toolStripButtonMerge,
				toolStripButtonReset
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(980, 31);
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
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
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
			toolStripButtonAutoFit.Image = Micromind.ClientUI.Properties.Resources.autofit;
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(100, 28);
			toolStripButtonAutoFit.Text = "Fit to Screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripButtonFitText.Image = Micromind.ClientUI.Properties.Resources.colbestsize;
			toolStripButtonFitText.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFitText.Name = "toolStripButtonFitText";
			toolStripButtonFitText.Size = new System.Drawing.Size(86, 28);
			toolStripButtonFitText.Text = "Fit to Text";
			toolStripButtonFitText.Click += new System.EventHandler(toolStripButtonFitText_Click);
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
			toolStripButtonExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonExpand.Image = Micromind.ClientUI.Properties.Resources._001_03;
			toolStripButtonExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonExpand.Name = "toolStripButtonExpand";
			toolStripButtonExpand.Size = new System.Drawing.Size(28, 28);
			toolStripButtonExpand.Text = "toolStripButton2";
			toolStripButtonExpand.Click += new System.EventHandler(toolStripButtonExpand_Click);
			toolStripButtonCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonCollapse.Image = Micromind.ClientUI.Properties.Resources._001_04;
			toolStripButtonCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonCollapse.Name = "toolStripButtonCollapse";
			toolStripButtonCollapse.Size = new System.Drawing.Size(28, 28);
			toolStripButtonCollapse.Text = "toolStripButton1";
			toolStripButtonCollapse.Click += new System.EventHandler(toolStripButtonCollapse_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonColumnChooser.Image = Micromind.ClientUI.Properties.Resources.ColumnChooser;
			toolStripButtonColumnChooser.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonColumnChooser.Name = "toolStripButtonColumnChooser";
			toolStripButtonColumnChooser.Size = new System.Drawing.Size(83, 28);
			toolStripButtonColumnChooser.Text = "Columns";
			toolStripButtonColumnChooser.Click += new System.EventHandler(toolStripButtonColumnChooser_Click);
			toolStripButtonClearFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonClearFilter.Image = Micromind.ClientUI.Properties.Resources.clearfilter;
			toolStripButtonClearFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonClearFilter.Name = "toolStripButtonClearFilter";
			toolStripButtonClearFilter.Size = new System.Drawing.Size(28, 28);
			toolStripButtonClearFilter.Text = "Clear All Filters";
			toolStripButtonClearFilter.Click += new System.EventHandler(toolStripButtonClearFilter_Click);
			toolStripButtonMerge.CheckOnClick = true;
			toolStripButtonMerge.Image = Micromind.ClientUI.Properties.Resources.merge;
			toolStripButtonMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMerge.Name = "toolStripButtonMerge";
			toolStripButtonMerge.Size = new System.Drawing.Size(97, 28);
			toolStripButtonMerge.Text = "Merge Cells";
			toolStripButtonMerge.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripButtonReset.Image = Micromind.ClientUI.Properties.Resources.reset;
			toolStripButtonReset.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonReset.Name = "toolStripButtonReset";
			toolStripButtonReset.Size = new System.Drawing.Size(102, 28);
			toolStripButtonReset.Text = "Reset Layout";
			toolStripButtonReset.ToolTipText = "Reset the layout to default layout";
			toolStripButtonReset.Click += new System.EventHandler(toolStripButtonReset_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(200, 6);
			toolStripMenuItemLocationWiseOnhand.Name = "toolStripMenuItemLocationWiseOnhand";
			toolStripMenuItemLocationWiseOnhand.Size = new System.Drawing.Size(203, 22);
			toolStripMenuItemLocationWiseOnhand.Text = "Location Wise Onhand...";
			treeViewList.AllowDrop = true;
			treeViewList.ContextMenuStrip = contextMenuStripTreeView;
			treeViewList.Dock = System.Windows.Forms.DockStyle.Fill;
			treeViewList.FullRowSelect = true;
			treeViewList.HideSelection = false;
			treeViewList.ImageIndex = 0;
			treeViewList.ImageList = imageList1;
			treeViewList.Location = new System.Drawing.Point(0, 0);
			treeViewList.Name = "treeViewList";
			treeViewList.SelectedImageIndex = 1;
			treeViewList.Size = new System.Drawing.Size(231, 461);
			treeViewList.StateImageList = imageList1;
			treeViewList.TabIndex = 291;
			treeViewList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(treeViewList_NodeMouseClick);
			treeViewList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(treeViewList_NodeMouseDoubleClick);
			treeViewList.MouseClick += new System.Windows.Forms.MouseEventHandler(treeViewList_MouseClick);
			treeViewList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(treeViewList_MouseDoubleClick);
			contextMenuStripTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				newGroupToolStripMenuItem,
				newReportToolStripMenuItem,
				toolStripSeparator5,
				editToolStripMenuItem,
				toolStripSeparator6,
				deleteToolStripMenuItem
			});
			contextMenuStripTreeView.Name = "contextMenuStripTreeView";
			contextMenuStripTreeView.Size = new System.Drawing.Size(146, 104);
			newGroupToolStripMenuItem.Name = "newGroupToolStripMenuItem";
			newGroupToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			newGroupToolStripMenuItem.Text = "New Group...";
			newGroupToolStripMenuItem.Click += new System.EventHandler(newGroupToolStripMenuItem_Click);
			newReportToolStripMenuItem.Name = "newReportToolStripMenuItem";
			newReportToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			newReportToolStripMenuItem.Text = "New Report...";
			newReportToolStripMenuItem.Click += new System.EventHandler(newReportToolStripMenuItem_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(142, 6);
			editToolStripMenuItem.Name = "editToolStripMenuItem";
			editToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			editToolStripMenuItem.Text = "Edit...";
			editToolStripMenuItem.Click += new System.EventHandler(editToolStripMenuItem_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(142, 6);
			toolStripSeparator6.Visible = false;
			deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			deleteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			deleteToolStripMenuItem.Text = "Delete";
			deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "folder_closed.png");
			imageList1.Images.SetKeyName(1, "folder_open.png");
			imageList1.Images.SetKeyName(2, "repport16x16.png");
			splitContainer1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			splitContainer1.Location = new System.Drawing.Point(12, 38);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Panel1.Controls.Add(treeViewList);
			splitContainer1.Panel2.Controls.Add(panel1);
			splitContainer1.Panel2.Controls.Add(labelViewListName);
			splitContainer1.Size = new System.Drawing.Size(956, 461);
			splitContainer1.SplitterDistance = 231;
			splitContainer1.TabIndex = 292;
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(labelNoPreview);
			panel1.Controls.Add(webBrowser);
			panel1.Location = new System.Drawing.Point(0, 44);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(718, 414);
			panel1.TabIndex = 296;
			webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			webBrowser.Location = new System.Drawing.Point(0, 0);
			webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			webBrowser.Name = "webBrowser";
			webBrowser.Size = new System.Drawing.Size(718, 414);
			webBrowser.TabIndex = 293;
			labelViewListName.AutoSize = true;
			labelViewListName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelViewListName.Location = new System.Drawing.Point(237, 16);
			labelViewListName.Name = "labelViewListName";
			labelViewListName.Size = new System.Drawing.Size(149, 25);
			labelViewListName.TabIndex = 295;
			labelViewListName.Text = "Report Name";
			buttonAddGroup.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonAddGroup.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			buttonAddGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			buttonAddGroup.Image = Micromind.ClientUI.Properties.Resources.newfolder;
			buttonAddGroup.Location = new System.Drawing.Point(12, 1);
			buttonAddGroup.Name = "buttonAddGroup";
			buttonAddGroup.Size = new System.Drawing.Size(26, 26);
			buttonAddGroup.TabIndex = 293;
			toolTip1.SetToolTip(buttonAddGroup, "Create New Group");
			buttonAddGroup.UseVisualStyleBackColor = true;
			buttonAddGroup.Click += new System.EventHandler(button1_Click);
			button2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			button2.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button2.Image = Micromind.ClientUI.Properties.Resources.newfile;
			button2.Location = new System.Drawing.Point(48, 1);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(26, 26);
			button2.TabIndex = 293;
			toolTip1.SetToolTip(button2, "Create New Report");
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(button2_Click);
			button3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			button3.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
			button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			button3.Image = Micromind.ClientUI.Properties.Resources.Delete;
			button3.Location = new System.Drawing.Point(84, 1);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(26, 26);
			button3.TabIndex = 293;
			toolTip1.SetToolTip(button3, "Delete Item");
			button3.UseVisualStyleBackColor = true;
			button3.Click += new System.EventHandler(button3_Click);
			panelEdit.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			panelEdit.Controls.Add(buttonAddGroup);
			panelEdit.Controls.Add(button2);
			panelEdit.Controls.Add(button3);
			panelEdit.Location = new System.Drawing.Point(0, 500);
			panelEdit.Name = "panelEdit";
			panelEdit.Size = new System.Drawing.Size(171, 32);
			panelEdit.TabIndex = 294;
			labelNoPreview.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelNoPreview.BackColor = System.Drawing.Color.White;
			labelNoPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelNoPreview.ForeColor = System.Drawing.Color.Blue;
			labelNoPreview.Location = new System.Drawing.Point(68, 180);
			labelNoPreview.Name = "labelNoPreview";
			labelNoPreview.Size = new System.Drawing.Size(577, 39);
			labelNoPreview.TabIndex = 298;
			labelNoPreview.Text = "No Preview Available on Screen(Double Click if Required)";
			labelNoPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(980, 572);
			base.Controls.Add(panelEdit);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(splitContainer1);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "ExternalReportForm";
			Text = "External Report";
			base.Load += new System.EventHandler(ProductListForm_Load);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			contextMenuStripTreeView.ResumeLayout(false);
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panelEdit.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			toolStripButtonMerge.Checked = UserPreferences.GetCurrentUserSetting(base.Name + "MergeCell", defaultValue: false);
		}

		private void LoadData()
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					PublicFunctions.StartWaiting(this);
					LoadCategories();
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
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditSmartList))
			{
				panelEdit.Enabled = false;
				treeViewList.LabelEdit = false;
				contextMenuStripTreeView.Enabled = false;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
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
			return "";
		}

		private UltraGridRow GetSelectedItem()
		{
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
			new DataExportHelper();
		}

		private void toolStripButtonAllowGrouping_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonColumnChooser_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonAutoFit_Click(object sender, EventArgs e)
		{
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
			DataSet categoryList = Factory.ExternalReportSystem.GetCategoryList();
			DataSet externalReportData = Factory.ExternalReportSystem.GetExternalReportData(Global.CurrentUser, Global.IsUserAdmin);
			if (categoryList != null && categoryList.Tables.Count > 0)
			{
				DataRow[] array = categoryList.Tables[0].Select("ParentID IS NULL");
				foreach (DataRow dataRow in array)
				{
					AddTreeNode(null, dataRow["CategoryID"].ToString(), dataRow["CategoryName"].ToString(), categoryList, externalReportData);
				}
			}
			TreeNode[] array2 = treeViewList.Nodes.Find(key, searchAllChildren: true);
			for (int j = 0; j < array2.Length; j = checked(j + 1))
			{
				if (array2[j].ImageIndex == num)
				{
					array2[j].EnsureVisible();
					treeViewList.SelectedNode = array2[j];
					break;
				}
			}
			if (!panelEdit.Enabled)
			{
				int num2 = 0;
				do
				{
					num2 = RemoveEmptyGroups(null);
				}
				while (num2 > 0);
			}
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
			DataRow[] array = data.Tables[0].Select("ParentID=" + categoryID);
			foreach (DataRow dataRow in array)
			{
				AddTreeNode(parent, dataRow["CategoryID"].ToString(), dataRow["CategoryName"].ToString(), data, reportsData);
			}
			DataRow[] array2 = reportsData.Tables[0].Select("CategoryID='" + categoryID + "'");
			for (int j = 0; j < array2.Length; j = checked(j + 1))
			{
				if (Security.GetCustomReportAccessRight(CustomReportTypes.ExternalReport, array2[j]["ExternalReportID"].ToString()).Visible)
				{
					parent.Nodes.Add(array2[j]["ExternalReportID"].ToString(), array2[j]["ExternalReportName"].ToString(), 2, 2).Tag = 2;
				}
			}
		}

		private void newGroupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewFolder();
		}

		private void NewFolder()
		{
			NewSmartlistGroup newSmartlistGroup = new NewSmartlistGroup();
			string parentID = "-1";
			if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.ImageIndex == 0)
			{
				parentID = treeViewList.SelectedNode.Text;
			}
			newSmartlistGroup.SetReport = "External";
			newSmartlistGroup.Text = "External Report Category";
			newSmartlistGroup.ParentID = parentID;
			if (newSmartlistGroup.ShowDialog() == DialogResult.OK)
			{
				parentID = newSmartlistGroup.ParentID;
				if (parentID == "")
				{
					parentID = "-1";
				}
				Factory.ExternalReportSystem.CreateCategory(newSmartlistGroup.CategoryName, int.Parse(parentID));
				LoadCategories();
			}
		}

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treeViewList.SelectedNode == null)
			{
				return;
			}
			if (treeViewList.SelectedNode.ImageIndex == 0)
			{
				string parentID = "-1";
				if (treeViewList.SelectedNode.Parent != null)
				{
					parentID = treeViewList.SelectedNode.Parent.Name;
				}
				NewSmartlistGroup newSmartlistGroup = new NewSmartlistGroup();
				newSmartlistGroup.CategoryName = treeViewList.SelectedNode.Text;
				newSmartlistGroup.CategoryID = treeViewList.SelectedNode.Name;
				newSmartlistGroup.SetReport = "External";
				newSmartlistGroup.Text = "External Report Category";
				newSmartlistGroup.ParentID = parentID;
				if (newSmartlistGroup.ShowDialog() == DialogResult.OK)
				{
					parentID = newSmartlistGroup.ParentID;
					if (parentID == "")
					{
						parentID = "-1";
					}
					if (Factory.ExternalReportSystem.UpdateCategory(newSmartlistGroup.CategoryName, int.Parse(parentID), int.Parse(treeViewList.SelectedNode.Name)))
					{
						treeViewList.SelectedNode.Text = newSmartlistGroup.CategoryName;
					}
				}
			}
			else
			{
				EditReport();
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DeleteItem();
			labelViewListName.Text = "";
		}

		private void DeleteItem()
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete this item?") != DialogResult.Yes)
			{
				return;
			}
			if (treeViewList.SelectedNode.ImageIndex == 2)
			{
				if (Factory.ExternalReportSystem.DeleteExternalReport(treeViewList.SelectedNode.Name))
				{
					treeViewList.SelectedNode.Remove();
				}
			}
			else if (treeViewList.SelectedNode.Nodes.Count > 0)
			{
				ErrorHelper.WarningMessage("You cannot delete groups which have subgroups or reports.");
			}
			else if (Factory.ExternalReportSystem.DeleteCategory(treeViewList.SelectedNode.Name))
			{
				treeViewList.SelectedNode.Remove();
			}
		}

		private void NewReport()
		{
			SmartListDetailsForm smartListDetailsForm = new SmartListDetailsForm();
			smartListDetailsForm.SetReport = "External";
			smartListDetailsForm.SetGroup = true;
			smartListDetailsForm.Text = "External Report Details";
			if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.ImageIndex == 0)
			{
				smartListDetailsForm.CategoryID = treeViewList.SelectedNode.Name;
			}
			else if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.Parent != null && treeViewList.SelectedNode.ImageIndex == 2)
			{
				smartListDetailsForm.CategoryID = treeViewList.SelectedNode.Parent.Name;
			}
			smartListDetailsForm.Size = new Size(575, 235);
			smartListDetailsForm.Show();
		}

		private void EditReport()
		{
			SmartListDetailsForm smartListDetailsForm = new SmartListDetailsForm();
			if (treeViewList.SelectedNode != null && treeViewList.SelectedNode.Parent != null)
			{
				smartListDetailsForm.SetReport = "External";
				smartListDetailsForm.SetGroup = true;
				smartListDetailsForm.Show();
				smartListDetailsForm.LoadData(treeViewList.SelectedNode.Text);
			}
		}

		private void newReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewReport();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			NewReport();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			NewFolder();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			DeleteItem();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonReset_Click(object sender, EventArgs e)
		{
			ErrorHelper.QuestionMessageYesNo("Are you sure you want to reset the report layout to it's default layout?");
			_ = 6;
		}

		private void ultraExpandableGroupBox1_ExpandedStateChanging(object sender, CancelEventArgs e)
		{
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			LoadReport();
		}

		private void toolStripButtonFitText_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonClearFilter_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonExpand_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonCollapse_Click(object sender, EventArgs e)
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				(base.ActiveMdiChild as IExternalReport)?.SaveReport(ExternalReportTypes.PDF);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private string SaveToDisk()
		{
			try
			{
				string text = "";
				if (treeViewList.SelectedNode != null)
				{
					text = treeViewList.SelectedNode.Text;
				}
				object obj = null;
				if (text != "")
				{
					obj = Factory.DatabaseSystem.GetFieldValue("EntityDocs", "EntityDocName", "EntityID", text);
				}
				byte[] array = null;
				if (obj != null)
				{
					array = Factory.EntityDocSystem.GetEntityFile(EntityTypesEnum.ExternalReports, text, "EXR", obj.ToString());
				}
				if (array.IsNullOrEmpty())
				{
					return "";
				}
				MemoryStream memoryStream = new MemoryStream(array);
				byte[] buffer = new byte[memoryStream.Length];
				memoryStream.Read(buffer, 0, checked((int)memoryStream.Length));
				EntityDocData entityDocData = null;
				if (memoryStream != null)
				{
					entityDocData = Factory.EntityDocSystem.GetEntityDocByID(EntityTypesEnum.ExternalReports, text, obj.ToString());
					if (entityDocData.Tables[0].Rows.Count > 0)
					{
						entityDocData.Tables[0].Rows[0]["EntityDocPath"].ToString();
					}
				}
				string tempFileName = Path.GetTempFileName();
				string text2 = Path.GetDirectoryName(tempFileName) + "\\" + Path.GetFileNameWithoutExtension(tempFileName) + Path.GetExtension(obj.ToString());
				FileStream fileStream = new FileStream(text2, FileMode.OpenOrCreate, FileAccess.ReadWrite);
				memoryStream.WriteTo(fileStream);
				fileStream.Close();
				return text2;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void OpenFile()
		{
			try
			{
				string text = SaveToDisk();
				if (!(text == ""))
				{
					tempFileList.Add(text);
					Process process = new Process();
					process.StartInfo = new ProcessStartInfo(text)
					{
						UseShellExecute = true,
						WindowStyle = ProcessWindowStyle.Normal
					};
					process.Start();
				}
			}
			catch
			{
				throw;
			}
		}

		private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			if (tempFilePath != "" && needDelete)
			{
				try
				{
					Application.DoEvents();
					Thread.Sleep(100);
					Application.DoEvents();
					File.Delete(tempFilePath);
					needDelete = false;
				}
				catch (Exception)
				{
					tempFileList.Add(tempFilePath);
				}
				tempFilePath = "";
			}
		}

		public bool CheckFile(string file)
		{
			bool flag = false;
			try
			{
				Image image = Image.FromFile(file);
				Graphics.FromImage(image);
				_ = image.RawFormat;
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void treeViewList_MouseDoubleClick(object sender, MouseEventArgs e)
		{
		}

		private void treeViewList_MouseClick(object sender, MouseEventArgs e)
		{
		}

		private void treeViewList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
		}

		private void treeViewList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			try
			{
				OpenFile();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}
	}
}
