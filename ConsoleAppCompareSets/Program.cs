using DataSetManager;
using PredictionStats;
using SpatialInterpolationModel;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleAppCompareSets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string source = @"c:\data\belgica\belgica.txt";

            var data = FileDataSetManager.ReadData(source);

            var gds = FileDataSetManager.MakeGridDataSetsWithTestSet(data, 200, new List<(int, double)> { (500, 20) });
            var sds = FileDataSetManager.MakeDataSetsWithTestSet(data.data, new List<int> { 200, 500 });

            var d1 = sds[1].data.OrderBy(x => x.Z).ToList();
            var d2 = gds.Item1.data.OrderBy(x => x.Z).ToList();
            var d3 = gds.Item2[0].GridData.SelectMany(x=>x.SelectMany(y=>y)).OrderBy(x => x.Z).ToList();
            var dif1=d1.Except(d3).OrderBy(x=>x.Z).ToList();
            var dif2=d3.Except(d1).OrderBy(x => x.Z).ToList();
            var testset = gds.Item1.data;

            InverseDistanceInterpolation id = new InverseDistanceInterpolation(2, 4, gds.Item2[0]);
            //InverseDistanceInterpolation id = new InverseDistanceInterpolation(2, 4, gds.Item2[0]);
            var resgrid = id.PredictGrid(testset);
            var calcPredValues1 = CalculateStats.CalculateParameters(resgrid);
            Console.WriteLine(calcPredValues1);

            InverseDistanceInterpolation id2 = new InverseDistanceInterpolation(2, 4, sds[1]);
            var res2 = id2.Predict(testset);
            var calcPredValues2 = CalculateStats.CalculateParameters(res2);
            Console.WriteLine(calcPredValues2);

            var difP1 = resgrid.Except(res2).ToList();
            var difP2 = res2.Except(resgrid).ToList();
        }
    }
}