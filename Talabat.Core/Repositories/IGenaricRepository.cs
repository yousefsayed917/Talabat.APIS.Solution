using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Eintites;
using Talabat.Core.Specification;

namespace Talabat.Core.Repositories
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        #region With Out Specification
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        #endregion
        #region With Specification
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
        Task <T> GetByIdWithSpecAsync(ISpecification<T> spec);
        #endregion

    }
}
