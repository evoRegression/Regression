﻿using System;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class ReLU : IActivationFunction
    {
        public string GetSerializedName()
        {
            return "ReLU";
        }

        public double Activation(double weightedSum)
        {
            return Math.Max(0, weightedSum);
        }

        public double Derivative(double weightedSum)
        {
            throw new NotImplementedException();
        }
    }
}
