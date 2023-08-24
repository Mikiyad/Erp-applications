using System;
using System.ComponentModel;

namespace Micromind.DataControls.FormDesigner
{
	internal class PropertyGridSite : ISite, IServiceProvider
	{
		private IServiceProvider sp;

		public IComponent Component => null;

		public IContainer Container => null;

		public bool DesignMode => false;

		public string Name
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public PropertyGridSite(IServiceProvider sp)
		{
			this.sp = sp;
		}

		public object GetService(Type serviceType)
		{
			if (sp != null)
			{
				return sp.GetService(serviceType);
			}
			return null;
		}
	}
}
