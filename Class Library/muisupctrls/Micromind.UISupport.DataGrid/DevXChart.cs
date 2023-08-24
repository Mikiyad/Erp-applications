using DevExpress.Utils;
using DevExpress.XtraCharts;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace Micromind.UISupport.DataGrid
{
	public class DevXChart : ChartControl
	{
		private IContainer components;

		public float FontSize
		{
			set
			{
				try
				{
					XYDiagram xYDiagram = base.Diagram as XYDiagram;
					if (xYDiagram != null)
					{
						xYDiagram.AxisY.NumericOptions.Format = NumericFormat.Number;
						xYDiagram.AxisX.Label.Font = new Font("Segoe UI", value);
					}
				}
				catch (Exception)
				{
				}
			}
		}

		public string CrossHairPattern
		{
			get
			{
				if (base.Series.Count > 0)
				{
					return base.Series[0].CrosshairLabelPattern;
				}
				return "";
			}
			set
			{
				if (base.Series.Count > 0)
				{
					base.Series[0].CrosshairLabelPattern = value;
				}
			}
		}

		public DevXChart()
		{
			InitializeComponent();
		}

		public DevXChart(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		public void SetData(DataTable dataSource, string valueMember, string argumentMember)
		{
			try
			{
				if (base.Series.Count != 0)
				{
					if (dataSource != null && dataSource.Rows.Count >= 1)
					{
						Series series = base.Series[0];
						series.DataSource = dataSource;
						series.ArgumentScaleType = ScaleType.Qualitative;
						series.ArgumentDataMember = argumentMember;
						series.ValueScaleType = ScaleType.Numerical;
						series.ValueDataMembers.AddRange(valueMember);
						base.ToolTipEnabled = DefaultBoolean.True;
						series.Label.TextColor = Color.White;
						series.Label.BackColor = Color.Transparent;
						series.Label.Antialiasing = true;
						series.ToolTipPointPattern = "{A}: {V:#,##0} ({VP:##0.00%})";
						if (series.View.GetType().BaseType == typeof(PieSeriesViewBase) || series.View.GetType().BaseType == typeof(PieSeriesView))
						{
							_ = base.Diagram;
							series.Label.PointOptions.PointView = PointView.Argument;
							((PiePointOptions)series.Label.PointOptions).PercentOptions.ValueAsPercent = false;
							((PieSeriesLabel)series.Label).Position = PieSeriesLabelPosition.Inside;
							series.Label.PointOptions.PointView = PointView.Argument;
						}
						else if (series.View.GetType() == typeof(FunnelSeriesView))
						{
							((FunnelPointOptions)series.Label.PointOptions).PercentOptions.ValueAsPercent = false;
							((FunnelSeriesLabel)series.Label).Position = FunnelSeriesLabelPosition.Center;
							series.Label.PointOptions.PointView = PointView.Argument;
						}
						else
						{
							XYDiagram xYDiagram = base.Diagram as XYDiagram;
							if (xYDiagram != null)
							{
								xYDiagram.AxisY.NumericOptions.Format = NumericFormat.Number;
								xYDiagram.AxisX.Label.MaxWidth = 75;
								((XYDiagram)base.Diagram).AxisY.Visible = false;
							}
						}
						PointOptions pointOptions = base.Series[0].Label.PointOptions;
						pointOptions.ArgumentNumericOptions.Format = NumericFormat.Number;
						pointOptions.ArgumentNumericOptions.Precision = 0;
						pointOptions.ValueNumericOptions.Format = NumericFormat.Number;
						pointOptions.ValueNumericOptions.Precision = 0;
						base.Legend.Visible = false;
					}
					else
					{
						base.Series[0].DataSource = null;
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void Clear()
		{
			if (base.Series.Count > 0)
			{
				base.Series[0].DataSource = null;
			}
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
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			((System.ComponentModel.ISupportInitialize)this).BeginInit();
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).BeginInit();
			SuspendLayout();
			base.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
			sideBySideBarSeriesLabel.LineVisible = true;
			base.SeriesTemplate.Label = sideBySideBarSeriesLabel;
			((System.ComponentModel.ISupportInitialize)sideBySideBarSeriesLabel).EndInit();
			((System.ComponentModel.ISupportInitialize)this).EndInit();
			ResumeLayout(false);
		}
	}
}
