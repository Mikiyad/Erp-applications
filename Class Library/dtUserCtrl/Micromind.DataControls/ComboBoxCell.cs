using System;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ComboBoxCell : DataGridViewTextBoxCell
	{
		public override Type EditType => typeof(ComboBoxEditingControl);

		public override Type ValueType => typeof(int);

		public override object DefaultNewRowValue => "";

		public ComboBoxCell()
		{
			base.Style.Format = "d";
		}

		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			_ = base.DataGridView.EditingControl;
		}
	}
}
