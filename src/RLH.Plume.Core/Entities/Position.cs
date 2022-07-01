using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Core.Entities
{
    public sealed class Position
    {
        [Key]
        public string Timestamp { get; set; }
        [Required]
        public double latitude { get; set; }
        [Required]
        public double longitude{ get; set; }
    }
}
