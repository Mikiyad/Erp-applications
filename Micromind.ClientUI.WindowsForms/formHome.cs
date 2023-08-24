using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraTab;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.WindowsForms.Main;
using Micromind.Common.Data;
using Micromind.DataControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms
{
	public class formHome : Form
	{
		private bool isFirstActivated;

		private DefaultLookAndFeel defaultLookAndFeel1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem resetLayoutToolStripMenuItem;

		public XtraTabControl tabControlMain;

		private ToolStripMenuItem toolStripMenuItem1;

		private NavBarGroup navBarGroup9;

		private ImageList imageListGroups;

		private StyleController styleController1;

		private XtraTabPage tabHome;

		private Dashboard dashboard1;

		private LabelControl labelControl1;

		private XtraTabControl xtraTabControl1;

		private XtraTabPage tabPageHome;

		private XRDesignPanel xrDesignPanel1;

		private XRDesignPanel xrDesignPanel2;

		private LabelControl labelCustomize;

		private IContainer components;

		public formHome(Form parent)
		{
			InitializeComponent();
			base.MdiParent = parent;
			base.Load += formHome_Load;
			base.FormClosing += formHome_FormClosing;
			EventHelper.RefreshApplicationRequested += EventHelper_RefreshApplicationRequested;
			tabControlMain.ShowTabHeader = DefaultBoolean.False;
		}

		private void EventHelper_RefreshApplicationRequested(object sender, EventArgs e)
		{
			Refresh();
		}

		private void formHome_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveLayout();
		}

		private void SaveLayout()
		{
			dashboard1.SaveLayout();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formHome));
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain = new DevExpress.XtraTab.XtraTabControl();
            this.tabHome = new DevExpress.XtraTab.XtraTabPage();
            this.labelCustomize = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageHome = new DevExpress.XtraTab.XtraTabPage();
            this.dashboard1 = new Micromind.DataControls.Dashboard();
            this.xrDesignPanel1 = new DevExpress.XtraReports.UserDesigner.XRDesignPanel();
            this.xrDesignPanel2 = new DevExpress.XtraReports.UserDesigner.XRDesignPanel();
            this.imageListGroups = new System.Windows.Forms.ImageList(this.components);
            this.navBarGroup9 = new DevExpress.XtraNavBar.NavBarGroup();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlMain)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPageHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xrDesignPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrDesignPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2007 Blue";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetLayoutToolStripMenuItem,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 52);
            // 
            // resetLayoutToolStripMenuItem
            // 
            this.resetLayoutToolStripMenuItem.Name = "resetLayoutToolStripMenuItem";
            this.resetLayoutToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
            this.resetLayoutToolStripMenuItem.Text = "Reset Layout";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(162, 24);
            this.toolStripMenuItem1.Text = "Customize...";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.HeaderButtons = DevExpress.XtraTab.TabButtons.None;
            this.tabControlMain.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Never;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.MultiLine = DevExpress.Utils.DefaultBoolean.False;
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedTabPage = this.tabHome;
            this.tabControlMain.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.tabControlMain.Size = new System.Drawing.Size(1062, 780);
            this.tabControlMain.TabIndex = 14;
            this.tabControlMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabHome});
            // 
            // tabHome
            // 
            this.tabHome.Controls.Add(this.labelCustomize);
            this.tabHome.Controls.Add(this.labelControl1);
            this.tabHome.Controls.Add(this.xtraTabControl1);
            this.tabHome.Controls.Add(this.xrDesignPanel1);
            this.tabHome.Controls.Add(this.xrDesignPanel2);
            this.tabHome.Name = "tabHome";
            this.tabHome.Size = new System.Drawing.Size(1056, 774);
            this.tabHome.Text = "Dashboard";
            // 
            // labelCustomize
            // 
            this.labelCustomize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCustomize.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.labelCustomize.Appearance.Options.UseForeColor = true;
            this.labelCustomize.Appearance.Options.UseTextOptions = true;
            this.labelCustomize.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelCustomize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelCustomize.Location = new System.Drawing.Point(860, 5);
            this.labelCustomize.Name = "labelCustomize";
            this.labelCustomize.Size = new System.Drawing.Size(192, 13);
            this.labelCustomize.TabIndex = 64;
            this.labelCustomize.Text = "Customize Dashboards...";
            this.labelCustomize.Click += new System.EventHandler(this.labelCustomize_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(88)))), ((int)(((byte)(134)))));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(11, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(132, 21);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "My Dashboards";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 28);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPageHome;
            this.xtraTabControl1.Size = new System.Drawing.Size(1056, 727);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageHome});
            // 
            // tabPageHome
            // 
            this.tabPageHome.Controls.Add(this.dashboard1);
            this.tabPageHome.Name = "tabPageHome";
            this.tabPageHome.Size = new System.Drawing.Size(1050, 697);
            this.tabPageHome.Text = "Home";
            // 
            // dashboard1
            // 
            this.dashboard1.AllowDrop = true;
            this.dashboard1.BackColor = System.Drawing.Color.White;
            this.dashboard1.DashboardKey = "DSHBDashboard";
            this.dashboard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboard1.Location = new System.Drawing.Point(0, 0);
            this.dashboard1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dashboard1.Name = "dashboard1";
            this.dashboard1.Size = new System.Drawing.Size(1050, 697);
            this.dashboard1.TabIndex = 0;
            // 
            // xrDesignPanel1
            // 
            this.xrDesignPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xrDesignPanel1.Location = new System.Drawing.Point(0, 0);
            this.xrDesignPanel1.Name = "xrDesignPanel1";
            this.xrDesignPanel1.Size = new System.Drawing.Size(1056, 28);
            this.xrDesignPanel1.TabIndex = 3;
            // 
            // xrDesignPanel2
            // 
            this.xrDesignPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xrDesignPanel2.Location = new System.Drawing.Point(0, 755);
            this.xrDesignPanel2.Name = "xrDesignPanel2";
            this.xrDesignPanel2.Size = new System.Drawing.Size(1056, 19);
            this.xrDesignPanel2.TabIndex = 4;
            // 
            // imageListGroups
            // 
            this.imageListGroups.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListGroups.ImageStream")));
            this.imageListGroups.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListGroups.Images.SetKeyName(0, "reports32.png");
            // 
            // navBarGroup9
            // 
            this.navBarGroup9.Caption = "Accounts";
            this.navBarGroup9.Expanded = true;
            this.navBarGroup9.Name = "navBarGroup9";
            // 
            // styleController1
            // 
            this.styleController1.LookAndFeel.SkinName = "VS2010";
            this.styleController1.LookAndFeel.UseDefaultLookAndFeel = false;
            // 
            // formHome
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1062, 780);
            this.ControlBox = false;
            this.Controls.Add(this.tabControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formHome";
            this.ShowInTaskbar = false;
            this.Text = "Home Page";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlMain)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabHome.ResumeLayout(false);
            this.tabHome.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPageHome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xrDesignPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrDesignPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            this.ResumeLayout(false);

		}

		private void formHome_Load(object sender, EventArgs e)
		{
			try
			{
				Init();
				RegistryHelper registryHelper = new RegistryHelper();
				string stringValue = registryHelper.GetStringValue(registryHelper.CurrentWindowsUserKey, "Skin", "");
				if (stringValue != "")
				{
					defaultLookAndFeel1.LookAndFeel.SkinName = stringValue;
				}
				else
				{
					defaultLookAndFeel1.LookAndFeel.SkinName = "iMaginary";
				}
				LoadUserSettings();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadUserSettings()
		{
			dashboard1.LoadLayout();
		}

		private void Init()
		{
		}

		public void OnActivated()
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					if (!isFirstActivated)
					{
						isFirstActivated = true;
					}
				}
				finally
				{
					Global.ChangeApplicationStatusMessage(Text);
				}
			}
		}

		private void CompanyMessage()
		{
		}

		public void Reset()
		{
			CompanyMessage();
		}

		public static ScreenAreas GetScreenArea()
		{
			return ScreenAreas.General;
		}

		public static int GetScreenID()
		{
			return 0;
		}

		public void RefreshData()
		{
			OnActivated();
		}

		private void navBarControl2_Click(object sender, EventArgs e)
		{
		}

		private void labelCustomize_Click(object sender, EventArgs e)
		{
			new CustomizeDashboardsForm().ShowDialog(this);
		}
	}
}
