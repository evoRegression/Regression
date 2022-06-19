using System;
using System.Collections.Generic;

using LinearRegressionBackend;
using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendDescriptors
{
    static class AvailableModels
    {
        public static ModelDescriptor[] AvailableModelsArray;
        private static readonly Dictionary<string, LossFunctionDescriptor> AvailableLossFunctions;
        private static readonly Dictionary<string, EstimatorDescriptor> AvailableOptimizers;

        static AvailableModels()
        {
            AvailableLossFunctions = new Dictionary<string, LossFunctionDescriptor> {
                {
                    "LeastAbsoluteError",
                    new LossFunctionDescriptor {
                        Name = "Least Absolute Error",
                        BuildLossFunction = () => new LeastAbsoluteError()
                    }
                },
                {
                    "LeastSquareError",
                    new LossFunctionDescriptor {
                        Name = "Least Square Error",
                        BuildLossFunction = () => new LeastSquareError()
                    }
                }
            };

            AvailableOptimizers = new Dictionary<string, EstimatorDescriptor> {
                {
                    "SimpleOrdinaryLeastSquare",
                    new EstimatorDescriptor {
                        Name = "Simple Ordinary Least Square",
                        IsIterative = false,
                        SupportedParameters = Array.Empty<OptimizerBuilderParams.Parameter>(),
                        BuildOptimizer = (parameters) => new SimpleOrdinaryLeastSquare()
                    }
                },
                {
                    "QuadraticOrdinaryLeastSquare",
                    new EstimatorDescriptor {
                        Name = "Quadratic Ordinary Least Square",
                        IsIterative = false,
                        SupportedParameters = Array.Empty<OptimizerBuilderParams.Parameter>(),
                        BuildOptimizer = (parameters) => new QuadraticOrdinaryLeastSquare()
                    }
                },
                {
                    "GradientDescent",
                    new EstimatorDescriptor {
                        Name = "Gradient Descent",
                        IsIterative = true,
                        SupportedParameters = new OptimizerBuilderParams.Parameter[] {
                            OptimizerBuilderParams.Parameter.LearningRate
                        },
                        BuildOptimizer = (parameters) => {
                            double learningRate = parameters.LearningRate;

                            return new GradientDescent(learningRate);
                        }
                    }
                },
                {
                    "NormalEquation",
                    new EstimatorDescriptor {
                        Name = "Normal Equation",
                        IsIterative = false,
                        SupportedParameters = Array.Empty<OptimizerBuilderParams.Parameter>(),
                        BuildOptimizer = (parameters) => new NormalEquation()
                    }
                }
            };

            AvailableModelsArray = new ModelDescriptor[] {
                new ModelDescriptor {
                    Name = "Linear Regression",

                    SupportedLossFunctions = new LossFunctionDescriptor[] {
                        AvailableLossFunctions["LeastSquareError"],
                        AvailableLossFunctions["LeastAbsoluteError"]
                    },

                    SupportedOptimizers = new EstimatorDescriptor[] {
                        AvailableOptimizers["SimpleOrdinaryLeastSquare"],
                        AvailableOptimizers["QuadraticOrdinaryLeastSquare"],
                        AvailableOptimizers["GradientDescent"],
                        AvailableOptimizers["NormalEquation"]
                    },

                    BuildModel = (parameters) => {
                        double slope = parameters.Slope;
                        double yIntercept = parameters.YIntercept;
                        IEstimator estimator = parameters.OptimizerDesc.BuildOptimizer(
                            new OptimizerBuilderParams { LearningRate = parameters.LearningRate });
                        ILossFunction lossFunction = parameters.LossFunctionDesc.BuildLossFunction();

                        return new LinearRegressionModel(slope, yIntercept, estimator, lossFunction);
                    }
                }
            };
        }
    }
}
