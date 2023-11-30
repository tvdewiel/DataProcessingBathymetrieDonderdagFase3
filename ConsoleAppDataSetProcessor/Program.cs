// See https://aka.ms/new-console-template for more information
using DataSetManager;
using System.Diagnostics;

Console.WriteLine("Hello, World!");
var timer = new Stopwatch();
timer.Start();
var data = FileDataSetManager.ReadData(@"C:\data\belgica\belgica.txt");
timer.Stop();
TimeSpan timeTaken = timer.Elapsed;
Console.WriteLine("Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));
timer.Reset();
timer.Start();
var sets = FileDataSetManager.MakeDataSetsWithTestSet(data.data, new List<int> { 5000, 200000/*, 25000, 50000, /*1000000, 1500000, 2000000, 2500000*/ });
timer.Stop();
timeTaken = timer.Elapsed;
Console.WriteLine("Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));
timer.Reset(); 
timer.Start();
FileDataSetManager.WriteDataSets(sets,
    new List<string> { @"C:\data\belgica\test\TSs.txt",
                @"C:\data\belgica\test\DS1s.txt"/*,
                @"C:\data\belgica\test\DS2s.txt",
                @"C:\data\belgica\test\DS3s.txt"*///,
                //@"C:\data\belgica\test\DS4s.txt",
                //@"C:\data\belgica\test\DS5s.txt",
                //@"C:\data\belgica\test\DS6s.txt",
                //@"C:\data\belgica\test\DS7s.txt"
                });
timer.Stop();
timeTaken = timer.Elapsed;
Console.WriteLine("Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));
