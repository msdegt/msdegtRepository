using System.Collections.Generic;

namespace Database
{
    public interface IDrawTable
    {
        string MakeTableResults(List<Employee> listEmp);
    }
}