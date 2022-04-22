using System;

using LinearRegressionBackend.DataProvider;

namespace LinearRegressionBackend.OOPExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] arrayUnsorted = { 1, 5, 3, 9, 5, 6 };

            Array.Sort(arrayUnsorted);

            double[][] myArray4 = new double[5][];
            myArray4[0] = new double[2];
            myArray4[1] = new double[2];
            myArray4[2] = new double[2];
            myArray4[3] = new double[2];
            myArray4[4] = new double[2];

            myArray4[0][0] = 1; myArray4[0][1] = 82;
            myArray4[1][0] = 3; myArray4[1][1] = 93;
            myArray4[2][0] = 5; myArray4[2][1] = 98;
            myArray4[3][0] = 7; myArray4[3][1] = 89;
            myArray4[4][0] = 9; myArray4[4][1] = 88;

            double testDongle = Numerical.StandardDeviation(myArray4, 1);
            Console.WriteLine(testDongle);

            Numerical.SaveText("C:/Users/cnsor/Desktop/test/test.txt", myArray4);

            Console.ReadLine();
        }
    }
}
