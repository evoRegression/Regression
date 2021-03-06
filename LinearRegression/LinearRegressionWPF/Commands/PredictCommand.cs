using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using LinearRegressionBackend.DataProvider;
using LinearRegressionWPF.ViewModels;
using MathNet.Numerics.LinearAlgebra;

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
           //It should only be set to true, if a neural network has been succesfully loaded.
            return _viewModel.IsNeuralNetworkLoaded;
        }

        public void Execute(object parameter)
        {
            var imageProcessor = new ImageProcess();

            Bitmap image = CreateImageFromStrokes(_viewModel.Drawing);
            var scaledImage = imageProcessor.Scale(image, 28, 28);
            //just for debugging
            //scaledImage.Save(@"c:\Resources\scled.bmp", ImageFormat.Bmp);
            var inputVectorFromImage = imageProcessor.GrayScale(scaledImage);

            _viewModel.Result = _viewModel.NeuralNetwork.Propagate(inputVectorFromImage).Output();
            StringBuilder resultText = new StringBuilder();
            if (IsCircle(_viewModel.Result))
            {
                resultText.AppendLine("Circle");
            }
            else
            {
                resultText.AppendLine("Triangle");
            }
            resultText.AppendLine(String.Format("{0:0.000000000}", _viewModel.Result[0]) + " " +
                String.Format("{0:0.000000000}", _viewModel.Result[1]));
            _viewModel.FormateStringResult(resultText.ToString());
        }

        private bool IsCircle(Vector<double> input)
        {
            return input[0] > input[1];
        }

        private static Bitmap CreateImageFromStrokes(StrokeCollection drawing)
        {
            RenderTargetBitmap bmp = CreateRenderTargetBitmapFromStrokeCollection(drawing);

            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(stream);
            Bitmap resultBitmap = new Bitmap(stream);
            stream.Close();
            //stream.Dispose();

            return resultBitmap;
        }

        private static RenderTargetBitmap CreateRenderTargetBitmapFromStrokeCollection(StrokeCollection drawing)
        {
            InkCanvas InkyStinky = AddStrokesToCanvas(drawing);

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(400, 400, 96, 96, PixelFormats.Default);
            CreateWhiteBackgroud(renderTargetBitmap);

            renderTargetBitmap.Render(InkyStinky);
            return renderTargetBitmap;
        }

        private static InkCanvas AddStrokesToCanvas(StrokeCollection drawing)
        {
            InkCanvas InkyStinky = new InkCanvas();
            InkyStinky.RenderSize = new System.Windows.Size(400, 400);
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
                    new System.Windows.Rect(0, 0, 400, 400));
            }
            bmp.Render(visual);
        }
    }
}
