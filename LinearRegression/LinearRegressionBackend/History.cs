namespace LinearRegressionBackend
{
    public class History
    {
        public double Loss { get; private set; }

        public double[] Parameters { get; private set; }

        public History(double loss, double[] parameters)
        {
            Loss = loss;
            Parameters = parameters;
        }
    }
}
