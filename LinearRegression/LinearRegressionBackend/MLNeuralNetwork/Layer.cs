using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class Layer
    {

        public Matrix<double> Weight { get; set; }
        public double[][] WeightValues
        {
            get
            {
                double[][] weightValues = new double[Weight.RowCount][];
                foreach ((int index, Vector<double> row)
                    in Weight.EnumerateRowsIndexed())
                {
                    weightValues[index] = row.ToArray();
                }
                return weightValues;
            }
        }
        public Vector<double> Bias { get; set; }
        public IActivationFunction ActivationFunction;

        public Layer(
            Matrix<double> weight,
            Vector<double> bias,
            IActivationFunction activationFunction)
        {
            Weight = weight;
            Bias = bias;
            ActivationFunction = activationFunction;
        }

        public void Propagate(Propagation prop)
        {
            Vector<double> sum = Weight * prop.Output() + Bias;
            Vector<double> activation = ActivationFunction.Activation(sum);
            prop.WeightedSums.Add(sum);
            prop.Activations.Add(activation);
        }

    }
}
