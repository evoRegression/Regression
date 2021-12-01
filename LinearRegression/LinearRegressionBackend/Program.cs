using MLContext;
using DataProvider;
using MLModel;
using System;

namespace OOPExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            IMLContext context = new MLContext.MLContext();
            IMLModel model = new MLModel.MLModel();
            IDataProvider dataProvider = new DataProvider.DataProvider();

            context.Init(dataProvider, model);
            context.Train();
            System.Console.Write("Give me a data point: ");
            double input = double.Parse(System.Console.ReadLine());
            double prediction = context.Predict(input);

            model.Export("Model.xml");

            System.Console.WriteLine($"Result: {prediction}");   
        }
    }
}
