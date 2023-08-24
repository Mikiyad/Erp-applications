using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Infragistics.Win.UltraWinDataSource;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class PrintDialogForm : XtraForm
	{
		private XtraReport report;

		public int ShiftID = -1;

		public int BatchID = -1;

		public string RegisterID = "";

		private IContainer components;

		private UltraDataSource ultraDataSource1;

		private SimpleButton buttonDisplay;

		private SimpleButton buttonCancel;

		private SimpleButton buttonPrint;

		public XtraReport Report => report;

		public PrintDialogForm()
		{
			InitializeComponent();
		}

		private void XtraForm1_Load(object sender, EventArgs e)
		{
		}

		private void buttonDisplay_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonPrint_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Yes;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
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
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 0");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 1");
			ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource();
			buttonDisplay = new DevExpress.XtraEditors.SimpleButton();
			buttonCancel = new DevExpress.XtraEditors.SimpleButton();
			buttonPrint = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).BeginInit();
			SuspendLayout();
			ultraDataSource1.Band.Columns.AddRange(new object[2]
			{
				ultraDataColumn,
				ultraDataColumn2
			});
			buttonDisplay.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonDisplay.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonDisplay.Appearance.Options.UseFont = true;
			buttonDisplay.Location = new System.Drawing.Point(209, 52);
			buttonDisplay.Name = "buttonDisplay";
			buttonDisplay.Size = new System.Drawing.Size(102, 40);
			buttonDisplay.TabIndex = 30;
			buttonDisplay.Text = "Display";
			buttonDisplay.Click += new System.EventHandler(buttonDisplay_Click);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCancel.Appearance.Options.UseFont = true;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(89, 98);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(222, 40);
			buttonCancel.TabIndex = 30;
			buttonCancel.Text = "Close";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonPrint.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonPrint.Appearance.Options.UseFont = true;
			buttonPrint.Location = new System.Drawing.Point(89, 52);
			buttonPrint.Name = "buttonPrint";
			buttonPrint.Size = new System.Drawing.Size(102, 40);
			buttonPrint.TabIndex = 34;
			buttonPrint.Text = "Print";
			buttonPrint.Click += new System.EventHandler(buttonPrint_Click);
			base.AcceptButton = buttonDisplay;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(406, 210);
			base.Controls.Add(buttonPrint);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonDisplay);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PrintDialogForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Print Dialog";
			base.Load += new System.EventHandler(XtraForm1_Load);
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).EndInit();
			ResumeLayout(false);
		}
	}
}
