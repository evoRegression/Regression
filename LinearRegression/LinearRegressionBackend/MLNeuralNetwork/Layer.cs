using System.Text.Json.Serialization;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class Layer
    {

        public Matrix<double> Weight;
        public double[][] WeightArray
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
        public Vector<double> Bias;
        public double[] BiasArray
        {
            get
            {
                return Bias.ToArray();
            }
        }
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

        [JsonConstructor]
        public Layer(
            double[][] weightArray,
            double[] biasArray)
        { }

        public void Propagate(Propagation prop)
        {
            Vector<double> sum = Weight * prop.Output() + Bias;
            Vector<double> activation = ActivationFunction.Activation(sum);
            prop.WeightedSums.Add(sum);
            prop.Activations.Add(activation);
        }

    }
}
