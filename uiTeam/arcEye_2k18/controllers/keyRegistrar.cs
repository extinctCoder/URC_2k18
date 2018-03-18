using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using arcEye_2k18.dataTemplate;
using LiveCharts.Geared;
using XDMessaging;
using XDMessaging.Messages;

namespace arcEye_2k18.controllers
{
    public partial class keyRegistrar
    {
        public keyRegistrar():this(ChannelList.keyStroke.ToString())
        {

        }

        public keyRegistrar(String _contentName)
        {
            this.initIMessageReceiver(_contentName);
        }
        private void onKeyDown(Key _key)
        {
            if (_key == Key.W || _key == Key.A || _key == Key.D || _key == Key.S)
            {
                movementController.roverMovementController(_key);
            }
            else if (_key == Key.Up || _key == Key.Left || _key == Key.Right || _key == Key.Down || _key == Key.NumPad8 || _key == Key.NumPad4 || _key == Key.NumPad5 || _key == Key.NumPad6)
            {
                movementController.cameraMovementController(_key);
            }
            else if (_key == Key.F || _key == Key.G || _key == Key.H || _key == Key.J || _key == Key.K || _key == Key.L || _key == Key.C || _key == Key.V || _key == Key.B || _key == Key.N || _key == Key.M || _key == Key.OemComma)
            {
                movementController.handMovementController(_key);
            }
            else if (_key == Key.F1)
            {
              //  this.Home_btn_OnClick(new ObjectDataProvider(), new RoutedEventArgs());
            }
            else if (_key == Key.F2)
            {
              //  this.Full_screen_btn_OnClick(new ObjectDataProvider(), new RoutedEventArgs());
            }
            else if (_key == Key.F3)
            {
              //  this.Normal_btn_OnClick(new ObjectDataProvider(), new RoutedEventArgs());
            }
            else if (_key == Key.F4)
            {
              //  this.Settings_btn_OnClick(new ObjectDataProvider(), new RoutedEventArgs());
            }
            else if (_key == Key.F12)
            {
               // this.dialog_host.IsOpen = true;
            }
            else if (_key == Key.LeftShift)
            {

            }
            else if (_key == Key.LeftCtrl)
            {

            }
            else
            {

            }
        }

        private void onKeyUp(Key _key)
        {
            if (_key == Key.W || _key == Key.A || _key == Key.D || _key == Key.S)
            {
                movementController.roverMovementController(Key.Back);
            }
            else if (_key == Key.Up || _key == Key.Left || _key == Key.Right || _key == Key.Down || _key == Key.NumPad8 || _key == Key.NumPad4 || _key == Key.NumPad5 || _key == Key.NumPad6)
            {
                movementController.cameraMovementController(Key.Back);
            }
            else if (_key == Key.F || _key == Key.G || _key == Key.H || _key == Key.J || _key == Key.K || _key == Key.L || _key == Key.C || _key == Key.V || _key == Key.B || _key == Key.N || _key == Key.M || _key == Key.OemComma)
            {
                movementController.handMovementController(Key.Back);
            }
            else if (_key == Key.F1 || _key == Key.F2 || _key == Key.F3 || _key == Key.F4)
            {
               // this.Home_btn_OnClick(new ObjectDataProvider(), new RoutedEventArgs());
            }
            else if (_key == Key.F12)
            {
              //  this.dialog_host.IsOpen = false;
            }
            else if (_key == Key.LeftShift)
            {

            }
            else if (_key == Key.LeftCtrl)
            {

            }
            else
            {

            }
        }
    }
    public partial class keyRegistrar : iMessageReceiver
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
                new statusBarData(statusBarPoint.Normal, "keyRegistrar initialization is successful"));
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
            if (xdMessageEventArgs.DataGram.Channel == ChannelList.keyStroke.ToString())
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
                    if (this._typedData.Message.keyPosition == KeyPosition.keyDown)
                    {
                        this.onKeyDown(this._typedData.Message.key);
                    }
                    else if (this._typedData.Message.keyPosition == KeyPosition.keyUp)
                    {
                        this.onKeyUp(this._typedData.Message.key);
                    }
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
