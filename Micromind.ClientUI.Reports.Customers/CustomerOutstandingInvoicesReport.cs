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

namespace Micromind.ClientUI.Reports.Customers
{
	public class CustomerOutstandingInvoicesReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private CustomerSelector customerSelector;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowZero;

		private Button buttonClose;

		private RadioButton radioButtonBaseCurrency;

		private Label label1;

		private MMSDateTimePicker dateTimePickerDate;

		private RadioButton radioButtonAccountCurrency;

		private CheckBox checkBoxDetail;

		private UltraGroupBox ultraGroupBox2;

		private LocationSelector locationSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsCustomer;

		public int ScreenID => 7011;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public CustomerOutstandingInvoicesReport()
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
				DataSet dataSet = new DataSet();
				if (!checkBoxDetail.Checked)
				{
					dataSet = Factory.CustomerSystem.GetCustomerOutstandingInvoicesReport(dateTimePickerDate.ValueTo, customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, locationSelector.FromLocation, locationSelector.ToLocation, checkBoxShowZero.Checked, radioButtonAccountCurrency.Checked, customerSelector.MultipleCustomers);
					if (dataSet.Tables.Count > 1)
					{
						string baseCurrencyID = Global.BaseCurrencyID;
						dataSet.Tables[1].Columns.Add("CurrencyNote");
						foreach (DataRow row in dataSet.Tables[1].Rows)
						{
							if (row["CurrencyID"] != DBNull.Value && row["CurrencyID"].ToString() != baseCurrencyID)
							{
								row["CurrencyNote"] = "Cur: " + row["CurrencyID"].ToString() + "  -  Rate: " + decimal.Parse(row["CurrencyRate"].ToString()).ToString(Format.QuantityFormat);
							}
						}
					}
				}
				else
				{
					dataSet = Factory.CustomerSystem.GetCustomerOutstandingInvoicesDetailReport(dateTimePickerDate.ValueTo, customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, locationSelector.FromLocation, locationSelector.ToLocation, checkBoxShowZero.Checked, radioButtonAccountCurrency.Checked, customerSelector.MultipleCustomers);
				}
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "As of date: " + dateTimePickerDate.ValueTo.ToShortDateString();
				reportHelper.AddGeneralReportData(ref dataSet, reportFilter);
				reportHelper.AddFilterData(ref dataSet, GetAllFormControls(this));
				XtraReport xtraReport = checkBoxDetail.Checked ? reportHelper.GetReport("Customer Outstanding Invoices Detail") : reportHelper.GetReport("Customer Outstanding Invoices");
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'Customer Outstanding Invoices.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				}
				else
				{
					xtraReport.DataSource = dataSet;
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

		private void radioButtonSelected_CheckedChanged(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Customers.CustomerOutstandingInvoicesReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			checkBoxShowZero = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			radioButtonBaseCurrency = new System.Windows.Forms.RadioButton();
			label1 = new System.Windows.Forms.Label();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			radioButtonAccountCurrency = new System.Windows.Forms.RadioButton();
			checkBoxDetail = new System.Windows.Forms.CheckBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(282, 373);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 7;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(customerSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 188);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Customers";
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(6, 19);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(455, 163);
			customerSelector.TabIndex = 0;
			checkBoxShowZero.AutoSize = true;
			checkBoxShowZero.Location = new System.Drawing.Point(18, 356);
			checkBoxShowZero.Name = "checkBoxShowZero";
			checkBoxShowZero.Size = new System.Drawing.Size(168, 17);
			checkBoxShowZero.TabIndex = 6;
			checkBoxShowZero.Text = "Show zero balance customers";
			checkBoxShowZero.UseVisualStyleBackColor = true;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(386, 373);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 7;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			radioButtonBaseCurrency.AutoSize = true;
			radioButtonBaseCurrency.Checked = true;
			radioButtonBaseCurrency.Location = new System.Drawing.Point(18, 320);
			radioButtonBaseCurrency.Name = "radioButtonBaseCurrency";
			radioButtonBaseCurrency.Size = new System.Drawing.Size(94, 17);
			radioButtonBaseCurrency.TabIndex = 2;
			radioButtonBaseCurrency.TabStop = true;
			radioButtonBaseCurrency.Text = "Base Currency";
			radioButtonBaseCurrency.UseVisualStyleBackColor = true;
			radioButtonBaseCurrency.Visible = false;
			radioButtonBaseCurrency.CheckedChanged += new System.EventHandler(radioButtonSelected_CheckedChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(15, 295);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(34, 13);
			label1.TabIndex = 9;
			label1.Text = "As of:";
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(55, 291);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(138, 20);
			dateTimePickerDate.TabIndex = 8;
			dateTimePickerDate.Value = new System.DateTime(2013, 12, 19, 16, 33, 43, 285);
			radioButtonAccountCurrency.AutoSize = true;
			radioButtonAccountCurrency.Location = new System.Drawing.Point(118, 320);
			radioButtonAccountCurrency.Name = "radioButtonAccountCurrency";
			radioButtonAccountCurrency.Size = new System.Drawing.Size(148, 17);
			radioButtonAccountCurrency.TabIndex = 10;
			radioButtonAccountCurrency.Text = "Foreign Currency - Hidden";
			radioButtonAccountCurrency.UseVisualStyleBackColor = true;
			radioButtonAccountCurrency.Visible = false;
			checkBoxDetail.AutoSize = true;
			checkBoxDetail.Location = new System.Drawing.Point(18, 379);
			checkBoxDetail.Name = "checkBoxDetail";
			checkBoxDetail.Size = new System.Drawing.Size(80, 17);
			checkBoxDetail.TabIndex = 12;
			checkBoxDetail.Text = "ShowDetail";
			checkBoxDetail.UseVisualStyleBackColor = true;
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 202);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(476, 81);
			ultraGroupBox2.TabIndex = 14;
			ultraGroupBox2.Text = "Location";
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(16, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(494, 409);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(checkBoxDetail);
			base.Controls.Add(radioButtonAccountCurrency);
			base.Controls.Add(label1);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(radioButtonBaseCurrency);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowZero);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "CustomerOutstandingInvoicesReport";
			Text = "Customer Outstanding Invoices";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
