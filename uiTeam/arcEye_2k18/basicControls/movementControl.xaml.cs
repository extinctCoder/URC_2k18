using System;
using System.Collections.Generic;
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
using arcEye_2k18.controllers;

namespace arcEye_2k18.basicControls
{
    /// <summary>
    /// Interaction logic for movementControl.xaml
    /// </summary>
    public partial class movementControl : UserControl
    {
        private static movementControl _movementControl;

        private movementControl()
        {
            InitializeComponent();
            this.defultButtonState();
        }

        public static movementControl obj
        {
            get
            {
                if (movementControl._movementControl == null)
                {
                    movementControl._movementControl=new movementControl();
                }

                return movementControl._movementControl;
            }
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

        public static void roverMovement(RoverMovement _roverMovement)
        {
            switch (_roverMovement)
            {
                case RoverMovement.forward:
                    movementControl.obj.rover_forward_btn.Background = Brushes.Red;
                    break;
                case RoverMovement.backward:
                    movementControl.obj.rover_backward_btn.Background = Brushes.Red;
                    break;

                case RoverMovement.left:
                    movementControl.obj.rover_left_btn.Background = Brushes.Red;
                    break;
                case RoverMovement.right:
                    movementControl.obj.rover_right_btn.Background = Brushes.Red;
                    break;
                case RoverMovement.stop:
                    movementControl.obj.defultButtonState();
                    break;
                default:
                    statusBar.statusAi("Unregistered user input.");
                    break;
            }
        }

        public static void cameraMovement(CameraMovement _cameaMovement)
        {
            switch (_cameaMovement)
            {
                case CameraMovement.forward:
                    movementControl.obj.camera_up_btn.Background = Brushes.Red;
                    break;
                case CameraMovement.backward:
                    movementControl.obj.camera_down_btn.Background = Brushes.Red;
                    break;

                case CameraMovement.left:
                    movementControl.obj.camera_left_btn.Background = Brushes.Red;
                    break;
                case CameraMovement.right:
                    movementControl.obj.camera_right_btn.Background = Brushes.Red;
                    break;
                case CameraMovement.stop:
                    movementControl.obj.defultButtonState();
                    break;
                default:
                    statusBar.statusAi("Unregistered user input.");
                    break;
            }
        }

        public static void handMovement(HandMovement _handMovement)
        {
            switch (_handMovement)
            {
                case HandMovement.firstDLeft:
                    movementControl.obj.first_degree_left_btn.Background = Brushes.Red;
                    break;
                case HandMovement.firstDRight:
                    movementControl.obj.first_degree_right_btn.Background = Brushes.Red;
                    break;
                case HandMovement.secondDUp:
                    movementControl.obj.second_degree_up_btn.Background = Brushes.Red;
                    break;
                case HandMovement.secondDDown:
                    movementControl.obj.second_degree_down_btn.Background = Brushes.Red;
                    break;
                case HandMovement.thirdDUp:
                    movementControl.obj.third_degree_up_btn.Background = Brushes.Red;
                    break;
                case HandMovement.thirdDDown:
                    movementControl.obj.third_degree_down_btn.Background = Brushes.Red;
                    break;
                case HandMovement.fourthDLeft:
                    movementControl.obj.fourth_degree_left_btn.Background = Brushes.Red;
                    break;
                case HandMovement.fourthDRight:
                    movementControl.obj.fourth_degree_right_btn.Background = Brushes.Red;
                    break;
                case HandMovement.fifthDUp:
                    movementControl.obj.fifth_degree_up_btn.Background = Brushes.Red;
                    break;
                case HandMovement.fifthDDown:
                    movementControl.obj.fifth_degree_down_btn.Background = Brushes.Red;
                    break;
                case HandMovement.clawOn:
                    movementControl.obj.claw_on_btn.Background = Brushes.Red;
                    break;
                case HandMovement.clawOff:
                    movementControl.obj.claw_off_btn.Background = Brushes.Red;
                    break;
                case HandMovement.stop:
                    movementControl.obj.defultButtonState();
                    break;
                default:
                    statusBar.statusAi("Unregistered user input.");
                    break;
            }
        }
    }
}
