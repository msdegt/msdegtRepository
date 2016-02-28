using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
     class EventLog : ILog
    {
        public void EventDevice(string message)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("log.txt", true))
                {
                    sw.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                ("Exception: " + e.Message).ToString();
            }
        }
    }
}
