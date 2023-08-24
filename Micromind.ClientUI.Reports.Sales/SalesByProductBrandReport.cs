using DevExpress.XtraReports.UI;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Sales
{
	public class SalesByProductBrandReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private DateControl dateControl1;

		private UltraGroupBox ultraGroupBox1;

		private ItemBrandSelector itemBrandSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsSales;

		public int ScreenID => 7037;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public SalesByProductBrandReport()
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
			ReportHelper reportHelper = new ReportHelper();
			XtraReport xtraReport = null;
			DataSet data = Factory.ProductSystem.GetSalesByProductBrandSummaryReport(dateControl1.FromDate, dateControl1.ToDate, itemBrandSelector.FromProductBrand, itemBrandSelector.ToProductBrand);
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			foreach (DataRow row in data.Tables[0].Rows)
			{
				num = default(decimal);
				num2 = default(decimal);
				num3 = default(decimal);
				num4 = default(decimal);
				decimal.TryParse(row["ReturnQuantity"].ToString(), out num);
				decimal.TryParse(row["ReturnAmount"].ToString(), out num2);
				decimal.TryParse(row["SalesQuantity"].ToString(), out num3);
				decimal.TryParse(row["SalesAmount"].ToString(), out num4);
				row["SalesQuantity"] = num3 + num;
				row["SalesAmount"] = num4 - num2;
			}
			xtraReport = reportHelper.GetReport("Sales by Item Brand");
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			if (xtraReport == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Sales by Item Brand.repx'");
				return;
			}
			xtraReport.DataSource = data;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Sales.SalesByProductBrandReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			dateControl1 = new Micromind.DataControls.DateControl();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			itemBrandSelector = new Micromind.DataControls.ItemBrandSelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(286, 202);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 202);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2016, 1, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(23, 112);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(462, 61);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2016, 1, 26, 23, 59, 59, 59);
			ultraGroupBox1.Controls.Add(itemBrandSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(23, 3);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(462, 89);
			ultraGroupBox1.TabIndex = 7;
			ultraGroupBox1.Text = "Item Brand";
			itemBrandSelector.BackColor = System.Drawing.Color.Transparent;
			itemBrandSelector.Location = new System.Drawing.Point(16, 19);
			itemBrandSelector.Name = "itemBrandSelector";
			itemBrandSelector.Size = new System.Drawing.Size(414, 54);
			itemBrandSelector.TabIndex = 7;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(497, 233);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "SalesByProductBrandReport";
			Text = "Sales by Item Brand";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
