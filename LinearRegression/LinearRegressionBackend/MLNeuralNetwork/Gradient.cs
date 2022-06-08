using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class Gradient
    {

        public Matrix<double>[] WeightGradient;
        public Vector<double>[] BiasGradient;

        public Gradient(int layerCount)
        {
            WeightGradient = new Matrix<double>[layerCount];
            BiasGradient = new Vector<double>[layerCount];
        }

    }
}
