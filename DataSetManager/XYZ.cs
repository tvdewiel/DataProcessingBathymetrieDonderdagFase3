using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetManager
{
    public class XYZ
    {
        public XYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public override string ToString()
        {
            return FormattableString.Invariant($"{X},{Y},{Z}\n");
        }
        public override bool Equals(object? obj)
        {
            return obj is XYZ xYZ &&
                   X == xYZ.X &&
                   Y == xYZ.Y &&
                   Z == xYZ.Z;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}
