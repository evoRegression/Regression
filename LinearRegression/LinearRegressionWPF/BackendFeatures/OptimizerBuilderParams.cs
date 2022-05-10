namespace LinearRegressionWPF.BackendFeatures
{
    internal class OptimizerBuilderParams
    {
        public enum Parameter
        {
            LearningRate
        }

        public double LearningRate { get; set; }
    }
}
