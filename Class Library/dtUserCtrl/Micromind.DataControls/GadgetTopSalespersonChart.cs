using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls.FlatDashboard;
using Micromind.DataControls.Libraries;
using Micromind.DataControls.Properties;
using Micromind.UISupport.DataGrid;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class GadgetTopSalespersonChart : UserControl, IGadget
	{
		private bool isDataLoaded;

		private bool isRefresh;

		private bool hasAccess = true;

		private Micromind.DataControls.FlatDashboard.FlatDashboard parentDashboard;

		private IContainer components;

		private BackgroundWorker backgroundWorker;

		private PictureBox pictureBoxWait;

		private Panel panel1;

		private ImageList imageList1;

		private DevXChart chartControl;

		private Label label1;

		private GadgetDateRangeComboBox comboBoxPeriod;

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

		public bool IsBusy => backgroundWorker.IsBusy;

		public string GadgetTitle
		{
			get
			{
				return "Top 10 Salespersons";
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

		public GadgetTypes GadgetType => GadgetTypes.TopSalespersonChart;

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

		public ViewType ChartType
		{
			set
			{
			}
		}

		public string Description
		{
			get
			{
				return "Top 10 salespersons for a selected period.";
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

		public GadgetTopSalespersonChart()
		{
			InitializeComponent();
			if (!Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, ((int)(5000 + GadgetType)).ToString()).Visible)
			{
				hasAccess = false;
			}
			base.SizeChanged += FavoriteBankAccountsGadget_SizeChanged;
			base.ClientSizeChanged += FavoriteBankAccountsGadget_ClientSizeChanged;
			backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
			comboBoxPeriod.SelectedIndexChanged += comboBoxPeriod_SelectedIndexChanged;
			comboBoxPeriod.LoadData();
		}

		private void comboBoxPeriod_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadData(isRefresh: true);
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
					if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
					{
						chartControl.SetData(dataSet.Tables[0], "Sales", "Salesperson");
						chartControl.Visible = true;
					}
					else
					{
						chartControl.Clear();
					}
					chartControl.FontSize = 8f;
					chartControl.CrossHairPattern = "{A}:{V:#,##0.##}";
					panel1.Enabled = true;
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
				DateTime fromDate = comboBoxPeriod.FromDate;
				DateTime toDate = comboBoxPeriod.ToDate;
				DataSet dataSet = (DataSet)(e.Result = Factory.CustomerSystemAsync.GetTopSalesperson(fromDate, toDate, 10));
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
			Micromind.DataControls.Libraries.ComboData comboData = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData2 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData3 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData4 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData5 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData6 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData7 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData8 = new Micromind.DataControls.Libraries.ComboData();
			DevExpress.XtraCharts.XYDiagram xYDiagram = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			backgroundWorker = new System.ComponentModel.BackgroundWorker();
			pictureBoxWait = new System.Windows.Forms.PictureBox();
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			comboBoxPeriod = new Micromind.DataControls.GadgetDateRangeComboBox(components);
			imageList1 = new System.Windows.Forms.ImageList(components);
			chartControl = new Micromind.UISupport.DataGrid.DevXChart(components);
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).BeginInit();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPeriod.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)chartControl).BeginInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram).BeginInit();
			((System.ComponentModel.ISupportInitialize)series).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel2).BeginInit();
			SuspendLayout();
			backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);
			pictureBoxWait.Anchor = System.Windows.Forms.AnchorStyles.None;
			pictureBoxWait.BackColor = System.Drawing.Color.White;
			pictureBoxWait.Image = Micromind.DataControls.Properties.Resources.wait;
			pictureBoxWait.Location = new System.Drawing.Point(185, 81);
			pictureBoxWait.Name = "pictureBoxWait";
			pictureBoxWait.Size = new System.Drawing.Size(32, 32);
			pictureBoxWait.TabIndex = 10;
			pictureBoxWait.TabStop = false;
			panel1.BackColor = System.Drawing.Color.Transparent;
			panel1.Controls.Add(label1);
			panel1.Controls.Add(comboBoxPeriod);
			panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel1.Location = new System.Drawing.Point(0, 198);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(415, 24);
			panel1.TabIndex = 13;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(7, 5);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(40, 13);
			label1.TabIndex = 4;
			label1.Text = "Period:";
			comboData.FieldType = System.Data.DbType.AnsiString;
			comboData.ID = "2";
			comboData.Name = "This Month";
			comboData.Tag = null;
			comboBoxPeriod.EditValue = comboData;
			comboBoxPeriod.Location = new System.Drawing.Point(52, 2);
			comboBoxPeriod.Name = "comboBoxPeriod";
			comboBoxPeriod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboData2.FieldType = System.Data.DbType.AnsiString;
			comboData2.ID = "0";
			comboData2.Name = "Today";
			comboData2.Tag = null;
			comboData3.FieldType = System.Data.DbType.AnsiString;
			comboData3.ID = "6";
			comboData3.Name = "This Week";
			comboData3.Tag = null;
			comboData4.FieldType = System.Data.DbType.AnsiString;
			comboData4.ID = "2";
			comboData4.Name = "This Month";
			comboData4.Tag = null;
			comboData5.FieldType = System.Data.DbType.AnsiString;
			comboData5.ID = "3";
			comboData5.Name = "This Year";
			comboData5.Tag = null;
			comboData6.FieldType = System.Data.DbType.AnsiString;
			comboData6.ID = "0";
			comboData6.Name = "Today";
			comboData6.Tag = null;
			comboData7.FieldType = System.Data.DbType.AnsiString;
			comboData7.ID = "6";
			comboData7.Name = "This Week";
			comboData7.Tag = null;
			comboData8.FieldType = System.Data.DbType.AnsiString;
			comboData8.ID = "3";
			comboData8.Name = "This Year";
			comboData8.Tag = null;
			comboBoxPeriod.Properties.Items.AddRange(new object[8]
			{
				comboData2,
				comboData3,
				comboData4,
				comboData5,
				comboData6,
				comboData7,
				comboData,
				comboData8
			});
			comboBoxPeriod.Size = new System.Drawing.Size(106, 20);
			comboBoxPeriod.TabIndex = 3;
			imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			imageList1.ImageSize = new System.Drawing.Size(48, 48);
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
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
			chartControl.Size = new System.Drawing.Size(415, 198);
			chartControl.TabIndex = 17;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(chartControl);
			base.Controls.Add(panel1);
			base.Controls.Add(pictureBoxWait);
			base.Name = "GadgetTopSalespersonChart";
			base.Size = new System.Drawing.Size(415, 222);
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPeriod.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).EndInit();
			((System.ComponentModel.ISupportInitialize)series).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel2).EndInit();
			((System.ComponentModel.ISupportInitialize)chartControl).EndInit();
			ResumeLayout(false);
		}
	}
}
