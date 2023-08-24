using DevExpress.XtraEditors;
using Infragistics.Win;
using Infragistics.Win.UltraWinDataSource;
using Infragistics.Win.UltraWinEditors;
using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.POS
{
	public class SetTotalForm : XtraForm
	{
		private decimal subtotal;

		private bool isDiscountPercent;

		private IContainer components;

		private UltraDataSource ultraDataSource1;

		private SimpleButton buttonSave;

		private SimpleButton buttonCancel;

		private UltraTextEditor textBoxTotal;

		private Label label1;

		private NumericKeypad numericKeypad1;

		private Line line1;

		public decimal Subtotal
		{
			set
			{
				subtotal = value;
			}
		}

		public decimal Total
		{
			get
			{
				return decimal.Parse(textBoxTotal.Text);
			}
			set
			{
				textBoxTotal.Text = value.ToString(Format.TotalAmountFormat);
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

		public SetTotalForm()
		{
			InitializeComponent();
		}

		private void XtraForm1_Load(object sender, EventArgs e)
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (decimal.Parse(textBoxTotal.Text) > subtotal)
				{
					ErrorHelper.InformationMessage("Total amount should be less or equal to the subtotal.");
					base.DialogResult = DialogResult.None;
					textBoxTotal.Focus();
					textBoxTotal.SelectAll();
					return;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
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
		}

		private void textBoxDiscountPercent_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBoxDiscountAmount_TextChanged(object sender, EventArgs e)
		{
		}

		private void CalculateTotal()
		{
		}

		private void DiscountForm_Activated(object sender, EventArgs e)
		{
			textBoxTotal.Focus();
			textBoxTotal.SelectAll();
		}

		private void textBoxDiscountAmount_Validated(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void textBoxDiscountPercent_Validated(object sender, EventArgs e)
		{
			CalculateTotal();
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
			ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource();
			buttonSave = new DevExpress.XtraEditors.SimpleButton();
			buttonCancel = new DevExpress.XtraEditors.SimpleButton();
			textBoxTotal = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			label1 = new System.Windows.Forms.Label();
			numericKeypad1 = new Micromind.UISupport.NumericKeypad();
			line1 = new Micromind.UISupport.Line();
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxTotal).BeginInit();
			SuspendLayout();
			ultraDataSource1.Band.Columns.AddRange(new object[2]
			{
				ultraDataColumn,
				ultraDataColumn2
			});
			buttonSave.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonSave.Appearance.Options.UseFont = true;
			buttonSave.Location = new System.Drawing.Point(61, 196);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(102, 40);
			buttonSave.TabIndex = 2;
			buttonSave.Text = "OK";
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCancel.Appearance.Options.UseFont = true;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(169, 196);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(97, 40);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			textBoxTotal.AlwaysInEditMode = true;
			appearance.BorderColor = System.Drawing.Color.FromArgb(16, 37, 127);
			appearance.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			appearance.TextHAlignAsString = "Right";
			textBoxTotal.Appearance = appearance;
			textBoxTotal.AutoSize = false;
			textBoxTotal.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			textBoxTotal.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold);
			textBoxTotal.Location = new System.Drawing.Point(12, 53);
			textBoxTotal.Name = "textBoxTotal";
			textBoxTotal.Size = new System.Drawing.Size(254, 33);
			textBoxTotal.TabIndex = 0;
			textBoxTotal.Text = "0.00";
			textBoxTotal.TextChanged += new System.EventHandler(textBoxDiscountAmount_TextChanged);
			textBoxTotal.Validating += new System.ComponentModel.CancelEventHandler(textBoxDiscountAmount_Validating);
			textBoxTotal.Validated += new System.EventHandler(textBoxDiscountAmount_Validated);
			label1.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label1.Location = new System.Drawing.Point(9, 28);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(181, 22);
			label1.TabIndex = 37;
			label1.Text = "Enter the Total Amount:";
			numericKeypad1.DisplayControl = textBoxTotal;
			numericKeypad1.Location = new System.Drawing.Point(286, 2);
			numericKeypad1.MaximumSize = new System.Drawing.Size(197, 244);
			numericKeypad1.MinimumSize = new System.Drawing.Size(197, 244);
			numericKeypad1.Name = "numericKeypad1";
			numericKeypad1.Size = new System.Drawing.Size(197, 244);
			numericKeypad1.TabIndex = 38;
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = true;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(275, 11);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(1, 224);
			line1.TabIndex = 39;
			line1.TabStop = false;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(487, 248);
			base.Controls.Add(line1);
			base.Controls.Add(numericKeypad1);
			base.Controls.Add(textBoxTotal);
			base.Controls.Add(label1);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonSave);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SetTotalForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Total";
			base.Activated += new System.EventHandler(DiscountForm_Activated);
			base.Load += new System.EventHandler(XtraForm1_Load);
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxTotal).EndInit();
			ResumeLayout(false);
		}
	}
}
