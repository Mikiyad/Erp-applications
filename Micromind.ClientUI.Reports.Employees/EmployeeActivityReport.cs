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

namespace Micromind.ClientUI.Reports.Employees
{
	public class EmployeeActivityReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private CheckBox checkBoxShowInactive;

		private Button buttonClose;

		private UltraGroupBox ultraGroupBox1;

		private EmployeeSelector2 employeeSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsHR;

		public int ScreenID => 7016;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public EmployeeActivityReport()
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
			DataSet data = Factory.EmployeeSystem.GetEmployeeActivityReport(employeeSelector.FromEmployee, employeeSelector.ToEmployee, employeeSelector.FromDepartment, employeeSelector.ToDepartment, employeeSelector.FromLocation, employeeSelector.ToLocation, employeeSelector.FromType, employeeSelector.ToType, employeeSelector.FromDivision, employeeSelector.ToDivision, employeeSelector.FromSponsor, employeeSelector.ToSponsor, employeeSelector.FromGroup, employeeSelector.ToGroup, employeeSelector.FromGrade, employeeSelector.ToGrade, employeeSelector.FromPosition, employeeSelector.ToPosition, employeeSelector.FromBank, employeeSelector.ToBank, employeeSelector.FromAccount, employeeSelector.ToAccount, checkBoxShowInactive.Checked, employeeSelector.MultipleEmployees);
			if (data.Tables.Count >= 1)
			{
				data.Tables[1].Columns.Add("ActivityTypeName");
				foreach (DataRow row in data.Tables[1].Rows)
				{
					if (row["ActivityType"] != DBNull.Value)
					{
						switch (int.Parse(row["ActivityType"].ToString()))
						{
						case 1:
							row["ActivityTypeName"] = "General";
							break;
						case 9:
							row["ActivityTypeName"] = "Disciplinary Action";
							break;
						case 2:
							row["ActivityTypeName"] = "Hire";
							break;
						case 7:
							row["ActivityTypeName"] = "Leave";
							break;
						case 4:
							row["ActivityTypeName"] = "Promotion";
							break;
						case 3:
							row["ActivityTypeName"] = "Rehire";
							break;
						case 12:
							row["ActivityTypeName"] = "Resumption";
							break;
						case 11:
							row["ActivityTypeName"] = "Salary Change";
							break;
						case 10:
							row["ActivityTypeName"] = "Termination";
							break;
						case 8:
							row["ActivityTypeName"] = "Training";
							break;
						case 5:
							row["ActivityTypeName"] = "Transfer";
							break;
						case 6:
							row["ActivityTypeName"] = "Vacation";
							break;
						}
					}
				}
			}
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Employee Activity");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Employee Activity.repx'");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Employees.EmployeeActivityReport));
			buttonOK = new System.Windows.Forms.Button();
			checkBoxShowInactive = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			employeeSelector = new Micromind.DataControls.EmployeeSelector2();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(286, 370);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			checkBoxShowInactive.AutoSize = true;
			checkBoxShowInactive.Location = new System.Drawing.Point(12, 342);
			checkBoxShowInactive.Name = "checkBoxShowInactive";
			checkBoxShowInactive.Size = new System.Drawing.Size(146, 17);
			checkBoxShowInactive.TabIndex = 4;
			checkBoxShowInactive.Text = "Show inactive employees";
			checkBoxShowInactive.UseVisualStyleBackColor = true;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 370);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			ultraGroupBox1.Controls.Add(employeeSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(457, 324);
			ultraGroupBox1.TabIndex = 15;
			ultraGroupBox1.Text = "Employees";
			employeeSelector.BackColor = System.Drawing.Color.Transparent;
			employeeSelector.CustomReportFieldName = "";
			employeeSelector.CustomReportKey = "";
			employeeSelector.CustomReportValueType = 1;
			employeeSelector.Location = new System.Drawing.Point(21, 18);
			employeeSelector.Name = "employeeSelector";
			employeeSelector.Size = new System.Drawing.Size(402, 300);
			employeeSelector.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(497, 402);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowInactive);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "EmployeeActivityReport";
			Text = "Employee Activity Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
