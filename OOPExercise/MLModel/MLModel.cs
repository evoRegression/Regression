using System;
using System.Linq;

namespace MLModel
{
    public class MLModel : IMLModel
    {
        public MLModel()
        {
            this.trainedData = null;
        }
        public double Evaluation()
        {
            return (double) trainedData - 100;
        }

        public int Predict(int dataPoint)
        {
            if (trainedData == null)
            {
                throw new Exception("Modell is not trained");
            }
            if (dataPoint < trainedData)
                return dataPoint;
            return (int)trainedData;
        }
        public void Train(int[] data)
        { 
            if (data == null || data.Length == 0)
                throw new Exception("Data array is empty");
            
            trainedData = (int) data.Select(dataPoint => Math.Sqrt(dataPoint)).Sum();
        }
        private int? trainedData;
    }
}
