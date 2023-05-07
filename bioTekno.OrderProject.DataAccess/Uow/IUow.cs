using bioTekno.OrderProject.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.DataAccess.Uow
{
    public interface IUow
    {
        IRepository<T> GetRepository<T>() where T : class,new();

        Task SaveChangesAsync();
    }
}
