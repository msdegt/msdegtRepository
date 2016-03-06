using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car("BMW", "yellow", 50000);
            Dog dog = new Dog("Lokky", 1, 23.5);

            Console.WriteLine("Свойства объекта " + car.GetType() + " :");
            ColorProperty.ColorPrint(car);
            Console.WriteLine("\n");
            Console.WriteLine("Свойства объекта " + dog.GetType() + " :");
            ColorProperty.ColorPrint(dog);
        }
    }
}
