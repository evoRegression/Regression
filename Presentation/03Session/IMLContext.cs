using System;

interface IMLContext
{
    void Init(IDataProvider dataProvider, IMLModel model);
    void Train();
    int Predict(int dataPoint);
}