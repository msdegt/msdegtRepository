using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SmartHouse
{
    class Menu
    {
        private IDictionary<string, Devices> devicesesDictionary = new Dictionary<string, Devices>();
        public string Input { get; set; }

        public void Show()
        {
            devicesesDictionary.Add("TV1", new Television(false));
            devicesesDictionary.Add("Ref1", new Refrigerator(false, false));
            devicesesDictionary.Add("Shut1", new WindowShutters(false, true));
            devicesesDictionary.Add("Boiler1", new Boiler(false));
            devicesesDictionary.Add("WS1", new WateringSystem(false));

            while (true)
            {
                Console.Clear();
                foreach (var dev in devicesesDictionary)
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
                if (commands[0].ToLower() == "add" && !devicesesDictionary.ContainsKey(commands[2]))
                {
                    if (commands[1] == "TV")
                    {
                        devicesesDictionary.Add(commands[2], new Television(false));
                        continue;
                    }
                    if (commands[1] == "ref")
                    {
                        devicesesDictionary.Add(commands[2], new Refrigerator(false, false));
                        continue;
                    }
                    if (commands[1] == "shut")
                    {
                        devicesesDictionary.Add(commands[2], new WindowShutters(false, true));
                        continue;
                    }
                    if (commands[1] == "boiler")
                    {
                        devicesesDictionary.Add(commands[2], new Boiler(false));
                        continue;
                    }
                    if (commands[1] == "WS")
                    {
                        devicesesDictionary.Add(commands[2], new WateringSystem(false));
                        continue;
                    }
                }

                if (commands[0].ToLower() == "add" && devicesesDictionary.ContainsKey(commands[2]))
                {
                    Console.WriteLine("Устройство с таким именем уже существует");
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadLine();
                    continue;
                }

                if (commands[0].ToLower() == "del" && !devicesesDictionary.ContainsKey(commands[2]))
                {
                    Console.WriteLine("Выполнение команды невозможно, т.к. устройства с таким именем не существует");
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadLine();
                    continue;
                }

                if (!devicesesDictionary.ContainsKey(commands[2]))
                {
                    Help();
                    continue;
                }

                if (commands[0].ToLower() == "del" && devicesesDictionary.ContainsKey(commands[2])) 
                {
                    devicesesDictionary.Remove(commands[2]);
                    continue;
                }
                
                if (devicesesDictionary[commands[2]] is IStatus)
                {
                    IStatus s = (IStatus)devicesesDictionary[commands[2]];
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

                if (devicesesDictionary[commands[2]] is IChannelSetup)
                {
                    IChannelSetup t = (IChannelSetup)devicesesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {                        
                        case "scan":
                            Console.Clear();
                            Console.WriteLine("Идет настройка каналов... ");
                            Console.ReadLine();
                            Console.WriteLine(t.ChannelScan());
                            break;
                        case "list_chan":
                            Console.WriteLine(t.ListChannel());
                            Console.ReadKey();
                            break;                        
                    }
                }

                if (devicesesDictionary[commands[2]] is ISetChannel)
                {
                    ISetChannel t = (ISetChannel)devicesesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "next":
                            t.NextChannel();
                            break;
                        case "early":
                            t.EarlyChannel();
                            break;
                        case "go_to":
                            Console.WriteLine("Введите номер канала: ");
                            Input = Console.ReadLine();
                            t.GoToChannel(Input);
                            break;
                        case "prev_chan":
                            t.PreviousChannel();
                            break;
                    }
                }

                if (devicesesDictionary[commands[2]] is ISetVolume)
                {
                    ISetVolume t = (ISetVolume) devicesesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "mute":
                            t.SetMute();
                            break;                                              
                        case "max_vol":
                            t.MaxVolume();
                            break;
                        case "min_vol":
                            t.MinVolume();
                            break;
                        case "set_vol":
                            Console.WriteLine("Введите уровень громкости: ");
                            Input = Console.ReadLine();
                            t.SetVolume(Input);
                            break;
                    }
                }

                if (devicesesDictionary[commands[2]] is IRateOfOpening)
                {
                    IRateOfOpening r = (IRateOfOpening)devicesesDictionary[commands[2]];
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

                if (devicesesDictionary[commands[2]] is ISetFreezeMode)
                {
                    ISetFreezeMode r = (ISetFreezeMode)devicesesDictionary[commands[2]];
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

                if (devicesesDictionary[commands[2]] is ISetTemperature)
                {
                    ISetTemperature r = (ISetTemperature)devicesesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "level_t":
                            Console.WriteLine("Введите желаемую температуру: ");
                            Input = Console.ReadLine();
                            r.SetLevelTemperature(Input);
                            break;
                    }
                }

                if (devicesesDictionary[commands[2]] is ICustomMode)
                {
                    ICustomMode w = (ICustomMode)devicesesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {                        
                        case "custom":                            
                            Console.WriteLine("Введите желаемый уровень температуры в диапазоне 30...90: ");
                            Input = Console.ReadLine();                               
                            w.SetCustomMode(Input);
                            break;
                    }
                }

                if (devicesesDictionary[commands[2]] is ITimeOfDayMode)
                {
                    ITimeOfDayMode w = (ITimeOfDayMode)devicesesDictionary[commands[2]];
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

                if (devicesesDictionary[commands[2]] is IModeHeating)
                {
                    IModeHeating b = (IModeHeating)devicesesDictionary[commands[2]];
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

                if (devicesesDictionary[commands[2]] is IEnterLevel)
                {
                    IEnterLevel e = (IEnterLevel)devicesesDictionary[commands[2]];
                    switch (commands[0].ToLower())
                    {
                        case "ent_l":
                            Console.WriteLine("Введите уровень влажности почвы: ");
                            Input = Console.ReadLine();
                            e.EnterLevel(Input);
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
