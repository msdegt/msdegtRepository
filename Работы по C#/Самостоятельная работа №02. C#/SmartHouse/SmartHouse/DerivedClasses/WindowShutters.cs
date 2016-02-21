using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SmartHouse
{
    class WindowShutters : Devices, IRateOfOpening, IStatus, ITimeOfDayMode
    {
        private bool statusOpen; // открыты или закрыты
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

            if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour <= 17 && Status == false && StatusOpen == false)  
            {
                Status = true;
                warning = "";
            }
            else if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour <= 17 && Status == false && StatusOpen)
            {
                StatusOpen = false;
                Status = true;
                warning = "";
            }
            else if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour <= 17 && Status)
            {               
                warning = "Ошибка! Жалюзи уже подняты.";
            }
        }
        
        public void SetEveningMode() // вечерний режим
        {
            statusMode = ShuttersMode.EveningMode;
            if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour <= 5 && Status)
            {
                warning = "";
                Status = false;
                if (StatusOpen)
                {                 
                    StatusOpen = false;
                }

            }
            else
            {
                if (StatusOpen)
                {
                    StatusOpen = false;
                    warning = "";
                }
                else
                {
                    warning = "Ошибка! Жалюзи уже опущены.";
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

            string mode = "";
            if (statusMode == ShuttersMode.MorningMode)
            {
                mode = "утренний";
            }
            else if (statusMode == ShuttersMode.EveningMode)
            {
                mode = "вечерний";
            }           

            return "Состояние открытия жалюзей: " + statusOpen + ", \nсостояние поднятия жалюзей: " + status + ", режим: " + mode + "\nСтрока состояния: " + warning + "\n";
        }

    }
}
