using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    public class Refrigerator : Device, IStatus, ISetTemperature, IRateOfOpening, ISetFreezeMode
    {

        private bool lamp; // состояние лампочки горит или нет
        private bool beep; // // сигнал
        private TemperatureLevel freeze; // режимы заморозки
        private double temperature; // значение температуры
        private double temp;        

        public event RefStateHandler TurnedOn;
        public event RefStateHandler TurnedOff;
        public event RefStateHandler Opened;
        public event RefStateHandler Closed;
        public event RefStateHandler SetTemp;

        public event RefStateHandler SetLow;
        public event RefStateHandler SetColder;
        public event RefStateHandler SetDeep;
        public event RefStateHandler Defrosting;

        public Refrigerator(bool status, bool statusDoor, double temperature) : base(status) // конструктор
        {
            StatusOpen = statusDoor;
            Temperature = temperature;
        }

        public bool StatusOpen { get; set; }
        public double Temperature
        {
            get
            {
                return temperature;
            }

            set
            {
                if (value >= 2 && value <= 15 )
                {
                    if (value >= 2 && value < 3)
                    {
                        freeze = TemperatureLevel.DeepFreeze;
                        temperature = value;
                    }
                    if (value >= 3 && value < 5)
                    {
                        freeze = TemperatureLevel.ColderFreezing;
                        temperature = value;
                    }
                    if (value >= 5 && value <= 6)
                    {
                        freeze = TemperatureLevel.LowFreeze;
                        temperature = value;
                    }
                    if (value >= 6 && value <= 15)
                    {
                        freeze = TemperatureLevel.Defrost; //// !!!!
                        temperature = value;
                    }
                }
            }
        }
        
        public void On() // включили холодильник
        {
            if (Status == false)
            {
                Status = true;
                if (TurnedOn != null)
                {
                    TurnedOn.Invoke("Холодильник включили");
                }               
            }
        }

        public void Off() // выключили холодильник
        {
            if (Status)
            {
                Status = false;
                if (TurnedOff != null)
                {
                    TurnedOff.Invoke("Холодильник выключили");
                }                
            }
        }

        public void Open() // открыли дверцу холодильника
        {
            if (StatusOpen == false)
            {
                StatusOpen = true;
                if (Opened != null)
                {
                    Opened.Invoke("Открыли дверцу холодильника");
                }                
                lamp = true;
                if (Temperature >= 2 && Temperature <= 6)  
                {
                    System.Threading.Thread.Sleep(10000);
                    for (int i = 0; Temperature < 8; i++)
                    {
                        Temperature += 0.5;
                    }                    
                }
                if (Temperature <= 8)
                {
                    beep = true;
                }               
            }
        }

        public void Close() // закрыли дверцу холодильника
        {
            if (StatusOpen)
            {
                StatusOpen = false;
                if (Closed != null)
                {
                    Closed.Invoke("Закрыли дверцу холодильника");
                }
                lamp = false;
                beep = false;
                if (Temperature > 2) 
                {
                    for (int i = 0; Temperature > temp; i ++)
                    {
                        Temperature -= 0.5;
                    }
                }
            }
        }

        public void SetLowFreeze() // режим низкой заморозки холодильника
        {
            if (Status)
            {
                freeze = TemperatureLevel.LowFreeze;
                Temperature = 6;
                temp = Temperature;
                if (SetLow != null)
                {
                    SetLow.Invoke("Установили режим низкой заморозки холодильника");
                }
            }
        }

        public void SetColderFreezing() // режим средней заморозки холодильника
        {
            if (Status)
            {
                freeze = TemperatureLevel.ColderFreezing;
                Temperature = 4;
                temp = Temperature;
                if (SetColder != null)
                {
                    SetColder.Invoke("Установили режим средней заморозки холодильника");
                }
            }
        }

        public void SetDeepFreeze() // режим высокой заморозки холодильника
        {
            if (Status)
            {
                freeze = TemperatureLevel.DeepFreeze;
                Temperature = 2;
                temp = Temperature;
                if (SetDeep != null)
                {
                    SetDeep.Invoke("Установили режим высокой заморозки холодильника");
                }
            }
        }

        public void SetDefrost() // режим разморозки холодильника
        {
            if (Status)
            {
                freeze = TemperatureLevel.Defrost;
                Temperature = 15;
                Status = false;
                lamp = false;
                if (Defrosting != null)
                {
                    Defrosting.Invoke("Установили режим размораживания холодильника");
                }
            }
        }

        public void SetLevelTemperature(double input) // установить температуру холодильника
        {
            if (Status)
            {
                Temperature = input;
                temp = Temperature;
                if (SetTemp != null)
                    {
                        SetTemp.Invoke("Установили температуру холодильника");
                    }                                                                               
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
            if (StatusOpen)
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
            else if (freeze == TemperatureLevel.Defrost)
            {
                mode = "разморозка";
            }
            else
            {
                mode = "не определен";
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

            return "Состояние: " + status + ", статус двери: " + statusDoor + ", \nстепень заморозки: " + mode + ", значение температуры: " + Temperature + ", \nсостояние лампочки: " + lamp + ", сигнал: " + beep + "\n";
        }
    }
}
