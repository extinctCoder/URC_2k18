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
            this.browser_4.Address = this._addressFour;
        }

        private void Cam_view_btn_1_OnClick(object sender, RoutedEventArgs e)
        {
            this.browser_1.Address = this._addressOne;
            this.browser_2.Address = this._addressTwo;
            this.browser_3.Address = this._addressThree;
            this.browser_4.Address = this._addressFour;
        }

        private void Cam_view_btn_2_OnClick(object sender, RoutedEventArgs e)
        {
            this.browser_1.Address = this._addressTwo;
            this.browser_2.Address = this._addressOne;
            this.browser_3.Address = this._addressThree;
            this.browser_4.Address = this._addressFour;
        }

        private void Cam_view_btn_3_OnClick(object sender, RoutedEventArgs e)
        {
            this.browser_1.Address = this._addressThree;
            this.browser_2.Address = this._addressTwo;
            this.browser_3.Address = this._addressOne;
            this.browser_4.Address = this._addressFour;
        }

        private void Cam_view_btn_4_OnClick(object sender, RoutedEventArgs e)
        {
            this.browser_1.Address = this._addressFour;
            this.browser_2.Address = this._addressTwo;
            this.browser_3.Address = this._addressThree;
            this.browser_4.Address = this._addressOne;
        }
    }

    public partial class visionModule
    {
        private string _addressOne = "http://192.168.0.102";
        private string _addressTwo = "http://192.168.0.102:8081";
        private string _addressThree = "http://192.168.0.102:8082";
        private string _addressFour = "http://192.168.0.102:8083";
    }
}
