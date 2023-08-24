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
	public class CashFlowReportForm : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private DateControl dateControl1;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox2;

		private LocationSelector locationSelector1;

		private PrintTemplateSelector printTemplateSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7004;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public CashFlowReportForm()
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

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DataSet data = Factory.JournalSystem.GetCashFlowReport(dateControl1.FromDate, dateControl1.ToDate, locationSelector1.FromLocation, locationSelector1.ToLocation);
			if (data.Tables.Count > 1)
			{
				data.Tables["CashFlow"].Columns.Add("SysDocTypeName");
				foreach (DataRow row in data.Tables["CashFlow"].Rows)
				{
					if (row["SysDocType"] != DBNull.Value)
					{
						int sysDocType = int.Parse(row["SysDocType"].ToString());
						row["SysDocTypeName"] = GetCashFlowSysDocTypeString(sysDocType);
					}
				}
			}
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			string text = "";
			text = ((!(printTemplateSelector.SelectedTemplate != "")) ? printTemplateSelector.DefaultTemplate : printTemplateSelector.SelectedTemplate);
			XtraReport report = reportHelper.GetReport(text);
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'" + text + ".repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		private string GetCashFlowSysDocTypeString(int sysDocType)
		{
			switch (sysDocType)
			{
			case 8:
				return "Operating Expenses";
			case 4:
				return "Payments";
			case 34:
				return "Purchases";
			case 35:
				return "Purchase Return";
			case 3:
				return "Cash Sale";
			case 28:
				return "Sales Return";
			case 7:
				return "Cheque Deposit";
			case 9:
				return "Expenses Paid by Cheque";
			case 5:
				return "Payment by Cheque";
			case 2:
				return "Received Cheque Payments";
			case 11:
				return "Credit Note";
			case 37:
				return "Credit Purchase Return";
			case 27:
				return "Credit Sales Return";
			case 10:
				return "Debit Note";
			case 41:
				return "Deposit";
			case 42:
				return "Expense";
			case 6:
				return "Transfer";
			case 1:
				return "Other Activities";
			case 39:
				return "Import Purchase Invoice";
			case 18:
				return "Inventory Adjustment";
			case 15:
				return "Issued Cheque Cancellation";
			case 14:
				return "Issued Cheque Clearance";
			case 16:
				return "Issued Cheque Return";
			case 17:
				return "Issued Security Chq";
			case 36:
				return "Packing List";
			case 33:
				return "Purchase Invoice";
			case 31:
				return "Purchase Order";
			case 30:
				return "Purchase Quote";
			case 32:
				return "Goods Received Note";
			case 13:
				return "Rec. Chq Cancellation";
			case 12:
				return "Returned Cheque";
			case 21:
				return "Stock Transfer Ret";
			case 25:
				return "Sales Invoice";
			case 23:
				return "Sales Order";
			case 22:
				return "Sales Quote";
			case 26:
				return "Cash Sale";
			case 20:
				return "Stock Transfer In";
			case 19:
				return "Stock Transfer Out";
			case 64:
				return "Bank Transfer Payment";
			case 65:
				return "Bank Transfer Receipt";
			case 44:
				return "Payroll Transaction";
			case 66:
				return "Cash Receipt Multiple Acc";
			default:
				return sysDocType.ToString();
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.CashFlowReportForm));
			dateControl1 = new Micromind.DataControls.DateControl();
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			locationSelector = new Micromind.DataControls.LocationSelector();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector1 = new Micromind.DataControls.LocationSelector();
			printTemplateSelector = new Micromind.DataControls.PrintTemplateSelector();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 12);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(377, 53);
			dateControl1.TabIndex = 0;
			dateControl1.Tag = "Date";
			dateControl1.ToDate = new System.DateTime(2017, 11, 20, 23, 59, 59, 59);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(261, 8);
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
			panelButtons.Location = new System.Drawing.Point(0, 185);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(471, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(471, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(361, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 18);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			ultraGroupBox2.Controls.Add(locationSelector1);
			ultraGroupBox2.Location = new System.Drawing.Point(9, 64);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(453, 81);
			ultraGroupBox2.TabIndex = 14;
			ultraGroupBox2.Text = "Location";
			locationSelector1.BackColor = System.Drawing.Color.Transparent;
			locationSelector1.CustomReportFieldName = "";
			locationSelector1.CustomReportKey = "";
			locationSelector1.CustomReportValueType = 1;
			locationSelector1.Location = new System.Drawing.Point(16, 19);
			locationSelector1.Name = "locationSelector1";
			locationSelector1.Size = new System.Drawing.Size(412, 59);
			locationSelector1.TabIndex = 7;
			printTemplateSelector.BackColor = System.Drawing.Color.Transparent;
			printTemplateSelector.CustomReportFieldName = "";
			printTemplateSelector.CustomReportKey = "";
			printTemplateSelector.CustomReportValueType = 1;
			printTemplateSelector.DefaultTemplate = "Cash Flow";
			printTemplateSelector.FormID = "CashFlowReportForm";
			printTemplateSelector.Location = new System.Drawing.Point(9, 148);
			printTemplateSelector.Name = "printTemplateSelector";
			printTemplateSelector.Size = new System.Drawing.Size(366, 28);
			printTemplateSelector.TabIndex = 24;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(471, 225);
			base.Controls.Add(printTemplateSelector);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dateControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "CashFlowReportForm";
			Text = "Cash Flow Statement Report";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
