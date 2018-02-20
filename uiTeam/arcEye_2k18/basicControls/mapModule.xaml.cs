using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
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
using GMap.NET;
using GMap.NET.MapProviders;

namespace arcEye_2k18.basicControls
{
    /// <summary>
    /// Interaction logic for mapModule.xaml
    /// </summary>
    public partial class mapModule : UserControl
    {
        public mapModule()
        {
            InitializeComponent();
            if (!PingNetwork("pingtest.com"))
            {
                this.map.Manager.Mode = AccessMode.CacheOnly;
                statusBar.statusAi_ThreadSafe("No Internet connection available, going to CacheOnly mode");
            }
            else
            {
                statusBar.statusAi_ThreadSafe("Internet connection available, going to normal mode");
            }

            this.map.MapProvider = GMapProviders.GoogleMap;
            this.map.Position = new PointLatLng(23.8103, 90.4125);
            this.map.Zoom = 10;
        }
    }

    public partial class mapModule
    {
        public static bool PingNetwork(string hostNameOrAddress)
        {
            using (Ping p = new Ping())
            {
                byte[] buffer = Encoding.ASCII.GetBytes("network test string");
                int timeout = 4444; // 4s

                try
                {
                    PingReply reply = p.Send(hostNameOrAddress, timeout, buffer);
                    return (reply.Status == IPStatus.Success) ? true : false;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
            return false;
        }
    }
}
