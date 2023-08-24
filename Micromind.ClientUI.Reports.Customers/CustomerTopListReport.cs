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
	public class CustomerTopListReport : Form, IForm
	{
		private string reportType = "";

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private DateControl dateControl1;

		private UltraGroupBox ultraGroupBox1;

		private RadioButton radioButtonBySalesProfit;

		private RadioButton radioButtonBySalesQty;

		private RadioButton radioButtonBySalesValue;

		private Label label1;

		private Label label2;

		private NumericUpDown topNumberPicker;

		public string ReportType
		{
			get
			{
				return reportType;
			}
			set
			{
				reportType = value;
			}
		}

		public ScreenAreas ScreenArea => ScreenAreas.ReportsCustomer;

		public int ScreenID => 7009;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public CustomerTopListReport()
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

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			string mode = "";
			if (radioButtonBySalesProfit.Checked)
			{
				mode = "Profit";
			}
			else if (radioButtonBySalesQty.Checked)
			{
				mode = "Quantity";
			}
			else if (radioButtonBySalesValue.Checked)
			{
				mode = "Amount";
			}
			string text = "";
			DataSet dataSet = new DataSet();
			dataSet = ((!(reportType == "C")) ? Factory.CustomerSystem.GetProductTopList(dateControl1.FromDate, dateControl1.ToDate, Convert.ToInt32(topNumberPicker.Value), mode) : Factory.CustomerSystem.GetCustomerTopList(dateControl1.FromDate, dateControl1.ToDate, Convert.ToInt32(topNumberPicker.Value), mode));
			if (dataSet.Tables.Count > 1)
			{
				dataSet.Tables[1].Columns.Add("SysDocTypeName");
				foreach (DataRow row in dataSet.Tables[1].Rows)
				{
					if (row["SysDocType"] != DBNull.Value)
					{
						int sysDocType = int.Parse(row["SysDocType"].ToString());
						row["SysDocTypeName"] = PublicFunctions.GetSysDocTypeString(sysDocType);
					}
				}
			}
			if (radioButtonBySalesProfit.Checked)
			{
				text = ((!(reportType == "C")) ? "Product Top List By Sales Profit" : "Customer Top List By Sales Profit");
			}
			else if (radioButtonBySalesQty.Checked)
			{
				text = ((!(reportType == "C")) ? "Product Top List By Sales Qty" : "Customer Top List By Sales Qty");
			}
			else if (radioButtonBySalesValue.Checked)
			{
				text = ((!(reportType == "C")) ? "Product Top List By Sales Value" : "Customer Top List By Sales Value");
			}
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref dataSet, reportFilter);
			reportHelper.AddFilterData(ref dataSet, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport(text);
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file," + text + ", Please make sure you have access to reports path and the files are not corrupted.");
				return;
			}
			report.DataSource = dataSet;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Customers.CustomerTopListReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			topNumberPicker = new System.Windows.Forms.NumericUpDown();
			label2 = new System.Windows.Forms.Label();
			radioButtonBySalesProfit = new System.Windows.Forms.RadioButton();
			radioButtonBySalesQty = new System.Windows.Forms.RadioButton();
			radioButtonBySalesValue = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			dateControl1 = new Micromind.DataControls.DateControl();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)topNumberPicker).BeginInit();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(268, 195);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(372, 195);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			ultraGroupBox1.Controls.Add(topNumberPicker);
			ultraGroupBox1.Controls.Add(label2);
			ultraGroupBox1.Controls.Add(radioButtonBySalesProfit);
			ultraGroupBox1.Controls.Add(radioButtonBySalesQty);
			ultraGroupBox1.Controls.Add(radioButtonBySalesValue);
			ultraGroupBox1.Controls.Add(label1);
			ultraGroupBox1.Location = new System.Drawing.Point(27, 75);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(441, 96);
			ultraGroupBox1.TabIndex = 6;
			ultraGroupBox1.Text = "Filter";
			topNumberPicker.Location = new System.Drawing.Point(86, 61);
			topNumberPicker.Maximum = new decimal(new int[4]
			{
				25,
				0,
				0,
				0
			});
			topNumberPicker.Name = "topNumberPicker";
			topNumberPicker.Size = new System.Drawing.Size(81, 20);
			topNumberPicker.TabIndex = 5;
			topNumberPicker.Value = new decimal(new int[4]
			{
				25,
				0,
				0,
				0
			});
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(16, 63);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(29, 13);
			label2.TabIndex = 4;
			label2.Text = "Top:";
			radioButtonBySalesProfit.AutoSize = true;
			radioButtonBySalesProfit.Location = new System.Drawing.Point(289, 32);
			radioButtonBySalesProfit.Name = "radioButtonBySalesProfit";
			radioButtonBySalesProfit.Size = new System.Drawing.Size(78, 17);
			radioButtonBySalesProfit.TabIndex = 3;
			radioButtonBySalesProfit.Text = "Sales Profit";
			radioButtonBySalesProfit.UseVisualStyleBackColor = true;
			radioButtonBySalesQty.AutoSize = true;
			radioButtonBySalesQty.Location = new System.Drawing.Point(179, 32);
			radioButtonBySalesQty.Name = "radioButtonBySalesQty";
			radioButtonBySalesQty.Size = new System.Drawing.Size(93, 17);
			radioButtonBySalesQty.TabIndex = 2;
			radioButtonBySalesQty.Text = "Sales Quantity";
			radioButtonBySalesQty.UseVisualStyleBackColor = true;
			radioButtonBySalesValue.AutoSize = true;
			radioButtonBySalesValue.Checked = true;
			radioButtonBySalesValue.Location = new System.Drawing.Point(84, 32);
			radioButtonBySalesValue.Name = "radioButtonBySalesValue";
			radioButtonBySalesValue.Size = new System.Drawing.Size(81, 17);
			radioButtonBySalesValue.TabIndex = 1;
			radioButtonBySalesValue.TabStop = true;
			radioButtonBySalesValue.Text = "Sales Value";
			radioButtonBySalesValue.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(16, 34);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(41, 13);
			label1.TabIndex = 0;
			label1.Text = "List By:";
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(27, 12);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(329, 57);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2017, 10, 18, 23, 59, 59, 59);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(480, 226);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "CustomerTopListReport";
			Text = "Customer Top List Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)topNumberPicker).EndInit();
			ResumeLayout(false);
		}
	}
}
