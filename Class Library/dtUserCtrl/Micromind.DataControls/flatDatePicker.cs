using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	internal class flatDatePicker : UserControl, ICustomReportControl
	{
		private bool isRequired;

		private Color bColor;

		private XPButton button;

		private MMTextBox textBox;

		private DateTimePicker comboBox;

		private ImageList imageList1;

		private IContainer components;

		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		public string CustomReportFieldName
		{
			get
			{
				return crFieldName;
			}
			set
			{
				crFieldName = value;
			}
		}

		public string CustomReportKey
		{
			get
			{
				return crKey;
			}
			set
			{
				crKey = value;
			}
		}

		public byte CustomReportValueType
		{
			get
			{
				return crValueType;
			}
			set
			{
				crValueType = value;
			}
		}

		public DateTime Value
		{
			get
			{
				return new DateTime(comboBox.Value.Year, comboBox.Value.Month, comboBox.Value.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 0);
			}
			set
			{
				comboBox.Value = value;
				textBox.Text = value.ToShortDateString();
			}
		}

		[Description("Determines if the filed is required.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		[Browsable(true)]
		public bool IsRequired
		{
			get
			{
				return isRequired;
			}
			set
			{
				isRequired = value;
			}
		}

		public DateTimePicker DateSystem => comboBox;

		public new string Text
		{
			get
			{
				return textBox.Text;
			}
			set
			{
				textBox.Text = value;
			}
		}

		public new Color ForeColor
		{
			get
			{
				return textBox.ForeColor;
			}
			set
			{
				textBox.ForeColor = value;
				comboBox.ForeColor = value;
			}
		}

		public new Color BackColor
		{
			get
			{
				return textBox.BackColor;
			}
			set
			{
				textBox.BackColor = value;
				comboBox.BackColor = value;
			}
		}

		public Color ButtonForeColor
		{
			get
			{
				return button.ForeColor;
			}
			set
			{
				button.ForeColor = value;
			}
		}

		public Color ButtonBackColor
		{
			get
			{
				return button.BackColor;
			}
			set
			{
				button.BackColor = value;
			}
		}

		public bool Editable
		{
			get
			{
				if (textBox.ReadOnly)
				{
					return false;
				}
				return true;
			}
			set
			{
				if (value)
				{
					textBox.ReadOnly = false;
				}
				else
				{
					textBox.ReadOnly = true;
				}
			}
		}

		public new bool Enabled
		{
			get
			{
				return textBox.Enabled;
			}
			set
			{
				if (value)
				{
					textBox.BackColor = bColor;
				}
				else
				{
					bColor = textBox.BackColor;
					textBox.BackColor = Color.WhiteSmoke;
				}
				textBox.Enabled = value;
				button.Enabled = value;
				comboBox.Enabled = value;
			}
		}

		public static DateTime LongDBDateTimeMin => SqlDateTime.MinValue.Value;

		public static DateTime LongDBDateTimeMax => SqlDateTime.MaxValue.Value;

		public flatDatePicker()
		{
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
			base.SizeChanged += OnResize;
			base.Leave += OnControl_Leave;
			comboBox.Value = DateTime.Now;
			textBox.Text = comboBox.Value.ToShortDateString();
			base.Width = textBox.Width + button.Width + 1;
			base.Height = textBox.Height;
			base.Paint += ControlPaint;
		}

		public string GetParameterValue()
		{
			string text = StoreConfiguration.ToSqlDateTimeString(new DateTime(Value.Year, Value.Month, Value.Day, 0, 0, 0, 0));
			if (crValueType == 1)
			{
				return "'" + text + "'";
			}
			if (crFieldName == "")
			{
				return "''=''";
			}
			return crFieldName + " = '" + text + "'";
		}

		private void OnControl_Leave(object o, EventArgs e)
		{
			try
			{
				if (!(textBox.Text.Trim() == ""))
				{
					DateTime value = DateTime.Parse(textBox.Text);
					comboBox.Value = value;
				}
			}
			catch
			{
				MessageBox.Show("'" + textBox.Text + "' is not a valid date.Please enter a valid date.", "Date", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				textBox.Focus();
				textBox.SelectAll();
			}
		}

		private void OnResize(object o, EventArgs e)
		{
			button.Top = 1;
			textBox.Width = base.Width - button.Width - 1;
			button.Left = textBox.Width;
			comboBox.Width = textBox.Width;
			base.Height = textBox.Height;
			Invalidate();
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
			components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(Micromind.DataControls.flatDatePicker));
			button = new Micromind.UISupport.XPButton();
			imageList1 = new System.Windows.Forms.ImageList(components);
			textBox = new Micromind.UISupport.MMTextBox();
			comboBox = new System.Windows.Forms.DateTimePicker();
			SuspendLayout();
			button.AdjustImageLocation = new System.Drawing.Point(0, 0);
			button.BackColor = System.Drawing.Color.Silver;
			button.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			button.BtnStyle = Micromind.UISupport.XPStyle.Default;
			button.Font = new System.Drawing.Font("Webdings", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 2);
			button.ImageIndex = 0;
			button.ImageList = imageList1;
			button.Location = new System.Drawing.Point(208, 0);
			button.Name = "button";
			button.Size = new System.Drawing.Size(18, 18);
			button.TabIndex = 0;
			button.TabStop = false;
			button.Click += new System.EventHandler(button_Click);
			button.MouseEnter += new System.EventHandler(button_MouseEnter);
			button.MouseLeave += new System.EventHandler(button_MouseLeave);
			imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			imageList1.ImageSize = new System.Drawing.Size(18, 18);
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			textBox.AcceptsReturn = false;
			textBox.AcceptsTab = false;
			textBox.AutoSize = true;
			textBox.BackColor = System.Drawing.Color.White;
			textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBox.HideSelection = true;
			textBox.IsComboTextBox = false;
			textBox.Lines = new string[0];
			textBox.Location = new System.Drawing.Point(0, 0);
			textBox.MaxLength = 32767;
			textBox.Multiline = false;
			textBox.Name = "textBox";
			textBox.PasswordChar = '\0';
			textBox.ReadOnly = false;
			textBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBox.Size = new System.Drawing.Size(208, 20);
			textBox.TabIndex = 8;
			textBox.TabStop = false;
			textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			textBox.WordWrap = true;
			textBox.TextChanged += new System.EventHandler(textBox_TextChanged);
			textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(OnKeyDown);
			comboBox.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			comboBox.Location = new System.Drawing.Point(0, 0);
			comboBox.MaxDate = new System.DateTime(2079, 6, 6, 0, 0, 0, 0);
			comboBox.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
			comboBox.Name = "comboBox";
			comboBox.Size = new System.Drawing.Size(224, 20);
			comboBox.TabIndex = 2;
			comboBox.TabStop = false;
			comboBox.ValueChanged += new System.EventHandler(comboBox_ValueChanged);
			base.Controls.Add(textBox);
			base.Controls.Add(button);
			base.Controls.Add(comboBox);
			base.Name = "flatDatePicker";
			base.Size = new System.Drawing.Size(232, 24);
			base.Load += new System.EventHandler(flatComboBox_Load);
			base.Validating += new System.ComponentModel.CancelEventHandler(flatDatePicker_Validating);
			base.Enter += new System.EventHandler(GotFocus);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(OnKeyDown);
			ResumeLayout(false);
		}

		private void button_Click(object sender, EventArgs e)
		{
			comboBox.Focus();
			SendKeys.Send("{f4}");
		}

		public void DropDown()
		{
			comboBox.Focus();
			SendKeys.Send("{f4}");
		}

		private void FlatComboBox_Load(object sender, EventArgs e)
		{
			base.Width = textBox.Width + -button.Width;
			base.Height = textBox.Height;
		}

		private void flatComboBox_Load(object sender, EventArgs e)
		{
		}

		private void comboBox_ValueChanged(object sender, EventArgs e)
		{
			textBox.Text = comboBox.Value.ToShortDateString();
		}

		private new void GotFocus(object sender, EventArgs e)
		{
			textBox.Focus();
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
			{
				comboBox.Value = comboBox.Value.AddDays(-1.0);
			}
			else if (e.KeyCode == Keys.Up)
			{
				comboBox.Value = comboBox.Value.AddDays(1.0);
			}
		}

		private void textBox_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (!textBox.Focused)
				{
					DateTime value = DateTime.Parse(textBox.Text);
					comboBox.Value = value;
				}
			}
			catch
			{
			}
		}

		private void button_MouseEnter(object sender, EventArgs e)
		{
			button.ImageIndex = 1;
		}

		private void button_MouseLeave(object sender, EventArgs e)
		{
			button.ImageIndex = 0;
		}

		private void ControlPaint(object o, PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.Clear(textBox.BackColor);
			Pen pen = new Pen(new SolidBrush(Color.Blue), 1f);
			Rectangle rect = new Rectangle(button.Location, button.Size);
			rect.Y--;
			rect.Height++;
			graphics.DrawRectangle(pen, rect);
			_ = textBox.Focused;
		}

		private void flatDatePicker_Validating(object sender, CancelEventArgs e)
		{
			if (!(textBox.Text.Trim() == string.Empty))
			{
				try
				{
					DateTime t = DateTime.Parse(textBox.Text);
					if (t > LongDBDateTimeMax)
					{
						MessageBox.Show("'" + textBox.Text + "' is not a valid date.Please enter a valid date.\nDate must be between '" + LongDBDateTimeMin.ToShortDateString() + "' and '" + LongDBDateTimeMax.ToShortDateString() + "'", "Date", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						e.Cancel = true;
						textBox.Focus();
						textBox.SelectAll();
					}
					if (t < LongDBDateTimeMin)
					{
						MessageBox.Show("'" + textBox.Text + "' is not a valid date.Please enter a valid date.\nDate must be between '" + LongDBDateTimeMin.ToShortDateString() + "' and '" + LongDBDateTimeMax.ToShortDateString() + "'", "Date", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						e.Cancel = true;
						textBox.Focus();
						textBox.SelectAll();
					}
				}
				catch
				{
					MessageBox.Show("'" + textBox.Text + "' is not a valid date.Please enter a valid date.\nDate must be between '" + LongDBDateTimeMin.ToShortDateString() + "' and '" + LongDBDateTimeMax.ToShortDateString() + "'", "Date", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					e.Cancel = true;
					textBox.Focus();
					textBox.SelectAll();
				}
			}
		}
	}
}
