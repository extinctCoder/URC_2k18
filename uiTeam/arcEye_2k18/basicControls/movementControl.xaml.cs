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
using arcEye_2k18.dataTemplate;
using XDMessaging;
using XDMessaging.Messages;

namespace arcEye_2k18.basicControls
{
    /// <summary>
    /// Interaction logic for movementControl.xaml
    /// </summary>
    public partial class movementControl : UserControl
    {

        public movementControl():this(ChannelList.movementControl.ToString())
        {
        }
        
        public movementControl(String _movementControlName)
        {
            InitializeComponent();
            this.defultButtonState();
            this.Name = _movementControlName;
            this.initIMessageReceiver(_movementControlName);
        }
    }

    public partial class movementControl
    {
        private void defultButtonState()
        {
            this.rover_forward_btn.Background = Brushes.Transparent;
            this.rover_backward_btn.Background = Brushes.Transparent;
            this.rover_left_btn.Background = Brushes.Transparent;
            this.rover_right_btn.Background = Brushes.Transparent;

            this.camera_up_btn.Background = Brushes.Transparent;
            this.camera_down_btn.Background = Brushes.Transparent;
            this.camera_left_btn.Background = Brushes.Transparent;
            this.camera_right_btn.Background = Brushes.Transparent;

            this.first_degree_left_btn.Background = Brushes.Transparent;
            this.first_degree_right_btn.Background = Brushes.Transparent;
            this.second_degree_up_btn.Background = Brushes.Transparent;
            this.second_degree_down_btn.Background = Brushes.Transparent;
            this.third_degree_up_btn.Background = Brushes.Transparent;
            this.third_degree_down_btn.Background = Brushes.Transparent;
            this.fourth_degree_left_btn.Background = Brushes.Transparent;
            this.fourth_degree_right_btn.Background = Brushes.Transparent;
            this.fifth_degree_up_btn.Background = Brushes.Transparent;
            this.fifth_degree_down_btn.Background = Brushes.Transparent;
            this.claw_on_btn.Background = Brushes.Transparent;
            this.claw_off_btn.Background = Brushes.Transparent;
        }

        public void roverMovement(RoverMovement _roverMovement)
        {
            switch (_roverMovement)
            {
                case RoverMovement.forward:
                    this.rover_forward_btn.Background = Brushes.Red;
                    break;
                case RoverMovement.backward:
                    this.rover_backward_btn.Background = Brushes.Red;
                    break;

                case RoverMovement.left:
                    this.rover_left_btn.Background = Brushes.Red;
                    break;
                case RoverMovement.right:
                    this.rover_right_btn.Background = Brushes.Red;
                    break;
                case RoverMovement.stop:
                    this.defultButtonState();
                    break;
                default:
                    //statusBar.statusAi("Unregistered user input.");
                    break;
            }
        }

        public void cameraMovement(CameraMovement _cameaMovement)
        {
            switch (_cameaMovement)
            {
                case CameraMovement.forward:
                    this.camera_up_btn.Background = Brushes.Red;
                    break;
                case CameraMovement.backward:
                    this.camera_down_btn.Background = Brushes.Red;
                    break;

                case CameraMovement.left:
                    this.camera_left_btn.Background = Brushes.Red;
                    break;
                case CameraMovement.right:
                    this.camera_right_btn.Background = Brushes.Red;
                    break;
                case CameraMovement.stop:
                    this.defultButtonState();
                    break;
                default:
                    //statusBar.statusAi("Unregistered user input.");
                    break;
            }
        }

        public void handMovement(HandMovement _handMovement)
        {
            switch (_handMovement)
            {
                case HandMovement.firstDLeft:
                    this.first_degree_left_btn.Background = Brushes.Red;
                    break;
                case HandMovement.firstDRight:
                    this.first_degree_right_btn.Background = Brushes.Red;
                    break;
                case HandMovement.secondDUp:
                    this.second_degree_up_btn.Background = Brushes.Red;
                    break;
                case HandMovement.secondDDown:
                    this.second_degree_down_btn.Background = Brushes.Red;
                    break;
                case HandMovement.thirdDUp:
                    this.third_degree_up_btn.Background = Brushes.Red;
                    break;
                case HandMovement.thirdDDown:
                    this.third_degree_down_btn.Background = Brushes.Red;
                    break;
                case HandMovement.fourthDLeft:
                    this.fourth_degree_left_btn.Background = Brushes.Red;
                    break;
                case HandMovement.fourthDRight:
                    this.fourth_degree_right_btn.Background = Brushes.Red;
                    break;
                case HandMovement.fifthDUp:
                    this.fifth_degree_up_btn.Background = Brushes.Red;
                    break;
                case HandMovement.fifthDDown:
                    this.fifth_degree_down_btn.Background = Brushes.Red;
                    break;
                case HandMovement.clawOn:
                    this.claw_on_btn.Background = Brushes.Red;
                    break;
                case HandMovement.clawOff:
                    this.claw_off_btn.Background = Brushes.Red;
                    break;
                case HandMovement.stop:
                    this.defultButtonState();
                    break;
                default:
                    //statusBar.statusAi("Unregistered user input.");
                    break;
            }
        }
    }
    public partial class movementControl : iMessageReceiver
    {
        public IXDBroadcaster _xdBroadcaster { get; set; }
        public XDMessagingClient _xdMessagingClient { get; set; }
        public IXDListener _xdListener { get; set; }
        public BackgroundWorker _backgroundWorker { get; set; }
        TypedDataGram<movementControlData> _typedData;

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
                    this.Dispatcher.Invoke((Action) (() =>
                    {
                        if (this._typedData.Message.cameaMovement == CameraMovement.None &&
                            this._typedData.Message.handMovement == HandMovement.None &&
                            this._typedData.Message.roverMovement != RoverMovement.None)
                        {
                            this.roverMovement(this._typedData.Message.roverMovement);
                        }
                        else if (this._typedData.Message.cameaMovement == CameraMovement.None &&
                                 this._typedData.Message.handMovement != HandMovement.None &&
                                 this._typedData.Message.roverMovement == RoverMovement.None)
                        {
                            this.handMovement(this._typedData.Message.handMovement);
                        }
                        else if (this._typedData.Message.cameaMovement != CameraMovement.None &&
                                 this._typedData.Message.handMovement == HandMovement.None &&
                                 this._typedData.Message.roverMovement == RoverMovement.None)
                        {
                            this.cameraMovement(this._typedData.Message.cameaMovement);
                        }
                        else
                        {
                            this.defultButtonState();
                        }
                    }));
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
