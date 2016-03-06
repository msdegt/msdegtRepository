using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Database
{
    class Menu
    {
        private string text;
        private string pasp;
        private string pasport = @"^[А-ЯЁA-Z]{2}[0-9]{6}$";

        public ILog Log { get; set; }

        public void Show(IOperations operations, IValidate validate, IReadFile read, ICheckFile check, IDrawTable draw)
        {
            operations.Employees = check.CheckFile(Log, read);
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите команду: ");
                string command = Console.ReadLine();
                
                switch (command.ToLower())
                {
                    case "add":
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите паспортные данные сотрудника: "); 
                            pasp = Console.ReadLine();
                            if (validate.Check(pasp, pasport) == false)
                            {
                                Console.WriteLine("Некорректный ввод паспортных данных сотрудника: ");
                                Console.ReadKey();
                            }
                            else
                            {
                                break;
                            }
                        }
                            
                        if (operations.Employees.Exists(x => x.PassportData == pasp) == false)
                            {
                                string name;
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Введите имя сотрудника (не более 10 символов): ");
                                    name = Console.ReadLine();
                                    if (name.Length > 10)
                                    {
                                        Console.WriteLine("Некорректный ввод имени сотрудника!");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                string surname;
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Введите фамилию сотрудника (не более 15 символов): ");
                                    surname = Console.ReadLine();
                                    if (surname.Length > 15)
                                    {
                                        Console.WriteLine("Некорректный ввод фамилии сотрудника!");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                    
                                string email;
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Введите email сотрудника: ");
                                    email = Console.ReadLine();
                                    string mail = @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"; 
                                    if (validate.Check(email, mail) == false)
                                    {
                                        Console.WriteLine("Некорректный ввод почты сотрудника: ");
                                        Console.ReadKey();
                                    } 
                                    else
                                    {
                                        break;
                                    }
                                }

                                string tel;
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Введите телефон сотрудника: ");
                                    tel = Console.ReadLine();
                                    string tel1 = @"^\+\d{2}\(\d{3}\)\d{3}-\d{2}-\d{2}$";
                                    if (validate.Check(tel, tel1) == false)
                                    {
                                        Console.WriteLine("Некорректный ввод номера телефона сотрудника: ");
                                        Console.ReadKey();
                                    } 
                                    else
                                    {
                                        break;
                                    }
                                }

                                bool statusF;
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Введите 'False', если сотрудник холост/не замужем, в противном случае - введите 'True': ");
                                    text = Console.ReadLine();
                                    if (Boolean.TryParse(text, out statusF) == false)
                                    {
                                        Console.WriteLine("Некорректный ввод семейного положения сотрудника: ");
                                        Console.ReadKey();
                                    } 
                                    else
                                    {
                                        break;
                                    }
                                }

                                DateTime bDate;
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Введите дату рождения сотрудника в формате dd.mm.yy: ");
                                    text = Console.ReadLine();
                                    if (DateTime.TryParse(text, out bDate) == false)
                                    {
                                        Console.WriteLine("Некорректный ввод даты рождения сотрудника: ");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                string position;
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Введите должность сотрудника (не более 18 символов): ");
                                    position = Console.ReadLine();
                                    if (surname.Length > 18)
                                    {
                                        Console.WriteLine("Некорректная длина пасортных данных сотрудника!");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                decimal salary;
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Введите должностной оклад сотрудника: ");
                                    text = Console.ReadLine();
                                    if (Decimal.TryParse(text, out salary) == false || salary < 0)
                                    {
                                        Console.WriteLine("Некорректный ввод должностного оклада сотрудника: ");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                DateTime eDate;
                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Введите дату приема на работу сотрудника в формате dd.mm.yy: ");
                                    text = Console.ReadLine();
                                    if (DateTime.TryParse(text, out eDate) == false)
                                    {
                                        Console.WriteLine("Некорректный ввод даты приема на работу сотрудника: ");
                                        Console.ReadKey();
                                    } 
                                    else
                                    {
                                        break;
                                    }
                                }

                                operations.AddEmp(pasp, name, surname, email, tel, statusF, bDate, position, salary, eDate);
                                Console.WriteLine("Сотрудник успешно добавлен в базу данных.");
                            }
                            else
                            {
                                Console.WriteLine("Сотрудник с такими паспортными данными уже существует!");
                            }
                            Console.ReadKey();
                        break;
                    case "del":
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите паспортные данные сотрудника: ");
                            pasp = Console.ReadLine();
                            if (validate.Check(pasp, pasport) == false)
                            {
                                Console.WriteLine("Некорректный ввод паспортных данных сотрудника: ");
                                Console.ReadKey();
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (operations.Employees.Exists(x => x.PassportData == pasp))
                        {
                            operations.Del(pasp);
                            Console.WriteLine("Удаление прошло успешно.");
                        }
                        else
                        {
                            Console.WriteLine("Сотрудника с такими паспортными данными не существует!");
                        }
                        Console.ReadKey();
                        break;
                    case "see_all":
                        Console.WriteLine(draw.MakeTableResults(operations.Employees));
                        Console.ReadKey();
                        break;
                    case "find_emp":
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите паспортные данные сотрудника: ");
                            pasp = Console.ReadLine();
                            if (validate.Check(pasp, pasport) == false)
                            {
                                Console.WriteLine("Некорректный ввод паспортных данных сотрудника: ");
                                Console.ReadKey();
                            }
                            else
                            {
                                break;
                            }
                        }
                        Console.Clear();
                        if (operations.Employees.Exists(x => x.PassportData == pasp))
                        {
                            Console.WriteLine(operations.FindEmployee(pasp).ToString());                                   
                        }
                        else
                        {
                            Console.WriteLine("Сотрудника с такими паспортными данными не существует!");
                        }
                        Console.ReadKey();
                        break;
                    case "sort_salary":
                        decimal min;
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите нижнюю границу зарплаты для сортировки: ");
                            text = Console.ReadLine();
                            if (Decimal.TryParse(text, out min) == false || min < 0)
                            {
                                Console.WriteLine("Некорректный ввод данных.");
                                Console.ReadKey();
                            }
                            else
                            {
                                break;
                            }
                        }
                        Console.WriteLine(draw.MakeTableResults(operations.SortSalary(min)));
                        Console.ReadKey();
                        break;

                    case "sort_date":
                        string date;
                        DateTime dateMin;
                        DateTime dateMax;
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите начало периода сортировки: ");
                            text = Console.ReadLine();
                            Console.WriteLine("Введите конец периода сортировки: ");
                            date= Console.ReadLine();
                            if (DateTime.TryParse(text, out dateMin) == false || DateTime.TryParse(date, out dateMax) == false)
                            {
                                Console.WriteLine("Некорректный ввод данных.");
                                Console.ReadKey();
                            }
                            else
                            {
                                break;
                            }
                        }
                        Console.WriteLine(draw.MakeTableResults(operations.SortDateOfEmployment(dateMin, dateMax)));
                        Console.ReadKey();
                        break;

                    case "exit":
                        if (read.ReadLine() == "XML")
                        {
                            Log = new LogXml();
                        }
                        if (read.ReadLine() == "BIN")
                        {
                            Log = new LogBinary();
                        }
                        Log.LogSave(operations.Employees);
                        return;
                }
                if (command.ToLower() != "add" && command.ToLower() != "del" && command.ToLower() != "see_all" && command.ToLower() != "find_emp" && command.ToLower() != "sort_salary" && command.ToLower() != "sort_date" && command.ToLower() != "exit")
                {
                    Help();
                }
            }
        }

        private static void Help()
        {
            Console.WriteLine("Доступные команды:");
            Console.WriteLine("\tadd создать запись сотрудника");
            Console.WriteLine("\tdel удалить запись сотрудника");
            Console.WriteLine("\tsee_all просмотреть все записи сотрудников");
            Console.WriteLine("\tfind_emp просмотреть запись конкретного сотрудника");
            Console.WriteLine("\tsort_salary сортировка сотрудников относительно нижней границы зарплаты");
            Console.WriteLine("\tsort_date сортировка сотрудников за данный период поступления на роботу");

            Console.WriteLine("\n \texit выход");
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadKey();
        }              
    }
}
