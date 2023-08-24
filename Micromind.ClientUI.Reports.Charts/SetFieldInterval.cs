using DevExpress.XtraPivotGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Charts
{
	public class SetFieldInterval : Form
	{
		private IContainer components;

		private ComboBox comboBox1;

		private Label label1;

		private Button buttonOK;

		public object SelectedItem
		{
			get
			{
				return comboBox1.SelectedItem;
			}
			set
			{
				comboBox1.SelectedItem = value;
			}
		}

		public SetFieldInterval()
		{
			InitializeComponent();
			LoadCombo();
		}

		private void LoadCombo()
		{
			foreach (object value in Enum.GetValues(typeof(PivotGroupInterval)))
			{
				comboBox1.Items.Add(value);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
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
			comboBox1 = new System.Windows.Forms.ComboBox();
			label1 = new System.Windows.Forms.Label();
			buttonOK = new System.Windows.Forms.Button();
			SuspendLayout();
			comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new System.Drawing.Point(76, 21);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new System.Drawing.Size(195, 21);
			comboBox1.TabIndex = 0;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 24);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(45, 13);
			label1.TabIndex = 1;
			label1.Text = "Interval:";
			buttonOK.Location = new System.Drawing.Point(192, 60);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(79, 21);
			buttonOK.TabIndex = 2;
			buttonOK.Text = "OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(283, 87);
			base.Controls.Add(buttonOK);
			base.Controls.Add(label1);
			base.Controls.Add(comboBox1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SetFieldInterval";
			base.ShowIcon = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Interval";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
