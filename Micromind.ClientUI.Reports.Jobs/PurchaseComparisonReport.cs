using DevExpress.XtraReports.UI;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Jobs
{
	public class PurchaseComparisonReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private JobSelector jobCostSelector;

		private TextBox textBoxReferenceNo;

		private Label label10;

		private XPButton buttonQOT;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7002;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public string ReferenceNo
		{
			get
			{
				return textBoxReferenceNo.Text;
			}
			set
			{
				textBoxReferenceNo.Text = value;
			}
		}

		public PurchaseComparisonReport()
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
			DataSet data = Factory.PurchaseQuoteSystem.GetPurchaseComparisonReport(textBoxReferenceNo.Text.Trim());
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:  To:";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			XtraReport report = reportHelper.GetReport("Quotation Comparison");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Project Status.repx'");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonQOT_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet dataSource = Factory.PurchaseQuoteSystem.GettPurchaseQuoteList();
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = dataSource;
				selectDocumentDialog.Text = "Select Purchase Quotation";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
					{
						ReferenceNo = selectedRow.Cells["Reference"].Value.ToString();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Jobs.PurchaseComparisonReport));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			textBoxReferenceNo = new System.Windows.Forms.TextBox();
			label10 = new System.Windows.Forms.Label();
			buttonQOT = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			panelButtons.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(181, 8);
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
			panelButtons.Location = new System.Drawing.Point(0, 73);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(391, 40);
			panelButtons.TabIndex = 2;
			textBoxReferenceNo.Location = new System.Drawing.Point(118, 31);
			textBoxReferenceNo.MaxLength = 15;
			textBoxReferenceNo.Name = "textBoxReferenceNo";
			textBoxReferenceNo.Size = new System.Drawing.Size(181, 20);
			textBoxReferenceNo.TabIndex = 3;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(12, 34);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(100, 13);
			label10.TabIndex = 149;
			label10.Text = "Reference Number:";
			buttonQOT.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonQOT.BackColor = System.Drawing.Color.DarkGray;
			buttonQOT.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonQOT.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonQOT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonQOT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonQOT.Location = new System.Drawing.Point(315, 29);
			buttonQOT.Name = "buttonQOT";
			buttonQOT.Size = new System.Drawing.Size(34, 24);
			buttonQOT.TabIndex = 162;
			buttonQOT.Text = "...";
			buttonQOT.UseVisualStyleBackColor = false;
			buttonQOT.Click += new System.EventHandler(buttonQOT_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(391, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(281, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(391, 113);
			base.Controls.Add(buttonQOT);
			base.Controls.Add(label10);
			base.Controls.Add(textBoxReferenceNo);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "PurchaseComparisonReport";
			Text = "Purchase Quote Comparison";
			panelButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
