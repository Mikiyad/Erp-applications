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

namespace Micromind.ClientUI.Reports.Vendors
{
	public class VendorBalanceSummaryReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private VendorSelector vendorSelector;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowZero;

		private Button buttonClose;

		private RadioButton radioButtonFC;

		private RadioButton radioButtonBase;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsVendor;

		public int ScreenID => 7042;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public VendorBalanceSummaryReport()
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
			DataSet data = Factory.VendorSystem.GetVendorBalanceSummary(vendorSelector.FromVendor, vendorSelector.ToVendor, vendorSelector.FromClass, vendorSelector.ToClass, vendorSelector.FromGroup, vendorSelector.ToGroup, checkBoxShowZero.Checked, vendorSelector.MultipleVendors, radioButtonFC.Checked);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Vendor Balance Summary");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Vendor Balance Summary.repx'");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Vendors.VendorBalanceSummaryReport));
			buttonOK = new System.Windows.Forms.Button();
			vendorSelector = new Micromind.DataControls.VendorSelector();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			checkBoxShowZero = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			radioButtonFC = new System.Windows.Forms.RadioButton();
			radioButtonBase = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Location = new System.Drawing.Point(286, 199);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			vendorSelector.BackColor = System.Drawing.Color.Transparent;
			vendorSelector.CustomReportFieldName = "";
			vendorSelector.CustomReportKey = "";
			vendorSelector.CustomReportValueType = 1;
			vendorSelector.Location = new System.Drawing.Point(6, 19);
			vendorSelector.Name = "vendorSelector";
			vendorSelector.Size = new System.Drawing.Size(464, 133);
			vendorSelector.TabIndex = 2;
			ultraGroupBox1.Controls.Add(vendorSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 152);
			ultraGroupBox1.TabIndex = 3;
			ultraGroupBox1.Text = "Vendors";
			checkBoxShowZero.AutoSize = true;
			checkBoxShowZero.Location = new System.Drawing.Point(25, 209);
			checkBoxShowZero.Name = "checkBoxShowZero";
			checkBoxShowZero.Size = new System.Drawing.Size(158, 17);
			checkBoxShowZero.TabIndex = 4;
			checkBoxShowZero.Text = "Show zero balance vendors";
			checkBoxShowZero.UseVisualStyleBackColor = true;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 199);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			radioButtonFC.AutoSize = true;
			radioButtonFC.Location = new System.Drawing.Point(147, 180);
			radioButtonFC.Name = "radioButtonFC";
			radioButtonFC.Size = new System.Drawing.Size(105, 17);
			radioButtonFC.TabIndex = 7;
			radioButtonFC.Text = "Foreign Currency";
			radioButtonFC.UseVisualStyleBackColor = true;
			radioButtonBase.AutoSize = true;
			radioButtonBase.Checked = true;
			radioButtonBase.Location = new System.Drawing.Point(25, 180);
			radioButtonBase.Name = "radioButtonBase";
			radioButtonBase.Size = new System.Drawing.Size(94, 17);
			radioButtonBase.TabIndex = 6;
			radioButtonBase.TabStop = true;
			radioButtonBase.Text = "Base Currency";
			radioButtonBase.UseVisualStyleBackColor = true;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(497, 244);
			base.Controls.Add(radioButtonFC);
			base.Controls.Add(radioButtonBase);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowZero);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "VendorBalanceSummaryReport";
			Text = "Vendor Balance Summary";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
