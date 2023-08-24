using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ChartTypesCombo : ImageComboBoxEdit
	{
		private IContainer components;

		public ViewType SelectedType
		{
			get
			{
				if (SelectedItem == null)
				{
					return ViewType.Bar;
				}
				return (ViewType)Enum.Parse(typeof(ViewType), SelectedItem.ToString());
			}
			set
			{
				int num2 = SelectedIndex = FindItem(value.ToString(), 0);
			}
		}

		public ChartTypesCombo()
		{
			InitializeComponent();
		}

		public ChartTypesCombo(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
		}

		public void LoadChartTypes()
		{
			base.Properties.Items.Clear();
			Enum.GetNames(typeof(ViewType));
			ImageList imageList = new ImageList();
			imageList.ImageSize = new Size(32, 32);
			ImageList imageList2 = new ImageList();
			imageList.Images.AddRange(SeriesViewFactory.SeriesViewImages);
			imageList2.Images.AddRange(SeriesViewFactory.SeriesViewImages);
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Bar, 0));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.StackedBar, 1));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.FullStackedBar, 2));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.SideBySideRangeBar, 3));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.SideBySideFullStackedBar, 4));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Bar3D, 5));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.StackedBar3D, 6));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.ManhattanBar, 10));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Point, 11));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Bubble, 12));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Line, 13));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Line3D, 20));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Pie, 25));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Doughnut, 26));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Pie3D, 27));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Doughnut3D, 28));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Funnel, 29));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Funnel3D, 30));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.Area, 31));
			base.Properties.Items.Add(new ImageComboBoxItem(ViewType.StackedArea, 32));
			base.Properties.LargeImages = imageList;
			base.Properties.SmallImages = imageList2;
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
		}
	}
}
