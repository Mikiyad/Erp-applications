using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Printing
{
	public class PrintPreviewForm : Form
	{
		public class SaveCommandHandler : DevExpress.XtraReports.UserDesigner.ICommandHandler
		{
			private XRDesignPanel panel;

			public SaveCommandHandler(XRDesignPanel panel)
			{
				this.panel = panel;
			}

			public void HandleCommand(ReportCommand command, object[] args)
			{
				Save();
			}

			public bool CanHandleCommand(ReportCommand command, ref bool useNextHandler)
			{
				useNextHandler = (command != ReportCommand.SaveFile && command != ReportCommand.SaveFileAs);
				return !useNextHandler;
			}

			private void Save()
			{
				panel.Report.SaveLayout(panel.Report.Tag.ToString());
				panel.ReportState = ReportState.Saved;
			}
		}

		private XtraReport report;

		public PrintTypes PrintType;

		private PrintControl printControl;

		private PrintBarManager printBarManager1;

		private PreviewBar previewBar1;

		private PrintPreviewBarItem printPreviewBarItem2;

		private PrintPreviewBarItem printPreviewBarItem3;

		private PrintPreviewBarItem printPreviewBarItem4;

		private PrintPreviewBarItem printPreviewBarItem5;

		private PrintPreviewBarItem printPreviewBarItem6;

		private PrintPreviewBarItem printPreviewBarItem7;

		private PrintPreviewBarItem printPreviewBarItem8;

		private PrintPreviewBarItem printPreviewBarItem9;

		private PrintPreviewBarItem printPreviewBarItem10;

		private PrintPreviewBarItem printPreviewBarItem11;

		private PrintPreviewBarItem printPreviewBarItem12;

		private PrintPreviewBarItem printPreviewBarItem13;

		private PrintPreviewBarItem printPreviewBarItem14;

		private PrintPreviewBarItem printPreviewBarItem15;

		private ZoomBarEditItem zoomBarEditItem1;

		private PrintPreviewRepositoryItemComboBox printPreviewRepositoryItemComboBox1;

		private PrintPreviewBarItem printPreviewBarItem16;

		private PrintPreviewBarItem printPreviewBarItem17;

		private PrintPreviewBarItem printPreviewBarItem18;

		private PrintPreviewBarItem printPreviewBarItem19;

		private PrintPreviewBarItem printPreviewBarItem20;

		private PrintPreviewBarItem printPreviewBarItem21;

		private PrintPreviewBarItem printPreviewBarItem22;

		private PrintPreviewBarItem printPreviewBarItem23;

		private PrintPreviewBarItem printPreviewBarItemExport;

		private PrintPreviewBarItem printPreviewBarItemEmail;

		private PrintPreviewBarItem printPreviewBarItem26;

		private PreviewBar previewBar2;

		private PrintPreviewStaticItem printPreviewStaticItem1;

		private BarStaticItem barStaticItem1;

		private ProgressBarEditItem progressBarEditItem1;

		private RepositoryItemProgressBar repositoryItemProgressBar1;

		private PrintPreviewBarItem printPreviewBarItem1;

		private BarButtonItem barButtonItem1;

		private PrintPreviewStaticItem printPreviewStaticItem2;

		private PreviewBar previewBar3;

		private PrintPreviewSubItem printPreviewSubItem1;

		private PrintPreviewSubItem printPreviewSubItem2;

		private PrintPreviewSubItem printPreviewSubItem4;

		private PrintPreviewBarItem printPreviewBarItem27;

		private PrintPreviewBarItem printPreviewBarItem28;

		private BarToolbarsListItem barToolbarsListItem1;

		private PrintPreviewSubItem printPreviewSubItem3;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private PrintPreviewBarCheckItem barButtonExportPDF;

		private PrintPreviewBarCheckItem barButtonExportHTML;

		private PrintPreviewBarCheckItem barButtonExportMHT;

		private PrintPreviewBarCheckItem barButtonExportRTF;

		private PrintPreviewBarCheckItem barButtonExportXLS;

		private PrintPreviewBarCheckItem barButtonExportXLSX;

		private PrintPreviewBarCheckItem barButtonExportCSV;

		private PrintPreviewBarCheckItem barButtonExportText;

		private PrintPreviewBarCheckItem barButtonExportImage;

		private PrintPreviewBarCheckItem barButtonExportPDF0;

		private PrintPreviewBarCheckItem barButtonExportPDF1;

		private PrintPreviewBarCheckItem barButtonExportPDF2;

		private PrintPreviewBarCheckItem barButtonExportPDF3;

		private PrintPreviewBarCheckItem barButtonExportPDF4;

		private PrintPreviewBarCheckItem barButtonExportPDF5;

		private PrintPreviewBarCheckItem barButtonExportPDF6;

		private PrintPreviewBarCheckItem barButtonExportPDF7;

		private BarSubItem barSubItem1;

		private BarSubItem barSubItem2;

		private BarSubItem barSubItem3;

		private BarButtonItem barButtonItem2;

		private BarButtonItem barButtonItemDesign;

		private IContainer components;

		private XRDesignMdiController mdiController;

		public XtraReport Document
		{
			get
			{
				return report;
			}
			set
			{
				report = value;
			}
		}

		public PrintPreviewForm()
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
			DevExpress.Utils.SuperToolTip superToolTip = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipTitleItem toolTipTitleItem = new DevExpress.Utils.ToolTipTitleItem();
			DevExpress.Utils.ToolTipItem toolTipItem = new DevExpress.Utils.ToolTipItem();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Printing.PrintPreviewForm));
			printControl = new DevExpress.XtraPrinting.Control.PrintControl();
			printBarManager1 = new DevExpress.XtraPrinting.Preview.PrintBarManager(components);
			previewBar1 = new DevExpress.XtraPrinting.Preview.PreviewBar();
			printPreviewBarItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem4 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem5 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem6 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem7 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem8 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem9 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem10 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem11 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem12 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem13 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem14 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem15 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			zoomBarEditItem1 = new DevExpress.XtraPrinting.Preview.ZoomBarEditItem();
			printPreviewRepositoryItemComboBox1 = new DevExpress.XtraPrinting.Preview.PrintPreviewRepositoryItemComboBox();
			printPreviewBarItem16 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem17 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem18 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem19 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem20 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem21 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem22 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem23 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			barButtonItemDesign = new DevExpress.XtraBars.BarButtonItem();
			printPreviewBarItemExport = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItemEmail = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem26 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			previewBar2 = new DevExpress.XtraPrinting.Preview.PreviewBar();
			printPreviewStaticItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
			barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
			progressBarEditItem1 = new DevExpress.XtraPrinting.Preview.ProgressBarEditItem();
			repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
			printPreviewBarItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			printPreviewStaticItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
			previewBar3 = new DevExpress.XtraPrinting.Preview.PreviewBar();
			printPreviewSubItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
			printPreviewSubItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
			printPreviewSubItem4 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
			printPreviewBarItem27 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			printPreviewBarItem28 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
			barToolbarsListItem1 = new DevExpress.XtraBars.BarToolbarsListItem();
			printPreviewSubItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewSubItem();
			barSubItem3 = new DevExpress.XtraBars.BarSubItem();
			barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			barButtonExportPDF = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportHTML = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportMHT = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportRTF = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportXLS = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportXLSX = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportCSV = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportText = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportImage = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportPDF0 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportPDF1 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportPDF2 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportPDF3 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportPDF4 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportPDF5 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportPDF6 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barButtonExportPDF7 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarCheckItem();
			barSubItem1 = new DevExpress.XtraBars.BarSubItem();
			barSubItem2 = new DevExpress.XtraBars.BarSubItem();
			((System.ComponentModel.ISupportInitialize)printBarManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)printPreviewRepositoryItemComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar1).BeginInit();
			SuspendLayout();
			printControl.BackColor = System.Drawing.Color.Empty;
			printControl.Dock = System.Windows.Forms.DockStyle.Fill;
			printControl.ForeColor = System.Drawing.Color.Empty;
			printControl.IsMetric = false;
			printControl.Location = new System.Drawing.Point(0, 53);
			printControl.Name = "printControl";
			printControl.Size = new System.Drawing.Size(820, 502);
			printControl.TabIndex = 0;
			printControl.TooltipFont = new System.Drawing.Font("Tahoma", 8.25f);
			printBarManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[3]
			{
				previewBar1,
				previewBar2,
				previewBar3
			});
			printBarManager1.DockControls.Add(barDockControlTop);
			printBarManager1.DockControls.Add(barDockControlBottom);
			printBarManager1.DockControls.Add(barDockControlLeft);
			printBarManager1.DockControls.Add(barDockControlRight);
			printBarManager1.Form = this;
			printBarManager1.ImageStream = (DevExpress.Utils.ImageCollectionStreamer)resources.GetObject("printBarManager1.ImageStream");
			printBarManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[61]
			{
				printPreviewStaticItem1,
				barStaticItem1,
				progressBarEditItem1,
				printPreviewBarItem1,
				barButtonItem1,
				printPreviewStaticItem2,
				printPreviewBarItem2,
				printPreviewBarItem3,
				printPreviewBarItem4,
				printPreviewBarItem5,
				printPreviewBarItem6,
				printPreviewBarItem7,
				printPreviewBarItem8,
				printPreviewBarItem9,
				printPreviewBarItem10,
				printPreviewBarItem11,
				printPreviewBarItem12,
				printPreviewBarItem13,
				printPreviewBarItem14,
				printPreviewBarItem15,
				zoomBarEditItem1,
				printPreviewBarItem16,
				printPreviewBarItem17,
				printPreviewBarItem18,
				printPreviewBarItem19,
				printPreviewBarItem20,
				printPreviewBarItem21,
				printPreviewBarItem22,
				printPreviewBarItem23,
				printPreviewBarItemExport,
				printPreviewBarItemEmail,
				printPreviewBarItem26,
				printPreviewSubItem1,
				printPreviewSubItem2,
				printPreviewSubItem3,
				printPreviewSubItem4,
				printPreviewBarItem27,
				printPreviewBarItem28,
				barToolbarsListItem1,
				barButtonExportPDF,
				barButtonExportHTML,
				barButtonExportMHT,
				barButtonExportRTF,
				barButtonExportXLS,
				barButtonExportXLSX,
				barButtonExportCSV,
				barButtonExportText,
				barButtonExportImage,
				barButtonExportPDF0,
				barButtonExportPDF1,
				barButtonExportPDF2,
				barButtonExportPDF3,
				barButtonExportPDF4,
				barButtonExportPDF5,
				barButtonExportPDF6,
				barButtonExportPDF7,
				barSubItem1,
				barSubItem2,
				barSubItem3,
				barButtonItem2,
				barButtonItemDesign
			});
			printBarManager1.MainMenu = previewBar3;
			printBarManager1.MaxItemId = 62;
			printBarManager1.PreviewBar = previewBar1;
			printBarManager1.PrintControl = printControl;
			printBarManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[2]
			{
				repositoryItemProgressBar1,
				printPreviewRepositoryItemComboBox1
			});
			printBarManager1.StatusBar = previewBar2;
			previewBar1.BarName = "Toolbar";
			previewBar1.DockCol = 0;
			previewBar1.DockRow = 1;
			previewBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			previewBar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[27]
			{
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem2),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem3),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem4),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem5, true),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem6, true),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem7),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem8, true),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem9),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem10),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem11),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem12),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem13, true),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem14),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem15, true),
				new DevExpress.XtraBars.LinkPersistInfo(zoomBarEditItem1),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem16),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem17, true),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem18),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem19),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem20),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem21, true),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem22),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem23),
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItemDesign),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItemExport, true),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItemEmail),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem26, true)
			});
			previewBar1.Text = "Toolbar";
			printPreviewBarItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem2.Caption = "Document Map";
			printPreviewBarItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.DocumentMap;
			printPreviewBarItem2.Enabled = false;
			printPreviewBarItem2.Hint = "Document Map";
			printPreviewBarItem2.Id = 6;
			printPreviewBarItem2.ImageIndex = 19;
			printPreviewBarItem2.Name = "printPreviewBarItem2";
			printPreviewBarItem3.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem3.Caption = "Parameters";
			printPreviewBarItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Parameters;
			printPreviewBarItem3.Enabled = false;
			printPreviewBarItem3.Hint = "Parameters";
			printPreviewBarItem3.Id = 7;
			printPreviewBarItem3.ImageIndex = 22;
			printPreviewBarItem3.Name = "printPreviewBarItem3";
			printPreviewBarItem4.Caption = "Search";
			printPreviewBarItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find;
			printPreviewBarItem4.Enabled = false;
			printPreviewBarItem4.Hint = "Search";
			printPreviewBarItem4.Id = 8;
			printPreviewBarItem4.ImageIndex = 20;
			printPreviewBarItem4.Name = "printPreviewBarItem4";
			printPreviewBarItem5.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem5.Caption = "Customize";
			printPreviewBarItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize;
			printPreviewBarItem5.Enabled = false;
			printPreviewBarItem5.Hint = "Customize";
			printPreviewBarItem5.Id = 9;
			printPreviewBarItem5.ImageIndex = 14;
			printPreviewBarItem5.Name = "printPreviewBarItem5";
			printPreviewBarItem6.Caption = "Open";
			printPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Open;
			printPreviewBarItem6.Enabled = false;
			printPreviewBarItem6.Hint = "Open a document";
			printPreviewBarItem6.Id = 10;
			printPreviewBarItem6.ImageIndex = 23;
			printPreviewBarItem6.Name = "printPreviewBarItem6";
			printPreviewBarItem7.Caption = "Save";
			printPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Save;
			printPreviewBarItem7.Enabled = false;
			printPreviewBarItem7.Hint = "Save the document";
			printPreviewBarItem7.Id = 11;
			printPreviewBarItem7.ImageIndex = 24;
			printPreviewBarItem7.Name = "printPreviewBarItem7";
			printPreviewBarItem8.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem8.Caption = "&Print...";
			printPreviewBarItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print;
			printPreviewBarItem8.Enabled = false;
			printPreviewBarItem8.Hint = "Print";
			printPreviewBarItem8.Id = 12;
			printPreviewBarItem8.ImageIndex = 0;
			printPreviewBarItem8.Name = "printPreviewBarItem8";
			printPreviewBarItem9.Caption = "P&rint";
			printPreviewBarItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect;
			printPreviewBarItem9.Enabled = false;
			printPreviewBarItem9.Hint = "Quick Print";
			printPreviewBarItem9.Id = 13;
			printPreviewBarItem9.ImageIndex = 1;
			printPreviewBarItem9.Name = "printPreviewBarItem9";
			printPreviewBarItem10.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem10.Caption = "Page Set&up...";
			printPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup;
			printPreviewBarItem10.Enabled = false;
			printPreviewBarItem10.Hint = "Page Setup";
			printPreviewBarItem10.Id = 14;
			printPreviewBarItem10.ImageIndex = 2;
			printPreviewBarItem10.Name = "printPreviewBarItem10";
			printPreviewBarItem11.Caption = "Header And Footer";
			printPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.EditPageHF;
			printPreviewBarItem11.Enabled = false;
			printPreviewBarItem11.Hint = "Header And Footer";
			printPreviewBarItem11.Id = 15;
			printPreviewBarItem11.ImageIndex = 15;
			printPreviewBarItem11.Name = "printPreviewBarItem11";
			printPreviewBarItem12.ActAsDropDown = true;
			printPreviewBarItem12.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem12.Caption = "Scale";
			printPreviewBarItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Scale;
			printPreviewBarItem12.Enabled = false;
			printPreviewBarItem12.Hint = "Scale";
			printPreviewBarItem12.Id = 16;
			printPreviewBarItem12.ImageIndex = 25;
			printPreviewBarItem12.Name = "printPreviewBarItem12";
			printPreviewBarItem13.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem13.Caption = "Hand Tool";
			printPreviewBarItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.HandTool;
			printPreviewBarItem13.Enabled = false;
			printPreviewBarItem13.Hint = "Hand Tool";
			printPreviewBarItem13.Id = 17;
			printPreviewBarItem13.ImageIndex = 16;
			printPreviewBarItem13.Name = "printPreviewBarItem13";
			printPreviewBarItem14.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem14.Caption = "Magnifier";
			printPreviewBarItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Magnifier;
			printPreviewBarItem14.Enabled = false;
			printPreviewBarItem14.Hint = "Magnifier";
			printPreviewBarItem14.Id = 18;
			printPreviewBarItem14.ImageIndex = 3;
			printPreviewBarItem14.Name = "printPreviewBarItem14";
			printPreviewBarItem15.Caption = "Zoom Out";
			printPreviewBarItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut;
			printPreviewBarItem15.Enabled = false;
			printPreviewBarItem15.Hint = "Zoom Out";
			printPreviewBarItem15.Id = 19;
			printPreviewBarItem15.ImageIndex = 5;
			printPreviewBarItem15.Name = "printPreviewBarItem15";
			zoomBarEditItem1.Caption = "Zoom";
			zoomBarEditItem1.Edit = printPreviewRepositoryItemComboBox1;
			zoomBarEditItem1.EditValue = "100%";
			zoomBarEditItem1.Enabled = false;
			zoomBarEditItem1.Hint = "Zoom";
			zoomBarEditItem1.Id = 20;
			zoomBarEditItem1.Name = "zoomBarEditItem1";
			zoomBarEditItem1.Width = 70;
			printPreviewRepositoryItemComboBox1.AutoComplete = false;
			printPreviewRepositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			printPreviewRepositoryItemComboBox1.DropDownRows = 11;
			printPreviewRepositoryItemComboBox1.Name = "printPreviewRepositoryItemComboBox1";
			printPreviewBarItem16.Caption = "Zoom In";
			printPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn;
			printPreviewBarItem16.Enabled = false;
			printPreviewBarItem16.Hint = "Zoom In";
			printPreviewBarItem16.Id = 21;
			printPreviewBarItem16.ImageIndex = 4;
			printPreviewBarItem16.Name = "printPreviewBarItem16";
			printPreviewBarItem17.Caption = "First Page";
			printPreviewBarItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage;
			printPreviewBarItem17.Enabled = false;
			printPreviewBarItem17.Hint = "First Page";
			printPreviewBarItem17.Id = 22;
			printPreviewBarItem17.ImageIndex = 7;
			printPreviewBarItem17.Name = "printPreviewBarItem17";
			printPreviewBarItem18.Caption = "Previous Page";
			printPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage;
			printPreviewBarItem18.Enabled = false;
			printPreviewBarItem18.Hint = "Previous Page";
			printPreviewBarItem18.Id = 23;
			printPreviewBarItem18.ImageIndex = 8;
			printPreviewBarItem18.Name = "printPreviewBarItem18";
			printPreviewBarItem19.Caption = "Next Page";
			printPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage;
			printPreviewBarItem19.Enabled = false;
			printPreviewBarItem19.Hint = "Next Page";
			printPreviewBarItem19.Id = 24;
			printPreviewBarItem19.ImageIndex = 9;
			printPreviewBarItem19.Name = "printPreviewBarItem19";
			printPreviewBarItem20.Caption = "Last Page";
			printPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage;
			printPreviewBarItem20.Enabled = false;
			printPreviewBarItem20.Hint = "Last Page";
			printPreviewBarItem20.Id = 25;
			printPreviewBarItem20.ImageIndex = 10;
			printPreviewBarItem20.Name = "printPreviewBarItem20";
			printPreviewBarItem21.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem21.Caption = "Multiple Pages";
			printPreviewBarItem21.Command = DevExpress.XtraPrinting.PrintingSystemCommand.MultiplePages;
			printPreviewBarItem21.Enabled = false;
			printPreviewBarItem21.Hint = "Multiple Pages";
			printPreviewBarItem21.Id = 26;
			printPreviewBarItem21.ImageIndex = 11;
			printPreviewBarItem21.Name = "printPreviewBarItem21";
			printPreviewBarItem22.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItem22.Caption = "&Color...";
			printPreviewBarItem22.Command = DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground;
			printPreviewBarItem22.Enabled = false;
			printPreviewBarItem22.Hint = "Background";
			printPreviewBarItem22.Id = 27;
			printPreviewBarItem22.ImageIndex = 12;
			printPreviewBarItem22.Name = "printPreviewBarItem22";
			printPreviewBarItem23.Caption = "&Watermark...";
			printPreviewBarItem23.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Watermark;
			printPreviewBarItem23.Enabled = false;
			printPreviewBarItem23.Hint = "Watermark";
			printPreviewBarItem23.Id = 28;
			printPreviewBarItem23.ImageIndex = 21;
			printPreviewBarItem23.Name = "printPreviewBarItem23";
			barButtonItemDesign.Caption = "Design";
			barButtonItemDesign.Glyph = Micromind.ClientUI.Properties.Resources.design;
			barButtonItemDesign.Hint = "Design";
			barButtonItemDesign.Id = 61;
			barButtonItemDesign.Name = "barButtonItemDesign";
			toolTipTitleItem.Appearance.Image = Micromind.ClientUI.Properties.Resources.design;
			toolTipTitleItem.Appearance.Options.UseImage = true;
			toolTipTitleItem.Image = Micromind.ClientUI.Properties.Resources.design;
			toolTipTitleItem.Text = "Designer";
			toolTipItem.LeftIndent = 6;
			toolTipItem.Text = "Open the designer to design the layout.";
			superToolTip.Items.Add(toolTipTitleItem);
			superToolTip.Items.Add(toolTipItem);
			barButtonItemDesign.SuperTip = superToolTip;
			barButtonItemDesign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barSubItemDesign_ItemClick);
			printPreviewBarItemExport.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItemExport.Caption = "Export Document...";
			printPreviewBarItemExport.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportFile;
			printPreviewBarItemExport.Enabled = false;
			printPreviewBarItemExport.Hint = "Export Document...";
			printPreviewBarItemExport.Id = 29;
			printPreviewBarItemExport.ImageIndex = 18;
			printPreviewBarItemExport.Name = "printPreviewBarItemExport";
			printPreviewBarItemEmail.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
			printPreviewBarItemEmail.Caption = "Send via E-Mail...";
			printPreviewBarItemEmail.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendFile;
			printPreviewBarItemEmail.Enabled = false;
			printPreviewBarItemEmail.Hint = "Send via E-Mail...";
			printPreviewBarItemEmail.Id = 30;
			printPreviewBarItemEmail.ImageIndex = 17;
			printPreviewBarItemEmail.Name = "printPreviewBarItemEmail";
			printPreviewBarItem26.Caption = "E&xit";
			printPreviewBarItem26.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ClosePreview;
			printPreviewBarItem26.Enabled = false;
			printPreviewBarItem26.Hint = "Close Preview";
			printPreviewBarItem26.Id = 31;
			printPreviewBarItem26.ImageIndex = 13;
			printPreviewBarItem26.Name = "printPreviewBarItem26";
			previewBar2.BarName = "Status Bar";
			previewBar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			previewBar2.DockCol = 0;
			previewBar2.DockRow = 0;
			previewBar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			previewBar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[6]
			{
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewStaticItem1),
				new DevExpress.XtraBars.LinkPersistInfo(barStaticItem1, true),
				new DevExpress.XtraBars.LinkPersistInfo(progressBarEditItem1),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem1),
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem1),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewStaticItem2, true)
			});
			previewBar2.OptionsBar.AllowQuickCustomization = false;
			previewBar2.OptionsBar.DrawDragBorder = false;
			previewBar2.OptionsBar.UseWholeRow = true;
			previewBar2.Text = "Status Bar";
			printPreviewStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			printPreviewStaticItem1.Caption = "Nothing";
			printPreviewStaticItem1.Id = 0;
			printPreviewStaticItem1.LeftIndent = 1;
			printPreviewStaticItem1.Name = "printPreviewStaticItem1";
			printPreviewStaticItem1.RightIndent = 1;
			printPreviewStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
			printPreviewStaticItem1.Type = "PageOfPages";
			barStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			barStaticItem1.Id = 1;
			barStaticItem1.Name = "barStaticItem1";
			barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
			progressBarEditItem1.Edit = repositoryItemProgressBar1;
			progressBarEditItem1.EditHeight = 12;
			progressBarEditItem1.Id = 2;
			progressBarEditItem1.Name = "progressBarEditItem1";
			progressBarEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			progressBarEditItem1.Width = 150;
			repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
			printPreviewBarItem1.Caption = "Stop";
			printPreviewBarItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.StopPageBuilding;
			printPreviewBarItem1.Enabled = false;
			printPreviewBarItem1.Hint = "Stop";
			printPreviewBarItem1.Id = 3;
			printPreviewBarItem1.Name = "printPreviewBarItem1";
			printPreviewBarItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
			barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
			barButtonItem1.Enabled = false;
			barButtonItem1.Id = 4;
			barButtonItem1.Name = "barButtonItem1";
			printPreviewStaticItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			printPreviewStaticItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			printPreviewStaticItem2.Caption = "100%";
			printPreviewStaticItem2.Id = 5;
			printPreviewStaticItem2.Name = "printPreviewStaticItem2";
			printPreviewStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
			printPreviewStaticItem2.Type = "ZoomFactor";
			printPreviewStaticItem2.Width = 40;
			previewBar3.BarName = "Main Menu";
			previewBar3.DockCol = 0;
			previewBar3.DockRow = 0;
			previewBar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			previewBar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[3]
			{
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewSubItem1),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewSubItem2),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewSubItem3)
			});
			previewBar3.OptionsBar.MultiLine = true;
			previewBar3.OptionsBar.UseWholeRow = true;
			previewBar3.Text = "Main Menu";
			printPreviewSubItem1.Caption = "&File";
			printPreviewSubItem1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.File;
			printPreviewSubItem1.Id = 32;
			printPreviewSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[6]
			{
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem10),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem8),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem9),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItemExport, true),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItemEmail),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem26, true)
			});
			printPreviewSubItem1.Name = "printPreviewSubItem1";
			printPreviewSubItem2.Caption = "&View";
			printPreviewSubItem2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.View;
			printPreviewSubItem2.Id = 33;
			printPreviewSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
			{
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewSubItem4, true),
				new DevExpress.XtraBars.LinkPersistInfo(barToolbarsListItem1, true)
			});
			printPreviewSubItem2.Name = "printPreviewSubItem2";
			printPreviewSubItem4.Caption = "&Page Layout";
			printPreviewSubItem4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayout;
			printPreviewSubItem4.Id = 35;
			printPreviewSubItem4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
			{
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem27),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem28)
			});
			printPreviewSubItem4.Name = "printPreviewSubItem4";
			printPreviewBarItem27.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem27.Caption = "&Facing";
			printPreviewBarItem27.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutFacing;
			printPreviewBarItem27.Enabled = false;
			printPreviewBarItem27.GroupIndex = 100;
			printPreviewBarItem27.Id = 36;
			printPreviewBarItem27.Name = "printPreviewBarItem27";
			printPreviewBarItem28.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
			printPreviewBarItem28.Caption = "&Continuous";
			printPreviewBarItem28.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageLayoutContinuous;
			printPreviewBarItem28.Enabled = false;
			printPreviewBarItem28.GroupIndex = 100;
			printPreviewBarItem28.Id = 37;
			printPreviewBarItem28.Name = "printPreviewBarItem28";
			barToolbarsListItem1.Caption = "Bars";
			barToolbarsListItem1.Id = 38;
			barToolbarsListItem1.Name = "barToolbarsListItem1";
			printPreviewSubItem3.Caption = "&Background";
			printPreviewSubItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Background;
			printPreviewSubItem3.Id = 34;
			printPreviewSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
			{
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem22),
				new DevExpress.XtraBars.LinkPersistInfo(printPreviewBarItem23)
			});
			printPreviewSubItem3.Name = "printPreviewSubItem3";
			barSubItem3.Caption = "Designer";
			barSubItem3.Id = 58;
			barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem2)
			});
			barSubItem3.Name = "barSubItem3";
			barButtonItem2.Caption = "Design...";
			barButtonItem2.Id = 60;
			barButtonItem2.Name = "barButtonItem2";
			barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barSubItemDesign_ItemClick);
			barDockControlTop.CausesValidation = false;
			barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			barDockControlTop.Location = new System.Drawing.Point(0, 0);
			barDockControlTop.Size = new System.Drawing.Size(820, 53);
			barDockControlBottom.CausesValidation = false;
			barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			barDockControlBottom.Location = new System.Drawing.Point(0, 555);
			barDockControlBottom.Size = new System.Drawing.Size(820, 25);
			barDockControlLeft.CausesValidation = false;
			barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			barDockControlLeft.Location = new System.Drawing.Point(0, 53);
			barDockControlLeft.Size = new System.Drawing.Size(0, 502);
			barDockControlRight.CausesValidation = false;
			barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			barDockControlRight.Location = new System.Drawing.Point(820, 53);
			barDockControlRight.Size = new System.Drawing.Size(0, 502);
			barButtonExportPDF.Caption = "PDF File";
			barButtonExportPDF.Checked = true;
			barButtonExportPDF.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf;
			barButtonExportPDF.Enabled = false;
			barButtonExportPDF.GroupIndex = 2;
			barButtonExportPDF.Hint = "PDF File";
			barButtonExportPDF.Id = 39;
			barButtonExportPDF.Name = "barButtonExportPDF";
			barButtonExportHTML.Caption = "HTML File";
			barButtonExportHTML.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportHtm;
			barButtonExportHTML.Enabled = false;
			barButtonExportHTML.GroupIndex = 2;
			barButtonExportHTML.Hint = "HTML File";
			barButtonExportHTML.Id = 40;
			barButtonExportHTML.Name = "barButtonExportHTML";
			barButtonExportMHT.Caption = "MHT File";
			barButtonExportMHT.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportMht;
			barButtonExportMHT.Enabled = false;
			barButtonExportMHT.GroupIndex = 2;
			barButtonExportMHT.Hint = "MHT File";
			barButtonExportMHT.Id = 41;
			barButtonExportMHT.Name = "barButtonExportMHT";
			barButtonExportRTF.Caption = "RTF File";
			barButtonExportRTF.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportRtf;
			barButtonExportRTF.Enabled = false;
			barButtonExportRTF.GroupIndex = 2;
			barButtonExportRTF.Hint = "RTF File";
			barButtonExportRTF.Id = 42;
			barButtonExportRTF.Name = "barButtonExportRTF";
			barButtonExportXLS.Caption = "XLS File";
			barButtonExportXLS.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXls;
			barButtonExportXLS.Enabled = false;
			barButtonExportXLS.GroupIndex = 2;
			barButtonExportXLS.Hint = "XLS File";
			barButtonExportXLS.Id = 43;
			barButtonExportXLS.Name = "barButtonExportXLS";
			barButtonExportXLSX.Caption = "XLSX File";
			barButtonExportXLSX.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXlsx;
			barButtonExportXLSX.Enabled = false;
			barButtonExportXLSX.GroupIndex = 2;
			barButtonExportXLSX.Hint = "XLSX File";
			barButtonExportXLSX.Id = 44;
			barButtonExportXLSX.Name = "barButtonExportXLSX";
			barButtonExportCSV.Caption = "CSV File";
			barButtonExportCSV.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportCsv;
			barButtonExportCSV.Enabled = false;
			barButtonExportCSV.GroupIndex = 2;
			barButtonExportCSV.Hint = "CSV File";
			barButtonExportCSV.Id = 45;
			barButtonExportCSV.Name = "barButtonExportCSV";
			barButtonExportText.Caption = "Text File";
			barButtonExportText.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportTxt;
			barButtonExportText.Enabled = false;
			barButtonExportText.GroupIndex = 2;
			barButtonExportText.Hint = "Text File";
			barButtonExportText.Id = 46;
			barButtonExportText.Name = "barButtonExportText";
			barButtonExportImage.Caption = "Image File";
			barButtonExportImage.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportGraphic;
			barButtonExportImage.Enabled = false;
			barButtonExportImage.GroupIndex = 2;
			barButtonExportImage.Hint = "Image File";
			barButtonExportImage.Id = 47;
			barButtonExportImage.Name = "barButtonExportImage";
			barButtonExportPDF0.Caption = "PDF File";
			barButtonExportPDF0.Checked = true;
			barButtonExportPDF0.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendPdf;
			barButtonExportPDF0.Enabled = false;
			barButtonExportPDF0.GroupIndex = 1;
			barButtonExportPDF0.Hint = "PDF File";
			barButtonExportPDF0.Id = 48;
			barButtonExportPDF0.Name = "barButtonExportPDF0";
			barButtonExportPDF1.Caption = "MHT File";
			barButtonExportPDF1.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendMht;
			barButtonExportPDF1.Enabled = false;
			barButtonExportPDF1.GroupIndex = 1;
			barButtonExportPDF1.Hint = "MHT File";
			barButtonExportPDF1.Id = 49;
			barButtonExportPDF1.Name = "barButtonExportPDF1";
			barButtonExportPDF2.Caption = "RTF File";
			barButtonExportPDF2.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendRtf;
			barButtonExportPDF2.Enabled = false;
			barButtonExportPDF2.GroupIndex = 1;
			barButtonExportPDF2.Hint = "RTF File";
			barButtonExportPDF2.Id = 50;
			barButtonExportPDF2.Name = "barButtonExportPDF2";
			barButtonExportPDF3.Caption = "XLS File";
			barButtonExportPDF3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXls;
			barButtonExportPDF3.Enabled = false;
			barButtonExportPDF3.GroupIndex = 1;
			barButtonExportPDF3.Hint = "XLS File";
			barButtonExportPDF3.Id = 51;
			barButtonExportPDF3.Name = "barButtonExportPDF3";
			barButtonExportPDF4.Caption = "XLSX File";
			barButtonExportPDF4.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXlsx;
			barButtonExportPDF4.Enabled = false;
			barButtonExportPDF4.GroupIndex = 1;
			barButtonExportPDF4.Hint = "XLSX File";
			barButtonExportPDF4.Id = 52;
			barButtonExportPDF4.Name = "barButtonExportPDF4";
			barButtonExportPDF5.Caption = "CSV File";
			barButtonExportPDF5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendCsv;
			barButtonExportPDF5.Enabled = false;
			barButtonExportPDF5.GroupIndex = 1;
			barButtonExportPDF5.Hint = "CSV File";
			barButtonExportPDF5.Id = 53;
			barButtonExportPDF5.Name = "barButtonExportPDF5";
			barButtonExportPDF6.Caption = "Text File";
			barButtonExportPDF6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendTxt;
			barButtonExportPDF6.Enabled = false;
			barButtonExportPDF6.GroupIndex = 1;
			barButtonExportPDF6.Hint = "Text File";
			barButtonExportPDF6.Id = 54;
			barButtonExportPDF6.Name = "barButtonExportPDF6";
			barButtonExportPDF7.Caption = "Image File";
			barButtonExportPDF7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendGraphic;
			barButtonExportPDF7.Enabled = false;
			barButtonExportPDF7.GroupIndex = 1;
			barButtonExportPDF7.Hint = "Image File";
			barButtonExportPDF7.Id = 55;
			barButtonExportPDF7.Name = "barButtonExportPDF7";
			barSubItem1.Caption = "&Help";
			barSubItem1.Id = 56;
			barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(barSubItem2)
			});
			barSubItem1.Name = "barSubItem1";
			barSubItem2.Caption = "About";
			barSubItem2.Id = 57;
			barSubItem2.Name = "barSubItem2";
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(820, 580);
			base.Controls.Add(printControl);
			base.Controls.Add(barDockControlLeft);
			base.Controls.Add(barDockControlRight);
			base.Controls.Add(barDockControlBottom);
			base.Controls.Add(barDockControlTop);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "PrintPreviewForm";
			Text = "Print Preview";
			base.Load += new System.EventHandler(PrintPreviewForm_Load);
			((System.ComponentModel.ISupportInitialize)printBarManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)printPreviewRepositoryItemComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar1).EndInit();
			ResumeLayout(false);
		}

		public void Preview()
		{
		}

		private void SetSecurity()
		{
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowExportDocument))
			{
				printControl.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportFile, DevExpress.XtraPrinting.CommandVisibility.None);
				printControl.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.SendFile, DevExpress.XtraPrinting.CommandVisibility.None);
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowDesignPrintTemplate))
			{
				barButtonItemDesign.Visibility = BarItemVisibility.Never;
			}
		}

		private void PrintPreviewForm_Load(object sender, EventArgs e)
		{
			try
			{
				printControl.PrintingSystem = report.PrintingSystem;
				report.CreateDocument();
				SetSecurity();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void barSubItemDesign_ItemClick(object sender, ItemClickEventArgs e)
		{
			XRDesignForm xRDesignForm = new XRDesignForm();
			mdiController = xRDesignForm.DesignMdiController;
			mdiController.DesignPanelLoaded += mdiController_DesignPanelLoaded;
			mdiController.OpenReport(report);
			xRDesignForm.ShowDialog();
			if (mdiController.ActiveDesignPanel != null)
			{
				mdiController.ActiveDesignPanel.CloseReport();
				report.CreateDocument();
			}
		}

		private void mdiController_DesignPanelLoaded(object sender, DesignerLoadedEventArgs e)
		{
			XRDesignPanel obj = (XRDesignPanel)sender;
			obj.AddCommandHandler(new SaveCommandHandler(obj));
		}
	}
}
