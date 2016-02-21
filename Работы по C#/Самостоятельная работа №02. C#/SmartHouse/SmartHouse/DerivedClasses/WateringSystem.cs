using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SmartHouse
{
    class WateringSystem : Devices, IStatus, IEnterLevel // автополив цветов
    {
        private bool statusWater; // статус воды открыт или закрыт
        private string warning = "";
        private WSMode statusWsMode;
        int soilMoisture;

        public WateringSystem(bool status) : base(status)
        {
        }

        public void On() // включили 
        {
            if (Status == false)
            {
                Status = true;
                warning = "";               
            }
            else
            {
                warning = "Ошибка! Автополив уже включен.";
            }
        }

        public void EnterLevel(string input)
        {
            if (Status)
            {
                if (Int32.TryParse(input, out soilMoisture))
                {
                    if (soilMoisture <= 100 && soilMoisture >= 0)
                    {

                        warning = "";
                        if (soilMoisture <= 30)
                        {
                            statusWsMode = WSMode.StrongMode;
                            statusWater = true;
                            Hydration();
                        }
                        else if (soilMoisture <= 60)
                        {
                            statusWsMode = WSMode.MediumMode;
                            statusWater = true;
                            Hydration();
                        }
                        else
                        {
                            statusWsMode = WSMode.WeakMode;
                            statusWater = true;
                            Hydration();
                        }
                    }
                    else
                    {
                        soilMoisture = 0;
                        warning = "Ошибка! Неверное значение уровня влажности почвы.";
                    }
                }
                else
                {
                    warning = "Ошибка! Некорректный ввод уровня влажности почвы.";
                }
            }
            else
            {
                warning = "Ошибка! Сначала надо включить автополив.";
            }
        }

        private void Hydration()
        {
            System.Threading.Thread.Sleep(10000);
            for (int i = 0; soilMoisture < 100; i++)
            {
                soilMoisture ++;
            }
            statusWater = false;
            Status = false;
        }


        public void Off() // выключили
        {
            if (Status)
            {
                Status = false;
                statusWater = false;
                warning = "";
            }
            else
            {
                warning = "Ошибка! Автополив уже выключен.";
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

            string mode = "";
            if (statusWsMode == WSMode.StrongMode)
            {
                mode = "100 капель";
            }
            else if (statusWsMode == WSMode.MediumMode)
            {
                mode = "80 капель";
            }
            else if (statusWsMode == WSMode.WeakMode)
            {
                mode = "30 капель";
            }

            return "Состояние: " + status + ", режим орошения: " + mode + ", \nуровень влажности почвы: " + soilMoisture + "\nСтрока состояния: " + warning + "\n";
        }
    }
}
