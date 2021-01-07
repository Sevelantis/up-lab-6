using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace up_lab_6
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ESC = "\x1b";
        private Dictionary<string, string> FontStyles = new Dictionary<string, string> 
            {
                {"default", "0"},
                {"italic", "1" },
                { "outline", "32" },
                {"shadowed", "128" }
            };
        private List<string> FontSizes = new List<string>() { "8", "12", "16", "24", "32" };

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            FontStyleComboBox.ItemsSource = FontStyles;
            FontStyleComboBox.SelectedIndex = 0;

            FontSizeComboBox.ItemsSource = FontSizes;
            FontSizeComboBox.SelectedIndex = 0;

            FontWeightSlider.Minimum = -7;
            FontWeightSlider.Maximum = 7;
            FontWeightSlider.Value = 0;
            FontWeightSlider.TickFrequency = 1;
            FontWeightSlider.IsSnapToTickEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = ESC + CommandBox.Text;
            string fullPath = @"C:\Users\szyminson\source\repos\up-lab-6\up-lab-6\bin\Debug\gpcl6win64.exe";
            string pclFile = "out.pcl";
            File.WriteAllText(pclFile, text);
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Path.GetFileName(fullPath);
            psi.WorkingDirectory = Path.GetDirectoryName(fullPath);
            psi.Arguments = "-f " + pclFile;
            Process.Start(psi);
        }

        private void NewLineButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBox.Text += "\x1b\x0d\x0a";
        }

        private void FontStyleButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBox.Text += "\x1b(s" + FontStyleComboBox.SelectedValue + "S";
        }

        private void FontSizeButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBox.Text += "\x1b(s" + FontSizeComboBox.SelectedItem + "H";
        }

        private void NewPageButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBox.Text += "\x1b\x0d\x0c";
        }

        private void FontWeightButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBox.Text += "\x1b(s" + FontWeightSlider.Value + "B";
        }

        private void EnableUnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBox.Text += "\x1b&d0D";
        }

        private void DisableUnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBox.Text += "\x1b&d@";
        }

        private void Rotate90Button_Click(object sender, RoutedEventArgs e)
        {
            CommandBox.Text += "\x1b&a90P\x1b\x0d\x0c";
        }

        private void Rotate0Button_Click(object sender, RoutedEventArgs e)
        {
            CommandBox.Text += "\x1b&a0P\x1b\x0d\x0c";
        }

        private void DrawRectangleButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBox.Text += "\x1b*c600a6b0P";
            CommandBox.Text += "\x1b*c6a600b0P";

            CommandBox.Text += "\x1b*p0x640Y";
            CommandBox.Text += "\x1b*c606a6b0P";

            CommandBox.Text += "\x1b*p600x40Y";

            CommandBox.Text += "\x1b*c6a600b0P";
        }

        private void DrawCircleButton_Click(object sender, RoutedEventArgs e)
        {
            CommandBox.Text += "\x1bE\x1b\x0bIN;SP1PA10,10;PD2500,10,10,1500,10,10;";//SP1;PA600,600;CI200;\x1b\x0a\x1bE";
        }
    }
}
