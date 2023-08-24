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

namespace Micromind.ClientUI.Reports.Items
{
	public class InventoryTransactionsLotwiseReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private ProductSelector productSelector;

		private DateControl dateControl1;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox2;

		private RadioButton rdoProduction;

		private RadioButton rdoExpiry;

		private DateControl dateControl2;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7022;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public InventoryTransactionsLotwiseReport()
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
			bool? production = null;
			if (rdoProduction.Checked)
			{
				production = true;
			}
			else if (rdoExpiry.Checked)
			{
				production = false;
			}
			DataSet data = Factory.ProductSystem.GetInventoryTransactionLotwiseReport(dateControl1.FromDate, dateControl1.ToDate, dateControl2.FromDate, dateControl2.ToDate, locationSelector.FromLocation, locationSelector.ToLocation, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, production, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
			if (data.Tables.Count > 1)
			{
				data.Tables[1].Columns.Add("SysDocTypeName");
				foreach (DataRow row in data.Tables[1].Rows)
				{
					if (row["SysDocType"] != DBNull.Value)
					{
						int sysDocType = int.Parse(row["SysDocType"].ToString());
						row["SysDocTypeName"] = PublicFunctions.GetSysDocTypeString(sysDocType);
					}
				}
			}
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Inventory Ledger Lotwise");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Inventory Ledger Lotwise.repx'");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.InventoryTransactionsLotwiseReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			buttonClose = new System.Windows.Forms.Button();
			dateControl1 = new Micromind.DataControls.DateControl();
			locationSelector = new Micromind.DataControls.LocationSelector();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			rdoProduction = new System.Windows.Forms.RadioButton();
			rdoExpiry = new System.Windows.Forms.RadioButton();
			dateControl2 = new Micromind.DataControls.DateControl();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(257, 488);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(102, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(451, 207);
			ultraGroupBox1.TabIndex = 3;
			ultraGroupBox1.Text = "Items";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(5, 18);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(446, 187);
			productSelector.TabIndex = 0;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(361, 488);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(102, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2015, 5, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(15, 306);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(451, 65);
			dateControl1.TabIndex = 6;
			dateControl1.ToDate = new System.DateTime(2015, 5, 31, 23, 59, 59, 59);
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 18);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(424, 59);
			locationSelector.TabIndex = 7;
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 222);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(451, 77);
			ultraGroupBox2.TabIndex = 8;
			ultraGroupBox2.Text = "Location";
			rdoProduction.AutoSize = true;
			rdoProduction.Location = new System.Drawing.Point(17, 372);
			rdoProduction.Name = "rdoProduction";
			rdoProduction.Size = new System.Drawing.Size(102, 17);
			rdoProduction.TabIndex = 9;
			rdoProduction.Text = "Production Date";
			rdoProduction.UseVisualStyleBackColor = true;
			rdoExpiry.AutoSize = true;
			rdoExpiry.Location = new System.Drawing.Point(131, 372);
			rdoExpiry.Name = "rdoExpiry";
			rdoExpiry.Size = new System.Drawing.Size(79, 17);
			rdoExpiry.TabIndex = 10;
			rdoExpiry.Text = "Expiry Date";
			rdoExpiry.UseVisualStyleBackColor = true;
			dateControl2.CustomReportFieldName = "";
			dateControl2.CustomReportKey = "";
			dateControl2.CustomReportValueType = 1;
			dateControl2.FromDate = new System.DateTime(2015, 5, 1, 0, 0, 0, 0);
			dateControl2.Location = new System.Drawing.Point(12, 401);
			dateControl2.Name = "dateControl2";
			dateControl2.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl2.Size = new System.Drawing.Size(451, 66);
			dateControl2.TabIndex = 11;
			dateControl2.ToDate = new System.DateTime(2015, 5, 31, 23, 59, 59, 59);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(478, 521);
			base.Controls.Add(dateControl2);
			base.Controls.Add(rdoExpiry);
			base.Controls.Add(rdoProduction);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "InventoryTransactionsLotwiseReport";
			Text = "Inventory Ledger (Lotwise)";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
