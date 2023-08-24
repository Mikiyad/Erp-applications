using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Test
{
	public class DataAccuracyTestForm : Form
	{
		private IContainer components;

		private Button button1;

		private TextBox textBox1;

		private ListView listView1;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeaderID;

		private ColumnHeader columnHeaderDescription;

		public DataAccuracyTestForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
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
			System.Windows.Forms.ListViewItem listViewItem = new System.Windows.Forms.ListViewItem(new string[4]
			{
				"",
				"Product Lot Test",
				"Product Lot Test",
				"fsdfsdf"
			}, -1);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[2]
			{
				"",
				"Test 2"
			}, -1);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
			button1 = new System.Windows.Forms.Button();
			textBox1 = new System.Windows.Forms.TextBox();
			listView1 = new System.Windows.Forms.ListView();
			columnHeader1 = new System.Windows.Forms.ColumnHeader();
			columnHeaderID = new System.Windows.Forms.ColumnHeader();
			columnHeaderDescription = new System.Windows.Forms.ColumnHeader();
			SuspendLayout();
			button1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			button1.Location = new System.Drawing.Point(30, 641);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(154, 39);
			button1.TabIndex = 0;
			button1.Text = "Start Test";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			textBox1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBox1.Location = new System.Drawing.Point(483, 43);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(743, 565);
			textBox1.TabIndex = 1;
			listView1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3]
			{
				columnHeader1,
				columnHeaderID,
				columnHeaderDescription
			});
			listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[3]
			{
				listViewItem,
				listViewItem2,
				listViewItem3
			});
			listView1.Location = new System.Drawing.Point(3, 43);
			listView1.Name = "listView1";
			listView1.Size = new System.Drawing.Size(474, 565);
			listView1.TabIndex = 3;
			listView1.UseCompatibleStateImageBehavior = false;
			listView1.View = System.Windows.Forms.View.Details;
			columnHeader1.Text = "C";
			columnHeader1.Width = 23;
			columnHeaderID.Text = "Code";
			columnHeaderID.Width = 98;
			columnHeaderDescription.Text = "Description";
			columnHeaderDescription.Width = 343;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1238, 705);
			base.Controls.Add(listView1);
			base.Controls.Add(textBox1);
			base.Controls.Add(button1);
			base.Name = "DataAccuracyTestForm";
			Text = "DataAccuracyTestForm";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
