using System;
using LinearRegressionBackend.DataProvider;
using LinearRegressionBackend.MLModel;

namespace LinearRegressionBackend.MLContext
{
    public interface IMLContext
    {
        void Init(IMLModel model);
        void Train();
        double Predict(double dataPoint);

    }
}