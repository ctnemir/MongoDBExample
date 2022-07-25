using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoDBExample.Models
{
    public class UserMusicFavorite
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public int userId { get; set; }
        [JsonIgnore]
        public List<Music> favorites { get; set; }
    }
}
