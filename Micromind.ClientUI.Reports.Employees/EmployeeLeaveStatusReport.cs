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

namespace Micromind.ClientUI.Reports.Employees
{
	public class EmployeeLeaveStatusReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private DateControl dateControl1;

		private UltraGroupBox ultraGroupBox2;

		private LeaveSelector leaveSelector;

		private Label label2;

		private MMSDateTimePicker mmsDateTimePicker1;

		private UltraGroupBox ultraGroupBox1;

		private EmployeeSelector2 employeeSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsHR;

		public int ScreenID => 7021;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public EmployeeLeaveStatusReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				leaveSelector.IsPanelVisible = false;
				mmsDateTimePicker1.Value = DateTime.Now;
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
			DataSet data = Factory.EmployeeSystem.GetEmployeeLeaveStatusReport(employeeSelector.FromEmployee, employeeSelector.ToEmployee, employeeSelector.FromDepartment, employeeSelector.ToDepartment, employeeSelector.FromLocation, employeeSelector.ToLocation, mmsDateTimePicker1.Value, mmsDateTimePicker1.Value, employeeSelector.FromType, employeeSelector.ToType, employeeSelector.FromDivision, employeeSelector.ToDivision, employeeSelector.FromSponsor, employeeSelector.ToSponsor, employeeSelector.FromGroup, employeeSelector.ToGroup, employeeSelector.FromGrade, employeeSelector.ToGrade, employeeSelector.FromPosition, employeeSelector.ToPosition, employeeSelector.FromBank, employeeSelector.ToBank, employeeSelector.FromAccount, employeeSelector.ToAccount, leaveSelector.FromLeave, leaveSelector.ToLeave, employeeSelector.MultipleEmployees);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Employee Leave Status");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'Employee Leave Status.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Employees.EmployeeLeaveStatusReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			leaveSelector = new Micromind.DataControls.LeaveSelector();
			dateControl1 = new Micromind.DataControls.DateControl();
			label2 = new System.Windows.Forms.Label();
			mmsDateTimePicker1 = new Micromind.UISupport.MMSDateTimePicker(components);
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			employeeSelector = new Micromind.DataControls.EmployeeSelector2();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Location = new System.Drawing.Point(287, 500);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(391, 500);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			ultraGroupBox2.Controls.Add(leaveSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 353);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(476, 71);
			ultraGroupBox2.TabIndex = 7;
			ultraGroupBox2.Text = "Leaves";
			leaveSelector.BackColor = System.Drawing.Color.Transparent;
			leaveSelector.CustomReportFieldName = "";
			leaveSelector.CustomReportKey = "";
			leaveSelector.CustomReportValueType = 1;
			leaveSelector.IsPanelVisible = true;
			leaveSelector.Location = new System.Drawing.Point(6, 18);
			leaveSelector.Name = "leaveSelector";
			leaveSelector.Size = new System.Drawing.Size(414, 53);
			leaveSelector.TabIndex = 0;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 5, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(13, 480);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(272, 51);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2017, 5, 25, 23, 59, 59, 59);
			dateControl1.Visible = false;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(18, 435);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(57, 13);
			label2.TabIndex = 146;
			label2.Text = "As of Date";
			mmsDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			mmsDateTimePicker1.Location = new System.Drawing.Point(80, 433);
			mmsDateTimePicker1.Name = "mmsDateTimePicker1";
			mmsDateTimePicker1.Size = new System.Drawing.Size(143, 20);
			mmsDateTimePicker1.TabIndex = 145;
			mmsDateTimePicker1.Value = new System.DateTime(2014, 10, 30, 0, 0, 0, 0);
			ultraGroupBox1.Controls.Add(employeeSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(13, 17);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(457, 330);
			ultraGroupBox1.TabIndex = 148;
			ultraGroupBox1.Text = "Employees";
			employeeSelector.BackColor = System.Drawing.Color.Transparent;
			employeeSelector.CustomReportFieldName = "";
			employeeSelector.CustomReportKey = "";
			employeeSelector.CustomReportValueType = 1;
			employeeSelector.Location = new System.Drawing.Point(21, 18);
			employeeSelector.Name = "employeeSelector";
			employeeSelector.Size = new System.Drawing.Size(402, 306);
			employeeSelector.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(498, 561);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(label2);
			base.Controls.Add(mmsDateTimePicker1);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "EmployeeLeaveStatusReport";
			Text = "Employee Leave Status Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
