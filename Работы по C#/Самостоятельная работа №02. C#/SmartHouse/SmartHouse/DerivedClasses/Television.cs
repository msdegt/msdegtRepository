using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse
{
    public class Television : Device, ISetChannel, IChannelSetup, ISetVolume
    { 
        private int maxChannel; // максимальный номер канала, т.е всего можно смотреть 60 каналов
        private bool chanState; // настроены каналы или нет
        private int currentVolume; // текущая громкость звука
        private int currentChannel; // текущий канал
        private int temp; // временная переменная
        private string namechannel; // название канала
        private List<string> channels;        

        public Television(bool status, int maxChannel) : base(status)
        {
            MAXchannel = maxChannel;
        }

        public int MAXchannel { get; set; }
        public int CurrentVolume
        {
            get
            {
                return currentVolume;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                   currentVolume = value;
                }               
            }
        }

        public int CurrentChannel
        {
            get
            {
                return currentChannel;
            }
            set
            {
                if (value <= MAXchannel && value > 0)
                {
                    currentChannel = value;
                }
            }
        }

        ///// включить
        public override void On()
        {
            if (Status == false)
            {
                Status = true;
                CurrentChannel = 1;
            }
        }
       

        ///// громкость больше
        public void MaxVolume()
        {
            if (Status)
            {
                if (CurrentVolume < 100)
                {
                    CurrentVolume += 1;
                }                
            }
        }

        /// громкость меньше
        public void MinVolume()
        {
            if (Status)
            {
                if (CurrentVolume > 0)
                {
                    CurrentVolume -= 1;
                }                
            }
        }

        ///// установить mute
        public void SetMute()
        {
            if (Status)
            {
                CurrentVolume = 0;
            }
        }

        ///// следующий канал
        public void NextChannel()
        {
            if (Status)
            {
                temp = CurrentChannel;
                if (CurrentChannel < MAXchannel)
                {
                    CurrentChannel += 1;
                }
                else
                {
                    CurrentChannel = 1;
                }
                NamChan();
            }
        }

        ///// канал, который был перед этим
        public void PreviousChannel()
        {
            if (Status)
            {
                CurrentChannel = temp;
                NamChan();
            }
        }

        ///// предыдущий канал
        public void EarlyChannel()
        {
            if (Status)
            {
                temp = CurrentChannel;
                if (CurrentChannel > 1)
                {
                    CurrentChannel -= 1;
                }
                else
                {
                    CurrentChannel = MAXchannel;
                }
                NamChan();
            }
        }

        // Название текущего канала
        protected void NamChan()
        {
            if (chanState)
            {
                if (CurrentChannel <= channels.Count && CurrentChannel > 0)
                {
                    namechannel = channels[CurrentChannel - 1];
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

        /// установить заданную громкость
        public void SetVolume(int input)
        {
            if (Status)
            {
                CurrentVolume = input;                                  
            }
        }
        
        ///// перейти на канал
        public void GoToChannel(int input)
        {
            if (Status)
            {
                temp = CurrentChannel;
                CurrentChannel = input;
                NamChan();                    
            }
        }
        
        ///// поиск каналов
        public string ChannelScan()
        {
            string str = "";
            if (Status)
            {
                channels = new List<string>();
                chanState = true;
                int frequency = 554;
                CurrentVolume = 0;
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
                str += "\nПроцесс завершен. Все каналы настроены. \nНажмите любую клавишу.";
                NamChan();
            }
            return str;            
        }

        ///// список каналов
        public string ListChannel()
        {
            string str = "\nСписок каналов: ";
            if (Status)
            {                
                for (int i = 0; i < channels.Count; i++)
                {
                    str += "\n№" + (i + 1) + " - " + channels[i];
                }
            }
            return str;
        }

        // вывод инфо на экран
        public override string ToString()
        {
            string chanState;
            if (this.chanState)
            {
                chanState = "настроены";
            }
            else
            {
                chanState = "требуют настройки";
            }

            return base.ToString() + ", уровень звука: " + CurrentVolume + ", текущий канал: " + CurrentChannel + ", \nимя текущего канала: " + namechannel + ", состояние каналов: " + chanState + "\n";
        }
    }
}
