using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class LogBinary : ILog
    {
        BinaryFormatter bf = new BinaryFormatter();
        public void LogSave(object entity)
        {
            List<Employee> newEmployees = (List<Employee>)entity;
            using (FileStream fs = new FileStream("employees.dat", FileMode.OpenOrCreate))
            {
                bf.Serialize(fs, newEmployees);
            }
        }

        public object LogOut()
        {
            List<Employee> newEmployees;
            using (FileStream fs = new FileStream("employees.dat", FileMode.Open))
            {
                newEmployees = (List<Employee>)bf.Deserialize(fs);
            }
            return newEmployees;
        }
    }
}
