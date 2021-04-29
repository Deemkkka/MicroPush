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
using Xceed.Wpf.Toolkit;

namespace MicroPush
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

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            secondWindow sW = new secondWindow();
            sW.Show();
            this.Close();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveWindow seW = new SaveWindow();
            seW.Show();
            this.Close();
        }
        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            optionsWindow oW = new optionsWindow();
            oW.Show();
            this.Close();
        }
        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            // Close this window
            this.Close();
        }

    }
}
