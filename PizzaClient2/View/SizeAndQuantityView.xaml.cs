using System.Windows;

namespace PizzaClient2.View
{
    /// <summary>
    /// Description for SizeAndQuantityView.
    /// </summary>
    public partial class SizeAndQuantityView : Window
    {
        /// <summary>
        /// Initializes a new instance of the SizeAndQuantityView class.
        /// </summary>
        public SizeAndQuantityView()
        {
            InitializeComponent();
        }

        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}