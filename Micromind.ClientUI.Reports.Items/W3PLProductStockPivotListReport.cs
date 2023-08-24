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

namespace Micromind.ClientUI.Reports.Items
{
	public class W3PLProductStockPivotListReport : Form, IForm
	{
		public bool IsValuation;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowInactive;

		private Button buttonClose;

		private CheckBox checkBoxShowZero;

		private MMSDateTimePicker dateTimePickerAsOfDate;

		private Line line1;

		private RadioButton radioButtonCurrent;

		private RadioButton radioButtonAsOf;

		private LocationSelector locationSelector;

		private UltraGroupBox ultraGroupBox2;

		private W3PLProductSelector productSelector;

		private UltraGroupBox ultraGroupBox3;

		private CustomerSelector customerSelector;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7026;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public W3PLProductStockPivotListReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				if (IsValuation)
				{
					Text = "Inventory Valuation Report";
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
				string text = "";
				_ = dateTimePickerAsOfDate.ValueTo;
				DataSet data = Factory.ProductSystem.GetW3PLProductStockListLocationWiseReport(productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, locationSelector.FromLocation, locationSelector.ToLocation, radioButtonAsOf.Checked, dateTimePickerAsOfDate.ValueTo, checkBoxShowZero.Checked, checkBoxShowInactive.Checked, customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, customerSelector.MultipleCustomers);
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
				{
					foreach (DataRow row in data.Tables["Quantity"].Rows)
					{
						row["AverageCost"] = 0;
						row["Value"] = 0;
					}
				}
				text = "W3PLInventory Valuation Location Wise";
				if (IsValuation)
				{
					text = "W3PLInventory Valuation Location Wise";
				}
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "To: " + (radioButtonCurrent.Checked ? DateTime.Now.ToShortDateString() : dateTimePickerAsOfDate.Value.ToShortDateString());
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				XtraReport report = reportHelper.GetReport(text);
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'W3PLInventory Valuation Location Wise.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				}
				else
				{
					report.DataSource = data;
					reportHelper.ShowReport(report);
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

		private void radioButtonLocationWise_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonAsOf_CheckedChanged(object sender, EventArgs e)
		{
			dateTimePickerAsOfDate.Enabled = radioButtonAsOf.Checked;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.W3PLProductStockPivotListReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.W3PLProductSelector();
			checkBoxShowInactive = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			checkBoxShowZero = new System.Windows.Forms.CheckBox();
			radioButtonCurrent = new System.Windows.Forms.RadioButton();
			radioButtonAsOf = new System.Windows.Forms.RadioButton();
			line1 = new Micromind.UISupport.Line();
			dateTimePickerAsOfDate = new Micromind.UISupport.MMSDateTimePicker(components);
			locationSelector = new Micromind.DataControls.LocationSelector();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox3).BeginInit();
			ultraGroupBox3.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(289, 483);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 7;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 186);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 121);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Products";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(7, 18);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(414, 99);
			productSelector.TabIndex = 1;
			checkBoxShowInactive.AutoSize = true;
			checkBoxShowInactive.Location = new System.Drawing.Point(18, 455);
			checkBoxShowInactive.Name = "checkBoxShowInactive";
			checkBoxShowInactive.Size = new System.Drawing.Size(137, 17);
			checkBoxShowInactive.TabIndex = 6;
			checkBoxShowInactive.Text = "Show inactive products";
			checkBoxShowInactive.UseVisualStyleBackColor = true;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(393, 483);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 8;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			checkBoxShowZero.AutoSize = true;
			checkBoxShowZero.Location = new System.Drawing.Point(18, 437);
			checkBoxShowZero.Name = "checkBoxShowZero";
			checkBoxShowZero.Size = new System.Drawing.Size(116, 17);
			checkBoxShowZero.TabIndex = 5;
			checkBoxShowZero.Text = "Show zero quantity";
			checkBoxShowZero.UseVisualStyleBackColor = true;
			radioButtonCurrent.AutoSize = true;
			radioButtonCurrent.Checked = true;
			radioButtonCurrent.Location = new System.Drawing.Point(18, 410);
			radioButtonCurrent.Name = "radioButtonCurrent";
			radioButtonCurrent.Size = new System.Drawing.Size(85, 17);
			radioButtonCurrent.TabIndex = 2;
			radioButtonCurrent.TabStop = true;
			radioButtonCurrent.Text = "Current Date";
			radioButtonCurrent.UseVisualStyleBackColor = true;
			radioButtonAsOf.AutoSize = true;
			radioButtonAsOf.Location = new System.Drawing.Point(114, 410);
			radioButtonAsOf.Name = "radioButtonAsOf";
			radioButtonAsOf.Size = new System.Drawing.Size(54, 17);
			radioButtonAsOf.TabIndex = 3;
			radioButtonAsOf.Text = "As Of:";
			radioButtonAsOf.UseVisualStyleBackColor = true;
			radioButtonAsOf.CheckedChanged += new System.EventHandler(radioButtonAsOf_CheckedChanged);
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-8, 477);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(520, 1);
			line1.TabIndex = 13;
			line1.TabStop = false;
			dateTimePickerAsOfDate.Enabled = false;
			dateTimePickerAsOfDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerAsOfDate.Location = new System.Drawing.Point(172, 408);
			dateTimePickerAsOfDate.Name = "dateTimePickerAsOfDate";
			dateTimePickerAsOfDate.Size = new System.Drawing.Size(143, 20);
			dateTimePickerAsOfDate.TabIndex = 4;
			dateTimePickerAsOfDate.Value = new System.DateTime(2014, 4, 8, 14, 15, 58, 16);
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(424, 53);
			locationSelector.TabIndex = 2;
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 312);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(476, 84);
			ultraGroupBox2.TabIndex = 14;
			ultraGroupBox2.Text = "Locations";
			ultraGroupBox3.Controls.Add(customerSelector);
			ultraGroupBox3.Location = new System.Drawing.Point(15, -3);
			ultraGroupBox3.Name = "ultraGroupBox3";
			ultraGroupBox3.Size = new System.Drawing.Size(473, 183);
			ultraGroupBox3.TabIndex = 20;
			ultraGroupBox3.Text = "Customers";
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(6, 14);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(430, 163);
			customerSelector.TabIndex = 0;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(500, 515);
			base.Controls.Add(ultraGroupBox3);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(radioButtonAsOf);
			base.Controls.Add(radioButtonCurrent);
			base.Controls.Add(line1);
			base.Controls.Add(dateTimePickerAsOfDate);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowZero);
			base.Controls.Add(checkBoxShowInactive);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "W3PLProductStockPivotListReport";
			Text = "W3PL Item Stock List Report";
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
