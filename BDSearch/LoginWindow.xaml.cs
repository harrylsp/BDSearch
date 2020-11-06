﻿using Common;
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
using System.Windows.Shapes;

namespace BDSearch
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
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

        }

        /// <summary>
        /// 生成机器码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.tbMachineCode.Text = Util.GenerateMachineCode(this.tbUserName.Text.Trim());

        }
    }
}
