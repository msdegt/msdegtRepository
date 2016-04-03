using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmartHouse;

namespace SmartHouseWebForms
{
    public partial class Default : System.Web.UI.Page
    {
        private IDictionary<int, Device> devicesDictionary;
        public ICreate Create { get; set; }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                devicesDictionary = (SortedDictionary<int, Device>)Session["Devices"];
            }
            else
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                addDevicesButton.Click += AddDevicesButtonClick;
            }
            InitialiseDevicesPanel();          
        }

        // Создание элементов графики для всех девайсов в коллекции
        protected void InitialiseDevicesPanel()
        {
            foreach (int key in devicesDictionary.Keys)
            {
                devicesPanel.Controls.Add(new DeviceControl(key, devicesDictionary));
            }
        }

        // Обработчик нажатия кнопки добавления девайсов
        protected void AddDevicesButtonClick(object sender, EventArgs e)
        {
            Device newDevice;
            Create = new CreateObject();
            switch (dropDownDevicesList.SelectedIndex)
            {
                default:
                    newDevice = Create.CreateTv();
                    break;
                case 1:
                    newDevice = Create.CreateRef();
                    break;
                case 2:
                    newDevice = Create.CreateBoiler();
                    break;
                case 3:
                    newDevice = Create.CreateShut();
                    break;
                case 4:
                    newDevice = Create.CreateWs();
                    break;
            }

            int id = (int)Session["NextId"];
            devicesDictionary.Add(id, newDevice); // Добавление девайса в коллекцию
            devicesPanel.Controls.Add(new DeviceControl(id, devicesDictionary)); // Добавление графики для девайса
            id++;
            Session["NextId"] = id;
        }
    }
}