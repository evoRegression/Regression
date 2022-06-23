using LinearRegressionBackend.MLNeuralNetwork;
using LinearRegressionWPF.Commands;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Point = System.Windows.Point;

namespace LinearRegressionWPF.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    internal class ImageToVectorViewModel
    {

        public ICommand PredictCommand { get; set; }

        public NeuralNetwork NeuralNetwork { get; set; }

        public  Vector<double> Result{ get; set;}

        /// <summary>
        /// 
        /// </summary>
        public ImageToVectorViewModel()
        {
            PredictCommand = new PredictCommand(this);

            InitNeuralNetwork();

            _strokes = new StrokeCollection();
            /*(_strokes as INotifyCollectionChanged).CollectionChanged += delegate
            {
                //the strokes have changed
            };*/
        }

        /// <summary>
        /// _viewModel.Result = _viewModel.NeuralNetwork.Propagate(vectorFromDrawing).Output();

        /// </summary>

        

        /*
         *  using (Stream inputStream = File.OpenRead(FILENAME))
            {
                importedNetwork = await NeuralNetwork.Import(inputStream);
            }

        public static async Task<NeuralNetwork> Import(Stream inputStream)
        {
            JsonSerializerOptions options = new() 
            {
                PropertyNameCaseInsensitive = true
            };

            return await JsonSerializer.DeserializeAsync<NeuralNetwork>(
                inputStream, options);
        }

        */
        private void InitNeuralNetwork()
        {
            //NeuralNetwork = NeuralNetwork.Import(File.OpenRead("placeholder")).Result; 
        }

        private readonly StrokeCollection _strokes;

        public StrokeCollection Drawing
        {
            get
            {
                return _strokes;
            }
        }

        /// <summary>
        /// Get A matrix from a bitmap image .
        /// </summary>
        /// <param name="image">The bitmap image to convert .</param>
        /// <param name="pixels">An array of pixels to store grayscale value.</param>
        /// <returns>Returns the two dimentional pixel array of grayscale values</returns>
        public double[,] ConvertToMatrix(Bitmap image)
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

            return pixels;
        }
        /// <summary>
        /// convert A inkcanvas control element to BitmapSource 
        /// </summary>
        /// <param name="inkCanvasElement"> the inkcanvas element to be rendered as bitmap source</param>
        /// <returns> BitmapSource type </returns>
        public BitmapSource ConvertToBitmapSource(UIElement inkCanvasElement)
        {
            RenderTargetBitmap target = new RenderTargetBitmap((int)inkCanvasElement.RenderSize.Width, (int)inkCanvasElement.RenderSize.Width, 96, 96, PixelFormats.Pbgra32);
            VisualBrush brush = new VisualBrush(inkCanvasElement);
            DrawingVisual visual = new DrawingVisual();
            DrawingContext drawingContext = visual.RenderOpen();
            
            drawingContext.DrawRectangle(brush, null, new Rect(new Point(0, 0),
                new Point((int)inkCanvasElement.RenderSize.Width, (int)inkCanvasElement.RenderSize.Width)));
            drawingContext.Close();
            target.Render(visual);
            
            return target;
        }
        /// <summary>
        /// Convert A bitmapSource to Bitmap type
        /// </summary>
        /// <param name="bitmapSource">the bitmap source to be copied as bitmap</param>
        /// <returns></returns>
        public Bitmap ConvertToBitmap(BitmapSource bitmapSource)
        {
            Bitmap bmp = new Bitmap(
              bitmapSource.PixelWidth,
              bitmapSource.PixelHeight,
              PixelFormat.Format32bppPArgb);

            // Lock the bits so we can change the bitmap programmatically. 
            BitmapData data = bmp.LockBits(
              new Rectangle(new System.Drawing.Point(0, 0), bmp.Size),
              ImageLockMode.WriteOnly,
              PixelFormat.Format32bppPArgb);

            bitmapSource.CopyPixels(
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);
            
            bmp.UnlockBits(data);

            return bmp;
        }
        /// <summary>
        /// Converts a bitmap to ImageSource with specific widthPixel and specific heighpixel
        /// </summary>
        /// <param name="bmp">The bitmap to be converted</param>
        /// <param name="width">The new widthPixels to get </param>
        /// <param name="heigh">The new heighPixels to get </param>
        /// <returns></returns>
        public ImageSource ConvertToImageSource(Bitmap bmp, int width, int heigh)
        {
            IntPtr handle = bmp.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(width, heigh));
        }
        /// <summary>
        /// Converts ImageSource to bitmap
        /// </summary>
        /// <param name="img">The image source to be converted to bitmap </param>
        /// <returns></returns>
        public Bitmap ImageSourceToBitmap(ImageSource img)
        {
            DataObject dataObject = new DataObject(DataFormats.Bitmap, img, true);
            Bitmap bmp = dataObject.GetData("System.Drawing.Bitmap") as Bitmap;
            return bmp;
        }
    }
}