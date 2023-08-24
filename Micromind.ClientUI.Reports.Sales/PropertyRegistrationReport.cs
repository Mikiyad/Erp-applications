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
	public class PropertyRegistrationReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private string type = "";

		private IContainer components;

		private Button buttonOK;

		private UltraGroupBox ultraGroupBox1;

		private Button buttonClose;

		private DateControl dateControl1;

		private PropertySelector propertySelector;

		private UltraGroupBox ultraGroupBox2;

		private UltraGroupBox ultraGroupBox3;

		private TenantSelector tenantSelector1;

		private UltraGroupBox ultraGroupBox4;

		private PropertyAgentSelector propertyAgentSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsPropertyRental;

		public int ScreenID => 7034;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public string TypeID
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		public PropertyRegistrationReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				if (type == "1")
				{
					Text = "Property Registration";
				}
				else if (type == "2")
				{
					Text = "Property Renewal";
				}
				else if (type == "3")
				{
					Text = "Property Cancellation";
				}
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
				DataSet data = Factory.PropertySystem.GetPropertyRentDetailsReport(dateControl1.FromDate, dateControl1.ToDate, tenantSelector1.FromCustomer, tenantSelector1.ToCustomer, tenantSelector1.FromClass, tenantSelector1.ToClass, tenantSelector1.FromGroup, tenantSelector1.ToGroup, propertySelector.FromUnit, propertySelector.ToUnit, propertySelector.FromProperty, propertySelector.ToProperty, type, tenantSelector1.MultipleCustomers, propertyAgentSelector.FromAgent, propertyAgentSelector.ToAgent);
				if (type == "1")
				{
					xtraReport = reportHelper.GetReport("Property Registration");
				}
				else if (type == "2")
				{
					xtraReport = reportHelper.GetReport("Property Renewal");
				}
				else if (type == "3")
				{
					xtraReport = reportHelper.GetReport("Property Cancellation");
				}
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Property Reports.repx'");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Sales.PropertyRegistrationReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			propertySelector = new Micromind.DataControls.PropertySelector();
			buttonClose = new System.Windows.Forms.Button();
			dateControl1 = new Micromind.DataControls.DateControl();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			tenantSelector1 = new Micromind.DataControls.TenantSelector();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			propertyAgentSelector = new Micromind.DataControls.PropertyAgentSelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(278, 446);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(ultraGroupBox2);
			ultraGroupBox1.Controls.Add(propertySelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 100);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Property";
			ultraGroupBox2.Location = new System.Drawing.Point(6, 106);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(464, 110);
			ultraGroupBox2.TabIndex = 6;
			ultraGroupBox2.Text = "ultraGroupBox2";
			propertySelector.BackColor = System.Drawing.Color.Transparent;
			propertySelector.CustomReportFieldName = "";
			propertySelector.CustomReportKey = "";
			propertySelector.CustomReportValueType = 1;
			propertySelector.Location = new System.Drawing.Point(6, 19);
			propertySelector.Name = "propertySelector";
			propertySelector.Size = new System.Drawing.Size(464, 81);
			propertySelector.TabIndex = 6;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(388, 446);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2016, 3, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(19, 375);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(463, 55);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2016, 3, 2, 23, 59, 59, 59);
			ultraGroupBox3.Controls.Add(tenantSelector1);
			ultraGroupBox3.Location = new System.Drawing.Point(12, 118);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(474, 152);
			ultraGroupBox3.TabIndex = 6;
			ultraGroupBox3.Text = "Tenant";
			tenantSelector1.BackColor = System.Drawing.Color.Transparent;
			tenantSelector1.CustomReportFieldName = "";
			tenantSelector1.CustomReportKey = "";
			tenantSelector1.CustomReportValueType = 1;
			tenantSelector1.Location = new System.Drawing.Point(7, 19);
			tenantSelector1.Name = "tenantSelector1";
			tenantSelector1.Size = new System.Drawing.Size(463, 123);
			tenantSelector1.TabIndex = 7;
			ultraGroupBox4.Controls.Add(propertyAgentSelector);
			ultraGroupBox4.Location = new System.Drawing.Point(14, 276);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(474, 83);
			ultraGroupBox4.TabIndex = 7;
			ultraGroupBox4.Text = "Agent";
			propertyAgentSelector.BackColor = System.Drawing.Color.Transparent;
			propertyAgentSelector.Location = new System.Drawing.Point(16, 19);
			propertyAgentSelector.Name = "propertyAgentSelector";
			propertyAgentSelector.Size = new System.Drawing.Size(433, 56);
			propertyAgentSelector.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(497, 478);
			base.Controls.Add(ultraGroupBox4);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "PropertyRegistrationReport";
			Text = "Property Registration Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).EndInit();
			ultraGroupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
