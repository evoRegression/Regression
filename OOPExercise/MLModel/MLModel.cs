using System;

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
            throw new NotImplementedException();
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
            int min = int.MaxValue;
            foreach( int i in data)
            {
                if (min > i)
                    min = i;
            }
            trainedData = min;
        }
        private int? trainedData;
    }
}
