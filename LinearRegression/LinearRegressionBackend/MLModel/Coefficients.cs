using System.Runtime.Serialization;

namespace MLModel
{
    [DataContract]
    class Coefficients
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
