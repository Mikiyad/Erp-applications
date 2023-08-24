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
	public class SalesByCustomerReport : Form, IForm
	{
		private List<string> TemplateList = new List<string>();

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private CustomerSelector customerSelector;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private DateControl dateControl1;

		private RadioButton radioButtonSummary;

		private RadioButton radioButtonDetail;

		private UltraGroupBox ultraGroupBox2;

		private Customer1Selector customer1Selector;

		private PrintTemplateSelector printTemplateSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsSales;

		public int ScreenID => 7034;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public SalesByCustomerReport()
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
				if (radioButtonSummary.Checked)
				{
					data = Factory.CustomerSystem.GetSalesByCustomerSummaryReport(dateControl1.FromDate, dateControl1.ToDate, customer1Selector.FromCustomer, customer1Selector.ToCustomer, customer1Selector.FromClass, customer1Selector.ToClass, customer1Selector.FromGroup, customer1Selector.ToGroup, customer1Selector.FromArea, customer1Selector.ToArea, customer1Selector.FromCountry, customer1Selector.ToCountry, customer1Selector.Customers, customer1Selector.CustomerClass, customer1Selector.CustomerGroup, customer1Selector.Area, customer1Selector.Country);
				}
				else
				{
					data = Factory.CustomerSystem.GetSalesByCustomerDetailReport(dateControl1.FromDate, dateControl1.ToDate, customer1Selector.FromCustomer, customer1Selector.ToCustomer, customer1Selector.FromClass, customer1Selector.ToClass, customer1Selector.FromGroup, customer1Selector.ToGroup, customer1Selector.FromArea, customer1Selector.ToArea, customer1Selector.FromCountry, customer1Selector.ToCountry, customer1Selector.Customers, customer1Selector.CustomerClass, customer1Selector.CustomerGroup, customer1Selector.Area, customer1Selector.Country);
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
				}
				string text = "";
				text = ((!(printTemplateSelector.SelectedTemplate != "")) ? printTemplateSelector.DefaultTemplate : printTemplateSelector.SelectedTemplate);
				xtraReport = reportHelper.GetReport(text);
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'" + text + ".repx'");
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

		private void radioButtonSummary_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonSummary.Checked)
			{
				printTemplateSelector.DefaultTemplate = "Sales by Customer Summary";
			}
			else
			{
				printTemplateSelector.DefaultTemplate = "Sales by Customer Detail";
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Sales.SalesByCustomerReport));
			buttonOK = new System.Windows.Forms.Button();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			buttonClose = new System.Windows.Forms.Button();
			dateControl1 = new Micromind.DataControls.DateControl();
			radioButtonSummary = new System.Windows.Forms.RadioButton();
			radioButtonDetail = new System.Windows.Forms.RadioButton();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			customer1Selector = new Micromind.DataControls.Customer1Selector();
			printTemplateSelector = new Micromind.DataControls.PrintTemplateSelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(224, 295);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(6, 19);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(77, 165);
			customerSelector.TabIndex = 0;
			ultraGroupBox1.Controls.Add(customerSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(442, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(46, 190);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Customers";
			ultraGroupBox1.Visible = false;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(328, 295);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 167);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(393, 61);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2017, 10, 18, 23, 59, 59, 59);
			radioButtonSummary.AutoSize = true;
			radioButtonSummary.Checked = true;
			radioButtonSummary.Location = new System.Drawing.Point(17, 229);
			radioButtonSummary.Name = "radioButtonSummary";
			radioButtonSummary.Size = new System.Drawing.Size(68, 17);
			radioButtonSummary.TabIndex = 2;
			radioButtonSummary.TabStop = true;
			radioButtonSummary.Text = "Summary";
			radioButtonSummary.UseVisualStyleBackColor = true;
			radioButtonSummary.CheckedChanged += new System.EventHandler(radioButtonSummary_CheckedChanged);
			radioButtonDetail.AutoSize = true;
			radioButtonDetail.Location = new System.Drawing.Point(106, 229);
			radioButtonDetail.Name = "radioButtonDetail";
			radioButtonDetail.Size = new System.Drawing.Size(52, 17);
			radioButtonDetail.TabIndex = 3;
			radioButtonDetail.Text = "Detail";
			radioButtonDetail.UseVisualStyleBackColor = true;
			ultraGroupBox2.Controls.Add(customer1Selector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(393, 149);
			ultraGroupBox2.TabIndex = 6;
			ultraGroupBox2.Text = "Customers";
			customer1Selector.BackColor = System.Drawing.Color.Transparent;
			customer1Selector.FromCustomer = "";
			customer1Selector.FromRange = "";
			customer1Selector.Location = new System.Drawing.Point(15, 16);
			customer1Selector.Name = "customer1Selector";
			customer1Selector.Size = new System.Drawing.Size(367, 128);
			customer1Selector.TabIndex = 0;
			customer1Selector.ToRange = "";
			printTemplateSelector.BackColor = System.Drawing.Color.Transparent;
			printTemplateSelector.CustomReportFieldName = "";
			printTemplateSelector.CustomReportKey = "";
			printTemplateSelector.CustomReportValueType = 1;
			printTemplateSelector.DefaultTemplate = "Sales by Customer Summary";
			printTemplateSelector.FormID = "SalesByCustomerReport";
			printTemplateSelector.Location = new System.Drawing.Point(12, 256);
			printTemplateSelector.Name = "printTemplateSelector";
			printTemplateSelector.Size = new System.Drawing.Size(366, 28);
			printTemplateSelector.TabIndex = 7;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(435, 326);
			base.Controls.Add(printTemplateSelector);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(radioButtonDetail);
			base.Controls.Add(radioButtonSummary);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "SalesByCustomerReport";
			Text = "Sales by Customer Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
