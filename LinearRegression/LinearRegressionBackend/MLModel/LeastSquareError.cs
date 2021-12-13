﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegressionBackend.MLModel
{
    public class LeastSquareError : ILossFunction
    {
        public double Loss(double[] thetas, double[][] inputData, double[] targetData)
        {
            if (inputData.Length == 0)
                return 0;

            double[] xAxis = inputData.Select(array => (double)array.GetValue(0)).ToArray();
            double[] yAxis = inputData.Select(array => (double)array.GetValue(1)).ToArray();
            return xAxis.Zip(yAxis, (x, y) => Math.Pow(y - (x*thetas[0] + thetas[1]), 2)).Sum() / xAxis.Length;
        }
        public double[] LossDerivates(double[] thetas, double[][] inputData, double[] targetData)
        {
            double[] xAxis = inputData.Select(array => (double)array.GetValue(0)).ToArray();
            double dSlope = SlopeDerivate(thetas, xAxis, targetData);
            double dIntercept = InterceptDerivate(thetas, xAxis, targetData);
            return new double[] { dSlope, dIntercept };
        }

        internal double SlopeDerivate(double[] thetas, double[] xAxis, double[] yAxis)
        {
            return xAxis.Zip(yAxis, (x, y) => (thetas[0] * x + thetas[1] - y) * x).Sum() / xAxis.Length;
        }

        internal double InterceptDerivate(double[] thetas, double[] xAxis, double[] yAxis)
        {
            return xAxis.Zip(yAxis, (x, y) => thetas[0] * x + thetas[1] - y).Sum() / xAxis.Length;
        }
    }
}
