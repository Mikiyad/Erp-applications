using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class SingleColumnDataComboBox : UltraCombo
	{
		private bool isDataLoaded;

		private IContainer components;

		public new object DataSource
		{
			get
			{
				return base.DataSource;
			}
			set
			{
				base.DataSource = value;
				ApplyBandSettings();
				Text = "";
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string SelectedID
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return "";
				}
				return base.SelectedRow.Cells[0].Value.ToString();
			}
			set
			{
				if (!isDataLoaded)
				{
					LoadData();
				}
				foreach (UltraGridRow row in base.Rows)
				{
					if (row.Cells[0].Value.ToString() == value.ToString())
					{
						row.Selected = true;
						break;
					}
				}
			}
		}

		public RowsCollection Items => base.Rows;

		public bool Editable
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		public int SelectedIndex
		{
			get
			{
				return -1;
			}
			set
			{
			}
		}

		internal bool IsDataLoaded
		{
			get
			{
				return isDataLoaded;
			}
			set
			{
				isDataLoaded = value;
			}
		}

		public event EventHandler SelectedIndexChanged;

		public SingleColumnDataComboBox()
		{
			InitializeComponent();
			base.ValueChanged += SingleColumnComboBox_ValueChanged;
			base.BeforeDropDown += SingleColumnComboBox_BeforeDropDown;
			base.KeyDown += SingleColumnComboBox_KeyDown;
		}

		private void SingleColumnComboBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Tab && e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape && !base.IsDroppedDown)
			{
				PerformAction(UltraComboAction.Dropdown);
			}
		}

		private void SingleColumnComboBox_BeforeDropDown(object sender, CancelEventArgs e)
		{
			if (!isDataLoaded)
			{
				LoadData();
			}
		}

		private void SingleColumnComboBox_ValueChanged(object sender, EventArgs e)
		{
			if (this.SelectedIndexChanged != null)
			{
				this.SelectedIndexChanged(sender, e);
			}
		}

		private void ApplyBandSettings()
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
				base.DisplayLayout.Bands[0].SortedColumns.Add(base.DisplayLayout.Bands[0].Columns[1], descending: false);
				base.ValueMember = base.DisplayLayout.Bands[0].Columns[1].Key;
				base.DisplayLayout.Bands[0].ColHeadersVisible = false;
				base.DisplayLayout.Bands[0].Override.BorderStyleCell = UIElementBorderStyle.None;
				base.DisplayLayout.Bands[0].Override.BorderStyleRow = UIElementBorderStyle.None;
				base.DisplayLayout.Bands[0].Override.DefaultColWidth = 200;
				base.DisplayLayout.Bands[0].Override.DefaultRowHeight = 16;
				base.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
				base.DisplayLayout.Appearance.BorderColor = SystemColors.InactiveCaption;
				base.DisplayLayout.Bands[0].Override.HotTrackRowAppearance.BackColor = Color.LightSteelBlue;
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (base.DisplayLayout.Bands.Count != 0)
			{
				if (!base.IsDroppedDown && Focused)
				{
					PerformAction(UltraComboAction.Dropdown);
				}
				base.OnKeyDown(e);
			}
		}

		public void DropDown()
		{
			PerformAction(UltraComboAction.Dropdown);
		}

		public bool SelectItemByText(string text)
		{
			return false;
		}

		public void Clear()
		{
			Text = "";
		}

		public virtual void LoadData()
		{
		}

		public virtual string GetSelectedItemName()
		{
			if (base.SelectedRow == null || base.SelectedRow.Cells.Count < 2)
			{
				return "";
			}
			return base.SelectedRow.Cells[1].Value.ToString();
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
		}
	}
}
