using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SmartHouse
{
    class Menu
    {
        private IDictionary<string, Device> DevicesDictionary = new Dictionary<string, Device>();

        private int temp;
        private double t;
        public string Input { get; set; }
        public ICreate CO { get; set; } 

        public void Show()
        {
            CO = new CreateObject();
            
            DevicesDictionary.Add("TV1", CO.CreateTv());
            DevicesDictionary.Add("Ref1", CO.CreateRef()); 
            DevicesDictionary.Add("Shut1", CO.CreateShut());
            DevicesDictionary.Add("Boiler1", CO.CreateBoiler());
            DevicesDictionary.Add("WS1", CO.CreateWs());

            while (true)
            {
                Console.Clear();
                foreach (var dev in DevicesDictionary)
                {
                    Console.WriteLine("Название: " + dev.Key + ", " + dev.Value.ToString());
                }
                Console.WriteLine();
                Console.Write("Введите команду: ");

                string[] commands = Console.ReadLine().Split(' ');
                if (commands[0].ToLower() == "exit" & commands.Length == 3)
                {
                    return;
                }
                if (commands.Length != 3) 
                {
                    Help();
                    continue;
                }
                if (commands[0].ToLower() == "add" && !DevicesDictionary.ContainsKey(commands[2]))
                {
                    if (commands[1] == "TV")
                    {
                        DevicesDictionary.Add(commands[2], CO.CreateTv());
                        continue;
                    }
                    if (commands[1] == "ref")
                    {
                        DevicesDictionary.Add(commands[2], CO.CreateRef());
                        continue;
                    }
                    if (commands[1] == "shut")
                    {
                        DevicesDictionary.Add(commands[2], CO.CreateShut());
                        continue;
                    }
                    if (commands[1] == "boiler")
                    {
                        DevicesDictionary.Add(commands[2], CO.CreateBoiler());
                        continue;
                    }
                    if (commands[1] == "WS")
                    {
                        DevicesDictionary.Add(commands[2], CO.CreateWs());
                        continue;
                    }
                }

                if (commands[0].ToLower() == "add" && DevicesDictionary.ContainsKey(commands[2]))
                {
                    Console.WriteLine("Устройство с таким именем уже существует");
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadLine();
                    continue;
                }

                if (commands[0].ToLower() == "del" && !DevicesDictionary.ContainsKey(commands[2]))
                {
                    Console.WriteLine("Выполнение команды невозможно, т.к. устройство с таким именем не существует");
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadLine();
                    continue;
                }

                if (!DevicesDictionary.ContainsKey(commands[2]))
                {
                    Help();
                    continue;
                }

                if (commands[0].ToLower() == "del" && DevicesDictionary.ContainsKey(commands[2])) 
                {
                    DevicesDictionary.Remove(commands[2]);
                    continue;
                }

                if (DevicesDictionary[commands[2]] is IStatus)
                {
                    IStatus s = (IStatus)DevicesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "on":
                            s.On();
                            break;
                        case "off":
                            s.Off();
                            break;                        
                    }
                }                

                if (DevicesDictionary[commands[2]] is IChannelSetup)
                {
                    IChannelSetup t = (IChannelSetup)DevicesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {                        
                        case "scan":
                            Console.Clear();
                            Console.WriteLine(t.ChannelScan());
                            Console.ReadKey();
                            break;
                        case "list_chan":
                            Console.WriteLine(t.ListChannel());
                            Console.ReadKey();
                            break;                        
                    }
                }

                if (DevicesDictionary[commands[2]] is ISetChannel)
                {
                    ISetChannel ch = (ISetChannel)DevicesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "next":
                            ch.NextChannel();
                            break;
                        case "early":
                            ch.EarlyChannel();
                            break;
                        case "go_to":
                            Console.WriteLine("Введите номер канала: ");
                            Input = Console.ReadLine();
                            if (Int32.TryParse(Input, out temp))
                            {
                                if (temp < 0 || temp > ch.MAXchannel)
                                {
                                    Console.WriteLine("Ошибка. Такого канала не существует!");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    ch.GoToChannel(temp);
                                }                              
                            }
                            else
                            {
                                Console.WriteLine("Ошибка! Некорректный ввод номера канала.");
                                Console.ReadKey();
                            }
                            break;
                        case "prev_chan":
                            ch.PreviousChannel();
                            break;
                    }
                }

                if (DevicesDictionary[commands[2]] is ISetVolume)
                {
                    ISetVolume v = (ISetVolume) DevicesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "mute":
                            v.SetMute();
                            break;                                              
                        case "max_vol":
                            v.MaxVolume();
                            break;
                        case "min_vol":
                            v.MinVolume();
                            break;
                        case "set_vol":
                            Console.WriteLine("Введите уровень громкости в пределах 0...100: ");
                            Input = Console.ReadLine();
                            if (Int32.TryParse(Input, out temp))
                            {
                                if (temp < 0 || temp > 100)
                                {
                                    Console.WriteLine("Ошибка! Недопустимое значение громкости.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    v.SetVolume(temp);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка! Некорректный ввод громкости.");
                                Console.ReadKey();
                            }
                            break;
                    }
                }

                if (DevicesDictionary[commands[2]] is IRateOfOpening)
                {
                    IRateOfOpening r = (IRateOfOpening)DevicesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {                      
                        case "open":
                            r.Open();
                            break;
                        case "close":
                            r.Close();
                            break;                        
                    }
                }

                if (DevicesDictionary[commands[2]] is ISetFreezeMode)
                {
                    ISetFreezeMode r = (ISetFreezeMode)DevicesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "low":
                            r.SetLowFreeze();
                            break;
                        case "colder":
                            r.SetColderFreezing();
                            break;
                        case "deep":
                            r.SetDeepFreeze();
                            break;
                        case "defrost":
                            r.SetDefrost();
                            break;                        
                    }
                }

                if (DevicesDictionary[commands[2]] is ISetTemperature)
                {
                    ISetTemperature r = (ISetTemperature)DevicesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "level_t":
                            Console.WriteLine("Введите желаемую температуру в диапазоне 2...15: ");
                            Input = Console.ReadLine();
                            if (Double.TryParse(Input, out t))
                            {
                                if (t < 2 || t > 15)
                                {
                                    Console.WriteLine("Ошибка! Недопустимое значение температуры.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    r.SetLevelTemperature(t);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка! Некорректный ввод температуры.");
                                Console.ReadKey();
                            }
                            break;
                    }
                }

                if (DevicesDictionary[commands[2]] is ICustomMode)
                {
                    ICustomMode w = (ICustomMode)DevicesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {                        
                        case "custom":                            
                            Console.WriteLine("Введите желаемый уровень температуры в диапазоне 30...90: ");
                            Input = Console.ReadLine();
                            if (Double.TryParse(Input, out t))
                            {
                                if (t < 30 || t > 90)
                                {
                                    Console.WriteLine("Ошибка! Недопустимое значение температуры.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    w.SetCustomMode(t);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка! Некорректный ввод температуры.");
                                Console.ReadKey();
                            }
                            break;
                    }
                }

                if (DevicesDictionary[commands[2]] is ITimeOfDayMode)
                {
                    ITimeOfDayMode w = (ITimeOfDayMode)DevicesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "morning":
                            w.SetMorningMode();
                            break;
                        case "evening":
                            w.SetEveningMode();
                            break;                                               
                    }
                }

                if (DevicesDictionary[commands[2]] is IModeHeating)
                {
                    IModeHeating b = (IModeHeating)DevicesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "min_mode":
                            b.SetMinMode();
                            break;
                        case "max_mode":
                            b.SetMaxMode();
                            break;                        
                    }
                }

                if (DevicesDictionary[commands[2]] is IEnterLevel)
                {
                    IEnterLevel e = (IEnterLevel)DevicesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "ent_l":
                            Console.WriteLine("Введите уровень влажности почвы: ");
                            Input = Console.ReadLine();
                            if (Int32.TryParse(Input, out temp))
                            {
                                if (temp < 0 || temp > 100)
                                {
                                    Console.WriteLine("Ошибка! Недопустимое значение уровня влажности почвы.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    e.EnterLevel(temp);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка! Некорректный ввод уровня влажности почвы.");
                                Console.ReadKey();
                            }
                            break;
                    }
                }
            }
        }

        private static void Help()
        {
            Console.WriteLine("Доступные команды:");
            Console.WriteLine("\tadd TV nameDevice");
            Console.WriteLine("\tadd ref nameDevice");
            Console.WriteLine("\tadd shut nameDevice");
            Console.WriteLine("\tadd boiler nameDevice");
            Console.WriteLine("\tadd WS nameDevice");

            Console.WriteLine("\tdel _ nameDevice");
            Console.WriteLine("\ton _ nameDevice");
            Console.WriteLine("\toff _ nameDevice");

            Console.WriteLine("Доступные команды для телевизора:");
            Console.WriteLine("\tnext TV nameDevice");
            Console.WriteLine("\tearly TV nameDevice");
            Console.WriteLine("\tgo_to TV nameDevice");
            Console.WriteLine("\tprev_chan TV nameDevice");
            Console.WriteLine("\tscan TV nameDevice");
            Console.WriteLine("\tlist_chan TV nameDevice");
            Console.WriteLine("\tmax_vol TV nameDevice");
            Console.WriteLine("\tmin_vol TV nameDevice");
            Console.WriteLine("\tset_vol TV nameDevice");
            Console.WriteLine("\tmute TV nameDevice");

            Console.WriteLine("Доступные команды для холодильника:");
            Console.WriteLine("\tlow ref nameDevice");
            Console.WriteLine("\tcolder ref nameDevice");
            Console.WriteLine("\tdeep ref nameDevice");
            Console.WriteLine("\tdefrost ref nameDevice");
            Console.WriteLine("\topen ref nameDevice");//
            Console.WriteLine("\tclose ref nameDevice");//
            Console.WriteLine("\tlevel_t ref nameDevice");

            Console.WriteLine("Доступные команды для жалюзи:");
            Console.WriteLine("\tmorning shut nameDevice");
            Console.WriteLine("\tevening shut nameDevice"); 
            Console.WriteLine("\topen shut nameDevice"); 
            Console.WriteLine("\tclose shut nameDevice");

            Console.WriteLine("Доступные команды для бойлера:");
            Console.WriteLine("\tmin_mode boiler nameDevice");
            Console.WriteLine("\tmax_mode boiler nameDevice");
            Console.WriteLine("\tcustom boiler nameDevice");

            Console.WriteLine("Доступные команды для системы полива:");
            Console.WriteLine("\tent_l WS nameDevice");

            Console.WriteLine("\n \texit _ _");
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadLine();
        }
    }
}
