﻿
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Point = System.Windows.Point;
namespace LinearRegressionWPF.ViewModels
{
    class ImageToVectorViewModel
    {
      public ImageToVectorViewModel()
        {
           
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
                for (int j = 0; j < image.Height; j++)
                {
                    Color cl = image.GetPixel(i, j);
                    int rl = cl.R;
                    int gl = cl.G;
                    int b1 = cl.B;
                    float gray = (float)(.299 * rl + .587 * gl + .114 * b1);
                    pixels[i, j] = (gray == 255) ? 0 : 1-(gray / 256);
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
            var target = new RenderTargetBitmap((int)inkCanvasElement.RenderSize.Width, (int)inkCanvasElement.RenderSize.Width, 96, 96, PixelFormats.Pbgra32);
            var brush = new VisualBrush(inkCanvasElement);
            var visual = new DrawingVisual();
            var drawingContext = visual.RenderOpen();
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
            //lock the bits so we can change the bitmap programmaticlly 
            BitmapData data = bmp.LockBits(
              new Rectangle(new System.Drawing.Point(0, 0), bmp.Size),
              ImageLockMode.WriteOnly,
              PixelFormat.Format32bppPArgb);
            bitmapSource.CopyPixels(
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);
            // unlock the bits 
            bmp.UnlockBits(data);
            return bmp;
        }
        /// <summary>
        /// convert a bitmap to ImageSource with specific widthPixel and specific heighpixel
        /// </summary>
        /// <param name="bmp">The bitmap to be converted</param>
        /// <param name="width"> the new widthPixels to get </param>
        /// <param name="heigh"> the new heighPixels to get </param>
        /// <returns></returns>
        public ImageSource ConvertToImageSource(Bitmap bmp, int width, int heigh)
        {
            var handle = bmp.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(width, heigh));
        }
        /// <summary>
        /// Convert ImageSource to bitmap
        /// </summary>
        /// <param name="img">the image source to be converted to bitmap </param>
        /// <returns></returns>
        public Bitmap ImageSourceToBitmap(ImageSource img)
        {
            var d = new DataObject(DataFormats.Bitmap, img, true);
            var bmp = d.GetData("System.Drawing.Bitmap") as System.Drawing.Bitmap;
            return bmp;
        }
    }
}