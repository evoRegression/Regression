namespace LinearRegressionWPF.BackendDescriptors
{
    class ModelBuilderParams
    {
        public LossFunctionDescriptor LossFunctionDesc { get; set; }
        public EstimatorDescriptor OptimizerDesc { get; set; }
        public double LearningRate { get; set; }
        public double Slope { get; set; }
        public double YIntercept { get; set; }
    }
}
