using DevExpress.Utils;
using DevExpress.XtraEditors;
using Infragistics.Win;
using Infragistics.Win.UltraWinDataSource;
using Infragistics.Win.UltraWinEditors;
using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class DiscountForm : XtraForm
	{
		private bool isDiscountPercent;

		private IContainer components;

		private UltraDataSource ultraDataSource1;

		private SimpleButton buttonSave;

		private SimpleButton buttonCancel;

		private TextEdit textBoxSubtotal;

		private Label label2;

		private Label label1;

		private TextEdit textBoxTotal;

		private Label label3;

		private Label labelVoucherNumber;

		private UltraTextEditor textBoxDiscountAmount;

		private UltraTextEditor textBoxDiscountPercent;

		private NumericKeypad numericKeypad1;

		private Line line1;

		public string Subtotal
		{
			set
			{
				textBoxSubtotal.Text = value;
			}
		}

		public string Total
		{
			set
			{
				textBoxTotal.Text = value;
			}
		}

		public string DiscountAmount
		{
			get
			{
				return textBoxDiscountAmount.Text;
			}
			set
			{
				textBoxDiscountAmount.Text = value;
				decimal d = decimal.Parse(textBoxDiscountAmount.Text);
				if (!(d == 0m))
				{
					decimal d2 = decimal.Parse(textBoxSubtotal.Text);
					textBoxDiscountPercent.Text = (d / d2 * 100m).ToString(Format.TotalAmountFormat);
				}
			}
		}

		public bool IsPercent
		{
			get
			{
				return isDiscountPercent;
			}
			set
			{
				isDiscountPercent = value;
			}
		}

		public DiscountForm()
		{
			InitializeComponent();
		}

		private void XtraForm1_Load(object sender, EventArgs e)
		{
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

		private void textBoxDiscountAmount_Validating(object sender, CancelEventArgs e)
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			num = decimal.Parse(textBoxDiscountAmount.Text, NumberStyles.Any);
			num2 = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
			if (!(num == 0m))
			{
				if (num > num2)
				{
					ErrorHelper.InformationMessage("Discount amount should be less or equal to the subtotal.");
					e.Cancel = true;
				}
				else if (num < 0m)
				{
					ErrorHelper.InformationMessage("Discount amount should be greater than or equal to zero.");
					e.Cancel = true;
				}
			}
		}

		private void textBoxDiscountPercent_TextChanged(object sender, EventArgs e)
		{
			if (textBoxDiscountPercent.Focused)
			{
				isDiscountPercent = true;
			}
		}

		private void textBoxDiscountAmount_TextChanged(object sender, EventArgs e)
		{
			if (textBoxDiscountAmount.Focused)
			{
				isDiscountPercent = false;
			}
		}

		private void CalculateTotal()
		{
			decimal num = default(decimal);
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal num2 = default(decimal);
			num = decimal.Parse(textBoxSubtotal.Text);
			textBoxSubtotal.Text = num.ToString(Format.TotalAmountFormat);
			decimal.TryParse(textBoxDiscountPercent.Text, out result2);
			decimal.TryParse(textBoxDiscountAmount.Text, out result);
			num2 = num;
			if (isDiscountPercent && result2 != 0m)
			{
				result = Math.Round(num * result2 / 100m, Global.CurDecimalPoints);
				textBoxDiscountAmount.Text = result.ToString(Format.TotalAmountFormat);
			}
			else if (num > 0m)
			{
				result2 = Math.Round(result / num2 * 100m, Global.CurDecimalPoints);
				textBoxDiscountPercent.Text = result2.ToString();
			}
			else
			{
				textBoxDiscountPercent.Text = "0";
				textBoxDiscountAmount.Text = 0.ToString(Format.TotalAmountFormat);
			}
			num2 = num - result;
			textBoxTotal.Text = num2.ToString(Format.TotalAmountFormat);
		}

		private void DiscountForm_Activated(object sender, EventArgs e)
		{
			textBoxDiscountAmount.Focus();
			textBoxDiscountAmount.SelectAll();
		}

		private void textBoxDiscountAmount_Validated(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxDiscountPercent_Validated(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxDiscountPercent_AfterEnterEditMode(object sender, EventArgs e)
		{
			numericKeypad1.DisplayControl = textBoxDiscountPercent;
		}

		private void textBoxDiscountAmount_AfterEnterEditMode(object sender, EventArgs e)
		{
			numericKeypad1.DisplayControl = textBoxDiscountAmount;
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
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 0");
			Infragistics.Win.UltraWinDataSource.UltraDataColumn ultraDataColumn2 = new Infragistics.Win.UltraWinDataSource.UltraDataColumn("Column 1");
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource();
			buttonSave = new DevExpress.XtraEditors.SimpleButton();
			buttonCancel = new DevExpress.XtraEditors.SimpleButton();
			textBoxSubtotal = new DevExpress.XtraEditors.TextEdit();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			textBoxTotal = new DevExpress.XtraEditors.TextEdit();
			label3 = new System.Windows.Forms.Label();
			labelVoucherNumber = new System.Windows.Forms.Label();
			textBoxDiscountAmount = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			textBoxDiscountPercent = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			numericKeypad1 = new Micromind.UISupport.NumericKeypad();
			line1 = new Micromind.UISupport.Line();
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxSubtotal.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxTotal.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxDiscountAmount).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxDiscountPercent).BeginInit();
			SuspendLayout();
			ultraDataSource1.Band.Columns.AddRange(new object[2]
			{
				ultraDataColumn,
				ultraDataColumn2
			});
			buttonSave.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonSave.Appearance.Options.UseFont = true;
			buttonSave.Location = new System.Drawing.Point(117, 192);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(102, 40);
			buttonSave.TabIndex = 2;
			buttonSave.Text = "OK";
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCancel.Appearance.Options.UseFont = true;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(225, 192);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(97, 40);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			textBoxSubtotal.EditValue = "0.00";
			textBoxSubtotal.Location = new System.Drawing.Point(115, 107);
			textBoxSubtotal.Name = "textBoxSubtotal";
			textBoxSubtotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			textBoxSubtotal.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			textBoxSubtotal.Properties.Appearance.Options.UseFont = true;
			textBoxSubtotal.Properties.Appearance.Options.UseForeColor = true;
			textBoxSubtotal.Properties.Appearance.Options.UseTextOptions = true;
			textBoxSubtotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			textBoxSubtotal.Properties.AutoHeight = false;
			textBoxSubtotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			textBoxSubtotal.Properties.ReadOnly = true;
			textBoxSubtotal.Size = new System.Drawing.Size(205, 33);
			textBoxSubtotal.TabIndex = 42;
			textBoxSubtotal.TabStop = false;
			label2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label2.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label2.Location = new System.Drawing.Point(78, 51);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(24, 22);
			label2.TabIndex = 38;
			label2.Text = "%";
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label1.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label1.Location = new System.Drawing.Point(13, 117);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(75, 22);
			label1.TabIndex = 37;
			label1.Text = "Subtotal:";
			textBoxTotal.EditValue = "0.00";
			textBoxTotal.Location = new System.Drawing.Point(115, 139);
			textBoxTotal.Name = "textBoxTotal";
			textBoxTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			textBoxTotal.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			textBoxTotal.Properties.Appearance.Options.UseFont = true;
			textBoxTotal.Properties.Appearance.Options.UseForeColor = true;
			textBoxTotal.Properties.Appearance.Options.UseTextOptions = true;
			textBoxTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			textBoxTotal.Properties.AutoHeight = false;
			textBoxTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			textBoxTotal.Properties.ReadOnly = true;
			textBoxTotal.Size = new System.Drawing.Size(205, 33);
			textBoxTotal.TabIndex = 41;
			textBoxTotal.TabStop = false;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label3.Location = new System.Drawing.Point(13, 150);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(135, 22);
			label3.TabIndex = 35;
			label3.Text = "Total:";
			labelVoucherNumber.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelVoucherNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoucherNumber.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			labelVoucherNumber.Location = new System.Drawing.Point(12, 9);
			labelVoucherNumber.Name = "labelVoucherNumber";
			labelVoucherNumber.Size = new System.Drawing.Size(367, 21);
			labelVoucherNumber.TabIndex = 36;
			labelVoucherNumber.Text = "Enter discount amount or percent:";
			textBoxDiscountAmount.AlwaysInEditMode = true;
			appearance.BorderColor = System.Drawing.Color.FromArgb(16, 37, 127);
			appearance.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			appearance.TextHAlignAsString = "Right";
			textBoxDiscountAmount.Appearance = appearance;
			textBoxDiscountAmount.AutoSize = false;
			textBoxDiscountAmount.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			textBoxDiscountAmount.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold);
			textBoxDiscountAmount.Location = new System.Drawing.Point(115, 44);
			textBoxDiscountAmount.Name = "textBoxDiscountAmount";
			textBoxDiscountAmount.Size = new System.Drawing.Size(205, 33);
			textBoxDiscountAmount.TabIndex = 0;
			textBoxDiscountAmount.Text = "0.00";
			textBoxDiscountAmount.AfterEnterEditMode += new System.EventHandler(textBoxDiscountAmount_AfterEnterEditMode);
			textBoxDiscountAmount.TextChanged += new System.EventHandler(textBoxDiscountAmount_TextChanged);
			textBoxDiscountAmount.Validating += new System.ComponentModel.CancelEventHandler(textBoxDiscountAmount_Validating);
			textBoxDiscountAmount.Validated += new System.EventHandler(textBoxDiscountAmount_Validated);
			textBoxDiscountPercent.AlwaysInEditMode = true;
			appearance2.BorderColor = System.Drawing.Color.FromArgb(16, 37, 127);
			appearance2.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			appearance2.TextHAlignAsString = "Right";
			textBoxDiscountPercent.Appearance = appearance2;
			textBoxDiscountPercent.AutoSize = false;
			textBoxDiscountPercent.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			textBoxDiscountPercent.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold);
			textBoxDiscountPercent.Location = new System.Drawing.Point(17, 44);
			textBoxDiscountPercent.Name = "textBoxDiscountPercent";
			textBoxDiscountPercent.Size = new System.Drawing.Size(59, 33);
			textBoxDiscountPercent.TabIndex = 1;
			textBoxDiscountPercent.Text = "0";
			textBoxDiscountPercent.AfterEnterEditMode += new System.EventHandler(textBoxDiscountPercent_AfterEnterEditMode);
			textBoxDiscountPercent.TextChanged += new System.EventHandler(textBoxDiscountPercent_TextChanged);
			textBoxDiscountPercent.Validated += new System.EventHandler(textBoxDiscountPercent_Validated);
			numericKeypad1.DisplayControl = textBoxDiscountAmount;
			numericKeypad1.Location = new System.Drawing.Point(356, 2);
			numericKeypad1.MaximumSize = new System.Drawing.Size(197, 244);
			numericKeypad1.MinimumSize = new System.Drawing.Size(197, 244);
			numericKeypad1.Name = "numericKeypad1";
			numericKeypad1.Size = new System.Drawing.Size(197, 244);
			numericKeypad1.TabIndex = 43;
			numericKeypad1.TabStop = false;
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = true;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(342, 22);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(1, 210);
			line1.TabIndex = 44;
			line1.TabStop = false;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(559, 250);
			base.Controls.Add(line1);
			base.Controls.Add(numericKeypad1);
			base.Controls.Add(textBoxDiscountPercent);
			base.Controls.Add(textBoxDiscountAmount);
			base.Controls.Add(textBoxSubtotal);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxTotal);
			base.Controls.Add(label3);
			base.Controls.Add(labelVoucherNumber);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonSave);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DiscountForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Discount";
			base.Activated += new System.EventHandler(DiscountForm_Activated);
			base.Load += new System.EventHandler(XtraForm1_Load);
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxSubtotal.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxTotal.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxDiscountAmount).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxDiscountPercent).EndInit();
			ResumeLayout(false);
		}
	}
}
