using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    abstract class Devices
    {
        public bool Status { get; set; }

        public abstract override string ToString();

        public Devices(bool status)
        {
            Status = status;
        }
    }
}
