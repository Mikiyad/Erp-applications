using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class LawyerSystem : MarshalByRefObject, ILawyerSystem, IDisposable
	{
		private Config config;

		public LawyerSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateLawyer(LawyerData data)
		{
			return new Lawyer(config).InsertLawyer(data);
		}

		public bool UpdateLawyer(LawyerData data)
		{
			return UpdateLawyer(data, checkConcurrency: false);
		}

		public bool UpdateLawyer(LawyerData data, bool checkConcurrency)
		{
			return new Lawyer(config).UpdateLawyer(data);
		}

		public LawyerData GetLawyer()
		{
			using (Lawyer lawyer = new Lawyer(config))
			{
				return lawyer.GetLawyer();
			}
		}

		public bool DeleteLawyer(string groupID)
		{
			using (Lawyer lawyer = new Lawyer(config))
			{
				return lawyer.DeleteLawyer(groupID);
			}
		}

		public LawyerData GetLawyerByID(string id)
		{
			using (Lawyer lawyer = new Lawyer(config))
			{
				return lawyer.GetLawyerByID(id);
			}
		}

		public DataSet GetLawyerByFields(params string[] columns)
		{
			using (Lawyer lawyer = new Lawyer(config))
			{
				return lawyer.GetLawyerByFields(columns);
			}
		}

		public DataSet GetLawyerByFields(string[] ids, params string[] columns)
		{
			using (Lawyer lawyer = new Lawyer(config))
			{
				return lawyer.GetLawyerByFields(ids, columns);
			}
		}

		public DataSet GetLawyerByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Lawyer lawyer = new Lawyer(config))
			{
				return lawyer.GetLawyerByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetLawyerList()
		{
			using (Lawyer lawyer = new Lawyer(config))
			{
				return lawyer.GetLawyerList();
			}
		}

		public DataSet GetLawyerComboList()
		{
			using (Lawyer lawyer = new Lawyer(config))
			{
				return lawyer.GetLawyerComboList();
			}
		}
	}
}
