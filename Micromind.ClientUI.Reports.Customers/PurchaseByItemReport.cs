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

namespace Micromind.ClientUI.Reports.Customers
{
	public class PurchaseByItemReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private DateControl dateControl1;

		private RadioButton radioButtonSummary;

		private RadioButton radioButtonDetail;

		private UltraGroupBox ultraGroupBox1;

		private ProductSelector productSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsPurchase;

		public int ScreenID => 7028;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public PurchaseByItemReport()
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
				ReportHelper reportHelper = new ReportHelper();
				XtraReport xtraReport = null;
				DataSet data;
				if (radioButtonDetail.Checked)
				{
					data = Factory.ProductSystem.GetPurchaseByItemDetailReport(dateControl1.FromDate, dateControl1.ToDate, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
					xtraReport = reportHelper.GetReport("Purchase By Item Detail");
				}
				else
				{
					data = Factory.ProductSystem.GetPurchaseByItemSummaryReport(dateControl1.FromDate, dateControl1.ToDate, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
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
						decimal.TryParse(row["PurchaseQuantity"].ToString(), out num3);
						decimal.TryParse(row["PurchaseAmount"].ToString(), out num4);
						row["PurchaseQuantity"] = num3 - num;
						row["PurchaseAmount"] = num4 - num2;
					}
					xtraReport = reportHelper.GetReport("Purchase By Item Summary");
				}
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Purchase By Item Summary.repx'");
				}
				else
				{
					xtraReport.DataSource = data;
					reportHelper.ShowReport(xtraReport);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Customers.PurchaseByItemReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			dateControl1 = new Micromind.DataControls.DateControl();
			radioButtonSummary = new System.Windows.Forms.RadioButton();
			radioButtonDetail = new System.Windows.Forms.RadioButton();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(286, 328);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 328);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2012, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 227);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(476, 57);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2012, 10, 24, 23, 59, 59, 59);
			radioButtonSummary.AutoSize = true;
			radioButtonSummary.Checked = true;
			radioButtonSummary.Location = new System.Drawing.Point(27, 288);
			radioButtonSummary.Name = "radioButtonSummary";
			radioButtonSummary.Size = new System.Drawing.Size(68, 17);
			radioButtonSummary.TabIndex = 2;
			radioButtonSummary.TabStop = true;
			radioButtonSummary.Text = "Summary";
			radioButtonSummary.UseVisualStyleBackColor = true;
			radioButtonDetail.AutoSize = true;
			radioButtonDetail.Location = new System.Drawing.Point(116, 288);
			radioButtonDetail.Name = "radioButtonDetail";
			radioButtonDetail.Size = new System.Drawing.Size(52, 17);
			radioButtonDetail.TabIndex = 3;
			radioButtonDetail.Text = "Detail";
			radioButtonDetail.UseVisualStyleBackColor = true;
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 209);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Items";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(5, 18);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(446, 185);
			productSelector.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(497, 359);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(radioButtonDetail);
			base.Controls.Add(radioButtonSummary);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "PurchaseByItemReport";
			Text = "Purchase by Item Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
