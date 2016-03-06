using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorConsole
{
    class Dog
    {
        private int age;
        private double weight;
        public Dog(string name, int age, double weight)
        {
            Name = name;
            Age = age;
            Weight = weight;
        }

        [ColorProperty(ConsoleColor.Magenta)]
        public string Name { get; set; }

        public int Age
        {
            get { return age; }
            set
            {
                if (value > 0)
                {
                    age = value;
                }
            }
        }
        public double Weight
        {
            get { return weight; }
            set
            {
                if (value > 0)
                {
                    weight = value;
                }
            }
        }
    }
}
