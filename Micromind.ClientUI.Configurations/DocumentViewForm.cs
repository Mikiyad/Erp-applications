using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class DocumentViewForm : Form
	{
		private IContainer components;

		private PrintControl printControl1;

		private PrintRibbonController printRibbonController1;

		private RibbonControl ribbonControl1;

		private PrintPreviewBarItem printPreviewBarItem2;

		private PrintPreviewBarItem printPreviewBarItem3;

		private PrintPreviewBarItem printPreviewBarItem4;

		private PrintPreviewBarItem printPreviewBarItem5;

		private PrintPreviewBarItem printPreviewBarItem6;

		private PrintPreviewBarItem printPreviewBarItem7;

		private PrintPreviewBarItem printPreviewBarItem9;

		private PrintPreviewBarItem printPreviewBarItem10;

		private PrintPreviewBarItem printPreviewBarItem11;

		private PrintPreviewBarItem printPreviewBarItem12;

		private PrintPreviewBarItem printPreviewBarItem13;

		private PrintPreviewBarItem printPreviewBarItem14;

		private PrintPreviewBarItem printPreviewBarItem15;

		private PrintPreviewBarItem printPreviewBarItem16;

		private PrintPreviewBarItem printPreviewBarItem17;

		private PrintPreviewBarItem printPreviewBarItem18;

		private PrintPreviewBarItem printPreviewBarItem19;

		private PrintPreviewBarItem printPreviewBarItem20;

		private PrintPreviewBarItem printPreviewBarItem21;

		private PrintPreviewBarItem printPreviewBarItem23;

		private PrintPreviewBarItem printPreviewBarItem24;

		private PrintPreviewBarItem printPreviewBarItem25;

		private PrintPreviewBarItem printPreviewBarItem26;

		private PrintPreviewBarItem printPreviewBarItem27;

		private PrintPreviewBarItem printPreviewBarItem28;

		private PrintPreviewBarItem printPreviewBarItem29;

		private PrintPreviewBarItem printPreviewBarItem30;

		private PrintPreviewBarItem printPreviewBarItem31;

		private PrintPreviewBarItem printPreviewBarItem32;

		private PrintPreviewBarItem printPreviewBarItem33;

		private PrintPreviewBarItem printPreviewBarItem34;

		private PrintPreviewBarItem printPreviewBarItem35;

		private PrintPreviewBarItem printPreviewBarItem36;

		private PrintPreviewBarItem printPreviewBarItem37;

		private PrintPreviewBarItem printPreviewBarItem38;

		private PrintPreviewBarItem printPreviewBarItem39;

		private PrintPreviewBarItem printPreviewBarItem40;

		private PrintPreviewBarItem printPreviewBarItem41;

		private PrintPreviewBarItem printPreviewBarItem42;

		private PrintPreviewBarItem printPreviewBarItem43;

		private PrintPreviewBarItem printPreviewBarItem44;

		private PrintPreviewBarItem printPreviewBarItem45;

		private PrintPreviewStaticItem printPreviewStaticItem1;

		private BarStaticItem barStaticItem1;

		private ProgressBarEditItem progressBarEditItem1;

		private RepositoryItemProgressBar repositoryItemProgressBar1;

		private PrintPreviewBarItem printPreviewBarItem48;

		private BarButtonItem barButtonItem1;

		private PrintPreviewStaticItem printPreviewStaticItem2;

		private ZoomTrackBarEditItem zoomTrackBarEditItem1;

		private RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;

		private PrintPreviewRibbonPage printPreviewRibbonPage1;

		private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup2;

		private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup3;

		private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup4;

		private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup5;

		private RibbonStatusBar ribbonStatusBar1;

		public XtraReport Document
		{
			get
			{
				return null;
			}
			set
			{
				printControl1.PrintingSystem = value.PrintingSystem;
				value.CreateDocument();
			}
		}

		public DocumentViewForm()
		{
			InitializeComponent();
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.DocumentViewForm));
			DevExpress.Utils.SuperToolTip superToolTip = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem6 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem6 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip7 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem7 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem7 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip8 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem8 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem8 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip9 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem9 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem9 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip10 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem10 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem10 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip11 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem11 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem11 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip12 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem12 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem12 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip13 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem13 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem13 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip14 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem14 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem14 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip15 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem15 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem15 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip16 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem16 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem16 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip17 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem17 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem17 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip18 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem18 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem18 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip19 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem19 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem19 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip20 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem20 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem20 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip21 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem21 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem21 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip22 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem22 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem22 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip23 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem23 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem23 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip24 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem24 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem24 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip25 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem25 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem25 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip26 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem26 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem26 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip27 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem27 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem27 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip28 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem28 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem28 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip29 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem29 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem29 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip30 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem30 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem30 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip31 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem31 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem31 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip32 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem32 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem32 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip33 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem33 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem33 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip34 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem34 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem34 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip35 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem35 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem35 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip36 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem36 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem36 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip37 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem37 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem37 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip38 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem38 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem38 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip39 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem39 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem39 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip40 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem40 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem40 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip41 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem41 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem41 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip42 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem42 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem42 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip43 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem43 = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem43 = new DevExpress.Utils.ToolTipItem();
			printControl1 = new DevExpress.XtraPrinting.Control.PrintControl();
			printRibbonController1 = new DevExpress.XtraPrinting.Preview.PrintRibbonController(components);
			ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
			printPreviewBarItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem4 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem5 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem6 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem7 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem9 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem10 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem11 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem12 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem13 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem14 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem15 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem16 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem17 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem18 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem19 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem20 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem21 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem23 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem24 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem25 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem26 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem27 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem28 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem29 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem30 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem31 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem32 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem33 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem34 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem35 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem36 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem37 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem38 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem39 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem40 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem41 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem42 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem43 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem44 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem45 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewStaticItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
			barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
			progressBarEditItem1 = new DevExpress.XtraPrinting.Preview.ProgressBarEditItem();
			repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
			printPreviewBarItem48 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			printPreviewStaticItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
			zoomTrackBarEditItem1 = new DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem();
			repositoryItemZoomTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
			printPreviewRibbonPage1 = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPage();
			printPreviewRibbonPageGroup2 = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroup();
			printPreviewRibbonPageGroup3 = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroup();
			printPreviewRibbonPageGroup4 = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroup();
			printPreviewRibbonPageGroup5 = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroup();
			ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
			((System.ComponentModel.ISupportInitialize)printRibbonController1).BeginInit();
			((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemZoomTrackBar1).BeginInit();
			SuspendLayout();
			printControl1.BackColor = System.Drawing.Color.Empty;
			printControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			printControl1.ForeColor = System.Drawing.Color.Empty;
			printControl1.IsMetric = false;
			printControl1.Location = new System.Drawing.Point(0, 121);
			printControl1.Name = "printControl1";
			printControl1.Size = new System.Drawing.Size(1091, 500);
			printControl1.TabIndex = 0;
			printControl1.TooltipFont = new System.Drawing.Font("Tahoma", 8.25f);
			printRibbonController1.PrintControl = printControl1;
			printRibbonController1.RibbonControl = ribbonControl1;
			printRibbonController1.RibbonStatusBar = ribbonStatusBar1;
			ribbonControl1.ExpandCollapseItem.Id = 0;
			ribbonControl1.ExpandCollapseItem.Name = "";
			ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[50]
			{
				ribbonControl1.ExpandCollapseItem,
				printPreviewBarItem2,
				printPreviewBarItem3,
				printPreviewBarItem4,
				printPreviewBarItem5,
				printPreviewBarItem6,
				printPreviewBarItem7,
				printPreviewBarItem9,
				printPreviewBarItem10,
				printPreviewBarItem11,
				printPreviewBarItem12,
				printPreviewBarItem13,
				printPreviewBarItem14,
				printPreviewBarItem15,
				printPreviewBarItem16,
				printPreviewBarItem17,
				printPreviewBarItem18,
				printPreviewBarItem19,
				printPreviewBarItem20,
				printPreviewBarItem21,
				printPreviewBarItem23,
				printPreviewBarItem24,
				printPreviewBarItem25,
				printPreviewBarItem26,
				printPreviewBarItem27,
				printPreviewBarItem28,
				printPreviewBarItem29,
				printPreviewBarItem30,
				printPreviewBarItem31,
				printPreviewBarItem32,
				printPreviewBarItem33,
				printPreviewBarItem34,
				printPreviewBarItem35,
				printPreviewBarItem36,
				printPreviewBarItem37,
				printPreviewBarItem38,
				printPreviewBarItem39,
				printPreviewBarItem40,
				printPreviewBarItem41,
				printPreviewBarItem42,
				printPreviewBarItem43,
				printPreviewBarItem44,
				printPreviewBarItem45,
				printPreviewStaticItem1,
				barStaticItem1,
				progressBarEditItem1,
				printPreviewBarItem48,
				barButtonItem1,
				printPreviewStaticItem2,
				zoomTrackBarEditItem1
			});
			ribbonControl1.Location = new System.Drawing.Point(0, 0);
			ribbonControl1.MaxItemId = 57;
			ribbonControl1.Name = "ribbonControl1";
			ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[1]
			{
				printPreviewRibbonPage1
			});
			ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[2]
			{
				repositoryItemProgressBar1,
				repositoryItemZoomTrackBar1
			});
			ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
			ribbonControl1.ShowToolbarCustomizeItem = false;
			ribbonControl1.Size = new System.Drawing.Size(1091, 121);
			ribbonControl1.StatusBar = ribbonStatusBar1;
			ribbonControl1.Toolbar.ShowCustomizeItem = false;
			ribbonControl1.TransparentEditors = true;
			printPreviewBarItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem2.Caption = "Parameters";
			printPreviewBarItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Parameters;
			printPreviewBarItem2.ContextSpecifier = printRibbonController1;
			printPreviewBarItem2.Enabled = false;
			printPreviewBarItem2.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem2.Glyph");
			printPreviewBarItem2.Id = 2;
			printPreviewBarItem2.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem2.LargeGlyph");
			printPreviewBarItem2.Name = "printPreviewBarItem2";
			superToolTip.FixedTooltipWidth = true;
			toolTipTitleItem.Text = "Parameters";
			toolTipItem.LeftIndent = 6;
			toolTipItem.Text = "Open the Parameters pane, which allows you to enter values for report parameters.";
			superToolTip.Items.Add(toolTipTitleItem);
			superToolTip.Items.Add(toolTipItem);
			superToolTip.MaxWidth = 210;
			printPreviewBarItem2.SuperTip = superToolTip;
			printPreviewBarItem3.Caption = "Find";
			printPreviewBarItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find;
			printPreviewBarItem3.ContextSpecifier = printRibbonController1;
			printPreviewBarItem3.Enabled = false;
			printPreviewBarItem3.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem3.Glyph");
			printPreviewBarItem3.Id = 3;
			printPreviewBarItem3.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem3.LargeGlyph");
			printPreviewBarItem3.Name = "printPreviewBarItem3";
			superToolTip2.FixedTooltipWidth = true;
			toolTipTitleItem2.Text = "Find";
			toolTipItem2.LeftIndent = 6;
			toolTipItem2.Text = "Show the Find dialog to find text in the document.";
			superToolTip2.Items.Add(toolTipTitleItem2);
			superToolTip2.Items.Add(toolTipItem2);
			superToolTip2.MaxWidth = 210;
			printPreviewBarItem3.SuperTip = superToolTip2;
			printPreviewBarItem4.Caption = "Options";
			printPreviewBarItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize;
			printPreviewBarItem4.ContextSpecifier = printRibbonController1;
			printPreviewBarItem4.Enabled = false;
			printPreviewBarItem4.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem4.Glyph");
			printPreviewBarItem4.Id = 4;
			printPreviewBarItem4.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem4.LargeGlyph");
			printPreviewBarItem4.Name = "printPreviewBarItem4";
			superToolTip3.FixedTooltipWidth = true;
			toolTipTitleItem3.Text = "Options";
			toolTipItem3.LeftIndent = 6;
			toolTipItem3.Text = "Open the Print Options dialog, in which you can change printing options.";
			superToolTip3.Items.Add(toolTipTitleItem3);
			superToolTip3.Items.Add(toolTipItem3);
			superToolTip3.MaxWidth = 210;
			printPreviewBarItem4.SuperTip = superToolTip3;
			printPreviewBarItem5.Caption = "Print";
			printPreviewBarItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print;
			printPreviewBarItem5.ContextSpecifier = printRibbonController1;
			printPreviewBarItem5.Enabled = false;
			printPreviewBarItem5.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem5.Glyph");
			printPreviewBarItem5.Id = 5;
			printPreviewBarItem5.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem5.LargeGlyph");
			printPreviewBarItem5.Name = "printPreviewBarItem5";
			superToolTip4.FixedTooltipWidth = true;
			toolTipTitleItem4.Text = "Print (Ctrl+P)";
			toolTipItem4.LeftIndent = 6;
			toolTipItem4.Text = "Select a printer, number of copies and other printing options before printing.";
			superToolTip4.Items.Add(toolTipTitleItem4);
			superToolTip4.Items.Add(toolTipItem4);
			superToolTip4.MaxWidth = 210;
			printPreviewBarItem5.SuperTip = superToolTip4;
			printPreviewBarItem6.Caption = "Quick Print";
			printPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect;
			printPreviewBarItem6.ContextSpecifier = printRibbonController1;
			printPreviewBarItem6.Enabled = false;
			printPreviewBarItem6.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem6.Glyph");
			printPreviewBarItem6.Id = 6;
			printPreviewBarItem6.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem6.LargeGlyph");
			printPreviewBarItem6.Name = "printPreviewBarItem6";
			superToolTip5.FixedTooltipWidth = true;
			toolTipTitleItem5.Text = "Quick Print";
			toolTipItem5.LeftIndent = 6;
			toolTipItem5.Text = "Send the document directly to the default printer without making changes.";
			superToolTip5.Items.Add(toolTipTitleItem5);
			superToolTip5.Items.Add(toolTipItem5);
			superToolTip5.MaxWidth = 210;
			printPreviewBarItem6.SuperTip = superToolTip5;
			printPreviewBarItem7.Caption = "Custom Margins...";
			printPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup;
			printPreviewBarItem7.ContextSpecifier = printRibbonController1;
			printPreviewBarItem7.Enabled = false;
			printPreviewBarItem7.Id = 7;
			printPreviewBarItem7.Name = "printPreviewBarItem7";
			superToolTip6.FixedTooltipWidth = true;
			toolTipTitleItem6.Text = "Page Setup";
			toolTipItem6.LeftIndent = 6;
			toolTipItem6.Text = "Show the Page Setup dialog.";
			superToolTip6.Items.Add(toolTipTitleItem6);
			superToolTip6.Items.Add(toolTipItem6);
			superToolTip6.MaxWidth = 210;
			printPreviewBarItem7.SuperTip = superToolTip6;
			printPreviewBarItem9.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem9.Caption = "Scale";
			printPreviewBarItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Scale;
			printPreviewBarItem9.ContextSpecifier = printRibbonController1;
			printPreviewBarItem9.Enabled = false;
			printPreviewBarItem9.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem9.Glyph");
			printPreviewBarItem9.Id = 9;
			printPreviewBarItem9.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem9.LargeGlyph");
			printPreviewBarItem9.Name = "printPreviewBarItem9";
			superToolTip7.FixedTooltipWidth = true;
			toolTipTitleItem7.Text = "Scale";
			toolTipItem7.LeftIndent = 6;
			toolTipItem7.Text = "Stretch or shrink the printed output to a percentage of its actual size.";
			superToolTip7.Items.Add(toolTipTitleItem7);
			superToolTip7.Items.Add(toolTipItem7);
			superToolTip7.MaxWidth = 210;
			printPreviewBarItem9.SuperTip = superToolTip7;
			printPreviewBarItem10.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem10.Caption = "Pointer";
			printPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Pointer;
			printPreviewBarItem10.ContextSpecifier = printRibbonController1;
			printPreviewBarItem10.Down = true;
			printPreviewBarItem10.Enabled = false;
			printPreviewBarItem10.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem10.Glyph");
			printPreviewBarItem10.GroupIndex = 1;
			printPreviewBarItem10.Id = 10;
			printPreviewBarItem10.Name = "printPreviewBarItem10";
			printPreviewBarItem10.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
			superToolTip8.FixedTooltipWidth = true;
			toolTipTitleItem8.Text = "Mouse Pointer";
			toolTipItem8.LeftIndent = 6;
			toolTipItem8.Text = "Show the mouse pointer.";
			superToolTip8.Items.Add(toolTipTitleItem8);
			superToolTip8.Items.Add(toolTipItem8);
			superToolTip8.MaxWidth = 210;
			printPreviewBarItem10.SuperTip = superToolTip8;
			printPreviewBarItem11.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem11.Caption = "Hand Tool";
			printPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.HandTool;
			printPreviewBarItem11.ContextSpecifier = printRibbonController1;
			printPreviewBarItem11.Enabled = false;
			printPreviewBarItem11.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem11.Glyph");
			printPreviewBarItem11.GroupIndex = 1;
			printPreviewBarItem11.Id = 11;
			printPreviewBarItem11.Name = "printPreviewBarItem11";
			printPreviewBarItem11.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
			superToolTip9.FixedTooltipWidth = true;
			toolTipTitleItem9.Text = "Hand Tool";
			toolTipItem9.LeftIndent = 6;
			toolTipItem9.Text = "Invoke the Hand tool to manually scroll through pages.";
			superToolTip9.Items.Add(toolTipTitleItem9);
			superToolTip9.Items.Add(toolTipItem9);
			superToolTip9.MaxWidth = 210;
			printPreviewBarItem11.SuperTip = superToolTip9;
			printPreviewBarItem12.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem12.Caption = "Magnifier";
			printPreviewBarItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Magnifier;
			printPreviewBarItem12.ContextSpecifier = printRibbonController1;
			printPreviewBarItem12.Enabled = false;
			printPreviewBarItem12.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem12.Glyph");
			printPreviewBarItem12.GroupIndex = 1;
			printPreviewBarItem12.Id = 12;
			printPreviewBarItem12.Name = "printPreviewBarItem12";
			printPreviewBarItem12.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
			superToolTip10.FixedTooltipWidth = true;
			toolTipTitleItem10.Text = "Magnifier";
			toolTipItem10.LeftIndent = 6;
			toolTipItem10.Text = "Invoke the Magnifier tool.\r\n\r\nClicking once on a document zooms it so that a single page becomes entirely visible, while clicking another time zooms it to 100% of the normal size.";
			superToolTip10.Items.Add(toolTipTitleItem10);
			superToolTip10.Items.Add(toolTipItem10);
			superToolTip10.MaxWidth = 210;
			printPreviewBarItem12.SuperTip = superToolTip10;
			printPreviewBarItem13.Caption = "Zoom Out";
			printPreviewBarItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut;
			printPreviewBarItem13.ContextSpecifier = printRibbonController1;
			printPreviewBarItem13.Enabled = false;
			printPreviewBarItem13.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem13.Glyph");
			printPreviewBarItem13.Id = 13;
			printPreviewBarItem13.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem13.LargeGlyph");
			printPreviewBarItem13.Name = "printPreviewBarItem13";
			superToolTip11.FixedTooltipWidth = true;
			toolTipTitleItem11.Text = "Zoom Out";
			toolTipItem11.LeftIndent = 6;
			toolTipItem11.Text = "Zoom out to see more of the page at a reduced size.";
			superToolTip11.Items.Add(toolTipTitleItem11);
			superToolTip11.Items.Add(toolTipItem11);
			superToolTip11.MaxWidth = 210;
			printPreviewBarItem13.SuperTip = superToolTip11;
			printPreviewBarItem14.Caption = "Zoom In";
			printPreviewBarItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn;
			printPreviewBarItem14.ContextSpecifier = printRibbonController1;
			printPreviewBarItem14.Enabled = false;
			printPreviewBarItem14.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem14.Glyph");
			printPreviewBarItem14.Id = 14;
			printPreviewBarItem14.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem14.LargeGlyph");
			printPreviewBarItem14.Name = "printPreviewBarItem14";
			superToolTip12.FixedTooltipWidth = true;
			toolTipTitleItem12.Text = "Zoom In";
			toolTipItem12.LeftIndent = 6;
			toolTipItem12.Text = "Zoom in to get a close-up view of the document.";
			superToolTip12.Items.Add(toolTipTitleItem12);
			superToolTip12.Items.Add(toolTipItem12);
			superToolTip12.MaxWidth = 210;
			printPreviewBarItem14.SuperTip = superToolTip12;
			printPreviewBarItem15.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem15.Caption = "Zoom";
			printPreviewBarItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Zoom;
			printPreviewBarItem15.ContextSpecifier = printRibbonController1;
			printPreviewBarItem15.Enabled = false;
			printPreviewBarItem15.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem15.Glyph");
			printPreviewBarItem15.Id = 15;
			printPreviewBarItem15.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem15.LargeGlyph");
			printPreviewBarItem15.Name = "printPreviewBarItem15";
			superToolTip13.FixedTooltipWidth = true;
			toolTipTitleItem13.Text = "Zoom";
			toolTipItem13.LeftIndent = 6;
			toolTipItem13.Text = "Change the zoom level of the document preview.";
			superToolTip13.Items.Add(toolTipTitleItem13);
			superToolTip13.Items.Add(toolTipItem13);
			superToolTip13.MaxWidth = 210;
			printPreviewBarItem15.SuperTip = superToolTip13;
			printPreviewBarItem16.Caption = "First Page";
			printPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage;
			printPreviewBarItem16.ContextSpecifier = printRibbonController1;
			printPreviewBarItem16.Enabled = false;
			printPreviewBarItem16.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem16.Glyph");
			printPreviewBarItem16.Id = 16;
			printPreviewBarItem16.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem16.LargeGlyph");
			printPreviewBarItem16.Name = "printPreviewBarItem16";
			superToolTip14.FixedTooltipWidth = true;
			toolTipTitleItem14.Text = "First Page (Ctrl+Home)";
			toolTipItem14.LeftIndent = 6;
			toolTipItem14.Text = "Navigate to the first page of the document.";
			superToolTip14.Items.Add(toolTipTitleItem14);
			superToolTip14.Items.Add(toolTipItem14);
			superToolTip14.MaxWidth = 210;
			printPreviewBarItem16.SuperTip = superToolTip14;
			printPreviewBarItem17.Caption = "Previous Page";
			printPreviewBarItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage;
			printPreviewBarItem17.ContextSpecifier = printRibbonController1;
			printPreviewBarItem17.Enabled = false;
			printPreviewBarItem17.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem17.Glyph");
			printPreviewBarItem17.Id = 17;
			printPreviewBarItem17.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem17.LargeGlyph");
			printPreviewBarItem17.Name = "printPreviewBarItem17";
			superToolTip15.FixedTooltipWidth = true;
			toolTipTitleItem15.Text = "Previous Page (PageUp)";
			toolTipItem15.LeftIndent = 6;
			toolTipItem15.Text = "Navigate to the previous page of the document.";
			superToolTip15.Items.Add(toolTipTitleItem15);
			superToolTip15.Items.Add(toolTipItem15);
			superToolTip15.MaxWidth = 210;
			printPreviewBarItem17.SuperTip = superToolTip15;
			printPreviewBarItem18.Caption = "Next  Page ";
			printPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage;
			printPreviewBarItem18.ContextSpecifier = printRibbonController1;
			printPreviewBarItem18.Enabled = false;
			printPreviewBarItem18.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem18.Glyph");
			printPreviewBarItem18.Id = 18;
			printPreviewBarItem18.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem18.LargeGlyph");
			printPreviewBarItem18.Name = "printPreviewBarItem18";
			superToolTip16.FixedTooltipWidth = true;
			toolTipTitleItem16.Text = "Next Page (PageDown)";
			toolTipItem16.LeftIndent = 6;
			toolTipItem16.Text = "Navigate to the next page of the document.";
			superToolTip16.Items.Add(toolTipTitleItem16);
			superToolTip16.Items.Add(toolTipItem16);
			superToolTip16.MaxWidth = 210;
			printPreviewBarItem18.SuperTip = superToolTip16;
			printPreviewBarItem19.Caption = "Last  Page ";
			printPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage;
			printPreviewBarItem19.ContextSpecifier = printRibbonController1;
			printPreviewBarItem19.Enabled = false;
			printPreviewBarItem19.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem19.Glyph");
			printPreviewBarItem19.Id = 19;
			printPreviewBarItem19.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem19.LargeGlyph");
			printPreviewBarItem19.Name = "printPreviewBarItem19";
			superToolTip17.FixedTooltipWidth = true;
			toolTipTitleItem17.Text = "Last Page (Ctrl+End)";
			toolTipItem17.LeftIndent = 6;
			toolTipItem17.Text = "Navigate to the last page of the document.";
			superToolTip17.Items.Add(toolTipTitleItem17);
			superToolTip17.Items.Add(toolTipItem17);
			superToolTip17.MaxWidth = 210;
			printPreviewBarItem19.SuperTip = superToolTip17;
			printPreviewBarItem20.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem20.Caption = "Many Pages";
			printPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.MultiplePages;
			printPreviewBarItem20.ContextSpecifier = printRibbonController1;
			printPreviewBarItem20.Enabled = false;
			printPreviewBarItem20.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem20.Glyph");
			printPreviewBarItem20.Id = 20;
			printPreviewBarItem20.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem20.LargeGlyph");
			printPreviewBarItem20.Name = "printPreviewBarItem20";
			superToolTip18.FixedTooltipWidth = true;
			toolTipTitleItem18.Text = "View Many Pages";
			toolTipItem18.LeftIndent = 6;
			toolTipItem18.Text = "Choose the page layout to arrange the document pages in preview.";
			superToolTip18.Items.Add(toolTipTitleItem18);
			superToolTip18.Items.Add(toolTipItem18);
			superToolTip18.MaxWidth = 210;
			printPreviewBarItem20.SuperTip = superToolTip18;
			printPreviewBarItem21.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem21.Caption = "Page Color";
			printPreviewBarItem21.Command = DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground;
			printPreviewBarItem21.ContextSpecifier = printRibbonController1;
			printPreviewBarItem21.Enabled = false;
			printPreviewBarItem21.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem21.Glyph");
			printPreviewBarItem21.Id = 21;
			printPreviewBarItem21.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem21.LargeGlyph");
			printPreviewBarItem21.Name = "printPreviewBarItem21";
			superToolTip19.FixedTooltipWidth = true;
			toolTipTitleItem19.Text = "Background Color";
			toolTipItem19.LeftIndent = 6;
			toolTipItem19.Text = "Choose a color for the background of the document pages.";
			superToolTip19.Items.Add(toolTipTitleItem19);
			superToolTip19.Items.Add(toolTipItem19);
			superToolTip19.MaxWidth = 210;
			printPreviewBarItem21.SuperTip = superToolTip19;
			printPreviewBarItem23.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem23.Caption = "Export To";
			printPreviewBarItem23.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportFile;
			printPreviewBarItem23.ContextSpecifier = printRibbonController1;
			printPreviewBarItem23.Enabled = false;
			printPreviewBarItem23.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem23.Glyph");
			printPreviewBarItem23.Id = 23;
			printPreviewBarItem23.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem23.LargeGlyph");
			printPreviewBarItem23.Name = "printPreviewBarItem23";
			superToolTip20.FixedTooltipWidth = true;
			toolTipTitleItem20.Text = "Export To...";
			toolTipItem20.LeftIndent = 6;
			toolTipItem20.Text = "Export the current document in one of the available formats, and save it to the file on a disk.";
			superToolTip20.Items.Add(toolTipTitleItem20);
			superToolTip20.Items.Add(toolTipItem20);
			superToolTip20.MaxWidth = 210;
			printPreviewBarItem23.SuperTip = superToolTip20;
			printPreviewBarItem24.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem24.Caption = "E-Mail As";
			printPreviewBarItem24.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendFile;
			printPreviewBarItem24.ContextSpecifier = printRibbonController1;
			printPreviewBarItem24.Enabled = false;
			printPreviewBarItem24.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem24.Glyph");
			printPreviewBarItem24.Id = 24;
			printPreviewBarItem24.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem24.LargeGlyph");
			printPreviewBarItem24.Name = "printPreviewBarItem24";
			superToolTip21.FixedTooltipWidth = true;
			toolTipTitleItem21.Text = "E-Mail As...";
			toolTipItem21.LeftIndent = 6;
			toolTipItem21.Text = "Export the current document in one of the available formats, and attach it to the e-mail.";
			superToolTip21.Items.Add(toolTipTitleItem21);
			superToolTip21.Items.Add(toolTipItem21);
			superToolTip21.MaxWidth = 210;
			printPreviewBarItem24.SuperTip = superToolTip21;
			printPreviewBarItem25.Caption = "Close Print Preview";
			printPreviewBarItem25.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview;
			printPreviewBarItem25.ContextSpecifier = printRibbonController1;
			printPreviewBarItem25.Enabled = false;
			printPreviewBarItem25.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem25.Glyph");
			printPreviewBarItem25.Id = 25;
			printPreviewBarItem25.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem25.LargeGlyph");
			printPreviewBarItem25.Name = "printPreviewBarItem25";
			superToolTip22.FixedTooltipWidth = true;
			toolTipTitleItem22.Text = "Close Print Preview";
			toolTipItem22.LeftIndent = 6;
			toolTipItem22.Text = "Close Print Preview of the document.";
			superToolTip22.Items.Add(toolTipTitleItem22);
			superToolTip22.Items.Add(toolTipItem22);
			superToolTip22.MaxWidth = 210;
			printPreviewBarItem25.SuperTip = superToolTip22;
			printPreviewBarItem26.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem26.Caption = "Orientation";
			printPreviewBarItem26.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageOrientation;
			printPreviewBarItem26.ContextSpecifier = printRibbonController1;
			printPreviewBarItem26.Enabled = false;
			printPreviewBarItem26.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem26.Glyph");
			printPreviewBarItem26.Id = 26;
			printPreviewBarItem26.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem26.LargeGlyph");
			printPreviewBarItem26.Name = "printPreviewBarItem26";
			superToolTip23.FixedTooltipWidth = true;
			toolTipTitleItem23.Text = "Page Orientation";
			toolTipItem23.LeftIndent = 6;
			toolTipItem23.Text = "Switch the pages between portrait and landscape layouts.";
			superToolTip23.Items.Add(toolTipTitleItem23);
			superToolTip23.Items.Add(toolTipItem23);
			superToolTip23.MaxWidth = 210;
			printPreviewBarItem26.SuperTip = superToolTip23;
			printPreviewBarItem27.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem27.Caption = "Size";
			printPreviewBarItem27.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PaperSize;
			printPreviewBarItem27.ContextSpecifier = printRibbonController1;
			printPreviewBarItem27.Enabled = false;
			printPreviewBarItem27.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem27.Glyph");
			printPreviewBarItem27.Id = 27;
			printPreviewBarItem27.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem27.LargeGlyph");
			printPreviewBarItem27.Name = "printPreviewBarItem27";
			superToolTip24.FixedTooltipWidth = true;
			toolTipTitleItem24.Text = "Page Size";
			toolTipItem24.LeftIndent = 6;
			toolTipItem24.Text = "Choose the paper size of the document.";
			superToolTip24.Items.Add(toolTipTitleItem24);
			superToolTip24.Items.Add(toolTipItem24);
			superToolTip24.MaxWidth = 210;
			printPreviewBarItem27.SuperTip = superToolTip24;
			printPreviewBarItem28.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem28.Caption = "Margins";
			printPreviewBarItem28.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageMargins;
			printPreviewBarItem28.ContextSpecifier = printRibbonController1;
			printPreviewBarItem28.Enabled = false;
			printPreviewBarItem28.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem28.Glyph");
			printPreviewBarItem28.Id = 28;
			printPreviewBarItem28.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem28.LargeGlyph");
			printPreviewBarItem28.Name = "printPreviewBarItem28";
			superToolTip25.FixedTooltipWidth = true;
			toolTipTitleItem25.Text = "Page Margins";
			toolTipItem25.LeftIndent = 6;
			toolTipItem25.Text = "Select the margin sizes for the entire document.\r\n\r\nTo apply specific margin sizes to the document, click Custom Margins.";
			superToolTip25.Items.Add(toolTipTitleItem25);
			superToolTip25.Items.Add(toolTipItem25);
			superToolTip25.MaxWidth = 210;
			printPreviewBarItem28.SuperTip = superToolTip25;
			printPreviewBarItem29.Caption = "PDF File";
			printPreviewBarItem29.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendPdf;
			printPreviewBarItem29.ContextSpecifier = printRibbonController1;
			printPreviewBarItem29.Description = "Adobe Portable Document Format";
			printPreviewBarItem29.Enabled = false;
			printPreviewBarItem29.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem29.Glyph");
			printPreviewBarItem29.Id = 29;
			printPreviewBarItem29.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem29.LargeGlyph");
			printPreviewBarItem29.Name = "printPreviewBarItem29";
			superToolTip26.FixedTooltipWidth = true;
			toolTipTitleItem26.Text = "E-Mail As PDF";
			toolTipItem26.LeftIndent = 6;
			toolTipItem26.Text = "Export the document to PDF and attach it to the e-mail.";
			superToolTip26.Items.Add(toolTipTitleItem26);
			superToolTip26.Items.Add(toolTipItem26);
			superToolTip26.MaxWidth = 210;
			printPreviewBarItem29.SuperTip = superToolTip26;
			printPreviewBarItem30.Caption = "Text File";
			printPreviewBarItem30.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendTxt;
			printPreviewBarItem30.ContextSpecifier = printRibbonController1;
			printPreviewBarItem30.Description = "Plain Text";
			printPreviewBarItem30.Enabled = false;
			printPreviewBarItem30.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem30.Glyph");
			printPreviewBarItem30.Id = 30;
			printPreviewBarItem30.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem30.LargeGlyph");
			printPreviewBarItem30.Name = "printPreviewBarItem30";
			superToolTip27.FixedTooltipWidth = true;
			toolTipTitleItem27.Text = "E-Mail As Text";
			toolTipItem27.LeftIndent = 6;
			toolTipItem27.Text = "Export the document to Text and attach it to the e-mail.";
			superToolTip27.Items.Add(toolTipTitleItem27);
			superToolTip27.Items.Add(toolTipItem27);
			superToolTip27.MaxWidth = 210;
			printPreviewBarItem30.SuperTip = superToolTip27;
			printPreviewBarItem31.Caption = "CSV File";
			printPreviewBarItem31.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendCsv;
			printPreviewBarItem31.ContextSpecifier = printRibbonController1;
			printPreviewBarItem31.Description = "Comma-Separated Values Text";
			printPreviewBarItem31.Enabled = false;
			printPreviewBarItem31.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem31.Glyph");
			printPreviewBarItem31.Id = 31;
			printPreviewBarItem31.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem31.LargeGlyph");
			printPreviewBarItem31.Name = "printPreviewBarItem31";
			superToolTip28.FixedTooltipWidth = true;
			toolTipTitleItem28.Text = "E-Mail As CSV";
			toolTipItem28.LeftIndent = 6;
			toolTipItem28.Text = "Export the document to CSV and attach it to the e-mail.";
			superToolTip28.Items.Add(toolTipTitleItem28);
			superToolTip28.Items.Add(toolTipItem28);
			superToolTip28.MaxWidth = 210;
			printPreviewBarItem31.SuperTip = superToolTip28;
			printPreviewBarItem32.Caption = "MHT File";
			printPreviewBarItem32.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendMht;
			printPreviewBarItem32.ContextSpecifier = printRibbonController1;
			printPreviewBarItem32.Description = "Single File Web Page";
			printPreviewBarItem32.Enabled = false;
			printPreviewBarItem32.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem32.Glyph");
			printPreviewBarItem32.Id = 32;
			printPreviewBarItem32.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem32.LargeGlyph");
			printPreviewBarItem32.Name = "printPreviewBarItem32";
			superToolTip29.FixedTooltipWidth = true;
			toolTipTitleItem29.Text = "E-Mail As MHT";
			toolTipItem29.LeftIndent = 6;
			toolTipItem29.Text = "Export the document to MHT and attach it to the e-mail.";
			superToolTip29.Items.Add(toolTipTitleItem29);
			superToolTip29.Items.Add(toolTipItem29);
			superToolTip29.MaxWidth = 210;
			printPreviewBarItem32.SuperTip = superToolTip29;
			printPreviewBarItem33.Caption = "XLS File";
			printPreviewBarItem33.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXls;
			printPreviewBarItem33.ContextSpecifier = printRibbonController1;
			printPreviewBarItem33.Description = "Microsoft Excel 2000-2003 Workbook";
			printPreviewBarItem33.Enabled = false;
			printPreviewBarItem33.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem33.Glyph");
			printPreviewBarItem33.Id = 33;
			printPreviewBarItem33.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem33.LargeGlyph");
			printPreviewBarItem33.Name = "printPreviewBarItem33";
			superToolTip30.FixedTooltipWidth = true;
			toolTipTitleItem30.Text = "E-Mail As XLS";
			toolTipItem30.LeftIndent = 6;
			toolTipItem30.Text = "Export the document to XLS and attach it to the e-mail.";
			superToolTip30.Items.Add(toolTipTitleItem30);
			superToolTip30.Items.Add(toolTipItem30);
			superToolTip30.MaxWidth = 210;
			printPreviewBarItem33.SuperTip = superToolTip30;
			printPreviewBarItem34.Caption = "XLSX File";
			printPreviewBarItem34.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXlsx;
			printPreviewBarItem34.ContextSpecifier = printRibbonController1;
			printPreviewBarItem34.Description = "Microsoft Excel 2007 Workbook";
			printPreviewBarItem34.Enabled = false;
			printPreviewBarItem34.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem34.Glyph");
			printPreviewBarItem34.Id = 34;
			printPreviewBarItem34.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem34.LargeGlyph");
			printPreviewBarItem34.Name = "printPreviewBarItem34";
			superToolTip31.FixedTooltipWidth = true;
			toolTipTitleItem31.Text = "E-Mail As XLSX";
			toolTipItem31.LeftIndent = 6;
			toolTipItem31.Text = "Export the document to XLSX and attach it to the e-mail.";
			superToolTip31.Items.Add(toolTipTitleItem31);
			superToolTip31.Items.Add(toolTipItem31);
			superToolTip31.MaxWidth = 210;
			printPreviewBarItem34.SuperTip = superToolTip31;
			printPreviewBarItem35.Caption = "RTF File";
			printPreviewBarItem35.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendRtf;
			printPreviewBarItem35.ContextSpecifier = printRibbonController1;
			printPreviewBarItem35.Description = "Rich Text Format";
			printPreviewBarItem35.Enabled = false;
			printPreviewBarItem35.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem35.Glyph");
			printPreviewBarItem35.Id = 35;
			printPreviewBarItem35.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem35.LargeGlyph");
			printPreviewBarItem35.Name = "printPreviewBarItem35";
			superToolTip32.FixedTooltipWidth = true;
			toolTipTitleItem32.Text = "E-Mail As RTF";
			toolTipItem32.LeftIndent = 6;
			toolTipItem32.Text = "Export the document to RTF and attach it to the e-mail.";
			superToolTip32.Items.Add(toolTipTitleItem32);
			superToolTip32.Items.Add(toolTipItem32);
			superToolTip32.MaxWidth = 210;
			printPreviewBarItem35.SuperTip = superToolTip32;
			printPreviewBarItem36.Caption = "Image File";
			printPreviewBarItem36.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendGraphic;
			printPreviewBarItem36.ContextSpecifier = printRibbonController1;
			printPreviewBarItem36.Description = "BMP, GIF, JPEG, PNG, TIFF, EMF, WMF";
			printPreviewBarItem36.Enabled = false;
			printPreviewBarItem36.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem36.Glyph");
			printPreviewBarItem36.Id = 36;
			printPreviewBarItem36.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem36.LargeGlyph");
			printPreviewBarItem36.Name = "printPreviewBarItem36";
			superToolTip33.FixedTooltipWidth = true;
			toolTipTitleItem33.Text = "E-Mail As Image";
			toolTipItem33.LeftIndent = 6;
			toolTipItem33.Text = "Export the document to Image and attach it to the e-mail.";
			superToolTip33.Items.Add(toolTipTitleItem33);
			superToolTip33.Items.Add(toolTipItem33);
			superToolTip33.MaxWidth = 210;
			printPreviewBarItem36.SuperTip = superToolTip33;
			printPreviewBarItem37.Caption = "PDF File";
			printPreviewBarItem37.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf;
			printPreviewBarItem37.ContextSpecifier = printRibbonController1;
			printPreviewBarItem37.Description = "Adobe Portable Document Format";
			printPreviewBarItem37.Enabled = false;
			printPreviewBarItem37.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem37.Glyph");
			printPreviewBarItem37.Id = 37;
			printPreviewBarItem37.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem37.LargeGlyph");
			printPreviewBarItem37.Name = "printPreviewBarItem37";
			superToolTip34.FixedTooltipWidth = true;
			toolTipTitleItem34.Text = "Export to PDF";
			toolTipItem34.LeftIndent = 6;
			toolTipItem34.Text = "Export the document to PDF and save it to the file on a disk.";
			superToolTip34.Items.Add(toolTipTitleItem34);
			superToolTip34.Items.Add(toolTipItem34);
			superToolTip34.MaxWidth = 210;
			printPreviewBarItem37.SuperTip = superToolTip34;
			printPreviewBarItem38.Caption = "HTML File";
			printPreviewBarItem38.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportHtm;
			printPreviewBarItem38.ContextSpecifier = printRibbonController1;
			printPreviewBarItem38.Description = "Web Page";
			printPreviewBarItem38.Enabled = false;
			printPreviewBarItem38.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem38.Glyph");
			printPreviewBarItem38.Id = 38;
			printPreviewBarItem38.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem38.LargeGlyph");
			printPreviewBarItem38.Name = "printPreviewBarItem38";
			superToolTip35.FixedTooltipWidth = true;
			toolTipTitleItem35.Text = "Export to HTML";
			toolTipItem35.LeftIndent = 6;
			toolTipItem35.Text = "Export the document to HTML and save it to the file on a disk.";
			superToolTip35.Items.Add(toolTipTitleItem35);
			superToolTip35.Items.Add(toolTipItem35);
			superToolTip35.MaxWidth = 210;
			printPreviewBarItem38.SuperTip = superToolTip35;
			printPreviewBarItem39.Caption = "Text File";
			printPreviewBarItem39.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportTxt;
			printPreviewBarItem39.ContextSpecifier = printRibbonController1;
			printPreviewBarItem39.Description = "Plain Text";
			printPreviewBarItem39.Enabled = false;
			printPreviewBarItem39.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem39.Glyph");
			printPreviewBarItem39.Id = 39;
			printPreviewBarItem39.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem39.LargeGlyph");
			printPreviewBarItem39.Name = "printPreviewBarItem39";
			superToolTip36.FixedTooltipWidth = true;
			toolTipTitleItem36.Text = "Export to Text";
			toolTipItem36.LeftIndent = 6;
			toolTipItem36.Text = "Export the document to Text and save it to the file on a disk.";
			superToolTip36.Items.Add(toolTipTitleItem36);
			superToolTip36.Items.Add(toolTipItem36);
			superToolTip36.MaxWidth = 210;
			printPreviewBarItem39.SuperTip = superToolTip36;
			printPreviewBarItem40.Caption = "CSV File";
			printPreviewBarItem40.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportCsv;
			printPreviewBarItem40.ContextSpecifier = printRibbonController1;
			printPreviewBarItem40.Description = "Comma-Separated Values Text";
			printPreviewBarItem40.Enabled = false;
			printPreviewBarItem40.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem40.Glyph");
			printPreviewBarItem40.Id = 40;
			printPreviewBarItem40.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem40.LargeGlyph");
			printPreviewBarItem40.Name = "printPreviewBarItem40";
			superToolTip37.FixedTooltipWidth = true;
			toolTipTitleItem37.Text = "Export to CSV";
			toolTipItem37.LeftIndent = 6;
			toolTipItem37.Text = "Export the document to CSV and save it to the file on a disk.";
			superToolTip37.Items.Add(toolTipTitleItem37);
			superToolTip37.Items.Add(toolTipItem37);
			superToolTip37.MaxWidth = 210;
			printPreviewBarItem40.SuperTip = superToolTip37;
			printPreviewBarItem41.Caption = "MHT File";
			printPreviewBarItem41.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportMht;
			printPreviewBarItem41.ContextSpecifier = printRibbonController1;
			printPreviewBarItem41.Description = "Single File Web Page";
			printPreviewBarItem41.Enabled = false;
			printPreviewBarItem41.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem41.Glyph");
			printPreviewBarItem41.Id = 41;
			printPreviewBarItem41.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem41.LargeGlyph");
			printPreviewBarItem41.Name = "printPreviewBarItem41";
			superToolTip38.FixedTooltipWidth = true;
			toolTipTitleItem38.Text = "Export to MHT";
			toolTipItem38.LeftIndent = 6;
			toolTipItem38.Text = "Export the document to MHT and save it to the file on a disk.";
			superToolTip38.Items.Add(toolTipTitleItem38);
			superToolTip38.Items.Add(toolTipItem38);
			superToolTip38.MaxWidth = 210;
			printPreviewBarItem41.SuperTip = superToolTip38;
			printPreviewBarItem42.Caption = "XLS File";
			printPreviewBarItem42.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXls;
			printPreviewBarItem42.ContextSpecifier = printRibbonController1;
			printPreviewBarItem42.Description = "Microsoft Excel 2000-2003 Workbook";
			printPreviewBarItem42.Enabled = false;
			printPreviewBarItem42.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem42.Glyph");
			printPreviewBarItem42.Id = 42;
			printPreviewBarItem42.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem42.LargeGlyph");
			printPreviewBarItem42.Name = "printPreviewBarItem42";
			superToolTip39.FixedTooltipWidth = true;
			toolTipTitleItem39.Text = "Export to XLS";
			toolTipItem39.LeftIndent = 6;
			toolTipItem39.Text = "Export the document to XLS and save it to the file on a disk.";
			superToolTip39.Items.Add(toolTipTitleItem39);
			superToolTip39.Items.Add(toolTipItem39);
			superToolTip39.MaxWidth = 210;
			printPreviewBarItem42.SuperTip = superToolTip39;
			printPreviewBarItem43.Caption = "XLSX File";
			printPreviewBarItem43.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXlsx;
			printPreviewBarItem43.ContextSpecifier = printRibbonController1;
			printPreviewBarItem43.Description = "Microsoft Excel 2007 Workbook";
			printPreviewBarItem43.Enabled = false;
			printPreviewBarItem43.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem43.Glyph");
			printPreviewBarItem43.Id = 43;
			printPreviewBarItem43.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem43.LargeGlyph");
			printPreviewBarItem43.Name = "printPreviewBarItem43";
			superToolTip40.FixedTooltipWidth = true;
			toolTipTitleItem40.Text = "Export to XLSX";
			toolTipItem40.LeftIndent = 6;
			toolTipItem40.Text = "Export the document to XLSX and save it to the file on a disk.";
			superToolTip40.Items.Add(toolTipTitleItem40);
			superToolTip40.Items.Add(toolTipItem40);
			superToolTip40.MaxWidth = 210;
			printPreviewBarItem43.SuperTip = superToolTip40;
			printPreviewBarItem44.Caption = "RTF File";
			printPreviewBarItem44.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportRtf;
			printPreviewBarItem44.ContextSpecifier = printRibbonController1;
			printPreviewBarItem44.Description = "Rich Text Format";
			printPreviewBarItem44.Enabled = false;
			printPreviewBarItem44.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem44.Glyph");
			printPreviewBarItem44.Id = 44;
			printPreviewBarItem44.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem44.LargeGlyph");
			printPreviewBarItem44.Name = "printPreviewBarItem44";
			superToolTip41.FixedTooltipWidth = true;
			toolTipTitleItem41.Text = "Export to RTF";
			toolTipItem41.LeftIndent = 6;
			toolTipItem41.Text = "Export the document to RTF and save it to the file on a disk.";
			superToolTip41.Items.Add(toolTipTitleItem41);
			superToolTip41.Items.Add(toolTipItem41);
			superToolTip41.MaxWidth = 210;
			printPreviewBarItem44.SuperTip = superToolTip41;
			printPreviewBarItem45.Caption = "Image File";
			printPreviewBarItem45.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportGraphic;
			printPreviewBarItem45.ContextSpecifier = printRibbonController1;
			printPreviewBarItem45.Description = "BMP, GIF, JPEG, PNG, TIFF, EMF, WMF";
			printPreviewBarItem45.Enabled = false;
			printPreviewBarItem45.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem45.Glyph");
			printPreviewBarItem45.Id = 45;
			printPreviewBarItem45.LargeGlyph = (System.Drawing.Image)resources.GetObject("printPreviewBarItem45.LargeGlyph");
			printPreviewBarItem45.Name = "printPreviewBarItem45";
			superToolTip42.FixedTooltipWidth = true;
			toolTipTitleItem42.Text = "Export to Image";
			toolTipItem42.LeftIndent = 6;
			toolTipItem42.Text = "Export the document to Image and save it to the file on a disk.";
			superToolTip42.Items.Add(toolTipTitleItem42);
			superToolTip42.Items.Add(toolTipItem42);
			superToolTip42.MaxWidth = 210;
			printPreviewBarItem45.SuperTip = superToolTip42;
			printPreviewStaticItem1.Caption = "Nothing";
			printPreviewStaticItem1.Id = 48;
			printPreviewStaticItem1.LeftIndent = 1;
			printPreviewStaticItem1.Name = "printPreviewStaticItem1";
			printPreviewStaticItem1.RightIndent = 1;
			printPreviewStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
			printPreviewStaticItem1.Type = "PageOfPages";
			barStaticItem1.Id = 49;
			barStaticItem1.Name = "barStaticItem1";
			barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
			barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime;
			progressBarEditItem1.ContextSpecifier = printRibbonController1;
			progressBarEditItem1.Edit = repositoryItemProgressBar1;
			progressBarEditItem1.EditHeight = 12;
			progressBarEditItem1.Id = 50;
			progressBarEditItem1.Name = "progressBarEditItem1";
			progressBarEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			progressBarEditItem1.Width = 150;
			repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
			repositoryItemProgressBar1.UseParentBackground = true;
			printPreviewBarItem48.Caption = "Stop";
			printPreviewBarItem48.Command = DevExpress.XtraPrinting.PrintingSystemCommand.StopPageBuilding;
			printPreviewBarItem48.ContextSpecifier = printRibbonController1;
			printPreviewBarItem48.Enabled = false;
			printPreviewBarItem48.Hint = "Stop";
			printPreviewBarItem48.Id = 51;
			printPreviewBarItem48.Name = "printPreviewBarItem48";
			printPreviewBarItem48.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
			barButtonItem1.Enabled = false;
			barButtonItem1.Id = 52;
			barButtonItem1.Name = "barButtonItem1";
			barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime;
			printPreviewStaticItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			printPreviewStaticItem2.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
			printPreviewStaticItem2.Caption = "100%";
			printPreviewStaticItem2.Id = 53;
			printPreviewStaticItem2.Name = "printPreviewStaticItem2";
			printPreviewStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
			printPreviewStaticItem2.Type = "ZoomFactorText";
			printPreviewStaticItem2.Width = 42;
			zoomTrackBarEditItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			zoomTrackBarEditItem1.ContextSpecifier = printRibbonController1;
			zoomTrackBarEditItem1.Edit = repositoryItemZoomTrackBar1;
			zoomTrackBarEditItem1.EditValue = 90;
			zoomTrackBarEditItem1.Enabled = false;
			zoomTrackBarEditItem1.Id = 54;
			zoomTrackBarEditItem1.Name = "zoomTrackBarEditItem1";
			zoomTrackBarEditItem1.Range = new int[2]
			{
				10,
				500
			};
			zoomTrackBarEditItem1.Width = 140;
			repositoryItemZoomTrackBar1.Alignment = DevExpress.Utils.VertAlignment.Center;
			repositoryItemZoomTrackBar1.AllowFocused = false;
			repositoryItemZoomTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			repositoryItemZoomTrackBar1.Maximum = 180;
			repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
			repositoryItemZoomTrackBar1.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
			repositoryItemZoomTrackBar1.UseParentBackground = true;
			printPreviewRibbonPage1.ContextSpecifier = printRibbonController1;
			printPreviewRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[4]
			{
				printPreviewRibbonPageGroup2,
				printPreviewRibbonPageGroup3,
				printPreviewRibbonPageGroup4,
				printPreviewRibbonPageGroup5
			});
			printPreviewRibbonPage1.Name = "printPreviewRibbonPage1";
			printPreviewRibbonPage1.Text = "Print Preview";
			printPreviewRibbonPageGroup2.ContextSpecifier = printRibbonController1;
			printPreviewRibbonPageGroup2.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewRibbonPageGroup2.Glyph");
			printPreviewRibbonPageGroup2.ItemLinks.Add(printPreviewBarItem5);
			printPreviewRibbonPageGroup2.ItemLinks.Add(printPreviewBarItem6);
			printPreviewRibbonPageGroup2.ItemLinks.Add(printPreviewBarItem4);
			printPreviewRibbonPageGroup2.ItemLinks.Add(printPreviewBarItem2);
			printPreviewRibbonPageGroup2.Kind = DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroupKind.Print;
			printPreviewRibbonPageGroup2.Name = "printPreviewRibbonPageGroup2";
			printPreviewRibbonPageGroup2.ShowCaptionButton = false;
			printPreviewRibbonPageGroup2.Text = "Print";
			printPreviewRibbonPageGroup3.ContextSpecifier = printRibbonController1;
			printPreviewRibbonPageGroup3.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewRibbonPageGroup3.Glyph");
			printPreviewRibbonPageGroup3.ItemLinks.Add(printPreviewBarItem9);
			printPreviewRibbonPageGroup3.ItemLinks.Add(printPreviewBarItem28);
			printPreviewRibbonPageGroup3.ItemLinks.Add(printPreviewBarItem26);
			printPreviewRibbonPageGroup3.ItemLinks.Add(printPreviewBarItem27);
			printPreviewRibbonPageGroup3.Kind = DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroupKind.PageSetup;
			printPreviewRibbonPageGroup3.Name = "printPreviewRibbonPageGroup3";
			superToolTip43.FixedTooltipWidth = true;
			toolTipTitleItem43.Text = "Page Setup";
			toolTipItem43.Appearance.Image = (System.Drawing.Image)resources.GetObject("resource.Image");
			toolTipItem43.Appearance.Options.UseImage = true;
			toolTipItem43.Image = (System.Drawing.Image)resources.GetObject("toolTipItem43.Image");
			toolTipItem43.LeftIndent = 6;
			toolTipItem43.Text = "Show the Page Setup dialog.";
			superToolTip43.Items.Add(toolTipTitleItem43);
			superToolTip43.Items.Add(toolTipItem43);
			superToolTip43.MaxWidth = 318;
			printPreviewRibbonPageGroup3.SuperTip = superToolTip43;
			printPreviewRibbonPageGroup3.Text = "Page Setup";
			printPreviewRibbonPageGroup4.ContextSpecifier = printRibbonController1;
			printPreviewRibbonPageGroup4.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewRibbonPageGroup4.Glyph");
			printPreviewRibbonPageGroup4.ItemLinks.Add(printPreviewBarItem3);
			printPreviewRibbonPageGroup4.ItemLinks.Add(printPreviewBarItem16, true);
			printPreviewRibbonPageGroup4.ItemLinks.Add(printPreviewBarItem17);
			printPreviewRibbonPageGroup4.ItemLinks.Add(printPreviewBarItem18);
			printPreviewRibbonPageGroup4.ItemLinks.Add(printPreviewBarItem19);
			printPreviewRibbonPageGroup4.Kind = DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroupKind.Navigation;
			printPreviewRibbonPageGroup4.Name = "printPreviewRibbonPageGroup4";
			printPreviewRibbonPageGroup4.ShowCaptionButton = false;
			printPreviewRibbonPageGroup4.Text = "Navigation";
			printPreviewRibbonPageGroup5.ContextSpecifier = printRibbonController1;
			printPreviewRibbonPageGroup5.Glyph = (System.Drawing.Image)resources.GetObject("printPreviewRibbonPageGroup5.Glyph");
			printPreviewRibbonPageGroup5.ItemLinks.Add(printPreviewBarItem10);
			printPreviewRibbonPageGroup5.ItemLinks.Add(printPreviewBarItem11);
			printPreviewRibbonPageGroup5.ItemLinks.Add(printPreviewBarItem12);
			printPreviewRibbonPageGroup5.ItemLinks.Add(printPreviewBarItem20);
			printPreviewRibbonPageGroup5.ItemLinks.Add(printPreviewBarItem13);
			printPreviewRibbonPageGroup5.ItemLinks.Add(printPreviewBarItem15);
			printPreviewRibbonPageGroup5.ItemLinks.Add(printPreviewBarItem14);
			printPreviewRibbonPageGroup5.Kind = DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroupKind.Zoom;
			printPreviewRibbonPageGroup5.Name = "printPreviewRibbonPageGroup5";
			printPreviewRibbonPageGroup5.ShowCaptionButton = false;
			printPreviewRibbonPageGroup5.Text = "Zoom";
			ribbonStatusBar1.ItemLinks.Add(printPreviewStaticItem1);
			ribbonStatusBar1.ItemLinks.Add(barStaticItem1, true);
			ribbonStatusBar1.ItemLinks.Add(progressBarEditItem1);
			ribbonStatusBar1.ItemLinks.Add(printPreviewBarItem48);
			ribbonStatusBar1.ItemLinks.Add(barButtonItem1);
			ribbonStatusBar1.ItemLinks.Add(printPreviewStaticItem2);
			ribbonStatusBar1.ItemLinks.Add(zoomTrackBarEditItem1);
			ribbonStatusBar1.Location = new System.Drawing.Point(0, 594);
			ribbonStatusBar1.Name = "ribbonStatusBar1";
			ribbonStatusBar1.Ribbon = ribbonControl1;
			ribbonStatusBar1.Size = new System.Drawing.Size(1091, 27);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1091, 621);
			base.Controls.Add(ribbonStatusBar1);
			base.Controls.Add(printControl1);
			base.Controls.Add(ribbonControl1);
			base.Name = "DocumentViewForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			Text = "Document Preview";
			((System.ComponentModel.ISupportInitialize)printRibbonController1).EndInit();
			((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemZoomTrackBar1).EndInit();
			ResumeLayout(false);
		}
	}
}
