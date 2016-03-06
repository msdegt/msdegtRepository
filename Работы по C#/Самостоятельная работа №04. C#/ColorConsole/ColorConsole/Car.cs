using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorConsole
{
    class Car
    {
        private int mileage;

        public Car(string brand, string color, int mileage)
        {
            Brand = brand;
            Color = color;
            Mileage = mileage;
        }

        public string Brand { get; set; }

        [ColorProperty(ConsoleColor.Yellow)]
        public string Color { get; set; }

        [ColorProperty(ConsoleColor.Red)]
        public int Mileage
        {
            get { return mileage; }
            set
            {
                if (value >= 0)
                {
                    mileage = value;
                }
            }
        }
    }
}
