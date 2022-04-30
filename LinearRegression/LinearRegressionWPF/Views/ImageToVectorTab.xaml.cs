﻿using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Windows.Media.Brushes;
using LinearRegressionBackend.DataProvider;
using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Views
{
    /// <summary>
    /// Interaction logic for ImageToVectorTab.xaml
    /// </summary>
    public partial class ImageToVectorTab
    {

        //initialize the dataconverter object 
        DataConverter dataConverter = new DataConverter();
        ImageToVectorViewModel imageToVectorViewModel = new ImageToVectorViewModel(); 
        public ImageToVectorTab()
        {
            InitializeComponent();

            DataContext = new ImageToVectorViewModel();
            // set drawing color to black 
            MyCanvas.DefaultDrawingAttributes.Color = Colors.Black;
            // activate the ue of customized cursors 
            MyCanvas.UseCustomCursor = true;
            // border indicating the active tool of drawing 
            brushbrd.BorderBrush = Brushes.Blue;
            //initialize the cursor of the inkcanvas to pen cursor
            MyCanvas.Cursor = Cursors.Pen;

           
        }



      

    
        private void BrushBtnClick(object sender, MouseButtonEventArgs e)
        {
            /* logic for switching between the button and the eraser */
            brushbrd.BorderBrush = Brushes.Blue;
            MyCanvas.EditingMode = InkCanvasEditingMode.Ink;
            MyCanvas.Cursor = Cursors.Pen;
            erasebrd.BorderBrush = Brushes.Gray;
        }

        private void EreaseBtnClick(object sender, MouseButtonEventArgs e)
        {
            /* logic for switching between the button and the eraser */
            erasebrd.BorderBrush = Brushes.Blue;
            MyCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            MyCanvas.Cursor = System.Windows.Input.Cursors.Cross;
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
  

        /// <summary>
        /// print the matrix on the interface an ivestigate visualy 
        /// </summary>
        /// <param name="src"> get Image source to show the matrix in the interface </param>
        public void DrawMatrix(ImageSource src)
        {
            try
            {
                Bitmap copy = imageToVectorViewModel.ImageSourceToBitmap(src);
                double[,] matrix = imageToVectorViewModel.ConvertToMatrix(imageToVectorViewModel.ImageSourceToBitmap(src));
                DataConverter.imgPixelsMatrix = matrix;
                for (int i = 0; i < copy.Width; i++)
                {
                    this.txtMatrix.Text = this.txtMatrix.Text + Environment.NewLine;
                    for (int j = 0; j < copy.Height; j++)
                    {
                        this.txtMatrix.Text = this.txtMatrix.Text + matrix[i, j].ToString("0.00") + "|  ";
                    }
                    this.txtMatrix.Text = this.txtMatrix.Text + Environment.NewLine + "_________________________________";

                }
            }
            catch (Exception ex)
            {
                txtMatrix.Text = ex.ToString();
            }
        }

        /// <summary>
        /// Get pixel numerical values from an Image converted to bitmap then converted to image source  
        /// </summary>
        /// <param name="width"> width in the unit pixels of the image </param>
        /// <param name="heigh"> width in the unit of pixels of image</param>
        public void GetMatrix(int width, int heigh)
        {
            this.txtMatrix.Text = "";
            BitmapSource img =imageToVectorViewModel.ConvertToBitmapSource(MyCanvas);
            ImageSource img_src = imageToVectorViewModel.ConvertToImageSource((imageToVectorViewModel.ConvertToBitmap(img)), width, heigh);
            imgCanvas.Source = img_src;
            DrawMatrix(img_src);
        }

        private void SlideVlaueChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /*Change the brush size from the slider value */
            MyCanvas.DefaultDrawingAttributes.Width = sizeSlide.Value;
            MyCanvas.DefaultDrawingAttributes.Height = sizeSlide.Value;
       
        }
    }
}
