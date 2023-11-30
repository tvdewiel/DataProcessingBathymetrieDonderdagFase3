using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionStats
{
    public class PredictionParameters
    {
        public PredictionParameters(double maxDeviation, double minDeviation, double mAE, double rMSE, double pPMC)
        {
            MaxDeviation = maxDeviation;
            MinDeviation = minDeviation;
            MAE = mAE;
            RMSE = rMSE;
            PPMC = pPMC;
        }

        public double MaxDeviation { get; set; }
        public double MinDeviation { get; set; }
        public double MAE { get; set; }
        public double RMSE { get; set; }
        public double PPMC { get; set; } //Pearson's product moment correlation
        public override string ToString()
        {
            return FormattableString.Invariant($"Min : {MinDeviation},Max : {MaxDeviation},MAE: {MAE},RMSE : {RMSE},PPMC : {PPMC}");
        }
    }
}
