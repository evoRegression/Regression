using System;

namespace MLModel
{
    public class MLModel : IMLModel
    {
        public MLModel()
        {
            // Szerintem nem muszály nullable primitív. Maradhat a default value neki.
            this.trainedData = null;
        }
        public double Evaluation()
        {
            throw new NotImplementedException();
        }

        public int Predict(int dataPoint)
        {
            // Nem muszály hibát dobni, ha nincs betanítva a model. Max adjon vissza hülyeséget a default value-k alapján.
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
            // Nagyon jó, hogy itt is dobunk hibát, ha hülyeséget add meg a user.
            if (data == null || data.Length == 0)
                throw new Exception("Data array is empty"); // Exception helyett ArgumentNullException
            int min = int.MaxValue;
            foreach( int i in data) // i helyett beszédesebb név.
            {
                if (min > i)
                    min = i;
            }
            trainedData = min;
        }
        private int? trainedData;
    }
}
