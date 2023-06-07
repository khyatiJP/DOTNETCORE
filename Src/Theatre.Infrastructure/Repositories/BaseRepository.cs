using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theatre.Infrastructure.Contracts;
using Theatre.Infrastructure.Data;

namespace Theatre.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbcontext;
        private DbSet<T> entity;
        public BaseRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            entity = _dbcontext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entity.ToListAsync();
        }
    }
}
