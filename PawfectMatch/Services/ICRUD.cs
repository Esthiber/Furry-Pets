using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public interface ICRUD<T>
    {
        public Task<bool> InsertAsync(T elem);
        public Task<bool> UpdateAsync(T elem);
        public Task<bool> DeleteAsync(int id);
        public Task<T> SearchByIdAsync(int id);
        public Task<bool> ExistAsync(int id);
        public Task<bool> SaveAsync(T elem);
        public Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria);

    }
}
