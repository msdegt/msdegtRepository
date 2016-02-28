﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SmartHouse
{
    public class WateringSystem : Device, IStatus, IEnterLevel // автополив цветов
    {
        private WSMode statusWsMode;
        private int soilMoisture;

        public WateringSystem(bool status) : base(status)
        {
        }

        public void On() // включили 
        {
            if (Status == false)
            {
                Status = true;                             
            }
        }

        public void Off() // выключили
        {
            if (Status)
            {
                Status = false;
            }
        }

        public void EnterLevel(int input)
        {
            if (Status)
            {
                soilMoisture = input;
                if (soilMoisture <= 30)
                {
                    statusWsMode = WSMode.StrongMode;
                    Hydration();
                }
                else if (soilMoisture <= 60)
                {
                    statusWsMode = WSMode.MediumMode;
                    Hydration();
                }
                else
                {
                    statusWsMode = WSMode.WeakMode;
                    Hydration();
                }
            }
        }

        private void Hydration()
        {
            System.Threading.Thread.Sleep(10000);
            for (int i = 0; soilMoisture < 100; i++)
            {
                soilMoisture++;
            }
            Status = false;
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

            return "Состояние: " + status + ", режим орошения: " + mode + ", \nуровень влажности почвы: " + soilMoisture + "\n";
        }
    }
}
