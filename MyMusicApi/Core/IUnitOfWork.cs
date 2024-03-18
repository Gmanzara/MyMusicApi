using MyMusicApi.Core.Repositories;
using MyMusicApi.Core.Repository;
using System;
using System.Threading.Tasks;

namespace MyMusicApi.Core
{
    public interface IUnitOfWork:IDisposable
    {        
        IMusicRepository Musics { get; }
        IArtistRepository Artists { get; }
        IUserRepository Users { get; }
        Task<int> CommitAsync();

    }
}
