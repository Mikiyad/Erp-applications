using DevExpress.XtraEditors;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class NumericKeypad : UserControl
	{
		private Control displayControl;

		private IContainer components;

		private SimpleButton simpleButton2;

		private SimpleButton simpleButton14;

		private SimpleButton simpleButton9;

		private SimpleButton simpleButton4;

		private SimpleButton simpleButton6;

		private SimpleButton simpleButton13;

		private SimpleButton simpleButton8;

		private SimpleButton simpleButton3;

		private SimpleButton simpleButton5;

		private SimpleButton buttonCal3;

		private SimpleButton buttonCal2;

		private SimpleButton buttonCal1;

		private SimpleButton simpleButton16;

		private SimpleButton simpleButton1;

		private SimpleButton simpleButton7;

		public Control DisplayControl
		{
			get
			{
				return displayControl;
			}
			set
			{
				displayControl = value;
			}
		}

		public NumericKeypad()
		{
			InitializeComponent();
		}

		private void buttonCal1_Click(object sender, EventArgs e)
		{
			_ = base.ActiveControl;
			SimpleButton simpleButton = sender as SimpleButton;
			if (displayControl == null)
			{
				return;
			}
			displayControl.Focus();
			if (displayControl.GetType().BaseType.BaseType == typeof(UltraGrid))
			{
				UltraGrid obj = displayControl as UltraGrid;
				obj.PerformAction(UltraGridAction.EnterEditMode);
				obj.ActiveCell.SelLength = 0;
			}
			string text = simpleButton.Text;
			if (text == "C")
			{
				displayControl.Text = "";
				displayControl.Focus();
				return;
			}
			if (text == "<-")
			{
				text = "{BS}";
			}
			if (text == "DEL")
			{
				text = "{DEL}";
			}
			if (text == "ENTER")
			{
				text = "{ENTER}";
			}
			SendKeys.Send(text);
			SendKeys.Flush();
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
			simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton14 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton9 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton13 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton8 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
			buttonCal3 = new DevExpress.XtraEditors.SimpleButton();
			buttonCal2 = new DevExpress.XtraEditors.SimpleButton();
			buttonCal1 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton16 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
			SuspendLayout();
			simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton2.Appearance.Options.UseFont = true;
			simpleButton2.Location = new System.Drawing.Point(146, 7);
			simpleButton2.Name = "simpleButton2";
			simpleButton2.Size = new System.Drawing.Size(43, 54);
			simpleButton2.TabIndex = 26;
			simpleButton2.TabStop = false;
			simpleButton2.Text = "C";
			simpleButton2.Click += new System.EventHandler(buttonCal1_Click);
			simpleButton14.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton14.Appearance.Options.UseFont = true;
			simpleButton14.Location = new System.Drawing.Point(100, 7);
			simpleButton14.Name = "simpleButton14";
			simpleButton14.Size = new System.Drawing.Size(43, 54);
			simpleButton14.TabIndex = 27;
			simpleButton14.TabStop = false;
			simpleButton14.Text = "9";
			simpleButton14.Click += new System.EventHandler(buttonCal1_Click);
			simpleButton9.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton9.Appearance.Options.UseFont = true;
			simpleButton9.Location = new System.Drawing.Point(54, 7);
			simpleButton9.Name = "simpleButton9";
			simpleButton9.Size = new System.Drawing.Size(43, 54);
			simpleButton9.TabIndex = 28;
			simpleButton9.TabStop = false;
			simpleButton9.Text = "8";
			simpleButton9.Click += new System.EventHandler(buttonCal1_Click);
			simpleButton4.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton4.Appearance.Options.UseFont = true;
			simpleButton4.Location = new System.Drawing.Point(8, 7);
			simpleButton4.Name = "simpleButton4";
			simpleButton4.Size = new System.Drawing.Size(43, 54);
			simpleButton4.TabIndex = 29;
			simpleButton4.TabStop = false;
			simpleButton4.Text = "7";
			simpleButton4.Click += new System.EventHandler(buttonCal1_Click);
			simpleButton6.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton6.Appearance.Options.UseFont = true;
			simpleButton6.Location = new System.Drawing.Point(146, 67);
			simpleButton6.Name = "simpleButton6";
			simpleButton6.Size = new System.Drawing.Size(43, 54);
			simpleButton6.TabIndex = 30;
			simpleButton6.TabStop = false;
			simpleButton6.Text = "<-";
			simpleButton6.Click += new System.EventHandler(buttonCal1_Click);
			simpleButton13.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton13.Appearance.Options.UseFont = true;
			simpleButton13.Location = new System.Drawing.Point(100, 67);
			simpleButton13.Name = "simpleButton13";
			simpleButton13.Size = new System.Drawing.Size(43, 54);
			simpleButton13.TabIndex = 31;
			simpleButton13.TabStop = false;
			simpleButton13.Text = "6";
			simpleButton13.Click += new System.EventHandler(buttonCal1_Click);
			simpleButton8.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton8.Appearance.Options.UseFont = true;
			simpleButton8.Location = new System.Drawing.Point(54, 67);
			simpleButton8.Name = "simpleButton8";
			simpleButton8.Size = new System.Drawing.Size(43, 54);
			simpleButton8.TabIndex = 32;
			simpleButton8.TabStop = false;
			simpleButton8.Text = "5";
			simpleButton8.Click += new System.EventHandler(buttonCal1_Click);
			simpleButton3.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton3.Appearance.Options.UseFont = true;
			simpleButton3.Location = new System.Drawing.Point(8, 67);
			simpleButton3.Name = "simpleButton3";
			simpleButton3.Size = new System.Drawing.Size(43, 54);
			simpleButton3.TabIndex = 33;
			simpleButton3.TabStop = false;
			simpleButton3.Text = "4";
			simpleButton3.Click += new System.EventHandler(buttonCal1_Click);
			simpleButton5.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton5.Appearance.Options.UseFont = true;
			simpleButton5.Location = new System.Drawing.Point(146, 128);
			simpleButton5.Name = "simpleButton5";
			simpleButton5.Size = new System.Drawing.Size(43, 54);
			simpleButton5.TabIndex = 34;
			simpleButton5.TabStop = false;
			simpleButton5.Text = "DEL";
			simpleButton5.Click += new System.EventHandler(buttonCal1_Click);
			buttonCal3.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCal3.Appearance.Options.UseFont = true;
			buttonCal3.Location = new System.Drawing.Point(100, 128);
			buttonCal3.Name = "buttonCal3";
			buttonCal3.Size = new System.Drawing.Size(43, 54);
			buttonCal3.TabIndex = 35;
			buttonCal3.TabStop = false;
			buttonCal3.Text = "3";
			buttonCal3.Click += new System.EventHandler(buttonCal1_Click);
			buttonCal2.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCal2.Appearance.Options.UseFont = true;
			buttonCal2.Location = new System.Drawing.Point(54, 127);
			buttonCal2.Name = "buttonCal2";
			buttonCal2.Size = new System.Drawing.Size(43, 54);
			buttonCal2.TabIndex = 36;
			buttonCal2.TabStop = false;
			buttonCal2.Text = "2";
			buttonCal2.Click += new System.EventHandler(buttonCal1_Click);
			buttonCal1.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			buttonCal1.Appearance.Options.UseFont = true;
			buttonCal1.Location = new System.Drawing.Point(8, 127);
			buttonCal1.Name = "buttonCal1";
			buttonCal1.Size = new System.Drawing.Size(43, 54);
			buttonCal1.TabIndex = 37;
			buttonCal1.TabStop = false;
			buttonCal1.Text = "1";
			buttonCal1.Click += new System.EventHandler(buttonCal1_Click);
			simpleButton16.Appearance.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold);
			simpleButton16.Appearance.Options.UseFont = true;
			simpleButton16.Location = new System.Drawing.Point(100, 185);
			simpleButton16.Name = "simpleButton16";
			simpleButton16.Size = new System.Drawing.Size(89, 53);
			simpleButton16.TabIndex = 38;
			simpleButton16.TabStop = false;
			simpleButton16.Text = "ENTER";
			simpleButton16.Click += new System.EventHandler(buttonCal1_Click);
			simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton1.Appearance.Options.UseFont = true;
			simpleButton1.Location = new System.Drawing.Point(8, 184);
			simpleButton1.Name = "simpleButton1";
			simpleButton1.Size = new System.Drawing.Size(43, 54);
			simpleButton1.TabIndex = 39;
			simpleButton1.TabStop = false;
			simpleButton1.Text = "0";
			simpleButton1.Click += new System.EventHandler(buttonCal1_Click);
			simpleButton7.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButton7.Appearance.Options.UseFont = true;
			simpleButton7.Location = new System.Drawing.Point(54, 184);
			simpleButton7.Name = "simpleButton7";
			simpleButton7.Size = new System.Drawing.Size(43, 54);
			simpleButton7.TabIndex = 40;
			simpleButton7.TabStop = false;
			simpleButton7.Text = ".";
			simpleButton7.Click += new System.EventHandler(buttonCal1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(simpleButton7);
			base.Controls.Add(simpleButton2);
			base.Controls.Add(simpleButton14);
			base.Controls.Add(simpleButton9);
			base.Controls.Add(simpleButton4);
			base.Controls.Add(simpleButton6);
			base.Controls.Add(simpleButton13);
			base.Controls.Add(simpleButton8);
			base.Controls.Add(simpleButton3);
			base.Controls.Add(simpleButton5);
			base.Controls.Add(buttonCal3);
			base.Controls.Add(buttonCal2);
			base.Controls.Add(buttonCal1);
			base.Controls.Add(simpleButton16);
			base.Controls.Add(simpleButton1);
			MaximumSize = new System.Drawing.Size(197, 244);
			MinimumSize = new System.Drawing.Size(197, 244);
			base.Name = "NumericKeypad";
			base.Size = new System.Drawing.Size(197, 244);
			ResumeLayout(false);
		}
	}
}
