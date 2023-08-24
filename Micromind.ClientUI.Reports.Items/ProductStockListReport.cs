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
	public class ProductStockListReport : Form, IForm
	{
		public bool IsValuation;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private ProductSelector productSelector;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowInactive;

		private Button buttonClose;

		private CheckBox checkBoxShowZero;

		private LocationSelector locationSelector;

		private RadioButton radioButtonLocationWise;

		private RadioButton radioButtonItemWise;

		private MMSDateTimePicker dateTimePickerAsOfDate;

		private Line line1;

		private RadioButton radioButtonCurrent;

		private RadioButton radioButtonAsOf;

		private Panel panel1;

		private RadioButton radioButtonCategoryWise;

		private RadioButton radioButtonClassWise;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7026;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public ProductStockListReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
			dateTimePickerAsOfDate.Value = DateTime.Today;
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
				DataSet data = null;
				ReportHelper reportHelper = null;
				XtraReport xtraReport = null;
				string text = "";
				DateTime valueTo = dateTimePickerAsOfDate.ValueTo;
				if (radioButtonItemWise.Checked)
				{
					data = Factory.ProductSystem.GetProductStockListLocationWiseReport(productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, locationSelector.FromLocation, locationSelector.ToLocation, radioButtonAsOf.Checked, valueTo, checkBoxShowZero.Checked, checkBoxShowInactive.Checked, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
					{
						foreach (DataRow row in data.Tables["Quantity"].Rows)
						{
							row["AverageCost"] = 0;
							row["Value"] = 0;
						}
					}
					reportHelper = new ReportHelper();
					string reportFilter = "";
					reportHelper.AddGeneralReportData(ref data, reportFilter);
					text = "Stock List Item Wise";
					if (IsValuation)
					{
						text = "Inventory Valuation Item Wise";
					}
					xtraReport = reportHelper.GetReport(text);
				}
				else if (radioButtonLocationWise.Checked)
				{
					data = Factory.ProductSystem.GetProductStockListLocationWiseReport(productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, locationSelector.FromLocation, locationSelector.ToLocation, radioButtonAsOf.Checked, valueTo, checkBoxShowZero.Checked, checkBoxShowInactive.Checked, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
					{
						foreach (DataRow row2 in data.Tables["Quantity"].Rows)
						{
							row2["AverageCost"] = 0;
							row2["Value"] = 0;
						}
					}
					text = "Stock List Location Wise";
					if (IsValuation)
					{
						text = "Inventory Valuation Location Wise";
					}
					reportHelper = new ReportHelper();
					string reportFilter2 = "As of Date: " + (radioButtonAsOf.Checked ? valueTo.ToShortDateString() : DateTime.Now.ToShortDateString());
					reportHelper.AddGeneralReportData(ref data, reportFilter2);
					xtraReport = reportHelper.GetReport(text);
				}
				else if (radioButtonCategoryWise.Checked)
				{
					data = Factory.ProductSystem.GetProductStockListCategoryWiseReport(productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, locationSelector.FromLocation, locationSelector.ToLocation, radioButtonAsOf.Checked, valueTo, checkBoxShowZero.Checked, checkBoxShowInactive.Checked);
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
					{
						foreach (DataRow row3 in data.Tables["Quantity"].Rows)
						{
							row3["AverageCost"] = 0;
							row3["Value"] = 0;
						}
					}
					text = "Stock List Category Wise";
					if (IsValuation)
					{
						text = "Inventory Valuation Category Wise";
					}
					reportHelper = new ReportHelper();
					string reportFilter3 = "As of Date: " + (radioButtonAsOf.Checked ? valueTo.ToShortDateString() : DateTime.Now.ToShortDateString());
					reportHelper.AddGeneralReportData(ref data, reportFilter3);
					xtraReport = reportHelper.GetReport(text);
				}
				else if (radioButtonClassWise.Checked)
				{
					data = Factory.ProductSystem.GetProductStockListClassWiseReport(productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, locationSelector.FromLocation, locationSelector.ToLocation, radioButtonAsOf.Checked, valueTo, checkBoxShowZero.Checked, checkBoxShowInactive.Checked);
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
					{
						foreach (DataRow row4 in data.Tables["Quantity"].Rows)
						{
							row4["AverageCost"] = 0;
							row4["Value"] = 0;
						}
					}
					text = "Stock List Class Wise";
					if (IsValuation)
					{
						text = "Inventory Valuation Class Wise";
					}
					reportHelper = new ReportHelper();
					string reportFilter4 = "As of Date: " + (radioButtonAsOf.Checked ? valueTo.ToShortDateString() : DateTime.Now.ToShortDateString());
					reportHelper.AddGeneralReportData(ref data, reportFilter4);
					xtraReport = reportHelper.GetReport(text);
				}
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'" + text + ".repx'", "Please make sure you have access to reports path and the files are not corrupted.");
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

		private void radioButtonLocationWise_CheckedChanged(object sender, EventArgs e)
		{
			locationSelector.Enabled = radioButtonLocationWise.Checked;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.ProductStockListReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			checkBoxShowInactive = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			checkBoxShowZero = new System.Windows.Forms.CheckBox();
			radioButtonLocationWise = new System.Windows.Forms.RadioButton();
			radioButtonItemWise = new System.Windows.Forms.RadioButton();
			radioButtonCurrent = new System.Windows.Forms.RadioButton();
			radioButtonAsOf = new System.Windows.Forms.RadioButton();
			line1 = new Micromind.UISupport.Line();
			dateTimePickerAsOfDate = new Micromind.UISupport.MMSDateTimePicker(components);
			locationSelector = new Micromind.DataControls.LocationSelector();
			panel1 = new System.Windows.Forms.Panel();
			radioButtonClassWise = new System.Windows.Forms.RadioButton();
			radioButtonCategoryWise = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(289, 427);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 7;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 209);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Products";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(6, 19);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(403, 181);
			productSelector.TabIndex = 2;
			checkBoxShowInactive.AutoSize = true;
			checkBoxShowInactive.Location = new System.Drawing.Point(18, 383);
			checkBoxShowInactive.Name = "checkBoxShowInactive";
			checkBoxShowInactive.Size = new System.Drawing.Size(137, 17);
			checkBoxShowInactive.TabIndex = 6;
			checkBoxShowInactive.Text = "Show inactive products";
			checkBoxShowInactive.UseVisualStyleBackColor = true;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(393, 427);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 8;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			checkBoxShowZero.AutoSize = true;
			checkBoxShowZero.Location = new System.Drawing.Point(18, 364);
			checkBoxShowZero.Name = "checkBoxShowZero";
			checkBoxShowZero.Size = new System.Drawing.Size(116, 17);
			checkBoxShowZero.TabIndex = 5;
			checkBoxShowZero.Text = "Show zero quantity";
			checkBoxShowZero.UseVisualStyleBackColor = true;
			radioButtonLocationWise.AutoSize = true;
			radioButtonLocationWise.Checked = true;
			radioButtonLocationWise.Location = new System.Drawing.Point(11, 5);
			radioButtonLocationWise.Name = "radioButtonLocationWise";
			radioButtonLocationWise.Size = new System.Drawing.Size(93, 17);
			radioButtonLocationWise.TabIndex = 0;
			radioButtonLocationWise.TabStop = true;
			radioButtonLocationWise.Text = "Location Wise";
			radioButtonLocationWise.UseVisualStyleBackColor = true;
			radioButtonLocationWise.CheckedChanged += new System.EventHandler(radioButtonLocationWise_CheckedChanged);
			radioButtonItemWise.AutoSize = true;
			radioButtonItemWise.Location = new System.Drawing.Point(132, 5);
			radioButtonItemWise.Name = "radioButtonItemWise";
			radioButtonItemWise.Size = new System.Drawing.Size(72, 17);
			radioButtonItemWise.TabIndex = 1;
			radioButtonItemWise.Text = "Item Wise";
			radioButtonItemWise.UseVisualStyleBackColor = true;
			radioButtonCurrent.AutoSize = true;
			radioButtonCurrent.Checked = true;
			radioButtonCurrent.Location = new System.Drawing.Point(18, 339);
			radioButtonCurrent.Name = "radioButtonCurrent";
			radioButtonCurrent.Size = new System.Drawing.Size(85, 17);
			radioButtonCurrent.TabIndex = 2;
			radioButtonCurrent.TabStop = true;
			radioButtonCurrent.Text = "Current Date";
			radioButtonCurrent.UseVisualStyleBackColor = true;
			radioButtonAsOf.AutoSize = true;
			radioButtonAsOf.Location = new System.Drawing.Point(114, 339);
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
			line1.Location = new System.Drawing.Point(-8, 421);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(520, 1);
			line1.TabIndex = 13;
			line1.TabStop = false;
			dateTimePickerAsOfDate.Enabled = false;
			dateTimePickerAsOfDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerAsOfDate.Location = new System.Drawing.Point(172, 339);
			dateTimePickerAsOfDate.Name = "dateTimePickerAsOfDate";
			dateTimePickerAsOfDate.Size = new System.Drawing.Size(143, 20);
			dateTimePickerAsOfDate.TabIndex = 4;
			dateTimePickerAsOfDate.Value = new System.DateTime(2014, 4, 8, 14, 15, 58, 16);
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(32, 24);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(424, 53);
			locationSelector.TabIndex = 2;
			panel1.Controls.Add(radioButtonClassWise);
			panel1.Controls.Add(radioButtonCategoryWise);
			panel1.Controls.Add(locationSelector);
			panel1.Controls.Add(radioButtonItemWise);
			panel1.Controls.Add(radioButtonLocationWise);
			panel1.Location = new System.Drawing.Point(12, 234);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(472, 90);
			panel1.TabIndex = 1;
			radioButtonClassWise.AutoSize = true;
			radioButtonClassWise.Location = new System.Drawing.Point(331, 4);
			radioButtonClassWise.Name = "radioButtonClassWise";
			radioButtonClassWise.Size = new System.Drawing.Size(77, 17);
			radioButtonClassWise.TabIndex = 4;
			radioButtonClassWise.Text = "Class Wise";
			radioButtonClassWise.UseVisualStyleBackColor = true;
			radioButtonCategoryWise.AutoSize = true;
			radioButtonCategoryWise.Location = new System.Drawing.Point(231, 4);
			radioButtonCategoryWise.Name = "radioButtonCategoryWise";
			radioButtonCategoryWise.Size = new System.Drawing.Size(94, 17);
			radioButtonCategoryWise.TabIndex = 3;
			radioButtonCategoryWise.Text = "Category Wise";
			radioButtonCategoryWise.UseVisualStyleBackColor = true;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(500, 459);
			base.Controls.Add(panel1);
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
			base.Name = "ProductStockListReport";
			Text = "Item List Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
