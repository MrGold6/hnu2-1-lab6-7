using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LabW4.Models;

namespace LabW4.Data
{
    public class LabW4Context : DbContext
    {
        public LabW4Context (DbContextOptions<LabW4Context> options)
            : base(options)
        {
        }

        public DbSet<LabW4.Models.Student> Student { get; set; } = default!;
        public DbSet<LabW4.Models.Voucher> Voucher { get; set; } = default!;
        public DbSet<LabW4.Models.University> University { get; set; } = default!;
        public DbSet<LabW4.Models.TradeUnion> TradeUnion { get; set; } = default!;
    }
}
