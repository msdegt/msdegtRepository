using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Database
{
    class LogXml : ILog
    {
        public void LogSave(object entity)
        {
            List<Employee> newEmployees = (List<Employee>) entity;
            XmlSerializer xs = new XmlSerializer(typeof(List<Employee>));
            using (FileStream fs = new FileStream("employees.xml", FileMode.OpenOrCreate))
            {
                xs.Serialize(fs, newEmployees);
            }
        }

        public object LogOut()
        {
            List<Employee> newEmployees;
            using (FileStream fs = new FileStream("employees.xml", FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Employee>));
                newEmployees = (List<Employee>)xs.Deserialize(fs);
            }
            return newEmployees;
        }
    }
}
