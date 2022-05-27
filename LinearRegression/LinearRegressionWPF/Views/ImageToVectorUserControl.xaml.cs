using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using LinearRegressionWPF.ViewModels;

using Brushes = System.Windows.Media.Brushes;

namespace LinearRegressionWPF.Views
{
    /// <summary>
    /// Interaction logic for ImageToVectorUserControl.xaml
    /// </summary>
    public partial class ImageToVectorUserControl : UserControl
    {
        ImageToVectorViewModel imageToVectorViewModel = new ImageToVectorViewModel();

        public ImageToVectorUserControl()
        {
            InitializeComponent();

            DataContext = new ImageToVectorViewModel();
            MyCanvas.DefaultDrawingAttributes.Color = Colors.Black;
            MyCanvas.UseCustomCursor = true;
            brushbrd.BorderBrush = Brushes.Blue;
            MyCanvas.Cursor = Cursors.Pen;
        }

        private void BrushBtnClick(object sender, MouseButtonEventArgs e)
        {
            brushbrd.BorderBrush = Brushes.Blue;
            MyCanvas.EditingMode = InkCanvasEditingMode.Ink;
            MyCanvas.Cursor = Cursors.Pen;
            erasebrd.BorderBrush = Brushes.Gray;
        }

        private void EreaseBtnClick(object sender, MouseButtonEventArgs e)
        {
            erasebrd.BorderBrush = Brushes.Blue;
            MyCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            MyCanvas.Cursor = Cursors.Cross;
            brushbrd.BorderBrush = Brushes.Gray;
        }

        private void ClearBtnClick(object sender, RoutedEventArgs e)
        {
            MyCanvas.Strokes.Clear();
        }

        private void FourByFourBtnClick(object sender, RoutedEventArgs e)
        {
            GetMatrix(4, 4);
        }

        public void DrawMatrix(ImageSource src)
        {
            try
            {
                Bitmap copy = imageToVectorViewModel.ImageSourceToBitmap(src);
                double[,] matrix = imageToVectorViewModel.ConvertToMatrix(imageToVectorViewModel.ImageSourceToBitmap(src));

                for (int i = 0; i < copy.Width; i++)
                {
                    txtMatrix.Text += Environment.NewLine;
                    for (int j = 0; j < copy.Height; j++)
                    {
                        txtMatrix.Text = txtMatrix.Text + matrix[j, i].ToString("0.00") + "|  ";
                    }

                    txtMatrix.Text = txtMatrix.Text + Environment.NewLine + "_________________________________";
                }
            }
            catch (Exception ex)
            {
                txtMatrix.Text = ex.ToString();
            }
        }

        public void GetMatrix(int width, int heigh)
        {
            txtMatrix.Text = "";
            BitmapSource img = imageToVectorViewModel.ConvertToBitmapSource(MyCanvas);
            ImageSource img_src = imageToVectorViewModel.ConvertToImageSource((imageToVectorViewModel.ConvertToBitmap(img)), width, heigh);
            imgCanvas.Source = img_src;
            DrawMatrix(img_src);
        }

        private void SlideVlaueChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MyCanvas.DefaultDrawingAttributes.Width = sizeSlide.Value;
            MyCanvas.DefaultDrawingAttributes.Height = sizeSlide.Value;
        }
    }
}
