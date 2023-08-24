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
	public class InventoryTransactionsReport : Form, IForm
	{
		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private ScreenAccessRight screenRight;

		public DataSet getdata;

		public string istype = "";

		private IContainer components;

		private Button buttonOK;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private ProductSelector productSelector;

		private DateControl dateControl1;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox2;

		private CheckBox checkBoxitemwithTansactions;

		private CheckBox checkBoxShowInactiveItems;

		private GroupBox groupBoxJob;

		private JobSelector jobSelector;

		private CheckBox checkBoxExcludeStockTransfer;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7022;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public DataSet GetData
		{
			get
			{
				return getdata;
			}
			set
			{
				getdata = value;
			}
		}

		public string IsType
		{
			get
			{
				return istype;
			}
			set
			{
				istype = value;
			}
		}

		public InventoryTransactionsReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!useJobCosting)
				{
					groupBoxJob.Visible = useJobCosting;
					jobSelector.Visible = useJobCosting;
				}
				if (istype == "A")
				{
					dateControl1.Visible = false;
				}
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
			if (istype == "A")
			{
				dateControl1.FromDate = new DateTime(2012, 1, 28);
				dateControl1.ToDate = new DateTime(2018, 12, 31);
			}
			DataSet data = Factory.ProductSystem.GetInventoryTransactionReport(dateControl1.FromDate, dateControl1.ToDate, locationSelector.FromLocation, locationSelector.ToLocation, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, jobSelector.FromJob, jobSelector.ToJob, checkBoxitemwithTansactions.Checked, checkBoxShowInactiveItems.Checked, checkBoxExcludeStockTransfer.Checked, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
			if (istype == "A")
			{
				GetData = data;
				Close();
				return;
			}
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
			XtraReport report = reportHelper.GetReport("Inventory Ledger");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Inventory Ledger.repx'");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.InventoryTransactionsReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			buttonClose = new System.Windows.Forms.Button();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			checkBoxitemwithTansactions = new System.Windows.Forms.CheckBox();
			checkBoxShowInactiveItems = new System.Windows.Forms.CheckBox();
			dateControl1 = new Micromind.DataControls.DateControl();
			groupBoxJob = new System.Windows.Forms.GroupBox();
			jobSelector = new Micromind.DataControls.JobSelector();
			checkBoxExcludeStockTransfer = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			groupBoxJob.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(262, 540);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(102, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(451, 217);
			ultraGroupBox1.TabIndex = 3;
			ultraGroupBox1.Text = "Items";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(5, 18);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(446, 193);
			productSelector.TabIndex = 0;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(366, 540);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(102, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 231);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(451, 77);
			ultraGroupBox2.TabIndex = 8;
			ultraGroupBox2.Text = "Location";
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 18);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(424, 59);
			locationSelector.TabIndex = 7;
			checkBoxitemwithTansactions.AutoSize = true;
			checkBoxitemwithTansactions.Location = new System.Drawing.Point(16, 476);
			checkBoxitemwithTansactions.Name = "checkBoxitemwithTansactions";
			checkBoxitemwithTansactions.Size = new System.Drawing.Size(157, 17);
			checkBoxitemwithTansactions.TabIndex = 9;
			checkBoxitemwithTansactions.Text = "Show Item with Transaction";
			checkBoxitemwithTansactions.UseVisualStyleBackColor = true;
			checkBoxShowInactiveItems.AutoSize = true;
			checkBoxShowInactiveItems.Location = new System.Drawing.Point(16, 499);
			checkBoxShowInactiveItems.Name = "checkBoxShowInactiveItems";
			checkBoxShowInactiveItems.Size = new System.Drawing.Size(122, 17);
			checkBoxShowInactiveItems.TabIndex = 10;
			checkBoxShowInactiveItems.Text = "Show Inactive Items";
			checkBoxShowInactiveItems.UseVisualStyleBackColor = true;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 6, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(10, 392);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(453, 54);
			dateControl1.TabIndex = 6;
			dateControl1.ToDate = new System.DateTime(2017, 6, 13, 23, 59, 59, 59);
			groupBoxJob.Controls.Add(jobSelector);
			groupBoxJob.Location = new System.Drawing.Point(10, 314);
			groupBoxJob.Name = "groupBoxJob";
			groupBoxJob.Size = new System.Drawing.Size(458, 69);
			groupBoxJob.TabIndex = 160;
			groupBoxJob.TabStop = false;
			groupBoxJob.Text = "Jobs";
			jobSelector.BackColor = System.Drawing.Color.Transparent;
			jobSelector.CustomReportFieldName = "";
			jobSelector.CustomReportKey = "";
			jobSelector.CustomReportValueType = 1;
			jobSelector.Location = new System.Drawing.Point(6, 17);
			jobSelector.Name = "jobSelector";
			jobSelector.Size = new System.Drawing.Size(429, 46);
			jobSelector.TabIndex = 0;
			checkBoxExcludeStockTransfer.AutoSize = true;
			checkBoxExcludeStockTransfer.Location = new System.Drawing.Point(16, 452);
			checkBoxExcludeStockTransfer.Name = "checkBoxExcludeStockTransfer";
			checkBoxExcludeStockTransfer.Size = new System.Drawing.Size(153, 17);
			checkBoxExcludeStockTransfer.TabIndex = 161;
			checkBoxExcludeStockTransfer.Text = "Exclude Inventory Transfer";
			checkBoxExcludeStockTransfer.UseVisualStyleBackColor = true;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(478, 570);
			base.Controls.Add(checkBoxExcludeStockTransfer);
			base.Controls.Add(groupBoxJob);
			base.Controls.Add(checkBoxShowInactiveItems);
			base.Controls.Add(checkBoxitemwithTansactions);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "InventoryTransactionsReport";
			Text = "Inventory Ledger Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			groupBoxJob.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
