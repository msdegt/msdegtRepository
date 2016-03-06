using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class LogRead : IReadFile
    {
        public string ReadLine()
        {
            string line = "";
            using (StreamReader sr = new StreamReader("option.ini", System.Text.Encoding.Default))
            {
                line = sr.ReadLine();
            }
            return line;
        }
    }
}
