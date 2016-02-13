using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SmartHouse
{
    class WateringSystem : Devices, IStatus // автополив цветов
    {
        private bool sensorGround = true; // сработал или нет датчик влажности почвы
        private bool statusWater = true; // статус воды открыт или закрыт
        private string warning = "";
        private static int i;
        private static System.Timers.Timer timer = new System.Timers.Timer();

        public WateringSystem(bool status) : base(status)
        {
        }

        public void On() // включили 
        {
            if (Status == false)
            {
                Status = true;
                warning = "";
                System.Threading.Thread.Sleep(10000);
                TimePassed();
                if (sensorGround)
                {
                    statusWater = true;
                    WetGround();
                    if (sensorGround == false) // предполагается, что датчик срабатывает на полив воды и выключается когда уровень влажности нормализуется
                    {
                        statusWater = false;
                    }
                }
            }
            else
            {
                warning = "Ошибка! Автополив уже включен.";
            }
        }

        private bool TimePassed() // имитация что почву необходимо полить 
        {
            return sensorGround = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (i <= 100)
            {
                Console.WriteLine("Влажность почвы составляет {0}%", i);
            }
            if (i > 100)
            {
                timer.Stop();
                Console.WriteLine("Цветы политы! Нажмите любую клавишу");
            }
            i += 25;
        }

        private bool WetGround() // имитация что почва насытилась влагой
        {
            i = 0;
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 1000; //Устанавливаем интервал в 1 сек.
            timer.Enabled = true; //Вкючаем таймер. 
            Console.WriteLine("Процесс полива начался. Подождите, пожалуйста...");                      
            Console.ReadLine();
            return sensorGround = false;
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

            return "Состояние: " + status + "\nСтрока состояния: " + warning + "\n";
        }
    }
}
