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
	public class SalesBySalespersonReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private SalespersonSelector salespersonSelector;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private DateControl dateControl1;

		private RadioButton radioButtonSummary;

		private RadioButton radioButtonDetail;

		private RadioButton radioButtonReportTo;

		private TemplateMapCombo templateMapCombo;

		private Label label1;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsSales;

		public int ScreenID => 7038;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public SalesBySalespersonReport()
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
				DataSet data;
				if (radioButtonDetail.Checked)
				{
					data = Factory.SalespersonSystem.GetSalesBySalespersonDetailReport(dateControl1.FromDate, dateControl1.ToDate, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry);
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
				else if (radioButtonSummary.Checked)
				{
					data = Factory.SalespersonSystem.GetSalesBySalespersonSummaryReport(dateControl1.FromDate, dateControl1.ToDate, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry);
				}
				else
				{
					data = Factory.SalespersonSystem.GetSalesBySalespersonByReportToReport(dateControl1.FromDate, dateControl1.ToDate, salespersonSelector.FromSalesperson, salespersonSelector.ToSalesperson, salespersonSelector.FromDivision, salespersonSelector.ToDivision, salespersonSelector.FromGroup, salespersonSelector.ToGroup, salespersonSelector.FromArea, salespersonSelector.ToArea, salespersonSelector.FromCountry, salespersonSelector.ToCountry);
					decimal result = default(decimal);
					decimal num = default(decimal);
					decimal num2 = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal num3 = default(decimal);
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					decimal num4 = default(decimal);
					foreach (DataRow row2 in data.Tables[0].Rows)
					{
						num = default(decimal);
						num2 = default(decimal);
						decimal.TryParse(row2["Sale"].ToString(), out num);
						decimal.TryParse(row2["SaleReturn"].ToString(), out result);
						num2 = num - result;
						row2["Sale"] = num2;
						decimal.TryParse(row2["CashSale"].ToString(), out result3);
						decimal.TryParse(row2["CashSaleReturn"].ToString(), out result2);
						num3 = result3 - result2;
						row2["CashSale"] = num3;
						decimal.TryParse(row2["CreditSale"].ToString(), out result5);
						decimal.TryParse(row2["CreditSaleReturn"].ToString(), out result4);
						num4 = result5 - result4;
						row2["CreditSale"] = num4;
					}
				}
				string text = "";
				text = ((templateMapCombo.SelectedID != "") ? templateMapCombo.SelectedID : (radioButtonSummary.Checked ? "Sales By Salesperson Summary" : ((!radioButtonDetail.Checked) ? "Sales By Salesperson ReportTo" : "Sales By Salesperson Detail")));
				xtraReport = reportHelper.GetReport(text);
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Sales By Salesperson.repx'");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Sales.SalesBySalespersonReport));
			buttonOK = new System.Windows.Forms.Button();
			salespersonSelector = new Micromind.DataControls.SalespersonSelector();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			buttonClose = new System.Windows.Forms.Button();
			dateControl1 = new Micromind.DataControls.DateControl();
			radioButtonSummary = new System.Windows.Forms.RadioButton();
			radioButtonDetail = new System.Windows.Forms.RadioButton();
			radioButtonReportTo = new System.Windows.Forms.RadioButton();
			templateMapCombo = new Micromind.DataControls.TemplateMapCombo();
			label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)templateMapCombo).BeginInit();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(280, 323);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			salespersonSelector.BackColor = System.Drawing.Color.Transparent;
			salespersonSelector.Location = new System.Drawing.Point(6, 19);
			salespersonSelector.Name = "salespersonSelector";
			salespersonSelector.Size = new System.Drawing.Size(436, 150);
			salespersonSelector.TabIndex = 0;
			ultraGroupBox1.Controls.Add(salespersonSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(462, 172);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Salespersons";
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(384, 323);
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
			dateControl1.Location = new System.Drawing.Point(12, 186);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(476, 62);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2012, 10, 24, 23, 59, 59, 59);
			radioButtonSummary.AutoSize = true;
			radioButtonSummary.Checked = true;
			radioButtonSummary.Location = new System.Drawing.Point(27, 244);
			radioButtonSummary.Name = "radioButtonSummary";
			radioButtonSummary.Size = new System.Drawing.Size(68, 17);
			radioButtonSummary.TabIndex = 2;
			radioButtonSummary.TabStop = true;
			radioButtonSummary.Text = "Summary";
			radioButtonSummary.UseVisualStyleBackColor = true;
			radioButtonDetail.AutoSize = true;
			radioButtonDetail.Location = new System.Drawing.Point(116, 244);
			radioButtonDetail.Name = "radioButtonDetail";
			radioButtonDetail.Size = new System.Drawing.Size(52, 17);
			radioButtonDetail.TabIndex = 3;
			radioButtonDetail.Text = "Detail";
			radioButtonDetail.UseVisualStyleBackColor = true;
			radioButtonReportTo.AutoSize = true;
			radioButtonReportTo.Location = new System.Drawing.Point(191, 244);
			radioButtonReportTo.Name = "radioButtonReportTo";
			radioButtonReportTo.Size = new System.Drawing.Size(73, 17);
			radioButtonReportTo.TabIndex = 6;
			radioButtonReportTo.Text = "Report To";
			radioButtonReportTo.UseVisualStyleBackColor = true;
			templateMapCombo.Assigned = false;
			templateMapCombo.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			templateMapCombo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			templateMapCombo.CustomReportFieldName = "";
			templateMapCombo.CustomReportKey = "";
			templateMapCombo.CustomReportValueType = 1;
			templateMapCombo.DescriptionTextBox = null;
			templateMapCombo.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			templateMapCombo.Editable = true;
			templateMapCombo.FilterFormID = "SalesBySalespersonReport";
			templateMapCombo.FilterScreenID = "";
			templateMapCombo.FilterString = "";
			templateMapCombo.HasAllAccount = false;
			templateMapCombo.HasCustom = false;
			templateMapCombo.IsDataLoaded = false;
			templateMapCombo.Location = new System.Drawing.Point(139, 284);
			templateMapCombo.MaxDropDownItems = 12;
			templateMapCombo.Name = "templateMapCombo";
			templateMapCombo.ShowInactiveItems = false;
			templateMapCombo.ShowQuickAdd = true;
			templateMapCombo.Size = new System.Drawing.Size(195, 20);
			templateMapCombo.TabIndex = 96;
			templateMapCombo.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(15, 284);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(89, 13);
			label1.TabIndex = 95;
			label1.Text = "Report Template:";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(491, 354);
			base.Controls.Add(templateMapCombo);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonReportTo);
			base.Controls.Add(radioButtonDetail);
			base.Controls.Add(radioButtonSummary);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "SalesBySalespersonReport";
			Text = "Sales by Salesperson Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)templateMapCombo).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
