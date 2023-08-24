using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Infragistics.Win.UltraWinDataSource;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class EndOfDayReportForm : XtraForm
	{
		private XtraReport report;

		public int ShiftID = -1;

		public int BatchID = -1;

		public string RegisterID = "";

		private IContainer components;

		private UltraDataSource ultraDataSource1;

		private SimpleButton buttonOK;

		private SimpleButton buttonCancel;

		private RadioButton radioButtonXReport;

		private RadioButton radioButtonYReport;

		private RadioButton radioButtonZReport;

		private Label label1;

		private Label label2;

		private Label label3;

		private SimpleButton buttonPrint;

		public XtraReport Report => report;

		public int DefaultReportType
		{
			set
			{
				switch (value)
				{
				case 1:
					radioButtonXReport.Checked = true;
					break;
				case 2:
					radioButtonYReport.Checked = true;
					break;
				case 3:
					radioButtonZReport.Checked = true;
					break;
				}
			}
		}

		public EndOfDayReportForm()
		{
			InitializeComponent();
		}

		private void XtraForm1_Load(object sender, EventArgs e)
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SetReportData();
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void buttonPrint_Click(object sender, EventArgs e)
		{
			SetReportData();
			base.DialogResult = DialogResult.None;
			Close();
		}

		private void SetReportData()
		{
			DataSet data = null;
			string text = "";
			if (radioButtonXReport.Checked)
			{
				data = Factory.SalesPOSSystem.GetPOSXReport(DateTime.MinValue, DateTime.MaxValue, ShiftID, BatchID, RegisterID);
				text = "X-Report";
			}
			else if (radioButtonYReport.Checked)
			{
				data = Factory.SalesPOSSystem.GetPOSYReport(DateTime.MinValue, DateTime.MaxValue, ShiftID, BatchID, RegisterID);
				text = "Y-Report";
			}
			else if (radioButtonZReport.Checked)
			{
				data = Factory.SalesPOSSystem.GetPOSZReport(DateTime.MinValue, DateTime.MaxValue, ShiftID, BatchID, RegisterID);
				text = "Z-Report";
			}
			ReportHelper reportHelper = new ReportHelper();
			reportHelper.AddGeneralReportData(ref data, text);
			switch (text)
			{
			case "X-Report":
				report = reportHelper.GetReport("POS X-Report 80mm");
				break;
			case "Y-Report":
				report = reportHelper.GetReport("POS Y-Report 80mm");
				break;
			case "Z-Report":
				report = reportHelper.GetReport("POS Z-Report 80mm");
				break;
			}
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'POS X-Report 80mm.repx'");
			}
			else
			{
				report.DataSource = data;
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
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 0");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 1");
			ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource();
			buttonOK = new DevExpress.XtraEditors.SimpleButton();
			buttonCancel = new DevExpress.XtraEditors.SimpleButton();
			radioButtonXReport = new System.Windows.Forms.RadioButton();
			radioButtonYReport = new System.Windows.Forms.RadioButton();
			radioButtonZReport = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			buttonPrint = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).BeginInit();
			SuspendLayout();
			ultraDataSource1.Band.Columns.AddRange(new object[2]
			{
				ultraDataColumn,
				ultraDataColumn2
			});
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonOK.Appearance.Options.UseFont = true;
			buttonOK.Location = new System.Drawing.Point(274, 267);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(102, 40);
			buttonOK.TabIndex = 30;
			buttonOK.Text = "Display";
			buttonOK.Click += new System.EventHandler(buttonSave_Click);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCancel.Appearance.Options.UseFont = true;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(382, 267);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(97, 40);
			buttonCancel.TabIndex = 30;
			buttonCancel.Text = "Close";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			radioButtonXReport.AutoSize = true;
			radioButtonXReport.Checked = true;
			radioButtonXReport.Font = new System.Drawing.Font("Tahoma", 14f);
			radioButtonXReport.Location = new System.Drawing.Point(23, 24);
			radioButtonXReport.Name = "radioButtonXReport";
			radioButtonXReport.Size = new System.Drawing.Size(102, 27);
			radioButtonXReport.TabIndex = 31;
			radioButtonXReport.TabStop = true;
			radioButtonXReport.Text = "X-Report";
			radioButtonXReport.UseVisualStyleBackColor = true;
			radioButtonYReport.AutoSize = true;
			radioButtonYReport.Font = new System.Drawing.Font("Tahoma", 14f);
			radioButtonYReport.Location = new System.Drawing.Point(23, 91);
			radioButtonYReport.Name = "radioButtonYReport";
			radioButtonYReport.Size = new System.Drawing.Size(102, 27);
			radioButtonYReport.TabIndex = 31;
			radioButtonYReport.Text = "Y-Report";
			radioButtonYReport.UseVisualStyleBackColor = true;
			radioButtonZReport.AutoSize = true;
			radioButtonZReport.Font = new System.Drawing.Font("Tahoma", 14f);
			radioButtonZReport.Location = new System.Drawing.Point(23, 163);
			radioButtonZReport.Name = "radioButtonZReport";
			radioButtonZReport.Size = new System.Drawing.Size(102, 27);
			radioButtonZReport.TabIndex = 31;
			radioButtonZReport.TabStop = true;
			radioButtonZReport.Text = "Z-Report";
			radioButtonZReport.UseVisualStyleBackColor = true;
			label1.Location = new System.Drawing.Point(42, 54);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(390, 39);
			label1.TabIndex = 32;
			label1.Text = "Generate X-Report for cash register status. Show sales and payment summary for the current Shift and Batch. Does not close shift or batch.";
			label2.Location = new System.Drawing.Point(42, 121);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(390, 39);
			label2.TabIndex = 32;
			label2.Text = "Generate Y-Report for cash register status. Show sales and payment summary for the current Shift and Batch. Generating this report will close the current Shift for the cash register.";
			label3.Location = new System.Drawing.Point(42, 193);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(390, 39);
			label3.TabIndex = 33;
			label3.Text = "Generate Z-Report for cash register status. Run this report for end of day processing. This will close the Batch. All shifts of the batch must be closed before generating Z-Report.";
			buttonPrint.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonPrint.Appearance.Options.UseFont = true;
			buttonPrint.Location = new System.Drawing.Point(129, 267);
			buttonPrint.Name = "buttonPrint";
			buttonPrint.Size = new System.Drawing.Size(102, 40);
			buttonPrint.TabIndex = 34;
			buttonPrint.Text = "Print";
			buttonPrint.Click += new System.EventHandler(buttonPrint_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(489, 319);
			base.Controls.Add(buttonPrint);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonZReport);
			base.Controls.Add(radioButtonYReport);
			base.Controls.Add(radioButtonXReport);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EndOfDayReportForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "End of Day";
			base.Load += new System.EventHandler(XtraForm1_Load);
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
