using Micromind.Common.Data;
using Micromind.DataControls.Libraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class CardTypesComboBox : SingleColumnComboBox
	{
		private Container components;

		public override int SelectedID
		{
			get
			{
				if (SelectedIndex < 0)
				{
					return -1;
				}
				ComboData comboData = base.SelectedItem as ComboData;
				if (comboData == null)
				{
					return -1;
				}
				return int.Parse(comboData.ID);
			}
			set
			{
				if (value < 0)
				{
					SelectedIndex = -1;
				}
				else
				{
					foreach (ComboData item in base.Items)
					{
						if (int.Parse(item.ID.ToString()) == value)
						{
							base.SelectedItem = item;
						}
					}
				}
			}
		}

		public CardTypesComboBox()
		{
			InitializeComponent();
			base.DropDownStyle = ComboBoxStyle.DropDownList;
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
			new System.Resources.ResourceManager(typeof(Micromind.DataControls.AccountTypesComboBox));
			SuspendLayout();
			base.Name = "ItemTypesComboBox";
			base.Size = new System.Drawing.Size(192, 20);
		}

		public override void LoadData()
		{
			base.Items.Clear();
			foreach (DataComboType value in Enum.GetValues(typeof(DataComboType)))
			{
				if (value != DataComboType.CustomList)
				{
					string name = value.ToString();
					int num = (int)value;
					ComboData item = new ComboData(name, num.ToString(), value);
					base.Items.Add(item);
				}
			}
			base.Sorted = true;
		}
	}
}
