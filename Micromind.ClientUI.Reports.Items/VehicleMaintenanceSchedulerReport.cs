using DevExpress.XtraReports.UI;
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

namespace Micromind.ClientUI.Reports.Items
{
	public class VehicleMaintenanceSchedulerReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private DateControl dateControl1;

		private GroupBox groupBoxCC;

		private VehicleSelector VehicleSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7022;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public VehicleMaintenanceSchedulerReport()
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
			DataSet data = Factory.MaintenanceSchedulerSystem.GetMaintenanceSchedulerReport(dateControl1.FromDate, dateControl1.ToDate, VehicleSelector.FromVehicle, VehicleSelector.ToVehicle);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Vehicle Maintenance Scheduler Report");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Vehicle Maintenance Scheduler Report.repx'");
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

		private void dateControl1_Load(object sender, EventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.VehicleMaintenanceSchedulerReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			groupBoxCC = new System.Windows.Forms.GroupBox();
			VehicleSelector = new Micromind.DataControls.VehicleSelector();
			dateControl1 = new Micromind.DataControls.DateControl();
			groupBoxCC.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(249, 180);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(102, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(353, 180);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(102, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			groupBoxCC.Controls.Add(VehicleSelector);
			groupBoxCC.Location = new System.Drawing.Point(2, 12);
			groupBoxCC.Name = "groupBoxCC";
			groupBoxCC.Size = new System.Drawing.Size(453, 69);
			groupBoxCC.TabIndex = 10;
			groupBoxCC.TabStop = false;
			groupBoxCC.Text = "Vehicle";
			VehicleSelector.BackColor = System.Drawing.Color.Transparent;
			VehicleSelector.CustomReportFieldName = "";
			VehicleSelector.CustomReportKey = "";
			VehicleSelector.CustomReportValueType = 1;
			VehicleSelector.Location = new System.Drawing.Point(6, 15);
			VehicleSelector.Name = "VehicleSelector";
			VehicleSelector.Size = new System.Drawing.Size(430, 48);
			VehicleSelector.TabIndex = 0;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(2, 98);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(453, 77);
			dateControl1.TabIndex = 6;
			dateControl1.ToDate = new System.DateTime(2017, 1, 12, 23, 59, 59, 59);
			dateControl1.Load += new System.EventHandler(dateControl1_Load);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(465, 210);
			base.Controls.Add(groupBoxCC);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "VehicleMaintenanceSchedulerReport";
			Text = "Vehicle Maintenance Scheduler";
			groupBoxCC.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
