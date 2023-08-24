using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.MMSDataGrid
{
	public class ButtonTextBoxControl : UserControl
	{
		private Type type = typeof(string);

		private string textFormat = "";

		private IContainer components;

		private Button buttonButton;

		private UltraTextEditor textBoxText;

		private Button buttonButton2;

		public string SecondButtonName => buttonButton2.Name;

		public override string Text
		{
			get
			{
				return textBoxText.Text;
			}
			set
			{
				if (type == typeof(decimal))
				{
					if (value != null && value != "")
					{
						textBoxText.Text = decimal.Parse(value).ToString(textFormat);
					}
					else
					{
						textBoxText.Text = decimal.Parse("0").ToString(textFormat);
					}
				}
				else
				{
					textBoxText.Text = value;
				}
			}
		}

		public event EventHandler ButtonClicked;

		public ButtonTextBoxControl(string name, string buttonText, HAlign hAlign, Type valueType, string format)
		{
			InitializeComponent();
			buttonButton.BackColor = Color.Gainsboro;
			textBoxText.BorderStyle = UIElementBorderStyle.None;
			textBoxText.Appearance.BackColor = (textBoxText.Appearance.BackColorDisabled = Color.FromArgb(54, 118, 216));
			textBoxText.Appearance.ForeColor = (textBoxText.Appearance.ForeColorDisabled = Color.White);
			BackColor = Color.FromArgb(54, 118, 216);
			buttonButton.ForeColor = Color.Black;
			buttonButton.Text = buttonText;
			buttonButton2.Visible = false;
			textBoxText.Appearance.TextHAlign = hAlign;
			buttonButton.Name = name;
			type = valueType;
			textBoxText.Enabled = false;
			textFormat = format;
		}

		public ButtonTextBoxControl(string name, string buttonText, string secondButtonName, string secondButtonText, HAlign hAlign, Type valueType, string format)
		{
			InitializeComponent();
			buttonButton.BackColor = Color.Gainsboro;
			buttonButton2.BackColor = Color.Gainsboro;
			textBoxText.BorderStyle = UIElementBorderStyle.None;
			textBoxText.Appearance.BackColor = (textBoxText.Appearance.BackColorDisabled = Color.FromArgb(54, 118, 216));
			textBoxText.Appearance.ForeColor = (textBoxText.Appearance.ForeColorDisabled = Color.White);
			BackColor = Color.FromArgb(54, 118, 216);
			buttonButton.ForeColor = Color.Black;
			buttonButton2.ForeColor = Color.Black;
			buttonButton.Text = buttonText;
			buttonButton2.Visible = true;
			buttonButton.Name = name;
			buttonButton2.Text = secondButtonText;
			buttonButton2.Name = secondButtonName;
			textBoxText.Appearance.TextHAlign = hAlign;
			base.Name = name;
			type = valueType;
			textBoxText.Enabled = false;
			textFormat = format;
		}

		private void buttonButton_Click(object sender, EventArgs e)
		{
			if (this.ButtonClicked != null)
			{
				this.ButtonClicked(sender, e);
			}
		}

		private void buttonButton2_Click(object sender, EventArgs e)
		{
			if (this.ButtonClicked != null)
			{
				this.ButtonClicked(sender, e);
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
			buttonButton = new System.Windows.Forms.Button();
			textBoxText = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			buttonButton2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)textBoxText).BeginInit();
			SuspendLayout();
			buttonButton.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonButton.BackColor = System.Drawing.Color.Gainsboro;
			buttonButton.Location = new System.Drawing.Point(7, 25);
			buttonButton.Name = "buttonButton";
			buttonButton.Size = new System.Drawing.Size(88, 48);
			buttonButton.TabIndex = 0;
			buttonButton.Text = "Edit";
			buttonButton.UseVisualStyleBackColor = false;
			buttonButton.Click += new System.EventHandler(buttonButton_Click);
			textBoxText.Dock = System.Windows.Forms.DockStyle.Top;
			textBoxText.Location = new System.Drawing.Point(0, 0);
			textBoxText.Name = "textBoxText";
			textBoxText.Size = new System.Drawing.Size(218, 21);
			textBoxText.TabIndex = 1;
			buttonButton2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonButton2.BackColor = System.Drawing.Color.Gainsboro;
			buttonButton2.Location = new System.Drawing.Point(101, 25);
			buttonButton2.Name = "buttonButton2";
			buttonButton2.Size = new System.Drawing.Size(88, 48);
			buttonButton2.TabIndex = 2;
			buttonButton2.Text = "Edit";
			buttonButton2.UseVisualStyleBackColor = false;
			buttonButton2.Click += new System.EventHandler(buttonButton2_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(buttonButton2);
			base.Controls.Add(textBoxText);
			base.Controls.Add(buttonButton);
			base.Name = "ButtonTextBoxControl";
			base.Size = new System.Drawing.Size(218, 79);
			((System.ComponentModel.ISupportInitialize)textBoxText).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
