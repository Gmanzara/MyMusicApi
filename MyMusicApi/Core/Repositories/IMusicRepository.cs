using MyMusicApi.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMusicApi.Core.Repository
{
    public interface IMusicRepository : IRepository<Music>
    {
        Task<IEnumerable<Music>> GetAllWithArtistAsync();
        Task<Music> GetAllWithArtistByIdAsync(int id);
        Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId);
    }
}
