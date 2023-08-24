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
	public class ProductCatalogReport : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private ProductSelector productSelector;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowInactive;

		private Button buttonClose;

		private CheckBox checkBoxShowZeroQuantity;

		private ComboBox comboBoxReportType;

		private Label label1;

		private ProductParentSelector productParentSelector;

		private Line line1;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7023;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public ProductCatalogReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
			comboBoxReportType.SelectedIndex = 0;
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
				DataSet data = (comboBoxReportType.SelectedIndex != 0) ? Factory.ProductParentSystem.GetProductParentCatalogReport(productParentSelector.FromItem, productParentSelector.ToItem, productParentSelector.FromCategory, productParentSelector.ToCategory, checkBoxShowInactive.Checked, checkBoxShowZeroQuantity.Checked, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin) : Factory.ProductSystem.GetProductCatalogReport(productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, checkBoxShowInactive.Checked, checkBoxShowZeroQuantity.Checked, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
				if (data.Tables.Count > 0)
				{
					data.Tables["Product"].Columns.Add("ItemTypeName");
					foreach (DataRow row in data.Tables["Product"].Rows)
					{
						if (row["ItemType"] != DBNull.Value)
						{
							int num = int.Parse(row["ItemType"].ToString());
							string text = "";
							switch (num)
							{
							case 1:
								text = "Inventory";
								break;
							case 2:
								text = "Non Inventory";
								break;
							default:
								text = "Service";
								break;
							}
							row["ItemTypeName"] = text;
						}
					}
				}
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "";
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				XtraReport report = reportHelper.GetReport("Item Catalog");
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'Item Catalog.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
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

		private void comboBoxReportType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxReportType.SelectedIndex == 1)
			{
				CheckBox checkBox = checkBoxShowZeroQuantity;
				bool @checked = checkBoxShowZeroQuantity.Enabled = false;
				checkBox.Checked = @checked;
				productParentSelector.Visible = true;
				productSelector.Visible = false;
			}
			else
			{
				checkBoxShowZeroQuantity.Enabled = true;
				productParentSelector.Visible = false;
				productSelector.Visible = true;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.ProductCatalogReport));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			productParentSelector = new Micromind.DataControls.ProductParentSelector();
			checkBoxShowInactive = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			checkBoxShowZeroQuantity = new System.Windows.Forms.CheckBox();
			comboBoxReportType = new System.Windows.Forms.ComboBox();
			label1 = new System.Windows.Forms.Label();
			line1 = new Micromind.UISupport.Line();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(249, 314);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 3;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Controls.Add(productParentSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(11, 47);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(440, 200);
			ultraGroupBox1.TabIndex = 0;
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(6, 9);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(403, 184);
			productSelector.TabIndex = 2;
			productParentSelector.BackColor = System.Drawing.Color.Transparent;
			productParentSelector.Location = new System.Drawing.Point(6, 9);
			productParentSelector.Name = "productParentSelector";
			productParentSelector.Size = new System.Drawing.Size(452, 97);
			productParentSelector.TabIndex = 7;
			checkBoxShowInactive.AutoSize = true;
			checkBoxShowInactive.Location = new System.Drawing.Point(23, 255);
			checkBoxShowInactive.Name = "checkBoxShowInactive";
			checkBoxShowInactive.Size = new System.Drawing.Size(137, 17);
			checkBoxShowInactive.TabIndex = 1;
			checkBoxShowInactive.Text = "Show inactive products";
			checkBoxShowInactive.UseVisualStyleBackColor = true;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(353, 314);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 4;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			checkBoxShowZeroQuantity.AutoSize = true;
			checkBoxShowZeroQuantity.Location = new System.Drawing.Point(166, 255);
			checkBoxShowZeroQuantity.Name = "checkBoxShowZeroQuantity";
			checkBoxShowZeroQuantity.Size = new System.Drawing.Size(166, 17);
			checkBoxShowZeroQuantity.TabIndex = 2;
			checkBoxShowZeroQuantity.Text = "Show Items with zero quantity";
			checkBoxShowZeroQuantity.UseVisualStyleBackColor = true;
			comboBoxReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxReportType.FormattingEnabled = true;
			comboBoxReportType.Items.AddRange(new object[2]
			{
				"Product",
				"Product Parent (Matrix Item)"
			});
			comboBoxReportType.Location = new System.Drawing.Point(94, 12);
			comboBoxReportType.Name = "comboBoxReportType";
			comboBoxReportType.Size = new System.Drawing.Size(125, 21);
			comboBoxReportType.TabIndex = 5;
			comboBoxReportType.SelectedIndexChanged += new System.EventHandler(comboBoxReportType_SelectedIndexChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(15, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(72, 13);
			label1.TabIndex = 6;
			label1.Text = "Catalogue by:";
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-24, 306);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(496, 1);
			line1.TabIndex = 7;
			line1.TabStop = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(460, 343);
			base.Controls.Add(line1);
			base.Controls.Add(label1);
			base.Controls.Add(comboBoxReportType);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowZeroQuantity);
			base.Controls.Add(checkBoxShowInactive);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "ProductCatalogReport";
			Text = "Products Catalogue";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
