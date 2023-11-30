using DataSetManager;
using PredictionStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialInterpolationModel
{
    public class NearestNeighbour
    {
        public NearestNeighbour(DataSet dataSet)
        {
            this.dataSet = dataSet;
        }

        public NearestNeighbour(GridDataSet gridDataSet)
        {
            GridDataSet = gridDataSet;
        }

        public DataSet dataSet { get; set; }
        public GridDataSet GridDataSet { get; set; }
        public double Z(double x, double y)
        {
            try
            {
                return FindNearestNeighbours(x, y, 1).First().Z;

            }
            catch (Exception ex)
            {
                throw new SpatialInterpolationModelException("Z", ex);
            }
        }
        public double ZGrid(double x, double y)
        {
            try
            {
                return FindNearestNeighboursGrid(x, y, 1).First().Z;

            }
            catch (Exception ex)
            {
                throw new SpatialInterpolationModelException("Z", ex);
            }
        }
        private List<XYZ> ListFromSortedList(SortedList<double, List<XYZ>> nn)
        {
            List<XYZ> list = new List<XYZ>();
            foreach (List<XYZ> l in nn.Values)
            {
                foreach (XYZ v in l) list.Add(v);
            }
            return list;
        }
        private (int, int) FindCell(double x, double y)
        {
            if (!GridDataSet.XYBoundary.WithinBounds(x, y)) throw new SpatialInterpolationModelException("FindCell bounds");
            int i = (int)((x - GridDataSet.XYBoundary.MinX) / GridDataSet.Delta);
            int j = (int)((y - GridDataSet.XYBoundary.MinY) / GridDataSet.Delta);
            if (i == GridDataSet.NX) i--;
            if (j == GridDataSet.NY) j--;
            return (i, j);
        }
        private void ProcessCell(SortedList<double, List<XYZ>> nn, int i, int j, double x, double y, int n)
        {
            foreach (XYZ p in GridDataSet.GridData[i][j])
            {
                double dsquare = Math.Pow(p.X - x, 2) + Math.Pow(p.Y - y, 2);
                if ((nn.Count < n) || (dsquare < nn.Keys[nn.Count - 1]))
                {
                    if (nn.ContainsKey(dsquare)) nn[dsquare].Add(p);
                    else nn.Add(dsquare, new List<XYZ>() { p });
                }
            }
        }
        private bool IsValidCell(int i, int j)
        {
            if ((i < 0) || (i >= GridDataSet.NX)) return false;
            if ((j < 0) || (j >= GridDataSet.NY)) return false;
            return true;
        }
        private void ProcessRing(int i, int j, int ring, SortedList<double, List<XYZ>> nn, double x, double y, int n)
        {
            for (int gx = i - ring; gx <= i + ring; gx++)
            {
                //onderste rij
                int gy = j - ring;
                if (IsValidCell(gx, gy)) ProcessCell(nn, gx, gy, x, y, n);
                //bovenste rij
                gy = j + ring;
                if (IsValidCell(gx, gy)) ProcessCell(nn, gx, gy, x, y, n);
            }
            for (int gy = j - ring + 1; gy <= j + ring - 1; gy++)
            {
                //linker kolom
                int gx = i - ring;
                if (IsValidCell(gx, gy)) ProcessCell(nn, gx, gy, x, y, n);
                //rechter kolom
                gx = i + ring;
                if (IsValidCell(gx, gy)) ProcessCell(nn, gx, gy, x, y, n);
            }
        }
        public List<XYZ> FindNearestNeighboursGrid(double x, double y, int n)
        {
            try
            {
                SortedList<double, List<XYZ>> nn = new SortedList<double, List<XYZ>>();
                (int i, int j) = FindCell(x, y);
                ProcessCell(nn, i, j, x, y, n);
                int ring = 0;
                while (nn.Count < n)
                {
                    ring++;
                    ProcessRing(i, j, ring, nn, x, y, n);
                }
                //calculate nr of correction rings
                int n_rings = (int)Math.Ceiling(Math.Sqrt(2) * (ring + 1)) - ring;
                for (int extraRings = 1; extraRings <= n_rings; extraRings++)
                {
                    ProcessRing(i, j, ring + extraRings, nn, x, y, n);//correcties
                }
                return (List<XYZ>)ListFromSortedList(nn).Take(n).ToList();
            }
            catch (Exception ex)
            {
                throw new SpatialInterpolationModelException("FindNNGrid", ex);
            }
        }
        public List<XYZ> FindNearestNeighbours(double x, double y, int n)
        {
            try
            {
                SortedList<double, List<XYZ>> nn = new SortedList<double, List<XYZ>>();
                double dsquare;
                double dmin;
                dsquare = Math.Pow(dataSet.data[0].X - x, 2) + Math.Pow(dataSet.data[0].Y - y, 2);
                dmin = dsquare;
                nn.Add(dsquare, new List<XYZ>() { dataSet.data[0] });
                for (int i = 1; i < n; i++)
                {
                    dsquare = Math.Pow(dataSet.data[i].X - x, 2) + Math.Pow(dataSet.data[i].Y - y, 2);
                    if (nn.ContainsKey(dsquare)) nn[dsquare].Add(dataSet.data[i]);
                    else nn.Add(dsquare, new List<XYZ>() { dataSet.data[i] });
                    if (dsquare > dmin) dmin = dsquare;
                }
                for (int i = n; i < dataSet.data.Count; i++)
                {
                    dsquare = Math.Pow(dataSet.data[i].X - x, 2) + Math.Pow(dataSet.data[i].Y - y, 2);
                    if (dsquare < dmin)
                    {
                        if (nn.ContainsKey(dsquare)) nn[dsquare].Add(dataSet.data[i]);
                        else nn.Add(dsquare, new List<XYZ>() { dataSet.data[i] });
                        if (nn.Count > n) nn.RemoveAt(n);
                        dmin = nn.Keys[n - 1];
                    }
                }
                return (List<XYZ>)ListFromSortedList(nn).Take(n).ToList();
            }
            catch (Exception ex)
            {
                throw new SpatialInterpolationModelException("FindNearestNeighbours", ex);
            }
        }
        public List<XYZoZp> Predict(List<XYZ> toPredict)
        {
            List<XYZoZp> pred = new List<XYZoZp>();
            try
            {
                foreach (XYZ p in toPredict)
                {
                    pred.Add(new XYZoZp(p.X, p.Y, p.Z, Z(p.X, p.Y)));
                }
                return pred;
            }
            catch (Exception ex) { throw new SpatialInterpolationModelException("Predict", ex); }
        }
        public List<XYZoZp> PredictGrid(List<XYZ> toPredict)
        {
            List<XYZoZp> pred = new List<XYZoZp>();
            try
            {
                foreach (XYZ p in toPredict)
                {
                    pred.Add(new XYZoZp(p.X, p.Y, p.Z, Zgrid(p.X, p.Y)));
                }
                return pred;
            }
            catch (Exception ex) { throw new SpatialInterpolationModelException("Predict", ex); }
        }
    }
}
