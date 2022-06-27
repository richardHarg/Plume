
using Microsoft.EntityFrameworkCore;
using RLH.Plume.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
