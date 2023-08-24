using System;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ComboBoxColumn : DataGridViewColumn
	{
		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				if (value != null && !value.GetType().IsAssignableFrom(typeof(ComboBoxCell)))
				{
					throw new InvalidCastException("Must be a ComboBoxCell");
				}
				base.CellTemplate = value;
			}
		}

		public ComboBoxColumn()
			: base(new ComboBoxCell())
		{
		}
	}
}
