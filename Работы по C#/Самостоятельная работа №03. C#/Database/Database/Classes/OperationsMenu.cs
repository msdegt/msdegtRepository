using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class OperationsMenu : IOperations
    {
        public List<Employee> Employees { get; set; }


        public void AddEmp(string passportData, string name, string surname, string email, string tel, bool familyStatus, DateTime birthdate, string position, decimal salary, DateTime dateOfEmployment)
        {
            Employees.Add(new Employee(passportData, name, surname, email, tel, familyStatus, birthdate, position, salary, dateOfEmployment));
        }

        public void Del(string passportData)
        {
            for (int i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].PassportData == passportData)
                {
                    Employees.Remove(Employees[i]);
                }
            }
        }

        public Employee FindEmployee(string passportData)
        {
            Employee emp = Employees.Find(x => x.PassportData == passportData);
            return emp;
        }

        public List<Employee> SortSalary(decimal minSalary)
        {
            List < Employee > sortSalaryList = new List<Employee>();
            for (int i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].Salary >= minSalary)
                {
                    sortSalaryList.Add(Employees[i]);
                }
            }
            return sortSalaryList;
        }

        public List<Employee> SortDateOfEmployment(DateTime dateMin, DateTime dateMax)
        {
            List<Employee> sortDateList = new List<Employee>();
            for (int i = 0; i < Employees.Count; i++)
            {
                if (Employees[i].DateOfEmployment >= dateMin && Employees[i].DateOfEmployment <= dateMax)
                {
                    sortDateList.Add(Employees[i]);
                }
            }
            return sortDateList;
        }        
    }
}
