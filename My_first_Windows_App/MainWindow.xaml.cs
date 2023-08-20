using System;
using System.Management;
using System.Windows;
using System.Diagnostics;
using System.Windows.Forms;

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
            DisplayMonitorInfo();
            DisplayRamInfo();
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
                    string processorID = queryObj["ProcessorID"].ToString();
                    string processorID_0x = "0x" + processorID.Substring(9);
                    string systemName = queryObj["SystemName"].ToString();
                    TextBox1.Text = processorName;
                    TextBox2.Text = systemName;
                    TextBox4.Text = processorID_0x;
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
                TextBox4.Text = "ERROR";
                MessageBox.Show("An error occurred while querying for WMI data: " + ex.Message);
            }
        }

        private void DisplayMonitorInfo()
        {
            ManagementObjectSearcher searcher3 = new ManagementObjectSearcher("SELECT * FROM Win32_DesktopMonitor");
            try
            {
                foreach (ManagementObject queryObj in searcher3.Get())
                {
                    string monitorName = queryObj["Name"].ToString();
                    int screenWidth = Convert.ToInt32(queryObj["ScreenWidth"]);
                    int screenHeight = Convert.ToInt32(queryObj["ScreenHeight"]);
                    string DeviceID = queryObj["DeviceID"].ToString();
                    TextBox5.Text = $"{monitorName} {DeviceID}";
                    TextBox6.Text = $"{screenWidth}x{screenHeight}";
                }
            }
            catch (ManagementException ex)
            {
                TextBox5.Text = "ERROR";
                TextBox6.Text = "ERROR";
                MessageBox.Show("An error occurred while querying for WMI data: " + ex.Message);
            }
        }

        private void DisplayRamInfo()
        {
            ManagementObjectSearcher searcher4 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
            ManagementObjectSearcher searcher5 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");
            try
            {
                foreach (ManagementObject queryObj in searcher4.Get())
                {
                    int MemoryType = Convert.ToInt16(queryObj["MemoryType"]);
                    switch (MemoryType)
                    {
                        case 1:
                            TextBox7.Text = "Other";
                            break;
                        case 20:
                            TextBox7.Text = "DDR";
                            break;
                        case 21:
                        case 22:
                            TextBox7.Text = "DDR2";
                            break;
                        case 24:
                            TextBox7.Text = "DDR3";
                            break;
                        case 26:
                            TextBox7.Text = "DDR4";
                            break;
                        default:
                            TextBox7.Text = "Unknown(RAM)";
                            break;
                    }
                    string deviceLocator = queryObj["DeviceLocator"].ToString();
                    string partNumber = queryObj["PartNumber"].ToString();
                    string speed = queryObj["Speed"].ToString();
                    //TextBox7.Text = MemoryType;
                    TextBox8.Text = deviceLocator;
                    TextBox9.Text = partNumber;
                    TextBox10.Text = speed;
                }
            }
            catch (ManagementException ex)
            {
                MessageBox.Show("An error occurred while querying for WMI data: " + ex.Message);
            }

            try
            {
                foreach (ManagementObject queryObj in searcher5.Get())
                {
                    ulong totalRam = Convert.ToUInt64(queryObj["TotalPhysicalMemory"]);
                    double totalRamGB = totalRam / (1024 * 1024 * 1024.0);

                    TextBox11.Text = $"{totalRamGB:F2} GB";
                }
            }
            catch (ManagementException ex)
            {
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

