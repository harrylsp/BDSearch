using BusinessService;
using Common;
using Model;
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

namespace BDSearch
{
    /// <summary>
    /// SearchControl.xaml 的交互逻辑
    /// </summary>
    public partial class SearchControl : UserControl
    {
        public SearchControl()
        {
            InitializeComponent();

            //List<TestModel> list = new List<TestModel>
            //{
            //    new TestModel{Index = 1, Name = "lsp" , Selected = true, Type = "1", Remark = "lsp"},
            //    new TestModel{Index = 2, Name = "lsp" , Selected = true, Type = "1", Remark = "lsp"}
            //};

            //for (int i = 0; i < 50; i++)
            //{
            //    list.Add(new TestModel { Index = 2, Name = "lsp", Selected = true, Type = "1", Remark = "lsp" });
            //}

            //this.dataGrid.ItemsSource = list;
            this.tbjf.Text = AppData.Token;
        }

        /// <summary>
        /// SS号查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var list = IsExist(this.tbSs.Text.Trim());
            if (list?.Count > 0)
            {
                this.tbKc.Text = "有";
                this.tbZjk.Text = "1";
                this.dataGrid.ItemsSource = list;
            }
            else
            {
                this.tbKc.Text = "无";
                this.tbZjk.Text = "";
            }
        }

        /// <summary>
        /// 查询对应ss号 链接信息
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        public List<BDModel> IsExist(string ss)
        {
            var where = " and fid='" + this.tbSs.Text.Trim() + "';";
            BDService db = new BDService();
            return db.Query(where);
        }

        /// <summary>
        /// 自建库 获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnZjk_Click(object sender, RoutedEventArgs e)
        {
            var ss = this.tbSs.Text.Trim();

            if (string.IsNullOrWhiteSpace(ss))
            {
                this.tbSs.IsError = true;
                this.tbSs.ErrorStr = "请输入SS号";
                return;
            }

            var localList = IsExist(ss);
            if (localList?.Count > 0)
            {
                this.tbZjk.IsError = true;
                this.tbZjk.ErrorStr = "本地已存在改分享链接";
                BtnSearch_Click(null, null);
                return;
            }

            Task.Factory.StartNew(() =>
            {
                var response = HttpUtil.UpdateSource(AppData.UserName, AppData.Password, ss, AppData.Token);

                if (response?.code == "1")
                {
                    Application.Current.Dispatcher?.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                    {
                        this.tbjf.Text = string.IsNullOrWhiteSpace(response.total) ? "0" : response.total;

                        //
                        var shareLinkInfo = response?.plist;
                        if (shareLinkInfo != null)
                        {
                            BDModel model = new BDModel();
                            model.fno = shareLinkInfo.id;
                            model.fid = shareLinkInfo.ss;
                            model.slink = shareLinkInfo.link.Trim().Split('提')[0];
                            model.scode = shareLinkInfo.link.Trim().Split('提')[1].Split(':')[1].Trim();

                            BDService db = new BDService();
                            db.AddBDFile(model);

                            var list = db.Query(" and fid = '" + ss + "';");
                            this.dataGrid.ItemsSource = list;
                        }

                    }));
                }
            });
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.MenuItem item = sender as System.Windows.Controls.MenuItem;
                Clipboard.SetText(item?.Tag.ToString());
            }
            catch (Exception ex)
            {
            }
        }
    }
}
