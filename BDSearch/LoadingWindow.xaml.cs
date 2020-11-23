using Common;
using DBService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// LoadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();

            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            if (!Util.VerifyUser(ConfigurationManager.AppSettings["IsLogin"]))
            {
                //  非法软件操作
                HandyControl.Controls.MessageBox.Error("非法软件操作！！！");
                this.Close();
                return;
            }

            Util.CreateKeyFile();

            new Thread(new ThreadStart(()=>
            {
                DbInit init = new DbInit();
                init.initTable();


                Thread.Sleep(2000);

                this.Dispatcher?.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                {
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();

                }));
            }))
            { IsBackground = true }.Start();
        }
    }
}
