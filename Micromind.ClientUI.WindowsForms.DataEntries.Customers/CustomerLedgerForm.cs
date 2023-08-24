using DevExpress.Utils;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;
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

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerLedgerForm : Form, IDataForm, IDataList, IExternalReport
	{
		private bool isFirstTimeActivating = true;

		public string parameter1 = "";

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

		private ToolStripButton toolStripButtonAllowGrouping;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonColumnChooser;

		private ToolStripButton toolStripButtonAutoFit;

		private XPButton buttonDelete;

		private DateControl dateControl1;

		private PictureBox pictureBoxVoid;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonMerge;

		private TextBox textBoxDescription;

		private customersFlatComboBox comboBoxCustomer;

		private Label label1;

		private DataGridList dataGridPDC;

		private Label label2;

		private Label label3;

		private Label label4;

		private AmountTextBox textBoxTotalBalance;

		private AmountTextBox textBoxPDC;

		private AmountTextBox textBoxNet;

		private Panel panelConsign;

		private AmountTextBox textBoxExposure;

		private AmountTextBox textBoxConsign;

		private Label label6;

		private Label label7;

		private NavBarControl navBarControl1;

		private NavBarGroup navBarGroupPDC;

		private NavBarGroupControlContainer navBarGroupControlContainer1;

		private NavBarGroupControlContainer navBarGroupControlContainer2;

		private NavBarGroup navBarGroupConsignment;

		private DataGridList dataGridConsign;

		private TextBox textBoxCurrency;

		private RadioButton radioButtonCustomerCurrency;

		private RadioButton radioButtonBaseCurrency;

		private XPButton buttonOutstandingList;

		private ToolStrip toolStrip1;

		private ScreenAccessRight screenRight;

		public string SelectedID
		{
			get
			{
				return comboBoxCustomer.SelectedID;
			}
			set
			{
				comboBoxCustomer.SelectedID = value;
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

		public CustomerLedgerForm()
		{
			InitializeComponent();
			base.Activated += CompanyAccountsListForm_Activated;
			dataGridList.DoubleClickRow += dataGridList_DoubleClickRow;
			dataGridList.NewMenuClicked += dataGridList_NewMenuClicked;
			dataGridList.OpenMenuClicked += dataGridList_OpenMenuClicked;
			dataGridList.DeleteMenuClicked += dataGridList_DeleteMenuClicked;
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowNewMenu = false;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
			base.FormClosing += CustomerLedgerForm_FormClosing;
			dataGridList.AllowUnfittedView = true;
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxCurrency.Text = comboBoxCustomer.DefaultCurrencyID;
		}

		private void CustomerLedgerForm_FormClosing(object sender, FormClosingEventArgs e)
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
			string a = "";
			if (dataGridList.ActiveRow.Cells.Exists("SysDocType"))
			{
				a = dataGridList.ActiveRow.Cells["SysDocType"].Text.ToString();
			}
			if (!(a == ""))
			{
				OpenForEdit();
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.CustomerLedgerForm));
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
			textBoxDescription = new System.Windows.Forms.TextBox();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			label1 = new System.Windows.Forms.Label();
			dataGridPDC = new Micromind.UISupport.DataGridList(components);
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			textBoxTotalBalance = new Micromind.UISupport.AmountTextBox();
			textBoxPDC = new Micromind.UISupport.AmountTextBox();
			textBoxNet = new Micromind.UISupport.AmountTextBox();
			panelConsign = new System.Windows.Forms.Panel();
			textBoxExposure = new Micromind.UISupport.AmountTextBox();
			textBoxConsign = new Micromind.UISupport.AmountTextBox();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
			navBarGroupConsignment = new DevExpress.XtraNavBar.NavBarGroup();
			navBarGroupControlContainer2 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
			dataGridConsign = new Micromind.UISupport.DataGridList(components);
			navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
			navBarGroupPDC = new DevExpress.XtraNavBar.NavBarGroup();
			textBoxCurrency = new System.Windows.Forms.TextBox();
			radioButtonCustomerCurrency = new System.Windows.Forms.RadioButton();
			radioButtonBaseCurrency = new System.Windows.Forms.RadioButton();
			buttonOutstandingList = new Micromind.UISupport.XPButton();
			panelButtons.SuspendLayout();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridPDC).BeginInit();
			panelConsign.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)navBarControl1).BeginInit();
			navBarControl1.SuspendLayout();
			navBarGroupControlContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridConsign).BeginInit();
			navBarGroupControlContainer1.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDone);
			panelButtons.Controls.Add(buttonOpen);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 550);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(888, 48);
			panelButtons.TabIndex = 9;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 15);
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
			linePanelDown.Size = new System.Drawing.Size(888, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDone.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDone.BackColor = System.Drawing.Color.DarkGray;
			buttonDone.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDone.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonDone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDone.Location = new System.Drawing.Point(780, 16);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(96, 24);
			buttonDone.TabIndex = 3;
			buttonDone.Text = "&Close";
			buttonDone.UseVisualStyleBackColor = false;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			buttonOpen.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpen.BackColor = System.Drawing.Color.DarkGray;
			buttonOpen.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpen.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpen.Location = new System.Drawing.Point(12, 15);
			buttonOpen.Name = "buttonOpen";
			buttonOpen.Size = new System.Drawing.Size(96, 24);
			buttonOpen.TabIndex = 0;
			buttonOpen.Text = "&Open";
			buttonOpen.UseVisualStyleBackColor = false;
			buttonOpen.Visible = false;
			buttonOpen.Click += new System.EventHandler(buttonGotoItem_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 15);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Visible = false;
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
			toolStrip1.Size = new System.Drawing.Size(888, 31);
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
			toolStripDropDownButton1.Image = Micromind.ClientUI.Properties.Resources.Export;
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
			toolStripButtonAllowGrouping.CheckOnClick = true;
			toolStripButtonAllowGrouping.Image = Micromind.ClientUI.Properties.Resources.Groupby;
			toolStripButtonAllowGrouping.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAllowGrouping.Name = "toolStripButtonAllowGrouping";
			toolStripButtonAllowGrouping.Size = new System.Drawing.Size(106, 28);
			toolStripButtonAllowGrouping.Text = "Allow Grouping";
			toolStripButtonAllowGrouping.Click += new System.EventHandler(toolStripButtonAllowGrouping_Click);
			toolStripButtonAutoFit.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonAutoFit.Image");
			toolStripButtonAutoFit.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAutoFit.Name = "toolStripButtonAutoFit";
			toolStripButtonAutoFit.Size = new System.Drawing.Size(90, 28);
			toolStripButtonAutoFit.Text = "Fit Columns";
			toolStripButtonAutoFit.ToolTipText = "Fit Columns to Screen";
			toolStripButtonAutoFit.Click += new System.EventHandler(toolStripButtonAutoFit_Click);
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(109, 28);
			toolStripButton1.Text = "Resize Columns";
			toolStripButton1.ToolTipText = "Resize columns to length of text";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonColumnChooser.Image = Micromind.ClientUI.Properties.Resources.ColumnChooser;
			toolStripButtonColumnChooser.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonColumnChooser.Name = "toolStripButtonColumnChooser";
			toolStripButtonColumnChooser.Size = new System.Drawing.Size(75, 28);
			toolStripButtonColumnChooser.Text = "Columns";
			toolStripButtonColumnChooser.Click += new System.EventHandler(toolStripButtonColumnChooser_Click);
			toolStripButtonMerge.CheckOnClick = true;
			toolStripButtonMerge.Image = Micromind.ClientUI.Properties.Resources.merge;
			toolStripButtonMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMerge.Name = "toolStripButtonMerge";
			toolStripButtonMerge.Size = new System.Drawing.Size(90, 28);
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
			dataGridList.Location = new System.Drawing.Point(12, 79);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(864, 261);
			dataGridList.TabIndex = 3;
			dataGridList.TabStop = false;
			dataGridList.Text = "dataGridList1";
			ultraGridPrintDocument1.Grid = dataGridList;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 12, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(368, 30);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(279, 48);
			dateControl1.TabIndex = 2;
			dateControl1.ToDate = new System.DateTime(2017, 12, 25, 23, 59, 59, 59);
			pictureBoxVoid.Image = Micromind.ClientUI.Properties.Resources.voidicon;
			pictureBoxVoid.Location = new System.Drawing.Point(802, 30);
			pictureBoxVoid.Name = "pictureBoxVoid";
			pictureBoxVoid.Size = new System.Drawing.Size(26, 20);
			pictureBoxVoid.TabIndex = 291;
			pictureBoxVoid.TabStop = false;
			pictureBoxVoid.Visible = false;
			textBoxDescription.Location = new System.Drawing.Point(93, 55);
			textBoxDescription.MaxLength = 255;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.ReadOnly = true;
			textBoxDescription.Size = new System.Drawing.Size(268, 20);
			textBoxDescription.TabIndex = 1;
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = textBoxDescription;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance13;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomer.Editable = true;
			comboBoxCustomer.FilterString = "";
			comboBoxCustomer.FilterSysDocID = "";
			comboBoxCustomer.HasAll = false;
			comboBoxCustomer.HasCustom = false;
			comboBoxCustomer.IsDataLoaded = false;
			comboBoxCustomer.Location = new System.Drawing.Point(93, 33);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ShowConsignmentOnly = false;
			comboBoxCustomer.ShowInactive = false;
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = false;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(268, 20);
			comboBoxCustomer.TabIndex = 0;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 34);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(54, 13);
			label1.TabIndex = 297;
			label1.Text = "Customer:";
			dataGridPDC.AllowUnfittedView = false;
			dataGridPDC.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPDC.DisplayLayout.Appearance = appearance25;
			dataGridPDC.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPDC.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPDC.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPDC.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridPDC.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPDC.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridPDC.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPDC.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPDC.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPDC.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridPDC.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPDC.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridPDC.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPDC.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridPDC.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPDC.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPDC.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridPDC.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridPDC.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPDC.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridPDC.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridPDC.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPDC.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGridPDC.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPDC.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPDC.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPDC.Location = new System.Drawing.Point(0, -1);
			dataGridPDC.Name = "dataGridPDC";
			dataGridPDC.ShowDeleteMenu = false;
			dataGridPDC.ShowMinusInRed = true;
			dataGridPDC.ShowNewMenu = false;
			dataGridPDC.Size = new System.Drawing.Size(563, 159);
			dataGridPDC.TabIndex = 298;
			dataGridPDC.TabStop = false;
			dataGridPDC.Text = "dataGridList1";
			label2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(621, 343);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(90, 13);
			label2.TabIndex = 299;
			label2.Text = "Total Balance:";
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(622, 364);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(36, 13);
			label3.TabIndex = 299;
			label3.Text = "PDC:";
			label4.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(622, 387);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(81, 13);
			label4.TabIndex = 299;
			label4.Text = "Net Balance:";
			textBoxTotalBalance.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalBalance.CustomReportFieldName = "";
			textBoxTotalBalance.CustomReportKey = "";
			textBoxTotalBalance.CustomReportValueType = 1;
			textBoxTotalBalance.IsComboTextBox = false;
			textBoxTotalBalance.Location = new System.Drawing.Point(726, 341);
			textBoxTotalBalance.Name = "textBoxTotalBalance";
			textBoxTotalBalance.ReadOnly = true;
			textBoxTotalBalance.Size = new System.Drawing.Size(149, 20);
			textBoxTotalBalance.TabIndex = 5;
			textBoxTotalBalance.TabStop = false;
			textBoxTotalBalance.Text = "0.00";
			textBoxTotalBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalBalance.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxPDC.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxPDC.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPDC.CustomReportFieldName = "";
			textBoxPDC.CustomReportKey = "";
			textBoxPDC.CustomReportValueType = 1;
			textBoxPDC.IsComboTextBox = false;
			textBoxPDC.Location = new System.Drawing.Point(726, 362);
			textBoxPDC.Name = "textBoxPDC";
			textBoxPDC.ReadOnly = true;
			textBoxPDC.Size = new System.Drawing.Size(149, 20);
			textBoxPDC.TabIndex = 6;
			textBoxPDC.TabStop = false;
			textBoxPDC.Text = "0.00";
			textBoxPDC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxPDC.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxNet.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxNet.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxNet.CustomReportFieldName = "";
			textBoxNet.CustomReportKey = "";
			textBoxNet.CustomReportValueType = 1;
			textBoxNet.IsComboTextBox = false;
			textBoxNet.Location = new System.Drawing.Point(726, 384);
			textBoxNet.Name = "textBoxNet";
			textBoxNet.ReadOnly = true;
			textBoxNet.Size = new System.Drawing.Size(149, 20);
			textBoxNet.TabIndex = 7;
			textBoxNet.TabStop = false;
			textBoxNet.Text = "0.00";
			textBoxNet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxNet.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			panelConsign.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panelConsign.Controls.Add(textBoxExposure);
			panelConsign.Controls.Add(textBoxConsign);
			panelConsign.Controls.Add(label6);
			panelConsign.Controls.Add(label7);
			panelConsign.Location = new System.Drawing.Point(616, 424);
			panelConsign.Name = "panelConsign";
			panelConsign.Size = new System.Drawing.Size(268, 75);
			panelConsign.TabIndex = 8;
			textBoxExposure.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxExposure.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxExposure.CustomReportFieldName = "";
			textBoxExposure.CustomReportKey = "";
			textBoxExposure.CustomReportValueType = 1;
			textBoxExposure.IsComboTextBox = false;
			textBoxExposure.Location = new System.Drawing.Point(110, 24);
			textBoxExposure.Name = "textBoxExposure";
			textBoxExposure.ReadOnly = true;
			textBoxExposure.Size = new System.Drawing.Size(149, 20);
			textBoxExposure.TabIndex = 1;
			textBoxExposure.TabStop = false;
			textBoxExposure.Text = "0.00";
			textBoxExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxExposure.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxConsign.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxConsign.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxConsign.CustomReportFieldName = "";
			textBoxConsign.CustomReportKey = "";
			textBoxConsign.CustomReportValueType = 1;
			textBoxConsign.IsComboTextBox = false;
			textBoxConsign.Location = new System.Drawing.Point(110, 3);
			textBoxConsign.Name = "textBoxConsign";
			textBoxConsign.ReadOnly = true;
			textBoxConsign.Size = new System.Drawing.Size(149, 20);
			textBoxConsign.TabIndex = 0;
			textBoxConsign.TabStop = false;
			textBoxConsign.Text = "0.00";
			textBoxConsign.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxConsign.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label6.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(7, 26);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(96, 13);
			label6.TabIndex = 301;
			label6.Text = "Total Exposure:";
			label7.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label7.Location = new System.Drawing.Point(6, 5);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(92, 13);
			label7.TabIndex = 302;
			label7.Text = "Consign Value:";
			navBarControl1.ActiveGroup = navBarGroupConsignment;
			navBarControl1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			navBarControl1.Controls.Add(navBarGroupControlContainer1);
			navBarControl1.Controls.Add(navBarGroupControlContainer2);
			navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[2]
			{
				navBarGroupPDC,
				navBarGroupConsignment
			});
			navBarControl1.Location = new System.Drawing.Point(12, 340);
			navBarControl1.Name = "navBarControl1";
			navBarControl1.OptionsNavPane.ExpandedWidth = 565;
			navBarControl1.Size = new System.Drawing.Size(565, 207);
			navBarControl1.TabIndex = 4;
			navBarControl1.Text = "navBarControl1";
			navBarControl1.View = new DevExpress.XtraNavBar.ViewInfo.Office1ViewInfoRegistrator();
			navBarGroupConsignment.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
			navBarGroupConsignment.Appearance.Options.UseBackColor = true;
			navBarGroupConsignment.Appearance.Options.UseTextOptions = true;
			navBarGroupConsignment.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			navBarGroupConsignment.Caption = "Open Consignments";
			navBarGroupConsignment.ControlContainer = navBarGroupControlContainer2;
			navBarGroupConsignment.Expanded = true;
			navBarGroupConsignment.GroupClientHeight = 80;
			navBarGroupConsignment.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
			navBarGroupConsignment.Name = "navBarGroupConsignment";
			navBarGroupControlContainer2.Controls.Add(dataGridConsign);
			navBarGroupControlContainer2.Name = "navBarGroupControlContainer2";
			navBarGroupControlContainer2.Size = new System.Drawing.Size(563, 160);
			navBarGroupControlContainer2.TabIndex = 1;
			dataGridConsign.AllowUnfittedView = false;
			dataGridConsign.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridConsign.DisplayLayout.Appearance = appearance37;
			dataGridConsign.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridConsign.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			dataGridConsign.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridConsign.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			dataGridConsign.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridConsign.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			dataGridConsign.DisplayLayout.MaxColScrollRegions = 1;
			dataGridConsign.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridConsign.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridConsign.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			dataGridConsign.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridConsign.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			dataGridConsign.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridConsign.DisplayLayout.Override.CellAppearance = appearance44;
			dataGridConsign.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridConsign.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			dataGridConsign.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			dataGridConsign.DisplayLayout.Override.HeaderAppearance = appearance46;
			dataGridConsign.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridConsign.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			dataGridConsign.DisplayLayout.Override.RowAppearance = appearance47;
			dataGridConsign.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridConsign.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			dataGridConsign.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridConsign.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridConsign.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridConsign.Location = new System.Drawing.Point(0, 0);
			dataGridConsign.Name = "dataGridConsign";
			dataGridConsign.ShowDeleteMenu = false;
			dataGridConsign.ShowMinusInRed = true;
			dataGridConsign.ShowNewMenu = false;
			dataGridConsign.Size = new System.Drawing.Size(562, 155);
			dataGridConsign.TabIndex = 299;
			dataGridConsign.TabStop = false;
			dataGridConsign.Text = "dataGridList1";
			navBarGroupControlContainer1.Controls.Add(dataGridPDC);
			navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
			navBarGroupControlContainer1.Size = new System.Drawing.Size(563, 160);
			navBarGroupControlContainer1.TabIndex = 0;
			navBarGroupPDC.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
			navBarGroupPDC.Appearance.Options.UseBackColor = true;
			navBarGroupPDC.Appearance.Options.UseTextOptions = true;
			navBarGroupPDC.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			navBarGroupPDC.AppearanceBackground.BackColor = System.Drawing.Color.WhiteSmoke;
			navBarGroupPDC.AppearanceBackground.Options.UseBackColor = true;
			navBarGroupPDC.Caption = "Post Dated Cheques";
			navBarGroupPDC.ControlContainer = navBarGroupControlContainer1;
			navBarGroupPDC.GroupClientHeight = 80;
			navBarGroupPDC.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
			navBarGroupPDC.Name = "navBarGroupPDC";
			textBoxCurrency.Location = new System.Drawing.Point(762, 55);
			textBoxCurrency.MaxLength = 255;
			textBoxCurrency.Name = "textBoxCurrency";
			textBoxCurrency.ReadOnly = true;
			textBoxCurrency.Size = new System.Drawing.Size(75, 20);
			textBoxCurrency.TabIndex = 304;
			radioButtonCustomerCurrency.AutoSize = true;
			radioButtonCustomerCurrency.Location = new System.Drawing.Point(652, 56);
			radioButtonCustomerCurrency.Name = "radioButtonCustomerCurrency";
			radioButtonCustomerCurrency.Size = new System.Drawing.Size(114, 17);
			radioButtonCustomerCurrency.TabIndex = 302;
			radioButtonCustomerCurrency.Text = "Customer Currency";
			radioButtonCustomerCurrency.UseVisualStyleBackColor = true;
			radioButtonBaseCurrency.AutoSize = true;
			radioButtonBaseCurrency.Checked = true;
			radioButtonBaseCurrency.Location = new System.Drawing.Point(652, 34);
			radioButtonBaseCurrency.Name = "radioButtonBaseCurrency";
			radioButtonBaseCurrency.Size = new System.Drawing.Size(94, 17);
			radioButtonBaseCurrency.TabIndex = 303;
			radioButtonBaseCurrency.TabStop = true;
			radioButtonBaseCurrency.Text = "Base Currency";
			radioButtonBaseCurrency.UseVisualStyleBackColor = true;
			buttonOutstandingList.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOutstandingList.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOutstandingList.BackColor = System.Drawing.Color.DarkGray;
			buttonOutstandingList.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOutstandingList.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOutstandingList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOutstandingList.Location = new System.Drawing.Point(742, 520);
			buttonOutstandingList.Name = "buttonOutstandingList";
			buttonOutstandingList.Size = new System.Drawing.Size(134, 24);
			buttonOutstandingList.TabIndex = 305;
			buttonOutstandingList.Text = "Outstanding Invoices";
			buttonOutstandingList.UseVisualStyleBackColor = false;
			buttonOutstandingList.Click += new System.EventHandler(buttonOutstandingList_Click);
			base.AcceptButton = buttonOpen;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonDone;
			base.ClientSize = new System.Drawing.Size(888, 598);
			base.Controls.Add(buttonOutstandingList);
			base.Controls.Add(textBoxCurrency);
			base.Controls.Add(radioButtonCustomerCurrency);
			base.Controls.Add(radioButtonBaseCurrency);
			base.Controls.Add(navBarControl1);
			base.Controls.Add(panelConsign);
			base.Controls.Add(textBoxNet);
			base.Controls.Add(textBoxPDC);
			base.Controls.Add(textBoxTotalBalance);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(comboBoxCustomer);
			base.Controls.Add(textBoxDescription);
			base.Controls.Add(pictureBoxVoid);
			base.Controls.Add(dateControl1);
			base.Controls.Add(dataGridList);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelButtons);
			DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(848, 562);
			base.Name = "CustomerLedgerForm";
			Text = "Customer Ledger";
			base.Load += new System.EventHandler(CompanyAccountsListForm_Load);
			panelButtons.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBoxVoid).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridPDC).EndInit();
			panelConsign.ResumeLayout(false);
			panelConsign.PerformLayout();
			((System.ComponentModel.ISupportInitialize)navBarControl1).EndInit();
			navBarControl1.ResumeLayout(false);
			navBarGroupControlContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridConsign).EndInit();
			navBarGroupControlContainer1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			LoadUserSettings();
		}

		private void SetupConsignList()
		{
			try
			{
				dataGridConsign.ApplyFormat();
				dataGridConsign.LoadLayout();
				dataGridConsign.ApplyUIDesign();
				dataGridConsign.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				dataGridConsign.ApplyFormat();
				if (dataGridConsign.DataSource != null)
				{
					dataGridConsign.ApplyQuantityColumnFormat(dataGridConsign.DisplayLayout.Bands[0].Columns["Value"], addSummary: true);
					UltraGridColumn ultraGridColumn = dataGridConsign.DisplayLayout.Bands[0].Columns["SysDocID"];
					bool hidden = dataGridConsign.DisplayLayout.Bands[0].Columns["CustomerID"].Hidden = true;
					ultraGridColumn.Hidden = hidden;
					if (dataGridConsign.DisplayLayout.Bands.Count > 0 && dataGridConsign.DisplayLayout.Bands[0].Summaries.Count > 0 && dataGridConsign.DisplayLayout.Bands[0].Summaries.Exists("count"))
					{
						dataGridConsign.DisplayLayout.Bands[0].Summaries["count"].SummaryPosition = SummaryPosition.UseSummaryPositionColumn;
						dataGridConsign.DisplayLayout.Bands[0].Summaries["count"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
						dataGridConsign.DisplayLayout.Bands[0].Summaries["count"].SummaryPositionColumn = dataGridConsign.DisplayLayout.Bands[0].Columns[1];
					}
					dataGridConsign.DisplayLayout.Override.AutoResizeColumnWidthOptions = AutoResizeColumnWidthOptions.All;
					dataGridConsign.DisplayLayout.Bands[0].Override.ColumnAutoSizeMode = ColumnAutoSizeMode.AllRowsInBand;
					dataGridConsign.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupPDCList()
		{
			try
			{
				dataGridPDC.ApplyFormat();
				dataGridPDC.LoadLayout();
				dataGridPDC.ApplyUIDesign();
				dataGridPDC.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				dataGridPDC.ApplyFormat();
				if (dataGridPDC.DataSource != null)
				{
					dataGridPDC.ApplyQuantityColumnFormat(dataGridPDC.DisplayLayout.Bands[0].Columns["Amount"], addSummary: true);
					UltraGridColumn ultraGridColumn = dataGridPDC.DisplayLayout.Bands[0].Columns["SysDocID"];
					UltraGridColumn ultraGridColumn2 = dataGridPDC.DisplayLayout.Bands[0].Columns["BankID"];
					bool flag2 = dataGridPDC.DisplayLayout.Bands[0].Columns["PayeeID"].Hidden = true;
					bool hidden = ultraGridColumn2.Hidden = flag2;
					ultraGridColumn.Hidden = hidden;
					if (dataGridPDC.DisplayLayout.Bands.Count > 0 && dataGridPDC.DisplayLayout.Bands[0].Summaries.Count > 0 && dataGridPDC.DisplayLayout.Bands[0].Summaries.Exists("count"))
					{
						dataGridPDC.DisplayLayout.Bands[0].Summaries["count"].SummaryPosition = SummaryPosition.UseSummaryPositionColumn;
						dataGridPDC.DisplayLayout.Bands[0].Summaries["count"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
						dataGridPDC.DisplayLayout.Bands[0].Summaries["count"].SummaryPositionColumn = dataGridPDC.DisplayLayout.Bands[0].Columns[1];
					}
					dataGridPDC.DisplayLayout.Override.AutoResizeColumnWidthOptions = AutoResizeColumnWidthOptions.All;
					dataGridPDC.DisplayLayout.Bands[0].Override.ColumnAutoSizeMode = ColumnAutoSizeMode.AllRowsInBand;
					dataGridPDC.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupList()
		{
			try
			{
				dataGridList.ApplyFormat();
				HideShowColumns();
				dataGridList.LoadLayout();
				dataGridList.ApplyUIDesign();
				dataGridList.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
				dataGridList.ApplyFormat();
				if (dataGridList.DataSource != null)
				{
					dataGridList.ApplyNumericColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Debit"], addSummary: true);
					dataGridList.ApplyNumericColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Credit"], addSummary: true);
					dataGridList.ApplyNumericColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["Balance"], addSummary: false);
					dataGridList.ApplyNumericColumnFormat(dataGridList.DisplayLayout.Bands[0].Columns["CurrencyRate"], addSummary: false);
					dataGridList.DisplayLayout.Bands[0].Columns["CurrencyRate"].Header.Caption = "Rate";
					dataGridList.DisplayLayout.Bands[0].Columns["CurrencyID"].Header.Caption = "CUR";
					ValueList sysDocTypesValueList = UILib.GetSysDocTypesValueList();
					dataGridList.DisplayLayout.Bands[0].Columns["SysDocType"].ValueList = sysDocTypesValueList;
					UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["Customer Code"];
					UltraGridColumn ultraGridColumn2 = dataGridList.DisplayLayout.Bands[0].Columns["Doc Type"];
					bool flag2 = dataGridList.DisplayLayout.Bands[0].Columns["Doc ID"].Hidden = true;
					bool hidden = ultraGridColumn2.Hidden = flag2;
					ultraGridColumn.Hidden = hidden;
					if (dataGridList.DisplayLayout.Bands.Count > 0 && dataGridList.DisplayLayout.Bands[0].Summaries.Count > 0 && dataGridList.DisplayLayout.Bands[0].Summaries.Exists("count"))
					{
						dataGridList.DisplayLayout.Bands[0].Summaries["count"].SummaryPosition = SummaryPosition.UseSummaryPositionColumn;
						dataGridList.DisplayLayout.Bands[0].Summaries["count"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
						dataGridList.DisplayLayout.Bands[0].Summaries["count"].SummaryPositionColumn = dataGridList.DisplayLayout.Bands[0].Columns[1];
					}
					dataGridList.DisplayLayout.Override.AllowColSizing = AllowColSizing.Free;
					dataGridList.DisplayLayout.Override.AutoResizeColumnWidthOptions = AutoResizeColumnWidthOptions.All;
					dataGridList.DisplayLayout.Bands[0].Override.ColumnAutoSizeMode = ColumnAutoSizeMode.AllRowsInBand;
					dataGridList.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
					dataGridList.DisplayLayout.Bands[0].Override.HeaderClickAction = HeaderClickAction.Select;
				}
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
					string text = "";
					if (radioButtonCustomerCurrency.Checked)
					{
						text = textBoxCurrency.Text;
					}
					if (text == Global.BaseCurrencyID)
					{
						text = "";
					}
					_ = (comboBoxCustomer.SelectedID == "");
					listData = Factory.CustomerSystem.GetCustomerBalanceDetailReport(dateControl1.FromDate, dateControl1.ToDate, SelectedID, SelectedID, "", "", "", "", "", "", "", "", "", "", showZeroBalance: true, text, "");
					if (listData != null && listData.Tables.Count != 0 && listData.Tables["Customer"].Rows.Count != 0)
					{
						bool result = false;
						bool.TryParse(listData.Tables["Customer"].Rows[0]["AllowConsignment"].ToString(), out result);
						if (result)
						{
							panelConsign.Visible = true;
							navBarGroupConsignment.Visible = true;
						}
						else
						{
							panelConsign.Visible = false;
							navBarGroupConsignment.Visible = false;
						}
						decimal result2 = default(decimal);
						decimal result3 = default(decimal);
						decimal result4 = default(decimal);
						decimal.TryParse(listData.Tables["Customer"].Rows[0]["Opening Balance"].ToString(), out result2);
						decimal.TryParse(listData.Tables["Customer"].Rows[0]["Ending Balance"].ToString(), out result3);
						decimal.TryParse(listData.Tables["Customer"].Rows[0]["PDC"].ToString(), out result4);
						textBoxTotalBalance.Text = result3.ToString(Format.TotalAmountFormat);
						textBoxPDC.Text = result4.ToString(Format.TotalAmountFormat);
						textBoxNet.Text = (result3 - result4).ToString(Format.TotalAmountFormat);
						DataRow dataRow = listData.Tables["ARJournal"].NewRow();
						dataRow["Description"] = "Opening Balance";
						dataRow["Date"] = dateControl1.FromDate;
						if (result2 > 0m)
						{
							dataRow["Debit"] = result2;
						}
						else
						{
							dataRow["Credit"] = Math.Abs(result2);
						}
						listData.Tables["ARJournal"].Rows.InsertAt(dataRow, 0);
						decimal num = default(decimal);
						listData.Tables["ARJournal"].Columns.Add("Balance", typeof(decimal));
						foreach (DataRow row in listData.Tables["ARJournal"].Rows)
						{
							decimal result5 = default(decimal);
							decimal result6 = default(decimal);
							if (row["Debit"] != DBNull.Value)
							{
								decimal.TryParse(row["Debit"].ToString(), out result5);
							}
							if (row["Credit"] != DBNull.Value)
							{
								decimal.TryParse(row["Credit"].ToString(), out result6);
							}
							num += result5 - result6;
							if (result5 == 0m)
							{
								row["Debit"] = DBNull.Value;
							}
							if (result6 == 0m)
							{
								row["Credit"] = DBNull.Value;
							}
							row["Balance"] = num;
						}
						decimal d = default(decimal);
						foreach (DataRow row2 in listData.Tables["Consignment"].Rows)
						{
							decimal result7 = default(decimal);
							if (row2["Value"] != DBNull.Value)
							{
								decimal.TryParse(row2["Value"].ToString(), out result7);
							}
							d += result7;
						}
						textBoxConsign.Text = d.ToString(Format.TotalAmountFormat);
						textBoxExposure.Text = (result3 + d).ToString(Format.TotalAmountFormat);
						dataGridList.DataSource = listData.Tables["ARJournal"];
						dataGridPDC.DataSource = listData.Tables["Cheque_Received"];
						dataGridConsign.DataSource = listData.Tables["Consignment"];
						dataGridList.DisplayLayout.Bands[0].Columns["Debit"].NullText = "";
						dataGridList.DisplayLayout.Bands[0].Columns["Credit"].NullText = "";
						dataGridList.DisplayLayout.Bands[0].Columns["DebitFC"].NullText = "";
						dataGridList.DisplayLayout.Bands[0].Columns["CreditFC"].NullText = "";
						if (radioButtonCustomerCurrency.Checked)
						{
							UltraGridColumn ultraGridColumn = dataGridList.DisplayLayout.Bands[0].Columns["DebitFC"];
							bool hidden = dataGridList.DisplayLayout.Bands[0].Columns["CreditFC"].Hidden = true;
							ultraGridColumn.Hidden = hidden;
						}
						else
						{
							UltraGridColumn ultraGridColumn2 = dataGridList.DisplayLayout.Bands[0].Columns["DebitFC"];
							bool hidden = dataGridList.DisplayLayout.Bands[0].Columns["CreditFC"].Hidden = false;
							ultraGridColumn2.Hidden = hidden;
						}
						ValueList sysDocTypesValueList = UILib.GetSysDocTypesValueList();
						dataGridList.DisplayLayout.Bands[0].Columns["SysDocType"].ValueList = sysDocTypesValueList;
					}
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
			ShowForm();
		}

		private void ShowForm()
		{
			if (dataGridList.ActiveRow.IsDataRow)
			{
				string selectedID = GetSelectedID();
				string selectedSysDocID = GetSelectedSysDocID();
				if (selectedID != null)
				{
					new FormHelper().EditTransaction(selectedSysDocID, selectedID);
				}
			}
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
					SetupPDCList();
					SetupConsignList();
					navBarGroupPDC.Expanded = true;
				}
			}
			catch (Exception e2)
			{
				dataGridList.LoadLayoutFailed = true;
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
			}
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
					else if (dataGridList.ActiveRow.Cells.Exists("Doc No"))
					{
						result = dataGridList.ActiveRow.Cells["Doc No"].Text.ToString();
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

		private void buttonOutstandingList_Click(object sender, EventArgs e)
		{
			CustomerOutStandingInvoiceListForm customerOutStandingInvoiceListForm = new CustomerOutStandingInvoiceListForm();
			customerOutStandingInvoiceListForm.CustomerID = comboBoxCustomer.SelectedID;
			customerOutStandingInvoiceListForm.ShowDialog();
		}
	}
}
