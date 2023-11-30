using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionStats
{
    public class XYZoZp
    {
        public XYZoZp(double x, double y, double zo, double zp)
        {
            X = x;
            Y = y;
            Zo = zo;
            Zp = zp;
        }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Zo { get; private set; } //observed
        public double Zp { get; private set; } //predicted

        public override bool Equals(object? obj)
        {
            return obj is XYZoZp zp &&
                   X == zp.X &&
                   Y == zp.Y &&
                   Zo == zp.Zo &&
                   Zp == zp.Zp;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Zo, Zp);
        }

        public override string ToString()
        {
            return FormattableString.Invariant($"{X},{Y},{Zo},{Zp}");
        }
    }
}
