using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class DrawTableResults : IDrawTable
    {
        public string MakeTableResults(List<Employee> listEmp) 
        {
            string sId = "+---+";
            string s0Passport = "--------+";
            string s1Name = "----------+";
            string s2Surname = "---------------+";
            string s3Tel = "-----------------+";
            string s4Position = "------------------+";
            string s = sId + s0Passport + s1Name + s2Surname + s3Tel + s4Position;
            string header = "| № |Паспорт |   Имя    |    Фамилия    |   № Телефона    |     Должность    |";

            string table = s + "\n" + header + "\n" + s + "\n";

            for (int i = 0; i < listEmp.Count; i++)
            {
                string id = (i + 1).ToString();
                string passport = listEmp[i].PassportData;
                string name = listEmp[i].Name;
                string surname = listEmp[i].Surname;
                string tel = listEmp[i].Tel;
                string position = listEmp[i].Position;

                if (id.Length < sId.Length)
                {
                    int num = sId.Length - id.Length;
                    for (int j = 0; j <= num - 3; j++)
                    {
                        id += " ";
                    }
                }
                if (passport.Length < s0Passport.Length)
                {
                    int num = s0Passport.Length - passport.Length;
                    for (int j = 0; j <= num - 2; j++)
                    {
                        passport += " ";
                    }
                }
                if (name.Length < s1Name.Length)
                {
                    int num = s1Name.Length - name.Length;
                    for (int j = 0; j <= num - 2; j++)
                    {
                        name += " ";
                    }
                }
                if (surname.Length < s2Surname.Length)
                {
                    int num = s2Surname.Length - surname.Length;
                    for (int j = 0; j <= num - 2; j++)
                    {
                        surname += " ";
                    }
                }
                if (tel.Length < s3Tel.Length)
                {
                    int num = s3Tel.Length - tel.Length;
                    for (int j = 0; j <= num - 4; j++)
                    {
                        tel += " ";
                    }
                }
                if (position.Length < s4Position.Length)
                {
                    int num = s4Position.Length - position.Length;
                    for (int j = 0; j <= num - 2; j++)
                    {
                        position += " ";
                    }
                }
                string str = "|" + id + "|" + passport + "|" + name + "|" + surname + "|" + tel + "|" + position + "|";
                table += str + "\n" + s + "\n";
            }
            return table;
        }
    }
}
