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
	public class BalanceSheetReportForm : Form, IForm
	{
		private bool allowposting = CompanyPreferences.FinancialTransactionPosting;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private MMSDateTimePicker dateTimePickerDate;

		private Label label1;

		private ComboBox comboBoxLevel;

		private Label label2;

		private CheckBox checkBoxAccount;

		private UltraGroupBox ultraGroupBox2;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox1;

		private DivisionSelector divisionSelector;

		private PrintTemplateSelector printTemplateSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7003;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public BalanceSheetReportForm()
		{
			InitializeComponent();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				DateTime value = dateTimePickerDate.Value;
				value = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
				DataSet data = Factory.JournalSystem.GetBalanceSheetReport(value, locationSelector.FromLocation, locationSelector.ToLocation, divisionSelector.FromDivision, divisionSelector.ToDivision, checked(comboBoxLevel.SelectedIndex + 1), checkBoxAccount.Checked, allowposting);
				data.Relations.Add("RelAsset", data.Tables["Asset"].Columns["GroupID"], data.Tables["Asset"].Columns["ParentID"], createConstraints: false);
				data.Relations.Add("RelLiability", data.Tables["Liability"].Columns["GroupID"], data.Tables["Liability"].Columns["ParentID"], createConstraints: false);
				data.Relations.Add("RelEquity", data.Tables["Equity"].Columns["GroupID"], data.Tables["Equity"].Columns["ParentID"], createConstraints: false);
				data.Relations.Add("RelAccAsset", data.Tables["Asset"].Columns["GroupID"], data.Tables["Accounts"].Columns["GroupID"], createConstraints: false);
				data.Relations.Add("RelAccLiability", data.Tables["Liability"].Columns["GroupID"], data.Tables["Accounts"].Columns["GroupID"], createConstraints: false);
				data.Relations.Add("RelAccEquity", data.Tables["Equity"].Columns["GroupID"], data.Tables["Accounts"].Columns["GroupID"], createConstraints: false);
				data.Tables["Asset"].Columns.Add("HasChild", typeof(bool));
				data.Tables["Liability"].Columns.Add("HasChild", typeof(bool));
				data.Tables["Equity"].Columns.Add("HasChild", typeof(bool));
				foreach (DataRow row in data.Tables["Asset"].Rows)
				{
					if (data.Tables["Asset"].Select("ParentID='" + row["GroupID"].ToString() + "'").Length != 0)
					{
						row["HasChild"] = true;
					}
					else
					{
						row["HasChild"] = false;
					}
				}
				foreach (DataRow row2 in data.Tables["Liability"].Rows)
				{
					if (data.Tables["Liability"].Select("ParentID='" + row2["GroupID"].ToString() + "'").Length != 0)
					{
						row2["HasChild"] = true;
					}
					else
					{
						row2["HasChild"] = false;
					}
				}
				foreach (DataRow row3 in data.Tables["Equity"].Rows)
				{
					if (data.Tables["Equity"].Select("ParentID='" + row3["GroupID"].ToString() + "'").Length != 0)
					{
						row3["HasChild"] = true;
					}
					else
					{
						row3["HasChild"] = false;
					}
				}
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "As Of : " + dateTimePickerDate.Value.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				string text = "";
				text = ((!(printTemplateSelector.SelectedTemplate != "")) ? printTemplateSelector.DefaultTemplate : printTemplateSelector.SelectedTemplate);
				XtraReport report = reportHelper.GetReport(text);
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'" + text + ".repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				}
				else
				{
					report.DataSource = data;
					reportHelper.ShowReport(report);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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

		private void BalanceSheetReportForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxLevel.SelectedIndex = 1;
				dateTimePickerDate.Value = DateTime.Today;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.BalanceSheetReportForm));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			label1 = new System.Windows.Forms.Label();
			comboBoxLevel = new System.Windows.Forms.ComboBox();
			label2 = new System.Windows.Forms.Label();
			checkBoxAccount = new System.Windows.Forms.CheckBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			divisionSelector = new Micromind.DataControls.DivisionSelector();
			printTemplateSelector = new Micromind.DataControls.PrintTemplateSelector();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(239, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 0;
			buttonOK.Text = "Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 226);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(449, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(449, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(339, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(83, 16);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(138, 20);
			dateTimePickerDate.TabIndex = 0;
			dateTimePickerDate.Tag = "As Of";
			dateTimePickerDate.Value = new System.DateTime(2017, 11, 1, 9, 25, 49, 631);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(8, 20);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(34, 13);
			label1.TabIndex = 5;
			label1.Text = "As of:";
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
			comboBoxLevel.Location = new System.Drawing.Point(83, 42);
			comboBoxLevel.Name = "comboBoxLevel";
			comboBoxLevel.Size = new System.Drawing.Size(138, 21);
			comboBoxLevel.TabIndex = 1;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(8, 45);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(66, 13);
			label2.TabIndex = 7;
			label2.Text = "Detail Level:";
			checkBoxAccount.AutoSize = true;
			checkBoxAccount.Location = new System.Drawing.Point(11, 164);
			checkBoxAccount.Name = "checkBoxAccount";
			checkBoxAccount.Size = new System.Drawing.Size(135, 17);
			checkBoxAccount.TabIndex = 2;
			checkBoxAccount.Text = "Display detail accounts";
			checkBoxAccount.UseVisualStyleBackColor = true;
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(8, 69);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(413, 81);
			ultraGroupBox2.TabIndex = 12;
			ultraGroupBox2.Text = "Location";
			ultraGroupBox2.Visible = false;
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 18);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			ultraGroupBox1.Controls.Add(divisionSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(8, 69);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(413, 81);
			ultraGroupBox1.TabIndex = 19;
			ultraGroupBox1.Text = "Division";
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
			printTemplateSelector.DefaultTemplate = "Balance Sheet";
			printTemplateSelector.FormID = "BalanceSheetReportForm";
			printTemplateSelector.Location = new System.Drawing.Point(8, 185);
			printTemplateSelector.Name = "printTemplateSelector";
			printTemplateSelector.Size = new System.Drawing.Size(366, 28);
			printTemplateSelector.TabIndex = 25;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(449, 266);
			base.Controls.Add(printTemplateSelector);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(checkBoxAccount);
			base.Controls.Add(label2);
			base.Controls.Add(comboBoxLevel);
			base.Controls.Add(label1);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "BalanceSheetReportForm";
			Text = "Balance Sheet Report";
			base.Load += new System.EventHandler(BalanceSheetReportForm_Load);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
