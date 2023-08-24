using Micromind.ClientLibraries;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;

namespace Micromind.DataCaches.Accounts
{
	public sealed class SysDocTypes
	{
		private static DataSet glTypes;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		static SysDocTypes()
		{
			SysDocTypes.Refereshed = null;
			glTypes = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (glTypes != null)
			{
				glTypes.Dispose();
				glTypes = null;
			}
		}

		public static DataSet GetGLTypes(bool isReferesh)
		{
			if (Factory.IsDBConnected)
			{
				if ((glTypes == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((glTypes == null) | isReferesh)
						{
							SetData();
						}
					}
				}
			}
			else if (glTypes == null)
			{
				throw new DBNotConnectedException();
			}
			return glTypes;
		}

		private static void SetData()
		{
			if (glTypes != null)
			{
				glTypes.Dispose();
				glTypes = null;
			}
			OnRefereshed();
		}

		private static void OnRefereshed()
		{
			if (SysDocTypes.Refereshed != null)
			{
				SysDocTypes.Refereshed();
			}
		}
	}
}
