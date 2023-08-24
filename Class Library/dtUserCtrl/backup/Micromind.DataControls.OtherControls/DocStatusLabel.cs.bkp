using Infragistics.Win;
using Infragistics.Win.Misc;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.OtherControls
{
	public class DocStatusLabel : UserControl
	{
		public enum StatusColors
		{
			Red,
			Green,
			Blue
		}

		private StatusColors color;

		private IContainer components;

		private UltraLabel labelStatus;

		private UltraPanel panelNumber;

		private LinkLabel labelInvoiceNumber;

		public new object Tag
		{
			get
			{
				return labelInvoiceNumber.Tag;
			}
			set
			{
				labelInvoiceNumber.Tag = value;
			}
		}

		public bool ShowDocNumber
		{
			get
			{
				return panelNumber.Visible;
			}
			set
			{
				panelNumber.Visible = value;
			}
		}

		public string DisplayStatus
		{
			set
			{
				labelStatus.Text = value.ToString().ToUpper();
			}
		}

		public string DocumentNumber
		{
			get
			{
				return labelInvoiceNumber.Text;
			}
			set
			{
				labelInvoiceNumber.Text = value.ToUpper();
			}
		}

		public bool LinkEnabled
		{
			get
			{
				return labelInvoiceNumber.Enabled;
			}
			set
			{
				labelInvoiceNumber.Enabled = value;
			}
		}

		public StatusColors StatusColor
		{
			get
			{
				return color;
			}
			set
			{
				Color color = Color.Red;
				if (StatusColor == StatusColors.Green)
				{
					color = Color.Green;
				}
				else if (StatusColor == StatusColors.Blue)
				{
					color = Color.Blue;
				}
				AppearanceBase appearance = labelStatus.Appearance;
				AppearanceBase appearance2 = labelStatus.Appearance;
				Color color3 = panelNumber.Appearance.BorderColor = color;
				Color color6 = appearance.ForeColor = (appearance2.BorderColor = color3);
			}
		}

		public event EventHandler LinkClicked;

		public DocStatusLabel()
		{
			InitializeComponent();
			labelInvoiceNumber.LinkClicked += labelInvoiceNumber_LinkClicked;
		}

		private void labelInvoiceNumber_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (this.LinkClicked != null)
			{
				this.LinkClicked(labelInvoiceNumber, e);
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
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			labelStatus = new Infragistics.Win.Misc.UltraLabel();
			panelNumber = new Infragistics.Win.Misc.UltraPanel();
			labelInvoiceNumber = new System.Windows.Forms.LinkLabel();
			panelNumber.ClientArea.SuspendLayout();
			panelNumber.SuspendLayout();
			SuspendLayout();
			labelStatus.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BorderColor = System.Drawing.Color.Red;
			appearance.ForeColor = System.Drawing.Color.Red;
			appearance.TextHAlignAsString = "Center";
			labelStatus.Appearance = appearance;
			labelStatus.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.Solid;
			labelStatus.Font = new System.Drawing.Font("Tahoma", 14f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelStatus.Location = new System.Drawing.Point(1, 0);
			labelStatus.Name = "labelStatus";
			labelStatus.Size = new System.Drawing.Size(156, 27);
			labelStatus.TabIndex = 146;
			labelStatus.Text = "OPEN";
			panelNumber.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance2.BorderColor = System.Drawing.Color.Red;
			panelNumber.Appearance = appearance2;
			panelNumber.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			panelNumber.ClientArea.Controls.Add(labelInvoiceNumber);
			panelNumber.Location = new System.Drawing.Point(1, 26);
			panelNumber.Name = "panelNumber";
			panelNumber.Size = new System.Drawing.Size(156, 25);
			panelNumber.TabIndex = 147;
			labelInvoiceNumber.Dock = System.Windows.Forms.DockStyle.Fill;
			labelInvoiceNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelInvoiceNumber.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			labelInvoiceNumber.Location = new System.Drawing.Point(0, 0);
			labelInvoiceNumber.Name = "labelInvoiceNumber";
			labelInvoiceNumber.Size = new System.Drawing.Size(154, 23);
			labelInvoiceNumber.TabIndex = 148;
			labelInvoiceNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(panelNumber);
			base.Controls.Add(labelStatus);
			base.Name = "DocStatusLabel";
			base.Size = new System.Drawing.Size(159, 52);
			panelNumber.ClientArea.ResumeLayout(false);
			panelNumber.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
