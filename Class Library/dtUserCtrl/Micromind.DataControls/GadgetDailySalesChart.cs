using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
	public class GadgetDailySalesChart : UserControl, IGadget
	{
		private bool hasAccess = true;

		private bool isDataLoaded;

		private bool isRefresh;

		private Micromind.DataControls.FlatDashboard.FlatDashboard parentDashboard;

		private IContainer components;

		private BackgroundWorker backgroundWorker;

		private PictureBox pictureBoxWait;

		private Panel panel1;

		private ImageList imageList1;

		private Label label2;

		private Label label1;

		private ComboBoxEdit comboBoxMonth;

		private ComboBoxEdit comboBoxYear;

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

		public ViewType ChartType
		{
			set
			{
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

		public string GadgetTitle
		{
			get
			{
				return "Daily Sales";
			}
			set
			{
			}
		}

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

		public GadgetTypes GadgetType => GadgetTypes.DailySalesChart;

		public string Description
		{
			get
			{
				return "Daily sales comparison chart.";
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

		public GadgetDailySalesChart()
		{
			InitializeComponent();
			if (!Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, ((int)(5000 + GadgetType)).ToString()).Visible)
			{
				hasAccess = false;
			}
			base.SizeChanged += FavoriteBankAccountsGadget_SizeChanged;
			base.ClientSizeChanged += FavoriteBankAccountsGadget_ClientSizeChanged;
			backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
			comboBoxMonth.Text = DateTime.Today.Month.ToString();
			comboBoxYear.Text = DateTime.Today.Year.ToString();
			comboBoxMonth.SelectedIndexChanged += comboBoxMonth_SelectedIndexChanged;
			comboBoxYear.SelectedIndexChanged += comboBoxMonth_SelectedIndexChanged;
		}

		private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
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
				panel1.Enabled = false;
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
					if (dataSet != null && dataSet.Tables.Count > 0)
					{
						chartControl.SetData(dataSet.Tables[0], "Amount", "D");
					}
					else
					{
						chartControl.Clear();
					}
					chartControl.CrossHairPattern = "{V:#,##0.##}";
					if (this.DataLoadCompleted != null)
					{
						this.DataLoadCompleted(sender, e);
					}
					panel1.Enabled = true;
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
				DataSet dataSet = (DataSet)(e.Result = Factory.CustomerSystemAsync.GetDailySalesReport(int.Parse(comboBoxYear.Text), int.Parse(comboBoxMonth.Text)));
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
			pictureBoxWait = new System.Windows.Forms.PictureBox();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			comboBoxMonth = new DevExpress.XtraEditors.ComboBoxEdit();
			comboBoxYear = new DevExpress.XtraEditors.ComboBoxEdit();
			imageList1 = new System.Windows.Forms.ImageList(components);
			chartControl = new Micromind.UISupport.DataGrid.DevXChart(components);
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).BeginInit();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxMonth.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxYear.Properties).BeginInit();
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
			pictureBoxWait.Location = new System.Drawing.Point(190, 81);
			pictureBoxWait.Name = "pictureBoxWait";
			pictureBoxWait.Size = new System.Drawing.Size(32, 32);
			pictureBoxWait.TabIndex = 10;
			pictureBoxWait.TabStop = false;
			panel1.BackColor = System.Drawing.Color.Transparent;
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(comboBoxMonth);
			panel1.Controls.Add(comboBoxYear);
			panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel1.Location = new System.Drawing.Point(0, 197);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(415, 25);
			panel1.TabIndex = 13;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(91, 5);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(32, 13);
			label2.TabIndex = 2;
			label2.Text = "Year:";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 5);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(40, 13);
			label1.TabIndex = 2;
			label1.Text = "Month:";
			comboBoxMonth.EditValue = "12";
			comboBoxMonth.Location = new System.Drawing.Point(45, 2);
			comboBoxMonth.Name = "comboBoxMonth";
			comboBoxMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboBoxMonth.Properties.Items.AddRange(new object[12]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12"
			});
			comboBoxMonth.Properties.NullText = "[EditValue is null]";
			comboBoxMonth.Properties.PopupSizeable = true;
			comboBoxMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxMonth.Size = new System.Drawing.Size(45, 20);
			comboBoxMonth.TabIndex = 1;
			comboBoxYear.EditValue = "2014";
			comboBoxYear.Location = new System.Drawing.Point(128, 2);
			comboBoxYear.Name = "comboBoxYear";
			comboBoxYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboBoxYear.Properties.Items.AddRange(new object[21]
			{
				"2000",
				"2001",
				"2002",
				"2003",
				"2004",
				"2005",
				"2006",
				"2007",
				"2008",
				"2009",
				"2010",
				"2011",
				"2012",
				"2013",
				"2014",
				"2015",
				"2016",
				"2017",
				"2018",
				"2019",
				"2020"
			});
			comboBoxYear.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxYear.Size = new System.Drawing.Size(56, 20);
			comboBoxYear.TabIndex = 1;
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
			chartControl.Size = new System.Drawing.Size(415, 197);
			chartControl.TabIndex = 14;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(chartControl);
			base.Controls.Add(panel1);
			base.Controls.Add(pictureBoxWait);
			base.Name = "GadgetDailySalesChart";
			base.Size = new System.Drawing.Size(415, 222);
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxMonth.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxYear.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)xYDiagram).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).EndInit();
			((System.ComponentModel.ISupportInitialize)series).EndInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel2).EndInit();
			((System.ComponentModel.ISupportInitialize)chartControl).EndInit();
			ResumeLayout(false);
		}
	}
}
