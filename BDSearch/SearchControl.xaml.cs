using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            List<TestModel> list = new List<TestModel>
            {
                new TestModel{Index = 1, Name = "lsp" , Selected = true, Type = "1", Remark = "lsp"},
                new TestModel{Index = 2, Name = "lsp" , Selected = true, Type = "1", Remark = "lsp"}
            };

            for (int i = 0; i < 50; i++)
            {
                list.Add(new TestModel { Index = 2, Name = "lsp", Selected = true, Type = "1", Remark = "lsp" });
            }

            this.dataGrid.ItemsSource = list;
        }

        /// <summary>
        /// SS号查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 自建库 获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnZjk_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
