using Micromind.ClientUI.Properties;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries
{
	public class FlagNameDialog : Form
	{
		private IContainer components;

		private TextBox textBoxName;

		private Line linePanelDown;

		private Label label6;

		private Label label5;

		private Label label4;

		private Label label3;

		private Label label2;

		private Label label1;

		private TextBox textBoxRed;

		private TextBox textBoxOrange;

		private TextBox textBoxPurple;

		private TextBox textBoxGreen;

		private TextBox textBoxYellow;

		private TextBox textBoxBlue;

		private Button buttonOK;

		private Button buttonCancel;

		public string RedName
		{
			get
			{
				return textBoxRed.Text;
			}
			set
			{
				textBoxRed.Text = value;
			}
		}

		public string BlueName
		{
			get
			{
				return textBoxBlue.Text;
			}
			set
			{
				textBoxBlue.Text = value;
			}
		}

		public string OrangeName
		{
			get
			{
				return textBoxOrange.Text;
			}
			set
			{
				textBoxOrange.Text = value;
			}
		}

		public string GreenName
		{
			get
			{
				return textBoxGreen.Text;
			}
			set
			{
				textBoxGreen.Text = value;
			}
		}

		public string PurpleName
		{
			get
			{
				return textBoxPurple.Text;
			}
			set
			{
				textBoxPurple.Text = value;
			}
		}

		public string YellowName
		{
			get
			{
				return textBoxYellow.Text;
			}
			set
			{
				textBoxYellow.Text = value;
			}
		}

		public FlagNameDialog()
		{
			InitializeComponent();
			base.StartPosition = FormStartPosition.CenterParent;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void FlagNameDialog_Activated(object sender, EventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.FlagNameDialog));
			linePanelDown = new Micromind.UISupport.Line();
			label6 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			textBoxRed = new System.Windows.Forms.TextBox();
			textBoxOrange = new System.Windows.Forms.TextBox();
			textBoxPurple = new System.Windows.Forms.TextBox();
			textBoxGreen = new System.Windows.Forms.TextBox();
			textBoxYellow = new System.Windows.Forms.TextBox();
			textBoxBlue = new System.Windows.Forms.TextBox();
			buttonOK = new System.Windows.Forms.Button();
			buttonCancel = new System.Windows.Forms.Button();
			SuspendLayout();
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(-33, 171);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(267, 1);
			linePanelDown.TabIndex = 30;
			linePanelDown.TabStop = false;
			label6.Image = Micromind.ClientUI.Properties.Resources.flagred;
			label6.Location = new System.Drawing.Point(8, 105);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(20, 20);
			label6.TabIndex = 24;
			label5.Image = Micromind.ClientUI.Properties.Resources.flagpurple;
			label5.Location = new System.Drawing.Point(8, 81);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(20, 20);
			label5.TabIndex = 25;
			label4.Image = Micromind.ClientUI.Properties.Resources.flagorange;
			label4.Location = new System.Drawing.Point(8, 57);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(20, 20);
			label4.TabIndex = 26;
			label3.Image = Micromind.ClientUI.Properties.Resources.flaghyellow;
			label3.Location = new System.Drawing.Point(8, 33);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(20, 20);
			label3.TabIndex = 27;
			label2.Image = Micromind.ClientUI.Properties.Resources.flaggreen;
			label2.Location = new System.Drawing.Point(8, 9);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(20, 20);
			label2.TabIndex = 28;
			label1.Image = Micromind.ClientUI.Properties.Resources.flagblue;
			label1.Location = new System.Drawing.Point(8, 129);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(20, 20);
			label1.TabIndex = 29;
			textBoxRed.Location = new System.Drawing.Point(57, 105);
			textBoxRed.MaxLength = 15;
			textBoxRed.Name = "textBoxRed";
			textBoxRed.Size = new System.Drawing.Size(148, 20);
			textBoxRed.TabIndex = 4;
			textBoxOrange.Location = new System.Drawing.Point(57, 57);
			textBoxOrange.MaxLength = 15;
			textBoxOrange.Name = "textBoxOrange";
			textBoxOrange.Size = new System.Drawing.Size(148, 20);
			textBoxOrange.TabIndex = 2;
			textBoxPurple.Location = new System.Drawing.Point(57, 81);
			textBoxPurple.MaxLength = 15;
			textBoxPurple.Name = "textBoxPurple";
			textBoxPurple.Size = new System.Drawing.Size(148, 20);
			textBoxPurple.TabIndex = 3;
			textBoxGreen.Location = new System.Drawing.Point(57, 9);
			textBoxGreen.MaxLength = 15;
			textBoxGreen.Name = "textBoxGreen";
			textBoxGreen.Size = new System.Drawing.Size(148, 20);
			textBoxGreen.TabIndex = 0;
			textBoxYellow.Location = new System.Drawing.Point(57, 33);
			textBoxYellow.MaxLength = 15;
			textBoxYellow.Name = "textBoxYellow";
			textBoxYellow.Size = new System.Drawing.Size(148, 20);
			textBoxYellow.TabIndex = 1;
			textBoxBlue.Location = new System.Drawing.Point(57, 129);
			textBoxBlue.MaxLength = 15;
			textBoxBlue.Name = "textBoxBlue";
			textBoxBlue.Size = new System.Drawing.Size(148, 20);
			textBoxBlue.TabIndex = 5;
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(45, 181);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(80, 25);
			buttonOK.TabIndex = 6;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(131, 181);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(74, 25);
			buttonCancel.TabIndex = 7;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(214, 214);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(label6);
			base.Controls.Add(label5);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxRed);
			base.Controls.Add(textBoxOrange);
			base.Controls.Add(textBoxPurple);
			base.Controls.Add(textBoxGreen);
			base.Controls.Add(textBoxYellow);
			base.Controls.Add(textBoxBlue);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FlagNameDialog";
			Text = "Flags";
			base.Activated += new System.EventHandler(FlagNameDialog_Activated);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
