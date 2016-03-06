using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    [Serializable]
    public class Employee : Person
    {
        private decimal salary;
        private string position;
        public Employee() { }
        public Employee(string passportData, string name, string surname, string email, string tel, bool familyStatus, DateTime birthdate, string position, decimal salary, DateTime dateOfEmployment) : base(passportData, name, surname, email, tel, familyStatus, birthdate)
        {
            Position = position;
            Salary = salary;
            DateOfEmployment = dateOfEmployment;            
        }


        public DateTime DateOfEmployment { get; set; } // дата принятия на работу
        
        public decimal Salary
        {
            get { return salary; }
            set
            {
                if (value >= 0)
                {
                    salary = value;
                }
            }
        }
        public string Position
        {
            get { return position; }
            set
            {
                if (value.Length > 0 && value.Length <= 18)
                {
                    position = value;
                }
                else
                {
                    position = "";
                }
            }
        }
        
        public override string ToString()
        {

            string dE = DateOfEmployment.ToString("dd.MM.yy");
            return base.ToString() + "\n\n" + "Персональные данные относительно работы в компании: " + "\n\tДолжность: " + Position + 
                ", \n\tДолжностной оклад: " + Salary + " $" + ", \n\tДата приема на работу: " + dE;
        }
    }
}
