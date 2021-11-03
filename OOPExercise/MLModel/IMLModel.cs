namespace MLModel
{
    public interface IMLModel
    {
        void Train(int[] data);
        int Predict(int dataPoint);
        double Evaluation();
    }
}
