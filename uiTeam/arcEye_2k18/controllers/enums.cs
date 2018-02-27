using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcEye_2k18.controllers
{
    class enums
    {
    }

    public enum RoverMovement
    {
        forward, backward, left, right, stop
    }

    public enum CameraMovement
    {
        forward, backward, left, right, stop
    }

    public enum HandMovement
    {
        firstDLeft, firstDRight, secondDUp, secondDDown, thirdDUp, thirdDDown, fourthDLeft, fourthDRight, fifthDUp, fifthDDown, clawOn, clawOff, stop
    }
}
