﻿using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Net;
using System.Threading;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CAP_Tools.Pages
{
    /// <summary>
    /// Interaction logic for InstallNX.xaml
    /// </summary>
    public partial class InstallNX : System.Windows.Controls.UserControl
    {
        //下载文件
        private string _saveDir;
        internal class FileMessage
        {
            public string FileName { get; set; }
            public string RelativeUrl { get; set; }
            public string Url { get; set; }
            public bool IsDownLoad { get; set; }
            public string SavePath { get; set; }
        }
        public InstallNX()
        {
            InitializeComponent();
            //在程序所在路径新建文件夹
            //进度条隐藏
            this.Prog.Visibility = Visibility.Hidden;
            //百分比隐藏
            this.label1.Visibility = Visibility.Hidden;
            this.label2.Visibility = Visibility.Hidden;
            //测试按钮隐藏
            this.Test.Visibility = Visibility.Hidden;
            _saveDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NX License Servers");
        }

        private static RegistryKey NXregistry()
        {
            return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
        }

        public object DialogResult { get; private set; }

        private void XZ_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            this.XZRoute.Text = m_Dir;
            DirectoryInfo dir = new DirectoryInfo(m_Dir);
            foreach (FileInfo file in dir.GetFiles("UGII.cab", SearchOption.AllDirectories))//在文件夹中搜索UGII.cab；
            {
                ///截取路径添加到变量
                string NXRoute = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(file.FullName)));
                this.NXRoute.Text = NXRoute;
                break;
            }
            DirectoryInfo dir2 = new DirectoryInfo(m_Dir);
            foreach (FileInfo file in dir.GetFiles("jt_catiav5.exe", SearchOption.AllDirectories))//在文件夹中搜索jt_catiav5.exe；
            {
                ///截取路径添加到变量
                string PJNXRoute = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName((System.IO.Path.GetDirectoryName(file.FullName)))));
                this.PJRoute.Text = PJNXRoute;
                break;
            }
            ///判断安装哪个版本
            string route = NXRoute.Text;
            string nx10 = route + "\\nx100";
            string nx11 = route + "\\nx110";
            string nx12 = route + "\\nx120";
            string nx122 = route + "\\nx1202";
            string nx = route + "\\nx";

            if (Directory.Exists(nx10))
            {
                this.a.Text = "- 正在安装 NX10.0";
                this.Version.Text = "NX10";
                this.NXInstall.IsEnabled = true;
                this.NXCrack.IsEnabled = true;
            }
            else
            {
                if (Directory.Exists(nx11))
                {
                    this.a.Text = "- 正在安装 NX11.0";
                    this.Version.Text = "NX11";
                    this.NXInstall.IsEnabled = true;
                }
                else
                {
                    if (Directory.Exists(nx12))
                    {
                        this.a.Text = "- 正在安装 NX12.0";
                        this.Version.Text = "NX12";
                        this.NXInstall.IsEnabled = true;
                    }
                    else
                    {
                        if (Directory.Exists(nx122))
                        {
                            
                            this.a.Text = "- 正在安装 NX12.0.2";
                            this.Version.Text = "NX12";
                            this.NXInstall.IsEnabled = true;
                        }
                        else
                        {
                            if (Directory.Exists(nx))
                            {
                                this.a.Text = "- 正在安装 NX(2019)";
                                this.Version.Text = "NX(2019)";
                                this.NXInstall.IsEnabled = true;
                            }
                            else
                            {
                                this.NXInstall.IsEnabled = false;
                                this.NXCrack.IsEnabled = false;
                                ModernDialog.ShowMessage("NX安装主程序不存在，请重新选择文件夹或检测安装程序完整性！\n\r或者选择的安装包为NX10.0以下版本", "警告", MessageBoxButton.OK);
                            }
                        }
                    }
                }
            }
            ///判断破解文件是否存在
            string pjfile = PJRoute.Text;
            if (NXInstall.IsEnabled == true)
            {
                if (Directory.Exists(pjfile))
                {
                    this.NXCrack.IsEnabled = true;
                }
                else
                {
                    this.NXCrack.IsEnabled = false;
                    ModernDialog.ShowMessage("未在安装目录找到破解文件，请检查安装包是否包含破解文件。\n\r请将破解文件整个文件夹复制到安装目录后重试！", "警告", MessageBoxButton.OK);
                }
            }
        }

        private void NXInstall_Click(object sender, RoutedEventArgs e)
        {
            string path1 = NXRoute.Text;
            DirectoryInfo dir = new DirectoryInfo(path1);
            foreach (FileInfo file in dir.GetFiles("UGII.cab", SearchOption.AllDirectories))//在文件夹中搜索setup.exe；
            {
                System.Diagnostics.Process.Start((System.IO.Path.GetDirectoryName(file.FullName)) + "\\setup.exe");//运行setup.exe
                break;
            }
        }

        private void NXCrack_Click(object sender, RoutedEventArgs e)
        {
            string version = Version.Text;
            string pjfile = PJRoute.Text;
            MessageBoxResult result = ModernDialog.ShowMessage("确定要破解" + version + "吗？请确保NX软件都己经关闭。", "提示", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (version == "NX10")
                {
                    ///获取NX安装路径
                    RegistryKey driverKey = NXregistry();
                    string EXE = (String)driverKey.GetValue("Unigraphics V28.0");
                    ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                    string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                    ///复制文件
                    Copy(pjfile, Home);
                    ModernDialog.ShowMessage(version + "破解完成，请继续安装许可证！", "提示", MessageBoxButton.OK);
                }
                else
                {
                    if (version == "NX11")
                    {
                        ///获取NX安装路径
                        RegistryKey driverKey = NXregistry();
                        string EXE = (String)driverKey.GetValue("Unigraphics V29.0");
                        ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                        string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                        ///复制文件
                        Copy(pjfile, Home);
                        ModernDialog.ShowMessage(version + "破解完成，请继续安装许可证！", "提示", MessageBoxButton.OK);
                    }
                    else
                    {
                        if (version == "NX12")
                        {
                            ///获取NX安装路径
                            RegistryKey driverKey = NXregistry();
                            string EXE = (String)driverKey.GetValue("Unigraphics V30.0");
                            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                            string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                            ///复制文件
                            Copy(pjfile, Home);
                            ModernDialog.ShowMessage(version + "破解完成，请继续安装许可证！", "提示", MessageBoxButton.OK);
                        }
                        else
                        {
                            if (version == "NX(2019)")
                            {
                                ///获取NX安装路径
                                RegistryKey driverKey = NXregistry();
                                string EXE = (String)driverKey.GetValue("Unigraphics V31.0");
                                ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                                string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                                ///复制文件
                                Copy(pjfile, Home);
                                ModernDialog.ShowMessage(version + "破解完成，请继续安装许可证！", "提示", MessageBoxButton.OK);
                            }
                            else
                            {
                                ModernDialog.ShowMessage("抱歉，您未安装" + version + "！请先安装" + version + "主程序后再进行破解文件", "警告", MessageBoxButton.OK);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 复制文件夹中的所有内容
        /// </summary>
        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {

            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private bool CheckNX1847()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V31.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX12()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V30.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX11()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX10()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V28.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }


        private void NXLicence_Click(object sender, RoutedEventArgs e)
        {
            if (HttpFileExist("https://capful.oss-cn-beijing.aliyuncs.com/NX/NX%20License%20Servers%20v%202.2.1902.exe"))
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\NX License Servers v 2.2.1902.exe"))
                {
                    System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\NX License Servers v 2.2.1902.exe");
                }
                else
                {
                    //许可证不存在是下载许可证
                    //按钮不可用
                    this.NXLicence.IsEnabled = false;
                    //进度条显示
                    this.Prog.Visibility = Visibility.Visible;
                    //进度条百分比显示
                    this.label1.Visibility = Visibility.Visible;
                    this.label2.Visibility = Visibility.Visible;
                    //远程文件路径
                    string imageUrl = "https://capful.oss-cn-beijing.aliyuncs.com/NX/NX%20License%20Servers%20v%202.2.1902.exe";
                    string fileExt = Path.GetExtension(imageUrl);
                    string fileNewName = Guid.NewGuid() + fileExt;
                    bool isDownLoad = false;
                    string filePath = Path.Combine(_saveDir, fileNewName);
                    if (File.Exists(filePath))
                    {
                        isDownLoad = true;
                    }
                    var file = new FileMessage
                    {
                        FileName = fileNewName,
                        RelativeUrl = "NX License Servers.zip",
                        Url = imageUrl,
                        IsDownLoad = isDownLoad,
                        SavePath = filePath
                    };
                    if (!file.IsDownLoad)
                    {
                        string fileDirPath = Path.GetDirectoryName(file.SavePath);
                        if (!Directory.Exists(fileDirPath))
                        {
                            Directory.CreateDirectory(fileDirPath);
                        }
                        try
                        {
                            WebClient client = new WebClient();
                            client.DownloadFileCompleted += client_DownloadFileCompleted;
                            client.DownloadProgressChanged += client_DownloadProgressChanged;
                            client.DownloadFileAsync(new Uri(file.Url), file.SavePath, file.FileName);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            else
            {
                ModernDialog.ShowMessage("网络连接失败，请检查网络！\n\r也可能是下载链接已失效，联系Capful", "警告", MessageBoxButton.OK);
            }
        }

        /// <summary>
        ///  判断远程文件是否存在
        /// </summary>
        /// <param name="fileUrl">文件URL</param>
        /// <returns>存在-true，不存在-false</returns>
        private bool HttpFileExist(string http_file_url)
        {
            WebResponse response = null;
            bool result = false;//下载结果
            try
            {
                response = WebRequest.Create(http_file_url).GetResponse();
                result = response == null ? false : true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState != null)
            {
                //下载完成
                this.label1.Content = "许可证下载完成";
                //重命名文件
                File.Move(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\" + e.UserState.ToString(), AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\NX License Servers v 2.2.1902.exe");
                //解压文件
                //ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\" + e.UserState.ToString(), AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\");
                //删除ZIP文件
                //File.Delete(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\" + e.UserState.ToString());
                //按钮可用
                this.NXLicence.IsEnabled = true;
                //运行许可证文件
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "NX License Servers\\NX License Servers v 2.2.1902.exe");
                //进度条隐藏
                this.Prog.Visibility = Visibility.Hidden;
                //百分比隐藏
                this.label1.Visibility = Visibility.Hidden;
                this.label2.Visibility = Visibility.Hidden;
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Prog.Minimum = 0;
            this.Prog.Maximum = (int)e.TotalBytesToReceive;
            this.Prog.Value = (int)e.BytesReceived;
            this.label1.Content = "许可证下载进度 : " + e.ProgressPercentage + "%"; 
            this.label2.Content = String.Format("{0}M/{1}M", Math.Round((double)e.BytesReceived / 1024 / 1024, 2), Math.Round((double)e.TotalBytesToReceive / 1024 / 1024, 2));
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

