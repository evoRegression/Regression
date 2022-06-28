using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using LinearRegressionBackend.DataProvider;
using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands
{
    internal class PredictCommand : ICommand
    {
        private ImageToVectorViewModel _viewModel;

        public PredictCommand(ImageToVectorViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //TODO - create image from strokes
            var imageProcessor = new ImageProcess();

            Bitmap image = CreateImageFromStrokes(_viewModel.Drawing);
            var scaledImage = imageProcessor.Scale(image, 4, 4);

            // for debugging
            scaledImage.Save(@"c:\temp2\scaled.bmp", ImageFormat.Bmp);
            
            var inputVectorFromImage = imageProcessor.GrayScale(scaledImage);

            //TODO - predict with neural network
            _viewModel.StringResult = "New String";
            //_viewModel.Result = _viewModel.NeuralNetwork.Propagate(inputVectorFromImage).Output();
            //NotifyPropertyChanged
        }

        private static Bitmap CreateImageFromStrokes(StrokeCollection drawing)
        {
            RenderTargetBitmap bmp = CreateRenderTargetBitmapFromStrokeCollection(drawing);

            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(stream);
            Bitmap resultBitmap = new Bitmap(stream);
            // for debugging purposes
            resultBitmap.Save(@"c:\temp2\b.bmp", ImageFormat.Bmp);
            stream.Close();
            //stream.Dispose();

            return resultBitmap;
        }

        private static RenderTargetBitmap CreateRenderTargetBitmapFromStrokeCollection(StrokeCollection drawing)
        {
            InkCanvas InkyStinky = AddStrokesToCanvas(drawing);

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(200, 200, 96, 96, PixelFormats.Default);
            CreateWhiteBackgroud(renderTargetBitmap);

            renderTargetBitmap.Render(InkyStinky);
            return renderTargetBitmap;
        }

        private static InkCanvas AddStrokesToCanvas(StrokeCollection drawing)
        {
            InkCanvas InkyStinky = new InkCanvas();
            InkyStinky.RenderSize = new System.Windows.Size(200, 200);
            InkyStinky.Strokes.Add(drawing);
            return InkyStinky;
        }

        private static void CreateWhiteBackgroud(RenderTargetBitmap bmp)
        {
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawRectangle(
                    System.Windows.Media.Brushes.White, new System.Windows.Media.Pen(),
                    new System.Windows.Rect(0, 0, 200, 200));
            }
            bmp.Render(visual);
        }
    }
}
