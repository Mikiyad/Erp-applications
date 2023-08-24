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
	public class ChequeMaturityReportForm : Form, IForm
	{
		private string selectedText = "";

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private GroupBox groupBox1;

		private BankAccountSelector accountSelector;

		private DateControl dateControl1;

		private UltraGroupBox ultraGroupBox1;

		private CustomerSelector customerSelector;

		private DateControl dateControl2;

		private Label label3;

		private Label label1;

		private UltraGroupBox ultraGroupBox2;

		private LocationSelector locationSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7003;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public ChequeMaturityReportForm()
		{
			InitializeComponent();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet data = Factory.ReceivedChequeSystem.GetChequeMaturityReport(dateControl1.FromDate, dateControl1.ToDate, dateControl2.FromDate, dateControl2.ToDate, accountSelector.FromAccount, accountSelector.ToAccount, customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, cleared: true, bounced: false, cancelled: false, customerSelector.MultipleCustomers);
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				XtraReport report = reportHelper.GetReport("Cheque Maturity Report");
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'Cheque Maturity Report.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
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

		private void PDCIssuedReportForm_Load(object sender, EventArgs e)
		{
			try
			{
				dateControl1.SelectedPeriod = DatePeriods.AllDates;
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

		private void radioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				selectedText = ((RadioButton)sender).Text;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.ChequeMaturityReportForm));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			groupBox1 = new System.Windows.Forms.GroupBox();
			accountSelector = new Micromind.DataControls.BankAccountSelector();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			label3 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			dateControl2 = new Micromind.DataControls.DateControl();
			dateControl1 = new Micromind.DataControls.DateControl();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			panelButtons.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(367, 8);
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
			panelButtons.Location = new System.Drawing.Point(0, 439);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(577, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(577, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(467, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			groupBox1.Controls.Add(accountSelector);
			groupBox1.Location = new System.Drawing.Point(12, 9);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(476, 69);
			groupBox1.TabIndex = 4;
			groupBox1.TabStop = false;
			groupBox1.Text = "Accounts";
			accountSelector.BackColor = System.Drawing.Color.Transparent;
			accountSelector.CustomReportFieldName = "";
			accountSelector.CustomReportKey = "";
			accountSelector.CustomReportValueType = 1;
			accountSelector.Location = new System.Drawing.Point(11, 13);
			accountSelector.Name = "accountSelector";
			accountSelector.Size = new System.Drawing.Size(430, 48);
			accountSelector.TabIndex = 1;
			ultraGroupBox1.Controls.Add(customerSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 84);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 182);
			ultraGroupBox1.TabIndex = 6;
			ultraGroupBox1.Text = "Customers";
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(6, 19);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(464, 163);
			customerSelector.TabIndex = 0;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(14, 352);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(84, 13);
			label3.TabIndex = 18;
			label3.Text = "Deposited Date:";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(298, 352);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(73, 13);
			label1.TabIndex = 19;
			label1.Text = "Cheque Date:";
			dateControl2.CustomReportFieldName = "";
			dateControl2.CustomReportKey = "";
			dateControl2.CustomReportValueType = 1;
			dateControl2.FromDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
			dateControl2.Location = new System.Drawing.Point(291, 368);
			dateControl2.Name = "dateControl2";
			dateControl2.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl2.Size = new System.Drawing.Size(286, 68);
			dateControl2.TabIndex = 7;
			dateControl2.ToDate = new System.DateTime(2017, 11, 8, 23, 59, 59, 59);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(11, 368);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(283, 68);
			dateControl1.TabIndex = 5;
			dateControl1.ToDate = new System.DateTime(2017, 11, 8, 23, 59, 59, 59);
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(17, 265);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(471, 81);
			ultraGroupBox2.TabIndex = 22;
			ultraGroupBox2.Text = "Location";
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 18);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(577, 479);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(label1);
			base.Controls.Add(label3);
			base.Controls.Add(dateControl2);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(groupBox1);
			base.Controls.Add(dateControl1);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "ChequeMaturityReportForm";
			Text = "Cheque Maturity Report";
			base.Load += new System.EventHandler(PDCIssuedReportForm_Load);
			panelButtons.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
