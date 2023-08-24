using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGauges.Base;
using DevExpress.XtraGauges.Core.Base;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGauges.Core.Primitive;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGauges.Win.Gauges.State;
using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.DataControls.FlatDashboard;
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
	public class CustomGadgetNumeric : UserControl, IGadget, ICustomGadget
	{
		private static Color positiveColor = Color.FromArgb(70, 158, 165);

		private static Color negativeColor = Color.FromArgb(165, 70, 113);

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

		private SizeableLabel labelMain;

		private Panel panel2;

		private TableLayoutPanel tableLayoutPanel1;

		private Panel panelIndicator;

		public LabelControl labelIndicator;

		private GaugeControl gaugeControl1;

		private StateIndicatorComponent stateIndicatorComponent1;

		private BackgroundWorker backgroundWorker;

		private ImageList imageList1;

		private Timer timerMain;

		private PictureBox pictureBoxWait;

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

		public CustomGadgetNumeric()
		{
			InitializeComponent();
			try
			{
				isInit = true;
				base.SizeChanged += FavoriteBankAccountsGadget_SizeChanged;
				base.ClientSizeChanged += FavoriteBankAccountsGadget_ClientSizeChanged;
				backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
				backgroundWorker.DoWork += backgroundWorker_DoWork;
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

		private void OnCustomDrawElement(object sender, CustomDrawElementEventArgs e)
		{
			e.Handled = true;
			StateIndicatorComponent obj = sender as StateIndicatorComponent;
			Point empty = Point.Empty;
			Point empty2 = Point.Empty;
			Point empty3 = Point.Empty;
			int num = (int)((double)e.Info.BoundBox.Width * Math.Sqrt(3.0) / 2.0);
			empty.X = (int)(e.Info.BoundBox.Width / 2f);
			empty2.X = (int)e.Info.BoundBox.Left;
			empty3.X = (int)e.Info.BoundBox.Right;
			int num2 = (int)((e.Info.BoundBox.Height - (float)num) / 2f);
			Color empty4 = Color.Empty;
			if (obj.StateIndex == 0)
			{
				empty.Y = num2;
				int num5 = empty2.Y = (empty3.Y = num2 + num);
				empty4 = positiveColor;
				labelIndicator.ForeColor = positiveColor;
			}
			else
			{
				empty.Y = num2 + num;
				int num5 = empty2.Y = (empty3.Y = num2);
				empty4 = negativeColor;
				labelIndicator.ForeColor = negativeColor;
			}
			e.Context.Graphics.FillPolygon(new SolidBrush(empty4), new Point[3]
			{
				empty,
				empty2,
				empty3
			});
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
					string text = "";
					string format = "";
					if (gadgetData != null)
					{
						DataRow dataRow = gadgetData.Tables[0].Rows[0];
						if (dataRow["AutoRefresh"] != DBNull.Value)
						{
							autoRefresh = bool.Parse(gadgetData.Tables[0].Rows[0]["AutoRefresh"].ToString());
						}
						if (dataRow["RefreshInterval"] != DBNull.Value)
						{
							refreshInterval = int.Parse(dataRow["RefreshInterval"].ToString());
						}
						if (autoRefresh)
						{
							timerMain.Interval = refreshInterval * 1000;
							Timer timer = timerMain;
							bool enabled = base.Enabled = true;
							timer.Enabled = enabled;
						}
						if (dataRow["TextColor"] != DBNull.Value)
						{
							labelMain.ForeColor = Color.FromArgb(int.Parse(dataRow["TextColor"].ToString()));
						}
						if (dataRow["IndValueColumn"] != DBNull.Value)
						{
							gaugeControl1.Tag = dataRow["IndValueColumn"].ToString();
						}
						else
						{
							gaugeControl1.Tag = null;
						}
						if (dataRow["IndTextValueColumn"] != DBNull.Value)
						{
							labelIndicator.Tag = dataRow["IndTextValueColumn"].ToString();
						}
						else
						{
							labelIndicator.Tag = null;
						}
						text = dataRow["AxisYTextPattern"].ToString();
						format = dataRow["LegendTextPattern"].ToString();
						if (dataRow["ShowIndicator"] != DBNull.Value)
						{
							gaugeControl1.Visible = bool.Parse(dataRow["ShowIndicator"].ToString());
						}
						else
						{
							gaugeControl1.Visible = false;
						}
						if (dataRow["ShowName"] != DBNull.Value)
						{
							labelIndicator.Visible = bool.Parse(dataRow["ShowName"].ToString());
						}
						else
						{
							labelIndicator.Visible = false;
						}
					}
					DataSet dataSet = e.Result as DataSet;
					if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count >= 1)
					{
						DataTable dataTable = dataSet.Tables[0];
						if (dataTable.TableName == "Ranges")
						{
							dataTable = dataSet.Tables[1];
						}
						_ = dataTable.Rows[0];
						if (dataTable.Columns.Contains(GroupLayout.ValueColumn))
						{
							string s = dataTable.Rows[0][GroupLayout.ValueColumn].ToString();
							decimal result = default(decimal);
							decimal.TryParse(s, out result);
							if (text.IsNullOrEmpty())
							{
								text = ((result >= 1000000000m) ? Format.NumberFormatB : ((result >= 1000000m) ? Format.NumberFormatM : ((!(result >= 10000m)) ? Format.NumberFormat : Format.NumberFormatK)));
							}
							labelMain.Text = result.ToString(text);
						}
						if (!gaugeControl1.Tag.IsNullOrEmpty() && dataTable.Columns.Contains(gaugeControl1.Tag.ToString()))
						{
							stateIndicatorComponent1.StateIndex = int.Parse(dataTable.Rows[0][gaugeControl1.Tag.ToString()].ToString());
						}
						if (!labelIndicator.Tag.IsNullOrEmpty() && dataTable.Columns.Contains(labelIndicator.Tag.ToString()))
						{
							string text2 = dataTable.Rows[0][labelIndicator.Tag.ToString()].ToString();
							decimal result2 = default(decimal);
							if (decimal.TryParse(text2, out result2))
							{
								labelIndicator.Text = result2.ToString(format);
							}
							else
							{
								labelIndicator.Text = text2;
							}
						}
						if (!gaugeControl1.Visible && !labelIndicator.Visible)
						{
							tableLayoutPanel1.RowCount = 1;
							panelIndicator.Visible = false;
						}
					}
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
				CustomGadgetData customGadgetData = gadgetData = Factory.CustomGadgetSystemAsync.GetCustomGadgetByID(GadgetID);
				if (customGadgetData != null && customGadgetData.CustomGadgetTable.Rows.Count != 0)
				{
					CustomGadget obj = (CustomGadget)Global.DeserializeFromStream((byte[])customGadgetData.CustomGadgetTable.Rows[0]["GadgetData"]);
					SqlCommand sqlCommand = new SqlCommand();
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
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState2 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState3 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState4 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState5 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState6 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState7 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState8 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState9 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState10 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			panel2 = new System.Windows.Forms.Panel();
			pictureBoxWait = new System.Windows.Forms.PictureBox();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			panelIndicator = new System.Windows.Forms.Panel();
			labelIndicator = new DevExpress.XtraEditors.LabelControl();
			gaugeControl1 = new DevExpress.XtraGauges.Win.GaugeControl();
			stateIndicatorComponent1 = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent();
			backgroundWorker = new System.ComponentModel.BackgroundWorker();
			imageList1 = new System.Windows.Forms.ImageList();
			timerMain = new System.Windows.Forms.Timer();
			labelMain = new Micromind.DataControls.SizeableLabel();
			DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge stateIndicatorGauge = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).BeginInit();
			tableLayoutPanel1.SuspendLayout();
			panelIndicator.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)stateIndicatorComponent1).BeginInit();
			((System.ComponentModel.ISupportInitialize)stateIndicatorGauge).BeginInit();
			SuspendLayout();
			panel2.Controls.Add(pictureBoxWait);
			panel2.Controls.Add(labelMain);
			panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			panel2.Location = new System.Drawing.Point(3, 3);
			panel2.Name = "panel2";
			panel2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
			panel2.Size = new System.Drawing.Size(174, 91);
			panel2.TabIndex = 1;
			pictureBoxWait.Anchor = System.Windows.Forms.AnchorStyles.None;
			pictureBoxWait.BackColor = System.Drawing.Color.White;
			pictureBoxWait.Image = Micromind.DataControls.Properties.Resources.wait;
			pictureBoxWait.Location = new System.Drawing.Point(65, 38);
			pictureBoxWait.Name = "pictureBoxWait";
			pictureBoxWait.Size = new System.Drawing.Size(32, 32);
			pictureBoxWait.TabIndex = 22;
			pictureBoxWait.TabStop = false;
			pictureBoxWait.Visible = false;
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.63636f));
			tableLayoutPanel1.Controls.Add(panelIndicator, 0, 1);
			tableLayoutPanel1.Controls.Add(panel2, 0, 0);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 2;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
			tableLayoutPanel1.Size = new System.Drawing.Size(180, 132);
			tableLayoutPanel1.TabIndex = 21;
			panelIndicator.Controls.Add(labelIndicator);
			panelIndicator.Controls.Add(gaugeControl1);
			panelIndicator.Dock = System.Windows.Forms.DockStyle.Fill;
			panelIndicator.Location = new System.Drawing.Point(3, 100);
			panelIndicator.Name = "panelIndicator";
			panelIndicator.Size = new System.Drawing.Size(174, 29);
			panelIndicator.TabIndex = 0;
			labelIndicator.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelIndicator.Appearance.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelIndicator.Appearance.Options.UseFont = true;
			labelIndicator.Appearance.Options.UseTextOptions = true;
			labelIndicator.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Word;
			labelIndicator.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			labelIndicator.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			labelIndicator.Location = new System.Drawing.Point(41, 1);
			labelIndicator.Name = "labelIndicator";
			labelIndicator.Size = new System.Drawing.Size(115, 25);
			labelIndicator.TabIndex = 18;
			labelIndicator.Text = "Name";
			labelIndicator.Visible = false;
			gaugeControl1.AutoLayout = false;
			gaugeControl1.BackColor = System.Drawing.Color.Transparent;
			gaugeControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			gaugeControl1.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[1]
			{
				stateIndicatorGauge
			});
			gaugeControl1.LayoutInterval = 0;
			gaugeControl1.LayoutPadding = new DevExpress.XtraGauges.Core.Base.Thickness(0);
			gaugeControl1.Location = new System.Drawing.Point(3, -11);
			gaugeControl1.Name = "gaugeControl1";
			gaugeControl1.Size = new System.Drawing.Size(35, 45);
			gaugeControl1.TabIndex = 17;
			stateIndicatorComponent1.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(70f, 124f);
			stateIndicatorComponent1.Name = "stateIndicatorComponent2";
			stateIndicatorComponent1.Size = new System.Drawing.SizeF(100f, 100f);
			stateIndicatorComponent1.StateIndex = 0;
			indicatorState.Name = "State1";
			indicatorState.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow1;
			indicatorState2.Name = "State2";
			indicatorState2.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow2;
			indicatorState3.Name = "State3";
			indicatorState3.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow3;
			indicatorState4.Name = "State4";
			indicatorState4.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow4;
			indicatorState5.Name = "State5";
			indicatorState5.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow5;
			indicatorState6.Name = "State6";
			indicatorState6.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow6;
			indicatorState7.Name = "State7";
			indicatorState7.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow7;
			indicatorState8.Name = "State8";
			indicatorState8.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow8;
			indicatorState9.Name = "State9";
			indicatorState9.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow9;
			indicatorState10.Name = "State10";
			indicatorState10.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow10;
			stateIndicatorComponent1.States.AddRange(new DevExpress.XtraGauges.Core.Model.IIndicatorState[10]
			{
				indicatorState,
				indicatorState2,
				indicatorState3,
				indicatorState4,
				indicatorState5,
				indicatorState6,
				indicatorState7,
				indicatorState8,
				indicatorState9,
				indicatorState10
			});
			stateIndicatorComponent1.CustomDrawElement += new DevExpress.XtraGauges.Core.Primitive.CustomDrawElementEventHandler(OnCustomDrawElement);
			imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			imageList1.ImageSize = new System.Drawing.Size(48, 48);
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			stateIndicatorGauge.Bounds = new System.Drawing.Rectangle(0, 0, 50, 50);
			stateIndicatorGauge.Indicators.AddRange(new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent[1]
			{
				stateIndicatorComponent1
			});
			stateIndicatorGauge.Name = "stateIndicatorGauge1";
			labelMain.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelMain.AutoEllipsis = true;
			labelMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 54f);
			labelMain.Location = new System.Drawing.Point(3, 0);
			labelMain.Name = "labelMain";
			labelMain.Size = new System.Drawing.Size(171, 94);
			labelMain.TabIndex = 21;
			labelMain.Text = "0";
			labelMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(tableLayoutPanel1);
			base.Name = "CustomGadgetNumeric";
			base.Size = new System.Drawing.Size(180, 132);
			panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)pictureBoxWait).EndInit();
			tableLayoutPanel1.ResumeLayout(false);
			panelIndicator.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)stateIndicatorComponent1).EndInit();
			((System.ComponentModel.ISupportInitialize)stateIndicatorGauge).EndInit();
			ResumeLayout(false);
		}
	}
}
