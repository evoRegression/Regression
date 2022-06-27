using LinearRegressionBackend.MLNeuralNetwork;

namespace LinearRegressionWPF.BackendDescriptors
{
    static class AvailableActivationFunctions
    {
        public static ActivationFunctionDescriptor[] AvailableActivationFunctionsArray;

        static AvailableActivationFunctions()
        {
            AvailableActivationFunctionsArray = new ActivationFunctionDescriptor[] {
                new ActivationFunctionDescriptor {
                    Name = "Sigmoid",
                    BuildActivationFunction = () => new Sigmoid(),
                },
                new ActivationFunctionDescriptor {
                    Name = "ReLU",
                    BuildActivationFunction = () => new ReLU(),
                },
                new ActivationFunctionDescriptor {
                    Name = "Tanh",
                    BuildActivationFunction = () => new Tanh(),
                },
            };
        }
    }
}
