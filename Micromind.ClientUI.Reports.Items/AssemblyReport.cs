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

namespace Micromind.ClientUI.Reports.Items
{
	public class AssemblyReport : Form, IForm
	{
		public bool IsValuation;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private ProductSelector productSelector;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private MMSDateTimePicker dateTimePickerAsOfDate;

		private Line line1;

		private RadioButton radioButtonCurrent;

		private RadioButton radioButtonAsOf;

		private GroupBox groupBoxCC;

		private CostCategorySelector costCategorySelector;

		private GroupBox groupBoxJob;

		private JobSelector jobSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7026;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public AssemblyReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
			dateTimePickerAsOfDate.Value = DateTime.Today;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				productSelector.ShowOnlyAssemlbyItems = true;
				if (IsValuation)
				{
					Text = "Assembly Report";
				}
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
				DateTime asOfDate = radioButtonCurrent.Checked ? DateTime.Now : dateTimePickerAsOfDate.ValueTo;
				DataSet data = Factory.AssemblyBuildSystem.GetAssemblyBuildReport(productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, jobSelector.FromJob, jobSelector.ToJob, costCategorySelector.FromCostCategory, costCategorySelector.ToCostCategory, asOfDate, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
				string fileName = "Assembly List";
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "As of Date: " + (radioButtonAsOf.Checked ? asOfDate.ToShortDateString() : DateTime.Now.ToShortDateString());
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				XtraReport report = reportHelper.GetReport(fileName);
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'Assembly List.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
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

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void radioButtonAsOf_CheckedChanged(object sender, EventArgs e)
		{
			dateTimePickerAsOfDate.Enabled = radioButtonAsOf.Checked;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.AssemblyReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			buttonClose = new System.Windows.Forms.Button();
			radioButtonCurrent = new System.Windows.Forms.RadioButton();
			radioButtonAsOf = new System.Windows.Forms.RadioButton();
			line1 = new Micromind.UISupport.Line();
			dateTimePickerAsOfDate = new Micromind.UISupport.MMSDateTimePicker(components);
			groupBoxCC = new System.Windows.Forms.GroupBox();
			costCategorySelector = new Micromind.DataControls.CostCategorySelector();
			groupBoxJob = new System.Windows.Forms.GroupBox();
			jobSelector = new Micromind.DataControls.JobSelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			groupBoxCC.SuspendLayout();
			groupBoxJob.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(256, 475);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 7;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(443, 209);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Products";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(6, 19);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(403, 190);
			productSelector.TabIndex = 2;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(360, 475);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 8;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			radioButtonCurrent.AutoSize = true;
			radioButtonCurrent.Checked = true;
			radioButtonCurrent.Location = new System.Drawing.Point(23, 388);
			radioButtonCurrent.Name = "radioButtonCurrent";
			radioButtonCurrent.Size = new System.Drawing.Size(85, 17);
			radioButtonCurrent.TabIndex = 2;
			radioButtonCurrent.TabStop = true;
			radioButtonCurrent.Text = "Current Date";
			radioButtonCurrent.UseVisualStyleBackColor = true;
			radioButtonAsOf.AutoSize = true;
			radioButtonAsOf.Location = new System.Drawing.Point(119, 388);
			radioButtonAsOf.Name = "radioButtonAsOf";
			radioButtonAsOf.Size = new System.Drawing.Size(54, 17);
			radioButtonAsOf.TabIndex = 3;
			radioButtonAsOf.Text = "As Of:";
			radioButtonAsOf.UseVisualStyleBackColor = true;
			radioButtonAsOf.CheckedChanged += new System.EventHandler(radioButtonAsOf_CheckedChanged);
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-8, 469);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(487, 1);
			line1.TabIndex = 13;
			line1.TabStop = false;
			dateTimePickerAsOfDate.Enabled = false;
			dateTimePickerAsOfDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerAsOfDate.Location = new System.Drawing.Point(177, 388);
			dateTimePickerAsOfDate.Name = "dateTimePickerAsOfDate";
			dateTimePickerAsOfDate.Size = new System.Drawing.Size(143, 20);
			dateTimePickerAsOfDate.TabIndex = 4;
			dateTimePickerAsOfDate.Value = new System.DateTime(2014, 4, 8, 14, 15, 58, 16);
			groupBoxCC.Controls.Add(costCategorySelector);
			groupBoxCC.Location = new System.Drawing.Point(7, 302);
			groupBoxCC.Name = "groupBoxCC";
			groupBoxCC.Size = new System.Drawing.Size(453, 69);
			groupBoxCC.TabIndex = 15;
			groupBoxCC.TabStop = false;
			groupBoxCC.Text = "Cost Categories";
			costCategorySelector.BackColor = System.Drawing.Color.Transparent;
			costCategorySelector.CustomReportFieldName = "";
			costCategorySelector.CustomReportKey = "";
			costCategorySelector.CustomReportValueType = 1;
			costCategorySelector.Location = new System.Drawing.Point(8, 16);
			costCategorySelector.Name = "costCategorySelector";
			costCategorySelector.Size = new System.Drawing.Size(439, 46);
			costCategorySelector.TabIndex = 0;
			groupBoxJob.Controls.Add(jobSelector);
			groupBoxJob.Location = new System.Drawing.Point(7, 227);
			groupBoxJob.Name = "groupBoxJob";
			groupBoxJob.Size = new System.Drawing.Size(453, 69);
			groupBoxJob.TabIndex = 14;
			groupBoxJob.TabStop = false;
			groupBoxJob.Text = "Jobs";
			jobSelector.BackColor = System.Drawing.Color.Transparent;
			jobSelector.CustomReportFieldName = "";
			jobSelector.CustomReportKey = "";
			jobSelector.CustomReportValueType = 1;
			jobSelector.Location = new System.Drawing.Point(6, 17);
			jobSelector.Name = "jobSelector";
			jobSelector.Size = new System.Drawing.Size(429, 46);
			jobSelector.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(467, 507);
			base.Controls.Add(groupBoxCC);
			base.Controls.Add(groupBoxJob);
			base.Controls.Add(radioButtonAsOf);
			base.Controls.Add(radioButtonCurrent);
			base.Controls.Add(line1);
			base.Controls.Add(dateTimePickerAsOfDate);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "AssemblyReport";
			Text = "Assembly Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			groupBoxCC.ResumeLayout(false);
			groupBoxJob.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
