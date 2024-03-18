using MyMusicApi.Core.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMusicApi.Core.Services
{
    public interface IUSerService
    {
        Task<User> Create(User user, string password);
        void Update(User user, string password);
        void Delete(int id);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetByAll();

    }
}
