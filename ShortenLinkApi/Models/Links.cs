using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShortenLinkApi.Models
{
    public class Link
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public long Count { get; set; }
        public string Session { get; set; }

        public string ShortUrlAbsolute { get { return $"{Startup.Domain}/{ShortUrl}"; } }
    }
}
