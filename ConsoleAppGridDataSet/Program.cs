







// See https://aka.ms/new-console-template for more information
using DataSetManager;
using PredictionStats;
using SpatialInterpolationModel;
using System.Diagnostics;

Console.WriteLine("Hello, World!");

string source = @"c:\data\belgica\belgica.txt";
var timer = new Stopwatch();
timer.Start();
var data = FileDataSetManager.ReadData(source);
timer.Stop();
TimeSpan timeTaken = timer.Elapsed;
Console.WriteLine("Time taken read source: " + timeTaken.ToString(@"m\:ss\.fff"));
timer.Reset();
timer.Start();
var gds = FileDataSetManager.MakeGridDataSetsWithTestSet(data,10000, new List<(int,double)> { (50000,2)/*,(200000,100),(100000,10)*/ });
timer.Stop();
timeTaken = timer.Elapsed;
Console.WriteLine("Time taken create grid data sets: " + timeTaken.ToString(@"m\:ss\.fff"));

timer.Reset();
timer.Start();
var sds = FileDataSetManager.MakeDataSetsWithTestSet(data.data,new List<int>{ 10000, 50000 });
timer.Stop();
timeTaken = timer.Elapsed;
Console.WriteLine("Time taken create seq data sets: " + timeTaken.ToString(@"m\:ss\.fff"));

timer.Reset();
timer.Start();
InverseDistanceInterpolation id = new InverseDistanceInterpolation(2, 4, gds.Item2[0]);
var res = id.PredictGrid(gds.Item1.data);
var calcPredValues = CalculateStats.CalculateParameters(res);
Console.WriteLine(calcPredValues);
timer.Stop();
timeTaken = timer.Elapsed;
Console.WriteLine("Time taken predict with grid: " + timeTaken.ToString(@"m\:ss\.fff"));


timer.Reset();
timer.Start();
InverseDistanceInterpolation id2 = new InverseDistanceInterpolation(2, 4, sds[1]);
var res2 = id2.Predict(gds.Item1.data);
var calcPredValues2 = CalculateStats.CalculateParameters(res2);
Console.WriteLine(calcPredValues2);
//calcPredValues2 = CalculateStats.CalculateParameters(res);
//Console.WriteLine(calcPredValues2);
timer.Stop();
timeTaken = timer.Elapsed;
Console.WriteLine("Time taken predict with seq: " + timeTaken.ToString(@"m\:ss\.fff"));
Console.WriteLine("end");
