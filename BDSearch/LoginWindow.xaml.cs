using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace BDSearch
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        Configuration Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userName = this.tbUserName.Text.Trim();
            var pwd = this.tbPassword.Text.Trim();
            var mcCode = this.tbMachineCode.Text;

            if (string.IsNullOrWhiteSpace(userName))
            {
                this.tbUserName.IsError = true;
                this.tbUserName.ErrorStr = "请输入用户名";
                return;
            }

            if (string.IsNullOrWhiteSpace(pwd))
            {
                this.tbPassword.IsError = true;
                this.tbPassword.ErrorStr = "请输入密码";
                return;
            }

            Task.Factory.StartNew(() =>
            {
                var response = HttpUtil.Login(userName, pwd, mcCode);

                Application.Current.Dispatcher?.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                {
                    if (response?.code == "1")
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();

                        // 写配置
                        Config.AppSettings.Settings["IsLogin"].Value = "1";
                        Config.Save();
                        ConfigurationManager.RefreshSection("appSettings");

                        AppData.Token = response.token;
                        AppData.UserName = userName;
                        AppData.Password = Util.GetMd5Hash(pwd);
                        AppData.Total = response.total;

                        this.Close();
                    }
                    else
                    {
                        this.tbLoginError.Text = response?.msg;
                        this.tbLoginError.Visibility = Visibility.Visible;
                    }
                }));
            });

        }

        /// <summary>
        /// 生成机器码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.tbMachineCode.Text = Util.GetMacAddress();

        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
