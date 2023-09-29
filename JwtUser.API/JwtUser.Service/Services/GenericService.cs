using AutoMapper;
using JwtUser.Core.Repositories;
using JwtUser.Core.Services;
using JwtUser.Core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace JwtUser.Service.Services
{
    public class GenericService<T,TDto> : IGenericService<T,TDto> where T : class where TDto : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GenericService(IGenericRepository<T> genericRepository, IUnitOfWork unitOfWork)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(TDto t)
        {
            var newEntity = ObjectMapper.Mapper.Map<T>(t);
            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var values = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
            return values!;
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var values = await _genericRepository.GetByIdAsync(id);

            if (values == null)
                return null!;
            else
                return ObjectMapper.Mapper.Map<TDto>(values);
        }

        public async Task<IQueryable<TDto>> GetListByFilter(Expression<Func<T, bool>> expression)
        {
            var list = _genericRepository.GetListByFilter(expression);
            return ObjectMapper.Mapper.Map<IQueryable<TDto>>(await list.ToListAsync());
        }

        public void Remove(T t)
        {
            _genericRepository.Remove(t);
            _unitOfWork.Commit();
        }

        public void Update(TDto entity)
        {
            var updateEntity = ObjectMapper.Mapper.Map<T>(entity);
            _genericRepository.Update(updateEntity);
            _unitOfWork.Commit();
        }
    }
}
