using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using arcEye_2k18.controllers;
using arcEye_2k18.dataTemplate;
using GMap.NET;
using GMap.NET.MapProviders;
using XDMessaging;
using XDMessaging.Messages;
using ThicknessConverter = Xceed.Wpf.DataGrid.Converters.ThicknessConverter;

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
            this._backgroundWorkerProgressBar = new BackgroundWorker();
            this._backgroundWorkerProgressBar.DoWork += BackgroundWorkerProgressBarOnDoWork;
            this.initMap();
            this.Name = ChannelList.mapModule.ToString();
            this.group_box.Header = ChannelList.mapModule.ToString();
            this.initIMessageReceiver(ChannelList.mapModule.ToString());
        }

        public mapModule(String _mapModuleName)
        {
            InitializeComponent();
            this._backgroundWorkerProgressBar = new BackgroundWorker();
            this._backgroundWorkerProgressBar.DoWork += BackgroundWorkerProgressBarOnDoWork;
            this.initMap();
            this.Name = _mapModuleName;
            this.group_box.Header = _mapModuleName;
            this.initIMessageReceiver(_mapModuleName);
        }
    }

    public partial class mapModule
    {
        private bool _progressBarFlag = false;
        private BackgroundWorker _backgroundWorkerProgressBar;
        private void initMap()
        {
            if (!PingNetwork("pingtest.com"))
            {
                this.map_module.Manager.Mode = AccessMode.CacheOnly;
                //statusBar.statusAi_ThreadSafe("No Internet connection available, going to CacheOnly mode");
            }
            else
            {
                //statusBar.statusAi_ThreadSafe("Internet connection available, going to normal mode");
            }
            this.map_module.OnTileLoadStart += MapModuleOnOnTileLoadStart;
            this.map_module.OnTileLoadComplete += MapModuleOnOnTileLoadComplete;
            this.map_module.MapProvider = GMapProviders.GoogleTerrainMap;
            this.map_module.Position = new PointLatLng(23.8103, 90.4125);
            this.map_module.Zoom = 10;
        }

        private void MapModuleOnOnTileLoadComplete(long elapsedMilliseconds)
        {
            this._progressBarFlag = false;
            if (!this._backgroundWorkerProgressBar.IsBusy)
            {
                this._backgroundWorkerProgressBar.RunWorkerAsync();
            }  
        }

        private void MapModuleOnOnTileLoadStart()
        {

            this._progressBarFlag = true;
            if (!this._backgroundWorkerProgressBar.IsBusy)
            {
                this._backgroundWorkerProgressBar.RunWorkerAsync();
            }
        }
        private void BackgroundWorkerProgressBarOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            this.group_box.Dispatcher.Invoke((Action)(() => { this.progress_bar.IsIndeterminate = this._progressBarFlag; }), DispatcherPriority.Background);
        }

        private static bool PingNetwork(string hostNameOrAddress)
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
    public partial class mapModule : iMessageReceiver
    {
        public IXDBroadcaster _xdBroadcaster { get; set; }
        public XDMessagingClient _xdMessagingClient { get; set; }
        public IXDListener _xdListener { get; set; }
        public BackgroundWorker _backgroundWorker { get; set; }
        TypedDataGram<mapModuleData> _typedData;

        void initIMessageReceiver(string _contentName)
        {
            this._backgroundWorker = new BackgroundWorker();
            this._backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            this._backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;

            this._xdMessagingClient = new XDMessagingClient();
            this._xdBroadcaster =
                this._xdMessagingClient.Broadcasters.GetBroadcasterForMode(XDTransportMode.HighPerformanceUI);
            this._xdListener = this._xdMessagingClient.Listeners.GetListenerForMode(XDTransportMode.HighPerformanceUI);
            this._xdListener.RegisterChannel(_contentName);
            this._xdListener.MessageReceived += XdListenerOnMessageReceived;
            this._xdBroadcaster.SendToChannel(ChannelList.statusBar.ToString(),
                new statusBarData(statusBarPoint.Normal, this.Name + "is initialization successful"));
        }

        void iMessageReceiver.XdListenerOnMessageReceived(object sender, XDMessageEventArgs xdMessageEventArgs)
        {
            XdListenerOnMessageReceived(sender, xdMessageEventArgs);
        }

        void iMessageReceiver.BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            BackgroundWorkerOnDoWork(sender, doWorkEventArgs);
        }

        void iMessageReceiver.BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            BackgroundWorkerOnRunWorkerCompleted(sender, runWorkerCompletedEventArgs);
        }

        void iMessageReceiver.initIMessageReceiver(string _contentName)
        {
            initIMessageReceiver(_contentName);
        }

        void XdListenerOnMessageReceived(object sender, XDMessageEventArgs xdMessageEventArgs)
        {
            if (xdMessageEventArgs.DataGram.Channel == this.Name)
            {
                this._typedData = xdMessageEventArgs.DataGram;
                if (!this._backgroundWorker.IsBusy)
                {
                    this._backgroundWorker.RunWorkerAsync();
                }

            }
        }
        void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            try
            {
                if (_typedData.IsValid)
                {
                    this.group_box.Dispatcher.Invoke((Action)(() =>
                        {
                            this.map_module.Position = new PointLatLng(_typedData.Message.xAxisValue,
                                _typedData.Message.yAxisValue);
                        }), DispatcherPriority.Background);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }
        void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            //
        }
    }
}
