using MyMusicApi.Core;
using MyMusicApi.Core.Repository;
using MyMusicApi.Data.Repositories;
using System.Threading.Tasks;

namespace MyMusicApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyMusicDbContext _context;
        private IMusicRepository _musicRepository;
        private IArtistRepository _artistRepository;
        private IUserRepository _userRepository;
        public UnitOfWork(MyMusicDbContext context)
        {
            this._context = context;
            
        }
        public IMusicRepository Musics => _musicRepository ?? new MusicRepository(_context);
        public IArtistRepository Artists => _artistRepository ?? new ArtistRepository(_context);
        public IUserRepository Users => _userRepository ?? new UserRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
