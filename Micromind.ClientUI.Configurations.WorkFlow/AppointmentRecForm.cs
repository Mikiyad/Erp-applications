using DevExpress.Utils;
using DevExpress.XtraScheduler.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations.WorkFlow
{
	public class AppointmentRecForm : AppointmentRecurrenceForm
	{
		private IContainer components;

		public AppointmentRecForm()
		{
			InitializeComponent();
			base.RecurrenceInfo.ToXml();
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
			((System.ComponentModel.ISupportInitialize)grpAptTime).BeginInit();
			grpAptTime.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)grpRecurrencePattern).BeginInit();
			grpRecurrencePattern.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)grpRecurrenceRange).BeginInit();
			grpRecurrenceRange.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)edtRangeStart.Properties.VistaTimeProperties).BeginInit();
			((System.ComponentModel.ISupportInitialize)edtRangeStart.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)edtRangeEnd.Properties.VistaTimeProperties).BeginInit();
			((System.ComponentModel.ISupportInitialize)edtRangeEnd.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)edtStartTime.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)edtEndTime.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)spinRangeOccurrencesCount.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)cbDuration.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)chkNoEndDate.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)chkEndByDate.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)chkDaily.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)chkWeekly.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)chkMonthly.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)chkYearly.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)chkEndAfterNumberOfOccurrences.Properties).BeginInit();
			SuspendLayout();
			grpAptTime.Size = new System.Drawing.Size(506, 10);
			grpAptTime.Visible = false;
			grpRecurrencePattern.Location = new System.Drawing.Point(16, 27);
			grpRecurrenceRange.Location = new System.Drawing.Point(16, 179);
			edtRangeStart.EditValue = new System.DateTime(0L);
			edtRangeEnd.EditValue = new System.DateTime(0L);
			edtStartTime.EditValue = new System.DateTime(0L);
			edtEndTime.EditValue = new System.DateTime(1, 1, 1, 0, 30, 0, 0);
			edtEndTime.Location = new System.Drawing.Point(216, 32);
			spinRangeOccurrencesCount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			spinRangeOccurrencesCount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			spinRangeOccurrencesCount.Properties.Mask.EditMask = "N00";
			weeklyRecurrenceControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
			weeklyRecurrenceControl1.Appearance.Options.UseBackColor = true;
			monthlyRecurrenceControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
			monthlyRecurrenceControl1.Appearance.Options.UseBackColor = true;
			yearlyRecurrenceControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
			yearlyRecurrenceControl1.Appearance.Options.UseBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(538, 390);
			base.Name = "AppointmentRecForm";
			Text = "AppointmentRecForm";
			((System.ComponentModel.ISupportInitialize)grpAptTime).EndInit();
			grpAptTime.ResumeLayout(false);
			grpAptTime.PerformLayout();
			((System.ComponentModel.ISupportInitialize)grpRecurrencePattern).EndInit();
			grpRecurrencePattern.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)grpRecurrenceRange).EndInit();
			grpRecurrenceRange.ResumeLayout(false);
			grpRecurrenceRange.PerformLayout();
			((System.ComponentModel.ISupportInitialize)edtRangeStart.Properties.VistaTimeProperties).EndInit();
			((System.ComponentModel.ISupportInitialize)edtRangeStart.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)edtRangeEnd.Properties.VistaTimeProperties).EndInit();
			((System.ComponentModel.ISupportInitialize)edtRangeEnd.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)edtStartTime.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)edtEndTime.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)spinRangeOccurrencesCount.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)cbDuration.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)chkNoEndDate.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)chkEndByDate.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)chkDaily.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)chkWeekly.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)chkMonthly.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)chkYearly.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)chkEndAfterNumberOfOccurrences.Properties).EndInit();
			ResumeLayout(false);
		}
	}
}
