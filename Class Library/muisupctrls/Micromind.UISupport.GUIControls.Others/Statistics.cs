using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport.GUIControls.Others
{
	public class Statistics : UserControl
	{
		private XPButton buttonStats;

		private Container components;

		private string tableName;

		private string fieldIDName;

		private object fieldIDValue;

		private string titleText;

		[Description("Text on the button")]
		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public string ButtonText
		{
			get
			{
				return buttonStats.Text;
			}
			set
			{
				buttonStats.Text = value;
			}
		}

		[Description("Button Height")]
		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public int ButtonHeight
		{
			get
			{
				return buttonStats.Height;
			}
			set
			{
				buttonStats.Height = value;
			}
		}

		[Description("Button Width")]
		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public int ButtonWidth
		{
			get
			{
				return buttonStats.Width;
			}
			set
			{
				buttonStats.Width = value;
			}
		}

		[Description("Table name, for example UnitTables")]
		[Category("Appearance")]
		[Browsable(true)]
		public string TableName
		{
			get
			{
				return tableName;
			}
			set
			{
				tableName = value;
			}
		}

		[Description("Record ID field, for example UnitID")]
		[Category("Appearance")]
		[Browsable(true)]
		public string FieldIDName
		{
			get
			{
				return fieldIDName;
			}
			set
			{
				fieldIDName = value;
			}
		}

		[Description("Record ID value, for example 22")]
		[Category("Appearance")]
		[Browsable(true)]
		public object FieldIDValue
		{
			get
			{
				return fieldIDValue;
			}
			set
			{
				fieldIDValue = value;
			}
		}

		[Description("Title text for this statistics, for example Unit details")]
		[Category("Appearance")]
		[Browsable(true)]
		public string TitleText
		{
			get
			{
				return titleText;
			}
			set
			{
				titleText = value;
			}
		}

		public Statistics()
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
			buttonStats = new Micromind.UISupport.XPButton();
			SuspendLayout();
			buttonStats.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonStats.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonStats.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonStats.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			buttonStats.Location = new System.Drawing.Point(0, 0);
			buttonStats.Name = "buttonStats";
			buttonStats.Size = new System.Drawing.Size(80, 24);
			buttonStats.TabIndex = 0;
			buttonStats.Text = "Stats";
			buttonStats.Click += new System.EventHandler(buttonStats_Click);
			base.Controls.Add(buttonStats);
			base.Name = "Statistics";
			base.Size = new System.Drawing.Size(80, 24);
			base.Resize += new System.EventHandler(Statistics_Resize);
			base.Load += new System.EventHandler(Statistics_Load);
			ResumeLayout(false);
		}

		private void buttonStats_Click(object sender, EventArgs e)
		{
			if (tableName != null && !(tableName == string.Empty))
			{
				try
				{
					using (RecordInfoDialog recordInfoDialog = new RecordInfoDialog())
					{
						recordInfoDialog.TableName = tableName;
						recordInfoDialog.FieldName = fieldIDName;
						recordInfoDialog.FieldID = fieldIDValue;
						recordInfoDialog.Title = titleText;
						recordInfoDialog.ShowDialog();
					}
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void Statistics_Resize(object sender, EventArgs e)
		{
			buttonStats.Height = base.Height;
			buttonStats.Width = base.Width;
		}

		private void Statistics_Load(object sender, EventArgs e)
		{
		}
	}
}
