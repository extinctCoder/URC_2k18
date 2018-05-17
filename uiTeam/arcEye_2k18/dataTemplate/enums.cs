using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcEye_2k18.dataTemplate
{
    class enums
    {
    }
    public enum RoverMovement
    {
        forward, backward, left, right, stop, None
    }

    public enum CameraMovement
    {
        forward, backward, left, right, stop, None
    }

    public enum HandMovement
    {
        firstDLeft, firstDRight, secondDUp, secondDDown, thirdDUp, thirdDDown, fourthDLeft, fourthDRight, fifthDUp, fifthDDown, clawOn, clawOff, stop, None
    }

    public enum statusBarPoint
    {
        Normal, Ai
    }

    public enum ChannelList
    {
        MainWindow, statusBar, gyroModule, mapModule, visionModule, angularGauge, solidGauge, lineChart, columnChart, keyStroke, movementControl
    }

    public enum ChannelListMod
    {
        MainWindow, statusBar, gyroModule, mapModule, visionModule, lineChart1, lineChart2, columnChart1, columnChart2, columnChart3, columnChart4, keyStroke, movementControl
    }

    public enum KeyPosition
    {
        keyDown, keyUp, normal
    }
}
