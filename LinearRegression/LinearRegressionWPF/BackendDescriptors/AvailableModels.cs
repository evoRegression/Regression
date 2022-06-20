using System.Collections.Generic;

using LinearRegressionBackend;
using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendDescriptors
{
    static class AvailableModels
    {
        public static ModelDescriptor[] AvailableModelsArray;
        private static Dictionary<string, LossFunctionDescriptor> AvailableLossFunctions;
        private static Dictionary<string, OptimizerDescriptor> AvailableOptimizers;

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

            AvailableOptimizers = new Dictionary<string, OptimizerDescriptor> {
                //{
                //    "SimpleOrdinaryLeastSquare",
                //    new OptimizerDescriptor {
                //        Name = "Simple Ordinary Least Square",
                //        IsIterative = false,
                //        SupportedParameters = new OptimizerBuilderParams.Parameter[] { },
                //        BuildOptimizer = (parameters) => new SimpleOrdinaryLeastSquare()
                //    }
                //},
                //{
                //    "QuadraticOrdinaryLeastSquare",
                //    new OptimizerDescriptor {
                //        Name = "Quadratic Ordinary Least Square",
                //        IsIterative = false,
                //        SupportedParameters = new OptimizerBuilderParams.Parameter[] { },
                //        BuildOptimizer = (parameters) => new QuadraticOrdinaryLeastSquare()
                //    }
                //},
                {
                    "GradientDescent",
                    new OptimizerDescriptor {
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
                //{
                //    "NormalEquation",
                //    new OptimizerDescriptor {
                //        Name = "Normal Equation",
                //        IsIterative = false,
                //        SupportedParameters = new OptimizerBuilderParams.Parameter[] { },
                //        BuildOptimizer = (parameters) => new NormalEquation()
                //    }
                //}
            };

            AvailableModelsArray = new ModelDescriptor[] {
                new ModelDescriptor {
                    Name = "Linear Regression",

                    SupportedLossFunctions = new LossFunctionDescriptor[] {
                        AvailableLossFunctions["LeastSquareError"],
                        AvailableLossFunctions["LeastAbsoluteError"]
                    },

                    SupportedOptimizers = new OptimizerDescriptor[] {
                        AvailableOptimizers["GradientDescent"],
                        //AvailableOptimizers["SimpleOrdinaryLeastSquare"],
                        //AvailableOptimizers["QuadraticOrdinaryLeastSquare"],
                        //AvailableOptimizers["NormalEquation"]
                    },

                    BuildModel = (parameters) => {
                        double slope = parameters.Slope;
                        double yIntercept = parameters.YIntercept;
                        IOptimizer optimizer = parameters.OptimizerDesc.BuildOptimizer(
                            new OptimizerBuilderParams { LearningRate = parameters.LearningRate });
                        ILossFunction lossFunction = parameters.LossFunctionDesc.BuildLossFunction();

                        return new LinearRegressionModel(slope, yIntercept, optimizer, lossFunction);
                    }
                }
            };
        }
    }
}
