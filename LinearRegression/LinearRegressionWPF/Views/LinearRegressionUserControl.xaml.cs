using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LinearRegressionWPF.Views
{
    /// <summary>
    /// Interaction logic for LinearRegressionUserControl.xaml
    /// </summary>
    public partial class LinearRegressionUserControl : UserControl
    {
        public LinearRegressionUserControl()
        {
            InitializeComponent();
        }

        private void TextBoxEnterUpdate(object sender, KeyEventArgs eventArgs)
        {
            if (sender == null)
            {
                return;
            }

            if (eventArgs.Key is Key.Enter or Key.Return)
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
