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
    }
}
