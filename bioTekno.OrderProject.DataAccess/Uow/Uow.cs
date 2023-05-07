using bioTekno.OrderProject.DataAccess.Contexts;
using bioTekno.OrderProject.DataAccess.Interfaces;
using bioTekno.OrderProject.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.DataAccess.Uow
{
    public class Uow : IUow
    {
        private readonly bioTeknoContext _context;

        public Uow(bioTeknoContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : class,new()
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
