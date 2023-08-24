using DevExpress.XtraReports.UI;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Items
{
	public class SalesPurchaseAnalysisReport : Form, IForm
	{
		public bool IsValuation;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private ProductSelector productSelector;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private Line line1;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox2;

		private LocationSelector locationSelector1;

		private DateControl dateControl1;

		private CheckBox checkBoxAverage;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7026;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public SalesPurchaseAnalysisReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				_ = base.IsDisposed;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "";
				DataSet data = Factory.ProductSystem.GetSalesPurchaseAnalysisPivotReport(dateControl1.FromDate, dateControl1.ToDate, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, locationSelector1.FromLocation, locationSelector1.ToLocation, isAsOfDate: false, showZero: false, isInactive: false, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin, productSelector.FromBrand, productSelector.ToBrand);
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				decimal num4 = default(decimal);
				foreach (DataRow row in data.Tables[0].Rows)
				{
					num = default(decimal);
					num3 = default(decimal);
					num2 = default(decimal);
					num4 = default(decimal);
					decimal.TryParse(row["SalesQty"].ToString(), out num);
					decimal.TryParse(row["SalesQtynew"].ToString(), out num2);
					decimal.TryParse(row["PurchaseQty"].ToString(), out num3);
					decimal.TryParse(row["PurchaseQtynew"].ToString(), out num4);
					num += num2;
					row["SalesQty"] = num;
					num3 += num4;
					row["PurchaseQty"] = num3;
				}
				text = ((!checkBoxAverage.Checked) ? "Sales Purchase Analysis" : "Sales Purchase AvgAnalysis");
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				XtraReport report = reportHelper.GetReport(text);
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'Sales Purchase Analysis.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				}
				else
				{
					report.DataSource = data;
					reportHelper.ShowReport(report);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void radioButtonLocationWise_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonAsOf_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void ultraComboEditor1_ValueChanged(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.SalesPurchaseAnalysisReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			buttonClose = new System.Windows.Forms.Button();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector1 = new Micromind.DataControls.LocationSelector();
			line1 = new Micromind.UISupport.Line();
			locationSelector = new Micromind.DataControls.LocationSelector();
			dateControl1 = new Micromind.DataControls.DateControl();
			checkBoxAverage = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(289, 398);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 7;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 212);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Products";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(6, 19);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(403, 181);
			productSelector.TabIndex = 2;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(393, 398);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 8;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			ultraGroupBox2.Controls.Add(locationSelector1);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 230);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(476, 84);
			ultraGroupBox2.TabIndex = 15;
			ultraGroupBox2.Text = "Locations";
			locationSelector1.BackColor = System.Drawing.Color.Transparent;
			locationSelector1.CustomReportFieldName = "";
			locationSelector1.CustomReportKey = "";
			locationSelector1.CustomReportValueType = 1;
			locationSelector1.Location = new System.Drawing.Point(6, 19);
			locationSelector1.Name = "locationSelector1";
			locationSelector1.Size = new System.Drawing.Size(424, 53);
			locationSelector1.TabIndex = 2;
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-8, 392);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(520, 1);
			line1.TabIndex = 13;
			line1.TabStop = false;
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(0, 0);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(414, 54);
			locationSelector.TabIndex = 0;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2012, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 320);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(476, 63);
			dateControl1.TabIndex = 17;
			dateControl1.ToDate = new System.DateTime(2012, 10, 24, 23, 59, 59, 59);
			checkBoxAverage.AutoSize = true;
			checkBoxAverage.Location = new System.Drawing.Point(317, 336);
			checkBoxAverage.Name = "checkBoxAverage";
			checkBoxAverage.Size = new System.Drawing.Size(66, 17);
			checkBoxAverage.TabIndex = 18;
			checkBoxAverage.Text = "Average";
			checkBoxAverage.UseVisualStyleBackColor = true;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(500, 430);
			base.Controls.Add(checkBoxAverage);
			base.Controls.Add(dateControl1);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(line1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "SalesPurchaseAnalysisReport";
			Text = "Sales Purchase Analysis";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
