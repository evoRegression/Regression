using System.Runtime.Serialization;

namespace LinearRegressionBackend.MLModel
{
    [DataContract]
    public class Coefficients
    {
        [DataMember]
        public double Slope { get; set; }

        [DataMember]
        public double Intercept { get; set; }

        public Coefficients()
        { }

        public Coefficients(double m, double b)
        {
            Slope = m;
            Intercept = b;
        }

        public double[] getThetas()
        {
            return new double[] { Slope, Intercept };
        }

        public void setThetas(double[] newThetas)
        {
            if (newThetas.Length == 2) {
                Slope = newThetas[0];
                Intercept = newThetas[1];
            }
        }
    }
}
