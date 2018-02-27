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
        }
    }
}
