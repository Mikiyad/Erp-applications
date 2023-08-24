using DevExpress.XtraReports.UI;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
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
	public class SalesByCustomerGroupReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private CustomerGroupSelector salespersonSelector;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private DateControl dateControl1;

		private RadioButton radioButtonSummary;

		private RadioButton radioButtonDetail;

		private TemplateMapCombo templateMapCombo;

		private Label label1;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsSales;

		public int ScreenID => 7033;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public SalesByCustomerGroupReport()
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
			ReportHelper reportHelper = new ReportHelper();
			XtraReport xtraReport = null;
			DataSet data;
			if (radioButtonDetail.Checked)
			{
				data = Factory.CustomerSystem.GetSalesByCustomerGroupDetailReport(dateControl1.FromDate, dateControl1.ToDate, salespersonSelector.FromCustomerGroup, salespersonSelector.ToCustomerGroup);
				if (data.Tables.Count > 1)
				{
					string baseCurrencyID = Global.BaseCurrencyID;
					data.Tables[1].Columns.Add("CurrencyNote");
					foreach (DataRow row in data.Tables[1].Rows)
					{
						if (row["CurrencyID"] != DBNull.Value && row["CurrencyID"].ToString() != baseCurrencyID)
						{
							row["CurrencyNote"] = "Cur: " + row["CurrencyID"].ToString() + "  -  Rate: " + decimal.Parse(row["CurrencyRate"].ToString()).ToString(Format.QuantityFormat) + "  -  Amount: " + decimal.Parse(row["TotalFC"].ToString()).ToString(Format.TotalAmountFormat);
						}
					}
				}
			}
			else
			{
				data = Factory.CustomerSystem.GetSalesByCustomerGroupSummaryReport(dateControl1.FromDate, dateControl1.ToDate, salespersonSelector.FromCustomerGroup, salespersonSelector.ToCustomerGroup);
				data.Tables[0].Columns.Add("Total", typeof(decimal));
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				decimal num4 = default(decimal);
				decimal num5 = default(decimal);
				decimal num6 = default(decimal);
				foreach (DataRow row2 in data.Tables[0].Rows)
				{
					num = default(decimal);
					num2 = default(decimal);
					num3 = default(decimal);
					num4 = default(decimal);
					num5 = default(decimal);
					num6 = default(decimal);
					decimal.TryParse(row2["Discount"].ToString(), out num);
					decimal.TryParse(row2["DiscountReturn"].ToString(), out num2);
					decimal.TryParse(row2["SalesReturn"].ToString(), out num3);
					decimal.TryParse(row2["CashSale"].ToString(), out num4);
					decimal.TryParse(row2["CreditSale"].ToString(), out num5);
					if (num2 != 0m)
					{
						num += num2;
						row2["Discount"] = num;
					}
					num6 = num4 + num5 - num3;
					row2["Total"] = num6;
				}
			}
			string text = "";
			text = ((templateMapCombo.SelectedID != "") ? templateMapCombo.SelectedID : ((!radioButtonSummary.Checked) ? "Sales By Customer Group Detail" : "Sales By Customer Group Summary"));
			xtraReport = reportHelper.GetReport(text);
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			if (xtraReport == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Sales By Customer Group Summary.repx'");
				return;
			}
			xtraReport.DataSource = data;
			reportHelper.ShowReport(xtraReport);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Sales.SalesByCustomerGroupReport));
			buttonOK = new System.Windows.Forms.Button();
			salespersonSelector = new Micromind.DataControls.CustomerGroupSelector();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			buttonClose = new System.Windows.Forms.Button();
			dateControl1 = new Micromind.DataControls.DateControl();
			radioButtonSummary = new System.Windows.Forms.RadioButton();
			radioButtonDetail = new System.Windows.Forms.RadioButton();
			templateMapCombo = new Micromind.DataControls.TemplateMapCombo();
			label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)templateMapCombo).BeginInit();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(286, 247);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			salespersonSelector.BackColor = System.Drawing.Color.Transparent;
			salespersonSelector.Location = new System.Drawing.Point(6, 19);
			salespersonSelector.Name = "salespersonSelector";
			salespersonSelector.Size = new System.Drawing.Size(436, 55);
			salespersonSelector.TabIndex = 0;
			ultraGroupBox1.Controls.Add(salespersonSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 76);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Customer Groups";
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 247);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2012, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 94);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(476, 77);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2012, 10, 24, 23, 59, 59, 59);
			radioButtonSummary.AutoSize = true;
			radioButtonSummary.Checked = true;
			radioButtonSummary.Location = new System.Drawing.Point(20, 166);
			radioButtonSummary.Name = "radioButtonSummary";
			radioButtonSummary.Size = new System.Drawing.Size(68, 17);
			radioButtonSummary.TabIndex = 2;
			radioButtonSummary.TabStop = true;
			radioButtonSummary.Text = "Summary";
			radioButtonSummary.UseVisualStyleBackColor = true;
			radioButtonDetail.AutoSize = true;
			radioButtonDetail.Location = new System.Drawing.Point(109, 166);
			radioButtonDetail.Name = "radioButtonDetail";
			radioButtonDetail.Size = new System.Drawing.Size(52, 17);
			radioButtonDetail.TabIndex = 3;
			radioButtonDetail.Text = "Detail";
			radioButtonDetail.UseVisualStyleBackColor = true;
			templateMapCombo.Assigned = false;
			templateMapCombo.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			templateMapCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			templateMapCombo.CustomReportFieldName = "";
			templateMapCombo.CustomReportKey = "";
			templateMapCombo.CustomReportValueType = 1;
			templateMapCombo.DescriptionTextBox = null;
			templateMapCombo.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			templateMapCombo.Editable = true;
			templateMapCombo.FilterFormID = "SalesByCustomerGroupReport";
			templateMapCombo.FilterScreenID = "";
			templateMapCombo.FilterString = "";
			templateMapCombo.HasAllAccount = false;
			templateMapCombo.HasCustom = false;
			templateMapCombo.IsDataLoaded = false;
			templateMapCombo.Location = new System.Drawing.Point(130, 198);
			templateMapCombo.MaxDropDownItems = 12;
			templateMapCombo.Name = "templateMapCombo";
			templateMapCombo.ShowInactiveItems = false;
			templateMapCombo.ShowQuickAdd = true;
			templateMapCombo.Size = new System.Drawing.Size(193, 20);
			templateMapCombo.TabIndex = 96;
			templateMapCombo.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(18, 200);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(89, 13);
			label1.TabIndex = 95;
			label1.Text = "Report Template:";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(497, 278);
			base.Controls.Add(templateMapCombo);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonDetail);
			base.Controls.Add(radioButtonSummary);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "SalesByCustomerGroupReport";
			Text = "Sales by Customer Group Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)templateMapCombo).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
