using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ProductLotDrillDialog : Form
	{
		private IContainer components;

		private Label label1;

		private TextBox textBoxLotNumber;

		private Label label2;

		private TextBox textBoxDocID;

		private TextBox textBoxVoucherID;

		private TextBox textBoxReceiptDate;

		private Label label3;

		private TextBox textBoxLocation;

		private Label label4;

		private TextBox textBoxLotQty;

		private Label label6;

		private Label label7;

		private Label label8;

		private TextBox textBoxBalanceQty;

		private Button buttonClose;

		private Line line1;

		private TextBox textBoxVendor;

		private Label label9;

		public ProductLotDrillDialog()
		{
			InitializeComponent();
		}

		private void ProductLotDrillDialog_Load(object sender, EventArgs e)
		{
		}

		public void LoadData(string lotID)
		{
			try
			{
				DataRow dataRow = Factory.ProductSystem.GetProductLot(lotID).Tables[0].Rows[0];
				textBoxLotNumber.Text = lotID;
				textBoxLocation.Text = dataRow["LocationID"].ToString();
				textBoxDocID.Text = dataRow["DocID"].ToString();
				textBoxVoucherID.Text = dataRow["ReceiptNumber"].ToString();
				textBoxReceiptDate.Text = DateTime.Parse(dataRow["ReceiptDate"].ToString()).ToShortDateString();
				textBoxVendor.Text = dataRow["Vendor"].ToString();
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(dataRow["BalanceQty"].ToString(), out result);
				decimal.TryParse(dataRow["ChildLotsBalance"].ToString(), out result2);
				textBoxLotQty.Text = decimal.Parse(dataRow["LotQty"].ToString()).ToString(Format.QuantityFormat);
				textBoxBalanceQty.Text = (result + result2).ToString(Format.QuantityFormat);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
			label1 = new System.Windows.Forms.Label();
			textBoxLotNumber = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxDocID = new System.Windows.Forms.TextBox();
			textBoxVoucherID = new System.Windows.Forms.TextBox();
			textBoxReceiptDate = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			textBoxLocation = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			textBoxLotQty = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			textBoxBalanceQty = new System.Windows.Forms.TextBox();
			buttonClose = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			textBoxVendor = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(6, 13);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(65, 13);
			label1.TabIndex = 0;
			label1.Text = "Lot Number:";
			textBoxLotNumber.Location = new System.Drawing.Point(81, 10);
			textBoxLotNumber.Name = "textBoxLotNumber";
			textBoxLotNumber.ReadOnly = true;
			textBoxLotNumber.Size = new System.Drawing.Size(106, 20);
			textBoxLotNumber.TabIndex = 0;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(6, 39);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(50, 13);
			label2.TabIndex = 0;
			label2.Text = "Voucher:";
			textBoxDocID.Location = new System.Drawing.Point(81, 36);
			textBoxDocID.Name = "textBoxDocID";
			textBoxDocID.ReadOnly = true;
			textBoxDocID.Size = new System.Drawing.Size(68, 20);
			textBoxDocID.TabIndex = 1;
			textBoxVoucherID.Location = new System.Drawing.Point(152, 36);
			textBoxVoucherID.Name = "textBoxVoucherID";
			textBoxVoucherID.ReadOnly = true;
			textBoxVoucherID.Size = new System.Drawing.Size(140, 20);
			textBoxVoucherID.TabIndex = 2;
			textBoxReceiptDate.Location = new System.Drawing.Point(81, 62);
			textBoxReceiptDate.Name = "textBoxReceiptDate";
			textBoxReceiptDate.ReadOnly = true;
			textBoxReceiptDate.Size = new System.Drawing.Size(106, 20);
			textBoxReceiptDate.TabIndex = 3;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(6, 65);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(73, 13);
			label3.TabIndex = 2;
			label3.Text = "Receipt Date:";
			textBoxLocation.Location = new System.Drawing.Point(261, 62);
			textBoxLocation.Name = "textBoxLocation";
			textBoxLocation.ReadOnly = true;
			textBoxLocation.Size = new System.Drawing.Size(215, 20);
			textBoxLocation.TabIndex = 4;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(190, 65);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(51, 13);
			label4.TabIndex = 4;
			label4.Text = "Location:";
			textBoxLotQty.Location = new System.Drawing.Point(81, 115);
			textBoxLotQty.Name = "textBoxLotQty";
			textBoxLotQty.ReadOnly = true;
			textBoxLotQty.Size = new System.Drawing.Size(106, 20);
			textBoxLotQty.TabIndex = 6;
			textBoxLotQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(6, 118);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(44, 13);
			label6.TabIndex = 7;
			label6.Text = "Lot Qty:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(195, 118);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(44, 13);
			label7.TabIndex = 7;
			label7.Text = "Lot Qty:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(190, 118);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(68, 13);
			label8.TabIndex = 7;
			label8.Text = "Balance Qty:";
			textBoxBalanceQty.Location = new System.Drawing.Point(261, 115);
			textBoxBalanceQty.Name = "textBoxBalanceQty";
			textBoxBalanceQty.ReadOnly = true;
			textBoxBalanceQty.Size = new System.Drawing.Size(106, 20);
			textBoxBalanceQty.TabIndex = 7;
			textBoxBalanceQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(393, 193);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(92, 23);
			buttonClose.TabIndex = 8;
			buttonClose.Text = "Close";
			buttonClose.UseVisualStyleBackColor = true;
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(0, 187);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(508, 1);
			line1.TabIndex = 25;
			line1.TabStop = false;
			textBoxVendor.Location = new System.Drawing.Point(81, 89);
			textBoxVendor.Name = "textBoxVendor";
			textBoxVendor.ReadOnly = true;
			textBoxVendor.Size = new System.Drawing.Size(395, 20);
			textBoxVendor.TabIndex = 5;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(6, 92);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(44, 13);
			label9.TabIndex = 26;
			label9.Text = "Vendor:";
			base.AcceptButton = buttonClose;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(494, 222);
			base.Controls.Add(textBoxVendor);
			base.Controls.Add(label9);
			base.Controls.Add(line1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(textBoxBalanceQty);
			base.Controls.Add(label8);
			base.Controls.Add(label7);
			base.Controls.Add(textBoxLotQty);
			base.Controls.Add(label6);
			base.Controls.Add(textBoxLocation);
			base.Controls.Add(label4);
			base.Controls.Add(textBoxReceiptDate);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxVoucherID);
			base.Controls.Add(textBoxDocID);
			base.Controls.Add(label2);
			base.Controls.Add(textBoxLotNumber);
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ProductLotDrillDialog";
			base.ShowInTaskbar = false;
			Text = "Lot Information";
			base.Load += new System.EventHandler(ProductLotDrillDialog_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
