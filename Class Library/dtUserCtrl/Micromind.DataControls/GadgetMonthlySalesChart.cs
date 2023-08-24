using DevExpress.Utils;
using DevExpress.XtraCharts;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls.FlatDashboard;
using Micromind.DataControls.Properties;
using Micromind.UISupport.DataGrid;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class GadgetMonthlySalesChart : UserControl, IGadget
	{
		private bool hasAccess = true;

		private bool isDataLoaded;

		private bool isRefresh;

		private Micromind.DataControls.FlatDashboard.FlatDashboard parentDashboard;

		private IContainer components;

		private BackgroundWorker backgroundWorker;

		private PictureBox pictureBoxWait;

		private ImageList imageList1;

		private DevXChart chartControl;

		public Micromind.DataControls.FlatDashboard.FlatDashboard ParentDashboard
		{
			get
			{
				return parentDashboard;
			}
			set
			{
				parentDashboard = value;
			}
		}

		public bool IsBusy => backgroundWorker.IsBusy;

		public Image Icon
		{
			get
			{
				if (imageList1.Images.Count > 0)
				{
					return imageList1.Images[0];
				}
				return null;
			}
		}

		public ViewType ChartType
		{
			set
			{
			}
		}

		public string GadgetTitle
		{
			get
			{
				return "Monthly Sales Comparison";
			}
			set
			{
			}
		}

		public GadgetStyles GadgetStyle
		{
			get
			{
				return GadgetStyles.Chart;
			}
			set
			{
			}
		}

		public GadgetTypes GadgetType => GadgetTypes.MonthlySalesChart;

		public string GadgetID
		{
			get
			{
				return "SYS" + (int)GadgetType;
			}
			set
			{
			}
		}

		public string Description
		{
			get
			{
				return "Displays monthly sales comparison chart.";
			}
			set
			{
			}
		}

		public GadgetCategories Category
		{
			get
			{
				return GadgetCategories.Sales;
			}
			set
			{
			}
		}

		public event EventHandler DataLoadCompleted;

		public GadgetMonthlySalesChart()
		{
			InitializeComponent();
			if (!Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, ((int)(5000 + GadgetType)).ToString()).Visible)
			{
				hasAccess = false;
			}
			base.SizeChanged += FavoriteBankAccountsGadget_SizeChanged;
			base.ClientSizeChanged += FavoriteBankAccountsGadget_ClientSizeChanged;
			backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
		}

		private void FavoriteBankAccountsGadget_ClientSizeChanged(object sender, EventArgs e)
		{
			Resize();
		}

		private void FavoriteBankAccountsGadget_SizeChanged(object sender, EventArgs e)
		{
			Resize();
		}

		private void FavoriteBankAccountsGadget_Resize(object sender, EventArgs e)
		{
			Resize();
		}

		private new void Resize()
		{
			pictureBoxWait.Left = base.Width / 2 - pictureBoxWait.Width / 2;
			pictureBoxWait.Top = base.Height / 2 - pictureBoxWait.Height / 2;
		}

		public void LoadData(bool isRefresh)
		{
			if (!IsBusy)
			{
				this.isRefresh = isRefresh;
				pictureBoxWait.Visible = true;
				pictureBoxWait.BringToFront();
				if (hasAccess)
				{
					backgroundWorker.RunWorkerAsync();
				}
			}
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (!isDataLoaded || isRefresh)
				{
					DataSet dataSet = e.Result as DataSet;
					if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 1)
					{
						chartControl.SetData(dataSet.Tables[0], "Sale", "Month");
						chartControl.Visible = true;
					}
					else
					{
						chartControl.Clear();
					}
					chartControl.CrossHairPattern = "{A}:{V:#,##0.##}";
					if (this.DataLoadCompleted != null)
					{
						this.DataLoadCompleted(sender, e);
					}
					isRefresh = false;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				pictureBoxWait.Visible = false;
			}
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				DateTime from = new DateTime(DateTime.Today.Year, 1, 1);
				DateTime today = DateTime.Today;
				DataSet dataSet = (DataSet)(e.Result = Factory.CustomerSystemAsync.GetMonthlySalesReport(from, today));
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			LoadData(isRefresh: true);
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
			DevExpress.XtraCharts.XYDiagram xYDiagram = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			backgroundWorker = new System.ComponentModel.BackgroundWorker();
			imageList1 = new System.Windows.Forms.ImageList(components);
			pictureBoxWait = new System.Windows.Forms.PictureBox();
			chartControl = new Micromind.UISupport.DataGrid.DevXChart(components);
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).BeginInit();
			((System.ComponentModel.ISupportInitialize)chartControl).BeginInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram).BeginInit();
			((System.ComponentModel.ISupportInitialize)series).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel2).BeginInit();
			SuspendLayout();
			backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);
			imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			imageList1.ImageSize = new System.Drawing.Size(48, 48);
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			pictureBoxWait.Anchor = System.Windows.Forms.AnchorStyles.None;
			pictureBoxWait.BackColor = System.Drawing.Color.White;
			pictureBoxWait.Image = Micromind.DataControls.Properties.Resources.wait;
			pictureBoxWait.Location = new System.Drawing.Point(186, 80);
			pictureBoxWait.Name = "pictureBoxWait";
			pictureBoxWait.Size = new System.Drawing.Size(32, 32);
			pictureBoxWait.TabIndex = 10;
			pictureBoxWait.TabStop = false;
			chartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
			chartControl.CrossHairPattern = null;
			chartControl.DataBindings = null;
			xYDiagram.AxisX.VisibleInPanesSerializable = "-1";
			xYDiagram.AxisY.GridLines.Visible = false;
			xYDiagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
			xYDiagram.AxisY.VisibleInPanesSerializable = "-1";
			xYDiagram.DefaultPane.BorderVisible = false;
			chartControl.Diagram = xYDiagram;
			chartControl.Dock = System.Windows.Forms.DockStyle.Fill;
			chartControl.Legend.Name = "Default Legend";
			chartControl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
			chartControl.Location = new System.Drawing.Point(0, 0);
			chartControl.Name = "chartControl";
			sideBySideBarSeriesLabel.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			series.Label = sideBySideBarSeriesLabel;
			series.Name = "Series 1";
			chartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[1]
			{
				series
			};
			sideBySideBarSeriesLabel2.LineVisibility = DevExpress.Utils.DefaultBoolean.True;
			chartControl.SeriesTemplate.Label = sideBySideBarSeriesLabel2;
			chartControl.Size = new System.Drawing.Size(415, 222);
			chartControl.TabIndex = 15;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(chartControl);
			base.Controls.Add(pictureBoxWait);
			base.Name = "GadgetMonthlySalesChart";
			base.Size = new System.Drawing.Size(415, 222);
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).EndInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).EndInit();
			((System.ComponentModel.ISupportInitialize)series).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel2).EndInit();
			((System.ComponentModel.ISupportInitialize)chartControl).EndInit();
			ResumeLayout(false);
		}
	}
}
