﻿using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CAP_Tools.Pages
{
    /// <summary>
    /// Interaction logic for Download.xaml
    /// </summary>
    public partial class Download : UserControl
    {
        public Download()
        {
            InitializeComponent();
        }

        private void ModernButton_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://pan.baidu.com/s/1hYtNW-0blxtdLdSoDDKeuQ");
            try
            {
                Clipboard.SetText("7t1i");
                return;
            }
            catch { }
            System.Threading.Thread.Sleep(10);
            ModernDialog.ShowMessage("密码已复制到剪切板", "提示", MessageBoxButton.OK);
        }

        private void ModernButton_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void ModernButton_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void ModernButton_Click_4(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://pan.baidu.com/s/18G9oj8SLNiXd3Kxj8SE50w");
            try
            {
                Clipboard.SetText("51py");
                return;
            }
            catch { }
            System.Threading.Thread.Sleep(10);
            ModernDialog.ShowMessage("密码已复制到剪切板", "提示", MessageBoxButton.OK);
        }
    }
}
