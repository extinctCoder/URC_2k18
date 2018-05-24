using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using arcEye_2k18.basicControls;
using arcEye_2k18.chartControls;
using arcEye_2k18.controllers;
using arcEye_2k18.dataTemplate;
using Dragablz;
using LiveCharts.Geared;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using XDMessaging;
using XDMessaging.Messages;

namespace arcEye_2k18
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            movementController.initIMessageReceiver(ChannelList.movementControl.ToString());
            this.initIMessageReceiver(ChannelList.MainWindow.ToString());
            InitializeComponent();
            this.initializeControls();
            homeLayout();
            
            
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this._xdBroadcaster.SendToChannel(ChannelList.keyStroke.ToString(),
                    new keyStrokeData(KeyPosition.keyDown, e.Key));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            e.Handled = true;
        }

        private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                this._xdBroadcaster.SendToChannel(ChannelList.keyStroke.ToString(),
                    new keyStrokeData(KeyPosition.keyUp, e.Key));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            e.Handled = true;
        }

    }
    public partial class MainWindow : iMessageReceiver
    {
        public IXDBroadcaster _xdBroadcaster { get; set; }
        public XDMessagingClient _xdMessagingClient { get; set; }
        public IXDListener _xdListener { get; set; }
        public BackgroundWorker _backgroundWorker { get; set; }

        TypedDataGram<keyStrokeData> _typedData;

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
                new statusBarData(statusBarPoint.Normal, "arcEye_2k18 MainWindow is initialization successful"));
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
            if (xdMessageEventArgs.DataGram.Channel == ChannelList.MainWindow.ToString())
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
                    //this.group_box.Dispatcher.Invoke((Action)(() =>
                    //{
                    //    this._seriesChartValues.Add(new dataModel() { DateTime = DateTime.Now, Value = _typedData.Message.coloumnChartValue });
                    //    SetAxisLimits(DateTime.Now);

                    //    //lets only use the last 150 values
                    //    if (_seriesChartValues.Count > 150) _seriesChartValues.RemoveAt(0);
                    //}), DispatcherPriority.Background);
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

    public partial class MainWindow
    {
        private columnChart _sensor1;
        private columnChart _sensor2;
        private columnChart _sensor3;
        private columnChart _sensor4;
        private lineChart _sensor5;
        private lineChart _sensor6;
        private mapModule _mapModule;
        private visionModule _visionModule;
        private gyroModule _gyroModule;
        
        private void initializeControls()
        {
            this._sensor1 = new columnChart(ChannelListMod.SoilSensor.ToString());
            this._sensor2 = new columnChart(ChannelListMod.UvSensor.ToString());
            this._sensor3 = new columnChart(ChannelListMod.GasSensore.ToString());
            this._sensor4 = new columnChart(ChannelListMod.TempSensor.ToString());
            this._sensor5 = new lineChart(ChannelListMod.AirTempSensor.ToString());
            this._sensor6 = new lineChart(ChannelListMod.AirHumSensor.ToString());
            this._mapModule = new mapModule();
            this._visionModule = new visionModule();
            this._gyroModule = new gyroModule();
        }

        private void clearLayout()
        {
            this.fullScreenGrid.Children.Clear();

            this.sensorOne.Children.Clear();
            this.sensorTwo.Children.Clear();
            this.sensorThree.Children.Clear();
            this.sensorFour.Children.Clear();
            this.sensorFive.Children.Clear();
            this.sensorSix.Children.Clear();
            this.mapModule.Children.Clear();
            this.visionModule.Children.Clear();
            this.gyroModule.Children.Clear();
        }

        private void homeLayout()
        {
            this.clearLayout();

            this.homeGrid.Visibility = Visibility.Visible;
            this.fullScreenGrid.Visibility = Visibility.Hidden;

            this.sensorOne.Children.Add(this._sensor1);
            this.sensorTwo.Children.Add(this._sensor2);
            this.sensorThree.Children.Add(this._sensor3);
            this.sensorFour.Children.Add(this._sensor4);
            this.sensorFive.Children.Add(this._sensor5);
            this.sensorSix.Children.Add(this._sensor6);
            this.mapModule.Children.Add(this._mapModule);
            this.visionModule.Children.Add(this._visionModule);
            this.gyroModule.Children.Add(this._gyroModule);
        }

        private void fullScreenLayout()
        {
            this.clearLayout();

            this.homeGrid.Visibility = Visibility.Hidden;
            this.fullScreenGrid.Visibility = Visibility.Visible;
        }

        private void InfoButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.dialogHost.IsOpen = true;
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.homeLayout();
        }

        private void VisionModuleButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.fullScreenLayout();
            this.fullScreenGrid.Children.Add(this._visionModule);
        }

        private void GpsModuleButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.fullScreenLayout();
            this.fullScreenGrid.Children.Add(this._mapModule);
        }

        private void GyroModuleButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.fullScreenLayout();
            this.fullScreenGrid.Children.Add(this._gyroModule);
        }

        private void Sensor1Button_OnClick(object sender, RoutedEventArgs e)
        {
            this.fullScreenLayout();
            this.fullScreenGrid.Children.Add(this._sensor1);
        }

        private void Sensor2Button_OnClick(object sender, RoutedEventArgs e)
        {
            this.fullScreenLayout();
            this.fullScreenGrid.Children.Add(this._sensor2);
        }

        private void Sensor3Button_OnClick(object sender, RoutedEventArgs e)
        {
            this.fullScreenLayout();
            this.fullScreenGrid.Children.Add(this._sensor3);
        }

        private void Sensor4Button_OnClick(object sender, RoutedEventArgs e)
        {
            this.fullScreenLayout();
            this.fullScreenGrid.Children.Add(this._sensor4);
        }

        private void Sensor5Button_OnClick(object sender, RoutedEventArgs e)
        {
            this.fullScreenLayout();
            this.fullScreenGrid.Children.Add(this._sensor5);
        }

        private void Sensor6Button_OnClick(object sender, RoutedEventArgs e)
        {
            this.fullScreenLayout();
            this.fullScreenGrid.Children.Add(this._sensor6);
        }
    }

}
