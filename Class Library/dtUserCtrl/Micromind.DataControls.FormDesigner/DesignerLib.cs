
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Micromind.DataControls.FormDesigner
{
	public static class DesignerLib
	{
		public static ArrayList visibleProperties;

		public static ArrayList readOnlyProperties;

		public static ICustomTypeDescriptor CreatePropertyDescriber(object control)
		{
			LoadArrays();
			return new DescriberObject(control);
		}

		public static void DesignForm(Form form)
		{
			try
			{
			}
			catch
			{
				throw;
			}
		}

		private static void DesignedFormClosing(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
		}

		private static void designer_DesingEnded(object sender, EventArgs e)
		{
		}

		public static void LoadFormLayout(Form form, string fileName)
		{
			try
			{
			}
			catch
			{
				throw;
			}
		}

		public static void LoadFormLayout(Form form, Stream layout)
		{
			try
			{
			}
			catch
			{
				throw;
			}
		}

		private static void LoadArrays()
		{
			if (visibleProperties == null)
			{
				visibleProperties = new ArrayList();
				readOnlyProperties = new ArrayList();
				visibleProperties.Add("Text");
				visibleProperties.Add("Size");
				visibleProperties.Add("Anchor");
				visibleProperties.Add("BackColor");
				visibleProperties.Add("BorderStyle");
				visibleProperties.Add("ForeColor");
				visibleProperties.Add("Font");
				visibleProperties.Add("Enabled");
				visibleProperties.Add("Dock");
				visibleProperties.Add("Image");
				visibleProperties.Add("TabIndex");
				visibleProperties.Add("TextAlign");
				visibleProperties.Add("Visible");
				visibleProperties.Add("Location");
				visibleProperties.Add("MaxLength");
				visibleProperties.Add("ReadOnly");
				visibleProperties.Add("TabStop");
				visibleProperties.Add("WordWrap");
				visibleProperties.Add("DialogResult");
				visibleProperties.Add("AcceptButton");
				visibleProperties.Add("CancelButton");
				visibleProperties.Add("Visible");
				visibleProperties.Add("Visible");
				visibleProperties.Add("Visible");
				visibleProperties.Add("Visible");
				visibleProperties.Add("Visible");
				visibleProperties.Add("Visible");
				visibleProperties.Add("Visible");
				visibleProperties.Add("Visible");
				visibleProperties.Add("Visible");
				visibleProperties.Add("Visible");
				readOnlyProperties.Add("Name");
			}
		}
	}
}
