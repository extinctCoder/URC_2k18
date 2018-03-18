using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using arcEye_2k18.dataTemplate;
using Amazon.EC2.Model;
using XDMessaging;

namespace arcEye_2k18.controllers
{
    public class dataPublisher
    {
        private IXDBroadcaster broadcast;
        private XDMessagingClient client;
        private Thread _thread;

        public dataPublisher()
        {
            _thread = new Thread(runDataPublisher);
            _thread.IsBackground = true;

            client = new XDMessagingClient();
            broadcast = client.Broadcasters
                .GetBroadcasterForMode(XDTransportMode.HighPerformanceUI);
            _thread.Start();
        }
        private void runDataPublisher()
        {
            double _value;
            gyroModuleData _gyroModuleData = new gyroModuleData();
            mapModuleData _mapModuleData = new mapModuleData();
            angularGaugeData _angularGaugeData = new angularGaugeData();
            solidGaugeData _solidGaugeData = new solidGaugeData();
            lineChartData _lineChartData = new lineChartData();
            coloumnChartData _coloumnChartData = new coloumnChartData();
            Random _random = new Random();
            try
            {
                while (true)
                {
                    _value = Convert.ToDouble(_random.Next(0, 100));
                    _gyroModuleData.xAxisData = _value / 25;
                    _gyroModuleData.yAxisData = _value / 25;
                    _gyroModuleData.zAxisData = _value / 25;
                    _mapModuleData.xAxisValue = _value;
                    _mapModuleData.yAxisValue = _value;
                    _solidGaugeData.solidGaugeValue = _value;
                    _angularGaugeData.angularGaugeValue = _value;
                    _lineChartData.lineChartValue = _value;
                    _coloumnChartData.coloumnChartValue = _value;

                    this.broadcast.SendToChannel(ChannelList.gyroModule.ToString(), _gyroModuleData);
                    this.broadcast.SendToChannel(ChannelList.mapModule.ToString(), _mapModuleData);
                    this.broadcast.SendToChannel(ChannelList.angularGauge.ToString(), _angularGaugeData);
                    this.broadcast.SendToChannel(ChannelList.solidGauge.ToString(), _solidGaugeData);
                    this.broadcast.SendToChannel(ChannelList.lineChart.ToString(), _lineChartData);
                    this.broadcast.SendToChannel(ChannelList.columnChart.ToString(), _coloumnChartData);

                    //this.broadcast.SendToChannel(ChannelList.statusBar.ToString(),
                    //    new statusBarData(statusBarPoint.Normal, "injected value is : "+_value.ToString()));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }
    }
}
