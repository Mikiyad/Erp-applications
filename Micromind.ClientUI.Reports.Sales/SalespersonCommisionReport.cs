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

namespace Micromind.ClientUI.Reports.Sales
{
	public class SalespersonCommisionReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private DateControl dateControl1;

		private UltraGroupBox ultraGroupBox1;

		private ItemBrandSelector itemBrandSelector;

		private UltraGroupBox ultraGroupBox2;

		private ProductCategorySelector productCategorySelector;

		private UltraGroupBox ultraGroupBox3;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox4;

		private SalespersonSelector salespersonSelector;

		private Label label3;

		private PercentTextBox textBoxCommissionPercent;

		private RadioButton radioButtonDetail;

		private RadioButton radioButtonSummary;

		private RadioButton radioButtonProducts;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsSales;

		public int ScreenID => 7037;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public SalespersonCommisionReport()
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
			DataSet dataSet = new DataSet();
			ReportHelper reportHelper = new ReportHelper();
			XtraReport xtraReport = null;
			decimal result = default(decimal);
			decimal.TryParse(textBoxCommissionPercent.Text, out result);
			if (radioButtonDetail.Checked)
			{
				dataSet = Factory.SalespersonSystem.GetSalesPersonCommissionDetailReport(dateControl1.FromDate, dateControl1.ToDate, itemBrandSelector.FromProductBrand, itemBrandSelector.ToProductBrand, productCategorySelector.FromProductCategory, productCategorySelector.ToProductCategory, locationSelector.FromLocation, locationSelector.ToLocation, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry, result);
				if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						if (!dataSet.Tables[0].Columns.Contains("CommissionPercent"))
						{
							dataSet.Tables[0].Columns.Add("CommissionPercent");
						}
						row["CommissionPercent"] = result;
					}
				}
				xtraReport = reportHelper.GetReport("Salesperson Commission Details");
			}
			else if (radioButtonSummary.Checked)
			{
				dataSet = Factory.SalespersonSystem.GetSalesPersonCommissionSummaryReport(dateControl1.FromDate, dateControl1.ToDate, itemBrandSelector.FromProductBrand, itemBrandSelector.ToProductBrand, productCategorySelector.FromProductCategory, productCategorySelector.ToProductCategory, locationSelector.FromLocation, locationSelector.ToLocation, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry, result);
				if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row2 in dataSet.Tables[0].Rows)
					{
						if (!dataSet.Tables[0].Columns.Contains("CommissionPercent"))
						{
							dataSet.Tables[0].Columns.Add("CommissionPercent");
						}
						row2["CommissionPercent"] = result;
					}
				}
				xtraReport = reportHelper.GetReport("Salesperson Commission Summary");
			}
			else
			{
				dataSet = Factory.SalespersonSystem.GetSalesPersonCommissionItemIncludedReport(dateControl1.FromDate, dateControl1.ToDate, itemBrandSelector.FromProductBrand, itemBrandSelector.ToProductBrand, productCategorySelector.FromProductCategory, productCategorySelector.ToProductCategory, locationSelector.FromLocation, locationSelector.ToLocation, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry, result);
				if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row3 in dataSet.Tables[0].Rows)
					{
						if (!dataSet.Tables[0].Columns.Contains("CommissionPercent"))
						{
							dataSet.Tables[0].Columns.Add("CommissionPercent");
						}
						row3["CommissionPercent"] = result;
					}
				}
				xtraReport = reportHelper.GetReport("Salesperson Commission More");
			}
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref dataSet, reportFilter);
			reportHelper.AddFilterData(ref dataSet, GetAllFormControls(this));
			if (xtraReport == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Salesperson Commission.repx'");
				return;
			}
			xtraReport.DataSource = dataSet;
			reportHelper.ShowReport(xtraReport);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Sales.SalespersonCommisionReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			itemBrandSelector = new Micromind.DataControls.ItemBrandSelector();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			productCategorySelector = new Micromind.DataControls.ProductCategorySelector();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			salespersonSelector = new Micromind.DataControls.SalespersonSelector();
			label3 = new System.Windows.Forms.Label();
			radioButtonDetail = new System.Windows.Forms.RadioButton();
			radioButtonSummary = new System.Windows.Forms.RadioButton();
			radioButtonProducts = new System.Windows.Forms.RadioButton();
			textBoxCommissionPercent = new Micromind.UISupport.PercentTextBox();
			dateControl1 = new Micromind.DataControls.DateControl();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(737, 307);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(841, 307);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			ultraGroupBox1.Controls.Add(itemBrandSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(488, 20);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(444, 89);
			ultraGroupBox1.TabIndex = 7;
			ultraGroupBox1.Text = "Item Brand";
			itemBrandSelector.BackColor = System.Drawing.Color.Transparent;
			itemBrandSelector.Location = new System.Drawing.Point(16, 19);
			itemBrandSelector.Name = "itemBrandSelector";
			itemBrandSelector.Size = new System.Drawing.Size(408, 54);
			itemBrandSelector.TabIndex = 7;
			ultraGroupBox2.Controls.Add(productCategorySelector);
			ultraGroupBox2.Location = new System.Drawing.Point(488, 115);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(444, 82);
			ultraGroupBox2.TabIndex = 8;
			ultraGroupBox2.Text = "Product Categories";
			productCategorySelector.BackColor = System.Drawing.Color.Transparent;
			productCategorySelector.Location = new System.Drawing.Point(6, 19);
			productCategorySelector.Name = "productCategorySelector";
			productCategorySelector.Size = new System.Drawing.Size(418, 51);
			productCategorySelector.TabIndex = 0;
			ultraGroupBox3.Controls.Add(locationSelector);
			ultraGroupBox3.Location = new System.Drawing.Point(13, 203);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(461, 76);
			ultraGroupBox3.TabIndex = 9;
			ultraGroupBox3.Text = "Locations";
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(436, 55);
			locationSelector.TabIndex = 0;
			ultraGroupBox4.Controls.Add(salespersonSelector);
			ultraGroupBox4.Location = new System.Drawing.Point(12, 20);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(462, 177);
			ultraGroupBox4.TabIndex = 10;
			ultraGroupBox4.Text = "Salespersons";
			salespersonSelector.BackColor = System.Drawing.Color.Transparent;
			salespersonSelector.Location = new System.Drawing.Point(6, 19);
			salespersonSelector.Name = "salespersonSelector";
			salespersonSelector.Size = new System.Drawing.Size(436, 149);
			salespersonSelector.TabIndex = 0;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(785, 222);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(79, 13);
			label3.TabIndex = 175;
			label3.Text = "Commission %: ";
			radioButtonDetail.AutoSize = true;
			radioButtonDetail.Location = new System.Drawing.Point(627, 270);
			radioButtonDetail.Name = "radioButtonDetail";
			radioButtonDetail.Size = new System.Drawing.Size(52, 17);
			radioButtonDetail.TabIndex = 177;
			radioButtonDetail.Text = "Detail";
			radioButtonDetail.UseVisualStyleBackColor = true;
			radioButtonSummary.AutoSize = true;
			radioButtonSummary.Checked = true;
			radioButtonSummary.Location = new System.Drawing.Point(538, 270);
			radioButtonSummary.Name = "radioButtonSummary";
			radioButtonSummary.Size = new System.Drawing.Size(68, 17);
			radioButtonSummary.TabIndex = 176;
			radioButtonSummary.TabStop = true;
			radioButtonSummary.Text = "Summary";
			radioButtonSummary.UseVisualStyleBackColor = true;
			radioButtonProducts.AutoSize = true;
			radioButtonProducts.Location = new System.Drawing.Point(701, 270);
			radioButtonProducts.Name = "radioButtonProducts";
			radioButtonProducts.Size = new System.Drawing.Size(49, 17);
			radioButtonProducts.TabIndex = 178;
			radioButtonProducts.Text = "More";
			radioButtonProducts.UseVisualStyleBackColor = true;
			textBoxCommissionPercent.CustomReportFieldName = "";
			textBoxCommissionPercent.CustomReportKey = "";
			textBoxCommissionPercent.CustomReportValueType = 1;
			textBoxCommissionPercent.IsComboTextBox = false;
			textBoxCommissionPercent.IsModified = false;
			textBoxCommissionPercent.Location = new System.Drawing.Point(872, 219);
			textBoxCommissionPercent.Name = "textBoxCommissionPercent";
			textBoxCommissionPercent.Size = new System.Drawing.Size(60, 20);
			textBoxCommissionPercent.TabIndex = 174;
			textBoxCommissionPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(494, 209);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(295, 61);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2016, 1, 26, 23, 59, 59, 59);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(948, 338);
			base.Controls.Add(radioButtonProducts);
			base.Controls.Add(radioButtonDetail);
			base.Controls.Add(radioButtonSummary);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxCommissionPercent);
			base.Controls.Add(ultraGroupBox4);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "SalespersonCommisionReport";
			Text = "Salesperson Commision Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
