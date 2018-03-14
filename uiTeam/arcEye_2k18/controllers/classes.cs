using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.ElasticBeanstalk;

namespace arcEye_2k18.controllers
{
    class classes
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
    public enum ChannelList { statusBar, gyroModule, mapModule, visionModule, angularGauge, solidGauge, lineChart, columnChart }
}
