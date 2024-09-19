using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Eintites;

namespace Talabat.Core.Repositories
{
    public interface IGenaricRepository <T> where T : BaseEntity
    {
         Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync (int id);

    }
}
