using System.Windows.Forms;

namespace Micromind.DataControls
{
	internal class ComboBoxEditingControl : AllAccountsComboBox, IDataGridViewEditingControl
	{
		private DataGridView dataGridView;

		private bool valueChanged;

		private int rowIndex;

		public object EditingControlFormattedValue
		{
			get
			{
				return Text;
			}
			set
			{
				if (value is string)
				{
					Text = value.ToString();
				}
			}
		}

		public int EditingControlRowIndex
		{
			get
			{
				return rowIndex;
			}
			set
			{
				rowIndex = value;
			}
		}

		public bool RepositionEditingControlOnValueChange => false;

		public DataGridView EditingControlDataGridView
		{
			get
			{
				return dataGridView;
			}
			set
			{
				dataGridView = value;
			}
		}

		public bool EditingControlValueChanged
		{
			get
			{
				return valueChanged;
			}
			set
			{
				valueChanged = value;
			}
		}

		public Cursor EditingPanelCursor => base.Cursor;

		public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return EditingControlFormattedValue;
		}

		public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			Font = dataGridViewCellStyle.Font;
			ForeColor = dataGridViewCellStyle.ForeColor;
			BackColor = dataGridViewCellStyle.BackColor;
		}

		public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
		{
			switch (key & Keys.KeyCode)
			{
			case Keys.Prior:
			case Keys.Next:
			case Keys.End:
			case Keys.Home:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				return true;
			default:
				return false;
			}
		}

		public void PrepareEditingControlForEdit(bool selectAll)
		{
		}
	}
}
