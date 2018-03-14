using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using XDMessaging;
using XDMessaging.Messages;

namespace arcEye_2k18.chartControls
{
    /// <summary>
    /// Interaction logic for angularGauge.xaml
    /// </summary>
    public partial class angularGauge : UserControl
    {
        public angularGauge() : this(ChannelList.angularGauge.ToString())
        {
        }

        public angularGauge(String _angularGaugeName) : this(_angularGaugeName, 0, 100)
        {
        }

        public angularGauge(String _angularGaugeName, int minValue, int maxValue)
        {
            InitializeComponent();
            this.Name = _angularGaugeName;
            this.group_box.Header = _angularGaugeName;
            this.angular_gauge.FromValue = minValue;
            this.angular_gauge.ToValue = maxValue;
            this.section_one.FromValue = minValue;
            this.section_one.ToValue = maxValue * 0.2;
            this.section_two.FromValue = maxValue * 0.2;
            this.section_two.ToValue = maxValue * 0.7;
            this.section_three.FromValue = maxValue * 0.7;
            this.section_three.ToValue = maxValue;
            this.initIMessageReceiver(_angularGaugeName);
        }
    }
    public partial class angularGauge : iMessageReceiver
    {
        public IXDBroadcaster _xdBroadcaster { get; set; }
        public XDMessagingClient _xdMessagingClient { get; set; }
        public IXDListener _xdListener { get; set; }
        public BackgroundWorker _backgroundWorker { get; set; }
        TypedDataGram<angularGaugeData> _typedData;

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
                    this.group_box.Dispatcher.Invoke((Action)(() =>
                        {
                            this.angular_gauge.Value = this._typedData.Message.angularGaugeValue;
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
