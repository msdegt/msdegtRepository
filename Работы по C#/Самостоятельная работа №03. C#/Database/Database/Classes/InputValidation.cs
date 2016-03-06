using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Database
{
    class InputValidation : IValidate
    {
        public bool Check(string userText, string regex)
        {
            if (Regex.IsMatch(userText, regex))
            {
                return true;
            }
            return false;
        }
    }
}
