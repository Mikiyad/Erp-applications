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
	public class InventoryAgingReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private ProductSelector productSelector;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox2;

		private MMSDateTimePicker dateTimePickerAsOfDate;

		private CheckBox checkBoxShowZero;

		private CheckBox checkBoxShowInactive;

		private Label label1;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7022;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public InventoryAgingReport()
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
			DataSet data = Factory.ProductSystem.GetInventoryAgingSummaryReport(productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, locationSelector.FromLocation, locationSelector.ToLocation, productSelector.FromBrand, productSelector.ToBrand, dateTimePickerAsOfDate.Value, checkBoxShowZero.Checked, checkBoxShowInactive.Checked, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
			DataTable dataTable = data.Tables.Add("AgingHeaders");
			dataTable.Columns.Add("Month1", typeof(string));
			dataTable.Columns.Add("Month2", typeof(string));
			dataTable.Columns.Add("Month3", typeof(string));
			dataTable.Columns.Add("Month4", typeof(string));
			DataRow dataRow = dataTable.Rows.Add();
			bool companyOption = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowInvAging1, defaultValue: true);
			bool companyOption2 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowInvAging2, defaultValue: true);
			bool companyOption3 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowInvAging3, defaultValue: true);
			bool companyOption4 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowInvAging4, defaultValue: true);
			CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvFrom1, 0);
			CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvFrom2, 31);
			CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvFrom3, 61);
			CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvFrom4, 91);
			int companyOption5 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvTo1, 30);
			int companyOption6 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvTo2, 60);
			int companyOption7 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvTo3, 90);
			int companyOption8 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvTo4, 120);
			string companyOption9 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvName1, "0-30 Days");
			string companyOption10 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvName2, "31-60 Days");
			string companyOption11 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvName3, "61-90 Days");
			string companyOption12 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingInvName4, "91-120 Days");
			if (companyOption4 || companyOption3 || !companyOption2)
			{
			}
			dataRow["Month1"] = companyOption9;
			dataRow["Month2"] = companyOption10;
			dataRow["Month3"] = companyOption11;
			dataRow["Month4"] = companyOption12;
			dataRow.EndEdit();
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Inventory Aging Summary");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Inventory Aging Summary.repx'");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
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

		private void label1_Click(object sender, EventArgs e)
		{
		}

		private void checkBoxShowZero_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void checkBoxShowInactive_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void dateTimePickerAsOfDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void ultraGroupBox2_Click(object sender, EventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.InventoryAgingReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			buttonClose = new System.Windows.Forms.Button();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			checkBoxShowZero = new System.Windows.Forms.CheckBox();
			checkBoxShowInactive = new System.Windows.Forms.CheckBox();
			label1 = new System.Windows.Forms.Label();
			dateTimePickerAsOfDate = new Micromind.UISupport.MMSDateTimePicker(components);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(230, 382);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(102, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(424, 205);
			ultraGroupBox1.TabIndex = 3;
			ultraGroupBox1.Text = "Items";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(5, 18);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(446, 184);
			productSelector.TabIndex = 0;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(338, 382);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(102, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 220);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(424, 77);
			ultraGroupBox2.TabIndex = 8;
			ultraGroupBox2.Text = "Location";
			ultraGroupBox2.Click += new System.EventHandler(ultraGroupBox2_Click);
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 18);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			checkBoxShowZero.AutoSize = true;
			checkBoxShowZero.Location = new System.Drawing.Point(24, 362);
			checkBoxShowZero.Name = "checkBoxShowZero";
			checkBoxShowZero.Size = new System.Drawing.Size(116, 17);
			checkBoxShowZero.TabIndex = 15;
			checkBoxShowZero.Text = "Show zero quantity";
			checkBoxShowZero.UseVisualStyleBackColor = true;
			checkBoxShowZero.Visible = false;
			checkBoxShowZero.CheckedChanged += new System.EventHandler(checkBoxShowZero_CheckedChanged);
			checkBoxShowInactive.AutoSize = true;
			checkBoxShowInactive.Location = new System.Drawing.Point(24, 342);
			checkBoxShowInactive.Name = "checkBoxShowInactive";
			checkBoxShowInactive.Size = new System.Drawing.Size(137, 17);
			checkBoxShowInactive.TabIndex = 16;
			checkBoxShowInactive.Text = "Show inactive products";
			checkBoxShowInactive.UseVisualStyleBackColor = true;
			checkBoxShowInactive.CheckedChanged += new System.EventHandler(checkBoxShowInactive_CheckedChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(21, 308);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(57, 13);
			label1.TabIndex = 17;
			label1.Text = "As of Date";
			label1.Click += new System.EventHandler(label1_Click);
			dateTimePickerAsOfDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerAsOfDate.Location = new System.Drawing.Point(82, 305);
			dateTimePickerAsOfDate.Name = "dateTimePickerAsOfDate";
			dateTimePickerAsOfDate.Size = new System.Drawing.Size(143, 20);
			dateTimePickerAsOfDate.TabIndex = 14;
			dateTimePickerAsOfDate.Value = new System.DateTime(2014, 10, 30, 0, 0, 0, 0);
			dateTimePickerAsOfDate.ValueChanged += new System.EventHandler(dateTimePickerAsOfDate_ValueChanged);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(453, 418);
			base.Controls.Add(label1);
			base.Controls.Add(checkBoxShowZero);
			base.Controls.Add(checkBoxShowInactive);
			base.Controls.Add(dateTimePickerAsOfDate);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "InventoryAgingReport";
			Text = "Inventory Aging Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
