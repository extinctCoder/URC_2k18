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
using arcEye_2k18.controllers;
using arcEye_2k18.dataTemplate;
using Amazon.EC2.Model;
using CefSharp.Wpf;

namespace arcEye_2k18.basicControls
{
    /// <summary>
    /// Interaction logic for visionModule.xaml
    /// </summary>
    public partial class visionModule : UserControl
    {
        public visionModule() : this(ChannelList.visionModule.ToString())
        {
        }
        public visionModule(String _visionModuleName)
        {
            InitializeComponent();
            this.group_box.Header = _visionModuleName;
            this.Name = _visionModuleName;
            this.browser_1.Address = this._addressOne;
            this.browser_2.Address = this._addressTwo;
            this.browser_3.Address = this._addressThree;
            this.browser_1.ZoomLevelIncrement = 1;
            this.browser_2.ZoomLevelIncrement = 0.01;
            this.browser_3.ZoomLevelIncrement = 0.01;

        }

        private void Cam_view_btn_1_OnClick(object sender, RoutedEventArgs e)
        {
            this.browser_1.Address = this._addressOne;
            this.browser_2.Address = this._addressTwo;
            this.browser_3.Address = this._addressThree;
            cam_view_btn_1.Background = Brushes.LightBlue;
            cam_view_btn_2.Background = Brushes.White;
            cam_view_btn_3.Background = Brushes.White;
            this.cam_view_btn_1.Content = "C1";
            this.cam_view_btn_2.Content = "C2";
            this.cam_view_btn_3.Content = "C3";
        }

        private void Cam_view_btn_2_OnClick(object sender, RoutedEventArgs e)
        {
            this.browser_1.Address = this._addressTwo;
            this.browser_2.Address = this._addressOne;
            this.browser_3.Address = this._addressThree;
            cam_view_btn_2.Background = Brushes.LightBlue;
            cam_view_btn_1.Background = Brushes.White;
            cam_view_btn_3.Background = Brushes.White;
            this.cam_view_btn_1.Content = "C2";
            this.cam_view_btn_2.Content = "C1";
            this.cam_view_btn_3.Content = "C3";

        }

        private void Cam_view_btn_3_OnClick(object sender, RoutedEventArgs e)
        {
            this.browser_1.Address = this._addressThree;
            this.browser_2.Address = this._addressTwo;
            this.browser_3.Address = this._addressOne;
            cam_view_btn_3.Background = Brushes.LightBlue;
            cam_view_btn_1.Background = Brushes.White;
            cam_view_btn_2.Background = Brushes.White;
            this.cam_view_btn_1.Content = "C3";
            this.cam_view_btn_2.Content = "C2";
            this.cam_view_btn_3.Content = "C1";
        }

    }

    public partial class visionModule
    {
        private string _addressOne = "http://facebook.com";
        private string _addressTwo = "http://youtube.com";
        private string _addressThree = "http://google.com";


    }
}