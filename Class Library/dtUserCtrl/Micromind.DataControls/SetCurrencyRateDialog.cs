using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class SetCurrencyRateDialog : Form
	{
		private IContainer components;

		private Label label1;

		private Button buttonCancel;

		private Button buttonOK;

		private UnitPriceTextBox textBoxRate;

		public decimal Rate
		{
			get
			{
				return decimal.Parse(textBoxRate.Text);
			}
			set
			{
				textBoxRate.Text = value.ToString();
			}
		}

		public SetCurrencyRateDialog()
		{
			InitializeComponent();
			base.AcceptButton = buttonOK;
			base.CancelButton = buttonCancel;
			base.Activated += SetCurrencyRateDialog_Activated;
		}

		private void SetCurrencyRateDialog_Activated(object sender, EventArgs e)
		{
			textBoxRate.SelectAll();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
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
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			textBoxRate = new Micromind.UISupport.UnitPriceTextBox();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(8, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(33, 13);
			label1.TabIndex = 1;
			label1.Text = "Rate:";
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(186, 52);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(82, 24);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.Location = new System.Drawing.Point(98, 52);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(82, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			textBoxRate.CustomReportFieldName = "";
			textBoxRate.CustomReportKey = "";
			textBoxRate.CustomReportValueType = 1;
			textBoxRate.IsComboTextBox = false;
			textBoxRate.IsModified = false;
			textBoxRate.Location = new System.Drawing.Point(47, 15);
			textBoxRate.MaxLength = 14;
			textBoxRate.Name = "textBoxRate";
			textBoxRate.Size = new System.Drawing.Size(221, 20);
			textBoxRate.TabIndex = 0;
			textBoxRate.Text = "1";
			textBoxRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(280, 86);
			base.Controls.Add(textBoxRate);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SetCurrencyRateDialog";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Currency Rate";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
