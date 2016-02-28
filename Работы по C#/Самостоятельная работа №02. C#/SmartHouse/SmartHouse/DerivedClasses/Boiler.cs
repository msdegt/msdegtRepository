using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    public class Boiler : Device, IModeHeating, ICustomMode, IStatus, ITemperature
    {
        private double temperature; // уровень температуры
        private BoilerMode statusMode; // режим  BoilerMode

        public Boiler(bool status, double temp) : base(status)
        {
            Temperature = temp;
        }

        public double Temperature
        {
            get
            {
                return temperature;
            }

            set
            {
                if (value >= 30 && value <= 90)
                {
                    if (value == 30)
                    {
                        statusMode = BoilerMode.MinMode;
                        temperature = value;
                    }
                    if (value == 90)
                    {
                        statusMode = BoilerMode.MaxMode;
                        temperature = value;
                    }
                    if (value != 30 && value != 90)
                    {
                        statusMode = BoilerMode.CustomMode;
                        temperature = value;
                    }
                }
            }
        }

        public void On() // включен 
        {
            if (Status == false)
            {
                Status = true;               
            }
        }

        public void Off() // выключен 
        {
            if (Status)
            {
                Status = false;
            }
        }

        public void SetMinMode() // минимальный режим
        {
            if (Status)
            {
                statusMode = BoilerMode.MinMode;
            }
        }

        public void SetMaxMode() // максимальный режим
        {
            if (Status)
            {
                statusMode = BoilerMode.MaxMode;
            }
        }

        public void SetCustomMode(double input) // пользовательский режим
        {
            if (Status)
            {
                statusMode = BoilerMode.CustomMode;
                Temperature = input;                                  
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
            else if (statusMode == BoilerMode.CustomMode)
            {
                mode = "пользовательский";
            }
            else
            {
                mode = "не определен";
            }

            return "Состояние: " + status + ", режим: " + mode + ", \nуровень температуры: " + Temperature + "\n";
        }
    }
}
