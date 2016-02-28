using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    public abstract class Device
    {
        public bool Status { get; set; }

        public abstract override string ToString();

        public Device(bool status)
        {
            Status = status;
        }
    }
}
