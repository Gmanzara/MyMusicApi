using Microsoft.EntityFrameworkCore;
using MyMusicApi.Core.Models;
using MyMusicApi.Core.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicApi.Data.Repositories
{
    public class MusicRepository : Repository<Music>,IMusicRepository
    {

        private MyMusicDbContext MyMusicDbContext
        {
            get { return Context as MyMusicDbContext; }
        }
        public MusicRepository(MyMusicDbContext context) : base(context) { }
        public async Task<IEnumerable<Music>> GetAllWithArtistAsync()
        {
            return await MyMusicDbContext.Musics
                .Include(m => m.Artist)
                .ToListAsync();
        }

        public async Task<Music> GetAllWithArtistByIdAsync(int id)
        {
            return await MyMusicDbContext.Musics
                .Include(m => m.Artist)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId)
        {
            return await MyMusicDbContext.Musics
                .Include(m => m.Artist)
                .Where(m=>m.ArtistId == artistId)
                .ToListAsync();
        }

    }
}
