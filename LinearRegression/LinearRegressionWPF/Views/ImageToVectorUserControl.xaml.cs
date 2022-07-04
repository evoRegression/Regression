using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LinearRegressionWPF.Views
{
    public partial class ImageToVectorUserControl : UserControl
    {
        public ImageToVectorUserControl()
        {
            InitializeComponent();            
            MyCanvas.DefaultDrawingAttributes.Color = Colors.Black;
            MyCanvas.UseCustomCursor = true;
            MyCanvas.Cursor = Cursors.Pen;
        }
            
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


        private void SlideVlaueChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           MyCanvas.DefaultDrawingAttributes.Width = sizeSlide.Value;
           MyCanvas.DefaultDrawingAttributes.Height = sizeSlide.Value;
        }
    }
}
