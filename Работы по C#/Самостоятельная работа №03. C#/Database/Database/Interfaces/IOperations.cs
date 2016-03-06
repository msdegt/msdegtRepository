using System;
using System.Collections.Generic;

namespace Database
{
    public interface IOperations
    {
        List<Employee> Employees { get; set; }
        void AddEmp(string passportData, string name, string surname, string email, string tel, bool familyStatus, DateTime birthdate, string position, 
            decimal salary, DateTime dateOfEmployment);

        void Del(string passportData);

        Employee FindEmployee(string passportData);

        List<Employee> SortSalary(decimal minSalary);

        List<Employee> SortDateOfEmployment(DateTime dateMin, DateTime dateMax);

    }
}