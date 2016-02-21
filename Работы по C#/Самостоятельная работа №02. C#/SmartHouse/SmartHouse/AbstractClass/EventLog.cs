using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    abstract class EventLog
    {
        public static void EventDevice(string message)
        {
            try
            {
                StreamWriter sw = new StreamWriter("log.txt", true); // SmartHouse\SmartHouse\bin\Debug создается, а если есть такой файл - дописывает (т.к. true). 
                sw.WriteLine(message);
                sw.Close();
            }
            catch (Exception e)
            {
                ("Exception: " + e.Message).ToString();
            }
        }
    }
}
