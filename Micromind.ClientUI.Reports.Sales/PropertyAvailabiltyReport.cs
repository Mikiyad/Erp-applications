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

namespace Micromind.ClientUI.Reports.Sales
{
	public class PropertyAvailabiltyReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private PropertyUnitSelector propertyunitSelector;

		private MMSDateTimePicker dateTimePickerDate;

		private Label label1;

		private UltraGroupBox ultraGroupBox2;

		private UltraGroupBox ultraGroupBox3;

		private PropertySelector propertySelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsPropertyRental;

		public int ScreenID => 7034;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public PropertyAvailabiltyReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
			dateTimePickerDate.Value = DateTime.Today;
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
			try
			{
				ReportHelper reportHelper = new ReportHelper();
				XtraReport xtraReport = null;
				DateTime value = dateTimePickerDate.Value;
				value = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
				DataSet data = Factory.PropertySystem.GetPropertyAvailabilityReport(value, propertyunitSelector.FromUnit, propertyunitSelector.ToUnit, propertySelector.FromProperty, propertySelector.ToProperty, propertySelector.FromClass, propertySelector.ToClass);
				xtraReport = reportHelper.GetReport("Property Availability");
				string reportFilter = "As Of : " + dateTimePickerDate.Value.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Property Availability.repx'");
				}
				else
				{
					xtraReport.DataSource = data;
					reportHelper.ShowReport(xtraReport);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Sales.PropertyAvailabiltyReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			propertyunitSelector = new Micromind.DataControls.PropertyUnitSelector();
			buttonClose = new System.Windows.Forms.Button();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker();
			label1 = new System.Windows.Forms.Label();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			propertySelector = new Micromind.DataControls.PropertySelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(253, 272);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(propertyunitSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 106);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(449, 83);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Property Unit";
			propertyunitSelector.BackColor = System.Drawing.Color.Transparent;
			propertyunitSelector.CustomReportFieldName = "";
			propertyunitSelector.CustomReportKey = "";
			propertyunitSelector.CustomReportValueType = 1;
			propertyunitSelector.Location = new System.Drawing.Point(6, 19);
			propertyunitSelector.Name = "propertyunitSelector";
			propertyunitSelector.Size = new System.Drawing.Size(428, 57);
			propertyunitSelector.TabIndex = 6;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(359, 272);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(73, 221);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(138, 20);
			dateTimePickerDate.TabIndex = 6;
			dateTimePickerDate.Value = new System.DateTime(2016, 3, 3, 13, 5, 1, 871);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(30, 224);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(34, 13);
			label1.TabIndex = 7;
			label1.Text = "As of:";
			ultraGroupBox2.Controls.Add(ultraGroupBox3);
			ultraGroupBox2.Controls.Add(propertySelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 5);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(449, 100);
			ultraGroupBox2.TabIndex = 8;
			ultraGroupBox2.Text = "Property";
			ultraGroupBox3.Location = new System.Drawing.Point(6, 106);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(464, 110);
			ultraGroupBox3.TabIndex = 6;
			ultraGroupBox3.Text = "ultraGroupBox3";
			propertySelector.BackColor = System.Drawing.Color.Transparent;
			propertySelector.CustomReportFieldName = "";
			propertySelector.CustomReportKey = "";
			propertySelector.CustomReportValueType = 1;
			propertySelector.Location = new System.Drawing.Point(6, 19);
			propertySelector.Name = "propertySelector";
			propertySelector.Size = new System.Drawing.Size(464, 81);
			propertySelector.TabIndex = 6;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(473, 308);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(label1);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "PropertyAvailabiltyReport";
			Text = "Property Availabilty Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
