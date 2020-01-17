using System.Collections.Generic;
using BareBones.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Infrastructure
{
    public class OrderingDbContextBase : DbContextBase
    {
        public OrderingDbContextBase(DbContextOptions options) : base(options)
        {
        }

        public OrderingDbContextBase(
            DbContextOptions options,
            IEnumerable<IDbContextInterceptor> interceptors) : base(options, interceptors)
        {
        }
    }
}
