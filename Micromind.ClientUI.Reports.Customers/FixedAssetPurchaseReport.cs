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
	public class FixedAssetPurchaseReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private DateControl dateControl1;

		private RadioButton radioButtonSummary;

		private RadioButton radioButtonDetail;

		private UltraGroupBox ultraGroupBox1;

		private FixedAssetSelector FixedAssetSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsPurchase;

		public int ScreenID => 7028;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public FixedAssetPurchaseReport()
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
				DataSet data = Factory.FixedAssetSystem.GetAssetPurchaseDetailReport(dateControl1.FromDate, dateControl1.ToDate, FixedAssetSelector.FromFixedAsset, FixedAssetSelector.ToFixedAsset, FixedAssetSelector.FromClass, FixedAssetSelector.ToClass, FixedAssetSelector.FromGroup, FixedAssetSelector.ToGroup);
				xtraReport = reportHelper.GetReport("Fixed Asset Purchase");
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'Fixed Asset Purchase.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Customers.FixedAssetPurchaseReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			dateControl1 = new Micromind.DataControls.DateControl();
			radioButtonSummary = new System.Windows.Forms.RadioButton();
			radioButtonDetail = new System.Windows.Forms.RadioButton();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			FixedAssetSelector = new Micromind.DataControls.FixedAssetSelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(272, 257);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(376, 257);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2015, 12, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 170);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(462, 65);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2015, 12, 10, 23, 59, 59, 59);
			radioButtonSummary.AutoSize = true;
			radioButtonSummary.Checked = true;
			radioButtonSummary.Location = new System.Drawing.Point(12, 241);
			radioButtonSummary.Name = "radioButtonSummary";
			radioButtonSummary.Size = new System.Drawing.Size(68, 17);
			radioButtonSummary.TabIndex = 2;
			radioButtonSummary.TabStop = true;
			radioButtonSummary.Text = "Summary";
			radioButtonSummary.UseVisualStyleBackColor = true;
			radioButtonSummary.Visible = false;
			radioButtonDetail.AutoSize = true;
			radioButtonDetail.Location = new System.Drawing.Point(96, 241);
			radioButtonDetail.Name = "radioButtonDetail";
			radioButtonDetail.Size = new System.Drawing.Size(52, 17);
			radioButtonDetail.TabIndex = 3;
			radioButtonDetail.Text = "Detail";
			radioButtonDetail.UseVisualStyleBackColor = true;
			radioButtonDetail.Visible = false;
			ultraGroupBox1.Controls.Add(FixedAssetSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(461, 152);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Fixed Asset";
			FixedAssetSelector.BackColor = System.Drawing.Color.Transparent;
			FixedAssetSelector.CustomReportFieldName = "";
			FixedAssetSelector.CustomReportKey = "";
			FixedAssetSelector.CustomReportValueType = 1;
			FixedAssetSelector.Location = new System.Drawing.Point(6, 18);
			FixedAssetSelector.Name = "FixedAssetSelector";
			FixedAssetSelector.Size = new System.Drawing.Size(444, 122);
			FixedAssetSelector.TabIndex = 1;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(483, 288);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(radioButtonDetail);
			base.Controls.Add(radioButtonSummary);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "FixedAssetPurchaseReport";
			Text = "Fixed Asset Purchase ";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
