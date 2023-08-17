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

namespace My_first_Windows_App
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
        private int textcount = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (textcount == 0)
            {
                textcount = 1;
                textBlock1.Text = "You successfully clicked the button";
            } else
            {
                textBlock1.Text = "You can try to click button";
                textcount = 0;
            }
        }
    }
}
