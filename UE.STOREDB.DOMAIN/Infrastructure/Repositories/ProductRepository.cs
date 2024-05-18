using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UE.STOREDB.DOMAIN.Core.Entities;
using UE.STOREDB.DOMAIN.Core.Interfaces;
using UE.STOREDB.DOMAIN.Infraestructure.Data;

namespace UE.STOREDB.DOMAIN.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _dbContext;

        public ProductRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbContext.Product.Where(p => p.IsActive == true).Include(p => p.ProductDetail).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _dbContext.Product.Where(p => p.IsActive == true && p.Id == id).Include(p => p.ProductDetail).FirstOrDefaultAsync();
        }

        public async Task<bool> Insert(Product product)
        {
            await _dbContext.Product.AddAsync(product);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Update(Product product)
        {
            _dbContext.Product.Update(product);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var findProduct = _dbContext.Product.Where(p => p.IsActive == true && p.Id == id).FirstOrDefault();
            if (findProduct == null) { return false; }
            _dbContext.Product.Remove(findProduct);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteLogic(int id)
        {
            var findProduct = await _dbContext.Product.Where(p => p.IsActive == true && p.Id == id).FirstOrDefaultAsync();
            if (findProduct == null) { return false; }
            findProduct.IsActive = false;
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
