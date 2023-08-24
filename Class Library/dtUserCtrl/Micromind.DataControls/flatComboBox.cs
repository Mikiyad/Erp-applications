using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class flatComboBox : ComboBox
	{
		private bool isRequired;

		private ToolTip toolTip = new ToolTip();

		protected Color bColor;

		private Keys receivedKey;

		private int dropDownWidth = 100;

		private bool textChangedFlag;

		protected XPButton button;

		protected MMTextBox textBox;

		protected ComboBox comboBox;

		private ImageList imageList1;

		private IContainer components;

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

		public ComboBox List => comboBox;

		public new bool Sorted
		{
			get
			{
				return comboBox.Sorted;
			}
			set
			{
				comboBox.Sorted = value;
			}
		}

		public override string Text
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

		public int DropWidth
		{
			get
			{
				return dropDownWidth;
			}
			set
			{
				dropDownWidth = value;
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
					if (textBox.Enabled)
					{
						bColor = textBox.BackColor;
					}
					textBox.BackColor = SystemColors.Control;
				}
				textBox.Enabled = value;
				button.Enabled = value;
				comboBox.Enabled = value;
			}
		}

		public event EventHandler OnItemSelected;

		public event EventHandler DataRequesting;

		public flatComboBox()
		{
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
			bColor = textBox.BackColor;
			button.Width = 18;
			button.Height = 18;
			button.Top = 1;
			base.SizeChanged += OnResize;
			textBox.LostFocus += OntextBox_LostFocus;
			textBox.IsComboTextBox = true;
		}

		protected void OnResize(object o, EventArgs e)
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
			System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(Micromind.DataControls.flatComboBox));
			button = new Micromind.UISupport.XPButton();
			imageList1 = new System.Windows.Forms.ImageList(components);
			textBox = new Micromind.UISupport.MMTextBox();
			comboBox = new System.Windows.Forms.ComboBox();
			SuspendLayout();
			button.AdjustImageLocation = new System.Drawing.Point(0, 0);
			button.BackColor = System.Drawing.Color.Silver;
			button.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			button.BtnStyle = Micromind.UISupport.XPStyle.Default;
			button.Font = new System.Drawing.Font("Webdings", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 2);
			button.ImageIndex = 0;
			button.ImageList = imageList1;
			button.Location = new System.Drawing.Point(208, 1);
			button.Name = "button";
			button.Size = new System.Drawing.Size(20, 20);
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
			textBox.TabIndex = 1;
			textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			textBox.WordWrap = true;
			textBox.Enter += new System.EventHandler(textBox_Enter);
			textBox.TextChanged += new System.EventHandler(textBox_TextChanged);
			textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(OntextBox_KeyDown);
			comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBox.ItemHeight = 13;
			comboBox.Location = new System.Drawing.Point(0, 0);
			comboBox.Name = "comboBox";
			comboBox.Size = new System.Drawing.Size(136, 21);
			comboBox.TabIndex = 2;
			comboBox.TabStop = false;
			comboBox.SelectedIndexChanged += new System.EventHandler(comboBox_SelectedIndexChanged);
			base.Controls.Add(button);
			base.Controls.Add(textBox);
			base.Controls.Add(comboBox);
			base.Name = "flatComboBox";
			base.Size = new System.Drawing.Size(264, 32);
			base.Enter += new System.EventHandler(GotFocus);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(OnKeyDown);
			ResumeLayout(false);
		}

		protected void button_Click(object sender, EventArgs e)
		{
			DropDown();
		}

		protected void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox.SelectedIndex < 0)
			{
				textBox.Text = "";
				return;
			}
			textBox.Text = comboBox.Text.Trim();
			textBox.Select(0, 0);
			if (this.OnItemSelected != null)
			{
				this.OnItemSelected(null, null);
			}
		}

		public new void DropDown()
		{
			if (this.DataRequesting != null)
			{
				this.DataRequesting(this, null);
			}
			comboBox.DropDownWidth = dropDownWidth;
			comboBox.Focus();
			SendKeys.Flush();
			SendKeys.Send("{f4}");
		}

		protected void textBox_TextChanged(object sender, EventArgs e)
		{
			if (textChangedFlag)
			{
				return;
			}
			string text = textBox.Text;
			if (receivedKey == Keys.Delete || receivedKey == Keys.Back)
			{
				toolTip.SetToolTip(textBox, "");
				toolTip.RemoveAll();
				toolTip.Active = false;
				return;
			}
			if (text == "")
			{
				toolTip.SetToolTip(textBox, "");
				toolTip.RemoveAll();
				toolTip.Active = false;
				return;
			}
			int num = comboBox.FindString(text);
			if (num >= 0)
			{
				textBox.Text = comboBox.Items[num].ToString().Trim();
				textBox.Focus();
				textBox.Select(text.Length, textBox.Text.Length);
			}
			try
			{
				if (textBox.TextLength > 0)
				{
					toolTip.SetToolTip(textBox, textBox.Text);
					toolTip.Active = true;
				}
				else
				{
					toolTip.SetToolTip(textBox, "");
					toolTip.Active = false;
				}
			}
			catch
			{
			}
		}

		protected void OntextBox_KeyDown(object o, KeyEventArgs e)
		{
			receivedKey = e.KeyCode;
			if (this.DataRequesting != null)
			{
				this.DataRequesting(this, null);
			}
			if (e.KeyCode != Keys.Down && e.KeyCode == Keys.Up)
			{
				try
				{
					if (comboBox.SelectedIndex == -1)
					{
						comboBox.SelectedItem = comboBox.Items[comboBox.Items.Count - 1];
					}
					else
					{
						comboBox.SelectedItem = comboBox.Items[comboBox.SelectedIndex - 1];
					}
				}
				catch
				{
				}
			}
		}

		protected void flatComboBox_Load(object sender, EventArgs e)
		{
			Init();
		}

		protected void Init()
		{
			base.Width = textBox.Width + button.Width + 1;
			base.Height = textBox.Height;
			base.Paint += ControlPaint;
		}

		private void ControlPaint(object o, PaintEventArgs e)
		{
			OnPaintBackground(e);
			Graphics graphics = e.Graphics;
			graphics.Clear(textBox.BackColor);
			Pen pen = new Pen(new SolidBrush(Color.Blue), 1f);
			Rectangle rect = new Rectangle(button.Location, button.Size);
			rect.Y--;
			rect.Height++;
			graphics.DrawRectangle(pen, rect);
			_ = textBox.Focused;
		}

		protected void OntextBox_LostFocus(object sender, EventArgs e)
		{
			int num = comboBox.FindStringExact(textBox.Text);
			if (num >= 0)
			{
				comboBox.SelectedIndex = num;
				textBox.Select(0, 0);
			}
			if (textBox.Text.Trim() == "")
			{
				comboBox.SelectedIndex = -1;
			}
		}

		private void OnKeyDown(object sender, KeyEventArgs e)
		{
		}

		private new void GotFocus(object sender, EventArgs e)
		{
			textBox.Focus();
		}

		private void textBox_Enter(object sender, EventArgs e)
		{
			textBox.SelectAll();
		}

		private void button_MouseEnter(object sender, EventArgs e)
		{
			button.ImageIndex = 1;
		}

		private void button_MouseLeave(object sender, EventArgs e)
		{
			button.ImageIndex = 0;
		}
	}
}
