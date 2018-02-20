using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using arcEye_2k18.basicControls;

namespace arcEye_2k18.controllers
{
    public static class movementController
    {
        public static void roverMovementController(object sender, KeyEventArgs e)
        {
            if (sender == null && e == null)
            {
                movementControl.roverMovement(RoverMovement.stop);
            }
            else
            {
                if (e.Key == Key.W)
                {
                    movementControl.roverMovement(RoverMovement.forward);
                }
                else if (e.Key == Key.A)
                {
                    movementControl.roverMovement(RoverMovement.left);
                }
                else if (e.Key == Key.S)
                {
                    movementControl.roverMovement(RoverMovement.backward);
                }
                else if (e.Key == Key.D)
                {
                    movementControl.roverMovement(RoverMovement.right);
                }
                else
                {
                    movementControl.roverMovement(RoverMovement.stop);
                }
            }
        }

        public static void handMovementController(object sender, KeyEventArgs e)
        {
            if (sender == null && e == null)
            {
                movementControl.handMovement(HandMovement.stop);
            }
            else
            {
                if (e.Key == Key.F)
                {
                    movementControl.handMovement(HandMovement.firstDLeft);
                }
                else if (e.Key == Key.C)
                {
                    movementControl.handMovement(HandMovement.firstDRight);
                }
                else if (e.Key == Key.G)
                {
                    movementControl.handMovement(HandMovement.secondDUp);
                }
                else if (e.Key == Key.V)
                {
                    movementControl.handMovement(HandMovement.secondDDown);
                }
                else if (e.Key == Key.H)
                {
                    movementControl.handMovement(HandMovement.thirdDUp);
                }
                else if (e.Key == Key.B)
                {
                    movementControl.handMovement(HandMovement.thirdDDown);
                }
                else if (e.Key == Key.J)
                {
                    movementControl.handMovement(HandMovement.fourthDLeft);
                }
                else if (e.Key == Key.N)
                {
                    movementControl.handMovement(HandMovement.fourthDRight);
                }
                else if (e.Key == Key.K)
                {
                    movementControl.handMovement(HandMovement.fifthDUp);
                }
                else if (e.Key == Key.M)
                {
                    movementControl.handMovement(HandMovement.fifthDDown);
                }
                else if (e.Key == Key.L)
                {
                    movementControl.handMovement(HandMovement.clawOn);
                }
                else if (e.Key == Key.OemComma)
                {
                    movementControl.handMovement(HandMovement.clawOff);
                }
                else
                {
                    movementControl.handMovement(HandMovement.stop);
                }
            }
        }

        public static void cameraMovementController(object sender, KeyEventArgs e)
        {
            if (sender == null && e == null)
            {
                movementControl.cameraMovement(CameraMovement.stop);
            }
            else
            {
                if (e.Key == Key.Up || e.Key == Key.NumPad8)
                {
                    movementControl.cameraMovement(CameraMovement.forward);
                }
                else if (e.Key == Key.Left || e.Key == Key.NumPad4)
                {
                    movementControl.cameraMovement(CameraMovement.left);
                }
                else if (e.Key == Key.Down || e.Key == Key.NumPad5)
                {
                    movementControl.cameraMovement(CameraMovement.backward);
                }
                else if (e.Key == Key.Right || e.Key == Key.NumPad6)
                {
                    movementControl.cameraMovement(CameraMovement.right);
                }
                else
                {
                    movementControl.cameraMovement(CameraMovement.stop);
                }
            }
        }
    }
}
