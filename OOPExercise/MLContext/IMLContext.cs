using System;
using DataProvider;
using MLModel;

namespace MLContext
{
    interface IMLContext
    {
        void Init(IDataProvider dataProvider, IMLModel model);
        void Train();
        int Predict(int dataPoint);
    }
}