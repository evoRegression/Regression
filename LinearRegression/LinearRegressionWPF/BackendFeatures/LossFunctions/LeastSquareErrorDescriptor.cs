using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendFeatures.LossFunctions
{
    class LeastSquareErrorDescriptor : ILossFunctionDescriptor
    {
        public string Name => "Least Square Error";

        public ILossFunction constructLossFunction()
        {
            return new LeastSquareError();
        }
    }
}
