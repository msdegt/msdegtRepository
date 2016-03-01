using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    public abstract class Device : IStatus
    {
        public Device(bool status)
        {
            Status = status;
        }

        public bool Status { get; set; }

        public virtual void On() // включили 
        {
            if (Status == false)
            {
                Status = true;
            }
        }

        public virtual void Off() // выключили
        {
            if (Status)
            {
                Status = false;
            }
        }

        public override string ToString()
        {
            string status;
            if (Status)
            {
                status = "включен";
            }
            else
            {
                status = "выключен";
            }
            return "Состояние: " + status;
        }
    }
}