using MyMusicApi.Core;
using MyMusicApi.Core.Models;
using MyMusicApi.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMusicApi.Services
{
    public class UserService : IUSerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<User> Create(User user, string password)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<User>> GetByAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User user, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}
