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
	public class EmployeeAnnualLeaveDueReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private MMLabel mmLabel3;

		private RadioButton radioButtonCD;

		private RadioButton radioButtonOA;

		private Label label2;

		private MMSDateTimePicker dateControl1;

		private EmployeeSelector2 employeeSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsHR;

		public int ScreenID => 7021;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public EmployeeAnnualLeaveDueReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				dateControl1.Value = DateTime.Now;
				radioButtonOA.Checked = true;
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
			object obj = null;
			if (radioButtonCD.Checked)
			{
				obj = "CD";
			}
			else if (radioButtonOA.Checked)
			{
				obj = "OA";
			}
			else if (!radioButtonCD.Checked && !radioButtonOA.Checked)
			{
				obj = "OA";
				radioButtonOA.Checked = true;
			}
			DataSet data = Factory.EmployeeSystem.GetEmployeeAnnualDueReport(employeeSelector.FromEmployee, employeeSelector.ToEmployee, employeeSelector.FromDepartment, employeeSelector.ToDepartment, employeeSelector.FromLocation, employeeSelector.ToLocation, dateControl1.Value, dateControl1.Value, obj, employeeSelector.FromType, employeeSelector.ToType, employeeSelector.FromDivision, employeeSelector.ToDivision, employeeSelector.FromSponsor, employeeSelector.ToSponsor, employeeSelector.FromGroup, employeeSelector.ToGroup, employeeSelector.FromGrade, employeeSelector.ToGrade, employeeSelector.FromPosition, employeeSelector.ToPosition, employeeSelector.FromBank, employeeSelector.ToBank, employeeSelector.FromAccount, employeeSelector.ToAccount, employeeSelector.MultipleEmployees);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "As of:" + dateControl1.Value.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			string text = "";
			text = ((obj != "OA") ? "Employee Annual Leave Due" : "Employee Annual Leave OA Due");
			XtraReport report = reportHelper.GetReport(text);
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'" + text + ".repx'", "Please make sure you have access to reports path and the files are not corrupted.");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Employees.EmployeeAnnualLeaveDueReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			employeeSelector = new Micromind.DataControls.EmployeeSelector2();
			buttonClose = new System.Windows.Forms.Button();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			radioButtonCD = new System.Windows.Forms.RadioButton();
			radioButtonOA = new System.Windows.Forms.RadioButton();
			label2 = new System.Windows.Forms.Label();
			dateControl1 = new Micromind.UISupport.MMSDateTimePicker();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Location = new System.Drawing.Point(267, 401);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(employeeSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 2);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(457, 333);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Employees";
			employeeSelector.BackColor = System.Drawing.Color.Transparent;
			employeeSelector.CustomReportFieldName = "";
			employeeSelector.CustomReportKey = "";
			employeeSelector.CustomReportValueType = 1;
			employeeSelector.Location = new System.Drawing.Point(21, 18);
			employeeSelector.Name = "employeeSelector";
			employeeSelector.Size = new System.Drawing.Size(402, 308);
			employeeSelector.TabIndex = 0;
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(371, 401);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(16, 378);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(73, 13);
			mmLabel3.TabIndex = 142;
			mmLabel3.Text = "Leave Based:";
			radioButtonCD.AutoSize = true;
			radioButtonCD.Location = new System.Drawing.Point(201, 377);
			radioButtonCD.Name = "radioButtonCD";
			radioButtonCD.Size = new System.Drawing.Size(94, 17);
			radioButtonCD.TabIndex = 141;
			radioButtonCD.TabStop = true;
			radioButtonCD.Text = "Calendar Days";
			radioButtonCD.UseVisualStyleBackColor = true;
			radioButtonOA.AutoSize = true;
			radioButtonOA.Location = new System.Drawing.Point(104, 377);
			radioButtonOA.Name = "radioButtonOA";
			radioButtonOA.Size = new System.Drawing.Size(82, 17);
			radioButtonOA.TabIndex = 140;
			radioButtonOA.TabStop = true;
			radioButtonOA.Text = "On Account";
			radioButtonOA.UseVisualStyleBackColor = true;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(16, 348);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(57, 13);
			label2.TabIndex = 144;
			label2.Text = "As of Date";
			dateControl1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateControl1.Location = new System.Drawing.Point(78, 346);
			dateControl1.Name = "dateControl1";
			dateControl1.Size = new System.Drawing.Size(143, 20);
			dateControl1.TabIndex = 143;
			dateControl1.Value = new System.DateTime(2014, 10, 30, 0, 0, 0, 0);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(484, 442);
			base.Controls.Add(label2);
			base.Controls.Add(dateControl1);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(radioButtonCD);
			base.Controls.Add(radioButtonOA);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "EmployeeAnnualLeaveDueReport";
			Text = "Employee Annual Leave Due Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
