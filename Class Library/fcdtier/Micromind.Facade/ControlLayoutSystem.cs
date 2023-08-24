using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ControlLayoutSystem : MarshalByRefObject, IControlLayoutSystem, IDisposable
	{
		private Config config;

		public ControlLayoutSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool SaveControlLayout(ControlLayoutData data)
		{
			return new ControlLayout(config).InsertControlLayout(data);
		}

		public ControlLayoutData GetControlLayout()
		{
			using (ControlLayout controlLayout = new ControlLayout(config))
			{
				return controlLayout.GetControlLayout();
			}
		}

		public bool DeleteControlLayout(ControlLayoutTypes type, string controlName, string layoutName)
		{
			using (ControlLayout controlLayout = new ControlLayout(config))
			{
				return controlLayout.DeleteControlLayout(type, controlName, layoutName);
			}
		}

		public byte[] GetControlLayoutByID(ControlLayoutTypes type, string controlName, string layoutName)
		{
			using (ControlLayout controlLayout = new ControlLayout(config))
			{
				return controlLayout.GetControlLayoutByID(type, controlName, layoutName);
			}
		}

		public DataSet GetControlLayoutComboList()
		{
			using (ControlLayout controlLayout = new ControlLayout(config))
			{
				return controlLayout.GetControlLayoutComboList();
			}
		}
	}
}
