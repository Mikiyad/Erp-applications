using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IUserFavoriteSystem
	{
		bool CreateUserFavorite(UserFavoriteData unitData);

		bool CreateUserFavorite(int favoriteType, string documentID, string favoriteName);

		bool DeleteUserFavorite(string userID, int favoriteType, string documentID);

		bool IsUserFavorite(string userID, int favoriteType, string documentID);

		DataSet GetUserFavoriteList(string userID, FavoriteTypes favoriteType);
	}
}
