using Infragistics.Win;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Micromind.DataControls;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class ReportViewerForm : Form
	{
		private bool groupbyDisabled;

		private ArrayList totalColumns = new ArrayList();

		private ArrayList hiddenColumns = new ArrayList();

		private Form initFilterForm;

		private bool isMultiBand;

		private IContainer components;

		private MMSReportGrid reportGrid;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButton2;

		private UltraCalcManager ultraCalcManager1;

		public bool GroupByDisabled
		{
			get
			{
				return groupbyDisabled;
			}
			set
			{
				groupbyDisabled = value;
				reportGrid.DisplayLayout.GroupByBox.Hidden = value;
			}
		}

		public ArrayList TotalColumns => totalColumns;

		public ArrayList HiddenColumns => hiddenColumns;

		public string ReportTitle
		{
			get
			{
				return Text;
			}
			set
			{
				Text = value;
			}
		}

		public MMSReportGrid ReportGrid => reportGrid;

		public bool IsMultiBand
		{
			get
			{
				return isMultiBand;
			}
			set
			{
				isMultiBand = value;
			}
		}

		public DataSet DataSource
		{
			set
			{
				reportGrid.DataSource = value;
			}
		}

		public Form InitFilterForm
		{
			get
			{
				return initFilterForm;
			}
			set
			{
				initFilterForm = value;
			}
		}

		public ReportViewerForm()
		{
			InitializeComponent();
			base.Load += ReportViewerForm_Load;
		}

		private void ReportViewerForm_Load(object sender, EventArgs e)
		{
			try
			{
				int index = 0;
				if (isMultiBand)
				{
					index = 1;
				}
				if (reportGrid.DataSource != null)
				{
					foreach (string totalColumn in TotalColumns)
					{
						reportGrid.DisplayLayout.Bands[index].Summaries.Add(totalColumn, SummaryType.Sum, reportGrid.DisplayLayout.Bands[index].Columns[totalColumn], SummaryPosition.UseSummaryPositionColumn);
						if (isMultiBand)
						{
							reportGrid.DisplayLayout.Bands[0].Summaries.Add(totalColumn, SummaryType.Sum, reportGrid.DisplayLayout.Bands[index].Columns[totalColumn], SummaryPosition.UseSummaryPositionColumn);
						}
					}
					foreach (string hiddenColumn in hiddenColumns)
					{
						reportGrid.DisplayLayout.Bands[index].Columns[hiddenColumn].Hidden = true;
					}
				}
				reportGrid.ApplyUIDesign();
				if (isMultiBand)
				{
					reportGrid.ApplySecondBandDesign();
				}
			}
			catch
			{
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.ReportViewerForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			reportGrid = new Micromind.DataControls.MMSReportGrid();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)reportGrid).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			SuspendLayout();
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2]
			{
				toolStripButton1,
				toolStripButton2
			});
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(763, 25);
			toolStrip1.TabIndex = 2;
			toolStrip1.Text = "toolStrip1";
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(23, 22);
			toolStripButton1.Text = "toolStripButton1";
			toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton2.Image = (System.Drawing.Image)resources.GetObject("toolStripButton2.Image");
			toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton2.Name = "toolStripButton2";
			toolStripButton2.Size = new System.Drawing.Size(23, 22);
			toolStripButton2.Text = "toolStripButton2";
			reportGrid.CalcManager = ultraCalcManager1;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			reportGrid.DisplayLayout.Appearance = appearance;
			reportGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			reportGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			reportGrid.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			reportGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			reportGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			reportGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			reportGrid.DisplayLayout.MaxColScrollRegions = 1;
			reportGrid.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			reportGrid.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			reportGrid.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			reportGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			reportGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			reportGrid.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			reportGrid.DisplayLayout.Override.CellAppearance = appearance8;
			reportGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			reportGrid.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			reportGrid.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			reportGrid.DisplayLayout.Override.HeaderAppearance = appearance10;
			reportGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			reportGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			reportGrid.DisplayLayout.Override.RowAppearance = appearance11;
			reportGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			reportGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			reportGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			reportGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			reportGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			reportGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			reportGrid.Location = new System.Drawing.Point(0, 25);
			reportGrid.Name = "reportGrid";
			reportGrid.ShowMinusInRed = true;
			reportGrid.Size = new System.Drawing.Size(763, 507);
			reportGrid.TabIndex = 1;
			reportGrid.Text = "mmsReportGrid1";
			ultraCalcManager1.ContainingControl = this;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(763, 532);
			base.Controls.Add(reportGrid);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ReportViewerForm";
			Text = "Report";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)reportGrid).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
