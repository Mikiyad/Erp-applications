using DevExpress.XtraEditors;
using Infragistics.Win.UltraWinDataSource;
using Micromind.ClientLibraries;
using Micromind.DataControls.MMSDataGrid;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class CheckQuantityForm : XtraForm
	{
		private IContainer components;

		private UltraDataSource ultraDataSource1;

		private SimpleButton buttonCancel;

		private Label labelItem;

		private Label labelVoucherNumber;

		private TextBox textBoxScan;

		private SimpleButton simpleButton1;

		private POSGrid dataGridItems;

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

		public CheckQuantityForm()
		{
			InitializeComponent();
		}

		private void XtraForm1_Load(object sender, EventArgs e)
		{
			dataGridItems.SetupUI();
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
			DataRow dataRow = null;
			DataSet dataSet = Factory.ProductSystem.POSGetProductData(SelectedItem.Trim());
			if (dataSet != null)
			{
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					dataRow = dataSet.Tables[0].Rows[0];
				}
				if (dataRow != null)
				{
					decimal.Parse(dataRow["Price"].ToString());
					labelItem.Text = dataRow["Code"].ToString() + "\n" + dataRow["Description"].ToString();
					DataSet productAvailability = Factory.ProductSystem.GetProductAvailability(textBoxScan.Text, "");
					dataGridItems.DataGrid.DataSource = productAvailability;
				}
				else
				{
					ErrorHelper.InformationMessage("Item Not Found.");
				}
				textBoxScan.Clear();
				textBoxScan.Focus();
				dataGridItems.ApplyFormat();
			}
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
			LoadData();
			base.DialogResult = DialogResult.None;
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
			labelVoucherNumber = new System.Windows.Forms.Label();
			textBoxScan = new System.Windows.Forms.TextBox();
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			dataGridItems = new Micromind.DataControls.MMSDataGrid.POSGrid();
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).BeginInit();
			SuspendLayout();
			ultraDataSource1.Band.Columns.AddRange(new object[2]
			{
				ultraDataColumn,
				ultraDataColumn2
			});
			buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCancel.Appearance.Options.UseFont = true;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(342, 357);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(97, 40);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "Close";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			labelItem.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelItem.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			labelItem.Location = new System.Drawing.Point(13, 89);
			labelItem.Name = "labelItem";
			labelItem.Size = new System.Drawing.Size(426, 52);
			labelItem.TabIndex = 37;
			labelVoucherNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoucherNumber.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			labelVoucherNumber.Location = new System.Drawing.Point(12, 8);
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
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.Location = new System.Drawing.Point(12, 147);
			dataGridItems.Margin = new System.Windows.Forms.Padding(6);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.Size = new System.Drawing.Size(427, 201);
			dataGridItems.TabIndex = 45;
			base.AcceptButton = simpleButton1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(456, 409);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(textBoxScan);
			base.Controls.Add(labelItem);
			base.Controls.Add(labelVoucherNumber);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(simpleButton1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CheckQuantityForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Stock Availability";
			base.Load += new System.EventHandler(XtraForm1_Load);
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
