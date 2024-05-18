using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UE.STOREDB.DOMAIN.Core.Entities;
using UE.STOREDB.DOMAIN.Core.Interfaces;
using UE.STOREDB.DOMAIN.Infraestructure.Data;

namespace UE.STOREDB.DOMAIN.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext _dbContext;

        public CategoryRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        ////Sincrono
        //public IEnumerable<Category> GetAll()
        //{
        //    return _dbContext
        //        .Category
        //        .Where(c=>c.IsActive==true)
        //        .ToList();
        //}

        //Asincrono
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Category
                .Where(c => c.IsActive == true)
                .ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _dbContext.Category
                .Where(c => c.IsActive == true && c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Insert(Category category)
        {
            await _dbContext.Category.AddAsync(category);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Update(Category category)
        {
            _dbContext.Category.Update(category);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var findCategory = await _dbContext.Category
                                .Where(c => c.IsActive == true && c.Id == id)
                                .FirstOrDefaultAsync();
            if (findCategory == null) return false;

            _dbContext.Category.Remove(findCategory);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        //Eliminación Lógica
        public async Task<bool> DeleteLogic(int id)
        {
            var findCategory = await _dbContext.Category
                                 .Where(c => c.IsActive == true && c.Id == id)
                                 .FirstOrDefaultAsync();
            if (findCategory != null) return false;

            findCategory.IsActive = false;
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;

        }


    }
}
