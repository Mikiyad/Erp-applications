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
	public class FixedAssetTransferReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private DateControl dateControl1;

		private CheckBox checkBoxbyAsset;

		private CheckBox checkBoxShowInactiveItems;

		private FixedAssetSelector FixedAssetSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7022;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public FixedAssetTransferReport()
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
			DataSet data = Factory.FixedAssetSystem.GetAssetTransferReport(dateControl1.FromDate, dateControl1.ToDate, FixedAssetSelector.FromLocation, FixedAssetSelector.ToLocation, FixedAssetSelector.FromFixedAsset, FixedAssetSelector.ToFixedAsset, FixedAssetSelector.FromClass, FixedAssetSelector.ToClass, FixedAssetSelector.FromGroup, FixedAssetSelector.ToGroup, checkBoxbyAsset.Checked, checkBoxShowInactiveItems.Checked);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Fixed Asset Transfer");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Fixed Asset Transfer’.repx'");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Customers.FixedAssetTransferReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			FixedAssetSelector = new Micromind.DataControls.FixedAssetSelector();
			buttonClose = new System.Windows.Forms.Button();
			dateControl1 = new Micromind.DataControls.DateControl();
			checkBoxbyAsset = new System.Windows.Forms.CheckBox();
			checkBoxShowInactiveItems = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(262, 277);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(102, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(FixedAssetSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(453, 153);
			ultraGroupBox1.TabIndex = 3;
			ultraGroupBox1.Text = "Fixed Asset";
			FixedAssetSelector.BackColor = System.Drawing.Color.Transparent;
			FixedAssetSelector.CustomReportFieldName = "";
			FixedAssetSelector.CustomReportKey = "";
			FixedAssetSelector.CustomReportValueType = 1;
			FixedAssetSelector.Location = new System.Drawing.Point(7, 19);
			FixedAssetSelector.Name = "FixedAssetSelector";
			FixedAssetSelector.Size = new System.Drawing.Size(438, 129);
			FixedAssetSelector.TabIndex = 1;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(366, 277);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(102, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2015, 12, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 171);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(453, 77);
			dateControl1.TabIndex = 6;
			dateControl1.ToDate = new System.DateTime(2015, 12, 15, 23, 59, 59, 59);
			checkBoxbyAsset.AutoSize = true;
			checkBoxbyAsset.Location = new System.Drawing.Point(19, 241);
			checkBoxbyAsset.Name = "checkBoxbyAsset";
			checkBoxbyAsset.Size = new System.Drawing.Size(67, 17);
			checkBoxbyAsset.TabIndex = 9;
			checkBoxbyAsset.Text = "By Asset";
			checkBoxbyAsset.UseVisualStyleBackColor = true;
			checkBoxbyAsset.Visible = false;
			checkBoxShowInactiveItems.AutoSize = true;
			checkBoxShowInactiveItems.Location = new System.Drawing.Point(19, 264);
			checkBoxShowInactiveItems.Name = "checkBoxShowInactiveItems";
			checkBoxShowInactiveItems.Size = new System.Drawing.Size(122, 17);
			checkBoxShowInactiveItems.TabIndex = 10;
			checkBoxShowInactiveItems.Text = "Show Inactive Items";
			checkBoxShowInactiveItems.UseVisualStyleBackColor = true;
			checkBoxShowInactiveItems.Visible = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(478, 307);
			base.Controls.Add(checkBoxShowInactiveItems);
			base.Controls.Add(checkBoxbyAsset);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "FixedAssetTransferReport";
			Text = "Fixed Asset Transfer";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
