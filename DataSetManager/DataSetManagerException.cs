using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetManager
{
    public class DataSetManagerException : Exception
    {
        public DataSetManagerException(string? message) : base(message)
        {
        }

        public DataSetManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
