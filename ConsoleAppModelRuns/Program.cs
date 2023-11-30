// See https://aka.ms/new-console-template for more information
using DataSetManager;
using PredictionStats;
using SpatialInterpolationModel;
using System.Diagnostics;

Console.WriteLine("Hello, World!");
var data = FileDataSetManager.ReadDataSet(@"C:\data\belgica\test\DS4s.txt");
var testset = FileDataSetManager.ReadDataSet(@"C:\data\belgica\test\TSs.txt");
var timer = new Stopwatch();
timer.Start();
NearestNeighbour nn = new NearestNeighbour(data);
var p=nn.Predict(testset.data);
var res=CalculateStats.CalculateParameters(p);
writePredictions(@"c:\data\belgica\test\res\NN.txt", p, res);
timer.Stop();
TimeSpan timeTaken = timer.Elapsed;
Console.WriteLine("Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));
Console.WriteLine(res);
timer.Reset();
timer.Start();
InverseDistanceInterpolation id = new InverseDistanceInterpolation(2,4,data);
var pid = id.Predict(testset.data);
var resid = CalculateStats.CalculateParameters(pid);
writePredictions(@"c:\data\belgica\test\res\ID_2_4.txt",pid,resid);
timer.Stop();
timeTaken = timer.Elapsed;
Console.WriteLine("Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));
Console.WriteLine(resid);

void writePredictions(string fileName,List<XYZoZp> predictions,PredictionParameters par)
{
    using (StreamWriter writer = File.CreateText(fileName))
    {
        writer.WriteLine(par.ToString());
        foreach (XYZoZp prediction in predictions)
        {
            writer.WriteLine(prediction.ToString());
        }
    }
}
