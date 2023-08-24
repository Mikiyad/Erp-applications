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
	public class MonthlySalesPivotReport : Form, IForm
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

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox2;

		private DateControl dateControl1;

		private RadioButton radioButtonMore;

		private RadioButton radioButtonCategory;

		private RadioButton radioButtonItem;

		private UltraGroupBox ultraGroupBox3;

		private SalespersonSelector salespersonSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7026;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public MonthlySalesPivotReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				dateTimePickerAsOfDate.Value = DateTime.Now;
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
				DataSet data = new DataSet();
				string text = "";
				_ = dateTimePickerAsOfDate.ValueTo;
				if (radioButtonItem.Checked)
				{
					data = Factory.ProductSystem.GetMonthlySalesPivotReport(dateControl1.FromDate, dateControl1.ToDate, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, locationSelector.FromLocation, locationSelector.ToLocation, radioButtonAsOf.Checked, dateTimePickerAsOfDate.ValueTo, showZero: false, isInactive: false, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry);
					text = "Monthly Sales Pivot";
				}
				else if (radioButtonCategory.Checked)
				{
					data = Factory.ProductSystem.GetMonthlySalesPivotReportByCatgory(dateControl1.FromDate, dateControl1.ToDate, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, locationSelector.FromLocation, locationSelector.ToLocation, radioButtonAsOf.Checked, dateTimePickerAsOfDate.ValueTo, showZero: false, isInactive: false, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry);
					text = "Monthly Sales Pivot By Category";
				}
				else if (radioButtonMore.Checked)
				{
					data = Factory.ProductSystem.GetMonthlySalesPivotReportMore(dateControl1.FromDate, dateControl1.ToDate, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, locationSelector.FromLocation, locationSelector.ToLocation, radioButtonAsOf.Checked, dateTimePickerAsOfDate.ValueTo, showZero: false, isInactive: false, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry);
					text = "Monthly Sales Pivot More";
				}
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "From:" + salespersonSelector.FromSalesperson;
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				XtraReport report = reportHelper.GetReport(text);
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file," + text + ", Please make sure you have access to reports path and the files are not corrupted.");
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

		private void radioButtonLocationWise_CheckedChanged(object sender, EventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.MonthlySalesPivotReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			buttonClose = new System.Windows.Forms.Button();
			radioButtonCurrent = new System.Windows.Forms.RadioButton();
			radioButtonAsOf = new System.Windows.Forms.RadioButton();
			line1 = new Micromind.UISupport.Line();
			dateTimePickerAsOfDate = new Micromind.UISupport.MMSDateTimePicker(components);
			locationSelector = new Micromind.DataControls.LocationSelector();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			dateControl1 = new Micromind.DataControls.DateControl();
			radioButtonMore = new System.Windows.Forms.RadioButton();
			radioButtonCategory = new System.Windows.Forms.RadioButton();
			radioButtonItem = new System.Windows.Forms.RadioButton();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			salespersonSelector = new Micromind.DataControls.SalespersonSelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(289, 594);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 7;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 212);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Products";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(6, 19);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(403, 187);
			productSelector.TabIndex = 2;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(393, 594);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 8;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			radioButtonCurrent.AutoSize = true;
			radioButtonCurrent.Checked = true;
			radioButtonCurrent.Location = new System.Drawing.Point(18, 566);
			radioButtonCurrent.Name = "radioButtonCurrent";
			radioButtonCurrent.Size = new System.Drawing.Size(85, 17);
			radioButtonCurrent.TabIndex = 2;
			radioButtonCurrent.TabStop = true;
			radioButtonCurrent.Text = "Current Date";
			radioButtonCurrent.UseVisualStyleBackColor = true;
			radioButtonCurrent.Visible = false;
			radioButtonAsOf.AutoSize = true;
			radioButtonAsOf.Location = new System.Drawing.Point(109, 566);
			radioButtonAsOf.Name = "radioButtonAsOf";
			radioButtonAsOf.Size = new System.Drawing.Size(54, 17);
			radioButtonAsOf.TabIndex = 3;
			radioButtonAsOf.Text = "As Of:";
			radioButtonAsOf.UseVisualStyleBackColor = true;
			radioButtonAsOf.Visible = false;
			radioButtonAsOf.CheckedChanged += new System.EventHandler(radioButtonAsOf_CheckedChanged);
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-8, 588);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(520, 1);
			line1.TabIndex = 13;
			line1.TabStop = false;
			dateTimePickerAsOfDate.Enabled = false;
			dateTimePickerAsOfDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerAsOfDate.Location = new System.Drawing.Point(172, 566);
			dateTimePickerAsOfDate.Name = "dateTimePickerAsOfDate";
			dateTimePickerAsOfDate.Size = new System.Drawing.Size(143, 20);
			dateTimePickerAsOfDate.TabIndex = 4;
			dateTimePickerAsOfDate.Value = new System.DateTime(2014, 4, 8, 14, 15, 58, 16);
			dateTimePickerAsOfDate.Visible = false;
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(424, 53);
			locationSelector.TabIndex = 2;
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 390);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(476, 84);
			ultraGroupBox2.TabIndex = 14;
			ultraGroupBox2.Text = "Locations";
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2012, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 480);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(476, 55);
			dateControl1.TabIndex = 15;
			dateControl1.ToDate = new System.DateTime(2012, 10, 24, 23, 59, 59, 59);
			radioButtonMore.AutoSize = true;
			radioButtonMore.Location = new System.Drawing.Point(204, 541);
			radioButtonMore.Name = "radioButtonMore";
			radioButtonMore.Size = new System.Drawing.Size(49, 17);
			radioButtonMore.TabIndex = 18;
			radioButtonMore.Text = "More";
			radioButtonMore.UseVisualStyleBackColor = true;
			radioButtonCategory.AutoSize = true;
			radioButtonCategory.Location = new System.Drawing.Point(109, 541);
			radioButtonCategory.Name = "radioButtonCategory";
			radioButtonCategory.Size = new System.Drawing.Size(82, 17);
			radioButtonCategory.TabIndex = 17;
			radioButtonCategory.Text = "By Category";
			radioButtonCategory.UseVisualStyleBackColor = true;
			radioButtonItem.AutoSize = true;
			radioButtonItem.Checked = true;
			radioButtonItem.Location = new System.Drawing.Point(18, 541);
			radioButtonItem.Name = "radioButtonItem";
			radioButtonItem.Size = new System.Drawing.Size(60, 17);
			radioButtonItem.TabIndex = 16;
			radioButtonItem.TabStop = true;
			radioButtonItem.Text = "By Item";
			radioButtonItem.UseVisualStyleBackColor = true;
			ultraGroupBox3.Controls.Add(salespersonSelector);
			ultraGroupBox3.Location = new System.Drawing.Point(12, 225);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(476, 165);
			ultraGroupBox3.TabIndex = 19;
			ultraGroupBox3.Text = "Salespersons";
			salespersonSelector.BackColor = System.Drawing.Color.Transparent;
			salespersonSelector.Location = new System.Drawing.Point(6, 19);
			salespersonSelector.Name = "salespersonSelector";
			salespersonSelector.Size = new System.Drawing.Size(436, 140);
			salespersonSelector.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(500, 626);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(radioButtonMore);
			base.Controls.Add(radioButtonCategory);
			base.Controls.Add(radioButtonItem);
			base.Controls.Add(dateControl1);
			base.Controls.Add(ultraGroupBox2);
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
			base.Name = "MonthlySalesPivotReport";
			Text = "Monthly Sales Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
