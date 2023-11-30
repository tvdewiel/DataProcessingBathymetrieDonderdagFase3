using DataSetManager;
using SpatialInterpolationModel;

namespace SearchMethods
{
    public class BruteForceSearch : IDataSearch
    {
        private DataSet dataSet;

        public BruteForceSearch(DataSet dataSet)
        {
            this.dataSet = dataSet;
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
        private List<XYZ> ListFromSortedList(SortedList<double, List<XYZ>> nn)
        {
            List<XYZ> list = new List<XYZ>();
            foreach (List<XYZ> l in nn.Values)
            {
                foreach (XYZ v in l) list.Add(v);
            }
            return list;
        }
    }
}