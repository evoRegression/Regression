using System;

namespace MLModel
{
    public interface IMLModel
    {
        void Train(double[][] data);
        double Predict(double dataPoint);
        double Evaluation();
        void Export(String path);
        void Import(String path);
    }
}
