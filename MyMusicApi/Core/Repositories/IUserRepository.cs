using MyMusicApi.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMusicApi.Core.Repository
{
    public interface IUserRepository : IRepository<Music>
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
        Task<IEnumerable<User>> GetAllUserAsnyc();
        Task<User> GetWithUserByIdAsync(int id);
    }
}
