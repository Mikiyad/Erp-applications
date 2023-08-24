using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class UserFavorites : StoreObject
	{
		public const string USERID_PARM = "@UserID";

		public const string DOCUMENTID_PARM = "@DocumentID";

		public const string FAVORITETYPE_PARM = "@FavoriteType";

		public const string FAVORITENAME_PARM = "@FavoriteName";

		public UserFavorites(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("User_Favorite", new FieldValue("UserID", "@UserID"), new FieldValue("DocumentID", "@DocumentID"), new FieldValue("FavoriteName", "@FavoriteName"), new FieldValue("FavoriteType", "@FavoriteType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@FavoriteType", SqlDbType.TinyInt);
			parameters.Add("@DocumentID", SqlDbType.NVarChar);
			parameters.Add("@FavoriteName", SqlDbType.NVarChar);
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@FavoriteType"].SourceColumn = "FavoriteType";
			parameters["@DocumentID"].SourceColumn = "DocumentID";
			parameters["@FavoriteName"].SourceColumn = "FavoriteName";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUserFavorite(int favoriteType, string documentID, string favoriteName)
		{
			try
			{
				UserFavoriteData userFavoriteData = new UserFavoriteData();
				DataRow dataRow = userFavoriteData.UserFavoriteTable.NewRow();
				dataRow["UserID"] = base.DBConfig.UserID;
				dataRow["FavoriteType"] = favoriteType;
				dataRow["DocumentID"] = documentID;
				dataRow["FavoriteName"] = favoriteName;
				userFavoriteData.UserFavoriteTable.Rows.Add(dataRow);
				return InsertUserFavorite(userFavoriteData);
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUserFavorite(UserFavoriteData accountUserFavoriteData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountUserFavoriteData, "User_Favorite", insertUpdateCommand);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool IsUserFavorite(string userID, int favoriteType, string documentID)
		{
			try
			{
				string exp = "SELECT *\r\n                           FROM User_Favorite WHERE UserID = '" + userID + "' AND FavoriteType = " + favoriteType + " AND DocumentID = '" + documentID + "'";
				if (ExecuteScalar(exp) == null)
				{
					return false;
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteUserFavorite(string userID, int favoriteType, string documentID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM User_Favorite WHERE UserID = '" + userID + "' AND FavoriteType = " + favoriteType + " AND DocumentID = '" + documentID + "'";
				return Delete(commandText, null);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetUserFavoriteList(string userID, FavoriteTypes favoriteType)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT *\r\n                           FROM User_Favorite WHERE UserID = '" + userID + "' AND FavoriteType = " + (int)favoriteType;
			FillDataSet(dataSet, "User_Favorite", textCommand);
			return dataSet;
		}
	}
}
