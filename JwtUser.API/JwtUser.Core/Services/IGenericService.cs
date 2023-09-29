using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JwtUser.Core.Services
{
    public interface IGenericService<T,TDto> where T : class where TDto: class
    {
        Task AddAsync(TDto t);

        Task<IEnumerable<TDto>> GetAllAsync();
        Task<IQueryable<TDto>> GetListByFilter(Expression<Func<T, bool>> expression);

        Task<TDto> GetByIdAsync(int id);

        void Remove(T t);

        void Update(TDto entity);
    }
}
