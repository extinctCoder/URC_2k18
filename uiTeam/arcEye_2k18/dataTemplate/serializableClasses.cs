using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace arcEye_2k18.dataTemplate
{
    class serializableClasses
    {
    }
    public class dataModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
    }
    [Serializable]
    public class mapModuleData
    {
        public double xAxisValue;
        public double yAxisValue;
    }
    [Serializable]
    public class gyroModuleData
    {
        public double xAxisData;
        public double yAxisData;
        public double zAxisData;
    }
    [Serializable]
    public class angularGaugeData
    {
        public double angularGaugeValue;
    }
    [Serializable]
    public class solidGaugeData
    {
        public double solidGaugeValue;
    }
    [Serializable]
    public class lineChartData
    {
        public double lineChartValue;
    }
    [Serializable]
    public class coloumnChartData
    {
        public double coloumnChartValue;
    }
    [Serializable]
    public class statusBarData
    {
        public statusBarData(statusBarPoint point, string statusBarString)
        {
            this.point = point;
            this.statusBarString = statusBarString;
        }
        public statusBarPoint point;
        public string statusBarString;
    }
    [Serializable]
    public class keyStrokeData
    {
        public keyStrokeData(KeyPosition keyPosition, Key key)
        {
            this.keyPosition = keyPosition;
            this.key = key;
        }
        public Key key;
        public KeyPosition keyPosition;
    }

    [Serializable]
    public class movementControlData
    {
        public movementControlData() : this(RoverMovement.None, CameraMovement.None, HandMovement.None)
        {

        }
        public movementControlData(RoverMovement roverMovement, CameraMovement cameaMovement, HandMovement handMovement)
        {
            this.roverMovement = roverMovement;
            this.handMovement = handMovement;
            this.cameaMovement = cameaMovement;
        }
        public RoverMovement roverMovement;
        public CameraMovement cameaMovement;
        public HandMovement handMovement;
    }
}
