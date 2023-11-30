using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataSetManager
{
    [Serializable]
    public class DataSet
    {
        public DataSet()
        {
        }
        [JsonConstructor]
        public DataSet(List<XYZ> data)
        {
            InitMinMax(data[0]);
            for (int i = 0; i < data.Count; i++) AddXYZ(data[i]);
        }
        private void InitMinMax(XYZ xyz)
        {
            XYBoundary = new XYBoundary(xyz.X, xyz.X, xyz.Y, xyz.Z);
            MaxZ = xyz.Z;
        }
        public List<XYZ> data { get; private set; } = new List<XYZ>();
        public XYBoundary XYBoundary { get; private set; }
        public double MinZ { get; private set; }
        public double MaxZ { get; private set; }
        public void AddXYZ(XYZ point)
        {
            if (point == null) throw new DataSetManagerException("DataSet-AddXYZ - null value");
            if (data.Count == 0) InitMinMax(point);
            else
            {
                if (point.X < XYBoundary.MinX) XYBoundary.MinX = point.X;
                if (point.X > XYBoundary.MaxX) XYBoundary.MaxX = point.X;
                if (point.Y < XYBoundary.MinY) XYBoundary.MinY = point.Y;
                if (point.Y > XYBoundary.MaxY) XYBoundary.MaxY = point.Y;
                if (point.Z < MinZ) MinZ = point.Z;
                if (point.Z > MaxZ) MaxZ = point.Z;
            }
            data.Add(point);
        }
    }
}
