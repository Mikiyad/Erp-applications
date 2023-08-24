using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors.Controls;
using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.DataControls.FlatDashboard;
using Micromind.DataControls.Libraries;
using Micromind.DataControls.Properties;
using Micromind.UISupport.DataGrid;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CustomGadgetChart : UserControl, IGadget, ICustomGadget, ICustomGadgetChart
	{
		private GadgetFilterOptions filterOption;

		private CustomGadgetData gadgetData;

		private string gadgetTitle = "Custom Gadget";

		private bool autoRefresh;

		private int refreshInterval = 300;

		private bool isDataLoaded;

		private bool isRefresh;

		private bool allowDateFilter = true;

		private bool hasAccess = true;

		private bool isInit;

		private Micromind.DataControls.FlatDashboard.FlatDashboard parentDashboard;

		private bool isLoaded;

		private GadgetStyles gadgetStyle = GadgetStyles.Chart;

		private string gadgetID = "";

		private IContainer components;

		private BackgroundWorker backgroundWorker;

		private PictureBox pictureBoxWait;

		private Panel panel1;

		private ImageList imageList1;

		private DevXChart chartControl;

		private Label labelPeriod;

		private GadgetDateRangeComboBox comboBoxPeriod;

		private Timer timerMain;

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

		public ViewType ChartType
		{
			get
			{
				return SeriesViewFactory.GetViewType(chartControl.Series[0].View);
			}
			set
			{
				chartControl.Series[0].ChangeView(value);
			}
		}

		public bool ColorEach
		{
			get
			{
				return (chartControl.Series[0].View as SeriesViewColorEachSupportBase)?.ColorEach ?? false;
			}
			set
			{
				SeriesViewColorEachSupportBase seriesViewColorEachSupportBase = chartControl.Series[0].View as SeriesViewColorEachSupportBase;
				if (seriesViewColorEachSupportBase != null)
				{
					seriesViewColorEachSupportBase.ColorEach = value;
				}
			}
		}

		public string ColorPaletteName
		{
			get
			{
				return chartControl.PaletteName;
			}
			set
			{
				if (value != null)
				{
					chartControl.PaletteName = value;
				}
			}
		}

		public bool AllowDateFilter
		{
			get
			{
				return allowDateFilter;
			}
			set
			{
				allowDateFilter = value;
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
				return gadgetTitle;
			}
			set
			{
				gadgetTitle = value;
			}
		}

		public GadgetFilterOptions FilterOption
		{
			get
			{
				return filterOption;
			}
			set
			{
				filterOption = value;
				if (value == GadgetFilterOptions.DatePeriod)
				{
					Label label = labelPeriod;
					bool visible = comboBoxPeriod.Visible = true;
					label.Visible = visible;
				}
				else
				{
					Label label2 = labelPeriod;
					bool visible = comboBoxPeriod.Visible = false;
					label2.Visible = visible;
				}
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
				gadgetStyle = value;
			}
		}

		public GadgetTypes GadgetType => GadgetTypes.Custom;

		public string GadgetID
		{
			get
			{
				return gadgetID;
			}
			set
			{
				gadgetID = value;
			}
		}

		public string Description
		{
			get;
			set;
		}

		public GadgetCategories Category
		{
			get;
			set;
		}

		public GroupLayout GroupLayout
		{
			get;
			set;
		}

		public event EventHandler DataLoadCompleted;

		public CustomGadgetChart()
		{
			InitializeComponent();
			try
			{
				isInit = true;
				base.SizeChanged += FavoriteBankAccountsGadget_SizeChanged;
				base.ClientSizeChanged += FavoriteBankAccountsGadget_ClientSizeChanged;
				backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
				comboBoxPeriod.SelectedIndexChanged += comboBoxPeriod_SelectedIndexChanged;
				comboBoxPeriod.LoadData();
				if (!allowDateFilter)
				{
					labelPeriod.Visible = false;
					comboBoxPeriod.Visible = false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				isInit = false;
			}
		}

		private void comboBoxPeriod_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInit)
			{
				LoadData(isRefresh: true);
			}
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
			if (IsBusy)
			{
				return;
			}
			this.isRefresh = isRefresh;
			pictureBoxWait.Visible = true;
			pictureBoxWait.BringToFront();
			if (!isLoaded)
			{
				if (!Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, gadgetID).Visible)
				{
					hasAccess = false;
				}
				isLoaded = true;
			}
			if (hasAccess)
			{
				backgroundWorker.RunWorkerAsync();
			}
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if ((!isDataLoaded || isRefresh) && e.Error == null)
				{
					if (gadgetData != null)
					{
						DataRow dataRow = gadgetData.Tables[0].Rows[0];
						if (gadgetData.Tables[0].Rows[0]["AutoRefresh"] != DBNull.Value)
						{
							autoRefresh = bool.Parse(gadgetData.Tables[0].Rows[0]["AutoRefresh"].ToString());
						}
						if (gadgetData.Tables[0].Rows[0]["RefreshInterval"] != DBNull.Value)
						{
							refreshInterval = int.Parse(gadgetData.Tables[0].Rows[0]["RefreshInterval"].ToString());
						}
						if (autoRefresh)
						{
							timerMain.Interval = refreshInterval * 1000;
							Timer timer = timerMain;
							bool enabled = base.Enabled = true;
							timer.Enabled = enabled;
						}
						bool flag2 = false;
						if (Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["ShowLegend"]))
						{
							chartControl.Legend.Visibility = DefaultBoolean.False;
						}
						else if (bool.Parse(dataRow["ShowLegend"].ToString()))
						{
							chartControl.Legend.Visibility = DefaultBoolean.True;
						}
						else
						{
							chartControl.Legend.Visibility = DefaultBoolean.False;
						}
						flag2 = false;
						Diagram diagram = chartControl.Diagram;
						if (Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["AxisYVisible"]))
						{
							if (diagram.GetType() == typeof(XYDiagram))
							{
								((XYDiagram)chartControl.Diagram).AxisY.Visibility = DefaultBoolean.False;
							}
						}
						else if (bool.Parse(dataRow["AxisYVisible"].ToString()))
						{
							if (diagram.GetType() == typeof(XYDiagram))
							{
								((XYDiagram)chartControl.Diagram).AxisY.Visibility = DefaultBoolean.True;
							}
						}
						else if (diagram.GetType() == typeof(XYDiagram))
						{
							((XYDiagram)chartControl.Diagram).AxisY.Visibility = DefaultBoolean.False;
						}
						flag2 = false;
						if (Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["IsRotated"]))
						{
							if (diagram.GetType() == typeof(XYDiagram))
							{
								((XYDiagram)chartControl.Diagram).Rotated = false;
							}
						}
						else
						{
							flag2 = bool.Parse(dataRow["IsRotated"].ToString());
							if (diagram.GetType() == typeof(XYDiagram))
							{
								((XYDiagram)chartControl.Diagram).Rotated = flag2;
							}
						}
						string text = dataRow["AxisXTitle"].ToString();
						if (Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(text))
						{
							if (diagram.GetType() == typeof(XYDiagram))
							{
								((XYDiagram)chartControl.Diagram).AxisX.Title.Visibility = DefaultBoolean.False;
							}
						}
						else if (diagram.GetType() == typeof(XYDiagram))
						{
							((XYDiagram)chartControl.Diagram).AxisX.Title.Visibility = DefaultBoolean.True;
							((XYDiagram)chartControl.Diagram).AxisX.Title.Text = text;
						}
						chartControl.CrosshairEnabled = DefaultBoolean.True;
						text = dataRow["AxisYTitle"].ToString();
						if (Micromind.ClientLibraries.ExtensionMethods.IsNullOrEmpty(text))
						{
							if (diagram.GetType() == typeof(XYDiagram))
							{
								((XYDiagram)chartControl.Diagram).AxisY.Title.Visibility = DefaultBoolean.False;
							}
						}
						else if (diagram.GetType() == typeof(XYDiagram))
						{
							((XYDiagram)chartControl.Diagram).AxisY.Title.Visibility = DefaultBoolean.True;
							((XYDiagram)chartControl.Diagram).AxisY.Title.Text = text;
						}
						text = dataRow["AxisYTextPattern"].ToString();
						if (diagram.GetType() == typeof(XYDiagram))
						{
							((XYDiagram)chartControl.Diagram).AxisY.Label.TextPattern = text;
						}
						chartControl.PaletteName = (Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["ColorPaletteName"]) ? "Default" : dataRow["ColorPaletteName"].ToString());
						chartControl.PaletteBaseColorNumber = ((!Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["ChartPallet"])) ? int.Parse(dataRow["ChartPallet"].ToString()) : 0);
						flag2 = false;
						if (!Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["TopNOption"]))
						{
							flag2 = bool.Parse(dataRow["TopNOption"].ToString());
						}
						foreach (Series item in chartControl.Series)
						{
							item.TopNOptions.Enabled = flag2;
							int num = Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["TopNCount"]) ? 5 : int.Parse(dataRow["TopNCount"].ToString());
							if (num == 0)
							{
								num = 5;
							}
							item.TopNOptions.Count = num;
							item.TopNOptions.OthersArgument = dataRow["TopNOthersText"].ToString();
							item.TopNOptions.ShowOthers = (!Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(dataRow["TopNCount"]) && bool.Parse(dataRow["ShowTopNOther"].ToString()));
							item.LegendTextPattern = dataRow["LegendTextPattern"].ToString();
						}
						chartControl.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
						chartControl.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
					}
					DataSet dataSet = e.Result as DataSet;
					if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count >= 1)
					{
						chartControl.DataSource = dataSet.Tables[0];
						chartControl.Visible = true;
					}
					else
					{
						chartControl.Clear();
					}
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
				_ = comboBoxPeriod.FromDate;
				_ = comboBoxPeriod.ToDate;
				CustomGadgetData customGadgetData = gadgetData = Factory.CustomGadgetSystemAsync.GetCustomGadgetByID(GadgetID);
				if (customGadgetData != null && customGadgetData.CustomGadgetTable.Rows.Count != 0)
				{
					CustomGadget obj = (CustomGadget)Global.DeserializeFromStream((byte[])customGadgetData.CustomGadgetTable.Rows[0]["GadgetData"]);
					SqlCommand sqlCommand = new SqlCommand();
					sqlCommand.Parameters.AddWithValue("@FromDate", CommonLib.ToSqlDateTimeString(comboBoxPeriod.FromDate));
					sqlCommand.Parameters.AddWithValue("@EndDate", CommonLib.ToSqlDateTimeString(comboBoxPeriod.ToDate));
					foreach (GadgetParameter parameter in obj.Parameters)
					{
						if (!sqlCommand.Parameters.Contains(parameter.ParameterName))
						{
							sqlCommand.Parameters.AddWithValue(parameter.ParameterName, "NULL");
						}
					}
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					foreach (SqlParameter parameter2 in sqlCommand.Parameters)
					{
						arrayList.Add(parameter2.ParameterName);
						arrayList2.Add(parameter2.Value.ToString());
					}
					Array array = arrayList.ToArray(typeof(string));
					Array array2 = arrayList2.ToArray(typeof(string));
					DataSet dataSet = (DataSet)(e.Result = Factory.CustomGadgetSystemAsync.GetCustomGadgetData(GadgetID, (string[])array, (string[])array2));
				}
			}
			catch (Exception)
			{
			}
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			LoadData(isRefresh: true);
		}

		private void timerMain_Tick(object sender, EventArgs e)
		{
			LoadData(isRefresh: true);
		}

		public void Init()
		{
			chartControl.Series.Clear();
			foreach (ChartSerie item in GroupLayout.ChartSeries)
			{
				if (GroupLayout.ValueColumn == "")
				{
					GroupLayout.ValueColumn = item.ValueColumn;
				}
				Series series = new Series(item.DisplayName, (ViewType)item.ChartType);
				series.LegendText = item.DisplayName;
				series.SetDataMembers(GroupLayout.ArgumentColumn, item.ValueColumn);
				series.ValueDataMembers.AddRange(item.ValueColumn);
				series.Label.Border.Visibility = DefaultBoolean.False;
				if (item.LabelPosition != -1)
				{
					if (series.Label.GetType() == typeof(SideBySideBarSeriesLabel))
					{
						((SideBySideBarSeriesLabel)series.Label).Position = (BarSeriesLabelPosition)item.LabelPosition;
					}
					else if (series.Label.GetType() == typeof(StackedBarSeriesLabel) || series.GetType() == typeof(FullStackedBarSeriesLabel))
					{
						((StackedBarSeriesLabel)series.Label).Position = (BarSeriesLabelPosition)item.LabelPosition;
					}
					else if (series.Label.GetType() == typeof(PieSeriesLabel) || series.GetType() == typeof(DoughnutSeriesLabel) || series.GetType() == typeof(NestedDoughnutSeriesLabel))
					{
						((PieSeriesLabel)series.Label).Position = (PieSeriesLabelPosition)item.LabelPosition;
						chartControl.ToolTipEnabled = DefaultBoolean.True;
					}
					else if (series.Label.GetType() == typeof(PointSeriesLabel) || series.GetType() == typeof(BubbleSeriesLabel) || series.GetType() == typeof(StackedLineSeriesLabel) || series.GetType() == typeof(RadarPointSeriesLabel))
					{
						((PointSeriesLabel)series.Label).Position = (PointLabelPosition)item.LabelPosition;
					}
					else if (series.Label.GetType() == typeof(FunnelSeriesLabel))
					{
						((FunnelSeriesLabel)series.Label).Position = (FunnelSeriesLabelPosition)item.LabelPosition;
						chartControl.ToolTipEnabled = DefaultBoolean.True;
					}
					else if (series.Label.GetType() == typeof(RangeBarSeriesLabel))
					{
						((RangeBarSeriesLabel)series.Label).Position = (RangeBarLabelPosition)item.LabelPosition;
					}
				}
				if (item.LabelVisible)
				{
					series.LabelsVisibility = DefaultBoolean.True;
				}
				else
				{
					series.LabelsVisibility = DefaultBoolean.False;
				}
				series.Label.TextPattern = item.LabelTextPattern;
				series.CrosshairLabelPattern = "{S}:{V:n}";
				chartControl.Series.Add(series);
			}
			chartControl.AnimationStartMode = ChartAnimationMode.OnLoad;
			chartControl.Legend.Direction = LegendDirection.LeftToRight;
			chartControl.Legend.Border.Visibility = DefaultBoolean.False;
			chartControl.FontSize = 8f;
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
			labelPeriod = new System.Windows.Forms.Label();
			comboBoxPeriod = new Micromind.DataControls.GadgetDateRangeComboBox(components);
			imageList1 = new System.Windows.Forms.ImageList(components);
			chartControl = new Micromind.UISupport.DataGrid.DevXChart(components);
			timerMain = new System.Windows.Forms.Timer(components);
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
			panel1.Controls.Add(labelPeriod);
			panel1.Controls.Add(comboBoxPeriod);
			panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel1.Location = new System.Drawing.Point(0, 198);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(415, 24);
			panel1.TabIndex = 13;
			labelPeriod.AutoSize = true;
			labelPeriod.Location = new System.Drawing.Point(7, 5);
			labelPeriod.Name = "labelPeriod";
			labelPeriod.Size = new System.Drawing.Size(40, 13);
			labelPeriod.TabIndex = 4;
			labelPeriod.Text = "Period:";
			comboData.FieldType = System.Data.DbType.AnsiString;
			comboData.ID = "2";
			comboData.Name = "This Month";
			comboData.Tag = null;
			comboBoxPeriod.EditValue = comboData;
			comboBoxPeriod.Location = new System.Drawing.Point(52, 2);
			comboBoxPeriod.Name = "comboBoxPeriod";
			comboBoxPeriod.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
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
			comboBoxPeriod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxPeriod.Size = new System.Drawing.Size(106, 20);
			comboBoxPeriod.TabIndex = 3;
			imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			imageList1.ImageSize = new System.Drawing.Size(48, 48);
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			chartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
			chartControl.CrossHairPattern = null;
			chartControl.DataBindings = null;
			xYDiagram.AxisX.VisibleInPanesSerializable = "-1";
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
			timerMain.Tick += new System.EventHandler(timerMain_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(chartControl);
			base.Controls.Add(panel1);
			base.Controls.Add(pictureBoxWait);
			base.Name = "CustomGadgetChart";
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
