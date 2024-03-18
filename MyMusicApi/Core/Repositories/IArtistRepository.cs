using MyMusicApi.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMusicApi.Core.Repository
{
    public interface IArtistRepository:IRepository<Artist>
    {
        Task<IEnumerable<Artist>> GetAllWithMusicAsync();
        Task<Artist> GetWithMusicByIdAsync(int id);
    }
}
