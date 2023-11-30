using DataSetManager;
using PredictionStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpatialInterpolationModel
{
    public class ModelProcessor
    {
        public ModelProcessor(IModel model)
        {
            this.model = model;
        }

        public IModel model { get; set; }
        public List<XYZoZp> Predict(List<XYZ> toPredict)
        {
            List<XYZoZp> pred = new List<XYZoZp>();
            try
            {
                foreach (XYZ p in toPredict)
                {
                    pred.Add(new XYZoZp(p.X, p.Y, p.Z, model.Z(p.X, p.Y)));
                }
                return pred;
            }
            catch (Exception ex) { throw new SpatialInterpolationModelException("Predict", ex); }
        }

    }
}
