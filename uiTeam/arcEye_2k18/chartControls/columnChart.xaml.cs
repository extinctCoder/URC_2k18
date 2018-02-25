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
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Geared;
using LiveCharts.Wpf;
using XDMessaging;
using XDMessaging.Messages;

namespace arcEye_2k18.chartControls
{
    /// <summary>
    /// Interaction logic for columnChart.xaml
    /// </summary>
    public partial class columnChart : UserControl , INotifyPropertyChanged
    {
        private double _axisMax;
        private double _axisMin;
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }
        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks;
            AxisMin = now.Ticks - TimeSpan.FromSeconds(1).Ticks;
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public partial class columnChart : UserControl
    {
        public columnChart() : this(ChannelList.columnChart.ToString())
        {
        }

        public columnChart(String _columnChartName) : this(_columnChartName, 0, 100)
        {
        }

        public columnChart(String _columnChartName, int minValue, int maxValue)
        {
            InitializeComponent();
            this.Name = _columnChartName;
            this.group_box.Header = _columnChartName;
            this._seriesChartValues = new GearedValues<dataModel>();
            this._seriesChartValues.WithQuality(Quality.Highest);
            this.initIMessageReceiver(_columnChartName);
            var mapper = Mappers.Xy<dataModel>()
                        .X(model => model.DateTime.Ticks)
                        .Y(model => model.Value);
            Charting.For<dataModel>(mapper);
            DateTimeFormatter = value => new DateTime((long)value).ToString("mm:ss");
            AxisStep = TimeSpan.FromSeconds(1).Ticks;
            AxisUnit = TimeSpan.TicksPerSecond;
            SetAxisLimits(DateTime.Now);
            DataContext = this;
        }
    }
    public partial class columnChart : iMessageReceiver
    {
        public IXDBroadcaster _xdBroadcaster { get; set; }
        public XDMessagingClient _xdMessagingClient { get; set; }
        public IXDListener _xdListener { get; set; }
        public BackgroundWorker _backgroundWorker { get; set; }

        TypedDataGram<coloumnChartData> _typedData;

        public GearedValues<dataModel> _seriesChartValues { get; set; }

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
                    //this._seriesChartValues.Add(new dataModel() { DateTime = DateTime.Now, Value = _typedData.Message.coloumnChartValue });
                    this.group_box.Dispatcher.Invoke((Action)(() =>
                    {
                        this._seriesChartValues.Add(new dataModel() { DateTime = DateTime.Now, Value = _typedData.Message.coloumnChartValue });
                        SetAxisLimits(DateTime.Now);

                        //lets only use the last 150 values
                        if (_seriesChartValues.Count > 150) _seriesChartValues.RemoveAt(0);
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
