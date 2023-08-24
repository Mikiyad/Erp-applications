using DevExpress.XtraReports.UI;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Customers
{
	public class CustomerYearMonthPaymentReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private CustomerSelector customerSelector;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private UltraGroupBox ultraGroupBox2;

		private DateControl dateControlYear;

		private UltraGroupBox ultraGroupBox3;

		private DateControl dateControlMonth;

		private UltraGroupBox ultraGroupBox4;

		private DateControl dateControlPayment;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsCustomer;

		public int ScreenID => 7009;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public CustomerYearMonthPaymentReport()
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
			DataSet data = Factory.CustomerSystem.GetCustomerYearMonthPaymentReport(customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, dateControlYear.FromDate, dateControlYear.ToDate, dateControlMonth.FromDate, dateControlMonth.ToDate, dateControlPayment.FromDate, dateControlPayment.ToDate, customerSelector.MultipleCustomers);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControlYear.FromDate.ToShortDateString() + "  To:" + dateControlYear.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			XtraReport report = reportHelper.GetReport("CustomerCreditAnalysis");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'Customer Year Month Payment Report.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Customers.CustomerYearMonthPaymentReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			buttonClose = new System.Windows.Forms.Button();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			dateControlYear = new Micromind.DataControls.DateControl();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			dateControlMonth = new Micromind.DataControls.DateControl();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			dateControlPayment = new Micromind.DataControls.DateControl();
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
			buttonOK.Location = new System.Drawing.Point(286, 460);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(customerSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 186);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Customers";
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(6, 19);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(464, 161);
			customerSelector.TabIndex = 0;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 460);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			ultraGroupBox2.Controls.Add(dateControlYear);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 204);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(476, 76);
			ultraGroupBox2.TabIndex = 6;
			ultraGroupBox2.Text = "Yearly sale";
			dateControlYear.CustomReportFieldName = "";
			dateControlYear.CustomReportKey = "";
			dateControlYear.CustomReportValueType = 1;
			dateControlYear.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControlYear.Location = new System.Drawing.Point(17, 23);
			dateControlYear.Name = "dateControlYear";
			dateControlYear.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControlYear.Size = new System.Drawing.Size(355, 45);
			dateControlYear.TabIndex = 2;
			dateControlYear.ToDate = new System.DateTime(2017, 10, 18, 23, 59, 59, 59);
			ultraGroupBox3.Controls.Add(dateControlMonth);
			ultraGroupBox3.Location = new System.Drawing.Point(12, 282);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(476, 81);
			ultraGroupBox3.TabIndex = 7;
			ultraGroupBox3.Text = "Monthly Sale";
			dateControlMonth.CustomReportFieldName = "";
			dateControlMonth.CustomReportKey = "";
			dateControlMonth.CustomReportValueType = 1;
			dateControlMonth.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControlMonth.Location = new System.Drawing.Point(17, 25);
			dateControlMonth.Name = "dateControlMonth";
			dateControlMonth.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControlMonth.Size = new System.Drawing.Size(355, 49);
			dateControlMonth.TabIndex = 2;
			dateControlMonth.ToDate = new System.DateTime(2017, 10, 18, 23, 59, 59, 59);
			ultraGroupBox4.Controls.Add(dateControlPayment);
			ultraGroupBox4.Location = new System.Drawing.Point(12, 369);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(476, 81);
			ultraGroupBox4.TabIndex = 8;
			ultraGroupBox4.Text = "Late Payment";
			dateControlPayment.CustomReportFieldName = "";
			dateControlPayment.CustomReportKey = "";
			dateControlPayment.CustomReportValueType = 1;
			dateControlPayment.FromDate = new System.DateTime(2019, 3, 1, 0, 0, 0, 0);
			dateControlPayment.Location = new System.Drawing.Point(17, 25);
			dateControlPayment.Name = "dateControlPayment";
			dateControlPayment.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControlPayment.Size = new System.Drawing.Size(355, 48);
			dateControlPayment.TabIndex = 2;
			dateControlPayment.ToDate = new System.DateTime(2017, 10, 18, 23, 59, 59, 59);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(498, 491);
			base.Controls.Add(ultraGroupBox4);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "CustomerYearMonthPaymentReport";
			Text = "Customer Credit Analysis";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
