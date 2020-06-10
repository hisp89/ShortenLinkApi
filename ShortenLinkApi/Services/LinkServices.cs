using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ShortenLinkApi.Models;

namespace ShortenLinkApi.Services
{
    public class LinkServices
    {
        private readonly IMongoCollection<Link> _links;

        public LinkServices(IDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _links = database.GetCollection<Link>(settings.LinksCollectionName);
        }

        public Task<List<Link>> Get() => _links.Find(i => true).ToListAsync();

        public async Task<Link> Get(string shortUrl)
        {
            var item = await _links.Find(i => i.ShortUrl == shortUrl).FirstOrDefaultAsync();
            if (item == null) return null;
            item.Count++;
            _links.ReplaceOne(i => i.Id == item.Id, item);

            return item;
        }

        public async Task<Link> Create(string url)
        {
            var item = await _links.Find(i => i.Url == url).FirstOrDefaultAsync();
            if (item != null) return item;
            item = new Link() { Url = url, ShortUrl = Utils.GetRandomString() };
            _links.InsertOne(item);
            return item;
        }
    }
}
