using System.Drawing;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.DataProvider
{
    public class ImageProcess : IImageConverter
    {
        public Vector<double> CreateLabel(string filename)
        {
            double[] labelArray = new double[3]; //in the case we have 3 shapes 

            if (filename.ToLower().Contains("circle"))
            {

                labelArray = new double[3] { 1, 0, 0 };
            }
            else if (filename.ToLower().Contains("square")) {

                labelArray = new double[3] { 0, 1, 0 };
            }
            else if (filename.ToLower().Contains("triangle")) {
                
                    labelArray = new double[3] { 0, 0, 1 };
            }
        
             Vector<double> labelVector = Vector<double>.Build.DenseOfArray(labelArray);

            return labelVector;
        }

        public Vector<double> GrayScale(Bitmap image)
        {
            double[,] pixels = new double[image.Width, image.Height];
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color cl = image.GetPixel(i, j);
                    int redColor = cl.R;
                    int greenColor = cl.G;
                    int blueColor = cl.B;

                    float gray = (float)(.299 * redColor + .587 * greenColor + .114 * blueColor);

                    pixels[i, j] = (gray == 255) ? 0 : 1 - (gray / 256);
                }
            }

            int index = 0;
            double[] arrayOfPixels = new double[pixels.Length];
            for (int i = 0; i < pixels.GetLength(0); i++)
            {
                for (int j = 0; j < pixels.GetLength(1); j++)
                {
                    arrayOfPixels[index] = pixels[i, j];
                    index++;
                }
            }

            Vector<double> pixelVector = Vector<double>.Build.DenseOfArray(arrayOfPixels);

            return pixelVector;
        }

        public Bitmap Scale(Bitmap bmp, int newWidth, int newHeight)
        {
            return new Bitmap(bmp, new Size(newWidth, newHeight));
        }
    }
}
