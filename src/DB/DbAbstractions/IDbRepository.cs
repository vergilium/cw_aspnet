using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DbAbstractions {
    public interface IDbRepository<T>
        where T : class, IDbIdentity {
        IQueryable<T> AllItems { get; }
        DbContext Context { get; }
        Task<List<T>> ToListAsync();
        Task<int> AddItemAsync(T item);
        Task<int> AddItemsAsync(IEnumerable<T> items);
        Task<T> GetItemAsync(int id);
        Task<bool> ChangeItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<int> SaveChangesAsync();
        Task<bool> UpdateItem(T item);
    }
    
    public interface IDbRepository<T, in TId>
        where T : class, IDbIdentity<TId> {
        IQueryable<T> AllItems { get; }
        DbContext Context { get; }
        Task<List<T>> ToListAsync();
        Task<int> AddItemAsync(T item);
        Task<int> AddItemsAsync(IEnumerable<T> items);
        Task<T> GetItemAsync(TId id);
        Task<bool> ChangeItemAsync(T item);
        Task<bool> DeleteItemAsync(TId id);
        Task<int> SaveChangesAsync();
        Task<bool> UpdateItem(T item);
    }
}