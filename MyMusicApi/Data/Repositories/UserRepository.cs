using Microsoft.EntityFrameworkCore;
using MyMusicApi.Core.Models;
using MyMusicApi.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace MyMusicApi.Data.Repositories
{
    public class UserRepository : Repository<User>,IUserRepository
    {    
        private MyMusicDbContext MyMusicDbContext
        {
            get { return Context as MyMusicDbContext; }
        }

        public UserRepository(MyMusicDbContext context) : base(context) { }

        public Task AddAsync(Music entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Music> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = await MyMusicDbContext.Users.SingleOrDefaultAsync(x => x.UserName == username);
            if(user == null) 
                return null;
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;
                return null;
            return user;
        }
        public static bool VerifyPasswordHash(string password, byte[] storeHash, byte[] storedSalt)
        {
            if(password == null) throw new ArgumentNullException("password");
            if(string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("values connot be empty or whitespace only string.","password");
            if (storedSalt == null) throw new ArgumentNullException("Invalid lengh of password salt(128 bytes expected).","passwordSalt");
            if (storeHash == null) throw new ArgumentNullException("Invalid lengh of password hash(64 bytes expected.","passwordHash");
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i< computeHash.Length; i++)
                {
                    if (computeHash[i] != storedSalt[i]) 
                        return false;
                }
            }
            return true;
        }
        public async Task<User> Create(User user, string password)
        {
            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password is required");
            var resultUser = await MyMusicDbContext.Users.AnyAsync(x => x.UserName == user.UserName);
            if(resultUser)
                throw new Exception("Username \"" + user.UserName + "\" is already taken");
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out  passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await MyMusicDbContext.Users.AddAsync(user);
            return user;
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if(password == null) throw new ArgumentNullException("password");
            if(string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("value cannot be empty");
            using(var hmac =  new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public void Delete(int id)
        {
            var user = MyMusicDbContext.Users.Find(id);
            if(user != null)
            {
                MyMusicDbContext.Users.Remove(user);
            }
        }
        public async Task<IEnumerable<User>> GetAllUserAsnyc()
        {
            return await MyMusicDbContext.Users
                .ToListAsync();
        }

        public async Task<User> GetWithUserByIdAsync(int id)
        {
            return await MyMusicDbContext.Users
                .Where(user =>user.Id == id)
                .FirstOrDefaultAsync();
        }

   
        public void Update(User user, string password = null)
        {
            var userr = MyMusicDbContext.Users.Find(user.Id);
            if (userr == null)
                throw new Exception("User not found");
            if(user.UserName != userr.UserName)
            {
                if (MyMusicDbContext.Users.Any(x => x.UserName == user.UserName))
                    throw new Exception("Username \"" + user.UserName + "\" is already taken");
            }

            user.FirstName = userr.FirstName;
            user.LastName = userr.LastName;
            user.UserName = userr.UserName;

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            MyMusicDbContext.Users.Update(user);
        }

        Task<IEnumerable<Music>> IRepository<Music>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        ValueTask<Music> IRepository<Music>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Music> Find(Expression<Func<Music, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Music> SingleOrDefaultAsync(Expression<Func<Music, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(Music entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(Music entities)
        {
            throw new NotImplementedException();
        }
    }
}
