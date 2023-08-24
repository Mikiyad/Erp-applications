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

namespace Micromind.ClientUI.Reports.Sales
{
	public class SalesByItemCustomerSalespersonReport : Form, IForm
	{
		private string selectedText = "";

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private DateControl dateControl1;

		private UltraGroupBox ultraGroupBox1;

		private ProductSelector productSelector;

		private UltraGroupBox ultraGroupBox2;

		private CustomerSelector customerSelector;

		private SalespersonSelector salespersonSelector;

		private UltraGroupBox ultraGroupBox3;

		private UltraGroupBox ultraGroupBox4;

		private LocationSelector locationSelector;

		private ComboBox comboBoxGroupBy;

		private Label label3;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsSales;

		public int ScreenID => 7035;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public SalesByItemCustomerSalespersonReport()
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

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				ReportHelper reportHelper = new ReportHelper();
				XtraReport xtraReport = null;
				string a = "";
				if (selectedText == "Items")
				{
					a = "1";
				}
				else if (selectedText == "Customer")
				{
					a = "2";
				}
				else if (selectedText == "Salesperson")
				{
					a = "3";
				}
				else if (selectedText == "Location")
				{
					a = "4";
				}
				else if (a == "")
				{
					a = "1";
				}
				DataSet data = Factory.ProductSystem.GetSalesByItemCustomerSalespersonReport(dateControl1.FromDate, dateControl1.ToDate, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry, locationSelector.FromLocation, locationSelector.ToLocation, customerSelector.MultipleCustomers, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
				xtraReport = reportHelper.GetReport("Sales By Item Customer Salesperson");
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Sales By Item Customer Salesperson.repx'");
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

		private void comboBoxGroupBy_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxGroupBy.SelectedIndex == 0)
			{
				selectedText = "Items";
			}
			else if (comboBoxGroupBy.SelectedIndex == 1)
			{
				selectedText = "Customer";
			}
			else if (comboBoxGroupBy.SelectedIndex == 2)
			{
				selectedText = "Salesperson";
			}
			else if (comboBoxGroupBy.SelectedIndex == 3)
			{
				selectedText = "Location";
			}
		}

		private void comboBoxGroupBy_SelectedValueChanged(object sender, EventArgs e)
		{
			selectedText = comboBoxGroupBy.SelectedText;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Sales.SalesByItemCustomerSalespersonReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			dateControl1 = new Micromind.DataControls.DateControl();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			salespersonSelector = new Micromind.DataControls.SalespersonSelector();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			comboBoxGroupBy = new System.Windows.Forms.ComboBox();
			label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(743, 408);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(847, 408);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(513, 310);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(318, 54);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2017, 10, 18, 23, 59, 59, 59);
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 210);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Items";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(5, 18);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(446, 186);
			productSelector.TabIndex = 0;
			ultraGroupBox2.Controls.Add(customerSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(501, 12);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(448, 210);
			ultraGroupBox2.TabIndex = 6;
			ultraGroupBox2.Text = "Customers";
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(6, 19);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(436, 156);
			customerSelector.TabIndex = 0;
			salespersonSelector.BackColor = System.Drawing.Color.Transparent;
			salespersonSelector.Location = new System.Drawing.Point(6, 19);
			salespersonSelector.Name = "salespersonSelector";
			salespersonSelector.Size = new System.Drawing.Size(436, 145);
			salespersonSelector.TabIndex = 0;
			ultraGroupBox3.Controls.Add(salespersonSelector);
			ultraGroupBox3.Location = new System.Drawing.Point(12, 228);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(476, 174);
			ultraGroupBox3.TabIndex = 7;
			ultraGroupBox3.Text = "Salespersons";
			ultraGroupBox4.Controls.Add(locationSelector);
			ultraGroupBox4.Location = new System.Drawing.Point(501, 228);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(448, 76);
			ultraGroupBox4.TabIndex = 8;
			ultraGroupBox4.Text = "Locations";
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(436, 55);
			locationSelector.TabIndex = 0;
			comboBoxGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxGroupBy.FormattingEnabled = true;
			comboBoxGroupBy.Items.AddRange(new object[4]
			{
				"Items",
				"Customer",
				"Salesperson",
				"Location"
			});
			comboBoxGroupBy.Location = new System.Drawing.Point(606, 367);
			comboBoxGroupBy.Name = "comboBoxGroupBy";
			comboBoxGroupBy.Size = new System.Drawing.Size(121, 21);
			comboBoxGroupBy.TabIndex = 9;
			comboBoxGroupBy.Visible = false;
			comboBoxGroupBy.SelectedIndexChanged += new System.EventHandler(comboBoxGroupBy_SelectedIndexChanged);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(520, 370);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(54, 13);
			label3.TabIndex = 18;
			label3.Text = "Group By:";
			label3.Visible = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(962, 440);
			base.Controls.Add(label3);
			base.Controls.Add(comboBoxGroupBy);
			base.Controls.Add(ultraGroupBox4);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "SalesByItemCustomerSalespersonReport";
			Text = "Sales by Item, Customer, Salesperson,Location Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
