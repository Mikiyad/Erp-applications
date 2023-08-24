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
	public class HRLetterReportForm : Form, IForm
	{
		private string reportType = "";

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private Label label2;

		private ComboBox comboBoxYear;

		private MonthComboBox comboBoxMonth;

		private UltraGroupBox ultraGroupBox1;

		private EmployeeSelector2 employeeSelector;

		private MMTextBox textBoxIssuedTo;

		private Label label1;

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

		public ScreenAreas ScreenArea => ScreenAreas.ReportsHR;

		public int ScreenID => 7017;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public HRLetterReportForm()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxMonth.LoadData();
				comboBoxMonth.Items.RemoveAt(0);
				int month = DateTime.Today.Month;
				comboBoxMonth.SelectedID = month;
				int year = DateTime.Today.Year;
				comboBoxYear.SelectedIndex = comboBoxYear.FindString(year.ToString());
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
			string text = "";
			checked
			{
				text = ReportType.Remove(ReportType.Length - 6).TrimEnd();
				string strGroupBy = "";
				if (text == "Salary Certificate")
				{
					strGroupBy = "1";
				}
				else if (text == "Confirmation Letter")
				{
					strGroupBy = "2";
				}
				else if (text == "Appointment Letter")
				{
					strGroupBy = "3";
				}
				else if (text == "Salary Increment Letter")
				{
					strGroupBy = "4";
				}
				else if (text == "Experience certificate")
				{
					strGroupBy = "5";
				}
				else if (text == "Last (N) Months Salary Slip")
				{
					strGroupBy = "6";
				}
				DataSet data = Factory.EmployeeSystem.GetHRLetterReport(employeeSelector.FromEmployee, employeeSelector.ToEmployee, employeeSelector.FromDepartment, employeeSelector.ToDepartment, employeeSelector.FromLocation, employeeSelector.ToLocation, employeeSelector.FromType, employeeSelector.ToType, employeeSelector.FromDivision, employeeSelector.ToDivision, employeeSelector.FromSponsor, employeeSelector.ToSponsor, employeeSelector.FromGroup, employeeSelector.ToGroup, employeeSelector.FromGrade, employeeSelector.ToGrade, employeeSelector.FromPosition, employeeSelector.ToPosition, employeeSelector.FromBank, employeeSelector.ToBank, employeeSelector.FromAccount, employeeSelector.ToAccount, comboBoxMonth.SelectedID + 1, Convert.ToInt32(comboBoxYear.SelectedItem), employeeSelector.MultipleEmployees, strGroupBy);
				if (!string.IsNullOrEmpty(textBoxIssuedTo.Text))
				{
					data.Tables[0].Columns.Add("IssuedTo");
					foreach (DataRow row in data.Tables[0].Rows)
					{
						row["IssuedTo"] = textBoxIssuedTo.Text;
					}
				}
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "Period : " + comboBoxMonth.SelectedText + " - " + comboBoxYear.SelectedItem;
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				XtraReport report = reportHelper.GetReport(text);
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file, " + text + ", Please make sure you have access to reports path and the files are not corrupted.");
					return;
				}
				report.DataSource = data;
				reportHelper.ShowReport(report);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Employees.HRLetterReportForm));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			comboBoxYear = new System.Windows.Forms.ComboBox();
			comboBoxMonth = new Micromind.DataControls.MonthComboBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			employeeSelector = new Micromind.DataControls.EmployeeSelector2();
			textBoxIssuedTo = new Micromind.UISupport.MMTextBox();
			label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Location = new System.Drawing.Point(241, 492);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(345, 492);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(7, 366);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(40, 13);
			label2.TabIndex = 23;
			label2.Text = "Period:";
			comboBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxYear.FormattingEnabled = true;
			comboBoxYear.Items.AddRange(new object[51]
			{
				"2000",
				"2001",
				"2002",
				"2003",
				"2004",
				"2005",
				"2006",
				"2007",
				"2008",
				"2009",
				"2010",
				"2011",
				"2012",
				"2013",
				"2014",
				"2015",
				"2016",
				"2017",
				"2018",
				"2019",
				"2020",
				"2021",
				"2022",
				"2023",
				"2024",
				"2025",
				"2026",
				"2027",
				"2028",
				"2029",
				"2030",
				"2031",
				"2032",
				"2033",
				"2034",
				"2035",
				"2036",
				"2037",
				"2038",
				"2039",
				"2040",
				"2041",
				"2042",
				"2043",
				"2044",
				"2045",
				"2046",
				"2047",
				"2048",
				"2049",
				"2050"
			});
			comboBoxYear.Location = new System.Drawing.Point(187, 362);
			comboBoxYear.Name = "comboBoxYear";
			comboBoxYear.Size = new System.Drawing.Size(88, 21);
			comboBoxYear.TabIndex = 27;
			comboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMonth.FormattingEnabled = true;
			comboBoxMonth.IsMonthNumbers = false;
			comboBoxMonth.Location = new System.Drawing.Point(75, 362);
			comboBoxMonth.Name = "comboBoxMonth";
			comboBoxMonth.Size = new System.Drawing.Size(107, 21);
			comboBoxMonth.TabIndex = 26;
			ultraGroupBox1.Controls.Add(employeeSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(5, 8);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(457, 332);
			ultraGroupBox1.TabIndex = 28;
			ultraGroupBox1.Text = "Employees";
			employeeSelector.BackColor = System.Drawing.Color.Transparent;
			employeeSelector.CustomReportFieldName = "";
			employeeSelector.CustomReportKey = "";
			employeeSelector.CustomReportValueType = 1;
			employeeSelector.Location = new System.Drawing.Point(21, 18);
			employeeSelector.Name = "employeeSelector";
			employeeSelector.Size = new System.Drawing.Size(402, 308);
			employeeSelector.TabIndex = 0;
			textBoxIssuedTo.CustomReportFieldName = "";
			textBoxIssuedTo.CustomReportKey = "";
			textBoxIssuedTo.CustomReportValueType = 1;
			textBoxIssuedTo.IsComboTextBox = false;
			textBoxIssuedTo.IsModified = false;
			textBoxIssuedTo.Location = new System.Drawing.Point(75, 396);
			textBoxIssuedTo.Multiline = true;
			textBoxIssuedTo.Name = "textBoxIssuedTo";
			textBoxIssuedTo.Size = new System.Drawing.Size(353, 79);
			textBoxIssuedTo.TabIndex = 29;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(7, 399);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(57, 13);
			label1.TabIndex = 30;
			label1.Text = "Issued To:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(463, 524);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxIssuedTo);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(comboBoxYear);
			base.Controls.Add(comboBoxMonth);
			base.Controls.Add(label2);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "HRLetterReportForm";
			Text = "HR Letter Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
