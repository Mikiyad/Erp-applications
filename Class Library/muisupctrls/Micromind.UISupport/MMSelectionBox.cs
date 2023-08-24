using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class MMSelectionBox : TextBox, ICustomReportControl, ICustomReportControlFeatured
	{
		private static bool simulateEnterAsTab;

		private bool showBorder = true;

		private bool isRequired;

		private string originalText = "";

		private Button buttonSelect = new Button();

		private Label labelhidden = new Label();

		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private string reportID = "";

		private bool isComboTextBox;

		public bool IsModified
		{
			get;
			set;
		}

		public new bool Modified
		{
			get;
			set;
		}

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

		public string TextValue
		{
			get
			{
				return Text;
			}
			set
			{
				Text = value;
			}
		}

		public string ReportID
		{
			get
			{
				return labelhidden.Text;
			}
			set
			{
				string text2 = reportID = (labelhidden.Text = value);
			}
		}

		[Description("Determines if the field is required.")]
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

		[Description("If draw border around")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(true)]
		[Browsable(true)]
		public bool ShowBorder
		{
			get
			{
				return showBorder;
			}
			set
			{
				showBorder = value;
			}
		}

		public new bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				if (value)
				{
					BackColor = Color.WhiteSmoke;
				}
				else
				{
					BackColor = Color.White;
				}
				base.ReadOnly = value;
			}
		}

		public bool IsComboTextBox
		{
			get
			{
				return isComboTextBox;
			}
			set
			{
				isComboTextBox = value;
			}
		}

		[Description("Simulates enter key as tab.")]
		[Browsable(false)]
		public static bool SimulateEnterAsTab
		{
			get
			{
				return simulateEnterAsTab;
			}
			set
			{
				simulateEnterAsTab = value;
			}
		}

		public MMSelectionBox()
		{
			InitializeComponent();
			base.Validating += MMSelectionBox_Validating;
			base.GotFocus += MMSelectionBox_GotFocus;
			base.TextChanged += MMSelectionBox_TextChanged;
			base.Validated += MMSelectionBox_Validated;
			buttonSelect.Click += ButtonSelect_Click;
			base.Height = 60;
			base.Width = 200;
			buttonSelect.Parent = this;
			buttonSelect.Height = 60;
			buttonSelect.Width = 30;
			base.Controls.Add(buttonSelect);
			base.Controls.Add(labelhidden);
			labelhidden.Visible = false;
			Cursor.Current = Cursors.WaitCursor;
			buttonSelect.Dock = DockStyle.Right;
			buttonSelect.Text = "..";
		}

		private void ButtonSelect_Click(object sender, EventArgs e)
		{
		}

		private void MMSelectionBox_Validated(object sender, EventArgs e)
		{
			originalText = Text;
		}

		private void MMSelectionBox_TextChanged(object sender, EventArgs e)
		{
			Modified = true;
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				return "'" + Text + "'";
			}
			if (crFieldName == "")
			{
				return "''=''";
			}
			return crFieldName + " = '" + Text + "'";
		}

		private void MMSelectionBox_GotFocus(object sender, EventArgs e)
		{
			if (Multiline && base.ScrollBars == ScrollBars.None)
			{
				base.ScrollBars = ScrollBars.Vertical;
			}
		}

		private void MMSelectionBox_Validating(object sender, CancelEventArgs e)
		{
			if (base.Enabled && !ReadOnly && base.Name == "textBoxCode" && Text != "")
			{
				Text = Text.Replace("'", "");
				Text = Text.Replace("--", "-");
				Text = Text.Replace("--", "-");
				Text = Text.Replace(";", "");
				Text = Text.Replace("/*", "");
				Text = Text.Replace("XP_", "XP");
			}
		}

		private void InitializeComponent()
		{
			SuspendLayout();
			base.Size = new System.Drawing.Size(200, 20);
			base.KeyDown += new System.Windows.Forms.KeyEventHandler(BATextBox_KeyDown);
			ResumeLayout(false);
		}

		public void DeFormatNumber()
		{
			try
			{
				Text = Global.RemoveChar(Text, ',');
			}
			catch
			{
			}
		}

		public void FormatNumber()
		{
			try
			{
				Text = decimal.Parse(Text).ToString(Format.TextBoxMoney);
			}
			catch
			{
			}
		}

		private void textBox_KeyDown(object sender, KeyEventArgs e)
		{
			base.OnKeyDown(e);
		}

		private void textBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
		}

		private void BATextBox_GotFocus(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void BATextBox_LostFocus(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void BATextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (simulateEnterAsTab && e.KeyCode == Keys.Return && !Multiline)
			{
				SendKeys.Send("{TAB}");
			}
		}

		private string GetSelectedID()
		{
			try
			{
				return "";
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}
	}
}
