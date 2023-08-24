using DevExpress.XtraReports.UI;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Jobs
{
	public class VehicleMaintenanceEntryReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private JobSelector jobCostSelector;

		private UltraGroupBox ultraGroupBox1;

		private GroupBox groupBoxJob;

		private DateControl dateControl1;

		private Button buttonOK;

		private VehicleSelector vehicleSelector;

		private ServiceSelector serviceSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7002;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public VehicleMaintenanceEntryReport()
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

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DataSet data = Factory.MaintenanceEntrySystem.GetMaintenanceEntryReport(dateControl1.FromDate, dateControl1.ToDate, vehicleSelector.FromVehicle, vehicleSelector.ToVehicle, serviceSelector.FromServiceItem, serviceSelector.ToServiceItem);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Vehicle Maintenance Entry");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Vehicle Maintenance Entry Report.repx'");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Jobs.VehicleMaintenanceEntryReport));
			panelButtons = new System.Windows.Forms.Panel();
			buttonOK = new System.Windows.Forms.Button();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			vehicleSelector = new Micromind.DataControls.VehicleSelector();
			groupBoxJob = new System.Windows.Forms.GroupBox();
			serviceSelector = new Micromind.DataControls.ServiceSelector();
			dateControl1 = new Micromind.DataControls.DateControl();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			groupBoxJob.SuspendLayout();
			SuspendLayout();
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 243);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(487, 40);
			panelButtons.TabIndex = 2;
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
			buttonOK.Location = new System.Drawing.Point(269, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(102, 24);
			buttonOK.TabIndex = 15;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(487, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(377, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			ultraGroupBox1.Controls.Add(vehicleSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(16, 8);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(451, 67);
			ultraGroupBox1.TabIndex = 11;
			ultraGroupBox1.Text = "Vehicle";
			vehicleSelector.BackColor = System.Drawing.Color.Transparent;
			vehicleSelector.CustomReportFieldName = "";
			vehicleSelector.CustomReportKey = "";
			vehicleSelector.CustomReportValueType = 1;
			vehicleSelector.Location = new System.Drawing.Point(6, 13);
			vehicleSelector.Name = "vehicleSelector";
			vehicleSelector.Size = new System.Drawing.Size(430, 48);
			vehicleSelector.TabIndex = 0;
			groupBoxJob.Controls.Add(serviceSelector);
			groupBoxJob.Location = new System.Drawing.Point(14, 84);
			groupBoxJob.Name = "groupBoxJob";
			groupBoxJob.Size = new System.Drawing.Size(453, 69);
			groupBoxJob.TabIndex = 14;
			groupBoxJob.TabStop = false;
			groupBoxJob.Text = "Service Item";
			serviceSelector.BackColor = System.Drawing.Color.Transparent;
			serviceSelector.CustomReportFieldName = "";
			serviceSelector.CustomReportKey = "";
			serviceSelector.CustomReportValueType = 1;
			serviceSelector.Location = new System.Drawing.Point(8, 15);
			serviceSelector.Name = "serviceSelector";
			serviceSelector.Size = new System.Drawing.Size(430, 48);
			serviceSelector.TabIndex = 0;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(16, 163);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(453, 77);
			dateControl1.TabIndex = 12;
			dateControl1.ToDate = new System.DateTime(2017, 1, 12, 23, 59, 59, 59);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(487, 283);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(groupBoxJob);
			base.Controls.Add(dateControl1);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "VehicleMaintenanceEntryReport";
			Text = "Vehicle Maintenance Entry Report";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			groupBoxJob.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
