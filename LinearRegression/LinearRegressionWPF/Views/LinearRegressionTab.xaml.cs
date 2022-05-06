using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Views
{
    /// <summary>
    /// Interaction logic for LinearRegressionTab.xaml
    /// </summary>
    public partial class LinearRegressionTab : UserControl
    {
        public LinearRegressionTab()
        {
            InitializeComponent();
        }


        

       

        private void TextBoxEnterUpdate(object sender, KeyEventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                TextBox tBox = (TextBox)sender;
                BindingExpression binding = BindingOperations.GetBindingExpression(tBox, TextBox.TextProperty);

                if (binding != null)
                {
                    binding.UpdateSource();
                    Keyboard.ClearFocus();
                }
            }
        }
    }
}
