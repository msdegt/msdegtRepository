using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    class Television : Devices, IStatus, ISetChannel, IChannelSetup, ISetVolume
    { 
        public const int MAXchannel = 60; // максимальный номер канала, т.е всего можно смотреть 60 каналов
        private int currentVolume; // текущая громкость звука
        private int currentChannel; // текущий канал
        private int temp; // временная переменная
        private string warning;
        private bool chanState; // настроены каналы или нет
        private List<string> channels = new List<string>();
        private string namechannel; // название канала

        public Television(bool status) : base(status)
        { 
            chanState = false;
        }

        ///// включить
        public void On()
        {
            if (Status == false)
            {
                Status = true;
                currentChannel = 1;
                currentVolume = 10;
                warning = "";
            }
            else
            {
                warning = "Ошибка! Телевизор уже включен.";
            }
        }

        ///// выключить
        public void Off()
        {
            if (Status)
            {
                Status = false;
                currentVolume = 0;
                warning = "";
            }
            else
            {
                warning = "Ошибка! Телевизор уже выключен.";
            }
        }

        /// установить заданную громкость
        public void SetVolume(string input)
        {
            if (Status)
            {
                warning = "";
                int volume;
                if (Int32.TryParse(input, out volume))
                {
                    if (volume >= 0 && volume <= 100)
                    {
                        currentVolume = volume;
                        warning = "";
                    }
                    else
                    {
                        warning = "Ошибка! Неверное значение громкости.";
                    }
                }
                else
                {
                    warning = "Ошибка! Некорректный ввод громкости.";
                }
            }
            else
            {
                warning = "Сначала надо включить телевизор";
            }
        }

        ///// громкость больше
        public void MaxVolume()
        {
            if (Status)
            {
                warning = "";
                if (currentVolume < 100)
                {
                    currentVolume += 1;
                    warning = "";
                }
                else
                {
                    warning = "Ошибка! Это максимальная громкость.";
                }
            }
            else
            {
                warning = "Сначала надо включить телевизор";
            }
        }

        /// громкость меньше
        public void MinVolume()
        {
            if (Status)
            {
                warning = "";
                if (currentVolume > 0)
                {
                    currentVolume -= 1;
                    warning = "";
                }
                else
                {
                    warning = "Ошибка! Это минимальная громкость.";
                }
            }
            else
            {
                warning = "Сначала надо включить телевизор";
            }
        }

        ///// установить mute
        public void SetMute()
        {
            if (Status)
            {
                warning = "";
                currentVolume = 0;
            }
            else
            {
                warning = "Сначала надо включить телевизор";
            }
        }

        ///// следующий канал
        public void NextChannel()
        {
            if (Status)
            {
                warning = "";
                temp = currentChannel;
                if (currentChannel < MAXchannel)
                {
                    currentChannel += 1;
                }
                else
                {
                    currentChannel = 1;
                }
                NamChan();
            }
            else
            {
                warning = "Сначала надо включить телевизор";
            }
        }

        ///// канал, который был перед этим
        public void PreviousChannel()
        {
            if (Status)
            {
                warning = "";
                currentChannel = temp;
                NamChan();
            }
            else
            {
                warning = "Сначала надо включить телевизор";
            }
        }

        ///// предыдущий канал
        public void EarlyChannel()
        {
            if (Status)
            {
                warning = "";
                temp = currentChannel;
                if (currentChannel > 1)
                {
                    currentChannel -= 1;
                }
                if (currentChannel <= 1)
                {
                    currentChannel = MAXchannel;
                }
                NamChan();
            }
            else
            {
                warning = "Сначала надо включить телевизор";
            }           
        }

        ///// перейти на канал
        public void GoToChannel(string input)
        {
            if (Status)
            {
                warning = "";
                temp = currentChannel;
                int numChannel;
                if (Int32.TryParse(input, out numChannel))
                {
                    if (numChannel <= MAXchannel && numChannel > 0)
                    {
                        currentChannel = numChannel;
                        warning = "";

                        NamChan();
                    }
                    else
                    {
                        warning = "Ошибка. Такого канала не существует!";
                    }
                }
                else
                {
                    warning = "Ошибка! Некорректный ввод номера канала.";
                }
            }
            else
            {
                warning = "Сначала надо включить телевизор";
            }            
        }

        ///// поиск каналов
        public string ChannelScan()
        {
            string str = "";
            if (Status)
            {                
                warning = "";
                chanState = true;
                int frequency = 554;
                currentVolume = 0;
                for (int i = 1; i <= MAXchannel; i++)
                {
                    str += "\nНастраивается " + i + " канал...частота - " + frequency + "MHz";

                    if (frequency == 604)
                    {
                        channels.Add("1+1");
                    }
                    if (frequency == 654)
                    {
                        channels.Add("СТБ");
                    }
                    if (frequency == 704)
                    {
                        channels.Add("Disney");
                    }
                    if (frequency == 754)
                    {
                        channels.Add("Business");
                    }
                    if (frequency == 804)
                    {
                        channels.Add("ТНТ");
                    }
                    if (frequency == 854)
                    {
                        channels.Add("Iнтер");
                    }
                    if (frequency == 904)
                    {
                        channels.Add("TV1000");
                    }
                    if (frequency == 954)
                    {
                        channels.Add("Music box");
                    }
                    if (frequency == 1004)
                    {
                        channels.Add("Футбол2");
                    }
                    if (frequency == 1054)
                    {
                        channels.Add("National Geographic channel");
                    }

                    frequency += 50;
                }
                warning = "Процесс завершен. Все каналы настроены.";
                NamChan();
            }
            else
            {
                warning = "Сначала надо включить телевизор";
            }
            return str;            
        }

        ///// список каналов
        public string ListChannel()
        {
            string str = "\nСписок каналов: ";
            if (Status)
            {                
                warning = "";
                for (int i = 0; i < channels.Count; i++)
                {
                    str += "\n№" + (i + 1) + " - " + channels[i];
                }
            }
            else
            {
                warning = "Сначала надо включить телевизор";
            }
            return str;
        }

        // Название текущего канала
        protected void NamChan() 
        {
            if (chanState)
            {
                if (currentChannel <= channels.Count && currentChannel > 0)
                {
                    namechannel = channels[currentChannel - 1];
                }
                else
                {
                    namechannel = "";
                }
            }
            else
            {
                namechannel = "";
            }
        }

        // вывод инфо на экран
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
            string chanState;
            if (this.chanState)
            {
                chanState = "настроены";
            }
            else
            {
                chanState = "требуют настройки";
            }
            return "Состояние: " + status + ", уровень звука: " + currentVolume + ", текущий канал: " + currentChannel + ", \nимя текущего канала: " + namechannel + ", состояние каналов: " + chanState + "\nСтрока состояния: " + warning + "\n";
        }
    }
}
