using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    class BuilderProject
    {
        private IOperations operations;
        private IValidate validate;
        private IReadFile read;
        private ICheckFile check;
        private IDrawTable draw;
        public void BuildProject()
        {
            operations = new OperationsMenu();
            validate = new InputValidation();
            read = new LogRead();
            check = new CheckFileExists();
            draw = new DrawTableResults();

            new Menu().Show(operations, validate, read, check, draw);
        }
    }
}
