using System.Collections.Generic;

namespace Database
{
    public interface ICheckFile
    {
        List<Employee> CheckFile(ILog log, IReadFile read);
    }
}