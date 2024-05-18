using UE.STOREDB.DOMAIN.Core.Entities;

namespace UE.STOREDB.DOMAIN.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Delete(int id);
        Task<bool> DeleteLogic(int id);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<bool> Insert(User user);
        Task<bool> Update(User user);
    }
}