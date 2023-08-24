using DevExpress.XtraReports.UI;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
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

namespace Micromind.ClientUI.Reports.Legal
{
	public class CaseStatusReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private DateControl dateControl1;

		private UltraGroupBox ultraGroupBox2;

		private LawyerSelector LawyerSelector;

		private TextBox textboxFileNo;

		private Label labelFileNumber;

		private Label label1;

		private TextBox textBoxVoucherID;

		private TextBox textBoxSysDocID;

		private XPButton xpButton2;

		private XPButton xpButton1;

		private CaseClientSelector caseClientSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsLegal;

		public int ScreenID => 7009;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public CaseStatusReport()
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
			DataSet data = Factory.LegalActivitySystem.GetCaseHistoryReport(dateControl1.FromDate, dateControl1.ToDate, caseClientSelector.FromCustomer, caseClientSelector.ToCustomer, LawyerSelector.FromLawyer, LawyerSelector.ToLawyer, textboxFileNo.Text, textBoxSysDocID.Text, textBoxVoucherID.Text);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Case History");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'Case History.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			DataSet legalActionReportList = Factory.LegalActionSystem.GetLegalActionReportList(DateTime.MinValue, DateTime.MaxValue);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = legalActionReportList;
			selectDocumentDialog.Text = "Select Legal Action";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				textBoxSysDocID.Text = text;
				textBoxVoucherID.Text = text2;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			DataSet legalActionReportList = Factory.LegalActionSystem.GetLegalActionReportList(DateTime.MinValue, DateTime.MaxValue);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = legalActionReportList;
			selectDocumentDialog.Text = "Select Legal Action";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				string text = selectDocumentDialog.SelectedRow.Cells["FileNo"].Value.ToString();
				textboxFileNo.Text = text;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Legal.CaseStatusReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			caseClientSelector = new Micromind.DataControls.CaseClientSelector();
			buttonClose = new System.Windows.Forms.Button();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			LawyerSelector = new Micromind.DataControls.LawyerSelector();
			textboxFileNo = new System.Windows.Forms.TextBox();
			labelFileNumber = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			textBoxVoucherID = new System.Windows.Forms.TextBox();
			textBoxSysDocID = new System.Windows.Forms.TextBox();
			xpButton1 = new Micromind.UISupport.XPButton();
			xpButton2 = new Micromind.UISupport.XPButton();
			dateControl1 = new Micromind.DataControls.DateControl();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(286, 364);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(caseClientSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(450, 111);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Case Clients";
			caseClientSelector.BackColor = System.Drawing.Color.Transparent;
			caseClientSelector.CustomReportFieldName = "";
			caseClientSelector.CustomReportKey = "";
			caseClientSelector.CustomReportValueType = 1;
			caseClientSelector.Location = new System.Drawing.Point(8, 23);
			caseClientSelector.Name = "caseClientSelector";
			caseClientSelector.Size = new System.Drawing.Size(428, 82);
			caseClientSelector.TabIndex = 0;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 364);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			ultraGroupBox2.Controls.Add(LawyerSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(11, 129);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(451, 88);
			ultraGroupBox2.TabIndex = 6;
			ultraGroupBox2.Text = "Lawyers";
			LawyerSelector.BackColor = System.Drawing.Color.Transparent;
			LawyerSelector.CustomReportFieldName = "";
			LawyerSelector.CustomReportKey = "";
			LawyerSelector.CustomReportValueType = 1;
			LawyerSelector.Location = new System.Drawing.Point(7, 24);
			LawyerSelector.Name = "LawyerSelector";
			LawyerSelector.Size = new System.Drawing.Size(430, 48);
			LawyerSelector.TabIndex = 0;
			textboxFileNo.Location = new System.Drawing.Point(56, 249);
			textboxFileNo.Name = "textboxFileNo";
			textboxFileNo.Size = new System.Drawing.Size(372, 20);
			textboxFileNo.TabIndex = 7;
			labelFileNumber.AutoSize = true;
			labelFileNumber.Location = new System.Drawing.Point(15, 253);
			labelFileNumber.Name = "labelFileNumber";
			labelFileNumber.Size = new System.Drawing.Size(40, 13);
			labelFileNumber.TabIndex = 8;
			labelFileNumber.Text = "FileNo:";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(5, 227);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(51, 13);
			label1.TabIndex = 9;
			label1.Text = "Case No:";
			textBoxVoucherID.Location = new System.Drawing.Point(145, 223);
			textBoxVoucherID.Name = "textBoxVoucherID";
			textBoxVoucherID.Size = new System.Drawing.Size(283, 20);
			textBoxVoucherID.TabIndex = 10;
			textBoxSysDocID.Location = new System.Drawing.Point(57, 223);
			textBoxSysDocID.Name = "textBoxSysDocID";
			textBoxSysDocID.Size = new System.Drawing.Size(84, 20);
			textBoxSysDocID.TabIndex = 11;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(432, 249);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(25, 20);
			xpButton1.TabIndex = 13;
			xpButton1.Text = "..";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			xpButton2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton2.BackColor = System.Drawing.Color.DarkGray;
			xpButton2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton2.Location = new System.Drawing.Point(432, 224);
			xpButton2.Name = "xpButton2";
			xpButton2.Size = new System.Drawing.Size(25, 20);
			xpButton2.TabIndex = 12;
			xpButton2.Text = "..";
			xpButton2.UseVisualStyleBackColor = false;
			xpButton2.Click += new System.EventHandler(xpButton2_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 280);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(476, 54);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2017, 11, 28, 23, 59, 59, 59);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(498, 395);
			base.Controls.Add(xpButton1);
			base.Controls.Add(xpButton2);
			base.Controls.Add(textBoxSysDocID);
			base.Controls.Add(textBoxVoucherID);
			base.Controls.Add(label1);
			base.Controls.Add(labelFileNumber);
			base.Controls.Add(textboxFileNo);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "CaseStatusReport";
			Text = "Case History Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
