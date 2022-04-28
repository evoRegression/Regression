using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegressionBackend.MLModel
{
    public class History 
    {
        public double Loss
        {
            get;
            private set;
        }

        public double[] Thetas
        {
            get;
            private set;
        }

        public History(double loss, double[] thetas)
        {
            Loss = loss;
            Thetas = thetas;
        }
    }
}
