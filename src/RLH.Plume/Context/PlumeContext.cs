
using Microsoft.EntityFrameworkCore;
using RLH.Plume.Core.Entities;

namespace RLH.Plume.Context
{
    public class PlumeContext : DbContext
    {
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Position> Positions { get; set; }

        public PlumeContext(DbContextOptions<PlumeContext> options) : base(options)
        {

        }
    }
}
