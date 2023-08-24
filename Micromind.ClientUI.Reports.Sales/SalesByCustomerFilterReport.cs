using DevExpress.XtraReports.UI;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
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
	public class SalesByCustomerFilterReport : Form, IForm
	{
		private List<string> TemplateList = new List<string>();

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private DateControl dateControl1;

		private Customer1Selector customer1Selector1;

		private Label label1;

		private Button buttonReportTemplate;

		private TextBox textBoxTemplate;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsSales;

		public int ScreenID => 7034;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public SalesByCustomerFilterReport()
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
				DataSet data = Factory.CustomerSystem.GetSalesByCustomerSummaryReport(dateControl1.FromDate, dateControl1.ToDate, customer1Selector1.FromCustomer, customer1Selector1.ToCustomer, customer1Selector1.FromClass, customer1Selector1.ToClass, customer1Selector1.FromGroup, customer1Selector1.ToGroup, customer1Selector1.FromArea, customer1Selector1.ToArea, customer1Selector1.FromCountry, customer1Selector1.ToCountry, customer1Selector1.Customers, customer1Selector1.CustomerClass, customer1Selector1.CustomerGroup, customer1Selector1.Area, customer1Selector1.Country);
				xtraReport = ((!(textBoxTemplate.Text != "")) ? reportHelper.GetReport("Sales By Customer Summary") : reportHelper.GetReport(textBoxTemplate.Text));
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Sales By Customer Summary.repx'");
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

		private void buttonReportTemplate_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			new List<string>();
			dataSet = Factory.PrintTemplateMapSystem.GetPrintTemplateMapComboList();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.SelectedDocuments = TemplateList;
			selectDocumentDialog.Text = "Select Template";
			selectDocumentDialog.HiddenColumns.Add("ScreenID");
			selectDocumentDialog.HiddenColumns.Add("ScreenType");
			selectDocumentDialog.HiddenColumns.Add("FileName");
			selectDocumentDialog.DataSource = dataSet;
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					TemplateList.Clear();
					string item = selectedRow.Cells["FileName"].Value.ToString();
					TemplateList.Add(item);
				}
				textBoxTemplate.Text = string.Join(",", TemplateList);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Sales.SalesByCustomerFilterReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			customer1Selector1 = new Micromind.DataControls.Customer1Selector();
			buttonClose = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			buttonReportTemplate = new System.Windows.Forms.Button();
			textBoxTemplate = new System.Windows.Forms.TextBox();
			dateControl1 = new Micromind.DataControls.DateControl();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(217, 271);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(customer1Selector1);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(385, 149);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Customers";
			customer1Selector1.BackColor = System.Drawing.Color.Transparent;
			customer1Selector1.FromCustomer = "";
			customer1Selector1.FromRange = "";
			customer1Selector1.Location = new System.Drawing.Point(15, 16);
			customer1Selector1.Name = "customer1Selector1";
			customer1Selector1.Size = new System.Drawing.Size(367, 128);
			customer1Selector1.TabIndex = 0;
			customer1Selector1.ToRange = "";
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(321, 271);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(17, 231);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(89, 13);
			label1.TabIndex = 6;
			label1.Text = "Report Template:";
			buttonReportTemplate.Location = new System.Drawing.Point(344, 227);
			buttonReportTemplate.Name = "buttonReportTemplate";
			buttonReportTemplate.Size = new System.Drawing.Size(26, 21);
			buttonReportTemplate.TabIndex = 89;
			buttonReportTemplate.Text = "...";
			buttonReportTemplate.UseVisualStyleBackColor = true;
			buttonReportTemplate.Click += new System.EventHandler(buttonReportTemplate_Click);
			textBoxTemplate.Location = new System.Drawing.Point(128, 228);
			textBoxTemplate.Name = "textBoxTemplate";
			textBoxTemplate.ReadOnly = true;
			textBoxTemplate.Size = new System.Drawing.Size(214, 20);
			textBoxTemplate.TabIndex = 90;
			textBoxTemplate.Text = "Sales By Customer Summary";
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 167);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(399, 61);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2017, 10, 18, 23, 59, 59, 59);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(428, 302);
			base.Controls.Add(buttonReportTemplate);
			base.Controls.Add(textBoxTemplate);
			base.Controls.Add(label1);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "SalesByCustomerFilterReport";
			Text = "Sales by Customer Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
