using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class RegisterSystem : MarshalByRefObject, IRegisterSystem, IDisposable
	{
		private Config config;

		public RegisterSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateRegister(RegisterData data)
		{
			return new Register(config).InsertRegister(data);
		}

		public bool UpdateRegister(RegisterData data)
		{
			return UpdateRegister(data, checkConcurrency: false);
		}

		public bool UpdateRegister(RegisterData data, bool checkConcurrency)
		{
			return new Register(config).UpdateRegister(data);
		}

		public RegisterData GetRegister()
		{
			using (Register register = new Register(config))
			{
				return register.GetRegister();
			}
		}

		public bool DeleteRegister(string groupID)
		{
			using (Register register = new Register(config))
			{
				return register.DeleteRegister(groupID);
			}
		}

		public RegisterData GetRegisterByID(string id)
		{
			using (Register register = new Register(config))
			{
				return register.GetRegisterByID(id);
			}
		}

		public DataSet GetRegisterByFields(params string[] columns)
		{
			using (Register register = new Register(config))
			{
				return register.GetRegisterByFields(columns);
			}
		}

		public DataSet GetRegisterByFields(string[] ids, params string[] columns)
		{
			using (Register register = new Register(config))
			{
				return register.GetRegisterByFields(ids, columns);
			}
		}

		public DataSet GetRegisterByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Register register = new Register(config))
			{
				return register.GetRegisterByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetRegisterList()
		{
			using (Register register = new Register(config))
			{
				return register.GetRegisterList();
			}
		}

		public DataSet GetRegisterComboList()
		{
			using (Register register = new Register(config))
			{
				return register.GetRegisterComboList();
			}
		}

		public decimal GetRegisterBalance(string registerID, string accountFieldIDName)
		{
			using (Register register = new Register(config))
			{
				return register.GetRegisterBalance(registerID, accountFieldIDName);
			}
		}
	}
}
