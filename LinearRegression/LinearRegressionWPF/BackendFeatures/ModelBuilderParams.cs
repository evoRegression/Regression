namespace LinearRegressionWPF.BackendFeatures
{
    class ModelBuilderParams
    {
        public LossFunctionDescriptor LossFunctionDesc { get; set; }
        public OptimizerDescriptor OptimizerDesc { get; set; }
        public double LearningRate { get; set; }
        public double Slope { get; set; }
        public double YIntercept { get; set; }
    }
}
