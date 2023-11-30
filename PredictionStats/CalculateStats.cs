using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionStats
{
    public static class CalculateStats
    {
        public static PredictionParameters CalculateParameters(List<XYZoZp> data)
        {
            try
            {
                double minDiff = Math.Abs(data[0].Zo - data[0].Zp);
                double maxDiff = minDiff;
                double diff;
                double sumDiff = 0.0;
                double sumDiffSquare = 0.0;
                double avgZo = data.Sum(x => x.Zo) / data.Count;
                double avgZp = data.Sum(x => x.Zp) / data.Count;
                double sZo = 0.0;
                double sZp = 0.0;
                double sumZoZp = 0.0;
                //maxDiff=data.Max(x=>Math.Abs(x.Zp-x.Zo));
                foreach (XYZoZp v in data)
                {
                    diff = Math.Abs(v.Zo - v.Zp);
                    if (diff > maxDiff) maxDiff = diff;
                    if (diff < minDiff) minDiff = diff;
                    sumDiff += diff;
                    sumDiffSquare += (diff * diff);
                    sZo += Math.Pow(v.Zo - avgZo, 2);
                    sZp += Math.Pow(v.Zp - avgZp, 2);
                    sumZoZp += (v.Zo * v.Zp);
                }
                sZo /= (data.Count - 1);
                sZp /= (data.Count - 1);
                return new PredictionParameters(maxDiff, minDiff, sumDiff / data.Count, Math.Sqrt(sumDiffSquare / data.Count), (sumZoZp - (data.Count * avgZo * avgZp)) / ((data.Count - 1) * sZo * sZp));
            }
            catch (Exception ex)
            {
                throw new CalculateStatsException("CalculateParameters", ex);
            }
        }
    }
}
