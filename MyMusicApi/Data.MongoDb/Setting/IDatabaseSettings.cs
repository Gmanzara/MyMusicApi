using MongoDB.Driver;
using MyMusicApi.Core.Models;

namespace MyMusicApi.Data.MongoDb.Setting
{
    public interface IDatabaseSettings
    {
        IMongoCollection<Composer> Composers { get; }
    }
}
