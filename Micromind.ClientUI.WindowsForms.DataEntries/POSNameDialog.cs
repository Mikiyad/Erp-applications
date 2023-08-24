using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries
{
	public class POSNameDialog : Form
	{
		private string sysDocID = "";

		private string voucherID = "";

		private IContainer components;

		private Button buttonCancel;

		private TextBox textBoxName;

		private Label label1;

		private Button buttonOK;

		private Line linePanelDown;

		public string EnteredName
		{
			get
			{
				return textBoxName.Text;
			}
			set
			{
				textBoxName.Text = value;
			}
		}

		public string SysDocID
		{
			get
			{
				return sysDocID;
			}
			set
			{
				sysDocID = value;
			}
		}

		public string VoucherID
		{
			get
			{
				return voucherID;
			}
			set
			{
				voucherID = value;
			}
		}

		public POSNameDialog()
		{
			InitializeComponent();
			base.StartPosition = FormStartPosition.CenterParent;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void EnterNameDialog_Activated(object sender, EventArgs e)
		{
			textBoxName.Focus();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.POSNameDialog));
			buttonCancel = new System.Windows.Forms.Button();
			textBoxName = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			buttonOK = new System.Windows.Forms.Button();
			linePanelDown = new Micromind.UISupport.Line();
			SuspendLayout();
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(279, 117);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(88, 25);
			buttonCancel.TabIndex = 0;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			textBoxName.Location = new System.Drawing.Point(60, 31);
			textBoxName.MaxLength = 15;
			textBoxName.Multiline = true;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(303, 37);
			textBoxName.TabIndex = 1;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 9.75f, System.Drawing.FontStyle.Bold);
			label1.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			label1.Location = new System.Drawing.Point(8, 42);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(48, 16);
			label1.TabIndex = 2;
			label1.Text = "Name:";
			buttonOK.Location = new System.Drawing.Point(186, 117);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(87, 25);
			buttonOK.TabIndex = 0;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(button2_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(-41, 110);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(505, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(227, 241, 254);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(375, 146);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxName);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "POSNameDialog";
			Text = "Enter Name";
			base.Activated += new System.EventHandler(EnterNameDialog_Activated);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
