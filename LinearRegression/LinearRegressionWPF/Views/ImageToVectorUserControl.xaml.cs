using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
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
            MyCanvas.Cursor = Cursors.Pen;
        }
        

        /// <summary>
        /// 
        /// </summary>

        /*
        public UsingCommandsSample()
        {
            InitializeComponent();
        }*/
        
        /*
        private void PredictButtonPress_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PredictButtonPress_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("The PredictButtonPress command was invoked");
        }

        private void ClearButtonPress_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ClearButtonPress_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("The ClearButtonPress command was invoked");
        }*/
        //////////////////


            
        private void BrushBtnClick(object sender, MouseButtonEventArgs e)
        {
            
            MyCanvas.EditingMode = InkCanvasEditingMode.Ink;
            MyCanvas.Cursor = Cursors.Pen;
        }

        private void ClearBtnClick(object sender, RoutedEventArgs e)
        {
            MyCanvas.Strokes.Clear();
        }

        private void EreaseBtnClick(object sender, MouseButtonEventArgs e)
        {
            MyCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            MyCanvas.Cursor = Cursors.Cross;
        }


        /*
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

                value1.Text = matrix[0, 0].ToString("0.00");
                value2.Text = matrix[1, 0].ToString("0.00");
                value3.Text = matrix[2, 0].ToString("0.00");
                value4.Text = matrix[3, 0].ToString("0.00");

                value5.Text = matrix[0, 1].ToString("0.00");
                value6.Text = matrix[1, 1].ToString("0.00");
                value7.Text = matrix[2, 1].ToString("0.00");
                value8.Text = matrix[3, 1].ToString("0.00");

                value9.Text = matrix[0, 2].ToString("0.00");
                value10.Text = matrix[1, 2].ToString("0.00");
                value11.Text = matrix[2, 2].ToString("0.00");
                value12.Text = matrix[3, 2].ToString("0.00");

                value13.Text = matrix[0, 3].ToString("0.00");
                value14.Text = matrix[1, 3].ToString("0.00");
                value15.Text = matrix[2, 3].ToString("0.00");
                value16.Text = matrix[3, 3].ToString("0.00");
            }
            catch (Exception ex)
            {
                txtMatrix.Text = ex.ToString();
            }
        }
        */

        /*
        public void GetMatrix(int width, int heigh)
        {
            txtMatrix.Text = "";
            BitmapSource img = imageToVectorViewModel.ConvertToBitmapSource(MyCanvas);
            ImageSource img_src = imageToVectorViewModel.ConvertToImageSource((imageToVectorViewModel.ConvertToBitmap(img)), width, heigh);
            imgCanvas.Source = img_src;
            DrawMatrix(img_src);
        }*/

        private void SlideVlaueChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           MyCanvas.DefaultDrawingAttributes.Width = sizeSlide.Value;
           MyCanvas.DefaultDrawingAttributes.Height = sizeSlide.Value;
        }

    }
}
