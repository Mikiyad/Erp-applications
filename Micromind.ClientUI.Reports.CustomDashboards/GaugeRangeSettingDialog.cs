using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.CustomDashboards
{
	public class GaugeRangeSettingDialog : Form
	{
		public UltraGridRow GridRow;

		private IContainer components;

		private Button buttonCancel;

		private Button buttonOK;

		private Line line1;

		private MMLabel mmLabel10;

		private ColorEdit colorEditColor;

		public GaugeRangeSettingDialog()
		{
			InitializeComponent();
			base.DialogResult = DialogResult.None;
			base.Load += GaugeRangeSettingDialog_Load;
		}

		private void GaugeRangeSettingDialog_Load(object sender, EventArgs e)
		{
			if (!GridRow.Cells["Color"].Value.IsNullOrEmpty())
			{
				colorEditColor.Color = Color.FromArgb(int.Parse(GridRow.Cells["Color"].Value.ToString()));
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (colorEditColor.Color.IsNullOrEmpty())
				{
					GridRow.Cells["Color"].Value = null;
				}
				else
				{
					GridRow.Cells["Color"].Value = colorEditColor.Color.ToArgb();
				}
				base.DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.CustomDashboards.GaugeRangeSettingDialog));
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			colorEditColor = new DevExpress.XtraEditors.ColorEdit();
			((System.ComponentModel.ISupportInitialize)colorEditColor.Properties).BeginInit();
			SuspendLayout();
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.Location = new System.Drawing.Point(384, 236);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(94, 29);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(284, 236);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(94, 29);
			buttonOK.TabIndex = 2;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-54, 231);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(586, 1);
			line1.TabIndex = 3;
			line1.TabStop = false;
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(12, 29);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(34, 13);
			mmLabel10.TabIndex = 29;
			mmLabel10.Text = "Color:";
			colorEditColor.EditValue = System.Drawing.Color.Empty;
			colorEditColor.Location = new System.Drawing.Point(67, 26);
			colorEditColor.Name = "colorEditColor";
			colorEditColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			colorEditColor.Size = new System.Drawing.Size(172, 20);
			colorEditColor.TabIndex = 30;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(492, 271);
			base.Controls.Add(colorEditColor);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(line1);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "GaugeRangeSettingDialog";
			Text = "Chart Serie Setting";
			((System.ComponentModel.ISupportInitialize)colorEditColor.Properties).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
