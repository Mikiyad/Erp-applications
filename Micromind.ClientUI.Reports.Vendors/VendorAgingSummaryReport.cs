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
	public class VendorAgingSummaryReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private VendorSelector vendorSelector;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowZero;

		private Button buttonClose;

		private Label label1;

		private MMSDateTimePicker dateTimePickerDate;

		private RadioButton radioButtonDetail;

		private RadioButton radioButtonSummary;

		private CheckBox checkBoxAutoAllocate;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsVendor;

		public int ScreenID => 7040;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public VendorAgingSummaryReport()
		{
			InitializeComponent();
			dateTimePickerDate.Value = DateTime.Now;
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
			DataSet dataSet = null;
			dataSet = ((!radioButtonSummary.Checked) ? Factory.VendorSystem.GetVendorAgingDetail(vendorSelector.FromVendor, vendorSelector.ToVendor, vendorSelector.FromClass, vendorSelector.ToClass, vendorSelector.FromGroup, vendorSelector.ToGroup, dateTimePickerDate.ValueTo, checkBoxShowZero.Checked, vendorSelector.MultipleVendors) : Factory.VendorSystem.GetVendorAgingSummary(vendorSelector.FromVendor, vendorSelector.ToVendor, vendorSelector.FromClass, vendorSelector.ToClass, vendorSelector.FromGroup, vendorSelector.ToGroup, dateTimePickerDate.ValueTo, checkBoxShowZero.Checked, vendorSelector.MultipleVendors));
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
				DataTable dataTable2 = dataSet.Tables["Vendor"];
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
						if (dataTable2.Columns.Contains("Over"))
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
							}
							row["over"] = result2;
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
			string reportFilter = "";
			reportHelper.AddGeneralReportData(ref dataSet, reportFilter);
			reportHelper.AddFilterData(ref dataSet, GetAllFormControls(this));
			XtraReport xtraReport = (!radioButtonSummary.Checked) ? reportHelper.GetReport("Vendor Aging Detail") : reportHelper.GetReport("Vendor Aging Summary");
			if (xtraReport == null)
			{
				if (radioButtonSummary.Checked)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'Vendor Aging Summary.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				}
				else
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'Vendor Aging Detail.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				}
			}
			else
			{
				xtraReport.DataSource = dataSet;
				reportHelper.ShowReport(xtraReport);
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Vendors.VendorAgingSummaryReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			vendorSelector = new Micromind.DataControls.VendorSelector();
			checkBoxShowZero = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			radioButtonDetail = new System.Windows.Forms.RadioButton();
			radioButtonSummary = new System.Windows.Forms.RadioButton();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			checkBoxAutoAllocate = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(286, 270);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 5;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(vendorSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 155);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Vendors";
			vendorSelector.BackColor = System.Drawing.Color.Transparent;
			vendorSelector.CustomReportFieldName = "";
			vendorSelector.CustomReportKey = "";
			vendorSelector.CustomReportValueType = 1;
			vendorSelector.Location = new System.Drawing.Point(6, 19);
			vendorSelector.Name = "vendorSelector";
			vendorSelector.Size = new System.Drawing.Size(464, 136);
			vendorSelector.TabIndex = 0;
			checkBoxShowZero.AutoSize = true;
			checkBoxShowZero.Location = new System.Drawing.Point(18, 205);
			checkBoxShowZero.Name = "checkBoxShowZero";
			checkBoxShowZero.Size = new System.Drawing.Size(158, 17);
			checkBoxShowZero.TabIndex = 3;
			checkBoxShowZero.Text = "Show zero balance vendors";
			checkBoxShowZero.UseVisualStyleBackColor = true;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 270);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 6;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(15, 175);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(34, 13);
			label1.TabIndex = 8;
			label1.Text = "As of:";
			radioButtonDetail.AutoSize = true;
			radioButtonDetail.Location = new System.Drawing.Point(359, 176);
			radioButtonDetail.Name = "radioButtonDetail";
			radioButtonDetail.Size = new System.Drawing.Size(82, 17);
			radioButtonDetail.TabIndex = 2;
			radioButtonDetail.Text = "Aging Detail";
			radioButtonDetail.UseVisualStyleBackColor = true;
			radioButtonSummary.AutoSize = true;
			radioButtonSummary.Checked = true;
			radioButtonSummary.Location = new System.Drawing.Point(255, 176);
			radioButtonSummary.Name = "radioButtonSummary";
			radioButtonSummary.Size = new System.Drawing.Size(98, 17);
			radioButtonSummary.TabIndex = 1;
			radioButtonSummary.TabStop = true;
			radioButtonSummary.Text = "Aging Summary";
			radioButtonSummary.UseVisualStyleBackColor = true;
			dateTimePickerDate.CustomFormat = " ";
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(55, 173);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(130, 20);
			dateTimePickerDate.TabIndex = 0;
			dateTimePickerDate.Value = new System.DateTime(2014, 1, 2, 14, 31, 49, 714);
			checkBoxAutoAllocate.AutoSize = true;
			checkBoxAutoAllocate.Location = new System.Drawing.Point(18, 228);
			checkBoxAutoAllocate.Name = "checkBoxAutoAllocate";
			checkBoxAutoAllocate.Size = new System.Drawing.Size(194, 17);
			checkBoxAutoAllocate.TabIndex = 4;
			checkBoxAutoAllocate.Text = "Auto allocate unallocated payments";
			checkBoxAutoAllocate.UseVisualStyleBackColor = true;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(497, 301);
			base.Controls.Add(checkBoxAutoAllocate);
			base.Controls.Add(radioButtonDetail);
			base.Controls.Add(radioButtonSummary);
			base.Controls.Add(label1);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowZero);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "VendorAgingSummaryReport";
			Text = "Vendor Aging Summary";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
