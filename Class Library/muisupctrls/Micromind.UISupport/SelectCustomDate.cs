using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class SelectCustomDate : DialogBoxBaseForm
	{
		private DateRange dateRange = new DateRange("Custom Date...", DateTime.Today, DateTime.Today);

		private bool isLoaded;

		private flatDatePicker textBoxTo;

		private flatDatePicker textBoxFrom;

		private Label labelTo;

		private Label labelFrom;

		private Label labelDates;

		private Label label1;

		private Label label2;

		private Line line1;

		private XPButton buttonOK;

		private XPButton buttonCancel;

		private Label label3;

		private Container components;

		public SelectCustomDate()
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

		public DateRange GetRange(Form owner, DateTime from, DateTime to)
		{
			try
			{
				textBoxFrom.Value = from;
				textBoxTo.Value = to;
				dateRange.From = from;
				dateRange.To = to;
			}
			catch
			{
				textBoxFrom.Value = DateTime.Today;
				textBoxTo.Value = DateTime.Today;
				dateRange.From = DateTime.Today;
				dateRange.To = DateTime.Today;
			}
			ShowDialog(owner);
			return dateRange;
		}

		private void InitializeComponent()
		{
			textBoxTo = new Micromind.UISupport.flatDatePicker();
			textBoxFrom = new Micromind.UISupport.flatDatePicker();
			labelTo = new System.Windows.Forms.Label();
			labelFrom = new System.Windows.Forms.Label();
			labelDates = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			line1 = new Micromind.UISupport.Line();
			buttonOK = new Micromind.UISupport.XPButton();
			buttonCancel = new Micromind.UISupport.XPButton();
			label3 = new System.Windows.Forms.Label();
			SuspendLayout();
			textBoxTo.Location = new System.Drawing.Point(192, 48);
			textBoxTo.Name = "textBoxTo";
			textBoxTo.Size = new System.Drawing.Size(120, 20);
			textBoxTo.TabIndex = 8;
			textBoxTo.Value = new System.DateTime(2004, 4, 7, 13, 38, 24, 0);
			textBoxFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			textBoxFrom.Location = new System.Drawing.Point(40, 48);
			textBoxFrom.Name = "textBoxFrom";
			textBoxFrom.Size = new System.Drawing.Size(120, 20);
			textBoxFrom.TabIndex = 7;
			textBoxFrom.Value = new System.DateTime(2004, 4, 7, 13, 38, 24, 0);
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(168, 48);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(21, 16);
			labelTo.TabIndex = 10;
			labelTo.Text = "To:";
			labelFrom.AutoSize = true;
			labelFrom.Location = new System.Drawing.Point(8, 48);
			labelFrom.Name = "labelFrom";
			labelFrom.Size = new System.Drawing.Size(34, 16);
			labelFrom.TabIndex = 9;
			labelFrom.Text = "From:";
			labelDates.AutoSize = true;
			labelDates.Location = new System.Drawing.Point(336, 16);
			labelDates.Name = "labelDates";
			labelDates.Size = new System.Drawing.Size(37, 16);
			labelDates.TabIndex = 2;
			labelDates.Text = "Dates:";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(336, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(37, 16);
			label1.TabIndex = 2;
			label1.Text = "Dates:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(336, 16);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(37, 16);
			label2.TabIndex = 2;
			label2.Text = "Dates:";
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(0, 96);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(336, 1);
			line1.TabIndex = 13;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.BackColor = System.Drawing.Color.Silver;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.Location = new System.Drawing.Point(176, 104);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(65, 22);
			buttonOK.TabIndex = 15;
			buttonOK.Text = "&OK";
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BackColor = System.Drawing.Color.Silver;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(248, 104);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(65, 22);
			buttonCancel.TabIndex = 14;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(8, 16);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(190, 19);
			label3.TabIndex = 16;
			label3.Text = "Please select the date range:";
			base.AcceptButton = buttonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(320, 130);
			base.Controls.Add(label3);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(line1);
			base.Controls.Add(textBoxTo);
			base.Controls.Add(textBoxFrom);
			base.Controls.Add(labelTo);
			base.Controls.Add(labelFrom);
			base.Controls.Add(labelDates);
			base.Controls.Add(label2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SelectCustomDate";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Date Range";
			base.Load += new System.EventHandler(SelectDateRange_Load);
			base.Activated += new System.EventHandler(SelectDateRange_Activated);
			ResumeLayout(false);
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			dateRange.From = DateTime.MaxValue;
			dateRange.To = DateTime.MaxValue;
			Close();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			dateRange.From = textBoxFrom.Value;
			dateRange.To = textBoxTo.Value;
			Close();
		}

		private void SelectDateRange_Activated(object sender, EventArgs e)
		{
			if (!isLoaded)
			{
				try
				{
					textBoxFrom.Value = dateRange.From;
					textBoxTo.Value = dateRange.To;
				}
				catch
				{
					textBoxFrom.Value = DateTime.Today;
					textBoxTo.Value = DateTime.Today;
				}
				isLoaded = true;
			}
		}

		private void SelectDateRange_Load(object sender, EventArgs e)
		{
			InitDialog();
		}
	}
}
