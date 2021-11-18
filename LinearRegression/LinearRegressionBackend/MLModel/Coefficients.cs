using System.Runtime.Serialization;

namespace MLModel
{
    [DataContract]
    class Coefficients : IExtensibleDataObject
    {
        [DataMember]
        public double Slope { get; set; }
        [DataMember]
        public double Intercept { get; set; }

        public Coefficients() { }
        public Coefficients(double m, double b)
        {
            Slope = m;
            Intercept = b;
        }

        private ExtensionDataObject extensionDataObjectValue;
        public ExtensionDataObject ExtensionData
        {
            get
            {
                return extensionDataObjectValue;
            }
            set
            {
                extensionDataObjectValue = value;
            }
        }
    }
}
