using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    [Serializable]
    public abstract class Person
    {
        private string passportData;
        private string name;
        private string surname;

        public Person() { }

        public Person(string passportData, string name, string surname, string email, string tel, bool familyStatus, DateTime birthdate)
        {
            PassportData = passportData;
            Name = name;
            Surname = surname;
            Email = email;
            Tel = tel;
            FamilyStatus = familyStatus;
            Birthdate = birthdate;
        }

        public string Email { get; set; }
        public string Tel { get; set; }
        public bool FamilyStatus { get; set; }
        public DateTime Birthdate { get; set; }
        
        public string PassportData
        {
            get { return passportData; }
            set
            {
                if (value.Length > 0 && value.Length <= 8)
                {
                    passportData = value;
                }
                else
                {
                    passportData = "";
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (value.Length > 0 && value.Length <= 10)
                {
                    name = value;
                }
                else
                {
                    name = "";
                }
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                if (value.Length > 0 && value.Length <= 15)
                {
                    surname = value;
                }
                else
                {
                    surname = "";
                }
            }
        }
        
        public override string ToString()
        {
            string familyStatus;
            if (FamilyStatus)
            {
                familyStatus = "женат/замужем";
            }
            else
            {
                familyStatus = "холост/не замужем";
            }
            string dB = Birthdate.ToString("dd.MM.yy");
            return "ФИО: " + Name + " " + Surname + "\n\nЛичные данные: " + "\n\tНомер паспорта: " + PassportData +
                   ", \n\tНомер телефона: " + Tel + ", \n\tСемейный статус: " + familyStatus + ", \n\tДата рождения: " +
                   dB + ", \n\tEmail: " + Email;
        }
    }
}
