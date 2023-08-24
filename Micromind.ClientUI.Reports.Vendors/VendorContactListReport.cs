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
	public class VendorContactListReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private VendorSelector vendorSelector;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowInactive;

		private Button buttonClose;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsVendor;

		public int ScreenID => 7043;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public VendorContactListReport()
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
			DataSet data = Factory.VendorSystem.GetVendorPrimaryContactListReport(vendorSelector.FromVendor, vendorSelector.ToVendor, vendorSelector.FromClass, vendorSelector.ToClass, vendorSelector.FromGroup, vendorSelector.ToGroup, checkBoxShowInactive.Checked, vendorSelector.MultipleVendors);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Vendor Prim Contact List");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'Vendor Prim Contact List.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Vendors.VendorContactListReport));
			buttonOK = new System.Windows.Forms.Button();
			vendorSelector = new Micromind.DataControls.VendorSelector();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			checkBoxShowInactive = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(286, 211);
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
			vendorSelector.Size = new System.Drawing.Size(452, 122);
			vendorSelector.TabIndex = 2;
			ultraGroupBox1.Controls.Add(vendorSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 146);
			ultraGroupBox1.TabIndex = 3;
			ultraGroupBox1.Text = "Vendors";
			checkBoxShowInactive.AutoSize = true;
			checkBoxShowInactive.Location = new System.Drawing.Point(25, 178);
			checkBoxShowInactive.Name = "checkBoxShowInactive";
			checkBoxShowInactive.Size = new System.Drawing.Size(134, 17);
			checkBoxShowInactive.TabIndex = 4;
			checkBoxShowInactive.Text = "Show inactive vendors";
			checkBoxShowInactive.UseVisualStyleBackColor = true;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 211);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(497, 243);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowInactive);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "VendorContactListReport";
			Text = "Vendor Contact List Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
