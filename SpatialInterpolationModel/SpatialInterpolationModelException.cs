using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialInterpolationModel
{
    public class SpatialInterpolationModelException : Exception
    {
        public SpatialInterpolationModelException(string? message) : base(message)
        {
        }

        public SpatialInterpolationModelException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
