using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SettingData : DataSet
	{
		public const string SETTINGS_TABLE = "Settings";

		public const string EMPLOYEEPICDIRECTORY_FIELD = "EmployeePicDirectory";

		public const string EMPLOYEEPICDIRECTORYNAME_FIELD = "Employees Images";

		public const string ITEMPICDIRECTORY_FIELD = "ItemPicDirectory";

		public const string ITEMPICDIRECTORYNAME_FIELD = "Item Images";

		public const string LOGOPICDIRECTORY_FIELD = "LogoPicDirectory";

		public const string LOGPICDIRECTORYNAME_FIELD = "Logo Images";

		public const string PARTNERPICDIRECTORY_FIELD = "PartnerPicDirectory";

		public const string PARTNERPICDIRECTORYNAME_FIELD = "Contact Images";

		public const string SHAREDID_FIELD = "E4C9AB0B1E954fe8A4839E8540290204I";

		public const string SHAREDAPPNAME_FIELD = "E4C9AB0B1E954fe8A4839E8540290205N";

		public const string SHAREDKEY_FIELD = "E4C9AB0B1E954fe8A4839E8540290203K";

		public const string MAINDIRECTORY_FIELD = "MainDirectory";

		public SettingData()
		{
		}

		public SettingData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
