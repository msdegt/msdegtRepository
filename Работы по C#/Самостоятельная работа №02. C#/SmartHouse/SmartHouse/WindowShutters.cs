using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SmartHouse
{
    class WindowShutters : Devices, IRateOfOpening, ICustomMode, IStatus, ITimeOfDayMode
    {
        private bool statusOpen; // открыты или закрыты
        private string time = DateTime.Now.ToString("HH:mm");
        private static System.Timers.Timer timer = new System.Timers.Timer();
        private static DateTime t1;
        private ShuttersMode statusMode; // режим
        private string warning;

        public bool StatusOpen
        {
            get
            {
                return statusOpen;
            }

            set
            {
                statusOpen = value;
            }
        }

        public WindowShutters(bool status, bool statOp) : base(status)
        {
            StatusOpen = statOp;
            statusMode = ShuttersMode.MorningMode;
        }

        public void On() // жалюзи поднять
        {
            if (Status == false)
            {
                Status = true;
                warning = "";
            }
            else
            {
                warning = "Ошибка! Жалюзи уже подняты.";
            }
        }

        public void Off() // жалюзи опустить
        {
            if (Status)
            {
                Status = false;
                warning = "";
            }
            else
            {
                warning = "Ошибка! Жалюзи уже опущены.";
            }
        }

        public void Open() // жалюзи открыты
        {
            if (StatusOpen == false)
            {
                StatusOpen = true;
                warning = "";
            }
            else
            {
                warning = "Ошибка! Жалюзи уже открыты.";
            }
        }

        public void Close() // жалюзи закрыты
        {
            if (StatusOpen)
            {
                StatusOpen = false;
                warning = "";
            }
            else
            {
                warning = "Ошибка! Жалюзи уже закрыты.";
            }
        }

        public void SetMorningMode() // утренний режим
        {
            statusMode = ShuttersMode.MorningMode;
            if (DateTime.Now.Hour == 6 && DateTime.Now.Minute == 30 && StatusOpen == false)  
            {
                Status = true;
            }
            if (time == "6:30" && StatusOpen)
            {
                StatusOpen = false;
                Status = true;
            }
        }

        public void SetEveningMode() // вечерний режим
        {
            statusMode = ShuttersMode.EveningMode;
            if (DateTime.Now.Hour == 18 && DateTime.Now.Minute == 00 && Status)
            {
                Status = false;
                warning = "";
            }
            if (time == "18:00" && Status == false)
            {
                warning = "Ошибка! Жалюзи уже опущены.";
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e) // событие для таймера
        {
            Console.WriteLine(DateTime.Now);
            if (DateTime.Now.Hour == t1.Hour && DateTime.Now.Minute == t1.Minute)
            {
                timer.Stop();
            }
        }

        private void Timer() 
        {
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 5000; //Устанавливаем интервал в 5 сек.
            timer.Enabled = true; //Вкючаем таймер. 
            Console.WriteLine("Текущее время: ");
            Console.ReadLine();
        }



        public void SetCustomMode() // пользовательский режим
        {
            Console.WriteLine("Введите время в вормате hh:mm : ");   
            string t = Console.ReadLine();
            if (DateTime.TryParse(t, out t1) == false)
            {
               warning = "Некорректный ввод времени";
            }
            else
            {
                Console.WriteLine("Введите 1, если необходимо опустить жалюзи или 2, если необходимо поднять.");
                int command = Int32.Parse(Console.ReadLine());

                if (command != 1 || command != 2)
                {
                    warning = "Такой команды не существует!";
                }

                Timer();//

                if (command == 1 && DateTime.Now.Hour == t1.Hour && DateTime.Now.Minute == t1.Minute && Status == true)
                {
                    Status = false;
                    statusMode = ShuttersMode.CustomMode;
                    warning = "";
                }
                if (command == 1 && DateTime.Now.Hour == t1.Hour && DateTime.Now.Minute == t1.Minute && Status == false)
                {
                    statusMode = ShuttersMode.CustomMode;
                    warning = "Ошибка! Жалюзи уже опущены.";
                }
                if (command == 2 && DateTime.Now.Hour == t1.Hour && DateTime.Now.Minute == t1.Minute && StatusOpen == false)
                {
                    Status = true;
                    statusMode = ShuttersMode.CustomMode;
                    warning = "";
                }
                if (command == 2 && DateTime.Now.Hour == t1.Hour && DateTime.Now.Minute == t1.Minute && StatusOpen == true)
                {
                    StatusOpen = false;
                    Status = true;
                    statusMode = ShuttersMode.CustomMode;
                    warning = "";
                }
            }
        }

        public override string ToString() 
        {
            string statusOpen;
            if (StatusOpen)
            {
                statusOpen = "жалюзи открыты";
            }
            else
            {
                statusOpen = "жалюзи закрыты";
            }

            string status;
            if (Status)
            {
                status = "жалюзи подняты";
            }
            else
            {
                status = "жалюзи опущены";
            }

            string mode;
            if (statusMode == ShuttersMode.MorningMode)
            {
                mode = "утренний";
            }
            else if (statusMode == ShuttersMode.EveningMode)
            {
                mode = "вечерний";
            }
            else
            {
                mode = "пользовательский";
            }

            return "Состояние открытия жалюзей: " + statusOpen + ", \nсостояние поднятия жалюзей: " + status + ", режим: " + mode + "\nСтрока состояния: " + warning + "\n";
        }

    }
}
