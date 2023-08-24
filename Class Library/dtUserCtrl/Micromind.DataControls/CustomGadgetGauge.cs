using DevExpress.LookAndFeel;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGauges.Base;
using DevExpress.XtraGauges.Core.Base;
using DevExpress.XtraGauges.Core.Drawing;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGauges.Win.Base;
using DevExpress.XtraGauges.Win.Gauges.Circular;
using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.DataControls.FlatDashboard;
using Micromind.DataControls.Libraries;
using Micromind.DataControls.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CustomGadgetGauge : UserControl, IGadget, ICustomGadget
	{
		private GadgetFilterOptions filterOption;

		private CustomGadgetData gadgetData;

		private string gadgetTitle = "Custom Gadget";

		private bool isDataLoaded;

		private bool isRefresh;

		private bool allowDateFilter = true;

		private bool hasAccess = true;

		private bool autoRefresh;

		private int refreshInterval = 300;

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

		private System.Windows.Forms.Label labelPeriod;

		private GadgetDateRangeComboBox comboBoxPeriod;

		private GaugeControl gaugeControl1;

		private CircularGauge circularGauge1;

		private ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent1;

		private ArcScaleComponent arcScaleComponent1;

		private ArcScaleNeedleComponent arcScaleNeedleComponent1;

		private ArcScaleSpindleCapComponent arcScaleSpindleCapComponent1;

		private Timer timerMain;

		private LabelComponent labelComponent1;

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
				return ViewType.Bar;
			}
			set
			{
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
					panel1.Visible = true;
				}
				else
				{
					panel1.Visible = false;
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

		public GroupLayout GroupLayout
		{
			get;
			set;
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

		public event EventHandler DataLoadCompleted;

		public CustomGadgetGauge()
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
			gaugeControl1.Visible = false;
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
					string formatString = "{V}%";
					bool flag = false;
					if (gadgetData != null)
					{
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
						if (gadgetData.Tables[0].Rows[0]["GShowGaugeText"] != DBNull.Value)
						{
							bool flag3 = bool.Parse(gadgetData.Tables[0].Rows[0]["GShowGaugeText"].ToString());
							arcScaleComponent1.MajorTickmark.ShowTick = flag3;
							arcScaleComponent1.MajorTickmark.ShowText = flag3;
						}
						if (gadgetData.Tables[0].Rows[0]["GShowNeedle"] != DBNull.Value && !bool.Parse(gadgetData.Tables[0].Rows[0]["GShowNeedle"].ToString()))
						{
							circularGauge1.Needles.Clear();
							labelComponent1.Position = new PointF2D(labelComponent1.Position.X, 155f);
						}
						if (gadgetData.Tables[0].Rows[0]["AxisYTextPattern"] != DBNull.Value)
						{
							formatString = gadgetData.Tables[0].Rows[0]["AxisYTextPattern"].ToString();
						}
						if (gadgetData.Tables[0].Rows[0]["ShowLegend"] != DBNull.Value && !bool.Parse(gadgetData.Tables[0].Rows[0]["ShowLegend"].ToString()))
						{
							circularGauge1.Labels.Clear();
						}
						if (gadgetData.Tables[0].Rows[0]["TopNOption"] != DBNull.Value)
						{
							flag = bool.Parse(gadgetData.Tables[0].Rows[0]["TopNOption"].ToString());
						}
					}
					DataSet dataSet = e.Result as DataSet;
					if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count >= 1)
					{
						DataTable dataTable = dataSet.Tables[0];
						DataTable gaugeRangeTable = gadgetData.GaugeRangeTable;
						DefaultPalette defaultPalette = new DefaultPalette();
						float num = 0f;
						if (dataTable.Columns.Contains(GroupLayout.ValueColumn) && circularGauge1.Labels.Count > 0)
						{
							num = float.Parse(dataTable.Rows[0][GroupLayout.ValueColumn].ToString());
						}
						_ = dataTable.Rows[0];
						arcScaleComponent1.Ranges.Clear();
						if (gaugeRangeTable != null)
						{
							float num2 = 0f;
							float num3 = 0f;
							int num4 = 0;
							foreach (DataRow row in gaugeRangeTable.Rows)
							{
								ArcScaleRange arcScaleRange = new ArcScaleRange
								{
									StartThickness = 30f,
									EndThickness = 30f,
									ShapeOffset = 7f
								};
								float num5 = float.Parse(row["StartValue"].ToString());
								float num6 = float.Parse(row["EndValue"].ToString());
								arcScaleRange.StartValue = num5;
								arcScaleRange.EndValue = num6;
								if (num4 == 0 && flag)
								{
									arcScaleRange.EndValue = num;
								}
								else if (num4 == 1 && flag)
								{
									arcScaleRange.StartValue = num;
								}
								if (num5 < num2)
								{
									num2 = num5;
								}
								if (num6 > num3)
								{
									num3 = num6;
								}
								int result = 141;
								int.TryParse(row["RangeColor"].ToString(), out result);
								Color brushColor = (!Micromind.ClientLibraries.ExtensionMethods.IsDBNullOrEmpty(row["RangeColor"])) ? Color.FromArgb(result) : ((defaultPalette.Count <= num4) ? defaultPalette[0].Color : defaultPalette[num4].Color);
								arcScaleRange.AppearanceRange.ContentBrush = new SolidBrushObject(brushColor);
								arcScaleComponent1.Ranges.Add(arcScaleRange);
								num4++;
							}
							arcScaleComponent1.MinValue = num2;
							arcScaleComponent1.MaxValue = num3;
							if (circularGauge1.Labels.Count > 0)
							{
								arcScaleComponent1.Value = num;
								circularGauge1.Labels[0].FormatString = formatString;
								circularGauge1.Labels[0].Text = num.ToString();
							}
						}
						gaugeControl1.Visible = true;
					}
					else
					{
						arcScaleComponent1.Value = 0f;
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
				gaugeControl1.Visible = true;
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
					sqlCommand.Parameters.AddWithValue("@ToDate", CommonLib.ToSqlDateTimeString(comboBoxPeriod.ToDate));
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
			Micromind.DataControls.Libraries.ComboData comboData = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData2 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData3 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData4 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData5 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData6 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData7 = new Micromind.DataControls.Libraries.ComboData();
			Micromind.DataControls.Libraries.ComboData comboData8 = new Micromind.DataControls.Libraries.ComboData();
			DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
			DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange2 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
			DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange3 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
			backgroundWorker = new System.ComponentModel.BackgroundWorker();
			pictureBoxWait = new System.Windows.Forms.PictureBox();
			panel1 = new System.Windows.Forms.Panel();
			labelPeriod = new System.Windows.Forms.Label();
			comboBoxPeriod = new Micromind.DataControls.GadgetDateRangeComboBox();
			imageList1 = new System.Windows.Forms.ImageList();
			gaugeControl1 = new DevExpress.XtraGauges.Win.GaugeControl();
			circularGauge1 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
			arcScaleBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
			arcScaleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
			labelComponent1 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
			arcScaleNeedleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
			arcScaleSpindleCapComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent();
			timerMain = new System.Windows.Forms.Timer();
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).BeginInit();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPeriod.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)circularGauge1).BeginInit();
			((System.ComponentModel.ISupportInitialize)arcScaleBackgroundLayerComponent1).BeginInit();
			((System.ComponentModel.ISupportInitialize)arcScaleComponent1).BeginInit();
			((System.ComponentModel.ISupportInitialize)labelComponent1).BeginInit();
			((System.ComponentModel.ISupportInitialize)arcScaleNeedleComponent1).BeginInit();
			((System.ComponentModel.ISupportInitialize)arcScaleSpindleCapComponent1).BeginInit();
			SuspendLayout();
			backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker_DoWork);
			pictureBoxWait.Anchor = System.Windows.Forms.AnchorStyles.None;
			pictureBoxWait.BackColor = System.Drawing.Color.White;
			pictureBoxWait.Image = Micromind.DataControls.Properties.Resources.wait;
			pictureBoxWait.Location = new System.Drawing.Point(161, 107);
			pictureBoxWait.Name = "pictureBoxWait";
			pictureBoxWait.Size = new System.Drawing.Size(32, 32);
			pictureBoxWait.TabIndex = 10;
			pictureBoxWait.TabStop = false;
			panel1.BackColor = System.Drawing.Color.Transparent;
			panel1.Controls.Add(labelPeriod);
			panel1.Controls.Add(comboBoxPeriod);
			panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel1.Location = new System.Drawing.Point(0, 250);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(366, 24);
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
			gaugeControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			gaugeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			gaugeControl1.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[1]
			{
				circularGauge1
			});
			gaugeControl1.Location = new System.Drawing.Point(0, 0);
			gaugeControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
			gaugeControl1.LookAndFeel.UseDefaultLookAndFeel = false;
			gaugeControl1.Name = "gaugeControl1";
			gaugeControl1.Size = new System.Drawing.Size(366, 250);
			gaugeControl1.TabIndex = 14;
			circularGauge1.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[1]
			{
				arcScaleBackgroundLayerComponent1
			});
			circularGauge1.Bounds = new System.Drawing.Rectangle(6, 6, 354, 238);
			circularGauge1.Labels.AddRange(new DevExpress.XtraGauges.Win.Base.LabelComponent[1]
			{
				labelComponent1
			});
			circularGauge1.Name = "circularGauge1";
			circularGauge1.Needles.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent[1]
			{
				arcScaleNeedleComponent1
			});
			circularGauge1.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[1]
			{
				arcScaleComponent1
			});
			circularGauge1.SpindleCaps.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent[1]
			{
				arcScaleSpindleCapComponent1
			});
			arcScaleBackgroundLayerComponent1.ArcScale = arcScaleComponent1;
			arcScaleBackgroundLayerComponent1.Name = "bg";
			arcScaleBackgroundLayerComponent1.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5f, 0.99f);
			arcScaleBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularHalf_Style27;
			arcScaleBackgroundLayerComponent1.Size = new System.Drawing.SizeF(200f, 102f);
			arcScaleBackgroundLayerComponent1.ZOrder = 1000;
			arcScaleComponent1.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent1.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent1.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent1.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent1.AppearanceScale.Brush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#3A3832");
			arcScaleComponent1.AppearanceScale.Width = 1.5f;
			arcScaleComponent1.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 10f);
			arcScaleComponent1.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#999999");
			arcScaleComponent1.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 159f);
			arcScaleComponent1.EndAngle = 0f;
			arcScaleComponent1.MajorTickmark.AllowTickOverlap = true;
			arcScaleComponent1.MajorTickmark.FormatString = "{0:F0}";
			arcScaleComponent1.MajorTickmark.ShapeOffset = -2f;
			arcScaleComponent1.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
			arcScaleComponent1.MajorTickmark.TextOffset = 18f;
			arcScaleComponent1.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
			arcScaleComponent1.MaxValue = 100f;
			arcScaleComponent1.MinorTickCount = 4;
			arcScaleComponent1.MinorTickmark.ShapeOffset = 4f;
			arcScaleComponent1.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
			arcScaleComponent1.MinorTickmark.ShowTick = false;
			arcScaleComponent1.Name = "scale1";
			arcScaleComponent1.RadiusX = 101f;
			arcScaleComponent1.RadiusY = 101f;
			arcScaleRange.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
			arcScaleRange.EndThickness = 22f;
			arcScaleRange.EndValue = 33f;
			arcScaleRange.Name = "Range0";
			arcScaleRange.ShapeOffset = -4f;
			arcScaleRange.StartThickness = 22f;
			arcScaleRange2.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
			arcScaleRange2.EndThickness = 22f;
			arcScaleRange2.EndValue = 66f;
			arcScaleRange2.Name = "Range1";
			arcScaleRange2.ShapeOffset = -4f;
			arcScaleRange2.StartThickness = 22f;
			arcScaleRange2.StartValue = 33f;
			arcScaleRange3.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
			arcScaleRange3.EndThickness = 22f;
			arcScaleRange3.EndValue = 100f;
			arcScaleRange3.Name = "Range2";
			arcScaleRange3.ShapeOffset = -4f;
			arcScaleRange3.StartThickness = 22f;
			arcScaleRange3.StartValue = 66f;
			arcScaleComponent1.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[3]
			{
				arcScaleRange,
				arcScaleRange2,
				arcScaleRange3
			});
			arcScaleComponent1.StartAngle = -180f;
			arcScaleComponent1.Value = 80f;
			labelComponent1.AppearanceText.Font = new System.Drawing.Font("Tahoma", 20f);
			labelComponent1.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
			labelComponent1.FormatString = "{0}%";
			labelComponent1.Name = "circularGauge1_Label1";
			labelComponent1.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 190f);
			labelComponent1.Text = "0";
			labelComponent1.ZOrder = -1001;
			arcScaleNeedleComponent1.ArcScale = arcScaleComponent1;
			arcScaleNeedleComponent1.EndOffset = 8f;
			arcScaleNeedleComponent1.Name = "needle";
			arcScaleNeedleComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style27;
			arcScaleNeedleComponent1.StartOffset = -6f;
			arcScaleNeedleComponent1.ZOrder = -50;
			arcScaleSpindleCapComponent1.ArcScale = arcScaleComponent1;
			arcScaleSpindleCapComponent1.Name = "circularGauge5_SpindleCap1";
			arcScaleSpindleCapComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.SpindleCapShapeType.Empty;
			arcScaleSpindleCapComponent1.Size = new System.Drawing.SizeF(10f, 10f);
			arcScaleSpindleCapComponent1.ZOrder = -100;
			timerMain.Tick += new System.EventHandler(timerMain_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(gaugeControl1);
			base.Controls.Add(panel1);
			base.Controls.Add(pictureBoxWait);
			base.Name = "CustomGadgetGauge";
			base.Size = new System.Drawing.Size(366, 274);
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).EndInit();
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPeriod.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)circularGauge1).EndInit();
			((System.ComponentModel.ISupportInitialize)arcScaleBackgroundLayerComponent1).EndInit();
			((System.ComponentModel.ISupportInitialize)arcScaleComponent1).EndInit();
			((System.ComponentModel.ISupportInitialize)labelComponent1).EndInit();
			((System.ComponentModel.ISupportInitialize)arcScaleNeedleComponent1).EndInit();
			((System.ComponentModel.ISupportInitialize)arcScaleSpindleCapComponent1).EndInit();
			ResumeLayout(false);
		}
	}
}
