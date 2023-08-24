using DevExpress.XtraGauges.Core.Base;
using DevExpress.XtraGauges.Core.Drawing;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGauges.Win.Base;
using DevExpress.XtraGauges.Win.Gauges.Circular;
using DevExpress.XtraTreeList;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Vendors;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Test
{
	public class Test2 : Form
	{
		private PropertyGrid propertyGrid1;

		private ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent1;

		private ArcScaleNeedleComponent arcScaleNeedleComponent1;

		private ArcScaleComponent arcScaleComponent2;

		private ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent2;

		private ArcScaleComponent arcScaleComponent3;

		private LabelComponent labelComponent2;

		private ArcScaleNeedleComponent arcScaleNeedleComponent2;

		private UltraPopupControlContainer ultraPopupControlContainer1;

		private ProductComboBox productComboBox1;

		private TextBox textBox1;

		private IContainer components;

		public Test2()
		{
			InitializeComponent();
			base.Tag = textBox1;
			textBox1.WordWrap = false;
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
			DevExpress.XtraGauges.Core.Model.ScaleLabel scaleLabel = new DevExpress.XtraGauges.Core.Model.ScaleLabel();
			DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
			DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange2 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
			DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange3 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
			DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange4 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
			DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange5 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
			DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange6 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			arcScaleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
			arcScaleBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
			arcScaleNeedleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
			arcScaleComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
			arcScaleBackgroundLayerComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
			arcScaleNeedleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
			labelComponent2 = new DevExpress.XtraGauges.Win.Base.LabelComponent();
			propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			productComboBox1 = new Micromind.DataControls.ProductComboBox();
			ultraPopupControlContainer1 = new Infragistics.Win.Misc.UltraPopupControlContainer(components);
			textBox1 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)arcScaleComponent2).BeginInit();
			((System.ComponentModel.ISupportInitialize)arcScaleBackgroundLayerComponent1).BeginInit();
			((System.ComponentModel.ISupportInitialize)arcScaleNeedleComponent1).BeginInit();
			((System.ComponentModel.ISupportInitialize)arcScaleComponent3).BeginInit();
			((System.ComponentModel.ISupportInitialize)arcScaleBackgroundLayerComponent2).BeginInit();
			((System.ComponentModel.ISupportInitialize)arcScaleNeedleComponent2).BeginInit();
			((System.ComponentModel.ISupportInitialize)labelComponent2).BeginInit();
			((System.ComponentModel.ISupportInitialize)productComboBox1).BeginInit();
			SuspendLayout();
			arcScaleComponent2.AcceptOrder = 0;
			arcScaleComponent2.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent2.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent2.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent2.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent2.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 10f);
			arcScaleComponent2.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#999999");
			arcScaleComponent2.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 159f);
			arcScaleComponent2.EndAngle = 0f;
			scaleLabel.Name = "Label0";
			scaleLabel.Text = "Value:332";
			arcScaleComponent2.Labels.AddRange(new DevExpress.XtraGauges.Core.Model.ILabel[1]
			{
				scaleLabel
			});
			arcScaleComponent2.MajorTickCount = 6;
			arcScaleComponent2.MajorTickmark.AllowTickOverlap = true;
			arcScaleComponent2.MajorTickmark.FormatString = "{0:F0}";
			arcScaleComponent2.MajorTickmark.ShapeOffset = -2f;
			arcScaleComponent2.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
			arcScaleComponent2.MajorTickmark.TextOffset = 18f;
			arcScaleComponent2.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
			arcScaleComponent2.MaxValue = 100f;
			arcScaleComponent2.MinorTickCount = 4;
			arcScaleComponent2.MinorTickmark.ShapeOffset = 4f;
			arcScaleComponent2.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
			arcScaleComponent2.MinorTickmark.ShowTick = false;
			arcScaleComponent2.Name = "scale1";
			arcScaleComponent2.RadiusX = 101f;
			arcScaleComponent2.RadiusY = 101f;
			arcScaleRange.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
			arcScaleRange.EndThickness = 22f;
			arcScaleRange.EndValue = 33f;
			arcScaleRange.Name = "Range0";
			arcScaleRange.ShapeOffset = -4f;
			arcScaleRange.StartThickness = 22f;
			arcScaleRange.StartValue = -0.3f;
			arcScaleRange2.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
			arcScaleRange2.EndThickness = 22f;
			arcScaleRange2.EndValue = 66f;
			arcScaleRange2.Name = "Range1";
			arcScaleRange2.ShapeOffset = -4f;
			arcScaleRange2.StartThickness = 22f;
			arcScaleRange2.StartValue = 33f;
			arcScaleRange3.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
			arcScaleRange3.EndThickness = 22f;
			arcScaleRange3.EndValue = 100.3f;
			arcScaleRange3.Name = "Range2";
			arcScaleRange3.ShapeOffset = -4f;
			arcScaleRange3.StartThickness = 22f;
			arcScaleRange3.StartValue = 66f;
			arcScaleComponent2.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[3]
			{
				arcScaleRange,
				arcScaleRange2,
				arcScaleRange3
			});
			arcScaleComponent2.StartAngle = -180f;
			arcScaleComponent2.Value = 80f;
			arcScaleBackgroundLayerComponent1.AcceptOrder = -1000;
			arcScaleBackgroundLayerComponent1.Name = "bg";
			arcScaleBackgroundLayerComponent1.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5f, 0.99f);
			arcScaleBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularHalf_Style27;
			arcScaleBackgroundLayerComponent1.Size = new System.Drawing.SizeF(200f, 102f);
			arcScaleBackgroundLayerComponent1.ZOrder = 1000;
			arcScaleNeedleComponent1.AcceptOrder = 50;
			arcScaleNeedleComponent1.EndOffset = 8f;
			arcScaleNeedleComponent1.Name = "needle";
			arcScaleNeedleComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style16;
			arcScaleNeedleComponent1.StartOffset = -6f;
			arcScaleNeedleComponent1.ZOrder = -50;
			arcScaleComponent3.AcceptOrder = 0;
			arcScaleComponent3.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent3.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent3.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent3.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
			arcScaleComponent3.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 10f);
			arcScaleComponent3.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#999999");
			arcScaleComponent3.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 159f);
			arcScaleComponent3.EndAngle = 0f;
			arcScaleComponent3.MajorTickCount = 6;
			arcScaleComponent3.MajorTickmark.AllowTickOverlap = true;
			arcScaleComponent3.MajorTickmark.FormatString = "{0:F0}";
			arcScaleComponent3.MajorTickmark.ShapeOffset = -2f;
			arcScaleComponent3.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
			arcScaleComponent3.MajorTickmark.TextOffset = 18f;
			arcScaleComponent3.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
			arcScaleComponent3.MaxValue = 100f;
			arcScaleComponent3.MinorTickCount = 4;
			arcScaleComponent3.MinorTickmark.ShapeOffset = 4f;
			arcScaleComponent3.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
			arcScaleComponent3.MinorTickmark.ShowTick = false;
			arcScaleComponent3.Name = "scale1";
			arcScaleComponent3.RadiusX = 101f;
			arcScaleComponent3.RadiusY = 101f;
			arcScaleRange4.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
			arcScaleRange4.EndThickness = 22f;
			arcScaleRange4.EndValue = 33f;
			arcScaleRange4.Name = "Range0";
			arcScaleRange4.ShapeOffset = -4f;
			arcScaleRange4.StartThickness = 22f;
			arcScaleRange4.StartValue = -0.3f;
			arcScaleRange5.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
			arcScaleRange5.EndThickness = 22f;
			arcScaleRange5.EndValue = 66f;
			arcScaleRange5.Name = "Range1";
			arcScaleRange5.ShapeOffset = -4f;
			arcScaleRange5.StartThickness = 22f;
			arcScaleRange5.StartValue = 33f;
			arcScaleRange6.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
			arcScaleRange6.EndThickness = 22f;
			arcScaleRange6.EndValue = 100.3f;
			arcScaleRange6.Name = "Range2";
			arcScaleRange6.ShapeOffset = -4f;
			arcScaleRange6.StartThickness = 22f;
			arcScaleRange6.StartValue = 66f;
			arcScaleComponent3.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[3]
			{
				arcScaleRange4,
				arcScaleRange5,
				arcScaleRange6
			});
			arcScaleComponent3.StartAngle = -180f;
			arcScaleComponent3.Value = 80f;
			arcScaleBackgroundLayerComponent2.AcceptOrder = -1000;
			arcScaleBackgroundLayerComponent2.ArcScale = arcScaleComponent3;
			arcScaleBackgroundLayerComponent2.Name = "bg";
			arcScaleBackgroundLayerComponent2.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5f, 0.99f);
			arcScaleBackgroundLayerComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularHalf_Style27;
			arcScaleBackgroundLayerComponent2.Size = new System.Drawing.SizeF(200f, 102f);
			arcScaleBackgroundLayerComponent2.ZOrder = 1000;
			arcScaleNeedleComponent2.AcceptOrder = 50;
			arcScaleNeedleComponent2.ArcScale = arcScaleComponent3;
			arcScaleNeedleComponent2.EndOffset = 8f;
			arcScaleNeedleComponent2.Name = "needle";
			arcScaleNeedleComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style27;
			arcScaleNeedleComponent2.StartOffset = -6f;
			arcScaleNeedleComponent2.ZOrder = -50;
			labelComponent2.AcceptOrder = -12;
			labelComponent2.AppearanceText.Font = new System.Drawing.Font("Tahoma", 30f);
			labelComponent2.Name = "labelComponent2";
			labelComponent2.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(125f, 220f);
			labelComponent2.Size = new System.Drawing.SizeF(200f, 40f);
			labelComponent2.Text = "43%";
			labelComponent2.ZOrder = 12;
			propertyGrid1.Dock = System.Windows.Forms.DockStyle.Right;
			propertyGrid1.Location = new System.Drawing.Point(505, 0);
			propertyGrid1.Name = "propertyGrid1";
			propertyGrid1.SelectedObject = productComboBox1;
			propertyGrid1.Size = new System.Drawing.Size(275, 521);
			propertyGrid1.TabIndex = 20;
			productComboBox1.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			productComboBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			productComboBox1.Assigned = false;
			productComboBox1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			productComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			productComboBox1.CustomReportFieldName = "";
			productComboBox1.CustomReportKey = "";
			productComboBox1.CustomReportValueType = 1;
			productComboBox1.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			productComboBox1.DisplayLayout.Appearance = appearance;
			productComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			productComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			productComboBox1.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			productComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			productComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			productComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			productComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			productComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			productComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			productComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			productComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			productComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			productComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			productComboBox1.DisplayLayout.Override.CellAppearance = appearance8;
			productComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			productComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			productComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			productComboBox1.DisplayLayout.Override.HeaderAppearance = appearance10;
			productComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			productComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			productComboBox1.DisplayLayout.Override.RowAppearance = appearance11;
			productComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			productComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			productComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			productComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			productComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			productComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			productComboBox1.Editable = true;
			productComboBox1.FilterCustomerID = "";
			productComboBox1.FilterString = "";
			productComboBox1.FilterSysDocID = "";
			productComboBox1.HasAllAccount = false;
			productComboBox1.HasCustom = false;
			productComboBox1.IsDataLoaded = false;
			productComboBox1.Location = new System.Drawing.Point(43, 406);
			productComboBox1.MaxDropDownItems = 12;
			productComboBox1.Name = "productComboBox1";
			productComboBox1.Show3PLItems = true;
			productComboBox1.ShowInactiveItems = false;
			productComboBox1.ShowQuickAdd = true;
			productComboBox1.Size = new System.Drawing.Size(282, 20);
			productComboBox1.TabIndex = 22;
			productComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBox1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBox1.Location = new System.Drawing.Point(43, 82);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBox1.Size = new System.Drawing.Size(446, 276);
			textBox1.TabIndex = 23;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(780, 521);
			base.Controls.Add(textBox1);
			base.Controls.Add(productComboBox1);
			base.Controls.Add(propertyGrid1);
			base.Name = "Test2";
			RightToLeftLayout = true;
			Text = "Test2";
			base.Load += new System.EventHandler(Test2_Load);
			((System.ComponentModel.ISupportInitialize)arcScaleComponent2).EndInit();
			((System.ComponentModel.ISupportInitialize)arcScaleBackgroundLayerComponent1).EndInit();
			((System.ComponentModel.ISupportInitialize)arcScaleNeedleComponent1).EndInit();
			((System.ComponentModel.ISupportInitialize)arcScaleComponent3).EndInit();
			((System.ComponentModel.ISupportInitialize)arcScaleBackgroundLayerComponent2).EndInit();
			((System.ComponentModel.ISupportInitialize)arcScaleNeedleComponent2).EndInit();
			((System.ComponentModel.ISupportInitialize)labelComponent2).EndInit();
			((System.ComponentModel.ISupportInitialize)productComboBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void button2_Click(object sender, EventArgs e)
		{
		}

		private void button3_Click(object sender, EventArgs e)
		{
			((IWorkFlowForm)new PurchaseOrderForm()).ShowForApproval("", "", -1);
		}

		private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
		}

		private void button3_Click_1(object sender, EventArgs e)
		{
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
		}

		private void Test2_Load(object sender, EventArgs e)
		{
			DataTable dataTable = new DataSet().Tables.Add("T");
			dataTable.Columns.Add("Arg");
			dataTable.Columns.Add("Val", typeof(decimal));
			dataTable.Rows.Add("Hasan", "20483000");
			dataTable.Rows.Add("Karim", "10483000");
			dataTable.Rows.Add("Majid", "14480003");
			dataTable.Rows.Add("John", "16000483");
			dataTable.Rows.Add("Mr.Hasan", "9480003");
			dataTable.Rows.Add("James", "19480003");
			dataTable.Rows.Add("Kamran", "4480003");
		}

		private void ultraButton1_Click(object sender, EventArgs e)
		{
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
		}

		private void button1_Click_2(object sender, EventArgs e)
		{
			try
			{
				decimal d = default(decimal);
				if ("".IsNullOrEmpty())
				{
					if (d >= 1000000000m)
					{
						_ = Format.NumberFormatB;
					}
					else if (d >= 1000000m)
					{
						_ = Format.NumberFormatM;
					}
					else if (d >= 10000m)
					{
						_ = Format.NumberFormatK;
					}
					else
					{
						_ = Format.NumberFormat;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
