using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartHouse;

namespace SmartHouseMVC.Controllers
{
    public class SmartHouseController : Controller
    {
        ISetChannel ch;
        ISetVolume v;
        IRateOfOpening r;
        IChannelSetup s;

        public ActionResult Index()
        {
            IDictionary<int, Device> devicesDictionary;
            ICreate Create;

            if (Session["Devices"] == null)
            {
                devicesDictionary = new SortedDictionary<int, Device>();
                Create = new CreateObject();

                devicesDictionary.Add(1, Create.CreateTv());
                devicesDictionary.Add(2, Create.CreateRef());
                devicesDictionary.Add(3, Create.CreateShut());
                devicesDictionary.Add(4, Create.CreateWs());
                devicesDictionary.Add(5, Create.CreateBoiler());

                Session["Devices"] = devicesDictionary;
                Session["NextId"] = 6;
            }
            else
            {
                devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            }

            SelectListItem[] devicesList = new SelectListItem[5];
            devicesList[0] = new SelectListItem { Text = "Телевизор", Value = "Tv", Selected = true };
            devicesList[1] = new SelectListItem { Text = "Холодильник", Value = "Ref" };
            devicesList[2] = new SelectListItem { Text = "Жалюзи", Value = "Shut" };
            devicesList[3] = new SelectListItem { Text = "Система капельного полива", Value = "Ws" };
            devicesList[4] = new SelectListItem { Text = "Бойлер", Value = "Boiler" };
            ViewBag.DevicesList = devicesList;
            ViewBag.Title = "Умный дом"; 

            return View(devicesDictionary);
        }

        public ActionResult Add(string deviceType)
        {

            Device newDevice;
            ICreate Create = new CreateObject();
            switch (deviceType)
            {
                default:
                    newDevice = Create.CreateTv();
                    break;
                case "Ref":
                    newDevice = Create.CreateRef();
                    break;
                case "Boiler":
                    newDevice = Create.CreateBoiler();
                    break;
                case "Shut":
                    newDevice = Create.CreateShut();
                    break;
                case "Ws":
                    newDevice = Create.CreateWs();
                    break;
            }

            int id = (int)Session["NextId"];
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            devicesDictionary.Add(id, newDevice); // Добавление девайса в коллекцию
            Session["Devices"] = devicesDictionary;
            id++;
            Session["NextId"] = id;
            return RedirectToAction("Index");
        }

        public ActionResult Operation(int id, string command)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            
            if (devicesDictionary[id] is ISetChannel)
            {
               ch = (ISetChannel)devicesDictionary[id];
            }
            if (devicesDictionary[id] is ISetVolume)
            {
                v = (ISetVolume)devicesDictionary[id];
            }
            if (devicesDictionary[id] is IRateOfOpening)
            {
                r = (IRateOfOpening)devicesDictionary[id];
            }
            if (devicesDictionary[id] is IChannelSetup)
            {
                s = (IChannelSetup)devicesDictionary[id];
            }

            switch (command)
            {
                case "on":
                    devicesDictionary[id].On();
                    break;
                case "off":
                    devicesDictionary[id].Off();
                    break;
                case "nCh":
                    ch.NextChannel();
                    break;
                case "eCh":
                    ch.EarlyChannel();
                    break;
                case "prevCh":
                    ch.PreviousChannel();
                    break;
                case "maxV":
                    v.MaxVolume();
                    break;
                case "minV":
                    v.MinVolume();
                    break;
                case "mute":
                    v.SetMute();
                    break;
                case "open":
                    r.Open();
                    break;
                case "close":
                    r.Close();
                    break;
                case "scan":
                    s.ChannelScan();
                    break;
                case "listChan":
                    string str = s.ListChannel().Replace("Список каналов:", " ");
                    string[] mass = str.Split('№');
                    TempData["Mass"] = mass;
                    return RedirectToAction("List", "ListTV");
            }

            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetChannel(int id, int chan = 1)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ch = (ISetChannel)devicesDictionary[id];
            if (chan < 0 || chan > ch.MAXchannel)
            {
                TempData["ErrorChannel"] = "Ошибка. Такого канала не существует!";
            }
            else
            {
                ch.GoToChannel(chan);
            }

            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetVolume(int id, int vol = 0)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            v = (ISetVolume)devicesDictionary[id];
            if (vol < 0 || vol > 100)
            {
                TempData["ErrorVolume"] = "Ошибка! Недопустимое значение громкости.";
            }
            else
            {
                v.SetVolume(vol);
            }
            
            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetFreezeMode(int id, string frMode)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetFreezeMode f = (ISetFreezeMode)devicesDictionary[id];
            Session["FreezeMode"] = frMode;
            switch (frMode)
            {
                case "Default":
                    break;
                case "LowFreeze":
                    f.SetLowFreeze();
                    break;
                case "ColderFreezing":
                    f.SetColderFreezing();
                    break;
                case "DeepFreeze":
                    f.SetDeepFreeze();
                    break;
                case "Defrost":
                    f.SetDefrost();
                    break;
            }

            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetTemperature(int id, double temp = 2)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ISetTemperature t = (ISetTemperature)devicesDictionary[id];
            if (temp < 2 || temp > 15)
            {
                TempData["ErrorTemperature"] = "Ошибка! Недопустимое значение температуры.";
            }
            else
            {
                t.SetLevelTemperature(temp);
            }
            
            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetCustomMode(int id, double custom = 30)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ICustomMode с = (ICustomMode)devicesDictionary[id];
            if (custom < 30 || custom > 90)
            {
                TempData["ErrorCustomMode"] = "Ошибка! Недопустимое значение температуры.";
            }
            else
            {
                с.SetCustomMode(custom);
            }
            
            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetTimeOfDayMode(int id, string timeMode)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            ITimeOfDayMode time = (ITimeOfDayMode)devicesDictionary[id];
            Session["TimeMode"] = timeMode;
            switch (timeMode)
            {
                default:
                    time.SetMorningMode();
                    break;
                case "EveningMode":
                    time.SetEveningMode();
                    break;
            }

            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult SetModeHeating(int id, string h)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            IModeHeating heating = (IModeHeating)devicesDictionary[id];
            Session["ModeHeating"] = h;
            switch (h)
            {
                default:
                    heating.SetMaxMode();
                    break;
                case "MinMode":
                    heating.SetMinMode();
                    break;
            }

            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult EnterLevel(int id, int level = 0)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            IEnterLevel l = (IEnterLevel)devicesDictionary[id];
            Session["Level"] = level; 
            if (level < 0 || level > 100)
            {
                TempData["ErrorSoilMoisture"] = "Ошибка! Недопустимое значение уровня влажности почвы."; 
            }
            else
            {
                l.EnterLevel(level);
            }

            Session["Devices"] = devicesDictionary;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            IDictionary<int, Device> devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            devicesDictionary.Remove(id);
            Session["Devices"] = devicesDictionary;

            return RedirectToAction("Index");
        }
    }
}