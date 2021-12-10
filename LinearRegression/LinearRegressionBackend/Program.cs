﻿using LinearRegressionBackend.MLContext;
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
            IMLContext context = new MLContext.MLContext();
            IMLModel model = new MLModel.MLModel(0,0);
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

                model.Export("Model.xml");

                Console.WriteLine($"Result: {prediction}");
            }
        }
    }
}
