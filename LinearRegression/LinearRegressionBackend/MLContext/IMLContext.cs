using System;
using DataProvider;
using LinearRegressionBackend.MLModel;

namespace MLContext
{
    public interface IMLContext
    {
        void Init(IDataProvider dataProvider, IMLModel model);
        void Train();
        double Predict(double dataPoint);

    }
}