using System.Text.Json.Serialization;

using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.DataProvider;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class Layer
    {

        public Matrix<double> Weight;
        public double[][] WeightArray
        {
            get => Numerical.MatrixToJaggedArray(Weight);
        }
        public Vector<double> Bias;
        public double[] BiasArray
        {
            get => Bias.ToArray();
        }
        public IActivationFunction ActivationFunction;
        public string ActivationFunctionName
        {
            get => ActivationFunction.GetSerializedName();
        }

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
            double[] biasArray,
            string activationFunctionName)
        {
            Weight = Matrix<double>.Build.DenseOfRowArrays(weightArray);
            Bias = Vector<double>.Build.Dense(biasArray);
            ActivationFunction =
                AvailableActivationFunctions
                    .Builders[activationFunctionName]();
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
