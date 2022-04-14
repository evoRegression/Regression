﻿using System;

namespace LinearRegressionBackend.MLNeuralNetwork {
    public class ReLU : IActivationFunction {
        public double Activation(double weightedSum) {
            return Math.Max(0, weightedSum);
        }
    }
}