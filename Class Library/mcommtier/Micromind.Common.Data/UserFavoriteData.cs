using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class UserFavoriteData : DataSet
	{
		public const string USERFAVORITE_TABLE = "User_Favorite";

		public const string USERID_FIELD = "UserID";

		public const string DOCUMENTID_FIELD = "DocumentID";

		public const string FAVORITETYPE_FIELD = "FavoriteType";

		public const string FAVORITENAME_FIELD = "FavoriteName";

		public DataTable UserFavoriteTable => base.Tables["User_Favorite"];

		public UserFavoriteData()
		{
			BuildDataTables();
		}

		public UserFavoriteData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("User_Favorite");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("UserID", typeof(string));
			columns.Add("DocumentID", typeof(string));
			columns.Add("FavoriteName", typeof(string));
			columns.Add("FavoriteType", typeof(string)).AllowDBNull = false;
			base.Tables.Add(dataTable);
		}
	}
}
