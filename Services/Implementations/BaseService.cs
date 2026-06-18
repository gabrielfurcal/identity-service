using System.Linq.Expressions;
using AutoMapper;
using identity_service.Context;
using identity_service.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace identity_service.Services.Implementations
{
    public class BaseService<T, K, M> : IBaseService<T, K, M> where T : class
    {
        private readonly IDbContextFactory<IdentityServiceDbContext> _contextFactory;
        private readonly IMapper _mapper;

       public BaseService(IDbContextFactory<IdentityServiceDbContext> contextFactory, IMapper mapper)
        {
            this._contextFactory = contextFactory;
            this._mapper = mapper;
        }

        public async Task<List<M>> FindAll()
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var entities = await _context.Set<T>().ToListAsync();
                    var listResponse = entities.Select(e => _mapper.Map<T, M>(e)).ToList();
                    return listResponse;
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding List of {nameof(T)}. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<M>> FindAll(Expression<Func<T, object>> Predicate)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var entities = await _context.Set<T>().Include(Predicate).ToListAsync();
                    var listResponse = entities.Select(e => _mapper.Map<T, M>(e)).ToList();
                    return listResponse;
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding List of {nameof(T)}. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<M>> FindAll(Expression<Func<T, object>>[] Predicates)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var entities = _context.Set<T>().AsQueryable();
                    
                    foreach (var item in Predicates)
                    {
                        entities = entities.Include(item);
                    }
                    
                    var entityList = await entities.ToListAsync();
                    var listResponse = entities.Select(e => _mapper.Map<T, M>(e)).ToList();

                    return listResponse;
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding List of {nameof(T)}. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<M>> FindFilteringList(Expression<Func<T, bool>> Predicate)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var entities = await _context.Set<T>().Where(Predicate).ToListAsync();
                    var listResponse = entities.Select(e => _mapper.Map<T, M>(e)).ToList();

                    return listResponse;
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding List of {nameof(T)}. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<M> FindById(K Id)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var entity = await _context.Set<T>().FindAsync(Id);

                    if (entity is null) throw new Exception($"{nameof(T)} not found");

                    return _mapper.Map<T, M>(entity);
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding element of {nameof(T)}, with ID: {Id}. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }            
        }

        public async Task<M> Save(M Dto, K? Id)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var entity = _mapper.Map<M, T>(Dto);

                    if (Id is null) await _context.Set<T>().AddAsync(entity);
                    else
                    {
                        _context.Entry(entity).State = EntityState.Modified;
                    }

                    await _context.SaveChangesAsync();

                    return _mapper.Map<T, M>(entity);
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving entity of {nameof(T)}. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }              
        }

        public async Task<bool> DeleteById(K Id)
        {
            try
            {
                using(IdentityServiceDbContext _context = _contextFactory.CreateDbContext())
                {
                    var entity = await _context.Set<T>().FindAsync(Id);

                    if (entity != null)
                    {
                        _context.Set<T>().Remove(entity);
                        await _context.SaveChangesAsync();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting element of {nameof(T)}, with ID: {Id}. Message: {ex.Message}");
                throw new Exception(ex.Message);
            }                
        }
    }
}