using Microsoft.EntityFrameworkCore;
using MyMusicApi.Core.Models;
using MyMusicApi.Core.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMusicApi.Data.Repositories
{
    public class ArtistRepository : Repository<Artist>,IArtistRepository
    {
        private MyMusicDbContext MyMusicDbContext
        {
            get { return Context as MyMusicDbContext; }
        }

        public ArtistRepository(MyMusicDbContext context):base(context) { }

        public async Task<IEnumerable<Artist>> GetAllWithMusicAsync()
        {
            return await MyMusicDbContext.Artists
                .Include(a => a.Musics)
                .ToListAsync();
        }

        public Task<Artist> GetWithMusicByIdAsync(int id)
        {
            return MyMusicDbContext.Artists
                .Include(a=>a.Musics)
                .SingleOrDefaultAsync(a=>a.Id == id);
        }
    }
}
