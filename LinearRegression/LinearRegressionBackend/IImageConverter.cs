using System;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend
{
    /// <summary>
    /// 
    /// </summary>
    public interface IImageConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pixels"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns></returns>
        Vector<double> Scale(double[,] pixels, int newWidth, int newHeight);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pixels"></param>
        /// <returns></returns>
        Vector<double> GrayScale(Vector<double> pixels);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        Vector<double> CreateLabel(string filename);
    }
}
