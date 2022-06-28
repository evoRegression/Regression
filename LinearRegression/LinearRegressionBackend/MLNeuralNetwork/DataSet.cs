using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.DataProvider;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class DataSet
    {
        public Matrix<double> TrainingInput;
        public Matrix<double> TrainingOutput;

        public double[][] TrainingInputArray
        {
            get => Numerical.MatrixToJaggedArray(TrainingInput);
        }
        public double[][] TrainingOutputArray
        {
            get => Numerical.MatrixToJaggedArray(TrainingOutput);
        }

        public Matrix<double> TestingInput;
        public Matrix<double> TestingOutput;

        public double[][] TestingInputArray
        {
            get => Numerical.MatrixToJaggedArray(TestingInput);
        }
        public double[][] TestingOutputArray
        {
            get => Numerical.MatrixToJaggedArray(TestingOutput);
        }

        public DataSet() { }

        [JsonConstructor]
        public DataSet(
            double[][] trainingInputArray,
            double[][] trainingOutputArray,
            double[][] testingInputArray,
            double[][] testingOutputArray)
        {
            if (trainingInputArray is not null)
            {
                TrainingInput =
                    Matrix<double>.Build.DenseOfRowArrays(trainingInputArray);
            }
            
            if (trainingOutputArray is not null)
            {
                TrainingOutput =
                    Matrix<double>.Build.DenseOfRowArrays(trainingOutputArray);
            }
            
            if (testingInputArray is not null)
            {
                TestingInput =
                    Matrix<double>.Build.DenseOfRowArrays(testingInputArray);
            }
            
            if (testingOutputArray is not null)
            {
                TestingOutput =
                    Matrix<double>.Build.DenseOfRowArrays(testingOutputArray);
            }
        }

        public async Task Export(
            Stream outputStream,
            JsonSerializerOptions options = null)
        {
            await JsonSerializer.SerializeAsync(outputStream, this, options);
        }

        public static async Task<DataSet> Import(Stream inputStream)
        {
            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            return await JsonSerializer.DeserializeAsync<DataSet>(
                inputStream, options);
        }
    }
}
