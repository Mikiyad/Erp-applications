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

namespace Micromind.ClientUI.Reports.Vendors
{
	public class ConsignmentOutIssuedReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private RadioButton radioButtonUnsettled;

		private RadioButton radioButtonSettled;

		private Label label1;

		private UltraGroupBox ultraGroupBox1;

		private CustomerSelector customerSelector;

		private RadioButton radioButtonAll;

		private GroupBox groupBox1;

		private DateControl dateControl1;

		private Label label2;

		private MMSDateTimePicker dateTimePickerDate;

		private ProductSelector productSelector;

		private UltraGroupBox ultraGroupBox2;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsVendor;

		public int ScreenID => 7039;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public ConsignmentOutIssuedReport()
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
			DateTime value = dateTimePickerDate.Value;
			value = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
			int intstatus = 0;
			if (radioButtonAll.Checked)
			{
				intstatus = 2;
			}
			else if (radioButtonSettled.Checked)
			{
				intstatus = 1;
			}
			else if (radioButtonUnsettled.Checked)
			{
				intstatus = 0;
			}
			DataSet data = Factory.ConsignOutSystem.GetConsignmentOutIssuedReport(value, dateControl1.FromDate, dateControl1.ToDate, customerSelector.FromCustomer, customerSelector.ToCustomer, intstatus, customerSelector.MultipleCustomers, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString() + Environment.NewLine + " Customer:" + customerSelector.FromCustomer + " - " + customerSelector.ToCustomer;
			XtraReport report = reportHelper.GetReport("ConsignmentOut Issued");
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'ConsignmentOut Issued.repx'");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Vendors.ConsignmentOutIssuedReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			radioButtonUnsettled = new System.Windows.Forms.RadioButton();
			radioButtonSettled = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			groupBox1 = new System.Windows.Forms.GroupBox();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			label2 = new System.Windows.Forms.Label();
			dateControl1 = new Micromind.DataControls.DateControl();
			productSelector = new Micromind.DataControls.ProductSelector();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(221, 397);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(333, 397);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			radioButtonUnsettled.AutoSize = true;
			radioButtonUnsettled.Location = new System.Drawing.Point(122, 14);
			radioButtonUnsettled.Name = "radioButtonUnsettled";
			radioButtonUnsettled.Size = new System.Drawing.Size(70, 17);
			radioButtonUnsettled.TabIndex = 9;
			radioButtonUnsettled.Text = "Unsettled";
			radioButtonUnsettled.UseVisualStyleBackColor = true;
			radioButtonSettled.AutoSize = true;
			radioButtonSettled.Location = new System.Drawing.Point(55, 14);
			radioButtonSettled.Name = "radioButtonSettled";
			radioButtonSettled.Size = new System.Drawing.Size(58, 17);
			radioButtonSettled.TabIndex = 8;
			radioButtonSettled.Text = "Settled";
			radioButtonSettled.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(263, 29);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(83, 13);
			label1.TabIndex = 11;
			label1.Text = "Settelment Date";
			label1.Visible = false;
			ultraGroupBox1.Controls.Add(customerSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(2, 64);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(440, 100);
			ultraGroupBox1.TabIndex = 12;
			ultraGroupBox1.Text = "Customers";
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(8, 21);
			customerSelector.Name = "customerSelector";
			customerSelector.ShowGroupAndClass = false;
			customerSelector.Size = new System.Drawing.Size(430, 75);
			customerSelector.TabIndex = 0;
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 14);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(36, 17);
			radioButtonAll.TabIndex = 13;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All";
			radioButtonAll.UseVisualStyleBackColor = true;
			groupBox1.Controls.Add(radioButtonAll);
			groupBox1.Controls.Add(radioButtonSettled);
			groupBox1.Controls.Add(radioButtonUnsettled);
			groupBox1.Controls.Add(dateTimePickerDate);
			groupBox1.Location = new System.Drawing.Point(215, 26);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(216, 38);
			groupBox1.TabIndex = 14;
			groupBox1.TabStop = false;
			groupBox1.Text = "Status";
			groupBox1.Visible = false;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(78, 0);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(138, 20);
			dateTimePickerDate.TabIndex = 17;
			dateTimePickerDate.Value = new System.DateTime(2015, 11, 29, 10, 14, 15, 364);
			dateTimePickerDate.Visible = false;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 17);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(57, 13);
			label2.TabIndex = 16;
			label2.Text = "Settlement";
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(65, 12);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(320, 52);
			dateControl1.TabIndex = 15;
			dateControl1.ToDate = new System.DateTime(2017, 10, 18, 23, 59, 59, 59);
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(10, 17);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(411, 185);
			productSelector.TabIndex = 17;
			ultraGroupBox2.Controls.Add(productSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(2, 170);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(440, 208);
			ultraGroupBox2.TabIndex = 18;
			ultraGroupBox2.Text = "Items";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(448, 433);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(label2);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.Controls.Add(label1);
			base.Controls.Add(groupBox1);
			base.Controls.Add(dateControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "ConsignmentOutIssuedReport";
			Text = "Consignment Out Profitability - Summary";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
