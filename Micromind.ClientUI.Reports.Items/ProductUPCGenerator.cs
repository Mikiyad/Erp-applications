using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraReports.UI;
using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Items
{
	public class ProductUPCGenerator : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private ProductSelector productSelector;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowInactive;

		private Button buttonClose;

		private CheckBox checkBoxShowZero;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsInventory;

		public int ScreenID => 7024;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public ProductUPCGenerator()
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

		private void report_BeforePrint(object sender, PrintEventArgs e)
		{
			CreateEAN13BarCode("123456");
			((XtraReport)sender).Bands.Add(new DetailBand());
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DataSet data = Factory.ProductSystem.GetProductListReport(productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, checkBoxShowZero.Checked, checkBoxShowInactive.Checked, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
			if (data.Tables.Count > 0)
			{
				data.Tables["Products"].Columns.Add("ItemTypeName");
				foreach (DataRow row in data.Tables["Products"].Rows)
				{
					if (row["ItemType"] != DBNull.Value)
					{
						int num = int.Parse(row["ItemType"].ToString());
						string value = "";
						switch (num)
						{
						case 1:
							value = "Inventory";
							break;
						case 2:
							value = "Non Inventory";
							break;
						case 3:
							value = "Service";
							break;
						case 4:
							value = "Discount";
							break;
						case 5:
							value = "Consignment Item";
							break;
						case 6:
							value = "Matrix";
							break;
						case 7:
							value = "Assembly";
							break;
						case 8:
							value = "ProjectFee";
							break;
						case 9:
							value = "Inventory3PL";
							break;
						}
						row["ItemTypeName"] = value;
					}
				}
			}
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			XtraReport report = reportHelper.GetReport("Item List-BarCode");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'Product List.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private DataSet GetBarCodeData(DataSet data)
		{
			DataSet result = new DataSet();
			if (data.Tables.Count > 0)
			{
				data.Tables["Products"].Columns.Add("UPCCodeGenerated");
				foreach (DataRow row in data.Tables["Products"].Rows)
				{
					string barCodeText = row["UPC"].ToString();
					XRBarCode xRBarCode2 = (XRBarCode)(row["UPCCodeGenerated"] = CreateEAN13BarCode(barCodeText));
				}
				result = data;
			}
			return result;
		}

		public XRBarCode CreateEAN13BarCode(string BarCodeText)
		{
			return new XRBarCode
			{
				Symbology = new EAN13Generator(),
				Text = BarCodeText,
				Width = 275,
				Height = 200
			};
		}

		private Bitmap CreateBarCode(string data)
		{
			Bitmap bitmap = new Bitmap(1, 1);
			Font font = new Font("Free 3 of 9", 60f, FontStyle.Regular, GraphicsUnit.Point);
			Bitmap bitmap2 = new Bitmap(bitmap, Graphics.FromImage(bitmap).MeasureString(data, font).ToSize());
			Graphics graphics = Graphics.FromImage(bitmap2);
			graphics.Clear(Color.White);
			graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
			graphics.DrawString(data, font, new SolidBrush(Color.Black), 0f, 0f);
			graphics.Flush();
			font.Dispose();
			graphics.Dispose();
			return bitmap2;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Items.ProductUPCGenerator));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			checkBoxShowInactive = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			checkBoxShowZero = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(286, 311);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 3;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 217);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Products";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(6, 19);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(403, 193);
			productSelector.TabIndex = 0;
			checkBoxShowInactive.AutoSize = true;
			checkBoxShowInactive.Location = new System.Drawing.Point(18, 269);
			checkBoxShowInactive.Name = "checkBoxShowInactive";
			checkBoxShowInactive.Size = new System.Drawing.Size(137, 17);
			checkBoxShowInactive.TabIndex = 2;
			checkBoxShowInactive.Text = "Show inactive products";
			checkBoxShowInactive.UseVisualStyleBackColor = true;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(390, 311);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 4;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			checkBoxShowZero.AutoSize = true;
			checkBoxShowZero.Location = new System.Drawing.Point(18, 246);
			checkBoxShowZero.Name = "checkBoxShowZero";
			checkBoxShowZero.Size = new System.Drawing.Size(151, 17);
			checkBoxShowZero.TabIndex = 1;
			checkBoxShowZero.Text = "Show zero stock inventory";
			checkBoxShowZero.UseVisualStyleBackColor = true;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(497, 343);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowZero);
			base.Controls.Add(checkBoxShowInactive);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "ProductUPCGenerator";
			Text = "Item List Report";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
