using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetManager
{
    public class XYBoundary
    {
        public XYBoundary(double minX, double maxX, double minY, double maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }
        public double DX { get => MaxX - MinX; }
        public double DY { get => MaxY - MinY; }
        public bool WithinBounds(double x, double y)
        {
            if ((x < MinX) || (x > MaxX) || (y < MinY) || (y > MaxY)) return false;
            return true;
        }
    }
}
