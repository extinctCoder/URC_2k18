using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using arcEye_2k18.controllers;
using HelixToolkit.Wpf;
using XDMessaging;
using XDMessaging.Messages;

namespace arcEye_2k18.basicControls
{
    /// <summary>
    /// Interaction logic for gyroModule.xaml
    /// </summary>
    public partial class gyroModule : UserControl
    {
        public gyroModule()
        {
            InitializeComponent();
            this.Name = ChannelList.gyroModule.ToString();
            this.group_box.Header = ChannelList.gyroModule.ToString();
            this.initIMessageReceiver(ChannelList.gyroModule.ToString());
        }

        public gyroModule(String _gyroModuleName)
        {
            InitializeComponent();
            this.Name = _gyroModuleName;
            this.group_box.Header = _gyroModuleName;
            this.initIMessageReceiver(_gyroModuleName);
        }
        
    }

    public partial class gyroModule : iMessageReceiver
    {
        public IXDBroadcaster _xdBroadcaster { get; set; }
        public XDMessagingClient _xdMessagingClient { get; set; }
        public IXDListener _xdListener { get; set; }
        public BackgroundWorker _backgroundWorker { get; set; }
        TypedDataGram<gyroModuleData> _typedData;

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
            this._xdBroadcaster.SendToChannel(ChannelList.statusBar.ToString(), this.Name + " initialization successful.");
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
                if (this._typedData.IsValid)
                {
                    this.group_box.Dispatcher.Invoke((Action)(() =>
                    {
                        this.rtn_x.Angle = _typedData.Message.xAxisData;
                        this.rtn_y.Angle = _typedData.Message.yAxisData;
                        this.rtn_z.Angle = _typedData.Message.zAxisData;
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
