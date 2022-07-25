using MongoDB.Driver;
using MongoDBExample.Models;

namespace MongoDBExample.Services
{
    public interface IUserMusicFavoritesService
    {


        public List<UserMusicFavorite> Get();

        public UserMusicFavorite GetByUserId(int userId);

        public void Remove(string id);

        public UserMusicFavorite Create(UserMusicFavorite model);

        public void Update(string id, UserMusicFavorite model);

    }
}