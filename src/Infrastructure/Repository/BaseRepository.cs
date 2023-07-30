using CleanArchitectureSample.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CleanArchitectureSample.Infrastructure.Repository
{
    public partial class BaseRepository
    {
        public BaseRepository(SampleDbContext dbContext)
        {
        }
    }
}
