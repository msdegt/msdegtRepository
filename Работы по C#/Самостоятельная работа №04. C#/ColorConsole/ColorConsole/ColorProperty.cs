using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ColorConsole
{
    class ColorProperty
    {
        public static void ColorPrint(object obj)
        {
            Type t = obj.GetType();
            PropertyInfo[] prop = t.GetProperties();

            foreach (PropertyInfo property in prop)
            {
                if (property.IsDefined(typeof (ColorPropertyAttribute), false))
                {
                    ColorPropertyAttribute a = (ColorPropertyAttribute) property.GetCustomAttribute(typeof (ColorPropertyAttribute), false);
                    Console.ForegroundColor = a.Color;
                    Console.WriteLine(property.GetValue(obj));
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(property.GetValue(obj));
                }
                
            }
        }
    }
}
