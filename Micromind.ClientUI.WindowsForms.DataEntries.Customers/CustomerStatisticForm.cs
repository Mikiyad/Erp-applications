using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerStatisticForm : Form
	{
		private DataSet lastClosingData;

		private IContainer components;

		private Label label1;

		private Button buttonOK;

		private Line linePanelDown;

		private TextBox textBoxLastDate;

		private TextBox textBoxLastRemark;

		private TextBox textBox1;

		private Label label2;

		private TextBox textBox2;

		private Label label5;

		private Label label6;

		private TextBox textBox3;

		private Label label7;

		private TextBox textBox4;

		private Label label8;

		private TextBox textBox5;

		private Label label9;

		private TextBox textBox6;

		private Label label10;

		private TextBox textBox7;

		private Label label11;

		private UltraGroupBox ultraGroupBox1;

		private TextBox textBox16;

		private TextBox textBox13;

		private TextBox textBox10;

		private TextBox textBox15;

		private TextBox textBox12;

		private TextBox textBox9;

		private TextBox textBox14;

		private TextBox textBox11;

		private TextBox textBox8;

		private Label label15;

		private Label label14;

		private Label label13;

		private Label label12;

		private Label label4;

		private Label label3;

		private TextBox textBox17;

		private Label label16;

		private Label label17;

		private TextBox textBox18;

		private Label label18;

		private TextBox textBox19;

		public CustomerStatisticForm()
		{
			InitializeComponent();
			base.Load += ClosePeriodDialog_Load;
			base.StartPosition = FormStartPosition.CenterParent;
		}

		private void ClosePeriodDialog_Load(object sender, EventArgs e)
		{
			try
			{
				LoadLastClosing();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadLastClosing()
		{
			try
			{
				lastClosingData = Factory.CompanyInformationSystem.GetLastClosingPeriod();
				if (lastClosingData != null && lastClosingData.Tables.Count > 0 && lastClosingData.Tables[0].Rows.Count > 0)
				{
					textBoxLastDate.Text = DateTime.Parse(lastClosingData.Tables["Period"].Rows[0]["CloseDate"].ToString()).ToShortDateString();
					textBoxLastRemark.Text = lastClosingData.Tables["Period"].Rows[0]["Remarks"].ToString();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
		}

		private void ClosePeriodDialog_Activated(object sender, EventArgs e)
		{
		}

		private void buttonUnlock_Click(object sender, EventArgs e)
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("Unlocking period will allow users to post transactions in the period.\nAre you sure you want to unlock the selected period lock?") != DialogResult.No)
				{
					int num = -1;
					if (lastClosingData != null)
					{
						num = int.Parse(lastClosingData.Tables[0].Rows[0]["PeriodID"].ToString());
						if (Factory.CompanyInformationSystem.UnlockPeriod(num))
						{
							LoadLastClosing();
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.CustomerStatisticForm));
			label1 = new System.Windows.Forms.Label();
			buttonOK = new System.Windows.Forms.Button();
			textBoxLastDate = new System.Windows.Forms.TextBox();
			textBoxLastRemark = new System.Windows.Forms.TextBox();
			linePanelDown = new Micromind.UISupport.Line();
			textBox1 = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBox2 = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			textBox3 = new System.Windows.Forms.TextBox();
			label7 = new System.Windows.Forms.Label();
			textBox4 = new System.Windows.Forms.TextBox();
			label8 = new System.Windows.Forms.Label();
			textBox5 = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			textBox6 = new System.Windows.Forms.TextBox();
			label10 = new System.Windows.Forms.Label();
			textBox7 = new System.Windows.Forms.TextBox();
			label11 = new System.Windows.Forms.Label();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			label14 = new System.Windows.Forms.Label();
			label15 = new System.Windows.Forms.Label();
			textBox8 = new System.Windows.Forms.TextBox();
			textBox9 = new System.Windows.Forms.TextBox();
			textBox10 = new System.Windows.Forms.TextBox();
			textBox11 = new System.Windows.Forms.TextBox();
			textBox12 = new System.Windows.Forms.TextBox();
			textBox13 = new System.Windows.Forms.TextBox();
			textBox14 = new System.Windows.Forms.TextBox();
			textBox15 = new System.Windows.Forms.TextBox();
			textBox16 = new System.Windows.Forms.TextBox();
			textBox17 = new System.Windows.Forms.TextBox();
			label16 = new System.Windows.Forms.Label();
			label17 = new System.Windows.Forms.Label();
			textBox18 = new System.Windows.Forms.TextBox();
			label18 = new System.Windows.Forms.Label();
			textBox19 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 50);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(100, 13);
			label1.TabIndex = 2;
			label1.Text = "Last Payment Date:";
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(568, 394);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(87, 25);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(button2_Click);
			textBoxLastDate.Location = new System.Drawing.Point(131, 48);
			textBoxLastDate.Name = "textBoxLastDate";
			textBoxLastDate.ReadOnly = true;
			textBoxLastDate.Size = new System.Drawing.Size(135, 20);
			textBoxLastDate.TabIndex = 0;
			textBoxLastRemark.Location = new System.Drawing.Point(131, 74);
			textBoxLastRemark.MaxLength = 15;
			textBoxLastRemark.Name = "textBoxLastRemark";
			textBoxLastRemark.ReadOnly = true;
			textBoxLastRemark.Size = new System.Drawing.Size(132, 20);
			textBoxLastRemark.TabIndex = 20;
			linePanelDown.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(-16, 387);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(685, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			textBox1.Location = new System.Drawing.Point(131, 12);
			textBox1.Name = "textBox1";
			textBox1.ReadOnly = true;
			textBox1.Size = new System.Drawing.Size(135, 20);
			textBox1.TabIndex = 21;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 14);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(82, 13);
			label2.TabIndex = 22;
			label2.Text = "Customer Code:";
			textBox2.Location = new System.Drawing.Point(359, 13);
			textBox2.Name = "textBox2";
			textBox2.ReadOnly = true;
			textBox2.Size = new System.Drawing.Size(269, 20);
			textBox2.TabIndex = 23;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(274, 16);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(85, 13);
			label5.TabIndex = 24;
			label5.Text = "Customer Name:";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(12, 77);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(113, 13);
			label6.TabIndex = 2;
			label6.Text = "Last Payment Amount:";
			textBox3.Location = new System.Drawing.Point(374, 49);
			textBox3.Name = "textBox3";
			textBox3.ReadOnly = true;
			textBox3.Size = new System.Drawing.Size(152, 20);
			textBox3.TabIndex = 25;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(269, 51);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(98, 13);
			label7.TabIndex = 26;
			label7.Text = "Bounced Cheques:";
			textBox4.Location = new System.Drawing.Point(131, 106);
			textBox4.MaxLength = 15;
			textBox4.Name = "textBox4";
			textBox4.ReadOnly = true;
			textBox4.Size = new System.Drawing.Size(132, 20);
			textBox4.TabIndex = 28;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(12, 109);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(100, 13);
			label8.TabIndex = 27;
			label8.Text = "Avg Payment Days:";
			textBox5.Location = new System.Drawing.Point(131, 132);
			textBox5.MaxLength = 15;
			textBox5.Name = "textBox5";
			textBox5.ReadOnly = true;
			textBox5.Size = new System.Drawing.Size(132, 20);
			textBox5.TabIndex = 30;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(12, 135);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(88, 13);
			label9.TabIndex = 29;
			label9.Text = "Highest Balance:";
			textBox6.Location = new System.Drawing.Point(131, 158);
			textBox6.MaxLength = 15;
			textBox6.Name = "textBox6";
			textBox6.ReadOnly = true;
			textBox6.Size = new System.Drawing.Size(132, 20);
			textBox6.TabIndex = 32;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(12, 161);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(85, 13);
			label10.TabIndex = 31;
			label10.Text = "Last Sales Date:";
			textBox7.Location = new System.Drawing.Point(131, 184);
			textBox7.MaxLength = 15;
			textBox7.Name = "textBox7";
			textBox7.ReadOnly = true;
			textBox7.Size = new System.Drawing.Size(132, 20);
			textBox7.TabIndex = 34;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(12, 187);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(85, 13);
			label11.TabIndex = 33;
			label11.Text = "Last Sales Date:";
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderSolid;
			ultraGroupBox1.Controls.Add(textBox16);
			ultraGroupBox1.Controls.Add(textBox13);
			ultraGroupBox1.Controls.Add(textBox10);
			ultraGroupBox1.Controls.Add(textBox15);
			ultraGroupBox1.Controls.Add(textBox12);
			ultraGroupBox1.Controls.Add(textBox9);
			ultraGroupBox1.Controls.Add(textBox14);
			ultraGroupBox1.Controls.Add(textBox11);
			ultraGroupBox1.Controls.Add(textBox8);
			ultraGroupBox1.Controls.Add(label15);
			ultraGroupBox1.Controls.Add(label14);
			ultraGroupBox1.Controls.Add(label13);
			ultraGroupBox1.Controls.Add(label12);
			ultraGroupBox1.Controls.Add(label4);
			ultraGroupBox1.Controls.Add(label3);
			ultraGroupBox1.Location = new System.Drawing.Point(5, 220);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(623, 108);
			ultraGroupBox1.TabIndex = 35;
			ultraGroupBox1.Text = "Sales";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(4, 35);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(36, 13);
			label3.TabIndex = 34;
			label3.Text = "Sales:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(4, 60);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(37, 13);
			label4.TabIndex = 34;
			label4.Text = "COGS";
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(4, 84);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(31, 13);
			label12.TabIndex = 34;
			label12.Text = "Profit";
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(130, 22);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(67, 13);
			label13.TabIndex = 35;
			label13.Text = "Year to Date";
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(240, 22);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(73, 13);
			label14.TabIndex = 35;
			label14.Text = "Previous Year";
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(364, 22);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(49, 13);
			label15.TabIndex = 35;
			label15.Text = "All Dates";
			textBox8.Location = new System.Drawing.Point(126, 38);
			textBox8.MaxLength = 15;
			textBox8.Name = "textBox8";
			textBox8.ReadOnly = true;
			textBox8.Size = new System.Drawing.Size(99, 20);
			textBox8.TabIndex = 36;
			textBox9.Location = new System.Drawing.Point(231, 38);
			textBox9.MaxLength = 15;
			textBox9.Name = "textBox9";
			textBox9.ReadOnly = true;
			textBox9.Size = new System.Drawing.Size(99, 20);
			textBox9.TabIndex = 36;
			textBox10.Location = new System.Drawing.Point(336, 38);
			textBox10.MaxLength = 15;
			textBox10.Name = "textBox10";
			textBox10.ReadOnly = true;
			textBox10.Size = new System.Drawing.Size(99, 20);
			textBox10.TabIndex = 36;
			textBox11.Location = new System.Drawing.Point(126, 60);
			textBox11.MaxLength = 15;
			textBox11.Name = "textBox11";
			textBox11.ReadOnly = true;
			textBox11.Size = new System.Drawing.Size(99, 20);
			textBox11.TabIndex = 36;
			textBox12.Location = new System.Drawing.Point(231, 60);
			textBox12.MaxLength = 15;
			textBox12.Name = "textBox12";
			textBox12.ReadOnly = true;
			textBox12.Size = new System.Drawing.Size(99, 20);
			textBox12.TabIndex = 36;
			textBox13.Location = new System.Drawing.Point(336, 60);
			textBox13.MaxLength = 15;
			textBox13.Name = "textBox13";
			textBox13.ReadOnly = true;
			textBox13.Size = new System.Drawing.Size(99, 20);
			textBox13.TabIndex = 36;
			textBox14.Location = new System.Drawing.Point(126, 84);
			textBox14.MaxLength = 15;
			textBox14.Name = "textBox14";
			textBox14.ReadOnly = true;
			textBox14.Size = new System.Drawing.Size(99, 20);
			textBox14.TabIndex = 36;
			textBox15.Location = new System.Drawing.Point(231, 84);
			textBox15.MaxLength = 15;
			textBox15.Name = "textBox15";
			textBox15.ReadOnly = true;
			textBox15.Size = new System.Drawing.Size(99, 20);
			textBox15.TabIndex = 36;
			textBox16.Location = new System.Drawing.Point(336, 84);
			textBox16.MaxLength = 15;
			textBox16.Name = "textBox16";
			textBox16.ReadOnly = true;
			textBox16.Size = new System.Drawing.Size(99, 20);
			textBox16.TabIndex = 36;
			textBox17.Location = new System.Drawing.Point(374, 77);
			textBox17.Name = "textBox17";
			textBox17.ReadOnly = true;
			textBox17.Size = new System.Drawing.Size(152, 20);
			textBox17.TabIndex = 36;
			label16.AutoSize = true;
			label16.Location = new System.Drawing.Point(269, 79);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(86, 13);
			label16.TabIndex = 37;
			label16.Text = "Current Balance:";
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(269, 104);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(32, 13);
			label17.TabIndex = 37;
			label17.Text = "PDC:";
			textBox18.Location = new System.Drawing.Point(374, 102);
			textBox18.Name = "textBox18";
			textBox18.ReadOnly = true;
			textBox18.Size = new System.Drawing.Size(152, 20);
			textBox18.TabIndex = 36;
			label18.AutoSize = true;
			label18.Location = new System.Drawing.Point(269, 130);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(69, 13);
			label18.TabIndex = 37;
			label18.Text = "Net Balance:";
			textBox19.Location = new System.Drawing.Point(374, 128);
			textBox19.Name = "textBox19";
			textBox19.ReadOnly = true;
			textBox19.Size = new System.Drawing.Size(152, 20);
			textBox19.TabIndex = 36;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(669, 429);
			base.Controls.Add(textBox19);
			base.Controls.Add(label18);
			base.Controls.Add(textBox18);
			base.Controls.Add(label17);
			base.Controls.Add(textBox17);
			base.Controls.Add(label16);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(textBox7);
			base.Controls.Add(label11);
			base.Controls.Add(textBox6);
			base.Controls.Add(label10);
			base.Controls.Add(textBox5);
			base.Controls.Add(label9);
			base.Controls.Add(textBox4);
			base.Controls.Add(label8);
			base.Controls.Add(textBox3);
			base.Controls.Add(label7);
			base.Controls.Add(textBox2);
			base.Controls.Add(label5);
			base.Controls.Add(textBox1);
			base.Controls.Add(label2);
			base.Controls.Add(textBoxLastRemark);
			base.Controls.Add(textBoxLastDate);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(label6);
			base.Controls.Add(label1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomerStatisticForm";
			Text = "Customer Statistics";
			base.Activated += new System.EventHandler(ClosePeriodDialog_Activated);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
