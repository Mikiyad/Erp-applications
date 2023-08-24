using DevExpress.Utils;
using DevExpress.XtraEditors;
using Infragistics.Win.UltraWinDataSource;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class CheckPriceForm : XtraForm
	{
		private bool canAccessCost = true;

		private IContainer components;

		private UltraDataSource ultraDataSource1;

		private SimpleButton buttonCancel;

		private Label labelItem;

		private TextEdit textBoxPrice;

		private Label label3;

		private Label labelVoucherNumber;

		private TextBox textBoxScan;

		private SimpleButton simpleButton1;

		private TextEdit textBoxCost;

		private Label label1;

		public string SelectedItem
		{
			get
			{
				return textBoxScan.Text;
			}
			set
			{
				textBoxScan.Text = value;
			}
		}

		public string Total
		{
			set
			{
				textBoxPrice.Text = value;
			}
		}

		public CheckPriceForm()
		{
			InitializeComponent();
		}

		private void XtraForm1_Load(object sender, EventArgs e)
		{
			LoadData();
			base.Activated += CheckPriceForm_Activated;
		}

		private void CheckPriceForm_Activated(object sender, EventArgs e)
		{
			textBoxScan.Focus();
		}

		private void LoadData()
		{
			if (textBoxScan.Text == "")
			{
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
			}
			DataRow dataRow = null;
			DataSet dataSet = Factory.ProductSystem.POSGetProductData(SelectedItem.Trim());
			if (dataSet == null)
			{
				return;
			}
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				dataRow = dataSet.Tables[0].Rows[0];
			}
			if (dataRow != null)
			{

				var p=dataRow["Price"].ToString();
				decimal num = 0.0m;

				if(!string.IsNullOrEmpty(p))
					num = decimal.Parse(dataRow["Price"].ToString());


				
				decimal num2 = decimal.Parse(dataRow["Cost"].ToString());
				textBoxPrice.Text = num.ToString(Format.UnitPriceFormat);
				if (canAccessCost)
				{
					textBoxCost.Text = num2.ToString(Format.UnitPriceFormat);
				}
				else
				{
					textBoxCost.Text = " No Permission";
				}
				labelItem.Text = dataRow["Code"].ToString() + "\n" + dataRow["Description"].ToString();
			}
			else
			{
				ErrorHelper.InformationMessage("Item Not Found.");
			}
			textBoxScan.Clear();
			textBoxScan.Focus();
			base.DialogResult = DialogResult.None;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			if (textBoxScan.Text == "")
			{
				return;
			}
			DataRow dataRow = null;
			if (Factory.ProductSystem.POSGetProductData(SelectedItem.Trim()) != null)
			{
				if (dataRow != null)
				{
					decimal num = decimal.Parse(dataRow["Price"].ToString());
					decimal num2 = decimal.Parse(dataRow["Cost"].ToString());
					textBoxPrice.Text = num.ToString(Format.UnitPriceFormat);
					textBoxCost.Text = num2.ToString(Format.UnitPriceFormat);
					labelItem.Text = dataRow["Code"].ToString() + "\n" + dataRow["Description"].ToString();
				}
				else
				{
					ErrorHelper.InformationMessage("Item Not Found.");
				}
				textBoxScan.Clear();
				textBoxScan.Focus();
				base.DialogResult = DialogResult.None;
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
			components = new System.ComponentModel.Container();
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 0");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 1");
			ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource(components);
			buttonCancel = new DevExpress.XtraEditors.SimpleButton();
			labelItem = new System.Windows.Forms.Label();
			textBoxPrice = new DevExpress.XtraEditors.TextEdit();
			label3 = new System.Windows.Forms.Label();
			labelVoucherNumber = new System.Windows.Forms.Label();
			textBoxScan = new System.Windows.Forms.TextBox();
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			textBoxCost = new DevExpress.XtraEditors.TextEdit();
			label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxPrice.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxCost.Properties).BeginInit();
			SuspendLayout();
			ultraDataSource1.Band.Columns.AddRange(new object[2]
			{
				ultraDataColumn,
				ultraDataColumn2
			});
			buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCancel.Appearance.Options.UseFont = true;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(342, 274);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(97, 40);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "Close";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			labelItem.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelItem.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelItem.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			labelItem.Location = new System.Drawing.Point(13, 92);
			labelItem.Name = "labelItem";
			labelItem.Size = new System.Drawing.Size(426, 87);
			labelItem.TabIndex = 37;
			textBoxPrice.EditValue = "0.00";
			textBoxPrice.Location = new System.Drawing.Point(87, 187);
			textBoxPrice.Name = "textBoxPrice";
			textBoxPrice.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			textBoxPrice.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			textBoxPrice.Properties.Appearance.Options.UseFont = true;
			textBoxPrice.Properties.Appearance.Options.UseForeColor = true;
			textBoxPrice.Properties.Appearance.Options.UseTextOptions = true;
			textBoxPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			textBoxPrice.Properties.AutoHeight = false;
			textBoxPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			textBoxPrice.Properties.ReadOnly = true;
			textBoxPrice.Size = new System.Drawing.Size(352, 33);
			textBoxPrice.TabIndex = 41;
			textBoxPrice.TabStop = false;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label3.Location = new System.Drawing.Point(13, 192);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(83, 22);
			label3.TabIndex = 35;
			label3.Text = "Price:";
			labelVoucherNumber.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelVoucherNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoucherNumber.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			labelVoucherNumber.Location = new System.Drawing.Point(12, 11);
			labelVoucherNumber.Name = "labelVoucherNumber";
			labelVoucherNumber.Size = new System.Drawing.Size(244, 21);
			labelVoucherNumber.TabIndex = 36;
			labelVoucherNumber.Text = "Scan the item here:";
			textBoxScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxScan.Location = new System.Drawing.Point(16, 35);
			textBoxScan.Multiline = true;
			textBoxScan.Name = "textBoxScan";
			textBoxScan.Size = new System.Drawing.Size(423, 40);
			textBoxScan.TabIndex = 43;
			simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton1.Appearance.Options.UseFont = true;
			simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			simpleButton1.Location = new System.Drawing.Point(395, 35);
			simpleButton1.Name = "simpleButton1";
			simpleButton1.Size = new System.Drawing.Size(35, 40);
			simpleButton1.TabIndex = 44;
			simpleButton1.Click += new System.EventHandler(simpleButton1_Click);
			textBoxCost.EditValue = "0.00";
			textBoxCost.Location = new System.Drawing.Point(87, 225);
			textBoxCost.Name = "textBoxCost";
			textBoxCost.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			textBoxCost.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			textBoxCost.Properties.Appearance.Options.UseFont = true;
			textBoxCost.Properties.Appearance.Options.UseForeColor = true;
			textBoxCost.Properties.Appearance.Options.UseTextOptions = true;
			textBoxCost.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			textBoxCost.Properties.AutoHeight = false;
			textBoxCost.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			textBoxCost.Properties.ReadOnly = true;
			textBoxCost.Size = new System.Drawing.Size(352, 33);
			textBoxCost.TabIndex = 46;
			textBoxCost.TabStop = false;
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label1.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label1.Location = new System.Drawing.Point(13, 230);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(83, 22);
			label1.TabIndex = 45;
			label1.Text = "Cost :";
			base.AcceptButton = simpleButton1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(451, 326);
			base.Controls.Add(textBoxCost);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxScan);
			base.Controls.Add(labelItem);
			base.Controls.Add(textBoxPrice);
			base.Controls.Add(label3);
			base.Controls.Add(labelVoucherNumber);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(simpleButton1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CheckPriceForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Check Price";
			base.Load += new System.EventHandler(XtraForm1_Load);
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxPrice.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxCost.Properties).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
