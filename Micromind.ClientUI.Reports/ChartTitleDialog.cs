using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports
{
	public class ChartTitleDialog : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		private Label label1;

		private XPButton buttonOk;

		private XPButton buttonCancel;

		private Line line2;

		private MMTextBox textBoxTitle1;

		private XPButton buttonFont1;

		private FontDialog fontDialog1;

		private Container components;

		private Font font1;

		private Color fontColor;

		public Font Font1
		{
			get
			{
				return font1;
			}
			set
			{
				font1 = value;
			}
		}

		public Color FontColor
		{
			get
			{
				return fontColor;
			}
			set
			{
				fontColor = value;
			}
		}

		public string Title1
		{
			get
			{
				return textBoxTitle1.Text;
			}
			set
			{
				textBoxTitle1.Text = value;
			}
		}

		public ChartTitleDialog()
		{
			InitializeComponent();
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
			textBoxTitle1 = new Micromind.UISupport.MMTextBox();
			buttonOk = new Micromind.UISupport.XPButton();
			buttonCancel = new Micromind.UISupport.XPButton();
			line2 = new Micromind.UISupport.Line();
			buttonFont1 = new Micromind.UISupport.XPButton();
			fontDialog1 = new System.Windows.Forms.FontDialog();
			SuspendLayout();
			label1.AutoSize = true;
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.Location = new System.Drawing.Point(8, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(29, 16);
			label1.TabIndex = 0;
			label1.Text = "Title:";
			textBoxTitle1.AcceptsReturn = false;
			textBoxTitle1.AcceptsTab = false;
			textBoxTitle1.AutoSize = true;
			textBoxTitle1.BackColor = System.Drawing.Color.White;
			textBoxTitle1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxTitle1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxTitle1.HideSelection = true;
			textBoxTitle1.IsComboTextBox = false;
			textBoxTitle1.Lines = new string[0];
			textBoxTitle1.Location = new System.Drawing.Point(64, 16);
			textBoxTitle1.MaxLength = 255;
			textBoxTitle1.Multiline = false;
			textBoxTitle1.Name = "textBoxTitle1";
			textBoxTitle1.PasswordChar = '\0';
			textBoxTitle1.ReadOnly = false;
			textBoxTitle1.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxTitle1.Size = new System.Drawing.Size(352, 20);
			textBoxTitle1.TabIndex = 0;
			textBoxTitle1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			textBoxTitle1.WordWrap = true;
			buttonOk.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOk.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOk.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOk.Location = new System.Drawing.Point(280, 96);
			buttonOk.Name = "buttonOk";
			buttonOk.Size = new System.Drawing.Size(64, 24);
			buttonOk.TabIndex = 3;
			buttonOk.Text = "&OK";
			buttonOk.Click += new System.EventHandler(buttonOk_Click);
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(352, 96);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(64, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			line2.BackColor = System.Drawing.Color.White;
			line2.DrawWidth = 1;
			line2.IsVertical = false;
			line2.LineBackColor = System.Drawing.Color.Black;
			line2.Location = new System.Drawing.Point(8, 88);
			line2.Name = "line2";
			line2.Size = new System.Drawing.Size(408, 1);
			line2.TabIndex = 132;
			line2.TabStop = false;
			buttonFont1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonFont1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonFont1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonFont1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonFont1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonFont1.Location = new System.Drawing.Point(312, 50);
			buttonFont1.Name = "buttonFont1";
			buttonFont1.Size = new System.Drawing.Size(104, 20);
			buttonFont1.TabIndex = 136;
			buttonFont1.Text = "Change font...";
			buttonFont1.Click += new System.EventHandler(buttonFont1_Click);
			base.AcceptButton = buttonOk;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(426, 127);
			base.Controls.Add(buttonFont1);
			base.Controls.Add(line2);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonOk);
			base.Controls.Add(textBoxTitle1);
			base.Controls.Add(label1);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			base.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChartTitleDialog";
			Text = "Change chart title";
			base.Load += new System.EventHandler(FilePickerForm_Load);
			ResumeLayout(false);
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void FilePickerForm_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void checkBoxPrint_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void checkBoxEmailAfterSaving_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void buttonFont1_Click(object sender, EventArgs e)
		{
			fontDialog1.Font = font1;
			fontDialog1.Color = fontColor;
			fontDialog1.ShowColor = true;
			if (fontDialog1.ShowDialog() == DialogResult.OK)
			{
				font1 = fontDialog1.Font;
				fontColor = fontDialog1.Color;
			}
			base.DialogResult = DialogResult.None;
		}
	}
}
