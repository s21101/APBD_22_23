using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using cw10.Models;

namespace cw10.Data
{
    public class cw10Context : DbContext
    {
        public cw10Context (DbContextOptions<cw10Context> options)
            : base(options)
        {
        }

        public DbSet<cw10.Models.Movie> Movie { get; set; } = default!;
    }
}
