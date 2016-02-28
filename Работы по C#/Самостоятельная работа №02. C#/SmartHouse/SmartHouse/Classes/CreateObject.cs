using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    class CreateObject : ICreate
    {
        public ILog Log { get; set; }
        public Refrigerator CreateRef()
        {
            Log = new EventLog();
            Refrigerator r = new Refrigerator(false, false, 122);
            r.TurnedOn += (message) => Log.EventDevice(message);
            r.TurnedOff += (message) => Log.EventDevice(message);
            r.Opened += (message) => Log.EventDevice(message);
            r.Closed += (message) => Log.EventDevice(message);
            r.SetTemp += (message) => Log.EventDevice(message);
            r.SetLow += (message) => Log.EventDevice(message);
            r.SetColder += (message) => Log.EventDevice(message);
            r.SetDeep += (message) => Log.EventDevice(message);
            r.Defrosting += (message) => Log.EventDevice(message);
            return r;
        }

        public Television CreateTv()
        {
            return new Television(false, 100);
        }

        public WindowShutters CreateShut()
        {
            return new WindowShutters(false, true);
        }

        public Boiler CreateBoiler()
        {
            return new Boiler(false, 45); 
        }

        public WateringSystem CreateWs()
        {
            return new WateringSystem(false);
        }
    }
}
