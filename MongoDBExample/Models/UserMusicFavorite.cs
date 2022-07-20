using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBExample.Models
{
    public class UserMusicFavorite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userId { get; set; }
        public List<Music> favorites { get; set; }
    }
}
