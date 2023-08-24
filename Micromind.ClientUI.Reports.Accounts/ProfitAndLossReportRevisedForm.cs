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

namespace Micromind.ClientUI.Reports.Accounts
{
	public class ProfitAndLossReportRevisedForm : Form, IForm
	{
		private bool allowposting = CompanyPreferences.FinancialTransactionPosting;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private DateControl dateControl1;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private ComboBox comboBoxLevel;

		private Label labelLevel;

		private CheckBox checkBoxDetail;

		private UltraGroupBox ultraGroupBox1;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox2;

		private DivisionSelector divisionSelector;

		private PrintTemplateSelector printTemplateSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7007;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public ProfitAndLossReportRevisedForm()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxLevel.SelectedIndex = 1;
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
			DataSet dataSet = null;
			checked
			{
				if (checkBoxDetail.Checked)
				{
					dataSet = Factory.JournalSystem.GetProfitAndLossReport(dateControl1.FromDate, dateControl1.ToDate, locationSelector.FromLocation, locationSelector.ToLocation, divisionSelector.FromDivision, divisionSelector.ToDivision, comboBoxLevel.SelectedIndex + 1, allowposting);
					dataSet.Relations.Add("RelIncome", dataSet.Tables["Income"].Columns["GroupID"], dataSet.Tables["Income"].Columns["ParentID"], createConstraints: false);
					dataSet.Relations.Add("RelCOGS", dataSet.Tables["COGS"].Columns["GroupID"], dataSet.Tables["COGS"].Columns["ParentID"], createConstraints: false);
					dataSet.Relations.Add("RelExpense", dataSet.Tables["Expense"].Columns["GroupID"], dataSet.Tables["Expense"].Columns["ParentID"], createConstraints: false);
					dataSet.Relations.Add("RelAccIncome", dataSet.Tables["Income"].Columns["GroupID"], dataSet.Tables["Accounts"].Columns["GroupID"], createConstraints: false);
					dataSet.Relations.Add("RelAccCOGS", new DataColumn[2]
					{
						dataSet.Tables["COGS"].Columns["GroupID"],
						dataSet.Tables["COGS"].Columns["SubType"]
					}, new DataColumn[2]
					{
						dataSet.Tables["Accounts"].Columns["GroupID"],
						dataSet.Tables["Accounts"].Columns["SubType"]
					}, createConstraints: false);
					dataSet.Relations.Add("RelAccExpense", new DataColumn[2]
					{
						dataSet.Tables["Expense"].Columns["GroupID"],
						dataSet.Tables["Expense"].Columns["SubType"]
					}, new DataColumn[2]
					{
						dataSet.Tables["Accounts"].Columns["GroupID"],
						dataSet.Tables["Accounts"].Columns["SubType"]
					}, createConstraints: false);
					dataSet.Tables["Income"].Columns.Add("HasChild", typeof(bool));
					dataSet.Tables["COGS"].Columns.Add("HasChild", typeof(bool));
					dataSet.Tables["Expense"].Columns.Add("HasChild", typeof(bool));
					foreach (DataRow row in dataSet.Tables["Income"].Rows)
					{
						if (dataSet.Tables["Income"].Select("ParentID='" + row["GroupID"].ToString() + "'").Length != 0)
						{
							row["HasChild"] = true;
						}
						else
						{
							row["HasChild"] = false;
						}
					}
					foreach (DataRow row2 in dataSet.Tables["COGS"].Rows)
					{
						if (dataSet.Tables["COGS"].Select("ParentID='" + row2["GroupID"].ToString() + "'").Length != 0)
						{
							row2["HasChild"] = true;
						}
						else
						{
							row2["HasChild"] = false;
						}
					}
					foreach (DataRow row3 in dataSet.Tables["Expense"].Rows)
					{
						if (dataSet.Tables["Expense"].Select("ParentID='" + row3["GroupID"].ToString() + "'").Length != 0)
						{
							row3["HasChild"] = true;
						}
						else
						{
							row3["HasChild"] = false;
						}
					}
				}
				else
				{
					dataSet = Factory.JournalSystem.GetProfitAndLossReportSummaryRevised(dateControl1.FromDate, dateControl1.ToDate, locationSelector.FromLocation, locationSelector.ToLocation, divisionSelector.FromDivision, divisionSelector.ToDivision, comboBoxLevel.SelectedIndex + 1, allowposting);
					dataSet.Relations.Add("RelExpenses", dataSet.Tables["Expenses"].Columns["GroupID"], dataSet.Tables["Expenses"].Columns["ParentID"], createConstraints: false);
					dataSet.Relations.Add("RelAccExpenses", dataSet.Tables["Expenses"].Columns["GroupID"], dataSet.Tables["Accounts"].Columns["GroupID"], createConstraints: false);
					dataSet.Tables["Expenses"].Columns.Add("HasChild", typeof(bool));
					foreach (DataRow row4 in dataSet.Tables["Expenses"].Rows)
					{
						if (dataSet.Tables["Expenses"].Select("ParentID='" + row4["GroupID"].ToString() + "'").Length != 0)
						{
							row4["HasChild"] = true;
						}
						else
						{
							row4["HasChild"] = false;
						}
					}
				}
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref dataSet, reportFilter);
				reportHelper.AddFilterData(ref dataSet, GetAllFormControls(this));
				string text = "";
				text = ((!(printTemplateSelector.SelectedTemplate != "")) ? printTemplateSelector.DefaultTemplate : printTemplateSelector.SelectedTemplate);
				XtraReport report = reportHelper.GetReport(text);
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'" + text + ".repx'", "Please make sure you have access to reports path and the files are not corrupted.");
					return;
				}
				report.DataSource = dataSet;
				reportHelper.ShowReport(report);
			}
		}

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void checkBoxDetail_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxDetail.Checked)
			{
				Label label = labelLevel;
				bool visible = comboBoxLevel.Visible = true;
				label.Visible = visible;
				printTemplateSelector.DefaultTemplate = "Profit and Loss Levelwise";
			}
			else
			{
				printTemplateSelector.DefaultTemplate = "Profit and Loss Revised";
			}
		}

		private void checkBoxDetail_CheckStateChanged(object sender, EventArgs e)
		{
			if (checkBoxDetail.Checked)
			{
				Label label = labelLevel;
				bool visible = comboBoxLevel.Visible = true;
				label.Visible = visible;
				comboBoxLevel.SelectedIndex = 1;
			}
			else
			{
				Label label2 = labelLevel;
				bool visible = comboBoxLevel.Visible = true;
				label2.Visible = visible;
				comboBoxLevel.SelectedIndex = 1;
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.ProfitAndLossReportRevisedForm));
			dateControl1 = new Micromind.DataControls.DateControl();
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			comboBoxLevel = new System.Windows.Forms.ComboBox();
			labelLevel = new System.Windows.Forms.Label();
			checkBoxDetail = new System.Windows.Forms.CheckBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			divisionSelector = new Micromind.DataControls.DivisionSelector();
			printTemplateSelector = new Micromind.DataControls.PrintTemplateSelector();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(32, 12);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(363, 59);
			dateControl1.TabIndex = 0;
			dateControl1.Tag = "Date";
			dateControl1.ToDate = new System.DateTime(2017, 11, 16, 23, 59, 59, 59);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(276, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 237);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(486, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(486, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(376, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			comboBoxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxLevel.FormattingEnabled = true;
			comboBoxLevel.Items.AddRange(new object[5]
			{
				"1",
				"2",
				"3",
				"4",
				"5"
			});
			comboBoxLevel.Location = new System.Drawing.Point(132, 168);
			comboBoxLevel.Name = "comboBoxLevel";
			comboBoxLevel.Size = new System.Drawing.Size(138, 21);
			comboBoxLevel.TabIndex = 4;
			labelLevel.AutoSize = true;
			labelLevel.Location = new System.Drawing.Point(89, 172);
			labelLevel.Name = "labelLevel";
			labelLevel.Size = new System.Drawing.Size(36, 13);
			labelLevel.TabIndex = 8;
			labelLevel.Text = "Level:";
			checkBoxDetail.AutoSize = true;
			checkBoxDetail.Location = new System.Drawing.Point(32, 170);
			checkBoxDetail.Name = "checkBoxDetail";
			checkBoxDetail.Size = new System.Drawing.Size(53, 17);
			checkBoxDetail.TabIndex = 9;
			checkBoxDetail.Text = "Detail";
			checkBoxDetail.UseVisualStyleBackColor = true;
			checkBoxDetail.CheckedChanged += new System.EventHandler(checkBoxDetail_CheckedChanged);
			checkBoxDetail.CheckStateChanged += new System.EventHandler(checkBoxDetail_CheckStateChanged);
			ultraGroupBox1.Controls.Add(locationSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(32, 77);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(451, 81);
			ultraGroupBox1.TabIndex = 14;
			ultraGroupBox1.Text = "Location";
			ultraGroupBox1.Visible = false;
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(16, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			ultraGroupBox2.Controls.Add(divisionSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(29, 77);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(428, 81);
			ultraGroupBox2.TabIndex = 20;
			ultraGroupBox2.Text = "Division";
			divisionSelector.BackColor = System.Drawing.Color.Transparent;
			divisionSelector.CustomReportFieldName = "";
			divisionSelector.CustomReportKey = "";
			divisionSelector.CustomReportValueType = 1;
			divisionSelector.Location = new System.Drawing.Point(6, 19);
			divisionSelector.Name = "divisionSelector";
			divisionSelector.Size = new System.Drawing.Size(414, 54);
			divisionSelector.TabIndex = 0;
			printTemplateSelector.BackColor = System.Drawing.Color.Transparent;
			printTemplateSelector.CustomReportFieldName = "";
			printTemplateSelector.CustomReportKey = "";
			printTemplateSelector.CustomReportValueType = 1;
			printTemplateSelector.DefaultTemplate = "Profit and Loss Revised";
			printTemplateSelector.FormID = "ProfitAndLossReportRevisedForm";
			printTemplateSelector.Location = new System.Drawing.Point(24, 194);
			printTemplateSelector.Name = "printTemplateSelector";
			printTemplateSelector.Size = new System.Drawing.Size(366, 28);
			printTemplateSelector.TabIndex = 21;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(486, 277);
			base.Controls.Add(printTemplateSelector);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(checkBoxDetail);
			base.Controls.Add(labelLevel);
			base.Controls.Add(comboBoxLevel);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dateControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "ProfitAndLossReportRevisedForm";
			Text = "Profit and Loss Report Consolidated";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
