using MyMusicApi.Core.Models;
using MyMusicApi.Core.Repositories;
using MyMusicApi.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMusicApi.Services
{
    public class ComposerService : IComposerService
    {
        private readonly IComposerRepository _context;

        public ComposerService(IComposerRepository context)
        {
            this._context = context;
        }

        public async Task<Composer> Create(Composer composer)
        {
            return await _context.Create(composer);
        }

        public async Task<bool> Delete(string id)
        {
            return await _context.Delete(id);
        }

        public Task<IEnumerable<Composer>> GetAllComposers()
        {
            return _context.GetAllComposers();
        }

        public async Task<Composer> GetComposerById(string id)
        {
            return await GetComposerById(id);
        }

        public void Update(string id, Composer composer)
        {
            _context.Update(id, composer);
        }
    }
}
