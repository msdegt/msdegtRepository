using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    class Boiler : Devices, IModeHeating, ICustomMode, IStatus 
    {
        private int temp; // уровень температуры
        private BoilerMode statusMode; // режим  BoilerMode
        private string warning;

        public Boiler(bool status) : base(status)
        {
            statusMode = BoilerMode.MinMode;
            temp = 30;
        }

        public void On() // включен 
        {
            if (Status == false)
            {
                Status = true;
                warning = "";
            }
            else
            {
                warning = "Ошибка! Бойлер уже включен.";
            }
        }

        public void Off() // выключен 
        {
            if (Status)
            {
                Status = false;
                warning = "";
            }
            else
            {
                warning = "Ошибка! Бойлер уже выключен.";
            }
        }

        public void SetMinMode() // минимальный режим
        {
            if (Status)
            {
                statusMode = BoilerMode.MinMode;
                temp = 30;
                warning = "";
            }
            else
            {
                warning = "Сначала надо включить бойлер.";
            }           
        }

        public void SetMaxMode() // максимальный режим
        {
            if (Status)
            {
                statusMode = BoilerMode.MaxMode;
                temp = 90;
                warning = "";
            }
            else
            {
                warning = "Сначала надо включить бойлер.";
            }
        }

        public void SetCustomMode(string input) // пользовательский режим
        {
            if (Status)
            {
                int t;
                if (Int32.TryParse(input, out t))
                {
                    if (t > 30 && t < 90)
                    {
                        statusMode = BoilerMode.CustomMode;
                        temp = t;
                        warning = "";
                    }
                    else
                    {
                        warning = "Ошибка! Недопустимое значение температуры.";
                    }
                }
                else
                {
                    warning = "Ошибка! Некорректный ввод температуры.";
                }
            }
            else
            {
                warning = "Сначала надо включить бойлер.";
            }
        }

        public override string ToString() 
        {
            string status;
            if (Status)
            {
                status = "включен";
            }
            else
            {
                status = "выключен";
            }

            string mode;
            if (statusMode == BoilerMode.MinMode)
            {
                mode = "минимальный";
            }
            else if (statusMode == BoilerMode.MaxMode)
            {
                mode = "максимальный";
            }
            else
            {
                mode = "пользовательский";
            }

            return "Состояние: " + status + ", режим: " + mode + ", \nуровень температуры: " + temp + "\nСтрока состояния: " + warning + "\n";
        }
    }
}
