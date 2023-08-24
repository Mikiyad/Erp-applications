using System;
using System.Reflection;
using System.Windows.Forms;

namespace Micromind.ClientLibraries
{
	public sealed class UIRefelector
	{
		public static Form parentForm;

		private UIRefelector()
		{
		}

		public static int GetScreenID(Form form)
		{
			if (form == null)
			{
				return -1;
			}
			object obj = null;
			MethodInfo method = form.GetType().GetMethod("GetScreenID");
			if (method != null)
			{
				obj = method.Invoke(null, null);
				if (obj == null)
				{
					return -1;
				}
				return int.Parse(obj.ToString());
			}
			return -1;
		}

		public static object GetScreenArea(Form form)
		{
			if (form == null)
			{
				return -1;
			}
			MethodInfo method = form.GetType().GetMethod("GetScreenArea");
			if (method != null)
			{
				return method.Invoke(null, null);
			}
			return null;
		}

		public static Form GetForm(Type type)
		{
			Form form = null;
			try
			{
				return type.InvokeMember(type.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, new object[1]
				{
					parentForm
				}) as Form;
			}
			catch
			{
				return type.InvokeMember(type.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null) as Form;
			}
		}
	}
}
