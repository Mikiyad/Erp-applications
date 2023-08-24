using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Native;
using Micromind.Common;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.CustomDashboards
{
	public class AddChartSeriesDialog : Form
	{
		private GadgetParameter gadgetParameter;

		private IContainer components;

		private Button buttonCancel;

		private Button buttonOK;

		private Micromind.UISupport.Line line1;

		private ListView listView1;

		public GadgetParameter GadgetParameter => gadgetParameter;

		public ViewType ChartType
		{
			get
			{
				return (ViewType)Enum.Parse(typeof(ViewType), listView1.SelectedItems[0].Name);
			}
			set
			{
			}
		}

		public AddChartSeriesDialog()
		{
			InitializeComponent();
			LoadItems();
			base.DialogResult = DialogResult.None;
		}

		private void LoadItems()
		{
			Enum.GetNames(typeof(ViewType));
			ImageList imageList = new ImageList();
			imageList.ImageSize = new Size(48, 48);
			imageList.Images.AddRange(SeriesViewFactory.SeriesViewImages);
			listView1.Items.Add(ViewType.Bar.ToString(), ViewType.Bar.ToString(), 0);
			listView1.Items.Add(ViewType.StackedBar.ToString(), "Stacked Bar", 1);
			listView1.Items.Add(ViewType.FullStackedBar.ToString(), "Full Stacked Bar", 2);
			listView1.Items.Add(ViewType.SideBySideRangeBar.ToString(), "Side By Side Range Bar", 3);
			listView1.Items.Add(ViewType.SideBySideFullStackedBar.ToString(), "Side By Side Full Stacked Bar", 4);
			listView1.Items.Add(ViewType.Bar3D.ToString(), "Bar 3D", 5);
			listView1.Items.Add(ViewType.StackedBar3D.ToString(), "Stacked Bar 3D", 6);
			listView1.Items.Add(ViewType.ManhattanBar.ToString(), "Manhattan Bar", 10);
			listView1.Items.Add(ViewType.Point.ToString(), "Point", 11);
			listView1.Items.Add(ViewType.Bubble.ToString(), "Bubble", 12);
			listView1.Items.Add(ViewType.Line.ToString(), "Line", 13);
			listView1.Items.Add(ViewType.Line3D.ToString(), "Line 3D", 20);
			listView1.Items.Add(ViewType.Pie.ToString(), "Pie", 25);
			listView1.Items.Add(ViewType.Doughnut.ToString(), "Doughnut", 26);
			listView1.Items.Add(ViewType.Pie3D.ToString(), "Pie 3D", 27);
			listView1.Items.Add(ViewType.Doughnut3D.ToString(), "Doughnut 3D", 28);
			listView1.Items.Add(ViewType.Funnel.ToString(), "Funnel", 29);
			listView1.Items.Add(ViewType.Funnel3D.ToString(), "Funnel 3D", 30);
			listView1.Items.Add(ViewType.Area.ToString(), "Area", 31);
			listView1.Items.Add(ViewType.StackedArea.ToString(), "Stacked Area", 32);
			listView1.LargeImageList = imageList;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0)
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		public void EditParameter(GadgetParameter parameter)
		{
		}

		private void AddChartSeriesDialog_Load(object sender, EventArgs e)
		{
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			buttonOK_Click(sender, e);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.CustomDashboards.AddChartSeriesDialog));
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			listView1 = new System.Windows.Forms.ListView();
			SuspendLayout();
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.Location = new System.Drawing.Point(610, 400);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(94, 29);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(510, 400);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(94, 29);
			buttonOK.TabIndex = 2;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-54, 395);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(812, 1);
			line1.TabIndex = 3;
			line1.TabStop = false;
			listView1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			listView1.Location = new System.Drawing.Point(12, 12);
			listView1.Name = "listView1";
			listView1.Size = new System.Drawing.Size(692, 377);
			listView1.TabIndex = 4;
			listView1.UseCompatibleStateImageBehavior = false;
			listView1.DoubleClick += new System.EventHandler(listView1_DoubleClick);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(718, 435);
			base.Controls.Add(listView1);
			base.Controls.Add(line1);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AddChartSeriesDialog";
			Text = "Chart Type";
			base.Load += new System.EventHandler(AddChartSeriesDialog_Load);
			ResumeLayout(false);
		}
	}
}
