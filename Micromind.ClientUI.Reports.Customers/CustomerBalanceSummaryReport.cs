using DevExpress.XtraReports.UI;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Customers
{
	public class CustomerBalanceSummaryReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private CustomerSelector customerSelector;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowZero;

		private Button buttonClose;

		private RadioButton radioButtonBase;

		private RadioButton radioButtonFC;

		private UltraGroupBox ultraGroupBox2;

		private SalespersonSelector salespersonSelector;

		private UltraGroupBox ultraGroupBox3;

		private CountrySelector countrySelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsCustomer;

		public int ScreenID => 7012;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public CustomerBalanceSummaryReport()
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

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DataSet data = Factory.CustomerSystem.GetCustomerBalanceSummary(customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, customerSelector.FromCountry, customerSelector.ToCountry, customerSelector.FromArea, customerSelector.ToArea, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry, checkBoxShowZero.Checked, radioButtonFC.Checked, customerSelector.MultipleCustomers);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Customer Balance Summary");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'Customer Balance Summar.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Customers.CustomerBalanceSummaryReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			checkBoxShowZero = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			radioButtonBase = new System.Windows.Forms.RadioButton();
			radioButtonFC = new System.Windows.Forms.RadioButton();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			salespersonSelector = new Micromind.DataControls.SalespersonSelector();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			countrySelector = new Micromind.DataControls.CountrySelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(286, 413);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(customerSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 180);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Customers";
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(6, 19);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(436, 155);
			customerSelector.TabIndex = 2;
			checkBoxShowZero.AutoSize = true;
			checkBoxShowZero.Location = new System.Drawing.Point(24, 387);
			checkBoxShowZero.Name = "checkBoxShowZero";
			checkBoxShowZero.Size = new System.Drawing.Size(168, 17);
			checkBoxShowZero.TabIndex = 3;
			checkBoxShowZero.Text = "Show zero balance customers";
			checkBoxShowZero.UseVisualStyleBackColor = true;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 413);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			radioButtonBase.AutoSize = true;
			radioButtonBase.Checked = true;
			radioButtonBase.Location = new System.Drawing.Point(24, 364);
			radioButtonBase.Name = "radioButtonBase";
			radioButtonBase.Size = new System.Drawing.Size(94, 17);
			radioButtonBase.TabIndex = 1;
			radioButtonBase.TabStop = true;
			radioButtonBase.Text = "Base Currency";
			radioButtonBase.UseVisualStyleBackColor = true;
			radioButtonFC.AutoSize = true;
			radioButtonFC.Location = new System.Drawing.Point(140, 365);
			radioButtonFC.Name = "radioButtonFC";
			radioButtonFC.Size = new System.Drawing.Size(105, 17);
			radioButtonFC.TabIndex = 2;
			radioButtonFC.Text = "Foreign Currency";
			radioButtonFC.UseVisualStyleBackColor = true;
			ultraGroupBox2.Controls.Add(salespersonSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 198);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(475, 161);
			ultraGroupBox2.TabIndex = 6;
			ultraGroupBox2.Text = "Salespersons";
			salespersonSelector.BackColor = System.Drawing.Color.Transparent;
			salespersonSelector.Location = new System.Drawing.Point(6, 19);
			salespersonSelector.Name = "salespersonSelector";
			salespersonSelector.Size = new System.Drawing.Size(433, 136);
			salespersonSelector.TabIndex = 0;
			ultraGroupBox3.Controls.Add(countrySelector);
			ultraGroupBox3.Location = new System.Drawing.Point(-238, 499);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(475, 76);
			ultraGroupBox3.TabIndex = 7;
			ultraGroupBox3.Text = "Country";
			ultraGroupBox3.Visible = false;
			countrySelector.BackColor = System.Drawing.Color.Transparent;
			countrySelector.CustomReportFieldName = "";
			countrySelector.CustomReportKey = "";
			countrySelector.CustomReportValueType = 1;
			countrySelector.Location = new System.Drawing.Point(7, 20);
			countrySelector.Name = "countrySelector";
			countrySelector.Size = new System.Drawing.Size(432, 50);
			countrySelector.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(502, 443);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(radioButtonFC);
			base.Controls.Add(radioButtonBase);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowZero);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "CustomerBalanceSummaryReport";
			Text = "Customer Balance Summary";
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
