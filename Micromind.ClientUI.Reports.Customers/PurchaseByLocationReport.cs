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
	public class PurchaseByLocationReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private DateControl dateControl1;

		private RadioButton radioButtonSummary;

		private RadioButton radioButtonDetail;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsPurchase;

		public int ScreenID => 7029;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public PurchaseByLocationReport()
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
			DataSet data;
			if (radioButtonDetail.Checked)
			{
				data = Factory.LocationSystem.GetPurchaseByLocationDetailReport(dateControl1.FromDate, dateControl1.ToDate, locationSelector.FromLocation, locationSelector.ToLocation);
				if (data.Tables.Count > 1)
				{
					string baseCurrencyID = Global.BaseCurrencyID;
					data.Tables[1].Columns.Add("CurrencyNote");
					foreach (DataRow row in data.Tables[1].Rows)
					{
						if (row["CurrencyID"] != DBNull.Value && row["CurrencyID"].ToString() != baseCurrencyID)
						{
							row["CurrencyNote"] = "Cur: " + row["CurrencyID"].ToString() + "  -  Rate: " + decimal.Parse(row["CurrencyRate"].ToString()).ToString(Format.QuantityFormat) + "  -  Amount: " + decimal.Parse(row["TotalFC"].ToString()).ToString(Format.TotalAmountFormat);
						}
					}
				}
				xtraReport = reportHelper.GetReport("Purchase By Location Detail");
			}
			else
			{
				data = Factory.LocationSystem.GetPurchaseByLocationSummaryReport(dateControl1.FromDate, dateControl1.ToDate, locationSelector.FromLocation, locationSelector.ToLocation);
				data.Tables[0].Columns.Add("Total", typeof(decimal));
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				decimal num4 = default(decimal);
				decimal num5 = default(decimal);
				decimal num6 = default(decimal);
				foreach (DataRow row2 in data.Tables[0].Rows)
				{
					num = default(decimal);
					num2 = default(decimal);
					num3 = default(decimal);
					num4 = default(decimal);
					num5 = default(decimal);
					num6 = default(decimal);
					decimal.TryParse(row2["Discount"].ToString(), out num);
					decimal.TryParse(row2["DiscountReturn"].ToString(), out num2);
					decimal.TryParse(row2["PurchaseReturn"].ToString(), out num3);
					decimal.TryParse(row2["CashSale"].ToString(), out num4);
					decimal.TryParse(row2["CreditSale"].ToString(), out num5);
					if (num2 != 0m)
					{
						num += num2;
						row2["Discount"] = num;
					}
					num6 = num4 + num5 - num3;
					row2["Total"] = num6;
				}
				xtraReport = reportHelper.GetReport("Purchase By Location Summary");
			}
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			if (xtraReport == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Purchase By Location Summary.repx'");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Customers.PurchaseByLocationReport));
			buttonOK = new System.Windows.Forms.Button();
			locationSelector = new Micromind.DataControls.LocationSelector();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			buttonClose = new System.Windows.Forms.Button();
			dateControl1 = new Micromind.DataControls.DateControl();
			radioButtonSummary = new System.Windows.Forms.RadioButton();
			radioButtonDetail = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(286, 204);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.Location = new System.Drawing.Point(6, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(436, 55);
			locationSelector.TabIndex = 0;
			ultraGroupBox1.Controls.Add(locationSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 76);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Locations";
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 204);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			dateControl1.FromDate = new System.DateTime(2012, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 94);
			dateControl1.Name = "dateControl1";
			dateControl1.Size = new System.Drawing.Size(476, 77);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2012, 10, 24, 23, 59, 59, 59);
			radioButtonSummary.AutoSize = true;
			radioButtonSummary.Checked = true;
			radioButtonSummary.Location = new System.Drawing.Point(27, 177);
			radioButtonSummary.Name = "radioButtonSummary";
			radioButtonSummary.Size = new System.Drawing.Size(68, 17);
			radioButtonSummary.TabIndex = 2;
			radioButtonSummary.TabStop = true;
			radioButtonSummary.Text = "Summary";
			radioButtonSummary.UseVisualStyleBackColor = true;
			radioButtonDetail.AutoSize = true;
			radioButtonDetail.Location = new System.Drawing.Point(116, 177);
			radioButtonDetail.Name = "radioButtonDetail";
			radioButtonDetail.Size = new System.Drawing.Size(52, 17);
			radioButtonDetail.TabIndex = 3;
			radioButtonDetail.Text = "Detail";
			radioButtonDetail.UseVisualStyleBackColor = true;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(497, 235);
			base.Controls.Add(radioButtonDetail);
			base.Controls.Add(radioButtonSummary);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "PurchaseByLocationReport";
			Text = "Purchase by Location Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
