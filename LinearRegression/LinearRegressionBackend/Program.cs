using LinearRegressionBackend.MLContext;
using LinearRegressionBackend.DataProvider;
using LinearRegressionBackend.MLModel;
using System;
using LinearRegressionBackend.DataProvider.Exceptions;

namespace LinearRegressionBackend.OOPExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            /*IMLContext context = new MLContext.MLContext();
            MLModel.MLModel model = new MLModel.MLModel(0,0);
            IDataProvider dataProvider = new DataProvider.DataProvider();

            Boolean correctData = false;
            try
            {
                context.Init(dataProvider, model);
                correctData = true;
            }
            catch (InvalidDataException e)
            {
                Console.WriteLine(e.Message);
            }

            if (correctData)
            {
                context.Train();
                Console.Write("Give me a data point: ");
                double input = double.Parse(Console.ReadLine());
                double prediction = context.Predict(input);

                model.Save("Model.xml");

                Console.WriteLine($"Result: {prediction}");
            */
            double[] arrayUnsorted = { 1, 5, 3, 9, 5, 6 };
            // Array.Sort(arrayUnsorted);

            double[] arraySorted = Numerical.SortArray(arrayUnsorted);

            //double[][] myArray3 = new double{ { 1, 2 }, { 3, 4 }, { 5, 6 } };
            double[][] myArray4 = new double[5][];
            myArray4[0] = new double[2];
            myArray4[1] = new double[2];
            myArray4[2] = new double[2];
            myArray4[3] = new double[2];
            myArray4[4] = new double[2];

            myArray4[0][0] = 1;
            myArray4[0][1] = 82;
            myArray4[1][0] = 3;
            myArray4[1][1] = 93;
            myArray4[2][0] = 5;
            myArray4[2][1] = 98;
            myArray4[3][0] = 7;
            myArray4[3][1] = 89;
            myArray4[4][0] = 9;
            myArray4[4][1] = 88;

            double[,] multiDimensionalArray = { { 1, 2 }, { 3, 4 }, { 3, 4 }, { 5, 7 }, { 9, 10 } };

            double testDongle =Numerical.StandardDeviation(myArray4,1);
            Console.WriteLine(testDongle);
            /*
            testDongle = Numerical.Max(myArray4,1);
            Console.WriteLine(testDongle);
            testDongle = Numerical.Min(myArray4,1);
            Console.WriteLine(testDongle);
            testDongle = Numerical.Variance(myArray4, 1);
            Console.WriteLine(testDongle);

            arrayUnsorted = Numerical.Arange(1, 10, 1);

            testDongle = Numerical.Variance(myArray4, 1);
            Console.WriteLine(testDongle);
            */
            Numerical.SaveText("C:/Users/cnsor/Desktop/test/test.txt", myArray4);

            Console.ReadLine();
        }
        
    }
}
