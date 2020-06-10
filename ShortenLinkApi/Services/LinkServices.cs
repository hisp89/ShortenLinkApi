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


        public async Task<Link> Get(string shortUrl)
        {
            var item = await _links.Find(i => i.ShortUrl == shortUrl).FirstOrDefaultAsync();
            if (item == null) return null;
            item.Count++;
            await _links.ReplaceOneAsync(i => i.Id == item.Id, item);

            return item;
        }
        public Task<List<Link>> GetListBySession(string session) => _links.Find(i => i.Session == session).ToListAsync();

        public async Task<Link> Create(string url, string sesion)
        {
            var item = await _links.Find(i => i.Url == url).FirstOrDefaultAsync();
            if (item != null) return item;
            item = new Link() { Session = sesion, Url = url, ShortUrl = Utils.GetRandomString() };
            _links.InsertOne(item);
            return item;
        }
    }
}
