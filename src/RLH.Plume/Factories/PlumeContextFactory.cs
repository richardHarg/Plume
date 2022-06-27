using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLH.JsonUtility;
using RLH.Plume.Configuration;
using RLH.Plume.Context;

namespace RLH.Plume.Factories
{
    public class PlumeContextFactory : IDesignTimeDbContextFactory<PlumeContext>
    {

        public PlumeContext CreateDbContext()
        {
            return CreateDbContext(new string[] { });
        }

        public PlumeContext CreateDbContext(string[] args)
        {
            using (IJsonFileParser parser = new JsonFileParser())
            {
                PlumeConfig config = parser.Parse<PlumeConfig>();
                var optionsBuilder = new DbContextOptionsBuilder<PlumeContext>();
                optionsBuilder.UseSqlServer(config.ConnectionString);
                return new PlumeContext(optionsBuilder.Options);
            }
        }
    }
}
