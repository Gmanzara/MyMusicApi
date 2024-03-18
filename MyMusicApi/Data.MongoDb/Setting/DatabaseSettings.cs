using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyMusicApi.Core.Models;

namespace MyMusicApi.Data.MongoDb.Setting
{
    public class DatabaseSettings : IDatabaseSettings
    {
        private readonly IMongoDatabase _db;
        public DatabaseSettings(IOptions<Settings> options, IMongoClient client)
        {
            _db = client.GetDatabase(options.Value.Database);

        }

        public IMongoCollection<Composer> Composers => _db.GetCollection<Composer>("Composers");
    }
}
