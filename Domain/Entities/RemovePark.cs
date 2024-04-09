using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RemovePark
    {
        public bool Removed { get; set; }
        public string LicensePlate { get; set; }
        public string Message { get; set; }
    }
}
