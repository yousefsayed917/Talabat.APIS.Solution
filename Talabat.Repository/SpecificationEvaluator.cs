using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Eintites;
using Talabat.Core.Specification;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator <T>where T : BaseEntity
    {
        public  static IQueryable<T> GetQuery(IQueryable<T>InputQuery,ISpecification<T> Spec )
        {
            var Query = InputQuery;
            if ( Spec.Criteria is not null ) 
                Query=Query.Where( Spec.Criteria );
            Query=Spec.Includes.Aggregate(Query,(CurrentQuery,IncludeExpression)=>CurrentQuery.Include(IncludeExpression));
            return Query;
        }
    }
}
