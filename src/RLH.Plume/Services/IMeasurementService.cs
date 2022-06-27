using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLH.Plume.Entities;
using RLH.Results;

namespace RLH.Plume.Services
{
    public interface IMeasurementService :  IDisposable
    {
        Task<ResultOf<Measurement>> CreateAsync(string[] values);



    }
}
