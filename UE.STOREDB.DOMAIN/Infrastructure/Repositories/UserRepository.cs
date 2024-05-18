using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UE.STOREDB.DOMAIN.Core.Entities;
using UE.STOREDB.DOMAIN.Infraestructure.Data;

namespace UE.STOREDB.DOMAIN.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreDbContext _dbContext;
        public UserRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.User.ToListAsync();
        }
        public async Task<User> GetById(int id)
        {
            return await _dbContext.User.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<bool> Insert(User user)
        {
            await _dbContext.User.AddAsync(user);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Update(User user)
        {
            _dbContext.User.Update(user);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var findUser = await _dbContext.User.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (findUser == null) { return false; }

            _dbContext.User.Remove(findUser);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteLogic(int id)
        {
            var findUser = await _dbContext.User.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (findUser == null) { return false; }

            findUser.IsActive = false;
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

    }
}
