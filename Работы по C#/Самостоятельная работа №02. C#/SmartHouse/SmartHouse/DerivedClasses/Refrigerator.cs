using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    class Refrigerator : Devices, IStatus, ISetTemperature, IRateOfOpening, ISetFreezeMode
    {

        private bool statusOpen;
        private TemperatureLevel freeze; // режимы заморозки
        private double temperature; // значение температуры
        private double temp;
        private bool lamp; // состояние лампочки горит или нет
        private bool beep; // // сигнал
        private string warning = "";  // предупреждения

        public event RefStateHandler FridgeActive;
        
        public bool StatusDoor { get; set; }
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

        public Refrigerator(bool status, bool statusDoor) : base(status) // конструктор
        {
            freeze = TemperatureLevel.ColderFreezing;
            temperature = 4;
            temp = temperature;
            StatusDoor = statusDoor;
        }
        public void On() // включили холодильник
        {
            Status = true;
            FridgeActive = () => EventLog.EventDevice("Холодильник включили");
            if (FridgeActive != null)
            {
                FridgeActive.Invoke();
            }
        }

        public void Off() // выключили холодильник
        {
            Status = false;
            FridgeActive = () => EventLog.EventDevice("Холодильник выключили");
            if (FridgeActive != null)
            {
                FridgeActive.Invoke();
            }
        }

        public void Open() // открыли дверцу холодильника
        {
            StatusDoor = true;
            FridgeActive = () => EventLog.EventDevice("Открыли дверцу холодильника");
            if (FridgeActive != null)
            {
                FridgeActive.Invoke();
            }
            if (Status)
            {
                lamp = true;
                warning = "";
                if (temperature < 6)
                {
                    System.Threading.Thread.Sleep(10000);
                    for (int i = 0; temperature < 8; i++)
                    {
                        temperature += 0.5;
                    }                    
                }
                if (temperature <= 8)
                {
                    beep = true; 
                    warning = "Необходимо закрыть дверцу холодильника!";
                }               
            }
        }

        public void Close() // закрыли дверцу холодильника
        {
            StatusDoor = false;
            FridgeActive = () => EventLog.EventDevice("Закрыли дверцу холодильника");
            if (FridgeActive != null)
            {
                FridgeActive.Invoke();
            }
            lamp = false;
            beep = false;
            warning = "";
            if (temperature > 2)
            {
                for (int i = 0; temperature > temp; i ++)
                {
                    temperature -= 0.5;
                }
            }
        }

        public void SetLevelTemperature(string input) // установили температуру холодильника
        {
            if (Status)
            {
                double t;
                if (Double.TryParse(input, out t))
                {
                    FridgeActive = () => EventLog.EventDevice("Установили температуру холодильника");
                    if (FridgeActive != null)
                    {
                        FridgeActive.Invoke();
                    }
                    if (t >= 2 && t <= 6)
                    {
                        temperature = t;
                        temp = temperature;
                        if (t >= 2 && t < 3)
                        {
                            freeze = TemperatureLevel.DeepFreeze;
                            warning = "";
                        }
                        if (t >= 3 && t < 5)
                        {
                            freeze = TemperatureLevel.ColderFreezing;
                            warning = "";
                        }
                        if (t >= 5 && t <= 6)
                        {
                            freeze = TemperatureLevel.LowFreeze;
                            warning = "";
                        }
                    }
                    else
                    {
                        warning = "Ошибка! Неверное значение температуры.";
                    }
                }
                else
                {
                    warning = "Ошибка! Некорректный ввод температуры.";
                }
            }
            else
            {
                warning = "Сначала надо включить холодильник";
            }                     
        }

        public void SetLowFreeze() // режим низкой заморозки холодильника
        {
            if (Status)
            {
                freeze = TemperatureLevel.LowFreeze;
                temperature = 6;
                temp = temperature;
                warning = "";
                FridgeActive = () => EventLog.EventDevice("Установили режим низкой заморозки холодильника");
                if (FridgeActive != null)
                {
                    FridgeActive.Invoke();
                }
            }
            else
            {
                warning = "Сначала надо включить холодильник";
            }
        }

        public void SetColderFreezing() // режим средней заморозки холодильника
        {
            if (Status)
            {
                freeze = TemperatureLevel.ColderFreezing;
                temperature = 4;
                temp = temperature;
                warning = "";
                FridgeActive = () => EventLog.EventDevice("Установили режим средней заморозки холодильника");
                if (FridgeActive != null)
                {
                    FridgeActive.Invoke();
                }
            }
            else
            {
                warning = "Сначала надо включить холодильник";
            }
        }

        public void SetDeepFreeze() // режим высокой заморозки холодильника
        {
            if (Status)
            {
                freeze = TemperatureLevel.DeepFreeze;
                temperature = 2;
                temp = temperature;
                warning = "";
                FridgeActive = () => EventLog.EventDevice("Установили режим высокой заморозки холодильника");
                if (FridgeActive != null)
                {
                    FridgeActive.Invoke();
                }
            }
            else
            {
                warning = "Сначала надо включить холодильник";
            }
        }

        public void SetDefrost() // режим разморозки холодильника
        {
            if (Status)
            {
                freeze = TemperatureLevel.Defrost;
                temperature = 15;
                Status = false;
                lamp = false;
                warning = "";
                FridgeActive = () => EventLog.EventDevice("Установили режим размораживания холодильника");
                if (FridgeActive != null)
                {
                    FridgeActive.Invoke();
                }
            }
            else
            {
                warning = "Сначала надо включить холодильник";
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

            string statusDoor;
            if (StatusDoor)
            {
                statusDoor = "открыта";
            }
            else
            {
                statusDoor = "закрыта";
            }

            string mode;
            if (freeze == TemperatureLevel.LowFreeze)
            {
                mode = "низкая";
            }
            else if (freeze == TemperatureLevel.ColderFreezing)
            {
                mode = "средняя";
            }
            else if (freeze == TemperatureLevel.DeepFreeze)
            {
                mode = "максимальная";
            }
            else
            {
                mode = "разморозка";
            }

            string lamp;
            if (this.lamp)
            {
                lamp = "горит";
            }
            else
            {
                lamp = "не горит";
            }

            string beep;
            if (this.beep)
            {
                beep = "включен";
            }
            else
            {
                beep = "выключен";
            }

            return "Состояние: " + status + ", статус двери: " + statusDoor + ", \nстепень заморозки: " + mode + ", значение температуры: " + temperature + ", \nсостояние лампочки: " + lamp + ", сигнал: " + beep + "\nСтрока состояния: " + warning + "\n";
        }
    }
}
