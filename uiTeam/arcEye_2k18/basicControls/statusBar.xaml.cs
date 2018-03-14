using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Threading;
using arcEye_2k18.controllers;
using LiveCharts.Geared;
using XDMessaging;
using XDMessaging.Messages;

namespace arcEye_2k18.basicControls
{
    /// <summary>
    /// Interaction logic for statusBar.xaml
    /// </summary>
    public partial class statusBar : UserControl
    {

        public statusBar():this(ChannelList.statusBar.ToString())
        {
            InitializeComponent();
        }

        public statusBar(String _statusBarName)
        {
            InitializeComponent();
            this.Name = _statusBarName;
            this.initIMessageReceiver(_statusBarName);
        }

    }
    public partial class statusBar : iMessageReceiver
    {
        public IXDBroadcaster _xdBroadcaster { get; set; }
        public XDMessagingClient _xdMessagingClient { get; set; }
        public IXDListener _xdListener { get; set; }
        public BackgroundWorker _backgroundWorker { get; set; }

        TypedDataGram<statusBarData> _typedData;

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
                if (this._typedData.IsValid)
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        if (this._typedData.Message.point == statusBarPoint.Normal)
                        {
                            this.left_content.Text = "ARC_EYE :_ " + this._typedData.Message.statusBarString;
                        }
                        else if (this._typedData.Message.point == statusBarPoint.Ai)
                        {
                            this.right_content.Text = this._typedData.Message.statusBarString + "_: ARC_EYE_AI";
                        }
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
