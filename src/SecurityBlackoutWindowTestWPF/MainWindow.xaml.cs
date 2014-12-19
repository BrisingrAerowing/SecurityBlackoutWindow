using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;

namespace SecurityBlackoutWindowTestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnShowDialog_Click(object sender, RoutedEventArgs e)
        {
            var hWnd = (bool)chkUseFullDesktop.IsChecked ? IntPtr.Zero : new WindowInteropHelper(this).Handle;
            SecurityBlackoutWindow.BlackoutWindow.Blackout(hWnd, () =>
            {
                return MessageBox.Show("This is a Test", "This is a Test", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        
    }
}
