using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class CheckFileExists : ICheckFile
    {
        List<Employee> list;
        public List<Employee> CheckFile(ILog log, IReadFile read)
        {
            if (read.ReadLine() == "XML")
            {
                log = new LogXml();
                if (File.Exists("employees.xml"))
                {
                    list = (List<Employee>)log.LogOut();
                }
                else
                {
                    list = new List<Employee>();
                }
            }
            if (read.ReadLine() == "BIN")
            {
                log = new LogBinary();
                if (File.Exists("employees.dat"))
                {
                    list = (List<Employee>)log.LogOut();
                }
                else
                {
                    list = new List<Employee>();
                }
            }
            //list.Add(new Employee("AB325721", "Иван", "Кононов", "ivan@server1.proseware.com", "+38(099)345-45-67", false, new DateTime(1990, 9, 07), "Менеджер", 500, new DateTime(2015, 8, 23)));
            //list.Add(new Employee("АК859062", "Антонина", "Громова", "jones@ms1.proseware.com", "+38(097)256-15-68", true, new DateTime(1972, 7, 15), "Технолог", 1200, new DateTime(2010, 4, 19)));
            //list.Add(new Employee("AН216084", "Петр", "Логинов", "petr@yandex.ua", "+38(095)213-33-13", false, new DateTime(1984, 2, 20), "Маркетолог", 800, new DateTime(2014, 9, 03)));
            //list.Add(new Employee("ВК114267", "Ирина", "Фадеева", "ira@mail.ru", "+38(050)468-09-77", true, new DateTime(1976, 1, 09), "Бухгалтер", 1500, new DateTime(2011, 1, 13)));
            //list.Add(new Employee("ВН307643", "Семен", "Сорокин", "semen@yahoo.com", "+38(066)178-89-55", true, new DateTime(1978, 4, 26), "Юрист", 1000, new DateTime(2013, 10, 28)));

            return list;
        }
    }
}
