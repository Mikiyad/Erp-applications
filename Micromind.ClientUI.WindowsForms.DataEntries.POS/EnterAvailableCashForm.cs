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
	public class EnterAvailableCashForm : XtraForm
	{
		private IContainer components;

		private UltraDataSource ultraDataSource1;

		private SimpleButton buttonSave;

		private SimpleButton buttonCancel;

		private Label label1;

		private Label labelTitle;

		private UltraTextEditor textBoxAmount;

		private NumericKeypad numericKeypad1;

		private Line line1;

		public bool IsEndingBalance
		{
			set
			{
				if (value)
				{
					labelTitle.Text = "Enter Ending Cash Balance:";
				}
			}
		}

		public decimal AvailableCashAmount
		{
			get
			{
				return decimal.Parse(textBoxAmount.Text);
			}
			set
			{
				_ = (decimal.Parse(textBoxAmount.Text) == 0m);
			}
		}

		public EnterAvailableCashForm()
		{
			InitializeComponent();
		}

		private void XtraForm1_Load(object sender, EventArgs e)
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			decimal result = default(decimal);
			if (!decimal.TryParse(textBoxAmount.Text, out result))
			{
				ErrorHelper.InformationMessage("Please enter a numeric value.");
				return;
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

		private void DiscountForm_Activated(object sender, EventArgs e)
		{
			textBoxAmount.Focus();
			textBoxAmount.SelectAll();
		}

		private void textBoxDiscountAmount_AfterEnterEditMode(object sender, EventArgs e)
		{
			numericKeypad1.DisplayControl = textBoxAmount;
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
			label1 = new System.Windows.Forms.Label();
			labelTitle = new System.Windows.Forms.Label();
			textBoxAmount = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			line1 = new Micromind.UISupport.Line();
			numericKeypad1 = new Micromind.UISupport.NumericKeypad();
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxAmount).BeginInit();
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
			label1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label1.Font = new System.Drawing.Font("Tahoma", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label1.Location = new System.Drawing.Point(12, 45);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(66, 27);
			label1.TabIndex = 37;
			label1.Text = "Cash:";
			labelTitle.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelTitle.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			labelTitle.Location = new System.Drawing.Point(12, 9);
			labelTitle.Name = "labelTitle";
			labelTitle.Size = new System.Drawing.Size(324, 21);
			labelTitle.TabIndex = 36;
			labelTitle.Text = "Enter Opening Cash Balance:";
			textBoxAmount.AlwaysInEditMode = true;
			appearance.BorderColor = System.Drawing.Color.FromArgb(16, 37, 127);
			appearance.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			appearance.TextHAlignAsString = "Right";
			textBoxAmount.Appearance = appearance;
			textBoxAmount.AutoSize = false;
			textBoxAmount.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			textBoxAmount.Font = new System.Drawing.Font("Tahoma", 14f, System.Drawing.FontStyle.Bold);
			textBoxAmount.Location = new System.Drawing.Point(84, 44);
			textBoxAmount.Name = "textBoxAmount";
			textBoxAmount.Size = new System.Drawing.Size(236, 33);
			textBoxAmount.TabIndex = 0;
			textBoxAmount.Text = "0.00";
			textBoxAmount.AfterEnterEditMode += new System.EventHandler(textBoxDiscountAmount_AfterEnterEditMode);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = true;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(342, 22);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(1, 210);
			line1.TabIndex = 44;
			line1.TabStop = false;
			numericKeypad1.DisplayControl = textBoxAmount;
			numericKeypad1.Location = new System.Drawing.Point(356, 2);
			numericKeypad1.MaximumSize = new System.Drawing.Size(197, 244);
			numericKeypad1.MinimumSize = new System.Drawing.Size(197, 244);
			numericKeypad1.Name = "numericKeypad1";
			numericKeypad1.Size = new System.Drawing.Size(197, 244);
			numericKeypad1.TabIndex = 43;
			numericKeypad1.TabStop = false;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(559, 250);
			base.Controls.Add(line1);
			base.Controls.Add(numericKeypad1);
			base.Controls.Add(textBoxAmount);
			base.Controls.Add(label1);
			base.Controls.Add(labelTitle);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonSave);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EnterAvailableCashForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Enter Available Cash";
			base.Activated += new System.EventHandler(DiscountForm_Activated);
			base.Load += new System.EventHandler(XtraForm1_Load);
			((System.ComponentModel.ISupportInitialize)ultraDataSource1).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxAmount).EndInit();
			ResumeLayout(false);
		}
	}
}
