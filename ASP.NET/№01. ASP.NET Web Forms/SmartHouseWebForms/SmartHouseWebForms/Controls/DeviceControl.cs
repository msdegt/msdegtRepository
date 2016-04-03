using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SmartHouse;
using Image = System.Web.UI.WebControls.Image;

namespace SmartHouseWebForms
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class DeviceControl : Panel
    {
        private int id; // Ключ девайса в коллекции девайсов
        private int temp;
        private double t;
        private readonly IDictionary<int, Device> DevicesDictionary;

        private ISetChannel ch;
        private ISetVolume v;
        private IRateOfOpening r;
        private IChannelSetup chSetup;

        private Label status; // Поле вывода текущего состояние устройства
        private bool statusOpen; // Переменная состояния открытия
        private Button onButton; // Кнопка включить устройство
        private Button offButton; // Кнопка выключить устройство

        private TextBox vTextBox; // Поле ввода для отображения/установки значения громкости
        private TextBox errorVol; // Поле ошибки громкости

        private TextBox сhanTextBox; // Поле ввода для отображения/установки значения канала
        private TextBox errorChan; // Поле ошибки канала

        private TextBox tempTextBox; // Поле ввода для отображения/установки температуры
        private TextBox errorTemp; // Поле ошибки температуры

        private TextBox сustomTextBox; // Поле ввода для отображения/установки данных пользовательского режима
        private TextBox errorCustom; // Поле ошибки пользовательсого значения

        private TextBox levelTextBox; // Поле ввода для отображения/установки температуры
        private TextBox errorLevel; // Поле ошибки уровня влажности

        private DropDownList freezeMode;
        private DropDownList timeOfDayMode;
        private DropDownList modeHeating;

        private Image im;

        private PlaceHolder ph;
        private PlaceHolder phErrorLevel;
        private PlaceHolder phErrorVol;
        private PlaceHolder phErrorChan;
        private PlaceHolder phErrorTemp;
        private PlaceHolder phErrorCustom;

        public DeviceControl(int id, IDictionary<int, Device> devicesDictionary)
        {
            this.id = id;
            DevicesDictionary = devicesDictionary;
            Initializer();
        }

        // Инициализатор графики фигуры
        protected void Initializer()
        {
            CssClass = "device-div";
            im = new Image();

            // Добавление gif анимации в зависимости от девайса
            if (DevicesDictionary[id] is ISetChannel && DevicesDictionary[id] is IChannelSetup)
            {
                chSetup = (IChannelSetup)DevicesDictionary[id];
                if (DevicesDictionary[id].Status == false)
                {
                    im.ImageUrl = "images/offTV.png";
                }
                else if (DevicesDictionary[id].Status && chSetup.СhanState == false)
                {
                    im.ImageUrl = "images/1.gif";
                }
                else if (DevicesDictionary[id].Status && chSetup.СhanState)
                {
                    im.ImageUrl = "images/2.gif";
                }
                Controls.Add(im);
            }
            if (DevicesDictionary[id] is IRateOfOpening)
            {
                im = new Image();
                statusOpen = ((IRateOfOpening)DevicesDictionary[id]).StatusOpen;
                if (statusOpen && DevicesDictionary[id] is ISetFreezeMode)
                {
                    im.ImageUrl = "images/OpenRef.gif";
                }
                else if (statusOpen == false && DevicesDictionary[id] is ISetFreezeMode)
                {
                    im.ImageUrl = "images/closeRef.gif";
                }
                else if (statusOpen && DevicesDictionary[id] is ITimeOfDayMode)
                {
                    im.ImageUrl = "images/OpenW.gif";
                }
                else if (statusOpen == false && DevicesDictionary[id] is ITimeOfDayMode)
                {
                    im.ImageUrl = "images/CloseW.gif";
                }
                Controls.Add(im);
            }
            if (DevicesDictionary[id] is ITimeOfDayMode)
            {
                im = new Image();
                if (DevicesDictionary[id].Status == false)
                {
                    im.ImageUrl = "images/down.gif";
                }
                else if (DevicesDictionary[id].Status)
                {
                    im.ImageUrl = "images/up.gif";
                }
                Controls.Add(im);
            }
            if (DevicesDictionary[id] is IEnterLevel)
            {
                if (DevicesDictionary[id].Status == false)
                {
                    im.ImageUrl = "images/wateringOff300.png";
                }
                else if (DevicesDictionary[id].Status)
                {
                    im.ImageUrl = "images/244x300_2watering.gif";
                }
                Controls.Add(im);
            }

            //Добавление остальных элементов графики 
            status = new Label();
            status.ID = status + id.ToString();
            status.Text = "<br>" + "Устройство: " + DevicesDictionary[id].Name + "<br>" + DevicesDictionary[id] + "<br>";
            status.CssClass = "device-label"; 
            Controls.Add(status);

            Controls.Add(Span("<br />"));

            if (DevicesDictionary[id] is ITimeOfDayMode)
            {
                onButton = Button("Поднять", "on ");
            }
            else
            {
                onButton = Button("Включить", "on ");
            }
            onButton.Click += ButtonClick;
            Controls.Add(onButton);

            if (DevicesDictionary[id] is ITimeOfDayMode)
            {
                offButton = Button("Опустить", "off ");
            }
            else
            {
                offButton = Button("Выключить", "off ");
            }
            offButton.Click += ButtonClick;
            Controls.Add(offButton);

            Controls.Add(Span("<br />"));

            if (DevicesDictionary[id] is ISetChannel)
            {
                Button nextChannel = Button("Канал ++", "nCh ");
                nextChannel.Click += ButtonClick;
                Controls.Add(nextChannel);
                Button earlyChannel = Button("Канал --", "eCh ");
                earlyChannel.Click += ButtonClick;
                Controls.Add(earlyChannel);
                Button previousChannel = Button("Канал перед этим", "prevCh ");
                previousChannel.Click += ButtonClick;
                Controls.Add(previousChannel);
                Controls.Add(Span("<br />"));
                Controls.Add(Span("Перейти на канал: "));
                сhanTextBox = TextBox("");
                сhanTextBox.ID = "сhan" + id;
                Controls.Add(сhanTextBox);
                Button goToChannel = Button("Перейти", "goToCh ");
                goToChannel.Click += GoToChannelButtonClick;
                Controls.Add(goToChannel);
                phErrorChan = PlaceHolder("phErrorChan");
                Controls.Add(phErrorChan);
                Controls.Add(Span("<br />"));
            }

            if (DevicesDictionary[id] is ISetVolume)
            {
                Button maxVolume = Button("Звук ++", "maxV ");
                maxVolume.Click += ButtonClick;
                Controls.Add(maxVolume);
                Button minVolume = Button("Звук --", "minV ");
                minVolume.Click += ButtonClick;
                Controls.Add(minVolume);
                Button setMute = Button("Mute", "mute ");
                setMute.Click += ButtonClick;
                Controls.Add(setMute);
                Controls.Add(Span("<br />"));
                Controls.Add(Span("Установить звук: "));
                vTextBox = TextBox("");
                vTextBox.ID = "v" + id;
                Controls.Add(vTextBox);
                Button setVolume = Button("Установить", "setV ");
                setVolume.Click += SetVolumeButtonClick;
                Controls.Add(setVolume);
                phErrorVol = PlaceHolder("phErrorVol");
                Controls.Add(phErrorVol);
                Controls.Add(Span("<br />"));
            }

            if (DevicesDictionary[id] is IRateOfOpening)
            {
                Button open = Button("Открыть", "open ");
                open.Click += ButtonClick;
                Controls.Add(open);
                Button close = Button("Закрыть", "close ");
                close.Click += ButtonClick;
                Controls.Add(close);
                Controls.Add(Span("<br />"));
            }

            if (DevicesDictionary[id] is ISetFreezeMode)
            {
                Controls.Add(Span("Выберите режим: "));
                freezeMode = new DropDownList();
                freezeMode.ID = "frMode" + id;
                freezeMode.Items.Add(TemperatureLevel.Default.ToString());////////////////////////
                freezeMode.Items.Add(TemperatureLevel.LowFreeze.ToString());
                freezeMode.Items.Add(TemperatureLevel.ColderFreezing.ToString());
                freezeMode.Items.Add(TemperatureLevel.DeepFreeze.ToString());
                freezeMode.Items.Add(TemperatureLevel.Defrost.ToString());
                if (HttpContext.Current.Session["Freeze"] != null)
                {
                    freezeMode.SelectedIndex = (int)HttpContext.Current.Session["Freeze"];
                }
                Controls.Add(freezeMode);
                Button setFreezeMode = Button("Установить", "setFreeze ");
                setFreezeMode.Click += SetFreezeModeButtonClick;
                Controls.Add(setFreezeMode);
                Controls.Add(Span("<br />"));
            }

            if (DevicesDictionary[id] is ISetTemperature)
            {
                Controls.Add(Span("Введите желаемую температуру в диапазоне 2...15: "));
                tempTextBox = TextBox("");
                tempTextBox.ID = "temp" + id;
                Controls.Add(tempTextBox);
                Button setTemperature = Button("Установить", "setT ");
                setTemperature.Click += SetTemperatureButtonClick;
                Controls.Add(setTemperature);
                phErrorTemp = PlaceHolder("phErrorTemp");
                Controls.Add(phErrorTemp);
                Controls.Add(Span("<br />"));
            }

            if (DevicesDictionary[id] is ICustomMode)
            {
                Controls.Add(Span("Введите желаемый уровень температуры в диапазоне 30...90: "));
                Controls.Add(Span("<br />"));
                сustomTextBox = TextBox("");
                сustomTextBox.ID = "custom" + id;
                Controls.Add(сustomTextBox);
                Button setCustomMode = Button("Установить", "setCustom ");
                setCustomMode.Click += SetCustomModeButtonClick;
                Controls.Add(setCustomMode);
                phErrorCustom = PlaceHolder("phErrorCustom");
                Controls.Add(phErrorCustom);
                Controls.Add(Span("<br />"));
            }

            if (DevicesDictionary[id] is ITimeOfDayMode)
            {
                Controls.Add(Span("Выберите режим: "));
                timeOfDayMode = new DropDownList(); 
                timeOfDayMode.ID = "timeMode" + id;
                timeOfDayMode.Items.Add(ShuttersMode.MorningMode.ToString());
                timeOfDayMode.Items.Add(ShuttersMode.EveningMode.ToString());
                if (HttpContext.Current.Session["TimeOfDay"] != null)
                {
                    timeOfDayMode.SelectedIndex = (int) HttpContext.Current.Session["TimeOfDay"];
                }
                Controls.Add(timeOfDayMode);
                Button setTimeOfDayMode = Button("Установить", "setTime ");
                setTimeOfDayMode.Click += SetTimeOfDayModeButtonClick;
                Controls.Add(setTimeOfDayMode);
                Controls.Add(Span("<br />"));
            }

            if (DevicesDictionary[id] is IModeHeating)
            {
                Controls.Add(Span("Выберите режим: "));
                modeHeating = new DropDownList(); 
                modeHeating.ID = "h" + id;
                modeHeating.Items.Add(BoilerMode.MaxMode.ToString());
                modeHeating.Items.Add(BoilerMode.MinMode.ToString());
                if (HttpContext.Current.Session["Heating"] != null)
                {
                    modeHeating.SelectedIndex = (int)HttpContext.Current.Session["Heating"];
                }
                Controls.Add(modeHeating);
                Button setModeHeating = Button("Установить", "setH ");
                setModeHeating.Click += SetModeHeatingButtonClick;
                Controls.Add(setModeHeating);
                Controls.Add(Span("<br />"));
            }

            if (DevicesDictionary[id] is IEnterLevel)
            {
                Controls.Add(Span("Введите уровень влажности почвы: "));
                if (HttpContext.Current.Session["level"] != null)
                {
                    levelTextBox = TextBox(HttpContext.Current.Session["level"].ToString());
                }
                else
                {
                    levelTextBox = TextBox("");
                }
                levelTextBox.ID = "l" + id;
                Controls.Add(levelTextBox);
                Button enterLevel = Button("Ввести", "setLevel ");
                enterLevel.Click += EnterLevelButtonClick;
                Controls.Add(enterLevel);
                phErrorLevel = PlaceHolder("phErrorLevel");
                Controls.Add(phErrorLevel);
                Controls.Add(Span("<br />"));
            }

            if (DevicesDictionary[id] is IChannelSetup)
            {
                Button сhannelScan = Button("Настроить", "scan ");
                сhannelScan.Click += ButtonClick;
                Controls.Add(сhannelScan);
                Button listChannel = Button("Список", "setT ");
                listChannel.Click += ListChannelButtonClick;
                Controls.Add(listChannel);
                Controls.Add(Span("<br />"));
                ph = PlaceHolder("PlaceHolder");
                Controls.Add(ph);
            }

            Controls.Add(Span("<br />"));

            Button deleteButton = Button("Удалить", "d ");
            deleteButton.Click += DeleteButtonClick;
            deleteButton.CssClass = "Del";
            Controls.Add(deleteButton);
        }

        // Функции для создания элементов управления
        protected Button Button(string text, string pref)
        {
            Button button = new Button();
            button.ID = pref + id;
            button.Text = text;
            return button;
        }

        protected HtmlGenericControl Span(string innerHTML)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHTML;
            return span;
        }

        protected TextBox TextBox(string value)
        {
            TextBox textBox = new TextBox();
            textBox.Text = value;
            textBox.Columns = 3;
            return textBox;
        }

        protected PlaceHolder PlaceHolder(string pref)
        {
            PlaceHolder placeHolder = new PlaceHolder();
            placeHolder.ID = pref + id; 
            return placeHolder;
        }

        ///////////////////////////Обработчики событий
        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            string[] mass = button.ID.Split(new char[] {' '});
            if (DevicesDictionary[id] is ISetChannel)
            {
                ch = (ISetChannel) DevicesDictionary[id];
            }
            if (DevicesDictionary[id] is ISetVolume)
            {
                v = (ISetVolume) DevicesDictionary[id];
            }
            if (DevicesDictionary[id] is IRateOfOpening)
            { 
                r = (IRateOfOpening) DevicesDictionary[id];
            }
            
            switch (mass[0])
                {
                    case "on":
                        DevicesDictionary[id].On();
                        break;
                    case "off":
                        DevicesDictionary[id].Off();
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
                        chSetup = (IChannelSetup)DevicesDictionary[id];
                        chSetup.ChannelScan();
                        break;
                }
            Controls.Clear();
            Initializer();
        }

        private void GoToChannelButtonClick(object sender, EventArgs e)
        {
            errorChan = TextBox("");
            errorChan.ID = "errorChan" + id;
            errorChan.CssClass = "error";
            errorChan.TextMode = TextBoxMode.MultiLine;

            ch = (ISetChannel)DevicesDictionary[id];
            if (Int32.TryParse(сhanTextBox.Text, out temp))
            {
                if (temp < 0 || temp > ch.MAXchannel)
                {
                    errorChan.Text = "Ошибка. Такого канала не существует!";
                }
                else
                {
                    ch.GoToChannel(temp);
                }
            }
            else
            {
                errorChan.Text = "Ошибка! Некорректный ввод номера канала.";               
            }
            Controls.Clear();
            Initializer();
            phErrorChan.Controls.Add(errorChan);
        }

        private void SetVolumeButtonClick(object sender, EventArgs e)
        {
            errorVol = TextBox("");
            errorVol.ID = "errorVol" + id;
            errorVol.CssClass = "error";
            errorVol.TextMode = TextBoxMode.MultiLine;

            v = (ISetVolume)DevicesDictionary[id];
            if (Int32.TryParse(vTextBox.Text, out temp))
            {
                if (temp < 0 || temp > 100)
                {
                    errorVol.Text = "Ошибка! Недопустимое значение громкости.";                  
                }
                else
                {
                    v.SetVolume(temp);
                }
            }
            else
            {
                errorVol.Text = "Ошибка! Некорректный ввод громкости.";
            }
            Controls.Clear();
            Initializer();
            phErrorVol.Controls.Add(errorVol);            
        }

        private void SetFreezeModeButtonClick(object sender, EventArgs e)  ////////////// работает неправильно
        {
            ISetFreezeMode f = (ISetFreezeMode)DevicesDictionary[id];
            HttpContext.Current.Session["Freeze"] = freezeMode.SelectedIndex; 
            switch (freezeMode.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    f.SetLowFreeze();
                    break;
                case 2:
                    f.SetColderFreezing();
                    break;
                case 3:
                    f.SetDeepFreeze();
                    break;
                case 4:
                    f.SetDefrost();
                    break;
            }
            Controls.Clear();
            Initializer();
        }

        private void SetTemperatureButtonClick(object sender, EventArgs e)
        {
            errorTemp = TextBox("");
            errorTemp.ID = "errorTemp" + id;
            errorTemp.CssClass = "error";
            errorTemp.TextMode = TextBoxMode.MultiLine;

            ISetTemperature temperature = (ISetTemperature)DevicesDictionary[id];
            if (Double.TryParse(tempTextBox.Text, out t))
            {
                if (t < 2 || t > 15)
                {
                    errorTemp.Text = "Ошибка! Недопустимое значение температуры.";
                }
                else
                {
                    temperature.SetLevelTemperature(t);
                }
            }
            else
            {
                errorTemp.Text = "Ошибка!Некорректный ввод температуры.";
            }
            Controls.Clear();
            Initializer();
            phErrorTemp.Controls.Add(errorTemp);
        }

        private void SetCustomModeButtonClick(object sender, EventArgs e)
        {
            errorCustom = TextBox("");
            errorCustom.ID = "errorCustom" + id;
            errorCustom.CssClass = "error";
            errorCustom.TextMode = TextBoxMode.MultiLine;

            ICustomMode c = (ICustomMode)DevicesDictionary[id];
            if (Double.TryParse(сustomTextBox.Text, out t))
            {
                if (t < 30 || t > 90)
                {
                    errorCustom.Text = "Ошибка! Недопустимое значение температуры."; 
                }
                else
                {
                    c.SetCustomMode(t);
                }
            }
            else
            {
                errorCustom.Text = "Ошибка! Некорректный ввод температуры.";
            }
            Controls.Clear();
            Initializer();
            phErrorCustom.Controls.Add(errorCustom);
        }

        private void SetTimeOfDayModeButtonClick(object sender, EventArgs e)
        {
            HttpContext.Current.Session["TimeOfDay"] = timeOfDayMode.SelectedIndex; 
            ITimeOfDayMode time = (ITimeOfDayMode)DevicesDictionary[id];
            switch (timeOfDayMode.SelectedIndex)
            {
                default:
                    time.SetMorningMode();
                    break;
                case 1:
                    time.SetEveningMode();
                    break;                
            }
            Controls.Clear();
            Initializer();
        }

        private void SetModeHeatingButtonClick(object sender, EventArgs e)
        {
            HttpContext.Current.Session["Heating"] = modeHeating.SelectedIndex; 
            IModeHeating h = (IModeHeating)DevicesDictionary[id];
            switch (modeHeating.SelectedIndex)
            {
                default:
                    h.SetMaxMode();
                    break;
                case 1:
                    h.SetMinMode();
                    break;
            }
            Controls.Clear();
            Initializer();
        }

        private void EnterLevelButtonClick(object sender, EventArgs e)
        {
            HttpContext.Current.Session["level"] = levelTextBox.Text;
            IEnterLevel eL = (IEnterLevel)DevicesDictionary[id];

            errorLevel = TextBox("");
            errorLevel.ID = "errorLevel" + id;
            errorLevel.CssClass = "error";
            errorLevel.TextMode = TextBoxMode.MultiLine;

            if (Int32.TryParse(levelTextBox.Text, out temp))
            {
                if (temp < 0 || temp > 100)
                {
                    errorLevel.Text = "Ошибка! Недопустимое значение уровня влажности почвы.";
                }
                else
                {
                    eL.EnterLevel(temp);
                    phErrorLevel.Controls.Remove(errorLevel);
                }
            }
            else
            {
                errorLevel.Text = "Ошибка! Некорректный ввод уровня влажности почвы.";
            }
            Controls.Clear();
            Initializer();
            phErrorLevel.Controls.Add(errorLevel);
        }

        private void ListChannelButtonClick(object sender, EventArgs e)
        {
            chSetup = (IChannelSetup)DevicesDictionary[id];
            string[] listCh = chSetup.ListChannel().Split(new char[] { '№' });
            BulletedList list = new BulletedList();
            list.ID = "list";
            for (int i = 0; i < listCh.Length; i++)
            {
                list.Items.Add(new ListItem(listCh[i]));
            }
            ph.Controls.Add(list);
        }

        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            DevicesDictionary.Remove(id); // Удаление девайса из коллекции
            Parent.Controls.Remove(this); // Удаление графики для девайса
        }
    }
}