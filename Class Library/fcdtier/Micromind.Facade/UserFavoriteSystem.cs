using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class UserFavoriteSystem : MarshalByRefObject, IUserFavoriteSystem, IDisposable
	{
		private Config config;

		public UserFavoriteSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateUserFavorite(UserFavoriteData data)
		{
			return new UserFavorites(config).InsertUserFavorite(data);
		}

		public bool CreateUserFavorite(int favoriteType, string documentID, string favoriteName)
		{
			return new UserFavorites(config).InsertUserFavorite(favoriteType, documentID, favoriteName);
		}

		public bool DeleteUserFavorite(string userID, int favoriteType, string documentID)
		{
			using (UserFavorites userFavorites = new UserFavorites(config))
			{
				return userFavorites.DeleteUserFavorite(userID, favoriteType, documentID);
			}
		}

		public bool IsUserFavorite(string userID, int favoriteType, string documentID)
		{
			using (UserFavorites userFavorites = new UserFavorites(config))
			{
				return userFavorites.IsUserFavorite(userID, favoriteType, documentID);
			}
		}

		public DataSet GetUserFavoriteList(string userID, FavoriteTypes favoriteType)
		{
			using (UserFavorites userFavorites = new UserFavorites(config))
			{
				return userFavorites.GetUserFavoriteList(userID, favoriteType);
			}
		}
	}
}
