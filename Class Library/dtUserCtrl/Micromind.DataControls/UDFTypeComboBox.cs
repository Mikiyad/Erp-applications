using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;

namespace Micromind.DataControls
{
	public class UDFTypeComboBox : SingleColumnComboBox
	{
		private Container components;

		public UDFTypeComboBox()
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
			new System.Resources.ResourceManager(typeof(Micromind.DataControls.AccountTypesComboBox));
			SuspendLayout();
			base.Name = "ItemTypesComboBox";
			base.Size = new System.Drawing.Size(192, 20);
		}

		public override void LoadData()
		{
			base.Items.Clear();
			foreach (UDFTypes value in Enum.GetValues(typeof(UDFTypes)))
			{
				base.Items.Add(value);
			}
		}
	}
}
