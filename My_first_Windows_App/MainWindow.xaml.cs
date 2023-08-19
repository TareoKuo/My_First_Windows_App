using System;
using System.Management;
using System.Windows;
using System.Diagnostics;

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
            DisplayHardwareInfo();
        }
        private void DisplayHardwareInfo()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    string processorName = queryObj["Name"].ToString();
                    string systemName = queryObj["SystemName"].ToString();
                    TextBox1.Text = processorName;
                    TextBox2.Text = systemName;
                }

                foreach (ManagementObject queryObj in searcher2.Get())
                {
                    string uuid = queryObj["UUID"].ToString();
                    TextBox3.Text = uuid;
                    break;
                }
            }
            catch (ManagementException ex)
            {
                TextBox1.Text = "ERROR";
                TextBox2.Text = "ERROR";
                TextBox3.Text = "ERROR";
                MessageBox.Show("An error occurred while querying for WMI data: " + ex.Message);
            }
        }

        private void Menu_Github_click()
        {
            string websiteUrl = "https://github.com/TareoKuo/My_first_Windows_App/tree/master";
            try
            {
                // 這是一個用於配置 Process 的初始化設定的新 ProcessStartInfo 物件
                // UseShellExecute 是否使用操作系統的外殼來啟動程序
                ProcessStartInfo startInfo = new ProcessStartInfo { FileName = websiteUrl, UseShellExecute = true };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法打開網頁：" + ex.Message);
            }
        }

        private void Menu_Refresh_click(object sender, RoutedEventArgs e)
        {
            DisplayHardwareInfo();
            MessageBox.Show("Refresh finished");
        }
        private void Menu_Exit_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Menu_Github_click(object sender, RoutedEventArgs e)
        {
            Menu_Github_click();
        }
    }
}

