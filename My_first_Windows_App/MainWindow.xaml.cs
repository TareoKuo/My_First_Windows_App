using System;
using System.Management;
using System.Windows;

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
                // 使用 ManagementObjectSearcher 來查詢 Win32_Processor 類的資訊
                // 使用 foreach 遍歷查詢結果中的每個 ManagementObject
                // 從 ManagementObject 中獲取處理器資訊（Name 屬性）
                // 將處理器資訊顯示在 TextBox1 控制項中
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    string processorName = queryObj["Name"].ToString();
                    string systemName = queryObj["SystemName"].ToString();
                    TextBox1.Text = processorName;
                    TextBox2.Text = systemName;
                }
            }
            catch (ManagementException ex)
            {
                // 如果出現 ManagementException 錯誤，將 "ERROR" 文字顯示在 TextBox1 中
                // 顯示一個彈出式視窗來顯示錯誤訊息
                TextBox1.Text = "ERROR";
                TextBox2.Text = "ERROR";
                MessageBox.Show("An error occurred while querying for WMI data: " + ex.Message);
            }
        }
    }
}
