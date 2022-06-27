using System;
using System.Drawing;
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
         Bitmap Scale(Bitmap bmp, int newWidth, int newHeight);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pixels"></param>
        /// <returns></returns>
        Vector<double> GrayScale(Bitmap image);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        Vector<double> CreateLabel(string filename);
    }
}
