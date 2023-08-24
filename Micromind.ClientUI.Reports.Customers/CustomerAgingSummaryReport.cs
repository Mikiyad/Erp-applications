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
	public class CustomerAgingSummaryReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private CustomerSelector customerSelector;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowZero;

		private Button buttonClose;

		private CheckBox checkBoxAutoAllocate;

		private MMSDateTimePicker dateTimePickerDate;

		private Label label1;

		private RadioButton radioButtonSummary;

		private RadioButton radioButtonDetail;

		private UltraGroupBox ultraGroupBox2;

		private SalespersonSelector salespersonSelector;

		private UltraGroupBox ultraGroupBoxGroupBy;

		private RadioButton radioButtonGPaymentTerm;

		private RadioButton radioButtonGSalesMan;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsCustomer;

		public int ScreenID => 7010;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public CustomerAgingSummaryReport()
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

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet dataSet = null;
				dataSet = ((!radioButtonSummary.Checked) ? Factory.CustomerSystem.GetCustomerAgingDetail(customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, dateTimePickerDate.ValueTo, checkBoxShowZero.Checked, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry, customerSelector.MultipleCustomers) : Factory.CustomerSystem.GetCustomerAgingSummary(customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, dateTimePickerDate.ValueTo, checkBoxShowZero.Checked, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry, customerSelector.MultipleCustomers));
				DataTable dataTable = dataSet.Tables.Add("AgingHeaders");
				dataTable.Columns.Add("Month1", typeof(string));
				dataTable.Columns.Add("Month2", typeof(string));
				dataTable.Columns.Add("Month3", typeof(string));
				dataTable.Columns.Add("Month4", typeof(string));
				dataTable.Columns.Add("Month5", typeof(string));
				dataTable.Columns.Add("Month6", typeof(string));
				dataTable.Columns.Add("Over", typeof(string));
				DataRow dataRow = dataTable.Rows.Add();
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingByDueDate, defaultValue: true);
				bool companyOption = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging1, defaultValue: true);
				bool companyOption2 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging2, defaultValue: true);
				bool companyOption3 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging3, defaultValue: true);
				bool companyOption4 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging4, defaultValue: true);
				bool companyOption5 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging5, defaultValue: true);
				bool companyOption6 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ShowAging6, defaultValue: true);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom0, 0);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom1, 1);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom2, 31);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom3, 61);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom4, 91);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom5, 121);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom6, 151);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingFrom7, 181);
				int companyOption7 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo0, 0);
				int companyOption8 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo1, 30);
				int companyOption9 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo2, 60);
				int companyOption10 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo3, 90);
				int companyOption11 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo4, 120);
				int companyOption12 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo5, 150);
				int companyOption13 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo6, 180);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingTo7, 999);
				CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName0, "Current");
				string companyOption14 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName1, "1-30 Days");
				string companyOption15 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName2, "31-60 Days");
				string companyOption16 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName3, "61-90 Days");
				string companyOption17 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName4, "91-120 Days");
				string companyOption18 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName5, "121-150 Days");
				string companyOption19 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName6, "151-180 Days");
				string companyOption20 = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.AgingName7, "Over 180 Days");
				int num = 0;
				num = (companyOption6 ? companyOption13 : (companyOption5 ? companyOption12 : (companyOption4 ? companyOption11 : (companyOption3 ? companyOption10 : (companyOption2 ? companyOption9 : ((!companyOption) ? companyOption7 : companyOption8))))));
				companyOption20 = "Over " + num + " Days";
				dataRow["Month1"] = companyOption14;
				dataRow["Month2"] = companyOption15;
				dataRow["Month3"] = companyOption16;
				dataRow["Month4"] = companyOption17;
				dataRow["Month5"] = companyOption18;
				dataRow["Month6"] = companyOption19;
				dataRow["Over"] = companyOption20;
				dataRow.EndEdit();
				if (checkBoxAutoAllocate.Checked && dataSet != null)
				{
					DataTable dataTable2 = dataSet.Tables["Customer"];
					foreach (DataRow row in dataTable2.Rows)
					{
						decimal result = default(decimal);
						decimal result2 = default(decimal);
						decimal result3 = default(decimal);
						decimal result4 = default(decimal);
						decimal result5 = default(decimal);
						decimal result6 = default(decimal);
						decimal result7 = default(decimal);
						decimal result8 = default(decimal);
						decimal result9 = default(decimal);
						decimal.TryParse(row["Unallocated"].ToString(), out result);
						if (!(result == 0m))
						{
							decimal.TryParse(row["Over"].ToString(), out result2);
							if (result > 0m && result2 > 0m)
							{
								if (result2 > result)
								{
									result2 -= result;
									result = default(decimal);
								}
								else
								{
									result -= result2;
									result2 = default(decimal);
								}
								row["Over"] = result2;
							}
							if (dataTable2.Columns.Contains("Month6"))
							{
								decimal.TryParse(row["Month6"].ToString(), out result3);
								if (result > 0m && result3 > 0m)
								{
									if (result3 > result)
									{
										result3 -= result;
										result = default(decimal);
									}
									else
									{
										result -= result3;
										result3 = default(decimal);
									}
								}
								row["Month6"] = result3;
							}
							if (dataTable2.Columns.Contains("Month5"))
							{
								decimal.TryParse(row["Month5"].ToString(), out result4);
								if (result > 0m && result4 > 0m)
								{
									if (result4 > result)
									{
										result4 -= result;
										result = default(decimal);
									}
									else
									{
										result -= result4;
										result4 = default(decimal);
									}
								}
								row["Month5"] = result4;
							}
							if (dataTable2.Columns.Contains("Month4"))
							{
								decimal.TryParse(row["Month4"].ToString(), out result5);
								if (result > 0m && result5 > 0m)
								{
									if (result5 > result)
									{
										result5 -= result;
										result = default(decimal);
									}
									else
									{
										result -= result5;
										result5 = default(decimal);
									}
								}
								row["Month4"] = result5;
							}
							if (dataTable2.Columns.Contains("Month3"))
							{
								decimal.TryParse(row["Month3"].ToString(), out result6);
								if (result > 0m && result6 > 0m)
								{
									if (result6 > result)
									{
										result6 -= result;
										result = default(decimal);
									}
									else
									{
										result -= result6;
										result6 = default(decimal);
									}
								}
								row["Month3"] = result6;
							}
							if (dataTable2.Columns.Contains("Month2"))
							{
								decimal.TryParse(row["Month2"].ToString(), out result7);
								if (result > 0m && result7 > 0m)
								{
									if (result7 > result)
									{
										result7 -= result;
										result = default(decimal);
									}
									else
									{
										result -= result7;
										result7 = default(decimal);
									}
								}
								row["Month2"] = result7;
							}
							if (dataTable2.Columns.Contains("Month1"))
							{
								decimal.TryParse(row["Month1"].ToString(), out result8);
								if (result > 0m && result8 > 0m)
								{
									if (result8 > result)
									{
										result8 -= result;
										result = default(decimal);
									}
									else
									{
										result -= result8;
										result8 = default(decimal);
									}
								}
								row["Month1"] = result8;
							}
							decimal.TryParse(row["CurrentBalance"].ToString(), out result9);
							if (result > 0m && result9 > 0m)
							{
								if (result9 > result)
								{
									result9 -= result;
									result = default(decimal);
								}
								else
								{
									result -= result9;
									result9 = default(decimal);
								}
								row["CurrentBalance"] = result9;
							}
							row["Unallocated"] = result;
						}
					}
				}
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "As of Date: " + dateTimePickerDate.ValueTo.ToShortDateString();
				reportHelper.AddGeneralReportData(ref dataSet, reportFilter);
				reportHelper.AddFilterData(ref dataSet, GetAllFormControls(this));
				XtraReport xtraReport = (!radioButtonSummary.Checked) ? reportHelper.GetReport("Customer Aging Detail") : ((!radioButtonGSalesMan.Checked) ? reportHelper.GetReport("Customer Aging Summary PTWise") : reportHelper.GetReport("Customer Aging Summary"));
				if (xtraReport == null)
				{
					if (radioButtonSummary.Checked)
					{
						if (radioButtonGSalesMan.Checked)
						{
							ErrorHelper.ErrorMessage("Cannot find the report file", "'Customer Aging Summary.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
						}
						else
						{
							ErrorHelper.ErrorMessage("Cannot find the report file", "'Customer Aging Summary PTWise.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
						}
					}
					else
					{
						ErrorHelper.ErrorMessage("Cannot find the report file", "'Customer Aging Detail.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
					}
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

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void radioButtonDetail_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonDetail.Checked)
			{
				checkBoxAutoAllocate.Checked = false;
				checkBoxAutoAllocate.Enabled = false;
				ultraGroupBoxGroupBy.Visible = false;
			}
			else
			{
				checkBoxAutoAllocate.Enabled = true;
				ultraGroupBoxGroupBy.Visible = true;
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Customers.CustomerAgingSummaryReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			checkBoxShowZero = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			checkBoxAutoAllocate = new System.Windows.Forms.CheckBox();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			label1 = new System.Windows.Forms.Label();
			radioButtonSummary = new System.Windows.Forms.RadioButton();
			radioButtonDetail = new System.Windows.Forms.RadioButton();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			salespersonSelector = new Micromind.DataControls.SalespersonSelector();
			ultraGroupBoxGroupBy = new Infragistics.Win.Misc.UltraGroupBox();
			radioButtonGPaymentTerm = new System.Windows.Forms.RadioButton();
			radioButtonGSalesMan = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBoxGroupBy).BeginInit();
			ultraGroupBoxGroupBy.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(274, 453);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(customerSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 4);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(449, 180);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Customers";
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(6, 19);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(438, 155);
			customerSelector.TabIndex = 0;
			checkBoxShowZero.AutoSize = true;
			checkBoxShowZero.Location = new System.Drawing.Point(18, 403);
			checkBoxShowZero.Name = "checkBoxShowZero";
			checkBoxShowZero.Size = new System.Drawing.Size(168, 17);
			checkBoxShowZero.TabIndex = 2;
			checkBoxShowZero.Text = "Show zero balance customers";
			checkBoxShowZero.UseVisualStyleBackColor = true;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(378, 453);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			checkBoxAutoAllocate.AutoSize = true;
			checkBoxAutoAllocate.Location = new System.Drawing.Point(18, 426);
			checkBoxAutoAllocate.Name = "checkBoxAutoAllocate";
			checkBoxAutoAllocate.Size = new System.Drawing.Size(194, 17);
			checkBoxAutoAllocate.TabIndex = 3;
			checkBoxAutoAllocate.Text = "Auto allocate unallocated payments";
			checkBoxAutoAllocate.UseVisualStyleBackColor = true;
			dateTimePickerDate.CustomFormat = " ";
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(56, 350);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(130, 20);
			dateTimePickerDate.TabIndex = 1;
			dateTimePickerDate.Value = new System.DateTime(2014, 1, 2, 14, 31, 49, 714);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(16, 353);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(34, 13);
			label1.TabIndex = 6;
			label1.Text = "As of:";
			radioButtonSummary.AutoSize = true;
			radioButtonSummary.Checked = true;
			radioButtonSummary.Location = new System.Drawing.Point(246, 371);
			radioButtonSummary.Name = "radioButtonSummary";
			radioButtonSummary.Size = new System.Drawing.Size(98, 17);
			radioButtonSummary.TabIndex = 7;
			radioButtonSummary.TabStop = true;
			radioButtonSummary.Text = "Aging Summary";
			radioButtonSummary.UseVisualStyleBackColor = true;
			radioButtonDetail.AutoSize = true;
			radioButtonDetail.Location = new System.Drawing.Point(350, 371);
			radioButtonDetail.Name = "radioButtonDetail";
			radioButtonDetail.Size = new System.Drawing.Size(82, 17);
			radioButtonDetail.TabIndex = 7;
			radioButtonDetail.Text = "Aging Detail";
			radioButtonDetail.UseVisualStyleBackColor = true;
			radioButtonDetail.CheckedChanged += new System.EventHandler(radioButtonDetail_CheckedChanged);
			ultraGroupBox2.Controls.Add(salespersonSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 184);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(449, 158);
			ultraGroupBox2.TabIndex = 8;
			ultraGroupBox2.Text = "Salespersons";
			salespersonSelector.BackColor = System.Drawing.Color.Transparent;
			salespersonSelector.Location = new System.Drawing.Point(6, 14);
			salespersonSelector.Name = "salespersonSelector";
			salespersonSelector.Size = new System.Drawing.Size(436, 139);
			salespersonSelector.TabIndex = 0;
			ultraGroupBoxGroupBy.Controls.Add(radioButtonGPaymentTerm);
			ultraGroupBoxGroupBy.Controls.Add(radioButtonGSalesMan);
			ultraGroupBoxGroupBy.Location = new System.Drawing.Point(220, 392);
			ultraGroupBoxGroupBy.Name = "ultraGroupBoxGroupBy";
			ultraGroupBoxGroupBy.Size = new System.Drawing.Size(236, 53);
			ultraGroupBoxGroupBy.TabIndex = 9;
			ultraGroupBoxGroupBy.Text = "Based on";
			radioButtonGPaymentTerm.AutoSize = true;
			radioButtonGPaymentTerm.Location = new System.Drawing.Point(119, 21);
			radioButtonGPaymentTerm.Name = "radioButtonGPaymentTerm";
			radioButtonGPaymentTerm.Size = new System.Drawing.Size(93, 17);
			radioButtonGPaymentTerm.TabIndex = 8;
			radioButtonGPaymentTerm.Text = "Payment Term";
			radioButtonGPaymentTerm.UseVisualStyleBackColor = true;
			radioButtonGSalesMan.AutoSize = true;
			radioButtonGSalesMan.Checked = true;
			radioButtonGSalesMan.Location = new System.Drawing.Point(19, 21);
			radioButtonGSalesMan.Name = "radioButtonGSalesMan";
			radioButtonGSalesMan.Size = new System.Drawing.Size(72, 17);
			radioButtonGSalesMan.TabIndex = 9;
			radioButtonGSalesMan.TabStop = true;
			radioButtonGSalesMan.Text = "SalesMan";
			radioButtonGSalesMan.UseVisualStyleBackColor = true;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(484, 489);
			base.Controls.Add(ultraGroupBoxGroupBy);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(radioButtonDetail);
			base.Controls.Add(radioButtonSummary);
			base.Controls.Add(label1);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(checkBoxAutoAllocate);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowZero);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "CustomerAgingSummaryReport";
			Text = "Customer Aging Summary & Detail";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBoxGroupBy).EndInit();
			ultraGroupBoxGroupBy.ResumeLayout(false);
			ultraGroupBoxGroupBy.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
