using MongoDBExample.Models;
using MongoDBExample.Services;
using HotChocolate.AspNetCore.Authorization;

namespace MongoDBExample.Types
{
    
    public class Query
    {
        [Authorize]
        public List<UserMusicFavorite> GetUserMusicFavorites([Service] UserMusicFavoritesService userMusicFavorite)
        {
            var list = userMusicFavorite.Get();
            return list;
        }
        public List<User> GetUsers([Service] UserService userService)
        {
            var list = userService.GetUsers();
            return list;
        }
    }
}
