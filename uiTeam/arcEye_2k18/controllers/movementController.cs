using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using arcEye_2k18.basicControls;
using arcEye_2k18.dataTemplate;
using XDMessaging;
using XDMessaging.Messages;

namespace arcEye_2k18.controllers
{ 
    public static class movementController
    {
        public static void roverMovementController(Key _key)
        {
            if (_key == Key.Back)
            {
                movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                    new movementControlData(RoverMovement.stop, CameraMovement.None, HandMovement.None));
            }
            else
            {
                if (_key == Key.W)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.forward, CameraMovement.None, HandMovement.None));
                }
                else if (_key == Key.A)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.left, CameraMovement.None, HandMovement.None));
                }
                else if (_key == Key.S)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.backward, CameraMovement.None, HandMovement.None));
                }
                else if (_key == Key.D)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.right, CameraMovement.None, HandMovement.None));
                }
                else
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.stop, CameraMovement.None, HandMovement.None));
                }
            }
        }

        public static void handMovementController(Key _key)
        {
            if (_key == Key.Back)
            {
                movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                    new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.stop));
            }
            else
            {
                if (_key == Key.F)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.firstDLeft));
                }
                else if (_key == Key.C)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.firstDRight));
                }
                else if (_key == Key.G)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.secondDUp));
                }
                else if (_key == Key.V)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.secondDDown));
                }
                else if (_key == Key.H)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.thirdDUp));
                }
                else if (_key == Key.B)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.thirdDDown));
                }
                else if (_key == Key.J)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.fourthDLeft));
                }
                else if (_key == Key.N)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.fourthDRight));
                }
                else if (_key == Key.K)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.fifthDUp));
                }
                else if (_key == Key.M)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.fifthDDown));
                }
                else if (_key == Key.L)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.clawOn));
                }
                else if (_key == Key.OemComma)
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.clawOff));
                }
                else
                {
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.None, HandMovement.stop));
                }
            }
        }

        public static void cameraMovementController(Key _key)
        {
            if (_key == Key.Back)
            {
                //  movementControl.cameraMovement(CameraMovement.stop);
                movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                    new movementControlData(RoverMovement.None, CameraMovement.stop, HandMovement.None));
            }
            else
            {
                if (_key == Key.Up || _key == Key.NumPad8)
                {
                    //movementControl.cameraMovement(CameraMovement.forward);
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.forward, HandMovement.None));

                }
                else if (_key == Key.Left || _key == Key.NumPad4)
                {
                    // movementControl.cameraMovement(CameraMovement.left);
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.left, HandMovement.None));
                }
                else if (_key == Key.Down || _key == Key.NumPad5)
                {
                    // movementControl.cameraMovement(CameraMovement.backward);
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.backward, HandMovement.None));
                }
                else if (_key == Key.Right || _key == Key.NumPad6)
                {
                    // movementControl.cameraMovement(CameraMovement.right);
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.right, HandMovement.None));
                }
                else
                {
                    // movementControl.cameraMovement(CameraMovement.stop);
                    movementController._xdBroadcaster.SendToChannel(ChannelList.movementControl.ToString(),
                        new movementControlData(RoverMovement.None, CameraMovement.stop, HandMovement.None));
                }
            }
        }
        public static IXDBroadcaster _xdBroadcaster { get; set; }
        public static XDMessagingClient _xdMessagingClient { get; set; }

        static TypedDataGram<movementControlData> _typedData;

        public static void initIMessageReceiver(string _contentName)
        {
           
            movementController._xdMessagingClient = new XDMessagingClient();
            movementController._xdBroadcaster =
                movementController._xdMessagingClient.Broadcasters.GetBroadcasterForMode(XDTransportMode.HighPerformanceUI);
            movementController._xdBroadcaster.SendToChannel(ChannelList.statusBar.ToString(),
                new statusBarData(statusBarPoint.Normal, "MovementController is initialization successful"));
        }
     
    }
}
